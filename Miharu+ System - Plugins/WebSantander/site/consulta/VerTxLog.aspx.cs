using System;
using WebSantander.code;
using Slyg.Tools;
using System.Collections.Generic;
using DBIntegration;
using DBIntegration.SchemaSantander;
using DBIntegration.SchemaRipley;
using System.Data;
using DBSecurity;
using Miharu.Security.Library.Session;
using System.Windows.Forms;
using Miharu.FileProvider.Library;
using Miharu.Desktop.Library;
using Miharu.Desktop.Library.Config;
using System.IO;
using Slyg.Tools.Imaging;
using System.Text.RegularExpressions;

namespace WebSantander.site.consulta
{
    public partial class VerTxLog : System.Web.UI.Page
    {
        #region Declaraciones

        Program _Program = new Program();
        private DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType _centro;

        private int MaxThumbnailWidth = 60;
        private int MaxThumbnailHeight = 80;

        #endregion

        #region Propiedades

        public Sesion MiharuSession
        {
            get { return (Sesion)Session["Session"]; }
        }

        public TypeConnectionString ConnectionString
        {
            get
            {
                if (MiharuSession.Parameter["ConnectionStrings"] == null)
                    return null;
                return (TypeConnectionString)MiharuSession.Parameter["ConnectionStrings"];
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Dialogo_Resultados.Visible = false;
                if (Session["Session"] != null)
                {
                    MostrarTxDetalle();
                }
                else
                    MensajeLabel.Text = "No se encontró una sesión activa";
            }
        }
        
        protected void ImbCerrarCambioEstado_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Dialogo_Resultados.Visible = false;
        }

        protected void btnCambioEstado_Click(object sender, EventArgs e)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                bool img = false;
               if (ddlEstado.SelectedIndex == 0 || ddlEstado.SelectedIndex == -1 || ddlEstado.SelectedValue == "")
                    throw new Exception("Debe seleccionar estado.");

                if (!fileArchivo.HasFile && ddlEstado.SelectedValue == "POSITIVO")
                    throw new Exception("Debe seleccionar archivo de soporte.");

                string[] Datos = lblId.Text.Split(new char[] { '-' });

                string id = Datos[0], campo = Datos[1];

                if (ddlEstado.SelectedValue == "POSITIVO")
                    img = GuardarImagen();

                if (img = true || ddlEstado.SelectedValue == "NEGATIVO")
                {

                    dbmIntegration = new DBIntegrationDataBaseManager(ConnectionString.Santander);
                    dbmIntegration.Connection_Open(MiharuSession.Usuario.id);
                    dbmIntegration.SchemaSantander.SP_Cambio_Estado_Expediente.DBExecute
                        (
                        nId_Data: long.Parse(id),
                        nCampo: int.Parse(campo),
                        nValor: ddlEstado.SelectedValue.ToString()
                        );
                    Dialogo_Resultados.Visible = false;
                    MostrarTxDetalle();
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            string[] Data = lblData.Text.Split(new char[] { '-' });
            try
            {
                dbmIntegration = new DBIntegrationDataBaseManager(ConnectionString.Santander);
                dbmIntegration.Connection_Open(MiharuSession.Usuario.id);
                dbmIntegration.SchemaSantander.SP_Cambio_Estado_Expediente.DBExecute
                    (
                    nId_Data: long.Parse(Data[0]),
                    nCampo: int.Parse(Data[1]),
                    nValor: "NEGATIVO"
                    );
                Dialogo_Resultados.Visible = false;
                MostrarTxDetalle();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Dialogo_Resultados.Visible = false;
        }

        #endregion

        #region Metodos

        protected void MostrarTxDetalle()
        {
            if (Request.Params["tx"] != null)
            {
                DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
                
                try
                {
                    dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionString.Santander);
                    dbmIntegration.Connection_Open(this.MiharuSession.Usuario.id); 

                    var Proceso = int.Parse(Request.Params["prc"]);
                    string [] Data = Request.Params["tx"].Split(new char [] {'-'});

                    short Proyecto = short.Parse(Data[0]),
                    Folder = short.Parse(Data[2]),
                    File = short.Parse(Data[3]);

                    long Expediente = long.Parse(Data[1]);
                    
                    var datos = dbmIntegration.SchemaSantander.PopUp_DataClient.DBExecute(
                    nProceso: Proceso,
                    nExpediente: Expediente,
                    nFolder: Folder,
                    nFile: File,
                    nId_Data:0,
                    nProyecto: Proyecto);

                    if (Proyecto == 1)
                    {
                        DatosEmbargo_GridView.Columns[7].Visible = true;
                        DatosEmbargo_GridView.Columns[8].Visible = true;
                        DatosEmbargo_GridView.Columns[9].Visible = true;
                        DatosEmbargo_GridView.Columns[10].Visible = false;
                    }
                    else
                    {
                        DatosEmbargo_GridView.Columns[7].Visible = false;
                        DatosEmbargo_GridView.Columns[8].Visible = false;
                        DatosEmbargo_GridView.Columns[9].Visible = false;
                        DatosEmbargo_GridView.Columns[10].Visible = true;
                    }
                    
                    DatosEmbargo_GridView.DataSource = datos;
                    DatosEmbargo_GridView.DataBind();

                    if (datos.Rows.Count <= 0)
                        MensajeLabel.Text = "No se encontraron datos para visualizar";
                    else
                        MensajeLabel.Text = "";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    if (dbmIntegration != null) dbmIntegration.Connection_Close();
                }
            }
        }

