Imports System.IO
Imports System.Reflection
Imports Miharu.Security.Library.WebService

Namespace _clases

    Public Class Program

#Region " Estructuras "

        Public Structure TypeConnectionString
            Public Security As String
        End Structure

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

        Public Shared ReadOnly Property SecurityWebServiceURL As String
            Get
                Dim rootWebConfig As String = ConfigurationManager.AppSettings("WebService.SecurityService")

                If (rootWebConfig IsNot Nothing And rootWebConfig.Length > 0) Then
                    Return rootWebConfig
                Else
                    Throw New Exception("Por favor asigne la cadena <add key=""WebService.SecurityService"" value=""?""/> al archivo Web.config.")
                End If
            End Get
        End Property

        Public Shared ReadOnly Property IdentifierDateFormat As String
            Get

                Dim rootWebConfig As String = ConfigurationManager.AppSettings("DAL.IdentifierDateFormat")

                If (rootWebConfig <> Nothing And rootWebConfig.Length > 0) Then
                    Return rootWebConfig
                Else
                    Throw New Exception("Por favor asigne la cadena <add key=""DAL.IdentifierDateFormat"" value=""?""/> al archivo Web.config.")
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DataRemoting As String
            Get

                Dim rootWebConfig As String = ConfigurationManager.AppSettings("DataRemoting")

                If (rootWebConfig <> Nothing And rootWebConfig.Length > 0) Then
                    Return rootWebConfig
                Else
                    Throw New Exception("Por favor asigne la cadena <add key=""DataRemoting"" value=""?""/> al archivo Web.config.")
                End If
            End Get
        End Property

#End Region

#Region " Funciones "

        Public Shared Function getCadenasConexion(ByRef nWebService As SecurityWebService) As TypeConnectionString
            Dim Cadenas As New TypeConnectionString()
            Dim ConnectionStrings = nWebService.getCadenasConexion()

            For Each Modulo As TypeModulo In ConnectionStrings
                Select Case Modulo.Id
                    Case 0 : Cadenas.Security = Modulo.ConnectionString
                End Select
            Next

            Return Cadenas
        End Function

#End Region

    End Class

End Namespace