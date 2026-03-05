using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miharu.Desktop.Library.Plugins;
using Miharu.Imaging.Library.Eventos;
using Miharu.Desktop.Controls.DesktopMessageBox;


namespace BcoColpatria.Plugin.Imaging.Embargos
{
    public class EventExecuterEmbargos : EventExecuter, IEventExecuter
    {
        #region "Declaraciones"      

        private EmbargosPlugin _Plugin;

        #endregion

         #region "Constructores"

        public EventExecuterEmbargos(EmbargosPlugin nPlugin)
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

        public void CargarPrecinto(int nidOt, int nidPrecinto, Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl nContenedorDesktop, ref bool nValido, ref string nMsgError)
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

        public void Ejecutar_Cruce_En_Linea(DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow nCampo, long nidExpediente, short nidFolder, short nidFile)
        {
            
        }

        public void EliminarImagen(long nidExpediente, short nidFolder, short nidFile)
        {
            
        }

        public void EnviarReproceso(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public string ExtensionImagen_Plugin(bool Entrada)
        {
            return String.Empty;
        }

        public void FinalizarActualizarDatosBusqueda(DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow nCampo, long nidExpediente, short nidFolder, short nidFile)
        {
            
        }

        public void FinalizarCalidad(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void FinalizarCargue(int nidCargue)
        {
            
        }

        public void FinalizarContenedorEmpaque(int nidOt, int nidEmpaque, int IdEmpaqueContenedor)
        {
            
        }

        public void FinalizarContenedorEmpaqueEliminar(int nidOt, string nToken)
        {
            
        }

        public void FinalizarCorreccionCapturaMaquina(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void FinalizarCruceGenerico(short nidEntidad, short nidProyecto, int nidFechaProceso, int nidOt)
        {
            
        }

        public void FinalizarEliminarPrecinto(int nidOt, int nidPrecinto)
        {
            
        }

        public void FinalizarIndexacion(int nidCargue, short nidPaquete)
        {
            
        }

        public void FinalizarLoadConfig(ref Miharu.Imaging.Indexer.View.Indexacion.IIndexerView IndexerView, DBCore.SchemaProcess.TBL_FileRow FileCoreRow)
        {
            
        }

        public void FinalizarPreCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void FinalizarPrecinto()
        {
            
        }

        public void FinalizarPrecintoEmpaque(int nidOt, int nidPrecinto)
        {
            
        }

        public void FinalizarPrepararDataGenerico(short nidEntidad, short nidProyecto, int nidFechaProceso, int nidOt)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoColpatriaConnectionString);
            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                dbmIntegration.Transaction_Begin();
                dbmIntegration.SchemaBcoColpatria.PA_Finalizar_Preparar_Data.DBExecute(nidEntidad, nidProyecto, nidFechaProceso, nidOt);
                dbmIntegration.Transaction_Commit();
            }
            catch (Exception ex)
            {
                dbmIntegration.Transaction_Rollback();
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Preparar Data", ref ex);
            }
            finally
            {
                if ((dbmIntegration != null))
                    dbmIntegration.Connection_Close();
            }
        }

        public void FinalizarPrimeraCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void FinalizarPrimeraCapturaAnexo(long nidAnexo)
        {
            
        }

        public void FinalizarProcesoAdicionalCaptura(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void FinalizarReIndexacion(int nidCargue, short nidPaquete)
        {
            
        }

        public void FinalizarReclasificar(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void FinalizarRecorte(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
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

        public void FinalizarTerceraCapturaAnexo(long nidAnexo)
        {
            
        }

        public void FinalizarValidaciones(long nidExpediente, short nidFolder, short nidFile, short nidVersion)
        {
            
        }

        public void GuardarContenedor(int nidOt, int nidPrecinto, int nidContenedor)
        {
            
        }

        public short IdFormatoImagen_Plugin(bool Entrada)
        {
            return -1;
        }

        public string Nombre_Imagen_Agrupada_Exportar(long nidExpediente, short nidFolder, int nGrupo, ref bool nValido, ref string nMsgError)
        {
            return String.Empty;
        }

        public string Nombre_Imagen_Exportar(long nidExpediente, short nidFolder, short nidFile, int nfk_Documento, int nGrupo, ref bool nValido, ref string nMsgError)
        {
            return String.Empty;
        }

        public void Reprocesar(long nidExpediente, short nidFolder, short nidFile)
        {
            
        }

        public void ValidarActualizarDatoBusqueda(DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow nCampo, long nidExpediente, short nidFolder, short nidFile, object newValor_File_Data, ref bool Result)
        {
            
        }

        public void ValidarCerrarFechaProcesoNoOT(short nidEntidadProcesamiento, short nidEntidad, short nidProyecto, int nidFechaProceso, ref bool nValido, ref bool nValido2, ref string nMsgError)
        {
            
        }

        public void ValidarEmpaque(int nidOt, short nidEmpaque, short nidEsquema, string nToken, ref bool nValido, ref string nMsgError)
        {
            
        }

        public void ValidarPrecintoEmpaque(int nidOt, int nidPrecinto, DBImaging.DBImagingDataBaseManager ndbImaging, Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl nContenedorDesktop, ref bool nValido, ref string nMsgError)
        {
            
        }

        public void ValidarSaveCalidad(List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
            
        }

        public void ValidarSaveLabelCaptura(List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, long nidExpediente, short nidFolder, short nidFile, string TipoCaptura, ref bool Result)
        {
            
        }

        public void ValidarSavePrimeraCaptura(List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
            
        }

        public void ValidarSaveSegundaCaptura(List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
            
        }

        public void ValidarSaveTerceraCaptura(List<Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura> campos, int fk_documento, ref bool Result)
        {
            
        }

        public void Validar_Reprocesar(long nidExpediente, short nidFolder, int nidDocumento, int nidCampo, int nidCampoTablaAsociada, bool nesCampo, ref bool nValido, ref string nMsgError)
        {
            
        }
        #endregion
    }
}
