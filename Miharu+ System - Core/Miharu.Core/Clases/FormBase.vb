Imports DBCore
Imports Miharu.Core.Main
Imports Slyg.Tools

Namespace Clases

    Public Enum SaveType
        Insert
        Update
        Delete
    End Enum

    Public Class FormBase
        Inherits System.Web.UI.Page

#Region " DECLARACIONES "
        Private _MySesion As Miharu.Security.Library.Session.Sesion
        Private WithEvents _myMaster1 As FormMasterPage
        Private Const nNumEncrip As Integer = 10
#End Region

#Region "PROPIEDADES"
        Public ReadOnly Property MySesion() As Miharu.Security.Library.Session.Sesion
            Get
                Return _MySesion
            End Get
        End Property

        Public Shadows ReadOnly Property MyMasterPage() As FormMasterPage
            Get
                Return CType(MyBase.Master, FormMasterPage)
            End Get
        End Property

        Public ReadOnly Property ConnectionString() As Program.TypeConnectionString
            Get
                If MySesion.Parameter("ConnectionStrings") Is Nothing Then
                    Return Nothing
                Else
                    Return CType(MySesion.Parameter("ConnectionStrings"), Program.TypeConnectionString)
                End If
            End Get
        End Property

        Public ReadOnly Property AccesoEntidad() As Boolean
            Get
                If Not IsNothing(Session("bAccesoEntidad")) Then
                    Return CBool(Session("bAccesoEntidad"))
                Else
                    Return False
                End If
            End Get
        End Property
#End Region

#Region " EVENTOS "
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            _MySesion = CType(Session("Sesion"), Security.Library.Session.Sesion)
        End Sub
#End Region

#Region " METODOS "
        Protected Overrides Sub CreateChildControls()
            MyBase.CreateChildControls()

            Try
                MyMaster = CType(Me.Master, FormMasterPage)
            Catch ex As Exception
            End Try
        End Sub

        Protected Sub SelectText(ByVal ctl As System.Web.UI.WebControls.TextBox)
            ctl.Attributes.Add("onfocus", ctl.ClientID + ".select();")
        End Sub
#End Region

