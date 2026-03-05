Imports System.IO
Imports Miharu.Core.Sitio.Boveda
Imports Miharu.Core.Arquitectura
Imports Miharu.Core.Clases
Imports Miharu.Core.Sitio.Administracion
Imports Miharu.Core.Sitio.Reporte

Namespace Controles

    Public Class MenuControl
        Inherits UserWebControlBase

        Public MyPageBase As New FormBase

#Region " EVENTOS "
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            LoadData()
            Config_Page()
        End Sub

        Public Event PageChanged(ByRef nPage As Security.Library.Session.Pagina)

        'Administracion
        Private Sub tvAdministracion_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvAdministracion.SelectedNodeChanged
            Select Case tvAdministracion.SelectedNode.Value
                Case "1.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/Administracion/proyecto.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Proyectos"

                Case "1.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Esquema.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Esquemas"

                Case "1.3"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Documento.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Documentos"

                Case "1.4.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("process.TBL_Folder"), tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Folders (Proceso)"

                Case "1.4.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("custody.TBL_Caja_Tipo"), tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Tipo Cajas"

                Case "1.4.4"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Campo).FullName, "Parametrizacion", "~/sitio/Administracion/Campo.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Campos"

                Case "1.4.5"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Tipologia).FullName, "Parametrizacion", "~/sitio/Administracion/Tipologia.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Tipologías"

                Case "1.4.6"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Campo_Lista.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Campo Lista"

                Case "1.4.7"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Campo_Lista_Item.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Campo Lista Item"

                Case "1.4.8"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/TablaAsociada.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Tabla Asociada"

                Case "1.4.9"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("custody.TBL_Folder_Tipo"), tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Tipo Folder (Custodia)"

                Case "1.4.10"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(CajaProceso).FullName, "Parametrizacion", "~/sitio/Administracion/CajaProceso.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Cajas de proceso"

                Case "1.5.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("imaging.TBL_Servidor_Tipo"), tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Tipo Servidor"

                Case "1.5.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("imaging.TBL_Servidor"), tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Servidor"

                Case "1.5.3"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Servidor_Volumen_Imaging.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración Volumen de Servidor"

                Case "1.5.4"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Folder_Imaging.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración Folders (Imaging)"

                Case "1.5.5"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/File_Imaging.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración Files (Imaging)"

                Case "1.5.6"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("imaging.TBL_Content_Type"), tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración Content Type"

                Case "1.6"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Parametrizacion", "~/sitio/Administracion/campo.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Tipo Campos"

                Case "1.7"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Validacion).FullName, "Parametrizacion", "~/sitio/Administracion/Validacion_.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Validaciones de Documentos"

                Case "1.8"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Roles).FullName, "Parametrizacion", "~/sitio/Administracion/Roles.aspx", tvAdministracion.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administrar roles usuarios"

            End Select
        End Sub

        'Plantillas Estante
        Private Sub tvPlantillasEstante_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvPlantillasEstante.SelectedNodeChanged
            Select Case tvPlantillasEstante.SelectedNode.Value
                Case "2.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/PlantillasEstante/PlantillasEstantes.aspx", tvPlantillasEstante.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Plantillas Estantes"
            End Select
        End Sub

        'Bóveda
        Private Sub tvBoveda_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvBoveda.SelectedNodeChanged
            Select Case tvBoveda.SelectedNode.Value
                Case "3.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/Boveda/Boveda.aspx", tvBoveda.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Bóvedas"
                Case "3.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(ImpresionCodigoBarras).FullName, "Impresión C. Barras", "~/sitio/Boveda/ImpresionCodigoBarras.aspx", tvBoveda.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Impresión de códigos de barras"
            End Select
        End Sub

        'TRD
        Private Sub tvTRD_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvTRD.SelectedNodeChanged
            Select Case tvTRD.SelectedNode.Value
                Case "5.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/trd/trd.aspx", tvTRD.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de TRD"

                Case "5.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("config.TBL_TRD_Serie"), tvTRD.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Series"

                Case "5.3"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/TRD/SubSerie.aspx", tvTRD.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Sub Series"
            End Select
        End Sub

        'Búsqueda
        Private Sub tvBusqueda_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvBusqueda.SelectedNodeChanged
            Select Case tvBusqueda.SelectedNode.Value
                Case "6.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("config.TBL_Campo_Busqueda"), tvBusqueda.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Campo de Búsqueda"

                Case "6.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/Sitio/Busqueda/CampoBusquedaEntidad.aspx", tvBusqueda.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Campo de Búsqueda Entidad"

                Case "6.3"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/Main/Error.aspx", tvBusqueda.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Campos"
            End Select
        End Sub

        'Búsqueda
        Private Sub tvReporte_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvReporte.SelectedNodeChanged
            Select Case tvReporte.SelectedNode.Value
                Case "7.1"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("config.TBL_Conexion"), tvReporte.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de conexiones a servidores"

                Case "7.2"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Miharu.Core.Catalogo).FullName, "Catálogo", "~/sitio/Administracion/Catalogo.aspx?Tabla=" & MyPageBase.encodeBase64("config.TBL_Categoria_Reporte"), tvReporte.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de categorias de reportes"

                Case "7.3"
                    MySesion.Pagina = New Security.Library.Session.Pagina(GetType(Reportes).FullName, "Catálogo", "~/Sitio/Reporte/Reportes.aspx", tvReporte.SelectedNode.Value)
                    RaiseEvent PageChanged(MySesion.Pagina)
                    MySesion.Pagina.Parameter("pagename") = "Administración de Reportes"
            End Select
        End Sub

