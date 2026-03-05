Imports DBCore
Imports System.Drawing
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Santander.Plugin.My.Resources
Imports Santander.Plugin.ValidacionListas.Forms.CruceValidacionListas
Imports Miharu.Imaging.Library
Imports Santander.Plugin.ValidacionListas.Forms.Validaciones

Namespace ValidacionListas

    Public Class Wrapper

#Region " Declaraciones "

        Public _Plugin As ListasImagingPlugin = Nothing
        Public WithEvents ProcesoPluginToolStripMenuItem As New ToolStripMenuItem()
        Public WithEvents CruceValidacionesToolStripMenuItem As New ToolStripMenuItem()
        Public _Validacion_Listas As New Button()
        Public _SantanderConnectionString As String
        Public _ToolsConnectionString As String

#End Region

#Region " Costructores "

        Public Sub New(ByVal nPlugin As ListasImagingPlugin)
            _Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub _Validacion_Listas_Click(ByVal sender As Object, ByVal e As EventArgs)
            LaunchUtil.Indexar(Nothing, GetType(ValidacionListasController), EstadoEnum.Proceso_Adicional, LaunchUtil.EtapaEnum.ValidacionListas, _Plugin.WorkSpace, Me._Validacion_Listas, True)
        End Sub

        Private Sub CruceValidacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CruceValidacionesToolStripMenuItem.Click
            Dim CruceValidacionListasForm As New CruceValidacionListasForm(_Plugin)
            CruceValidacionListasForm.ShowDialog()
        End Sub

#End Region

#Region " Metodos "

        Public Sub AplicarCambios()
            Try
                ''Menu de Configuraciones
                ProcesoPluginToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CruceValidacionesToolStripMenuItem})
                ProcesoPluginToolStripMenuItem.Name = "ProcesoPluginToolStripMenuItem"
                ProcesoPluginToolStripMenuItem.Size = New Size(193, 22)
                ProcesoPluginToolStripMenuItem.Text = "Proceso Listas..."
                ProcesoPluginToolStripMenuItem.Visible = True

                ''Cruce Validacion Listas
                CruceValidacionesToolStripMenuItem.Name = "GeneracionDataValidacionesToolStripMenuItem"
                CruceValidacionesToolStripMenuItem.Size = New Size(193, 22)
                CruceValidacionesToolStripMenuItem.Text = "Generacion Data - Validacion Listas"
                CruceValidacionesToolStripMenuItem.Visible = True


                Me._Validacion_Listas.Anchor = AnchorStyles.None
                Me._Validacion_Listas.BackColor = Color.White
                Me._Validacion_Listas.FlatStyle = FlatStyle.Flat
                Me._Validacion_Listas.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                Me._Validacion_Listas.Image = Indexar
                Me._Validacion_Listas.ImageAlign = ContentAlignment.TopCenter
                Me._Validacion_Listas.Location = New Point(28, 328)
                Me._Validacion_Listas.Name = "ValidacionListasButton"
                Me._Validacion_Listas.Size = New Size(100, 60)
                Me._Validacion_Listas.TabIndex = 81
                Me._Validacion_Listas.Text = "Validacion Listas"
                Me._Validacion_Listas.TextAlign = ContentAlignment.BottomCenter
                Me._Validacion_Listas.UseVisualStyleBackColor = False

                _Plugin.WorkSpace.ProcesoAdicionalCapturaButton.Visible = False
                _Plugin.WorkSpace.WSPanel.Controls.Add(_Validacion_Listas)
                AddHandler _Validacion_Listas.Click, AddressOf _Validacion_Listas_Click

                _Plugin.WorkSpace.MainMenuStrip.Items.AddRange(New ToolStripItem() {ProcesoPluginToolStripMenuItem})

                ' Ocultar opciones
                '_Plugin.WorkSpace.OTButton.Enabled = False
                '_Plugin.WorkSpace.FechaProcesoButton.Enabled = False
                '_Plugin.WorkSpace.DestapeButton.Enabled = False
                '_Plugin.WorkSpace.EmpaqueButton.Enabled = False


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Santander - Validacion Listas al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub


        Public Sub DeshacerCambios()

        End Sub

#End Region


    End Class

End Namespace