Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Parametrizacion
    Public Class FormNuevoDocumento

#Region " Declaraciones "

        Private _entidad As String
        Private _proyecto As String
        Private _esquema As String
        Private _fkentidad As Short
        Private _fkproyecto As Short
        Private _fkesquema As Short
        Private _idDocumento As String
        Private _nombreDocumento As String
        Private _fkTipologia As Short
        Private _fkDocumentoGrupo As Short

#End Region

#Region " Propiedades "

        Public Property Entidad() As String
            Get
                Return _entidad
            End Get
            Set(ByVal value As String)
                _entidad = value
            End Set
        End Property

        Public Property Proyecto() As String
            Get
                Return _proyecto
            End Get
            Set(ByVal value As String)
                _proyecto = value
            End Set
        End Property

        Public Property Esquema() As String
            Get
                Return _esquema
            End Get
            Set(ByVal value As String)
                _esquema = value
            End Set
        End Property

        Public Property fkEntidad() As Short
            Get
                Return _fkentidad
            End Get
            Set(ByVal value As Short)
                _fkentidad = value
            End Set
        End Property

        Public Property fkProyecto() As Short
            Get
                Return _fkproyecto
            End Get
            Set(ByVal value As Short)
                _fkproyecto = value
            End Set
        End Property

        Public Property fkEsquema() As Short
            Get
                Return _fkesquema
            End Get
            Set(ByVal value As Short)
                _fkesquema = value
            End Set
        End Property

        Public Property IdDocumento() As String
            Get
                Return _idDocumento
            End Get
            Set(ByVal value As String)
                _idDocumento = value
            End Set
        End Property

        Public Property NombreDocumento() As String
            Get
                Return _nombreDocumento
            End Get
            Set(ByVal value As String)
                _nombreDocumento = value
            End Set
        End Property

        Public Property FkTipologia() As Short
            Get
                Return _fkTipologia
            End Get
            Set(ByVal value As Short)
                _fkTipologia = value
            End Set
        End Property

        Public Property DocumentoGrupo() As Short
            Get
                Return _fkDocumentoGrupo
            End Get
            Set(ByVal value As Short)
                _fkDocumentoGrupo = value
            End Set
        End Property

#End Region

#Region "Eventos"

        Private Sub FormNuevoDocumento_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            cargarTipologias()
            cargarDocumentosGrupo()

            EntidadTextBox.Text = _entidad
            ProyectoTextBox.Text = _proyecto
            EsquemaTextBox.Text = _esquema
            IdDocumentoTextBox.Text = _idDocumento
            NombreDocumentoTextBox.Text = _nombreDocumento
            TipologiaComboBox.SelectedValue = _fkTipologia
            DocumentoGrupoComboBox.SelectedValue = _fkDocumentoGrupo

            NombreDocumentoTextBox.Focus()
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            DialogResult = DialogResult.Cancel
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            GuardarDocumento()
        End Sub

#End Region

#Region "Metodos"

        Private Sub cargarTipologias()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim TipologiaDataTable = dbmCore.SchemaConfig.TBL_Tipologia.DBFindByTipologia_Eliminado(False)
                TipologiaComboBox.Fill(TipologiaDataTable, TipologiaDataTable.id_TipologiaColumn, TipologiaDataTable.Nombre_TipologiaColumn)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub cargarDocumentosGrupo()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim DocumentoGrupoDataTable = dbmCore.SchemaConfig.TBL_Documento_Grupo.DBGet(Nothing)
                DocumentoGrupoComboBox.Fill(DocumentoGrupoDataTable, DocumentoGrupoDataTable.id_Documento_GrupoColumn, DocumentoGrupoDataTable.Nombre_Documento_GrupoColumn)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarDocumento()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            'dbmCore.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat
            Try
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                If IdDocumentoTextBox.Text = "" Then

                    Dim DocumentoType As New DBCore.SchemaConfig.TBL_DocumentoType
                    With DocumentoType
                        .fk_Entidad = fkEntidad
                        .fk_Proyecto = fkProyecto
                        .fk_Esquema = fkEsquema
                        .id_Documento = dbmCore.SchemaConfig.TBL_Documento.DBNextId()
                        .Nombre_Documento = NombreDocumentoTextBox.Text
                        .fk_Tipologia = CShort(TipologiaComboBox.SelectedValue)
                        If DocumentoGrupoComboBox.Text <> "" Then
                            .fk_Documento_Grupo = CShort(DocumentoGrupoComboBox.SelectedValue)
                        End If
                        .Eliminado = False
                        .fk_Usuario_Log = Program.Sesion.Usuario.id
                        .Fecha_Log = SlygNullable.SysDate
                    End With

                    dbmCore.SchemaConfig.TBL_Documento.DBInsert(DocumentoType)
                Else

                    Dim DocumentoType As New DBCore.SchemaConfig.TBL_DocumentoType
                    With DocumentoType
                        .Nombre_Documento = NombreDocumentoTextBox.Text
                        .fk_Tipologia = CShort(TipologiaComboBox.SelectedValue)
                        If DocumentoGrupoComboBox.Text <> "" Then
                            .fk_Documento_Grupo = CShort(DocumentoGrupoComboBox.SelectedValue)
                        End If
                    End With
                    dbmCore.SchemaConfig.TBL_Documento.DBUpdate(DocumentoType, CInt(IdDocumentoTextBox.Text))
                End If

                dbmCore.Transaction_Commit()
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                Throw New Exception(ex.Message)
            Finally
                dbmCore.Connection_Close()
                DialogResult = DialogResult.OK
            End Try
        End Sub

#End Region

    End Class

End Namespace