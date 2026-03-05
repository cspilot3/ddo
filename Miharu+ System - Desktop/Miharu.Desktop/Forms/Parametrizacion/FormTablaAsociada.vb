Namespace Forms.Parametrizacion
    Public Class FormTablaAsociada

#Region " Propiedades "

        Private _entidadId As Short
        Public Property EntidadId() As Short
            Get
                Return _entidadId
            End Get
            Set(ByVal value As Short)
                _entidadId = value
            End Set
        End Property


        Private _documentoId As Integer
        Public Property DocumentoId() As Integer
            Get
                Return _documentoId
            End Get
            Set(ByVal value As Integer)
                _documentoId = value
            End Set
        End Property

        Private _campoId As Short
        Public Property CampoId() As Short
            Get
                Return _campoId
            End Get
            Set(ByVal value As Short)
                _campoId = value
            End Set
        End Property

        Private _nombreDocumento As String
        Public Property NombreDocumento() As String
            Get
                Return _nombreDocumento
            End Get
            Set(ByVal value As String)
                _nombreDocumento = value
            End Set
        End Property

        Private _nombreEntidad As String
        Public Property NombreEntidad() As String
            Get
                Return _nombreEntidad
            End Get
            Set(ByVal value As String)
                _nombreEntidad = value
            End Set
        End Property

        Private _nombreCampo As String
        Public Property NombreCampo() As String
            Get
                Return _nombreCampo
            End Get
            Set(ByVal value As String)
                _nombreCampo = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub FormTablaAsociada_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            EntidadTextBox.Text = _nombreEntidad
            DocumentoTextBox.Text = _nombreDocumento
            CampoTextBox.Text = _nombreCampo
            Buscar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Buscar()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim camposDataTable = dbmCore.SchemaConfig.CTA_Tabla_Asociada_Parametrizacion.DBFindByfk_Entidadfk_Documentofk_Campoid_Campo_Tabla(_entidadId, _documentoId, _campoId, Nothing)
                CamposDataGridView.AutoGenerateColumns = False
                CamposDataGridView.DataSource = camposDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

        End Sub

#End Region

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Dim NuevoCampoTablaForm = New FormNuevaTablaAsociada()
            NuevoCampoTablaForm.Entidad = _entidadId
            NuevoCampoTablaForm.Documento = _documentoId
            NuevoCampoTablaForm.Fk_Campo = _campoId
            NuevoCampoTablaForm.Id_Campo_Tabla = 0
            If NuevoCampoTablaForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Buscar()
            End If
        End Sub

        Private Sub CamposDataGridView_DoubleClick(sender As System.Object, e As EventArgs) Handles CamposDataGridView.DoubleClick
            Dim NuevoCampoTablaForm = New FormNuevaTablaAsociada()
            NuevoCampoTablaForm.Entidad = _entidadId
            NuevoCampoTablaForm.Documento = _documentoId
            NuevoCampoTablaForm.Fk_Campo = _campoId
            NuevoCampoTablaForm.Id_Campo_Tabla = CShort(CamposDataGridView.SelectedRows(0).Cells("id_Campo_Tabla").Value)
            If NuevoCampoTablaForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Buscar()
            End If
        End Sub
    End Class
End Namespace