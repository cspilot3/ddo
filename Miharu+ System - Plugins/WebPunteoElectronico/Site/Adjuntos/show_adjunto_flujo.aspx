<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="show_adjunto_flujo.aspx.cs" Inherits="WebPunteoElectronico.Site.Adjuntos.show_adjunto_flujo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Exportar</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <link href="../../Styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    </head>
<body >
    <form id="objForm" runat="server">
    <div>
        <span class="Titulo_Principal">Exportar</span>
        <br />
        <br />
        
        <div id="AdjuntosTabla">
            <asp:GridView ID="FileFlujoGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Nombre_Flujo_File" HeaderText="Nombre_Flujo_File" />
                    <asp:HyperLinkField DataNavigateUrlFields="URL" Text="Descargar" Target="_blank" />
                </Columns>
            </asp:GridView>
        </div>

    </div>

    

    </form>
</body>
</html>