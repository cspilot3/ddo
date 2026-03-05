<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerDetalleData.aspx.cs" Inherits="WebPunteoElectronico.Site.Consulta.VerDetalleData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: Banco Agrario de Colombia ::</title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="objForm" runat="server">
    <table align="center">
        <tr>
            <td>
                <div class="Titulo_Secundario" style="width: 900px; height: 30px;">
                    <h3>
                        <asp:Label ID="TipoDoc_Label" runat="server"></asp:Label>
                    </h3>
                </div>
                <br />
                <asp:Panel ID="panelVisor" runat="server" ScrollBars="Auto" Width="1000px" Height="200px"
                    BorderStyle="Solid" BorderWidth="1" BorderColor="Gray">
                    <asp:GridView ID="RegistroTx_GridView" runat="server" CellPadding="4" GridLines="None"
                        ForeColor="#333333" Style="font-family: Arial, Helvetica, sans-serif; font-size: 9px"
                        Width="100%">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#63AC29" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </asp:Panel>
                <div>
                    <asp:LinkButton ID="VerImagen_LinkButton" runat="server" 
                        OnClick="VerImagen_LinkButton_Click" Visible="False">Ver imagen</asp:LinkButton>
                </div>
                <div id="Imagen_Div" runat="server">
                    <iframe id="Imagen_Iframe" runat="server" width="1000px" height="600px"></iframe>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
