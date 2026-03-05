Imports System.Windows.Forms
Imports DBAgrario.SchemaConfig
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Reportes.TiempoCiudadesConsolidado

    Public Class FormTiempoCiudadesConsolidado

#Region " Declaraciones "

        Private _regionalDataTable As TBL_RegionalDataTable
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

        Public Property ModoFechaProceso() As Boolean
            Get
                Return CkbxFechaProceso.Checked
            End Get
            Set(ByVal value As Boolean)
                CkbxFechaProceso.Checked = value
            End Set
        End Property

        Public Property FechaMovimiento() As Date
            Get
                Return dtpFechaMovimiento.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaMovimiento.Value = value
            End Set
        End Property

        Public Property ModoFechaMovimiento() As Boolean
            Get
                Return CkbxFechaMovimiento.Checked
            End Get
            Set(ByVal value As Boolean)
                CkbxFechaMovimiento.Checked = value
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

            _Regional = CShort(RegionalDesktopComboBox.SelectedValue)
            _COB = CShort(COBDesktopComboBox.SelectedValue)
            _Oficina = CShort(OficinaDesktopComboBox.SelectedValue)

            Me.DialogResult = DialogResult.OK
            Me.Hide()
        End Sub

        Private Sub FormParametrosContenedor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            CargaRegional()
        End Sub

        Private Sub RegionalDesktopComboBox_LostFocus(sender As Object, e As System.EventArgs) Handles RegionalDesktopComboBox.LostFocus
            COBDesktopComboBox.Focus()
        End Sub

        Private Sub RegionalDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RegionalDesktopComboBox.SelectedIndexChanged
            CargaCOB()
        End Sub

        Private Sub COBDesktopComboBox_LostFocus(sender As Object, e As System.EventArgs) Handles COBDesktopComboBox.LostFocus
            OficinaDesktopComboBox.Focus()
        End Sub

        Private Sub COBDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles COBDesktopComboBox.SelectedIndexChanged
            CargaOficina()
        End Sub

        Private Sub OficinaDesktopComboBox_LostFocus(sender As Object, e As System.EventArgs) Handles OficinaDesktopComboBox.LostFocus
            btnAceptar.Focus()
        End Sub

        Private Sub dtpFechaProceso_LostFocus(sender As Object, e As System.EventArgs) Handles dtpFechaProceso.LostFocus
            dtpFechaMovimiento.Focus()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dmBanAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dmBanAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dmBanAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                _regionalDataTable = dmBanAgrario.SchemaConfig.TBL_Regional.DBGet(Nothing)
                _cobDataTable = dmBanAgrario.SchemaConfig.TBL_COB.DBGet(Nothing)
                _oficinaDataTable = dmBanAgrario.SchemaConfig.TBL_Oficina.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                dmBanAgrario.Connection_Close()
            End Try
        End Sub

        Private Sub CargaRegional()
            Try
                Utilities.LlenarCombo(RegionalDesktopComboBox, _regionalDataTable, TBL_RegionalEnum.id_Regional.ColumnName, TBL_RegionalEnum.Nombre_Regional.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

        Private Sub CargaCob()
            Try
                Dim cobView = _cobDataTable.DefaultView
                cobView.RowFilter = "fk_Regional = " + RegionalDesktopComboBox.SelectedValue.ToString()

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