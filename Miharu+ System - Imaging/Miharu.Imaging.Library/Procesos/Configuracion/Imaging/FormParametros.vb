Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Procesos.Configuracion.Imaging

    Public Class FormParametros
        Inherits FormBase

        Private ParametrosDataTable As DBImaging.SchemaConfig.TBL_ParametroDataTable

        Private Sub FormParametros_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                ParametrosDataTable = dbmImaging.SchemaConfig.TBL_Parametro.DBGet(Nothing)

                TBLParametrosBindingSource.DataSource = ParametrosDataTable
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarParametros", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    dbmImaging.Transaction_Begin()

                    For Each Parametro As DBImaging.SchemaConfig.TBL_ParametroRow In ParametrosDataTable
                        If Not Parametro.Nombre_Parametro.StartsWith("@") Then
                            Throw New Exception("Los nombres de los parametros deben comenzar por @")
                        End If
                    Next

                    dbmImaging.SchemaConfig.TBL_Parametro.DBSaveTable(ParametrosDataTable)

                    dbmImaging.Transaction_Commit()
                    DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("Hubo problemas al guardar los datos, por favor comuniquese con el administrador" & vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Private Function Validar() As Boolean
            'Dim Validacion As Boolean = True
            'Dim sbErrores As New StringBuilder

            'If Validacion = False Then
            '    DesktopMessageBox.DesktopMessageShow(sbErrores.ToString(), "Error de data", Desktop.Controls.DesktopMessageBox.IconEnum.ErrorIcon, True)
            'End If

            Return True
        End Function

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub
    End Class
End Namespace