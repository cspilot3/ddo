Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class organigrama
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.1.3"

        Private tblBase As DBSecurity.SchemaConfig.TBL_EntidadDataTable
        Private tblDependencia As DBSecurity.SchemaConfig.TBL_DependenciaDataTable

        Private MyDiagrama As Web.Controls.Diagrama

        Private MaxId As Short
        Private BloqueId As Integer

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
                DrawDiagrama()
                ActivarOpciones(False)
            Else
                Load_Data()
            End If
        End Sub

        Private Sub Page_HijaClose() Handles Me.HijaClose
            DrawDiagrama()
        End Sub

        Private Sub gvBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvBase.SelectedIndexChanged
            If gvBase.SelectedIndex >= 0 And gvBase.Rows.Count > 0 Then
                EditarRegistro()
            End If
        End Sub
        Private Sub ibSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibSave.Click
            GuardarCambios()
        End Sub

        Private Sub ucFiltro_Click(ByVal nParametro As String) Handles ucFiltro.Click
            Buscar(nParametro)
        End Sub

        Private Sub icbEdit_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbEdit.Click
            EditarNodo(CShort(SelectedBloque.Value))
        End Sub
        Private Sub icbDelete_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbDelete.Click
            EliminarNodo(CShort(SelectedBloque.Value))
        End Sub
        Private Sub icbAddAsistencial_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbAddAsistencial.Click
            AgregarNodo(CInt(SelectedBloque.Value), Web.Controls.Diagrama.EnumRelacion.ASISTENCIAL)
        End Sub
        Private Sub icbAddSubordinado_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles icbAddSubordinado.Click
            AgregarNodo(CInt(SelectedBloque.Value), Web.Controls.Diagrama.EnumRelacion.SUBORDINACION)
        End Sub

        Private Sub btnDatosAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDatosAceptar.Click
            ActualizarNodo(CShort(lblCodNodo.Text))
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaConfig.TBL_EntidadDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            tblDependencia = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblDependencia") = tblDependencia

            MyDiagrama = New Web.Controls.Diagrama
            Me.MySesion.Pagina.Parameter("Diagrama") = MyDiagrama


            MaxId = 0
            Me.MySesion.Pagina.Parameter("MaxId") = MaxId

            BloqueId = 0
            Me.MySesion.Pagina.Parameter("BloqueId") = BloqueId

        End Sub
        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_EntidadDataTable)
            tblDependencia = CType(Me.MySesion.Pagina.Parameter("tblDependencia"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)

            MyDiagrama = CType(Me.MySesion.Pagina.Parameter("Diagrama"), Web.Controls.Diagrama)

            MaxId = CShort(Me.MySesion.Pagina.Parameter("MaxId"))
            BloqueId = CInt(Me.MySesion.Pagina.Parameter("BloqueId"))
        End Sub

        Private Sub Buscar(ByVal nParametro As String)
            gvBase.SelectedIndex = -1

            If nParametro <> "" Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.SchemaConfig.TBL_Entidad.DBFillByNombre_Entidad(tblBase, nParametro)
                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
                Finally
                    dbmSecurity.Connection_Close()
                End Try
            Else
                tblBase.Rows.Clear()
            End If

            ActivarOpciones(False)
            Visualizar_Resultados()
            pnlDetalle.Visible = False
        End Sub
        Private Sub Visualizar_Resultados()
            gvBase.DataSource = tblBase
            gvBase.DataBind()
        End Sub

        Private Sub EditarRegistro()
            Dim RowBase As DBSecurity.SchemaConfig.TBL_EntidadRow

            tcBase.ActiveTabIndex = 1

            pnlDetalle.Visible = True

            Me.ActivarOpciones(True)

            RowBase = CType(tblBase.Rows(gvBase.SelectedRow.DataItemIndex), DBSecurity.SchemaConfig.TBL_EntidadRow)

            lblCodEntidad.Text = CStr(RowBase.id_Entidad)
            lblNombreEntidad.Text = RowBase.Nombre_Entidad

            ' Cargar estructura
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblDependencia.Clear()
                dbmSecurity.SchemaConfig.TBL_Dependencia.DBFillByfk_EntidadNombre_Dependencia(tblDependencia, RowBase.id_Entidad, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            If tblDependencia.Rows.Count = 0 Then
                Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow

                RowDependencia = tblDependencia.NewTBL_DependenciaRow

                RowDependencia.fk_Entidad = RowBase.id_Entidad
                RowDependencia.id_Dependencia = 1
                RowDependencia.fk_Padre = 0
                RowDependencia.fk_Nivel = 1
                RowDependencia.Nombre_Dependencia = "Gerencia"
                RowDependencia.Codigo_Dependencia = "001"
                'RowDependencia.fk_Entidad_Log = MySesion.Entidad.id
                RowDependencia.fk_Usuario_Log = MySesion.Usuario.id

                tblDependencia.Rows.Add(RowDependencia)

            End If

            InsertItems()
            DrawDiagrama()
        End Sub
        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

                Try
                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.SchemaConfig.TBL_Dependencia.DBSaveTable(tblDependencia)

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                    ActivarOpciones(True)
                    pnlDetalle.Visible = True
                    tcBase.ActiveTabIndex = 1

                Catch ex As Exception
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If

            DrawDiagrama()
        End Sub
        Private Sub ActivarOpciones(ByVal nActivo As Boolean)
            ibSave.Visible = nActivo
            divSave.Style("display") = CStr(IIf(nActivo, "inline", "none"))
        End Sub

        Private Sub DrawDiagrama()
            Dim Ancho As Integer
            Dim Bloques As List(Of Web.Controls.Bloque)
            Dim Lineas As List(Of Web.Controls.Linea)

            Me.pnlBase.Controls.Clear()

            Ancho = MyDiagrama.getAncho

            If Ancho < pnlBase.Width.Value Then
                MyDiagrama.ConfigPosiciones(CInt(pnlBase.Width.Value / 2), 0)
            Else
                MyDiagrama.ConfigPosiciones(CInt(Ancho / 2), 0)
            End If

            Bloques = MyDiagrama.getBloques()

            For Each B As Web.Controls.Bloque In Bloques
                B.EnableViewState = False
                Me.pnlBase.Controls.Add(B)
            Next

            Lineas = MyDiagrama.getLineas()

            For Each L As Web.Controls.Linea In Lineas
                L.EnableViewState = False
                Me.pnlBase.Controls.Add(L)
            Next
        End Sub
        Private Sub InsertItems()
            Dim Nodo As Web.Controls.DiagramaNodo

            MyDiagrama.Clear()

            For Each RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow In tblDependencia.Rows
                If MaxId < RowDependencia.id_Dependencia Then MaxId = RowDependencia.id_Dependencia

                If RowDependencia.RowState <> DataRowState.Deleted Then
                    Nodo = MyDiagrama.NewNodo
                    Nodo.Etiqueta = RowDependencia.Nombre_Dependencia
                    Nodo.Codigo = RowDependencia.Codigo_Dependencia

                    If RowDependencia.fk_Padre = 0 Then
                        MyDiagrama.Add(RowDependencia.id_Dependencia, Nodo)
                    Else
                        MyDiagrama.Add(RowDependencia.id_Dependencia, Nodo, RowDependencia.fk_Padre, CType(RowDependencia.fk_Nivel, Web.Controls.Diagrama.EnumRelacion))
                    End If
                End If
            Next

            Me.MySesion.Pagina.Parameter("MaxId") = MaxId
        End Sub
        Private Sub AgregarNodo(ByVal nPadre As Integer, ByVal nTipo As Web.Controls.Diagrama.EnumRelacion)
            If MyDiagrama.getNodo(nPadre).Relacion = Web.Controls.Diagrama.EnumRelacion.ASISTENCIAL Then
                Master.ShowAlert("No se puede agregar nodos a una nodo asistencial", MiharuMasterForm.MsgBoxIcon.IconWarning)
            Else
                Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow

                MaxId += CShort(1)
                Me.MySesion.Pagina.Parameter("MaxId") = MaxId

                RowDependencia = tblDependencia.NewTBL_DependenciaRow

                RowDependencia.fk_Entidad = CShort(lblCodEntidad.Text)
                RowDependencia.id_Dependencia = MaxId
                RowDependencia.fk_Padre = CShort(nPadre)
                RowDependencia.Nombre_Dependencia = ""
                RowDependencia.Codigo_Dependencia = ""
                RowDependencia.fk_Nivel = CByte(nTipo)
                'RowDependencia.fk_Entidad_Log = MySesion.Entidad.id
                RowDependencia.fk_Usuario_Log = MySesion.Usuario.id

                tblDependencia.Rows.Add(RowDependencia)

                Dim Nodo As Web.Controls.DiagramaNodo

                Nodo = MyDiagrama.NewNodo
                Nodo.Etiqueta = RowDependencia.Nombre_Dependencia
                Nodo.Codigo = RowDependencia.Codigo_Dependencia

                MyDiagrama.Add(RowDependencia.id_Dependencia, Nodo, RowDependencia.fk_Padre, nTipo)
            End If

            DrawDiagrama()
        End Sub
        Private Sub EditarNodo(ByVal nId As Short)
            Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(lblCodEntidad.Text), nId)

            lblCodNodo.Text = CStr(RowDependencia.id_Dependencia)
            txtNombre.Text = RowDependencia.Nombre_Dependencia
            txtCodigo.Text = RowDependencia.Codigo_Dependencia

            txtNombre.Focus()
            ModalPopupDatos.Show()

            DrawDiagrama()
        End Sub
        Private Sub EliminarNodo(ByVal nId As Short)
            If tblDependencia.Rows.Count > 1 Then
                Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow

                RowDependencia = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(lblCodEntidad.Text), nId)

                RowDependencia.Delete()

                MyDiagrama.Remove(nId)
            Else
                Master.ShowAlert("No se puede eliminar el nodo raíz", MiharuMasterForm.MsgBoxIcon.IconWarning)
            End If

            DrawDiagrama()
        End Sub
        Private Sub ActualizarNodo(ByVal nId As Short)
            Dim RowDependencia As DBSecurity.SchemaConfig.TBL_DependenciaRow = tblDependencia.FindByfk_Entidadid_Dependencia(CShort(lblCodEntidad.Text), nId)
            Dim Nodo As Web.Controls.DiagramaNodo = MyDiagrama.getNodo(nId)

            Nodo.Codigo = txtCodigo.Text
            Nodo.Etiqueta = txtNombre.Text

            RowDependencia.id_Dependencia = CShort(lblCodNodo.Text)
            RowDependencia.Nombre_Dependencia = txtNombre.Text
            RowDependencia.Codigo_Dependencia = txtCodigo.Text

            ModalPopupDatos.Hide()

            DrawDiagrama()
        End Sub


#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace