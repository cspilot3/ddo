using System;
using Miharu.Security.Library.Session;
using DBAgrario.SchemaCore;
using DBAgrario;
using System.Data;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Consulta
{
    public partial class VerDetalleData : System.Web.UI.Page
    {
        #region Declaraciones

        public string Path_Nodo = "1.1";

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
                if (Session["Session"] != null)
                {
                    MostrarTxDetalle();
                    CargarImagen();
                }
            }
        }

        protected void VerImagen_LinkButton_Click(object sender, EventArgs e)
        {
            CargarImagen();
        }

        #endregion

        #region Metodos

        public void MostrarTxDetalle()
        {
            this.TipoDoc_Label.Text = "Documento desconocido";

            if (Request["dt"] != null)
            {
                DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

                try
                {
                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                    dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);

                    var ProcesoDataId = long.Parse(Request["dt"]);

                    var datos = dbmBanagrario.SchemaProcess.CTA_Consulta_Proceso_Detalle.DBFindByid_Proceso_Detalle(ProcesoDataId);

                    //Registrar accion
                    var query = dbmBanagrario.DataBase.LastQuery;
                    Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                    if (datos.Count > 0)
                    {
                        TipoDoc_Label.Text = datos[0].Nombre_Tx;

                        var datat = TransponerTabla(datos, int.Parse(datos[0].fk_Documento));

                        RegistroTx_GridView.DataSource = datat;
                        RegistroTx_GridView.DataBind();

                        var CoreIndexDataTable = dbmBanagrario.SchemaProcess.TBL_Core_Index.DBGet(datos[0].fk_Core_Index);
                        var imagenData = dbmBanagrario.SchemaCore.CTA_Imaging_File.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(CoreIndexDataTable[0].fk_Expediente, CoreIndexDataTable[0].fk_Folder, (short)CoreIndexDataTable[0].fk_File, null, 0, new CTA_Imaging_FileEnumList(CTA_Imaging_FileEnum.id_Version, false));

                        if (imagenData.Count > 0)
                        {
                            Session["__Imagen_File_Unique_Identifier"] = imagenData[0].File_Unique_Identifier;
                        }
                        else
                        {
                            VerImagen_LinkButton.Visible = false;
                            Imagen_Div.Visible = false;
                        }
                    }
                    else
                    {
                        VerImagen_LinkButton.Visible = false;
                        Imagen_Div.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                }
            }
        }

        private void CargarImagen()
        {
            var token = "";

            if (Session["__Imagen_File_Unique_Identifier"] != null)
                token = Session["__Imagen_File_Unique_Identifier"].ToString();

            var URLVisor = (this.MiharuSession.Entidad.id == 9) ? Program.URLVisorImagenInterno : Program.URLVisorImagenExterno;
            Imagen_Iframe.Attributes["src"] = URLVisor + "?Token=" + token;

            //Registrar accion
            var QueryImg = "SELECT * FROM Imaging.TBL_File WHERE File_Unique_Identifier = '" + token + "'";
            Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, QueryImg, "", "");
        }

        #endregion

        #region Funciones

        public DataTable TransponerTabla(DataTable nData, int nDocumentoId)
        {
            var retData = new DataTable();
            retData.Columns.Add("Nombre", typeof(string));
            retData.Columns.Add("Valor", typeof(string));

            var hasData = (nData.Rows.Count > 0);

            foreach (DataColumn col in nData.Columns)
            {
                if (!Program.ConsultaCamposOcultos.Contains(col.ColumnName))
                {
                    var val = "";
                    if (hasData)
                    {
                        try
                        {
                            val = nData.Rows[0][col.ColumnName].ToString();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }

                    retData.Rows.Add(new[] { TransaccionDataCache.FindCampo(nDocumentoId, col.ColumnName), val });
                }
            }

            return retData;
        }

        #endregion
    }
}