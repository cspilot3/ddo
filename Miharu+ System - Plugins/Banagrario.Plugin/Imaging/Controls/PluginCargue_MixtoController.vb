Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Specialized
Imports Banagrario.Plugin.Imaging.Forms.Cargue
Imports DBImaging.SchemaProcess
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging
Imports Slyg.Tools
Imports DBImaging.Esquemas
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress

Namespace Imaging.Controls

    Public Class PluginCargueMixtoController

#Region " Declaraciones "
        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80
        Public NoCargue As String

        Private _paquetes As StringCollection

        Private _cargue As New xsdCargue
        Private _progressForm As New Miharu.Tools.Progress.FormProgress

        Private _idPaquete As Short

        Private _validos As Integer
        Private _invalidos As Integer

        Private _plugin As New BanagrarioImagingPlugin
        Public CargueNoPaqueteForm As FormCargueNoPaqueteMixto

#End Region

#Region " Propiedades "

        Public ReadOnly Property Datos() As xsdCargue
            Get
                Return _cargue
            End Get
        End Property

        Public ReadOnly Property Validos() As Integer
            Get
                Return _validos
            End Get
        End Property

        Public ReadOnly Property Invalidos() As Integer
            Get
                Return _invalidos
            End Get
        End Property

        Public ReadOnly Property Paquetes As StringCollection
            Get
                Return _paquetes
            End Get
        End Property

        Public Property FechaProceso As DateTime

        Public Property FechaProcesoInt() As Integer
        Public Property NewOt() As Integer

#End Region

