Imports System.Drawing
Imports System.Windows.Forms

Namespace DesktopOptionCheck

    Public Class DesktopOptionCheckControl
        Inherits Control

#Region " Declaraciones "

        Private vTable As New TableLayoutPanel
        Private vLabel As New Label
        Private vLabelSi As New Label
        Private vRadioSi As New RadioButton
        Private vLabelNo As New Label
        Private vRadioNo As New RadioButton

        Private pChecked As Boolean = False

#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call.

            vTable.RowCount = 1
            vTable.ColumnCount = 3

            vLabel.Text = Text
            vTable.Controls.Add(vLabel, 0, 0)

            vLabelSi.Text = "Si"
            vTable.Controls.Add(vLabel, 1, 0)

            vRadioSi.Name = "SI"
            vTable.Controls.Add(vRadioSi, 1, 0)

            vLabelNo.Text = "No"
            vTable.Controls.Add(vLabelNo, 2, 0)

            vRadioNo.Name = "NO"
            vTable.Controls.Add(vRadioNo, 2, 0)


            FocusIn = Color.LightYellow
            FocusOut = Color.Gainsboro
            DisabledEnter = False

        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopCheckBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            Me.vTable.BackColor = FocusIn
        End Sub

        Private Sub DesktopCheckBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
            Me.vTable.BackColor = FocusOut
        End Sub

#End Region

#Region "Propertys"

        Public Overrides Property Text As String

        Public Property Checked As Boolean
            Get
                Return pChecked
            End Get

            Set(ByVal value As Boolean)
                If pChecked Then
                    vRadioSi.Checked = True
                    vRadioNo.Checked = False
                Else
                    vRadioSi.Checked = False
                    vRadioNo.Checked = True
                End If

                pChecked = value
            End Set
        End Property

        Public Property DisabledEnter As Boolean

        Property FocusIn As Color

        Property FocusOut As Color

#End Region

    End Class

End Namespace