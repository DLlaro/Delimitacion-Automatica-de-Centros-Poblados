import numpy as np
import tensorflow as tf
import os
import math
import cv2
import rasterio
import albumentations as A

class TIFFDataGeneratorAug(tf.keras.utils.Sequence):
    def __init__(self, image_paths, mask_paths,
                 batch_size=2, shuffle=True,
                 normalize=True,
                 target_size=(512, 512),
                 n_channels=3,
                 augment=False):
        
        self.image_paths = image_paths
        self.mask_paths = mask_paths
        self.batch_size = batch_size
        self.shuffle = shuffle
        self.normalize = normalize
        self.target_size = target_size
        self.n_channels = n_channels
        self.augment = augment
        
        # -------- DATA AUGMENTATION --------
        self.augmentation = A.Compose([
            A.HorizontalFlip(p=0.5),
            A.VerticalFlip(p=0.5),
            A.RandomRotate90(p=0.5),
            A.ShiftScaleRotate(
                shift_limit=0.03,
                scale_limit=0.05,
                rotate_limit=10,
                border_mode=cv2.BORDER_REFLECT_101,
                p=0.4
            ),
            A.RandomBrightnessContrast(p=0.2),
            A.GaussNoise(var_limit=(3, 10), p=0.1)
        ], additional_targets={'mask': 'mask'})
        
        self.on_epoch_end()

    def __len__(self):
        return math.ceil(len(self.image_paths) / self.batch_size)

    def on_epoch_end(self):
        self.indexes = np.arange(len(self.image_paths))
        if self.shuffle:
            np.random.shuffle(self.indexes)

    def __getitem__(self, idx):

        batch_indexes = self.indexes[idx*self.batch_size:(idx+1)*self.batch_size]
        batch_imgs, batch_masks = [], []

        for i in batch_indexes:

            # -------- LEER IMAGEN --------
            with rasterio.open(self.image_paths[i]) as src:
                img = src.read()  # (C, H, W)
                img = np.transpose(img, (1, 2, 0))  # (H, W, C)

            # -------- LEER MÁSCARA --------
            with rasterio.open(self.mask_paths[i]) as src:
                mask = src.read(1)

            img = img.astype(np.float32)
            mask = mask.astype(np.float32)

            # -------- AJUSTE DEL NÚMERO DE CANALES --------
            if img.shape[-1] > self.n_channels:
                img = img[..., :self.n_channels]
            elif img.shape[-1] < self.n_channels:
                img = np.concatenate([img] * (self.n_channels // img.shape[-1] + 1), axis=-1)
                img = img[..., :self.n_channels]

            # -------- REDIMENSIONAR --------
            if img.shape[:2] != self.target_size:
                img = cv2.resize(img, self.target_size, interpolation=cv2.INTER_LINEAR)
            if mask.shape[:2] != self.target_size:
                mask = cv2.resize(mask, self.target_size, interpolation=cv2.INTER_NEAREST)

            # -------- NORMALIZACIÓN --------
            if self.normalize:
                img_min, img_max = np.min(img), np.max(img)
                if img_max > img_min:
                    img = (img - img_min) / (img_max - img_min)
                else:
                    img = np.zeros_like(img)

            # -------- APLICAR DATA AUGMENTATION --------
            if self.augment:
                augmented = self.augmentation(image=img, mask=mask)
                img = augmented["image"]
                mask = augmented["mask"]

            mask = (mask > 0.5).astype(np.float32)

            # -------- FORMATO FINAL DE LA MÁSCARA --------
            if mask.ndim == 2:
                mask = np.expand_dims(mask, axis=-1)
            else:
                mask = mask[..., :1]

            batch_imgs.append(img)
            batch_masks.append(mask)

        X = np.stack(batch_imgs, axis=0)
        y = np.stack(batch_masks, axis=0)
        return X, y
