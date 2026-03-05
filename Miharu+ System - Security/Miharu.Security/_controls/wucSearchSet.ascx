<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wucSearchSet.ascx.vb"
    Inherits="Miharu.Security._controls.wucSearchSet" %>
<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="miharu" %>
<link href="../../../_styles/gridview/GridviewStyles.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
   function searchWord(valor){
    var x= valor;
    if(x.length >=3)
    {document.getElementById('<%=btn.ClientId%>').click();}
}
</script>

<style type="text/css">
    .style1
    {
        width: 325px;
    }
    .style2
    {
        width: 38px;
    }
    .style3
    {
        width: 325px;
    }
</style>

        <table style="width: 700px;" >
            <tr>
                <td align="left" class="style1" >
                    <asp:Label ID="lblSearch" runat="server" Text="Buscar" ForeColor="#006699" 
                Font-Size="X-Small"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtSearch" runat="server" 
                onkeypress="searchWord(this.value)" Height="16px"
                Width="150px"></asp:TextBox>
                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Size="XX-Small" ForeColor="#006699" 
                Text="(Nombre o Codigo)"></asp:Label>
                </td>
                <td class="style2" >
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" >
                    <asp:Label ID="lblNotificacion" runat="server" Font-Size="XX-Small" 
                ForeColor="#CC0000"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="style2" >
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" >
                    <asp:Button ID="btn" runat="server" Style="display: none" Text="Buscar">
                    </asp:Button>
                    <asp:LinkButton ID="lkbtnAll" runat="server" Font-Size="X-Small">Ver Todos</asp:LinkButton>
                </td>
                <td class="style2" >
                    &nbsp;</td>
                <td class="style3">
                    <asp:Label ID="lblAsignado" runat="server" Text="Registros asignados" ForeColor="#006699" 
                Font-Size="X-Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" >
         
                    <miharu:SlygGridView ID="gvSet" runat="server" AllowPaging="True" ClickAction="OnClickNoEvents"
                      CssClass="yui-datatable-theme" Font-Size="X-Small" PageSize="13" 
                        PreSelectedStyleCssClass="" >
                        <EditRowStyle CssClass="row-edit"></EditRowStyle>
                        <PagerStyle CssClass="pager-stl"></PagerStyle>
                        <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                        <SelectedRowStyle CssClass="row-Select"></SelectedRowStyle>
                        <PagerSettings PageButtonCount="20" />
                        <RowStyle CssClass="nor-data-row"></RowStyle>
                    </miharu:SlygGridView>
                </td>
                <td class="style2" >
                    <asp:Button ID="btn_IzqDer" runat="server" Text="&gt;" Width="30px" 
                ForeColor="#000066" ToolTip="Agregar Registro" />
                    <br />
                    <br />
                    <asp:Button ID="btnDerIzq" runat="server" Text="&lt;" Width="30px" 
                ForeColor="#000066" ToolTip="Quitar Registro"  />
                    <br />
                    <br />
                    <asp:Button ID="btn_all_IzqDer" runat="server" Text="&gt;&gt;" Width="30px" 
                ForeColor="#000066" ToolTip="Agregar Todos"  />
                    <br />
                    <br />
                    <asp:Button ID="btn_all_DerIzq" runat="server" Text="&lt;&lt;" Width="30px" 
                ForeColor="#000066" ToolTip="Quitar Todos" />
                </td>
                <td class="style3">
                    <miharu:SlygGridView ID="gvGet" runat="server" AllowPaging="True" ClickAction="OnClickNoEvents"
                CssClass="yui-datatable-theme" EnableSort="True" GridNum="0" PreSelectedIndex="-1"
                PreSelectedStyleCssClass="row-PreSelect" Font-Size="X-Small" PageSize="13">
                        <EditRowStyle CssClass="row-edit"></EditRowStyle>
                        <PagerStyle CssClass="pager-stl"></PagerStyle>
                        <AlternatingRowStyle CssClass="alt-data-row"></AlternatingRowStyle>
                        <SelectedRowStyle CssClass="row-Select"></SelectedRowStyle>
                        <PagerSettings PageButtonCount="20" />
                        <RowStyle CssClass="nor-data-row"></RowStyle>
                    </miharu:SlygGridView>
                </td>
            </tr>
        </table>
 

