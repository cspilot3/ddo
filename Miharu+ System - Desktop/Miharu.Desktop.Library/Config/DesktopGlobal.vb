Namespace Config

    <Serializable()> _
    Public Class DesktopGlobal

#Region " Declaraciones "

        Private _sesionId As Guid

#End Region

#Region " Constructores "

        Public Sub New()
            _SesionID = Guid.NewGuid()
            PCName = Environment.MachineName
        End Sub

#End Region

#Region " Propiedades "

        Public Property ConnectionStrings As DesktopConfig.TypeConnectionString

        Public Property ConnectionURLStrings As DesktopConfig.TypeConnectionOCRURLsString

        Public Property OCRParameterStrings As DesktopConfig.TypeOCRParameterString

        Public Property CentroProcesamientoRow As DBCore.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType

        Public Property PuestoTrabajoRow As DBSecurity.SchemaConfig.TBL_Puesto_TrabajoSimpleType

        Public Property BovedaRow As DBCore.SchemaCustody.TBL_BovedaSimpleType

        Public Property IdentifierDateFormat As String

        Public Property ServidorImagenRow As DBCore.SchemaImaging.TBL_ServidorSimpleType

        Property PcName As String

        Public ReadOnly Property SesionId As Guid
            Get
                Return _sesionId
            End Get
        End Property

        Property SecurityServiceUrl As String

        Property ClientIpAddress As String

#End Region

    End Class

End Namespace