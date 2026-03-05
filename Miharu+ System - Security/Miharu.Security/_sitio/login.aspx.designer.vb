'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Namespace _sitio

    Partial Public Class login

        '''<summary>
        '''Control upCuadros.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents upCuadros As Global.System.Web.UI.UpdatePanel

        '''<summary>
        '''Control pnlBase.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlBase As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control lblTitulo.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblTitulo As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control lblUsuario.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblUsuario As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control txtUsuario.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents txtUsuario As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control lblPwd.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblPwd As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control txtContraseña.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents txtContraseña As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control rfvUser.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents rfvUser As Global.System.Web.UI.WebControls.RequiredFieldValidator

        '''<summary>
        '''Control rfvUser_ValidatorCalloutExtender.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents rfvUser_ValidatorCalloutExtender As Global.AjaxControlToolkit.ValidatorCalloutExtender

        '''<summary>
        '''Control btnIngresar.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents btnIngresar As Global.System.Web.UI.WebControls.Button

        '''<summary>
        '''Control upUpdatePassword.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents upUpdatePassword As Global.System.Web.UI.UpdatePanel

        '''<summary>
        '''Control pnlPassword.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlPassword As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control pnlPasswordHead.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlPasswordHead As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control OldPasswordLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents OldPasswordLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control OldPasswordTextBox.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents OldPasswordTextBox As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control NewPasswordLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents NewPasswordLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control NewPasswordTextBox.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents NewPasswordTextBox As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control ConfirmPasswordLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ConfirmPasswordLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control ConfirmPasswordTextBox.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ConfirmPasswordTextBox As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control PasswordAceptarButton.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents PasswordAceptarButton As Global.System.Web.UI.WebControls.Button

        '''<summary>
        '''Control PasswordCancelarButton.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents PasswordCancelarButton As Global.System.Web.UI.WebControls.Button

        '''<summary>
        '''Control ModalPopupPassword.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ModalPopupPassword As Global.AjaxControlToolkit.ModalPopupExtender
    End Class
End Namespace
