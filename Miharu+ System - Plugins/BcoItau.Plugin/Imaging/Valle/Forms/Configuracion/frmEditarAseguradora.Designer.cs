namespace BcoItau.Plugin.Imaging.Atlantico.Forms.Configuracion
{
    partial class frmEditarAseguradora
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
            this.gbEditarAseguradora = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.cbEstados = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtDv = new System.Windows.Forms.TextBox();
            this.lblDv = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.lblNit = new System.Windows.Forms.Label();
            this.txtNombreAseguradora = new System.Windows.Forms.TextBox();
            this.lblCuentaBcoBogota = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.gbEditarAseguradora.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEditarAseguradora
            // 
            this.gbEditarAseguradora.Controls.Add(this.txtCodigo);
            this.gbEditarAseguradora.Controls.Add(this.txtTipo);
            this.gbEditarAseguradora.Controls.Add(this.lblCodigo);
            this.gbEditarAseguradora.Controls.Add(this.btnCancelar);
            this.gbEditarAseguradora.Controls.Add(this.btnActualizar);
            this.gbEditarAseguradora.Controls.Add(this.cbEstados);
            this.gbEditarAseguradora.Controls.Add(this.lblEstado);
            this.gbEditarAseguradora.Controls.Add(this.txtDv);
            this.gbEditarAseguradora.Controls.Add(this.lblDv);
            this.gbEditarAseguradora.Controls.Add(this.txtNit);
            this.gbEditarAseguradora.Controls.Add(this.lblNit);
            this.gbEditarAseguradora.Controls.Add(this.txtNombreAseguradora);
            this.gbEditarAseguradora.Controls.Add(this.lblCuentaBcoBogota);
            this.gbEditarAseguradora.Controls.Add(this.lblTipo);
            this.gbEditarAseguradora.Location = new System.Drawing.Point(16, 7);
            this.gbEditarAseguradora.Name = "gbEditarAseguradora";
            this.gbEditarAseguradora.Size = new System.Drawing.Size(410, 338);
            this.gbEditarAseguradora.TabIndex = 8;
            this.gbEditarAseguradora.TabStop = false;
            this.gbEditarAseguradora.Text = "Editar Aseguradora";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(160, 151);
            this.txtCodigo.MaxLength = 3;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(226, 20);
            this.txtCodigo.TabIndex = 3;
            // 
            // txtTipo
            // 
            this.txtTipo.Location = new System.Drawing.Point(160, 37);
            this.txtTipo.MaxLength = 10;
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(226, 20);
            this.txtTipo.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(17, 151);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(54, 13);
            this.lblCodigo.TabIndex = 16;
            this.lblCodigo.Text = "Codigo: ";
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::BcoItau.Plugin.Properties.Resources.btnSalir;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(244, 292);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Image = global::BcoItau.Plugin.Properties.Resources.btnGuardar;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(79, 292);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(105, 23);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // cbEstados
            // 
            this.cbEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstados.FormattingEnabled = true;
            this.cbEstados.Location = new System.Drawing.Point(160, 230);
            this.cbEstados.Name = "cbEstados";
            this.cbEstados.Size = new System.Drawing.Size(169, 21);
            this.cbEstados.TabIndex = 5;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(17, 238);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(54, 13);
            this.lblEstado.TabIndex = 12;
            this.lblEstado.Text = "Estado: ";
            // 
            // txtDv
            // 
            this.txtDv.Location = new System.Drawing.Point(160, 194);
            this.txtDv.MaxLength = 3;
            this.txtDv.Name = "txtDv";
            this.txtDv.Size = new System.Drawing.Size(226, 20);
            this.txtDv.TabIndex = 4;
            // 
            // lblDv
            // 
            this.lblDv.AutoSize = true;
            this.lblDv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDv.Location = new System.Drawing.Point(17, 197);
            this.lblDv.Name = "lblDv";
            this.lblDv.Size = new System.Drawing.Size(32, 13);
            this.lblDv.TabIndex = 6;
            this.lblDv.Text = "DV: ";
            // 
            // txtNit
            // 
            this.txtNit.Location = new System.Drawing.Point(160, 113);
            this.txtNit.MaxLength = 15;
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(226, 20);
            this.txtNit.TabIndex = 2;
            // 
            // lblNit
            // 
            this.lblNit.AutoSize = true;
            this.lblNit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNit.Location = new System.Drawing.Point(17, 117);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(36, 13);
            this.lblNit.TabIndex = 4;
            this.lblNit.Text = "NIT: ";
            // 
            // txtNombreAseguradora
            // 
            this.txtNombreAseguradora.Location = new System.Drawing.Point(160, 73);
            this.txtNombreAseguradora.MaxLength = 300;
            this.txtNombreAseguradora.Name = "txtNombreAseguradora";
            this.txtNombreAseguradora.Size = new System.Drawing.Size(226, 20);
            this.txtNombreAseguradora.TabIndex = 1;
            // 
            // lblCuentaBcoBogota
            // 
            this.lblCuentaBcoBogota.AutoSize = true;
            this.lblCuentaBcoBogota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuentaBcoBogota.Location = new System.Drawing.Point(17, 80);
            this.lblCuentaBcoBogota.Name = "lblCuentaBcoBogota";
            this.lblCuentaBcoBogota.Size = new System.Drawing.Size(133, 13);
            this.lblCuentaBcoBogota.TabIndex = 2;
            this.lblCuentaBcoBogota.Text = "Nombre Aseguradora: ";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.Location = new System.Drawing.Point(17, 37);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(40, 13);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo: ";
            // 
            // frmEditarAseguradora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 363);
            this.Controls.Add(this.gbEditarAseguradora);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditarAseguradora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar Aseguradora";
            this.Load += new System.EventHandler(this.frmEditarAseguradora_Load);
            this.gbEditarAseguradora.ResumeLayout(false);
            this.gbEditarAseguradora.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEditarAseguradora;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.ComboBox cbEstados;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtDv;
        private System.Windows.Forms.Label lblDv;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label lblNit;
        private System.Windows.Forms.TextBox txtNombreAseguradora;
        private System.Windows.Forms.Label lblCuentaBcoBogota;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.TextBox txtTipo;
        private System.Windows.Forms.TextBox txtCodigo;
    }
}