Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopDataGridView
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Microsoft.Reporting.WinForms
Imports Slyg.Tools.Imaging
Imports Slyg.Tools
Imports Miharu.Security.Library.Session
Imports System.IO
Imports System.Text
Imports System.Runtime.CompilerServices
Imports Miharu.Desktop.Controls.Validation

Namespace Config

    Public Class Utilities

#Region "Manejo Datatables"

        Public Shared Function ClonarDataTable(ByVal dtIn As DataTable) As DataTable
            Dim dtClonado As New DataTable
            Dim rowOut As DataRow

            Try

                'Crear columnas al nuevo datatable
                For Each col As DataColumn In dtIn.Columns
                    dtClonado.Columns.Add(col.ColumnName, GetType(String))
                Next

                For Each row As DataRow In dtIn.Rows
                    rowOut = dtClonado.NewRow()
                    For i As Integer = 0 To row.ItemArray.Length - 1
                        If (Not IsDBNull(row(i))) Then
                            rowOut(i) = row(i)
                        Else
                            rowOut(i) = ""
                        End If
                    Next
                    dtClonado.Rows.Add(rowOut)
                Next
            Catch ex As Exception
                Throw New Exception()
            End Try

            Return dtClonado
        End Function

        Public Shared Function CountTable(ByVal nTable As DataTable, ByVal ParamArray nParametros As String()) As DataTable
            Dim table2 As New DataTable

            Dim column As DataColumn
            For Each parametro As String In nParametros
                column = New DataColumn
                column.ColumnName = parametro
                table2.Columns.Add(column)
            Next
            column = New DataColumn
            column.ColumnName = "Count"
            table2.Columns.Add(column)


            Dim view As DataView = ClonarDataTable(nTable).DefaultView

            Dim row As DataRow
            For Each rowColumna As DataRow In view.ToTable(True, nParametros).Rows
                row = table2.NewRow

                For Each parametro As String In nParametros
                    row(parametro) = rowColumna(parametro)
                Next

                table2.Rows.Add(row)
            Next

            For Each rowParametro As DataRow In table2.Rows
                Dim viewCount As DataView = ClonarDataTable(nTable).DefaultView

                Dim filtro(nParametros.Length - 1) As String
                Dim count As Integer = 0
                For Each parametro As String In nParametros
                    filtro(count) = parametro & "= '" & rowParametro(parametro).ToString & "'"
                    count += 1
                Next

                viewCount.RowFilter = Join(filtro, " and ")
                rowParametro("Count") = viewCount.ToTable.Rows.Count
            Next

            Return table2
        End Function
#End Region

#Region "Manejo DataGridViews"

        Public Shared Sub DataGridViewToCsv(ByRef data As DesktopDataGridViewControl, ByRef archivo As FileStream, Optional ByVal separator As String = ",", Optional ByVal nEncabezado As Boolean = True)
            Using sw As New StreamWriter(archivo, Encoding.UTF8)
                Dim strExport As String = ""
                Dim final As Integer

                If nEncabezado Then
                    final = 1
                    For Each dc As DataGridViewColumn In data.Columns
                        If final < data.ColumnCount Then
                            strExport += dc.Name + separator
                            final += 1
                        Else
                            strExport += dc.Name
                        End If
                    Next
                    'strExport = strExport.Substring(0, strExport.Length - 3) & Environment.NewLine.ToString()
                    sw.Write(strExport & Environment.NewLine.ToString())
                End If
                For Each dr As DataGridViewRow In data.Rows
                    strExport = ""
                    final = 1
                    For Each dc As DataGridViewCell In dr.Cells
                        If dc.Value IsNot Nothing Then
                            strExport += dc.Value.ToString()
                        Else
                            strExport += ""
                        End If
                        If final < dr.Cells.Count Then
                            strExport += separator
                        End If
                        final += 1
                    Next
                    'strExport = strExport.Substring(0, strExport.Length - 3) & Environment.NewLine.ToString()
                    sw.Write(strExport & Environment.NewLine.ToString())
                Next
                sw.Close()
            End Using
        End Sub
#End Region

#Region " Security "

        Public Shared Function ValidarPermiso(ByVal nPermiso As String, ByVal nAsseblyName As String, ByVal nMensaje As String, ByVal nUsuario As Usuario, ByVal nSecurityServiceUrl As String, ByVal nClientIpAddress As String) As Boolean
            If (Not nUsuario.PerfilManager.PuedeEditar(nPermiso)) Then
                Dim dlgAutorizacion As New FormAutorizacion(nMensaje, nAsseblyName, nPermiso, nSecurityServiceUrl, nClientIPAddress)
                If (dlgAutorizacion.ShowDialog() = DialogResult.OK) Then
                    Return True
                End If
            Else
                Return True
            End If

            Return False
        End Function


        ''Modificación Desarrollo V8 - Destape
        ''Sobrecarga al metodo Validar Permiso
        'Public Shared Function ValidarPermiso(ByVal nPermiso As String, ByVal nAsseblyName As String, ByVal nMensaje As String, ByVal nConnectionStringSecurity As String, ByVal nUsuario As Usuario) As Boolean
        '    If (Not nUsuario.PerfilManager.PuedeEditar(nPermiso)) Then
        '        Dim dbmSecurity As DBSecurityDataBaseManager = Nothing
        '        Try
        '            dbmSecurity = New DBSecurityDataBaseManager(nConnectionStringSecurity)
        '            dbmSecurity.Connection_Open(nUsuario.id)

        '            Dim dlgAutorizacion As New FormAutorizacion(dbmSecurity, nMensaje, nAsseblyName, nPermiso)
        '            If (dlgAutorizacion.ShowDialog() <> DialogResult.OK) Then
        '                Return False
        '            End If
        '            Return True
        '        Catch
        '            Throw
        '        Finally
        '            Try : dbmSecurity.Connection_Close() : Catch : End Try
        '        End Try
        '    Else
        '        Return True
        '    End If
        'End Function

#End Region

