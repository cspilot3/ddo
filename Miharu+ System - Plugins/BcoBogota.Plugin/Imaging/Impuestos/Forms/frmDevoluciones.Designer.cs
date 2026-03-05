namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
{
    partial class frmDevoluciones
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
            this.gbFormulariosDevueltos = new System.Windows.Forms.GroupBox();
            this.gbFiltroBusqueda = new System.Windows.Forms.GroupBox();
            this.lblValorBusqueda = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFiltros = new System.Windows.Forms.ComboBox();
            this.btnInsertarRegistro = new System.Windows.Forms.Button();
            this.cbOficinas = new System.Windows.Forms.ComboBox();
            this.dtpFechaRecaudo = new System.Windows.Forms.DateTimePicker();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.txtFormulario = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblCausalDevolucion = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.lblNumFormulario = new System.Windows.Forms.Label();
            this.lblOficina = new System.Windows.Forms.Label();
            this.lblFechaRecaudo = new System.Windows.Forms.Label();
            this.dgvDevueltos = new System.Windows.Forms.DataGridView();
            this.gbIngresarFormulario = new System.Windows.Forms.GroupBox();
            this.mtxtValor = new System.Windows.Forms.MaskedTextBox();
            this.cbCausalDevolucion = new System.Windows.Forms.ComboBox();
            this.btlSalir = new System.Windows.Forms.Button();
            this.Fecha_Recaudo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oficina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Formulario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Placa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Causal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subsanado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha_Proceso_Subsano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbFormulariosDevueltos.SuspendLayout();
            this.gbFiltroBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevueltos)).BeginInit();
            this.gbIngresarFormulario.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFormulariosDevueltos
            // 
            this.gbFormulariosDevueltos.Controls.Add(this.gbFiltroBusqueda);
            this.gbFormulariosDevueltos.Controls.Add(this.btnInsertarRegistro);
            this.gbFormulariosDevueltos.Controls.Add(this.cbOficinas);
            this.gbFormulariosDevueltos.Controls.Add(this.dtpFechaRecaudo);
            this.gbFormulariosDevueltos.Controls.Add(this.txtPlaca);
            this.gbFormulariosDevueltos.Controls.Add(this.txtFormulario);
            this.gbFormulariosDevueltos.Controls.Add(this.lblValor);
            this.gbFormulariosDevueltos.Controls.Add(this.lblCausalDevolucion);
            this.gbFormulariosDevueltos.Controls.Add(this.lblPlaca);
            this.gbFormulariosDevueltos.Controls.Add(this.lblNumFormulario);
            this.gbFormulariosDevueltos.Controls.Add(this.lblOficina);
            this.gbFormulariosDevueltos.Controls.Add(this.lblFechaRecaudo);
            this.gbFormulariosDevueltos.Controls.Add(this.dgvDevueltos);
            this.gbFormulariosDevueltos.Controls.Add(this.gbIngresarFormulario);
            this.gbFormulariosDevueltos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFormulariosDevueltos.Location = new System.Drawing.Point(12, 12);
            this.gbFormulariosDevueltos.Name = "gbFormulariosDevueltos";
            this.gbFormulariosDevueltos.Size = new System.Drawing.Size(866, 541);
            this.gbFormulariosDevueltos.TabIndex = 21;
            this.gbFormulariosDevueltos.TabStop = false;
            this.gbFormulariosDevueltos.Text = "Formularios Devueltos";
            // 
            // gbFiltroBusqueda
            // 
            this.gbFiltroBusqueda.Controls.Add(this.lblValorBusqueda);
            this.gbFiltroBusqueda.Controls.Add(this.txtFiltro);
            this.gbFiltroBusqueda.Controls.Add(this.btnBuscar);
            this.gbFiltroBusqueda.Controls.Add(this.label1);
            this.gbFiltroBusqueda.Controls.Add(this.cbFiltros);
            this.gbFiltroBusqueda.Location = new System.Drawing.Point(503, 27);
            this.gbFiltroBusqueda.Name = "gbFiltroBusqueda";
            this.gbFiltroBusqueda.Size = new System.Drawing.Size(346, 265);
            this.gbFiltroBusqueda.TabIndex = 19;
            this.gbFiltroBusqueda.TabStop = false;
            this.gbFiltroBusqueda.Text = "Busqueda";
            // 
            // lblValorBusqueda
            // 
            this.lblValorBusqueda.AutoSize = true;
            this.lblValorBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorBusqueda.Location = new System.Drawing.Point(10, 71);
            this.lblValorBusqueda.Name = "lblValorBusqueda";
            this.lblValorBusqueda.Size = new System.Drawing.Size(100, 13);
            this.lblValorBusqueda.TabIndex = 18;
            this.lblValorBusqueda.Text = "Valor Busqueda:";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltro.Location = new System.Drawing.Point(118, 68);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(200, 20);
            this.txtFiltro.TabIndex = 8;
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Location = new System.Drawing.Point(118, 99);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(123, 23);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Filtrar Por: ";
            // 
            // cbFiltros
            // 
            this.cbFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFiltros.FormattingEnabled = true;
            this.cbFiltros.Items.AddRange(new object[] {
            "Formulario"});
            this.cbFiltros.Location = new System.Drawing.Point(118, 33);
            this.cbFiltros.Name = "cbFiltros";
            this.cbFiltros.Size = new System.Drawing.Size(200, 21);
            this.cbFiltros.TabIndex = 7;
            // 
            // btnInsertarRegistro
            // 
            this.btnInsertarRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertarRegistro.Location = new System.Drawing.Point(194, 254);
            this.btnInsertarRegistro.Name = "btnInsertarRegistro";
            this.btnInsertarRegistro.Size = new System.Drawing.Size(123, 23);
            this.btnInsertarRegistro.TabIndex = 6;
            this.btnInsertarRegistro.Text = "Ingresar Registro";
            this.btnInsertarRegistro.UseVisualStyleBackColor = true;
            this.btnInsertarRegistro.Click += new System.EventHandler(this.btnInsertarRegistro_Click);
            // 
            // cbOficinas
            // 
            this.cbOficinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOficinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOficinas.FormattingEnabled = true;
            this.cbOficinas.Location = new System.Drawing.Point(194, 81);
            this.cbOficinas.Name = "cbOficinas";
            this.cbOficinas.Size = new System.Drawing.Size(200, 21);
            this.cbOficinas.TabIndex = 1;
            // 
            // dtpFechaRecaudo
            // 
            this.dtpFechaRecaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaRecaudo.Location = new System.Drawing.Point(194, 52);
            this.dtpFechaRecaudo.Name = "dtpFechaRecaudo";
            this.dtpFechaRecaudo.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaRecaudo.TabIndex = 0;
            // 
            // txtPlaca
            // 
            this.txtPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.Location = new System.Drawing.Point(194, 149);
            this.txtPlaca.MaxLength = 20;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(200, 20);
            this.txtPlaca.TabIndex = 3;
            this.txtPlaca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlaca_KeyPress);
            this.txtPlaca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPlaca_KeyUp);
            // 
            // txtFormulario
            // 
            this.txtFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormulario.Location = new System.Drawing.Point(194, 115);
            this.txtFormulario.MaxLength = 200;
            this.txtFormulario.Name = "txtFormulario";
            this.txtFormulario.Size = new System.Drawing.Size(200, 20);
            this.txtFormulario.TabIndex = 2;
            this.txtFormulario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFormulario_KeyPress);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValor.Location = new System.Drawing.Point(41, 224);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(36, 13);
            this.lblValor.TabIndex = 16;
            this.lblValor.Text = "Valor";
            // 
            // lblCausalDevolucion
            // 
            this.lblCausalDevolucion.AutoSize = true;
            this.lblCausalDevolucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCausalDevolucion.Location = new System.Drawing.Point(41, 188);
            this.lblCausalDevolucion.Name = "lblCausalDevolucion";
            this.lblCausalDevolucion.Size = new System.Drawing.Size(113, 13);
            this.lblCausalDevolucion.TabIndex = 15;
            this.lblCausalDevolucion.Text = "Causal Devolución";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.Location = new System.Drawing.Point(41, 156);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(39, 13);
            this.lblPlaca.TabIndex = 14;
            this.lblPlaca.Text = "Placa";
            // 
            // lblNumFormulario
            // 
            this.lblNumFormulario.AutoSize = true;
            this.lblNumFormulario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumFormulario.Location = new System.Drawing.Point(41, 122);
            this.lblNumFormulario.Name = "lblNumFormulario";
            this.lblNumFormulario.Size = new System.Drawing.Size(130, 13);
            this.lblNumFormulario.TabIndex = 13;
            this.lblNumFormulario.Text = "Numero de Formulario";
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOficina.Location = new System.Drawing.Point(41, 88);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(47, 13);
            this.lblOficina.TabIndex = 12;
            this.lblOficina.Text = "Oficina";
            // 
            // lblFechaRecaudo
            // 
            this.lblFechaRecaudo.AutoSize = true;
            this.lblFechaRecaudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaRecaudo.Location = new System.Drawing.Point(41, 52);
            this.lblFechaRecaudo.Name = "lblFechaRecaudo";
            this.lblFechaRecaudo.Size = new System.Drawing.Size(97, 13);
            this.lblFechaRecaudo.TabIndex = 11;
            this.lblFechaRecaudo.Text = "Fecha Recaudo";
            // 
            // dgvDevueltos
            // 
            this.dgvDevueltos.AllowUserToAddRows = false;
            this.dgvDevueltos.AllowUserToDeleteRows = false;
            this.dgvDevueltos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevueltos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha_Recaudo,
            this.Oficina,
            this.Formulario,
            this.Placa,
            this.Causal,
            this.Valor,
            this.Subsanado,
            this.Fecha_Proceso_Subsano});
            this.dgvDevueltos.Location = new System.Drawing.Point(21, 318);
            this.dgvDevueltos.Name = "dgvDevueltos";
            this.dgvDevueltos.ReadOnly = true;
            this.dgvDevueltos.Size = new System.Drawing.Size(829, 206);
            this.dgvDevueltos.TabIndex = 10;
            // 
            // gbIngresarFormulario
            // 
            this.gbIngresarFormulario.Controls.Add(this.mtxtValor);
            this.gbIngresarFormulario.Controls.Add(this.cbCausalDevolucion);
            this.gbIngresarFormulario.Location = new System.Drawing.Point(20, 26);
            this.gbIngresarFormulario.Name = "gbIngresarFormulario";
            this.gbIngresarFormulario.Size = new System.Drawing.Size(413, 266);
            this.gbIngresarFormulario.TabIndex = 20;
            this.gbIngresarFormulario.TabStop = false;
            this.gbIngresarFormulario.Text = "Ingresar Formulario";
            // 
            // mtxtValor
            // 
            this.mtxtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtValor.Location = new System.Drawing.Point(174, 191);
            this.mtxtValor.Name = "mtxtValor";
            this.mtxtValor.Size = new System.Drawing.Size(200, 20);
            this.mtxtValor.TabIndex = 5;
            this.mtxtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtValor_KeyPress);
            this.mtxtValor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mtxtValor_KeyUp);
            // 
            // cbCausalDevolucion
            // 
            this.cbCausalDevolucion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCausalDevolucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCausalDevolucion.FormattingEnabled = true;
            this.cbCausalDevolucion.Location = new System.Drawing.Point(174, 154);
            this.cbCausalDevolucion.Name = "cbCausalDevolucion";
            this.cbCausalDevolucion.Size = new System.Drawing.Size(200, 21);
            this.cbCausalDevolucion.TabIndex = 4;
            // 
            // btlSalir
            // 
            this.btlSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btlSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btlSalir.Image = global::BcoBogota.Plugin.Properties.Resources.btnSalir;
            this.btlSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btlSalir.Location = new System.Drawing.Point(384, 561);
            this.btlSalir.Name = "btlSalir";
            this.btlSalir.Size = new System.Drawing.Size(123, 23);
            this.btlSalir.TabIndex = 15;
            this.btlSalir.Text = "Cancelar";
            this.btlSalir.UseVisualStyleBackColor = true;
            this.btlSalir.Click += new System.EventHandler(this.btlSalir_Click);
            // 
            // Fecha_Recaudo
            // 
            this.Fecha_Recaudo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha_Recaudo.DataPropertyName = "Fecha_Recaudo";
            this.Fecha_Recaudo.FillWeight = 65.14872F;
            this.Fecha_Recaudo.HeaderText = "Fecha Recaudo";
            this.Fecha_Recaudo.Name = "Fecha_Recaudo";
            this.Fecha_Recaudo.ReadOnly = true;
            // 
            // Oficina
            // 
            this.Oficina.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Oficina.DataPropertyName = "Oficina";
            this.Oficina.FillWeight = 65.14872F;
            this.Oficina.HeaderText = "Oficina";
            this.Oficina.Name = "Oficina";
            this.Oficina.ReadOnly = true;
            // 
            // Formulario
            // 
            this.Formulario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Formulario.DataPropertyName = "Formulario";
            this.Formulario.FillWeight = 126.3238F;
            this.Formulario.HeaderText = "Numero de Formulario";
            this.Formulario.Name = "Formulario";
            this.Formulario.ReadOnly = true;
            // 
            // Placa
            // 
            this.Placa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Placa.DataPropertyName = "Placa";
            this.Placa.FillWeight = 65.14872F;
            this.Placa.HeaderText = "Placa";
            this.Placa.Name = "Placa";
            this.Placa.ReadOnly = true;
            // 
            // Causal
            // 
            this.Causal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Causal.DataPropertyName = "Causal";
            this.Causal.FillWeight = 65.14872F;
            this.Causal.HeaderText = "Causal";
            this.Causal.Name = "Causal";
            this.Causal.ReadOnly = true;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // Subsanado
            // 
            this.Subsanado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Subsanado.DataPropertyName = "Subsanado";
            this.Subsanado.FillWeight = 110.7971F;
            this.Subsanado.HeaderText = "Subsanado";
            this.Subsanado.MaxInputLength = 10;
            this.Subsanado.Name = "Subsanado";
            this.Subsanado.ReadOnly = true;
            // 
            // Fecha_Proceso_Subsano
            // 
            this.Fecha_Proceso_Subsano.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha_Proceso_Subsano.DataPropertyName = "Fecha_Proceso_Subsano";
            this.Fecha_Proceso_Subsano.HeaderText = "Fecha Proceso Subsano";
            this.Fecha_Proceso_Subsano.Name = "Fecha_Proceso_Subsano";
            this.Fecha_Proceso_Subsano.ReadOnly = true;
            // 
            // frmDevoluciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 593);
            this.Controls.Add(this.btlSalir);
            this.Controls.Add(this.gbFormulariosDevueltos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDevoluciones";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devoluciones";
            this.Load += new System.EventHandler(this.frmDevoluciones_Load);
            this.gbFormulariosDevueltos.ResumeLayout(false);
            this.gbFormulariosDevueltos.PerformLayout();
            this.gbFiltroBusqueda.ResumeLayout(false);
            this.gbFiltroBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevueltos)).EndInit();
            this.gbIngresarFormulario.ResumeLayout(false);
            this.gbIngresarFormulario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFormulariosDevueltos;
        private System.Windows.Forms.Button btnInsertarRegistro;
        private System.Windows.Forms.ComboBox cbOficinas;
        private System.Windows.Forms.DateTimePicker dtpFechaRecaudo;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.TextBox txtFormulario;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblCausalDevolucion;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.Label lblNumFormulario;
        private System.Windows.Forms.Label lblOficina;
        private System.Windows.Forms.Label lblFechaRecaudo;
        private System.Windows.Forms.DataGridView dgvDevueltos;
        private System.Windows.Forms.Button btlSalir;
        private System.Windows.Forms.GroupBox gbFiltroBusqueda;
        private System.Windows.Forms.Label lblValorBusqueda;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFiltros;
        private System.Windows.Forms.GroupBox gbIngresarFormulario;
        private System.Windows.Forms.ComboBox cbCausalDevolucion;
        private System.Windows.Forms.MaskedTextBox mtxtValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Recaudo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oficina;
        private System.Windows.Forms.DataGridViewTextBoxColumn Formulario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Placa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Causal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subsanado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Proceso_Subsano;
    }
}