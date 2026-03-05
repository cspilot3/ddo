<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterSite.Master" AutoEventWireup="true"
    CodeBehind="RestorePassword.aspx.cs" Inherits="Miharu.Security.WebService.DMZ.tools.RestorePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../_styles/site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var restablecerLinkButtonName = "<% =restablecerLinkButton.ClientID %>";

        function Deshabilitar() {
            document.getElementById(restablecerLinkButtonName).disabled = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="framechild" style="overflow: hidden">
        <div class="accountInfo">
            <table>
                <tr>
                    <td rowspan="2">
                        <img src="../_images/MiharuSecurityImage.png" alt="" />
                    </td>
                    <td class="center">
                        <h2>
                            Restablecer Contraseña
                        </h2>
                        <p>
                            <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>                        
                        <asp:Panel ID="formPanel" runat="server">
                            <fieldset class="login">
                                <legend>Contraseñas</legend>
                                <asp:ValidationSummary ID="loginValidationSummary" CssClass="error_message" runat="server"
                                    ValidationGroup="loginUserValidationGroup" />
                                <p>
                                    <asp:Label ID="newPasswordLabel" runat="server" AssociatedControlID="newPasswordTextBox">Nueva Contraseña:</asp:Label>
                                    <asp:TextBox ID="newPasswordTextBox" runat="server" CssClass="readonly" TextMode="Password"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="confirmPasswordLabel" runat="server" AssociatedControlID="confirmPasswordTextBox">Confirmar Contraseña:</asp:Label>
                                    <asp:TextBox ID="confirmPasswordTextBox" runat="server" CssClass="readonly" TextMode="Password"></asp:TextBox>
                                    <asp:CompareValidator ID="passwordCompareValidator" runat="server" ErrorMessage="Las contraseñas deben ser iguales"
                                        CssClass="error_message" ToolTip="Las contraseñas deben ser iguales." ValidationGroup="loginUserValidationGroup"
                                        ControlToValidate="newPasswordTextBox" ControlToCompare="confirmPasswordTextBox">*</asp:CompareValidator>
                                </p>
                            </fieldset>
                            <table style="border: 0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:LinkButton ID="restablecerLinkButton" runat="server" CommandName="Restablecer"
                                            CssClass="button" ValidationGroup="loginUserValidationGroup" OnClientClick="Deshabilitar();">Restablecer contraseña</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        var boton = document.getElementById(restablecerLinkButtonName);
        if (boton)
            boton.disabled = false;    
    </script>
</asp:Content>
