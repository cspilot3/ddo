Imports Miharu.Desktop.Library
Imports System.Configuration

Public Class Program

#Region " Declaraciones "


#End Region

#Region " Propiedades "

    Public Shared ReadOnly Property AppPath As String
        Get
            Return System.Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

    Public Const ConfigurationPrefix = "PluginAccionesValores_"

    Private Shared Properties As New Specialized.NameValueCollection

    Public Shared Property AccesoDesktopAssembly() As String = "Miharu.Desktop"
    Public Shared Property AccesoExportar As String = "6.1.6"

    Public Shared Property AccionesValoresEntidadId() As Short = 13
    Public Shared Property AccionesValoresProyectoImagingId() As Short = 1
    Public Shared Property AccionesValoresEsquemaImagingId() As Short = 1

    Shared Property TempPath As String = "temp\"

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

#End Region

#Region " Metodos "

    Public Shared Sub InicializarConfiguracion()
        Dim p As New Program()
        Dim props = p.GetType().GetProperties()
        For Each prop In props
            If (prop.CanWrite()) Then
                Try
                    Dim stringValue = System.Configuration.ConfigurationManager.AppSettings.Get(ConfigurationPrefix & prop.Name)

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
