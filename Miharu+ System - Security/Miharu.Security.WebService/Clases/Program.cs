using System.Configuration;

namespace Miharu.Security.WebService.Clases
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
