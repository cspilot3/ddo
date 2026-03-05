using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using Slyg.Report;
using DBAgrario;
using System.Data;


namespace WebPunteoElectronico.Site.Estadisticos.R003_Documento_Remitido_Sin_Ordenamiento_Definido
{
    public class R003_Documentos_Remitidos_Sin_Ordenamiento_Definido : WebGraph
    {
        #region Declaraciones

        private int IdUsuario;
        public string Path_Nodo = "7.1.3";
        public string Query = "";

        #endregion

        #region Propiedades

        public override string ReportName
        {
            get { return "Documentos remitidos sin el ordenamiento definido"; }
        }

        public override string ReportId
        {
            get { return "EstadisticoReportedocumentosremitidossinordenamientodefinido"; }
        }

        public override string ZipFileName
        {
            get { return "Documentos_remitidos_sin_ordenamiento_definido.zip"; }
        }

        public override List<WebGraph.ModoParametrosEnum> ModoParametros
        {
            get { return new List<WebGraph.ModoParametrosEnum>() { WebGraph.ModoParametrosEnum.Movimiento }; }
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
            var Login = (string)nParameters["Login"];
            IdUsuario = (int)nParameters["IdUsuario"];

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);


                DataTable DatosDataTable = dbmBanagrario.SchemaReport.PA_Estadistico_03_Documentos_Remitidos_Sin_Ordenamiento_Definido.DBExecute(IdRegional,
                                                                                                                    idCOB,
                                                                                                                    IdOficina,
                                                                                                                    FechaMovimientoInicial,
                                                                                                                    FechaMovimientoFinal
                                                                                                                    );

                //Registrar accion
                Query= dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(IdUsuario, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, Query, "", "");

                base.DataReporte.Datos = DatosDataTable;
                base.DataReporte.EjeX = "Oficinas";
                base.DataReporte.EjeY = "Tx";
                
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
            // validar de Fecha Proceso

            var FechaInicialS = (string)nParameters["FechaMovimientoInicial"];
            var FechaFinalS = (string)nParameters["FechaMovimientoFinal"];

            var FechaInicialD = Slyg.Tools.DataConvert.ToDate(FechaInicialS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');
            var FechaFinalD = Slyg.Tools.DataConvert.ToDate(FechaFinalS, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/');

            if ((FechaFinalD.Value - FechaInicialD.Value).Days > 30)
            {
                nMessageError = "El rango de Fechas de movimiento debe ser menor o igual a 30 días";
                return false;
            }

            nMessageError = "";
            return true;
        }

        #endregion
    }
}