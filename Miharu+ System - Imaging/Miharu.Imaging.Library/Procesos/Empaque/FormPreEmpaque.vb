Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Config

Namespace Procesos.Empaque

    Public Class FormPreEmpaque

#Region " Propiedades "

        Public Property EventManager As EventManager

#End Region

#Region " Declaraciones "

        Dim _cargaFechas As Boolean

#End Region

#Region " Eventos "

        Private Sub CrearOTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CrearOTButton.Click
            Dim fEmpaque As New FormEmpaque
            fEmpaque.IdOT = CInt(OTComboBox.SelectedValue)
            fEmpaque.EventManager = Me._EventManager
            fEmpaque.ShowDialog()

            Buscar()
        End Sub

        Private Sub AbrirOTButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AbrirOTButton.Click
            AbrirOT()

        End Sub

        Private Sub FormPreEmpaque_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarFechas()
        End Sub

        Private Sub FechaProcesoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles FechaProcesoComboBox.SelectedIndexChanged
            CargarOT()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub EmpaqueDataGridView_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As Windows.Forms.DataGridViewCellEventArgs) Handles EmpaqueDataGridView.CellContentDoubleClick
            AbrirOT()
        End Sub

#End Region

#Region " Funciones "

        Private Sub CargarFechas()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                _cargaFechas = True
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim fechaProcesoData = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_ProyectoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, 0, New DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnumList(DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnum.Fecha_Proceso, False))
                FechaProcesoComboBox.Fill(fechaProcesoData, fechaProcesoData.id_fecha_procesoColumn, fechaProcesoData.Fecha_ProcesoColumn)

                _cargaFechas = False
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                CargarOT()
            End Try
        End Sub

        Private Sub CargarOT()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not _cargaFechas) Then
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim OTData = dbmImaging.SchemaProcess.CTA_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_procesofk_Sede_Procesamientofk_Centro_ProcesamientoCerrado(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, _
                                                                                                                                                                                          Program.ImagingGlobal.Entidad, _
                                                                                                                                                                                          Program.ImagingGlobal.Proyecto, _
                                                                                                                                                                                          CInt(FechaProcesoComboBox.SelectedValue), _
                                                                                                                                                                                          Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede, _
                                                                                                                                                                                          Program.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento, _
                                                                                                                                                                                          False)
                    OTComboBox.Fill(OTData, OTData.id_OTColumn, OTData.DescripcionColumn)

                    If (OTData.Rows.Count > 0) Then
                        CrearOTButton.Enabled = True
                        AbrirOTButton.Enabled = True
                    Else
                        CrearOTButton.Enabled = False
                        AbrirOTButton.Enabled = False
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Buscar()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim empaqueData = dbmImaging.SchemaProcess.PA_Obtiene_Empaques.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(OTComboBox.SelectedValue))
                EmpaqueDataGridView.DataSource = empaqueData
                EmpaqueDataGridView.Refresh()

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub AbrirOT()
            Dim rowsSelected = EmpaqueDataGridView.SelectedCells

            If (rowsSelected.Count > 0) Then
                Dim fEmpaque As New FormEmpaque
                fEmpaque.IdOT = CInt(OTComboBox.SelectedValue)
                fEmpaque.IdEmpaque = CShort(EmpaqueDataGridView(4, rowsSelected(0).RowIndex).Value)
                fEmpaque.EventManager = Me._EventManager
                fEmpaque.ShowDialog()
            Else
                MessageBox.Show("Por favor seleccione un precinto", "Destape")
            End If

            Buscar()
        End Sub

#End Region

    End Class

End Namespace