Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Plugins
Imports DBAgrario.SchemaFirmas
Imports System.Windows.Forms

Namespace Firmas.FormWrappers

    Public Class EventExecuterFirmasImaging
        Inherits EventExecuter
        Implements IEventExecuter

#Region " Declaraciones "

        Private ReadOnly _plugin As FirmasImagingPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As FirmasImagingPlugin)
            Me._Plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "

        Public Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.EliminarImagen

        End Sub

        Public Sub FinalizarCargue(ByVal nidCargue As Integer) Implements IEventExecuter.FinalizarCargue
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            'Return

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)

                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType

                manager = New FileProviderManager(servidor, centro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                manager.Connect()

                Dim cBarrasFileDataTable = dbmAgrario.SchemaFirmas.PA_Cargue_Archivo_CBarras.DBExecute(nidCargue)
                Dim cBarrasAnterior As String = "**********"
                Dim idExpediente As Long
                Dim idFolder As Short
                Dim idFile As Short
                Dim folio As Short

                For Each cBarrasFileRow As CTA_Cargue_Archivo_CBarrasRow In cBarrasFileDataTable
                    If (cBarrasAnterior = cBarrasFileRow.CBarras And cBarrasFileRow.CBarras <> "") Then
                        folio += CShort(1)
                        Dim imagen() As Byte = Nothing
                        Dim thumbnail() As Byte = Nothing

                        ' Leer folio actual
                        manager.GetFolio(cBarrasFileRow.fk_Expediente, cBarrasFileRow.fk_folder, cBarrasFileRow.id_File, 1, 1, imagen, thumbnail)

                        ' Crear nuevo folio en anterior
                        manager.CreateFolio(idExpediente, idFolder, idFile, 1, folio, imagen, thumbnail, False)

                        Dim fileType = New DBCore.SchemaProcess.TBL_FileType()
                        fileType.Folios_File = folio

                        dbmCore.SchemaProcess.TBL_File.DBUpdate(fileType, idExpediente, idFolder, idFile)

                        Dim fileImagingType = New DBCore.SchemaImaging.TBL_FileType()
                        fileImagingType.Folios_Documento_File = folio

                        dbmCore.SchemaImaging.TBL_File.DBUpdate(fileImagingType, idExpediente, idFolder, idFile, CShort(1))

                        ' Borrar file actual
                        manager.DeleteItem(cBarrasFileRow.fk_Expediente, cBarrasFileRow.fk_folder, cBarrasFileRow.id_File, 1)

                        dbmAgrario.SchemaFirmas.PA_Eliminar_Expediente.DBExecute(cBarrasFileRow.fk_Expediente, cBarrasFileRow.fk_folder, cBarrasFileRow.id_File)

                        Miharu.Desktop.Library.Config.Utilities.ClearMemory()
                        Application.DoEvents()
                    Else
                        ' Actualizar las banderas porque es un nuevo documento
                        idExpediente = cBarrasFileRow.fk_Expediente
                        idFolder = cBarrasFileRow.fk_folder
                        idFile = cBarrasFileRow.id_File
                        cBarrasAnterior = cBarrasFileRow.CBarras
                        folio = 1

                        ' Validar dígito de verificación
                        Dim digito1 As Integer = -1
                        Dim newCodigo As String = ""
                        Dim longitud As Integer = cBarrasFileRow.CBarras.Length

                        Select Case longitud
                            Case 44 ' Judiciales 
                                ExtraerDigitoYCodigo(cBarrasFileRow.CBarras, 14, newCodigo, digito1)

                            Case 38 ' Clientes
                                ExtraerDigitoYCodigo(cBarrasFileRow.CBarras, 13, newCodigo, digito1)

                            Case 28 ' Funcionarios
                                ExtraerDigitoYCodigo(cBarrasFileRow.CBarras, 10, newCodigo, digito1)

                        End Select

                        If (digito1 <> -1) Then
                            Dim digito2 = FMDigitoVerif(newCodigo, longitud)

                            Dim dataType = New DBCore.SchemaProcess.TBL_File_DataType

                            dataType.fk_Expediente = cBarrasFileRow.fk_Expediente
                            dataType.fk_Folder = cBarrasFileRow.fk_folder
                            dataType.fk_File = cBarrasFileRow.id_File
                            dataType.fk_Documento = cBarrasFileRow.fk_Documento
                            dataType.fk_Campo = CShort(2)
                            dataType.Valor_File_Data = CStr(IIf(digito1 = digito2, 1, 0))
                            dataType.Conteo_File_Data = 1

                            dbmCore.SchemaProcess.TBL_File_Data.DBInsert(dataType)
                        End If
                    End If
                Next

                ' Cambiar tipo de transacción
                dbmAgrario.SchemaFirmas.PA_Cambiar_Tipo_Transaccion.DBExecute(nidCargue)
            Catch ex As Exception
                Throw New Exception("Error en Finalización de Cargue" + ex.Message)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Public Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short) Implements IEventExecuter.FinalizarIndexacion

        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion

        End Sub

        Public Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSavePrimeraCaptura

            Valida_Captura_EnteVsFirmas(campos, fk_documento, Result)

        End Sub

        Public Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveSegundaCaptura

            Valida_Captura_EnteVsFirmas(campos, fk_documento, Result)

        End Sub

        Public Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveTerceraCaptura

            Valida_Captura_EnteVsFirmas(campos, fk_documento, Result)

        End Sub

        Public Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveCalidad

            Valida_Captura_EnteVsFirmas(campos, fk_documento, Result)

        End Sub

        Public Sub FinalizarPrecaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPreCaptura

        End Sub

        Public Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPrimeraCaptura

        End Sub

        Public Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarSegundaCaptura

        End Sub

        Public Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarTerceraCaptura

        End Sub

        Public Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarCalidad

        End Sub

        Public Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarValidaciones

        End Sub

        Public Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReclasificar
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmBancoAgrario.SchemaFirmas.PA_Cargar_Data_Reproceso.DBExecute(nidExpediente, nidFolder, nidFile)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Reproceso", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarRecorte(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarRecorte

            'Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            'Try
            '    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)

            '    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            '    dbmAgrario.SchemaFirmas.PA_Preparar_Data.DBExecute(nidExpediente, nidFolder, nidFile, nidVersion)

            'Catch ex As Exception
            '    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            'End Try

        End Sub

        Public Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.EnviarReproceso
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmBancoAgrario.SchemaFirmas.PA_Guardar_Data_Reproceso.DBExecute(nidExpediente, nidFolder, nidFile)
                'dbmBancoAgrario.SchemaCrossing.PA_New_00_Reversar_Cruce_File.DBExecute(nidExpediente, nidFolder, nidFile)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReproceso

        End Sub

        Public Sub AbrirFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(ByVal nidOt As Integer) Implements IEventExecuter.AbrirOt

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT

        End Sub

        Public Sub CerrarFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.CerrarFechaProceso

        End Sub

        Public Sub CerrarOt(ByVal nidOt As Integer) Implements IEventExecuter.CerrarOt

        End Sub

        Public Sub CrearFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.CrearFechaProceso

        End Sub

        Public Sub CrearOt(ByVal nidOt As Integer) Implements IEventExecuter.CrearOt

        End Sub

        Public Sub FinalizarPrecinto() Implements IEventExecuter.FinalizarPrecinto
            If (MessageBox.Show("Acaba de Finalizar el Precinto, Desea realizar la validación de tarjetas?", "Proceso de Firmas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                Dim formValidacionTarjetas As New Forms.Destape.FormValidarTarjetas(Me._plugin)
                formValidacionTarjetas.ShowDialog()
            End If
        End Sub

        Public Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarEmpaque
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                ' Validar si ya se realizó el cierre
                Dim cruces = dbmAgrario.SchemaFirmas.PA_Existe_Cruce.DBExecute(nidOT)
                If (cruces = 0) Then Throw New Exception("No se puede realizar el empaque de tarjetas si no se ha realizado el proceso de cruce")

                Dim valida = dbmAgrario.SchemaFirmas.PA_Empaque_Validar_Token.DBExecute(nidOt, nidEmpaque, nToken)
                If (valida = 0) Then
                    Throw New Exception("La tarjeta: " & nToken & " no coincide con el tipo de empaque")
                ElseIf (valida = -1) Then
                    Throw New Exception("La tarjeta: " & nToken & " no pertenece a la OT actual")
                End If

            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarProcesoAdicionalCaptura

        End Sub

        Public Sub GuardarContenedor(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer) Implements IEventExecuter.GuardarContenedor

        End Sub

        Public Sub ValidarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal ndbImaging As DBImaging.DBImagingDataBaseManager, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarPrecintoEmpaque

        End Sub

        Public Sub FinalizarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarPrecintoEmpaque

        End Sub

        Public Sub CargarPrecinto(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.CargarPrecinto

        End Sub

        Public Sub FinalizarEliminarPrecinto(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarEliminarPrecinto

        End Sub

        Public Sub ValidarSaveLabelCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal TipoCaptura As String, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveLabelCaptura

        End Sub

        Public Sub ValidarActualizarDatoBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal newValor_File_Data As Object, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarActualizarDatoBusqueda

        End Sub

        Public Sub FinalizarActualizarDatosBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarActualizarDatosBusqueda

        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Ejecutar_Cruce_En_Linea

        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarLoadConfig

        End Sub

        Public Sub FinalizarContenedorEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal IdEmpaqueContenedor As Integer) Implements IEventExecuter.FinalizarContenedorEmpaque

        End Sub

        Public Sub FinalizarContenedorEmpaqueEliminar(ByVal nidOt As Integer, ByVal ntoken As String) Implements IEventExecuter.FinalizarContenedorEmpaqueEliminar

        End Sub

        Public Sub Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.Reprocesar
        End Sub

        Public Sub Validar_Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidDocumento As Integer, ByVal nidCampo As Integer, ByVal nidCampoTablaAsociada As Integer, ByVal nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.Validar_Reprocesar

        End Sub

        Public Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Exportar
            Return String.Empty
        End Function

        Public Function ExtensionImagen_Plugin(ByVal Entrada As Boolean) As String Implements IEventExecuter.ExtensionImagen_Plugin
            Return String.Empty
        End Function

        Public Function IdFormatoImagen_Plugin(ByVal Entrada As Boolean) As Short Implements IEventExecuter.IdFormatoImagen_Plugin
            Return -1
        End Function

        Public Sub FinalizarCrucegenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarCruceGenerico

        End Sub

        Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Agrupada_Exportar
            Return String.Empty
        End Function

        Public Sub FinalizarCorreccionCapturaMaquina(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCorreccionCapturaMaquina

        End Sub

        Public Sub FinalizarPrimeraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrimeraCapturaAnexo

        End Sub

        Public Sub FinalizarTerceraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCapturaAnexo

        End Sub

        Public Sub FinalizarPrepararDatagenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarPrepararDataGenerico

        End Sub
#End Region

#Region " Métodos "

        Private Sub ExtraerDigitoYCodigo(ByVal nCBarras As String, ByVal nPosicion As Integer, ByRef nNewCBarras As String, ByRef nDigito As Integer)
            nNewCBarras = nCBarras.Substring(0, nPosicion) & "0" & nCBarras.Substring(nPosicion + 1, nCBarras.Length - nPosicion - 1)
            nDigito = CInt(nCBarras.Substring(nPosicion, 1))
        End Sub

        Private Sub Valida_Captura_EnteVsFirmas(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal nfk_Documento As Integer, ByRef Result As Boolean)
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim CamposValidacion_Datarow As DBAgrario.SchemaFirmasConfig.TBL_Validaciones_EspecialesRow
                Dim indexCampoTabla As Integer = 0
                Dim indexCampoValida As Integer = 0

                Try
                    CamposValidacion_Datarow = dbmAgrario.SchemaFirmasConfig.TBL_Validaciones_Especiales.DBFindByfk_Documento(nfk_Documento).Rows(0)
                Catch : End Try


                If Not CamposValidacion_Datarow Is Nothing Then


                    For Each Campo In campos

                        If (Campo.Control.Tipo = DesktopConfig.CampoTipo.TablaAsociada) Then

                            If CamposValidacion_Datarow.fk_campo_1 = Campo.id Then 'Id de la tabla Asociada

                                indexCampoTabla = campos.LastIndexOf(Campo)

                            End If

                        End If

                        If Campo.id = CamposValidacion_Datarow.fk_Campo_2 Then
                            indexCampoValida = campos.LastIndexOf(Campo)
                        End If

                    Next

                    If indexCampoTabla <> 0 And indexCampoValida <> 0 Then
                        Dim ValueTable As DataTable = CType(campos.Item(indexCampoTabla).Control.Value, DataTable)

                        If Not ValueTable.Rows.Count = CInt(campos.Item(indexCampoValida).Control.Value) Then

                            Result = False

                            MessageBox.Show("Por favor validar la captura ya que la cantidad de entes debe ser igual a la cantidad de firmas validadas")

                        End If

                    End If

                End If



            Catch ex As Exception
                Throw New Exception("Error en Captura, Validacion Ente vs cantidad de Firmas" + ex.Message)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        ''' <summary>
        ''' Método suministrado por el banco para validar el código de barras
        ''' </summary>
        ''' <param name="nCodigoBarra">Código de barras a calcular dígito de verificación</param>
        ''' <param name="nValor">Longitúd del código de barras a calcular</param>
        ''' <returns>Digito de verificación</returns>
        ''' <remarks></remarks>
        Function FmDigitoVerif(nCodigoBarra As String, nValor As Integer) As Integer
            Const vgTablaDigito As String = "716759534743413729231917130703"
            Dim vtSuma = 0
            Dim vtNumero As Integer
            Dim i As Integer
            Dim vtn As Integer
            Dim vtConstante As Integer
            For i = Len(nCodigoBarra) To 1 Step -1
                vtNumero = CInt(Mid(nCodigoBarra, i, 1))
                vtn = i + nValor - Len(nCodigoBarra)
                vtConstante = CInt(Val(Mid(vgTablaDigito, 2 * vtn, 1)) + 10 * Val(Mid(vgTablaDigito, 2 * vtn - 1, 1)))
                vtSuma = vtSuma + vtNumero * vtConstante
            Next i%

            Dim vtDig = vtSuma Mod 11
            If vtDig > 1 Then
                vtDig = 11 - vtDig
            End If

            FmDigitoVerif = vtDig
        End Function

        Function ValidacionEntes(ByVal frm As System.Windows.Forms.Form)

            For Each control_ As System.Windows.Forms.Control.ControlCollection In frm.Controls

            Next

        End Function

#End Region

    End Class

End Namespace