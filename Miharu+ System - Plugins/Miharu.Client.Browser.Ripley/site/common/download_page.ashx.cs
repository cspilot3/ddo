using System.Web;
using System.Text;
using System.Web.SessionState;
using Miharu.Client.Browser.code;
using Miharu.Security.Library.Session;

namespace Miharu.Client.Browser.site.common
{
    /// <summary>
    /// Descripción breve de download_page
    /// </summary>
    public class download_page : IHttpHandler, IReadOnlySessionState
    {
        #region Propiedades

        public bool IsReusable { get { return true; } }

        #endregion

        #region Eventos

        public void ProcessRequest(HttpContext context)
        {
            FileData fileData = null;

            string ContainerMode = context.Request.Params["mode"];
            string FileContainer = context.Request.Params["container"];

            object Container = null;

            if (ContainerMode != null && ContainerMode.ToLower() == "page") // Parametro de página
            {                
                if(context.Session[consts.SessionManager] != null)
                {
                    var SessionManager = (Sesion)context.Session[consts.SessionManager];

                    if(SessionManager.Pagina != null)
                        Container = SessionManager.Pagina.Parameter[FileContainer];
                }                
            }
            else // Parametro de sesion
            {
                Container = context.Session[FileContainer];
            }

            var EmtyText = "";

            if (Container == null)
            {
                EmtyText = "No se encontro un archivo para descargar en la variable: " + FileContainer;
            }
            else
            {
                fileData = Container as FileData;
                if (fileData == null)
                    EmtyText = "El objeto almacenado en la variable: " + FileContainer + ", no es un FileData válido";
            }

            if (fileData == null)
            {
                fileData = new FileData();                
                fileData.File = Encoding.ASCII.GetBytes(EmtyText);
                fileData.ContentType = consts.EmptyFileContentType;
                fileData.Filename = "error.txt";
            }

            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();

            context.Response.AddHeader("content-disposition", "attachment; filename=" + fileData.Filename);
            context.Response.ContentType = fileData.ContentType;
            context.Response.OutputStream.Write(fileData.File, 0, fileData.File.Length);
        }

        #endregion
    }
}
