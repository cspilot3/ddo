Imports System.Text
Imports Miharu
Imports DBCore

Partial Public Class consulta
    Inherits Imaging.FormBase

#Region " Declaraciones "

    Private Const MyPathPermiso As String = "1"

    Private BusquedaDataTable As SchemaImaging.CTA_BusquedaDataTable
    Private BusquedaDocumentoDataTable As SchemaImaging.CTA_Busqueda_DocumentoDataTable
    Private BusquedaDataDataTable As DataTable 'SchemaImaging.CTA_Busqueda_DataDataTable
    Private DataDataTable As DataTable

    Private CampoBusquedaDataTable As SchemaConfig.TBL_Campo_BusquedaDataTable

#End Region

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

        If Not Me.IsPostBack Then
            Config_Page()
            ActivarOpciones()
        Else
            Load_Data()
        End If
    End Sub

    Private Sub ResultadosDataGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ResultadosDataGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim RowBusqueda As SchemaImaging.CTA_BusquedaRow = CType(CType(e.Row.DataItem, DataRowView).Row, SchemaImaging.CTA_BusquedaRow)

            Dim imgTipo As Image = CType(e.Row.Cells(0).FindControl("imgTipo"), Image)

            If RowBusqueda.Es_Campo_Folder Then
                imgTipo.ImageUrl = "~/_images/basic/folder.png"
            Else
                imgTipo.ImageUrl = "~/_images/basic/page.png"
            End If
        End If
    End Sub

    Private Sub ResultadosDataGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ResultadosDataGridView.SelectedIndexChanged
        ShowDocumentos()
    End Sub

    Private Sub TipologiasDataGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles TipologiasDataGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim RowBusquedaDocumento As SchemaImaging.CTA_Busqueda_DocumentoRow = CType(CType(e.Row.DataItem, DataRowView).Row, SchemaImaging.CTA_Busqueda_DocumentoRow)
            Dim imgLock As Image = CType(e.Row.Cells(2).FindControl("imgLock"), Image)

            If RowBusquedaDocumento.IsVerDataNull Then RowBusquedaDocumento.VerData = True

            imgLock.Visible = Not RowBusquedaDocumento.VerData And Not MySesion.Usuario.isRoot
        End If
    End Sub

    Private Sub TipologiasDataGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TipologiasDataGridView.SelectedIndexChanged
        ShowData()
    End Sub

    Private Sub DataDataGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataDataGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim RowData As DataRow = CType(e.Row.DataItem, DataRowView).Row

            If CShort(RowData.Item("id_Estado")) <= EstadoEnum.Indexacion Then
                e.Row.Cells(2).Controls(0).Visible = False
            End If

            If EsImagen(CStr(RowData.Item("id_Content_Type"))) Then
                If CBool(RowData.Item("VerImagen")) Or MySesion.Usuario.isRoot Then
                    e.Row.Cells(3).Controls(0).Visible = False
                End If
            Else
                If CBool(RowData.Item("Descargar")) Or MySesion.Usuario.isRoot Then
                    e.Row.Cells(3).Controls(0).Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub DataDataGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataDataGridView.SelectedIndexChanged
        ShowFile()
    End Sub

    Private Sub ddlCampo_1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCampo_1.SelectedIndexChanged
        ShowOperadores(ddlCampo_1, ddlOperador_1)
    End Sub

    Private Sub ddlCampo_2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCampo_2.SelectedIndexChanged
        ShowOperadores(ddlCampo_2, ddlOperador_2)
    End Sub

    Private Sub icbBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibBuscar.Click
        Buscar()
    End Sub

#End Region

