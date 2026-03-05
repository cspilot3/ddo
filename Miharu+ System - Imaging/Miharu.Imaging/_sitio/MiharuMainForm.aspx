<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterPage.master"
    CodeBehind="MiharuMainForm.aspx.vb" Inherits="Miharu.Imaging.MiharuMainForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="../_controls/wucMenu.ascx" TagName="wucMenu" TagPrefix="miharu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">

    <script type="text/javascript">
        function SubformCargado()
        {
            try { document.getElementById('div_Cargando').style.display = 'none'; }catch(ex){}
            try { document.getElementById('ifPagina').style.display = 'inline'; }catch(ex){}
        }
    </script>

    <table cellspacing="0" cellpadding="0" border="0" style="width: 1000px;">
        <tr>
            <td style="width: 200px; height: 585px" valign="top">
                <asp:UpdatePanel ID="upMenu" runat="server">
                    <ContentTemplate>
                        <miharu:wucMenu ID="objMenu" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 5px">
            </td>
            <td valign="top" style="width: 795px; height: 585px" align="left">
                <asp:UpdatePanel ID="upCuerpo" runat="server">
                    <ContentTemplate>
                        <div id='div_Cargando' class="Cargando_div">
                            <table align="center" class="Cargando_tabla">
                                <tr>
                                    <td valign='middle' align='center'>
                                        <img src="<%= ResolveClientUrl("~/_images/basic/ajax-loader.gif") %>" alt="" ondblclick="SubformCargado();" />
                                    </td>
                                    <td style="width: 200px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <iframe id="ifPagina" scrolling="no" marginheight="0" marginwidth="0" frameborder="0"
                            height="580" width="795" style="background-color: #FFFFFF; display: none;" src="<%= ResolveClientUrl(MySesion.Pagina.PageDir) %>">
                        </iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
