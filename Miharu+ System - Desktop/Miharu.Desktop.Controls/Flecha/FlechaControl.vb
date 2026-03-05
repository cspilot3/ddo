Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace Flecha

    Public Class FlechaControl
        Inherits Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "

        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
        Protected Overloads Overrides Sub Dispose(ByVal isDisposing As Boolean)
            'If isDisposing Then
            '    If Not (components Is Nothing) Then
            '        components.Dispose()
            '    End If
            'End If
            MyBase.Dispose(isDisposing)
        End Sub

        'Requerido por el Diseñador de Windows Forms
        'Private components As ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        Private WithEvents picBase As Windows.Forms.PictureBox

#End Region

#Region " Declaraciones "

        'Private BackColorAroow As System.Drawing.Color = Color.Empty
        Private _ArrowWidth As Integer = 20
        Private _Tipo As EnumTipo = EnumTipo.RIGHT

        Private _FillColor As Color = Color.Teal
        Private _BorderColor As Color = Color.Black
        Private _BorderWidth As Byte = 1

        Private _DashStyle As DashStyle = DashStyle.Solid

        Public Enum EnumTipo As Byte
            DOWN
            DOWN_LEFT
            DOWN_RIGHT
            LEFT
            LEFT_DOWN
            LEFT_UP
            RIGHT
            RIGHT_DOWN
            RIGHT_UP
            UP
            UP_LEFT
            UP_RIGHT
        End Enum

#End Region

#Region " Propiedades "

        Public Property ArrowWidth() As Integer
            Get
                Return _ArrowWidth
            End Get
            Set(ByVal Value As Integer)
                _ArrowWidth = Value
                picBase.Refresh()
            End Set
        End Property
        Public Property Tipo() As EnumTipo
            Get
                Return _Tipo
            End Get
            Set(ByVal Value As EnumTipo)
                _Tipo = Value
                picBase.Refresh()
            End Set
        End Property

        Public Property FillColor() As Color
            Get
                Return _FillColor
            End Get
            Set(ByVal Value As Color)
                _FillColor = Value
                picBase.Refresh()
            End Set
        End Property
        Public Property BorderColor() As Color
            Get
                Return _BorderColor
            End Get
            Set(ByVal Value As Color)
                _BorderColor = Value
                picBase.Refresh()
            End Set
        End Property
        Public Property BorderWidth() As Byte
            Get
                Return _BorderWidth
            End Get
            Set(ByVal Value As Byte)
                _BorderWidth = Value
                picBase.Refresh()
            End Set
        End Property

        Public Property DashStyle() As DashStyle
            Get
                Return _DashStyle
            End Get
            Set(ByVal value As DashStyle)
                _DashStyle = value
                picBase.Refresh()
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub picBase_Paint(ByVal sender As Object, ByVal e As Windows.Forms.PaintEventArgs) Handles picBase.Paint
            Dim DrawPen As New Pen(_BorderColor, _BorderWidth)

            DrawPen.DashStyle = _DashStyle

            Select Case _Tipo
                Case EnumTipo.DOWN : DrawDown(e.Graphics, DrawPen)
                Case EnumTipo.DOWN_LEFT : DrawDownLeft(e.Graphics, DrawPen)
                Case EnumTipo.DOWN_RIGHT : DrawDownRight(e.Graphics, DrawPen)
                Case EnumTipo.LEFT : DrawLeft(e.Graphics, DrawPen)
                Case EnumTipo.LEFT_DOWN : DrawLeftDown(e.Graphics, DrawPen)
                Case EnumTipo.LEFT_UP : DrawLeftUp(e.Graphics, DrawPen)
                Case EnumTipo.RIGHT : DrawRight(e.Graphics, DrawPen)
                Case EnumTipo.RIGHT_DOWN : DrawRightDown(e.Graphics, DrawPen)
                Case EnumTipo.RIGHT_UP : DrawRightUp(e.Graphics, DrawPen)
                Case EnumTipo.UP : DrawUp(e.Graphics, DrawPen)
                Case EnumTipo.UP_LEFT : DrawUpLeft(e.Graphics, DrawPen)
                Case EnumTipo.UP_RIGHT : DrawUpRight(e.Graphics, DrawPen)
            End Select
        End Sub

