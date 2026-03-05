namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    partial class frmRelacionCuentasMunicipio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnActualizarGrid = new System.Windows.Forms.Button();
            this.btnInsertarNuevo = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.CerrarButton = new System.Windows.Forms.Button();
            this.txtFiltrarPor = new System.Windows.Forms.TextBox();
            this.cbFiltrarPor = new System.Windows.Forms.ComboBox();
            this.lblFiltrarPor = new System.Windows.Forms.Label();
            this.dgvCuentasMunicipios = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo_Cuenta_Corriente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No_Cuenta_BcoBogota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo_Cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero_Cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo_Compensacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Municipio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.tooltipGeneral = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentasMunicipios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnActualizarGrid);
            this.groupBox1.Controls.Add(this.btnInsertarNuevo);
            this.groupBox1.Controls.Add(this.btnExportar);
            this.groupBox1.Controls.Add(this.CerrarButton);
            this.groupBox1.Controls.Add(this.txtFiltrarPor);
            this.groupBox1.Controls.Add(this.cbFiltrarPor);
            this.groupBox1.Controls.Add(this.lblFiltrarPor);
            this.groupBox1.Controls.Add(this.dgvCuentasMunicipios);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 385);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración y Busqueda";
            // 
            // btnActualizarGrid
            // 
            this.btnActualizarGrid.BackColor = System.Drawing.SystemColors.Control;
            this.btnActualizarGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnActualizarGrid.FlatAppearance.BorderSize = 0;
            this.btnActualizarGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarGrid.Image = global::BcoItau.Plugin.Properties.Resources.refresh_grid;
            this.btnActualizarGrid.Location = new System.Drawing.Point(837, 34);
            this.btnActualizarGrid.Name = "btnActualizarGrid";
            this.btnActualizarGrid.Size = new System.Drawing.Size(33, 31);
            this.btnActualizarGrid.TabIndex = 57;
            this.btnActualizarGrid.UseVisualStyleBackColor = false;
            this.btnActualizarGrid.Click += new System.EventHandler(this.btnActualizarGrid_Click);
            // 
            // btnInsertarNuevo
            // 
            this.btnInsertarNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertarNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertarNuevo.Image = global::BcoItau.Plugin.Properties.Resources.btnAgregar;
            this.btnInsertarNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsertarNuevo.Location = new System.Drawing.Point(495, 342);
            this.btnInsertarNuevo.Name = "btnInsertarNuevo";
            this.btnInsertarNuevo.Size = new System.Drawing.Size(131, 28);
            this.btnInsertarNuevo.TabIndex = 56;
            this.btnInsertarNuevo.Text = "Insertar Nuevo";
            this.btnInsertarNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsertarNuevo.UseVisualStyleBackColor = true;
            this.btnInsertarNuevo.Click += new System.EventHandler(this.btnInsertarNuevo_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Image = global::BcoItau.Plugin.Properties.Resources.FacturacionMain;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(378, 342);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(82, 28);
            this.btnExportar.TabIndex = 55;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // CerrarButton
            // 
            this.CerrarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CerrarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CerrarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CerrarButton.Image = global::BcoItau.Plugin.Properties.Resources.btnSalir;
            this.CerrarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CerrarButton.Location = new System.Drawing.Point(269, 342);
            this.CerrarButton.Name = "CerrarButton";
            this.CerrarButton.Size = new System.Drawing.Size(74, 28);
            this.CerrarButton.TabIndex = 54;
            this.CerrarButton.Text = "&Cerrar";
            this.CerrarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CerrarButton.UseVisualStyleBackColor = true;
            // 
            // txtFiltrarPor
            // 
            this.txtFiltrarPor.Location = new System.Drawing.Point(233, 27);
            this.txtFiltrarPor.Name = "txtFiltrarPor";
            this.txtFiltrarPor.Size = new System.Drawing.Size(154, 20);
            this.txtFiltrarPor.TabIndex = 3;
            this.txtFiltrarPor.TextChanged += new System.EventHandler(this.txtFiltrarPor_TextChanged);
            // 
            // cbFiltrarPor
            // 
            this.cbFiltrarPor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltrarPor.FormattingEnabled = true;
            this.cbFiltrarPor.Items.AddRange(new object[] {
            "Sin Filtro",
            "No Cuenta Bco Bogota",
            "Nombre Cuenta",
            "Codigo Compensacion",
            "Nombre Municipio",
            "Estado"});
            this.cbFiltrarPor.Location = new System.Drawing.Point(88, 27);
            this.cbFiltrarPor.Name = "cbFiltrarPor";
            this.cbFiltrarPor.Size = new System.Drawing.Size(139, 21);
            this.cbFiltrarPor.TabIndex = 2;
            this.cbFiltrarPor.SelectedIndexChanged += new System.EventHandler(this.cbFiltrarPor_SelectedIndexChanged);
            // 
            // lblFiltrarPor
            // 
            this.lblFiltrarPor.AutoSize = true;
            this.lblFiltrarPor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrarPor.Location = new System.Drawing.Point(12, 35);
            this.lblFiltrarPor.Name = "lblFiltrarPor";
            this.lblFiltrarPor.Size = new System.Drawing.Size(70, 13);
            this.lblFiltrarPor.TabIndex = 1;
            this.lblFiltrarPor.Text = "Filtrar Por: ";
            // 
            // dgvCuentasMunicipios
            // 
            this.dgvCuentasMunicipios.AllowUserToAddRows = false;
            this.dgvCuentasMunicipios.AllowUserToDeleteRows = false;
            this.dgvCuentasMunicipios.AllowUserToOrderColumns = true;
            this.dgvCuentasMunicipios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuentasMunicipios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Tipo_Cuenta_Corriente,
            this.No_Cuenta_BcoBogota,
            this.Nombre_Cuenta,
            this.Tipo_Cuenta,
            this.Numero_Cuenta,
            this.Codigo_Compensacion,
            this.Nombre_Municipio,
            this.Nombre_Departamento,
            this.Nombre_Estado,
            this.Editar,
            this.Eliminar});
            this.dgvCuentasMunicipios.Location = new System.Drawing.Point(15, 86);
            this.dgvCuentasMunicipios.MultiSelect = false;
            this.dgvCuentasMunicipios.Name = "dgvCuentasMunicipios";
            this.dgvCuentasMunicipios.ReadOnly = true;
            this.dgvCuentasMunicipios.Size = new System.Drawing.Size(855, 237);
            this.dgvCuentasMunicipios.TabIndex = 0;
            this.dgvCuentasMunicipios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCuentasMunicipios_CellContentClick);
            this.dgvCuentasMunicipios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCuentasMunicipios_CellDoubleClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // Tipo_Cuenta_Corriente
            // 
            this.Tipo_Cuenta_Corriente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Tipo_Cuenta_Corriente.DataPropertyName = "Tipo_Cuenta_Corriente";
            this.Tipo_Cuenta_Corriente.HeaderText = "Tipo de Cuenta Corriente";
            this.Tipo_Cuenta_Corriente.Name = "Tipo_Cuenta_Corriente";
            this.Tipo_Cuenta_Corriente.ReadOnly = true;
            this.Tipo_Cuenta_Corriente.Width = 137;
            // 
            // No_Cuenta_BcoBogota
            // 
            this.No_Cuenta_BcoBogota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.No_Cuenta_BcoBogota.DataPropertyName = "No_Cuenta_BcoBogota";
            this.No_Cuenta_BcoBogota.HeaderText = "No Cuenta BcoBogota";
            this.No_Cuenta_BcoBogota.Name = "No_Cuenta_BcoBogota";
            this.No_Cuenta_BcoBogota.ReadOnly = true;
            this.No_Cuenta_BcoBogota.Width = 127;
            // 
            // Nombre_Cuenta
            // 
            this.Nombre_Cuenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Cuenta.DataPropertyName = "Nombre_Cuenta";
            this.Nombre_Cuenta.HeaderText = "Nombre de Cuenta";
            this.Nombre_Cuenta.Name = "Nombre_Cuenta";
            this.Nombre_Cuenta.ReadOnly = true;
            this.Nombre_Cuenta.Width = 111;
            // 
            // Tipo_Cuenta
            // 
            this.Tipo_Cuenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Tipo_Cuenta.DataPropertyName = "Tipo_Cuenta";
            this.Tipo_Cuenta.HeaderText = "Tipo Cuenta";
            this.Tipo_Cuenta.Name = "Tipo_Cuenta";
            this.Tipo_Cuenta.ReadOnly = true;
            this.Tipo_Cuenta.Width = 83;
            // 
            // Numero_Cuenta
            // 
            this.Numero_Cuenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Numero_Cuenta.DataPropertyName = "Numero_Cuenta";
            this.Numero_Cuenta.HeaderText = "Numero Cuenta";
            this.Numero_Cuenta.Name = "Numero_Cuenta";
            this.Numero_Cuenta.ReadOnly = true;
            this.Numero_Cuenta.Width = 97;
            // 
            // Codigo_Compensacion
            // 
            this.Codigo_Compensacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Codigo_Compensacion.DataPropertyName = "Codigo_Compensacion";
            this.Codigo_Compensacion.HeaderText = "Codigo Compensacion";
            this.Codigo_Compensacion.Name = "Codigo_Compensacion";
            this.Codigo_Compensacion.ReadOnly = true;
            this.Codigo_Compensacion.Width = 126;
            // 
            // Nombre_Municipio
            // 
            this.Nombre_Municipio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Municipio.DataPropertyName = "Nombre_Municipio";
            this.Nombre_Municipio.HeaderText = "Municipio";
            this.Nombre_Municipio.Name = "Nombre_Municipio";
            this.Nombre_Municipio.ReadOnly = true;
            this.Nombre_Municipio.Width = 77;
            // 
            // Nombre_Departamento
            // 
            this.Nombre_Departamento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Departamento.DataPropertyName = "Nombre_Departamento";
            this.Nombre_Departamento.HeaderText = "Departamento";
            this.Nombre_Departamento.Name = "Nombre_Departamento";
            this.Nombre_Departamento.ReadOnly = true;
            this.Nombre_Departamento.Width = 99;
            // 
            // Nombre_Estado
            // 
            this.Nombre_Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre_Estado.DataPropertyName = "Nombre_Estado";
            this.Nombre_Estado.HeaderText = "Estado";
            this.Nombre_Estado.Name = "Nombre_Estado";
            this.Nombre_Estado.ReadOnly = true;
            this.Nombre_Estado.Width = 65;
            // 
            // Editar
            // 
            this.Editar.ActiveLinkColor = System.Drawing.Color.Blue;
            this.Editar.DataPropertyName = "Editar";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue;
            this.Editar.DefaultCellStyle = dataGridViewCellStyle3;
            this.Editar.HeaderText = "Editar";
            this.Editar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Editar.LinkColor = System.Drawing.Color.Blue;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Editar.TrackVisitedState = false;
            this.Editar.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // Eliminar
            // 
            this.Eliminar.ActiveLinkColor = System.Drawing.Color.Blue;
            this.Eliminar.DataPropertyName = "Eliminar";
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Eliminar.LinkColor = System.Drawing.Color.Blue;
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Eliminar.TrackVisitedState = false;
            this.Eliminar.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // frmRelacionCuentasMunicipio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 402);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRelacionCuentasMunicipio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relación Cuentas Municipio";
            this.Load += new System.EventHandler(this.frmRelacionCuentasMunicipio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentasMunicipios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCuentasMunicipios;
        private System.Windows.Forms.TextBox txtFiltrarPor;
        private System.Windows.Forms.ComboBox cbFiltrarPor;
        private System.Windows.Forms.Label lblFiltrarPor;
        internal System.Windows.Forms.Button CerrarButton;
        internal System.Windows.Forms.Button btnExportar;
        internal System.Windows.Forms.Button btnInsertarNuevo;
        private System.Windows.Forms.Button btnActualizarGrid;
        private System.Windows.Forms.ToolTip tooltipGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo_Cuenta_Corriente;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Cuenta_BcoBogota;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo_Cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero_Cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo_Compensacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Municipio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Estado;
        private System.Windows.Forms.DataGridViewLinkColumn Editar;
        private System.Windows.Forms.DataGridViewLinkColumn Eliminar;
    }
}