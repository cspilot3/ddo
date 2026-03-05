namespace Davivienda.Plugin.Imaging.Embargos.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExportar));
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.CarpetaSalidaTextBox = new System.Windows.Forms.TextBox();
            this.BuscarCarpetaButton = new System.Windows.Forms.Button();
            this.FechaProcesoFinalLabel = new System.Windows.Forms.Label();
            this.FechaProcesoFinalDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FechaProcesoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FechaProcesoLabel = new System.Windows.Forms.Label();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.ExportarButton = new System.Windows.Forms.Button();
            this.OTLabel = new System.Windows.Forms.Label();
            this.OTDesktopComboBox = new Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl();
            this.MainGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainGroupBox.Controls.Add(this.OTDesktopComboBox);
            this.MainGroupBox.Controls.Add(this.OTLabel);
            this.MainGroupBox.Controls.Add(this.panel1);
            this.MainGroupBox.Controls.Add(this.FechaProcesoFinalLabel);
            this.MainGroupBox.Controls.Add(this.FechaProcesoFinalDateTimePicker);
            this.MainGroupBox.Controls.Add(this.FechaProcesoDateTimePicker);
            this.MainGroupBox.Controls.Add(this.FechaProcesoLabel);
            this.MainGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainGroupBox.Location = new System.Drawing.Point(21, 12);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(603, 194);
            this.MainGroupBox.TabIndex = 1;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Parametros de exportación";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCarpeta);
            this.panel1.Controls.Add(this.CarpetaSalidaTextBox);
            this.panel1.Controls.Add(this.BuscarCarpetaButton);
            this.panel1.Location = new System.Drawing.Point(15, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 65);
            this.panel1.TabIndex = 18;
            // 
            // lblCarpeta
            // 
            this.lblCarpeta.AutoSize = true;
            this.lblCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarpeta.Location = new System.Drawing.Point(3, 9);
            this.lblCarpeta.Name = "lblCarpeta";
            this.lblCarpeta.Size = new System.Drawing.Size(122, 15);
            this.lblCarpeta.TabIndex = 5;
            this.lblCarpeta.Text = "Carpeta de Salida";
            // 
            // CarpetaSalidaTextBox
            // 
            this.CarpetaSalidaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CarpetaSalidaTextBox.Location = new System.Drawing.Point(7, 28);
            this.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox";
            this.CarpetaSalidaTextBox.Size = new System.Drawing.Size(477, 20);
            this.CarpetaSalidaTextBox.TabIndex = 6;
            // 
            // BuscarCarpetaButton
            // 
            this.BuscarCarpetaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BuscarCarpetaButton.Image = ((System.Drawing.Image)(resources.GetObject("BuscarCarpetaButton.Image")));
            this.BuscarCarpetaButton.Location = new System.Drawing.Point(503, 22);
            this.BuscarCarpetaButton.Name = "BuscarCarpetaButton";
            this.BuscarCarpetaButton.Size = new System.Drawing.Size(43, 30);
            this.BuscarCarpetaButton.TabIndex = 7;
            this.BuscarCarpetaButton.UseVisualStyleBackColor = true;
            this.BuscarCarpetaButton.Click += new System.EventHandler(this.BuscarCarpetaButton_Click);
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
            this.FechaProcesoFinalDateTimePicker.Location = new System.Drawing.Point(303, 37);
            this.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker";
            this.FechaProcesoFinalDateTimePicker.Size = new System.Drawing.Size(279, 22);
            this.FechaProcesoFinalDateTimePicker.TabIndex = 11;
            this.FechaProcesoFinalDateTimePicker.ValueChanged += new System.EventHandler(this.FechaProcesoFinalDateTimePicker_ValueChanged);
            // 
            // FechaProcesoDateTimePicker
            // 
            this.FechaProcesoDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoDateTimePicker.Location = new System.Drawing.Point(15, 38);
            this.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker";
            this.FechaProcesoDateTimePicker.Size = new System.Drawing.Size(279, 22);
            this.FechaProcesoDateTimePicker.TabIndex = 1;
            this.FechaProcesoDateTimePicker.ValueChanged += new System.EventHandler(this.FechaProcesoDateTimePicker_ValueChanged);
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
            this.CancelarButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelarButton.Image")));
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(500, 212);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(119, 37);
            this.CancelarButton.TabIndex = 4;
            this.CancelarButton.Text = "Cancelar/Cerrar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // ExportarButton
            // 
            this.ExportarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportarButton.Image = ((System.Drawing.Image)(resources.GetObject("ExportarButton.Image")));
            this.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportarButton.Location = new System.Drawing.Point(394, 212);
            this.ExportarButton.Name = "ExportarButton";
            this.ExportarButton.Size = new System.Drawing.Size(89, 37);
            this.ExportarButton.TabIndex = 3;
            this.ExportarButton.Text = "Exportar";
            this.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportarButton.UseVisualStyleBackColor = true;
            this.ExportarButton.Click += new System.EventHandler(this.ExportarButton_Click);
            // 
            // OTLabel
            // 
            this.OTLabel.AutoSize = true;
            this.OTLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OTLabel.Location = new System.Drawing.Point(18, 82);
            this.OTLabel.Name = "OTLabel";
            this.OTLabel.Size = new System.Drawing.Size(28, 13);
            this.OTLabel.TabIndex = 68;
            this.OTLabel.Text = "OT:";
            // 
            // OTDesktopComboBox
            // 
            this.OTDesktopComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.OTDesktopComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.OTDesktopComboBox.DisabledEnter = false;
            this.OTDesktopComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OTDesktopComboBox.fk_Campo = 0;
            this.OTDesktopComboBox.fk_Documento = 0;
            this.OTDesktopComboBox.fk_Validacion = 0;
            this.OTDesktopComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OTDesktopComboBox.FormattingEnabled = true;
            this.OTDesktopComboBox.Location = new System.Drawing.Point(67, 79);
            this.OTDesktopComboBox.Name = "OTDesktopComboBox";
            this.OTDesktopComboBox.Size = new System.Drawing.Size(121, 21);
            this.OTDesktopComboBox.TabIndex = 69;
            // 
            // FormExportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 261);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.ExportarButton);
            this.Controls.Add(this.MainGroupBox);
            this.Name = "FormExportar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar";
            this.Load += new System.EventHandler(this.FormExportar_Load);
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox MainGroupBox;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblCarpeta;
        internal System.Windows.Forms.TextBox CarpetaSalidaTextBox;
        internal System.Windows.Forms.Button BuscarCarpetaButton;
        internal System.Windows.Forms.Label FechaProcesoFinalLabel;
        internal System.Windows.Forms.DateTimePicker FechaProcesoFinalDateTimePicker;
        internal System.Windows.Forms.DateTimePicker FechaProcesoDateTimePicker;
        internal System.Windows.Forms.Label FechaProcesoLabel;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button ExportarButton;
        internal Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl OTDesktopComboBox;
        internal System.Windows.Forms.Label OTLabel;

    }
}