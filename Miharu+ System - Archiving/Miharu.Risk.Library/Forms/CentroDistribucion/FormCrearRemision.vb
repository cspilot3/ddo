Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.CentroDistribucion

    Public Class FormCrearRemision
        Inherits FormBase

#Region " Declaraciones "
        Private _Id_Remision As Long = 0
        Dim Bovedas As DBArchiving.Schemadbo.CTA_CajaDataTable
#End Region

#Region " Constructor "

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

        End Sub

        Public Sub New(ByVal idRemision As Long)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Id_Remision = idRemision
        End Sub

#End Region

#Region " Eventos "
        Private Sub FormCrearRemision_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarDatos()
        End Sub

        Private Sub ExternoDesktopCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExternoDesktopCheckBox.CheckedChanged
            OcultarElementos(ExternoDesktopCheckBox.Checked)
        End Sub

        Private Sub IngresarCajaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles IngresarCajaButton.Click
            AgregarCaja()
        End Sub

        Private Sub CBarrasDesktopTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles CBarrasDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                e.Handled = True
                'CBarrasDesktopTextBox.Text = DesktopTextBoxControl.CBarrasFlash(CBarrasDesktopTextBox.Text)
                AgregarCaja()
            End If
        End Sub

        Private Sub CrearRemisionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CrearRemisionButton.Click
            If ValidarCrearRemision() Then
                GuardarRemision()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Se deben diligenciar todos los datos para continuar.", "Datos inválidos", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub EliminarRemisionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EliminarRemisionButton.Click
            If ValidarEliminarRemision(_Id_Remision) Then
                EliminarRemision(_Id_Remision)
                DesktopMessageBoxControl.DesktopMessageShow("Remisión [" & _Id_Remision.ToString() & "] eliminada del sistema.", "Eliminación de remisión", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.Close()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No se puede eliminar la remisión." & vbCrLf & vbCrLf & "Tiene items asociados o ya fué enviada.", "Remisión con Items", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub CajasDataGridView_UserDeletingRow(ByVal sender As System.Object, ByVal e As DataGridViewRowCancelEventArgs) Handles CajasDataGridView.UserDeletingRow
            If DesktopMessageBoxControl.DesktopMessageShow("¿Desea eliminar la caja [" & e.Row.Cells(2).Value.ToString() & "] seleccionada de la remisión?", "Eliminación de Caja", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = DialogResult.OK Then
                EliminarCajaRemision(_Id_Remision, CInt(e.Row.Cells(0).Value))
            Else
                e.Cancel = True
            End If

        End Sub
#End Region

#Region " Metodos "
        Private Sub CargarDatos()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            Try
                If _Id_Remision = 0 Then
                    Bovedas = dbmArchiving.Schemadbo.CTA_Caja.DBFindByfk_Sedefk_Entidadfk_EstadoUsa_Custodia_Externa(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, DBCore.EstadoEnum.Centro_Distribucion, False)

                    BovedaComboBox.DataSource = Utilities.clonarDataTable(Bovedas).DefaultView.ToTable(True, Bovedas.fk_Boveda_CustodiaColumn.ColumnName, Bovedas.Nombre_BovedaColumn.ColumnName)
                    BovedaComboBox.ValueMember = Bovedas.fk_Boveda_CustodiaColumn.ColumnName
                    BovedaComboBox.DisplayMember = Bovedas.Nombre_BovedaColumn.ColumnName
                Else
                    Dim dtBovedas = dbmCore.Schemadbo.CTA_Remisiones.DBFindById_Remision_Caja(_Id_Remision)

                    If dtBovedas(0).IsNombre_SedeNull() OrElse dtBovedas(0).Nombre_Sede = "" Then
                        DireccionEnvioDesktopTextBox.Text = dtBovedas(0).Direccion
                        ResponsableDesktopTextBox.Text = dtBovedas(0).Responsable
                        OcultarElementos(True)
                    Else
                        BovedaComboBox.DataSource = dtBovedas
                        BovedaComboBox.ValueMember = dtBovedas.fk_BovedaColumn.ColumnName
                        BovedaComboBox.DisplayMember = dtBovedas.Nombre_BovedaColumn.ColumnName
                        OcultarElementos(False)
                    End If

                    CrearRemisionButton.Enabled = False
                    EliminarRemisionButton.Enabled = True
                    RemisionGroupBox.Enabled = False
                    CajasRemicionGroupBox.Enabled = True
                    CargaItemsRemision(_Id_Remision)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarDatos", ex)
            End Try

            dbmArchiving.Connection_Close()
            dbmCore.Connection_Close()
        End Sub

        Private Sub OcultarElementos(ByVal bExterno As Boolean)
            Try
                If bExterno Then
                    ExternoPanel.Visible = True
                    ExternoPanel.Enabled = True
                    BovedaComboBox.Visible = False
                Else
                    ExternoPanel.Visible = False
                    ExternoPanel.Enabled = False
                    BovedaComboBox.Visible = True
                End If
                ExternoDesktopCheckBox.Checked = bExterno
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("OcultarElementos", ex)
            End Try
        End Sub

        Private Sub AgregarCaja()
            'Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            'Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            'Try
            '    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            '    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            '    If CBarrasDesktopTextBox.Text <> "" Then
            '        Dim dtCajas = dbmArchiving.Schemadbo.PA_Cajas_Validas_Remision.DBExecute(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CBarrasDesktopTextBox.Text, DBCore.EstadoEnum.Centro_Distribucion, ExternoDesktopCheckBox.Checked, CShort(BovedaComboBox.SelectedValue))

            '        If dtCajas.Count > 0 Then
            '            '    If Not CodigoAgregado(_Id_Remision, dtCajas(0).id_Caja) Then

            '            '        dbmCore.Transaction_Begin()

            '            '        'Inserta la caja en la remisión.
            '            '        Dim tRemisionItem As New DBCore.SchemaProcess.TBL_Remision_Caja_ItemType
            '            '        tRemisionItem.fk_Remision_Caja = _Id_Remision
            '            '        tRemisionItem.fk_Caja = dtCajas(0).id_Caja
            '            '        tRemisionItem.Cantidad_Folders = dbmCore.SchemaCustody.TBL_Folder.DBFindByfk_Caja(dtCajas(0).id_Caja).Count
            '            '        dbmCore.SchemaProcess.TBL_Remision_Caja_Item.DBInsert(tRemisionItem)

            '            '        'Actualiza estado de la caja
            '            '        Dim tCaja As New DBCore.SchemaCustody.TBL_CajaType
            '            '        tCaja.fk_Estado = DBCore.EstadoEnum.Asignado_a_Remision
            '            '        dbmCore.SchemaCustody.TBL_Caja.DBUpdate(tCaja, dtCajas(0).id_Caja)

            '            '        dbmCore.Transaction_Commit()

            '            '        'Actualiza los estados de folders y files
            '            '        Dim dItems = dbmArchiving.Schemadbo.CTA_Items_Caja.DBFindByIdCajaModulofk_Estado(dtCajas(0).id_Caja, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Centro_Distribucion)
            '            '        For Each item In dItems
            '            '            If item.fk_Estado = DBCore.EstadoEnum.Centro_Distribucion Then
            '            '                If item.CBarras.Substring(0, 3) = "000" Then 'Folder
            '            '                    Utilities.ActualizaEstadoFolder(dbmArchiving, dbmCore, item.CBarras, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Asignado_a_Remision, Program.Sesion.Usuario.id, item.fk_OT)
            '            '                Else 'File
            '            '                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, item.CBarras, item.fk_OT, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Asignado_a_Remision, Program.Sesion.Usuario.id, Nothing)
            '            '                End If
            '            '            End If
            '            '        Next

            '            '        CargarDatos()
            '            '    Else
            '            '        DesktopMessageBoxControl.DesktopMessageShow("Código de Barras de caja ya se encuentra asignado a esta remisión.", "Código de Barras Asignado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            '            '    End If
            '            '    UbicarFoco(CBarrasDesktopTextBox)
            '            'Else
            '            '    DesktopMessageBoxControl.DesktopMessageShow("Código de Barras de caja no existe, o no pertenece a este destino.", "Código de Barras no válido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            '            '    UbicarFoco(CBarrasDesktopTextBox)
            '            'End If
            '    Else
            '        UbicarFoco(CBarrasDesktopTextBox)
            '    End If
            'Catch ex As Exception
            '    dbmCore.Transaction_Rollback()
            '    DesktopMessageBoxControl.DesktopMessageShow("AgregarCaja", ex)
            'Finally
            '    dbmArchiving.Connection_Close()
            '    dbmCore.Connection_Close()
            'End Try
        End Sub

        Private Shared Sub UbicarFoco(ByRef campo As DesktopTextBoxControl)
            Try
                campo.SelectAll()
                campo.Focus()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("UbicarFoco", ex)
            End Try
        End Sub

        Private Sub GuardarRemision()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                Dim tRemision As New DBCore.SchemaProcess.TBL_Remision_CajaType

                Dim idRemision = dbmCore.SchemaProcess.TBL_Remision_Caja.DBNextId()
                tRemision.Id_Remision_Caja = idRemision
                If ExternoDesktopCheckBox.Checked Then
                    tRemision.fk_Entidad = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                    tRemision.Direccion_Envio = DireccionEnvioDesktopTextBox.Text
                    tRemision.Nombre_Responsable = ResponsableDesktopTextBox.Text
                Else
                    tRemision.fk_Entidad = Bovedas(BovedaComboBox.SelectedIndex).fk_Entidad_custodia
                    tRemision.fk_Sede = Bovedas(BovedaComboBox.SelectedIndex).fk_Sede_Custodia
                    tRemision.fk_Boveda = CShort(BovedaComboBox.SelectedValue)
                End If
                tRemision.fk_Estado = DBCore.EstadoEnum.Centro_Distribucion

                dbmCore.SchemaProcess.TBL_Remision_Caja.DBInsert(tRemision)
                dbmCore.Transaction_Commit()

                _Id_Remision = idRemision
                CargarDatos()
                UbicarFoco(CBarrasDesktopTextBox)
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargaItemsRemision(ByVal idRemision As Long)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim dtItemasRemision = dbmCore.Schemadbo.CTA_Items_Remision.DBFindByfk_Remision_Cajafk_Caja(idRemision, Nothing)
                CajasDataGridView.AutoGenerateColumns = False
                CajasDataGridView.DataSource = dtItemasRemision
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaItemsRemision", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub EliminarCajaRemision(ByVal idRemision As Long, ByVal idCaja As Integer)
            'Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            'Try
            '    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            '    dbmCore.Transaction_Begin()

            '    'Se elimina el item de la remisión
            '    dbmCore.SchemaProcess.TBL_Remision_Caja_Item.DBDelete(idRemision, idCaja)

            '    'Se devuelve el estado de la caja
            '    Dim tCaja As New DBCore.SchemaCustody.TBL_CajaType
            '    tCaja.fk_Estado = DBCore.EstadoEnum.Centro_Distribucion
            '    dbmCore.SchemaCustody.TBL_Caja.DBUpdate(tCaja, idCaja)

            '    dbmCore.Transaction_Commit()
            'Catch ex As Exception
            '    dbmCore.Transaction_Rollback()
            '    DesktopMessageBoxControl.DesktopMessageShow("EliminarCajaRemision", ex)
            'Finally
            '    dbmCore.Connection_Close()
            'End Try
        End Sub

        Private Sub EliminarRemision(ByVal IdRemision As Long)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()
                dbmCore.SchemaProcess.TBL_Remision_Caja.DBDelete(IdRemision)
                dbmCore.Transaction_Commit()
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("EliminarRemision", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function CodigoAgregado(ByVal remision As Long, ByVal caja As Integer) As Boolean
            'Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            'Dim bExiste As Boolean = False
            'Try
            '    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            '    Dim dtRemisionItems = dbmCore.SchemaProcess.TBL_Remision_Caja_Item.DBFindByfk_Remision_Cajafk_Caja(remision, caja)
            '    If dtRemisionItems.Count > 0 Then
            '        bExiste = True
            '    End If
            'Catch ex As Exception
            '    DesktopMessageBoxControl.DesktopMessageShow("CodigoAgregado", ex)
            'Finally
            '    dbmCore.Connection_Close()
            'End Try
            'Return bExiste
        End Function

        Private Function ValidarCrearRemision() As Boolean
            Dim bReturn As Boolean = True
            Try
                If ExternoDesktopCheckBox.Checked Then
                    If DireccionEnvioDesktopTextBox.Text = "" Or ResponsableDesktopTextBox.Text = "" Then
                        bReturn = False
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarCrearRemision", ex)
            End Try
            Return bReturn
        End Function

        Private Function ValidarEliminarRemision(ByVal idRemision As Long) As Boolean
            'Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            'Dim bReturn As Boolean = True
            'Try
            '    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            '    Dim dtEstado = dbmCore.SchemaProcess.TBL_Remision_Caja.DBGet(idRemision)
            '    If dtEstado.Count > 0 AndAlso dtEstado(0).fk_Estado <> DBCore.EstadoEnum.Centro_Distribucion Then
            '        bReturn = False
            '    End If

            '    Dim dtItems = dbmCore.SchemaProcess.TBL_Remision_Caja_Item.DBFindByfk_Remision_Cajafk_Caja(idRemision, Nothing)
            '    If dtItems.Count > 0 Then
            '        bReturn = False
            '    End If
            'Catch ex As Exception
            '    DesktopMessageBoxControl.DesktopMessageShow("ValidarEliminarRemision", ex)
            'Finally
            '    dbmCore.Connection_Close()
            'End Try
            'Return bReturn
        End Function
#End Region

    End Class

End Namespace