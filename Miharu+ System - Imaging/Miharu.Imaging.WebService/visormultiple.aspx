<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="visormultiple.aspx.vb"
    Inherits="Miharu.Imaging.WebService.visormultiple" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">
        function Desbloquear() {
            if (window.opener != null && window.opener.Bloquear_no != null)
                window.opener.Bloquear_no();
        }

        function ResizeFrame() {
            var ventana_ancho = jQuery(window).width();
            var ventana_alto = jQuery(window).height();
            document.objFom.ventana_width.value = ventana_ancho - 20;
            document.objFom.ventana_height.value = ventana_alto - 20;
        }
    </script>
    <title>:: MIHARU Imaging ::</title>
    <style type="text/css">
        .Primero
        {
            width: 50%;
            height: 50%;
            top: 0;
            left: 0;
            position: absolute;
        }
         .Segundo
        {
            width: 50%;
            height: 50%;
            top: 0;
            right: 0;
            position: absolute;
        }
         .Tercero
        {
            width: 50%;
            height: 50%;
            bottom: 0;
            left: 0;
            position: absolute;
        }
         .Cuarto
        {
            width: 50%;
            height: 50%;
            bottom: 0;
            right: 0;
            position: absolute;
        }
    </style>
</head>
<body onload="Desbloquear();" style="background-color: #E0DFE3;">
    <form id="objFom" runat="server">
   
    <div class="Primero">
        <iframe id="Iframe1" src="visor.aspx?" runat="server" width="100%" height="100%"/>
    </div>
    <div class="Segundo">
        <iframe id="Iframe2" src="visor.aspx?" runat="server" width="100%" height="100%"/>
    </div>
    <div class="Tercero">
        <iframe id="Iframe3" src="visor.aspx?" runat="server" width="100%" height="100%"/>
    </div>
    <div class="Cuarto">
        <iframe id="Iframe4" src="visor.aspx?token=<%= TokenImagen4 %>" runat="server" width="100%" height="100%"/>
    </div>
    
    </form>
</body>
</html>
