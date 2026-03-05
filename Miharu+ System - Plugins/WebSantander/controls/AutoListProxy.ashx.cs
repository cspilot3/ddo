using System.Web;
using WebSantander.code;

namespace  WebSantander.controls
{
    /// <summary>
    /// Descripción breve de AutoListProxy
    /// </summary>
    public class AutoListProxy : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region Propiedades

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region Metodos

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(AutoListManager.Instance.GetCurrentAutoListAjaxData());
        }

        #endregion
    }
}