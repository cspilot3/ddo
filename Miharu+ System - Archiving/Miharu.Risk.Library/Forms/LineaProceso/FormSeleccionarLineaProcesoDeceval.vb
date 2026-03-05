Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.LineaProceso

    Public Class FormSeleccionarLineaProcesoDeceval
        Inherits FormBase

#Region " Declaraciones "

        Private _TipoCaptura As DesktopConfig.TipoCaptura
        Private _LineaProceso As Integer
        Private dtLineaProceso As DataTable
        Private _Estado As DBCore.EstadoEnum

#End Region

#Region " Constructor "

        Public Sub New(ByVal TipoCaptura As DesktopConfig.TipoCaptura)

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TipoCaptura = TipoCaptura
        End Sub

        Public Sub New(ByVal Estado As DBCore.EstadoEnum)

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Estado = Estado
        End Sub

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TipoCaptura = Nothing
        End Sub
#End Region

#Region " Propiedades "

        Public ReadOnly Property LineaProceso As Integer
            Get
                Return _LineaProceso
            End Get
        End Property

#End Region

#Region " Eventos "

        Private Sub FormLineaProceso_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            'Librea cualquier línea de procesos de sesión antes de asignar la nueva.
            Program.Sesion.Parameter("_idLineaProceso") = Nothing

            'Lista de las lineas de proceso disponibles.
            CargaLineasProceso()
        End Sub

        Private Sub SeleccionarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeleccionarButton.Click
            SeleccionarLinea()
        End Sub

        Private Sub LineasDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles LineasDataGridView.CellDoubleClick
            LineasDataGridView.Rows(e.RowIndex).Selected = True
            SeleccionarLinea()
        End Sub

        Private Sub FormSeleccionarLineaProceso_FormClosing(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            If Me.DialogResult = DialogResult.Cancel Then
                Me.DialogResult = DialogResult.No
            End If
        End Sub

        Private Sub FormSeleccionarLineaProceso_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            FiltrarLineasProceso()
        End Sub
#End Region

#Region " Metodos "

        Private Sub CargaLineasProceso()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim Estado As DBCore.EstadoEnum

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Select Case _TipoCaptura
                    Case DesktopConfig.TipoCaptura.Primera_Captura
                        Estado = DBCore.EstadoEnum.Mesa_de_Control_Deceval

                    Case DesktopConfig.TipoCaptura.Segunda_Captura
                        Estado = DBCore.EstadoEnum.Mesa_de_Control_Deceval

                    Case DesktopConfig.TipoCaptura.Tercera_Captura
                        Estado = DBCore.EstadoEnum.Mesa_de_Control_Deceval

                    Case Else
                        If Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede <> Program.DesktopGlobal.BovedaRow.fk_Sede Then
                            Estado = DBCore.EstadoEnum.Centro_Distribucion
                        Else
                            Estado = DBCore.EstadoEnum.Empaque
                        End If

                End Select

                'DataTable Líneas de Proceso
                dtLineaProceso = dbmArchiving.Schemadbo.CTA_Linea_Proceso.DBFindByfk_Entidad_Procesofk_Sede_Procesofk_Centro_Procesamientofk_Entidad_Clientefk_Proyectofk_EstadoActivo(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                                                                                                                      Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento,
                                                                                                                                                                                  Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Estado, True)

                If dtLineaProceso.Rows.Count > 0 Then
                    LineasDataGridView.AutoGenerateColumns = False
                    LineasDataGridView.DataSource = dtLineaProceso
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("No existen líneas de proceso disponibles.", "Líneas de Proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    Me.DialogResult = DialogResult.No
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaLineasProceso", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub SeleccionarLinea()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If LineasDataGridView.SelectedRows.Count > 0 Then
                    Program.Sesion.Parameter("_idLineaProceso") = CInt(LineasDataGridView.SelectedRows(0).Cells(0).Value)
                    Me._LineaProceso = CInt(LineasDataGridView.SelectedRows(0).Cells(0).Value)

                    'Se actualiza la línea con el usuario y fecha log actual.
                    Dim typeLineaProceso As New SchemaRisk.TBL_Linea_ProcesoType
                    typeLineaProceso.fk_Usuario = Program.Sesion.Usuario.id
                    typeLineaProceso.Fecha_Log = SlygNullable.SysDate
                    dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBUpdate(typeLineaProceso, CInt(Program.Sesion.Parameter("_idLineaProceso")))

                    Me.DialogResult = DialogResult.OK

                    ''Redirecciona a la ventana de Mesa de Control Deceval
                    'Dim formMesaControlDeceval As New Forms.MesaControlFisicos.FormMesaControlFisicosDeceval(DesktopConfig.TipoCaptura.Primera_Captura)
                    'formMesaControlDeceval.ShowDialog()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una línea de proceso.", "Selección Línea Proceso", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("SeleccionarLinea", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub FiltrarLineasProceso()
            Dim view As DataView = Utilities.clonarDataTable(dtLineaProceso).DefaultView

            If OtBuscarDesktopTextBox.Text <> "" Then
                Dim filtro As String = "id_Linea_Proceso = " & OtBuscarDesktopTextBox.Text
                view.RowFilter = filtro
            End If

            LineasDataGridView.AutoGenerateColumns = False
            LineasDataGridView.DataSource = view.ToTable()
        End Sub

#End Region

    End Class

End Namespace