Namespace DesktopReportDataGridView
    Public Class FormParametrosExportacion2

#Region "Declaraciones"
        Private Salto_Linea_ As String
#End Region

#Region " Propiedades "

        Public ReadOnly Property ManejaEncabezado() As String
            Get
                Return chkEncabezado.Checked
            End Get
        End Property

        Public ReadOnly Property SeparadoComa() As String
            Get
                Return ComaRadioButton.Checked
            End Get
        End Property
        Public ReadOnly Property SeparadoTabulador() As String
            Get
                Return TabuladorRadioButton.Checked
            End Get
        End Property
        Public ReadOnly Property SeparadoPuntoyComa() As String
            Get
                Return PuntoComaRadioButton.Checked
            End Get
        End Property
        Public ReadOnly Property SeparadoVacio() As String
            Get
                Return VacioRadioButton.Checked
            End Get
        End Property

        Public ReadOnly Property CodificacionArchivo() As String
            Get
                Return CodificacionArchivoComboBox.SelectedValue.ToString()
            End Get
        End Property

        Public Property Salto_linea() As String
            Get
                Return Salto_Linea_
            End Get
            Set(value As String)
                Salto_Linea_ = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub ok_Click(sender As System.Object, e As System.EventArgs) Handles ok.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region


    End Class
End Namespace