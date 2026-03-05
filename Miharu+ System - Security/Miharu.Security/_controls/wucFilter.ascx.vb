Imports Miharu.Security._clases

Namespace _controls

    Partial Public Class wucFilter
        Inherits UserWebControlBase

#Region " Declaraciones "

        Public Event Click(ByVal nParametro As String)

#End Region

#Region " Propiedades "

        Public Property Parametro() As String
            Get
                If ViewState("FiltroText") Is Nothing Then
                    ViewState("FiltroText") = ""
                End If

                Return CStr(ViewState("FiltroText"))
            End Get
            Protected Set(ByVal value As String)
                ViewState("FiltroText") = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Config_Page()
        End Sub

        Protected Sub btnNinguno_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNinguno.Click
            Filtrar("")
        End Sub

        Protected Sub btnTotos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTotos.Click
            Filtrar("*")
        End Sub

        Protected Sub btnAD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAD.Click
            Filtrar("[A-D]*")
        End Sub

        Protected Sub btnEH_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEH.Click
            Filtrar("[E-H]*")
        End Sub

        Protected Sub btnIL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIL.Click
            Filtrar("[I-L]*")
        End Sub

        Protected Sub btnMP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMP.Click
            Filtrar("[M-P]*")
        End Sub

        Protected Sub btnQT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQT.Click
            Filtrar("[Q-T]*")
        End Sub

        Protected Sub btnUZ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUZ.Click
            Filtrar("[U-Z]*")
        End Sub

#End Region

#Region " Metodos "

        Private Sub Filtrar(ByVal nParametro As String)
            Parametro = nParametro

            Config_Page()

            RaiseEvent Click(nParametro)
        End Sub

        Private Sub Config_Page()
            btnNinguno.CssClass = "Button"
            btnTotos.CssClass = "Button"
            btnAD.CssClass = "Button"
            btnEH.CssClass = "Button"
            btnIL.CssClass = "Button"
            btnMP.CssClass = "Button"
            btnQT.CssClass = "Button"
            btnUZ.CssClass = "Button"

            Select Case Parametro
                Case "*"
                    btnTotos.CssClass = "Button_Selected"

                Case "[A-D]*"
                    btnAD.CssClass = "Button_Selected"

                Case "[E-H]*"
                    btnEH.CssClass = "Button_Selected"

                Case "[I-L]*"
                    btnIL.CssClass = "Button_Selected"

                Case "[M-P]*"
                    btnMP.CssClass = "Button_Selected"

                Case "[Q-T]*"
                    btnQT.CssClass = "Button_Selected"

                Case "[U-Z]*"
                    btnUZ.CssClass = "Button_Selected"

                Case Else
                    btnNinguno.CssClass = "Button_Selected"

            End Select
        End Sub

#End Region

    End Class
End Namespace