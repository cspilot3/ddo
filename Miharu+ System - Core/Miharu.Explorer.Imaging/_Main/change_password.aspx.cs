using System;
using System.Web;
using System.Web.Services;
using Miharu.Explorer.Imaging._Clases;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.SecurityDMZServiceReference;
using Miharu.Security.Library.Session;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Miharu.Explorer.Imaging._Main
{
    public partial class change_password : System.Web.UI.Page
    {
        private static Sesion MySesion;

        private enum EnumCipherType
        {
            Nothing = 0,
            TDES = 1,
            AES = 2
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = Utils.MySession;
        }

        private static bool Validar(string nNewPassword, string nConfirmPassword)
        {
            try
            {
                if (nNewPassword == "")
                    throw new Exception("El nuevo password no puede ser vacío");
                //ScriptHelper.Site.ShowAlert(this, "", Utils.MsgBoxIcon.IconError);
                if (nNewPassword != nConfirmPassword)
                    //ScriptHelper.Site.ShowAlert(this, "La confirmación del password no coincide", Utils.MsgBoxIcon.IconError);
                    throw new Exception("La confirmación del password no coincide");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["ErrorAutenticacion"] = ex.Message;
                return false;
            }
            return true;
        }

        [WebMethod]
        public static int Change_Password(string nOldPassword, string nNewPassword, string nConfirmPassword)
        {

            int resultado;
            if (Utils.Interno)
                resultado = Change_Password_Interno(nOldPassword, nNewPassword, nConfirmPassword);
            else
                resultado = Change_Password_Externo(nOldPassword, nNewPassword, nConfirmPassword);

            return resultado;   
        }

        [WebMethod]
        public static int Change_Password_Interno(string nOldPassword, string nNewPassword, string nConfirmPassword)
        {

            if (Validar(nNewPassword, nConfirmPassword))
            {
                var WebService = new Security.Library.WebService.SecurityWebService(
                    Utils.SecurityWebServiceURL, MySesion.ClientIPAddress);

                try
                {
                    WebService.CrearCanalSeguro();
                    WebService.setUser(MySesion.Usuario.Login, nOldPassword);
                    string nMsgError;

                    var Respuesta = WebService.ChangePassword(MySesion.Usuario.Login, nNewPassword,
                        out nMsgError);

                    switch (Respuesta)
                    {
                        case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.INVALIDO_PASSWORD:
                            //ScriptHelper.Site.ShowAlert(this, "Contraseña no válida", Utils.MsgBoxIcon.IconWarning);
                            //break;
                            return 2;
                        case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.ERROR_PASSWORD:
                            //ScriptHelper.Site.ShowAlert(this, nMsgError, Utils.MsgBoxIcon.IconWarning);
                            //break;
                            return 3;
                        case Miharu.Security.Library.SecurityServiceReference.EnumValidateUser.VALIDO:
                            //ScriptHelper.Site.ShowAlert(this, "La contraseña se cambió exitosamente",
                            //                            Utils.MsgBoxIcon.IconInformation);
                            //break;
                            return 1;
                        default:
                            return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    //ScriptHelper.Site.ShowAlert(this, ex.Message, Utils.MsgBoxIcon.IconError);
                }
            }
            return HttpContext.Current.Session["ErrorAutenticacion"].ToString().ToLower().Contains("vacío") ? 4 : 5;
        }

        [WebMethod]
        public static int Change_Password_Externo(string nOldPassword, string nNewPassword, string nConfirmPassword)
        {
            if (Validar(nNewPassword, nConfirmPassword))
            {
                var WebService = new Security.Library.WebService.SecurityDMZWebService(
                    Utils.SecurityWebServiceURL, Utils.MySession.ClientIPAddress);

                try
                {
                    WebService.CrearCanalSeguro();
                    WebService.setUser(MySesion.Usuario.Login, nOldPassword);
                    string nMsgError;

                    //var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultBase();
                    //Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultCrearCanalSeguro)DeSerializeString(Decrypt(WebService.CrearCanalSeguro(), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultBase));

                    var Respuesta = WebService.ChangePassword(MySesion.Usuario.Login, nNewPassword,
                        out nMsgError);

                    switch (Respuesta)
                    {
                        case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.INVALIDO_PASSWORD:
                            //ScriptHelper.Site.ShowAlert(this, "Contraseña no válida", Utils.MsgBoxIcon.IconWarning);
                            //break;
                            return 2;
                        case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.ERROR_PASSWORD:
                            //ScriptHelper.Site.ShowAlert(this, nMsgError, Utils.MsgBoxIcon.IconWarning);
                            //break;
                            return 3;
                        case Miharu.Security.Library.SecurityDMZServiceReference.EnumValidateUser.VALIDO:
                            //ScriptHelper.Site.ShowAlert(this, "La contraseña se cambió exitosamente",
                            //                            Utils.MsgBoxIcon.IconInformation);
                            //break;
                            return 1;
                        default:
                            return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    //ScriptHelper.Site.ShowAlert(this, ex.Message, Utils.MsgBoxIcon.IconError);
                }
            }
            return HttpContext.Current.Session["ErrorAutenticacion"].ToString().ToLower().Contains("vacío") ? 4 : 5;
        }

        //convierte cadena xml a objeto
        public static object DeSerializeString(string strString, Type type)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
            using (TextReader reader = new StringReader(strString))
            {
                object obj = serializer.Deserialize(reader);
                return obj;
            }
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
    }
}