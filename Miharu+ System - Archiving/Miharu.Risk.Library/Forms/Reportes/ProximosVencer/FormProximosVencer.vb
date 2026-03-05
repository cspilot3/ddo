Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopReportDataGridView
Imports System.Windows.Forms


Public Class FormProximosVencer

    Private Sub FormProximosVencer_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        CargarCombos()
    End Sub

    Private Sub CargarCombos()
        Dim Archiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        dbmCore.Connection_Open(Program.Sesion.Usuario.id)
        Archiving.Connection_Open(Program.Sesion.Usuario.id)

        Try
            Dim Entidad = Archiving.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.RiskGlobal.Entidad)
            EntidadProximoVencer.DataSource = Entidad
            EntidadProximoVencer.ValueMember = Entidad.id_EntidadColumn.ColumnName
            EntidadProximoVencer.DisplayMember = Entidad.Nombre_EntidadColumn.ColumnName

            Dim Proyecto = Archiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidad(CShort(EntidadProximoVencer.SelectedValue))
            ProyectoProximoVencer.DataSource = Proyecto
            ProyectoProximoVencer.ValueMember = Proyecto.id_ProyectoColumn.ColumnName
            ProyectoProximoVencer.DisplayMember = Proyecto.Nombre_ProyectoColumn.ColumnName

            Dim Esquema = Archiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(CShort(EntidadProximoVencer.SelectedValue), CShort(ProyectoProximoVencer.SelectedValue))
            EsquemaProximoVencer.DataSource = Esquema
            EsquemaProximoVencer.ValueMember = Esquema.fk_esquemaColumn.ColumnName
            EsquemaProximoVencer.DisplayMember = Esquema.Nombre_esquemaColumn.ColumnName

            Dim UsuarioSolicitante = dbmCore.SchemaSecurity.CTA_Usuario.DBFindByfk_Entidad(CShort(EntidadProximoVencer.SelectedValue))
            UsuarioSolicitanteProximoVencer.DataSource = UsuarioSolicitante
            UsuarioSolicitanteProximoVencer.ValueMember = UsuarioSolicitante.id_UsuarioColumn.ColumnName
            UsuarioSolicitanteProximoVencer.DisplayMember = UsuarioSolicitante.NombresColumn.ColumnName

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("CargarCombos", ex)
        End Try

        Archiving.Connection_Close()
        dbmCore.Connection_Close()
    End Sub

    Private Sub BuscarButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarButton.Click
        ResultadosPanel.Controls.Clear()

        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        Try

            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim ValidarSolicitudes = dbmArchiving.SchemaProcess.PA_Validar_Solicitudes_Vencimiento.DBExecute(CShort(EntidadProximoVencer.SelectedValue), CShort(ProyectoProximoVencer.SelectedValue), Now())

            If ValidarSolicitudes.Rows.Count = 0 Then
                LeerDataSet(CShort(EntidadProximoVencer.SelectedValue), CShort(ProyectoProximoVencer.SelectedValue), CShort(EsquemaProximoVencer.SelectedValue), CShort(UsuarioSolicitanteProximoVencer.SelectedValue))
            Else
                Dim Meses As String
                Dim Year As String
                Dim Month As String
                For Each row As DataRow In ValidarSolicitudes.Rows
                    Year = row("Fecha_Vencimiento").ToString().Substring(0, 4)
                    Month = row("Fecha_Vencimiento").ToString().Substring(5, 2)
                    Meses += " " + MonthName(CInt(Month)).Substring(0, 1).ToUpper() + MonthName(CInt(Month)).Substring(1) + " " + Year
                    Dim s = Format(row("Fecha_Vencimiento").ToString(), "yyyy-MM")

                Next row
                DesktopMessageBoxControl.DesktopMessageShow("Ya se ha realizado una solicitud de documento por vencimiento para el(los) mes(es) de" + Meses, "Gestion de vencimientos", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            End If

        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("CargarCombos", ex)
        End Try

    End Sub


    Private Sub LeerDataSet(fkEntidad As Integer, fkProyecto As Integer, fkEsquema As Integer, fkUsuario As Integer)

        Dim dataTable As DataTable = Nothing
        Dim nuevaGrilla = New DesktopReportDataGridViewControlValidation
        Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
        dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
        Try
            dataTable = dbmArchiving.SchemaReport.PA_Reporte_Proximo_Vencer.DBExecute(fkEntidad, fkProyecto, fkEsquema, DateTime.Now)

            If dataTable.Rows.Count > 0 Then
                nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core
                nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools
                nuevaGrilla.Conection_String_Archiving = Program.DesktopGlobal.ConnectionStrings.Archiving
                nuevaGrilla.fkProyecto = fkProyecto
                nuevaGrilla.fkEntidad = fkEntidad
                nuevaGrilla.fkEsquema = fkEsquema
                nuevaGrilla.GetUsuario = fkUsuario
                nuevaGrilla.Dock = DockStyle.Fill
                nuevaGrilla.Titulo = "Reporte"
                nuevaGrilla.InternalGridView.DataSource = dataTable
                ResultadosPanel.Controls.Add(nuevaGrilla)
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No hay documentos para procesar por solicitud de vencimiento", "Gestion de vencimientos", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            End If

        Catch ex As Exception

        End Try



        dbmArchiving.Connection_Close()

    End Sub

End Class