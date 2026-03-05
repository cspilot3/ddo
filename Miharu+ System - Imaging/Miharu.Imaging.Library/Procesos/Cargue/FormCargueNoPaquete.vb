Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls

Namespace Procesos.Cargue

    Public Class FormCargueNoPaquete

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

        Private Sub SelectFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub FormCargueNoPaquete_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Me.DialogResult = DialogResult.None
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()

        End Sub

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

#Region " Funciones "

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

#End Region

    End Class
End Namespace