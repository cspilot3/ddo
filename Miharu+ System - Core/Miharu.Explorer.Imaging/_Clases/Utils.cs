using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.WebService;

namespace Miharu.Explorer.Imaging._Clases
{
    public class Utils : System.Web.UI.MasterPage
    {
        public enum MsgBoxIcon : byte
        {
            IconInformation = 1,
            IconWarning = 2,
            IconError = 3,
        }

        public const string IconInformation = "MB_information";
        public const string IconWarning = "MB_warning";
        public const string IconError = "MB_error";

        public struct TypeConnectionString
        {
            public string Security;
            public string Core;
            public string Archiving;
        }

        public struct urlValida
        { 
            public string URLBusqueda;
            public string URLFaltanteLogico;
            public string URLMultiactiva;
            public string URLCobroJuridico;
            public string URLNovedadesEnMesa;
            public string URLSolicitudes;
            public string URLPreCaptura;
        }


        public static string SecurityWebServiceURL = WebConfigurationManager.AppSettings["WebService.SecurityService"];

        public static string ImagingWebServiceURL = WebConfigurationManager.AppSettings["WebService.ImagingService"];

        public static string VisorURL = WebConfigurationManager.AppSettings["WebService.Visor"];

        public static string ForgottenPasswordURL = WebConfigurationManager.AppSettings["WebService.ForgottenPassword"];

        public static string AssemblyName = "Miharu.Explorer.Imaging";

        public static urlValida getUrl()
        {
            var url = new urlValida();
            try
            {
                //switch (MySession.Entidad.id)
                //{
                //    case 2:
                //        url.URLBusqueda = "../_Site/Garantias/Bancoomeva/Consulta_Bancoomeva.aspx";
                //        url.URLSolicitudes = "../_Site/Garantias/Solicitudes.aspx";
                //        url.URLFaltanteLogico = "../_Site/Garantias/Bancoomeva/FaltanteLogico.aspx";
                //        url.URLMultiactiva = "../_Site/Garantias/Bancoomeva/Reporte_Multiactiva.aspx";
                //        url.URLCobroJuridico = "../_Site/Garantias/Bancoomeva/Reporte_CobroJuridico.aspx";
                //        url.URLNovedadesEnMesa = "../_Site/Garantias/Bancoomeva/Reporte_NovedadesMesa.aspx";

                //        break;

                //    default:
                        url.URLBusqueda = "../_Site/Garantias/Consulta.aspx";
                        url.URLSolicitudes = "../_Site/Garantias/Solicitudes.aspx";
                        url.URLFaltanteLogico = "";

                        url.URLPreCaptura = "../Captura/WebFormIndexerView.aspx";

                        //break;

                //}

                return url;
            }
            catch
            {
                return url;
            }
        }


        public static TypeConnectionString getCadenasConexion(ref SecurityWebService nWebService)
        {
            var Cadenas = new TypeConnectionString();
            var ConnectionStrings = nWebService.getCadenasConexion();

            foreach (TypeModulo Modulo in ConnectionStrings)
            {
                switch (Modulo.Id)
                {
                    case 0:
                        Cadenas.Security = Modulo.ConnectionString;
                        break;
                    case 6:
                        Cadenas.Core = Modulo.ConnectionString;
                        break;
                    case 7:
                        Cadenas.Archiving = Modulo.ConnectionString;
                        break;
                }
            }

            return Cadenas;
        }

        public static TypeConnectionString getCadenasConexion(ref SecurityDMZWebService nWebService)
        {
            var Cadenas = new TypeConnectionString();
            var ConnectionStrings = nWebService.getCadenasConexion();

            foreach (TypeModulo Modulo in ConnectionStrings)
            {
                switch (Modulo.Id)
                {
                    case 0:
                        Cadenas.Security = Modulo.ConnectionString + Remoting;
                        break;
                    case 6:
                        Cadenas.Core = Modulo.ConnectionString + Remoting;
                        break;
                    case 7:
                        Cadenas.Archiving = Modulo.ConnectionString + Remoting;
                        break;
                }
            }

            return Cadenas;
        }

        public static Sesion MySession
        {
            get { return (Sesion) HttpContext.Current.Session["Sesion"]; }
            set { HttpContext.Current.Session["Sesion"]= value; }
        }

