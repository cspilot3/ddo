Namespace Procesos.Configuracion.Imaging

    Public Class FormParValidaciones_ComparacionParametro
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
            If (Me.Campo1ComboBox.SelectedIndex = 0) Then
                Me.Campo1ComboBox.Focus()
                MessageBox.Show("Debe seleccionar el campo 1", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf (Me.ParametroComboBox.SelectedIndex = 0) Then
                Me.ParametroComboBox.Focus()
                MessageBox.Show("Debe seleccionar el campo 2", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Return True
            End If

            Return False
        End Function

#End Region

#Region " Implementacion IParValidaciones "

        Public Property Campo_1 As Slyg.Tools.SlygNullable(Of Short) Implements IParValidaciones.Campo_1
            Get
                Return CShort(CType(Me.Campo1ComboBox.SelectedItem, Slyg.Tools.Item).Value)
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of Short))
                For Each Item As Slyg.Tools.Item In Me.Campo1ComboBox.Items
                    If (CShort(Item.Value) = value.Value) Then
                        Me.Campo1ComboBox.SelectedItem = Item
                    End If
                Next
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
                Return Me.OperadorComboBox.Text
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of String))
                Me.OperadorComboBox.Text = value.Value
            End Set
        End Property

        Public Property Respuesta As Boolean Implements IParValidaciones.Respuesta
            Get
                Return False
            End Get
            Set(value As Boolean)

            End Set
        End Property

        Public Property Valor_Comparacion As Slyg.Tools.SlygNullable(Of String) Implements IParValidaciones.Valor_Comparacion
            Get
                Return Me.ParametroComboBox.Text
            End Get
            Set(value As Slyg.Tools.SlygNullable(Of String))
                Me.ParametroComboBox.Text = value.Value
            End Set
        End Property

        Public Sub setData(nCampos As DBImaging.SchemaConfig.CTA_CampoDataTable, nParametros As DBImaging.SchemaConfig.TBL_ParametroDataTable) Implements IParValidaciones.setData
            Me.Campo1ComboBox.Items.Clear()
            Me.Campo1ComboBox.ValueMember = "Value"
            Me.Campo1ComboBox.DisplayMember = "Display"

            Me.Campo1ComboBox.Items.Add(New Slyg.Tools.Item(-1, "- Seleccionar un campo - "))
            For Each Campo In nCampos
                Me.Campo1ComboBox.Items.Add(New Slyg.Tools.Item(Campo.id_Campo, Campo.Nombre_Campo))
            Next

            Me.Campo1ComboBox.SelectedIndex = 0


            Me.ParametroComboBox.Items.Clear()
            Me.ParametroComboBox.ValueMember = "Value"
            Me.ParametroComboBox.DisplayMember = "Display"

            Me.ParametroComboBox.Items.Add(New Slyg.Tools.Item(-1, "- Seleccionar un parámetro - "))
            For Each Parametro In nParametros
                Me.ParametroComboBox.Items.Add(New Slyg.Tools.Item(Parametro.Nombre_Parametro, Parametro.Nombre_Parametro))
            Next

            Me.ParametroComboBox.SelectedIndex = 0

            Me.OperadorComboBox.SelectedIndex = 0
        End Sub

#End Region

    End Class

End Namespace