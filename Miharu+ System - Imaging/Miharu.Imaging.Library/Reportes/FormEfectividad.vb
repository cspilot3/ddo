Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes

    Public Class FormEfectividad
        Dim TieneOficina As Boolean = False
        Dim porMeses As Boolean = False


#Region " Declaraciones "

        Private _TipoReporte As String

#End Region

#Region " Propiedades "

        Public Property FechaInicial() As Date
            Get
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property
        Public Property FechaFinal() As Date
            Get
                Return dtpFechaFinal.Value.Date.AddDays(1).AddSeconds(-1)
            End Get
            Set(ByVal value As Date)
                dtpFechaFinal.Value = value
            End Set
        End Property

        Public Property Sede() As Slyg.Tools.SlygNullable(Of Integer)
            Get
                If (cbxSede.Text = "") Then
                    Return DBNull.Value
                Else
                    Return CInt(cbxSede.SelectedValue.ToString())
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of Integer))
                cbxSede.SelectedValue = value.Value.ToString
            End Set

        End Property

        Public Property Oficina() As Slyg.Tools.SlygNullable(Of String)
            Get
                If (dntOFicina.Text = "") Then
                    Return DBNull.Value
                Else
                    Return dntOFicina.SelectedValue.ToString()
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of String))
                dntOFicina.SelectedValue = value.Value.ToString
            End Set

        End Property

        Public Property ColaDocumental() As Slyg.Tools.SlygNullable(Of Integer)
            Get
                If (cmbColaDocumental.Text = "") Then
                    Return DBNull.Value
                Else
                    Return CInt(cmbColaDocumental.SelectedValue.ToString())
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of Integer))
                cmbColaDocumental.SelectedValue = value.Value.ToString
            End Set

        End Property

        Public Property BancoEpago() As Slyg.Tools.SlygNullable(Of Integer)

            'TODO: Cambiar cuando se corrija lo detipo de oficina (Banco o Epago)
            Get
                If (cmbBancoEpago.Text = "") Then
                    Return DBNull.Value
                Else
                    'Return CInt(cmbBancoEpago.SelectedValue.ToString())
                    Return -1
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of Integer))
                cmbBancoEpago.SelectedValue = value.Value.ToString
            End Set
        End Property

#End Region

#Region " Constructor "

        Public Sub New(ByVal nTipoReporte As String)
            InitializeComponent()
            _TipoReporte = nTipoReporte
        End Sub

#End Region

#Region " Eventos "

        Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            If Not Validar() Then
                Exit Sub
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub FormRangoFechas_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim MesesConsulta As String = ConsultarParametroSistema(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, "NumeroMesesConsulta")
            If MesesConsulta = "" Then
                MesesConsulta = "3"
            End If
            If MesesConsulta Is Nothing Or Not IsNumeric(MesesConsulta) Then
                MesesConsulta = "3"
            End If
            MesesConsulta = CStr(CInt(MesesConsulta) - 1)

            CargarColaDocumental()

            If Program.DesktopGlobal.CentroProcesamientoRow.fk_TipoOficina = 1 Then
                'TODO
                'Cuando se ajuste lo de las oficinas y/o sedes del banco corregir esto
                Dim bancoEpagoDataTable As New DataTable
                bancoEpagoDataTable.Columns.Add("id")
                bancoEpagoDataTable.Columns.Add("Nombre")

                Dim BancoEpagoRow = bancoEpagoDataTable.NewRow()
                BancoEpagoRow("id") = -1
                BancoEpagoRow("Nombre") = "- TODOS -"
                bancoEpagoDataTable.Rows.Add(BancoEpagoRow)


                Dim BancoEpagoRow1 = bancoEpagoDataTable.NewRow()
                BancoEpagoRow1("id") = 1
                BancoEpagoRow1("Nombre") = "BANCO"
                bancoEpagoDataTable.Rows.Add(BancoEpagoRow1)


                Dim BancoEpagoRow2 = bancoEpagoDataTable.NewRow()
                BancoEpagoRow2("id") = 2
                BancoEpagoRow2("Nombre") = "EPAGO"
                bancoEpagoDataTable.Rows.Add(BancoEpagoRow2)

                bancoEpagoDataTable.AcceptChanges()

                cmbBancoEpago.DisplayMember = "Nombre"
                cmbBancoEpago.ValueMember = "id"

                Utilities.LlenarCombo(cmbBancoEpago, bancoEpagoDataTable, "id", "Nombre")
                cmbBancoEpago.SelectedItem = -1
                cmbBancoEpago.Enabled = False
                'Dim opciones() As String = {"- TODOS -", "Banco", "Epago"}
                'cmbBancoEpago.Items.AddRange(opciones)
                'cmbBancoEpago.SelectedIndex = 0
                'TODO

                Dim sedeDataTable As New DataTable
                sedeDataTable.Columns.Add("id_Sede")
                sedeDataTable.Columns.Add("Nombre_Sede")

                Dim SedeRow = sedeDataTable.NewRow()
                SedeRow("id_Sede") = Program.DesktopGlobal.CentroProcesamientoRow.id_Sede
                SedeRow("Nombre_Sede") = Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Sede

                sedeDataTable.Rows.Add(SedeRow)
                sedeDataTable.AcceptChanges()

                cbxSede.DisplayMember = "Nombre_Sede"
                cbxSede.ValueMember = "id_Sede"

                Utilities.LlenarCombo(cbxSede, sedeDataTable, DBSecurity.SchemaConfig.TBL_SedeEnum.id_Sede.ColumnName, DBSecurity.SchemaConfig.TBL_SedeEnum.Nombre_Sede.ColumnName)

                cbxSede.SelectedIndex = 0
                cbxSede.Enabled = False

                Dim oficinaDataTable As New DataTable
                oficinaDataTable.Columns.Add("Codigo_Centro")
                oficinaDataTable.Columns.Add("Nombre_Centro_Procesamiento")

                Dim oficinaRow = oficinaDataTable.NewRow()
                oficinaRow("Codigo_Centro") = Program.DesktopGlobal.CentroProcesamientoRow.Codigo_Centro
                oficinaRow("Nombre_Centro_Procesamiento") = Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento

                oficinaDataTable.Rows.Add(oficinaRow)
                oficinaDataTable.AcceptChanges()

                dntOFicina.DisplayMember = "Nombre_Centro_Procesamiento"
                dntOFicina.ValueMember = "Codigo_Centro"

                Utilities.LlenarCombo(dntOFicina, oficinaDataTable, "Codigo_Centro", "Nombre_Centro_Procesamiento")
                dntOFicina.Refresh()

                dntOFicina.SelectedIndex = 0
                dntOFicina.Enabled = False
            Else
                CargarBancoEpago()
            End If
        End Sub


