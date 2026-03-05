namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms
{
    partial class frmFechaRecaudoCiudad
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
            this.lblFechaRecaudo = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbxBase = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCiudad = new System.Windows.Forms.ComboBox();
            this.lblFechaRecaudo2 = new System.Windows.Forms.Label();
            this.dtpFechaInicial2 = new System.Windows.Forms.DateTimePicker();
            this.RutaTextBox = new Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.lblRuta = new System.Windows.Forms.Label();
            this.gbxBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFechaRecaudo
            // 
            this.lblFechaRecaudo.AutoSize = true;
            this.lblFechaRecaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaRecaudo.Location = new System.Drawing.Point(5, 11);
            this.lblFechaRecaudo.Name = "lblFechaRecaudo";
            this.lblFechaRecaudo.Size = new System.Drawing.Size(107, 15);
            this.lblFechaRecaudo.TabIndex = 6;
            this.lblFechaRecaudo.Text = "Fecha Recaudo";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(59, 201);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(80, 24);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicial.Location = new System.Drawing.Point(6, 29);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(292, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(158, 201);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 24);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // gbxBase
            // 
            this.gbxBase.Controls.Add(this.label1);
            this.gbxBase.Controls.Add(this.cbCiudad);
            this.gbxBase.Controls.Add(this.lblFechaRecaudo2);
            this.gbxBase.Controls.Add(this.dtpFechaInicial2);
            this.gbxBase.Controls.Add(this.lblFechaRecaudo);
            this.gbxBase.Controls.Add(this.dtpFechaInicial);
            this.gbxBase.Location = new System.Drawing.Point(7, 6);
            this.gbxBase.Name = "gbxBase";
            this.gbxBase.Size = new System.Drawing.Size(304, 139);
            this.gbxBase.TabIndex = 7;
            this.gbxBase.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ciudad";
            // 
            // cbCiudad
            // 
            this.cbCiudad.FormattingEnabled = true;
            this.cbCiudad.Location = new System.Drawing.Point(6, 112);
            this.cbCiudad.Name = "cbCiudad";
            this.cbCiudad.Size = new System.Drawing.Size(292, 21);
            this.cbCiudad.TabIndex = 9;
            // 
            // lblFechaRecaudo2
            // 
            this.lblFechaRecaudo2.AutoSize = true;
            this.lblFechaRecaudo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaRecaudo2.Location = new System.Drawing.Point(6, 53);
            this.lblFechaRecaudo2.Name = "lblFechaRecaudo2";
            this.lblFechaRecaudo2.Size = new System.Drawing.Size(107, 15);
            this.lblFechaRecaudo2.TabIndex = 8;
            this.lblFechaRecaudo2.Text = "Fecha Recaudo";
            // 
            // dtpFechaInicial2
            // 
            this.dtpFechaInicial2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicial2.Location = new System.Drawing.Point(7, 71);
            this.dtpFechaInicial2.Name = "dtpFechaInicial2";
            this.dtpFechaInicial2.Size = new System.Drawing.Size(292, 20);
            this.dtpFechaInicial2.TabIndex = 7;
            // 
            // RutaTextBox
            // 
            this.RutaTextBox.AllowPromptAsInput = false;
            this.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.RutaTextBox.FocusOut = System.Drawing.Color.White;
            this.RutaTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RutaTextBox.Location = new System.Drawing.Point(4, 166);
            this.RutaTextBox.MinLength = 0;
            this.RutaTextBox.Name = "RutaTextBox";
            this.RutaTextBox.PromptChar = ' ';
            this.RutaTextBox.ResetOnPrompt = false;
            this.RutaTextBox.ResetOnSpace = false;
            this.RutaTextBox.Size = new System.Drawing.Size(271, 20);
            this.RutaTextBox.TabIndex = 1;
            this.RutaTextBox.Click += new System.EventHandler(this.RutaTextBox_Click);
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(281, 164);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(27, 23);
            this.SelectFolderButton.TabIndex = 2;
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click_1);
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuta.Location = new System.Drawing.Point(4, 148);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(79, 15);
            this.lblRuta.TabIndex = 5;
            this.lblRuta.Text = "Elegir Ruta";
            // 
            // frmFechaRecaudoCiudad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 236);
            this.Controls.Add(this.lblRuta);
            this.Controls.Add(this.RutaTextBox);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gbxBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFechaRecaudoCiudad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elegir Fecha Recaudo";
            this.Load += new System.EventHandler(this.frmFechaRecaudoCiudad_Load);
            this.gbxBase.ResumeLayout(false);
            this.gbxBase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblFechaRecaudo;
        internal System.Windows.Forms.Button btnAceptar;
        internal System.Windows.Forms.DateTimePicker dtpFechaInicial;
        internal System.Windows.Forms.Button btnCancelar;
        internal System.Windows.Forms.GroupBox gbxBase;
        private Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl RutaTextBox;
        internal System.Windows.Forms.Button SelectFolderButton;
        internal System.Windows.Forms.Label lblRuta;
        internal System.Windows.Forms.Label lblFechaRecaudo2;
        internal System.Windows.Forms.DateTimePicker dtpFechaInicial2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cbCiudad;
    }
}