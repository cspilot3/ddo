Imports DBArchiving
Imports Slyg.Tools

Namespace Config

    <Serializable()> _
    Public Class DesktopConfig

#Region " OCR Module Settings "

        Public Class ModuloOCRParameters
            Public Shared ReadOnly PathOUTData As String = "Path_OUT_Data"
            Public Shared ReadOnly ProcessMethodOCR As String = "Process_Method_OCR"
            Public Shared ReadOnly ApplyWordConfidence As String = "Apply_Word_Confidence"
            Public Shared ReadOnly MinWordConfidence As String = "Min_Word_Confidence"
        End Class

        Public Class ModuloOCRURLs
            Public Shared ReadOnly CoordinateTable As String = "coordinate-table"
            Public Shared ReadOnly Rectangle As String = "rectangle"
            Public Shared ReadOnly DetectTable As String = "detect-table"
            Public Shared ReadOnly CorrectSesgoImage As String = "correct-sesgo-image"
            Public Shared ReadOnly PreprocessImage As String = "preprocess_image"
            Public Shared ReadOnly CleanDataTable As String = "clean-data-table"
            Public Shared ReadOnly DeleteLinesImage As String = "delete_lines_image"
            Public Shared ReadOnly GetFileHOCRContent As String = "get_File_hocrContent"
            Public Shared ReadOnly ValuePageOCR As String = "get_Page_Data_Image_OCR"
        End Class

#End Region

