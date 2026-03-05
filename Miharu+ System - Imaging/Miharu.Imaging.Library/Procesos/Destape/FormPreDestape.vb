Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library

Namespace Procesos.Destape
    Public Class FormPreDestape

#Region " Declaraciones "

        Dim _cargaFechas As Boolean
        Dim _EventManager As EventManager

#End Region

#Region " Propiedades "

        Public Property EventManager As EventManager
            Get
                Return Me._EventManager
            End Get
            Set(value As EventManager)
                _EventManager = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub CrearOTButton_Click(sender As System.Object, e As EventArgs) Handles CrearOTButton.Click
            Dim fDestape As New FormDestape
            fDestape.IdOT = CInt(OTComboBox.SelectedValue)
            fDestape.EventManager = _EventManager
            fDestape.ShowDialog()

            Buscar()
        End Sub

        Private Sub AbrirOTButton_Click(sender As System.Object, e As EventArgs) Handles AbrirOTButton.Click
            AbrirOT()
        End Sub

        Private Sub PrecintosDataGridView_CellContentDoubleClick(sender As System.Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles PrecintosDataGridView.CellContentDoubleClick
            AbrirOT()
        End Sub

        Private Sub FormPreDestape_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Me.CorrecionDestapeButton.Visible = Program.PluginManager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.CorreccionDestape)
            Me.MasivoDestapeButton.Visible = Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Masivo

            CargarFechas()
        End Sub

        Private Sub FechaProcesoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles FechaProcesoComboBox.SelectedIndexChanged
            CargarOT()
        End Sub

        Private Sub BuscarButton_Click(sender As System.Object, e As EventArgs) Handles BuscarButton.Click
            Buscar()
        End Sub

        Private Sub CorrecionDestape_Click(sender As System.Object, e As System.EventArgs) Handles CorrecionDestapeButton.Click
            Dim fDestapeCorreccion As New FormDestapeCorreccion
            fDestapeCorreccion.IdOT = CInt(OTComboBox.SelectedValue)
            fDestapeCorreccion.ShowDialog()
        End Sub

        Private Sub MasivoDestapeButton_Click(sender As System.Object, e As System.EventArgs) Handles MasivoDestapeButton.Click
            Dim fDestapeMasivo As New FormDestapeMasivo
            fDestapeMasivo.IdOT = CInt(OTComboBox.SelectedValue)
            fDestapeMasivo.EventManager = Me._EventManager
            fDestapeMasivo.ShowDialog()
        End Sub
#End Region

#Region " Funciones "

        Private Sub CargarFechas()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                _cargaFechas = True
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim fechaProcesoData = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_ProyectoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, 0, New DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnumList(DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnum.Fecha_Proceso, False))
                '(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing, 0, New DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnumList(DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnum.Fecha_Proceso, False))
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
                        CorrecionDestapeButton.Enabled = True
                        MasivoDestapeButton.Enabled = True
                    Else
                        CrearOTButton.Enabled = False
                        AbrirOTButton.Enabled = False
                        CorrecionDestapeButton.Enabled = False
                        MasivoDestapeButton.Enabled = False
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

                Dim PrecintoData = dbmImaging.SchemaProcess.PA_Obtiene_Precintos.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(OTComboBox.SelectedValue))
                PrecintosDataGridView.DataSource = PrecintoData
                PrecintosDataGridView.Refresh()

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub AbrirOT()
            Dim rowsSelected = PrecintosDataGridView.SelectedCells

            If (rowsSelected.Count > 0) Then
                Dim fDestape As New FormDestape
                fDestape.EventManager = _EventManager
                fDestape.IdOT = CInt(OTComboBox.SelectedValue)
                fDestape.IdPrecinto = CShort(PrecintosDataGridView(4, rowsSelected(0).RowIndex).Value)
                fDestape.ShowDialog()
                Buscar()
            Else
                MessageBox.Show("Por favor seleccione un precinto", "Destape")
            End If
        End Sub

#End Region


        
    End Class

End Namespace