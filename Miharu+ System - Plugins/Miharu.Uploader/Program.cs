using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Miharu.FileSending.Library.Clases;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.WebService;
using Miharu.Uploader.Config;
using Miharu.Uploader.Forms;

namespace Miharu.Uploader
{
    static class Program
    {

        #region Declaraciones

        internal static UploaderConfig Config;
        internal static string[] Permisos;
        internal static int UserID;

        #endregion

        #region Propiedades

        public static Sesion MiharuSession;

        private static FileSendingConfig.TypeConnectionString _CadenasConexion;
        
        internal static string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length <= 0)
                    return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                // Select the first one
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                // If it is not an empty string, return it
                return titleAttribute.Title != "" ? titleAttribute.Title : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
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
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren//t any Description attributes, return an empty string
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
                // If there is a Description attribute, return its value
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
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren//t any Product attributes, return an empty string
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
                // If there is a Product attribute, return its value
            }
        }

        internal static string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren//t any Copyright attributes, return an empty string
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                // If there is a Copyright attribute, return its value
            }
        }

        internal static string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren//t any Company attributes, return an empty string
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
                // If there is a Company attribute, return its value
            }
        }

        internal static String AppPath
        {
            get { return Application.StartupPath + "\\"; }
        }

        internal static String AppDataPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd('\\') + "\\PYC\\" + AssemblyTitle + "\\"; }
        }

        internal static int ClientID
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["ClientID"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return int.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"ClientID\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static int SystemID
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SystemID"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return int.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"SystemID\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string FileServerIP
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["FileServerIP"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"FileServerIP\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string SecurityWebServiceURL
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SecurityWebServiceURL"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SecurityWebServiceURL\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static int FileServerPort
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["FileServerPort"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return int.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"FileServerPort\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string FileServerAppName
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["FileServerAppName"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"FileServerAppName\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static int PackageSize
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["PackageSize"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return int.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"PackageSize\" value=\"?\"/> al archivo App.config.");
            }
        }
        
        public static UploaderConfig UploaderConfigFile;

        internal static string SourceDirectory
        {
            get { return UploaderConfigFile.WorkingFolder + "Source\\"; }
        }
        
        internal static string TempDirectory
        {
            get { return UploaderConfigFile.WorkingFolder + "Temp\\"; }
        }

        public static short Proyecto
        {
            get;
            set;
        }

        public static short Esquema
        {
            get;            
            set;
        }

        public static short EntidadCliente
        {
            get;
            set;
        }

        public static short Reporte
        {
            get;
            set;
        }

        public static bool IsImage
        {
            get;
            set;
        }

        public static bool IsData
        {
            get;
            set;
        }

        public static byte[] PathBytes
        {
            get;
            set;
        }

        internal static short EntidadProcesamiento
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["EntidadProcesamiento"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"EntidadProcesamiento\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static short SedeProcesamiento
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SedeProcesamiento"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"SedeProcesamiento\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static short CentroProcesamiento
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["CentroProcesamiento"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return short.Parse(rootWebConfig);
                throw new Exception("Por favor asigne la cadena <add key=\"CentroProcesamiento\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static string UploaderWebServiceURL
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["UploaderWebServiceURL"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"UploaderWebServiceURL\" value=\"?\"/> al archivo App.config.");
            }
        }

        internal static FileSendingConfig.TypeConnectionString ConnectionStringList
        {
            get { return _CadenasConexion; }
            set { _CadenasConexion = value; }
        }

        internal static string IdentifierDateFormat
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["IdentifierDateFormat"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"IdentifierDateFormat\" value=\"?\"/> al archivo App.config.");
            }
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(true);

                var IP = GetClientIpAddress();

                var WebService = new SecurityDMZWebService(SecurityWebServiceURL, IP);         

                if (WebService.IsIPBloqueada())
                {
                    MessageBox.Show("La dirección IP local [" + WebService.ClientIPAddress +"] se encuentra bloqueada, por favor comuníquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                LoadConfig();
                //var MainForm = new FormMain();
                var LoginForm = new FormLogin();
                var continuar = true;
                var contador = 0;

                while (continuar)
                {
                    LoginForm.SelectText();
                    var respuesta = LoginForm.ShowDialog();

                    contador++;

                    if (respuesta == DialogResult.OK)
                    {
                        var InicioSesion = IniciarSesion(WebService, LoginForm.Login, LoginForm.Password);
                        if (InicioSesion == "OK")
                        {
                            if (MiharuSession.Usuario.isRoot || MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                LoginForm.Dispose();
                                continuar = false;
                            }
                            else
                            {
                                MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo",
                                    AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(InicioSesion, "Error de Inicio de Sesión", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (contador > 3 || respuesta != DialogResult.OK)
                            return;
                    }
                }
                var MainForm = new FormMain();
                MainForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Inicio de Sesión", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        public static string IniciarSesion(SecurityDMZWebService nWebService, string nUserName, string nPassword)
        {
            
            try
            {
                nWebService.CrearCanalSeguro();
                nWebService.setUser(nUserName, nPassword);

                short idEntidad;
                int idUsuario;
                Security.Library.SecurityDMZServiceReference.EnumValidateUser LogonResult;

                if (nWebService.ValidateUser(out idEntidad, out idUsuario, out LogonResult))
                {
                    MiharuSession = new Sesion();
                    var LocalSession = MiharuSession;
                    ConnectionStringList = getCadenasConexion(ref nWebService);

                    nWebService.FillSession(ref LocalSession, AssemblyName);

                    switch (LogonResult)
                    {
                        case Security.Library.SecurityDMZServiceReference.EnumValidateUser.CAMBIAR_PASSWORD:
                            if (!(MiharuSession.Usuario.PerfilManager.Permisos.Count > 0))
                            {
                                return "El usuario no cuenta con permisos para ingresar a este módulo";
                            }
                            LocalSession.Usuario.Login = nUserName;
                            LocalSession.Usuario.Password = nPassword;
                            MessageBox.Show("La contraseña ha vencido, por favor actualice la contraseña",
                                AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (ChangePassword(nPassword)) return "OK";
                            break;

                        case Security.Library.SecurityDMZServiceReference.EnumValidateUser.VALIDO:
                            if (!(MiharuSession.Usuario.PerfilManager.Permisos.Count > 0))
                            {
                                return "El usuario no cuenta con permisos para ingresar a este módulo";
                            }
                            break;

                        default:
                            return "Usuario o contraseña invalida";
                    }
                }
                else
                {
                    switch (LogonResult)
                    {
                        case Security.Library.SecurityDMZServiceReference.EnumValidateUser.FALTA_LOGIN:
                            return "Debe ingresar el usuario";
                        case Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_LOGIN:
                            return "Usuario o contraseña inválida";
                        case Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_PASSWORD:
                            return "Usuario o contraseña inválida";
                        case Security.Library.SecurityDMZServiceReference.EnumValidateUser.INACTIVO:
                            return "El usuario no se encuentra activo";
                        default:
                            return "Usuario o contraseña inválida";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error no identificado: " + ex.Message;
            }
            return "OK";
        }
        
        public static FileSendingConfig.TypeConnectionString getCadenasConexion(ref SecurityDMZWebService nWebService)
        {
            var Cadenas = new FileSendingConfig.TypeConnectionString();
            var ConnectionStrings = nWebService.getCadenasConexion();

            foreach (var typeModulo in ConnectionStrings)
            {
                switch (typeModulo.Id)
                {
                    case 0:
                        Cadenas.Security = typeModulo.ConnectionString;
                        break;
                    case 2:
                        Cadenas.Imaging = typeModulo.ConnectionString;
                        break;
                    case 3:
                        Cadenas.Archiving = typeModulo.ConnectionString;
                        break;
                    case 6:
                        Cadenas.Core = typeModulo.ConnectionString;
                        break;
                    case 26:
                        Cadenas.Integration = typeModulo.ConnectionString;
                        break;
                }
            }

            return Cadenas;
        }

        public static string GetClientIpAddress()
        {
            var LocalIP = "";
            for (var posicion = 0; posicion <= Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1; posicion++)
            {
                var tempIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[posicion].ToString();
                if (!IsValidIP(tempIP)) continue;
                LocalIP = tempIP;
                break;
            }
            return LocalIP;
        }

        private static bool IsValidIP(string nIP)
        {
            const string pattern =
                @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            var check = new Regex(pattern);

            return nIP != "" && check.IsMatch(nIP, 0);
        }

        private static bool ChangePassword(string nPassword)
        {
            var newPasswordForm = new FormNewPassword {OldPassword = nPassword};

            if (newPasswordForm.ShowDialog() != DialogResult.OK) return false;
            MiharuSession.Usuario.Password = newPasswordForm.NewPassword;
            return true;

        }

        internal static void LoadConfig()
        {
            if (File.Exists(AppDataPath + UploaderConfig.ConfigFileName))
            {
                
                UploaderConfigFile = UploaderConfig.Deserialize(AppDataPath);
                UploaderConfigFile.WorkingFolder = UploaderConfigFile.WorkingFolder.TrimEnd('\\') + "\\";
                Program.Config = UploaderConfigFile;
            }
            else
            {
                UploaderConfigFile = new UploaderConfig();                
                //UploaderConfigFile.WorkingFolder = UploaderConfigFile.WorkingFolder.TrimEnd('\\') + "\\";
                SaveConfig();
                Program.Config = new UploaderConfig();
            }
        }

        internal static void SaveConfig()
        {
            if (!Directory.Exists(AppDataPath))
                Directory.CreateDirectory(AppDataPath);

            //UploaderConfig.Serialize(UploaderConfigFile, AppDataPath);
            UploaderConfig.Serialize(Program.Config, AppDataPath);
        }


    }
}
