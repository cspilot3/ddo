<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterWebUserControl.ascx.cs"
    Inherits="WebPunteoElectronico.Controls.FilterWebUserControl" %>
<table>
    <tr>
        <td>
            <asp:Button ID="NoneButton" runat="server" Width="70px" Height="20px" OnClick="NoneButton_Click"
                Text="Ninguno" />
        </td>
        <td>
            <asp:Button ID="AllButton" runat="server" Width="70px" Height="20px" OnClick="AllButton_Click"
                Text="Todos" />
        </td>
        <td>
            <asp:Button ID="ADButton" runat="server" Width="50px" Height="20px" OnClick="ADButton_Click"
                Text="A-D" />
        </td>
        <td>
            <asp:Button ID="EHButton" runat="server" Width="50px" Height="20px" OnClick="EHButton_Click"
                Text="E-H" />
        </td>
        <td>
            <asp:Button ID="ILButton" runat="server" Width="50px" Height="20px" OnClick="ILButton_Click"
                Text="I-L" />
        </td>
        <td>
            <asp:Button ID="MPButton" runat="server" Width="50px" Height="20px" OnClick="MPButton_Click"
                Text="M-P" />
        </td>
        <td>
            <asp:Button ID="QTButton" runat="server" Width="50px" Height="20px" OnClick="QTButton_Click"
                Text="R-T" />
        </td>
        <td>
            <asp:Button ID="UZButton" runat="server" Width="50px" Height="20px" OnClick="UZButton_Click"
                Text="U-Z" />
        </td>
    </tr>
</table>
