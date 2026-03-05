<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerTxLog.aspx.cs" Inherits="WebSantander.site.consulta.VerTxLog" %>

<%@ Register Assembly="Miharu.Client.Browser" Namespace="Miharu.Client.Browser.code.Grid"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../styles/Modal/modal.css" rel="Stylesheet" type="text/css" />
    <script runat="server">
        void RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = DatosEmbargo_GridView.Rows[index];
            string item, Id_Data;

            Label Id = (Label)row.FindControl("lblId");
            Id_Data = Id.Text.ToString();


            Label lblComando = (Label)row.FindControl("lbl" + e.CommandName.ToString());
            item = lblComando.Text.ToString();

            switch (e.CommandName)
            {
                case "Historial":
                    Dialogo_Resultados.Visible = true;
                    Historial.Visible = true;
                    Cambio_Estado.Visible = false;
                    Positivo.Visible = false;
                    ConsultarHistorial(2, long.Parse(item));
                    break;
                case "Imagen":
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Imagen", "window.open(\"Visor.aspx?token=" + item + " \", \"Historial\", \"width=900px,height=700px,resizable=no\");", true);
                    break;
                case "Factiva":
                    ValidarValor(Id_Data, item, 5);
                    break;
                case "Contraloria":
                    ValidarValor(Id_Data, item, 6);
                    break;
                case "Procuraduria":
                    ValidarValor(Id_Data, item, 7);
                    break;
            }
        }
    </script>
    <title>:: Santander ::</title>
    <style type="text/css">
        .style1
        {
        }
    </style>
</head>
<body>
    <form id="objForm" runat="server">
    <asp:Label ID="MensajeLabel" runat="server" Text=""></asp:Label>
    <asp:GridView ID="DatosEmbargo_GridView" runat="server" Style="font-family: Arial, Helvetica, sans-serif;
        font-size: small" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966"
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        OnRowCommand="RowCommand" >
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
            <asp:BoundField DataField="Tipo_Identificacion" HeaderText="Tipo Identificacion" />
            <asp:BoundField DataField="Numero_Identificacion" HeaderText="Numero Identificacion" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:ButtonField CommandName="Imagen" HeaderText="Imagen" Text="Imagen" />
            <asp:ButtonField CommandName="Historial" HeaderText="Historial" Text="Historial" />
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'>
                    </asp:Label>
                    <asp:Label ID="lblFactiva" runat="server" Text='<%# Bind("Validacion_Factiva") %>'>
                    </asp:Label>
                    <asp:Label ID="lblContraloria" runat="server" Text='<%# Bind("Validacion_Contraloria") %>'>
                    </asp:Label>
                    <asp:Label ID="lblProcuraduria" runat="server" Text='<%# Bind("Validacion_Procuraduria") %>'>
                    </asp:Label>
                    <asp:Label ID="lblHistorial" runat="server" Text='<%# Bind("Id") %>'>
                    </asp:Label>
                    <asp:Label ID="lblImagen" runat="server" Text='<%# Bind("Imagen") %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Factiva" DataTextField="Validacion_Factiva" HeaderText="Validacion Factiva"
                Text="Button" />
            <asp:ButtonField CommandName="Contraloria" DataTextField="Validacion_Contraloria"
                HeaderText="Validacion Contraloria" Text="Button" />
            <asp:ButtonField CommandName="Procuraduria" DataTextField="Validacion_Procuraduria"
                HeaderText="Validacion Procuraduria" Text="Button" />
            <asp:BoundField DataField="Cliente" HeaderText="Es Cliente" />
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#330099" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <SortedAscendingCellStyle BackColor="#FEFCEB" />
        <SortedAscendingHeaderStyle BackColor="#AF0101" />
        <SortedDescendingCellStyle BackColor="#F6F0C0" />
        <SortedDescendingHeaderStyle BackColor="#7E0000" />
    </asp:GridView>
    <table id="Dialogo_Resultados" runat="server" visible="False" class="Dialogo_Capa">
        <tr>
            <td id="Historial" runat="server" align="center" valign="middle">
                <div class="Dialogo_Contenedor">
                    <div class="Dialogo_Titulo">
                        <table class="Tbl">
                            <tr>
                                <td align="left">
                                    <asp:ImageButton ID="ImbCerrarHistorial" runat="server" ToolTip="Cerrar Dialogo"
                                        ImageUrl="~/styles/Modal/Dialogo_Cerrar.png" OnClick="ImbCerrarCambioEstado_Click" />&nbsp;&nbsp;
                                    Historial
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Dialogo_Contenido">
                        <table style="width: 100%;">
                            <tr align="center">
                                <td align="center">
                                    <asp:GridView ID="GridHistorial" runat="server" BackColor="White" BorderColor="#CC9966"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#330099" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td id="Cambio_Estado" runat="server" align="center" valign="middle">
                <div class="Dialogo_Contenedor">
                    <div class="Dialogo_Titulo">
                        <table class="Tbl">
                            <tr>
                                <td align="left">
                                    <asp:ImageButton ID="ImbCerrarCambioEstado" runat="server" ToolTip="Cerrar Dialogo"
                                        ImageUrl="~/styles/Modal/Dialogo_Cerrar.png" OnClick="ImbCerrarCambioEstado_Click" />&nbsp;&nbsp;
                                    Cambiar Estado
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Dialogo_Contenido">
                        <br />
                        <table style="width: 100%;">
                            <tr align="left">
                                <td class="style1">
                                    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td class="style1">
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                                    &nbsp;
                                    <asp:DropDownList ID="ddlEstado" runat="server">
                                        <asp:ListItem>-- Seleccione --</asp:ListItem>
                                        <asp:ListItem>POSITIVO</asp:ListItem>
                                        <asp:ListItem>NEGATIVO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr align="left">
                                <td class="style1">
                                    <asp:Label ID="lblArchivo" runat="server" Text="Soporte"></asp:Label>
                                    <asp:FileUpload ID="fileArchivo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Button ID="btnCambioEstado" runat="server" Text="Cambiar Estado" OnClick="btnCambioEstado_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </div>
            </td>
            <td runat="server" id="Positivo" align="center" valign="middle">
                <div class="Dialogo_Contenedor">
                    <div class="Dialogo_Titulo">
                        <table class="Tbl">
                            <tr>
                                <td align="left">
                                    <asp:ImageButton ID="imbCerrarPositivo" runat="server" ToolTip="Cerrar Dialogo" ImageUrl="~/styles/Modal/Dialogo_Cerrar.png"
                                        OnClick="ImbCerrarCambioEstado_Click" />&nbsp;&nbsp; Cambiar Estado
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Dialogo_Contenido">
                        <br />
                        <table style="width: 100%;">
                            <tr align="left">
                                <td class="style1">
                                    <asp:Label ID="lblData" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td class="style1">
                                    <asp:Label ID="lblNegativo" runat="server" Visible="False">El estado seleccionado no puede ser cambiado.</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlPositivo" runat="server" Visible="false">
                                        <table style="width:100%;" >
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblPositivo" runat="server" 
                                                        Text="¿Desea cambiar el estado del registro seleccionado a negativo?" 
                                                        Visible="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSi" runat="server" Text="Si" 
                                                        Visible="True" onclick="btnSi_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnNo" runat="server" Text="No" Visible="True" onclick="btnNo_Click" 
                                                        />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    &nbsp;
                                    </td>
                            </tr>
                            </table>
                        <br />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
