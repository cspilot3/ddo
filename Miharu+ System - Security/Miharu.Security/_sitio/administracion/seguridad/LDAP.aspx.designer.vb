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
Namespace _sitio.administracion.seguridad

    Partial Public Class LDAP

        '''<summary>
        '''Control divAdd.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents divAdd As Global.System.Web.UI.HtmlControls.HtmlGenericControl

        '''<summary>
        '''Control ibAdd.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ibAdd As Global.System.Web.UI.WebControls.ImageButton

        '''<summary>
        '''Control divDelete.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents divDelete As Global.System.Web.UI.HtmlControls.HtmlGenericControl

        '''<summary>
        '''Control ibDelete.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ibDelete As Global.System.Web.UI.WebControls.ImageButton

        '''<summary>
        '''Control ibDelete_ConfirmButtonExtender.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ibDelete_ConfirmButtonExtender As Global.AjaxControlToolkit.ConfirmButtonExtender

        '''<summary>
        '''Control divSave.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents divSave As Global.System.Web.UI.HtmlControls.HtmlGenericControl

        '''<summary>
        '''Control ibSave.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ibSave As Global.System.Web.UI.WebControls.ImageButton

        '''<summary>
        '''Control lblTitulo.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblTitulo As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control BaseTabContainer.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents BaseTabContainer As Global.AjaxControlToolkit.TabContainer

        '''<summary>
        '''Control ConsultaTabPanel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ConsultaTabPanel As Global.AjaxControlToolkit.TabPanel

        '''<summary>
        '''Control imgIcoConsulta.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents imgIcoConsulta As Global.System.Web.UI.WebControls.Image

        '''<summary>
        '''Control lblConsulta.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblConsulta As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control EntidadLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents EntidadLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control EntidadDropDownList.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents EntidadDropDownList As Global.System.Web.UI.WebControls.DropDownList

        '''<summary>
        '''Control FiltroLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents FiltroLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control ucFiltro.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ucFiltro As Global.Miharu.Security._controls.wucFilter

        '''<summary>
        '''Control BaseGridView.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents BaseGridView As Global.Miharu.Web.Controls.SlygGridView

        '''<summary>
        '''Control DetalleTabPanel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents DetalleTabPanel As Global.AjaxControlToolkit.TabPanel

        '''<summary>
        '''Control imgIcoDetalle.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents imgIcoDetalle As Global.System.Web.UI.WebControls.Image

        '''<summary>
        '''Control lblDetalle.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblDetalle As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control DetallePanel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents DetallePanel As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control EntidadEditLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents EntidadEditLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control NombreEntidadLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents NombreEntidadLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control GrupoLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents GrupoLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control CodLDAPLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents CodLDAPLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control GrupoTextBox.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents GrupoTextBox As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control GrupoRequiredFieldValidator.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents GrupoRequiredFieldValidator As Global.System.Web.UI.WebControls.RequiredFieldValidator

        '''<summary>
        '''Control GrupoRequiredFieldValidator_ValidatorCalloutExtender.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents GrupoRequiredFieldValidator_ValidatorCalloutExtender As Global.AjaxControlToolkit.ValidatorCalloutExtender

        '''<summary>
        '''Control PerfilLabel.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents PerfilLabel As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control PerfilDropDownList.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents PerfilDropDownList As Global.System.Web.UI.WebControls.DropDownList
    End Class
End Namespace