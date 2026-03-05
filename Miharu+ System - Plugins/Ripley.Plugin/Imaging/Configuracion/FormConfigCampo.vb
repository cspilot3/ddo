
Imports Miharu.Desktop.Library.Config

Namespace Imaging.Configuracion
    
    Public Class FormConfigCampo

#Region " Declaraciones "

        Private _plugin As Plugin

#End Region

#Region " Constructor "

        Public Sub New(nRipleyPlugin As Plugin)

            InitializeComponent()
            _plugin = nRipleyPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormConfigCampo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargaCombos()
        End Sub

        Private Sub id_DocumentoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles id_DocumentoDesktopComboBox.SelectedIndexChanged
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim CamposDataTable = dbmIntegration.SchemaRipley.CTA_Config_Campo.DBFindByfk_DocumentoActivoEs_Busqueda(CInt(id_DocumentoDesktopComboBox.SelectedValue), Nothing, Nothing)
                CamposDataGridView.AutoGenerateColumns = False
                CTACampoParametrizacionDataTableBindingSource.DataSource = CamposDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Campos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
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

                For Each row As DataGridViewRow In CamposDataGridView.Rows
                    Dim CampoRow = New DBIntegration.SchemaRipley.TBL_Config_CampoType

                    With CampoRow
                        .fk_Documento = CShort(row.Cells("fk_Documento").Value)
                        .id_Campo = CShort(row.Cells("id_Campo").Value)
                        .Nombre_Campo = row.Cells("Nombre_Campo").Value.ToString
                        .Columna = CShort(row.Cells("Columna").Value)
                        .Activo = CBool(row.Cells("Activo").Value)
                        .Es_Busqueda = CBool(row.Cells("Es_Busqueda").Value)
                    End With

                    Dim campo = dbmIntegration.SchemaRipley.TBL_Config_Campo.DBGet(CampoRow.fk_Documento, CampoRow.id_Campo)

                    If campo.Rows.Count > 0 Then
                        dbmIntegration.SchemaRipley.TBL_Config_Campo.DBUpdate(CampoRow, CampoRow.fk_Documento, CampoRow.id_Campo)
                    Else
                        dbmIntegration.SchemaRipley.TBL_Config_Campo.DBInsert(CampoRow)
                    End If
                Next

                MessageBox.Show("Campos guardados exitosamente", "Guardar Campos", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Campos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace