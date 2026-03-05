using System;
using System.Collections.Generic;
using System.Text;

namespace Slyg.Tools.Net.FileTransfer
{
    [Serializable]
    public class FileTransferMessageTCP
    {
        #region Enumeraciones

        public enum Commands : short
        {
            SEND_FILE = 1,
            READY = 2,
            OKSend = 3,
            OKReceive = 4
        }

        #endregion

        #region Propiedades

        [System.Xml.Serialization.XmlAttribute()]
        public Commands Command { get; set; }

        [System.Xml.Serialization.XmlElement()]
        public object[] Parameters { get; set; }

        #endregion

        #region Clases

        public class SEND_FILEParameter
        {
            public SEND_FILEParameter()
            {
            }

            public SEND_FILEParameter(object[] nParameters)
            {
                Identifier = (Guid)nParameters[0];
                FileName = nParameters[1].ToString();
                FileSize = int.Parse(nParameters[2].ToString());
                PackageSize = int.Parse(nParameters[3].ToString());
                FilePackages = long.Parse(nParameters[4].ToString());
                ServerIPAddress = nParameters[5].ToString();
                TCPPort = int.Parse(nParameters[6].ToString());
                UDPPort = int.Parse(nParameters[7].ToString());
            }

            public Guid Identifier { set; get; }

            public string FileName { set; get; }

            public int FileSize { get; set; }

            public int PackageSize { get; set; }

            public long FilePackages { get; set; }

            public string ServerIPAddress { get; set; }

            public int TCPPort { get; set; }

            public int UDPPort { get; set; }

            public object[] getParameters()
            {
                return new object[8] { Identifier, FileName, FileSize, PackageSize, FilePackages, ServerIPAddress, TCPPort, UDPPort };
            }
        }

        public class READYParameter
        {
            public READYParameter()
            {
            }

            public READYParameter(object[] nParameters)
            {
                Identifier = (Guid)nParameters[0];
                All = (bool)nParameters[1];

                if (nParameters.Length > 2)
                {
                    Parts = new int[nParameters.Length - 2];
                    
                    for (int i = 0; i < Parts.Length; i++)
                    {
                        Parts[i] = (int)nParameters[i + 2];
                    }
                }
            }

            public Guid Identifier { set; get; }

            public bool All { set; get; }

            public int[] Parts { set; get; }

            public object[] getParameters()
            {
                object[] Parametros;

                if (!All && Parts != null)
                {
                    Parametros = new object[Parts.Length + 2];
                    Parametros[0] = Identifier;
                    Parametros[1] = All;

                    for (int i = 0; i < Parts.Length; i++)
                    {
                        Parametros[i + 2] = Parts[i];
                    }
                }
                else
                {
                    Parametros = new object[2] { Identifier, All };
                }

                return Parametros;
            }
        }

        public class OKSendParameter
        {
            public OKSendParameter()
            {
            }

            public OKSendParameter(object[] nParameters)
            {
                Identifier = (Guid)nParameters[0];
            }

            public Guid Identifier { set; get; }

            public object[] getParameters()
            {
                return new object[1] { Identifier };
            }
        }

        public class OKReceiveParameter
        {
            public OKReceiveParameter()
            {
            }

            public OKReceiveParameter(object[] nParameters)
            {
                Identifier = (Guid)nParameters[0];
            }

            public Guid Identifier { set; get; }

            public object[] getParameters()
            {
                return new object[1] { Identifier };
            }
        }

        #endregion
    }
}
