Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Slyg.Tools
Imports DBImaging.SchemaProcess
Imports DBCore
Imports DBImaging
Imports Miharu.Desktop.Controls.Utils
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace ValidacionListas.Forms.CruceValidacionListas

    Public Class CruceValidacionListasForm

#Region " Declaraciones "

        Public Plugin As ListasImagingPlugin
        Public Cargando As Boolean = False

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As ListasImagingPlugin)
            InitializeComponent()

            Plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CruceValidacionListasForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Cargando = True
            CargarOT()
            Cargando = False
        End Sub

        Private Sub OTComboBox_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles OTDesktopComboBox.SelectedIndexChanged
            If Not Cargando Then
                CargarCargues()
            End If
        End Sub

        Private Sub FechaProcesoPicker_ValueChanged(sender As Object, e As System.EventArgs) Handles FechaProcesoPicker.ValueChanged
            Cargando = True
            CargarOT()
            Cargando = False
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
            GenerarData()
        End Sub

#End Region

#Region " Metodos "

        Private Sub GenerarData()
            Dim dbmSantander As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                If Validar() Then
                    dbmSantander = New DBIntegration.DBIntegrationDataBaseManager(Plugin.SantanderConnectionString)
                    dbmSantander.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                    Dim Cruce = dbmSantander.SchemaSantander.PA_Cruce_Validacion_Listas.DBExecute(CInt(FechaProcesoPicker.Value.ToString("yyyyMMdd")), Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, OTDesktopComboBox.SelectedValue.ToString(), CargueDesktopComboBox.SelectedValue.ToString())

                    If Cruce = True Then
                        MessageBox.Show("Data generada correctamente", "Generación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CargarCargues()
                    Else
                        MessageBox.Show("No hay data que generar", "Generación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If


                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If dbmSantander IsNot Nothing Then dbmSantander.Connection_Close()
            End Try

        End Sub

        Private Sub CargarOT()
            OTDesktopComboBox.DataSource = Nothing
            OTDesktopComboBox.SelectedIndex = -1


            Dim dbmImaging As DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, CInt(FechaProcesoPicker.Value.ToString("yyyyMMdd")), False, Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)

                If OTDataTable.Count > 0 Then
                    For Each OTRow In OTDataTable
                        Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, OTDataTable.id_OTColumn.ColumnName, OTDataTable.id_OTColumn.ColumnName)
                    Next
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            If OTDesktopComboBox.Items.Count Then
                OTDesktopComboBox.SelectedIndex = 0
                CargarCargues()
            End If


        End Sub

        Private Sub CargarCargues()
            CargueDesktopComboBox.DataSource = Nothing
            CargueDesktopComboBox.SelectedIndex = -1


            Dim OT = Integer.Parse(OTDesktopComboBox.SelectedValue.ToString())

            Dim dbmSantander As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try

                dbmSantander = New DBIntegration.DBIntegrationDataBaseManager(Plugin.SantanderConnectionString)
                dbmSantander.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                Dim CarguesDataTable = dbmSantander.SchemaSantander.PA_Get_Cargue_x_OT.DBExecute(OT, DBCore.EstadoEnum.Indexado)

                If CarguesDataTable.Count > 0 Then
                    For Each CargueRow In CarguesDataTable

                        Utilities.LlenarCombo(CargueDesktopComboBox, CarguesDataTable, CarguesDataTable.idColumn.ColumnName, CarguesDataTable.NombreColumn.ColumnName, True, "-1", "--TODOS--")
                    Next
                    CargueDesktopComboBox.SelectedIndex = 0
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmSantander IsNot Nothing) Then dbmSantander.Connection_Close()
            End Try



        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                If OTDesktopComboBox.SelectedIndex = -1 Then
                    Throw New Exception("No hay OT's para la fecha de proceso seleccionada")
                End If

                If CargueDesktopComboBox.SelectedIndex = -1 Then
                    Throw New Exception("No hay Cargues para la OT y fecha de proceso seleccionada")
                End If


                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                                                               Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                                                                  CInt(FechaProcesoPicker.Value.ToString("yyyyMMdd")), _
                                                                                                                                               Nothing, _
                                                                                                                                               Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                If OTDataTable.Count = 0 Then
                    Throw New Exception("No hay OT's para la fecha de proceso seleccionada")
                End If

                'Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(CInt(OTDesktopComboBox.SelectedValue))
                Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargue_OT.DBExecute(
                                                                                        CInt(OTDesktopComboBox.SelectedValue) _
                                                                                        , CLng(IIf(CargueDesktopComboBox.SelectedIndex <= 0, -1, CargueDesktopComboBox.SelectedValue))
                                                                                        )

                If (Not Resultado) Then
                    Throw New Exception("La OT: " + OTDesktopComboBox.SelectedValue.ToString() + " no ha sido totalmente procesada")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return True
        End Function

#End Region

    End Class

End Namespace





