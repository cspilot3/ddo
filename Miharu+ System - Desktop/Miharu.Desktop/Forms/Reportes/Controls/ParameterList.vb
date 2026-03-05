Namespace Forms.Reportes.Controls

    Public Class ParameterList
        Implements IParameter

#Region " Declaraciones"

        Public Event LoadList(sender As ParameterList)

#End Region

#Region " Propiedades "

        Public Property DataSource() As Object
            Get
                Return Me.valueComboBox.DataSource
            End Get
            Set(value As Object)
                Me.valueComboBox.DataSource = value
            End Set
        End Property

        Public Property DisplayMember() As String
            Get
                Return Me.valueComboBox.DisplayMember
            End Get
            Set(value As String)
                Me.valueComboBox.DisplayMember = value
            End Set
        End Property

        Public Property ValueMember() As String
            Get
                Return Me.valueComboBox.ValueMember
            End Get
            Set(value As String)
                Me.valueComboBox.ValueMember = value
            End Set
        End Property

        Public Property Query() As String

#End Region

#Region " Implementacion IParameter "

        Public Function GetParameter() As Object Implements IParameter.GetParameter
            Return Me.Text
        End Function

        Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
            If (nullCheckBox.Checked) Then
                Return "null"
            ElseIf (Me.valueComboBox.SelectedIndex < 0) Then
                Return "[" & Me.ParameterName & "]"
            End If

            Return Me.valueComboBox.SelectedValue.ToString()
        End Function

        Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType
            Get
                Return ParameterTypeEnum.Texto
            End Get
        End Property

        Public Property ParameterName As String Implements IParameter.ParameterName

#End Region

        Private Sub valueComboBox_Enter(sender As System.Object, e As EventArgs) Handles valueComboBox.Enter
            RaiseEvent LoadList(Me)
        End Sub
    End Class

End Namespace