using System;
using Miharu.Security.Library.Session;

namespace  Miharu.Client.Browser.code
{
    public abstract class master_base : System.Web.UI.MasterPage
    {
        #region Propiedades

        public page_generic PageGeneric = null;

        public RequestType RequestType = RequestType.Post;

        public Sesion SessionManager
        {
            get
            {
                if (Session[consts.SessionManager] == null)
                    Session[consts.SessionManager] = new Sesion();

                return (Sesion)Session[consts.SessionManager]; 
            }
        }
    
        #endregion

        #region Eventos

        public abstract void Config_Page();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Request["RequestType"] != null) RequestType = (Request["RequestType"] == "Ajax") ? RequestType.Ajax : RequestType.Post;

            if (RequestType == RequestType.Post) Config_Page();
        }

        #endregion

        #region Metodos

        protected override void OnPreRender(EventArgs e)
        {            
            base.OnPreRender(e);
        }

        public void SetSessionValue(string nKey, object nValue)
        {
            Session[nKey] = nValue;
        }

        #endregion

        #region Funciones

        public T GetSessionValue<T>(string nKey)
        {

            if (Session[nKey] == null)
            {
                var type = typeof(T);
                var obj = type.Assembly.CreateInstance(type.FullName);
                Session[nKey] = obj;
            }
            return (T)Session[nKey];
        }

        #endregion
    }
}