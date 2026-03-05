<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MessageBoxTemplate.ascx.vb" Inherits="Miharu.Core.MessageBoxTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<ajaxToolkit:ModalPopupExtender ID="MsgBoxPopUp" runat="server" PopupControlID="pnlPopUp"
    PopupDragHandleControlID="pnlPopUpBarra" DropShadow="True" 
    TargetControlID="pnlPopUpBarra" BackgroundCssClass="modalBackground" OkControlID="OkMessageButtonCliente33"
    CancelControlID="CancelMessageButtonCliente">
</ajaxToolkit:ModalPopupExtender>

<asp:Panel runat="server" ID="pnlPopUp" CssClass="modalPopup" Style="display: none"
    DefaultButton="OkMessageButtonCliente33">
    <table style='width: 350px;' border='0' cellpadding='0' cellspacing='0'>
        <tr style='width: 100%;'>
            <td colspan='5' style='width: 100%;'>
                <div class='modalBarra' style='width: 100%;'>
                    <table style="width: 100%;" border="0" cellpadding='0' cellspacing='0'>
                        <tr style='width: 100%;'>
                            <td style='height: 3px' colspan='3' />
                        </tr>
                        <tr style='width: 100%;'>
                            <td style='width: 10px'>
                            </td>
                            <td align='left' valign='middle' style='width: 100%;'>
                                <asp:Panel ID="pnlPopUpBarra" runat="server" Height="15px">
                                    <asp:Label ID="MsgBoxTitulo" runat="server" Text="Titulo"></asp:Label>
                                </asp:Panel>
                            </td>
                            <td style='width: 30px;' align="left" dir="ltr">
                                <table border="0" align="left">
                                    <tr>
                                        <td>
                                            <input runat="server" type="image" name="CancelMessageButtonCliente" id="CancelMessageButtonCliente"
                                                src="~/_images/basic/icon-close.png" style="border-width: 0px; cursor: pointer" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr style='width: 100%'>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>
        <tr style='width: 100%'>
            <td style='width: 10px'>
            </td>
            <td style='width: 100%'>
                <asp:Label ID="MsgBoxMensaje" runat="server" Text="Mensaje"></asp:Label>
            </td>
            <td style='width: 5px'>
            </td>
            <td align='right' valign='middle' style='width: 50px'>
                <asp:Image ID="MsgBoxIcono" runat="server" ImageUrl="" />
            </td>
            <td style='width: 10px'>
            </td>
        </tr>
        <tr style='width: 100%'>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>
        <tr style='width: 100%'>
            <td align='center' colspan='5'>
                <asp:Button ID="OkMessageButtonCliente33" runat="server" Text="Aceptar" />
            </td>
        </tr>
        <tr style='width: 100%'>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>
    </table>
</asp:Panel>