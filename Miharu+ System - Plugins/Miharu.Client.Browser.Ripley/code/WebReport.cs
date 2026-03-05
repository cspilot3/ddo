using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;

namespace  Miharu.Client.Browser.code
{
    public abstract class WebReport
    {
        #region Propiedades

        public abstract string ReportName { get; }
        
        #endregion

        #region Constructores

        #endregion

        #region Metodos

        public abstract void Launch(ref ReportViewer nReportViewer);

        public abstract void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters);

        public abstract void Launch(ref ReportViewer reportViewer, NameValueCollection nameValueCollection);
        
        #endregion        

        #region Funciones

        public abstract string getParameter(string nParameterName, string nDefaultValue);

        #endregion
    }
}
