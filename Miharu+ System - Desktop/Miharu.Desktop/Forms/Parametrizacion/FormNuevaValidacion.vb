Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Forms.Parametrizacion

    Public Class FormNuevaValidacion
        Inherits Library.FormBase

#Region " Declaraciones "

        Private _fk_Validacion As Short = 0
        Private _fk_entidad As Short
        Private _fk_proyecto As Short
        Private _fk_esquema As Short
        Private _fk_Documento As Integer
        Private _pregunta As String
        Private _obligatorio As Boolean
        Private _subsanable As Boolean
        Private _usaMotivo As Boolean
        Private _Lista As Integer
        Private _categoria As Integer
        Private _preguntaReporte As String
        Private _eliminado As Boolean
        Private _tipo As Integer

#End Region

#Region " Constructor "

        Public Sub New(id_Validacion As Short, fk_entidad As Short, fk_proyecto As Short, fk_esquema As Short, fk_Documento As Integer, pregunta As String, obligatorio As Boolean, subsanable As Boolean, usaMotivo As Boolean, Lista As Integer, categoria As Integer, preguntaReporte As String, eliminado As Boolean, tipo As Integer)
            _fk_Validacion = id_Validacion

            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto
            _fk_esquema = fk_esquema
            _fk_Documento = fk_Documento
            _pregunta = pregunta
            _obligatorio = obligatorio
            _subsanable = subsanable
            _usaMotivo = usaMotivo
            _Lista = Lista
            _categoria = categoria
            _preguntaReporte = preguntaReporte
            _eliminado = eliminado
            _tipo = tipo

            InitializeComponent()
        End Sub

        Public Sub New(fk_entidad As Short, fk_proyecto As Short, fk_esquema As Short, fk_Documento As Integer)
            _fk_Validacion = 0
            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto
            _fk_esquema = fk_esquema
            _fk_Documento = fk_Documento
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormNuevaValidacion_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            llenarCombos(False)

            If _fk_Validacion <> 0 Then
                PreguntaDesktopTextBox.Text = _pregunta
                ObligatoriaCheckBox.Checked = _obligatorio
                SubsanableCheckBox.Checked = _subsanable
                UsaMotivoCheckBox.Checked = _usaMotivo
                UsaMotivo()
                ListaDesktopComboBox.SelectedValue = _Lista
                CategoriaDesktopComboBox.SelectedValue = _categoria
                PreguntaReporteDesktopTextBox.Text = _preguntaReporte
                EliminadoCheckBox.Checked = _eliminado
                TipoDesktopComboBox.SelectedValue = _tipo
            Else
                llenarCombos(True)
            End If
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub UsaMotivoCheckBox_CheckedChanged(sender As System.Object, e As EventArgs) Handles UsaMotivoCheckBox.CheckedChanged
            UsaMotivo()
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
                'dmCore.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Validacion As New DBCore.SchemaConfig.TBL_ValidacionType
                With Validacion
                    .Eliminado = EliminadoCheckBox.Checked
                    .Usa_Motivo = UsaMotivoCheckBox.Checked
                    .Pregunta_Validacion_Reporte = PreguntaReporteDesktopTextBox.Text
                    .Pregunta_Validacion = PreguntaDesktopTextBox.Text
                    .Obligatorio = ObligatoriaCheckBox.Checked
                    .Es_Subsanable = SubsanableCheckBox.Checked
                    .fk_Validacion_Categoria = CShort(CategoriaDesktopComboBox.SelectedValue)
                    .fk_Tipo_Validacion = CShort(TipoDesktopComboBox.SelectedValue)
                    .fk_Entidad = _fk_entidad
                    .fk_Documento = _fk_Documento
                    .fk_Usuario_Log = Program.Sesion.Usuario.id
                    .Fecha_Log = SlygNullable.SysDate
                End With

                If (UsaMotivoCheckBox.Checked) Then
                    Validacion.fk_Campo_Lista = CShort(ListaDesktopComboBox.SelectedValue)
                Else
                    Validacion.fk_Campo_Lista = DBNull.Value
                End If

                dmCore.Transaction_Begin()

                If _fk_Validacion = 0 Then
                    Validacion.id_Validacion = dmCore.SchemaConfig.TBL_Validacion.DBNextId_for_id_Validacion(Validacion.fk_Documento.Value)
                    dmCore.SchemaConfig.TBL_Validacion.DBInsert(Validacion)
                Else
                    dmCore.SchemaConfig.TBL_Validacion.DBUpdate(Validacion, Validacion.fk_Documento.Value, _fk_Validacion)
                End If

                dmCore.Transaction_Commit()

                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Datos Guardados con exito", "Validaciones", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.DialogResult = DialogResult.OK

            Catch ex As Exception
                If (dmCore IsNot Nothing) Then dmCore.Transaction_Rollback()
                Me.DialogResult = DialogResult.Cancel
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            Finally
                If (dmCore IsNot Nothing) Then dmCore.Connection_Close()
                Me.Close()
            End Try
        End Sub

        Public Sub UsaMotivo()
            If Not UsaMotivoCheckBox.Checked Then ListaDesktopComboBox.SelectedValue = ""
            ListaDesktopComboBox.Enabled = UsaMotivoCheckBox.Checked
        End Sub

        Public Sub llenarCombos(CargarProyectos As Boolean)
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Lista = dmCore.SchemaConfig.TBL_Campo_Lista.DBGet(_fk_entidad, Nothing)
                Dim Categoria = dmCore.SchemaConfig.TBL_Validacion_Categoria.DBGet(Nothing)
                Dim Tipo = dmCore.SchemaConfig.TBL_Tipo_Validacion.DBGet(Nothing)

                CategoriaDesktopComboBox.Fill(Categoria, Categoria.id_Validacion_CategoriaColumn, Categoria.Nombre_Validacion_CategoriaColumn, True)
                ListaDesktopComboBox.Fill(Lista, Lista.id_Campo_ListaColumn, Lista.Nombre_Campo_ListaColumn, True)
                TipoDesktopComboBox.Fill(Tipo, Tipo.id_Tipo_ValidacionColumn, Tipo.Nombre_Tipo_ValidacionColumn, True)

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace