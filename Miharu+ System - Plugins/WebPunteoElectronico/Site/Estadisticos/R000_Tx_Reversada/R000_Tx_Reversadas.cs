using System;
using System.Collections.Generic;
using WebPunteoElectronico.Clases;
using Slyg.Report;
using DBAgrario;
using System.Data;

namespace WebPunteoElectronico.Site.Estadisticos.R000_Tx_Reversada
{
    public class R000_Tx_Reversadas : WebGraph
    {
        #region Propiedades

        public override string ReportName
        {
            get { return "23-Transacciones reversadas en Log"; }
        }

        public override string ReportId
        {
            get { return "EstadisticoTransaccionesReversadas"; }
        }

        public override string ZipFileName
        {
            get { return "Estadistico_Transacciones_Reversadas.zip"; }
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

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);


                DataTable DatosDataTable = dbmBanagrario.SchemaReport.PA_Estadistico_Tx_Reversada.DBExecute(IdRegional,
                                                                                                                    idCOB,
                                                                                                                    IdOficina,
                                                                                                                    FechaMovimientoInicial,
                                                                                                                    FechaMovimientoFinal
                                                                                                                    );

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