using System;
using System.IO;
using System.Web;
using System.Reflection;
using System.Configuration;

namespace WebPunteoElectronico.Clases
{
    public delegate void DaughterClose_Delegate();

    public enum MsgBoxIcon : byte
    {
        IconInformation = 1,
        IconWarning = 2,
        IconError = 3,
    }

    public enum EnumModo
    {
        Busqueda,
        Detalle,
        Proceso,
        Autorizacion,
        Envio
    }

    public class TypeConnectionString
    {
        public string Security;
        public string BanAgrario;
        public string SMS;
        public string Core;
        public string Imaging;
        public string Workflow;
    }

    public class Program
    {
        #region Declaraciones

        #region Mensajes

        public const string IconInformation = "MB_information";
        public const string IconWarning = "MB_warning";
        public const string IconError = "MB_error";

        #endregion

        public const short Modulo = 13;

        public const short EntidadId = 9;

        public const short ProyectoId = 2;

        public const short EsquemaId = 1;

        public static ConsultaCamposOcultos ConsultaCamposOcultos = new ConsultaCamposOcultos();

        public const string DateFormat = "{0:yyyy/MM/dd}";

        #region Permisos



        #region Consultas

        public const string Permiso_Consultas = "1";


        #endregion

        #region Oficinas tipo B

        public const string Permiso_Oficinas_tipo_B = "2";
        public const string Permiso_Oficinas_tipo_B_Proceso = "2.1";
        public const string Permiso_Oficinas_tipo_B_Reprocesos = "2.2";
        public const string Permiso_Oficinas_tipo_B_Configuración = "2.3";


        #endregion

        #region Reportes

        public const string Permiso_Reporte = "3";
        public const string Permiso_Reporte_Digitalización = "3.1";
        public const string Permiso_Reporte_Dig_Contenedores_Oficinas = "3.1.1";
        public const string Permiso_Reporte_Dig_Diferencias_Documentos = "3.1.2";
        public const string Permiso_Reporte_Dig_Documentos_Sin_Ordenamiento = "3.1.3";
        public const string Permiso_Reporte_Dig_Transacciones_Sin_Anexos = "3.1.4";
        public const string Permiso_Reporte_Dig_Novedades_en_validaciones = "3.1.5";
        public const string Permiso_Reporte_Dig_Consolidado_Novedades_Validaciones = "3.1.6";
        public const string Permiso_Reporte_Dig_Oficinas_Pendientes_Transmision = "3.1.7";
        public const string Permiso_Reporte_Dig_Oficinas_Pendientes_Proceso = "3.1.8";
        public const string Permiso_Reporte_Dig_Oficinas_pendientes_de_cierre = "3.1.9";
        public const string Permiso_Reporte_Dig_Tx_Desmat_con_Soporte_Fisico = "3.1.10";
        public const string Permiso_Reporte_Cruce = "3.2";
        public const string Permiso_Reporte_Cruce_Soportes_Sobrantes = "3.2.1";
        public const string Permiso_Reporte_Cruce_Registros_sobrantes = "3.2.2";
        public const string Permiso_Reporte_Cruce_Resultado_cruce_automático = "3.2.3";
        public const string Permiso_Reporte_Cruce_Transacciones_no_Identificadas = "3.2.4";
        public const string Permiso_Reporte_Cruce_Transacciones_con_cruce_exitoso = "3.2.5";
        public const string Permiso_Reporte_Cruce_Documentos_No_Identificadas = "3.2.6";
        public const string Permiso_Reporte_Cruce_Resultado_del_cierre = "3.2.7";
        public const string Permiso_Reporte_Cruce_Consolidado_Cruce_Automático = "3.2.8";
        public const string Permiso_Reporte_Cruce_Transacciones_357 = "3.2.9";
        public const string Permiso_Reporte_Cruce_Transacciones_Mensuales = "3.2.10";
        public const string Permiso_Reporte_Cruce_Transacciones_excluidas_del_cruce = "3.2.11";
        public const string Permiso_Reporte_Cruce_Transacciones_reversadas_en_log = "3.2.12";
        public const string Permiso_Reporte_Cruce_Consolidado_medios_de_pago = "3.2.13";
        public const string Permiso_Reporte_Devolucion_Canje_Recibido = "3.2.14";
        public const string Permiso_Facturacion = "2.1.8";

