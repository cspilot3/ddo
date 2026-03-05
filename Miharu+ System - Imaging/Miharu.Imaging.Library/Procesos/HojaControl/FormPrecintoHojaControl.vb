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
Imports Miharu.Desktop.Controls.DesktopReportViewer

Public Class FormPrecintoHojaControl

    Private _cedula As String
    Private _operacion As String
    Private Carpetas As String
    Private RutaHojaControl As String

    Sub New(ncedula As String, noperacion As String)
        InitializeComponent()
        _cedula = ncedula
        _operacion = noperacion
    End Sub

    Private Sub FormCarpetas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CargarCombo()

    End Sub

    Private Sub CargarPrecinto(value As String)
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Try

            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Me.PrecintoDataGridView.Visible = True
            Me.PrecintoDataGridView.AutoGenerateColumns = False
            Me.PrecintoDataGridView.DataSource = dbmImaging.SchemaProcess.PA_Consulta_Hoja_Control_Empaque.DBExecute("", _cedula, "Caja", CStr(value), Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.Proyecto)
            Me.PrecintoDataGridView.Refresh()

            If (Me.PrecintoDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron Precintos para la Cedula", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub
    Private Sub CargarCombo()
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        dbmCore.Connection_Open(1)
        TipoGestionComboBox.DropDownStyle = ComboBoxStyle.DropDownList
        TipoGestionComboBox.DataSource = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, 9)
        TipoGestionComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
        TipoGestionComboBox.ValueMember = "Valor_Campo_Lista_Item"
    End Sub

    Private Sub PrecintoDataGridView_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles PrecintoDataGridView.CellContentClick
        Carpetas = PrecintoDataGridView.CurrentRow.Cells("Precinto").Value.ToString()
        Dim Reporte = New DesktopReportViewerHojaControl
        Dim crearHojaControl As New CrearHojaControl(Reporte)
        Dim Cedulas As New DataTable
        Dim workCol As DataColumn = Cedulas.Columns.Add("Cedulas", Type.GetType("System.String"))
        Dim workRow As DataRow = Cedulas.NewRow()
        workRow("Cedulas") = _cedula
        Cedulas.Rows.Add(workRow)

        If Validar(Carpetas) Then
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Dim DataTableHojaControl = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@RutaHojaControl")
            For Each item As DataRow In DataTableHojaControl
                RutaHojaControl = CStr(item("Valor_Parametro_Sistema"))
            Next

            If _operacion = "Generar" Then
                Dim crear = dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(_cedula, "Creacion", Program.Sesion.Usuario.id, 0, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                If (crear.Rows.Count <= 0) Then
                    crearHojaControl.ObtenerReporteTotal(Cedulas, RutaHojaControl + "\" + Carpetas, "Guardar")
                    crearHojaControl.ObtenerReporteTotal(Cedulas, "", "Imprimir")
                Else
                    If DesktopMessageBoxControl.DesktopMessageShow("Ya se ha generado esta Hoja de Control ¿Esta seguro de generarla nuevamente?", "Hoja de Control", DesktopMessageBoxControl.IconEnum.WarningIcon, True) <> DialogResult.OK Then
                        Return
                    End If
                    crearHojaControl.ObtenerReporteTotal(Cedulas, RutaHojaControl + "\" + Carpetas, "Guardar")
                    crearHojaControl.ObtenerReporteTotal(Cedulas, "", "Imprimir")
                End If
            ElseIf _operacion = "Reimprimir" Then
                Dim validate = dbmImaging.SchemaProcess.PA_Crear_Hoja_Control.DBExecute(_cedula, "Reimprimir", Program.Sesion.Usuario.id, 0, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                If (validate.Rows.Count <= 0) Then
                    DesktopMessageBoxControl.DesktopMessageShow("Aun no se ha generado hoja de control para esta cedula", "Hoja de control", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                Else
                    crearHojaControl.ObtenerReporteTotal(Cedulas, "", "Imprimir")
                End If
            End If

        End If
    End Sub
    Private Function Validar(Precinto As String) As Boolean

        Dim Valido As Boolean
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Dim OT = dbmImaging.SchemaProcess.PA_Consulta_Hoja_Control_Empaque.DBExecute(Precinto, "", "OTPrecinto", CStr(TipoGestionComboBox.SelectedValue), Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
            Dim fk_OT As Integer
            For Each row As DataRow In OT.Rows
                fk_OT = CInt(row("fk_OT"))
            Next

            Dim DataOt = dbmImaging.SchemaProcess.TBL_OT.DBGet(fk_OT)
            If DataOt.Rows.Count > 0 Then
                Valido = True
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

    Private Sub TipoGestionComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TipoGestionComboBox.SelectedIndexChanged
        Dim value As Object = TipoGestionComboBox.SelectedValue
        If Not (TypeOf value Is DataRowView) Then
            CargarPrecinto(CStr(value))
        End If
        Return
    End Sub
End Class