<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Miharu.Explorer.Imaging._Main.Login" %>
<%@ Import Namespace="Miharu.Explorer.Imaging._Clases" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../_Styles/Login.css" rel="stylesheet" type="text/css" />
    <title>e-Explorer</title>
    <script src="../_Scripts/jquery.js" type="text/javascript"></script>
    <link href="../_Scripts/alert/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../_Scripts/alert/jquery.alerts.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        _RecoverPasswordURL = '';

        $(function () {
            $("#userTxt").focus();
            $('#lblRecoverPassword').click(function () { Recuperar_Password(); });
            $('#autenticateBtn').click(function () { Autenticar(); });
        });

        function Autenticar() {
            $.ajax({
                contentType: "Application/json; charset=utf-8"
                , dataType: "json"
                , type: "POST"
                , url: "Login.aspx/Autenticar"
                , data: "{user:'" + $('#userTxt').val() + "', pass:'" + $('#passTxt').val() + "'}"
                , success: function(resp) { entrar(resp.d); }
                , error: function() { jAlert("Error ingresando a la pagina, por favor comuniquese con el administrador del sistema.", "Error autenticacion"); }
            });
        }

        function entrar(resp) {
            if (resp == 1) {
                window.location = "Inicio.aspx";
            }
            if (resp != 1) {
                if (resp == 2) {
                    window.location = "change_password.aspx";
                }
                else if (resp == 3) {
                    jAlert("El usuario se encuentra inactivo, por favor comuniquese con el administrador del sistema", "e-Explorer");
                }
                else if (resp == 4) {
                    jAlert("El usuario no tiene acceso a ninguna funcionalidad de la aplicación", "e-Explorer");
                }
                else if (resp == 5) {
                    jAlert("Debe ingresar el usuario", "e-Explorer");
                }
                else {
                    jAlert("Usuario o contraseña invalida, por favor intente nuevamente", "e-Explorer");
                }
            }

        }

        function runScript(e) {
            if (e.keyCode == 13) {
                Autenticar();
            }
        }

        function Recuperar_Password() {
            window.open(_RecoverPasswordURL);
        }
        
    </script>
</head>
<body>
    <script type="text/javascript">
        _RecoverPasswordURL = '<%= getRecoverURL %>';
    </script>
    <form id="form1" runat="server">
    <div style="padding: 100px 0 0 300px;">
        <div id="login-box">
            <h2>
                e-Explorer</h2>
            Consulta de Data e Imágenes de documentos.
            <table style="margin: 50px 0 50px 0">
                <tr>
                    <td>
                        <div id="login-box-name" style="text-align: right;">
                            Usuario:</div>
                    </td>
                    <td>
                        <div id="login-box-field">
                            <input type="text" id="userTxt" runat="server" style="width: 210px" class="form-login" maxlength="2048" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="pass-box-name" style="text-align: right;">
                            Contraseña:</div>
                    </td>
                    <td>
                        <div id="pass-box-field">
                            <input type="password" id="passTxt" runat="server" style="width: 210px" class="form-login" maxlength="2048" onkeypress="return runScript(event)" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center; vertical-align:middle;">
                        <a href="#">
                            <img id="autenticateBtn" src="../_Images/Login/btn.png" alt="" width="103" height="42" /></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center; vertical-align:middle;">
                       <asp:LinkButton ID="lblRecoverPassword" runat="server" Style="color: #FFFFFF; font-weight: bold; text-decoration: underline;">¿Olvidó su contraseña?</asp:LinkButton>
                    </td>         
                     </tr>
            </table>
        </div>
        <div id="login-box-logo">
            <asp:Image ID="Image1" ImageUrl="../_Images/LogoExplorer.png" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
