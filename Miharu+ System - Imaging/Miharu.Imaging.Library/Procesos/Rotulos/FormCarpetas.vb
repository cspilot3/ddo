Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Imaging.Library.Eventos
Imports System.Xml.Linq
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Threading
Public Class FormCarpetas


#Region " Propiedades "

    Public Property EventManager As EventManager

    Public Property IdOT() As Integer

    Public Property IdEmpaque() As Short

    Public Property IdEmpaqueContenedor() As Short

#End Region

    Private _cedula As String
    Private _p2 As Boolean
    Private Carpetas As String

    Sub New(Cedula As String, p2 As Boolean)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _cedula = Cedula
        _p2 = p2
    End Sub

    Private Sub FormCarpetas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CargarProyectos()
    End Sub

    Private Sub CargarProyectos()
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Try

            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Dim DataTableRotulo = dbmImaging.SchemaProcess.PA_Rotulo_Carpeta.DBExecute(_cedula, CType(0, Global.Slyg.Tools.SlygNullable(Of Integer)), "ObtenerCarpeta")
            If DataTableRotulo.Rows.Count > 0 Then
                Me.CarpetasDataGridView.Visible = True
                Me.CarpetasDataGridView.AutoGenerateColumns = False
                Me.CarpetasDataGridView.DataSource = DataTableRotulo
                Me.CarpetasDataGridView.Refresh()
            Else
                If (Me.CarpetasDataGridView.RowCount = 0) Then
                    MessageBox.Show("No se encontraron Carpetas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub
    Private Sub CarpetasDataGridView_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CarpetasDataGridView.CellContentClick
        Carpetas = CarpetasDataGridView.CurrentRow.Cells("Expediente").Value.ToString()
        If Validar() Then
            If Not Carpetas = "" Then
                Dim objVisorRotuloCarpeta As New FormVisorRotuloCarpeta(Carpetas, IdOT, False, 0, "")
                objVisorRotuloCarpeta.ShowDialog()
            End If
        End If
    End Sub
    Private Function Validar() As Boolean
        Dim Valido As Boolean

        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Try

            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Dim OT = dbmImaging.SchemaProcess.PA_Rotulo_Carpeta.DBExecute(_cedula, CType(Carpetas, Global.Slyg.Tools.SlygNullable(Of Integer)), "OT")
            Dim fk_OT As Integer
            For Each row As DataRow In OT.Rows
                fk_OT = CInt(row("fk_OT"))
            Next
            Dim DataOt = dbmImaging.SchemaProcess.TBL_OT.DBGet(fk_OT)
            If DataOt.Rows.Count > 0 Then
                If DataOt(0).Cerrado = True Then
                    Valido = True
                Else
                    Valido = False
                    DesktopMessageBoxControl.DesktopMessageShow("La OT no ha sido totalmente procesada.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Else
                Valido = False
                DesktopMessageBoxControl.DesktopMessageShow("No se encontro la OT a procesar.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
        Return Valido
    End Function
End Class