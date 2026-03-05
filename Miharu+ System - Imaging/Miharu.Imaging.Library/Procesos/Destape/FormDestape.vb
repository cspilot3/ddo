Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Procesos.Destape
    Public Class FormDestape
        Inherits FormBase

#Region " Declaraciones "

        Private _CamposDinamicosPrecinto As New List(Of DesktopConfig.CrearControlesType)
        Private _CamposDinamicosContenedor As New List(Of DesktopConfig.CrearControlesType)
        Private _CamposDinamicosPrecintoTableForm As Panel
        Private _CamposDinamicosContenedorTableForm As Panel
        Private _ContenedoresListBox_IgnoreSelectedIndexChanged As Boolean = False
        Private _DocumentosListBox_IgnoreSelectedIndexChanged As Boolean = False

#End Region

#Region " Propiedades "

        Public Property IdOT() As Integer
        Public Property IdPrecinto() As Short
        Public Property IdContenedor() As Short

        Public Property EventManager As EventManager

#End Region

#Region " Eventos "

        Private Sub FormDestape_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            DocumentoGroupBox.Visible = Not (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor)
            DocumentosDataGridView.AutoGenerateColumns = False
            ContenedoresDataGridView.AutoGenerateColumns = False

            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                DocumentoGroupBox.Visible = False
                ContenedorAbrirButton.Visible = False
                ContenedorCerrarButton.Visible = False
                DocumentoMenuPanel.Enabled = False
            End If

            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Cantidades_Enviadas_Recibidas) Then
                Me.CantidadDocumentosEnviadosLabel.Visible = True
                Me.CantidadDocumentosEnviadosTextBox.Visible = True
                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                    Me.CantidadDocumentosRecibidosLabel.Visible = True
                    Me.CantidadDocumentosRecibidosTextBox.Visible = True
                End If
                If (Program.ImagingGlobal.ProyectoImagingRow.Muestra_Cantidad_Recibida) Then
                    Me.CantidadDocumentosRecibidosLabel.Visible = True
                    Me.CantidadDocumentosRecibidosTextBox.Visible = True
                End If
            End If

            Contenedor_CargarEsquemas()
            Precinto_DeshabilitarCampos()
            Precinto_CrearCamposDinamicos()
            Contenedor_CrearCamposDinamicos()

            IniciarKeyManager(Me)

            If (IdPrecinto <> 0) Then
                Precinto_CargarDataExistenteCamposDinamicos(Nothing)
                Contenedor_CargarContenedoresExistentes()
                Contenedor_Iniciar()

                PrecintoEliminarButton.Enabled = True
            Else
                PrecintoEliminarButton.Enabled = False
            End If
        End Sub

        Private Sub KeyManager_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs)
            If (Not e.Control And Not e.Shift) Then
                Select Case e.KeyCode
                    Case Keys.F2 : Precinto_Guardar()
                    Case Keys.F3 : Contenedor_Guardar()
                    Case Keys.F4 : Documento_Guardar()
                    Case Keys.F5 : Contenedor_CerrarContenedor()
                    Case Keys.F6 : Precinto_Finalizar()
                End Select
            End If
            If (e.Control And Not e.Shift) Then
                Select Case e.KeyCode
                    Case Keys.F6 : Precinto_Abrir()
                    Case Keys.F5 : Contenedor_AbrirContenedor()
                    Case Keys.F3 : Contenedor_NuevoContenedor()
                    Case Keys.F4 : Documento_NuevoDocumento()
                End Select
            End If
        End Sub

        Private Sub FormDestape_SizeChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
            PrecintoCamposDinamicosPanel.Refresh()
            ContenedorCamposDinamicosPanel.Refresh()
        End Sub

        Private Sub PrecintoTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles PrecintoNumeroTextBox.KeyDown
            If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab) Then
                Precinto_Validar(PrecintoNumeroTextBox.Text)
            End If
        End Sub

        Private Sub PrecintoValidarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoValidarButton.Click
            Precinto_Validar(PrecintoNumeroTextBox.Text)
        End Sub

        Private Sub PrecintoInsertarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoInsertarButton.Click
            Precinto_Guardar()
        End Sub

        Private Sub PrecintoAbrirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoAbrirButton.Click
            Precinto_Abrir()
        End Sub

        Private Sub PrecintoFinalizarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoFinalizarButton.Click
            Precinto_Finalizar()
        End Sub

        Private Sub PrecintoEliminarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoEliminarButton.Click
            Precinto_Eliminar()
        End Sub

        Private Sub PrecintoLastCampoDinamico_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.PrecintoMenuPanel.Focus()
            End If
        End Sub

        Private Sub PrecintoGroupBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoGroupBox.Enter
            MostrarMenu(PrecintoMenuPanel)
        End Sub

        Private Sub ContenedorInsertarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorInsertarButton.Click
            Contenedor_Guardar()
        End Sub

        Private Sub ContenedorNuevoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorNuevoButton.Click
            Contenedor_NuevoContenedor()
        End Sub

        Private Sub ContenedorAbrirButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorAbrirButton.Click
            Contenedor_AbrirContenedor()
        End Sub

        Private Sub ContenedorCerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorCerrarButton.Click
            Contenedor_CerrarContenedor()
        End Sub

        Private Sub ContenedorEliminarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorEliminarButton.Click
            Contenedor_Eliminar()
        End Sub

        Private Sub ContenedoresDataGridView_MouseDoubleClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ContenedoresDataGridView.MouseDoubleClick
            Contenedor_Seleccionar()
        End Sub

        Private Sub ContenedorLastCampoDinamico_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.ContenedorMenuPanel.Focus()
            End If
        End Sub

        Private Sub ContenedorCodigoTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles ContenedorTokenTextBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                    ContenedorEsquemaComboBox_KeyPress(sender, e)
                Else
                    Me.ContenedorEsquemaComboBox.Focus()
                End If

            End If
        End Sub

        Private Sub ContenedorEsquemaComboBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles ContenedorEsquemaComboBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True

                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Cantidades_Enviadas_Recibidas) Then
                    Me.CantidadDocumentosEnviadosTextBox.Focus()
                Else
                    Me.ContenedorCamposDinamicosPanel.Focus()
                End If
            End If
        End Sub

        Private Sub CantidadDocumentosEnviadosTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles CantidadDocumentosEnviadosTextBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True

                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Or (Program.ImagingGlobal.ProyectoImagingRow.Muestra_Cantidad_Recibida) Then
                    Me.CantidadDocumentosRecibidosTextBox.Focus()
                Else
                    Me.ContenedorCamposDinamicosPanel.Focus()
                End If
            End If
        End Sub

        Private Sub CantidadDocumentosRecibidosTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles CantidadDocumentosRecibidosTextBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True

                Me.ContenedorCamposDinamicosPanel.Focus()

            End If
        End Sub

        Private Sub ContenedorDatosGroupBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorDatosGroupBox.Enter
            MostrarMenu(ContenedorMenuPanel)
        End Sub

        Private Sub DocumentoInsertarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentoInsertarButton.Click
            Documento_Guardar()
        End Sub

        Private Sub DocumentoNuevoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentoNuevoButton.Click
            Documento_NuevoDocumento()
        End Sub

        Private Sub DocumentoEliminarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentoEliminarButton.Click
            Documento_Eliminar()
        End Sub

        Private Sub DocumentoTokenTextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles DocumentoTokenTextBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.DocumentoTipoComboBox.Focus()
            End If
        End Sub

        Private Sub DocumentoTipoComboBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles DocumentoTipoComboBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.DocumentoMenuPanel.Focus()
            End If
        End Sub

        Private Sub DocumentosDataGridView_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentosDataGridView.DoubleClick
            Documento_Seleccionar()
        End Sub

        Private Sub DocumentoGroupBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles DocumentoGroupBox.Enter
            MostrarMenu(DocumentoMenuPanel)
        End Sub

