Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopCBarras
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports System.Text

Namespace Forms.MesaControlFisicos

    Public Class FormMesaControlFisicosDeceval
        Inherits FormBase

#Region " Declaraciones "

        Private _CBarrasFolder As String
        Private _TipoCaptura As DesktopConfig.TipoCaptura
        Dim TableFolder As DBArchiving.Schemadbo.CTA_Files_Mesa_ControlDataTable = Nothing

#End Region

#Region " Constructor "

        Sub New(ByVal CBarrasFolder As String, ByVal TipoCaptura As DesktopConfig.TipoCaptura)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)
            _CBarrasFolder = CBarrasFolder
            _TipoCaptura = TipoCaptura
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormMesaControlFisicos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                'LineaProcesoLabel.Text = Program.Sesion.Parameter("_idLineaProceso").ToString()
                CBarrasFolderLabel.Text = _CBarrasFolder
                EntidadLabel.Text = Program.RiskGlobal.NombreEntidad
                ProyectoLabel.Text = Program.RiskGlobal.NombreProyecto

                CargaFiles()
                'DesktopDataGridViewNovedades.Visible = False
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormMesaControlFisicos_Load", ex)
            End Try
        End Sub

        Private Sub ProcesarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProcesarButton.Click
            Procesar()
        End Sub

        Private Sub CerrarCarpetaButton_Click_1(sender As System.Object, e As System.EventArgs) Handles CerrarCarpetaButton.Click
            CerrarCarpeta2()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub cbarrasDesktopCBarrasControl_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
            If e.KeyCode = Keys.Enter Then
                If CType(sender, DesktopCBarrasControl).Text = "" Then
                    cbarrasDesktopCBarrasControl_KeyDown(sender, New KeyEventArgs(Keys.Tab))
                Else
                    ProcesarButton.Focus()
                End If
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaFiles()
            Try
                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim nEstado As Short
                Select Case _TipoCaptura
                    Case DesktopConfig.TipoCaptura.Primera_Captura : nEstado = DBCore.EstadoEnum.Mesa_de_Control_Deceval
                    Case DesktopConfig.TipoCaptura.Segunda_Captura : nEstado = DBCore.EstadoEnum.Segunda_Captura
                    Case DesktopConfig.TipoCaptura.Tercera_Captura : nEstado = DBCore.EstadoEnum.Tercera_Captura
                End Select


                'TableFolder = dbmArchiving.Schemadbo.CTA_Files_Mesa_Control.DBFindByfk_Entidadfk_Proyectoid_Folder_Estadofk_Linea_Proceso(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, nEstado, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()))

                'If TableFolder.Count > 0 Then
                '    Program.Sesion.Parameter("_idLineaProceso") = TableFolder.Rows(0)(TableFolder.fk_Linea_ProcesoColumn).ToString()
                '    'Program.Sesion.Parameter("fk_Expediente") = TableFolder.Rows(0)(TableFolder.fk_ExpedienteColumn).ToString()
                'End If

                If Program.RiskGlobal.Usa_Validacion_Destape Then
                    TableFolder = dbmArchiving.Schemadbo.CTA_Files_Mesa_Control.DBFindByfk_Entidadfk_ProyectoCBarras_Folderid_Folder_Estadofk_Linea_Proceso(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _CBarrasFolder, nEstado, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()))
                Else
                    'TableFolder = dbmArchiving.Schemadbo.CTA_Files_Mesa_Control.DBFindByfk_Entidadfk_ProyectoCBarras_Folderfk_Linea_Procesoid_File_Estado(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _CBarrasFolder, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()), nEstado)
                    TableFolder = dbmArchiving.Schemadbo.PA_Get_Files_Mesa_Control_Reproceso.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _CBarrasFolder, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()), nEstado)
                End If

                dbmArchiving.Connection_Close()

                If TableFolder.Count > 0 And Program.RiskGlobal.Usa_Validacion_Destape Then
                    FilesDesktopDataGridView.AutoGenerateColumns = False
                    FilesDesktopDataGridView.DataSource = TableFolder

                    'Se cargan los files de la carpeta y se les aplica color a las filas dependiendo del estado.
                    For Each row As DataGridViewRow In FilesDesktopDataGridView.Rows

                        Select Case _TipoCaptura
                            Case DesktopConfig.TipoCaptura.Primera_Captura
                                If CInt(row.Cells("id_estado").Value) = 30 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) >= 0 And CInt(row.Cells("id_estado").Value) <= 29 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.NoAplica
                                ElseIf CInt(row.Cells("id_estado").Value) > 30 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.ProcesoSuperior : End If

                            Case DesktopConfig.TipoCaptura.Segunda_Captura
                                If CInt(row.Cells("id_estado").Value) = 33 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) >= 0 And CInt(row.Cells("id_estado").Value) <= 32 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.NoAplica
                                ElseIf CInt(row.Cells("id_estado").Value) = 34 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) > 33 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.ProcesoSuperior : End If

                            Case DesktopConfig.TipoCaptura.Tercera_Captura
                                If CInt(row.Cells("id_estado").Value) = 35 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) >= 0 And CInt(row.Cells("id_estado").Value) <= 34 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.NoAplica
                                ElseIf CInt(row.Cells("id_estado").Value) > 35 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.ProcesoSuperior : End If
                        End Select

                        row.Selected = False
                    Next

                    EsquemaLabel.Text = TableFolder.Rows(0)(TableFolder.Nombre_esquemaColumn).ToString()


                ElseIf TableFolder.Count > 0 And Not Program.RiskGlobal.Usa_Validacion_Destape Then

                    FilesDesktopDataGridView.AutoGenerateColumns = False
                    FilesDesktopDataGridView.DataSource = TableFolder

                    'Se cargan los files de la carpeta y se les aplica color a las filas dependiendo del estado.
                    For Each row As DataGridViewRow In FilesDesktopDataGridView.Rows

                        Select Case _TipoCaptura
                            Case DesktopConfig.TipoCaptura.Primera_Captura
                                If CInt(row.Cells("id_estado").Value) = 30 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) >= 0 And CInt(row.Cells("id_estado").Value) <= 29 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.NoAplica
                                ElseIf CInt(row.Cells("id_estado").Value) > 30 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.ProcesoSuperior : End If

                            Case DesktopConfig.TipoCaptura.Segunda_Captura
                                If CInt(row.Cells("id_estado").Value) = 33 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) >= 0 And CInt(row.Cells("id_estado").Value) <= 32 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.NoAplica
                                ElseIf CInt(row.Cells("id_estado").Value) = 34 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) > 33 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.ProcesoSuperior : End If

                            Case DesktopConfig.TipoCaptura.Tercera_Captura
                                If CInt(row.Cells("id_estado").Value) = 35 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.PendienteProcesar
                                ElseIf CInt(row.Cells("id_estado").Value) >= 0 And CInt(row.Cells("id_estado").Value) <= 34 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.NoAplica
                                ElseIf CInt(row.Cells("id_estado").Value) > 35 Then : row.DefaultCellStyle.BackColor = DesktopConfig.ColoresEstado.ProcesoSuperior : End If
                        End Select

                        row.Selected = False
                    Next

                    EsquemaLabel.Text = TableFolder.Rows(0)(TableFolder.Nombre_esquemaColumn).ToString()

                ElseIf TableFolder.Count <= 0 And Not Program.RiskGlobal.Usa_Validacion_Destape Then
                    CerrarCarpeta2()
                    DesktopMessageBoxControl.DesktopMessageShow("La carpeta o el documento no se encuentran en estado " & _TipoCaptura.ToString().Replace("_", " ") & " o no perteneces a la linea de proceso [" + Program.Sesion.Parameter("_idLineaProceso").ToString() + "], por tanto no se puede procesar.", "Estado de carpeta - documento no válido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Me.Close()
                ElseIf TableFolder.Count <= 0 And Program.RiskGlobal.Usa_Validacion_Destape Then
                    DesktopMessageBoxControl.DesktopMessageShow("La carpeta o el documento no se encuentran en estado " & _TipoCaptura.ToString().Replace("_", " ") & " o no perteneces a la linea de proceso [" + Program.Sesion.Parameter("_idLineaProceso").ToString() + "], por tanto no se puede procesar.", "Estado de carpeta - documento no válido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Me.Close()
                End If


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiles", ex)
            End Try
        End Sub

        Private Sub Procesar()
            Try
                Dim dtFilesMesa As New DataView(Utilities.ClonarDataTable(CType(FilesDesktopDataGridView.DataSource, DataTable)))
                Dim idEstadoActual As DBCore.EstadoEnum
                Dim nombreEstadoActual As String = ""

                If cbarrasDesktopCBarrasControl.Text <> "" And dtFilesMesa.Count > 0 Then
                    If dtFilesMesa.Count > 0 Then
                        _TipoCaptura = CType(1, DesktopConfig.TipoCaptura)
                        Select Case _TipoCaptura
                            Case DesktopConfig.TipoCaptura.Primera_Captura
                                dtFilesMesa.RowFilter = "CBarras_File = '" & cbarrasDesktopCBarrasControl.Text & "'" : idEstadoActual = DBCore.EstadoEnum.Mesa_de_Control_Deceval : nombreEstadoActual = DBCore.EstadoEnum.Mesa_de_Control_Deceval.ToString()
                            Case DesktopConfig.TipoCaptura.Segunda_Captura
                                dtFilesMesa.RowFilter = "CBarras_File = '" & cbarrasDesktopCBarrasControl.Text & "' AND id_Estado=" & CStr(DBCore.EstadoEnum.Segunda_Captura) : idEstadoActual = DBCore.EstadoEnum.Segunda_Captura : nombreEstadoActual = DBCore.EstadoEnum.Segunda_Captura.ToString()
                            Case DesktopConfig.TipoCaptura.Tercera_Captura
                                dtFilesMesa.RowFilter = "CBarras_File = '" & cbarrasDesktopCBarrasControl.Text & "' AND id_Estado=" & CStr(DBCore.EstadoEnum.Tercera_Captura) : idEstadoActual = DBCore.EstadoEnum.Tercera_Captura : nombreEstadoActual = DBCore.EstadoEnum.Tercera_Captura.ToString()
                        End Select

                        If dtFilesMesa.ToTable().Rows.Count > 0 Then

                            'Se valida si es una devolución
                            If CShort(dtFilesMesa(0).Item("fk_Registro_Tipo")) = DesktopConfig.RegistroTipo.Devolucion Then
                                FormPrimeraCapturarDataAdicionalDevolucion.Captura(cbarrasDesktopCBarrasControl.Text)
                            Else
                                Select Case _TipoCaptura
                                    Case DesktopConfig.TipoCaptura.Primera_Captura : FormPrimeraCapturarDataAdicionalDeceval.Captura(cbarrasDesktopCBarrasControl.Text)
                                    Case DesktopConfig.TipoCaptura.Segunda_Captura : FormPrimeraCapturarDataAdicionalDeceval.Captura(cbarrasDesktopCBarrasControl.Text)
                                    Case DesktopConfig.TipoCaptura.Tercera_Captura : FormPrimeraCapturarDataAdicionalDeceval.Captura(cbarrasDesktopCBarrasControl.Text)
                                End Select
                            End If

                            CargaFiles()
                            cbarrasDesktopCBarrasControl.Text = ""
                            cbarrasDesktopCBarrasControl.Focus()
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("El documento no se encuentra en estado " & nombreEstadoActual.Replace("_", " ") & " o no se encuentra en la linea de proceso [" & Program.Sesion.Parameter("_idLineaProceso").ToString() & "]", "Error en código de barras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                            cbarrasDesktopCBarrasControl.Text = ""
                            cbarrasDesktopCBarrasControl.Focus()
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un código de barras que se encuentre en la carpeta seleccionada.", "Error en código de barras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        cbarrasDesktopCBarrasControl.Focus()
                    End If
                Else
                    'CerrarCarpetaButton.Focus()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Procesar", ex)
            End Try
        End Sub

#End Region
        Private Sub CerrarCarpeta2()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim Resultados = dbmArchiving.Schemadbo.PA_Mesa_Cierre_Carpeta.DBExecute(_CBarrasFolder, _TipoCaptura, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()), Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

                If (Resultados.Rows.Count = 0) Then
                    'Si no actualiza ningun estado de carpeta
                    DesktopMessageBoxControl.DesktopMessageShow("No es posible cerrar la Carpeta, ya que existen documentos que no se han procesado.", "Cerrar Carpeta", DesktopMessageBoxControl.IconEnum.WarningIcon, True)

                Else
                    'Si actualiza al menos una OT de la carpeta
                    Dim Mensaje As New Text.StringBuilder
                    Mensaje.AppendLine("Se cerraron las siguientes OTs de la carpeta")
                    Mensaje.AppendLine()

                    For Each Row As DataRow In Resultados.Rows
                        Mensaje.AppendLine("[OT " & Row("fk_OT").ToString() & "], [Estado " & Row("Nombre_Estado").ToString() & "]")
                    Next

                    DesktopMessageBoxControl.DesktopMessageShow(Mensaje.ToString(), "Cierre Carpeta", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    CerrarCarpetaButton.Enabled = False : Me.Close()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarCarpeta", ex)
            End Try

            dbmArchiving.Connection_Close()
        End Sub
    End Class

End Namespace