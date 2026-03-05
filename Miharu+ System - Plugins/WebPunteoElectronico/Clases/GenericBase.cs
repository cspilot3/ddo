using System;
using Miharu.Security.Library.Session;

namespace WebPunteoElectronico.Clases
{
    public abstract class GenericBase : System.Web.UI.Page
    {
        #region Propiedades

        public Sesion MiharuSession { get; protected set; }

        public new MasterBase Master
        {
            get { return (MasterBase) base.Master; }
        }

        public System.Web.UI.MasterPage OriginalMaster
        {
            get { return base.Master; }
        }

        public TypeConnectionString ConnectionString
        {
            get
            {
                if (this.MiharuSession.Parameter["ConnectionStrings"] == null)
                    return null;
                return (TypeConnectionString) this.MiharuSession.Parameter["ConnectionStrings"];
            }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                this.MiharuSession = getSession();
            }
            catch
            {
            }
        }

        #endregion

        #region Metodos

        protected abstract void Config_Page();

        protected abstract void Load_Data();

        private Sesion getSession()
        {
            if (Session["Session"] == null)
                CreateSession();

            return (Sesion) Session["Session"];
        }

        protected void CreateSession()
        {
            this.MiharuSession = new Sesion();
            Session["Session"] = this.MiharuSession;
        }

        public void TraceError(Exception ex)
        {
            Program.TraceError(ex);
        }

        protected void SelectText(System.Web.UI.WebControls.TextBox ctl)
        {
            ctl.Attributes.Add("onfocus", ctl.ClientID + ".select();");
        }

        #endregion

        #region Funciones

        public bool ValidarPermisos(string nPathPermiso)
        {
            return this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(nPathPermiso);
        }

        public void SetSessionValue(string nKey, object nValue)
        {
            MiharuSession.Parameter[nKey] = nValue;
        }

        public T GetSessionValue<T>(string nKey)
        {
            if (MiharuSession.Parameter[nKey] == null)
            {
                var type = typeof (T);
                var obj = type.Assembly.CreateInstance(type.FullName);
                MiharuSession.Parameter[nKey] = obj;
            }
            return (T) MiharuSession.Parameter[nKey];
        }

        #endregion
    }
}