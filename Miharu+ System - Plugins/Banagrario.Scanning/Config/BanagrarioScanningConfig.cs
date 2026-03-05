using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Banagrario.Scanning.Config
{
    public class BanagrarioScanningConfig
    {
        #region Constantes

        public const string ConfigFileName = "BanagrarioScanningConfig.dat";

        #endregion

        #region Propiedades

        public string WorkingFolder { get; set; }

        public int OfficeID { get; set; }

        public string OfficeName { get; set; }

        #endregion

        #region Costructores

        public BanagrarioScanningConfig()
        {
            WorkingFolder = "C:\\Banagrario_Scanning";
            OfficeID = -1;
        }

        #endregion

        #region Funciones

        public static void Serialize(BanagrarioScanningConfig nObjectConfig, string nPath)
        {
            XmlSerializer objXmlSerializer = new XmlSerializer(typeof(BanagrarioScanningConfig));
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

        public static BanagrarioScanningConfig Deserialize(string nPath)
        {
            XmlSerializer objXmlSerializer = new XmlSerializer(typeof(BanagrarioScanningConfig));
            TextReader objTextReader = new StreamReader(nPath + ConfigFileName);

            try
            {
                return (BanagrarioScanningConfig)objXmlSerializer.Deserialize(objTextReader);
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

        #endregion
    }
}