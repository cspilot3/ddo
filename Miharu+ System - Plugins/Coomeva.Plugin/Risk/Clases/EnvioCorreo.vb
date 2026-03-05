Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Coomeva.Plugin.Risk.FormWrappers
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.Plugins
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports Microsoft.Reporting.WinForms

Public Class EnvioCorreo
    
#Region " Declaraciones "

    Private _Plugin As CoomevaRiskPlugin

#End Region

#Region " Constructor "

    Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin)
        ' This call is required by the designer.
        _Plugin = nCoomevaDesktopPlugin
    End Sub

#End Region

#Region " Metodos "

    Public Sub EnviarCorreo(ByVal fechaRecoleccion As DateTime)

        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing

        Try
            dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)

            dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
            dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'obtiene listado de oficinas activas
            Dim TablaOficinas = dbmArchiving.SchemaConfig.TBL_Oficina.DBFindByfk_Entidadfk_ProyectoEstado(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, 1)

            If TablaOficinas.Rows.Count > 0 Then

                '------------------Validacion listas para enviar correos--------------------
                Dim NotificacionDataTable = dbmCore.SchemaConfig.TBL_Notificacion.DBFindByNombre_Notificacion("Gestión Oficinas")
                If NotificacionDataTable.Count = 1 Then
                    Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBFindByfk_NotificacionNombre_Lista(NotificacionDataTable(0).id_Notificacion, "Gestión Oficinas - Coomeva")
                    If NotificacionListasDataTable.Count = 1 Then
                        Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(NotificacionDataTable(0).id_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                        If CorreoDatatable.Count = 1 Then
                            Dim Mensaje As String = ""
                            Dim EnvioCorreo As Boolean = False

                            Mensaje = CorreoDatatable(0).SALUDO & CorreoDatatable(0).CUERPO & CorreoDatatable(0).FIRMA

                            ' Por cada oficina coomeva activa realiza la validacion de envio correo gestión oficinas
                            For Each row_Oficina As DataRow In TablaOficinas.Rows
                                Try
                                    ' Crear archivo adjunto
                                    Dim mimeType As String = ""
                                    Dim encoding As String = ""
                                    Dim fileNameExtension As String = ""
                                    Dim warnings As Warning() = Nothing
                                    Dim streamids As String() = Nothing
                                    ' Correo Oficina
                                    Dim CorreoOficina As String = row_Oficina("Correo_Oficina")


                                    Dim Faltantes_Datatable = dbmArchiving.SchemaReport.PA_Reporte_Oficinas_Faltantes_Log.DBExecute(Format(fechaRecoleccion, "yyyy/MM/dd"), CShort(row_Oficina("id_Oficina")))
                                    Dim FaltantesLogicos_Datatable = dbmArchiving.SchemaReport.PA_Reporte_Oficinas_Faltantes_logicos.DBExecute(Format(fechaRecoleccion, "yyyy/MM/dd"), CShort(row_Oficina("id_Oficina")))
                                    Dim Sobrantes_Datatable = dbmArchiving.SchemaReport.PA_Reporte_Oficinas_Sobrantes.DBExecute(Format(fechaRecoleccion, "yyyy/MM/dd"), CShort(row_Oficina("id_Oficina")))
                                    Dim Novedades_Datatable = dbmArchiving.SchemaReport.PA_Reporte_Oficinas_Novedades.DBExecute(Format(fechaRecoleccion, "yyyy/MM/dd"), CShort(row_Oficina("id_Oficina")))

                                    If (Faltantes_Datatable.Rows.Count > 0 Or FaltantesLogicos_Datatable.Rows.Count > 0 Or Sobrantes_Datatable.Rows.Count > 0 Or Novedades_Datatable.Rows.Count > 0) And (CorreoOficina IsNot Nothing And CorreoOficina <> "") Then
                                        'If (Faltantes_Datatable.Rows.Count > 0 Or Novedades_Datatable.Rows.Count > 0) And (CorreoOficina IsNot Nothing And CorreoOficina <> "") Then
                                        EnvioCorreo = True
                                        Dim report = New ReportViewer()

                                        report.LocalReport.ReportEmbeddedResource = "Coomeva.Plugin.GestionOficinasCorreo.rdlc"
                                        report.LocalReport.DataSources.Add(New ReportDataSource("Faltantes", CType(Faltantes_Datatable, DataTable)))
                                        report.LocalReport.DataSources.Add(New ReportDataSource("FaltantesLogicos", CType(FaltantesLogicos_Datatable, DataTable)))
                                        report.LocalReport.DataSources.Add(New ReportDataSource("Sobrantes", CType(Sobrantes_Datatable, DataTable)))
                                        report.LocalReport.DataSources.Add(New ReportDataSource("Novedades", CType(Novedades_Datatable, DataTable)))

                                        Dim attach = report.LocalReport.Render("Excel", Nothing, mimeType, encoding, fileNameExtension, streamids, warnings)

                                        'realiza agendamiento de mail para envio
                                        SendMail(CorreoOficina, CorreoDatatable(0).CORREOS, "", CorreoDatatable(0).ASUNTO, Mensaje, "Reporte_Gestion_Oficinas_" & Replace(Date.Now.ToString("yyyy/MM/dd"), "/", "") & ".xls", attach)
                                    End If

                                Catch ex As Exception
                                    DMB.DesktopMessageShow("Error en generación de correo gestión Oficinas ", ex)
                                End Try
                            Next

                            If EnvioCorreo Then
                                DesktopMessageBoxControl.DesktopMessageShow("El correo de oficinas ha sido enviado exitosamente.", "Gestión de Oficinas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    End If
                End If

            Else
                DesktopMessageBoxControl.DesktopMessageShow("No existen oficinas configuradas para el envio de correo.", "Gestión de Oficinas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
           
        Catch ex As Exception
            DMB.DesktopMessageShow("Generacion de correo - Validacion Listas", ex)
        Finally
            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
        End Try

    End Sub

    Private Sub SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte())
        Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

        Try
            DBMTools = New DBTools.DBToolsDataBaseManager(_Plugin.ToolsConnectionString)
            DBMTools.Connection_Open()

            DBMTools.InsertMail(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.Sesion.Usuario.id, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBMTools.Connection_Close()
        End Try
    End Sub


#End Region

End Class
