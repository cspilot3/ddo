<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="p_Centros_Procesamiento.aspx.vb" Inherits="Miharu.Core.Sitio.Boveda.p_Centros_Procesamiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../_js/CmiGridView.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">
<asp:UpdatePanel ID="upCuerpo" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="2" width="90%">
                <tr>
                    <td align="left">
                        
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        Centros de Procesamiento disponibles:</td>
                </tr>
                <tr>
                    <td>
                        <cc1:CoreGridView ID="grdCentroProcesamiento" runat="server" AutoGenerateColumns="false">
                            <Columns>

                                <asp:BoundField DataField="id_Centro_Procesamiento" HeaderText="Id" />
                                <asp:BoundField DataField="Nombre_Centro_Procesamiento" HeaderText="Centro Procesamiento" />

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Enlazado" ItemStyle-Width="40">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Aplica" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </cc1:CoreGridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/_images/tool/save.png" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
