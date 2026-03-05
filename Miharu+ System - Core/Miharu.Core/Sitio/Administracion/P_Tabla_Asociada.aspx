<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/PopupMasterPage.Master" CodeBehind="P_Tabla_Asociada.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.P_Tabla_Asociada" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../_js/CmiGridView.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterContent" runat="server">

<asp:UpdatePanel ID="upCuerpo" runat="server">
    <ContentTemplate>

    <table>
        <tr>
            <td class="style1">
                <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="id_Campo_Tabla" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre_Campo" HeaderText="Nombre" />
                        <asp:BoundField DataField="Length_Campo" HeaderText="Longitud" />
                        <asp:BoundField DataField="Campo_Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Campo_Lita" HeaderText="Lista Asociada" />
                        <asp:BoundField DataField="Es_Campo_Busqueda" HeaderText="Es Campo Busqueda" />
                        <asp:BoundField DataField="Campo_Busqueda" HeaderText="Campo Busqueda" />
                        <asp:BoundField DataField="Es_Obligatorio_Campo" HeaderText="Obligatorio" />
                        <asp:BoundField DataField="Es_Exportable" HeaderText="Exportable" />
                        <asp:BoundField DataField="Eliminado_Campo" HeaderText="Inactivo" />
                    </Columns>
                </cc1:CoreGridView>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:ImageButton ID="NuevoImageButton" runat="server" 
                    ImageUrl="~/_images/tool/new.png" />
                <asp:ImageButton ID="GuardarImageButton" runat="server" 
                    ImageUrl="~/_images/tool/save.png" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Id Campo Tabla" 
                    Width="200px"></asp:Label>
                <cc1:DNumber ID="id_Campo_Tabla" runat="server" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Nombre Campo" 
                    Width="200px"></asp:Label>
                <cc1:DTexto ID="Nombre_Campo" runat="server" IsRequiered="True" 
                    ValidationGroup="Guardar" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Longitud Campo" 
                    Width="200px"></asp:Label>
                <cc1:DNumber ID="Length_Campo" runat="server" IsRequiered="True" MaxLength="3" 
                    ValidationGroup="Guardar" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Campo Tipo" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Campo_Tipo" runat="server" Height="20px" Width="184px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Campo Lista" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Campo_Lista" runat="server" Height="20px" 
                    Width="184px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label16" runat="server" CssClass="Label" Height="16px" 
                    Text="Campo Busqueda" Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Campo_Busqueda" runat="server" Height="20px" 
                    Width="184px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Obligatorio" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Es_Obligatorio_Campo" runat="server" CssClass="Label" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Exportable" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Es_Exportable" runat="server" CssClass="Label" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Inactivo" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Eliminado_Campo" runat="server" CssClass="Label" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>

    </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>
