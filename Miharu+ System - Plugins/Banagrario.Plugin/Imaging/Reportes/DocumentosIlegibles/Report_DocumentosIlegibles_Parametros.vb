Imports DBSecurity
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Reportes.DocumentosIlegibles

    Public Class Report_DocumentosIlegibles_Parametros

        Public _plugin As BanagrarioImagingPlugin = Nothing

#Region " Propiedades "

        Public Property Fechaproceso() As Date
            Get
                Return dtpFechaproceso.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaproceso.Value = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub FormMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            CargaControles()
        End Sub

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)

            ' This call is required by the designer.
            InitializeComponent()

            _plugin = nPlugin

        End Sub

        Private Sub CargaControles()
            Dim dmImaging As New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim dmSecurity As New DBSecurityDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
            Dim dbbanagrario As New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            Try
                dmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dmSecurity.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbbanagrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                Dim CiudadDataTable = dmSecurity.SchemaConfig.TBL_Sede.DBGet(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing)
                
                Dim TipoOficinaDataTable = dbbanagrario.SchemaConfig.TBL_Oficina_Tipo.DBGet(Nothing)
                Utilities.LlenarCombo(CiudadComboBox, CiudadDataTable, CiudadDataTable.id_SedeColumn.ColumnName, CiudadDataTable.Nombre_SedeColumn.ColumnName, True, "-1", "Todos...")
                Utilities.LlenarCombo(TipoOficinaComboBox, TipoOficinaDataTable, TipoOficinaDataTable.id_Oficina_TipoColumn.ColumnName, TipoOficinaDataTable.Nombre_Oficina_TipoColumn.ColumnName, True, "-1", "Todos...")
                
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaControles", ex)

            Finally
                dmSecurity.Connection_Close()
                dmImaging.Connection_Close()
                dbbanagrario.Connection_Close()
            End Try

        End Sub

#End Region

    End Class

End Namespace