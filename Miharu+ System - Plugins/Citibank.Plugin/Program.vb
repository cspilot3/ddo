Imports System.Reflection
Imports Miharu.Desktop.Library.Config
Imports System.IO

Public Class Program

#Region " Propiedades "

    Friend Shared ModuloId As Short = 30

    Public Shared ReadOnly Property AppPath As String
        Get
            Return Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

    Public Const ConfigurationPrefix = "PluginCitibank_"

    ' Proyecto al que responde el Plugin
    Public Shared Property CitibankEntidadId() As Short = 35
    Public Shared Property Imagenes_Asistidos_ProyectoId() As Short = 2
    Public Shared Property Imagenes_Embargos_ProyectoId() As Short = 5
    Public Shared Property Imagenes_DesEmbargos_ProyectoId() As Short = 6

    ' Módulo principal
    Public Shared Property AccesoDesktopAssembly() As String = "Miharu.Desktop"

    ' Accesos
    Public Shared Property AccesoMesaControl As String = "6.1.6"
    Public Shared Property PerfilAutorizacion As String = "8.*"

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

    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property

#End Region

#Region " Metodos "

    Shared Property TempPath As String = "temp\"

    Public Shared Sub InicializarConfiguracion()
        Dim p As New Program()
        Dim props = p.GetType().GetProperties()
        For Each prop In props
            If (prop.CanWrite()) Then
                Try
                    Dim stringValue = Configuration.ConfigurationManager.AppSettings.Get(ConfigurationPrefix & prop.Name)

                    If (Not stringValue Is Nothing AndAlso stringValue.ToString().Trim() <> "") Then
                        Dim val = Convert.ChangeType(stringValue, prop.PropertyType)
                        prop.SetValue(p, val, Nothing)
                    End If
                Catch : End Try
            End If
        Next
    End Sub

#End Region

End Class
