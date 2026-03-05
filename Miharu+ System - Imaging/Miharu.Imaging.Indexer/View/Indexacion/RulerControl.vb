Imports System.Windows.Forms
Imports System.Drawing

Namespace View.Indexacion

    Public Class RulerControl

#Region " Declaraciones "

        'Private _X As Integer
        Private _Y As Integer
        Private _Desplazamiento As Integer = 25

#End Region

#Region " Propiedades "

        Public ReadOnly Property MyParent() As Panel
            Get
                Return CType(Me.Parent, Panel)
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub RulerControl_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
            Dim Fuente As New Font("Microsoft Sans Serif", 7)
            Dim Largo = Me.Size.Width - 10
            Const Base As Integer = 1000
            Dim Avance = Largo / Base
            Dim Paso As Integer

            For i = 0 To Base - 10 Step 5
                Paso = 5 + CInt(i * Avance)
                e.Graphics.DrawLine(Pens.Black, Paso, 1, Paso, 3)
            Next
            For i = 0 To Base - 10 Step 25
                Paso = 5 + CInt(i * Avance)
                e.Graphics.DrawLine(Pens.Black, Paso, 3, Paso, 8)
            Next
            For i = 0 To Base - 10 Step 50
                Paso = 5 + CInt(i * Avance)
                e.Graphics.DrawLine(Pens.Black, Paso, 8, Paso, 13)
                e.Graphics.DrawString(CStr(i), Fuente, Brushes.Black, Paso + 1, 5)
            Next
        End Sub

        Private Sub RulerControl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            If (Me.Parent IsNot Nothing) Then
                _Desplazamiento = CInt(Me.MyParent.Size.Height / 100)
            End If
        End Sub

        Private Sub RulerControl_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
            If (e.Control And e.Shift) Then

            End If
        End Sub


        Private Sub RulerControl_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
            If e.Button = Windows.Forms.MouseButtons.Left Then
                '_X = e.Location.X
                _Y = e.Location.Y

                Me.Cursor = Windows.Forms.Cursors.SizeAll
            End If
        End Sub
        Private Sub RulerControl_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseEnter
            Me.Cursor = Cursors.Hand
        End Sub
        Private Sub RulerControl_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseLeave
            Me.Cursor = Cursors.Default
        End Sub
        Private Sub RulerControl_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseMove
            If (e.Button = Windows.Forms.MouseButtons.Left) Then
                Desplazar(New Point(Me.Location.X, Me.Location.Y + e.Y - _Y))
            End If
        End Sub
        Private Sub RulerControl_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseUp
            Me.Cursor = Windows.Forms.Cursors.Hand
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            MyBase.New()

            ' Llamada necesaria para el Diseñador de Windows Forms.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

        Private Sub Desplazar(ByVal NuevoPunto As Point)
            If NuevoPunto.X < 0 Then NuevoPunto.X = 0
            If NuevoPunto.Y < 0 Then NuevoPunto.Y = 0
            If NuevoPunto.X > MyParent.Width - Me.Width Then NuevoPunto.X = MyParent.Width - Me.Width
            If NuevoPunto.Y > MyParent.Height - Me.Height Then NuevoPunto.Y = MyParent.Height - Me.Height

            Me.Location = NuevoPunto
        End Sub

        Public Sub Desplazar_Top()
            Desplazar(New Point(Me.Left, 0))
        End Sub

        Public Sub Desplazar_Botton()
            Desplazar(New Point(Me.Left, MyParent.Height - Me.Height))
        End Sub

        Public Sub Desplazar_Up()
            Desplazar(New Point(Me.Left, Me.Top - _Desplazamiento))
        End Sub

        Public Sub Desplazar_Down()
            Desplazar(New Point(Me.Left, Me.Top + _Desplazamiento))
        End Sub

#End Region

#Region " Funciones "

#End Region

    End Class

End Namespace