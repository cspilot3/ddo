using System;
using System.Collections.Generic;
using System.Web;

namespace Miharu.Security.WebService.App_Code
{
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/Result/")]
    public class ResultBase
    {        
        [System.Xml.Serialization.XmlAttribute]
        public bool Result;

        [System.Xml.Serialization.XmlAttribute]
        public string Message;
    }
}