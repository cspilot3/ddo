<%@ Page UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/MainMasterPage.Master" CodeBehind="Login.aspx.vb"
    Inherits="Miharu.Core.Main.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterLink" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterBody" runat="server">
    <link href="../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 585px; width: 100px">
                &nbsp;
            </td>
            <td align="center" valign="middle">
                <img alt="" src="../_images/MiharuCore.png" />
            </td>
            <td style="height: 525px; width: 200px">
                &nbsp;
            </td>
            <td align="left" valign="middle" style="width: 250px">
                <asp:UpdatePanel ID="upCuadros" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlBase" runat="server" DefaultButton="btnIngresar">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" style="height: 20px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="lblTitulo" runat="server" CssClass="Titulo_Principal">Inicio de Sesión</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblUsuario" runat="server" CssClass="Label" Height="13px" Width="90px">Usuario</asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="txtUsuario" runat="server" Style="margin-left: 0px; font-weight: 700;" Width="180px" CssClass="textboxlogin" EnableViewState="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblPwd" runat="server" CssClass="Label" Height="13px" Width="90px">Contraseña</asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="txtContraseña" runat="server" Width="180px" TextMode="Password" CssClass="textboxlogin"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px">
                                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUsuario" Display="None" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un nombre de usuario."
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxtoolkit:ValidatorCalloutExtender ID="rfvUser_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="rfvUser">
                                        </ajaxtoolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 20px;">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="upUpdatePassword" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlPassword" runat="server" Style="display: none" Width="220px">
                <table class="table_window" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="DialogBox_nw">
                        </td>
                        <td class="DialogBox_n">
                            <asp:Panel ID="pnlPasswordHead" runat="server">
                                <div class="title_window">
                                    Cambiar contraseña</div>
                            </asp:Panel>
                        </td>
                        <td class="DialogBox_close" onclick="hideModalPopupViaClient('<%= ModalPopupPassword.ClientID %>')">
                        </td>
                        <td class="DialogBox_ne">
                        </td>
                    </tr>
                </table>
                <table class="table_window" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="DialogBox_w">
                        </td>
                        <td valign="top" class="Content">
                            <table class="table_window" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 0px 0px 0px 10px">
                                        <asp:Label ID="lblPassword1" runat="server" Text="Contraseña" CssClass="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 0px 0px 0px 10px">
                                        <asp:TextBox ID="txtPassword1" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 0px 0px 0px 10px">
                                        <asp:Label ID="lblPassword2" runat="server" Text="Confirmar contraseña" CssClass="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 0px 0px 0px 10px">
                                        <asp:TextBox ID="txtPassword2" runat="server" Width="180px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px" align="right">
                                        <asp:Button ID="btnPasswordAceptar" runat="server" Text="Aceptar" />
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td style="width: 90px">
                                        <asp:Button ID="btnPasswordCancelar" runat="server" Text="Cancelar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 10px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="DialogBox_e">
                        </td>
                    </tr>
                </table>
                <table class="table_window" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="DialogBox_sw">
                        </td>
                        <td class="DialogBox_s">
                            <div style="width: 10px; height: 7px">
                            </div>
                        </td>
                        <td class="DialogBox_se">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxtoolkit:ModalPopupExtender ID="ModalPopupPassword" runat="server" TargetControlID="pnlPasswordHead" PopupControlID="pnlPassword" PopupDragHandleControlID="pnlPasswordHead"
                CancelControlID="btnPasswordCancelar" DropShadow="True" BackgroundCssClass="modalBackground">
            </ajaxtoolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