#Region "Core"

        Public Shared Function InsertaFolderCore(ByRef dmCore As DBCore.DBCoreDataBaseManager, ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nRiskGlobal As RiskGlobal, ByVal nEsquema As Integer, ByVal nListLlaves As List(Of DesktopConfig.StrLlaves), ByVal nConnectionStrings As DesktopConfig.TypeConnectionString, ByVal nUsuario As Integer, Optional ByVal nEstado As DBCore.EstadoEnum = DBCore.EstadoEnum.Cargado, Optional ByVal nCBarrasFile As String = "") As DesktopConfig.FolderCORE
            Dim objFolder = FindFolderByKeys(dmArchiving, nListLlaves, nRiskGlobal, nConnectionStrings)

            'Valida que el folder no exista, si existe devuelve los valores del existente.
            Dim sReturn As DesktopConfig.FolderCORE = Nothing
            sReturn.Expediente = 0
            sReturn.Folder = 0

            If objFolder.Existe Then
                sReturn.Expediente = objFolder.fk_Expediente
                sReturn.Folder = objFolder.id_Folder

                'Estado File
                Dim dtFileCoreData = dmCore.Schemadbo.CTA_File_Estado.DBFindByfk_Expedientefk_FolderModulo(objFolder.fk_Expediente, objFolder.id_Folder, DesktopConfig.Modulo.Archiving)

                'Se actualizan únicamente los elementos que se encuentran en estado mayor a 2000-Enviado a prestamo
                Dim dtFileCore = dtFileCoreData.DefaultView

                If nCBarrasFile = "" Then
                    dtFileCore.RowFilter = "fk_Estado >=" & DBCore.EstadoEnum.Enviado_a_prestamo
                Else
                    dtFileCore.RowFilter = "fk_Estado >=" & DBCore.EstadoEnum.Enviado_a_prestamo & " AND CBarras_File='" & nCBarrasFile & "'"
                End If

                For Each file In dtFileCore
                    Dim tFileCore As New DBCore.SchemaProcess.TBL_File_EstadoType
                    tFileCore.fk_Estado = nEstado
                    tFileCore.fk_Usuario = nUsuario
                    tFileCore.Fecha_Log = SlygNullable.SysDate
                    dmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tFileCore, objFolder.fk_Expediente, objFolder.id_Folder, CShort(file("fk_File")), DesktopConfig.Modulo.Archiving)
                Next
            End If

            'Si no existe el folder, se crea.
            If sReturn.Expediente = 0 Or sReturn.Folder = 0 Then

                Dim tableEsquema = dmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyectofk_esquema(nRiskGlobal.Entidad, nRiskGlobal.Proyecto, Nothing)

                Dim viewEsquema As DataView = ClonarDataTable(tableEsquema).DefaultView
                viewEsquema.RowFilter = "fk_esquema='" & CStr(nEsquema) & "'"
                Dim tableTrDs As DataTable = viewEsquema.ToTable(True)

                'Crea el EXPEDIENTE.
                Dim idExpediente = dmCore.SchemaProcess.PA_Insertar_Expediente.DBExecute(nRiskGlobal.Entidad, nRiskGlobal.Proyecto, CShort(nEsquema), CShort(tableTrDs.Rows(0)("fk_TRD").ToString), CShort(tableTrDs.Rows(0)("fk_TRD_serie").ToString), CShort(tableTrDs.Rows(0)("fk_TRD_subserie").ToString), Nothing)

                'Crea el FOLDER.
                Dim registroFolder As New DBCore.SchemaProcess.TBL_FolderType
                registroFolder.Fecha_Final = Nothing
                registroFolder.Fecha_Inicial = Nothing
                registroFolder.fk_Expediente = idExpediente
                registroFolder.id_Folder = dmCore.SchemaProcess.TBL_Folder.DBNextId(idExpediente)
                registroFolder.CBarras_Folder = TipologiaFomat(0) & CbarrasSoloFomat(CInt(NextCBarras(dmArchiving, DesktopConfig.Consecutivo.CBarras)))
                dmCore.SchemaProcess.TBL_Folder.DBInsert(registroFolder)
                sReturn.Folder = registroFolder.id_Folder
                sReturn.Expediente = idExpediente


                'Crea el ESTADO DEL FOLDER
                Dim registroFolderEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType
                registroFolderEstado.fk_Estado = nEstado
                registroFolderEstado.fk_Expediente = registroFolder.fk_Expediente
                registroFolderEstado.fk_Folder = registroFolder.id_Folder
                registroFolderEstado.Modulo = DesktopConfig.Modulo.Archiving
                registroFolderEstado.fk_Usuario = nUsuario
                registroFolderEstado.Fecha_Log = SlygNullable.SysDate

                dmCore.SchemaProcess.TBL_Folder_estado.DBInsert(registroFolderEstado)

                'Crea las LLAVES DE EXPEDIENTE.
                Dim registroLlavesExpediente As New DBCore.SchemaProcess.TBL_Expediente_LlaveType
                Dim value As New Object

                registroLlavesExpediente.fk_Expediente = idExpediente

                For Each llave As DesktopConfig.StrLlaves In nListLlaves
                    registroLlavesExpediente.fk_campo_tipo = llave.Tipo
                    registroLlavesExpediente.fk_proyecto_Llave = llave.id_Llave

                    Select Case llave.Tipo
                        Case DesktopConfig.CampoTipo.Texto
                            value = llave.Valor_Llave.ToString()
                        Case DesktopConfig.CampoTipo.Numerico
                            value = CLng(llave.Valor_Llave)
                        Case DesktopConfig.CampoTipo.Fecha
                            Dim arrDate = llave.Valor_Llave.ToString().Split(CChar(" "))
                            value = CDate(arrDate(0))
                        Case DesktopConfig.CampoTipo.Lista
                            value = CInt(llave.Valor_Llave)
                    End Select

                    registroLlavesExpediente.Valor_Llave = value
                    dmCore.SchemaProcess.TBL_Expediente_Llave.DBInsert(registroLlavesExpediente)
                Next
            End If

            Return sReturn
        End Function

        Public Shared Function TipologiaFomat(ByVal nTipologia As Integer) As String
            Return String.Concat("000", CStr(nTipologia)).Substring(String.Concat("000", CStr(nTipologia)).Length - 3)
        End Function

        Public Shared Function CbarrasSoloFomat(ByVal nCBarras As Integer) As String
            Return String.Concat("000000000", CStr(nCBarras)).Substring(String.Concat("000000000", CStr(nCBarras)).Length - 9)
        End Function

        Public Shared Function CBarrasFlash(ByVal nCBarrasShort As String) As String
            Dim tipologia As String = nCBarrasShort.Substring(1, 3)
            Dim cBarras As String = nCBarrasShort.Substring(4)
            Return tipologia & CbarrasSoloFomat(CInt(cBarras))
        End Function



        Public Shared Function NextCBarras(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nTipo As DesktopConfig.Consecutivo) As Decimal
            Dim table As DataTable = dmArchiving.SchemaConfig.PA_NextConsecutivo.DBExecute(nTipo)

            If table.Rows.Count = 0 Then Throw New Exception("EL id de la secuencia no existe en la tabla de Secuencias.")

            Return CDec(table.Rows(0)(0))
        End Function

        Public Shared Function InsertaFileCore(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Integer, ByVal nDocumento As Integer, ByVal nRiskGlobal As RiskGlobal) As Short
            Dim tableTipologia = dmArchiving.Schemadbo.CTA_Tipologias_Esquema.DBFindByfk_entidadfk_proyectoid_documento(nRiskGlobal.Entidad, nRiskGlobal.Proyecto, nDocumento)

            Dim registro As New DBCore.SchemaProcess.TBL_FileType
            registro.fk_Documento = nDocumento
            registro.fk_Expediente = nExpediente
            registro.fk_Folder = nFolder
            registro.File_Unique_Identifier = Guid.NewGuid
            registro.Folios_File = 1
            registro.Monto_File = 0
            registro.CBarras_File = TipologiaFomat(tableTipologia.Rows(0)(tableTipologia.id_tipologiaColumn).ToString) & CbarrasSoloFomat(NextCBarras(dmArchiving, DesktopConfig.Consecutivo.CBarras))

            'Valida si existe un documento de la misma tipología en estado 0:Faltante, y se utiliza este.
            Dim dtFileEstado = dbmCore.Schemadbo.CTA_File_Estado.DBFindByfk_Expedientefk_FolderModulofk_Estado(nExpediente, nFolder, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Faltante)
            If dtFileEstado.Count > 0 Then
                registro.id_File = dtFileEstado(0).fk_File
                dbmCore.SchemaProcess.TBL_File.DBUpdate(registro, nExpediente, nFolder, dtFileEstado(0).fk_File)
            Else
                registro.id_File = dbmCore.SchemaProcess.TBL_File.DBNextId(nExpediente, nFolder)
                dbmCore.SchemaProcess.TBL_File.DBInsert(registro)
            End If

            Return registro.id_File
        End Function

        Public Shared Sub InsertaFileDataCore(ByRef dmCore As DBCore.DBCoreDataBaseManager, ByRef dtCamposDataValido As DataView, ByRef registro As DataRow, ByVal nNumLlaves As Integer, ByVal nExpediente As Long, ByVal nFolder As Integer, ByVal nFile As Integer, ByVal nDocumento As Integer, ByVal nEsCargue As Boolean, ByVal nDataAdicional As Boolean)
            Const camposEstandar As Integer = 4

            For i = 0 To dtCamposDataValido.Count - 1
                Dim campo = CInt(dtCamposDataValido(i).Item("fk_Campo"))
                Dim tipoLlave = CInt(dtCamposDataValido(i).Item("fk_Campo_Tipo"))

                Dim valorCampo
                If nDataAdicional Then
                    valorCampo = registro(nNumLlaves + camposEstandar + i) ' Inserta la data Adicional
                Else
                    valorCampo = registro(1 + i) ' Inserta las llaves
                End If

                Dim fileData As New DBCore.SchemaProcess.TBL_File_DataType

                Select Case tipoLlave
                    Case DesktopConfig.CampoTipo.Texto
                        valorCampo = valorCampo.ToString()

                    Case DesktopConfig.CampoTipo.Numerico
                        valorCampo = CLng(valorCampo)

                    Case DesktopConfig.CampoTipo.Fecha
                        Dim arrDate = valorCampo.ToString().Split(CChar(" "))
                        valorCampo = CDate(arrDate(0))
                End Select

                fileData.fk_Expediente = nExpediente
                fileData.fk_Folder = nFolder
                fileData.fk_File = nFile

                If nDataAdicional Then
                    fileData.fk_Documento = nDocumento
                Else
                    fileData.fk_Documento = CInt(dtCamposDataValido(i).Item("fk_Documento"))
                End If

                fileData.fk_Campo = campo
                fileData.Valor_File_Data = valorCampo
                If nEsCargue Then
                    fileData.Conteo_File_Data = 0
                Else
                    fileData.Conteo_File_Data = valorCampo.ToString().Length
                End If

                'Si existe la data, la actualiza.
                Dim dtData = dmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(nExpediente, nFolder, nFile, nDocumento, campo)
                If dtData.Count > 0 Then
                    dmCore.SchemaProcess.TBL_File_Data.DBUpdate(fileData, nExpediente, nFolder, nFile, nDocumento, campo)
                Else
                    dmCore.SchemaProcess.TBL_File_Data.DBInsert(fileData)
                End If
            Next
        End Sub

        Public Shared Function FindFolderByKeys(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nLlaves As List(Of DesktopConfig.StrLlaves), ByVal nRiskGlobal As RiskGlobal, ByVal nConnectionStrings As DesktopConfig.TypeConnectionString) As DesktopConfig.Folder

            Dim varFolder As New DesktopConfig.Folder
            varFolder.Existe = False
            varFolder.fk_Expediente = 0
            varFolder.id_Folder = 0
            varFolder.fk_Esquema = 0
            varFolder.CBarras_Folder = ""
            varFolder.fk_Ot = 0
            varFolder.CBarras_File = ""
            varFolder.Es_File = False
            varFolder.Folder = Nothing
            varFolder.EstadoCore = Nothing
            varFolder.EstadoCargado = False
            varFolder.OTEstadoCargado = 0

            Dim filtro(9) As SlygNullable(Of String)
            Dim i As Integer = 0

            For Each llave As DesktopConfig.StrLlaves In nLlaves
                filtro(i) = llave.Valor_Llave
                i += 1
            Next

            'TODO: Validar como se recupera el esquema.
            Dim expedienteDataTable = dmArchiving.SchemaCore.CTA_Expediente_Llave_enLinea.DBFindByfk_Entidadfk_Proyectofk_EsquemaKey_1Key_2Key_3Key_4Key_5Key_6Key_7Key_8Key_9Key_10(nRiskGlobal.Entidad, nRiskGlobal.Proyecto, Nothing, filtro(0), filtro(1), filtro(2), filtro(3), filtro(4), filtro(5), filtro(6), filtro(7), filtro(8), filtro(9))
            'Dim expedienteDataTable = dmArchiving.SchemaCore.PA_Expediente_Llave_enLinea.DBExecute(nRiskGlobal.Entidad, nRiskGlobal.Proyecto, Nothing, filtro(0), filtro(1), filtro(2), filtro(3), filtro(4), filtro(5), filtro(6), filtro(7), filtro(8), filtro(9))

            If (expedienteDataTable.Count > 0) Then
                Dim folderDataTable = dmArchiving.SchemaCore.CTA_Folder.DBFindByfk_Expedienteid_Folder(expedienteDataTable(0).id_Expediente, Nothing, 1, New DBArchiving.SchemaCore.CTA_FolderEnumList(DBArchiving.SchemaCore.CTA_FolderEnum.id_Folder, False))

                If (folderDataTable.Count > 0) Then
                    varFolder = FindFolderByCBarras(dmArchiving, folderDataTable(0).CBarras_Folder)
                End If
            End If

            Return varFolder
        End Function

        Public Shared Function FindFolderByCBarras(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nCBarras As String) As DesktopConfig.Folder
            Dim tableFolderLlaves = dmArchiving.Schemadbo.CTA_Folder_llaves.DBFindByCBarras_folder(nCBarras)

            Dim varFolder As New DesktopConfig.Folder
            varFolder.Existe = False
            varFolder.fk_Expediente = 0
            varFolder.id_Folder = 0
            varFolder.fk_Esquema = 0
            varFolder.CBarras_Folder = ""
            varFolder.fk_Ot = 0
            varFolder.CBarras_File = ""
            varFolder.Es_File = False
            varFolder.Folder = Nothing
            varFolder.EstadoCore = Nothing
            varFolder.EstadoCargado = False
            varFolder.OTEstadoCargado = 0
            varFolder.EstadoDestapado = False
            varFolder.OTEstadoDestapado = 0

            For Each row As DBArchiving.Schemadbo.CTA_Folder_llavesRow In tableFolderLlaves
                varFolder.Existe = True
                varFolder.fk_Expediente = row.fk_expediente
                varFolder.id_Folder = row.id_folder
                varFolder.fk_Esquema = row.fk_esquema
                varFolder.CBarras_Folder = row.CBarras_folder

                If Not row.Isfk_estadoNull AndAlso (row.fk_estado = DBCore.EstadoEnum.Cargado Or row.fk_estado = DBCore.EstadoEnum.Destapado) Then
                    varFolder.fk_Ot = row.fk_OT
                End If

                If Not row.Isfk_estadoNull AndAlso row.fk_estado = DBCore.EstadoEnum.Cargado Then
                    varFolder.EstadoCargado = True
                    varFolder.OTEstadoCargado = row.fk_OT

                    If row.Isfk_precintoNull Then
                        varFolder.PrecintoCargado = ""
                    Else
                        varFolder.PrecintoCargado = row.fk_precinto
                    End If
                End If

                If Not row.Isfk_estadoNull AndAlso row.fk_estado = DBCore.EstadoEnum.Destapado Then
                    varFolder.EstadoDestapado = True
                    varFolder.OTEstadoDestapado = row.fk_OT

                    If row.Isfk_precintoNull Then
                        varFolder.PrecintoDestapado = ""
                    Else
                        varFolder.PrecintoDestapado = row.fk_precinto
                    End If
                End If

                If Not row.Isfk_estadoNull AndAlso row.fk_estado = DBCore.EstadoEnum.Reproceso Then
                    varFolder.EstadoReproceso = True
                    varFolder.OTEstadoReproceso = row.fk_OT

                    If row.Isfk_precintoNull Then
                        varFolder.PrecintoReproceso = ""
                    Else
                        varFolder.PrecintoReproceso = row.fk_precinto
                    End If
                End If

                varFolder.Folder = row.ToCTA_Folder_llavesSimpleType()

                varFolder.EstadoCore = dmArchiving.SchemaCore.CTA_Folder_Estado.DBFindByfk_Expedientefk_FolderModulo(row.fk_expediente, row.id_folder, DesktopConfig.Modulo.Archiving)(0).ToCTA_Folder_EstadoSimpleType()
            Next

            Return varFolder
        End Function

        Public Shared Function FindFileByCBarras(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nCBarras As String) As DesktopConfig.Folder
            Dim tableFileLlaves = dmArchiving.Schemadbo.CTA_File_llaves.DBFindByCBarras_file(nCBarras)

            Dim varFolder As New DesktopConfig.Folder
            varFolder.Existe = False
            varFolder.fk_Expediente = 0
            varFolder.id_Folder = 0
            varFolder.fk_Esquema = 0
            varFolder.CBarras_Folder = ""
            varFolder.fk_Ot = 0
            varFolder.CBarras_File = ""
            varFolder.Es_File = False
            varFolder.Folder = Nothing
            varFolder.EstadoCore = Nothing
            varFolder.EstadoCargado = False

            For Each row As DBArchiving.Schemadbo.CTA_File_llavesRow In tableFileLlaves
                varFolder = FindFolderByCBarras(dmArchiving, row.CBarras_folder)
                varFolder.CBarras_File = nCBarras
                varFolder.Es_File = True
            Next

            Return varFolder
        End Function

#End Region

