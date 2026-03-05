Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Net
Imports Miharu.Desktop.Forms
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ
Imports Miharu.Security.Library.WebService
Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Deployment.Application
Imports System.Configuration
Imports Miharu.Security.Library.SecurityServiceReference
Imports Miharu.Security.Library

Public Class Program

#Region " Variables Globales "

    'Private Const ID_MODULO As String = "7" 'Miharu.Desktop

    Friend Shared Sesion As New Security.Library.Session.Sesion()
    Friend Shared DesktopGlobal As New DesktopGlobal()

    Friend Shared MainForm As FormMain

#End Region

#Region " Propiedades "

    Friend Shared ReadOnly Property AssemblyTitle() As String
        Get
            ' Get all Title attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute), False)

            ' If there is at least one Title attribute
            If (attributes.Length > 0) Then
                ' Select the first one
                Dim titleAttribute As AssemblyTitleAttribute = CType(attributes(0), AssemblyTitleAttribute)

                ' If it is not an empty string, return it
                If (titleAttribute.Title <> "") Then Return titleAttribute.Title

            End If

            ' If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
            Return Path.GetFileNameWithoutExtension([Assembly].GetExecutingAssembly().CodeBase)
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyVersion() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Version.ToString()
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyVersionPublicacion() As String
        Get
            Try
                Return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
            Catch ex As Exception
                Return "N.A."
            End Try
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyDescription() As String
        Get
            ' Get all Description attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)

            ' If there aren't any Description attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Description attribute, return its value
            Return CType(attributes(0), AssemblyDescriptionAttribute).Description
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyProduct() As String
        Get
            ' Get all Product attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyProductAttribute), False)

            ' If there aren't any Product attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Product attribute, return its value
            Return CType(attributes(0), AssemblyProductAttribute).Product
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyCopyright() As String
        Get
            ' Get all Copyright attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)

            ' If there aren't any Copyright attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Copyright attribute, return its value
            Return CType(attributes(0), AssemblyCopyrightAttribute).Copyright
        End Get
    End Property

    Friend Shared ReadOnly Property AssemblyCompany() As String
        Get
            ' Get all Company attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)

            ' If there aren't any Company attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Company attribute, return its value
            Return CType(attributes(0), AssemblyCompanyAttribute).Company
        End Get
    End Property

    Friend Shared ReadOnly Property CrLf() As String
        Get
            Return Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString()
        End Get
    End Property

    Friend Shared ReadOnly Property AppPath() As String
        Get
            Return Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

    Friend Shared ReadOnly Property SecurityWebServiceUrl As String
        Get
            Return ConfigurationManager.AppSettings("WebService.SecurityService")
        End Get
    End Property

    Public Shared ReadOnly Property IdentifierDateFormat() As String
        Get
            Dim rootWebConfig As String = ConfigurationManager.AppSettings("IdentifierDateFormat")
            If rootWebConfig IsNot Nothing AndAlso rootWebConfig.Length > 0 Then
                Return rootWebConfig
            Else
                Throw New Exception("Por favor asigne la cadena <add key=""IdentifierDateFormat"" value=""?""/> al archivo Web.config.")
            End If
        End Get
    End Property

    Friend Shared ReadOnly Property EsExterno As String
        Get
            Return ConfigurationManager.AppSettings("EsExterno")
        End Get
    End Property

    Friend Shared ReadOnly Property Remoting As String
        Get
            Return ConfigurationManager.AppSettings("Remoting")
        End Get
    End Property

    Friend Shared ReadOnly Property SecurityDMZWebServiceUrl As String
        Get
            Return ConfigurationManager.AppSettings("WebService.SecurityDMZService")
        End Get
    End Property

#End Region

#Region " Metodos "

    Shared Sub Main()

        If CBool((EsExterno)) Then
            MainExterno()
        Else
            MainInterno()
        End If
    End Sub

    Shared Sub MainExterno()

        System.Net.ServicePointManager.SecurityProtocol = CType(3072, System.Net.SecurityProtocolType)

        Dim webDMZService As SecurityDMZWebService = Nothing
        Dim respuesta As DialogResult
        Dim splashForm As FormSplash
        Dim unidadesMapeo As New List(Of Slyg.Tools.Mapping.Mapeo)

        Try
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(True)

            DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = IdentifierDateFormat

            DesktopGlobal.SecurityServiceUrl = SecurityDMZWebServiceUrl
            DesktopGlobal.ClientIpAddress = GetClientIpAddress()

            ' Mostrar ventana de presentación
            'splashForm = New FormSplash
            'splashForm.Show()
            'Application.DoEvents()

            Sesion.IsExternal = CBool(EsExterno)

            webDMZService = New SecurityDMZWebService(DesktopGlobal.SecurityServiceUrl, DesktopGlobal.ClientIpAddress)