#End Region

#Region " Metodos "

        Private Sub MostrarMenu(ByVal nMenu As Panel)
            For Each m In MenuPanel.Controls
                Dim menuItem = CType(m, Panel)
                menuItem.Visible = (menuItem.Equals(nMenu))
            Next
        End Sub

        Private Sub Precinto_CrearCamposDinamicos()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim lastIdCampo As Short = 0

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim tableCampos = dbmImaging.SchemaConfig.TBL_Precinto_Campo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)
                If tableCampos.Count > 0 Then
                    For Each row In tableCampos
                        If row.Eliminado = False Then
                            Dim campoControl As New DesktopConfig.CrearControlesType
                            With campoControl
                                .fk_Entidad = row.fk_Entidad
                                .fk_Campo = row.id_Campo
                                .Label = row.Nombre_Campo
                                .MinLength = row.Length_Min_Campo
                                .MaxLength = row.Length_Campo
                                .NombreCampo = row.Nombre_Campo
                                .Tipo = CType(row.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                                .Width = 200
                                .LabelWidth = 200
                                .Usa_Decimales = row.Usa_Decimales
                                .Cantidad_Decimales = row.Cantidad_Decimales
                                .Obligatorio = row.Es_Obligatorio_Campo
                            End With
                            If (Not row.Isfk_Campo_ListaNull()) Then
                                campoControl.CampoLista = row.fk_Campo_Lista
                            End If
                            lastIdCampo = row.id_Campo
                            _CamposDinamicosPrecinto.Add(campoControl)
                        End If
                    Next

                End If
                _CamposDinamicosPrecintoTableForm = Utilities.CreaControlesImaging(dbmImaging, _CamposDinamicosPrecinto, Program.DesktopGlobal.ConnectionStrings, Program.ImagingGlobal, True, 213, True)
                _CamposDinamicosPrecintoTableForm.AutoScroll = True
                _CamposDinamicosPrecintoTableForm.Dock = DockStyle.Fill
                PrecintoCamposDinamicosPanel.Controls.Add(_CamposDinamicosPrecintoTableForm)
                PrecintoCamposDinamicosPanel.Refresh()

                Dim lastCampo = Precinto_BuscarCampoDinamico(lastIdCampo)
                If (lastCampo IsNot Nothing) Then AddHandler lastCampo.KeyPress, AddressOf PrecintoLastCampoDinamico_KeyPress


            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_CargarDataExistenteCamposDinamicos(ByVal nPrecintoInfo As DBImaging.SchemaProcess.TBL_PrecintoType)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If (nPrecintoInfo Is Nothing) Then
                    Dim dataPrecinto = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(IdOT, IdPrecinto)
                    If (dataPrecinto.Count = 0) Then Throw New Exception("Id de precinto no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdPrecinto)
                    nPrecintoInfo = dataPrecinto(0).ToTBL_PrecintoType
                End If

                Me.PrecintoNumeroTextBox.Text = nPrecintoInfo.Precinto
                Me.PrecintoNumeroTextBox.ReadOnly = True

                Me.PrecintoCerradoLabel.Visible = nPrecintoInfo.Cerrado

                Me.PrecintoAbrirButton.Enabled = nPrecintoInfo.Cerrado
                Me.PrecintoFinalizarButton.Enabled = Not Me.PrecintoAbrirButton.Enabled

                PrecintoEliminarButton.Enabled = True

                Dim tableCamposData = dbmImaging.SchemaProcess.TBL_Precinto_Data.DBGet(IdOT, IdPrecinto, Nothing)
                For Each row In tableCamposData
                    Dim campoPrecintoData = Precinto_BuscarCampoDinamico(row.fk_Campo)
                    Select Case campoPrecintoData.GetType()
                        Case GetType(DesktopTextBox.DesktopTextBoxControl)
                            CType(campoPrecintoData, DesktopTextBox.DesktopTextBoxControl).Text = row.Data_Campo.ToString
                        Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                            CType(campoPrecintoData, DesktopCheckBox.DesktopCheckBoxControl).Checked = CBool(row.Data_Campo)
                        Case GetType(DesktopComboBox.DesktopComboBoxControl)
                            CType(campoPrecintoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue = row.Data_Campo
                    End Select
                Next

                Me.PrecintoCamposDinamicosPanel.Enabled = True
                Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_Validar(ByVal NroPrecinto As String)
            If (PrecintoNumeroTextBox.ReadOnly) Then
                PrecintoCamposDinamicosPanel.Focus()
                Return
            End If

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim PrecintoDataTable = dbmImaging.SchemaProcess.PA_Get_Precinto.DBExecute(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, NroPrecinto)

                If PrecintoDataTable.Rows.Count = 0 Then

                    Dim dataPrecinto = dbmImaging.SchemaProcess.TBL_Precinto.DBFindByfk_OTPrecinto(IdOT, NroPrecinto)

                    If (dataPrecinto.Rows.Count = 0 OrElse Not dataPrecinto(0).Cerrado) Then
                        PrecintoCamposDinamicosPanel.Enabled = True
                        PrecintoNumeroTextBox.ReadOnly = True
                    End If

                    If (dataPrecinto.Rows.Count = 0) Then
                        Contenedor_LimpiarControles()
                        Documento_LimpiarControles()
                    Else
                        IdPrecinto = dataPrecinto(0).id_Precinto
                        Precinto_CargarDataExistenteCamposDinamicos(dataPrecinto(0).ToTBL_PrecintoType)
                        Contenedor_CargarContenedoresExistentes()
                    End If

                    Me.PrecintoInsertarButton.Enabled = Not (dataPrecinto.Rows.Count > 0 AndAlso dataPrecinto(0).Cerrado)

                    If (Not Me.PrecintoInsertarButton.Enabled) Then Throw New ApplicationException("El precinto ya se encuentra cerrado")

                    Dim primerCampo = Precinto_BuscarCampoDinamico(1)
                    If (primerCampo IsNot Nothing) Then primerCampo.Select()

                Else
                    Me.PrecintoInsertarButton.Enabled = False
                    Me.PrecintoFinalizarButton.Enabled = False
                    Throw New ApplicationException("El precinto ya se encuentra destapado")
                End If


            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_Guardar()
            If (Not PrecintoInsertarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try

                If (PrecintoNumeroTextBox.Text.Trim() = "") Then
                    PrecintoNumeroTextBox.Focus()
                    Throw New ApplicationException("El código del precinto no puede estar vacio")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim PrecintoDataTable = dbmImaging.SchemaProcess.PA_Get_Precinto.DBExecute(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, PrecintoNumeroTextBox.Text)

                If PrecintoDataTable.Rows.Count = 0 Then

                    Dim esInsertar = Me.PrecintoInsertarButton.Text.StartsWith("Insertar")

                    If (esInsertar) Then
                        IdPrecinto = dbmImaging.SchemaProcess.TBL_Precinto.DBNextId(IdOT)

                        Dim nuevoPrecinto = New DBImaging.SchemaProcess.TBL_PrecintoType With {
                            .fk_OT = IdOT,
                            .id_Precinto = IdPrecinto,
                            .Precinto = PrecintoNumeroTextBox.Text,
                            .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                            .Fecha_Apertura = SlygNullable.SysDate,
                            .Cerrado = False,
                            .fk_Puesto_Trabajo = Program.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo
                        }

                        dbmImaging.SchemaProcess.TBL_Precinto.DBInsert(nuevoPrecinto)


                    Else
                        Dim precintoInfoData = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(IdOT, IdPrecinto)
                        If (precintoInfoData.Count = 0) Then Throw New Exception("Id de precinto no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdPrecinto)
                    End If

                    Dim precintoDimData = New DBImaging.SchemaProcess.TBL_Precinto_DataType With {
                        .fk_OT = IdOT,
                        .fk_Precinto = IdPrecinto,
                        .fk_Entidad = Program.ImagingGlobal.Entidad,
                        .fk_Proyecto = Program.ImagingGlobal.Proyecto
                    }

                    For Each campoPrecintoData As Control In _CamposDinamicosPrecintoTableForm.Controls
                        If Not (campoPrecintoData.GetType().ToString() = "System.Windows.Forms.Label") Then
                            precintoDimData.fk_Campo = CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).fk_Campo

                            Select Case campoPrecintoData.GetType()
                                Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                    precintoDimData.Data_Campo = CType(campoPrecintoData, DesktopTextBox.DesktopTextBoxControl).Text
                                Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                    precintoDimData.Data_Campo = CType(campoPrecintoData, DesktopCheckBox.DesktopCheckBoxControl).Checked
                                Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                    precintoDimData.Data_Campo = CType(campoPrecintoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue
                            End Select

                            If (esInsertar) Then
                                dbmImaging.SchemaProcess.TBL_Precinto_Data.DBInsert(precintoDimData)
                            Else
                                Dim data = dbmImaging.SchemaProcess.TBL_Precinto_Data.DBGet(precintoDimData.fk_OT, precintoDimData.fk_Precinto, precintoDimData.fk_Campo)
                                If (data.Count = 0) Then
                                    dbmImaging.SchemaProcess.TBL_Precinto_Data.DBInsert(precintoDimData)
                                Else
                                    dbmImaging.SchemaProcess.TBL_Precinto_Data.DBUpdate(precintoDimData, precintoDimData.fk_OT, precintoDimData.fk_Precinto, precintoDimData.fk_Campo)
                                End If
                            End If
                        End If
                    Next

                    dbmImaging.Transaction_Commit()

                    Contenedor_CargarContenedoresExistentes()
                    Contenedor_Iniciar()

                    If (Not esInsertar) Then
                        MessageBox.Show("Precinto Actualizado Con Exito", "Guardado", MessageBoxButtons.OK)
                    End If

                    Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"

                Else
                    Me.PrecintoInsertarButton.Enabled = False
                    Me.PrecintoFinalizarButton.Enabled = False
                    Throw New ApplicationException("El precinto ya se encuentra destapado")

                End If


            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_Eliminar()
            If (Not PrecintoEliminarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para eliminar el precinto", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceURL, Program.DesktopGlobal.ClientIPAddress)) Then
                    Throw New Exception("No se permitió eliminar el precinto")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                If (IdPrecinto = 0) Then Throw New Exception("Aún no se ha ingresado un precinto")

                Dim cargado = dbmImaging.SchemaProcess.PA_Precinto_Destape_Tiene_Imagenes_Cargadas.DBExecute(IdOT, IdPrecinto)
                If (cargado) Then Throw New ApplicationException("No se permite modificar el precinto puesto que ya se cargaron imagenes asociadas")

                dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(IdOT, IdPrecinto)

                dbmImaging.Transaction_Commit()

                MessageBox.Show("Precinto Eliminado Con Exito", "Guardado", MessageBoxButtons.OK)

                Close()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_Abrir()
            If (Not PrecintoAbrirButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para abrir el precinto", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceURL, Program.DesktopGlobal.ClientIPAddress)) Then
                    Throw New Exception("No se permitió abrir el precinto")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                If (IdPrecinto = 0) Then Throw New Exception("Aún no se ha ingresado un precinto")

                Dim data = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(IdOT, IdPrecinto)
                If (data.Count = 0) Then Throw New Exception("No se encontró el precinto " & PrecintoNumeroTextBox.Text)

                Dim cargado = dbmImaging.SchemaProcess.PA_Precinto_Destape_Tiene_Imagenes_Cargadas.DBExecute(IdOT, IdPrecinto)
                If (cargado) Then Throw New ApplicationException("No se permite modificar el precinto puesto que ya se cargaron imagenes asociadas")

                Dim precinto = New DBImaging.SchemaProcess.TBL_PrecintoType() With {
                    .Cerrado = False,
                    .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                    .Fecha_Apertura = SlygNullable.SysDate
                }

                dbmImaging.SchemaProcess.TBL_Precinto.DBUpdate(precinto, IdOT, IdPrecinto)

                dbmImaging.Transaction_Commit()

                MessageBox.Show("Se Abrío El Precinto Con Exito", "Guardado", MessageBoxButtons.OK)

                Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"
                Me.PrecintoInsertarButton.Enabled = True

                Me.PrecintoCerradoLabel.Visible = False
                Me.PrecintoFinalizarButton.Enabled = True
                Me.PrecintoAbrirButton.Enabled = False
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_Finalizar()
            If (Not PrecintoFinalizarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (IdPrecinto = 0) Then Throw New Exception("Aún no se ha ingresado un precinto")

                Dim dataCnt = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoDataTable)
                If (dataCnt.Count = 0) Then
                    Throw New ApplicationException("No se permite cerrar el precinto puesto que no se ha registrado ningún contenedor")
                End If

                If (DocumentoGroupBox.Visible) Then
                    Dim docData = dataCnt.Select("Cerrado=0")
                    If (docData.Length > 0) Then
                        Throw New ApplicationException("Primero debe cerrar todos los contenedores para cerrar el precinto")
                    End If
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim precintoData = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(IdOT, IdPrecinto)
                If (precintoData.Count = 0) Then Throw New Exception("No se encontró el precinto " & PrecintoNumeroTextBox.Text & " ID " & IdPrecinto)

                Dim cargado = dbmImaging.SchemaProcess.PA_Precinto_Destape_Tiene_Imagenes_Cargadas.DBExecute(IdOT, IdPrecinto)
                If (cargado) Then Throw New ApplicationException("No se permite modificar el precinto puesto que ya se cargaron imagenes asociadas")

                Dim precinto = New DBImaging.SchemaProcess.TBL_PrecintoType() With {
                    .Cerrado = True,
                    .fk_Usuario_Cierre = Program.Sesion.Usuario.id,
                    .Fecha_Cierre = SlygNullable.SysDate
                }

                dbmImaging.SchemaProcess.TBL_Precinto.DBUpdate(precinto, IdOT, IdPrecinto)

                'If (DocumentoGroupBox.Visible) Then
                Dim contenedor = New DBImaging.SchemaProcess.TBL_ContenedorType() With {
                                        .Cerrado = True,
                                        .fk_Usuario_Cierre = Program.Sesion.Usuario.id,
                                        .Fecha_Cierre = SlygNullable.SysDate
                                    }

                    dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedor, IdOT, IdPrecinto, Nothing)
                'End If

                dbmImaging.Transaction_Commit()

                MessageBox.Show("Precinto Cerrado Con Exito", "Guardado", MessageBoxButtons.OK)

                Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"
                Me.PrecintoInsertarButton.Enabled = False
                Me.PrecintoCerradoLabel.Visible = True
                Me.PrecintoFinalizarButton.Enabled = False
                Me.PrecintoAbrirButton.Enabled = True

                EventManager.FinalizarPrecinto()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_DeshabilitarCampos()
            Me.PrecintoAbrirButton.Enabled = False
            Me.PrecintoEliminarButton.Enabled = False

            Me.PrecintoCamposDinamicosPanel.Enabled = False

            Me.Contenedor_DeshabilitarCampos()
        End Sub

        Private Sub Contenedor_CrearCamposDinamicos()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim lastIdCampo As Short = 0

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim tableCampos = dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing, 0, New DBImaging.SchemaConfig.TBL_Contenedor_CampoEnumList(DBImaging.SchemaConfig.TBL_Contenedor_CampoEnum.Orden, True))
                ' If tableCampos.Count > 0 Then
                For Each row In tableCampos
                    If row.Eliminado = False Then
                        Dim campoControl As New DesktopConfig.CrearControlesType
                        With campoControl
                            .fk_Entidad = row.fk_Entidad
                            .fk_Campo = row.id_Campo
                            .Label = row.Nombre_Campo
                            .MinLength = row.Length_Min_Campo
                            .MaxLength = row.Length_Campo
                            .NombreCampo = row.Nombre_Campo
                            .Tipo = CType(row.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            .Width = 200
                            .LabelWidth = 200
                            .Usa_Decimales = row.Usa_Decimales
                            .Cantidad_Decimales = row.Cantidad_Decimales
                            .Obligatorio = row.Es_Obligatorio_Campo
                        End With
                        If (Not row.Isfk_Campo_ListaNull()) Then
                            campoControl.CampoLista = row.fk_Campo_Lista
                        End If
                        lastIdCampo = row.id_Campo
                        _CamposDinamicosContenedor.Add(campoControl)
                    End If
                Next

                _CamposDinamicosContenedorTableForm = Utilities.CreaControlesImaging(dbmImaging, _CamposDinamicosContenedor, Program.DesktopGlobal.ConnectionStrings, Program.ImagingGlobal, True, 213, True)
                _CamposDinamicosContenedorTableForm.AutoScroll = True
                _CamposDinamicosContenedorTableForm.Dock = DockStyle.Fill
                ContenedorCamposDinamicosPanel.Controls.Add(_CamposDinamicosContenedorTableForm)
                ContenedorCamposDinamicosPanel.Refresh()

                Dim lastCampo = Contenedor_BuscarCampoDinamico(lastIdCampo)
                If (lastCampo IsNot Nothing) Then AddHandler lastCampo.KeyPress, AddressOf ContenedorLastCampoDinamico_KeyPress
                'End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_CargarDataExistenteCamposDinamicos(ByVal nContenedorInfo As DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoType)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Me.ContenedorTokenTextBox.Text = nContenedorInfo.Token
                If (Me.CantidadDocumentosEnviadosTextBox.Visible) Then
                    Me.CantidadDocumentosEnviadosTextBox.Text = CStr(nContenedorInfo.cantidad_Enviados)
                End If

                If (Me.CantidadDocumentosRecibidosTextBox.Visible) Then
                    Me.CantidadDocumentosRecibidosTextBox.Text = CStr(nContenedorInfo.cantidad_Recibidos)
                End If

                If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                    Try : Me.ContenedorEsquemaComboBox.SelectedValue = nContenedorInfo.fk_Esquema : Catch : End Try
                End If

                Me.ContenedorTokenTextBox.ReadOnly = True
                If (Me.CantidadDocumentosRecibidosTextBox.Visible) Then
                    Me.CantidadDocumentosRecibidosTextBox.ReadOnly = True
                End If
                If (Me.CantidadDocumentosEnviadosTextBox.Visible) Then
                    Me.CantidadDocumentosEnviadosTextBox.ReadOnly = True
                End If

                Me.ContenedorEsquemaComboBox.Enabled = False

                'Dim tableCamposData = dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBGet(IdOT, IdPrecinto, IdContenedor, Nothing)
                Dim tableCamposData = dbmImaging.SchemaProcess.PA_Get_Data_Contenedor.DBExecute(IdOT, IdPrecinto, IdContenedor, False)

                For Each row In tableCamposData
                    Dim campoData = Contenedor_BuscarCampoDinamico(row.fk_Campo)
                    Select Case campoData.GetType()
                        Case GetType(DesktopTextBox.DesktopTextBoxControl)
                            CType(campoData, DesktopTextBox.DesktopTextBoxControl).Text = row.Data_Campo.ToString
                        Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                            CType(campoData, DesktopCheckBox.DesktopCheckBoxControl).Checked = CBool(row.Data_Campo)
                        Case GetType(DesktopComboBox.DesktopComboBoxControl)
                            CType(campoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue = row.Data_Campo
                    End Select
                Next

                Me.ContenedorCamposDinamicosPanel.Enabled = True
                Me.ContenedorInsertarButton.Text = "Actualizar Contenedor F3"

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_CargarContenedoresExistentes()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim contenedoresData = dbmImaging.SchemaProcess.CTA_Destape_Contenedor_Resumido.DBFindByfk_OTfk_Precintoid_Contenedor(IdOT, IdPrecinto, Nothing)

                _ContenedoresListBox_IgnoreSelectedIndexChanged = True
                ContenedoresDataGridView.ClearSelection()
                ContenedoresDataGridView.DataSource = contenedoresData
                CantContenedorLabel.Text = CType(contenedoresData.Count, String)

                For Each row As DataGridViewRow In ContenedoresDataGridView.Rows
                    If (CType(CType(row.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoRow).Cerrado) Then
                        row.DefaultCellStyle.BackColor = Drawing.Color.LightGreen
                    End If
                Next
                _ContenedoresListBox_IgnoreSelectedIndexChanged = False

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_CargarEsquemas()
            If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                Me.ContenedorEsquemaLabel.Visible = True
                Me.ContenedorEsquemaComboBox.Visible = True
                Me.Nombre_Esquema.Visible = True

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim esquemasData = dbmImaging.SchemaCore.CTA_Esquema.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                    Utilities.LlenarCombo(Me.ContenedorEsquemaComboBox, esquemasData, DBImaging.SchemaCore.CTA_EsquemaEnum.id_Esquema.ColumnName, DBImaging.SchemaCore.CTA_EsquemaEnum.Nombre_Esquema.ColumnName)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            Else
                Me.ContenedorEsquemaLabel.Visible = False
                Me.ContenedorEsquemaComboBox.Visible = False
                Me.Nombre_Esquema.Visible = False
            End If
        End Sub

        Private Sub Contenedor_Iniciar()

            Me.Contenedor_LimpiarControles()

            If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                Try : ContenedorEsquemaComboBox.SelectedIndex = 0 : Catch : End Try
            End If

            ContenedorEsquemaComboBox.Enabled = True

            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Codigo_Contenedor) Then
                ContenedorTokenTextBox.Text = ""
                ContenedorTokenTextBox.ReadOnly = False
                ContenedorTokenTextBox.Focus()
            Else
                ContenedorTokenTextBox.Text = "[AUTO]"
                ContenedorTokenTextBox.ReadOnly = True

                If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                    ContenedorEsquemaComboBox.Focus()
                End If
            End If

            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Cantidades_Enviadas_Recibidas) Then
                Me.CantidadDocumentosEnviadosTextBox.ReadOnly = False
                Me.CantidadDocumentosEnviadosTextBox.Text = ""
                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                    Me.CantidadDocumentosRecibidosTextBox.ReadOnly = False
                    Me.CantidadDocumentosRecibidosTextBox.Text = ""
                End If
                If (Program.ImagingGlobal.ProyectoImagingRow.Muestra_Cantidad_Recibida) Then
                    Me.CantidadDocumentosRecibidosTextBox.ReadOnly = False
                    Me.CantidadDocumentosRecibidosTextBox.Text = ""
                End If
            End If

            Documento_LimpiarControles()
            Documento_SetDataSourceDocumentos(New DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)
            DocumentosDataGridView.Enabled = False
        End Sub

        Private Sub Contenedor_Guardar()
            If (Not ContenedorInsertarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim _nidContenedor As Integer = 0

            Try
                If (PrecintoCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite guardar el contenedor puesto que el precinto se encuentra cerrado")
                End If

                If (IdPrecinto = 0) Then
                    Throw New ApplicationException("No se permite guardar el contenedor puesto que el precinto no se ha guardado")
                End If

                If (ContenedorTokenTextBox.Text.Trim() = "") Then
                    ContenedorTokenTextBox.Focus()
                    Throw New ApplicationException("El código del contenedor no puede estar vacio")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim contenedorData = New DBImaging.SchemaProcess.TBL_ContenedorType With {
                        .fk_OT = IdOT,
                        .fk_Precinto = IdPrecinto,
                        .fk_Esquema = CShort(IIf(Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor, CShort(ContenedorEsquemaComboBox.SelectedValue), 1)),
                        .id_Contenedor = IdContenedor,
                        .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                        .Fecha_Apertura = SlygNullable.SysDate,
                        .cantidad_Enviados = 0,
                        .cantidad_Recibidos = 0,
                        .Cerrado = False,
                        .Cargado = False
                    }


                Dim esInsertar = Me.ContenedorInsertarButton.Text.StartsWith("Insertar")

                If (esInsertar) Then
                    IdContenedor = dbmImaging.SchemaProcess.TBL_Contenedor.DBNextId(IdOT, IdPrecinto)
                    contenedorData.id_Contenedor = IdContenedor
                    contenedorData.Token = New SlygNullable(Of String)(CType(IIf(Program.ImagingGlobal.ProyectoImagingRow.Usa_Codigo_Contenedor, ContenedorTokenTextBox.Text, PrecintoNumeroTextBox.Text & "-" & IdContenedor), String))
                    _nidContenedor = IdContenedor

                    If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Cantidades_Enviadas_Recibidas) Then
                        contenedorData.cantidad_Enviados = CInt(Me.CantidadDocumentosEnviadosTextBox.Text)
                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                            contenedorData.cantidad_Recibidos = CInt(Me.CantidadDocumentosRecibidosTextBox.Text)
                        End If
                        If (Program.ImagingGlobal.ProyectoImagingRow.Muestra_Cantidad_Recibida) Then
                            contenedorData.cantidad_Recibidos = CInt(Me.CantidadDocumentosRecibidosTextBox.Text)
                        End If
                    End If

                    Dim ContenedorDataAnterior = dbmImaging.SchemaProcess.TBL_Contenedor.DBFindByfk_OTToken(IdOT, contenedorData.Token)
                    If ContenedorDataAnterior.Rows.Count > 0 Then
                        Throw New Exception("Ya existe un contenedor con este código para la OT [" + IdOT.ToString + "]")
                    End If
                    dbmImaging.SchemaProcess.TBL_Contenedor.DBInsert(contenedorData)

                    Dim savedRow = dbmImaging.SchemaProcess.CTA_Destape_Contenedor_Resumido.DBFindByfk_OTfk_Precintoid_Contenedor(contenedorData.fk_OT, contenedorData.fk_Precinto, contenedorData.id_Contenedor)

                    _ContenedoresListBox_IgnoreSelectedIndexChanged = True
                    Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoDataTable)
                    Dim newRow = data.NewCTA_Destape_Contenedor_ResumidoRow
                    newRow.ItemArray = savedRow(0).ItemArray
                    data.AddCTA_Destape_Contenedor_ResumidoRow(newRow)
                    CantContenedorLabel.Text = CType(data.Count, String)

                    ContenedoresDataGridView.ClearSelection()
                    Dim row = Contenedor_GetRowContenedor(IdContenedor)
                    If (row IsNot Nothing) Then row.Selected = True

                    If (DocumentoGroupBox.Visible) Then
                        ContenedorEsquemaComboBox.Enabled = False
                        ContenedorTokenTextBox.Text = contenedorData.Token
                        Documento_CargarDocumentosExistentes()
                        DocumentosDataGridView.Enabled = True
                        Documento_Iniciar()
                    End If

                    _ContenedoresListBox_IgnoreSelectedIndexChanged = False

                Else
                    Dim contenedorInfoData = dbmImaging.SchemaProcess.TBL_Contenedor.DBGet(IdOT, IdPrecinto, IdContenedor)
                    _nidContenedor = IdContenedor
                    If (contenedorInfoData.Count = 0) Then Throw New Exception("Id de contenedor no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdPrecinto)
                End If

                If (_CamposDinamicosContenedorTableForm.Controls.Count > 0) Then
                    Dim contenedorDimData = New DBImaging.SchemaProcess.TBL_Contenedor_DataType With {
                        .fk_OT = IdOT,
                        .fk_Precinto = IdPrecinto,
                        .fk_Contenedor = IdContenedor,
                        .fk_Entidad = Program.ImagingGlobal.Entidad,
                        .fk_Proyecto = Program.ImagingGlobal.Proyecto
                    }

                    For Each campoContenedorData As Control In _CamposDinamicosContenedorTableForm.Controls
                        If Not (campoContenedorData.GetType().ToString() = "System.Windows.Forms.Label") Then
                            contenedorDimData.fk_Campo = CType(campoContenedorData.Tag, DesktopConfig.CrearControlesType).fk_Campo

                            Select Case campoContenedorData.GetType()
                                Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                    contenedorDimData.Data_Campo = CType(campoContenedorData, DesktopTextBox.DesktopTextBoxControl).Text
                                Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                    contenedorDimData.Data_Campo = CType(campoContenedorData, DesktopCheckBox.DesktopCheckBoxControl).Checked
                                Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                    contenedorDimData.Data_Campo = CType(campoContenedorData, DesktopComboBox.DesktopComboBoxControl).SelectedValue
                            End Select

                            If (esInsertar) Then
                                dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBInsert(contenedorDimData)
                            Else
                                Dim data = dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBGet(contenedorDimData.fk_OT, contenedorDimData.fk_Precinto, contenedorDimData.fk_Contenedor, contenedorDimData.fk_Campo)
                                If (data.Count = 0) Then
                                    dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBInsert(contenedorDimData)
                                Else
                                    dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBUpdate(contenedorDimData, contenedorDimData.fk_OT, contenedorDimData.fk_Precinto, contenedorDimData.fk_Contenedor, contenedorDimData.fk_Campo)
                                End If
                            End If
                        End If
                    Next
                End If

                If (esInsertar And Not DocumentoGroupBox.Visible) Then
                    Contenedor_NuevoContenedor()

                Else
                    If (Not esInsertar) Then MessageBox.Show("Contenedor Actualizado Con Exito", "Guardado", MessageBoxButtons.OK)

                End If

                dbmImaging.Transaction_Commit()

                Me.PrecintoInsertarButton.Text = "Actualizar Contenedor F3"

                EventManager.GuardarContenedor(IdOT, IdPrecinto, _nidContenedor)
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_Eliminar()
            If (Not ContenedorEliminarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (PrecintoCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite eliminar el contenedor puesto que el precinto se encuentra cerrado")
                End If

                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para eliminar el contenedor", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceURL, Program.DesktopGlobal.ClientIPAddress)) Then
                    Throw New Exception("No se permitió eliminar el contenedor")
                End If

                Dim cnt = Contenedor_BuscarContenedor(ContenedorTokenTextBox.Text)
                If (cnt Is Nothing) Then Throw New ApplicationException("El contenedor con token " & ContenedorTokenTextBox.Text & " no se encuentra en el precinto")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Contenedor.DBGet(cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el contenedor en la base de datos OT=" & cnt.fk_OT & " Precinto=" & cnt.fk_Precinto & " Contenedor=" & cnt.id_Contenedor)
                If (originalRow(0).Cargado) Then Throw New ApplicationException("No se permite modificar el contenedor puesto que ya se cargaron imagenes asociadas")

                dbmImaging.SchemaProcess.TBL_Contenedor.DBDelete(cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)

                Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoDataTable)
                _ContenedoresListBox_IgnoreSelectedIndexChanged = True
                ContenedoresDataGridView.ClearSelection()
                data.RemoveCTA_Destape_Contenedor_ResumidoRow(cnt)
                _ContenedoresListBox_IgnoreSelectedIndexChanged = False

                Contenedor_NuevoContenedor()

                dbmImaging.Transaction_Commit()
                MessageBox.Show("Contenedor Eliminado Con Exito", "Guardado", MessageBoxButtons.OK)
                ContenedorTokenTextBox.Focus()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_AbrirContenedor()
            If (Not ContenedorAbrirButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not DocumentoGroupBox.Visible) Then
                    Throw New ApplicationException("No se permite abrir el contenedor cuando el destape se realiza a nivel de contenedor")
                End If

                If (PrecintoCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite abrir el contenedor puesto que el precinto se encuentra cerrado")
                End If

                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para abrir el contenedor", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceURL, Program.DesktopGlobal.ClientIPAddress)) Then
                    Throw New Exception("No se permitió abrir el contenedor")
                End If

                Dim cnt = Contenedor_BuscarContenedor(ContenedorTokenTextBox.Text)
                If (cnt Is Nothing) Then Throw New ApplicationException("El contenedor con token " & ContenedorTokenTextBox.Text & " no se encuentra en el precinto")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Contenedor.DBGet(cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el contenedor en la base de datos OT=" & cnt.fk_OT & " Precinto=" & cnt.fk_Precinto & " Contenedor=" & cnt.id_Contenedor)
                If (originalRow(0).Cargado) Then Throw New ApplicationException("No se permite modificar el contenedor puesto que ya se cargaron imagenes asociadas")

                Dim contenedor = New DBImaging.SchemaProcess.TBL_ContenedorType() With {
                     .Cerrado = False,
                     .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                     .Fecha_Apertura = SlygNullable.SysDate
                 }

                dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedor, cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                Dim saveRows = dbmImaging.SchemaProcess.CTA_Destape_Contenedor_Resumido.DBFindByfk_OTfk_Precintoid_Contenedor(cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                cnt.ItemArray = saveRows(0).ItemArray

                Dim row = Contenedor_GetRowContenedor(cnt.id_Contenedor)
                row.DefaultCellStyle.BackColor = Drawing.Color.White

                ContenedorAbrirButton.Enabled = False
                ContenedorCerrarButton.Enabled = True

                dbmImaging.Transaction_Commit()
                MessageBox.Show("Se Abrio El Contenedor Con Exito", "Guardado", MessageBoxButtons.OK)
                ContenedorTokenTextBox.Focus()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_CerrarContenedor()
            If (Not ContenedorCerrarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not DocumentoGroupBox.Visible) Then
                    Throw New ApplicationException("No se permite cerrar el contenedor cuando el destape se realiza a nivel de contenedor")
                End If

                Dim documentosData = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)
                If (documentosData.Count = 0) Then
                    Throw New ApplicationException("No se permite cerrar el contenedor puesto que no se ha registrado ningún documento")
                End If

                If (Program.ImagingGlobal.ProyectoImagingRow.Muestra_Cantidad_Recibida) Then
                    If DocumentosDataGridView.RowCount < CInt(Me.CantidadDocumentosRecibidosTextBox.Text) Then
                        Throw New ApplicationException("No se permite cerrar el contenedor puesto que no se ha registrado la totalidad de los documentos")
                    End If
                End If

                Dim cnt = Contenedor_BuscarContenedor(ContenedorTokenTextBox.Text)
                If (cnt Is Nothing) Then Throw New ApplicationException("El contenedor con token " & ContenedorTokenTextBox.Text & " no se encuentra en el precinto")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Contenedor.DBGet(cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el contenedor en la base de datos OT=" & cnt.fk_OT & " Precinto=" & cnt.fk_Precinto & " Contenedor=" & cnt.id_Contenedor)
                If (originalRow(0).Cargado) Then Throw New ApplicationException("No se permite modificar el contenedor puesto que ya se cargaron imagenes asociadas")

                Dim contenedor = New DBImaging.SchemaProcess.TBL_ContenedorType() With {
                    .Cerrado = True,
                    .fk_Usuario_Cierre = Program.Sesion.Usuario.id,
                    .Fecha_Cierre = SlygNullable.SysDate
                }
                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Cantidades_Enviadas_Recibidas) Then
                    If Not (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                        contenedor.cantidad_Recibidos = documentosData.Count
                    End If
                End If

                dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedor, cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                Dim saveRows = dbmImaging.SchemaProcess.CTA_Destape_Contenedor_Resumido.DBFindByfk_OTfk_Precintoid_Contenedor(cnt.fk_OT, cnt.fk_Precinto, cnt.id_Contenedor)
                cnt.ItemArray = saveRows(0).ItemArray

                Dim row = Contenedor_GetRowContenedor(cnt.id_Contenedor)
                row.DefaultCellStyle.BackColor = Drawing.Color.LightGreen

                ContenedorAbrirButton.Enabled = True
                ContenedorCerrarButton.Enabled = False

                dbmImaging.Transaction_Commit()
                MessageBox.Show("Contenedor Cerrado Con Exito", "Guardado", MessageBoxButtons.OK)
                ContenedorTokenTextBox.Focus()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_NuevoContenedor()
            If (Not ContenedorNuevoButton.Enabled) Then Return

            IdContenedor = 0
            Contenedor_Iniciar()
            ContenedorInsertarButton.Text = "Insertar Contenedor F3"
            ContenedorCerradoLabel.Visible = False

            ContenedorCamposDinamicosPanel.Enabled = True
            ContenedorAbrirButton.Enabled = False
            ContenedorCerrarButton.Enabled = False
            ContenedorEliminarButton.Enabled = False
            DocumentosDataGridView.Enabled = False

            DocumentoTokenTextBox.ReadOnly = True
            DocumentoTipoComboBox.Enabled = False

            _ContenedoresListBox_IgnoreSelectedIndexChanged = True
            ContenedoresDataGridView.ClearSelection()
            _ContenedoresListBox_IgnoreSelectedIndexChanged = False
        End Sub

        Private Sub Contenedor_Seleccionar()
            If (Not _ContenedoresListBox_IgnoreSelectedIndexChanged) Then
                Contenedor_LimpiarControles()
                Documento_LimpiarControles()
                Documento_SetDataSourceDocumentos(New DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)

                If (ContenedoresDataGridView.SelectedRows.Count > 0) Then
                    Dim it = ContenedoresDataGridView.SelectedRows(0)
                    If (it Is Nothing) Then
                        Contenedor_NuevoContenedor()
                    Else
                        Dim row = CType(CType(it.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoRow)
                        IdContenedor = row.id_Contenedor
                        Contenedor_CargarDataExistenteCamposDinamicos(row.ToCTA_Destape_Contenedor_ResumidoType)
                        Me.ContenedorInsertarButton.Text = "Actualizar Contenedor F3"
                        Documento_CargarDocumentosExistentes()
                        If (row.Cerrado) Then
                            ContenedorAbrirButton.Enabled = True
                            ContenedorCerrarButton.Enabled = False
                            ContenedorCerradoLabel.Visible = True
                            ContenedorCamposDinamicosPanel.Enabled = False
                        Else
                            Documento_Iniciar()
                            ContenedorAbrirButton.Enabled = False
                            ContenedorCerrarButton.Enabled = True
                            ContenedorCerradoLabel.Visible = False
                            ContenedorCamposDinamicosPanel.Enabled = True
                        End If

                        ContenedorEliminarButton.Enabled = True
                        DocumentosDataGridView.Enabled = True
                        ContenedorTokenTextBox.Focus()
                    End If
                End If
            End If
        End Sub

        Private Sub Contenedor_DeshabilitarCampos()
            Me.ContenedorInsertarButton.Enabled = False
            Me.ContenedorAbrirButton.Enabled = False
            Me.ContenedorEliminarButton.Enabled = False
            Me.ContenedorCerrarButton.Enabled = False
            Me.ContenedorDatosGroupBox.Enabled = False

            Me.Documento_DeshabilitarCampos()
        End Sub

        Private Sub Contenedor_LimpiarControles()
            If Not (_CamposDinamicosContenedorTableForm Is Nothing) Then
                For Each campoContenedorData As Control In _CamposDinamicosContenedorTableForm.Controls
                    If Not (campoContenedorData.GetType().ToString() = "System.Windows.Forms.Label") Then
                        Select Case campoContenedorData.GetType()
                            Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                CType(campoContenedorData, DesktopTextBox.DesktopTextBoxControl).Text = ""
                            Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                CType(campoContenedorData, DesktopCheckBox.DesktopCheckBoxControl).Checked = False
                            Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                CType(campoContenedorData, DesktopComboBox.DesktopComboBoxControl).SelectedIndex = -1
                        End Select
                    End If
                Next
            End If


            Me.ContenedorDatosGroupBox.Enabled = True

            Me.ContenedorInsertarButton.Enabled = True
            Me.ContenedorAbrirButton.Enabled = False
            Me.ContenedorEliminarButton.Enabled = False
            Me.ContenedorCerrarButton.Enabled = False

            Me.ContenedorTokenTextBox.Focus()
        End Sub

        Private Sub Documento_SetDataSourceDocumentos(ByVal nData As DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)
            _DocumentosListBox_IgnoreSelectedIndexChanged = True
            DocumentosDataGridView.DataSource = nData
            DocumentosDataGridView.ClearSelection()
            _DocumentosListBox_IgnoreSelectedIndexChanged = False
        End Sub

        Private Sub Documento_CargarDocumentosExistentes()
            If (Not _ContenedoresListBox_IgnoreSelectedIndexChanged) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim documentosData = dbmImaging.SchemaProcess.CTA_Destape_Documento_Resumido.DBFindByfk_OTfk_Precintofk_ContenedorToken(IdOT, IdPrecinto, IdContenedor, Nothing)
                    Documento_SetDataSourceDocumentos(documentosData)

                    CantDocLabel.Text = CType(documentosData.Count, String)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub Documento_CargarTiposDocumento()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim esquema = CShort(ContenedorEsquemaComboBox.SelectedValue)

                Dim documentosData = dbmImaging.SchemaCore.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, esquema, False)
                Utilities.LlenarCombo(Me.DocumentoTipoComboBox, documentosData, DBImaging.SchemaCore.CTA_DocumentoEnum.id_Documento.ColumnName, DBImaging.SchemaCore.CTA_DocumentoEnum.Nombre_Documento.ColumnName)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Documento_Iniciar()
            DocumentoTipoComboBox.Enabled = True

            DocumentoTokenTextBox.ReadOnly = False
            DocumentoTokenTextBox.Text = ""
            Try : DocumentoTipoComboBox.SelectedIndex = 0 : Catch : End Try
            DocumentoTipoComboBox.Enabled = True
            DocumentoTokenTextBox.Focus()

            Documento_CargarTiposDocumento()
            If DocumentosDataGridView.RowCount > 0 Then
                DocumentoTipoComboBox.Text = DocumentosDataGridView.Rows(0).Cells(1).Value().ToString()
                DocumentoTipoComboBox.Enabled = False
            End If
        End Sub

        Private Sub Documento_Guardar()
            If (Not DocumentoInsertarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not DocumentoGroupBox.Visible) Then
                    Throw New ApplicationException("No se permite guardar el documento cuando el destape se realiza a nivel de contenedor")
                End If

                If (ContenedorCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite guardar el documento puesto que el contenedor se encuentra cerrado")
                End If

                If (IdContenedor = 0) Then
                    Throw New ApplicationException("Primero debe guardar el contenedor correspondiente")
                End If

                If (DocumentoTokenTextBox.Text.Trim() = "") Then
                    DocumentoTokenTextBox.Focus()
                    Throw New ApplicationException("El código del documento no puede estar vacio")
                End If

                If (CInt(DocumentoTipoComboBox.SelectedValue) = 0) Then
                    Throw New ApplicationException("No se encuentran configurados documentos para el esquema del contenedor")
                End If

                Dim doc = Documento_BuscarDocumento(DocumentoTokenTextBox.Text)
                If (doc IsNot Nothing) Then Throw New ApplicationException("El documento con token " & DocumentoTokenTextBox.Text & " ya se encuentra en el contenedor")

                If (Program.ImagingGlobal.ProyectoImagingRow.Muestra_Cantidad_Recibida) Then
                    If DocumentosDataGridView.RowCount = CInt(CantidadDocumentosRecibidosTextBox.Text) Then
                        Throw New ApplicationException("No es posible guardar. La cantidad de documentos recibidos es igual a la cantidad de documentos ya ingresados")
                    End If
                End If



                Dim documentoInfo = New DBImaging.SchemaProcess.TBL_Contenedor_DetalleType With {
                        .fk_OT = IdOT,
                        .fk_Precinto = IdPrecinto,
                        .fk_Contenedor = IdContenedor,
                        .Token = DocumentoTokenTextBox.Text,
                        .fk_Documento = CInt(DocumentoTipoComboBox.SelectedValue),
                        .Cargado = False
                    }
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()
                dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBInsert(documentoInfo)

                _DocumentosListBox_IgnoreSelectedIndexChanged = True
                Dim data = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)
                Dim newRow = data.NewCTA_Destape_Documento_ResumidoRow
                newRow.ItemArray = documentoInfo.ToDataRow(data).ItemArray
                newRow.Nombre_Documento = CType(DocumentoTipoComboBox.SelectedItem, DataRowView).Row(DBImaging.SchemaCore.CTA_DocumentoEnum.Nombre_Documento.ColumnName).ToString
                data.AddCTA_Destape_Documento_ResumidoRow(newRow)
                _DocumentosListBox_IgnoreSelectedIndexChanged = False

                DocumentoTokenTextBox.Text = ""
                
                If DocumentosDataGridView.RowCount > 0 Then
                    DocumentoTipoComboBox.Enabled = False
                End If

                CantDocLabel.Text = CStr(DocumentosDataGridView.RowCount)
                dbmImaging.Transaction_Commit()
                DocumentoTokenTextBox.Focus()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Documento_Eliminar()
            If (Not DocumentoEliminarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not DocumentoGroupBox.Visible) Then
                    Throw New ApplicationException("No se permite eliminar el documento cuando el destape se realiza a nivel de contenedor")
                End If

                If (ContenedorCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite eliminar el documento puesto que el contenedor se encuentra cerrado")
                End If

                Dim doc = Documento_BuscarDocumento(DocumentoTokenTextBox.Text)
                If (doc Is Nothing) Then Throw New ApplicationException("El documento con token " & DocumentoTokenTextBox.Text & " no se encuentra en el contenedor")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBGet(doc.fk_OT, doc.fk_Precinto, doc.fk_Contenedor, doc.Token)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el documento en la base de datos OT=" & doc.fk_OT & " Precinto=" & doc.fk_Precinto & " Contenedor=" & doc.fk_Contenedor & " Token" & doc.Token)
                If (originalRow(0).Cargado) Then Throw New ApplicationException("No se permite modificar el documento puesto que ya se cargaron imagenes asociadas")

                dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBDelete(doc.fk_OT, doc.fk_Precinto, doc.fk_Contenedor, doc.Token)

                Dim data = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)
                _DocumentosListBox_IgnoreSelectedIndexChanged = True
                DocumentosDataGridView.ClearSelection()
                data.RemoveCTA_Destape_Documento_ResumidoRow(doc)
                _DocumentosListBox_IgnoreSelectedIndexChanged = False

                Documento_NuevoDocumento()

                dbmImaging.Transaction_Commit()

                MessageBox.Show("Documento Eliminado Con Exito", "Guardado", MessageBoxButtons.OK)
                DocumentoTokenTextBox.Focus()

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Documento_DeshabilitarCampos()
            Me.DocumentoInsertarButton.Enabled = False
            Me.DocumentoEliminarButton.Enabled = False
        End Sub

        Private Sub Documento_LimpiarControles()
            Me.DocumentoInsertarButton.Enabled = True
            Me.DocumentoEliminarButton.Enabled = False

            Me.DocumentoTokenTextBox.Text = ""
            Me.DocumentoTipoComboBox.Enabled = True
            Me.CantDocLabel.Text = "0"
            Try : DocumentoTipoComboBox.SelectedIndex = 0 : Catch : End Try

        End Sub

        Private Sub Documento_NuevoDocumento()
            If (Not DocumentoNuevoButton.Enabled) Then Return

            Try
                If (ContenedorCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite agregar documentos puesto que el contenedor se encuentra cerrado")
                End If

                If (IdContenedor = 0) Then
                    Throw New ApplicationException("Primero debe guardar el contenedor correspondiente")
                End If

                Documento_Iniciar()
                DocumentoEliminarButton.Enabled = False
                DocumentoInsertarButton.Enabled = True

                _DocumentosListBox_IgnoreSelectedIndexChanged = True
                DocumentosDataGridView.ClearSelection()
                _DocumentosListBox_IgnoreSelectedIndexChanged = False
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub Documento_Seleccionar()
            If (Not _DocumentosListBox_IgnoreSelectedIndexChanged) Then
                Documento_LimpiarControles()

                If (DocumentosDataGridView.SelectedRows.Count > 0) Then
                    Dim it = DocumentosDataGridView.SelectedRows(0)
                    If (it Is Nothing) Then
                        Documento_NuevoDocumento()
                    Else
                        Dim row = CType(CType(it.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoRow)
                        DocumentoTokenTextBox.Text = row.Token
                        Try : DocumentoTipoComboBox.SelectedValue = row.fk_Documento : Catch : End Try
                        DocumentoTokenTextBox.ReadOnly = True
                        DocumentoTipoComboBox.Enabled = False
                        DocumentoTokenTextBox.Focus()
                        DocumentoEliminarButton.Enabled = True
                        DocumentoInsertarButton.Enabled = False
                    End If
                End If
            End If
        End Sub

        Private Sub IniciarKeyManager(ByVal nControl As Control)
            AddHandler nControl.KeyDown, AddressOf KeyManager_KeyDown
            For Each ctr As Control In nControl.Controls
                IniciarKeyManager(ctr)
            Next
        End Sub


#End Region

#Region " Funciones "

        Private Function Precinto_BuscarCampoDinamico(ByVal nIdCampo As Short) As Control
            For Each campoPrecintoData As Control In _CamposDinamicosPrecintoTableForm.Controls
                If (campoPrecintoData.GetType().ToString() <> "System.Windows.Forms.Label") Then
                    If (CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).fk_Campo = nIdCampo) Then Return campoPrecintoData
                End If
            Next
            Return Nothing
        End Function

        Private Function Contenedor_BuscarCampoDinamico(ByVal nIdCampo As Short) As Control
            For Each campoContenedorData As Control In _CamposDinamicosContenedorTableForm.Controls
                If (campoContenedorData.GetType().ToString() <> "System.Windows.Forms.Label") Then
                    If (CType(campoContenedorData.Tag, DesktopConfig.CrearControlesType).fk_Campo = nIdCampo) Then Return campoContenedorData
                End If
            Next
            Return Nothing
        End Function

        Private Function Contenedor_GetRowContenedor(nIdContenedor As Short) As DataGridViewRow
            For Each row As DataGridViewRow In ContenedoresDataGridView.Rows
                If (CType(CType(row.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoRow).id_Contenedor = nIdContenedor) Then
                    Return row
                End If
            Next
            Return Nothing
        End Function

        Private Function Contenedor_BuscarContenedor(nToken As String) As DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoRow
            Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoDataTable)
            Dim docData = CType(data.Select(DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoEnum.Token.ColumnName & "='" & nToken & "'"), DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoRow())
            If (docData.Length > 0) Then
                Return docData(0)
            End If

            Return Nothing
        End Function

        Private Function Documento_BuscarDocumento(nToken As String) As DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoRow
            Dim data = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoDataTable)
            Dim docData = CType(data.Select(DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoEnum.Token.ColumnName & "='" & nToken & "'"), DBImaging.SchemaProcess.CTA_Destape_Documento_ResumidoRow())
            If (docData.Length > 0) Then
                Return docData(0)
            End If

            Return Nothing
        End Function

#End Region

    End Class
End Namespace