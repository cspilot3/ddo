Imports Slyg.Tools
Imports DBImaging.SchemaConfig

Namespace Procesos.Configuracion.Imaging

    Public Interface IParValidaciones

        Property Respuesta As Boolean
        Property Campo_1 As SlygNullable(Of Short)
        Property Campo_2 As SlygNullable(Of Short)
        Property Valor_Comparacion As SlygNullable(Of String)
        Property Operador_Comparacion As SlygNullable(Of String)

        Sub setData(nCampos As CTA_CampoDataTable, nParametros As TBL_ParametroDataTable)

    End Interface

    Public Class ParValidacionesFactory
        Public Shared Function Create(nModo As DBImaging.EnumModoRespuestaAutomatica) As IParValidaciones
            Select Case nModo
                Case DBImaging.EnumModoRespuestaAutomatica.Comparacion_Campo
                    Return New FormParValidaciones_Campo()

                Case DBImaging.EnumModoRespuestaAutomatica.Comparacion_Constante
                    Return New FormParValidaciones_ComparacionConstante()

                Case DBImaging.EnumModoRespuestaAutomatica.Comparacion_Parametro
                    Return New FormParValidaciones_ComparacionParametro()

                Case DBImaging.EnumModoRespuestaAutomatica.Constante
                    Return New FormParValidaciones_Constante()

                Case Else
                    Throw New Exception("Modo no permitido")
            End Select
        End Function
    End Class

End Namespace