Imports System.Windows.Forms
Imports Miharu.Custody.Library.Forms.Arqueo
Imports Miharu.Custody.Library.Forms.Boveda
Imports Miharu.Custody.Library.Forms.Despacho
Imports Miharu.Custody.Library.Forms.Custodia
Imports Miharu.Desktop.Library.Forms
Imports Miharu.Desktop.Library
Imports Miharu.Custody.Library.Forms.Solicitudes
Imports Miharu.Custody.Library.Forms.Recepcion
Imports Miharu.Custody.Library.Forms.Parametrizacion.Solicitudes
Imports Miharu.Custody.Library.Forms.AdministracionBodega
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Public Class FormCustodyWorkSpace
    Inherits FormBase
    Implements IWorkspace

#Region " Declaraciones "

    Private Const MyPathProceso As String = "5.1"
    Private Const MyPathRecepcion As String = "5.1.1"
    Private Const MyPathAdministracionSolicitudes As String = "5.1.2"
    Private Const MyPathAtencionSolicitudes As String = "5.1.3"
    Private Const MyPathDespacho As String = "5.1.4"
    Private Const MyPathSolicitudesMasivas As String = "5.1.5"
    Private Const MyPathDescuelgue As String = "5.1.6"
    Private Const MyPathCustodia As String = "5.1.7"
    Private Const MyPathTrasladoCajas As String = "5.1.8"
    Private Const MyPathTrasladoCarpetas As String = "5.1.9"
    'Parametrizacion
    Private Const MyPathParametrizacion As String = "5.3"
    Private Const MyPathParametrizacionSolicitudes As String = "5.3.1"
    Private Const MyPathParametrizacionMotivo As String = "5.3.1.1"
    Private Const MyPathParametrizacionPrioridad As String = "5.3.1.2"
    Private Const MyPathParametrizacionTipo As String = "5.3.1.3"
    Private Const MyPathestructuraSolicitudes As String = "5.3.2"
    Private _ProcessLibrary As ProccessLibrary

#End Region

#Region " Impletemación IWorkspace"

    Public Property ProcessLibrary As IProcessLibrary Implements IWorkspace.ProcessLibrary
        Get
            Return _ProcessLibrary
        End Get
        Set(ByVal value As IProcessLibrary)
            _ProcessLibrary = CType(value, ProccessLibrary)
        End Set
    End Property

    Public Sub ConfigWorkspace() Implements IWorkspace.ConfigWorkspace
        Try
            ''Se habilitan los botones de acuerdo al perfil del usuario
            If Not Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathProceso) Then MainTabControl.TabPages.Remove(ProcesoTabPage)
            RecepcionButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathRecepcion)
            AtencionSolicitudesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathAtencionSolicitudes)
            AdministracionSolicitudesButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathAdministracionSolicitudes)
            DespachoButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathDespacho)
            CargueSolicitudesMasivasButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathSolicitudesMasivas)
            Button2.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathDescuelgue)
            CajaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathCustodia)
            TrasladoCajaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathTrasladoCajas)
            TrasladoCarpetaButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathTrasladoCarpetas)


            'Parametrizacion
            HerramientasToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathParametrizacion)
            SolicitudesToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathParametrizacionSolicitudes)
            MotivosPorRolToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathParametrizacionMotivo)
            PrioridadPorRolToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathParametrizacionPrioridad)
            TipoPorRolToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathParametrizacionTipo)
            EstructuraSolicitudesMasivasToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(MyPathestructuraSolicitudes)

            'Vencimientos
            ProximosAVencerToolStripMenuItem.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.LetrasVencimiento)

        Catch ex As Exception
            DMB.DesktopMessageShow("CargaPermisos", ex)
        End Try
    End Sub

#End Region

#Region " Eventos "

    Private Sub ProximosAVencerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ProximosAVencerToolStripMenuItem.Click
        Dim proximosvencer As New FormProximosVencer()
        proximosvencer.ShowDialog()
    End Sub

    Private Sub CajaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim objCustodiaCaja As New FormCustodiaCaja
        objCustodiaCaja.ShowDialog()
    End Sub

    Private Sub TipoPorRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipoPorRolToolStripMenuItem.Click
        Dim Tipos As New Formtipoxrol
        Tipos.ShowDialog()
    End Sub

    Private Sub PrioridadPorRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrioridadPorRolToolStripMenuItem.Click
        Dim Prioridades As New Formprioridadxrol
        Prioridades.ShowDialog()
    End Sub

    Private Sub MotivosPorRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MotivosPorRolToolStripMenuItem.Click
        Dim Motivo As New FormMotivoXRol
        Motivo.ShowDialog()
    End Sub

    Private Sub EstructuraSolicitudesMasivasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EstructuraSolicitudesMasivasToolStripMenuItem.Click
        Dim Solicitud As New FormEstructuraCargue
        Solicitud.ShowDialog()
    End Sub

    Private Sub CargueSolicitudesMasivasButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CargueSolicitudesMasivasButton.Click
        Dim Cargue As New FormCargueSolicitudes()
        Cargue.ShowDialog()
    End Sub

    Private Sub FormCustodyWorkSpace_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub RecepcionButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RecepcionButton.Click
        Dim objRecepcion As New FormRemisiones()
        objRecepcion.ShowDialog()
    End Sub

    Private Sub AtencionSolicitudesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AtencionSolicitudesButton.Click
        Dim objAtencionSolicitud As New FormAtencionSolicitudes()
        objAtencionSolicitud.ShowDialog()
    End Sub

    Private Sub AdministracionSolicitudesButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AdministracionSolicitudesButton.Click
        Dim objAdmonSolicitud As New FormAdministracionSolicitudes()
        objAdmonSolicitud.ShowDialog()
    End Sub

    Private Sub DespachoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DespachoButton.Click
        Dim objDespacho As New FormDespacho
        objDespacho.ShowDialog()
    End Sub

    Private Sub CajaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CajaButton.Click
        Dim objCustodiaCaja As New FormCustodiaCaja
        objCustodiaCaja.ShowDialog()
    End Sub

    Private Sub CreacionArqueoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CreacionArqueoButton.Click
        Dim ArqueoForm As New FormArqueo()
        ArqueoForm.Show()
    End Sub

    Private Sub BovedasButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BovedasButton.Click
        Dim objBovedaVisualizacion As New FormVisualizacionBoveda()
        objBovedaVisualizacion.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As EventArgs) Handles Button2.Click
        Dim objSolicitudesPorCliente As New FormSolicitudesPorCliente()
        objSolicitudesPorCliente.ShowDialog()
    End Sub

    Private Sub TrasladoCajaButton_Click(sender As System.Object, e As System.EventArgs) Handles TrasladoCajaButton.Click
        Dim objTrasladoCajas As New FormMovimientoCajas()
        objTrasladoCajas.ShowDialog()
    End Sub

    Private Sub TrasladoCarpetaButton_Click(sender As System.Object, e As System.EventArgs) Handles TrasladoCarpetaButton.Click
        Dim objTrasladoCarpetas As New FormMovimientoCarpetas()
        objTrasladoCarpetas.ShowDialog()
    End Sub

    Private Sub CarpetasYDocumentosToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CarpetasYDocumentosToolStripMenuItem.Click
        Dim formImprimirCbarrasFIleFolder As New Forms.CBarras.FormCBarrasFolderFile()
        formImprimirCbarrasFIleFolder.ShowDialog()
    End Sub
#End Region
    
End Class