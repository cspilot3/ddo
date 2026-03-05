Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.Reportes.CentroDistribucion

Namespace Forms.CentroDistribucion

    Public Class FormRemisionCiudades
        Inherits FormBase

#Region "Declaraciones"

        Private _Sede, _Entidad, _Proyecto, _CentroProcesamiento As Short
        Private _NroRemision, _NroGuia As String
        Private _Remisiones As New DataSet

#End Region

#Region "Propiedades"

        Public Property Sede As Short
            Get
                Return _Sede
            End Get
            Set(value As Short)
                _Sede = value
            End Set
        End Property

        Public Property Entidad As Short
            Get
                Return _Entidad
            End Get
            Set(value As Short)
                _Entidad = value
            End Set
        End Property

        Public Property Proyecto As Short
            Get
                Return _Proyecto
            End Get
            Set(value As Short)
                _Proyecto = value
            End Set
        End Property

        Public Property CentroProcesamiento As Short
            Get
                Return _CentroProcesamiento
            End Get
            Set(value As Short)
                _CentroProcesamiento = value
            End Set
        End Property

        Public Property NroRemision As String
            Get
                Return _NroRemision
            End Get
            Set(value As String)
                _NroRemision = value
            End Set
        End Property

        Public Property NroGuia As String
            Get
                Return _NroGuia
            End Get
            Set(value As String)
                _NroGuia = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            InitializeComponent()
            Sede = Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede
            Entidad = Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad
            Proyecto = Program.RiskGlobal.Proyecto
            CentroProcesamiento = Program.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento
            CargarRemisiones()
        End Sub

#End Region

#Region "Eventos"

        Private Sub btnCrearRemision_Click(sender As System.Object, e As System.EventArgs) Handles btnCrearRemision.Click
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                'Program.DesktopGlobal.PuestoTrabajoRow
                Dim Remision As New DBCore.SchemaProcess.TBL_Remision_CajaType
                Dim Boveda = dbmCore.SchemaCustody.TBL_Boveda_Centro_Procesamiento.DBFindByfk_Entidad_Centro_Procesamientofk_Sede_Centro_Procesamientofk_Centro_Procesamiento(_Entidad, _Sede, _CentroProcesamiento)(0).ToTBL_Boveda_Centro_ProcesamientoSimpleType

                Dim id_Remision_Caja = dbmCore.SchemaProcess.TBL_Remision_Caja.DBNextId()

                Remision.Id_Remision_Caja = id_Remision_Caja
                Remision.fk_Entidad = _Entidad
                Remision.fk_Sede = _Sede
                Remision.fk_Centro_Procesamiento = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                Remision.fk_Boveda = Boveda.fk_Boveda
                Remision.fk_Estado = DBCore.EstadoEnum.Centro_Distribucion
                Remision.fk_Entidad_Cliente = Program.RiskGlobal.ProyectoRow.fk_Entidad
                Remision.fk_Proyecto_Cliente = Program.RiskGlobal.ProyectoRow.id_Proyecto
                Remision.Abierta = True
                Remision.Remitida = False
                dbmCore.SchemaProcess.TBL_Remision_Caja.DBInsert(Remision)
                If RegistrarRemision(id_Remision_Caja) Then
                    dbmCore.Transaction_Commit()
                    CargarRemisiones()
                Else
                    dbmCore.Transaction_Rollback()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CrearRemision", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub btnCerrarRemision_Click(sender As System.Object, e As System.EventArgs) Handles btnCerrarRemision.Click
            Dim Resultado As DialogResult
            Resultado = MessageBox.Show("Desea cerrar la remision seleccionada", "Confirmar Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If Resultado = Windows.Forms.DialogResult.Yes Then

                Dim FormRegistroGuia As New Forms.CentroDistribucion.FormRegistroGuia
                Dim idRemisionCaja = CLng(GridRemisiones.SelectedRows(0).Cells(0).Value)
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Dim ItemsRemision = New DBCore.Schemadbo.CTA_Items_Remision_ImpresionDataTable
                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    ItemsRemision = dbmCore.Schemadbo.CTA_Items_Remision_Impresion.DBFindById_Remision_Caja(idRemisionCaja)
                Catch ex As Exception
                    MsgBox("Error revisando los documentos de la resmisión. - " & ex.Message)
                Finally
                    dbmCore.Connection_Close()
                End Try
                If ItemsRemision.Rows.Count = 0 Then
                    MessageBox.Show("No se encontraron documentos asignados en la resmisión.", "Remisión Vacía", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    FormRegistroGuia.id_Remision_Caja = idRemisionCaja
                    Resultado = FormRegistroGuia.ShowDialog()
                    If Resultado = Windows.Forms.DialogResult.OK Then
                        NroRemision = FormRegistroGuia.NroRemision
                        NroGuia = FormRegistroGuia.NroGuia
                        Dim FormImpresionRemision As New Forms.Reportes.CentroDistribucion.FormImpresionRemision(idRemisionCaja)
                        FormImpresionRemision.ShowDialog()
                        CargarRemisiones()
                    Else
                        NroRemision = ""
                        NroGuia = ""
                    End If
                End If
            End If
        End Sub

        Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

            Dim Index As Integer = GridRemisiones.CurrentRow.Index
            If Index >= 0 Then
                RegistrarRemision(CInt(GridRemisiones.Rows(Index).Cells(0).Value))
            Else
                MessageBox.Show("No se ha seleccionado remisión", "RemisionCiudades", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End Sub

        Private Sub GridRemisiones_DoubleClick(sender As System.Object, e As System.EventArgs) Handles GridRemisiones.DoubleClick
            RegistrarRemision(CInt(GridRemisiones.CurrentRow.Cells(0).Value))
        End Sub

#End Region

#Region "Funciones"

        Private Sub CargarRemisiones()

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim dtRemisiones = dbmCore.Schemadbo.CTA_Remisiones.DBFindByfk_Sedefk_Entidad_Clientefk_Proyecto_ClienteAbiertaRemitida(_Sede, Program.RiskGlobal.ProyectoRow.fk_Entidad, Program.RiskGlobal.ProyectoRow.id_Proyecto, True, False)
                _Remisiones.Tables.Clear()
                _Remisiones.Tables.Add(dtRemisiones)

                GridRemisiones.DataSource = _Remisiones.Tables(0)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaDataSet", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Function RegistrarRemision(id_Remision As Long) As Boolean

            Dim LineaProceso As New Forms.LineaProceso.FormSeleccionarLineaProceso

            If LineaProceso.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim RegistroRemision As New FormRegistrarRemision
                RegistroRemision.Remision = id_Remision
                RegistroRemision.ModoRegistroORecepcionRemision = True
                RegistroRemision.LineaProceso = LineaProceso.LineaProceso
                RegistroRemision.ShowDialog()

                Return True
            Else
                Return False
            End If

        End Function

#End Region

    End Class

End Namespace
