Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Reportes

    Public Class FormRangoFechasColaD

#Region " Declaraciones "

        Private _UsaOficinas As Boolean
        Private _UsaColasDtal As Boolean

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

        Public Property Oficina() As Slyg.Tools.SlygNullable(Of Integer)
            Get
                If (dntOFicina.Text = "") Then
                    Return DBNull.Value
                Else
                    Return Integer.Parse(CStr(dntOFicina.SelectedValue))
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of Integer))
                dntOFicina.SelectedValue = value.Value.ToString
            End Set
        End Property

        Public Property ColaDtal() As Slyg.Tools.SlygNullable(Of Integer)
            Get
                If (dntColaDtal.Text = "") Then
                    Return DBNull.Value
                Else
                    Return Integer.Parse(CStr(dntColaDtal.SelectedValue))
                End If
            End Get
            Set(ByVal value As Slyg.Tools.SlygNullable(Of Integer))
                dntColaDtal.SelectedValue = value.Value.ToString
            End Set
        End Property


#End Region

#Region " Constructor "

        Public Sub New(ByVal nUsaOficina As Boolean, ByRef nUsaColaDtal As Boolean)
            InitializeComponent()
            _UsaOficinas = nUsaOficina
            _UsaColasDtal = nUsaColaDtal
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

            dntOFicina.Enabled = OficinaLabel.Enabled = _UsaOficinas
            If (_UsaOficinas) Then
                dntOFicina.Visible = True
                OficinaLabel.Visible = True

                Dim oficinaDataTable As DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable

                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Security_Core].Config.PA_CentrosProcesamiento_ComboBox", New List(Of QueryParameter) From {
                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                    New QueryParameter With {.name = "fk_Sede", .value = "-1"}
                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                oficinaDataTable = CType(ClientUtil.mapToTypedTable(New DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable(), queryResponse.dataTable), DBSecurity.SchemaConfig.CTA_CentrosProcesamiento_ComboBoxDataTable)

                Utilities.Llenarcombo(dntOFicina, oficinaDataTable, "Codigo_Centro", "Nombre_Centro_Procesamiento", True, "-1", "- Todos -")

                dntOFicina.Refresh()
            Else
                dntOFicina.Visible = False
                OficinaLabel.Visible = False
            End If

            If _UsaColasDtal Then
                dntColaDtal.Visible = True
                LblCola.Visible = True
                Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[PA_Esquema_Lista_Get]", New List(Of QueryParameter) From {
                                          New QueryParameter With {.name = "fk_Entidad", .value = CInt(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad).ToString()},
                                          New QueryParameter With {.name = "fk_Proyecto", .value = CInt(Program.ImagingGlobal.Proyecto).ToString()}
                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                'Return queryResponseListaItems.dataTable
                Utilities.Llenarcombo(dntColaDtal, queryResponseListaItems.dataTable, "id_Esquema", "Nombre_Esquema", True, "-1", "- Todos -")
                dntColaDtal.Refresh()

            Else
                dntColaDtal.Visible = False
                LblCola.Visible = False
            End If
            Dim comboItemsIni As New List(Of String)
            Dim comboItemsFin As New List(Of String)
            For i As Integer = 0 To CInt(MesesConsulta)
                Dim fecha As DateTime = DateTime.Now.AddMonths(-i)
                Dim item As String = fecha.ToString("yyyy/MM") & " - " & fecha.ToString("MMMM yyyy").ToUpper
                comboItemsIni.Add(item)
                comboItemsFin.Add(item)
            Next
            cmbMesInicial.DataSource = comboItemsIni

        End Sub


#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If ChBMensual.Checked = True Then
                If cmbMesInicial.SelectedIndex = -1 Then
                    MessageBox.Show("Debe seleccionar un Mes Inicial", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbMesInicial.Focus()
                    Return False
                End If
                Dim MesIni As String = Mid(Me.cmbMesInicial.Text, 1, 7).Replace("/", "")

                Me.dtpFechaInicial.Value = New DateTime(CInt(Microsoft.VisualBasic.Left(MesIni, 4)), CInt(Microsoft.VisualBasic.Right(MesIni, 2)), 1)
                Me.dtpFechaFinal.Value = New DateTime(CInt(Microsoft.VisualBasic.Left(MesIni, 4)), CInt(Microsoft.VisualBasic.Right(MesIni, 2)), 1).AddMonths(1).AddSeconds(-1)
            Else
                Me.dtpFechaFinal.Value = Me.dtpFechaInicial.Value
            End If
            If dtpFechaInicial.Value > dtpFechaFinal.Value Then
                MessageBox.Show("La Fecha Final debe ser superior a la Fecha Inicial", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFechaFinal.Focus()
            Else
                Return True
            End If

            Return False
        End Function

        Private Sub ChBMensual_CheckedChanged(sender As Object, e As EventArgs) Handles ChBMensual.CheckedChanged
            If ChBMensual.Checked Then
                dtpFechaFinal.Value = New DateTime(dtpFechaInicial.Value.Year, dtpFechaInicial.Value.Month, DateTime.DaysInMonth(dtpFechaInicial.Value.Year, dtpFechaInicial.Value.Month))
                Me.dtpFechaInicial.Visible = False
                Me.cmbMesInicial.Visible = True
                Me.lblFechaInicial.Text = "Seleccione Mes"
            Else
                Me.dtpFechaInicial.Visible = True
                Me.cmbMesInicial.Visible = False
                Me.lblFechaInicial.Text = "Seleccione Fecha"
            End If

        End Sub
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


#End Region

    End Class

End Namespace