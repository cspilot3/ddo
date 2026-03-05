namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms
{
    partial class FormExportWeb
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
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.FechaProcesoFinalLabel = new System.Windows.Forms.Label();
            this.FechaProcesoFinalDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FechaProcesoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FechaProcesoLabel = new System.Windows.Forms.Label();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.ExportarButton = new System.Windows.Forms.Button();
            this.MainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainGroupBox.Controls.Add(this.FechaProcesoFinalLabel);
            this.MainGroupBox.Controls.Add(this.FechaProcesoFinalDateTimePicker);
            this.MainGroupBox.Controls.Add(this.FechaProcesoDateTimePicker);
            this.MainGroupBox.Controls.Add(this.FechaProcesoLabel);
            this.MainGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainGroupBox.Location = new System.Drawing.Point(12, 12);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(601, 79);
            this.MainGroupBox.TabIndex = 1;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Parametros de exportación";
            // 
            // FechaProcesoFinalLabel
            // 
            this.FechaProcesoFinalLabel.AutoSize = true;
            this.FechaProcesoFinalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoFinalLabel.Location = new System.Drawing.Point(300, 19);
            this.FechaProcesoFinalLabel.Name = "FechaProcesoFinalLabel";
            this.FechaProcesoFinalLabel.Size = new System.Drawing.Size(138, 15);
            this.FechaProcesoFinalLabel.TabIndex = 12;
            this.FechaProcesoFinalLabel.Text = "Fecha Proceso Final";
            // 
            // FechaProcesoFinalDateTimePicker
            // 
            this.FechaProcesoFinalDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoFinalDateTimePicker.Location = new System.Drawing.Point(303, 38);
            this.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker";
            this.FechaProcesoFinalDateTimePicker.Size = new System.Drawing.Size(279, 22);
            this.FechaProcesoFinalDateTimePicker.TabIndex = 11;
            // 
            // FechaProcesoDateTimePicker
            // 
            this.FechaProcesoDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoDateTimePicker.Location = new System.Drawing.Point(15, 38);
            this.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker";
            this.FechaProcesoDateTimePicker.Size = new System.Drawing.Size(279, 22);
            this.FechaProcesoDateTimePicker.TabIndex = 1;
            // 
            // FechaProcesoLabel
            // 
            this.FechaProcesoLabel.AutoSize = true;
            this.FechaProcesoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoLabel.Location = new System.Drawing.Point(12, 19);
            this.FechaProcesoLabel.Name = "FechaProcesoLabel";
            this.FechaProcesoLabel.Size = new System.Drawing.Size(145, 15);
            this.FechaProcesoLabel.TabIndex = 0;
            this.FechaProcesoLabel.Text = "Fecha Proceso Inicial";
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelarButton.Image = global::BcoPopular.Plugin.Properties.Resources.btnSalir;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(315, 111);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(104, 37);
            this.CancelarButton.TabIndex = 4;
            this.CancelarButton.Text = "Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            // 
            // ExportarButton
            // 
            this.ExportarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportarButton.Image = global::BcoPopular.Plugin.Properties.Resources.Aceptar;
            this.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportarButton.Location = new System.Drawing.Point(200, 111);
            this.ExportarButton.Name = "ExportarButton";
            this.ExportarButton.Size = new System.Drawing.Size(104, 37);
            this.ExportarButton.TabIndex = 3;
            this.ExportarButton.Text = "Exportar";
            this.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportarButton.UseVisualStyleBackColor = true;
            this.ExportarButton.Click += new System.EventHandler(this.ExportarButton_Click);
            // 
            // FormExportWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 160);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.ExportarButton);
            this.Controls.Add(this.MainGroupBox);
            this.Name = "FormExportWeb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormExportWeb";
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox MainGroupBox;
        internal System.Windows.Forms.Label FechaProcesoFinalLabel;
        internal System.Windows.Forms.DateTimePicker FechaProcesoFinalDateTimePicker;
        internal System.Windows.Forms.DateTimePicker FechaProcesoDateTimePicker;
        internal System.Windows.Forms.Label FechaProcesoLabel;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button ExportarButton;
    }
}