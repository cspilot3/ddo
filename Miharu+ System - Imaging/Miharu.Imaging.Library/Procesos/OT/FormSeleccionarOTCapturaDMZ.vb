Imports QueryResponse = Miharu.Desktop.Library.MiharuDMZ.QueryResponse
Imports ClientUtil = Miharu.Desktop.Library.MiharuDMZ.ClientUtil
Imports QueryParameter = Miharu.Desktop.Library.MiharuDMZ.QueryParameter
Imports QueryResponseType = Miharu.Desktop.Library.MiharuDMZ.QueryResponseType
Imports QueryRequestType = Miharu.Desktop.Library.MiharuDMZ.QueryRequestType

Namespace Procesos.OT

    Public Class FormSeleccionarOTCapturaDMZ
        Inherits FormSeleccionarOT

#Region " Declaraciones "

        Private _Estado As DBCore.EstadoEnum

#End Region

#Region " Constructores "

        Public Sub New(nEstado As DBCore.EstadoEnum)
            MyBase.New()

            _Estado = nEstado
        End Sub

#End Region

#Region " Implementacion FormSeleccionarOT "

        Protected Overrides Function getFechas() As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
            If Program.Sesion.IsExternal Then
                Dim dtFechaProceso As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable = Nothing
                Dim queryResponseFechaProceso As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_Fecha_Proceso_get_Captura]", New List(Of QueryParameter) From {
                                     New QueryParameter With {.name = "id_Entidad", .value = Convert.ToInt16(Program.ImagingGlobal.Entidad).ToString()},
                                     New QueryParameter With {.name = "id_Proyecto", .value = Convert.ToInt16(Program.ImagingGlobal.Proyecto).ToString()},
                                     New QueryParameter With {.name = "id_Entidad_Procesamiento", .value = Convert.ToInt16(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad).ToString()},
                                     New QueryParameter With {.name = "id_Sede_Procesamiento", .value = Convert.ToInt16(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede).ToString()},
                                     New QueryParameter With {.name = "id_Centro_Procesamiento", .value = Convert.ToInt16(Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento).ToString()},
                                     New QueryParameter With {.name = "id_Estado", .value = Convert.ToInt16(_Estado).ToString()}
                                 }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
                dtFechaProceso = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable(), queryResponseFechaProceso.dataTable), DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable)
                Return dtFechaProceso
            Else
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Return dbmImaging.SchemaProcess.PA_Fecha_Proceso_get_Captura.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, _Estado)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            Return New DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable()
        End Function

        Protected Overrides Function getOTs() As DBImaging.SchemaProcess.CTA_OTDataTable

            If Program.Sesion.IsExternal Then
                'Return dbmImaging.SchemaProcess.PA_OT_get_Captura.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, CInt(FechaProcesoComboBox.SelectedValue), _Estado)
                Dim dtOT As DBImaging.SchemaProcess.CTA_OTDataTable = Nothing
                Dim queryResponsedtOT As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].[Process].[PA_OT_get_Captura]", New List(Of QueryParameter) From {
                                     New QueryParameter With {.name = "id_Entidad", .value = Convert.ToInt16(Program.ImagingGlobal.Entidad).ToString()},
                                     New QueryParameter With {.name = "id_Proyecto", .value = Convert.ToInt16(Program.ImagingGlobal.Proyecto).ToString()},
                                     New QueryParameter With {.name = "id_Entidad_Procesamiento", .value = Convert.ToInt16(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad).ToString()},
                                     New QueryParameter With {.name = "id_Sede_Procesamiento", .value = Convert.ToInt16(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede).ToString()},
                                     New QueryParameter With {.name = "id_Centro_Procesamiento", .value = Convert.ToInt16(Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento).ToString()},
                                     New QueryParameter With {.name = "id_Fecha_Proceso", .value = CInt(FechaProcesoComboBox.SelectedValue).ToString()},
                                     New QueryParameter With {.name = "id_Estado", .value = Convert.ToInt16(_Estado).ToString()}
                                 }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
                dtOT = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_OTDataTable(), queryResponsedtOT.dataTable), DBImaging.SchemaProcess.CTA_OTDataTable)
                Return dtOT
            Else
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Return dbmImaging.SchemaProcess.PA_OT_get_Captura.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, CInt(FechaProcesoComboBox.SelectedValue), _Estado)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            Return New DBImaging.SchemaProcess.CTA_OTDataTable()
        End Function

#End Region

    End Class

End Namespace