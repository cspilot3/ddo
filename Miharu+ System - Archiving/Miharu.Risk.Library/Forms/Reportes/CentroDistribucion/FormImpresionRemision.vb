Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library.Config
Imports Microsoft.Reporting.WinForms

Namespace Forms.Reportes.CentroDistribucion

    Public Class FormImpresionRemision
        Inherits Desktop.Library.FormBase

#Region " Declaraciones "
        Private _id_Remision As Long
        Private _FechaReporte As Object
        Private _Ciudad As Object
        Private _Responsable_Boveda As Object
        Private _Nombre_Boveda As Object
        Private _Entidad_Custodiada As Object
        Private _Entidad As Object
        Private _Proyecto As Object
        Private _Nro_Remision As Object
        Private _Nro_Guia As Object
        Private _Responsable As Object
        Private _Nombre_Sede As Object
#End Region

#Region " Constructor "
        Public Sub New(ByVal idRemesion As Long)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _id_Remision = idRemesion
        End Sub
#End Region

#Region " Eventos "
        Private Sub FormImpresionRemision_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarReporte()
            Me.rptRemision.RefreshReport()
        End Sub
#End Region

#Region " Metodos "
        Private Sub CargarReporte()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim ItemsRemision = dbmCore.Schemadbo.CTA_Items_Remision_Impresion.DBFindById_Remision_Caja(_id_Remision)
                If rptRemision.LocalReport.DataSources.Count > 0 Then rptRemision.LocalReport.DataSources.RemoveAt(0)

                'Reporte con imagen
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim dataReporte = New xsdRemision.ItemsRemisionDataTable
                For Each remision In ItemsRemision
                    Dim fila = dataReporte.NewItemsRemisionRow()
                    fila.CBarras_Folder = remision.CBarras_Folder
                    fila.CBarras_File = remision.CBarras_File
                    fila.Tipo_Documental = remision.Nombre_Documento
                    dataReporte.Rows.Add(fila)
                Next

                'Asignación parametros puntuales
                _FechaReporte = ItemsRemision.Rows(0)("Fecha_Remite")
                _Ciudad = ItemsRemision.Rows(0)("Nombre_Sede")
                _Responsable_Boveda = ItemsRemision.Rows(0)("Responsable_Boveda")
                _Nombre_Boveda = ItemsRemision.Rows(0)("Nombre_Boveda")
                _Entidad_Custodiada = ItemsRemision.Rows(0)("Nombre_Entidad")
                _Entidad = ItemsRemision.Rows(0)("Entidad_Cliente")
                _Proyecto = ItemsRemision.Rows(0)("Proyecto_Cliente")
                If Not IsDBNull(ItemsRemision.Rows(0)("Nro_Remision")) Then
                    _Nro_Remision = ItemsRemision.Rows(0)("Nro_Remision")
                Else
                    _Nro_Remision = "Sin Información"
                End If

                If Not IsDBNull(ItemsRemision.Rows(0)("Nro_Guia")) Then
                    _Nro_Guia = ItemsRemision.Rows(0)("Nro_Guia")
                Else
                    _Nro_Guia = "Sin Información"
                End If

                _Responsable = ItemsRemision.Rows(0)("Responsable_Remision")
                _Nombre_Sede = ItemsRemision.Rows(0)("Nombre_Sede")

                Dim Parametros As New List(Of ReportParameter)

                Parametros.Add(New ReportParameter("FechaReporteParameter", _Ciudad.ToString() + " " + _FechaReporte.ToString()))
                Parametros.Add(New ReportParameter("UsuarioDestinoParameter", _Responsable_Boveda.ToString))
                Parametros.Add(New ReportParameter("EntidadDestinoParameter", _Nombre_Boveda.ToString))
                Parametros.Add(New ReportParameter("MensajeEntidadCustodiadaParameter", "Por medio de la presente se remiten los siguiente documentos para su custodia en boveda de procesos y canje: " + _Entidad_Custodiada.ToString))
                Parametros.Add(New ReportParameter("EntidadParameter", _Entidad.ToString))
                Parametros.Add(New ReportParameter("ProyectoParameter", _Proyecto.ToString))
                Parametros.Add(New ReportParameter("NumeroRemisionParameter", _Nro_Remision.ToString))
                Parametros.Add(New ReportParameter("NumeroGuiaParameter", _Nro_Guia.ToString))
                Parametros.Add(New ReportParameter("ResponsableEnvioParameter", _Responsable.ToString))
                Parametros.Add(New ReportParameter("SedeEnvioParameter", _Nombre_Sede.ToString))
                rptRemision.LocalReport.SetParameters(Parametros)

                Utilities.NewDataSource(rptRemision, "ItemsRemision", dataReporte)
                Me.rptRemision.RefreshReport()

                'Cambia de estado a la remisión (520 - 5000) y las respectivas cajas.
                Dim dtRemision = dbmCore.SchemaProcess.TBL_Remision_Caja.DBGet(_id_Remision)

                If dtRemision(0).Isfk_BovedaNull() Then
                    dbmCore.Schemadbo.PA_Especial_Cambia_Estado_Remision.DBExecute(_id_Remision, DBCore.EstadoEnum.Entregado_a_custodia_cliente)

                    Dim dCajasRemision = dbmCore.Schemadbo.CTA_Items_Remision.DBFindByfk_Remision_Cajafk_Caja(_id_Remision, Nothing)
                    For Each cajas In dCajasRemision
                        'Actualiza los estados de folders y files
                        Dim dItems = dbmArchiving.Schemadbo.CTA_Items_Caja.DBFindByIdCajaModulofk_Estado(cajas.fk_Caja, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Enviado_a_custodia)
                        For Each item In dItems
                            If item.fk_Estado = DBCore.EstadoEnum.Asignado_a_Remision Then
                                If item.CBarras.Substring(0, 3) = "000" Then 'Folder
                                    Utilities.ActualizaEstadoFolder(dbmArchiving, dbmCore, item.CBarras, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Entregado_a_custodia_cliente, Program.Sesion.Usuario.id, item.fk_OT)
                                Else 'File
                                    Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, item.CBarras, item.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Entregado_a_custodia_cliente, Program.Sesion.Usuario.id, Nothing)
                                End If
                            End If
                        Next
                    Next
                Else
                    'dbmCore.Schemadbo.PA_Especial_Cambia_Estado_Remision.DBExecute(_id_Remision, DBCore.EstadoEnum.Enviado_a_custodia)

                    Dim dCajasRemision = dbmCore.Schemadbo.CTA_Items_Remision_Impresion.DBFindById_Remision_Caja(_id_Remision)
                    For Each cajas In dCajasRemision
                        'Actualiza los estados de folders y files
                        'If cajas.fk_Estado = DBCore.EstadoEnum.Asignado_a_Remision Then
                        'Folder
                        Utilities.ActualizaEstadoFolder(dbmArchiving, dbmCore, cajas.CBarras_Folder, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Enviado_a_custodia, Program.Sesion.Usuario.id, cajas.fk_OT)
                        'File
                        Utilities.ActualizaEstadoFile(dbmArchiving, dbmCore, cajas.CBarras_File, cajas.fk_OT, Nothing, DesktopConfig.Modulo.Archiving, DBCore.EstadoEnum.Enviado_a_custodia, Program.Sesion.Usuario.id, Nothing)
                        'End If
                    Next
                End If

                Dim dtLineasEnviadas = dbmArchiving.SchemaRisk.CTA_LineasProceso_Remision.DBFindByfk_Remision_Caja(_id_Remision)
                Dim linea As Integer = 0
                For Each LineaProceso As DataRow In dtLineasEnviadas.Rows

                    Dim LineaRegistro = New DBArchiving.SchemaRisk.TBL_Linea_ProcesoType
                    LineaRegistro.fk_Estado = DBCore.EstadoEnum.Empaque
                    LineaRegistro.fk_Sede_Proceso = Program.DesktopGlobal.BovedaRow.fk_Sede

                    dbmArchiving.SchemaRisk.TBL_Linea_Proceso.DBUpdate(LineaRegistro, CType(CInt(LineaProceso.Table.Rows(linea).ItemArray(1)), Slyg.Tools.SlygNullable(Of Integer)))
                    linea += 1
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarReporte", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
            End Try
        End Sub
#End Region

    End Class

End Namespace