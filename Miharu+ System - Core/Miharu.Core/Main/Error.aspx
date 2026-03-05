<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Error.aspx.vb" Inherits="Miharu.Core._Error" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%--<script runat="server">

    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
    '    System.Threading.Thread.Sleep(2000)
    '    If pnlError.Visible Then
    '        msgMas.Text = msgMas.Text.Replace("menos", "más")
    '        btnSubmit.ImageUrl = "../_images/basic/expand.jpg"
    '        pnlError.Visible = False
    '    Else
    '        msgMas.Text = msgMas.Text.Replace("más", "menos")
    '        btnSubmit.ImageUrl = "../_images/basic/collapse.jpg"
    '        pnlError.Visible = True
    '    End If
        
    'End Sub
</script>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <style type="text/css">
        .style6
        {
            width: 414px;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
    
    <asp:Panel ID="pnlUnique" runat="server" Width="100%" HorizontalAlign="Center" ScrollBars="Auto">
        <div style="width:100%;vertical-align:middle;">
            <asp:Label ID="lblMensajeError" runat="server" ForeColor="Silver" Font-Size="Large" />
            <br />
            <asp:Image ID="imgError" runat="server" ImageUrl="~/_images/basic/ImgError.jpg" />
            <br />
            <br />

                <%--ExpandedImage="~/images/collapse.jpg"
                ImageControlID="Image1"
                CollapsedImage="~/images/expand.jpg"--%>

            <table style="width:100%;">
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label2" runat="server" ForeColor="Silver" 
                            Text="Para visualizar más detalles del error, haga click: "></asp:Label>
                        <asp:ImageButton ID="btnSubmit" runat="server" 
                            ImageUrl="~/_images/basic/expand.jpg" Text="Button" />
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Panel ID="Detail" runat="server" Height="22px">
                            <asp:Label ID="MessageDetail" runat="server" Font-Size="Small" 
                                ForeColor="Silver" />
                        </asp:Panel>
                        <cc2:CollapsiblePanelExtender ID="cpe" runat="Server" AutoCollapse="False" 
                            AutoExpand="False" CollapseControlID="btnSubmit" Collapsed="True" 
                            CollapsedSize="0" CollapsedText="Ver Detalles..." 
                            ExpandControlID="btnSubmit" ExpandDirection="Vertical" ExpandedSize="300" 
                            ExpandedText="Ocultar Detalles" ScrollContents="false" TargetControlID="Detail" 
                            TextLabelID="Label2" />
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                </tr>
            </table>

            <br />
        </div>

    </asp:Panel>
</asp:Content>