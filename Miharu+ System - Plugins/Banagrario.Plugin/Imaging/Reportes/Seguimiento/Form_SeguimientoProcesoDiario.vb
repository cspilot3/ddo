Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports DBSecurity.SchemaConfig

Namespace Imaging.Reportes.Seguimiento

    Public Class FormSeguimientoProcesoDiario

#Region " Declaraciones "

        Private _sedesTable As TBL_SedeDataTable
        Public Plugin As BanagrarioImagingPlugin = Nothing

#End Region

#Region " Propiedades "

        Public Property FechaProceso() As Date
            Get
                Return dtpFechaProceso.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaProceso.Value = value
            End Set
        End Property

        Private _sedeDestape As Short
        Public Property SedeDestape() As Short
            Get
                Return _SedeDestape
            End Get
            Set(ByVal value As Short)
                _SedeDestape = value
            End Set
        End Property

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)
            InitializeComponent()
            Plugin = nPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dmSecurity = New DBSecurity.DBSecurityDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                dmSecurity.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                _sedesTable = dmSecurity.SchemaConfig.TBL_Sede.DBGet(Nothing, Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub CargaSedes()
            Try
                Utilities.LlenarCombo(SedeDestapeComboBox, _sedesTable, TBL_SedeEnum.id_Sede.ColumnName, TBL_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "Seleccione")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

#End Region

#Region " Eventos "

        Private Sub Form_SeguimientoProcesoDiario_Load(sender As Object, e As EventArgs) Handles Me.Load
            CargaSedes()
        End Sub

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            _SedeDestape = CShort(SedeDestapeComboBox.SelectedValue)
            Me.DialogResult = DialogResult.OK
            Me.Hide()
        End Sub

#End Region

    End Class

End Namespace
