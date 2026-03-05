namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    partial class frmExportacionImagenes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProgresoIndividual = new System.Windows.Forms.Label();
            this.ProgressBarExportacíon = new System.Windows.Forms.ProgressBar();
            this.RutaTextBox = new Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl();
            this.GenerarButton = new System.Windows.Forms.Button();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.dtFechaProcesoFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaProcesoInicial = new System.Windows.Forms.DateTimePicker();
            this.lblFechaProceso = new System.Windows.Forms.Label();
            this.cbFormatosImagenes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorkerExportacionImagenes = new System.ComponentModel.BackgroundWorker();
            this.lblProgresoGeneral = new System.Windows.Forms.Label();
            this.ProgressBarExportacíonGeneral = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProgresoGeneral);
            this.groupBox1.Controls.Add(this.ProgressBarExportacíonGeneral);
            this.groupBox1.Controls.Add(this.lblProgresoIndividual);
            this.groupBox1.Controls.Add(this.ProgressBarExportacíon);
            this.groupBox1.Controls.Add(this.RutaTextBox);
            this.groupBox1.Controls.Add(this.GenerarButton);
            this.groupBox1.Controls.Add(this.SelectFolderButton);
            this.groupBox1.Controls.Add(this.dtFechaProcesoFinal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaProcesoInicial);
            this.groupBox1.Controls.Add(this.lblFechaProceso);
            this.groupBox1.Controls.Add(this.cbFormatosImagenes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 446);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exportación Imagenes";
            // 
            // lblProgresoIndividual
            // 
            this.lblProgresoIndividual.AutoSize = true;
            this.lblProgresoIndividual.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgresoIndividual.Location = new System.Drawing.Point(11, 369);
            this.lblProgresoIndividual.Name = "lblProgresoIndividual";
            this.lblProgresoIndividual.Size = new System.Drawing.Size(138, 13);
            this.lblProgresoIndividual.TabIndex = 72;
            this.lblProgresoIndividual.Text = "Progreso Individual (%)";
            this.lblProgresoIndividual.Visible = false;
            // 
            // ProgressBarExportacíon
            // 
            this.ProgressBarExportacíon.Location = new System.Drawing.Point(12, 385);
            this.ProgressBarExportacíon.MarqueeAnimationSpeed = 10;
            this.ProgressBarExportacíon.Name = "ProgressBarExportacíon";
            this.ProgressBarExportacíon.Size = new System.Drawing.Size(389, 16);
            this.ProgressBarExportacíon.Step = 1;
            this.ProgressBarExportacíon.TabIndex = 62;
            this.ProgressBarExportacíon.Visible = false;
            // 
            // RutaTextBox
            // 
            this.RutaTextBox.AllowPromptAsInput = false;
            this.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.RutaTextBox.FocusOut = System.Drawing.Color.White;
            this.RutaTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RutaTextBox.Location = new System.Drawing.Point(26, 177);
            this.RutaTextBox.MinLength = 0;
            this.RutaTextBox.Name = "RutaTextBox";
            this.RutaTextBox.PromptChar = ' ';
            this.RutaTextBox.ResetOnPrompt = false;
            this.RutaTextBox.ResetOnSpace = false;
            this.RutaTextBox.Size = new System.Drawing.Size(312, 20);
            this.RutaTextBox.TabIndex = 61;
            this.RutaTextBox.Click += new System.EventHandler(this.RutaTextBox_Click);
            // 
            // GenerarButton
            // 
            this.GenerarButton.BackColor = System.Drawing.SystemColors.Control;
            this.GenerarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerarButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerarButton.Image = global::BcoItau.Plugin.Properties.Resources.Process_Accept;
            this.GenerarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GenerarButton.Location = new System.Drawing.Point(150, 223);
            this.GenerarButton.Name = "GenerarButton";
            this.GenerarButton.Size = new System.Drawing.Size(100, 60);
            this.GenerarButton.TabIndex = 60;
            this.GenerarButton.Tag = "Ctrl + P";
            this.GenerarButton.Text = "&Generar";
            this.GenerarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GenerarButton.UseVisualStyleBackColor = false;
            this.GenerarButton.Click += new System.EventHandler(this.GenerarButton_Click);
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Image = global::BcoItau.Plugin.Properties.Resources.MainFolder;
            this.SelectFolderButton.Location = new System.Drawing.Point(357, 175);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(27, 23);
            this.SelectFolderButton.TabIndex = 59;
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // dtFechaProcesoFinal
            // 
            this.dtFechaProcesoFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFechaProcesoFinal.Location = new System.Drawing.Point(150, 105);
            this.dtFechaProcesoFinal.Name = "dtFechaProcesoFinal";
            this.dtFechaProcesoFinal.Size = new System.Drawing.Size(226, 20);
            this.dtFechaProcesoFinal.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Fecha Recaudo Final:";
            // 
            // dtpFechaProcesoInicial
            // 
            this.dtpFechaProcesoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaProcesoInicial.Location = new System.Drawing.Point(150, 67);
            this.dtpFechaProcesoInicial.Name = "dtpFechaProcesoInicial";
            this.dtpFechaProcesoInicial.Size = new System.Drawing.Size(226, 20);
            this.dtpFechaProcesoInicial.TabIndex = 52;
            // 
            // lblFechaProceso
            // 
            this.lblFechaProceso.AutoSize = true;
            this.lblFechaProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaProceso.Location = new System.Drawing.Point(11, 72);
            this.lblFechaProceso.Name = "lblFechaProceso";
            this.lblFechaProceso.Size = new System.Drawing.Size(139, 13);
            this.lblFechaProceso.TabIndex = 51;
            this.lblFechaProceso.Text = "Fecha Recaudo Inicial:";
            // 
            // cbFormatosImagenes
            // 
            this.cbFormatosImagenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormatosImagenes.FormattingEnabled = true;
            this.cbFormatosImagenes.Location = new System.Drawing.Point(150, 29);
            this.cbFormatosImagenes.Name = "cbFormatosImagenes";
            this.cbFormatosImagenes.Size = new System.Drawing.Size(226, 21);
            this.cbFormatosImagenes.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Formato Salida:";
            // 
            // backgroundWorkerExportacionImagenes
            // 
            this.backgroundWorkerExportacionImagenes.WorkerReportsProgress = true;
            this.backgroundWorkerExportacionImagenes.WorkerSupportsCancellation = true;
            this.backgroundWorkerExportacionImagenes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerExportacionImagenes_DoWork);
            this.backgroundWorkerExportacionImagenes.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerExportacionImagenes_ProgressChanged);
            this.backgroundWorkerExportacionImagenes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerExportacionImagenes_RunWorkerCompleted);
            // 
            // lblProgresoGeneral
            // 
            this.lblProgresoGeneral.AutoSize = true;
            this.lblProgresoGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgresoGeneral.Location = new System.Drawing.Point(11, 328);
            this.lblProgresoGeneral.Name = "lblProgresoGeneral";
            this.lblProgresoGeneral.Size = new System.Drawing.Size(127, 13);
            this.lblProgresoGeneral.TabIndex = 74;
            this.lblProgresoGeneral.Text = "Progreso General (%)";
            this.lblProgresoGeneral.Visible = false;
            // 
            // ProgressBarExportacíonGeneral
            // 
            this.ProgressBarExportacíonGeneral.Location = new System.Drawing.Point(12, 344);
            this.ProgressBarExportacíonGeneral.MarqueeAnimationSpeed = 10;
            this.ProgressBarExportacíonGeneral.Name = "ProgressBarExportacíonGeneral";
            this.ProgressBarExportacíonGeneral.Size = new System.Drawing.Size(389, 16);
            this.ProgressBarExportacíonGeneral.Step = 1;
            this.ProgressBarExportacíonGeneral.TabIndex = 73;
            this.ProgressBarExportacíonGeneral.Visible = false;
            // 
            // frmExportacionImagenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 464);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportacionImagenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportación de Imagenes";
            this.Load += new System.EventHandler(this.frmExportacionImagenes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFormatosImagenes;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker dtFechaProcesoFinal;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker dtpFechaProcesoInicial;
        internal System.Windows.Forms.Label lblFechaProceso;
        internal System.Windows.Forms.Button SelectFolderButton;
        internal System.Windows.Forms.Button GenerarButton;
        private System.ComponentModel.BackgroundWorker backgroundWorkerExportacionImagenes;
        private Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl RutaTextBox;
        internal System.Windows.Forms.ProgressBar ProgressBarExportacíon;
        internal System.Windows.Forms.Label lblProgresoIndividual;
        internal System.Windows.Forms.Label lblProgresoGeneral;
        internal System.Windows.Forms.ProgressBar ProgressBarExportacíonGeneral;

    }
}