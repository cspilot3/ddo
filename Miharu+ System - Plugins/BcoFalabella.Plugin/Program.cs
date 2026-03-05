using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Library.Plugins;
using System.Configuration;
using System.Reflection;

namespace BcoFalabella.Plugin
{
    static class Program
    {
        #region "Declaraciones"
        private static Miharu.Security.Library.Session.Sesion _sesion;
        private static DesktopGlobal _desktopGlobal;
        private static RiskGlobal _riskGlobal;

        private static ImagingGlobal _imagingGlobal;
        private static DesktopPluginManager _pluginManager;
        #endregion

        #region "Propiedades"
        static internal string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                // If there is at least one Title attribute
                if ((attributes.Length > 0))
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];

                    // If it is not an empty string, return it
                    if ((!string.IsNullOrEmpty(titleAttribute.Title)))
                        return titleAttribute.Title;

                }

                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        static internal string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        static internal string AssemblyName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }
        static internal string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                // If there aren't any Description attributes, return an empty string
                if ((attributes.Length == 0))
                    return "";

                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        static internal string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                // If there aren't any Product attributes, return an empty string
                if ((attributes.Length == 0))
                    return "";

                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        static internal string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                // If there aren't any Copyright attributes, return an empty string
                if ((attributes.Length == 0))
                    return "";

                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        static internal string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

                // If there aren't any Company attributes, return an empty string
                if ((attributes.Length == 0))
                    return "";

                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        static internal string CrLf
        {
            get { return Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString(); }
        }
        static internal string AppPath
        {
            get { return System.Windows.Forms.Application.StartupPath.TrimEnd('\\') + "\\"; }
        }

        static internal Miharu.Security.Library.Session.Sesion Sesion
        {
            get { return _sesion; }
            set { _sesion = value; }
        }
        static internal DesktopGlobal DesktopGlobal
        {
            get { return _desktopGlobal; }
            set { _desktopGlobal = value; }
        }
        public static RiskGlobal RiskGlobal
        {
            get { return _riskGlobal; }
            set { _riskGlobal = value; }
        }

        public static ImagingGlobal ImagingGlobal
        {
            get { return _imagingGlobal; }
            set { _imagingGlobal = value; }
        }

        public static string TempPath
        {
            get { return "temp\\"; }
        }

        public static DesktopPluginManager PluginManager
        {
            get { return _pluginManager; }
            set { _pluginManager = value; }
        }

        public static string AccesoDesktopAssembly { get { return "Miharu.Desktop"; } }


        #endregion
    }
}
