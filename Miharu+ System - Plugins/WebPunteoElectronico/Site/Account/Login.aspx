<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Master/MasterForm.master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebPunteoElectronico.Site.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
<%--    <script type="text/javascript">
        if (document.addEventListener) {
            document.addEventListener("DOMContentLoaded", function () { document.getElementById('<%= UserNameTextBox.ClientID %>').value = 'eliseo.roa'; document.getElementById('<%= PasswordTextBox.ClientID %>').value = '123'; }, false);
        }
    </script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <div class="accountInfo">
        <table>
            <tr>
                <td colspan="2">
                    <center>
                        <h2>
                            Iniciar sesión
                        </h2>
                        <p>
                            Especifique su nombre de usuario y contraseña.
                        </p>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/basic/key-icon.png" />
                </td>
                <td>
                    <fieldset class="login">
                        <legend>Información de cuenta</legend>
                        <asp:ValidationSummary ID="LoginValidationSummary" CssClass="failureNotification"
                            runat="server" ValidationGroup="LoginUserValidationGroup" />
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserNameTextBox">Nombre de usuario:</asp:Label>
                            <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="textEntry" Text=""></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserNameTextBox"
                                CssClass="failureNotification" ErrorMessage="El nombre de usuario es obligatorio."
                                ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="PasswordTextBox">Contraseña:</asp:Label>
                            <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        </p>
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Iniciar sesión"
                            ValidationGroup="LoginUserValidationGroup" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
