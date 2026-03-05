using DBCore;
using DBImaging;
using Miharu.Data.ServiceRemote.Model;
using Miharu.Data.ServiceRemote.Utils;
using Miharu.Desktop.Library.Config;
using Miharu.FileProvider.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slyg.Tools;
using Slyg.Tools.Imaging;
using Slyg.Tools.Imaging.FreeImageAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace Miharu.Data.ServiceRemote
{
    /// <summary>
    /// Descripción breve de DataRemoteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class DataRemoteService : System.Web.Services.WebService
    {
        public const int MaxThumbnailWidth = 60;
        public const int MaxThumbnailHeight = 80;

        #region WebMethods  

        [WebMethod]
        public string dataBridge(string request)
        {
            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));
            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(new DataBase.ProcessQuery().execute(queryRequest)));
        }

        [WebMethod]
        public string ImageCount(string request)
        {
            string Token = string.Empty;
            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "token")
                    {
                        Token = parameter.value;
                    }
                }
            }
            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(GetImageCount(Token)));
        }

        [WebMethod]
        public string GetFolioCargueItem(string request)
        {
            int fk_Cargue = 0;
            short fk_Cargue_Paquete = 0;
            short fk_Cargue_Paquete_Item = 0;
            short folio = 0;
            short entidad = 0;
            short sede = 0;
            short idcentro = 0;

            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "fk_Cargue") fk_Cargue = int.Parse(parameter.value);
                    if (parameter.name == "fk_Cargue_Paquete") fk_Cargue_Paquete = short.Parse(parameter.value);
                    if (parameter.name == "fk_Cargue_Paquete_Item") fk_Cargue_Paquete_Item = short.Parse(parameter.value);
                    if (parameter.name == "folio") folio = short.Parse(parameter.value);
                    if (parameter.name == "fk_Entidad") entidad = short.Parse(parameter.value);
                    if (parameter.name == "fk_Sede") sede = short.Parse(parameter.value);
                    if (parameter.name == "id_Centro_Procesamiento") idcentro = short.Parse(parameter.value);
                }
            }

            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(GetFolioCargueItem(fk_Cargue, fk_Cargue_Paquete, fk_Cargue_Paquete_Item, folio, entidad, sede, idcentro)));
        }

        [WebMethod]
        public string GetFoliosFile(string request)
        {
            long fk_Expediente = 0;
            short fk_Folder = 0;
            short fk_File = 0;
            short fk_Version = 0;
            bool es_Anexo = false;
            long fk_Anexo = 0;

            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "fk_Expediente") fk_Expediente = long.Parse(parameter.value);
                    if (parameter.name == "fk_Folder") fk_Folder = short.Parse(parameter.value);
                    if (parameter.name == "fk_File") fk_File = short.Parse(parameter.value);
                    if (parameter.name == "fk_Version") fk_Version = short.Parse(parameter.value);
                    if (parameter.name == "es_Anexo") es_Anexo = bool.Parse(parameter.value);
                    if (parameter.name == "fk_Anexo") fk_Anexo = long.Parse(parameter.value);
                }
            }

            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(GetFoliosFile(fk_Expediente, fk_Folder, fk_File, fk_Version, es_Anexo, fk_Anexo)));
        }

        [WebMethod]
        public string GetFolioFile(string request)
        {
            long fk_Expediente = 0;
            short fk_Folder = 0;
            short fk_File = 0;
            short fk_Version = 0;
            bool es_Anexo = false;
            long fk_Anexo = 0;
            short folio = 0;

            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "fk_Expediente") fk_Expediente = long.Parse(parameter.value);
                    if (parameter.name == "fk_Folder") fk_Folder = short.Parse(parameter.value);
                    if (parameter.name == "fk_File") fk_File = short.Parse(parameter.value);
                    if (parameter.name == "fk_Version") fk_Version = short.Parse(parameter.value);
                    if (parameter.name == "es_Anexo") es_Anexo = bool.Parse(parameter.value);
                    if (parameter.name == "fk_Anexo") fk_Anexo = long.Parse(parameter.value);
                    if (parameter.name == "folio") folio = short.Parse(parameter.value);
                }
            }

            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(GetFolioFile(fk_Expediente, fk_Folder, fk_File, fk_Version, es_Anexo, fk_Anexo, folio)));
        }

        [WebMethod]
        public string CreateImageFolioManager(string request)
        {
            int fk_Entidad = 0;
            int fk_Proyecto = 0;
            int fk_Usuario = 0;
            int fk_Documento = 0;
            long fk_Expediente = 0;
            short fk_Folder = 0;
            short fk_File = 0;
            int folio = 0;
            int TotalFolios = 0;
            byte[] dataImage = null;
            byte[] dataImageThumbnail = null;            

            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "fk_Entidad") fk_Entidad = int.Parse(parameter.value);
                    if (parameter.name == "fk_Proyecto") fk_Proyecto = int.Parse(parameter.value);
                    if (parameter.name == "fk_Documento") fk_Documento = int.Parse(parameter.value);
                    if (parameter.name == "fk_Usuario") fk_Usuario = int.Parse(parameter.value);
                    if (parameter.name == "fk_Expediente") fk_Expediente = long.Parse(parameter.value);
                    if (parameter.name == "fk_Folder") fk_Folder = short.Parse(parameter.value);
                    if (parameter.name == "fk_File") fk_File = short.Parse(parameter.value);
                    if (parameter.name == "TotalFolios") TotalFolios = short.Parse(parameter.value);
                    if (parameter.name == "folio") folio = int.Parse(parameter.value);
                    if (parameter.name == "dataImage") dataImage = Convert.FromBase64String(parameter.value);
                    if (parameter.name == "dataImageThumbnail") dataImageThumbnail = Convert.FromBase64String(parameter.value);
                }
            }

            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(CreateImageFolioManager(fk_Entidad, fk_Proyecto, fk_Documento, fk_Usuario, fk_Expediente, fk_Folder, fk_File, TotalFolios, folio, dataImage, dataImageThumbnail)));
        }


        #endregion

        #region Funciones

        private QueryResponse GetImageCount(string nToken)
        {
            DBCore.DBCoreDataBaseManager dbmCore = null;
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;

            QueryResponse queryResponse = new QueryResponse();
            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);

                dbmCore.Connection_Open(0);
                dbmImaging.Connection_Open(0);

                Guid identificador;

                if (!Guid.TryParse(nToken, out identificador))
                {
                    throw new Exception("Token no válido, " + nToken);
                }

                var fileDataTable = new DBCore.SchemaImaging.TBL_FileDataTable();

                fileDataTable.Columns.Remove("Eliminado");
                fileDataTable.AcceptChanges();

                fileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(identificador);
                if (fileDataTable.Count == 0)
                {
                    throw new Exception("No se encontró un registro asociado al Token: " + nToken);
                }

                var FileImagingRow = fileDataTable[0];
                manager = new FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, ref dbmImaging, 0);
                manager.Connect();

                if (FileImagingRow.Es_Anexo)
                {
                    queryResponse.scalar = manager.GetFolios(FileImagingRow.fk_Anexo);
                    queryResponse.success = true;
                }
                else
                {
                    queryResponse.scalar = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version);
                    queryResponse.success = true;
                }
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
                if (dbmImaging != null) dbmImaging.Connection_Close();
                if (manager != null) manager.Disconnect();
            }
            return queryResponse;
        }

        [WebMethod]
        public string GetFolio(string request)
        {
            string Token = string.Empty;
            short Folio = 0;
            bool IsOCRUsed = false;
            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "token") Token = parameter.value;
                    if (parameter.name == "folio") Folio = short.Parse(parameter.value);
                    if (parameter.name == "IsOCRUsed") IsOCRUsed = bool.Parse(parameter.value);
                }
            }

            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(GetFolio(Token, Folio, IsOCRUsed)));
        }

        private QueryResponse GetFolio(string nToken, short nFolio, bool nIsOCRUsed)
        {
            DBCore.DBCoreDataBaseManager dbmCore = null;
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;

            string dbmCoreConnectionString = string.Empty;
            string dbmImagingConnectionString = string.Empty;
            string dbmOcrConnectionString = string.Empty;

            byte[] imagen = null;
            byte[] thumbnail = null;

            QueryResponse queryResponse = new QueryResponse();
            DBStorage.SchemaImaging.TBL_File_FolioDataTable dtFileFolio = new DBStorage.SchemaImaging.TBL_File_FolioDataTable();

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);

                dbmCore.Connection_Open(0);
                dbmImaging.Connection_Open(0);

                Guid identificador;

                if (!Guid.TryParse(nToken, out identificador))
                {
                    throw new Exception("Token no válido, " + nToken);
                }

                var fileDataTable = new DBCore.SchemaImaging.TBL_FileDataTable();

                fileDataTable.Columns.Remove("Eliminado");
                fileDataTable.AcceptChanges();

                fileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(identificador);
                if (fileDataTable.Count == 0)
                {
                    throw new Exception("No se encontró un registro asociado al Token: " + nToken);
                }

                var FileImagingRow = fileDataTable[0];

                if (nIsOCRUsed)
                {
                    DBOCR.DBOCRDataBaseManager dbmOcr = null;

                    try
                    {
                        dbmOcr = new DBOCR.DBOCRDataBaseManager(Program.DBOCRConnectionString);

                        dbmOcr.Connection_Open(1);

                        var nameFileOUTOCR = dbmOcr.SchemaConfig.TBL_Parametro.DBFindByNombre_Parametro("Path_OUT_Data");

                        string rutaImagenBase = nameFileOUTOCR[0].Valor_Parametro;
                        string nameImageExpediente = FileImagingRow.fk_Expediente.ToString() + FileImagingRow.fk_Folder + FileImagingRow.fk_File;
                        string filePath = rutaImagenBase + "\\" + nameImageExpediente + "\\";
                        string nameCorrectsesgoImage = filePath + nameImageExpediente + nFolio + ".tif";

                        imagen = File.ReadAllBytes(nameCorrectsesgoImage);
                        var dataImageThumbnail = ImageManager.GetThumbnailData(nameCorrectsesgoImage, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight);
                        thumbnail = dataImageThumbnail[0];

                    }
                    catch
                    {
                        imagen = null;
                        thumbnail = null;
                    }
                    finally
                    {
                        if (dbmOcr != null) dbmOcr.Connection_Close();
                    }
                }

                if (imagen == null || thumbnail == null)
                {
                    try
                    {
                        manager = new FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, ref dbmImaging, 0);
                        manager.Connect();

                        if (FileImagingRow.Es_Anexo)
                        {
                            manager.GetFolio(FileImagingRow.fk_Anexo, nFolio, ref imagen, ref thumbnail);
                        }
                        else
                        {
                            manager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, nFolio, ref imagen, ref thumbnail);
                        }
                    }
                    finally
                    {
                        if (manager != null) manager.Disconnect();
                    }
                }

                DBStorage.SchemaImaging.TBL_File_FolioRow fileFolioRow = dtFileFolio.NewTBL_File_FolioRow();
                fileFolioRow.fk_Expediente = FileImagingRow.fk_Expediente;
                fileFolioRow.fk_Folder = FileImagingRow.fk_Folder;
                fileFolioRow.fk_File = FileImagingRow.fk_File;
                fileFolioRow.fk_Version = FileImagingRow.id_Version;
                fileFolioRow.id_File_Record_Folio = nFolio;
                fileFolioRow.Image_Binary = imagen;
                fileFolioRow.Thumbnail_Binary = thumbnail;

                dtFileFolio.Rows.Add(fileFolioRow);

                queryResponse.dataTable = dtFileFolio;
                queryResponse.success = true;
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }

            return queryResponse;
        }

        [WebMethod]
        public string SendDataPostRequest(string request)
        {
            // Desencripta el JSON encriptado recibido como parámetro
            string decryptedRequest = CryptoUtil.decrypt(request);
            PostRequestModel requestData = JsonConvert.DeserializeObject<PostRequestModel>(decryptedRequest);

            string jsonPayload = requestData.JsonPayload;
            string url = requestData.Url;

            // Crea la solicitud HTTP POST
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            // Agrega la cabecera Accept-Encoding para soportar compresión
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");

            // Escribe el JSON en el cuerpo de la solicitud
            using (StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonPayload);
            }

            // Procesa la respuesta
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // Lee la respuesta y retorna como string
                            string responseContent = reader.ReadToEnd();
                            //string serialized = JsonConvert.SerializeObject(responseContent);
                            string encrypted = Utils.CryptoUtil.encrypt(responseContent);
                            return encrypted;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Manejo de errores en caso de fallo en la solicitud
                using (var errorResponse = (HttpWebResponse)ex.Response)
                {
                    if (errorResponse != null)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string errorText = reader.ReadToEnd();
                            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(errorText));
                        }
                    }
                }
            }

            // Si no se pudo obtener una respuesta válida, devuelve una cadena vacía
            return string.Empty;
        }

        [WebMethod]
        public string GetFoliosCargueItem(string request)
        {
            int fk_Cargue = 0;
            short fk_Cargue_Paquete = 0;
            short fk_Cargue_Paquete_Item = 0;
            short entidad = 0;
            short sede = 0;
            short idcentro = 0;

            QueryRequest queryRequest = JsonConvert.DeserializeObject<QueryRequest>(Utils.CryptoUtil.decrypt(request));

            if (queryRequest.parameters.Count > 0)
            {
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (parameter.name == "fk_Cargue") fk_Cargue = int.Parse(parameter.value);
                    if (parameter.name == "fk_Cargue_Paquete") fk_Cargue_Paquete = short.Parse(parameter.value);
                    if (parameter.name == "fk_Cargue_Paquete_Item") fk_Cargue_Paquete_Item = short.Parse(parameter.value);
                    if (parameter.name == "fk_Entidad") entidad = short.Parse(parameter.value);
                    if (parameter.name == "fk_Sede") sede = short.Parse(parameter.value);
                    if (parameter.name == "id_Centro_Procesamiento") idcentro = short.Parse(parameter.value);
                }
            }

            return Utils.CryptoUtil.encrypt(JsonConvert.SerializeObject(GetFoliosCargueItem(fk_Cargue, fk_Cargue_Paquete, fk_Cargue_Paquete_Item, entidad, sede, idcentro)));
        }

        private QueryResponse GetFoliosCargueItem(int fk_Cargue, short fk_Cargue_Paquete, short fk_Cargue_Paquete_Item, short entidad, short sede, short idcentro)
        {
    
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;
            int folios = 0;

            QueryResponse queryResponse = new QueryResponse();
            DBStorage.SchemaImaging.TBL_File_FolioDataTable dtFileFolio = new DBStorage.SchemaImaging.TBL_File_FolioDataTable();

            try
            {               
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);
               
                dbmImaging.Connection_Open(0);

                DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType centro = null;

                centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(entidad, sede, idcentro)[0].ToCTA_Centro_ProcesamientoSimpleType();
                
                try
                {
                    manager = new FileProviderManager(fk_Cargue, centro, ref dbmImaging, 1);
                    manager.Connect();

                    folios = manager.GetFolios(fk_Cargue, fk_Cargue_Paquete, fk_Cargue_Paquete_Item);
                }
                catch
                {
                    folios = 0;
                }
                finally
                {
                    if (manager != null) manager.Disconnect();
                }
                queryResponse.scalar = folios;
                queryResponse.success = true;
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }

            return queryResponse;
        }

        private QueryResponse GetFolioCargueItem(int fk_Cargue, short fk_Cargue_Paquete, short fk_Cargue_Paquete_Item, short folio, short entidad, short sede, short idcentro)
        {
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;

            byte[] imagen = null;
            byte[] thumbnail = null;

            QueryResponse queryResponse = new QueryResponse();
            DataTable dataTable = new DataTable();           

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);

                dbmImaging.Connection_Open(0);

                DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType centro = null;

                centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(entidad, sede, idcentro)[0].ToCTA_Centro_ProcesamientoSimpleType();

                try
                {
                    manager = new FileProviderManager(fk_Cargue, centro, ref dbmImaging, 1);
                    manager.Connect();

                    manager.GetFolio(fk_Cargue, fk_Cargue_Paquete, fk_Cargue_Paquete_Item, folio, ref imagen, ref thumbnail);
                }
                catch
                {
                    imagen = null;
                    thumbnail = null;
                }
                finally
                {
                    if (manager != null) manager.Disconnect();
                }

                dataTable.TableName = "Response";

                dataTable.Columns.Add("fk_Cargue", typeof(long));
                dataTable.Columns.Add("fk_Cargue_Paquete", typeof(short));
                dataTable.Columns.Add("fk_Cargue_Item", typeof(int));
                dataTable.Columns.Add("id_Item_Folio", typeof(short));
                dataTable.Columns.Add("Image_Binary", typeof(byte[]));
                dataTable.Columns.Add("Thumbnail_Binary", typeof(byte[]));

                dataTable.Rows.Add(dataTable.NewRow());

                dataTable.Rows[0]["fk_Cargue"] = fk_Cargue;
                dataTable.Rows[0]["fk_Cargue_Paquete"] = fk_Cargue_Paquete;
                dataTable.Rows[0]["fk_Cargue_Item"] = fk_Cargue_Paquete_Item;
                dataTable.Rows[0]["id_Item_Folio"] = folio;
                dataTable.Rows[0]["Image_Binary"] = imagen;
                dataTable.Rows[0]["Thumbnail_Binary"] = thumbnail;
              
                dataTable.AcceptChanges();

                queryResponse.dataTable = dataTable;
                queryResponse.success = true;
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }

            return queryResponse;
        }

        private QueryResponse GetFoliosFile(long fk_Expediente, short fk_Folder, short fk_File, short fk_Version, bool es_Anexo, long fk_Anexo)
        {
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;
            int folios = 0;

            QueryResponse queryResponse = new QueryResponse();

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);
                dbmImaging.Connection_Open(0);
                
                try
                {
                    manager = new FileProviderManager(fk_Expediente, fk_Folder, ref dbmImaging, 1);
                    manager.Connect();

                    if (es_Anexo)
                    {
                        folios = manager.GetFolios(fk_Anexo);
                    }
                    else
                    {
                        folios = manager.GetFolios(fk_Expediente, fk_Folder, fk_File, fk_Version);
                    }                    
                }
                catch
                {
                    folios = 0;
                }
                finally
                {
                    if (manager != null) manager.Disconnect();
                }
                queryResponse.scalar = folios;
                queryResponse.success = true;
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }

            return queryResponse;
        }

        private QueryResponse GetFolioFile(long fk_Expediente, short fk_Folder, short fk_File, short fk_Version, bool es_Anexo, long fk_Anexo, short folio)
        {
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;

            byte[] imagen = null;
            byte[] thumbnail = null;

            QueryResponse queryResponse = new QueryResponse();
            DataTable dataTable = new DataTable();
            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);

                dbmImaging.Connection_Open(0);

                try
                {
                    manager = new FileProviderManager(fk_Expediente, fk_Folder, ref dbmImaging, 1);
                    manager.Connect();

                    if (es_Anexo)
                    {
                        manager.GetFolio(fk_Anexo, folio, ref imagen, ref thumbnail);
                    }
                    else
                    {
                        manager.GetFolio(fk_Expediente, fk_Folder, fk_File, fk_Version, folio, ref imagen, ref thumbnail);
                    }
                }
                catch
                {
                    imagen = null;
                    thumbnail = null;
                }
                finally
                {
                    if (manager != null) manager.Disconnect();
                }

                dataTable.TableName = "Response";

                dataTable.Columns.Add("fk_Expediente", typeof(long));
                dataTable.Columns.Add("fk_Folder", typeof(short));
                dataTable.Columns.Add("fk_File", typeof(short));
                dataTable.Columns.Add("fk_Version", typeof(short));
                dataTable.Columns.Add("id_File_Record_Folio", typeof(short));
                dataTable.Columns.Add("Image_Binary", typeof(byte[]));
                dataTable.Columns.Add("Thumbnail_Binary", typeof(byte[]));

                dataTable.Rows.Add(dataTable.NewRow());

                dataTable.Rows[0]["fk_Expediente"] = fk_Expediente;
                dataTable.Rows[0]["fk_Folder"] = fk_Folder;
                dataTable.Rows[0]["fk_File"] = fk_File;
                dataTable.Rows[0]["fk_Version"] = fk_Version;
                dataTable.Rows[0]["id_File_Record_Folio"] = folio;
                dataTable.Rows[0]["Image_Binary"] = imagen;
                dataTable.Rows[0]["Thumbnail_Binary"] = thumbnail;

                dataTable.AcceptChanges();

                queryResponse.dataTable = dataTable;
                queryResponse.success = true;
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }

            return queryResponse;
        }
        

        private QueryResponse CreateImageFolioManager(int fk_Entidad, int fk_Proyecto,int fk_Documento, int idUsuario, long fk_Expediente, short fk_Folder, short fk_File, int TotalFolios, int folio, byte[] dataImage , byte[] dataImageThumbnail)
        {            
            QueryResponse queryResponse = new QueryResponse();

            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            DBSecurity.DBSecurityDataBaseManager dbmSecurity = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;

            short fk_Version = 1;

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString);
                dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);

                dbmImaging.Connection_Open(0);
                dbmSecurity.Connection_Open(0);
                dbmCore.Connection_Open(0);

                string nombreEnsamblado = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                var dtServicioEntidadServidor = dbmSecurity.SchemaImaging.CTA_Servicio_Entidad_Servidor_Centro_procesamiento.DBFindByfk_Entidadfk_ProyectoNombre_Servicio(fk_Entidad, fk_Proyecto, nombreEnsamblado);
                if (dtServicioEntidadServidor == null || dtServicioEntidadServidor.Count == 0)
                {
                    throw new ArgumentException($"No se pudo obtener la configuracion de la Entidad:{fk_Entidad}, Proyecto:{fk_Proyecto} para obtener el servidor y centro de procesamiento. proceda a realizar la configuracion pertinente en dbmSecurity.SchemaImaging.CTA_Servicio_Entidad_Servidor_Centro_procesamiento.");
                }
                var servicioServidorProcesamiento = dtServicioEntidadServidor[0];

                var _ServidorTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(servicioServidorProcesamiento.fk_Entidad_Servidor, servicioServidorProcesamiento.fk_Servidor);
                if (_ServidorTable == null || _ServidorTable.Count == 0)
                {
                    throw new ArgumentException($"No se pudo obtener el servidor de la Entidad:{servicioServidorProcesamiento.fk_Entidad_Servidor}, Servidor:{servicioServidorProcesamiento.fk_Servidor}. proceda a realizar la configuracion pertinente en dbmImaging.SchemaCore.CTA_Servidor.");
                }

                var ServidorImageRow = _ServidorTable[0].ToCTA_ServidorSimpleType();

                var _CentroTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(servicioServidorProcesamiento.fk_Entidad_Centro_Procesamiento, servicioServidorProcesamiento.fk_Sede_Centro_Procesamiento, servicioServidorProcesamiento.fk_Centro_Procesamiento);
                if (_CentroTable == null || _CentroTable.Count == 0)
                {
                    throw new ArgumentException($"No se pudo obtener el Centro de procesamiento de la Entidad:{servicioServidorProcesamiento.fk_Entidad_Centro_Procesamiento}, Sede:{servicioServidorProcesamiento.fk_Sede_Centro_Procesamiento}, Centro:{servicioServidorProcesamiento.fk_Centro_Procesamiento}. proceda a realizar la configuracion pertinente en dbmImaging.SchemaCore.CTA_Servidor.");
                }

                var CentroImageRow = _CentroTable[0].ToCTA_Centro_ProcesamientoSimpleType();

                var dtProyecto = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto((SlygNullable<short>)fk_Entidad, (SlygNullable<short>)fk_Proyecto);
                if (dtProyecto == null || dtProyecto.Count == 0)
                {
                    throw new ArgumentException($"No se pudo obtener la configuracion del proyecto de la Entidad:{fk_Entidad}, Proyecto:{fk_Proyecto}. proceda a realizar la configuracion pertinente en dbmImaging.SchemaConfig.CTA_Proyecto.");
                }

                var ProyectoImagingRow = dtProyecto[0].ToCTA_ProyectoSimpleType();

                var formato = Utilities.GetEnumFormat(ProyectoImagingRow.Extension_Formato_Imagen_Salida);
                var compresion = Utilities.GetEnumCompression((DesktopConfig.FormatoImagenEnum)ProyectoImagingRow.id_Formato_Imagen_Salida);


                FileProviderManager manager = null;

                try 
                {
                    manager = new FileProviderManager(ServidorImageRow, CentroImageRow, ref dbmImaging, idUsuario);
                    manager.Connect();

                    /* Permite almacenar las imagenes recibidas en ruta temporal
                    //string tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
                    //if (!Directory.Exists(tempPath))
                    //{
                    //    Directory.CreateDirectory(tempPath);
                    //}

                    //string fileName = $"thumbnail_{Guid.NewGuid()}_{folio}.{formato}";
                    //string fullPath = Path.Combine(tempPath, fileName);

                    //File.WriteAllBytes(fullPath, dataImageThumbnail);

                    //string fileNameImage = $"Image_{Guid.NewGuid()}_{folio}.{formato}";
                    //string fullPathImage = Path.Combine(tempPath, fileName);

                    //File.WriteAllBytes(fullPathImage, dataImage);*/

                    // Verificar si ya existe la imagen
                    int foliosImage;
                    Guid Identificador;
                    (foliosImage,Identificador) = GetImageCount(ref dbmImaging,ref dbmCore, fk_Expediente, fk_Folder, fk_File, fk_Version, idUsuario);
                                          
                    if (folio == 1)
                    {
                        if(foliosImage == 0)
                        {
                            Guid guidImage = Guid.NewGuid();

                            var fileImgType = new DBCore.SchemaImaging.TBL_FileType();
                            fileImgType.fk_Expediente = fk_Expediente;
                            fileImgType.fk_Folder = (short)fk_Folder;
                            fileImgType.fk_File = fk_File;
                            fileImgType.id_Version = fk_Version;
                            fileImgType.File_Unique_Identifier = guidImage;
                            fileImgType.Folios_Documento_File = (short)(TotalFolios);
                            fileImgType.Tamaño_Imagen_File = 0;
                            fileImgType.Nombre_Imagen_File = "";
                            fileImgType.Key_Cargue_Item = "";
                            fileImgType.Save_FileName = "";
                            fileImgType.fk_Content_Type = ProyectoImagingRow.Extension_Formato_Imagen_Salida;
                            fileImgType.fk_Usuario_Log = idUsuario;
                            fileImgType.Validaciones_Opcionales = false;
                            fileImgType.Es_Anexo = false;
                            fileImgType.fk_Anexo = null;
                            fileImgType.fk_Entidad_Servidor = ServidorImageRow.fk_Entidad;
                            fileImgType.fk_Servidor = ServidorImageRow.id_Servidor;
                            fileImgType.Fecha_Creacion = SlygNullable.SysDate;
                            fileImgType.En_Transferencia = false;

                            var fileProcesType = new DBCore.SchemaProcess.TBL_FileType();
                            fileProcesType.fk_Expediente = fk_Expediente;
                            fileProcesType.fk_Folder = (short)fk_Folder;
                            fileProcesType.id_File = fk_File;
                            fileProcesType.File_Unique_Identifier = guidImage;
                            fileProcesType.fk_Documento = fk_Documento;
                            fileProcesType.Folios_File = ((SlygNullable<short>)(TotalFolios));
                            fileProcesType.Monto_File = 0;
                            fileProcesType.CBarras_File = fk_Expediente.ToString() + fk_Folder.ToString() + fk_File.ToString();

                            var FileEstadoType = new DBCore.SchemaProcess.TBL_File_EstadoType();
                            FileEstadoType.fk_Expediente = fk_Expediente;
                            FileEstadoType.fk_Folder = (short)fk_Folder;
                            FileEstadoType.fk_File = fk_File;
                            FileEstadoType.Modulo = new Slyg.Tools.SlygNullable<byte>(2);
                            FileEstadoType.fk_Estado = (short)DBCore.EstadoEnum.Indexado;  // estado Indexado 
                            FileEstadoType.fk_Usuario = idUsuario;
                            FileEstadoType.Fecha_Log = DateTime.Now;

                            dbmCore.SchemaProcess.TBL_File.DBInsert(fileProcesType);
                            dbmCore.SchemaProcess.TBL_File_Estado.DBInsert(FileEstadoType);
                            dbmCore.SchemaImaging.TBL_File.DBInsert(fileImgType);
                            manager.CreateItem((long)fk_Expediente, fk_Folder, fk_File, fk_Version, ProyectoImagingRow.Extension_Formato_Imagen_Salida, guidImage);
                        }
                        else
                        {
                            var fileImgType = new DBCore.SchemaImaging.TBL_FileType();
                            fileImgType.Folios_Documento_File = (short)(TotalFolios);
                            fileImgType.fk_Content_Type = ProyectoImagingRow.Extension_Formato_Imagen_Salida;
                            fileImgType.fk_Usuario_Log = idUsuario;
                            fileImgType.Validaciones_Opcionales = false;
                            fileImgType.Es_Anexo = false;
                            fileImgType.fk_Anexo = null;
                            fileImgType.fk_Entidad_Servidor = ServidorImageRow.fk_Entidad;
                            fileImgType.fk_Servidor = ServidorImageRow.id_Servidor;
                            fileImgType.fk_Anexo = null;
                            fileImgType.Fecha_Creacion = SlygNullable.SysDate;
                            fileImgType.En_Transferencia = false;
                            dbmCore.SchemaImaging.TBL_File.DBUpdate(fileImgType, fk_Expediente, fk_Folder, fk_File, fk_Version);

                            var fileProcesType = new DBCore.SchemaProcess.TBL_FileType();
                            fileProcesType.fk_Expediente = fk_Expediente;
                            fileProcesType.fk_Folder = (short)fk_Folder;
                            fileProcesType.id_File = fk_File;
                            fileProcesType.File_Unique_Identifier = Identificador;
                            fileProcesType.fk_Documento = fk_Documento;
                            fileProcesType.Folios_File = ((SlygNullable<short>)(TotalFolios));
                            fileProcesType.Monto_File = 0;
                            fileProcesType.CBarras_File = fk_Expediente.ToString() + fk_Folder.ToString() + fk_File.ToString();
                            dbmCore.SchemaProcess.TBL_File.DBUpdate(fileProcesType, fk_Expediente, fk_Folder, fk_File);

                            var FileEstadoType = new DBCore.SchemaProcess.TBL_File_EstadoType();
                            FileEstadoType.fk_Expediente = fk_Expediente;
                            FileEstadoType.fk_Folder = (short)fk_Folder;
                            FileEstadoType.fk_File = fk_File;
                            FileEstadoType.Modulo = new Slyg.Tools.SlygNullable<byte>(2);
                            FileEstadoType.fk_Estado = (short)DBCore.EstadoEnum.Indexado;  // estado Indexado 
                            FileEstadoType.fk_Usuario = idUsuario;
                            FileEstadoType.Fecha_Log = DateTime.Now;
                            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(FileEstadoType, fk_Expediente, fk_Folder, fk_File, 2);

                            manager.DeleteItem(fk_Expediente, fk_Folder, fk_File, fk_Version);
                            manager.CreateItem(fk_Expediente, fk_Folder, fk_File, fk_Version, ProyectoImagingRow.Extension_Formato_Imagen_Salida, Identificador);
                        }
                    }
                    manager.CreateFolio((long)fk_Expediente, (short)fk_Folder, fk_File, fk_Version, (short)folio, dataImage, dataImageThumbnail, false);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Se presento un Error en el modulo manager : " + ex.Message + ex.StackTrace);
                }
                finally
                {
                    if ((manager != null))
                        manager.Disconnect();
                }

                queryResponse.scalar = 1;
                queryResponse.success = true;

            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            finally
            {
                if (dbmImaging != null) dbmImaging.Connection_Close();
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
                
            }

            return queryResponse;
        }

        private (int,Guid) GetImageCount(ref DBImagingDataBaseManager dbmImaging, ref DBCoreDataBaseManager dbmCore, long fk_Expediente, short fk_Folder, short fk_File,short fk_Version, int idUsuario)
        {
            int imageCount = 0;
            Guid Identificador = Guid.Empty;

            FileProviderManager manager = null;

            try
            {
                DBCore.SchemaImaging.TBL_FileDataTable fileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(fk_Expediente, fk_Folder, fk_File, fk_Version);

                if (fileDataTable.Count == 0)
                {
                    return (imageCount, Identificador);
                }

                var FileImagingRow = fileDataTable[0];
                Identificador = FileImagingRow.File_Unique_Identifier;

                manager = new FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, ref dbmImaging, 0);
                manager.Connect();

                if (FileImagingRow.Es_Anexo)
                {
                    imageCount = manager.GetFolios(FileImagingRow.fk_Anexo);
                }
                else
                {
                    imageCount = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version);
                }

                return (imageCount,Identificador);
            }
            catch (Exception ex)
            {
                return (imageCount, Identificador);
            }
            finally
            {
                if ((manager != null))
                    manager.Disconnect();
            }
        }



        #endregion

    }
}
