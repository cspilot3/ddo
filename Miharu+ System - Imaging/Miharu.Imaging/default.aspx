<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Miharu.Imaging._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: MIHARU Imaging ::</title>

    <script type="text/javascript">
        //<!--
        var OpenChield = 0;
        var PaginaHija;

        function Iniciar_MIharu() {
            return;

            if (OpenChield == 0) {
                var height = 0;
                var width = 0;

                if (self.screen) // for NN4 and IE4
                {
                    width = screen.availWidth - 10;
                    height = screen.availHeight;
                }
                else if (self.java) // for NN3 with enabled Java
                {
                    var jkit = java.awt.Toolkit.getDefaultToolkit();
                    var scrsize = jkit.getScreenSize();

                    width = scrsize.width - 10;
                    height = scrsize.height - 75;
                }

                if (width > 0 && height > 0) {
                    PaginaHija = window.open("_sitio/login.aspx", "ImagingWindow", "width=" + width + "px,height=" + height + "px,top=0px,left=0px,toolbar=no,directories=no,menubar=no,status=yes,resizable=yes");
                }
                else {
                    PaginaHija = window.open("_sitio/login.aspx", "ImagingWindow", "width:1000px,height=740px,top=0px,left=0px,toolbar=no,directories=no,menubar=no,status=yes,resizable=yes");
                }

                if (PaginaHija == null) {
                    window.alert("Su explorador no permite el uso de ventanas emergentes, por favor habilite las ventanas emergentes para el sitio actual y vuelva a ingresar")
                }
                else {
                    OpenedChield();
                }
            }
            else {
                try {
                    PaginaHija.focus();
                }
                catch (ex) {
                    OpenChield = 0;
                }
            }
        }
        function OpenedChield() {
            OpenChield = 1;
        }
        function ClosedChield() {
            OpenChield = 0;
        }
        function Cerrar_Hija() {
            if (OpenChield == 1) {
                try { PaginaHija.close(); } catch (ex) { }

            }
        }
        //-->   
        
    </script>

</head>
<body onload="Iniciar_MIharu();" onunload="Cerrar_Hija();">
    <form id="objForm" runat="server">
    <div>
        <center>
            <br />
            <br />
            <asp:Label ID="lblTitulo" runat="server" Text="MIHARU Imaging" Style="font-size: xx-large;
                color: #003399; font-weight: bold"></asp:Label>
            <br />
            <br />
            <a title="Iniciar Miharu" onclick="Iniciar_MIharu();" style="cursor: pointer; text-decoration: none">
                <img alt="" src="_images/basic/MiharuImagingImage.png" />
            </a>
            <br />
            <br />
            <br />
           <%-- <a href="http://www.slyg.com.co" target="_blank" style="cursor: pointer; text-decoration: none">
                <img alt="" src="_images/basic/logoSLYG.png" style="border-style: none; border-width: 0px" /></a>--%>
            <br />
            <p style="font-size: small; color: #000080; font-weight: bold; font-family: 'Courier New', Courier, 'espacio sencillo';">
                &nbsp;</p>
            <p style="font-size: small; color: #000080; font-weight: bold; font-family: 'Courier New', Courier, 'espacio sencillo';">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/_images/logo_cliente/LogoEpago.bmp" />
            </p>
            <p style="font-size: small; color: #000080; font-weight: bold; font-family: 'Courier New', Courier, 'espacio sencillo';">
                <br />
            </p>
        </center>
    </div>
    </form>
</body>
</html>
