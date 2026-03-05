<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterSite.Master" AutoEventWireup="true"
    CodeBehind="ForgottenPassword.aspx.cs" Inherits="Miharu.Security.WebService.DMZ.tools.ForgottenPassword" %>

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
                            <asp:Label ID="okMessageLabel" runat="server" Text="Especifique su nombre de usuario."></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="messageLabel" runat="server" Text="" CssClass="error_message"></asp:Label>
                        <asp:Panel ID="formPanel" runat="server">
                            <fieldset class="login">
                                <legend>Información de cuenta</legend>
                                <asp:ValidationSummary ID="loginValidationSummary" CssClass="error_message" runat="server"
                                    ValidationGroup="loginUserValidationGroup" />
                                <p>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserNameTextBox">Usuario:</asp:Label>
                                    <asp:TextBox ID="UserNameTextBox" runat="server" Text="" CssClass="readonly"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserNameTextBox"
                                        CssClass="error_message" ErrorMessage="El nombre de usuario es obligatorio."
                                        ToolTip="El nombre de usuario es obligatorio." ValidationGroup="loginUserValidationGroup">*</asp:RequiredFieldValidator>
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