#Region "Archiving"
        Public Shared Function PermiteDevolucion(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nCBarras As String) As DesktopConfig.Devolucion
            Dim valida As DesktopConfig.Devolucion
            valida.PermiteDevolucion = True
            valida.File = 0
            valida.OT = 0
            valida.LineaProceso = 0

            'Valida que exista el codigo de barras en la base de datos
            Dim tableDevolucion As DataTable = dmArchiving.Schemadbo.CTA_Llaves_CodigoFile.DBFindByCBarras_file(nCBarras)
            If tableDevolucion.Rows.Count > 0 Then

                'Valida que el documento este en prestamo y pueda ser devuelto
                Dim tableDevoluciones As DataTable = dmArchiving.Schemadbo.CTA_Files_Prestamo.DBFindByCBarras_file(nCBarras)
                If Not tableDevoluciones.Rows.Count = 0 Then
                    valida.File = CShort(tableDevoluciones.Rows(0)("id_file").ToString)
                    valida.OT = CInt(tableDevoluciones.Rows(0)("fk_OT").ToString)
                    valida.LineaProceso = CInt(tableDevoluciones.Rows(0)("fk_Linea_Proceso").ToString)
                    valida.PermiteDevolucion = True
                Else
                    valida.PermiteDevolucion = False
                End If

            Else
                valida.PermiteDevolucion = False
            End If

            Return valida
        End Function

        Public Shared Function CargueFilesDisponibles(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nRiskGlobal As RiskGlobal, ByVal nEsquema As Short, ByVal nFolder As Short, ByVal nExpediente As Long, ByVal nDocumento As Short, ByVal nEstado As Slyg.Tools.SlygNullable(Of Short), ByVal ot As Integer) As DesktopConfig.FilesEstadoCargue
            Dim valor As DesktopConfig.FilesEstadoCargue
            valor.FilesDisponibles = False
            valor.IdFile = ""
            valor.Ot = 0
            valor.AceptaSobrantes = False
            valor.Cbarras = ""

            Dim tipologia As Short = CShort(dmArchiving.SchemaCore.CTA_Documento.DBFindByid_Documento(nDocumento).Rows(0)("fk_tipologia"))
            Dim tableFilesDisponibles = dmArchiving.Schemadbo.CTA_Files_Disponibles.DBFindByfk_folderfk_Expedientefk_tipologiafk_estadoAdicones_Nuevos(nFolder, nExpediente, tipologia, nEstado, True)
            Dim tableEsquema = nRiskGlobal.CTA_Esquema_DBFindByfk_entidadfk_proyectofk_esquema(nRiskGlobal.Entidad, nRiskGlobal.Proyecto, nEsquema)
            valor.AceptaSobrantes = CBool(tableEsquema.Rows(0)("Acepta_Sobrantes").ToString)



            If Not tableFilesDisponibles.Rows.Count = 0 Then
                If tableFilesDisponibles.Rows.Count > 1 Then
                    Dim data As New DataView(tableFilesDisponibles)
                    data.RowFilter = tableFilesDisponibles.fk_OTColumn.ColumnName & " = " & ot.ToString()
                    Dim dataFinal = data.ToTable()

                    If dataFinal.Rows.Count > 0 Then
                        valor.FilesDisponibles = True
                        valor.IdFile = dataFinal.Rows(0)(tableFilesDisponibles.id_FileColumn.ColumnName).ToString()
                        valor.Ot = dataFinal.Rows(0)(tableFilesDisponibles.fk_OTColumn.ColumnName).ToString()
                        valor.Cbarras = dataFinal.Rows(0)(tableFilesDisponibles.cbarras_fileColumn.ColumnName).ToString()
                    Else
                        valor.FilesDisponibles = True
                        valor.IdFile = tableFilesDisponibles(0).id_File.ToString
                        valor.Ot = tableFilesDisponibles(0).fk_OT.ToString
                        valor.Cbarras = tableFilesDisponibles(0).cbarras_file.ToString
                    End If

                Else
                    valor.FilesDisponibles = True
                    valor.IdFile = tableFilesDisponibles(0).id_File.ToString
                    valor.Ot = tableFilesDisponibles(0).fk_OT.ToString
                    valor.Cbarras = tableFilesDisponibles(0).cbarras_file.ToString
                End If
            End If

            Return valor
        End Function

        Public Shared Function CargueFilesDisponibles(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal riskGlobal As RiskGlobal, ByVal nCBarrasFile As String) As DesktopConfig.FilesEstadoCargue
            Dim valor As DesktopConfig.FilesEstadoCargue
            valor.FilesDisponibles = False
            valor.IdFile = ""
            valor.Ot = 0
            valor.AceptaSobrantes = True
            valor.Cbarras = ""

            Dim tableFilesDisponibles = dmArchiving.Schemadbo.CTA_Files_Disponibles.DBFindBycbarras_filefk_estadoDevoluciones(nCBarrasFile, DBCore.EstadoEnum.Cargado, True)

            If Not tableFilesDisponibles.Rows.Count = 0 Then
                valor.FilesDisponibles = True
                valor.IdFile = tableFilesDisponibles.Rows(0)("id_File").ToString
                valor.Ot = tableFilesDisponibles.Rows(0)("fk_OT").ToString
                valor.Cbarras = tableFilesDisponibles(0).cbarras_file
            End If

            Return valor
        End Function


        Public Shared Function ExisteFolderArchiving(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal ot As SlygNullable(Of Integer)) As Boolean
            Dim bReturn As Boolean
            Dim dtFolderArchiving = dmArchiving.SchemaRisk.TBL_Folder.DBFindByfk_expedienteid_Folderfk_OT(nExpediente, nFolder, OT)

            If dtFolderArchiving.Count = 0 Then 'Not (dtFolderArchiving.Count <> 0 Or dtFolderEstadosCore.Count <> 0) Then
                bReturn = False
            Else
                bReturn = True
            End If

            Return bReturn
        End Function

        Public Shared Sub InsertaFileArchiving(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nFile As Integer, ByVal nFolder As Short, ByVal nExpediente As Long, ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso, ByVal nSobrante As Boolean, ByVal nClase As DesktopConfig.RegistroTipo, ByVal ot As Integer, ByVal nCajaProceso As SlygNullable(Of String), ByVal nEstado As DBCore.EstadoEnum, ByVal nCargado As Boolean, ByVal nUsuario As Integer, Optional ByVal nLineaProceso As SlygNullable(Of Integer) = Nothing)
            'Valida si existe un documento de la misma tipología en estado 0:Faltante, y se utiliza este.
            Dim dtFileRisk = dmArchiving.SchemaRisk.TBL_File.DBFindByfk_Folderid_Filefk_expedientefk_Estado(nFolder, nFile, nExpediente, DBCore.EstadoEnum.Faltante)

            Dim registroFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
            registroFileArchiving.fk_OT = ot
            registroFileArchiving.fk_Folder = nFolder
            registroFileArchiving.id_File = nFile
            registroFileArchiving.fk_expediente = nExpediente
            registroFileArchiving.Es_sobrante = nSobrante
            registroFileArchiving.fk_Caja_Proceso = nCajaProceso
            registroFileArchiving.fk_Estado = nEstado
            registroFileArchiving.fk_Registro_Tipo = nClase
            registroFileArchiving.fk_Reproceso = Nothing
            If nMotivoReproceso <> DesktopConfig.MotivoReproceso.N_A Then registroFileArchiving.fk_Reproceso_Motivo = nMotivoReproceso
            registroFileArchiving.Impreso = False
            registroFileArchiving.Es_Cargado = nCargado
            registroFileArchiving.Imagen_Cargada = False
            If Not IsNothing(nLineaProceso) Then registroFileArchiving.fk_Linea_Proceso = nLineaProceso

            If dtFileRisk.Count > 0 Then
                registroFileArchiving.fk_Folder = dtFileRisk(0).fk_Folder
                registroFileArchiving.id_File = dtFileRisk(0).id_File
                registroFileArchiving.fk_expediente = dtFileRisk(0).fk_expediente
                dmArchiving.SchemaRisk.TBL_File.DBUpdate(registroFileArchiving, dtFileRisk(0).fk_OT, nFolder, nFile, nExpediente)
            Else
                registroFileArchiving.fk_Folder = nFolder
                registroFileArchiving.id_File = nFile
                registroFileArchiving.fk_expediente = nExpediente
                dmArchiving.SchemaRisk.TBL_File.DBInsert(registroFileArchiving)
            End If

            'Si existe el file estado en core, entonces se actualiza de lo contrario se crea.
            Dim fileEstado = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_Folderfk_FileModulo(nExpediente, nFolder, nFile, DesktopConfig.Modulo.Archiving)
            If fileEstado.Count > 0 Then
                Dim registroEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                registroEstadoFile.fk_Estado = nEstado
                registroEstadoFile.fk_Expediente = nExpediente
                registroEstadoFile.fk_Folder = nFolder
                registroEstadoFile.Modulo = DesktopConfig.Modulo.Archiving
                registroEstadoFile.fk_File = nFile
                registroEstadoFile.fk_Usuario = nUsuario
                registroEstadoFile.Fecha_Log = SlygNullable.SysDate
                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(registroEstadoFile, nExpediente, nFolder, nFile, DesktopConfig.Modulo.Archiving)
            Else
                Dim registroEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                registroEstadoFile.fk_Estado = nEstado
                registroEstadoFile.fk_Expediente = nExpediente
                registroEstadoFile.fk_Folder = nFolder
                registroEstadoFile.Modulo = DesktopConfig.Modulo.Archiving
                registroEstadoFile.fk_File = nFile
                registroEstadoFile.fk_Usuario = nUsuario
                registroEstadoFile.Fecha_Log = SlygNullable.SysDate
                dbmCore.SchemaProcess.TBL_File_Estado.DBInsert(registroEstadoFile)
            End If
        End Sub

        Public Shared Sub FolderSobranteReproceso(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef nRiskGlobal As RiskGlobal, ByRef nSesion As Security.Library.Session.Sesion, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal ot As Integer)
            Dim registro As New DBArchiving.SchemaRisk.TBL_FolderType

            Dim tableExpediente = dmArchiving.SchemaCore.CTA_Expediente.DBFindByid_Expedientefk_Entidadfk_Proyecto(nExpediente, nRiskGlobal.Entidad, nRiskGlobal.Proyecto)
            Dim tableEsquema As DBArchiving.Schemadbo.CTA_EsquemaDataTable = Nothing

            For Each row As DBArchiving.SchemaCore.CTA_ExpedienteRow In tableExpediente
                tableEsquema = dmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyectofk_esquema(row.fk_Entidad, row.fk_Proyecto, row.fk_Esquema)
            Next

            If CBool(tableEsquema.Rows(0)(tableEsquema.Acepta_Sobrantes_FolderColumn)) = True Then
                registro.Es_sobrante = True
            Else
                registro.Es_sobrante = False
                registro.fk_Reproceso_Motivo = DesktopConfig.MotivoReproceso.FoldersSobrantes
                registro.fk_Estado = DBCore.EstadoEnum.Reproceso
            End If

            dmArchiving.SchemaRisk.TBL_Folder.DBUpdate(registro, nExpediente, nFolder, ot)
        End Sub

        Public Shared Sub InsertaFolderArchiving(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal ot As Integer, ByVal sPrecinto As SlygNullable(Of String), Optional ByVal nEstado As DBCore.EstadoEnum = DBCore.EstadoEnum.Cargado)
            Dim dtFolderArchiving = dmArchiving.SchemaRisk.TBL_Folder.DBFindByfk_expedienteid_Folderfk_OT(nExpediente, nFolder, OT)
            Dim dtFolderEstadosCore = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(nExpediente, nFolder, DesktopConfig.Modulo.Archiving)

            If Not (dtFolderArchiving.Count <> 0 And dtFolderEstadosCore.Count <> 0) Then
                Dim registro As New DBArchiving.SchemaRisk.TBL_FolderType
                registro.fk_Estado = nEstado
                registro.fk_expediente = nExpediente
                registro.fk_OT = OT
                registro.fk_Precinto = sPrecinto
                registro.id_Folder = nFolder
                registro.Impreso = False

                dmArchiving.SchemaRisk.TBL_Folder.DBInsert(registro)
            Else
                Throw New Exception("Las llaves que se estan asociando, ya tienen un folder creado.")
            End If
        End Sub

        'Public Shared Sub ActualizaFileArchiving(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal OT As Integer, ByVal Folder As Integer, ByVal Expediente As Long, ByVal File As Short, ByVal RegistroTipo As DesktopConfig.RegistroTipo, ByVal cajaProceso As String)
        '    Try
        '        Dim registro As New SchemaRisk.TBL_FileType
        '        registro.fk_Estado = DBCore.EstadoEnum.Destapado
        '        registro.fk_Registro_Tipo = RegistroTipo
        '        registro.fk_Caja_Proceso = cajaProceso
        '        dmArchiving.SchemaRisk.TBL_File.DBUpdate(registro, OT, Folder, File, Expediente)
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Sub

        Public Shared Sub ActualizaEstadoDevolucionCore(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal dmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal nTipo As Short, ByVal ot As Integer, ByVal nUsuario As Integer)
            'Tipo: 0=Folder, 1=File
            Select Case nTipo
                Case 0 'Folder
                    Dim dtFolder = dmArchiving.Schemadbo.CTA_Folder.DBFindByCBarras_Folder(nCBarras)

                    Dim tFolder As New DBArchiving.SchemaRisk.TBL_FolderType
                    tFolder.fk_Estado = DBCore.EstadoEnum.Cargado
                    dmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolder, dtFolder.Rows(0).Item("fk_Expediente"), dtFolder.Rows(0).Item("id_Folder"), dtFolder.Rows(0).Item("fk_OT"))

                    For Each file In dmArchiving.Schemadbo.CTA_File.DBFindByfk_Expedientefk_Folderfk_OT(dtFolder.Rows(0).Item("fk_Expediente"), dtFolder.Rows(0).Item("id_Folder"), dtFolder.Rows(0).Item("fk_OT"))
                        ActualizaEstadoDevolucionCore(dmArchiving, dmCore, file.CBarras_File, 1, ot, nUsuario)
                    Next

                Case 1 'File
                    Dim dtFile = dmArchiving.Schemadbo.CTA_File.DBFindByCBarras_Filefk_OT(nCBarras, ot)

                    Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
                    tEstadoFile.fk_Expediente = CLng(dtFile.Rows(0).Item("fk_Expediente"))
                    tEstadoFile.fk_Folder = CShort(dtFile.Rows(0).Item("fk_Folder"))
                    tEstadoFile.fk_File = CShort(dtFile.Rows(0).Item("id_File"))
                    tEstadoFile.Modulo = DesktopConfig.Modulo.Archiving
                    tEstadoFile.fk_Estado = DBCore.EstadoEnum.Cargado
                    tEstadoFile.fk_Usuario = nUsuario
                    tEstadoFile.Fecha_Log = SlygNullable.SysDate
                    dmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, tEstadoFile.fk_Expediente, tEstadoFile.fk_Folder, tEstadoFile.fk_File, tEstadoFile.Modulo)
            End Select
        End Sub

        Public Shared Sub ActualizaEstadoFile(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso, ByVal nReproceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nNuevaOt As Slyg.Tools.SlygNullable(Of Integer), ByVal nLineaProceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nRegistroTipo As String, ByVal nCBarras As String, ByVal nUsa_Tabla_Fisico As Boolean, ByVal nReutilizafaltante As Boolean)
            'Se actualiza en Core
            Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
            tEstadoFile.fk_Estado = nEstado
            tEstadoFile.fk_Usuario = nUsuario
            tEstadoFile.Fecha_Log = SlygNullable.SysDate
            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, nFile, nModulo)

            'Se actualiza en Archiving
            Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
            tFileArchiving.fk_OT = nNuevaOt
            tFileArchiving.fk_Estado = nEstado
            tFileArchiving.fk_Caja_Proceso = nCajaProceso
            If nMotivoReproceso <> DesktopConfig.MotivoReproceso.N_A Then tFileArchiving.fk_Reproceso_Motivo = nMotivoReproceso
            If Not IsNothing(nReproceso) Then tFileArchiving.fk_Reproceso = nReproceso
            If Not IsNothing(nLineaProceso) Then tFileArchiving.fk_Linea_Proceso = nLineaProceso
            If Not String.IsNullOrEmpty(nRegistroTipo) Then
                Dim RT As DesktopConfig.RegistroTipo

                Select Case nRegistroTipo
                    Case "Adicion"
                        RT = DesktopConfig.RegistroTipo.Adicion
                    Case "Adición"
                        RT = DesktopConfig.RegistroTipo.Adicion
                    Case "Nuevo"
                        RT = DesktopConfig.RegistroTipo.Nuevo
                    Case "Devolucion"
                        RT = DesktopConfig.RegistroTipo.Devolucion
                    Case "Devolución"
                        RT = DesktopConfig.RegistroTipo.Devolucion
                    Case "Reingreso"
                        RT = DesktopConfig.RegistroTipo.Reingreso
                    Case Else
                        RT = Nothing
                End Select

                tFileArchiving.fk_Registro_Tipo = RT
            End If

            dmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, ot, nFolder, nFile, nExpediente)
            If nUsa_Tabla_Fisico Then
                Inserta_File_Fisico(dmArchiving, nExpediente, nFolder, nFile, ot, nPrecinto, nNuevaOt, nModulo, nEstado, nUsuario, tFileArchiving.fk_Registro_Tipo, nCBarras, nReutilizafaltante)
            End If

        End Sub

        'Public Shared Sub ActualizaEstadoFileCarpeta(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso, ByVal nReproceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nNuevaOt As Slyg.Tools.SlygNullable(Of Integer), ByVal nLineaProceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nRegistroTipo As String, ByVal nCBarras As String, ByVal nUsa_Tabla_Fisico As Boolean, ByVal nReutilizafaltante As Boolean)
        '    'Se actualiza en Core
        '    Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
        '    tEstadoFile.fk_Estado = nEstado
        '    tEstadoFile.fk_Usuario = nUsuario
        '    tEstadoFile.Fecha_Log = SlygNullable.SysDate
        '    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, 30, nModulo)
        '    dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, 40, nModulo)

        '    'Se actualiza en Archiving
        '    Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
        '    tFileArchiving.fk_OT = nNuevaOt
        '    tFileArchiving.fk_Estado = nEstado
        '    tFileArchiving.fk_Caja_Proceso = nCajaProceso
        '    If nMotivoReproceso <> DesktopConfig.MotivoReproceso.N_A Then tFileArchiving.fk_Reproceso_Motivo = nMotivoReproceso
        '    If Not IsNothing(nReproceso) Then tFileArchiving.fk_Reproceso = nReproceso
        '    If Not IsNothing(nLineaProceso) Then tFileArchiving.fk_Linea_Proceso = nLineaProceso
        '    If Not String.IsNullOrEmpty(nRegistroTipo) Then
        '        Dim RT As DesktopConfig.RegistroTipo

        '        Select Case nRegistroTipo
        '            Case "Adicion"
        '                RT = DesktopConfig.RegistroTipo.Adicion
        '            Case "Adición"
        '                RT = DesktopConfig.RegistroTipo.Adicion
        '            Case "Nuevo"
        '                RT = DesktopConfig.RegistroTipo.Nuevo
        '            Case "Devolucion"
        '                RT = DesktopConfig.RegistroTipo.Devolucion
        '            Case "Devolución"
        '                RT = DesktopConfig.RegistroTipo.Devolucion
        '            Case "Reingreso"
        '                RT = DesktopConfig.RegistroTipo.Reingreso
        '            Case Else
        '                RT = Nothing
        '        End Select

        '        tFileArchiving.fk_Registro_Tipo = RT
        '    End If

        '    dmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, ot, nFolder, 30, nExpediente)
        '    dmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, ot, nFolder, 40, nExpediente)

        '    If nUsa_Tabla_Fisico Then
        '        Inserta_File_Fisico(dmArchiving, nExpediente, nFolder, nFile, ot, nPrecinto, nNuevaOt, nModulo, nEstado, nUsuario, tFileArchiving.fk_Registro_Tipo, nCBarras, nReutilizafaltante)
        '    End If

        'End Sub

        Public Shared Sub ActualizaEstadoFileDevolucion(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso, ByVal nReproceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nNuevaOt As Slyg.Tools.SlygNullable(Of Integer), ByVal nLineaProceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nRegistroTipo As String, ByVal nCBarras As String, ByVal nUsa_Tabla_Fisico As Boolean, ByVal nSobrante As Boolean, ByVal nReutilizafaltante As Boolean)
            'Se actualiza en Core
            Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
            tEstadoFile.fk_Estado = nEstado
            tEstadoFile.fk_Usuario = nUsuario
            tEstadoFile.Fecha_Log = SlygNullable.SysDate
            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, nExpediente, nFolder, nFile, nModulo)

            'Se actualiza en Archiving
            Dim tFileArchiving As New DBArchiving.SchemaRisk.TBL_FileType
            tFileArchiving.fk_OT = nNuevaOt
            tFileArchiving.fk_Estado = nEstado
            tFileArchiving.fk_Caja_Proceso = nCajaProceso
            tFileArchiving.Es_sobrante = nSobrante
            If nMotivoReproceso <> DesktopConfig.MotivoReproceso.N_A Then tFileArchiving.fk_Reproceso_Motivo = nMotivoReproceso
            If Not IsNothing(nReproceso) Then tFileArchiving.fk_Reproceso = nReproceso
            If Not IsNothing(nLineaProceso) Then tFileArchiving.fk_Linea_Proceso = nLineaProceso
            If Not String.IsNullOrEmpty(nRegistroTipo) Then
                Dim RT As DesktopConfig.RegistroTipo

                Select Case nRegistroTipo
                    Case "Adicion"
                        RT = DesktopConfig.RegistroTipo.Adicion
                    Case "Adición"
                        RT = DesktopConfig.RegistroTipo.Adicion
                    Case "Nuevo"
                        RT = DesktopConfig.RegistroTipo.Nuevo
                    Case "Devolucion"
                        RT = DesktopConfig.RegistroTipo.Devolucion
                    Case "Devolución"
                        RT = DesktopConfig.RegistroTipo.Devolucion
                    Case "Reingreso"
                        RT = DesktopConfig.RegistroTipo.Reingreso
                    Case Else
                        RT = Nothing
                End Select

                tFileArchiving.fk_Registro_Tipo = RT
            End If

            dmArchiving.SchemaRisk.TBL_File.DBUpdate(tFileArchiving, ot, nFolder, nFile, nExpediente)
            If nUsa_Tabla_Fisico Then
                Inserta_File_Fisico(dmArchiving, nExpediente, nFolder, nFile, ot, nPrecinto, nNuevaOt, nModulo, nEstado, nUsuario, tFileArchiving.fk_Registro_Tipo, nCBarras, nReutilizafaltante)
            End If
        End Sub

        Private Shared Sub Inserta_File_Fisico(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nNuevaOt As Slyg.Tools.SlygNullable(Of Integer), ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nRegistroTipo As Integer, ByVal nCBarras As String, Optional ByVal nReutilizafaltante As Boolean = False)
            If nEstado = DBCore.EstadoEnum.Destapado Then
                Dim _ot As Slyg.Tools.SlygNullable(Of Integer)
                _ot = ot
                If Not IsNothing(nNuevaOt) Then _ot = nNuevaOt
                dmArchiving.SchemaRisk.PA_Inserta_File_Fisico.DBExecute(nExpediente, nFolder, nFile, _ot, nPrecinto, nRegistroTipo, nCBarras, 0, nReutilizafaltante)

            End If
        End Sub

        'Public Shared Sub ActualizaEstadoFileCarpeta(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), Optional ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso = DesktopConfig.MotivoReproceso.N_A, Optional ByVal nReproceso As Slyg.Tools.SlygNullable(Of Integer) = Nothing, Optional ByRef nLineaProceso As Slyg.Tools.SlygNullable(Of Integer) = Nothing, Optional ByVal nRegistroTipo As String = "", Optional ByVal nUsa_Tabla_Fisico As Boolean = False, Optional nReutilizafaltante As Boolean = False)
        '    Dim tFileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(nCBarras)
        '    ActualizaEstadoFileCarpeta(dmArchiving, dbmCore, tFileCore(0).fk_Expediente, tFileCore(0).fk_Folder, tFileCore(0).id_File, ot, nPrecinto, nModulo, nEstado, nUsuario, nCajaProceso, nMotivoReproceso, nReproceso, ot, nLineaProceso, nRegistroTipo, nCBarras, nUsa_Tabla_Fisico, nReutilizafaltante)
        'End Sub

        Public Shared Sub ActualizaEstadoFile(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), Optional ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso = DesktopConfig.MotivoReproceso.N_A, Optional ByVal nReproceso As Slyg.Tools.SlygNullable(Of Integer) = Nothing, Optional ByRef nLineaProceso As Slyg.Tools.SlygNullable(Of Integer) = Nothing, Optional ByVal nRegistroTipo As String = "", Optional ByVal nUsa_Tabla_Fisico As Boolean = False, Optional nReutilizafaltante As Boolean = False)
            Dim tFileCore As DBCore.SchemaProcess.TBL_FileDataTable

            tFileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(nCBarras)

            ActualizaEstadoFile(dmArchiving, dbmCore, tFileCore(0).fk_Expediente, tFileCore(0).fk_Folder, tFileCore(0).id_File, ot, nPrecinto, nModulo, nEstado, nUsuario, nCajaProceso, nMotivoReproceso, nReproceso, ot, nLineaProceso, nRegistroTipo, nCBarras, nUsa_Tabla_Fisico, nReutilizafaltante)
        End Sub

        Public Shared Sub ActualizaEstadoFile(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nMotivoReproceso As DesktopConfig.MotivoReproceso, ByVal nReproceso As Slyg.Tools.SlygNullable(Of Integer), ByRef nLineaProceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nRegistroTipo As String, ByVal nNuevaOt As Integer, ByVal nUsa_Tabla_Fisico As Boolean, nReutilizafaltante As Boolean)
            Dim tFileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(nCBarras)
            ActualizaEstadoFile(dmArchiving, dbmCore, tFileCore(0).fk_Expediente, tFileCore(0).fk_Folder, tFileCore(0).id_File, ot, nPrecinto, nModulo, nEstado, nUsuario, nCajaProceso, nMotivoReproceso, nReproceso, nNuevaOt, nLineaProceso, nRegistroTipo, nCBarras, nUsa_Tabla_Fisico, nReutilizafaltante)
        End Sub

        Public Shared Sub ActualizaEstadoFile(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nNuevaOt As Slyg.Tools.SlygNullable(Of Integer), ByVal nLineaProceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nRegistroTipo As String, ByVal nUsa_Tabla_Fisico As Boolean, nReutilizafaltante As Boolean)
            Dim tFileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(nCBarras)
            ActualizaEstadoFile(dmArchiving, dbmCore, tFileCore(0).fk_Expediente, tFileCore(0).fk_Folder, tFileCore(0).id_File, ot, nPrecinto, nModulo, nEstado, nUsuario, nCajaProceso, DesktopConfig.MotivoReproceso.N_A, Nothing, nNuevaOt, nLineaProceso, nRegistroTipo, nCBarras, nUsa_Tabla_Fisico, nReutilizafaltante)
        End Sub

        Public Shared Sub ActualizaEstadoFileDevolucion(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal ot As Slyg.Tools.SlygNullable(Of Integer), ByVal nPrecinto As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer, ByVal nCajaProceso As Slyg.Tools.SlygNullable(Of String), ByVal nNuevaOt As Slyg.Tools.SlygNullable(Of Integer), ByVal nLineaProceso As Slyg.Tools.SlygNullable(Of Integer), ByVal nRegistroTipo As String, ByVal nUsa_Tabla_Fisico As Boolean, ByVal nSobrante As Boolean, nReutilizafaltante As Boolean)
            Dim tFileCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(nCBarras)
            ActualizaEstadoFileDevolucion(dmArchiving, dbmCore, tFileCore(0).fk_Expediente, tFileCore(0).fk_Folder, tFileCore(0).id_File, ot, nPrecinto, nModulo, nEstado, nUsuario, nCajaProceso, DesktopConfig.MotivoReproceso.N_A, Nothing, nNuevaOt, nLineaProceso, nRegistroTipo, nCBarras, nUsa_Tabla_Fisico, nSobrante, nReutilizafaltante)
        End Sub

        Public Shared Sub ActualizaEstadoFileImaging(ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarras As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As Integer, ByVal nUsuario As Integer)
            'Se actualiza en Core
            Dim tFileEstadoCore = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(nCBarras)


            Dim tEstadoFile As New DBCore.SchemaProcess.TBL_File_EstadoType
            Dim tFile As New DBImaging.SchemaProcess.TBL_FileType

            tEstadoFile.fk_Estado = nEstado
            tEstadoFile.fk_Usuario = nUsuario
            tEstadoFile.Fecha_Log = SlygNullable.SysDate
            tFile.Reprocesado = 1
            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(tEstadoFile, tFileEstadoCore(0).fk_Expediente, tFileEstadoCore(0).fk_Folder, tFileEstadoCore(0).id_File, nModulo)
            dbmImaging.SchemaProcess.TBL_File.DBUpdate(tFile, tFileEstadoCore(0).fk_Expediente, tFileEstadoCore(0).fk_Folder, tFileEstadoCore(0).id_File, 1)
        End Sub


        Public Shared Sub ActualizaEstadoFolder(ByRef dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByRef dmCore As DBCore.DBCoreDataBaseManager, ByVal nCBarrasFolder As String, ByVal nModulo As DesktopConfig.Modulo, ByVal nEstado As DBCore.EstadoEnum, ByVal nUsuario As Integer, ByVal ot As SlygNullable(Of Integer))
            Dim tFolderCore = dmArchiving.Schemadbo.CTA_Folder_Detalle.DBFindByCBarras_FolderModulo(nCBarrasFolder, nModulo)

            'Se actualiza en Core, si el nuevo estado es superior al estado actual.
            If nEstado > CShort(tFolderCore(0).fk_Estado) Then
                Dim tEstadoFolder As New DBCore.SchemaProcess.TBL_Folder_estadoType
                tEstadoFolder.fk_Expediente = tFolderCore(0).fk_Expediente
                tEstadoFolder.fk_Folder = tFolderCore(0).id_Folder
                tEstadoFolder.Modulo = tFolderCore(0).Modulo
                tEstadoFolder.fk_Estado = tFolderCore(0).fk_Estado
                tEstadoFolder.fk_Usuario = nUsuario
                tEstadoFolder.Fecha_Log = SlygNullable.SysDate
                tEstadoFolder.fk_Estado = nEstado
                dmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(tEstadoFolder, tEstadoFolder.fk_Expediente, tEstadoFolder.fk_Folder, tEstadoFolder.Modulo)
            End If

            'Se actualiza en Archiving.
            Dim tFolderArchiving As New DBArchiving.SchemaRisk.TBL_FolderType
            tFolderArchiving.fk_expediente = tFolderCore(0).fk_Expediente
            tFolderArchiving.id_Folder = tFolderCore(0).id_Folder
            tFolderArchiving.fk_Estado = nEstado
            dmArchiving.SchemaRisk.TBL_Folder.DBUpdate(tFolderArchiving, tFolderArchiving.fk_expediente, tFolderArchiving.id_Folder, ot)
        End Sub
