Imports System.Windows.Forms
Imports DBAgrario
Imports DBAgrario.SchemaArchiving
Imports DBArchiving
Imports DBCore
Imports DBSecurity
Imports DBSecurity.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Risk.Forms.OT

    Public Class FormSeleccionarOT
        Inherits Form

#Region " Declaraciones "

        Private _Plugin As BanagrarioRiskPlugin

        Private _OTS_Data As CTA_Risk_OTDataTable

        Private _OTS_List As New ListItemDataTable(CTA_Risk_OTEnum.Fecha_Proceso.ColumnName, CTA_Risk_OTEnum.id_OT.ColumnName)

#End Region

#Region " Propiedades "
        Public Property OT_Seleccionada() As Integer
        Public Property OT_Fecha() As String
        'Public Property CentroProcesamiento() As Integer
        'Public Property SedeProcesamiento() As Integer

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormSeleccionarOT_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            LlenarOTSFiltro()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            SeleccionarOt()
        End Sub

        Private Sub OtListBox_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles OtListBox.DoubleClick
            SeleccionarOt()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub OtListBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles OtListBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                AceptarButton.Focus()
            End If
        End Sub

        Private Sub OtBuscarDesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles OtBuscarDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                'If OtBuscarDesktopTextBox.Text = "" Then
                '    '    OtListBox.Focus()
                '    'Else
                '    '    BuscarButton.Focus()

                'End If
                LlenarOTSFiltro()
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Sub LlenarCombos()
            Dim dmSecurity As DBSecurityDataBaseManager = Nothing

            Try

                dmSecurity = New DBSecurityDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                'dmSecurity.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmSecurity.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim sedesData = dmSecurity.SchemaConfig.CTA_Sede.DBFindByfk_Entidadid_Sede(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing, 0, New CTA_SedeEnumList(CTA_SedeEnum.Nombre_Ciudad_Sede, True))
                Utilities.LlenarCombo(SedeComboBox, sedesData, CTA_SedeEnum.id_Sede.ColumnName, CTA_SedeEnum.Nombre_Ciudad_Sede.ColumnName)

                For Each item In SedeComboBox.Items
                    Dim row = DirectCast(item, DataRowView).Row
                    If (row(CTA_SedeEnum.id_Sede.ColumnName).ToString() = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()) Then
                        SedeComboBox.SelectedItem = item
                        Exit For
                    End If
                Next

                SedeComboBox.Enabled = False
                'TODO: Agregar control de cordinador Bogota

                CargarOtsFecha()

                OtListBox.Focus()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GenerarOT", ex)
            Finally
                Try : dmSecurity.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Private Sub CargarOtsFecha()
            If (SedeComboBox.SelectedIndex = -1) Then
                Return
            End If


            Dim dmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dmArchiving.Transaction_Begin()
                dmArchiving.Schemadbo.PA_Actualizar_OTs.DBExecute()
                dmArchiving.Transaction_Commit()

            Catch ex As Exception
                Try : dmArchiving.Transaction_Rollback() : Catch : End Try
            Finally
                Try : dmArchiving.Connection_Close() : Catch : End Try
            End Try


            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                OtListBox.Items.Clear()
                OtListBox.SelectedItem = Nothing

                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim _EntidadProcesamientoId = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                Dim _SedeProcesamientoId = CShort(SedeComboBox.SelectedValue)

                Dim OTDisponible As Integer = 0
                Dim CantidadOTDisponibles As Integer = 0

                _OTS_List.Rows.Clear()
                _OTS_Data = dbmAgrario.SchemaProcess.PA_OT_Get.DBExecute(_EntidadProcesamientoId, _SedeProcesamientoId, Program.OTDiasAtras)
                For Each row In _OTS_Data
                    _OTS_List.Add(row.Fecha_Proceso.ToString("yyyy-MM-dd") + " OT(" + row.id_OT.ToString() + "-" + GetNombreEstado(row.fk_Estado) + ")", CStr(row.id_OT))

                    If (row.fk_Estado < EstadoEnum.Cerrado) Then
                        OTDisponible = row.id_OT
                        CantidadOTDisponibles += 1
                    End If
                Next

                If (CantidadOTDisponibles = 1) Then

                    OT_Seleccionada = OTDisponible

                    'CentroProcesamiento = 1
                    'SedeProcesamiento = 3

                    DialogResult = DialogResult.OK
                    Close()
                    Return
                End If

                OtListBox.DataSource = _OTS_List
                OtListBox.ValueMember = CTA_Risk_OTEnum.id_OT.ColumnName
                OtListBox.DisplayMember = CTA_Risk_OTEnum.Fecha_Proceso.ColumnName

                For Each item In OtListBox.Items
                    Dim fechaOT As String = CStr(DirectCast(item, DataRowView).Row(CTA_Risk_OTEnum.Fecha_Proceso.ColumnName))
                    If (fechaOT.IndexOf(Now.ToString("yyyy-MM-dd")) > -1) Then
                        OtListBox.SelectedItem = item
                        Exit For
                    End If
                Next

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible cargar las OTS, " + ex.Message, "GenerarOT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Finally
                Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Public Sub LlenarOTSFiltro()
            Dim view As DataView = Utilities.clonarDataTable(_OTS_List).DefaultView
            Dim filtro As String = CTA_Risk_OTEnum.Fecha_Proceso.ColumnName & " LIKE '%" & OtBuscarDesktopTextBox.Text & "%'"
            view.RowFilter = filtro

            OtListBox.DataSource = view.ToTable
            OtListBox.ValueMember = CTA_Risk_OTEnum.id_OT.ColumnName
            OtListBox.DisplayMember = CTA_Risk_OTEnum.Fecha_Proceso.ColumnName
        End Sub

        Public Sub SeleccionarOt()
            If CStr(OtListBox.SelectedValue) = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una OT para continuar", "Seleccion OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                OT_Seleccionada = CInt(OtListBox.SelectedValue)
                OT_Fecha = FechaOT(OT_Seleccionada)
                _Plugin.Manager.RiskGlobal.OT = OT_Seleccionada

                'CentroProcesamiento = 1
                'SedeProcesamiento = 3

                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End Sub

        Public Function FechaOT(ByVal nid_OT As Integer) As String
            Dim dmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dmArchiving.Transaction_Begin()
                Dim OT_DataTable = dmArchiving.SchemaRisk.TBL_OT.DBFindByid_OT(nid_OT)
                Dim fecha As String = OT_DataTable(0).Fecha_OT.ToString("dd-MM-yyyy")
                Return fecha
            Catch ex As Exception
                Return "Fecha no valida" + ex.Message
            Finally
                If (dmArchiving IsNot Nothing) Then dmArchiving.Connection_Close()
            End Try
        End Function

        Private Function GetNombreEstado(ByVal fkEstado As Short) As String
            Dim estado = DirectCast(fkEstado, EstadoEnum)
            Return estado.ToString()
        End Function

#End Region

    End Class

End Namespace