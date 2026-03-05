<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="visor.aspx.vb" Inherits="Miharu.Imaging.Explorer.WebService.visor" %>

<%@ Register Assembly="Miharu.Web.Controls" Namespace="Miharu.Web.Controls" TagPrefix="miharu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: MIHARU Imaging ::</title>
    <link href="_styles/StyleSheet_Miharu.css" rel="stylesheet" type="text/css" />
    <link href="_scripts/ModalBox/modalbox.css" rel="stylesheet" type="text/css" />
    <script src="_scripts/_DocumentViewer/tjpzoom.js" type="text/javascript"></script>
    <script src="_scripts/ModalBox/lib/prototype.js" type="text/javascript"></script>
    <script src="_scripts/ModalBox/lib/scriptaculous.js?load=effects" type="text/javascript"></script>
    <script src="_scripts/ModalBox/modalbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Desbloquear() {
            try {
                if (window.opener != null && window.opener.Bloquear_no != null)
                    window.opener.Bloquear_no();
            }
            catch (e) { }
        }
    </script>
</head>
<body onload="Desbloquear()" style="background-color: #E0DFE3">
    <form id="objFom" runat="server">
    <div class="Oculto">
        <input type="hidden" id="EndRequestAction" value="" runat="server" />
    </div>
    <script type="text/javascript">
        EndRequestAction();

        function EndRequestAction() {
            var CtlAction = document.getElementById('<%= EndRequestAction.ClientID %>');
            var arrAction = CtlAction.value.split("|");

            CtlAction.value = "";

            if (arrAction[0] == "Close") {
                window.close();
                return;
            }
            else if (arrAction[0] != "") {
                // Determinar los parametros
                var arrParametros = arrAction[1].split(";");

                if (arrAction[0] == "Alert") {
                    ShowAlert(arrParametros[0], arrParametros[1], arrParametros[2])
                }
            }
        }

        function ShowAlert(Mensaje, Icono, Ancho) {
            Modalbox.show("<div class=\'" + Icono + "\'>" +
                            "<table>" +
                                 "<tr>" +
                                    "<td style='width:40px'></td>" +
                                    "<td><p>" + Mensaje + "</p></td>" +
                                 "</tr>" +
                                 "<tr>" +
                                    "<td style='height:20px'></td>" +
                                    "<td></td>" +
                                 "</tr>" +
                                 "<tr>" +
                                    "<td></td>" +
                                    "<td><input type=\'button\' value=\'Aceptar\' onclick=\'Modalbox.hide()\' /></td>" +
                                 "</tr>" +
                            "</table>" +
                          "</div>", { title: ':: MIHARU ::', width: Ancho });
        }        
        
    </script>
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">
                <miharu:DocumentViewer ID="tvVisor" runat="server" Height="635px" Scrolling="True"
                    Width="970px" BorderWidth="0px" Resolucion="0.65">
                </miharu:DocumentViewer>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
