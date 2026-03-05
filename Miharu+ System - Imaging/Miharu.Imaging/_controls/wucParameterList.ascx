<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucParameterList.ascx.vb" Inherits="Miharu.Imaging.wucParameterList" %>

<link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />

<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">

<tr>
    <td>
    <asp:Label ID="Label" runat="server" Text="Label" CssClass="Label" Width="100px"  ></asp:Label>    &nbsp;
    </td>
    <td>
    <asp:DropDownList ID="valueDropDownList" runat="server" CssClass="TextBox" Width="250px" > </asp:DropDownList> &nbsp;
    </td>
    <td>
    <asp:CheckBox ID="nullCheckBox" runat="server" CssClass="CheckBox" Width="200px"  />
    </td>
</tr>


</asp:Panel>