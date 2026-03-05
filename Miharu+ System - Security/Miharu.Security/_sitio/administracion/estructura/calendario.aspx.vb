Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Public Class calendario
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.11"

        Private BaseDataTable As DBSecurity.SchemaConfig.TBL_CalendarioDataTable
        Private EntidadDataTable As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private CalendarioDataTable As DBSecurity.SchemaConfig.TBL_CalendarioDataTable
        Private HorarioDataTable As DBSecurity.SchemaConfig.TBL_Calendario_HorarioDataTable
        Private YearDataTable As DBSecurity.SchemaSecurity.CTA_YearDataTable
        Private FestivoDataTable As DBSecurity.SchemaConfig.TBL_FestivoDataTable
        'Private FestivoDataTable As DataTable

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
                ActivarOpciones(False, False)
            Else
                Load_Data()
            End If
        End Sub

        Private Sub ddlEntidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntidad.SelectedIndexChanged
            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text
            Buscar(ucFiltro.Parametro)
        End Sub
        Private Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlYear.SelectedIndexChanged
            If ddlYear.SelectedValue <> "" Then
                BuscarFestivos(CShort(ddlYear.SelectedValue))
            Else
                FestivoDataTable.Clear()
                Visualizar_Festivos()
            End If
        End Sub

        Private Sub gvBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvBase.SelectedIndexChanged
            If gvBase.SelectedIndex >= 0 And gvBase.Rows.Count > 0 Then
                EditarRegistro()
            End If
        End Sub

        Private Sub ibAdd_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibAdd.Click
            NuevoRegistro()
        End Sub
        Private Sub ibDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibDelete.Click
            EliminarRegistro()
        End Sub
        Private Sub ibSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibSave.Click
            GuardarCambios()
        End Sub

        Private Sub ucFiltro_Click(ByVal nParametro As String) Handles ucFiltro.Click
            Buscar(nParametro)
        End Sub

        Private Sub gvFestivo_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles gvFestivo.RowDeleting
            DeleteFestivo(CShort(e.RowIndex))
        End Sub

        Protected Sub btnAddFecha_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles btnAddFecha.Click
            AddFestivo()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            BaseDataTable = New DBSecurity.SchemaConfig.TBL_CalendarioDataTable
            Me.MySesion.Pagina.Parameter("BaseDataTable") = BaseDataTable

            EntidadDataTable = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("EntidadDataTable") = EntidadDataTable

            CalendarioDataTable = New DBSecurity.SchemaConfig.TBL_CalendarioDataTable
            Me.MySesion.Pagina.Parameter("CalendarioDataTable") = CalendarioDataTable

            HorarioDataTable = New DBSecurity.SchemaConfig.TBL_Calendario_HorarioDataTable
            Me.MySesion.Pagina.Parameter("HorarioDataTable") = HorarioDataTable

            YearDataTable = New DBSecurity.SchemaSecurity.CTA_YearDataTable
            Me.MySesion.Pagina.Parameter("YearDataTable") = YearDataTable

            FestivoDataTable = New DBSecurity.SchemaConfig.TBL_FestivoDataTable
            Me.MySesion.Pagina.Parameter("FestivoDataTable") = FestivoDataTable

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                ' Entidades
                EntidadDataTable.Clear()
                dbmSecurity.SchemaConfig.TBL_Entidad.DBFill(EntidadDataTable, Nothing)

                ' Años
                YearDataTable = dbmSecurity.SchemaSecurity.CTA_Year.DBGet()

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowYears()
            ShowEntidades()

            If ddlYear.SelectedValue <> "" Then
                BuscarFestivos(CShort(ddlYear.SelectedValue))
            Else
                FestivoDataTable.Rows.Clear()
                Visualizar_Festivos()
            End If
        End Sub
        Private Sub Load_Data()
            BaseDataTable = CType(Me.MySesion.Pagina.Parameter("BaseDataTable"), DBSecurity.SchemaConfig.TBL_CalendarioDataTable)
            EntidadDataTable = CType(Me.MySesion.Pagina.Parameter("EntidadDataTable"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            CalendarioDataTable = CType(Me.MySesion.Pagina.Parameter("CalendarioDataTable"), DBSecurity.SchemaConfig.TBL_CalendarioDataTable)
            HorarioDataTable = CType(Me.MySesion.Pagina.Parameter("HorarioDataTable"), DBSecurity.SchemaConfig.TBL_Calendario_HorarioDataTable)
            YearDataTable = CType(Me.MySesion.Pagina.Parameter("YearDataTable"), DBSecurity.SchemaSecurity.CTA_YearDataTable)
            FestivoDataTable = CType(Me.MySesion.Pagina.Parameter("FestivoDataTable"), DBSecurity.SchemaConfig.TBL_FestivoDataTable)
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" And ddlEntidad.SelectedIndex >= 0 Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    BaseDataTable.Clear()
                    dbmSecurity.SchemaConfig.TBL_Calendario.DBFillByfk_Entidadid_CalendarioNombre_Calendario(BaseDataTable, CShort(ddlEntidad.SelectedValue), Nothing, nParametro)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            Else
                BaseDataTable.Rows.Clear()
            End If

            ActivarOpciones(False, False)
            Visualizar_Resultados()
            pnlDetalle.Visible = False
        End Sub
        Private Sub BuscarFestivos(ByVal nYear As Short)
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                FestivoDataTable = dbmSecurity.SchemaConfig.PA_Basic_TBL_Festivo_find_Year.DBExecute(nYear)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            Visualizar_Festivos()

        End Sub
        Private Sub Visualizar_Resultados()
            gvBase.DataSource = BaseDataTable
            gvBase.DataBind()
        End Sub
        Private Sub Visualizar_Festivos()
            gvFestivo.DataSource = FestivoDataTable
            gvFestivo.DataBind()
        End Sub
        Private Sub ClearForm()
            CalendarioDataTable.Rows.Clear()
            HorarioDataTable.Rows.Clear()

            lblNombreEntidad.Text = ddlEntidad.SelectedItem.Text

            lblCodCalendario.Text = "-1"
            txtNombre.Text = ""
        End Sub

        Private Sub NuevoRegistro()
            Dim RowHorario As DBSecurity.SchemaConfig.TBL_Calendario_HorarioRow
            Dim i As Byte

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            For i = 0 To 23
                RowHorario = HorarioDataTable.NewTBL_Calendario_HorarioRow

                RowHorario.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                RowHorario.fk_Calendario = CShort(lblCodCalendario.Text)
                RowHorario.id_Calendario_Horario = CByte((i + 1))
                RowHorario.Hora = i

                If i < 8 Then
                    RowHorario.Domingo = False
                    RowHorario.Lunes = False
                    RowHorario.Martes = False
                    RowHorario.Miercoles = False
                    RowHorario.Jueves = False
                    RowHorario.Viernes = False
                    RowHorario.Sabado = False
                    RowHorario.Festivo = False
                ElseIf i < 13 Then
                    RowHorario.Domingo = False
                    RowHorario.Lunes = True
                    RowHorario.Martes = True
                    RowHorario.Miercoles = True
                    RowHorario.Jueves = True
                    RowHorario.Viernes = True
                    RowHorario.Sabado = False
                    RowHorario.Festivo = False
                ElseIf i < 14 Then
                    RowHorario.Domingo = False
                    RowHorario.Lunes = False
                    RowHorario.Martes = False
                    RowHorario.Miercoles = False
                    RowHorario.Jueves = False
                    RowHorario.Viernes = False
                    RowHorario.Sabado = False
                    RowHorario.Festivo = False
                ElseIf i < 18 Then
                    RowHorario.Domingo = False
                    RowHorario.Lunes = True
                    RowHorario.Martes = True
                    RowHorario.Miercoles = True
                    RowHorario.Jueves = True
                    RowHorario.Viernes = True
                    RowHorario.Sabado = False
                    RowHorario.Festivo = False
                Else
                    RowHorario.Domingo = False
                    RowHorario.Lunes = False
                    RowHorario.Martes = False
                    RowHorario.Miercoles = False
                    RowHorario.Jueves = False
                    RowHorario.Viernes = False
                    RowHorario.Sabado = False
                    RowHorario.Festivo = False
                End If

                HorarioDataTable.Rows.Add(RowHorario)

            Next

            ShowHorario()

            txtNombre.Focus()
            Me.ActivarOpciones(True, True)
        End Sub
        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_CalendarioRow

            ClearForm()
            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            txtNombre.Focus()
            Me.ActivarOpciones(True, False)

            RowBase = CType(BaseDataTable.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_CalendarioRow)

            ' Data
            lblCodCalendario.Text = CStr(RowBase.id_Calendario)
            txtNombre.Text = RowBase.Nombre_Calendario

            CalendarioDataTable.Rows.Clear()
            CalendarioDataTable.Rows.Add(RowBase.ItemArray)
            CalendarioDataTable.AcceptChanges()

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                HorarioDataTable.Clear()
                dbmSecurity.SchemaConfig.TBL_Calendario_Horario.DBFill(HorarioDataTable, RowBase.fk_Entidad, RowBase.id_Calendario, Nothing)
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            ShowHorario()

        End Sub
        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                Dim RowCalendario As DBSecurity.SchemaConfig.TBL_CalendarioRow
                Dim isNuevo As Boolean = False

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    If lblCodCalendario.Text = "-1" Then
                        isNuevo = True
                        lblCodCalendario.Text = CStr(dbmSecurity.SchemaConfig.TBL_Calendario.DBNextId_for_id_Calendario(CShort(ddlEntidad.SelectedValue)))

                        RowCalendario = CalendarioDataTable.NewTBL_CalendarioRow

                        RowCalendario.fk_Entidad = CShort(ddlEntidad.SelectedValue)
                        RowCalendario.id_Calendario = CShort(lblCodCalendario.Text)
                    Else
                        RowCalendario = CalendarioDataTable.FindByfk_Entidadid_Calendario(CShort(ddlEntidad.SelectedValue), CShort(lblCodCalendario.Text))
                    End If

                    RowCalendario.Nombre_Calendario = txtNombre.Text

                    If isNuevo Then
                        CalendarioDataTable.Rows.Add(RowCalendario)
                    End If

                    UpdateHorario()

                    dbmSecurity.SchemaConfig.TBL_Calendario.DBSaveTable(CalendarioDataTable)
                    'dbmSecurity.SchemaConfig.TBL_Calendario_Horario.DBDelete(RowCalendario.fk_Entidad, RowCalendario.id_Calendario, Nothing)
                    dbmSecurity.SchemaConfig.TBL_Calendario_Horario.DBSaveTable(HorarioDataTable)

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    Buscar(ucFiltro.Parametro)

                    ActivarOpciones(True, False)
                    pnlDetalle.Visible = True
                    tcBase.ActiveTabIndex = 1

                Catch ex As Exception
                    dbmSecurity.Transaction_Rollback()
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                    If isNuevo Then
                        lblCodCalendario.Text = "-1"
                        CalendarioDataTable.RejectChanges()
                    End If
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub
        Private Sub EliminarRegistro()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowCalendario As DBSecurity.SchemaConfig.TBL_CalendarioRow

            RowCalendario = CalendarioDataTable.FindByfk_Entidadid_Calendario(CShort(ddlEntidad.SelectedValue), CShort(lblCodCalendario.Text))
            RowCalendario.Delete()

            HorarioDataTable.Rows.Clear()

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.SchemaConfig.TBL_Calendario.DBSaveTable(CalendarioDataTable)

                tcBase.ActiveTabIndex = 0
                Buscar(ucFiltro.Parametro)

                Me.Master.ShowAlert("El registro se eliminó exitosamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                ClearForm()
                tcBase.ActiveTabIndex = 0

                pnlDetalle.Visible = False
                Me.ActivarOpciones(False, False)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub
        Private Sub ActivarOpciones(ByVal nActivo As Boolean, ByVal nIsNew As Boolean)
            ibSave.Visible = nActivo
            divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))

            ibDelete.Visible = nActivo And Not nIsNew
            divDelete.Style("display") = CStr(IIf(ibDelete.Visible, "inline", "none"))
        End Sub

        Private Sub UpdateHorario()
            Dim RowHorario As DBSecurity.SchemaConfig.TBL_Calendario_HorarioRow

            For Each Item As GridViewRow In gvHorario.Rows
                RowHorario = CType(HorarioDataTable.Rows(Item.DataItemIndex), DBSecurity.SchemaConfig.TBL_Calendario_HorarioRow)

                RowHorario.fk_Calendario = CShort(lblCodCalendario.Text)
                RowHorario.Domingo = CType(Item.Cells(1).FindControl("chkDomingo"), CheckBox).Checked
                RowHorario.Lunes = CType(Item.Cells(2).FindControl("chkLunes"), CheckBox).Checked
                RowHorario.Martes = CType(Item.Cells(3).FindControl("chkMartes"), CheckBox).Checked
                RowHorario.Miercoles = CType(Item.Cells(4).FindControl("chkMiercoles"), CheckBox).Checked
                RowHorario.Jueves = CType(Item.Cells(5).FindControl("chkJueves"), CheckBox).Checked
                RowHorario.Viernes = CType(Item.Cells(6).FindControl("chkViernes"), CheckBox).Checked
                RowHorario.Sabado = CType(Item.Cells(7).FindControl("chkSabado"), CheckBox).Checked
                RowHorario.Festivo = CType(Item.Cells(8).FindControl("chkFestivo"), CheckBox).Checked

            Next
        End Sub
        Private Sub ShowHorario()
            gvHorario.DataSource = HorarioDataTable
            gvHorario.DataBind()

            Dim RowHorario As DBSecurity.SchemaConfig.TBL_Calendario_HorarioRow

            For Each Item As GridViewRow In gvHorario.Rows
                RowHorario = CType(HorarioDataTable.Rows(Item.DataItemIndex), DBSecurity.SchemaConfig.TBL_Calendario_HorarioRow)

                CType(Item.Cells(0).FindControl("lblHora"), Label).Text = Format(RowHorario.Hora, "00") & ":00"
                CType(Item.Cells(1).FindControl("chkDomingo"), CheckBox).Checked = RowHorario.Domingo
                CType(Item.Cells(2).FindControl("chkLunes"), CheckBox).Checked = RowHorario.Lunes
                CType(Item.Cells(3).FindControl("chkMartes"), CheckBox).Checked = RowHorario.Martes
                CType(Item.Cells(4).FindControl("chkMiercoles"), CheckBox).Checked = RowHorario.Miercoles
                CType(Item.Cells(5).FindControl("chkJueves"), CheckBox).Checked = RowHorario.Jueves
                CType(Item.Cells(6).FindControl("chkViernes"), CheckBox).Checked = RowHorario.Viernes
                CType(Item.Cells(7).FindControl("chkSabado"), CheckBox).Checked = RowHorario.Sabado
                CType(Item.Cells(8).FindControl("chkFestivo"), CheckBox).Checked = RowHorario.Festivo
            Next
        End Sub

        Private Sub ShowEntidades()
            ddlEntidad.DataSource = EntidadDataTable
            ddlEntidad.DataValueField = "id_Entidad"
            ddlEntidad.DataTextField = "Nombre_Entidad"
            ddlEntidad.DataBind()

            If Not MySesion.Usuario.PerfilManager.PuedeAcceder("3.2.1") Then ' Entidades
                ddlEntidad.SelectedValue = CStr(MySesion.Entidad.id)
                ddlEntidad.Enabled = False
            End If
        End Sub
        Private Sub ShowYears()
            ddlYear.DataSource = YearDataTable
            ddlYear.DataValueField = "Year"
            ddlYear.DataTextField = "Year"
            ddlYear.DataBind()
        End Sub

        Private Sub AddFestivo()
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                If dbmSecurity.SchemaConfig.PA_Basic_TBL_Festivo_existe.DBExecute(CDate(txtFecha.Text)).Rows(0)("Existe") Is "" Then
                    Dim PartesFecha() As String = txtFecha.Text.Split("-/.".ToCharArray)
                    Dim Festivo As New Date(CInt(PartesFecha(2)), CInt(PartesFecha(1)), CInt(PartesFecha(0)))

                    dbmSecurity.SchemaConfig.TBL_Festivo.DBInsert(Festivo)

                    YearDataTable = CType(Me.MySesion.Pagina.Parameter("YearDataTable"), DBSecurity.SchemaSecurity.CTA_YearDataTable)
                    ShowYears()

                    ddlYear.SelectedValue = Format(CDate(txtFecha.Text), "yyyy")
                    BuscarFestivos(CShort(ddlYear.SelectedValue))
                Else
                    Master.ShowAlert("La fecha " & txtFecha.Text & ", ya se encuentra creada", MiharuMasterForm.MsgBoxIcon.IconWarning)
                End If
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
        End Sub
        Private Sub DeleteFestivo(ByVal nIndex As Short)
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
            Dim RowFecha As DBSecurity.SchemaConfig.TBL_FestivoRow
            Dim Year As String = ddlYear.SelectedValue
            If ddlYear.SelectedValue <> "" Then
                BuscarFestivos(CShort(ddlYear.SelectedValue))
            End If
            RowFecha = CType(FestivoDataTable.Rows(nIndex), DBSecurity.SchemaConfig.TBL_FestivoRow)
            RowFecha.Delete()

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                dbmSecurity.SchemaConfig.TBL_Festivo.DBSaveTable(FestivoDataTable)

                YearDataTable = CType(Me.MySesion.Pagina.Parameter("YearDataTable"), DBSecurity.SchemaSecurity.CTA_YearDataTable)
                ShowYears()

                Try : ddlYear.SelectedValue = Year : Catch : End Try

                If ddlYear.SelectedValue <> "" Then
                    BuscarFestivos(CShort(ddlYear.SelectedValue))
                Else
                    FestivoDataTable.Rows.Clear()
                    Visualizar_Festivos()
                End If

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace