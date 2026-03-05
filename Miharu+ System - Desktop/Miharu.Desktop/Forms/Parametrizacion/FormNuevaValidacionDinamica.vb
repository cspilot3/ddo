Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Parametrizacion

    Public Class FormNuevaValidacionDinamica
        Inherits Library.FormBase

#Region " Declaraciones "
        Private _fk_entidad As Short
        Private _fk_proyecto As Short
        Private _fk_esquema As Short
        Private _fk_Tipo_Validacion_Dinamica As Short
        Private _fk_Documento_1 As Integer
        Private _fk_Campo_1 As Short
        Private _fk_Documento_2 As Integer
        Private _fk_Campo_2 As Short
        Private _Operador As String
        Private _Valor_Comparar As String
        Private _fk_Validacion As Short = 0
        Private _fk_Documento_Validacion As Integer
        Private _fk_Documento_Obligatorio_1 As Integer
        Private _fk_Documento_Obligatorio_2 As Integer
        Private _fk_Documento_Obligatorio_3 As Integer
        Private _Multiplica_Cantidad_Documento_Obligatorio As Boolean
        Private _Eliminado As Boolean
#End Region

#Region " Constructor "

        Public Sub New(fk_entidad As Short, fk_proyecto As Short, fk_esquema As Short, fk_Tipo_Validacion_Dinamica As Short, fk_Documento_1 As Integer, fk_Campo_1 As Short, fk_Documento_2 As Integer, fk_Campo_2 As Short, Operador As String, Valor_Comparar As String, fk_Validacion As Short, fk_Documento_Validacion As Integer, fk_Documento_Obligatorio_1 As Integer, fk_Documento_Obligatorio_2 As Integer, fk_Documento_Obligatorio_3 As Integer, Multiplica_Cantidad_Documento_Obligatorio As Boolean, Eliminado As Boolean)
            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto
            _fk_esquema = fk_esquema
            _fk_Tipo_Validacion_Dinamica = fk_Tipo_Validacion_Dinamica
            _fk_Documento_1 = fk_Documento_1
            _fk_Campo_1 = fk_Campo_1
            _fk_Documento_2 = fk_Documento_2
            _fk_Campo_2 = fk_Campo_2
            _Operador = Operador
            _Valor_Comparar = Valor_Comparar
            _fk_Validacion = fk_Validacion
            _fk_Documento_Validacion = fk_Documento_Validacion
            _fk_Documento_Obligatorio_1 = fk_Documento_Obligatorio_1
            _fk_Documento_Obligatorio_2 = fk_Documento_Obligatorio_2
            _fk_Documento_Obligatorio_3 = fk_Documento_Obligatorio_3
            _Multiplica_Cantidad_Documento_Obligatorio = Multiplica_Cantidad_Documento_Obligatorio
            _Eliminado = Eliminado

            InitializeComponent()
        End Sub

        Public Sub New(fk_entidad As Short, fk_proyecto As Short, fk_esquema As Short)
            _fk_Validacion = 0
            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto
            _fk_esquema = fk_esquema

            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormNuevaValidacion_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            LlenarCombos(False)

            Dim listOperadores As List(Of String) = New List(Of String)
            listOperadores.Add("=")
            listOperadores.Add("<>")
            listOperadores.Add(">")
            listOperadores.Add("<")
            listOperadores.Add(">=")
            listOperadores.Add("<=")

            OperadorDesktopComboBoxControl.DataSource = listOperadores

            If _fk_Tipo_Validacion_Dinamica <> 0 And _fk_Documento_1 <> 0 And _fk_Campo_1 <> 0 Then
                TipoValidacionDinamicaDesktopComboBox.SelectedValue = _fk_Tipo_Validacion_Dinamica
                Documentos1DesktopComboBox.SelectedValue = _fk_Documento_1
                Campo1DesktopComboBoxControl.SelectedValue = _fk_Campo_1
                Documentos2DesktopComboBox.SelectedValue = _fk_Documento_2
                Campo2DesktopComboBoxControl.SelectedValue = _fk_Campo_2
                OperadorDesktopComboBoxControl.SelectedItem = _Operador
                ValorCompararDesktopTextBoxControl.Text = _Valor_Comparar
                ValidacionesDesktopComboBoxControl.SelectedValue = _fk_Validacion
                DocumentoValidacionDesktopComboBoxControl.SelectedValue = _fk_Documento_Validacion
                DocumentoObligatorio1DesktopComboBoxControl.SelectedValue = _fk_Documento_Obligatorio_1
                DocumentoObligatorio2DesktopComboBoxControl.SelectedValue = _fk_Documento_Obligatorio_2
                DocumentoObligatorio3DesktopComboBoxControl.SelectedValue = _fk_Documento_Obligatorio_3
                MultiplicaCantidadDocumentoObligatorioCheckBox.Checked = _Multiplica_Cantidad_Documento_Obligatorio
                EliminadoCheckBox.Checked = _Eliminado
            Else
                LlenarCombos(True)
            End If
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub AgregarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Guardar()
        End Sub

