<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_site.Master" AutoEventWireup="true" CodeBehind="restore_password.aspx.cs" Inherits="WebSantander.site.account.restore_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            Frm = {
                CurrentPage: "login.aspx",

                Init: function () {
                },

                NewPasswordTextBox_KeyDown: function (event) {
                    if (event.keyCode == 13) {
                        document.getElementById("ConfirmPasswordTextBox").focus();
                        document.getElementById("ConfirmPasswordTextBox").select();
                    }

                },

                ConfirmPasswordTextBox_KeyDown: function (event) {
                    if (event.keyCode == 13 && !document.getElementById("ChangePasswordLinkButton").disabled)
                        document.getElementById("ChangePasswordLinkButton").click();

                }
            };

            function ValidateSubmit() {
                parent.Site.ShowProcess();
                return true;
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">     
    <div class="framechild" style="overflow: hidden">
        <img src="<%=ResolveClientUrl("~/images/basic/sol_soluciones.png")%>" alt="" class="sol_soluciones" />
        <div class="accountInfo">
            <table>
                <tr>
                    <td class="center">
                        <h2>
                            Restablecer Contraseña
                        </h2>
                        <p>
                            Especifique su nombre de usuario.
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="login">
                            <legend>Contraseñas</legend>
                            <asp:ValidationSummary ID="LoginValidationSummary" CssClass="error_message" runat="server" ValidationGroup="LoginUserValidationGroup" />                            
                            <p>
                                <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPasswordTextBox">Nueva Contraseña:</asp:Label>
                                <asp:TextBox ID="NewPasswordTextBox" runat="server" CssClass="readonly" TextMode="Password" onkeydown="Frm.NewPasswordTextBox_KeyDown(event);"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPasswordTextBox">Confirmar Contraseña:</asp:Label>
                                <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" CssClass="readonly" TextMode="Password" onkeydown="Frm.ConfirmPasswordTextBox_KeyDown(event);"></asp:TextBox>
                                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ErrorMessage="Las contraseñas deben ser iguales" CssClass="error_message" ToolTip="Las contraseñas deben ser iguales." ValidationGroup="LoginUserValidationGroup" ControlToValidate="NewPasswordTextBox"
                                    ControlToCompare="ConfirmPasswordTextBox">*</asp:CompareValidator>
                            </p>
                        </fieldset>
                        <table style="border: 0"  width="100%">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="CancelarLinkButton" runat="server" CommandName="Cancelar" CssClass="button" PostBackUrl="~/site/account/login.aspx">Regresar</asp:LinkButton>
                                </td>
                                <td></td>
                                <td style="text-align: right">
                                    <asp:LinkButton ID="RestablecerLinkButton" runat="server" CommandName="Restablecer" CssClass="button" ValidationGroup="LoginUserValidationGroup">Restablecer contraseña</asp:LinkButton>
                                </td>
                            </tr>
                        </table>                        
                    </td>
                </tr>
            </table>            
            <img src="../../images/basic/key_accept.png" alt="" class="llave" />
        </div>
    </div>
</asp:Content>
