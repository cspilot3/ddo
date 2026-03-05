Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes

    Public Class FormMes

#Region " Declaraciones "

        Private _TipoReporte As String

#End Region

#Region " Propiedades "

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

        Public Property Estado() As Slyg.Tools.SlygNullable(Of String)
            Get
                If (dntEstado.Text = "") Then
                    Return DBNull.Value
                Else
                    Return CStr(dntEstado.Text)
                End If
            End Get
            Set(ByVal Text As Slyg.Tools.SlygNullable(Of String))
                dntEstado.Text = Text.Value.ToString
            End Set

        End Property

        Public Property Mes() As Slyg.Tools.SlygNullable(Of String)
            Get
                If (cmbMes.Text = "") Then
                    Return DBNull.Value
                Else
                    Return CStr(cmbMes.Text)
                End If
            End Get
            Set(ByVal Text As Slyg.Tools.SlygNullable(Of String))
                cmbMes.Text = Text.Value.ToString
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

            If Program.DesktopGlobal.CentroProcesamientoRow.fk_TipoOficina = 1 Then
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
                CargarSede()
            End If

            Dim opciones() As String = {"- TODOS -", "PENDIENTES DIGITALIZAR", "PENDIENTES TRANSMITIR"}
            dntEstado.Items.AddRange(opciones)
            dntEstado.SelectedIndex = 0

            Dim comboItemsIni As New List(Of String)
            Dim comboItemsFin As New List(Of String)
            For i As Integer = 0 To CInt(MesesConsulta)
                Dim fecha As DateTime = DateTime.Now.AddMonths(-i)
                Dim item As String = fecha.ToString("yyyy/MM") & " - " & fecha.ToString("MMMM yyyy").ToUpper
                comboItemsIni.Add(item)
                comboItemsFin.Add(item)
            Next
            cmbMes.DataSource = comboItemsIni
        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            If cmbMes.SelectedIndex = -1 Then
                MessageBox.Show("Debe seleccionar un Mes", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbMes.Focus()
                Return False
            End If

            Dim MesIni As String = Mid(Me.cmbMes.Text, 1, 7).Replace("/", "")

            If Trim(Me.dntEstado.Text) = "" Then
                MessageBox.Show("Debe seleccionar un estado a consultar", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dntEstado.Focus()
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
        Private Sub CargarSede()

            Dim sedeDataTable As DBSecurity.SchemaConfig.TBL_SedeDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.TBL_Sede", New List(Of QueryParameter) From {
                New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()}
                }, QueryRequestType.Table, QueryResponseType.Table)

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

            Utilities.Llenarcombo(dntOFicina, oficinaDataTable, "Codigo_Centro", "Nombre_Centro_Procesamiento", True, "-1", "- Todos -")
            dntOFicina.Refresh()
        End Sub
#End Region

    End Class

End Namespace