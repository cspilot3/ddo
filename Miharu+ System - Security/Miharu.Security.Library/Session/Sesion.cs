using System;

namespace Miharu.Security.Library.Session
{
    [Serializable]
    public class Sesion
    {
        #region Propiedades

        public Pagina Pagina { get; set; }

        public Entidad Entidad { get; private set; }

        public Usuario Usuario { get; private set; }

        public string ClientIPAddress { get; set; }

        public Parameters Parameter { get; private set; }

        public bool UserLogged { get; internal set; }

        public bool IsExternal { get; set; }

        #endregion

        #region Costructores

        public Sesion()
        {
            this.Entidad = new Entidad();
            this.Usuario = new Usuario();

            this.Parameter = new Parameters();

            this.UserLogged = false;
            this.IsExternal = false;
        }

        ~Sesion()
        {
            this.Pagina = null;
            this.Entidad = null;
            this.Usuario = null;
            this.Parameter.Clear();
            this.Parameter = null;
        }

        #endregion
    }
}