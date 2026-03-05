Imports System.Text
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.Devoluciones
Imports Slyg.Tools

Namespace Forms.Devoluciones

    Public Class FormImprimirDevolucion
        Inherits FormBase

#Region " Declaraciones "
        Private _CBarrasItemsReproceso As List(Of String)
        Private _Precinto As String
#End Region

#Region " Constructor "
        Public Sub New(ByVal ItemsReproceso As List(Of String), ByVal Precinto As String)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me._CBarrasItemsReproceso = ItemsReproceso
            Me._Precinto = Precinto
        End Sub
#End Region

#Region " Eventos "
        Private Sub FormImprimirDevolucion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            If ValidaFormulario() Then
                ImprimirGuia()
                ElimiarFoldersCustodia()
            End If
        End Sub
#End Region

#Region " Metodos "
        Private Sub ImprimirGuia()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                ImprimirButton.Enabled = False

                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Transaction_Begin()

                'Crea el reproceso
                Dim typeReproceso As New SchemaRisk.TBL_ReprocesoType
                typeReproceso.id_Reproceso = dbmArchiving.SchemaRisk.TBL_Reproceso.DBNextId()
                typeReproceso.fk_Entidad = Program.RiskGlobal.Entidad
                typeReproceso.fk_Proyecto = Program.RiskGlobal.Proyecto
                typeReproceso.Fecha_Reproceso = SlygNullable.SysDate
                typeReproceso.Guia = GuiaTextBox.Text
                typeReproceso.Sello = SelloTextBox.Text
                typeReproceso.fk_Usuario = Program.Sesion.Usuario.id
                dbmArchiving.SchemaRisk.TBL_Reproceso.DBInsert(typeReproceso)

                'Muestra el reporte
                Dim itemsReporte = New xsdDevoluciones

                Dim Ocultarllave2 As Boolean = True
                'Dim Ocultarllave3 As Boolean = True

                'Los itesm de la devolución
                For Each item In _CBarrasItemsReproceso
                    'Valida que el proyecto use control envio de documentos
                    Dim DatatableProyecto = dbmArchiving.Schemadbo.CTA_Proyecto_parametrizacion.DBFindByfk_Entidadid_Proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                    Dim Usa_control_envío_de_documentos = DatatableProyecto.Rows(0).Item("Usa_control_envío_de_documentos").ToString()
                    If CBool(Usa_control_envío_de_documentos) Then
                        'Crea el file soporte de entrega con el estado en transito
                        Dim DataTableLineaProceso = dbmArchiving.Schemadbo.PA_Crea_File_Control_Documento.DBExecute(Program.RiskGlobal.Entidad,
                                                                                      Program.RiskGlobal.Proyecto,
                                                                                      item,
                                                                                      Program.Sesion.Usuario.id,
                                                                                      GuiaTextBox.Text, SelloTextBox.Text,
                                                                                      Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                      Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede,
                                                                                      Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

                        If DataTableLineaProceso.Rows.Count > 0 Then
                            Dim Mensaje = "Se generó la linea de proceso : " & DataTableLineaProceso.Rows(0).Item("fk_Linea_Proceso").ToString()
                            DesktopMessageBoxControl.DesktopMessageShow(Mensaje, "Devolución [Linea proceso: " & DataTableLineaProceso.Rows(0).Item("fk_Linea_Proceso").ToString() & "]", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        End If
                    End If
                    'Cambia de estado a los items a 1: Rechazado e Incluye el file en el reproceso.
                    'dbmArchiving, dbmCore, item, Nothing, Program.RiskGlobal.Precinto, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Rechazado, Program.Sesion.Usuario.id, Nothing, typeReproceso.id_Reproceso)
                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, item, Nothing, Program.RiskGlobal.Precinto, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Rechazado, Program.Sesion.Usuario.id, Nothing, , typeReproceso.id_Reproceso)

                    Dim orderItems As New RPT_Items_DevolucionEnumList
                    orderItems.Add(RPT_Items_DevolucionEnum.Llave1, True)
                    orderItems.Add(RPT_Items_DevolucionEnum.Llave2, True)
                    'orderItems.Add(RPT_Items_DevolucionEnum.Llave3, True)


                    Dim ItemsDevolucion = dbmArchiving.Schemadbo.RPT_Items_Devolucion.DBFindByCBarras_FileRespuesta(item, False, 0, orderItems)

                    For Each ItemDev In ItemsDevolucion
                        Dim fila = itemsReporte.Items.NewItemsRow()
                        fila.Elemento = ItemDev.Nombre_Documento
                        fila.CBarras = ItemDev.CBarras_File
                        fila.Llave1 = CStr(ItemDev.Llave1)
                        If (Not ItemDev.Llave2 Is DBNull.Value) Then
                            fila.Llave2 = CStr(ItemDev.Llave2)
                            Ocultarllave2 = False
                        End If

                        'If (Not ItemDev.Llave3 Is DBNull.Value) Then
                        '    fila.Llave3 = CStr(ItemDev.Llave3)
                        '    'Ocultarllave3 = True
                        'End If

                        fila.Folios = CStr(ItemDev.Folios_File)
                        fila.NombreLLave1 = CStr(ItemDev.NombreLlave1)
                        If (Not ItemDev.NombreLlave2 Is DBNull.Value) Then fila.NombreLlave2 = CStr(ItemDev.NombreLlave2)
                        'If (Not ItemDev.NombreLlave3 Is DBNull.Value) Then fila.NombreLlave3 = CStr(ItemDev.NombreLlave3)
                        fila.SubCausal = ItemDev.Pregunta_Validacion
                        fila.MotivoDevolucion = ItemDev.Nombre_Reproceso_Motivo
                        fila.Esquema = ItemDev.Nombre_esquema
                        itemsReporte.Items.Rows.Add(fila)
                    Next
                Next

                'Los datos del Reproceso

                Dim dtDataPunto = dbmArchiving.Schemadbo.CTA_Puntos_Proyecto.DBFindByid_Precinto(_Precinto)

                Dim filaReproceso = itemsReporte.Reproceso.NewReprocesoRow()
                filaReproceso.Guia = GuiaTextBox.Text
                filaReproceso.Sello = SelloTextBox.Text

                If dtDataPunto.Rows.Count > 0 Then
                    filaReproceso.UsuarioDestino = dtDataPunto(0).Responsable
                    filaReproceso.DireccionDestino = dtDataPunto(0).Direccion
                    filaReproceso.CiudadDestino = dtDataPunto(0).Ciudad
                    filaReproceso.EntidadDestino = dtDataPunto(0).Nombre_Punto
                Else
                    filaReproceso.UsuarioDestino = Program.RiskGlobal.ProyectoRow.Responsable_Proyecto.ToUpper()
                    filaReproceso.EntidadDestino = Program.RiskGlobal.ProyectoRow.Nombre_Entidad_Responsable.ToUpper()
                End If

                'Dim UsuarioFirma As New SchemaSecurity.CTA_UsuarioDataTable
                'If Not Program.DesktopGlobal.CentroProcesamientoRow.Isfk_Usuario_Respo Then
                Dim UsuarioFirma = dbmArchiving.SchemaSecurity.CTA_Usuario.DBFindByid_Usuario(Program.DesktopGlobal.CentroProcesamientoRow.fk_Usuario_Responsable)
                'End If

                If UsuarioFirma.Count > 0 Then
                    filaReproceso.UsuarioRemite = UsuarioFirma(0).Nombres.ToUpper()
                    If (Not UsuarioFirma(0).IsNombre_CargoNull) Then filaReproceso.CargoRemite = UsuarioFirma(0).Nombre_Cargo.ToUpper()
                    filaReproceso.DependenciaRemite = UsuarioFirma(0).Nombre_Dependencia.ToUpper()
                End If

                Dim CiudadRemite = dbmArchiving.SchemaSecurity.CTA_Sede.DBFindByfk_Entidadid_Sede(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)
                If CiudadRemite.Count > 0 Then
                    filaReproceso.CiudadRemite = CiudadRemite(0).Nombre_Ciudad
                End If
                itemsReporte.Reproceso.Rows.Add(filaReproceso)

                dbmArchiving.Transaction_Commit()

                'Se muestra el informe
                Dim sTituloReporte As String = Now.Year.ToString() & Now.Month.ToString() & Now.Day.ToString() & "-Devolucion-" & typeReproceso.id_Reproceso.ToString() & "-Guia-" & filaReproceso.Guia & "-Sello-" & filaReproceso.Sello
                If AgrupadoCheckBox.Checked Then
                    Dim FormImpresionDevolucion As New FormImpresionDevolucion(itemsReporte, "Miharu.Risk.Library.GuiaDevolucion.rdlc", sTituloReporte, Ocultarllave2)
                    FormImpresionDevolucion.ShowDialog()
                End If

                'Reporte sin agrupar
                If OrdenPunteoCheckBox.Checked Then
                    Dim FormImpresionDevolucion As New FormImpresionDevolucion(itemsReporte, "Miharu.Risk.Library.GuiaDevolucionNoGroup.rdlc", sTituloReporte, Ocultarllave2)
                    FormImpresionDevolucion.ShowDialog()
                End If


                Me.Close()
            Catch ex As Exception
                dbmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("ImprimirGuia", ex)
            Finally
                dbmCore.Connection_Close()
                dbmArchiving.Connection_Close()
                Me.Close()
            End Try
        End Sub

        Private Sub ElimiarFoldersCustodia()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'Se revisa si existen folders  en custodia que se puedan eliminar.
                For Each item In _CBarrasItemsReproceso
                    Dim FileFolders = dbmCore.Schemadbo.CTA_File_Folder.DBFindByCBarras_File(item)
                    If FileFolders.Count > 0 Then
                        Dim FolderCustodia = dbmCore.Schemadbo.CTA_Folder_En_Custodia.DBFindByCBarras_Folderfk_Expedienteid_Folder(FileFolders(0).CBarras_Folder, FileFolders(0).fk_Expediente, FileFolders(0).id_Folder)
                        If FolderCustodia.Count = 0 Then
                            'Se elimina el folder de custody.
                            dbmCore.SchemaCustody.TBL_Folder.DBDelete(FileFolders(0).fk_Expediente, FileFolders(0).id_Folder)
                        End If
                    End If
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ElimiarFoldersCustodia", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function ValidaFormulario() As Boolean
            Dim bReturn As Boolean = True
            Dim msg As New StringBuilder()
            Try
                If GuiaTextBox.Text = "" Then msg.AppendLine("- El número de guía es obligatorio.")
                If SelloTextBox.Text = "" Then msg.AppendLine("- El sello es obligatorio.")
                If Not (AgrupadoCheckBox.Checked Or OrdenPunteoCheckBox.Checked) Then msg.AppendLine("- Debe seleccionar al menos una forma de impresión.")

                If msg.Length > 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow(msg.ToString(), "Datos incompletos", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    bReturn = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidaFormulario", ex)
            End Try
            Return bReturn
        End Function


#End Region

    End Class

End Namespace