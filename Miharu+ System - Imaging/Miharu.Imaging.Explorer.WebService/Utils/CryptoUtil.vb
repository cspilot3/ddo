Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Web

Public Class CryptoUtil
    Private Shared key As Byte() = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
    Private Shared iv As Byte() = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}


    Public Shared Function encrypt(ByVal value As String) As String
        Return Convert.ToBase64String(rawEncrypt(value, key, iv))
    End Function

    Public Shared Function decrypt(ByVal value As String) As String
        Return rawDecrypt(Convert.FromBase64String(value), key, iv)
    End Function

    Private Shared Function rawEncrypt(ByVal plainText As String, ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
        Dim encrypted As Byte()

        Using aes As AesManaged = New AesManaged()
            Dim encryptor As ICryptoTransform = aes.CreateEncryptor(Key, IV)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor, CryptoStreamMode.Write)

                    Using sw As StreamWriter = New StreamWriter(cs)
                        sw.Write(plainText)
                    End Using

                    encrypted = ms.ToArray()
                End Using
            End Using
        End Using

        Return encrypted
    End Function

    Private Shared Function rawDecrypt(ByVal cipherText As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As String
        Dim plaintext As String = Nothing

        Using aes As AesManaged = New AesManaged()
            Dim decryptor As ICryptoTransform = aes.CreateDecryptor(Key, IV)

            Using ms As MemoryStream = New MemoryStream(cipherText)

                Using cs As CryptoStream = New CryptoStream(ms, decryptor, CryptoStreamMode.Read)

                    Using reader As StreamReader = New StreamReader(cs)
                        plaintext = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using

        Return plaintext
    End Function
End Class