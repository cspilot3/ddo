Imports System.Text
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion.Risk

    Public Class FormParEsquema
        Inherits FormBase

#Region " Metodos "

        Public Sub llenarcombos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim TableTRD = dbmArchiving.SchemaCore.CTA_TRD.DBFindByfk_Entidadid_TRD(Program.RiskGlobal.Entidad, Nothing)
            Utilities.LlenarCombo(TRDComboBox, TableTRD, TableTRD.id_TRDColumn.ColumnName, TableTRD.Nombre_TRDColumn.ColumnName)

            Dim TableSerie = dbmArchiving.SchemaCore.CTA_TRD_Serie.DBFindByfk_TRD(CShort(TRDComboBox.SelectedValue))
            Utilities.LlenarCombo(SerieComboBox, TableSerie, TableSerie.id_TRD_SerieColumn.ColumnName, TableSerie.Nombre_TRD_SerieColumn.ColumnName)

            Dim TableSubserie = dbmArchiving.SchemaCore.CTA_TRD_Subserie.DBFindByfk_TRDfk_TRD_Serie(CShort(TRDComboBox.SelectedValue), CShort(SerieComboBox.SelectedValue))
            Utilities.LlenarCombo(SubserieComboBox, TableSubserie, TableSubserie.id_TRD_SubserieColumn.ColumnName, TableSubserie.Nombre_TRD_SubserieColumn.ColumnName)

            Dim TableFolderTipo = dbmArchiving.SchemaCore.CTA_Folder_Tipo.DBGet()
            Utilities.LlenarCombo(folderTipoComboBox, TableFolderTipo, TableFolderTipo.id_Folder_TipoColumn.ColumnName, TableFolderTipo.Nombre_Folder_TipoColumn.ColumnName)

            Dim EsquemasFacturacion = dbmArchiving.Schemadbo.CTA_Esquema_x_Facturacion.DBFindByfk_Entidad_Cliente(Program.RiskGlobal.Entidad)
            Utilities.LlenarCombo(EsquemaFacturacionDesktopComboBox, EsquemasFacturacion, EsquemasFacturacion.IDColumn.ColumnName, EsquemasFacturacion.ValorColumn.ColumnName)

            dbmArchiving.Connection_Close()
        End Sub
        Public Sub llenarEsquemas()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim TableEsquema = dbmArchiving.Schemadbo.CTA_Esquema_Parametrizacion.DBFindByfk_entidadfk_proyectoid_esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing)
            Utilities.LlenarCombo(EsquemaComboBox, TableEsquema, TableEsquema.id_esquemaColumn.ColumnName, TableEsquema.Nombre_esquemaColumn.ColumnName)
            dbmArchiving.Connection_Close()
        End Sub
        Public Sub SeleccionarEsquema()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim TableEsquema = dbmArchiving.Schemadbo.CTA_Esquema_Parametrizacion.DBFindByfk_entidadfk_proyectoid_esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, CShort(EsquemaComboBox.SelectedValue))

            For Each row As Schemadbo.CTA_Esquema_ParametrizacionRow In TableEsquema
                TRDComboBox.SelectedValue = row.fk_Trd
                SerieComboBox.SelectedValue = row.fk_TRD_Serie
                SubserieComboBox.SelectedValue = row.fk_TRD_Subserie
                folderTipoComboBox.SelectedValue = row.fk_folder_tipo
                AceptasobrantesCheckBox.Checked = row.Acepta_Sobrantes
                fk_esquema.Text = CStr(row.fk_Esquema)
                aceptafaltanteobligatoriosCheckBox.Checked = row.Acepta_Faltantes_Obligatorios
                aceptafoldersobranteCheckBox.Checked = row.Acepta_Sobrantes_Folder
                EsquemaFacturacionDesktopComboBox.SelectedValue = row.Esquema_Facturacion
            Next

            dbmArchiving.Connection_Close()
        End Sub

        Public Sub GuardarCambios()
            Dim dmsdesktop As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                'Dim valida As Boolean = True
                If Validacion() Then

                    dmsdesktop.Connection_Open(Program.Sesion.Usuario.id)

                    Dim Registro As New SchemaConfig.TBL_EsquemaType
                    Registro.Acepta_Sobrantes = AceptasobrantesCheckBox.Checked
                    Registro.fk_Entidad = Program.RiskGlobal.Entidad
                    Registro.fk_Esquema = CShort(EsquemaComboBox.SelectedValue)
                    Registro.fk_Proyecto = Program.RiskGlobal.Proyecto
                    Registro.fk_TRD = CShort(TRDComboBox.SelectedValue)
                    Registro.fk_TRD_Serie = CShort(SerieComboBox.SelectedValue)
                    Registro.fk_TRD_Subserie = CShort(SubserieComboBox.SelectedValue)
                    Registro.fk_Folder_Tipo = CShort(folderTipoComboBox.SelectedValue)
                    Registro.Acepta_Faltantes_Obligatorios = aceptafaltanteobligatoriosCheckBox.Checked
                    Registro.Acepta_Sobrantes_Folder = aceptafoldersobranteCheckBox.Checked
                    Registro.fk_Entidad_Cliente = Program.RiskGlobal.Entidad
                    Registro.fk_Entidad_Facturacion = CShort(Split(EsquemaFacturacionDesktopComboBox.SelectedValue.ToString(), "-")(0))
                    Registro.fk_Esquema_Facturacion = CShort(Split(EsquemaFacturacionDesktopComboBox.SelectedValue.ToString(), "-")(1))

                    dmsdesktop.Transaction_Begin()
                    If fk_esquema.Text = "0" Then 'Nuevo
                        dmsdesktop.SchemaConfig.TBL_Esquema.DBInsert(Registro)
                        fk_esquema.Text = CStr(-1)
                    Else 'Actualizacion
                        dmsdesktop.SchemaConfig.TBL_Esquema.DBUpdate(Registro, Registro.fk_Entidad, Registro.fk_Proyecto, Registro.fk_Esquema)
                    End If

                    DesktopMessageBoxControl.DesktopMessageShow("Datos actualizados con exito", "Esquema OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    dmsdesktop.Transaction_Commit()
                End If

            Catch ex As Exception
                dmsdesktop.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("GuardarCambios", ex)
            Finally
                dmsdesktop.Connection_Close()
            End Try
        End Sub

        Public Function Validacion() As Boolean
            Dim valida As Boolean = True
            Dim CadenaError As New StringBuilder

            Try
                ' ReSharper disable UnusedVariable
                Dim EntidadFacturacion = CShort(Split(EsquemaFacturacionDesktopComboBox.SelectedValue.ToString(), "-")(0))
                Dim EsquemaFacturacion = CShort(Split(EsquemaFacturacionDesktopComboBox.SelectedValue.ToString(), "-")(1))
                ' ReSharper restore UnusedVariable
            Catch ex As Exception
                CadenaError.AppendLine("Debe seleccionar un esquema de facturacion por defecto")
                valida = False
            End Try

            If CShort(TRDComboBox.SelectedValue) = 0 Then
                CadenaError.AppendLine("Debe seleccionar una TRD")
                valida = False
            End If

            If CShort(SerieComboBox.SelectedValue) = 0 Then
                CadenaError.AppendLine("Debe seleccionar una Serie")
                valida = False
            End If

            If CShort(SubserieComboBox.SelectedValue) = 0 Then
                CadenaError.AppendLine("Debe seleccionar una Subserie")
                valida = False
            End If

            If valida = False Then
                DesktopMessageBoxControl.DesktopMessageShow(CadenaError.ToString, "Faltan campos por diligenciar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If

            Return valida
        End Function

#End Region

#Region " Eventos "

        Private Sub FormParEsquema_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            llenarcombos()
            llenarEsquemas()
            SeleccionarEsquema()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub TRDComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TRDComboBox.SelectedValueChanged
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim TableSerie = dbmArchiving.SchemaCore.CTA_TRD_Serie.DBFindByfk_TRD(CShort(TRDComboBox.SelectedValue))
            Utilities.LlenarCombo(SerieComboBox, TableSerie, TableSerie.id_TRD_SerieColumn.ColumnName, TableSerie.Nombre_TRD_SerieColumn.ColumnName)

            Dim TableSubserie = dbmArchiving.SchemaCore.CTA_TRD_Subserie.DBFindByfk_TRDfk_TRD_Serie(CShort(TRDComboBox.SelectedValue), CShort(SerieComboBox.SelectedValue))
            Utilities.LlenarCombo(SubserieComboBox, TableSubserie, TableSubserie.id_TRD_SubserieColumn.ColumnName, TableSubserie.Nombre_TRD_SubserieColumn.ColumnName)
            dbmArchiving.Connection_Close()
        End Sub

        Private Sub SerieComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SerieComboBox.SelectedValueChanged
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim TableSubserie = dbmArchiving.SchemaCore.CTA_TRD_Subserie.DBFindByfk_TRDfk_TRD_Serie(CShort(TRDComboBox.SelectedValue), CShort(SerieComboBox.SelectedValue))
            Utilities.LlenarCombo(SubserieComboBox, TableSubserie, TableSubserie.id_TRD_SubserieColumn.ColumnName, TableSubserie.Nombre_TRD_SubserieColumn.ColumnName)
            dbmArchiving.Connection_Close()
        End Sub

        Private Sub EsquemaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaComboBox.SelectedValueChanged
            SeleccionarEsquema()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If aceptafoldersobranteCheckBox.Checked = True And AceptasobrantesCheckBox.Checked = False Then
                DesktopMessageBoxControl.DesktopMessageShow("No se permiten sobrantes si la carpeta no los permite, por favor cambie los parametros", "Sobrantes", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Else
                GuardarCambios()
            End If
        End Sub

#End Region

    End Class

End Namespace