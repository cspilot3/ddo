Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.CentroDistribucion
Imports Slyg.Tools

Namespace Forms.CentroDistribucion

    Public Class FormSeleccionarRemision
        Inherits FormBase


        Public Sub New()
            InitializeComponent()
            CargarRemisiones()
        End Sub

#Region " Declaraciones "

        Private _LineaProceso As Integer
        Private _IdRemision As Integer
        Private DtRemisiones As DataTable

#End Region

#Region " Propiedades "

        Public ReadOnly Property Remision As Integer
            Get
                Return _IdRemision
            End Get
        End Property

        Public ReadOnly Property LineaProceso As Integer
            Get
                Return _LineaProceso
            End Get
        End Property

#End Region

#Region "EVENTOS"

        Private Sub btnBuscarRemision_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscarRemision.Click

            Dim view As DataView = Utilities.ClonarDataTable(DtRemisiones).DefaultView
            If txtRemision.Text <> "" Then
                Dim filtro As String = "Nro_Remision = " & txtRemision.Text
                view.RowFilter = filtro
            End If
            gridRemisiones.AutoGenerateColumns = False
            gridRemisiones.DataSource = view.ToTable()

        End Sub

        Private Sub btnCerrarRemision_Click(sender As System.Object, e As System.EventArgs) Handles btnCerrarRemision.Click
            CerrarRemision()
        End Sub

        Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click
            Procesar(CInt(gridRemisiones.SelectedRows(0).Cells("IdCajaRemision").Value))
            'Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

        Private Sub btnCerrar_Click(sender As System.Object, e As System.EventArgs) Handles btnCerrar.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

#End Region

#Region "METODOS"

        Public Sub CargarRemisiones()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim dtRemisiones = dbmCore.SchemaProcess.PA_Recibir_Remision.DBExecute(Program.DesktopGlobal.BovedaRow.id_Boveda, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, CType(CInt(Program.Sesion.Parameter("_idLineaProceso")), Global.Slyg.Tools.SlygNullable(Of Integer)), 0)
                DtRemisiones = dtRemisiones
                gridRemisiones.DataSource = DtRemisiones
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaRemisiones", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub CerrarRemision()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Dim Carpetas = gridRemisiones.SelectedRows(gridRemisiones.CurrentRow.Index).Cells(1).Value
            Dim Documentos = gridRemisiones.SelectedRows(gridRemisiones.CurrentRow.Index).Cells(2).Value
            Dim Remision = gridRemisiones.SelectedRows(gridRemisiones.CurrentRow.Index).Cells(0).Value

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If CLng(Documentos) = 0 Then

                    Dim Update As New DBCore.SchemaProcess.TBL_Remision_CajaType
                    Update.Abierta = False
                    dbmCore.SchemaProcess.TBL_Remision_Caja.DBUpdate(Update, CLng(Remision))

                Else
                    Dim Seleccion As DialogResult
                    Dim dCajasRemision = dbmCore.Schemadbo.CTA_Items_Remision_Impresion.DBFindById_Remision_CajaFecha_Recibe(CLng(Remision), DBNull.Value)

                    Seleccion = MessageBox.Show("No se han recibido " & Carpetas.ToString & " Carpetas y " & Documentos.ToString & " Documentos." & Chr(13) & Chr(13) & " ¿Desea confirmar el cierre de la remision " & gridRemisiones.SelectedRows(gridRemisiones.CurrentRow.Index).Cells(3).Value.ToString(), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If Seleccion = Windows.Forms.DialogResult.Yes Then

                        dbmCore.Transaction_Begin()

                        Dim Respuesta = dbmCore.SchemaProcess.PA_Cerrar_Remision_Ciudades.DBExecute(CType(CInt(Remision), SlygNullable(Of Long)), Program.Sesion.Usuario.id, CType(CInt(dCajasRemision.Rows(0).ItemArray(7)), SlygNullable(Of Integer)))
                        Dim mensaje As String = String.Empty

                        For Each fila As DataRow In Respuesta.Rows
                            mensaje += Chr(13) & fila.ItemArray(0).ToString
                        Next

                        dbmCore.Transaction_Commit()

                        MessageBox.Show("Se crearon las siguientes líneas para que sean procesadas en el centro de procesamiento de origen: " & Chr(13) & mensaje, "Fin del proceso", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    ElseIf Seleccion = Windows.Forms.DialogResult.No Then
                        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    End If
                End If

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("CerrarRemision", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
            End Try
        End Sub

        Public Sub Procesar(Remision As Integer)
            _IdRemision = CInt(gridRemisiones.SelectedRows(0).Cells("IdCajaRemision").Value)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End Sub

#End Region

        Private Sub gridRemisiones_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gridRemisiones.DoubleClick
            Procesar(CInt(gridRemisiones.CurrentRow.Cells("idCajaRemision").Value))
        End Sub
    End Class

End Namespace
