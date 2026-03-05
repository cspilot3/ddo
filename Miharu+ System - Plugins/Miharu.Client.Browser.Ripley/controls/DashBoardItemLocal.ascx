<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardItemLocal.ascx.cs" Inherits="Miharu.Client.Browser.controls.DashBoardItemLocal" %>
<div class="DashBoardItem">
    <table title="<%=Tooltip %>">
        <tr>
            <td class="center">
                <img alt="" src="<%=ImageUrl %>" onclick="<%=OnClientClick %>" />
            </td>
        </tr>
        <tr>
            <td class="center">
                <a onclick="<%=OnClientClick %>">
                    <%=Title %>
                </a>
            </td>
        </tr>
    </table>
</div>