#End Region

#Region " Funciones "

        Private Function Validar() As Boolean

            If dtpFechaInicial.Value > dtpFechaFinal.Value Then
                MessageBox.Show("La Fecha Final debe ser superior a la Fecha Inicial", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFechaFinal.Focus()
                Return False
            End If

            'El rango de fechas no puede ser superior a 31 dias
            If DateDiff(DateInterval.Day, dtpFechaInicial.Value, dtpFechaFinal.Value) > 31 Then
                MessageBox.Show("El rango entre Fecha Inicial y Fecha Final no puede ser superior a 31 días", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFechaFinal.Focus()
                Return False
            End If

            Return True

        End Function

        Public Function ConsultarParametroSistema(fk_Entidad As Integer, fk_Proyecto As Integer, Nombre_Parametro_Sistema As String) As String
            Try
                Dim queryResponseCampoInactividad As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Consulta_Parametro_Sistema_Get]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = CInt(fk_Entidad).ToString()},
                                          New QueryParameter With {.name = "fk_Proyecto", .value = CInt(fk_Proyecto).ToString()},
                                          New QueryParameter With {.name = "Nombre_Parametro_Sistema", .value = Trim(Nombre_Parametro_Sistema)}
                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                Return Trim(CStr(queryResponseCampoInactividad.dataTable.Rows(0).Item("Valor_Parametro_Sistema")))
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
#End Region

#Region " Metodos "
        Private Sub CargarBancoEpago()
            Dim opciones() As String = {"- TODOS -", "Banco", "Epago"}
            cmbBancoEpago.Items.AddRange(opciones)
            cmbBancoEpago.SelectedIndex = 0
            CargarSede()
        End Sub
        Private Sub CargarSede()

            Dim sedeDataTable As DBSecurity.SchemaConfig.TBL_SedeDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.TBL_Sede", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

            'TODO
            'Crear vista para contemplar solo las sedes que sean de tipo banco o epago
            'no se hace hoy 20251107 porque aún no se sabe ni se ha definido cuales son de cuales o si es por sede o por oficina

            sedeDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.TBL_SedeDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.TBL_SedeDataTable)

            cbxSede.DisplayMember = "Nombre_Sede"
            cbxSede.ValueMember = "id_Sede"

            Utilities.Llenarcombo(cbxSede, sedeDataTable, DBSecurity.SchemaConfig.TBL_SedeEnum.id_Sede.ColumnName, DBSecurity.SchemaConfig.TBL_SedeEnum.Nombre_Sede.ColumnName, True, "-1", "--TODOS--")

            cbxSede.Refresh()

            CargarOficinas()
        End Sub

        Private Sub CargarOficinas()
            Dim oficinaDataTable As DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.PA_CentrosProcesamiento_ComboBox", New List(Of QueryParameter) From {
                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                    New QueryParameter With {.name = "fk_Sede", .value = cbxSede.SelectedValue.ToString()}
                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
            oficinaDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable)

            Utilities.Llenarcombo(dntOFicina, oficinaDataTable, "Codigo_Centro", "Nombre_Centro_Procesamiento", True, "-1", "- TODOS -")
            dntOFicina.Refresh()
        End Sub

        Private Sub CargarColaDocumental()
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Esquema_Lista_Get]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = CInt(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad).ToString()},
                                          New QueryParameter With {.name = "fk_Proyecto", .value = CInt(Program.ImagingGlobal.Proyecto).ToString()}
                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)


            Utilities.Llenarcombo(cmbColaDocumental, queryResponseListaItems.dataTable, "id_Esquema", "Nombre_Esquema", True, "-1", "- Todos -")
            cmbColaDocumental.Refresh()
        End Sub

        Private Sub cmbBancoEpago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBancoEpago.SelectedIndexChanged
            CargarSede()
        End Sub

        Private Sub cbxSede_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSede.SelectedIndexChanged
            CargarOficinas()
        End Sub
#End Region

    End Class

End Namespace