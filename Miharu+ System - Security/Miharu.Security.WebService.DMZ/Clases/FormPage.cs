using System.Web;
using System.Web.UI;

namespace Miharu.Security.WebService.DMZ.Clases
{
    public class FormPage : Page
    {
        #region Funciones

        public string GetClientIpAddress()
        {
            // Guardar la IP del visitante 
            // El visitante puede acceder por proxy, entonces tomo la IP que lo está utilizando 
            var clientIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // Si no venía de un proxy, tomo la ip del visitante 
            if (string.IsNullOrEmpty(clientIpAddress))
                clientIpAddress = Request.ServerVariables["REMOTE_ADDR"];

            return clientIpAddress;
        }

        public string ResolveFullUrl(string url)
        {
            var webApplicationRootUrl =
                string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                    HttpContext.Current.Request.ApplicationPath);

            if (!webApplicationRootUrl.EndsWith("/"))
                webApplicationRootUrl += "/";

            url = url.Substring(2); // Quitar ~/
            url = webApplicationRootUrl + url;

            return url;
        }

        #endregion
    }
}