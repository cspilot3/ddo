using Miharu.CentralizacionEmbargos.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.Embargos
{
    public static class Program
    {
        #region " Declaraciones "

        public static ServiceConfigurationManager Config = new ServiceConfigurationManager();
        public static ServiceConfigurationManager.TypeConnectionString ConnectionStrings;
        public static ServiceConfigurationManager.TypeParametersString ConnectionParametersStrings;

        #endregion

        #region ' Propiedades '

        internal static string AppDataPath
        {
            get
            {
                DateTime today = DateTime.Now;                                                      // Obtener la fecha actual
                string fechaActual = today.ToString("yyyyMMdd");                                    // Formato AAAAMMDD

                // Agregar subcarpeta "LOGS" a la ruta base
                string logDirectory = Path.Combine(Config.SystemLogPath, "LOGS");

                // Construir la ruta del archivo de registro
                string _SystemLogPath = logDirectory;
                if (!Directory.Exists(_SystemLogPath)) Directory.CreateDirectory(_SystemLogPath);   // Crear el directorio si no existe

                _SystemLogPath += $"\\log_{fechaActual}.txt";

                return _SystemLogPath;
            }
        }

        #endregion

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CentralizacionEmbargosService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
