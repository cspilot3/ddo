Namespace Eventos

    Public Interface IEventExecuter

        Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)

        Sub FinalizarCargue(ByVal nidCargue As Integer)

        Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short)

        Sub FinalizarReIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short)

        Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)

        Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)

        Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)

        Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)

        Sub FinalizarPreCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarRecorte(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow)

        Sub CrearFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer)
        Sub ValidarCerrarFechaProcesoNoOT(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String)
        Sub CerrarFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer)
        Sub AbrirFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer)

        Sub CrearOt(ByVal nidOt As Integer)
        Sub CerrarOt(ByVal nidOt As Integer)
        Sub AbrirOt(ByVal nidOt As Integer)

        Sub FinalizarPrecinto()
        Sub GuardarContenedor(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer)
        Sub ValidarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal ndbImaging As DBImaging.DBImagingDataBaseManager, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String)
        Sub FinalizarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidPrecinto As Integer)
        Sub CargarPrecinto(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String)
        Sub FinalizarEliminarPrecinto(ByVal nidOt As Integer, ByVal nidPrecinto As Integer)

        Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String)
        Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
        Sub FinalizarContenedorEmpaque(nidOt As Integer, nidEmpaque As Integer, IdEmpaqueContenedor As Integer)
        Sub FinalizarContenedorEmpaqueEliminar(nidOt As Integer, nToken As String)

        Sub ValidarSaveLabelCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal TipoCaptura As String, ByRef Result As Boolean)
        Sub ValidarActualizarDatoBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal newValor_File_Data As Object, ByRef Result As Boolean)
        Sub FinalizarActualizarDatosBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
        Sub Ejecutar_Cruce_En_Linea(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)

        Sub Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
        Sub Validar_Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidDocumento As Integer, ByVal nidCampo As Integer, ByVal nidCampoTablaAsociada As Integer, ByVal nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String)

        Sub FinalizarCruceGenerico(ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer, ByVal nidOt As Integer)
        Sub FinalizarPrepararDataGenerico(ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer, ByVal nidOt As Integer)

        Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, nfk_Documento As Integer, nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
        Function ExtensionImagen_Plugin(ByVal Entrada As Boolean) As String
        Function IdFormatoImagen_Plugin(ByVal Entrada As Boolean) As Short
        Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String

        Sub FinalizarCorreccionCapturaMaquina(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)

        Sub FinalizarPrimeraCapturaAnexo(ByVal nidAnexo As Long)
        Sub FinalizarTerceraCapturaAnexo(ByVal nidAnexo As Long)
    End Interface

End Namespace