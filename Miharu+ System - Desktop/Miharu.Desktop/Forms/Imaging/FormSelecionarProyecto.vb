Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBImaging.SchemaSecurity
Imports Miharu.Imaging.Library
Imports Miharu.Desktop.Library.Config
Imports DBImaging.SchemaCore
Imports System.Data.SqlClient

Public Class FormSelecionarProyecto
#Region " Constructores "
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region " Function "
    Public Function Validar() As Boolean
        Dim bReturn As Boolean = True
        Try
            If EntidadComboBox.SelectedValue.ToString() = "" Then
                bReturn = False
            ElseIf ProyectoComboBox.SelectedValue.ToString() = "" Then
                bReturn = False
            ElseIf SeleccionfechasRadioButton.Checked = False And CargarLogRadioButton.Checked = False And CargarLogLlavesRadioButton.Checked = False Then
                bReturn = False
            End If
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Validar", ex)
        End Try
        Return bReturn
    End Function
#End Region
#Region " Metodos "
    Private Sub FormSeleecionarProyecto_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
        Try
            EntidadComboBox.ValueMember = "id_entidad"
            EntidadComboBox.DisplayMember = "nombre_entidad"
            ProyectoComboBox.ValueMember = "id_proyecto"
            ProyectoComboBox.DisplayMember = "nombre_proyecto"

            Dim entidadProyectoEnumList As New CTA_EntidadEnumList
            entidadProyectoEnumList.Add(CTA_EntidadEnum.Nombre_Entidad, True)

            EntidadComboBox.DataSource = dbmImaging.SchemaSecurity.CTA_Entidad.DBGet(0, entidadProyectoEnumList)
            Dim proyectoDataTable = dbmImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_Usuario.DBFindByfk_EntidadAplica_FisicoAplica_Imagenfk_Usuario(CShort(EntidadComboBox.SelectedValue), Nothing, True, Program.Sesion.Usuario.id)
            ProyectoComboBox.DataSource = proyectoDataTable


            EntidadComboBox.Focus()
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub
    Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
        Dim Entidad As Short = CShort(EntidadComboBox.SelectedValue)
        Dim Proyecto As Short = CShort(ProyectoComboBox.SelectedValue)
        Dim NombreProyecto As String = ProyectoComboBox.Text
        If Validar() Then

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                Miharu.Imaging.Library.Program.DesktopGlobal = New DesktopGlobal()
                Miharu.Imaging.Library.Program.ImagingGlobal = New ImagingGlobal()
                Miharu.Imaging.Library.Program.ImagingGlobal.Entidad = Entidad
                Miharu.Imaging.Library.Program.ImagingGlobal.Proyecto = Proyecto
                Miharu.Imaging.Library.Program.DesktopGlobal = Program.DesktopGlobal
                Miharu.Imaging.Library.Program.Sesion = Program.Sesion

                Dim parametrosDataTable = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet(DBImaging.ParametroSistemaEnum.USA_DOMINIO)

                If (parametrosDataTable.Count > 0) Then
                    Miharu.Imaging.Library.Program.ImagingGlobal.UsaDominioExterno = parametrosDataTable(0).Valor_Parametro_Sistema = "1"
                Else
                    Miharu.Imaging.Library.Program.ImagingGlobal.UsaDominioExterno = False
                End If

                Dim proyectoDataTable = dbmImaging.SchemaCore.CTA_Proyecto.DBFindByfk_Entidadid_Proyecto(Entidad, Proyecto)

                If proyectoDataTable.Count = 0 Then
                    Try
                        dbmImaging.Transaction_Begin()

                        Dim registroType As New DBImaging.SchemaConfig.TBL_ProyectoType
                        registroType.fk_Entidad = Entidad
                        registroType.fk_Proyecto = Proyecto
                        registroType.Input_Folder = ""
                        registroType.Usa_Rango_Paquetes = False
                        registroType.Inicio_Nombre_Paquete = ""
                        registroType.Formato_Fecha_Paquete = ""
                        registroType.Usa_Archivo_Indices = False
                        registroType.Nombre_Archivo_Indices = ""
                        registroType.fk_Separador = Nothing
                        registroType.Columnas_Archivo = CShort(0)
                        registroType.Usa_Encabezado_Columnas = False
                        registroType.fk_Identificador_Texto = Nothing
                        registroType.Columna_Imagen = CShort(0)
                        registroType.Caracteres_Omitir = CByte(0)
                        registroType.fk_Formato_Entrada = CShort(3)
                        registroType.Columna_Key = CShort(0)
                        registroType.Usa_Indexacion = False
                        registroType.Usa_Paquete_x_Imagen = False
                        registroType.fk_Formato_Salida = CShort(3)
                        registroType.Usa_Folder_Unico = False
                        registroType.Usa_File_Unico = False
                        registroType.fk_Entidad_Servidor = CShort(1)
                        registroType.fk_Servidor = CShort(1)
                        registroType.Captura_Llaves_Paquete = False

                        dbmImaging.SchemaConfig.TBL_Proyecto.DBInsert(registroType)
                        dbmImaging.Transaction_Commit()
                    Catch
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                        Throw
                    End Try
                End If

                Dim dtProyecto = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Miharu.Imaging.Library.Program.ImagingGlobal.Entidad, Miharu.Imaging.Library.Program.ImagingGlobal.Proyecto)
                If dtProyecto.Count > 0 Then
                    Miharu.Imaging.Library.Program.ImagingGlobal.ProyectoImagingRow = dtProyecto(0).ToCTA_ProyectoSimpleType()

                    'Se obtienen las llaves
                    Dim listaLlaves As New List(Of DesktopConfig.LlaveProyecto)

                    Dim proyectoIdLlaveEnumList As New CTA_Config_Proyecto_LlaveEnumList()
                    proyectoIdLlaveEnumList.Add(CTA_Config_Proyecto_LlaveEnum.id_Proyecto_Llave, True)

                    Dim dtProyectoLlaves = dbmImaging.SchemaCore.CTA_Config_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Miharu.Imaging.Library.Program.ImagingGlobal.Entidad, CShort(Miharu.Imaging.Library.Program.ImagingGlobal.Proyecto), 0, proyectoIdLlaveEnumList)
                    If dtProyectoLlaves.Count > 0 Then
                        For Each row In dtProyectoLlaves
                            Dim llave As DesktopConfig.LlaveProyecto
                            llave.Id = CShort(row.id_Proyecto_Llave)
                            llave.Nombre = row.Nombre_Proyecto_Llave
                            llave.Tipo = CType(row.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            listaLlaves.Add(llave)
                        Next
                    End If
                    Miharu.Imaging.Library.Program.ImagingGlobal.LLavesProyecto = listaLlaves
                End If

                Try
                    Dim proyectoImagingDataTable = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Miharu.Imaging.Library.Program.ImagingGlobal.Entidad, Miharu.Imaging.Library.Program.ImagingGlobal.Proyecto)

                    If (proyectoImagingDataTable.Count > 0) Then
                        'DesktopMessageBoxControl.DesktopMessageShow("No se encontró parametrización para la mesa de control de imágenes", "Mesa Control Imagenes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Miharu.Imaging.Library.Program.ImagingGlobal.ProyectoImagingRow = proyectoImagingDataTable(0).ToCTA_ProyectoSimpleType()

                        If proyectoImagingDataTable(0).Captura_Llaves_Paquete Then
                            Dim proyectoLlavePaqueteDataTable = dbmImaging.SchemaProcess.CTA_Proyecto_Llave_Paquete.DBFindByfk_Entidadfk_Proyecto(proyectoImagingDataTable(0).fk_Entidad, proyectoImagingDataTable(0).fk_Proyecto)
                            If proyectoLlavePaqueteDataTable.Count = 0 Then
                                DesktopMessageBoxControl.DesktopMessageShow("El proyecto maneja llaves por paquete, pero no se encontro configuración de estas.", "Llaves por Paquete", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            Else
                                Miharu.Imaging.Library.Program.ImagingGlobal.ProyectoImagingLlaveDataTable = proyectoLlavePaqueteDataTable
                            End If
                        End If
                    End If

                    Dim esquemasDataTable = dbmImaging.SchemaCore.CTA_Esquema.DBFindByfk_Entidadfk_Proyecto(Entidad, Proyecto)
                    Miharu.Imaging.Library.Program.ImagingGlobal.Esquemas = esquemasDataTable

                Catch
                    Throw
                End Try

                Dim tipologiasDataTable = dbmImaging.SchemaProcess.CTA_Tipologias_Esquema.DBFindByfk_entidadfk_Proyectoid_Esquema(Entidad, Proyecto, Nothing)
                Miharu.Imaging.Library.Program.ImagingGlobal.Tipologias = tipologiasDataTable

                Me.DialogResult = DialogResult.OK
                Me.Close()

                If SeleccionfechasRadioButton.Checked Then
                    Dim DatatableExpedientes As DataTable = Nothing
                    Dim DataTableExportacionFolders As DataTable = Nothing
                    Dim DataTableExportacion_Data As DataTable = Nothing
                    Dim DataTableExportacion_Validaciones As DataTable = Nothing
                    Dim FormExportCredivalores = New Procesos.Exportar.FormExportCredivalores(Entidad, Proyecto, NombreProyecto, DatatableExpedientes, False)
                    FormExportCredivalores.ShowDialog()
                End If
                If CargarLogRadioButton.Checked Then
                    Dim DataTableExpediente As DataTable = Nothing
                    Dim objCargueCredivalores As New FormCargueCredivalores(Entidad, Proyecto, NombreProyecto)
                    objCargueCredivalores.ShowDialog()
                End If
                If CargarLogLlavesRadioButton.Checked Then
                    Dim DataTableExpediente As DataTable = Nothing
                    Dim objCargueCredivaloresLLaves As New FormExportClientesEnLog(Entidad, Proyecto, NombreProyecto)
                    objCargueCredivaloresLLaves.ShowDialog()
                End If

            Catch ex As Exception
                Me.DialogResult = DialogResult.Cancel
                DesktopMessageBoxControl.DesktopMessageShow("Aceptar", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

        End If
    End Sub


    Private Sub EntidadComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles EntidadComboBox.SelectedIndexChanged
        Load_Proyectos()
    End Sub
    Private Sub Load_Proyectos()
        Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
        ProyectoComboBox.ValueMember = "id_proyecto"
        ProyectoComboBox.DisplayMember = "nombre_proyecto"
        ProyectoComboBox.DataSource = dbmImaging.SchemaSecurity.CTA_Entidad_Proyecto_Rol_Usuario.DBFindByfk_EntidadAplica_FisicoAplica_Imagenfk_Usuario(CShort(EntidadComboBox.SelectedValue), Nothing, True, Program.Sesion.Usuario.id)
        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
    End Sub
    Private Function getProyectos(Entidad As Short) As DataTable
        Dim ListProyectos As New List(Of String)
        Dim dt As DataTable = Nothing
        Dim dv As DataView = Nothing
        Dim dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        dbmCore.Connection_Open(Program.Sesion.Usuario.id)
        Try
            Dim conn As New SqlConnection
            conn.ConnectionString = (dbmCore.DataBase.ConnectionString)
            Dim sqlquery As String = "  select id_Proyecto, Nombre_Proyecto from [DB_Miharu.Core].[Config].[TBL_Proyecto]   WHERE fk_entidad = @fk_entidad group by id_Proyecto, Nombre_Proyecto;"
            Dim SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@fk_entidad", Entidad)
            }
            dt = ExecuteQuery(sqlquery, conn, SqlParameter)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
        End Try
        Return dt
    End Function
    Private Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim dt As DataTable = Nothing
        Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
            Try
                dt = New DataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(dt)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (da IsNot Nothing) Then da.SelectCommand.Connection.Close()
            End Try
            Return dt
        End Using
    End Function
#End Region

End Class