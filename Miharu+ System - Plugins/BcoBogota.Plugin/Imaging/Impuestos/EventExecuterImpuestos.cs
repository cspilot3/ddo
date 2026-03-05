using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miharu.Imaging.Library.Eventos;
using Miharu.Desktop.Library.Plugins;
using DBIntegration;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace Imaging.Impuestos
{
    public class EventExecuterImpuestos : EventExecuter, IEventExecuter
    {
        #region "Declaraciones"

        // ReSharper disable once NotAccessedField.Local
        private ImpuestosPlugin _Plugin;
        #endregion

        #region "Constructores"

        public EventExecuterImpuestos(ImpuestosPlugin nPlugin)
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
            
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;
            Int16 id_Campo = default(Int16);
            string FORMULARIO = "";

            try {
	            dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(_Plugin.BcoBogotaConnectionString);
	            dbmCore = new DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

	            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
	            dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);

	            var CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_Plugin.Manager.ImagingGlobal.Entidad, fk_documento);
	            var drowCampoFiltrado_aux = CamposDataTable.AsEnumerable().Where(x=>x.Nombre_Campo.ToUpper() == "FORMULARIO").ToList();

                if ((drowCampoFiltrado_aux.Count > 0))
                {
                    id_Campo = drowCampoFiltrado_aux.FirstOrDefault().id_Campo;

		            //recorre cada campo para encontrar valor de Formulario
		            foreach (var Item_loopVariable in campos) {
                        if (Item_loopVariable.id == id_Campo)
                        {
                            FORMULARIO = Item_loopVariable.Control.Value.ToString();
                            break;
			            }
		            }

                    if (!string.IsNullOrEmpty((FORMULARIO.Trim())))
                    {
                        var dtResult = dbmIntegration.SchemaBcoBogota.TBL_Formularios_Unicos.DBFindByFormulario((FORMULARIO.Trim()));
                        if (dtResult.Rows.Count > 0)
                        {
                            DesktopMessageBoxControl.DesktopMessageShow("Este Formulario esta Duplicado, ya existe un registro en Base de dato con este numero de Formulario '"+FORMULARIO.ToString()+"'¡¡¡¡", "Guardar Captura", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                            Result = false;
                        }
                        else
                        {
                            DBIntegration.SchemaBcoBogota.TBL_Formularios_UnicosType rowInsertar = new DBIntegration.SchemaBcoBogota.TBL_Formularios_UnicosType();
                            rowInsertar.Formulario = FORMULARIO;
                            dbmIntegration.SchemaBcoBogota.TBL_Formularios_Unicos.DBInsert(rowInsertar);
                            Result = true;
                        }
                    }
                    
	            }
            } catch (Exception ex) {
                DesktopMessageBoxControl.DesktopMessageShow("ValidarSavePrimeraCaptura", ref ex);
            } finally {
	            if ((dbmIntegration != null))
		            dbmIntegration.Connection_Close();
	            if ((dbmCore != null))
		            dbmCore.Connection_Close();
            }
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
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            Int16 id_Campo = default(Int16);
            string FORMULARIO = "";
            DateTime Fecha_Proceso = new DateTime();
            string Fecha_Recaudo = "";

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(_Plugin.BcoBogotaConnectionString);
                dbmCore = new DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);
                dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);

                var FechaProcesoDataTable = dbmImaging.SchemaProcess.PA_FechaProceso_Expediente.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, fk_Expediente);

                if (FechaProcesoDataTable.Rows.Count > 0)
                {
                    Fecha_Proceso = DateTime.ParseExact(FechaProcesoDataTable.Rows[0]["id_fecha_proceso"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                    if (((Convert.ToBoolean(FechaProcesoDataTable.Rows[0]["Cerrado"])) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 == false))
                    {
                        //Si la fecha de proceso se encuentra cerrada no permite modificacion en Busqueda - F11
                        DesktopMessageBoxControl.DesktopMessageShow("La Fecha de Proceso se encuentra cerrada, No es posible realizar modificación.", "Modificar Busqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                        Result = false;
                    }
                    else
                    {
                        var CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_Plugin.Manager.ImagingGlobal.Entidad, nCampo.fk_Documento);
                        var drowCampoFiltrado_aux = CamposDataTable.AsEnumerable().Where(x => x.Nombre_Campo.ToUpper() == "FORMULARIO").ToList();
                        var drowCampoFiltrado_aux_FechaRecaudo = CamposDataTable.AsEnumerable().Where(x => x.Nombre_Campo.ToUpper() == "FECHA RECAUDO").ToList();

                        if (drowCampoFiltrado_aux_FechaRecaudo.Count > 0)
                        {
                            short idCampoFechaRecaudo = drowCampoFiltrado_aux_FechaRecaudo.FirstOrDefault().id_Campo;
                            int fk_Documento = drowCampoFiltrado_aux_FechaRecaudo.FirstOrDefault().fk_Documento;

                            var dtFechaRecaudo = dbmIntegration.SchemaBcoBogota.PA_Obtiene_Valor_FileData.DBExecute(nCampo.fk_Expediente, fk_Documento, idCampoFechaRecaudo);

                            Fecha_Recaudo = dtFechaRecaudo.Rows.Count > 0 ? dtFechaRecaudo.Rows[0]["VALOR_FILE_DATA"].ToString() : "";
                        }

                        if ((drowCampoFiltrado_aux.Count > 0))
                        {
                            id_Campo = drowCampoFiltrado_aux.FirstOrDefault().id_Campo;

                            //recorre cada campo para encontrar valor de Formulario
                            if (nCampo.fk_Campo == id_Campo)
                            {
                                FORMULARIO = newValor_File_Data.ToString();

                                if (!string.IsNullOrEmpty((FORMULARIO.Trim())))
                                {
                                    var dtResult = dbmIntegration.SchemaBcoBogota.TBL_Formularios_Unicos.DBFindByFormulario((FORMULARIO.Trim()));
                                    if (dtResult.Rows.Count > 0)
                                    {
                                        DesktopMessageBoxControl.DesktopMessageShow("Este Formulario esta Duplicado, ya existe un registro en Base de dato con este numero de Formulario '" + FORMULARIO.ToString() + "'¡¡¡¡", "Guardar Formulario", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                                        Result = false;
                                    }
                                    else
                                    {
                                        //Borrar Formulario Errado
                                        var EncontradoUpdate = dbmIntegration.SchemaBcoBogota.TBL_Formularios_Unicos.DBFindByFormulario(nCampo.Valor_File_Data.ToString());

                                        if (EncontradoUpdate.Rows.Count > 0)
                                        {
                                            DBIntegration.SchemaBcoBogota.TBL_Formularios_UnicosType rowUpdate = new DBIntegration.SchemaBcoBogota.TBL_Formularios_UnicosType();
                                            rowUpdate.Formulario = newValor_File_Data.ToString();

                                            int idUpdate = Convert.ToInt32(EncontradoUpdate.Rows[0]["id_File_Formulario"].ToString());
                                            dbmIntegration.SchemaBcoBogota.TBL_Formularios_Unicos.DBUpdate(rowUpdate, idUpdate);
                                        }
                                        else
                                        {
                                            //Inserta nuevo Formulario
                                            DBIntegration.SchemaBcoBogota.TBL_Formularios_UnicosType rowInsertar = new DBIntegration.SchemaBcoBogota.TBL_Formularios_UnicosType();
                                            rowInsertar.Formulario = newValor_File_Data.ToString();
                                            dbmIntegration.SchemaBcoBogota.TBL_Formularios_Unicos.DBInsert(rowInsertar);
                                        }

                                        Result = true;
                                    }


                                }
                            }
                        }

                        //Devuelve Los cruces por Fecha de Proceso y Fecha Recuado (Capturada)
                        if (dbmIntegration.SchemaBcoBogota.PA_Devolver_Cruce.DBExecute(Fecha_Proceso.ToString("yyyy/MM/dd"), Fecha_Recaudo).Rows[0]["RESULT"].ToString() == "OK")
                        {
                            DesktopMessageBoxControl.DesktopMessageShow("Se ha realizado Devolución de Cruces por Fecha proceso " + Fecha_Proceso.ToString("yyyy/MM/dd") + " y Fecha Recaudo " + Fecha_Recaudo + "." + Environment.NewLine + "NOTA IMPORTANTE: Recuerde que si se ha realizado ya Preparar Data y Cruces, debe nuevamente hacer estos procesos!!!", "NOTA IMPORTANTE", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                        }
                        else
                            DesktopMessageBoxControl.DesktopMessageShow("Ha Ocurrido un error al momento de Devolver Cruces por Fecha Proceso y Fecha Recaudo!!", "Error", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                    }
                }
                

                
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("ValidarActualizarDatoBusqueda", ref ex);
                Result = false;
            }
            finally
            {
                if ((dbmIntegration != null))
                    dbmIntegration.Connection_Close();
                if ((dbmCore != null))
                    dbmCore.Connection_Close();
                if ((dbmIntegration != null))
                    dbmIntegration.Connection_Close();
            }

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

