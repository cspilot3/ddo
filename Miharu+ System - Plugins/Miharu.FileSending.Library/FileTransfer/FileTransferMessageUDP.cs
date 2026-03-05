using System;
using System.Collections.Generic;
using System.Text;

namespace Slyg.Tools.Net.FileTransfer
{
    [Serializable]
    public class FileTransferMessageUDP
    {
        [System.Xml.Serialization.XmlAttribute()]
        public Guid Identificador { get; set; }

        [System.Xml.Serialization.XmlAttribute()]
        public int PartIndex { get; set; }

        [System.Xml.Serialization.XmlAttribute()]
        public byte[] Data { get; set; }
    }
}
