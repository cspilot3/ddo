using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSantander
{
    public class Reports
    {
        public static class site
        {
            public static class Reportes
            {
                public static class Reporte_Embargos_Desembargos
                {
                    public const string Reporte_Embargo_Desembargo = "~/site/reportes/Plantillas/ReportEmbargosDesembargos.rdlc";
                }

                public static class Reporte_Validacion_Listas
                {
                    public const string Reporte_Validacion_Lista = "~/site/reportes/Plantillas/ReportValidacionListas.rdlc";
                }

                public static class Reporte_Facturaciones
                {
                    public const string Reporte_Facturacion = "~/site/reportes/Plantillas/ReportFacturacion.rdlc";
                }

                public static class Reporte_Facturacion_Detallado
                {
                    public const string Reporte_Facturacion_Detallada = "~/site/reportes/Plantillas/ReportFacturacionDetallado.rdlc";
                }

                public static class Reporte_Cruces
                {
                    public const string Reporte_Cruce = "~/site/reportes/Plantillas/ReportCruce.rdlc";
                }

                public static class Reporte_Cargues
                {
                    public const string Reporte_Cargue = "~/site/reportes/Plantillas/ReportCargue.rdlc";
                }
            }
        }
    }
}