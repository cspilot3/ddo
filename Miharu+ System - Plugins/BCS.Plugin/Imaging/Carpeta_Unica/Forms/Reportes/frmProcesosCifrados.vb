Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.FileProvider.Library
Imports System.Windows.Forms
Imports System.IO
Imports Slyg.Tools
Imports Miharu.Imaging
Imports Slyg.Tools.Imaging
Imports Miharu.Tools.Progress
Imports Ionic.Zip
Imports System.Dynamic
Imports System.Threading
Imports BCS.Plugin.Library
Imports DBImaging.SchemaCore
Imports DBImaging.SchemaSecurity
Imports Slyg.Tools.Imaging.ImageManager
Imports BCS.Plugin.Imaging.Carpeta_Unica

Public Class frmProcesosCifrados

    Private _Plugin As CarpetaUnicaPlugin
    Private _StrFechaProceso As String

    Public Sub New(plugin As CarpetaUnicaPlugin, strFechaProceso As String)
        Me._Plugin = plugin
        Me._StrFechaProceso = strFechaProceso
        InitializeComponent()
    End Sub


    Private Sub frmProcesosCifrados_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

        Try
            dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
            dbmIntegration.Transaction_Begin()
            Dim Cifrados = dbmIntegration.SchemaBCSCarpetaUnica.PA_Cifrado_EncriptarBCS.DBExecute(Me._StrFechaProceso, Me._Plugin.Manager.Sesion.Usuario.id)
            dbmIntegration.Transaction_Commit()

            Me.dtCifrados.DataSource = Cifrados

        Catch ex As Exception
            dbmIntegration.Transaction_Rollback()
            DesktopMessageBoxControl.DesktopMessageShow("Error al cargar cifrados", ex)
        Finally
            dbmIntegration.Connection_Close()
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class