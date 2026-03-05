using System.Configuration;
using System.Reflection;

namespace Miharu.Uploader.WebService
{
    static class Program
    {
        #region Propiedades

        internal static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length <= 0)
                    return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];

                return titleAttribute.Title != "" ? titleAttribute.Title : System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
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
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        internal static string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        internal static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        internal static string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        internal static string IdentifierDateFormat
        {
            get { return ConfigurationManager.AppSettings["IdentifierDateFormat"]; }
        }

        internal static short EntidadCliente
        {
            get { return short.Parse(ConfigurationManager.AppSettings["EntidadCliente"]); }
        }

        internal static short DependenciaCliente
        {
            get { return short.Parse(ConfigurationManager.AppSettings["DependenciaCliente"]); }
        }

        internal static short EsquemaSeguridad
        {
            get { return short.Parse(ConfigurationManager.AppSettings["EsquemaSeguridad"]); }
        }

        internal static short Proyecto
        {
            get { return short.Parse(ConfigurationManager.AppSettings["Proyecto"]); }
        }

        internal static short Esquema
        {
            get { return short.Parse(ConfigurationManager.AppSettings["Esquema"]); }
        }

        internal static string CoreConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Core"].ConnectionString; }
        }

        internal static string ImagingConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Imaging"].ConnectionString; }
        }

        internal static string StorageConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Storage"].ConnectionString; }
        }

        internal static string SecurityConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Security"].ConnectionString; }
        }

        internal static string IntegrationConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Integration"].ConnectionString; }
        }

        #endregion

    }
}