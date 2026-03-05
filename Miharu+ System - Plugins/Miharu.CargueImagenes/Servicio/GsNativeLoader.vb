
Imports Ghostscript.NET
Imports Ghostscript.NET.Processor
Imports System.IO
Imports System.Runtime.InteropServices

Module GsNativeLoader

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Function SetDllDirectory(lpPathName As String) As Boolean
    End Function

    Public Function GetGsVersionInfo64(baseDir As String) As GhostscriptVersionInfo
        Dim gsDir = Path.Combine(baseDir, "GhostScript", "x64")
        Dim gsDll = Path.Combine(gsDir, "gsdll64.dll")

        If Not Directory.Exists(gsDir) Then
            Throw New DirectoryNotFoundException($"No existe la carpeta Ghostscript esperada: {gsDir}")
        End If
        If Not File.Exists(gsDll) Then
            Throw New FileNotFoundException("No se encontró gsdll64.dll en la ruta esperada.", gsDll)
        End If

        ' Ayuda a resolver dependencias nativas desde esa carpeta
        SetDllDirectory(gsDir)

        ' Crear el VersionInfo apuntando DIRECTO a la DLL nativa
        Return New GhostscriptVersionInfo(gsDll)
    End Function

End Module

