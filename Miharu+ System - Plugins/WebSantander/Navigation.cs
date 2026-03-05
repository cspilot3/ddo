using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSantander
{
    public class Navigation
    {
        public static class controls
        {
            public const string proxy_data = "~/controls/proxy_data.aspx";
        }
        public static class site
        {
            public const string main = "~/site/main.aspx";
            public static class account
            {
                public const string change_password = "~/site/account/change_password.aspx";
                public const string connect = "~/site/account/connect.aspx";
                public const string forgotten_password = "~/site/account/forgotten_password.aspx";
                public const string login = "~/site/account/login.aspx";
                public const string restore_password = "~/site/account/restore_password.aspx";
            }
            public static class application
            {
                public const string about = "~/site/application/about.aspx";
                public const string dashboard = "~/site/application/dashboard.aspx";
            }
            public static class consulta
            {
                public const string consultas = "~/site/consulta/consultas.aspx";
            }
            public static class reporte
            {
                public const string general = "~/site/reportes/reportes.aspx";
                //public const string ReporteEmbargoDesembargo = "~/site/reportes/reportes.aspx?rpt=1";
                public const string ReporteEmbargos = "~/site/reportes/reportes.aspx?rpt=1&pry=2";
                public const string ReporteDesembargos = "~/site/reportes/reportes.aspx?rpt=1&pry=3";
                public const string ReporteValidacionListas = "~/site/reportes/reportes.aspx?rpt=2&pry=1";
                //public const string ReporteFacturacion = "~/site/reportes/reportes.aspx?rpt=3";
                public const string ReporteFacturacionValidacionListas = "~/site/reportes/reportes.aspx?rpt=3&pry=1";
                public const string ReporteFacturacionEmbargos = "~/site/reportes/reportes.aspx?rpt=3&pry=2";
                public const string ReporteFacturacionDesembargos = "~/site/reportes/reportes.aspx?rpt=3&pry=3";
                //public const string ReporteFacturacionDetallado = "~/site/reportes/reportes.aspx?rpt=4";
                public const string ReporteFacturacionDetalladoValidacionListas = "~/site/reportes/reportes.aspx?rpt=4&pry=1";
                public const string ReporteFacturacionDetalladoEmbargos = "~/site/reportes/reportes.aspx?rpt=4&pry=2";
                public const string ReporteFacturacionDetalladoDesembargos = "~/site/reportes/reportes.aspx?rpt=4&pry=3";
                //public const string ReporteCruce = "~/site/reportes/reportes.aspx?rpt=5";
                public const string ReporteCruceEmbargos = "~/site/reportes/reportes.aspx?rpt=5&pry=2";
                public const string ReporteCruceDesembargos = "~/site/reportes/reportes.aspx?rpt=5&pry=3";
                //public const string ReporteCargue = "~/site/reportes/reportes.aspx?rpt=6";
                public const string ReporteCargueValidacionListas = "~/site/reportes/reportes.aspx?rpt=6&pry=1";
                public const string ReporteCargueEmbargos = "~/site/reportes/reportes.aspx?rpt=6&pry=2";
                public const string ReporteCargueDesembargos = "~/site/reportes/reportes.aspx?rpt=6&pry=3";
            }
        }
    }
}