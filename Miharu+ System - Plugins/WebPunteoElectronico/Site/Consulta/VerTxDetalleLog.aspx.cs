using System;
using Miharu.Security.Library.Session;
using DBAgrario;
using System.Data;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Consulta
{
    public partial class VerTxDetalleLog : System.Web.UI.Page
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
                    var id_Archivo_Detalle = long.Parse(Request.Params["tx"]);
                    var datos = dbmBanagrario.SchemaReport.TBL_Archivo_Detalle.DBGet(id_Archivo_Detalle);

                    //Registrar accion
                    var query = dbmBanagrario.DataBase.LastQuery;
                    Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                    var datat = TransponerTabla(datos);
                    RegistroTx_GridView.DataSource = datat;
                    RegistroTx_GridView.DataBind();

                    if (datos.Count == 0)
                        MensajeLabel.Text = "No se encontraron datos para visualizar para el id: " + id_Archivo_Detalle.ToString();
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
                    case "id_Archivo_Detalle": nombre_columna = "Numero de Transacción"; break;
                    case "fk_Archivo": nombre_columna = "Numero de Log"; break;
                    case "fk_Oficina_Archivo": nombre_columna = "Codigo de Oficina"; break;
                    case "Fecha_Movimiento": nombre_columna = "Fecha de Proceso"; break;
                    case "Producto": nombre_columna = "Producto"; break;
                    case "Codigo_Tx": nombre_columna = "Codigo Transaccion (Cobis)"; break;
                    case "Nombre_Tx": nombre_columna = "Nombre Transaccion"; break;
                    case "Codigo_Causal": nombre_columna = "Codigo Causal"; break;
                    case "Nombre_Causal": nombre_columna = "Nombre Causal"; break;
                    case "Tipo_Transaccion": nombre_columna = "Tipo de Transaccion"; break;
                    case "Campo_Uno": nombre_columna = "Campo Uno"; break;
                    case "Campo_Dos": nombre_columna = "Campo Dos"; break;
                    case "Campo_Tres": nombre_columna = "Campo Tres"; break;
                    case "Campo_Cuatro": nombre_columna = "Campo Cuatro"; break;
                    case "Campo_Cinco": nombre_columna = "Campo Cinco"; break;
                    case "Campo_Seis": nombre_columna = "Campo Seis"; break;
                    case "Campo_Siete": nombre_columna = "Campo Siete"; break;
                    case "Campo_Ocho": nombre_columna = "Campo Ocho"; break;
                    case "Campo_Nueve": nombre_columna = "Campo Nueve"; break;
                    case "Campo_Diez": nombre_columna = "Campo Diez"; break;
                    case "MP_Efectivo": nombre_columna = "Efectivo"; break;
                    case "MP_Cheque_Propio": nombre_columna = "Cheque Propio"; break;
                    case "MP_Cheque_Local": nombre_columna = "Cheque Local"; break;
                    case "MP_Cheque_Gerencia": nombre_columna = "Cheque Gerencia"; break;
                    case "MP_Remesa_Negociada": nombre_columna = "Remesa Negociada"; break;
                    case "MP_Remesa_Cobro": nombre_columna = "Remesa al Cobro"; break;
                    case "Sebra": nombre_columna = "Sebra"; break;
                    case "MP_Nota_Debito": nombre_columna = "Nota Debito"; break;
                    case "MP_Nota_Credito": nombre_columna = "Nota Credito"; break;
                    case "Numero_Cheque_Gerencia": nombre_columna = "Numero de Cheque de Gerencia"; break;
                    case "Numero_Cuenta_Afectada": nombre_columna = "Numero de Cuenta Afectda"; break;
                    case "Naturaleza_Medio_pago": nombre_columna = "Naturaleza de Medio de Pago"; break;
                    case "Usuario": nombre_columna = "Usuario"; break;
                    case "Ticket": nombre_columna = "Ticket"; break;
                    case "Timbre": nombre_columna = "Timbre"; break;
                    case "Tx_Caja_Extendida": nombre_columna = "Transaccion de Caja Extendida"; break;
                    case "Valor": nombre_columna = "Valor"; break;
                    case "Comision": nombre_columna = "Comision"; break;
                    case "Estado_Tx": nombre_columna = "Estado de la transacción"; break;
                    case "Codigo_Producto": nombre_columna = "Código del Producto"; break;
                    case "Materializada": nombre_columna = "Materializada"; break;
                    case "Fecha_Disponible_Cruce": nombre_columna = "Fecha Disponible Cruce"; break;
                    case "Comprobado": nombre_columna = "Comprobado"; break;
                    case "fk_Oficina": nombre_columna = "Codigo Oficina Real"; break;
                    case "Codigo_Juzgado_Original": nombre_columna = "Juzgado Original"; break;
                    case "Cruzar": nombre_columna = "Cruzar"; break;
                    case "fk_Proceso": nombre_columna = "Proceso"; break;
                    case "fk_Core_Index": nombre_columna = "Core Index"; break;
                    case "Es_Log": nombre_columna = ""; break;
                    case "Key_01": nombre_columna = "Llave 01"; break;
                    case "Key_02": nombre_columna = "Llave 02"; break;
                    case "Key_03": nombre_columna = "Llave 03"; break;
                    case "Key_04": nombre_columna = "Llave 04"; break;
                    case "Key_05": nombre_columna = "Llave 05"; break;
                    case "Key_06": nombre_columna = "Llave 06"; break;
                    case "Key_07": nombre_columna = "Llave 07"; break;
                    case "Key_08": nombre_columna = "Llave 08"; break;
                    case "Key_09": nombre_columna = "Llave 09"; break;
                    case "Key_10": nombre_columna = "Llave 10"; break;
                    case "Excluir_Llaves": nombre_columna = "Excluir Llaves"; break;
                    case "fk_Motivo_Exclusion": nombre_columna = "Motivo Exclusion"; break;
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