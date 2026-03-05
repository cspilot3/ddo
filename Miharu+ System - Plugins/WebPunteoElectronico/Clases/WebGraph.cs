using System.Collections.Generic;
using System.Web.UI.WebControls;
using Slyg.Report;
using System.Data;

namespace WebPunteoElectronico.Clases
{
    public abstract class WebGraph
    {
        #region Enumeraciones

        public enum ModoParametrosEnum: byte
        {
            Movimiento = 1, // Fecha movimiento
            Proceso = 2, // Fecha proceso
            Mixto = 3 // Fecha proceso y Fecha movimiento
        }

        #endregion

        #region Declaraciones

        internal DataReport DataReporte = new DataReport();

        #endregion

        #region Propiedades

        public abstract string ReportName { get; }
        
        public abstract string ReportId { get; }

        public abstract string ZipFileName { get; }

        public abstract List<ModoParametrosEnum> ModoParametros { get; }

        #endregion

        #region Constructores

        public WebGraph() { }

        #endregion

        #region Metodos

        public abstract bool Load(Dictionary<string, object> nParameters);

        public abstract void Draw(ref Literal nLiteral, TipoReporteEnum nTipo, short nWidth, short nHeight);
        
        #endregion

        #region Funciones

        public abstract byte[] BuildZipData();

        public abstract bool Validate(Dictionary<string, object> nParameters, out string nMessageError);

        #endregion
    }
}
