using System;

namespace Miharu.Security.Library.Session
{
    [Serializable]
    public class Pagina
    {
        #region Propiedades

        public string Name { get; set; }

        public string PageTitle { get; set; }

        public string PageDir { get; set; }

        public string SecurityPath { get; set; }

        public Parameters Parameter { get; set; }

        #endregion

        #region Constructores

        public Pagina(string nPageName, string nPageTitle, string nPageDir, string nSecurityPath)
        {
            this.Name = nPageName;
            this.PageTitle = nPageTitle;
            this.PageDir = nPageDir;
            this.SecurityPath = nSecurityPath;

            this.Parameter = new Parameters();
        }

        ~Pagina()
        {
            this.Parameter.Clear();
            this.Parameter = null;
        }

        #endregion
    }
}