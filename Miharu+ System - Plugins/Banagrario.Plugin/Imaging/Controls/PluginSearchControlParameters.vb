Imports Miharu.Imaging.Library.Controls
Imports DBCore
Imports DBAgrario
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Controls

    Public Class PluginSearchControlParameters
        Implements IImagingSearchControlParameters

#Region " Declaraciones "

        Private _Plugin As BanagrarioImagingPlugin

#End Region

#Region " Eventos "

        Private Sub ImagingSearchControlParameters_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not Me.DesignMode Then
                LoadConfig()
            End If
        End Sub

        Private Sub ValorTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                Search()
            End If
        End Sub

        Private Sub BusquedaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BusquedaButton.Click
            Search()
        End Sub

        Private Sub RegionalComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RegionalComboBox.SelectedIndexChanged
            LoadCOPS()
        End Sub

        Private Sub COBComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles COBComboBox.SelectedIndexChanged
            LoadCOBS()
        End Sub

        Private Sub EsquemaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaComboBox.SelectedIndexChanged
            LoadDocumentos()
        End Sub

        Private Sub EstadoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EstadoDesktopComboBox.SelectedIndexChanged
            Select Case CType(CType(EstadoDesktopComboBox.SelectedItem, Slyg.Tools.ComboBoxListItem).Value, EstadoReproceso)
                Case EstadoReproceso.ErrorDara, _
                    EstadoReproceso.TxNoIdentificada, _
                    EstadoReproceso.DocNoIdentificada, _
                    EstadoReproceso.SoporteSobrante, _
                    EstadoReproceso.Validacion_Forma

                    FechaFinDateTimePicker.Visible = True

                Case Else
                    FechaFinDateTimePicker.Visible = False

            End Select

            FechaInicioLabel.Visible = FechaFinDateTimePicker.Visible
            FechaFinLabel.Visible = FechaFinDateTimePicker.Visible
        End Sub

#End Region

#Region " Enumeraciones "

        Enum EstadoReproceso As Short
            ErrorDara = 1
            TxNoIdentificada = 2
            DocNoIdentificada = 3
            SoporteSobrante = 4
            Validacion_Forma = 5
        End Enum

#End Region

