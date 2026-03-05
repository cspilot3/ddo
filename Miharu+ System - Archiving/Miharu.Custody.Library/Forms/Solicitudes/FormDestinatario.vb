Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports System.Linq
Imports System.ComponentModel

Namespace Forms.Solicitudes

    Public Class FormDestinatario

        Dim Flag As Boolean
        Dim _IdEntidad As Short
        Dim dtSolicitudesEncontradas As DataTable
        Dim SolicitudSeleccionada, SolicitudesDescolgarCount, ContadorDescuelgue, TotalDocumentosParaDescolgar As Integer
        Private _fk_Entidad As Short
        Private _fk_Sede As Short
        Public _DestanatarioCancelar As Boolean
        Private _desktopDataGridViewControl As Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl
        Public _Nombres As String
        Public _Sede As String
        Public _Direccion As String
        Public _Precinto As String


#Region " Eventos "


        Sub New(desktopDataGridViewControl As Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl, IdEntidad As Short)
            InitializeComponent()
            ' TODO: Complete member initialization 
            _desktopDataGridViewControl = desktopDataGridViewControl
            _IdEntidad = IdEntidad
        End Sub


        Private Sub FormDestinatario_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Dim dmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Try
                dmCore.Connection_Open(Program.Sesion.Usuario.id)
                'Dim EntidadEncontrada = dmCore.SchemaSecurity.CTA_Entidad().DBFindByid_Entidad(_IdEntidad).Rows(0)("Nombre_Entidad").ToString()
                'Me.lblEntidadEncontrada.Text = EntidadEncontrada
                Flag = True

                'CargarFiltroSolicitudes(CShort(Me._IdEntidad), 1500)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Formulario", ex)
            Finally
                dmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            _DestanatarioCancelar = True
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
            If NombresTextBox.Text = "" Then
                MessageBox.Show("Por favor ingresar los nombres", "Destinatario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Else
                _Nombres = NombresTextBox.Text
            End If
            If SedeTextBox.Text = "" And Not NombresTextBox.Text = "" Then
                MessageBox.Show("Por favor ingresar la sede", "Destinatario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Else
                _Sede = SedeTextBox.Text
            End If
            If DireccionTextBox.Text = "" And Not SedeTextBox.Text = "" And Not NombresTextBox.Text = "" Then
                MessageBox.Show("Por favor ingresar la dirección", "Destinatario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Else
                _Direccion = DireccionTextBox.Text
            End If
            _Precinto = PrecintoTextBox.Text
            If Not DireccionTextBox.Text = "" And Not SedeTextBox.Text = "" And Not NombresTextBox.Text = "" Then
                Me.Close()
            End If
        End Sub


#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()


        End Sub

        Private Sub CargaFiltroEntidad()
            Dim dmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                'Carga filtro de las Entidades
                Flag = False
                Dim tEntidades = dmArchiving.Schemadbo.CTA_Entidad_Rol_Usuario.DBFindByid_Usuario(Program.Sesion.Usuario.id).DefaultView.ToTable(True)
                'Utilities.LlenarCombo(EntidadDesktopComboBox, tEntidades, tEntidades.Columns("fk_Entidad").ColumnName, tEntidades.Columns("Nombre_Entidad").ColumnName, True, "-1", "Seleccione...")
                'CargarFiltroSolicitudes(CShort(EntidadDesktopComboBox.SelectedValue), 1500)
                Flag = True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaFiltroEntidad", ex)
            Finally
                dmArchiving.Connection_Close()
            End Try
        End Sub


#End Region

    End Class

End Namespace