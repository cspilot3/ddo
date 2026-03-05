Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Procesos.Destape
    Public Class FormDestapeCorreccion
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
            ContenedoresDataGridView.AutoGenerateColumns = False

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
                Contenedor_LimpiarControles()
                'Contenedor_Iniciar()
            End If
        End Sub

        Private Sub KeyManager_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs)
            If (Not e.Control And Not e.Shift) Then
                Select Case e.KeyCode
                    Case Keys.F2 : Precinto_Guardar()
                    Case Keys.F3 : Contenedor_Guardar()
                        'Case Keys.F4 : Documento_Guardar()
                        'Case Keys.F5 : Contenedor_CerrarContenedor()
                        'Case Keys.F6 : Precinto_Finalizar()
                End Select
            End If
            If (e.Control And Not e.Shift) Then
                Select Case e.KeyCode
                    'Case Keys.F6 : Precinto_Abrir()
                    'Case Keys.F5 : Contenedor_AbrirContenedor()
                    'Case Keys.F3 : Contenedor_NuevoContenedor()
                    'Case Keys.F4 : Documento_NuevoDocumento()
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

        Private Sub PrecintoLastCampoDinamico_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.MenuPanel.Focus()
            End If
        End Sub

        Private Sub PrecintoGroupBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles PrecintoGroupBox.Enter
            PrecintoInsertarButton.Visible = True
            ContenedorInsertarButton.Visible = False
        End Sub

        Private Sub ContenedorInsertarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ContenedorInsertarButton.Click
            Contenedor_Guardar()
        End Sub

        Private Sub ContenedoresDataGridView_MouseDoubleClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles ContenedoresDataGridView.MouseDoubleClick
            Contenedor_Seleccionar()
        End Sub

        Private Sub ContenedorLastCampoDinamico_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            If (e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab)) Then
                e.Handled = True
                Me.MenuPanel.Focus()
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
            PrecintoInsertarButton.Visible = False
            ContenedorInsertarButton.Visible = True
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
                                .Es_Modificable = row.Es_Modificable
                            End With
                            If (Not row.Isfk_Campo_ListaNull()) Then
                                campoControl.CampoLista = row.fk_Campo_Lista
                            End If
                            lastIdCampo = row.id_Campo
                            _CamposDinamicosPrecinto.Add(campoControl)
                        End If
                    Next

                End If

                _CamposDinamicosPrecintoTableForm = Utilities.CreaControlesImagingCorreccion(dbmImaging, _CamposDinamicosPrecinto, Program.DesktopGlobal.ConnectionStrings, Program.ImagingGlobal, True, 213, True)
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
                    Me.PrecintoInsertarButton.Enabled = False
                    Throw New ApplicationException("El precinto no se encuentra destapado")
                Else
                    Dim dataPrecinto = dbmImaging.SchemaProcess.TBL_Precinto.DBFindByfk_OTPrecinto(IdOT, NroPrecinto)

                    If (dataPrecinto.Rows.Count > 0) Then
                        If dataPrecinto(0).Cerrado Then

                            PrecintoCamposDinamicosPanel.Enabled = True
                            PrecintoNumeroTextBox.ReadOnly = True
                        Else
                            Throw New ApplicationException("El precinto no se encuentra cerrado")
                        End If
                    Else
                        Throw New ApplicationException("El precinto no se encuentra destapado")
                    End If

                    If (dataPrecinto.Rows.Count > 0) Then
                        IdPrecinto = dataPrecinto(0).id_Precinto
                        Precinto_CargarDataExistenteCamposDinamicos(dataPrecinto(0).ToTBL_PrecintoType)
                        Contenedor_CargarContenedoresExistentes()
                    End If

                    Me.PrecintoInsertarButton.Visible = True

                    Dim primerCampo = Precinto_BuscarCampoDinamico(1)
                    If (primerCampo IsNot Nothing) Then primerCampo.Select()
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
                    Me.PrecintoInsertarButton.Enabled = False
                    Throw New ApplicationException("El precinto no se encuentra destapado")
                Else

                    Dim precintoInfoData = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(IdOT, IdPrecinto)

                    If (precintoInfoData.Count > 0) Then

                        Dim idPrecintoLog = dbmImaging.SchemaAudit.TBL_Precinto_Log.DBNextId
                        Dim precintoLog = New DBImaging.SchemaAudit.TBL_Precinto_LogType With {
                            .id_Precinto_Log = idPrecintoLog,
                            .fk_OT = IdOT,
                            .fk_Precinto = IdPrecinto,
                            .fk_Usuario_Log = Program.Sesion.Usuario.id,
                            .Fecha_Log = Date.Now
                        }
                        dbmImaging.SchemaAudit.TBL_Precinto_Log.DBInsert(precintoLog)

                        For Each campoPrecintoData As Control In _CamposDinamicosPrecintoTableForm.Controls
                            If Not (campoPrecintoData.GetType().ToString() = "System.Windows.Forms.Label") Then
                                Dim precintoDimData = New DBImaging.SchemaProcess.TBL_Precinto_DataType With {
                                    .fk_OT = IdOT,
                                    .fk_Precinto = IdPrecinto,
                                    .fk_Campo = CType(campoPrecintoData.Tag, DesktopConfig.CrearControlesType).fk_Campo
                                }

                                Select Case campoPrecintoData.GetType()
                                    Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                        precintoDimData.Data_Campo = CType(campoPrecintoData, DesktopTextBox.DesktopTextBoxControl).Text
                                    Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                        precintoDimData.Data_Campo = CType(campoPrecintoData, DesktopCheckBox.DesktopCheckBoxControl).Checked
                                    Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                        precintoDimData.Data_Campo = CType(campoPrecintoData, DesktopComboBox.DesktopComboBoxControl).SelectedValue
                                End Select


                                Dim data = dbmImaging.SchemaProcess.TBL_Precinto_Data.DBGet(precintoDimData.fk_OT, precintoDimData.fk_Precinto, precintoDimData.fk_Campo)

                                If (data.Count > 0) Then
                                    dbmImaging.SchemaProcess.TBL_Precinto_Data.DBUpdate(precintoDimData, precintoDimData.fk_OT, precintoDimData.fk_Precinto, precintoDimData.fk_Campo)

                                    Dim precintoLogDetalle = New DBImaging.SchemaAudit.TBL_Precinto_Log_DetalleType With {
                                    .fk_Precinto_Log = idPrecintoLog,
                                    .fk_Campo = precintoDimData.fk_Campo,
                                    .Valor_Antiguo = data(0).Data_Campo,
                                    .Valor_Nuevo = precintoDimData.Data_Campo
                                }
                                    dbmImaging.SchemaAudit.TBL_Precinto_Log_Detalle.DBInsert(precintoLogDetalle)

                                End If
                            End If
                        Next

                        dbmImaging.Transaction_Commit()

                        Contenedor_CargarContenedoresExistentes()
                        Me.Contenedor_LimpiarControles()

                        MessageBox.Show("Precinto Actualizado Con Exito", "Guardado", MessageBoxButtons.OK)

                        Me.PrecintoInsertarButton.Text = "Actualizar Precinto   F2"

                    Else
                        Throw New Exception("Id de precinto no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdPrecinto)
                    End If
                End If
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Precinto_DeshabilitarCampos()
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
                            .Es_Modificable = row.Es_Modificable
                        End With
                        If (Not row.Isfk_Campo_ListaNull()) Then
                            campoControl.CampoLista = row.fk_Campo_Lista
                        End If
                        lastIdCampo = row.id_Campo
                        _CamposDinamicosContenedor.Add(campoControl)
                    End If
                Next

                _CamposDinamicosContenedorTableForm = Utilities.CreaControlesImagingCorreccion(dbmImaging, _CamposDinamicosContenedor, Program.DesktopGlobal.ConnectionStrings, Program.ImagingGlobal, True, 213, True)
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

        Private Sub Contenedor_Guardar()
            If (Not ContenedorInsertarButton.Enabled) Then Return

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim _nidContenedor As Integer = 0

            Try
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

                Dim contenedorInfoData = dbmImaging.SchemaProcess.TBL_Contenedor.DBGet(IdOT, IdPrecinto, IdContenedor)
                If (contenedorInfoData.Count > 0) Then

                    Dim idContenedorLog = dbmImaging.SchemaAudit.TBL_Contenedor_Log.DBNextId
                    Dim contenedorLog = New DBImaging.SchemaAudit.TBL_Contenedor_LogType With {
                        .id_Contendor_Log = idContenedorLog,
                        .fk_OT = IdOT,
                        .fk_Precinto = IdPrecinto,
                        .fk_Contenedor = IdContenedor,
                        .fk_Usuario_Log = Program.Sesion.Usuario.id,
                        .Fecha_Log = SlygNullable.SysDate
                    }
                    dbmImaging.SchemaAudit.TBL_Contenedor_Log.DBInsert(contenedorLog)

                    If (_CamposDinamicosContenedorTableForm.Controls.Count > 0) Then
                        For Each campoContenedorData As Control In _CamposDinamicosContenedorTableForm.Controls
                            If Not (campoContenedorData.GetType().ToString() = "System.Windows.Forms.Label") Then

                                Dim contenedorDimData = New DBImaging.SchemaProcess.TBL_Contenedor_DataType With {
                                    .fk_OT = IdOT,
                                    .fk_Precinto = IdPrecinto,
                                    .fk_Contenedor = IdContenedor,
                                    .fk_Campo = CType(campoContenedorData.Tag, DesktopConfig.CrearControlesType).fk_Campo
                                }

                                Select Case campoContenedorData.GetType()
                                    Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                        contenedorDimData.Data_Campo = CType(campoContenedorData, DesktopTextBox.DesktopTextBoxControl).Text
                                    Case GetType(DesktopCheckBox.DesktopCheckBoxControl)
                                        contenedorDimData.Data_Campo = CType(campoContenedorData, DesktopCheckBox.DesktopCheckBoxControl).Checked
                                    Case GetType(DesktopComboBox.DesktopComboBoxControl)
                                        contenedorDimData.Data_Campo = CType(campoContenedorData, DesktopComboBox.DesktopComboBoxControl).SelectedValue
                                End Select


                                Dim data = dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBGet(contenedorDimData.fk_OT, contenedorDimData.fk_Precinto, contenedorDimData.fk_Contenedor, contenedorDimData.fk_Campo)
                                If (data.Count > 0) Then
                                    dbmImaging.SchemaProcess.TBL_Contenedor_Data.DBUpdate(contenedorDimData, contenedorDimData.fk_OT, contenedorDimData.fk_Precinto, contenedorDimData.fk_Contenedor, contenedorDimData.fk_Campo)

                                    Dim contenedorLogDetalle = New DBImaging.SchemaAudit.TBL_Contenedor_Log_DetalleType With {
                                    .fk_Contenedor_Log = idContenedorLog,
                                    .fk_Campo = contenedorDimData.fk_Campo,
                                    .Valor_Antiguo = data(0).Data_Campo,
                                    .Valor_Nuevo = contenedorDimData.Data_Campo
                                }
                                    dbmImaging.SchemaAudit.TBL_Contenedor_Log_Detalle.DBInsert(contenedorLogDetalle)
                                End If
                            End If
                        Next
                    End If

                Else
                    Throw New Exception("Id de contenedor no encontrado IdOT: " & IdOT & " IdPrecinto: " & IdPrecinto)
                End If

                MessageBox.Show("Contenedor Actualizado Con Exito", "Guardado", MessageBoxButtons.OK)

                dbmImaging.Transaction_Commit()

                Me.PrecintoInsertarButton.Text = "Actualizar Contenedor F3"
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Contenedor_Seleccionar()
            If (Not _ContenedoresListBox_IgnoreSelectedIndexChanged) Then
                Contenedor_LimpiarControles()

                If (ContenedoresDataGridView.SelectedRows.Count > 0) Then
                    Dim it = ContenedoresDataGridView.SelectedRows(0)
                    If (it IsNot Nothing) Then
                        Dim row = CType(CType(it.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Destape_Contenedor_ResumidoRow)
                        IdContenedor = row.id_Contenedor
                        Contenedor_CargarDataExistenteCamposDinamicos(row.ToCTA_Destape_Contenedor_ResumidoType)
                        Me.ContenedorInsertarButton.Text = "Actualizar Contenedor F3"

                        'ContenedorCerradoLabel.Visible = False
                        ContenedorInsertarButton.Visible = True
                        PrecintoInsertarButton.Visible = False
                        ContenedorCamposDinamicosPanel.Enabled = True
                        ContenedorTokenTextBox.Focus()
                End If
                End If
            End If
        End Sub

        Private Sub Contenedor_DeshabilitarCampos()
            Me.ContenedorInsertarButton.Enabled = False
            Me.ContenedorDatosGroupBox.Enabled = False
        End Sub

        Private Sub Contenedor_LimpiarControles()
            If Not (_CamposDinamicosContenedorTableForm Is Nothing) Then
                For Each campoContenedorData As Control In _CamposDinamicosContenedorTableForm.Controls
                    If Not (campoContenedorData.GetType().ToString() = "System.Windows.Forms.Label") Then
                        Select Case campoContenedorData.GetType()
                            Case GetType(DesktopTextBox.DesktopTextBoxControl)
                                CType(campoContenedorData, DesktopTextBox.DesktopTextBoxControl).Text = String.Empty
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
            Me.ContenedorTokenTextBox.Text = String.Empty
            Me.ContenedorTokenTextBox.Focus()
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

#End Region

    End Class
End Namespace