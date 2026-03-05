using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Slyg.Tools;

namespace Miharu.Uploader.Config
{
    public class UploaderConfig
    {
        #region Constantes

        public const string ConfigFileName = "UploaderConfig.dat";

        
        public enum EnumCipherType
        {
            Nothing = 0,
            TDES = 1,
            AES = 2
        }

        #endregion

        #region Propiedades

        public string WorkingFolder { get; set; }
        public int OfficeID { get; set; }
        public string OfficeName { get; set; }

        #endregion

        #region Costructores

        public UploaderConfig()
        {
            WorkingFolder = "C:\\MiharuUploader";
        }

        #endregion

        #region Funciones

        public static void Serialize(UploaderConfig nObjectConfig, string nPath)
        {
            var objXmlSerializer = new XmlSerializer(typeof(UploaderConfig));
            var objTextWriter = new StreamWriter(nPath + ConfigFileName);

            try
            {
                objXmlSerializer.Serialize(objTextWriter, nObjectConfig);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo leer el archivo de configuración. " + ex.Message, ex);
            }
            finally
            {
                objTextWriter.Close();
            }
        }

        public static UploaderConfig Deserialize(string nPath)
        {
            var objXmlSerializer = new XmlSerializer(typeof(UploaderConfig));
            TextReader objTextReader = new StreamReader(nPath + ConfigFileName);

            try
            {
                return (UploaderConfig)objXmlSerializer.Deserialize(objTextReader);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo leer el archivo de configuración. " + ex.Message, ex);
            }
            finally
            {
                objTextReader.Close();
            }
        }

        public static string Encrypt(string nCadenaCifrar, EnumCipherType nTipoCifrado)
        {
            string TextoCifrado;

            switch (nTipoCifrado)
            {
                case EnumCipherType.Nothing:
                    TextoCifrado = Zip(nCadenaCifrar);
                    break;
                case EnumCipherType.TDES:
                    TextoCifrado = Zip(TDESEncrypt(nCadenaCifrar, "Un@Contra$e~AFuer#eNu3v4"));
                    break;
                case EnumCipherType.AES:
                    TextoCifrado = Zip(AESEncrypt(nCadenaCifrar, "MyPassword"));
                    break;
                default:
                    TextoCifrado = "Tipo de Cifrado no reconocido";
                    break;
            }
            return TextoCifrado;
        }

        public static string Decrypt(string nCadenaCifrada, EnumCipherType nTipoCifrado)
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
            var inputArray = Encoding.UTF8.GetBytes(input);
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
            var tripleDES = new TripleDESCryptoServiceProvider()
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