        #endregion

        #region Ajustes

        public const string Permiso_Ajustes = "4";
        public const string Permiso_Ajuste_Soportes_Sobrantes = "4.1";
        public const string Permiso_Ajustes_Registros_Sobrantes = "4.2";
        public const string Permiso_Ajustes_Diferencia_Valor = "4.3";
        public const string Permiso_Ajustes_Diferencia_Medios_Pago = "4.4";
        public const string Permiso_Ajustes_Diferencia_Producto = "4.5";
        public const string Permiso_Ajuste_Soportes_Sobrantes_Ajustar = "4.1.1";
        public const string Permiso_Ajuste_Soportes_Sobrantes_Aprobar = "4.1.2";
        public const string Permiso_Ajuste_Soportes_Sobrantes_Reabrir = "4.1.3";
        public const string Permiso_Ajustes_Registros_Sobrantes_Ajustar = "4.2.1";
        public const string Permiso_Ajustes_Registros_Sobrantes_Aprobar = "4.2.2";
        public const string Permiso_Ajustes_Registros_Sobrantes_Reabrir = "4.2.3";
        public const string Permiso_Ajustes_Diferencia_Valor_Ajustar = "4.3.1";
        public const string Permiso_Ajustes_Diferencia_Valor_Aprobar = "4.3.2";
        public const string Permiso_Ajustes_Diferencia_Valor_Reabrir = "4.3.3";
        public const string Permiso_Ajustes_Diferencia_Medios_Pago_Ajustar = "4.4.1";
        public const string Permiso_Ajustes_Diferencia_Medios_Pago_Aprobar = "4.4.2";
        public const string Permiso_Ajustes_Diferencia_Medios_Pago_Reabrir = "4.4.3";
        public const string Permiso_Ajustes_Diferencia_Producto_Ajustar = "4.5.1";
        public const string Permiso_Ajustes_Diferencia_Producto_Aprobar = "4.5.2";
        public const string Permiso_Ajustes_Diferencia_Producto_Reabrir = "4.5.3";

        #endregion

        #region Gerencial

        public const string Permiso_Reporte_Gerencial = "5";


        #endregion

        #region Servicio

        public const string Permiso_Servicio_Cliente = "6";
        public const string Permiso_Servicio_Cliente_Registrar_PQR = "6.1";
        public const string Permiso_Servicio_Cliente_Consultar_PQRs = "6.2";


        #endregion

        #region Estadisticos

