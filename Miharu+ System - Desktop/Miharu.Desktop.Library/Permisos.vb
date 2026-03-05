Public Class Permisos

    Public Const Configuracion As String = "1"
    Public Const Facturacion As String = "2"

    Public Class Risk
        Public Const Path As String = "3"

        Public Class Proceso
            Public Const Path As String = "3.1"

            Public Class Cargue
                Public Const Path As String = "3.1.1"

                Public Const Universal As String = "3.1.1.1"
                Public Const Parcial As String = "3.1.1.2"
                Public Const Actualizacion As String = "3.1.1.3"
                Public Const Desembolsos As String = "3.1.1.4"

            End Class
            
            Public Const OT As String = "3.1.2"
            Public Const Recepcion As String = "3.1.3"
            Public Const Actualizar As String = "3.1.4"
            Public Const Destape As String = "3.1.5"
            Public Const Inserciones As String = "3.1.6"
            Public Const Empaque As String = "3.1.7"
            Public Const Centro_Distribucion As String = "3.1.8"
            Public Const Devoluciones As String = "3.1.9"

        End Class

        Public Class Mesa_Control
            Public Const Path As String = "3.2"

            Public Const Primera_Captura As String = "3.2.1"
            Public Const Segunda_Captura As String = "3.2.2"
            Public Const Tercera_Captura As String = "3.2.3"

        End Class

        Public Const Correccion As String = "3.3"
        Public Const Busqueda As String = "3.4"

        Public Const Reportes As String = "3.5"

        Public Const Configuracion As String = "3.6"

        Public Const Estructura As String = "3.7"

        Public Const Eliminacion As String = "3.8.1"
        Public Const Correccion_Data As String = "3.8.2"


        Public Class ProcesoV2

            Public Const Path As String = "3.9"

            Public Class Cierre
                Public Const path As String = "3.9.1"

                Public Const CierreFecha As String = "3.9.1.1"

            End Class

            Public Class Correo
                Public Const path As String = "3.9.2"

                Public Const EnviarCorreo As String = "3.9.2.1"
            End Class


        End Class

        Public Const Autorizaciones As String = "11"
    End Class


    Public Const Paper As String = "4"
    Public Const Custody As String = "5"

    Public Class Imaging
        Public Const Path As String = "6"

        Public Class Proceso
            Public Const Path As String = "6.1"

            Public Class Control
                Public Const Path As String = "6.1.1"

                Public Const Seguimiento As String = "6.1.1.1"
                Public Const Fecha_de_proceso As String = "6.1.1.2"
                Public Const OT As String = "6.1.1.3"
                Public Const Acceso As String = "6.1.1.4"
                Public Const Autorizaciones As String = "6.1.1.5"
                Public Const AutorizacionTxVsRegistros As String = "6.1.1.5.1"
            End Class

            Public Class Fisicos
                Public Const Path As String = "6.1.2"

                Public Const Destape As String = "6.1.2.1"
                Public Const Empaque As String = "6.1.2.2"
            End Class

            Public Class Indexacion
                Public Const Path As String = "6.1.3"

                Public Const Cargue As String = "6.1.3.1"
                Public Const Indexar As String = "6.1.3.2"
            End Class

            Public Class Captura
                Public Const Path As String = "6.1.4"

                Public Const Precaptura As String = "6.1.4.1"
                Public Const Reprocesos As String = "6.1.4.2"
                Public Const Primera_captura As String = "6.1.4.3"
                Public Const Segunda_captura As String = "6.1.4.4"
                Public Const Tercera_captura As String = "6.1.4.5"
                Public Const Calidad_captura As String = "6.1.4.6"
                Public Const Validaciones_opcionales As String = "6.1.4.7"
                Public Const Correccion_maquina_captura As String = "6.1.4.8"
                Public Const Adicional_captura As String = "6.1.4.9"
            End Class

            Public Class Recortes
                Public Const Path As String = "6.1.5"

                Public Const Recorte As String = "6.1.5.1"
                Public Const Calidad_recorte As String = "6.1.5.2"
            End Class

            Public Const Exportar As String = "6.1.6"
            Public Const Especiales As String = "6.1.7"
            Public Const Exportar_Log As String = "6.1.8"
            Public Const Correos As String = "6.1.9"

        End Class

        Public Const Busqueda As String = "6.2"
        Public Const Informes As String = "6.3"

        Public Class Configuracion
            Public Const Path As String = "6.4"

            Public Const Parametros As String = "6.4.1"
            Public Const Documentos As String = "6.4.2"
            Public Const Campos As String = "6.4.3"
            Public Const Tablas_asociadas As String = "6.4.4"
            Public Const Validaciones As String = "6.4.5"
            Public Const Tipo_OT As String = "6.4.6"
            Public Const Contenedor_campos As String = "6.4.7"
            Public Const Precinto_campos As String = "6.4.8"
            Public Const Empaque_campos As String = "6.4.9"
        End Class



        Public Class Firmas
            Public Const Path As String = "10"

            Public Class FirmasCoordinador
                Public Const Path As String = "10.1"

                Public Const Cargue_Log As String = "10.1.1"
                Public Const Excluidos_Cruce As String = "10.1.2"
                Public Const Marcar_Contigencias As String = "10.1.3"
                Public Const Preparar_Data As String = "10.1.4"
                Public Const Causales_Rechazo As String = "10.1.5"
            End Class

            Public Class FirmasFuncionario
                Public Const Path As String = "10.2"

                Public Const Verificar_Empaque As String = "10.2.1"
                Public Const Validar_Tarjetas As String = "10.2.2"
                Public Const Crear_Dat As String = "10.2.3"
            End Class

        End Class

        Public Class BCS
            Public Const Path As String = "13"

            Public Class BCSCoordinadorCU
                Public Const Path As String = "13.1"

                Public Const Cargue_Log As String = "13.1.1"
                Public Const Cruce As String = "13.1.2"
                Public Const Reportes As String = "13.1.3"
                Public Const ActEmpaque As String = "13.1.4"
            End Class

            Public Class Soporte
                Public Const Path As String = "15"

                Public Const Eliminacion_Cargues As String = "15.1"
            End Class
        End Class

        Public Class CoomevaTC
            Public Const Path As String = "14"

            Public Class CoomevaTC_Funcionario
                Public Const Path As String = "14.1"

                Public Const Publicacion As String = "14.1.1"
            End Class
        End Class

        Public Class BancoBogota
            Public Const Path As String = "16"

            Public Const PublicacionDatos As String = "16.1"
            Public Const CruceDatos As String = "16.2"
            Public Const ExportacionImagenes As String = "16.3"
        End Class

        Public Class Coomeva_Multiactiva
            Public Const Path As String = "17"

            Public Class Coomeva_Multiactiva_Funcionario
                Public Const Path As String = "17.1"

                Public Const Publicacion As String = "17.1.1"

                Public Const Rechazos As String = "17.1.2"
            End Class
        End Class

        Public Class Colpensiones_Beps
            Public Const Path As String = "18"

            Public Class Colpensiones_Beps_Funcionario
                Public Const Path As String = "18.1"

                Public Const Publicacion As String = "18.1.1"
            End Class

            Public Class Colpensiones_Beps_Funcionario_Especial
                Public Const Path As String = "18.2"

                Public Const Rechazos_Bizagi As String = "18.2.1"
                Public Const ConfiguracionRechazos_Bizagi As String = "18.2.2"
            End Class
        End Class

        Public Class BancoPopular
            Public Const Path As String = "23"

            Public Const Reportes As String = "23.1"
        End Class

    End Class

    Public Const Informes As String = "9"

    Public Class CPI
        Public Const Path As String = "12"

        Public Const Prevalidar As String = "12.1"
    End Class

    Public Class Proceso_Especiales
        Public Const Path As String = "19"

        Public Const Validaciones_Dinamicas As String = "19.1"
    End Class

    Public Const CorreccionDestape As String = "20"
    Public Const LetrasVencimiento As String = "21"
    Public Const CampoCarpeta As String = "22"

    Public Class Proceso_Generico
        Public Const Path As String = "24"

        Public Const Cargue_Log As String = "24.1"
        Public Const Generar_Hoja_Control As String = "24.2"
        Public Const Generar_Rotulos_Carpeta As String = "24.3"
        Public Const Generar_Rotulos_Caja As String = "24.4"
        Public Const Generar_Fuid_Por_Caja As String = "24.5"
        Public Const Generar_Fuid_Global As String = "24.6"
        Public Const Reemplazar_Imagenes As String = "24.7"
        Public Const Cierre As String = "24.8"
    End Class

    Public Const Digitalizacion As String = "25"
    Public Const BusquedaLote As String = "25.1"

    Public Const Relevo As String = "26"
    Public Const RelevoCentralizador As String = "27"

End Class
