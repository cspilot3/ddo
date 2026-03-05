using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using Slyg.Data.Schemas;

namespace Miharu.CentralizacionEmbargos.Configuration
{

    public static class Parameter
    {
        public const string ThreadCount = "threadCount";
    }

    public class ServiceConfigurationManager
    {
        #region " Declaraciones "
        #endregion

        #region " Estructuras "

        [Serializable]
        public struct TypeConnectionString
        {
            public string Security;
            public string Archiving;
            public string Core;
            public string Imaging;
            public string OCR;
            public string Tools;
            public string Softrac;
            public string Core_Risks;
            public string Imaging_Risks;
            public string Archiving_Risks;
            public string Embargos;
        }

        [Serializable]
        public struct TypeParametersString
        {
            public int threadCount;
        }

        #endregion

        #region " Enumeraciones"
        public enum Modulo : byte
        {
            Security = 0,
            Imaging = 2,
            Core = 6,
            Archiving = 7,
            PunteoBanAgrario = 13,
            Tools = 3,
            Softrac = 35,
            Core_Risks = 38,
            Imaging_Risks = 39,
            Archiving_Risks = 40,
            OCR = 41,
            Embargos = 42
        }

        #endregion

        #region " Propiedades "
        public int Intervalo { get; set; }
        public int usuario_log { get; private set; }
        public string ConnectionStringSecurity { get; }
        public string SystemLogPath { get; }
        public Guid sesionID { get; private set; }
        public string pcName { get; private set; }
        public int Linea { get; private set; }

        #endregion

        #region " Constructores "

        public ServiceConfigurationManager()
        {
            InitializeSession();

            ConnectionStringSecurity = ConfigurationManager.AppSettings["ConnectionStringSecurity"];
            SystemLogPath = AppDomain.CurrentDomain.BaseDirectory; //ConfigurationManager.AppSettings["SystemLogPath"];
        }

        #endregion

        #region " Metodos"

        private void InitializeSession()
        {
            sesionID = Guid.NewGuid();                                      // genera un new guid Unico
            pcName = Environment.MachineName;                               // Nombre del servidor
            Linea = 1;                                                      // Ajustar de acuerdo a la linea
            usuario_log = 2;                                                // Ajustar de acuerdo al usuario
            Intervalo = 5000;
        }

        #endregion

        #region " funciones "

        public ServiceConfigurationManager.TypeConnectionString GetCadenasConexion()
        {
            ServiceConfigurationManager.TypeConnectionString cadenas = new ServiceConfigurationManager.TypeConnectionString();

            var connectionStrings = GetModuloDataTable();

            foreach (var Modulo in connectionStrings)
            {
                switch ((ServiceConfigurationManager.Modulo)Modulo.id_Modulo)
                {
                    case ServiceConfigurationManager.Modulo.Security:
                        cadenas.Security = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.Imaging:
                        cadenas.Imaging = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.Core:
                        cadenas.Core = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.Archiving:
                        cadenas.Archiving = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.Tools:
                        cadenas.Tools = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.Softrac:
                        cadenas.Softrac = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.OCR:
                        cadenas.OCR = Modulo.ConnectionString;
                        break;
                    case ServiceConfigurationManager.Modulo.Embargos:
                        cadenas.Embargos = Modulo.ConnectionString;
                        break;
                }
            }

            return cadenas;
        }

        public ServiceConfigurationManager.TypeParametersString GetParametersSystem()
        {
            ServiceConfigurationManager.TypeParametersString parameters = new ServiceConfigurationManager.TypeParametersString();

            var connectionStrings = GetParametersDataTable();

            foreach (var parameter in connectionStrings)
            {
                switch (parameter.Nombre_Parametro)
                {
                    case Parameter.ThreadCount:
                        parameters.threadCount = int.TryParse(parameter.Valor_Parametro, out int result) ? result : 5;                                       // se establecen 5 hilos por defecto
                        break;
                }
            }

            return parameters;
        }

        private DBSecurity.SchemaSecurity.TBL_ModuloDataTable GetModuloDataTable()
        {
            DBSecurity.DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(ConnectionStringSecurity);
                dbmSecurity.Connection_Open(Embargos.Program.Config.usuario_log);

                DBSecurity.SchemaSecurity.TBL_ModuloDataTable moduloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(null);

                return moduloDataTable;
            }
            catch
            {
                throw;
            }
            finally
            {
                dbmSecurity?.Connection_Close();
            }
        }

        private DBEmbargos.SchemaConfig.TBL_ParametroDataTable GetParametersDataTable()
        {
            DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos = null;

            try
            {
                dbmEmbargos = new DBEmbargos.DBEmbargosDataBaseManager(Embargos.Program.ConnectionStrings.Embargos);
                dbmEmbargos.Connection_Open(Embargos.Program.Config.usuario_log);

                var parametersDataTable = dbmEmbargos.SchemaConfig.TBL_Parametro.DBGet(null);

                return parametersDataTable;
            }
            catch
            {
                throw;
            }
            finally
            {
                dbmEmbargos?.Connection_Close();
            }
        }

        #endregion
    }
}
