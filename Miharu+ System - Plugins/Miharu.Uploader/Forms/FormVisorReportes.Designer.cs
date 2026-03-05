namespace Miharu.Uploader.Forms
{
    partial class FormVisorReportes
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
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ReportesTreeView = new System.Windows.Forms.TreeView();
            this.resultadosPanel = new System.Windows.Forms.Panel();
            this.parametrosGroupBox = new System.Windows.Forms.GroupBox();
            this.camposPanel = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.ExportarMasivoButton = new System.Windows.Forms.Button();
            this.ejecutarButton = new System.Windows.Forms.Button();
            this.nombreReporteLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.parametrosGroupBox.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.ReportesTreeView);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.resultadosPanel);
            this.SplitContainer1.Panel2.Controls.Add(this.parametrosGroupBox);
            this.SplitContainer1.Panel2.Controls.Add(this.nombreReporteLabel);
            this.SplitContainer1.Size = new System.Drawing.Size(792, 573);
            this.SplitContainer1.SplitterDistance = 227;
            this.SplitContainer1.TabIndex = 1;
            // 
            // ReportesTreeView
            // 
            this.ReportesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportesTreeView.Location = new System.Drawing.Point(0, 0);
            this.ReportesTreeView.Name = "ReportesTreeView";
            this.ReportesTreeView.Size = new System.Drawing.Size(227, 573);
            this.ReportesTreeView.TabIndex = 0;
            // 
            // resultadosPanel
            // 
            this.resultadosPanel.AutoScroll = true;
            this.resultadosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultadosPanel.Location = new System.Drawing.Point(0, 216);
            this.resultadosPanel.Name = "resultadosPanel";
            this.resultadosPanel.Size = new System.Drawing.Size(561, 357);
            this.resultadosPanel.TabIndex = 3;
            // 
            // parametrosGroupBox
            // 
            this.parametrosGroupBox.Controls.Add(this.camposPanel);
            this.parametrosGroupBox.Controls.Add(this.Panel1);
            this.parametrosGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.parametrosGroupBox.Location = new System.Drawing.Point(0, 29);
            this.parametrosGroupBox.Name = "parametrosGroupBox";
            this.parametrosGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.parametrosGroupBox.Size = new System.Drawing.Size(561, 187);
            this.parametrosGroupBox.TabIndex = 0;
            this.parametrosGroupBox.TabStop = false;
            this.parametrosGroupBox.Text = "Parámetros";
            // 
            // camposPanel
            // 
            this.camposPanel.AutoScroll = true;
            this.camposPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(5, 18);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Padding = new System.Windows.Forms.Padding(5);
            this.camposPanel.Size = new System.Drawing.Size(494, 164);
            this.camposPanel.TabIndex = 0;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.ExportarMasivoButton);
            this.Panel1.Controls.Add(this.ejecutarButton);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel1.Location = new System.Drawing.Point(499, 18);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(57, 164);
            this.Panel1.TabIndex = 15;
            // 
            // ExportarMasivoButton
            // 
            this.ExportarMasivoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportarMasivoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportarMasivoButton.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportarMasivoButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExportarMasivoButton.Location = new System.Drawing.Point(3, 111);
            this.ExportarMasivoButton.Name = "ExportarMasivoButton";
            this.ExportarMasivoButton.Size = new System.Drawing.Size(51, 50);
            this.ExportarMasivoButton.TabIndex = 2;
            this.ExportarMasivoButton.Text = "Exportar Masivo";
            this.ExportarMasivoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ExportarMasivoButton.UseVisualStyleBackColor = true;
            this.ExportarMasivoButton.Visible = false;
            // 
            // ejecutarButton
            // 
            this.ejecutarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ejecutarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ejecutarButton.Location = new System.Drawing.Point(4, 4);
            this.ejecutarButton.Name = "ejecutarButton";
            this.ejecutarButton.Size = new System.Drawing.Size(46, 43);
            this.ejecutarButton.TabIndex = 1;
            this.ejecutarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ejecutarButton.UseVisualStyleBackColor = true;
            // 
            // nombreReporteLabel
            // 
            this.nombreReporteLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nombreReporteLabel.ForeColor = System.Drawing.Color.SeaGreen;
            this.nombreReporteLabel.Location = new System.Drawing.Point(0, 0);
            this.nombreReporteLabel.Name = "nombreReporteLabel";
            this.nombreReporteLabel.Size = new System.Drawing.Size(561, 29);
            this.nombreReporteLabel.TabIndex = 5;
            this.nombreReporteLabel.Text = "Nombre Informe";
            this.nombreReporteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormVisorReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.SplitContainer1);
            this.Name = "FormVisorReportes";
            this.Text = "FormVisorReportes";
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.parametrosGroupBox.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.TreeView ReportesTreeView;
        internal System.Windows.Forms.Panel resultadosPanel;
        internal System.Windows.Forms.GroupBox parametrosGroupBox;
        internal System.Windows.Forms.Panel camposPanel;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button ExportarMasivoButton;
        internal System.Windows.Forms.Button ejecutarButton;
        internal System.Windows.Forms.Label nombreReporteLabel;

    }
}