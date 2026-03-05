using System;
using DBIntegration.SchemaRipley;
using DBIntegration.SchemaConfig;
using Miharu.Client.Browser.code;
using DBSecurity;

namespace Miharu.Client.Browser.site.reportes
{
    public partial class reporte_resumen_movimiento : page_form
    {

        #region Propiedades

       public AutoListHelper<DBIntegration.SchemaConfig.CTA_ProyectoDataTable, DBIntegration.SchemaConfig.CTA_ProyectoEnum, DBIntegration.SchemaConfig.CTA_ProyectoRow> ProyectosList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaConfig.CTA_ProyectoDataTable, DBIntegration.SchemaConfig.CTA_ProyectoEnum, DBIntegration.SchemaConfig.CTA_ProyectoRow>>("ProyectosList"); }
            set { SetSessionValue("ProyectosList", value); }
        }

        private AutoListHelper<CTA_Rol_Punto_UsuarioDataTable, CTA_Rol_Punto_UsuarioEnum, CTA_Rol_Punto_UsuarioRow> PuntosList
        {
            get { return GetSessionValue<AutoListHelper<CTA_Rol_Punto_UsuarioDataTable, CTA_Rol_Punto_UsuarioEnum, CTA_Rol_Punto_UsuarioRow>>("PuntosList"); }
            set { SetSessionValue("PuntosList", value); }
        }

        private AutoListHelper<DBIntegration.SchemaRipley.CTA_Documento_UsuarioDataTable, DBIntegration.SchemaRipley.CTA_Documento_UsuarioEnum, DBIntegration.SchemaRipley.CTA_Documento_UsuarioRow> DocumentosList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaRipley.CTA_Documento_UsuarioDataTable, DBIntegration.SchemaRipley.CTA_Documento_UsuarioEnum, DBIntegration.SchemaRipley.CTA_Documento_UsuarioRow>>("DocumentosList"); }
            set { SetSessionValue("DocumentosList", value); }
        }      

        private WebReport WebReport
        {
            get { return this.Session[consts.SessionWebReport] as WebReport; }
            set { this.Session[consts.SessionWebReport] = value; }
        }

        #endregion

        #region Eventos

        public override void Config_Page()
        {
            this.Master.Title = "Resumen de Movimiento";
            EditOptions.Add(Option.report);
            CargarCombos();

            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);

                DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.SessionManager.Usuario.id, Program.idModulo, 301, this.SessionManager.ClientIPAddress);
            }
            catch (Exception ex)
            {
                //Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

        }

        private void CargarCombos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmRipley = null;
            try
            {
                dbmRipley = new DBIntegration.DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                dbmRipley.Connection_Open(this.SessionManager.Usuario.id);

                this.ProyectosList.Init(dbmRipley.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidad(Program.idCliente), CTA_ProyectoEnum.Nombre_Proyecto);
                this.PuntosList.Init(dbmRipley.SchemaRipley.CTA_Rol_Punto_Usuario.DBFindByfk_Usuariofk_Entidad(this.SessionManager.Usuario.id, Program.idCliente), CTA_Rol_Punto_UsuarioEnum.Nombre_Punto);
               
            }
            catch (Exception ex)
            {
                Program.TraceError(ex);
                ScriptHelper.Site.ShowAlert(this.Page, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmRipley != null) dbmRipley.Connection_Close();
            }
        }
        
        public void CargarDocumentosPorProyecto(ScriptBuilder nHtml)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var proyectoInput = ProyectosList.GetRowByText(GetValue("proyectoInput"));
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                this.DocumentosList.Init(dbmIntegration.SchemaRipley.CTA_Documento_Usuario.DBFindByfk_Entidadfk_Proyectofk_UsuarioVer_RegistroVer_DataVer_ImagenDescargar(Program.idCliente, proyectoInput.id_Proyecto, this.SessionManager.Usuario.id, true, true, null, null), DBIntegration.SchemaRipley.CTA_Documento_UsuarioEnum.Nombre_Documento);
                nHtml.Append("Frm.DocumentosList = " + this.DocumentosList.GetJson() + ";");
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
        
        public void CargarReporte(ScriptBuilder nHtml)
        {
            try
            {
                var fechaInicialInput = GetValue<DateTime>("fechaInicialInput", false);
                var fechaFinalInput = GetValue<DateTime>("fechaFinalInput", false).AddDays(1).AddSeconds(-1);
                var proyectoInput = ProyectosList.GetRowByText(GetValue("proyectoInput"));
                var puntoInput = PuntosList.GetRowByText(GetValue("puntoInput"));
                var documentoInput = DocumentosList.GetRowByText(GetValue("documentoInput"));


                if (fechaInicialInput > fechaFinalInput)
                    throw new Exception("La fecha Final No puede ser superior a la fecha inicial");

                var fecha30 = fechaInicialInput.AddDays(30);

                if (fechaFinalInput > fecha30)
                    throw new Exception("La fecha Final No puede ser superior a 30 Dias");

                var fechaInicial = fechaInicialInput.ToString("yyyy/MM/dd");
                var fechaFinal = fechaFinalInput.ToString("yyyy/MM/dd");

                resumen_movimiento_report ReporteResumen = new resumen_movimiento_report();
                ReporteResumen.CadenaConexion = this.ConnectionStrings.Ripley;
                ReporteResumen.IdUsuario = this.SessionManager.Usuario.id;
                ReporteResumen.IdProyecto = (proyectoInput != null) ? proyectoInput.id_Proyecto : -1;
                ReporteResumen.IdPunto = (puntoInput != null) ? puntoInput.id_Punto : -1;
                ReporteResumen.IdDocumento = (documentoInput != null) ? documentoInput.id_Documento : -1;
                ReporteResumen.FechaInicio = fechaInicialInput;
                ReporteResumen.FechaFinal = fechaFinalInput;
                ReporteResumen.TieneExportacion = documentoInput.Descargar;
                this.WebReport = ReporteResumen;

                //ordenes_pago_vrs_entrada_almacen_report ordenes_pago_vrs_entrada_almacen_report = new ordenes_pago_vrs_entrada_almacen_report();
                //ordenes_pago_vrs_entrada_almacen_report.Parameter_EntidadId = IdEntidad;
                //ordenes_pago_vrs_entrada_almacen_report.Parameter_ProyectoId = IdProyecto;
                //ordenes_pago_vrs_entrada_almacen_report.Parameter_ObraId = Obra.id_obra;
                //this.WebReport = ordenes_pago_vrs_entrada_almacen_report;
            }
            catch (Exception ex) { TraceError(nHtml, ex); }
        }

        #endregion

        #region Funciones

        public string GetScriptVariables()
        {
            try
            {
                var sb = new ScriptBuilder();
                sb.Append("Frm.ProyectosList = " + this.ProyectosList.GetJson() + ";");
                sb.Append("Frm.PuntosList = " + this.PuntosList.GetJson() + ";");
                sb.Append("Frm.DocumentosList = " + this.DocumentosList.GetJson() + ";");
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