<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="descargar.aspx.vb" Inherits="Miharu.Imaging.WebService.descargar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjunto</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <link href="_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Desbloquear() {
            if (window.opener != null && window.opener.Bloquear_no != null)
                window.opener.Bloquear_no();
        }
    </script>
</head>
<body onload="Desbloquear();Descargar();">
    <form id="objForm" runat="server">
    <div>
        <span class="Titulo_Principal">Visualizar adjunto</span>
        <br />
        <asp:Label ID="ErrorLabel" runat="server" CssClass="Mensaje_Error" Text=""></asp:Label>
        <br />
        <span class="Parrafo">Si la descarga no inicia en unos segundos,
            <br />
            haga click en el siguiente </span>
        <asp:LinkButton ID="DescargarLinkButton" CssClass="Mensaje_Error" runat="server">vínculo</asp:LinkButton>
    </div>
    <script type="text/javascript">
        setTimeout("window.close()", 10000);

        function Descargar() {
            document.getElementById('<%= DescargarLinkButton.ClientID %>').click();
        }
    
    </script>
    </form>
</body>
</html>
