Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports SLYG.Tools

Namespace Forms.Recepcion

    Public Class FormRecibirRemision
        Inherits FormBase

#Region " Declaraciones "
        Private _IdRemision As Long
        Private _countCajasRemision As Integer = 0
#End Region

#Region " Constructores "
        Public Sub New(ByVal idRemision As Long)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _IdRemision = idRemision
        End Sub
#End Region

#Region " Eventos "
        Private Sub FormRecibirRemision_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCajas()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub CodigoCajaDesktopTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles CodigoCajaDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                e.Handled = True
                'CodigoCajaDesktopTextBox.Text = DesktopTextBoxControl.CBarrasFlash(CodigoCajaDesktopTextBox.Text)
                RecibirCaja()
            End If
        End Sub

        Private Sub AgregarCajaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AgregarCajaButton.Click
            RecibirCaja()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            If ValidarRecepcion() Then
                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    'Cambia los estados de las cajas: Por Custodiar (1000)
                    dbmCore.Schemadbo.PA_Especial_Cambia_Estado_Remision.DBExecute(_IdRemision, DBCore.EstadoEnum.Por_Custodiar)

                    Dim dCajasRemision = dbmCore.Schemadbo.CTA_Items_Remision.DBFindByfk_Remision_Cajafk_Caja(_IdRemision, Nothing)
                    For Each cajas In dCajasRemision
                        'Facturación
                        Dim dtFilesCaja = dmArchiving.Schemadbo.CTA_Folder_File.DBFindByCodigo_Caja(cajas.Codigo_Caja)
                        For Each FileCaja In dtFilesCaja
                            Utilities.AgregarMovimiento(dmArchiving, Program.Sesion.Entidad.id, FileCaja.fk_Esquema_Facturacion, DesktopConfig.Servicio_Facturacion.Recepción_Centro_Distribucion, FileCaja.fk_Entidad, FileCaja.fk_Proyecto, FileCaja.fk_Esquema, 1, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow)
                        Next

                        'Actualiza los estados de folders y files
                        Dim dItems = dmArchiving.Schemadbo.CTA_Items_Caja.DBFindByIdCajaModulofk_Estado(cajas.fk_Caja, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Enviado_a_custodia)
                        For Each item In dItems
                            If item.fk_Estado = DBCore.EstadoEnum.Enviado_a_custodia Then
                                If item.CBarras.Substring(0, 3) = "000" Then 'Folder
                                    dmArchiving.Schemadbo.PA_Actualiza_Estado_Folder_2.DBExecute(item.CBarras, item.fk_OT, Program.Sesion.Usuario.id, DBCore.EstadoEnum.Por_Custodiar, "")
                                Else 'File
                                    Utilities.ActualizaEstadoFile(dmArchiving, dbmCore, item.CBarras, item.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Por_Custodiar, Program.Sesion.Usuario.id, Nothing)
                                End If
                            End If
                        Next

                        'Inserta registro en Boveda_Entrada
                        dmArchiving.Transaction_Begin()
                        Dim tBovedaEntrada As New DBArchiving.SchemaCustody.TBL_Boveda_EntradaType
                        tBovedaEntrada.fk_Entidad = Program.DesktopGlobal.BovedaRow.fk_Entidad
                        tBovedaEntrada.fk_Sede = Program.DesktopGlobal.BovedaRow.fk_Sede
                        tBovedaEntrada.fk_Boveda = Program.DesktopGlobal.BovedaRow.id_Boveda
                        tBovedaEntrada.id_Boveda_Entrada = dmArchiving.SchemaCustody.TBL_Boveda_Entrada.DBNextId_for_id_Boveda_Entrada(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede, Program.DesktopGlobal.BovedaRow.id_Boveda)
                        tBovedaEntrada.Fecha_Boveda_Entrada = SlygNullable.SysDate
                        tBovedaEntrada.fk_Caja = cajas.fk_Caja
                        dmArchiving.SchemaCustody.TBL_Boveda_Entrada.DBInsert(tBovedaEntrada)
                        dmArchiving.Transaction_Commit()
                    Next

                    'La remisión queda en un estado superior: 1100 (Custodia)
                    Dim tRemision As New DBCore.SchemaProcess.TBL_Remision_CajaType
                    tRemision.fk_Estado = DBCore.EstadoEnum.Custodia
                    dbmCore.SchemaProcess.TBL_Remision_Caja.DBUpdate(tRemision, _IdRemision)

                    DesktopMessageBoxControl.DesktopMessageShow("Remisión recibida correctamente.", "Remisión recibida", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Me.Close()
                Catch ex As Exception
                    dmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("GuardarButton_Click", ex)
                Finally
                    dmArchiving.Connection_Close()
                    dbmCore.Connection_Close()
                End Try
            Else
                DesktopMessageBoxControl.DesktopMessageShow("El número de cajas recibidas no coincide con el número de cajas de la remisión.", "Número Cajas no válido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                UbicaFoco()
            End If
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargarCajas()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim dtCajas = dbmCore.Schemadbo.CTA_Items_Remision.DBFindByfk_Remision_Cajafk_Caja(_IdRemision, Nothing)
                CajasDesktopDataGridView.AutoGenerateColumns = False
                CajasDesktopDataGridView.DataSource = dtCajas
                _countCajasRemision = dtCajas.Count
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarCajas", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub RecibirCaja()
            Try
                If ExisteCaja(CLng(CodigoCajaDesktopTextBox.Text)) Then
                    UbicaFoco()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El código ingresado no es válido.", "Código Inválido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    UbicaFoco()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("RecibirCaja", ex)
            End Try
        End Sub

        Private Sub UbicaFoco()
            CodigoCajaDesktopTextBox.Focus()
            CodigoCajaDesktopTextBox.SelectAll()
        End Sub
#End Region

#Region " Funciones "
        Private Function ExisteCaja(ByVal codigo As Long) As Boolean
            Dim bReturn As Boolean = False
            Try
                For Each row As DataGridViewRow In CajasDesktopDataGridView.Rows
                    If CLng(row.Cells("CodigoCaja").Value) = codigo Then
                        If Not CBool(row.Cells("Recibido").Value) Then
                            row.Cells("Recibido").Value = True
                            bReturn = True
                            Exit For
                        End If
                    End If
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ExisteCaja", ex)
            End Try
            Return bReturn
        End Function

        Private Function ValidarRecepcion() As Boolean
            Dim bReturn As Boolean = False
            Dim count As Integer = 0
            Try
                For Each row As DataGridViewRow In CajasDesktopDataGridView.Rows
                    If CBool(row.Cells("Recibido").Value) Then
                        count += 1
                    End If
                Next
                If count = _countCajasRemision Then
                    bReturn = True
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidarRecepcion", ex)
            End Try
            Return bReturn
        End Function
#End Region

    End Class

End Namespace