#End Region

#Region " METODOS "
        Private Sub LoadData()
            Try
                If Not Me.MySesion Is Nothing Then
                    Dim LogoClienteURL As String

                    LogoClienteURL = "~/_images/logos/entidad-" & Me.MySesion.Entidad.id & ".bmp"

                    If File.Exists(Server.MapPath(LogoClienteURL)) Then
                        imgLogoCliente.ImageUrl = LogoClienteURL
                    Else
                        imgLogoCliente.ImageUrl = "~/_images/logos/LogoPyC.jpg"
                    End If
                    load_usuario_Conexion()
                Else
                    imgLogoCliente.ImageUrl = "~/_images/logos/LogoPyC.jpg"
                End If
            Catch ex As Exception
                MyPageBase.ExceptionController.ShowMessage(ex.Message, MsgBoxIcon.IconError, "", "Error")
            End Try
        End Sub

        Private Sub Config_Page()
            ' TODO: Validar permisos
            'ValidarAccesos(tvVentanilla.Nodes)
            'ValidarAccesos(tvAdministracion.Nodes)
            'ValidarAccesos(tvConsultas.Nodes)
            'ValidarAccesos(tvDescargueCierreHojas.Nodes)
            'ValidarAccesos(tvPlanillas.Nodes)
            'ValidarAccesos(tvUtilidades.Nodes)
        End Sub

        Private Sub ValidarAccesos(ByVal nNodos As TreeNodeCollection)
            Dim Nodo As TreeNode
            Dim NodoAux As New TreeNode
            Dim Contador As Integer
            Dim sw As Integer = 0

            Contador = 0
            If nNodos.Count > 0 Then NodoAux = nNodos.Item(0)
            While Contador < nNodos.Count
                Nodo = nNodos.Item(Contador)

                If Not Me.MySesion Is Nothing Then
                    If MySesion.Usuario.PerfilManager.PuedeAcceder(Nodo.Value) Then
                        ValidarAccesos(Nodo.ChildNodes)
                        Contador += 1
                        sw = 1
                    Else
                        nNodos.Remove(Nodo)
                    End If
                Else
                    nNodos.Remove(Nodo)
                End If
            End While
            If sw = 0 And NodoAux.Value <> "" Then
                Nodo_Padre(NodoAux.Value.Substring(0, 1))
            End If
        End Sub

        Private Sub Nodo_Padre(ByVal nNodo As String)
            Select Case nNodo  'seccion
                Case "1"
                    'apRadicacionCorrespondencia.Visible = False
                Case "2"
                    'apPlanillas.Visible = False

                Case "3"
                    'apHojasRuta.Visible = False
                Case "6"
                    'apConsultas.Visible = False
                Case "7"
                    'apUtilidades.Visible = False
                Case "8"
                    'apAdministracion.Visible = False
            End Select
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

        Private Sub load_usuario_Conexion()
            'lblConexion_Entidad.Text = Me.MySesion.Entidad.Nombre
            'lblConexion_Nombres.Text = Me.MySesion.Usuario.Nombres
            'lblConexion_Apellidos.Text = Me.MySesion.Usuario.Apellidos
            'lblConexion_Login.Text = Me.MySesion.Usuario.Login
            'lblConexion_IP.Text = Me.MySesion.ClientIPAddress
            'lblConexion_Version.Text = Program.AssemblyVersion
            'lblGrupoEmpresarial.Text = MySesion.Entidad.Grupo
            'lblCentroCorrespondencia.Text = Me.MySesionCC.NombreCentroCorrespondencia
        End Sub
#End Region

    End Class
End Namespace