        protected void ConsultarHistorial(int Proceso, long Id)
        {
                DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

                try
                {
                    dbmIntegration = new DBIntegrationDataBaseManager(ConnectionString.Santander);
                    dbmIntegration.Connection_Open(MiharuSession.Usuario.id);
                    
                    var Datos = dbmIntegration.SchemaSantander.PopUp_DataClient.DBExecute(
                    nProceso: Proceso,
                    nExpediente: 0,
                    nFolder: 0,
                    nFile: 0,
                    nId_Data: Id,
                    nProyecto: 0);

                    GridHistorial.DataSource = Datos;
                    GridHistorial.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    if (dbmIntegration != null) dbmIntegration.Connection_Close();
                }

        }

        protected void ValidarValor(string id, string valor, int campo)
        {            
            valor = valor.ToUpper();

                switch (valor)
                {
                    case "NEGATIVO":
                        Dialogo_Resultados.Visible = true;
                        Historial.Visible = false;
                        Cambio_Estado.Visible = false;
                        Positivo.Visible = true;
                        pnlPositivo.Visible = false;
                        lblNegativo.Visible = true;
                    break;
                    case "POSITIVO":
                        Dialogo_Resultados.Visible = true;
                        Historial.Visible = false;
                        Cambio_Estado.Visible = false;
                        Positivo.Visible = true;
                        pnlPositivo.Visible = true;
                        lblNegativo.Visible = false;
                        lblData.Text = id + "-" + campo;
                    break;
                    case "MULTIPLE":
                        lblId.Text = id + "-" + campo.ToString();
                        Dialogo_Resultados.Visible = true;
                        Historial.Visible = false;
                        Cambio_Estado.Visible = true;
                        Positivo.Visible = false;
                    break;
                    default:
                        Dialogo_Resultados.Visible = true;
                        Historial.Visible = false;
                        Cambio_Estado.Visible = false;
                        Positivo.Visible = true;
                        pnlPositivo.Visible = false;
                        lblNegativo.Visible = true;
                    break;
                }
        }

        protected bool GuardarImagen()
        {
            FileInfo Info = new FileInfo(fileArchivo.PostedFile.FileName);
            string path = Server.MapPath("~/Temp/" + fileArchivo.FileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }                

            fileArchivo.SaveAs(path);

            string[] Data = Request.Params["tx"].Split(new char[] { '-' });

            short Proyecto = short.Parse(Data[0]),
            Folder = short.Parse(Data[2]),
            File = short.Parse(Data[3]);

            long Expediente = long.Parse(Data[1]);

            if (Info.Extension.ToUpper() != ".PDF")
                throw new Exception("Formato de archivo no permitido.");

            FileProviderManager manager = null;

            try
            {
                DBCore.DBCoreDataBaseManager dbmCore = null;
                DBImaging.DBImagingDataBaseManager dbmImaging = null;

                try
                {
                    dbmImaging = new DBImaging.DBImagingDataBaseManager(ConnectionString.Imaging);
                    dbmCore = new DBCore.DBCoreDataBaseManager(dbmImaging, ConnectionString.Core);

                    dbmImaging.Connection_Open(MiharuSession.Usuario.id);

                    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted);

                    var ProyectoImaging = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.idCliente, Proyecto)[0].ToCTA_ProyectoSimpleType();
                    var CentroProcesamiento = dbmCore.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.idCliente, null, null)[0].ToCTA_Centro_ProcesamientoSimpleType();
                    var ServidorImagen = dbmCore.SchemaImaging.TBL_Servidor.DBFindByfk_Entidad(Program.idCliente)[0].ToTBL_ServidorSimpleType();

