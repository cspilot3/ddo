<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MiharuMasterPopUp.master"
    CodeBehind="p_dependencia_padre.aspx.vb" Inherits="Miharu.Security._sitio.administracion.estructura.p_dependencia_padre"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../_styles/StyleSheet_Marco.css" rel="stylesheet" type="text/css" />
    <link href="../../../_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    <asp:UpdatePanel ID="upCuerpo" runat="server">
        <ContentTemplate>
  <table border="0" cellpadding="0" cellspacing="0" style="margin: 10px 10px 10px 10px">    
        <tr>
            <td  valign="top">
              <div style="border: 1px solid #CCCCCC; overflow: auto; height: 230px; width: 500px">
                <asp:TreeView ID="tvDependencia" runat="server" ImageSet="Msdn" NodeIndent="10">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" Font-Underline="True" />
                    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                        Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                    <Nodes>
                    </Nodes>
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="1px" VerticalPadding="2px" />
                </asp:TreeView>
            </div> 
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td style="width: 90px" align="right">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td style="width: 90px">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

