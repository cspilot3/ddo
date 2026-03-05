namespace Banagrario.Scanning.Forms
{
    partial class FormReprocesos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReprocesos));
            this.BaseGroupBox = new System.Windows.Forms.GroupBox();
            this.CargueListView = new System.Windows.Forms.ListView();
            this.IDColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TokenColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DocumentoColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescripcionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconosImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.ReprocesosLabel = new System.Windows.Forms.Label();
            this.EsquemaPictureBox = new System.Windows.Forms.PictureBox();
            this.ProcesarButton = new System.Windows.Forms.Button();
            this.CerrarButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BaseGroupBox.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EsquemaPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseGroupBox
            // 
            this.BaseGroupBox.Controls.Add(this.CargueListView);
            this.BaseGroupBox.Controls.Add(this.panel3);
            this.BaseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseGroupBox.Location = new System.Drawing.Point(5, 5);
            this.BaseGroupBox.Name = "BaseGroupBox";
            this.BaseGroupBox.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.BaseGroupBox.Size = new System.Drawing.Size(802, 342);
            this.BaseGroupBox.TabIndex = 3;
            this.BaseGroupBox.TabStop = false;
            // 
            // CargueListView
            // 
            this.CargueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDColumnHeader,
            this.FechaColumnHeader,
            this.TokenColumnHeader,
            this.DocumentoColumnHeader,
            this.DescripcionColumnHeader});
            this.CargueListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CargueListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CargueListView.FullRowSelect = true;
            this.CargueListView.HideSelection = false;
            this.CargueListView.Location = new System.Drawing.Point(10, 45);
            this.CargueListView.MultiSelect = false;
            this.CargueListView.Name = "CargueListView";
            this.CargueListView.Size = new System.Drawing.Size(782, 287);
            this.CargueListView.SmallImageList = this.IconosImageList;
            this.CargueListView.TabIndex = 1;
            this.CargueListView.UseCompatibleStateImageBehavior = false;
            this.CargueListView.View = System.Windows.Forms.View.Details;
            this.CargueListView.SelectedIndexChanged += new System.EventHandler(this.CargueListView_SelectedIndexChanged);
            this.CargueListView.DoubleClick += new System.EventHandler(this.CargueListView_DoubleClick);
            // 
            // IDColumnHeader
            // 
            this.IDColumnHeader.Text = "";
            this.IDColumnHeader.Width = 40;
            // 
            // FechaColumnHeader
            // 
            this.FechaColumnHeader.Text = "Fecha";
            this.FechaColumnHeader.Width = 100;
            // 
            // TokenColumnHeader
            // 
            this.TokenColumnHeader.Text = "Identificador";
            this.TokenColumnHeader.Width = 150;
            // 
            // DocumentoColumnHeader
            // 
            this.DocumentoColumnHeader.Text = "Documento";
            this.DocumentoColumnHeader.Width = 200;
            // 
            // DescripcionColumnHeader
            // 
            this.DescripcionColumnHeader.Text = "Descripción";
            this.DescripcionColumnHeader.Width = 300;
            // 
            // IconosImageList
            // 
            this.IconosImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconosImageList.ImageStream")));
            this.IconosImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconosImageList.Images.SetKeyName(0, "image_edit.png");
            this.IconosImageList.Images.SetKeyName(1, "image_edit.png");
            this.IconosImageList.Images.SetKeyName(2, "image_edit.png");
            this.IconosImageList.Images.SetKeyName(3, "folder_image.png");
            this.IconosImageList.Images.SetKeyName(4, "images.png");
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ReprocesosLabel);
            this.panel3.Controls.Add(this.EsquemaPictureBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(782, 29);
            this.panel3.TabIndex = 9;
            // 
            // ReprocesosLabel
            // 
            this.ReprocesosLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReprocesosLabel.Location = new System.Drawing.Point(26, 5);
            this.ReprocesosLabel.Name = "ReprocesosLabel";
            this.ReprocesosLabel.Size = new System.Drawing.Size(128, 16);
            this.ReprocesosLabel.TabIndex = 0;
            this.ReprocesosLabel.Text = "Reprocesos";
            // 
            // EsquemaPictureBox
            // 
            this.EsquemaPictureBox.Image = global::Banagrario.Scanning.Properties.Resources.image_edit;
            this.EsquemaPictureBox.Location = new System.Drawing.Point(4, 5);
            this.EsquemaPictureBox.Name = "EsquemaPictureBox";
            this.EsquemaPictureBox.Size = new System.Drawing.Size(16, 16);
            this.EsquemaPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.EsquemaPictureBox.TabIndex = 8;
            this.EsquemaPictureBox.TabStop = false;
            // 
            // ProcesarButton
            // 
            this.ProcesarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcesarButton.Image = global::Banagrario.Scanning.Properties.Resources.tick;
            this.ProcesarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ProcesarButton.Location = new System.Drawing.Point(14, 6);
            this.ProcesarButton.Name = "ProcesarButton";
            this.ProcesarButton.Size = new System.Drawing.Size(80, 31);
            this.ProcesarButton.TabIndex = 4;
            this.ProcesarButton.Text = "&Procesar";
            this.ProcesarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ProcesarButton.Click += new System.EventHandler(this.ProcesarButton_Click);
            // 
            // CerrarButton
            // 
            this.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CerrarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CerrarButton.Image = global::Banagrario.Scanning.Properties.Resources.cancel;
            this.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CerrarButton.Location = new System.Drawing.Point(110, 6);
            this.CerrarButton.Name = "CerrarButton";
            this.CerrarButton.Size = new System.Drawing.Size(80, 31);
            this.CerrarButton.TabIndex = 5;
            this.CerrarButton.Text = "&Cerrar";
            this.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CerrarButton.Click += new System.EventHandler(this.CerrarButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(5, 347);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 41);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CerrarButton);
            this.panel2.Controls.Add(this.ProcesarButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(602, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 41);
            this.panel2.TabIndex = 0;
            // 
            // FormReprocesos
            // 
            this.AcceptButton = this.ProcesarButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CerrarButton;
            this.ClientSize = new System.Drawing.Size(812, 393);
            this.Controls.Add(this.BaseGroupBox);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormReprocesos";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Reprocesos";
            this.Load += new System.EventHandler(this.FormReprocesos_Load);
            this.BaseGroupBox.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EsquemaPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button ProcesarButton;
        internal System.Windows.Forms.Button CerrarButton;
        internal System.Windows.Forms.GroupBox BaseGroupBox;
        internal System.Windows.Forms.PictureBox EsquemaPictureBox;
        internal System.Windows.Forms.Label ReprocesosLabel;
        internal System.Windows.Forms.ListView CargueListView;
        internal System.Windows.Forms.ColumnHeader TokenColumnHeader;
        internal System.Windows.Forms.ColumnHeader DocumentoColumnHeader;
        internal System.Windows.Forms.ColumnHeader FechaColumnHeader;
        internal System.Windows.Forms.ColumnHeader DescripcionColumnHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColumnHeader IDColumnHeader;
        internal System.Windows.Forms.ImageList IconosImageList;
    }
}