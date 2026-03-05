Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Imaging.Library.Eventos
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports GraphicsMagick
Imports System.Runtime.InteropServices
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Data.SqlClient
Namespace Imaging.CruceInformacionDeceval.Exportar
    Public Class FormCruceInformacionDeceval
#Region " Declaraciones "
#End Region
#Region " Propiedades "
        Public Property Plugin() As Imaging.Exportar.Plugin
#End Region
#Region " Eventos "
        Private Sub CruceButton_Click(sender As System.Object, e As System.EventArgs) Handles CruceButton.Click
            CruzarInformacion()
        End Sub
        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub
#End Region
#Region " Metodos "
        Private Sub FormCruceInformacion_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarDatos()
        End Sub
        Private Sub CargarDatos()
            Me.GrillaPendientes.AutoGenerateColumns = False
            Me.GrillaPendientes.DataSource = getPendientes()
            Me.GrillaPendientes.Refresh()
        End Sub
        Private Sub CruzarInformacion()
            Me.GrillaCruzar.AutoGenerateColumns = False
            Me.GrillaCruzar.DataSource = getCruce()
            Me.GrillaCruzar.Refresh()
        End Sub
#End Region
#Region " Funciones "
        Private Function getPendientes() As DataTable
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim data As DataTable
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                data = dbmCore.SchemaProcess.PA_Cruce_Informacion_Archivo_Deceval.DBExecute(Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                1)
                Return data
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
            Return Nothing
        End Function
        Private Function getCruce() As DataTable
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim data As DataTable
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                data = dbmCore.SchemaProcess.PA_Cruce_Informacion_Archivo_Deceval.DBExecute(Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                2)
                Return data
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
            Return Nothing
        End Function
#End Region
    End Class
End Namespace