using System;
using WebSantander.code;
using DBIntegration;
using Slyg.Tools;
using System.Collections.Generic;
using System.Web.UI;

namespace WebSantander.site.reportes
{
    public partial class reportes : page_form
    {
        #region Propiedades

        private WebReport WebReport
        {
            get { return this.Session[consts.SessionWebReport] as WebReport; }
            set { this.Session[consts.SessionWebReport] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void Config_Page()
        {
            string rpt = Request.Params["rpt"];
            this.MiharuSession.Parameter["pry"] = Request.Params["pry"];
            switch (rpt)
            {
                case "1":
                this.Master.Title = "Reporte Embargos y Desembargos";
                break;
                case "2":
                this.Master.Title = "Reporte Validación de Listas";
                break;
                case "3":
                this.Master.Title = "Reporte Facturación";
                break;
                case "4":
                this.Master.Title = "Reporte Facturación Detallado";
                break;
                case "5":
                this.Master.Title = "Reporte Cruce Embargos y Desembargos";
                break;
                case "6":
                this.Master.Title = "Reporte Cargues";
                break;
            }
            
            EditOptions.Add(Option.find);
        }

        #endregion

        #region Funciones

        public void Buscar(ScriptBuilder nHtml)
        {
            var fechaProcesoInicialInput = GetValue<DateTime>("fechaProcesoInicialInput", false);
            var fechaProcesoFinalInput = GetValue<DateTime>("fechaProcesoFinalInput", false);
            var reporteInput = GetValue<string>("reporteInput", false);
            var fechaProceso30 = fechaProcesoInicialInput.AddDays(30);
            var min = DateTime.MinValue;
            int rpt = int.Parse(reporteInput);

            try
            {
                if (fechaProcesoInicialInput > fechaProcesoFinalInput)
                    throw new Exception("La fecha de proceso final no puede ser superior a la fecha de proceso inicial.");

                if ((fechaProcesoInicialInput != min && fechaProcesoFinalInput == min) || (fechaProcesoInicialInput == min && fechaProcesoFinalInput != min))
                    throw new Exception("Faltan datos por ingresar (Fecha Proceso).");

                if (fechaProcesoFinalInput > fechaProceso30)
                    throw new Exception("La fecha de proceso final no puede ser superior a 30 Dias.");

                var fechaProcesoInicial = int.Parse(fechaProcesoInicialInput.ToString("yyyyMMdd"));
                var fechaProcesoFinal = int.Parse(fechaProcesoFinalInput.ToString("yyyyMMdd"));
                
                site.reportes.ReportesWeb Reporte = new ReportesWeb();
                Reporte.CadenaConexion = this.ConnectionString.Santander;
                Reporte.IdUsuario = this.MiharuSession.Usuario.id;
                Reporte.Reporte = rpt;
                Reporte.FechaProcesoInicial = fechaProcesoInicial;
                Reporte.FechaProcesoFinal = fechaProcesoFinal;
                Reporte.Entidad = this.MiharuSession.Entidad.id;
                Reporte.Proyecto = short.Parse(this.MiharuSession.Parameter["pry"].ToString());
                
                this.WebReport = Reporte;
            }
            catch (Exception ex)
            {
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
        }

        #endregion

    }
}