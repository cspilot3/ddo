using System;

namespace Miharu.Security.Library.Session
{
    [Serializable]
    public class Entidad
    {
        #region Propiedades

        public short id { get; set; }

        public short idGrupo { get; set; }

        public string Grupo { get; set; }

        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string NIT { get; set; }

        public string Contacto { get; set; }

        public string Telefono { get; set; }

        #endregion
    }
}