namespace Exportador_Acciones_Valores
{
    partial class FormImagen
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
            this.FolioPictureBox = new System.Windows.Forms.PictureBox();
            this.ExpedienteTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FolioTextBox = new System.Windows.Forms.TextBox();
            this.MostrarFileButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FolioPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FolioPictureBox
            // 
            this.FolioPictureBox.Location = new System.Drawing.Point(23, 12);
            this.FolioPictureBox.Name = "FolioPictureBox";
            this.FolioPictureBox.Size = new System.Drawing.Size(231, 261);
            this.FolioPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FolioPictureBox.TabIndex = 0;
            this.FolioPictureBox.TabStop = false;
            // 
            // ExpedienteTextBox
            // 
            this.ExpedienteTextBox.Location = new System.Drawing.Point(336, 11);
            this.ExpedienteTextBox.Name = "ExpedienteTextBox";
            this.ExpedienteTextBox.Size = new System.Drawing.Size(100, 20);
            this.ExpedienteTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Expediente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Folder";
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.Location = new System.Drawing.Point(336, 37);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(100, 20);
            this.FolderTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "File";
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(336, 63);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(100, 20);
            this.FileTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Folio";
            // 
            // FolioTextBox
            // 
            this.FolioTextBox.Location = new System.Drawing.Point(336, 89);
            this.FolioTextBox.Name = "FolioTextBox";
            this.FolioTextBox.Size = new System.Drawing.Size(100, 20);
            this.FolioTextBox.TabIndex = 7;
            // 
            // MostrarFileButton
            // 
            this.MostrarFileButton.Location = new System.Drawing.Point(336, 115);
            this.MostrarFileButton.Name = "MostrarFileButton";
            this.MostrarFileButton.Size = new System.Drawing.Size(100, 23);
            this.MostrarFileButton.TabIndex = 9;
            this.MostrarFileButton.Text = "Mostrar";
            this.MostrarFileButton.UseVisualStyleBackColor = true;
            this.MostrarFileButton.Click += new System.EventHandler(this.MostrarFileButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Mostrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(346, 200);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 447);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MostrarFileButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FolioTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FolderTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExpedienteTextBox);
            this.Controls.Add(this.FolioPictureBox);
            this.Name = "FormImagen";
            this.Text = "FormImagen";
            ((System.ComponentModel.ISupportInitialize)(this.FolioPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox FolioPictureBox;
        private System.Windows.Forms.TextBox ExpedienteTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FolderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FolioTextBox;
        private System.Windows.Forms.Button MostrarFileButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}