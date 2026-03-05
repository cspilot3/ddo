Imports System.Reflection
Imports Miharu.Security.Library.WebService

Namespace Clases

    Public Class Program

#Region " ESTRUCTURAS "

        Public Structure TypeConnectionString
            Public Security As String
            Public Workflow As String
            Public Tools As String
            Public Core As String
        End Structure

#End Region

#Region " DECLARACIONES "

        Public Structure TypeFileServerConfig
            Public FileServerIPName As String
            Public FileServerAppName As String
            Public FileServerPort As Integer
            Public FileServerPath As String
        End Structure

#End Region

#Region " PROPIEDADES "

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
                Return System.IO.Path.GetFileNameWithoutExtension([Assembly].GetExecutingAssembly().CodeBase)
            End Get
        End Property
        Friend Shared ReadOnly Property AssemblyVersion() As String
            Get
                Return [Assembly].GetExecutingAssembly().GetName().Version.ToString()
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

#End Region

#Region " FUNCIONES "

        Public Shared Function getSecurityWebServiceURL() As String
            Dim rootWebConfig As System.Configuration.Configuration

            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/web.config")

            If (rootWebConfig.AppSettings.Settings.Count > 0) Then
                Dim customSetting As System.Configuration.KeyValueConfigurationElement

                customSetting = rootWebConfig.AppSettings.Settings("WebService.SecurityService")

                If Not (customSetting.Value Is Nothing) Then
                    Return customSetting.Value
                End If
            End If

            Return ""
        End Function
        Public Shared Function getCadenasConexion(ByRef nWebService As SecurityWebService) As TypeConnectionString
            Dim Cadenas As New TypeConnectionString
            Dim ConnectionStrings As List(Of TypeModulo)

            ConnectionStrings = nWebService.getCadenasConexion()

            For Each Modulo As Miharu.Security.Library.WebService.TypeModulo In ConnectionStrings
                Select Case Modulo.Id
                    Case 0 : Cadenas.Security = Modulo.ConnectionString
                    Case 1 : Cadenas.Workflow = Modulo.ConnectionString
                    Case 3 : Cadenas.Tools = Modulo.ConnectionString
                    Case 6 : Cadenas.Core = Modulo.ConnectionString 'Módulo del security
                End Select
            Next

            Return Cadenas
        End Function

        Public Shared Function getActiveXValidateUser() As Boolean
            Dim rootWebConfig As System.Configuration.Configuration

            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/web.config")

            If (rootWebConfig.AppSettings.Settings.Count > 0) Then
                Dim customSetting As System.Configuration.KeyValueConfigurationElement

                customSetting = rootWebConfig.AppSettings.Settings("ActiveX.ValidateUser")

                If Not (customSetting.Value Is Nothing) Then
                    Return CBool(customSetting.Value)
                End If
            End If

            Return True
        End Function

        Public Shared Function getFileServerConfig() As TypeFileServerConfig
            Dim rootWebConfig As System.Configuration.Configuration
            Dim FileServerConfig As New TypeFileServerConfig

            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/web.config")

            If (rootWebConfig.AppSettings.Settings.Count > 0) Then
                FileServerConfig.FileServerIPName = rootWebConfig.AppSettings.Settings("FileServerIPName").Value
                FileServerConfig.FileServerAppName = rootWebConfig.AppSettings.Settings("FileServerAppName").Value
                FileServerConfig.FileServerPort = CInt(rootWebConfig.AppSettings.Settings("FileServerPort").Value)
                FileServerConfig.FileServerPath = rootWebConfig.AppSettings.Settings("FileServerPath").Value
            End If

            Return FileServerConfig

        End Function

        Public Shared Function getIdentifierDateFormat() As String
            Dim rootWebConfig As System.Configuration.Configuration

            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/web.config")

            If (rootWebConfig.AppSettings.Settings.Count > 0) Then
                Dim customSetting As System.Configuration.KeyValueConfigurationElement

                customSetting = rootWebConfig.AppSettings.Settings("IdentifierDateFormat")

                If Not (customSetting.Value Is Nothing) Then
                    Return customSetting.Value
                End If
            End If

            Return ""
        End Function

        Public Shared Function getIdentifierDateDataBaseFormat() As String
            Dim rootWebConfig As System.Configuration.Configuration

            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/web.config")

            If (rootWebConfig.AppSettings.Settings.Count > 0) Then
                Dim customSetting As System.Configuration.KeyValueConfigurationElement

                customSetting = rootWebConfig.AppSettings.Settings("IdentifierDateDataBaseFormat")

                If Not (customSetting.Value Is Nothing) Then
                    Return customSetting.Value
                End If
            End If

            Return ""
        End Function

#End Region

    End Class
End Namespace