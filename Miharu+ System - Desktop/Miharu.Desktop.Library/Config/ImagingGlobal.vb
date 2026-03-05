Imports DBImaging
Imports DBImaging.SchemaProcess
Imports DBImaging.SchemaCore

Namespace Config

    <Serializable()> _
    Public Class ImagingGlobal
        
        Public UsaDominioExterno As Boolean

        Public Entidad As Short

        Public NombreEntidad As String

        Public Proyecto As Short

        Public LLavesProyecto As List(Of DesktopConfig.LlaveProyecto)

        Public ProyectoImagingRow As SchemaConfig.CTA_ProyectoSimpleType

        Public proyectoOCRRow As DBOCR.SchemaConfig.TBL_Proyecto_OCRSimpleType

        Public ProyectoImagingLlaveDataTable As CTA_Proyecto_Llave_PaqueteDataTable

        Public Esquemas As CTA_EsquemaDataTable

        Public Tipologias As CTA_Tipologias_EsquemaDataTable

    End Class

End Namespace