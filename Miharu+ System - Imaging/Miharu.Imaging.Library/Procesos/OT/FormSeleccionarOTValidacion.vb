Namespace Procesos.OT

    Public Class FormSeleccionarOTValidacion
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

                Return dbmImaging.SchemaProcess.PA_Fecha_Proceso_get_Validaciones.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)

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

                Return dbmImaging.SchemaProcess.PA_OT_get_Validaciones.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, CInt(FechaProcesoComboBox.SelectedValue))
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