#End Region

#Region "Utilidades Controles"

        Public Shared Sub LlenarCombo(ByVal nCombo As ComboBox, ByVal nTable As DataTable, ByVal nValue As DataColumn, ByVal nText As DataColumn)
            LlenarCombo(nCombo, nTable, nValue.ColumnName, nText.ColumnName)
        End Sub

        Public Shared Sub LlenarCombo(ByVal nCombo As ComboBox, ByVal nTable As DataTable, ByVal nValue As String, ByVal nText As String)
            nCombo.ValueMember = nValue
            nCombo.DisplayMember = nText

            If nValue <> nText Then nCombo.DataSource = ClonarDataTable(nTable).DefaultView.ToTable(True, nValue, nText)
            If nValue = nText Then nCombo.DataSource = ClonarDataTable(nTable).DefaultView.ToTable(True, nValue)
        End Sub

        Public Shared Function FindControl(ByVal nContainer As Control, ByVal nControl As String) As Control
            Return nContainer.Controls.Find(nControl, True)(0)
        End Function

        Public Shared Function ImprimirCBarras(ByRef objCBarras As Controls.BarCode.BarCodeControl, ByVal nCBarras As String, ByVal nTitulo As String, ByVal nParametros As List(Of DesktopConfig.AtributesCBarras)) As Controls.BarCode.BarCodeControl

            'Propiedades del control

            If nParametros.Count = 0 Then
                If nTitulo.Length < 25 Then
                    objCBarras.BarCodeHeight = 30
                    nTitulo &= vbNewLine & ""
                Else
                    objCBarras.BarCodeHeight = 20
                End If
            Else
                objCBarras.BarCodeHeight = 20
            End If

            objCBarras.BarCode = nCBarras
            objCBarras.HeaderText = nTitulo
            objCBarras.BarCodeType = Controls.BarCode.BarCodeTypeType.EAN128
            objCBarras.Align = Controls.BarCode.AlignType.Left
            objCBarras.ShowFooter = True
            objCBarras.Width = 300
            objCBarras.Height = 150
            objCBarras.Weight = Controls.BarCode.BarCodeWeight.Small
            objCBarras.ShowHeader = True

            'Modifica el titulo para que quepa en label y lo envie a 2 lineas si es necesario

            Dim tituloFinal As String
            If nTitulo.Length > 50 Then nTitulo = nTitulo.Substring(0, 50)
            tituloFinal = nTitulo
            If nTitulo.Length > 25 Then tituloFinal = nTitulo.Substring(0, 25) & vbNewLine & nTitulo.Substring(25, nTitulo.Length - 25)
            If tituloFinal.Length <= 25 And nParametros.Count = 0 Then tituloFinal &= vbNewLine & " "

            objCBarras.HeaderText = tituloFinal
            objCBarras.HeaderFont = New Drawing.Font(Drawing.FontFamily.GenericSansSerif, 8, Drawing.FontStyle.Bold)
            objCBarras.FooterFont = New Drawing.Font(Drawing.FontFamily.GenericSansSerif, 7)
            objCBarras.BarCodePrintDocument.DocumentName = nCBarras
            objCBarras.AutoSizeMode = AutoSizeMode.GrowOnly

            'Parametros

            objCBarras.FooterLines.Clear()
            objCBarras.FooterColumns = 2

            'Agrega el texto del Footer

            For Each atributo As DesktopConfig.AtributesCBarras In nParametros
                objCBarras.FooterLines.Add(New Controls.BarCode.FooterLineItem(atributo.Label & " : " & atributo.Valor, 1))
            Next

            If nParametros.Count = 0 Then
                objCBarras.FooterLines.Add(New Controls.BarCode.FooterLineItem(" ", 1))
            End If
            objCBarras.Update()
            objCBarras.Print()

            Return objCBarras

        End Function

        Public Shared Function CmToFoot(ByVal nCm As Double) As Double
            Return nCm / (12 / 2.5) / 100
        End Function

        Public Shared Function ConvertName(ByVal nCadena As String) As String
            Return nCadena.Replace(" ", "_")
        End Function

        Public Shared Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Drawing.Image
            Dim ms As MemoryStream = New MemoryStream(byteArrayIn)
            Dim returnImage As Drawing.Image
            Try
                returnImage = Drawing.Image.FromStream(ms)
            Catch ex As Exception
                Throw New Exception()
            End Try
            Return returnImage
        End Function

        Public Shared Sub DataGridFillCombo(ByVal nGrilla As DataGridView, ByVal nCombo As String, ByVal nTable As DataTable, ByVal value As String, ByVal text As String)
            Dim column As DataGridViewComboBoxColumn = DirectCast(nGrilla.Columns(nCombo), DataGridViewComboBoxColumn)
            With column
                .DataSource = nTable
                .DisplayMember = text
                .ValueMember = value
            End With
        End Sub

        Public Shared Function CreaControles(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, ByVal nCampos As List(Of DesktopConfig.CrearControlesType), ByRef nConnectionStrings As DesktopConfig.TypeConnectionString, ByRef nRiskGlobal As RiskGlobal, ByVal nShowData As Boolean, ByVal nAncho As Integer, Optional ByRef DisableSpaceBar As Boolean = False, Optional EnabledD1 As Boolean = False) As TableLayoutPanel
            Dim tableLayoutPanelControles As New TableLayoutPanel

            Dim alto As Integer = 0
            Dim ancho As Integer = 0

            tableLayoutPanelControles.ColumnCount = 2
            tableLayoutPanelControles.RowCount = nCampos.Count - 1

            Dim i As Integer = 0
            For Each campo As DesktopConfig.CrearControlesType In nCampos
                Dim nombreCampo As String = ConvertName(campo.NombreCampo)
                Dim controlCampo As New Control

                Dim llaveLabel As New Label
                'Campos de Texto (Numerico, Fecha, Texto)
                If campo.Tipo = DesktopConfig.CampoTipo.Numerico Or _
                   campo.Tipo = DesktopConfig.CampoTipo.Fecha Or _
                   campo.Tipo = DesktopConfig.CampoTipo.Texto Then

                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveTextBox As New DesktopTextBoxControl
                    llaveTextBox.Name = nombreCampo
                    llaveTextBox.MaximumLength = campo.MaxLength
                    llaveTextBox.MinimumLength = campo.MinLength
                    llaveTextBox.fk_Documento = campo.fk_Documento
                    llaveTextBox.fk_Campo = campo.fk_Campo
                    llaveTextBox.fk_Validacion = campo.fk_Validacion

                    Select Case campo.Tipo
                        Case DesktopConfig.CampoTipo.Numerico
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico

                            llaveTextBox.Usa_Decimales = campo.Usa_Decimales
                            llaveTextBox.Caracter_Decimal = campo.Caracter_Decimal
                            llaveTextBox.Cantidad_Decimales = campo.Cantidad_Decimales
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And nShowData And Not IsDBNull(campo.File_Data) Then
                                llaveTextBox.Text = CInt(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                        Case DesktopConfig.CampoTipo.Fecha
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                            llaveTextBox.MaximumLength = 10
                            llaveTextBox.MinimumLength = 10
                            llaveTextBox.Obligatorio = campo.Obligatorio
                            If campo.Existe_File_Data And nShowData Then
                                llaveTextBox.Text = CDate(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                        Case DesktopConfig.CampoTipo.Texto
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And nShowData Then
                                llaveTextBox.Text = CStr(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                    End Select

                    llaveTextBox.Width = campo.Width
                    controlCampo = llaveTextBox
                    alto += 35

                    'Campos Booleanos
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                    Dim llaveCheckBox As New Controls.DesktopCheckBox.DesktopCheckBoxControl
                    llaveCheckBox.Name = ConvertName(nombreCampo)
                    llaveCheckBox.Text = campo.Label
                    llaveCheckBox.DisableSpaceBar = DisableSpaceBar
                    llaveCheckBox.EnabledD1 = EnabledD1
                    llaveCheckBox.fk_Documento = campo.fk_Documento
                    llaveCheckBox.fk_Campo = campo.fk_Campo
                    llaveCheckBox.fk_Validacion = campo.fk_Validacion

                    controlCampo = llaveCheckBox
                    alto += 40

                    'Campos de Lista
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.Lista Then
                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveComboBox As New DesktopComboBoxControl
                    llaveComboBox.Name = ConvertName(nombreCampo)
                    Dim tableDrop = dmArchiving.SchemaCore.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(nRiskGlobal.Entidad, campo.CampoLista)
                    llaveComboBox.ValueMember = tableDrop.Valor_Campo_Lista_ItemColumn.ColumnName
                    llaveComboBox.DisplayMember = tableDrop.Etiqueta_Campo_Lista_ItemColumn.ColumnName
                    llaveComboBox.DataSource = tableDrop
                    llaveComboBox.fk_Documento = campo.fk_Documento
                    llaveComboBox.fk_Campo = campo.fk_Campo
                    llaveComboBox.fk_Validacion = campo.fk_Validacion
                    controlCampo = llaveComboBox
                    alto += 35

                    'Campo tipo query
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.Query Then
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveTextBox As New DesktopTextBoxControl
                    llaveTextBox.Name = nombreCampo
                    llaveTextBox.Text = "Automatico"
                    llaveTextBox.Enabled = False
                    llaveTextBox.MaxLength = campo.MaxLength
                    controlCampo = llaveTextBox

                    'Campos de Data asociada
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.TablaAsociada Then
                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim lLaveDataGridView As New DesktopDataGridViewControl
                    lLaveDataGridView.Name = ConvertName(nombreCampo)

                    'Propiedades grilla
                    lLaveDataGridView.Width = 500
                    lLaveDataGridView.ScrollBars = ScrollBars.Both

                    Dim tableAsociada = dmArchiving.SchemaCore.CTA_Tabla_Asociada.DBFindByfk_Entidadfk_Documentofk_Campo(campo.fk_Entidad, campo.fk_Documento, campo.fk_Campo)
                    For Each row In tableAsociada
                        Select Case row.fk_Campo_Tipo
                            Case DesktopConfig.CampoTipo.Texto
                                Dim columna As New DataGridViewTextBoxColumn()
                                columna.Name = row.Nombre_Campo
                                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                lLaveDataGridView.Columns.Add(columna)

                            Case DesktopConfig.CampoTipo.Numerico
                                Dim columna As New DataGridViewTextBoxColumn()
                                columna.Name = row.Nombre_Campo
                                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                lLaveDataGridView.Columns.Add(columna)

                            Case DesktopConfig.CampoTipo.Fecha
                                Dim columna As New DataGridViewTextBoxColumn()
                                columna.Name = row.Nombre_Campo
                                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                lLaveDataGridView.Columns.Add(columna)

                            Case DesktopConfig.CampoTipo.Lista
                                Dim columna As New DataGridViewComboBoxColumn()
                                columna.Name = row.Nombre_Campo
                                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                                Dim dtListaItem = dmArchiving.SchemaCore.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(row.fk_Entidad, row.fk_Campo_Lista)
                                columna.ValueMember = CStr(dtListaItem.id_Campo_Lista_ItemColumn.ColumnName)
                                columna.DisplayMember = CStr(dtListaItem.Etiqueta_Campo_Lista_ItemColumn.ColumnName)
                                columna.DataSource = dtListaItem
                                lLaveDataGridView.Columns.Add(columna)

                            Case DesktopConfig.CampoTipo.SiNo
                                Dim columna As New DataGridViewCheckBoxColumn()
                                columna.Name = row.Nombre_Campo
                                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                lLaveDataGridView.Columns.Add(columna)
                        End Select
                    Next
                    controlCampo = lLaveDataGridView

                    ancho += lLaveDataGridView.Width
                    alto += controlCampo.Height
                End If

                If campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                    controlCampo.Width = 650
                    tableLayoutPanelControles.Controls.Add(controlCampo, 0, i)
                Else
                    tableLayoutPanelControles.Controls.Add(llaveLabel, 0, i)
                    tableLayoutPanelControles.Controls.Add(controlCampo, 1, i)
                End If

                If ancho < campo.LabelWidth + campo.Width Then
                    ancho = campo.LabelWidth + campo.Width + 30
                End If

                i += 1
            Next

            tableLayoutPanelControles.Width = nAncho - 30
            tableLayoutPanelControles.Height = alto

            Return tableLayoutPanelControles
        End Function

        Public Shared Function CreaControlesImaging(ByVal dmImaging As DBImaging.DBImagingDataBaseManager, ByVal campos As List(Of DesktopConfig.CrearControlesType), ByRef connectionStrings As DesktopConfig.TypeConnectionString, ByRef imagingGlobal As ImagingGlobal, ByVal bShowData As Boolean, ByVal nAncho As Integer, Optional ByVal nAnchorLeftRight As Boolean = False) As Panel
            Dim panelControles As New Panel
            panelControles.Width = nAncho
            Dim currentPos = New Drawing.Point(4, -25)

            For k = 0 To campos.Count - 1
                Dim campo = campos(k)
                'For Each campo As DesktopConfig.CrearControlesType In campos
                Dim nombreCampo As String = ConvertName(campo.NombreCampo)
                Dim controlCampo As Control = Nothing

                Dim llaveLabel As New Label

                'Campos de Texto (Numerico, Fecha, Texto)
                If campo.Tipo = DesktopConfig.CampoTipo.Numerico Or _
                   campo.Tipo = DesktopConfig.CampoTipo.Fecha Or _
                   campo.Tipo = DesktopConfig.CampoTipo.Texto Then

                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveTextBox As New DesktopTextBoxControl
                    llaveTextBox.Name = nombreCampo

                    llaveTextBox.MaximumLength = campo.MaxLength
                    llaveTextBox.MinimumLength = campo.MinLength

                    Select Case campo.Tipo
                        Case DesktopConfig.CampoTipo.Numerico
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico

                            llaveTextBox.Usa_Decimales = campo.Usa_Decimales
                            llaveTextBox.Caracter_Decimal = campo.Caracter_Decimal
                            llaveTextBox.Cantidad_Decimales = campo.Cantidad_Decimales
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And bShowData And Not IsDBNull(campo.File_Data) Then
                                llaveTextBox.Text = CInt(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                        Case DesktopConfig.CampoTipo.Fecha
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                            llaveTextBox.MaximumLength = 10
                            llaveTextBox.MinimumLength = 10
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And bShowData Then
                                llaveTextBox.Text = CDate(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                        Case DesktopConfig.CampoTipo.Texto
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And bShowData Then
                                llaveTextBox.Text = CStr(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                    End Select

                    llaveTextBox.Width = campo.Width
                    controlCampo = llaveTextBox

                    'Campos Booleanos
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                    Dim llaveCheckBox As New Controls.DesktopCheckBox.DesktopCheckBoxControl
                    llaveCheckBox.Name = ConvertName(nombreCampo)
                    llaveCheckBox.Text = campo.Label
                    llaveCheckBox.Width = campo.Width
                    controlCampo = llaveCheckBox

                    'Campos de Lista
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.Lista Then
                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveComboBox As New DesktopComboBoxControl
                    llaveComboBox.Name = ConvertName(nombreCampo)
                    Dim tableDrop = dmImaging.SchemaConfig.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(imagingGlobal.Entidad, campo.CampoLista)
                    llaveComboBox.ValueMember = tableDrop.Valor_Campo_Lista_ItemColumn.ColumnName
                    llaveComboBox.DisplayMember = tableDrop.Etiqueta_Campo_Lista_ItemColumn.ColumnName
                    llaveComboBox.DataSource = tableDrop
                    llaveComboBox.Width = campo.Width
                    controlCampo = llaveComboBox

                    'Campo tipo query
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.Query Then
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveTextBox As New DesktopTextBoxControl
                    llaveTextBox.Name = nombreCampo
                    llaveTextBox.Text = "Automatico"
                    llaveTextBox.Enabled = False
                    llaveTextBox.MaxLength = campo.MaxLength
                    llaveTextBox.Width = campo.Width
                    controlCampo = llaveTextBox
                End If

                If campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                    controlCampo.Width = campo.Width
                    currentPos.Y = currentPos.Y + 22
                    controlCampo.Location = New Drawing.Point(currentPos.X, currentPos.Y)
                    panelControles.Controls.Add(controlCampo)
                Else
                    llaveLabel.TextAlign = Drawing.ContentAlignment.BottomLeft
                    llaveLabel.Height = 25
                    llaveLabel.ForeColor = Drawing.Color.Maroon
                    currentPos.Y = currentPos.Y + 15
                    llaveLabel.Location = New Drawing.Point(currentPos.X, currentPos.Y)

                    currentPos.Y = currentPos.Y + 25
                    controlCampo.Location = New Drawing.Point(currentPos.X, currentPos.Y)
                    panelControles.Controls.Add(controlCampo)
                    panelControles.Controls.Add(llaveLabel)
                End If

                controlCampo.Tag = campo
                If (nAnchorLeftRight) Then
                    controlCampo.Anchor = ((AnchorStyles.Top Or AnchorStyles.Left) _
                                           Or AnchorStyles.Right)
                End If
            Next

            panelControles.Refresh()

            Return panelControles
        End Function

        Public Shared Function CreaControlesImagingCorreccion(ByVal dmImaging As DBImaging.DBImagingDataBaseManager, ByVal campos As List(Of DesktopConfig.CrearControlesType), ByRef connectionStrings As DesktopConfig.TypeConnectionString, ByRef imagingGlobal As ImagingGlobal, ByVal bShowData As Boolean, ByVal nAncho As Integer, Optional ByVal nAnchorLeftRight As Boolean = False) As Panel
            Dim panelControles As New Panel
            panelControles.Width = nAncho
            Dim currentPos = New Drawing.Point(4, -25)

            For k = 0 To campos.Count - 1
                Dim campo = campos(k)
                'For Each campo As DesktopConfig.CrearControlesType In campos
                Dim nombreCampo As String = ConvertName(campo.NombreCampo)
                Dim controlCampo As Control = Nothing

                Dim llaveLabel As New Label

                'Campos de Texto (Numerico, Fecha, Texto)
                If campo.Tipo = DesktopConfig.CampoTipo.Numerico Or _
                   campo.Tipo = DesktopConfig.CampoTipo.Fecha Or _
                   campo.Tipo = DesktopConfig.CampoTipo.Texto Then

                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveTextBox As New DesktopTextBoxControl
                    llaveTextBox.Name = nombreCampo

                    llaveTextBox.MaximumLength = campo.MaxLength
                    llaveTextBox.MinimumLength = campo.MinLength

                    Select Case campo.Tipo
                        Case DesktopConfig.CampoTipo.Numerico
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico

                            llaveTextBox.Usa_Decimales = campo.Usa_Decimales
                            llaveTextBox.Caracter_Decimal = campo.Caracter_Decimal
                            llaveTextBox.Cantidad_Decimales = campo.Cantidad_Decimales
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And bShowData And Not IsDBNull(campo.File_Data) Then
                                llaveTextBox.Text = CInt(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                        Case DesktopConfig.CampoTipo.Fecha
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                            llaveTextBox.MaximumLength = 10
                            llaveTextBox.MinimumLength = 10
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And bShowData Then
                                llaveTextBox.Text = CDate(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                        Case DesktopConfig.CampoTipo.Texto
                            llaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            llaveTextBox.Obligatorio = campo.Obligatorio

                            If campo.Existe_File_Data And bShowData Then
                                llaveTextBox.Text = CStr(campo.File_Data)
                                llaveTextBox.Enabled = False
                            End If

                    End Select

                    llaveTextBox.Width = campo.Width
                    controlCampo = llaveTextBox

                    'Campos Booleanos
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                    Dim llaveCheckBox As New Controls.DesktopCheckBox.DesktopCheckBoxControl
                    llaveCheckBox.Name = ConvertName(nombreCampo)
                    llaveCheckBox.Text = campo.Label
                    llaveCheckBox.Width = campo.Width
                    controlCampo = llaveCheckBox

                    'Campos de Lista
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.Lista Then
                    'Crea el Label del campo
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveComboBox As New DesktopComboBoxControl
                    llaveComboBox.Name = ConvertName(nombreCampo)
                    Dim tableDrop = dmImaging.SchemaConfig.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(imagingGlobal.Entidad, campo.CampoLista)
                    llaveComboBox.ValueMember = tableDrop.Valor_Campo_Lista_ItemColumn.ColumnName
                    llaveComboBox.DisplayMember = tableDrop.Etiqueta_Campo_Lista_ItemColumn.ColumnName
                    llaveComboBox.DataSource = tableDrop
                    llaveComboBox.Width = campo.Width
                    controlCampo = llaveComboBox

                    'Campo tipo query
                ElseIf campo.Tipo = DesktopConfig.CampoTipo.Query Then
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    Dim llaveTextBox As New DesktopTextBoxControl
                    llaveTextBox.Name = nombreCampo
                    llaveTextBox.Text = "Automatico"
                    llaveTextBox.Enabled = False
                    llaveTextBox.MaxLength = campo.MaxLength
                    llaveTextBox.Width = campo.Width
                    controlCampo = llaveTextBox
                End If

                If campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                    controlCampo.Width = campo.Width
                    currentPos.Y = currentPos.Y + 22
                    controlCampo.Location = New Drawing.Point(currentPos.X, currentPos.Y)
                    panelControles.Controls.Add(controlCampo)
                Else
                    llaveLabel.TextAlign = Drawing.ContentAlignment.BottomLeft
                    llaveLabel.Height = 25
                    llaveLabel.ForeColor = Drawing.Color.Maroon
                    currentPos.Y = currentPos.Y + 15
                    llaveLabel.Location = New Drawing.Point(currentPos.X, currentPos.Y)

                    currentPos.Y = currentPos.Y + 25
                    controlCampo.Location = New Drawing.Point(currentPos.X, currentPos.Y)
                    panelControles.Controls.Add(controlCampo)
                    panelControles.Controls.Add(llaveLabel)
                End If

                controlCampo.Tag = campo
                controlCampo.Enabled = campo.Es_Modificable
                If (nAnchorLeftRight) Then
                    controlCampo.Anchor = ((AnchorStyles.Top Or AnchorStyles.Left) _
                                           Or AnchorStyles.Right)
                End If
            Next

            panelControles.Refresh()

            Return panelControles
        End Function

        Public Shared Function CreaControlesValidacion(ByVal nValidaciones As List(Of ValidacionCaptura), ByVal bShowData As Boolean, ByVal nAncho As Integer) As TableLayoutPanel
            Dim tableLayoutPanelControles As New TableLayoutPanel

            Const alto As Integer = 0

            tableLayoutPanelControles.ColumnCount = 1
            tableLayoutPanelControles.RowCount = nValidaciones.Count - 1
            tableLayoutPanelControles.Dock = DockStyle.Fill

            Dim i As Integer = 0
            For Each campo As ValidacionCaptura In nValidaciones
                tableLayoutPanelControles.Controls.Add(campo.Control, 0, i)

                i += 1
            Next

            'tableLayoutPanelControles.Width = nAncho - 30
            tableLayoutPanelControles.Height = alto

            Return tableLayoutPanelControles
        End Function

        Public Shared Function CreaControlesTerceraCaptura(ByVal nCampos As List(Of DesktopConfig.CrearControlesType), ByRef nConnectionStrings As DesktopConfig.TypeConnectionString, ByRef nRiskGlobal As RiskGlobal) As TableLayoutPanel
            Dim tableLayoutPanelControles As New TableLayoutPanel
            Dim dmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing

            Try
                dmArchiving = New DBArchiving.DBArchivingDataBaseManager(nConnectionStrings.Archiving)
                dmArchiving.Connection_Open(1)
                Dim alto As Integer = 0
                Dim ancho As Integer = 0

                tableLayoutPanelControles.ColumnCount = 4
                tableLayoutPanelControles.RowCount = nCampos.Count - 1
                Dim i As Integer = 0
                For Each campo As DesktopConfig.CrearControlesType In nCampos
                    Dim nombreCampo As String = ConvertName(campo.NombreCampo)
                    Dim controlCampoPrimeraCaptura As New Control
                    Dim controlCampoSegundaCaptura As New Control
                    Dim controlCampoTerceraCaptura As New Control

                    'Crea el Label del campo
                    Dim llaveLabel As New Label
                    llaveLabel.Name = "lbl_" & nombreCampo
                    llaveLabel.Text = campo.Label
                    llaveLabel.Width = campo.LabelWidth

                    'Crea los campos
                    If campo.Tipo = DesktopConfig.CampoTipo.Numerico Or campo.Tipo = DesktopConfig.CampoTipo.Fecha Or campo.Tipo = DesktopConfig.CampoTipo.Texto Or campo.Tipo = DesktopConfig.CampoTipo.Query Then

                        Dim llaveTextBoxPrimeraCaptura As New DesktopTextBoxControl
                        llaveTextBoxPrimeraCaptura.Name = nombreCampo & "PrimeraCaptura"
                        llaveTextBoxPrimeraCaptura.MaxLength = campo.MaxLength

                        Dim llaveTextBoxSegundaCaptura As New DesktopTextBoxControl
                        llaveTextBoxSegundaCaptura.Name = nombreCampo & "SegundaCaptura"
                        llaveTextBoxSegundaCaptura.MaxLength = campo.MaxLength

                        Dim llaveTextBoxTerceraCaptura As New DesktopTextBoxControl
                        llaveTextBoxTerceraCaptura.Name = nombreCampo & "TerceraCaptura"
                        llaveTextBoxTerceraCaptura.MaxLength = campo.MaxLength

                        Select Case campo.Tipo
                            Case DesktopConfig.CampoTipo.Numerico
                                llaveTextBoxPrimeraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                                llaveTextBoxPrimeraCaptura.Text = CInt(campo.File_Data_PrimeraCaptura)
                                llaveTextBoxPrimeraCaptura.Enabled = False

                                llaveTextBoxSegundaCaptura.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                                llaveTextBoxSegundaCaptura.Text = CInt(campo.File_Data_SegundaCaptura)
                                llaveTextBoxSegundaCaptura.Enabled = False

                                llaveTextBoxTerceraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                                llaveTextBoxTerceraCaptura.Enabled = True


                            Case DesktopConfig.CampoTipo.Fecha
                                llaveTextBoxPrimeraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                                llaveTextBoxPrimeraCaptura.Text = campo.File_Data_PrimeraCaptura
                                llaveTextBoxPrimeraCaptura.Enabled = False

                                llaveTextBoxSegundaCaptura.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                                llaveTextBoxSegundaCaptura.Text = campo.File_Data_SegundaCaptura
                                llaveTextBoxSegundaCaptura.Enabled = False

                                llaveTextBoxTerceraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                                llaveTextBoxTerceraCaptura.Enabled = True

                            Case DesktopConfig.CampoTipo.Texto
                                llaveTextBoxPrimeraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Normal
                                llaveTextBoxPrimeraCaptura.Text = CStr(IIf(IsDBNull(campo.File_Data_PrimeraCaptura), "", campo.File_Data_PrimeraCaptura))
                                llaveTextBoxPrimeraCaptura.Enabled = False

                                llaveTextBoxSegundaCaptura.Type = DesktopTextBoxControl.TipoTextBox.Normal
                                llaveTextBoxSegundaCaptura.Text = CStr(IIf(IsDBNull(campo.File_Data_SegundaCaptura), "", campo.File_Data_SegundaCaptura))
                                llaveTextBoxSegundaCaptura.Enabled = False

                                llaveTextBoxTerceraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Normal
                                llaveTextBoxTerceraCaptura.Enabled = True

                                'Campo tipo query
                            Case DesktopConfig.CampoTipo.Query
                                llaveTextBoxPrimeraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Normal
                                llaveTextBoxPrimeraCaptura.Text = CStr(IIf(IsDBNull(campo.File_Data_PrimeraCaptura), "", campo.File_Data_PrimeraCaptura))
                                llaveTextBoxPrimeraCaptura.Enabled = False

                                llaveTextBoxSegundaCaptura.Type = DesktopTextBoxControl.TipoTextBox.Normal
                                llaveTextBoxSegundaCaptura.Text = CStr(IIf(IsDBNull(campo.File_Data_SegundaCaptura), "", campo.File_Data_SegundaCaptura))
                                llaveTextBoxSegundaCaptura.Enabled = False

                                llaveTextBoxTerceraCaptura.Type = DesktopTextBoxControl.TipoTextBox.Normal
                                llaveTextBoxTerceraCaptura.Enabled = False
                                llaveTextBoxTerceraCaptura.Text = "Automatico"
                        End Select

                        llaveTextBoxPrimeraCaptura.Width = campo.Width
                        llaveTextBoxSegundaCaptura.Width = campo.Width
                        llaveTextBoxTerceraCaptura.Width = campo.Width

                        controlCampoPrimeraCaptura = llaveTextBoxPrimeraCaptura
                        controlCampoSegundaCaptura = llaveTextBoxSegundaCaptura
                        controlCampoTerceraCaptura = llaveTextBoxTerceraCaptura

                        alto += 35

                    ElseIf campo.Tipo = DesktopConfig.CampoTipo.SiNo Then
                        Dim llaveCheckBoxPrimeraCaptura As New Controls.DesktopCheckBox.DesktopCheckBoxControl
                        llaveCheckBoxPrimeraCaptura.Name = ConvertName(nombreCampo)

                        Dim llaveCheckBoxSegundaCaptura As New Controls.DesktopCheckBox.DesktopCheckBoxControl
                        llaveCheckBoxSegundaCaptura.Name = ConvertName(nombreCampo)

                        Dim llaveCheckBoxTerceraCaptura As New Controls.DesktopCheckBox.DesktopCheckBoxControl
                        llaveCheckBoxTerceraCaptura.Name = ConvertName(nombreCampo)

                        controlCampoPrimeraCaptura = llaveCheckBoxPrimeraCaptura
                        controlCampoSegundaCaptura = llaveCheckBoxSegundaCaptura
                        controlCampoTerceraCaptura = llaveCheckBoxTerceraCaptura

                        alto += 40

                    ElseIf campo.Tipo = DesktopConfig.CampoTipo.Lista Then
                        Dim llaveComboBoxPrimeraCaptura As New DesktopComboBoxControl
                        llaveComboBoxPrimeraCaptura.Name = ConvertName(nombreCampo)

                        Dim llaveComboBoxSegundaCaptura As New DesktopComboBoxControl
                        llaveComboBoxSegundaCaptura.Name = ConvertName(nombreCampo)

                        Dim llaveComboBoxTerceraCaptura As New DesktopComboBoxControl
                        llaveComboBoxTerceraCaptura.Name = ConvertName(nombreCampo)

                        Dim tableDrop = dmArchiving.SchemaCore.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(nRiskGlobal.Entidad, campo.CampoLista)
                        llaveComboBoxPrimeraCaptura.ValueMember = tableDrop.Valor_Campo_Lista_ItemColumn.ColumnName
                        llaveComboBoxPrimeraCaptura.DisplayMember = tableDrop.Etiqueta_Campo_Lista_ItemColumn.ColumnName

                        llaveComboBoxSegundaCaptura.ValueMember = tableDrop.Valor_Campo_Lista_ItemColumn.ColumnName
                        llaveComboBoxSegundaCaptura.DisplayMember = tableDrop.Etiqueta_Campo_Lista_ItemColumn.ColumnName

                        llaveComboBoxTerceraCaptura.ValueMember = tableDrop.Valor_Campo_Lista_ItemColumn.ColumnName
                        llaveComboBoxTerceraCaptura.DisplayMember = tableDrop.Etiqueta_Campo_Lista_ItemColumn.ColumnName

                        controlCampoPrimeraCaptura = llaveComboBoxPrimeraCaptura
                        controlCampoSegundaCaptura = llaveComboBoxSegundaCaptura
                        controlCampoTerceraCaptura = llaveComboBoxTerceraCaptura

                        alto += 35
                    End If


                    'Agrega los elementos al Panel
                    tableLayoutPanelControles.Controls.Add(llaveLabel, 0, i)
                    tableLayoutPanelControles.Controls.Add(controlCampoPrimeraCaptura, 1, i)
                    tableLayoutPanelControles.Controls.Add(controlCampoSegundaCaptura, 2, i)
                    tableLayoutPanelControles.Controls.Add(controlCampoTerceraCaptura, 3, i)

                    If ancho < campo.LabelWidth + campo.Width Then
                        ancho = campo.LabelWidth + (campo.Width * 3) + 30
                    End If

                    i += 1
                Next

                tableLayoutPanelControles.Width = ancho
                tableLayoutPanelControles.Height = alto
            Catch ex As Exception
                Throw
            Finally
                If dmArchiving IsNot Nothing Then dmArchiving.Connection_Close()
            End Try

            Return tableLayoutPanelControles
        End Function

        Public Shared Function GetValueControl(ByVal container As Control, ByVal nControlName As String) As Object
            Dim valor As Object = Nothing
            Dim nombreControl As String = ConvertName(nControlName)
            Dim controlCampo As Control = FindControl(container, nombreControl)

            Dim tipo As Type = controlCampo.GetType()

            If tipo = GetType(DesktopTextBoxControl) Then
                Dim control As DesktopTextBoxControl = CType(controlCampo, DesktopTextBoxControl)

                If control.Type = DesktopTextBoxControl.TipoTextBox.Numerico Then
                    valor = DInt(control.Text)
                ElseIf control.Type = DesktopTextBoxControl.TipoTextBox.Normal Then
                    valor = DStr(control.Text)
                ElseIf control.Type = DesktopTextBoxControl.TipoTextBox.Fecha Then
                    valor = DDate(control.Text)
                End If

            ElseIf tipo = GetType(DesktopTextBoxControl) Then

            End If

            Return valor
        End Function

        Public Shared Function SelectIntoDataTable(ByVal selectFilter As String, ByVal sourceDataTable As DataTable) As DataTable
            Dim newDataTable As DataTable = sourceDataTable.Clone

            Dim dataRows As DataRow() = sourceDataTable.Select(selectFilter)
            Dim typeDataRow As DataRow
            For Each typeDataRow In dataRows
                newDataTable.ImportRow(typeDataRow)
            Next

            Return newDataTable
        End Function
        Public Shared Sub LlenarcomboOrder(ByRef nCombo As DesktopComboBoxControl, ByVal nTableSource As DataTable, ByVal value As String, ByVal texto As String, ByVal nSelection As Boolean, Optional ByVal nValueSel As String = "-1", Optional ByVal nTextSel As String = "Seleccione...")
            Dim table As DataTable

            Try
                table = ClonarDataTable(nTableSource)
            Catch ex As Exception
                table = Nothing
            End Try

            If value <> texto Then
                table = table.DefaultView.ToTable(True, value, texto)
            Else
                table = table.DefaultView.ToTable(True, value)
            End If


            'agrega columna para ordenamiento
            Try
                Dim columna As New DataColumn("OrdenVS")
                columna.DefaultValue = 2
                table.Columns.Add(columna)
            Catch ex As Exception
            End Try


            'Agrega en el primer item la opcion de seleccione..
            If nSelection = True Then
                Try
                    Dim row As DataRow = table.NewRow
                    row("OrdenVS") = 1
                    row(value) = nValueSel
                    row(texto) = nTextSel
                    table.Rows.Add(row)
                Catch ex As Exception
                End Try

                Dim view2 = table.DefaultView
                view2.Sort = "OrdenVS"
                table = view2.ToTable
            End If

            'Ordena Ascendentemente una columna            
            If texto <> "" Then
                Try
                    Dim view3 = table.DefaultView
                    table = view3.ToTable
                Catch ex As Exception
                End Try
            End If

            'Llena un DropDownList con un origen de datos
            Try
                With nCombo
                    .ValueMember = value
                    .DisplayMember = texto
                    .DataSource = table
                End With
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub Llenarcombo(ByRef nCombo As DesktopComboBoxControl, ByVal nTableSource As DataTable, ByVal value As String, ByVal texto As String, ByVal nSelection As Boolean, Optional ByVal nValueSel As String = "-1", Optional ByVal nTextSel As String = "Seleccione...")
            Dim table As DataTable

            Try
                table = ClonarDataTable(nTableSource)
            Catch ex As Exception
                table = Nothing
            End Try

            If value <> texto Then
                table = table.DefaultView.ToTable(True, value, texto)
            Else
                table = table.DefaultView.ToTable(True, value)
            End If


            'agrega columna para ordenamiento
            Try
                Dim columna As New DataColumn("OrdenVS")
                columna.DefaultValue = 2
                table.Columns.Add(columna)
            Catch ex As Exception
            End Try


            'Agrega en el primer item la opcion de seleccione..
            If nSelection = True Then
                Try
                    Dim row As DataRow = table.NewRow
                    row("OrdenVS") = 1
                    row(value) = nValueSel
                    row(texto) = nTextSel
                    table.Rows.Add(row)
                Catch ex As Exception
                End Try

                Dim view2 = table.DefaultView
                view2.Sort = "OrdenVS"
                table = view2.ToTable
            End If

            'Ordena Ascendentemente una columna            
            If texto <> "" Then
                Try
                    Dim view3 = table.DefaultView
                    view3.Sort = "OrdenVS," & texto
                    table = view3.ToTable
                Catch ex As Exception
                End Try
            End If

            'Llena un DropDownList con un origen de datos
            Try
                With nCombo
                    .ValueMember = value
                    .DisplayMember = texto
                    .DataSource = table
                End With
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub Llenarcombo_(ByRef nCombo As Windows.Forms.ComboBox, ByVal nTableSource As DataTable, ByVal value As String, ByVal texto As String, ByVal nSelection As Boolean, Optional ByVal nValueSel As String = "-1", Optional ByVal nTextSel As String = "Seleccione...")
            Dim table As DataTable

            Try
                table = ClonarDataTable(nTableSource)
            Catch ex As Exception
                table = Nothing
            End Try

            If value <> texto Then
                table = table.DefaultView.ToTable(True, value, texto)
            Else
                table = table.DefaultView.ToTable(True, value)
            End If

            'agrega columna para ordenamiento
            Try
                Dim columna As New DataColumn("OrdenVS")
                columna.DefaultValue = 2
                table.Columns.Add(columna)
            Catch ex As Exception
            End Try


            'Agrega en el primer item la opcion de seleccione..
            If nSelection = True Then
                Try
                    Dim row As DataRow = table.NewRow
                    row("OrdenVS") = 1
                    row(value) = nValueSel
                    row(texto) = nTextSel
                    table.Rows.Add(row)
                Catch ex As Exception
                End Try

                Dim view2 = table.DefaultView
                view2.Sort = "OrdenVS"
                table = view2.ToTable
            End If

            'Ordena Ascendentemente una columna            
            If texto <> "" Then
                Try
                    Dim view3 = table.DefaultView
                    view3.Sort = "OrdenVS," & texto
                    table = view3.ToTable
                Catch ex As Exception
                End Try
            End If

            'Llena un DropDownList con un origen de datos
            Try
                With nCombo
                    .ValueMember = value
                    .DisplayMember = texto
                    .DataSource = table
                End With
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Utilidades Texto"
        Public Shared Function LimpiarCadena(ByVal cadena As String) As String
            Try
                cadena = cadena.Replace("/", "_")
                cadena = cadena.Replace(".", "_")
                cadena = cadena.Replace(":", "_")
                cadena = cadena.Replace(" ", "_")
            Catch ex As Exception
            End Try
            Return cadena
        End Function

        Public Shared Function ObtieneEstadobyNombre(ByVal sEstado As String) As Short
            Dim idEstado As Short = -1
            Try
                For Each estado As DBCore.EstadoEnum In [Enum].GetValues(GetType(DBCore.EstadoEnum))
                    If estado.ToString() = sEstado Then
                        idEstado = estado
                        Exit For
                    End If
                Next
            Catch ex As Exception
            End Try
            Return idEstado
        End Function

        Public Shared Sub CrearDirectorio(path As String, Optional Elimina As Boolean = True)
            If (Not Directory.Exists(path)) Then
                Directory.CreateDirectory(path)
            Else
                If (Elimina) Then
                    Directory.Delete(path, Elimina)
                End If
                Directory.CreateDirectory(path)
            End If
        End Sub

        Public Shared Sub EscribeLog(pathStrFile As String, StrLine As String, Optional CrearFile As Boolean = False, Optional LeerFile As Boolean = False)
            Dim modeFile As FileMode = Nothing
            Dim modeFile_2 As FileMode = Nothing
            Dim strMessageComplete As String = StrLine + Environment.NewLine
            If (CrearFile) Then
                modeFile = FileMode.CreateNew
                modeFile_2 = FileAccess.Write
                Using fs As New FileStream(pathStrFile, modeFile)
                    Using w As New BinaryWriter(fs, Encoding.UTF8)
                        w.Write(strMessageComplete + " Date : " + Date.Now.ToString())
                    End Using
                End Using
            ElseIf (LeerFile) Then
                modeFile = FileMode.Append
                Using fs As New FileStream(pathStrFile, modeFile)
                    Using w As New BinaryWriter(fs, Encoding.UTF8)
                        w.Write(strMessageComplete + " Date : " + Date.Now.ToString())
                    End Using
                End Using
            End If
        End Sub

#End Region

#Region "Reportes"

        ''' <summary>
        ''' Enlaza un Origen de Datos a un Reporte
        ''' </summary>
        ''' <param name="nReportView">Id del Control ReportViewer</param>
        ''' <param name="nName">Nombre del origen de datos del reporte</param>
        ''' <param name="nData">Origen de datos (DataSet, DataTable)</param>
        ''' <remarks></remarks>
        Public Shared Sub NewDataSource(ByVal nReportView As ReportViewer, ByVal nName As String, ByVal nData As Object)
            If Not nData Is Nothing Then
                Dim reportData = New ReportDataSource(nName, nData)
                nReportView.LocalReport.DataSources.Add(reportData)
                nReportView.LocalReport.Refresh()
            Else
                nReportView.LocalReport.DataSources.Add(Nothing)
                nReportView.LocalReport.Refresh()
            End If
        End Sub

        ''' <summary>
        ''' Enlaza un Origen de Datos a un Reporte
        ''' </summary>
        ''' <param name="nReportView">Id del Control ReportViewer</param>
        ''' <param name="nName">Nombre del origen de datos del reporte</param>
        ''' <param name="nData">Origen de datos (DataSet, DataTable)</param>
        ''' <param name="nParametros">Parametros que recibe el Reporte</param>
        ''' <remarks></remarks>
        Public Shared Sub NewDataSource(ByVal nReportView As ReportViewer, ByVal nName As String, ByVal nData As Object, ByVal nParametros As List(Of ReportParameter))
            Try
                Dim reportData As New ReportDataSource(nName, nData)
                nReportView.LocalReport.DataSources.Add(reportData)
                nReportView.LocalReport.Refresh()

                nReportView.LocalReport.SetParameters(nParametros)

            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region "Impresion"
        Public Shared Function IniciarImpresion(ByVal nConnectionStrings As DesktopConfig.TypeConnectionString, ByVal nSesion As Security.Library.Session.Sesion, ByVal nTableCBarras As DataTable, Optional ByRef nCBarrasProgressBar As ProgressBar = Nothing) As DialogResult
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(nConnectionStrings.Archiving)

            Try
                dmArchiving.Connection_Open(nSesion.Usuario.id)

                Dim tableCBarrasTipo = nTableCBarras.DefaultView.ToTable(True, "fk_OT", "fk_Expediente", "id_Folder", "fk_File", "CBarras", "Tipo")

                If tableCBarrasTipo.Rows.Count <> 0 Then
                    If nCBarrasProgressBar IsNot Nothing Then
                        nCBarrasProgressBar.Minimum = 0
                        nCBarrasProgressBar.Maximum = tableCBarrasTipo.Rows.Count
                        nCBarrasProgressBar.Value = 0
                    End If


                    For Each rowCBarras As DataRow In tableCBarrasTipo.Rows
                        Dim viewParametros As DataView = nTableCBarras.DefaultView
                        Dim cBarras As String = rowCBarras("CBarras").ToString
                        viewParametros.RowFilter = "CBarras" & "='" & cBarras & "'"

                        Dim parametros As New List(Of DesktopConfig.AtributesCBarras)
                        parametros.Clear()
                        For Each rowParametros As DataRow In viewParametros.ToTable.Rows
                            Dim parametro As New DesktopConfig.AtributesCBarras
                            parametro.Label = rowParametros("Label").ToString
                            parametro.Valor = rowParametros("Valor").ToString

                            If Not (parametro.Valor = "" And parametro.Valor = "") Then
                                parametros.Add(parametro)
                            End If
                        Next

                        Dim barCodeControl As New Controls.BarCode.BarCodeControl
                        ImprimirCBarras(barCodeControl, cBarras, viewParametros.ToTable.Rows(0)("Title").ToString(), parametros)
                        'BarCodeControl_.Print()

                        Dim ot As Integer = CInt(rowCBarras("fk_OT"))
                        Dim expediente As Long = CLng(rowCBarras("fk_Expediente"))
                        Dim folder As Short = CShort(rowCBarras("id_Folder"))
                        Dim file As Integer = CInt(rowCBarras("fk_File"))

                        'codigo de barras de un carpeta

                        If rowCBarras("Tipo").ToString = "0" Then
                            Dim folderType = New DBArchiving.SchemaRisk.TBL_FolderType()
                            folderType.Impreso = True
                            dmArchiving.SchemaRisk.TBL_Folder.DBUpdate(folderType, expediente, folder, ot)
                        Else

                            'codigo de barras de un documento
                            Dim registroFile As New DBArchiving.SchemaRisk.TBL_FileType
                            registroFile.Impreso = True

                            dmArchiving.SchemaRisk.TBL_File.DBUpdate(registroFile, ot, folder, file, expediente)
                        End If

                        If nCBarrasProgressBar IsNot Nothing Then
                            nCBarrasProgressBar.Value += 1
                        End If
                    Next
                    Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Codigos de barras impresos con exito (Total codigos impresos: " & CStr(tableCBarrasTipo.Rows.Count) & ")", "Impresion codigos de barras", Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                    Return DialogResult.OK

                Else
                    Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Los codigos de barras del precinto ya han sido impresos.", "Impresion codigos de barras", Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Return DialogResult.Cancel
                End If

            Catch ex As Exception
                dmArchiving.Transaction_Rollback()
                Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("IniciarImpresion", ex)
                Throw
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Function

        Public Shared Function IniciarImpresionImaging(ByVal nConnectionStrings As DesktopConfig.TypeConnectionString, ByVal nSesion As Security.Library.Session.Sesion, ByVal nTableCBarras As DataTable, Optional ByRef nCBarrasProgressBar As ProgressBar = Nothing) As DialogResult
            Try
                Dim tableCBarrasTipo = nTableCBarras.DefaultView.ToTable(True, "CBarras", "Tipo")

                If tableCBarrasTipo.Rows.Count <> 0 Then
                    If nCBarrasProgressBar IsNot Nothing Then
                        nCBarrasProgressBar.Minimum = 0
                        nCBarrasProgressBar.Maximum = tableCBarrasTipo.Rows.Count
                        nCBarrasProgressBar.Value = 0
                    End If

                    For Each rowCBarras As DataRow In tableCBarrasTipo.Rows
                        Dim viewParametros As DataView = nTableCBarras.DefaultView
                        Dim cBarras As String = rowCBarras("CBarras").ToString
                        viewParametros.RowFilter = "CBarras" & "='" & cBarras & "'"

                        Dim parametros As New List(Of DesktopConfig.AtributesCBarras)
                        parametros.Clear()
                        For Each rowParametros As DataRow In viewParametros.ToTable.Rows
                            Dim parametro As New DesktopConfig.AtributesCBarras
                            parametro.Label = rowParametros("Label").ToString
                            parametro.Valor = rowParametros("Valor").ToString

                            If Not (parametro.Label = "" And parametro.Valor = "") Then
                                parametros.Add(parametro)
                            End If
                        Next

                        Dim barCodeControl As New Controls.BarCode.BarCodeControl
                        ImprimirCBarras(barCodeControl, cBarras, viewParametros.ToTable.Rows(0)("Title").ToString(), parametros)

                        If nCBarrasProgressBar IsNot Nothing Then
                            nCBarrasProgressBar.Value += 1
                        End If
                    Next
                    Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Codigos de barras impresos con exito (Total codigos impresos: " & CStr(tableCBarrasTipo.Rows.Count) & ")", "Impresion codigos de barras", Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                    Return DialogResult.OK

                Else
                    Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Los codigos de barras de la solicitud ya han sido impresos.", "Impresion codigos de barras", Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Return DialogResult.Cancel
                End If

            Catch ex As Exception
                Controls.DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("IniciarImpresion", ex)
                Throw
            End Try
        End Function

#End Region

#Region "Facturación"

        Public Shared Sub AgregarMovimiento(ByVal dmArchiving As DBArchiving.DBArchivingDataBaseManager, _
                                            ByVal fkEntidad As Short, _
                                            ByVal fkEsquema As Short, _
                                            ByVal fkServicio As DesktopConfig.Servicio_Facturacion, _
                                            ByVal fkEntidadCliente As Short, _
                                            ByVal fkProyectoCliente As Short, _
                                            ByVal fkEsquemaCliente As Short, _
                                            ByVal cantidadMovimiento As Integer, _
                                            ByVal fkUsuarioLog As Integer, _
                                            ByVal centroProcesamientoRow As DBCore.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType)
            Dim bTieneTransaccion As Boolean = dmArchiving.DataBase.ExistsTransaction
            Try
                Dim typeMovimiento As New DBArchiving.SchemaBill.TBL_MovimientoType

                If Not bTieneTransaccion Then
                    dmArchiving.Transaction_Begin()
                End If

                dmArchiving.SchemaBill.PA_Inserta_Movimiento.DBExecute(centroProcesamientoRow.fk_Entidad, centroProcesamientoRow.fk_Sede, _
                                                                       centroProcesamientoRow.id_Centro_Procesamiento, fkEsquema, _
                                                                       CInt(fkServicio), fkEntidadCliente, fkProyectoCliente, _
                                                                       fkEsquemaCliente, cantidadMovimiento, fkUsuarioLog)

                If Not bTieneTransaccion Then
                    dmArchiving.Transaction_Commit()
                End If
            Catch
                If Not bTieneTransaccion Then
                    dmArchiving.Transaction_Rollback()
                End If

                Throw
            End Try
        End Sub

#End Region

#Region "Clases Auxiliares"

        Public Class ListItem

            Public Property Value() As String

            Public Property Text() As String

            Public Sub New(ByVal value As String, ByVal text As String)
                Me.Value = value
                Me.Text = text
            End Sub

        End Class

#End Region

#Region "Funciones para la DAL"

        ''' <summary>
        ''' Convierte un valor a SlygNullable(Of Long) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="nValue">Valor a convertir</param>
        ''' <returns>Valor Tipo SlygNullable(Of Long)</returns>
        ''' <remarks></remarks>
        Public Shared Function Dlng(ByVal nValue As Object) As SlygNullable(Of Long)

            Try
                If IsDBNull(nValue) = True Then
                    nValue = DBNull.Value
                End If
            Catch ex As Exception
            End Try


            Dim valor As SlygNullable(Of Long)
            If nValue Is Nothing Or CStr(nValue) = "" Then
                valor = DBNull.Value
            Else

                Try
                    If nValue.ToString = "-1" Then
                        valor = DBNull.Value
                    Else
                        Try
                            valor = CLng(nValue)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Long)")
                        End Try
                    End If
                Catch ex As Exception
                    valor = DBNull.Value
                End Try

            End If
            Return valor
        End Function

        ''' <summary>
        ''' Convierte un valor a SlygNullable(Of Date) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="nValue">Valor a convertir</param>
        ''' <returns>Valor Tipo SlygNullable(Of Date)</returns>
        ''' <remarks></remarks>
        Public Shared Function DDate(ByVal nValue As Object) As SlygNullable(Of Date)

            Try
                If IsDBNull(nValue) = True Then
                    nValue = DBNull.Value
                End If
            Catch ex As Exception
            End Try

            If nValue Is Nothing Or CStr(nValue) = "" Then
                Return DBNull.Value
            Else

                Try
                    If nValue.ToString = "-1" Then
                        Return DBNull.Value
                    End If
                Catch ex As Exception
                End Try

                Try
                    Return CDate(nValue)
                Catch ex As Exception
                    Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Date)")
                End Try
            End If
        End Function

        ''' <summary>
        ''' Convierte un valor a SlygNullable(Of String) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="nValue">Valor a convertir</param>
        ''' <returns>Valor Tipo SlygNullable(Of String)</returns>
        ''' <remarks></remarks>
        Public Shared Function DStr(ByVal nValue As Object) As SlygNullable(Of String)

            Try
                If IsDBNull(nValue) = True Then
                    nValue = DBNull.Value
                End If
            Catch ex As Exception
            End Try

            If nValue Is Nothing Or CStr(nValue) = "" Then
                Return DBNull.Value
            Else

                Try
                    If nValue.ToString = "-1" Then
                        Return DBNull.Value
                    End If
                Catch ex As Exception
                End Try

                Try
                    Return CStr(nValue)
                Catch ex As Exception
                    Throw New Exception("El Valor no se puede Convertir a System.OblectNullable(of String)")
                End Try
            End If
        End Function

        ''' <summary>
        ''' Convierte un valor a SlygNullable(Of Integer) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="nValue">Valor a convertir</param>
        ''' <returns>Valor Tipo SlygNullable(Of Integer)</returns>
        ''' <remarks></remarks>
        Public Shared Function DInt(ByVal nValue As Object) As SlygNullable(Of Integer)

            Try
                If IsDBNull(nValue) = True Then
                    nValue = DBNull.Value
                End If
            Catch ex As Exception
            End Try


            Dim valor As SlygNullable(Of Integer)


            If nValue Is Nothing Or CStr(nValue) = "" Then
                valor = DBNull.Value
            Else

                Try
                    If nValue.ToString = "-1" Then
                        valor = DBNull.Value
                    Else
                        Try
                            valor = CInt(nValue)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Integer)")
                        End Try
                    End If
                Catch ex As Exception
                    Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Integer)")
                End Try

            End If

            Return valor
        End Function

        ''' <summary>
        ''' Convierte un valor a SlygNullable(Of Short) (Si el valor es Vacio, Nulo o -1 Entonces retorna nothing)
        ''' </summary>
        ''' <param name="nValue">Valor a convertir</param>
        ''' <returns>Valor Tipo SlygNullable(Of Short)</returns>
        ''' <remarks></remarks>
        Public Shared Function DShort(ByVal nValue As Object) As SlygNullable(Of Short)

            Try
                If IsDBNull(nValue) = True Then
                    nValue = DBNull.Value
                End If
            Catch ex As Exception
            End Try


            Dim valor As SlygNullable(Of Short)
            If nValue Is Nothing Or CStr(nValue) = "" Then
                valor = DBNull.Value
            Else

                Try
                    If nValue.ToString = "-1" Then
                        valor = DBNull.Value
                    Else
                        Try
                            valor = CShort(nValue)
                        Catch ex As Exception
                            Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Long)")
                        End Try
                    End If
                Catch ex As Exception
                    Throw New Exception("El Valor no se puede Convertir a System.VallueNullable(of Short)")
                End Try

            End If
            Return valor
        End Function

#End Region

#Region " Tools "

        Public Shared Function GetEnumFormat(ByVal nExtension As String) As ImageManager.EnumFormat
            Select Case nExtension.ToLower().TrimStart("."c)
                Case "bmp"
                    Return ImageManager.EnumFormat.Bmp
                Case "gif"
                    Return ImageManager.EnumFormat.Gif
                Case "jpg", "jpeg"
                    Return ImageManager.EnumFormat.Jpeg
                Case "pdf"
                    Return ImageManager.EnumFormat.Pdf
                Case "png"
                    Return ImageManager.EnumFormat.Png
                Case Else
                    Return ImageManager.EnumFormat.Tiff
            End Select
        End Function

        Public Shared Function GetEnumCompression(ByVal nFormato As DesktopConfig.FormatoImagenEnum) As ImageManager.EnumCompression
            Select Case nFormato
                Case DesktopConfig.FormatoImagenEnum.TIFF_Bitonal
                    Return ImageManager.EnumCompression.Ccitt4
                Case DesktopConfig.FormatoImagenEnum.TIFF_Color
                    Return ImageManager.EnumCompression.Jpeg
                Case Else
                    Return ImageManager.EnumCompression.Lzw

            End Select
        End Function

        Public Shared Function GetEnumDefaultFlags(ByVal nFormato As ImageManager.EnumFormat) As FreeImageAPI.FREE_IMAGE_SAVE_FLAGS
            Select Case nFormato
                Case ImageManager.EnumFormat.Jpeg
                    Return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD
                Case ImageManager.EnumFormat.Tiff
                    Return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.TIFF_JPEG
                Case ImageManager.EnumFormat.Png
                    Return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.PNG_Z_BEST_COMPRESSION
                Case Else
                    Return FreeImageAPI.FREE_IMAGE_SAVE_FLAGS.DEFAULT
            End Select
        End Function

        'Declaración de la API
        Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean

        'Funcion de liberacion de memoria
        Public Shared Sub ClearMemory()
            Try
                Dim mem As Process
                mem = Process.GetCurrentProcess()
                SetProcessWorkingSetSize(mem.Handle, -1, -1)
            Catch ex As Exception
                'Control de errores
            End Try
        End Sub

#End Region

    End Class

    Public Module Ext

        <Extension()> _
        Public Function Value(Of T)(ByVal row As DataGridViewRow, ByVal columnName As String) As T
            Try

                If GetType(T).In(GetType(Short), GetType(Integer), GetType(Long), GetType(Byte)) Then
                    Try
                        Return CType(row.Cells(columnName).Value, T)
                    Catch ex As Exception
                        Dim cero As Object = 0
                        Return CType(cero, T)
                    End Try
                End If

                Return CType(row.Cells(columnName).Value, T)

            Catch ex As Exception
                Throw New Exception("Error al obtener el valor de la columna [" + columnName + "][" + ex.Message + "]")
            End Try
        End Function

        <Extension()> _
        Public Function Value(ByVal row As DataGridViewRow, ByVal columnName As String) As String
            Try
                If (IsDBNull(row.Cells(columnName).Value)) Then
                    Return ""
                End If


                If String.IsNullOrEmpty(row.Cells(columnName).Value) Then
                    Return ""
                End If

                Return row.Cells(columnName).Value.ToString()

            Catch ex As Exception
                Throw New Exception("Error al obtener el valor de la columna [" + columnName + "][" + ex.Message + "]")
            End Try
        End Function

        <Extension()> _
        Public Function [In](Of T)(ByVal par As T, ByVal ParamArray values As T()) As Boolean
            For Each value As T In values
                If par.Equals(value) Then
                    Return True
                End If
            Next

            Return False
        End Function

        <Extension()> _
        Public Sub Fill(ByVal nCombo As ComboBox, ByVal nData As DataTable, ByVal nValue As DataColumn, ByVal nText As DataColumn)
            Utilities.LlenarCombo(nCombo, nData, nValue.ColumnName, nText.ColumnName)
        End Sub

        <Extension()> _
        Public Sub Fill(ByRef nCombo As DesktopComboBoxControl, ByVal data As DataTable, ByVal value As DataColumn, ByVal text As DataColumn, ByVal nSelection As Boolean, Optional ByVal nOrderBy As String = "", Optional ByVal nValueSel As String = "-1", Optional ByVal nTextSel As String = "Seleccione ...")
            Utilities.LlenarCombo(nCombo, data, value.ColumnName, text.ColumnName, nSelection, nValueSel, nTextSel)
        End Sub

    End Module

End Namespace