Imports Miharu.Imaging.Library.Controls
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Ripley.Plugin.Imaging
Imports DBCore
Imports DBIntegration

Namespace Controls

    Public Class PluginSearchControlParameters
        Implements IImagingSearchControlParameters

#Region " Declaraciones"

        Private CampoBusquedaOffDataTable As New DBCore.SchemaImaging.CTA_Campo_BusquedaDataTable
        Private _plugin As Imaging.Plugin

#End Region

#Region " Eventos "

        Private Sub ImagingSearchControlParameters_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not Me.DesignMode Then
                LoadConfig()
            End If
        End Sub

        Private Sub ValorTextBox_KeyDown(ByVal sender As System.Object, ByVal e As Windows.Forms.KeyEventArgs) Handles ValorTextBox.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                Search()
            End If
        End Sub

        Private Sub BusquedaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BusquedaButton.Click
            Search()
        End Sub


#End Region

#Region " Constructor "

        Public Sub New(ByVal nRipleyPlugin As Imaging.Plugin)

            InitializeComponent()
            _plugin = nRipleyPlugin

        End Sub

#End Region

#Region " Implementacion IImagingSearchControlParameters"

        Public Event DisplayResult(ByVal nData As DataTable) Implements IImagingSearchControlParameters.DisplayResult

        Public Event BeginSearch() Implements IImagingSearchControlParameters.BeginSearch

        Public Event EndSearch() Implements IImagingSearchControlParameters.EndSearch

        Public Sub SetFocus() Implements IImagingSearchControlParameters.SetFocus
            ValorTextBox.Focus()
            ValorTextBox.SelectAll()
        End Sub

        Public Sub Search() Implements IImagingSearchControlParameters.Search
            BusquedaButton.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            RaiseEvent BeginSearch()

            If PuntoComboBox.SelectedIndex >= 0 Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)

                    dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                    Dim fechaInicio = New Date(FechaInicioDateTimePicker.Value.Year, FechaInicioDateTimePicker.Value.Month, FechaInicioDateTimePicker.Value.Day, 0, 0, 0)
                    Dim fechaFin = New Date(FechaFinDateTimePicker.Value.Year, FechaFinDateTimePicker.Value.Month, FechaFinDateTimePicker.Value.Day, 23, 59, 59)


                    Dim campoTipo As Byte = CampoBusquedaOffDataTable(CampoComboBox.SelectedIndex).fk_Campo_Tipo
                    'Dim FolderOffDataTable = dbmCore.SchemaImaging.PA_Busqueda_Folders.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, campoTipo, CShort(PuntoComboBox.SelectedValue), ValorTextBox.Text)
                    Dim FolderOffDataTable = dbmIntegration.SchemaRipley.PA_Busqueda_Folders.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                       _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, _
                                                                                                       campoTipo, _
                                                                                                       CShort(CampoComboBox.SelectedValue), _
                                                                                                        Trim(ValorTextBox.Text), _
                                                                                                        CShort(PuntoComboBox.SelectedValue), _
                                                                                                        fechaInicio, _
                                                                                                        fechaFin)




                    RaiseEvent DisplayResult(CType(FolderOffDataTable, DataTable))

                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                End Try

                SetFocus()
            End If

            RaiseEvent EndSearch()

            BusquedaButton.Enabled = True
            Me.Cursor = Cursors.Default
        End Sub

#End Region

#Region " Metodos "

        Public Sub LoadConfig()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Dim PuntoDataTable As DBIntegration.SchemaRipley.TBL_Config_PuntoDataTable

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)

                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                PuntoDataTable = dbmIntegration.SchemaRipley.TBL_Config_Punto.DBGet(Nothing)
                CampoBusquedaOffDataTable.Clear()
                CampoBusquedaOffDataTable = dbmCore.SchemaImaging.CTA_Campo_Busqueda.DBFindByfk_Entidadfk_Proyecto(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto)
            Catch ex As Exception
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
            'PuntoComboBox, PuntoDataTable, PuntoDataTable.id_PuntoColumn.ColumnName, PuntoDataTable.Nombre_PuntoColumn.ColumnName, True, "", "-1", "- Todos -")

            Utilities.LlenarCombo(PuntoComboBox, PuntoDataTable, PuntoDataTable.id_PuntoColumn.ColumnName, PuntoDataTable.Nombre_PuntoColumn.ColumnName, True, "-1", "- Todos -")


            CampoComboBox.DataSource = CampoBusquedaOffDataTable
            CampoComboBox.DisplayMember = "Nombre_Campo_Busqueda"
            CampoComboBox.ValueMember = "id_Campo_Busqueda"
        End Sub


#End Region

    End Class
End Namespace