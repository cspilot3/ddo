using System;
using Miharu.Security.Library.Session;
using DBAgrario;
using System.Data;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.ConsultaFirmas
{
    public partial class VerTxLog : System.Web.UI.Page
    {
        #region Declaraciones

        public string Path_Nodo = "1.2";

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
                    MostrarTxDetalle();
                else
                    MensajeLabel.Text = "No se encontró una sesión activa";
            }
        }

        #endregion

        #region Metodos

        public void MostrarTxDetalle()
        {
            if (Request.Params["tx"] != null)
            {
                DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

                try
                {
                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                    dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);
                    var id_Cargue_Maestro = int.Parse(Request.Params["tx"]);
                    var datos = dbmBanagrario.SchemaFirmas.TBL_Cargue_Maestro.DBGet(id_Cargue_Maestro);

                    //Registrar accion
                    var query = dbmBanagrario.DataBase.LastQuery;
                    Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                    var datat = TransponerTabla(datos);
                    RegistroTx_GridView.DataSource = datat;
                    RegistroTx_GridView.DataBind();

                    if (datos.Count == 0)
                        MensajeLabel.Text = "No se encontraron datos para visualizar para el id: " + id_Cargue_Maestro.ToString();
                    else
                        MensajeLabel.Text = "";
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

        #endregion

        #region Funciones

        public DataTable TransponerTabla(DataTable nData)
        {
            var retData = new DataTable();

            retData.Columns.Add("Nombre", typeof(string));
            retData.Columns.Add("Valor", typeof(string));

            var hasData = (nData.Rows.Count > 0);

            foreach (DataColumn col in nData.Columns)
            {
                var val = "";
                if (hasData && !nData.Rows[0].IsNull(col.ColumnName))
                    val = nData.Rows[0][col.ColumnName].ToString();

                var nombre_columna = String.Empty;

                switch (col.ColumnName)
                {
                    case "fk_Cargue": nombre_columna = ""; break;
                    case "id_Cargue_Maestro": nombre_columna = "Numero de transacción"; break;
                    case "fk_Proceso_Maestro": nombre_columna = ""; break;
                    case "fk_fecha_Proceso": nombre_columna = ""; break;
                    case "Fecha_Movimiento": nombre_columna = "Fecha de Movimiento"; break;
                    case "Codigo_Oficina": nombre_columna = "Codigo de Oficina"; break;
                    case "Nombre_Oficina": nombre_columna = "Nombre de Oficina"; break;
                    case "Codigo_Transaccion": nombre_columna = "Codigo de Transaccion"; break;
                    case "Nombre_Transaccion": nombre_columna = "Nombre de Transaccion"; break;
                    case "Producto": nombre_columna = "Producto"; break;
                    case "Clase_Cuenta": nombre_columna = "Clase de Cuenta"; break;
                    case "Numero_Cuenta": nombre_columna = "Numero de Cuenta"; break;
                    case "Ente": nombre_columna = "Ente"; break;
                    case "Clase_Ente": nombre_columna = "Clase de Ente"; break;
                    case "Tipo_Persona": nombre_columna = "Tipo de Persona"; break;
                    case "Numero_Hojas_Tarjeta": nombre_columna = "Numero de Hojas deTarjeta"; break;
                    case "Digito_Verificacion": nombre_columna = "Digito de Verificacion"; break;
                    case "Usuario": nombre_columna = "Usuario"; break;
                    case "Excluido": nombre_columna = "Excluido"; break;
                    case "Cruzado": nombre_columna = "Cruzado"; break;
                    case "esFaltante": nombre_columna = ""; break;
                    default: nombre_columna = col.ColumnName; break;
                }

                if (val != "" && nombre_columna != "" && val != "0.00")
                    retData.Rows.Add(new object[] { nombre_columna, val });
            }

            return retData;
        }

        #endregion
    }
}