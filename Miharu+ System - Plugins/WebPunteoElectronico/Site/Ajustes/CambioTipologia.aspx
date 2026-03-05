<%@ Page Title="" Language="C#"  AutoEventWireup="true"
    CodeBehind="CambioTipologia.aspx.cs" Inherits="WebPunteoElectronico.Site.Ajustes.CambioTipologia" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cambio de tipologia</title>
    <link href="../../Styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="ErrorLabel" runat="server" Text="" CssClass="Error"></asp:Label>
        <asp:Label ID="MessageLabel" runat="server" Text="" CssClass="Label"></asp:Label>
        <asp:Panel ID="MainPanel" runat="server">
            <table cellpadding="0" cellspacing="0" border="0" width="550">
                <tr>
                    <td colspan="3" style="border: 1px solid #000080; background-color: #000080; text-align: center;">
                        <asp:Label ID="TituloLabel" runat="server" Text="Cambio de tipologia" CssClass="Titulo_Principal"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td class="Label1">
                        Esquema
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <asp:DropDownList ID="EsquemaDropDownList" runat="server" AutoPostBack="True" 
                            Width="400px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td class="Label1">
                        Tipologia
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <asp:DropDownList ID="TipologiaDropDownList" runat="server" Width="400px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="Button1" type="button" value="Cancelar" onclick="window.close();" />
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td align="right">
                        <asp:Button ID="AceptarButton" runat="server" Text="Aceptar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
