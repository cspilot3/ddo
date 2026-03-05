<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="esquemas.aspx.cs" Inherits="Miharu.Client.Browser.site.administracion.seguridad.esquemas" MasterPageFile="~/master/master_form.Master" %>

<%@ Register Src="~/controls/filter.ascx" TagName="filter" TagPrefix="block" %>
<%@ Register TagPrefix="miharu" Namespace="Miharu.Client.Browser.code.Grid" Assembly="Miharu.Client.Browser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/flexigrid.custom.js")%>" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/styles/flexigrid-1.1/flexigrid.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/jquery.floatnumber.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .gridclass
        {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            top: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" runat="Server" ContentPlaceHolderID="body">
    <script type="text/javascript">
        Frm = {
            CurrentPage: "esquemas.aspx",
            Entidad: [],
            id_entidad: "",
            id_esquema_seguridad: "",

            Init: function () {
                Global.InitConfigHandlers("tabs", Frm.OptionClick);
                Global.InitFilter(Frm.FilterClick);
                Frm.entidad_filtro.autoList(Frm.Entidad, function () { });
                Frm.nombre_entidad_esquem.autoList(Frm.Entidad, function () { });
            },


            FilterClick: function (filter) {
                var param = "Filtro=" + (filter == "*" ? "%" : filter);
                param += "&" + ValidPar("entidad_filtro", "", false);
                AjaxRequest("CambiarFiltro", "", param, function () {
                    $('#MainGrid_flex').flexReload();
                });
            },

            MainGridClick: function (value, row, cells, evt, grid) {
                $("#Detalle").show();
                Global.SelectTab("tabs", 1, "Edit");
                Frm.nombre_entidad_esquem.val(GetRowValue(cells, 'nombre_entidad'));
                Frm.nombre_esquem.val(GetRowValue(cells, 'nombre_esquema_seguridad'));
                Frm.longitud_equem.val(GetRowValue(cells, 'min_longitud')).number(0);
                Frm.especiales_esquem.val(GetRowValue(cells, 'min_especiales')).number(0);
                Frm.mayusculas_esquem.val(GetRowValue(cells, 'min_mayusculas')).number(0);
                Frm.minusculas_esquem.val(GetRowValue(cells, 'min_minusculas')).number(0);
                Frm.numeros_esquem.val(GetRowValue(cells, 'min_numeros')).number(0);
                Frm.historial_esquem.val(GetRowValue(cells, 'num_historial')).number(0);
                Frm.cambio_pswd_esquem.prop('checked', Boolean(parseInt(GetRowValue(cells, 'cambiar_password'))));
                Frm.dias_cambio_equem.val(GetRowValue(cells, 'dias_cambio_password')).number(0);
                Frm.id_entidad = GetRowValue(cells, 'fk_entidad');
                Frm.id_esquema_seguridad = GetRowValue(cells, 'id_esquema_seguridad');

            },

            OptionClick: function (option) {
                try {
                    if (option == "add") {
                        //New

                        $("#Detalle").show();
                        $("input[type=text]", "#Detalle").val('');
                        Frm.id_entidad = "";
                        Frm.id_esquema_seguridad = "";
                        SetAutoListDataSource(Frm.nombre_entidad_esquem, Frm.Entidad, false);
                        Global.SelectTab("tabs", 1, "New");

                    } else {
                        if (option == "remove") {
                            Frm.EliminarEsquema();
                        } else {
                            //Save
                            Frm.GuardarEsquema();
                        }
                    }
                }
                catch (e) { alert(e); }
            },


            EliminarEsquema: function () {
                try {
                    if (Frm.id_entidad != "") {
                        var param = "id_entidad=" + Frm.id_entidad;
                        param += "&id_esquema_seguridad=" + Frm.id_esquema_seguridad;
                    } else {
                        param += "&" + Par("id_entidad", "Constructora", true);
                        if (!confirm('¿Está seguro de eliminar el registro?')) return;
                    }
                    AjaxRequest("EliminarEsquemaSeguridad", "", param, function (html) {
                        TryExec(html);
                        Frm.OcultarDetalle();
                        Frm.FilterClick("*");
                        $('#MainGrid_flex').flexReload();
                        Frm.id_entidad = "";
                        Frm.id_esquema_seguridad = "";
                    });
                }
                catch (e) { alert(e); }
            },

            GuardarEsquema: function () {
                try {
                    var param = ValidPar("nombre_entidad_esquem", "Constructora", true);
                    param += "&" + ValidPar("nombre_esquem", "Esquema Seguridad", true);
                    param += "&" + ValidPar("longitud_equem", "Longitud", true);
                    param += "&" + ValidPar("especiales_esquem", "Especiales", true);
                    param += "&" + ValidPar("mayusculas_esquem", "Mayúsculas", true);
                    param += "&" + ValidPar("minusculas_esquem", "Minúsculas", true);
                    param += "&" + ValidPar("numeros_esquem", "Números", true);
                    param += "&" + ValidPar("historial_esquem", "Historial", true);
                    param += "&cambio_pswd_esquem=" + (Frm.cambio_pswd_esquem.is(":checked") ? "1" : "0");
                    param += "&" + ValidPar("dias_cambio_equem", "Días Cambio Contraseña", true);
                    param += "&id_esquema_seguridad=" + Frm.id_esquema_seguridad;

                    AjaxRequest("GuardarEsquemaSeguridad", "", param, function (html) {
                        TryExec(html);
                        Frm.OcultarDetalle();
                        Frm.FilterClick("*");
                    });
                } catch (e) { alert(e); }
            },

            OcultarDetalle: function () {
                $("#Detalle").hide();
                Global.SelectTab("tabs", 0, "");
            }
        }
    </script>
    <script type="text/javascript">
        Global.EditOptions = <%= EditOptions.GetJson() %>;
        Frm.Entidad = <%= Entidad.GetJson() %>;
    </script>
    <div class="form">
        <div id="tabs" class="tabcontrol">
            <ul>
                <li><a id="tab_filtro" href="#tabs-1">Búsqueda</a></li>
                <li><a id="tab_detalle" href="#tabs-2">Detalle</a></li>
            </ul>
            <div id="tabs-1">
                <div class="tab-panel">
                    <table class="aleft" width="300px">
                        <tr>
                            <td style="width: 100px;">
                                Constructora
                            </td>
                            <td>
                                <input id="entidad_filtro" style="width: 400px;" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div>
                                    <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Nombre]" CssClass="Label" Width="400px" Height="14px"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <block:filter ID="MyFilter" runat="server" ClientIDMode="Static" />
                    </div>
                    <miharu:SlygFlexigrid ID="MainGrid" runat="server" Title="Esquemas Seguridad" UrlData="../../../controls/proxy_data.aspx" OnRowDblClick="Frm.MainGridClick"
                        CssClass="gridclass" />
                </div>
            </div>
            <div id="tabs-2">
                <div class="tab-panel aleft">
                    <div class="block">
                        <table id="Detalle" class="aleft hidden">
                            <tr>
                                <td>
                                    Constructora
                                </td>
                                <td>
                                    <input id="nombre_entidad_esquem" type="text" disabled="disabled" style="width: 383px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Esquema de Seguridad
                                </td>
                                <td>
                                    <input id="nombre_esquem" type="text" style="width: 400px;" maxlength="100" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Longitud
                                </td>
                                <td>
                                    <input id="longitud_equem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Especiales
                                </td>
                                <td>
                                    <input id="especiales_esquem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Mayúsculas
                                </td>
                                <td>
                                    <input id="mayusculas_esquem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Minúsculas
                                </td>
                                <td>
                                    <input id="minusculas_esquem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Números
                                </td>
                                <td>
                                    <input id="numeros_esquem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Historial
                                </td>
                                <td>
                                    <input id="historial_esquem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Exigir Cambio Periódico de Contraseña?
                                </td>
                                <td>
                                    <input id="cambio_pswd_esquem" type="checkbox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Días Cambio
                                </td>
                                <td>
                                    <input id="dias_cambio_equem" type="text" style="width: 100px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
