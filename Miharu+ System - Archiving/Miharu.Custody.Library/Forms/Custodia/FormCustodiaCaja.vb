Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBCore
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports System.ComponentModel
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports System.Data.SqlClient
Imports DBSecurity
Imports DBImaging.SchemaSecurity

Namespace Forms.Custodia
    Public Class FormCustodiaCaja
        Inherits FormBase

#Region " Eventos "
        Private Sub FormCustodiaCaja_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaFiltros()
        End Sub

        Private Sub guardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AsignarGuiaButton.Click
            Guardar()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            CargaFiltroProyecto(Program.Sesion.Usuario.id, CShort(EntidadDesktopComboBox.SelectedValue))
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            CargaGrilla()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub CustodiaCajaDesktopData_EditingControlShowing(ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs) Handles CustodiaCajaDesktopData.EditingControlShowing
            Dim comboBox As ComboBox = Nothing
            If TypeOf e.Control Is DataGridViewComboBoxEditingControl Then
                comboBox = CType(e.Control, ComboBox)
                If (comboBox IsNot Nothing) Then
                    RemoveHandler comboBox.SelectedIndexChanged, New EventHandler(AddressOf ComboBox_SelectedIndexChanged)
                    AddHandler comboBox.SelectedIndexChanged, New EventHandler(AddressOf ComboBox_SelectedIndexChanged)
                End If
            End If
        End Sub

        Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                Dim gData As Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl = CType(comboBox.Parent.Parent, Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl)
                Dim rowIndex As Integer = gData.CurrentCell.RowIndex
                Dim RowData As DataGridViewRow = CustodiaCajaDesktopData.Rows(rowIndex)
                Dim comboBoxEntidad As DataGridViewComboBoxCell = CType(RowData.Cells(6), DataGridViewComboBoxCell)
                Dim comboBoxSede As DataGridViewComboBoxCell = CType(RowData.Cells(7), DataGridViewComboBoxCell)
                Dim comboBoxBoveda As DataGridViewComboBoxCell = CType(RowData.Cells(8), DataGridViewComboBoxCell)
                Dim datos As New DataTable
                datos.Columns.Add("id")
                datos.Columns.Add("Nombre")
                Dim Aux As Integer = gData.CurrentCellAddress.X
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                Select Case Me.CustodiaCajaDesktopData.CurrentCellAddress.X
                    Case 6
                        Dim idEntidad As Short = 0
                        Try
                            idEntidad = CShort(comboBox.SelectedValue)
                            Dim tsede = dbmCore.SchemaCustody.PA_Get_Caja_Pendiente_Custodia.DBExecute(idEntidad, CShort(0), CShort(0), CShort(0), 3)
                            'If tsede.Rows.Count > 0 Then
                            DataGridFillCombo(comboBoxSede, tsede, "id_Sede", "Nombre_Sede")
                            'End If
                        Catch
                        End Try
                    Case 7
                        Dim idSede As Short = 0
                        Try
                            idSede = CShort(comboBox.SelectedValue)
                            Dim tboveda = dbmCore.SchemaCustody.PA_Get_Caja_Pendiente_Custodia.DBExecute(CShort(comboBoxEntidad.Value), CShort(0), idSede, CShort(0), 4)
                            'If tboveda.Rows.Count > 0 Then
                            DataGridFillCombo(comboBoxBoveda, tboveda, "id_Boveda", "Nombre_Boveda")
                            'End If
                        Catch
                        End Try
                End Select
            Catch ex As Exception
                dbmCore.Connection_Close()
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CustodiaCajaDesktopData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CustodiaCajaDesktopData.CellContentClick
            Try
                If e.ColumnIndex = Me.CustodiaCajaDesktopData.Columns.Item("Seleccionar").Index Then
                    Dim RowData As DataGridViewRow = CustodiaCajaDesktopData.Rows(e.RowIndex)
                    Dim chkCell As DataGridViewCheckBoxCell = CType(RowData.Cells(0), DataGridViewCheckBoxCell)
                    Dim textoBoxCodigo As DataGridViewTextBoxCell = CType(RowData.Cells(5), DataGridViewTextBoxCell)
                    Dim comboBoxEntidad As DataGridViewComboBoxCell = CType(RowData.Cells(6), DataGridViewComboBoxCell)
                    Dim comboBoxSede As DataGridViewComboBoxCell = CType(RowData.Cells(7), DataGridViewComboBoxCell)
                    Dim comboBoxBoveda As DataGridViewComboBoxCell = CType(RowData.Cells(8), DataGridViewComboBoxCell)
                    textoBoxCodigo.ReadOnly = False
                    comboBoxEntidad.ReadOnly = False
                    comboBoxSede.ReadOnly = False
                    comboBoxBoveda.ReadOnly = False
                    CustodiaCajaDesktopData.EditMode = DataGridViewEditMode.EditOnEnter
                    CustodiaCajaDesktopData.BeginEdit(False)
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub CustodiaCajaDesktopData_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles CustodiaCajaDesktopData.DataError
            Try
                Dim oDGVC As DataGridViewColumn = Me.CustodiaCajaDesktopData.Columns(e.ColumnIndex)
                Dim sTextoMensaje As String
                sTextoMensaje = "Error en la columna: " & oDGVC.DataPropertyName & vbLf + e.Exception.Message
                'MessageBox.Show(sTextoMensaje, "Error de edición", MessageBoxButtons.OK)
                e.Cancel = False
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region " Metodos "
        Public Sub New()
            InitializeComponent()
        End Sub

        Public Function VerificaSeleccion(ControlCombo As ComboBox) As Boolean
            Dim _retorno As Boolean = False
            Try
                If (ControlCombo.SelectedIndex <> 0) Then
                    _retorno = True
                End If
            Catch
                _retorno = False
            End Try
            Return _retorno
        End Function

        Public Function VerificaSeleccion(ControlCombo As DataGridViewComboBoxCell) As Boolean
            Dim _retorno As Boolean = False
            Try
                If (ControlCombo.FormattedValue.ToString() <> "") Then
                    _retorno = True
                End If
            Catch
                _retorno = False
            End Try
            Return _retorno
        End Function

        Private Sub CargaFiltros()
            Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                EntidadDesktopComboBox.ValueMember = "id_entidad"
                EntidadDesktopComboBox.DisplayMember = "nombre_entidad"

                Dim entidadProyectoEnumList As New CTA_EntidadEnumList
                entidadProyectoEnumList.Add(CTA_EntidadEnum.Nombre_Entidad, True)

                Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBGet(0, entidadProyectoEnumList)

                Utilities.LlenarCombo(EntidadDesktopComboBox, EntidadDataTable, CTA_EntidadEnum.id_Entidad.ColumnName, CTA_EntidadEnum.Nombre_Entidad.ColumnName, True, "-1", "Todas")

                CargaFiltroProyecto(Program.Sesion.Usuario.id, CShort(EntidadDesktopComboBox.SelectedValue))

                EntidadDesktopComboBox.Focus()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            Finally
                dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargaFiltroProyecto(ByVal fk_usuario As Integer, ByVal fk_Entidad As Short)
            Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim proyectoDataTable = dbmImaging.SchemaSecurity.CTA_Entidad_Proyecto_Custodia.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))
                If proyectoDataTable.Rows.Count > 0 Then
                    Utilities.LlenarCombo(ProyectoDesktopComboBox, proyectoDataTable, CTA_Entidad_Proyecto_CustodiaEnum.id_Proyecto.ColumnName, CTA_Entidad_Proyecto_CustodiaEnum.Nombre_Proyecto.ColumnName, True, "-1", "Todos")
                Else
                    With ProyectoDesktopComboBox
                        .DataSource = Nothing
                    End With
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltroProyecto", ex)
            Finally
                dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargaGrilla()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim tentidad = dbmCore.SchemaCustody.PA_Get_Caja_Pendiente_Custodia.DBExecute(CShort(0), CShort(0), 0, CShort(0), 5)
                If tentidad.Rows.Count > 0 Then
                    Utilities.DataGridFillCombo(CustodiaCajaDesktopData, "Entidad", tentidad, "id_Entidad", "Nombre_Entidad")
                End If

                Dim dDatos = dbmCore.SchemaCustody.PA_Get_Caja_Pendiente_Custodia.DBExecute(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue), 0, CShort(0), 1)

                If dDatos.Rows.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("No hay cajas pendientes por custodiar", "Consulta cajas pendientes por custodiar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                CustodiaCajaDesktopData.AutoGenerateColumns = False
                CustodiaCajaDesktopData.DataSource = dDatos
                CustodiaCajaDesktopData.ClearSelection()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Consulta Pendientes Por Custodiar", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub Guardar()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim blValidaError As Boolean = False
            Dim blValida As Boolean = True
            Dim MensajeError As String = ""
            Dim validaPosicion As String = ""
            Dim Respuesta As DialogResult
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()
                For Each RowData As DataGridViewRow In CustodiaCajaDesktopData.Rows
                    If Not IsDBNull(RowData.Cells("Seleccionar").Value) Then
                        If CBool(RowData.Cells("Seleccionar").Value) Then
                            Dim textoBoxCodigo As DataGridViewTextBoxCell = CType(RowData.Cells(5), DataGridViewTextBoxCell)
                            Dim comboBoxEntidad As DataGridViewComboBoxCell = CType(RowData.Cells(6), DataGridViewComboBoxCell)
                            Dim comboBoxSede As DataGridViewComboBoxCell = CType(RowData.Cells(7), DataGridViewComboBoxCell)
                            Dim comboBoxBoveda As DataGridViewComboBoxCell = CType(RowData.Cells(8), DataGridViewComboBoxCell)
                            Dim CodigoCaja As String = RowData.Cells("Codigo_Caja").Value.ToString()
                            Try
                                validaPosicion = textoBoxCodigo.Value.ToString()
                            Catch
                                validaPosicion = ""
                            End Try
                            If validaPosicion = "" Then
                                'DesktopMessageBoxControl.DesktopMessageShow("¡Debe digitar la posición!", "Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                                'Return
                                MensajeError = MensajeError & " Para el codigo de caja " & CodigoCaja & ", debe digitar la posición." & vbCrLf
                                blValidaError = True
                            ElseIf Not VerificaSeleccion(comboBoxEntidad) Or Not VerificaSeleccion(comboBoxSede) Or Not VerificaSeleccion(comboBoxBoveda) Then
                                'DesktopMessageBoxControl.DesktopMessageShow("¡Debe seleccionar una Entidad y/o Sede y/o Boveda!", "Selección", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
                                'Return
                                MensajeError = MensajeError & " Para el codigo de caja " & CodigoCaja & ", debe seleccionar la Entidad, la Sede y la Boveda." & vbCrLf
                                blValidaError = True
                            Else
                                Dim CodigoPosicion As String = textoBoxCodigo.Value.ToString()
                                Dim fk_Sede As Integer = CInt(comboBoxSede.Value.ToString())
                                Dim fk_Boveda As String = comboBoxBoveda.Value.ToString()
                                Dim dTabla As DataTable = dbmCore.SchemaCustody.PA_Set_Custodia_Caja.DBExecute(CodigoCaja, CodigoPosicion, CShort(fk_Sede), CShort(fk_Boveda))
                                Dim dDatos As DataSet = New DataSet()
                                dDatos.Merge(dTabla)
                                Try
                                    MensajeError = MensajeError & " Para el codigo de caja " & CodigoCaja & ", " & dDatos.Tables(0).Rows(0).Item("Error").ToString() & vbCrLf
                                    If MensajeError.Length > 0 Then
                                        blValidaError = True
                                    End If
                                Catch
                                End Try
                            End If
                            blValida = False
                        End If
                    End If
                Next
                dbmCore.Transaction_Commit()
                'CargaGrilla()
                If blValida Then
                    DesktopMessageBoxControl.DesktopMessageShow("¡Debe Seleccionar Una Caja Para Custodiar!", "Custodiar Caja", DesktopMessageBoxControl.IconEnum.WarningIcon, True, False)
                ElseIf blValidaError Then
                    'Limpiar()
                    Respuesta = MessageBox.Show("Debe verificar las siguientes inconsistencias: " & vbCrLf & _
                                                MensajeError & vbCrLf & "Las demas Cajas Han Sido Custodiadas Con Exito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Else
                    Limpiar()
                    DesktopMessageBoxControl.DesktopMessageShow("¡Las Cajas Han Sido Custodiadas Con Exito!", "Custodiar Caja", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                End If
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            End Try
            dbmCore.Connection_Close()
        End Sub

        Public Sub DataGridFillCombo(ByRef cmbCombo As DataGridViewComboBoxCell, ByVal nTable As DataTable, ByVal value As String, ByVal text As String)
            Try
                With cmbCombo
                    .DataSource = nTable
                    .DisplayMember = text
                    .ValueMember = value
                End With
            Catch ex As Exception
            End Try
        End Sub

        Public Sub Limpiar()
            Try
                With EntidadDesktopComboBox
                    .SelectedIndex = 0
                End With
                With ProyectoDesktopComboBox
                    .DataSource = Nothing
                End With
                With CustodiaCajaDesktopData
                    .DataSource = Nothing
                    .ClearSelection()
                End With
            Catch ex As Exception
            End Try
        End Sub
#End Region
    End Class
End Namespace