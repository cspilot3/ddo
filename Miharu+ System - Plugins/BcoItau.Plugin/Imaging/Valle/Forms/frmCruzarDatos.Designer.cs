namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    partial class frmCruzarDatos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCruzarDatos));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pbCruceDatos = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTitleProgress = new System.Windows.Forms.Label();
            this.lblFechaProceso = new System.Windows.Forms.Label();
            this.dtpCruceDatos = new System.Windows.Forms.DateTimePicker();
            this.btnCruzarDatos = new System.Windows.Forms.Button();
            this.backgroundWorkerCruceDatos = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(238, 126);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 37);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pbCruceDatos
            // 
            this.pbCruceDatos.Location = new System.Drawing.Point(18, 93);
            this.pbCruceDatos.MarqueeAnimationSpeed = 10;
            this.pbCruceDatos.Name = "pbCruceDatos";
            this.pbCruceDatos.Size = new System.Drawing.Size(340, 15);
            this.pbCruceDatos.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbCruceDatos.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTitleProgress);
            this.groupBox1.Controls.Add(this.pbCruceDatos);
            this.groupBox1.Controls.Add(this.lblFechaProceso);
            this.groupBox1.Controls.Add(this.dtpCruceDatos);
            this.groupBox1.Controls.Add(this.btnCruzarDatos);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 174);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cruce";
            // 
            // lblTitleProgress
            // 
            this.lblTitleProgress.AutoSize = true;
            this.lblTitleProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleProgress.Location = new System.Drawing.Point(15, 75);
            this.lblTitleProgress.Name = "lblTitleProgress";
            this.lblTitleProgress.Size = new System.Drawing.Size(19, 13);
            this.lblTitleProgress.TabIndex = 7;
            this.lblTitleProgress.Text = "...";
            // 
            // lblFechaProceso
            // 
            this.lblFechaProceso.AutoSize = true;
            this.lblFechaProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaProceso.Location = new System.Drawing.Point(15, 22);
            this.lblFechaProceso.Name = "lblFechaProceso";
            this.lblFechaProceso.Size = new System.Drawing.Size(110, 13);
            this.lblFechaProceso.TabIndex = 6;
            this.lblFechaProceso.Text = "Fecha de Proceso";
            // 
            // dtpCruceDatos
            // 
            this.dtpCruceDatos.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCruceDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCruceDatos.Location = new System.Drawing.Point(140, 19);
            this.dtpCruceDatos.Name = "dtpCruceDatos";
            this.dtpCruceDatos.Size = new System.Drawing.Size(218, 20);
            this.dtpCruceDatos.TabIndex = 5;
            // 
            // btnCruzarDatos
            // 
            this.btnCruzarDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCruzarDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCruzarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnCruzarDatos.Image")));
            this.btnCruzarDatos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCruzarDatos.Location = new System.Drawing.Point(39, 126);
            this.btnCruzarDatos.Name = "btnCruzarDatos";
            this.btnCruzarDatos.Size = new System.Drawing.Size(140, 37);
            this.btnCruzarDatos.TabIndex = 4;
            this.btnCruzarDatos.Text = "Cruzar Datos";
            this.btnCruzarDatos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCruzarDatos.UseVisualStyleBackColor = true;
            this.btnCruzarDatos.Click += new System.EventHandler(this.btnCruzarDatos_Click);
            // 
            // backgroundWorkerCruceDatos
            // 
            this.backgroundWorkerCruceDatos.WorkerReportsProgress = true;
            this.backgroundWorkerCruceDatos.WorkerSupportsCancellation = true;
            this.backgroundWorkerCruceDatos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCruceDatos_DoWork);
            this.backgroundWorkerCruceDatos.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerCruceDatos_ProgressChanged);
            this.backgroundWorkerCruceDatos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerCruceDatos_RunWorkerCompleted);
            // 
            // frmCruzarDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 187);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCruzarDatos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cruzar Datos";
            this.Load += new System.EventHandler(this.frmCruzarDatos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ProgressBar pbCruceDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCruzarDatos;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCruceDatos;
        private System.Windows.Forms.DateTimePicker dtpCruceDatos;
        private System.Windows.Forms.Label lblFechaProceso;
        private System.Windows.Forms.Label lblTitleProgress;
    }
}