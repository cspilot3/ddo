using System;
using WebSantander.code;
using WebSantander.code.Grid;
using Slyg.Tools;
using System.Collections.Generic;
using DBIntegration.SchemaSantander;
using DBIntegration.SchemaRipley;
using System.Data;
using DBSecurity;

namespace WebSantander.site.consulta
{
    public partial class consultas : page_form
    {
        
        #region Propiedades

        public AutoListHelper<DBIntegration.SchemaConfig.CTA_ProyectoDataTable, DBIntegration.SchemaConfig.CTA_ProyectoEnum, DBIntegration.SchemaConfig.CTA_ProyectoRow> ProyectosList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaConfig.CTA_ProyectoDataTable, DBIntegration.SchemaConfig.CTA_ProyectoEnum, DBIntegration.SchemaConfig.CTA_ProyectoRow>>("ProyectosList"); }
            set { SetSessionValue("ProyectosList", value); }
        }

        public AutoListHelper<DBIntegration.SchemaConfig.TBL_Ente_CoactivoDataTable,DBIntegration.SchemaConfig.TBL_Ente_CoactivoEnum,DBIntegration.SchemaConfig.TBL_Ente_CoactivoRow> EntidadSolicitanteList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaConfig.TBL_Ente_CoactivoDataTable, DBIntegration.SchemaConfig.TBL_Ente_CoactivoEnum, DBIntegration.SchemaConfig.TBL_Ente_CoactivoRow>>("EntidadSolicitante"); }
            set { SetSessionValue("EntidadSolicitante",value);}
        }
                
        #endregion
        
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void Config_Page()
        {
            this.Master.Title = "Consulta";
            EditOptions.Add(Option.find);
            
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;
            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionString.Santander);
                
                //dbmIntegration.Connection_Open(this.MiharuSession.Usuario.id);
                dbmIntegration.Connection_Open(this.MiharuSession.Usuario.id);

                this.ProyectosList.Init(dbmIntegration.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidad(Program.idCliente), DBIntegration.SchemaConfig.CTA_ProyectoEnum.Nombre_Proyecto);
                this.EntidadSolicitanteList.Init(dbmIntegration.SchemaConfig.TBL_Ente_Coactivo.DBGet(null),DBIntegration.SchemaConfig.TBL_Ente_CoactivoEnum.Nombre_Ente);

                MainGrid.LastFilter = "";
                MainGrid.Initialize(new DataTable());
                MainGrid.DataSource = null;
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "Proyecto", Header = "Proyecto", Width = 200 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "FechaProceso", Header = "Fecha Proceso", Width = 100 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "Nit", Header = "Nit", Width = 100 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "TipoDocumental", Header = "Tipo Documental", Width = 300 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "LinkData", Header = "Datos", Width = 60 });
            }
            catch (Exception ex)
            {
                Program.TraceError(ex);
                ScriptHelper.Site.ShowAlert(this.Page, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
            }

            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionString.Security);

                DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
                //dbmSecurity.Connection_Open(this.MiharuSession.Usuario.id);
                dbmSecurity.Connection_Open(this.MiharuSession.Usuario.id);
                //dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.MiharuSession.Usuario.id, Program.idModulo, 201, this.MiharuSession.ClientIPAddress);
                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.MiharuSession.Usuario.id, Program.idModulo, 201, this.MiharuSession.ClientIPAddress);

            }
            catch (Exception ex)
            {
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
                        
        }
        
        public void Buscar(ScriptBuilder nHtml)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var ProyectosInput = ProyectosList.GetRowByText(GetValue("ProyectoInput",false));
                var fechaRecibidoInicialInput = GetValue<DateTime>("fechaRecibidoInicialInput", false);
                var fechaRecibidoFinalInput = GetValue<DateTime>("fechaRecibidoFinalInput", false);
                var fechaProcesoInicialInput = GetValue<DateTime>("fechaProcesoInicialInput", false);
                var fechaProcesoFinalInput = GetValue<DateTime>("fechaProcesoFinalInput", false);
                var EntidadSolicitanteInput = EntidadSolicitanteList.GetRowByText(GetValue("EntidadInput",false));
                var Id_Exp_Of = GetValue<string>("IdentificacionOficioInput", false);
                var EstadoClienteInput = GetValue<string>("EstadoClienteInput", false);
                var CedulaNitInput = GetValue<string>("CedulaNitInput", false);
                var min = DateTime.MinValue;
                var numero = 0;
                if (ProyectosInput == null)
                    throw new Exception("Debe seleccionar proyecto para continuar.");
                
                if (fechaRecibidoInicialInput > fechaRecibidoFinalInput)
                    throw new Exception("La fecha de recibo final no puede ser superior a la fecha de recibo inicial.");

                if (fechaProcesoInicialInput > fechaProcesoFinalInput)
                    throw new Exception("La fecha de proceso final no puede ser superior a la fecha de proceso inicial.");

                if ((fechaRecibidoInicialInput != min && fechaRecibidoFinalInput == min) || (fechaRecibidoInicialInput == min && fechaRecibidoFinalInput != min))
                    throw new Exception("Faltan datos por ingresar (Fecha Recibido).");

                if ((fechaProcesoInicialInput != min && fechaProcesoFinalInput == min) || (fechaProcesoInicialInput == min && fechaProcesoFinalInput != min))
                    throw new Exception("Faltan datos por ingresar (Fecha Proceso).");

                var fechaRecibo30 = fechaRecibidoInicialInput.AddDays(30);
                var fechaProceso30 = fechaProcesoInicialInput.AddDays(30);

                if (fechaRecibidoFinalInput > fechaRecibo30)
                    throw new Exception("La fecha de recibo final no puede ser superior a 30 Dias.");

                if (fechaProcesoFinalInput > fechaProceso30)
                    throw new Exception("La fecha de proceso final no puede ser superior a 30 Dias.");

                var fechaRebiboInicial = fechaRecibidoInicialInput.ToString("yyyyMMdd");
                var fechaReciboFinal = fechaRecibidoFinalInput.ToString("yyyyMMdd");
                var fechaProcesoInicial = fechaProcesoInicialInput.ToString("yyyyMMdd");
                var fechaProcesoFinal = fechaProcesoFinalInput.ToString("yyyyMMdd");
                string Id, Estado, Exp_of; short Entidad;

                if (fechaRecibidoInicialInput == min)
                    fechaRebiboInicial = "-1";

                if (fechaRecibidoFinalInput == min)
                    fechaReciboFinal = "-1";
                else
                    fechaReciboFinal = fechaRecibidoFinalInput.AddDays(1).AddSeconds(-1).ToString("yyyyMMdd");

                if (fechaProcesoInicialInput == min)
                    fechaProcesoInicial = "-1";

                if (fechaProcesoFinalInput == min)
                    fechaProcesoFinal = "-1";
                else
                    fechaProcesoFinal = fechaProcesoFinalInput.AddDays(1).AddSeconds(-1).ToString("yyyyMMdd");
               
                if (Id_Exp_Of != "" && int.TryParse(Id_Exp_Of,out numero) == false)
                    throw new Exception("El campo Identificación Expediente/Oficio debe ser numerico.");

                if (Id_Exp_Of == "")
                    Exp_of = "-1";
                else
                    Exp_of = Id_Exp_Of;

                if (EstadoClienteInput == "")
                    Estado = "-1";
                else
                    Estado = EstadoClienteInput;

                if (CedulaNitInput == "")
                    Id = "-1";
                else
                    Id = CedulaNitInput;

                if (EntidadSolicitanteInput == null)
                    Entidad = -1;
                else
                    Entidad = EntidadSolicitanteInput.Id_Ente_Coactivo;

                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionString.Santander);
                //dbmIntegration.Connection_Open(this.MiharuSession.Usuario.id);
                dbmIntegration.Connection_Open(this.MiharuSession.Usuario.id);

                var Data = dbmIntegration.SchemaSantander.Consulta_DataClient.DBExecute(
                nEntidad: Program.idCliente,
                nProyecto: ProyectosInput.id_Proyecto,
                nFecha_Recibido_Inicial: fechaRebiboInicial,
                nFecha_Recibido_Final: fechaReciboFinal,
                nFecha_Proceso_Inicial: fechaProcesoInicial,
                nFecha_Proceso_Final: fechaProcesoFinal,
                nEntidad_Solicitante: Entidad,
                nId_Exp_Of: Exp_of,
                nEstado: Estado,
                nId: Id
                );

                MainGrid.DataSource = Data;

            }
            catch (Exception ex)
            {
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        #endregion

        #region Funciones

        public string GetScriptVariables()
        {
            try
            {
                var URLVisor = (this.MiharuSession.Entidad.id == Program.idProcesador) ? Program.URLVisorImagenInterno : Program.URLVisorImagenExterno;
                var sb = new ScriptBuilder();
                sb.Append("Frm.ProyectosList = " + this.ProyectosList.GetJson() + ";");
                sb.Append("Frm.EntidadSolicitanteList = " + this.EntidadSolicitanteList.GetJson() + ";");
                sb.Append("Frm.UrlVisorImagen = '" + URLVisor + "';");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "alert('" + ex.Message + "');";
            }
        }

        #endregion
    }
}