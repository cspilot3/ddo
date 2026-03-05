Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Imaging.Forms.Parametrización

    Public Class FormMatrizDocumento

#Region " Declaraciones "

        Private _plugin As BanagrarioImagingPlugin
        Private _matrizDocumentoDataTable As New DBAgrario.SchemaConfig.TBL_Matriz_DocumentoDataTable
        Private _cargarGrilla As Boolean = False

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()
            _plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormMatriz_Documento_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargarCombos()
            DocumentoDesktopComboBox.Enabled = False

        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            CargarDocumentos()
        End Sub

        Private Sub DocumentoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentoDesktopComboBox.SelectedIndexChanged
            If DocumentoDesktopComboBox.Enabled = True And _cargarGrilla Then
                CargarMatrizDocumento()
            End If
        End Sub

        Private Sub AñadirButton_Click(sender As System.Object, e As EventArgs) Handles AñadirButton.Click
            Dim nuevoCampo As New FormNuevoCampoMatrizDocumento(_Plugin)
            nuevoCampo.fkDocumento = CInt(DocumentoDesktopComboBox.SelectedValue)
            nuevoCampo.NombreDocumento = DocumentoDesktopComboBox.Text
            If nuevoCampo.ShowDialog() = DialogResult.OK Then
                CargarMatrizDocumento()
            End If
        End Sub

        Private Sub DataGridViewMatriz_CellContentClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles DataGridViewMatriz.CellContentClick

        End Sub

        Private Sub DataGridViewMatriz_DoubleClick(sender As System.Object, e As EventArgs) Handles DataGridViewMatriz.DoubleClick
            Dim nuevoCampo As New FormNuevoCampoMatrizDocumento(_plugin)
            nuevoCampo.fkDocumento = CInt(DocumentoDesktopComboBox.SelectedValue)
            nuevoCampo.NombreDocumento = DocumentoDesktopComboBox.Text
            nuevoCampo.Columna = CShort(DataGridViewMatriz.SelectedRows(0).Cells("id_Columna").Value)
            If nuevoCampo.ShowDialog() = DialogResult.OK Then
                CargarMatrizDocumento()
            End If
        End Sub

        Private Sub EliminarButton_Click(sender As System.Object, e As EventArgs) Handles EliminarButton.Click
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If MessageBox.Show("Esta seguro de eliminar este campo?", "Matriz Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim documento = CShort(DataGridViewMatriz.SelectedRows(0).Cells("fk_Documento").Value)
                    Dim columna = CShort(DataGridViewMatriz.SelectedRows(0).Cells("id_Columna").Value)
                    dbmAgrario.SchemaConfig.TBL_Matriz_Documento.DBDelete(documento, columna)
                End If

                MessageBox.Show("El campo fue eliminado exitosamente", "Matriz Documento", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarMatrizDocumento()
            Catch ex As Exception
                DMB.DesktopMessageShow("Eliminar Campo Matriz Documento", ex)
            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarCombos()

            Dim dbmCore As New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

            Dim esquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable

            Try
                ''Esquemas 
                esquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Program.BanagrarioEntidadId, Program.BanagrarioProyectoImagingId, Nothing)
                Utilities.LlenarCombo(EsquemaDesktopComboBox, esquemaDataTable, esquemaDataTable.id_EsquemaColumn.ColumnName, esquemaDataTable.Nombre_EsquemaColumn.ColumnName, True, "-1", "- Todos -")

            Catch ex As Exception
                MessageBox.Show("Error", ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub
        Private Sub CargarDocumentos()
            _cargarGrilla = False
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            Dim documentosDataTable As DBCore.SchemaConfig.TBL_DocumentoDataTable
            Try
                ''Documentos
                documentosDataTable = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(Program.BanagrarioEntidadId, Program.BanagrarioProyectoImagingId, CShort(EsquemaDesktopComboBox.SelectedValue), Nothing, False)
                Utilities.LlenarCombo(DocumentoDesktopComboBox, documentosDataTable, documentosDataTable.id_DocumentoColumn.ColumnName, documentosDataTable.Nombre_DocumentoColumn.ColumnName, True, "-1", "- Todos -")
                DocumentoDesktopComboBox.Enabled = True
            Catch ex As Exception
                MessageBox.Show("Error", ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
            DocumentoDesktopComboBox.Enabled = True
            _cargarGrilla = True
        End Sub

        Private Sub CargarMatrizDocumento()
            Dim dbmBancoAgrario As New DBAgrario.DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
            dbmBancoAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
            _matrizDocumentoDataTable.Clear()
            Try
                _matrizDocumentoDataTable = dbmBancoAgrario.SchemaConfig.TBL_Matriz_Documento.DBGet(CShort(DocumentoDesktopComboBox.SelectedValue), Nothing)
                DataGridViewMatriz.DataSource = _matrizDocumentoDataTable
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AccesoDesktopAssembly, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmBancoAgrario.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace