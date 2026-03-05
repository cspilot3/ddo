Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Reportes.DetalleDestapeEmpaque

    Public Class FormTiempoCiudadesDetalladoDestape

#Region " Declaraciones "

        Private _usuarioTable As DBSecurity.SchemaSecurity.TBL_UsuarioDataTable
        Private _contenedorTable As DBAgrario.SchemaProcess.TBL_DestapeDataTable
        Public Plugin As BanagrarioImagingPlugin = Nothing

#End Region

#Region " Constructor "

        Public Sub New(ByVal nPlugin As BanagrarioImagingPlugin)
            InitializeComponent()
            Plugin = nPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Propiedades "

        Dim _reporte As Integer
        Public Property Reporte() As Integer
            Get
                Return _Reporte
            End Get
            Set(ByVal value As Integer)
                _Reporte = value
            End Set
        End Property

        Dim _tipoDetalle As String
        Public Property TipoDetalle() As String
            Get
                Return _TipoDetalle
            End Get
            Set(ByVal value As String)
                _TipoDetalle = value
            End Set
        End Property

        Dim _usuario As Integer
        Public Property Usuario() As Integer
            Get
                Return _Usuario
            End Get
            Set(ByVal value As Integer)
                _Usuario = value
            End Set
        End Property

        Dim _contenedor As String
        Public Property Contenedor() As String
            Get
                Return _Contenedor
            End Get
            Set(ByVal value As String)
                _Contenedor = value
            End Set
        End Property

#End Region

#Region " Metodos "

        Private Sub CargaTablas()
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(Plugin.BancoAgrarioConnectionString)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                dbmAgrario.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                _usuarioTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBGet(Nothing)
                _contenedorTable = dbmAgrario.SchemaProcess.TBL_Destape.DBGet(Nothing)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub CargarDatos()
            Try
                Utilities.LlenarCombo(UsuarioComboBox, _usuarioTable, _usuarioTable.id_UsuarioColumn.ColumnName, _usuarioTable.Login_UsuarioColumn.ColumnName, True, "-1", "Seleccione")
                Utilities.LlenarCombo(ContenedorComboBox, _contenedorTable, _contenedorTable.id_DestapeColumn.ColumnName, _contenedorTable.codigo_ContenedorColumn.ColumnName, True, "-1", "Seleccione")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltros", ex)
            End Try
        End Sub

#End Region

#Region " Eventos "

        Private Sub Form_TiempoCiudadesDetalladoDestape_Load(sender As Object, e As EventArgs) Handles Me.Load
            CargarDatos()
        End Sub

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            If Not ReporteComboBox.SelectedItem = Nothing Then
                _Reporte = ReporteComboBox.SelectedIndex + 1
                _TipoDetalle = ReporteComboBox.SelectedItem
                _Usuario = UsuarioComboBox.SelectedValue
                _Contenedor = ContenedorComboBox.SelectedValue
                Me.DialogResult = DialogResult.OK
                Me.Hide()
            Else
                MsgBox("Debe seleccionar reporte", MsgBoxStyle.OkOnly, "Filtro")
            End If
        End Sub

#End Region

    End Class

End Namespace
