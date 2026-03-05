namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    partial class frmRelacion_CuentasDispersadas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDispersadasCuentas = new System.Windows.Forms.DataGridView();
            this.chkSeleccionarTodos = new System.Windows.Forms.CheckBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnEdiarTodos = new System.Windows.Forms.Button();
            this.btnMostrarDatos = new System.Windows.Forms.Button();
            this.dtpFechaRecaudo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.id_Cuenta_Dispersada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fecha_Recaudo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Municipio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Municipio_20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Departamento_80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero_Cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha_Dispersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDispersadasCuentas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDispersadasCuentas);
            this.groupBox1.Controls.Add(this.chkSeleccionarTodos);
            this.groupBox1.Controls.Add(this.btnExportar);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnEdiarTodos);
            this.groupBox1.Controls.Add(this.btnMostrarDatos);
            this.groupBox1.Controls.Add(this.dtpFechaRecaudo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(870, 395);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dispersión";
            // 
            // dgvDispersadasCuentas
            // 
            this.dgvDispersadasCuentas.AllowUserToAddRows = false;
            this.dgvDispersadasCuentas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvDispersadasCuentas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDispersadasCuentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDispersadasCuentas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDispersadasCuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDispersadasCuentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_Cuenta_Dispersada,
            this.Seleccionar,
            this.Fecha_Recaudo,
            this.Nombre_Municipio,
            this.Nombre_Departamento,
            this.Municipio_20,
            this.Departamento_80,
            this.Nombre_Cuenta,
            this.Numero_Cuenta,
            this.Estado,
            this.Fecha_Dispersion,
            this.Editar});
            this.dgvDispersadasCuentas.Location = new System.Drawing.Point(12, 93);
            this.dgvDispersadasCuentas.MultiSelect = false;
            this.dgvDispersadasCuentas.Name = "dgvDispersadasCuentas";
            this.dgvDispersadasCuentas.ReadOnly = true;
            this.dgvDispersadasCuentas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvDispersadasCuentas.ShowEditingIcon = false;
            this.dgvDispersadasCuentas.Size = new System.Drawing.Size(844, 237);
            this.dgvDispersadasCuentas.TabIndex = 3;
            this.dgvDispersadasCuentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDispersadasCuentas_CellContentClick);
            // 
            // chkSeleccionarTodos
            // 
            this.chkSeleccionarTodos.AutoSize = true;
            this.chkSeleccionarTodos.Location = new System.Drawing.Point(17, 62);
            this.chkSeleccionarTodos.Name = "chkSeleccionarTodos";
            this.chkSeleccionarTodos.Size = new System.Drawing.Size(169, 17);
            this.chkSeleccionarTodos.TabIndex = 2;
            this.chkSeleccionarTodos.Text = "Seleccionar todos los registros";
            this.chkSeleccionarTodos.UseVisualStyleBackColor = true;
            this.chkSeleccionarTodos.CheckedChanged += new System.EventHandler(this.chkSeleccionarTodos_CheckedChanged);
            // 
            // btnExportar
            // 
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Image = global::BcoItau.Plugin.Properties.Resources.FacturacionMain;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(502, 347);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(82, 28);
            this.btnExportar.TabIndex = 6;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::BcoItau.Plugin.Properties.Resources.btnSalir;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(281, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Cerrar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnEdiarTodos
            // 
            this.btnEdiarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdiarTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdiarTodos.Image = global::BcoItau.Plugin.Properties.Resources.btnGenerarEstructura;
            this.btnEdiarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdiarTodos.Location = new System.Drawing.Point(370, 347);
            this.btnEdiarTodos.Name = "btnEdiarTodos";
            this.btnEdiarTodos.Size = new System.Drawing.Size(115, 28);
            this.btnEdiarTodos.TabIndex = 5;
            this.btnEdiarTodos.Text = "Editar Varios";
            this.btnEdiarTodos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdiarTodos.UseVisualStyleBackColor = true;
            this.btnEdiarTodos.Click += new System.EventHandler(this.btnEdiarTodos_Click);
            // 
            // btnMostrarDatos
            // 
            this.btnMostrarDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrarDatos.Location = new System.Drawing.Point(343, 24);
            this.btnMostrarDatos.Name = "btnMostrarDatos";
            this.btnMostrarDatos.Size = new System.Drawing.Size(123, 23);
            this.btnMostrarDatos.TabIndex = 1;
            this.btnMostrarDatos.Text = "Mostrar Datos";
            this.btnMostrarDatos.UseVisualStyleBackColor = true;
            this.btnMostrarDatos.Click += new System.EventHandler(this.btnMostrarDatos_Click);
            // 
            // dtpFechaRecaudo
            // 
            this.dtpFechaRecaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaRecaudo.Location = new System.Drawing.Point(121, 27);
            this.dtpFechaRecaudo.Name = "dtpFechaRecaudo";
            this.dtpFechaRecaudo.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaRecaudo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Fecha Recaudo:";
            // 
            // id_Cuenta_Dispersada
            // 
            this.id_Cuenta_Dispersada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.id_Cuenta_Dispersada.DataPropertyName = "id_Cuenta_Dispersada";
            this.id_Cuenta_Dispersada.FillWeight = 5F;
            this.id_Cuenta_Dispersada.HeaderText = "id_Cuenta_Dispersada";
            this.id_Cuenta_Dispersada.Name = "id_Cuenta_Dispersada";
            this.id_Cuenta_Dispersada.ReadOnly = true;
            this.id_Cuenta_Dispersada.Visible = false;
            this.id_Cuenta_Dispersada.Width = 139;
            // 
            // Seleccionar
            // 
            this.Seleccionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Seleccionar.FalseValue = "0";
            this.Seleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Seleccionar.HeaderText = "Seleccionar";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.ReadOnly = true;
            this.Seleccionar.TrueValue = "1";
            this.Seleccionar.Width = 69;
            // 
            // Fecha_Recaudo
            // 
            this.Fecha_Recaudo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Fecha_Recaudo.DataPropertyName = "Fecha_Recaudo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Fecha_Recaudo.DefaultCellStyle = dataGridViewCellStyle3;
            this.Fecha_Recaudo.HeaderText = "Fecha Recaudo";
            this.Fecha_Recaudo.Name = "Fecha_Recaudo";
            this.Fecha_Recaudo.ReadOnly = true;
            // 
            // Nombre_Municipio
            // 
            this.Nombre_Municipio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Municipio.DataPropertyName = "Nombre_Municipio";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Nombre_Municipio.DefaultCellStyle = dataGridViewCellStyle4;
            this.Nombre_Municipio.HeaderText = "Nombre Municipio";
            this.Nombre_Municipio.Name = "Nombre_Municipio";
            this.Nombre_Municipio.ReadOnly = true;
            this.Nombre_Municipio.Width = 107;
            // 
            // Nombre_Departamento
            // 
            this.Nombre_Departamento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Departamento.DataPropertyName = "Nombre_Departamento";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Nombre_Departamento.DefaultCellStyle = dataGridViewCellStyle5;
            this.Nombre_Departamento.HeaderText = "Nombre Departamento";
            this.Nombre_Departamento.Name = "Nombre_Departamento";
            this.Nombre_Departamento.ReadOnly = true;
            this.Nombre_Departamento.Width = 127;
            // 
            // Municipio_20
            // 
            this.Municipio_20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Municipio_20.DataPropertyName = "Municipio_20";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Municipio_20.DefaultCellStyle = dataGridViewCellStyle6;
            this.Municipio_20.HeaderText = "Municipio 20%";
            this.Municipio_20.Name = "Municipio_20";
            this.Municipio_20.ReadOnly = true;
            this.Municipio_20.Width = 92;
            // 
            // Departamento_80
            // 
            this.Departamento_80.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Departamento_80.DataPropertyName = "Departamento_80";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Departamento_80.DefaultCellStyle = dataGridViewCellStyle7;
            this.Departamento_80.HeaderText = "Departamento 80%";
            this.Departamento_80.Name = "Departamento_80";
            this.Departamento_80.ReadOnly = true;
            this.Departamento_80.Width = 112;
            // 
            // Nombre_Cuenta
            // 
            this.Nombre_Cuenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Cuenta.DataPropertyName = "Nombre_Cuenta";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Nombre_Cuenta.DefaultCellStyle = dataGridViewCellStyle8;
            this.Nombre_Cuenta.HeaderText = "Nombre Cuenta";
            this.Nombre_Cuenta.Name = "Nombre_Cuenta";
            this.Nombre_Cuenta.ReadOnly = true;
            this.Nombre_Cuenta.Width = 97;
            // 
            // Numero_Cuenta
            // 
            this.Numero_Cuenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Numero_Cuenta.DataPropertyName = "Numero_Cuenta";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Numero_Cuenta.DefaultCellStyle = dataGridViewCellStyle9;
            this.Numero_Cuenta.HeaderText = "Numero Cuenta";
            this.Numero_Cuenta.Name = "Numero_Cuenta";
            this.Numero_Cuenta.ReadOnly = true;
            this.Numero_Cuenta.Width = 97;
            // 
            // Estado
            // 
            this.Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Estado.DataPropertyName = "Estado";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Estado.DefaultCellStyle = dataGridViewCellStyle10;
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Estado.Width = 65;
            // 
            // Fecha_Dispersion
            // 
            this.Fecha_Dispersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Fecha_Dispersion.DataPropertyName = "Fecha_Dispersion";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Fecha_Dispersion.DefaultCellStyle = dataGridViewCellStyle11;
            this.Fecha_Dispersion.HeaderText = "Fecha Dispersion";
            this.Fecha_Dispersion.Name = "Fecha_Dispersion";
            this.Fecha_Dispersion.ReadOnly = true;
            this.Fecha_Dispersion.Width = 105;
            // 
            // Editar
            // 
            this.Editar.ActiveLinkColor = System.Drawing.Color.Blue;
            this.Editar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Editar.DataPropertyName = "Editar";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Blue;
            this.Editar.DefaultCellStyle = dataGridViewCellStyle12;
            this.Editar.HeaderText = "Editar";
            this.Editar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Editar.LinkColor = System.Drawing.Color.Blue;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Editar.TrackVisitedState = false;
            this.Editar.VisitedLinkColor = System.Drawing.Color.Blue;
            this.Editar.Width = 40;
            // 
            // frmRelacion_CuentasDispersadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 414);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRelacion_CuentasDispersadas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuentas Dispersadas";
            this.Load += new System.EventHandler(this.frmRelacion_CuentasDispersadas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDispersadasCuentas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMostrarDatos;
        private System.Windows.Forms.DateTimePicker dtpFechaRecaudo;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button btnEdiarTodos;
        internal System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataGridView dgvDispersadasCuentas;
        private System.Windows.Forms.CheckBox chkSeleccionarTodos;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_Cuenta_Dispersada;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Recaudo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Municipio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Municipio_20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Departamento_80;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero_Cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Dispersion;
        private System.Windows.Forms.DataGridViewLinkColumn Editar;
    }
}