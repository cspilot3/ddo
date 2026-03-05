<%@ Page Language="C#" enableEventValidation="false" AutoEventWireup="true" CodeBehind="Solicitudes.aspx.cs" Inherits="Miharu.Explorer._Site.Garantias.Solicitudes" %>

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
    <script src="../../../_Scripts/Core.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jqGridExportToExcel.js" type="text/javascript"></script>
    <script src="../../../_Scripts/jquery-dateFormat.js" type="text/javascript"></script>
    <script src="Solicitudes.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="ComboCampoBusqueda" runat="server" />
        <asp:HiddenField ID="ComboTipo" runat="server" />
        <asp:HiddenField ID="ComboMotivo" runat="server" />
        <asp:HiddenField ID="ComboPrioridad" runat="server" />
        <asp:HiddenField ID="ComboTipo2" runat="server" />
        <asp:HiddenField ID="ComboMotivo2" runat="server" />
        <asp:HiddenField ID="ComboPrioridad2" runat="server" />
        <asp:HiddenField ID="EntidadUsuario" runat="server" />
        <asp:HiddenField ID="ImagingURL" runat="server" />
        <asp:HiddenField ID="PermisoSolicitudes" runat="server" />
        <table class="" id="tableContent" style="width: 95%; height: inherit;" >
            <tr>
                <td style="padding: 10px; vertical-align: top; border-color: #C5DBEC;
                    border-right-style: solid;" class="style1">
                    <table style="width: 100%;">
                        <tr>
                            <td class="Titulo_Terceario">
                                Solicitudes Masivas</td>
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
                                Proyecto
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlProyecto" runat="server" CssClass="control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                     </table>  
                     <br /> 
                     <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                     </table>   
                     <br />
                     <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Button ID="Button4" Text="Cargar" class="button" OnClick = "Upload" runat="server" />
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
                    <table id="tblCarpetas">
                    </table>
                    <div id="divCarpetas">
                    </div>
                    <br />
                    <table id="tblDocumentos">
                    </table>
                    <div id="divDocumentos">
                    </div>
                    <table id="tblFaltantesLogicos">
                    </table>
                    <div id="divFaltantesLogicos">
                    </div>
                </td>
            </tr>
        </table>       
    </div>
    </form>
</body>
</html>
