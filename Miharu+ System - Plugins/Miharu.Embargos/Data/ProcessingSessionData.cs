using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.CentralizacionEmbargos.Data
{
    public class ProcessingSessionData
    {
        #region " Propiedades "

        public DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoType ExpedienteCruceEnLineaRow { get; set; }
        public DBCore.SchemaImaging.CTA_FileSimpleType FileRow { get; set; }
        public int usuario_log { get; set; }
        public Guid sesionID { get; set; }
        public string pcName { get; set; }
        public int Linea { get; set; }
        public string documentDataPath { get; set; }
        public string hOCRCoordPath { get; set; }
        public string hOCRTokePath { get; set; }

        #endregion

        #region ' Metodos '

        public void SetProcessingSessionData(DBCore.SchemaImaging.CTA_FileRow expedienteFileRow, DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoRow nExpedienteRow)
        {
            SetConfigInitial();
            SetExpedienteCruceEnLinea(nExpedienteRow);
            SetFileRow(expedienteFileRow);
        }

        public void SetExpedienteCruceEnLinea(DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoRow expedienteCruceEnlinea)
        {
            ExpedienteCruceEnLineaRow = new DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoType
            {
                fk_Entidad = expedienteCruceEnlinea.fk_Entidad,
                fk_Proyecto = expedienteCruceEnlinea.fk_Proyecto,
                fk_Expediente = expedienteCruceEnlinea.fk_Expediente,
                fk_Folder = expedienteCruceEnlinea.fk_Folder,
                fk_File = expedienteCruceEnlinea.fk_File,
                Etapa = expedienteCruceEnlinea.IsEtapaNull() ? "1": expedienteCruceEnlinea.Etapa,
                Bajar_Datos = expedienteCruceEnlinea.Bajar_Datos,
                Pendiente_Eliminar = expedienteCruceEnlinea.Pendiente_Eliminar
            };
        }

        public void SetFileRow(DBCore.SchemaImaging.CTA_FileRow expedienteFileRow)
        {
            FileRow = new DBCore.SchemaImaging.CTA_FileSimpleType
            {
                fk_Expediente = expedienteFileRow.fk_Expediente,
                fk_Folder = expedienteFileRow.fk_Folder,
                fk_File = expedienteFileRow.fk_File,
                fk_Entidad_Servidor = expedienteFileRow.Isfk_Entidad_ServidorNull() ? (short)0 : expedienteFileRow.fk_Entidad_Servidor,
                fk_Servidor = expedienteFileRow.Isfk_ServidorNull() ? (short)0 : expedienteFileRow.fk_Servidor,
                fk_Cargue = expedienteFileRow.Isfk_CargueNull() ? 0 : expedienteFileRow.fk_Cargue,
                fk_Cargue_Paquete = expedienteFileRow.Isfk_Cargue_PaqueteNull() ? (short)0 : expedienteFileRow.fk_Cargue_Paquete,
                id_Version = expedienteFileRow.id_Version,
                fk_Documento = expedienteFileRow.fk_Documento,
                Es_Anexo = expedienteFileRow.Es_Anexo,
                fk_Anexo = expedienteFileRow.Isfk_AnexoNull() ? 0 : expedienteFileRow.fk_Anexo,
                Validaciones_Opcionales = expedienteFileRow.Validaciones_Opcionales,
                fk_Content_Type = expedienteFileRow.fk_Content_Type
            };
        }

        

        private void SetConfigInitial()
        {
            usuario_log = Embargos.Program.Config.usuario_log;
            sesionID = Embargos.Program.Config.sesionID;
            pcName = Embargos.Program.Config.pcName;
            Linea = Embargos.Program.Config.Linea;
        }
        #endregion

        #region ' Funciones '

        #endregion

    }
}
