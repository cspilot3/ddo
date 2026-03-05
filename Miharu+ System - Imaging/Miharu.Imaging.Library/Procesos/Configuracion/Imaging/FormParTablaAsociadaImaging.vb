Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Procesos.Configuracion.Imaging

    Public Class FormParTablaAsociadaImaging

#Region " Declaraciones "

        Private TablaAsociadaDataTable As DBImaging.SchemaConfig.CTA_Tabla_Asociada_ParametrizacionDataTable

#End Region

#Region " Eventos "

        Private Sub FormTablaAsociadaImaging_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargaCombos()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Documento = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CShort(EsquemaDesktopComboBox.SelectedValue), False, 0, New DBImaging.SchemaConfig.CTA_DocumentoEnumList(DBImaging.SchemaConfig.CTA_DocumentoEnum.Nombre_Documento, True))
                Utilities.LlenarCombo(id_DocumentoDesktopComboBox, Documento, Documento.id_DocumentoColumn.ColumnName, Documento.Nombre_DocumentoColumn.ColumnName)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub id_DocumentoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles id_DocumentoDesktopComboBox.SelectedIndexChanged
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim campos = dbmImaging.SchemaConfig.CTA_Campo_Parametrizacion.DBFindByfk_Documentoid_CampoEliminado_Campo(CInt(id_DocumentoDesktopComboBox.SelectedValue), Nothing, False)

                Utilities.LlenarCombo(CamposDesktopComboBox, campos, campos.id_CampoColumn.ColumnName, campos.Nombre_CampoColumn.ColumnName)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub CamposDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles CamposDesktopComboBox.SelectedIndexChanged
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                TablaAsociadaDataTable = dbmImaging.SchemaConfig.CTA_Tabla_Asociada_Parametrizacion.DBFindByfk_Entidadfk_Documentofk_Campoid_Campo_Tabla(Program.ImagingGlobal.Entidad, CInt(id_DocumentoDesktopComboBox.SelectedValue), CShort(CamposDesktopComboBox.SelectedValue), Nothing)

                CTATablaAsociadaConfiguracionDataTableBindingSource.DataSource = TablaAsociadaDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub
#End Region

#Region " Metodos "

        Public Sub CargaCombos()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim Esquema As New DataView(Program.ImagingGlobal.Esquemas)
                Esquema.RowFilter = Program.ImagingGlobal.Esquemas.fk_EntidadColumn.ColumnName & "=" & Program.ImagingGlobal.Entidad _
                                    & " AND " & Program.ImagingGlobal.Esquemas.fk_ProyectoColumn.ColumnName & "=" & Program.ImagingGlobal.Proyecto

                Utilities.LlenarCombo(EsquemaDesktopComboBox, Esquema.ToTable(), Program.ImagingGlobal.Esquemas.id_EsquemaColumn, Program.ImagingGlobal.Esquemas.Nombre_EsquemaColumn)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub GuardarCambios()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                'dbmImaging.DataBase.Identifier_Date_Format = Program.DesktopGlobal.IdentifierDateFormat

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                For Each Fila In TablaAsociadaDataTable
                    Dim Registro As New DBImaging.SchemaConfig.TBL_Tabla_AsociadaType
                    Dim RegistroCore As New DBCore.SchemaConfig.TBL_Tabla_AsociadaType

                    Registro.fk_Documento = Fila.fk_Documento
                    RegistroCore.fk_Documento = Fila.fk_Documento
                    Registro.fk_Campo = Fila.fk_Campo
                    Registro.id_Campo_Tabla = Fila.id_Campo_Tabla
                    RegistroCore.id_Campo_Tabla = Fila.id_Campo_Tabla


                    If (Fila.IsNull("Mascara")) Then
                        Registro.Mascara = ""
                    Else
                        Registro.Mascara = Fila.Mascara
                    End If

                    If (Fila.IsNull("Formato")) Then
                        Registro.Formato = ""
                    Else
                        Registro.Formato = Fila.Formato
                    End If

                    Registro.fk_Usuario_Log = Program.Sesion.Usuario.id
                    Registro.Fecha_Log = DateTime.Now

                    If (dbmImaging.SchemaConfig.TBL_Tabla_Asociada.DBGet(Registro.fk_Documento, Registro.fk_Campo, Registro.id_Campo_Tabla).Count = 0) Then
                        dbmImaging.SchemaConfig.TBL_Tabla_Asociada.DBInsert(Registro)
                    Else
                        dbmImaging.SchemaConfig.TBL_Tabla_Asociada.DBUpdate(Registro, Registro.fk_Documento, Registro.fk_Campo, Registro.id_Campo_Tabla)
                        dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBUpdate(RegistroCore, RegistroCore.fk_Documento, RegistroCore.fk_Campo, RegistroCore.id_Campo_Tabla)
                    End If
                Next

                dbmImaging.Transaction_Commit()
                dbmCore.Transaction_Commit()

                DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()

                DesktopMessageBoxControl.DesktopMessageShow("Hubo problemas al guardar los datos, por favor comuniquese con el administrador" & vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region


    End Class
End Namespace