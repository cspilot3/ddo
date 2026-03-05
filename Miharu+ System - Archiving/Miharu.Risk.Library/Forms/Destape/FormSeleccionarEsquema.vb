Imports System.Windows.Forms
Imports Miharu.Desktop.Library

Namespace Forms.Destape

    Public Class FormSeleccionarEsquema
        Inherits FormBase

#Region " Propiedades "

        Public Property Esquema() As Short

#End Region

#Region " Funciones "

        Public Shared Function GetEsquema() As Short
            Dim MeForm As New FormSeleccionarEsquema()
            MeForm.ShowDialog()
            Return MeForm.Esquema
        End Function

#End Region

#Region " Metodos "

        Public Sub LlenarCombos()
            EsquemaComboBox.DisplayMember = Program.RiskGlobal.Esquemas.Nombre_esquemaColumn.ColumnName
            EsquemaComboBox.ValueMember = Program.RiskGlobal.Esquemas.fk_esquemaColumn.ColumnName
            EsquemaComboBox.DataSource = Program.RiskGlobal.Esquemas
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormSeleccionarEsquema_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            If Program.RiskGlobal.Esquemas.Count = 1 Then
                Esquema = Program.RiskGlobal.Esquemas(0).fk_esquema
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If

            LlenarCombos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            _Esquema = 0
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            _Esquema = CShort(EsquemaComboBox.SelectedValue)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub EsquemaComboBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles EsquemaComboBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

#End Region

    End Class

End Namespace