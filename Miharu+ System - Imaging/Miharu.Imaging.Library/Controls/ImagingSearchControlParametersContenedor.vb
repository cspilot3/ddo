Imports Miharu.Desktop.Library.Config.DesktopConfig
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Controls

    Public Class ImagingSearchControlParametersContenedor
        Implements IImagingSearchControlParameters

#Region " Declaraciones"

        Private CampoBusquedaOffDataTable As New DBCore.SchemaImaging.CTA_Campo_Busqueda_ContenedorDataTable

#End Region

#Region " Eventos "

        Private Sub ImagingSearchControlParametersContenedor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
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

            If CampoComboBox.SelectedIndex >= 0 Then
                Try
                    Dim campoTipo As Byte = CampoBusquedaOffDataTable(CampoComboBox.SelectedIndex).fk_Campo_Tipo

                    Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.PA_Busqueda_Contenedor", New List(Of QueryParameter) From {
                    New QueryParameter With {.name = "id_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                    New QueryParameter With {.name = "id_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                    New QueryParameter With {.name = "id_Campo_Tipo", .value = campoTipo.ToString()},
                    New QueryParameter With {.name = "id_Campo_Busqueda", .value = CShort(CampoComboBox.SelectedValue).ToString()},
                    New QueryParameter With {.name = "Valor", .value = ValorTextBox.Text.ToString()}
                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    Dim FolderOffDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.CTA_Busqueda_ContenedorDataTable(), queryResponse.dataTable), DBCore.SchemaImaging.CTA_Busqueda_ContenedorDataTable)

                    RaiseEvent DisplayResult(CType(FolderOffDataTable, DataTable))

                Catch ex As Exception
                    Throw New Exception(ex.Message)
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
            Try
                CampoBusquedaOffDataTable.Clear()

                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.CTA_Campo_Busqueda_Contenedor", New List(Of QueryParameter) From {
                   New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                   New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()}
                    }, QueryRequestType.Table, QueryResponseType.Table)

                CampoBusquedaOffDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.CTA_Campo_Busqueda_ContenedorDataTable(), queryResponse.dataTable), DBCore.SchemaImaging.CTA_Campo_Busqueda_ContenedorDataTable)

            Catch ex As Exception
                Return
            End Try

            CampoComboBox.DataSource = CampoBusquedaOffDataTable
            CampoComboBox.DisplayMember = "Nombre_Campo_Busqueda"
            CampoComboBox.ValueMember = "id_Campo_Busqueda"
        End Sub

#End Region

    End Class
End Namespace