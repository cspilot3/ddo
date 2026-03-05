using System;
using System.Collections.Generic;
using System.Web;
using Miharu.Security.Library.Session;
using DBAgrario;
using System.Data;
using DBAgrario.SchemaProcess;
using Slyg.Tools;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Consulta
{
    public partial class Consulta_Data : System.Web.UI.Page
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

            try
            {
                var VerificaSize = PageSize;
                var VerificaSizeLength = VerificaSize.ToString().Length;
                if (VerificaSizeLength > 2)
                {
                    var Val1 = int.Parse(VerificaSize.ToString().Substring(0, VerificaSizeLength / 2));
                    var Val2 = int.Parse(VerificaSize.ToString().Substring((VerificaSizeLength / 2), VerificaSizeLength / 2));

                    if (Val1 == Val2)
                        PageSize = Val1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var PageNumber = 1;
            if (Request["page"] != null) PageNumber = int.Parse(Request["page"]);

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(UsuarioId);

                var FechaInicio = (SlygNullable<string>)GetValorParametro("FechaInicialProc_Input", typeof(string));
                var FechaFin = (SlygNullable<string>)GetValorParametro("FechaFinalProc_Input", typeof(string));
                var OficinaNombre = (SlygNullable<string>)GetValorParametro("Oficina_Input", typeof(string));
                var TipoTransaccionNombre = (SlygNullable<string>)GetValorParametro("TipoTransaccion_Input", typeof(string));
                var CobName = (SlygNullable<string>)GetValorParametro("COB_Input", typeof(string));

                if (FechaInicio.IsDbNull || FechaInicio.IsNull) throw new Exception("No se ha ingresado la fecha de inicio");
                if (FechaFin.IsDbNull || FechaFin.IsNull) throw new Exception("No se ha ingresado la fecha de fin");

                var COB = TransaccionDataCache.FindCOB(CobName);
                var Oficina = TransaccionDataCache.FindOficina(OficinaNombre);
                var Documento = TransaccionDataCache.FindDocumento(TipoTransaccionNombre);

                var CampoUno = (SlygNullable<string>)GetValorParametro("C_Campo_Uno", typeof(string));
                var CampoDos = (SlygNullable<string>)GetValorParametro("C_Campo_Dos", typeof(string));
                var CampoTres = (SlygNullable<string>)GetValorParametro("C_Campo_Tres", typeof(string));
                var CampoCuatro = (SlygNullable<string>)GetValorParametro("C_Campo_Cuatro", typeof(string));
                var CampoCinco = (SlygNullable<string>)GetValorParametro("C_Campo_Cinco", typeof(string));
                var CampoSeis = (SlygNullable<string>)GetValorParametro("C_Campo_Seis", typeof(string));
                var CampoSiete = (SlygNullable<string>)GetValorParametro("C_Campo_Siete", typeof(string));
                var CampoOcho = (SlygNullable<string>)GetValorParametro("C_Campo_Ocho", typeof(string));
                var CampoNueve = (SlygNullable<string>)GetValorParametro("C_Campo_Nueve", typeof(string));
                var CampoDiez = (SlygNullable<string>)GetValorParametro("C_Campo_Diez", typeof(string));
                var Codigo_Causal = (SlygNullable<string>)GetValorParametro("C_Codigo_Causal", typeof(string));
                var Comision = (SlygNullable<string>)GetValorParametro("C_Comision", typeof(string));
                var Key_04 = (SlygNullable<string>)GetValorParametro("C_Key_04", typeof(string));
                var Key_05 = (SlygNullable<string>)GetValorParametro("C_Key_05", typeof(string));
                var No_Chq_Gerencia = (SlygNullable<string>)GetValorParametro("C_Numero_Cheque_Gerencia", typeof(string));
                var No_Cta_Afectada = (SlygNullable<string>)GetValorParametro("C_Numero_Cuenta_Afectada", typeof(string));
                var Producto = (SlygNullable<string>)GetValorParametro("C_Producto", typeof(string));


                var EfectivoIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Efectivo_Ini", typeof(decimal));
                var EfectivoFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Efectivo_Fin", typeof(decimal));

                var ChequePropioIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Cheque propio_Ini", typeof(decimal));
                var ChequePropioFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Cheque propio_Fin", typeof(decimal));

                var ChequeLocalIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Cheque local_Ini", typeof(decimal));
                var ChequeLocalFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Cheque local_Fin", typeof(decimal));

                var ChequeGerenciaIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Cheque gerencia_Ini", typeof(decimal));
                var ChequeGerenciaFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Cheque gerencia_Fin", typeof(decimal));

                var NotaDebitoIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Nota debito_Ini", typeof(decimal));
                var NotaDebitoFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Nota debito_Fin", typeof(decimal));

                var NotaCreditoIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Nota credito_Ini", typeof(decimal));
                var NotaCreditoFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Nota credito_Fin", typeof(decimal));

                var RemesaNegociadaIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Remesa negociada_Ini", typeof(decimal));
                var RemesaNegociadaFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Remesa negociada_Fin", typeof(decimal));

                var RemesaCobroIni = (SlygNullable<decimal>)GetValorParametro("Vlr. Remesa al cobro_Ini", typeof(decimal));
                var RemesaCobroFin = (SlygNullable<decimal>)GetValorParametro("Vlr. Remesa al cobro_Fin", typeof(decimal));

                var ValorIni = (SlygNullable<decimal>)GetValorParametro("Valor_Ini", typeof(decimal));
                var ValorFin = (SlygNullable<decimal>)GetValorParametro("Valor_Fin", typeof(decimal));

                short CodigoCOB = -1;
                if (COB.id_COB.Value == 0)
                    CodigoCOB = -1;
                else
                    CodigoCOB = (short)COB.id_COB;

                int CodigoOficina;
                if (Oficina == null)
                    CodigoOficina = -1;
                else
                    CodigoOficina = Oficina.id_Oficina;

                int CodigoDocumento;
                if (Documento == null)
                    CodigoDocumento = -1;
                else
                    CodigoDocumento = Documento.id_Documento;

                var data = dbmBanagrario.SchemaProcess.PA_Consulta_Proceso_Detalle.DBExecute(
                    FechaInicio,
                    FechaFin,
                    CodigoOficina,
                    CodigoDocumento,
                    Producto,
                    Codigo_Causal,
                    CampoUno,
                    CampoDos,
                    CampoTres,
                    CampoCuatro,
                    CampoCinco,
                    CampoSeis,
                    CampoSiete,
                    CampoOcho,
                    CampoNueve,
                    CampoDiez,
                    ValorIni,
                    ValorFin,
                    EfectivoIni,
                    EfectivoFin,
                    ChequePropioIni,
                    ChequePropioFin,
                    ChequeLocalIni,
                    ChequeLocalFin,
                    ChequeGerenciaIni,
                    ChequeGerenciaFin,
                    NotaDebitoIni,
                    NotaDebitoFin,
                    NotaCreditoIni,
                    NotaCreditoFin,
                    RemesaNegociadaIni,
                    RemesaNegociadaFin,
                    RemesaCobroIni,
                    RemesaCobroFin,
                    No_Chq_Gerencia,
                    No_Cta_Afectada,
                    Comision,
                    Key_04,
                    Key_05,
                    PageSize,
                    PageNumber,
                    CodigoCOB);

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                Parametros.FechaInicio = FechaInicio;
                Parametros.FechaFin = FechaFin;
                Parametros.Oficina = CodigoOficina;
                Parametros.Documento = CodigoDocumento;
                Parametros.Producto = Producto;
                Parametros.Codigo_Causal = Codigo_Causal;
                Parametros.CampoUno = CampoUno;
                Parametros.CampoDos = CampoDos;
                Parametros.CampoTres = CampoTres;
                Parametros.CampoCuatro = CampoCuatro;
                Parametros.CampoCinco = CampoCinco;
                Parametros.CampoSeis = CampoSeis;
                Parametros.CampoSiete = CampoSiete;
                Parametros.CampoOcho = CampoOcho;
                Parametros.CampoNueve = CampoNueve;
                Parametros.CampoDiez = CampoDiez;
                Parametros.ValorIni = ValorIni;
                Parametros.ValorFin = ValorFin;
                Parametros.Efectivo_Fin = EfectivoFin;
                Parametros.Efectivo_Ini = EfectivoIni;
                Parametros.Chq_Local_Ini = ChequeLocalIni;
                Parametros.Chq_Local_Fin = ChequeLocalFin;
                Parametros.Chq_Propio_Ini = ChequePropioIni;
                Parametros.Chq_Propio_Fin = ChequePropioFin;
                Parametros.Chq_gerencia_Ini = ChequeGerenciaIni;
                Parametros.Chq_gerencia_Fin = ChequeGerenciaFin;
                Parametros.Nota_Debito_Ini = NotaDebitoIni;
                Parametros.Nota_Debito_Fin = NotaDebitoFin;
                Parametros.Nota_Credito_Ini = NotaCreditoIni;
                Parametros.Nota_Credito_Fin = NotaCreditoFin;
                Parametros.Remesa_Negociada_Ini = RemesaNegociadaIni;
                Parametros.Remesa_Negociada_Fin = RemesaNegociadaFin;
                Parametros.Remesa_al_Cobro_Ini = RemesaCobroIni;
                Parametros.Remesa_al_Cobro_Fin = RemesaCobroFin;
                Parametros.No_Cta_Afectada = No_Cta_Afectada;
                Parametros.Comision = Comision;
                Parametros.Key_04 = Key_04;
                Parametros.Key_05 = Key_05;
                Parametros.CodigoCOB = CodigoCOB;
                Parametros.MiharuSession = MiharuSession;

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
            catch (System.Threading.ThreadAbortException)
            {
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

        public void WriteData(CTA_Consulta_Proceso_DetalleDataTable nProcesoData, int nPageSize, int nPageNumber)
        {
            var js = "";
            js = nProcesoData != null ? JSonGridDataConsulta.Instance().GenerateJsonGridData(nProcesoData, CTA_Consulta_Proceso_DetalleEnum.id_Proceso_Detalle.ColumnName, nProcesoData[0].TotalReg, nPageSize, nPageNumber) : JSonGridDataConsulta.Instance().GenerateJsonGridData(null, CTA_Consulta_Proceso_DetalleEnum.id_Proceso_Detalle.ColumnName, 0, nPageSize, nPageNumber);

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
                for (int i = 0; i < nData.Rows.Count; i++)
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