namespace BcoColpatria.Plugin.Imaging.Embargos.Form
{
    partial class FormGenerarCartas
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.OTDesktopComboBox = new Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl();
            this.OTLabel = new System.Windows.Forms.Label();
            this.FechaProcesoLabel = new System.Windows.Forms.Label();
            this.FechaProcesoPicker = new System.Windows.Forms.DateTimePicker();
            this.ReportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.GenerarButton = new System.Windows.Forms.Button();
            this.CTA_ImagenesDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CTA_Formato_ParametrosDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TBL_FormatoDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CTA_ImagenesDataTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CTA_Formato_ParametrosDataTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBL_FormatoDataTableBindingSource)).BeginInit();
            this.SuspendLayout();
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
            this.OTDesktopComboBox.Location = new System.Drawing.Point(153, 44);
            this.OTDesktopComboBox.Name = "OTDesktopComboBox";
            this.OTDesktopComboBox.Size = new System.Drawing.Size(121, 21);
            this.OTDesktopComboBox.TabIndex = 67;
            // 
            // OTLabel
            // 
            this.OTLabel.AutoSize = true;
            this.OTLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OTLabel.Location = new System.Drawing.Point(22, 47);
            this.OTLabel.Name = "OTLabel";
            this.OTLabel.Size = new System.Drawing.Size(28, 13);
            this.OTLabel.TabIndex = 66;
            this.OTLabel.Text = "OT:";
            // 
            // FechaProcesoLabel
            // 
            this.FechaProcesoLabel.AutoSize = true;
            this.FechaProcesoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoLabel.Location = new System.Drawing.Point(20, 18);
            this.FechaProcesoLabel.Name = "FechaProcesoLabel";
            this.FechaProcesoLabel.Size = new System.Drawing.Size(114, 13);
            this.FechaProcesoLabel.TabIndex = 65;
            this.FechaProcesoLabel.Text = "Fecha de Proceso:";
            // 
            // FechaProcesoPicker
            // 
            this.FechaProcesoPicker.Location = new System.Drawing.Point(153, 12);
            this.FechaProcesoPicker.Name = "FechaProcesoPicker";
            this.FechaProcesoPicker.Size = new System.Drawing.Size(252, 21);
            this.FechaProcesoPicker.TabIndex = 64;
            this.FechaProcesoPicker.ValueChanged += new System.EventHandler(this.FechaProcesoPicker_ValueChanged);
            // 
            // ReportViewer1
            // 
            this.ReportViewer1.DocumentMapWidth = 84;
            reportDataSource1.Name = "CTA_ImagenesDataSet";
            reportDataSource1.Value = null;
            reportDataSource2.Name = "CTA_Formato_Parametros_AsuntoDataSet";
            reportDataSource2.Value = null;
            reportDataSource3.Name = "CTA_Formato_Parametros_FirmaDataSet";
            reportDataSource3.Value = null;
            reportDataSource4.Name = "TBL_FormatoDataSet";
            reportDataSource4.Value = null;
            this.ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.ReportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.ReportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.ReportViewer1.LocalReport.ReportEmbeddedResource = "BcoCoopcentral.Plugin.Report_GenerarCartas.rdlc";
            this.ReportViewer1.Location = new System.Drawing.Point(20, 78);
            this.ReportViewer1.Name = "ReportViewer1";
            this.ReportViewer1.Size = new System.Drawing.Size(101, 39);
            this.ReportViewer1.TabIndex = 63;
            this.ReportViewer1.Visible = false;
            // 
            // GenerarButton
            // 
            this.GenerarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerarButton.Location = new System.Drawing.Point(328, 94);
            this.GenerarButton.Name = "GenerarButton";
            this.GenerarButton.Size = new System.Drawing.Size(75, 23);
            this.GenerarButton.TabIndex = 62;
            this.GenerarButton.Text = "Generar";
            this.GenerarButton.UseVisualStyleBackColor = true;
            this.GenerarButton.Click += new System.EventHandler(this.GenerarButton_Click);
            // 
            // CTA_ImagenesDataTableBindingSource
            // 
            this.CTA_ImagenesDataTableBindingSource.DataSource = typeof(DBIntegration.SchemaConfig.CTA_ImagenesDataTable);
            // 
            // CTA_Formato_ParametrosDataTableBindingSource
            // 
            this.CTA_Formato_ParametrosDataTableBindingSource.DataSource = typeof(DBIntegration.SchemaProcess.CTA_Formato_ParametrosDataTable);
            // 
            // TBL_FormatoDataTableBindingSource
            // 
            this.TBL_FormatoDataTableBindingSource.DataSource = typeof(DBIntegration.SchemaConfig.TBL_FormatoDataTable);
            // 
            // FormGenerarCartas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 134);
            this.Controls.Add(this.OTDesktopComboBox);
            this.Controls.Add(this.OTLabel);
            this.Controls.Add(this.FechaProcesoLabel);
            this.Controls.Add(this.FechaProcesoPicker);
            this.Controls.Add(this.ReportViewer1);
            this.Controls.Add(this.GenerarButton);
            this.Name = "FormGenerarCartas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Cartas Respuesta";
            this.Load += new System.EventHandler(this.FormGenerarCartas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CTA_ImagenesDataTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CTA_Formato_ParametrosDataTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBL_FormatoDataTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl OTDesktopComboBox;
        internal System.Windows.Forms.Label OTLabel;
        internal System.Windows.Forms.Label FechaProcesoLabel;
        internal System.Windows.Forms.DateTimePicker FechaProcesoPicker;
        internal Microsoft.Reporting.WinForms.ReportViewer ReportViewer1;
        internal System.Windows.Forms.Button GenerarButton;
        internal System.Windows.Forms.BindingSource CTA_ImagenesDataTableBindingSource;
        internal System.Windows.Forms.BindingSource CTA_Formato_ParametrosDataTableBindingSource;
        internal System.Windows.Forms.BindingSource TBL_FormatoDataTableBindingSource;
    }
}