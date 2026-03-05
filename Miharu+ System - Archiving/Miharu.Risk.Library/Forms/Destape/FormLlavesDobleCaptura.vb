Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Destape

    Public Class FormLlavesDobleCaptura
        Inherits FormBase

#Region " Declaraciones "

        Private _TableForm As TableLayoutPanel
        Private _Valores As Dictionary(Of String, String)

#End Region

#Region " Constructores "

        Public Sub New(ByVal Valores As Dictionary(Of String, String))
            InitializeComponent()
            _Valores = Valores
        End Sub

#End Region

#Region " Eventos "

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub FormLlavesDobleCaptura_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            ControlCrearCampos()
            SpaceFlowLayoutPanel.Focus()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.DialogResult = DialogResult.OK

            For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                Dim NombreLlave As String = Llave.Nombre
                Dim Campo1 As DesktopTextBoxControl = CType(Utilities.FindControl(_TableForm, NombreLlave.Replace(" ", "_")), DesktopTextBoxControl)
                Dim ValorCampoDestape = _Valores.Item(NombreLlave.Replace(" ", "_"))

                If Campo1.Text <> ValorCampoDestape Then
                    Me.DialogResult = DialogResult.Cancel
                    Exit For
                End If
            Next

            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub ControlCrearCampos()
            If Program.RiskGlobal.CargueParcial = True Then
                SpaceFlowLayoutPanel.Controls.Add(ControlesCargue(Program.RiskGlobal.LLavesProyecto))
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function ControlesCargue(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As TableLayoutPanel
            _TableForm = New TableLayoutPanel
            Try
                _TableForm.ColumnCount = 2
                _TableForm.RowCount = Llaves.Count
                _TableForm.Width = 500
            Catch ex As Exception
                Throw New Exception("No existen llaves en el proyecto.")
            End Try


            Dim i As Integer = 0
            For Each Llave As DesktopConfig.LlaveProyecto In Llaves
                Dim NombreLlave As String = Llave.Nombre

                Dim LlaveLabel As New Label
                LlaveLabel.Name = "lbl_" & NombreLlave.Replace(" ", "_")
                LlaveLabel.Text = NombreLlave
                LlaveLabel.Width = 200

                Dim LlaveTextBox As New DesktopTextBoxControl
                LlaveTextBox.Name = NombreLlave.Replace(" ", "_")
                LlaveTextBox.Width = 250
                Select Case Llave.Tipo
                    Case DesktopConfig.CampoTipo.Numerico
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                    Case DesktopConfig.CampoTipo.Fecha
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                    Case DesktopConfig.CampoTipo.Texto
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                End Select


                _TableForm.Controls.Add(LlaveLabel, 0, i)
                _TableForm.Controls.Add(LlaveTextBox, 1, i)

                If i = 0 Then LlaveTextBox.Focus()
                i += 1
            Next

            _TableForm.Refresh()

            Return _TableForm
        End Function

#End Region

    End Class

End Namespace