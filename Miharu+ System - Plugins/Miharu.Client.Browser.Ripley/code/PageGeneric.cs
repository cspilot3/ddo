using System;
using System.Collections.Generic;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.Session;

namespace  Miharu.Client.Browser.code
{
    public abstract class page_generic : System.Web.UI.Page
    {
        #region Propiedades

        public TypeConnectionString ConnectionStrings
        {
            get { return (TypeConnectionString) this.SessionManager.Parameter["ConnectionStrings"]; }
            set { this.SessionManager.Parameter["ConnectionStrings"] = value; }
        }

        public Sesion SessionManager
        {
            get
            {
                if (Session[consts.SessionManager] == null)
                    Session[consts.SessionManager] = new Sesion();

                return (Sesion) Session[consts.SessionManager];
            }
        }

        public TBL_Usuario_SesionSimpleType SessionToken
        {
            get { return (TBL_Usuario_SesionSimpleType)Session[consts.SessionToken]; }
            set { Session[consts.SessionToken] = value; }
        }

        public List<string> UserPagesWithAccess
        {
            get
            {
                if (Session[consts.UserPagesWithAccess] == null)
                    Session[consts.UserPagesWithAccess] = new List<string>();

                return (List<string>) Session[consts.UserPagesWithAccess];
            }
        }

        public new master_base Master
        {
            get { return (master_base)base.Master; }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.PageGeneric = this;
        }

        #endregion

        #region Metodos
        
        public void TraceError(Exception ex)
        {
            Program.TraceError(ex);
        }

        #endregion

        #region Funciones

        public bool ValidarPermiso(string nPathPermiso)
        {
            if (this.SessionManager.Usuario == null || this.SessionManager.Usuario.PerfilManager == null) return false;
            return this.SessionManager.Usuario.PerfilManager.PuedeAcceder(nPathPermiso);            
        }
        
        public void SetSessionValue(string nKey, object nValue)
        {
            Session[nKey] = nValue;
        }

        public T GetSessionValue<T>(string nKey)
        {
            if (Session[nKey] != null)
            {
                if (Session[nKey].GetType().FullName != typeof(T).FullName)
                    Session[nKey] = null;
            }
            if (Session[nKey] == null)
            {
                var type = typeof(T);
                var obj = type.Assembly.CreateInstance(type.FullName);
                Session[nKey] = obj;
            }
            return (T)Session[nKey];
        }

        public string ResolveUrlPage(string nUrl)
        {
            return FormatUrl(ResolveUrl(nUrl));
        }

        public string FormatUrl(string nUrl)
        {
            var url = nUrl.ToLower().TrimStart('/');
            var app = Request.ApplicationPath != null ? Request.ApplicationPath.ToLower().TrimStart('/') : "";

            if (url.StartsWith(app)) url = url.Substring(app.Length, url.Length - app.Length).TrimStart('/');
            return url;
        }

        #endregion
    }
}