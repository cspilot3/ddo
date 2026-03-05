Imports System.Reflection
Imports System.Configuration

Public Class Program
#Region " Enumeraciones "

    Public Enum Modulo As Byte
        Security = 0
        Imaging = 2
        Core = 6
        SofTrac = 35
        Integration = 32
    End Enum

    Public Class Contenedor
        Public Valido As Boolean
        Public Observacion As String
        Public PrecintoId As String
        Public ContenedorId As Short
        Public CBarrasContenedor As String
    End Class

#End Region

#Region " Declaraciones "

    Public Shared Config As New SFTPTransferConfig
    Public Shared ConnectionStrings As SFTPTransferConfig.TypeConnectionString

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
            Return System.IO.Path.GetFileNameWithoutExtension([Assembly].GetExecutingAssembly().CodeBase)
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

    Friend Shared ReadOnly Property AppDataPath() As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd("\"c) + "\PYC\Miharu.SFTPTransfer\"
        End Get
    End Property

    Public Shared ReadOnly Property IdentifierDateFormat() As String
        Get
            Dim rootWebConfig As String = ConfigurationManager.AppSettings("IdentifierDateFormat")
            If rootWebConfig IsNot Nothing AndAlso rootWebConfig.Length > 0 Then
                Return rootWebConfig
            Else
                Throw New Exception("Por favor asigne la cadena <add key=""IdentifierDateFormat"" value=""?""/> al archivo Web.config.")
            End If
        End Get
    End Property
#End Region
End Class
