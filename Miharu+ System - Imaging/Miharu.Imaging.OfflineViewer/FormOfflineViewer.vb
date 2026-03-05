Imports System.IO
Imports Miharu.Imaging.OffLineViewer.Library

Public Class FormOffLineViewer

#Region " Estructuras "

    'Private Enum EnumAjuste
    '    ALTO
    '    ANCHO
    '    NINGUNO
    'End Enum

#End Region

#Region " Declaraciones "

    Private Const MinimmumHeigth As Integer = 20
    Private Const DefaultHeigth As Integer = 120
    Private TipologiasSize As Integer
    Private InformacionSize As Integer
    Private ResultadosSize As Integer
    Private ImagenSize As Integer

    Private _Locked As Boolean = False
    Private _ImagePath As String

#End Region

#Region " Eventos "

    Private Sub lblToggleBusqueda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblToggleBusqueda.Click
        If lblToggleBusqueda.Text = "^" Then
            PanelSearch.Height = MinimmumHeigth
            lblToggleBusqueda.Text = "v"
        Else
            PanelSearch.Height = DefaultHeigth
            lblToggleBusqueda.Text = "^"
        End If
    End Sub

    Private Sub lblToggleTipologias_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblToggleTipologias.Click
        If lblToggleTipologias.Text = "^" Then
            TipologiasSize = SplitTipologiasInformacion.SplitterDistance
            SplitTipologiasInformacion.SplitterDistance = SplitTipologiasInformacion.Panel1MinSize
            lblToggleTipologias.Text = "v"
        Else
            If TipologiasSize = MinimmumHeigth Then
                SplitTipologiasInformacion.SplitterDistance = DefaultHeigth
                lblToggleInformacion.Text = "v"
            Else
                SplitTipologiasInformacion.SplitterDistance = TipologiasSize
                lblToggleTipologias.Text = "v"
            End If
            lblToggleTipologias.Text = "^"
        End If
    End Sub

    Private Sub lblToggleInformacion_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblToggleInformacion.Click
        If lblToggleInformacion.Text = "v" Then
            InformacionSize = SplitTipologiasInformacion.SplitterDistance
            SplitTipologiasInformacion.SplitterDistance = SplitTipologiasInformacion.Height - SplitTipologiasInformacion.Panel2MinSize
            lblToggleInformacion.Text = "^"
        Else
            If InformacionSize = MinimmumHeigth Then
                SplitTipologiasInformacion.SplitterDistance = DefaultHeigth
                lblToggleTipologias.Text = "^"
            Else
                SplitTipologiasInformacion.SplitterDistance = InformacionSize
            End If
            lblToggleInformacion.Text = "v"
        End If
    End Sub

    Private Sub lblToggleBusquedaInformacion_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblToggleBusquedaInformacion.Click
        If lblToggleBusquedaInformacion.Text = "<" Then
            SplitPrincipal.Panel1Collapsed = True
            lblToggleBusquedaInformacion.Text = ">"
        Else
            SplitPrincipal.Panel1Collapsed = False
            lblToggleBusquedaInformacion.Text = "<"
        End If
    End Sub

    Private Sub lblToggleResultados_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblToggleResultados.Click
        If lblToggleResultados.Text = "^" Then
            ResultadosSize = SplitResultadosImagen.SplitterDistance
            SplitResultadosImagen.SplitterDistance = SplitResultadosImagen.Panel1MinSize
            lblToggleResultados.Text = "v"
        Else
            If ResultadosSize = MinimmumHeigth Then
                SplitResultadosImagen.SplitterDistance = DefaultHeigth
                lblToggleImagen.Text = "v"
            Else
                SplitResultadosImagen.SplitterDistance = ResultadosSize
            End If
            lblToggleResultados.Text = "^"
        End If
    End Sub

    Private Sub lblToggleImagen_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lblToggleImagen.Click
        If lblToggleImagen.Text = "v" Then
            ImagenSize = SplitResultadosImagen.SplitterDistance
            SplitResultadosImagen.SplitterDistance = SplitResultadosImagen.Height - SplitResultadosImagen.Panel2MinSize
            lblToggleImagen.Text = "^"
        Else
            If ImagenSize = MinimmumHeigth Then
                SplitResultadosImagen.SplitterDistance = DefaultHeigth
                lblToggleResultados.Text = "^"
            Else
                SplitResultadosImagen.SplitterDistance = ImagenSize
            End If
            lblToggleImagen.Text = "v"
        End If
    End Sub

    Private Sub tsmAcercaDe_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsmAcercaDe.Click
        Dim FormAcercade As New FormAbout
        FormAcercade.Show()
    End Sub

    Private Sub tsmSalir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tsmSalir.Click
        End
    End Sub

    Private Sub btnIniciarBusqueda_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnIniciarBusqueda.Click
        Buscar()
    End Sub

    Private Sub ResultadosDataGridView_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ResultadosDataGridView.SelectionChanged
        ShowTipologias()
    End Sub

    Private Sub TipologiasDataGridView_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipologiasDataGridView.SelectionChanged
        ShowData()
    End Sub

    Private Sub validarImagenesToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles validarImagenesToolStripMenuItem.Click
        ValidarImagenes()
    End Sub

