namespace MigradorImagenes
{
    partial class ExportForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.entradaTextBox = new System.Windows.Forms.TextBox();
            this.avanceProgressBar = new System.Windows.Forms.ProgressBar();
            this.procesarButton = new System.Windows.Forms.Button();
            this.salidaTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.domeNetbutton = new System.Windows.Forms.Button();
            this.RenombrarCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ruta entrada";
            // 
            // entradaTextBox
            // 
            this.entradaTextBox.Location = new System.Drawing.Point(12, 35);
            this.entradaTextBox.Name = "entradaTextBox";
            this.entradaTextBox.Size = new System.Drawing.Size(352, 20);
            this.entradaTextBox.TabIndex = 1;
            this.entradaTextBox.Text = "D:\\temp\\Imagenes_Risk";
            // 
            // avanceProgressBar
            // 
            this.avanceProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.avanceProgressBar.Location = new System.Drawing.Point(0, 316);
            this.avanceProgressBar.Name = "avanceProgressBar";
            this.avanceProgressBar.Size = new System.Drawing.Size(838, 23);
            this.avanceProgressBar.TabIndex = 2;
            // 
            // procesarButton
            // 
            this.procesarButton.Location = new System.Drawing.Point(370, 35);
            this.procesarButton.Name = "procesarButton";
            this.procesarButton.Size = new System.Drawing.Size(75, 71);
            this.procesarButton.TabIndex = 3;
            this.procesarButton.Text = "Procesar";
            this.procesarButton.UseVisualStyleBackColor = true;
            this.procesarButton.Click += new System.EventHandler(this.procesarButton_Click);
            // 
            // salidaTextBox
            // 
            this.salidaTextBox.Location = new System.Drawing.Point(12, 86);
            this.salidaTextBox.Name = "salidaTextBox";
            this.salidaTextBox.Size = new System.Drawing.Size(352, 20);
            this.salidaTextBox.TabIndex = 5;
            this.salidaTextBox.Text = "D:\\temp\\Salida";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ruta salida";
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logRichTextBox.Location = new System.Drawing.Point(12, 134);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(814, 176);
            this.logRichTextBox.TabIndex = 6;
            this.logRichTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Log";
            // 
            // domeNetbutton
            // 
            this.domeNetbutton.Location = new System.Drawing.Point(488, 35);
            this.domeNetbutton.Name = "domeNetbutton";
            this.domeNetbutton.Size = new System.Drawing.Size(75, 71);
            this.domeNetbutton.TabIndex = 8;
            this.domeNetbutton.Text = "Procesar (FMMB)";
            this.domeNetbutton.UseVisualStyleBackColor = true;
            this.domeNetbutton.Click += new System.EventHandler(this.domeNetbutton_Click);
            // 
            // RenombrarCheckbox
            // 
            this.RenombrarCheckbox.AutoSize = true;
            this.RenombrarCheckbox.Location = new System.Drawing.Point(576, 63);
            this.RenombrarCheckbox.Name = "RenombrarCheckbox";
            this.RenombrarCheckbox.Size = new System.Drawing.Size(129, 17);
            this.RenombrarCheckbox.TabIndex = 9;
            this.RenombrarCheckbox.Text = "Renombrar Contenido";
            this.RenombrarCheckbox.UseVisualStyleBackColor = true;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 339);
            this.Controls.Add(this.RenombrarCheckbox);
            this.Controls.Add(this.domeNetbutton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.salidaTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.procesarButton);
            this.Controls.Add(this.avanceProgressBar);
            this.Controls.Add(this.entradaTextBox);
            this.Controls.Add(this.label1);
            this.Name = "ExportForm";
            this.Text = "Convertir";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox entradaTextBox;
        private System.Windows.Forms.ProgressBar avanceProgressBar;
        private System.Windows.Forms.Button procesarButton;
        private System.Windows.Forms.TextBox salidaTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button domeNetbutton;
        private System.Windows.Forms.CheckBox RenombrarCheckbox;
    }
}

