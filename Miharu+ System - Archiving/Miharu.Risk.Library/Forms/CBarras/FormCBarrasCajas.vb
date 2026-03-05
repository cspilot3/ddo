Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.BarCode
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.CBarras

    Public Class FormCBarrasCajas
        Inherits FormBase

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().            
        End Sub

        Private Sub ImprimirCBarras()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim UltimoCbarrasCaja = dbmArchiving.SchemaConfig.Tbl_Secuencia.DBGet(DesktopConfig.Consecutivo.Cajas)(0).Secuencia

            If ParametroDesktopTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un rango o un codigo de barras", "Error procesando CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Exit Sub
            End If

            Try
                Dim BarCodeControl_ As New BarCodeControl
                Dim Parametros As New List(Of DesktopConfig.AtributesCBarras)

                'Imprimir rango de codigos de barras
                If TipoDesktopComboBox.Text = "Rango" Then
                    Dim Consecutivo = Utilities.NextCBarras(dbmArchiving, DesktopConfig.Consecutivo.Cajas)
                    Dim UltimoConsecutivoCaja As Integer = 0

                    For i As Integer = 0 To CInt(ParametroDesktopTextBox.Text) - 1 Step 1
                        Dim CBarras = "999" & Utilities.CbarrasSoloFomat(CInt(Consecutivo + i))
                        Utilities.ImprimirCBarras(BarCodeControl_, CBarras, "Caja", Parametros)
                        'BarCodeControl_.Print()
                        UltimoConsecutivoCaja = CInt(Consecutivo + i)
                    Next

                    Try
                        dbmArchiving.Transaction_Begin()
                        dbmArchiving.SchemaConfig.Tbl_Secuencia.DBUpdate(Nothing, Nothing, Nothing, UltimoConsecutivoCaja, DesktopConfig.Consecutivo.Cajas)
                        dbmArchiving.Transaction_Commit()

                        DesktopMessageBoxControl.DesktopMessageShow("Codigos de barras impreso con exito", "Codigos de barras OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Catch ex As Exception
                        dbmArchiving.Transaction_Rollback()
                    End Try

                    'Imprimir codigo de barras unico
                ElseIf TipoDesktopComboBox.Text = "CBarras" Then
                    If ParametroDesktopTextBox.Text.Substring(0, 3) = "999" Then
                        If CInt(ParametroDesktopTextBox.Text.Substring(4)) < UltimoCbarrasCaja Then
                            Utilities.ImprimirCBarras(BarCodeControl_, ParametroDesktopTextBox.Text, "Caja", Parametros)
                            'BarCodeControl_.Print()
                            DesktopMessageBoxControl.DesktopMessageShow("Codigo de barras impreso con exito", "Codigo de barras OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras no puede ser mayor al consecutivo de base de datos.", "Error imprimiendo CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        End If

                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El codigo de barras debe iniciar en '999' para las cajas.", "Error imprimiendo CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If

                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una opcion de impresion de codigos de barras", "Error imprimiendo CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ImprimirCBarras", ex)
            End Try

            dbmArchiving.Connection_Close()
        End Sub

#End Region

#Region " Eventos "

        Private Sub TipoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoDesktopComboBox.SelectedIndexChanged
            ParametroDesktopTextBox.Text = ""
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            ImprimirCBarras()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace