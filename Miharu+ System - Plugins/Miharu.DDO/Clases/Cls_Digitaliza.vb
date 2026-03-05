Imports Miharu.Desktop.Library.Config
Imports QueryResponse = Miharu.Desktop.Library.MiharuDMZ.QueryResponse
Imports ClientUtil = Miharu.Desktop.Library.MiharuDMZ.ClientUtil
Imports QueryParameter = Miharu.Desktop.Library.MiharuDMZ.QueryParameter
Imports QueryResponseType = Miharu.Desktop.Library.MiharuDMZ.QueryResponseType
Imports QueryRequestType = Miharu.Desktop.Library.MiharuDMZ.QueryRequestType

Public Class Cls_Digitaliza

    Protected ListaItemsDataTable As DataTable

    Public Function ConsultaSiriesDocumentales() As DataTable
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Esquema_Lista_Get]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(Program.ImagingGlobal.Proyecto).ToString()}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return queryResponseListaItems.dataTable

        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: Consulta Series Documentales")
            Return Nothing
        End Try

    End Function

    Public Function ConsultaEsquemaCampo(idEsquema As Integer, fk_Entidad As Integer, fk_Proyecto As Integer) As DataTable
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Campos_Esquema_Get]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Esquema", .value = CInt(idEsquema).ToString()},
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return queryResponseListaItems.dataTable
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: ConsultaEsquemaCampo")
            Return Nothing
        End Try

    End Function

    Public Function ConsultaEsquemaCampolista(fk_Entidad As Integer, id_Campo_Lista As Integer) As DataTable
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Campos_Esquema_Lista_Get]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "id_Campo_Lista", .value = CInt(id_Campo_Lista).ToString()}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return queryResponseListaItems.dataTable
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: ConsultaEsquemaCampoLista")
            Return Nothing
        End Try

    End Function

    Public Function ConsultaParametroSistema(fk_Entidad As Integer, fk_Proyecto As Integer, Nombre_Parametro_Sistema As String) As Integer
        Try
            Dim queryResponseCampoInactividad As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Consulta_Parametro_Sistema_Get]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
                                      New QueryParameter With {.name = "Nombre_Parametro_Sistema", .value = Trim(Nombre_Parametro_Sistema)}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return CInt(queryResponseCampoInactividad.dataTable.Rows(0).Item("Valor_Parametro_Sistema"))
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: ConsultaParametroSistema")
            Return Nothing
        End Try

    End Function

    Public Function FileTransferDDO(fk_Entidad As Integer, fk_Proyecto As Integer, Nombre_Parametro_Sistema As String) As Integer
        Try
            Dim queryResponseCampoInactividad As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Consulta_Parametro_Sistema_Get]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
                                      New QueryParameter With {.name = "Nombre_Parametro_Sistema", .value = Trim(Nombre_Parametro_Sistema)}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return CInt(queryResponseCampoInactividad.dataTable.Rows(0).Item("Valor_Parametro_Sistema"))
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: FileTransferDDO")
            Return Nothing
        End Try

    End Function

    Public Function RegistrarLoteaDigitalizar(fk_Entidad As Integer, fk_Proyecto As Integer, IdEsquema As Integer, Esquema As String, Lote As String, FechaProceso As String, FechaDigitaliza As String, FechaCargue As String, Jornada As String, Oficina As Integer, NomOficina As String, NumLabel As String, Imagenes As Integer, NombreMaquina As String, Funcionario As String, EstadoLote As String, Eliminado As Integer) As DataTable
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_ControlLoteDitalizado_set]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
                                      New QueryParameter With {.name = "fk_Esquema", .value = CInt(IdEsquema).ToString()},
                                      New QueryParameter With {.name = "Esquema", .value = Trim(Esquema)},
                                      New QueryParameter With {.name = "Lote", .value = Trim(Lote)},
                                      New QueryParameter With {.name = "FechaProceso", .value = Trim(FechaProceso)},
                                      New QueryParameter With {.name = "FechaDigitalizacion", .value = Trim(FechaDigitaliza)},
                                      New QueryParameter With {.name = "FechaCargue", .value = Trim(FechaCargue)},
                                      New QueryParameter With {.name = "Oficina", .value = CInt(Oficina).ToString()},
                                      New QueryParameter With {.name = "NomOficina", .value = Trim(NomOficina)},
                                      New QueryParameter With {.name = "NumLabel", .value = Trim(NumLabel)},
                                      New QueryParameter With {.name = "Jornada", .value = Trim(Jornada)},
                                      New QueryParameter With {.name = "Imagenes", .value = CInt(Imagenes).ToString()},
                                      New QueryParameter With {.name = "PcName", .value = NombreMaquina.ToString()},
                                      New QueryParameter With {.name = "Funcionario", .value = Trim(Funcionario)},
                                      New QueryParameter With {.name = "EstadoLote", .value = Trim(EstadoLote)},
                                      New QueryParameter With {.name = "FechaMovimiento", .value = Trim(FechaMvto)},
                                      New QueryParameter With {.name = "Eliminado", .value = CBool(Eliminado).ToString()}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return queryResponseListaItems.dataTable
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: RegistrarLoteaDigitalizar")
            Return Nothing
        End Try

    End Function

    Public Function ActualizarLoteDigitalizacion(fk_Entidad As Integer, fk_Proyecto As Integer, Lote As String, FechaCargue As String, Imagenes As Integer, EstadoLote As String, Eliminado As Integer) As DataTable
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_ControlLoteEstado_set]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
                                      New QueryParameter With {.name = "Lote", .value = Trim(Lote)},
                                      New QueryParameter With {.name = "FechaCargue", .value = Trim(FechaCargue)},
                                      New QueryParameter With {.name = "Imagenes", .value = CInt(Imagenes).ToString()},
                                      New QueryParameter With {.name = "EstadoLote", .value = Trim(EstadoLote)},
                                      New QueryParameter With {.name = "Eliminado", .value = CBool(Eliminado).ToString()}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return queryResponseListaItems.dataTable
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: ActualizarLoteDigitalizacion")
            Return Nothing
        End Try

    End Function

    Public Function ConsultarLoteTransferido(fk_Entidad As Integer, fk_Proyecto As Integer, Lote As String) As DataTable
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_ControlLoteEstado_get]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                      New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
                                      New QueryParameter With {.name = "Lote", .value = Trim(Lote)}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

            Return queryResponseListaItems.dataTable
        Catch ex As Exception
            RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, "Cls_Digitaliza", Environment.MachineName, "Accion: ConsultarLoteTransferido")
            Return Nothing
        End Try

    End Function

    Public Sub RegistrarError(Mensaje As String, StackTrace As String, Usuario As String, Formulario As String, PcName As String, Contexto As String)
        Try
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_InsertarLogError]", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "Mensaje", .value = Trim(Mensaje).ToString()},
                                      New QueryParameter With {.name = "StackTrace", .value = Trim(StackTrace).ToString()},
                                      New QueryParameter With {.name = "Usuario", .value = Trim(Usuario).ToString()},
                                      New QueryParameter With {.name = "Formulario", .value = Trim(Formulario).ToString()},
                                      New QueryParameter With {.name = "PcName", .value = Trim(PcName).ToString()},
                                      New QueryParameter With {.name = "Contexto", .value = Trim(Contexto)}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
        Catch ex As Exception
            'Return Nothing
        End Try

    End Sub

End Class
