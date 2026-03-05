using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Services;
using Miharu.Desktop.Library.Config;
using System.Security.Cryptography;
using System.Globalization;

namespace Miharu.Uploader.WebService
{
    /// <summary>
    /// Summary description for UploaderWebService
    /// </summary>
    [WebService(Namespace = "http://slyg.com.co/Miharu.Uploader.WebService/UploaderWebService/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UploaderWebService : System.Web.Services.WebService
    {

        private enum EnumCipherType
        {
            Nothing = 0,
            TDES = 1,
            AES = 2
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Uploader.WebService/ResultBase/")]
        public class ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public bool Result;

            [System.Xml.Serialization.XmlElement]
            public string Message;
        }

        [WebMethod]
        public ResultGetProcesos GetProcesos(string nIdUsuario, string nIdEntidad)
        {
            var UsuarioDecrypt = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));
            var EntidadDecrypt = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));

            var CifradoTipo = getCifradoTipo(EntidadDecrypt, UsuarioDecrypt);
            var respuesta = new ResultGetProcesos();
            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);
                dbmCore.Connection_Open(1);

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                var ProcesosDT = dbmCore.SchemaSecurity.CTA_Rol_Acceso.DBFindByfk_Entidadfk_Usuario(EntidadDecrypt, UsuarioDecrypt);

                respuesta.Procesos = new List<string>();

                foreach (var proceso in ProcesosDT)
                {
                    respuesta.Procesos.Add(Encrypt(serializer.Serialize(proceso.ToCTA_Rol_AccesoSimpleType()), (EnumCipherType)CifradoTipo));
                }

                respuesta.Result = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error en obtención de procesos: " + ex.Message;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Uploader.WebService/ResultGetProcesos/")]
        public class ResultGetProcesos : ResultBase
        {
            [System.Xml.Serialization.XmlArray]
            public List<String> Procesos;
        }

        [WebMethod]
        public ResultGetCifradoTipo GetCifradoTipo(string nIdEntidad)
        {
            var _nIdEntidad = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));

            var respuesta = new ResultGetCifradoTipo();
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(Program.IntegrationConnectionString);
                dbmIntegration.Connection_Open(1);

                var EntidadDataTable = dbmIntegration.SchemaConfig.TBL_Entidad.DBGet(_nIdEntidad);

                if (EntidadDataTable.Rows.Count > 0)
                {
                    var EntidadRow = EntidadDataTable[0];
                    respuesta.Cifrado = Encrypt(EntidadRow.fk_Cifrado_Tipo.ToString(), EnumCipherType.TDES);
                }
              
                respuesta.Result = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error en obtención de Tipo de Cifrado: " + ex.Message;
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }

            return  respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Uploader.WebService/ResultGetCifradoTipo/")]
        public class ResultGetCifradoTipo : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public string Cifrado;
        }

        [WebMethod]
        public ResultGetReporteCargue GetReporteCargue(string nIdEntidad, string nIdProyecto, string nIdUsuario)
        {
            var UsuarioDecrypt = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));
            var EntidadDecrypt = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));
            var ProyectoDecrypt = short.Parse(Decrypt(nIdProyecto, EnumCipherType.TDES));

            var CifradoTipo = getCifradoTipo(EntidadDecrypt, UsuarioDecrypt);
            var respuesta = new ResultGetReporteCargue();

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);
                dbmCore.Connection_Open(UsuarioDecrypt);

                var ReportesDT = dbmCore.SchemaConfig.PA_Reportes_Uso_Externo.DBExecute(EntidadDecrypt, ProyectoDecrypt);

                respuesta.Reportes = new List<string>();

                foreach (var RowReporte in ReportesDT)
                {
                    respuesta.Reportes.Add(Encrypt(serializer.Serialize(RowReporte.ToCTA_Reportes_Uso_ExternoSimpleType()), (EnumCipherType)CifradoTipo));
                }
                respuesta.Message = "";
                respuesta.Result = true;

            }
            catch (Exception ex)
            {
                respuesta.Message = "Error: " + ex.Message;
                respuesta.Result = false;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }
            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Uploader.WebService/ResultGetReporteCargue/")]
        public class ResultGetReporteCargue : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<string> Reportes;
        }
        
        [WebMethod]
        public ResultCargueData CargueData(string nIdUsuario, string nIdEntidad, string nIDProyecto, string nIdEsquema, string nIdReporte, string Datos)
        {
            var UsuarioDecrypt = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));
            var EntidadDecrypt = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));
            var ProyectoDecrypt = short.Parse(Decrypt(nIDProyecto, EnumCipherType.TDES));
            var EsquemaDecrypt = short.Parse(Decrypt(nIdEsquema, EnumCipherType.TDES));
            var ReporteDecrypt = short.Parse(Decrypt(nIdReporte, EnumCipherType.TDES));
            var DatosDecrypt = Decrypt(Datos, EnumCipherType.TDES);

            var CifradoTipo = getCifradoTipo(EntidadDecrypt, UsuarioDecrypt);
            var respuesta = new ResultCargueData();
            MemoryStream ms;
            byte[] buffer;

            //DBCore.DBCoreDataBaseManager dbmCore = null;
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //var DatosReporte = serializer.Deserialize(Datos, typeof(DataTable));
            var DatosReporte = serializer.Deserialize<string>(Decrypt(Datos.ToString(CultureInfo.InvariantCulture), (EnumCipherType)CifradoTipo));

            try
            {
                DataSet Data = new DataSet();
                buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(DatosReporte);
                ms = new MemoryStream(buffer);
                Data.ReadXml(ms);

                //dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(Program.IntegrationConnectionString);
                //dbmCore.Connection_Open(nIdUsuario);
                dbmIntegration.Connection_Open(UsuarioDecrypt);

                //var reporte = dbmCore.SchemaConfig.TBL_Reporte.DBGet(nIdReporte)[0];
                //var cSql = reporte.Consulta + " " + reporte.fk_Entidad + ", " + reporte.fk_Proyecto + ", " +
                //           reporte.fk_Esquema;

                BulkInsert.InsertDataTableReport(Data.Tables[0], dbmIntegration, "#Report_Cargue_Archivos");
                var Resultados = dbmIntegration.SchemaProcess.PA_Cargue_DataClient.DBExecute(UsuarioDecrypt, EntidadDecrypt, ProyectoDecrypt, EsquemaDecrypt, ReporteDecrypt);

                //dbmCore.Transaction_Begin();
                //var resultados = SqlData.ExecuteQuery(cSql, dbmCore);
                //dbmCore.Transaction_Commit();

                //respuesta.ResultadoCargue = new List<string>();

                //foreach (DataRow result in Resultados.Rows)
                //{
                //    respuesta.ResultadoCargue.Add(Encrypt(serializer.Serialize(result), (EnumCipherType)CifradoTipo));
                //}
                
            }
            catch (Exception ex)
            {
                //if (dbmCore != null) dbmCore.Transaction_Rollback();
                respuesta.Message =
                    "Error al cargar el archivo a la base de datos, la configuración es diferente al esquema del archivo seleccionado." +
                    ex.Message;
                respuesta.Result = false;
            }
            finally
            {
                //if (dbmCore != null) dbmCore.Connection_Close();
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }

            respuesta.Result = true;
            respuesta.Message = "";

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/ResultCargueData/")]
        public class ResultCargueData : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public List<string> ResultadoCargue;
        }

        [WebMethod]
        public ResultGetCampos GetCampos(string nIdEntidad, string nIdProyecto, string nIdEsquema, string nIdUsuario)
        {
            var _nIdEntidad = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));
            var _nIdProyecto = short.Parse(Decrypt(nIdProyecto, EnumCipherType.TDES));
            var _nIdEsquema = short.Parse(Decrypt(nIdEsquema, EnumCipherType.TDES));
            var _nIdUsuario = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));

            var CifradoTipo = getCifradoTipo(_nIdEntidad, _nIdUsuario);
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var respuesta = new ResultGetCampos();

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(Program.IntegrationConnectionString);
                dbmIntegration.Connection_Open(_nIdUsuario);

                var CamposDT = dbmIntegration.SchemaConfig.TBL_Campos.DBFindByfk_Entidadfk_Proyectofk_Esquema(_nIdEntidad,_nIdProyecto,_nIdEsquema);

                respuesta.Campos = new List<string>();
                foreach (var RowCampos in CamposDT)
                {
                    respuesta.Campos.Add(Encrypt(serializer.Serialize(RowCampos.ToTBL_CamposSimpleType()), (EnumCipherType)CifradoTipo));
                }
            }
            catch (Exception ex)
            {
                if (dbmIntegration != null) dbmIntegration.Transaction_Rollback();
                respuesta.Message =
                    "Error al cargar los campos de este tipo de archivo, verifique la configuración." +
                    ex.Message;
                respuesta.Result = false;
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }

            respuesta.Result = true;
            respuesta.Message = "";

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Banagrario.WebService/ResultGetCampos/")]
        public class ResultGetCampos : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public List<string> Campos;
        }

        [WebMethod]
        public ResultGetOficinas getOfficeList(string nIdUsuario, string nIdEntidad)
        {
            var UsuarioDecrypt = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));
            var EntidadDecrypt = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));

            var CifradoTipo = getCifradoTipo(EntidadDecrypt, UsuarioDecrypt);
            var respuesta = new ResultGetOficinas();
            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.CoreConnectionString);
                dbmCore.Connection_Open(1);

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                var OficinasDT = dbmCore.SchemaConfig.TBL_Oficinas.DBFindByfk_Entidadfk_ProyectoEliminada(EntidadDecrypt, null, false);
                //var OficinasDT = dbmCore.SchemaConfig.CTA_Rol_Acceso.DBFindByfk_Entidadfk_Usuario(EntidadDecrypt, UsuarioDecrypt);

                respuesta.Oficinas = new List<string>();

                foreach (var oficina in OficinasDT)
                {
                    //respuesta.Oficinas.Add(Encrypt(serializer.Serialize(oficina.ToCTA_Rol_AccesoSimpleType()), (EnumCipherType)CifradoTipo));
                    respuesta.Oficinas.Add(Encrypt(serializer.Serialize(oficina.ToTBL_OficinasSimpleType()), (EnumCipherType)CifradoTipo));
                }

                respuesta.Result = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error en obtención de Oficinas: " + ex.Message;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Uploader.WebService/ResultGetOficinas/")]
        public class ResultGetOficinas : ResultBase
        {
            [System.Xml.Serialization.XmlArray]
            public List<String> Oficinas;
        }


        #region Interno

        private short getCifradoTipo(short nIdEntidad, int nIdUsuario)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            short CifradoTipo = 0;

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(Program.IntegrationConnectionString);
                dbmIntegration.Connection_Open(nIdUsuario);

                var EntidadDataTable = dbmIntegration.SchemaConfig.TBL_Entidad.DBGet(nIdEntidad);

                if (EntidadDataTable.Rows.Count > 0)
                {
                    var EntidadRow = EntidadDataTable[0];
                    CifradoTipo = EntidadRow.fk_Cifrado_Tipo;
                }
            }
            catch (Exception ex)
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
                throw new Exception("Error: " + ex.Message);
            }
            return CifradoTipo;
        }

        private static string Encrypt(string nCadenaCifrar, EnumCipherType nTipoCifrado)
        {
            string TextoCifrado;

            switch (nTipoCifrado)
            {
                case EnumCipherType.Nothing :
                    TextoCifrado = Zip(nCadenaCifrar);
                    break;
                case EnumCipherType.TDES:
                    TextoCifrado = Zip(TDESEncrypt(nCadenaCifrar, "Un@Contra$e~AFuer#eNu3v4"));
                    break;
                case EnumCipherType.AES :
                    TextoCifrado = Zip(AESEncrypt(nCadenaCifrar, "MyPassword"));
                    break;
                default :
                    TextoCifrado = "Tipo de Cifrado no reconocido";
                    break;
            }
            return TextoCifrado;
        }

        private string Decrypt(string nCadenaCifrada, EnumCipherType nTipoCifrado)
        {
            string TextoDescifrado;

            switch (nTipoCifrado)
            {
                case EnumCipherType.Nothing:
                    TextoDescifrado = UnZip(nCadenaCifrada);
                    break;
                case EnumCipherType.TDES:
                    TextoDescifrado = TDESDecrypt(UnZip(nCadenaCifrada), "Un@Contra$e~AFuer#eNu3v4");
                    break;
                case EnumCipherType.AES:
                    TextoDescifrado = AESDecrypt(UnZip(nCadenaCifrada), "MyPassword");
                    break;
                default:
                    TextoDescifrado = "Tipo de Cifrado no reconocido";
                    break;
            }
            return TextoDescifrado;
        }


        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">Text to be encrypted</param>
        /// <param name="Password">Password to encrypt with</param>
        /// <param name="Salt">Salt to encrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>An encrypted string</returns>
        public static string AESEncrypt(string PlainText, string Password,
            string Salt = "Kosher", string HashAlgorithm = "SHA1",
            int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",
            int KeySize = 256)
        {
            if (string.IsNullOrEmpty(PlainText))
                return "";
            var InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            var SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            var PlainTextBytes = Encoding.Default.GetBytes(PlainText);
            var DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            var KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            var SymmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            byte[] CipherTextBytes;
            using (var Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (var MemStream = new MemoryStream())
                {
                    using (var CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="CipherText">Text to be decrypted</param>
        /// <param name="Password">Password to decrypt with</param>
        /// <param name="Salt">Salt to decrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string AESDecrypt(string CipherText, string Password,
            string Salt = "Kosher", string HashAlgorithm = "SHA1",
            int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",
            int KeySize = 256)
        {
            if (string.IsNullOrEmpty(CipherText))
                return "";
            var InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            var SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            var CipherTextBytes = Convert.FromBase64String(CipherText);
            var DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            var KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            var SymmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount;
            using (var Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (var MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (var CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {

                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Encoding.Default.GetString(PlainTextBytes, 0, ByteCount);
        }

        public static string TDESEncrypt(string input, string key)
        {
            var inputArray = Encoding.Default.GetBytes(input);
            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.Default.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tripleDES.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string TDESDecrypt(string input, string key)
        {
            var inputArray = Convert.FromBase64String(input);
            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.Default.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tripleDES.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Encoding.Default.GetString(resultArray);
        }

        public static string Zip(string nCadena)
        {
            var buffer = Encoding.Default.GetBytes(nCadena);
            var ms = new MemoryStream();
            using (var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;

            var compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            var gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string UnZip(string compressedText)
        {
            var gzBuffer = Convert.FromBase64String(compressedText);
            using (var ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                var buffer = new byte[msgLength];

                ms.Position = 0;
                using (var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.Default.GetString(buffer, 0, buffer.Length);
            }
        }



        #endregion
    }
}
