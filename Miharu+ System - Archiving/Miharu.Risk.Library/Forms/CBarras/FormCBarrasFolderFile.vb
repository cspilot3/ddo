Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Forms.CBarras

    Public Class FormCBarrasFolderFile
        Inherits FormBase

#Region " Metodos "

        Public Sub ImprimirCBarras()
            If cbarrasDesktopCBarrasControl.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un codigo de barras", "Codigo de barras vacio", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Exit Sub
            End If

            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim CBarrasLocal As String = cbarrasDesktopCBarrasControl.Text
            Dim TableParametros As DataTable = dbmArchiving.Schemadbo.PA_Imprimir_CBarras_Unico.DBExecute(CBarrasLocal)

            If TableParametros.Rows.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se ha encontrado el codigo de barras asociado a ninguna carpeta", "No se encontro Codigo de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else

                Dim Parametros As New List(Of DesktopConfig.AtributesCBarras)
                Parametros.Clear()
                For Each RowParametros As DataRow In TableParametros.Rows
                    Dim parametro As New DesktopConfig.AtributesCBarras
                    parametro.Label = RowParametros("Label").ToString
                    parametro.Valor = RowParametros("Valor").ToString

                    If Not (parametro.Valor = "" And parametro.Valor = "") Then
                        Parametros.Add(parametro)
                    End If
                Next

                Dim BarCodeControl_ As New Desktop.Controls.BarCode.BarCodeControl
                Utilities.ImprimirCBarras(BarCodeControl_, CBarrasLocal, TableParametros.Rows(0)("Title").ToString(), Parametros)
                'BarCodeControl_.Print()

                'codigo de barras de un carpeta
                dbmArchiving.Transaction_Begin()
                If TableParametros.Rows(0)("Tipo").ToString = "0" Then
                    Dim TableFolder = dbmArchiving.Schemadbo.CTA_Folder.DBFindByCBarras_Folderfk_Estado(CBarrasLocal, Nothing)
                    Dim Expediente As Long = CLng(TableFolder.Rows(0)(TableFolder.fk_ExpedienteColumn))
                    Dim Folder As Short = CShort(TableFolder.Rows(0)(TableFolder.id_FolderColumn))
                    'Dim OT As Integer = CInt(TableFolder.Rows(0)(TableFolder.fk_OTColumn))

                    Dim RegistroFolder As New DBArchiving.SchemaRisk.TBL_FolderType With {.Impreso = True}
                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(RegistroFolder, Expediente, Folder, Nothing)
                Else
                    'codigo de barras de un documento

                    Try
                        Dim TableFile = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(CBarrasLocal)
                        Dim Expediente As Long = CLng(TableFile.Rows(0)(TableFile.fk_ExpedienteColumn))
                        Dim Folder As Short = CShort(TableFile.Rows(0)(TableFile.fk_FolderColumn))
                        Dim OT As Integer = CInt(TableFile.Rows(0)(TableFile.fk_OTColumn))
                        Dim File As Short = CShort(TableFile.Rows(0)(TableFile.id_FileColumn))

                        Dim RegistroFile As New DBArchiving.SchemaRisk.TBL_FileType With {.Impreso = True}
                        dbmArchiving.SchemaRisk.TBL_File.DBUpdate(RegistroFile, OT, Folder, File, Expediente)
                    Catch ex As Exception

                    End Try
                End If
                dbmArchiving.Transaction_Commit()
            End If
            dbmArchiving.Connection_Close()

            cbarrasDesktopCBarrasControl.Focus()
            cbarrasDesktopCBarrasControl.SelectAll()

        End Sub
#End Region

#Region " Eventos "

        Private Sub FormCBarrasFolderFile_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            cbarrasDesktopCBarrasControl.Text = CBarras
            cbarrasDesktopCBarrasControl.Focus()
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            ImprimirCBarras()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub
#End Region

#Region " Propiedades "

        Public Property CBarras As String

#End Region

    End Class
End Namespace