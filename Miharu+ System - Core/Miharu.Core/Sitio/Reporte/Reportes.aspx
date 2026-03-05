<%@ Page UICulture="es" Culture="es-MX" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main/FormMasterPage.Master" CodeBehind="Reportes.aspx.vb"
    Inherits="Miharu.Core.Sitio.Reporte.Reportes" %>

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
        .style2
        {
            width: 216px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MasterFilter" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MasterGrid" runat="server">
    <asp:Panel ID="pnlGrilla" runat="server" Style="width: 100%;">
        <asp:Label ID="NumRegistros" runat="server" Text="Label" CssClass="Label"></asp:Label>
        <br />
        <br />
        <cc1:CoreGridView ID="grdData" runat="server" AutoGenerateColumns="true" OnEndPreSelect="OnPreselectMasterGrid">
        </cc1:CoreGridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MasterDetail" runat="server">
    <asp:Panel ID="panelDetalle" runat="server" Style="width: 100%;" Visible="true" ScrollBars="Auto">
        <table style="width: 100%;">
            <tr>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Id Reporte" Width="200px"></asp:Label>
                </td>
                <td>
                    <cc1:DNumber ID="id_Reporte" runat="server" Enabled="false" WaterText="Auto" />
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Parametros" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ParametrosImageButton" runat="server" ImageUrl="~/_images/basic/find.png" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Nombre" Width="200px"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Nombre_Reporte" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="" InvalidValueMessage="   *"
                        IsRequiered="True" MaxLength="100" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Descripcion" Width="200px"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Descripcion_Reporte" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="50px" InvalidValueMessage="   *"
                        IsRequiered="False" MaxLength="200" MensajeColor="Red" Multiline="MultiLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Categoria" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Categoria_Reporte" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Entidad" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Entidad" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Conexion" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Conexion" runat="server" Style="margin-right: 0" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Usa Archivo" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Usa_Archivo" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Maneja Encabezado" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Maneja_Encabezado" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Caracter de Separación" Width="200px"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Caracter_Separado" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="" InvalidValueMessage="   *"
                        IsRequiered="False" MaxLength="100" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Identificador de Texto" Width="200px"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Identificador_Texto" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="" InvalidValueMessage="   *"
                        IsRequiered="False" MaxLength="100" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Titulos Salida" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="SalidasImageButton" runat="server" ImageUrl="~/_images/basic/find.png" />
                </td>
            </tr>
            <tr>
                <td class="style2" style="vertical-align: top">
                    <asp:Label ID="Label25" runat="server" CssClass="Label" Text="Consulta" Width="200px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label28" runat="server" CssClass="Label" ForeColor="Maroon" Text="Recuerde que si cambia la consulta debera configurar nuevamente los parametros."
                        Width="160px"></asp:Label>
                </td>
                <td style="vertical-align: top">
                    <cc1:DTexto ID="Consulta" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="150px" InvalidValueMessage="   *"
                        IsRequiered="True" MaxLength="8000" MensajeColor="Red" Multiline="MultiLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Usa Consulta de Confirmación" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Usa_Consulta_Confirmacion" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td class="style2" style="vertical-align: top">
                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Consulta de Confirmación" Width="200px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label6" runat="server" CssClass="Label" ForeColor="Maroon" Text="Recuerde que la consulta de confirmación intentará utilizar los mismos parametros que la consulta principal, es necesario que los incluya."
                        Width="160px"></asp:Label>
                </td>
                <td style="vertical-align: top">
                    <cc1:DTexto ID="Consulta_Confirmacion" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="150px" InvalidValueMessage="   *"
                        IsRequiered="False" MaxLength="8000" MensajeColor="Red" Multiline="MultiLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Exportar a texto plano" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Exportar_Texto_Plano" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Fila Omitir" Width="200px"></asp:Label>
                </td>
                <td>
                    <cc1:DTexto ID="Filas_Omitir" runat="server" AutoPostBack="False" BackColor_="" CssClass_="" EmptyValueMessage="*" Heigth="" InvalidValueMessage="   *"
                        IsRequiered="False" MaxLength="100" MensajeColor="Red" Multiline="SingleLine" Text="" TooltipMessage="" ValidationGroup="Guardar" Width="400px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Usa Columnas ancho fijo" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Usa_Columnas_Ancho_Fijo" CssClass="Label" AutoPostBack="True" />
                    <asp:ImageButton ID="ImgParametrosColumna" runat="server" ImageUrl="~/_images/basic/find.png" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Formato" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_Formato" runat="server" Style="margin-right: 0" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Usa encabezados" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Maneja_encabezados" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Notificacion" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="fk_notificacion" runat="server" Style="margin-right: 0" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Genera ZIP archivo" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="Genera_ZIP_Reporte" CssClass="Label" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Asignar Contraseña" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="AsignarClaveReporte" runat="server" ImageUrl="~/_images/basic/key.gif" />
                </td>
            </tr>

        </table>
    </asp:Panel>
</asp:Content>
