using Miharu.Security.Library.Licence;
using System;

namespace Miharu.Security.Library.Session
{
    [Serializable]
    public class Usuario
    {
        #region Propiedades

        public int id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
        public bool isRoot { get; set; }
        public PerfilManager PerfilManager { get; private set; }

        #endregion

        #region Constructores

        public Usuario()
        {
            this.PerfilManager = new PerfilManager();
        }

        #endregion
    }
}