Imports System.Windows.Forms

Namespace Progress

    Public Class FormProgress

#Region " Declaraciones "

        Private _Cancelar As Boolean
        Private _LastValue As Integer
        Private _CanCancel As Boolean

#End Region

#Region " Propiedades "

        Public ReadOnly Property Cancelar() As Boolean
            Get
                Return _Cancelar
            End Get
        End Property

        Public Property Cancelar_() As Boolean
            Get

            End Get
            Set(value As Boolean)
                _Cancelar = value
            End Set
        End Property


        Public Property CanCancel() As Boolean
            Get
                Return _CanCancel
            End Get
            Set(ByVal value As Boolean)
                _CanCancel = value
                btnCancelar.Enabled = Not _Cancelar And value
            End Set
        End Property

        Public Property Style As ProgressBarStyle
            Get
                Return Me.pgbContador.Style
            End Get
            Set(ByVal value As ProgressBarStyle)
                Me.pgbContador.Style = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            _Cancelar = True
            btnCancelar.Enabled = False
        End Sub

#End Region

#Region " Metodos "

        Public Sub SetProceso(ByVal nTexto As String)
            lblProceso.Text = nTexto
        End Sub

        Public Sub SetAccion(ByVal nTexto As String)
            lblAccion.Text = nTexto
        End Sub

        Public Sub SetMaxValue(ByVal nValor As Integer)
            pgbContador.Maximum = nValor
        End Sub

        Public Sub SetProgreso(ByVal nValor As Integer)
            Dim Progreso As Single

            pgbContador.Value = CInt(IIf(pgbContador.Maximum > nValor, nValor, pgbContador.Maximum))

            Progreso = CSng(IIf(pgbContador.Maximum > 0, nValor / pgbContador.Maximum, 0)) * 100

            lblProgreso.Text = Format(Progreso, "#0") & "%"
            'lblProgreso.Text = CStr(IIf(Progreso Mod 1 > 0, Format(Progreso, "#0.00"), Progreso)) & "%"

            If nValor < _LastValue Then
                _LastValue = 0
                Me.Refresh()
                Windows.Forms.Application.DoEvents()

            ElseIf ((nValor - _LastValue) / pgbContador.Maximum) > 0.01 Then
                _LastValue = pgbContador.Value
                Me.Refresh()
                Windows.Forms.Application.DoEvents()

            End If
        End Sub
        
#End Region

    End Class

End Namespace