#If Not Debug Then
            ' Validar que la versión corresponda
            Dim VersionApp As String

            VersionApp = webDMZService.getAssemblyVersion(AssemblyName)

            If Not VersionApp = AssemblyVersion Then
                MessageBox.Show("La versión del aplicativo no corresponde a la registrada en la base de datos, por" & vbCrLf & _
                                "favor comuniquese con el el administrador del sistema para obtener la última versión" & vbCrLf & vbCrLf & _
                                "Versión registrada: [" & VersionApp & "]" & vbCrLf & _
                                "Versión ejecutable: [" & AssemblyVersion & "]", _
                                AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End
            End If
#End If

            If webDMZService.IsIPBloqueada() Then
                MessageBox.Show("La dirección IP local [" & webDMZService.ClientIPAddress & "] se encuentra bloqueada, por favor comuníquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Return
            End If


            ' Formulario principal
            MainForm = New FormMain

            ' Validar usuario
            Dim loginForm As New FormLogin
            Dim continuar As Boolean
            Dim contador As Integer = 0

            'splashForm.Close()
            'Application.DoEvents()

            Do
                loginForm.SelectText()
                respuesta = loginForm.ShowDialog()

                contador += 1

                If (respuesta = DialogResult.OK) Then

                    If IniciarSesionExterno(webDMZService, loginForm.Login, loginForm.Password) Then
                        If (AlReadyloggedDMZ(loginForm.Login)) Then
                            If (Sesion.Usuario.isRoot Or Sesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                                ' Seguir con el cargue del aplicativo
                                loginForm.Dispose()
                                continuar = False
                            Else
                                MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                ' Preguntar nuevamente el usuario
                                continuar = True

                            End If
                        Else
                            ' Preguntar nuevamente el usuario
                            continuar = True
                        End If

                    ElseIf contador = 4 Then
                        ' Finalizar la aplicación
                        End

                    Else
                        ' Preguntar nuevamente el usuario
                        continuar = True
                    End If
                Else
                    Return
                End If
            Loop While continuar



            Dim unidadMapeoDataTable As DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable
            Dim sedeRow As DBSecurity.SchemaConfig.TBL_SedeRow
            Dim puestoTrabajoDataTable As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable

            Try
                ' Cargar la configuración del puesto de trabajo
                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_core].Config.TBL_Puesto_Trabajo", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "PC_Name", .value = Environment.MachineName.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                puestoTrabajoDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable)

                If (puestoTrabajoDataTable.Count = 0) Then
                    MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no se encuentra registrado como un puesto de trabajo de la entidad [" & Sesion.Entidad.Nombre & "], por favor comuniquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                DesktopGlobal.PuestoTrabajoRow = puestoTrabajoDataTable(0).ToTBL_Puesto_TrabajoSimpleType

                Dim sedeDataTable As DBSecurity.SchemaConfig.TBL_SedeDataTable

                queryResponse = ClientUtil.resolver("[DB_Miharu.Security_core].Config.TBL_Sede", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = puestoTrabajoDataTable(0).fk_Entidad.ToString()},
                New QueryParameter With {.name = "id_Sede", .value = puestoTrabajoDataTable(0).fk_Sede.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                sedeDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.TBL_SedeDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.TBL_SedeDataTable)

                sedeRow = sedeDataTable(0)

                ' Cargar la configuración del centro de procesamiento
                Dim centroProcesamientoDataTable As DBCore.SchemaSecurity.CTA_Centro_ProcesamientoDataTable

                queryResponse = ClientUtil.resolver("[DB_Miharu.Core].Security.CTA_Centro_Procesamiento", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = puestoTrabajoDataTable(0).fk_Entidad.ToString()},
                New QueryParameter With {.name = "fk_Sede", .value = puestoTrabajoDataTable(0).fk_Sede.ToString()},
                New QueryParameter With {.name = "id_Centro_Procesamiento", .value = puestoTrabajoDataTable(0).fk_Centro_Procesamiento.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                centroProcesamientoDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaSecurity.CTA_Centro_ProcesamientoDataTable(), queryResponse.dataTable), DBCore.SchemaSecurity.CTA_Centro_ProcesamientoDataTable)

                DesktopGlobal.CentroProcesamientoRow = centroProcesamientoDataTable(0).ToCTA_Centro_ProcesamientoSimpleType()

                ' Cargar la configuración de Mapeo
                queryResponse = ClientUtil.resolver("[DB_Miharu.Security_core].Config.TBL_Unidad_Mapeo", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = DesktopGlobal.CentroProcesamientoRow.fk_Entidad.ToString()},
                New QueryParameter With {.name = "fk_Sede", .value = DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                New QueryParameter With {.name = "fk_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                unidadMapeoDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable)

                'Carga la configuración de la Bóveda
                Dim dtBovedaCProc As DBCore.SchemaCustody.TBL_Boveda_Centro_ProcesamientoDataTable

                queryResponse = ClientUtil.resolver("[DB_Miharu.Core].Custody.TBL_Boveda_Centro_Procesamiento", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.fk_Entidad.ToString()},
                New QueryParameter With {.name = "fk_Sede_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                New QueryParameter With {.name = "fk_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                dtBovedaCProc = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaCustody.TBL_Boveda_Centro_ProcesamientoDataTable(), queryResponse.dataTable), DBCore.SchemaCustody.TBL_Boveda_Centro_ProcesamientoDataTable)

                If dtBovedaCProc.Count > 0 Then
                    Dim dtBoveda As DBCore.SchemaCustody.TBL_BovedaDataTable

                    queryResponse = ClientUtil.resolver("[DB_Miharu.Core].Custody.TBL_Boveda", New List(Of QueryParameter) From {
                    New QueryParameter With {.name = "fk_Entidad", .value = dtBovedaCProc(0).fk_Entidad_Boveda.ToString()},
                    New QueryParameter With {.name = "fk_Sede", .value = dtBovedaCProc(0).fk_Sede_Boveda.ToString()},
                    New QueryParameter With {.name = "id_Boveda", .value = dtBovedaCProc(0).fk_Boveda.ToString()}
                    }, QueryRequestType.Table, QueryResponseType.Table)

                    dtBoveda = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaCustody.TBL_BovedaDataTable(), queryResponse.dataTable), DBCore.SchemaCustody.TBL_BovedaDataTable)

                    If dtBoveda.Count > 0 Then
                        DesktopGlobal.BovedaRow = dtBoveda(0).ToTBL_BovedaSimpleType()
                    Else
                        MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no esta relacionado con una bóveda.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                Else
                    MessageBox.Show("El Centro de procesamiento no esta asignado a una boveda.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                'Carga la configuración de Servidor de Imágenes.
                Dim asignacionDataTable As DBCore.SchemaImaging.TBL_Servidor_Centro_ProcesamientoDataTable
                Dim servidorDataTable As DBCore.SchemaImaging.TBL_ServidorDataTable

                queryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.TBL_Servidor_Centro_Procesamiento", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.fk_Entidad.ToString()},
                New QueryParameter With {.name = "fk_Sede_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                New QueryParameter With {.name = "fk_Centro_Procesamiento", .value = DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                asignacionDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.TBL_Servidor_Centro_ProcesamientoDataTable(), queryResponse.dataTable), DBCore.SchemaImaging.TBL_Servidor_Centro_ProcesamientoDataTable)

                If (asignacionDataTable.Count > 0) Then
                    queryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.TBL_Servidor", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = asignacionDataTable(0).fk_Entidad_Servidor.ToString()},
                New QueryParameter With {.name = "id_Servidor", .value = asignacionDataTable(0).fk_Servidor.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

                    servidorDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.TBL_ServidorDataTable(), queryResponse.dataTable), DBCore.SchemaImaging.TBL_ServidorDataTable)

                    DesktopGlobal.ServidorImagenRow = servidorDataTable(0).ToTBL_ServidorSimpleType()
                Else
                    MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no esta relacionado con un Servidor de Imágenes.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

            Catch ex As Exception
                DMB.DesktopMessageShow("Inicio", ex)
                Return
            End Try

            'Carga formato de fecha
            DesktopGlobal.IdentifierDateFormat = IdentifierDateFormat

            Dim errorMapeo As Slyg.Tools.Mapping.Mapeo.EnumNET_ERROR

            ' Mapear unidades de red
            For Each rowUnidadMapeo As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow In unidadMapeoDataTable.Rows
                If rowUnidadMapeo.Activa_Unidad_Mapeo Then
                    Dim unidadMapeo As Slyg.Tools.Mapping.Mapeo

                    If rowUnidadMapeo.Usar_Usuario_Contexto Then
                        unidadMapeo = New Slyg.Tools.Mapping.Mapeo(rowUnidadMapeo.Unidad_Mapeo, rowUnidadMapeo.Carpeta_Unidad_Mapeo)
                    Else
                        unidadMapeo = New Slyg.Tools.Mapping.Mapeo(rowUnidadMapeo.Unidad_Mapeo, rowUnidadMapeo.Carpeta_Unidad_Mapeo, rowUnidadMapeo.User_Unidad_Mapeo, rowUnidadMapeo.Password_Unidad_Mapeo)
                    End If

                    unidadesMapeo.Add(unidadMapeo)
                    errorMapeo = unidadMapeo.Conectar()

                    If errorMapeo <> Slyg.Tools.Mapping.Mapeo.EnumNET_ERROR.NO_ERROR Then
                        MessageBox.Show("No se pudo realizar la conexión a [" & rowUnidadMapeo.Nombre_Unidad_Mapeo & "]: " & unidadMapeo.MensajeError(errorMapeo), AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            Next

            ' Mostrar usuario
            MainForm.PCNameToolStripStatusLabel.Text = Environment.MachineName
            MainForm.IPToolStripStatusLabel.Text = GetClientIpAddress()

            ' Mostrar Versión
            MainForm.VersionToolStripStatusLabel.Text = AssemblyVersion
            MainForm.PublicacionToolStripStatusLabel.Text = AssemblyVersionPublicacion

            ' Centro de procesamiento
            MainForm.EntidadToolStripStatusLabel.Text = Sesion.Entidad.Nombre
            MainForm.SedeToolStripStatusLabel.Text = sedeRow.Nombre_Sede
            MainForm.CentroToolStripStatusLabel.Text = DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
            MainForm.EntidadToolStripStatusLabel.Text = Sesion.Entidad.Nombre

            ' Usuario
            MainForm.UsuarioToolStripStatusLabel.Text = Sesion.Usuario.Apellidos.TrimEnd(" "c) & " " & Sesion.Usuario.Nombres.TrimEnd(" "c)

            CopiarPlugings()

            ' Mostrar formulario principal
            MainForm.ShowDialog()

            'TO DO REVISAR
            'Dim usuari As New DBSecurity.SchemaSecurity.TBL_UsuarioType
            'usuari.Logeado = False
            'dbmSecurity.Connection_Open(usuari.id_Usuario)
            'dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(usuari, Sesion.Usuario.id)
            'dbmSecurity.Connection_Close()

        Catch ex As Exception
            DMB.DesktopMessageShow("Inicio", ex)
            End
        Finally
            ' Desconectar unidades de red
            For Each UnidadMapeo In unidadesMapeo
                UnidadMapeo.Desconectar()
            Next
        End Try
    End Sub

    Shared Sub MainInterno()
        Dim webService As SecurityWebService = Nothing
        Dim respuesta As DialogResult
        Dim splashForm As FormSplash
        Dim unidadesMapeo As New List(Of Slyg.Tools.Mapping.Mapeo)

        Try
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(True)

            DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = IdentifierDateFormat

            DesktopGlobal.SecurityServiceUrl = SecurityWebServiceUrl
            DesktopGlobal.ClientIpAddress = GetClientIpAddress()

            ' Mostrar ventana de presentación
            splashForm = New FormSplash
            splashForm.Show()
            Application.DoEvents()

            webService = New SecurityWebService(DesktopGlobal.SecurityServiceUrl, DesktopGlobal.ClientIpAddress)

#If Not Debug Then
            ' Validar que la versión corresponda
            Dim VersionApp As String

            VersionApp = webService.getAssemblyVersion(AssemblyName)

            If Not VersionApp = AssemblyVersion Then
                MessageBox.Show("La versión del aplicativo no corresponde a la registrada en la base de datos, por" & vbCrLf & _
                                "favor comuniquese con el el administrador del sistema para obtener la última versión" & vbCrLf & vbCrLf & _
                                "Versión registrada: [" & VersionApp & "]" & vbCrLf & _
                                "Versión ejecutable: [" & AssemblyVersion & "]", _
                                AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End
            End If
#End If


            If webService.IsIPBloqueada() Then
                MessageBox.Show("La dirección IP local [" & webService.ClientIPAddress & "] se encuentra bloqueada, por favor comuníquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Return
            End If


            ' Formulario principal
            MainForm = New FormMain

            ' Validar usuario
            Dim loginForm As New FormLogin
            Dim continuar As Boolean
            Dim contador As Integer = 0

            splashForm.Close()
            Application.DoEvents()

            Do
                loginForm.SelectText()
                respuesta = loginForm.ShowDialog()

                contador += 1

                If (respuesta = DialogResult.OK) Then

                    If IniciarSesion(webService, loginForm.Login, loginForm.Password) Then
                        If (AlReadylogged(loginForm.Login)) Then
                            If (Sesion.Usuario.isRoot Or Sesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                                ' Seguir con el cargue del aplicativo
                                loginForm.Dispose()
                                continuar = False
                            Else
                                MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                ' Preguntar nuevamente el usuario
                                continuar = True

                            End If
                        Else
                            ' Preguntar nuevamente el usuario
                            continuar = True
                        End If

                    ElseIf contador = 4 Then
                        ' Finalizar la aplicación
                        End

                    Else
                        ' Preguntar nuevamente el usuario
                        continuar = True
                    End If
                Else
                    Return
                End If
            Loop While continuar


            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim unidadMapeoDataTable As DBSecurity.SchemaConfig.TBL_Unidad_MapeoDataTable
            Dim sedeRow As DBSecurity.SchemaConfig.TBL_SedeRow
            Dim puestoTrabajoDataTable As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoDataTable

            Try
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(DesktopGlobal.ConnectionStrings.Security)
                dbmCore = New DBCore.DBCoreDataBaseManager(DesktopGlobal.ConnectionStrings.Core)

                dbmSecurity.Connection_Open(Sesion.Usuario.id)
                dbmCore.Connection_Open(Sesion.Usuario.id)

#If Not Debug Then
                'Valida la versión de publicación ClickOnce
                Dim VersionDataTable = dbmSecurity.SchemaConfig.TBL_Version.DBFindByVersionActivafk_Modulo(AssemblyVersionPublicacion, True, DesktopConfig.Modulo.Archiving)
                If AssemblyVersionPublicacion <> "N.A." AndAlso VersionDataTable.Count = 0 Then
                    MessageBox.Show("La versión actual de la aplicación [" & AssemblyVersionPublicacion & "], no coincide con la de publicación, por favor contacte al administrador.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
#End If


                ' Cargar la configuración del puesto de trabajo
                puestoTrabajoDataTable = dbmSecurity.SchemaConfig.TBL_Puesto_Trabajo.DBFindByPC_Name(Environment.MachineName)
                If (puestoTrabajoDataTable.Count = 0) Then
                    MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no se encuentra registrado como un puesto de trabajo de la entidad [" & Sesion.Entidad.Nombre & "], por favor comuniquese con el administrador del sistema", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                DesktopGlobal.PuestoTrabajoRow = puestoTrabajoDataTable(0).ToTBL_Puesto_TrabajoSimpleType

                Dim sedeDataTable = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(puestoTrabajoDataTable(0).fk_Entidad, puestoTrabajoDataTable(0).fk_Sede)
                sedeRow = sedeDataTable(0)

                ' Cargar la configuración del centro de procesamiento
                Dim centroProcesamientoDataTable = dbmCore.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(puestoTrabajoDataTable(0).fk_Entidad, puestoTrabajoDataTable(0).fk_Sede, puestoTrabajoDataTable(0).fk_Centro_Procesamiento)
                DesktopGlobal.CentroProcesamientoRow = centroProcesamientoDataTable(0).ToCTA_Centro_ProcesamientoSimpleType()

                ' Cargar la configuración de Mapeo
                unidadMapeoDataTable = dbmSecurity.SchemaConfig.TBL_Unidad_Mapeo.DBGet(DesktopGlobal.CentroProcesamientoRow.fk_Entidad, DesktopGlobal.CentroProcesamientoRow.fk_Sede, DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Nothing)

                'Carga la configuración de la Bóveda
                Dim dtBovedaCProc = dbmCore.SchemaCustody.TBL_Boveda_Centro_Procesamiento.DBFindByfk_Entidad_Centro_Procesamientofk_Sede_Centro_Procesamientofk_Centro_Procesamiento(DesktopGlobal.CentroProcesamientoRow.fk_Entidad, DesktopGlobal.CentroProcesamientoRow.fk_Sede, DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

                If dtBovedaCProc.Count > 0 Then
                    Dim dtBoveda = dbmCore.SchemaCustody.TBL_Boveda.DBFindByfk_Entidadfk_Sedeid_Boveda(dtBovedaCProc(0).fk_Entidad_Boveda, dtBovedaCProc(0).fk_Sede_Boveda, dtBovedaCProc(0).fk_Boveda)
                    If dtBoveda.Count > 0 Then
                        DesktopGlobal.BovedaRow = dtBoveda(0).ToTBL_BovedaSimpleType()
                    Else
                        MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no esta relacionado con una bóveda.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                Else
                    MessageBox.Show("El Centro de procesamiento no esta asignado a una boveda.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                'Carga la configuración de Servidor de Imágenes.

                Dim asignacionDataTable = dbmCore.SchemaImaging.TBL_Servidor_Centro_Procesamiento.DBGet(DesktopGlobal.CentroProcesamientoRow.fk_Entidad, DesktopGlobal.CentroProcesamientoRow.fk_Sede, DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

                If (asignacionDataTable.Count > 0) Then
                    DesktopGlobal.ServidorImagenRow = dbmCore.SchemaImaging.TBL_Servidor.DBGet(asignacionDataTable(0).fk_Entidad_Servidor, asignacionDataTable(0).fk_Servidor)(0).ToTBL_ServidorSimpleType()
                Else
                    MessageBox.Show("El equipo actual [" & Environment.MachineName & "] no esta relacionado con un Servidor de Imágenes.", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

            Catch ex As Exception
                DMB.DesktopMessageShow("Inicio", ex)
                Return
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            'Carga formato de fecha
            DesktopGlobal.IdentifierDateFormat = IdentifierDateFormat
            DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = DesktopGlobal.IdentifierDateFormat
            DBImaging.DBImagingDataBaseManager.IdentifierDateFormat = DesktopGlobal.IdentifierDateFormat


            Dim errorMapeo As Slyg.Tools.Mapping.Mapeo.EnumNET_ERROR

            ' Mapear unidades de red
            For Each rowUnidadMapeo As DBSecurity.SchemaConfig.TBL_Unidad_MapeoRow In unidadMapeoDataTable.Rows
                If rowUnidadMapeo.Activa_Unidad_Mapeo Then
                    Dim unidadMapeo As Slyg.Tools.Mapping.Mapeo

                    If rowUnidadMapeo.Usar_Usuario_Contexto Then
                        unidadMapeo = New Slyg.Tools.Mapping.Mapeo(rowUnidadMapeo.Unidad_Mapeo, rowUnidadMapeo.Carpeta_Unidad_Mapeo)
                    Else
                        unidadMapeo = New Slyg.Tools.Mapping.Mapeo(rowUnidadMapeo.Unidad_Mapeo, rowUnidadMapeo.Carpeta_Unidad_Mapeo, rowUnidadMapeo.User_Unidad_Mapeo, rowUnidadMapeo.Password_Unidad_Mapeo)
                    End If

                    unidadesMapeo.Add(unidadMapeo)
                    errorMapeo = unidadMapeo.Conectar()

                    If errorMapeo <> Slyg.Tools.Mapping.Mapeo.EnumNET_ERROR.NO_ERROR Then
                        MessageBox.Show("No se pudo realizar la conexión a [" & rowUnidadMapeo.Nombre_Unidad_Mapeo & "]: " & unidadMapeo.MensajeError(errorMapeo), AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            Next

            'Carga los esquemas de facturación
            Dim dmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
            Try
                dmArchiving = New DBArchiving.DBArchivingDataBaseManager(DesktopGlobal.ConnectionStrings.Archiving)

                dmArchiving.Connection_Open(Sesion.Usuario.id)
                Dim dtEsquema = dmArchiving.SchemaBill.TBL_Esquema.DBFindByfk_Entidad(Sesion.Entidad.id)

                If Not dtEsquema.Count > 0 Then
                    MessageBox.Show("No existen esquemas de facturación asignados a la entidad [" & Sesion.Entidad.Nombre & "].", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Catch ex As Exception
                DMB.DesktopMessageShow("Inicio: Esquemas Facturación", ex)
                Return
            Finally
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
            End Try

            ' Mostrar usuario
            MainForm.PCNameToolStripStatusLabel.Text = Environment.MachineName
            MainForm.IPToolStripStatusLabel.Text = GetClientIpAddress()

            ' Mostrar Versión
            MainForm.VersionToolStripStatusLabel.Text = AssemblyVersion
            MainForm.PublicacionToolStripStatusLabel.Text = AssemblyVersionPublicacion

            ' Centro de procesamiento
            MainForm.EntidadToolStripStatusLabel.Text = Sesion.Entidad.Nombre
            MainForm.SedeToolStripStatusLabel.Text = sedeRow.Nombre_Sede
            MainForm.CentroToolStripStatusLabel.Text = DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
            MainForm.EntidadToolStripStatusLabel.Text = Sesion.Entidad.Nombre

            ' Usuario
            MainForm.UsuarioToolStripStatusLabel.Text = Sesion.Usuario.Apellidos.TrimEnd(" "c) & " " & Sesion.Usuario.Nombres.TrimEnd(" "c)

            CopiarPlugings()

            ' Mostrar formulario principal
            MainForm.ShowDialog()

            Dim usuari As New DBSecurity.SchemaSecurity.TBL_UsuarioType
            usuari.Logeado = False
            dbmSecurity.Connection_Open(usuari.id_Usuario)
            dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(usuari, Sesion.Usuario.id)
            dbmSecurity.Connection_Close()

        Catch ex As Exception
            DMB.DesktopMessageShow("Inicio", ex)
            End

        Finally
            ' Desconectar unidades de red
            For Each UnidadMapeo In unidadesMapeo
                UnidadMapeo.Desconectar()
            Next
        End Try
    End Sub

    Private Shared Sub CopiarPlugings()
        'Se crea la carpeta si no existe
        If Not (Directory.Exists(AppPath & "Plugins\")) Then Directory.CreateDirectory(AppPath & "Plugins\")

        Dim plugingList = Directory.GetFiles(AppPath, "*.Plugin.dll")
        For Each PluginFile In plugingList
            Dim newPluginFile = AppPath & "Plugins\" & Path.GetFileName(PluginFile)

            'Se elimina el archivo si existe
            If File.Exists(newPluginFile) Then File.Delete(newPluginFile)

            'Se mueve el archivo.
            File.Move(PluginFile, newPluginFile)
        Next
    End Sub

#End Region

#Region " Funciones "

    Private Shared Function IniciarSesion(ByRef nWebService As SecurityWebService, ByVal nUserName As String, ByVal nPassword As String) As Boolean
        Try
            nWebService.CrearCanalSeguro()
            nWebService.setUser(nUserName, nPassword)

            DesktopGlobal.ConnectionStrings = GetCadenasConexion(nWebService)
            DesktopGlobal.ConnectionURLStrings = GetCadenasConexionOCRUrls()
            DesktopGlobal.OCRParameterStrings = GetOCRParametros()

            Dim idEntidad As Short = -1
            Dim idUsuario As Integer = -1
            Dim logonResult = EnumValidateUser.LOGIN_ERROR

            If (nWebService.ValidateUser(idEntidad, idUsuario, logonResult)) Then

                Dim localSession = Sesion

                nWebService.FillSession(localSession, AssemblyName)

                Select Case (logonResult)
                    Case EnumValidateUser.CAMBIAR_PASSWORD
                        If (Sesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                            localSession.Usuario.Password = nPassword

                            MessageBox.Show("La contraseña ha vencido, por favor actualice la contraseña", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            Return ChangePassword(nPassword)
                        Else
                            MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Case EnumValidateUser.VALIDO
                        If (Sesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                            localSession.Usuario.Password = nPassword

                            Return True
                        Else
                            MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Case Else
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Select
            Else
                Select Case logonResult
                    Case EnumValidateUser.FALTA_LOGIN
                        MessageBox.Show("Debe ingresar el usuario", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case EnumValidateUser.INVALIDO_LOGIN
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case EnumValidateUser.INVALIDO_PASSWORD
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case EnumValidateUser.INACTIVO
                        MessageBox.Show("El usuario no se encuentra activo", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case Else
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

        Return False
    End Function

    Private Shared Function IniciarSesionExterno(ByRef nWebService As SecurityDMZWebService, ByVal nUserName As String, ByVal nPassword As String) As Boolean
        Try
            nWebService.CrearCanalSeguro()
            nWebService.setUser(nUserName, nPassword)

            DesktopGlobal.ConnectionStrings = GetCadenasConexionDMZ(nWebService)
            DesktopGlobal.ConnectionURLStrings = GetCadenasConexionOCRUrlsDMZ()
            DesktopGlobal.OCRParameterStrings = GetOCRParametrosDMZ()

            Dim idEntidad As Short = -1
            Dim idUsuario As Integer = -1
            Dim logonResult As SecurityDMZServiceReference.EnumValidateUser = SecurityDMZServiceReference.EnumValidateUser.LOGIN_ERROR

            If (nWebService.ValidateUser(idEntidad, idUsuario, logonResult)) Then

                Dim localSession = Sesion

                nWebService.FillSession(localSession, AssemblyName)

                Select Case (logonResult)
                    Case SecurityDMZServiceReference.EnumValidateUser.CAMBIAR_PASSWORD
                        If (Sesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                            localSession.Usuario.Password = nPassword

                            MessageBox.Show("La contraseña ha vencido, por favor actualice la contraseña", AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            Return ChangePassword(nPassword)
                        Else
                            MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Case SecurityDMZServiceReference.EnumValidateUser.VALIDO
                        If (Sesion.Usuario.PerfilManager.Permisos.Count > 0) Then
                            localSession.Usuario.Password = nPassword

                            Return True
                        Else
                            MessageBox.Show("El usuario no cuenta con permisos para ingresar a este módulo", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Case Else
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Select
            Else
                Select Case logonResult
                    Case SecurityDMZServiceReference.EnumValidateUser.FALTA_LOGIN
                        MessageBox.Show("Debe ingresar el usuario", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case SecurityDMZServiceReference.EnumValidateUser.INVALIDO_LOGIN
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case SecurityDMZServiceReference.EnumValidateUser.INVALIDO_PASSWORD
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case SecurityDMZServiceReference.EnumValidateUser.INACTIVO
                        MessageBox.Show("El usuario no se encuentra activo", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    Case Else
                        MessageBox.Show("Usuario o contraseña invalida", AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

        Return False
    End Function

    Public Shared Function GetCadenasConexion(ByRef nWebService As SecurityWebService) As DesktopConfig.TypeConnectionString
        Dim cadenas As New DesktopConfig.TypeConnectionString

        Dim connectionStrings = nWebService.getCadenasConexion()

        For Each Modulo In connectionStrings
            Select Case CType(Modulo.Id, DesktopConfig.Modulo)
                Case DesktopConfig.Modulo.Security : cadenas.Security = Modulo.ConnectionString
                Case DesktopConfig.Modulo.Imaging : cadenas.Imaging = Modulo.ConnectionString
                Case DesktopConfig.Modulo.Core : cadenas.Core = Modulo.ConnectionString
                Case DesktopConfig.Modulo.Archiving : cadenas.Archiving = Modulo.ConnectionString
                Case DesktopConfig.Modulo.Tools : cadenas.Tools = Modulo.ConnectionString
                Case DesktopConfig.Modulo.Softrac : cadenas.Softrac = Modulo.ConnectionString
                Case DesktopConfig.Modulo.OCR : cadenas.OCR = Modulo.ConnectionString
                Case DesktopConfig.Modulo.Integration : cadenas.Integration = Modulo.ConnectionString
            End Select
        Next

        Return cadenas
    End Function

    Public Shared Function GetCadenasConexionDMZ(ByRef nWebService As SecurityDMZWebService) As DesktopConfig.TypeConnectionString
        Dim cadenas As New DesktopConfig.TypeConnectionString

        Dim connectionStrings = nWebService.getCadenasConexion()

        For Each Modulo In connectionStrings
            Select Case CType(Modulo.Id, DesktopConfig.Modulo)
                Case DesktopConfig.Modulo.Security : cadenas.Security = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.Imaging : cadenas.Imaging = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.Core : cadenas.Core = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.Archiving : cadenas.Archiving = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.Tools : cadenas.Tools = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.Softrac : cadenas.Softrac = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.OCR : cadenas.OCR = Modulo.ConnectionString + Remoting
                Case DesktopConfig.Modulo.Integration : cadenas.Integration = Modulo.ConnectionString + Remoting
            End Select
        Next

        Return cadenas
    End Function

    ''' <summary>
    ''' Obtiene las cadenas de conexión para las URL relacionadas con el OCR.
    ''' </summary>
    ''' <returns>Objeto que contiene las cadenas de conexión para las distintas URL relacionadas con el OCR.</returns>
    Public Shared Function GetCadenasConexionOCRUrls() As DesktopConfig.TypeConnectionOCRURLsString
        'se debe corregir 20240806 JJBM
        Dim cadenas As New DesktopConfig.TypeConnectionOCRURLsString

        Dim connectionStrings = GetDataConexionOCRUrls()

        If connectionStrings IsNot Nothing Then
            For Each Modulo In connectionStrings
                Select Case Modulo.Nombre_ModuloURLs
                    Case DesktopConfig.ModuloOCRURLs.CoordinateTable : cadenas.CoordinateTable = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.Rectangle : cadenas.Rectangle = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.DetectTable : cadenas.DetectTable = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.CorrectSesgoImage : cadenas.CorrectSesgoImage = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.PreprocessImage : cadenas.PreprocessImage = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.CleanDataTable : cadenas.CleanDataTable = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.DeleteLinesImage : cadenas.DeleteLinesImage = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.GetFileHOCRContent : cadenas.GetFileHOCRContent = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.ValuePageOCR : cadenas.ValuePageOCR = Modulo.ConnectionURLsString
                End Select
            Next
        End If

        Return cadenas
    End Function

    ''' <summary>
    ''' Obtiene las cadenas de conexión para las URL relacionadas con el OCR.
    ''' </summary>
    ''' <returns>Objeto que contiene las cadenas de conexión para las distintas URL relacionadas con el OCR.</returns>
    Public Shared Function GetCadenasConexionOCRUrlsDMZ() As DesktopConfig.TypeConnectionOCRURLsString
        'se debe corregir 20240806 JJBM
        Dim cadenas As New DesktopConfig.TypeConnectionOCRURLsString

        Dim connectionStrings = GetDataConexionOCRUrlsDMZ()

        If connectionStrings IsNot Nothing Then
            For Each Modulo In connectionStrings
                Select Case Modulo.Nombre_ModuloURLs
                    Case DesktopConfig.ModuloOCRURLs.CoordinateTable : cadenas.CoordinateTable = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.Rectangle : cadenas.Rectangle = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.DetectTable : cadenas.DetectTable = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.CorrectSesgoImage : cadenas.CorrectSesgoImage = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.PreprocessImage : cadenas.PreprocessImage = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.CleanDataTable : cadenas.CleanDataTable = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.DeleteLinesImage : cadenas.DeleteLinesImage = Modulo.ConnectionURLsString
                    Case DesktopConfig.ModuloOCRURLs.GetFileHOCRContent : cadenas.GetFileHOCRContent = Modulo.ConnectionURLsString
                End Select
            Next
        End If

        Return cadenas
    End Function

    Public Shared Function GetOCRParametros() As DesktopConfig.TypeOCRParameterString
        Dim parameters As New DesktopConfig.TypeOCRParameterString

        Dim connectionParametersOCR = GetParametersDataTable()

        If connectionParametersOCR IsNot Nothing Then
            For Each parameterOCR In connectionParametersOCR
                Select Case parameterOCR.Nombre_Parametro
                    Case DesktopConfig.ModuloOCRParameters.PathOUTData : parameters.PathOUTData = parameterOCR.Valor_Parametro
                        'Case DesktopConfig.ModuloOCRParameters.ProcessMethodOCR
                        '    Dim processMethodValue As Integer
                        '    parameters.ProcessMethodOCR = If(Integer.TryParse(parameterOCR.Valor_Parametro, processMethodValue), processMethodValue, 1)
                    Case DesktopConfig.ModuloOCRParameters.MinWordConfidence
                        Dim minWordConfidenceValue As Integer
                        parameters.MinWordConfidence = If(Integer.TryParse(parameterOCR.Valor_Parametro, minWordConfidenceValue), minWordConfidenceValue, 60)
                    Case DesktopConfig.ModuloOCRParameters.ApplyWordConfidence
                        Dim resultApplyWordConfidence As Integer
                        Dim statusApplyWordConfidence = If(Integer.TryParse(parameterOCR.Valor_Parametro, resultApplyWordConfidence), resultApplyWordConfidence, 1)
                        parameters.ApplyWordConfidence = If(statusApplyWordConfidence = 0, False, True)

                End Select
            Next
        End If

        Return parameters
    End Function

    Public Shared Function GetOCRParametrosDMZ() As DesktopConfig.TypeOCRParameterString
        Dim parameters As New DesktopConfig.TypeOCRParameterString

        Dim connectionParametersOCR = GetParametersDataTableDMZ()

        If connectionParametersOCR IsNot Nothing Then
            For Each parameterOCR In connectionParametersOCR
                Select Case parameterOCR.Nombre_Parametro
                    Case DesktopConfig.ModuloOCRParameters.PathOUTData : parameters.PathOUTData = parameterOCR.Valor_Parametro
                        'Case DesktopConfig.ModuloOCRParameters.ProcessMethodOCR
                        '    Dim processMethodValue As Integer
                        '    parameters.ProcessMethodOCR = If(Integer.TryParse(parameterOCR.Valor_Parametro, processMethodValue), processMethodValue, 1)
                    Case DesktopConfig.ModuloOCRParameters.MinWordConfidence
                        Dim minWordConfidenceValue As Integer
                        parameters.MinWordConfidence = If(Integer.TryParse(parameterOCR.Valor_Parametro, minWordConfidenceValue), minWordConfidenceValue, 60)
                    Case DesktopConfig.ModuloOCRParameters.ApplyWordConfidence
                        Dim resultApplyWordConfidence As Integer
                        Dim statusApplyWordConfidence = If(Integer.TryParse(parameterOCR.Valor_Parametro, resultApplyWordConfidence), resultApplyWordConfidence, 1)
                        parameters.ApplyWordConfidence = If(statusApplyWordConfidence = 0, False, True)

                End Select
            Next
        End If

        Return parameters
    End Function

    ''' <summary>
    ''' Obtiene los datos de las conexiones para las URL relacionadas con el OCR desde la base de datos.
    ''' </summary>
    ''' <returns>Tabla de datos que contiene las conexiones para las distintas URL relacionadas con el OCR.</returns>
    Private Shared Function GetDataConexionOCRUrls() As DBOCR.SchemaConfig.TBL_ModuloURLsDataTable
        'se debe corregir 20240806 JJBM
        Dim dataTableOCRURLs As DBOCR.SchemaConfig.TBL_ModuloURLsDataTable = Nothing

        If DesktopGlobal.ConnectionStrings.OCR IsNot Nothing Then
            Dim dbmOCR As DBOCR.DBOCRDataBaseManager = Nothing

            Try
                dbmOCR = New DBOCR.DBOCRDataBaseManager(DesktopGlobal.ConnectionStrings.OCR)
                dbmOCR.Connection_Open(Sesion.Usuario.id)

                dataTableOCRURLs = dbmOCR.SchemaConfig.TBL_ModuloURLs.DBGet(Nothing)

            Catch ex As Exception
                If (dbmOCR IsNot Nothing) Then dbmOCR.Transaction_Rollback()
                Throw
            Finally
                If (dbmOCR IsNot Nothing) Then dbmOCR.Connection_Close()
            End Try
        End If
        Return dataTableOCRURLs
    End Function

    ''' <summary>
    ''' Obtiene los datos de las conexiones para las URL relacionadas con el OCR desde la base de datos.
    ''' </summary>
    ''' <returns>Tabla de datos que contiene las conexiones para las distintas URL relacionadas con el OCR.</returns>
    Private Shared Function GetDataConexionOCRUrlsDMZ() As DBOCR.SchemaConfig.TBL_ModuloURLsDataTable
        'se debe corregir 20240806 JJBM
        Dim dataTableOCRURLs As DBOCR.SchemaConfig.TBL_ModuloURLsDataTable = Nothing

        If DesktopGlobal.ConnectionStrings.OCR IsNot Nothing Then
            Try
                Dim queryResponse As QueryResponse = ClientUtil.resolver("DB_OCR.Config.TBL_ModuloURLs", New List(Of QueryParameter), QueryRequestType.Table, QueryResponseType.Table)

                dataTableOCRURLs = CType(ClientUtil.mapToTypedTable(New DBOCR.SchemaConfig.TBL_ModuloURLsDataTable(), queryResponse.dataTable), DBOCR.SchemaConfig.TBL_ModuloURLsDataTable)

            Catch ex As Exception
                Throw
            End Try
        End If
        Return dataTableOCRURLs
    End Function

    ''' <summary>
    ''' Obtiene los datos de los parametros relacionadas con el OCR desde la base de datos.
    ''' </summary>
    ''' <returns>Tabla de datos que contiene los parametros relacionadas con el OCR.</returns>
    Private Shared Function GetParametersDataTable() As DBOCR.SchemaConfig.TBL_ParametroDataTable
        'se debe corregir 20240806 JJBM
        Dim parametersDataTable As DBOCR.SchemaConfig.TBL_ParametroDataTable = Nothing
        If DesktopGlobal.ConnectionStrings.OCR IsNot Nothing Then
            Dim dbmOCR As DBOCR.DBOCRDataBaseManager = Nothing

            Try
                dbmOCR = New DBOCR.DBOCRDataBaseManager(DesktopGlobal.ConnectionStrings.OCR)
                dbmOCR.Connection_Open(Sesion.Usuario.id)

                parametersDataTable = dbmOCR.SchemaConfig.TBL_Parametro.DBGet(Nothing)
            Catch
                Throw
            Finally
                If dbmOCR IsNot Nothing Then
                    dbmOCR.Connection_Close()
                End If
            End Try
        End If
        Return parametersDataTable
    End Function

    ''' <summary>
    ''' Obtiene los datos de los parametros relacionadas con el OCR desde la base de datos.
    ''' </summary>
    ''' <returns>Tabla de datos que contiene los parametros relacionadas con el OCR.</returns>
    Private Shared Function GetParametersDataTableDMZ() As DBOCR.SchemaConfig.TBL_ParametroDataTable
        'se debe corregir 20240806 JJBM
        Dim parametersDataTable As DBOCR.SchemaConfig.TBL_ParametroDataTable = Nothing
        If DesktopGlobal.ConnectionStrings.OCR IsNot Nothing Then

            Try
                Dim queryResponse As QueryResponse = ClientUtil.resolver("DB_OCR.Config.TBL_Parametro", New List(Of QueryParameter), QueryRequestType.Table, QueryResponseType.Table)

                parametersDataTable = CType(ClientUtil.mapToTypedTable(New DBOCR.SchemaConfig.TBL_ParametroDataTable(), queryResponse.dataTable), DBOCR.SchemaConfig.TBL_ParametroDataTable)
            Catch
                Throw
            End Try
        End If
        Return parametersDataTable
    End Function

    Public Shared Function GetClientIpAddress() As String
        Dim localIp As String = ""

        For posicion As Integer = 0 To Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1
            Dim tempIp As String = Dns.GetHostEntry(Dns.GetHostName()).AddressList(posicion).ToString()

            If IsValidIp(tempIp) Then
                localIp = tempIp
                Exit For
            End If
        Next

        Return localIp
    End Function

    Private Shared Function IsValidIp(ByVal nIpAddress As String) As Boolean
        'create our match pattern
        Const pattern As String = "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"

        'create our Regular Expression object
        Dim check As New Regex(pattern)

        'boolean variable to hold the status
        Dim valid As Boolean

        'check to make sure an ip address was provided
        If (nIpAddress = "") Then
            'no address provided so return false
            valid = False
        Else
            'address provided so use the IsMatch Method
            'of the Regular Expression object
            valid = check.IsMatch(nIpAddress, 0)
        End If

        'return the results
        Return valid
    End Function

    Public Shared Function AlReadyloggedDMZ(ByVal nUser As String) As Boolean

        Dim ipClient As String
        Dim ipLoggedUsuario As String
        Dim valid As Boolean
        Dim logueado As Boolean

        'Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(DesktopGlobal.ConnectionStrings.Security)

        ipClient = GetClientIpAddress()


        Try
            ' Abrir conexion security
            'dbmSecurity.Connection_Open(Sesion.Usuario.id)

            Dim usuarios As DBSecurity.SchemaSecurity.TBL_UsuarioDataTable = Nothing
            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_core].security.TBL_Usuario", New List(Of QueryParameter) From {
                                                                     New QueryParameter With {.name = "Login_Usuario", .value = nUser.ToString()}
                                                                 }, QueryRequestType.Table, QueryResponseType.Table)
            usuarios = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaSecurity.TBL_UsuarioDataTable(), queryResponse.dataTable), DBSecurity.SchemaSecurity.TBL_UsuarioDataTable)


            'Dim usuarios = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nUser)

            If (usuarios.Count <> 0) Then
                Dim usuarioRow = usuarios(0)

                If (usuarioRow.IsLogeo_IPNull()) Then
                    ipLoggedUsuario = ""
                Else
                    ipLoggedUsuario = usuarioRow.Logeo_IP
                End If
                If usuarioRow.IsLogeadoNull Then
                    logueado = False
                Else
                    logueado = usuarioRow.Logeado
                End If

                If (logueado And (ipClient <> ipLoggedUsuario)) Then
                    MessageBox.Show("El usuario " + nUser + " ya ha ingresado en la máquina con IP " + ipLoggedUsuario, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    valid = False
                ElseIf ((logueado And (ipClient = ipLoggedUsuario)) Or (Not logueado)) Then
                    Dim usuarioType = New DBSecurity.SchemaSecurity.TBL_UsuarioType()
                    usuarioType.Logeado = True
                    usuarioType.Logeo_IP = ipClient
                    usuarioType.Logeo_Fecha = SlygNullable.SysDate

                    ' To do: 
                    'dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(usuarioType, usuarioRow.id_Usuario)
                    valid = True

                End If
            Else
                MessageBox.Show("Usuario no existe, por favor verificar")
                valid = False
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("IniciarSesion", ex)
            'Finally
            '    dbmSecurity.Connection_Close()
        End Try


        Return valid
    End Function


    Public Shared Function AlReadylogged(ByVal nUser As String) As Boolean

        Dim ipClient As String
        Dim ipLoggedUsuario As String
        Dim valid As Boolean
        Dim logueado As Boolean

        Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(DesktopGlobal.ConnectionStrings.Security)

        ipClient = GetClientIpAddress()


        Try
            ' Abrir conexion security
            dbmSecurity.Connection_Open(Sesion.Usuario.id)

            Dim usuarios = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nUser)

            If (usuarios.Count <> 0) Then
                Dim usuarioRow = usuarios(0)

                If (usuarioRow.IsLogeo_IPNull()) Then
                    ipLoggedUsuario = ""
                Else
                    ipLoggedUsuario = usuarioRow.Logeo_IP
                End If
                If usuarioRow.IsLogeadoNull Then
                    logueado = False
                Else
                    logueado = usuarioRow.Logeado
                End If

                If (logueado And (ipClient <> ipLoggedUsuario)) Then
                    MessageBox.Show("El usuario " + nUser + " ya ha ingresado en la máquina con IP " + ipLoggedUsuario, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    valid = False
                ElseIf ((logueado And (ipClient = ipLoggedUsuario)) Or (Not logueado)) Then
                    Dim usuarioType = New DBSecurity.SchemaSecurity.TBL_UsuarioType()
                    usuarioType.Logeado = True
                    usuarioType.Logeo_IP = ipClient
                    usuarioType.Logeo_Fecha = SlygNullable.SysDate

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(usuarioType, usuarioRow.id_Usuario)
                    valid = True

                End If
            Else
                MessageBox.Show("Usuario no existe, por favor verificar")
                valid = False
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("IniciarSesion", ex)
        Finally
            dbmSecurity.Connection_Close()
        End Try


        Return valid
    End Function

    Public Shared Function ChangePassword(ByVal nOldPasword As String) As Boolean
        Dim newPasswordForm As New FormNewPassword()

        newPasswordForm.OldPasswrd = nOldPasword

        Dim respuesta = newPasswordForm.ShowDialog()

        If (respuesta = DialogResult.OK) Then
            Sesion.Usuario.Password = newPasswordForm.NewPasswrd
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
