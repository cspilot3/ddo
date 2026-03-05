Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools
Imports Miharu.Risk.Library.Eventos
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Reportes.VisorReportes.ReporteNovedadesDeceval
    Public Class FormExport
#Region " Variables "

        Dim _EventManager As EventManager
        Dim dtLineaProceso As DBArchiving.SchemaRisk.TBL_Linea_ProcesoDataTable = Nothing
#End Region

#Region " Propiedades "

#End Region

#Region " Eventos "
        Private Sub FormExport_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            'Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            'dbmCore.Connection_Open(1)

            'Try
            '    Dim EntidadDataTable = dbmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
            '    Utilities.LlenarCombo(EntidadDesktopComboBox, EntidadDataTable, "id_Entidad", "Nombre_Entidad")
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message, "LlenarComboEntidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Finally
            '    dbmCore.Connection_Close()
            'End Try
        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click
            CargarLineasProceso()
        End Sub

        Private Sub GenerarReporteButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGenerarReporteNovedades.Click
            GenerarReporte()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub
#End Region

#Region " Metodos "

        'Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        '    Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

        '    dbmCore.Connection_Open(1)

        '    Try
        '        Dim ProyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))
        '        Utilities.LlenarCombo(ProyectoDesktopComboBox, ProyectoDataTable, "id_Proyecto", "Nombre_Proyecto")
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, "LlenarComboProyecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Finally
        '        dbmCore.Connection_Close()
        '    End Try
        'End Sub

        'Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        '    Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

        '    dbmCore.Connection_Open(1)

        '    Try
        '        Dim EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBFindByfk_Entidadfk_Proyecto(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
        '        Utilities.LlenarCombo(EsquemaDesktopComboBox, EsquemaDataTable, "id_Esquema", "Nombre_Esquema")
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, "LlenarComboEsquema", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Finally
        '        dbmCore.Connection_Close()
        '    End Try
        'End Sub


        Private Sub CargarLineasProceso()
            'Me.LineasProcesoDataGridView.AutoGenerateColumns = False
            Me.LineasProcesoDataGridView.DataSource = getLineasProceso()
            Me.LineasProcesoDataGridView.Refresh()

            If (Me.LineasProcesoDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron Líneas de Proceso para la fecha de proceso seleccionada", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Me.LineasProcesoDataGridView.Columns("Estado").Visible = False
                Me.LineasProcesoDataGridView.Columns("Activo").Visible = False
            End If
        End Sub

        Private Sub GenerarReporte()
            Try
                Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim Estado As New DBCore.EstadoEnum
                Dim Activo As New Boolean

                Dim listaLineasProceso As List(Of String) = New List(Of String)
                Dim listaLineasProcesoSinCerrar As List(Of String) = New List(Of String)
                Dim Cadenalineas As String = Nothing
                Dim i As Integer
                For i = 0 To LineasProcesoDataGridView.RowCount - 1
                    If CBool(LineasProcesoDataGridView.Rows(i).Cells(0).Value) = True Then

                        Estado = CType(CInt(LineasProcesoDataGridView.Rows(i).Cells(2).Value), DBCore.EstadoEnum)
                        Activo = CBool(LineasProcesoDataGridView.Rows(i).Cells(3).Value)
                        If (Estado = DBCore.EstadoEnum.Empacado And Activo = False) Then
                            listaLineasProceso.Add(CStr(LineasProcesoDataGridView.Rows(i).Cells(1).Value))
                        Else
                            listaLineasProcesoSinCerrar.Add(CStr(LineasProcesoDataGridView.Rows(i).Cells(1).Value))
                        End If
                    End If
                Next

                If (listaLineasProcesoSinCerrar.Count = 0) Then
                    If (listaLineasProceso.Count > 0) Then
                        Program.Sesion.Parameter("_LineasProceso") = listaLineasProceso
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una línea de proceso que se encuentre cerrada", "Problemas al generar reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Else
                    'Sacar las líneas de proceso que no se encuentren cerradas
                    For i = 0 To listaLineasProcesoSinCerrar.Count - 1
                        If i = 0 Then
                            Cadenalineas = listaLineasProcesoSinCerrar(i)
                        Else
                            Cadenalineas = Cadenalineas & "," & listaLineasProcesoSinCerrar(i)
                        End If
                    Next
                    DesktopMessageBoxControl.DesktopMessageShow("Por favor validar que la(s) línea(s) " & Cadenalineas & " se encuentre(n) cerrada(s).", "Problemas al generar reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.DoEvents()

                Return
            Finally
                Me.Enabled = True
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function getFechaInicio() As Date
            Return CDate(Me.FechaInicioDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getFechaFin() As Date
            Return CDate(Me.FechaFinDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getLineasProceso() As DataTable
            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing
            Dim _dtLineasProceso As DataTable = Nothing

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim stringInicio As String = FechaInicioDateTimePicker.Value.ToString("yyyy/MM/dd")
                Dim fechaInicio As Date = CDate(stringInicio)
                Dim stringFin As String = FechaFinDateTimePicker.Value.ToString("yyyy/MM/dd")
                Dim fechaFin As Date = CDate(stringFin)

                _dtLineasProceso = dbmArchiving.Schemadbo.PA_Get_LineasProcesoXFechaCargue.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, fechaInicio, fechaFin)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return _dtLineasProceso
        End Function
#End Region
    End Class
End Namespace