<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="web_report_viewer.aspx.cs"
    Inherits="Miharu.Client.Browser.site.informes.web_report_viewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            height:100%;
        }
    </style>
</head>
<body>
    <form id="ReportForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="position: absolute; z-index: 0; top: 10px; right: 10px; left: 10px; bottom: 5px">
        <rsweb:ReportViewer ID="PageReportViewer" runat="server" Width="100%" Height="100%"
            HyperlinkTarget="" BorderWidth="1" BorderStyle="Solid" BorderColor="#999999">
            <LocalReport >
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>