#Region " Estructuras "

        <Serializable()> _
        Public Structure Folder
            Dim Existe As Boolean

            Dim fk_Expediente As Long
            Dim id_Folder As Short
            Dim fk_Esquema As Short
            Dim fk_Ot As Integer
            Dim CBarras_Folder As String

            Dim Es_File As Boolean
            Dim CBarras_File As String

            Dim EstadoCargado As Boolean
            Dim OTEstadoCargado As Short
            Dim PrecintoCargado As String

            Dim EstadoDestapado As Boolean
            Dim OTEstadoDestapado As Short
            Dim PrecintoDestapado As String

            Dim EstadoReproceso As Boolean
            Dim OTEstadoReproceso As Short
            Dim PrecintoReproceso As String

            'Dim OTs As List(Of String)
            Dim Folder As Schemadbo.CTA_Folder_llavesSimpleType
            Dim EstadoCore As SchemaCore.CTA_Folder_EstadoSimpleType
        End Structure

        <Serializable()> _
        Public Structure StrLlaves
            Dim id_Llave As Integer
            Dim Nombre_Llave As String
            Dim Valor_Llave As String
            Dim Tipo As CampoTipo
        End Structure

        <Serializable()> _
        Public Structure CampoDocumento
            Dim Id_Campo As Short
            Dim fk_Campo_Tipo As Byte
            Dim fk_Campo_Lista As SlygNullable(Of Short)
            Dim fk_Proyecto_Llave As SlygNullable(Of Short)
            Dim fk_Documento As Integer
            Dim Nombre_Campo As String
            Dim EsLlave As Boolean
            Dim Valor_Campo As Object
        End Structure

        <Serializable()> _
        Public Structure CrearControlesType
            Public Label As String
            Public LabelWidth As Integer
            Public NombreCampo As String
            Public Tipo As CampoTipo
            Public Width As Integer
            Public MinLength As Short
            Public MaxLength As Short
            Public CampoLista As Short
            Public Existe_File_Data As Boolean
            Public File_Data As Object
            Public File_Data_PrimeraCaptura As Object
            Public File_Data_SegundaCaptura As Object
            Public fk_Entidad As Short
            Public fk_Documento As Integer
            Public fk_Campo As Short
            Public fk_Validacion As Short
            Public Obligatorio As Boolean
            Public Usa_Decimales As Boolean
            Public Caracter_Decimal As Char
            Public Cantidad_Decimales As Short
            Public Es_Modificable As Boolean
            Public Es_Campo_Control As Boolean
        End Structure

        <Serializable()> _
        Public Structure DatosCore
            Public Existe As Boolean
            Public Expediente As String
            Public Folder As String
        End Structure

        <Serializable()> _
        Public Structure LlaveProyecto
            Public Id As Short
            Public Nombre As String
            Public Tipo As CampoTipo
        End Structure

        <Serializable()> _
        Public Structure AtributesCBarras
            Public Label As String
            Public Valor As String
        End Structure

        <Serializable()> _
        Public Structure FilesEstadoCargue
            Public Ot As Integer
            Public IdFile As String
            Public FilesDisponibles As Boolean
            Public AceptaSobrantes As Boolean
            Public Cbarras As String
        End Structure

        <Serializable()> _
        Public Structure TypeConnectionString
            Public Security As String
            Public Archiving As String
            Public Core As String
            Public Imaging As String
            Public Integration As String
            Public OCR As String
            Public Tools As String
            Public Softrac As String
            Public Core_Risks As String
            Public Imaging_Risks As String
            Public Archiving_Risks As String
        End Structure

        <Serializable()> _
        Public Structure TypeConnectionOCRURLsString
            Public CoordinateTable As String
            Public Rectangle As String
            Public DetectTable As String
            Public CorrectSesgoImage As String
            Public PreprocessImage As String
            Public CleanDataTable As String
            Public DeleteLinesImage As String
            Public GetFileHOCRContent As String
            Public ValuePageOCR As String
        End Structure

        <Serializable()> _
        Public Structure TypeOCRParameterString
            Public PathOUTData As String
            'Public ProcessMethodOCR As Integer
            Public ApplyWordConfidence As Boolean
            Public MinWordConfidence As Integer
        End Structure

        <Serializable()> _
        Public Structure LlaveExpediente
            Public Id_Llave_Proyecto As Short
            Public Valor As Object
            Public Tipo As Integer
        End Structure

        <Serializable()> _
        Public Structure FolderCORE
            Public Expediente As Long
            Public Folder As Short
            Public CBarras As String
        End Structure

        <Serializable()> _
        Public Structure FolderCOREOT
            Public Expediente As Long
            Public Folder As Short
            Public CBarras As String
            Public OT As Integer
        End Structure

        <Serializable()> _
        Public Structure Devolucion
            Public PermiteDevolucion As Boolean
            Public OT As Integer
            Public File As Short
            Public LineaProceso As Integer
        End Structure

        <Serializable()> _
        Public Structure TypeResult
            Public Result As Boolean
            Public Parameters As List(Of String)
            Public Resumen As ValidacionCargue
            Public OT As SlygNullable(Of Integer)
            Public Cargue As SlygNullable(Of Integer)
        End Structure

        <Serializable()> _
        Public Structure TypeResultDataDate
            Public Result As Boolean
            Public tablaResultados As DataTable
            Public Parameters As List(Of String)
        End Structure

        <Serializable()> _
        Public Structure ValidacionCargue
            Public Valido As Integer
            Public NoValido As Integer

            Public EsquemaNoValido As Integer
            Public TipoDocumentoNoValido As Integer
            Public ClaseRegistroNoValido As Integer
            Public DevolucionSinCodigoBarras As Integer
            Public AdicionConLlavesInexistentes As Integer
            Public NuevoConLlavesInexistentes As Integer
            Public NumeroLlavesNoCoincide As Integer
            Public TipoDatoLlavesNoCoincide As Integer
            Public NumeroCamposDataNoCoincide As Integer
            Public TipoDatoCamposDataNoCoincide As Integer

        End Structure

        <Serializable()> _
        Public Structure ColoresEstado
            Public Shared NoAplica As Drawing.Color = Drawing.Color.IndianRed
            Public Shared PendienteProcesar As Drawing.Color = Drawing.Color.LightYellow
            Public Shared Procesado As Drawing.Color = Drawing.Color.SeaGreen
            Public Shared ProcesoSuperior As Drawing.Color = Drawing.Color.LightGray
        End Structure

        <Serializable()> _
        Public Structure LlavePosicion
            Dim Id_Llave As Short
            Dim Nombre_Llave As String
            Dim Posicion_Inicial As Short
            Dim Posicion_Longitud As Short
        End Structure

        <Serializable()> _
        Public Class ProductividadType
            Public Usuario As Integer
            Public Fecha As SlygNullable(Of Date)
            Public Documentos As Long
            Public Campos As Long
            Public Caracteres As Long
            Public CamposLlave As Long
            Public Validaciones As Long

            Public Sub New()
                Me.Usuario = 0
                Me.Fecha = Nothing
                Me.Documentos = 0
                Me.Campos = 0
                Me.Caracteres = 0
                Me.Validaciones = 0
            End Sub
        End Class

#End Region

