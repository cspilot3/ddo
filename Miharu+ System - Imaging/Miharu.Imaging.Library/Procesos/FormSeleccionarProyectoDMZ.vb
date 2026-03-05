Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports DBImaging.SchemaSecurity
Imports DBImaging.SchemaCore
Imports QueryResponse = Miharu.Desktop.Library.MiharuDMZ.QueryResponse
Imports ClientUtil = Miharu.Desktop.Library.MiharuDMZ.ClientUtil
Imports QueryParameter = Miharu.Desktop.Library.MiharuDMZ.QueryParameter
Imports QueryResponseType = Miharu.Desktop.Library.MiharuDMZ.QueryResponseType
Imports QueryRequestType = Miharu.Desktop.Library.MiharuDMZ.QueryRequestType

Namespace Procesos

    Public Class FormSeleccionarProyectoDMZ
        Inherits Form

#Region " Eventos "

        Private Sub FormSeleccionarProyecto_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Load_Config()
            Aceptar()
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
            'Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            'dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Try
                EntidadComboBox.ValueMember = "id_entidad"
                EntidadComboBox.DisplayMember = "nombre_entidad"

                Dim entidadProyectoEnumList As New CTA_EntidadEnumList
                entidadProyectoEnumList.Add(CTA_EntidadEnum.Nombre_Entidad, True)

                Dim dataTableEntidades As DBImaging.SchemaSecurity.CTA_EntidadDataTable = Nothing
                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Security.CTA_Entidad_Usuario_Externo", New List(Of QueryParameter) From {
                                                                     New QueryParameter With {.name = "id_Usuario", .value = Program.Sesion.Usuario.id.ToString()}
                                                                     }, QueryRequestType.Table, QueryResponseType.Table)
                dataTableEntidades = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaSecurity.CTA_EntidadDataTable(), queryResponse.dataTable), DBImaging.SchemaSecurity.CTA_EntidadDataTable)
                EntidadComboBox.DataSource = dataTableEntidades
                EntidadComboBox.SelectedItem = 0

                EntidadComboBox.Focus()
                Load_Proyectos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Private Sub Load_Proyectos()

            ProyectoDataGridView.AutoGenerateColumns = False

            Dim dataTableProyecto As DBImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_UsuarioDataTable = Nothing
            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].security.CTA_Entidad_Proyecto_Rol_Usuario", New List(Of QueryParameter) From {
                                                         New QueryParameter With {.name = "fk_Entidad", .value = EntidadComboBox.SelectedValue.ToString()},
                                                         New QueryParameter With {.name = "Aplica_Fisico", .value = String.Empty},
                                                         New QueryParameter With {.name = "Aplica_Imagen", .value = True.ToString()},
                                                         New QueryParameter With {.name = "fk_Usuario", .value = Program.Sesion.Usuario.id.ToString()}
                                                     }, QueryRequestType.Table, QueryResponseType.Table)
            dataTableProyecto = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_UsuarioDataTable(), queryResponse.dataTable), DBImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_UsuarioDataTable)

            ProyectoDataGridView.DataSource = dataTableProyecto
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

                Try
                    Program.ImagingGlobal = New ImagingGlobal()
                    Program.ImagingGlobal.Entidad = nEntidad
                    Program.ImagingGlobal.NombreEntidad = EntidadComboBox.Text.ToString()
                    Program.ImagingGlobal.Proyecto = nProyecto

                    'Dim parametrosDataTable = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet(DBImaging.ParametroSistemaEnum.USA_DOMINIO)
                    Dim parametrosDataTable As DBImaging.SchemaConfig.TBL_Parametro_SistemaDataTable = Nothing
                    Dim queryResponseParametros As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema", New List(Of QueryParameter), QueryRequestType.Table, QueryResponseType.Table)
                    parametrosDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.TBL_Parametro_SistemaDataTable(), queryResponseParametros.dataTable), DBImaging.SchemaConfig.TBL_Parametro_SistemaDataTable)

                    If (parametrosDataTable.Count > 0) Then
                        Program.ImagingGlobal.UsaDominioExterno = parametrosDataTable(0).Valor_Parametro_Sistema = "1"
                    Else
                        Program.ImagingGlobal.UsaDominioExterno = False
                    End If

                    'Dim proyectoDataTable = dbmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidadid_Proyecto(nEntidad, nProyecto)
                    Dim proyectoDataTable As DBImaging.SchemaCore.CTA_ProyectoDataTable = Nothing
                    Dim queryResponseProyecto As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Core.CTA_Proyecto", New List(Of QueryParameter) From {
                                                         New QueryParameter With {.name = "fk_Entidad", .value = nEntidad.ToString()},
                                                         New QueryParameter With {.name = "id_Proyecto", .value = nProyecto.ToString()}
                                                     }, QueryRequestType.Table, QueryResponseType.Table)
                    proyectoDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaCore.CTA_ProyectoDataTable(), queryResponseProyecto.dataTable), DBImaging.SchemaCore.CTA_ProyectoDataTable)

                    If proyectoDataTable.Count = 0 Then
                        Try
                            Dim registroType As DBImaging.SchemaConfig.TBL_ProyectoDataTable = Nothing
                            Dim queryResponseRegistro As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.PA_Insertar_Proyecto", New List(Of QueryParameter) From {
                                     New QueryParameter With {.name = "fk_Entidad", .value = nEntidad.ToString()},
                                     New QueryParameter With {.name = "fk_Proyecto", .value = nProyecto.ToString()},
                                     New QueryParameter With {.name = "Input_Folder", .value = String.Empty},
                                     New QueryParameter With {.name = "Usa_Rango_Paquetes", .value = False.ToString()},
                                     New QueryParameter With {.name = "Inicio_Nombre_Paquete", .value = String.Empty},
                                     New QueryParameter With {.name = "Formato_Fecha_Paquete", .value = String.Empty},
                                     New QueryParameter With {.name = "Usa_Archivo_Indices", .value = False.ToString()},
                                     New QueryParameter With {.name = "Nombre_Archivo_Indices", .value = String.Empty},
                                     New QueryParameter With {.name = "fk_Separador", .value = String.Empty},
                                     New QueryParameter With {.name = "Columnas_Archivo", .value = CShort(0).ToString()},
                                     New QueryParameter With {.name = "Usa_Encabezado_Columnas", .value = False.ToString()},
                                     New QueryParameter With {.name = "fk_Identificador_Texto", .value = String.Empty},
                                     New QueryParameter With {.name = "Columna_Imagen", .value = CShort(0).ToString()},
                                     New QueryParameter With {.name = "Caracteres_Omitir", .value = CByte(0).ToString()},
                                     New QueryParameter With {.name = "fk_Formato_Entrada", .value = CShort(3).ToString()},
                                     New QueryParameter With {.name = "Columna_Key", .value = CShort(0).ToString()},
                                     New QueryParameter With {.name = "Usa_Indexacion", .value = False.ToString()},
                                     New QueryParameter With {.name = "Usa_Paquete_x_Imagen", .value = False.ToString()},
                                     New QueryParameter With {.name = "fk_Formato_Salida", .value = CShort(3).ToString()},
                                     New QueryParameter With {.name = "Usa_Folder_Unico", .value = False.ToString()},
                                     New QueryParameter With {.name = "Usa_File_Unico", .value = False.ToString()},
                                     New QueryParameter With {.name = "fk_Entidad_Servidor", .value = CShort(1).ToString()},
                                     New QueryParameter With {.name = "fk_Servidor", .value = CShort(1).ToString()},
                                     New QueryParameter With {.name = "Captura_Llaves_Paquete", .value = False.ToString()},
                                     New QueryParameter With {.name = "Usa_OCR_Captura", .value = False.ToString()},
                                     New QueryParameter With {.name = "Usa_OCR_Indexacion", .value = False.ToString()}
                                 }, QueryRequestType.Table, QueryResponseType.Table)
                        Catch
                            Throw
                        End Try
                    End If

                    'Dim dtProyecto = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    Dim dtProyecto As DBImaging.SchemaConfig.CTA_ProyectoDataTable = Nothing
                    Dim queryResponsedtProyecto As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Config].[CTA_Proyecto]", New List(Of QueryParameter) From {
                                                         New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                         New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()}
                                                     }, QueryRequestType.Table, QueryResponseType.Table)
                    dtProyecto = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_ProyectoDataTable(), queryResponsedtProyecto.dataTable), DBImaging.SchemaConfig.CTA_ProyectoDataTable)


                    If dtProyecto.Count > 0 Then
                        Program.ImagingGlobal.ProyectoImagingRow = dtProyecto(0).ToCTA_ProyectoSimpleType

                        ' Almacena las configuraciones del proyecto OCR
                        If Program.ImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura = True Then

                            'Dim dtproyectoOCR = dbmOCR.SchemaConfig.TBL_Proyecto_OCR.DBFindByfk_Lineafk_Entidadfk_Proyecto(1, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                            Dim dtproyectoOCR As DBOCR.SchemaConfig.TBL_Proyecto_OCRDataTable = Nothing
                            Dim queryResponsedtproyectoOCR As QueryResponse = ClientUtil.resolver("[DB_OCR].Config.TBL_Proyecto_OCR", New List(Of QueryParameter) From {
                                                                 New QueryParameter With {.name = "fk_Linea", .value = CShort(1).ToString()},
                                                                 New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                                 New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()}
                                                             }, QueryRequestType.Table, QueryResponseType.Table)
                            dtproyectoOCR = CType(ClientUtil.mapToTypedTable(New DBOCR.SchemaConfig.TBL_Proyecto_OCRDataTable(), queryResponsedtproyectoOCR.dataTable), DBOCR.SchemaConfig.TBL_Proyecto_OCRDataTable)

                            If dtproyectoOCR.Count > 0 Then
                                Program.ImagingGlobal.proyectoOCRRow = dtproyectoOCR(0).ToTBL_Proyecto_OCRSimpleType()
                            End If
                        End If

                        'Se obtienen las llaves
                        Dim listaLlaves As New List(Of DesktopConfig.LlaveProyecto)

                        Dim proyectoIdLlaveEnumList As New CTA_Config_Proyecto_LlaveEnumList()
                        proyectoIdLlaveEnumList.Add(CTA_Config_Proyecto_LlaveEnum.id_Proyecto_Llave, True)

                        'Dim dtProyectoLlaves = dbmImaging.SchemaCore.CTA_Config_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, CShort(Program.ImagingGlobal.Proyecto), 0, proyectoIdLlaveEnumList)
                        Dim dtProyectoLlaves As DBImaging.SchemaCore.CTA_Config_Proyecto_LlaveDataTable
                        Dim queryResponsedtProyectoLlaves As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Core.CTA_Config_Proyecto_Llave", New List(Of QueryParameter) From {
                                                             New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                             New QueryParameter With {.name = "fk_Proyecto", .value = CShort(Program.ImagingGlobal.Proyecto).ToString()}
                                                         }, QueryRequestType.Table, QueryResponseType.Table)
                        dtProyectoLlaves = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaCore.CTA_Config_Proyecto_LlaveDataTable(), queryResponsedtProyectoLlaves.dataTable), DBImaging.SchemaCore.CTA_Config_Proyecto_LlaveDataTable)

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
                        'Dim proyectoImagingDataTable = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                        Dim proyectoImagingDataTable As DBImaging.SchemaConfig.CTA_ProyectoDataTable
                        Dim queryResponseProyectoImaging As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.CTA_Proyecto", New List(Of QueryParameter) From {
                                                             New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                             New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()}
                                                         }, QueryRequestType.Table, QueryResponseType.Table)
                        proyectoImagingDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_ProyectoDataTable(), queryResponseProyectoImaging.dataTable), DBImaging.SchemaConfig.CTA_ProyectoDataTable)

                        If (proyectoImagingDataTable.Count = 0) Then
                            DesktopMessageBoxControl.DesktopMessageShow("No se encontró parametrización para la mesa de control de imágenes", "Mesa Control Imagenes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Else
                            Program.ImagingGlobal.ProyectoImagingRow = proyectoImagingDataTable(0).ToCTA_ProyectoSimpleType()

                            If proyectoImagingDataTable(0).Captura_Llaves_Paquete Then
                                'Dim proyectoLlavePaqueteDataTable = dbmImaging.SchemaProcess.CTA_Proyecto_Llave_Paquete.DBFindByfk_Entidadfk_Proyecto(proyectoImagingDataTable(0).fk_Entidad, proyectoImagingDataTable(0).fk_Proyecto)
                                Dim proyectoLlavePaqueteDataTable As DBImaging.SchemaProcess.CTA_Proyecto_Llave_PaqueteDataTable
                                Dim queryResponseProyectoLlavePaquete As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.CTA_Proyecto_Llave_Paquete", New List(Of QueryParameter) From {
                                                                     New QueryParameter With {.name = "fk_Entidad", .value = proyectoImagingDataTable(0).fk_Entidad.ToString()},
                                                                     New QueryParameter With {.name = "fk_Proyecto", .value = proyectoImagingDataTable(0).fk_Proyecto.ToString()}
                                                                 }, QueryRequestType.Table, QueryResponseType.Table)
                                proyectoLlavePaqueteDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Proyecto_Llave_PaqueteDataTable(), queryResponseProyectoLlavePaquete.dataTable), DBImaging.SchemaProcess.CTA_Proyecto_Llave_PaqueteDataTable)

                                If proyectoLlavePaqueteDataTable.Count = 0 Then
                                    DesktopMessageBoxControl.DesktopMessageShow("El proyecto maneja llaves por paquete, pero no se encontro configuración de estas.", "Llaves por Paquete", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Else
                                    Program.ImagingGlobal.ProyectoImagingLlaveDataTable = proyectoLlavePaqueteDataTable
                                End If
                            End If
                        End If

                        'Dim esquemasDataTable = dbmImaging.SchemaCore.CTA_Esquema.DBFindByfk_Entidadfk_Proyecto(nEntidad, nProyecto)
                        Dim esquemasDataTable As DBImaging.SchemaCore.CTA_EsquemaDataTable
                        Dim queryResponseEsquemasDataTable As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Core.CTA_Esquema", New List(Of QueryParameter) From {
                                                             New QueryParameter With {.name = "fk_Entidad", .value = nEntidad.ToString()},
                                                             New QueryParameter With {.name = "fk_Proyecto", .value = nProyecto.ToString()}
                                                         }, QueryRequestType.Table, QueryResponseType.Table)
                        esquemasDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaCore.CTA_EsquemaDataTable(), queryResponseEsquemasDataTable.dataTable), DBImaging.SchemaCore.CTA_EsquemaDataTable)

                        Program.ImagingGlobal.Esquemas = esquemasDataTable

                    Catch
                        Throw
                    End Try

                    'Dim tipologiasDataTable = dbmImaging.SchemaProcess.CTA_Tipologias_Esquema.DBFindByfk_entidadfk_Proyectoid_Esquema(nEntidad, nProyecto, Nothing)
                    Dim tipologiasDataTable As DBImaging.SchemaProcess.CTA_Tipologias_EsquemaDataTable = Nothing
                    Dim queryResponseTipologiasDataTable As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.CTA_Tipologias_Esquema", New List(Of QueryParameter) From {
                                                                                New QueryParameter With {.name = "fk_entidad", .value = nEntidad.ToString()},
                                                                                New QueryParameter With {.name = "fk_Proyecto", .value = nProyecto.ToString()},
                                                                                New QueryParameter With {.name = "id_Esquema", .value = String.Empty}
                                                                            }, QueryRequestType.Table, QueryResponseType.Table)
                    tipologiasDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Tipologias_EsquemaDataTable(), queryResponseTipologiasDataTable.dataTable), DBImaging.SchemaProcess.CTA_Tipologias_EsquemaDataTable)

                    Program.ImagingGlobal.Tipologias = tipologiasDataTable

                    Me.DialogResult = DialogResult.OK
                    Me.Close()

                Catch ex As Exception
                    Me.DialogResult = DialogResult.Cancel
                    DesktopMessageBoxControl.DesktopMessageShow("Aceptar", ex)
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