#End Region

#Region " Metodos "

        Private Sub DrawDown(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF((Me.Width / 2) - (_ArrowWidth / 2), 1.0F)
            Dim point2 As New PointF((Me.Width / 2) - (_ArrowWidth / 2), Me.Height - (2 * _ArrowWidth))
            Dim point3 As New PointF((Me.Width / 2) - (_ArrowWidth), Me.Height - (2 * _ArrowWidth))
            Dim point4 As New PointF((Me.Width / 2), (Me.Height - 1))
            Dim point5 As New PointF((Me.Width / 2) + (_ArrowWidth), Me.Height - (2 * _ArrowWidth))
            Dim point6 As New PointF((Me.Width / 2) + (_ArrowWidth / 2), Me.Height - (2 * _ArrowWidth))
            Dim point7 As New PointF((Me.Width / 2) + (_ArrowWidth / 2), 1.0F)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawDownLeft(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(Me.Width - 1, 1.0F)
            Dim point2 As New PointF(Me.Width - 1, Me.Height - (ArrowWidth * 0.5) - 1)
            Dim point3 As New PointF(ArrowWidth * 2, Me.Height - (ArrowWidth * 0.5) - 1)
            Dim point4 As New PointF(ArrowWidth * 2, Me.Height - 1)
            Dim point5 As New PointF(1.0F, Me.Height - ArrowWidth - 1)
            Dim point6 As New PointF(ArrowWidth * 2, Me.Height - (ArrowWidth * 2) - 1)
            Dim point7 As New PointF(ArrowWidth * 2, Me.Height - (ArrowWidth * 1.5) - 1)
            Dim point8 As New PointF(Me.Width - ArrowWidth - 1, Me.Height - (ArrowWidth * 1.5) - 1)
            Dim point9 As New PointF(Me.Width - ArrowWidth - 1, 1.0F)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawDownRight(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(ArrowWidth + 1, 1.0F)
            Dim point2 As New PointF(ArrowWidth + 1, Me.Height - (ArrowWidth * 1.5) - 1)
            Dim point3 As New PointF(Me.Width - (ArrowWidth * 2), Me.Height - (ArrowWidth * 1.5) - 1)
            Dim point4 As New PointF(Me.Width - (ArrowWidth * 2), Me.Height - (ArrowWidth * 2) - 1)
            Dim point5 As New PointF(Me.Width - 1, Me.Height - ArrowWidth - 1)
            Dim point6 As New PointF(Me.Width - (ArrowWidth * 2), Me.Height - 1)
            Dim point7 As New PointF(Me.Width - (ArrowWidth * 2), Me.Height - (ArrowWidth * 0.5) - 1)
            Dim point8 As New PointF(1.0F, Me.Height - (ArrowWidth * 0.5) - 1)
            Dim point9 As New PointF(1.0F, 1.0F)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawLeft(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF((Me.Width - 1), (Me.Height / 2) - (_ArrowWidth / 2))
            Dim point2 As New PointF((2 * _ArrowWidth), (Me.Height / 2) - (_ArrowWidth / 2))
            Dim point3 As New PointF((2 * _ArrowWidth), (Me.Height / 2) - (_ArrowWidth))
            Dim point4 As New PointF(1.0F, (Me.Height / 2))
            Dim point5 As New PointF((2 * _ArrowWidth), (Me.Height / 2) + (_ArrowWidth))
            Dim point6 As New PointF((2 * _ArrowWidth), (Me.Height / 2) + (_ArrowWidth / 2))
            Dim point7 As New PointF((Me.Width - 1), (Me.Height / 2) + (_ArrowWidth / 2))

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawLeftDown(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(Me.Width - 1, 1.0F)
            Dim point2 As New PointF((ArrowWidth * 0.5) + 1, 1.0F)
            Dim point3 As New PointF((ArrowWidth * 0.5) + 1, Me.Height - (ArrowWidth * 2))
            Dim point4 As New PointF(1.0F, Me.Height - (ArrowWidth * 2))
            Dim point5 As New PointF(ArrowWidth + 1, Me.Height - 1)
            Dim point6 As New PointF((ArrowWidth * 2) + 1, Me.Height - (ArrowWidth * 2))
            Dim point7 As New PointF((ArrowWidth * 1.5) + 1, Me.Height - (ArrowWidth * 2))
            Dim point8 As New PointF((ArrowWidth * 1.5) + 1, ArrowWidth + 1)
            Dim point9 As New PointF(Me.Width - 1, ArrowWidth + 1)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawLeftUp(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(Me.Width - 1, Me.Height - 1)
            Dim point2 As New PointF((ArrowWidth * 0.5) + 1, Me.Height - 1)
            Dim point3 As New PointF((ArrowWidth * 0.5) + 1, (ArrowWidth * 2) + 1)
            Dim point4 As New PointF(1.0F, (ArrowWidth * 2) + 1)
            Dim point5 As New PointF(ArrowWidth + 1, 1.0F)
            Dim point6 As New PointF((ArrowWidth * 2) + 1, (ArrowWidth * 2) + 1)
            Dim point7 As New PointF((ArrowWidth * 1.5) + 1, (ArrowWidth * 2) + 1)
            Dim point8 As New PointF((ArrowWidth * 1.5) + 1, Me.Height - ArrowWidth - 1)
            Dim point9 As New PointF(Me.Width - 1, Me.Height - ArrowWidth - 1)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawRight(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(1.0F, (Me.Height / 2) - (_ArrowWidth / 2))
            Dim point2 As New PointF(Me.Width - (2 * _ArrowWidth), (Me.Height / 2) - (_ArrowWidth / 2))
            Dim point3 As New PointF(Me.Width - (2 * _ArrowWidth), (Me.Height / 2) - (_ArrowWidth))
            Dim point4 As New PointF(Me.Width - 1, (Me.Height / 2))
            Dim point5 As New PointF(Me.Width - (2 * _ArrowWidth), (Me.Height / 2) + (_ArrowWidth))
            Dim point6 As New PointF(Me.Width - (2 * _ArrowWidth), (Me.Height / 2) + (_ArrowWidth / 2))
            Dim point7 As New PointF(1.0F, (Me.Height / 2) + (_ArrowWidth / 2))

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawRightDown(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(1.0F, 1)
            Dim point2 As New PointF(Me.Width - (_ArrowWidth * 0.5) - 1, 1)
            Dim point3 As New PointF(Me.Width - (_ArrowWidth * 0.5) - 1, Me.Height - (_ArrowWidth * 2))
            Dim point4 As New PointF(Me.Width - 1, Me.Height - (_ArrowWidth * 2))
            Dim point5 As New PointF(Me.Width - _ArrowWidth - 1, Me.Height - 1)
            Dim point6 As New PointF(Me.Width - (_ArrowWidth * 2) - 1, Me.Height - (_ArrowWidth * 2))
            Dim point7 As New PointF(Me.Width - (_ArrowWidth * 1.5) - 1, Me.Height - (_ArrowWidth * 2))
            Dim point8 As New PointF(Me.Width - (_ArrowWidth * 1.5) - 1, _ArrowWidth + 1)
            Dim point9 As New PointF(1.0F, _ArrowWidth + 1)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawRightUp(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(1.0F, Me.Height - _ArrowWidth - 1)
            Dim point2 As New PointF(Me.Width - (_ArrowWidth * 1.5) - 1, Me.Height - _ArrowWidth - 1)
            Dim point3 As New PointF(Me.Width - (_ArrowWidth * 1.5) - 1, (_ArrowWidth * 2))
            Dim point4 As New PointF(Me.Width - (2 * _ArrowWidth) - 1, (_ArrowWidth * 2))
            Dim point5 As New PointF(Me.Width - _ArrowWidth - 1, 1.0F)
            Dim point6 As New PointF(Me.Width - 1, (_ArrowWidth * 2))
            Dim point7 As New PointF(Me.Width - (_ArrowWidth * 0.5) - 1, (_ArrowWidth * 2))
            Dim point8 As New PointF(Me.Width - (_ArrowWidth * 0.5) - 1, Me.Height - 1)
            Dim point9 As New PointF(1.0F, Me.Height - 1)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawUp(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF((Me.Width / 2) - (_ArrowWidth / 2), (Me.Height - 1))
            Dim point2 As New PointF((Me.Width / 2) - (_ArrowWidth / 2), (2 * _ArrowWidth))
            Dim point3 As New PointF((Me.Width / 2) - (_ArrowWidth), (2 * _ArrowWidth))
            Dim point4 As New PointF((Me.Width / 2), 1.0F)
            Dim point5 As New PointF((Me.Width / 2) + (_ArrowWidth), (2 * _ArrowWidth))
            Dim point6 As New PointF((Me.Width / 2) + (_ArrowWidth / 2), (2 * _ArrowWidth))
            Dim point7 As New PointF((Me.Width / 2) + (_ArrowWidth / 2), (Me.Height - 1))

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawUpLeft(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(Me.Width - 1, Me.Height - 1)
            Dim point2 As New PointF(Me.Width - 1, (ArrowWidth * 0.5) + 1)
            Dim point3 As New PointF(ArrowWidth * 2, (ArrowWidth * 0.5) + 1)
            Dim point4 As New PointF(ArrowWidth * 2, 1.0F)
            Dim point5 As New PointF(1.0F, ArrowWidth + 1)
            Dim point6 As New PointF(ArrowWidth * 2, (ArrowWidth * 2) + 1)
            Dim point7 As New PointF(ArrowWidth * 2, (ArrowWidth * 1.5) + 1)
            Dim point8 As New PointF(Me.Width - ArrowWidth - 1, (ArrowWidth * 1.5) + 1)
            Dim point9 As New PointF(Me.Width - ArrowWidth - 1, Me.Height - 1)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub
        Private Sub DrawUpRight(ByVal g As Graphics, ByRef DrawPen As Pen)
            Dim DrawBrush As New SolidBrush(_FillColor)

            'g.Clear(BackColorAroow)

            Dim point1 As New PointF(1.0F, Me.Height - 1)
            Dim point2 As New PointF(1.0F, (ArrowWidth * 0.5) + 1)
            Dim point3 As New PointF(Me.Width - (ArrowWidth * 2), (ArrowWidth * 0.5) + 1)
            Dim point4 As New PointF(Me.Width - (ArrowWidth * 2), 1.0F)
            Dim point5 As New PointF(Me.Width - 1, ArrowWidth + 1)
            Dim point6 As New PointF(Me.Width - (ArrowWidth * 2), (ArrowWidth * 2) + 1)
            Dim point7 As New PointF(Me.Width - (ArrowWidth * 2), (ArrowWidth * 1.5) + 1)
            Dim point8 As New PointF(ArrowWidth + 1, (ArrowWidth * 1.5) + 1)
            Dim point9 As New PointF(ArrowWidth + 1, Me.Height - 1)

            Dim curvePoints As PointF() = {point1, point2, point3, point4, point5, point6, point7, point8, point9}
            Const newFillMode As FillMode = FillMode.Winding

            g.FillPolygon(DrawBrush, curvePoints, newFillMode)
            g.DrawPolygon(DrawPen, curvePoints)

        End Sub

        Private Sub InitializeComponent()
            Me.picBase = New Windows.Forms.PictureBox()
            CType(Me.picBase, ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()

            'picBase
            Me.picBase.BackColor = Color.Transparent
            Me.picBase.Dock = Windows.Forms.DockStyle.Fill
            Me.picBase.Location = New Point(0, 0)
            Me.picBase.Name = "picBase"
            Me.picBase.Size = New Size(150, 150)
            Me.picBase.TabIndex = 1
            Me.picBase.TabStop = False

            'Flecha
            Me.Controls.Add(Me.picBase)
            Me.Name = "Flecha"
            CType(Me.picBase, ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

            MyBase.BackColor = Color.Transparent
        End Sub

#End Region

    End Class

End Namespace