#End Region

#Region " Metodos "

    Public Sub Load_Data()
        _Locked = True

        Me.ResultadosDataGridView.AutoGenerateColumns = False
        Me.TipologiasDataGridView.AutoGenerateColumns = False
        Me.CamposDataGridView.AutoGenerateColumns = False
        Me.CamposDataGridView.AutoGenerateColumns = False
        Me.ValidacionesDataGridView.AutoGenerateColumns = False

        If Not File.Exists(Program.AppPath & Program.DataBaseName) Then Throw New Exception("No se encontró la base de datos de indexación: " & Program.DataBaseName)

        Dim CampoBusquedaDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM TBL_Campo_Busqueda", Program.Conexion)

        ' Leer los campos de búsqueda
        CampoBusquedaDataAdapter.Fill(OffLineDataSet.TBL_Campo_Busqueda)

        CampoBusquedaComboBox.DataSource = OffLineDataSet.TBL_Campo_Busqueda
        CampoBusquedaComboBox.DisplayMember = "Nombre_Campo_Busqueda"
        CampoBusquedaComboBox.ValueMember = "id_Campo_Busqueda"

        Dim ConfigDataAdapter = New OleDb.OleDbDataAdapter("SELECT * FROM TBL_Config", Program.Conexion)
        ConfigDataAdapter.Fill(OffLineDataSet.TBL_Config)
        If (OffLineDataSet.TBL_Config.Count > 0) Then
            tslEntidad.Text = OffLineDataSet.TBL_Config(0).Nombre_Entidad
            tslProyecto.Text = OffLineDataSet.TBL_Config(0).Nombre_Proyecto

            Me.ColumnKey1.HeaderText = OffLineDataSet.TBL_Config(0).Key_1
            Me.ColumnKey2.HeaderText = OffLineDataSet.TBL_Config(0).Key_2
            Me.ColumnKey3.HeaderText = OffLineDataSet.TBL_Config(0).Key_3
        End If

        _Locked = False
    End Sub

    Private Sub Buscar()
        _Locked = True

        If CampoBusquedaComboBox.SelectedIndex >= 0 Then
            Dim RowCampoBusqueda = CType(CType(CampoBusquedaComboBox.SelectedItem, DataRowView).Row, xsdOffLineData.TBL_Campo_BusquedaRow)

            Dim SQL As String = ""
            SQL &= "SELECT DISTINCT Folder.fk_Expediente AS id_Expediente, Folder.id_Folder, Folder.id_Esquema, Folder.Nombre_Esquema, Folder.Key_1, Folder.Key_2, Folder.Key_3, Folder.CBarras_Folder "
            SQL &= "FROM CTA_Folder AS Folder INNER JOIN CTA_File_Data AS Data ON (Folder.fk_Expediente = Data.fk_Expediente) AND (Folder.id_Folder = Data.fk_Folder) "
            SQL &= "WHERE Data.fk_Campo_Tipo = " & RowCampoBusqueda.fk_Campo_Tipo & " AND Data.fk_Campo_Busqueda = " & RowCampoBusqueda.id_Campo_Busqueda & " AND Data.Valor_File_Data = '" & txtCampoBusqueda.Text & "'"

            Dim FolderAdapter = New OleDb.OleDbDataAdapter(SQL, Program.Conexion)

            OffLineDataSet.TBL_File_Validacion.Clear()
            OffLineDataSet.TBL_File_Data.Clear()
            OffLineDataSet.TBL_File.Clear()
            OffLineDataSet.TBL_Folder.Clear()
            FolderAdapter.Fill(OffLineDataSet.TBL_Folder)

            If (OffLineDataSet.TBL_Folder.Count = 0) Then
                MessageBox.Show("No se encontraron coincidencias", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCampoBusqueda.Focus()
                txtCampoBusqueda.SelectAll()
            End If
        End If

        _Locked = False

        ShowTipologias()
    End Sub

    Private Sub ShowTipologias()
        If (Not _Locked) Then
            TipologiasDataGridView.ClearSelection()

            If (ResultadosDataGridView.RowCount > 0 And ResultadosDataGridView.SelectedRows.Count > 0) Then
                Dim RowFolder = CType(CType(ResultadosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, xsdOffLineData.TBL_FolderRow)

                Dim SQL As String = ""
                SQL &= "SELECT fk_Expediente AS id_Expediente, fk_Folder AS id_Folder, id_File, id_Version, File_Unique_Identifier, Nombre_Documento, Nombre_Imagen_File, Folios_Documento_File, Tamaño_Imagen_File "
                SQL &= "FROM CTA_File "
                SQL &= "WHERE fk_Expediente = " & RowFolder.id_Expediente & " AND fk_Folder = " & RowFolder.id_Folder

                Dim FileDataAdapter = New OleDb.OleDbDataAdapter(SQL, Program.Conexion)

                OffLineDataSet.TBL_File_Validacion.Clear()
                OffLineDataSet.TBL_File.Clear()
                OffLineDataSet.TBL_File_Data.Clear()

                FileDataAdapter.Fill(OffLineDataSet.TBL_File)
            End If

            'ShowData()
        End If
    End Sub

    Private Sub ShowData()
        If (Not _Locked) Then
            If (TipologiasDataGridView.RowCount > 0 And TipologiasDataGridView.SelectedRows.Count > 0) Then
                Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, xsdOffLineData.TBL_FileRow)
                _ImagePath = Program.AppPath & RowFile.Nombre_Imagen_File
                ShowImagen()

                ' Cargar Campos
                Dim SQL As String = ""
                SQL &= "SELECT fk_Expediente AS id_Expediente, fk_Folder AS id_Folder, fk_File AS id_File, fk_Version AS id_Version, fk_Campo AS id_Campo, Nombre_Campo, Es_Campo_Busqueda, fk_Campo_Tipo, fk_Campo_Busqueda, Valor_File_Data, fk_Documento "
                SQL &= "FROM CTA_File_Data "
                SQL &= "WHERE fk_Expediente = " & RowFile.id_Expediente & " AND fk_Folder = " & RowFile.id_Folder & " AND fk_File = " & RowFile.id_File & " AND fk_Version = " & RowFile.id_Version

                Dim FileDataDataAdapter = New OleDb.OleDbDataAdapter(SQL, Program.Conexion)
                OffLineDataSet.TBL_File_Data.Clear()
                FileDataDataAdapter.Fill(OffLineDataSet.TBL_File_Data)

                ' Cargar Validaciones
                SQL = ""
                SQL &= "SELECT fk_Expediente AS id_Expediente, fk_Folder AS id_Folder, fk_File AS id_File, fk_Version AS id_Version, fk_Validacion AS id_Validacion, Pregunta_Validacion, Respuesta, fk_Documento "
                SQL &= "FROM CTA_File_Validacion "
                SQL &= "WHERE fk_Expediente = " & RowFile.id_Expediente & " AND fk_Folder = " & RowFile.id_Folder & " AND fk_File = " & RowFile.id_File & " AND fk_Version = " & RowFile.id_Version

                Dim FileValidacionDataAdapter = New OleDb.OleDbDataAdapter(SQL, Program.Conexion)
                OffLineDataSet.TBL_File_Data.Clear()
                FileValidacionDataAdapter.Fill(OffLineDataSet.TBL_File_Validacion)
            End If
        End If
    End Sub

    Private Sub ShowImagen()
        Dim directoryName = Path.GetDirectoryName(_ImagePath)
        Dim fileName = Path.GetFileName(_ImagePath)
        Dim fileNames = Directory.GetFiles(directoryName, fileName & "*")

        If (fileNames IsNot Nothing AndAlso fileNames.Length > 0) Then
            Me.Enabled = False

            Me.Cursor = Cursors.AppStarting

            'Select Case Path.GetExtension(_ImagePath).ToLower()
            '   Case ".jpg", ".gif", ".bmp", ".png", ".tif"
            ivCentral.ImagePath = fileNames
            'End Select

            Me.Cursor = Cursors.Default

            Me.Enabled = True
        Else
            MessageBox.Show("No se encontró el archivo asociado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ValidarImagenes()

    End Sub

#End Region

End Class
