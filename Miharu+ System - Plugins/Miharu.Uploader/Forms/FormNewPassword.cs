using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Desktop.Controls.DesktopMessageBox;
using Miharu.Security.Library.SecurityDMZServiceReference;
using Miharu.Security.Library.WebService;

namespace Miharu.Uploader.Forms
{
    public partial class FormNewPassword : Form
    {

        #region Propiedades

        public string OldPassword
        {
            get { return this.OldPasswordTextBox.Text; }
            set { OldPasswordTextBox.Text = value; }
        }

        public string NewPassword
        {
            get { return NewPasswordTextBox.Text; }
        }

        #endregion

        #region Declaraciones

        public FormNewPassword()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            Cambiar_Contraseña(OldPasswordTextBox.Text, NewPasswordTextBox.Text, ConfirmPasswordTextBox.Text);
        }

        #endregion

        #region Metodos

        private void Cambiar_Contraseña(string nOldPassword, string nNewPassword, string nConfirmPassword)
        {
            if (!Validar()) return;
            var WebService = new SecurityDMZWebService(Program.SecurityWebServiceURL, Program.GetClientIpAddress());
            
            try
            {
                string nMsgError;
                WebService.CrearCanalSeguro();
                WebService.setUser(Program.MiharuSession.Usuario.Login, nOldPassword);

                var Respuesta = WebService.ChangePassword(Program.MiharuSession.Usuario.Login,
                    NewPasswordTextBox.Text, out nMsgError);

                switch (Respuesta)
                {
                    case EnumValidateUser.INVALIDO_PASSWORD :
                        MessageBox.Show("La contraseña anterior no es válida", Program.AssemblyTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case EnumValidateUser.ERROR_PASSWORD :
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show(nMsgError, Program.AssemblyTitle, MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;
                    case EnumValidateUser.VALIDO :
                        MessageBox.Show("La contraseña se cambio exitosamente", Program.AssemblyTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    default:
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show(nMsgError, Program.AssemblyTitle, MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (NewPasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Las contraseñas ingresadas no son iguales",
                    Program.AssemblyName,
                    Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                NewPasswordTextBox.SelectAll();
                NewPasswordTextBox.Focus();
            }
            else
                return true;
            return false;
        }

        #endregion

    }
}
