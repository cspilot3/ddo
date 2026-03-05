<%@ Page  UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Validacion_.aspx.vb" Inherits="Miharu.Core.Sitio.Administracion.Validacion" %>
<%@ Register Assembly="Miharu.Core" Namespace="Miharu.Core" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterHead" runat="server">
    <link href="../../_styles/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/ModalPopUp/StyleSheetModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Marco/StyleSheetMaster.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Tabpanel/TabpanelStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/Gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/default.css" rel="stylesheet" type="text/css" />
    <link href="../../_js/windows/themes/alphacube.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MasterBodyUnique" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
    <asp:Panel ID="pnlFiltro" runat="server" style="width: 100%;" Visible="true" >
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Entidad: " Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_EntidadFiltro" runat="server" Height="20px" Width="184px" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Proyecto: " Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_ProyectoFiltro" runat="server" Height="20px" Width="184px" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Esquema: " Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_EsquemaFiltro" runat="server" Height="20px" Width="184px" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Documento: " Width="200px"></asp:Label>
                    <asp:DropDownList ID="fk_DocumentoFiltro" runat="server" Height="20px" Width="184px" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br /><br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="true" OnEndPreSelect="OnPreselectMasterGrid">
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" style="width: 100%;" Visible="true" >
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Id Validacion" 
                    Width="200px"></asp:Label>
                <cc1:DNumber ID="id_Validacion" runat="server" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Entidad" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Entidad" runat="server" Height="20px" Width="184px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Proyecto" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Proyecto" runat="server" Height="20px" 
                    Width="184px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Esquema" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Esquema" runat="server" Height="20px" Width="184px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Documento" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Documento" runat="server" Height="20px" Width="184px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Pregunta" 
                    Width="200px"></asp:Label>
                <cc1:DTexto ID="Pregunta_Validacion" runat="server" IsRequiered="True" 
                    ValidationGroup="Guardar" Width="300px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Es Obligatorio" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Obligatorio" runat="server" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Es Subsanable" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Es_Subsanable" runat="server" />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Usa Motivo" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Usa_Motivo" runat="server" />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Lista Motivos" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Campo_Lista" runat="server" Height="20px" Width="184px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Categoria Validacion" 
                    Width="200px"></asp:Label>
                <asp:DropDownList ID="fk_Validacion_Categoria" runat="server" Height="20px" Width="184px">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Pregunta (Reporte)" 
                    Width="200px"></asp:Label>
                <cc1:DTexto ID="Pregunta_Validacion_Reporte" runat="server" IsRequiered="False" 
                    ValidationGroup="Guardar" Width="300px" />
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Eliminado" 
                    Width="200px"></asp:Label>
                <asp:CheckBox ID="Eliminado" runat="server" />
            </td>
        </tr>
        
        <tr>
            <td>
                <cc1:DTexto ID="fk_Usuario_Log" runat="server" IsRequiered="False" 
                    ValidationGroup="Guardar" Width="300px" Visible="False"/>
            </td>
                <td>
                    <cc1:DFecha ID="Fecha_Log" runat="server" IsRequiered="False" ValidationGroup="Guardar" Visible="False" />
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>
