namespace Miharu.Uploader.Forms
{
    partial class FormNewProcess
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
            this.newImageProcessButton = new System.Windows.Forms.Button();
            this.newDataProcessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newImageProcessButton
            // 
            this.newImageProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.newImageProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newImageProcessButton.Image = global::Miharu.Uploader.Properties.Resources.Imaging;
            this.newImageProcessButton.Location = new System.Drawing.Point(12, 12);
            this.newImageProcessButton.Name = "newImageProcessButton";
            this.newImageProcessButton.Size = new System.Drawing.Size(100, 100);
            this.newImageProcessButton.TabIndex = 0;
            this.newImageProcessButton.Text = "Imágenes";
            this.newImageProcessButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newImageProcessButton.UseVisualStyleBackColor = true;
            this.newImageProcessButton.Click += new System.EventHandler(this.newImageProcessButton_Click);
            // 
            // newDataProcessButton
            // 
            this.newDataProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newDataProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newDataProcessButton.Image = global::Miharu.Uploader.Properties.Resources.DataProc;
            this.newDataProcessButton.Location = new System.Drawing.Point(150, 12);
            this.newDataProcessButton.Name = "newDataProcessButton";
            this.newDataProcessButton.Size = new System.Drawing.Size(100, 100);
            this.newDataProcessButton.TabIndex = 1;
            this.newDataProcessButton.Text = "Datos";
            this.newDataProcessButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newDataProcessButton.UseVisualStyleBackColor = true;
            this.newDataProcessButton.Click += new System.EventHandler(this.newDataProcessButton_Click);
            // 
            // FormNewProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 124);
            this.Controls.Add(this.newDataProcessButton);
            this.Controls.Add(this.newImageProcessButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormNewProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo Proceso ...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newImageProcessButton;
        private System.Windows.Forms.Button newDataProcessButton;
    }
}