#End Region

#Region " Metodos "

        Public Sub Guardar()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Validacion As New DBCore.SchemaConfig.TBL_Validaciones_DinamicasType
                With Validacion
                    .fk_Entidad = _fk_entidad
                    .fk_Proyecto = _fk_proyecto
                    .fk_Esquema = _fk_esquema
                    .fk_Tipo_Validacion_Dinamica = CInt(TipoValidacionDinamicaDesktopComboBox.SelectedValue)
                    .fk_Documento_1 = CInt(Documentos1DesktopComboBox.SelectedValue)
                    .fk_Campo_1 = CInt(Campo1DesktopComboBoxControl.SelectedValue)
                    .fk_Documento_2 = CInt(Documentos2DesktopComboBox.SelectedValue)
                    .fk_Campo_2 = CInt(Campo2DesktopComboBoxControl.SelectedValue)
                    .Operador = OperadorDesktopComboBoxControl.SelectedValue.ToString()
                    .Valor_Comparar = ValorCompararDesktopTextBoxControl.Text
                    .fk_Validacion = CInt(ValidacionesDesktopComboBoxControl.SelectedValue)
                    .fk_Documento_Validacion = CInt(DocumentoValidacionDesktopComboBoxControl.SelectedValue)
                    .fk_Documento_Obligatorio_1 = CInt(DocumentoObligatorio1DesktopComboBoxControl.SelectedValue)
                    .fk_Documento_Obligatorio_2 = CInt(DocumentoObligatorio2DesktopComboBoxControl.SelectedValue)
                    .fk_Documento_Obligatorio_3 = CInt(DocumentoObligatorio3DesktopComboBoxControl.SelectedValue)
                    .Multiplica_Cantidad_Documento_Obligatorio = MultiplicaCantidadDocumentoObligatorioCheckBox.Checked
                    .Eliminado = EliminadoCheckBox.Checked
                End With

                Try
                    If (Documentos2DesktopComboBox.SelectedIndex > 0 And Campo2DesktopComboBoxControl.SelectedIndex <= 0) Then
                        Throw New Exception("Seleccione un campo valido!!!")
                    End If
                Catch ex As Exception
                    Throw New Exception("Seleccione un campo valido!!!")
                End Try

                Try
                    If (TipoValidacionDinamicaDesktopComboBox.SelectedIndex <= 0 Or Documentos1DesktopComboBox.SelectedIndex <= 0 Or Campo1DesktopComboBoxControl.SelectedIndex <= 0 Or ValidacionesDesktopComboBoxControl.SelectedIndex <= 0) Then
                        Throw New Exception("Verifique los valores ingresados!!!")
                    End If
                Catch ex As Exception
                    Throw New Exception("Verifique los valores ingresados!!!")
                End Try

                dmCore.Transaction_Begin()

                Dim ValidacionExistente = dmCore.SchemaConfig.TBL_Validaciones_Dinamicas.DBFindByfk_Entidadfk_Proyectofk_Tipo_Validacion_Dinamicafk_Documento_1fk_Campo_1fk_Esquema(Validacion.fk_Entidad.Value, Validacion.fk_Proyecto.Value, Validacion.fk_Tipo_Validacion_Dinamica, CInt(Validacion.fk_Documento_1.Value), CInt(Validacion.fk_Campo_1.Value), Validacion.fk_Esquema.Value)

                If ValidacionExistente.Count = 0 Then
                    dmCore.SchemaConfig.TBL_Validaciones_Dinamicas.DBInsert(Validacion)
                Else
                    dmCore.SchemaConfig.TBL_Validaciones_Dinamicas.DBUpdate(Validacion, Validacion.fk_Entidad.Value, Validacion.fk_Proyecto.Value, Validacion.fk_Esquema.Value, Validacion.fk_Tipo_Validacion_Dinamica.Value, CInt(Validacion.fk_Documento_1.Value), CInt(Validacion.fk_Campo_1.Value))
                End If

                dmCore.Transaction_Commit()

                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Datos Guardados con exito", "Validaciones", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                If (dmCore IsNot Nothing) Then dmCore.Transaction_Rollback()
                'Me.DialogResult = DialogResult.Cancel
                'DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Validaciones Dinamicas", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dmCore IsNot Nothing) Then dmCore.Connection_Close()
                'Me.Close()
            End Try
        End Sub


        Public Sub LlenarCombos(CargarProyectos As Boolean)
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Tipo = dmCore.SchemaConfig.TBL_Tipo_Validacion_Dinamica.DBGet(Nothing)
                TipoValidacionDinamicaDesktopComboBox.Fill(Tipo, Tipo.Num_AccionColumn, Tipo.Nombre_Validacion_DinamicaColumn, True)

                LlenarDocumentos()
            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

        Public Sub LlenarDocumentos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento = dmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(CShort(_fk_entidad), CShort(_fk_proyecto), CShort(_fk_esquema), False)
                Documentos1DesktopComboBox.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)
                Documentos1DesktopComboBox.SelectedIndex = 0
                Documentos2DesktopComboBox.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)
                DocumentoValidacionDesktopComboBoxControl.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)
                DocumentoObligatorio1DesktopComboBoxControl.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)
                DocumentoObligatorio2DesktopComboBoxControl.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)
                DocumentoObligatorio3DesktopComboBoxControl.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)
            Catch ex As Exception
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

        Public Sub LlenarValidaciones(DocumentoSeleccionado As Integer)
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Validaciones = dmCore.SchemaConfig.TBL_Validacion.DBFindByfk_Documento(DocumentoSeleccionado)
                ValidacionesDesktopComboBoxControl.Fill(Validaciones, Validaciones.id_ValidacionColumn, Validaciones.Pregunta_ValidacionColumn, True)
            Catch ex As Exception
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