#Region " Metodos "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            _Plugin = nBanagrarioDesktopPlugin
            CargueNoPaqueteForm = New FormCargueNoPaqueteMixto(nBanagrarioDesktopPlugin)
        End Sub

        Public Sub Run()
            Try
                _progressForm.SetProceso("Cargue")
                _progressForm.SetProgreso(0)
                _progressForm.SetMaxValue(100)

                _progressForm.SetAccion("Leer directorios")

                _cargue.Clear()

                If LoadImagesDirectories() Then
                    If _progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                    Dim idDirectorio As Integer = 0

                    _idPaquete = 0

                    For Each directorio As String In _paquetes
                        If _progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                        idDirectorio += 1

                        _progressForm.SetAccion("Leer imágenes - directorio " & idDirectorio & " de " & _paquetes.Count)

                        Dim rowPaquete As xsdCargue.PaqueteRow

                        _idPaquete = CShort(_idPaquete + 1)

                        rowPaquete = _cargue.Paquete.AddPaqueteRow(_idPaquete, directorio, 0, 0, 0, True, "", "", 0, 0, "")

                        LoadImagesNoIndice(directorio, rowPaquete)
                    Next

                    If _cargue.Paquete.Rows.Count = 0 Then
                        Throw New Exception("No se encontraron imagenes para indexar")
                    Else
                        Validar()

                        _progressForm.Hide()

                        Dim cargueForm As New FormCargue()
                        cargueForm.setData(Me)

                        Dim respuesta = cargueForm.ShowDialog()

                        If (respuesta = DialogResult.OK) Then
                            CargarDirecto()
                        End If
                    End If
                End If

            Catch ex As Exception
                _progressForm.Hide()
                Application.DoEvents()

                MessageBox.Show(ex.Message, "Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                _progressForm.Close()

            End Try
        End Sub

        Private Sub LoadImagesNoIndice(ByVal nPaquete As String, ByRef nRowPaquete As xsdCargue.PaqueteRow)
            Dim archivos() As String
            Dim codigo As String
            Dim i As Integer = 0

            archivos = Directory.GetFiles(nPaquete, "*" & _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada)

            _progressForm.SetMaxValue(archivos.Length)

            For Each archivo As String In archivos
                i += 1

                codigo = Path.GetFileNameWithoutExtension(archivo)
                InsertImage(archivo, nPaquete, codigo, i, nRowPaquete)

                _progressForm.SetProgreso(i)
            Next
        End Sub

        Private Sub InsertImage(ByVal nNombre As String, ByVal nPathPaquete As String, ByVal nCodigo As String, ByVal nId As Integer, ByRef nRowPaquete As xsdCargue.PaqueteRow)
            Dim newRowPaquete As xsdCargue.PaqueteRow

            If nRowPaquete Is Nothing Then
                _idPaquete = CShort(_idPaquete + 1)
                newRowPaquete = _cargue.Paquete.AddPaqueteRow(_idPaquete, nPathPaquete, 0, 0, 0, True, "", "", 0, 0, "")
            Else
                newRowPaquete = nRowPaquete
            End If

            If File.Exists(nNombre) Then
                _cargue.Item.AddItemRow(newRowPaquete, nId, CShort(ImageManager.GetFolios(nNombre)), getTamaño(nNombre), Path.GetFileName(nNombre), nCodigo, True, "", 0, 0, 0, 0, "")
            Else
                _cargue.Item.AddItemRow(newRowPaquete, nId, 0, 0, Path.GetFileName(nNombre), nCodigo, True, "", 0, 0, 0, 0, "")
            End If
        End Sub

        Private Sub Validar()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim i As Integer = 0
            Dim cargue As Long
            Dim paqueteNombre As String

            _progressForm.SetAccion("Leer directorios")
            _progressForm.SetProgreso(0)
            _progressForm.SetMaxValue(_cargue.Item.Rows.Count)
            Application.DoEvents()

            _validos = 0
            _invalidos = 0

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                For Each rowPaquete As xsdCargue.PaqueteRow In _cargue.Paquete.Rows
                    Dim items() As xsdCargue.ItemRow

                    'Si maneja llaves por paquete, valida que exista.
                    rowPaquete.PaqueteValido = True

                    If _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Captura_Llaves_Paquete Then
                        paqueteNombre = Path.GetDirectoryName(rowPaquete.Path).Substring(Path.GetDirectoryName(rowPaquete.Path).LastIndexOf("\"c) + 1)

                        Dim filtro(9) As SlygNullable(Of String)
                        Dim j As Integer = 0

                        Try
                            For Each Llave In _plugin.Manager.ImagingGlobal.ProyectoImagingLlaveDataTable
                                Select Case Llave.fk_Campo_Tipo
                                    Case DesktopConfig.CampoTipo.Texto
                                        filtro(j) = paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)

                                    Case DesktopConfig.CampoTipo.Numerico
                                        filtro(j) = CLng(paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)).ToString()

                                    Case DesktopConfig.CampoTipo.Fecha
                                        Dim fechaPaquete = paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)
                                        filtro(j) = fechaPaquete.Substring(0, 4) & "/" & fechaPaquete.Substring(4, 2) & "/" & fechaPaquete.Substring(6, 2)

                                    Case DesktopConfig.CampoTipo.Lista
                                        filtro(j) = CLng(paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)).ToString()
                                End Select
                                j += 1
                            Next
                        Catch ex As Exception
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "El paquete [" & paqueteNombre & "] no cumple con los parámetros para obtener las llaves, por favor revisar la configuración del proyecto."
                        End Try
                    End If

                    If (Not _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Captura_Llaves_Paquete) OrElse (rowPaquete.PaqueteValido) Then
                        ' Se analizan los Items simpre y cuándo el paquete sea válido ó no se maneje captura de llaves por paquete.
                        items = CType(_cargue.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete), xsdCargue.ItemRow())
                        rowPaquete.Total = 0
                        For Each rowItem As xsdCargue.ItemRow In items

                            ' Validar que la imagen exista físicamente
                            If _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Archivo_Indices Then
                                If Not File.Exists(rowPaquete.Path & rowItem.Path) Then
                                    rowItem.Valido = False
                                    rowItem.Descripcion = "No se encontro el archivo físico."
                                End If
                            End If

                            ' Validar si el item ya fue cargado
                            cargue = dbmImaging.SchemaProcess.PA_Basic_TBL_Cargue_Item_validate.DBExecute(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, rowItem.Path, rowItem.Key, rowItem.Tamaño)

                            If cargue > 0 Then
                                rowItem.Valido = False
                                rowItem.Descripcion = "Se encontró un item con características similares en el cargue: " & cargue
                            End If

                            If rowItem.Valido Then
                                rowPaquete.Validos += 1
                                _validos += 1
                            Else
                                rowPaquete.Invalidos += 1
                                _invalidos += 1
                            End If

                            rowPaquete.Total += 1

                            i += 1
                            _progressForm.SetProgreso(i)
                            Application.DoEvents()
                        Next

                    End If
                Next
            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CargarDirecto()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Dim cargueType As New TBL_CargueType()
            Dim fechaProcesoType As New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType()
            Dim otType As New DBImaging.SchemaProcess.TBL_OTType()
            Dim progressFormEsp As New FormProgress()



            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType
                manager = New FileProviderManager(servidor, centro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                manager.Connect()

                Dim parametroDataTable = dbmAgrario.SchemaConfig.TBL_Parametro_Sistema.DBFindByNombre_Parametro_Sistema("Sede_Cargue_Mixto")
                Dim sede As Short = 0
                Dim sedeAsignada As Short = 0
                If parametroDataTable.Count > 0 Then
                    For Each parametro In parametroDataTable
                        sede = CShort(parametro.Valor_Parametro_Sistema)
                        sedeAsignada = CShort(parametro.Valor_Parametro_Sistema)
                    Next
                End If

                If sede = 0 Then
                    sede = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                End If
                If sedeAsignada = 0 Then
                    sedeAsignada = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
                End If

                'Crear la fecha de proceso
                Dim datosFechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, CType(Me._FechaProceso.ToString("yyyyMMdd"), Integer))

                If datosFechaProcesoDataTable.Count = 0 Then

                    fechaProcesoType.fk_Entidad_Procesamiento = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                    fechaProcesoType.fk_Entidad = _plugin.Manager.ImagingGlobal.Entidad
                    fechaProcesoType.fk_Proyecto = _plugin.Manager.ImagingGlobal.Proyecto
                    fechaProcesoType.id_fecha_proceso = CType(Me._FechaProceso.ToString("yyyyMMdd"), Integer)
                    fechaProcesoType.Fecha_Proceso = Me._FechaProceso
                    fechaProcesoType.fk_Usuario_Apertura = _plugin.Manager.Sesion.Usuario.id
                    fechaProcesoType.Fecha_Apertura = SlygNullable.SysDate
                    fechaProcesoType.Cerrado = False

                    dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBInsert(fechaProcesoType)
                    Me.FechaProcesoInt = fechaProcesoType.id_fecha_proceso.Value
                Else
                    Me.FechaProcesoInt = datosFechaProcesoDataTable(0).id_fecha_proceso
                End If


                'Crear la OT de imágenes
                Dim otDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, CType(Me._FechaProceso.ToString("yyyyMMdd"), Integer), False, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)

                If otDataTable.Count = 0 Then
                    dbmImaging.Transaction_Begin()

                    otType.fk_Entidad_Procesamiento = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                    otType.fk_Entidad = _plugin.Manager.ImagingGlobal.Entidad
                    otType.fk_Proyecto = _plugin.Manager.ImagingGlobal.Proyecto
                    otType.fk_fecha_proceso = Me.FechaProcesoInt
                    otType.fk_OT_Tipo = CShort(1) 'Basica
                    otType.fk_Sede_Procesamiento = sede '_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                    otType.fk_Centro_Procesamiento = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                    otType.fk_Usuario_Apertura = _plugin.Manager.Sesion.Usuario.id
                    otType.Fecha_Apertura = SlygNullable.SysDate
                    otType.Exportado = False
                    otType.Cerrado = False
                    otType.id_OT = dbmImaging.SchemaProcess.TBL_OT.DBNextId()

                    dbmImaging.SchemaProcess.TBL_OT.DBInsert(otType)

                    dbmImaging.Transaction_Commit()
                    Me.NewOt = otType.id_OT.Value
                Else
                    Me.NewOt = otDataTable(0).id_OT
                End If


                '' Obtener el nuevo id para el cargue
                cargueType.fk_Entidad = _plugin.Manager.ImagingGlobal.Entidad
                cargueType.fk_Proyecto = _plugin.Manager.ImagingGlobal.Proyecto
                cargueType.fk_Estado = DBCore.EstadoEnum.Creado
                cargueType.fk_Entidad_Procesamiento = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad
                cargueType.fk_Sede_Procesamiento_Cargue = sede ' _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
                cargueType.fk_Centro_Procesamiento_Cargue = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                cargueType.fk_Entidad_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad
                cargueType.fk_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor
                cargueType.fk_OT = Me.NewOt '1 ' TODO: Validar código
                cargueType.Fecha_Proceso = Me._FechaProceso
                cargueType.Observaciones = "CP-" & _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
                cargueType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id


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

                Dim formato = Utilities.getEnumFormat(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.getEnumCompression(CType(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                Dim totalPaquetes As Integer = Datos.Paquete.Select("PaqueteValido = 1").Length
                Dim paquete As Integer = 0

                progressFormEsp.MaxValueProcess = totalPaquetes

                ' Actualizar la data de los paquetes
                For Each rowPaquete As xsdCargue.PaqueteRow In Datos.Paquete.Rows
                    If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                    'Se procesa si el paquete es válido.
                    If (rowPaquete.PaqueteValido) Then
                        Dim items As Integer = Datos.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete & " AND Valido = 1").Length

                        If items > 0 Then
                            paquete += 1
                            progressFormEsp.Process = "Cargar paquete " & paquete & " de " & totalPaquetes
                            progressFormEsp.ValueProcess = paquete

                            '-----------------------------------------------------------------------------------------------------------------
                            progressFormEsp.Action = "Cargar items"
                            progressFormEsp.ValueAction = 0
                            progressFormEsp.MaxValueAction = items

                            Application.DoEvents()
                            '-----------------------------------------------------------------------------------------------------------------

                            Dim paqueteType As New TBL_Cargue_PaqueteType()

                            paqueteType.fk_Cargue = cargueType.id_Cargue
                            paqueteType.id_Cargue_Paquete = rowPaquete.id_Paquete
                            paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                            paqueteType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                            paqueteType.Fecha_Proceso = cargueType.Fecha_Proceso
                            paqueteType.Path_Cargue_Paquete = rowPaquete.Path
                            paqueteType.Bloqueado = False
                            paqueteType.Data_Path = Path.GetDirectoryName(rowPaquete.Path).Substring(Path.GetDirectoryName(rowPaquete.Path).LastIndexOf("\"c) + 1)
                            paqueteType.fk_Sede_Procesamiento_Asignada = sedeAsignada '_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
                            paqueteType.fk_Centro_Procesamiento_Asignado = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado

                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                            Dim item As Integer = 0

                            progressFormEsp.MaxValueAction = items

                            For Each rowItem As xsdCargue.ItemRow In rowPaquete.GetItemRows()
                                If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                If rowItem.Valido Then
                                    item += 1

                                    Dim itemType As New TBL_Cargue_ItemType()

                                    itemType.fk_Cargue = cargueType.id_Cargue
                                    itemType.fk_Cargue_Paquete = rowItem.fk_Paquete
                                    itemType.id_Cargue_Item = rowItem.id_Item
                                    itemType.Folios_Cargue_Item = rowItem.Folios
                                    itemType.Tamaño_Cargue_Item = rowItem.Tamaño
                                    itemType.Path_Cargue_Item = Path.GetFileName(rowItem.Path)
                                    itemType.Key_Cargue_Item = rowItem.Key
                                    itemType.fk_Estado = DBCore.EstadoEnum.Indexacion
                                    itemType.Bloqueado = False
                                    itemType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id

                                    dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(itemType)

                                    manager.CreateItem(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item)

                                    'Se crean los Folios del item.
                                    For folio As Short = 1 To rowItem.Folios
                                        If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                        Dim dataImage = ImageManager.GetFolioData(rowPaquete.Path & rowItem.Path, folio, formato, compresion)
                                        Dim dataImageThumbnail = ImageManager.GetThumbnailData(rowPaquete.Path & rowItem.Path, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight)

                                        manager.CreateFolio(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item, folio, dataImage, dataImageThumbnail(0), False)

                                        ' Insertar Folio
                                        Dim folioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                                        folioType.fk_Cargue = itemType.fk_Cargue
                                        folioType.fk_Cargue_Paquete = itemType.fk_Cargue_Paquete
                                        folioType.fk_Cargue_Item = itemType.id_Cargue_Item
                                        folioType.id_Folio = folio
                                        folioType.Indexado = False
                                        dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(folioType)

                                        Utilities.ClearMemory()
                                        Application.DoEvents()
                                    Next
                                End If

                                '-----------------------------------------------------------------------------------------------------------------
                                progressFormEsp.ValueAction = item
                                Application.DoEvents()
                                '-----------------------------------------------------------------------------------------------------------------
                            Next

                        Else
                            rowPaquete.PaqueteValido = False
                        End If
                    End If

                Next

                Dim cargueUpdateType As New TBL_CargueType()
                cargueUpdateType.fk_Estado = DBCore.EstadoEnum.Indexacion
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(cargueUpdateType, cargueType.id_Cargue)

                '---------------------------------------------------------------------------
                ' Actualizar Dashboard
                '---------------------------------------------------------------------------
                dbmImaging.SchemaProcess.PA_Dashboard_Paquetes_insert.DBExecute(cargueType.id_Cargue)
                dbmImaging.SchemaProcess.PA_Cargue_Indice_Insert.DBExecute(cargueType.id_Cargue)

                '---------------------------------------------------------------------------

                Application.DoEvents()

            Catch ex As Exception
                'RollBack de creacion de OT
                dbmImaging.Transaction_Rollback()

                'Elimina el cargue realizado
                If Not IsNothing(cargueType.id_Cargue) Then
                    dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(cargueType.id_Cargue, Nothing)
                    manager.DeleteCargue(cargueType.id_Cargue)
                End If

                progressFormEsp.Hide()
                Application.DoEvents()

                MessageBox.Show(ex.Message, "Cargue Mixtos", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return
            Finally
                progressFormEsp.Visible = False
                progressFormEsp.Close()

                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try

            MessageBox.Show("El cargue se realizó con éxito con el identificador: " & cargueType.id_Cargue.ToString(), "Cargue Mixtos", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Sub

#End Region

#Region " Funciones "

        Private Function LoadImagesDirectories() As Boolean
            Try
                _paquetes = New StringCollection

                Dim fechaPaqueteForm = New FormCargueNoPaqueteMixto(_Plugin)
                Dim respuesta = fechaPaqueteForm.ShowDialog()

                If (respuesta = DialogResult.OK) Then
                    'Recupera esquema seleccionado.
                    Me.FechaProceso = fechaPaqueteForm.Fecha
                    Dim directorios As String()

                    Try
                        directorios = Directory.GetDirectories(fechaPaqueteForm.RutaTextBox.Text)

                        Dim i As Integer = 0

                        _progressForm.Show()
                        Application.DoEvents()

                        _progressForm.SetMaxValue(directorios.Length)

                        Dim rutaCargueInicial As String
                        Dim rutaCargueCodBarras As String

                        If directorios.Length = 0 Then
                            Dim directorio = fechaPaqueteForm.RutaTextBox.Text.TrimEnd("\"c) & "\"
                            Dim paquete = (directorio).Split("\"c)

                            rutaCargueInicial = paquete(paquete.Length - 2).Substring(0, 1)
                            'Ruta_Cargue_Cod_Barras = Paquete(Paquete.Length - 2)

                            If (rutaCargueInicial.ToString <> "c") And (rutaCargueInicial.ToString <> "C") Then 'And IsNumeric(Ruta_Cargue_Cod_Barras)
                                _paquetes.Add(fechaPaqueteForm.RutaTextBox.Text.TrimEnd("\"c) & "\")
                            End If
                        End If

                        For Each directorio As String In directorios
                            If _progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                            Dim paquete = (directorio).Split("\"c)

                            rutaCargueInicial = paquete(paquete.Length - 1).Substring(0, 1)
                            rutaCargueCodBarras = paquete(paquete.Length - 1)

                            If (rutaCargueInicial.ToString <> "c") And (rutaCargueInicial.ToString <> "C") And IsNumeric(rutaCargueCodBarras) Then

                                _paquetes.Add(directorio.TrimEnd("\"c) & "\")
                            End If

                            i += 1
                            _progressForm.SetProgreso(i)
                        Next

                        If _paquetes.Count = 0 Then
                            MessageBox.Show("No se encontraron paquetes disponibles para el cargue, revise el formato del código, la fecha y la direccion del paquete.", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            Return True
                        End If

                    Catch ex As Exception
                        _progressForm.Hide()
                        MessageBox.Show(ex.Message, "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Finally

                    End Try
                End If

            Catch ex As Exception
                _progressForm.Hide()
                MessageBox.Show("ERROR al leer las imagenes " & ex.Message, " Validación ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return False

        End Function

        Private Function getTamaño(ByVal nFileName As String) As Long
            Dim archivo As New FileInfo(nFileName)

            ' retornar el valor en Bytes
            Return archivo.Length
        End Function

#End Region

    End Class

End Namespace