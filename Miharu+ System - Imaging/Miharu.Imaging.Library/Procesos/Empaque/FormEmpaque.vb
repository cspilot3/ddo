Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Procesos.Empaque
    Public Class FormEmpaque
        Inherits FormBase

#Region " Declaraciones "

        Private _CamposDinamicosPrecinto As New List(Of DesktopConfig.CrearControlesType)
        Private _CamposDinamicosPrecintoTableForm As Panel
        Private _ContenedoresListBox_IgnoreSelectedIndexChanged As Boolean = False
        Private _DocumentosListBox_IgnoreSelectedIndexChanged As Boolean = False
        Private Current_Esquema As Integer

#End Region

#Region " Propiedades "

        Public Property EventManager As EventManager

        Public Property IdOT() As Integer

        Public Property IdEmpaque() As Short

        Public Property IdEmpaqueContenedor() As Short

#End Region

#Region " Eventos "

        Private Sub FormEmpaque_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            DocumentoGroupBox.Visible = Not (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor)
            DocumentosDataGridView.AutoGenerateColumns = False
            ContenedoresDataGridView.AutoGenerateColumns = False

            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Destape_Contenedor) Then
                DocumentoGroupBox.Visible = False
                ContenedorAbrirButton.Visible = False
                ContenedorCerrarButton.Visible = False
                DocumentoMenuPanel.Enabled = False
            End If

            Contenedor_CargarEsquemas()

            Precinto_DeshabilitarCampos()

            Precinto_CrearCamposDinamicos()

            IniciarKeyManager(Me)

            Try
                If (IdEmpaque <> 0) Then
                    Precinto_CargarDataExistenteCamposDinamicos(Nothing)
                    Dim valido As Boolean = True
                    Dim msgError As String = ""

                    EventManager.CargarPrecinto(IdOT, IdEmpaque, ContenedorEsquemaComboBox, valido, msgError)

                    If (valido) Then
                        Contenedor_CargarContenedoresExistentes()
                        Contenedor_Iniciar()

                        PrecintoEliminarButton.Enabled = True
                    Else
                        Throw New Exception(msgError)
                    End If

                Else
                    PrecintoEliminarButton.Enabled = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub KeyManager_KeyDown(sender As System.Object, e As KeyEventArgs)
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

        Private Sub PrecintoTextBox_KeyDown(sender As System.Object, e As KeyEventArgs) Handles PrecintoNumeroTextBox.KeyDown
            If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab) Then
                Precinto_Validar(PrecintoNumeroTextBox.Text)
            End If
        End Sub

        Private Sub PrecintoValidarButton_Click(sender As System.Object, e As EventArgs) Handles PrecintoValidarButton.Click
            Precinto_Validar(PrecintoNumeroTextBox.Text)
        End Sub

        Private Sub PrecintoInsertarButton_Click(sender As System.Object, e As EventArgs) Handles PrecintoInsertarButton.Click
            Precinto_Guardar()
        End Sub

        Private Sub PrecintoAbrirButton_Click(sender As System.Object, e As EventArgs) Handles PrecintoAbrirButton.Click
            Precinto_Abrir()
        End Sub

        Private Sub PrecintoFinalizarButton_Click(sender As System.Object, e As EventArgs) Handles PrecintoFinalizarButton.Click
            Precinto_Finalizar()
        End Sub

        Private Sub PrecintoEliminarButton_Click(sender As System.Object, e As EventArgs) Handles PrecintoEliminarButton.Click
            Dim Respuesta = MessageBox.Show("¿Está seguro que desea eliminar el Precinto?", "Empaque", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

            If (Respuesta = DialogResult.OK) Then
                Precinto_Eliminar()
            End If

        End Sub

        Private Sub ContenedorInsertarButton_Click(sender As System.Object, e As EventArgs) Handles ContenedorInsertarButton.Click
            Contenedor_Guardar()
        End Sub

        Private Sub ContenedorNuevoButton_Click(sender As System.Object, e As EventArgs) Handles ContenedorNuevoButton.Click
            Contenedor_NuevoContenedor()
        End Sub

        Private Sub ContenedorAbrirButton_Click(sender As System.Object, e As EventArgs) Handles ContenedorAbrirButton.Click
            Contenedor_AbrirContenedor()
        End Sub

        Private Sub ContenedorCerrarButton_Click(sender As System.Object, e As EventArgs) Handles ContenedorCerrarButton.Click
            Contenedor_CerrarContenedor()
        End Sub

        Private Sub ContenedorEliminarButton_Click(sender As System.Object, e As EventArgs) Handles ContenedorEliminarButton.Click
            Contenedor_Eliminar()
        End Sub

        Private Sub ContenedoresDataGridView_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ContenedoresDataGridView.MouseDoubleClick
            Contenedor_Seleccionar()
        End Sub

        Private Sub DocumentoInsertarButton_Click(sender As System.Object, e As EventArgs) Handles DocumentoInsertarButton.Click
            Documento_Guardar()
        End Sub

        Private Sub DocumentoNuevoButton_Click(sender As System.Object, e As EventArgs) Handles DocumentoNuevoButton.Click
            Documento_NuevoDocumento()
        End Sub

        Private Sub DocumentoEliminarButton_Click(sender As System.Object, e As EventArgs) Handles DocumentoEliminarButton.Click
            Documento_Eliminar()
        End Sub

        Private Sub DocumentoTokenTextBox_KeyPress(sender As System.Object, e As KeyPressEventArgs) Handles DocumentoTokenTextBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.DocumentoInsertarButton.Focus()
            End If
        End Sub

        Private Sub DocumentosDataGridView_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles DocumentosDataGridView.MouseDoubleClick
            Documento_Seleccionar()
        End Sub

        Private Sub FormEmpaque_SizeChanged(sender As System.Object, e As EventArgs) Handles MyBase.SizeChanged
            PrecintoCamposDinamicosPanel.Refresh()
        End Sub

        Private Sub PrecintoGroupBox_Enter(sender As System.Object, e As EventArgs) Handles PrecintoGroupBox.Enter
            MostrarMenu(PrecintoMenuPanel)
        End Sub

        Private Sub ContenedorDatosGroupBox_Enter(sender As System.Object, e As EventArgs) Handles ContenedorDatosGroupBox.Enter
            MostrarMenu(ContenedorMenuPanel)
        End Sub

        Private Sub DocumentoGroupBox_Enter(sender As System.Object, e As EventArgs) Handles DocumentoGroupBox.Enter
            MostrarMenu(DocumentoMenuPanel)
        End Sub

        Private Sub PrecintoLastCampoDinamico_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.PrecintoMenuPanel.Focus()
            End If
        End Sub

        Private Sub ContenedorCodigoTextBox_KeyPress(sender As System.Object, e As KeyPressEventArgs) Handles ContenedorTokenTextBox.KeyPress
            If ((e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) And (ContenedorEsquemaComboBox.Enabled = True)) Then
                e.Handled = True
                Me.ContenedorEsquemaComboBox.Focus()
            End If

            If ((e.KeyChar = ChrW(13)) And (ContenedorEsquemaComboBox.Enabled = False)) Then
                e.Handled = True
                Contenedor_Guardar()
            End If
        End Sub

        Private Sub ContenedorEsquemaComboBox_KeyPress(sender As System.Object, e As KeyPressEventArgs) Handles ContenedorEsquemaComboBox.KeyPress
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.ContenedorInsertarButton.Focus()
            End If
        End Sub

#End Region

#Region " Funciones "

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

                Dim tableCampos = dbmImaging.SchemaConfig.TBL_Empaque_Campo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)
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
                                .Es_Campo_Control = row.Es_Campo_Control
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

        Private Function Precinto_BuscarCampoDinamico(nIdCampo As Short) As Control
            For Each campoPrecintoData As Control In _CamposDinamicosPrecintoTableForm.Controls
                If (campoPrecintoData.GetType().ToString() <> "System.Windows.Forms.Label") Then
                    If (CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).fk_Campo = nIdCampo) Then Return campoPrecintoData
                End If
            Next
            Return Nothing
        End Function

        Private Sub Precinto_CargarDataExistenteCamposDinamicos(nPrecintoInfo As DBImaging.SchemaProcess.TBL_EmpaqueType)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If (nPrecintoInfo Is Nothing) Then
                    Dim dataPrecinto = dbmImaging.SchemaProcess.TBL_Empaque.DBGet(IdOT, IdEmpaque)
                    If (dataPrecinto.Count = 0) Then Throw New Exception("Id de precinto no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdEmpaque)
                    nPrecintoInfo = dataPrecinto(0).ToTBL_EmpaqueType
                End If

                Me.PrecintoNumeroTextBox.Text = nPrecintoInfo.Precinto
                Me.PrecintoNumeroTextBox.ReadOnly = True

                Me.PrecintoCerradoLabel.Visible = nPrecintoInfo.Cerrado

                Me.PrecintoAbrirButton.Enabled = nPrecintoInfo.Cerrado
                Me.PrecintoFinalizarButton.Enabled = Not Me.PrecintoAbrirButton.Enabled

                PrecintoEliminarButton.Enabled = True

                Dim tableCamposData = dbmImaging.SchemaProcess.TBL_Empaque_Data.DBGet(IdOT, IdEmpaque, Nothing)
                For Each row In tableCamposData
                    Dim campoPrecintoData = Precinto_BuscarCampoDinamico(row.fk_Campo)
                    If campoPrecintoData IsNot Nothing Then
                            Select campoPrecintoData.GetType()
                            Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                CType(campoPrecintoData, DesktopTextBox.DesktopTextBoxControl).Text = row.Data_Campo.ToString
                            Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                CType(campoPrecintoData, DesktopCheckBox.DesktopCheckBoxControl).Checked = CBool(row.Data_Campo)
                            Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                CType(campoPrecintoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue = row.Data_Campo
                        End Select
                    End If
                Next

                Me.PrecintoCamposDinamicosPanel.Enabled = True
                Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_Validar(NroPrecinto As String)
            If (PrecintoNumeroTextBox.ReadOnly) Then
                PrecintoCamposDinamicosPanel.Focus()
                Return
            End If

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim dataPrecinto = dbmImaging.SchemaProcess.TBL_Empaque.DBFindByfk_OTPrecinto(IdOT, NroPrecinto)

                If (dataPrecinto.Rows.Count = 0 OrElse Not dataPrecinto(0).Cerrado) Then
                    PrecintoCamposDinamicosPanel.Enabled = True
                    PrecintoNumeroTextBox.ReadOnly = True
                End If

                If (dataPrecinto.Rows.Count = 0) Then
                    Contenedor_LimpiarControles()
                    Documento_LimpiarControles()
                Else
                    IdEmpaque = dataPrecinto(0).id_Empaque
                    Precinto_CargarDataExistenteCamposDinamicos(dataPrecinto(0).ToTBL_EmpaqueType)
                    Contenedor_CargarContenedoresExistentes()
                End If

                Me.PrecintoInsertarButton.Enabled = Not (dataPrecinto.Rows.Count > 0 AndAlso dataPrecinto(0).Cerrado)

                If (Not Me.PrecintoInsertarButton.Enabled) Then Throw New ApplicationException("El precinto ya se encuentra cerrado")

                Dim primerCampo = Precinto_BuscarCampoDinamico(1)
                If (primerCampo IsNot Nothing) Then primerCampo.Select()
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

                Dim esInsertar = Me.PrecintoInsertarButton.Text.StartsWith("Insertar")

                If (esInsertar) Then
                    IdEmpaque = dbmImaging.SchemaProcess.TBL_Empaque.DBNextId(IdOT)

                    Dim nuevoPrecinto = New DBImaging.SchemaProcess.TBL_EmpaqueType With {
                        .fk_OT = IdOT,
                        .id_Empaque = IdEmpaque,
                        .Precinto = PrecintoNumeroTextBox.Text,
                        .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                        .Fecha_Apertura = SlygNullable.SysDate,
                        .Cerrado = False,
                        .fk_Puesto_Trabajo = Program.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo
                    }

                    dbmImaging.SchemaProcess.TBL_Empaque.DBInsert(nuevoPrecinto)


                Else
                    Dim precintoInfoData = dbmImaging.SchemaProcess.TBL_Empaque.DBGet(IdOT, IdEmpaque)
                    If (precintoInfoData.Count = 0) Then Throw New Exception("Id de precinto no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdEmpaque)
                End If

                Dim precintoDimData = New DBImaging.SchemaProcess.TBL_Empaque_DataType With {
                    .fk_OT = IdOT,
                    .fk_Empaque = IdEmpaque,
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
                            dbmImaging.SchemaProcess.TBL_Empaque_Data.DBInsert(precintoDimData)
                        Else
                            Dim data = dbmImaging.SchemaProcess.TBL_Empaque_Data.DBGet(precintoDimData.fk_OT, precintoDimData.fk_Empaque, precintoDimData.fk_Campo)
                            If (data.Count = 0) Then
                                dbmImaging.SchemaProcess.TBL_Empaque_Data.DBInsert(precintoDimData)
                            Else
                                dbmImaging.SchemaProcess.TBL_Empaque_Data.DBUpdate(precintoDimData, precintoDimData.fk_OT, precintoDimData.fk_Empaque, precintoDimData.fk_Campo)
                            End If
                        End If
                    End If
                Next

                Dim valido As Boolean = True
                Dim msgError As String = ""

                EventManager.ValidarPrecintoEmpaque(IdOT, IdEmpaque, dbmImaging, ContenedorEsquemaComboBox, valido, msgError)

                If (valido) Then
                    dbmImaging.Transaction_Commit()

                    Contenedor_CargarContenedoresExistentes()
                    Contenedor_Iniciar()
                Else
                    Throw New Exception(msgError)
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                End If


                If (Not esInsertar) Then
                    MessageBox.Show("Precinto Actualizado Con Exito", "Guardado", MessageBoxButtons.OK)
                End If

                Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"

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
                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para eliminar el precinto", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceUrl, Program.DesktopGlobal.ClientIpAddress)) Then
                    Throw New Exception("No se permitió eliminar el precinto")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                EventManager.FinalizarEliminarPrecinto(IdOT, IdEmpaque)

                dbmImaging.Transaction_Begin()

                If (IdEmpaque = 0) Then Throw New Exception("Aún no se ha ingresado un precinto")

                dbmImaging.SchemaProcess.TBL_Empaque.DBDelete(IdOT, IdEmpaque)

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
                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para abrir el precinto", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceUrl, Program.DesktopGlobal.ClientIpAddress)) Then
                    Throw New Exception("No se permitió abrir el precinto")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                If (IdEmpaque = 0) Then Throw New Exception("Aún no se ha ingresado un precinto")

                Dim data = dbmImaging.SchemaProcess.TBL_Empaque.DBGet(IdOT, IdEmpaque)
                If (data.Count = 0) Then Throw New Exception("No se encontró el precinto " & PrecintoNumeroTextBox.Text)

                Dim precinto = New DBImaging.SchemaProcess.TBL_EmpaqueType() With {
                    .Cerrado = False,
                    .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                    .Fecha_Apertura = SlygNullable.SysDate
                }

                dbmImaging.SchemaProcess.TBL_Empaque.DBUpdate(precinto, IdOT, IdEmpaque)

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
                If (IdEmpaque = 0) Then Throw New Exception("Aún no se ha ingresado un precinto")

                Dim dataCnt = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoDataTable)
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
                Dim precintoData = dbmImaging.SchemaProcess.TBL_Empaque.DBGet(IdOT, IdEmpaque)
                If (precintoData.Count = 0) Then Throw New Exception("No se encontró el precinto " & PrecintoNumeroTextBox.Text & " ID " & IdEmpaque)

                Dim precinto = New DBImaging.SchemaProcess.TBL_EmpaqueType() With {
                    .Cerrado = True,
                    .fk_Usuario_Cierre = Program.Sesion.Usuario.id,
                    .Fecha_Cierre = SlygNullable.SysDate
                }

                Dim TipoFuid As Integer = Nothing
                Dim TipoGestion As String = Nothing
                '--==================== INICIO REQUERIMIENTO RITM0364368 =====================================
                Dim _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable
                _documentosStickerDataTable = dbmImaging.SchemaConfig.TBL_Documento_Sticker.DBFindByfk_Entidadfk_Proyectogenera_Sticker_Fisicofk_Documento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, True, Nothing)
                '--==================== FIN REQUERIMIENTO RITM0364368 =====================================

                If Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Cajas Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Rotulo_de_Carpeta Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Hoja_Control Or _documentosStickerDataTable.Rows.Count > 0 Then
                    If Program.ImagingGlobal.ProyectoImagingRow.Usa_Generacion_de_Fuid Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Hoja_Control Then
                        For Each campoPrecintoData As Control In _CamposDinamicosPrecintoTableForm.Controls
                            If Not (campoPrecintoData.GetType().ToString() = "System.Windows.Forms.Label") Then
                                Dim fk_Campo = CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).fk_Campo
                                Dim Es_Campo_Control = CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).Es_Campo_Control
                                Dim Campo_Precinto_Data As String = String.Empty
                                If Es_Campo_Control Then
                                    Select Case campoPrecintoData.GetType()
                                        Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                            Campo_Precinto_Data = CType(campoPrecintoData, DesktopTextBox.DesktopTextBoxControl).Text
                                        Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                            Campo_Precinto_Data = CType(campoPrecintoData, DesktopCheckBox.DesktopCheckBoxControl).Checked.ToString()
                                        Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                            Campo_Precinto_Data = CType(campoPrecintoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue.ToString()
                                    End Select
                                End If
                                Dim TipoFuidHistoriaLaboral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieHistoriaLaboralUMV").Item(0).Valor_Parametro_Sistema
                                Dim TipoFuidNomina = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieNominaUMV").Item(0).Valor_Parametro_Sistema
                                Dim TipoFuidLiquidacion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieAutoliquidacionUMV").Item(0).Valor_Parametro_Sistema
                                Dim TipoGestionArchivoCentral = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoCentral").Item(0).Valor_Parametro_Sistema
                                Dim TipoGestionArchivoGestion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoGestion").Item(0).Valor_Parametro_Sistema

                                If TipoGestionArchivoCentral = Campo_Precinto_Data Then
                                    TipoGestion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoCentral").Item(0).Valor_Parametro_Sistema
                                End If

                                If TipoGestionArchivoGestion = Campo_Precinto_Data Then
                                    TipoGestion = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@TipoGestionArchivoGestion").Item(0).Valor_Parametro_Sistema
                                End If

                                If TipoFuidHistoriaLaboral = Campo_Precinto_Data Then
                                    TipoFuid = CInt(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieHistoriaLaboralUMV").Item(0).Valor_Parametro_Sistema)
                                End If
                                If TipoFuidNomina = Campo_Precinto_Data Then
                                    TipoFuid = CInt(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieNominaUMV").Item(0).Valor_Parametro_Sistema)
                                End If
                                If TipoFuidLiquidacion = Campo_Precinto_Data Then
                                    TipoFuid = CInt(dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBGet("@SerieAutoliquidacionUMV").Item(0).Valor_Parametro_Sistema)
                                End If

                            End If
                        Next
                    End If
                    Dim Caja = PrecintoNumeroTextBox.Text
                    Dim objRotulo As New FormRotulos(Caja, IdOT, TipoFuid, TipoGestion)
                    objRotulo.ShowDialog()
                    If Not objRotulo.Valido Then
                        Throw New Exception("No se permite cerrar el precinto puesto que no se ha registrado ninguna acción.")
                    End If
                End If

                EventManager.FinalizarPrecintoEmpaque(IdOT, IdEmpaque)

                dbmImaging.SchemaProcess.TBL_Empaque.DBUpdate(precinto, IdOT, IdEmpaque)

                dbmImaging.Transaction_Commit()


                MessageBox.Show("Precinto Cerrado Con Exito", "Guardado", MessageBoxButtons.OK)

                Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"
                Me.PrecintoInsertarButton.Enabled = False
                Me.PrecintoCerradoLabel.Visible = True
                Me.PrecintoFinalizarButton.Enabled = False
                Me.PrecintoAbrirButton.Enabled = True
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

        Private Sub Contenedor_CargarContenedoresExistentes()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim contenedoresData = dbmImaging.SchemaProcess.CTA_Empaque_Contenedor_Resumido.DBFindByfk_OTfk_Empaqueid_Empaque_Contenedor(IdOT, IdEmpaque, Nothing)

                If contenedoresData.Rows.Count > 0 Then
                    Current_Esquema = CInt(contenedoresData.Rows(0).Item(3))
                    ContenedorEsquemaComboBox.Enabled = False
                End If

                _ContenedoresListBox_IgnoreSelectedIndexChanged = True
                ContenedoresDataGridView.ClearSelection()
                ContenedoresDataGridView.DataSource = contenedoresData
                CantContenedorLabel.Text = CType(contenedoresData.Count, String)

                For Each row As DataGridViewRow In ContenedoresDataGridView.Rows
                    If (CType(CType(row.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow).Cerrado) Then
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
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim esquemasData = dbmImaging.SchemaCore.CTA_Esquema.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
                Utilities.LlenarCombo(Me.ContenedorEsquemaComboBox, esquemasData, DBImaging.SchemaCore.CTA_EsquemaEnum.id_Esquema.ColumnName, DBImaging.SchemaCore.CTA_EsquemaEnum.Nombre_Esquema.ColumnName)

                If esquemasData.Rows.Count = 1 Then
                    ContenedorEsquemaComboBox.Enabled = False
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_Iniciar()
            Me.Contenedor_LimpiarControles()

            'Try : ContenedorEsquemaComboBox.SelectedIndex = 0 : Catch : End Try
            'ContenedorEsquemaComboBox.Enabled = True

            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Codigo_Contenedor) Then
                ContenedorTokenTextBox.Text = ""
                ContenedorTokenTextBox.ReadOnly = False
                ContenedorTokenTextBox.Focus()
            Else
                ContenedorTokenTextBox.Text = "[AUTO]"
                ContenedorTokenTextBox.ReadOnly = True
                ContenedorEsquemaComboBox.Focus()
            End If

            Documento_LimpiarControles()
            Documento_SetDataSourceDocumentos(New DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)
            DocumentosDataGridView.Enabled = False
        End Sub

        Private Sub Documento_SetDataSourceDocumentos(ByVal nData As DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)
            _DocumentosListBox_IgnoreSelectedIndexChanged = True
            DocumentosDataGridView.DataSource = nData
            DocumentosDataGridView.ClearSelection()
            _DocumentosListBox_IgnoreSelectedIndexChanged = False
        End Sub

        Private Function Contenedor_GetRowContenedor(ByVal nIdEmpaqueContenedor As Short) As DataGridViewRow
            For Each row As DataGridViewRow In ContenedoresDataGridView.Rows
                If (CType(CType(row.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow).id_Empaque_Contenedor = nIdEmpaqueContenedor) Then
                    Return row
                End If
            Next
            Return Nothing
        End Function

        Private Sub Contenedor_Guardar()
            If (Not ContenedorInsertarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim nIdEmpaqueContenedor As Integer

            Try
                If (PrecintoCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite guardar el contenedor puesto que el precinto se encuentra cerrado")
                End If

                If (IdEmpaque = 0) Then
                    Throw New ApplicationException("No se permite guardar el contenedor puesto que el precinto no se ha guardado")
                End If

                If (ContenedorTokenTextBox.Text.Trim() = "") Then
                    ContenedorTokenTextBox.Focus()
                    Throw New ApplicationException("El código del contenedor no puede estar vacio")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim contenedorData = New DBImaging.SchemaProcess.TBL_Empaque_ContenedorType With {
                        .fk_OT = IdOT,
                        .fk_Empaque = IdEmpaque,
                        .fk_Esquema = CShort(ContenedorEsquemaComboBox.SelectedValue),
                        .id_Empaque_Contenedor = IdEmpaqueContenedor,
                        .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                        .Fecha_Apertura = SlygNullable.SysDate,
                        .Cerrado = False,
                        .Token = New SlygNullable(Of String)(CType(IIf(Program.ImagingGlobal.ProyectoImagingRow.Usa_Codigo_Contenedor, ContenedorTokenTextBox.Text, PrecintoNumeroTextBox.Text & "-" & IdEmpaqueContenedor), String))
                    }

                'Validar si contenedor fue destapado
                If Program.ImagingGlobal.ProyectoImagingRow.Usa_Validacion_Empaque_Contenedor_Destape Then
                    Dim Carpeta = dbmImaging.SchemaProcess.PA_Validar_Contenedor.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, ContenedorTokenTextBox.Text, Program.ImagingGlobal.ProyectoImagingRow.Usa_Validacion_Empaque_Contenedor_Destape_OT, IdOT)
                    If Carpeta.Rows.Count = 0 Then
                        Throw New ApplicationException("La carpeta [" + ContenedorTokenTextBox.Text + "] no ha sido destapada por favor validar.")
                    End If
                End If

                
                If Program.ImagingGlobal.ProyectoImagingRow.Usa_Validacion_Empaque_Campo Then
                    For Each campoPrecintoData As Control In _CamposDinamicosPrecintoTableForm.Controls
                        If Not (campoPrecintoData.GetType().ToString() = "System.Windows.Forms.Label") Then
                            Dim fk_Campo = CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).fk_Campo
                            Dim Es_Campo_Control = CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).Es_Campo_Control

                            If Es_Campo_Control Then
                                Dim Campo_Precinto_Data As String = String.Empty
                                Select Case campoPrecintoData.GetType()
                                    Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                        Campo_Precinto_Data = CType(campoPrecintoData, DesktopTextBox.DesktopTextBoxControl).Text
                                    Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                        Campo_Precinto_Data = CType(campoPrecintoData, DesktopCheckBox.DesktopCheckBoxControl).Checked.ToString()
                                    Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                        Campo_Precinto_Data = CType(campoPrecintoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue.ToString()
                                End Select

                                Dim Data_Comparar As String = Nothing

                                Data_Comparar = dbmImaging.SchemaProcess.PA_Validar_Campo_Empaque.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, IdOT, fk_Campo, ContenedorTokenTextBox.Text, PrecintoNumeroTextBox.Text, Program.ImagingGlobal.ProyectoImagingRow.Usa_Validacion_Empaque_Campo_OT)

                                If Data_Comparar IsNot Nothing Then
                                    If Data_Comparar <> Campo_Precinto_Data Then
                                        Throw New ApplicationException("Los parámetros del contenedor que está intentando empacar no corresponde a la caja que está empacando:  [" + ContenedorTokenTextBox.Text + "].")
                                    End If
                                Else
                                    Throw New ApplicationException("Los parámetros del contenedor que está intentando empacar no corresponde a la caja que está empacando:  [" + ContenedorTokenTextBox.Text + "].")
                                End If
                            End If
                        End If
                    Next
                End If


                Dim esInsertar = Me.ContenedorInsertarButton.Text.StartsWith("Insertar")

                If (esInsertar) Then

                    Dim valido As Boolean = True
                    Dim msgError As String = ""
                    EventManager.ValidarEmpaque(contenedorData.fk_OT, contenedorData.fk_Empaque, contenedorData.fk_Esquema, contenedorData.Token, valido, msgError)

                    If Program.ImagingGlobal.ProyectoImagingRow.Usa_Validacion_Empaque_Campo Then
                        valido = dbmImaging.SchemaProcess.PA_Empaque_Validar_Token.DBExecute(contenedorData.fk_OT, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(contenedorData.fk_Empaque), contenedorData.Token)
                        If (valido = False) Then
                            Throw New Exception("El Cod de barras: " & contenedorData.Token.ToString() & " no pertenece a la OT actual")
                        End If
                    End If

                    If (valido) Then
                        IdEmpaqueContenedor = dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBNextId(IdOT, IdEmpaque)
                        contenedorData.id_Empaque_Contenedor = IdEmpaqueContenedor
                        nIdEmpaqueContenedor = IdEmpaqueContenedor

                        Dim ContenedorDataAnterior = dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBFindByfk_OTToken(IdOT, contenedorData.Token)
                        If ContenedorDataAnterior.Rows.Count > 0 Then
                            Throw New Exception("Ya existe un contenedor con este código para la OT [" + IdOT.ToString + "]")
                        End If

                        'Validar si contenedor ya fue empacado en otra OT
                        If Program.ImagingGlobal.ProyectoImagingRow.Usa_Validacion_Empaque_Contenedor Then
                            Dim ValidacionOTContenedorDataAnterior = dbmImaging.SchemaProcess.PA_Validar_Empaque_Contenedor.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, contenedorData.Token)
                            If ValidacionOTContenedorDataAnterior.Rows.Count > 0 Then
                                Throw New Exception("Ya existe un contenedor con este código para la OT [" + ValidacionOTContenedorDataAnterior.Rows(0).Item(0).ToString + "]")
                            End If
                        End If

                        dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBInsert(contenedorData)
                        Current_Esquema = contenedorData.fk_Esquema
                        ContenedorEsquemaComboBox.Enabled = False

                        Dim savedRow = dbmImaging.SchemaProcess.CTA_Empaque_Contenedor_Resumido.DBFindByfk_OTfk_Empaqueid_Empaque_Contenedor(contenedorData.fk_OT, contenedorData.fk_Empaque, contenedorData.id_Empaque_Contenedor)

                        _ContenedoresListBox_IgnoreSelectedIndexChanged = True
                        Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoDataTable)
                        Dim newRow = data.NewCTA_Empaque_Contenedor_ResumidoRow
                        newRow.ItemArray = savedRow(0).ItemArray
                        data.AddCTA_Empaque_Contenedor_ResumidoRow(newRow)
                        CantContenedorLabel.Text = CType(data.Count, String)

                        ContenedoresDataGridView.ClearSelection()
                        Dim row = Contenedor_GetRowContenedor(IdEmpaqueContenedor)
                        If (row IsNot Nothing) Then row.Selected = True

                        If (DocumentoGroupBox.Visible) Then
                            ContenedorEsquemaComboBox.Enabled = False
                            ContenedorTokenTextBox.Text = contenedorData.Token
                            Documento_CargarDocumentosExistentes()
                            DocumentosDataGridView.Enabled = True
                            Documento_Iniciar()
                            'Else
                            '    ContenedorEsquemaComboBox.Enabled = True
                        End If
                    Else
                        Throw New Exception(msgError)
                    End If

                    _ContenedoresListBox_IgnoreSelectedIndexChanged = False
                Else
                    Dim contenedorInfoData = dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBGet(IdOT, IdEmpaque, IdEmpaqueContenedor)
                    If (contenedorInfoData.Count = 0) Then Throw New Exception("Id de contenedor no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdEmpaque)
                End If

                If (esInsertar And Not DocumentoGroupBox.Visible) Then
                    Contenedor_NuevoContenedor()
                Else
                    If (Not esInsertar) Then MessageBox.Show("Contenedor Actualizado Con Exito", "Guardado", MessageBoxButtons.OK)
                End If

                Dim ContenedorDestape = dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBFindByfk_OTToken(IdOT, contenedorData.Token)
                If ContenedorDestape.Rows.Count > 0 Then
                    Dim ContenedorEmpacado As New DBImaging.SchemaProcess.TBL_ContenedorType()
                    ContenedorEmpacado.Empacado = True

                    dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(ContenedorEmpacado, ContenedorDestape(0).fk_OT, ContenedorDestape(0).fk_Precinto, ContenedorDestape(0).fk_Contenedor)
                End If

                dbmImaging.Transaction_Commit()

                EventManager.FinalizarContenedorEmpaque(IdOT, IdEmpaque, nIdEmpaqueContenedor)

                Me.PrecintoInsertarButton.Text = "Actualizar Contenedor F3"

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Function Contenedor_BuscarContenedorToken(nToken As String) As DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow
            Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoDataTable)
            Dim docData = CType(data.Select(DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoEnum.Token.ColumnName & "='" & nToken & "'"), DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow())
            If (docData.Length > 0) Then
                Return docData(0)
            End If

            Return Nothing
        End Function

        Private Function Contenedor_BuscarContenedorId(nId As Short) As DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow
            Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoDataTable)
            Dim docData = CType(data.Select(DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoEnum.id_Empaque_Contenedor.ColumnName & "=" & nId), DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow())
            If (docData.Length > 0) Then
                Return docData(0)
            End If

            Return Nothing
        End Function

        Private Sub Contenedor_Eliminar()
            If (Not ContenedorEliminarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim fk_OT As Integer
            Dim fk_Empaque As Integer
            Dim id_Empaque_Contenedor As Integer
            Dim Token As String

            Try
                If (PrecintoCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite eliminar el contenedor puesto que el precinto se encuentra cerrado")
                End If

                Dim cnt = Contenedor_BuscarContenedorId(IdEmpaqueContenedor)
                If (cnt Is Nothing) Then Throw New ApplicationException("El contenedor con token " & ContenedorTokenTextBox.Text & " no se encuentra en el precinto")

                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para eliminar el contenedor", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceUrl, Program.DesktopGlobal.ClientIpAddress)) Then
                    Throw New Exception("No se permitió eliminar el contenedor")
                End If

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                fk_OT = cnt.fk_OT
                fk_Empaque = cnt.fk_Empaque
                id_Empaque_Contenedor = cnt.id_Empaque_Contenedor

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBGet(cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el contenedor en la base de datos IdOT=" & cnt.fk_OT & " IdEmpaque=" & cnt.fk_Empaque & " IdEmpContenedor=" & cnt.id_Empaque_Contenedor)
                Token = originalRow(0).Token

                dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBDelete(cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)

                Dim data = CType(ContenedoresDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoDataTable)
                _ContenedoresListBox_IgnoreSelectedIndexChanged = True
                ContenedoresDataGridView.ClearSelection()
                data.RemoveCTA_Empaque_Contenedor_ResumidoRow(cnt)
                _ContenedoresListBox_IgnoreSelectedIndexChanged = False

                Contenedor_NuevoContenedor()

                dbmImaging.Transaction_Commit()
                EventManager.FinalizarContenedorEmpaqueEliminar(fk_OT, Token)
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
                    Throw New ApplicationException("No se permite abrir el contenedor cuando el empaque se realiza a nivel de contenedor")
                End If

                If (PrecintoCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite abrir el contenedor puesto que el precinto se encuentra cerrado")
                End If

                If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Se requiere autorización de un usuario con Perfil Superior para abrir el contenedor", Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceUrl, Program.DesktopGlobal.ClientIpAddress)) Then
                    Throw New Exception("No se permitió abrir el contenedor")
                End If

                Dim cnt = Contenedor_BuscarContenedorToken(ContenedorTokenTextBox.Text)
                If (cnt Is Nothing) Then Throw New ApplicationException("El contenedor con token " & ContenedorTokenTextBox.Text & " no se encuentra en el precinto")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBGet(cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el contenedor en la base de datos IdOT=" & cnt.fk_OT & " IdEmpaque=" & cnt.fk_Empaque & " IdEmpContenedor=" & cnt.id_Empaque_Contenedor)

                Dim contenedor = New DBImaging.SchemaProcess.TBL_Empaque_ContenedorType() With {
                     .Cerrado = False,
                     .fk_Usuario_Apertura = Program.Sesion.Usuario.id,
                     .Fecha_Apertura = SlygNullable.SysDate
                 }

                dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBUpdate(contenedor, cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                Dim saveRows = dbmImaging.SchemaProcess.CTA_Empaque_Contenedor_Resumido.DBFindByfk_OTfk_Empaqueid_Empaque_Contenedor(cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                cnt.ItemArray = saveRows(0).ItemArray

                Dim row = Contenedor_GetRowContenedor(cnt.id_Empaque_Contenedor)
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
                    Throw New ApplicationException("No se permite cerrar el contenedor cuando el empaque se realiza a nivel de contenedor")
                End If

                Dim documentosData = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)
                If (documentosData.Count = 0) Then
                    Throw New ApplicationException("No se permite cerrar el contenedor puesto que no se ha registrado ningún documento")
                End If

                Dim cnt = Contenedor_BuscarContenedorId(IdEmpaqueContenedor)
                If (cnt Is Nothing) Then Throw New ApplicationException("El contenedor con token " & ContenedorTokenTextBox.Text & " no se encuentra en el precinto")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBGet(cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el contenedor en la base de datos IdOT=" & cnt.fk_OT & " IdEmpaque=" & cnt.fk_Empaque & " IdEmpContenedor=" & cnt.id_Empaque_Contenedor)

                Dim contenedor = New DBImaging.SchemaProcess.TBL_Empaque_ContenedorType() With {
                    .Cerrado = True,
                    .fk_Usuario_Cierre = Program.Sesion.Usuario.id,
                    .Fecha_Cierre = SlygNullable.SysDate
                }

                dbmImaging.SchemaProcess.TBL_Empaque_Contenedor.DBUpdate(contenedor, cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                Dim saveRows = dbmImaging.SchemaProcess.CTA_Empaque_Contenedor_Resumido.DBFindByfk_OTfk_Empaqueid_Empaque_Contenedor(cnt.fk_OT, cnt.fk_Empaque, cnt.id_Empaque_Contenedor)
                cnt.ItemArray = saveRows(0).ItemArray

                Dim row = Contenedor_GetRowContenedor(cnt.id_Empaque_Contenedor)
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

            IdEmpaqueContenedor = 0
            Contenedor_Iniciar()
            ContenedorInsertarButton.Text = "Insertar Contenedor F3"
            ContenedorCerradoLabel.Visible = False

            ContenedorAbrirButton.Enabled = False
            ContenedorCerrarButton.Enabled = False
            ContenedorEliminarButton.Enabled = False
            DocumentosDataGridView.Enabled = False

            DocumentoTokenTextBox.ReadOnly = True

            _ContenedoresListBox_IgnoreSelectedIndexChanged = True
            ContenedoresDataGridView.ClearSelection()
            _ContenedoresListBox_IgnoreSelectedIndexChanged = False
        End Sub

        Private Sub Contenedor_Seleccionar()
            If (Not _ContenedoresListBox_IgnoreSelectedIndexChanged) Then
                Contenedor_LimpiarControles()
                Documento_LimpiarControles()
                Documento_SetDataSourceDocumentos(New DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)

                If (ContenedoresDataGridView.SelectedRows.Count > 0) Then
                    Dim it = ContenedoresDataGridView.SelectedRows(0)
                    If (it Is Nothing) Then
                        Contenedor_NuevoContenedor()
                    Else
                        Dim row = CType(CType(it.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Empaque_Contenedor_ResumidoRow)
                        IdEmpaqueContenedor = row.id_Empaque_Contenedor
                        ContenedorTokenTextBox.Text = row.Token

                        Me.ContenedorInsertarButton.Text = "Actualizar Contenedor F3"
                        Documento_CargarDocumentosExistentes()
                        If (row.Cerrado) Then
                            ContenedorAbrirButton.Enabled = True
                            ContenedorCerrarButton.Enabled = False
                            ContenedorCerradoLabel.Visible = True
                        Else
                            Documento_Iniciar()
                            ContenedorAbrirButton.Enabled = False
                            ContenedorCerrarButton.Enabled = True
                            ContenedorCerradoLabel.Visible = False
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
            Me.ContenedorDatosGroupBox.Enabled = True

            Me.ContenedorInsertarButton.Enabled = True
            Me.ContenedorAbrirButton.Enabled = False
            Me.ContenedorEliminarButton.Enabled = False
            Me.ContenedorCerrarButton.Enabled = False

            Me.ContenedorTokenTextBox.Focus()
        End Sub

        Private Sub Documento_CargarDocumentosExistentes()
            If (Not _ContenedoresListBox_IgnoreSelectedIndexChanged) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim documentosData = dbmImaging.SchemaProcess.CTA_Empaque_Documento_Resumido.DBFindByfk_OTfk_Empaquefk_Empaque_ContenedorToken(IdOT, IdEmpaque, IdEmpaqueContenedor, Nothing)
                    Documento_SetDataSourceDocumentos(documentosData)

                    CantDocLabel.Text = CType(documentosData.Count, String)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub Documento_Iniciar()
            DocumentoTokenTextBox.ReadOnly = False
            DocumentoTokenTextBox.Text = ""
            'Try : DocumentoTipoComboBox.SelectedIndex = 0 : Catch : End Try
            DocumentoTokenTextBox.Focus()
        End Sub

        Private Function Documento_BuscarDocumento(nToken As String) As DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoRow
            Dim data = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)
            Dim docData = CType(data.Select(DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoEnum.Token.ColumnName & "='" & nToken & "'"), DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoRow())
            If (docData.Length > 0) Then
                Return docData(0)
            End If

            Return Nothing
        End Function

        Private Sub Documento_Guardar()
            If (Not DocumentoInsertarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                If (Not DocumentoGroupBox.Visible) Then
                    Throw New ApplicationException("No se permite guardar el documento cuando el empaque se realiza a nivel de contenedor")
                End If

                If (ContenedorCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite guardar el documento puesto que el contenedor se encuentra cerrado")
                End If

                If (IdEmpaqueContenedor = 0) Then
                    Throw New ApplicationException("Primero debe guardar el contenedor correspondiente")
                End If

                If (DocumentoTokenTextBox.Text.Trim() = "") Then
                    DocumentoTokenTextBox.Focus()
                    Throw New ApplicationException("El código del documento no puede estar vacio")
                End If

                Dim doc = Documento_BuscarDocumento(DocumentoTokenTextBox.Text)
                If (doc IsNot Nothing) Then Throw New ApplicationException("El documento con token " & DocumentoTokenTextBox.Text & " ya se encuentra en el contenedor")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim docDestape = dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBFindByfk_OTToken(IdOT, DocumentoTokenTextBox.Text)
                If (docDestape.Count = 0) Then Throw New ApplicationException("El documento con codigo " & DocumentoTokenTextBox.Text & " no se encuentra destapado en la OT (" & IdOT & ")")

                Dim documentoInfo = New DBImaging.SchemaProcess.TBL_Empaque_DetalleType With {
                        .fk_OT = IdOT,
                        .fk_Empaque = IdEmpaque,
                        .fk_Empaque_Contenedor = IdEmpaqueContenedor,
                        .Token = DocumentoTokenTextBox.Text
                    }

                dbmImaging.SchemaProcess.TBL_Empaque_Detalle.DBInsert(documentoInfo)

                _DocumentosListBox_IgnoreSelectedIndexChanged = True
                Dim inserted = dbmImaging.SchemaProcess.CTA_Empaque_Documento_Resumido.DBFindByfk_OTfk_Empaquefk_Empaque_ContenedorToken(IdOT, IdEmpaque, IdEmpaqueContenedor, DocumentoTokenTextBox.Text)

                Dim data = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)
                Dim newRow = data.NewCTA_Empaque_Documento_ResumidoRow
                newRow.ItemArray = inserted(0).ItemArray
                data.AddCTA_Empaque_Documento_ResumidoRow(newRow)
                _DocumentosListBox_IgnoreSelectedIndexChanged = False

                If docDestape.Rows.Count > 0 Then
                    Dim DocumentoEmpacado As New DBImaging.SchemaProcess.TBL_Contenedor_DetalleType()
                    DocumentoEmpacado.Empacado = True

                    dbmImaging.SchemaProcess.TBL_Contenedor_Detalle.DBUpdate(DocumentoEmpacado, docDestape(0).fk_OT, docDestape(0).fk_Precinto, docDestape(0).fk_Contenedor, _DocumentoTokenTextBox.Text)

                    Dim ContenedorEmpacado As New DBImaging.SchemaProcess.TBL_ContenedorType()
                    ContenedorEmpacado.Empacado = True

                    dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(ContenedorEmpacado, docDestape(0).fk_OT, docDestape(0).fk_Precinto, docDestape(0).fk_Contenedor)
                End If

                DocumentoTokenTextBox.Text = ""
                'Try : DocumentoTipoComboBox.SelectedIndex = 0 : Catch : End Try

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
                    Throw New ApplicationException("No se permite eliminar el documento cuando el empaque se realiza a nivel de contenedor")
                End If

                If (ContenedorCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite eliminar el documento puesto que el contenedor se encuentra cerrado")
                End If

                Dim doc = Documento_BuscarDocumento(DocumentoTokenTextBox.Text)
                If (doc Is Nothing) Then Throw New ApplicationException("El documento con token " & DocumentoTokenTextBox.Text & " no se encuentra en el contenedor")

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                Dim originalRow = dbmImaging.SchemaProcess.TBL_Empaque_Detalle.DBGet(doc.fk_OT, doc.fk_Empaque, doc.fk_Empaque_Contenedor, doc.Token)
                If (originalRow.Count = 0) Then Throw New Exception("No se encontró el documento en la base de datos IdOT=" & doc.fk_OT & " IdEmpaque=" & doc.fk_Empaque & " IdEmpContenedor=" & doc.fk_Empaque_Contenedor & " Token" & doc.Token)

                dbmImaging.SchemaProcess.TBL_Empaque_Detalle.DBDelete(doc.fk_OT, doc.fk_Empaque, doc.fk_Empaque_Contenedor, doc.Token)

                Dim data = CType(DocumentosDataGridView.DataSource, DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoDataTable)
                _DocumentosListBox_IgnoreSelectedIndexChanged = True
                DocumentosDataGridView.ClearSelection()
                data.RemoveCTA_Empaque_Documento_ResumidoRow(doc)
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
            'Try : DocumentoTipoComboBox.SelectedIndex = 0 : Catch : End Try

        End Sub

        Private Sub Documento_NuevoDocumento()
            If (Not DocumentoNuevoButton.Enabled) Then Return

            Try
                If (ContenedorCerradoLabel.Visible) Then
                    Throw New ApplicationException("No se permite agregar documentos puesto que el contenedor se encuentra cerrado")
                End If

                If (IdEmpaqueContenedor = 0) Then
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
                        Dim row = CType(CType(it.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Empaque_Documento_ResumidoRow)
                        DocumentoTokenTextBox.Text = row.Token
                        DocumentoTokenTextBox.ReadOnly = True
                        DocumentoTokenTextBox.Focus()
                        DocumentoEliminarButton.Enabled = True
                        DocumentoInsertarButton.Enabled = False
                    End If
                End If
            End If
        End Sub

        Private Sub IniciarKeyManager(nControl As Control)
            AddHandler nControl.KeyDown, AddressOf KeyManager_KeyDown
            For Each ctr As Control In nControl.Controls
                IniciarKeyManager(ctr)
            Next
        End Sub
#End Region

    End Class

End Namespace