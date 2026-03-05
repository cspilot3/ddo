Imports System.Windows.Forms

Namespace Imaging.Forms.Parametrización

    Public Class FormNuevaNovedad


#Region " Declaraciones "

        Private _Plugin As BanagrarioImagingPlugin
        Public _idNovedad As Integer
#End Region

#Region " Contructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            GuardarNovedad()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            DialogResult = DialogResult.Cancel
        End Sub

        Private Sub GuardarNovedad()

            If (Validar()) Then

                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try

                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    dbmAgrario.Transaction_Begin()

                    Dim NovedadDataTable = dbmAgrario.SchemaConfig.TBL_Novedades.DBFindByid_Novedad(_idNovedad)
                    Dim NovedadType As New DBAgrario.SchemaConfig.TBL_NovedadesType
                    If NovedadDataTable.Count > 0 Then
                        With NovedadType
                            .Novedad = NovedadTextBox.Text
                            .Eliminado = CheckBoxActivo.Checked
                        End With

                        dbmAgrario.SchemaConfig.TBL_Novedades.DBUpdate(NovedadType, _idNovedad, Program.BanagrarioDocumentoTapaId)
                    Else
                        With NovedadType
                            .fk_Documento = Program.BanagrarioDocumentoTapaId
                            .Novedad = NovedadTextBox.Text
                            .Eliminado = CheckBoxActivo.Checked
                        End With

                        dbmAgrario.SchemaConfig.TBL_Novedades.DBInsert(NovedadType)
                    End If

                    dbmAgrario.Transaction_Commit()
                    MessageBox.Show("La novedad se guardo exitosamente", "Novedades", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Novedades", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try
            End If

        End Sub


#Region " Funciones "

        Private Function Validar() As Boolean

            If NovedadTextBox.Text = "" Then
                MessageBox.Show("Debe digitar una novedad", "Novedades", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class

End Namespace