Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormValidaciones
        Inherits Library.FormBase

#Region " Eventos "

        Private Sub FormValidaciones_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            llenarCombos()
        End Sub

        Private Sub FiltrarButton_Click(sender As System.Object, e As EventArgs) Handles FiltrarButton.Click
            CargarDatos()
        End Sub

        Private Sub Button2_Click(sender As System.Object, e As EventArgs) Handles Button2.Click
            Try
                Dim Entidad = CShort(EntidadDesktopComboBox.SelectedValue)
                Dim Proyecto = CShort(ProyectoComboBox.SelectedValue)
                Dim Esquema = CShort(EsquemaDesktopComboBox.SelectedValue)
                Dim Documento = CInt(DocumentoDesktopComboBox.SelectedValue)

                If Documento = -1 Then
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Por favor seleccione un documento", "Validaciones", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Return
                End If

                Dim Agregar As New FormNuevaValidacion(Entidad, Proyecto, Esquema, Documento)
                If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()

            Catch
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Por favor verifique que esten seleccionados [Entidad, Proyecto, Esquema y Documento]", "Validaciones", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End Try
        End Sub

        Private Sub ValidacionesDataGridView_CellContentDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles ValidacionesDataGridView.CellContentDoubleClick
            Dim Row = ValidacionesDataGridView.Rows(e.RowIndex)

            Dim idValidacion = Row.Value(Of Short)("id_Validacion")
            Dim fkEntidad = Row.Value(Of Short)("fk_Entidad")
            Dim fkProyecto = Row.Value(Of Short)("fk_Proyecto")
            Dim fkEsquema = Row.Value(Of Short)("fk_Esquema")
            Dim fkDocumento = Row.Value(Of Integer)("id_Documento")
            Dim pregunta = Row.Value("Pregunta_Validacion")
            Dim Esobligatorio = Row.Value(Of Boolean)("Obligatorio")
            Dim EsSubsanable = Row.Value(Of Boolean)("Es_Subsanable")
            Dim usaMotivo = Row.Value(Of Boolean)("Usa_Motivo")
            Dim categoria = Row.Value(Of Integer)("id_Validacion_Categoria")
            Dim preguntaReporte = Row.Value("Pregunta_Validacion_Reporte")
            Dim EsEliminado = Row.Value(Of Boolean)("eliminado")
            Dim Lista As Integer = 0
            Dim tipo = Row.Value(Of Integer)("fk_Tipo_Validacion")

            If Not Row.Value("fk_Campo_Lista") <> "" Then Lista = Row.Value(Of Integer)("fk_Campo_Lista")

            Dim Agregar As New FormNuevaValidacion(idValidacion, fkEntidad, fkProyecto, fkEsquema, fkDocumento, pregunta, Esobligatorio, EsSubsanable, usaMotivo, Lista, categoria, preguntaReporte, EsEliminado, tipo)
            If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            llenarProyectos()
        End Sub

        Private Sub ProyectoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ProyectoComboBox.SelectedIndexChanged
            llenarEsquemas()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            llenarDocumentos()
        End Sub

#End Region

#Region " Metodos "

        Public Sub CargarDatos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento As New Slyg.Tools.SlygNullable(Of Integer)
                If (DocumentoDesktopComboBox.SelectedValue.ToString <> "-1") Then Documento = CInt(DocumentoDesktopComboBox.SelectedValue)

                Dim Validaciones = dmCore.SchemaConfig.CTA_Validacion.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_Documento(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), Documento)

                If Validaciones.Rows.Count = 0 Then
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No se han encontrado validaciones para los filtros realizados.", "Validaciones", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                ValidacionesDataGridView.AutoGenerateColumns = False
                ValidacionesDataGridView.DataSource = Validaciones
            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

        Public Sub llenarCombos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim EntidadDataTable = dmCore.SchemaSecurity.CTA_Entidad.DBGet()
                EntidadDesktopComboBox.Fill(EntidadDataTable, EntidadDataTable.id_EntidadColumn, EntidadDataTable.Nombre_EntidadColumn)

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
                llenarProyectos()
            End Try
        End Sub

        Public Sub llenarProyectos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim ProyectoDataTable = dmCore.SchemaConfig.TBL_Proyecto.DBGet(CShort(EntidadDesktopComboBox.SelectedValue), Nothing)
                ProyectoComboBox.Fill(ProyectoDataTable, ProyectoDataTable.id_ProyectoColumn, ProyectoDataTable.Nombre_ProyectoColumn)

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
                llenarEsquemas()
            End Try
        End Sub

        Public Sub llenarEsquemas()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim EsquemaDataTable = dmCore.SchemaConfig.TBL_Esquema.DBGet(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue), Nothing)
                EsquemaDesktopComboBox.Fill(EsquemaDataTable, EsquemaDataTable.id_EsquemaColumn, EsquemaDataTable.Nombre_EsquemaColumn)

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
                llenarDocumentos()
            End Try
        End Sub

        Public Sub llenarDocumentos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento = dmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), False)
                DocumentoDesktopComboBox.Fill(Documento, Documento.id_DocumentoColumn, Documento.Nombre_DocumentoColumn, True)

            Catch ex As Exception
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace