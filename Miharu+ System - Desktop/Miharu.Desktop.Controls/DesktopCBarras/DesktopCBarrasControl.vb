Imports System.Drawing
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace DesktopCBarras

    Public Class DesktopCBarrasControl
        Inherits TextBox

#Region " Declaraciones "

        Private ConnectionString As String

        Private Entidad As Short

        Private Proyecto As Short

        Private _permitePegar As Boolean = False

#End Region

#Region " Propiedades "

        Public Property FocusIn As Color

        Public Property FocusOut As Color

        Public Property Permite_Pegar As Boolean
            Get
                Return _permitePegar
            End Get
            Set(value As Boolean)
                _permitePegar = value
            End Set
        End Property

        Public Overrides Property Text() As String
            Get
                Return GetCBarras()
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property



#End Region

#Region " Constructores "

        Public Sub New()
            MyBase.New()

            'El Diseñador de componentes requiere esta llamada.
            InitializeComponent()

        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopTextBox_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
            If (Not Me.ReadOnly) Then
                Me.BackColor = Me.FocusIn

                Me.SelectAll()
            End If
        End Sub

        Private Sub DesktopTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

            If (e.Control) Then
                If (e.KeyCode = Keys.X Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Subtract Or e.KeyCode = Keys.Add) Then
                    e.SuppressKeyPress = True
                    Return
                ElseIf ((e.KeyCode = Keys.C Or e.KeyCode = Keys.V) And Not _permitePegar) Then
                    e.SuppressKeyPress = True
                    Return
                End If

            ElseIf (e.Shift) Then
                If ((e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Insert) And Not _permitePegar) Then
                    e.SuppressKeyPress = True
                    Return
                End If
            Else
                Select Case e.KeyCode
                    Case Keys.Left, Keys.Right, Keys.Back, Keys.Delete, Keys.Home, Keys.End
                        Return

                        'Case Else

                        '    If (Me.MaxLength > 0 And Me.Text.Length >= Me.MaxLength) Then
                        '        e.SuppressKeyPress = True
                        '    End If
                End Select
            End If
        End Sub

        Private Sub DesktopTextBox_MouseDown(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
            If e.Button = MouseButtons.Right Then
                Exit Sub
            End If
        End Sub

        Private Sub DesktopTextBox_ReadOnlyChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.ReadOnlyChanged
            If (Me.ReadOnly) Then
                Me.BackColor = Color.LightGray
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub InitializeComponent()
            Me.SuspendLayout()

            Me.ShortcutsEnabled = True
            Me.FocusIn = Color.LightYellow
            Me.FocusOut = Color.White
            Me.Font = New Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0)

            Me.ResumeLayout(False)
        End Sub

        Public Sub Init(nConnectionString As String)
            Init(-1, -1, nConnectionString)
        End Sub

        Public Sub Init(nEntidad As Short, nProyecto As Short, nConnectionString As String)
            Me.Entidad = nEntidad
            Me.Proyecto = nProyecto
            Me.ConnectionString = nConnectionString
        End Sub

#End Region

#Region " Funciones "

        Private Function GetCBarras() As String
            Me.Enabled = False

            Dim cbarras As String = MyBase.Text

            If (Validar()) Then
                Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Me.ConnectionString)
                Try
                    dbmArchiving.Connection_Open(1)

                    Dim resultTable As DBArchiving.SchemaPLight.CTA_Homologacion_CBarrasDataTable = Nothing

                    If (Me.Entidad = -1) Then
                        resultTable = dbmArchiving.SchemaPLight.PA_CBarras_Find_1.DBExecute(Long.Parse(MyBase.Text))
                    Else
                        resultTable = dbmArchiving.SchemaPLight.PA_CBarras_Find_2.DBExecute(Me.Entidad, Me.Proyecto, Long.Parse(MyBase.Text))
                    End If

                    If (resultTable.Count = 1) Then
                        cbarras = resultTable(0).CBarrar_Risk
                    ElseIf (resultTable.Count > 1) Then
                        Dim cbarrasSelectorForm = New FormCBarrasSelector()
                        cbarrasSelectorForm.LoadData(resultTable)

                        If (cbarrasSelectorForm.ShowDialog() = DialogResult.OK) Then
                            cbarras = resultTable(cbarrasSelectorForm.Index).CBarrar_Risk
                        End If
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("CBarras", ex)
                Finally
                    dbmArchiving.Connection_Close()
                    Me.Enabled = True
                End Try
            Else
                Me.Enabled = True
            End If

            Return cbarras
        End Function

        Private Function Validar() As Boolean
            Return Slyg.Tools.DataConvert.IsNumeric(MyBase.Text)
        End Function

#End Region

    End Class

End Namespace