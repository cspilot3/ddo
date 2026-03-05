<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="Miharu.Explorer.Imaging._Main.change_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../_Styles/Login.css" rel="stylesheet" type="text/css" />
    <title>e-Explorer</title>
    <script src="../_Scripts/jquery.js" type="text/javascript"></script>
    <link href="../_Scripts/alert/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../_Scripts/alert/jquery.alerts.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        $(function () {
            $("#userTxt").focus();
            $('#autenticateBtn').click(function () { Change_Password(); });
            $('#cancelBtn').click(function () { window.location = "Login.aspx"; });
        });

        function Change_Password() {
            $.ajax({
                contentType: "Application/json; charset=utf-8"
                , dataType: "json"
                , type: "POST"
                , url: "change_password.aspx/Change_Password"
                , data: "{nOldPassword:'" + $('#passOldTxt').val() + "',  nNewPassword:'" + $('#passNewTxt').val() + "', nConfirmPassword:'" + $('#passNewTxt2').val() + "'}"
                , success: function (resp) { entrar(resp.d); }
                , error: function() {
                     jAlert("Error ingresando a la pagina, por favor comuniquese con el administrador del sistema.", "Error autenticacion");
                }
            });
        }

        function entrar(resp) {
            if (resp == 1) {
                jAlert("La contraseña se cambió exitosamente", "e-Explorer");
                window.location = "Inicio.aspx";
            }
            if (resp != 1) {
                if (resp == 2) {
                    jAlert("Contraseña no válida", "e-Explorer");
                }
                else if (resp == 3) {
                    jAlert("Error en la contraseña", "e-Explorer");
                }
                else if (resp == 4) {
                    jAlert("El nuevo password no puede ser vacío", "e-Explorer");
                }
                else if (resp == 5) {
                    jAlert("No coincide la confirmación del password", "e-Explorer");
                }
                else {
                    jAlert("Usuario o contraseña invalida, por favor intente nuevamente", "e-Explorer");
                }
            }
        }

        function runScript(e) {
            if (e.keyCode == 13) {
                Change_Password();
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 100px 0 0 300px;">
        <div id="login-box">
            <h1 style="width: 331px">
                Cambio de Contraseña</h1>
            <table style="margin: 10px 0 10px 0">
                <tr>
                    <td>
                        <div id="pass-box-name" style="text-align: right;">
                            Contraseña anterior:</div>
                    </td>
                    <td>
                        <div id="pass-box-field">
                            <input type="password" id="passOldTxt" style="width: 210px" class="form-login" maxlength="2048" onkeypress="return runScript(event)" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="Div1" style="text-align: right;">
                            Nueva contraseña:</div>
                    </td>
                    <td>
                        <div id="Div2">
                            <input type="password" id="passNewTxt" style="width: 210px" class="form-login" maxlength="2048" onkeypress="return runScript(event)" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="Div3" style="text-align: right;">
                            Confirmar nueva contraseña:</div>
                    </td>
                    <td>
                        <div id="Div4">
                            <input type="password" id="passNewTxt2" style="width: 210px" class="form-login" maxlength="2048" onkeypress="return runScript(event)" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; vertical-align: middle;">
                        <a href="#" runat="server">
                            <img id="autenticateBtn" src="../_Images/Login/btn_aceptar.png" alt="" width="103" height="42" /></a>
                        <a href="#" runat="server">
                            <img id="cancelBtn" src="../_Images/Login/btn_cancel.png" alt="" width="103" height="42" /></a>
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
