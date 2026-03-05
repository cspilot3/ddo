namespace Banagrario.Scanning.Forms
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OfficeLabel = new System.Windows.Forms.Label();
            this.OfficeComboBox = new System.Windows.Forms.ComboBox();
            this.TempPathLabel = new System.Windows.Forms.Label();
            this.TempPathTextBox = new System.Windows.Forms.TextBox();
            this.CargarPathButton = new System.Windows.Forms.Button();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CargarPathButton);
            this.groupBox1.Controls.Add(this.OfficeLabel);
            this.groupBox1.Controls.Add(this.OfficeComboBox);
            this.groupBox1.Controls.Add(this.TempPathLabel);
            this.groupBox1.Controls.Add(this.TempPathTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // OfficeLabel
            // 
            this.OfficeLabel.AutoSize = true;
            this.OfficeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OfficeLabel.Location = new System.Drawing.Point(6, 67);
            this.OfficeLabel.Name = "OfficeLabel";
            this.OfficeLabel.Size = new System.Drawing.Size(47, 13);
            this.OfficeLabel.TabIndex = 3;
            this.OfficeLabel.Text = "Oficina";
            // 
            // OfficeComboBox
            // 
            this.OfficeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OfficeComboBox.FormattingEnabled = true;
            this.OfficeComboBox.Location = new System.Drawing.Point(6, 83);
            this.OfficeComboBox.Name = "OfficeComboBox";
            this.OfficeComboBox.Size = new System.Drawing.Size(399, 21);
            this.OfficeComboBox.TabIndex = 2;
            // 
            // TempPathLabel
            // 
            this.TempPathLabel.AutoSize = true;
            this.TempPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TempPathLabel.Location = new System.Drawing.Point(6, 16);
            this.TempPathLabel.Name = "TempPathLabel";
            this.TempPathLabel.Size = new System.Drawing.Size(112, 13);
            this.TempPathLabel.TabIndex = 1;
            this.TempPathLabel.Text = "Carpeta de trabajo";
            // 
            // TempPathTextBox
            // 
            this.TempPathTextBox.Location = new System.Drawing.Point(9, 32);
            this.TempPathTextBox.Name = "TempPathTextBox";
            this.TempPathTextBox.Size = new System.Drawing.Size(362, 20);
            this.TempPathTextBox.TabIndex = 0;
            // 
            // CargarPathButton
            // 
            this.CargarPathButton.Image = global::Banagrario.Scanning.Properties.Resources.Directorios;
            this.CargarPathButton.Location = new System.Drawing.Point(377, 28);
            this.CargarPathButton.Name = "CargarPathButton";
            this.CargarPathButton.Size = new System.Drawing.Size(28, 24);
            this.CargarPathButton.TabIndex = 10;
            this.CargarPathButton.Click += new System.EventHandler(this.CargarPathButton_Click);
            // 
            // AceptarButton
            // 
            this.AceptarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AceptarButton.Image = global::Banagrario.Scanning.Properties.Resources.tick;
            this.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AceptarButton.Location = new System.Drawing.Point(206, 132);
            this.AceptarButton.Name = "AceptarButton";
            this.AceptarButton.Size = new System.Drawing.Size(85, 24);
            this.AceptarButton.TabIndex = 1;
            this.AceptarButton.Text = "&Aceptar";
            this.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
            // 
            // CancelarButton
            // 
            this.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelarButton.Image = global::Banagrario.Scanning.Properties.Resources.cancel;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(325, 132);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(85, 24);
            this.CancelarButton.TabIndex = 2;
            this.CancelarButton.Text = "&Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // FormConfig
            // 
            this.AcceptButton = this.AceptarButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelarButton;
            this.ClientSize = new System.Drawing.Size(429, 166);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.AceptarButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label OfficeLabel;
        private System.Windows.Forms.ComboBox OfficeComboBox;
        private System.Windows.Forms.Label TempPathLabel;
        private System.Windows.Forms.TextBox TempPathTextBox;
        internal System.Windows.Forms.Button AceptarButton;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button CargarPathButton;
    }
}