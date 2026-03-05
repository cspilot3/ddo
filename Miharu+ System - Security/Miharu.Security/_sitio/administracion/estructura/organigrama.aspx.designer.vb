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

Namespace _sitio.administracion.estructura

    Partial Public Class organigrama

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
        '''Control tcBase.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents tcBase As Global.AjaxControlToolkit.TabContainer

        '''<summary>
        '''Control tpConsulta.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents tpConsulta As Global.AjaxControlToolkit.TabPanel

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
        '''Control lblFiltro.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblFiltro As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control ucFiltro.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ucFiltro As Global.Miharu.Security._controls.wucFilter

        '''<summary>
        '''Control gvBase.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents gvBase As Global.Miharu.Web.Controls.SlygGridView

        '''<summary>
        '''Control tpDetalle.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents tpDetalle As Global.AjaxControlToolkit.TabPanel

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
        '''Control pnlDetalle.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlDetalle As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control SelectedBloque.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents SelectedBloque As Global.System.Web.UI.HtmlControls.HtmlInputHidden

        '''<summary>
        '''Control lblCodEntidad.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblCodEntidad As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control icbEdit.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents icbEdit As Global.Miharu.Web.Controls.ImageChangingButton

        '''<summary>
        '''Control icbDelete.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents icbDelete As Global.Miharu.Web.Controls.ImageChangingButton

        '''<summary>
        '''Control icbDelete_ConfirmButtonExtender.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents icbDelete_ConfirmButtonExtender As Global.AjaxControlToolkit.ConfirmButtonExtender

        '''<summary>
        '''Control icbAddSubordinado.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents icbAddSubordinado As Global.Miharu.Web.Controls.ImageChangingButton

        '''<summary>
        '''Control icbAddAsistencial.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents icbAddAsistencial As Global.Miharu.Web.Controls.ImageChangingButton

        '''<summary>
        '''Control lblNombreEntidad.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblNombreEntidad As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control pnlBase.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlBase As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control pnlDatos.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlDatos As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control pnlDatosHead.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents pnlDatosHead As Global.System.Web.UI.WebControls.Panel

        '''<summary>
        '''Control lblNombre.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblNombre As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control txtNombre.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents txtNombre As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control lblCodNodo.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblCodNodo As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control lblCodigo.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents lblCodigo As Global.System.Web.UI.WebControls.Label

        '''<summary>
        '''Control txtCodigo.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents txtCodigo As Global.System.Web.UI.WebControls.TextBox

        '''<summary>
        '''Control btnDatosAceptar.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents btnDatosAceptar As Global.System.Web.UI.WebControls.Button

        '''<summary>
        '''Control btnDatosCancelar.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents btnDatosCancelar As Global.System.Web.UI.WebControls.Button

        '''<summary>
        '''Control ModalPopupDatos.
        '''</summary>
        '''<remarks>
        '''Campo generado automáticamente.
        '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
        '''</remarks>
        Protected WithEvents ModalPopupDatos As Global.AjaxControlToolkit.ModalPopupExtender
    End Class
End Namespace