        public static bool IsValidSession()
        {
            return HttpContext.Current.Session["Sesion"] != null;
        }

        public void SesionCaduco()
        {
            HttpContext.Current.Session["Sesion"] = null;
            Response.Redirect("Login.aspx");
        }

        public static TypeConnectionString ConnectionString
        {
            get { return (TypeConnectionString) HttpContext.Current.Session["ConnectionStrings"]; }
            set { HttpContext.Current.Session["ConnectionStrings"] = value; }
        }

        public static string IdentifierDateFormat = WebConfigurationManager.AppSettings["IdentifierDateFormat"];

        public static List<MenuStruct> MyMenu()
        {
            var A = new List<MenuStruct>();
            urlValida url = getUrl();

            

                MenuStruct A1;
                A1.Image = "~/_Images/Menu/Garantias.png";
                A1.Parent = "";
                A1.Text = "Garantias";
                A1.URL = "";
                A1.Value = "A1";
                A.Add(A1);

                MenuStruct A3;
                A3.Image = "~/_Images/Menu/Captura.png";
                A3.Parent = "";
                A3.Text = "Captura";
                A3.URL = "";
                A3.Value = "A3";
                A.Add(A3);

                MenuStruct A31;
                A31.Image = "~/_Images/Menu/PreCaptura.png";
                A31.Parent = "A3";
                A31.Text = "Pre-Captura";
                A31.URL = url.URLPreCaptura;
                A31.Value = "A31";
                A.Add(A31);

                MenuStruct A11;
                A11.Image = "~/_Images/Menu/Buscar.png";
                A11.Parent = "A1";
                A11.Text = "Buscar";
                A11.URL = url.URLBusqueda;
                A11.Value = "A11";
                A.Add(A11);

                MenuStruct A12;
                A12.Image = "~/_Images/Menu/Garantias.png";
                A12.Parent = "A1";
                A12.Text = "Solicitudes Masivas";
                A12.URL = url.URLSolicitudes;
                A12.Value = "A12";
                A.Add(A12);

                if (url.URLFaltanteLogico != "") //Validacion menu faltantes logicos
                {
                    MenuStruct A2;
                    A2.Image = "~/_Images/Menu/Reportes.png";
                    A2.Parent = "";
                    A2.Text = "Reportes";
                    A2.URL = "";
                    A2.Value = "A2";
                    A.Add(A2);
               
                    MenuStruct A21;
                    A21.Image = "~/_Images/Menu/Faltantes.png";
                    A21.Parent = "A2";
                    A21.Text = "Faltantes";
                    A21.URL = url.URLFaltanteLogico;
                    A21.Value = "";
                    A.Add(A21);

                    MenuStruct A22;
                    A22.Image = "~/_Images/Menu/Rpt_Multiactiva.png";
                    A22.Parent = "A2";
                    A22.Text = "Multiactiva";
                    A22.URL = url.URLMultiactiva;
                    A22.Value = "";
                    A.Add(A22);

                    MenuStruct A23;
                    A23.Image = "~/_Images/Menu/Rpt_CobroJuridico.png";
                    A23.Parent = "A2";
                    A23.Text = "Cobro_Jurídico";
                    A23.URL = url.URLCobroJuridico;
                    A23.Value = "";
                    A.Add(A23);

                    MenuStruct A24;
                    A24.Image = "~/_Images/Menu/Rpt_NovedadesMesa.png";
                    A24.Parent = "A2";
                    A24.Text = "Novedades en Mesa";
                    A24.URL = url.URLNovedadesEnMesa;
                    A24.Value = "";
                    A.Add(A24);

                }            

            //MenuStruct d;
            //d.Image = "~/_Images/Menu/Settings.png";
            //d.Parent = "";
            //d.Text = "Parametrizar";
            //d.URL = "";
            //d.Value = "3";
            //b.Add(d);

            //MenuStruct e;
            //e.Image = "~/_Images/Menu/Project.png";
            //e.Parent = "3";
            //e.Text = "Proyecto";
            //e.URL = "../_Site/Garantias_Settings/Administracion/Proyecto.aspx";
            //e.Value = "4";
            //b.Add(e);

            return A;
        }

        public static bool Interno = WebConfigurationManager.AppSettings["Interno"] == "1";

        public static string Remoting = WebConfigurationManager.AppSettings["Remoting"];
    }
}
