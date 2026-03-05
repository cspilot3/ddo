Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Namespace DesktopMessageBox

    Public Class DesktopMessageBoxControl

#Region " Declaraciones "
        Private _title As String
        Private _message As String
        Private _showIcon As String
        Private _okOnly As String

        Const NombreAplicacion As String = "Miharu DDO"
#End Region

#Region " Enumeraciones "
        Enum IconEnum
            WarningIcon
            ErrorIcon
            AdvertencyIcon
            SuccessfullIcon
        End Enum
#End Region

#Region " Propiedades "

        Public Property Verificar As Boolean

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
                TitleMessage.Text = value
            End Set
        End Property

        Public Property Message() As String
            Get
                Return _message
            End Get
            Set(ByVal value As String)
                _message = value
                ErrorMessage.Text = value
            End Set
        End Property

        Public Shadows Property Icon() As IconEnum
            Get
                Return _showIcon
            End Get
            Set(ByVal value As IconEnum)
                _showIcon = value

                If value = IconEnum.ErrorIcon Then
                    IconPicture.Image = Desktop.Controls.My.Resources.Resources._Error
                ElseIf value = IconEnum.AdvertencyIcon Then
                    IconPicture.Image = Desktop.Controls.My.Resources.Resources.Alert
                ElseIf value = IconEnum.WarningIcon Then
                    IconPicture.Image = Desktop.Controls.My.Resources.Resources.Warning
                ElseIf value = IconEnum.SuccessfullIcon Then
                    IconPicture.Image = Desktop.Controls.My.Resources.Resources.Ok
                End If

            End Set
        End Property

        Public Shadows Property OkOnly() As Boolean
            Get
                Return _okOnly
            End Get
            Set(ByVal value As Boolean)
                _okOnly = value

                cancel.Visible = Not value
            End Set
        End Property
#End Region

#Region " Eventos "

        Private Sub DesktopMessageBoxControl_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            chkVerificado.Visible = Verificar
            ok.Enabled = Not Verificar
            Me.BringToFront()

            If Verificar Then
                cancel.Focus()
            Else
                ok.Focus()
            End If
        End Sub

        Private Sub ok_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ok.Click
            Me.DialogResult = DialogResult.OK
        End Sub

        Private Sub cancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cancel.Click
            Me.DialogResult = DialogResult.Cancel
        End Sub

        Private Sub chkVerificado_CheckedChanged(sender As System.Object, e As EventArgs) Handles chkVerificado.CheckedChanged
            If _Verificar = True Then
                If chkVerificado.Checked Then
                    ok.Enabled = True
                Else
                    ok.Enabled = False
                End If
            End If
        End Sub
#End Region

#Region " Funciones "
        Public Shared Function DesktopMessageShow(ByVal nMessage As String, ByVal nTitle As String, ByVal nIcon As IconEnum, Optional ByVal nSoloAcpetar As Boolean = False, Optional ByVal nVerificar As Boolean = False) As DialogResult
            Dim desktopMessageBox As New DesktopMessageBoxControl
            desktopMessageBox.Message = nMessage
            desktopMessageBox.Title = nTitle
            desktopMessageBox.Icon = nIcon
            desktopMessageBox.OkOnly = nSoloAcpetar
            desktopMessageBox.Text = NombreAplicacion
            desktopMessageBox.Verificar = nVerificar
            Return desktopMessageBox.ShowDialog()
        End Function

        Public Shared Function DesktopMessageShow(ByVal nMessage As StringBuilder, ByVal nTitle As String, ByVal nIcon As IconEnum, Optional ByVal nSoloAcpetar As Boolean = False) As DialogResult
            Dim desktopMessageBox As New DesktopMessageBoxControl

            'Oculta el textbox
            desktopMessageBox.ErrorMessage.Visible = False
            desktopMessageBox.HtmlErrorMessage.Visible = True

            'Muestra el Webbrowser
            desktopMessageBox.HtmlErrorMessage.Navigate("about:blank")
            Dim doc As HtmlDocument = desktopMessageBox.HtmlErrorMessage.Document

            Dim html As New StringBuilder
            html.AppendLine("<head><STYLE TYPE='text/css'>BODY{background-color:Gainsboro;font-family:sans-serif;font-size: 12px;}</STYLE></head>")
            html.AppendLine("<body>")
            html.AppendLine(nMessage.ToString())
            html.AppendLine("</body>")
            doc.Write(html.ToString())


            desktopMessageBox.Title = nTitle
            desktopMessageBox.Icon = nIcon
            desktopMessageBox.OkOnly = nSoloAcpetar
            desktopMessageBox.Text = NombreAplicacion

            Return desktopMessageBox.ShowDialog()
        End Function

        Public Shared Function DesktopMessageShow(ByVal sMetodo As String, ByRef eException As Exception) As DialogResult
            Try
                Dim desktopMessageBox As New DesktopMessageBoxControl
                desktopMessageBox.Message = eException.Message
                desktopMessageBox.Message &= vbCrLf & vbCrLf
                desktopMessageBox.Message &= "---------------------"
                desktopMessageBox.Message &= vbCrLf & vbCrLf
                desktopMessageBox.Message &= eException.StackTrace
                desktopMessageBox.Title = "Se generó un error en " + sMetodo
                desktopMessageBox.Icon = IconEnum.ErrorIcon
                desktopMessageBox.OkOnly = True
                desktopMessageBox.Text = NombreAplicacion

                EscribeLog(eException)
                Return desktopMessageBox.ShowDialog()
            Catch
                Return MessageBox.Show(eException.Message, sMetodo, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function
#End Region

#Region " Metodos "
        Private Shared Sub EscribeLog(ByVal e As Exception)
            Try
                Const sSource As String = NombreAplicacion
                Const sLog As String = "Application"
                Dim sEvent As String = "Mensage de error: " & e.Message & vbCrLf & vbCrLf & "StackTrace: " & e.StackTrace
                Dim sMachine As String = Environment.MachineName

                If Not EventLog.SourceExists(sSource, sMachine) Then
                    'EventLog.CreateEventSource(sSource, sLog, sMachine)
                    Dim creationData As New EventSourceCreationData(sSource, sLog)
                    creationData.MachineName = Environment.MachineName
                    EventLog.CreateEventSource(creationData)
                End If

                Dim eLog As New EventLog(sLog, sMachine, sSource)
                eLog.WriteEntry(sEvent, EventLogEntryType.Error)

                'Escribe en archivo físico            
                Dim fileNameErrors As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd("\"c) & "\" & NombreAplicacion & "_" & Now.Year & Now.Month & Now.Day & ".txt"

                'Crea Archivo de errores.
                If Not File.Exists(fileNameErrors) Then
                    Dim fileStream As FileStream = File.Create(fileNameErrors)
                    fileStream.Close()
                End If

                Dim fs As FileStream = New FileStream(fileNameErrors, FileMode.Append, FileAccess.Write)
                Dim sw As StreamWriter = New StreamWriter(fs)
                sw.WriteLine("[" & DateTime.Now.ToString() & "] : " & e.Message & vbCrLf & vbCrLf & "StackTrace: " & e.StackTrace & vbCrLf)
                sw.Close()
                fs.Close()
            Catch : End Try
        End Sub
#End Region

    End Class

End Namespace