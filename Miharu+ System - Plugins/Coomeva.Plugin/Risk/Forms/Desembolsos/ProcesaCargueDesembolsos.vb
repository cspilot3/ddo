Imports System.ComponentModel
Imports DBArchiving
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports Coomeva.Plugin.Risk.FormWrappers

Namespace Forms.Desembolsos

    Public Class ProcesaCargueDesembolsos

#Region " Declaraciones "

        'Private Const CAMPOS_ESTANDAR As Integer = 4 'Campos Esquema, Tipología, Clase, CBarras
        Private _Plugin As CoomevaRiskPlugin
#End Region

#Region " Estructuras "

        Public Structure LlavesTipo
            Dim Llaves As List(Of String)
            Dim Tipo As Short
        End Structure

#End Region

#Region " Constructor "

        Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin)
            ' This call is required by the designer.
            _Plugin = nCoomevaDesktopPlugin
            ' Add any initialization after the InitializeComponent() call.

        End Sub

#End Region

#Region " Funciones "

        Public Function ProcesaCargueDesembolsos(ByVal data As DataTable, ByVal fileName As String, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim trRetrun As New DesktopConfig.TypeResult

            Try
                dbmArchiving = New DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)
                dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Const tabla As String = "#CARGUE_DESEMBOLSO"
                BulkInsert.InsertDataTable(data, dbmArchiving, tabla)

                '1.
                dbmArchiving.Transaction_Begin()
                'Valida e inserta los datos cargados
                Dim TablaErrores = dbmArchiving.SchemaLoad.PA_Cargue_Desembolso.DBExecute(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id, fileName)


                If Not CBool(TablaErrores.Rows(0)("Exitoso")) Then
                    'Por insercion de tabla error
                    dbmArchiving.Transaction_Commit()
                    ProcesaError(data, TablaErrores, trRetrun)
                Else
                    Try
                        trRetrun.Cargue = CInt(TablaErrores.Rows(0)("id_Cargue"))
                        trRetrun.Result = True
                        '2.
                        dbmArchiving.Transaction_Commit()
                    Catch ex As Exception
                        '3.
                        dbmArchiving.Transaction_Rollback()

                        Dim ListMsgError As New List(Of String)
                        Dim bRegistroValido As Boolean
                        trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                        trRetrun.Parameters = ListMsgError
                        ReportaError(ex.Message, ListMsgError, trRetrun, bRegistroValido)
                        trRetrun.Result = False
                    End Try

                End If

            Catch
                '4.
                dbmArchiving.Transaction_Rollback()
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function

        Public Function ProcesaActualizacion(ByRef data As DataTable, ByVal fileName As String, ByVal idEntidad As Short, ByVal idEsquema As Short, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim dbmArchiving As New DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)
            dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            Dim trRetrun As New DesktopConfig.TypeResult

            Try

                Const tabla As String = "#VALIDA_CARGUE_ACTUALIZACION"
                BulkInsert.InsertDataTable(data, dbmArchiving, tabla)

                'Valida los datos insertados
                Dim TablaErrores = dbmArchiving.Schemadbo.PA_Valida_Cargue_Actualizaciones.DBExecute(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, tabla)
                ProcesaError(data, TablaErrores, trRetrun)

                If trRetrun.Result Then
                    Try
                        dbmArchiving.Schemadbo.PA_Carga_Cargue_Actualizaciones.DBExecute(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, tabla)
                        trRetrun.Result = True

                    Catch ex As Exception
                        Dim ListMsgError As New List(Of String)
                        Dim bRegistroValido As Boolean
                        trRetrun.Resumen = New DesktopConfig.ValidacionCargue() With {.NoValido = 1}

                        trRetrun.Parameters = ListMsgError
                        ReportaError(ex.Message, ListMsgError, trRetrun, bRegistroValido)
                        trRetrun.Result = False
                    End Try

                End If

            Catch
                Throw
            Finally
                dbmArchiving.Connection_Close()
            End Try

            Return trRetrun
        End Function

        Private Function ValidaUniversal(ByVal data As DataTable, ByRef CargueBackgroundWorker As BackgroundWorker) As DesktopConfig.TypeResult
            Dim trRetrun As New DesktopConfig.TypeResult
            trRetrun.Result = True

            Dim nNumeroCamposData As Integer
            Dim ListmsgError As New List(Of String)
            Dim countReg As Integer = 0
            Dim countRegistrosNoValidos As Integer = 0
            Dim resultCargue As New DesktopConfig.ValidacionCargue

            nNumeroCamposData = data.Columns.Count

            'El número de campos recibidos, debe ser igual al número de llaves del proyecto más 1 (Esquema)
            If Not (nNumeroCamposData = _Plugin.Manager.RiskGlobal.LLavesProyecto.Count + 1) Then
                trRetrun.Result = False
                ListmsgError.Add("- El número de campos recibidos, debe ser igual al número de llaves del proyecto mas uno (Esquema). Valide los datos y que el carácter de separación sea el adecuado.")
                trRetrun.Parameters = ListmsgError
            End If

            For Each row As DataRow In data.Rows
                Dim nIdEsquema = -1
                countReg += 1
                If IsNumeric(row(0)) Then
                    nIdEsquema = CInt(row(0))
                End If

                Dim nCountUniversalFolderLlave = 1
                Dim bRegistroValido As Boolean = True

                If nIdEsquema <> -1 Then
                    For Each Llave In _Plugin.Manager.RiskGlobal.LLavesProyecto
                        Dim valorCampo = row(nCountUniversalFolderLlave)
                        Dim bTipoDatoValido As Boolean = False

                        Select Case Llave.Tipo
                            Case DesktopConfig.CampoTipo.Texto
                                bTipoDatoValido = (valorCampo.ToString() <> "")
                            Case DesktopConfig.CampoTipo.Numerico
                                bTipoDatoValido = IsNumeric(valorCampo)
                            Case DesktopConfig.CampoTipo.Fecha
                                Dim arrDate = valorCampo.ToString().Split(CChar(" "))
                                bTipoDatoValido = IsDate(arrDate(0))
                        End Select

                        If Not bTipoDatoValido Then
                            ListmsgError.Add("Línea " + countReg.ToString() + ": Tipo de dato de campos de data no coincidente")
                            trRetrun.Result = False
                            bRegistroValido = False
                        End If
                        nCountUniversalFolderLlave += 1
                    Next
                    CargueBackgroundWorker.ReportProgress(countReg)
                Else
                    ListmsgError.Add("Línea " + countReg.ToString() + ": Esquema no válido, ó registro vacío.")
                    trRetrun.Result = False
                    bRegistroValido = False
                End If

                If Not bRegistroValido Then
                    countRegistrosNoValidos += 1
                End If
            Next
            resultCargue.NoValido = countRegistrosNoValidos
            resultCargue.Valido = data.Rows.Count - countRegistrosNoValidos

            trRetrun.Parameters = ListmsgError
            trRetrun.Resumen = resultCargue

            Return trRetrun
        End Function

        Public Sub ReportaError(ByVal nError As String, ByRef ListMsgError As List(Of String), ByRef trRetrun As DesktopConfig.TypeResult, ByRef bRegistroValido As Boolean)
            ListMsgError.Add(nError)
            trRetrun.Result = False
            bRegistroValido = False
        End Sub

        Public Sub ProcesaError(ByVal data As DataTable, ByVal TablaErrores As DataTable, ByRef trRetrun As DesktopConfig.TypeResult)
            Dim bRegistroValido As Boolean
            trRetrun.Result = True
            Dim ListMsgError As New List(Of String)
            Dim resultCargue As New DesktopConfig.ValidacionCargue

            If TablaErrores.Rows.Count > 0 Then

                For Each DetalleError As DataRow In TablaErrores.Rows
                    ReportaError(DetalleError("Detalle").ToString, ListMsgError, trRetrun, bRegistroValido)
                Next

                resultCargue.AdicionConLlavesInexistentes = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 1"))
                resultCargue.ClaseRegistroNoValido = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 2"))
                resultCargue.DevolucionSinCodigoBarras = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 3"))
                resultCargue.EsquemaNoValido = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 4"))
                resultCargue.NuevoConLlavesInexistentes = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 5"))
                resultCargue.NumeroCamposDataNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 6"))
                resultCargue.NumeroLlavesNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 7"))
                resultCargue.TipoDatoCamposDataNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 8"))
                resultCargue.TipoDatoLlavesNoCoincide = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 9"))
                resultCargue.TipoDocumentoNoValido = CInt(TablaErrores.Compute("Count(Tipo)", "Tipo = 10"))
                resultCargue.NoValido = TablaErrores.Rows.Count
                resultCargue.Valido = data.Rows.Count - TablaErrores.Rows.Count

                trRetrun.Resumen = resultCargue
                trRetrun.Parameters = ListMsgError
                trRetrun.Result = False
            End If

        End Sub

        'Private Function CountNoVacios(ByVal row As DataRow) As Integer
        '    Dim nReturn As Integer = 0

        '    For Each Campo In row.ItemArray
        '        If Campo.ToString() <> "" Then
        '            nReturn += 1
        '        End If
        '    Next
        '    Return nReturn            
        'End Function

#End Region

#Region " Metodos "

        'Private Sub AgregaEsquemaFacturacion(ByRef listEsquemas As List(Of Short), ByVal Esquema As Short)
        '    If Not listEsquemas.Contains(Esquema) Then
        '        listEsquemas.Add(Esquema)
        '    End If            
        'End Sub

#End Region

        Private Sub IBindableComponent()
            Throw New NotImplementedException
        End Sub

    End Class

End Namespace