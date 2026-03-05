namespace BcoItau.Plugin.Imaging.Atlantico.Forms.Configuracion
{
    partial class frmEligeEstado_FechaDispersion
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
            this.gbEditarVarios = new System.Windows.Forms.GroupBox();
            this.lblMsjProgress = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.cbEstadosDispersion = new System.Windows.Forms.ComboBox();
            this.lblFechaDispersion = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.dtpFechaDispersion = new System.Windows.Forms.DateTimePicker();
            this.backgroundWorkerActualizando = new System.ComponentModel.BackgroundWorker();
            this.pbActualizando = new System.Windows.Forms.ProgressBar();
            this.lblProgreso = new System.Windows.Forms.Label();
            this.lblInformante = new System.Windows.Forms.Label();
            this.gbEditarVarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEditarVarios
            // 
            this.gbEditarVarios.Controls.Add(this.lblInformante);
            this.gbEditarVarios.Controls.Add(this.lblProgreso);
            this.gbEditarVarios.Controls.Add(this.pbActualizando);
            this.gbEditarVarios.Controls.Add(this.lblMsjProgress);
            this.gbEditarVarios.Controls.Add(this.btnCancelar);
            this.gbEditarVarios.Controls.Add(this.btnAceptar);
            this.gbEditarVarios.Controls.Add(this.cbEstadosDispersion);
            this.gbEditarVarios.Controls.Add(this.lblFechaDispersion);
            this.gbEditarVarios.Controls.Add(this.lblEstado);
            this.gbEditarVarios.Controls.Add(this.dtpFechaDispersion);
            this.gbEditarVarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEditarVarios.Location = new System.Drawing.Point(12, 10);
            this.gbEditarVarios.Name = "gbEditarVarios";
            this.gbEditarVarios.Size = new System.Drawing.Size(362, 233);
            this.gbEditarVarios.TabIndex = 0;
            this.gbEditarVarios.TabStop = false;
            this.gbEditarVarios.Text = "Elegir Datos Dispersion - Varios";
            // 
            // lblMsjProgress
            // 
            this.lblMsjProgress.AutoSize = true;
            this.lblMsjProgress.Location = new System.Drawing.Point(45, 133);
            this.lblMsjProgress.Name = "lblMsjProgress";
            this.lblMsjProgress.Size = new System.Drawing.Size(0, 13);
            this.lblMsjProgress.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::BcoItau.Plugin.Properties.Resources.btnSalir;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(181, 190);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(89, 33);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::BcoItau.Plugin.Properties.Resources.Aceptar;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(70, 190);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(95, 33);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // cbEstadosDispersion
            // 
            this.cbEstadosDispersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadosDispersion.FormattingEnabled = true;
            this.cbEstadosDispersion.Location = new System.Drawing.Point(127, 38);
            this.cbEstadosDispersion.Name = "cbEstadosDispersion";
            this.cbEstadosDispersion.Size = new System.Drawing.Size(181, 21);
            this.cbEstadosDispersion.TabIndex = 3;
            // 
            // lblFechaDispersion
            // 
            this.lblFechaDispersion.AutoEllipsis = true;
            this.lblFechaDispersion.AutoSize = true;
            this.lblFechaDispersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaDispersion.Location = new System.Drawing.Point(13, 81);
            this.lblFechaDispersion.Name = "lblFechaDispersion";
            this.lblFechaDispersion.Size = new System.Drawing.Size(109, 13);
            this.lblFechaDispersion.TabIndex = 2;
            this.lblFechaDispersion.Text = "Fecha Dispersion:";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(13, 41);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(50, 13);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "Estado:";
            // 
            // dtpFechaDispersion
            // 
            this.dtpFechaDispersion.Location = new System.Drawing.Point(127, 79);
            this.dtpFechaDispersion.Name = "dtpFechaDispersion";
            this.dtpFechaDispersion.Size = new System.Drawing.Size(222, 20);
            this.dtpFechaDispersion.TabIndex = 0;
            // 
            // backgroundWorkerActualizando
            // 
            this.backgroundWorkerActualizando.WorkerReportsProgress = true;
            this.backgroundWorkerActualizando.WorkerSupportsCancellation = true;
            this.backgroundWorkerActualizando.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerActualizando_DoWork);
            this.backgroundWorkerActualizando.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerActualizando_ProgressChanged);
            this.backgroundWorkerActualizando.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerActualizando_RunWorkerCompleted);
            // 
            // pbActualizando
            // 
            this.pbActualizando.Location = new System.Drawing.Point(14, 163);
            this.pbActualizando.Name = "pbActualizando";
            this.pbActualizando.Size = new System.Drawing.Size(333, 17);
            this.pbActualizando.Step = 1;
            this.pbActualizando.TabIndex = 8;
            // 
            // lblProgreso
            // 
            this.lblProgreso.AutoSize = true;
            this.lblProgreso.Location = new System.Drawing.Point(14, 143);
            this.lblProgreso.Name = "lblProgreso";
            this.lblProgreso.Size = new System.Drawing.Size(93, 13);
            this.lblProgreso.TabIndex = 9;
            this.lblProgreso.Text = "Progreso...(0%)";
            this.lblProgreso.Visible = false;
            // 
            // lblInformante
            // 
            this.lblInformante.AutoSize = true;
            this.lblInformante.Location = new System.Drawing.Point(14, 126);
            this.lblInformante.Name = "lblInformante";
            this.lblInformante.Size = new System.Drawing.Size(79, 13);
            this.lblInformante.TabIndex = 10;
            this.lblInformante.Text = "Informante...";
            this.lblInformante.Visible = false;
            // 
            // frmEligeEstado_FechaDispersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 255);
            this.Controls.Add(this.gbEditarVarios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEligeEstado_FechaDispersion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Varios";
            this.Load += new System.EventHandler(this.frmEligeEstado_FechaDispersion_Load);
            this.gbEditarVarios.ResumeLayout(false);
            this.gbEditarVarios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEditarVarios;
        private System.Windows.Forms.Label lblFechaDispersion;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.DateTimePicker dtpFechaDispersion;
        private System.Windows.Forms.ComboBox cbEstadosDispersion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblMsjProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorkerActualizando;
        private System.Windows.Forms.Label lblProgreso;
        private System.Windows.Forms.ProgressBar pbActualizando;
        private System.Windows.Forms.Label lblInformante;
    }
}