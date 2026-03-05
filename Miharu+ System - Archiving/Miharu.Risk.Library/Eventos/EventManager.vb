Namespace Eventos

    Public Class EventManager

        Public Shared Reingreso As Short
        Public Shared RegTipoReingreso As Boolean = False

        'Declaraciones del evento FinalizarDestape
        Public Delegate Sub ValidarDestapeDelegate(ByVal nidExpediente As Long, ByRef nCancel As Boolean, ByRef nNewEstado As Short)
        Public Shared Event OnValidarDestape As ValidarDestapeDelegate

        Public Shared Sub ValidarDestape(ByVal nidExpediente As Long, ByRef nCancel As Boolean, ByRef nNewEstado As Short)
            RaiseEvent OnValidarDestape(nidExpediente, nCancel, nNewEstado)
        End Sub

        'Declaraciones del evento FinalizarDestape
        Public Delegate Sub FinalizarDestapeDelegate(ByVal nidPrecinto As String)
        Public Shared Event OnFinalizarDestape As FinalizarDestapeDelegate

        Public Shared Sub FinalizarDestape(ByVal nidPrecinto As String)
            RaiseEvent OnFinalizarDestape(nidPrecinto)
        End Sub


        'Declaraciones del evento FinalizarPrecinto
        Public Delegate Sub FinalizarPrecintoDelegate(ByVal nidPrecinto As String)
        Public Shared Event OnFinalizarPrecinto As FinalizarPrecintoDelegate

        Public Shared Sub FinalizarPrecinto(ByVal nidPrecinto As String)
            RaiseEvent OnFinalizarPrecinto(nidPrecinto)
        End Sub



        'Declaraciones del evento ActualizaSobrantes
        Public Delegate Sub ActualizaSobrantesDelegate(ByVal nfk_Expediente As Long, ByVal nfk_Folder As Integer, ByVal nfk_OT As Integer, ByVal nfk_Usuario As Integer, ByVal nfk_Estado As Integer, ByVal nfk_Precinto As String, ByVal nfk_Caja_Proceso As Integer, ByVal llave1 As String, ByVal llave2 As String, ByRef ndmArchiving As DBArchiving.DBArchivingDataBaseManager)
        Public Shared Event OnActualizaSobrantes As ActualizaSobrantesDelegate

        Public Shared Sub ActualizaSobrantes(ByVal nfk_Expediente As Long, ByVal nfk_Folder As Integer, ByVal nfk_OT As Integer, ByVal nfk_Usuario As Integer, ByVal nfk_Estado As Integer, ByVal nfk_Precinto As String, ByVal nfk_Caja_Proceso As Integer, ByVal nllave1 As String, ByVal nllave2 As String, ByRef ndmArchiving As DBArchiving.DBArchivingDataBaseManager)
            RaiseEvent OnActualizaSobrantes(nfk_Expediente, nfk_Folder, nfk_OT, nfk_Usuario, nfk_Estado, nfk_Precinto, nfk_Caja_Proceso, nllave1, nllave2, ndmArchiving)
        End Sub

        ''Declaraciones del evento ValidarReingreso
        Public Delegate Sub ValidarReingresoDelegate(ByVal nfk_Expediente As Long, ByVal nfk_Usuario As Integer, ByVal llave1 As String, ByVal llave2 As String)
        Public Shared Event OnValidarReingreso As ValidarReingresoDelegate

        Public Shared Sub ValidarReingreso(ByVal nfk_Expediente As Long, ByVal nfk_Usuario As Integer, ByVal nllave1 As String, ByVal nllave2 As String)
            RaiseEvent OnValidarReingreso(nfk_Expediente, nfk_Usuario, nllave1, nllave2)
        End Sub


        'Declaraciones del evento InsertaSobrantes
        Public Delegate Sub InsertaSobrantesDelegate(ByVal nLlave1 As String, ByVal nLLave2 As String)
        Public Shared Event OnInsertaSobrantes As InsertaSobrantesDelegate

        Public Shared Sub InsertaSobrantes(ByVal nLlave1 As String, ByVal nLLave2 As String)
            RaiseEvent OnInsertaSobrantes(nLlave1, nLLave2)
        End Sub

        'Declaraciones del evento InsertaLogEstados
        Public Delegate Sub InsertaLogEstadosDelegate(ByVal nValorFiltro1 As String, ByVal nfk_Modulo As Integer, ByVal nfk_Estado As Integer, ByVal nTipoEstadoActualizar As Short)
        Public Shared Event OnInsertaLogEstados As InsertaLogEstadosDelegate

        Public Shared Sub InsertaLogEstados(ByVal nValorFiltro As String, ByVal nfk_Modulo As Integer, ByVal nfk_Estado As Integer, ByVal nTipoEstadoActualizar As Short)
            RaiseEvent OnInsertaLogEstados(nValorFiltro, nfk_Modulo, nfk_Estado, nTipoEstadoActualizar)
        End Sub

    End Class

End Namespace