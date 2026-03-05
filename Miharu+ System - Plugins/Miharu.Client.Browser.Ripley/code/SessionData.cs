using System.Collections.Generic;

namespace  Miharu.Client.Browser.code
{
    public class SessionData
    {
        #region Propiedades

        public List<string> UserPagesWithAccess { get; set; }

        #endregion

        #region Constructores

        public SessionData()
        {
            this.UserPagesWithAccess = new List<string>();
        }

        #endregion
    }
}