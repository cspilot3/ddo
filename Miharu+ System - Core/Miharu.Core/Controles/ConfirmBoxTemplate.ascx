<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ConfirmBoxTemplate.ascx.vb" Inherits="Miharu.Core.ConfirmBoxTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BehaviorID="mdlPopupConfirm"
    OnOkScript="okClick();" OnCancelScript="cancelClick();" TargetControlID="div_ModalConfirm"
    PopupControlID="div_ModalConfirm" OkControlID="btnOk" CancelControlID="btnNo"
    BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopUpBarra33" />

<div id="div_ModalConfirm" runat="server" align="center" class="modalPopup" style="display: none">
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
                                <asp:Panel ID="pnlPopUpBarra33" runat="server" Height="15px">
                                    <asp:Label ID="MsgBoxTitulo33" runat="server" Text="Titulo"></asp:Label>
                                </asp:Panel>
                            </td>
                            <td style='width: 10px'>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>
        <tr>
            <td style='width: 10px'>
            </td>
            <td style='width: 100%'>
                <asp:Label ID="txtConfirmText" runat="server" Text="¿Esta seguro que desea realizar esta accion?"></asp:Label>
            </td>
            <td style='width: 5px'>
            </td>
            <td align='right' valign='middle' style='width: 50px'>
                <asp:Image ID="MsgBoxIcono" runat="server" ImageUrl="~/_images/basic/icon-warning.png" />
            </td>
            <td style='width: 10px'>
            </td>
        </tr>
        <tr>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>
        <tr>
            <td align='center' colspan='5'>
                <asp:Button ID="btnOk" runat="server" Text="Yes" Width="50px" />
                <asp:Button ID="btnNo" runat="server" Text="No" Width="50px" />
            </td>
        </tr>
        <tr>
            <td style='height: 10px' colspan='5'>
            </td>
        </tr>
    </table>
</div>