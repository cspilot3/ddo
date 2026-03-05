Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Plugins

Namespace Imaging.FormWrappers

    Public Class EventExecuterImaging
        Inherits EventExecuter
        Implements IEventExecuter
        
#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)
            Me._Plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "

        Public Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.EliminarImagen
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim coreIndexDataTable = dbmBancoAgrario.SchemaProcess.TBL_Core_Index.DBFindByfk_Expedientefk_Folderfk_File(nidExpediente, nidFolder, nidFile)
                'Dim id_Core_Index As Long

                If (coreIndexDataTable.Rows.Count > 0) Then
                    'Dim FacturacionDataTable = dbmBancoAgrario.SchemaProcess.TBL_Core_Index.DBGet(CoreIndexDataTable(0).id_Core_Index)
                    'If Not FacturacionDataTable(0).Facturado Then
                    ' Se actualiza el estado  de la Imagen a eliminada
                    Dim coreIndexType As New DBAgrario.SchemaProcess.TBL_Core_IndexType()
                    coreIndexType.Eliminado = True
                    dbmBancoAgrario.SchemaProcess.TBL_Core_Index.DBUpdate(coreIndexType, coreIndexDataTable(0).id_Core_Index)

                    '    MessageBox.Show("El File se marcó como eliminado debido a que ya se encuentra facturado", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If

                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarCargue(ByVal nidCargue As Integer) Implements IEventExecuter.FinalizarCargue

        End Sub

        Public Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short) Implements IEventExecuter.FinalizarIndexacion
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim indicesDataTable = dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_Cargue.DBExecute(nidCargue, nidPaquete)
                dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_Anexo_23.DBExecute(nidCargue, nidPaquete)

                For Each IndiceDataRow In indicesDataTable
                    dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_File_Sin_Salida.DBExecute(IndiceDataRow.fk_Expediente, IndiceDataRow.fk_Folder, IndiceDataRow.fk_File)
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Plugin Indexación", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)

                dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_Anexo_23.DBExecute(nidCargue, nidPaquete)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Plugin Re-Indexación", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSavePrimeraCaptura

        End Sub

        Public Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveSegundaCaptura

        End Sub

        Public Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveTerceraCaptura

        End Sub

        Public Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveCalidad

        End Sub

        Public Sub FinalizarPrecaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPreCaptura

        End Sub

        Public Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPrimeraCaptura
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmBancoAgrario.SchemaCrossing.PA_New_00_Cruce_En_Linea_Cola.DBExecute(nidExpediente, nidFolder, nidFile, "1")

                Dim anexo23DataTable = dbmBancoAgrario.SchemaConfig.TBL_Anexo_23.DBGet(Nothing)
                Dim fileDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(nidExpediente, nidFolder, nidFile)
                Dim codigoContenedor = String.Empty
                Dim tipoContenedor As Short = 0

                If (anexo23DataTable.Rows.Count > 0 And fileDataTable.Rows.Count > 0) _
                   AndAlso (anexo23DataTable(0).fk_Documento = fileDataTable(0).fk_Documento) Then

                    Dim codigoContenedorDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBGet(nidExpediente, nidFolder, nidFile, anexo23DataTable(0).fk_Documento, anexo23DataTable(0).Col_Codigo_Contenedor)
                    Dim tipoContenedorDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBGet(nidExpediente, nidFolder, nidFile, anexo23DataTable(0).fk_Documento, anexo23DataTable(0).Col_Tipo_Contenedor)

                    If codigoContenedorDataTable.Rows.Count > 0 AndAlso Not codigoContenedorDataTable(0).IsValor_File_DataNull() Then
                        codigoContenedor = CStr(codigoContenedorDataTable(0).Valor_File_Data)
                    End If

                    If tipoContenedorDataTable.Rows.Count > 0 AndAlso Not tipoContenedorDataTable(0).IsValor_File_DataNull() Then
                        tipoContenedor = CShort(tipoContenedorDataTable(0).Valor_File_Data)
                    End If

                    Dim destapeData = dbmBancoAgrario.SchemaProcess.TBL_Destape.DBFindByfk_Expedientefk_Folderfk_file(nidExpediente, nidFolder, nidFile)

                    If destapeData.Rows.Count > 0 Then
                        Dim destapeType = New DBAgrario.SchemaProcess.TBL_DestapeType

                        With destapeType
                            .codigo_Contenedor = codigoContenedor
                            .Tipo_Contenedor = tipoContenedor
                        End With

                        dbmBancoAgrario.SchemaProcess.TBL_Destape.DBUpdate(destapeType, destapeData(0).id_Destape)

                    End If

                End If


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarSegundaCaptura

        End Sub

        Public Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarTerceraCaptura
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_File.DBExecute(nidExpediente, nidFolder, nidFile)
                'Try
                'dbmBancoAgrario.SchemaCrossing.PA_New_00_Finalizar_Tercera_Captura.DBExecute(nidExpediente, nidFolder, nidFile)
                dbmBancoAgrario.SchemaCrossing.PA_New_00_Cruce_En_Linea_Cola.DBExecute(nidExpediente, nidFolder, nidFile, "3")
                'Catch ex As Exception
                'End Try

                Dim anexo23DataTable = dbmBancoAgrario.SchemaConfig.TBL_Anexo_23.DBGet(Nothing)
                Dim fileDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(nidExpediente, nidFolder, nidFile)
                Dim codigoContenedor = String.Empty
                Dim tipoContenedor As Short = 0

                If (anexo23DataTable.Rows.Count > 0 And fileDataTable.Rows.Count > 0) _
                   AndAlso (anexo23DataTable(0).fk_Documento = fileDataTable(0).fk_Documento) Then

                    Dim codigoContenedorDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBGet(nidExpediente, nidFolder, nidFile, anexo23DataTable(0).fk_Documento, anexo23DataTable(0).Col_Codigo_Contenedor)
                    Dim tipoContenedorDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBGet(nidExpediente, nidFolder, nidFile, anexo23DataTable(0).fk_Documento, anexo23DataTable(0).Col_Tipo_Contenedor)

                    If codigoContenedorDataTable.Rows.Count > 0 AndAlso Not codigoContenedorDataTable(0).IsValor_File_DataNull() Then
                        codigoContenedor = CStr(codigoContenedorDataTable(0).Valor_File_Data)
                    End If

                    If tipoContenedorDataTable.Rows.Count > 0 AndAlso Not tipoContenedorDataTable(0).IsValor_File_DataNull() Then
                        tipoContenedor = CShort(tipoContenedorDataTable(0).Valor_File_Data)
                    End If

                    Dim destapeData = dbmBancoAgrario.SchemaProcess.TBL_Destape.DBFindByfk_Expedientefk_Folderfk_file(nidExpediente, nidFolder, nidFile)

                    If destapeData.Rows.Count > 0 Then
                        Dim destapeType = New DBAgrario.SchemaProcess.TBL_DestapeType

                        With destapeType
                            .codigo_Contenedor = codigoContenedor
                            .Tipo_Contenedor = tipoContenedor
                        End With

                        dbmBancoAgrario.SchemaProcess.TBL_Destape.DBUpdate(destapeType, destapeData(0).id_Destape)

                    End If

                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarCalidad
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                'dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_File.DBExecute(nidExpediente, nidFolder, nidFile)
                'Try
                'dbmBancoAgrario.SchemaCrossing.PA_New_00_Finalizar_Tercera_Captura.DBExecute(nidExpediente, nidFolder, nidFile)
                dbmBancoAgrario.SchemaCrossing.PA_New_00_Cruce_En_Linea_Cola.DBExecute(nidExpediente, nidFolder, nidFile, "C")
                'Catch ex As Exception

                'End Try

                Dim anexo23DataTable = dbmBancoAgrario.SchemaConfig.TBL_Anexo_23.DBGet(Nothing)
                Dim fileDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(nidExpediente, nidFolder, nidFile)
                Dim codigoContenedor = String.Empty
                Dim tipoContenedor As Short = 0

                If (anexo23DataTable.Rows.Count > 0 And fileDataTable.Rows.Count > 0) _
                   AndAlso (anexo23DataTable(0).fk_Documento = fileDataTable(0).fk_Documento) Then

                    Dim codigoContenedorDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBGet(nidExpediente, nidFolder, nidFile, anexo23DataTable(0).fk_Documento, anexo23DataTable(0).Col_Codigo_Contenedor)
                    Dim tipoContenedorDataTable = dbmCore.SchemaProcess.TBL_File_Data.DBGet(nidExpediente, nidFolder, nidFile, anexo23DataTable(0).fk_Documento, anexo23DataTable(0).Col_Tipo_Contenedor)

                    If codigoContenedorDataTable.Rows.Count > 0 AndAlso Not codigoContenedorDataTable(0).IsValor_File_DataNull() Then
                        codigoContenedor = CStr(codigoContenedorDataTable(0).Valor_File_Data)
                    End If

                    If tipoContenedorDataTable.Rows.Count > 0 AndAlso Not tipoContenedorDataTable(0).IsValor_File_DataNull() Then
                        tipoContenedor = CShort(tipoContenedorDataTable(0).Valor_File_Data)
                    End If

                    Dim destapeData = dbmBancoAgrario.SchemaProcess.TBL_Destape.DBFindByfk_Expedientefk_Folderfk_file(nidExpediente, nidFolder, nidFile)

                    If destapeData.Rows.Count > 0 Then
                        Dim destapeType = New DBAgrario.SchemaProcess.TBL_DestapeType

                        With destapeType
                            .codigo_Contenedor = codigoContenedor
                            .Tipo_Contenedor = tipoContenedor
                        End With

                        dbmBancoAgrario.SchemaProcess.TBL_Destape.DBUpdate(destapeType, destapeData(0).id_Destape)

                    End If

                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarValidaciones
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmBancoAgrario.Transaction_Begin()
                dbmCore.Transaction_Begin()
                dbmImaging.Transaction_Begin()

                Dim fileDataTable = dbmCore.SchemaProcess.TBL_File.DBGet(nidExpediente, nidFolder, nidFile)
                Dim anexoDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(nidExpediente, nidFolder, CShort(nidFile + 1), Nothing)

                If (anexoDataTable.Count > 0 AndAlso Not anexoDataTable(0).Isfk_AnexoNull()) Then
                    Dim validacionesHeredadasDataTable = dbmBancoAgrario.SchemaConfig.TBL_Validaciones_Heredadas.DBGet(fileDataTable(0).fk_Documento)

                    If (validacionesHeredadasDataTable.Count > 0 AndAlso validacionesHeredadasDataTable(0).Heredar) Then
                        Dim folderRow = dbmCore.SchemaImaging.TBL_Folder.DBGet(nidExpediente, nidFolder)(0)

                        Dim filesDataTable = dbmCore.SchemaImaging.CTA_File.DBFindByfk_Carguefk_Cargue_Paquetefk_AnexoValidaciones_Opcionales(folderRow.fk_Cargue, folderRow.fk_Cargue_Paquete, anexoDataTable(0).fk_Anexo, False)
                        Dim validacionesDataTable = dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(nidExpediente, nidFolder, nidFile, Nothing, fileDataTable(0).fk_Documento)

                        For Each FileRow In filesDataTable
                            ' Borrar las validaciones actuales
                            dbmCore.SchemaProcess.TBL_File_Validacion.DBDelete(FileRow.fk_Expediente, FileRow.fk_Folder, CShort(1), Nothing, fileDataTable(0).fk_Documento)

                            ' Responder las validaciones
                            For Each Validacion In validacionesDataTable
                                dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(FileRow.fk_Expediente, FileRow.fk_Folder, 1, Validacion.fk_Validacion, Validacion.Respuesta, Nothing, fileDataTable(0).fk_Documento)
                            Next

                            ' Eliminar de Dashboard
                            dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(FileRow.fk_Expediente, FileRow.fk_Folder, CShort(1))

                            ' Marcar File
                            Dim fileType = New DBCore.SchemaImaging.TBL_FileType()
                            fileType.Validaciones_Opcionales = True
                            dbmCore.SchemaImaging.TBL_File.DBUpdate(fileType, FileRow.fk_Expediente, FileRow.fk_Folder, CShort(1), Nothing)
                        Next
                    End If
                End If

                dbmBancoAgrario.Transaction_Commit()
                dbmCore.Transaction_Commit()
                dbmImaging.Transaction_Commit()

            Catch ex As Exception
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Transaction_Rollback()
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                DesktopMessageBoxControl.DesktopMessageShow("Cargue", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReclasificar
            'Dim dbmBancoAgrario As DBAgrarioDataBaseManager = Nothing

            'Try
            '    dbmBancoAgrario = New DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)

            '    dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            '    dbmBancoAgrario.SchemaProcess.PA_Preparar_Data_File.DBExecute(nidExpediente, nidFolder, nidFile)

            'Catch ex As Exception
            '    DMB.DesktopMessageShow("Cargue", ex)
            'Finally
            '    If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            'End Try
        End Sub

        Public Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.EnviarReproceso
            Dim dbmBancoAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmBancoAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)

                dbmBancoAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmBancoAgrario.SchemaCrossing.PA_New_00_Reversar_Cruce_File.DBExecute(nidExpediente, nidFolder, nidFile)
                dbmBancoAgrario.SchemaProcess.PA_Eliminar_Cruce_Votacion_Reproceso.DBExecute(nidExpediente, nidFolder, nidFile)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Reproceso", ex)
            Finally
                If (dbmBancoAgrario IsNot Nothing) Then dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReproceso

        End Sub

        Public Sub FinalizarRecorte(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements IEventExecuter.FinalizarRecorte

        End Sub

        Public Sub AbrirFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(nidOt As Integer) Implements IEventExecuter.AbrirOt

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT

        End Sub

        Public Sub CerrarFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements IEventExecuter.CerrarFechaProceso

        End Sub

        Public Sub CerrarOt(nidOt As Integer) Implements IEventExecuter.CerrarOt

        End Sub

        Public Sub CrearFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements IEventExecuter.CrearFechaProceso

        End Sub

        Public Sub CrearOt(nidOt As Integer) Implements IEventExecuter.CrearOt

        End Sub

        Public Sub FinalizarPrecinto() Implements IEventExecuter.FinalizarPrecinto

        End Sub

        Public Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarEmpaque

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

    End Class

End Namespace