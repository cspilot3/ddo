Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBArchiving.SchemaSecurity
Imports Miharu.Desktop.Library.Config

Namespace Risk.Reportes.EmpaqueContenedor

    Public Class FormEmpaqueContenedor

#Region " Declaraciones "

        Private _plugin As BanagrarioRiskPlugin
        Public SedeDataTable As CTA_SedeDataTable

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioRiskPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormEmpaqueContenedor_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCombos()
        End Sub

        Private Sub BuscarButton_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            If ValidarRangoFechas(Fecha1DateTimePicker.Value.ToString("yyyy/MM/dd"), Fecha2DateTimePicker.Value.ToString("yyyy/MM/dd")) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If

        End Sub

#End Region

#Region " Metodos "

        Public Sub CargarCombos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Try
                SedeDataTable = dbmArchiving.SchemaSecurity.CTA_Sede.DBFindByfk_Entidad(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad)
                Utilities.LlenarCombo(SedeDesktopComboBox, SedeDataTable, CTA_SedeEnum.id_Sede.ColumnName, CTA_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarCombos", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function ValidarRangoFechas(ByVal nFecha1 As String, ByVal nFecha2 As String) As Boolean

            If (DateDiff("d", CDate(nFecha1), CDate(nFecha2)) >= 15) Then
                DesktopMessageBoxControl.DesktopMessageShow("El Rango de Fechas elegido para la fecha de proceso tiene mas de 15 dias de diferencia", "Validación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return False
            Else
                Return True
            End If
        End Function

#End Region

    End Class

End Namespace