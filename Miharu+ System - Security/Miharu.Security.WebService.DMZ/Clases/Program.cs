using System.Configuration;
using System.Web;

namespace Miharu.Security.WebService.DMZ.Clases
{
    public class Program
    {
        #region Propiedades

        public static string IdentifierDateFormat
        {
            get { return ConfigurationManager.AppSettings["IdentifierDateFormat"]; }
        }

        public static string EncryptPassword
        {
            get { return ConfigurationManager.AppSettings["EncryptPassword"]; }

        }

        public static string SecurityConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Security"].ConnectionString; }

        }

        public static string ToolsConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Tools"].ConnectionString; }

        }

        public static string WebServiceSecurity
        {
            get { return ConfigurationManager.AppSettings["WebService.SecurityService"]; }
        }

        #endregion

        #region Funciones

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

        #endregion

        public class LDAP
        {
            public static string Validar
            {
                get { return ConfigurationManager.AppSettings["LDAP.Validar"]; }
            }

            public static string ServerPath
            {
                get { return ConfigurationManager.AppSettings["LDAP.ServerPath"]; }
            }

            public static short Entidad
            {
                get { return short.Parse(ConfigurationManager.AppSettings["LDAP.Entidad"]); }
            }

            public static short Dependencia
            {
                get { return short.Parse(ConfigurationManager.AppSettings["LDAP.Dependencia"]); }
            }

            public static short Esquema
            {
                get { return short.Parse(ConfigurationManager.AppSettings["LDAP.Esquema"]); }
            }

        }
    }
}
