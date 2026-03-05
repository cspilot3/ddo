using System;
using System.Data;
using System.Text;

namespace Slyg.Report
{
    public class DataReport
    {
        public String Tipo_Reporte;
        public String Id_Reporte;
        public String Nombre_Reporte;
        public String EjeX;
        public String EjeY;
        public Int32  PrecisionDecimal;
        public DataTable Datos;
        public String DatosXML;        
        public String CampoMostrarX;
    }
    public class DataReportReturn
    {
        public String DatosXML;
        public byte[] DatosTXT;
    }
}