        public const string Permiso_Estadisiticos = "7";
        public const string Permiso_Reporte_Estad_Digitalizacion = "7.1";
        public const string Permiso_Reporte_Estad_Cruce = "7.2";
        public const string Permiso_Reporte_Estad_Consolidados = "7.3";
        public const string Permiso_Reporte_Estad_Ajustes = "7.4";
        public const string Permiso_Reporte_Estad_Digit_Contenedores_Oficinas = "7.1.1";
        public const string Permiso_Reporte_Estad_Digit_Diferencias_Documentos = "7.1.2";
        public const string Permiso_Reporte_Estad_Digit_Documentos_Sin_Ordenamiento = "7.1.3";
        public const string Permiso_Reporte_Estad_Digit_Soportes_Inconsistencias = "7.1.4";
        public const string Permiso_Reporte_Estad_Digit_Oficinas_Pend_Transmicion = "7.1.5";
        public const string Permiso_Reporte_Estad_Digit_Oficinas_Pendientes_Proceso = "7.1.6";
        public const string Permiso_Reporte_Estad_Digit_Tx_Desmat_Soporte_Fisico = "7.1.7";
        public const string Permiso_Reporte_Estad_Digit_Novedades_en_validaciones = "7.1.8";
        public const string Permiso_Reporte_Estad_Cruc_Soportes_Sobrantes = "7.2.1";
        public const string Permiso_Reporte_Estad_Cruc_Registros_sobrantes = "7.2.2";
        public const string Permiso_Reporte_Estad_Cruc_Resultado_cruce_automático = "7.2.3";
        public const string Permiso_Reporte_Estad_Cruc_Tx_no_Identificadas = "7.2.4";
        public const string Permiso_Reporte_Estad_Cruc_Transacciones_con_Cruce_Exitoso = "7.2.5";
        public const string Permiso_Reporte_Estad_Cruc_Transacciones_Excl_del_cruce = "7.2.6";
        public const string Permiso_Reporte_Estad_Cruc_Transacciones_reversadas_en_log = "7.2.7";
        public const string Permiso_Reporte_Estad_Cruc_Documentos_No_Identificadas = "7.2.8";
        public const string Permiso_Reporte_Estad_Cruc_Oficinas_pendientes_de_cierre = "7.2.9";
        public const string Permiso_Reporte_Estad_Cruc_Transacciones_357 = "7.2.10";
        public const string Permiso_Reporte_Estad_Consol_Consol_Novedades_Validaciones = "7.3.1";
        public const string Permiso_Reporte_Estad_Consol_Transacciones_mensuales = "7.3.2";
        public const string Permiso_Reporte_Estad_Consol_Consold_Cruce_Automático = "7.3.3";
        public const string Permiso_Reporte_Estad_Consol_Consolidado_Total = "7.3.4";
        public const string Permiso_Reporte_Estad_Ajuste_Soportes_Sobrantes = "7.4.1";
        public const string Permiso_Reporte_Estad_Ajuste_Registros_sobrantes = "7.4.2";
        public const string Permiso_Reporte_Estad_Ajuste_Resultado_cruce_automático = "7.4.3";

        #endregion
        
        #region Autorizaciones

        public const string Permiso_Autorizacion = "8";
        public const string Permiso_Autorizacion_Modificar_precinto = "8.1";
        public const string Permiso_Autorizacion_Eliminar_precinto = "8.2";
        public const string Permiso_Autorizacion_Modificar_contenedor = "8.3";
        public const string Permiso_Autorizacion_Eliminar_contenedor = "8.4";
        public const string Permiso_Autorizacion_Perfil_Nacional = "8.5";
        public const string Permiso_Autorizacion_Empacar_en_caja_cerrada = "8.6";
        public const string Permiso_Autorizacion_Agregar_Conten_en_precinto_destapado = "8.7";
        public const string Permiso_Autorizacion_Acceso_empacar_Conten_cant_diferente = "8.8";

        #endregion

        #region Firmas

        public const string Permiso_Firmas = "10";
        
        public const string Permiso_Firmas_Reportes = "10.1";

        public const string Permiso_Firmas_Reportes_Gestion_Tarjetas_Firmas_Faltantes = "10.1.1";
        public const string Permiso_Firmas_Reportes_Control_Envio_Oficina = "10.1.2";
        public const string Permiso_Firmas_Reportes_Soportes_Sobrantes = "10.1.3";
        public const string Permiso_Firmas_Reportes_Cruce_Exitoso = "10.1.4";
        public const string Permiso_Firmas_Reportes_Transacciones_Excluidas_Cruce = "10.1.5";
        public const string Permiso_Firmas_Reportes_Transacciones_Mensuales = "10.1.6";
        public const string Permiso_Firmas_Reportes_Tarjetas_Firmas_Rechazadas = "10.1.7";
        public const string Permiso_Firmas_Reportes_Resultado_Cierre = "10.1.8";
        public const string Permiso_Firmas_Reportes_Documentos_No_Identificados = "10.1.9";

        public const string Permiso_Firmas_Consultas = "10.2";
        public const string Permiso_Firmas_Ajustes = "10.3";

        #endregion

        #endregion

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
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
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
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren//t any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
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

