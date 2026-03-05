using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Slyg.Tools.Net.FileTransfer
{
    public class FileTransferDefinition
    {
        #region Enumeraciones

        public enum DefinitionType : byte
        {
            CLIENT = 1,
            SERVER = 2,
            PUSH = 3
        }

        #endregion

        #region Propiedades

        public Guid Identifier { get; set; }

        public DefinitionType Type { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public int PackageSize { get; set; }

        public long FilePackages { get; set; }

        public bool[] Parts { get; set; }

        public string ServerIPAddress { get; set; }

        public int TCPPort { get; set; }

        public int UDPPort { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdate { get; set; }
                
        #endregion

        #region Funciones

        public static void Serialize(FileTransferDefinition nObject, string nFileName)
        {
            XmlSerializer Serializador = new XmlSerializer(typeof(FileTransferDefinition));
            var Archivo = new StreamWriter(nFileName);

            try
            {
                nObject.LastUpdate = DateTime.Now;
                Serializador.Serialize(Archivo, nObject);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo almacenar el archivo. " + ex.Message, ex);
            }
            finally
            {
                Archivo.Close();
            }
        }

        public static FileTransferDefinition Deserialize(string nFileName)
        {
            XmlSerializer Serializador = new XmlSerializer(typeof(FileTransferDefinition));
            TextReader Archivo = new StreamReader(nFileName);

            try
            {
                return (FileTransferDefinition)Serializador.Deserialize(Archivo);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo leer el archivo. " + ex.Message, ex);
            }
            finally
            {
                Archivo.Close();
            }
        }

        public bool IsCompleted()
        {
            if (this.Parts != null)
            {
                for (int i = 0; i < this.Parts.Length; i++)
                {
                    if (!this.Parts[i])
                        return false;
                }
            }

            return true;
        }

        public int[] getMissingParts(int nMaxParts)
        {
            if (this.Parts != null)
            {
                List<int> PartIndex = new List<int>();

                for (int i = 0; i < this.Parts.Length && PartIndex.Count < nMaxParts; i++)
                {
                    if (!this.Parts[i])
                        PartIndex.Add(i);
                }

                return PartIndex.ToArray();
            }
            else
            {
                return null;
            }
        }

        #endregion       
    }
}
