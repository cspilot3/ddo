<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardItem.ascx.cs" Inherits="Miharu.Client.Browser.controls.DashBoardItem" %>
<div class="DashBoardItem">
    <table title="<%=Tooltip %>">
        <tr>
            <td class="center">
                <asp:ImageButton ID="Icon_Image" runat="server" OnClick="Title_Label_Click" />
            </td>
        </tr>
        <tr>
            <td class="center">
                <asp:LinkButton ID="Title_Label" runat="server" OnClick="Title_Label_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
