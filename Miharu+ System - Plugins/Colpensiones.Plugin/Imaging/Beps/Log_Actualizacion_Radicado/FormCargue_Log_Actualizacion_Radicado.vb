Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO

Public Class FormCargue_Log_Actualizacion_Radicado

#Region " Propiedades "

    Property SelectedPath As String
        Get
            Return RutaTextBox.Text.TrimEnd("\"c) & "\"
        End Get
        Set(ByVal value As String)
            RutaTextBox.Text = value
        End Set
    End Property

#End Region

#Region " Eventos "

    Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
        SelectFolderPath()
    End Sub

#End Region

#Region " Metodos "

    Private Function Validar() As Boolean
        If (RutaTextBox.Text = "") Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            RutaTextBox.Focus()

        ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            RutaTextBox.Focus()
            RutaTextBox.SelectAll()
        Else
            Return True
        End If

        Return False
    End Function

    Private Sub SelectFolderPath()
        Dim LectorFolderBrowserDialog = New FolderBrowserDialog()
        Dim Respuesta As DialogResult

        LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
        LectorFolderBrowserDialog.ShowNewFolderButton = False
        LectorFolderBrowserDialog.Description = "Seleccione la carpeta"

        Respuesta = LectorFolderBrowserDialog.ShowDialog()

        If (Respuesta = DialogResult.OK) Then
            RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
        End If
    End Sub

#End Region

    Private Sub BtnCargar_Click(sender As System.Object, e As System.EventArgs) Handles BtnCargar.Click

    End Sub
End Class