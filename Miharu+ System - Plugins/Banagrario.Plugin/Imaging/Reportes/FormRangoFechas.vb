Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Reportes

    Public Class FormRangoFechas

#Region " Declaraciones "

        Private _UsaDigitador As Boolean

        Public _plugin As BanagrarioImagingPlugin = Nothing

#End Region

#Region " Propiedades "

        Public Property FechaInicial() As Date
            Get
                'Return dtpFechaInicial.Value.Date.AddHours(16)
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property
        Public Property FechaFinal() As Date
            Get
                'Return dtpFechaFinal.Value.Date.AddHours(40).AddMilliseconds(-10)
                Return dtpFechaFinal.Value.Date.AddHours(24).AddMilliseconds(-10)
            End Get
            Set(ByVal value As Date)
                dtpFechaFinal.Value = value
            End Set
        End Property

        Public Property Usuario() As Integer
            Get
                Return Integer.Parse(UsuarioDesktopComboBox.SelectedValue.ToString)
            End Get
            Set(ByVal value As Integer)
                UsuarioDesktopComboBox.SelectedValue = value
            End Set
        End Property

#End Region

#Region " Constructor "

        Public Sub New(ByVal nUsaDigitador As Boolean, ByVal nPlugin As BanagrarioImagingPlugin)

            InitializeComponent()

            _UsaDigitador = nUsaDigitador
            _plugin = nPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub
        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub
        Private Sub FormRangoFechas_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

            UsuarioDesktopComboBox.Enabled = UsuarioLabel.Enabled = _UsaDigitador

            If _UsaDigitador Then

                Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                Dim tblUsuarios As DBImaging.SchemaSecurity.CTA_UsuarioDataTable
                Try
                    dbmImaging.Connection_Open(1)
                    tblUsuarios = dbmImaging.SchemaSecurity.CTA_Usuario.DBFindByfk_Entidadid_Usuario(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Nothing)

                    Utilities.LlenarCombo(UsuarioDesktopComboBox, tblUsuarios, "id_Usuario", "Nombres", True, "-1", "- Todos -")
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Carga de Usuarios", ex)
                Finally
                    dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

#End Region

#Region " Funciones "

        'Private Function Validar() As Boolean
        '    If dtpFechaInicial.Value > dtpFechaFinal.Value Then
        '        MessageBox.Show("La Fecha Final debe ser superior a la Fecha Inicial", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        dtpFechaFinal.Focus()

        '    Else
        '        Return True
        '    End If

        '    Return False
        'End Function

#End Region

#Region " Metodos "


#End Region

    End Class
End Namespace