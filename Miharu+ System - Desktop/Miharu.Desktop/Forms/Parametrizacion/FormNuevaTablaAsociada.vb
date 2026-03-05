Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Slyg.Tools

Namespace Forms.Parametrizacion

    Public Class FormNuevaTablaAsociada
#Region " Propiedades "

        Public Property Entidad() As Short
        Public Property Documento() As Integer
        Public Property fk_Campo() As Short
        Public Property Id_Campo_Tabla() As Short

#End Region

#Region "Eventos"

        Private Sub FormNuevoCampo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarListas()

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                If (Id_Campo_Tabla <> 0) Then
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim CampoTablaRow = dbmCore.SchemaConfig.CTA_Tabla_Asociada_Parametrizacion.DBFindByfk_Entidadfk_Documentofk_Campoid_Campo_Tabla(Entidad, Documento, fk_Campo, Id_Campo_Tabla)(0)

                    With CampoTablaRow
                        Nombre_Campo_TextBox.Text = .Nombre_Campo
                        Campo_Tipo_ComboBox.SelectedValue = .fk_Campo_Tipo
                        Campo_Lista_ComboBox.Enabled = (.fk_Campo_Tipo = 5) 'Habilita el combo Campo_Lista si es de tipo lista
                        If .fk_Campo_Lista = 0 Then
                            Campo_Lista_ComboBox.SelectedValue = -1
                        Else
                            Campo_Lista_ComboBox.SelectedValue = .fk_Campo_Lista
                        End If
                        Campo_Lista_ComboBox.Enabled = (.fk_Campo_Tipo = 5)
                        If .fk_Campo_Busqueda = 0 Then
                            Campo_Busqueda_ComboBox.SelectedValue = -1
                        Else
                            Campo_Busqueda_ComboBox.SelectedValue = .fk_Campo_Busqueda
                        End If
                        Campo_Busqueda_CheckBox.Checked = .Es_Campo_Busqueda
                        Campo_Busqueda_ComboBox.Enabled = .Es_Campo_Busqueda

                        Longitud_Campo_TextBox.Text = CStr(.Length_Campo)
                        Cantidad_Decimales_TextBox.Text = CStr(.Cantidad_Decimales)
                        Caracter_Decimal_TextBox.Text = .Caracter_Decimal
                        Obligatorio_CheckBox.Checked = .Es_Obligatorio_Campo
                        Exportable_CheckBox.Checked = .Es_Exportable
                        Eliminado_CheckBox.Checked = .Eliminado_Campo
                        Usa_Decimales_CheckBox.Checked = .Usa_Decimales
                    End With
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()

            End Try

            
        End Sub

        Private Sub CargarListas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Campo_TipoData = dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing)
                Dim Campo_ListaData = dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(Entidad, Nothing)

                Campo_Tipo_ComboBox.Fill(Campo_TipoData, Campo_TipoData.id_Campo_TipoColumn, Campo_TipoData.Nombre_Campo_TipoColumn, True)
                Campo_Lista_ComboBox.Fill(Campo_ListaData, Campo_ListaData.id_Campo_ListaColumn, Campo_ListaData.Nombre_Campo_ListaColumn, True)

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                CargarBusqueda()
            End Try
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub Campo_Busqueda_CheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles Campo_Busqueda_CheckBox.CheckedChanged
            If Campo_Busqueda_CheckBox.Checked Then
                CargarBusqueda()
            End If
            Campo_Busqueda_ComboBox.Enabled = Campo_Busqueda_CheckBox.Checked
        End Sub

        Private Sub Campo_Tipo_ComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles Campo_Tipo_ComboBox.SelectedIndexChanged
            Campo_Lista_ComboBox.Enabled = (CInt(Campo_Tipo_ComboBox.SelectedValue) = 5)
        End Sub

#End Region

#Region " Metodos "

        Private Sub GuardarCambios()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                Dim campoListaId As Slyg.Tools.SlygNullable(Of Short)
                Dim campoBusquedaId As Slyg.Tools.SlygNullable(Of Short)

                Try
                    Select Case Convert.ToInt16(Campo_Tipo_ComboBox.SelectedValue)
                        Case -1
                            Throw New Exception("Debe seleccionar un Tipo de Campo")
                    End Select
                Catch ex As Exception
                    Throw New Exception("Debe seleccionar un Tipo de Campo")
                End Try

                If (Longitud_Campo_TextBox.Text = "") Then Longitud_Campo_TextBox.Text = "0"
                If (Cantidad_Decimales_TextBox.Text = "") Then Cantidad_Decimales_TextBox.Text = "0"

                Select Case Convert.ToInt16(Campo_Lista_ComboBox.SelectedValue)
                    Case -1
                        campoListaId = DBNull.Value
                    Case Else
                        campoListaId = CType(Campo_Lista_ComboBox.SelectedValue, Short)
                End Select

                Select Case Convert.ToInt16(Campo_Busqueda_ComboBox.SelectedValue)
                    Case -1
                        campoBusquedaId = DBNull.Value
                    Case Else
                        campoBusquedaId = CType(Campo_Busqueda_ComboBox.SelectedValue, Short)
                End Select

                Dim campocore As New DBCore.SchemaConfig.TBL_Tabla_AsociadaType
                With campocore
                    .fk_Entidad = Entidad
                    .fk_Documento = Documento
                    .fk_Campo = fk_Campo
                    .fk_Campo_Tipo = CByte(Campo_Tipo_ComboBox.SelectedValue)
                    .fk_Campo_Lista = campoListaId
                    .Es_Campo_Busqueda = Campo_Busqueda_CheckBox.Checked
                    .fk_Campo_Busqueda = campoBusquedaId
                    .Nombre_Campo = Nombre_Campo_TextBox.Text
                    .Es_Obligatorio_Campo = Obligatorio_CheckBox.Checked
                    .Length_Campo = CShort(Longitud_Campo_TextBox.Text)
                    .Es_Exportable = Exportable_CheckBox.Checked
                    .Eliminado_Campo = Eliminado_CheckBox.Checked
                    .Usa_Decimales = Usa_Decimales_CheckBox.Checked
                    .Caracter_Decimal = Caracter_Decimal_TextBox.Text
                    .Cantidad_Decimales = CShort(Cantidad_Decimales_TextBox.Text)
                    .Valor_por_Defecto = Valor_Defecto_TextBox.Text
                    .fk_Usuario_Log = Program.Sesion.Usuario.id
                    .Fecha_Log = SlygNullable.SysDate
                End With

                If (Id_Campo_Tabla.ToString = "0") Then
                    campocore.id_Campo_Tabla = dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBNextId(Documento, fk_Campo)
                    dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBInsert(campocore)
                Else
                    dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBUpdate(campocore, Documento, fk_Campo, Id_Campo_Tabla)
                End If

                dbmCore.Transaction_Commit()
            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Tabla Asociada Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Tabla Asociada Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.DialogResult = DialogResult.OK
            End Try
        End Sub

        Private Sub CargarBusqueda()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Campo_BusquedaData = dbmCore.SchemaConfig.TBL_Campo_Busqueda.DBGet(Nothing, Nothing)
                Campo_Busqueda_ComboBox.Fill(Campo_BusquedaData, Campo_BusquedaData.id_Campo_BusquedaColumn, Campo_BusquedaData.Nombre_Campo_BusquedaColumn, True)

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Tabla Asociada Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace