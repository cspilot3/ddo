Imports System.Reflection
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Plugins

Public Class Program

#Region " Declaraciones "

    Private Shared _sesion As Security.Library.Session.Sesion
    Private Shared _desktopGlobal As DesktopGlobal
    Private Shared _riskGlobal As RiskGlobal

    Private Shared _pluginManager As DesktopPluginManager

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
            Return IO.Path.GetFileNameWithoutExtension([Assembly].GetExecutingAssembly().CodeBase)
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
    Friend Shared ReadOnly Property AppPath() As String
        Get
            Return Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

    Friend Shared Property Sesion() As Security.Library.Session.Sesion
        Get
            Return _sesion
        End Get
        Set(ByVal value As Security.Library.Session.Sesion)
            _sesion = value
        End Set
    End Property
    Friend Shared Property DesktopGlobal() As DesktopGlobal
        Get
            Return _DesktopGlobal
        End Get
        Set(ByVal value As DesktopGlobal)
            _DesktopGlobal = value
        End Set
    End Property
    Public Shared Property RiskGlobal As RiskGlobal
        Get
            Return _RiskGlobal
        End Get
        Set(ByVal value As RiskGlobal)
            _RiskGlobal = value
        End Set
    End Property

    Public Shared ReadOnly Property TempPath() As String
        Get
            Return "temp\"
        End Get
    End Property

    Public Shared Property PluginManager As DesktopPluginManager
        Get
            Return _PluginManager
        End Get
        Set(ByVal value As DesktopPluginManager)
            _PluginManager = value
        End Set
    End Property

    Public Shared Property AccesoDesktopAssembly() As String = "Miharu.Desktop"

#End Region

End Class
