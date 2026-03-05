Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Risk

    Public Class FormParCampos
        Inherits FormBase

#Region " Eventos "

        Private Sub FormParCampos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaEsquemas()
            Cargacampos(CInt(DocumentosComboBox.SelectedValue))
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            CargaDocumentos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub DocumentosComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentosComboBox.SelectedValueChanged
            Cargacampos(CInt(DocumentosComboBox.SelectedValue))
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            GuardarCambios()
        End Sub

        Private Sub CamposDesktopDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles CamposDesktopDataGridView.CellContentClick
            Try
                Dim fila As DataGridViewRow = CType(sender, DataGridView).Rows(e.RowIndex)

                If (CBool(fila.Cells("Es_Campo_Destape").Value)) Then
                    fila.Cells("Usa_Captura").Value = True
                    fila.Cells("Es_cargue_carpeta").ReadOnly = True
                    fila.Cells("Es_campo_cargue").ReadOnly = True
                    fila.Cells("Usa_Doble_Captura").ReadOnly = True
                Else
                    fila.Cells("Usa_Captura").Value = False
                    fila.Cells("Es_cargue_carpeta").ReadOnly = False
                    fila.Cells("Es_campo_cargue").ReadOnly = False
                    fila.Cells("Usa_Doble_Captura").ReadOnly = False
                End If
            Catch : End Try
        End Sub

        Private Sub CamposDesktopDataGridView_CellClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles CamposDesktopDataGridView.CellClick
            If (e.ColumnIndex = CamposDesktopDataGridView.Columns("AsignarCampoTrigger").Index And e.RowIndex >= 0) Then
                ConfigurarCampoTrigger(CType(CType(CamposDesktopDataGridView.Rows(e.RowIndex).DataBoundItem, DataRowView).Row, Schemadbo.CTA_Campo_ParametrizacionRow))
            End If
        End Sub
#End Region

#Region " Metodos "

        Public Sub CargaEsquemas()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim tableEsquemas = dbmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
            dbmArchiving.Connection_Close()

            Utilities.LlenarCombo(EsquemaDesktopComboBox, tableEsquemas, "fk_Esquema", "Nombre_Esquema")
            cargaDocumentos()
        End Sub

        Public Sub CargaDocumentos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim tableDocs = dbmArchiving.Schemadbo.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEs_Obligatorio(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, CShort(EsquemaDesktopComboBox.SelectedValue), Nothing)
            Utilities.LlenarCombo(DocumentosComboBox, tableDocs, tableDocs.id_DocumentoColumn.ColumnName, tableDocs.Nombre_DocumentoColumn.ColumnName)
            dbmArchiving.Connection_Close()
        End Sub

        Public Sub Cargacampos(ByVal documento As Integer)
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing

            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim tableCampos = dbmArchiving.Schemadbo.CTA_Campo_Parametrizacion.DBFindByfk_Entidadfk_Documento(Program.RiskGlobal.Entidad, documento)
                CamposDesktopDataGridView.AutoGenerateColumns = False
                CamposDesktopDataGridView.DataSource = tableCampos

                For Each Row As DataGridViewRow In CamposDesktopDataGridView.Rows
                    If CBool(Row.Cells("Es_Llave").Value) = True Then
                        Row.Cells("Es_cargue_carpeta").ReadOnly = True
                        Row.Cells("Es_campo_cargue").ReadOnly = True
                    End If

                    'Inhabilita la opción de cargue para los campos tipo tabla asociada.
                    If CByte(tableCampos.Rows(Row.Index).Item("fk_Campo_Tipo")) = DesktopConfig.CampoTipo.TablaAsociada Then
                        Row.Cells("Es_cargue_carpeta").ReadOnly = True
                        Row.Cells("Es_campo_cargue").ReadOnly = True
                    End If

                    If CBool(Row.Cells("Es_campo_Destape").Value) = True Then
                        Row.Cells("Es_cargue_carpeta").ReadOnly = True
                        Row.Cells("Es_campo_cargue").ReadOnly = True
                        Row.Cells("Usa_Doble_Captura").ReadOnly = True
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try            
        End Sub

        Public Sub GuardarCambios()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            For Each row As DataGridViewRow In CamposDesktopDataGridView.Rows
                If CBool(row.Cells("Es_cargue_carpeta").Value) = True And CBool(row.Cells("Es_campo_cargue").Value) = True Then
                    DesktopMessageBoxControl.DesktopMessageShow("No se pude seleccionar cargue carpeta y campo cargue para un mismo campo", "Campo con doble cargue", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Exit Sub
                End If
            Next

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Transaction_Begin()

                For Each row As DataGridViewRow In CamposDesktopDataGridView.Rows
                    Dim Registro As New SchemaConfig.TBL_CampoType
                    Registro.Es_Campo_Cargue = CBool(row.Cells("Es_Campo_cargue").Value)
                    Registro.Es_Cargue_Carpeta = CBool(row.Cells("Es_Cargue_Carpeta").Value)
                    Registro.Es_Imprimible = CBool(row.Cells("Es_Imprimible").Value)
                    Registro.fk_Campo = CShort(row.Cells("id_campo").Value)
                    Registro.Usa_Captura = CBool(row.Cells("Usa_Captura").Value)
                    Registro.Usa_Doble_Captura = CBool(row.Cells("Usa_Doble_Captura").Value)
                    Registro.Es_Campo_Destape = CBool(row.Cells("Es_Campo_Destape").Value)
                    Registro.Es_Campo_Actualizacion = CBool(row.Cells("Es_Campo_Actualizacion").Value)
                    Registro.Es_Categoria = CBool(row.Cells("Es_Categoria").Value)
                    Registro.Usa_Trigger = CBool(row.Cells("Usa_Trigger").Value)

                    If row.Cells("label").Value Is DBNull.Value Then
                        Registro.Label = ""
                    Else
                        Registro.Label = CStr(row.Cells("label").Value)
                    End If

                    Registro.fk_Documento = CShort(row.Cells("fk_documento").Value)

                    If CStr(row.Cells("fk_campo").Value) = "0" Then 'Nuevo
                        dbmArchiving.SchemaConfig.TBL_Campo.DBInsert(Registro)
                        row.Cells("fk_campo").Value = -1
                    Else 'Actualizacion
                        dbmArchiving.SchemaConfig.TBL_Campo.DBUpdate(Registro, Registro.fk_Documento, Registro.fk_Campo)
                    End If
                Next

                dbmArchiving.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("Datos actualizados con éxito", "Parametrización Campos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                dbmArchiving.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("GuardarCambios", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

        End Sub


        Private Sub ConfigurarCampoTrigger(ByVal ctaCampoParametrizacionRow As Schemadbo.CTA_Campo_ParametrizacionRow)
            Dim fCampoTrigger As New FormCamposTrigger()
            fCampoTrigger.CampoRow = ctaCampoParametrizacionRow

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                With ctaCampoParametrizacionRow
                    Dim campoCoreRow = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(.fk_Entidad, .fk_Documento, .id_Campo)

                    If campoCoreRow.Rows.Count > 0 Then
                        Dim campoRow = CType(campoCoreRow.Rows(0), DBCore.SchemaConfig.TBL_CampoRow)
                        'If (Not campoRow.Isfk_Campo_ListaNull) AndAlso (.Usa_Trigger) Then
                        If (.Usa_Trigger) Then
                            If fCampoTrigger.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                MessageBox.Show("Campos trigger parametrizados con éxito", "Campos Trigger", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    End If
                End With

            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub
#End Region

    End Class

End Namespace