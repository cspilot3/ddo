<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucParameterNumeric.ascx.vb" Inherits="Miharu.Imaging.wucParameterNumeric" %>

<link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />

<asp:Panel ID="Panel1" runat="server">
<tr>
    <td>
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="Label"  Width="100px"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="valueTextBox" runat="server" CssClass="TextBox"  Width="300px"></asp:TextBox>
    </td>    
    <td>
    <asp:CheckBox ID="nullCheckBox" runat="server" CssClass="CheckBox"  Width="200px"/>
    </td>
</tr>
</asp:Panel>

