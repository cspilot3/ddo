namespace Banagrario.Scanning.Forms
{
    partial class FormCorreccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCorreccion));
            this.MenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.NewImagesListView = new System.Windows.Forms.ListView();
            this.OldImagesListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.FormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SeleccionarOrigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TransferirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.AdquirirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.InsertFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.InsertFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.NextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PreviousToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CancelarToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProcesoToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuToolStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.FormMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuToolStrip
            // 
            this.MenuToolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.MenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TransferirToolStripButton,
            this.ToolStripSeparator2,
            this.AdquirirToolStripButton,
            this.toolStripSeparator7,
            this.InsertFolderToolStripButton,
            this.ToolStripSeparator3,
            this.InsertFileToolStripButton,
            this.DeleteToolStripButton,
            this.ToolStripSeparator4,
            this.NextToolStripButton,
            this.PreviousToolStripButton,
            this.CancelarToolStripButton});
            this.MenuToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MenuToolStrip.Name = "MenuToolStrip";
            this.MenuToolStrip.Size = new System.Drawing.Size(755, 55);
            this.MenuToolStrip.TabIndex = 4;
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 55);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.NewImagesListView, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.OldImagesListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.MainStatusStrip, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 79);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(755, 397);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(749, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nuevas imágenes";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NewImagesListView
            // 
            this.NewImagesListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.NewImagesListView.BackColor = System.Drawing.Color.Silver;
            this.NewImagesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewImagesListView.HideSelection = false;
            this.NewImagesListView.Location = new System.Drawing.Point(3, 211);
            this.NewImagesListView.MultiSelect = false;
            this.NewImagesListView.Name = "NewImagesListView";
            this.NewImagesListView.Size = new System.Drawing.Size(749, 162);
            this.NewImagesListView.TabIndex = 7;
            this.NewImagesListView.UseCompatibleStateImageBehavior = false;
            this.NewImagesListView.SelectedIndexChanged += new System.EventHandler(this.ImagesListView_SelectedIndexChanged);
            // 
            // OldImagesListView
            // 
            this.OldImagesListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.OldImagesListView.BackColor = System.Drawing.Color.Silver;
            this.OldImagesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OldImagesListView.HideSelection = false;
            this.OldImagesListView.Location = new System.Drawing.Point(3, 23);
            this.OldImagesListView.MultiSelect = false;
            this.OldImagesListView.Name = "OldImagesListView";
            this.OldImagesListView.Size = new System.Drawing.Size(749, 162);
            this.OldImagesListView.TabIndex = 6;
            this.OldImagesListView.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(749, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Imágenes actuales";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMenuStrip
            // 
            this.FormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SeleccionarOrigenToolStripMenuItem});
            this.FormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.FormMenuStrip.Name = "FormMenuStrip";
            this.FormMenuStrip.Size = new System.Drawing.Size(755, 24);
            this.FormMenuStrip.TabIndex = 6;
            // 
            // SeleccionarOrigenToolStripMenuItem
            // 
            this.SeleccionarOrigenToolStripMenuItem.Name = "SeleccionarOrigenToolStripMenuItem";
            this.SeleccionarOrigenToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.SeleccionarOrigenToolStripMenuItem.Text = "Seleccionar origen";
            this.SeleccionarOrigenToolStripMenuItem.Click += new System.EventHandler(this.SeleccionarOrigenToolStripMenuItem_Click);
            // 
            // TransferirToolStripButton
            // 
            this.TransferirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TransferirToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.floppy_disk_48;
            this.TransferirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TransferirToolStripButton.Name = "TransferirToolStripButton";
            this.TransferirToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.TransferirToolStripButton.ToolTipText = "Transferir";
            this.TransferirToolStripButton.Click += new System.EventHandler(this.TransferirToolStripButton_Click);
            // 
            // AdquirirToolStripButton
            // 
            this.AdquirirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AdquirirToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.paper_content_48;
            this.AdquirirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AdquirirToolStripButton.Name = "AdquirirToolStripButton";
            this.AdquirirToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.AdquirirToolStripButton.ToolTipText = "Insertar imágenes desde el escaner";
            this.AdquirirToolStripButton.Click += new System.EventHandler(this.AdquirirToolStripButton_Click);
            // 
            // InsertFolderToolStripButton
            // 
            this.InsertFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsertFolderToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.folder_add_48;
            this.InsertFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertFolderToolStripButton.Name = "InsertFolderToolStripButton";
            this.InsertFolderToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.InsertFolderToolStripButton.ToolTipText = "Insertar todas las imagenes de una carpeta";
            this.InsertFolderToolStripButton.Click += new System.EventHandler(this.InsertFolderToolStripButton_Click);
            // 
            // InsertFileToolStripButton
            // 
            this.InsertFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsertFileToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.image_add_48;
            this.InsertFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertFileToolStripButton.Name = "InsertFileToolStripButton";
            this.InsertFileToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.InsertFileToolStripButton.Text = "ToolStripButton1";
            this.InsertFileToolStripButton.ToolTipText = "Insertar una imagen";
            this.InsertFileToolStripButton.Click += new System.EventHandler(this.InsertFileToolStripButton_Click);
            // 
            // DeleteToolStripButton
            // 
            this.DeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.cross_48;
            this.DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteToolStripButton.Name = "DeleteToolStripButton";
            this.DeleteToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.DeleteToolStripButton.ToolTipText = "Eliminar la imagen seleccionada";
            this.DeleteToolStripButton.Click += new System.EventHandler(this.DeleteToolStripButton_Click);
            // 
            // NextToolStripButton
            // 
            this.NextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.arrow_right_48;
            this.NextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextToolStripButton.Name = "NextToolStripButton";
            this.NextToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.NextToolStripButton.ToolTipText = "Mover la imagen a la siguiente posición";
            this.NextToolStripButton.Click += new System.EventHandler(this.NextToolStripButton_Click);
            // 
            // PreviousToolStripButton
            // 
            this.PreviousToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PreviousToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.arrow_left_green_48;
            this.PreviousToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviousToolStripButton.Name = "PreviousToolStripButton";
            this.PreviousToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.PreviousToolStripButton.ToolTipText = "Mover la imagen a la posición anterior";
            this.PreviousToolStripButton.Click += new System.EventHandler(this.PreviousToolStripButton_Click);
            // 
            // CancelarToolStripButton
            // 
            this.CancelarToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CancelarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CancelarToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.cancel_48;
            this.CancelarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelarToolStripButton.Name = "CancelarToolStripButton";
            this.CancelarToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.CancelarToolStripButton.Text = "Cancelar";
            this.CancelarToolStripButton.Click += new System.EventHandler(this.CancelarToolStripButton_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProcesoToolStripStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 376);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(755, 21);
            this.MainStatusStrip.TabIndex = 10;
            // 
            // ProcesoToolStripStatusLabel
            // 
            this.ProcesoToolStripStatusLabel.Name = "ProcesoToolStripStatusLabel";
            this.ProcesoToolStripStatusLabel.Size = new System.Drawing.Size(49, 16);
            this.ProcesoToolStripStatusLabel.Text = "Proceso";
            // 
            // FormCorreccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 476);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.MenuToolStrip);
            this.Controls.Add(this.FormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCorreccion";
            this.Text = "Corrección";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormCorreccion_Load);
            this.MenuToolStrip.ResumeLayout(false);
            this.MenuToolStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.FormMenuStrip.ResumeLayout(false);
            this.FormMenuStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip MenuToolStrip;
        internal System.Windows.Forms.ToolStripButton TransferirToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripButton AdquirirToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripButton InsertFolderToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton InsertFileToolStripButton;
        internal System.Windows.Forms.ToolStripButton DeleteToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton NextToolStripButton;
        internal System.Windows.Forms.ToolStripButton PreviousToolStripButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ListView NewImagesListView;
        internal System.Windows.Forms.ListView OldImagesListView;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.MenuStrip FormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SeleccionarOrigenToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CancelarToolStripButton;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ProcesoToolStripStatusLabel;
    }
}