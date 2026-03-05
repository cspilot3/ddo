Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBCore
Imports SLYG.Tools

Namespace Forms.Arqueo

    Public Class FormArqueo

#Region " Declaraciones "

        Private tblBase As New SchemaCustody.TBL_ArqueoDataTable
        Private tblArqueo As New SchemaCustody.TBL_ArqueoDataTable
        Private tblArqueoParametro As New SchemaCustody.TBL_Arqueo_ParametroDataTable
        Private tblArqueoParametroESQ As New SchemaCustody.CTA_Arqueo_Parametro_ESQDataTable
        Private idUltimo As Integer

#End Region

#Region " Eventos "

        ''' <summary>
        ''' Eventos Globales
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FormArqueo_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load            
            idUltimo = 0
            InformacionLabel.Text = "Listo"
            Activar_Controles(False, False)
            InformacionGeneralLabel.Text = "[Entidad: " & Program.Sesion.Entidad.Nombre & "] | [Usuario: " & Program.Sesion.Usuario.Apellidos.TrimEnd(" "c) & " " & Program.Sesion.Usuario.Nombres.TrimEnd(" "c) & "]"

        End Sub
        Private Sub NuevoToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles NuevoToolStripButton.Click
            Action_New()
        End Sub
        Private Sub BuscarToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarToolStripButton.Click
            Action_Search()
        End Sub
        Private Sub GuardarToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarToolStripButton.Click
            Action_Save()
        End Sub
        Private Sub ReporteToolStripButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReporteToolStripButton.Click
            Ver_Reporte()
        End Sub

        ''' <summary>
        ''' Eventos en la Primera Pestaña (Filtro)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub FechaCreacionCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaCreacionCheckBox.CheckedChanged
            FechaCreacionDesdeDateTimePicker.Enabled = FechaCreacionCheckBox.Checked
            FechaCreacionHastaDateTimePicker.Enabled = FechaCreacionCheckBox.Checked
        End Sub
        Private Sub FechaCierreCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaCierreCheckBox.CheckedChanged
            FechaCierreDesdeDateTimePicker.Enabled = FechaCierreCheckBox.Checked
            FechaCierreHastaDateTimePicker.Enabled = FechaCierreCheckBox.Checked
        End Sub

        ''' <summary>
        ''' Eventos en la Segunda Pestaña (Detalle)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub gvBase_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles gvBase.SelectionChanged
            If Not InformacionLabel.Text.Contains("Realizando") Then
                If gvBase.RowCount > 0 Then
                    If gvBase.CurrentRow.Index >= 0 And gvBase.Rows.Count > 0 Then
                        EditarRegistro()
                    End If
                End If
            End If

        End Sub
        Private Sub gvBase_RowHeaderMouseDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellMouseEventArgs) Handles gvBase.RowHeaderMouseDoubleClick
            If Not InformacionLabel.Text.Contains("Realizando") Then
                If gvBase.RowCount > 0 Then
                    If gvBase.CurrentRow.Index >= 0 And gvBase.Rows.Count > 0 Then
                        EditarRegistro()
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Eventos en la tercera Pestaña (Edicion)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub AddParameterButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddParameterButton.Click
            Dim ParametroForm As New FormParametro()
            ParametroForm.idArqueoParametro = idUltimo + 1
            If ParametroForm.ShowDialog() = DialogResult.OK Then
                tblArqueoParametroESQ.AddCTA_Arqueo_Parametro_ESQRow(ParametroForm.rowArqueoParametroESQ.fk_Entidad, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Arqueo, _
                                                                     ParametroForm.rowArqueoParametroESQ.id_Arqueo_Parametro, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Sede, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Boveda, _
                                                                     ParametroForm.rowArqueoParametroESQ.Nombre_Boveda, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Seccion, _
                                                                     ParametroForm.rowArqueoParametroESQ.Nombre_Boveda_Seccion, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Estante, _
                                                                     ParametroForm.rowArqueoParametroESQ.Codigo_Boveda_Estante, _
                                                                     ParametroForm.rowArqueoParametroESQ.Fila, _
                                                                     ParametroForm.rowArqueoParametroESQ.Columna, _
                                                                     ParametroForm.rowArqueoParametroESQ.Profundidad, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Entidad_Cliente, _
                                                                     ParametroForm.rowArqueoParametroESQ.Nombre_Entidad, _
                                                                     ParametroForm.rowArqueoParametroESQ.fk_Proyecto, _
                                                                     ParametroForm.rowArqueoParametroESQ.Nombre_Proyecto)
                idUltimo += 1
                Visualizar_Resultados_Parametros()
            End If

        End Sub
        Private Sub RemoveParameterButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RemoveParameterButton.Click
            For Each selectedRow As DataGridViewRow In ParametrosDataGridView.SelectedRows
                Try
                    Dim rowPrepare As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
                    Dim rowToRemove As SchemaCustody.CTA_Arqueo_Parametro_ESQRow
                    rowToRemove = CType(rowPrepare.Row, SchemaCustody.CTA_Arqueo_Parametro_ESQRow)
                    tblArqueoParametroESQ.RemoveCTA_Arqueo_Parametro_ESQRow(rowToRemove)
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Eliminar", ex)
                End Try
            Next
        End Sub