#Region " Metodos "

    Private Sub Config_Page()
        BusquedaDataTable = New SchemaImaging.CTA_BusquedaDataTable
        Me.MySesion.Pagina.Parameter("BusquedaDataTable") = BusquedaDataTable

        BusquedaDocumentoDataTable = New SchemaImaging.CTA_Busqueda_DocumentoDataTable
        Me.MySesion.Pagina.Parameter("BusquedaDocumentoDataTable") = BusquedaDocumentoDataTable

        BusquedaDataDataTable = New DataTable ' SchemaImaging.CTA_Busqueda_DataDataTable
        Me.MySesion.Pagina.Parameter("BusquedaDataDataTable") = BusquedaDataDataTable

        DataDataTable = New DataTable
        Me.MySesion.Pagina.Parameter("DataDataTable") = DataDataTable

        CampoBusquedaDataTable = New SchemaConfig.TBL_Campo_BusquedaDataTable
        Me.MySesion.Pagina.Parameter("CampoBusquedaDataTable") = CampoBusquedaDataTable

        Dim dbmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

        Try
            dbmCore.Connection_Open(MySesion.Usuario.id)

            ShowCampos(dbmCore)
            ShowOperadores(ddlCampo_1, ddlOperador_1)
            ShowOperadores(ddlCampo_2, ddlOperador_2)

        Catch ex As Exception
            Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
        Finally
            dbmCore.Connection_Close()
        End Try
    End Sub

    Private Sub Load_Data()
        BusquedaDataTable = CType(Me.MySesion.Pagina.Parameter("BusquedaDataTable"), SchemaImaging.CTA_BusquedaDataTable)
        BusquedaDocumentoDataTable = CType(Me.MySesion.Pagina.Parameter("BusquedaDocumentoDataTable"), SchemaImaging.CTA_Busqueda_DocumentoDataTable)
        BusquedaDataDataTable = CType(Me.MySesion.Pagina.Parameter("BusquedaDataDataTable"), DataTable) 'SchemaImaging.CTA_Busqueda_DataDataTable)
        DataDataTable = CType(Me.MySesion.Pagina.Parameter("DataDataTable"), DataTable)

        CampoBusquedaDataTable = CType(Me.MySesion.Pagina.Parameter("CampoBusquedaDataTable"), SchemaConfig.TBL_Campo_BusquedaDataTable)
    End Sub

    Private Sub Buscar()
        If Validar() Then
            BusquedaDataTable.Clear()

            ResultadosDataGridView.SelectedIndex = -1

            Dim dbmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                Dim SQL As String = getConsulta()

                dbmCore.Connection_Open(MySesion.Usuario.id)

                BusquedaDataTable = dbmCore.SchemaImaging.PA_Busqueda_get.DBExecute(SQL)

                If BusquedaDataTable.Rows.Count > 99 Then
                    Master.ShowAlert("Se encontraron mas de 100 registros coincidentes, por estabilidad del sistema solo se mostraran los 100 primeros", MiharuMasterForm.MsgBoxIcon.IconWarning)
                ElseIf BusquedaDataTable.Rows.Count = 0 Then
                    Master.ShowAlert("No se encontraron registros coincidentes", MiharuMasterForm.MsgBoxIcon.IconInformation)
                End If

                ResultadosDataGridView.DataSource = BusquedaDataTable
                ResultadosDataGridView.DataBind()

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmCore.Connection_Close()
            End Try

            ResultadosDataGridView.SelectedIndex = -1
            ResultadosDataGridView.DataSource = BusquedaDataTable
            ResultadosDataGridView.DataBind()

            ShowDocumentos()
        Else
            ActivarOpciones()
        End If

        ' Almacenar en cache
        Me.MySesion.Pagina.Parameter("BusquedaDataTable") = BusquedaDataTable
    End Sub

    Private Sub ActivarOpciones()
        pnlResultadosMarco.Style("Display") = CStr(IIf(BusquedaDataTable.Rows.Count > 0, "inline", "none"))
        pnlTipologiasMarco.Style("Display") = CStr(IIf(pnlResultadosMarco.Visible And BusquedaDocumentoDataTable.Rows.Count > 0, "inline", "none"))
        pnlDataMarco.Style("Display") = CStr(IIf(pnlTipologiasMarco.Visible And BusquedaDataDataTable.Rows.Count > 0, "inline", "none"))
    End Sub

    Private Sub ShowCampos(ByRef nDBMCore As DBCoreDataBaseManager)
        ddlCampo_1.Items.Clear()
        ddlCampo_2.Items.Clear()

        If MySesion.Usuario.isRoot Then
            BusquedaDataTable.Clear()
            CampoBusquedaDataTable = nDBMCore.SchemaConfig.TBL_Campo_Busqueda.DBGet(Nothing, Nothing)
        Else
            CampoBusquedaDataTable = nDBMCore.SchemaConfig.PA_Campo_Busqueda_find_Usuario.DBExecute(MySesion.Usuario.id)
        End If
        For Each RowCampo As SchemaConfig.TBL_Campo_BusquedaRow In CampoBusquedaDataTable.Rows
            ddlCampo_1.Items.Add(New ListItem(RowCampo.Nombre_Campo_Busqueda, RowCampo.fk_Campo_Tipo & ";" & RowCampo.id_Campo_Busqueda))
            ddlCampo_2.Items.Add(New ListItem(RowCampo.Nombre_Campo_Busqueda, RowCampo.fk_Campo_Tipo & ";" & RowCampo.id_Campo_Busqueda))
        Next
    End Sub

    Private Sub ShowOperadores(ByRef nCampo As DropDownList, ByRef nOperador As DropDownList)
        nOperador.Items.Clear()

        If nCampo.SelectedIndex >= 0 Then
            Dim Partes As String() = nCampo.SelectedValue.Split(";"c)

            Select Case Partes(0)
                Case "1" 'Texto
                    nOperador.Items.Add(New ListItem("=", "="))
                    nOperador.Items.Add(New ListItem("<>", "<>"))

                Case "2", "3" 'Número, Fecha
                    nOperador.Items.Add(New ListItem("=", "="))
                    nOperador.Items.Add(New ListItem("<>", "<>"))
                    nOperador.Items.Add(New ListItem("<", "<"))
                    nOperador.Items.Add(New ListItem(">", ">"))
                    nOperador.Items.Add(New ListItem("<=", "<="))
                    nOperador.Items.Add(New ListItem(">=", ">="))

            End Select
        End If
    End Sub

    Private Sub ShowDocumentos()
        BusquedaDocumentoDataTable.Clear()

        If ResultadosDataGridView.SelectedIndex >= 0 Then
            Dim RowBusqueda As SchemaImaging.CTA_BusquedaRow = BusquedaDataTable(ResultadosDataGridView.SelectedRow.DataItemIndex)
            Dim dbmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

            Try
                dbmCore.Connection_Open(MySesion.Usuario.id)

                If RowBusqueda.Es_Campo_Folder Then
                    BusquedaDocumentoDataTable = dbmCore.SchemaImaging.PA_Busqueda_Documento_get.DBExecute(MySesion.Usuario.id, RowBusqueda.fk_Expediente, RowBusqueda.fk_Folder, Nothing, Nothing)
                Else
                    BusquedaDocumentoDataTable = dbmCore.SchemaImaging.PA_Busqueda_Documento_get.DBExecute(MySesion.Usuario.id, RowBusqueda.fk_Expediente, RowBusqueda.fk_Folder, RowBusqueda.id_File, RowBusqueda.id_Version)
                End If

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmCore.Connection_Close()
            End Try

        End If

        TipologiasDataGridView.SelectedIndex = -1
        TipologiasDataGridView.DataSource = BusquedaDocumentoDataTable
        TipologiasDataGridView.DataBind()

        ShowData()

        Me.MySesion.Pagina.Parameter("BusquedaDocumentoDataTable") = BusquedaDocumentoDataTable
    End Sub

    Private Sub ShowData()
        BusquedaDataDataTable.Clear()
        DataDataTable.Clear()

        DataDataGridView.Columns.Clear()

        If TipologiasDataGridView.SelectedIndex >= 0 Then
            Dim RowBusqueda As SchemaImaging.CTA_BusquedaRow = CType(BusquedaDataTable.Rows(ResultadosDataGridView.SelectedRow.DataItemIndex), SchemaImaging.CTA_BusquedaRow)
            Dim RowBusquedaDocumento As SchemaImaging.CTA_Busqueda_DocumentoRow = CType(BusquedaDocumentoDataTable.Rows(TipologiasDataGridView.SelectedRow.DataItemIndex), SchemaImaging.CTA_Busqueda_DocumentoRow)

            If RowBusquedaDocumento.VerData Or MySesion.Usuario.isRoot Then
                Dim dbmCore As New DBCoreDataBaseManager(MyBase.ConnectionString.Core)

                Try
                    dbmCore.Connection_Open(MySesion.Usuario.id)

                    Dim CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBGet(RowBusquedaDocumento.id_Documento, Nothing)

                    If RowBusqueda.Es_Campo_Folder Then
                        BusquedaDataDataTable = dbmCore.SchemaImaging.PA_Busqueda_Data_get.DBExecute(MySesion.Usuario.id, RowBusquedaDocumento.fk_Expediente, RowBusquedaDocumento.fk_Folder, RowBusquedaDocumento.id_Documento, Nothing, Nothing)
                    Else
                        BusquedaDataDataTable = dbmCore.SchemaImaging.PA_Busqueda_Data_get.DBExecute(MySesion.Usuario.id, RowBusquedaDocumento.fk_Expediente, RowBusquedaDocumento.fk_Folder, RowBusquedaDocumento.id_Documento, RowBusqueda.id_File, RowBusqueda.id_Version)
                    End If

                    CrearData(dbmCore, CamposDataTable)
                    CrearColumnasData(CamposDataTable)

                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmCore.Connection_Close()
                End Try
            End If
        End If

        ActivarOpciones()

        DataDataGridView.SelectedIndex = -1
        DataDataGridView.DataSource = DataDataTable
        DataDataGridView.DataBind()

        Me.MySesion.Pagina.Parameter("BusquedaDataDataTable") = BusquedaDataDataTable
    End Sub

    Private Sub CrearData(ByRef nDBMCore As DBCoreDataBaseManager, ByRef nCampos As SchemaConfig.TBL_CampoDataTable)
        Dim FileDataDataTable As New SchemaProcess.TBL_File_DataDataTable
        Dim Col As Integer = 0

        DataDataTable.Columns.Clear()

        DataDataTable.Columns.Add("fk_Expediente", GetType(Long))
        DataDataTable.Columns.Add("fk_Folder", GetType(Short))
        DataDataTable.Columns.Add("fk_File", GetType(Short))
        DataDataTable.Columns.Add("id_Version", GetType(Short))
        DataDataTable.Columns.Add("File_Unique_Identifier", GetType(Guid))
        DataDataTable.Columns.Add("Folios_Documento_File", GetType(String))
        DataDataTable.Columns.Add("Tamaño_Imagen_File", GetType(String))
        DataDataTable.Columns.Add("Icon_URL_Content_Type", GetType(String))
        DataDataTable.Columns.Add("LockedIcon_URL", GetType(String))
        DataDataTable.Columns.Add("VerImagen", GetType(Boolean))
        DataDataTable.Columns.Add("Descargar", GetType(Boolean))
        DataDataTable.Columns.Add("id_Estado", GetType(Short))
        DataDataTable.Columns.Add("Nombre_Estado", GetType(String))
        DataDataTable.Columns.Add("id_Content_Type", GetType(String))

        For Col = 1 To nCampos.Rows.Count
            DataDataTable.Columns.Add("Col_" & Col, GetType(String))
        Next

        For Each RowBusquedaData As DataRow In BusquedaDataDataTable.Rows
            Dim RowData = DataDataTable.NewRow

            RowData.Item("fk_Expediente") = RowBusquedaData("fk_Folder")
            RowData.Item("fk_Folder") = RowBusquedaData("fk_Folder")
            RowData.Item("fk_File") = RowBusquedaData("fk_File")
            RowData.Item("id_Version") = RowBusquedaData("id_Version")
            RowData.Item("File_Unique_Identifier") = RowBusquedaData("File_Unique_Identifier")
            RowData.Item("Folios_Documento_File") = Format(RowBusquedaData("Folios_Documento_File"), "#,###")
            RowData.Item("Tamaño_Imagen_File") = Format(CLng(RowBusquedaData("Tamaño_Imagen_File")) / 1024, "#,##0.00")
            RowData.Item("Icon_URL_Content_Type") = RowBusquedaData("Icon_URL_Content_Type")
            RowData.Item("LockedIcon_URL") = "~/_images/basic/lock.png"
            RowData.Item("id_Estado") = RowBusquedaData("id_Estado")
            RowData.Item("Nombre_Estado") = RowBusquedaData("Nombre_Estado")
            RowData.Item("id_Content_Type") = RowBusquedaData("id_Content_Type")

            If RowBusquedaData.IsNull("VerImagen") Then
                RowData.Item("VerImagen") = MySesion.Usuario.isRoot
            Else
                RowData.Item("VerImagen") = RowBusquedaData("VerImagen")
            End If

            If RowBusquedaData.IsNull("Descargar") Then
                RowData.Item("Descargar") = MySesion.Usuario.isRoot
            Else
                RowData.Item("Descargar") = RowBusquedaData("Descargar")
            End If

            FileDataDataTable = nDBMCore.SchemaProcess.PA_File_Data_get.DBExecute(CLng(RowBusquedaData("fk_Expediente")), CShort(RowBusquedaData("fk_Folder")), CShort(RowBusquedaData("fk_File")), Nothing, Nothing)

            Col = 0
            For Each RowCampo As SchemaConfig.TBL_CampoRow In nCampos
                Dim FileDataRow As SchemaProcess.TBL_File_DataRow = Nothing

                Col += 1

                FileDataDataTable.DefaultView.RowFilter = "fk_Documento = " & RowBusquedaData("fk_Documento").ToString() & " AND fk_Campo = " & RowCampo.id_Campo

                If (FileDataDataTable.DefaultView.Count > 0) Then
                    FileDataRow = CType(FileDataDataTable.DefaultView.Item(0).Row, SchemaProcess.TBL_File_DataRow)
                End If

                If FileDataRow Is Nothing Then
                    RowData.Item("Col_" & Col) = ""
                ElseIf FileDataRow.IsValor_File_DataNull Then
                    RowData.Item("Col_" & Col) = ""
                Else
                    RowData.Item("Col_" & Col) = FileDataRow.Valor_File_Data.ToString()
                End If
            Next

            DataDataTable.Rows.Add(RowData)
        Next
    End Sub

    Private Sub CrearColumnasData(ByRef nCampos As SchemaConfig.TBL_CampoDataTable)
        Dim NewBoundField As BoundField
        Dim NewImageField As ImageField

        NewBoundField = New BoundField
        NewBoundField.DataField = "fk_File"
        NewBoundField.HeaderText = "Cod."
        DataDataGridView.Columns.Add(NewBoundField)

        NewBoundField = New BoundField
        NewBoundField.DataField = "id_Version"
        NewBoundField.HeaderText = "V"
        DataDataGridView.Columns.Add(NewBoundField)

        NewImageField = New ImageField
        NewImageField.ItemStyle.Width = New Unit(1, UnitType.Pixel)
        NewImageField.DataImageUrlField = "Icon_URL_Content_Type"
        NewImageField.HeaderText = ""
        DataDataGridView.Columns.Add(NewImageField)

        NewImageField = New ImageField
        NewImageField.ItemStyle.Width = New Unit(1, UnitType.Pixel)
        NewImageField.DataImageUrlField = "LockedIcon_URL"
        NewImageField.HeaderText = ""
        DataDataGridView.Columns.Add(NewImageField)

        NewBoundField = New BoundField
        NewBoundField.DataField = "Nombre_Estado"
        NewBoundField.HeaderText = "Estado"
        DataDataGridView.Columns.Add(NewBoundField)

        NewBoundField = New BoundField
        NewBoundField.DataField = "Folios_Documento_File"
        NewBoundField.HeaderText = "Folios"
        DataDataGridView.Columns.Add(NewBoundField)

        NewBoundField = New BoundField
        NewBoundField.DataField = "Tamaño_Imagen_File"
        NewBoundField.HeaderText = "Tamaño (KB)"
        DataDataGridView.Columns.Add(NewBoundField)

        Dim Col As Integer = 0

        For Each RowCampo As SchemaConfig.TBL_CampoRow In nCampos
            Col += 1

            NewBoundField = New BoundField
            NewBoundField.DataField = "Col_" & Col
            NewBoundField.HeaderText = RowCampo.Nombre_Campo
            DataDataGridView.Columns.Add(NewBoundField)
        Next
    End Sub

    Private Sub ShowFile()
        If DataDataGridView.SelectedRow.DataItemIndex >= 0 Then
            Dim RowData = DataDataTable.Rows(DataDataGridView.SelectedRow.DataItemIndex)

            If CByte(RowData.Item("id_Estado")) > EstadoEnum.Indexacion Then ' Publicado, En transferencia
                If EsImagen(CStr(RowData.Item("id_Content_Type"))) Then
                    If CBool(RowData.Item("VerImagen")) Then
                        Master.ShowWindow(Program.VisorPageURL & "?Token=" & RowData.Item("File_Unique_Identifier").ToString(), "Adjunto", "980", "650", , , , , , , , )
                    End If
                Else
                    If CBool(RowData.Item("Descargar")) Then
                        Master.ShowWindow(Program.DescargarPageURL & "?Token=" & RowData.Item("File_Unique_Identifier").ToString(), "Adjunto", "300", "200", , , , , , , , )
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region " Funciones "

    Private Function Validar() As Boolean
        ' Debe existir almenos un parametro de consulta
        If txtParametro_1.Text = "" And txtParametro_2.Text = "" Then
            Master.ShowAlert("Debe ingresar almenos un parámetro de búsqueda", MiharuMasterForm.MsgBoxIcon.IconError)
            txtParametro_1.Focus()
            Return False
        End If

        ' Campos de búsqueda
        If Not Validar_Parametro(txtParametro_1, ddlCampo_1) Then Return False
        If Not Validar_Parametro(txtParametro_2, ddlCampo_2) Then Return False

        Return True
    End Function

    Private Function Validar_Parametro(ByRef nParametro As TextBox, ByRef nCampo As DropDownList) As Boolean
        If nParametro.Text <> "" Then
            If nCampo.SelectedIndex < 0 Then
                Master.ShowAlert("Debe seleccionar el campo de búsqueda", MiharuMasterForm.MsgBoxIcon.IconError)
                nCampo.Focus()
                Return False
            Else
                Dim Partes As String() = nCampo.SelectedValue.Split(";"c)

                Select Case Partes(0)
                    Case "1" ' texto
                        If nParametro.Text.Contains("'") Or nParametro.Text.Contains(";") Or nParametro.Text.Contains("--") Then
                            Master.ShowAlert("No se permite lo caracteres ['], [;] y [--] en el campo de búsqueda", MiharuMasterForm.MsgBoxIcon.IconError)
                            nParametro.Focus()
                            Return False
                        End If

                    Case "2" 'Número
                        If Not IsNumeric(nParametro.Text) Then
                            Master.ShowAlert("El parámetro de búsqueda debe ser un valor numérico", MiharuMasterForm.MsgBoxIcon.IconError)
                            nParametro.Focus()
                            Return False
                        End If

                    Case "3" 'Fecha
                        If Not IsDate(nParametro.Text) Then
                            Master.ShowAlert("El parámetro de búsqueda debe ser una fecha válida", MiharuMasterForm.MsgBoxIcon.IconError)
                            nParametro.Focus()
                            Return False
                        End If
                End Select
            End If
        End If

        Return True
    End Function

    Private Function Validar_Fecha(ByRef nCampo As TextBox) As Boolean
        If nCampo.Text <> "" Then
            If Not IsDate(nCampo.Text) Then
                Master.ShowAlert("El valor debe ser una fecha válida", MiharuMasterForm.MsgBoxIcon.IconError)
                nCampo.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function getConsulta() As String
        Dim TipoCampo_1 As Byte = 0
        Dim CampoBusqueda_1 As Short = 0
        Dim OperadorCampo_1 As String = ""
        Dim Campo_1 As Object = Nothing
        Dim Valor_Campo_1 As String = ""

        If txtParametro_1.Text <> "" Then
            Dim Partes As String() = ddlCampo_1.SelectedValue.Split(";"c)

            TipoCampo_1 = CByte(Partes(0))
            CampoBusqueda_1 = CShort(Partes(1))

            OperadorCampo_1 = ddlOperador_1.SelectedValue

            Select Case TipoCampo_1
                Case 1
                    If OperadorCampo_1 = "=" Then OperadorCampo_1 = "="
                    Valor_Campo_1 = "CONVERT(VARCHAR(MAX), B.Valor_File_Data) "
                    Campo_1 = "'" & txtParametro_1.Text & "'"
                Case 2
                    'Valor_Campo_1 = "[Process].Fn_getNumero(B.Valor_File_Data) "
                    Valor_Campo_1 = "CAST(B.Valor_File_Data AS VARCHAR)"
                    'Campo_1 = "CONVERT(REAL, '" & txtParametro_1.Text & "')"
                    Campo_1 = "CONVERT(VARCHAR, '" & txtParametro_1.Text & "')"
                Case 3
                    Valor_Campo_1 = "[Process].Fn_getFecha(B.Valor_File_Data) "
                    Campo_1 = "CONVERT(DATETIME, '" & Format(CDate(txtParametro_1.Text), "yyyy-MM-dd") & "', 102)"

            End Select
        End If

        Dim TipoCampo_2 As Byte = 0
        Dim CampoBusqueda_2 As Short = 0
        Dim OperadorCampo_2 As String = ""
        Dim Campo_2 As Object = Nothing
        Dim Valor_Campo_2 As String = ""

        If txtParametro_2.Text <> "" Then
            Dim Partes As String() = ddlCampo_2.SelectedValue.Split(";"c)

            TipoCampo_2 = CByte(Partes(0))
            CampoBusqueda_2 = CShort(Partes(1))

            OperadorCampo_2 = ddlOperador_2.SelectedValue

            Select Case TipoCampo_2
                Case 1
                    If OperadorCampo_2 = "=" Then OperadorCampo_1 = "LIKE"
                    Valor_Campo_2 = "CONVERT(VARCHAR(MAX), Valor_File_Data) "
                    Campo_2 = "'" & txtParametro_2.Text & "'"
                Case 2
                    'Valor_Campo_2 = "[Process].Fn_getNumero(Valor_File_Data) "
                    Valor_Campo_2 = " CAST(Valor_File_Data AS VARCHAR) "
                    'Campo_2 = "CONVERT(REAL, '" & txtParametro_2.Text & "')"
                    Campo_2 = "CONVERT(VARCHAR, '" & txtParametro_2.Text & "')"
                Case 3
                    Valor_Campo_2 = "[Process].Fn_getFecha(Valor_File_Data) "
                    Campo_2 = "CONVERT(DATETIME, '" & Format(CDate(txtParametro_2.Text), "yyyy-MM-dd") & " 23:59:00', 102)"
            End Select
        End If

        'Dim OperadorLogico As String = CStr(IIf(rbAnd.Checked, " AND ", " OR "))
        Dim SQL As New StringBuilder("")

        SQL.Append("SELECT TOP 100")

        SQL.Append(vbCrLf & "B.id_Entidad, B.Nombre_Entidad, B.id_Proyecto, B.Nombre_Proyecto, B.id_Esquema, B.Nombre_Esquema, B.id_Documento, B.Nombre_Documento, ")
        SQL.Append(vbCrLf & "B.fk_Expediente, B.fk_Folder, B.id_File, B.id_Version, B.File_Unique_Identifier, '' AS Valor_File_Data, 1 AS fk_Campo_Busqueda, 0 AS Es_Campo_Folder, B.Data_1, ")
        SQL.Append(vbCrLf & "B.Data_2, B.Data_3, B.fk_Campo_Tipo")

        SQL.Append(vbCrLf & "FROM Imaging.CTA_Busqueda AS B")

        If Not MySesion.Usuario.isRoot Then
            SQL.Append(vbCrLf & "INNER JOIN Process.CTA_Usuario_Permisos AS UP ON")
            SQL.Append(vbCrLf & "B.id_Entidad = UP.fk_Entidad_Rol AND")
            SQL.Append(vbCrLf & "B.id_Proyecto = UP.fk_Proyecto AND")
            SQL.Append(vbCrLf & "B.id_Esquema = UP.fk_Esquema AND")
            SQL.Append(vbCrLf & "(B.id_Documento = UP.fk_Documento OR B.Es_Campo_Folder = 1)")
        End If

        SQL.Append(vbCrLf & "WHERE")

        If Not MySesion.Usuario.isRoot Then
            SQL.Append(vbCrLf & "(UP.fk_Usuario = " & MySesion.Usuario.id & ") AND")
        End If

        If Not Campo_1 Is Nothing Then
            If Campo_2 Is Nothing Then
                SQL.Append(vbCrLf & "((fk_Campo_Tipo = " & TipoCampo_1 & ") AND (B.fk_Campo_Busqueda = " & CampoBusqueda_1 & ") AND (" & Valor_Campo_1 & OperadorCampo_1 & " " & CStr(Campo_1) & "))")
            Else
                SQL.Append(vbCrLf & "(((fk_Campo_Tipo = " & TipoCampo_1 & ") AND (B.fk_Campo_Busqueda = " & CampoBusqueda_1 & ") AND (" & Valor_Campo_1 & OperadorCampo_1 & " " & CStr(Campo_1) & ")) OR") ' & OperadorLogico)
                SQL.Append(vbCrLf & "((fk_Campo_Tipo = " & TipoCampo_2 & ") AND (B.fk_Campo_Busqueda = " & CampoBusqueda_2 & ") AND (" & Valor_Campo_2 & OperadorCampo_2 & " " & CStr(Campo_2) & ")))")
            End If
        ElseIf Not Campo_2 Is Nothing Then
            SQL.Append(vbCrLf & "((fk_Campo_Tipo = " & TipoCampo_2 & ") AND (B.fk_Campo_Busqueda = " & CampoBusqueda_2 & ") AND (" & Valor_Campo_2 & OperadorCampo_2 & " " & CStr(Campo_2) & "))")
        End If


        SQL.Append(vbCrLf & "GROUP BY")

        SQL.Append(vbCrLf & "B.id_Entidad, B.Nombre_Entidad, B.id_Proyecto, B.Nombre_Proyecto, B.id_Esquema, B.Nombre_Esquema, B.id_Documento, B.Nombre_Documento, ")
        SQL.Append(vbCrLf & "B.fk_Expediente, B.fk_Folder, B.id_File, B.id_Version, B.File_Unique_Identifier, B.Data_1, B.Data_2, B.Data_3, B.fk_Campo_Tipo")


        If (rbAnd.Checked And Not Campo_1 Is Nothing And Not Campo_2 Is Nothing) Then
            SQL.Append(vbCrLf & "HAVING COUNT(*) > 1")
        End If

        SQL.Append(vbCrLf & "ORDER BY B.Nombre_Entidad, B.Nombre_Proyecto, B.Nombre_Esquema DESC")

        Return SQL.ToString()
    End Function

    Private Function EsImagen(ByVal nExtencion As String) As Boolean
        Select Case UCase(nExtencion)
            Case ".TIF", ".JPG", ".GIF", ".BMP", ".PNG" ', ".PDF"
                Return True

            Case Else
                Return False

        End Select
    End Function

#End Region

End Class


