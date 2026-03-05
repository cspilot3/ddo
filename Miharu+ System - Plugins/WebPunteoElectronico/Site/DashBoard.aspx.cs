using System;
using Miharu.Security.Library.Session;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site
{
    public enum ServerMode
    {
        Ninguno,
        Estadisticos_Digitalizacion,
        Estadisticos_Cruce,
        Reportes_Digitalizacion,
        Reportes_Cruce,
        Reportes_Firmas,
        Estadistico_Ajustes,
        Estadistico_Consolidados,
        SMS,
    }

    public partial class DashBoard : FormBase
    {
        #region Declaraciones

        public static int PQRConsecutivo = 1;

        #endregion

        #region Propiedades
        
        public ServerMode ServerMode
        {
            get { return (ServerMode)this.MiharuSession.Parameter["ServerMode"]; }
            set { this.MiharuSession.Parameter["ServerMode"] = value; }
        }

        public string ServerModeString
        {
            get
            {
                var Value = this.MiharuSession.Parameter["ServerMode"];
                return Enum.GetName(typeof(ServerMode), Value ?? ServerMode.Ninguno);
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
            else
                Load_Data();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.ShowTitle = false;
            Master.Title = "";

            #region Consultas

            this.ConsultasDashBoardItem.OnItemClick += ConsultasDashBoardItem_OnItemClick;

            #endregion

            #region Ajustes

            this.SoportesSobrantesDashBoardItem.OnItemClick += (SoportesSobrantesDashBoardItem_OnItemClick);
            this.RegistrosSobrantesDashBoardItem.OnItemClick += (RegistrosSobrantesDashBoardItem_OnItemClick);
            this.DiferenciasValorDashBoardItem.OnItemClick += (DiferenciasValorDashBoardItem_OnItemClick);
            this.DiferenciasProductoDashBoardItem.OnItemClick += (DiferenciasProductoDashBoardItem_OnItemClick);
            this.DiferenciasMedioPagoDashBoardItem.OnItemClick += (DiferenciasMedioPagoDashBoardItem_OnItemClick);
            this.AjustesRealizadosDashBoardItem.OnItemClick += (AjustesRealizadosDashBoardItem_OnItemClick);
            
            #endregion

            #region Servicio

            //this.ServicioDashBoardItem.OnItemClick += new WebPunteoElectronico.Controls.DashBoardItem.ItemClick(ServicioDashBoardItem_OnItemClick);
            this.SMSDashBoardItemLocal.OnItemClick += (SMSDashBoardItemLocal_OnItemClick);
            this.SMSReporteDashBoardItemLocal.OnItemClick += (SMSReporteDashBoardItemLocal_OnItemClick);

            #endregion

            #region Reportes

            this.Report_Contenedores_OficDashBoardItem.OnItemClick += (Report_Contenedores_OficDashBoardItem_OnItemClick);
            this.Report_Dif_Doc_Fisc_Tul_ContDashBoardItem.OnItemClick += (Report_Dif_Doc_Fisc_Tul_ContDashBoardItem_OnItemClick);
            this.Report_Doc_Remit_Sin_OrdenDashBoardItem.OnItemClick += (Report_Doc_Remit_Sin_OrdenDashBoardItem_OnItemClick);
            this.Report_Soport_Tx_inconsistenciasDashBoardItem.OnItemClick += (Report_Soport_Tx_inconsistenciasDashBoardItem_OnItemClick);
            this.Report_Noved_Valid_FormDashBoardItem.OnItemClick += (Report_Noved_Valid_FormDashBoardItem_OnItemClick);
            this.Report_Consolid_Nov_Valid_FormDashBoardItem.OnItemClick += (Report_Consolid_Nov_Valid_FormDashBoardItem_OnItemClick);
            this.Report_Ofic_Pendi_Envio_MovDashBoardItem.OnItemClick += (Report_Ofic_Pendi_Envio_MovDashBoardItem_OnItemClick);
            this.Report_Pendiente_ProcesoDashBoardItem.OnItemClick += (Report_Pendiente_ProcesoDashBoardItem_OnItemClick);
            this.Report_Pendiente_CierreDashBoardItem.OnItemClick += (Report_Pendiente_CierreDashBoardItem_OnItemClick);
            this.Report_Consolid_MedioPagoDashBoardItem.OnItemClick += (Report_Consolid_MedioPagoDashBoardItem_OnItemClick);
            this.Report_Tx_Desmat_Soporte_FisicoDashBoardItem.OnItemClick += (Report_Tx_Desmat_Soporte_FisicoDashBoardItem_OnItemClick);
            this.Report_SoportesSobrantesDashBoardItem.OnItemClick += (Report_SoportesSobrantesDashBoardItem_OnItemClick);
            this.Report_RegistrosSobrantesDashBoardItem.OnItemClick += (Report_RegistrosSobrantesDashBoardItem_OnItemClick);
            this.Report_ResultadoCruceAutomaticoDashBoardItem.OnItemClick +=(Report_ResultadoCruceAutomaticoDashBoardItem_OnItemClick);
            this.Report_Tx_No_IdentificadasDashBoardItem.OnItemClick +=(Report_Tx_No_IdentificadasDashBoardItem_OnItemClick);
            this.Report_Tx_CruceExitosoDashBoardItem.OnItemClick +=(Report_Tx_CruceExitosoDashBoardItem_OnItemClick);
            this.Report_DocNoIdentificadosDashBoardItem.OnItemClick +=(Report_DocNoIdentificadosDashBoardItem_OnItemClick);
            this.Report_CierreDashBoardItem.OnItemClick +=(Report_CierreDashBoardItem_OnItemClick);
            this.Report_Consolid_Cruce_AutomDashBoardItem.OnItemClick +=(Report_Consolid_Cruce_AutomDashBoardItem_OnItemClick);
            this.Report_Tx357DashBoardItem.OnItemClick +=(Report_Tx357DashBoardItem_OnItemClick);
            this.Report_Tx_MensualesDashBoardItem.OnItemClick +=(Report_Tx_MensualesDashBoardItem_OnItemClick);
            this.Report_TxExcluidasCruceDashBoardItem.OnItemClick +=(Report_TxExcluidasCruceDashBoardItem_OnItemClick);
            this.Report_TxReversadasLogDashBoardItem.OnItemClick +=(Report_TxReversadasLogDashBoardItem_OnItemClick);
            this.Report_Devolucion_Canje_RecibidoDashBoardItem.OnItemClick +=(Report_Devolucion_Canje_RecibidoDashBoardItem_OnItemClick);
            
            #endregion

            this.OficinasTipoBDashBoardItem.OnItemClick += (OficinasTipoBDashBoardItem_OnItemClick);
            this.ReporteGerencialDashBoardItem.OnItemClick += (ReporteGerencialDashBoardItem_OnItemClick);
            this.AcercaDeDashBoardItem.OnItemClick += (AcercaDe_Item_OnItemClick);

            #region Estadisticos

            this.Estad_SoportesSobrantesDashBoardItem.OnItemClick +=(Estad_SoportesSobrantes_OnItemClick);
            this.Estad_RegistrosSobrantesDashBoardItem.OnItemClick +=(Estad_RegistrosSobrantesDashBoardItem_OnItemClick);
            this.Estad_ResulProcesCruceAutoDashBoardItem.OnItemClick +=(Estad_ResulProcesCruceAutoDashBoardItem_OnItemClick);
            this.Estad_TxCruceExitosoDashBoardItem.OnItemClick +=(Estad_TxCruceExitosoDashBoardItem_OnItemClick);
            this.Estad_NovedadValidFormDashBoardItem.OnItemClick +=(Estad_NovedadValidFormDashBoardItem_OnItemClick);
            this.Estad_ConsoliNovedadValidFormDashBoardItem.OnItemClick +=(Estad_ConsoliNovedadValidFormDashBoardItem_OnItemClick);
            this.Estad_ConsolidCruceAutomaticoDashBoardItem.OnItemClick +=(Estad_ConsolidCruceAutomaticoDashBoardItem_OnItemClick);
            this.Estad_TxNoIdentificadasDashBoardItem.OnItemClick +=(Estad_TxNoIdentificadasDashBoardItem_OnItemClick);
            this.Estad_DocNoIdentifiDashBoardItem.OnItemClick +=(Estad_DocNoIdentifiDashBoardItem_OnItemClick);
            this.Estad_TxReversadasDashBoardItem.OnItemClick +=(Estad_TxReversadasDashBoardItem_OnItemClick);
            this.Estad_ContenedorOficinaDashBoardItem.OnItemClick +=(Estad_ContenedorOficinaDashBoardItem_OnItemClick);
            this.Estad_DifDocTulaContenedorDashBoardItem.OnItemClick +=(Estad_DifDocTulaContenedorDashBoardItem_OnItemClick);
            this.Estad_DocSinOrdenDefinidoDashBoardItem.OnItemClick +=(Estad_DocSinOrdenDefinidoDashBoardItem_OnItemClick);
            this.Estad_InconsistenciasAnexosDashBoardItem.OnItemClick +=(Estad_InconsistenciasAnexosDashBoardItem_OnItemClick);
            this.Estad_TxDesmatConSoporteDashBoardItem.OnItemClick +=(Estad_TxDesmatConSoporteDashBoardItem_OnItemClick);
            this.Estad_TxProcesadasMensualmenteDashBoardItem.OnItemClick +=(Estad_TxProcesadasMensualmenteDashBoardItem_OnItemClick);
            this.Estad_PendientTransmisionDashBoardItem.OnItemClick +=(Estad_PendientTransmisionDashBoardItem_OnItemClick);

            this.Estad_OficinaPendienteProcesoDashBoardItem.OnItemClick +=(Estad_OficinaPendienteProcesoDashBoardItem_OnItemClick);
            this.Estad_TxExcluidaDashBoardItem.OnItemClick +=(Estad_TxExcluidaDashBoardItem_OnItemClick);
            this.Estad_OficinaPendienteCierreDashBoardItem.OnItemClick +=(Estad_OficinaPendienteCierreDashBoardItem_OnItemClick);
            this.Estad_Tx357DashBoardItem.OnItemClick +=(Estad_Tx357DashBoardItem_OnItemClick);

            this.Estad_AjusteSoporteSobranteDashBoardItem.OnItemClick +=(Estad_AjusteSoporteSobranteDashBoardItem_OnItemClick);
            this.Estad_AjusteRegistroSobranteDashBoardItem.OnItemClick +=(Estad_AjusteRegistroSobranteDashBoardItem_OnItemClick);
            this.Estad_AjusteResultadoCruceDashBoardItem.OnItemClick +=(Estad_AjusteResultadoCruceDashBoardItem_OnItemClick);

            this.Estad_ConsolidTotalDashBoardItem.OnItemClick +=(Estad_ConsolidTotalDashBoardItem_OnItemClick);

            #endregion

            #region Firmas

            this.RF01_Gestion_Tarjetas_Firmas_FaltantesDashBoardItem.OnItemClick += (RF01_Gestion_Tarjetas_Firmas_FaltantesDashBoardItem_OnItemClick);
            this.RF02_Control_Envio_OficinaDashBoardItem.OnItemClick += (RF02_Control_Envio_OficinaDashBoardItem_OnItemClick);
            this.RF03_Soportes_SobrantesDashBoardItem.OnItemClick += (RF03_Soportes_SobrantesDashBoardItem_OnItemClick);
            this.RF04_Cruce_ExitosoDashBoardItem.OnItemClick += (RF04_Cruce_ExitosoDashBoardItem_OnItemClick);
            this.RF05_Transacciones_Excluidas_CruceDashBoardItem.OnItemClick += (RF05_Transacciones_Excluidas_CruceDashBoardItem_OnItemClick);
            this.RF06_Transacciones_MensualesDashBoardItem.OnItemClick += (RF06_Transacciones_MensualesDashBoardItem_OnItemClick);
            this.RF07_Tarjetas_Firmas_RechazadasDashBoardItem.OnItemClick += (RF07_Tarjetas_Firmas_RechazadasDashBoardItem_OnItemClick);
            this.RF08_Resultado_CierreDashBoardItem.OnItemClick += (RF08_Resultado_CierreDashBoardItem_OnItemClick);
            this.RF09_Documentos_No_IdentificadosDashBoardItem.OnItemClick += (RF09_Documentos_No_IdentificadosDashBoardItem_OnItemClick);

            this.ConsultasFirmasDashBoardItem.OnItemClick += (ConsultasFirmasDashBoardItem_OnItemClick);
            this.AjustesFirmasFaltantesDashBoardItem.OnItemClick += (AjustesFirmasFaltantesDashBoardItem_OnItemClick);

            #endregion
        }

        #region Informes

        void ConsultasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Consulta.Consultas).FullName, "Consultas", Navigation.Site.Consulta.Consultas, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }
        
        #endregion

        #region Ajustes


        //Soportes Sobrantes ------------

        void SoportesSobrantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Ajustes.AjusteSoportesSobrantes).FullName, "SoportesSobrantes", Navigation.Site.Ajustes.AjusteSoportesSobrantes, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        ////Registros Sobrantes

        void RegistrosSobrantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Ajustes.AjusteRegistroSobrante).FullName, "RegistrosSobrantes", Navigation.Site.Ajustes.AjusteRegistroSobrante, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        //Ajustes Realizados

        void AjustesRealizadosDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Ninguno;
            LaunchReportA(new Reportes.Reporte_AjusteRealizado.Reporte_AjustesRealizados());
        }

        //Diferencias Valor--------

        void DiferenciasValorDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Ajustes.AjusteDiferenciaValor).FullName, "Diferencias Valor", Navigation.Site.Ajustes.AjusteDiferenciaValor, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        //Diferencias Producto--------

        void DiferenciasProductoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Ajustes.AjusteDiferenciaProducto).FullName, "Diferencias Producto", Navigation.Site.Ajustes.AjusteDiferenciaProducto, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        //Diferencias Medio de Pago--------

        void DiferenciasMedioPagoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Ajustes.AjusteDiferenciaMedioPago).FullName, "Diferencias Medio Pago", Navigation.Site.Ajustes.AjusteDiferenciaMedioPago, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }
        
        #endregion

        #region Servicio

        //Servicio al cliente ------------

        void SMSDashBoardItemLocal_OnItemClick(Controls.DashBoardItem nItem)
        {
            DBSecurity.DBSecurityDataBaseManager dbm_Security = null;
            try
            {
                var integration = new Sms.Registro.Integration.Library.WebService.IntegrationWebService(Program.SmsRegistroIntegrationWebService);
                integration.CreateSecureChannel();

                dbm_Security = new DBSecurity.DBSecurityDataBaseManager(ConnectionString.Security);
                dbm_Security.Connection_Open(MiharuSession.Usuario.id);

                var usuarioData = dbm_Security.SchemaSecurity.TBL_Usuario.DBGet(MiharuSession.Usuario.id);
                if (usuarioData.Count == 0) throw new Exception("Error al consultar el usuario " + MiharuSession.Usuario.id);

                integration.CreateSession(Program.SmsRegistroIntegrationEntidad, Program.SmsRegistroIntegrationCliente, Program.SmsRegistroIntegrationCiudad, MiharuSession.Usuario.Login, MiharuSession.Usuario.Nombres, MiharuSession.Usuario.Apellidos, usuarioData[0].Telefono_Usuario, "", usuarioData[0].Direccion_Usuario, usuarioData[0].Email_Usuario, "");

                this.Master.ShowWindow(Program.SmsRegistroIntegrationURL + "?token=" + integration.Token + "&PQR=" + PQRConsecutivo++, "Registro", 1024, 650);
                //this.Master.ScriptBag.Append("window.open('" + Program.SmsRegistroIntegrationURL + "?token=" + integration.Token + "', 'Registro', '');");

            }
            catch (Exception ex)
            {
                this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbm_Security != null) dbm_Security.Connection_Close();
            }
        }

        void SMSReporteDashBoardItemLocal_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.SMS;
            LaunchReportG(new Reportes.ConsultaCasosSMS.Reporte_ConsultaCasosSMS());
        }

        #endregion

        #region Estadisticos

        #region Digitalizacion

        void Estad_NovedadValidFormDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphA(new Estadisticos.R014_Novedad_en_validacion_de_Forma.R014_Novedades_en_validaciones_de_Forma());
        }

        void Estad_ContenedorOficinaDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphA(new Estadisticos.R001_Contenedor_Oficina.R001_Contenedores_Oficinas());
        }

        void Estad_DifDocTulaContenedorDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphB(new Estadisticos.R002_Diferencia_entre_Doc_Fisicos_en_Tula_y_en_Contenedor.R002_Diferencias_entre_Doc_Fisicos_en_Tula_y_en_Contenedores());
        }

        void Estad_DocSinOrdenDefinidoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphB(new Estadisticos.R003_Documento_Remitido_Sin_Ordenamiento_Definido.R003_Documentos_Remitidos_Sin_Ordenamiento_Definido());
        }

        void Estad_InconsistenciasAnexosDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphA(new Estadisticos.R004_Soporte_Tx_Con_Inconsistencia_y_o_Anexo_Faltante.R004_Soportes_Tx_Con_Inconsistencias_y_o_Anexos_Faltantes());
        }

        void Estad_PendientTransmisionDashBoardItem_OnItemClick(Controls.DashBoardItem nIten)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphB(new Estadisticos.R006_Oficina_Sin_Transmision_No_Envio_Movimiento.R006_Oficinas_Sin_Transmision_No_Envio_Movimiento());
        }

        void Estad_TxDesmatConSoporteDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphA(new Estadisticos.R009_Transaccion_Desmaterializadas_Con_Soporte_Fisico.R009_Transacciones_Desmaterializadas_Con_Soporte_Fisico());
        }

        void Estad_OficinaPendienteProcesoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Digitalizacion;
            LaunchGraphB(new Estadisticos.R007_Oficina_Pendiente_Proceso.R007_Oficinas_Pendiente_Proceso());
        }

        #endregion

        #region Cruce

        void Estad_SoportesSobrantes_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphA(new Estadisticos.R011_Soporte_Sobrante.R011_Soportes_Sobrantes());
        }

        void Estad_RegistrosSobrantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphA(new Estadisticos.R012_Registros_Sobrantes.R012_Registro_Sobrante());
        }

        void Estad_ResulProcesCruceAutoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphA(new Estadisticos.R013_Resultado_Proceso_Cruce_Automatico.R013_Resultados_Proceso_Cruce_Automatico());
        }

        void Estad_TxCruceExitosoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphA(new Estadisticos.R020_Tx_Con_Cruce_Exitoso.R020_Tx_Con_Cruces_Exitosos());
        }


        void Estad_TxNoIdentificadasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphA(new Estadisticos.R019_Tx_No_Identificada.R019_Tx_No_Identificadas());
        }

        void Estad_DocNoIdentifiDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphA(new Estadisticos.R024_Doc_No_Identificado.R024_Doc_No_Identificados());
        }

        void Estad_TxReversadasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphB(new Estadisticos.R023_Transaccion_Reversada_Archivo_Detalle.R023_Transacciones_Reversadas_Archivo_Detalle());
        }
        void Estad_Tx357DashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphB(new Estadisticos.R028_Transaccion_357.R028_Transacciones_357());
        }

        void Estad_OficinaPendienteCierreDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphB(new Estadisticos.R025_Oficina_Pendiente_Cierre.R025_Oficinas_Pendientes_Cierres());
        }

        void Estad_TxExcluidaDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadisticos_Cruce;
            LaunchGraphB(new Estadisticos.R022_Transaccion_Excluida_Archivo_Detalle.R022_Transacciones_Excluidas_Archivo_Detalle());
        }

        #endregion

        #region Ajustes

        void Estad_AjusteResultadoCruceDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Ajustes;
            LaunchGraphA(new Estadisticos.R013_Ajuste_Resultado_Proceso_Cruce_Automatico.R013_Ajuste_Resultados_Proceso_Cruce_Automaticos());
        }

        void Estad_AjusteRegistroSobranteDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Ajustes;
            LaunchGraphA(new Estadisticos.R012_Ajuste_Registro_Sobrante.R012_Ajuste_Registros_Sobrantes());
        }

        void Estad_AjusteSoporteSobranteDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Ajustes;
            LaunchGraphA(new Estadisticos.R011_Ajuste_Soporte_Sobrante.R011_Ajuste_Soportes_Sobrantes());
        }

        #endregion

        #region Consolidados

        void Estad_ConsolidTotalDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Consolidados;
            LaunchGraphA(new Estadisticos.R000_Consolidado_Total.R000_Consolidados_Totales());
        }

        void Estad_ConsoliNovedadValidFormDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Consolidados;
            LaunchGraphA(new Estadisticos.R016_Consolidado_Novedad_De_Forma.R016_Consolidado_Novedades_De_Forma());
        }

        void Estad_TxProcesadasMensualmenteDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Consolidados;
            LaunchGraphA(new Estadisticos.R000_Transaccion_Procesada_Mensualmente_Tipo.R000_Transacciones_Procesadas_Mensualmente_Tipos());
        }

        void Estad_ConsolidCruceAutomaticoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Estadistico_Consolidados;
            LaunchGraphA(new Estadisticos.R018_Consolidado_Cruce_Automatico.R018_Consolidado_Cruces_Automaticos());
        }

        #endregion

        #endregion

        #region Reportes

        #region Digitalizacion

        void Report_Contenedores_OficDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportA(new Reportes.R001_ContenedoresOficina.R001_ContenedoresOficinas());
        }

        void Report_Dif_Doc_Fisc_Tul_ContDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportB(new Reportes.R002_DiferenciaDocEnviadoReportado.R002_DiferenciasDocsEnviadosReportados());
        }

        void Report_Doc_Remit_Sin_OrdenDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportB(new Reportes.R003_DocRemitidoSinOrdenamiento.R003_DocsRemitidosSinOrdenamiento());
        }

        void Report_Soport_Tx_inconsistenciasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportA(new Reportes.R004_TranNoOriginaloIlegible.R004_TransNoOriginaloIlegible());
        }

        void Report_Noved_Valid_FormDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportA(new Reportes.R014_NovedadValidacionForma.R014_NovedadesValidacionesForma());
        }

        void Report_Consolid_Nov_Valid_FormDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportB(new Reportes.R016_ConsolidadoNovedadDeForma.R016_ConsolidadosNovedadesDeForma());
        }

        void Report_Ofic_Pendi_Envio_MovDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportB(new Reportes.R006_OficinaPendienteTransmision.R006_OficinasPendienteTransmision());
        }

        void Report_Pendiente_ProcesoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportB(new Reportes.R007_OficinaPendienteProceso.R007_OficinasPendienteProceso());
        }

        void Report_Pendiente_CierreDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportC(new Reportes.R025_Oficina_Pendiente_Cierre.R025_Oficinas_Pendientes_Cierre());
        }

        void Report_Tx_Desmat_Soporte_FisicoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Digitalizacion;
            LaunchReportA(new Reportes.R009_OperacionSinSoporteDefinido.R009_Txs_Desmats_ConSoportes());
        }

        #endregion

        #region Cruce

        void Report_TxReversadasLogDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportB(new Reportes.R023_Tx_Reversada_Log.R023_Txs_Reversadas_Log());
        }

        void Report_TxExcluidasCruceDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportB(new Reportes.R022_Tx_Excluida_Cruce.R022_Txs_Excluidas_Cruce());
        }

        void Report_Tx_MensualesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportF(new Reportes.R008_Tx_Mensual.R008_Txs_Mensuales());
        }

        void Report_Tx357DashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportB(new Reportes.R028_Transacciones_357.R028_Transaccion_357());
        }

        void Report_Consolid_Cruce_AutomDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R018_ConsolidadoCruceAutomatico.R018_ConsolidadosCrucesAutomaticos());
        }

        void Report_CierreDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R021_Resultado_Cierre.R021_Resultados_Cierres());
        }

        void Report_DocNoIdentificadosDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R024_Doc_No_Identificado.R024_Docs_No_Identificados());
        }

        void Report_Tx_CruceExitosoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R020_Tx_Con_Cruce_Exitoso.R020_Txs_Con_Cruces_Exitosos());
        }

        void Report_Tx_No_IdentificadasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R019_Tx_No_Identificada.R019_Txs_No_Identificadas());
        }

        void Report_ResultadoCruceAutomaticoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R013_ResultadoCruceAutomatico.R013_ResultadosCrucesAutomaticos());
        }

        void Report_RegistrosSobrantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportA(new Reportes.R012_Registro_Sobrante.R012_RegistrosSobrantes());
        }

        void Report_SoportesSobrantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            //this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            //LaunchReportA(new Reportes.R011_Soporte_Sobrante.R011_Soporte_Sobrantes());

            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.R011_Soporte_Sobrante_Multiple.R011_Soportes_Sobrante_Multiple).FullName, "SoportesSobrantes", Navigation.Site.Reportes.R011_Soporte_Sobrante_Multiple.R011_Soportes_Sobrante_Multiple, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        void Report_Consolid_MedioPagoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportE(new Reportes.R005_Consolidado_Medio_Pago_Maestro.R005_Consolidado_Medios_Pago_Maestro());
        }

        void Report_Devolucion_Canje_RecibidoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Cruce;
            LaunchReportH(new Reportes.R029_Devolucion_Canje_Recibido.R029_Devolucion_Canje_Recibidos());
        }

        #endregion

        #region Firmas

        void RF01_Gestion_Tarjetas_Firmas_FaltantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportI(new Reportes.RF01_Gestion_Tarjetas_Firmas_Faltantes.RF01_Gestion_Tarjetas_Firmas_Faltante());
        }
        void RF02_Control_Envio_OficinaDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF02_Control_Envio_Oficinas.RF02_Control_Envio_Oficina());
        }
        void RF03_Soportes_SobrantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF03_Soportes_Sobrantes.RF03_Soporte_Sobrante());
        }
        void RF04_Cruce_ExitosoDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF04_Cruce_Exitoso.RF04_Cruce_Exitosos());
        }
        void RF05_Transacciones_Excluidas_CruceDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF05_Transacciones_Excluidas_Cruce.RF05_Transacciones_Excluida_Cruce());
        }
        void RF06_Transacciones_MensualesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportJ(new Reportes.RF06_Transacciones_Mensuales.RF06_Transacciones_Mensual());
        }
        void RF07_Tarjetas_Firmas_RechazadasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF07_Tarjetas_Firmas_Rechazadas.RF07_Tarjetas_Firmas_Rechazada());
        }
        void RF08_Resultado_CierreDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF08_Resultado_Cierre.RF08_Resultado_Cierres());
        }
        void RF09_Documentos_No_IdentificadosDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.ServerMode = WebPunteoElectronico.Site.ServerMode.Reportes_Firmas;
            LaunchReportA(new Reportes.RF09_Documentos_No_Identificados.RF09_Documentos_No_Identificado());
        }

        void ConsultasFirmasDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.ConsultaFirmas.Consulta_Firmas).FullName, "ConsultaFirmas", Navigation.Site.ConsultaFirmas.Consulta_Firmas, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        void AjustesFirmasFaltantesDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Ajustes.AjusteTarjetaFaltante).FullName, "Tarjetas Faltantes", Navigation.Site.Ajustes.AjusteTarjetaFaltante, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        #endregion

        #endregion

        void AcercaDe_Item_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.About).FullName, "About", Navigation.Site.About, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        void ReporteGerencialDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Estadisticos.WebGraphManagerViewer).FullName, "Reporte Gerencial", Navigation.Site.Estadisticos.WebGraphManagerViewer, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        void OficinasTipoBDashBoardItem_OnItemClick(Controls.DashBoardItem nItem)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.OficinasB.OficinaB).FullName, "OficinaB", Navigation.Site.OficinasB.OficinaB, "0");
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            // Asegurar la navegación
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.DashBoard).FullName, "DashBoard", Navigation.Site.DashBoard, "0");

            //Configurar permisos de acceso
            #region Consultas

            this.ConsultasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Consultas);

            #endregion

            #region Ajustes

            this.AjustesDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes);
            this.SoportesSobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajuste_Soportes_Sobrantes);
            this.RegistrosSobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Registros_Sobrantes);
            this.DiferenciasProductoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Diferencia_Producto);
            this.DiferenciasValorDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Diferencia_Valor);
            this.DiferenciasMedioPagoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Diferencia_Medios_Pago);
            
            #endregion

            #region Servicio Cliente

            this.ServicioClienteDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Servicio_Cliente);

            this.SMSDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Servicio_Cliente_Registrar_PQR);
            this.SMSReporteDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Servicio_Cliente_Registrar_PQR);

            #endregion

            #region Reportes

            this.ReportesDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte);

            this.Report_DigDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Digitalización);
            this.Report_Contenedores_OficDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Contenedores_Oficinas);
            this.Report_Dif_Doc_Fisc_Tul_ContDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Diferencias_Documentos);
            this.Report_Doc_Remit_Sin_OrdenDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Documentos_Sin_Ordenamiento);
            this.Report_Soport_Tx_inconsistenciasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Transacciones_Sin_Anexos);
            this.Report_Ofic_Pendi_Envio_MovDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Oficinas_Pendientes_Transmision);
            this.Report_Pendiente_ProcesoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Oficinas_Pendientes_Proceso);
            this.Report_Tx_Desmat_Soporte_FisicoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Tx_Desmat_con_Soporte_Fisico);
            this.Report_Noved_Valid_FormDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Novedades_en_validaciones);
            this.Report_Consolid_Nov_Valid_FormDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Consolidado_Novedades_Validaciones);
            this.Report_Pendiente_CierreDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Dig_Oficinas_pendientes_de_cierre);

            this.CruceDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce);

            this.Report_Consolid_MedioPagoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Consolidado_medios_de_pago);
            this.Report_Tx_MensualesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Transacciones_Mensuales);
            this.Report_SoportesSobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Soportes_Sobrantes);
            this.Report_RegistrosSobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Registros_sobrantes);
            this.Report_ResultadoCruceAutomaticoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Resultado_cruce_automático);
            this.Report_Consolid_Cruce_AutomDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Consolidado_Cruce_Automático);
            this.Report_Tx_No_IdentificadasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Transacciones_no_Identificadas);
            this.Report_Tx_CruceExitosoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Transacciones_con_cruce_exitoso);
            this.Report_CierreDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Resultado_del_cierre);
            this.Report_TxExcluidasCruceDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Transacciones_excluidas_del_cruce);
            this.Report_TxReversadasLogDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Transacciones_reversadas_en_log);
            this.Report_DocNoIdentificadosDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Documentos_No_Identificadas);
            this.Report_Tx357DashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Transacciones_357);
            this.Report_Devolucion_Canje_RecibidoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Devolucion_Canje_Recibido);

            this.RF01_Gestion_Tarjetas_Firmas_FaltantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Cruce_Soportes_Sobrantes);

            #endregion

            #region Gerencial

            this.ReporteGerencialDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Gerencial);

            #endregion

            #region Estadisticos
                                                                                                                                             
            this.EstadisticosDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Estadisiticos);

            this.DigitalizacionDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digitalizacion);

            this.Estad_ContenedorOficinaDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Contenedores_Oficinas);
            this.Estad_DifDocTulaContenedorDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Diferencias_Documentos);
            this.Estad_DocSinOrdenDefinidoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Documentos_Sin_Ordenamiento);
            this.Estad_InconsistenciasAnexosDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Soportes_Inconsistencias);
            this.Estad_PendientTransmisionDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Oficinas_Pend_Transmicion);
            this.Estad_OficinaPendienteProcesoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Oficinas_Pendientes_Proceso);
            this.Estad_TxDesmatConSoporteDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Tx_Desmat_Soporte_Fisico);
            this.Estad_NovedadValidFormDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Digit_Novedades_en_validaciones);

            this.CruceDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruce);

            this.Estad_SoportesSobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Soportes_Sobrantes);
            this.Estad_RegistrosSobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Registros_sobrantes);
            this.Estad_ResulProcesCruceAutoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Resultado_cruce_automático);
            this.Estad_TxNoIdentificadasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Tx_no_Identificadas);
            this.Estad_TxCruceExitosoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Transacciones_con_Cruce_Exitoso);
            this.Estad_TxExcluidaDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Transacciones_Excl_del_cruce);

            this.Estad_TxReversadasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Transacciones_reversadas_en_log);
            this.Estad_DocNoIdentifiDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Documentos_No_Identificadas);
            this.Estad_OficinaPendienteCierreDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Oficinas_pendientes_de_cierre);
            this.Estad_Tx357DashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Cruc_Transacciones_357);

            this.ConsolidadoDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Consolidados);


            this.Estad_TxProcesadasMensualmenteDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Consol_Transacciones_mensuales);
            this.Estad_ConsoliNovedadValidFormDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Consol_Consol_Novedades_Validaciones);
            this.Estad_ConsolidCruceAutomaticoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Consol_Consold_Cruce_Automático);
            this.Estad_ConsolidTotalDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Consol_Consolidado_Total);

            this.AjusteDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Ajustes);

            this.Estad_AjusteSoporteSobranteDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Ajuste_Soportes_Sobrantes);
            this.Estad_AjusteRegistroSobranteDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Ajuste_Registros_sobrantes);
            this.Estad_AjusteResultadoCruceDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Reporte_Estad_Ajuste_Resultado_cruce_automático);
            
            #endregion

            #region Oficinas tipo B

            this.OficinasTipoBDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Oficinas_tipo_B);

            #endregion

            #region Firmas
            this.FirmasDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas);

            this.Report_FirmasDashBoardItemLocal.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes);

            this.RF01_Gestion_Tarjetas_Firmas_FaltantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Gestion_Tarjetas_Firmas_Faltantes);
            this.RF02_Control_Envio_OficinaDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Control_Envio_Oficina);
            this.RF03_Soportes_SobrantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Soportes_Sobrantes);
            this.RF04_Cruce_ExitosoDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Cruce_Exitoso);
            this.RF05_Transacciones_Excluidas_CruceDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Transacciones_Excluidas_Cruce);
            this.RF06_Transacciones_MensualesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Transacciones_Mensuales);
            this.RF07_Tarjetas_Firmas_RechazadasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Tarjetas_Firmas_Rechazadas);
            this.RF08_Resultado_CierreDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Resultado_Cierre);
            this.RF09_Documentos_No_IdentificadosDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Reportes_Documentos_No_Identificados);

            this.ConsultasFirmasDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Consultas);
            this.AjustesFirmasFaltantesDashBoardItem.Visible = this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Firmas_Ajustes);

            #endregion
        }

        protected override void Load_Data() { }

        private void LaunchReportA(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerA).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerA, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportB(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerB).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerB, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportC(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerC).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerC, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportE(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerE).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerE, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportF(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerF).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerF, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportG(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerG).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerG, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportH(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerH).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerH, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportI(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerI).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerI, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchReportJ(WebReport nWebReport)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Reportes.WebReportViewerJ).FullName, "Reportes", Navigation.Site.Reportes.WebReportViewerJ, "0");
            this.MiharuSession.Pagina.Parameter["WebReport"] = nWebReport;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchGraphA(WebGraph nWebGraph)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Estadisticos.WebGraphViewerA).FullName, "Estadisticos", Navigation.Site.Estadisticos.WebGraphViewerA, "0");
            this.MiharuSession.Pagina.Parameter["WebGraph"] = nWebGraph;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        private void LaunchGraphB(WebGraph nWebGraph)
        {
            this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Estadisticos.WebGraphViewerB).FullName, "Estadisticos", Navigation.Site.Estadisticos.WebGraphViewerB, "0");
            this.MiharuSession.Pagina.Parameter["WebGraph"] = nWebGraph;
            this.Response.Redirect(this.MiharuSession.Pagina.PageDir);
        }

        #endregion
    }
}