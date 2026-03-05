Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Procesos.Configuracion.Imaging

    Public Class FormAgregarPrecintoCampo
        Inherits FormBase

#Region " Declaraciones "

        Private CampoLista As Short = 0

#End Region

#Region "Eventos"

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub FormAgregarPrecintoCampo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
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

#Region " Funciones "

        Private Function Validar() As Boolean
            Dim Mensaje As String = ""

            If Longitud_Campo_TextBox.Text = "" Then Longitud_Campo_TextBox.Text = "0"
            If Longitud_Minima_TextBox.Text = "" Then Longitud_Minima_TextBox.Text = "0"
            If Cantidad_Decimales_TextBox.Text = "" Then Cantidad_Decimales_TextBox.Text = "0"
            If Nombre_Campo_TextBox.Text = "" Then Mensaje = "El Nombre del Campo es Requerido"
            If Not IsNumeric(Longitud_Campo_TextBox.Text) Then Mensaje = "El Campo Longitud no es numerico"
            If Not IsNumeric(Longitud_Minima_TextBox.Text) Then Mensaje = "El valor longitud minima no es numerico"
            If Not IsNumeric(Cantidad_Decimales_TextBox.Text) Then Mensaje = "El campo cantidad decimales no es numerico"
            If Campo_Tipo_ComboBox.SelectedValue.ToString = "-1" Then Mensaje = "Debe seleccionar un Tipo de Campo"

            If Mensaje <> "" Then
                DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & Mensaje, "Problemas en Campo Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Return False
            End If

            Return True
        End Function

#End Region

#Region " Metodos "

        Private Sub GuardarCambios()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If Validar() Then

                    dbmImaging.Transaction_Begin()

                    Dim campoListaId As Slyg.Tools.SlygNullable(Of Short) = DBNull.Value
                    If CInt(Campo_Lista_ComboBox.SelectedValue) <> -1 Then campoListaId = CShort(Campo_Lista_ComboBox.SelectedValue)

                    Dim precintoCampo As New DBImaging.SchemaConfig.TBL_Precinto_CampoType()
                    With precintoCampo
                        .fk_Entidad = Program.ImagingGlobal.Entidad
                        .fk_Proyecto = Program.ImagingGlobal.Proyecto
                        .Nombre_Campo = Nombre_Campo_TextBox.Text
                        .fk_Campo_Tipo = CByte(Campo_Tipo_ComboBox.SelectedValue)
                        .fk_Campo_Lista = campoListaId
                        .Es_Obligatorio_Campo = Obligatorio_CheckBox.Checked
                        .Length_Campo = CShort(Longitud_Campo_TextBox.Text)
                        .Length_Min_Campo = CShort(Longitud_Minima_TextBox.Text)
                        .Usa_Decimales = Usa_Decimales_CheckBox.Checked
                        .Cantidad_Decimales = CShort(Cantidad_Decimales_TextBox.Text)
                        .Eliminado = EliminadoCheckBox.Checked
                        .fk_Usuario_Log = Program.Sesion.Usuario.id
                        .Fecha_Log = Date.Now
                    End With

                    If (id_CampoTextBox.Text = "") Then
                        precintoCampo.id_Campo = dbmImaging.SchemaConfig.TBL_Precinto_Campo.DBNextId(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                        dbmImaging.SchemaConfig.TBL_Precinto_Campo.DBInsert(precintoCampo)
                    Else
                        dbmImaging.SchemaConfig.TBL_Precinto_Campo.DBUpdate(precintoCampo, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CShort(id_CampoTextBox.Text))
                    End If

                    dbmImaging.Transaction_Commit()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()

                    DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Me.Close()

                End If

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
