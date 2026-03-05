Imports System.Windows.Forms
Imports Banagrario.Plugin.Imaging.Controls

Namespace Imaging.Forms.Cargue

    Public Class FormCargue

#Region " Declaraciones "

        Private _urlValidar As String
        'Private _Plugin As BanagrarioImagingPlugin

#End Region

#Region " Eventos "

        Private Sub dgvItems_DataBindingComplete(ByVal sender As System.Object, ByVal e As DataGridViewBindingCompleteEventArgs) Handles dgvItems.DataBindingComplete
            lblItems.Text = "Items - [ " & dgvItems.RowCount & " ]"
            For Each itemRow As DataGridViewRow In dgvItems.Rows
                Dim valido = CBool(itemRow.Cells(ValidoDataGridViewCheckBoxColumn.Index).Value)

                If (Not valido) Then
                    itemRow.DefaultCellStyle.ForeColor = Drawing.Color.Red
                End If
            Next
        End Sub

        Private Sub dgvPaquetes_DataBindingComplete(ByVal sender As System.Object, ByVal e As DataGridViewBindingCompleteEventArgs) Handles dgvPaquetes.DataBindingComplete
            lblPaquetes.Text = "Paquetes - [ " & dgvPaquetes.RowCount & " ]"
            For Each paqueteRow As DataGridViewRow In dgvPaquetes.Rows
                Dim valido = CBool(paqueteRow.Cells(PaqueteValido.Index).Value)

                If (Not valido) Then
                    paqueteRow.DefaultCellStyle.ForeColor = Drawing.Color.Red
                End If
            Next
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub SetData(ByRef nData As Miharu.Imaging.Library.Procesos.Cargue.CargueBase)
            dsCargue = nData.Datos

            dgvPaquetes.DataSource = dsCargue
            dgvItems.DataSource = dsCargue

            dgvPaquetes.Refresh()
            dgvItems.Refresh()

            lblValidos.Text = CStr(nData.Validos)
            lblInvalidos.Text = CStr(nData.Invalidos)
            lblTotal.Text = CStr(nData.Validos + nData.Invalidos)

            AceptarButton.Enabled = (nData.Validos > 0)
            _urlValidar = nData.Paquetes(0).TrimEnd("\"c)
        End Sub

        Public Sub SetData(ByVal nData As PluginCargueController)
            dsCargue = nData.Datos

            dgvPaquetes.DataSource = dsCargue
            dgvItems.DataSource = dsCargue

            dgvPaquetes.Refresh()
            dgvItems.Refresh()

            lblValidos.Text = CStr(nData.Validos)
            lblInvalidos.Text = CStr(nData.Invalidos)
            lblTotal.Text = CStr(nData.Validos + nData.Invalidos)

            AceptarButton.Enabled = (nData.Validos > 0)
            _urlValidar = nData.Paquetes(0).TrimEnd("\"c)
        End Sub

        Public Sub SetData(ByVal nData As PluginCargueMixtoController)
            dsCargue = nData.Datos

            dgvPaquetes.DataSource = dsCargue
            dgvItems.DataSource = dsCargue

            dgvPaquetes.Refresh()
            dgvItems.Refresh()

            lblValidos.Text = CStr(nData.Validos)
            lblInvalidos.Text = CStr(nData.Invalidos)
            lblTotal.Text = CStr(nData.Validos + nData.Invalidos)

            AceptarButton.Enabled = (nData.Validos > 0)
            _urlValidar = nData.Paquetes(0).TrimEnd("\"c)
        End Sub

#End Region

#Region " Funciones "

        Public Function ValidaPath() As Boolean
            Try
                Dim objFormValidar As New FormValidarPath()
                Dim nombreFolder As String

                objFormValidar.ShowDialog()
                ' ReSharper disable once VBStringLastIndexOfIsCultureSpecific.1
                nombreFolder = _urlValidar.Substring(_urlValidar.LastIndexOf("\") + 1) 'Path.GetDirectoryName(UrlValidar)


                If (objFormValidar.Codigo_Oficina = nombreFolder.Substring(0, 4) And _
                    objFormValidar.Fecha_Proceso = nombreFolder.Substring(4, 8)) Then
                    Return True
                End If
                Application.DoEvents()
            Catch ex As Exception
                Return False
            End Try

            Return False
        End Function

#End Region

    End Class

End Namespace