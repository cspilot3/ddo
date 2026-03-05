using System;
using Miharu.Security.Library.Session;
using Slyg.Data;
using System.Data;

namespace  Miharu.Client.Browser.code
{
    public abstract class control_form : System.Web.UI.UserControl
    {
        #region Propiedades

        public page_form MyPage
        {
            get { return Page as page_form; }
        }

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (MyPage.RequestType == RequestType.Post) Config_Page();
        }

        #endregion

        #region Metodos

        public abstract void Config_Page();

        public void TraceError(ScriptBuilder nHtml, Exception nException)
        {
            MyPage.TraceError(nHtml, nException);
        }

        public void SetSessionValue(string nKey, object nValue)
        {
            MyPage.SetSessionValue(nKey, nValue);
        }

        #endregion

        #region Funciones

        public string GetValue(ColumnEnum nColumnEnum, bool nRequired = true)
        {
            return MyPage.GetValue(nColumnEnum.ColumnName, nRequired);
        }

        public string GetValue(string nRequestKey, bool nRequired = true)
        {
            return MyPage.GetValue(nRequestKey, nRequired);
        }

        public T GetValue<T>(ColumnEnum nColumnEnum, bool nRequired = true)
        {
            return MyPage.GetValue<T>(nColumnEnum.ColumnName, nRequired);
        }

        public T GetValue<T>(string nRequestKey, bool nRequired = true)
        {
            return MyPage.GetValue<T>(nRequestKey, nRequired);
        }
        
        public T GetSessionValue<T>(string nKey)
        {
            return MyPage.GetSessionValue<T>(nKey);
        }

        public string GetJsonValue(DataRow nRow, ColumnEnum nColumnEnum)
        {
            return MyPage.GetJsonValue(nRow, nColumnEnum.ColumnName);
        }

        public string GetJsonValue(DataRow nRow, string nColumnName)
        {
            return MyPage.GetJsonValue(nRow, nColumnName);
        }        

        #endregion
    }
}