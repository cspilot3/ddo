using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;
using System.Configuration;
using System.Security.Permissions;
using Banagrario.Scanning.Config;
using System.IO;
using Banagrario.Library.WebService;
using Banagrario.Scanning.Utils;

namespace Banagrario.Scanning
{
    static class Program
    {
        #region Declaraciones

        internal static BanagrarioScanningConfig Config;
        internal static string[] Permisos;
        internal static int UserID;
       
        #endregion

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
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
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

        internal static String AppPath
        {
            get { return System.Windows.Forms.Application.StartupPath + "\\"; }
        }

        internal static String AppDataPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd('\\') + "\\SLYG\\" + Program.AssemblyTitle + "\\"; }
        }

        internal static string BanagrarioWebServiceURL
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["WebService.BanagrarioService"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return rootWebConfig;
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"WebService.BanagrarioService\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string FileServerIP
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["FileServerIP"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return rootWebConfig;
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"FileServerIP\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static int FileServerPort
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["FileServerPort"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return int.Parse(rootWebConfig);
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"FileServerPort\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string FileServerAppName
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["FileServerAppName"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return rootWebConfig;
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"FileServerAppName\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static int PackageSize
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["PackageSize"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return int.Parse(rootWebConfig);
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"PackageSize\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string DefinitionDirectory
        {
            get { return Config.WorkingFolder + "Definition\\"; }
        }

        internal static string SourceDirectory
        {
            get { return Config.WorkingFolder + "Source\\"; }
        }

        internal static short EntidadCliente
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["EntidadCliente"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return short.Parse(rootWebConfig);
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"EntidadCliente\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static short Proyecto
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["Proyecto"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return short.Parse(rootWebConfig);
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"Proyecto\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static short Esquema
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["Esquema"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return short.Parse(rootWebConfig);
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"Esquema\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static short EntidadProcesamiento
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["EntidadProcesamiento"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return short.Parse(rootWebConfig);
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"EntidadProcesamiento\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static short SedeProcesamiento
        {
            get
            {
                BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

                string rootWebConfig = ServicioWeb.SedeProcesamiento();
                //string rootWebConfig = ConfigurationManager.AppSettings["SedeProcesamiento"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return short.Parse(rootWebConfig);
                else
                    //throw new Exception("Por favor asigne la cadena <add key=\"SedeProcesamiento\" value=\"?\"/> al archivo App.config.");
                throw new Exception("No hay una Sede asignada a las OficinasTipoB");
            }
        }

        internal static short CentroProcesamiento
        {
            get
            {
                BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);
                string rootWebConfig = ServicioWeb.CentroProcesamiento();

                //string rootWebConfig = ConfigurationManager.AppSettings["CentroProcesamiento"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return short.Parse(rootWebConfig);
                else
                    //throw new Exception("Por favor asigne la cadena <add key=\"CentroProcesamiento\" value=\"?\"/> al archivo App.config.");
                throw new Exception("No hay un Centro asociado");
            }
        }

        //internal static string SentDirectory
        //{
        //    get { return Config.WorkingFolder + "Sent\\"; }
        //}

        //internal static string LogDirectory
        //{
        //    get { return Config.WorkingFolder + "Log\\"; }
        //}

        internal static string TempDirectory
        {
            get { return Config.WorkingFolder + "Temp\\"; }
        }

        internal static string UserName { get; set; }

        #endregion

        #region Metodos

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                ValidarUsuario();

                LoadConfig();

                if (Config.OfficeID == -1)
                {
                    if (Program.PuedeAcceder("1.3"))
                    {
                        Forms.FormConfig ConfigForm = new Forms.FormConfig();

                        ConfigForm.TempPath = Config.WorkingFolder;
                        ConfigForm.OfficeID = Config.OfficeID;
                        ConfigForm.OfficeName = ConfigForm.OfficeName;

                        DialogResult Respuesta = ConfigForm.ShowDialog();

                        if (Respuesta == DialogResult.OK)
                        {
                            Config.WorkingFolder = ConfigForm.TempPath.TrimEnd('\\') + "\\";
                            Config.OfficeID = ConfigForm.OfficeID;
                            Config.OfficeName = ConfigForm.OfficeName;

                            SaveConfig();
                        }
                        else
                        {
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        throw new Exception("No se encuentra configurada la oficina y el usuario no cuenta con permisos para realizar esta operación");
                    }
                }

                Application.Run(new Forms.FormMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ValidarUsuario()
        {
            var Parameters = ClickOnceUtil.GetQueryStringParameters();

            if (Parameters.Count == 0)
            {
                MessageBox.Show("No se encontraron parámetros de inicio, se usará el usuario de Windows para validarse en el sistema", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Program.UserName = Environment.UserName;
            }
            else
            {
                Program.UserName = Parameters.Get("User");
            }

            BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

            Permisos = ServicioWeb.getPermisos(Program.UserName, out UserID);
            
            //    throw new Exception("No se encontraron parámetros de incio");            
        }

        internal static void LoadConfig()
        {
            if (File.Exists(Program.AppDataPath + BanagrarioScanningConfig.ConfigFileName))
            {
                Config = BanagrarioScanningConfig.Deserialize(Program.AppDataPath);
                Config.WorkingFolder = Config.WorkingFolder.TrimEnd('\\') + "\\";
            }
            else
            {
                Program.Config = new BanagrarioScanningConfig();
                Config.WorkingFolder = Config.WorkingFolder.TrimEnd('\\') + "\\";
                SaveConfig();
            }
        }

        internal static void SaveConfig()
        {
            if (!Directory.Exists(Program.AppDataPath))
                Directory.CreateDirectory(Program.AppDataPath);

            BanagrarioScanningConfig.Serialize(Program.Config, Program.AppDataPath);
        }

        internal static bool PuedeAcceder(string nAcceso)
        {
            foreach (string Permiso in Permisos)
            {
                if (ValidarPermiso(nAcceso, Permiso))
                    return true;
            }

            return false;
        }

        private static bool ValidarPermiso(string nAcceso, string nPermiso)
        {
            if (nPermiso == "*")
            {
                return true;
            }
            else if (nAcceso == nPermiso)
            {
                return true;
            }
            else if (nPermiso.StartsWith(nAcceso))
            {
                return true;
            }
            else if (nPermiso.EndsWith(".*"))
            {
                string RaizPermiso = nPermiso.TrimEnd('*').TrimEnd('.');

                return nAcceso.StartsWith(RaizPermiso);
            }
            else
            {
                return false;
            }
        }
        
        //private static void GetCentroProcesamiento()
        //{
        //    BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

        //    Centro = ServicioWeb.CentroProcesamiento();
                      
        //}
        
        //private static void GetSedeProcesamiento()
        //{         
        //        BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

        //        Sede = ServicioWeb.SedeProcesamiento();
            
        // }
        

        #endregion
    }
}