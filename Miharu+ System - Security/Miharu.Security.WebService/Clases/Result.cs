namespace Miharu.Security.WebService.Clases
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://Miharu.Security.WebService/Result/")]
    public class ResultBase
    {        
        [System.Xml.Serialization.XmlAttribute]
        public bool Result;

        [System.Xml.Serialization.XmlAttribute]
        public string Message;
    }
}