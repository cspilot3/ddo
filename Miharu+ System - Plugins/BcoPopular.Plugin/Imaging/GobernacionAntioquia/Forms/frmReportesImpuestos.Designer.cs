namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms
{
    partial class frmReportesImpuestos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportesImpuestos));
            this.menuStripReportes = new System.Windows.Forms.MenuStrip();
            this.ReportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsolidadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SistematizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TarjetasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CuadreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Dispercion100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionDeptoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionMunicipioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionDispercionSistematizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faltantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobrantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InformeConsolidadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InformeCiudadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SistematizacionCorregidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispersionAutoMunicipiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispersionAutoDepartamentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispersionAutoSistematizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DetalleRechazosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportViewerImpuestos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.imageListImpuestos = new System.Windows.Forms.ImageList(this.components);
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblGenerando = new System.Windows.Forms.Label();
            this.pictureBoxCargando = new System.Windows.Forms.PictureBox();
            this.backgroundWorkerReport = new System.ComponentModel.BackgroundWorker();
            this.cuadreConsolidadoTarjetastoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripReportes.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCargando)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripReportes
            // 
            this.menuStripReportes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReportesToolStripMenuItem,
            this.otrosToolStripMenuItem});
            this.menuStripReportes.Location = new System.Drawing.Point(0, 0);
            this.menuStripReportes.Name = "menuStripReportes";
            this.menuStripReportes.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStripReportes.Size = new System.Drawing.Size(1457, 28);
            this.menuStripReportes.TabIndex = 0;
            this.menuStripReportes.Text = "menuStrip1";
            // 
            // ReportesToolStripMenuItem
            // 
            this.ReportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConsolidadoToolStripMenuItem,
            this.PagosToolStripMenuItem,
            this.SistematizacionToolStripMenuItem,
            this.TarjetasToolStripMenuItem,
            this.CuadreToolStripMenuItem,
            this.cuadreConsolidadoTarjetastoolStripMenuItem,
            this.Dispercion100ToolStripMenuItem,
            this.DispercionDeptoToolStripMenuItem,
            this.DispercionMunicipioToolStripMenuItem,
            this.DispercionDispercionSistematizacionToolStripMenuItem});
            this.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem";
            this.ReportesToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.ReportesToolStripMenuItem.Text = "Reportes";
            // 
            // ConsolidadoToolStripMenuItem
            // 
            this.ConsolidadoToolStripMenuItem.Name = "ConsolidadoToolStripMenuItem";
            this.ConsolidadoToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.ConsolidadoToolStripMenuItem.Text = "CONSOLIDADO";
            this.ConsolidadoToolStripMenuItem.Click += new System.EventHandler(this.ConsolidadoToolStripMenuItem_Click);
            // 
            // PagosToolStripMenuItem
            // 
            this.PagosToolStripMenuItem.Name = "PagosToolStripMenuItem";
            this.PagosToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.PagosToolStripMenuItem.Text = "PAGOS IDEA";
            this.PagosToolStripMenuItem.Click += new System.EventHandler(this.PagosToolStripMenuItem_Click);
            // 
            // SistematizacionToolStripMenuItem
            // 
            this.SistematizacionToolStripMenuItem.Name = "SistematizacionToolStripMenuItem";
            this.SistematizacionToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.SistematizacionToolStripMenuItem.Text = "SISTEMATIZACION";
            this.SistematizacionToolStripMenuItem.Click += new System.EventHandler(this.SistematizacionToolStripMenuItem_Click);
            // 
            // TarjetasToolStripMenuItem
            // 
            this.TarjetasToolStripMenuItem.Name = "TarjetasToolStripMenuItem";
            this.TarjetasToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.TarjetasToolStripMenuItem.Text = "REPORTE TARJETAS";
            this.TarjetasToolStripMenuItem.Click += new System.EventHandler(this.TarjetasToolStripMenuItem_Click);
            // 
            // CuadreToolStripMenuItem
            // 
            this.CuadreToolStripMenuItem.Name = "CuadreToolStripMenuItem";
            this.CuadreToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.CuadreToolStripMenuItem.Text = "CUADRE TARJETAS";
            this.CuadreToolStripMenuItem.Click += new System.EventHandler(this.CuadreToolStripMenuItem_Click);
            // 
            // Dispercion100ToolStripMenuItem
            // 
            this.Dispercion100ToolStripMenuItem.Name = "Dispercion100ToolStripMenuItem";
            this.Dispercion100ToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.Dispercion100ToolStripMenuItem.Text = "DISPERSION 100%";
            this.Dispercion100ToolStripMenuItem.Click += new System.EventHandler(this.Dispercion100ToolStripMenuItem_Click);
            // 
            // DispercionDeptoToolStripMenuItem
            // 
            this.DispercionDeptoToolStripMenuItem.Name = "DispercionDeptoToolStripMenuItem";
            this.DispercionDeptoToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.DispercionDeptoToolStripMenuItem.Text = "DISPERSION DEPARTAMENTO";
            this.DispercionDeptoToolStripMenuItem.Click += new System.EventHandler(this.DispercionDeptoToolStripMenuItem_Click);
            // 
            // DispercionMunicipioToolStripMenuItem
            // 
            this.DispercionMunicipioToolStripMenuItem.Name = "DispercionMunicipioToolStripMenuItem";
            this.DispercionMunicipioToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.DispercionMunicipioToolStripMenuItem.Text = "DISPERSION MUNICIPIOS";
            this.DispercionMunicipioToolStripMenuItem.Click += new System.EventHandler(this.DispercionMunicipioToolStripMenuItem_Click);
            // 
            // DispercionDispercionSistematizacionToolStripMenuItem
            // 
            this.DispercionDispercionSistematizacionToolStripMenuItem.Name = "DispercionDispercionSistematizacionToolStripMenuItem";
            this.DispercionDispercionSistematizacionToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.DispercionDispercionSistematizacionToolStripMenuItem.Text = "DISPERSION SISTEMATIZACION";
            this.DispercionDispercionSistematizacionToolStripMenuItem.Click += new System.EventHandler(this.DispercionDispercionSistematizacionToolStripMenuItem_Click);
            // 
            // DetalleRechazosToolStripMenuItem
            // 
            this.DetalleRechazosToolStripMenuItem.Name = "DetalleRechazosToolStripMenuItem";
            this.DetalleRechazosToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DetalleRechazosToolStripMenuItem.Text = "DETALLADO RECHAZOS";
            this.DetalleRechazosToolStripMenuItem.Click += new System.EventHandler(this.DetalleRechazosToolStripMenuItem_Click);
            // 
            // otrosToolStripMenuItem
            // 
            this.otrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.faltantesToolStripMenuItem,
            this.sobrantesToolStripMenuItem,
            this.InformeConsolidadoToolStripMenuItem,
            this.InformeCiudadToolStripMenuItem,
            this.SistematizacionCorregidaToolStripMenuItem,
            this.DispersionAutoMunicipiosToolStripMenuItem,
            this.DispersionAutoDepartamentosToolStripMenuItem,
            this.DetalleRechazosToolStripMenuItem,
            this.DispersionAutoSistematizacionToolStripMenuItem});
            this.otrosToolStripMenuItem.Name = "otrosToolStripMenuItem";
            this.otrosToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.otrosToolStripMenuItem.Text = "Otros";
            // 
            // faltantesToolStripMenuItem
            // 
            this.faltantesToolStripMenuItem.Name = "faltantesToolStripMenuItem";
            this.faltantesToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.faltantesToolStripMenuItem.Text = "FALTANTES";
            this.faltantesToolStripMenuItem.Click += new System.EventHandler(this.faltantesToolStripMenuItem_Click);
            // 
            // sobrantesToolStripMenuItem
            // 
            this.sobrantesToolStripMenuItem.Name = "sobrantesToolStripMenuItem";
            this.sobrantesToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.sobrantesToolStripMenuItem.Text = "SOBRANTES";
            this.sobrantesToolStripMenuItem.Click += new System.EventHandler(this.sobrantesToolStripMenuItem_Click);
            // 
            // InformeConsolidadoToolStripMenuItem
            // 
            this.InformeConsolidadoToolStripMenuItem.Name = "InformeConsolidadoToolStripMenuItem";
            this.InformeConsolidadoToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.InformeConsolidadoToolStripMenuItem.Text = "INFORME MENSUAL CONSOLIDADO";
            this.InformeConsolidadoToolStripMenuItem.Click += new System.EventHandler(this.InformeConsolidadoToolStripMenuItem_Click);
            // 
            // InformeCiudadToolStripMenuItem
            // 
            this.InformeCiudadToolStripMenuItem.Name = "InformeCiudadToolStripMenuItem";
            this.InformeCiudadToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.InformeCiudadToolStripMenuItem.Text = "INFORME MENSUAL X CIUDAD";
            this.InformeCiudadToolStripMenuItem.Click += new System.EventHandler(this.InformeCiudadToolStripMenuItem_Click);
            // 
            // SistematizacionCorregidaToolStripMenuItem
            // 
            this.SistematizacionCorregidaToolStripMenuItem.Name = "SistematizacionCorregidaToolStripMenuItem";
            this.SistematizacionCorregidaToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.SistematizacionCorregidaToolStripMenuItem.Text = "SISTEMATIZACIÓN CORREGIDA";
            this.SistematizacionCorregidaToolStripMenuItem.Click += new System.EventHandler(this.SistematizacionCorregidaToolStripMenuItem_Click);
            // 
            // DispersionAutoMunicipiosToolStripMenuItem
            // 
            this.DispersionAutoMunicipiosToolStripMenuItem.Name = "DispersionAutoMunicipiosToolStripMenuItem";
            this.DispersionAutoMunicipiosToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.DispersionAutoMunicipiosToolStripMenuItem.Text = "DISPERSION AUTOMATICA MUNICIPIOS";
            this.DispersionAutoMunicipiosToolStripMenuItem.Click += new System.EventHandler(this.DispersionAutoMunicipiosToolStripMenuItem_Click);
            // 
            // DispersionAutoDepartamentosToolStripMenuItem
            // 
            this.DispersionAutoDepartamentosToolStripMenuItem.Name = "DispersionAutoDepartamentosToolStripMenuItem";
            this.DispersionAutoDepartamentosToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.DispersionAutoDepartamentosToolStripMenuItem.Text = "DISPERSION AUTOMATICA DEPARTAMENTOS";
            this.DispersionAutoDepartamentosToolStripMenuItem.Click += new System.EventHandler(this.DispersionAutoDepartamentosToolStripMenuItem_Click);
            // 
            // DispersionAutoSistematizacionToolStripMenuItem
            // 
            this.DispersionAutoSistematizacionToolStripMenuItem.Name = "DispersionAutoSistematizacionToolStripMenuItem";
            this.DispersionAutoSistematizacionToolStripMenuItem.Size = new System.Drawing.Size(383, 24);
            this.DispersionAutoSistematizacionToolStripMenuItem.Text = "DISPERSION AUTOMATICA SISTEMATIZACIÓN";
            this.DispersionAutoSistematizacionToolStripMenuItem.Click += new System.EventHandler(this.DispersionAutoSistematizacionToolStripMenuItem_Click);
            // 
            // DispercionToolStripMenuItem
            // 
            this.DispercionToolStripMenuItem.Name = "DispercionToolStripMenuItem";
            this.DispercionToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.DispercionToolStripMenuItem.Text = "DISPERCION QUINCENAL";
            this.DispercionToolStripMenuItem.Click += new System.EventHandler(this.DispercionToolStripMenuItem_Click);
            // 
            // reportViewerImpuestos
            // 
            this.reportViewerImpuestos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerImpuestos.Location = new System.Drawing.Point(0, 28);
            this.reportViewerImpuestos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reportViewerImpuestos.Name = "reportViewerImpuestos";
            this.reportViewerImpuestos.Size = new System.Drawing.Size(1457, 710);
            this.reportViewerImpuestos.TabIndex = 1;
            this.reportViewerImpuestos.Visible = false;
            // 
            // imageListImpuestos
            // 
            this.imageListImpuestos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListImpuestos.ImageStream")));
            this.imageListImpuestos.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListImpuestos.Images.SetKeyName(0, "MainFolder.png");
            this.imageListImpuestos.Images.SetKeyName(1, "ajax-loader.gif");
            this.imageListImpuestos.Images.SetKeyName(2, "btnBuscar.png");
            this.imageListImpuestos.Images.SetKeyName(3, "BtnCargar.png");
            this.imageListImpuestos.Images.SetKeyName(4, "btnCopiar.png");
            this.imageListImpuestos.Images.SetKeyName(5, "btnGuardar.png");
            this.imageListImpuestos.Images.SetKeyName(6, "btnSalir.png");
            this.imageListImpuestos.Images.SetKeyName(7, "cross.png");
            this.imageListImpuestos.Images.SetKeyName(8, "MainFolder.png");
            this.imageListImpuestos.Images.SetKeyName(9, "Process-Accept.png");
            // 
            // pnlBackground
            // 
            this.pnlBackground.Controls.Add(this.lblGenerando);
            this.pnlBackground.Controls.Add(this.pictureBoxCargando);
            this.pnlBackground.Location = new System.Drawing.Point(621, 279);
            this.pnlBackground.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(184, 143);
            this.pnlBackground.TabIndex = 2;
            // 
            // lblGenerando
            // 
            this.lblGenerando.AutoSize = true;
            this.lblGenerando.Location = new System.Drawing.Point(25, 117);
            this.lblGenerando.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGenerando.Name = "lblGenerando";
            this.lblGenerando.Size = new System.Drawing.Size(132, 17);
            this.lblGenerando.TabIndex = 1;
            this.lblGenerando.Text = "Por favor espere...!!";
            // 
            // pictureBoxCargando
            // 
            this.pictureBoxCargando.Image = global::BcoPopular.Plugin.Properties.Resources.ajax_loader;
            this.pictureBoxCargando.Location = new System.Drawing.Point(0, -6);
            this.pictureBoxCargando.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxCargando.Name = "pictureBoxCargando";
            this.pictureBoxCargando.Size = new System.Drawing.Size(185, 118);
            this.pictureBoxCargando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCargando.TabIndex = 0;
            this.pictureBoxCargando.TabStop = false;
            this.pictureBoxCargando.Visible = false;
            // 
            // backgroundWorkerReport
            // 
            this.backgroundWorkerReport.WorkerReportsProgress = true;
            this.backgroundWorkerReport.WorkerSupportsCancellation = true;
            this.backgroundWorkerReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReport_DoWork);
            this.backgroundWorkerReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerReport_ProgressChanged);
            this.backgroundWorkerReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerReport_RunWorkerCompleted);
            // 
            // cuadreConsolidadoTarjetastoolStripMenuItem
            // 
            this.cuadreConsolidadoTarjetastoolStripMenuItem.Name = "cuadreConsolidadoTarjetastoolStripMenuItem";
            this.cuadreConsolidadoTarjetastoolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.cuadreConsolidadoTarjetastoolStripMenuItem.Text = "CUADRE CONSOLIDADO TARJETAS";
            this.cuadreConsolidadoTarjetastoolStripMenuItem.Click += new System.EventHandler(this.cuadreConsolidadoTarjetastoolStripMenuItem_Click);
            // 
            // frmReportesImpuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 738);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.reportViewerImpuestos);
            this.Controls.Add(this.menuStripReportes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStripReportes;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmReportesImpuestos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reportes Impuestos";
            this.Load += new System.EventHandler(this.frmReportesImpuestos_Load);
            this.menuStripReportes.ResumeLayout(false);
            this.menuStripReportes.PerformLayout();
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCargando)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripReportes;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerImpuestos;
        private System.Windows.Forms.ToolStripMenuItem ReportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConsolidadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PagosToolStripMenuItem;
        public System.Windows.Forms.ImageList imageListImpuestos;
        private System.Windows.Forms.ToolStripMenuItem otrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faltantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobrantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InformeConsolidadoToolStripMenuItem;        

        private System.Windows.Forms.ToolStripMenuItem SistematizacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TarjetasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CuadreToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem Dispercion100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionDeptoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionMunicipioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionDispercionSistematizacionToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem SistematizacionCorregidaToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem DispersionAutoMunicipiosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispersionAutoDepartamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispersionAutoSistematizacionToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem InformeCiudadToolStripMenuItem;
        private System.Windows.Forms.Panel pnlBackground;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReport;
        private System.Windows.Forms.PictureBox pictureBoxCargando;
        private System.Windows.Forms.Label lblGenerando;
        private System.Windows.Forms.ToolStripMenuItem DetalleRechazosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuadreConsolidadoTarjetastoolStripMenuItem;
    }
}