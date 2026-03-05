Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.CentroDistribucion

Namespace Forms.CentroDistribucion

    Public Class FormBandejaDistribucion
        Inherits FormBase

#Region " Declaraciones "
        'Tabla 0: Entidad
        'Tabla 1: Proyecto
        'Table 2: Sede
        'Table 3: Bóveda
        'Tabla 4: Caja
        'Tabla 5: Remisiones
        Dim _dsCentroDistribucion As New DataSet
#End Region

#Region " Constructor "
        Public Sub New()
            CargaDataSet()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.        
        End Sub
#End Region

#Region " Eventos "
        Private Sub FormBandejaDistribucion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarBusquedasPendientes(-1)
            CargarBusquedasRemisiones(-1, -1)
            CargarCajas()
            CargarRemisiones(True)
        End Sub

        Private Sub CrearRemisionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CrearRemisionButton.Click
            Dim objCrearRemision = New FormCrearRemision()
            objCrearRemision.ShowDialog()

            CargaDataSet()

            CargarBusquedasPendientes(-1)
            CargarBusquedasRemisiones(-1, -1)
            'CargarCajas()
            'CargarRemisiones(PendientesDesktopCheckBox.Checked)
        End Sub

        Private Sub RemisionesDesktopDataGridView_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellMouseEventArgs) Handles RemisionesDesktopDataGridView.CellMouseDoubleClick
            If e.RowIndex > -1 Then
                Dim objRemision As New FormCrearRemision(CInt(RemisionesDesktopDataGridView.Rows(e.RowIndex).Cells("Id_Remision").Value))
                objRemision.ShowDialog()
                CargarCajas()
                CargarRemisiones(PendientesDesktopCheckBox.Checked)
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub CerrarRemisionesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarRemisionesButton.Click
            Me.Close()
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            If RemisionesDesktopDataGridView.SelectedRows.Count > 0 Then
                Dim objImpresion As New FormImpresionRemision(CLng(RemisionesDesktopDataGridView.SelectedRows(0).Cells(0).Value))
                objImpresion.ShowDialog()
                CargarRemisiones(PendientesDesktopCheckBox.Checked)
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una remisión para imprimirla.", "Selección de Remisión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub PendientesDesktopCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles PendientesDesktopCheckBox.CheckedChanged
            CargarRemisiones(PendientesDesktopCheckBox.Checked)
        End Sub

        Private Sub EntidadComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            CargarBusquedasPendientes(CShort(EntidadComboBox.SelectedValue))
        End Sub

        Private Sub ProyectoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoComboBox.SelectedIndexChanged
            CargarCajas()
        End Sub

        Private Sub EntidadRemisionDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadRemisionDesktopComboBox.SelectedIndexChanged
            CargarBusquedasRemisiones(CShort(EntidadRemisionDesktopComboBox.SelectedValue), -1)
        End Sub

        Private Sub SedeRemisionDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SedeRemisionDesktopComboBox.SelectedIndexChanged
            CargarBusquedasRemisiones(CShort(EntidadRemisionDesktopComboBox.SelectedValue), CShort(SedeRemisionDesktopComboBox.SelectedValue))
        End Sub

        Private Sub BovedaRemisionDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles BovedaRemisionDesktopComboBox.SelectedIndexChanged
            CargarRemisiones(PendientesDesktopCheckBox.Checked)
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargaDataSet()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Dim tRemisiones As New linkCore.Schemadbo.CTA_RemisionesDataTable
                Dim dtEntidad = dbmCore.Schemadbo.CTA_Entidad.DBGet()
                Dim dtProyecto = dbmCore.Schemadbo.CTA_Proyectos_Vigentes.DBFindByfk_Entidad(Nothing)
                Dim dtSede = dbmCore.Schemadbo.CTA_Sede.DBFindByfk_Entidad(Nothing)
                Dim dtBoveda = dbmCore.SchemaCustody.TBL_Boveda.DBFindByfk_Entidadfk_Sede(Nothing, Nothing)
                Dim tCajas = dbmArchiving.Schemadbo.CTA_Caja.DBFindByfk_Sedefk_Entidadfk_Estadofk_Entidad_Clientefk_Proyecto_Cliente(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, DBCore.EstadoEnum.Centro_Distribucion, Nothing, Nothing)

                Dim orderRemisiones As New DBCore.Schemadbo.CTA_RemisionesEnumList
                orderRemisiones.Add(DBCore.Schemadbo.CTA_RemisionesEnum.fk_Estado, True)
                Dim tRemisiones = dbmCore.Schemadbo.CTA_Remisiones.DBFindByfk_Estadofk_Entidadfk_Bovedafk_Sede(Nothing, Nothing, Nothing, Nothing, 0, orderRemisiones)

                _dsCentroDistribucion.Tables.Clear()
                _dsCentroDistribucion.Tables.Add(dtEntidad)
                _dsCentroDistribucion.Tables.Add(dtProyecto)
                _dsCentroDistribucion.Tables.Add(dtSede)
                _dsCentroDistribucion.Tables.Add(dtBoveda)
                _dsCentroDistribucion.Tables.Add(tCajas)
                _dsCentroDistribucion.Tables.Add(tRemisiones)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDataSet", ex)
            Finally
                dbmArchiving.Connection_Close()
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargarCajas()
            Try
                Dim nEntidad As Slyg.Tools.SlygNullable(Of Short)
                Dim nProyecto As Slyg.Tools.SlygNullable(Of Short)

                If CShort(EntidadComboBox.SelectedValue) = -1 Then
                    nEntidad = DBNull.Value
                Else
                    nEntidad = CShort(EntidadComboBox.SelectedValue)
                End If

                If CShort(ProyectoComboBox.SelectedValue) = -1 Then
                    nProyecto = DBNull.Value
                Else
                    nProyecto = CShort(ProyectoComboBox.SelectedValue)
                End If

                Dim tCajas As New DataTable
                If nEntidad.IsDbNull And nProyecto.IsDbNull Then
                    tCajas = _dsCentroDistribucion.Tables(4)
                ElseIf Not nEntidad.IsDbNull And nProyecto.IsDbNull Then
                    tCajas = Utilities.SelectIntoDataTable("fk_Entidad_Cliente=" & nEntidad.ToString(), _dsCentroDistribucion.Tables(4))
                ElseIf Not nEntidad.IsDbNull And Not nProyecto.IsDbNull Then
                    tCajas = Utilities.SelectIntoDataTable("fk_Entidad_Cliente=" & nEntidad.ToString() & " AND fk_Proyecto_Cliente=" & nProyecto.ToString(), _dsCentroDistribucion.Tables(4))
                End If

                CajasDataGridView.AutoGenerateColumns = False
                CajasDataGridView.DataSource = tCajas

                DistribucionTabControl.TabPages(0).Text = "Pendientes (" & tCajas.Rows.Count.ToString() & ")"
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarCajas", ex)
            End Try
        End Sub

        Private Sub CargarRemisiones(ByVal bPendientes As Boolean)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim nEntidad As Slyg.Tools.SlygNullable(Of Short)
                Dim nSede As Slyg.Tools.SlygNullable(Of Short)
                Dim nBoveda As Slyg.Tools.SlygNullable(Of Short)

                If CShort(EntidadRemisionDesktopComboBox.SelectedValue) = -1 Then
                    nEntidad = DBNull.Value
                Else
                    nEntidad = CShort(EntidadRemisionDesktopComboBox.SelectedValue)
                End If

                If CShort(SedeRemisionDesktopComboBox.SelectedValue) = -1 Then
                    nSede = DBNull.Value
                Else
                    nSede = CShort(SedeRemisionDesktopComboBox.SelectedValue)
                End If

                If CShort(BovedaRemisionDesktopComboBox.SelectedValue) = -1 Then
                    nBoveda = DBNull.Value
                Else
                    nBoveda = CShort(BovedaRemisionDesktopComboBox.SelectedValue)
                End If

                Dim tRemisiones As New DataTable

                If nEntidad.IsDbNull And nSede.IsDbNull And nBoveda.IsDbNull Then
                    If bPendientes Then
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Centro_Distribucion, _dsCentroDistribucion.Tables(5))
                    Else
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Enviado_a_custodia, _dsCentroDistribucion.Tables(5))
                    End If
                ElseIf Not nEntidad.IsDbNull And nSede.IsDbNull And nBoveda.IsDbNull Then
                    If bPendientes Then
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Centro_Distribucion & " AND fk_Entidad=" & nEntidad.ToString(), _dsCentroDistribucion.Tables(5))
                    Else
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Enviado_a_custodia & " AND fk_Entidad=" & nEntidad.ToString(), _dsCentroDistribucion.Tables(5))
                    End If
                ElseIf Not nEntidad.IsDbNull And Not nSede.IsDbNull And nBoveda.IsDbNull Then
                    If bPendientes Then
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Centro_Distribucion & " AND fk_Entidad=" & nEntidad.ToString() & "AND fk_Sede=" & nSede.ToString(), _dsCentroDistribucion.Tables(5))
                    Else
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Enviado_a_custodia & " AND fk_Entidad=" & nEntidad.ToString() & "AND fk_Sede=" & nSede.ToString(), _dsCentroDistribucion.Tables(5))
                    End If
                ElseIf Not nEntidad.IsDbNull And Not nSede.IsDbNull And Not nBoveda.IsDbNull Then
                    If bPendientes Then
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Centro_Distribucion & " AND fk_Entidad=" & nEntidad.ToString() & "AND fk_Sede=" & nSede.ToString() & " AND fk_Boveda=" & nBoveda.ToString(), _dsCentroDistribucion.Tables(5))
                    Else
                        tRemisiones = Utilities.SelectIntoDataTable("fk_Estado=" & DBCore.EstadoEnum.Enviado_a_custodia & " AND fk_Entidad=" & nEntidad.ToString() & "AND fk_Sede=" & nSede.ToString() & " AND fk_Boveda=" & nBoveda.ToString(), _dsCentroDistribucion.Tables(5))
                    End If
                End If

                RemisionesDesktopDataGridView.AutoGenerateColumns = False
                RemisionesDesktopDataGridView.DataSource = tRemisiones

                DistribucionTabControl.TabPages(1).Text = "Remisiones (" & tRemisiones.Rows.Count.ToString() & ")"
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarRemisiones", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CargarBusquedasPendientes(ByVal nEntidad As Short)
            Try
                If nEntidad = -1 Then
                    Dim dtEntidad = _dsCentroDistribucion.Tables(0)
                    Utilities.LlenarCombo(EntidadComboBox, dtEntidad, "id_Entidad", "Nombre_Entidad", True, , "-Todos-")
                End If

                Dim dtProyecto = Utilities.SelectIntoDataTable("fk_Entidad=" & EntidadComboBox.SelectedValue.ToString(), _dsCentroDistribucion.Tables(1))
                Utilities.LlenarCombo(ProyectoComboBox, dtProyecto, "id_Proyecto", "Nombre_Proyecto", True, , "-Todos-")

                CargarCajas()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarBusquedasPendientes", ex)
            End Try
        End Sub

        Private Sub CargarBusquedasRemisiones(ByVal nEntidad As Short, ByVal nSede As Short)
            Try
                If nEntidad = -1 Then
                    Dim dtEntidad = _dsCentroDistribucion.Tables(0)
                    Utilities.LlenarCombo(EntidadRemisionDesktopComboBox, dtEntidad, "id_Entidad", "Nombre_Entidad", True, , "-Todos-")
                End If

                If nSede = -1 Then
                    Dim dtSede = Utilities.SelectIntoDataTable("fk_Entidad=" & EntidadRemisionDesktopComboBox.SelectedValue.ToString(), _dsCentroDistribucion.Tables(2))
                    Utilities.LlenarCombo(SedeRemisionDesktopComboBox, dtSede, "id_Sede", "Nombre_Sede", True, , "-Todos-")
                End If


                If CShort(SedeRemisionDesktopComboBox.SelectedValue) = -1 Then
                    nSede = -1
                Else
                    nSede = CShort(SedeRemisionDesktopComboBox.SelectedValue)
                End If

                Dim dtBoveda = Utilities.SelectIntoDataTable("fk_Entidad=" & EntidadRemisionDesktopComboBox.SelectedValue.ToString() & " AND fk_Sede=" & nSede.ToString(), _dsCentroDistribucion.Tables(3))
                Utilities.LlenarCombo(BovedaRemisionDesktopComboBox, dtBoveda, "id_Boveda", "Nombre_Boveda", True, , "-Todos-")

                CargarRemisiones(PendientesDesktopCheckBox.Checked)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarBusquedasPendientes", ex)
            End Try
        End Sub
#End Region

    End Class

End Namespace