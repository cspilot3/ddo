<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Miharu.Client.Browser.site.account.login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        Frm = {
            CurrentPage: "login.aspx",

            Init: function () {
            },

            UserNameTextBox_KeyDown: function (event) {
                if (event.keyCode == 13) {
                    document.getElementById("PasswordTextBox").focus();
                    document.getElementById("PasswordTextBox").select();
                }
            },

            PasswordTextBox_KeyDown: function (event) {
                if (event.keyCode == 13 && !document.getElementById("LoginLinkButton").disabled)
                    document.getElementById("LoginLinkButton").click();

            }
        };

        function ValidateSubmit() {
            parent.Site.ShowProcess();
            return true;
        }
    </script>
    
    <div class="framechild" style="overflow: hidden">
        <img src="<%=ResolveClientUrl("~/images/basic/sol_soluciones.png")%>" alt="" class="sol_soluciones" />
        <div class="accountInfo">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h2>
                            Iniciar sesión
                        </h2>
                        <p>
                            Especifique su nombre de usuario y contraseña.
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="login">
                            <legend>Información de cuenta</legend>
                            <asp:ValidationSummary ID="LoginValidationSummary" CssClass="error_message" runat="server" ValidationGroup="LoginUserValidationGroup" />
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserNameTextBox">Usuario:</asp:Label>
                                <asp:TextBox ID="UserNameTextBox" runat="server" Text="" CssClass="readonly"  onkeydown="Frm.UserNameTextBox_KeyDown(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserNameTextBox" CssClass="error_message" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox">Contraseña:</asp:Label>
                                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="readonly" onkeydown="Frm.PasswordTextBox_KeyDown(event);"></asp:TextBox>                                
                            </p>
                            <p>
                                 <asp:LinkButton ID="ForgottenLinkButton" runat="server" CommandName="Login"  PostBackUrl="~/site/account/forgotten_password.aspx">Ha olvidado su contraseña</asp:LinkButton>
                            </p>
                        </fieldset>
                        <p style="text-align: right">
                            <asp:LinkButton ID="LoginLinkButton" runat="server" CommandName="Login" CssClass="button" ValidationGroup="LoginUserValidationGroup">Iniciar sesión</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </table>
            <img src="../../images/basic/candado_login.png" alt="" class="candado" />
            <img src="../../images/basic/key_accept.png" alt="" class="llave" />
        </div>
    </div>
</asp:Content>
