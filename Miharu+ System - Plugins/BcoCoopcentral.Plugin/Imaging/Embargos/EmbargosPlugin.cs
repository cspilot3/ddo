using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miharu.Desktop.Library.Plugins;
using Miharu.Desktop.Library;
using Miharu.Imaging.Library;
using System.Windows.Forms;
using DBSecurity;
using System.Data;

namespace BcoCoopcentral.Plugin.Imaging.Embargos
{
    public class EmbargosPlugin : IDesktopPlugin
    {
        #region "Declaraciones"

        public const short ModuloId = 30;

        public const string ConfigurationPrefix = "PluginBcoCoopcentral_";

        private EventExecuterEmbargos _EventExecuter;

        private DesktopPluginManager _Manager;

        private FormImagingWorkSpace _WorkSpace;

        private string _BcoCoopcentralConnectionString;

        private string _ToolsConnectionString;

        private FormImagingWorkSpaceWrapperEmbargo _Wrapper;

        public const string TempPath = "temp\\";
        static internal string AppPath
        {
            get { return System.Windows.Forms.Application.StartupPath.TrimEnd('\\') + "\\"; }
        }

        #endregion

        public EmbargosPlugin()
        {
            this._EventExecuter = new EventExecuterEmbargos(this);
        }

        #region "Propiedades"

        public DesktopPluginManager Manager
        {
            get { return this._Manager; }
        }

        public FormImagingWorkSpace WorkSpace
        {
            get { return this._WorkSpace; }
        }

        public string BcoCoopcentralConnectionString
        {
            get { return this._BcoCoopcentralConnectionString; }
        }

        public string ToolsConnectionString
        {
            get { return this._ToolsConnectionString; }
        }

        public FormImagingWorkSpaceWrapperEmbargo Wrapper
        {
            get { return this._Wrapper; }
        }

        // Propiedades configurables
        public static short Imaging_EntidadId { get; set; }
        public static short Imaging_BcoCoopcentral_ProyectoId { get; set; }

        #endregion


        #region " Implementacion IDesktopPlugin "

        public bool IsValidPlugin(Miharu.Desktop.Library.ProcessLibraryType nProcessType, int nEntidad, int nProyecto)
        {
            Imaging_EntidadId = 55;
            Imaging_BcoCoopcentral_ProyectoId = 1;

            PluginHelper.InicializarConfiguracion(this, ConfigurationPrefix);

            if ((nProcessType != ProcessLibraryType.Imaging))
                return false;
            if ((nEntidad != Imaging_EntidadId))
                return false;
            if ((nProyecto != Imaging_BcoCoopcentral_ProyectoId))
                return false;
            return true;
        }

        public void Initialize(Miharu.Desktop.Library.Plugins.DesktopPluginManager nManager)
        {
            this._Manager = nManager;
            this._WorkSpace = (FormImagingWorkSpace)nManager.WorkSpace;

            DBSecurity.DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(this.Manager.DesktopGlobal.ConnectionStrings.Security);
                dbmSecurity.Connection_Open(1);
                // System

                var ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(null);

                if ((ModuloDataTable.Count > 0))
                {
                    foreach (var modulo_loopVariable in ModuloDataTable)
                    {

                        switch (modulo_loopVariable.id_Modulo)
                        {
                            case 33:
                                this._BcoCoopcentralConnectionString = modulo_loopVariable.ConnectionString;
                                break;
                            case 3:
                                this._ToolsConnectionString = modulo_loopVariable.ConnectionString;
                                break;
                        }
                    }
                }
                else
                {
                    throw new Exception("No se pudo cargar la cadena de conexión para el módulo: " + ModuloId.ToString());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if ((dbmSecurity != null))
                    dbmSecurity.Connection_Close();
            }
        }

        public void Activate()
        {
            try
            {
                this._Wrapper = new FormImagingWorkSpaceWrapperEmbargo(this);
                this._Wrapper.AplicarCambios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message);
            }
        }

        public string GetCode()
        {
            return "BancoCoopcentralEmbargo_Plugin";
        }

        public string GetName()
        {
            return "Libreria especializada para BancoCoopcentral-Embargo";
        }

        public void Close()
        {
        }

        public object InitializeLifetimeService()
        {
            return null;
        }

        public Miharu.Desktop.Library.Plugins.EventExecuter EventExecuter
        {
            get { return this._EventExecuter; }
        }
        #endregion
    }
}
