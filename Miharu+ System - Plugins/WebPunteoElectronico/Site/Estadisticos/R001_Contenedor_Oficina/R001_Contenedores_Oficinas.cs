using System;
using System.Collections.Generic;
using System.Data;
using DBAgrario;
using Slyg.Report;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Estadisticos.R001_Contenedor_Oficina
{
    public class R001_Contenedores_Oficinas : WebGraph
    {

        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "7.1.1";
        public string Query = "";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "01-Contenedor Oficinas"; }
        }

        public override string ReportId
        {
            get { return "ContenedorOficina1"; }
        }

        public override string ZipFileName
        {
            get { return "estadistico_contenedor_oficinas.zip"; }
        }

        public override List<WebGraph.ModoParametrosEnum> ModoParametros
        {
            get { return new List<WebGraph.ModoParametrosEnum>() { WebGraph.ModoParametrosEnum.Movimiento, WebGraph.ModoParametrosEnum.Proceso, WebGraph.ModoParametrosEnum.Mixto }; }
        }

        #endregion

        #region Metodos

        public override void Draw(ref System.Web.UI.WebControls.Literal nLiteral, TipoReporteEnum nTipo, short nWidth, short nHeight)
        {
            base.DataReporte.Tipo_Reporte = ReportGenerator.getTipoReporteEnumString(nTipo);
            nLiteral.Text = ReportGenerator.CreateGraph(base.DataReporte, nWidth, nHeight);
        }

        #endregion

        #region Funciones

        public override bool Load(Dictionary<string, object> nParameters)
        {
            var ConnectionString = (TypeConnectionString)nParameters["ConnectionString"];
            var IdRegional = (short)nParameters["IdRegional"];
            var NombreRegional = (string)nParameters["NombreRegional"];
            var idCOB = (short)nParameters["IdCOB"];
            var NombreCOB = (string)nParameters["NombreCOB"];
            var IdOficina = (int)nParameters["IdOficina"];
            var NombreOficina = (string)nParameters["NombreOficina"];
            var FechaMovimientoInicial = (string)nParameters["FechaMovimientoInicial"];
            var FechaMovimientoFinal = (string)nParameters["FechaMovimientoFinal"];
            var FechaProcesoInicial = (string)nParameters["FechaProcesoInicial"];
            var FechaProcesoFinal = (string)nParameters["FechaProcesoFinal"];
            int Modo = (int)nParameters["Modo"];
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];
            
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

                try
                {
                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                    dbmBanagrario.Connection_Open(1);


                    DataTable DatosDataTable = dbmBanagrario.SchemaReport.PA_Estadistico_01_Contenedor_Oficina.DBExecute(IdRegional,
                                                                                                                        idCOB,
                                                                                                                        IdOficina,
                                                                                                                        FechaMovimientoInicial,
                                                                                                                        FechaMovimientoFinal,
                                                                                                                        FechaProcesoInicial,
                                                                                                                        FechaProcesoFinal,
                                                                                                                        Modo);

                    //Registrar accion
                    Query= dbmBanagrario.DataBase.LastQuery;
                    Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, Query, "", "");

                    base.DataReporte.Datos = DatosDataTable;
                    base.DataReporte.EjeX = "Oficinas";
                    base.DataReporte.EjeY = "Contenedores Por Oficina";
                    
                    base.DataReporte.Id_Reporte = this.ReportId;
                    base.DataReporte.Nombre_Reporte = this.ReportName;
                    base.DataReporte.PrecisionDecimal = 0;
                    
                    base.DataReporte.CampoMostrarX = "Nombre_Oficina";

                    return DatosDataTable.Rows.Count > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                }
        }

        public override byte[] BuildZipData()
        {
            //Registrar accion
            Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, Query, "", "");

            return ReportGenerator.BuildZipData(this.DataReporte.Datos, this.ZipFileName);                        
        }

        public override bool Validate(Dictionary<string, object> nParameters, out string nMessageError)
        {
            int Modo = (int)nParameters["Modo"];

            if (Modo != 2) // validar de Fecha Proceso
            {
                var FechaInicialS = (string)nParameters["FechaMovimientoInicial"];
                var FechaFinalS = (string)nParameters["FechaMovimientoFinal"];

                var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
                var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

                if ((FechaFinalD.Value - FechaInicialD.Value).Days > 30)
                {
                    nMessageError = "El rango de Fechas de movimiento debe ser menor o igual a 30 días";
                    return false;
                }
            }

            if (Modo != 1) // Validar Fecha de Movimiento
            {
                string FechaInicialS = (string)nParameters["FechaProcesoInicial"];
                string FechaFinalS = (string)nParameters["FechaProcesoFinal"];

                var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
                var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

                if ((FechaFinalD.Value - FechaInicialD.Value).Days > 30)
                {
                    nMessageError = "El rango de Fechas de proceso debe ser menor o igual a 30 días";
                    return false;
                }
            }

            nMessageError = "";
            return true;
        }

        #endregion 
    }
}