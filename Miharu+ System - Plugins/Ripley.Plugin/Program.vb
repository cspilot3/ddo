Imports System.Reflection

Public Class Program

#Region " Declaraciones "


#End Region

#Region " Propiedades "

    Friend Shared ModuloId As Short = 23

    Public Shared ReadOnly Property AppPath As String
        Get
            Return Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

    Public Const ConfigurationPrefix = "PluginRipley_"

    ' Proyecto al que responde el Plugin
    Public Shared Property RipleyEntidadId() As Short = 14
    Public Shared Property RipleyProyectoImagenesId() As Short = 1

    ' Módulo principal
    Public Shared Property AccesoDesktopAssembly() As String = "Miharu.Desktop"

    ' Accesos
    Public Shared Property AccesoMesaControl As String = "6.1.6"
    Public Shared Property PerfilAutorizacion As String = "8.*"
    
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
