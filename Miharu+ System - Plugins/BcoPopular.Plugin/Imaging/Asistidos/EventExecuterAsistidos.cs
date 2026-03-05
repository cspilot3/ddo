using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miharu.Imaging.Library.Eventos;
using Miharu.Desktop.Library.Plugins;
using DBIntegration;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace Imaging.Asistidos
{
    public class EventExecuterAsistidos : EventExecuter, IEventExecuter
    {
        #region "Declaraciones"

        // ReSharper disable once NotAccessedField.Local
        private AsistidosPlugin _Plugin;
        #endregion

        #region "Constructores"

        public EventExecuterAsistidos(AsistidosPlugin nPlugin)
        {
            this._Plugin = nPlugin;
        }

        #endregion

        #region "Implementation IEventExecuter"

        public void AbrirFechaProceso(short nidEntidadProcesamiento, short nidEntidad, short nidProyecto, int nidFechaProceso)
        {
        }

        public void AbrirOt(int nidOt)
        {
        }

        public void ValidarCerrarFechaProcesoNoOT(short nidEntidadProcesamiento, short nidEntidad, short nidProyecto, int nidFechaProceso, ref bool nValido, ref bool nValido2, ref string nMsgError)
        {
        }

        public void CerrarFechaProceso(short nidEntidadProcesamiento, short nidEntidad, short nidProyecto, int nidFechaProceso)
        {
        }


        public void CerrarOt(int nidOt)
        {
        }

        public void CrearFechaProceso(short nidEntidadProcesamiento, short nidEntidad, short nidProyecto, int nidFechaProceso)
        {
        }


        public void CrearOt(int nidOt)
        {
        }


        public void EliminarImagen(long nidExpediente, short nidFolder, short nidFile)
        {
        }


        public void EnviarReproceso(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarCalidad(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarCargue(int nidCargue)
        {
        }

        public void FinalizarIndexacion(int nidCargue, short nidPaquete)
        {
           
        }


        public void FinalizarPreCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarPrecinto()
        {
        }

        public void FinalizarPrimeraCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarReclasificar(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarRecorte(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarReIndexacion(int nidCargue, short nidPaquete)
        {
        }

        public void FinalizarReproceso(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }


        public void FinalizarSegundaCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }

        public void FinalizarTerceraCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void FinalizarValidaciones(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }


        public void ValidarEmpaque(int nidOt, short nidEmpaque, short nidEsquema, string nToken, ref bool nValido, ref string nMsgError)
        {
        }



        public void ValidarSaveCalidad(System.Collections.Generic.List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
        }


        public void ValidarSavePrimeraCaptura(System.Collections.Generic.List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {

        }


        public void ValidarSaveSegundaCaptura(System.Collections.Generic.List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
        }


        public void ValidarSaveTerceraCaptura(System.Collections.Generic.List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
        }


        public void FinalizarProcesoAdicionalCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void GuardarContenedor(int nidOt, int nidPrecinto, int nidContenedor)
        {
        }


        public void ValidarPrecintoEmpaque(int nidOt, int nidEmpaque, DBImaging.DBImagingDataBaseManager ndbmImaging, Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl nContenedorDesktop, ref bool nValido, ref string nMsgError)
        {
        }


        public void CargarPrecinto(int nidOt, int nidPrecinto, Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl nContenedorDesktop, ref bool nValido, ref string nMsgError)
        {
        }

        public void FinalizarEliminarPrecinto(int nidOt, int nidEmpaque)
        {
        }

        public void ValidarSaveLabelCaptura(System.Collections.Generic.List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, long fk_Expediente, short fk_Folder, short fk_File, string TipoCaptura, ref bool Result)
        {
        }

        public void ValidarActualizarDatoBusqueda(DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow nCampo, long fk_Expediente, short fk_Folder, short fk_File, object newValor_File_Data, ref bool Result)
        {

        }

        public void Ejecutar_Cruce_En_Linea(DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow nCampo, long fk_Expediente, short fk_Folder, short fk_File)
        {
        }

        public void FinalizarLoadConfig(ref Miharu.Imaging.Indexer.View.Indexacion.IIndexerView IndexerView, DBCore.SchemaProcess.TBL_FileRow FileCoreRow)
        {
        }

        public void FinalizarContenedorEmpaque(int nidOt, int nidEmpaque, int IdEmpaqueContenedor)
        {
        }

        public void FinalizarContenedorEmpaqueEliminar(int nidOt, string ntoken)
        {
        }

        public void FinalizarPrecintoEmpaque(int nidOt, int nidEmpaque)
        {
        }

        public void FinalizarActualizarDatosBusqueda(DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow nCampo, long fk_Expediente, short fk_Folder, short fk_File)
        {
        }

        public void Reprocesar(long nidExpediente, Int16 nidFolder, Int16 nidFile)
        {

        }

        public void Validar_Reprocesar(long nidExpediente, Int16 nidFolder, int niDocumento, int nidCampo, int nidCampoTablaAsociada, bool nesCampo, ref bool nValido, ref string nMsgError)
        {

        }

        public string Nombre_Imagen_Exportar(long nidExpediente, short nidFolder, short nidFile, int nFk_Documento, int nGrupo, ref bool nValido, ref string nMsgError)
        {
            return string.Empty;
        }

        public string ExtensionImagen_Plugin(bool Entrada)
        {
            return String.Empty;
        }

        public short IdFormatoImagen_Plugin(bool Entrada)
        {
            return -1;
        }

        public void FinalizarCruceGenerico(short nidEntidad, short nidProyecto, int nidFechaProceso, int nidOT)
        {

        }

        public string Nombre_Imagen_Agrupada_Exportar(long nidExpediente, short nidFolder, int nGrupo, ref bool nValido, ref string nMsgError)
        {
            return string.Empty;
        }

        public void FinalizarCorreccionCapturaMaquina(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
        }

        public void FinalizarPrimeraCapturaAnexo(long nidAnexo)
        {
        }

        public void FinalizarTerceraCapturaAnexo(long nidAnexo)
        {
        }

        public void FinalizarPrepararDataGenerico(short nidEntidad, short nidProyecto, int nidFechaProceso, int nidOT)
        {

        }
        #endregion

    }
}
