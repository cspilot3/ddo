<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucFilter.ascx.vb"
    Inherits="Miharu.Imaging.wucFilter" %>
    
<link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
<table>
    <tr>
        <td>
            <asp:Button ID="btnNinguno" runat="server" CssClass="Button_Selected" Text="Ninguno"
                Width="70px" Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnTotos" runat="server" CssClass="Button" Text="Todos" Width="70px"
                Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnAD" runat="server" CssClass="Button" Text="A-D" Width="50px" Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnEH" runat="server" CssClass="Button" Text="E-H" Width="50px" Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnIL" runat="server" CssClass="Button" Text="I-L" Width="50px" Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnMP" runat="server" CssClass="Button" Text="M-P" Width="50px" Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnQT" runat="server" CssClass="Button" Text="R-T" Width="50px" Height="20px" />
        </td>
        <td>
            <asp:Button ID="btnUZ" runat="server" CssClass="Button" Text="U-Z" Width="50px" Height="20px" />
        </td>        
    </tr>
</table>
