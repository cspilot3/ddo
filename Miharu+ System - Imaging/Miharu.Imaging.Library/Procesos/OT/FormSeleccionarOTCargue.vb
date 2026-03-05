Namespace Procesos.OT

    Public Class FormSeleccionarOTCargue
        Inherits FormSeleccionarOT

#Region " Constructores "

        Public Sub New()
            MyBase.New()

        End Sub

#End Region

#Region " Implementacion FormSeleccionarOT "

        Protected Overrides Function getFechas() As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_ProyectoCerradofk_Entidad_Procesamiento(Program.ImagingGlobal.Entidad, _
                                                                                                                               Program.ImagingGlobal.Proyecto, _
                                                                                                                               False, _
                                                                                                                               Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable()
        End Function

        Protected Overrides Function getOTs() As DBImaging.SchemaProcess.CTA_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.CTA_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_procesofk_Sede_Procesamientofk_Centro_ProcesamientoCerrado(Program.DesktopGlobal.PuestoTrabajoRow.fk_Entidad, _
                                                                                                                                                                                Program.ImagingGlobal.Entidad, _
                                                                                                                                                                                Program.ImagingGlobal.Proyecto, _
                                                                                                                                                                                CInt(FechaProcesoComboBox.SelectedValue), _
                                                                                                                                                                                Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede, _
                                                                                                                                                                                Program.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento, _
                                                                                                                                                                                False)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_OTDataTable()
        End Function

#End Region

    End Class

End Namespace