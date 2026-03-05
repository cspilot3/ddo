Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library

Namespace Forms.Empaque

    Public Class FormAgregarDocumento
        Inherits FormBase

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            EnviarDocCustodia()
        End Sub

        Private Sub cbarrasDesktopCBarrasControl_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
            If e.KeyCode() = Windows.Forms.Keys.Enter Then
                cbarrasDesktopCBarrasControl_KeyDown(sender, New KeyEventArgs(Keys.Tab))
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)
        End Sub

        Private Sub EnviarDocCustodia()
            Dim dbmArchiving As DBArchiving.DBArchivingDataBaseManager = Nothing

            Try
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim Resultados As DataTable = dbmArchiving.Schemadbo.PA_Empaca_File_Caja_Final.DBExecute(cbarrasDesktopCBarrasControl.Text, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Program.RiskGlobal.ID_CajaCustodia, Program.RiskGlobal.Usa_Empaque_Adicion)

                If Resultados.Rows.Count > 0 Then
                    If CInt(Resultados.Rows(0)("CORRECTO")) = 1 Then
                        FileLabel.Text = cbarrasDesktopCBarrasControl.Text
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow(Resultados.Rows(0)("MENSAJE").ToString(), "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("AgregarDocumento", ex)
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try

            cbarrasDesktopCBarrasControl.Focus()
            cbarrasDesktopCBarrasControl.SelectAll()
        End Sub

#End Region

        Private Sub cbarrasDesktopCBarrasControl_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbarrasDesktopCBarrasControl.KeyPress
            If e.KeyChar = CChar(ChrW(13)) Then
                SendKeys.Send("{TAB}")
            End If
        End Sub

        Private Sub FormAgregarDocumento_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            If (Program.RiskGlobal.Usa_Empaque_Adicion) Then
                LblInformativo.Text = "Recuerde que los documentos que se procesan individualmente se asignaran a la caja seleccionada"
            Else
                LblInformativo.Text = "Recuerde que los documentos que se procesan individualmente no se asignaran a la caja seleccionada, sino que deben ir a la carpeta que les corresponde."
            End If
        End Sub
    End Class

End Namespace