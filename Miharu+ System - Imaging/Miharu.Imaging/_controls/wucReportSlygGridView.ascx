<%@ Control Language="vb" AutoEventWireup="True" CodeBehind="wucReportSlygGridView.ascx.vb" Inherits="Miharu.Imaging.wucReportSlygGridView" EnableViewState="True" ViewStateMode="Enabled" %>


<%@ Register assembly="Miharu.Web.Controls" namespace="Miharu.Web.Controls" tagprefix="miharu" %>


<asp:Panel ID="Panel1" runat="server" Width="750px" >
<div style="height:inherit; width:inherit; overflow:auto;">

        <asp:Label ID="nombreReporteLabel" runat="server" Text="Reporte: " 
            style="text-align: left" Font-Bold="True"></asp:Label>
            &nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="EnvioCorreoImageButton" runat="server" Width="100px" 
            ImageUrl="~/_images/menu/EnviarCorreo.png" Height="23px" Visible="False" />
            &nbsp;
        <asp:CheckBox ID="EncabezadoCheckBox" runat="server" Text="Maneja Encabezado" 
            style="font-family: Calibri; font-size: small; font-weight: 700" 
            checked= "true" AutoPostBack="True" CausesValidation="True"/>
            &nbsp;
        <asp:DropDownList ID="SeparadorDropDownList" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <asp:DropDownList ID="FormatoDropDownList" runat="server" AutoPostBack="True" 
            CausesValidation="True">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="ExportarImageButton" runat="server" 
        style="text-align: right; margin-right: 0px" Width="81px" 
            ImageUrl="~/_images/menu/Exportar.png" Height="25px" />
        <miharu:SlygGridView ID="ResultadoReporteSlygGridViewControl" runat="server" 
            AllowPaging="True" BorderStyle="Solid" EnableSort="False">
        </miharu:SlygGridView> 
    
    </div>
    <asp:Label ID="MensajeLabel" runat="server" Text="." 
        style="font-size: xx-small; font-weight: 700"></asp:Label>    
</asp:Panel>