        public static string SecurityWebServiceURL
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["WebService.SecurityService"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"WebService.SecurityService\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string VisorWorkflowUrl
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["VisorWorkflowUrl"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"VisorWorkflowUrl\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string BanagrarioWebServiceURL
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["WebService.BanagrarioService"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"WebService.BanagrarioService\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string SmsRegistroIntegrationWebService
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["SmsRegistro.Integration.WebService"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SmsRegistro.Integration.WebService\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string SmsRegistroIntegrationURL
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["SmsRegistro.Integration.Url"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SmsRegistro.Integration.Url\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string SmsRegistroIntegrationEntidad
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SmsRegistro.Integration.Entidad"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SmsRegistro.Integration.Entidad\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string SmsRegistroIntegrationCliente
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["SmsRegistro.Integration.Cliente"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SmsRegistro.Integration.Cliente\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string SmsRegistroIntegrationCiudad
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SmsRegistro.Integration.Ciudad"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SmsRegistro.Integration.Ciudad\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string SmsRegistroWorkflowUrl
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["SmsRegistro.Workflow.Url"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"SmsRegistro.Workflow.Url\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string URLVisorImagen
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["URLVisorImagen"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"URLVisorImagen\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string URLVisorImagenInterno
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["URLVisorImagenInterno"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"URLVisorImagenInterno\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string URLVisorImagenExterno
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["URLVisorImagenExterno"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"URLVisorImagenExterno\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string VisorTxDetalle
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["VisorTxDetalle"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"VisorTxDetalle\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string VisorTxDetalleFirmas
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["VisorTxDetalleFirmas"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"VisorTxDetalle\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string VisorTxDetalleLog
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["VisorTxDetalleLog"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"VisorTxDetalleLog\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string IdentifierDateFormat
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["IdentifierDateFormat"];
                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"IdentifierDateFormat\" value=\"?\"/> al archivo Web.config.");
            }
        }

        public static string OficinasBUrl
        {
            get
            {
                string rootWebConfig = ConfigurationManager.AppSettings["OficinasB.Url"];

                if (rootWebConfig != null && rootWebConfig.Length > 0)
                    return rootWebConfig;
                else
                    throw new Exception("Por favor asigne la cadena <add key=\"OficinasB.Url\" value=\"?\"/> al archivo Web.config.");
            }
        }


        public static string LocalServerURL
        {
            get
            {
                if (HttpContext.Current.Request.ApplicationPath != null)
                {
                    var empty = string.Empty;
                    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).TrimEnd('/') + "/" + (HttpContext.Current.Request.ApplicationPath == null || HttpContext.Current.Request.ApplicationPath.TrimEnd('/').TrimStart('/') != "" ? HttpContext.Current.Request.ApplicationPath.TrimEnd('/').TrimStart('/') + "/" : empty);
                }
                return null;
            }
        }

        public static string ConnectionStringPunteoLog
        {
            get
            {
                var rootWebConfig = ConfigurationManager.AppSettings["ConnectionStringPunteoLog"];

                if (!string.IsNullOrEmpty(rootWebConfig))
                    return rootWebConfig;
                throw new Exception("Por favor asigne la cadena <add key=\"ConnectionStringPunteoLog\" value=\"?\"/> al archivo Web.config.");
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

        public static object ParseDateTime(string nDateString)
        {
            try
            {
                var parts = nDateString.Split('/');
                return new DateTime(Convert.ToInt32(parts[2]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[0]));
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible leer la fecha como una fecha valida  [" + nDateString + "], " + ex.Message);
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

        public static TypeConnectionString getCadenasConexion(ref Miharu.Security.Library.WebService.SecurityWebService nWebService)
        {
            var Cadenas = new TypeConnectionString();
            var ConnectionStrings = nWebService.getCadenasConexion();

            foreach (var typeModulo in ConnectionStrings)
            {
                switch (typeModulo.Id)
                {
                    case 0:
                        Cadenas.Security = typeModulo.ConnectionString;
                        break;
                    case 1:
                        Cadenas.Workflow = typeModulo.ConnectionString;
                        break;
                    case 2:
                        Cadenas.Imaging = typeModulo.ConnectionString;
                        break;
                    case 6:
                        Cadenas.Core = typeModulo.ConnectionString;
                        break;
                    case 13:
                        Cadenas.BanAgrario = typeModulo.ConnectionString;
                        break;
                    case 14:
                        Cadenas.SMS = typeModulo.ConnectionString;
                        break;
                }
            }

            return Cadenas;
        }

        #endregion
    }
}