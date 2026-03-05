<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardItemLocal.ascx.cs"
    Inherits="WebPunteoElectronico.Controls.DashBoardItemLocal" %>
<table class="DashBoardItem" cellpadding="0" cellspacing="0" title="<%=Tooltip %>">
    <tr>
        <td align="center">
            <img alt="" src="<%=ImageUrl %>" onclick="<%=OnClientClick %>" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <a onclick="<%=OnClientClick %>">
                <%=Title %>
            </a>
        </td>
    </tr>
</table>
