Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Procesos.Destape

    Public Class FormOT
        Inherits Form

#Region " Declaraciones "

        Private _pv As Boolean

#End Region

#Region " Propiedades "

        Public Property WorkSpace() As FormImagingWorkSpace

#End Region

#Region " Eventos "

        Private Sub FormOT_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarFechas()
            CargarOTs()

            If FechaProcesoComboBox.Items.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se encuentran fechas de proceso para crear OTs", "OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return
            End If
        End Sub

        Private Sub FechaProcesoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles FechaProcesoComboBox.SelectedValueChanged
            CargarOTs()
        End Sub

        Private Sub CrearOTButton_Click(sender As System.Object, e As EventArgs) Handles CrearOTButton.Click
            Dim fCrearOT As New FormCrearOT
            fCrearOT.FechaProceso = CInt(FechaProcesoComboBox.SelectedValue)

            If fCrearOT.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CargarOTs()
                Me.WorkSpace.EventManager.CrearOT(fCrearOT.NewOT)
            End If
        End Sub

        Private Sub CerrarOTButton_Click(sender As System.Object, e As EventArgs) Handles CerrarOTButton.Click
            If FechaProcesoDataGridView.SelectedCells.Count > 0 Then

                If DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro de cerrar las OTs seleccionadas?", "Cerrar OT", DesktopMessageBoxControl.IconEnum.WarningIcon, True) <> DialogResult.OK Then
                    Return
                End If

                Dim OTSeleccionada = FechaProcesoDataGridView.Rows(FechaProcesoDataGridView.SelectedCells(0).RowIndex)
                Dim idOT = CInt(OTSeleccionada.Cells("id_OT").Value)


                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    ''--=============== INICIO REQUERIMIENTO RITM0229763 ===========-
                    'Dim ValidaPrepararDataCruce As String
                    'If Program.ImagingGlobal.ProyectoImagingRow.Usa_Cruce_Generico Then
                    '    ValidaPrepararDataCruce = dbmImaging.SchemaProcess.PA_Get_Prepara_Data_Cruce.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, 0, idOT, 3)
                    '    If (ValidaPrepararDataCruce <> "") Then
                    '        MessageBox.Show("¡" & ValidaPrepararDataCruce & "!", "Preparar Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Return
                    '    End If
                    'End If
                    ''--=============== FIN REQUERIMIENTO RITM0229763 ===========-

                    dbmImaging.SchemaProcess.PA_Cerrar_OT.DBExecute(idOT, Program.Sesion.Usuario.id)
                    Dim DataOt = dbmImaging.SchemaProcess.TBL_OT.DBGet(idOT)
                    If DataOt(0).Cerrado = True Then
                        Me.WorkSpace.EventManager.CerrarOt(idOT)

                        If Program.ImagingGlobal.ProyectoImagingRow.Notificacion_Cierre_OT Then
                            If DesktopMessageBoxControl.DesktopMessageShow("Desea enviar el correo con el informe de publicacioíon?", "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon) = DialogResult.OK Then
                                Try
                                    dbmImaging.SchemaProcess.PA_Notificacion_Cierre_OT.DBExecute(idOT, Program.Sesion.Usuario.id)
                                Catch ex As Exception
                                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                End Try

                            End If
                        End If

                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No se puede cerrar la OT porque tiene documentos en proceso.", "Cerrar OT", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
                CargarOTs()
            End If
        End Sub

        Private Sub FechaProcesoDataGridView_CellEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FechaProcesoDataGridView.CellEnter
            Try
                If Convert.ToBoolean(FechaProcesoDataGridView.Rows(e.RowIndex).Cells("CERRADO").Value) = True Then
                    CerrarOTButton.Enabled = False
                Else
                    CerrarOTButton.Enabled = True
                End If

            Catch ex As Exception

            End Try
        End Sub

        Private Sub AbrirOTButton_Click(sender As System.Object, e As EventArgs) Handles AbrirOTButton.Click
            ReabrirOT()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarOTs()
            If Not _pv Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim OTData = dbmImaging.SchemaProcess.PA_Obtiene_OTs.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesoComboBox.SelectedValue), Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede, Program.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento)

                    FechaProcesoDataGridView.DataSource = OTData
                    FechaProcesoDataGridView.Refresh()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub CargarFechas()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                _pv = True
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim fechaProcesoData = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_ProyectoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, 0, New DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnumList(DBImaging.SchemaProcess.TBL_Fecha_ProcesoEnum.Fecha_Proceso, False))
                FechaProcesoComboBox.Fill(fechaProcesoData, fechaProcesoData.id_fecha_procesoColumn, fechaProcesoData.Fecha_ProcesoColumn)

                _pv = False
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub ReabrirOT()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            If FechaProcesoDataGridView.SelectedCells.Count > 0 Then
                If DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro de reabrir la OT?", "Cerrar OT", DesktopMessageBoxControl.IconEnum.WarningIcon, True) <> DialogResult.OK Then
                    Return
                End If

                Dim Row = FechaProcesoDataGridView.Rows(FechaProcesoDataGridView.SelectedCells(0).RowIndex)
                Dim idOT = Row.Value(Of Integer)("id_OT")
                Dim FechaProceso = Row.Value(Of Integer)("fk_Fecha_Proceso")

                Try
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    dbmImaging.SchemaProcess.PA_Reabrir_OT.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, idOT)

                    Me.WorkSpace.EventManager.AbrirOT(idOT)
                    Me.WorkSpace.EventManager.AbrirFechaProceso(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, FechaProceso)

                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                CargarOTs()
            End If
        End Sub

#End Region


    End Class

End Namespace