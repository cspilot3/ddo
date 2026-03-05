using System;
using System.Web;
using System.Web.Services;
using Miharu.Explorer.Imaging._Clases;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.SecurityDMZServiceReference;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.WebService;
using System.Net;

namespace Miharu.Explorer.Imaging._Main
{
    public partial class Login : System.Web.UI.Page
    {
        public string _RecoverPassURL;

        public void Page_Load()
        {
            if (!IsPostBack)
                _RecoverPassURL = Utils.ForgottenPasswordURL;
        }

        public string getRecoverURL
        {
            get { return _RecoverPassURL; } 
        }

        [WebMethod]
        public static double Autenticar(string user, string pass)
        {
            double resultado;
            if (Utils.Interno)
                resultado = AutenticarInterno(user, pass);
            else
                resultado = AutenticarExterno(user, pass);

            return resultado;            
        }

        public static double AutenticarInterno(string user, string pass)
        {
            var WebService = new SecurityWebService(Utils.SecurityWebServiceURL, "127.0.0.1");

            var LogonResult = Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.INVALIDO_LOGIN;
            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(user, pass);

                short idEntidad;
                int idUsuario;

                if (WebService.ValidateUser(out idEntidad, out idUsuario, out LogonResult))
                {
                    var LocalSession = new Sesion();

                    var ConnectionStrings = Utils.getCadenasConexion(ref WebService);
                    Utils.ConnectionString = ConnectionStrings;

                    WebService.FillSession(ref LocalSession, Utils.AssemblyName);

                    switch (LogonResult)
                    {
                        case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.CAMBIAR_PASSWORD:
                            if (LocalSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                LocalSession.Usuario.Login = user;
                                LocalSession.Usuario.Password = pass;
                                Utils.MySession = LocalSession;
                                return 2;
                            }
                            return 4;
                        case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.VALIDO:
                            LocalSession.Usuario.Login = user;
                            LocalSession.Usuario.Password = pass;
                            Utils.MySession = LocalSession;
                            return Utils.MySession.Usuario.PerfilManager.Permisos.Count > 0 ? 1 : 4;
                    }
                }
                return 0;

            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["ErrorAutenticacion"] = ex.Message;
                switch (LogonResult)
                {
                    case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.FALTA_LOGIN:
                        return 5;
                    case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.INVALIDO_LOGIN:
                        return ex.Message.ToLower().Contains("no se encuentra activo") ? 3 : 6;
                    default:
                        return 0;
                }
            }
        }

        public static double AutenticarExterno(string user, string pass)
        {

            var hostname = Dns.GetHostName();
            IPHostEntry ipList = Dns.GetHostEntry(hostname);

            string ip = ipList.AddressList[ipList.AddressList.Length - 1].ToString();
            //var WebService = new SecurityDMZWebService(Utils.SecurityWebServiceURL, "127.0.0.1");

            var WebService = new SecurityDMZWebService(Utils.SecurityWebServiceURL, ip);
            

            var LogonResult = Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_LOGIN;
            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(user, pass);

                short idEntidad;
                int idUsuario;

                if (WebService.ValidateUser(out idEntidad, out idUsuario, out LogonResult))
                {
                    var LocalSession = new Sesion();

                    var ConnectionStrings = Utils.getCadenasConexion(ref WebService);
                    Utils.ConnectionString = ConnectionStrings;

                    WebService.FillSession(ref LocalSession, Utils.AssemblyName);

                    switch (LogonResult)
                    {
                        case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.CAMBIAR_PASSWORD:
                            if (LocalSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                LocalSession.Usuario.Login = user;
                                LocalSession.Usuario.Password = pass;
                                Utils.MySession = LocalSession;
                                return 2;
                            }
                            return 4;
                        case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.VALIDO:
                            LocalSession.Usuario.Login = user;
                            LocalSession.Usuario.Password = pass;
                            Utils.MySession = LocalSession;
                            return Utils.MySession.Usuario.PerfilManager.Permisos.Count > 0 ? 1 : 4;
                    }
                }
                return 0;

            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["ErrorAutenticacion"] = ex.Message;
                switch (LogonResult)
                {
                    case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.FALTA_LOGIN:
                        return 5;
                    case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_LOGIN:
                        return ex.Message.ToLower().Contains("no se encuentra activo") ? 3 : 6;
                    default:
                        return 0;
                }
            }
        }
    }
}