#Region " Enumeraciones "

        Enum Consecutivo As Short
            CBarras = 0
            OTs = 1
            Cajas = 2
            Movimientos = 3
        End Enum

        Public Enum TipoCargue As Byte
            Universal = 1
            Parcial = 2
            Actualizacion = 3
            Prevalidar = 4
            Deceval = 5
        End Enum

        Public Enum RegistroTipo As Byte
            Adicion = 1
            Devolucion = 2
            Nuevo = 3
            Reingreso = 4
        End Enum

        Enum TipoFileCargue
            ConCargueSobrante
            ConCargueReproceso
            SinCargue
            ConCargue
        End Enum

        Public Enum Modulo As Byte
            Security = 0
            Imaging = 2
            Core = 6
            Archiving = 7
            PunteoBanAgrario = 13
            Tools = 3
            Integration = 30
            Softrac = 35
            Core_Risks = 38
            Imaging_Risks = 39
            Archiving_Risks = 40
            OCR = 41
        End Enum

        'Public Enum ModuloOCRURLs As Byte
        '    CoordinateTable = 1
        '    Rectangle = 2
        '    DetectTable = 4
        '    CorrectSesgoImage = 5
        '    PreprocessImage = 6
        '    CleanDataTable = 7
        '    DeleteLinesImage = 8
        'End Enum

        Public Enum SolicitudTipo As Short
            Fotocopia = 1
            Fax = 2
            Prestamo = 3
            RevisionEnBoveda = 4
            Retiro = 5
            Imagen = 6
        End Enum

        Public Enum CampoTipo As Byte
            Texto = 1
            Numerico = 2
            Fecha = 3
            SiNo = 4
            Lista = 5
            ListaEnlazada = 6
            Query = 8
            TablaAsociada = 9
        End Enum

        Public Enum TipoPermiso
            CONSULTA = 1
            AGREGAR = 2
            EDITAR = 3
            ELIMINAR = 4
            EXPORTAR = 5
            IMPRIMIR = 6
        End Enum

        Public Enum FolderEstado
            Pendiente = 1
            Recibido = 2
            Sobrante = 3
            Cancelado = 4
        End Enum

        Public Enum TipoCaptura As Short
            Primera_Captura = 1
            Segunda_Captura = 2
            Tercera_Captura = 3
        End Enum

        Public Enum MotivoReproceso As Short
            N_A = 0
            ValidacionesObligatorias = 1
            FoldersSobrantes = 2
            FilesSobrantes = 3
            FaltantesObligatorios = 4
            ValidacionesDestapeObligatorias = 5
        End Enum

        Public Enum Servicio_Facturacion As Short
            Garantías = 1
            Custodia = 2

            Cargue_Parcial = 4
            Cargue_Universal = 5
            Cargue_Actualizacion = 35

            Recepción = 6
            Destape = 7

            Primera_Captura_Carpeta = 9
            Primera_Captura_Documento = 17
            Primera_Captura_Campo = 18
            Primera_Captura_Caracter = 19
            Primera_Captura_Validacion = 20

            Primera_Captura_Devolucion_Documento = 29
            Primera_Captura_Devolucion_Campo = 30
            Primera_Captura_Devolucion_Caracter = 31
            Primera_Captura_Validacion_Devolucion = 32

            Segunda_Captura_Carpeta = 10
            Segunda_Captura_Documento = 21
            Segunda_Captura_Campo = 22
            Segunda_Captura_Caracter = 23
            Segunda_Captura_Validacion = 24

            Empaque = 12
            Centro_de_Distribución = 13
            Recepción_Centro_Distribucion = 14
            Atención_Solicitudes = 15
            Despacho_Solicitudes = 16
        End Enum

        Public Enum Tipo_Inconsistencia As Short
            Campo_Inconsistente = 1
            Validacion_Inconsitente = 2
        End Enum

        Public Enum Tipo_Productividad As Short
            Documento = 1
            Campo = 2
            Caracter = 3
            Validacion = 4
        End Enum

        Public Enum Etapa_Productividad As Byte
            Indexacion = 1
            Validaciones = 2
            Primera_Captura = 3
            Segunda_Captura = 4
            Tercera_Captura = 5
            Errores_cometidos = 6
            PreCaptura = 7
            CalidadCaptura = 8
            Recortes = 9
            CalidadRecortes = 10
            ProcesoAdicional_Captura = 11
        End Enum

        Public Enum FormatoImagenEnum
            BMP = 1
            GIF = 2
            JPEG = 3
            PDF = 4
            PNG = 5
            TIFF_Color = 6
            TIFF_Bitonal = 7
        End Enum

        Public Enum BCSTiposLog
            CTACORaammddN = 1
            CTACORaammddA = 2
            Bcsspl18ddmmaaaa = 3
            Microfinanza = 4
            Acuse_recibido = 5
            Goro_Volumen = 6
        End Enum

#End Region

    End Class

End Namespace