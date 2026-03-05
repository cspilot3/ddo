Namespace Controls

    Public Class ImagingSearchControlParameters
        Implements IImagingSearchControlParameters

#Region " Declaraciones"

        Private CampoBusquedaOffDataTable As New DBCore.SchemaImaging.CTA_Campo_BusquedaDataTable

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
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    Dim campoTipo As Byte = CampoBusquedaOffDataTable(CampoComboBox.SelectedIndex).fk_Campo_Tipo
                    Dim FolderOffDataTable = dbmCore.SchemaImaging.PA_Busqueda_Folders.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, campoTipo, CShort(CampoComboBox.SelectedValue), ValorTextBox.Text)

                    RaiseEvent DisplayResult(CType(FolderOffDataTable, DataTable))

                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
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

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                CampoBusquedaOffDataTable.Clear()
                CampoBusquedaOffDataTable = dbmCore.SchemaImaging.CTA_Campo_Busqueda.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
            Catch ex As Exception
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CampoComboBox.DataSource = CampoBusquedaOffDataTable
            CampoComboBox.DisplayMember = "Nombre_Campo_Busqueda"
            CampoComboBox.ValueMember = "id_Campo_Busqueda"
        End Sub


#End Region

    End Class
End Namespace