Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Imaging.Library.Eventos

Namespace Imaging.Hipotecarios
    Public Class EventExecuterHipotecarios
        Inherits EventExecuter
        Implements IEventExecuter

#Region " Declaraciones "

        ' ReSharper disable once NotAccessedField.Local
        Private _Plugin As HipotecariosPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As HipotecariosPlugin)
            Me._Plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "
        Public Sub AbrirFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.AbrirOt

        End Sub

        Public Sub CargarPrecinto(nidOt As Integer, nidPrecinto As Integer, nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CargarPrecinto

        End Sub

        Public Sub CerrarFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CerrarFechaProceso

        End Sub

        Public Sub CerrarOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CerrarOt

        End Sub

        Public Sub CrearFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CrearFechaProceso

        End Sub

        Public Sub CrearOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CrearOt

        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Ejecutar_Cruce_En_Linea

        End Sub

        Public Sub EliminarImagen(nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.EliminarImagen

        End Sub

        Public Sub EnviarReproceso(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.EnviarReproceso

        End Sub

        Public Function ExtensionImagen_Plugin(Entrada As Boolean) As String Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ExtensionImagen_Plugin

        End Function

        Public Sub FinalizarActualizarDatosBusqueda(nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarActualizarDatosBusqueda

        End Sub

        Public Sub FinalizarCalidad(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCalidad

        End Sub

        Public Sub FinalizarCargue(nidCargue As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCargue

        End Sub

        Public Sub FinalizarContenedorEmpaque(nidOt As Integer, nidEmpaque As Integer, IdEmpaqueContenedor As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarContenedorEmpaque

        End Sub

        Public Sub FinalizarContenedorEmpaqueEliminar(nidOt As Integer, nToken As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarContenedorEmpaqueEliminar

        End Sub

        Public Sub FinalizarCorreccionCapturaMaquina(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCorreccionCapturaMaquina

        End Sub

        Public Sub FinalizarCruceGenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCruceGenerico

        End Sub

        Public Sub FinalizarEliminarPrecinto(nidOt As Integer, nidPrecinto As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarEliminarPrecinto

        End Sub

        Public Sub FinalizarIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarIndexacion

        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, FileCoreRow As DBCore.SchemaProcess.TBL_FileRow) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarLoadConfig

        End Sub

        Public Sub FinalizarPreCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPreCaptura

        End Sub

        Public Sub FinalizarPrecinto() Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrecinto

        End Sub

        Public Sub FinalizarPrecintoEmpaque(nidOt As Integer, nidPrecinto As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrecintoEmpaque

        End Sub

        Public Sub FinalizarPrimeraCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrimeraCaptura

        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarProcesoAdicionalCaptura

        End Sub

        Public Sub FinalizarReclasificar(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReclasificar

        End Sub

        Public Sub FinalizarRecorte(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarRecorte

        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion

        End Sub

        Public Sub FinalizarReproceso(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReproceso

        End Sub

        Public Sub FinalizarSegundaCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarSegundaCaptura

        End Sub

        Public Sub FinalizarTerceraCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCaptura

        End Sub

        Public Sub FinalizarValidaciones(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarValidaciones

        End Sub

        Public Sub GuardarContenedor(nidOt As Integer, nidPrecinto As Integer, nidContenedor As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.GuardarContenedor

        End Sub

        Public Function IdFormatoImagen_Plugin(Entrada As Boolean) As Short Implements Miharu.Imaging.Library.Eventos.IEventExecuter.IdFormatoImagen_Plugin

        End Function

        Public Function Nombre_Imagen_Agrupada_Exportar(nidExpediente As Long, nidFolder As Short, nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Nombre_Imagen_Agrupada_Exportar

        End Function

        Public Function Nombre_Imagen_Exportar(nidExpediente As Long, nidFolder As Short, nidFile As Short, nfk_Documento As Integer, nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Nombre_Imagen_Exportar

        End Function

        Public Sub Reprocesar(nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Reprocesar

        End Sub

        Public Sub Validar_Reprocesar(nidExpediente As Long, nidFolder As Short, nidDocumento As Integer, nidCampo As Integer, nidCampoTablaAsociada As Integer, nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Validar_Reprocesar

        End Sub

        Public Sub ValidarActualizarDatoBusqueda(nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, nidExpediente As Long, nidFolder As Short, nidFile As Short, newValor_File_Data As Object, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarActualizarDatoBusqueda

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT

        End Sub

        Public Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarEmpaque

        End Sub

        Public Sub ValidarPrecintoEmpaque(nidOt As Integer, nidPrecinto As Integer, ndbImaging As DBImaging.DBImagingDataBaseManager, nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarPrecintoEmpaque

        End Sub

        Public Sub ValidarSaveCalidad(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveCalidad

        End Sub

        Public Sub ValidarSaveLabelCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, nidExpediente As Long, nidFolder As Short, nidFile As Short, TipoCaptura As String, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveLabelCaptura

        End Sub

        Public Sub ValidarSavePrimeraCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSavePrimeraCaptura

        End Sub

        Public Sub ValidarSaveSegundaCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveSegundaCaptura

        End Sub

        Public Sub ValidarSaveTerceraCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveTerceraCaptura

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

