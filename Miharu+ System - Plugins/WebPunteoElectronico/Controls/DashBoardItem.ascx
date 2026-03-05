<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardItem.ascx.cs"
    Inherits="WebPunteoElectronico.Controls.DashBoardItem" %>
<table class="DashBoardItem" cellpadding="0" cellspacing="0" title="<%=Tooltip %>">
    <tr>
        <td align="center">
            <asp:ImageButton ID="Icon_Image" runat="server" OnClick="Title_Label_Click" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:LinkButton ID="Title_Label" runat="server" OnClick="Title_Label_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
