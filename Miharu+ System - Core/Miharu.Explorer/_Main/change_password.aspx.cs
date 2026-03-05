using System;
using System.Web;
using System.Web.Services;
using Miharu.Explorer._Clases;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.Session;

namespace Miharu.Explorer._Main
{
    public partial class change_password : System.Web.UI.Page
    {
        private static Sesion MySesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = Utils.MySession;
        }

        private static bool Validar(string nNewPassword, string nConfirmPassword)
        {
            try
            {
                if (nNewPassword == "")
                    throw new Exception("El nuevo password no puede ser vacío");
                //ScriptHelper.Site.ShowAlert(this, "", Utils.MsgBoxIcon.IconError);
                if (nNewPassword != nConfirmPassword)
                    //ScriptHelper.Site.ShowAlert(this, "La confirmación del password no coincide", Utils.MsgBoxIcon.IconError);
                    throw new Exception("La confirmación del password no coincide");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["ErrorAutenticacion"] = ex.Message;
                return false;
            }
            return true;
        }

        [WebMethod]
        public static int Change_Password(string nOldPassword, string nNewPassword, string nConfirmPassword)
        {
            if (Validar(nNewPassword, nConfirmPassword))
            {
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
                        case EnumValidateUser.INVALIDO_PASSWORD:
                            //ScriptHelper.Site.ShowAlert(this, "Contraseña no válida", Utils.MsgBoxIcon.IconWarning);
                            //break;
                            return 2;
                        case EnumValidateUser.ERROR_PASSWORD:
                            //ScriptHelper.Site.ShowAlert(this, nMsgError, Utils.MsgBoxIcon.IconWarning);
                            //break;
                            return 3;
                        case EnumValidateUser.VALIDO:
                            //ScriptHelper.Site.ShowAlert(this, "La contraseña se cambió exitosamente",
                            //                            Utils.MsgBoxIcon.IconInformation);
                            //break;
                            return 1;
                        default:
                            return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    //ScriptHelper.Site.ShowAlert(this, ex.Message, Utils.MsgBoxIcon.IconError);
                }
            }
            return HttpContext.Current.Session["ErrorAutenticacion"].ToString().ToLower().Contains("vacío") ? 4 : 5;
        }
    }
}