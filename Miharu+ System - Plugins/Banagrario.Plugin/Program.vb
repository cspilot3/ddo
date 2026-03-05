Imports System.Configuration
Imports System.Reflection
Imports System.IO

Public Class Program

#Region " Declaraciones "

    Public Const TempPath As String = "temp\"

    Public Shared PermisoMenuConfiguracionBanagrario As String = "11.1"

#End Region

#Region " Propiedades "

    Public Shared ReadOnly Property AppPath As String
        Get
            Return Windows.Forms.Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

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

    Public Const ConfigurationPrefix = "PluginBanagrario_"

    Public Shared Property ModuloId() As Short = 13
    Public Shared Property AccesoDesktopAssembly() As String = "Miharu.Desktop"

    Public Shared Property PrecintoMinCaracteres() As Integer = 3
    Public Shared Property PrecintoMaxCaracteres() As Integer = 15

    Public Shared Property OTMaximoDiasAdelante() As Integer = 30
    Public Shared Property OTDiasDiasSinCerrar() As Integer = 200
    Public Shared Property OTDiasAtras As Integer = 30

    Public Shared Property BanagrarioEntidadId() As Short = 9
    Public Shared Property BanagrarioProyectoRiskId() As Short = 1
    Public Shared Property BanagrarioEsquemaRiskId() As Short = 1

    Public Shared Property BanagrarioProyectoImagingId() As Short = 2
    Public Shared Property FirmasProyectoImagingId() As Short = 3

    Public Shared Property BanagrarioDocumentoTapaId() As Integer = 94

    Public Shared Property Tapa_CampoId_Oficina() As Short = 1
    Public Shared Property Tapa_CampoId_Fecha() As Short = 2
    Public Shared Property Tapa_CampoId_Contenedor() As Short = 3
    Public Shared Property Tapa_CampoId_CantidadSegunContenedor() As Short = 4
    Public Shared Property Tapa_CampoId_CantidadFisicos() As Short = 5
    Public Shared Property Tapa_CampoId_DocumentosOrdenados() As Short = 6
    Public Shared Property Tapa_CampoId_TipoContenedor() As Short = 7
    Public Shared Property Tapa_CampoId_TipoMovimiento() As Short = 8
    Public Shared Property Tapa_CampoId_Anexo() As Short = 9
    Public Shared Property Tapa_CampoId_PresentaNovedades() As Short = 10
    Public Shared Property Tapa_CampoId_CoincideFechaAnexo23() As Short = 11


    Public Shared Property Proyecto_LlaveId_Oficina() As Short = 1
    Public Shared Property Proyecto_LlaveId_Fecha() As Short = 2
    Public Shared Property Proyecto_LlaveId_Contenedor() As Short = 3

    Public Shared Property Banagrario_ListaOficinaId() As Short = 1
    Public Shared Property Banagrario_ListaTipoMovimientoId() As Short = 2
    Public Shared Property Banagrario_ListaTipoProductoId() As Short = 27
    Public Shared Property Banagrario_ListaTipoContenedorId() As Short = 4

    Public Shared Property CajaMinCaracteres() As Integer = 4
    Public Shared Property CajaMaxCaracteres() As Integer = 12

    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property

    Public Shared Property ImagingGlobal() As Miharu.Desktop.Library.Config.ImagingGlobal

    Public Shared Property DesktopGlobal() As Miharu.Desktop.Library.Config.DesktopGlobal

    Public Shared Property Sesion() As Miharu.Security.Library.Session.Sesion

#End Region

#Region " Metodos "

    Public Shared Sub InicializarConfiguracion()
        Dim p As New Program()
        Dim props = p.GetType().GetProperties()
        For Each prop In props
            If (prop.CanWrite()) Then
                Try
                    Dim stringValue = ConfigurationManager.AppSettings.Get(ConfigurationPrefix & prop.Name)

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
