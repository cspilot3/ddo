Imports System.IO
Imports Miharu.Security.Library.Session
Imports Miharu.Security._clases

Namespace _controls

    Partial Public Class wucMenu
        Inherits UserWebControlBase

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            LoadData()

            Config_Page()
        End Sub

        ' Administración
        Private Sub tvAdministracion_SelectedNodeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tvAdministracion.SelectedNodeChanged
            If Not tvInformes.SelectedNode Is Nothing Then tvInformes.SelectedNode.Selected = False
            'If Not tvAdministracion.SelectedNode Is Nothing Then tvAdministracion.SelectedNode.Selected = False

            Select Case tvAdministracion.SelectedNode.Value
                Case "1.1" ' Estructura
                    Me.MySesion.Pagina = New Pagina("Estructura", "Estructura", "~/_sitio/blankform.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.1" ' Grupo empresarial
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.grupo_empresarial).FullName, "Grupo empresarial", "~/_sitio/administracion/estructura/grupo_empresarial.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.2" ' Entidades
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.entidades).FullName, "Entidades", "~/_sitio/administracion/estructura/entidades.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.3" ' Dependencias
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.dependencias).FullName, "Dependencias", "~/_sitio/administracion/estructura/dependencias.aspx", tvAdministracion.SelectedNode.Value)
                    ' Case "1.1.3" ' Dependencias
                    '    Me.MySesion.Pagina = New Pagina(GetType(organigrama).FullName, "Organigrama", "~/_sitio/administracion/estructura/organigrama.aspx", tvAdministracion.SelectedNode.Value)

                    '----------Adionales Menu--------
                Case "1.1.4" 'Regionales
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.regionales).FullName, "Regionales", "~/_sitio/administracion/estructura/regionales.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.5" 'Pais
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.pais).FullName, "Pais", "~/_sitio/administracion/estructura/pais.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.6" 'Regiones
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.region).FullName, "Regiones", "~/_sitio/administracion/estructura/region.aspx", tvAdministracion.SelectedNode.Value)
                    '--------------------------------

                Case "1.1.7" ' Ciudades
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.ciudades).FullName, "Ciudades", "~/_sitio/administracion/estructura/Ciudades.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.8" ' Sedes
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.sedes).FullName, "Sedes", "~/_sitio/administracion/estructura/Sedes.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.9"
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.puestotrabajo).FullName, "Puestos de Trabajo", "~/_sitio/administracion/estructura/puestotrabajo.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.10"
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.centrosprocesamiento).FullName, "Centros de Procesamiento", "~/_sitio/administracion/estructura/centrosprocesamiento.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.1.11" 'Calendario
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.estructura.calendario).FullName, "Calendario", "~/_sitio/administracion/estructura/calendario.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2" ' Seguridad
                    Me.MySesion.Pagina = New Pagina("Seguridad", "Seguridad", "~/_sitio/blankform.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.1" ' Modulos
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.modulos).FullName, "Modulos", "~/_sitio/administracion/seguridad/modulos.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.2" ' Perfiles
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.perfiles).FullName, "Perfiles", "~/_sitio/administracion/seguridad/perfiles.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.3" ' Esquemas
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.esquemas).FullName, "Esquemas", "~/_sitio/administracion/seguridad/esquemas.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.4" ' Usuarios
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.usuarios).FullName, "Usuarios", "~/_sitio/administracion/seguridad/usuarios.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.5" ' Parametros
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.parametros).FullName, "Parámetros", "~/_sitio/administracion/seguridad/parametros.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.6" ' IPs Bloqueadas
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.ipsbloqueadas).FullName, "IPs bloqueadas", "~/_sitio/administracion/seguridad/ipsbloqueadas.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.7" ' Roles
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.roles).FullName, "Roles", "~/_sitio/administracion/seguridad/roles.aspx", tvAdministracion.SelectedNode.Value)

                Case "1.2.8" ' LDAP
                    Me.MySesion.Pagina = New Pagina(GetType(_sitio.administracion.seguridad.LDAP).FullName, "LDAP", "~/_sitio/administracion/seguridad/LDAP.aspx", tvAdministracion.SelectedNode.Value)

            End Select

            Config_Page()
        End Sub

        ' Informes
        Private Sub tvInformes_SelectedNodeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tvInformes.SelectedNodeChanged
            'If Not tvInformes.SelectedNode Is Nothing Then tvInformes.SelectedNode.Selected = False
            If Not tvAdministracion.SelectedNode Is Nothing Then tvAdministracion.SelectedNode.Selected = False

            Select Case tvAdministracion.SelectedNode.Value
                Case "3.1" ' Informe 1
                    'MySesion.Pagina = New Pagina("Parametros base", "Parametros base", "~/_sitio/blankform.aspx", tvAdministracion.SelectedNode.Value)

                Case "3.2" ' Informe 2
            End Select
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadData()
            If Not Me.MySesion Is Nothing Then
                Dim LogoClienteURL As String

                LogoClienteURL = "~/_images/logo_cliente/entidad-" & MySesion.Entidad.id & ".bmp"

                If File.Exists(Server.MapPath(LogoClienteURL)) Then
                    imgLogoCliente.ImageUrl = LogoClienteURL
                Else
                    imgLogoCliente.ImageUrl = "~/_images/logo_cliente/logo.bmp"
                End If

                lblConexion_Entidad.Text = MySesion.Entidad.Nombre

                lblConexion_Nombres.Text = MySesion.Usuario.Nombres
                lblConexion_Apellidos.Text = MySesion.Usuario.Apellidos

                lblConexion_Login.Text = MySesion.Usuario.Login

                lblConexion_IP.Text = MySesion.ClientIPAddress

                lblConexion_Version.Text = Program.AssemblyVersion
            Else
                imgLogoCliente.ImageUrl = "~/_images/logo_cliente/logo.bmp"
            End If
        End Sub
        Private Sub Config_Page()
            ' Validar permisos
            ValidarAccesos(tvInformes.Nodes)
            ValidarAccesos(tvAdministracion.Nodes)

            If Not Me.MySesion Is Nothing Then
                If Not Me.MySesion.Pagina Is Nothing Then
                    ' Seleccionar el item actual
                    Select Case MySesion.Pagina.SecurityPath.Substring(0, 1) ' Sección
                        Case "0"
                            AccordionMenu.SelectedIndex = 2

                        Case "1"
                            AccordionMenu.SelectedIndex = 0
                            SeleccionarNodo(tvAdministracion.Nodes, MySesion.Pagina.SecurityPath)

                        Case "2"
                            AccordionMenu.SelectedIndex = 1
                            SeleccionarNodo(tvInformes.Nodes, MySesion.Pagina.SecurityPath)

                    End Select
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

#End Region

    End Class

End Namespace