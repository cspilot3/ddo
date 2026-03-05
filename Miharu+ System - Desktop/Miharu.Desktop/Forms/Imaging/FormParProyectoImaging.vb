Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports DBCore
Imports System.Windows.Forms
Imports System.Text
Imports Slyg.Tools

Namespace Forms.Imaging

    Public Class FormParProyectoImaging
        Inherits Library.FormBase

#Region " Declaraciones "

        Private _ListLlaves As List(Of DesktopConfig.LlavePosicion)
        Private idProyecto As Short = 0

#End Region

#Region " Eventos "

        Private Sub FormParProyectoImaging_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCombos()
        End Sub

        Private Sub fk_entidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles fk_entidadDesktopComboBox.SelectedIndexChanged
            Dim dmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Dim Proyecto = dmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidad(CShort(fk_entidadDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_proyectoDesktopComboBox, Proyecto, Proyecto.id_ProyectoColumn.ColumnName, Proyecto.Nombre_ProyectoColumn.ColumnName)

            dmImaging.Connection_Close()
        End Sub

        Private Sub fk_proyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles fk_proyectoDesktopComboBox.SelectedIndexChanged
            SeleccionarProyecto()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub fk_Entidad_ServidorDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadServidorDesktopComboBox.SelectedIndexChanged
            CambiaEntidadServidor()
        End Sub

        Private Sub CapturaLlavesPaqueteDesktopCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CapturaLlavesPaqueteDesktopCheckBox.CheckedChanged
            If CapturaLlavesPaqueteDesktopCheckBox.Checked Then
                Dim Entidad = CShort(fk_entidadDesktopComboBox.SelectedValue)
                Dim Proyecto = CShort(fk_proyectoDesktopComboBox.SelectedValue)

                If Entidad > 0 And Proyecto > 0 Then
                    AsignarLLavesButton.Enabled = True
                Else
                    CapturaLlavesPaqueteDesktopCheckBox.Checked = False
                    DesktopMessageBoxControl.DesktopMessageShow("Se debe realizar la configuración de Entidad y Proyecto antes de la asignación de llaves.", "Configuración requerida", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Else
                AsignarLLavesButton.Enabled = False
                _ListLlaves = Nothing
            End If
        End Sub

        Private Sub AsignarLLavesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AsignarLLavesButton.Click
            Dim Entidad = CShort(fk_entidadDesktopComboBox.SelectedValue)
            Dim Proyecto = CShort(fk_proyectoDesktopComboBox.SelectedValue)

            Dim objParProyectoLlave As New FormParProyectoLlaveImaging(Entidad, Proyecto, _ListLlaves)
            Dim Respuesta = objParProyectoLlave.ShowDialog()

            If Respuesta = DialogResult.OK Then
                _ListLlaves = objParProyectoLlave.ListaLLaves
            Else
                CapturaLlavesPaqueteDesktopCheckBox.Checked = False
            End If
        End Sub

        Private Sub Usa_Rango_PaquetesDesktopCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles Usa_Rango_PaquetesDesktopCheckBox.CheckedChanged
            CambiaRangoPaquetes()
        End Sub

        Private Sub Usa_Archivo_IndicesDesktopCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles Usa_Archivo_IndicesDesktopCheckBox.CheckedChanged
            CambiaArchivoIndices()
        End Sub

        Private Sub BuscarFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarFolderButton.Click
            InputFolderFolderBrowserDialog.SelectedPath = InputFolderDesktopTextBox.Text
            If InputFolderFolderBrowserDialog.ShowDialog = DialogResult.OK Then
                InputFolderDesktopTextBox.Text = InputFolderFolderBrowserDialog.SelectedPath
            End If
        End Sub

        Private Sub Usa_IndexacionDesktopCheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles Usa_IndexacionDesktopCheckBox.CheckedChanged
            CambiaUsaIndexacion()
        End Sub

        Private Sub Usa_Columna_EsquemaDesktopCheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles Usa_Columna_EsquemaDesktopCheckBox.CheckedChanged
            CambiaUsaColumnaEsquema()
        End Sub

        Private Sub Usa_Columna_DocumentoDesktopCheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles Usa_Columna_DocumentoDesktopCheckBox.CheckedChanged
            CambiaUsaColumnaDocumento()
        End Sub

        Private Sub UsaDestapeContenedorDesktopCheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles UsaDestapeContenedorDesktopCheckBox.CheckedChanged
            UsaEmpaqueContenedorDesktopCheckBox.Checked = UsaDestapeContenedorDesktopCheckBox.Checked
        End Sub

        Private Sub NotificacionCierreOTCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles NotificacionCierreOTCheckBox.CheckedChanged
            If NotificacionCierreOTCheckBox.Checked Then
                NotificacionCierreFechaProcesoCheckBox.Checked = False
            End If
        End Sub

        Private Sub NotificacionCierreFechaProcesoCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles NotificacionCierreFechaProcesoCheckBox.CheckedChanged
            If NotificacionCierreFechaProcesoCheckBox.Checked Then
                NotificacionCierreOTCheckBox.Checked = False
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub CargarCombos()
            Dim dmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Dim Separador = dmImaging.SchemaConfig.TBL_Separador.DBGet(Nothing)
            Utilities.LlenarCombo(fk_SeparadorDesktopComboBox, Separador, Separador.id_SeparadorColumn.ColumnName, Separador.Nombre_SeparadorColumn.ColumnName)

            Dim IdentificadorTexto = dmImaging.SchemaConfig.TBL_Identificador_Texto.DBGet(Nothing)
            Utilities.LlenarCombo(fk_Identificador_TextoDesktopComboBox, IdentificadorTexto, IdentificadorTexto.id_Identificador_TextoColumn.ColumnName, IdentificadorTexto.Nombre_Identificador_TextoColumn.ColumnName)

            Dim Extensiones = dmImaging.SchemaConfig.TBL_Formato_Imagen.DBGet(Nothing)
            Utilities.LlenarCombo(FormatoSalidaDesktopComboBox, Extensiones, Extensiones.id_Formato_ImagenColumn.ColumnName, Extensiones.Nombre_Formato_ImagenColumn.ColumnName)
            Utilities.LlenarCombo(FormatoEntradaDesktopComboBox, Extensiones, Extensiones.id_Formato_ImagenColumn.ColumnName, Extensiones.Nombre_Formato_ImagenColumn.ColumnName)

            Dim Entidad = dmImaging.SchemaSecurity.CTA_Entidad.DBGet(0, New DBImaging.SchemaSecurity.CTA_EntidadEnumList(DBImaging.SchemaSecurity.CTA_EntidadEnum.Nombre_Entidad, True))
            Utilities.LlenarCombo(fk_entidadDesktopComboBox, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)
            Utilities.LlenarCombo(EntidadServidorDesktopComboBox, Entidad, Entidad.id_EntidadColumn.ColumnName, Entidad.Nombre_EntidadColumn.ColumnName)

            Dim Proyecto = dmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidad(CShort(fk_entidadDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(fk_proyectoDesktopComboBox, Proyecto, Proyecto.id_ProyectoColumn.ColumnName, Proyecto.Nombre_ProyectoColumn.ColumnName)

            CambiaEntidadServidor()

            dmImaging.Connection_Close()
        End Sub

        Public Sub CambiaEntidadServidor()
            Dim dmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dmCore.Connection_Open(Program.Sesion.Usuario.id)

            Dim Servidor = dmCore.SchemaImaging.TBL_Servidor.DBFindByfk_Entidad(CShort(EntidadServidorDesktopComboBox.SelectedValue))
            Utilities.LlenarCombo(ServidorDesktopComboBox, Servidor, Servidor.id_ServidorColumn.ColumnName, Servidor.Nombre_ServidorColumn.ColumnName)

            dmCore.Connection_Close()
        End Sub

        Public Sub SeleccionarProyecto()
            Dim dmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

            Try
                dmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim ProyectoRow = dmImaging.SchemaProcess.CTA_Proyecto_Parametrizacion.DBFindByid_Proyectofk_Entidad(CShort(fk_proyectoDesktopComboBox.SelectedValue), CShort(fk_entidadDesktopComboBox.SelectedValue))(0)

                idProyecto = ProyectoRow.fk_Proyecto

                ' Cargue
                Me.InputFolderDesktopTextBox.Text = ProyectoRow.Input_Folder
                Me.Usa_Archivo_IndicesDesktopCheckBox.Checked = ProyectoRow.Usa_Archivo_Indices
                Me.Usa_Encabezado_ColumnasDesktopCheckBox.Checked = ProyectoRow.Usa_Encabezado_Columnas
                Me.Nombre_Archivo_IndicesDesktopTextBox.Text = ProyectoRow.Nombre_Archivo_Indices
                Me.fk_SeparadorDesktopComboBox.SelectedValue = ProyectoRow.fk_Separador
                Me.fk_Identificador_TextoDesktopComboBox.SelectedValue = ProyectoRow.fk_Identificador_Texto
                Me.Caracteres_OmitirDesktopTextBo.Text = ProyectoRow.Caracteres_Omitir.ToString()
                Me.ColumnasArchivoDesktopTextBox.Text = ProyectoRow.Columnas_Archivo.ToString()
                Me.ColumnaImagenDesktopTextBox.Text = ProyectoRow.Columna_Imagen.ToString()
                Me.ColumnaKeyDesktopTextBox.Text = ProyectoRow.Columna_Key.ToString()

                ' Formato
                Me.FormatoEntradaDesktopComboBox.SelectedValue = ProyectoRow.fk_Formato_Entrada
                Me.FormatoSalidaDesktopComboBox.SelectedValue = ProyectoRow.fk_Formato_Salida

                ' Servidor
                Me.EntidadServidorDesktopComboBox.SelectedValue = ProyectoRow.fk_Entidad_Servidor
                Me.ServidorDesktopComboBox.SelectedValue = ProyectoRow.fk_Servidor

                ' Paquetes
                Me.Usa_Rango_PaquetesDesktopCheckBox.Checked = ProyectoRow.Usa_Rango_Paquetes
                Me.Inicio_Nombre_PaqueteDesktopTextBox.Text = ProyectoRow.Inicio_Nombre_Paquete
                Me.Formato_Fecha_PaqueteDesktopTextBox.Text = ProyectoRow.Formato_Fecha_Paquete
                Me.Usa_Paquete_x_ImagenDesktopCheckBox.Checked = ProyectoRow.Usa_Paquete_x_Imagen
                Me.CapturaLlavesPaqueteDesktopCheckBox.Checked = ProyectoRow.Captura_Llaves_Paquete

                Dim ProyectoLLavePaquete = dmImaging.SchemaConfig.TBL_Proyecto_Llave_Paquete.DBFindByfk_Entidadfk_Proyecto(CShort(Me.fk_entidadDesktopComboBox.SelectedValue), CShort(Me.fk_proyectoDesktopComboBox.SelectedValue))
                If ProyectoLLavePaquete.Count > 0 Then
                    _ListLlaves = New List(Of DesktopConfig.LlavePosicion)
                    For Each Llave In ProyectoLLavePaquete
                        Dim LlavePosicion As New DesktopConfig.LlavePosicion
                        LlavePosicion.Id_Llave = Llave.Id_Proyecto_Llave_Paquete
                        LlavePosicion.Nombre_Llave = ""
                        LlavePosicion.Posicion_Inicial = Llave.Posicion_Inicial
                        LlavePosicion.Posicion_Longitud = Llave.Posicion_Longitud
                        _ListLlaves.Add(LlavePosicion)
                    Next
                End If

                ' Destape
                Me.UsaCodigoContenedorDesktopCheckBox.Checked = ProyectoRow.Usa_Codigo_Contenedor
                Me.UsaDestapeContenedorDesktopCheckBox.Checked = ProyectoRow.Usa_Destape_Contenedor
                Me.UsaCantidadesEnviadasyRecibidasDesktopCheckBox.Checked = ProyectoRow.Usa_Cantidades_Enviadas_Recibidas
                Me.MuestraCantidadRecibidaDesktopCheckBox.Checked = ProyectoRow.Muestra_Cantidad_Recibida
                Me.CorrespondenciaDestapeVsImagenesCheckBox.Checked = ProyectoRow.Correspondencia_Destape_Vs_Imagenes
                Me.CantidadOTDesktopTextBox.Text = ProyectoRow.Cantidad_Maxima_OTxTipo.ToString
                Me.NotificacionCierreOTCheckBox.Checked = ProyectoRow.Notificacion_Cierre_OT
                Me.NotificacionCierreFechaProcesoCheckBox.Checked = ProyectoRow.Notificacion_Cierre_FechaProceso

                ' Empaque
                Me.EmpaqueMinDesktopTextBox.Text = ProyectoRow.Empaque_Min.ToString()
                Me.EmpaqueMaxDesktopTextBox.Text = ProyectoRow.Empaque_Max.ToString()
                Me.UsaEmpaqueContenedorDesktopCheckBox.Checked = ProyectoRow.Usa_Destape_Contenedor
                Me.RequiereExportacionDesktopCheckBox.Checked = ProyectoRow.Requiere_Exportacion

                ' Indexación            
                Me.Usa_IndexacionDesktopCheckBox.Checked = ProyectoRow.Usa_Indexacion
                Me.Usa_Folder_UnicoDesktopCheckBox.Checked = ProyectoRow.Usa_Folder_Unico
                Me.Usa_File_UnicoDesktopCheckBox.Checked = ProyectoRow.Usa_File_Unico
                Me.Show_InformationDesktopCheckBox.Checked = ProyectoRow.Show_Information
                Me.Usa_Columna_EsquemaDesktopCheckBox.Checked = ProyectoRow.Usa_Columna_Esquema
                Me.Columna_EsquemaDesktopTextBox.Text = ProyectoRow.Columna_Esquema.ToString()
                Me.Default_EsquemaDesktopTextBox.Text = ProyectoRow.Default_Esquema.ToString()
                Me.Usa_Columna_DocumentoDesktopCheckBox.Checked = ProyectoRow.Usa_Columna_Documento
                Me.Columna_DocumentoDesktopTextBox.Text = ProyectoRow.Columna_Documento.ToString()
                Me.Default_DocumentoDesktopTextBox.Text = ProyectoRow.Default_Documento.ToString()
                Me.Usa_CalidadDesktopCheckBox.Checked = ProyectoRow.Usa_Calidad
                Me.Usa_RecortesDesktopCheckBox.Checked = ProyectoRow.Usa_Recortes
                Me.UsaDominioExternoDesktopCheckBox.Checked = ProyectoRow.Usa_Dominio_Externo

                ' Seguimiento
                Me.Seguimiento_Show_Col_ObservacionesDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Observaciones
                Me.Seguimiento_Show_Col_Key01DesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Key01
                Me.Seguimiento_Show_Col_Key02DesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Key02
                Me.Seguimiento_Show_Col_Key03DesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Key03
                Me.Seguimiento_Show_Col_PaquetesDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Paquetes
                Me.Seguimiento_Show_Col_fk_PaqueteDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_fk_Paquete
                Me.Seguimiento_Show_Col_ItemsDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Items
                Me.Seguimiento_Show_Col_IndexacionDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Indexacion
                Me.Seguimiento_Show_Col_PreCapturaDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_PreCaptura
                Me.Seguimiento_Show_Col_PrimeraCapturaDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_PrimeraCaptura
                Me.Seguimiento_Show_Col_SegundaCapturaDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_SegundaCaptura
                Me.Seguimiento_Show_Col_TerceraCapturaDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_TerceraCaptura
                Me.Seguimiento_Show_Col_CalidadDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Calidad
                Me.Seguimiento_Show_Col_IndexadoDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Indexado
                Me.Seguimiento_Show_Col_Validaciones_TotalesDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Validaciones_Totales
                Me.Seguimiento_Show_Col_Validaciones_PendientesDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Validaciones_Pendientes
                Me.Seguimiento_Show_Col_RetenidoDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Retenido
                Me.Seguimiento_Show_Col_ReprocesoDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Reproceso
                Me.Seguimiento_Show_Col_RecortesDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Recortes
                Me.Seguimiento_Show_Col_CalidadRecortesDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Calidad_Recortes
                Me.Seguimiento_Show_Col_Validacion_ListasDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_Validacion_Listas
                Me.Seguimiento_Show_Col_Correccion_CapturaDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_CorreccionCaptura
                Me.Seguimiento_Show_Col_OCR_CapturaDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_OCRCaptura
                Me.Seguimiento_Show_Col_OCR_IndexacionDesktopCheckBox.Checked = ProyectoRow.Seguimiento_Show_Col_OCRIndexacion

                'Proceso
                Me.Usa_Exportacion_Validos.Checked = ProyectoRow.Usa_Exportacion_Validos
                Me.Exportar_Unico_Archivo_TIFF.Checked = ProyectoRow.Exportar_Unico_Archivo_TIFF
                Me.Usa_Fecha_Proceso_Cerrada_Captura_Adicional.Checked = ProyectoRow.Usa_Fecha_Proceso_Cerrada_Captura_Adicional
                Me.Usa_Cruce_Linea.Checked = ProyectoRow.Usa_Cruce_Linea
                Me.Usa_Cargue_Masivo.Checked = ProyectoRow.Usa_Cargue_Masivo
                Me.Usa_Fecha_Proceso_Cerrada_F11.Checked = ProyectoRow.Usa_Fecha_Proceso_Cerrada_F11
                Me.Usa_Reconocimiento_CBarras.Checked = ProyectoRow.Usa_Reconocimiento_CBarras
                Me.Usa_Carga_Datos_Captura.Checked = ProyectoRow.Usa_Carga_Datos_Captura
                Me.Usa_Renombramiento_Imagen_Exportacion.Checked = ProyectoRow.Usa_Renombramiento_Imagen_Exportacion
                Me.Usa_Cargue_PDF.Checked = ProyectoRow.Usa_Cargue_PDF
                Me.Usa_Exportacion_PDF.Checked = ProyectoRow.Usa_Exportacion_PDF
                Me.Usa_Cruce_Generico.Checked = ProyectoRow.Usa_Cruce_Generico
                Me.Usa_Cargue_Log_Generico.Checked = ProyectoRow.Usa_Cargue_Log_Generico
                Me.Usa_Correccion_Captura_Maquina.Checked = ProyectoRow.Usa_Correccion_Captura_Maquina
                Me.Usa_Transmision_Data_Maquina.Checked = ProyectoRow.Usa_Transmision_Data_Maquina
                Me.Usa_Guardar_Nombre_Imagen.Checked = ProyectoRow.Usa_Guardar_Nombre_Imagen
                Me.Usa_Destape_Masivo.Checked = ProyectoRow.Usa_Destape_Masivo
                Me.Usa_Campo_Llave.Checked = ProyectoRow.Usa_Campo_Llave
                Me.Usa_OCR_Captura.Checked = ProyectoRow.Usa_OCR_Captura
                Me.Usa_OCR_indexacion.Checked = ProyectoRow.Usa_OCR_Indexacion

                'Rotulos
                Me.Usa_Rotulos_Carpetas.Checked = ProyectoRow.Usa_Rotulo_de_Carpeta
                Me.Usa_Rotulos_de_Cajas.Checked = ProyectoRow.Usa_Rotulo_de_Cajas

                'Hoja control
                Me.Usa_Hoja_Control.Checked = ProyectoRow.Usa_Hoja_Control

                'Generar Fuid

                'Validaciones de empaque
                Me.Usa_Validacion_Empaque_Contenedor.Checked = ProyectoRow.Usa_Validacion_Empaque_Contenedor
                Me.Usa_Validacion_Empaque_Contenedor_OT.Checked = ProyectoRow.Usa_Validacion_Empaque_Contenedor_OT
                Me.Usa_Validacion_Empaque_Campo.Checked = ProyectoRow.Usa_Validacion_Empaque_Campo
                Me.Usa_Validacion_Empaque_Campo_OT.Checked = ProyectoRow.Usa_Validacion_Empaque_Campo_OT
                Me.Usa_Validacion_Empaque_Contenedor_Destape_OT.Checked = ProyectoRow.Usa_Validacion_Empaque_Contenedor_Destape_OT
                Me.Usa_Validacion_Empaque_Contenedor_Destape.Checked = ProyectoRow.Usa_Validacion_Empaque_Contenedor_Destape

                CambiaRangoPaquetes()
                CambiaArchivoIndices()
                CambiaUsaIndexacion()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("SeleccionarProyecto", ex)
            Finally
                dmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub GuardarCambios()
            If Validar() Then
                Dim dmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    dmImaging.Transaction_Begin()

                    Dim ProyectoType As New DBImaging.SchemaConfig.TBL_ProyectoType

                    ProyectoType.fk_Entidad = CShort(fk_entidadDesktopComboBox.SelectedValue)
                    ProyectoType.fk_Proyecto = CShort(fk_proyectoDesktopComboBox.SelectedValue)

                    ' Cargue
                    ProyectoType.Input_Folder = Me.InputFolderDesktopTextBox.Text
                    ProyectoType.Usa_Archivo_Indices = Me.Usa_Archivo_IndicesDesktopCheckBox.Checked
                    ProyectoType.Usa_Encabezado_Columnas = Me.Usa_Encabezado_ColumnasDesktopCheckBox.Checked
                    ProyectoType.Nombre_Archivo_Indices = Me.Nombre_Archivo_IndicesDesktopTextBox.Text

                    If (Me.fk_SeparadorDesktopComboBox.SelectedIndex >= 0) Then
                        ProyectoType.fk_Separador = Me.fk_SeparadorDesktopComboBox.SelectedValue.ToString()
                    Else
                        ProyectoType.fk_Separador = DBNull.Value
                    End If

                    If (Me.fk_Identificador_TextoDesktopComboBox.SelectedIndex >= 0) Then
                        ProyectoType.fk_Identificador_Texto = Me.fk_Identificador_TextoDesktopComboBox.SelectedValue.ToString()
                    Else
                        ProyectoType.fk_Identificador_Texto = DBNull.Value
                    End If

                    If Not IsNumeric(EmpaqueMinDesktopTextBox.Text) Then EmpaqueMinDesktopTextBox.Text = "0"
                    If Not IsNumeric(EmpaqueMaxDesktopTextBox.Text) Then EmpaqueMaxDesktopTextBox.Text = "0"

                    ProyectoType.Caracteres_Omitir = Byte.Parse(Me.Caracteres_OmitirDesktopTextBo.Text)
                    ProyectoType.Columnas_Archivo = Short.Parse(Me.ColumnasArchivoDesktopTextBox.Text)
                    ProyectoType.Columna_Imagen = Short.Parse(Me.ColumnaImagenDesktopTextBox.Text)
                    ProyectoType.Columna_Key = Short.Parse(Me.ColumnaKeyDesktopTextBox.Text)

                    ' Formato
                    ProyectoType.fk_Formato_Entrada = CType(Me.FormatoEntradaDesktopComboBox.SelectedValue, Short)
                    ProyectoType.fk_Formato_Salida = CType(Me.FormatoSalidaDesktopComboBox.SelectedValue, Short)

                    ' Servidor
                    ProyectoType.fk_Entidad_Servidor = CType(Me.EntidadServidorDesktopComboBox.SelectedValue, Short)
                    ProyectoType.fk_Servidor = CType(Me.ServidorDesktopComboBox.SelectedValue, Short)

                    ' Paquetes
                    ProyectoType.Usa_Rango_Paquetes = Me.Usa_Rango_PaquetesDesktopCheckBox.Checked
                    ProyectoType.Inicio_Nombre_Paquete = Me.Inicio_Nombre_PaqueteDesktopTextBox.Text
                    ProyectoType.Formato_Fecha_Paquete = Me.Formato_Fecha_PaqueteDesktopTextBox.Text
                    ProyectoType.Usa_Paquete_x_Imagen = Me.Usa_Paquete_x_ImagenDesktopCheckBox.Checked
                    ProyectoType.Captura_Llaves_Paquete = Me.CapturaLlavesPaqueteDesktopCheckBox.Checked

                    ' Destape
                    ProyectoType.Usa_Destape_Contenedor = Me.UsaDestapeContenedorDesktopCheckBox.Checked
                    ProyectoType.Usa_Codigo_Contenedor = Me.UsaCodigoContenedorDesktopCheckBox.Checked
                    ProyectoType.Usa_Cantidades_Enviadas_Recibidas = Me.UsaCantidadesEnviadasyRecibidasDesktopCheckBox.Checked
                    ProyectoType.Muestra_Cantidad_Recibida = Me.MuestraCantidadRecibidaDesktopCheckBox.Checked
                    ProyectoType.Correspondencia_Destape_Vs_Imagenes = Me.CorrespondenciaDestapeVsImagenesCheckBox.Checked
                    ProyectoType.Cantidad_Maxima_OTxTipo = CInt(Me.CantidadOTDesktopTextBox.Text)
                    ProyectoType.Notificacion_Cierre_OT = Me.NotificacionCierreOTCheckBox.Checked
                    ProyectoType.Notificacion_Cierre_FechaProceso = Me.NotificacionCierreFechaProcesoCheckBox.Checked

                    ' Empaque
                    ProyectoType.Empaque_Min = CShort(Me.EmpaqueMinDesktopTextBox.Text)
                    ProyectoType.Empaque_Max = CShort(Me.EmpaqueMaxDesktopTextBox.Text)
                    ProyectoType.Requiere_Exportacion = Me.RequiereExportacionDesktopCheckBox.Checked

                    ' Indexación            
                    ProyectoType.Usa_Indexacion = Me.Usa_IndexacionDesktopCheckBox.Checked
                    ProyectoType.Usa_Folder_Unico = Me.Usa_Folder_UnicoDesktopCheckBox.Checked
                    ProyectoType.Usa_File_Unico = Me.Usa_File_UnicoDesktopCheckBox.Checked
                    ProyectoType.Show_Information = Me.Show_InformationDesktopCheckBox.Checked
                    ProyectoType.Usa_Columna_Esquema = Me.Usa_Columna_EsquemaDesktopCheckBox.Checked
                    ProyectoType.Columna_Esquema = Short.Parse(Me.Columna_EsquemaDesktopTextBox.Text)
                    ProyectoType.Default_Esquema = Short.Parse(Me.Default_EsquemaDesktopTextBox.Text)
                    ProyectoType.Usa_Columna_Documento = Me.Usa_Columna_DocumentoDesktopCheckBox.Checked
                    ProyectoType.Columna_Documento = Short.Parse(Me.Columna_DocumentoDesktopTextBox.Text)
                    ProyectoType.Default_Documento = Short.Parse(Me.Default_DocumentoDesktopTextBox.Text)
                    ProyectoType.Usa_Calidad = Me.Usa_CalidadDesktopCheckBox.Checked
                    ProyectoType.Usa_Recortes = Me.Usa_RecortesDesktopCheckBox.Checked
                    ProyectoType.Usa_Dominio_Externo = Me.UsaDominioExternoDesktopCheckBox.Checked


                    ' Seguimiento
                    ProyectoType.Seguimiento_Show_Col_Observaciones = Me.Seguimiento_Show_Col_ObservacionesDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Key01 = Me.Seguimiento_Show_Col_Key01DesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Key02 = Me.Seguimiento_Show_Col_Key02DesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Key03 = Me.Seguimiento_Show_Col_Key03DesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Paquetes = Me.Seguimiento_Show_Col_PaquetesDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_fk_Paquete = Me.Seguimiento_Show_Col_fk_PaqueteDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Items = Me.Seguimiento_Show_Col_ItemsDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Indexacion = Me.Seguimiento_Show_Col_IndexacionDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_PreCaptura = Me.Seguimiento_Show_Col_PreCapturaDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_PrimeraCaptura = Me.Seguimiento_Show_Col_PrimeraCapturaDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_SegundaCaptura = Me.Seguimiento_Show_Col_SegundaCapturaDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_TerceraCaptura = Me.Seguimiento_Show_Col_TerceraCapturaDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Calidad = Me.Seguimiento_Show_Col_CalidadDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Indexado = Me.Seguimiento_Show_Col_IndexadoDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Validaciones_Totales = Me.Seguimiento_Show_Col_Validaciones_TotalesDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Validaciones_Pendientes = Me.Seguimiento_Show_Col_Validaciones_PendientesDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Retenido = Me.Seguimiento_Show_Col_RetenidoDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Reproceso = Me.Seguimiento_Show_Col_ReprocesoDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Recortes = Me.Seguimiento_Show_Col_RecortesDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Calidad_Recortes = Me.Seguimiento_Show_Col_CalidadRecortesDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_Validacion_Listas = Me.Seguimiento_Show_Col_Validacion_ListasDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_CorreccionCaptura = Me.Seguimiento_Show_Col_Correccion_CapturaDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_OCRCaptura = Me.Seguimiento_Show_Col_OCR_CapturaDesktopCheckBox.Checked
                    ProyectoType.Seguimiento_Show_Col_OCRIndexacion = Me.Seguimiento_Show_Col_OCR_IndexacionDesktopCheckBox.Checked

                    'Proceso
                    ProyectoType.Usa_Exportacion_Validos = Me.Usa_Exportacion_Validos.Checked
                    ProyectoType.Exportar_Unico_Archivo_TIFF = Me.Exportar_Unico_Archivo_TIFF.Checked
                    ProyectoType.Usa_Fecha_Proceso_Cerrada_Captura_Adicional = Me.Usa_Fecha_Proceso_Cerrada_Captura_Adicional.Checked
                    ProyectoType.Usa_Cruce_Linea = Me.Usa_Cruce_Linea.Checked
                    ProyectoType.Usa_Cargue_Masivo = Me.Usa_Cargue_Masivo.Checked
                    ProyectoType.Usa_Fecha_Proceso_Cerrada_F11 = Me.Usa_Fecha_Proceso_Cerrada_F11.Checked
                    ProyectoType.Usa_Reconocimiento_CBarras = Me.Usa_Reconocimiento_CBarras.Checked
                    ProyectoType.Usa_Carga_Datos_Captura = Me.Usa_Carga_Datos_Captura.Checked
                    ProyectoType.Usa_Renombramiento_Imagen_Exportacion = Me.Usa_Renombramiento_Imagen_Exportacion.Checked
                    ProyectoType.Usa_Cargue_PDF = Me.Usa_Cargue_PDF.Checked
                    ProyectoType.Usa_Exportacion_PDF = Me.Usa_Exportacion_PDF.Checked
                    ProyectoType.Usa_Cruce_Generico = Me.Usa_Cruce_Generico.Checked
                    ProyectoType.Usa_Cargue_Log_Generico = Me.Usa_Cargue_Log_Generico.Checked
                    ProyectoType.Usa_Correccion_Captura_Maquina = Me.Usa_Correccion_Captura_Maquina.Checked
                    ProyectoType.Usa_Transmision_Data_Maquina = Me.Usa_Transmision_Data_Maquina.Checked
                    ProyectoType.Usa_Guardar_Nombre_Imagen = Me.Usa_Guardar_Nombre_Imagen.Checked
                    ProyectoType.Usa_Destape_Masivo = Me.Usa_Destape_Masivo.Checked
                    ProyectoType.Usa_Campo_Llave = Me.Usa_Campo_Llave.Checked
                    ProyectoType.Usa_OCR_Captura = Me.Usa_OCR_Captura.Checked
                    ProyectoType.Usa_OCR_Indexacion = Me.Usa_OCR_indexacion.Checked

                    'Rotulos
                    ProyectoType.Usa_Rotulo_de_Carpeta = Me.Usa_Rotulos_Carpetas.Checked
                    ProyectoType.Usa_Rotulo_de_Cajas = Me.Usa_Rotulos_de_Cajas.Checked

                    'Hoja de control
                    ProyectoType.Usa_Hoja_Control = Me.Usa_Hoja_Control.Checked

                    'Generar Fuid
                    ProyectoType.Usa_Generacion_de_Fuid = Me.UsaGenerarFuidCheckBox.Checked

                    'Validaciones de empaque
                    ProyectoType.Usa_Validacion_Empaque_Contenedor_Destape_OT = Me.Usa_Validacion_Empaque_Contenedor_Destape_OT.Checked
                    ProyectoType.Usa_Validacion_Empaque_Contenedor_Destape = Me.Usa_Validacion_Empaque_Contenedor_Destape.Checked
                    ProyectoType.Usa_Validacion_Empaque_Contenedor_OT = Me.Usa_Validacion_Empaque_Contenedor_OT.Checked
                    ProyectoType.Usa_Validacion_Empaque_Campo = Me.Usa_Validacion_Empaque_Campo.Checked
                    ProyectoType.Usa_Validacion_Empaque_Campo_OT = Me.Usa_Validacion_Empaque_Campo_OT.Checked
                    ProyectoType.Usa_Validacion_Empaque_Contenedor = Me.Usa_Validacion_Empaque_Contenedor.Checked

                    'Auditoria
                    ProyectoType.fk_Usuario_Log = Program.Sesion.Usuario.id
                    ProyectoType.Fecha_Log = SlygNullable.SysDate
                    ' Insertar o Actualizar la configuración del proyecto
                    If idProyecto = 0 Then
                        dmImaging.SchemaConfig.TBL_Proyecto.DBInsert(ProyectoType)
                    Else
                        dmImaging.SchemaConfig.TBL_Proyecto.DBUpdate(ProyectoType, ProyectoType.fk_Entidad, ProyectoType.fk_Proyecto)
                        idProyecto = CShort(fk_proyectoDesktopComboBox.SelectedValue)

                        ' Se eliminan los valores almacenados para este proyecto.
                        dmImaging.SchemaConfig.TBL_Proyecto_Llave_Paquete.DBDelete(ProyectoType.fk_Entidad, ProyectoType.fk_Proyecto, Nothing)
                    End If

                    'Llaves posición del proyecto
                    If CapturaLlavesPaqueteDesktopCheckBox.Checked Then
                        For Each LlavePosicion In _ListLlaves
                            dmImaging.SchemaConfig.TBL_Proyecto_Llave_Paquete.DBInsert(ProyectoType.fk_Entidad, ProyectoType.fk_Proyecto, LlavePosicion.Id_Llave, LlavePosicion.Posicion_Inicial, LlavePosicion.Posicion_Longitud)
                        Next
                    End If

                    dmImaging.Transaction_Commit()
                    DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito", "Prametrización Proyecto", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    dmImaging.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("Se generarón problemas al guardar los datos, por favor comuniquese con el administrador", "Problemas en Proyecto Imaging", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                Finally
                    dmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Public Sub CambiaRangoPaquetes()
            Me.PaquetesGroupBox.Enabled = Me.Usa_Rango_PaquetesDesktopCheckBox.Checked

            If (Not Me.Usa_Rango_PaquetesDesktopCheckBox.Checked) Then
                Me.Inicio_Nombre_PaqueteDesktopTextBox.Text = ""
                Me.Formato_Fecha_PaqueteDesktopTextBox.Text = ""
            End If
        End Sub

        Public Sub CambiaArchivoIndices()
            Me.ArchivosGroupBox.Enabled = Me.Usa_Archivo_IndicesDesktopCheckBox.Checked

            If (Not Me.Usa_Archivo_IndicesDesktopCheckBox.Checked) Then
                Me.Usa_Encabezado_ColumnasDesktopCheckBox.Checked = False
                Me.fk_SeparadorDesktopComboBox.SelectedIndex = -1
                Me.Nombre_Archivo_IndicesDesktopTextBox.Text = ""
                Me.fk_Identificador_TextoDesktopComboBox.SelectedIndex = -1
                Me.Caracteres_OmitirDesktopTextBo.Text = "0"
            End If
        End Sub

        Private Sub CambiaUsaIndexacion()
            Me.IndexacionManualGroupBox.Enabled = Me.Usa_IndexacionDesktopCheckBox.Checked
            Me.IndexacionAutomaticaGroupBox.Enabled = Not Me.Usa_IndexacionDesktopCheckBox.Checked

            If (Not Me.Usa_IndexacionDesktopCheckBox.Checked) Then
                Me.Usa_Folder_UnicoDesktopCheckBox.Checked = False
                Me.Usa_File_UnicoDesktopCheckBox.Checked = False
                Me.Show_InformationDesktopCheckBox.Checked = False
            Else
                Me.Usa_Columna_EsquemaDesktopCheckBox.Checked = False
                Me.Columna_EsquemaDesktopTextBox.Text = "0"
                Me.Default_EsquemaDesktopTextBox.Text = "0"

                Me.Usa_Columna_DocumentoDesktopCheckBox.Checked = False
                Me.Columna_DocumentoDesktopTextBox.Text = "0"
                Me.Default_DocumentoDesktopTextBox.Text = "0"
            End If

            CambiaUsaColumnaEsquema()
            CambiaUsaColumnaDocumento()
        End Sub

        Private Sub CambiaUsaColumnaEsquema()
            Me.Columna_EsquemaDesktopTextBox.Enabled = Me.Usa_Columna_EsquemaDesktopCheckBox.Checked
            Me.Default_EsquemaDesktopTextBox.Enabled = Not Me.Usa_Columna_EsquemaDesktopCheckBox.Checked
        End Sub

        Private Sub CambiaUsaColumnaDocumento()
            Me.Columna_DocumentoDesktopTextBox.Enabled = Me.Usa_Columna_DocumentoDesktopCheckBox.Checked
            Me.Default_DocumentoDesktopTextBox.Enabled = Not Me.Usa_Columna_DocumentoDesktopCheckBox.Checked
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            Dim validacion As Boolean = True
            Dim erroresStringBuilder As New StringBuilder

            If IsNothing(EntidadServidorDesktopComboBox.SelectedValue) Then
                erroresStringBuilder.AppendLine("La entidad del servidor es obligatoria.")
                validacion = False
            End If

            If IsNothing(ServidorDesktopComboBox.SelectedValue) Then
                erroresStringBuilder.AppendLine("El servidor es obligatorio.")
                validacion = False
            End If

            If InputFolderDesktopTextBox.Text = "" Then
                erroresStringBuilder.AppendLine("El valor de input folder no puede ser vacio.")
                validacion = False
            End If

            If ColumnasArchivoDesktopTextBox.Text = "" Then
                erroresStringBuilder.AppendLine("El valor de Columnas archivo no puede ser vacio.")
                validacion = False
            End If

            If ColumnaImagenDesktopTextBox.Text = "" Then
                erroresStringBuilder.AppendLine("El valor de Columna imagen no puede ser vacio.")
                validacion = False
            End If

            If Caracteres_OmitirDesktopTextBo.Text = "" Then
                erroresStringBuilder.AppendLine("El valor de Caracteres a omitir no puede ser vacio.")
                validacion = False
            End If

            If ColumnaKeyDesktopTextBox.Text = "" Then
                erroresStringBuilder.AppendLine("El valor de Columna key  no puede ser vacio.")
                validacion = False
            End If

            If CapturaLlavesPaqueteDesktopCheckBox.Checked Then
                If _ListLlaves Is Nothing Then
                    erroresStringBuilder.AppendLine("No se han asignado los valores de las posiciones de los paquetes.")
                    validacion = False
                End If
            End If

            If (Usa_Rango_PaquetesDesktopCheckBox.Checked And Not Usa_Archivo_IndicesDesktopCheckBox.Checked) Then
                erroresStringBuilder.AppendLine("La opción de rango de paquetes solo se puede usar combinada con archivo de indices.")
                validacion = False
            End If

            If (Not UsaDestapeContenedorDesktopCheckBox.Checked And Usa_IndexacionDesktopCheckBox.Checked) Then
                erroresStringBuilder.AppendLine("La indexaciòn solo es vàlida si se realiza el destape a nivel de contenedor.")
                validacion = False
            End If



            If Not validacion Then
                DesktopMessageBoxControl.DesktopMessageShow(erroresStringBuilder.ToString(), "Error de datos", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End If

            Return validacion
        End Function

#End Region

    End Class
End Namespace