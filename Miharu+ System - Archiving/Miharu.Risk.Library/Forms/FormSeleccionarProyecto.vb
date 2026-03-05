Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Namespace Forms

    Public Class FormSeleccionarProyecto
        Inherits Form

#Region " Eventos "

        Private Sub ProyectoDataGridView_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles ProyectoDataGridView.DoubleClick
            Aceptar()
        End Sub

        Private Sub FormSeleccionarProyecto_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                EntidadComboBox.ValueMember = "id_entidad"
                EntidadComboBox.DisplayMember = "nombre_entidad"

                Dim Orden As New DBArchiving.SchemaSecurity.CTA_EntidadEnumList
                Orden.Add(DBArchiving.SchemaSecurity.CTA_EntidadEnum.Nombre_Entidad, True)
                EntidadComboBox.DataSource = dbmArchiving.SchemaSecurity.CTA_Entidad.DBGet(0, Orden)

                ProyectoDataGridView.AutoGenerateColumns = False

                'Dim Proyecto = dbmArchiving.Schemadbo.CTA_Proyectos_Vigentes.DBFindByfk_Entidad(CShort(EntidadComboBox.SelectedValue))
                Dim Proyecto = dbmArchiving.SchemaSecurity.CTA_Entidad_Proyecto_Rol_Usuario.DBFindByfk_Entidadfk_Usuario(CShort(EntidadComboBox.SelectedValue), Program.Sesion.Usuario.id)
                ProyectoDataGridView.DataSource = Proyecto

                EntidadComboBox.Focus()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally
                dbmArchiving.Connection_Close()
            End Try
        End Sub

        Private Sub EntidadComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            Dim dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            ProyectoDataGridView.AutoGenerateColumns = False
            'ProyectoDataGridView.DataSource = dbmArchiving.Schemadbo.CTA_Proyectos_Vigentes.DBFindByfk_Entidad(CShort(EntidadComboBox.SelectedValue))
            ProyectoDataGridView.DataSource = dbmArchiving.SchemaSecurity.CTA_Entidad_Proyecto_Rol_Usuario.DBFindByfk_Entidadfk_Usuario(CShort(EntidadComboBox.SelectedValue), Program.Sesion.Usuario.id)

            dbmArchiving.Connection_Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Aceptar()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Cancelar()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Cancelar()
            Try
                'Cuando se cancela, se limpian las variables dle proceso.
                'Program.RiskGlobal = Nothing
                Me.Dispose()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cancelar", ex)
            End Try
        End Sub

        Private Sub Aceptar()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Dim nEntidad = CShort(EntidadComboBox.SelectedValue)
            Dim nProyecto = CShort(ProyectoDataGridView.SelectedRows(0).Cells("Id_Proyecto").Value)

            Try
                If Validar() Then
                    Program.RiskGlobal = New RiskGlobal()

                    Program.RiskGlobal.Entidad = nEntidad
                    Program.RiskGlobal.Proyecto = nProyecto

                    Dim TableProyecto = dbmArchiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidadid_Proyecto(nEntidad, nProyecto)

                    If TableProyecto.Count = 0 Then
                        Try
                            dbmArchiving.Transaction_Begin()

                            Dim Registro As New DBArchiving.SchemaConfig.TBL_ProyectoType
                            Registro.fk_Entidad = nEntidad
                            Registro.fk_Proyecto = nProyecto
                            Registro.fk_Modulo = DesktopConfig.Modulo.Archiving
                            Registro.Usa_Cargue_Parcial = False
                            Registro.Usa_Cargue_Universal = False
                            Registro.Usa_Mesa_Control_Imagenes = False
                            Registro.Usa_Mesa_Control_Imagenes = False
                            Registro.Usa_Custodia_Externa = False
                            Registro.Usa_Validacion_Destape = True
                            Registro.Usa_Empaque_Adicion = False
                            Registro.Usa_Tabla_Fisico = False
                            Registro.Usa_Tabla_Faltante_Logico = False

                            dbmArchiving.SchemaConfig.TBL_Proyecto.DBInsert(Registro)
                            dbmArchiving.Transaction_Commit()
                        Catch ex As Exception
                            dbmArchiving.Transaction_Rollback()
                        End Try
                    End If

                    Dim dtProyecto = dbmArchiving.Schemadbo.CTA_Proyecto.DBFindByfk_Entidadid_Proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)
                    If dtProyecto.Count > 0 Then
                        Program.RiskGlobal.ProyectoRow = dtProyecto(0)

                        If TableProyecto.Rows.Count = 0 Then
                            Program.RiskGlobal.CargueUniversal = False
                            Program.RiskGlobal.CargueParcial = False
                            Program.RiskGlobal.Usa_Mesa_Control_Imagenes = False
                        Else
                            Program.RiskGlobal.CargueUniversal = dtProyecto(0).Usa_cargue_universal
                            Program.RiskGlobal.CargueParcial = dtProyecto(0).usa_cargue_parcial
                            Program.RiskGlobal.Usa_Mesa_Control_Imagenes = dtProyecto(0).Usa_Mesa_Control_Imagenes
                        End If

                        Program.RiskGlobal.Folder_Tipo = dtProyecto(0).fk_Folder_Tipo
                        Program.RiskGlobal.CajaXDefecto = dtProyecto(0).fk_Caja_Defecto

                        Try
                            Program.RiskGlobal.Usa_Cargue_Carpeta = dtProyecto(0).Usa_Solo_Cargue_Carpeta
                        Catch : End Try

                        Try
                            Program.RiskGlobal.Usa_Tabla_Fisico = dtProyecto(0).Usa_Tabla_Fisico
                        Catch : End Try

                        Try
                            Program.RiskGlobal.Usa_Validacion_Destape = dtProyecto(0).Usa_Validacion_Destape
                        Catch : End Try

                        Try
                            Program.RiskGlobal.Usa_Empaque_Adicion = dtProyecto(0).Usa_Empaque_Adicion
                        Catch : End Try

                        Try
                            Program.RiskGlobal.EntidadCustodia = Program.DesktopGlobal.BovedaRow.fk_Entidad
                        Catch : End Try

                        Try
                            Program.RiskGlobal.SedeCustodia = Program.DesktopGlobal.BovedaRow.fk_Sede
                        Catch : End Try

                        Try
                            Program.RiskGlobal.BovedaCustodia = Program.DesktopGlobal.BovedaRow.id_Boveda
                        Catch : End Try


                        If Program.RiskGlobal.EntidadCustodia Is Nothing Or Program.RiskGlobal.SedeCustodia Is Nothing Or Program.RiskGlobal.BovedaCustodia Is Nothing Then
                            DesktopMessageBoxControl.DesktopMessageShow("No se ha configurado la bóveda para el centro de procesamiento", "Parámetros del Sistema", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Return
                        End If

                        'Se obtienen las llaves
                        Dim ListaLlaves As New List(Of DesktopConfig.LlaveProyecto)

                        Dim ProyectoIdLlaveOrder As New DBArchiving.Schemadbo.CTA_Proyecto_LlavesEnumList()
                        ProyectoIdLlaveOrder.Add(DBArchiving.Schemadbo.CTA_Proyecto_LlavesEnum.id_Proyecto_Llave, True)

                        Dim dtProyectoLlaves = dbmArchiving.Schemadbo.CTA_Proyecto_Llaves.DBFindByfk_Entidadfk_Proyecto(Program.RiskGlobal.Entidad, CShort(Program.RiskGlobal.Proyecto), 0, ProyectoIdLlaveOrder)
                        If dtProyectoLlaves.Count > 0 Then
                            For Each row In dtProyectoLlaves
                                Dim Llave As DesktopConfig.LlaveProyecto
                                Llave.Id = CShort(row.id_Proyecto_Llave)
                                Llave.Nombre = row.Nombre_Proyecto_Llave
                                Llave.Tipo = CType(row.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                                ListaLlaves.Add(Llave)
                            Next
                        End If
                        Program.RiskGlobal.LLavesProyecto = ListaLlaves
                    End If

                    Dim TableNombreEntidad = dbmArchiving.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(CShort(EntidadComboBox.SelectedValue))
                    Program.RiskGlobal.NombreEntidad = TableNombreEntidad(0).Nombre_Entidad

                    Dim TableNombreProyecto = dbmArchiving.SchemaCore.CTA_Proyecto.DBFindByfk_Entidadid_Proyecto(CShort(EntidadComboBox.SelectedValue), CShort(ProyectoDataGridView.SelectedRows(0).Cells("Id_Proyecto").Value))
                    Program.RiskGlobal.NombreProyecto = TableNombreProyecto(0).Nombre_Proyecto

                    ' Carga Parametros Mesa de control fisicos

                    Program.RiskGlobal.ValidacionesMesa = dbmArchiving.SchemaCore.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(Nothing, DBArchiving.EnumEtapaCaptura.Mesa_Control)
                    Program.RiskGlobal.DatosMesa = dbmArchiving.Schemadbo.CTA_Campos_Documentos_Mesa.DBFindByfk_Entidadfk_Proyecto(CShort(EntidadComboBox.SelectedValue), CShort(ProyectoDataGridView.SelectedRows(0).Cells("Id_Proyecto").Value))

                    ' Si se usa mesa de control imágenes carga la parametrización
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Try
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                        Dim ProyectoImagingDataTable = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                        If (ProyectoImagingDataTable.Count = 0) Then
                            If (Program.RiskGlobal.Usa_Mesa_Control_Imagenes) Then
                                DesktopMessageBoxControl.DesktopMessageShow("No se encontró parametrización para la mesa de control de imágenes", "Mesa Control Imagenes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                            Else
                                Program.RiskGlobal.ProyectoImagingRow = Nothing
                            End If
                        Else
                            Program.RiskGlobal.ProyectoImagingRow = ProyectoImagingDataTable(0)
                        End If
                    Catch
                        Throw
                    Finally
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    'Se almacena si debe utilizar Centro de distribución.
                    If Program.RiskGlobal.SedeCustodia Is Nothing OrElse Program.RiskGlobal.SedeCustodia.IsDbNull OrElse Program.RiskGlobal.SedeCustodia.IsNull Then
                        Program.RiskGlobal.UsaCentroDistribucion = True
                    Else
                        If Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede = CShort(Program.RiskGlobal.SedeCustodia) Then
                            Program.RiskGlobal.UsaCentroDistribucion = False
                        Else
                            Program.RiskGlobal.UsaCentroDistribucion = True
                        End If
                    End If

                    Dim Esquemas = dbmArchiving.Schemadbo.CTA_Esquema.DBFindByfk_entidadfk_proyecto(nEntidad, nProyecto)
                    Program.RiskGlobal.Esquemas = Esquemas

                    Dim Tipologias = dbmArchiving.Schemadbo.CTA_Tipologias_Esquema.DBFindByfk_entidadfk_proyectofk_esquema(nEntidad, nProyecto, Nothing)
                    Program.RiskGlobal.Tipologias = Tipologias

                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Debe seleccionar una entidad y un proyecto.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    EntidadComboBox.Focus()
                End If
            Catch ex As Exception
                Me.DialogResult = DialogResult.Cancel
                DesktopMessageBoxControl.DesktopMessageShow("Aceptar", ex)
            End Try

            dbmArchiving.Connection_Close()
        End Sub

#End Region

#Region " Funciones "
        Public Function Validar() As Boolean
            Dim bReturn As Boolean = True
            Try
                If EntidadComboBox.SelectedValue.ToString = "" Then
                    bReturn = False
                ElseIf ProyectoDataGridView.SelectedRows.Count < 1 Then
                    bReturn = False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Validar", ex)
            End Try
            Return bReturn
        End Function
#End Region

        Public Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub
    End Class

End Namespace