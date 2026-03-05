namespace BcoPopular.Plugin.Imaging.GobernacionSantander.Forms
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
            this.DispercionDeptoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionMunicipioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionDispercionSistematizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TapaRadicacionFisicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TapaRadicacionDispersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faltantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobrantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispersionAutoMunicipiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InscripcionAutoMunicipiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispersionAutoDepartamentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispersionAutoSistematizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DetalladoPorFechaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DetalleRechazosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DetalladoFisicoVsLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportViewerImpuestos = new Microsoft.Reporting.WinForms.ReportViewer();
            this.imageListImpuestos = new System.Windows.Forms.ImageList(this.components);
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblGenerando = new System.Windows.Forms.Label();
            this.pictureBoxCargando = new System.Windows.Forms.PictureBox();
            this.backgroundWorkerReport = new System.ComponentModel.BackgroundWorker();
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
            this.menuStripReportes.Size = new System.Drawing.Size(1093, 24);
            this.menuStripReportes.TabIndex = 0;
            this.menuStripReportes.Text = "menuStrip1";
            // 
            // ReportesToolStripMenuItem
            // 
            this.ReportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DispercionDeptoToolStripMenuItem,
            this.DispercionMunicipioToolStripMenuItem,
            this.DispercionDispercionSistematizacionToolStripMenuItem,
            this.TapaRadicacionFisicosToolStripMenuItem,
            this.TapaRadicacionDispersionToolStripMenuItem});
            this.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem";
            this.ReportesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ReportesToolStripMenuItem.Text = "Reportes";
            // 
            // DispercionDeptoToolStripMenuItem
            // 
            this.DispercionDeptoToolStripMenuItem.Name = "DispercionDeptoToolStripMenuItem";
            this.DispercionDeptoToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.DispercionDeptoToolStripMenuItem.Text = "DISPERSIÓN DEPARTAMENTO";
            this.DispercionDeptoToolStripMenuItem.Click += new System.EventHandler(this.DispercionDeptoToolStripMenuItem_Click);
            // 
            // DispercionMunicipioToolStripMenuItem
            // 
            this.DispercionMunicipioToolStripMenuItem.Name = "DispercionMunicipioToolStripMenuItem";
            this.DispercionMunicipioToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.DispercionMunicipioToolStripMenuItem.Text = "DISPERSIÓN MUNICIPIOS";
            this.DispercionMunicipioToolStripMenuItem.Click += new System.EventHandler(this.DispercionMunicipioToolStripMenuItem_Click);
            // 
            // DispercionDispercionSistematizacionToolStripMenuItem
            // 
            this.DispercionDispercionSistematizacionToolStripMenuItem.Name = "DispercionDispercionSistematizacionToolStripMenuItem";
            this.DispercionDispercionSistematizacionToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.DispercionDispercionSistematizacionToolStripMenuItem.Text = "DISPERSIÓN SISTEMATIZACIÓN";
            this.DispercionDispercionSistematizacionToolStripMenuItem.Click += new System.EventHandler(this.DispercionDispercionSistematizacionToolStripMenuItem_Click);
            // 
            // TapaRadicacionFisicosToolStripMenuItem
            // 
            this.TapaRadicacionFisicosToolStripMenuItem.Name = "TapaRadicacionFisicosToolStripMenuItem";
            this.TapaRadicacionFisicosToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.TapaRadicacionFisicosToolStripMenuItem.Text = "TAPA RADICACIÓN FISICOS";
            this.TapaRadicacionFisicosToolStripMenuItem.Click += new System.EventHandler(this.TapaRadicacionFisicosToolStripMenuItem_Click);
            // 
            // TapaRadicacionDispersionToolStripMenuItem
            // 
            this.TapaRadicacionDispersionToolStripMenuItem.Name = "TapaRadicacionDispersionToolStripMenuItem";
            this.TapaRadicacionDispersionToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.TapaRadicacionDispersionToolStripMenuItem.Text = "CARTA DISPERSIÓN Y RECHAZOS";
            this.TapaRadicacionDispersionToolStripMenuItem.Click += new System.EventHandler(this.TapaRadicacionDispersionToolStripMenuItem_Click);
            // 
            // otrosToolStripMenuItem
            // 
            this.otrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.faltantesToolStripMenuItem,
            this.sobrantesToolStripMenuItem,
            this.DispersionAutoMunicipiosToolStripMenuItem,
            this.InscripcionAutoMunicipiosToolStripMenuItem,
            this.DispersionAutoDepartamentosToolStripMenuItem,
            this.DispersionAutoSistematizacionToolStripMenuItem,
            this.DetalladoPorFechaToolStripMenuItem,
            this.DetalleRechazosToolStripMenuItem,
            this.DetalladoFisicoVsLogToolStripMenuItem});
            this.otrosToolStripMenuItem.Name = "otrosToolStripMenuItem";
            this.otrosToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.otrosToolStripMenuItem.Text = "Otros";
            // 
            // faltantesToolStripMenuItem
            // 
            this.faltantesToolStripMenuItem.Name = "faltantesToolStripMenuItem";
            this.faltantesToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.faltantesToolStripMenuItem.Text = "FALTANTES";
            this.faltantesToolStripMenuItem.Click += new System.EventHandler(this.faltantesToolStripMenuItem_Click);
            // 
            // sobrantesToolStripMenuItem
            // 
            this.sobrantesToolStripMenuItem.Name = "sobrantesToolStripMenuItem";
            this.sobrantesToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.sobrantesToolStripMenuItem.Text = "SOBRANTES";
            this.sobrantesToolStripMenuItem.Click += new System.EventHandler(this.sobrantesToolStripMenuItem_Click);
            // 
            // DispersionAutoMunicipiosToolStripMenuItem
            // 
            this.DispersionAutoMunicipiosToolStripMenuItem.Name = "DispersionAutoMunicipiosToolStripMenuItem";
            this.DispersionAutoMunicipiosToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DispersionAutoMunicipiosToolStripMenuItem.Text = "DISPERSION AUTOMATICA MUNICIPIOS";
            this.DispersionAutoMunicipiosToolStripMenuItem.Click += new System.EventHandler(this.DispersionAutoMunicipiosToolStripMenuItem_Click);
            // 
            // InscripcionAutoMunicipiosToolStripMenuItem
            // 
            this.InscripcionAutoMunicipiosToolStripMenuItem.Name = "InscripcionAutoMunicipiosToolStripMenuItem";
            this.InscripcionAutoMunicipiosToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.InscripcionAutoMunicipiosToolStripMenuItem.Text = "INSCRIPCION AUTOMATICA MUNICIPIOS";
            this.InscripcionAutoMunicipiosToolStripMenuItem.Click += new System.EventHandler(this.InscripcionAutoMunicipiosToolStripMenuItem_Click);
            // 
            // DispersionAutoDepartamentosToolStripMenuItem
            // 
            this.DispersionAutoDepartamentosToolStripMenuItem.Name = "DispersionAutoDepartamentosToolStripMenuItem";
            this.DispersionAutoDepartamentosToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DispersionAutoDepartamentosToolStripMenuItem.Text = "DISPERSION AUTOMATICA DEPARTAMENTOS";
            this.DispersionAutoDepartamentosToolStripMenuItem.Click += new System.EventHandler(this.DispersionAutoDepartamentosToolStripMenuItem_Click);
            // 
            // DispersionAutoSistematizacionToolStripMenuItem
            // 
            this.DispersionAutoSistematizacionToolStripMenuItem.Name = "DispersionAutoSistematizacionToolStripMenuItem";
            this.DispersionAutoSistematizacionToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DispersionAutoSistematizacionToolStripMenuItem.Text = "DISPERSION AUTOMATICA SISTEMATIZACIÓN";
            this.DispersionAutoSistematizacionToolStripMenuItem.Click += new System.EventHandler(this.DispersionAutoSistematizacionToolStripMenuItem_Click);
            // 
            // DetalladoPorFechaToolStripMenuItem
            // 
            this.DetalladoPorFechaToolStripMenuItem.Name = "DetalladoPorFechaToolStripMenuItem";
            this.DetalladoPorFechaToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DetalladoPorFechaToolStripMenuItem.Text = "DETALLADO POR FECHA";
            this.DetalladoPorFechaToolStripMenuItem.Click += new System.EventHandler(this.DetalladoPorFechaToolStripMenuItem_Click);
            // 
            // DetalleRechazosToolStripMenuItem
            // 
            this.DetalleRechazosToolStripMenuItem.Name = "DetalleRechazosToolStripMenuItem";
            this.DetalleRechazosToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DetalleRechazosToolStripMenuItem.Text = "DETALLADO RECHAZOS";
            this.DetalleRechazosToolStripMenuItem.Click += new System.EventHandler(this.DetalleRechazosToolStripMenuItem_Click);
            // 
            // DetalladoFisicoVsLogToolStripMenuItem
            // 
            this.DetalladoFisicoVsLogToolStripMenuItem.Name = "DetalladoFisicoVsLogToolStripMenuItem";
            this.DetalladoFisicoVsLogToolStripMenuItem.Size = new System.Drawing.Size(322, 22);
            this.DetalladoFisicoVsLogToolStripMenuItem.Text = "DETALLADO CRUCE FISICO VS LOG";
            this.DetalladoFisicoVsLogToolStripMenuItem.Click += new System.EventHandler(this.DetalladoFisicoVsLogToolStripMenuItem_Click);
            // 
            // reportViewerImpuestos
            // 
            this.reportViewerImpuestos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerImpuestos.Location = new System.Drawing.Point(0, 24);
            this.reportViewerImpuestos.Name = "reportViewerImpuestos";
            this.reportViewerImpuestos.Size = new System.Drawing.Size(1093, 576);
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
            this.pnlBackground.Location = new System.Drawing.Point(466, 227);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(138, 116);
            this.pnlBackground.TabIndex = 2;
            // 
            // lblGenerando
            // 
            this.lblGenerando.AutoSize = true;
            this.lblGenerando.Location = new System.Drawing.Point(19, 95);
            this.lblGenerando.Name = "lblGenerando";
            this.lblGenerando.Size = new System.Drawing.Size(100, 13);
            this.lblGenerando.TabIndex = 1;
            this.lblGenerando.Text = "Por favor espere...!!";
            // 
            // pictureBoxCargando
            // 
            this.pictureBoxCargando.Image = global::BcoPopular.Plugin.Properties.Resources.ajax_loader;
            this.pictureBoxCargando.Location = new System.Drawing.Point(0, -5);
            this.pictureBoxCargando.Name = "pictureBoxCargando";
            this.pictureBoxCargando.Size = new System.Drawing.Size(139, 96);
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
            // frmReportesImpuestos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 600);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.reportViewerImpuestos);
            this.Controls.Add(this.menuStripReportes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStripReportes;
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
        //private System.Windows.Forms.ToolStripMenuItem ConsolidadoToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem PagosToolStripMenuItem;
        public System.Windows.Forms.ImageList imageListImpuestos;
        private System.Windows.Forms.ToolStripMenuItem otrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faltantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobrantesToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem DispercionToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem InformeConsolidadoToolStripMenuItem;        

        //private System.Windows.Forms.ToolStripMenuItem SistematizacionToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem TarjetasToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem CuadreToolStripMenuItem;

        //private System.Windows.Forms.ToolStripMenuItem Dispercion100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionDeptoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionMunicipioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispercionDispercionSistematizacionToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem TapaRadicacionFisicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TapaRadicacionDispersionToolStripMenuItem;

        //private System.Windows.Forms.ToolStripMenuItem SistematizacionCorregidaToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem DispersionAutoMunicipiosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InscripcionAutoMunicipiosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispersionAutoDepartamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DispersionAutoSistematizacionToolStripMenuItem;

        //private System.Windows.Forms.ToolStripMenuItem InformeCiudadToolStripMenuItem;
        private System.Windows.Forms.Panel pnlBackground;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReport;
        private System.Windows.Forms.PictureBox pictureBoxCargando;
        private System.Windows.Forms.Label lblGenerando;
        private System.Windows.Forms.ToolStripMenuItem DetalladoPorFechaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DetalleRechazosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DetalladoFisicoVsLogToolStripMenuItem;
    }
}