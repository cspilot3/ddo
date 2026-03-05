<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="WebReportViewerD.aspx.cs" Inherits="WebPunteoElectronico.Site.Reportes.WebReportViewerD" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
<script type="text/javascript">
    function Navegar(nUrl) {
        ShowProcess();
        window.location.href(nUrl);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">    
    <div style="position: absolute; top: 60px; right: 10px; left: 10px; bottom: 5px">
        <rsweb:ReportViewer ID="PageReportViewer" runat="server" Width="100%" Height="100%"
            HyperlinkTarget="" BorderWidth="1" BorderStyle="Solid" BorderColor="#999999">
            <LocalReport EnableHyperlinks="True">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>
