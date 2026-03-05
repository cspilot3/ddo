namespace Banagrario.Scanning.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.FormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SeleccionarOrigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AyudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewProcessToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TransmitirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ReprocesosToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AdquirirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.InsertFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.InsertFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.NextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PreviousToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.VistaToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.IconosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgresoToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.AccionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ContadorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CancelarToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ImagesListView = new System.Windows.Forms.ListView();
            this.FormMenuStrip.SuspendLayout();
            this.MenuToolStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormMenuStrip
            // 
            this.FormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ArchivoToolStripMenuItem,
            this.AyudaToolStripMenuItem});
            this.FormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.FormMenuStrip.Name = "FormMenuStrip";
            this.FormMenuStrip.Size = new System.Drawing.Size(900, 24);
            this.FormMenuStrip.TabIndex = 1;
            // 
            // ArchivoToolStripMenuItem
            // 
            this.ArchivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurarToolStripMenuItem,
            this.SeleccionarOrigenToolStripMenuItem,
            this.toolStripSeparator6,
            this.CerrarToolStripMenuItem});
            this.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem";
            this.ArchivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ArchivoToolStripMenuItem.Text = "&Archivo";
            // 
            // configurarToolStripMenuItem
            // 
            this.configurarToolStripMenuItem.Name = "configurarToolStripMenuItem";
            this.configurarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configurarToolStripMenuItem.Text = "Configurar...";
            this.configurarToolStripMenuItem.Click += new System.EventHandler(this.configurarToolStripMenuItem_Click);
            // 
            // SeleccionarOrigenToolStripMenuItem
            // 
            this.SeleccionarOrigenToolStripMenuItem.Name = "SeleccionarOrigenToolStripMenuItem";
            this.SeleccionarOrigenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SeleccionarOrigenToolStripMenuItem.Text = "Seleccionar origen...";
            this.SeleccionarOrigenToolStripMenuItem.Click += new System.EventHandler(this.SeleccionarOrigenToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
            // 
            // CerrarToolStripMenuItem
            // 
            this.CerrarToolStripMenuItem.Name = "CerrarToolStripMenuItem";
            this.CerrarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CerrarToolStripMenuItem.Text = "Cerrar";
            this.CerrarToolStripMenuItem.Click += new System.EventHandler(this.CerrarToolStripMenuItem_Click);
            // 
            // AyudaToolStripMenuItem
            // 
            this.AyudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem";
            this.AyudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.AyudaToolStripMenuItem.Text = "A&yuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // MenuToolStrip
            // 
            this.MenuToolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.MenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProcessToolStripButton,
            this.ToolStripSeparator1,
            this.TransmitirToolStripButton,
            this.ReprocesosToolStripButton,
            this.ToolStripSeparator2,
            this.AdquirirToolStripButton,
            this.toolStripSeparator7,
            this.ToolStripSeparator3,
            this.InsertFolderToolStripButton,
            this.InsertFileToolStripButton,
            this.DeleteToolStripButton,
            this.ToolStripSeparator4,
            this.NextToolStripButton,
            this.PreviousToolStripButton,
            this.ToolStripSeparator5,
            this.VistaToolStripDropDownButton});
            this.MenuToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MenuToolStrip.Name = "MenuToolStrip";
            this.MenuToolStrip.Size = new System.Drawing.Size(900, 55);
            this.MenuToolStrip.TabIndex = 3;
            this.MenuToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuToolStrip_ItemClicked);
            // 
            // NewProcessToolStripButton
            // 
            this.NewProcessToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewProcessToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.database_add_48;
            this.NewProcessToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewProcessToolStripButton.Name = "NewProcessToolStripButton";
            this.NewProcessToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.NewProcessToolStripButton.ToolTipText = "Nuevo proceso";
            this.NewProcessToolStripButton.Click += new System.EventHandler(this.NewProcessToolStripButton_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // TransmitirToolStripButton
            // 
            this.TransmitirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TransmitirToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.floppy_disk_48;
            this.TransmitirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TransmitirToolStripButton.Name = "TransmitirToolStripButton";
            this.TransmitirToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.TransmitirToolStripButton.ToolTipText = "Transmitir";
            this.TransmitirToolStripButton.Click += new System.EventHandler(this.TransmitirToolStripButton_Click);
            // 
            // ReprocesosToolStripButton
            // 
            this.ReprocesosToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ReprocesosToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.spanner_48;
            this.ReprocesosToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReprocesosToolStripButton.Name = "ReprocesosToolStripButton";
            this.ReprocesosToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.ReprocesosToolStripButton.ToolTipText = "Reprocesos";
            this.ReprocesosToolStripButton.Click += new System.EventHandler(this.ReprocesosToolStripButton_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // AdquirirToolStripButton
            // 
            this.AdquirirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AdquirirToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.paper_content_48;
            this.AdquirirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AdquirirToolStripButton.Name = "AdquirirToolStripButton";
            this.AdquirirToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.AdquirirToolStripButton.ToolTipText = "Insertar imágenes desde el escaner";
            this.AdquirirToolStripButton.Visible = false;
            this.AdquirirToolStripButton.Click += new System.EventHandler(this.AdquirirToolStripButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 55);
            this.toolStripSeparator7.Visible = false;
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 55);
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
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            // 
            // NextToolStripButton
            // 
            this.NextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextToolStripButton.Image = global::Banagrario.Scanning.Properties.Resources.arrow_right_48;
            this.NextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextToolStripButton.Name = "NextToolStripButton";
            this.NextToolStripButton.Size = new System.Drawing.Size(52, 52);
            this.NextToolStripButton.ToolTipText = "Mover la imagen a la posición siguiente";
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
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 55);
            // 
            // VistaToolStripDropDownButton
            // 
            this.VistaToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.VistaToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IconosToolStripMenuItem,
            this.ListaToolStripMenuItem});
            this.VistaToolStripDropDownButton.Image = global::Banagrario.Scanning.Properties.Resources.table_48;
            this.VistaToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VistaToolStripDropDownButton.Name = "VistaToolStripDropDownButton";
            this.VistaToolStripDropDownButton.Size = new System.Drawing.Size(61, 52);
            this.VistaToolStripDropDownButton.ToolTipText = "Modo de visualización de las imágenes";
            // 
            // IconosToolStripMenuItem
            // 
            this.IconosToolStripMenuItem.Checked = true;
            this.IconosToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IconosToolStripMenuItem.Image = global::Banagrario.Scanning.Properties.Resources.table_48;
            this.IconosToolStripMenuItem.Name = "IconosToolStripMenuItem";
            this.IconosToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.IconosToolStripMenuItem.Text = "Iconos";
            this.IconosToolStripMenuItem.Click += new System.EventHandler(this.IconosToolStripMenuItem_Click);
            // 
            // ListaToolStripMenuItem
            // 
            this.ListaToolStripMenuItem.Image = global::Banagrario.Scanning.Properties.Resources.navigate_48;
            this.ListaToolStripMenuItem.Name = "ListaToolStripMenuItem";
            this.ListaToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.ListaToolStripMenuItem.Text = "Lista";
            this.ListaToolStripMenuItem.Click += new System.EventHandler(this.ListaToolStripMenuItem_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgresoToolStripProgressBar,
            this.AccionToolStripStatusLabel,
            this.ContadorToolStripStatusLabel,
            this.CancelarToolStripStatusLabel,
            this.ToolStripStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 384);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(900, 24);
            this.MainStatusStrip.TabIndex = 4;
            this.MainStatusStrip.Text = "StatusStrip1";
            // 
            // ProgresoToolStripProgressBar
            // 
            this.ProgresoToolStripProgressBar.Name = "ProgresoToolStripProgressBar";
            this.ProgresoToolStripProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // AccionToolStripStatusLabel
            // 
            this.AccionToolStripStatusLabel.Name = "AccionToolStripStatusLabel";
            this.AccionToolStripStatusLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // ContadorToolStripStatusLabel
            // 
            this.ContadorToolStripStatusLabel.Name = "ContadorToolStripStatusLabel";
            this.ContadorToolStripStatusLabel.Size = new System.Drawing.Size(23, 19);
            this.ContadorToolStripStatusLabel.Text = "0%";
            this.ContadorToolStripStatusLabel.Visible = false;
            // 
            // CancelarToolStripStatusLabel
            // 
            this.CancelarToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.CancelarToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.CancelarToolStripStatusLabel.Name = "CancelarToolStripStatusLabel";
            this.CancelarToolStripStatusLabel.Size = new System.Drawing.Size(57, 19);
            this.CancelarToolStripStatusLabel.Text = "Cancelar";
            this.CancelarToolStripStatusLabel.Visible = false;
            this.CancelarToolStripStatusLabel.Click += new System.EventHandler(this.CancelarToolStripStatusLabel_Click);
            // 
            // ToolStripStatusLabel
            // 
            this.ToolStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.ToolStripStatusLabel.Size = new System.Drawing.Size(248, 19);
            this.ToolStripStatusLabel.Text = "Fecha: [yyyymmdd] - Oficina: [Bogotá]";
            // 
            // ImagesListView
            // 
            this.ImagesListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.ImagesListView.BackColor = System.Drawing.Color.Silver;
            this.ImagesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagesListView.HideSelection = false;
            this.ImagesListView.Location = new System.Drawing.Point(0, 79);
            this.ImagesListView.MultiSelect = false;
            this.ImagesListView.Name = "ImagesListView";
            this.ImagesListView.Size = new System.Drawing.Size(900, 305);
            this.ImagesListView.TabIndex = 5;
            this.ImagesListView.UseCompatibleStateImageBehavior = false;
            this.ImagesListView.SelectedIndexChanged += new System.EventHandler(this.ImagesListView_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 408);
            this.Controls.Add(this.ImagesListView);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MenuToolStrip);
            this.Controls.Add(this.FormMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Banco Agrario Oficinas Tipo B";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormMenuStrip.ResumeLayout(false);
            this.FormMenuStrip.PerformLayout();
            this.MenuToolStrip.ResumeLayout(false);
            this.MenuToolStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.MenuStrip FormMenuStrip;
        internal System.Windows.Forms.ToolStripMenuItem ArchivoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AyudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        internal System.Windows.Forms.ToolStrip MenuToolStrip;
        internal System.Windows.Forms.ToolStripButton NewProcessToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton TransmitirToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton InsertFolderToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton InsertFileToolStripButton;
        internal System.Windows.Forms.ToolStripButton DeleteToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton NextToolStripButton;
        internal System.Windows.Forms.ToolStripButton PreviousToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripDropDownButton VistaToolStripDropDownButton;
        internal System.Windows.Forms.ToolStripMenuItem IconosToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ListaToolStripMenuItem;
        internal System.Windows.Forms.StatusStrip MainStatusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel;
        internal System.Windows.Forms.ListView ImagesListView;
        private System.Windows.Forms.ToolStripMenuItem SeleccionarOrigenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton AdquirirToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem configurarToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ReprocesosToolStripButton;
        private System.Windows.Forms.ToolStripProgressBar ProgresoToolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel AccionToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel CancelarToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel ContadorToolStripStatusLabel;
    }
}

