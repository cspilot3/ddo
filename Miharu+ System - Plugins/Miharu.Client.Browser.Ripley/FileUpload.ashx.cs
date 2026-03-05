using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;
using Miharu.Client.Browser.code;

namespace  Miharu.Client.Browser
{
    /// <summary>
    /// Descripción breve de FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler, IReadOnlySessionState
    {
        #region Propiedades

        public bool IsReusable { get { return true; } }

        #endregion

        #region Eventos

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Stream inputStream = null;
                var filename = HttpContext.Current.Request.Headers["X-File-Name"];
                
                var extensionsString = HttpContext.Current.Request.Params["0"];
                var extensionsArray = (extensionsString != null) ? extensionsString.ToLower().Split(',') : null;
                
                if (string.IsNullOrEmpty(filename) && HttpContext.Current.Request.Files.Count <= 0)
                    throw new Exception("Archivo vacio");                

                if (filename == null) //This work for IE
                {                    
                    var uploadedfile = context.Request.Files[0];                 
                    filename = Path.GetFileName(uploadedfile.FileName);                    
                    
                    inputStream = uploadedfile.InputStream;
                }
                else //This work for Firefox and Chrome.
                {
                    filename = HttpUtility.UrlDecode(filename);
                    inputStream = HttpContext.Current.Request.InputStream;                    
                }

                var fileExt = Path.GetExtension(filename);
                if (fileExt != null) fileExt = fileExt.ToLower();

                if (extensionsString != "*")
                {
                    if (extensionsArray != null && !extensionsArray.Contains(fileExt))
                        throw new Exception("Archivo no permitido " + fileExt + ", solo se permiten los archivos con extension [" + extensionsString + "]");
                }
                else
                { 
                    if (inputStream.Length>10000000)//validacion del tamaño del documento
                        throw new Exception("El tamaño maximo permitido es de 10 MB");
                }

                var fileData = new FileData();
                fileData.File = new byte[inputStream.Length];
                inputStream.Read(fileData.File, 0, fileData.File.Length);
                fileData.Filename = filename;

                context.Session[consts.SessionFile] = fileData;
                

                context.Response.Write("{success:true, name:'" + filename + "'}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{success:false, error:'" + new ScriptBuilder().CleanScript(ex.Message) + "'}");
            }
        }

        #endregion
    }
}