#End Region

#Region " Metodos "

        Private Sub Action_Search()
            'gvBase.SelectedIndex = -1
            InformacionLabel.Text = "Realizando busqueda ..."

            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Dim idArqueo As SlygNullable(Of Integer) = DBNull.Value
            If Not CodigoTextBox.Text Is Nothing Then
                If IsNumeric(CodigoTextBox.Text) Then
                    idArqueo = CInt(CodigoTextBox.Text)
                End If
            End If

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim fechas As Integer = 0

                If FechaCreacionCheckBox.Checked Then
                    If FechaCierreCheckBox.Checked Then
                        fechas = 3
                    Else
                        fechas = 1
                    End If
                Else
                    If FechaCierreCheckBox.Checked Then
                        fechas = 2
                    End If
                End If

                Dim FechaInicialCreacion As New DateTime(FechaCreacionDesdeDateTimePicker.Value.Year, FechaCreacionDesdeDateTimePicker.Value.Month, FechaCreacionDesdeDateTimePicker.Value.Day)
                Dim FechaFinalCreacion As New DateTime(FechaCreacionHastaDateTimePicker.Value.Year, FechaCreacionHastaDateTimePicker.Value.Month, FechaCreacionHastaDateTimePicker.Value.Day, 23, 59, 59)
                Dim FechaInicialCierre As New DateTime(FechaCierreDesdeDateTimePicker.Value.Year, FechaCierreDesdeDateTimePicker.Value.Month, FechaCierreDesdeDateTimePicker.Value.Day)
                Dim FechaFinalCierre As New DateTime(FechaCierreHastaDateTimePicker.Value.Year, FechaCierreHastaDateTimePicker.Value.Month, FechaCierreHastaDateTimePicker.Value.Day, 23, 59, 59)


                tblBase.Clear()
                Select Case fechas
                    Case 3
                        tblBase = dbmCore.SchemaCustody.PA_Obtiene_Arqueos.DBExecute(Program.Sesion.Entidad.Id, idArqueo, ActivosCheckBox.Checked, InactivosCheckBox.Checked, FechaInicialCreacion, FechaFinalCreacion, FechaInicialCierre, FechaInicialCierre)
                    Case 2
                        tblBase = dbmCore.SchemaCustody.PA_Obtiene_Arqueos_Cierre.DBExecute(Program.Sesion.Entidad.Id, idArqueo, ActivosCheckBox.Checked, InactivosCheckBox.Checked, FechaInicialCierre, FechaFinalCierre)
                    Case 1
                        tblBase = dbmCore.SchemaCustody.PA_Obtiene_Arqueos_Creacion.DBExecute(Program.Sesion.Entidad.Id, idArqueo, ActivosCheckBox.Checked, InactivosCheckBox.Checked, FechaInicialCreacion, FechaFinalCreacion)
                    Case 0
                        tblBase = dbmCore.SchemaCustody.PA_Obtiene_Arqueos_Sin_Fecha.DBExecute(Program.Sesion.Entidad.Id, idArqueo, ActivosCheckBox.Checked, InactivosCheckBox.Checked)
                    Case Else
                        Throw New Exception("Error Inesperado en el seleccionador de fechas")
                End Select

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Busqueda", ex)
            Finally
                dbmCore.Connection_Close()
            End Try

            'ActivarOpciones(False, False)
            Visualizar_Resultados()
            ArqueoTabControl.SelectedTab = TabPage2
            InformacionLabel.Text = "Busqueda Completa..."
            Activar_Controles(False, False)
        End Sub
        Protected Sub Action_New()
            Limpiar_Campos()
            ArqueoTabControl.SelectedTab = TabPage3
            Activar_Controles(True, False)
            InformacionLabel.Text = "Nuevo Registro..."
        End Sub
        Private Sub Action_Save()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                InformacionLabel.Text = "Guardando Arqueo ..."
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                'Dim Program.Sesion.Entidad.Id = CInt(EditEntidadComboBox.SelectedValue)
                Dim Codigo As Short

                Codigo = dbmCore.SchemaCustody.TBL_Arqueo.DBNextId_for_id_Arqueo(Program.Sesion.Entidad.id)
                EditCodigoTextBox.Text = Codigo.ToString()

                Dim Modo As Byte

                Select Case EditModoComboBox.SelectedItem.ToString()
                    Case "Caja"
                        Modo = 1
                    Case "Carpeta"
                        Modo = 2
                    Case "Documento"
                        Modo = 3
                    Case Else
                        Modo = 1
                End Select

                Dim usuario = Program.Sesion.Usuario.id
                Dim activo = EditActivoCheckBox.Checked
                Dim Descripcion = EditDescripcionTextBox

                Dim rowArqueo As New SchemaCustody.TBL_ArqueoType

                With rowArqueo
                    .fk_Entidad = Program.Sesion.Entidad.id
                    .id_Arqueo = Codigo
                    .fk_Arqueo_Modo = Modo
                    .Fecha_Log = SlygNullable.SysDate
                    .fk_Usuario_Log = CShort(usuario)
                    .Fecha_Creacion_Arqueo = SlygNullable.SysDate
                    .Activo_Arqueo = activo
                    .Decripcion_Arqueo = Descripcion.Text
                End With

                dbmCore.SchemaCustody.TBL_Arqueo.DBInsert(rowArqueo)

                tblArqueoParametro.Clear()
                Dim idParametro As Integer = 1
                For Each rowParametroESQ In tblArqueoParametroESQ
                    Dim rowParametro = tblArqueoParametro.NewTBL_Arqueo_ParametroRow
                    With rowParametro
                        .fk_Entidad = Program.Sesion.Entidad.id
                        .fk_Sede = rowParametroESQ.fk_Sede
                        .fk_Arqueo = Codigo
                        .id_Arqueo_Parametro = idParametro
                        .fk_Boveda = rowParametroESQ.fk_Boveda
                        .fk_Seccion = rowParametroESQ.fk_Seccion
                        .fk_Estante = rowParametroESQ.fk_Estante
                        .Fila = rowParametroESQ.Fila
                        .Columna = rowParametroESQ.Columna
                        .Profundidad = rowParametroESQ.Profundidad
                        .fk_Entidad_Cliente = rowParametroESQ.fk_Entidad_Cliente
                        .fk_Proyecto = rowParametroESQ.fk_Proyecto
                    End With
                    dbmCore.SchemaCustody.TBL_Arqueo_Parametro.DBInsert(rowParametro)

                    InformacionLabel.Text = "Generando Valores de Arqueo ..."

                    Dim tblBodegaPosicion As New SchemaCustody.CTA_Bodega_PosicionDataTable
                    'Dim tblArqueoPosicion As New SchemaCustody.TBL_Arqueo_PosicionDataTable

                    Dim Entidad As SlygNullable(Of Short)
                    Dim Sede As SlygNullable(Of Short)
                    Dim Boveda As SlygNullable(Of Short)
                    Dim Seccion As SlygNullable(Of Short)
                    Dim Estante As SlygNullable(Of Short)
                    Dim Fila As SlygNullable(Of Short)
                    Dim Columna As SlygNullable(Of Short)
                    Dim Profundidad As SlygNullable(Of Short)
                    Dim entidadCliente As SlygNullable(Of Short)
                    Dim proyecto As SlygNullable(Of Short)

                    Entidad = Program.Sesion.Entidad.id
                    If rowParametroESQ.fk_Sede = -1 Then
                        Sede = Nothing
                    Else
                        Sede = CShort(rowParametroESQ.fk_Sede)
                    End If

                    If rowParametroESQ.fk_Boveda = -1 Or rowParametroESQ.Nombre_Boveda = "" Then
                        Boveda = Nothing
                    Else
                        Boveda = CShort(rowParametroESQ.fk_Boveda)
                    End If

                    If rowParametroESQ.fk_Seccion = -1 Or rowParametroESQ.Nombre_Boveda_Seccion = "" Then
                        Seccion = Nothing
                    Else
                        Seccion = CShort(rowParametroESQ.fk_Seccion)
                    End If

                    If rowParametroESQ.fk_Estante = -1 Or rowParametroESQ.Codigo_Boveda_Estante = "" Then
                        Estante = Nothing
                    Else
                        Estante = CShort(rowParametroESQ.fk_Estante)
                    End If

                    If rowParametroESQ.Fila = -1 Or rowParametroESQ.Fila = 0 Then
                        Fila = Nothing
                    Else
                        Fila = CShort(rowParametroESQ.Fila)
                    End If

                    If rowParametroESQ.Columna = -1 Or rowParametroESQ.Columna = 0 Then
                        Columna = Nothing
                    Else
                        Columna = CShort(rowParametroESQ.Columna)
                    End If

                    If rowParametroESQ.Profundidad = -1 Or rowParametroESQ.Profundidad = 0 Then
                        Profundidad = Nothing
                    Else
                        Profundidad = CShort(rowParametroESQ.Profundidad)
                    End If

                    If rowParametroESQ.fk_Entidad_Cliente = -1 Or rowParametroESQ.Nombre_Entidad = "" Then
                        entidadCliente = Nothing
                    Else
                        entidadCliente = CShort(rowParametroESQ.fk_Entidad_Cliente)
                    End If

                    If rowParametroESQ.fk_Proyecto = -1 Or rowParametroESQ.Nombre_Proyecto = "" Then
                        proyecto = Nothing
                    Else
                        proyecto = CShort(rowParametroESQ.fk_Proyecto)
                    End If

                    dbmCore.SchemaCustody.CTA_Bodega_Posicion.DBFillByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccionfk_Boveda_EstanteFila_Boveda_PosicionColumna_Boveda_PosicionProfundidad_Boveda_Posicionfk_Entidad_Clientefk_Proyecto_Cliente( _
                        tblBodegaPosicion, _
                        Entidad, _
                        Sede, _
                        Boveda, _
                        Seccion, _
                        Estante, _
                        Fila, _
                        Columna, _
                        Profundidad, _
                        entidadCliente, _
                        proyecto)

                    For Each rowPosicion As SchemaCustody.CTA_Bodega_PosicionRow In tblBodegaPosicion

                        Dim Existe As Boolean
                        Existe = dbmCore.SchemaCustody.PA_Especial_TBL_Arqueo_Posicion_Existe.DBExecute(Entidad, Codigo, rowPosicion.fk_Caja, rowPosicion.id_Boveda_Posicion)

                        If Not Existe Then
                            Dim rowArqueoPosicion As New SchemaCustody.TBL_Arqueo_PosicionType

                            With rowArqueoPosicion
                                .fk_Entidad = Entidad
                                .fk_Arqueo = Codigo
                                .fk_Caja = rowPosicion.fk_Caja
                                .fk_Boveda_Posicion = rowPosicion.id_Boveda_Posicion
                                .Arqueado = False
                                .Bloqueado = False
                                .fk_Usuario_Log = Program.Sesion.Usuario.id
                                .Fecha_Log = SlygNullable.SysDate
                            End With

                            dbmCore.SchemaCustody.TBL_Arqueo_Posicion.DBInsert(rowArqueoPosicion)
                        End If


                    Next
                    InformacionLabel.Text = "Valores de Arqueo Generados"

                    idParametro += 1
                Next


                Select Case Modo
                    Case 2
                        dbmCore.SchemaCustody.PA_Arqueo_Insert_Detalle.DBExecute(Program.Sesion.Entidad.id, Codigo, Program.Sesion.Usuario.id, False)
                    Case 3
                        dbmCore.SchemaCustody.PA_Arqueo_Insert_Detalle.DBExecute(Program.Sesion.Entidad.id, Codigo, Program.Sesion.Usuario.id, True)

                End Select

                dbmCore.Transaction_Commit()

                MessageBox.Show("Los datos se almacenaron exitosamente", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

                ArqueoTabControl.SelectedTab = TabPage2

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("Arqueo", ex)
            Finally
                dbmCore.Connection_Close()
            End Try

            InformacionLabel.Text = "Arqueo Guardado ..."
            Action_Search()
            Activar_Controles(False, True)
        End Sub

        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblBase
        End Sub
        Private Sub Ver_Reporte()
            Dim rowBase As SchemaCustody.TBL_ArqueoRow
            rowBase = CType(tblBase.Rows(gvBase.CurrentRow.Index), SchemaCustody.TBL_ArqueoRow)

            Dim reporteForm As New FormReporteArqueo()
            With reporteForm
                .Entidad = rowBase.fk_Entidad
                .Arqueo = rowBase.id_Arqueo
            End With
            reporteForm.Show()
        End Sub

        Private Sub Limpiar_Campos()
            EditCodigoTextBox.Text = String.Empty
            EditDescripcionTextBox.Text = String.Empty
            tblArqueoParametroESQ.Clear()
        End Sub
        Private Sub EditarRegistro()
            Dim rowBase As SchemaCustody.TBL_ArqueoRow

            rowBase = CType(tblBase.Rows(gvBase.CurrentRow.Index), SchemaCustody.TBL_ArqueoRow)

            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                tblArqueo.Clear()

                dbmCore.SchemaCustody.TBL_Arqueo.DBFill(tblArqueo, rowBase.fk_Entidad, rowBase.id_Arqueo)

                Dim rowArqueo As SchemaCustody.TBL_ArqueoRow
                rowArqueo = tblArqueo.FindByfk_Entidadid_Arqueo(rowBase.fk_Entidad, rowBase.id_Arqueo)

                EditCodigoTextBox.Text = rowArqueo.id_Arqueo.ToString()
                EditModoComboBox.SelectedIndex = rowArqueo.fk_Arqueo_Modo - 1
                EditDescripcionTextBox.Text = rowArqueo.Decripcion_Arqueo
                EditActivoCheckBox.Checked = rowArqueo.Activo_Arqueo

                tblArqueoParametroESQ.Clear()
                dbmCore.SchemaCustody.CTA_Arqueo_Parametro_ESQ.DBFillByfk_Entidadfk_Arqueo(tblArqueoParametroESQ, rowBase.fk_Entidad, rowBase.id_Arqueo)
                Visualizar_Resultados_Parametros()
            Catch ex As Exception
            Finally
                dbmCore.Connection_Close()
            End Try

            ArqueoTabControl.SelectedTab = TabPage3

            InformacionLabel.Text = "Arqueo en Edición ..."
            Activar_Controles(False, True)
        End Sub
        Private Sub Activar_Controles(ByVal Estado As Boolean, ByVal Estado_Reporte As Boolean)

            EditActivoCheckBox.Enabled = Estado
            EditModoComboBox.Enabled = Estado
            EditDescripcionTextBox.Enabled = Estado

            AddParameterButton.Enabled = Estado
            RemoveParameterButton.Enabled = Estado
            GuardarToolStripButton.Enabled = Estado
            ReporteToolStripButton.Enabled = Estado_Reporte
        End Sub
        Private Sub Visualizar_Resultados_Parametros()
            Try
                ParametrosDataGridView.DataSource = Nothing
                ParametrosDataGridView.DataSource = tblArqueoParametroESQ
                ParametrosDataGridView.Refresh()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Visualizar Parametros", ex)
            End Try
        End Sub

        Public Shared Function getClientIPAddress() As String
            Dim LocalIP As String = ""

            For Posicion As Integer = 0 To Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1
                Dim TempIP As String = Dns.GetHostEntry(Dns.GetHostName()).AddressList(Posicion).ToString()

                If IsValidIP(TempIP) Then
                    LocalIP = TempIP
                    Exit For
                End If
            Next

            Return LocalIP
        End Function
        Private Shared Function IsValidIP(ByVal nIPAddress As String) As Boolean
            'create our match pattern
            Const pattern As String = "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"

            'create our Regular Expression object
            Dim check As New Regex(pattern)

            'boolean variable to hold the status
            Dim valid As Boolean

            'check to make sure an ip address was provided
            If (nIPAddress = "") Then
                'no address provided so return false
                valid = False
            Else
                'address provided so use the IsMatch Method
                'of the Regular Expression object
                valid = check.IsMatch(nIPAddress, 0)
            End If

            'return the results
            Return valid
        End Function
#End Region

    End Class

End Namespace