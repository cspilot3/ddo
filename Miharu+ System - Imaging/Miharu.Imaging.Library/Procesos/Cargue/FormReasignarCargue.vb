Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Procesos.Cargue

    Public Class FormReasignarCargue

#Region " Declaraciones "

        Private _SedeProcesamientoDataTable As DBSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamientoDataTable
        Private _CentroProcesamientoDataTable As DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoDataTable

        Public Class CarguePaquete
            Public Property idCargue As Integer
            Public Property idPaquete As Short

            Public Sub New(nidCargue As Integer, nidPaquete As Short)
                Me.idCargue = nidCargue
                Me.idPaquete = nidPaquete
            End Sub
        End Class

#End Region

#Region " Constructores "
        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            CargaTablas()

        End Sub
#End Region

#Region " Propiedades "

        Public Property Paquetes As List(Of CarguePaquete)

#End Region

#Region " Eventos "

        Private Sub FormReasignarCargue_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaSede()
        End Sub

        Private Sub SedeProcesamientoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SedeProcesamientoDesktopComboBox.SelectedIndexChanged
            CargaCentroPorcesamiento()
            AsignarCargueButton.Enabled = (SedeProcesamientoDesktopComboBox.SelectedIndex <> 0 And CentroProcesamientoDesktopComboBox.SelectedIndex <> 0)
        End Sub

        Private Sub CentroProcesamientoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CentroProcesamientoDesktopComboBox.SelectedIndexChanged
            AsignarCargueButton.Enabled = (SedeProcesamientoDesktopComboBox.SelectedIndex <> 0 And CentroProcesamientoDesktopComboBox.SelectedIndex <> 0)
        End Sub

        Private Sub AsignarCargueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AsignarCargueButton.Click
            AsignarCargue()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaSede()
            Try
                Utilities.LlenarCombo(SedeProcesamientoDesktopComboBox, _SedeProcesamientoDataTable, DBSecurity.SchemaConfig.TBL_SedeEnum.id_Sede.ColumnName, DBSecurity.SchemaConfig.TBL_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "--Seleccione...--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaSede", ex)
            End Try
        End Sub

        Private Sub CargaCentroPorcesamiento()
            Try
                Dim CentroProcesamientoView = _CentroProcesamientoDataTable.DefaultView
                CentroProcesamientoView.RowFilter = "fk_Sede = " + SedeProcesamientoDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(CentroProcesamientoDesktopComboBox, CentroProcesamientoView.ToTable(), DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoEnum.id_Centro_Procesamiento.ColumnName, DBSecurity.SchemaConfig.TBL_Centro_ProcesamientoEnum.Nombre_Centro_Procesamiento.ColumnName, True, "-1", "Seleccione...")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCentroPorcesamiento", ex)
            End Try
        End Sub

        Private Sub CargaTablas()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
            Try

                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

                _SedeProcesamientoDataTable = dbmSecurity.SchemaConfig.CTA_Sedes_CentrosProcesamiento.DBFindByfk_Entidad(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                _CentroProcesamientoDataTable = dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBGet(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing, Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub AsignarCargue()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If (SedeProcesamientoDesktopComboBox.SelectedIndex <> -1 And CentroProcesamientoDesktopComboBox.SelectedIndex <> -1) Then
                    For Each Paquete In Me.Paquetes
                        Dim PaqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType
                        PaqueteType.fk_Sede_Procesamiento_Asignada = CShort(SedeProcesamientoDesktopComboBox.SelectedValue)
                        PaqueteType.fk_Centro_Procesamiento_Asignado = CShort(CentroProcesamientoDesktopComboBox.SelectedValue)

                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(PaqueteType, Paquete.idCargue, Nothing)

                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Cargue.DBExecute(Paquete.idCargue, PaqueteType.fk_Sede_Procesamiento_Asignada, PaqueteType.fk_Centro_Procesamiento_Asignado)
                            '---------------------------------------------------------------------------
                        Else
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(PaqueteType, Paquete.idCargue, Paquete.idPaquete)

                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            dbmImaging.SchemaProcess.PA_Dashboard_Reasignar_Paquete.DBExecute(Paquete.idCargue, Paquete.idPaquete, PaqueteType.fk_Sede_Procesamiento_Asignada, PaqueteType.fk_Centro_Procesamiento_Asignado)
                            '---------------------------------------------------------------------------
                        End If
                    Next

                    DesktopMessageBoxControl.DesktopMessageShow("Se realizó la asignación de cargues satisfactoriamente", "Asignación de Cargue", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Me.DialogResult = DialogResult.OK
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar la Sede y el Centro de Procesamiento", "Asignación de Cargue", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("AsignarCargue", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace