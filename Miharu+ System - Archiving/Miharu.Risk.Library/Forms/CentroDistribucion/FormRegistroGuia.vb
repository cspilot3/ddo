Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.CentroDistribucion

Namespace Forms.CentroDistribucion

    Public Class FormRegistroGuia
        Inherits FormBase

        Private _Estado As Integer = 1
        Private _NroGuia, _NroRemision As String
        Private _id_Remision_Caja As Long

        Public Property id_Remision_Caja As Long
            Get
                Return _id_Remision_Caja
            End Get
            Set(value As Long)
                _id_Remision_Caja = value
            End Set
        End Property


        Public Property NroRemision As String
            Get
                Return _NroRemision
            End Get
            Set(value As String)
                _NroRemision = value
            End Set
        End Property

        Public Property NroGuia As String
            Get
                Return _NroGuia
            End Get
            Set(value As String)
                _NroGuia = value
            End Set
        End Property

        Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click
            If ValidarCampos() Then
                If _Estado = 1 Then
                    _NroGuia = txtNumeroGuia.Text
                    _NroRemision = txtNumeroRemision.Text
                    txtNumeroGuia.Clear()
                    txtNumeroRemision.Clear()
                    _Estado += 1
                    lblRegistroRemisionGuia.Text = "CONFIRME EL NÚMERO DE REMISION Y GUIA ASOCIADA:"
                    txtNumeroRemision.Focus()
                Else
                    If _Estado = 2 And NroGuia = txtNumeroGuia.Text And NroRemision = txtNumeroRemision.Text Then
                        RegistraGuia()
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        _Estado = 1
                        MsgBox("Los datos ingresados no coinciden, se cancela el proceso")
                        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    End If
                    lblRegistroRemisionGuia.Text = "INGRESE EL NÚMERO DE REMISION Y GUIA ASOCIADA:"
                    txtNumeroRemision.Focus()
                End If
            End If
        End Sub

        Private Sub btnCerrar_Click(sender As System.Object, e As System.EventArgs) Handles btnCerrar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

        Private Function ValidarCampos() As Boolean
            If txtNumeroRemision.Text <> "" And txtNumeroGuia.Text = "" Then
                MsgBox("Debe diligenciar el campo Número de Guia", MsgBoxStyle.Information, "Datos")
                Return False
            ElseIf txtNumeroRemision.Text = "" And txtNumeroGuia.Text <> "" Then
                MsgBox("Debe diligenciar el campo Número de Remisión", MsgBoxStyle.Information, "Datos")
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub RegistraGuia()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                Dim Remision As New DBCore.SchemaProcess.TBL_Remision_CajaType
                Remision.Nro_Guia = txtNumeroGuia.Text
                Remision.Nro_Remision = txtNumeroRemision.Text
                Remision.Remitida = True
                dbmCore.SchemaProcess.TBL_Remision_Caja.DBUpdate(Remision, id_Remision_Caja)
                dbmCore.Transaction_Commit()

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearRemision", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub txtNumeroRemision_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroRemision.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                txtNumeroGuia.Focus()
            End If
        End Sub

        Private Sub txtNumeroGuia_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroGuia.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                btnAceptar.Focus()
            End If
        End Sub
    End Class
End Namespace
