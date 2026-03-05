using Miharu.CentralizacionEmbargos.Data;
using Miharu.CentralizacionEmbargos.Logging;
using Miharu.CentralizacionEmbargos.Services.Threading;
using Miharu.Desktop.Library.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Miharu.Embargos
{
    public partial class CentralizacionEmbargosService : ServiceBase
    {
        #region " Declaraciones "

        private volatile bool stopRequested = false;
        private FileLogger dataFileLoggger = new FileLogger();

        #endregion


        #region " Constructores "

        public CentralizacionEmbargosService()
        {
            InitializeComponent();
        }

        #endregion

        #region " Metodos "

        protected override void OnStart(string[] args)
        {
            IniciarServicio();
        }

        protected override void OnStop()
        {
            DetenerServicio();
        }

        private void IniciarServicio()
        {
            try
            {
                Program.ConnectionStrings = Program.Config.GetCadenasConexion();

                if (Program.ConnectionStrings.Security == "")
                {
                    dataFileLoggger.AddErrorEntry("No se pudo obtener la cadena de conexión a la base de datos Secuity");
                    Stop();
                    return;
                }

                if (Program.ConnectionStrings.Core == "")
                {
                    dataFileLoggger.AddErrorEntry("No se pudo obtener la cadena de conexión a la base de datos Core");
                    Stop();
                    return;
                }

                if (Program.ConnectionStrings.Imaging == "")
                {
                    dataFileLoggger.AddErrorEntry("No se pudo obtener la cadena de conexión a la base de datos Imaging");
                    Stop();
                    return;
                }

                if (Program.ConnectionStrings.OCR == "")
                {
                    dataFileLoggger.AddErrorEntry("No se pudo obtener la cadena de conexión a la base de datos OCR");
                    Stop();
                    return;
                }

                Thread workerThread = new Thread(Proceso);

                stopRequested = false;
                workerThread.Start();

            }
            catch (Exception ex)
            {
                dataFileLoggger.AddErrorEntry("Error IniciarServicio ex: " + ex.Message + " " + ex.StackTrace);
                Stop();
            }
        }

        private void DetenerServicio()
        {
            stopRequested = true;      // Solicitar detener el servicio
        }

        private void Proceso()
        {
            try
            {
                while (!stopRequested)
                {
                    try
                    {
                        var dtExpedientesForCruceEnLinea = GetExpedientesForCruceEnLinea();

                        if (dtExpedientesForCruceEnLinea.Count == 0)
                        {
                            TransferPendingRecordsToProcessing();
                        }
                        else
                        {
                            try
                            {
                                Program.ConnectionParametersStrings = Program.Config.GetParametersSystem();

                                ProcesadorHilos procesadorHilosInstance = new ProcesadorHilos();
                                procesadorHilosInstance.servicio = this;

                                foreach (var nExpedienteRow in dtExpedientesForCruceEnLinea)
                                {
                                    try
                                    {
                                        procesadorHilosInstance.AgregarHilo(nExpedienteRow);
                                    }
                                    catch (Exception ex)
                                    {
                                        dataFileLoggger.AddErrorEntry($"Error Hilo con Expediente: {nExpedienteRow.fk_Expediente} " + ex.ToString());
                                    }
                                }

                                while (!procesadorHilosInstance.TerminoHilos())
                                {
                                    System.Threading.Thread.Sleep(100);
                                }
                            }
                            catch (Exception ex)
                            {
                                dataFileLoggger.AddErrorEntry("Error Hilo Principal Procesamiento En Cola: " + ex.ToString());
                            }
                            finally
                            {
                                // Eliminamos registros Cola
                                DeletePendingRecordsEnColaAndProceso();
                            }
                        }

                        if (stopRequested) return;

                        Thread.Sleep(Program.Config.Intervalo);  // Esperar n segundos antes de continuar
                    }
                    catch (Exception ex)
                    {
                        dataFileLoggger.AddErrorEntry("Error Proceso : " + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                dataFileLoggger.AddErrorEntry("**TERMINACIÓN HILO PRINCIPAL**: Se ha producido un error durante la ejecución del hilo principal del servicio. Detalles del error: " + ex.ToString());
            }
        }

        public void ProcesoPrincipalHilo(Object nParametroHilo)
        {

            ProcessingSessionData processingSessionData = new ProcessingSessionData();

            DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos = null;
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmEmbargos = new DBEmbargos.DBEmbargosDataBaseManager(Program.ConnectionStrings.Embargos);
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging);
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core);

                dbmEmbargos.Connection_Open(Program.Config.usuario_log);
                dbmImaging.Connection_Open(Program.Config.usuario_log);
                dbmCore.Connection_Open(Program.Config.usuario_log);

                var nExpedienteRow = (DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoRow)nParametroHilo;

                if (nExpedienteRow == null)
                    throw new Exception("No se encuentran Configuraciones en ocrCaptureFile y extractionRules para procesar.");

                var fileRow = GetExpedientesFileRow(ref dbmCore, nExpedienteRow);
                processingSessionData.SetProcessingSessionData(fileRow, nExpedienteRow);                              

                // Bloquea el Expediente del docuemnto
                bool fileBlocked = BlockFileForProcessing(ref dbmImaging, ref dbmCore, processingSessionData, nExpedienteRow.Bajar_Datos);
                if(fileBlocked)
                {
                    try
                    {
                        var blockedFilesTableRow = GetBlockedFilesTable(ref dbmImaging, processingSessionData);
                        if (blockedFilesTableRow != null)
                        {
                            if (nExpedienteRow.Bajar_Datos == true)
                            {
                                ProcessCaseAndDownloadInheritanceData(ref dbmEmbargos, ref dbmImaging, ref dbmCore, processingSessionData);
                            }
                            else
                            {
                                ProcessCaseForDocumentMatch(ref dbmEmbargos, ref dbmImaging, ref dbmCore, processingSessionData);
                            }
                        }
                    }
                    catch
                    {
                        UnLockExpediente(processingSessionData);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (dbmEmbargos != null) dbmEmbargos.Connection_Close();
                if (dbmImaging != null) dbmImaging.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }


        private void ProcessCaseAndDownloadInheritanceData(ref DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos, ref DBImaging.DBImagingDataBaseManager dbmImaging, ref DBCore.DBCoreDataBaseManager dbmCore, ProcessingSessionData processingSessionData)
        {

            var fkEntidadBase = processingSessionData.ExpedienteCruceEnLineaRow.fk_Entidad;
            var fkProyectoBase = processingSessionData.ExpedienteCruceEnLineaRow.fk_Proyecto;
            var fkCargueBase = processingSessionData.FileRow.fk_Cargue;
            var fkDocumentoBase = processingSessionData.FileRow.fk_Documento;
            var fkExpedienteBase = processingSessionData.FileRow.fk_Expediente;
            var fkFolderBase = processingSessionData.FileRow.fk_Folder;
            var fkFileBase = processingSessionData.FileRow.fk_File;

            // Bajar data
            dbmEmbargos.SchemaProcess.PA_Bajar_Data.DBExecute(fkExpedienteBase, fkFolderBase, fkCargueBase);

            //verificar si se encuentra el expediente en Herencia pendiente
            var dtexpedienteHerencia = dbmEmbargos.SchemaProcess.TBL_Herencia_Expediente.DBFindByfk_Expediente_OrigenHerencia_Completada(fkExpedienteBase, false);
            if(dtexpedienteHerencia != null && dtexpedienteHerencia.Count > 0)
            {
                var expedienteHerenciaRow = dtexpedienteHerencia[0];

                // Inserta registros en la cola de cruce en línea para el expediente destino y ser procesado para bajar data,
                dbmEmbargos.SchemaProcess.PA_Insertar_Cruce_En_Linea_Masivo.DBExecute(expedienteHerenciaRow.fk_Expediente_Destino, "1");

                // Actualizamos Herencia 
                var herenciaExpediente = new DBEmbargos.SchemaProcess.TBL_Herencia_ExpedienteType();
                herenciaExpediente.Fecha_Herencia = DateTime.Now;
                herenciaExpediente.Herencia_Completada = true;
                dbmEmbargos.SchemaProcess.TBL_Herencia_Expediente.DBUpdate(herenciaExpediente, expedienteHerenciaRow.id_Herencia_Expediente);
            }

            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(fkExpedienteBase, fkFolderBase, fkFileBase);

            // Marcar a Eliminar
            dbmEmbargos.SchemaProcess.PA_Cruce_Linea_Proceso_Marcar_Eliminar.DBExecute(fkEntidadBase, fkProyectoBase, fkExpedienteBase, fkFolderBase, fkFileBase);
        }

        private void ProcessCaseForDocumentMatch(ref DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos,ref DBImaging.DBImagingDataBaseManager dbmImaging, ref DBCore.DBCoreDataBaseManager dbmCore, ProcessingSessionData processingSessionData)
        {
            var entidadId = processingSessionData.ExpedienteCruceEnLineaRow.fk_Entidad;
            var proyectoId = processingSessionData.ExpedienteCruceEnLineaRow.fk_Proyecto;
            var documentoId = processingSessionData.FileRow.fk_Documento;
            var expedienteId = processingSessionData.FileRow.fk_Expediente;
            var folderId = processingSessionData.FileRow.fk_Folder;
            var fileId = processingSessionData.FileRow.fk_File;

            try
            {
                var dtExpedienteCruce = dbmEmbargos.SchemaProcess.PA_Validar_Expediente_Llaves_Cruce.DBExecute(documentoId, expedienteId, folderId, fileId);
                var expedienteCruce = (dtExpedienteCruce != null && dtExpedienteCruce.Rows.Count > 0) ? Convert.ToInt32(dtExpedienteCruce.Rows[0]["fk_Expediente"]) : 0;

                if (expedienteCruce != 0 )
                {// Cruzo

                    // traer información a heredar.
                    var dtExpedienteProcessodata = dbmEmbargos.SchemaProcess.TBL_Proceso_Data.DBFindByfk_Expediente(expedienteCruce);
                    if(dtExpedienteProcessodata != null && dtExpedienteProcessodata.Count > 0)
                    {
                        //Asocia los campos de la matriz documentos y pasarlo a las tablas de tbl_file_data y tbl_file_data_asociada respetivamente
                        dbmEmbargos.SchemaProcess.PA_TransferenciaDataCruzada_Expediente.DBExecute(expedienteCruce, entidadId, documentoId, expedienteId);

                        // Actualiza Registro de Herencia.
                        var dtHerenciaExpediente = dbmEmbargos.SchemaProcess.TBL_Herencia_Expediente.DBFindByfk_Expediente_Destino(expedienteId);
                        if(dtHerenciaExpediente != null && dtHerenciaExpediente.Count > 0)
                        {
                            var herenciaExpedienteRow = dtHerenciaExpediente[0];

                            var herenciaExpediente = new DBEmbargos.SchemaProcess.TBL_Herencia_ExpedienteType();
                            herenciaExpediente.fk_Expediente_Origen = expedienteCruce;
                            herenciaExpediente.Fecha_Herencia = DateTime.Now;
                            herenciaExpediente.Herencia_Completada = true;
                            dbmEmbargos.SchemaProcess.TBL_Herencia_Expediente.DBUpdate(herenciaExpediente, herenciaExpedienteRow.id_Herencia_Expediente);
                        }
                        else
                        {
                            var herenciaExpediente = new DBEmbargos.SchemaProcess.TBL_Herencia_ExpedienteType();
                            herenciaExpediente.fk_Expediente_Origen = expedienteCruce;
                            herenciaExpediente.fk_Expediente_Destino = expedienteId;
                            herenciaExpediente.Fecha_Registro = DateTime.Now;
                            herenciaExpediente.Fecha_Herencia = DateTime.Now;
                            herenciaExpediente.Herencia_Completada = true;
                            dbmEmbargos.SchemaProcess.TBL_Herencia_Expediente.DBInsert(herenciaExpediente);
                        }

                        // Actualizar a estado Indexado (38)
                        var UpdateEstado = new DBCore.SchemaProcess.TBL_File_EstadoType();
                        UpdateEstado.Fecha_Log = DateTime.Now;
                        UpdateEstado.fk_Usuario = processingSessionData.usuario_log;
                        UpdateEstado.fk_Estado = (short)DBCore.EstadoEnum.Indexado;
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, expedienteId, folderId, fileId, (Byte)Desktop.Library.Config.DesktopConfig.Modulo.Imaging);

                        // Eliminar registro por estado 38
                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(expedienteId, folderId, fileId);
                    }
                    else
                    {
                        var herenciaExpediente = new DBEmbargos.SchemaProcess.TBL_Herencia_ExpedienteType();
                        herenciaExpediente.fk_Expediente_Origen = expedienteCruce;
                        herenciaExpediente.fk_Expediente_Destino = expedienteId;
                        herenciaExpediente.Fecha_Registro = DateTime.Now;
                        herenciaExpediente.Herencia_Completada = false;
                        dbmEmbargos.SchemaProcess.TBL_Herencia_Expediente.DBInsert(herenciaExpediente);

                        // Actualizar a estado Herencia Pendiente (53)
                        var UpdateEstado = new DBCore.SchemaProcess.TBL_File_EstadoType();
                        UpdateEstado.Fecha_Log = DateTime.Now;
                        UpdateEstado.fk_Usuario = processingSessionData.usuario_log;
                        UpdateEstado.fk_Estado = (short)DBCore.EstadoEnum.Herencia_Pendiente;
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, expedienteId, folderId, fileId, (Byte)Desktop.Library.Config.DesktopConfig.Modulo.Imaging);

                        var CapDashboardType = new DBImaging.SchemaProcess.TBL_Dashboard_CapturasType();
                        CapDashboardType.fk_Usuario_log = DBNull.Value;
                        CapDashboardType.Sesion = DBNull.Value;
                        CapDashboardType.fk_Estado = (short)DBCore.EstadoEnum.Herencia_Pendiente;

                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, expedienteId, folderId, fileId); 
                    }

                    // Marcar a Eliminar
                    dbmEmbargos.SchemaProcess.PA_Cruce_Linea_Proceso_Marcar_Eliminar.DBExecute(entidadId, proyectoId, expedienteId, folderId, fileId);
                }
                else
                {// No cruzo

                    dbmEmbargos.SchemaProcess.PA_Insertar_Llaves.DBExecute(documentoId, expedienteId, folderId, fileId);

                    var NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(documentoId, (short)DBCore.EstadoEnum.Procesamiento_Llaves);

                    // Actualizar estado al File
                    var UpdateEstado = new DBCore.SchemaProcess.TBL_File_EstadoType();
                    UpdateEstado.Fecha_Log = DateTime.Now;
                    UpdateEstado.fk_Usuario = processingSessionData.usuario_log;
                    UpdateEstado.fk_Estado = NextEstado;
                    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, expedienteId, folderId, fileId, (Byte)Desktop.Library.Config.DesktopConfig.Modulo.Imaging);

                    // Actualizar Dashboard
                    if (NextEstado == (short)DBCore.EstadoEnum.Indexado)
                    {
                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(expedienteId, folderId, fileId);
                    }
                    else
                    {
                        var CapDashboardType = new DBImaging.SchemaProcess.TBL_Dashboard_CapturasType();
                        CapDashboardType.fk_Usuario_log = DBNull.Value;
                        CapDashboardType.Sesion = DBNull.Value;
                        CapDashboardType.fk_Estado = NextEstado;

                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, expedienteId, folderId, fileId);
                    }

                    // Marcar a Eliminar
                    dbmEmbargos.SchemaProcess.PA_Cruce_Linea_Proceso_Marcar_Eliminar.DBExecute(entidadId, proyectoId, expedienteId, folderId, fileId);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina los registros marcados como pendientes para eliminar en las tablas 
        /// [TBL_Cruce_En_Linea_Cola] y [TBL_Cruce_En_Linea_Proceso], utilizando un procedimiento almacenado.
        /// </summary>
        private void DeletePendingRecordsEnColaAndProceso()
        {
            DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos = null;

            try
            {
                dbmEmbargos = new DBEmbargos.DBEmbargosDataBaseManager(Program.ConnectionStrings.Embargos);
                dbmEmbargos.Connection_Open(Program.Config.usuario_log);

                dbmEmbargos.SchemaProcess.PA_Eliminar_Cruce_En_Linea.DBExecute();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbmEmbargos != null) dbmEmbargos.Connection_Close();
            }
        }

        /// <summary>
        /// Ejecuta la lógica de marcado e inserción de registros en la tabla de procesamiento de cruces en línea,
        /// moviéndolos desde la tabla de cola solo si no hay procesos en curso.
        /// </summary>
        private void TransferPendingRecordsToProcessing()
        {
            DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos = null;

            try
            {
                dbmEmbargos = new DBEmbargos.DBEmbargosDataBaseManager(Program.ConnectionStrings.Embargos);
                dbmEmbargos.Connection_Open(Program.Config.usuario_log);

                dbmEmbargos.SchemaProcess.PA_Cruce_En_Linea_Proceso.DBExecute();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbmEmbargos != null) dbmEmbargos.Connection_Close();
            }
        }

        #endregion


        #region " Funciones "

        private DBImaging.SchemaProcess.TBL_Dashboard_CapturasRow GetBlockedFilesTable(ref DBImaging.DBImagingDataBaseManager dbmImaging, ProcessingSessionData processingSessionData)
        {
            try
            {
                var dashboardCapturasTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Carguefk_Cargue_Paquetefk_Entidadfk_Proyecto(processingSessionData.FileRow.fk_Expediente, 
                                                                                                                                                                                                                                                  processingSessionData.FileRow.fk_Folder,
                                                                                                                                                                                                                                                  processingSessionData.FileRow.fk_File,
                                                                                                                                                                                                                                                  processingSessionData.FileRow.fk_Documento,
                                                                                                                                                                                                                                                  processingSessionData.FileRow.fk_Cargue,
                                                                                                                                                                                                                                                  processingSessionData.FileRow.fk_Cargue_Paquete,
                                                                                                                                                                                                                                                  (short)processingSessionData.ExpedienteCruceEnLineaRow.fk_Entidad,
                                                                                                                                                                                                                                                  (short)processingSessionData.ExpedienteCruceEnLineaRow.fk_Proyecto);
                return dashboardCapturasTable?.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Intenta bloquear un archivo para su procesamiento en el módulo de Imaging, 
        /// asignando el estado adecuado según el origen de los datos.
        /// </summary>
        /// <param name="dbmImaging">Referencia al administrador de base de datos del módulo Imaging.</param>
        /// <param name="dbmCore">Referencia al administrador de base de datos (Core).</param>
        /// <param name="processingSessionData">Datos de sesión que contienen información del archivo y del usuario.</param>
        /// <param name="loadFromDataRow">Indica si el estado debe basarse directamente en los datos cargados desde DataRow.</param>
        /// <returns>True si el archivo fue bloqueado exitosamente; de lo contrario, false.</returns>
        private bool BlockFileForProcessing(ref DBImaging.DBImagingDataBaseManager dbmImaging, ref DBCore.DBCoreDataBaseManager dbmCore, ProcessingSessionData processingSessionData, bool loadFromDataRow)
        {
            try
            {
                // Determinar el estado del expediente dependiendo de la fuente de datos
                short expedienteStatus = loadFromDataRow
                                                ? (short)DBCore.EstadoEnum.Indexado
                                                : GetCurrentExpedienteStatus(ref dbmCore, processingSessionData);

                bool statusBloqueFile = dbmImaging.SchemaProcess.PA_Bloqueo_File.DBExecute(processingSessionData.FileRow.fk_Expediente,
                                                                                           processingSessionData.FileRow.fk_Folder,
                                                                                           processingSessionData.FileRow.fk_File,
                                                                                           expedienteStatus,
                                                                                           processingSessionData.usuario_log,
                                                                                           processingSessionData.sesionID,
                                                                                           processingSessionData.pcName);
                return statusBloqueFile;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene el estado actual del expediente asociado a un archivo en el módulo de Imaging.
        /// </summary>
        /// <param name="dbmCore">Referencia al administrador de base de datos del núcleo (Core).</param>
        /// <param name="sessionData">Datos de sesión de procesamiento que contienen la información del archivo.</param>
        /// <returns>El estado actual del expediente, o el valor por defecto si no se encuentra uno distinto.</returns>
        private short GetCurrentExpedienteStatus(ref DBCore.DBCoreDataBaseManager dbmCore, ProcessingSessionData sessionData)
        {
            short defaultStatus = (short)DBCore.EstadoEnum.Procesamiento_Llaves;

            var estadoList = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_FolderModulo(  sessionData.FileRow.fk_Expediente,
                                                                                                            sessionData.FileRow.fk_Folder,
                                                                                                            (byte)DesktopConfig.Modulo.Imaging
                                                                                                        );

            if (estadoList != null && estadoList.Count > 0)
            {
                var estado = estadoList[0].fk_Estado;
                if (estado != defaultStatus)
                    return estado;
            }

            return defaultStatus;
        }

        private void UnLockExpediente(ProcessingSessionData processingSessionData)
        {
            DBImaging.DBImagingDataBaseManager dbmImaging = null;

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging);
                dbmImaging.Connection_Open(Program.Config.usuario_log);

                dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(processingSessionData.FileRow.fk_Expediente, processingSessionData.FileRow.fk_Folder, processingSessionData.FileRow.fk_File);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }
        }


        /// <summary>
        /// Obtiene los expedientes pendientes para el proceso de cruce en línea desde la base de datos.
        /// </summary>
        private DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoDataTable GetExpedientesForCruceEnLinea()
        {
            DBEmbargos.DBEmbargosDataBaseManager dbmEmbargos = null;

            try
            {
                dbmEmbargos = new DBEmbargos.DBEmbargosDataBaseManager(Program.ConnectionStrings.Embargos);
                dbmEmbargos.Connection_Open(Program.Config.usuario_log);

                var dtCruceLineaProceso = dbmEmbargos.SchemaProcess.CTA_Cruce_En_Linea_Proceso.DBGet();

                return (dtCruceLineaProceso != null) ? dtCruceLineaProceso : new DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoDataTable();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbmEmbargos != null) dbmEmbargos.Connection_Close();
            }
        }

        private DBCore.SchemaImaging.CTA_FileRow GetExpedientesFileRow(ref DBCore.DBCoreDataBaseManager dbmCore, DBEmbargos.SchemaProcess.CTA_Cruce_En_Linea_ProcesoRow expedienteCruceEnLineaRow)
        {
            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core);
                dbmCore.Connection_Open(Program.Config.usuario_log);

                var expedienteFileRows = dbmCore.SchemaImaging.CTA_File.DBFindByfk_Expedientefk_Folderfk_File(expedienteCruceEnLineaRow.fk_Expediente, 
                                                                                                              (Slyg.Tools.SlygNullable<short>)expedienteCruceEnLineaRow.fk_Folder, 
                                                                                                              (Slyg.Tools.SlygNullable<short>)expedienteCruceEnLineaRow.fk_File);
                
                return expedienteFileRows?.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }


        #endregion
    }
}
