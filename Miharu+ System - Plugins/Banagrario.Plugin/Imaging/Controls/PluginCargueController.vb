Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Specialized
Imports Banagrario.Plugin.Imaging.Forms.Cargue
Imports Miharu.Desktop.Library
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Progress
Imports Slyg.Tools

Namespace Imaging.Controls

    Public Class PluginCargueController

#Region " Declaraciones "

        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80
        Public NoCargue As String

        Private _paquetes As StringCollection

        Private _cargue As New DBImaging.Esquemas.xsdCargue
        Private _progressForm As New Miharu.Tools.Progress.FormProgress

        Private _idPaquete As Short

        Private _validos As Integer
        Private _invalidos As Integer

        Private _plugin As New BanagrarioImagingPlugin
        Public CargueNoPaqueteForm As CargueGetInfoClass

#End Region

#Region " Propiedades "

        Public ReadOnly Property Datos() As DBImaging.Esquemas.xsdCargue
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
            _plugin = nBanagrarioDesktopPlugin
            CargueNoPaqueteForm = New CargueGetInfoClass(nBanagrarioDesktopPlugin)
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

                        Dim rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow

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
                        cargueForm.SetData(Me)

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

        Private Sub LoadImagesNoIndice(ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow)
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

        Private Sub InsertImage(ByVal nNombre As String, ByVal nPathPaquete As String, ByVal nCodigo As String, ByVal nId As Integer, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow)
            Dim newRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow

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
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)

            Dim i As Integer = 0
            Dim cargue As Long

            _progressForm.SetAccion("Leer directorios")
            _progressForm.SetProgreso(0)
            _progressForm.SetMaxValue(_cargue.Item.Rows.Count)
            Application.DoEvents()

            _validos = 0
            _invalidos = 0

            Try
                ' Actualizar los datos del cargue
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In _cargue.Paquete.Rows
                    Dim items() As DBImaging.Esquemas.xsdCargue.ItemRow

                    'Si maneja llaves por paquete, valida que exista.
                    rowPaquete.PaqueteValido = True

                    Dim cBarrasPaquete = Path.GetDirectoryName(rowPaquete.Path).Substring(Path.GetDirectoryName(rowPaquete.Path).LastIndexOf("\"c) + 1)
                    Dim expedienteDataTable = dbmCore.SchemaProcess.PA_Expediente_getByKeysEsquema.DBExecute(Program.BanagrarioEntidadId, Program.BanagrarioProyectoRiskId, Program.BanagrarioEsquemaRiskId, Nothing, Nothing, cBarrasPaquete, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                    If expedienteDataTable.Count > 0 Then
                        Dim expedienteRow = expedienteDataTable(0)

                        ' Leer las llaves
                        Dim oficinaPaquete = dbmCore.SchemaProcess.TBL_Expediente_Llave.DBGet(expedienteRow.id_Expediente, CShort(1))(0).Valor_Llave.ToString()
                        Dim fechaPaquete = dbmCore.SchemaProcess.TBL_Expediente_Llave.DBGet(expedienteRow.id_Expediente, CShort(2))(0).Valor_Llave.ToString()
                        fechaPaquete = CDate(fechaPaquete).ToString("yyyy/MM/dd").Replace("-"c, "").Replace("/"c, "")
                        'fechaPaquete = fechaPaquete.Substring(0, 10).Replace("-"c, "").Replace("/"c, "")

                        Dim estadoFolder = dbmArchiving.SchemaRisk.TBL_Folder.DBGet(expedienteRow.id_Expediente, Nothing, Nothing)(0).fk_Estado

                        Dim oficinaDataTable = dbmAgrario.SchemaConfig.TBL_Oficina.DBGet(Integer.Parse(oficinaPaquete))

                        Dim codigoLlaves = ((oficinaPaquete.ToString & fechaPaquete).PadLeft(12).Replace(" ", "0"c)) & cBarrasPaquete
                        Dim validacionContenedor = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBFindByData_Path(codigoLlaves)

                        If validacionContenedor.Count = 0 Then
                            If (estadoFolder >= 20) Then
                                Dim fechaMovimiento = Integer.Parse(fechaPaquete)

                                If fechaMovimiento <= CargueGetInfoClass.FechaProceso Then

                                    If (Not oficinaDataTable(0).Activa) Then
                                        rowPaquete.PaqueteValido = False
                                        rowPaquete.Descripcion = "El paquete [" & cBarrasPaquete & "] no puede ser cargado por que su oficina no se encuentra activa"
                                    End If
                                Else
                                    rowPaquete.PaqueteValido = False
                                    rowPaquete.Descripcion = "El paquete [" & cBarrasPaquete & "] no puede ser cargado porque su fecha de proceso es menor a la fecha de movimiento."
                                End If
                            Else
                                rowPaquete.PaqueteValido = False
                                rowPaquete.Descripcion = "El paquete [" & cBarrasPaquete & "] No corresponden al estado de destapado."
                            End If
                        Else
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "El paquete [" & cBarrasPaquete & "] ya se encuentra cargado en el sistema."

                        End If
                    Else
                        rowPaquete.PaqueteValido = False
                        rowPaquete.Descripcion = "El paquete [" & cBarrasPaquete & "] no se encuentra en el sistema."

                    End If


                    items = CType(_cargue.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete), DBImaging.Esquemas.xsdCargue.ItemRow())
                    rowPaquete.Total = 0
                    For Each rowItem As DBImaging.Esquemas.xsdCargue.ItemRow In items

                        ' Validar que la imagen exista físicamente
                        If _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Archivo_Indices Then
                            If Not File.Exists(rowPaquete.Path & rowItem.Path) Then
                                rowItem.Valido = False
                                rowItem.Descripcion = "No se encontro el archivo físico."
                            End If
                        End If

                        If Not rowPaquete.PaqueteValido Then
                            rowItem.Valido = False
                            rowItem.Descripcion = "El paquete al que corresponde la imagen no es válido."
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

                        If (_progressForm.Cancelar) Then Throw New Exception("Accion cancelada por el usuario")
                    Next

                Next

            Catch
                Throw
            Finally
                dbmCore.Connection_Close()
                dbmImaging.Connection_Close()
                dbmArchiving.Connection_Close()
                dbmAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargarDirecto()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Dim cargueType As New DBImaging.SchemaProcess.TBL_CargueType()
            Dim fechaProcesoType As New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType()
            Dim otType As New DBImaging.SchemaProcess.TBL_OTType()
            Dim progressFormEsp As New FormProgress()
            Dim paquetesInsertados = New List(Of Short)

            Dim fechaInicioProceso = Now

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)

                ' Actualizar los datos del cargue
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType()
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType
                manager = New FileProviderManager(servidor, centro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                manager.Connect()

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
                    otType.fk_Sede_Procesamiento = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
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
                cargueType.fk_Sede_Procesamiento_Cargue = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede
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



                Dim cargueData = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(cargueType.id_Cargue)
                cargueType.Fecha_Cargue = cargueData(0).Fecha_Cargue

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
                Dim idCarguePaquete As Integer = 0

                ' Actualizar la data de los paquetes
                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In Datos.Paquete.Rows
                    If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                    'Se procesa si el paquete es válido.
                    If (rowPaquete.PaqueteValido) Then
                        Dim items As Integer = Datos.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete & " AND Valido = 1").Length
                        idCarguePaquete += 1

                        If (items > 0) Then
                            Dim cBarrasPaquete = Path.GetDirectoryName(rowPaquete.Path).Substring(Path.GetDirectoryName(rowPaquete.Path).LastIndexOf("\"c) + 1)
                            Dim expedienteDataTable = dbmCore.SchemaProcess.PA_Expediente_getByKeysEsquema.DBExecute(Program.BanagrarioEntidadId, Program.BanagrarioProyectoRiskId, Program.BanagrarioEsquemaRiskId, Nothing, Nothing, cBarrasPaquete, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                            Dim expedienteRow = expedienteDataTable(0)

                            ' Leer las llaves
                            Dim oficinaPaquete = dbmCore.SchemaProcess.TBL_Expediente_Llave.DBGet(expedienteRow.id_Expediente, CShort(1))(0).Valor_Llave.ToString()
                            Dim fechaPaquete = dbmCore.SchemaProcess.TBL_Expediente_Llave.DBGet(expedienteRow.id_Expediente, CShort(2))(0).Valor_Llave.ToString()
                            fechaPaquete = CDate(fechaPaquete).ToString("yyyy/MM/dd").Replace("-"c, "").Replace("/"c, "")
                            'fechaPaquete = fechaPaquete.Substring(0, 10).Replace("-"c, "").Replace("/"c, "")


                            paquete += 1

                            progressFormEsp.Process = "Cargar paquete " & paquete & " de " & totalPaquetes
                            progressFormEsp.ValueProcess = paquete

                            Dim paqueteNombre = (oficinaPaquete & fechaPaquete).PadLeft(12).Replace(" ", "0"c) & cBarrasPaquete
                            '-----------------------------------------------------------------------------------------------------------------

                            progressFormEsp.Action = "Cargar items"
                            progressFormEsp.ValueAction = 0
                            progressFormEsp.MaxValueAction = items

                            Application.DoEvents()
                            '-----------------------------------------------------------------------------------------------------------------

                            Dim paqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()

                            paqueteType.fk_Cargue = cargueType.id_Cargue
                            paqueteType.id_Cargue_Paquete = CShort(idCarguePaquete)
                            paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                            paqueteType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                            paqueteType.Fecha_Proceso = cargueType.Fecha_Cargue
                            paqueteType.Path_Cargue_Paquete = rowPaquete.Path
                            paqueteType.Data_Path = paqueteNombre
                            paqueteType.fk_Sede_Procesamiento_Asignada = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede_Asignada
                            paqueteType.fk_Centro_Procesamiento_Asignado = _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Centro_Procesamiento_Asignado
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                            ''Se inserta el registro correspondiente a la aprobación de cargue a pesar de ser invalida la fecha por vigencia
                            If FormValidacionFechCargue.MotivoCreado Then
                                InsertarAprobFechaCargue(cargueType.id_Cargue, paqueteType.id_Cargue_Paquete)
                            End If

                            ' Crear lista de paquetes insertados
                            paquetesInsertados.Add(rowPaquete.id_Paquete)


                            '            103110123457
                            '311020140112103110123457
                            'Dim datapath = Path.GetDirectoryName(RowPaquete.Path).Substring(Path.GetDirectoryName(RowPaquete.Path).LastIndexOf("\"c) + 1)
                            'Dim contenedor = datapath.Substring(datapath.Length - 12)
                            Dim fecha = Me._FechaProceso.ToString("yyyy/MM/dd")

                            ' Actualizar el destape
                            Dim destape = dbmAgrario.SchemaProcess.TBL_Destape.DBFindBycodigo_Contenedorfecha_Proceso(cBarrasPaquete, fecha)

                            If (destape.Count > 0) Then
                                Dim destapeType = New DBAgrario.SchemaProcess.TBL_DestapeType()
                                destapeType.fk_Cargue = cargueType.id_Cargue
                                destapeType.fk_Cargue_Paquete = rowPaquete.id_Paquete

                                dbmAgrario.SchemaProcess.TBL_Destape.DBUpdate(destapeType, destape(0).id_Destape)
                            End If

                            Dim item As Integer = 0

                            progressFormEsp.MaxValueAction = items

                            For Each rowItem As DBImaging.Esquemas.xsdCargue.ItemRow In rowPaquete.GetItemRows()
                                If progressFormEsp.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                If rowItem.Valido Then

                                    item += 1

                                    Dim itemType As New DBImaging.SchemaProcess.TBL_Cargue_ItemType()

                                    itemType.fk_Cargue = cargueType.id_Cargue
                                    itemType.fk_Cargue_Paquete = CShort(idCarguePaquete)
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

                Dim cargueUpdateType As New DBImaging.SchemaProcess.TBL_CargueType()
                cargueUpdateType.fk_Estado = DBCore.EstadoEnum.Indexacion
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(cargueUpdateType, cargueType.id_Cargue)

                '---------------------------------------------------------------------------
                ' Actualizar Dashboard
                '---------------------------------------------------------------------------
                dbmImaging.SchemaProcess.PA_Dashboard_Paquetes_insert.DBExecute(cargueType.id_Cargue)
                dbmImaging.SchemaProcess.PA_Cargue_Indice_Insert.DBExecute(cargueType.id_Cargue)
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

                'RollBack creación de OT
                dbmImaging.Transaction_Rollback()

                'Elimina el cargue realizado
                If Not IsNothing(cargueType.id_Cargue) Then
                    dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(cargueType.id_Cargue, Nothing)
                    manager.DeleteCargue(cargueType.id_Cargue)
                End If

                NoCargue = ""
                progressFormEsp.Hide()
                Application.DoEvents()

                MessageBox.Show(ex.Message, "Pluging", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return
            Finally
                progressFormEsp.Visible = False
                progressFormEsp.Close()

                If (Not dbmCore Is Nothing) Then dbmCore.Connection_Close()
                If (Not dbmImaging Is Nothing) Then dbmImaging.Connection_Close()
                If (Not dbmAgrario Is Nothing) Then dbmAgrario.Connection_Close()
                If (Not manager Is Nothing) Then manager.Disconnect()
            End Try
            NoCargue = cargueType.id_Cargue.ToString()

            Dim counter As Integer = 1
            '_Paquetes_Nombre.Add(RowPaquete.Path.TrimEnd("\"c) & "\")

            For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In Datos.Paquete.Rows
                'For Each PaqueteInsertado In PaquetesInsertados
                Try
                    If (paquetesInsertados.Contains(rowPaquete.id_Paquete)) Then
                        Dim partesPath = rowPaquete.Path.TrimEnd("\"c).Split("\"c)

                        partesPath(partesPath.Length - 1) = "C" & partesPath(partesPath.Length - 1)

                        Dim newPath As String = ""
                        For Each parte As String In partesPath
                            newPath &= parte & "\"
                        Next

                        newPath = newPath.TrimEnd("\"c)
                        If NoCargue <> "" Then
                            Dim counterpaquete = counter.ToString().PadLeft(4).Replace(" ", "0"c)
                            Directory.Move(rowPaquete.Path, newPath & "." & NoCargue & "." & counterpaquete)
                        End If

                        counter += 1
                    End If
                Catch
                    MessageBox.Show("Error al renombrar la carpeta con dirección: " & rowPaquete.Path, "Pluging", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Next

            MessageBox.Show("El cargue se realizó con éxito con el identificador: " & cargueType.id_Cargue.ToString(), "Pluging", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Sub

        Private Sub InsertarAprobFechaCargue(ByVal idCargue As Integer, ByVal idCarguePaquete As Short)
            Dim dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                ''Inserción Validacion_Cargue
                Dim newValidacionCargueType As New DBAgrario.SchemaProcess.TBL_Validacion_CargueType()

                newValidacionCargueType.Descripcion_motivo = FormValidacionFechCargue.Descripcion
                newValidacionCargueType.Motivo = FormValidacionFechCargue.Motivo
                newValidacionCargueType.fk_Usuario = _plugin.Manager.Sesion.Usuario.id
                newValidacionCargueType.Fecha_Log = DateTime.Now
                newValidacionCargueType.fk_Cargue = idCargue
                newValidacionCargueType.fk_Cargue_Paquete = idCarguePaquete

                dbmAgrario.SchemaProcess.TBL_Validacion_Cargue.DBInsert(newValidacionCargueType)
            Catch
                MessageBox.Show("Error al intentar almacenar el registro en la tabla TBL_Validacion_Cargue ", "Pluging", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmAgrario.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function LoadImagesDirectories() As Boolean
            Try
                _paquetes = New StringCollection

                Dim fechaPaqueteForm As CargueGetInfoClass
                fechaPaqueteForm = New CargueGetInfoClass(_plugin)
                Dim respuesta As DialogResult

                respuesta = fechaPaqueteForm.ShowDialog()

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
                            rutaCargueCodBarras = paquete(paquete.Length - 2)

                            If (rutaCargueInicial.ToString <> "c") And (rutaCargueInicial.ToString <> "C") And IsNumeric(rutaCargueCodBarras) Then
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