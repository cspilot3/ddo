Imports System.IO
Imports Miharu
Imports DBCore

Partial Public Class wucMenu
    Inherits Imaging.UserWebControlBase


#Region " Declaraciones"

    Dim _MySessionControl As SesionControl
    Dim reportesTable As DBCore.SchemaConfig.TBL_ReporteDataTable
    Dim categoriasTable As DBCore.SchemaConfig.CTA_Categoria_Reportes_WebDataTable

#End Region

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Load_Data()

        'Load_Info_Categorias()

        'Cargar_Categorias()

        CargarMenuReportes()

        'Load_Info_Reportes()

        LoadInfoReportes()

        Cargar_Reportes()

        Config_Page()
    End Sub


    ' Consulta
    Private Sub tvConsulta_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvConsulta.SelectedNodeChanged
        'If Not tvConsulta.SelectedNode Is Nothing Then tvConsulta.SelectedNode.Selected = False
        If Not tvInformes.SelectedNode Is Nothing Then tvInformes.SelectedNode.Selected = False
        If Not tvAdministracion.SelectedNode Is Nothing Then tvAdministracion.SelectedNode.Selected = False

        Select Case tvConsulta.SelectedNode.Value
            Case "1" ' Consulta
                Me.MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Imaging.consulta).FullName, "Consulta", "~/_sitio/Consulta/consulta.aspx", tvConsulta.SelectedNode.Value)

        End Select

        Config_Page()
    End Sub

    ' Informes
    Private Sub tvInformes_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvInformes.SelectedNodeChanged
        If Not tvConsulta.SelectedNode Is Nothing Then tvConsulta.SelectedNode.Selected = False
        'If Not tvInformes.SelectedNode Is Nothing Then tvInformes.SelectedNode.Selected = False
        If Not tvAdministracion.SelectedNode Is Nothing Then tvAdministracion.SelectedNode.Selected = False

        Select Case Obtener_Nivel_Arbol(tvInformes.SelectedNode.Value)
            Case 2
                'Evaluar_Categoria_Seleccionada()

                'Load_Info_Reportes()
                Me.MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Imaging.reporte2).FullName, "Reporte", "~/_sitio/blankform.aspx", tvInformes.SelectedNode.Value)
            Case 3
                Me.MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Imaging.reporte2).FullName, "Reporte", "~/_sitio/reporte/reporte2.aspx", tvInformes.SelectedNode.Value)
        End Select

        Config_Page()
    End Sub

    ' Administración
    Private Sub tvAdministracion_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvAdministracion.SelectedNodeChanged
        If Not tvConsulta.SelectedNode Is Nothing Then tvConsulta.SelectedNode.Selected = False
        If Not tvInformes.SelectedNode Is Nothing Then tvInformes.SelectedNode.Selected = False
        'If Not tvAdministracion.SelectedNode Is Nothing Then tvAdministracion.SelectedNode.Selected = False

        Select Case tvAdministracion.SelectedNode.Value
            Case "3.1" ' Base

        End Select

        Config_Page()
    End Sub

#End Region

