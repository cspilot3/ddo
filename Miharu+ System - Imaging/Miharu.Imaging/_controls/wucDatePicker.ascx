<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucDatePicker.ascx.vb" Inherits="Miharu.Imaging.wucDatePicker" ViewStateMode="Enabled" %>

<link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
<link href="../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />

<asp:Panel ID="Panel1" runat="server" EnableViewState=true >
<tr>
<table>
    <tr>
        <td>
            <asp:Label ID="_label" runat="server" CssClass="Label" Text="Label" 
                width="100px"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="_valueTextBox" runat="server" CssClass="Textbox" width="300px"></asp:TextBox>
        </td>
        <td>
            <asp:ImageButton ID="_ImageButton" runat="server" 
                ImageUrl="~/_images/menu/Calendario.png" style="width: 14px" width="100px" />
        </td>
        <td>
            <asp:CheckBox ID="_nullCheckBox" runat="server" CssClass="CheckBox" Text="Nulo" 
                width="200px" />
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Calendar ID="_calendar" runat="server" CssClass="Calendar" Visible="False" 
                width="100px"></asp:Calendar>
        </td>
    </tr>
    </tr>
</asp:Panel>
