Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports DBCore
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.ProcesamientoLight

    Public Class Form_ImpresionCBarras_PL
        Inherits FormBase

#Region " Declaraciones "

        Dim lEntidad As DBCore.SchemaSecurity.CTA_EntidadDataTable
        Dim lProyectos As DBCore.SchemaConfig.TBL_ProyectoDataTable
        Dim lEsquemas As DBCore.SchemaConfig.TBL_EsquemaDataTable
        Dim HomologacionCBarras As SchemaPLight.TBL_Homologacion_CBarrasDataTable

#End Region

#Region " Eventos "

        Private Sub Form_ImpresionCBarras_PL_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            lEntidad = New DBCore.SchemaSecurity.CTA_EntidadDataTable
            lProyectos = New DBCore.SchemaConfig.TBL_ProyectoDataTable
            lEsquemas = New DBCore.SchemaConfig.TBL_EsquemaDataTable
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub ImprimirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImprimirButton.Click
            ImprimirCBarras()
        End Sub

        Private Sub BuscarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarButton.Click
            CBarrasDesktopTextBox.Text = ""
            EntidadDesktopComboBox.Enabled = True
            ProyectoDesktopComboBox.Enabled = True
            EsquemaDesktopComboBox.Enabled = True

            Try
                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                HomologacionCBarras = dbmArchiving.SchemaPLight.TBL_Homologacion_CBarras.DBFindByCBarrar_PL(Utilities.Dlng(CBarrar_PLDesktopTextBox.Text))
                dbmArchiving.Connection_Close()

                'Si Encuentra codigos de barras asociados
                If HomologacionCBarras.Count > 0 Then
                    LlenaVariables()

                    If HomologacionCBarras.Count = 1 Then
                        ImprimirButton.Focus()
                    Else
                        EntidadDesktopComboBox.Focus()
                    End If

                    'si no encuentra ningun codigo de barras
                Else
                    CBarrar_PLDesktopTextBox.Focus()
                    CBarrar_PLDesktopTextBox.SelectAll()
                    DesktopMessageBoxControl.DesktopMessageShow("No se ha encontrado el codigo de barras digitado [" & CBarrar_PLDesktopTextBox.Text & "], por favor pruebe con otro.", "Codigo de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Codigo de barras", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            End Try
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            Llena_Proyectos()
        End Sub

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            Llena_Esquemas()
        End Sub

        Private Sub EsquemaDesktopComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EsquemaDesktopComboBox.SelectedIndexChanged
            Dim ViewFiltro As New DataView(HomologacionCBarras)
            ViewFiltro.RowFilter = HomologacionCBarras.fk_EntidadColumn.ColumnName & "=" & CStr(EntidadDesktopComboBox.SelectedValue) & _
                                   " AND " & HomologacionCBarras.fk_ProyectoColumn.ColumnName & "=" & CStr(ProyectoDesktopComboBox.SelectedValue) & _
                                   " AND " & HomologacionCBarras.fk_EsquemaColumn.ColumnName & "=" & CStr(EsquemaDesktopComboBox.SelectedValue)

            If ViewFiltro.ToTable().Rows.Count > 0 Then
                CBarrasDesktopTextBox.Text = CStr(ViewFiltro.ToTable().Rows(0)(HomologacionCBarras.CBarrar_RiskColumn.ColumnName))
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub

        Public Sub ImprimirCBarras()
            If CBarrasDesktopTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe digitar un codigo de barras", "Codigo de barras vacio", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Exit Sub
            End If

            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim CBarras As String = CBarrasDesktopTextBox.Text
            Dim TableParametros As DataTable = dbmArchiving.Schemadbo.PA_Imprimir_CBarras_Unico.DBExecute(CBarras)

            If TableParametros.Rows.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("No se ha encontrado el codigo de barras asociado a ninguna carpeta", "No se encontro Codigo de barras", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else

                Dim Parametros As New List(Of DesktopConfig.AtributesCBarras)
                Parametros.Clear()
                For Each RowParametros As DataRow In TableParametros.Rows
                    Dim parametro As New DesktopConfig.AtributesCBarras
                    parametro.Label = RowParametros("Label").ToString
                    parametro.Valor = RowParametros("Valor").ToString

                    If Not (parametro.Valor = "" And parametro.Valor = "") Then
                        Parametros.Add(parametro)
                    End If
                Next

                Dim BarCodeControl_ As New Desktop.Controls.BarCode.BarCodeControl
                Utilities.ImprimirCBarras(BarCodeControl_, CBarras, TableParametros.Rows(0)("Title").ToString(), Parametros)
                'BarCodeControl_.Print()

                'codigo de barras de un carpeta
                dbmArchiving.Transaction_Begin()
                If TableParametros.Rows(0)("Tipo").ToString = "0" Then
                    Dim TableFolder = dbmArchiving.Schemadbo.CTA_Folder.DBFindByCBarras_Folderfk_Estado(CBarras, Nothing)
                    Dim Expediente As Long = CLng(TableFolder.Rows(0)(TableFolder.fk_ExpedienteColumn))
                    Dim Folder As Short = CShort(TableFolder.Rows(0)(TableFolder.id_FolderColumn))

                    Dim RegistroFolder As New SchemaRisk.TBL_FolderType With {.Impreso = True}
                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(RegistroFolder, Expediente, Folder, Nothing)
                Else
                    'codigo de barras de un documento

                    Try
                        Dim TableFile = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(CBarras)
                        Dim Expediente As Long = CLng(TableFile.Rows(0)(TableFile.fk_ExpedienteColumn))
                        Dim Folder As Short = CShort(TableFile.Rows(0)(TableFile.fk_FolderColumn))
                        Dim File As Short = CShort(TableFile.Rows(0)(TableFile.id_FileColumn))

                        Dim RegistroFile As New SchemaRisk.TBL_FileType With {.Impreso = True}
                        dbmArchiving.SchemaRisk.TBL_File.DBUpdate(RegistroFile, Nothing, Folder, File, Expediente)
                    Catch ex As Exception

                    End Try
                End If
                dbmArchiving.Transaction_Commit()
            End If
            dbmArchiving.Connection_Close()

            CBarrar_PLDesktopTextBox.Focus()
            CBarrar_PLDesktopTextBox.SelectAll()
        End Sub

        Public Sub Llena_Proyectos()
            Dim ViewFiltro As New DataView(lProyectos)
            ViewFiltro.RowFilter = lProyectos.fk_EntidadColumn.ColumnName & "=" & CStr(EntidadDesktopComboBox.SelectedValue)
            Utilities.LlenarCombo(ProyectoDesktopComboBox, ViewFiltro.ToTable(), lProyectos.id_ProyectoColumn.ColumnName, lProyectos.Nombre_ProyectoColumn.ColumnName)
        End Sub

        Public Sub Llena_Esquemas()
            Dim ViewFiltro As New DataView(lEsquemas)
            ViewFiltro.RowFilter = lEsquemas.fk_EntidadColumn.ColumnName & "=" & CStr(EntidadDesktopComboBox.SelectedValue) & " AND " & _
                                   lEsquemas.fk_ProyectoColumn.ColumnName & "=" & CStr(ProyectoDesktopComboBox.SelectedValue)
            Utilities.LlenarCombo(EsquemaDesktopComboBox, ViewFiltro.ToTable(), lEsquemas.id_EsquemaColumn.ColumnName, lEsquemas.Nombre_EsquemaColumn.ColumnName)
        End Sub

        Public Sub LlenaVariables()
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            lEntidad = New DBCore.SchemaSecurity.CTA_EntidadDataTable
            lProyectos = New DBCore.SchemaConfig.TBL_ProyectoDataTable
            lEsquemas = New DBCore.SchemaConfig.TBL_EsquemaDataTable

            Dim vEntidades = HomologacionCBarras.DefaultView.ToTable(True, HomologacionCBarras.fk_EntidadColumn.ColumnName)
            Dim vProyectos = HomologacionCBarras.DefaultView.ToTable(True, HomologacionCBarras.fk_EntidadColumn.ColumnName, HomologacionCBarras.fk_ProyectoColumn.ColumnName)
            Dim vEsquemas = HomologacionCBarras.DefaultView.ToTable(True, HomologacionCBarras.fk_EntidadColumn.ColumnName, HomologacionCBarras.fk_ProyectoColumn.ColumnName, HomologacionCBarras.fk_EsquemaColumn.ColumnName)

            For Each Enti As DataRow In vEntidades.Rows
                dbmCore.SchemaSecurity.CTA_Entidad.DBFillByid_Entidad(lEntidad, CShort(Enti(HomologacionCBarras.fk_EntidadColumn.ColumnName)))
            Next

            For Each Proy As DataRow In vProyectos.Rows
                dbmCore.SchemaConfig.TBL_Proyecto.DBFillByfk_Entidadid_Proyecto(lProyectos, CShort(Proy(HomologacionCBarras.fk_EntidadColumn.ColumnName)), CShort(Proy(HomologacionCBarras.fk_ProyectoColumn.ColumnName)))
            Next

            For Each Esque As DataRow In vEsquemas.Rows
                dbmCore.SchemaConfig.TBL_Esquema.DBFillByfk_Entidadfk_Proyectoid_Esquema(lEsquemas, CShort(Esque(HomologacionCBarras.fk_EntidadColumn.ColumnName)), CShort(Esque(HomologacionCBarras.fk_ProyectoColumn.ColumnName)), CShort(Esque(HomologacionCBarras.fk_EsquemaColumn.ColumnName)))
            Next

            dbmCore.Connection_Close()

            Utilities.LlenarCombo(EntidadDesktopComboBox, lEntidad, lEntidad.id_EntidadColumn.ColumnName, lEntidad.Nombre_EntidadColumn.ColumnName)
            Utilities.LlenarCombo(ProyectoDesktopComboBox, lProyectos, lProyectos.id_ProyectoColumn.ColumnName, lProyectos.Nombre_ProyectoColumn.ColumnName)
            Utilities.LlenarCombo(EsquemaDesktopComboBox, lEsquemas, lEsquemas.id_EsquemaColumn.ColumnName, lEsquemas.Nombre_EsquemaColumn.ColumnName)

            'Entidades
            If lEntidad.Rows.Count = 1 Then
                EntidadDesktopComboBox.SelectedValue = CStr(lEntidad.Rows(0)(lEntidad.id_EntidadColumn.ColumnName))
                EntidadDesktopComboBox.Enabled = False
            End If

            'Proyectos
            If lProyectos.Rows.Count = 1 Then
                ProyectoDesktopComboBox.SelectedValue = CStr(lProyectos.Rows(0)(lProyectos.id_ProyectoColumn.ColumnName))
                ProyectoDesktopComboBox.Enabled = False
            End If

            'Esquemas
            If lEsquemas.Rows.Count = 1 Then
                EsquemaDesktopComboBox.SelectedValue = CStr(lEsquemas.Rows(0)(lEsquemas.id_EsquemaColumn.ColumnName))
                EsquemaDesktopComboBox.Enabled = False
            End If



            'Valores de los proyectos segun entidad seleccionada
            Dim ViewFiltro1 As New DataView(lProyectos)
            ViewFiltro1.RowFilter = lProyectos.fk_EntidadColumn.ColumnName & "=" & CStr(lEntidad.Rows(0)(lEntidad.id_EntidadColumn.ColumnName))
            Utilities.LlenarCombo(ProyectoDesktopComboBox, ViewFiltro1.ToTable(), lProyectos.id_ProyectoColumn.ColumnName, lProyectos.Nombre_ProyectoColumn.ColumnName)

            'Valores de los esquemas segun entidad y proyecto seleccionados
            Dim ViewFiltro2 As New DataView(lEsquemas)
            ViewFiltro2.RowFilter = lEsquemas.fk_EntidadColumn.ColumnName & "=" & CStr(lEntidad.Rows(0)(lEntidad.id_EntidadColumn.ColumnName)) & " AND " & _
                                    lEsquemas.fk_ProyectoColumn.ColumnName & "=" & CStr(lProyectos.Rows(0)(lProyectos.id_ProyectoColumn.ColumnName))
            Utilities.LlenarCombo(EsquemaDesktopComboBox, ViewFiltro2.ToTable(), lEsquemas.id_EsquemaColumn.ColumnName, lEsquemas.Nombre_EsquemaColumn.ColumnName)

            Dim ViewFiltro3 As New DataView(lEsquemas)
            ViewFiltro3.RowFilter = lEsquemas.fk_EntidadColumn.ColumnName & "=" & CStr(lEntidad.Rows(0)(lEntidad.id_EntidadColumn.ColumnName)) & " AND " & _
                                    lEsquemas.fk_ProyectoColumn.ColumnName & "=" & CStr(lProyectos.Rows(0)(lProyectos.id_ProyectoColumn.ColumnName)) & " AND " & _
                                    lEsquemas.id_EsquemaColumn.ColumnName & "=" & CStr(lEsquemas.Rows(0)(lEsquemas.id_EsquemaColumn.ColumnName))
            Utilities.LlenarCombo(EsquemaDesktopComboBox, ViewFiltro3.ToTable(), lEsquemas.id_EsquemaColumn.ColumnName, lEsquemas.Nombre_EsquemaColumn.ColumnName)



            'Codigo de barras
            If lEntidad.Rows.Count = 1 And lProyectos.Rows.Count = 1 And lEsquemas.Rows.Count = 1 Then
                Dim ViewCBarras As New DataView(HomologacionCBarras)
                ViewCBarras.RowFilter = HomologacionCBarras.fk_EntidadColumn.ColumnName & "=" & CStr(EntidadDesktopComboBox.SelectedValue) & _
                                        " AND " & HomologacionCBarras.fk_ProyectoColumn.ColumnName & "=" & CStr(ProyectoDesktopComboBox.SelectedValue) & _
                                        " AND " & HomologacionCBarras.fk_EsquemaColumn.ColumnName & "=" & CStr(EsquemaDesktopComboBox.SelectedValue)


                If ViewCBarras.ToTable().Rows.Count > 0 Then
                    CBarrasDesktopTextBox.Text = CStr(ViewCBarras.ToTable().Rows(0)(HomologacionCBarras.CBarrar_RiskColumn.ColumnName))
                End If
            End If


        End Sub

#End Region

    End Class

End Namespace