Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Forms

Namespace Clases

    Public Class ProcessLibraryFactory

        Public Shared Function getProcessLibrary(ByVal nLibreria As ProcessLibraryType) As IProcessLibrary
            Select Case nLibreria
                Case ProcessLibraryType.Custody
                    Return New Miharu.Custody.Library.ProccessLibrary()

                Case ProcessLibraryType.Imaging
                    Return New Imaging.Library.ProcessLibrary()

                Case ProcessLibraryType.Paper
                    Throw New Exception("La funcionalidad no ha sido implementada")

                Case ProcessLibraryType.Risk
                    Return New Miharu.Risk.Library.ProcessLibrary()

                Case Else
                    Throw New Exception("La funcionalidad no ha sido implementada")

            End Select

        End Function

    End Class
End Namespace