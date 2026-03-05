Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports DBImaging.SchemaSecurity
Imports DBImaging.SchemaCore

Namespace Procesos

    Public Class FormSeleccionarProyecto
        Inherits Form

#Region " Eventos "

        Private Sub FormSeleccionarProyecto_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Load_Config()
        End Sub

        Private Sub ProyectoDataGridView_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDataGridView.DoubleClick
            Aceptar()
        End Sub

        Private Sub EntidadComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            Load_Proyectos()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If ProyectoDataGridView.SelectedRows.Count > 0 Then
                Aceptar()
            Else
                MessageBox.Show("No hay proyectos vigentes para la entidad seleccionada o no se escogio uno.", "Proyectos Imaging", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Cancelar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Load_Config()
            Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Try
                EntidadComboBox.ValueMember = "id_entidad"
                EntidadComboBox.DisplayMember = "nombre_entidad"

                Dim entidadProyectoEnumList As New CTA_EntidadEnumList
                entidadProyectoEnumList.Add(CTA_EntidadEnum.Nombre_Entidad, True)

                EntidadComboBox.DataSource = dbmImaging.SchemaSecurity.CTA_Entidad.DBGet(0, entidadProyectoEnumList)

                'Dim proyectoDataTable = dbmImaging.SchemaProcess.CTA_Proyectos_Vigentes.DBFindByfk_EntidadAplica_FisicoAplica_Imagen(CShort(EntidadComboBox.SelectedValue), Nothing, True)
                Dim proyectoDataTable = dbmImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_Usuario.DBFindByfk_EntidadAplica_FisicoAplica_Imagenfk_Usuario(CShort(EntidadComboBox.SelectedValue), Nothing, True, Program.Sesion.Usuario.id)
                ProyectoDataGridView.AutoGenerateColumns = False
                ProyectoDataGridView.DataSource = proyectoDataTable

                EntidadComboBox.Focus()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Load_Proyectos()
            Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            ProyectoDataGridView.AutoGenerateColumns = False
            'ProyectoDataGridView.DataSource = dbmImaging.SchemaProcess.CTA_Proyectos_Vigentes.DBFindByfk_EntidadAplica_FisicoAplica_Imagen(CShort(EntidadComboBox.SelectedValue), Nothing, True)
            ProyectoDataGridView.DataSource = dbmImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_Usuario.DBFindByfk_EntidadAplica_FisicoAplica_Imagenfk_Usuario(CShort(EntidadComboBox.SelectedValue), Nothing, True, Program.Sesion.Usuario.id)

            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Sub

        Private Sub Cancelar()
            Try
                'Cuando se cancela, se limpian las variables del proceso.
                Program.ImagingGlobal = Nothing
                Me.Dispose()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cancelar", ex)
            End Try
        End Sub

        Private Sub Aceptar()
            Dim nEntidad = CShort(EntidadComboBox.SelectedValue)
            Dim nProyecto = CShort(ProyectoDataGridView.SelectedRows(0).Cells("Id_Proyecto").Value)

            If Validar() Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmOCR As DBOCR.DBOCRDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmOCR = New DBOCR.DBOCRDataBaseManager(Program.DesktopGlobal.ConnectionStrings.OCR)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    dbmOCR.Connection_Open(Program.Sesion.Usuario.id)

                    Program.ImagingGlobal = New ImagingGlobal()
                    Program.ImagingGlobal.Entidad = nEntidad
                    Program.ImagingGlobal.NombreEntidad = EntidadComboBox.Text.ToString()
                    Program.ImagingGlobal.Proyecto = nProyecto

                    Dim parametrosDataTable = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet(DBImaging.ParametroSistemaEnum.USA_DOMINIO)

                    If (parametrosDataTable.Count > 0) Then
                        Program.ImagingGlobal.UsaDominioExterno = parametrosDataTable(0).Valor_Parametro_Sistema = "1"
                    Else
                        Program.ImagingGlobal.UsaDominioExterno = False
                    End If

                    Dim proyectoDataTable = dbmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidadid_Proyecto(nEntidad, nProyecto)

                    If proyectoDataTable.Count = 0 Then
                        Try
                            dbmImaging.Transaction_Begin()

                            Dim registroType As New DBImaging.SchemaConfig.TBL_ProyectoType
                            registroType.fk_Entidad = nEntidad
                            registroType.fk_Proyecto = nProyecto
                            registroType.Input_Folder = ""
                            registroType.Usa_Rango_Paquetes = False
                            registroType.Inicio_Nombre_Paquete = ""
                            registroType.Formato_Fecha_Paquete = ""
                            registroType.Usa_Archivo_Indices = False
                            registroType.Nombre_Archivo_Indices = ""
                            registroType.fk_Separador = Nothing
                            registroType.Columnas_Archivo = CShort(0)
                            registroType.Usa_Encabezado_Columnas = False
                            registroType.fk_Identificador_Texto = Nothing
                            registroType.Columna_Imagen = CShort(0)
                            registroType.Caracteres_Omitir = CByte(0)
                            registroType.fk_Formato_Entrada = CShort(3)
                            registroType.Columna_Key = CShort(0)
                            registroType.Usa_Indexacion = False
                            registroType.Usa_Paquete_x_Imagen = False
                            registroType.fk_Formato_Salida = CShort(3)
                            registroType.Usa_Folder_Unico = False
                            registroType.Usa_File_Unico = False
                            registroType.fk_Entidad_Servidor = CShort(1)
                            registroType.fk_Servidor = CShort(1)
                            registroType.Captura_Llaves_Paquete = False
                            registroType.Usa_OCR_Captura = False
                            registroType.Usa_OCR_Indexacion = False

                            dbmImaging.SchemaConfig.TBL_Proyecto.DBInsert(registroType)
                            dbmImaging.Transaction_Commit()
                        Catch
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try
                    End If

                    Dim dtProyecto = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    If dtProyecto.Count > 0 Then
                        Program.ImagingGlobal.ProyectoImagingRow = dtProyecto(0).ToCTA_ProyectoSimpleType()

                        ' Almacena las configuraciones del proyecto OCR
                        If Program.ImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura = True Then

                            Dim dtproyectoOCR = dbmOCR.SchemaConfig.TBL_Proyecto_OCR.DBFindByfk_Lineafk_Entidadfk_Proyecto(1, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                            If dtproyectoOCR.Count > 0 Then
                                Program.ImagingGlobal.proyectoOCRRow = dtproyectoOCR(0).ToTBL_Proyecto_OCRSimpleType()
                            End If
                        End If

                        'Se obtienen las llaves
                        Dim listaLlaves As New List(Of DesktopConfig.LlaveProyecto)

                        Dim proyectoIdLlaveEnumList As New CTA_Config_Proyecto_LlaveEnumList()
                        proyectoIdLlaveEnumList.Add(CTA_Config_Proyecto_LlaveEnum.id_Proyecto_Llave, True)

                        Dim dtProyectoLlaves = dbmImaging.SchemaCore.CTA_Config_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, CShort(Program.ImagingGlobal.Proyecto), 0, proyectoIdLlaveEnumList)
                        If dtProyectoLlaves.Count > 0 Then
                            For Each row In dtProyectoLlaves
                                Dim llave As DesktopConfig.LlaveProyecto
                                llave.Id = CShort(row.id_Proyecto_Llave)
                                llave.Nombre = row.Nombre_Proyecto_Llave
                                llave.Tipo = CType(row.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                                listaLlaves.Add(llave)
                            Next
                        End If
                        Program.ImagingGlobal.LLavesProyecto = listaLlaves
                    End If

                    Try
                        Dim proyectoImagingDataTable = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

                        If (proyectoImagingDataTable.Count = 0) Then
                            DesktopMessageBoxControl.DesktopMessageShow("No se encontró parametrización para la mesa de control de imágenes", "Mesa Control Imagenes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Else
                            Program.ImagingGlobal.ProyectoImagingRow = proyectoImagingDataTable(0).ToCTA_ProyectoSimpleType()

                            If proyectoImagingDataTable(0).Captura_Llaves_Paquete Then
                                Dim proyectoLlavePaqueteDataTable = dbmImaging.SchemaProcess.CTA_Proyecto_Llave_Paquete.DBFindByfk_Entidadfk_Proyecto(proyectoImagingDataTable(0).fk_Entidad, proyectoImagingDataTable(0).fk_Proyecto)
                                If proyectoLlavePaqueteDataTable.Count = 0 Then
                                    DesktopMessageBoxControl.DesktopMessageShow("El proyecto maneja llaves por paquete, pero no se encontro configuración de estas.", "Llaves por Paquete", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Else
                                    Program.ImagingGlobal.ProyectoImagingLlaveDataTable = proyectoLlavePaqueteDataTable
                                End If
                            End If
                        End If

                        Dim esquemasDataTable = dbmImaging.SchemaCore.CTA_Esquema.DBFindByfk_Entidadfk_Proyecto(nEntidad, nProyecto)
                        Program.ImagingGlobal.Esquemas = esquemasDataTable

                    Catch
                        Throw
                    End Try

                    Dim tipologiasDataTable = dbmImaging.SchemaProcess.CTA_Tipologias_Esquema.DBFindByfk_entidadfk_Proyectoid_Esquema(nEntidad, nProyecto, Nothing)
                    Program.ImagingGlobal.Tipologias = tipologiasDataTable

                    Me.DialogResult = DialogResult.OK
                    Me.Close()

                Catch ex As Exception
                    Me.DialogResult = DialogResult.Cancel
                    DesktopMessageBoxControl.DesktopMessageShow("Aceptar", ex)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmOCR IsNot Nothing) Then dbmOCR.Connection_Close()
                End Try
            Else
                MessageBox.Show("Debe seleccionar una entidad y un proyecto.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                EntidadComboBox.Focus()
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            Dim bReturn As Boolean = True
            Try
                If EntidadComboBox.SelectedValue.ToString() = "" Then
                    bReturn = False
                ElseIf ProyectoDataGridView.SelectedRows.Count < 1 Then
                    bReturn = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Validar", ex)
            End Try
            Return bReturn
        End Function

#End Region

    End Class

End Namespace