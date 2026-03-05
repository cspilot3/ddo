namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    partial class frmPublicacionDatos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPublicacionDatos));
            this.gbPublicacion = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTitleProgress = new System.Windows.Forms.Label();
            this.pgPublicacionDatos = new System.Windows.Forms.ProgressBar();
            this.dpFechaProceso = new System.Windows.Forms.DateTimePicker();
            this.btnPublicarDatos = new System.Windows.Forms.Button();
            this.backgroundWorkerPublicacion = new System.ComponentModel.BackgroundWorker();
            this.timerPublicacion = new System.Windows.Forms.Timer(this.components);
            this.gbPublicacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPublicacion
            // 
            this.gbPublicacion.AutoSize = true;
            this.gbPublicacion.Controls.Add(this.btnCancelar);
            this.gbPublicacion.Controls.Add(this.lblTitleProgress);
            this.gbPublicacion.Controls.Add(this.pgPublicacionDatos);
            this.gbPublicacion.Controls.Add(this.dpFechaProceso);
            this.gbPublicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPublicacion.ForeColor = System.Drawing.Color.Black;
            this.gbPublicacion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gbPublicacion.Location = new System.Drawing.Point(13, 13);
            this.gbPublicacion.Name = "gbPublicacion";
            this.gbPublicacion.Size = new System.Drawing.Size(330, 262);
            this.gbPublicacion.TabIndex = 0;
            this.gbPublicacion.TabStop = false;
            this.gbPublicacion.Text = "Fecha Proceso";
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(205, 206);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 37);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblTitleProgress
            // 
            this.lblTitleProgress.AutoSize = true;
            this.lblTitleProgress.Location = new System.Drawing.Point(19, 114);
            this.lblTitleProgress.Name = "lblTitleProgress";
            this.lblTitleProgress.Size = new System.Drawing.Size(133, 13);
            this.lblTitleProgress.TabIndex = 2;
            this.lblTitleProgress.Text = "Progreso General (0%)";
            this.lblTitleProgress.Visible = false;
            // 
            // pgPublicacionDatos
            // 
            this.pgPublicacionDatos.Location = new System.Drawing.Point(22, 143);
            this.pgPublicacionDatos.Name = "pgPublicacionDatos";
            this.pgPublicacionDatos.Size = new System.Drawing.Size(279, 23);
            this.pgPublicacionDatos.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgPublicacionDatos.TabIndex = 1;
            this.pgPublicacionDatos.Visible = false;
            // 
            // dpFechaProceso
            // 
            this.dpFechaProceso.Location = new System.Drawing.Point(40, 42);
            this.dpFechaProceso.Name = "dpFechaProceso";
            this.dpFechaProceso.Size = new System.Drawing.Size(235, 20);
            this.dpFechaProceso.TabIndex = 0;
            // 
            // btnPublicarDatos
            // 
            this.btnPublicarDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublicarDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublicarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnPublicarDatos.Image")));
            this.btnPublicarDatos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPublicarDatos.Location = new System.Drawing.Point(35, 219);
            this.btnPublicarDatos.Name = "btnPublicarDatos";
            this.btnPublicarDatos.Size = new System.Drawing.Size(140, 37);
            this.btnPublicarDatos.TabIndex = 1;
            this.btnPublicarDatos.Text = "Publicar Datos";
            this.btnPublicarDatos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPublicarDatos.UseVisualStyleBackColor = true;
            this.btnPublicarDatos.Click += new System.EventHandler(this.btnPublicarDatos_Click);
            // 
            // backgroundWorkerPublicacion
            // 
            this.backgroundWorkerPublicacion.WorkerReportsProgress = true;
            this.backgroundWorkerPublicacion.WorkerSupportsCancellation = true;
            this.backgroundWorkerPublicacion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPublicacion_DoWork);
            this.backgroundWorkerPublicacion.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerPublicacion_ProgressChanged);
            this.backgroundWorkerPublicacion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPublicacion_RunWorkerCompleted);
            // 
            // timerPublicacion
            // 
            this.timerPublicacion.Interval = 1000;
            this.timerPublicacion.Tick += new System.EventHandler(this.timerPublicacion_Tick);
            // 
            // frmPublicacionDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 276);
            this.ControlBox = false;
            this.Controls.Add(this.btnPublicarDatos);
            this.Controls.Add(this.gbPublicacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPublicacionDatos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publicación Datos Impuestos";
            this.Load += new System.EventHandler(this.frmPublicacionDatos_Load);
            this.gbPublicacion.ResumeLayout(false);
            this.gbPublicacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPublicacion;
        private System.Windows.Forms.DateTimePicker dpFechaProceso;
        private System.Windows.Forms.Button btnPublicarDatos;
        private System.Windows.Forms.Button btnCancelar;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPublicacion;
        private System.Windows.Forms.Label lblTitleProgress;
        private System.Windows.Forms.ProgressBar pgPublicacionDatos;
        private System.Windows.Forms.Timer timerPublicacion;
    }
}