#Region "FUNCIONES"

        Public Sub Disabled_Menu_Form(ByVal PageName As String, ByVal MyPathPermiso As String)
            Dim agregar = True
            Dim editar = True
            If ValidarNavegacion(PageName, MyPathPermiso) Then

                If Not Validar_Permiso_Consulta(PageName, MyPathPermiso) Then
                    MyMasterPage.ToolControl.DisableVisible("Find")
                End If

                If Not Validar_Permiso_Agregar(PageName, MyPathPermiso) Then
                    MyMasterPage.ToolControl.DisableVisible("New")
                    agregar = False
                End If
                If Not Validar_Permiso_Editar(PageName, MyPathPermiso) Then
                    MyMasterPage.ToolControl.DisableVisible("Edit")
                    editar = False
                End If
                If Not Validar_Permiso_Eliminar(PageName, MyPathPermiso) Then
                    MyMasterPage.ToolControl.DisableVisible("Delete")
                End If
                If agregar = False And editar = False Then
                    MyMasterPage.ToolControl.DisableAction("Save")
                End If
                If Not Validar_Permiso_Exportar(PageName, MyPathPermiso) Then
                    ' MyMasterPage.ToolControl.DisableAction("") Falta exportar 
                End If
                If Not Validar_Permiso_Imprimir(PageName, MyPathPermiso) Then
                    'MyMasterPage.ToolControl.DisableAction("") Falta Imprimir 
                End If
            Else
                Return
            End If
        End Sub

        Private Function ValidarPermisos(ByVal nPathPermiso As String, ByVal tipo_permiso As String) As Boolean
            Dim bReturn As Boolean = False
            Select Case tipo_permiso
                Case "Acceder"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeAcceder(nPathPermiso)
                Case "Consultar"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeConsultar(nPathPermiso)
                Case "Agregar"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeAgregar(nPathPermiso)
                Case "Eliminar"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeEliminar(nPathPermiso)
                Case "Editar"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeEditar(nPathPermiso)
                Case "Exportar"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeExportar(nPathPermiso)
                Case "Imprimir"
                    bReturn = _MySesion.Usuario.PerfilManager.PuedeImprimir(nPathPermiso)
            End Select
            Return bReturn
        End Function

        Protected Function ValidarNavegacion(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If _MySesion Is Nothing Or Session("Sesion") Is Nothing Then
                'Master.ShowAlert("La sesión ha caducado, por favor salga y vuelva a ingresar al aplicativo", Workflow.MiharuMasterForm.MsgBoxIcon.IconError)
                Response.Redirect("~/" & "_sitio/Inicio.aspx")

            ElseIf _MySesion.Pagina.Name <> nPageName Then
                'Master.ShowAlert("El usuario no esta autorizado para ingresar a esta sección", Workflow.MiharuMasterForm.MsgBoxIcon.IconError)
                Response.Redirect("~/" & "_sitio/Inicio.aspx")

            ElseIf nPathPermiso = "0" Then
                Return True

            ElseIf ValidarPermisos(nPathPermiso) Then
                Return True

            Else
                'Master.ShowAlert("El usuario no esta autorizado para ingresar a esta sección", Workflow.MiharuMasterForm.MsgBoxIcon.IconError)
                Response.Redirect("~/" & "_sitio/Inicio.aspx")

            End If

            Return False
        End Function
        Private Function ValidarPermisos(ByVal nPathPermiso As String) As Boolean
            Return _MySesion.Usuario.PerfilManager.PuedeAcceder(nPathPermiso)
        End Function
        Protected Function Validar_Permiso_Consulta(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If ValidarPermisos(nPathPermiso, "Consultar") Then
                Return True
            Else
                Return False
            End If
        End Function
        Protected Function Validar_Permiso_Agregar(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If ValidarPermisos(nPathPermiso, "Agregar") Then
                Return True
            Else
                Return False
            End If
        End Function
        Protected Function Validar_Permiso_Eliminar(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If ValidarPermisos(nPathPermiso, "Eliminar") Then
                Return True
            Else
                Return False
            End If
        End Function
        Protected Function Validar_Permiso_Editar(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If ValidarPermisos(nPathPermiso, "Editar") Then
                Return True
            Else
                Return False
            End If
        End Function
        Protected Function Validar_Permiso_Exportar(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If ValidarPermisos(nPathPermiso, "Exportar") Then
                Return True
            Else
                Return False
            End If
        End Function
        Protected Function Validar_Permiso_Imprimir(ByVal nPageName As String, ByVal nPathPermiso As String) As Boolean
            If ValidarPermisos(nPathPermiso, "Imprimir") Then
                Return True
            Else
                Return False
            End If
        End Function


        Protected Function valida_Excepcion(ByVal excepcion As Exception) As String
            Dim sReturn As String = ""
            Dim sqlEx As System.Data.SqlClient.SqlException
            Try
                sqlEx = CType(excepcion.InnerException, SqlClient.SqlException)

                Select Case sqlEx.Number
                    Case 547
                        sReturn = "El registro no puede ser eliminado, ya que esta siendo utilizado en otro lugar."
                    Case 2627
                        sReturn = "El registro no puede ser creado, existe otro registro con esta llave."
                    Case Else
                        sReturn = excepcion.Message
                End Select
            Catch ex As Exception
                Throw excepcion
            End Try
            Return sReturn
        End Function

        'Funcion para rellenar una cadena con determinado caracter (de izquiera a derecha)
        Public Function Replicate(ByVal valor As String, ByVal tamano As Integer, ByVal caracter As String) As String
            Dim sReturn As String = String.Empty
            Try
                Dim lengthStr As Integer = valor.Length

                For i As Integer = 0 To tamano - lengthStr - 1
                    sReturn &= caracter
                Next
                sReturn &= valor
            Catch ex As Exception
                Throw
            End Try
            Return sReturn
        End Function

#End Region

#Region "DEFINICION DE VARIABLES"
        Private _BodyType As BodyType = BodyType.Tabs
        Private _ShowToolBox As Boolean = True
        Private _ShowMenu As Boolean = True
        Private _Cod_Mapa_Modulo As Integer = -1
        Private mExceptionController As New ExceptionController
        Private _DateFormatDataBase As String = "YYYY/MM/DD HH24:MI:SS"
        Private _BindListControlInfoList As New BindListControlInfoCollection
#End Region

#Region "PROPIEDADES"

        Public Property IsPopupValid() As Boolean
            Get
                Return CBool(Session("IsPopupValid"))
            End Get
            Set(ByVal value As Boolean)
                Session("IsPopupValid") = value
            End Set
        End Property

        Public Property PopupAction() As String
            Get
                Return CStr(Session("PopupAction"))
            End Get
            Set(ByVal value As String)
                Session("PopupAction") = value
            End Set
        End Property

        Public Property PopupResult() As NameObjectCollection
            Get
                Return CType(Session("PopupResult"), NameObjectCollection)
            End Get
            Set(ByVal value As NameObjectCollection)
                Session("PopupResult") = value
            End Set
        End Property

        Public Property BodyType() As BodyType
            Get
                Return _BodyType
            End Get
            Set(ByVal value As BodyType)
                _BodyType = value
            End Set
        End Property

        Public Property ShowToolBox() As Boolean
            Get
                Return _ShowToolBox
            End Get
            Set(ByVal value As Boolean)
                _ShowToolBox = value
            End Set
        End Property

        Public Property ShowMenu() As Boolean
            Get
                Return _ShowMenu
            End Get
            Set(ByVal value As Boolean)
                _ShowMenu = value
            End Set
        End Property

        Public Property Cod_Mapa_Modulo() As Integer
            Get
                Return _Cod_Mapa_Modulo
            End Get
            Set(ByVal value As Integer)
                _Cod_Mapa_Modulo = value
            End Set
        End Property

        Public Property SaveType() As SaveType
            Get
                If (ViewState("SaveType__1") Is Nothing) Then
                    ViewState("SaveType__1") = SaveType.Insert.ToString
                End If
                Return CType([Enum].Parse(GetType(SaveType), CStr(ViewState("SaveType__1"))), SaveType)
            End Get
            Set(ByVal value As SaveType)
                ViewState("SaveType__1") = value.ToString
            End Set
        End Property

        Public Property CurrentMasterTab() As MasterTabType
            Get
                Return MyMasterPage.GetActiveTabIndex
            End Get
            Set(ByVal value As MasterTabType)
                MyMasterPage.SetActiveTabIndex(value)
            End Set
        End Property

        Public Property SessionData() As SessionData
            Get
                If (Session("SessionData") Is Nothing) Then
                    Session.Add("SessionData", New SessionData)
                End If
                Return CType(Session("SessionData"), Core.SessionData)
            End Get
            Set(ByVal value As SessionData)
                Session("SessionData") = value
            End Set
        End Property

        Public ReadOnly Property BindListControlInfoList() As BindListControlInfoCollection
            Get
                Return _BindListControlInfoList
            End Get
        End Property

        Public Property GridData() As DataTable
            Get
                Return CType(Session("GridData"), DataTable)
            End Get
            Set(ByVal value As DataTable)
                Session("GridData") = value
            End Set
        End Property

        Public Property DatosNuevos() As DataTable
            Get
                Return CType(Session("DatosNuevos"), DataTable)
            End Get
            Set(ByVal value As DataTable)
                Session("DatosNuevos") = value
            End Set
        End Property

        Public Property PageData() As Object
            Get
                Return Session("PageData")
            End Get
            Set(ByVal value As Object)
                Session("PageData") = value
            End Set
        End Property

        Public ReadOnly Property ExceptionController() As ExceptionController
            Get
                Return mExceptionController
            End Get
        End Property

        Public Overridable ReadOnly Property AudUsrColumnName() As String
            Get
                Return "AUD_USR_INS"
            End Get
        End Property

        Public Overridable ReadOnly Property AudFchColumnName() As String
            Get
                Return "AUD_FCH_INS"
            End Get
        End Property

        Public Property DateFormatDataBase() As String
            Get
                Return _DateFormatDataBase
            End Get
            Set(ByVal value As String)
                _DateFormatDataBase = value
            End Set
        End Property

        Public Property ModalDialogNombre() As String
            Get
                Return CStr(Session("ModalDialogNombre"))
            End Get
            Set(ByVal value As String)
                Session("ModalDialogNombre") = value
            End Set
        End Property

        Public Overridable ReadOnly Property RequiredValidUser() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property ClearPageData() As Boolean
            Get
                Return True
            End Get
        End Property


#End Region

#Region "CONTROL DE PAGINA"
        Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
            Try
                Initialize()
                mExceptionController.Initialize(CType(Me.Master, IExceptionController))
                MyMasterPage.SetPageBase(Me)
            Catch : End Try
            MyBase.OnInit(e)
        End Sub

        Protected Overrides Sub LoadViewState(ByVal savedState As Object)
            MyBase.LoadViewState(savedState)
            _BindListControlInfoList.AddRange(CType(ViewState("BindListControlInfoList"), Global.System.Collections.Generic.IEnumerable(Of Global.Miharu.Core.BindListControlInfo)))
        End Sub

        Public Overridable Sub Initialize()
            _BodyType = BodyType.Tabs
            _ShowToolBox = True
            _ShowMenu = True
        End Sub

        Public Sub Alert(ByVal text As String)
            'MyMasterpage.Alert(text)
        End Sub

        Public Sub OpenModalDialog(ByVal action As String, ByVal url As String, ByVal name As String, ByVal nTitle As String, ByVal width As Integer, ByVal height As Integer, Optional ByVal left As Integer = -1, Optional ByVal top As Integer = -1)
            If (left = -1) Then
                left = CInt((1010 - width) / 2)
            End If
            If (top = -1) Then
                top = CInt((600 - height) / 2)
            End If

            PopupAction = action
            ModalDialogNombre = name
            IsPopupValid = True
            'Dim strUrl = ResolveUrl(url) & ";"
            Dim strUrl = url & ";"
            Dim strNombre = name & ";"
            Dim strCadena = "width=" & width & ",height=" & height & ",left=" & left & ",top=" & top & ";"
            Me.MyMasterPage.ShowDialog(strUrl & strNombre & strCadena & nTitle)
        End Sub

        Public Sub OpenModalPopup(ByVal action As String, ByVal url As String, ByVal name As String, Optional ByVal width As Integer = 500, Optional ByVal height As Integer = 300, Optional ByVal left As Integer = -1, Optional ByVal top As Integer = -1)
            If (left = -1) Then
                left = CInt((1010 - width) / 2)
            End If
            If (top = -1) Then
                top = CInt((600 - height) / 2)
            End If

            PopupAction = action
            ModalDialogNombre = name
            IsPopupValid = True
            Dim strUrl = ResolveUrl(url) & ";"
            Dim strNombre = name & ";"
            Dim strCadena = "toolbar=0,location=0,directories=0,status=0,menubar=no,scrollbars=no,resizable=no,width=" & width & ",height=" & height & ",left=" & left & ",top=" & top & ";"
            Me.MyMasterPage.ShowPopUp(strUrl & strNombre & strCadena)
        End Sub

        Protected Overrides Function SaveViewState() As Object
            Try
                ViewState("BindListControlInfoList") = _BindListControlInfoList.ToArray()
                Return MyBase.SaveViewState()
            Catch ex As Exception
                Throw New Exception
            End Try
        End Function

        'Método para mostrar un error
        Public Sub showErrorPage(ByVal objError As Exception)
            Session("msgError") = objError
            Response.Redirect("~/Main/Error.aspx")
        End Sub

        'Función para encriptar Base 64
        Public Function encodeBase64(ByVal sMensaje As String) As String
            Dim sRetornar As String = ""
            Dim encoding As New System.Text.ASCIIEncoding()
            Dim sCripTem As Byte()
            Try
                sCripTem = encoding.GetBytes(sMensaje)

                For i As Short = 0 To nNumEncrip
                    sMensaje = Convert.ToBase64String(sCripTem)
                    sCripTem = encoding.GetBytes(sMensaje)
                Next
                sRetornar = sMensaje
            Catch ex As Exception
                showErrorPage(ex)
            End Try
            Return sRetornar
        End Function

        'Función para desencriptar Base 64
        Public Function decodeBase64(ByVal sMensaje As String) As String
            Dim sRetornar As String = ""
            Dim sCripTem As Byte()
            Dim enc As New System.Text.ASCIIEncoding()
            Try
                For i As Short = 0 To nNumEncrip
                    sCripTem = Convert.FromBase64String(sMensaje)
                    sMensaje = enc.GetString(sCripTem)
                Next
                sRetornar = sMensaje
            Catch ex As Exception
                showErrorPage(ex)
            End Try
            Return sRetornar
        End Function
#End Region

#Region "AUTOMATIZACION DE CONTROLES"
        Public Sub Fill_ListControl(ByRef listControl As ListControl, ByVal data As DataTable, ByVal columnText As String, ByVal columnValue As String, Optional ByVal addEmptyRow As Boolean = True, Optional ByVal textEmptyText As String = "-", Optional ByVal textEmptyValue As String = "", Optional ByVal Fuente As String = "")
            Dim text As String = "", value As String = ""

            listControl.Items.Clear()
            If (addEmptyRow) Then
                listControl.Items.Add(New ListItem(textEmptyText, textEmptyValue))
            End If
            For Each row As DataRow In data.Rows
                text = ""
                value = ""
                Try
                    text = CStr(row(columnText))
                    value = CStr(row(columnValue))
                Catch ex As Exception
                    Throw New Exception("No fue posible realizar el enlace de datos del control " & listControl.ID & " text='" & text & "' value='" & value & "' Fuente='" & Fuente & "' " & ex.Message, ex)
                End Try

                listControl.Items.Add(New ListItem(text, value))
            Next

            Try
                listControl.SelectedIndex = 0
            Catch : End Try
        End Sub

        Public Sub Bind_ListControl_Sql(ByRef listControl As DropDownList, ByVal sqlText As String, ByVal columnText As String, ByVal columnValue As String, Optional ByVal addEmptyRow As Boolean = True, Optional ByVal textEmptyText As String = "-", Optional ByVal textEmptyValue As String = "")
            Try
                BindListControlInfoList.Add(New BindListControlInfo(listControl.ID, addEmptyRow, textEmptyText, textEmptyValue))

            Catch ex As Exception
                ExceptionController.Register(ex, True)
            End Try
        End Sub

        Public Sub Bind_ListControl_Sql(ByRef listControl As ListBox, ByVal sqlText As String, ByVal columnText As String, ByVal columnValue As String, Optional ByVal addEmptyRow As Boolean = True, Optional ByVal textEmptyText As String = "-", Optional ByVal textEmptyValue As String = "")
            Try
                BindListControlInfoList.Add(New BindListControlInfo(listControl.ID, addEmptyRow, textEmptyText, textEmptyValue))

            Catch ex As Exception
                ExceptionController.Register(ex, True)
            End Try
        End Sub


        Public Sub SetControlValue(ByVal ctr As Control, ByVal value As Object)
            If TypeOf ctr Is CoreDateBox Then
                CType(ctr, CoreDateBox).Text = CStr(FormatToControlValue(value))

            ElseIf TypeOf ctr Is CoreHourBox Then
                CType(ctr, CoreHourBox).Text = CStr(FormatToControlValue(value))

            ElseIf TypeOf ctr Is TextBox Then
                CType(ctr, TextBox).Text = CStr(FormatToControlValue(value))

            ElseIf TypeOf ctr Is DropDownList Then
                Try
                    CType(ctr, DropDownList).SelectedValue = CStr(FormatToControlValue(value))
                    CType(ctr, DropDownList).SelectedIndex = CType(ctr, DropDownList).SelectedIndex
                Catch
                    CType(ctr, DropDownList).SelectedIndex = -1
                End Try

            ElseIf TypeOf ctr Is ListBox Then
                Try
                    CType(ctr, ListBox).SelectedValue = CStr(FormatToControlValue(value))
                Catch
                    CType(ctr, ListBox).SelectedIndex = -1
                End Try


            ElseIf TypeOf ctr Is CheckBox Then
                Dim val = FormatToControlValue(value)
                If (val.ToString().ToUpper() = "TRUE" Or val.ToString().ToUpper() = "S") Then
                    CType(ctr, CheckBox).Checked = True
                Else
                    CType(ctr, CheckBox).Checked = False
                End If

            End If
        End Sub

        'Public Sub Parse_ControlsToParametersValues(ByRef container As Control, ByRef parameters As CmdParameterCollection, Optional ByVal controlPrefix As String = "Detail_", Optional ByVal parametersPrefix As String = "In_")
        '    Dim containerControls As Control() = SearchControls(container, controlPrefix)

        '    For Each ctr As Control In containerControls
        '        Dim colName As String = GetControlSufixName(controlPrefix, ctr.ID)
        '        If (colName = AudUsrColumnName) Then
        '            parameters(parametersPrefix & colName) = SessionData.CurrentUser.Login
        '        ElseIf (colName = AudUsrColumnName) Then
        '            parameters(parametersPrefix & colName) = DateTime.Now.ToString("yyyy-MM-dd")
        '        Else
        '            Dim ctrVal As Object = GetControlValue(ctr)
        '            parameters(parametersPrefix & colName) = ctrVal
        '        End If
        '    Next
        'End Sub

        Public Sub Parse_DataRowToControlsValues(ByRef container As Control, ByRef row As DataRow, Optional ByVal controlPrefix As String = "Detail_", Optional ByVal columnPrefix As String = "")
            Dim containerControls As Control() = SearchControls(container, controlPrefix)

            For Each ctr As Control In containerControls
                Dim colName As String = GetControlSufixName(controlPrefix, ctr.ID)
                SetControlValue(ctr, row(columnPrefix & colName))
            Next
        End Sub

        Public Sub ChangeControlEnableProperty(ByVal ctr As Control, ByVal Enable As Boolean)
            If ctr Is Nothing Then Return

            If TypeOf ctr Is TextBox Then
                CType(ctr, TextBox).Enabled = Enable

            ElseIf TypeOf ctr Is DropDownList Then
                CType(ctr, DropDownList).Enabled = Enable

            ElseIf TypeOf ctr Is ListBox Then
                CType(ctr, ListBox).Enabled = Enable

            ElseIf TypeOf ctr Is CheckBox Then
                CType(ctr, CheckBox).Enabled = Enable

            End If
        End Sub

        Public Overridable Sub Clear_Controls(ByRef container As Control, Optional ByVal prefixSection As String = "Detail_")

            Dim containerControls As Control() = SearchControls(container)


            For Each ctr As Control In containerControls
                If TypeOf ctr Is DropDownList Then
                    Try
                        CType(ctr, DropDownList).SelectedIndex = 0
                    Catch : End Try
                ElseIf TypeOf ctr Is ListBox Then
                    Try
                        CType(ctr, ListBox).SelectedIndex = 0
                    Catch : End Try
                ElseIf TypeOf ctr Is CheckBox Then
                    Try
                        CType(ctr, CheckBox).Checked = False
                    Catch : End Try
                Else
                    SetControlValue(ctr, DBNull.Value)
                End If
            Next

        End Sub

        Public Function FormatToParameterValue(ByVal ctr As CoreHourBox, Optional ByVal isEmptyToNull As Boolean = True) As Object
            Dim val As Object = DBNull.Value
            Try
                If (ctr.Text.Trim() = "") Then
                    val = IIf(isEmptyToNull, DBNull.Value, "")
                Else
                    val = ctr.ToDateString
                End If

            Catch ex As Exception
                val = DBNull.Value
            End Try
            Return val
        End Function

        Public Function FormatToParameterValue(ByVal ctr As CoreDateBox, Optional ByVal isEmptyToNull As Boolean = True) As Object
            Dim val As Object = DBNull.Value
            Try
                If (ctr.Text.Trim() = "") Then
                    val = IIf(isEmptyToNull, DBNull.Value, "")
                Else
                    val = ctr.ToDateString
                End If

            Catch ex As Exception
                val = DBNull.Value
            End Try
            Return val
        End Function

        Public Function FormatToParameterValue(ByVal ctr As TextBox, Optional ByVal isEmptyToNull As Boolean = True) As Object
            Dim val As Object = DBNull.Value
            Try
                If (ctr.Text = "") Then
                    val = IIf(isEmptyToNull, DBNull.Value, "")
                Else
                    val = ctr.Text
                End If

            Catch ex As Exception
                val = DBNull.Value
            End Try
            Return val
        End Function

        Public Function FormatToParameterValue(ByVal ctr As DropDownList, Optional ByVal doesEmptyRow As Boolean = False, Optional ByVal isBoundCOntrol As Boolean = True) As Object
            If (isBoundCOntrol) Then
                If (BindListControlInfoList.Contains(ctr.ID)) Then
                    doesEmptyRow = BindListControlInfoList(ctr.ID).AddEmptyRow
                End If
            End If

            Dim val As Object = DBNull.Value
            Try
                Dim minIndex As Integer = CInt(IIf(doesEmptyRow, 1, 0))
                If (ctr.SelectedIndex < minIndex) Then
                    val = DBNull.Value
                Else
                    val = ctr.SelectedValue
                End If

            Catch ex As Exception
                val = DBNull.Value
            End Try
            Return val
        End Function

        Public Function FormatToParameterValue(ByVal ctr As ListBox, Optional ByVal doesEmptyRow As Boolean = False, Optional ByVal isBoundCOntrol As Boolean = True) As Object
            If (isBoundCOntrol) Then
                If (BindListControlInfoList.Contains(ctr.ID)) Then
                    doesEmptyRow = BindListControlInfoList(ctr.ID).AddEmptyRow
                End If
            End If

            Dim val As Object = DBNull.Value
            Try
                Dim minIndex As Integer = CInt(IIf(doesEmptyRow, 1, 0))
                If (ctr.SelectedIndex < minIndex) Then
                    val = DBNull.Value
                Else
                    val = ctr.SelectedValue
                End If

            Catch ex As Exception
                val = DBNull.Value
            End Try
            Return val
        End Function

        Public Function FormatToParameterValue(ByVal ctr As CheckBox, Optional ByVal checkedValue As String = "S", Optional ByVal unCheckedValue As String = "N") As Object
            Dim val As Object = DBNull.Value
            Try
                val = IIf(ctr.Checked, checkedValue, unCheckedValue)
            Catch ex As Exception
                val = DBNull.Value
            End Try
            Return val
        End Function

        Public Function FormatToControlValue(ByVal value As Object, Optional ByVal Nullvalue As Object = "") As Object
            If value Is Nothing Then
                Return Nullvalue
            ElseIf value Is DBNull.Value Then
                Return Nullvalue
            ElseIf TypeOf value Is Date Then
                Return CType(value, Date).ToString("yyyy-MM-dd")
            ElseIf TypeOf value Is DateTime Then
                Return CType(value, DateTime).ToString("yyyy-MM-dd")
            Else
                Return value
            End If
        End Function

        Public Function GetInSaveType() As String
            'If (SaveType = Web_CMI.SaveType.Update) Then
            '    Return "U"
            'ElseIf (SaveType = Web_CMI.SaveType.Delete) Then
            '    Return "D"
            'Else
            '    Return "I"
            'End If
            Return ""
        End Function

        Public Function SearchControls(ByVal container As Control) As Control()
            Dim foundControls As New List(Of Control)

            For Each ctr As Control In container.Controls
                Try
                    foundControls.Add(ctr)
                Catch
                End Try

                Dim childControls As Control() = SearchControls(ctr)
                If (Not childControls Is Nothing And childControls.Length > 0) Then
                    foundControls.AddRange(childControls)
                End If

            Next

            Return foundControls.ToArray
        End Function

        Public Function SearchControls(ByVal container As Control, ByVal prefix As String) As Control()
            Dim foundControls As New List(Of Control)

            For Each ctr As Control In container.Controls
                Dim strPre As String = ""
                Try
                    strPre = ctr.ID.Substring(0, prefix.Length)
                    If (strPre.ToUpper() = prefix.ToUpper()) Then
                        foundControls.Add(ctr)
                    End If
                Catch
                End Try

                Dim childControls As Control() = SearchControls(ctr, prefix)
                If (Not childControls Is Nothing And childControls.Length > 0) Then
                    foundControls.AddRange(childControls)
                End If

            Next

            Return foundControls.ToArray
        End Function

        Public Function SearchControl(ByVal container As Control, ByVal controlId As String) As Control
            For Each ctr As Control In container.Controls
                Try
                    If (ctr.ID.ToUpper() = controlId.ToUpper()) Then
                        Return ctr
                    End If
                Catch : End Try

                Dim childControl As Control = SearchControl(ctr, controlId)
                If (Not childControl Is Nothing) Then
                    Return childControl
                End If
            Next

            Return Nothing
        End Function

        Public Function GetControlValue(ByVal ctr As Control) As Object
            Dim value As Object = Nothing

            If TypeOf ctr Is CoreHourBox Then
                value = FormatToParameterValue(CType(ctr, CoreHourBox))

            ElseIf TypeOf ctr Is CoreDateBox Then
                value = FormatToParameterValue(CType(ctr, CoreDateBox))

            ElseIf TypeOf ctr Is TextBox Then
                value = FormatToParameterValue(CType(ctr, TextBox))
                If (Not value Is Nothing And value.ToString().Contains("'")) Then
                    Throw New Exception("Caracter no válido! (') Valor = (" & value.ToString() & ")")
                End If

            ElseIf TypeOf ctr Is DropDownList Then
                value = FormatToParameterValue(CType(ctr, DropDownList))

            ElseIf TypeOf ctr Is ListBox Then
                value = FormatToParameterValue(CType(ctr, ListBox))

            ElseIf TypeOf ctr Is CheckBox Then
                value = FormatToParameterValue(CType(ctr, CheckBox))
            End If

            Return value
        End Function

        Public Function GetControlSufixName(ByVal prefix As String, ByVal controlId As String) As String
            Try
                Dim sufix As String = controlId.Substring(prefix.Length, controlId.Length - prefix.Length)
                Return sufix
            Catch
                Return ""
            End Try
        End Function
#End Region

#Region "TRANSMISION DE EVENTOS"

        Public Event CommandAction(ByVal Action As String)

        Public Event CommandActionNew()
        Public Event CommandActionFind()
        Public Event CommandActionEdit()
        Public Event CommandActionDelete()
        Public Event CommandActionSave()
        Public Event CommandActionExport()

        Public Event ModalClosed(ByVal parameters As String)

        Public Sub RaiseCommandAction(ByVal Action As String)
            RaiseEvent CommandAction(Action)

            Select Case Action
                Case "New"
                    RaiseEvent CommandActionNew()
                    MyMasterPage.MasterDetailPanel.Update()
                Case "Find"
                    RaiseEvent CommandActionFind()
                    MyMasterPage.MasterGridPanel.Update()
                Case "Edit"
                    RaiseEvent CommandActionEdit()
                    MyMasterPage.MasterGridPanel.Update()
                    MyMasterPage.MasterDetailPanel.Update()
                Case "Delete"
                    RaiseEvent CommandActionDelete()
                    MyMasterPage.MasterDetailPanel.Update()
                Case "Save"
                    RaiseEvent CommandActionSave()
                    MyMasterPage.MasterDetailPanel.Update()
                Case "Export"
                    RaiseEvent CommandActionExport()
                    MyMasterPage.MasterDetailPanel.Update()
            End Select
        End Sub

        Public Sub RaiseModalClosed(ByVal parameters As String)
            RaiseEvent ModalClosed(parameters)
        End Sub
#End Region

#Region "Enums"
        Public Enum MasterTabType
            Filter = 0
            Grid = 1
            Detail = 2
        End Enum
#End Region

#Region "Controles"

        ''' <summary>
        ''' Llena un DropDownList desde un origen de datos
        ''' </summary>
        ''' <param name="Combo">Nombre del control DropDownList Contenido en el GridView</param>
        ''' <param name="TableSource">Tabla que contiene el origen de datos</param>
        ''' <param name="value">Valor que tomara el DropDownList</param>
        ''' <param name="texto">Texto que se va a mostrar en el DropDownList</param>
        ''' <remarks></remarks>
        Public Sub Llenacombo(ByRef Combo As DropDownList, ByVal TableSource As DataTable, ByVal value As String, ByVal texto As String, Optional ByVal Activo As Boolean = True, Optional ByVal ColumnActive As String = "ACTIVO", Optional ByVal OrderBy As String = "", Optional ByVal Selection As Boolean = True, Optional ByVal ValueSel As String = "-1", Optional ByVal TextSel As String = "Seleccione...")
            Dim table As New DataTable

            Try
                table = clonarDataTable(TableSource)
            Catch ex As Exception
                table = Nothing
            End Try

            table = table.DefaultView.ToTable(True, value, texto)

            'agrega columna para ordenamiento
            Try
                Dim columna As New DataColumn("OrdenVS")
                columna.DefaultValue = 2
                table.Columns.Add(columna)
            Catch ex As Exception
            End Try

            'Si el origen de datos tiene la columna ACTIVO y la opcion de activo es True entonces filtra los registros en estado activo
            If Activo = True Then
                Try
                    Dim view As New DataView
                    view = table.DefaultView

                    Try
                        view.RowFilter = ColumnActive & "<>'I'"
                    Catch ex As Exception
                    End Try

                    table = view.ToTable
                Catch ex As Exception
                End Try
            End If

            'Agrega en el primer item la opcion de seleccione..
            If Selection = True Then
                Try
                    Dim row As DataRow = table.NewRow
                    row("OrdenVS") = 1
                    row(value) = ValueSel
                    row(texto) = TextSel
                    table.Rows.Add(row)
                Catch ex As Exception
                End Try

                Dim view2 As New DataView
                view2 = table.DefaultView
                view2.Sort = "OrdenVS"
                table = view2.ToTable
            End If

            'Ordena Ascendentemente una columna
            OrderBy = texto
            If OrderBy <> "" Then
                Try
                    Dim view3 As New DataView
                    view3 = table.DefaultView
                    view3.Sort = "OrdenVS," & OrderBy
                    table = view3.ToTable
                Catch ex As Exception
                End Try
            End If

            'Llena un DropDownList con un origen de datos
            Try
                With Combo
                    .DataSource = table
                    .DataValueField = value
                    .DataTextField = texto
                    .DataBind()
                End With
            Catch ex As Exception
            End Try
        End Sub

        Public Sub LlenaGrilla(ByRef GridView As CoreGridView, ByVal DataTable As DataTable, ByVal GridDataSession As Boolean, ByVal nTitle As Boolean)
            Try
                If nTitle = True Then
                    AddHandler GridView.RowDataBound, AddressOf CoreGridViewHeaders
                End If

                Dim dtSource As DataTable = DataTable

                dtSource = procesaDataTable(dtSource, ConnectionString.Core)

                If GridDataSession = True Then
                    GridData = dtSource
                End If

                GridView.DataSource = dtSource
                GridView.DataBind()
            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Sub LlenaGrilla(ByRef GridView As CoreGridView, ByVal DataTable As DataTable, ByVal ParamArray FilterColumns() As String)
            Try
                Dim table As DataTable = clonarDataTable(DataTable)
                LlenaGrilla(GridView, table.DefaultView.ToTable(True, FilterColumns), True, True)
            Catch ex As Exception
                Throw
            End Try
        End Sub
        Public Sub LlenaGrilla(ByRef GridView As CoreGridView, ByVal DataSource As Object)
            GridView.DataSource = DataSource
            GridView.DataBind()
        End Sub

        Public Sub CoreGridViewLinkControls(ByVal DataSource As DataTable, ByVal Registro As Integer, ByVal Contenedor As Object)
            Try
                Dim table As DataTable = DataSource

                If (table IsNot Nothing AndAlso table.Columns.Count > 0) Then
                    For i As Integer = 0 To table.Columns.Count - 1 Step 1
                        SetControlValue(Contenedor, table.Columns(i).Caption, table.Rows(Registro)(i))
                    Next
                End If

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Private Sub CoreGridViewHeaders(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            If e.Row.RowType = DataControlRowType.Header Then
                For Each Columna As TableCell In e.Row.Cells
                    Columna.Text = Columna.Text.Replace("fk_", "")
                    Columna.Text = Columna.Text.Replace("_", " ")
                Next
            End If
        End Sub

        Public Sub DropDownListCascading(ByRef ChildrenDrop As DropDownList, ByVal Table As DataTable, ByVal Value As String, ByVal Text As String, ByVal ParamArray FilteringString As String())
            Dim Seleccion As Integer = ChildrenDrop.SelectedIndex

            Dim View As DataView
            View = Table.DefaultView
            Try
                Dim Filtros As String = Join(FilteringString, " and ")
                View.RowFilter = Filtros
            Catch ex As Exception
            End Try

            Dim TablaSource As New DataTable
            Dim column As DataColumn

            column = New DataColumn(Value)
            TablaSource.Columns.Add(column)

            Try
                column = New DataColumn(Text)
                TablaSource.Columns.Add(column)
            Catch ex As Exception
            End Try

            For Each item As ListItem In ChildrenDrop.Items
                If item.Value = "-1" And item.Text = "Seleccione..." Then
                Else
                    Dim row As DataRow = TablaSource.NewRow
                    row(Value) = item.Value
                    row(Text) = item.Text
                    TablaSource.Rows.Add(row)
                End If
            Next

            Dim TableData As DataTable
            Try
                TableData = View.ToTable(True, Value, Text)
            Catch ex As Exception
                TableData = View.ToTable(True, Value)
            End Try

            If CompareTables(TableData, TablaSource) = False Then
                Llenacombo(ChildrenDrop, TableData, Value, Text)
            Else
                ChildrenDrop.SelectedIndex = Seleccion
            End If
        End Sub

        Public Function GetControlValue(ByVal contenedor As Object, ByVal Control_ As String) As Object
            Try
                Dim control As Object = CType(contenedor, UI.Control).FindControl(Control_)

                Dim valor As Object = DBNull.Value


                If control.ToString() = "System.Web.UI.WebControls.TextBox" Then
                    valor = CStr(CType(control, TextBox).Text)
                End If

                If control.ToString() = "System.Web.UI.WebControls.Label" Then
                    valor = CStr(CType(control, Label).Text)
                End If

                If control.ToString() = "System.Web.UI.WebControls.CheckBox" Then
                    valor = CStr(CBool(IIf(CType(control, CheckBox).Checked = True, 1, 0)))
                End If

                If control.ToString() = "System.Web.UI.WebControls.DropDownList" Then
                    valor = CStr(CType(control, DropDownList).SelectedValue)
                End If

                If control.ToString.IndexOf("DFecha", System.StringComparison.Ordinal) > 0 Then
                    Try
                        Dim fecha As Date = CDate(CType(control, Core.DFecha).Text)
                        Dim cadena As String = fecha.Year & "/" & fecha.Month & "/" & fecha.Day
                        valor = CStr(CDate(cadena))
                    Catch ex As Exception
                        valor = CType(control, Core.DFecha).Text
                    End Try
                End If

                If control.ToString.IndexOf("DTexto", System.StringComparison.Ordinal) > 0 Then
                    valor = CStr(CType(control, Core.DTexto).Text)
                End If

                If control.ToString.IndexOf("DNumber", System.StringComparison.Ordinal) > 0 Then
                    Try
                        valor = CStr(CInt(CType(control, Core.DNumber).Text))
                    Catch ex As Exception
                        valor = DBNull.Value
                    End Try
                End If

                If valor.ToString = "-1" Then valor = DBNull.Value
                If valor.ToString = "" Then valor = DBNull.Value
                Return valor
            Catch
                Return Nothing
            End Try
        End Function

        Public Sub SetControlValue(ByVal Container As Object, ByVal Control_ As String, ByVal valor As Object)
            Try
                Dim control As Object = CType(Container, UI.Control).FindControl(Control_)
                If IsDBNull(valor) Then
                    valor = ""
                End If
                Try
                    If control.ToString() = "System.Web.UI.WebControls.TextBox" Then
                        CType(control, TextBox).Text = CStr(valor)
                    End If

                    If control.ToString() = "System.Web.UI.WebControls.Label" Then
                        CType(control, Label).Text = CStr(valor)
                    End If

                    If control.ToString() = "System.Web.UI.WebControls.CheckBox" Then
                        CType(control, CheckBox).Checked = CBool(IIf(CStr(valor) = "", False, valor))
                    End If

                    If control.ToString() = "System.Web.UI.WebControls.DropDownList" Then
                        If CStr(valor) = "" Then
                            CType(control, DropDownList).SelectedIndex = -1
                        Else
                            Dim valorSplit As String() = CStr(valor).Split(CChar("-"))
                            CType(control, DropDownList).SelectedValue = valorSplit(0)
                        End If
                    End If

                    If control.ToString.IndexOf("DFecha", System.StringComparison.Ordinal) > 0 Then
                        Try
                            CType(control, Core.DFecha).Text = CStr(valor)
                        Catch ex As Exception
                            CType(control, Core.DFecha).Text = CStr(valor)
                        End Try
                    End If

                    If control.ToString.IndexOf("DTexto", System.StringComparison.Ordinal) > 0 Then
                        CType(control, Core.DTexto).Text = CStr(valor)
                    End If

                    If control.ToString.IndexOf("DNumber", System.StringComparison.Ordinal) > 0 Then
                        Dim valorSplit As String() = CStr(valor).Split(CChar("-"))

                        If valorSplit.Length > 1 Then
                            CType(control, Core.DNumber).Text = valorSplit(0)
                        Else
                            CType(control, Core.DNumber).Text = CStr(valor)
                        End If

                    End If

                Catch
                End Try
            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Sub SetControlEnabled(ByVal Container As Object, ByVal Control_ As String, ByVal valor As Boolean)
            Try
                Dim control As Object = CType(Container, UI.Control).FindControl(Control_)

                If control.ToString() = "System.Web.UI.WebControls.TextBox" Then
                    CType(control, TextBox).Enabled = valor
                End If

                If control.ToString() = "System.Web.UI.WebControls.Label" Then
                    CType(control, Label).Enabled = valor
                End If

                If control.ToString() = "System.Web.UI.WebControls.CheckBox" Then
                    CType(control, CheckBox).Enabled = valor
                End If

                If control.ToString() = "System.Web.UI.WebControls.DropDownList" Then
                    CType(control, DropDownList).Enabled = valor
                End If

                If control.ToString.IndexOf("DFecha", System.StringComparison.Ordinal) > 0 Then
                    CType(control, Core.DFecha).Enabled = valor
                End If

                If control.ToString.IndexOf("DTexto", System.StringComparison.Ordinal) > 0 Then
                    CType(control, Core.DTexto).Enabled = valor
                End If

                If control.ToString.IndexOf("DNumber", System.StringComparison.Ordinal) > 0 Then
                    CType(control, Core.DNumber).Enabled = valor
                End If

            Catch ex As Exception
                Throw New Exception()
            End Try
        End Sub

        Public Sub FillCheckBoxList(ByRef CheckControl As CheckBoxList, ByVal DataSource As DataTable, ByVal Value As String, ByVal Text As String)
            Dim Table As DataTable = clonarDataTable(DataSource)
            Dim view As DataView = Table.DefaultView

            Try
                Table = view.ToTable(True, Value, Text)
            Catch ex As Exception
                Table = view.ToTable(True, Value)
            End Try

            CheckControl.DataSource = Table
            CheckControl.DataValueField = Value
            CheckControl.DataTextField = Text

            Try
                CheckControl.DataBind()
            Catch ex As Exception
                Throw New Exception("No se puede enlazar el control '" & CheckControl.ID & "' con el origen de datos.")
            End Try

        End Sub
        Public Sub FillCheckBoxList(ByRef CheckControl As CheckBoxList, ByVal DataSource As DataTable, ByVal Value As String, ByVal Text As String, ByVal Check As String)
            FillCheckBoxList(CheckControl, DataSource, Value, Text)

            Try
                For i As Integer = 0 To DataSource.Rows.Count - 1 Step 1
                    CheckControl.Items(i).Selected = CBool(DataSource.Rows(i)(Check).ToString)
                Next
            Catch ex As Exception
                Throw New Exception("El valor Check no se ha encontrado o no puede ser convertido a booleano.")
            End Try

            CheckControl.CssClass = "Label"
        End Sub
        Public Sub FillCheckBoxList(ByRef CheckControl As CheckBoxList, ByVal DataSource As DataTable, ByVal Value As String, ByVal Text As String, ByVal Check As String, ByVal Enabled As String)
            FillCheckBoxList(CheckControl, DataSource, Value, Text, Check)

            Try
                For i As Integer = 0 To DataSource.Rows.Count - 1
                    CheckControl.Items(i).Enabled = CBool(DataSource.Rows(i)(Enabled).ToString)
                Next
            Catch ex As Exception
                Throw New Exception("El valor Enabled no se ha encontrado o no puede ser convertido a booleano.")
            End Try

            CheckControl.CssClass = "Label"
        End Sub

        Public Sub FillCheckBoxList(ByRef CheckControl As CheckBoxList, ByVal DataSource As DataTable, ByVal Value As String, ByVal Text As String, ByVal Check As String, ByVal Enabled As String, ByVal ParamArray FilteringString As String())
            Dim View As DataView = DataSource.DefaultView

            Try
                Dim Filtros As String = Join(FilteringString, " and ")
                View.RowFilter = Filtros
            Catch ex As Exception
            End Try

            'Dim TablaSource As New DataTable
            'Dim column As DataColumn

            'column = New DataColumn(Value)
            'TablaSource.Columns.Add(column)

            'Try
            '    column = New DataColumn(Text)
            '    TablaSource.Columns.Add(column)
            'Catch ex As Exception
            'End Try

            'For Each item As ListItem In CheckControl.Items
            '    If item.Value <> "-1" And item.Text <> "Seleccione..." Then
            '        Dim row As DataRow = TablaSource.NewRow
            '        row(Value) = item.Value
            '        row(Text) = item.Text
            '        TablaSource.Rows.Add(row)
            '    End If
            'Next

            Dim TableData As DataTable = View.ToTable

            FillCheckBoxList(CheckControl, TableData, Value, Text, Check, Enabled)

        End Sub


#Region "Tables"

        Public Function clonarDataTable(ByVal dtIn As DataTable) As DataTable
            Dim dtClonado As New DataTable
            Dim rowOut As DataRow

            Try

                'Crear columnas al nuevo datatable
                For Each col As DataColumn In dtIn.Columns
                    dtClonado.Columns.Add(col.ColumnName, GetType(String))
                Next

                For Each row As DataRow In dtIn.Rows
                    rowOut = dtClonado.NewRow()
                    For i As Integer = 0 To row.ItemArray.Count - 1
                        rowOut(i) = row(i)
                    Next
                    dtClonado.Rows.Add(rowOut)
                Next
            Catch ex As Exception
                Throw
            End Try

            Return dtClonado
        End Function

        'Funcion para almacenar y obtener valores ya registrados en el recorrido del datatable
        Public Function gestionCampoNombre(ByRef data As DataTable, ByVal opcion As Short, ByVal Nombre_Columna As String, ByVal Valor As String, Optional ByVal ValorText As String = "") As String
            Dim sReturn As String = ""
            Try
                If opcion = 1 Then 'Insertar registro
                    Dim dr As DataRow = data.NewRow()
                    dr.Item("Nombre_Columna") = Nombre_Columna
                    dr.Item("Valor") = Valor
                    dr.Item("ValorText") = ValorText
                    data.Rows.Add(dr)
                Else ' Busqueda
                    For Each element As DataRow In data.Rows
                        If element("Nombre_Columna").ToString() = Nombre_Columna And element("Valor").ToString() = Valor Then
                            sReturn = element("ValorText").ToString()
                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception
                Throw
            End Try
            Return sReturn
        End Function

        Public Function procesaDataTable(ByVal data As DataTable, ByVal Conexion As String) As DataTable
            Dim dtReturn As New DataTable
            Dim sValorCampo As String = ""

            'DT para almacenar Nombre_Columna, Valor, ValorText
            Dim dtCampoNombre As New DataTable

            Dim dcNombreColumna As New DataColumn("Nombre_Columna")
            Dim dcValor As New DataColumn("Valor")
            Dim dcValorText As New DataColumn("ValorText")

            dtCampoNombre.Columns.Add(dcNombreColumna)
            dtCampoNombre.Columns.Add(dcValor)
            dtCampoNombre.Columns.Add(dcValorText)

            Try
                'Se clona el datatable
                data = clonarDataTable(data)

                For Each k As DataRow In data.Rows
                    For i As Integer = 0 To k.ItemArray.Count - 1
                        If data.Columns(i).ToString().IndexOf("fk_", System.StringComparison.Ordinal) > -1 Then
                            'Realiza la búsqueda del nombre de los fk
                            Dim sResultBus As String = gestionCampoNombre(dtCampoNombre, 0, data.Columns(i).ToString(), k.Item(i).ToString(), "")
                            If sResultBus <> "" Then
                                sValorCampo = sResultBus
                            Else
                                sValorCampo = existeTablaCampo(data.Columns(i).ToString().Replace("fk_", ""), k.Item(i).ToString(), Conexion, k)
                                gestionCampoNombre(dtCampoNombre, 1, data.Columns(i).ToString(), k.Item(i).ToString(), sValorCampo)
                            End If

                            If sValorCampo <> "" Then
                                k.Item(i) = k.Item(i).ToString & "-" & sValorCampo
                            End If
                        End If
                    Next
                Next
                dtReturn = data
            Catch ex As Exception
                Throw
            End Try

            Return dtReturn
        End Function

        Public Function existeTablaCampo(ByVal sNombreTabla As String, ByVal valorLlave As String, ByVal Conexion As String, ByVal row As DataRow) As String
            Dim tTabla As DataTable
            Dim tColumnas As DataTable
            Dim tValorCampo As DataTable
            Dim bRetornar As String = ""
            Dim sTipoObjeto As String = ""
            Dim sSchema As String = ""
            Dim sSQL As String = ""
            Dim dm As DBCoreDataBaseManager
            dm = New DBCoreDataBaseManager(Conexion)

            Try
                tTabla = dm.DataBase.ExecuteQueryGet("SELECT TABLE_NAME NAME, TABLE_SCHEMA [SCHEMA], TABLE_TYPE FROM Information_Schema.Tables WHERE TABLE_TYPE IN ('BASE TABLE','VIEW') AND TABLE_NAME IN ('TBL_" & sNombreTabla & "','CTA_" & sNombreTabla & "') ORDER BY TABLE_SCHEMA, TABLE_NAME ASC")

                'Si existen Registros, se busca el campo Nombre
                If tTabla.Rows.Count > 0 Then
                    sSchema = tTabla.Rows(0).Item("SCHEMA").ToString()
                    tColumnas = dm.DataBase.ExecuteQueryGet("SELECT SC.ID, SC.NAME, SO.XTYPE FROM SYSCOLUMNS SC, SYSOBJECTS SO WHERE SC.ID = SO.ID AND SO.XTYPE IN ('U','V') AND SO.NAME IN ('TBL_" & sNombreTabla & "','CTA_" & sNombreTabla & "')")
                    If tColumnas.Rows.Count > 0 Then
                        For Each Campo As DataRow In tColumnas.Rows
                            sTipoObjeto = Campo.Item("XTYPE").ToString().Trim()

                            If Campo.Item("NAME").ToString = "Nombre_" & sNombreTabla And valorLlave <> "" Then
                                sSQL = "SELECT Nombre_" & sNombreTabla & " FROM " & sSchema
                                If sTipoObjeto = "U" Then
                                    sSQL &= ".TBL_" & sNombreTabla
                                Else
                                    sSQL &= ".CTA_" & sNombreTabla
                                End If
                                sSQL &= " WHERE ID_" & sNombreTabla & "=" & valorLlave

                                'Si en el DataRow se encuentra un campo fk_Entidad, y en la tabla actual tambien, se realiza el filtro por esta.
                                If sTipoObjeto = "V" And sNombreTabla <> "Entidad" Then
                                    Dim fk_entidad As String = ""
                                    Try
                                        If Not IsNothing(row.Item("fk_entidad")) Then
                                            fk_entidad = row.Item("fk_entidad").ToString()

                                            If fk_entidad <> "" Then
                                                sSQL &= " AND FK_ENTIDAD =" & fk_entidad.Split(CChar("-"))(0)
                                            End If
                                        End If
                                    Catch : End Try
                                End If


                                tValorCampo = dm.DataBase.ExecuteQueryGet(sSQL)

                                If tValorCampo.Rows.Count > 0 Then
                                    bRetornar = tValorCampo.Rows(0).Item(0).ToString()
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                Throw
            End Try
            Return bRetornar
        End Function

        ''' <summary>
        ''' Funcion que compara Cantidad de registros, columnas, valores de dos Datatable
        ''' </summary>
        ''' <param name="Table1">Tabla 1</param>
        ''' <param name="Table2">Tabla 2</param>
        ''' <returns>Si las tablas son iguales True de lo contrario False</returns>
        ''' <remarks></remarks>
        Public Function CompareTables(ByVal Table1 As DataTable, ByVal Table2 As DataTable) As Boolean
            Dim Iguales As Boolean = True

            Try
                'Ordena en las tablas cada uno de los registros para validar que existan las mismas columnas
                'y los registros esten en una posicion igual
                Dim view As DataView = clonarDataTable(Table1).DefaultView
                Dim view2 As DataView = clonarDataTable(Table2).DefaultView
                Dim OrderVS(Table1.Columns.Count - 1) As String
                Dim count As Integer = 0
                For Each column As DataColumn In Table1.Columns
                    OrderVS(count) = column.ColumnName
                    count += 1
                Next
                view.Sort = Join(OrderVS, ",")
                view2.Sort = Join(OrderVS, ",")
                Table1 = view.ToTable
                Table2 = view2.ToTable

                'Valida el numero de filas de las tablas
                If Table1.Rows.Count <> Table2.Rows.Count Then
                    Iguales = False
                End If

                'valida el numero de columnas de las tablas
                If Table1.Columns.Count <> Table2.Columns.Count Then
                    Iguales = False
                End If

                'valida cada uno de los valores que tiene la tabla
                For i As Integer = 0 To Table1.Rows.Count - 1 Step 1
                    For j As Integer = 0 To Table1.Columns.Count - 1 Step 1
                        If Table1.Rows(i)(j).ToString.ToUpper.Trim <> Table2.Rows(i)(j).ToString.ToUpper.Trim Then
                            Iguales = False
                        End If
                    Next
                Next

            Catch ex As Exception
                Iguales = False
            End Try

            Return Iguales
        End Function

#End Region

#Region "Conversion de datos y variables globales"

        Public Function DDataSet(ByVal Table As DataTable) As DataSet
            Dim data As New DataSet
            data.Tables.Add(Table)
            Return data
        End Function

        Public Property GlobalParameterCollection() As ParameterCollection
            Get
                Return CType(Session("GlobalParameterCollection"), ParameterCollection)
            End Get
            Set(ByVal value As ParameterCollection)
                Session("GlobalParameterCollection") = value
            End Set
        End Property

        Public Function DDataSet(ByVal Datasource As Object) As DataSet
            Try
                Return CType(Datasource, DataSet)
            Catch ex As Exception
                Throw New Exception()
            End Try
        End Function

        Public Function DDataView(ByVal Datasource As Object, ByVal tableNum As Integer) As DataView
            Try
                Return CType(Datasource, DataSet).Tables(tableNum).DefaultView
            Catch ex As Exception
                Throw New Exception()
            End Try
        End Function

        Public Function DDataTable(ByVal Datasource As Object, ByVal tableNum As Integer) As DataTable
            Try
                Return CType(Datasource, DataSet).Tables(tableNum)
            Catch ex As Exception
                Throw New Exception()
            End Try
        End Function

        Public Property GlobalData() As DataSet
            Get
                Return CType(Session("GlobalDataSet"), DataSet)
            End Get
            Set(ByVal value As DataSet)
                Session("GlobalDataSet") = value
            End Set
        End Property

        Public Property MyMaster As FormMasterPage
            Get
                Return _myMaster1
            End Get
            Set(value As FormMasterPage)
                _myMaster1 = value
            End Set
        End Property

        Private Function DBTypeToVSType(ByVal Tipo As String) As Object
            Dim tipo_ As Object = DbType.String

            Tipo = Tipo.ToUpper

            For Each a In Strings
                If a = Tipo Then tipo_ = DbType.String
            Next

            For Each a In Numbers
                If a = Tipo Then tipo_ = DbType.Int64
            Next

            For Each a In Dates
                If a = Tipo Then tipo_ = DbType.Date
            Next

            For Each a In Booleans
                If a = Tipo Then tipo_ = DbType.Boolean
            Next

            Return tipo_
        End Function

        Private Function DBTypeToVSType(ByVal Tipo As String, ByVal valor As Object) As Object
            Dim valor_ As Object = Nothing
            Tipo = Tipo.ToUpper

            For Each a In Strings
                If a = Tipo Then valor_ = DStr(valor)
            Next

            For Each a In Numbers
                If a = Tipo Then valor_ = Dlng(valor)
            Next

            Select Case Tipo
                Case "INT"
                    valor_ = DInt(valor)
            End Select

            For Each a In Dates
                If a = Tipo Then valor_ = DDate(valor)
            Next

            For Each a In Booleans
                If a = Tipo Then valor_ = CBool(valor)
            Next

            Return valor_
        End Function

        Private Function DBTypeToVSType2(ByVal Tipo As String) As DbType
            Dim valor_ As Object = Nothing

            Select Case Tipo.ToUpper()
                Case "VARCHAR", "VARCHAR2", "NVARCHAR", "NCHAR", "CHAR", "SQL_VARIANT"
                    valor_ = DbType.String
                Case "BIT", "BOOLEAN"
                    valor_ = DbType.Boolean
                Case "NUMERIC", "NUMBER", "INT", "TINYINT", "SMALLINT"
                    valor_ = DbType.Int32
                    'Case "TINYINT", "SMALLINT"
                    '    valor_ = DbType.Int16
                Case "BIGINT"
                    valor_ = DbType.Int64
                Case "DATETIME", "DATE"
                    valor_ = DbType.DateTime
            End Select

            Return valor_
        End Function

#End Region

#Region "Funciones para la DAL"

        ''' <summary>
        ''' Convierte un valor a Slyg.Tools.SlygNullable(Of Long) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="Value">Valor a convertir</param>
        ''' <returns>Valor Tipo Slyg.Tools.SlygNullable(Of Long)</returns>
        ''' <remarks></remarks>
        Public Function Dlng(ByVal Value As Object) As Slyg.Tools.SlygNullable(Of Long)

            Try
                If IsDBNull(Value) = True Then
                    Value = DBNull.Value
                End If
            Catch ex As Exception
            End Try


            Dim valor As Slyg.Tools.SlygNullable(Of Long)
            If Value Is Nothing Or Value Is "" Then
                valor = DBNull.Value
            Else

                Try
                    If Value.ToString = "-1" Then
                        valor = DBNull.Value
                    Else
                        Try
                            valor = CLng(Value)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Long)")
                        End Try
                    End If
                Catch ex As Exception
                    valor = DBNull.Value
                End Try

            End If
            Return valor
        End Function

        ''' <summary>
        ''' Convierte un valor a Slyg.Tools.SlygNullable(Of Date) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="Value">Valor a convertir</param>
        ''' <returns>Valor Tipo Slyg.Tools.SlygNullable(Of Date)</returns>
        ''' <remarks></remarks>
        Public Function DDate(ByVal Value As Object) As Slyg.Tools.SlygNullable(Of Date)

            Try
                If IsDBNull(Value) = True Then
                    Value = DBNull.Value
                End If
            Catch ex As Exception
            End Try

            If Value Is Nothing Or Value Is "" Then
                Return DBNull.Value
            Else

                Try
                    If Value.ToString = "-1" Then
                        Return DBNull.Value
                    End If
                Catch ex As Exception
                End Try

                Try
                    Return CDate(Value)
                Catch ex As Exception
                    Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Date)")
                End Try
            End If
        End Function

        ''' <summary>
        ''' Convierte un valor a Slyg.Tools.SlygNullable(Of String) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="Value">Valor a convertir</param>
        ''' <returns>Valor Tipo Slyg.Tools.SlygNullable(Of String)</returns>
        ''' <remarks></remarks>
        Public Function DStr(ByVal Value As Object) As Slyg.Tools.SlygNullable(Of String)
            Dim Valor As New Slyg.Tools.SlygNullable(Of String)(DBNull.Value)

            If Not IsDBNull(Value) Or Value.ToString = "-1" Then
                If Value Is Nothing Or Value Is "" Then
                    Valor = Nothing
                Else
                    Try
                        Valor = CStr(Value)
                    Catch ex As Exception
                        Throw New Exception("El Valor no se puede Convertir a System.OblectNullable(of String)")
                    End Try
                End If
            End If

            Return Valor
        End Function

        ''' <summary>
        ''' Convierte un valor a Slyg.Tools.SlygNullable(Of Integer) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="Value">Valor a convertir</param>
        ''' <returns>Valor Tipo Slyg.Tools.SlygNullable(Of Integer)</returns>
        ''' <remarks></remarks>
        Public Function DInt(ByVal Value As Object) As Slyg.Tools.SlygNullable(Of Integer)

            Try
                If IsDBNull(Value) = True Then
                    Value = Nothing
                End If
            Catch ex As Exception
            End Try


            Dim valor As Slyg.Tools.SlygNullable(Of Integer)
            If Value Is Nothing Or Value Is "" Then
                valor = Nothing
            Else

                Try
                    If Value.ToString = "-1" Then
                        valor = Nothing
                    Else
                        Try
                            valor = CInt(Value)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Integer)")
                        End Try
                    End If
                Catch ex As Exception
                    valor = Nothing
                End Try

            End If
            Return valor
        End Function

        ''' <summary>
        ''' Convierte un valor a Slyg.Tools.SlygNullable(Of Integer) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="Value">Valor a convertir</param>
        ''' <returns>Valor Tipo Slyg.Tools.SlygNullable(Of Integer)</returns>
        ''' <remarks></remarks>
        Public Function DByte(ByVal Value As Object) As Slyg.Tools.SlygNullable(Of Byte)

            Try
                If IsDBNull(Value) = True Then
                    Value = Nothing
                End If
            Catch ex As Exception
            End Try


            Dim valor As Slyg.Tools.SlygNullable(Of Byte)
            If Value Is Nothing Or Value Is "" Then
                valor = Nothing
            Else

                Try
                    If Value.ToString = "-1" Then
                        valor = Nothing
                    Else
                        Try
                            valor = CByte(Value)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Byte)")
                        End Try
                    End If
                Catch ex As Exception
                    valor = Nothing
                End Try

            End If
            Return valor
        End Function

        ''' <summary>
        ''' Convierte un valor a Slyg.Tools.SlygNullable(Of Short) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="Value">Valor a convertir</param>
        ''' <returns>Valor Tipo Slyg.Tools.SlygNullable(Of Short)</returns>
        ''' <remarks></remarks>
        Public Function DShort(ByVal Value As Object) As Slyg.Tools.SlygNullable(Of Short)

            Try
                If IsDBNull(Value) = True Then
                    Value = Nothing
                End If
            Catch ex As Exception
            End Try


            Dim valor As Slyg.Tools.SlygNullable(Of Short)
            If Value Is Nothing Or Value Is "" Then
                valor = Nothing
            Else

                Try
                    If Value.ToString = "-1" Then
                        valor = Nothing
                    Else
                        Try
                            valor = CShort(Value)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Long)")
                        End Try
                    End If
                Catch ex As Exception
                    valor = Nothing
                End Try

            End If
            Return valor
        End Function

#End Region

#End Region

#Region "DB Manager Automatic"

#Region "Variables"

        Private Const DBTabla As String = "Tabla"
        Private Const DBColumna As String = "Columna"
        Private Const DBTablaRef As String = "TablaRef"
        Private Const DBEsquemaRef As String = "esquemaRef"
        Private Const DBColumnaRef As String = "ColumnaRef"
        Private Const DBReferencia As String = "Referencia"
        Private Const DBLlavePrimaria As String = "LlavePrimaria"
        Private Const DBNulo As String = "Nulo"
        Private Const DBTipo As String = "TipoDato"
        Private Const DBEsquema As String = "Esquema"
        Private Const DBLongitud As String = "Longitud"
        Private Const DBPresicion As String = "PrecisionNumerica"

        Private Strings As String() = {"VARCHAR", "VARCHAR2", "NVARCHAR", "NCHAR", "CHAR", "SQL_VARIANT"}
        Private Numbers As String() = {"NUMERIC", "SMALLINT", "INT", "NUMBER", "TINYINT", "BIGINT"}
        Private Booleans As String() = {"BIT", "BOOLEAN"}
        Private Dates As String() = {"DATETIME", "DATE"}
#End Region

        Public Function DataDictionary(ByVal Schema As String, ByVal Table As String) As DataTable
            Dim Ordenador = New Schemadbo.CTA_DiccionarioDatosEnumList()
            Ordenador.Add(Schemadbo.CTA_DiccionarioDatosEnum.PosicionColumna, True)
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim TableData As DataTable = Nothing
            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                TableData = (dbmCore.Schemadbo.CTA_DiccionarioDatos.DBFindByTablaEsquema(Table, Schema, 0, Ordenador))
                TableData = TableData.DefaultView.ToTable(True, DBPresicion, DBLongitud, DBEsquema, DBTipo, DBNulo, DBLlavePrimaria, DBTabla, DBColumna)
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
            Return TableData
        End Function

        Public Sub ValidationInit(ByVal Container As Object, ByVal TableDictionary As DataTable)
            For Each row As DataRow In TableDictionary.Rows
                Dim control As Object = CType(Container, UI.Control).FindControl(row(DBColumna).ToString)

                If control.ToString.IndexOf("DNumber", System.StringComparison.Ordinal) > 0 Then
                    If row(DBColumna).ToString.ToUpper.Substring(0, 3) <> "ID_" Then
                        Dim Number As DNumber = CType(control, DNumber)
                        Number.IsRequiered = Not CBool(row(DBNulo))
                        Number.ValidationGroup = "Guardar"
                    End If
                End If

                If control.ToString.IndexOf("DTexto", System.StringComparison.Ordinal) > 0 Then
                    Dim Text_ As DTexto = CType(control, DTexto)
                    Text_.IsRequiered = Not CBool(row(DBNulo))
                    Text_.ValidationGroup = "Guardar"
                End If

                If control.ToString.IndexOf("DFecha", System.StringComparison.Ordinal) > 0 Then
                    Dim Fecha As DFecha = CType(control, DFecha)
                    Fecha.IsRequiered = Not CBool(row(DBNulo))
                    Fecha.ValidationGroup = "Guardar"
                End If
            Next
        End Sub

        Public Sub LimpiarControles(ByVal Container As Object, ByVal TableDictionary As DataTable, ByVal Entidad As String)
            Dim table As DataTable = TableDictionary

            For Each row As DataRow In table.Rows
                If row(DBColumna).ToString.ToUpper <> "FK_ENTIDAD" Then
                    SetControlValue(Container, row(DBColumna).ToString.ToUpper, "")
                Else
                    SetControlValue(Container, row(DBColumna).ToString.ToUpper, Entidad)
                End If

                Dim control As New DNumber
                control.ID = "XXX"
                Try
                    Try
                        control = CType(CType(Container, Control).FindControl(row(DBColumna).ToString), DNumber)
                    Catch : End Try

                    Dim EnabledControl As DataView = table.DefaultView
                    EnabledControl.RowFilter = DBColumna & "='" & row(DBColumna).ToString.ToUpper & "'"

                    If control.ID.ToUpper.Substring(0, 3) <> "ID_" Then
                        SetControlEnabled(Container, row(DBColumna).ToString.ToUpper, True)
                    End If
                Catch : End Try

            Next

        End Sub

#Region "DB"

        Public Function NextId(ByVal NombreTabla As String, ByVal campoId As String, ByVal sConnString As String, ByVal ParamArray arrayParametros() As String) As Integer
            Dim dm As DBCoreDataBaseManager

            Dim nReturn As Integer = 0
            Dim sSQL As New StringBuilder
            Dim sNombreCampo As String = ""
            Dim sValorCampo As String = ""
            Dim nContador = 0

            Try
                dm = New DBCoreDataBaseManager(sConnString)

                'arrayParametros debe llegan en formato NombreCampo|ValorCampo
                sSQL.Append("SELECT ISNULL(MAX(" & campoId & ")+1, 1) AS NEXTID ")
                sSQL.Append("FROM " & NombreTabla & " ")

                For Each sCampo As String In arrayParametros
                    Dim arrParametro() As String = arrayParametros(0).Split(CChar("|"))
                    sNombreCampo = arrParametro(0)
                    sValorCampo = arrParametro(1)

                    If nContador = 0 Then
                        sSQL.Append("WHERE " & sNombreCampo & "='" & sValorCampo & "' ")
                    Else
                        sSQL.Append("AND " & sNombreCampo & "='" & sValorCampo & "' ")
                    End If
                    nContador += 1
                Next

                dm.Connection_Open(MySesion.Usuario.id)
                Dim dtResult As DataTable = dm.DataBase.ExecuteQueryGet(sSQL.ToString())
                If dtResult.Rows.Count > 0 Then
                    nReturn = CInt(dtResult.Rows(0).Item(0).ToString())
                End If
                dm.Connection_Close()
            Catch ex As Exception
                Throw
            End Try
            Return nReturn
        End Function

        Public Function CreateInsert(ByVal nUsuario As Integer, ByVal Container As Object, ByVal TableDictionary As DataTable) As Integer
            Return CreateInsert(nUsuario, Container, TableDictionary, ConnectionString.Core)
        End Function

        Public Function CreateInsert(ByVal nUsuario As Integer, ByVal Container As Object, ByVal TableDictionary As DataTable, ByVal conexion As String) As Integer
            Dim Result As Boolean
            Dim excepcion_ As New System.Exception
            Dim NextId As Integer
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(conexion)
                dbmCore.Connection_Open(nUsuario)
                dbmCore.Transaction_Begin()

                Dim table As DataTable = TableDictionary
                Dim nInParams As New List(Of Slyg.Data.Schemas.Parameter)
                Dim nKeys As New List(Of Slyg.Data.Schemas.Parameter)
                Dim KeysSelect As New ParameterCollection

                Dim view As DataView = table.DefaultView
                view.RowFilter = DBLlavePrimaria & "=1"
                Dim tablePrimary As DataTable = view.ToTable

                For Each row1 In tablePrimary.Rows
                    Dim row As DataRow = CType(row1, DataRow)

                    If row(DBColumna).ToString.ToUpper.IndexOf("ID_", System.StringComparison.Ordinal) = -1 Then
                        If GetControlValue(Container, row(DBColumna).ToString).ToString <> "" Then
                            nKeys.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(row(DBTipo).ToString), DbType), CStr(row(DBTipo)), DBTypeToVSType(row(DBTipo).ToString, GetControlValue(Container, row(DBColumna).ToString)), False, 0, 10, 0, ParameterDirection.Input))
                            KeysSelect.Add(row(DBColumna).ToString, GetControlValue(Container, row(DBColumna).ToString).ToString)
                        End If
                    End If
                Next

                For Each row1 In table.Rows
                    Dim row As DataRow = CType(row1, DataRow)
                    If row(DBColumna).ToString.ToUpper.IndexOf("ID_", System.StringComparison.Ordinal) = -1 Then
                        If (Not IsDBNull(GetControlValue(Container, row(DBColumna).ToString())) And row(DBColumna).ToString <> "Fecha_Log") Then
                            nInParams.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(row(DBTipo).ToString), DbType), CStr(row(DBTipo)), DBTypeToVSType(CStr(row(DBTipo)), GetControlValue(Container, row(DBColumna).ToString)), False, 0, 10, 0, ParameterDirection.Input))
                        ElseIf row(DBColumna).ToString = "Fecha_Log" Then
                            nInParams.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(row(DBTipo).ToString), DbType), CStr(row(DBTipo)), SlygNullable.SysDate, False, 0, 10, 0, ParameterDirection.Input))
                        End If
                    End If
                Next

                For Each row1 In table.Rows
                    Dim row As DataRow = CType(row1, DataRow)

                    If row(DBColumna).ToString.ToUpper.IndexOf("ID_", System.StringComparison.Ordinal) >= 0 Then
                        If IsDBNull(GetControlValue(Container, row(DBColumna).ToString())) Then
                            NextId = dbmCore.DataBase.DBNextId(CStr(table.Rows(0)(DBEsquema)), CStr(table.Rows(0)(DBTabla)), nKeys, row(DBColumna).ToString, Result, excepcion_)
                            SetControlValue(Container, row(DBColumna).ToString, NextId)
                        End If

                        nInParams.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(CStr(row(DBTipo))), DbType), CStr(row(DBTipo)), DBTypeToVSType(CStr(row(DBTipo)), GetControlValue(Container, row(DBColumna).ToString)), False, 0, 10, 0, ParameterDirection.Input))
                    End If

                Next

                dbmCore.DataBase.DBInsert(table.Rows(0)(DBEsquema).ToString, table.Rows(0)(DBTabla).ToString, nInParams, Result, excepcion_)
                If Not Result Then Throw excepcion_

                If dbmCore IsNot Nothing Then dbmCore.Transaction_Commit()

            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

            Return NextId
        End Function

        Public Sub CreateUpdate(ByVal nUsuario As Integer, ByVal Container As Object, ByVal TableDictionary As DataTable)
            CreateUpdate(nUsuario, Container, TableDictionary, ConnectionString.Core)
        End Sub
        Public Sub CreateUpdate(ByVal nUsuario As Integer, ByVal Container As Object, ByVal TableDictionary As DataTable, ByVal Conexion As String)
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim Result As Boolean
            Try
                dbmCore = New DBCoreDataBaseManager(Conexion)
                dbmCore.Connection_Open(nUsuario)
                dbmCore.Transaction_Begin()

                Dim table As DataTable = TableDictionary
                Dim nInParams As New List(Of Slyg.Data.Schemas.Parameter)

                Dim excepcion As New System.Exception
                Dim nKeys As New List(Of Slyg.Data.Schemas.Parameter)

                Dim view As DataView = table.DefaultView
                view.RowFilter = DBLlavePrimaria & "=1"
                Dim tablePrimary As DataTable = view.ToTable

                For Each row As DataRow In table.Rows
                    If (Not IsDBNull(GetControlValue(Container, row(DBColumna).ToString())) And row(DBColumna).ToString <> "Fecha_Log") Then
                        nInParams.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(row(DBTipo).ToString), DbType), CStr(row(DBTipo)), DBTypeToVSType(CStr(row(DBTipo)), GetControlValue(Container, row(DBColumna).ToString)), False, 0, 10, 0, ParameterDirection.Input))
                    ElseIf row(DBColumna).ToString = "Fecha_Log" Then
                        nInParams.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(row(DBTipo).ToString), DbType), CStr(row(DBTipo)), SlygNullable.SysDate, False, 0, 10, 0, ParameterDirection.Input))
                    End If
                Next
                
                For Each row1 In tablePrimary.Rows
                    Dim row As DataRow = CType(row1, DataRow)
                    Dim Value As Object = DBTypeToVSType(CStr(row(DBTipo)), GetControlValue(Container, row(DBColumna).ToString))
                    Dim Name As String = row(DBColumna).ToString
                    Dim Type As DbType = DBTypeToVSType2(CStr(row(DBTipo)))
                    Dim TypeSpecific = CStr(row(DBTipo))

                    nKeys.Add(New Slyg.Data.Schemas.Parameter(Name, Type, TypeSpecific, Value, False, 0, 10, 0, ParameterDirection.Input))
                Next

                dbmCore.DataBase.DBUpdate(table.Rows(0)(DBEsquema).ToString, table.Rows(0)(DBTabla).ToString, nKeys, nInParams, Result, excepcion)
                If Not Result Then Throw excepcion
                dbmCore.Transaction_Commit()
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub CreateDelete(ByVal nUsuario As Integer, ByVal Container As Object, ByVal TableDictionary As DataTable)
            CreateDelete(nUsuario, Container, TableDictionary, ConnectionString.Core)
        End Sub
        Public Sub CreateDelete(ByVal nUsuario As Integer, ByVal Container As Object, ByVal TableDictionary As DataTable, ByVal Conexion As String)
            Dim dm As DBCoreDataBaseManager
            dm = New DBCoreDataBaseManager(Conexion)
            dm.Connection_Open(nUsuario)
            dm.Transaction_Begin()

            Dim table As DataTable = TableDictionary
            Dim excepcion As New System.Exception
            Dim nKeys As New List(Of Slyg.Data.Schemas.Parameter)
            Dim Result As Boolean

            Dim view As DataView = table.DefaultView
            view.RowFilter = DBLlavePrimaria & "=1"
            table = view.ToTable

            For Each row1 In table.Rows
                Dim row As DataRow = CType(row1, DataRow)

                nKeys.Add(New Slyg.Data.Schemas.Parameter(row(DBColumna).ToString, CType(DBTypeToVSType(CStr(row(DBTipo))), DbType), CStr(row(DBTipo)), DBTypeToVSType(CStr(row(DBTipo)), GetControlValue(Container, row(DBColumna).ToString)), False, 0, 10, 0, ParameterDirection.Input))
            Next

            dm.DataBase.DBDelete(table.Rows(0)(DBEsquema).ToString, table.Rows(0)(DBTabla).ToString, nKeys, Result, excepcion)

            dm.Transaction_Commit()
            dm.Connection_Close()

            If Not Result Then Throw excepcion
        End Sub

        Public Function CreateSelect(ByVal nUsuario As Integer, ByVal Esquema As String, ByVal Tabla As String) As DataTable
            Dim Table As DataTable
            Try
                If Esquema = "" Or Tabla = "" Then
                    Throw New Exception("No hay una tabla o un esquema para consultar")
                End If

                Dim dm As DBCoreDataBaseManager
                dm = New DBCoreDataBaseManager(ConnectionString.Core)
                dm.Connection_Open(nUsuario)
                Table = dm.DataBase.ExecuteQueryGet("select * from " & Esquema & "." & Tabla)
                dm.Connection_Close()
            Catch ex As Exception
                Throw
            End Try
            Return Table
        End Function
        Public Function CreateSelect(ByVal nUsuario As Integer, ByVal Schema As String, ByVal Table As String, ByVal Keys As ParameterCollection, ByVal ParamArray Parametros As String()) As DataTable
            Try
                Dim dm As DBCoreDataBaseManager
                dm = New DBCoreDataBaseManager(ConnectionString.Core)
                dm.Connection_Open(nUsuario)

                Dim Sentencia(Keys.Count - 1) As String
                Dim Count As Integer = 0

                For Each Parametro As Parameter In Keys
                    Sentencia(Count) = Parametro.ToString & "='" & Parametro.DefaultValue & "'"
                    Count += 1
                Next

                Dim Select_ As String = "Select  " & Join(Parametros, ",") & " From " & Schema & "." & Table & " Where " & Join(Sentencia, " and ")
                Dim Table_ As DataTable = dm.DataBase.ExecuteQueryGet(Select_)

                dm.Connection_Close()
                Return Table_

            Catch ex As Exception
                Throw
            End Try

        End Function

#End Region

#End Region

    End Class
End Namespace