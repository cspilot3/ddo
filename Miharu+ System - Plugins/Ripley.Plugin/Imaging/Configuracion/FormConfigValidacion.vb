Imports Miharu.Desktop.Library.Config

Namespace Imaging.Configuracion

    Public Class FormConfigValidacion

#Region " Declaraciones "

        Private _plugin As Plugin

#End Region

#Region " Constructores "

        Public Sub New(nRipleyPlugin As Plugin)

            InitializeComponent()
            _plugin = nRipleyPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormConfigValidacion_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargaCombos()
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub id_DocumentoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles id_DocumentoDesktopComboBox.SelectedIndexChanged
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim ValidacionDataTable = dbmIntegration.SchemaRipley.CTA_Config_Validacion_Parametrizacion.DBFindByfk_Documento(CInt(id_DocumentoDesktopComboBox.SelectedValue))
                ValidacionesDataGridView.AutoGenerateColumns = False
                CTAValidacionParametrizacionDataTableBindingSource.DataSource = ValidacionDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Metodos "

        Public Sub CargaCombos()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim Documento = dbmIntegration.SchemaRipley.TBL_Config_Documento.DBGet(Nothing)
                Utilities.LlenarCombo(id_DocumentoDesktopComboBox, Documento, Documento.id_DocumentoColumn.ColumnName, Documento.Nombre_DocumentoColumn.ColumnName)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Documentos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub GuardarCambios()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                For Each row As DataGridViewRow In ValidacionesDataGridView.Rows
                    Dim ValidacionesRow = New DBIntegration.SchemaRipley.TBL_Config_ValidacionType

                    With ValidacionesRow
                        .fk_Documento = CShort(row.Cells("fk_Documento").Value)
                        .id_Validacion = CShort(row.Cells("id_Validacion").Value)
                        .Nombre_Validacion = row.Cells("Nombre_Validacion").Value.ToString
                        .Activo = CBool(row.Cells("Activo").Value)
                    End With

                    Dim campo = dbmIntegration.SchemaRipley.TBL_Config_Validacion.DBGet(ValidacionesRow.fk_Documento, ValidacionesRow.id_Validacion)

                    If campo.Rows.Count > 0 Then
                        dbmIntegration.SchemaRipley.TBL_Config_Validacion.DBUpdate(ValidacionesRow, ValidacionesRow.fk_Documento, ValidacionesRow.id_Validacion)
                    Else
                        dbmIntegration.SchemaRipley.TBL_Config_Validacion.DBInsert(ValidacionesRow)
                    End If
                Next

                MessageBox.Show("Campos guardados exitosamente", "Guardar Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Guardar Validaciones", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

#End Region
        
    End Class
End Namespace