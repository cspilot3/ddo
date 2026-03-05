<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterForm.master"
    CodeBehind="entidades.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.entidades" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="Miharu" %>
<%@ Register Src="../../../_controls/wucFilter.ascx" TagName="wucFilter" TagPrefix="uc1" %>
<%@ Register Src="../../../_controls/wucSearchSet.ascx" TagName="wucSearchSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_DialogBox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var BloqueActual = null;

        function SelectBloque(Bloque) {
            if (BloqueActual != null) {
                BloqueActual.style.border = "1px solid #000000";
            }
            BloqueActual = Bloque;
            BloqueActual.style.border = "2px solid #0000CC";
            document.getElementById("<%= SelectedBloque.ClientID %>").value = BloqueActual.title;

            //Visualizar opciones
            document.getElementById("<%= icbEdit.ClientID %>").style.display = 'inline';
            document.getElementById("<%= icbDelete.ClientID %>").style.display = 'inline';
            document.getElementById("<%= icbAddSubordinado.ClientID %>").style.display = 'inline';
            document.getElementById("<%= icbAddAsistencial.ClientID %>").style.display = 'inline';
        }
        function UnselectBloque() {
            if (BloqueActual != null) {
                BloqueActual.style.border = "1px solid #000000";
                BloqueActual = null
            }
            document.getElementById("<%= SelectedBloque.ClientID %>").value = "";

            //Ocultar opciones
            document.getElementById("<%= icbEdit.ClientID %>").style.display = 'none';
            document.getElementById("<%= icbDelete.ClientID %>").style.display = 'none';
            document.getElementById("<%= icbAddSubordinado.ClientID %>").style.display = 'none';
            document.getElementById("<%= icbAddAsistencial.ClientID %>").style.display = 'none';
        }

        function ShowOpciones(Modo) {
            document.getElementById("<%= divSave.ClientID %>").style.display = Modo;
            document.getElementById("<%= divDelete.ClientID %>").style.display = Modo;
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

        function GetScroll() {
            var CtlBase = document.getElementById("<%= pnlDependencia.ClientID %>");

            if (CtlBase != null) {
                ScrollY = document.getElementById("<%= pnlDependencia.ClientID %>").scrollTop;
                ScrollX = document.getElementById("<%= pnlDependencia.ClientID %>").scrollLeft;
            }
        }
        function SetScroll() {
            var CtlBase = document.getElementById("<%= pnlDependencia.ClientID %>");

            if (CtlBase != null) {
                CtlBase.scrollLeft = (ScrollX + CtlBase.clientWidth > CtlBase.scrollWidth) ? CtlBase.scrollWidth - CtlBase.clientWidth : ScrollX;
                CtlBase.scrollTop = (ScrollY + CtlBase.clientHeight > CtlBase.scrollHeight) ? CtlBase.scrollHeight - CtlBase.clientHeight : ScrollY;
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
                            <div id="divAdd" runat="server" style="width: 25px">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibAdd" runat="server" ImageUrl="~/_images/opciones/nuevo.png"
                                        ToolTip="Agregar un nuevo registro" />
                                </div>
                            </div>
                        </td>
                        <td style="width: 30px">
                            <div id="divDelete" runat="server" style="width: 25px; display: none">
                                <div class="BotonCambiante">
                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/_images/opciones/delete.png"
                                        ToolTip="Eliminar el registro actual" />
                                </div>
                                <ajaxToolkit:ConfirmButtonExtender ID="ibDelete_ConfirmButtonExtender" runat="server"
                                    ConfirmText="¿Confirma que desea eliminar el registro?" TargetControlID="ibDelete">
                                </ajaxToolkit:ConfirmButtonExtender>
                            </div>
                        </td>
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
                        <td align="right">
                            <asp:Label ID="lblTitulo" runat="server" Text="Entidades" CssClass="Titulo_Principal"></asp:Label>
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
                                                    <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
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
                            <ajaxToolkit:TabContainer ID="tcDetalle" runat="server" CssClass="ajax__tab_technorati-theme"
                                ActiveTabIndex="0" Width="750">
                                <ajaxToolkit:TabPanel runat="server" ID="tbGenerales" HeaderText="Generales">
                                    <ContentTemplate>
                                        <div style="margin: 5px; height: 400px; width: 735px;">
                                            <asp:Panel ID="pnlDetalle" runat="server" Visible="False">
                                                <table id="tblDetalle" cellspacing="0" cellpadding="0" border="0" style="margin-left: 20px">
                                                    <tr>
                                                        <td colspan="3" style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px">
                                                            <asp:Label ID="lblGrupo" runat="server" CssClass="Label">Grupo</asp:Label>
                                                        </td>
                                                        <td style="height: 14px">
                                                        </td>
                                                        <td style="height: 14px">
                                                            <asp:DropDownList ID="ddlGrupo" TabIndex="1" runat="server" CssClass="Textbox" Width="250px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 8px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblNombre" runat="server" CssClass="Label">Nombre</asp:Label>
                                                        </td>
                                                        <td style="width: 10px">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCodEntidad" runat="server" CssClass="Oculto"></asp:Label>
                                                            <asp:TextBox ID="txtNombre" TabIndex="2" runat="server" CssClass="Textbox" Width="400px"
                                                                MaxLength="100" />
                                                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="&lt;b&gt;Falta campo requerido&lt;/b&gt;&lt;br /&gt;Se requiere un Nombre de Entidad."
                                                                Display="None" ValidationGroup="Guardar" SetFocusOnError="True" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="rfvNombre_ValidatorCalloutExtender" runat="server"
                                                                TargetControlID="rfvNombre" Enabled="True">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkActivo" runat="server" CssClass="Label" Text="Activo" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 8px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px">
                                                            <asp:Label ID="lblCodigo" runat="server" CssClass="Label">Código</asp:Label>
                                                        </td>
                                                        <td style="height: 14px">
                                                        </td>
                                                        <td style="height: 14px">
                                                            <asp:TextBox ID="txtCodigo" TabIndex="3" runat="server" CssClass="Textbox" Width="250px"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 8px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px">
                                                            <asp:Label ID="lblNIT" runat="server" CssClass="Label">NIT</asp:Label>
                                                        </td>
                                                        <td style="height: 14px">
                                                        </td>
                                                        <td style="height: 14px">
                                                            <asp:TextBox ID="txtNIT" TabIndex="4" runat="server" CssClass="Textbox" Width="150px"
                                                                MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 8px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px">
                                                            <asp:Label ID="lblContacto" runat="server" CssClass="Label">Contacto</asp:Label>
                                                        </td>
                                                        <td style="height: 14px">
                                                        </td>
                                                        <td style="height: 14px">
                                                            <asp:TextBox ID="txtContacto" TabIndex="5" runat="server" CssClass="Textbox" Width="500px"
                                                                MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 8px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px">
                                                            <asp:Label ID="lblTelefono" runat="server" CssClass="Label">Teléfono</asp:Label>
                                                        </td>
                                                        <td style="height: 14px">
                                                        </td>
                                                        <td style="height: 14px">
                                                            <asp:TextBox ID="txtTelefono" TabIndex="6" runat="server" CssClass="Textbox" Width="200px"
                                                                MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 8px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px">
                                                            <asp:Label ID="lblImagen" runat="server" CssClass="Label">Imagen</asp:Label>
                                                        </td>
                                                        <td style="height: 14px">
                                                        </td>
                                                        <td style="height: 14px" align="right">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="imgLogo" runat="server" Width="200px" Height="40px" ToolTip="Imagen BMP de 200px X 40px que se usará como logo de la Entidad"
                                                                            BorderStyle="Solid" BorderWidth="1" BorderColor="#666666" />
                                                                    </td>
                                                                    <td style="width: 10px">
                                                                    </td>
                                                                    <td>
                                                                        <Miharu:ImageChangingButton ID="btnBuscarImagen" runat="server" ImageUrl="~/_images/opciones/search.png" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" ID="tbDependencias" HeaderText="Dependencias">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlDependencia" runat="server">
                                            <input id="SelectedBloque" type="hidden" value="" runat="server" />
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
                                            <asp:Panel ID="pnlDatos" runat="server" Style="display: none" Width="720px" DefaultButton="btnDatosAceptar">
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
                                                                    <td style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lblNombreDependencia" runat="server" Text="Nombre" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:TextBox ID="txtNombreDependencia" runat="server" Width="260px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:CheckBox ID="chk_ActivoDep" runat="server" CssClass="Label" Text="Activo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCodNodo" CssClass="Oculto" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lblCodDependencia" runat="server" Text="Código" CssClass="Label"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:TextBox ID="txtCodDependencia" runat="server" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lblDireccion" runat="server" CssClass="Label">Dirección</asp:Label>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:TextBox ID="txtDireccion" runat="server" Width="260px" MaxLength="100" />
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lblTelefonoDep" runat="server" CssClass="Label">Teléfono</asp:Label>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:TextBox ID="txtTelefonoDep" runat="server" Width="150px" MaxLength="30" />
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lblUbicacion" runat="server" CssClass="Label">Ubicación</asp:Label>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:TextBox ID="txtUbicacion" runat="server" Width="210px" MaxLength="50" />
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lblCentroCosto" runat="server" CssClass="Label">Centro Costo</asp:Label>
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:TextBox ID="txtCentroCosto" runat="server" Width="210px" MaxLength="50" />
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 0px 0px 0px 10px; width: 100px">
                                                                        <asp:Label ID="lbltitleDep" runat="server" CssClass="Label">Asignar Sedes</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding: 0px 0px 0px 10px;">
                                                                        <uc2:wucSearchSet ID="wucDependencias" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="height: 10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 50px">
                                                                        <asp:Label ID="lblwucSede" runat="server" CssClass="Oculto"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 200px" align="center">
                                                                        <asp:Button ID="btnDatosAceptar" runat="server" Text="Aceptar" />
                                                                    </td>
                                                                    <td style="width: 300px">
                                                                        <asp:Button ID="btnDatosCancelar" runat="server" Text="Cancelar" />
                                                                        <asp:Label ID="lblnewNodo" runat="server" CssClass="Oculto"></asp:Label>
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
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
