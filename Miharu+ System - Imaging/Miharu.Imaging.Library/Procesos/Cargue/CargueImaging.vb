Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools

Namespace Procesos.Cargue

    Public Class CargueImaging
        Inherits CargueBase

#Region " VARIABLES "
        Private Usa_Cargue_PDF As Boolean
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
#End Region


#Region " Metodos "

        Public Sub New(nFechaProceso As DateTime, ot As Integer, nEventManager As EventManager)
            _FechaProceso = nFechaProceso
            _OT = ot
            _EventManager = nEventManager

            Usa_Cargue_PDF = Program.ImagingGlobal.ProyectoImagingRow.Usa_Cargue_PDF
            formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
            compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

            Load_FormatoCargue()
        End Sub

        Protected Overrides Sub Validar()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim i As Integer = 0

            ProgressForm.SetAccion("Leer directorios")
            ProgressForm.SetProgreso(0)
            ProgressForm.SetMaxValue(_Cargue.Item.Rows.Count)
            Application.DoEvents()

            _Validos = 0
            _Invalidos = 0

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                ' Actualizar los datos del cargue
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In _Cargue.Paquete.Rows
                    rowPaquete.PaqueteValido = True

                    ' Validar destape
                    If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor AndAlso Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                        ' Validar el contenedor
                        Dim contenedorDataTable = dbmImaging.SchemaProcess.TBL_Contenedor.DBFindByfk_OTToken(_OT, rowPaquete.Key)

                        If (contenedorDataTable.Count = 0) Then
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "No se encontró un contenedor que coincida en la OT"
                        ElseIf (contenedorDataTable(0).Cargado) Then
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "El contenedor ya fué cargado"
                        Else
                            ' Validar que el precinto este cerrado
                            Dim precintoDataTable = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(_OT, contenedorDataTable(0).fk_Precinto)

                            If (Not precintoDataTable(0).Cerrado) Then
                                rowPaquete.PaqueteValido = False
                                rowPaquete.Descripcion = "El Precinto: " & precintoDataTable(0).Precinto & ", no esta cerrado"
                            Else
                                rowPaquete.Precinto = contenedorDataTable(0).fk_Precinto
                                rowPaquete.Contenedor = contenedorDataTable(0).id_Contenedor
                                rowPaquete.CBarrasContenedor = contenedorDataTable(0).Token
                            End If
                        End If
                    End If

                    'Si maneja llaves por paquete, valida que exista.                    
                    If (Program.ImagingGlobal.ProyectoImagingRow.Captura_Llaves_Paquete) Then
                        Dim filtro(9) As SlygNullable(Of String)
                        Dim j As Integer = 0

                        Try
                            For Each Llave In Program.ImagingGlobal.ProyectoImagingLlaveDataTable
                                Select Case Llave.fk_Campo_Tipo
                                    Case DesktopConfig.CampoTipo.Texto
                                        filtro(j) = rowPaquete.Key.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)

                                    Case DesktopConfig.CampoTipo.Numerico
                                        filtro(j) = CLng(rowPaquete.Key.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)).ToString()

                                    Case DesktopConfig.CampoTipo.Fecha
                                        Dim fechaPaquete = rowPaquete.Key.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)
                                        filtro(j) = fechaPaquete.Substring(0, 4) & "/" & fechaPaquete.Substring(4, 2) & "/" & fechaPaquete.Substring(6, 2)

                                    Case DesktopConfig.CampoTipo.Lista
                                        filtro(j) = CLng(rowPaquete.Key.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)).ToString()
                                End Select
                                j += 1
                            Next
                        Catch ex As Exception
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "El paquete [" & rowPaquete.Key & "] no cumple con los parámetros para obtener las llaves, por favor revisar la configuración del proyecto."
                        End Try
                    End If

                    'Se analizan los Items simpre y cuándo el paquete sea válido ó no se maneje captura de llaves por paquete.                    
                    Dim items = CType(_Cargue.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete), DBImaging.Esquemas.xsdCargue.ItemRow())
                    rowPaquete.Total = 0

                    For Each rowItem As DBImaging.Esquemas.xsdCargue.ItemRow In items
                        ' Validar que la imagen exista físicamente
                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Archivo_Indices) Then
                            If Not File.Exists(rowPaquete.Path & rowItem.Path) Then
                                rowItem.Valido = False
                                rowItem.Descripcion = "No se encontro el archivo físico."
                            End If
                        End If

                        If (rowPaquete.PaqueteValido) Then
                            ' Validar destape del documento
                            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then

                                    ' Validar el contenedor
                                    Dim contenedorDataTable = dbmImaging.SchemaProcess.TBL_Contenedor.DBFindByfk_OTToken(_OT, rowItem.Key)

                                    If (contenedorDataTable.Count = 0) Then
                                        rowPaquete.PaqueteValido = False
                                        rowPaquete.Descripcion = "No se encontró un contenedor que coincida en la OT"
                                    ElseIf (contenedorDataTable(0).Cargado) Then
                                        rowPaquete.PaqueteValido = False
                                        rowPaquete.Descripcion = "El contenedor ya fué cargado"
                                    Else
                                        ' Validar que el precinto este cerrado
                                        Dim precintoDataTable = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(_OT, contenedorDataTable(0).fk_Precinto)

                                        If (Not precintoDataTable(0).Cerrado) Then
                                            rowPaquete.PaqueteValido = False
                                            rowPaquete.Descripcion = "El Precinto: " & precintoDataTable(0).Precinto & ", no esta cerrado"
                                        Else
                                            rowPaquete.Precinto = contenedorDataTable(0).fk_Precinto
                                            rowPaquete.Contenedor = contenedorDataTable(0).id_Contenedor
                                            rowPaquete.CBarrasContenedor = contenedorDataTable(0).Token
                                        End If
                                    End If
                                End If
                            Else ' Destape por documento
                                ' Validar el contenedor
                                Dim detalleDataTable = dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBFindByfk_OTToken(_OT, rowItem.Key)

                                If (detalleDataTable.Count = 0) Then
                                    rowItem.Valido = False
                                    rowItem.Descripcion = "No se encontró un Documento que coincida en la OT"
                                ElseIf (detalleDataTable(0).Cargado) Then
                                    rowItem.Valido = False
                                    rowItem.Descripcion = "El documento ya fué cargado"
                                Else
                                    ' Validar que el precinto este cerrado
                                    Dim precintoDataTable = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(_OT, detalleDataTable(0).fk_Precinto)

                                    If (Not precintoDataTable(0).Cerrado) Then
                                        rowItem.Valido = False
                                        rowItem.Descripcion = "El Precinto: " & precintoDataTable(0).Precinto & ", no esta cerrado"
                                    Else
                                        Dim contenedorDataTable = dbmImaging.SchemaProcess.TBL_Contenedor.DBGet(_OT, detalleDataTable(0).fk_Precinto, detalleDataTable(0).fk_Contenedor)

                                        rowItem.Esquema = contenedorDataTable(0).fk_Esquema
                                        rowItem.Documento = detalleDataTable(0).fk_Documento
                                        rowItem.Precinto = contenedorDataTable(0).fk_Precinto
                                        rowItem.Contenedor = contenedorDataTable(0).id_Contenedor
                                        rowItem.CBarrasContenedor = contenedorDataTable(0).Token
                                        rowPaquete.CBarrasContenedor = contenedorDataTable(0).Token
                                    End If
                                End If
                            End If

                            'If (RowItem.Valido And Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                            '    ' Validar si el item ya fue cargado
                            '    Cargue = dbmImaging.SchemaProcess.PA_Basic_TBL_Cargue_Item_validate.DBExecute(
                            '        Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad,
                            '        Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto,
                            '        Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                            '        Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede,
                            '        Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento,
                            '        RowItem.Path,
                            '        RowItem.Key, RowItem.Tamaño)

                            '    If Cargue > 0 Then
                            '        RowItem.Valido = False
                            '        RowItem.Descripcion = "Se encontró un item con características similares en el cargue: " & Cargue
                            '    End If
                            'End If
                        Else
                            rowItem.Valido = False
                        End If

                        If (rowItem.Valido) Then
                            rowPaquete.Validos += 1
                            _Validos += 1
                        Else
                            rowPaquete.Invalidos += 1
                            _Invalidos += 1
                        End If

                        rowPaquete.Total += 1

                        i += 1
                        ProgressForm.SetProgreso(i)
                        Application.DoEvents()
                    Next
                Next
            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Protected Overrides Sub CargarConIndexacion()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim cargueType As New DBImaging.SchemaProcess.TBL_CargueType()
            Dim progressFormEsp As New Progress.FormProgress()
            Dim manager As FileProviderManager = Nothing
            Dim carpetasRenombrar As New List(Of RenamePath)
            Dim fechaInicioProceso = Now

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.ServidorImagenRow.fk_Entidad, Program.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                ' Obtener el nuevo id para el cargue
                cargueType.fk_Entidad = Program.ImagingGlobal.Entidad
                cargueType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                cargueType.fk_Estado = DBCore.EstadoEnum.Creado
                cargueType.fk_Entidad_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                cargueType.fk_Sede_Procesamiento_Cargue = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                cargueType.fk_Centro_Procesamiento_Cargue = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                cargueType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                cargueType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                cargueType.fk_OT = _OT
                cargueType.Fecha_Proceso = Me._FechaProceso
                cargueType.Observaciones = "CP-" & Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
                cargueType.fk_Usuario_Log = Program.Sesion.Usuario.id

                cargueType.id_Cargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                    cargueType.fk_Entidad,
                    cargueType.fk_Proyecto,
                    cargueType.fk_Estado,
                    cargueType.fk_Entidad_Procesamiento,
                    cargueType.fk_Sede_Procesamiento_Cargue,
                    cargueType.fk_Centro_Procesamiento_Cargue,
                    cargueType.fk_Entidad_Servidor,
                    cargueType.fk_Servidor,
                    cargueType.fk_OT,
                    cargueType.Fecha_Proceso,
                    cargueType.Observaciones,
                    cargueType.fk_Usuario_Log
                    )
                '-----------------------------------------------------------------------------------------------------------------

                progressFormEsp.Show()

                progressFormEsp.Process = ""
                progressFormEsp.Action = ""
                progressFormEsp.ValueProcess = 0
                progressFormEsp.ValueAction = 0

                Application.DoEvents()
                '-----------------------------------------------------------------------------------------------------------------
                'Dim Usa_Cargue_PDF = Program.ImagingGlobal.ProyectoImagingRow.Usa_Cargue_PDF

                'Dim formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
                'Dim CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
                'Dim formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                'Dim compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                'Dim IdformatoEntradaAux = EventManager.IdFormatoImagen_Plugin(True)
                'Dim ExtensionPluginSalidaAux = EventManager.ExtensionImagen_Plugin(True)

                'If (Not Usa_Cargue_PDF) Then
                '    formatoAux = formato
                '    CompresionAux = compresion
                'Else
                '    formatoAux = ImageManager.EnumFormat.Pdf
                '    CompresionAux = Utilities.GetEnumCompression(CType(formatoAux, DesktopConfig.FormatoImagenEnum))
                'End If

                Dim totalPaquetes As Integer = Datos.Paquete.Select("PaqueteValido = 1").Length
                Dim paquete As Integer = 0

                progressFormEsp.MaxValueProcess = totalPaquetes

                ' Actualizar la data de los paquetes
                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In Datos.Paquete.Rows
                    If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                    'Se procesa si el paquete es válido.
                    If (rowPaquete.PaqueteValido) Then
                        Dim items As Integer = Datos.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete & " AND Valido = 1").Length

                        If (items > 0) Then
                            paquete += 1
                            progressFormEsp.Process = "Cargar paquete " & paquete & " de " & totalPaquetes
                            progressFormEsp.ValueProcess = paquete

                            '-----------------------------------------------------------------------------------------------------------------
                            progressFormEsp.Action = "Cargar items"
                            progressFormEsp.ValueAction = 0
                            progressFormEsp.MaxValueAction = items

                            Application.DoEvents()
                            '-----------------------------------------------------------------------------------------------------------------

                            Dim paqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()

                            paqueteType.fk_Cargue = cargueType.id_Cargue
                            paqueteType.id_Cargue_Paquete = rowPaquete.id_Paquete
                            paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                            paqueteType.fk_Usuario_Log = Program.Sesion.Usuario.id
                            paqueteType.Fecha_Proceso = cargueType.Fecha_Proceso
                            paqueteType.Path_Cargue_Paquete = rowPaquete.Path
                            paqueteType.Bloqueado = False
                            paqueteType.Data_Path = rowPaquete.Path
                            paqueteType.fk_Sede_Procesamiento_Asignada = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
                            paqueteType.fk_Centro_Procesamiento_Asignado = Program.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado

                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                            ' Actualizar el contenedor                            
                            Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                            contenedorType.Cargado = True
                            contenedorType.fk_Cargue = cargueType.id_Cargue
                            contenedorType.fk_Paquete = rowPaquete.id_Paquete
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, _OT, rowPaquete.Precinto, rowPaquete.Contenedor)


                            carpetasRenombrar.Add(New RenamePath(rowPaquete.Path, cargueType.id_Cargue, rowPaquete.id_Paquete))

                            Dim item As Integer = 0

                            progressFormEsp.MaxValueAction = items

                            For Each rowItem As DBImaging.Esquemas.xsdCargue.ItemRow In rowPaquete.GetItemRows()
                                If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                If (rowItem.Valido) Then
                                    item += 1

                                    Dim itemType As New DBImaging.SchemaProcess.TBL_Cargue_ItemType()

                                    itemType.fk_Cargue = cargueType.id_Cargue
                                    itemType.fk_Cargue_Paquete = rowItem.fk_Paquete
                                    itemType.id_Cargue_Item = rowItem.id_Item
                                    itemType.Folios_Cargue_Item = rowItem.Folios
                                    itemType.Tamaño_Cargue_Item = rowItem.Tamaño
                                    itemType.Path_Cargue_Item = Path.GetFileName(rowItem.Path)
                                    itemType.Key_Cargue_Item = rowItem.Key
                                    itemType.fk_Estado = DBCore.EstadoEnum.Indexacion
                                    itemType.Bloqueado = False
                                    itemType.fk_Usuario_Log = Program.Sesion.Usuario.id

                                    dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(itemType)

                                    manager.CreateItem(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item)


                                    'Se crean los Folios del item.
                                    For folio As Short = 1 To rowItem.Folios
                                        If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                        '20210223 - Insertar Folio
                                        Dim folioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                                        folioType.fk_Cargue = itemType.fk_Cargue
                                        folioType.fk_Cargue_Paquete = itemType.fk_Cargue_Paquete
                                        folioType.fk_Cargue_Item = itemType.id_Cargue_Item
                                        folioType.id_Folio = folio
                                        folioType.Indexado = False
                                        dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(folioType)

                                        If Not formatoAux = ImageManager.EnumFormat.Pdf Then
                                            Dim dataImage = ImageManager.GetFolioData(rowPaquete.Path & rowItem.Path, folio, formato, compresion)
                                            Dim dataImageThumbnail = ImageManager.GetThumbnailData(rowPaquete.Path & rowItem.Path, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight)

                                            manager.CreateFolio(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item, folio, dataImage, dataImageThumbnail(0), False)

                                            dataImage = Nothing
                                            dataImageThumbnail = Nothing

                                        Else
                                            Dim flags As FreeImageAPI.FREE_IMAGE_SAVE_FLAGS = Utilities.GetEnumDefaultFlags(formato)

                                            'Dim dataImage = ImageManager.GetFolioDataPdfOnly(rowPaquete.Path & rowItem.Path, folio, formato, compresion, flags)
                                            Dim dataImage = ImageManager.GetUnresizedFolioDataPdfOnly(rowPaquete.Path & rowItem.Path, folio, formato, compresion, flags)

                                            Using dataImageStream As Stream = New MemoryStream()
                                                dataImageStream.Write(dataImage, 0, dataImage.Length)

                                                Using bitmap = New FreeImageAPI.FreeImageBitmap(dataImageStream, ImageManager.GetImageFormat(formato))

                                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(bitmap, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                                                    manager.CreateFolio(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item, folio, dataImage, dataImageThumbnail(0), False)

                                                    dataImageThumbnail = Nothing
                                                End Using

                                            End Using

                                            dataImage = Nothing

                                        End If


                                        ' Insertar Folio  (SE MUEVE A UNAS LINEAS ARRIBA)

                                        Utilities.ClearMemory()
                                        Application.DoEvents()
                                    Next
                                End If

                                '-----------------------------------------------------------------------------------------------------------------
                                progressFormEsp.ValueAction = item
                                Application.DoEvents()
                                '-----------------------------------------------------------------------------------------------------------------
                            Next
                        End If
                    End If
                Next

                Dim cargueUpdateType As New DBImaging.SchemaProcess.TBL_CargueType()
                cargueUpdateType.fk_Estado = DBCore.EstadoEnum.Indexacion
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(cargueUpdateType, cargueType.id_Cargue)

                ' Renombrar paquete
                For Each CarpetaRenombrar In carpetasRenombrar
                    CarpetaRenombrar.RenameDirectory()
                Next

                '---------------------------------------------------------------------------
                ' Actualizar Dashboard
                '---------------------------------------------------------------------------
                dbmImaging.SchemaProcess.PA_Dashboard_Paquetes_insert.DBExecute(cargueType.id_Cargue)
                'dbmImaging.SchemaProcess.PA_Cargue_Indice_Insert.DBExecute(CargueType.id_Cargue)
                '---------------------------------------------------------------------------


                '-------------------------------------------------------------------------------------------------------------
                ' LOGGING - PERFORMANCE
                '-------------------------------------------------------------------------------------------------------------
                Dim fechaFinProceso = Now
                Dim traceMessage As String = ""
                traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                traceMessage &= "Cargue:" & vbTab & cargueType.id_Cargue.Value & vbTab
                DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Cargue][Cargar]")
                '-------------------------------------------------------------------------------------------------------------

                Application.DoEvents()
            Catch ex As Exception
                'Elimina el cargue realizado
                If (Not cargueType.id_Cargue Is Nothing AndAlso (dbmImaging IsNot Nothing) AndAlso (manager IsNot Nothing)) Then
                    ' Actualizar el contenedor
                    For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In Datos.Paquete.Rows
                        Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                        contenedorType.Cargado = False
                        contenedorType.fk_Cargue = DBNull.Value
                        contenedorType.fk_Paquete = DBNull.Value
                        dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, _OT, rowPaquete.Precinto, rowPaquete.Contenedor)
                    Next

                    dbmImaging.SchemaProcess.PA_Liberar_Contenedores_Cargue.DBExecute(_OT, cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(cargueType.id_Cargue, Nothing)
                    dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)
                    manager.DeleteCargue(cargueType.id_Cargue)
                End If

                progressFormEsp.Hide()
                Application.DoEvents()

                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return
            Finally
                progressFormEsp.Visible = False
                progressFormEsp.Close()

                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try

            EventManager.FinalizarCargue(Me._idCargue)

            Me._idCargue = cargueType.id_Cargue
            MessageBox.Show("El cargue se realizó con éxito con el identificador: " & cargueType.id_Cargue.ToString(), Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Sub

        Protected Overrides Sub CargarSinIndexacion()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim cargueType As New DBImaging.SchemaProcess.TBL_CargueType()
            Dim progressFormEsp As New Progress.FormProgress()
            Dim manager As FileProviderManager = Nothing
            Dim expedientes As New List(Of Long)
            Dim fechaInicioProceso = Now
            Dim carpetasRenombrar As New List(Of RenamePath)
            Dim paraTransferencia As Boolean

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.ServidorImagenRow.fk_Entidad, Program.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                ' Definir si los Folders se marcan para transferencia
                paraTransferencia = (Program.DesktopGlobal.ServidorImagenRow.fk_Entidad <> Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad_Servidor Or Program.DesktopGlobal.ServidorImagenRow.id_Servidor <> Program.ImagingGlobal.ProyectoImagingRow.fk_Servidor)

                ' Obtener el nuevo id para el cargue
                cargueType.fk_Entidad = Program.ImagingGlobal.Entidad
                cargueType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                cargueType.fk_Estado = DBCore.EstadoEnum.Creado
                cargueType.fk_Entidad_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                cargueType.fk_Sede_Procesamiento_Cargue = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                cargueType.fk_Centro_Procesamiento_Cargue = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                cargueType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                cargueType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                cargueType.fk_OT = _OT
                cargueType.Fecha_Proceso = Me._FechaProceso
                cargueType.Observaciones = "CP-" & Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
                cargueType.fk_Usuario_Log = Program.Sesion.Usuario.id

                cargueType.id_Cargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                    cargueType.fk_Entidad,
                    cargueType.fk_Proyecto,
                    cargueType.fk_Estado,
                    cargueType.fk_Entidad_Procesamiento,
                    cargueType.fk_Sede_Procesamiento_Cargue,
                    cargueType.fk_Centro_Procesamiento_Cargue,
                    cargueType.fk_Entidad_Servidor,
                    cargueType.fk_Servidor,
                    cargueType.fk_OT,
                    cargueType.Fecha_Proceso,
                    cargueType.Observaciones,
                    cargueType.fk_Usuario_Log
                    )

                Dim dataCargue = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(cargueType.id_Cargue)
                cargueType.Fecha_Cargue = dataCargue(0).Fecha_Cargue
                '-----------------------------------------------------------------------------------------------------------------

                progressFormEsp.Show()

                progressFormEsp.Process = ""
                progressFormEsp.Action = ""
                progressFormEsp.ValueProcess = 0
                progressFormEsp.ValueAction = 0

                Application.DoEvents()
                '-----------------------------------------------------------------------------------------------------------------

                Dim formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                Dim totalPaquetes As Integer = Datos.Paquete.Select("PaqueteValido = 1").Length
                Dim paquete As Integer = 0

                progressFormEsp.MaxValueProcess = totalPaquetes

                Dim idExpediente As Long = -1

                ' Actualizar la data de los paquetes
                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In Datos.Paquete.Rows
                    If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                    'Se procesa si el paquete es válido.
                    If (rowPaquete.PaqueteValido) Then
                        Dim items As Integer = Datos.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete & " AND Valido = 1").Length

                        If (items > 0) Then
                            paquete += 1
                            progressFormEsp.Process = "Cargar paquete " & paquete & " de " & totalPaquetes
                            progressFormEsp.ValueProcess = paquete

                            '-----------------------------------------------------------------------------------------------------------------
                            progressFormEsp.Action = "Cargar items"
                            progressFormEsp.ValueAction = 0
                            progressFormEsp.MaxValueAction = items

                            Application.DoEvents()
                            '-----------------------------------------------------------------------------------------------------------------

                            Dim paqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()

                            paqueteType.fk_Cargue = cargueType.id_Cargue
                            paqueteType.id_Cargue_Paquete = rowPaquete.id_Paquete
                            paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                            paqueteType.fk_Usuario_Log = Program.Sesion.Usuario.id
                            paqueteType.Fecha_Proceso = cargueType.Fecha_Cargue
                            paqueteType.Path_Cargue_Paquete = rowPaquete.Path
                            paqueteType.Bloqueado = False
                            paqueteType.Data_Path = rowPaquete.Path
                            paqueteType.fk_Sede_Procesamiento_Asignada = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
                            paqueteType.fk_Centro_Procesamiento_Asignado = Program.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado

                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                            ' Actualizar el contenedor
                            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                                Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                                contenedorType.Cargado = True
                                contenedorType.fk_Cargue = cargueType.id_Cargue
                                contenedorType.fk_Paquete = rowPaquete.id_Paquete
                                dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, _OT, rowPaquete.Precinto, rowPaquete.Contenedor)
                            End If

                            carpetasRenombrar.Add(New RenamePath(rowPaquete.Path, cargueType.id_Cargue, rowPaquete.id_Paquete))

                            Dim item As Integer = 0

                            progressFormEsp.MaxValueAction = items

                            For Each rowItem As DBImaging.Esquemas.xsdCargue.ItemRow In rowPaquete.GetItemRows()
                                If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                If (rowItem.Valido) Then
                                    item += 1

                                    ' Actualizar el contenedor
                                    If (item = 1 AndAlso Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                                        Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                                        contenedorType.Cargado = True
                                        contenedorType.fk_Cargue = cargueType.id_Cargue
                                        dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, _OT, rowItem.Precinto, rowItem.Contenedor)
                                    End If

                                    Dim itemType As New DBImaging.SchemaProcess.TBL_Cargue_ItemType()

                                    itemType.fk_Cargue = cargueType.id_Cargue
                                    itemType.fk_Cargue_Paquete = rowItem.fk_Paquete
                                    itemType.id_Cargue_Item = rowItem.id_Item
                                    itemType.Folios_Cargue_Item = rowItem.Folios
                                    itemType.Tamaño_Cargue_Item = rowItem.Tamaño
                                    itemType.Path_Cargue_Item = Path.GetFileName(rowItem.Path)
                                    itemType.Key_Cargue_Item = rowItem.Key
                                    itemType.fk_Estado = DBCore.EstadoEnum.Indexacion
                                    itemType.Bloqueado = False
                                    itemType.fk_Usuario_Log = Program.Sesion.Usuario.id

                                    dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(itemType)

                                    Dim newExpediente As Boolean = False
                                    Const idFolder As Short = 1
                                    Const idVersion As Short = 1
                                    Dim idFile As Short

                                    If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                                        idExpediente = -1
                                    End If

                                    ' Validar si el file se puede asociar a un expediente existente
                                    If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                                        idExpediente = dbmImaging.SchemaProcess.PA_Contenedor_Detalle_Expediente.DBExecute(_OT, rowItem.Precinto, rowItem.Contenedor)

                                        If (idExpediente <> -1) Then
                                            dbmCore.Transaction_Begin()
                                            idFile = dbmCore.SchemaProcess.TBL_File.DBNextId(idExpediente, idFolder)
                                            dbmCore.Transaction_Commit()
                                        End If
                                    End If

                                    ' Crear un nuevo Expediente
                                    If (idExpediente = -1) Then
                                        newExpediente = True
                                        idExpediente = dbmCore.SchemaProcess.PA_Insertar_Expediente.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, rowItem.Esquema, CShort(1), CShort(1), CShort(1), Nothing)
                                        idFile = 1
                                    Else
                                        idFile += CShort(1)
                                    End If

                                    ' Crear lista de expedientes
                                    If (Not expedientes.Contains(idExpediente)) Then
                                        expedientes.Add(idExpediente)
                                    End If

                                    If (newExpediente) Then
                                        ' Crear Folder Process
                                        Dim folderProType As New DBCore.SchemaProcess.TBL_FolderType()
                                        folderProType.fk_Expediente = idExpediente
                                        folderProType.id_Folder = idFolder
                                        folderProType.CBarras_Folder = rowPaquete.CBarrasContenedor
                                        folderProType.Fecha_Inicial = SlygNullable.SysDate
                                        folderProType.Fecha_Final = SlygNullable.SysDate
                                        dbmCore.SchemaProcess.TBL_Folder.DBInsert(folderProType)

                                        ' Crear Folder Imaging
                                        Dim folderImgType As New DBCore.SchemaImaging.TBL_FolderType()
                                        folderImgType.fk_Expediente = idExpediente
                                        folderImgType.fk_Folder = idFolder
                                        folderImgType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                                        folderImgType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                                        folderImgType.Fecha_Creacion = SlygNullable.SysDate
                                        folderImgType.Fecha_Transferencia = Nothing

                                        folderImgType.En_Transferencia = paraTransferencia

                                        If (paraTransferencia) Then
                                            folderImgType.fk_Entidad_Servidor_Transferencia = Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad_Servidor
                                            folderImgType.fk_Servidor_Transferencia = Program.ImagingGlobal.ProyectoImagingRow.fk_Servidor
                                        Else
                                            folderImgType.fk_Entidad_Servidor_Transferencia = Nothing
                                            folderImgType.fk_Servidor_Transferencia = Nothing
                                        End If

                                        folderImgType.fk_Cargue = paqueteType.fk_Cargue
                                        folderImgType.fk_Cargue_Paquete = paqueteType.id_Cargue_Paquete
                                        dbmCore.SchemaImaging.TBL_Folder.DBInsert(folderImgType)
                                    End If

                                    ' Crear el File
                                    Dim fileProType As New DBCore.SchemaProcess.TBL_FileType()
                                    fileProType.fk_Expediente = idExpediente
                                    fileProType.fk_Folder = idFolder
                                    fileProType.id_File = idFile
                                    fileProType.File_Unique_Identifier = Guid.NewGuid
                                    fileProType.fk_Documento = rowItem.Documento
                                    fileProType.Folios_File = rowItem.Folios
                                    fileProType.Monto_File = 0
                                    fileProType.CBarras_File = idFile.ToString()
                                    dbmCore.SchemaProcess.TBL_File.DBInsert(fileProType)

                                    Dim fileImgType As New DBCore.SchemaImaging.TBL_FileType()
                                    fileImgType.fk_Expediente = idExpediente
                                    fileImgType.fk_Folder = idFolder
                                    fileImgType.fk_File = idFile
                                    fileImgType.id_Version = idVersion
                                    fileImgType.File_Unique_Identifier = fileProType.File_Unique_Identifier
                                    fileImgType.Folios_Documento_File = rowItem.Folios
                                    fileImgType.Tamaño_Imagen_File = rowItem.Tamaño
                                    fileImgType.Nombre_Imagen_File = rowItem.Path
                                    fileImgType.Key_Cargue_Item = rowItem.Key
                                    fileImgType.Save_FileName = rowItem.Path
                                    fileImgType.fk_Content_Type = Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                    fileImgType.fk_Usuario_Log = Program.Sesion.Usuario.id
                                    fileImgType.Validaciones_Opcionales = False
                                    fileImgType.Es_Anexo = False
                                    fileImgType.fk_Anexo = Nothing
                                    fileImgType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                                    fileImgType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                                    fileImgType.Fecha_Creacion = SlygNullable.SysDate
                                    fileImgType.Fecha_Transferencia = Nothing
                                    dbmCore.SchemaImaging.TBL_File.DBInsert(fileImgType)

                                    manager.CreateItem(idExpediente, idFolder, idFile, idVersion, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, fileProType.File_Unique_Identifier)

                                    ' Actualizar el Detalle
                                    Dim detalleType = New DBImaging.SchemaProcess.TBL_Contenedor_DetalleType()
                                    detalleType.Cargado = True
                                    detalleType.fk_Expediente = idExpediente
                                    detalleType.fk_Folder = idFolder
                                    detalleType.fk_File = idFile
                                    dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBUpdate(detalleType, _OT, rowItem.Precinto, rowItem.Contenedor, rowItem.Key)


                                    ' Crear la data adicional
                                    For Each rowCampo As DBImaging.Esquemas.xsdCargue.CampoRow In rowItem.GetCampoRows()
                                        Dim fileDataType As New DBCore.SchemaProcess.TBL_File_DataType()
                                        fileDataType.fk_Expediente = idExpediente
                                        fileDataType.fk_Folder = idFolder
                                        fileDataType.fk_File = idFile
                                        fileDataType.fk_Documento = rowItem.Documento
                                        fileDataType.fk_Campo = rowCampo.id_Campo
                                        fileDataType.Valor_File_Data = rowCampo.Valor
                                        fileDataType.Conteo_File_Data = rowCampo.Valor.ToString().Length
                                        dbmCore.SchemaProcess.TBL_File_Data.DBInsert(fileDataType)
                                    Next

                                    ' Calcular el siguiente Estado
                                    Dim nextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(rowItem.Documento, DBCore.EstadoEnum.Indexacion)

                                    ' Crear el Estado
                                    Dim fileEstadoType As New DBCore.SchemaProcess.TBL_File_EstadoType()
                                    fileEstadoType.fk_Expediente = idExpediente
                                    fileEstadoType.fk_Folder = idFolder
                                    fileEstadoType.fk_File = idFile
                                    fileEstadoType.Modulo = DesktopConfig.Modulo.Imaging
                                    fileEstadoType.fk_Estado = nextEstado
                                    fileEstadoType.fk_Usuario = Program.Sesion.Usuario.id
                                    fileEstadoType.Fecha_Log = SlygNullable.SysDate
                                    dbmCore.SchemaProcess.TBL_File_Estado.DBInsert(fileEstadoType)

                                    If (nextEstado < DBCore.EstadoEnum.Indexado) Then
                                        ' Crear el File en Imaging
                                        Dim fileMcType As New DBImaging.SchemaProcess.TBL_FileType()
                                        fileMcType.fk_Expediente = idExpediente
                                        fileMcType.fk_Folder = idFolder
                                        fileMcType.fk_File = idFile
                                        fileMcType.id_Version = idVersion
                                        fileMcType.fk_Reproceso = Nothing
                                        fileMcType.fk_Reproceso_Motivo = Nothing
                                        fileMcType.Actualizado = False
                                        fileMcType.Reprocesado = False
                                        fileMcType.Fecha_Reproceso = Nothing
                                        fileMcType.fk_Documento = rowItem.Documento
                                        fileMcType.Usuario_Primera_Captura = Nothing
                                        fileMcType.Fecha_Primera_Captura = Nothing
                                        fileMcType.Usuario_Segunda_Captura = Nothing
                                        fileMcType.Fecha_Segunda_Captura = Nothing
                                        fileMcType.Usuario_Tercera_Captura = Nothing
                                        fileMcType.Fecha_Tercera_Captura = Nothing
                                        fileMcType.Usuario_Calidad = Nothing
                                        fileMcType.Fecha_Calidad = Nothing
                                        dbmImaging.SchemaProcess.TBL_File.DBInsert(fileMcType)

                                        ' Crear el acceso en DashBoard
                                        ' Captura
                                        If (nextEstado < DBCore.EstadoEnum.Indexado) Then
                                            Dim dasBoardCapturaType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                                            dasBoardCapturaType.fk_Expediente = idExpediente
                                            dasBoardCapturaType.fk_Folder = idFolder
                                            dasBoardCapturaType.fk_File = idFile
                                            dasBoardCapturaType.fk_Documento = rowItem.Documento
                                            dasBoardCapturaType.fk_Cargue = cargueType.id_Cargue
                                            dasBoardCapturaType.fk_Cargue_Paquete = paqueteType.id_Cargue_Paquete
                                            dasBoardCapturaType.fk_Entidad_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                                            dasBoardCapturaType.fk_Sede_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                                            dasBoardCapturaType.fk_Centro_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                                            dasBoardCapturaType.fk_Entidad = Program.ImagingGlobal.Entidad
                                            dasBoardCapturaType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                                            dasBoardCapturaType.fk_Estado = nextEstado
                                            dasBoardCapturaType.fk_Usuario_log = Nothing
                                            dasBoardCapturaType.Sesion = Nothing
                                            dasBoardCapturaType.PCName = Nothing
                                            dasBoardCapturaType.fk_OT = _OT

                                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(dasBoardCapturaType)
                                        End If

                                        ' Validaciones
                                        Dim validacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(rowItem.Documento, DBImaging.EnumEtapaCaptura.Opcional, 1, New DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True))

                                        If (validacionesDataTable.Count > 1) Then
                                            Dim dasBoardValidacionesType As New DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType()
                                            dasBoardValidacionesType.fk_Expediente = idExpediente
                                            dasBoardValidacionesType.fk_Folder = idFolder
                                            dasBoardValidacionesType.fk_File = idFile
                                            dasBoardValidacionesType.fk_Documento = rowItem.Documento
                                            dasBoardValidacionesType.fk_Cargue = cargueType.id_Cargue
                                            dasBoardValidacionesType.fk_Cargue_Paquete = paqueteType.id_Cargue_Paquete
                                            dasBoardValidacionesType.fk_Entidad_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                                            dasBoardValidacionesType.fk_Sede_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                                            dasBoardValidacionesType.fk_Centro_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                                            dasBoardValidacionesType.fk_Entidad = Program.ImagingGlobal.Entidad
                                            dasBoardValidacionesType.fk_Proyecto = Program.ImagingGlobal.Proyecto
                                            dasBoardValidacionesType.Procesado = False
                                            dasBoardValidacionesType.fk_Usuario_log = Nothing
                                            dasBoardValidacionesType.Sesion = Nothing
                                            dasBoardValidacionesType.PCName = Nothing
                                            dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(dasBoardValidacionesType)
                                        End If
                                    End If

                                    If formatoAux = ImageManager.EnumFormat.Pdf Then
                                        If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                        Dim flags As FreeImageAPI.FREE_IMAGE_SAVE_FLAGS = Utilities.GetEnumDefaultFlags(formato)

                                        For folio As Short = 1 To rowItem.Folios


                                            'Dim dataImage = ImageManager.GetData(rowPaquete.Path & rowItem.Path)
                                            Dim dataImage = ImageManager.GetFolioDataPdfOnly(rowPaquete.Path & rowItem.Path, folio, formato, compresion, flags)

                                            Using dataImageStream As Stream = New MemoryStream()

                                                dataImageStream.Write(dataImage, 0, dataImage.Length)
                                                Using bitmap = New FreeImageAPI.FreeImageBitmap(dataImageStream, ImageManager.GetImageFormat(formato))

                                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(bitmap, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)

                                                    manager.CreateFolio(idExpediente, idFolder, idFile, idVersion, CShort(1), dataImage, dataImageThumbnail(0), False)

                                                End Using
                                            End Using


                                        Next
                                    Else
                                        'Se crean los Folios del File.
                                        For folio As Short = 1 To rowItem.Folios
                                            If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                            Dim dataImage = ImageManager.GetFolioData(rowPaquete.Path & rowItem.Path, folio, formato, compresion)
                                            Dim dataImageThumbnail = ImageManager.GetThumbnailData(rowPaquete.Path & rowItem.Path, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight)

                                            manager.CreateFolio(idExpediente, idFolder, idFile, idVersion, folio, dataImage, dataImageThumbnail(0), False)

                                            '                                    
                                            Utilities.ClearMemory()
                                            Application.DoEvents()
                                        Next
                                    End If
                                End If

                                '-----------------------------------------------------------------------------------------------------------------
                                progressFormEsp.ValueAction = item
                                Application.DoEvents()
                                '-----------------------------------------------------------------------------------------------------------------
                            Next
                        End If
                    End If
                Next

                Dim cargueUpdateType As New DBImaging.SchemaProcess.TBL_CargueType()
                cargueUpdateType.fk_Estado = DBCore.EstadoEnum.Indexacion
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(cargueUpdateType, cargueType.id_Cargue)

                '-------------------------------------------------------------------------------------------------------------
                ' Finalización Cargue
                '-------------------------------------------------------------------------------------------------------------
                Me._idCargue = cargueType.id_Cargue

                EventManager.FinalizarCargue(Me._idCargue)

                ' Renombrar paquete
                For Each CarpetaRenombrar In carpetasRenombrar
                    CarpetaRenombrar.RenameDirectory()
                Next

                '-------------------------------------------------------------------------------------------------------------
                ' LOGGING - PERFORMANCE
                '-------------------------------------------------------------------------------------------------------------
                Dim fechaFinProceso = Now
                Dim traceMessage As String = ""
                traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                traceMessage &= "Cargue:" & vbTab & cargueType.id_Cargue.Value & vbTab
                DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Cargue][Cargar]")
                '-------------------------------------------------------------------------------------------------------------

                Application.DoEvents()

                MessageBox.Show("El cargue se realizó con éxito con el identificador: " & cargueType.id_Cargue.ToString(), Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                'Elimina el cargue realizado
                If (Not cargueType.id_Cargue Is Nothing AndAlso (dbmImaging IsNot Nothing) AndAlso (manager IsNot Nothing)) Then
                    dbmCore.SchemaProcess.PA_Borrar_Expedientes.DBExecute(cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.PA_Borrar_Expedientes.DBExecute(cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)

                    ' Actualizar el contenedor
                    If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                        For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In Datos.Paquete.Rows
                            Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                            contenedorType.Cargado = False
                            contenedorType.fk_Cargue = DBNull.Value
                            contenedorType.fk_Paquete = DBNull.Value
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, _OT, rowPaquete.Precinto, rowPaquete.Contenedor)
                        Next
                    End If

                    For Each Expediente In expedientes
                        dbmImaging.SchemaProcess.PA_Liberar_Contenedor_Detalle.DBExecute(_OT, Expediente)
                        manager.DeleteExpediente(Expediente)
                    Next
                End If

                progressFormEsp.Hide()
                Application.DoEvents()

                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                progressFormEsp.Visible = False
                progressFormEsp.Close()

                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub Load_FormatoCargue()
            If (Not Usa_Cargue_PDF) Then
                formatoAux = formato
                CompresionAux = compresion
            Else
                formatoAux = ImageManager.EnumFormat.Pdf
                CompresionAux = Utilities.GetEnumCompression(CType(formatoAux, DesktopConfig.FormatoImagenEnum))
            End If

        End Sub

#End Region

    End Class

End Namespace