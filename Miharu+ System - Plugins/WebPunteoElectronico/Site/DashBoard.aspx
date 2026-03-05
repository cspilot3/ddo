<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="WebPunteoElectronico.Site.DashBoard"
    MasterPageFile="~/Master/MasterForm.Master" %>

<%@ Register Src="../Controls/DashBoardItem.ascx" TagName="DashBoardItem" TagPrefix="slyg" %>
<%@ Register Src="../Controls/DashBoardItemLocal.ascx" TagName="DashBoardItemLocal"
    TagPrefix="slyg" %>
<asp:Content ID="MyHeadContentPlaceHolder" runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <script type="text/javascript">
        function DisplayTableMenu() {
            var request = leerGET();
            var TableMenu = request["TableMenu"];

            switch (TableMenu) {

                case "ServerMode":
                    var ServerMode = '<%= ServerModeString  %>';

                    switch (ServerMode) {
                        case "Estadisticos_Digitalizacion":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-estadisticos-digital');
                            break;

                        case "Estadisticos_Cruce":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-estadisticos-cruce');
                            break;

                        case "Reportes_Digitalizacion":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes-digitalizacion');
                            break;

                        case "Reportes_Cruce":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes-cruce');
                            break;

                        case "Reportes_Firmas":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes-firmas');
                            break;

                        case "Estadistico_Ajustes":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-estadisticos-ajustes');
                            break;

                        case "Estadistico_Consolidados":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-estadisticos-consolidados');
                            break;

                        case "SMS":
                            ShowDashBoardMenu('#Dashboard-main', '#Dashboard-sms');
                            break;

                        default:
                            break;
                    }

                    break;

                case "Ajustes":
                    ShowDashBoardMenu('#Dashboard-main', '#Dashboard-ajustes');
                    break;

                case "Servicio":
                    ShowDashBoardMenu('#Dashboard-main', '#Dashboard-sms');
                    break;
                case "Firmas":
                    ShowDashBoardMenu('#Dashboard-main', '#Dashboard-Firmas');
                    break;

                default:
                    break;
            }
        }

        function ShowDashBoardMenu(aOcultar, aMostrar) {
            $(aOcultar).css("display", "none");
            $(aMostrar).css("display", "");
        }

//        function ActividadesResize(e) {
//            var w = $(e.srcElement).width();
//            var g = $("#MainSlygFlexigrid_flex");
//            //    g.flexOptions({ width: w });
//            g.fixHeight(100);
//            //    g.resize(w, g[0].p.height);
//        }

        function leerGET() {
            var cadGET = location.search.substr(1, location.search.length);
            var arrGET = cadGET.split("&");
            var asocGET = new Array();
            var variable = "";
            var valor = "";
            for (var i = 0; i < arrGET.length; i++) {
                var aux = arrGET[i].split("=");
                variable = aux[0];
                valor = aux[1];
                asocGET[variable] = valor;
            }

            return asocGET;
        }
    </script>
