namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    partial class frmRelacionAseguradoras
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnActualizarGrid = new System.Windows.Forms.Button();
            this.btnInsertarNuevo = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.CerrarButton = new System.Windows.Forms.Button();
            this.txtFiltrarPor = new System.Windows.Forms.TextBox();
            this.cbFiltrarPor = new System.Windows.Forms.ComboBox();
            this.lblFiltrarPor = new System.Windows.Forms.Label();
            this.dgvAseguradoras = new System.Windows.Forms.DataGridView();
            this.tooltipGeneral = new System.Windows.Forms.ToolTip(this.components);
            this.Eliminar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Editar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Activo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DENOMINACION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Aseguradora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAseguradoras)).BeginInit();
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
            this.groupBox1.Controls.Add(this.dgvAseguradoras);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 385);
            this.groupBox1.TabIndex = 1;
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
            this.txtFiltrarPor.Location = new System.Drawing.Point(233, 35);
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
            "TIPO",
            "CODIGO",
            "DENOMINACION",
            "NIT",
            "DV"});
            this.cbFiltrarPor.Location = new System.Drawing.Point(88, 35);
            this.cbFiltrarPor.Name = "cbFiltrarPor";
            this.cbFiltrarPor.Size = new System.Drawing.Size(139, 21);
            this.cbFiltrarPor.TabIndex = 2;
            this.cbFiltrarPor.SelectedIndexChanged += new System.EventHandler(this.cbFiltrarPor_SelectedIndexChanged);
            // 
            // lblFiltrarPor
            // 
            this.lblFiltrarPor.AutoSize = true;
            this.lblFiltrarPor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrarPor.Location = new System.Drawing.Point(12, 43);
            this.lblFiltrarPor.Name = "lblFiltrarPor";
            this.lblFiltrarPor.Size = new System.Drawing.Size(70, 13);
            this.lblFiltrarPor.TabIndex = 1;
            this.lblFiltrarPor.Text = "Filtrar Por: ";
            // 
            // dgvAseguradoras
            // 
            this.dgvAseguradoras.AllowUserToAddRows = false;
            this.dgvAseguradoras.AllowUserToDeleteRows = false;
            this.dgvAseguradoras.AllowUserToOrderColumns = true;
            this.dgvAseguradoras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAseguradoras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_Aseguradora,
            this.TIPO,
            this.CODIGO,
            this.DENOMINACION,
            this.NIT,
            this.DV,
            this.Activo,
            this.Editar,
            this.Eliminar});
            this.dgvAseguradoras.Location = new System.Drawing.Point(15, 86);
            this.dgvAseguradoras.MultiSelect = false;
            this.dgvAseguradoras.Name = "dgvAseguradoras";
            this.dgvAseguradoras.ReadOnly = true;
            this.dgvAseguradoras.Size = new System.Drawing.Size(855, 237);
            this.dgvAseguradoras.TabIndex = 0;
            this.dgvAseguradoras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAseguradoras_CellContentClick);
            this.dgvAseguradoras.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAseguradoras_CellContentDoubleClick);
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
            // Editar
            // 
            this.Editar.ActiveLinkColor = System.Drawing.Color.Blue;
            this.Editar.DataPropertyName = "Editar";
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
            this.Editar.DefaultCellStyle = dataGridViewCellStyle5;
            this.Editar.HeaderText = "Editar";
            this.Editar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Editar.LinkColor = System.Drawing.Color.Blue;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Editar.TrackVisitedState = false;
            this.Editar.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // Activo
            // 
            this.Activo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Activo.DataPropertyName = "Activo";
            this.Activo.HeaderText = "Activo";
            this.Activo.Name = "Activo";
            this.Activo.ReadOnly = true;
            this.Activo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Activo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DV
            // 
            this.DV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DV.DataPropertyName = "DV";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DV.DefaultCellStyle = dataGridViewCellStyle4;
            this.DV.HeaderText = "DV";
            this.DV.Name = "DV";
            this.DV.ReadOnly = true;
            // 
            // NIT
            // 
            this.NIT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NIT.DataPropertyName = "NIT";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NIT.DefaultCellStyle = dataGridViewCellStyle3;
            this.NIT.HeaderText = "NIT";
            this.NIT.Name = "NIT";
            this.NIT.ReadOnly = true;
            // 
            // DENOMINACION
            // 
            this.DENOMINACION.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DENOMINACION.DataPropertyName = "DENOMINACION";
            this.DENOMINACION.HeaderText = "Nombre Aseguradora";
            this.DENOMINACION.Name = "DENOMINACION";
            this.DENOMINACION.ReadOnly = true;
            // 
            // CODIGO
            // 
            this.CODIGO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CODIGO.DataPropertyName = "CODIGO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CODIGO.DefaultCellStyle = dataGridViewCellStyle2;
            this.CODIGO.HeaderText = "Codigo";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            // 
            // TIPO
            // 
            this.TIPO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TIPO.DataPropertyName = "TIPO";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TIPO.DefaultCellStyle = dataGridViewCellStyle1;
            this.TIPO.HeaderText = "Tipo";
            this.TIPO.Name = "TIPO";
            this.TIPO.ReadOnly = true;
            // 
            // Id_Aseguradora
            // 
            this.Id_Aseguradora.DataPropertyName = "Id_Aseguradora";
            this.Id_Aseguradora.HeaderText = "Id_Aseguradora";
            this.Id_Aseguradora.Name = "Id_Aseguradora";
            this.Id_Aseguradora.ReadOnly = true;
            this.Id_Aseguradora.Visible = false;
            // 
            // frmRelacionAseguradoras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 416);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRelacionAseguradoras";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relación Aseguradoras";
            this.Load += new System.EventHandler(this.frmRelacionAseguradoras_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAseguradoras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnActualizarGrid;
        internal System.Windows.Forms.Button btnInsertarNuevo;
        internal System.Windows.Forms.Button btnExportar;
        internal System.Windows.Forms.Button CerrarButton;
        private System.Windows.Forms.TextBox txtFiltrarPor;
        private System.Windows.Forms.ComboBox cbFiltrarPor;
        private System.Windows.Forms.Label lblFiltrarPor;
        private System.Windows.Forms.DataGridView dgvAseguradoras;
        private System.Windows.Forms.ToolTip tooltipGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Aseguradora;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DENOMINACION;
        private System.Windows.Forms.DataGridViewTextBoxColumn NIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DV;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Activo;
        private System.Windows.Forms.DataGridViewLinkColumn Editar;
        private System.Windows.Forms.DataGridViewLinkColumn Eliminar;
    }
}