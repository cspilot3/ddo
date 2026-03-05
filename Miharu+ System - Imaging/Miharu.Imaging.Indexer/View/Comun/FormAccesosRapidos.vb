Imports System.Windows.Forms

Namespace View.Comun

    Public Class FormAccesosRapidos

#Region " Estructuras "

        Public Structure TypeAcceso
            Public Acceso As String
            Public Descripción As String

            Public Sub New(ByVal nAcceso As String, ByVal nDescripcion As String)
                Me.Acceso = nAcceso
                Me.Descripción = nDescripcion
            End Sub

        End Structure

#End Region

#Region " Declaraciones "

        Private _Lista As New List(Of TypeAcceso)

#End Region

#Region " Propiedades "

        Public ReadOnly Property Lista() As List(Of TypeAcceso)
            Get
                Return _Lista
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub FormAccesosRapidos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            ShowData()
        End Sub
        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub ShowData()
            lvAcceso.Items.Clear()

            For Each Item In _Lista
                lvAcceso.Items.Add(New ListViewItem(New String() {"", Item.Acceso, Item.Descripción}))
            Next
        End Sub

#End Region

    End Class

End Namespace