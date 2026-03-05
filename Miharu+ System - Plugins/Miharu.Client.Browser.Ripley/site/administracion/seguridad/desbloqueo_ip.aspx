<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="desbloqueo_ip.aspx.cs" Inherits="Miharu.Client.Browser.site.administracion.seguridad.desbloqueo_ip" MasterPageFile="~/master/master_form.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/flexigrid.custom.js")%>" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/styles/flexigrid-1.1/flexigrid.css")%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="body" runat="Server" ContentPlaceHolderID="body">
    <script type="text/javascript">
        Frm = {
            CurrentPage: "desbloqueo_ip.aspx",
            IPBloqueadas: [],

            Init: function () {
                Global.ShowOptions('E', 'D');
                Frm.CargarIpsBloqueadas();
                Options.find.click(Frm.CargarIpsBloqueadas);
                Options.unlock.disabled(true);
                Options.unlock.click(Frm.DesbloquearIps);
            },

            DesbloquearIps: function () {
                try {
                    var inputs = $(".inputcheckbox", "#tbody_Ips_Bloqueadas");
                    var cadena = "";
                    for (var i = 0; i < inputs.length; i++) {
                        var data = GetNode(inputs[i], 'TR').data;
                        if (inputs[i].checked == true) {
                            cadena += data.ip_address.toString() + "|";
                        }
                    }
                    var param = "cadena=" + cadena;
                    AjaxRequest("DesbloquearIPs", "", param, function (html) {
                        TryExec(html);
                        Frm.CargarIpsBloqueadas();
                    });
                } catch (e) { alert(e); }
            },

            ValidarBloqueos: function () {
                var inputs = $(".inputcheckbox", "#tbody_Ips_Bloqueadas");
                var count = 0;
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].checked == true) {
                        count += 1;
                    }
                }
                Options.unlock.disabled(count == 0);
            },

            CargarIpsBloqueadas: function () {
                var param = ValidPar("ip_address", "", false);

                AjaxRequest("CargarIPBloqueada", "", param, function (html) {
                    TryExec(html);

                    $("#tbody_Ips_Bloqueadas").html("");
                    var IPHtml;

                    if (Frm.IPBloqueadas.length > 0) {
                        $(Frm.IPBloqueadas).each(function (i, ips) {
                            var rowId = ReplaceAll("ip_" + ips.ip_address, ":", "_");
                            rowId = ReplaceAll(rowId, ".", "_");

                            IPHtml = "<tr  id='" + rowId + "' >";
                            IPHtml += "<td style='width:260px; text-align:left; '><input id='" + rowId + "_check' class='inputcheckbox' type='checkbox'/></td>";
                            IPHtml += "<td style='width:260px; text-align:left; '>{0}</td>";
                            IPHtml += "<td style='width:260px; text-align:left; '>{1}</td>";
                            IPHtml += "<td style='width:260px; text-align:left; '>{2}</td></tr>";

                            IPHtml = IPHtml.replace("{0}", ips.ip_address);
                            IPHtml = IPHtml.replace("{1}", ips.fecha_conexion);
                            IPHtml = IPHtml.replace("{2}", ips.fecha_log);

                            $("#tbody_Ips_Bloqueadas").append(IPHtml);
                            Get(rowId).data = ips;

                            $("#" + rowId + "_check").click(Frm.ValidarBloqueos);
                        });

                    } else { Options.unlock.disabled(true); }
                });
            }
        }
    </script>
    <script type="text/javascript">
        Global.EditOptions =  <%= EditOptions.GetJson() %>;        
    </script>
    <div class="form">
        <div class="a-panel" style="top: 5px; left: 5px; right: 5px; height: 30px">
            <table class="aleft">
                <tr>
                    <td>
                        IP Adress
                    </td>
                    <td>
                        <input id="ip_address" maxlength="30" type="text" style="width: 100px" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="a-panel borde auto" style="top: 35px; left: 10px; right: 10px; bottom: 10px">
            <table width="100%;">
                <thead class="titulo">
                    <tr>
                        <td>
                        </td>
                        <td>
                            IP Adress
                        </td>
                        <td>
                            Fecha Conexión
                        </td>
                        <td>
                            Fecha Login
                        </td>
                    </tr>
                </thead>
                <tbody id="tbody_Ips_Bloqueadas">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
