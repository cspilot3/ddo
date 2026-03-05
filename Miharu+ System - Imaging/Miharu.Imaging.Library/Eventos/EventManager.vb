Imports Miharu.Desktop.Library.Config

Namespace Eventos

    Public Class EventManager
        Inherits MarshalByRefObject

#Region " Declaraciones "

        Private _eventExecuterList As New List(Of IEventExecuter)

#End Region

#Region " Propiedades "

        Public Property MessageEliminados As String = ""

        Public ReadOnly Property EventExecuterImaging As List(Of IEventExecuter)
            Get
                Return Me._EventExecuterList
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByVal nEventExecuterImaging As List(Of IEventExecuter))
            If (nEventExecuterImaging Is Nothing) Then
                Throw New Exception("nEventExecuterImaging no puede ser una referencia de Ojeto Nula")
            End If

            Me._EventExecuterList = nEventExecuterImaging
        End Sub

#End Region

#Region " Metodos "

        Public Sub ValidarPermiso(ByVal nPermiso As String, ByVal nModulo As String, ByVal nMensaje As String, ByRef nValido As Boolean)
            nValido = Utilities.ValidarPermiso(nPermiso, nModulo, nMensaje, Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceURL, Program.DesktopGlobal.ClientIPAddress)
        End Sub

        Public Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.EliminarImagen(nidExpediente, nidFolder, nidFile)
            Next
        End Sub

        Public Sub FinalizarCargue(ByVal nidCargue As Integer)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarCargue(nidCargue)
            Next
        End Sub

        Public Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarIndexacion(nidCargue, nidPaquete)
            Next
        End Sub

        Public Sub FinalizarReIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarReIndexacion(nidCargue, nidPaquete)
            Next
        End Sub

        Public Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarSavePrimeraCaptura(campos, fk_documento, Result)
            Next
        End Sub

        Public Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarSaveSegundaCaptura(campos, fk_documento, Result)
            Next
        End Sub

        Public Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarSaveTerceraCaptura(campos, fk_documento, Result)
            Next
        End Sub

        Public Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarSaveCalidad(campos, fk_documento, Result)
            Next
        End Sub

        Public Sub FinalizarPreCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarPreCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarPrimeraCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarSegundaCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarTerceraCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarCalidad(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarValidaciones(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarReproceso(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.EnviarReproceso(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarReclasificar(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Public Sub FinalizarRecorte(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.FinalizarRecorte(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Sub CrearFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.CrearFechaProceso(nidEntidadProcesamiento, nidEntidad, nidProyecto, nidFechaProceso)
            Next
        End Sub

        Sub ValidarCerrarFechaProcesoNoOT(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer, ByRef nValido As Boolean, nValido2 As Boolean, ByRef nMsgError As String)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento, nidEntidad, nidProyecto, nidFechaProceso, nValido, nValido2, nMsgError)
            Next
        End Sub

        Sub CerrarFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.CerrarFechaProceso(nidEntidadProcesamiento, nidEntidad, nidProyecto, nidFechaProceso)
            Next
        End Sub

        Sub AbrirFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer)
            For Each EventExecuter In Me._EventExecuterList
                EventExecuter.AbrirFechaProceso(nidEntidadProcesamiento, nidEntidad, nidProyecto, nidFechaProceso)
            Next
        End Sub

        Sub CrearOt(ByVal nidOt As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.CrearOt(nidOt)
            Next
        End Sub

        Sub CerrarOt(ByVal nidOt As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.CerrarOt(nidOt)
            Next
        End Sub

        Sub AbrirOt(ByVal nidOt As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.AbrirOt(nidOt)
            Next
        End Sub

        Sub FinalizarPrecinto()
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarPrecinto()
            Next
        End Sub

        Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarEmpaque(nidOt, nidEmpaque, nidEsquema, nToken, nValido, nMsgError)
            Next
        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarProcesoAdicionalCaptura(nidExpediente, nidFolder, nidFile, nidVersion)
            Next
        End Sub

        Sub GuardarContenedor(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.GuardarContenedor(nidOt, nidPrecinto, nidContenedor)
            Next
        End Sub

        Sub ValidarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal ndbImaging As DBImaging.DBImagingDataBaseManager, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarPrecintoEmpaque(nidOt, nidEmpaque, ndbImaging, nContenedorDesktop, nValido, nMsgError)
            Next
        End Sub

        Sub FinalizarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarPrecintoEmpaque(nidOt, nidEmpaque)
            Next
        End Sub

        Sub CargarPrecinto(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.CargarPrecinto(nidOt, nidEmpaque, nContenedorDesktop, nValido, nMsgError)
            Next
        End Sub

        Sub FinalizarEliminarPrecinto(ByVal nidOt As Integer, ByVal nidEmpaque As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarEliminarPrecinto(nidOt, nidEmpaque)
            Next
        End Sub

        Public Sub ValidarSaveLabelCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal TipoCaptura As String, ByRef Result As Boolean)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarSaveLabelCaptura(campos, fk_documento, nidExpediente, nidFolder, nidFile, TipoCaptura, Result)
            Next
        End Sub

        Public Sub ValidarActualizarDatoBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal newValor_File_Data As Object, ByRef Result As Boolean)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.ValidarActualizarDatoBusqueda(nCampo, nidExpediente, nidFolder, nidFile, newValor_File_Data, Result)
            Next
        End Sub

        Public Sub FinalizarActualizarDatosBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarActualizarDatosBusqueda(nCampo, nidExpediente, nidFolder, nidFile)
            Next
        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.Ejecutar_Cruce_En_Linea(nCampo, nidExpediente, nidFolder, nidFile)
            Next
        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarLoadConfig(IndexerView, FileCoreRow)
            Next
        End Sub

        Sub FinalizarContenedorEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal IdEmpaqueContenedor As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarContenedorEmpaque(nidOt, nidEmpaque, IdEmpaqueContenedor)
            Next
        End Sub

        Sub FinalizarContenedorEmpaqueEliminar(ByVal nidOt As Integer, ByVal ntoken As String)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarContenedorEmpaqueEliminar(nidOt, ntoken)
            Next
        End Sub

        Public Sub Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.Reprocesar(nidExpediente, nidFolder, nidFile)
            Next
        End Sub

        Public Sub Validar_Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidDocumento As Integer, ByVal nidCampo As Integer, ByVal nidCampoTablaAsociada As Integer, ByVal nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.Validar_Reprocesar(nidExpediente, nidFolder, nidDocumento, nidCampo, nidCampoTablaAsociada, nesCampo, nValido, nMsgError)
            Next
        End Sub

        Public Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
            For Each EventExecuter In Me._eventExecuterList
                Return EventExecuter.Nombre_Imagen_Exportar(nidExpediente, nidFolder, nidFile, nFk_Documento, nGrupo, nValido, nMsgError)
            Next
            Return Nothing
        End Function

        Public Function ExtensionImagen_Plugin(ByVal Entrada As Boolean) As String
            For Each EventExecuter In Me._eventExecuterList
                Return EventExecuter.ExtensionImagen_Plugin(Entrada)
            Next
            Return Nothing
        End Function

        Public Function IdFormatoImagen_Plugin(ByVal Entrada As Boolean) As Short
            For Each EventExecuter In Me._eventExecuterList
                Return EventExecuter.IdFormatoImagen_Plugin(Entrada)
            Next
            Return Nothing
        End Function

        Sub FinalizarCruceGenerico(ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer, ByVal nidOT As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarCruceGenerico(nidEntidad, nidProyecto, nidFechaProceso, nidOT)
            Next
        End Sub

        Public Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
            For Each EventExecuter In Me._eventExecuterList
                Return EventExecuter.Nombre_Imagen_Agrupada_Exportar(nidExpediente, nidFolder, nGrupo, nValido, nMsgError)
            Next
            Return Nothing
        End Function

        Public Sub FinalizarPrimeraCapturaAnexo(ByVal nidAnexo As Long)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarPrimeraCapturaAnexo(nidAnexo)
            Next
        End Sub

        Public Sub FinalizarTerceraCapturaAnexo(ByVal nidAnexo As Long)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarTerceraCapturaAnexo(nidAnexo)
            Next
        End Sub

        Sub FinalizarPrepararDataGenerico(ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer, ByVal nidOT As Integer)
            For Each EventExecuter In Me._eventExecuterList
                EventExecuter.FinalizarPrepararDataGenerico(nidEntidad, nidProyecto, nidFechaProceso, nidOT)
            Next
        End Sub
#End Region

#Region " Funciones "

        Public Overrides Function InitializeLifetimeService() As Object
            Return Nothing
        End Function

#End Region


    End Class

End Namespace