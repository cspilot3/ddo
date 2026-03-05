<Serializable()> _
Public Class BindListControlInfo
    'Public ControlType As Type = GetType(Object)
    Public ControlID As String = ""
    Public AddEmptyRow As Boolean = True
    Public TextEmptyText As String = "-"
    Public TextEmptyValue As String = ""

    Public Sub New()
    End Sub

    Public Sub New(ByVal _ControlID As String, ByVal _AddEmptyRow As Boolean, ByVal _TextEmptyText As String, ByVal _TextEmptyValue As String) 'ByVal _ControlType As Type
        'ControlType = _ControlType
        ControlID = _ControlID
        AddEmptyRow = _AddEmptyRow
        TextEmptyText = _TextEmptyText
        TextEmptyValue = _TextEmptyValue
    End Sub
End Class

Public Class BindListControlInfoCollection
    Inherits System.Collections.Generic.List(Of BindListControlInfo)

    Default Public Overloads Property Item(ByVal index As Integer) As BindListControlInfo
        Get
            Return MyBase.Item(index)
        End Get
        Set(ByVal value As BindListControlInfo)
            MyBase.Item(index) = value
        End Set
    End Property

    Default Public Overloads Property Item(ByVal controlID As String) As BindListControlInfo
        Get
            For i As Integer = 0 To Count - 1
                If (MyBase.Item(i).ControlID = controlID) Then
                    Return MyBase.Item(i)
                End If
            Next
            Return Nothing
        End Get
        Set(ByVal value As BindListControlInfo)
            For i As Integer = 0 To Count - 1
                If (MyBase.Item(i).ControlID = controlID) Then
                    MyBase.Item(i) = value
                End If
            Next
        End Set
    End Property

    Public Shadows Sub Add(ByVal value As BindListControlInfo)
        For i As Integer = 0 To Count - 1
            If (MyBase.Item(i).ControlID = value.ControlID) Then
                MyBase.Item(i) = value
                Return
            End If
        Next
        MyBase.Add(value)
    End Sub

    Public Shadows Function Contains(ByVal controlID As String) As Boolean
        Return (Not Item(controlID) Is Nothing)
    End Function

End Class