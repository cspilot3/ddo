Namespace Procesos.Configuracion.Imaging
    Public Class FormParValidaciones_Constante
        Implements IParValidaciones

#Region " Eventos "

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Me.DialogResult = Windows.Forms.DialogResult.None
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

#Region " Implementacion IParValidaciones "

        Public Property Campo_1 As Slyg.Tools.SlygNullable(Of Short) Implements IParValidaciones.Campo_1
            Get
                Return CShort(-1)
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of Short))

            End Set
        End Property

        Public Property Campo_2 As Slyg.Tools.SlygNullable(Of Short) Implements IParValidaciones.Campo_2
            Get
                Return CShort(-1)
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of Short))

            End Set
        End Property

        Public Property Operador_Comparacion As Slyg.Tools.SlygNullable(Of String) Implements IParValidaciones.Operador_Comparacion
            Get
                Return ""
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of String))

            End Set
        End Property

        Public Property Respuesta As Boolean Implements IParValidaciones.Respuesta
            Get
                Return Me.TrueRadioButton.Checked
            End Get
            Set(value As Boolean)
                Me.TrueRadioButton.Checked = value
                Me.FalseRadioButton.Checked = Not value
            End Set
        End Property

        Public Property Valor_Comparacion As Slyg.Tools.SlygNullable(Of String) Implements IParValidaciones.Valor_Comparacion
            Get
                Return ""
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of String))

            End Set
        End Property

        Public Sub setData(nCampos As DBImaging.SchemaConfig.CTA_CampoDataTable, nParametros As DBImaging.SchemaConfig.TBL_ParametroDataTable) Implements IParValidaciones.setData

        End Sub

#End Region

    End Class
End Namespace