namespace BcoBogota.Plugin.Imaging.Impuestos.Forms.Configuracion
{
    partial class frmFechaRecaudo_Doble
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
            this.lblFechaRecaudoInicial = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbxBase = new System.Windows.Forms.GroupBox();
            this.lblFechaRecaudoFinal = new System.Windows.Forms.Label();
            this.dtpFechaRecaudoFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.RutaTextBox_Fecha = new Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl();
            this.SelectFolderButton_Fecha = new System.Windows.Forms.Button();
            this.lblRuta_FechaRecaudo = new System.Windows.Forms.Label();
            this.gbxBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFechaRecaudoInicial
            // 
            this.lblFechaRecaudoInicial.AutoSize = true;
            this.lblFechaRecaudoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaRecaudoInicial.Location = new System.Drawing.Point(5, 22);
            this.lblFechaRecaudoInicial.Name = "lblFechaRecaudoInicial";
            this.lblFechaRecaudoInicial.Size = new System.Drawing.Size(150, 15);
            this.lblFechaRecaudoInicial.TabIndex = 1;
            this.lblFechaRecaudoInicial.Text = "Fecha Recaudo Inicial";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(72, 205);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(80, 24);
            this.btnAceptar.TabIndex = 66;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(171, 205);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 24);
            this.btnCancelar.TabIndex = 67;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbxBase
            // 
            this.gbxBase.Controls.Add(this.lblFechaRecaudoFinal);
            this.gbxBase.Controls.Add(this.dtpFechaRecaudoFinal);
            this.gbxBase.Controls.Add(this.lblFechaRecaudoInicial);
            this.gbxBase.Controls.Add(this.dtpFechaInicial);
            this.gbxBase.Location = new System.Drawing.Point(17, 7);
            this.gbxBase.Name = "gbxBase";
            this.gbxBase.Size = new System.Drawing.Size(304, 139);
            this.gbxBase.TabIndex = 65;
            this.gbxBase.TabStop = false;
            // 
            // lblFechaRecaudoFinal
            // 
            this.lblFechaRecaudoFinal.AutoSize = true;
            this.lblFechaRecaudoFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaRecaudoFinal.Location = new System.Drawing.Point(6, 74);
            this.lblFechaRecaudoFinal.Name = "lblFechaRecaudoFinal";
            this.lblFechaRecaudoFinal.Size = new System.Drawing.Size(143, 15);
            this.lblFechaRecaudoFinal.TabIndex = 3;
            this.lblFechaRecaudoFinal.Text = "Fecha Recaudo Final";
            // 
            // dtpFechaRecaudoFinal
            // 
            this.dtpFechaRecaudoFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaRecaudoFinal.Location = new System.Drawing.Point(7, 92);
            this.dtpFechaRecaudoFinal.Name = "dtpFechaRecaudoFinal";
            this.dtpFechaRecaudoFinal.Size = new System.Drawing.Size(292, 20);
            this.dtpFechaRecaudoFinal.TabIndex = 2;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicial.Location = new System.Drawing.Point(6, 40);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(292, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // RutaTextBox_Fecha
            // 
            this.RutaTextBox_Fecha.AllowPromptAsInput = false;
            this.RutaTextBox_Fecha.FocusIn = System.Drawing.Color.LightYellow;
            this.RutaTextBox_Fecha.FocusOut = System.Drawing.Color.White;
            this.RutaTextBox_Fecha.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RutaTextBox_Fecha.Location = new System.Drawing.Point(17, 174);
            this.RutaTextBox_Fecha.MinLength = 0;
            this.RutaTextBox_Fecha.Name = "RutaTextBox_Fecha";
            this.RutaTextBox_Fecha.PromptChar = ' ';
            this.RutaTextBox_Fecha.ResetOnPrompt = false;
            this.RutaTextBox_Fecha.ResetOnSpace = false;
            this.RutaTextBox_Fecha.Size = new System.Drawing.Size(271, 20);
            this.RutaTextBox_Fecha.TabIndex = 68;
            this.RutaTextBox_Fecha.Visible = false;
            this.RutaTextBox_Fecha.Click += new System.EventHandler(this.RutaTextBox_Fecha_Click);
            // 
            // SelectFolderButton_Fecha
            // 
            this.SelectFolderButton_Fecha.Image = global::BcoBogota.Plugin.Properties.Resources.MainFolder;
            this.SelectFolderButton_Fecha.Location = new System.Drawing.Point(294, 172);
            this.SelectFolderButton_Fecha.Name = "SelectFolderButton_Fecha";
            this.SelectFolderButton_Fecha.Size = new System.Drawing.Size(27, 23);
            this.SelectFolderButton_Fecha.TabIndex = 69;
            this.SelectFolderButton_Fecha.UseVisualStyleBackColor = true;
            this.SelectFolderButton_Fecha.Visible = false;
            // 
            // lblRuta_FechaRecaudo
            // 
            this.lblRuta_FechaRecaudo.AutoSize = true;
            this.lblRuta_FechaRecaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuta_FechaRecaudo.Location = new System.Drawing.Point(19, 156);
            this.lblRuta_FechaRecaudo.Name = "lblRuta_FechaRecaudo";
            this.lblRuta_FechaRecaudo.Size = new System.Drawing.Size(79, 15);
            this.lblRuta_FechaRecaudo.TabIndex = 70;
            this.lblRuta_FechaRecaudo.Text = "Elegir Ruta";
            // 
            // frmFechaRecaudo_Doble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 237);
            this.ControlBox = false;
            this.Controls.Add(this.lblRuta_FechaRecaudo);
            this.Controls.Add(this.RutaTextBox_Fecha);
            this.Controls.Add(this.SelectFolderButton_Fecha);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gbxBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFechaRecaudo_Doble";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elegir Fechas de Recaudo";
            this.gbxBase.ResumeLayout(false);
            this.gbxBase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblFechaRecaudoInicial;
        internal System.Windows.Forms.Button btnAceptar;
        internal System.Windows.Forms.Button btnCancelar;
        internal System.Windows.Forms.GroupBox gbxBase;
        internal System.Windows.Forms.Label lblFechaRecaudoFinal;
        internal System.Windows.Forms.DateTimePicker dtpFechaRecaudoFinal;
        internal System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl RutaTextBox_Fecha;
        internal System.Windows.Forms.Button SelectFolderButton_Fecha;
        private System.Windows.Forms.Label lblRuta_FechaRecaudo;

    }
}