namespace BcoPopular.Plugin.Imaging.Asistidos.Forms
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
            this.InformeMensualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RepConciliacionPrecapturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DispercionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ReportesToolStripMenuItem});
            this.menuStripReportes.Location = new System.Drawing.Point(0, 0);
            this.menuStripReportes.Name = "menuStripReportes";
            this.menuStripReportes.Size = new System.Drawing.Size(1040, 24);
            this.menuStripReportes.TabIndex = 0;
            this.menuStripReportes.Text = "menuStrip1";
            // 
            // ReportesToolStripMenuItem
            // 
            this.ReportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RepConciliacionPrecapturaToolStripMenuItem,
            this.InformeMensualToolStripMenuItem});
            this.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem";
            this.ReportesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ReportesToolStripMenuItem.Text = "Reportes";
            // 
            // InformeMensualToolStripMenuItem
            // 
            this.InformeMensualToolStripMenuItem.Name = "InformeMensualToolStripMenuItem";
            this.InformeMensualToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.InformeMensualToolStripMenuItem.Text = "INFORME MENSUAL";
            this.InformeMensualToolStripMenuItem.Click += new System.EventHandler(this.InformeMensualToolStripMenuItem_Click);
            // 
            // RepConciliacionPrecapturaToolStripMenuItem
            // 
            this.RepConciliacionPrecapturaToolStripMenuItem.Name = "RepConciliacionPrecapturaToolStripMenuItem";
            this.RepConciliacionPrecapturaToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.RepConciliacionPrecapturaToolStripMenuItem.Text = "REPORTE CONCILIACION PRECAPTURA";
            this.RepConciliacionPrecapturaToolStripMenuItem.Click += new System.EventHandler(this.RepConciliacionPrecapturaToolStripMenuItem_Click);
            // 
            // DispercionToolStripMenuItem
            // 
            this.DispercionToolStripMenuItem.Name = "DispercionToolStripMenuItem";
            this.DispercionToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.DispercionToolStripMenuItem.Text = "DISPERCION QUINCENAL";
            // 
            // reportViewerImpuestos
            // 
            this.reportViewerImpuestos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerImpuestos.Location = new System.Drawing.Point(0, 24);
            this.reportViewerImpuestos.Name = "reportViewerImpuestos";
            this.reportViewerImpuestos.Size = new System.Drawing.Size(1040, 576);
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
            this.ClientSize = new System.Drawing.Size(1040, 600);
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
        private System.Windows.Forms.ToolStripMenuItem InformeMensualToolStripMenuItem;
        public System.Windows.Forms.ImageList imageListImpuestos;
        private System.Windows.Forms.ToolStripMenuItem DispercionToolStripMenuItem;
        private System.Windows.Forms.Panel pnlBackground;
        private System.ComponentModel.BackgroundWorker backgroundWorkerReport;
        private System.Windows.Forms.PictureBox pictureBoxCargando;
        private System.Windows.Forms.Label lblGenerando;
        private System.Windows.Forms.ToolStripMenuItem RepConciliacionPrecapturaToolStripMenuItem;
    }
}