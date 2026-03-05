Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Risk.Library.Forms.LineaProceso
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports Miharu.Desktop.Controls.DesktopCBarras

Namespace Forms.MesaControlFisicos

    Public Class FormPistolearCarpeta
        Inherits FormBase

#Region " Declaraciones "

        'Private TablePrecintos As DataTable
        Private _TipoCaptura As DesktopConfig.TipoCaptura

#End Region

#Region " Constructor "

        Sub New(ByVal TipoCaptura As DesktopConfig.TipoCaptura)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TipoCaptura = TipoCaptura

            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormPistolearCarpeta_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim formLineaProceso As New FormSeleccionarLineaProceso(_TipoCaptura)
            Dim Respuesta As DialogResult = formLineaProceso.ShowDialog()

            If Respuesta = DialogResult.OK Then
                LineaProcesoLabel.Text = Program.Sesion.Parameter("_idLineaProceso").ToString()
            ElseIf Respuesta = DialogResult.No Then
                'No se puede trabajar sin línea de proceso.
                Me.Close()
            End If
        End Sub

        Private Sub CBarrasDesktopTextBox_LostFocus(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbarrasDesktopCBarrasControl.LostFocus
            BuscarCBarras()
        End Sub

        Private Sub SiguienteButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SiguienteButton.Click
            OpenValidacionLlaves()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub FormPistolearCarpeta_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            CerrarLineaProceso()
        End Sub

        Private Sub cbarrasDesktopCBarrasControl_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
            If e.KeyCode = Keys.Enter Then
                If CType(sender, DesktopCBarrasControl).Text = "" Then
                    CerrarButton.Focus()
                Else
                    SiguienteButton.Focus()
                End If
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub BuscarCBarras()
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Try
                If cbarrasDesktopCBarrasControl.Text <> "" Then
                    dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    Dim TableLlaves = dbmArchiving.Schemadbo.CTA_CBarras_Llaves.DBFindBycodigofk_entidadfk_proyecto(cbarrasDesktopCBarrasControl.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                    If TableLlaves.Rows.Count = 0 Then
                        LlavesDesktopDataGridView.AutoGenerateColumns = False
                        LlavesDesktopDataGridView.DataSource = Nothing
                        DesktopMessageBoxControl.DesktopMessageShow("El código de barras buscado no pertenece a esta linea de proceso o no se encuentra en la base de datos.", "Código de barras no encontrado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        SiguienteButton.Enabled = False
                        cbarrasDesktopCBarrasControl.Text = String.Empty
                        cbarrasDesktopCBarrasControl.Focus()
                    Else
                        LlavesDesktopDataGridView.AutoGenerateColumns = False
                        LlavesDesktopDataGridView.DataSource = TableLlaves
                        SiguienteButton.Enabled = True
                        SiguienteButton.Focus()
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarCBarras", ex)
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try
        End Sub

        Public Sub OpenValidacionLlaves()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim CBarrasDocTable = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByCBarras_File(cbarrasDesktopCBarrasControl.Text)
            dbmArchiving.Connection_Close()
            Dim CBarrasDoc As String = ""

            Try
                If CBarrasDocTable.Rows.Count = 0 Then
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    CBarrasDocTable = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByCBarras_Folderfk_Linea_Proceso(cbarrasDesktopCBarrasControl.Text, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()))
                    dbmArchiving.Connection_Close()

                    If CBarrasDocTable.Rows.Count > 0 Then
                        CBarrasDoc = CBarrasDocTable.Rows(0)(CBarrasDocTable.CBarras_FolderColumn).ToString()
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El código digitado no existe en base de datos o se encuentra en una linea de proceso diferente a la actual.", "Código de barras inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If
                ElseIf CBarrasDocTable.Rows.Count > 0 Then
                    CBarrasDoc = CBarrasDocTable.Rows(0)(CBarrasDocTable.CBarras_FolderColumn).ToString()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El código digitado no existe en base de datos, se encuentra en una linea de proceso diferente a la actual o pertenece a otro proyecto.", "Código de barras inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If

                If CBarrasDoc <> "" Then
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    If (Program.RiskGlobal.Usa_Empaque_Adicion) Then
                        Dim Validado As DataTable = dbmArchiving.Schemadbo.PA_Validacion_CBarras_Nuevo.DBExecute(cbarrasDesktopCBarrasControl.Text, CInt(Program.Sesion.Parameter("_idLineaProceso").ToString()))
                        dbmArchiving.Connection_Close()

                        If CInt(Validado.Rows(0)("Tipo_Registro")) = 3 Then
                            If CInt(Validado.Rows(0)("Validado")) = 1 Then
                                Dim formMesa As New FormMesaControlFisicos(CBarrasDoc, _TipoCaptura)
                                formMesa.ShowDialog()
                            ElseIf CInt(Validado.Rows(0)("Validado")) = 0 Then
                                Dim FormCodigoVsLlaves As New FormCodigoVsLlaves(CBarrasDoc)
                                FormCodigoVsLlaves.ShowDialog()
                            End If
                        ElseIf CInt(Validado.Rows(0)("Tipo_Registro")) = 1 Then
                            Dim formMesa As New FormMesaControlFisicos(CBarrasDoc, _TipoCaptura)
                            formMesa.ShowDialog()
                        ElseIf CInt(Validado.Rows(0)("Tipo_Registro")) = 2 Then
                            Dim formMesa As New FormMesaControlFisicos(CBarrasDoc, _TipoCaptura)
                            formMesa.ShowDialog()
                        ElseIf CInt(Validado.Rows(0)("Tipo_Registro")) = 0 Then
                            DesktopMessageBoxControl.DesktopMessageShow("El código de barras digitado no pertenece a la linea seleccionada.", "Código de barras inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        End If
                    Else
                        Dim Validados = dbmArchiving.Schemadbo.CTA_Folder_llaves.DBFindByValidacion_CBarraCBarras_folder(True, CBarrasDoc)
                        dbmArchiving.Connection_Close()
                        'Se valida si las llaves la fueron validadas.
                        If Validados.Rows.Count = 0 Then
                            Dim FormCodigoVsLlaves As New FormCodigoVsLlaves(CBarrasDoc)
                            FormCodigoVsLlaves.ShowDialog()
                        Else
                            Dim formMesa As New FormMesaControlFisicos(CBarrasDoc, _TipoCaptura)
                            formMesa.ShowDialog()
                        End If
                    End If
                   

                    LlavesDesktopDataGridView.DataSource = Nothing
                    cbarrasDesktopCBarrasControl.Focus()
                    cbarrasDesktopCBarrasControl.SelectAll()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("OpenValidacionLlaves", ex)
            End Try
        End Sub

        Private Sub CerrarLineaProceso()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim Estado As New DBCore.EstadoEnum

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                'Si la línea no tiene asociado ningún file se elimina, de lo contrario la procesa.
                Dim dtFilesLineaProceso = dbmArchiving.Schemadbo.CTA_Folder_File.DBFindByfk_Linea_Proceso(CInt(Program.Sesion.Parameter("_idLineaProceso")))

                If dtFilesLineaProceso.Count = 0 Then
                    dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBDelete(CInt(Program.Sesion.Parameter("_idLineaProceso")))
                    Program.Sesion.Parameter("_idLineaProceso") = Nothing
                Else
                    If DesktopMessageBoxControl.DesktopMessageShow("¿Desea cerrar la línea de proceso: [" & Program.Sesion.Parameter("_idLineaProceso").ToString() & "]?", "Cerrar línea de proceso", DesktopMessageBoxControl.IconEnum.WarningIcon) = DialogResult.OK Then
                        Select Case _TipoCaptura
                            Case DesktopConfig.TipoCaptura.Primera_Captura : Estado = DBCore.EstadoEnum.Mesa_de_Control
                            Case DesktopConfig.TipoCaptura.Segunda_Captura : Estado = DBCore.EstadoEnum.Segunda_Captura
                            Case DesktopConfig.TipoCaptura.Tercera_Captura : Estado = DBCore.EstadoEnum.Tercera_Captura
                        End Select

                        'Para realizar el cierre no debe existir ningún item en el mismo estado.
                        Dim viewFilesLineaProceso As DataView = dtFilesLineaProceso.DefaultView
                        'viewFilesLineaProceso.RowFilter = "fk_Estado_File=" & CStr(Estado)
                        viewFilesLineaProceso.RowFilter = "fk_estado=" & CStr(Estado) & " OR fk_Estado_File=" & CStr(Estado)

                        If viewFilesLineaProceso.Count > 0 Then
                            'Crea mensaje con la relación de documentos pendientes.
                            Dim mensaje As New StringBuilder()
                            mensaje.AppendLine("CARPETA - DOCUMENTO")
                            For Each row As DataRow In viewFilesLineaProceso.ToTable(True).Rows
                                mensaje.AppendLine(row("CBarras_Folder").ToString() & " - " & row("CBarras_File").ToString())
                            Next

                            DesktopMessageBoxControl.DesktopMessageShow("No se puede cerrar la línea de proceso, ya que aún existen items pendientes de procesar ó carpetas sin cerrar." & vbNewLine & vbNewLine & mensaje.ToString(), "Cierre de línea", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Else
                            'Se obtiene el menor estado, que sea mayor al estado actual para cambiar la línea.
                            viewFilesLineaProceso.RowFilter = "fk_estado > " & CStr(Estado)
                            viewFilesLineaProceso.Sort = "fk_estado ASC"

                            Dim dtEstados = viewFilesLineaProceso.ToTable(True, "fk_estado")

                            If (dtEstados.Rows.Count = 0) Then
                                viewFilesLineaProceso.RowFilter = "fk_estado = " & DBCore.EstadoEnum.Reproceso
                                Dim dtReproceso = viewFilesLineaProceso.ToTable(True, "fk_estado")
                                If (dtReproceso.Rows.Count = 0) Then
                                    DesktopMessageBoxControl.DesktopMessageShow("Es posible que la linea de proceso no tenga ningún documento para el siguiente estado [Por favor revise los documentos enviados a reproceso].", "Problemas cerrando linea", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Else
                                    CambiarLineaProceso(CType(dtReproceso.Rows(0).Item("fk_Estado"), DBCore.EstadoEnum))
                                End If
                            Else
                                CambiarLineaProceso(CType(dtEstados.Rows(0).Item("fk_Estado"), DBCore.EstadoEnum))
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarLineaProceso", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub CambiarLineaProceso(ByVal Estado As DBCore.EstadoEnum)
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim typeLineaProceso As New SchemaRisk.TBL_Linea_ProcesoType
                typeLineaProceso.fk_Estado = Estado
                typeLineaProceso.Fecha_Log = SlygNullable.SysDate
                If (Estado = DBCore.EstadoEnum.Reproceso) Then
                    typeLineaProceso.Activo = False
                End If

                dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBUpdate(typeLineaProceso, CInt(Program.Sesion.Parameter("_idLineaProceso")))
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CerrarLineaProceso", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace