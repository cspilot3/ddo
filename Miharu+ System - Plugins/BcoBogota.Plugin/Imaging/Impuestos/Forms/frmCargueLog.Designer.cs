namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
{
    partial class frmCargueLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            Miharu.Desktop.Controls.DesktopTextBox.Rango rango3 = new Miharu.Desktop.Controls.DesktopTextBox.Rango();
            this.gbCargue = new System.Windows.Forms.GroupBox();
            this.chkEncabezado = new System.Windows.Forms.CheckBox();
            this.OpcionesSeparadorGroupBox = new System.Windows.Forms.GroupBox();
            this.PuntoComaRadioButton = new System.Windows.Forms.RadioButton();
            this.TabuladorRadioButton = new System.Windows.Forms.RadioButton();
            this.ComaRadioButton = new System.Windows.Forms.RadioButton();
            this.DesktopComboBoxControlTiposLog = new Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl();
            this.Label2 = new System.Windows.Forms.Label();
            this.ProgresoGroupBox = new System.Windows.Forms.GroupBox();
            this.ProcesadosLabel = new System.Windows.Forms.Label();
            this.ProcesadosTituloLabel = new System.Windows.Forms.Label();
            this.TotalRegistrosLabel = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.CargandoPictureBox = new System.Windows.Forms.PictureBox();
            this.CargarButton = new System.Windows.Forms.Button();
            this.DatosCargadosDesktopDataGridView = new Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl();
            this.ArchivoDesktopTextBox = new Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl();
            this.SelecionarArchivoLabel = new System.Windows.Forms.Label();
            this.BuscarArchivoButton = new System.Windows.Forms.Button();
            this.dtpFechaProceso = new System.Windows.Forms.DateTimePicker();
            this.lblFechaProceso = new System.Windows.Forms.Label();
            this.TiempoLabel = new System.Windows.Forms.Label();
            this.CargueProgressBar = new System.Windows.Forms.ProgressBar();
            this.ArchivoOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CerrarButton = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.CargueBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.dtpFechaRecaudo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.gbCargue.SuspendLayout();
            this.OpcionesSeparadorGroupBox.SuspendLayout();
            this.ProgresoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CargandoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosCargadosDesktopDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gbCargue
            // 
            this.gbCargue.Controls.Add(this.dtpFechaRecaudo);
            this.gbCargue.Controls.Add(this.label3);
            this.gbCargue.Controls.Add(this.chkEncabezado);
            this.gbCargue.Controls.Add(this.OpcionesSeparadorGroupBox);
            this.gbCargue.Controls.Add(this.DesktopComboBoxControlTiposLog);
            this.gbCargue.Controls.Add(this.Label2);
            this.gbCargue.Controls.Add(this.ProgresoGroupBox);
            this.gbCargue.Controls.Add(this.CargandoPictureBox);
            this.gbCargue.Controls.Add(this.CargarButton);
            this.gbCargue.Controls.Add(this.DatosCargadosDesktopDataGridView);
            this.gbCargue.Controls.Add(this.ArchivoDesktopTextBox);
            this.gbCargue.Controls.Add(this.SelecionarArchivoLabel);
            this.gbCargue.Controls.Add(this.BuscarArchivoButton);
            this.gbCargue.Controls.Add(this.dtpFechaProceso);
            this.gbCargue.Controls.Add(this.lblFechaProceso);
            this.gbCargue.Location = new System.Drawing.Point(12, 12);
            this.gbCargue.Name = "gbCargue";
            this.gbCargue.Size = new System.Drawing.Size(503, 506);
            this.gbCargue.TabIndex = 52;
            this.gbCargue.TabStop = false;
            this.gbCargue.Text = "Cargue";
            // 
            // chkEncabezado
            // 
            this.chkEncabezado.AutoSize = true;
            this.chkEncabezado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEncabezado.Location = new System.Drawing.Point(200, 164);
            this.chkEncabezado.Name = "chkEncabezado";
            this.chkEncabezado.Size = new System.Drawing.Size(140, 17);
            this.chkEncabezado.TabIndex = 59;
            this.chkEncabezado.Text = "Maneja encabezado";
            this.chkEncabezado.UseVisualStyleBackColor = true;
            this.chkEncabezado.Visible = false;
            // 
            // OpcionesSeparadorGroupBox
            // 
            this.OpcionesSeparadorGroupBox.Controls.Add(this.PuntoComaRadioButton);
            this.OpcionesSeparadorGroupBox.Controls.Add(this.TabuladorRadioButton);
            this.OpcionesSeparadorGroupBox.Controls.Add(this.ComaRadioButton);
            this.OpcionesSeparadorGroupBox.Enabled = false;
            this.OpcionesSeparadorGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpcionesSeparadorGroupBox.Location = new System.Drawing.Point(6, 164);
            this.OpcionesSeparadorGroupBox.Name = "OpcionesSeparadorGroupBox";
            this.OpcionesSeparadorGroupBox.Size = new System.Drawing.Size(180, 91);
            this.OpcionesSeparadorGroupBox.TabIndex = 58;
            this.OpcionesSeparadorGroupBox.TabStop = false;
            this.OpcionesSeparadorGroupBox.Text = "Opciones de Separador";
            // 
            // PuntoComaRadioButton
            // 
            this.PuntoComaRadioButton.AutoSize = true;
            this.PuntoComaRadioButton.Location = new System.Drawing.Point(6, 67);
            this.PuntoComaRadioButton.Name = "PuntoComaRadioButton";
            this.PuntoComaRadioButton.Size = new System.Drawing.Size(119, 17);
            this.PuntoComaRadioButton.TabIndex = 2;
            this.PuntoComaRadioButton.Text = "Punto y Coma (;)";
            this.PuntoComaRadioButton.UseVisualStyleBackColor = true;
            // 
            // TabuladorRadioButton
            // 
            this.TabuladorRadioButton.AutoSize = true;
            this.TabuladorRadioButton.Location = new System.Drawing.Point(7, 44);
            this.TabuladorRadioButton.Name = "TabuladorRadioButton";
            this.TabuladorRadioButton.Size = new System.Drawing.Size(114, 17);
            this.TabuladorRadioButton.TabIndex = 1;
            this.TabuladorRadioButton.Text = "Tabulador (     )";
            this.TabuladorRadioButton.UseVisualStyleBackColor = true;
            // 
            // ComaRadioButton
            // 
            this.ComaRadioButton.AutoSize = true;
            this.ComaRadioButton.Checked = true;
            this.ComaRadioButton.Location = new System.Drawing.Point(7, 21);
            this.ComaRadioButton.Name = "ComaRadioButton";
            this.ComaRadioButton.Size = new System.Drawing.Size(72, 17);
            this.ComaRadioButton.TabIndex = 0;
            this.ComaRadioButton.TabStop = true;
            this.ComaRadioButton.Text = "Coma (,)";
            this.ComaRadioButton.UseVisualStyleBackColor = true;
            // 
            // DesktopComboBoxControlTiposLog
            // 
            this.DesktopComboBoxControlTiposLog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DesktopComboBoxControlTiposLog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DesktopComboBoxControlTiposLog.DisabledEnter = false;
            this.DesktopComboBoxControlTiposLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DesktopComboBoxControlTiposLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DesktopComboBoxControlTiposLog.FormattingEnabled = true;
            this.DesktopComboBoxControlTiposLog.Location = new System.Drawing.Point(213, 56);
            this.DesktopComboBoxControlTiposLog.Name = "DesktopComboBoxControlTiposLog";
            this.DesktopComboBoxControlTiposLog.Size = new System.Drawing.Size(270, 21);
            this.DesktopComboBoxControlTiposLog.TabIndex = 57;
            this.DesktopComboBoxControlTiposLog.SelectedIndexChanged += new System.EventHandler(this.DesktopComboBoxControlTiposLog_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(8, 60);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(61, 13);
            this.Label2.TabIndex = 56;
            this.Label2.Text = "Tipo Log:";
            // 
            // ProgresoGroupBox
            // 
            this.ProgresoGroupBox.Controls.Add(this.ProcesadosLabel);
            this.ProgresoGroupBox.Controls.Add(this.ProcesadosTituloLabel);
            this.ProgresoGroupBox.Controls.Add(this.TotalRegistrosLabel);
            this.ProgresoGroupBox.Controls.Add(this.Label1);
            this.ProgresoGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgresoGroupBox.Location = new System.Drawing.Point(195, 187);
            this.ProgresoGroupBox.Name = "ProgresoGroupBox";
            this.ProgresoGroupBox.Size = new System.Drawing.Size(175, 68);
            this.ProgresoGroupBox.TabIndex = 55;
            this.ProgresoGroupBox.TabStop = false;
            this.ProgresoGroupBox.Text = "Progreso";
            // 
            // ProcesadosLabel
            // 
            this.ProcesadosLabel.AutoSize = true;
            this.ProcesadosLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.ProcesadosLabel.Location = new System.Drawing.Point(107, 44);
            this.ProcesadosLabel.Name = "ProcesadosLabel";
            this.ProcesadosLabel.Size = new System.Drawing.Size(14, 13);
            this.ProcesadosLabel.TabIndex = 3;
            this.ProcesadosLabel.Text = "0";
            // 
            // ProcesadosTituloLabel
            // 
            this.ProcesadosTituloLabel.AutoSize = true;
            this.ProcesadosTituloLabel.Location = new System.Drawing.Point(7, 44);
            this.ProcesadosTituloLabel.Name = "ProcesadosTituloLabel";
            this.ProcesadosTituloLabel.Size = new System.Drawing.Size(81, 13);
            this.ProcesadosTituloLabel.TabIndex = 2;
            this.ProcesadosTituloLabel.Text = "Procesados: ";
            // 
            // TotalRegistrosLabel
            // 
            this.TotalRegistrosLabel.AutoSize = true;
            this.TotalRegistrosLabel.ForeColor = System.Drawing.Color.Red;
            this.TotalRegistrosLabel.Location = new System.Drawing.Point(107, 21);
            this.TotalRegistrosLabel.Name = "TotalRegistrosLabel";
            this.TotalRegistrosLabel.Size = new System.Drawing.Size(14, 13);
            this.TotalRegistrosLabel.TabIndex = 1;
            this.TotalRegistrosLabel.Text = "0";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(68, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Registros: ";
            // 
            // CargandoPictureBox
            // 
            this.CargandoPictureBox.Image = global::BcoBogota.Plugin.Properties.Resources.ajax_loader;
            this.CargandoPictureBox.Location = new System.Drawing.Point(165, 322);
            this.CargandoPictureBox.Name = "CargandoPictureBox";
            this.CargandoPictureBox.Size = new System.Drawing.Size(125, 112);
            this.CargandoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CargandoPictureBox.TabIndex = 54;
            this.CargandoPictureBox.TabStop = false;
            this.CargandoPictureBox.Visible = false;
            // 
            // CargarButton
            // 
            this.CargarButton.AutoSize = true;
            this.CargarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CargarButton.Image = global::BcoBogota.Plugin.Properties.Resources.BtnCargar;
            this.CargarButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CargarButton.Location = new System.Drawing.Point(374, 193);
            this.CargarButton.Name = "CargarButton";
            this.CargarButton.Size = new System.Drawing.Size(101, 62);
            this.CargarButton.TabIndex = 52;
            this.CargarButton.Text = "&Cargar Archivo";
            this.CargarButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CargarButton.UseVisualStyleBackColor = true;
            this.CargarButton.Click += new System.EventHandler(this.CargarButton_Click);
            // 
            // DatosCargadosDesktopDataGridView
            // 
            this.DatosCargadosDesktopDataGridView.AllowUserToAddRows = false;
            this.DatosCargadosDesktopDataGridView.AllowUserToDeleteRows = false;
            this.DatosCargadosDesktopDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DatosCargadosDesktopDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DatosCargadosDesktopDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DatosCargadosDesktopDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DatosCargadosDesktopDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.DatosCargadosDesktopDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.DatosCargadosDesktopDataGridView.Location = new System.Drawing.Point(6, 274);
            this.DatosCargadosDesktopDataGridView.MultiSelect = false;
            this.DatosCargadosDesktopDataGridView.Name = "DatosCargadosDesktopDataGridView";
            this.DatosCargadosDesktopDataGridView.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DatosCargadosDesktopDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DatosCargadosDesktopDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DatosCargadosDesktopDataGridView.Size = new System.Drawing.Size(471, 219);
            this.DatosCargadosDesktopDataGridView.TabIndex = 53;
            // 
            // ArchivoDesktopTextBox
            // 
            this.ArchivoDesktopTextBox._Obligatorio = false;
            this.ArchivoDesktopTextBox._PermitePegar = false;
            this.ArchivoDesktopTextBox.BackColor = System.Drawing.Color.LightGray;
            this.ArchivoDesktopTextBox.Cantidad_Decimales = ((short)(0));
            this.ArchivoDesktopTextBox.Caracter_Decimal = '\0';
            this.ArchivoDesktopTextBox.DateFormat = null;
            this.ArchivoDesktopTextBox.DisabledEnter = false;
            this.ArchivoDesktopTextBox.DisabledTab = false;
            this.ArchivoDesktopTextBox.EnabledShortCuts = false;
            this.ArchivoDesktopTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.ArchivoDesktopTextBox.FocusOut = System.Drawing.Color.White;
            this.ArchivoDesktopTextBox.Location = new System.Drawing.Point(212, 28);
            this.ArchivoDesktopTextBox.MaskedTextBox_Property = "";
            this.ArchivoDesktopTextBox.MaximumLength = ((short)(0));
            this.ArchivoDesktopTextBox.MinimumLength = ((short)(0));
            this.ArchivoDesktopTextBox.Name = "ArchivoDesktopTextBox";
            this.ArchivoDesktopTextBox.Obligatorio = false;
            this.ArchivoDesktopTextBox.permitePegar = false;
            rango3.MaxValue = 2147483647D;
            rango3.MinValue = 0D;
            this.ArchivoDesktopTextBox.Rango = rango3;
            this.ArchivoDesktopTextBox.ReadOnly = true;
            this.ArchivoDesktopTextBox.Size = new System.Drawing.Size(190, 20);
            this.ArchivoDesktopTextBox.TabIndex = 50;
            this.ArchivoDesktopTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal;
            this.ArchivoDesktopTextBox.Usa_Decimales = false;
            this.ArchivoDesktopTextBox.Validos_Cantidad_Puntos = false;
            this.ArchivoDesktopTextBox.Click += new System.EventHandler(this.ArchivoDesktopTextBox_Click);
            // 
            // SelecionarArchivoLabel
            // 
            this.SelecionarArchivoLabel.AutoSize = true;
            this.SelecionarArchivoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelecionarArchivoLabel.Location = new System.Drawing.Point(8, 32);
            this.SelecionarArchivoLabel.Name = "SelecionarArchivoLabel";
            this.SelecionarArchivoLabel.Size = new System.Drawing.Size(185, 13);
            this.SelecionarArchivoLabel.TabIndex = 49;
            this.SelecionarArchivoLabel.Text = "Seleccionar archivo de cargue:";
            // 
            // BuscarArchivoButton
            // 
            this.BuscarArchivoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuscarArchivoButton.Image = global::BcoBogota.Plugin.Properties.Resources.btnBuscar;
            this.BuscarArchivoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BuscarArchivoButton.Location = new System.Drawing.Point(408, 27);
            this.BuscarArchivoButton.Name = "BuscarArchivoButton";
            this.BuscarArchivoButton.Size = new System.Drawing.Size(75, 23);
            this.BuscarArchivoButton.TabIndex = 51;
            this.BuscarArchivoButton.Text = "&Buscar";
            this.BuscarArchivoButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BuscarArchivoButton.UseVisualStyleBackColor = true;
            this.BuscarArchivoButton.Click += new System.EventHandler(this.BuscarArchivoButton_Click);
            // 
            // dtpFechaProceso
            // 
            this.dtpFechaProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaProceso.Location = new System.Drawing.Point(214, 87);
            this.dtpFechaProceso.Name = "dtpFechaProceso";
            this.dtpFechaProceso.Size = new System.Drawing.Size(270, 20);
            this.dtpFechaProceso.TabIndex = 0;
            // 
            // lblFechaProceso
            // 
            this.lblFechaProceso.AutoSize = true;
            this.lblFechaProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaProceso.Location = new System.Drawing.Point(10, 87);
            this.lblFechaProceso.Name = "lblFechaProceso";
            this.lblFechaProceso.Size = new System.Drawing.Size(96, 13);
            this.lblFechaProceso.TabIndex = 47;
            this.lblFechaProceso.Text = "Fecha Proceso:";
            // 
            // TiempoLabel
            // 
            this.TiempoLabel.AutoSize = true;
            this.TiempoLabel.ForeColor = System.Drawing.Color.Maroon;
            this.TiempoLabel.Location = new System.Drawing.Point(326, 534);
            this.TiempoLabel.Name = "TiempoLabel";
            this.TiempoLabel.Size = new System.Drawing.Size(49, 13);
            this.TiempoLabel.TabIndex = 55;
            this.TiempoLabel.Text = "00:00:00";
            this.TiempoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CargueProgressBar
            // 
            this.CargueProgressBar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CargueProgressBar.Location = new System.Drawing.Point(24, 526);
            this.CargueProgressBar.Name = "CargueProgressBar";
            this.CargueProgressBar.Size = new System.Drawing.Size(290, 28);
            this.CargueProgressBar.TabIndex = 54;
            // 
            // ArchivoOpenFileDialog
            // 
            this.ArchivoOpenFileDialog.Filter = "Archivos de Cargue (*.*;*.txt; *.xls;*.xlsx; *.csv;*.dat)|*.txt;*.xls;*.xlsx;*.cs" +
    "v;*.dat;*.*";
            this.ArchivoOpenFileDialog.InitialDirectory = "\"c:\\\"";
            this.ArchivoOpenFileDialog.Multiselect = true;
            this.ArchivoOpenFileDialog.ReadOnlyChecked = true;
            this.ArchivoOpenFileDialog.RestoreDirectory = true;
            // 
            // CerrarButton
            // 
            this.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CerrarButton.Image = global::BcoBogota.Plugin.Properties.Resources.btnSalir;
            this.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CerrarButton.Location = new System.Drawing.Point(421, 526);
            this.CerrarButton.Name = "CerrarButton";
            this.CerrarButton.Size = new System.Drawing.Size(74, 28);
            this.CerrarButton.TabIndex = 53;
            this.CerrarButton.Text = "&Cerrar";
            this.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CerrarButton.UseVisualStyleBackColor = true;
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // CargueBackgroundWorker
            // 
            this.CargueBackgroundWorker.WorkerReportsProgress = true;
            this.CargueBackgroundWorker.WorkerSupportsCancellation = true;
            this.CargueBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CargueBackgroundWorker_DoWork);
            this.CargueBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CargueBackgroundWorker_ProgressChanged);
            this.CargueBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CargueBackgroundWorker_RunWorkerCompleted);
            // 
            // dtpFechaRecaudo
            // 
            this.dtpFechaRecaudo.Enabled = false;
            this.dtpFechaRecaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaRecaudo.Location = new System.Drawing.Point(214, 120);
            this.dtpFechaRecaudo.Name = "dtpFechaRecaudo";
            this.dtpFechaRecaudo.Size = new System.Drawing.Size(270, 20);
            this.dtpFechaRecaudo.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Fecha Recaudo:";
            // 
            // frmCargueLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 570);
            this.Controls.Add(this.TiempoLabel);
            this.Controls.Add(this.CargueProgressBar);
            this.Controls.Add(this.CerrarButton);
            this.Controls.Add(this.gbCargue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCargueLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargue log - Impuestos";
            this.Load += new System.EventHandler(this.frmCargueLog_Load);
            this.gbCargue.ResumeLayout(false);
            this.gbCargue.PerformLayout();
            this.OpcionesSeparadorGroupBox.ResumeLayout(false);
            this.OpcionesSeparadorGroupBox.PerformLayout();
            this.ProgresoGroupBox.ResumeLayout(false);
            this.ProgresoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CargandoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosCargadosDesktopDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbCargue;
        internal System.Windows.Forms.DateTimePicker dtpFechaProceso;
        internal System.Windows.Forms.Label lblFechaProceso;
        internal Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl ArchivoDesktopTextBox;
        internal System.Windows.Forms.Label SelecionarArchivoLabel;
        internal System.Windows.Forms.Button BuscarArchivoButton;
        internal System.Windows.Forms.Button CargarButton;
        internal Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl DatosCargadosDesktopDataGridView;
        internal System.Windows.Forms.PictureBox CargandoPictureBox;
        internal System.Windows.Forms.Label TiempoLabel;
        internal System.Windows.Forms.ProgressBar CargueProgressBar;
        internal System.Windows.Forms.Button CerrarButton;
        internal System.Windows.Forms.GroupBox ProgresoGroupBox;
        internal System.Windows.Forms.Label ProcesadosLabel;
        internal System.Windows.Forms.Label ProcesadosTituloLabel;
        internal System.Windows.Forms.Label TotalRegistrosLabel;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.OpenFileDialog ArchivoOpenFileDialog;
        internal Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl DesktopComboBoxControlTiposLog;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Timer Timer1;
        internal System.ComponentModel.BackgroundWorker CargueBackgroundWorker;
        internal System.Windows.Forms.CheckBox chkEncabezado;
        internal System.Windows.Forms.GroupBox OpcionesSeparadorGroupBox;
        internal System.Windows.Forms.RadioButton PuntoComaRadioButton;
        internal System.Windows.Forms.RadioButton TabuladorRadioButton;
        internal System.Windows.Forms.RadioButton ComaRadioButton;
        internal System.Windows.Forms.DateTimePicker dtpFechaRecaudo;
        internal System.Windows.Forms.Label label3;

    }
}