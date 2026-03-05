Imports DBCore.SchemaImaging
Imports System.Drawing

Namespace View.Recorte

    Public Class CalidadDataControl
        Implements IDataControl

#Region " Declaraciones "

        Public Enum EnumOption
            Undefined
            OK
            [Error]
        End Enum

#End Region

#Region " Propiedades "

        Private _Option As EnumOption
        Public Property [Option] As EnumOption
            Get
                Return _Option
            End Get
            Set(value As EnumOption)
                _Option = value

                OKRadioButton.Checked = (_Option = EnumOption.OK)
                ErrorRadioButton.Checked = (_Option = EnumOption.Error)

                Select Case _Option
                    Case EnumOption.Undefined
                        DataButton.ForeColor = Color.Black

                    Case EnumOption.OK
                        DataButton.ForeColor = Color.Green

                    Case EnumOption.Error
                        DataButton.ForeColor = Color.Red

                End Select
            End Set
        End Property

#End Region

#Region " Eventos "

        Public Event OnDataButtonClick(ByVal sender As CalidadDataControl)
        Public Event OnOkButtonClick(ByVal sender As CalidadDataControl)
        Public Event OnErrorButtonClick(ByVal sender As CalidadDataControl)

        Private Sub DataButton_Click(sender As System.Object, e As EventArgs) Handles DataButton.Click
            RaiseEvent OnDataButtonClick(Me)
        End Sub

        Private Sub OKRadioButton_Click(sender As System.Object, e As EventArgs) Handles OKRadioButton.Click
            If (OKRadioButton.Checked) Then
                Me.Option = EnumOption.OK
                RaiseEvent OnOkButtonClick(Me)
            End If
        End Sub

        Private Sub ErrorRadioButton_Click(sender As System.Object, e As EventArgs) Handles ErrorRadioButton.Click
            If (ErrorRadioButton.Checked) Then
                Me.Option = EnumOption.Error
                RaiseEvent OnErrorButtonClick(Me)
            End If
        End Sub

#End Region

#Region " Implementacion IDataControl "

        Public Property id As Short Implements IDataControl.id

        Public Property Label As String Implements IDataControl.Label
            Get
                Return DataButton.Text
            End Get
            Set(value As String)
                DataButton.Text = value
            End Set
        End Property

        Private _Selected As Boolean
        Public Property Selected As Boolean Implements IDataControl.Selected
            Get
                Return _Selected
            End Get
            Set(value As Boolean)
                _Selected = value

                If (_Selected) Then
                    DataButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                Else
                    DataButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                End If

                OptionPanel.Visible = _Selected
            End Set
        End Property

        Private _Selector As Selector
        Public Property Selector As Selector Implements IDataControl.Selector
            Get
                Return _Selector
            End Get
            Set(value As Selector)
                _Selector = value
            End Set
        End Property

        Public Property Data As TBL_File_RecorteRow Implements IDataControl.Data

#End Region

    End Class

End Namespace