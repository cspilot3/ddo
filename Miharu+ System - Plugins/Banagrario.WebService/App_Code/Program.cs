using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

namespace Banagrario.WebService
{
    static class Program
    {
        #region Propiedades

        internal static string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
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
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren//t any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        internal static string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren//t any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        internal static string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren//t any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        internal static string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren//t any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
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

        internal static short EntidadProcesamiento
        {
            get { return short.Parse(ConfigurationManager.AppSettings["EntidadProcesamiento"]); }
        }

        internal static short SedeProcesamiento
        {
            get { return short.Parse(ConfigurationManager.AppSettings["SedeProcesamiento"]); }
        }

        internal static short CentroProcesamiento
        {
            get { return short.Parse(ConfigurationManager.AppSettings["CentroProcesamiento"]); }
        }

        internal static string BanagrarioConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Banagrario"].ConnectionString; }
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

        #endregion
    }
}