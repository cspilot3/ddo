Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config

Namespace Reportes.VisorReportes.Genericos

    Public Class Report_Estados_Operacion

#Region " Propiedades "

        Public Property Proyecto() As Short
            Get
                Return CShort(ProyectoDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                ProyectoDesktopComboBox.SelectedValue = value
            End Set
        End Property

        Public Property Entidad() As Short
            Get
                Return CShort(EntidadDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                EntidadDesktopComboBox.SelectedValue = value
            End Set
        End Property

        Public Property Esquema() As Short
            Get
                Return CShort(EsquemaDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                EsquemaDesktopComboBox.SelectedValue = value
            End Set
        End Property

        Public Property Tipo() As Short
            Get
                Return CShort(EsquemaDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                EsquemaDesktopComboBox.SelectedValue = value
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(1)

            TipoDesktopComboBox.SelectedIndex = 0

            Try
                Dim EntidadDataTable = dbmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
                Utilities.LlenarCombo(EntidadDesktopComboBox, EntidadDataTable, "id_Entidad", "Nombre_Entidad")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboEntidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            CargandoPictureBox.Visible = True
            CargandoPictureBox.Refresh()
            Ejecutar()
            CargandoPictureBox.Visible = False
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            dbmCore.Connection_Open(1)

            Try
                Dim ProyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))
                Utilities.LlenarCombo(ProyectoDesktopComboBox, ProyectoDataTable, "id_Proyecto", "Nombre_Proyecto")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboProyecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Metodos "


#End Region

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            dbmCore.Connection_Open(1)

            Try
                Dim EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBFindByfk_Entidadfk_Proyecto(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                Utilities.LlenarCombo(EsquemaDesktopComboBox, EsquemaDataTable, "id_Esquema", "Nombre_Esquema")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboEsquema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub Ejecutar()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(1)

            Try

                If TipoDesktopComboBox.SelectedIndex = 0 Then
                    Dim Reporte = dbmArchiving.SchemaReport.PA_Estado_Operacion.DBExecute(Entidad, Proyecto, Esquema)
                    ResultadosDataGridView.DataSource = Reporte
                Else
                    Dim Reporte = dbmArchiving.SchemaReport.PA_Estado_Operacion_Detalle.DBExecute(Entidad, Proyecto, Esquema)
                    ResultadosDataGridView.DataSource = Reporte
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboEsquema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            Exportar()
        End Sub

        Private Sub Exportar()
            If ResultadosDataGridView.RowCount > 0 Then
                Dim fs As FileStream
                Dim sSeparador As String = ","
                Try
                    SaveFileDialog.Filter = "Archivo CSV (*.csv)|*.csv"
                    SaveFileDialog.FileName = "Reporte " & EntidadDesktopComboBox.SelectedText & ".csv"
                    SaveFileDialog.Title = "Guardar Reporte"

                    'Se obtiene el separador seleccionado
                    If ComaRadioButton.Checked Then
                        sSeparador = ","
                    ElseIf TabuladorRadioButton.Checked Then
                        sSeparador = vbTab
                    ElseIf PuntoComaRadioButton.Checked Then
                        sSeparador = ";"
                    ElseIf VacioRadioButton.Checked Then
                        sSeparador = ""
                    End If

                    Dim Resultado = SaveFileDialog.ShowDialog()
                    If Resultado = DialogResult.OK Then
                        fs = CType(SaveFileDialog.OpenFile(), FileStream)
                        Utilities.DataGridViewToCSV(ResultadosDataGridView, fs, sSeparador, chkEncabezado.Checked)
                        DesktopMessageBoxControl.DesktopMessageShow("Se exporto el reporte al archivo: " & fs.Name & ".", "Reporte Exportado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    End If
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Exportar", ex)
                End Try
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No existen registros para exportar.", "No existen registros", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

    End Class

End Namespace