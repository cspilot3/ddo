namespace Exportador_Acciones_Valores
{
    partial class FormExportar
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
            this.gbxBase = new System.Windows.Forms.GroupBox();
            this.BuscarCarpetaButton = new System.Windows.Forms.Button();
            this.CarpetaLabel = new System.Windows.Forms.Label();
            this.CarpetaSalidaTextBox = new System.Windows.Forms.TextBox();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.ExportarButton = new System.Windows.Forms.Button();
            this.gbxBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxBase
            // 
            this.gbxBase.Controls.Add(this.BuscarCarpetaButton);
            this.gbxBase.Controls.Add(this.CarpetaLabel);
            this.gbxBase.Controls.Add(this.CarpetaSalidaTextBox);
            this.gbxBase.Location = new System.Drawing.Point(10, 13);
            this.gbxBase.Name = "gbxBase";
            this.gbxBase.Size = new System.Drawing.Size(336, 79);
            this.gbxBase.TabIndex = 20;
            this.gbxBase.TabStop = false;
            this.gbxBase.Text = "Parametros de exportación";
            // 
            // BuscarCarpetaButton
            // 
            this.BuscarCarpetaButton.Image = global::Exportador_Acciones_Valores.Properties.Resources.btnDestape;
            this.BuscarCarpetaButton.Location = new System.Drawing.Point(288, 30);
            this.BuscarCarpetaButton.Name = "BuscarCarpetaButton";
            this.BuscarCarpetaButton.Size = new System.Drawing.Size(37, 30);
            this.BuscarCarpetaButton.TabIndex = 8;
            this.BuscarCarpetaButton.UseVisualStyleBackColor = true;
            this.BuscarCarpetaButton.Click += new System.EventHandler(this.BuscarCarpetaButton_Click);
            // 
            // CarpetaLabel
            // 
            this.CarpetaLabel.AutoSize = true;
            this.CarpetaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CarpetaLabel.Location = new System.Drawing.Point(10, 20);
            this.CarpetaLabel.Name = "CarpetaLabel";
            this.CarpetaLabel.Size = new System.Drawing.Size(122, 15);
            this.CarpetaLabel.TabIndex = 7;
            this.CarpetaLabel.Text = "Carpeta de Salida";
            // 
            // CarpetaSalidaTextBox
            // 
            this.CarpetaSalidaTextBox.Location = new System.Drawing.Point(13, 40);
            this.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox";
            this.CarpetaSalidaTextBox.Size = new System.Drawing.Size(269, 20);
            this.CarpetaSalidaTextBox.TabIndex = 4;
            // 
            // CancelarButton
            // 
            this.CancelarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelarButton.Image = global::Exportador_Acciones_Valores.Properties.Resources.cancelar;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(246, 98);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(89, 34);
            this.CancelarButton.TabIndex = 19;
            this.CancelarButton.Text = "Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // ExportarButton
            // 
            this.ExportarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportarButton.Image = global::Exportador_Acciones_Valores.Properties.Resources.Aceptar;
            this.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportarButton.Location = new System.Drawing.Point(147, 98);
            this.ExportarButton.Name = "ExportarButton";
            this.ExportarButton.Size = new System.Drawing.Size(89, 34);
            this.ExportarButton.TabIndex = 18;
            this.ExportarButton.Text = "Exportar";
            this.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportarButton.UseVisualStyleBackColor = true;
            this.ExportarButton.Click += new System.EventHandler(this.ExportarButton_Click);
            // 
            // FormExportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 144);
            this.Controls.Add(this.gbxBase);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.ExportarButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExportar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar";
            this.gbxBase.ResumeLayout(false);
            this.gbxBase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbxBase;
        internal System.Windows.Forms.Button BuscarCarpetaButton;
        internal System.Windows.Forms.Label CarpetaLabel;
        internal System.Windows.Forms.TextBox CarpetaSalidaTextBox;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button ExportarButton;
    }
}