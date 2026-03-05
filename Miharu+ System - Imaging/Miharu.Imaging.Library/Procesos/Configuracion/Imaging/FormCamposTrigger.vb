Namespace Procesos.Configuracion.Imaging
    Public Class FormCamposTrigger

#Region " Declaraciones "

        Private dataCargando As Boolean = False
        Private Id_Etapa As Integer
        Private _campoRow As DBImaging.SchemaConfig.CTA_Campo_ParametrizacionRow

#End Region

#Region "Propiedades"

        Public Property CampoRow() As DBImaging.SchemaConfig.CTA_Campo_ParametrizacionRow
            Get
                Return _campoRow
            End Get
            Set(ByVal value As DBImaging.SchemaConfig.CTA_Campo_ParametrizacionRow)
                _campoRow = value
            End Set
        End Property

#End Region

#Region "Eventos"

        Private Sub AñadirButtonClick(sender As System.Object, e As EventArgs) Handles AñadirButton.Click
            Try
                For Each item As DataRowView In CamposListBox.SelectedItems
                    'Dim campoListaRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.CampoRow)
                    'campoListaRow.Ocultar = False

                    If Me.ValorComboBox.Visible = True Then
                        Dim campoListaRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.CampoRow)
                        campoListaRow.Ocultar = False

                        Dim campoTriggerRow = TriggerDataSet.CampoTrigger.NewCampoTriggerRow
                        With campoTriggerRow
                            .id_Lista = CStr(ValorComboBox.SelectedValue)
                            .id_Etapa = CByte(EtapaComboBox.SelectedValue)
                            .id_Campo = campoListaRow.id_Campo
                            .id_Campo_Tabla = campoListaRow.id_Campo_Tabla
                            .Nombre_Campo = campoListaRow.Nombre_Campo
                        End With
                        TriggerDataSet.CampoTrigger.AddCampoTriggerRow(campoTriggerRow)
                    ElseIf Me.ValorTextBox.Visible = True Then

                        Dim campoListaRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.ValidacionRow)
                        campoListaRow.Ocultar = False

                        Dim campoTBL_TriggerRow = TriggerDataSet.TBL_CampoTrigger.NewTBL_CampoTriggerRow
                        With campoTBL_TriggerRow
                            .id_Etapa = CByte(EtapaComboBox.SelectedValue)
                            .fk_Validacion = campoListaRow.fk_Validacion_Ocultar
                            .Nombre_Campo = campoListaRow.Pregunta_Validacion
                            .fk_Documento = campoListaRow.fk_Documento
                        End With
                        TriggerDataSet.TBL_CampoTrigger.AddTBL_CampoTriggerRow(campoTBL_TriggerRow)

                    End If

                Next
            Catch
                Throw
            Finally
                CargarCampos()
            End Try
        End Sub

        Private Sub AñadirTodosButtonClick(sender As System.Object, e As EventArgs) Handles AñadirTodosButton.Click
            Try
                For Each item As DataRowView In CamposListBox.Items
                    If Me.ValorComboBox.Visible = True Then

                        Dim campoListaRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.CampoRow)
                        campoListaRow.Ocultar = False

                        Dim campoTriggerRow = TriggerDataSet.CampoTrigger.NewCampoTriggerRow
                        With campoTriggerRow
                            .id_Lista = CStr(ValorComboBox.SelectedValue)
                            .id_Etapa = CByte(EtapaComboBox.SelectedValue)
                            .id_Campo = campoListaRow.id_Campo
                            .id_Campo_Tabla = campoListaRow.id_Campo_Tabla
                            .Nombre_Campo = campoListaRow.Nombre_Campo
                        End With
                        TriggerDataSet.CampoTrigger.AddCampoTriggerRow(campoTriggerRow)
                    ElseIf Me.ValorTextBox.Visible = True Then
                        Dim campoListaRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.ValidacionRow)
                        campoListaRow.Ocultar = False

                        Dim campoTBL_TriggerRow = TriggerDataSet.TBL_CampoTrigger.NewTBL_CampoTriggerRow
                        With campoTBL_TriggerRow
                            .id_Etapa = CByte(EtapaComboBox.SelectedValue)
                            .fk_Validacion = campoListaRow.fk_Validacion_Ocultar
                            .Nombre_Campo = campoListaRow.Pregunta_Validacion
                            .fk_Documento = campoListaRow.fk_Documento
                        End With
                        TriggerDataSet.TBL_CampoTrigger.AddTBL_CampoTriggerRow(campoTBL_TriggerRow)

                    End If

                Next
            Catch
                Throw
            Finally
                CargarCampos()
            End Try
        End Sub

        Private Sub QuitarButtonClick(sender As System.Object, e As EventArgs) Handles QuitarButton.Click
            Try
                If Me.ValorComboBox.Visible = True Then
                    Dim camposTriggerBorrar As New List(Of DBImaging.Esquemas.xsdTrigger.CampoTriggerRow)
                    For Each item As DataRowView In AñadirCamposListBox.SelectedItems
                        Dim campoTriggerRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.CampoTriggerRow)
                        camposTriggerBorrar.Add(campoTriggerRow)
                        Dim campoListaRow = TriggerDataSet.Campo.FindByid_Campoid_Campo_Tabla(campoTriggerRow.id_Campo, campoTriggerRow.id_Campo_Tabla)
                        campoListaRow.Ocultar = False
                    Next

                    For Each campoTriggerRow As DBImaging.Esquemas.xsdTrigger.CampoTriggerRow In camposTriggerBorrar
                        Dim CampoTriggerBorrar = TriggerDataSet.CampoTrigger.FindByid_Listaid_Etapaid_Campoid_Campo_Tabla(campoTriggerRow.id_Lista, campoTriggerRow.id_Etapa, campoTriggerRow.id_Campo, campoTriggerRow.id_Campo_Tabla)
                        TriggerDataSet.CampoTrigger.Rows.Remove(CampoTriggerBorrar)
                    Next

                ElseIf Me.ValorTextBox.Visible = True Then

                    Dim TBL_camposTriggerBorrar As New List(Of DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow)

                    For Each item As DataRowView In AñadirCamposListBox.SelectedItems
                        Dim campoTriggerRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow)
                        TBL_camposTriggerBorrar.Add(campoTriggerRow)
                        'Dim campoListaRow = TriggerDataSet.Validacion.FindByfk_Validacion_Ocultarfk_Documento(campoTriggerRow.fk_Validacion, campoTriggerRow.fk_Documento)
                        Dim campoListaRow = TriggerDataSet.Validacion.FindByfk_Validacion_Ocultarfk_Documentofk_Etapa_Captura(campoTriggerRow.fk_Validacion, campoTriggerRow.fk_Documento, campoTriggerRow.id_Etapa)
                        campoListaRow.Ocultar = False
                    Next

                    For Each campoTriggerRow As DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow In TBL_camposTriggerBorrar
                        Dim CampoTriggerBorrar = TriggerDataSet.TBL_CampoTrigger.FindByid_Etapafk_Validacion(campoTriggerRow.id_Etapa, campoTriggerRow.fk_Validacion)
                        TriggerDataSet.TBL_CampoTrigger.Rows.Remove(CampoTriggerBorrar)
                    Next

                End If

                
            Catch
                Throw
            Finally
                CargarCampos()
            End Try
        End Sub

        Private Sub QuitarTodosButtonClick(sender As System.Object, e As EventArgs) Handles QuitarTodosButton.Click
            Try
                If Me.ValorComboBox.Visible = True Then
                    Dim camposTriggerBorrar As New List(Of DBImaging.Esquemas.xsdTrigger.CampoTriggerRow)
                    For Each item As DataRowView In AñadirCamposListBox.Items
                        Dim campoTriggerRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.CampoTriggerRow)
                        camposTriggerBorrar.Add(campoTriggerRow)
                        Dim campoListaRow = TriggerDataSet.Campo.FindByid_Campoid_Campo_Tabla(campoTriggerRow.id_Campo, campoTriggerRow.id_Campo_Tabla)
                        campoListaRow.Ocultar = False
                    Next

                    For Each campoTriggerRow As DBImaging.Esquemas.xsdTrigger.CampoTriggerRow In camposTriggerBorrar
                        Dim CampoTriggerBorrar = TriggerDataSet.CampoTrigger.FindByid_Listaid_Etapaid_Campoid_Campo_Tabla(campoTriggerRow.id_Lista, campoTriggerRow.id_Etapa, campoTriggerRow.id_Campo, campoTriggerRow.id_Campo_Tabla)
                        TriggerDataSet.CampoTrigger.Rows.Remove(CampoTriggerBorrar)
                    Next
                ElseIf Me.ValorTextBox.Visible = True Then

                    Dim TBL_camposTriggerBorrar As New List(Of DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow)

                    For Each item As DataRowView In AñadirCamposListBox.Items 'SelectedItems
                        Dim campoTriggerRow = CType(item.Row, DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow)
                        TBL_camposTriggerBorrar.Add(campoTriggerRow)
                        Dim campoListaRow = TriggerDataSet.Validacion.FindByfk_Validacion_Ocultarfk_Documentofk_Etapa_Captura(campoTriggerRow.fk_Validacion, campoTriggerRow.fk_Documento, campoTriggerRow.id_Etapa)
                        campoListaRow.Ocultar = False
                    Next

                    For Each campoTriggerRow As DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow In TBL_camposTriggerBorrar
                        Dim CampoTriggerBorrar = TriggerDataSet.TBL_CampoTrigger.FindByid_Etapafk_Validacion(campoTriggerRow.id_Etapa, campoTriggerRow.fk_Validacion)
                        TriggerDataSet.TBL_CampoTrigger.Rows.Remove(CampoTriggerBorrar)
                    Next

                End If

            Catch
                Throw
            Finally
                CargarCampos()
            End Try
        End Sub

        Private Sub FormCamposTriggerLoad(sender As System.Object, e As EventArgs) Handles MyBase.Load
            NombreCampoTextBox.Text = _campoRow.Nombre_Campo
            CargarData()
            CargarCampos()
        End Sub

        Private Sub ValorComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs)
            CargarCampos()
        End Sub

        Private Sub EtapaComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EtapaComboBox.SelectedIndexChanged
            CargarCampos()
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            GuardarTodo()
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub

