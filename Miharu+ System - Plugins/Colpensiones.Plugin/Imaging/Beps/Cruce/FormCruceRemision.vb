Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config


Namespace Imaging.Beps.Cruce


    Public Class FormCruceRemisiones

#Region " Declaraciones "

        Private _Plugin As Plugin

#End Region

#Region " Contructores "

        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()

            _Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CruceButton_Click(sender As System.Object, e As System.EventArgs) Handles CruceButton.Click
            If Cruza_Remisiones() Then
                Me.Close()
            End If
        End Sub


        Private Sub FormCruceRemision_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            If Not Existen_Registros_Cruzar() Then
                DesktopMessageBoxControl.DesktopMessageShow("No hay registros para realizar cruce", "No hay registros", DesktopMessageBoxControl.IconEnum.SuccessfullIcon)
                Me.Close()
            End If
        End Sub


#End Region

#Region " Metodos "

        Private Sub Cierrebtn_Click(sender As System.Object, e As System.EventArgs) Handles Cierrebtn.Click
            Me.Close()
        End Sub

#End Region

#Region " Funciones "


        Private Function Cruza_Remisiones() As Boolean

            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)

            Try
                If Validar() Then
                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    dbmIntegration.SchemaColpensionesBEPS.PA_Validacion_Inserta_Datos.DBExecute(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                    DesktopMessageBoxControl.DesktopMessageShow("Se ha ejecutado el cruce", "Cruce realizado!", DesktopMessageBoxControl.IconEnum.SuccessfullIcon)
                    Return True
                End If
                Return False
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.ToString(), "fallo", DesktopMessageBoxControl.IconEnum.ErrorIcon)
                Return False
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Function

        Private Function Existen_Registros_Cruzar() As Boolean

            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.ColpensionesConnectionString)
            Try

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim CruceDataTable = dbmIntegration.SchemaColpensionesBEPS.PA_Get_Cruce_Report_Inventario.DBExecute(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)

                If CruceDataTable.Rows.Count > 0 Then
                    lblRegistros.Text = "Registros Posibles por Cruzar: " + CruceDataTable.Rows.Count.ToString
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.ToString, "Existen_Registros_Cruzar", DesktopMessageBoxControl.IconEnum.ErrorIcon)
                Me.Close()
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Function

        Private Function Validar() As Boolean

            Dim Resultado = DesktopMessageBoxControl.DesktopMessageShow("Desea realizar el cruce?", "Realizar cruce remisiones?", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False, False)
            If (Resultado = DialogResult.OK) Then
                Return True
            Else
                Return False
            End If

        End Function

#End Region

    End Class


End Namespace
