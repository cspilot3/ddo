Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Imaging.Forms.Parametrización
    Public Class FormNuevoCampoMatrizDocumento

        Private _plugin As BanagrarioImagingPlugin

        Private _fkDocumento As Integer

        Public Property fkDocumento() As Integer
            Get
                Return _fkDocumento
            End Get
            Set(ByVal value As Integer)
                _fkDocumento = value
            End Set
        End Property

        Private _columna As Short
        Public Property Columna() As Short
            Get
                Return _columna
            End Get
            Set(ByVal value As Short)
                _columna = value
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

        
        Public Sub New(nPlugin As BanagrarioImagingPlugin)

            InitializeComponent()
            _plugin = nPlugin
        End Sub

        Private Sub FormNuevoCampoMatrizDocumento_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarCombos()
            DocumentoTextBox.Text = _nombreDocumento

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try

                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim MatrizDataTable = dbmAgrario.SchemaConfig.TBL_Matriz_Documento.DBGet(CShort(_fkDocumento), _columna)
                If MatrizDataTable.Rows.Count > 0 Then
                    Dim MatrizDocumentoType = dbmAgrario.SchemaConfig.TBL_Matriz_Documento.DBGet(CShort(_fkDocumento), _columna)(0).ToTBL_Matriz_DocumentoType

                    With MatrizDocumentoType
                        ColumnaMaskedTextBox.Text = .id_Columna.ToString
                        CampoDesktopComboBox.SelectedValue = .id_Campo
                        If Not IsNothing(.id_Campo_Tabla) Then
                            CampoTablaDesktopComboBox.SelectedValue = .id_Campo_Tabla
                        End If
                        TipologiaDesktopComboBox.SelectedValue = .fk_Tipologia
                        NombreCampoTextBox.Text = .Nombre_Campo
                        If Not IsNothing(.Key) Then
                            LlaveTextBox.Text = .Key.ToString
                        End If
                        ProductoDesktopCheckBox.Checked = .Es_Producto
                        If Not IsNothing(.Fecha_Inicio_Proceso) Then
                            FechaInicialProcesoDateTimePicker.Value = .Fecha_Inicio_Proceso
                        End If
                        If Not IsNothing(.Fecha_Fin_Proceso) Then
                            FechaFinalProcesoDateTimePicker.Value = .Fecha_Fin_Proceso
                            AplicaFechaFinCheckBox.Checked = True
                        Else
                            AplicaFechaFinCheckBox.Checked = False
                        End If
                    End With
                End If

            Catch ex As Exception
                DMB.DesktopMessageShow("Guardar Matriz Documento", ex)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try


        End Sub

        Private Sub CargarCombos()

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim camposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBGet(_fkDocumento, Nothing)
                Utilities.LlenarCombo(CampoDesktopComboBox, camposDataTable, camposDataTable.id_CampoColumn.ColumnName, camposDataTable.Nombre_CampoColumn.ColumnName)

                Dim TipologiasDataTable = dbmAgrario.SchemaConfig.CTA_Tipologia_Core.DBGet()
                Utilities.LlenarCombo(TipologiaDesktopComboBox, TipologiasDataTable, TipologiasDataTable.id_TipologiaColumn.ColumnName, TipologiasDataTable.Nombre_TipologiaColumn.ColumnName)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try

        End Sub

        Private Sub CampoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles CampoDesktopComboBox.SelectedIndexChanged
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim camposTabla = dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBGet(_fkDocumento, CShort(CampoDesktopComboBox.SelectedValue), Nothing)
                Utilities.LlenarCombo(CampoTablaDesktopComboBox, camposTabla, camposTabla.id_Campo_TablaColumn.ColumnName, camposTabla.Nombre_CampoColumn.ColumnName)
                CampoTablaDesktopComboBox.Enabled = camposTabla.Rows.Count > 0

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Guardar()
        End Sub

        Private Sub Guardar()
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try

                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim MatrizDocumentoType As New DBAgrario.SchemaConfig.TBL_Matriz_DocumentoType
                With MatrizDocumentoType
                    .fk_Documento = CShort(fkDocumento)
                    .id_Columna = CShort(ColumnaMaskedTextBox.Text)
                    .id_Campo = CShort(CampoDesktopComboBox.SelectedValue)
                    If (CShort(CampoTablaDesktopComboBox.SelectedValue) <> 0) Then
                        .id_Campo_Tabla = CShort(CampoTablaDesktopComboBox.SelectedValue)
                    End If
                    .fk_Tipologia = CShort(TipologiaDesktopComboBox.SelectedValue)
                    .Nombre_Campo = NombreCampoTextBox.Text
                    If IsNumeric(LlaveTextBox.Text) Then
                        .Key = CShort(LlaveTextBox.Text)
                    End If
                    .Es_Producto = ProductoDesktopCheckBox.Checked
                    .Fecha_Inicio_Proceso = FechaInicialProcesoDateTimePicker.Value
                    If AplicaFechaFinCheckBox.Checked Then
                        .Fecha_Fin_Proceso = FechaFinalProcesoDateTimePicker.Value
                    Else
                        .Fecha_Fin_Proceso = DBNull.Value
                    End If
                    .fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id
                    .Fecha_Log = SlygNullable.SysDate

                End With

                Dim MatrizDocumentoDataTable = dbmAgrario.SchemaConfig.TBL_Matriz_Documento.DBGet(CShort(fkDocumento), MatrizDocumentoType.id_Columna)

                If MatrizDocumentoDataTable.Rows.Count = 0 Then
                    dbmAgrario.SchemaConfig.TBL_Matriz_Documento.DBInsert(MatrizDocumentoType)
                Else
                    dbmAgrario.SchemaConfig.TBL_Matriz_Documento.DBUpdate(MatrizDocumentoType, CShort(fkDocumento), MatrizDocumentoType.id_Columna)
                End If

                MessageBox.Show("El Campo se guardo exitosamente", "Matriz Documento", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
            Catch ex As Exception
                DMB.DesktopMessageShow("Guardar Matriz Documento", ex)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try
        End Sub

    End Class

End Namespace