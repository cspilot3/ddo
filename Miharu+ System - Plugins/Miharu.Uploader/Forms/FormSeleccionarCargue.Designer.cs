namespace Miharu.Uploader.Forms
{
    partial class FormSeleccionarCargue
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
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.CargarPathButton = new System.Windows.Forms.Button();
            this.RutaTextBox = new Miharu.Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl();
            this.CargueComboBox = new Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl();
            this.lblCargue = new System.Windows.Forms.Label();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblRuta);
            this.GroupBox1.Controls.Add(this.CargarPathButton);
            this.GroupBox1.Controls.Add(this.RutaTextBox);
            this.GroupBox1.Controls.Add(this.CargueComboBox);
            this.GroupBox1.Controls.Add(this.lblCargue);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(346, 109);
            this.GroupBox1.TabIndex = 8;
            this.GroupBox1.TabStop = false;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRuta.Location = new System.Drawing.Point(9, 62);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(80, 13);
            this.lblRuta.TabIndex = 12;
            this.lblRuta.Text = "Ruta Archivo";
            // 
            // CargarPathButton
            // 
            this.CargarPathButton.Image = global::Miharu.Uploader.Properties.Resources.Directorios;
            this.CargarPathButton.Location = new System.Drawing.Point(301, 74);
            this.CargarPathButton.Name = "CargarPathButton";
            this.CargarPathButton.Size = new System.Drawing.Size(28, 24);
            this.CargarPathButton.TabIndex = 11;
            this.CargarPathButton.Click += new System.EventHandler(this.CargarPathButton_Click);
            // 
            // RutaTextBox
            // 
            this.RutaTextBox.AllowPromptAsInput = false;
            this.RutaTextBox.Enabled = false;
            this.RutaTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.RutaTextBox.FocusOut = System.Drawing.Color.White;
            this.RutaTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RutaTextBox.Location = new System.Drawing.Point(12, 78);
            this.RutaTextBox.MinLength = 0;
            this.RutaTextBox.Name = "RutaTextBox";
            this.RutaTextBox.PromptChar = ' ';
            this.RutaTextBox.ResetOnPrompt = false;
            this.RutaTextBox.ResetOnSpace = false;
            this.RutaTextBox.Size = new System.Drawing.Size(281, 20);
            this.RutaTextBox.TabIndex = 2;
            // 
            // CargueComboBox
            // 
            this.CargueComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CargueComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CargueComboBox.DisabledEnter = false;
            this.CargueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CargueComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CargueComboBox.FormattingEnabled = true;
            this.CargueComboBox.Location = new System.Drawing.Point(12, 32);
            this.CargueComboBox.Name = "CargueComboBox";
            this.CargueComboBox.Size = new System.Drawing.Size(317, 21);
            this.CargueComboBox.TabIndex = 0;
            // 
            // lblCargue
            // 
            this.lblCargue.AutoSize = true;
            this.lblCargue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCargue.Location = new System.Drawing.Point(9, 16);
            this.lblCargue.Name = "lblCargue";
            this.lblCargue.Size = new System.Drawing.Size(47, 13);
            this.lblCargue.TabIndex = 1;
            this.lblCargue.Text = "Cargue";
            // 
            // AceptarButton
            // 
            this.AceptarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AceptarButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.AceptarButton.Image = global::Miharu.Uploader.Properties.Resources.tick;
            this.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AceptarButton.Location = new System.Drawing.Point(180, 131);
            this.AceptarButton.Name = "AceptarButton";
            this.AceptarButton.Size = new System.Drawing.Size(86, 32);
            this.AceptarButton.TabIndex = 9;
            this.AceptarButton.Text = "&Aceptar";
            this.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AceptarButton.UseVisualStyleBackColor = true;
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelarButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.CancelarButton.Image = global::Miharu.Uploader.Properties.Resources.cancel;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(272, 131);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(88, 32);
            this.CancelarButton.TabIndex = 10;
            this.CancelarButton.Text = "&Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // FormSeleccionarCargue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 175);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.AceptarButton);
            this.Controls.Add(this.CancelarButton);
            this.Name = "FormSeleccionarCargue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargue ...";
            this.Load += new System.EventHandler(this.FormSeleccionarCargue_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        private Desktop.Controls.DesktopTextTextBox.DesktopTextTextBoxControl RutaTextBox;
        internal Desktop.Controls.DesktopComboBox.DesktopComboBoxControl CargueComboBox;
        internal System.Windows.Forms.Label lblCargue;
        internal System.Windows.Forms.Button AceptarButton;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Label lblRuta;
        internal System.Windows.Forms.Button CargarPathButton;

    }
}