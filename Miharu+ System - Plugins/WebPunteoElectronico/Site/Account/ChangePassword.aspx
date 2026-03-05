<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="WebPunteoElectronico.Site.Account.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="accountInfo">
        <table>
            <tr>
                <td colspan="2">
                    <center>
                        <h2>
                            Cambiar contraseña
                        </h2>
                        <p>
                            Ingrese la nueva contraseña.
                        </p>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/basic/keys-icon.png" />
                </td>
                <td>
                    <fieldset class="login">
                        <legend>Información de cuenta</legend>
                        <asp:ValidationSummary ID="LoginValidationSummary" CssClass="failureNotification"
                            runat="server" ValidationGroup="ChangePasswordValidationGroup" />
                        <p>
                            <asp:Label ID="OldPasswordLabel" runat="server" AssociatedControlID="OldPasswordTextBox">Contraseña anterior:</asp:Label>
                            <asp:TextBox ID="OldPasswordTextBox" runat="server" CssClass="textEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="OldPasswordRequired" runat="server" ControlToValidate="OldPasswordTextBox"
                                CssClass="failureNotification" ErrorMessage="La contraseña anterior es obligatoria."
                                ToolTip="La contraseña anterior es obligatoria." ValidationGroup="ChangePasswordValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPasswordTextBox">Nueva contraseña:</asp:Label>
                            <asp:TextBox ID="NewPasswordTextBox" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPasswordTextBox"
                                CssClass="failureNotification" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                ValidationGroup="ChangePasswordValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPasswordTextBox">Confirmar contraseña:</asp:Label>
                            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" CssClass="passwordEntry"
                                TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ID="ConfirmPasswordCompare" runat="server" CssClass="failureNotification"
                                ErrorMessage="Las contraseñas ingresadas no coinciden" ToolTip="Las contraseñas ingresadas no coinciden"
                                ControlToValidate="NewPasswordTextBox" ControlToCompare="ConfirmPasswordTextBox"
                                EnableClientScript="False" Type="String" ValidationGroup="ChangePasswordValidationGroup">*</asp:CompareValidator>
                        </p>
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="ChangePasswordButton" runat="server" CommandName="Login" Text="Cambiar contraseña"
                            ValidationGroup="ChangePasswordValidationGroup" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