#End Region

        Private Sub Documentos1DesktopComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Documentos1DesktopComboBox.SelectedIndexChanged
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            If Documentos1DesktopComboBox.SelectedIndex > 0 Then
                Try
                    dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Campos = dmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_DocumentoEliminado_Campo(CShort(_fk_entidad), CInt(Documentos1DesktopComboBox.SelectedValue), False)
                    Campo1DesktopComboBoxControl.Fill(Campos, Campos.id_CampoColumn, Campos.Nombre_CampoColumn, True)

                    DocumentoValidacionDesktopComboBoxControl.SelectedValue = Documentos1DesktopComboBox.SelectedValue

                    If CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "2" Or CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "4" Then
                        Documentos2DesktopComboBox.SelectedIndex = -1
                        Campo2DesktopComboBoxControl.SelectedIndex = -1
                        Documentos2DesktopComboBox.SelectedValue = Documentos1DesktopComboBox.SelectedValue
                    End If
                Catch ex As Exception
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Error en la selección del documento", "Nueva Validación", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End Try
            End If
        End Sub

        Private Sub Documentos2DesktopComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Documentos2DesktopComboBox.SelectedIndexChanged
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing
            If Documentos2DesktopComboBox.SelectedIndex > 0 Then
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Campos = dmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_DocumentoEliminado_Campo(CShort(_fk_entidad), CShort(Documentos2DesktopComboBox.SelectedValue), False)
                Campo2DesktopComboBoxControl.Fill(Campos, Campos.id_CampoColumn, Campos.Nombre_CampoColumn, True)
            End If
        End Sub

        Private Sub DocumentoValidacionDesktopComboBoxControl_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles DocumentoValidacionDesktopComboBoxControl.SelectedIndexChanged
            If DocumentoValidacionDesktopComboBoxControl.SelectedIndex > 0 Then
                LlenarValidaciones(CInt(DocumentoValidacionDesktopComboBoxControl.SelectedValue))
            End If
        End Sub

        Private Sub TipoValidacionDinamicaDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TipoValidacionDinamicaDesktopComboBox.SelectedIndexChanged
            If TipoValidacionDinamicaDesktopComboBox.SelectedIndex > 0 Then

                Documentos1DesktopComboBox.SelectedIndex = 0
                'Campo1DesktopComboBoxControl.SelectedIndex = -1
                Campo1DesktopComboBoxControl.DataSource = Nothing

                Documentos2DesktopComboBox.SelectedIndex = 0
                'Campo2DesktopComboBoxControl.SelectedIndex = -1
                Campo2DesktopComboBoxControl.DataSource = Nothing

                If (CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "1") Or (CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "3") Then
                    Documentos2DesktopComboBox.Enabled = False
                    Campo2DesktopComboBoxControl.Enabled = False
                    DocumentoObligatorio1DesktopComboBoxControl.Enabled = True
                    DocumentoObligatorio2DesktopComboBoxControl.Enabled = True
                    DocumentoObligatorio3DesktopComboBoxControl.Enabled = True
                    MultiplicaCantidadDocumentoObligatorioCheckBox.Enabled = True

                ElseIf (CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "2") Or (CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "4") Or (CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "5") Then
                    If CStr(TipoValidacionDinamicaDesktopComboBox.SelectedValue) = "5" Then
                        Documentos2DesktopComboBox.Enabled = True
                        Campo2DesktopComboBoxControl.Enabled = True
                    Else
                        Documentos2DesktopComboBox.Enabled = False
                        Campo2DesktopComboBoxControl.Enabled = True
                    End If

                    DocumentoObligatorio1DesktopComboBoxControl.Enabled = False
                    DocumentoObligatorio2DesktopComboBoxControl.Enabled = False
                    DocumentoObligatorio3DesktopComboBoxControl.Enabled = False
                    MultiplicaCantidadDocumentoObligatorioCheckBox.Enabled = False

                    DocumentoObligatorio1DesktopComboBoxControl.SelectedIndex = 0
                    DocumentoObligatorio2DesktopComboBoxControl.SelectedIndex = 0
                    DocumentoObligatorio3DesktopComboBoxControl.SelectedIndex = 0
                    MultiplicaCantidadDocumentoObligatorioCheckBox.Checked = False

                    End If
            End If
        End Sub
    End Class
End Namespace