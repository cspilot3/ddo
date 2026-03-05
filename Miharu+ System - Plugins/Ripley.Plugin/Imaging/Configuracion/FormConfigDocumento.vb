Namespace Imaging.Configuracion
    Public Class FormConfigDocumento

#Region " Declaraciones "

        Private _plugin As Plugin

#End Region

#Region " Constructor "

        Public Sub New(nPlugin As Plugin)
            InitializeComponent()

            _plugin = nPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

        Private Sub ActualizarButton_Click(sender As System.Object, e As EventArgs) Handles ActualizarButton.Click
            CargarDocumentos_Nuevos()
        End Sub

#End Region

#Region " Metodos "

        Private Sub FormConfigDocumento_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarDocumentos()
        End Sub

        Private Sub CargarDocumentos()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim DocumentosDataTable = dbmIntegration.SchemaRipley.TBL_Config_Documento.DBGet(Nothing)
                DocumentosDataGridView.AutoGenerateColumns = False
                DocumentosDataGridView.DataSource = DocumentosDataTable
                DocumentosDataGridView.Refresh()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Documentos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarCambios()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                For Each row As DataGridViewRow In DocumentosDataGridView.Rows
                    Dim CampoRow = New DBIntegration.SchemaRipley.TBL_Config_DocumentoType

                    With CampoRow
                        .id_Documento = CShort(row.Cells(0).Value)
                        .Nombre_Documento = row.Cells(1).Value.ToString
                        .fk_Documento_Core = CShort(row.Cells(2).Value)
                    End With

                    Dim campo = dbmIntegration.SchemaRipley.TBL_Config_Documento.DBFindByfk_Documento_Core(CampoRow.fk_Documento_Core)

                    If campo.Rows.Count > 0 Then
                        dbmIntegration.SchemaRipley.TBL_Config_Documento.DBUpdate(CampoRow, CampoRow.id_Documento)
                    Else
                        dbmIntegration.SchemaRipley.TBL_Config_Documento.DBInsert(CampoRow)
                    End If
                Next

                MessageBox.Show("Campos guardados exitosamente", "Guardar Campos", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Campos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub CargarDocumentos_Nuevos()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.RipleyConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim DocumentosDataTable = dbmIntegration.SchemaRipley.CTA_Config_Documento.DBGet()
                DocumentosDataGridView.AutoGenerateColumns = False
                DocumentosDataGridView.DataSource = DocumentosDataTable
                DocumentosDataGridView.Refresh()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Cargar Documentos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace