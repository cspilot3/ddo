Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library

Namespace Procesos.Configuracion.Imaging

    Public Class FormAgregarEmpaqueContenedorCampo
        Inherits FormBase

#Region " Declaraciones "

        Private CampoLista As Short = 0

#End Region

#Region "Eventos"

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub FormAgregarContenedorCampo_Load(sender As System.Object, e As EventArgs)


        End Sub

#End Region

#Region "Funciones"

        Private Sub GuardarCambios()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                If Nombre_Campo_TextBox.Text = "" Then Throw New Exception("El Nombre del Campo es Requerido")

                Dim contenedorCampo As New DBImaging.SchemaConfig.TBL_Empaque_Contenedor_CampoType()
                With contenedorCampo
                    .fk_Entidad = Program.ImagingGlobal.Entidad
                    .fk_Proyecto = Program.ImagingGlobal.Proyecto
                    .Nombre_Campo = Nombre_Campo_TextBox.Text
                    .Eliminado = EliminadoCheckBox.Checked
                    .fk_Usuario_Log = Program.Sesion.Usuario.id
                    .Fecha_Log = Date.Now
                End With

                If (id_CampoTextBox.Text = "") Then
                    'contenedorCampo.id_Campo = dbmImaging.SchemaConfig.TBL_Empaque_Contenedor_Campo.DBNextId(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    contenedorCampo.id_Campo = dbmImaging.SchemaConfig.TBL_Empaque_Contenedor_Campo.DBNextId_for_id_Campo(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    dbmImaging.SchemaConfig.TBL_Empaque_Contenedor_Campo.DBInsert(contenedorCampo)
                Else
                    dbmImaging.SchemaConfig.TBL_Empaque_Contenedor_Campo.DBUpdate(contenedorCampo, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(id_CampoTextBox.Text, Short))
                End If

                dbmImaging.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                Me.Close()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

            End Try
        End Sub

#End Region

    End Class

End Namespace