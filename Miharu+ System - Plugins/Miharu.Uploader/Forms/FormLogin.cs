using System;
using System.Windows.Forms;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace Miharu.Uploader.Forms
{
    public partial class FormLogin : Form
    {
        #region Propiedades

        public string Login
        {
            get { return this.LoginTextBox.Text; }
            set { this.LoginTextBox.Text = ""; }
        }

        public string Password
        {
            get { return this.PasswordTextBox.Text; }
            set { this.PasswordTextBox.Text = ""; }
        }

        #endregion

        #region Eventos

        public FormLogin()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Validar()){
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            //var MainForm = new FormMain();
            }
        }

        public void SelectText()
        {
            LoginTextBox.Focus();
            LoginTextBox.SelectAll();
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (LoginTextBox.Text.Trim() == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("El campo Usuario no puede ser vacio",
                   Program.AssemblyName,
                   Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                LoginTextBox.SelectAll();
                LoginTextBox.Focus();
                return false;
            }

            if (PasswordTextBox.Text.Trim() == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("EL campo Contraseña no puede ser vacio",
                   Program.AssemblyName,
                   Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                PasswordTextBox.SelectAll();
                PasswordTextBox.Focus();
                return false;
            }

            return true;
        }
        #endregion
    }
}
