Imports Slyg.Tools

Namespace Config

    Public Class RiskGlobal

        Public Entidad As Short
        Public Proyecto As Short
        Public CargueUniversal As Boolean
        Public CargueParcial As Boolean
        Public LLavesProyecto As List(Of DesktopConfig.LlaveProyecto)
        Public OT As Integer
        Public Precinto As String
        Public CajaProceso As String
        Public NombreEntidad As String
        Public NombreProyecto As String
        Public ID_CajaCustodia As Integer
        Public CBarras_CajaCustodia As String
        Public Folder_Tipo As Short
        Public CajaXDefecto As Integer
        Public Usa_Cargue_Carpeta As Boolean
        Public Usa_Mesa_Control_Imagenes As Boolean
        Public Usa_Tabla_Fisico As Boolean
        Public Usa_Validacion_Destape As Boolean
        Public Usa_Empaque_Adicion As Boolean


        Public Esquemas As DBArchiving.Schemadbo.CTA_EsquemaDataTable
        Public Tipologias As DBArchiving.Schemadbo.CTA_Tipologias_EsquemaDataTable

        Public EntidadCustodia As SlygNullable(Of Short)
        Public SedeCustodia As SlygNullable(Of Short)
        Public BovedaCustodia As SlygNullable(Of Short)

        Public UsaCentroDistribucion As Boolean

        Public ProyectoImagingRow As DBImaging.SchemaConfig.CTA_ProyectoRow
        Public ProyectoRow As DBArchiving.Schemadbo.CTA_ProyectoRow
        Public ValidacionesMesa As DBArchiving.SchemaCore.CTA_ValidacionDataTable
        Public DatosMesa As DBArchiving.Schemadbo.CTA_Campos_Documentos_MesaDataTable
        Public LLaves As String

        Public Function CTA_Esquema_DBFindByfk_entidadfk_proyectofk_esquema(ByVal fk_Entidad As Short, ByVal fk_Proyecto As Short, ByVal fk_Esquema As Integer) As DataTable
            Dim View As New DataView(Esquemas)
            View.RowFilter = Esquemas.fk_entidadColumn.ColumnName & "=" & CStr(fk_Entidad) _
                             & " AND " & Esquemas.fk_proyectoColumn.ColumnName & "=" & CStr(fk_Proyecto) _
                             & " AND " & Esquemas.fk_esquemaColumn.ColumnName & "=" & CStr(fk_Esquema)

            Return View.ToTable()
        End Function

        Public Function CTA_EsquemaByidEsquema(ByVal id_Esquema As Integer) As DataTable
            Dim viewEsquema As New DataView(Esquemas)
            viewEsquema.RowFilter = Esquemas.fk_esquemaColumn.ColumnName & "=" & id_Esquema
            Return viewEsquema.ToTable()
        End Function

        Public Function ValidacionesMesa_DBFindfkDocumento(ByVal fk_Documento As Integer) As DataTable
            Dim View As New DataView(ValidacionesMesa)
            View.RowFilter = ValidacionesMesa.fk_DocumentoColumn.ColumnName & "=" & CStr(fk_Documento)
            Return View.ToTable()
        End Function

        Public Function DatosMesa_DBFindfkDocumento(ByVal fk_Documento As Integer, ByVal usa_Captura As Boolean) As DataTable
            Dim View As New DataView(DatosMesa)
            View.RowFilter = DatosMesa.fk_DocumentoColumn.ColumnName & "=" & CStr(fk_Documento) & _
                             " AND " & DatosMesa.Usa_CapturaColumn.ColumnName & "=" & CShort(IIf(usa_Captura, 1, 0))
            Return View.ToTable()
        End Function

    End Class

End Namespace