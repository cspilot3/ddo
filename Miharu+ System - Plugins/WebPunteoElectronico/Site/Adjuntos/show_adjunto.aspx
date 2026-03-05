<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="show_adjunto.aspx.cs" Inherits="WebPunteoElectronico.Site.Adjuntos.show_adjunto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Exportar</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <link href="../../Styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
</head>
<body onload="Descargar();">
    <form id="objForm" runat="server">
    <div>
        <span class="Titulo_Principal">Exportar</span>
        <br />
        <br />
        <span class="Parrafo">Si la descarga no inicia en unos segundos,
            <br />
            haga click en el siguiente </span>
        <asp:LinkButton ID="lnkbDescarga" CssClass="Mensaje_Error" OnClick="lnkbDescarga_Click" runat="server">vínculo</asp:LinkButton>
    </div>

    <script type="text/javascript">
        setTimeout("CerrarVentana()", 80000);

        function Descargar() {
            document.getElementById('<%= lnkbDescarga.ClientID %>').click();
        }

        function CerrarVentana() {
            window.close();
        }
    
    </script>

    </form>
</body>
</html>