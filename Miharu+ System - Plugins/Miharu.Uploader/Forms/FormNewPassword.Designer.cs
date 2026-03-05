namespace Miharu.Uploader.Forms
{
    partial class FormNewPassword
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
            Miharu.Desktop.Controls.DesktopTextBox.Rango rango7 = new Miharu.Desktop.Controls.DesktopTextBox.Rango();
            Miharu.Desktop.Controls.DesktopTextBox.Rango rango8 = new Miharu.Desktop.Controls.DesktopTextBox.Rango();
            Miharu.Desktop.Controls.DesktopTextBox.Rango rango9 = new Miharu.Desktop.Controls.DesktopTextBox.Rango();
            this.gbxLogin = new System.Windows.Forms.GroupBox();
            this.ConfirmPasswordTextBox = new Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl();
            this.CnfirmPasswordLabel = new System.Windows.Forms.Label();
            this.NewPasswordTextBox = new Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.OldPasswordTextBox = new Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl();
            this.OldPasswordLabel = new System.Windows.Forms.Label();
            this.NewPasswordLabel = new System.Windows.Forms.Label();
            this.LoginPictureBox = new System.Windows.Forms.PictureBox();
            this.gbxLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxLogin
            // 
            this.gbxLogin.Controls.Add(this.ConfirmPasswordTextBox);
            this.gbxLogin.Controls.Add(this.CnfirmPasswordLabel);
            this.gbxLogin.Controls.Add(this.NewPasswordTextBox);
            this.gbxLogin.Controls.Add(this.CancelarButton);
            this.gbxLogin.Controls.Add(this.AceptarButton);
            this.gbxLogin.Controls.Add(this.OldPasswordTextBox);
            this.gbxLogin.Controls.Add(this.OldPasswordLabel);
            this.gbxLogin.Controls.Add(this.NewPasswordLabel);
            this.gbxLogin.Controls.Add(this.LoginPictureBox);
            this.gbxLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxLogin.Location = new System.Drawing.Point(0, 0);
            this.gbxLogin.Margin = new System.Windows.Forms.Padding(0);
            this.gbxLogin.Name = "gbxLogin";
            this.gbxLogin.Padding = new System.Windows.Forms.Padding(0);
            this.gbxLogin.Size = new System.Drawing.Size(425, 248);
            this.gbxLogin.TabIndex = 1;
            this.gbxLogin.TabStop = false;
            // 
            // ConfirmPasswordTextBox
            // 
            this.ConfirmPasswordTextBox._Obligatorio = false;
            this.ConfirmPasswordTextBox._PermitePegar = false;
            this.ConfirmPasswordTextBox.Cantidad_Decimales = ((short)(0));
            this.ConfirmPasswordTextBox.Caracter_Decimal = '\0';
            this.ConfirmPasswordTextBox.DateFormat = null;
            this.ConfirmPasswordTextBox.DisabledEnter = false;
            this.ConfirmPasswordTextBox.DisabledTab = false;
            this.ConfirmPasswordTextBox.EnabledShortCuts = false;
            this.ConfirmPasswordTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.ConfirmPasswordTextBox.FocusOut = System.Drawing.Color.White;
            this.ConfirmPasswordTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(200, 154);
            this.ConfirmPasswordTextBox.MaskedTextBox_Property = "";
            this.ConfirmPasswordTextBox.MaximumLength = ((short)(0));
            this.ConfirmPasswordTextBox.MinimumLength = ((short)(0));
            this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
            this.ConfirmPasswordTextBox.Obligatorio = false;
            this.ConfirmPasswordTextBox.PasswordChar = '*';
            this.ConfirmPasswordTextBox.permitePegar = false;
            rango7.MaxValue = 2147483647D;
            rango7.MinValue = 0D;
            this.ConfirmPasswordTextBox.Rango = rango7;
            this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(214, 26);
            this.ConfirmPasswordTextBox.TabIndex = 5;
            this.ConfirmPasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal;
            this.ConfirmPasswordTextBox.Usa_Decimales = false;
            this.ConfirmPasswordTextBox.Validos_Cantidad_Puntos = false;
            // 
            // CnfirmPasswordLabel
            // 
            this.CnfirmPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CnfirmPasswordLabel.Location = new System.Drawing.Point(197, 134);
            this.CnfirmPasswordLabel.Name = "CnfirmPasswordLabel";
            this.CnfirmPasswordLabel.Size = new System.Drawing.Size(136, 16);
            this.CnfirmPasswordLabel.TabIndex = 4;
            this.CnfirmPasswordLabel.Text = "Confirmar contraseña";
            // 
            // NewPasswordTextBox
            // 
            this.NewPasswordTextBox._Obligatorio = false;
            this.NewPasswordTextBox._PermitePegar = false;
            this.NewPasswordTextBox.Cantidad_Decimales = ((short)(0));
            this.NewPasswordTextBox.Caracter_Decimal = '\0';
            this.NewPasswordTextBox.DateFormat = null;
            this.NewPasswordTextBox.DisabledEnter = false;
            this.NewPasswordTextBox.DisabledTab = false;
            this.NewPasswordTextBox.EnabledShortCuts = false;
            this.NewPasswordTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.NewPasswordTextBox.FocusOut = System.Drawing.Color.White;
            this.NewPasswordTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.NewPasswordTextBox.Location = new System.Drawing.Point(201, 100);
            this.NewPasswordTextBox.MaskedTextBox_Property = "";
            this.NewPasswordTextBox.MaximumLength = ((short)(0));
            this.NewPasswordTextBox.MinimumLength = ((short)(0));
            this.NewPasswordTextBox.Name = "NewPasswordTextBox";
            this.NewPasswordTextBox.Obligatorio = false;
            this.NewPasswordTextBox.PasswordChar = '*';
            this.NewPasswordTextBox.permitePegar = false;
            rango8.MaxValue = 2147483647D;
            rango8.MinValue = 0D;
            this.NewPasswordTextBox.Rango = rango8;
            this.NewPasswordTextBox.Size = new System.Drawing.Size(214, 26);
            this.NewPasswordTextBox.TabIndex = 3;
            this.NewPasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal;
            this.NewPasswordTextBox.Usa_Decimales = false;
            this.NewPasswordTextBox.Validos_Cantidad_Puntos = false;
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelarButton.FlatAppearance.BorderSize = 0;
            this.CancelarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CancelarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelarButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(316, 196);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(96, 35);
            this.CancelarButton.TabIndex = 7;
            this.CancelarButton.Text = "&Cancelar";
            this.CancelarButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // AceptarButton
            // 
            this.AceptarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AceptarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AceptarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AceptarButton.FlatAppearance.BorderSize = 0;
            this.AceptarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.AceptarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AceptarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AceptarButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.AceptarButton.Location = new System.Drawing.Point(206, 196);
            this.AceptarButton.Name = "AceptarButton";
            this.AceptarButton.Size = new System.Drawing.Size(93, 35);
            this.AceptarButton.TabIndex = 6;
            this.AceptarButton.Text = "&Aceptar";
            this.AceptarButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
            // 
            // OldPasswordTextBox
            // 
            this.OldPasswordTextBox._Obligatorio = false;
            this.OldPasswordTextBox._PermitePegar = false;
            this.OldPasswordTextBox.Cantidad_Decimales = ((short)(0));
            this.OldPasswordTextBox.Caracter_Decimal = '\0';
            this.OldPasswordTextBox.DateFormat = null;
            this.OldPasswordTextBox.DisabledEnter = false;
            this.OldPasswordTextBox.DisabledTab = false;
            this.OldPasswordTextBox.EnabledShortCuts = false;
            this.OldPasswordTextBox.FocusIn = System.Drawing.Color.LightYellow;
            this.OldPasswordTextBox.FocusOut = System.Drawing.Color.White;
            this.OldPasswordTextBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.OldPasswordTextBox.Location = new System.Drawing.Point(201, 47);
            this.OldPasswordTextBox.MaskedTextBox_Property = "";
            this.OldPasswordTextBox.MaximumLength = ((short)(0));
            this.OldPasswordTextBox.MinimumLength = ((short)(0));
            this.OldPasswordTextBox.Name = "OldPasswordTextBox";
            this.OldPasswordTextBox.Obligatorio = false;
            this.OldPasswordTextBox.PasswordChar = '*';
            this.OldPasswordTextBox.permitePegar = false;
            rango9.MaxValue = 2147483647D;
            rango9.MinValue = 0D;
            this.OldPasswordTextBox.Rango = rango9;
            this.OldPasswordTextBox.Size = new System.Drawing.Size(214, 26);
            this.OldPasswordTextBox.TabIndex = 1;
            this.OldPasswordTextBox.Type = Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl.TipoTextBox.Normal;
            this.OldPasswordTextBox.Usa_Decimales = false;
            this.OldPasswordTextBox.Validos_Cantidad_Puntos = false;
            // 
            // OldPasswordLabel
            // 
            this.OldPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OldPasswordLabel.Location = new System.Drawing.Point(198, 27);
            this.OldPasswordLabel.Name = "OldPasswordLabel";
            this.OldPasswordLabel.Size = new System.Drawing.Size(136, 16);
            this.OldPasswordLabel.TabIndex = 0;
            this.OldPasswordLabel.Text = "Contraseña anterior";
            // 
            // NewPasswordLabel
            // 
            this.NewPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPasswordLabel.Location = new System.Drawing.Point(198, 80);
            this.NewPasswordLabel.Name = "NewPasswordLabel";
            this.NewPasswordLabel.Size = new System.Drawing.Size(136, 16);
            this.NewPasswordLabel.TabIndex = 2;
            this.NewPasswordLabel.Text = "Nueva contraseña";
            // 
            // LoginPictureBox
            // 
            this.LoginPictureBox.Image = global::Miharu.Uploader.Properties.Resources.password;
            this.LoginPictureBox.Location = new System.Drawing.Point(8, 16);
            this.LoginPictureBox.Name = "LoginPictureBox";
            this.LoginPictureBox.Size = new System.Drawing.Size(172, 164);
            this.LoginPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LoginPictureBox.TabIndex = 0;
            this.LoginPictureBox.TabStop = false;
            // 
            // FormNewPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 248);
            this.Controls.Add(this.gbxLogin);
            this.Name = "FormNewPassword";
            this.Text = "Miharu Uploader - Cambiar Contraseña";
            this.gbxLogin.ResumeLayout(false);
            this.gbxLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbxLogin;
        internal Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl ConfirmPasswordTextBox;
        internal System.Windows.Forms.Label CnfirmPasswordLabel;
        internal Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl NewPasswordTextBox;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button AceptarButton;
        internal Miharu.Desktop.Controls.DesktopTextBox.DesktopTextBoxControl OldPasswordTextBox;
        internal System.Windows.Forms.Label OldPasswordLabel;
        internal System.Windows.Forms.Label NewPasswordLabel;
        internal System.Windows.Forms.PictureBox LoginPictureBox;
    }
}