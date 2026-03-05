using System;
using System.IO;
using System.Web;
using System.Reflection;
using System.Configuration;

namespace Miharu.Client.Browser.code
{
    public delegate void DaughterClose_Delegate();

    public enum MsgBoxIcon : byte
    {
        IconInformation = 1,
        IconWarning = 2,
        IconError = 3,
    }

    public class ImageData
    {
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
    }

    public class FileData
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }

        public FileData()
        {
            this.ContentType = "application/octet-stream";
        }
    }

    public class TypeConnectionString
    {
        public string Security;
        public string Ripley;
        public string Core;
        public string Imaging;
    }

    public class Program
    {
        #region Mensajes

        public const string IconInformation = "MB_information";
        public const string IconWarning = "MB_warning";
        public const string IconError = "MB_error";

        #endregion

        #region Declaraciones

        public const string DateFormat = "{0:yyyy/MM/dd}";

        #endregion

        #region Propiedades

        internal static string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        internal static string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        internal static string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
                // If there aren//t any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        internal static string AssemblyName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        internal static string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                // If there aren//t any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        internal static string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                // If there aren//t any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        internal static string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
                // If there aren//t any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        public static string SecurityWebServiceURL
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["SecurityWebServiceURL"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"SecurityWebServiceURL\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string IdentifierDateFormat
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["IdentifierDateFormat"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"IdentifierDateFormat\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static int SessionTimeOut
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SessionTimeOut"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return int.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"SessionTimeOut\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static int SessionScan
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SessionScan"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return int.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"SessionScan\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static short idModulo
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["idModulo"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"idModulo\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static short idCliente
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["idCliente"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"idCliente\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static short idProcesador
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["idProcesador"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"idProcesador\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static short idProyecto
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["idProyecto"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"idProyecto\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static bool ForgottenPassword
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["ForgottenPassword"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return bool.Parse(rootWebConfig);

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"ForgottenPassword\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string URLVisorImagen
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["URLVisorImagen"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception(
                    "Por favor asigne la cadena <add key=\"URLVisorImagen\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string URLVisorImagenInterno
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["URLVisorImagenInterno"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception(
                    "Por favor asigne la cadena <add key=\"URLVisorImagenInterno\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string URLVisorImagenExterno
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["URLVisorImagenExterno"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception(
                    "Por favor asigne la cadena <add key=\"URLVisorImagenExterno\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string LocalServerURL
        {
            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).TrimEnd('/') + "/" +
                       (HttpContext.Current.Request.ApplicationPath.TrimEnd('/').TrimStart('/') == ""
                            ? ""
                            : HttpContext.Current.Request.ApplicationPath.TrimEnd('/').TrimStart('/') + "/");
            }
        }

        public static string Remoting
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["Remoting"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;

                throw new Exception(
                    "Por favor asigne la cadena <add key=\"Remoting\" value=\"?\"/> al archivo Web.config.");
            }
        }
        #endregion

        #region Metodos

        public static void TraceError(Exception ex)
        {
            System.Diagnostics.Debug.Write(ex.StackTrace);
        }

        #endregion

        #region Funciones

        public static string SetYesNoValue(bool nValue)
        {
            return nValue ? "yes" : "no";
        }

        public static DateTime ParseDateTime(string nDateString)
        {
            try
            {
                var deli = (nDateString.IndexOf("-", System.StringComparison.Ordinal) > 0) ? '-' : '/';
                var parts = nDateString.Split(deli);
                return new DateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible leer la fecha como una fecha valida  [" + nDateString + "], " +
                                    ex.Message);
            }
        }

        public static Decimal ParseDecimal(string nDecimalString)
        {
            try
            {
                return
                    decimal.Parse(nDecimalString.Replace(",", "")
                                                .Replace(".", Slyg.Tools.DataConvert.GetPuntoFlotante()));
            }
            catch (System.Exception ex)
            {
                throw new Exception("No fue posible leer el campo decimal [" + nDecimalString + "], " + ex.Message);
            }
        }

        public static string getIPName()
        {
            // Guardar la IP del visitante 
            //El visitante puede acceder por proxy, entonces tomo la IP que lo está utilizando 
            string IPName = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //Si no venía de un proxy, tomo la ip del visitante 
            if (string.IsNullOrEmpty(IPName))
                IPName = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return IPName;
        }

        public static TypeConnectionString getCadenasConexion(
            ref Miharu.Security.Library.WebService.SecurityWebService nWebService)
        {
            var Cadenas = new TypeConnectionString();
            var ConnectionStrings = nWebService.getCadenasConexion();

            foreach (var modulo in ConnectionStrings)
            {
                switch (modulo.Id)
                {
                    case 0:
                        Cadenas.Security = modulo.ConnectionString + Remoting;
                        break;
                    case 2:
                        Cadenas.Imaging = modulo.ConnectionString + Remoting;
                        break;
                    case 6:
                        Cadenas.Core = modulo.ConnectionString + Remoting;
                        break;
                    case 23:
                        Cadenas.Ripley = modulo.ConnectionString + Remoting;
                        break;
                }
            }

            return Cadenas;
        }

        #endregion        
    }
}