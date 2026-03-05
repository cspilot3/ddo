Imports System.Reflection

Public Class Program

#Region " Propiedades "

    Public Const TempPath As String = "temp\"

    Public Shared ReadOnly Property AppPath As String
        Get
            Return Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

    ' Módulo principal
    Public Shared Property AccesoDesktopAssembly() As String = "Miharu.Desktop"

    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property

#End Region

End Class
