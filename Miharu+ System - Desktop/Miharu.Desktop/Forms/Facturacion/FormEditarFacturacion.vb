Imports Miharu.Desktop.Library.Config
Imports DBCore
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports DBArchiving.SchemaBill
Imports Miharu.Desktop.Library.Config.DesktopConfig
Imports System.Text

Public Class FormEditarFacturacion
    Inherits Miharu.Desktop.Library.FormBase

#Region " Declaraciones "
    Private _id_Entidad As Short
    Private _id_Facturacion As Integer
    Public _dsCliente As New DataSet()
#End Region

#Region " Constructores "

    Public Sub New(ByVal id_Entidad As Short, ByVal id_Facturacion As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _id_Entidad = id_Entidad
        _id_Facturacion = id_Facturacion
        IDLabel.Text = "FACTURACIÓN: " & _id_Facturacion
    End Sub

#End Region

#Region " Eventos "
    Private Sub FormEditarFacturacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        CargarFiltros()
    End Sub

    Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
        Me.Close()
    End Sub

    Private Sub EntidadClienteComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadClienteComboBox.SelectedIndexChanged
        Dim dtProyectoCliente = Utilities.SelectIntoDataTable("fk_Entidad=" & EntidadClienteComboBox.SelectedValue.ToString(), _dsCliente.Tables(1))
        Utilities.LlenarCombo(ProyectoComboBox, dtProyectoCliente, "id_Proyecto", "Nombre_Proyecto", True)

        Dim dtEsquemaFacturacion = Utilities.SelectIntoDataTable("fk_Entidad_Cliente=" & EntidadClienteComboBox.SelectedValue.ToString(), _dsCliente.Tables(3))
        Utilities.LlenarCombo(EsquemaFacturacionComboBox, dtEsquemaFacturacion, "ID", "Valor", True)
    End Sub

    Private Sub ProyectoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoComboBox.SelectedIndexChanged
        Dim dtEsquemaCliente = Utilities.SelectIntoDataTable("fk_Proyecto=" & ProyectoComboBox.SelectedValue.ToString() & " AND fk_Entidad=" & EntidadClienteComboBox.SelectedValue.ToString(), _dsCliente.Tables(2))
        Utilities.LlenarCombo(EsquemaClienteComboBox, dtEsquemaCliente, "fk_Esquema", "Nombre_Esquema", True)
    End Sub

    Private Sub EsquemaFacturacionComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaFacturacionComboBox.SelectedIndexChanged
        Try
            If CStr(EsquemaFacturacionComboBox.SelectedValue) <> "-1" Then
                Dim arrEsquema = EsquemaFacturacionComboBox.SelectedValue.ToString().Split(CChar("-"))

                Dim dtServicioFacturacion = Utilities.SelectIntoDataTable("fk_Entidad=" & arrEsquema(0).ToString() & "AND fk_Esquema=" & arrEsquema(1).ToString(), _dsCliente.Tables(4))
                Utilities.LlenarCombo(ServicioFacturacionComboBox, dtServicioFacturacion, "id_Servicio", "Nombre_Servicio", True)
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("EsquemaFacturacionComboBox_SelectedIndexChanged", ex)
        End Try
    End Sub

    Private Sub ModificarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ModificarButton.Click
        If ValidaActualizacion() Then
            ModificaFacturacion()
        End If
    End Sub
#End Region

#Region " Metodos "
    Private Sub CargarFiltros()
        Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Try
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim dtEntidadCliente = dmArchiving.SchemaSecurity.CTA_Entidad.DBGet()
            Dim dtProyectoCliente = dmArchiving.SchemaCore.CTA_Proyecto.DBFindByfk_Entidad(Nothing)
            Dim dtEsquemaCliente = dmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(Nothing, Nothing)

            Dim dtEsquemaFacturacion = dmArchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidadfk_Entidad_Cliente(Nothing, Nothing)
            Dim dtServicioFacturacion = dmArchiving.Schemadbo.CTA_Servicio_x_Esquema.DBFindByfk_Entidadfk_Esquema(Nothing, Nothing)

            _dsCliente.Tables.Add(dtEntidadCliente) '0: Entidad
            _dsCliente.Tables.Add(dtProyectoCliente) '1: Proyecto
            _dsCliente.Tables.Add(dtEsquemaCliente) '2: Esquema

            _dsCliente.Tables.Add(dtEsquemaFacturacion) '3: Esquema Facturacion
            _dsCliente.Tables.Add(dtServicioFacturacion) '4: Servicio Facturacion

            Utilities.LlenarCombo(EntidadClienteComboBox, dtEntidadCliente, dtEntidadCliente.id_EntidadColumn.ColumnName, dtEntidadCliente.Nombre_EntidadColumn.ColumnName, True)
        Catch ex As Exception
            DMB.DesktopMessageShow("CargarFiltros", ex)
        Finally
            dmArchiving.Connection_Close()
        End Try
    End Sub

    Private Sub ModificaFacturacion()
        Dim dmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Try
            dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            dmArchiving.Transaction_Begin()

            Dim tFacturacionDetalle As New TBL_Facturacion_DetalleType
            tFacturacionDetalle.fk_Entidad = _id_Entidad
            tFacturacionDetalle.fk_Facturacion = _id_Facturacion
            tFacturacionDetalle.id_Facturacion_Detalle = dmArchiving.SchemaBill.TBL_Facturacion_Detalle.DBNextId(_id_Entidad, _id_Facturacion)
            tFacturacionDetalle.fk_Sede = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
            tFacturacionDetalle.fk_Centro_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento

            Dim arrEsquema = EsquemaFacturacionComboBox.SelectedValue.ToString().Split(CChar("-"))
            tFacturacionDetalle.fk_Esquema = CShort(arrEsquema(1))
            tFacturacionDetalle.fk_Servicio = CShort(ServicioFacturacionComboBox.SelectedValue)

            tFacturacionDetalle.fk_Entidad_Cliente = CShort(EntidadClienteComboBox.SelectedValue)
            tFacturacionDetalle.fk_Proyecto_Cliente = CShort(ProyectoComboBox.SelectedValue)
            tFacturacionDetalle.fk_Esquema_Cliente = CShort(EsquemaClienteComboBox.SelectedValue)
            tFacturacionDetalle.Fecha_Movimiento = FechaMovimientoDateTimePicker.Value
            tFacturacionDetalle.Cantidad_Movimiento = CInt(CantidadTextBox.Text)
            tFacturacionDetalle.fk_Usuario_Log = Program.Sesion.Usuario.id
            tFacturacionDetalle.Tipo_Registro = "A"
            tFacturacionDetalle.Transmitido = False
            dmArchiving.SchemaBill.TBL_Facturacion_Detalle.DBInsert(tFacturacionDetalle)

            dmArchiving.Transaction_Commit()
            DMB.DesktopMessageShow("Modificación realizada correctamente.", "Facturación actualizada", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Me.Close()
        Catch ex As Exception
            dmArchiving.Transaction_Rollback()
            DMB.DesktopMessageShow("ModificaFacturacion", ex)
        Finally
            dmArchiving.Connection_Close()
        End Try
    End Sub
#End Region

#Region " Funciones "
    Private Function ValidaActualizacion() As Boolean
        Dim bReturn As Boolean = True
        Dim msg As New StringBuilder
        Try
            If IsNothing(EntidadClienteComboBox.SelectedValue) OrElse CStr(EntidadClienteComboBox.SelectedValue) = "-1" Then msg.AppendLine("- Entidad Cliente")
            If IsNothing(ProyectoComboBox.SelectedValue) OrElse CStr(ProyectoComboBox.SelectedValue) = "-1" Then msg.AppendLine("- Proyecto Cliente")
            If IsNothing(EsquemaClienteComboBox.SelectedValue) OrElse CStr(EsquemaClienteComboBox.SelectedValue) = "-1" Then msg.AppendLine("- Esquema Cliente")
            If IsNothing(EsquemaFacturacionComboBox.SelectedValue) OrElse CStr(EsquemaFacturacionComboBox.SelectedValue) = "-1" Then msg.AppendLine("- Esquema Facturación")
            If IsNothing(ServicioFacturacionComboBox.SelectedValue) OrElse CStr(ServicioFacturacionComboBox.SelectedValue) = "-1" Then msg.AppendLine("- Servicio Facturación")
            If CantidadTextBox.Text = "" Then msg.AppendLine("- Cantidad Facturación")

            If msg.ToString <> "" Then
                bReturn = False
                DMB.DesktopMessageShow("Por favor revise los siguientes campos: " & vbCrLf & msg.ToString(), "Datos incompletos", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        Catch ex As Exception
            DMB.DesktopMessageShow("ValidaActualizacion", ex)
        End Try
        Return bReturn
    End Function
#End Region

End Class