#Region " Metodos "

    Private Sub Load_Data()
        If Not Me.MySesion Is Nothing Then
            Dim LogoClienteURL As String

            LogoClienteURL = "~/_images/logo_cliente/entidad-" & Me.MySesion.Entidad.id & ".bmp"

            If File.Exists(Server.MapPath(LogoClienteURL)) Then
                imgLogoCliente.ImageUrl = LogoClienteURL
            Else
                imgLogoCliente.ImageUrl = "~/_images/logo_cliente/LogoEpago.bmp"
            End If

            lblConexion_Entidad.Text = Me.MySesion.Entidad.Nombre
            lblConexion_Nombres.Text = Me.MySesion.Usuario.Nombres
            lblConexion_Apellidos.Text = Me.MySesion.Usuario.Apellidos
            lblConexion_Login.Text = Me.MySesion.Usuario.Login
            lblConexion_IP.Text = Me.MySesion.ClientIPAddress
            lblConexion_Version.Text = Program.AssemblyVersion
            _MySessionControl = New SesionControl(CType(Session("Sesion"), Security.Library.Session.Sesion))

        Else
            imgLogoCliente.ImageUrl = "~/_images/logo_cliente/LogoEpago.bmp"
        End If
    End Sub

    Private Sub Evaluar_Categoria_Seleccionada()
        If Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") Is Nothing Then
            Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") = tvInformes.SelectedNode.Value
        Else
            If Me.MySesion.Pagina.Parameter("Categoria_Seleccionada").ToString <> tvInformes.SelectedNode.Value Then
                Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") = tvInformes.SelectedNode.Value
                Me.MySesion.Pagina.Parameter("Reportes") = Nothing
            End If
        End If

    End Sub

    Private Sub Config_Page()
        ' Validar permisos
        ValidarAccesos(tvConsulta.Nodes)
        ValidarAccesos(tvInformes.Nodes)
        ValidarAccesos(tvAdministracion.Nodes)

        If Not Me.MySesion Is Nothing Then
            If Not Me.MySesion.Pagina Is Nothing Then
                'Seleccionar el item actual
                'Select Case MySesion.Pagina.SecurityPath.Substring(0, 1) ' Módulo
                '    Case "0"
                '        AccordionMenu.SelectedIndex = 4

                '    Case "1"
                '        AccordionMenu.SelectedIndex = 0
                '        SeleccionarNodo(tvConsulta.Nodes, Me.MySesion.Pagina.SecurityPath)

                '    Case "2"
                '        AccordionMenu.SelectedIndex = 1
                '        SeleccionarNodo(tvInformes.Nodes, Me.MySesion.Pagina.SecurityPath)

                '    Case "3"
                '        AccordionMenu.SelectedIndex = 2
                '        SeleccionarNodo(tvAdministracion.Nodes, Me.MySesion.Pagina.SecurityPath)

                'End Select
            End If
        End If
    End Sub

    Private Sub ValidarAccesos(ByVal nNodos As TreeNodeCollection)
        Dim Nodo As TreeNode
        Dim Contador As Integer

        Contador = 0

        While Contador < nNodos.Count
            Nodo = nNodos.Item(Contador)

            If Not Me.MySesion Is Nothing Then
                If MySesion.Usuario.PerfilManager.PuedeAcceder(Nodo.Value) Then
                    ValidarAccesos(Nodo.ChildNodes)
                    Contador += 1
                Else
                    nNodos.Remove(Nodo)
                End If
            Else
                nNodos.Remove(Nodo)
            End If
        End While
    End Sub

    Private Sub SeleccionarNodo(ByVal nNodos As TreeNodeCollection, ByVal nPath As String)
        For Each Nodo As TreeNode In nNodos
            If Nodo.Value = nPath Then
                Nodo.Expand()
                Nodo.Select()
                Return
            ElseIf nPath.StartsWith(Nodo.Value) Then
                Nodo.Expand()
                SeleccionarNodo(Nodo.ChildNodes, nPath)
            End If
        Next
    End Sub


    'Private Sub Load_Info_Categorias()

    '    If Me.MySesion.Pagina.Parameter("Categorias") Is Nothing Then

    '        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
    '        dbmCore = New DBCore.DBCoreDataBaseManager(_MySessionControl.ConnectionString.Core)
    '        dbmCore.Connection_Open(MySesion.Usuario.id)
    '        categoriasTable = dbmCore.SchemaConfig.CTA_Categoria_Reportes_Web.DBFindByVisible_Webfk_Entidad(True, MySesion.Entidad.id, 0, New DBCore.SchemaConfig.CTA_Categoria_Reportes_WebEnumList(DBCore.SchemaConfig.CTA_Categoria_Reportes_WebEnum.Nombre_Categoria_Reporte, True))
    '        Me.MySesion.Pagina.Parameter("Categorias") = categoriasTable
    '    Else
    '        categoriasTable = CType(Me.MySesion.Pagina.Parameter("Categorias"), DBCore.SchemaConfig.CTA_Categoria_Reportes_WebDataTable)
    '    End If

    'End Sub

    'Private Sub Cargar_Categorias()

    '    For Each categoria In categoriasTable

    '        Dim nodo As TreeNode
    '        If Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") Is Nothing Then
    '            nodo = Get_Nodo(categoria.Nombre_Categoria_Reporte, "2." & categoria.Id_Categoria_Reporte.ToString, "~/_images/menu/category0.png")
    '        Else
    '            If Me.MySesion.Pagina.Parameter("Categoria_Seleccionada").ToString = "2." & categoria.Id_Categoria_Reporte.ToString Then
    '                nodo = Get_Nodo(categoria.Nombre_Categoria_Reporte, "2." & categoria.Id_Categoria_Reporte.ToString, "~/_images/menu/category.png")
    '            Else
    '                nodo = Get_Nodo(categoria.Nombre_Categoria_Reporte, "2." & categoria.Id_Categoria_Reporte.ToString, "~/_images/menu/category0.png")
    '            End If
    '        End If

    '        tvInformes.Nodes.Add(nodo)

    '    Next

    '    If tvInformes.Nodes.Count = 1 Then
    '        tvInformes.FindNode(tvInformes.Nodes(0).Value).Select()
    '        Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") = tvInformes.SelectedNode.Value
    '    Else
    '        If Not Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") Is Nothing And tvInformes.SelectedNode Is Nothing Then
    '            tvInformes.FindNode(Me.MySesion.Pagina.Parameter("Categoria_Seleccionada").ToString).Select()
    '        End If
    '    End If

    '    tvInformes.ExpandAll()

    'End Sub

    Private Sub CargarMenuReportes()


        If Not Me.MySesion.Pagina.Parameter("MenuReportes") Is Nothing Then
            Dim ListaNodostemporal As List(Of TreeNode) = CType(Me.MySesion.Pagina.Parameter("MenuReportes"), List(Of TreeNode))


            For Each nodo As TreeNode In ListaNodostemporal
                tvInformes.Nodes.Add(nodo)
            Next
        Else
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            dbmCore = New DBCore.DBCoreDataBaseManager(_MySessionControl.ConnectionString.Core)
            dbmCore.Connection_Open(MySesion.Usuario.id)


            Dim CategoriasDataTable = dbmCore.SchemaConfig.CTA_Categoria_Reportes_Web.DBFindByVisible_Webfk_Entidad(True, MySesion.Entidad.id)
            Dim ListaNodos As New List(Of TreeNode)


            For Each categoria In CategoriasDataTable
                Dim NodoCategoria = New TreeNode(categoria.Nombre_Categoria_Reporte, "2." + CStr(categoria.Id_Categoria_Reporte), "~/_images/menu/Category.png")


                tvInformes.Nodes.Add(NodoCategoria)
                ListaNodos.Add(NodoCategoria)


                Dim ReportesCategoriaDataTable = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_ReporteVisible_Web(categoria.Id_Categoria_Reporte, True)


                For Each reporte In ReportesCategoriaDataTable
                    Dim NodoReporte = New TreeNode(reporte.Nombre_Reporte, "2." + CStr(categoria.Id_Categoria_Reporte) + "." + CStr(reporte.Id_Reporte), "~/_images/menu/report.png")
                    NodoCategoria.ChildNodes.Add(NodoReporte)
                Next
            Next
            Me.MySesion.Pagina.Parameter("MenuReportes") = ListaNodos
        End If

        tvInformes.ExpandAll()

    End Sub


    'Private Sub Load_Info_Reportes()

    '    If Not Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") Is Nothing Then

    '        If Me.MySesion.Pagina.Parameter("Reportes") Is Nothing Then

    '            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
    '            dbmCore = New DBCore.DBCoreDataBaseManager(_MySessionControl.ConnectionString.Core)
    '            dbmCore.Connection_Open(MySesion.Usuario.id)
    '            Dim id_Categoria As String = Me.MySesion.Pagina.Parameter("Categoria_Seleccionada").ToString.Replace("2.", "")
    '            reportesTable = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_ReporteVisible_Web(CShort(id_Categoria), True, 0, New DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, True))
    '            Me.MySesion.Pagina.Parameter("Reportes") = reportesTable

    '        Else
    '            reportesTable = CType(Me.MySesion.Pagina.Parameter("Reportes"), DBCore.SchemaConfig.TBL_ReporteDataTable)
    '        End If

    '    End If

    'End Sub

    Private Sub LoadInfoReportes()

        If Not Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") Is Nothing Then

            If Me.MySesion.Pagina.Parameter("Reportes") Is Nothing Then

                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                dbmCore = New DBCore.DBCoreDataBaseManager(_MySessionControl.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                reportesTable = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_ReporteVisible_Web(Nothing, True, 0, New DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, True))
                Me.MySesion.Pagina.Parameter("Reportes") = reportesTable

            Else
                reportesTable = CType(Me.MySesion.Pagina.Parameter("Reportes"), DBCore.SchemaConfig.TBL_ReporteDataTable)
            End If

        End If

    End Sub

    Private Sub Cargar_Reportes()

        If Not Me.MySesion.Pagina.Parameter("Categoria_Seleccionada") Is Nothing AndAlso Not reportesTable Is Nothing Then
            tvInformes.FindNode(Me.MySesion.Pagina.Parameter("Categoria_Seleccionada").ToString).Select()
            If tvInformes.SelectedNode.ChildNodes.Count = 0 Then
                For Each reporte In reportesTable
                    Dim nodo As TreeNode = Get_Nodo(reporte.Nombre_Reporte, Me.MySesion.Pagina.Parameter("Categoria_Seleccionada").ToString & "." & reporte.Id_Reporte, "~/_images/menu/report.png")
                    tvInformes.SelectedNode.ChildNodes.Add(nodo)
                Next
            End If
        End If
        tvInformes.ExpandAll()
    End Sub



#End Region

#Region " Funciones "

    Private Function Get_Nodo(Nombre_Reporte As String, value As String, urlImage As String) As TreeNode
        Dim nodo As TreeNode = New TreeNode(Nombre_Reporte, value, urlImage)
        nodo.ToolTip = Nombre_Reporte
        Return nodo
    End Function

    Private Function Obtener_Nivel_Arbol(texto As String) As Integer
        If texto Is Nothing Then
            Return 0
        End If
        Dim secciones As String() = texto.Split(CChar("."))
        Return secciones.Length
    End Function

#End Region



End Class


Public Class SesionControl

#Region " Declaraciones "
    Private _sesion As Security.Library.Session.Sesion
#End Region

#Region " Propiedades "

    Public ReadOnly Property MySesion() As Miharu.Security.Library.Session.Sesion
        Get
            Return _sesion
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

#End Region

#Region " Procedimientos "

    Sub New(sesion As Security.Library.Session.Sesion)
        _sesion = sesion
    End Sub


#End Region

#Region " Metodos "

#End Region

End Class

Public Enum NivelArbolReportes

    Categoria = 2
    Reporte = 3

End Enum