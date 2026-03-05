using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Miharu.Data.ServiceRemote.Utils
{
    public class Program
    {
        public static string CoreConnectionString
        {
            get
            {
                var rootWebConfig = ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Core"];
                if (rootWebConfig != null && rootWebConfig.ConnectionString.Length > 0)
                {
                    return rootWebConfig.ConnectionString;
                }
                else
                {
                    throw new Exception("Por favor asigne la cadena <add name=\"SQLCnnMiharu.Core\" connectionString=\"?\"/> al archivo Web.config.");
                }
            }
        }

        public static string ImagingConnectionString
        {
            get
            {
                var rootWebConfig = ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Imaging"];
                if (rootWebConfig != null && rootWebConfig.ConnectionString.Length > 0)
                {
                    return rootWebConfig.ConnectionString;
                }
                else
                {
                    throw new Exception("Por favor asigne la cadena <add name=\"SQLCnnMiharu.Imaging\" connectionString=\"?\"/> al archivo Web.config.");
                }
            }
        }

        public static string SecurityConnectionString
        {
            get
            {
                var rootWebConfig = ConfigurationManager.ConnectionStrings["SQLCnnMiharu.Security_Core"];
                if (rootWebConfig != null && rootWebConfig.ConnectionString.Length > 0)
                {
                    return rootWebConfig.ConnectionString;
                }
                else
                {
                    throw new Exception("Por favor asigne la cadena <add name=\"SQLCnnMiharu.Security_Core\" connectionString=\"?\"/> al archivo Web.config.");
                }
            }
        }

        public static string DBOCRConnectionString
        {
            get
            {
                var rootWebConfig = ConfigurationManager.ConnectionStrings["SQLCnnMiharu.OCR"];
                if (rootWebConfig != null && rootWebConfig.ConnectionString.Length > 0)
                {
                    return rootWebConfig.ConnectionString;
                }
                else
                {
                    throw new Exception("Por favor asigne la cadena <add name=\"SQLCnnMiharu.OCR\" connectionString=\"?\"/> al archivo Web.config.");
                }
            }
        }

    }
}