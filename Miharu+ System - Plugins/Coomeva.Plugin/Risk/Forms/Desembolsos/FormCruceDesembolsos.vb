Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Coomeva.Plugin.Risk.FormWrappers
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Progress

Namespace Forms.Desembolsos
    Public Class FormCruceDesembolsos
        Inherits FormBase
#Region " Declaraciones "

        Private _Plugin As CoomevaRiskPlugin
        Private _TipoCargue As DesktopConfig.TipoCargue

#End Region

#Region " Constructor "

        Sub New(ByVal nCoomevaDesktopPlugin As CoomevaRiskPlugin)
            ' This call is required by the designer.
            InitializeComponent()
            _Plugin = nCoomevaDesktopPlugin
        End Sub

#End Region

        'Private Sub FormCruceDesembolsos_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        '    Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)
        '    Dim Table  = dbmArchiving.SchemaRisk.TBL_Control_Cargue_Desembolso.DBFillByfk_Entidadfk_Proyectocruzado(
        '    Try
        '        dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
        '    Catch ex As Exception
        '        DesktopMessageBoxControl.DesktopMessageShow("GeneraCierre", ex)

        '    Finally
        '        dbmArchiving.Connection_Close()
        '    End Try
        'End Sub

        Private Sub GeneraCruce()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(_Plugin.CoomevaConnectionString)

            Try
                Me.Enabled = False
                Me.Cursor = Cursors.WaitCursor

                dbmArchiving.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ProcesoDataTable = dbmArchiving.SchemaRisk.PA_Validar_Cruce_Desembolso.DBExecute(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, FechaRecolecciondateTimePicker.Value.ToString("yyyyMMdd"))
                Dim Msg As String = ""

                If (ProcesoDataTable.Rows.Count > 0) Then
                    Dim msj As String = ""
                    For Each dr As DataRow In ProcesoDataTable.Rows
                        If dr(0).ToString <> "" Then
                            msj = msj & dr("Descripcion").ToString & vbCrLf
                        End If
                    Next

                    MessageBox.Show(msj, "Cruce Desembolsos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return

                End If

               
                Dim Respuesta = dbmArchiving.SchemaRisk.PA_Cruce_Desembolso.DBExecute(_Plugin.Manager.RiskGlobal.Entidad, _Plugin.Manager.RiskGlobal.Proyecto, FechaRecolecciondateTimePicker.Value.ToString("yyyyMMdd"), _Plugin.Manager.Sesion.Usuario.id)

                If Respuesta.Rows.Count > 0 Then
                    Dim msj As String = ""
                    For Each dr As DataRow In Respuesta.Rows

                        msj = msj & dr("Error") & ": " & dr("Descripcion").ToString & vbCrLf

                    Next
                    MessageBox.Show(msj, "Cruce Desembolsos", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Else
                    MessageBox.Show("Cruce Exitoso", "Cruce Desembolsos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If


            Catch ex As Exception
                Application.DoEvents()
                DesktopMessageBoxControl.DesktopMessageShow("GeneraCierre", ex)
            Finally
                dbmArchiving.Connection_Close()
                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub CruzarButton_Click(sender As System.Object, e As System.EventArgs) Handles CruzarButton.Click


            '*************VALIDACIONES:
            '1.El proceso del mes debe estar finalizado
            '2.Todos los archivos log de Desembolsos deben estar cargados


            '************CONDICIONES DEL CRUCE
            '1.Unicamente contra los físicos (no contra el log).
            '2.Cruzar meses anteriores

            If FechaRecolecciondateTimePicker.Value.Year <= Date.Now.Year AndAlso FechaRecolecciondateTimePicker.Value.Month <= Date.Now.Month Then

                Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cruce correspondiente a " + FechaRecolecciondateTimePicker.Value.ToString("y") + "?", "Cierre Fecha Recolección", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

                If (Respuesta = DialogResult.OK) Then
                    GeneraCruce()
                End If
            Else
                MessageBox.Show("Fecha de Cruce No permitida: " + FechaRecolecciondateTimePicker.Value.ToString("y") + ", Por Favor Verificar", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub


    End Class

End Namespace
