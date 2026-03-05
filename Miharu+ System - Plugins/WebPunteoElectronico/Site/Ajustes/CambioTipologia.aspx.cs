using System;
using Slyg.Tools;
using System.Web.UI.WebControls;
using DBAgrario;
using DBAgrario.SchemaConfig;
using DBCore;
using DBCore.SchemaConfig;
using DBCore.SchemaProcess;
using DBImaging.SchemaProcess;
using DBImaging;
using Miharu.Desktop.Library.Config;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Ajustes
{
    public partial class CambioTipologia : FormBase
    {
        #region Declaraciones

        public string Query = "";
        public string Path_Nodo = "3.2.4.1";
        public Tipo_Accion_Log TipoAccion;

        #endregion

        #region Propiedades


        private long Expediente
        {
            get { return (long) this.MiharuSession.Pagina.Parameter["Expediente"]; }
            set { this.MiharuSession.Pagina.Parameter["Expediente"] = value; }
        }

        private short Folder
        {
            get { return (short) this.MiharuSession.Pagina.Parameter["Folder"]; }
            set { this.MiharuSession.Pagina.Parameter["Folder"] = value; }
        }

        private short file
        {
            get { return (short) this.MiharuSession.Pagina.Parameter["File"]; }
            set { this.MiharuSession.Pagina.Parameter["File"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EsquemaDropDownList.SelectedIndexChanged += EsquemaDropDownList_SelectedIndexChanged;
            AceptarButton.Click += AceptarButton_Click;
        }

        private void EsquemaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDocumentos(short.Parse(EsquemaDropDownList.SelectedValue));
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            try
            {
                Expediente = long.Parse(Request.QueryString["Ex"]);
                Folder = short.Parse(Request.QueryString["Fo"]);
                file = short.Parse(Request.QueryString["Fi"]);

                Load_Data();

                ErrorLabel.Text = "";
            }
            catch (Exception ex)
            {
                MainPanel.Visible = false;
                ErrorLabel.Visible = true;

                ErrorLabel.Text = ex.Message;
            }

            MessageLabel.Text = "";
        }

        protected override void Load_Data()
        {
            DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmCore = new DBCoreDataBaseManager(ConnectionString.Core);
                dbmCore.Connection_Open(this.MiharuSession.Usuario.id);

                var EsquemasDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet((short) 9, (short) 2, null, 0, new TBL_EsquemaEnumList(TBL_EsquemaEnum.Nombre_Esquema, true));

                EsquemaDropDownList.Items.Clear();
                EsquemaDropDownList.Items.Add(new ListItem("- Seleccionar -", "-1"));

                foreach (var EsquemaRow in EsquemasDataTable)
                {
                    EsquemaDropDownList.Items.Add(new ListItem(EsquemaRow.Nombre_Esquema, EsquemaRow.id_Esquema.ToString()));
                }

                EsquemaDropDownList.SelectedIndex = 1;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }

            LoadDocumentos(1);
        }

        private void LoadDocumentos(short nEsquema)
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                var orden = new CTA_Documento_TX_ConcatenacionEnumList();
                //orden.Add(CTA_Documento_TX_ConcatenacionEnum.Codigo_Tx, false); // Se adicionan todos los criterios de ordenamiento a la variable orden que se quieran utilizar
                orden.Add(CTA_Documento_TX_ConcatenacionEnum.CodTxNumConv, true);
                

                var DocumentosDataTable = dbmBanagrario.SchemaConfig.CTA_Documento_TX_Concatenacion.DBFindByfk_Entidadfk_Proyectofk_Esquema((short) 9, (short) 2, nEsquema, 0, orden);
                
                TipologiaDropDownList.Items.Clear();
                TipologiaDropDownList.Items.Add(new ListItem("- Seleccionar -", "-1"));

                foreach (var DocumentoRow in DocumentosDataTable)
                {
                    TipologiaDropDownList.Items.Add(new ListItem(DocumentoRow.Nombre_Documento, DocumentoRow.id_Documento.ToString()));
                }

                //TipologiaDropDownList.SelectedIndex = 0;
                
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        private void Save()
        {
            if (Validar())
            {
                DBCoreDataBaseManager dbmCore = null;
                DBImagingDataBaseManager dbmImaging = null;
                DBAgrarioDataBaseManager DBMBangrario = null;

                try
                {
                    dbmCore = new DBCoreDataBaseManager(ConnectionString.Core);
                    dbmImaging = new DBImagingDataBaseManager(ConnectionString.Imaging);
                    DBMBangrario = new DBAgrarioDataBaseManager(ConnectionString.BanAgrario);

                    dbmCore.Connection_Open(this.MiharuSession.Usuario.id);
                    dbmImaging.Connection_Open(this.MiharuSession.Usuario.id);
                    DBMBangrario.Connection_Open(this.MiharuSession.Usuario.id);

                    //Vlr_Anterior
                    var Vlr_Anterior = DBMBangrario.SchemaProcess.PA_Get_Detalle_Soporte.DBExecute((int) Expediente);

                    // Tipologia
                    var ExpedienteType = new TBL_ExpedienteType {fk_Esquema = short.Parse(EsquemaDropDownList.SelectedValue)};
                    dbmCore.SchemaProcess.TBL_Expediente.DBUpdate(ExpedienteType, Expediente);

                    var FileType = new DBCore.SchemaProcess.TBL_FileType {fk_Documento = int.Parse(TipologiaDropDownList.SelectedValue)};
                    dbmCore.SchemaProcess.TBL_File.DBUpdate(FileType, Expediente, Folder, file);

                    // Estado
                    var FileEstadoType = new TBL_File_EstadoType {Fecha_Log = SlygNullable.SysDate, fk_Usuario = this.MiharuSession.Usuario.id, fk_Estado = (short) EstadoEnum.Captura};
                    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(FileEstadoType, Expediente, Folder, file, (byte) DesktopConfig.Modulo.Imaging);

                    // Registrar Tx en Reproceso
                    DBMBangrario.SchemaProcess.PA_Registrar_Reproceso.DBExecute(Expediente, Folder, file, this.MiharuSession.Usuario.id);

                    //Last_Query
                    var strQuery = DBMBangrario.DataBase.LastQuery;

                    // Borrar data
                    dbmCore.SchemaProcess.TBL_File_Data.DBDelete(Expediente, Folder, file, null, null);
                    dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(Expediente, Folder, file, null, null, null, null);
                    dbmCore.SchemaProcess.TBL_File_Validacion.DBDelete(Expediente, Folder, file, null, FileType.fk_Documento);

                    // Solicita nuevamente validaciones opcionales
                    var ImagingFileType = new DBCore.SchemaImaging.TBL_FileType {Validaciones_Opcionales = false};
                    dbmCore.SchemaImaging.TBL_File.DBUpdate(ImagingFileType, Expediente, Folder, file, null);

                    //---------------------------------------------------------------------------
                    // Actualizar Dashboard
                    //---------------------------------------------------------------------------
                    var FolderDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(Expediente, Folder);
                    var CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(FolderDataTable[0].fk_Cargue);
                    var PaqueteDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(FolderDataTable[0].fk_Cargue, FolderDataTable[0].fk_Cargue_Paquete);

                    // Actualizar capturas
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(Expediente, Folder, file);

                    var CapDashboardType = new TBL_Dashboard_CapturasType {fk_Expediente = Expediente, fk_Folder = Folder, fk_File = file, fk_Documento = int.Parse(TipologiaDropDownList.SelectedValue), fk_Cargue = FolderDataTable[0].fk_Cargue, fk_Cargue_Paquete = FolderDataTable[0].fk_Cargue_Paquete, fk_Entidad_Procesamiento = CargueDataTable[0].fk_Entidad_Procesamiento, fk_Sede_Procesamiento = PaqueteDataTable[0].fk_Sede_Procesamiento_Asignada, fk_Centro_Procesamiento = PaqueteDataTable[0].fk_Centro_Procesamiento_Asignado, fk_Entidad = CargueDataTable[0].fk_Entidad, fk_Proyecto = CargueDataTable[0].fk_Proyecto, fk_Estado = (short) EstadoEnum.Captura, fk_OT = CargueDataTable[0].fk_OT};

                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(CapDashboardType);

                    // Actualizar validaciones opcionales
                    var ValidacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(int.Parse(TipologiaDropDownList.SelectedValue), (byte) EnumEtapaCaptura.Opcional, 1, new DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, true));

                    if (ValidacionesDataTable.Count > 0)
                    {
                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(Expediente, Folder, file);

                        var ValDashboardType = new TBL_Dashboard_ValidacionesType {fk_Expediente = Expediente, fk_Folder = Folder, fk_File = file, fk_Documento = int.Parse(TipologiaDropDownList.SelectedValue), fk_Cargue = FolderDataTable[0].fk_Cargue, fk_Cargue_Paquete = FolderDataTable[0].fk_Cargue_Paquete, fk_Entidad_Procesamiento = CargueDataTable[0].fk_Entidad_Procesamiento, fk_Sede_Procesamiento = PaqueteDataTable[0].fk_Sede_Procesamiento_Asignada, fk_Centro_Procesamiento = PaqueteDataTable[0].fk_Centro_Procesamiento_Asignado, fk_Entidad = CargueDataTable[0].fk_Entidad, fk_Proyecto = CargueDataTable[0].fk_Proyecto, Procesado = false};

                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(ValDashboardType);
                    }
                    //---------------------------------------------------------------------------

                    dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(Expediente, Folder, file, null, null, null);
                    dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(Expediente, Folder, file, null, null, null, null, null);

                    //Vlr_Despues
                    var Vlr_Despues = DBMBangrario.SchemaProcess.PA_Get_Detalle_Soporte.DBExecute((int) Expediente);

                    //Registrar Accion
                    Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Actualizar, Path_Nodo, strQuery, Vlr_Anterior, Vlr_Despues);


                    AceptarButton.Enabled = false;

                    MessageLabel.Text = "Los cambios se aplicaron exitosamente";
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
                finally
                {
                    if (dbmCore != null) dbmCore.Connection_Close();
                    if (dbmImaging != null) dbmImaging.Connection_Close();
                    if (DBMBangrario != null) DBMBangrario.Connection_Close();
                }
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            ErrorLabel.Text = "";
            MessageLabel.Text = "";

            if (EsquemaDropDownList.SelectedIndex == 0)
                ErrorLabel.Text = "Debe seleccionar el esquema";
            else if (TipologiaDropDownList.SelectedIndex == 0)
                ErrorLabel.Text = "Debe seleccionar la tipologia";
            else
                return true;

            return false;
        }

        #endregion
    }
}