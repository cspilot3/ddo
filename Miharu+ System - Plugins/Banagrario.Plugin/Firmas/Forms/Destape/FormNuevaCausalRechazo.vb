Imports System.Windows.Forms

Namespace Firmas.Forms.Destape

    Public Class FormNuevaCausalRechazo

#Region " Declaraciones"

        Private _plugin As FirmasImagingPlugin
        Public IdCausalRechazo As Integer

#End Region

#Region " Contructores "

        Public Sub New(ByVal nFirmasImagingPlugin As FirmasImagingPlugin)
            InitializeComponent()
            _plugin = nFirmasImagingPlugin
        End Sub

#End Region

#Region " Eventos "


        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCausalNueva()
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub GuardarCausalNueva()

            If (Validar()) Then

                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try

                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)

                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    dbmAgrario.Transaction_Begin()

                    Dim causalRDataTable As DataTable = dbmAgrario.SchemaFirmas.TBL_Causales_Rechazo.DBGet(IdCausalRechazo)
                    Dim causalRechazoType As New DBAgrario.SchemaFirmas.TBL_Causales_RechazoType
                    If causalRDataTable.Rows.Count > 0 Then
                        With causalRechazoType
                            .Descripcion = CausalRechazoTextBox.Text
                            .Eliminado = CheckBoxActivo.Checked
                        End With

                        dbmAgrario.SchemaFirmas.TBL_Causales_Rechazo.DBUpdate(causalRechazoType, IdCausalRechazo)
                    Else
                        With causalRechazoType
                            .id_Rechazo = dbmAgrario.SchemaFirmas.TBL_Causales_Rechazo.DBNextId()
                            .Descripcion = CausalRechazoTextBox.Text
                            .Eliminado = CheckBoxActivo.Checked
                        End With

                        dbmAgrario.SchemaFirmas.TBL_Causales_Rechazo.DBInsert(causalRechazoType)
                    End If

                    dbmAgrario.Transaction_Commit()
                    MessageBox.Show("La causal de rechazo se guardo exitosamente", "Causales De Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Causales De Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                End Try

            End If
        End Sub


        Private Function Validar() As Boolean
            If CausalRechazoTextBox.Text = "" Then
                MessageBox.Show("Debe digitar una nueva causal", "Causales De Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Return True
            End If

            Return False

        End Function
#End Region
        
    End Class

End Namespace
