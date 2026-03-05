<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master" 
    CodeBehind="reporte2.aspx.vb" Inherits="Miharu.Imaging.reporte2" Title="" Async="true"
    EnableViewState="True" ViewStateMode="Enabled"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>


<%@ Register src="../../_controls/wucReportSlygGridView.ascx" tagname="wucReportSlygGridView" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />
    <link href="../../_styles/StyleSheet_Reporte.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server"> 
    <script type="text/javascript">
    function OpenWindows(url) {
        window.open(url, " Título", "directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no");
    }
</script>
 <table style="width: 780px; " border="0" cellpadding="0" 
        cellspacing="0">
    <tr>
        <td class="LeftBorder"></td>
        <td class="CenterBorder">
                <asp:Panel ID="PanelTitulo" Style="background-color: #F0F0F0"
                    runat="server">
                    <strong>Parametros</strong>
                </asp:Panel>
        </td>
        <td class="style1" ></td>

    </tr>
    <tr>
        <td class="LeftBorder"></td>
        <td class="CenterBorder">
            <asp:Panel ID="PanelBotonesEjecucion" Style="background-color: #F0F0F0"
                    runat="server">
                <asp:ImageButton ID="ImageButton1" 
                runat="server" ImageUrl="~/_images/menu/run.png" 
                ToolTip="Ejecutar Consulta" style="text-align: left" />

                <!--<asp:ImageButton ID="ExportarMasivoButton" runat="server"
                ImageUrl="~/_images/menu/Exportar.png" ToolTip="Exportar Masivo" Width="79px" 
                    style="text-align: left" Visible="False" Height="27px" />-->
                    </asp:Panel>
        </td>
            <td class="RightBorder"></td>
    </tr>
    <tr>
        <td class="LeftBorder"></td>
        <td class="CenterBorder">
                <!--<div style="width:420px; height:200px; overflow:scroll; ">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"
                            ViewStateMode="Enabled" ></asp:PlaceHolder>
                        <br />
                        </div>-->
            <div style="width:780px; overflow:scroll;">
                <asp:Panel ID="PanelParametros" runat="server" Style="background-color: #F0F0F0"
                             ViewStateMode="Enabled">
                </asp:Panel>
            </div>
        </td>
        <td class="RightBorder"></td>
    </tr>
    <tr>
            <td class="LeftBorder">            
            </td>
            <td class="CenterBorder">
                    <asp:Panel ID="PanelFileUpload" runat="server" Style="background-color: #F0F0F0"
                    ViewStateMode="Enabled">
                    <asp:Button ID="Button1" runat="server" Text="Subir Archivo" CssClass="Button"/>
                    <asp:Label ID="LblFile" runat="server" Text="[Sin Archivo]" 
                        Font-Bold="True"></asp:Label>
                        </asp:Panel>                
             </td>
             <td class="RightBorder"></td>
    </tr>
    <tr> 
            <td class="LeftBorder"></td>
            <td class="CenterBorder">
                <div style="width:780px; overflow:scroll;">
                    <asp:Panel ID="Panel1" runat="server" Style="background-color: #F0F0F0"
                        ViewStateMode="Enabled">
                            <uc1:wucReportSlygGridView ID="wucReportSlygGridView1" runat="server" 
                                Visible="False" />
                    </asp:Panel>
                </div>
            </td>
            <td class="RightBorder"></td>
                
    </tr>
 </table>
</asp:Content>