</asp:Content>
<asp:Content ID="MyBodyContentPlaceHolder" runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <fieldset id="" class="DashBoardGroup" style="text-align: left">
        <legend>Dashboard</legend>
        <table id="Dashboard-main" border="0" cellpadding="2" cellspacing="2">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="ConsultasDashBoardItem" runat="server" Title="Consultas"
                        ImageUrl="~/Images/dashboard/consultas.png" Tooltip="Módulo de consultas" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AjustesDashBoardItemLocal" runat="server" Title="Ajustes"
                        Tooltip="Módulo de ajustes" ImageUrl="~/Images/dashboard/ajustes.png" OnClientClick="ShowDashBoardMenu('#Dashboard-main', '#Dashboard-ajustes')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ServicioClienteDashBoardItemLocal" runat="server" Title="Servicio al cliente"
                        Tooltip="Módulo de servicio al cliente" ImageUrl="~/Images/dashboard/servicio_cliente.png"
                        OnClientClick="ShowDashBoardMenu('#Dashboard-main', '#Dashboard-sms')" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="OficinasTipoBDashBoardItem" runat="server" Title="Oficinas Tipo B"
                        Tooltip="Descargar aplicación para Oficinas Tipo B" ImageUrl="~/Images/dashboard/oficinastipob.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItem ID="AcercaDeDashBoardItem" runat="server" Title="Acerca de" Tooltip="Acerca de WebPunteoElectrónico..."
                        ImageUrl="~/Images/dashboard/acercade.png" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportesDashBoardItemLocal" runat="server" Title="Reportes"
                        Tooltip="Módulo de reportes" ImageUrl="~/Images/dashboard/reportes.png" OnClientClick="ShowDashBoardMenu('#Dashboard-main', '#Dashboard-reportes')" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="ReporteGerencialDashBoardItem" runat="server" Title="Informe Gerencial"
                        Tooltip="Módulo de informe gerencial" ImageUrl="~/Images/dashboard/reporte_gerencial.png" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="EstadisticosDashBoardItem" runat="server" Title="Estadisticos"
                        Tooltip="Módulo de estadísticos" ImageUrl="~/Images/dashboard/estadisticos.png"
                        OnClientClick="ShowDashBoardMenu('#Dashboard-main', '#Dashboard-estadisticos')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="FirmasDashBoardItemLocal" runat="server" Title="Firmas"
                        Tooltip="Módulo de Firmas" ImageUrl="~/Images/dashboard/firmas.png" OnClientClick="ShowDashBoardMenu('#Dashboard-main', '#Dashboard-Firmas')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-estadisticos" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItemLocal ID="DigitalizacionDashBoardItemLocal" runat="server" Title="Digitalización"
                        ImageUrl="~/Images/dashboard/digitalizacion.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos', '#Dashboard-estadisticos-digital')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="CruceDashBoardItemLocal" runat="server" Title="Cruce"
                        ImageUrl="~/Images/dashboard/cruce.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos', '#Dashboard-estadisticos-cruce')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ConsolidadoDashBoardItemLocal" runat="server" Title="Consolidados"
                        ImageUrl="~/Images/dashboard/consolid_valid_forma.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos', '#Dashboard-estadisticos-consolidados')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AjusteDashBoardItemLocal" runat="server" Title="Ajustes"
                        ImageUrl="~/Images/dashboard/diferencia_datos.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos', '#Dashboard-estadisticos-ajustes')" />
                </td>
                <td>
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="EstadBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-estadisticos-digital" border="0" cellpadding="2" cellspacing="2"
            style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Estad_ContenedorOficinaDashBoardItem" runat="server" Title="01-Cont. Oficinas"
                        Tooltip="01-Contenedor Oficinas" ImageUrl="~/Images/dashboard/contenedor_ofic.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_DifDocTulaContenedorDashBoardItem" runat="server" Title="02- Dif. Doc Fisicos"
                        Tooltip="02-Diferencias entre Doc. Físicos en Tula y en Contenedores" ImageUrl="~/Images/dashboard/dif_fisicos_contenedor.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_DocSinOrdenDefinidoDashBoardItem" runat="server" Title="03-Doc. sin Orden"
                        Tooltip="03-Documentos Remitidos Sin Ordenamiento Definido" ImageUrl="~/Images/dashboard/docsinorden.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_InconsistenciasAnexosDashBoardItem" runat="server"
                        Tooltip="04-Soportes Transacciones Con Inconsistencias y/o Anexos Faltantes"
                        Title="04-Sop. Tx sin Anexo" ImageUrl="~/Images/dashboard/anexos_faltantes.png" />
                </td>
                
                <td>
                    <slyg:DashBoardItemLocal ID="Estad_Dig_BackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos-digital', '#Dashboard-estadisticos')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Estad_PendientTransmisionDashBoardItem" runat="server" Title="06-Ofic. Pend. Trans."
                        Tooltip="06-Oficinas Pendientes de Transmisión y/o Envío Movimiento" ImageUrl="~/Images/dashboard/pendiente_envio_mov.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_OficinaPendienteProcesoDashBoardItem" runat="server"
                        Title="07-Of. Pend. Proc." Tooltip="07-Oficina Pendiente Proceso" ImageUrl="~/Images/dashboard/estad_oficina_pendiente_cierre.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_TxDesmatConSoporteDashBoardItem" runat="server" Title="09-Tx Desmat. Sop."
                        Tooltip="09-Transacciones Desmaterializadas Con Soporte Físico" ImageUrl="~/Images/dashboard/tx_desmat_con_soporte_fisico.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_NovedadValidFormDashBoardItem" runat="server" Title="14-Nov. Valid. Forma"
                        Tooltip="14-Novedades en validaciones de forma" ImageUrl="~/Images/dashboard/validaciones_forma.png" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table id="Dashboard-estadisticos-cruce" border="0" cellpadding="2" cellspacing="2"
            style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Estad_SoportesSobrantesDashBoardItem" runat="server" Tooltip="Soportes Sobrantes"
                        Title="11-Sop. Sobrantes" ImageUrl="~/Images/dashboard/soportes_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_RegistrosSobrantesDashBoardItem" Tooltip="Registros sobrantes"
                        runat="server" Title="12-Reg. sobrantes" ImageUrl="~/Images/dashboard/registros_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_ResulProcesCruceAutoDashBoardItem" runat="server" Title="13-Resul. Cruce auto."
                        Tooltip="Resultado Proceso cruce automático" ImageUrl="~/Images/dashboard/resul_cruce_autom.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_TxNoIdentificadasDashBoardItem" runat="server" Title="19-Tx No Identif."
                        Tooltip="Transacciones No Identificadas" ImageUrl="~/Images/dashboard/txnoidentifi.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_TxCruceExitosoDashBoardItem" runat="server" Title="20-Tx Cruce Exitoso"
                        Tooltip="Transacciones Con Cruce Exitoso" ImageUrl="~/Images/dashboard/txcruceexitoso.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="EstadCruceBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos-cruce', '#Dashboard-estadisticos')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Estad_TxExcluidaDashBoardItem" runat="server" Title="22-Tx. Excluida Log"
                        Tooltip="22-Transaccion Excluida Archivo Detalle" ImageUrl="~/Images/dashboard/tx_excluidas.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_TxReversadasDashBoardItem" runat="server" Title="23-Tx Revers. Log"
                        Tooltip="23-Transacciones Reversadas en Log" ImageUrl="~/Images/dashboard/txreversadas.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_DocNoIdentifiDashBoardItem" runat="server" Title="24-Doc. No Identif."
                        Tooltip="24-Documentos No Identificados" ImageUrl="~/Images/dashboard/docnoidentifi.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_OficinaPendienteCierreDashBoardItem" runat="server"
                        Title="25-Of. Pend. Cierre" Tooltip="25-Oficinas Pendientes Cierre" ImageUrl="~/Images/dashboard/resultado_cierre.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_Tx357DashBoardItem" runat="server" Title="28-Tx. 357"
                        Tooltip="28-Transaccion 357" ImageUrl="~/Images/dashboard/tx_357.png" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-estadisticos-ajustes" border="0" cellpadding="2" cellspacing="2"
            style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Estad_AjusteSoporteSobranteDashBoardItem" runat="server"
                        Title="11-Sop. Sobrantes" Tooltip="11-Ajuste Soporte Sobrante" ImageUrl="~/Images/dashboard/soportes_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_AjusteRegistroSobranteDashBoardItem" runat="server"
                        Tooltip="12-Registros Sobrantes" Title="12-Reg. Sobrantes" ImageUrl="~/Images/dashboard/registros_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_AjusteResultadoCruceDashBoardItem" Tooltip="13-Resultados Cruce Automatico"
                        runat="server" Title="13-Res. Cruce Auto." ImageUrl="~/Images/dashboard/dif_valor.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="EstadConsolidadoBackDashBoardItemLocal" runat="server"
                        Title="Regresar" ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos-ajustes', '#Dashboard-estadisticos')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-estadisticos-consolidados" border="0" cellpadding="2" cellspacing="2"
            style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Estad_TxProcesadasMensualmenteDashBoardItem" runat="server"
                        Title="08-Tx Mensuales" Tooltip="08-Tx Mensuales" ImageUrl="~/Images/dashboard/txprocesmensual.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_ConsoliNovedadValidFormDashBoardItem" runat="server"
                        Title="16-Con. Nov. Val. Forma" Tooltip="16-Consolidado Novedades en validaciones de forma"
                        ImageUrl="~/Images/dashboard/consolid_valid_forma.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_ConsolidCruceAutomaticoDashBoardItem" runat="server"
                        Title="18-Cons. Cruce auto " Tooltip="Consolidado del cruce automático " ImageUrl="~/Images/dashboard/consolidadocruceautom.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Estad_ConsolidTotalDashBoardItem" runat="server" Title="Cons. Total"
                        Tooltip="Consolidado Total" ImageUrl="~/Images/dashboard/Consolidado_Total.png" />
                </td>
                <td>
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="DashBoardItemLocal2" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-estadisticos-consolidados', '#Dashboard-estadisticos')" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table id="Dashboard-reportes" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItemLocal ID="Report_DigDashBoardItemLocal" runat="server" Title="Digitalización"
                        ImageUrl="~/Images/dashboard/digitalizacion.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-reportes-digitalizacion')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="Report_CruceDashBoardItemLocal" runat="server" Title="Cruce"
                        ImageUrl="~/Images/dashboard/cruce.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-reportes-cruce')" />
                </td>
                <td>
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportesBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-reportes-cruce" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Report_Consolid_MedioPagoDashBoardItem" runat="server" Title="05-Cons. Med. Pago"
                        Tooltip="05-Consolidado Medios de Pago" ImageUrl="~/Images/dashboard/consolid_medio_pago.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Tx_MensualesDashBoardItem" runat="server" Title="08-Tx Mensuales"
                        Tooltip="Transacciones Mensuales" ImageUrl="~/Images/dashboard/txprocesmensual.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_SoportesSobrantesDashBoardItem" runat="server" Tooltip="Soportes Sobrantes"
                        Title="11-Sop. Sobrantes" ImageUrl="~/Images/dashboard/soportes_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_RegistrosSobrantesDashBoardItem" Tooltip="Registros sobrantes"
                        runat="server" Title="12-Reg. sobrantes" ImageUrl="~/Images/dashboard/registros_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_ResultadoCruceAutomaticoDashBoardItem" runat="server"
                        Title="13-Resul. Cruce" Tooltip="Resultado Proceso cruce automático" ImageUrl="~/Images/dashboard/resul_cruce_autom.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Consolid_Cruce_AutomDashBoardItem" runat="server"
                        Title="18-Cons. Cruce" Tooltip="Consolidado del cruce automático " ImageUrl="~/Images/dashboard/consolidadocruceautom.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Tx_No_IdentificadasDashBoardItem" runat="server" Title="19-Tx No Identif."
                        Tooltip="Transacciones No Identificadas" ImageUrl="~/Images/dashboard/txnoidentifi.png" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportCruceBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes-cruce', '#Dashboard-reportes')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Report_Tx_CruceExitosoDashBoardItem" runat="server" Title="20-Tx Cruce Exit."
                        Tooltip="Transacciones Con Cruce Exitoso" ImageUrl="~/Images/dashboard/txcruceexitoso.png" />
                </td>
                 <td>
                    <slyg:DashBoardItem ID="Report_CierreDashBoardItem" runat="server" Title="21-Resul. Cierre"
                        Tooltip="21-Resultado Cierre" ImageUrl="~/Images/dashboard/resultado_cierre.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_TxExcluidasCruceDashBoardItem" runat="server" Title="22-Tx Excluidas"
                        Tooltip="Transacciones Excluidas de Cruce" ImageUrl="~/Images/dashboard/tx_excluidas.png" />
                </td>
                 <td>
                    <slyg:DashBoardItem ID="Report_TxReversadasLogDashBoardItem" runat="server" Title="23-Tx Reversadas"
                        Tooltip="23-Transacciones Reversadas en Log" ImageUrl="~/Images/dashboard/txreversadas.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_DocNoIdentificadosDashBoardItem" runat="server" Title="24-Doc. No Identif."
                        Tooltip="Documentos No Identificados" ImageUrl="~/Images/dashboard/docnoidentifi.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Tx357DashBoardItem" runat="server" Title="26-Tx 357 "
                        Tooltip="Transacciones 357 " ImageUrl="~/Images/dashboard/tx_357.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Devolucion_Canje_RecibidoDashBoardItem" runat="server"
                        Title="29-Devol. Canje" Tooltip="Devolución de Canje Recibido" ImageUrl="~/Images/dashboard/Devolucion_Recibido.png" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-reportes-digitalizacion" border="0" cellpadding="2" cellspacing="2"
            style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Report_Contenedores_OficDashBoardItem" runat="server" Tooltip="Cont. Oficinas"
                        Title="01-Cont Oficinas" ImageUrl="~/Images/dashboard/contenedor_ofic.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Dif_Doc_Fisc_Tul_ContDashBoardItem" runat="server"
                        Title="02- Dif. Fisicos" Tooltip="02-Diferencias entre Doc. Físicos en Tula y en Contenedores"
                        ImageUrl="~/Images/dashboard/dif_fisicos_contenedor.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Doc_Remit_Sin_OrdenDashBoardItem" runat="server" Title="03-Doc. sin Orden"
                        Tooltip="03-Documentos Remitidos Sin Ordenamiento Definido" ImageUrl="~/Images/dashboard/docsinorden.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Soport_Tx_inconsistenciasDashBoardItem" runat="server"
                        Tooltip="04-Soportes Transacciones Con Inconsistencias y/o Anexos Faltantes"
                        Title="04-Tx sin Anexos" ImageUrl="~/Images/dashboard/anexos_faltantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Ofic_Pendi_Envio_MovDashBoardItem" runat="server"
                        Title="06-Pend. Transm." Tooltip="06-Oficinas Pendientes de Transmisión y/o Envío Movimiento"
                        ImageUrl="~/Images/dashboard/pendiente_envio_mov.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportDigBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes-digitalizacion', '#Dashboard-reportes')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="Report_Pendiente_ProcesoDashBoardItem" runat="server" Title="07-Pendien. Proc."
                        Tooltip="07-Oficinas Pendientes Proceso" ImageUrl="~/Images/dashboard/ofic_pend_proceso.png" />
                </td>
                 <td>
                    <slyg:DashBoardItem ID="Report_Tx_Desmat_Soporte_FisicoDashBoardItem" runat="server"
                        Title="09-Tx Des. Sop." Tooltip="09-Transacciones Desmaterializadas Con Soporte Físico"
                        ImageUrl="~/Images/dashboard/tx_desmat_con_soporte_fisico.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Noved_Valid_FormDashBoardItem" runat="server" Title="14-Valid. Forma"
                        Tooltip="14-Novedades en validaciones de Forma" ImageUrl="~/Images/dashboard/validaciones_forma.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="Report_Consolid_Nov_Valid_FormDashBoardItem" runat="server"
                        Title="16-Con. Nove. Vali." Tooltip="16-Consolidado de novedades en validaciones de forma"
                        ImageUrl="~/Images/dashboard/consolid_valid_forma.png" />
                </td>
                
                <td>
                    <slyg:DashBoardItem ID="Report_Pendiente_CierreDashBoardItem" runat="server" Title="25-Pend. Cierre"
                        Tooltip="25-Oficinas Pendientes de Cierre" ImageUrl="~/Images/dashboard/oficina_pend_cierre.png" />
                </td>  
            </tr>
        </table>
        <table id="Dashboard-reportes-firmas" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="RF01_Gestion_Tarjetas_Firmas_FaltantesDashBoardItem" runat="server" Title="Tarjetas Faltantes"
                        Tooltip="Tarjetas Faltantes" ImageUrl="~/Images/dashboard/registros_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RF02_Control_Envio_OficinaDashBoardItem" runat="server" Title="Control Envio Oficina"
                        Tooltip="Control Envio Oficina" ImageUrl="~/Images/dashboard/ofic_pend_proceso.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RF03_Soportes_SobrantesDashBoardItem" runat="server" Tooltip="Soportes Sobrantes"
                        Title="Soportes Sobrantes" ImageUrl="~/Images/dashboard/soportes_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RF04_Cruce_ExitosoDashBoardItem" Tooltip="Cruce Exitoso"
                        runat="server" Title="Cruce Exitoso" ImageUrl="~/Images/dashboard/txcruceexitoso.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RF05_Transacciones_Excluidas_CruceDashBoardItem" runat="server" Title="Tx Excluidas Cruce" 
                    Tooltip="Transacciones Excluidas Cruce" ImageUrl="~/Images/dashboard/resul_cruce_autom.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RF06_Transacciones_MensualesDashBoardItem" runat="server"
                        Title="Tx Mensuales" Tooltip="Transacciones Mensuales" ImageUrl="~/Images/dashboard/txprocesmensual.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RF07_Tarjetas_Firmas_RechazadasDashBoardItem" runat="server" Title="Tarjetas Rechazadas"
                        Tooltip="Tarjetas Firmas Rechazadas" ImageUrl="~/Images/dashboard/consolidadocruceautom.png" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="ReportFirmasBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-reportes-firmas', '#Dashboard-Firmas')" />
                </td>
            </tr>
            <tr>
                <td>
                    <slyg:DashBoardItem ID="RF08_Resultado_CierreDashBoardItem" runat="server" Title="Resultado Cierre"
                        Tooltip="Resultado Cierre" ImageUrl="~/Images/dashboard/resultado_cierre.png" />
                </td>
                 <td>
                    <slyg:DashBoardItem ID="RF09_Documentos_No_IdentificadosDashBoardItem" runat="server" Title="Doc No Identificados"
                        Tooltip="Documentos No Identificados" ImageUrl="~/Images/dashboard/txnoidentifi.png" />
                </td>
                <td>
                </td>
                 <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table id="Dashboard-ajustes" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="SoportesSobrantesDashBoardItem" runat="server" Title="Soportes Sobrantes"
                        ImageUrl="~/Images/dashboard/soportes_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="RegistrosSobrantesDashBoardItem" runat="server" Title="Registros Sobrantes"
                        ImageUrl="~/Images/dashboard/registros_sobrantes.png" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="DifereciaDatosDashBoardItemLocal" runat="server" Title="Diferencia Datos"
                        ImageUrl="~/Images/dashboard/diferencia_datos.png" OnClientClick="ShowDashBoardMenu('#Dashboard-ajustes', '#Dashboard-ajustes-dif_datos')" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="AjustesRealizadosDashBoardItem" runat="server"
                        Title="Reporte Ajustes" ImageUrl="~/Images/dashboard/ajustes.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AjustesBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-ajustes', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-ajustes-dif_datos" border="0" cellpadding="2" cellspacing="2"
            style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="DiferenciasValorDashBoardItem" runat="server" Title="Diferencia en Valor"
                        ImageUrl="~/Images/dashboard/dif_valor.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="DiferenciasProductoDashBoardItem" runat="server" Title="Diferencia en Producto"
                        ImageUrl="~/Images/dashboard/dif_producto.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="DiferenciasMedioPagoDashBoardItem" runat="server" Title="Diferencia en Medio Pago"
                        ImageUrl="~/Images/dashboard/dif_medio_pago.png" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="DashBoardItemLocal1" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-ajustes-dif_datos', '#Dashboard-ajustes')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-gerencial" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="GerencialBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-gerencial', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-administracion" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItemLocal ID="BaseDashBoardItemLocal" runat="server" Title="Base"
                        ImageUrl="~/Images/dashboard/Bases.png" OnClientClick="ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-administracion-Base')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="TransportadorasDashBoardItemLocal" runat="server" Title="Transportadoras"
                        ImageUrl="~/Images/dashboard/truck-icon.png" OnClientClick="ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-administracion-Transportadoras')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="seguridadDashBoardItemLocal" runat="server" Title="Seguridad"
                        ImageUrl="~/Images/dashboard/Security.png" OnClientClick="ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-administracion-seguridad')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="bancoDashBoardItemLocal" runat="server" Title="Bancos"
                        ImageUrl="~/Images/dashboard/Company.png" OnClientClick="ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-administracion-banco')" />
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AdminBanRepDashBoardItemLocal" runat="server" Title="Banco de la Republica"
                        ImageUrl="~/Images/dashboard/Agency.png" OnClientClick="ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-administracion-BanRep')" />
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="AdministracionBackDashBoardItemLocal" runat="server"
                        Title="Regresar" ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-administracion', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
         <table id="Dashboard-Firmas" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
             <td>
                <slyg:DashBoardItemLocal ID="Report_FirmasDashBoardItemLocal" runat="server" Title="Reportes"
                    ImageUrl="~/Images/dashboard/ajustes.png" OnClientClick="ShowDashBoardMenu('#Dashboard-Firmas', '#Dashboard-reportes-firmas')" />
                </td>
               <td>
                 <slyg:DashBoardItem ID="ConsultasFirmasDashBoardItem" runat="server" Title="Consultas"
                        ImageUrl="~/Images/dashboard/consultas_firmas.png" Tooltip="Módulo de consultas Firmas" display="none"/>
                </td>
                <td>
                    <slyg:DashBoardItem ID="AjustesFirmasFaltantesDashBoardItem" runat="server" Title="Ajustes"
                        ImageUrl="~/Images/dashboard/registros_sobrantes.png" Tooltip="Módulo de Ajustes de Firmas"/>
                </td>
             <td class="DashBoard-separator">
             </td>
             <td>
                 <slyg:DashBoardItemLocal ID="DashBoardItemLocal3" runat="server" Title="Regresar"
                 ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-Firmas', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
        <table id="Dashboard-sms" border="0" cellpadding="2" cellspacing="2" style="display: none">
            <tr>
                <td>
                    <slyg:DashBoardItem ID="SMSDashBoardItemLocal" runat="server" Title="Registrar PQR"
                        ImageUrl="~/Images/dashboard/servicio_cliente.png" />
                </td>
                <td>
                    <slyg:DashBoardItem ID="SMSReporteDashBoardItemLocal" runat="server" Title="Consulta PQRs"
                        ImageUrl="~/Images/dashboard/anexos_faltantes.png" />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="DashBoard-separator">
                </td>
                <td>
                    <slyg:DashBoardItemLocal ID="SMSBackDashBoardItemLocal" runat="server" Title="Regresar"
                        ImageUrl="~/Images/dashboard/back.png" OnClientClick="ShowDashBoardMenu('#Dashboard-sms', '#Dashboard-main')" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script type="text/javascript">
        DisplayTableMenu();            
    </script>
</asp:Content>
