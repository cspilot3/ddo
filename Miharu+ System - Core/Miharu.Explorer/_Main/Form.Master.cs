using System;
using System.Drawing;
using System.Web;
using Miharu.Explorer._Clases;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.SecurityDMZServiceReference;

namespace Miharu.Explorer._Main
{
    public partial class Form : System.Web.UI.MasterPage
    {

        #region Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = "";
                dvChangePassword.Visible = false;
            }

            MenuLiteral.Text = Menu.getMenu(Utils.MyMenu(), Page);

            if (Utils.IsValidSession())
            {
                lblUser.Text = "Login: " + Utils.MySession.Usuario.Login;
                lblEntidad.Text = "Entidad: " + Utils.MySession.Entidad.Nombre;
                lblGrupo.Text = "Grupo: " + Utils.MySession.Entidad.Grupo;
                lblIP.Text = "IP: " + Utils.MySession.ClientIPAddress;
                lblVersion.Text = "Version: 1.0";
            }
        }

        #endregion

        #region Funciones

        private static string Cambiar_Password(string nNewPassword, string nConfirmNewPassword)
        {
            string resultado;

            if (Utils.Interno)
                resultado = Cambiar_Password_Interno(nNewPassword, nConfirmNewPassword);
            else
                resultado = Cambiar_Password_Externo(nNewPassword, nConfirmNewPassword);

            return resultado;
        }

        private static string Cambiar_Password_Interno(string nNewPassword, string nConfirmNewPassword)
        {
            var MySesion = Utils.MySession;
            var nOldPassword = MySesion.Usuario.Password;

            if (!Validar(nNewPassword, nConfirmNewPassword)) return HttpContext.Current.Session["ErrorAutenticacion"].ToString();
            var WebService = new Security.Library.WebService.SecurityWebService(
                Utils.SecurityWebServiceURL, MySesion.ClientIPAddress);

            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(MySesion.Usuario.Login, nOldPassword);
                string nMsgError;

                var Respuesta = WebService.ChangePassword(MySesion.Usuario.Login, nNewPassword,
                    out nMsgError);

                switch (Respuesta)
                {
                    case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.INVALIDO_PASSWORD:
                        return "Contraseña no válida";
                    case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.ERROR_PASSWORD:
                        return nMsgError;
                    case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.VALIDO:
                        Utils.MySession.Usuario.Password = nNewPassword;
                        return "La contraseña se cambió exitosamente";
                    default:
                        return "Ocurrio un error, por favor intente de nuevo";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private static string Cambiar_Password_Externo(string nNewPassword, string nConfirmNewPassword)
        {
            var MySesion = Utils.MySession;
            var nOldPassword = MySesion.Usuario.Password;

            if (!Validar(nNewPassword, nConfirmNewPassword)) return HttpContext.Current.Session["ErrorAutenticacion"].ToString();
            var WebService = new Security.Library.WebService.SecurityDMZWebService(
                Utils.SecurityWebServiceURL, "127.0.0.1");

            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(MySesion.Usuario.Login, nOldPassword);
                string nMsgError;

                var Respuesta = WebService.ChangePassword(MySesion.Usuario.Login, nNewPassword,
                    out nMsgError);

                switch (Respuesta)
                {
                    case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_PASSWORD:
                        return "Contraseña no válida";
                    case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.ERROR_PASSWORD:
                        return nMsgError;
                    case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.VALIDO:
                        Utils.MySession.Usuario.Password = nNewPassword;
                        return "La contraseña se cambió exitosamente";
                    default:
                        return "Ocurrio un error, por favor intente de nuevo";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private static bool Validar(string nNewPassword, string nConfirmPassword)
        {
            try
            {
                if (nNewPassword == "")
                    throw new Exception("El nuevo password no puede ser vacío");
                if (nNewPassword != nConfirmPassword)
                    throw new Exception("La confirmación del password no coincide");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["ErrorAutenticacion"] = ex.Message;
                return false;
            }
            return true;
        }

        #endregion

        #region Eventos

        protected void lbCerrar_Click(object sender, EventArgs e)
        {
            Session["Sesion"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void lbChangePassword_Click(object sender, EventArgs e)
        {
            this.dvChangePassword.Visible = true;
        }

        protected void btnPasswordAceptar_Click(object sender, EventArgs e)
        {
            if (lblMensaje.Text == "La contraseña se cambió exitosamente")
            {
                lblMensaje.Text = "";
                btnPasswordCancelar.Enabled = true;
                txtPassword1.Enabled = true;
                txtPassword2.Enabled = true;
                this.dvChangePassword.Visible = false;
            }
            else
            {
                lblMensaje.Text = Cambiar_Password(txtPassword1.Text, txtPassword2.Text);
                lblMensaje.ForeColor = lblMensaje.Text == "La contraseña se cambió exitosamente" ? Color.Green : Color.Red;
                btnPasswordCancelar.Enabled = lblMensaje.ForeColor == Color.Red;
                txtPassword1.Enabled = lblMensaje.ForeColor == Color.Red;
                txtPassword2.Enabled = lblMensaje.ForeColor == Color.Red;
            }
        }

        protected void btnPasswordCancelar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            this.dvChangePassword.Visible = false;
        }

        #endregion

    }
}
