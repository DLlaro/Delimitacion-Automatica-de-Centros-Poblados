using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Delimitador_CCPP_selva
{
    public partial class Form1 : Form
    {
        private Process? pythonServerProcess;
        private HttpClient httpClient;
        private const string SERVER_URL = "http://localhost:5000";

        public Form1()
        {
            InitializeComponent();
            btnEjecutar.Enabled = false;

            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(10);

            // Configurar status inicial
            ActualizarEstadoServidor("Iniciando servidor...", Color.Orange);

            // Iniciar servidor Python al cargar el form
            IniciarServidorPython();
        }

        private void ActualizarEstadoServidor(string mensaje, Color color)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ActualizarEstadoServidor(mensaje, color)));
                return;
            }

            lblServerStatus.Text = $"?? Servidor: {mensaje}";
            lblServerStatus.ForeColor = color;

            // Cambiar el icono según el estado
            if (mensaje.Contains("Listo") || mensaje.Contains("Conectado"))
            {
                lblServerStatus.Text = $"Servidor: {mensaje}";
            }
            else if (mensaje.Contains("Error") || mensaje.Contains("Detenido"))
            {
                lblServerStatus.Text = $"Servidor: {mensaje}";
            }
            else
            {
                lblServerStatus.Text = $"Servidor: {mensaje}";
            }
        }
        private void LimpiarProcesosPythonAnteriores()
        {
            try
            {
                AppendLog("Verificando procesos Python anteriores...\n", Color.Green);

                // Comando para matar procesos Python en WSL que usen el puerto 5000
                var psi = new ProcessStartInfo
                {
                    FileName = "wsl.exe",
                    Arguments = "bash -c \"lsof -ti:5000 | xargs kill -9 2>/dev/null || true\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(psi))
                {
                    process.WaitForExit(3000); // Esperar máximo 3 segundos
                }

                System.Threading.Thread.Sleep(1000); // Esperar 1 segundo para que se libere el puerto

                AppendLog("Procesos anteriores limpiados\n", Color.LimeGreen);
            }
            catch (Exception ex)
            {
                AppendLog($"Advertencia al limpiar procesos: {ex.Message}\n", Color.Orange);
            }
        }
        private void IniciarServidorPython()
        {
            try
            {
                LimpiarProcesosPythonAnteriores();

                AppendLog("Iniciando servidor Python...\n", Color.Green);
                ActualizarEstadoServidor("Cargando modelo...", Color.Orange);

                var psi = new ProcessStartInfo
                {
                    FileName = "wsl.exe",
                    Arguments = "$HOME/tf220/bin/python3 $HOME/Unet/Sistema/server.py",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                };

                pythonServerProcess = new Process();
                pythonServerProcess.StartInfo = psi;

                pythonServerProcess.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        AppendLog("[SERVER] " + e.Data + "\n", Color.Blue);

                        // Detectar cuando el servidor está listo
                        if (e.Data.Contains("Running on") || e.Data.Contains("Modelo cargado y listo"))
                        {
                            this.Invoke(new Action(() =>
                            {
                                btnEjecutar.Enabled = true;
                                ActualizarEstadoServidor("Listo", Color.LimeGreen);

                                AppendLog("Servidor listo para predicciones\n", Color.DarkGreen);
                            }));
                        }
                    }
                };

                pythonServerProcess.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        string[] noErrors = new string[]
                        {
                            "WARNING",
                            "FutureWarning",
                            "DeprecationWarning",
                            "UserWarning",
                            "Using TensorFlow backend",
                            "numpy",
                            "torch",
                            "CUDA",
                            "GPU"
                        };

                        // Solo mostrar si es un error real
                        bool isRealError = !noErrors.Any(pattern =>
                            e.Data.Contains(pattern, StringComparison.OrdinalIgnoreCase));

                        if (isRealError && (e.Data.Contains("Error") ||
                                            e.Data.Contains("Exception") ||
                                            e.Data.Contains("Failed") ||
                                            e.Data.Contains("Traceback")))
                        {
                            AppendLog("[ERR] " + e.Data + "\n", Color.Red);
                        }
                        else
                        {
                            // Mostrar como información en lugar de error
                            //AppendLog("[INFO] " + e.Data + "\n", Color.Orange);
                        }
                    }
                };
                pythonServerProcess.Exited += (s, e) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        ActualizarEstadoServidor("Detenido inesperadamente", Color.Red);
                        btnEjecutar.Enabled = false;
                    }));
                };

                pythonServerProcess.EnableRaisingEvents = true;
                pythonServerProcess.Start();
                pythonServerProcess.BeginOutputReadLine();
                pythonServerProcess.BeginErrorReadLine();

                AppendLog("Esperando a que el servidor cargue el modelo...\n", Color.Brown);
            }
            catch (Exception ex)
            {
                ActualizarEstadoServidor("Error al iniciar", Color.Red);

                MessageBox.Show($"Error al iniciar servidor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DetenerServidorPythonAsync()
        {
            try
            {
                if (pythonServerProcess != null && !pythonServerProcess.HasExited)
                {
                    AppendLog("Deteniendo servidor Python...\n", Color.Orange);

                    // 1. Intento suave
                    try
                    {
                        await httpClient.GetAsync($"{SERVER_URL}/shutdown");
                    }
                    catch { }

                    await Task.Delay(700);

                    // 2. Matar proceso real dentro de WSL
                    var killPsi = new ProcessStartInfo
                    {
                        FileName = "wsl.exe",
                        Arguments = "bash -c \"pkill -f server.py || true\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    Process.Start(killPsi);

                    await Task.Delay(500);

                    // 3. Matar proceso wrapper wsl.exe
                    if (!pythonServerProcess.HasExited)
                        pythonServerProcess.Kill(true);

                    pythonServerProcess.Dispose();
                    pythonServerProcess = null;

                    AppendLog("Servidor detenido completamente\n", Color.Gray);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Error al detener servidor: {ex.Message}\n", Color.Red);
            }
        }

        private bool isShuttingDown = false; // Bandera para evitar doble limpieza

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            if (!isShuttingDown)
            {
                isShuttingDown = true;
                e.Cancel = true; // Cancelar el cierre inmediato

                // mostrar ventana de cierre
                var closingForm = new FrmClosing();
                closingForm.Show();
                closingForm.Refresh(); // fuerza que se muestre

                await DetenerServidorPythonAsync();

                closingForm.Close();

                this.Close(); // Intentar cerrar nuevamente después de la limpieza
            }
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Limpieza adicional si es necesario
            httpClient?.Dispose();
            base.OnFormClosed(e);
        }

        string nombreArchivo = "";
        private void btnTiff_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Archivos TIFF (*.tif)|*.tif";
            ofd.Title = "Seleccione Imagen Satelital";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtTiff.Text = ofd.FileName;
                //Match match_nombreArchivo = Regex.Match(txtTiff.Text, @"[^\\]+$");
                //nombreArchivo = match_nombreArchivo.Value;
                txtSalida.Text = Path.ChangeExtension(txtTiff.Text, null) + "_prediccion.gpkg";
                if (File.Exists(ofd.FileName))
                {
                    picBoxOriginal.Image = Image.FromFile(ofd.FileName);
                    picBoxMask.Image = null;
                    picBoxPrediccion.Image = null;
                }
                else
                {
                    MessageBox.Show("Archivo TIFF inválido.");
                }
            }
        }

        private void btnPuntos_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "GPKG (*.gpkg)|*.gpkg";
            ofd.Title = "Seleccione puntos CCPP";

            if (ofd.ShowDialog() == DialogResult.OK)
                txtPuntos.Text = ofd.FileName;
        }
        private void btnSalida_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = Path.GetDirectoryName(txtTiff.Text);
                sfd.Title = "Seleccione la carpeta y escriba el nombre del archivo";
                sfd.Filter = "GeoPackage (*.gpkg)|*.gpkg";
                sfd.FileName = Path.GetFileNameWithoutExtension(txtTiff.Text) + "_predic.gpkg"; // nombre por defecto

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtSalida.Text = Path.GetFullPath(sfd.FileName);

                }
            }
        }
        private async void btnEjecutar_Click_1(object sender, EventArgs e)
        {
            if (!File.Exists(txtTiff.Text))
            {
                MessageBox.Show("Seleccione un archivo TIFF válido.");
                return;
            }

            if (!File.Exists(txtPuntos.Text))
            {
                MessageBox.Show("Seleccione un archivo GPKG válido.");
                return;
            }

            btnEjecutar.Enabled = false;
            ActualizarEstadoServidor("Procesando predicción...", Color.Brown);

            string gpkgOutWin = Path.Combine(txtSalida.Text);
            string tempFolder = Path.GetTempPath();
            string maskOutWin = Path.Combine(tempFolder, $"{Guid.NewGuid()}_mask.png");

            // Convert to WSL paths
            string tifWSL = ToWslPath(txtTiff.Text);
            string puntosWSL = ToWslPath(txtPuntos.Text);
            string gpkgWSL = ToWslPath(gpkgOutWin);
            string maskWSL = ToWslPath(maskOutWin);

            bool area_valida = VerificarAreaMinima();
            if (!area_valida)
            {
                return;
            }

            AppendLog("\n=== Iniciando predicción ===\n", Color.Brown);
            AppendLog($"Imagen: {txtTiff.Text}\n", Color.Gray);

            try
            {
                var payload = new
                {
                    tif_path = tifWSL,
                    puntos_path = puntosWSL,
                    gpkg_path = gpkgWSL,
                    mask_png_path = maskWSL,
                    area_minima = comBoxArea.Text,
                    interseccion = cheBoxInterseccion.Checked
                };

                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                AppendLog("Enviando petición al servidor...\n", Color.Black);


                var response = await httpClient.PostAsync($"{SERVER_URL}/predict", content);
                var result = await response.Content.ReadAsStringAsync();

                AppendLog($"Respuesta del servidor: {result}\n", Color.LimeGreen);

                var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(result);


                if (jsonResponse != null && jsonResponse.ContainsKey("status"))
                {
                    string status = jsonResponse["status"].GetString();

                    if (status == "OK")

                    {
                        CargarResultados(maskOutWin);

                        int polysDetected = jsonResponse.ContainsKey("polygons_detected")
                            ? jsonResponse["polygons_detected"].GetInt32() : 0;
                        int polysMatched = jsonResponse.ContainsKey("polygons_matched")
                            ? jsonResponse["polygons_matched"].GetInt32() : 0;
                        ActualizarEstadoServidor($"Listo - {polysMatched} polígonos encontrados", Color.LimeGreen);

                        MessageBox.Show(
                            $"Predicción completada!\n\n" +
                            $"Polígonos detectados: {polysDetected}\n" +
                            $"Polígonos con punto CCPP: {polysMatched}",
                            "Éxito",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else if (status == "NO_POLYGONS")
                    {
                        ActualizarEstadoServidor("Listo - Sin polígonos", Color.Orange);

                        MessageBox.Show("No se detectaron polígonos en la imagen.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (status == "NO_MATCH")

                    {
                        ActualizarEstadoServidor("Listo - Sin coincidencias", Color.Orange);

                        MessageBox.Show("Los polígonos detectados no intersectan con ningún punto CCPP.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (status == "ERROR")


                    {
                        string errorMsg = jsonResponse.ContainsKey("message")
                            ? jsonResponse["message"].GetString() : "Error desconocido";
                        ActualizarEstadoServidor("Error en predicción", Color.Red);

                        MessageBox.Show($"Error en el servidor: {errorMsg}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException ex)


            {
                AppendLog($"[ERROR] No se pudo conectar al servidor: {ex.Message}\n", Color.Red);
                ActualizarEstadoServidor("Error de conexión", Color.Red);

                MessageBox.Show(
                    "No se pudo conectar al servidor Python.\n" +
                    "Asegúrese de que el servidor esté ejecutándose.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                AppendLog($"[ERROR] {ex.Message}\n", Color.Red);
                ActualizarEstadoServidor("Error", Color.Red);

                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEjecutar.Enabled = true;
                if (lblServerStatus.Text.Contains("Procesando"))
                {
                    ActualizarEstadoServidor("Listo", Color.LimeGreen);
                }
                AppendLog("=== Predicción finalizada ===\n\n", Color.Brown);
            }
        }

        private string ToWslPath(string winPath)
        {
            if (winPath.StartsWith(@"\\wsl.localhost"))



            {
                string p = winPath
                    .Replace(@"\\wsl.localhost\Ubuntu", "")
                    .Replace("\\", "/");
                return p;
            }
            if (winPath.StartsWith("/"))
                return winPath;
            string path = winPath.Replace("\\", "/");
            string drive = path.Substring(0, 1).ToLower();
            string rest = path.Substring(2);
            return $"/mnt/{drive}{rest}";
        }

        private void CargarResultados(string maskPathWin)
        {
            if (!File.Exists(maskPathWin))
            {
                MessageBox.Show("La máscara generada no se encontró.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Bitmap maskBitmap;
                using (var stream = new FileStream(maskPathWin, FileMode.Open, FileAccess.Read))
                {
                    maskBitmap = new Bitmap(stream);
                }
                picBoxMask.Image = maskBitmap;

                Bitmap original = new Bitmap(txtTiff.Text);
                picBoxPrediccion.Image = Superponer(original, maskBitmap);
                if (File.Exists(maskPathWin))
                {
                    File.Delete(maskPathWin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar resultados: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppendLog(string text, Color color)
        {
            if (richOutput.InvokeRequired)
            {
                richOutput.Invoke(new Action(() => AppendLog(text, color)));
            }
            else
            {
                richOutput.SelectionStart = richOutput.TextLength;
                richOutput.SelectionColor = color;
                richOutput.AppendText(text);
                richOutput.ScrollToCaret();
            }
        }

        private Bitmap Superponer(Bitmap original, Bitmap mask)
        {
            // 1. Redimensionar máscara si no coincide
            if (mask.Width != original.Width || mask.Height != original.Height)
            {
                mask = new Bitmap(mask, new Size(original.Width, original.Height));
            }

            Bitmap result = new Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb);

            Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);

            BitmapData dataOriginal = original.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dataMask = mask.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dataResult = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int strideO = dataOriginal.Stride;
            int strideM = dataMask.Stride;
            int strideR = dataResult.Stride;

            unsafe
            {
                byte* ptrO = (byte*)dataOriginal.Scan0;
                byte* ptrM = (byte*)dataMask.Scan0;
                byte* ptrR = (byte*)dataResult.Scan0;

                float alpha = 150f / 255f;  // Transparencia fija 150

                for (int y = 0; y < original.Height; y++)
                {
                    byte* rowO = ptrO + (y * strideO);
                    byte* rowM = ptrM + (y * strideM);
                    byte* rowR = ptrR + (y * strideR);

                    for (int x = 0; x < original.Width; x++)
                    {
                        // B G R (24 bits)
                        int idx = x * 3;

                        byte bO = rowO[idx];
                        byte gO = rowO[idx + 1];
                        byte rO = rowO[idx + 2];

                        byte bM = rowM[idx];
                        byte gM = rowM[idx + 1];
                        byte rM = rowM[idx + 2];

                        // los pixeles de la mascara son 1
                        if (rM > 0 || gM > 0 || bM > 0)
                        {
                            byte rC = 255;  // rojo
                            byte gC = 0;
                            byte bC = 0;

                            rowR[idx] = (byte)(bO * (1 - alpha) + bC * alpha);
                            rowR[idx + 1] = (byte)(gO * (1 - alpha) + gC * alpha);
                            rowR[idx + 2] = (byte)(rO * (1 - alpha) + rC * alpha);
                        }
                        else
                        {
                            rowR[idx] = bO;
                            rowR[idx + 1] = gO;
                            rowR[idx + 2] = rO;
                        }
                    }
                }
            }

            original.UnlockBits(dataOriginal);
            mask.UnlockBits(dataMask);
            result.UnlockBits(dataResult);

            return result;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAreaMinima.Text = comBoxArea.Text;
        }

        private void txtAreaMinima_TextChanged(object sender, EventArgs e)
        {
            btnEjecutar.Enabled = true;
        }

        private void cboxHabilitarArea_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxHabilitarArea.Checked)
            {
                //txtAreaMinima.Enabled = true;
                comBoxArea.Enabled = true;
            }
            else
            {
                //txtAreaMinima.Enabled = false;
                comBoxArea.Enabled = false;
            }
        }

        private bool VerificarAreaMinima()
        {
            if (!cboxHabilitarArea.Checked)
            {
                return true; // Si no está habilitado, consideramos que pasa la validación
            }

            string textoArea = comBoxArea.Text.Trim();

            // Verificar que no esté vacío y sea un número válido
            if (string.IsNullOrWhiteSpace(textoArea) || !Regex.IsMatch(textoArea, @"^\d+$"))
            {
                MessageBox.Show("Área Mínima inválida, ingrese un área correcta");
                return false;
            }

            // Verificar que el número sea mayor que cero
            if (int.TryParse(textoArea, out int areaMinima) && areaMinima <= 0)
            {
                MessageBox.Show("El área mínima debe ser mayor que cero");
                return false;
            }

            btnEjecutar.Enabled = true; // Habilitar el botón si la validación es exitosa
            return true;
        }

        private void comBoxArea_TextChanged(object sender, EventArgs e)
        {
            btnEjecutar.Enabled = true;
        }
    }
}