                    var formato = Utilities.GetEnumFormat(ProyectoImaging.Extension_Formato_Imagen_Salida);
                    var compresion = Utilities.GetEnumCompression((DesktopConfig.FormatoImagenEnum)ProyectoImaging.id_Formato_Imagen_Salida);
                    
                    _centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CentroProcesamiento.fk_Entidad, CentroProcesamiento.fk_Sede, CentroProcesamiento.id_Centro_Procesamiento)[0].ToCTA_Centro_ProcesamientoSimpleType();

                    var servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(ServidorImagen.fk_Entidad, ServidorImagen.id_Servidor)[0].ToCTA_ServidorSimpleType();

                    manager = new FileProviderManager(servidor, _centro, ref dbmImaging, MiharuSession.Usuario.id);
                    manager.Connect();

                    var DataImaging = dbmCore.SchemaImaging.TBL_File.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(
                    nfk_Expediente: Expediente,
                    nfk_Folder: Folder,
                    nfk_File: File,
                    nid_Version: null
                    )[0].ToTBL_FileSimpleType();

                    short FoliosUpdate = DataImaging.Folios_Documento_File;

                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    string pdfText = sr.ReadToEnd();
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection matches = regex.Matches(pdfText);
                    short cont = Convert.ToInt16(matches.Count);
                    sr.Close();
                    fs.Close();
                    
                    for (int i = 1; i <= cont ; i++)
                    {
                        FoliosUpdate += 1;
                        var FolioBitmap = ImageManager.GetFolioBitmap(path, i);
                        var dataImage = ImageManager.GetFolioData(FolioBitmap, 1, 1, formato, compresion);
                        var dataImageThumbnail = ImageManager.GetThumbnailData(path, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight);

                        if (i == 1)
                        {
                            manager.CreateItem(Expediente, Folder, File, DataImaging.id_Version, ProyectoImaging.Extension_Formato_Imagen_Salida, Guid.NewGuid());
                        }
                        manager.CreateFolio(Expediente, Folder, File, DataImaging.id_Version, FoliosUpdate, dataImage, dataImageThumbnail[0], false);
                    }

                    //FoliosUpdate += cont;

                    //Actualizacion numero de folios por expediente-folder-file
                    DBCore.SchemaImaging.TBL_FileType UpdateFoliosImaging = new DBCore.SchemaImaging.TBL_FileType();
                    UpdateFoliosImaging.Folios_Documento_File = FoliosUpdate;
                    dbmCore.SchemaImaging.TBL_File.DBUpdate(UpdateFoliosImaging, Expediente, Folder, File, DataImaging.id_Version);

                    //Actualizacion numero de folios por expediente-folder-file
                    DBCore.SchemaProcess.TBL_FileType UpdateFoliosProcess = new DBCore.SchemaProcess.TBL_FileType();
                    UpdateFoliosProcess.Folios_File = FoliosUpdate;
                    dbmCore.SchemaProcess.TBL_File.DBUpdate(UpdateFoliosProcess, Expediente, Folder, File);

                    System.IO.File.Delete(path);

                    dbmImaging.Transaction_Commit();
                }
                catch (Exception ex)
                {
                    //if (dbmCore != null) dbmCore.Connection_Close();
                    if (dbmImaging != null) dbmImaging.Transaction_Rollback();
                    Response.Write(ex.Message);
                    return false;
                }
                finally
                {
                    if (dbmImaging != null) dbmImaging.Connection_Close();
                }                              
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            return true;
        }        

        #endregion

    }

}