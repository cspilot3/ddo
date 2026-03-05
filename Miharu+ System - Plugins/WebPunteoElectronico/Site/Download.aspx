<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="WebPunteoElectronico.Site.Download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjunto</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body onload="Init();">
    <form id="objForm" runat="server">
    <div style="width: 100%">
        <span class="t1" style="width: 100%">Visualizar adjunto</span>
        <br />
        <br />
        <span>Si la descarga no inicia en unos segundos,
            <br />
            haga click en el siguiente </span>
        <asp:LinkButton ID="DownloadLinkButton" runat="server">vínculo</asp:LinkButton>
    </div>
    <script type="text/javascript">
        setTimeout("window.close()", 10000);

        function Init() {
            if (window.opener != null) window.opener.Unlock();

            document.getElementById('<%= DownloadLinkButton.ClientID %>').click();
        }        
    </script>
    </form>
</body>
</html>
