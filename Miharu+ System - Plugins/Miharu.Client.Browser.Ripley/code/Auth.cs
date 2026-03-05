namespace Miharu.Client.Browser.code
{
    public static class Auth
    {
        public const string Path = "*";

        public class Administracion
        {
            public const string Path = "1";

            public class Seguridad
            {
                public const string Path = "1";
                public const string Perfiles = "1.2";
                public const string Roles = "1.2";
                public const string Usuarios = "1.1";
            }
        }

        public class Consultas
        {
            public const string Path = "2";

            public const string Consulta = "2.1";
        }

        public class Informes
        {
            public const string Path = "3";

            public const string Resumen_Movimiento = "3.1";
        }

        
    }
}