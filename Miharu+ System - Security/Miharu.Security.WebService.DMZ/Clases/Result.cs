namespace Miharu.Security.WebService.DMZ.Clases
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://Miharu.Security.WebService.DMZ/Result/")]
    public class ResultBase
    {        
        [System.Xml.Serialization.XmlAttribute]
        public bool Result;

        [System.Xml.Serialization.XmlAttribute]
        public string Message;
    }
}