#Region " Constructor "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _Plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Implementacion IImagingSearchControlParameters"

        'Public Event DisplayResult(ByVal nData As System.Data.DataTable) Implements IImagingSearchControlParameters.DisplayResult
        Public Event DisplayResult(ByVal nData As DataTable) Implements IImagingSearchControlParameters.DisplayResult

        Public Event BeginSearch() Implements IImagingSearchControlParameters.BeginSearch

        Public Event EndSearch() Implements IImagingSearchControlParameters.EndSearch

        Public Sub SetFocus() Implements IImagingSearchControlParameters.SetFocus
            'ValorTextBox.Focus()
            'ValorTextBox.SelectAll()
        End Sub

        Public Sub Search() Implements IImagingSearchControlParameters.Search
            BusquedaButton.Enabled = False
            Cursor = Cursors.WaitCursor

            RaiseEvent BeginSearch()

            If RegionalComboBox.SelectedIndex >= 0 Then
                Dim dbmAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

                Try
                    dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim esquema = CType(EsquemaComboBox.SelectedValue, Short)
                    Dim documento = CType(DocumentoComboBox.SelectedValue, Integer)
                    Dim regional = CType(RegionalComboBox.SelectedValue, Integer)
                    Dim cob = CType(COBComboBox.SelectedValue, Integer)
                    Dim oficina = CType(OficinaComboBox.SelectedValue, Integer)

                    Dim fechaInicio = New Date(FechaInicioDateTimePicker.Value.Year, FechaInicioDateTimePicker.Value.Month, FechaInicioDateTimePicker.Value.Day, 0, 0, 0)
                    Dim fechaFin = New Date(FechaFinDateTimePicker.Value.Year, FechaFinDateTimePicker.Value.Month, FechaFinDateTimePicker.Value.Day, 23, 59, 59)

                    Dim FolderOffDataTable As DataTable

                    Select Case CType(CType(EstadoDesktopComboBox.SelectedItem, Slyg.Tools.ComboBoxListItem).Value, EstadoReproceso)
                        Case EstadoReproceso.ErrorDara
                            FolderOffDataTable = dbmAgrario.SchemaProcess.PA_Busqueda_Folders_Oficinas_Error.DBExecute(Program.BanagrarioEntidadId, _
                                                                                                                            Program.BanagrarioProyectoImagingId, _
                                                                                                                            esquema, _
                                                                                                                            documento, _
                                                                                                                            regional, _
                                                                                                                            cob, _
                                                                                                                            oficina, _
                                                                                                                            ProcesoRadioButton.Checked, _
                                                                                                                            fechaInicio, _
                                                                                                                            fechaFin)

                        Case EstadoReproceso.TxNoIdentificada
                            FolderOffDataTable = dbmAgrario.SchemaProcess.PA_Busqueda_Folders_Oficinas_TX_NoIdentificada.DBExecute(Program.BanagrarioEntidadId, _
                                                                                                                                        Program.BanagrarioProyectoImagingId, _
                                                                                                                                        esquema, _
                                                                                                                                        documento, _
                                                                                                                                        regional, _
                                                                                                                                        cob, _
                                                                                                                                        oficina, _
                                                                                                                                        ProcesoRadioButton.Checked, _
                                                                                                                                        fechaInicio, _
                                                                                                                                        fechaFin)

                        Case EstadoReproceso.DocNoIdentificada
                            FolderOffDataTable = dbmAgrario.SchemaProcess.PA_Busqueda_Folders_Oficinas_DocNoIdentificado.DBExecute(Program.BanagrarioEntidadId, _
                                                                                                                                        Program.BanagrarioProyectoImagingId, _
                                                                                                                                        esquema, _
                                                                                                                                        documento, _
                                                                                                                                        regional, _
                                                                                                                                        cob, _
                                                                                                                                        oficina, _
                                                                                                                                        ProcesoRadioButton.Checked, _
                                                                                                                                        fechaInicio, _
                                                                                                                                        fechaFin)

                        Case EstadoReproceso.SoporteSobrante
                            FolderOffDataTable = dbmAgrario.SchemaProcess.PA_Busqueda_Folders_Oficinas_SoporteSobrante.DBExecute(Program.BanagrarioEntidadId, _
                                                                                                                                    Program.BanagrarioProyectoImagingId, _
                                                                                                                                    esquema, _
                                                                                                                                    documento, _
                                                                                                                                    regional, _
                                                                                                                                    cob, _
                                                                                                                                    oficina, _
                                                                                                                                    ProcesoRadioButton.Checked, _
                                                                                                                                    fechaInicio, _
                                                                                                                                    fechaFin)

                        Case EstadoReproceso.Validacion_Forma
                            FolderOffDataTable = dbmAgrario.SchemaProcess.PA_Busqueda_Folders_Oficinas_Novedades.DBExecute(Program.BanagrarioEntidadId, _
                                                                                                                                Program.BanagrarioProyectoImagingId, _
                                                                                                                                esquema, _
                                                                                                                                documento, _
                                                                                                                                regional, _
                                                                                                                                cob, _
                                                                                                                                oficina, _
                                                                                                                                ProcesoRadioButton.Checked, _
                                                                                                                                fechaInicio, _
                                                                                                                                fechaFin)

                        Case Else
                            FolderOffDataTable = dbmAgrario.SchemaProcess.PA_Busqueda_Folders_Oficinas.DBExecute(Program.BanagrarioEntidadId, _
                                                                                                                    Program.BanagrarioProyectoImagingId, _
                                                                                                                    esquema, _
                                                                                                                    documento, _
                                                                                                                    regional, _
                                                                                                                    cob, _
                                                                                                                    oficina,
                                                                                                                    ProcesoRadioButton.Checked, _
                                                                                                                    fechaInicio)

                    End Select

                    RaiseEvent DisplayResult(FolderOffDataTable)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    dbmAgrario.Connection_Close()

                End Try

                SetFocus()
            End If

            RaiseEvent EndSearch()

            Cursor = Cursors.Default
            BusquedaButton.Enabled = True
        End Sub

#End Region

