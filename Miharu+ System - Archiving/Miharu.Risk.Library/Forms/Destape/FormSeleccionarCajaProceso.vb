Imports System.Windows.Forms
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Destape

    Public Class FormSeleccionarCajaProceso
        Inherits FormBase

#Region " Declaraciones "

        Dim TableCajas As DataTable
        Dim _CajaSeleccionada As String

#End Region

#Region " Propiedades "

        Public ReadOnly Property CajaSeleccionada() As String
            Get
                Return _CajaSeleccionada
            End Get
        End Property

#End Region

#Region " Funciones "
        Public Sub CargarCajas()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            TableCajas = Utilities.clonarDataTable(dbmArchiving.SchemaCore.CTA_caja.DBFindByEs_Procesofk_Entidadfk_Sede(True, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede))
            dbmArchiving.Connection_Close()

            cajaListBox.DataSource = TableCajas
            cajaListBox.ValueMember = "id_caja"
            cajaListBox.DisplayMember = "codigo_caja"
        End Sub

        Public Sub CargarCajasFiltro()
            Dim view As DataView = Utilities.clonarDataTable(TableCajas).DefaultView
            view.RowFilter = "codigo_caja like '%" & CajaBuscarDesktopTextBox.Text & "%'"

            cajaListBox.DataSource = view.ToTable
            cajaListBox.ValueMember = "codigo_caja"
            cajaListBox.DisplayMember = "codigo_caja"

        End Sub

#End Region

#Region " Eventos "

        Private Sub cajaListBox_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles cajaListBox.DoubleClick
            _CajaSeleccionada = CStr(cajaListBox.Text)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub FormSeleccionarCajaProceso_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCajas()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            CargarCajasFiltro()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            _CajaSeleccionada = CStr(cajaListBox.Text)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            _CajaSeleccionada = Program.RiskGlobal.CajaProceso
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub cajaListBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles cajaListBox.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        End Sub

        Private Sub CajaBuscarDesktopTextBox_LostFocus(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles CajaBuscarDesktopTextBox.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If CajaBuscarDesktopTextBox.Text = "" Then
                    cajaListBox.Focus()
                Else
                    BuscarButton.Focus()
                End If
            End If
        End Sub

#End Region

    End Class

End Namespace