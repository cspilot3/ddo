Imports DBAgrario
Imports DBArchiving
Imports DBSecurity
Imports DBSecurity.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports SLYG.Tools

Namespace Risk.Forms.OT

    Public Class FormCrearOT

#Region " Declaraciones "
        Private _Plugin As BanagrarioRiskPlugin
#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub FormCrearOT_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            ' Validar que la fecha de operación no sea mayor a la actual
            If Validar() Then
                GuardarOT()
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub LlenarCombos()
            Dim dmarchiving As DBArchivingDataBaseManager = Nothing
            Dim dmSecurity As DBSecurityDataBaseManager = Nothing

            Try
                dmarchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                'dmarchiving.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmarchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dmSecurity = New DBSecurityDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                'dmSecurity.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dmSecurity.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                Dim EsquemasFacturacion = dmarchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidadfk_Entidad_Cliente(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.RiskGlobal.Entidad)
                Utilities.LlenarCombo(EsquemaComboBox, EsquemasFacturacion, EsquemasFacturacion.IDColumn.ColumnName, EsquemasFacturacion.ValorColumn.ColumnName)

                Dim sedesData = dmSecurity.SchemaConfig.CTA_Sede.DBFindByfk_Entidadid_Sede(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing, 0, New CTA_SedeEnumList(CTA_SedeEnum.Nombre_Ciudad_Sede, True))
                Utilities.LlenarCombo(SedeComboBox, sedesData, CTA_SedeEnum.id_Sede.ColumnName, CTA_SedeEnum.Nombre_Ciudad_Sede.ColumnName)

                For Each item In SedeComboBox.Items
                    Dim row = DirectCast(item, DataRowView).Row
                    If (row(CTA_SedeEnum.id_Sede.ColumnName).ToString() = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()) Then
                        SedeComboBox.SelectedItem = item
                        Exit For
                    End If
                Next

                SedeComboBox.Enabled = _Plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Control.Path)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("GenerarOT", ex)
            Finally
                Try : dmarchiving.Connection_Close() : Catch : End Try
                Try : dmSecurity.Connection_Close() : Catch : End Try
            End Try
        End Sub

        Public Sub GuardarOT()
            If IsNothing(EsquemaComboBox.SelectedValue) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe selecciona un esquema de facturacion para crear la OT", "Error creando OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                Dim EsquemaFacturacion As Short = CShort(Split(EsquemaComboBox.SelectedValue.ToString(), "-")(1))

                Dim dmArchiving As DBArchivingDataBaseManager = Nothing
                Try
                    dmArchiving = New DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
                    'dmArchiving.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                    dmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    dmArchiving.Transaction_Begin()

                    Dim NextOt As New SchemaRisk.TBL_OTType
                    NextOt.id_OT = dmArchiving.SchemaRisk.TBL_OT.DBNextId
                    NextOt.fk_Entidad = _Plugin.Manager.RiskGlobal.Entidad
                    NextOt.fk_Proyecto = _Plugin.Manager.RiskGlobal.Proyecto
                    NextOt.fk_Cargue = Nothing
                    NextOt.Fecha_OT = SlygNullable.SysDate
                    NextOt.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
                    If _Plugin.Manager.RiskGlobal.ProyectoRow.usa_cargue_parcial Then
                        NextOt.fk_Estado = DBCore.EstadoEnum.Cargado
                    Else
                        NextOt.fk_Estado = DBCore.EstadoEnum.Creado
                    End If
                    NextOt.fk_Entidad_Procesamiento = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                    NextOt.fk_Sede_Procesamiento = CShort(SedeComboBox.SelectedValue)
                    NextOt.fk_Esquema_Facturacion = EsquemaFacturacion
                    NextOt.Fecha_Proceso = FechaOperacionDateTimePicker.Value

                    dmArchiving.SchemaRisk.TBL_OT.DBInsert(NextOt)
                    dmArchiving.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Se ha generado la Ot ( " & CStr(NextOt.id_OT) & " ).", "OT Generada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                Catch ex As Exception
                    Try : dmArchiving.Transaction_Rollback() : Catch : End Try
                    DesktopMessageBoxControl.DesktopMessageShow("GenerarOT", ex)
                Finally
                    Try : dmArchiving.Connection_Close() : Catch : End Try
                End Try

            End If
        End Sub

        Public Function Validar() As Boolean
            If (FechaOperacionDateTimePicker.Value.ToString("yyyy-MM-dd") < Now.ToString("yyyy-MM-dd")) Then
                Return MostrarMensajeValidacion("La Fecha de Operación no puede ser menor a la fecha actual")
            End If

            If (FechaOperacionDateTimePicker.Value > Now.AddDays(Program.OTMaximoDiasAdelante)) Then
                Return MostrarMensajeValidacion("La Fecha de Operación no puede ser mayor a la fecha " + Now.AddDays(Program.OTMaximoDiasAdelante).ToString("yyyy-MM-dd"))
            End If

            If (SedeComboBox.SelectedIndex = -1) Then
                Return MostrarMensajeValidacion("la ciudad de procesamiento es requerida ")
            End If

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
                'dbmAgrario.DataBase.Identifier_Date_Format = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim _EntidadProcesamientoId = _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                Dim _SedeProcesamientoId = CShort(SedeComboBox.SelectedValue)

                Dim ValidacionOT = dbmAgrario.SchemaProcess.PA_OT_Validar.DBExecute(FechaOperacionDateTimePicker.Value.ToString("yyyy-MM-dd"), _EntidadProcesamientoId, _SedeProcesamientoId, Program.OTDiasDiasSinCerrar)

                If (ValidacionOT <> "") Then
                    Return MostrarMensajeValidacion("No se es posible crear la OT, " + ValidacionOT)
                End If

            Catch ex As Exception
                Return MostrarMensajeValidacion("No fue posible validar la fecha de operacion, " + ex.Message)
            Finally
                Try : dbmAgrario.Connection_Close() : Catch : End Try
            End Try

            Return True
        End Function

        Public Function MostrarMensajeValidacion(ByVal mensaje As String) As Boolean
            If DesktopMessageBoxControl.DesktopMessageShow(mensaje, "GenerarOT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True) = MsgBoxResult.Ok Then
                FechaOperacionDateTimePicker.Focus()
            End If
            Return False
        End Function

#End Region

    End Class

End Namespace