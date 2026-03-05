using System;
using System.Collections.Generic;
using System.Web;
using Miharu.Security.Library.Session;
using DBAgrario;
using System.Data;
using DBAgrario.SchemaFirmas;
using Slyg.Tools;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.ConsultaFirmas
{
    public partial class Consulta_Firmas_Data : System.Web.UI.Page
    {
        #region Declaraciones

        private ParametrosConsulta Parametros = new ParametrosConsulta();
        public string Path_Nodo = "1";

        private static int UsuarioId
        {
            get { return ((Sesion)HttpContext.Current.Session["Session"]).Usuario.id; }
        }

        public Sesion MiharuSession
        {
            get { return (Sesion)Session["Session"]; }
        }

        private TypeConnectionString ConnectionString
        {
            get { return (TypeConnectionString)MiharuSession.Parameter["ConnectionStrings"]; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Session"] != null)
                ConsultarData();
        }

        #endregion

        #region Metodos

        public void ConsultarData()
        {
            var PageSize = 10;

            if (Request["rp"] != null) PageSize = int.Parse(Request["rp"]);

            var VerificaSize = PageSize;
            var VerificaSizeLength = VerificaSize.ToString().Length;
            if (VerificaSizeLength > 2)
            {
                var Val1 = int.Parse(VerificaSize.ToString().Substring(0, VerificaSizeLength / 2));
                var Val2 = int.Parse(VerificaSize.ToString().Substring((VerificaSizeLength / 2), VerificaSizeLength / 2));

                if (Val1 == Val2)
                    PageSize = Val1;
            }

            var PageNumber = 1;
            if (Request["page"] != null) PageNumber = int.Parse(Request["page"]);

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(UsuarioId);
               
                var FechaInicialMovimiento = Request["FechaInicialMov_Input"];
                FechaInicialMovimiento = (FechaInicialMovimiento != "") ? FechaInicialMovimiento.Replace("/", "") : "-1";
                var FechaFinalMovimiento = Request["FechaFinalMov_Input"];
                FechaFinalMovimiento = (FechaFinalMovimiento != "") ? FechaFinalMovimiento.Replace("/", "") : "-1";
                var FechaInicioProceso = Request["FechaInicialProc_Input"];
                FechaInicioProceso = (FechaInicioProceso != "") ? FechaInicioProceso.Replace("/", "") : "-1"; 
                var FechaFinProceso = Request["FechaFinalProc_Input"];
                FechaFinProceso = (FechaFinProceso != "") ? FechaFinProceso.Replace("/", "") : "-1"; 
                
                var OficinaNombre = (SlygNullable<string>)GetValorParametro("Oficina_Input", typeof(string));
                var TipoTransaccionNombre = (SlygNullable<string>)GetValorParametro("TipoTransaccion_Input", typeof(string));
                var CobName = (SlygNullable<string>)GetValorParametro("COB_Input", typeof(string));
                var Usuario = (SlygNullable<string>)GetValorParametro("Usuarios_Input", typeof(string));
                var Nro_Producto = (SlygNullable<string>)GetValorParametro("Nro_Producto_Input", typeof(string));
                var Nro_Ente = (SlygNullable<string>)GetValorParametro("Nro_Ente_Input", typeof(string));

                var COB = TransaccionDataCache.FindCOB(CobName);
                var Oficina = TransaccionDataCache.FindOficina(OficinaNombre);
                
                SlygNullable<short> CodigoCOB;
                if (COB == null)
                {
                    CodigoCOB = DBNull.Value;
                }
                else
                {
                    if (COB.id_COB.Value == 0)
                        CodigoCOB = -1;
                    else
                        CodigoCOB = (short)COB.id_COB;
                }
                SlygNullable<int> CodigoOficina = Oficina == null ? DBNull.Value : Oficina.id_Oficina;

                var data = dbmBanagrario.SchemaFirmas.PA_Consulta_Firmas_Proceso_Detalle.DBExecute(Convert.ToInt32(CodigoCOB),
                                                                                                  CodigoOficina,
                                                                                                  int.Parse(FechaInicialMovimiento),
                                                                                                  int.Parse(FechaFinalMovimiento),
                                                                                                  int.Parse(FechaInicioProceso),
                                                                                                  int.Parse(FechaFinProceso),
                                                                                                  Usuario,
                                                                                                  TipoTransaccionNombre,
                                                                                                  Nro_Producto,
                                                                                                  Nro_Ente,
                                                                                                  PageSize,
                                                                                                  PageNumber);
                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                Parametros.CodigoCOB = CodigoCOB;
                Parametros.CodigoOficina = CodigoOficina;
                Parametros.FechaInicio=FechaInicialMovimiento;
                Parametros.FechaFin=FechaFinalMovimiento;
                Parametros.FechaInicioP=FechaInicioProceso;
                Parametros.FechaFinP=FechaFinProceso;
                Parametros.Usuario=Usuario;
                Parametros.TipoTransaccionNombre=TipoTransaccionNombre;
                Parametros.Nro_Producto=Nro_Producto;
                Parametros.Nro_Ente=Nro_Ente;
                Parametros.PageSize=PageSize;
                Parametros.PageNumber=PageNumber;
                Parametros.cnx=ConnectionString.BanAgrario;
                Parametros.usrcnx = this.MiharuSession.Usuario.id;
                Parametros.usrlgn = this.MiharuSession.Usuario.Login;


                if (Oficina == null)
                    Session["Oficina"] = "--Todos--";
                else
                    Session["Oficina"] = Oficina.Nombre_Oficina;

                if (CobName.Value == null)
                    Session["COB"] = "--Todos--";
                else
                    Session["COB"] = CobName.Value;
               

                MiharuSession.Parameter["ParametrosConsulta"] = Parametros;
                WriteData(data, PageSize, PageNumber);
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                var a = ex.Message;
            }
            catch (Exception ex)
            {
                TraceError(ex);
                WriteData(null, PageSize, PageNumber);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        public void WriteData(CTA_Consulta_Firmas_Proceso_DetalleDataTable nProcesoData, int nPageSize, int nPageNumber)
        {
            var js = "";
            js = nProcesoData != null ? JSonGridDataConsulta.Instance().GenerateJsonGridData(nProcesoData, CTA_Consulta_Firmas_Proceso_DetalleEnum.id_Proceso_Detalle.ColumnName, nProcesoData[0].TotalReg, nPageSize, nPageNumber) : JSonGridDataConsulta.Instance().GenerateJsonGridData(null, CTA_Consulta_Firmas_Proceso_DetalleEnum.id_Proceso_Detalle.ColumnName, 0, nPageSize, nPageNumber);

            Response.Clear();
            Response.Write(js);
            Response.End();
        }

        private void TraceError(Exception ex)
        {
            Program.TraceError(ex);
        }

        #endregion

        #region Funciones

        private object GetValorParametro(string nCampo, Type nType)
        {
            return NullableHelper.Instance().ConvertToNullableValue(Request[nCampo], nType);
        }

        #endregion
    }

    public class JSonGridDataConsulta
    {
        private JSonGridDataConsulta() { }

        public static JSonGridDataConsulta Instance()
        {
            return new JSonGridDataConsulta();
        }

        public string GenerateJsonGridData(DataTable nData, string nColumnNameId, int nTotal, int nPageSize, int nPageNumber)
        {
            var rows = new Rows { page = nPageNumber, total = nTotal };
            if (nData != null)
            {
                for (var i = 0; i < nData.Rows.Count; i++)
                {
                    var r = new Row { id = nData.Rows[i][nColumnNameId].ToString(), cell = new List<string>() };
                    r.cell.AddRange(GetRowData(nData.Rows[i]));
                    rows.rows.Add(r);
                }
            }

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonresult = serializer.Serialize(rows);

            return jsonresult;
        }

        private IEnumerable<string> GetRowData(DataRow nRow)
        {
            var rowData = new List<string>();
            for (int i = 0; i < nRow.Table.Columns.Count; i++)
            {
                if (!Program.ConsultaCamposOcultos.Contains(nRow.Table.Columns[i].ColumnName))
                    rowData.Add(nRow[i].ToString());
            }

            return rowData;
        }
    }

    public class Rows
    {
        public int page { get; set; }
        public int total { get; set; }

        public List<Row> rows { get; set; }

        public Rows()
        {
            this.page = 0;
            this.total = 0;
            this.rows = new List<Row>();
        }
    }

    public class Row
    {
        public string id { get; set; }
        public List<string> cell { get; set; }

        public Row()
        {
            this.id = "";
            this.cell = new List<string>();
        }
    }
}