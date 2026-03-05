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
using QueryResponse = Miharu.Desktop.Library.MiharuDMZ.QueryResponse;
using ClientUtil = Miharu.Desktop.Library.MiharuDMZ.ClientUtil;
using QueryParameter = Miharu.Desktop.Library.MiharuDMZ.QueryParameter;
using QueryResponseType = Miharu.Desktop.Library.MiharuDMZ.QueryResponseType;
using QueryRequestType = Miharu.Desktop.Library.MiharuDMZ.QueryRequestType;

namespace Davivienda.Plugin.Imaging.Embargos
{
    public class EmbargosPlugin : IDesktopPlugin
    {
        #region "Declaraciones"

        public const short ModuloId = 30;

        public const string ConfigurationPrefix = "PluginBcoDavivienda_";

        private EventExecuterEmbargos _EventExecuter;

        private DesktopPluginManager _Manager;

        private FormImagingWorkSpace _WorkSpace;

        private string _BcoDaviviendaConnectionString;

        private string _ToolsConnectionString;

        private FormImagingWorkSpaceWrapperEmbargos _Wrapper;

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

        public string BcoDaviviendaConnectionString
        {
            get { return this._BcoDaviviendaConnectionString; }
        }

        public string ToolsConnectionString
        {
            get { return this._ToolsConnectionString; }
        }

        public FormImagingWorkSpaceWrapperEmbargos Wrapper
        {
            get { return this._Wrapper; }
        }

        // Propiedades configurables
        public static short Imaging_EntidadId { get; set; }
        public static short Imaging_BcoDaviviendaEmbargos_ProyectoId { get; set; }

        #endregion

        #region " Implementacion IDesktopPlugin "

        public bool IsValidPlugin(Miharu.Desktop.Library.ProcessLibraryType nProcessType, int nEntidad, int nProyecto)
        {
            Imaging_EntidadId = 41;
            Imaging_BcoDaviviendaEmbargos_ProyectoId = 3;

            PluginHelper.InicializarConfiguracion(this, ConfigurationPrefix);

            if ((nProcessType != ProcessLibraryType.Imaging))
                return false;
            if ((nEntidad != Imaging_EntidadId))
                return false;
            if ((nProyecto != Imaging_BcoDaviviendaEmbargos_ProyectoId))
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
                DBSecurity.SchemaSecurity.TBL_ModuloDataTable ModuloDataTable = null;
                if (nManager.Sesion.IsExternal)
                {
                    //var ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(null);
                    List<QueryParameter> parametros = new List<QueryParameter>();
                    QueryResponse queryResponseParametros = ClientUtil.resolver( "[DB_Miharu.Security_core].Security.TBL_Modulo", parametros, QueryRequestType.Table, QueryResponseType.Table);
                    ModuloDataTable = (DBSecurity.SchemaSecurity.TBL_ModuloDataTable)ClientUtil.mapToTypedTable( new DBSecurity.SchemaSecurity.TBL_ModuloDataTable(), queryResponseParametros.dataTable );
                }
                else
                {
                    dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(this.Manager.DesktopGlobal.ConnectionStrings.Security);
                    dbmSecurity.Connection_Open(1);

                    try
                    {
                        ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(null);
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

                if ((ModuloDataTable.Count > 0))
                {
                    foreach (var modulo_loopVariable in ModuloDataTable)
                    {

                        switch (modulo_loopVariable.id_Modulo)
                        {
                            case 33:
                                this._BcoDaviviendaConnectionString = modulo_loopVariable.ConnectionString;
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
        }

        public void Activate()
        {
            try
            {
                this._Wrapper = new FormImagingWorkSpaceWrapperEmbargos(this);
                this._Wrapper.AplicarCambios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al activar el plugin " + GetName() + ", " + ex.Message);
            }
        }

        public string GetCode()
        {
            return "BancoDavivienda_Plugin";
        }

        public string GetName()
        {
            return "Libreria especializada para BancoDavivienda-Embargos";
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
