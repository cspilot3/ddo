<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporte_CobroJuridico.aspx.cs"
    Inherits="Miharu.Explorer._Site.Garantias.Bancoomeva.Reporte_CobroJuridico" %>

<%@ Register assembly="Miharu.Explorer" namespace="Explorer.Controls" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../_Styles/Controls.css" rel="stylesheet" type="text/css" />
    <link href="../../../_Scripts/jqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../_Scripts/jquery-ui/ui.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../_Scripts/alert/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../../../_Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jqGrid/jqGrid.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../_Scripts/alert/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jqGridExportToExcel.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jquery-dateFormat.js" type="text/javascript"></script>
    <script src="../../../_Scripts/Core.js" type="text/javascript"></script>  
    <script src="Reporte_CobroJuridico.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

// <![CDATA[

        function Button1_onclick() {

        }

function Button2_onclick() {

}

// ]]>
    </script>

    <style type="text/css">
        .style1
        {
            width: 25%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="MasterScriptManager" ScriptMode="Release" runat="server">
        </asp:ScriptManager>
        <%--<asp:HiddenField ID="ComboCampoBusqueda" runat="server" />--%>
        <asp:HiddenField ID="ComboTipo" runat="server" />
        <asp:HiddenField ID="ComboMotivo" runat="server" />
        <asp:HiddenField ID="ComboPrioridad" runat="server" />
        <asp:HiddenField ID="ComboTipo2" runat="server" />
        <asp:HiddenField ID="ComboMotivo2" runat="server" />
        <asp:HiddenField ID="ComboPrioridad2" runat="server" />
        <asp:HiddenField ID="EntidadUsuario" runat="server" />
        <asp:HiddenField ID="ImagingURL" runat="server" />
        <asp:HiddenField ID="PermisoSolicitudes" runat="server" />
        <table id="tableContent" style="width: 95%; height: inherit;" >
            <tr>
                <td style="padding: 10px; vertical-align: top; border-color: #C5DBEC;
                    border-right-style: solid;" class="style1">
                    <table style="width: 100%;">
                        <tr>
                            <td class="Titulo_Terceario">
                                Reporte Cobro Juridico</td>
                        </tr>
                        <tr>
                            <td class="contentText">
                                Entidad
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="control">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="contentText">
                                Fecha Recoleccion
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc1:DFecha ID="DFecha1" runat="server" DateFormat="yyyy/MM/dd"
                                MaskFormat="YearMonthDay" CssClass="control" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="Button1" type="button" value="Buscar" class="button" onclick="buscarRepCobroJuridico()" />
                                <input id="BtnExportar" type="button" value="Exportar" class="button" onclick="Exportar()" disabled="disabled" />
                            </td>
                        </tr>
                    </table>


                    
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    
                </td>
                <td style="width: 75%; vertical-align: top; padding-left: 10px; border-color: #C5DBEC;
                    border-right-style: solid;" >
                    <table id="tblReporte">
                    </table>
                    <div id="divReporte">
                    </div>
                    <br />
                </td>
            </tr>
        </table>       
    </div>
    </form>
</body>
</html>
