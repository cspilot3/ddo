Imports System.Windows.Forms
Imports Slyg.Tools
Imports Colpensiones.Plugin.Imaging.Beps.Utilidades

Namespace Imaging.Beps.Rechazo_Bizagi

    Public Class FormNuevaCausalRechazo

#Region " Declaraciones"

        Private _plugin As Plugin
        Public IdCausalRechazo As Integer
        Public fk_Tipo_Novedad As Integer
        Public rechazos As New Utilidades.Rechazos
#End Region

#Region " Contructores "

        Public Sub New(ByVal nPlugin As Plugin)
            InitializeComponent()
            _plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "


        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCausalNueva()
        End Sub

        Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub GuardarCausalNueva()

            If (Validar()) Then

                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                Try

                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)

                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    dbmIntegration.Transaction_Begin()

                    Dim causalRDataTable As DataTable = dbmIntegration.SchemaColpensionesBEPS.TBL_Rechazos_Bizagi.DBGet(IdCausalRechazo)
                    Dim causalRechazoType As New DBIntegration.SchemaColpensionesBEPS.TBL_Rechazos_BizagiType
                    If causalRDataTable.Rows.Count > 0 Then
                        With causalRechazoType
                            .Descripcion_Rechazo = CausalRechazoTextBox.Text
                            .Eliminado = CheckBoxActivo.Checked
                            .fk_Usuario = _plugin.Manager.Sesion.Usuario.id
                            .Fecha_Log = SlygNullable.SysDate
                            .fk_Tipo_Novedad = rechazos.Find(ComboBoxTipoRechazo.Text)
                        End With

                        dbmIntegration.SchemaColpensionesBEPS.TBL_Rechazos_Bizagi.DBUpdate(causalRechazoType, IdCausalRechazo)
                    Else
                        With causalRechazoType
                            .Descripcion_Rechazo = CausalRechazoTextBox.Text
                            .Eliminado = CheckBoxActivo.Checked
                            .fk_Usuario = _plugin.Manager.Sesion.Usuario.id
                            .Fecha_Log = SlygNullable.SysDate
                            .fk_Tipo_Novedad = ComboBoxTipoRechazo.SelectedValue
                        End With

                        dbmIntegration.SchemaColpensionesBEPS.TBL_Rechazos_Bizagi.DBInsert(causalRechazoType)
                    End If

                    dbmIntegration.Transaction_Commit()
                    MessageBox.Show("La causal de rechazo se guardo exitosamente", "Causales De Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.CausalRechazoTextBox.Text = ""
                    Me.CheckBoxActivo.Checked = False

                    Me.DialogResult = DialogResult.OK
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Causales De Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                End Try

            End If
        End Sub

        Private Sub FormNuevaCausalRechazo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            CargarTipoRechazoBizagi()
        End Sub

        Private Sub CargarTipoRechazoBizagi()

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try

                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._plugin.ColpensionesConnectionString)

                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim TiposRechazos = dbmIntegration.SchemaColpensionesBEPS.TBL_Tipo_Novedad.DBFindByEs_Rechazo_BizagiEliminado(True, False)

                For Each RegistroRechazo In TiposRechazos
                    rechazos.Add(RegistroRechazo.id_Tipo_Novedad, RegistroRechazo.Nombre_Tipo_Novedad)
                    ComboBoxTipoRechazo.Items.Add(RegistroRechazo.Nombre_Tipo_Novedad)
                Next

                If ComboBoxTipoRechazo.Items.Count > 0 Then
                    ComboBoxTipoRechazo.Text = rechazos.Find(fk_Tipo_Novedad)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "CargarTipoRechazoBizagi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try


        End Sub

#End Region

#Region " Funciones "
        Private Function Validar() As Boolean
            If CausalRechazoTextBox.Text = "" Then
                MessageBox.Show("Debe digitar una nueva causal", "Causales De Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CausalRechazoTextBox.Focus()
                Return False
            End If

            Return True
        End Function

#End Region

    End Class


End Namespace
