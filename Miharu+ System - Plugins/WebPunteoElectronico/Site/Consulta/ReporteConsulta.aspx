<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteConsulta.aspx.cs"
    Inherits="WebPunteoElectronico.Site.Consulta.ReporteConsulta" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; top: 10px; right: 10px; left: 10px; bottom: 5px">
        <rsweb:ReportViewer ID="rvUno" runat="server" Width="100%" Height="100%"
            HyperlinkTarget="" BorderWidth="1" BorderStyle="Solid" BorderColor="#999999">
            <LocalReport EnableHyperlinks="True">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
    <asp:ScriptManager ID="ScriptManager1" ScriptMode="Release" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
