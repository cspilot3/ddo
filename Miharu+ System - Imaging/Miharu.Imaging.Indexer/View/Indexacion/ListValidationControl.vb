Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Slyg.Tools
Imports System.Drawing

Namespace View.Indexacion


    Public Class ListValidationControl
        Implements IListValidationControl

#Region " Declaraciones"
        Private Class Record
            Public Property FileName() As String
            'Public Property Cedula_Capturada() As String
            'Public Property Cedula_Leida() As String
            'Public Property Message() As String

            Public Sub New(nFileName As String)
                Me.FileName = nFileName
            End Sub

        End Class
#End Region

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

        Private Sub ValidarButton_Click(sender As System.Object, e As System.EventArgs) Handles ValidarButton.Click
            ValidarArchivos()
        End Sub

#End Region

#Region " Implementacion IListValidationControl"

        Public Sub SetFocus() Implements IListValidationControl.SetFocus
            RutaTextBox.Focus()
            RutaTextBox.SelectAll()
        End Sub

        Public Sub ValidarArchivos() Implements IListValidationControl.ValidarArchivos
        End Sub

        'Public Sub save(ByVal nfk_Expediente As Integer, ByVal nfk_Folder As Integer, ByVal nfk_File As Integer, ByVal nid_Version As Integer) Implements IListValidationControl.Save
        'End Sub

#End Region

#Region " Metodos "

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

        Public Function save(ByVal nfk_Expediente As Long, ByVal nfk_Folder As Short, ByVal nfk_File As Short, ByVal nid_Version As Short) As Boolean


            Return True
        End Function
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