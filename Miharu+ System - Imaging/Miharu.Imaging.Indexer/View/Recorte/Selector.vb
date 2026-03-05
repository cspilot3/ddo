Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace View.Recorte

    Public Class Selector

#Region " Declaraciones "

        Public Posicion As Point
        Public Tamaño As Size
        Public Property Folio As Short
        Public Property Escala As Integer

        Public Property Angulo As Short

        Private _DataControl As IDataControl

        Public ReadOnly Property DataControl As IDataControl
            Get
                Return _DataControl
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New(nDataControl As IDataControl)
            Posicion = New Point()
            Tamaño = New Size()
            Angulo = 0

            _DataControl = nDataControl
        End Sub

#End Region

#Region " Metodos "

        Public Sub Draw(ByRef g As Graphics, Optional ByVal X As Integer = Nothing, Optional ByVal Y As Integer = Nothing)
            Dim blackPen As New Pen(Color.Black, 2)
            Dim backgroundBrush As SolidBrush

            If (Me.DataControl.Data.Bloqueado) Then
                backgroundBrush = New SolidBrush(Color.FromArgb(70, 18, 237, 60))
            Else
                backgroundBrush = New SolidBrush(Color.FromArgb(70, 255, 165, 0))
            End If

            Dim drawFont As New Font("Arial", 10, FontStyle.Bold)
            Dim drawBrush As New SolidBrush(Color.Blue)
            Dim drawPoint As New PointF()

            If X = Nothing Then X = Posicion.X
            If Y = Nothing Then Y = Posicion.Y

            drawPoint.X = Redimension(X)
            drawPoint.Y = Redimension(Y) + 5

            g.FillRectangle(backgroundBrush, Redimension(X), Redimension(Y), Redimension(Tamaño.Width), Redimension(Tamaño.Height))
            g.DrawRectangle(BlackPen, Redimension(X), Redimension(Y), Redimension(Tamaño.Width), Redimension(Tamaño.Height))
            g.DrawString(DataControl.Label, drawFont, drawBrush, drawPoint)
        End Sub
        
        Public Sub Selected(ByRef g As Graphics)
            ' Create pen.
            Dim Brush As New HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Gray, Color.FromArgb(0, 0, 128, 0))
            Dim BlackPen As New Pen(Color.Black, 1)            
            Dim fillBrush As SolidBrush

            If (Me.DataControl.Data.Bloqueado) Then
                fillBrush = New SolidBrush(Color.FromArgb(255, 255, 0, 0))
            Else
                fillBrush = New SolidBrush(Color.FromArgb(255, 0, 0, 255))
            End If

            ' Recuadro
            g.FillRectangle(Brush, Redimension(Posicion.X) - 7, Redimension(Posicion.Y) - 7, 5, Redimension(Tamaño.Height) + 14)
            g.FillRectangle(Brush, Redimension(Posicion.X + Tamaño.Width) + 2, Redimension(Posicion.Y) - 7, 5, Redimension(Tamaño.Height) + 14)
            g.FillRectangle(Brush, Redimension(Posicion.X) - 2, Redimension(Posicion.Y) - 7, Redimension(Tamaño.Width) + 4, 5)
            g.FillRectangle(Brush, Redimension(Posicion.X) - 2, Redimension(Posicion.Y + Tamaño.Height) + 2, Redimension(Tamaño.Width) + 4, 5)

            g.FillRectangle(fillBrush, Redimension(Posicion.X) - 8, Redimension(Posicion.Y) - 8, 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X) - 8, Redimension(Posicion.Y) - 8, 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X) - 8, Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) - 4, 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X) - 8, Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) - 4, 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X) - 8, Redimension(Posicion.Y + Tamaño.Height), 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X) - 8, Redimension(Posicion.Y + Tamaño.Height), 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X + Tamaño.Width), Redimension(Posicion.Y) - 8, 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X + Tamaño.Width), Redimension(Posicion.Y) - 8, 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X + Tamaño.Width), Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) - 4, 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X + Tamaño.Width), Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) - 4, 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X + Tamaño.Width), Redimension(Posicion.Y + Tamaño.Height), 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X + Tamaño.Width), Redimension(Posicion.Y + Tamaño.Height), 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X + CInt(Tamaño.Width / 2)) - 4, Redimension(Posicion.Y) - 8, 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X + CInt(Tamaño.Width / 2)) - 4, Redimension(Posicion.Y) - 8, 7, 7)

            g.FillRectangle(fillBrush, Redimension(Posicion.X + CInt(Tamaño.Width / 2)) - 4, Redimension(Posicion.Y + Tamaño.Height), 7, 7)
            g.DrawRectangle(BlackPen, Redimension(Posicion.X + CInt(Tamaño.Width / 2)) - 4, Redimension(Posicion.Y + Tamaño.Height), 7, 7)

        End Sub

        Public Sub MovingAll(ByVal DiffX As Integer, ByVal DiffY As Integer)
            Posicion.X += DiffX
            Posicion.Y += DiffY
        End Sub

        Public Sub MovingUpLeft(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Width - DiffX >= 10 Then
                Posicion.X += DiffX
                Tamaño.Width -= DiffX
            End If
            If Tamaño.Height - DiffY >= 10 Then
                Posicion.Y += DiffY
                Tamaño.Height -= DiffY
            End If
        End Sub

        Public Sub MovingUp(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Height - DiffY >= 10 Then
                Posicion.Y += DiffY
                Tamaño.Height -= DiffY
            End If
        End Sub

        Public Sub MovingUpRight(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Width + DiffX >= 10 Then
                Tamaño.Width += DiffX
            End If
            If Tamaño.Height - DiffY >= 10 Then
                Posicion.Y += DiffY
                Tamaño.Height -= DiffY
            End If
        End Sub

        Public Sub MovingDownLeft(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Width - DiffX >= 10 Then
                Posicion.X += DiffX
                Tamaño.Width -= DiffX
            End If
            If Tamaño.Height + DiffY >= 10 Then
                Tamaño.Height += DiffY
            End If
        End Sub

        Public Sub MovingDown(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Height + DiffY >= 10 Then
                Tamaño.Height += DiffY
            End If
        End Sub

        Public Sub MovingDownRight(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Width + DiffX >= 10 Then
                Tamaño.Width += DiffX
            End If
            If Tamaño.Height + DiffY >= 10 Then
                Tamaño.Height += DiffY
            End If
        End Sub

        Public Sub MovingLeft(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Width - DiffX >= 10 Then
                Posicion.X += DiffX
                Tamaño.Width -= DiffX
            End If
        End Sub

        Public Sub MovingRight(ByVal DiffX As Integer, ByVal DiffY As Integer)
            If Tamaño.Width + DiffX >= 10 Then
                Tamaño.Width += DiffX
            End If
        End Sub

        Public Sub RotateRight()
            Angulo += CShort(90)

            If Angulo >= 360 Then Angulo = 0
        End Sub

        Public Sub RotateLeft()
            Angulo -= CShort(90)

            If Angulo < 0 Then Angulo = 270
        End Sub

#End Region

#Region " Funciones "

        Public Function IsSelecting(ByVal X As Integer, ByVal Y As Integer) As Boolean
            If (X > Redimension(Posicion.X) And X < Redimension((Posicion.X + Tamaño.Width))) Then
                If (Y > Redimension(Posicion.Y) And Y < Redimension((Posicion.Y + Tamaño.Height))) Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function IsMovable(ByVal X As Integer, ByVal Y As Integer) As Enumeraciones.Movables
            If (Me.DataControl.Data.Bloqueado) Then
                Return Movables.NoMovable
            ElseIf ((X > Redimension(Posicion.X)) And (X < Redimension(Posicion.X + Tamaño.Width))) And ((Y > Redimension(Posicion.Y)) And (Y < Redimension(Posicion.Y + Tamaño.Height))) Then
                Return Movables.NoMovable

            ElseIf ((X > Redimension(Posicion.X) - 8) And (X < Redimension(Posicion.X) - 1)) And ((Y > Redimension(Posicion.Y) - 8) And (Y < Redimension(Posicion.Y) - 1)) Then
                Return Movables.movableUpLeft

            ElseIf ((X > Redimension(Posicion.X) - 8) And (X < Redimension(Posicion.X) - 1)) And ((Y > Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) - 4) And (Y < Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) + 3)) Then
                Return Movables.movableLeft

            ElseIf ((X > Redimension(Posicion.X) - 8) And (X < Redimension(Posicion.X) - 1)) And ((Y > Redimension(Posicion.Y + Tamaño.Height) + 1) And (Y < Redimension(Posicion.Y + Tamaño.Height) + 8)) Then
                Return Movables.movableDownLeft

            ElseIf ((X > Redimension(Posicion.X + Tamaño.Width) + 1) And (X < Redimension(Posicion.X + Tamaño.Width) + 8)) And ((Y > Redimension(Posicion.Y) - 8) And (Y < Redimension(Posicion.Y) - 1)) Then
                Return Movables.movableUpRight

            ElseIf ((X > Redimension(Posicion.X + Tamaño.Width) + 1) And (X < Redimension(Posicion.X + Tamaño.Width) + 8)) And ((Y > Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) - 4) And (Y < Redimension(Posicion.Y + CInt(Tamaño.Height / 2)) + 3)) Then
                Return Movables.movableRight

            ElseIf ((X > Redimension(Posicion.X + Tamaño.Width) + 1) And (X < Redimension(Posicion.X + Tamaño.Width) + 8)) And ((Y > Redimension(Posicion.Y + Tamaño.Height) + 1) And (Y < Redimension(Posicion.Y + Tamaño.Height) + 8)) Then
                Return Movables.movableDownRight

            ElseIf ((X > Redimension(Posicion.X + CInt(Tamaño.Width / 2)) - 4) And (X < Redimension(Posicion.X + CInt(Tamaño.Width / 2)) + 3)) And ((Y > Redimension(Posicion.Y) - 8) And (Y < Redimension(Posicion.Y) - 1)) Then
                Return Movables.movableUP

            ElseIf ((X > Redimension(Posicion.X + CInt(Tamaño.Width / 2)) - 4) And (X < Redimension(Posicion.X + CInt(Tamaño.Width / 2)) + 3)) And ((Y > Redimension(Posicion.Y + Tamaño.Height) + 1) And (Y < Redimension(Posicion.Y + Tamaño.Height) + 8)) Then
                Return Movables.movableDown

            ElseIf ((X > Redimension(Posicion.X) - 8) And (X < Redimension(Posicion.X + Tamaño.Width) + 8)) And ((Y > Redimension(Posicion.Y) - 8) And (Y < Redimension(Posicion.Y + Tamaño.Height) + 8)) Then
                Return Movables.MovableAll

            Else
                Return Movables.NoMovable
            End If

        End Function

        Private Function Redimension(nCoordenada As Integer) As Integer
            Return CInt(nCoordenada * Escala / 100)
        End Function

#End Region

    End Class

End Namespace

