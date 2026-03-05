<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerTxLog.aspx.cs"
    Inherits="WebPunteoElectronico.Site.ConsultaFirmas.VerTxLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: Banco Agrario de Colombia ::</title>
</head>
<body>
    <form id="objForm" runat="server">
    <asp:Label ID="MensajeLabel" runat="server" Text=""></asp:Label>
    <asp:Panel ID="panelVisor" runat="server" ScrollBars="Auto" Width="990px" Height="630px">
        <asp:GridView ID="RegistroTx_GridView" runat="server" CellPadding="4" GridLines="None"
            ForeColor="#333333" Style="font-family: Arial, Helvetica, sans-serif; font-size: small"
            Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#339933" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
    </asp:Panel>
    </form>
</body>
</html>
