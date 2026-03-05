Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Procesos.Destape
    Public Class FormCrearOT
        Inherits Form

#Region " Propiedades "

        Public Property FechaProceso() As Integer

        Public Property NewOT() As Integer

#End Region

#Region " Eventos "

        Private Sub FormCrearOT_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarTiposOT()

            Me.lineaProcesoLabel.Visible = Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico
            Me.lineaProcesoTextBox.Visible = Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            GuardarOT()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarTiposOT()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim TipoOTData = dbmImaging.SchemaProcess.TBL_OT_Tipo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)
                TipoOTComboBox.Fill(TipoOTData, TipoOTData.id_OT_TipoColumn, TipoOTData.Nombre_OT_TipoColumn)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally                
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarOT()
            If (Validar) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                'Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    'dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                    'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    dbmImaging.Transaction_Begin()

                    Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesofk_OT_Tipofk_Sede_Procesamiento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Me.FechaProceso, CShort(TipoOTComboBox.SelectedValue), Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)

                    If (OTDataTable.Count < Program.ImagingGlobal.ProyectoImagingRow.Cantidad_Maxima_OTxTipo) Or (Program.ImagingGlobal.ProyectoImagingRow.Cantidad_Maxima_OTxTipo = 0) Then
                        Dim OTType As New DBImaging.SchemaProcess.TBL_OTType
                        With OTType
                            .fk_Entidad = Program.ImagingGlobal.Entidad
                            .fk_Proyecto = Program.ImagingGlobal.Proyecto
                            .fk_fecha_proceso = Me.FechaProceso
                            .id_OT = dbmImaging.SchemaProcess.TBL_OT.DBNextId()
                            .fk_OT_Tipo = CShort(TipoOTComboBox.SelectedValue)
                            .fk_Entidad_Procesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad
                            .fk_Sede_Procesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede
                            .fk_Centro_Procesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento
                            .fk_Usuario_Apertura = Program.Sesion.Usuario.id
                            .Fecha_Apertura = SlygNullable.SysDate
                            .Cerrado = False
                        End With

                        If (Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico) Then
                            OTType.fk_Linea_Proceso = Integer.Parse(Me.lineaProcesoTextBox.Text)

                            'Dim LineaDatatable = dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBFindByid_Linea_Procesofk_Entidad_Clientefk_Proyecto(Integer.Parse(Me.lineaProcesoTextBox.Text), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

                            'If LineaDatatable.Count <= 0 Then
                            '    Throw New Exception("La linea no pertenece a la entidad y proyecto seleccionado, favor validar.")
                            'End If

                        End If

                        dbmImaging.SchemaProcess.TBL_OT.DBInsert(OTType)

                        If (Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico) Then
                            dbmImaging.SchemaProcess.PA_Crear_Destape_Por_OT.dbExecute(OTType.id_OT)
                        End If
                        Me.NewOT = OTType.id_OT.Value
                    Else
                        Throw New Exception("Ya se creó el número máximo de OTs de este tipo para esta fecha de proceso en esta sede")
                    End If

                    dbmImaging.Transaction_Commit()

                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (Not Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico) Then Return True

            If (Not DataConvert.IsNumeric(Me.lineaProcesoTextBox.Text)) Then
                MessageBox.Show("La línea de proceso debe ser un valor numérico", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing

            Try
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim lineaProceso = Integer.Parse(Me.lineaProcesoTextBox.Text)
                Dim lineaProcesoTable = dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBGet(lineaProceso)

                If (lineaProcesoTable.Count = 0) Then
                    MessageBox.Show("La línea de proceso no existe", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                Dim lineaProcesoRow = lineaProcesoTable(0)

                If (lineaProcesoRow.Cargado_Imaging) Then
                    MessageBox.Show("La línea de proceso ya fue cargada", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                If (lineaProcesoRow.fk_Entidad_Cliente <> Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad Or
                    lineaProcesoRow.fk_Proyecto <> Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto) Then
                    MessageBox.Show("La línea de proceso no corresponde al proyecto actual", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                Return True
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

        End Function

#End Region

    End Class

End Namespace