namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
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
            this.WordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reporte_1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reporte_2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.matriz1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matriz2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dispersion1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desperion2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faltantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobrantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inconsistenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novedadesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteCartaEntregaFisicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteFranquiciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empaqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesEmpaqueOperaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteFormulariosConRespuestaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fechaRecaudoDetalladoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infivalleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nOVALLEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.WordToolStripMenuItem,
            this.excelToolStripMenuItem1,
            this.otrosToolStripMenuItem});
            this.menuStripReportes.Location = new System.Drawing.Point(0, 0);
            this.menuStripReportes.Name = "menuStripReportes";
            this.menuStripReportes.Size = new System.Drawing.Size(1093, 24);
            this.menuStripReportes.TabIndex = 0;
            this.menuStripReportes.Text = "menuStrip1";
            // 
            // WordToolStripMenuItem
            // 
            this.WordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Reporte_1ToolStripMenuItem,
            this.Reporte_2ToolStripMenuItem});
            this.WordToolStripMenuItem.Name = "WordToolStripMenuItem";
            this.WordToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.WordToolStripMenuItem.Text = "Word";
            // 
            // Reporte_1ToolStripMenuItem
            // 
            this.Reporte_1ToolStripMenuItem.Name = "Reporte_1ToolStripMenuItem";
            this.Reporte_1ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.Reporte_1ToolStripMenuItem.Text = "Reporte Municipios - 1";
            this.Reporte_1ToolStripMenuItem.Click += new System.EventHandler(this.Reporte_1ToolStripMenuItem_Click);
            // 
            // Reporte_2ToolStripMenuItem
            // 
            this.Reporte_2ToolStripMenuItem.Name = "Reporte_2ToolStripMenuItem";
            this.Reporte_2ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.Reporte_2ToolStripMenuItem.Text = "Reporte Municipios - 2";
            this.Reporte_2ToolStripMenuItem.Click += new System.EventHandler(this.Reporte_2ToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem1
            // 
            this.excelToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matriz1ToolStripMenuItem,
            this.matriz2ToolStripMenuItem,
            this.dispersion1ToolStripMenuItem,
            this.desperion2ToolStripMenuItem});
            this.excelToolStripMenuItem1.Name = "excelToolStripMenuItem1";
            this.excelToolStripMenuItem1.Size = new System.Drawing.Size(64, 20);
            this.excelToolStripMenuItem1.Text = "Matrices";
            // 
            // matriz1ToolStripMenuItem
            // 
            this.matriz1ToolStripMenuItem.Name = "matriz1ToolStripMenuItem";
            this.matriz1ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.matriz1ToolStripMenuItem.Text = "Matriz - 1";
            this.matriz1ToolStripMenuItem.Click += new System.EventHandler(this.matriz1ToolStripMenuItem_Click);
            // 
            // matriz2ToolStripMenuItem
            // 
            this.matriz2ToolStripMenuItem.Name = "matriz2ToolStripMenuItem";
            this.matriz2ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.matriz2ToolStripMenuItem.Text = "Matriz - 2";
            this.matriz2ToolStripMenuItem.Click += new System.EventHandler(this.matriz2ToolStripMenuItem_Click);
            // 
            // dispersion1ToolStripMenuItem
            // 
            this.dispersion1ToolStripMenuItem.Name = "dispersion1ToolStripMenuItem";
            this.dispersion1ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.dispersion1ToolStripMenuItem.Text = "Dispersion Detallada";
            this.dispersion1ToolStripMenuItem.Click += new System.EventHandler(this.dispersion1ToolStripMenuItem_Click);
            // 
            // desperion2ToolStripMenuItem
            // 
            this.desperion2ToolStripMenuItem.Name = "desperion2ToolStripMenuItem";
            this.desperion2ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.desperion2ToolStripMenuItem.Text = "Dispersion Resumida";
            this.desperion2ToolStripMenuItem.Click += new System.EventHandler(this.desperion2ToolStripMenuItem_Click);
            // 
            // otrosToolStripMenuItem
            // 
            this.otrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.faltantesToolStripMenuItem,
            this.sobrantesToolStripMenuItem,
            this.inconsistenciasToolStripMenuItem,
            this.novedadesToolStripMenuItem1,
            this.reporteCartaEntregaFisicoToolStripMenuItem,
            this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem,
            this.reporteFranquiciasToolStripMenuItem,
            this.empaqueToolStripMenuItem,
            this.fechaRecaudoDetalladoToolStripMenuItem,
            this.todosToolStripMenuItem});
            this.otrosToolStripMenuItem.Name = "otrosToolStripMenuItem";
            this.otrosToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.otrosToolStripMenuItem.Text = "Otros";
            // 
            // faltantesToolStripMenuItem
            // 
            this.faltantesToolStripMenuItem.Name = "faltantesToolStripMenuItem";
            this.faltantesToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.faltantesToolStripMenuItem.Text = "Faltantes";
            this.faltantesToolStripMenuItem.Click += new System.EventHandler(this.faltantesToolStripMenuItem_Click);
            // 
            // sobrantesToolStripMenuItem
            // 
            this.sobrantesToolStripMenuItem.Name = "sobrantesToolStripMenuItem";
            this.sobrantesToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.sobrantesToolStripMenuItem.Text = "Sobrantes";
            this.sobrantesToolStripMenuItem.Click += new System.EventHandler(this.sobrantesToolStripMenuItem_Click);
            // 
            // inconsistenciasToolStripMenuItem
            // 
            this.inconsistenciasToolStripMenuItem.Name = "inconsistenciasToolStripMenuItem";
            this.inconsistenciasToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.inconsistenciasToolStripMenuItem.Text = "Conciliacion";
            this.inconsistenciasToolStripMenuItem.Click += new System.EventHandler(this.inconsistenciasToolStripMenuItem_Click);
            // 
            // novedadesToolStripMenuItem1
            // 
            this.novedadesToolStripMenuItem1.Name = "novedadesToolStripMenuItem1";
            this.novedadesToolStripMenuItem1.Size = new System.Drawing.Size(312, 22);
            this.novedadesToolStripMenuItem1.Text = "Novedades";
            // 
            // reporteCartaEntregaFisicoToolStripMenuItem
            // 
            this.reporteCartaEntregaFisicoToolStripMenuItem.Name = "reporteCartaEntregaFisicoToolStripMenuItem";
            this.reporteCartaEntregaFisicoToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.reporteCartaEntregaFisicoToolStripMenuItem.Text = "Reporte Carta Entrega Fisico";
            this.reporteCartaEntregaFisicoToolStripMenuItem.Click += new System.EventHandler(this.reporteCartaEntregaFisicoToolStripMenuItem_Click);
            // 
            // reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem
            // 
            this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem.Name = "reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem";
            this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem.Text = "Reporte Agrupado Departamentos NO VALLE";
            this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem.Click += new System.EventHandler(this.reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem_Click);
            // 
            // reporteFranquiciasToolStripMenuItem
            // 
            this.reporteFranquiciasToolStripMenuItem.Name = "reporteFranquiciasToolStripMenuItem";
            this.reporteFranquiciasToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.reporteFranquiciasToolStripMenuItem.Text = "Reporte Franquicias";
            this.reporteFranquiciasToolStripMenuItem.Click += new System.EventHandler(this.reporteFranquiciasToolStripMenuItem_Click);
            // 
            // empaqueToolStripMenuItem
            // 
            this.empaqueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportesEmpaqueOperaciónToolStripMenuItem,
            this.reporteFormulariosConRespuestaToolStripMenuItem});
            this.empaqueToolStripMenuItem.Name = "empaqueToolStripMenuItem";
            this.empaqueToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.empaqueToolStripMenuItem.Text = "Empaque";
            // 
            // reportesEmpaqueOperaciónToolStripMenuItem
            // 
            this.reportesEmpaqueOperaciónToolStripMenuItem.Name = "reportesEmpaqueOperaciónToolStripMenuItem";
            this.reportesEmpaqueOperaciónToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.reportesEmpaqueOperaciónToolStripMenuItem.Text = "Reportes Empaque Operación";
            this.reportesEmpaqueOperaciónToolStripMenuItem.Click += new System.EventHandler(this.reportesEmpaqueOperaciónToolStripMenuItem_Click);
            // 
            // reporteFormulariosConRespuestaToolStripMenuItem
            // 
            this.reporteFormulariosConRespuestaToolStripMenuItem.Name = "reporteFormulariosConRespuestaToolStripMenuItem";
            this.reporteFormulariosConRespuestaToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.reporteFormulariosConRespuestaToolStripMenuItem.Text = "Inconsistencias Historicas";
            this.reporteFormulariosConRespuestaToolStripMenuItem.Click += new System.EventHandler(this.reporteFormulariosConRespuestaToolStripMenuItem_Click);
            // 
            // fechaRecaudoDetalladoToolStripMenuItem
            // 
            this.fechaRecaudoDetalladoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infivalleToolStripMenuItem,
            this.nOVALLEToolStripMenuItem});
            this.fechaRecaudoDetalladoToolStripMenuItem.Name = "fechaRecaudoDetalladoToolStripMenuItem";
            this.fechaRecaudoDetalladoToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.fechaRecaudoDetalladoToolStripMenuItem.Text = "Fecha Recaudo Detallado";
            // 
            // infivalleToolStripMenuItem
            // 
            this.infivalleToolStripMenuItem.Name = "infivalleToolStripMenuItem";
            this.infivalleToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.infivalleToolStripMenuItem.Text = "Infivalle";
            this.infivalleToolStripMenuItem.Click += new System.EventHandler(this.infivalleToolStripMenuItem_Click);
            // 
            // nOVALLEToolStripMenuItem
            // 
            this.nOVALLEToolStripMenuItem.Name = "nOVALLEToolStripMenuItem";
            this.nOVALLEToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.nOVALLEToolStripMenuItem.Text = "NO VALLE";
            this.nOVALLEToolStripMenuItem.Click += new System.EventHandler(this.nOVALLEToolStripMenuItem_Click);
            // 
            // todosToolStripMenuItem
            // 
            this.todosToolStripMenuItem.Name = "todosToolStripMenuItem";
            this.todosToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.todosToolStripMenuItem.Text = "Todos";
            this.todosToolStripMenuItem.Click += new System.EventHandler(this.todosToolStripMenuItem_Click);
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
            this.pictureBoxCargando.Image = global::BcoBogota.Plugin.Properties.Resources.ajax_loader;
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
        private System.Windows.Forms.ToolStripMenuItem WordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Reporte_1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Reporte_2ToolStripMenuItem;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerImpuestos;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem matriz1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matriz2ToolStripMenuItem;
        public System.Windows.Forms.ImageList imageListImpuestos;
        private System.Windows.Forms.ToolStripMenuItem otrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faltantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobrantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inconsistenciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novedadesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem todosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dispersion1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desperion2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteCartaEntregaFisicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteFranquiciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empaqueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesEmpaqueOperaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteFormulariosConRespuestaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fechaRecaudoDetalladoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infivalleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nOVALLEToolStripMenuItem;
        private System.Windows.Forms.Panel pnlBackground;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReport;
        private System.Windows.Forms.PictureBox pictureBoxCargando;
        private System.Windows.Forms.Label lblGenerando;
    }
}