namespace WebSantander.code
{
    public static class Auth
    {
        public const string Path = "*";
        
        public class Consultas
        {
            public const string Path = "1";

            public const string Consulta = "1.1";
        }

        public class Reporte
        {
            public const string Path = "2";

            public class Reportes
            {
                public const string Path = "2";
                public const string Embargos_Desembargos = "2.1";
                public const string Validacion_Listas = "2.2";
                public const string Facturacion = "2.3";
                public const string Facturacion_Detallado = "2.4";
                public const string Cruce = "2.5";
            }
        }


        
    }
}