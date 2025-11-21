namespace Delimitador_CCPP_selva
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTiff = new Button();
            btnPuntos = new Button();
            txtTiff = new TextBox();
            txtPuntos = new TextBox();
            picBoxOriginal = new PictureBox();
            picBoxPrediccion = new PictureBox();
            txtSalida = new TextBox();
            picBoxMask = new PictureBox();
            label1 = new Label();
            btnSalida = new Button();
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            label6 = new Label();
            label4 = new Label();
            label5 = new Label();
            statusStrip1 = new StatusStrip();
            lblServerStatus = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            richOutput = new RichTextBox();
            btnEjecutar = new Button();
            groupBox3 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label3 = new Label();
            cheBoxInterseccion = new CheckBox();
            comBoxArea = new ComboBox();
            cboxHabilitarArea = new CheckBox();
            label2 = new Label();
            txtAreaMinima = new TextBox();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)picBoxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxPrediccion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxMask).BeginInit();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnTiff
            // 
            btnTiff.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(btnTiff, 2);
            btnTiff.Dock = DockStyle.Fill;
            btnTiff.Location = new Point(64, 4);
            btnTiff.Margin = new Padding(4, 4, 4, 4);
            btnTiff.Name = "btnTiff";
            btnTiff.Size = new Size(111, 61);
            btnTiff.TabIndex = 0;
            btnTiff.Text = "Seleccionar Tiff";
            btnTiff.UseVisualStyleBackColor = true;
            btnTiff.Click += btnTiff_Click;
            // 
            // btnPuntos
            // 
            btnPuntos.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(btnPuntos, 2);
            btnPuntos.Dock = DockStyle.Fill;
            btnPuntos.Location = new Point(64, 73);
            btnPuntos.Margin = new Padding(4, 4, 4, 4);
            btnPuntos.Name = "btnPuntos";
            btnPuntos.Size = new Size(111, 56);
            btnPuntos.TabIndex = 1;
            btnPuntos.Text = "Seleccionar Puntos";
            btnPuntos.UseVisualStyleBackColor = true;
            btnPuntos.Click += btnPuntos_Click;
            // 
            // txtTiff
            // 
            tableLayoutPanel1.SetColumnSpan(txtTiff, 15);
            txtTiff.Dock = DockStyle.Fill;
            txtTiff.Location = new Point(183, 4);
            txtTiff.Margin = new Padding(4, 4, 4, 4);
            txtTiff.Name = "txtTiff";
            txtTiff.ReadOnly = true;
            txtTiff.Size = new Size(892, 27);
            txtTiff.TabIndex = 4;
            // 
            // txtPuntos
            // 
            tableLayoutPanel1.SetColumnSpan(txtPuntos, 15);
            txtPuntos.Dock = DockStyle.Fill;
            txtPuntos.Location = new Point(183, 73);
            txtPuntos.Margin = new Padding(4, 4, 4, 4);
            txtPuntos.Name = "txtPuntos";
            txtPuntos.ReadOnly = true;
            txtPuntos.Size = new Size(892, 27);
            txtPuntos.TabIndex = 5;
            // 
            // picBoxOriginal
            // 
            picBoxOriginal.BorderStyle = BorderStyle.FixedSingle;
            picBoxOriginal.Dock = DockStyle.Fill;
            picBoxOriginal.Location = new Point(4, 26);
            picBoxOriginal.Margin = new Padding(4, 4, 4, 4);
            picBoxOriginal.Name = "picBoxOriginal";
            tableLayoutPanel2.SetRowSpan(picBoxOriginal, 3);
            picBoxOriginal.Size = new Size(346, 354);
            picBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxOriginal.TabIndex = 6;
            picBoxOriginal.TabStop = false;
            // 
            // picBoxPrediccion
            // 
            picBoxPrediccion.BorderStyle = BorderStyle.FixedSingle;
            picBoxPrediccion.Dock = DockStyle.Fill;
            picBoxPrediccion.Location = new Point(712, 26);
            picBoxPrediccion.Margin = new Padding(4, 4, 4, 4);
            picBoxPrediccion.Name = "picBoxPrediccion";
            tableLayoutPanel2.SetRowSpan(picBoxPrediccion, 3);
            picBoxPrediccion.Size = new Size(347, 354);
            picBoxPrediccion.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxPrediccion.TabIndex = 7;
            picBoxPrediccion.TabStop = false;
            // 
            // txtSalida
            // 
            tableLayoutPanel1.SetColumnSpan(txtSalida, 14);
            txtSalida.Dock = DockStyle.Fill;
            txtSalida.Location = new Point(183, 137);
            txtSalida.Margin = new Padding(4, 4, 4, 4);
            txtSalida.Name = "txtSalida";
            txtSalida.Size = new Size(832, 27);
            txtSalida.TabIndex = 9;
            // 
            // picBoxMask
            // 
            picBoxMask.BorderStyle = BorderStyle.FixedSingle;
            picBoxMask.Dock = DockStyle.Fill;
            picBoxMask.Location = new Point(358, 26);
            picBoxMask.Margin = new Padding(4, 4, 4, 4);
            picBoxMask.Name = "picBoxMask";
            tableLayoutPanel2.SetRowSpan(picBoxMask, 3);
            picBoxMask.Size = new Size(346, 354);
            picBoxMask.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxMask.TabIndex = 10;
            picBoxMask.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Location = new Point(64, 134);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(86, 40);
            label1.TabIndex = 11;
            label1.Text = "Carpeta de Salida:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSalida
            // 
            btnSalida.Dock = DockStyle.Fill;
            btnSalida.Location = new Point(1023, 137);
            btnSalida.Margin = new Padding(4, 4, 4, 4);
            btnSalida.Name = "btnSalida";
            btnSalida.Size = new Size(52, 35);
            btnSalida.TabIndex = 14;
            btnSalida.Text = "...";
            btnSalida.UseVisualStyleBackColor = true;
            btnSalida.Click += btnSalida_Click;
            // 
            // groupBox1
            // 
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.SetColumnSpan(groupBox1, 18);
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(64, 290);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.SetRowSpan(groupBox1, 7);
            groupBox1.Size = new Size(1071, 412);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(picBoxOriginal, 0, 1);
            tableLayoutPanel2.Controls.Add(label6, 2, 0);
            tableLayoutPanel2.Controls.Add(picBoxMask, 1, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 0);
            tableLayoutPanel2.Controls.Add(picBoxPrediccion, 2, 1);
            tableLayoutPanel2.Controls.Add(label5, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(4, 24);
            tableLayoutPanel2.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 6.29370642F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 60.48951F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel2.Size = new Size(1063, 384);
            tableLayoutPanel2.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(712, 0);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(91, 20);
            label6.TabIndex = 12;
            label6.Text = "Intersección:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 0);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 11;
            label4.Text = "Imagen:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(358, 0);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(67, 20);
            label5.TabIndex = 11;
            label5.Text = "Máscara:";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblServerStatus });
            statusStrip1.Location = new Point(0, 935);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 15, 0);
            statusStrip1.Size = new Size(1204, 26);
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblServerStatus
            // 
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(151, 20);
            lblServerStatus.Text = "toolStripStatusLabel1";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 20;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.88058138F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.088266F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(label1, 1, 2);
            tableLayoutPanel1.Controls.Add(txtTiff, 3, 0);
            tableLayoutPanel1.Controls.Add(txtPuntos, 3, 1);
            tableLayoutPanel1.Controls.Add(txtSalida, 3, 2);
            tableLayoutPanel1.Controls.Add(btnSalida, 17, 2);
            tableLayoutPanel1.Controls.Add(richOutput, 1, 12);
            tableLayoutPanel1.Controls.Add(btnPuntos, 1, 1);
            tableLayoutPanel1.Controls.Add(btnTiff, 1, 0);
            tableLayoutPanel1.Controls.Add(btnEjecutar, 16, 13);
            tableLayoutPanel1.Controls.Add(groupBox3, 1, 3);
            tableLayoutPanel1.Controls.Add(groupBox1, 1, 5);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 20;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.263069F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.757167F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.227656F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.184012F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.08898926F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.99396658F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.07547235F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 0.9433963F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.Size = new Size(1204, 961);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // richOutput
            // 
            tableLayoutPanel1.SetColumnSpan(richOutput, 14);
            richOutput.Dock = DockStyle.Fill;
            richOutput.Location = new Point(64, 710);
            richOutput.Margin = new Padding(4, 4, 4, 4);
            richOutput.Name = "richOutput";
            tableLayoutPanel1.SetRowSpan(richOutput, 6);
            richOutput.Size = new Size(831, 192);
            richOutput.TabIndex = 18;
            richOutput.Text = "";
            // 
            // btnEjecutar
            // 
            tableLayoutPanel1.SetColumnSpan(btnEjecutar, 3);
            btnEjecutar.Dock = DockStyle.Fill;
            btnEjecutar.FlatAppearance.BorderColor = Color.Gainsboro;
            btnEjecutar.FlatAppearance.BorderSize = 3;
            btnEjecutar.FlatStyle = FlatStyle.System;
            btnEjecutar.Location = new Point(963, 769);
            btnEjecutar.Margin = new Padding(4, 4, 4, 4);
            btnEjecutar.Name = "btnEjecutar";
            tableLayoutPanel1.SetRowSpan(btnEjecutar, 4);
            btnEjecutar.Size = new Size(172, 108);
            btnEjecutar.TabIndex = 17;
            btnEjecutar.Text = "Ejecutar";
            btnEjecutar.UseVisualStyleBackColor = true;
            btnEjecutar.Click += btnEjecutar_Click_1;
            // 
            // groupBox3
            // 
            tableLayoutPanel1.SetColumnSpan(groupBox3, 12);
            groupBox3.Controls.Add(tableLayoutPanel3);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(64, 180);
            groupBox3.Margin = new Padding(4, 4, 4, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.SetRowSpan(groupBox3, 2);
            groupBox3.Size = new Size(711, 102);
            groupBox3.TabIndex = 29;
            groupBox3.TabStop = false;
            groupBox3.Text = "Filtros:";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 5;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.33048F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.6695156F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 129F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18F));
            tableLayoutPanel3.Controls.Add(label3, 0, 1);
            tableLayoutPanel3.Controls.Add(cheBoxInterseccion, 1, 1);
            tableLayoutPanel3.Controls.Add(comBoxArea, 2, 0);
            tableLayoutPanel3.Controls.Add(cboxHabilitarArea, 1, 0);
            tableLayoutPanel3.Controls.Add(label2, 0, 0);
            tableLayoutPanel3.Controls.Add(txtAreaMinima, 4, 0);
            tableLayoutPanel3.Controls.Add(label7, 3, 0);
            tableLayoutPanel3.Dock = DockStyle.Left;
            tableLayoutPanel3.Location = new Point(4, 24);
            tableLayoutPanel3.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel3.Size = new Size(705, 74);
            tableLayoutPanel3.TabIndex = 29;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(4, 45);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(282, 20);
            label3.TabIndex = 27;
            label3.Text = "Solo polígonos intersectados con puntos:";
            // 
            // cheBoxInterseccion
            // 
            cheBoxInterseccion.Anchor = AnchorStyles.None;
            cheBoxInterseccion.AutoSize = true;
            cheBoxInterseccion.Location = new Point(353, 47);
            cheBoxInterseccion.Margin = new Padding(4, 4, 4, 4);
            cheBoxInterseccion.Name = "cheBoxInterseccion";
            cheBoxInterseccion.Size = new Size(18, 17);
            cheBoxInterseccion.TabIndex = 28;
            cheBoxInterseccion.UseVisualStyleBackColor = true;
            // 
            // comBoxArea
            // 
            comBoxArea.Anchor = AnchorStyles.Left;
            comBoxArea.Enabled = false;
            comBoxArea.FormattingEnabled = true;
            comBoxArea.Items.AddRange(new object[] { "1000", "2000", "4000", "6000" });
            comBoxArea.Location = new Point(397, 4);
            comBoxArea.Margin = new Padding(4, 4, 4, 4);
            comBoxArea.Name = "comBoxArea";
            comBoxArea.Size = new Size(112, 28);
            comBoxArea.TabIndex = 24;
            comBoxArea.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            comBoxArea.TextChanged += comBoxArea_TextChanged;
            // 
            // cboxHabilitarArea
            // 
            cboxHabilitarArea.Anchor = AnchorStyles.None;
            cboxHabilitarArea.AutoSize = true;
            cboxHabilitarArea.Location = new Point(353, 10);
            cboxHabilitarArea.Margin = new Padding(4, 4, 4, 4);
            cboxHabilitarArea.Name = "cboxHabilitarArea";
            cboxHabilitarArea.Size = new Size(18, 17);
            cboxHabilitarArea.TabIndex = 25;
            cboxHabilitarArea.UseVisualStyleBackColor = true;
            cboxHabilitarArea.CheckedChanged += cboxHabilitarArea_CheckedChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(4, 8);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(232, 20);
            label2.TabIndex = 19;
            label2.Text = "Área mínima (m^2) de detección:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtAreaMinima
            // 
            txtAreaMinima.Anchor = AnchorStyles.None;
            txtAreaMinima.BorderStyle = BorderStyle.FixedSingle;
            txtAreaMinima.Enabled = false;
            txtAreaMinima.Location = new Point(596, 5);
            txtAreaMinima.Margin = new Padding(4, 4, 4, 4);
            txtAreaMinima.Name = "txtAreaMinima";
            txtAreaMinima.Size = new Size(87, 27);
            txtAreaMinima.TabIndex = 23;
            txtAreaMinima.TextChanged += txtAreaMinima_TextChanged;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(529, 0);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(40, 37);
            label7.TabIndex = 29;
            label7.Text = "Área:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new Size(1204, 961);
            Controls.Add(statusStrip1);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "Demarcación de CCPP Selva";
            ((System.ComponentModel.ISupportInitialize)picBoxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxPrediccion).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxMask).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTiff;
        private Button btnPuntos;
        private TextBox txtTiff;
        private TextBox txtPuntos;
        private PictureBox picBoxOriginal;
        private PictureBox picBoxPrediccion;
        private TextBox txtSalida;
        private PictureBox picBoxMask;
        private Label label1;
        private Button btnSalida;
        private GroupBox groupBox1;
        private Label label6;
        private Label label5;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblServerStatus;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnEjecutar;
        private RichTextBox richOutput;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private ComboBox comBoxArea;
        private CheckBox cboxHabilitarArea;
        private GroupBox groupBox3;
        private Label label3;
        private CheckBox cheBoxInterseccion;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox txtAreaMinima;
        private Label label7;
    }
}
