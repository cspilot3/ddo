Imports System.Text
Imports System.Windows.Forms
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Facturacion

    Public Class FormEsquemaFacturacion
        Inherits Miharu.Desktop.Library.FormBase

#Region " Declaraciones "

        Private _fk_Entidad As SlygNullable(Of Short)
        Private _fk_Esquema As SlygNullable(Of Short)
        Private _Nuevo As Boolean

#End Region

#Region " Constructor "

        Sub New(ByVal fk_Entidad As SlygNullable(Of Short), ByVal fk_Esquema As SlygNullable(Of Short), ByVal Nuevo As Boolean)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _fk_Entidad = fk_Entidad
            _fk_Esquema = fk_Esquema
            _Nuevo = Nuevo
        End Sub

#End Region

        Private Sub FormEsquemaFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarCombos()
            CargaInformacion()
        End Sub

        Public Sub LlenarCombos()
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim Entidades = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet()
            Utilities.LlenarCombo(fk_EntidadDesktopComboBox, Entidades, Entidades.id_EntidadColumn.ColumnName, Entidades.Nombre_EntidadColumn.ColumnName)

            dmArchiving.Connection_Close()
        End Sub

        Public Sub CargarDetalles(ByVal Entidad As SlygNullable(Of Short), ByVal Esquema As SlygNullable(Of Short))
            Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim EntidadesGrilla = dmArchiving.Schemadbo.CTA_Facturacion_Entidades_Esquema.DBFindByfk_Entidadid_Esquema(Entidad, Esquema)
            EntidadesDesktopDataGridView.AutoGenerateColumns = False
            EntidadesDesktopDataGridView.DataSource = EntidadesGrilla

            Dim EsquemaGrilla = dmArchiving.Schemadbo.CTA_Facturacion_Servicios_Esquema.DBFindByfk_Entidadid_Esquema(Entidad, Esquema)
            ServiciosDesktopDataGridView.AutoGenerateColumns = False
            ServiciosDesktopDataGridView.DataSource = EsquemaGrilla

            dmArchiving.Connection_Close()
        End Sub

        Public Sub CargaInformacion()
            fk_EntidadDesktopComboBox.Enabled = _Nuevo
            fk_EntidadDesktopComboBox.SelectedValue = _fk_Entidad.ToString()
            id_EsquemaDesktopTextBox.Text = _fk_Esquema.ToString()

            If _Nuevo = False Then
                Dim dmarchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dmarchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim Info = dmarchiving.SchemaBill.TBL_Esquema.DBGet(_fk_Entidad, _fk_Esquema)
                dmarchiving.Connection_Close()

                Nombre_EsquemaDesktopTextBox.Text = Info(0).Nombre_Esquema
                Codigo_FacturacionDesktopTextBox.Text = Info(0).Codigo_Facturacion
                Descripcion_DesktopTextBox.Text = Info(0).Descripcion_Esquema

                CargarDetalles(_fk_Entidad, _fk_Esquema)

            Else
                CargarDetalles(CShort(0), CShort(0))
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            GuardarInfo()
        End Sub

        Public Sub GuardarInfo()
            If Validaciones() Then
                Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Try
                    Dim Esquema As New SchemaBill.TBL_EsquemaType
                    Esquema.fk_Entidad = CShort(fk_EntidadDesktopComboBox.SelectedValue)
                    Esquema.Codigo_Facturacion = Codigo_FacturacionDesktopTextBox.Text
                    Esquema.Descripcion_Esquema = Descripcion_DesktopTextBox.Text
                    Esquema.Nombre_Esquema = Nombre_EsquemaDesktopTextBox.Text

                    dmArchiving.Transaction_Begin()
                    'Guarda la informacio básica del esquema
                    If id_EsquemaDesktopTextBox.Text = "" Then
                        Esquema.id_Esquema = dmArchiving.SchemaBill.TBL_Esquema.DBNextId_for_id_Esquema(Esquema.fk_Entidad)
                        dmArchiving.SchemaBill.TBL_Esquema.DBInsert(Esquema)

                        id_EsquemaDesktopTextBox.Text = CStr(Esquema.id_Esquema)
                        _fk_Entidad = Esquema.fk_Entidad
                        _fk_Esquema = Esquema.id_Esquema
                        _Nuevo = False
                    Else
                        dmArchiving.SchemaBill.TBL_Esquema.DBUpdate(Esquema, Esquema.fk_Entidad, CShort(id_EsquemaDesktopTextBox.Text))
                    End If
                    dmArchiving.Transaction_Commit()
                    dmArchiving.Connection_Close()

                    If ServiciosDesktopDataGridView.Rows.Count = 0 And EntidadesDesktopDataGridView.Rows.Count = 0 Then
                        CargarDetalles(_fk_Entidad, _fk_Esquema)
                    End If

                    'Guarda los servicios que aplican para el esquema
                    dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    Dim Servicios = dmArchiving.SchemaBill.TBL_Servicio_x_Esquema.DBGet(_fk_Entidad, _fk_Esquema, Nothing)
                    dmArchiving.Transaction_Begin()
                    For Each RowServicio As DataGridViewRow In ServiciosDesktopDataGridView.Rows
                        Dim Aplica As Boolean

                        If IsDBNull(RowServicio.Cells("AplicaServicio").Value) Then
                            Aplica = False
                        Else
                            Aplica = CBool(RowServicio.Cells("AplicaServicio").Value)
                        End If

                        Dim ServicioType As New SchemaBill.TBL_Servicio_x_EsquemaType
                        ServicioType.fk_Entidad = _fk_Entidad
                        ServicioType.fk_Esquema = _fk_Esquema
                        ServicioType.fk_Servicio = CShort(RowServicio.Cells("id_Servicio").Value)

                        Dim viewServicio As New DataView(Servicios)
                        viewServicio.RowFilter = "fk_Servicio = " & CStr(ServicioType.fk_Servicio)

                        If viewServicio.ToTable().Rows.Count = 0 And Aplica = True Then
                            dmArchiving.SchemaBill.TBL_Servicio_x_Esquema.DBInsert(ServicioType)
                        ElseIf viewServicio.ToTable().Rows.Count > 0 And Aplica = False Then
                            dmArchiving.SchemaBill.TBL_Servicio_x_Esquema.DBDelete(ServicioType.fk_Entidad, ServicioType.fk_Esquema, ServicioType.fk_Servicio)
                        End If
                    Next
                    dmArchiving.Transaction_Commit()

                    'Guarda las entidades a las que aplica el esquema
                    Dim Entidades = dmArchiving.SchemaBill.TBL_Esquema_x_Cliente.DBGet(_fk_Entidad, _fk_Esquema, Nothing)
                    dmArchiving.Transaction_Begin()
                    For Each RowEntidad As DataGridViewRow In EntidadesDesktopDataGridView.Rows
                        Dim Aplica As Boolean

                        If IsDBNull(RowEntidad.Cells("AplicaEntidad").Value) Then
                            Aplica = False
                        Else
                            Aplica = CBool(RowEntidad.Cells("AplicaEntidad").Value)
                        End If

                        Dim EntidadType As New SchemaBill.TBL_Esquema_x_ClienteType
                        EntidadType.fk_Entidad = _fk_Entidad
                        EntidadType.fk_Esquema = _fk_Esquema
                        EntidadType.fk_Entidad_Cliente = CShort(RowEntidad.Cells("id_Entidad").Value)

                        Dim viewEntidad As New DataView(Entidades)
                        viewEntidad.RowFilter = "fk_Entidad_Cliente = " & CStr(EntidadType.fk_Entidad_Cliente)

                        If viewEntidad.ToTable().Rows.Count = 0 And Aplica = True Then
                            dmArchiving.SchemaBill.TBL_Esquema_x_Cliente.DBInsert(EntidadType)
                        ElseIf viewEntidad.ToTable().Rows.Count > 0 And Aplica = False Then
                            dmArchiving.SchemaBill.TBL_Esquema_x_Cliente.DBDelete(EntidadType.fk_Entidad, EntidadType.fk_Esquema, EntidadType.fk_Entidad_Cliente)
                        End If
                    Next

                    dmArchiving.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Los datos han sido guardados con exito", "Esquema Facturacion OK", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    dmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("GuardarInfo", ex)
                Finally
                    dmArchiving.Connection_Close()
                End Try
            End If
        End Sub

        Public Function Validaciones() As Boolean
            Dim valida As Boolean = True
            Dim Cadena As New StringBuilder

            If IsNothing(fk_EntidadDesktopComboBox.SelectedValue) Then
                Cadena.AppendLine("Debe seleccionar una entidad de facturación del esquema.")
                valida = False
            End If

            If Nombre_EsquemaDesktopTextBox.Text = "" Then
                Cadena.AppendLine("El nombre del esquema es obligatorio.")
                valida = False
            End If

            If Descripcion_DesktopTextBox.Text = "" Then
                Cadena.AppendLine("La descripcion del esquema es obligatoria.")
                valida = False
            End If

            If Codigo_FacturacionDesktopTextBox.Text = "" Then
                Cadena.AppendLine("el codigo de facturación del esquema es obligatorio.")
                valida = False
            End If

            If valida = False Then
                DesktopMessageBoxControl.DesktopMessageShow(Cadena.ToString(), "Error Guardando Esquema", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If

            Return valida
        End Function

    End Class
End Namespace