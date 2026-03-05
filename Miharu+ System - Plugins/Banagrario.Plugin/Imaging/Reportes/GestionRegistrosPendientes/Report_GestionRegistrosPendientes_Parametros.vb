Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBAgrario
Imports DBAgrario.SchemaConfig
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Reportes.GestionRegistrosPendientes

    Public Class Report_GestionRegistrosPendientes_Parametros

#Region " Declaraciones "

        Public _plugin As BanagrarioImagingPlugin = Nothing

        Private _RegionalDataTable As TBL_RegionalDataTable
        Private _COBDataTable As TBL_COBDataTable
        Private _OficinaDataTable As TBL_OficinaDataTable

#End Region

#Region " Propiedades "

        Public Property FechaInicial() As Date
            Get
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property

        Private _Regional As Short
        Public Property Regional() As Short
            Get
                Return _Regional
            End Get
            Set(ByVal value As Short)
                _Regional = value
            End Set
        End Property

        Private _COB As Short
        Public Property COB() As Short
            Get
                Return _COB
            End Get
            Set(ByVal value As Short)
                _COB = value
            End Set
        End Property

        Private _Oficina As Short
        Public Property Oficina() As Short
            Get
                Return _Oficina
            End Get
            Set(ByVal value As Short)
                _Oficina = value
            End Set
        End Property

#End Region

#Region " Constructor "

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)

            InitializeComponent()

            _plugin = nPlugin

            CargaTablas()

        End Sub

#End Region

#Region " Eventos "

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub
        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

            _Regional = CShort(RegionalDesktopComboBox.SelectedValue)
            _COB = CShort(COBDesktopComboBox.SelectedValue)
            _Oficina = CShort(OficinaDesktopComboBox.SelectedValue)

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Hide()
        End Sub

        Private Sub FormRangoFechas_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaRegional()
        End Sub

        Private Sub RegionalDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles RegionalDesktopComboBox.SelectedIndexChanged
            CargaCOB()
        End Sub

        Private Sub COBDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles COBDesktopComboBox.SelectedIndexChanged
            CargaOficina()
        End Sub

#End Region

#Region " Funciones "

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dmBanAgrario As New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            Try

                dmBanAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                _RegionalDataTable = dmBanAgrario.SchemaConfig.TBL_Regional.DBGet(Nothing)
                _COBDataTable = dmBanAgrario.SchemaConfig.TBL_COB.DBGet(Nothing)
                _OficinaDataTable = dmBanAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargaRegional()
            Try
                Utilities.LlenarCombo(RegionalDesktopComboBox, _RegionalDataTable, TBL_RegionalEnum.id_Regional.ColumnName, TBL_RegionalEnum.Nombre_Regional.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargaCOB()
            Try
                Dim COBView = _COBDataTable.DefaultView
                COBView.RowFilter = "fk_Regional = " + RegionalDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(COBDesktopComboBox, COBView.ToTable(), TBL_COBEnum.id_COB.ColumnName, TBL_COBEnum.Nombre_COB.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaCOB", ex)
            End Try
        End Sub

        Private Sub CargaOficina()
            Try
                Dim OficinaView = _OficinaDataTable.DefaultView
                OficinaView.RowFilter = "fk_COB = " + COBDesktopComboBox.SelectedValue.ToString()

                Utilities.LlenarCombo(OficinaDesktopComboBox, OficinaView.ToTable(), TBL_OficinaEnum.id_Oficina.ColumnName, TBL_OficinaEnum.Nombre_Oficina.ColumnName, True, "-1", "--TODOS--")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaOficina", ex)
            End Try
        End Sub


#End Region
        
    End Class

End Namespace