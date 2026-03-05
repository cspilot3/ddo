Imports System.Windows.Forms
Imports DBAgrario.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports DBSecurity.SchemaSecurity

Namespace Imaging.Reportes.ControlIndexacion

    Public Class FormControlIndexacion

#Region " Declaraciones "

        Private _usuarioTable As TBL_UsuarioDataTable
        Private _cobDataTable As TBL_COBDataTable
        Private _oficinaDataTable As TBL_OficinaDataTable
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

        Private _regional As Short
        Public Property Regional() As Short
            Get
                Return _Regional
            End Get
            Set(ByVal value As Short)
                _Regional = value
            End Set
        End Property

        Private _cob As Short
        Public Property Cob() As Short
            Get
                Return _cob
            End Get
            Set(ByVal value As Short)
                _cob = value
            End Set
        End Property

        Private _oficina As Short
        Public Property Oficina() As Short
            Get
                Return _Oficina
            End Get
            Set(ByVal value As Short)
                _Oficina = value
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

#Region " Eventos "

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

            _Regional = CShort(UsuarioDesktopComboBox.SelectedValue)
            _COB = CShort(OficinaDesktopComboBox.SelectedValue)
            _Oficina = CShort(COBDesktopComboBox.SelectedValue)

            Me.DialogResult = DialogResult.OK
            Me.Hide()
        End Sub

        Private Sub FormParametrosContenedor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            CargaRegional()
        End Sub

        Private Sub RegionalDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles UsuarioDesktopComboBox.SelectedIndexChanged
            CargaCOB()
        End Sub

        Private Sub COBDesktopComboBox_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles COBDesktopComboBox.SelectedIndexChanged
            CargaOficina()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dmBanAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dmBanAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dmSecurity = New DBSecurity.DBSecurityDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                dmSecurity.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                dmBanAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                _usuarioTable = dmSecurity.SchemaSecurity.TBL_Usuario.DBGet(Nothing)
                _cobDataTable = dmBanAgrario.SchemaConfig.TBL_COB.DBGet(Nothing)
                _oficinaDataTable = dmBanAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmSecurity.Connection_Close()
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargaRegional()
            Try
                Utilities.LlenarCombo(UsuarioDesktopComboBox, _usuarioTable, TBL_UsuarioEnum.id_Usuario.ColumnName, TBL_UsuarioEnum.Login_Usuario.ColumnName, True, "-1", "Seleccione")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargaCob()
            Try
                Dim cobView = _cobDataTable.DefaultView
                'COBView.RowFilter = "fk_Regional = " + UsuarioDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(COBDesktopComboBox, cobView.ToTable(), TBL_COBEnum.id_COB.ColumnName, TBL_COBEnum.Nombre_COB.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCOB", ex)
            End Try
        End Sub

        Private Sub CargaOficina()
            Try
                Dim oficinaView = _oficinaDataTable.DefaultView
                oficinaView.RowFilter = "fk_COB = " + COBDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(OficinaDesktopComboBox, oficinaView.ToTable(), TBL_OficinaEnum.id_Oficina.ColumnName, TBL_OficinaEnum.Nombre_Oficina.ColumnName, True, "-1", "--TODOS--")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaOficina", ex)
            End Try
        End Sub

#End Region


    End Class

End Namespace