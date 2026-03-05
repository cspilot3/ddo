<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_site.Master" AutoEventWireup="true" CodeBehind="forgotten_password.aspx.cs" Inherits="Miharu.Client.Browser.site.account.forgotten_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        Frm = {
            CurrentPage: "forgotten_password.aspx",

            Init: function () {
            },

            UserNameTextBox_KeyDown: function (event) {
                if (event.keyCode == 13 && !document.getElementById("RestablecerLinkButton").disabled)
                    document.getElementById("RestablecerLinkButton").click();

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
                    <td style="text-align: center;">
                        <h2>Restablecer Contraseña
                        </h2>
                        <p>
                            Especifique su nombre de usuario.
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
                                <asp:TextBox ID="UserNameTextBox" runat="server" Text="" CssClass="readonly" onkeydown="Frm.UserNameTextBox_KeyDown(event);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserNameTextBox" CssClass="error_message" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                        </fieldset>
                        <table style="border: 0" width="100%">
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
        </div>
    </div>
</asp:Content>
