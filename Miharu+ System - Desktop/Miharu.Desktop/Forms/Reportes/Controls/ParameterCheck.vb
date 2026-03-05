Namespace Forms.Reportes.Controls

    Public Class ParameterCheck
        Implements IParameter

#Region " Implementacion IParameter "

        Public Function GetParameter() As Object Implements IParameter.GetParameter
            Return Me.Text
        End Function

        Public Function GetStringParameter() As String Implements IParameter.GetStringParameter
            If (nullCheckBox.Checked) Then
                Return "null"
            End If

            Return CStr(IIf(valueCheckBox.Checked, "1", "0"))
        End Function

        Public ReadOnly Property ParameterType As ParameterTypeEnum Implements IParameter.ParameterType
            Get
                Return ParameterTypeEnum.Texto
            End Get
        End Property

        Public Property ParameterName As String Implements IParameter.ParameterName

#End Region

    End Class

End Namespace