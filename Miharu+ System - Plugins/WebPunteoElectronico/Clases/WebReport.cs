using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;

namespace WebPunteoElectronico.Clases
{
    public abstract class WebReport
    {
        #region Propiedades

        public abstract string ReportName { get; }

        #endregion

        #region Constructores

        public WebReport() { }

        #endregion

        #region Metodos

        public virtual void Reload(ref ReportViewer nReportViewer)
        {
            throw new Exception("No se ha implementado el método RELOAD para el reporte: " + this.ReportName);
        }

        public abstract void Launch(ref ReportViewer nReportViewer);

        public abstract void Launch(ref ReportViewer nReportViewer, Dictionary<string, object> nParameters);

        public abstract void Launch(ref ReportViewer nReportViewer, NameValueCollection nQueryString);

        #endregion

        #region Funciones

        public abstract string getParameter(string nParameterName);

        public abstract bool Validate(Dictionary<string, object> nParameters, out string nMessageError);

        #endregion
    }
}
