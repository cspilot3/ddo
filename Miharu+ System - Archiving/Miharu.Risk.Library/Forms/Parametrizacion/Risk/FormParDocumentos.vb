Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBArchiving.Schemadbo
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Risk

    Public Class FormParDocumentos
        Inherits FormBase

#Region " Declaraciones "

        Dim TableDocDisponibles As CTA_DocumentosDisponoblesDataTable

#End Region

#Region " Eventos "

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            llenarDocumentosGrilla()
        End Sub

        Private Sub FormParDocumentos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LlenarControles()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub AgregarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AgregarButton.Click
            AgregarDocumentos()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            ActualizarDocumentos()
        End Sub

#End Region

#Region " Metodos "

        Public Sub LlenarControles()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim EsquemasTable = dbmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
            dbmArchiving.Connection_Close()
            Utilities.LlenarCombo(EsquemaDesktopComboBox, EsquemasTable, EsquemasTable.fk_esquemaColumn.ColumnName, EsquemasTable.Nombre_esquemaColumn.ColumnName)

            LlenarDocumentosDisponibes()
            llenarDocumentosGrilla()
        End Sub

        Public Sub LlenarDocumentosDisponibes()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            TableDocDisponibles = dbmArchiving.Schemadbo.CTA_DocumentosDisponobles.DBFindByfk_entidadfk_proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
            dbmArchiving.Connection_Close()

            Utilities.LlenarCombo(DocumentosComboBox, TableDocDisponibles, TableDocDisponibles.id_documentoColumn.ColumnName, TableDocDisponibles.nombre_documentoColumn.ColumnName)
        End Sub

        Public Sub llenarDocumentosGrilla()
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim TableDoc = dbmArchiving.Schemadbo.CTA_DOCUMENTO_ESQUEMA.DBFindByfk_Entidadfk_Proyectofk_EsquemaEs_Obligatorio(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, CShort(EsquemaDesktopComboBox.SelectedValue), Nothing)
                
                DocumentosDesktopDataGridView.AutoGenerateColumns = False
                DocumentosDesktopDataGridView.DataSource = TableDoc
            Catch ex As Exception
                Throw
            Finally
                dbmArchiving.Connection_Close()
            End Try

        End Sub

        Public Sub AgregarDocumentos()
            If CStr(DocumentosComboBox.SelectedValue) = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un documento", "Documento", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Dim Id_documento As String = CStr(DocumentosComboBox.SelectedValue)
                Dim view As DataView = TableDocDisponibles.DefaultView
                view.RowFilter = TableDocDisponibles.id_documentoColumn.ColumnName & "=" & Id_documento
                Dim Table As DataTable = view.ToTable

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                    dbmArchiving.Transaction_Begin()

                    Dim Registro As New SchemaConfig.TBL_DocumentoType
                    Registro.Es_Obligatorio = False
                    Registro.fk_Documento = CInt(Table.Rows(0)(TableDocDisponibles.id_documentoColumn.ColumnName))
                    Registro.fk_Entidad = CShort(Table.Rows(0)(TableDocDisponibles.fk_entidadColumn.ColumnName))
                    Registro.fk_Esquema = CShort(Table.Rows(0)(TableDocDisponibles.fk_esquemaColumn.ColumnName))
                    Registro.fk_Proyecto = CShort(Table.Rows(0)(TableDocDisponibles.fk_proyectoColumn.ColumnName))
                    Registro.Folios_Doble_Captura = False
                    Registro.Monto_Doble_Captura = False
                    Registro.Digitalizar = False

                    dbmArchiving.SchemaConfig.TBL_Documento.DBInsert(Registro)
                    dbmArchiving.Transaction_Commit()

                    DesktopMessageBoxControl.DesktopMessageShow("Documento agregado con exito", "Documento OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    LlenarControles()
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Hubo un error agregando el documento", "Error de Documento", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    dbmArchiving.Transaction_Rollback()
                Finally
                    dbmArchiving.Connection_Close()
                End Try
            End If

        End Sub

        Public Sub ActualizarDocumentos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                dbmArchiving.Transaction_Begin()

                For Each row As DataGridViewRow In DocumentosDesktopDataGridView.Rows
                    Dim Obligatorio As Boolean = CBool(row.Cells("Es_Obligatorio").Value)
                    Dim FoliosCaptura As Boolean = CBool(row.Cells("Folios_Doble_Captura").Value)
                    Dim MontoCaptura As Boolean = CBool(row.Cells("Monto_Doble_Captura").Value)
                    Dim IdDocumento As Integer = CInt(row.Cells("fk_Documento").Value)
                    Dim nDigitalizar As Boolean = CBool(row.Cells("Digitalizar").Value)
                    dbmArchiving.SchemaConfig.TBL_Documento.DBUpdate(Nothing, Nothing, Nothing, Nothing, Obligatorio, FoliosCaptura, MontoCaptura, Nothing, nDigitalizar, IdDocumento)
                Next

                dbmArchiving.Transaction_Commit()
                DesktopMessageBoxControl.DesktopMessageShow("Documento Actualizados con exito", "Documento OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ActualizarDocumentos", ex)
                dbmArchiving.Transaction_Rollback()
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace