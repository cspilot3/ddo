Namespace Events

    Public Class EventManager
        Inherits MarshalByRefObject

        ' Declaraciones del evento ValidarPermiso
        Public Delegate Sub ValidarPermisoDelegate(nPermiso As String, nModulo As String, nMensaje As String, ByRef nValido As Boolean)
        Public Event OnValidarPermiso As ValidarPermisoDelegate
        Friend Sub ValidarPermiso(nPermiso As String, nModulo As String, nMensaje As String, ByRef nValido As Boolean)
            RaiseEvent OnValidarPermiso(nPermiso, nModulo, nMensaje, nValido)
        End Sub

        ' Declaraciones del evento ValidarSavePrimeraCaptura
        Public Delegate Sub ValidarSavePrimeraCapturaDelegate(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
        Public Event OnValidarSavePrimeraCaptura As ValidarSavePrimeraCapturaDelegate
        Friend Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            RaiseEvent OnValidarSavePrimeraCaptura(campos, fk_documento, Result)
        End Sub

        ' Declaraciones del evento ValidarSaveSegundaCaptura
        Public Delegate Sub ValidarSaveSegundaCapturaDelegate(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
        Public Event OnValidarSaveSegundaCaptura As ValidarSaveSegundaCapturaDelegate
        Friend Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            RaiseEvent OnValidarSaveSegundaCaptura(campos, fk_documento, Result)
        End Sub

        ' Declaraciones del evento ValidarSaveTerceraCaptura
        Public Delegate Sub ValidarSaveTerceraCapturaDelegate(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
        Public Event OnValidarSaveTerceraCaptura As ValidarSaveTerceraCapturaDelegate
        Friend Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            RaiseEvent OnValidarSaveTerceraCaptura(campos, fk_documento, Result)
        End Sub

        ' Declaraciones del evento ValidarSaveCalidad
        Public Delegate Sub ValidarSaveCalidadDelegate(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
        Public Event OnValidarSaveCalidad As ValidarSaveCalidadDelegate
        Friend Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            RaiseEvent OnValidarSaveCalidad(campos, fk_documento, Result)
        End Sub

        ' Declaraciones del evento FinalizarIndexacion
        Public Delegate Sub FinalizarIndexacionDelegate(ByVal nidCargue As Integer, ByVal nidPaquete As Short)
        Public Event OnFinalizarIndexacion As FinalizarIndexacionDelegate
        Friend Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short)
            RaiseEvent OnFinalizarIndexacion(nidCargue, nidPaquete)
        End Sub

        ' Declaraciones del evento FinalizarReIndexacion
        Public Delegate Sub FinalizarReIndexacionDelegate(ByVal nidCargue As Integer, ByVal nidPaquete As Short)
        Public Event OnFinalizarReIndexacion As FinalizarIndexacionDelegate
        Friend Sub FinalizarReIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short)
            RaiseEvent OnFinalizarReIndexacion(nidCargue, nidPaquete)
        End Sub

        ' Declaraciones del evento FinalizarPreCaptura
        Public Delegate Sub FinalizarPreCapturaDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarPreCaptura As FinalizarPreCapturaDelegate
        Friend Sub FinalizarPreCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarPreCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarPrimeraCaptura
        Public Delegate Sub FinalizarPrimeraCapturaDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarPrimeraCaptura As FinalizarPrimeraCapturaDelegate
        Friend Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarPrimeraCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarSegundaCaptura
        Public Delegate Sub FinalizarSegundaCapturaDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarSegundaCaptura As FinalizarSegundaCapturaDelegate
        Friend Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarSegundaCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarTerceraCaptura
        Public Delegate Sub FinalizarTerceraCapturaDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarTerceraCaptura As FinalizarTerceraCapturaDelegate
        Friend Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarTerceraCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarCalidad
        Public Delegate Sub FinalizarCalidadDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarCalidad As FinalizarCalidadDelegate
        Friend Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarCalidad(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarValidaciones
        Public Delegate Sub FinalizarValidacionesDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarValidaciones As FinalizarValidacionesDelegate
        Friend Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarValidaciones(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento EnviarReproceso
        Public Delegate Sub EnviarReprocesoDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnEnviarReproceso As EnviarReprocesoDelegate
        Friend Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnEnviarReproceso(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarReclasificar
        Public Delegate Sub FinalizarReclasificarDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarReclasificar As FinalizarReclasificarDelegate
        Friend Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarReclasificar(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarRecorte
        Public Delegate Sub FinalizarRecorteDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarRecorte As FinalizarRecorteDelegate
        Friend Sub FinalizarRecorte(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarRecorte(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento FinalizarRecorte
        Public Delegate Sub EliminarImagenDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
        Public Event OnEliminarImagen As EliminarImagenDelegate
        Friend Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
            RaiseEvent OnEliminarImagen(nidExpediente, nidFolder, nidFile)
        End Sub

        ' Declaraciones del evento FinalizarProcesoAdicionalCaptura
        Public Delegate Sub FinalizarProcesoAdicionalCapturaDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Public Event OnFinalizarProcesoAdicionalCaptura As FinalizarProcesoAdicionalCapturaDelegate
        Friend Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            RaiseEvent OnFinalizarProcesoAdicionalCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
        End Sub

        ' Declaraciones del evento ValidarSaveLabelCaptura
        Public Delegate Sub ValidarSaveLabelCapturaDelegate(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal TipoCaptura As String, ByRef Result As Boolean)
        Public Event OnValidarSaveLabelCaptura As ValidarSaveLabelCapturaDelegate
        Friend Sub ValidarSaveLabelCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal TipoCaptura As String, ByRef Result As Boolean)
            RaiseEvent OnValidarSaveLabelCaptura(campos, fk_documento, nidExpediente, nidFolder, nidFile, TipoCaptura, Result)
        End Sub

        ' Declaraciones del evento ValidarActualizarDatoBusqueda
        Public Delegate Sub ValidarActualizarDatoBusquedaDelegate(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByRef Result As Boolean)
        Public Event OnValidarActualizarDatoBusqueda As ValidarActualizarDatoBusquedaDelegate
        Friend Sub ValidarActualizarDatoBusqueda(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByRef Result As Boolean)
            RaiseEvent OnValidarActualizarDatoBusqueda(nidExpediente, nidFolder, nidFile, Result)
        End Sub

        ' Declaraciones del evento FinalizarLoadConfig
        Public Delegate Sub FinalizarLoadConfigDelegate(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow)
        Public Event OnFinalizarLoadConfig As FinalizarLoadConfigDelegate
        Friend Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow)
            RaiseEvent OnFinalizarLoadConfig(IndexerView, FileCoreRow)
        End Sub

        ' Declaraciones del evento FinalizarLoadConfig
        Public Delegate Sub FinalizarLoadConfigAnexoDelegate(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaImaging.TBL_AnexoRow)
        Public Event OnFinalizarLoadConfigAnexo As FinalizarLoadConfigAnexoDelegate
        Friend Sub FinalizarLoadConfigAnexo(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal AnexoImagingRow As DBCore.SchemaImaging.TBL_AnexoRow)
            RaiseEvent OnFinalizarLoadConfigAnexo(IndexerView, AnexoImagingRow)
        End Sub

        ' Declaraciones del evento FinalizarPrimeraCapturaAnexo
        Public Delegate Sub FinalizarPrimeraCapturaAnexoDelegate(ByVal nidAnexo As Long)
        Public Event OnFinalizarPrimeraCapturaAnexo As FinalizarPrimeraCapturaAnexoDelegate
        Friend Sub FinalizarPrimeraCapturaAnexo(ByVal nidAnexo As Long)
            RaiseEvent OnFinalizarPrimeraCapturaAnexo(nidAnexo)
        End Sub

        ' Declaraciones del evento FinalizarTerceraCapturaAnexo
        Public Delegate Sub FinalizarTerceraCapturaAnexoDelegate(ByVal nidAnexo As Long)
        Public Event OnFinalizarTerceraCapturaAnexo As FinalizarTerceraCapturaAnexoDelegate
        Friend Sub FinalizarTerceraCapturaAnexo(ByVal nidAnexo As Long)
            RaiseEvent OnFinalizarTerceraCapturaAnexo(nidAnexo)
        End Sub

    End Class

End Namespace