#End Region

#Region "Metodos"

        Private Sub CargarCampos()
            If Not dataCargando Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    If Me.ValorComboBox.Visible = True Then

                        Dim camposData = dbmCore.SchemaConfig.CTA_Campos_Tabla_Asociada.DBFindByfk_Entidadfk_Documento(_campoRow.fk_Entidad, _campoRow.fk_Documento)
                        TriggerDataSet.Campo.Clear()

                        For Each tblCampoRow As DBCore.SchemaConfig.CTA_Campos_Tabla_AsociadaRow In camposData

                            Dim CampoItem = TriggerDataSet.Campo.NewCampoRow
                            With CampoItem
                                .id_Campo = tblCampoRow.id_Campo
                                .id_Campo_Tabla = CShort(tblCampoRow.id_Campo_Tabla)
                                .Nombre_Campo = tblCampoRow.Nombre_Campo_Completo
                                .Ocultar = False
                            End With
                            TriggerDataSet.Campo.AddCampoRow(CampoItem)
                        Next

                        For Each campoListaRow As DBImaging.Esquemas.xsdTrigger.CampoRow In TriggerDataSet.Campo.Rows
                            Dim campoTriggerRow = TriggerDataSet.CampoTrigger.FindByid_Listaid_Etapaid_Campoid_Campo_Tabla(CStr(ValorComboBox.SelectedValue), CByte(EtapaComboBox.SelectedValue), campoListaRow.id_Campo, campoListaRow.id_Campo_Tabla)
                            If campoTriggerRow IsNot Nothing Then
                                campoListaRow.Ocultar = True
                            Else
                                campoListaRow.Ocultar = False
                            End If
                        Next

                    ElseIf Me.ValorTextBox.Visible = True Then

                        Dim camposValidacionData = dbmImaging.SchemaConfig.CTA_Campos_Validaciones.DBFindByfk_Documentofk_Etapa_CapturaEliminado(_campoRow.fk_Documento, CByte(EtapaComboBox.SelectedValue), False) ' CByte(30)
                        TriggerDataSet.Validacion.Clear()

                        For Each ctaCampoTriggerRow As DBImaging.SchemaConfig.CTA_Campos_ValidacionesRow In camposValidacionData
                            Dim CampoTriggerItem = TriggerDataSet.Validacion.NewValidacionRow

                            With CampoTriggerItem
                                .fk_Validacion_Ocultar = ctaCampoTriggerRow.id_Validacion
                                .fk_Documento = ctaCampoTriggerRow.fk_Documento
                                .Pregunta_Validacion = ctaCampoTriggerRow.Pregunta_Validacion
                                .fk_Etapa_Captura = ctaCampoTriggerRow.fk_Etapa_Captura
                            End With

                            TriggerDataSet.Validacion.AddValidacionRow(CampoTriggerItem)
                        Next

                        For Each campoValidacionRow As DBImaging.Esquemas.xsdTrigger.ValidacionRow In TriggerDataSet.Validacion.Rows
                            Dim campoTBL_TriggerRow = TriggerDataSet.TBL_CampoTrigger.FindByid_Etapafk_Validacion(CByte(EtapaComboBox.SelectedValue), campoValidacionRow.fk_Validacion_Ocultar)
                            If campoTBL_TriggerRow IsNot Nothing Then
                                campoValidacionRow.Ocultar = True
                            Else
                                campoValidacionRow.Ocultar = False
                            End If
                        Next

                        For Each campoTriggerRow As DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow In TriggerDataSet.TBL_CampoTrigger.Rows
                            Dim campoValidacionRow = TriggerDataSet.Validacion.FindByfk_Validacion_Ocultarfk_Documentofk_Etapa_Captura(campoTriggerRow.fk_Validacion, campoTriggerRow.fk_Documento, campoTriggerRow.id_Etapa)
                            If campoValidacionRow IsNot Nothing Then
                                If campoValidacionRow.fk_Etapa_Captura = CByte(EtapaComboBox.SelectedValue) And campoValidacionRow.Ocultar = True Then
                                    campoTriggerRow.Ocultar = True
                                Else
                                    campoTriggerRow.Ocultar = False
                                End If
                            Else
                                campoTriggerRow.Ocultar = False
                            End If
                        Next

                        Me.CamposListBox.DataSource = MostrarValidacionBindingSource
                        Me.CamposListBox.DisplayMember = "Pregunta_Validacion"
                        Me.CamposListBox.ValueMember = "fk_Validacion_Ocultar"
                        Me.AñadirCamposListBox.DataSource = Ocultar1_BindingSource
                    End If

                    CamposListBox.Refresh()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, String.Format("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub CargarData()
            dataCargando = True
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim CampoData = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(_campoRow.fk_Entidad, _campoRow.fk_Documento, _campoRow.id_Campo)
                If CampoData.Rows.Count > 0 Then
                    Dim CampoCoreRow = CType(CampoData.Rows(0), DBCore.SchemaConfig.TBL_CampoRow)

                    If Not CampoCoreRow.Isfk_Campo_ListaNull Then

                        'Activa Control Valor Tipo Lista en Formulario
                        Me.ValorComboBox.Enabled = True
                        Me.ValorComboBox.Visible = True
                        Me.ValorTextBox.Enabled = False
                        Me.ValorTextBox.Visible = False
                        Me.lblMostrar.Text = "Campos a Mostrar"
                        Me.lblOcultar.Text = "Campos a Ocultar"
                        Me.OperadorComboBox.Enabled = False

                        Dim CampoListaItemData = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(CampoCoreRow.fk_Entidad, CampoCoreRow.fk_Campo_Lista)

                        For Each tblCampoListaItemRow As DBCore.SchemaConfig.TBL_Campo_Lista_ItemRow In CampoListaItemData
                            Dim ListaItem = TriggerDataSet.Lista.NewListaRow()
                            With ListaItem
                                .id_Lista = tblCampoListaItemRow.Valor_Campo_Lista_Item
                                .Nombre_Lista = tblCampoListaItemRow.Etiqueta_Campo_Lista_Item
                            End With

                            TriggerDataSet.Lista.AddListaRow(ListaItem)

                            Dim EtapasData = dbmImaging.SchemaConfig.TBL_Etapa_Captura.DBGet(Nothing)

                            For Each tblEtapaCapturaRow As DBImaging.SchemaConfig.TBL_Etapa_CapturaRow In EtapasData
                                Dim EtapaItem = TriggerDataSet.Etapa.NewEtapaRow

                                With EtapaItem
                                    .id_Lista = ListaItem.id_Lista
                                    .id_Etapa = tblEtapaCapturaRow.id_Etapa_Captura
                                    .Nombre_Etapa = tblEtapaCapturaRow.Nombre_Etapa_Captura
                                End With

                                TriggerDataSet.Etapa.AddEtapaRow(EtapaItem)

                                Dim camposData = dbmImaging.SchemaConfig.CTA_Campo_Trigger.DBFindByfk_Documentofk_Campo_Trigger(_campoRow.fk_Documento, _campoRow.id_Campo)

                                For Each ctaCampoTriggerRow As DBImaging.SchemaConfig.CTA_Campo_TriggerRow In camposData
                                    Dim CampoItem = TriggerDataSet.CampoTrigger.NewCampoTriggerRow

                                    If ctaCampoTriggerRow.fk_Etapa_Captura = EtapaItem.id_Etapa And ctaCampoTriggerRow.Valor = ListaItem.id_Lista Then
                                        With CampoItem
                                            .id_Lista = ListaItem.id_Lista
                                            .id_Etapa = EtapaItem.id_Etapa
                                            .id_Campo = ctaCampoTriggerRow.fk_Campo_Ocultar
                                            .id_Campo_Tabla = ctaCampoTriggerRow.fk_Campo_Tabla_Ocultar
                                            .Nombre_Campo = ctaCampoTriggerRow.Nombre_Campo
                                        End With

                                        TriggerDataSet.CampoTrigger.AddCampoTriggerRow(CampoItem)
                                    End If
                                Next
                            Next
                        Next
                    Else
                        'CAOP CAMPO VALIDACIONES TRIGGER TIPO TEXTO

                        'Activa Control Valor Tipo Texto en Formulario
                        Me.ValorComboBox.Enabled = False
                        Me.ValorComboBox.Visible = False
                        Me.ValorTextBox.Enabled = True
                        Me.ValorTextBox.Visible = True
                        Me.lblMostrar.Text = "Validaciones a Mostrar"
                        Me.lblOcultar.Text = "Validaciones a Ocultar"
                        Me.OperadorComboBox.Enabled = True

                        Dim CampoItemData = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(_campoRow.fk_Entidad, _campoRow.fk_Documento, _campoRow.id_Campo)

                        For Each tblCampoItemRow As DBCore.SchemaConfig.TBL_CampoRow In CampoItemData
                            Dim CampoItem = TriggerDataSet.TBL_Campo.NewTBL_CampoRow()
                            With CampoItem
                                .id_Campo = tblCampoItemRow.id_Campo
                                .Nombre_Campo = tblCampoItemRow.Nombre_Campo
                            End With
                            TriggerDataSet.TBL_Campo.AddTBL_CampoRow(CampoItem)

                            Dim EtapasData = dbmImaging.SchemaConfig.TBL_Etapa_Captura.DBGet(Nothing)

                            For Each tblEtapaCapturaRow As DBImaging.SchemaConfig.TBL_Etapa_CapturaRow In EtapasData
                                Dim EtapaItem = TriggerDataSet.TBL_Etapa.NewTBL_EtapaRow

                                With EtapaItem
                                    .id_Campo = CampoItem.id_Campo
                                    .Id_Etapa = tblEtapaCapturaRow.id_Etapa_Captura
                                    .Nombre_Etapa = tblEtapaCapturaRow.Nombre_Etapa_Captura
                                End With

                                TriggerDataSet.TBL_Etapa.AddTBL_EtapaRow(EtapaItem)

                                Dim camposData = dbmImaging.SchemaConfig.CTA_Campo_Trigger_Validacion.DBFindByfk_Documentofk_Campo_Trigger(_campoRow.fk_Documento, _campoRow.id_Campo)

                                For Each ctaCampoTriggerRow As DBImaging.SchemaConfig.CTA_Campo_Trigger_ValidacionRow In camposData
                                    Dim CampoTriggerItem = TriggerDataSet.TBL_CampoTrigger.NewTBL_CampoTriggerRow

                                    If ctaCampoTriggerRow.fk_Etapa_Captura = EtapaItem.Id_Etapa Then
                                        With CampoTriggerItem
                                            .id_Etapa = EtapaItem.Id_Etapa
                                            .id_Campo = CampoItem.id_Campo
                                            .fk_Campo = ctaCampoTriggerRow.fk_Validacion_Ocultar
                                            .Nombre_Campo = ctaCampoTriggerRow.Pregunta_Validacion
                                            .fk_Validacion = ctaCampoTriggerRow.fk_Validacion_Ocultar
                                            .Valor = ctaCampoTriggerRow.Valor
                                            .Operador_Validacion = ctaCampoTriggerRow.Operador_Validacion
                                            .fk_Documento = ctaCampoTriggerRow.fk_Documento
                                        End With

                                        TriggerDataSet.TBL_CampoTrigger.AddTBL_CampoTriggerRow(CampoTriggerItem)

                                        Me.EtapaComboBox.SelectedValue = CampoTriggerItem.id_Etapa
                                        Id_Etapa = CampoTriggerItem.id_Etapa
                                        Me.ValorTextBox.Text = CampoTriggerItem.Valor
                                        Me.OperadorComboBox.Text = CampoTriggerItem.Operador_Validacion
                                    End If
                                Next
                            Next
                        Next

                        Me.EtapaComboBox.DataSource = TBL_EtapaBindingSource
                        'CAOP CAMPO VALIDACIONES TRIGGER TIPO TEXTO
                    End If

                    AñadirCamposListBox.Refresh()
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            dataCargando = False
        End Sub

        Private Sub GuardarTodo()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                If Me.ValorComboBox.Visible = True Then
                    'CASE CONTROLES LISTAS
                    '***Borra toda la tabla antes de guardar los elementos nuevos
                    dbmImaging.SchemaConfig.TBL_Campo_Trigger.DBDelete(Nothing, _campoRow.fk_Documento, _campoRow.id_Campo, Nothing, Nothing, Nothing)

                    For Each campoTriggerRow As DBImaging.Esquemas.xsdTrigger.CampoTriggerRow In TriggerDataSet.CampoTrigger.Rows
                        Dim newCampoTriggerRow = New DBImaging.SchemaConfig.TBL_Campo_TriggerType

                        With newCampoTriggerRow
                            .fk_Etapa_Captura = campoTriggerRow.id_Etapa
                            .fk_Documento = _campoRow.fk_Documento
                            .fk_Campo_Trigger = _campoRow.id_Campo
                            .Valor = campoTriggerRow.id_Lista
                            .fk_Campo_Ocultar = campoTriggerRow.id_Campo
                            .fk_Campo_Tabla_Ocultar = campoTriggerRow.id_Campo_Tabla
                        End With
                        dbmImaging.SchemaConfig.TBL_Campo_Trigger.DBInsert(newCampoTriggerRow)
                    Next

                ElseIf Me.ValorTextBox.Visible = True Then
                    'CASE CONTROLES TEXTBOX
                    '***Borra toda la tabla antes de guardar los elementos nuevos
                    dbmImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBDelete(Nothing, _campoRow.fk_Documento, _campoRow.id_Campo, Nothing, Nothing)

                    For Each TBL_campoTriggerRow As DBImaging.Esquemas.xsdTrigger.TBL_CampoTriggerRow In TriggerDataSet.TBL_CampoTrigger.Rows
                        Dim newCampoTriggerRow = New DBImaging.SchemaConfig.TBL_Campo_Trigger_ValidacionType

                        With newCampoTriggerRow
                            .fk_Etapa_Captura = TBL_campoTriggerRow.id_Etapa
                            .fk_Documento = _campoRow.fk_Documento
                            .fk_Campo_Trigger = _campoRow.id_Campo
                            .Valor = ValorTextBox.Text
                            .fk_Validacion_Ocultar = TBL_campoTriggerRow.fk_Validacion
                            .Operador_Validacion = OperadorComboBox.SelectedItem.ToString
                            .fk_Usuario_Log = Program.Sesion.Usuario.id
                            .Fecha_Log = Slyg.Tools.SlygNullable.SysDate
                        End With
                        dbmImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBInsert(newCampoTriggerRow)
                    Next

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
            End Try

            DialogResult = Windows.Forms.DialogResult.OK
        End Sub

#End Region

    End Class
End Namespace