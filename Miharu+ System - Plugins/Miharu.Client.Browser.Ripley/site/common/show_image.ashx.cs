using System.Web;
using System.IO;
using System.Web.SessionState;
using Miharu.Client.Browser.code;
using Miharu.Security.Library.Session;

namespace Miharu.Client.Browser.site.common
{
    /// <summary>
    /// Descripción breve de show_image
    /// </summary>
    public class show_image : IHttpHandler, IReadOnlySessionState
    {

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    ImageData imageData = null;

        //    string ImageContainer = Request.Params["container"];

        //    if (Session[ImageContainer] == null)
        //    {
        //        imageData = new ImageData();

        //        using (var EmptyImageStream = new FileStream(Server.MapPath(consts.EmptyImage), FileMode.Open, FileAccess.Read))
        //        {
        //            imageData.Image = new byte[EmptyImageStream.Length];
        //            EmptyImageStream.Read(imageData.Image, 0, imageData.Image.Length);
        //            imageData.ContentType = consts.EmptyImageContentType;
        //        }
        //    }
        //    else
        //    {
        //        imageData = (ImageData)Session[ImageContainer];
        //    }

        //    Response.ContentType = imageData.ContentType;
        //    Response.OutputStream.Write(imageData.Image, 0, imageData.Image.Length);
        //}

        #region Propiedades

        public bool IsReusable { get { return true; } }

        #endregion

        #region Eventos

        public void ProcessRequest(HttpContext context)
        {
            ImageData imageData = null;

            var containerMode = context.Request.Params["mode"];
            var imageContainer = context.Request.Params["container"];
            var defaultImage = context.Request.Params["default"];

            object container = null;

            if (containerMode != null && containerMode.ToLower() == "page") // Parametro de página
            {
                if (context.Session[consts.SessionManager] != null)
                {
                    var sessionManager = (Sesion)context.Session[consts.SessionManager];

                    if (sessionManager.Pagina != null)
                        container = sessionManager.Pagina.Parameter[imageContainer];
                }
            }
            else // Parametro de sesion
            {
                container = context.Session[imageContainer];
            }

            if (container != null)
                imageData = container as ImageData;

            if (imageData == null)
            {
                imageData = new ImageData();
                var filename = consts.EmptyImage;


                if (!string.IsNullOrEmpty(defaultImage))
                    filename = defaultImage;

                using (var emptyImageStream = new FileStream(context.Server.MapPath(filename), FileMode.Open, FileAccess.Read))
                {
                    imageData.Image = new byte[emptyImageStream.Length];
                    emptyImageStream.Read(imageData.Image, 0, imageData.Image.Length);
                    imageData.ContentType = consts.EmptyImageContentType;
                }
            }

            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();

            context.Response.ContentType = imageData.ContentType;
            context.Response.OutputStream.Write(imageData.Image, 0, imageData.Image.Length);
        }

        #endregion
    }
}