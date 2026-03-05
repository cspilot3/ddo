using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miharu.Desktop.Library.Config;
using System.Data;
using System.ComponentModel;
using Imaging.Impuestos;
using DBIntegration;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
{
    public class ProcesaCargue_BcoBogota
    {
        public bool ProcesaLog_BcoBogota(ref DataTable data, string fileName, ref BackgroundWorker CargueBackgroundWorker, ImpuestosPlugin _Plugin, ref int IdCargue, DBIntegration.SchemaBcoBogota.TBL_Log_DataType Data_Log, bool Valida_Duplicados, string NombreProceso, string FechaProceso, ref DesktopConfig.TypeResult retorno_result)
        {
            var dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoBogotaConnectionString);
            bool retorno = false;
            CargueBackgroundWorker.ReportProgress(0);                                                                                                                                                                                                                                                
            short Entidad = _Plugin.Manager.ImagingGlobal.Entidad;
            int Proyecto = _Plugin.Manager.ImagingGlobal.Proyecto;
            int usuario = _Plugin.Manager.Sesion.Usuario.id;
            List<string> LtErrores = new List<string>();

            try
            {
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                object obj = new object();
                DataTable TablaErrores = new DataTable();

                lock ((this))
                {
                    const string tabla = "#VALIDA_CARGUE_PARCIAL_BCO_BOGOTA_IMPUESTOS";
                    BulkInsert.InsertDataTableReport(data, dbmIntegration, tabla);

                    TablaErrores = dbmIntegration.SchemaBcoBogota.PA_Procesa_Log_DINAMICO.DBExecute(Data_Log.fk_Tipo_log, Entidad, Proyecto, tabla, usuario, fileName, Data_Log.Fecha_Proceso);

                    if ((TablaErrores != null))
                    {
                        if ((TablaErrores.Rows.Count > 0))
                        {
                            dbmIntegration.Transaction_Rollback();
                            retorno_result.Result = false;
                            LtErrores.Add(TablaErrores.Rows[0]["ErrorMessage"].ToString());
                            retorno_result.Parameters = LtErrores;
                        }
                        else
                        {
                            retorno = true;
                            retorno_result.Result = true;
                        }
                    }

                    var UltimoCargueInvalido = dbmIntegration.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValidoFecha_Proceso(Entidad, (short)Proyecto, Data_Log.fk_Tipo_log, fileName, false, Data_Log.Fecha_Proceso);
                    if ((UltimoCargueInvalido.Rows.Count > 0))
                    {
                        IdCargue = Convert.ToInt32(UltimoCargueInvalido.Rows[0]["id_Cargue"]);
                    }
                }


            }
            catch (Exception ex)
            {
                retorno_result.Result = false;
                LtErrores.Add(ex.Message);
                retorno_result.Parameters = LtErrores;
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }

            return retorno;
        }

    }
}
