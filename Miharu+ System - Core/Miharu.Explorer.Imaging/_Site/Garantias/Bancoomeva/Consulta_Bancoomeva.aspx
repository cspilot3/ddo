<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consulta_Bancoomeva.aspx.cs" Inherits="Miharu.Explorer.Imaging._Site.Garantias.Bancoomeva.Consulta_Bancoomeva" %>

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
    <script src="Consulta_Bancoomeva.js" type="text/javascript"></script>
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
        <asp:HiddenField ID="hdnllavesFolder" runat="server" />
        <table id="tableContent" style="width: 95%;">
            <tr>
                <td style="padding: 10px; width: 25%; vertical-align: top; border-color: #C5DBEC; border-right-style: solid;">
                    <table style="width: 100%">
                        <tr>
                            <td class="Titulo_Terceario">
                                Busqueda por llaves
                            </td>
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
                        <tr>
                            <td class="contentText">
                                Llave
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlProyectoLlave" runat="server" CssClass="control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="contentText">
                                Valor llave
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="txtllave" type="text" class="control" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                                <br />
                                <input id="Button1" type="button" value="Buscar" class="button" onclick="buscar()" />
                                <input id="Button2" type="button" value="Solicitar" class="button" onclick="getGestionSolicitud()" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 75%; vertical-align: top; padding-left: 10px;">
                    <table id="tblCarpetas">
                    </table>
                    <div id="divCarpetas">
                    </div>
                    <br />
                    <table id="tblDocumentos">
                    </table>
                    <div id="divDocumentos">
                    </div>
                </td>
            </tr>
        </table>
        <div id="DataAsociada" style="display: none;">
            <table id="tblDocumentosData">
            </table>
        </div>
        <div id="HistorialEstados" style="display: none;">
            <table id="tblHistorialEstados">
            </table>
        </div>
        <div id="HistorialSolicitud" style="display: none;">
            <table id="tblHistorialSolicitud">
            </table>
            <div id="divHistorialSolicitud">
            </div>
        </div>
        <div id="GestionSolicitud" style="display: none;">
            <div>
                <div id="tabs">
                    <ul>
                        <li><a href="#tabGestion">Solicitud</a></li>
                        <li><a href="#tabSolicitante">Destinatario</a></li>
                    </ul>
                    <div id="tabGestion">
                        <table>
                            <tr>
                                <td>
                                    Tipo Solicitud
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipo" runat="server" Width="200px" onchange="changeTipo()">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Motivo
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMotivo" runat="server" Width="200px" onchange="changeMotivo()">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Prioridad
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPrioridad" runat="server" Width="200px" onchange="changePrioridad()">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div style="width: 590px; height: 400px">
                            <table id="tblGestion">
                            </table>
                        </div>
                    </div>
                    <div id="tabSolicitante">
                        <input type="radio" name="usuario" id="usuario" value="1" onclick="MostrarUsuario(1)" />Usuario
                        <br />
                        <input type="radio" name="usuario" id="rdUsuarioEntidad" value="2" onclick="MostrarUsuario(2)" />Usuario registrado
                        <br />
                        <input type="radio" name="usuario" id="rdSolicitante" value="3" onclick="MostrarUsuario(3)" />Usuario No registrado
                        <br />
                        <br />
                        <br />
                        <div id="divUsuarioEntidad" style="display: none">
                            <asp:DropDownList ID="ddlUsuarioEntidad" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div id="divSolicitante" style="display: none">
                            <table>
                                <tr>
                                    <td>
                                        Nombres
                                    </td>
                                    <td>
                                        <input id="txtNombre" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Apellidos
                                    </td>
                                    <td>
                                        <input id="txtApellido" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Identificación
                                    </td>
                                    <td>
                                        <input id="txtIdentificacion" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Entidad
                                    </td>
                                    <td>
                                        <input id="txtEntidad" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Direccion
                                    </td>
                                    <td>
                                        <input id="txtDireccion" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Departamento
                                    </td>
                                    <td>
                                        <input id="txtDepartamento" type="text" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ciudad
                                    </td>
                                    <td>
                                        <input id="txtCiudad" type="text" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <input id="btnAceptarSolicitud" type="button" class="button" value="Solicitar" onclick="GuardarSolicitud()" />
                    </div>
                </div>
            </div>
        </div>
        <div id="divImagen" style="display: none; width: 80%; height: 80%">
        </div>
    </div>
    </form>
</body>
</html>
