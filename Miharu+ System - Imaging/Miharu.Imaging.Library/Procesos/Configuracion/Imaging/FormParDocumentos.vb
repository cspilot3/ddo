Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports DBImaging.SchemaConfig

Namespace Procesos.Configuracion.Imaging

    Public Class FormParDocumentos
        Inherits FormBase

#Region " Declaraciones "

        Private DocumentosDataTable As CTA_DocumentoDataTable

#End Region

#Region " Eventos "

        Private Sub FormParDocumentos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaCombos()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                DocumentosDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CShort(EsquemaDesktopComboBox.SelectedValue), False, 0, New DBImaging.SchemaConfig.CTA_DocumentoEnumList(DBImaging.SchemaConfig.CTA_DocumentoEnum.Nombre_Documento, True))

                DocumentosDataGridView.DataSource = DocumentosDataTable

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
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
            If Validar() Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    'dbmImaging.DataBase.Identifier_Date_Format = Program.DesktopGlobal.IdentifierDateFormat

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    dbmImaging.Transaction_Begin()
                    dbmCore.Transaction_Begin()

                    For Each Fila In DocumentosDataTable

                        Fila.id_Documento_Carta_Respuesta = If(IsDBNull(Fila("id_Documento_Carta_Respuesta")), Nothing, Fila.id_Documento_Carta_Respuesta)
                        Fila.id_Documento_Correo_Evidencia = If(IsDBNull(Fila("id_Documento_Correo_Evidencia")), Nothing, Fila.id_Documento_Correo_Evidencia)
                        Fila.id_Documento_Acuse_Recibido = If(IsDBNull(Fila("id_Documento_Acuse_Recibido")), Nothing, Fila.id_Documento_Acuse_Recibido)

                        Dim Registro As New DBImaging.SchemaConfig.TBL_DocumentoType

                        Registro.fk_Entidad = Fila.fk_Entidad
                        Registro.fk_Proyecto = Fila.fk_Proyecto
                        Registro.fk_Esquema = Fila.fk_Esquema
                        Registro.id_Documento = Fila.id_Documento
                        Registro.Es_Obligatorio = Fila.Es_Obligatorio
                        Registro.Es_Anexo = Fila.Es_Anexo
                        Registro.id_Documento_Carta_Respuesta = Fila.id_Documento_Carta_Respuesta
                        Registro.id_Documento_Correo_Evidencia = Fila.id_Documento_Correo_Evidencia
                        Registro.Genera_Carta_Respuesta = Fila.Genera_Carta_Respuesta
                        Registro.id_Documento_Acuse_Recibido = Fila.id_Documento_Acuse_Recibido

                        'If Not Fila.Isid_Documento_Carta_RespuestaNull Then
                        '    Registro.id_Documento_Carta_Respuesta = Fila.id_Documento_Carta_Respuesta
                        'End If

                        If (dbmImaging.SchemaConfig.TBL_Documento.DBGet(Fila.id_Documento).Count = 0) Then
                            dbmImaging.SchemaConfig.TBL_Documento.DBInsert(Registro)
                        Else
                            dbmImaging.SchemaConfig.TBL_Documento.DBUpdate(Registro, Registro.id_Documento)
                        End If

                        Registro.fk_Usuario_Log = Program.Sesion.Usuario.id
                        Registro.Fecha_Log = DateTime.Now
                    Next

                    dbmCore.Transaction_Commit()
                    dbmImaging.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Documento Imaging", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("Hubo problemas al guardar los datos, por favor comuniquese con el administrador" & vbCrLf & vbCrLf & ex.Message, "Problemas en Documento Imaging", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function Validar() As Boolean
            'Dim Validacion As Boolean = True
            'Dim sbErrores As New StringBuilder

            'If Validacion = False Then
            '    DesktopMessageBox.DesktopMessageShow(sbErrores.ToString(), "Error de data", Desktop.Controls.DesktopMessageBox.IconEnum.ErrorIcon, True)
            'End If

            Return True
        End Function

#End Region

    End Class
End Namespace