Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Slyg.Tools

Namespace Forms.Parametrizacion

    Public Class FormNuevoCampo

#Region " Propiedades "

        Public Property Entidad() As Short

        Public Property Documento() As Integer

        Public Property Id_Campo() As Short

        Public Property fk_Campo_Tipo() As Byte

        Public Property fk_Campo_Lista() As Short

        Public Property Es_Campo_Busqueda() As Boolean

        Public Property fk_Campo_Busqueda() As Short

        Public Property Nombre_Campo() As String

        Public Property Es_Campo_Folder() As Boolean

        Public Property Es_Obligatorio_Campo() As Boolean

        Public Property Length_Campo() As Short

        Public Property Length_Min_Campo() As String

        Public Property Es_Exportable() As Boolean

        Public Property Eliminado_Campo() As Boolean

        Public Property Usa_Decimales() As Boolean

        Public Property Caracter_Decimal() As String

        Public Property Body_Query() As String

        Public Property Validar_Registros() As Boolean

        Public Property Validar_Totales() As Boolean

        Public Property Valor_por_Defecto() As String
    
        Public Property Cantidad_Decimales() As Short

        Public Property Campo_Control_Duplicado() As Boolean
            
#End Region

#Region "Eventos"

        Private Sub FormNuevoCampo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarListas()

            If (Id_Campo.ToString() <> "0") Then
                Nombre_Campo_TextBox.Text = Nombre_Campo
                Campo_Tipo_ComboBox.SelectedValue = fk_Campo_Tipo
                Campo_Lista_ComboBox.Enabled = (fk_Campo_Tipo = 5) 'Habilita el combo Campo_Lista si es de tipo lista
                If fk_Campo_Lista = 0 Then
                    Campo_Lista_ComboBox.SelectedValue = -1
                Else
                    Campo_Lista_ComboBox.SelectedValue = fk_Campo_Lista
                End If
                Campo_Lista_ComboBox.Enabled = (fk_Campo_Tipo = 5)
                If fk_Campo_Busqueda = 0 Then
                    Campo_Busqueda_ComboBox.SelectedValue = -1
                Else
                    Campo_Busqueda_ComboBox.SelectedValue = fk_Campo_Busqueda
                End If
                Campo_Busqueda_CheckBox.Checked = Es_Campo_Busqueda
                Campo_Busqueda_ComboBox.Enabled = Es_Campo_Busqueda

                Longitud_Campo_TextBox.Text = CStr(Length_Campo)
                Longitud_Minima_TextBox.Text = Length_Min_Campo
                Cantidad_Decimales_TextBox.Text = CStr(Cantidad_Decimales)
                Caracter_Decimal_TextBox.Text = Caracter_Decimal
                Body_Query_TextBox.Text = Body_Query
                Campo_Folder_CheckBox.Checked = Es_Campo_Folder
                Obligatorio_CheckBox.Checked = Es_Obligatorio_Campo
                Exportable_CheckBox.Checked = Es_Exportable
                Eliminado_CheckBox.Checked = Eliminado_Campo
                Usa_Decimales_CheckBox.Checked = Usa_Decimales
                Validar_Registros_CheckBox.Checked = Validar_Registros
                Validar_Totales_CheckBox.Checked = Validar_Registros
                Valor_Defecto_TextBox.Text = Valor_por_Defecto
                Campo_Control_Duplicado_CheckBox.Checked = Campo_Control_Duplicado
            End If
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
                If (Longitud_Minima_TextBox.Text = "") Then Longitud_Minima_TextBox.Text = "0"
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

                Dim campocore As New DBCore.SchemaConfig.TBL_CampoType
                With campocore
                    .fk_Entidad = Entidad
                    .fk_Documento = Documento
                    .fk_Campo_Tipo = CByte(Campo_Tipo_ComboBox.SelectedValue)
                    .fk_Campo_Lista = campoListaId
                    .Es_Campo_Busqueda = Campo_Busqueda_CheckBox.Checked
                    .fk_Campo_Busqueda = campoBusquedaId
                    .Nombre_Campo = Nombre_Campo_TextBox.Text
                    .Es_Campo_Folder = Campo_Folder_CheckBox.Checked
                    .Es_Obligatorio_Campo = Obligatorio_CheckBox.Checked
                    .Length_Campo = CShort(Longitud_Campo_TextBox.Text)
                    .Length_Min_Campo = CShort(Longitud_Minima_TextBox.Text)
                    .Es_Exportable = Exportable_CheckBox.Checked
                    .Eliminado_Campo = Eliminado_CheckBox.Checked
                    .Usa_Decimales = Usa_Decimales_CheckBox.Checked
                    .Caracter_Decimal = Caracter_Decimal_TextBox.Text
                    .Cantidad_Decimales = CShort(Cantidad_Decimales_TextBox.Text)
                    .Body_Query = Body_Query_TextBox.Text
                    .Validar_Registros = Validar_Registros_CheckBox.Checked
                    .Validar_Totales = Validar_Totales_CheckBox.Checked
                    .Valor_por_Defecto = Valor_Defecto_TextBox.Text
                    .Campo_Control_Duplicado = Campo_Control_Duplicado_CheckBox.Checked
                    .fk_Usuario_Log = Program.Sesion.Usuario.id
                    .Fecha_Log = SlygNullable.SysDate
                End With
                If (Id_Campo.ToString = "0") Then
                    campocore.id_Campo = dbmCore.SchemaConfig.TBL_Campo.DBNextId(Documento)
                    dbmCore.SchemaConfig.TBL_Campo.DBInsert(campocore)
                Else
                    dbmCore.SchemaConfig.TBL_Campo.DBUpdate(campocore, Documento, Id_Campo)
                End If

                dbmCore.Transaction_Commit()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Me.Close()

            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
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
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

        Private Sub Campo_Busqueda_CheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles Campo_Busqueda_CheckBox.CheckedChanged
            If Campo_Busqueda_CheckBox.Checked Then
                CargarBusqueda()
            End If
            Campo_Busqueda_ComboBox.Enabled = Campo_Busqueda_CheckBox.Checked
        End Sub

        Private Sub Campo_Tipo_ComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles Campo_Tipo_ComboBox.SelectedIndexChanged
            Campo_Lista_ComboBox.Enabled = (CInt(Campo_Tipo_ComboBox.SelectedValue) = 5)
        End Sub
    End Class

End Namespace