#Region " Metodos "

        Public Sub LoadConfig()
            Dim dbmCore As New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            Dim DBMPunteo As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

            Dim RegionalDataTable As DBAgrario.SchemaConfig.TBL_RegionalDataTable
            Dim EsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable

            Try
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                DBMPunteo.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                RegionalDataTable = DBMPunteo.SchemaConfig.TBL_Regional.DBGet(Nothing)
                EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Program.BanagrarioEntidadId, Program.BanagrarioProyectoImagingId, Nothing)

            Catch ex As Exception
                Return
            Finally
                dbmCore.Connection_Close()
                DBMPunteo.Connection_Close()
            End Try

            Utilities.LlenarCombo(RegionalComboBox, RegionalDataTable, RegionalDataTable.id_RegionalColumn.ColumnName, RegionalDataTable.Nombre_RegionalColumn.ColumnName, True, "-1", "- Todos -")
            Utilities.LlenarCombo(EsquemaComboBox, EsquemaDataTable, EsquemaDataTable.id_EsquemaColumn.ColumnName, EsquemaDataTable.Nombre_EsquemaColumn.ColumnName, True, "-1", "- Todos -")

            ' Se crean los estados
            EstadoDesktopComboBox.Items.Add(New Slyg.Tools.ComboBoxListItem("Error Data", EstadoReproceso.ErrorDara))
            EstadoDesktopComboBox.Items.Add(New Slyg.Tools.ComboBoxListItem("Transacción No Identificada", EstadoReproceso.TxNoIdentificada))
            EstadoDesktopComboBox.Items.Add(New Slyg.Tools.ComboBoxListItem("Documento No Identificado", EstadoReproceso.DocNoIdentificada))
            EstadoDesktopComboBox.Items.Add(New Slyg.Tools.ComboBoxListItem("Soporte Sobrante", EstadoReproceso.SoporteSobrante))
            'EstadoDesktopComboBox.Items.Add(New Slyg.Tools.ComboBoxListItem("Novedades En Validaciones de Forma", EstadoReproceso.Validacion_Forma))

            EstadoDesktopComboBox.SelectedIndex = 0

            LoadDocumentos()
        End Sub

        Public Sub LoadCOPS()
            Dim DBMPunteo As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)

            Dim COBDataTable As DBAgrario.SchemaConfig.TBL_COBDataTable

            Try
                DBMPunteo.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                COBDataTable = DBMPunteo.SchemaConfig.TBL_COB.DBFindByfk_Regional(CType(RegionalComboBox.SelectedValue, Short))

            Catch ex As Exception
                Return
            Finally
                DBMPunteo.Connection_Close()
            End Try

            Utilities.LlenarCombo(COBComboBox, COBDataTable, COBDataTable.id_COBColumn.ColumnName, COBDataTable.Nombre_COBColumn.ColumnName, True, "-1", "- Todos -")
        End Sub

        Public Sub LoadCOBS()
            Dim DBMPunteo As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            Dim tblOficina As DBAgrario.SchemaConfig.TBL_OficinaDataTable

            Try
                DBMPunteo.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                tblOficina = DBMPunteo.SchemaConfig.TBL_Oficina.DBFindByfk_COB(CType(COBComboBox.SelectedValue, Short))

            Catch ex As Exception
                Return
            Finally
                DBMPunteo.Connection_Close()
            End Try

            Utilities.LlenarCombo(OficinaComboBox, tblOficina, tblOficina.id_OficinaColumn.ColumnName, tblOficina.Nombre_OficinaColumn.ColumnName, True, "-1", "- Todos -")
        End Sub

        Public Sub LoadDocumentos()
            If (EsquemaComboBox.SelectedIndex >= 0) Then
                Dim dbmCore As New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                Dim DocumentosDataTable As DBCore.SchemaConfig.TBL_DocumentoDataTable

                Try
                    dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    DocumentosDataTable = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.BanagrarioEntidadId, Program.BanagrarioProyectoImagingId, CType(EsquemaComboBox.SelectedValue, Short), False)

                Catch ex As Exception
                    Return
                Finally
                    dbmCore.Connection_Close()
                End Try

                Utilities.LlenarCombo(DocumentoComboBox, DocumentosDataTable, DocumentosDataTable.id_DocumentoColumn.ColumnName, DocumentosDataTable.Nombre_DocumentoColumn.ColumnName, True, "-1", "- Todos -")
            Else
                DocumentoComboBox.Items.Clear()
            End If
        End Sub

#End Region

    End Class

End Namespace