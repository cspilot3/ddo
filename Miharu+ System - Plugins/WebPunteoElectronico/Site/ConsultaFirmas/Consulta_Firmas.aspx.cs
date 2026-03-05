using System;
using System.Collections.Generic;
using System.Web;
using DBAgrario;
using DBAgrario.SchemaConfig;
using DBAgrario.SchemaFirmas;
using System.Web.Services;
using System.Text;
using System.Data;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.ConsultaFirmas
{
    public partial class Consulta_Firmas : FormBase
    {
        #region Declaraciones

        private string ColumnsToShow = CTA_Consulta_Firmas_Proceso_DetalleEnum.id_Cargue_Detalle.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.id_Proceso_Detalle.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Codigo_Regional.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Codigo_COB.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Codigo_Oficina.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Fecha_Movimiento.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Fecha_Proceso.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Nombre_Cob.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Nombre_Oficina.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Producto.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Codigo_Transaccion.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Nombre_Transaccion.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Numero_Cuenta.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Usuario.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Ente.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.tipo.ColumnName + "," +
                                       CTA_Consulta_Firmas_Proceso_DetalleEnum.Url.ColumnName;

        #endregion

        #region Propiedades

        public AutoListHelper<TBL_TransaccionDataTable, TBL_TransaccionEnum, TBL_TransaccionRow> TransaccionFirmas
        {
            get { return GetSessionValue<AutoListHelper<TBL_TransaccionDataTable, TBL_TransaccionEnum, TBL_TransaccionRow>>("TransaccionFirmas"); }
            set { SetSessionValue("TransaccionFirmas", value); }
        }
        
        private static List<short> ListasCliente
        {
            get
            {
                if (HttpContext.Current.Session["__ListasCliente_"] == null)
                    HttpContext.Current.Session["__ListasCliente_"] = new List<short>();

                return (List<short>)HttpContext.Current.Session["__ListasCliente_"];
            }
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

            Master.ShowTitle = false;
            Master.Title = "";

        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            CargarTransaccionDataCache();
        }

        protected override void Load_Data()
        {
        }

        private static void AdicionarListaEnCliente(short nListaId)
        {
            if (!ListasCliente.Contains(nListaId))
                ListasCliente.Add(nListaId);
        }

        private void CargarTransaccionDataCache()
        {
            if (TransaccionDataCache.OficinaData.Count == 0 || this.Request["ForzarConfig"] != null)
            {
                DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

                try
                {
                    dbmBanagrario = new DBAgrarioDataBaseManager(this.ConnectionString.BanAgrario);
                    dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                    TransaccionFirmas.Init(dbmBanagrario.SchemaFirmas.TBL_Transaccion.DBGet(null), TBL_TransaccionEnum.Nombre_Transaccion);
                    TransaccionDataCache.UpdateCacheCOB(dbmBanagrario.SchemaConfig.CTA_Regional_COB_Concatenacion.DBFindByid_COBfk_Regional(null, null, 0, new CTA_Regional_COB_ConcatenacionEnumList(CTA_Regional_COB_ConcatenacionEnum.Nombre_COB, true)));
                    TransaccionDataCache.UpdateCacheDocumento(dbmBanagrario.SchemaConfig.CTA_Documento_Concatenacion.DBFindByfk_Entidadfk_Proyecto(Program.EntidadId, Program.ProyectoId, 0, new CTA_Documento_ConcatenacionEnumList(CTA_Documento_ConcatenacionEnum.Nombre_Documento, true)));
                    TransaccionDataCache.UpdateCacheCampos(dbmBanagrario.SchemaConfig.CTA_Consulta_Config_Campo.DBFindByfk_Entidad(Program.EntidadId));

                    var listaItems = dbmBanagrario.SchemaCore.CTA_Config_Campo_Lista.DBFindByfk_Entidad(Program.EntidadId);
                    listaItems.AddCTA_Config_Campo_ListaRow(Program.EntidadId, 0, 1, "Si", "1");
                    listaItems.AddCTA_Config_Campo_ListaRow(Program.EntidadId, 0, 2, "No", "0");
                    TransaccionDataCache.UpdateCacheListaItems(listaItems);
                }
                catch (Exception ex)
                {
                    Program.TraceError(ex);
                    this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconWarning);
                }
                finally
                {
                    if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                }
            }
        }

        #endregion

        #region Funciones

        [WebMethod]
        public static string MostrarOficinas(string nStrCOB)
        {
            try
            {
                DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
                var mySession = (Miharu.Security.Library.Session.Sesion)HttpContext.Current.Session["Session"];
                var cnnStr = (TypeConnectionString)mySession.Parameter["ConnectionStrings"];

                try
                {
                    dbmBanagrario = new DBAgrarioDataBaseManager(cnnStr.BanAgrario);
                    dbmBanagrario.Connection_Open(mySession.Usuario.id);

                    var COB = short.Parse(nStrCOB.Split(' ')[0]);

                    TransaccionDataCache.UpdateCacheOficina(dbmBanagrario.SchemaConfig.CTA_Regional_COB_Oficina_Concatenacion.DBFindByid_COB(COB, 0, new CTA_Regional_COB_Oficina_ConcatenacionEnumList(CTA_Regional_COB_Oficina_ConcatenacionEnum.Nombre_Oficina, true)));
                }
                catch (Exception ex)
                {
                    Program.TraceError(ex);
                }
                finally
                {
                    if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                }

                return GetOficinasJavascript();
            }
            catch
            {
                return "[]";
            }
        }

        [WebMethod]
        public static string MostrarCampos(string tipotransaccion)
        {
            try
            {
                var mySession = (Miharu.Security.Library.Session.Sesion)HttpContext.Current.Session["Session"];

                if (mySession != null)
                {
                    var lista_campos = "";
                    var fecha_campos = "";

                    var html = new StringBuilder();
                    html.AppendLine("<table id='Campos_Table' name='Campos_Table'>");

                    var documento = TransaccionDataCache.FindDocumento(tipotransaccion);

                    if (documento != null)
                    {
                        var campos = TransaccionDataCache.FindCampos(documento);
                        int MediosPago = 0;

                        foreach (var campo in campos)
                        {
                            if ((campo.Nombre_Campo == "Vlr. Efectivo") ||
                                (campo.Nombre_Campo == "Vlr. Cheque local") ||
                                (campo.Nombre_Campo == "Vlr. Cheque propio") ||
                                (campo.Nombre_Campo == "Vlr. Cheque gerencia") ||
                                (campo.Nombre_Campo == "Vlr. Remesa negociada") ||
                                (campo.Nombre_Campo == "Vlr. Remesa al cobro") ||
                                (campo.Nombre_Campo == "Vlr. Nota debito") ||
                                (campo.Nombre_Campo == "Vlr. Nota credito"))
                            {
                                html.AppendLine("<tr><td class='td1'>" + campo.Nombre_Campo + " entre</td><td><input type='text' id='" + campo.Nombre_Campo + "_Ini' name='" + campo.Nombre_Campo + "_Ini' value='' class='cinp' onblur=\"formatNumber(this,'$');\" onfocus=\"LimpiaControl(this);\"      /></td><td>y</td><td><input type='text' id='" + campo.Nombre_Campo + "_Fin' name='" + campo.Nombre_Campo + "_Fin' value='' class='cinp' onblur=\"formatNumber(this,'$');\" onfocus=\"LimpiaControl(this);\"/></td></tr>");
                                MediosPago += 1;
                            }
                            else
                            {
                                if (campo.Nombre_Columna.ToUpper() == "VALOR" ||
                                    campo.Nombre_Columna.ToUpper() == "MONTO")
                                {
                                    MediosPago = 0;
                                }
                                else
                                {
                                    var campoId = "C_" + campo.Nombre_Columna;

                                    short listaId;
                                    if (campo.fk_Campo_Tipo == 3)
                                    {
                                        if (fecha_campos != "")
                                            fecha_campos += ",";

                                        fecha_campos += campoId;
                                    }
                                    else if (campo.fk_Campo_Tipo == 4)
                                    {
                                        listaId = 0;
                                        AdicionarListaEnCliente(0);

                                        if (lista_campos != "")
                                            lista_campos += ",";

                                        lista_campos += campoId + ":" + "0";
                                    }
                                    else if (campo.fk_Campo_Tipo == 5 && !campo.Isfk_Campo_ListaNull())
                                    {
                                        listaId = campo.fk_Campo_Lista;
                                        AdicionarListaEnCliente(listaId);

                                        if (lista_campos != "")
                                            lista_campos += ",";

                                        lista_campos += campoId + ":" + listaId;
                                    }

                                    html.AppendLine("<tr><td class='td1'>" + campo.Nombre_Campo + " </td><td><input type='text' id='" + campoId + "' name='" + campoId + "' value='' class='cinp'></td></tr>");
                                }
                            }
                        }

                        if (MediosPago < 1)
                            html.AppendLine("<tr><td class='td1'>Valor entre</td><td><input type='text' id='Valor_Ini' name='Valor_Ini' value='' class='cinp' onblur=\"formatNumber(this,'$');\" onfocus=\"LimpiaControl(this);\"      ></td><td>y</td><td><input type='text' id='Valor_Fin' name='Valor_Fin' value='' class='cinp' onblur=\"formatNumber(this,'$');\" onfocus=\"LimpiaControl(this);\"></td></tr>");
                    }
                    html.AppendLine("</table>");

                    var valoresLista = ObtenerValoresDeListasEnCliente();

                    html.AppendLine("<input type='hidden' id='fecha_campos' value='" + fecha_campos + "'>");
                    html.AppendLine("<input type='hidden' id='lista_campos' value='" + lista_campos + "'>");
                    html.AppendLine("<input type='hidden' id='lista_data' value='" + valoresLista + "'>");

                    return html.ToString();
                }
            }
            catch (Exception ex)
            {
                Program.TraceError(ex);
            }

            return "";
        }

        private static string ObtenerValoresDeListasEnCliente()
        {
            var scr = "";

            foreach (var listaid in ListasCliente)
            {
                if (scr != "") scr += ";";

                var items = TransaccionDataCache.FindListaItems(listaid);

                var srcLista = "";
                foreach (var item in items)
                {
                    if (srcLista != "")
                        srcLista += ",";

                    srcLista += FormatearValorLista(item.Valor_Campo_Lista_Item) + ":" + FormatearValorLista(item.Etiqueta_Campo_Lista_Item);
                }

                scr += listaid.ToString() + "=" + srcLista;
            }

            return scr;
        }

        private static string FormatearValorLista(string nValor)
        {
            return nValor.Replace(":", "").Replace(";", "").Replace(",", "");
        }

        //private static string FormatIdentifier(string nId)
        //{
        //    return nId.Replace(" ", "");
        //}

        public string GetCOBSJavascript()
        {
            var script = "";

            foreach (var row in TransaccionDataCache.COBData)
            {
                if (script != "")
                    script += ",";

                script += "'" + FormatearValorLista(row.Nombre_COB) + "'";
            }

            script = "[" + script + "]";

            return script;
        }

        public static string GetOficinasJavascript()
        {
            var script = "";

            foreach (var row in TransaccionDataCache.OficinaData)
            {
                if (script != "")
                    script += ",";

                script += "'" + FormatearValorLista(row.Nombre_Oficina) + "'";
            }

            script = "[" + script + "]";

            return script;
        }


        public string GetTransaccionFirmas()
        {
            var script = "";

            foreach (TBL_TransaccionRow tx in TransaccionFirmas.Data)
            {
                var row = tx.Nombre_Transaccion;

                if (script != "")
                    script += ",";

                script += "'" + FormatearValorLista(row.Trim()) + "'";
            }
            script = "[" + script + "]";
            return script;
        }


        public string GetTiposTransaccion()
        {
            var script = "";

            foreach (var row in TransaccionDataCache.DocumentoData)
            {
                if (script != "")
                    script += ",";

                script += "'" + FormatearValorLista(row.Nombre_Documento) + "'";
            }

            script = "[" + script + "]";

            return script;
        }

        public string GetGrillaColModel()
        {
            var script = "";

            foreach (DataColumn col in new CTA_Consulta_Firmas_Proceso_DetalleDataTable().Columns)
            {
                if (!Program.ConsultaCamposOcultos.Contains(col.ColumnName))
                {
                    if (script != "")
                        script += ",";

                    var hideStr = ", hide: true";

                    if (ColumnsToShow != null && ColumnsToShow.IndexOf(col.ColumnName, System.StringComparison.Ordinal) >= 0)
                        hideStr = "";

                    script += "{ display: '" + col.ColumnName + "', name: '" + col.ColumnName + "', width: 130, sortable: false " + hideStr + "}";
                }
            }

            script = "[" + script + "]";

            return script;
        }

        //private object GetValorParametro(string nCampo, Type nType)
        //{
        //    return NullableHelper.Instance().ConvertToNullableValue(this.Request[nCampo], nType);
        //}

        #endregion

    }
}