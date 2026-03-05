Imports System.Windows.Forms
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormValidacionesDinamicas
        Inherits Library.FormBase

#Region " Eventos "

        Private Sub FormValidaciones_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            LlenarCombos()
        End Sub

        Private Sub FiltrarButton_Click(sender As System.Object, e As EventArgs) Handles FiltrarButton.Click
            CargarDatos()
        End Sub

        Private Sub NuevaValidacionButton_Click(sender As System.Object, e As EventArgs) Handles NuevaValidacionButton.Click
            Try
                Dim Entidad = CShort(EntidadDesktopComboBox.SelectedValue)
                Dim Proyecto = CShort(ProyectoComboBox.SelectedValue)
                Dim Esquema = CShort(EsquemaDesktopComboBox.SelectedValue)
                Dim Documento = CInt(DocumentoDesktopComboBox.SelectedValue)

                'If Documento = -1 Then
                '    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Por favor seleccione un documento", "Validaciones", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                '    Return
                'End If

                Dim Agregar As New FormNuevaValidacionDinamica(Entidad, Proyecto, Esquema)
                If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()

            Catch
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Por favor verifique que esten seleccionados [Entidad, Proyecto, Esquema y Documento]", "Validaciones", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End Try
        End Sub

        Private Sub ValidacionesDataGridView_CellContentDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles ValidacionesDataGridView.CellContentDoubleClick
            Dim Row = ValidacionesDataGridView.Rows(e.RowIndex)

            Dim fkEntidad = Row.Value(Of Short)("fk_Entidad")
            Dim fkProyecto = Row.Value(Of Short)("fk_Proyecto")
            Dim fkEsquema = Row.Value(Of Short)("fk_Esquema")
            Dim fk_Tipo_Validacion_Dinamica = Row.Value(Of Short)("fk_Tipo_Validacion_Dinamica")
            Dim fk_Documento_1 = Row.Value(Of Integer)("fk_Documento_1")
            Dim fk_Campo_1 = Row.Value(Of Short)("fk_Campo_1")
            Dim fk_Documento_2 = Row.Value(Of Integer)("fk_Documento_2")
            Dim fk_Campo_2 = Row.Value(Of Short)("fk_Campo_2")
            Dim Operador = Row.Value(Of String)("Operador")
            Dim Valor_Comparar = Row.Value(Of String)("Valor_Comparar")
            Dim fk_Validacion = Row.Value(Of Short)("fk_Validacion")
            Dim fk_Documento_Validacion = Row.Value(Of Short)("fk_Documento_Validacion")
            Dim fk_Documento_Obligatorio_1 = Row.Value(Of Integer)("fk_Documento_Obligatorio_1")
            Dim fk_Documento_Obligatorio_2 = Row.Value(Of Integer)("fk_Documento_Obligatorio_2")
            Dim fk_Documento_Obligatorio_3 = Row.Value(Of Integer)("fk_Documento_Obligatorio_3")
            Dim Multiplica_Cantidad_Documento_Obligatorio = Row.Value(Of Boolean)("Multiplica_Cantidad_Documento_Obligatorio")
            Dim Eliminado = Row.Value(Of Boolean)("Eliminado")

            Dim Agregar As New FormNuevaValidacionDinamica(fkEntidad, fkProyecto, fkEsquema, fk_Tipo_Validacion_Dinamica, fk_Documento_1, fk_Campo_1, fk_Documento_2, fk_Campo_2, Operador, Valor_Comparar, fk_Validacion, fk_Documento_Validacion, fk_Documento_Obligatorio_1, fk_Documento_Obligatorio_2, fk_Documento_Obligatorio_3, Multiplica_Cantidad_Documento_Obligatorio, Eliminado)
            If Agregar.ShowDialog() = DialogResult.OK Then CargarDatos()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            LlenarProyectos()
        End Sub

        Private Sub ProyectoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ProyectoComboBox.SelectedIndexChanged
            LlenarEsquemas()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            LlenarDocumentos()
        End Sub

#End Region

#Region " Metodos "

        Public Sub CargarDatos()
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento As New Slyg.Tools.SlygNullable(Of Integer)

                If DocumentoDesktopComboBox.SelectedValue IsNot Nothing Then
                    If (DocumentoDesktopComboBox.SelectedValue.ToString <> "-1") Then Documento = CInt(DocumentoDesktopComboBox.SelectedValue)
                End If

                Dim Validaciones = dmCore.SchemaConfig.CTA_Validaciones_Dinamicas.DBFindByfk_Entidadfk_Proyectofk_Esquemafk_Documento_1Eliminado(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue), CShort(EsquemaDesktopComboBox.SelectedValue), Documento, False)

                If Validaciones.Rows.Count = 0 Then
                    DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("No se han encontrado validaciones para los filtros realizados.", "Validaciones", DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

                ValidacionesDataGridView.AutoGenerateColumns = True
                ValidacionesDataGridView.DataSource = Validaciones
            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

        Public Sub LlenarCombos()
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
                LlenarProyectos()
            End Try
        End Sub

        Public Sub LlenarProyectos()
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
                LlenarEsquemas()
            End Try
        End Sub

        Public Sub LlenarEsquemas()
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

            End Try
        End Sub

        Public Sub LlenarDocumentos()
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