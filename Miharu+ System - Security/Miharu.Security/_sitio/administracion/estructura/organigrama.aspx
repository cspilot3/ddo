<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="organigrama.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.organigrama" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var BloqueActual = null;
        
        function SelectBloque(Bloque)
        {
            if(BloqueActual != null)
            {
                BloqueActual.style.border="1px solid #000000";
            }
            BloqueActual=Bloque;
            BloqueActual.style.border="2px solid #0000CC";
            document.getElementById("<%= SelectedBloque.ClientID %>").value=BloqueActual.title;
            
            //Visualizar opciones
            document.getElementById("<%= icbEdit.ClientID %>").style.display='inline';
            document.getElementById("<%= icbDelete.ClientID %>").style.display='inline';
            document.getElementById("<%= icbAddSubordinado.ClientID %>").style.display='inline';
            document.getElementById("<%= icbAddAsistencial.ClientID %>").style.display='inline';
        }
        function UnselectBloque()
        {
            if(BloqueActual != null)
            {
                BloqueActual.style.border="1px solid #000000";
                BloqueActual = null
            }            
            document.getElementById("<%= SelectedBloque.ClientID %>").value="";
            
            //Ocultar opciones
            document.getElementById("<%= icbEdit.ClientID %>").style.display='none';
            document.getElementById("<%= icbDelete.ClientID %>").style.display='none';            
            document.getElementById("<%= icbAddSubordinado.ClientID %>").style.display='none';
            document.getElementById("<%= icbAddAsistencial.ClientID %>").style.display='none';
        }
        
        function ShowOpciones(Modo)
        {
            document.getElementById("<%= divSave.ClientID %>").style.display=Modo;            
        }        
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Cuerpo" runat="server">
    <script type="text/javascript">
        var prm2 = Sys.WebForms.PageRequestManager.getInstance();
        var ScrollX = 0;
        var ScrollY = 0;
        
        prm2.add_beginRequest(GetScroll);
        prm2.add_pageLoaded(SetScroll);
        
        function GetScroll()
        {
            var CtlBase = document.getElementById("<%= pnlBase.ClientID %>");
            
            if(CtlBase != null)
            {
                ScrollY = document.getElementById("<%= pnlBase.ClientID %>").scrollTop;
                ScrollX = document.getElementById("<%= pnlBase.ClientID %>").scrollLeft;
            }
        }                        
        function SetScroll()
        {
            var CtlBase = document.getElementById("<%= pnlBase.ClientID %>");
            
            if(CtlBase != null)
            {
                CtlBase.scrollLeft = (ScrollX + CtlBase.clientWidth > CtlBase.scrollWidth)?CtlBase.scrollWidth-CtlBase.clientWidth:ScrollX;
                CtlBase.scrollTop = (ScrollY + CtlBase.clientHeight > CtlBase.scrollHeight)?CtlBase.scrollHeight-CtlBase.clientHeight:ScrollY;
            }
        }
    </script>

    <table style="width: 780px; height: 585px;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 760px;">
                    <tr>
                        <td style="width: 10px">
                            &#160;
                        </td>
                        <td style="width: 30px">
                            <div id="divSave" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibSave" runat="server" ImageUrl="~/_images/opciones/save.png"
                                        ToolTip="Guardar los cambios" ValidationGroup="Guardar" />
                                </div>
                            </div>
                        </td>
                        <td style="width: 30px">
                            &#160;
                        </td>
                        <td style="width: 30px">
                            &#160;
                        </td>
                        <td style="width: 10px">
                            &#160;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTitulo" runat="server" Text="Dependencias" CssClass="Titulo_Principal"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding: 0px 5px 5px 5px;" align="left">
                <ajaxToolkit:TabContainer ID="tcBase" runat="server" CssClass="ajax__tab_technorati-theme"
                    ActiveTabIndex="0" Width="770">
                    <ajaxToolkit:TabPanel runat="server" ID="tpConsulta">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 20px" onclick="ShowOpciones('none')">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgIcoConsulta" runat="server" ImageUrl="~/_images/opciones/consulta.png" />
                                    </td>
                                    <td style="width: 5px" />
                                    <td>
                                        <asp:Label ID="lblConsulta" runat="server" Text="Consulta"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="padding: 5px" cellspacing="5" border="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Nombre]" CssClass="Label"
                                            Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:wucFilter ID="ucFiltro" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="border: 1px solid #CCCCCC; overflow: auto; height: 430px; width: 735px">
                                            <Miharu:SlygGridView ID="gvBase" runat="server" AutoGenerateColumns="False" GridNum="0"
                                                CssClass="yui-datatable-theme" EnableSort="True" ClickAction="OnDblClickSelectedPostBack">
                                                <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="id_Entidad" HeaderText="Cod." ItemStyle-Width="1" />
                                                    <asp:BoundField DataField="Nombre_Entidad" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="NIT_Entidad" HeaderText="NIT" />
                                                </Columns>
                                                <EditRowStyle CssClass="row-edit"></EditRowStyle>
                                                <PagerStyle CssClass="pager-stl"></PagerStyle>
                                                <RowStyle CssClass="nor-data-row"></RowStyle>
                                                <SelectedRowStyle CssClass="row-edit"></SelectedRowStyle>
                                            </Miharu:SlygGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tpDetalle" runat="server">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 20px" onclick="ShowOpciones('inline')">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgIcoDetalle" runat="server" ImageUrl="~/_images/opciones/detalle.png" />
                                    </td>
                                    <td style="width: 5px" />
                                    <td>
                                        <asp:Label ID="lblDetalle" runat="server" Text="Detalle"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="margin: 5px; height: 475px; width: 737px;">
                                <asp:Panel ID="pnlDetalle" runat="server" Visible="False">
                                    <input id="SelectedBloque" type="hidden" value="" runat="server" />
                                    <asp:Label ID="lblCodEntidad" runat="server" CssClass="Oculto"></asp:Label>
                                    <table style="width: 727px;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td colspan="11" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px; height: 20px">
                                            </td>
                                            <td style="width: 20px">
                                                <Miharu:ImageChangingButton ID="icbEdit" runat="server" ImageUrl="~/_images/basic/editar.png"
                                                    ToolTip="Editar datos del nodo" CssClass="Oculto" EnableViewState="True" />
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td style="width: 20px">
                                                <Miharu:ImageChangingButton ID="icbDelete" runat="server" ImageUrl="~/_images/basic/eliminar.png"
                                                    ToolTip="Eliminar el nodo seleccionado" CssClass="Oculto" EnableViewState="True" />
                                                <ajaxToolkit:ConfirmButtonExtender ID="icbDelete_ConfirmButtonExtender" runat="server"
                                                    ConfirmText="¿Confirma que desea eliminar el Nodo seleccionado?" Enabled="True"
                                                    TargetControlID="icbDelete">
                                                </ajaxToolkit:ConfirmButtonExtender>
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                            <td style="width: 25px">
                                                <Miharu:ImageChangingButton ID="icbAddSubordinado" runat="server" ImageUrl="~/_images/basic/subordinado.png"
                                                    ToolTip="Agregar nodo subordinado" CssClass="Oculto" EnableViewState="True" />
                                            </td>
                                            <td style="width: 20px">
                                                <Miharu:ImageChangingButton ID="icbAddAsistencial" runat="server" ImageUrl="~/_images/basic/asistencial.png"
                                                    ToolTip="Agregar nodo asistencial" CssClass="Oculto" EnableViewState="True" />
                                            </td>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 590px">
                                                <asp:Label ID="lblNombreEntidad" runat="server" CssClass="Label">Entidad</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                <asp:Panel ID="pnlBase" runat="server" Width="740" Height="435" Style="overflow: auto;
                                                    position: relative; top: 0px; left: 0px;" BorderStyle="Inset" BorderWidth="2px"
                                                    EnableViewState="True">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlDatos" runat="server" Style="display: none" Width="320px" DefaultButton="btnDatosAceptar">
                                        <table class="table_window" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="DialogBox_nw">
                                                </td>
                                                <td class="DialogBox_n">
                                                    <asp:Panel ID="pnlDatosHead" runat="server">
                                                        <div class="title_window">
                                                            Dependencia</div>
                                                    </asp:Panel>
                                                </td>
                                                <td class="DialogBox_close" onclick="hideModalPopupViaClient('<%= ModalPopupDatos.ClientID %>')">
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
                                                                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                                                <asp:TextBox ID="txtNombre" runat="server" Width="280px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 10px">
                                                                <asp:Label ID="lblCodNodo" CssClass="Oculto" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                                                <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="padding: 0px 0px 0px 10px">
                                                                <asp:TextBox ID="txtCodigo" runat="server" Width="280px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 125px" align="right">
                                                                <asp:Button ID="btnDatosAceptar" runat="server" Text="Aceptar" />
                                                            </td>
                                                            <td style="width: 50px">
                                                            </td>
                                                            <td style="width: 125px">
                                                                <asp:Button ID="btnDatosCancelar" runat="server" Text="Cancelar" />
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
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupDatos" runat="server" TargetControlID="pnlDatosHead"
                                        PopupControlID="pnlDatos" PopupDragHandleControlID="pnlDatosHead" CancelControlID="btnDatosCancelar"
                                        DropShadow="True" BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
