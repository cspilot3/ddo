Imports System.Reflection
Imports System.Windows.Forms
Imports System.IO

Public Class Program

#Region " Declaraciones "

    Public Shared Conexion As OleDb.OleDbConnection = Nothing

    Public Shared DataBaseName As String = "ExportedData.accdb"

    '  Private Const AppPath1 As String = "D:\Exportacion\"
    Public Shared ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & AppPath & DataBaseName & ";Persist Security Info=False"
    'Public Shared ConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & AppPath1 & DataBaseName
#End Region

#Region " Propiedades "

    Friend Shared ReadOnly Property AssemblyTitle() As String
        Get
            ' Get all Title attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute), False)

            ' If there is at least one Title attribute
            If (attributes.Length > 0) Then
                ' Select the first one
                Dim titleAttribute As AssemblyTitleAttribute = CType(attributes(0), AssemblyTitleAttribute)

                ' If it is not an empty string, return it
                If (titleAttribute.Title <> "") Then Return titleAttribute.Title

            End If

            ' If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
            Return Path.GetFileNameWithoutExtension([Assembly].GetExecutingAssembly().CodeBase)
        End Get
    End Property
    Friend Shared ReadOnly Property AssemblyVersion() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Version.ToString()
        End Get
    End Property
    Friend Shared ReadOnly Property AssemblyName() As String
        Get
            Return [Assembly].GetExecutingAssembly().GetName().Name
        End Get
    End Property
    Friend Shared ReadOnly Property AssemblyDescription() As String
        Get
            ' Get all Description attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)

            ' If there aren't any Description attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Description attribute, return its value
            Return CType(attributes(0), AssemblyDescriptionAttribute).Description
        End Get
    End Property
    Friend Shared ReadOnly Property AssemblyProduct() As String
        Get
            ' Get all Product attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyProductAttribute), False)

            ' If there aren't any Product attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Product attribute, return its value
            Return CType(attributes(0), AssemblyProductAttribute).Product
        End Get
    End Property
    Friend Shared ReadOnly Property AssemblyCopyright() As String
        Get
            ' Get all Copyright attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)

            ' If there aren't any Copyright attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Copyright attribute, return its value
            Return CType(attributes(0), AssemblyCopyrightAttribute).Copyright
        End Get
    End Property
    Friend Shared ReadOnly Property AssemblyCompany() As String
        Get
            ' Get all Company attributes on this assembly
            Dim attributes As Object() = [Assembly].GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)

            ' If there aren't any Company attributes, return an empty string
            If (attributes.Length = 0) Then Return ""

            ' If there is a Company attribute, return its value
            Return CType(attributes(0), AssemblyCompanyAttribute).Company
        End Get
    End Property

    Friend Shared ReadOnly Property CrLf() As String
        Get
            Return Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString()
        End Get
    End Property
    Friend Shared ReadOnly Property AppPath() As String
        Get
            Return Application.StartupPath.TrimEnd("\"c) + "\"
        End Get
    End Property

#End Region

#Region " Metodos "

    Public Shared Sub Main()
        Dim SplashForm As New FormSplash()

#If Not Debug Then
        SplashForm.Show()
        Application.DoEvents()
#End If

        Dim MainForm As New FormOffLineViewer()

        Try
            Conexion = New OleDb.OleDbConnection(ConnectionString)

            Conexion.Open()

            MainForm.Load_Data()

            SplashForm.Close()
            MainForm.ShowDialog()

        Catch ex As Exception
            SplashForm.Close()
            Application.DoEvents()

            MessageBox.Show(ex.Message, AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (Conexion IsNot Nothing) Then Conexion.Close()
        End Try
    End Sub

#End Region

End Class
