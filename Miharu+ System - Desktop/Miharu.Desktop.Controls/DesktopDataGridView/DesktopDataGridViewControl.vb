Imports System.Windows.Forms

Namespace DesktopDataGridView

    Public Class DesktopDataGridViewControl
        Inherits Windows.Forms.DataGridView

#Region " Constructor "

        Public Sub New()
            AsignarPropiedades()
        End Sub

#End Region

#Region " Eventos "

        Private Sub DesktopDataGridView_KeyPress(ByVal sender As System.Object, ByVal e As Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub AsignarPropiedades()

            'Estilos Grilla
            Me.BackgroundColor = Drawing.Color.White
            Me.GridColor = Drawing.Color.Gainsboro

            'Estilos Columnas Grilla
            Me.ColumnHeadersDefaultCellStyle.BackColor = Drawing.Color.Gainsboro
            Me.ColumnHeadersDefaultCellStyle.SelectionBackColor = Drawing.Color.Gainsboro


            'Estilos Filas Grilla
            Me.RowHeadersDefaultCellStyle.BackColor = Drawing.Color.Gainsboro
            Me.RowHeadersDefaultCellStyle.SelectionBackColor = Drawing.Color.Gainsboro


            'Estilos Celdas Grilla
            Me.DefaultCellStyle.SelectionBackColor = Drawing.Color.LightGray
            Me.DefaultCellStyle.SelectionForeColor = Drawing.Color.Black


            'Funcionalidades Seleccion
            Me.MultiSelect = False
            Me.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            'Me.ReadOnly = True

            'Funcionalidades Columnas
            Me.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        End Sub

#End Region

    End Class

End Namespace