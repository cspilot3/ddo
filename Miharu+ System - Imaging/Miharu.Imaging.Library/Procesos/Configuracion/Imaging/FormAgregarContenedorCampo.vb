Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library

Namespace Procesos.Configuracion.Imaging

    Public Class FormAgregarContenedorCampo
        Inherits FormBase

#Region " Declaraciones "

        Private CampoLista As Short = 0

#End Region

#Region "Eventos"

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub FormAgregarContenedorCampo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarListas()

            If (CampoListaTextBox.Text = "") Then
                CampoLista = -1
            Else
                CampoLista = CShort(CampoListaTextBox.Text)
            End If

            If (CampoTipoTextBox.Text <> "") Then
                Campo_Lista_ComboBox.SelectedValue = CampoLista
                Campo_Tipo_ComboBox.SelectedValue = CampoTipoTextBox.Text
            End If
        End Sub

#End Region

#Region "Funciones"

        Private Sub GuardarCambios()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim longitud As Short
                Dim longitudMinima As Short
                Dim cantidadDecimal As Short
                Dim orden As Short
                Dim campoListaId As Slyg.Tools.SlygNullable(Of Short)

                If Nombre_Campo_TextBox.Text = "" Then Throw New Exception("El Nombre del Campo es Requerido")

                Try
                    Select Case Longitud_Campo_TextBox.Text
                        Case ""
                            longitud = 0
                        Case Else
                            longitud = CShort(Longitud_Campo_TextBox.Text)
                    End Select
                Catch
                    Throw New Exception("El Campo Longitud Es Numérico")
                End Try

                Try
                    Select Case Longitud_Minima_TextBox.Text
                        Case ""
                            longitudMinima = 0
                        Case Else
                            longitudMinima = CShort(Longitud_Minima_TextBox.Text)
                    End Select
                Catch
                    Throw New Exception("El Valor Longitud Minima Del Campo Es Numérico")
                End Try

                Try
                    Select Case Cantidad_Decimales_TextBox.Text
                        Case ""
                            cantidadDecimal = 0
                        Case Else
                            cantidadDecimal = CShort(Cantidad_Decimales_TextBox.Text)
                    End Select
                Catch
                    Throw New Exception("El campo Cantidad Decimales Es Numérico")
                End Try

                Try
                    Select Case OrdenCampo_TextBox.Text
                        Case ""
                            orden = 0
                        Case Else
                            orden = CShort(OrdenCampo_TextBox.Text)
                    End Select
                Catch
                    Throw New Exception("El Valor Orden Campo Es Numérico")
                End Try

                Try
                    Select Case CShort(Campo_Tipo_ComboBox.SelectedValue)
                        Case -1
                            Throw New Exception("Debe seleccionar un Tipo de Campo")
                    End Select
                Catch
                    Throw New Exception("Debe seleccionar un Tipo de Campo")
                End Try

                Select Case CShort(Campo_Lista_ComboBox.SelectedValue)
                    Case -1
                        campoListaId = DBNull.Value
                    Case Else
                        campoListaId = CShort(Campo_Lista_ComboBox.SelectedValue)
                End Select

                Dim contenedorCampo As New DBImaging.SchemaConfig.TBL_Contenedor_CampoType()
                With contenedorCampo
                    .fk_Entidad = Program.ImagingGlobal.Entidad
                    .fk_Proyecto = Program.ImagingGlobal.Proyecto
                    .Nombre_Campo = Nombre_Campo_TextBox.Text
                    .fk_Campo_Tipo = CByte(Campo_Tipo_ComboBox.SelectedValue)
                    .fk_Campo_Lista = campoListaId
                    .Es_Obligatorio_Campo = Obligatorio_CheckBox.Checked
                    .Length_Campo = longitud
                    .Length_Min_Campo = longitudMinima
                    .Usa_Decimales = Usa_Decimales_CheckBox.Checked
                    .Cantidad_Decimales = cantidadDecimal
                    .Eliminado = EliminadoCheckBox.Checked
                    .Orden = orden
                    .fk_Usuario_Log = Program.Sesion.Usuario.id
                    .Fecha_Log = Date.Now
                End With

                If (id_CampoTextBox.Text = "") Then
                    contenedorCampo.id_Campo = dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBNextId(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBInsert(contenedorCampo)
                Else
                    dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBUpdate(contenedorCampo, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(id_CampoTextBox.Text, Short))
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

        Private Sub CargarListas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim CampoTipoTable = dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing)
                Dim Campo_Lista = dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(Program.ImagingGlobal.Entidad, Nothing)

                Campo_Tipo_ComboBox.Fill(CampoTipoTable, CampoTipoTable.id_Campo_TipoColumn, CampoTipoTable.Nombre_Campo_TipoColumn, True)
                Campo_Lista_ComboBox.Fill(Campo_Lista, Campo_Lista.id_Campo_ListaColumn, Campo_Lista.Nombre_Campo_ListaColumn, True)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace