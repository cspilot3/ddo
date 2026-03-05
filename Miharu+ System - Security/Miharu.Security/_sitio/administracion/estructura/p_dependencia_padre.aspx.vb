'MUESTRA UN  ARBOL DE DEPENDENCIAS FILTRADO POR LA ENTIDAD
'RETORNA EL ID Y EL NOMBRE DE LA DEPENDENCIA SELECCIONADA POR EL USUARIO 
Imports Miharu.Security._clases

Namespace _sitio.administracion.estructura

    Partial Public Class p_dependencia_padre
        Inherits PaginaBasePopUp

#Region " Declaraciones "
        Private tblDependenciaPadre As DBSecurity.SchemaConfig.TBL_DependenciaDataTable

#End Region

#Region " Eventos "
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not Me.IsPostBack Then
                Config_Page()
            Else
                Load_Data()
            End If
        End Sub
        Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Cargar()
        End Sub
        Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Master.Cerrar(False)
        End Sub
#End Region

#Region " Metodos "
        Private Sub Config_Page()
            Dim idEntidad As Short = CShort(Me.MySesion.Pagina.Parameter("EntidadId"))

            tblDependenciaPadre = New DBSecurity.SchemaConfig.TBL_DependenciaDataTable
            Me.MySesion.Pagina.Parameter("tblDependenciaPadre") = tblDependenciaPadre

            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)
                tblDependenciaPadre = dbmSecurity.SchemaConfig.TBL_Dependencia.DBFindByfk_EntidadNombre_Dependencia(idEntidad, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterPopUp.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try
            ShowArbol()
        End Sub
        Private Sub Load_Data()
            tblDependenciaPadre = CType(Me.MySesion.Pagina.Parameter("tblDependenciaPadre"), DBSecurity.SchemaConfig.TBL_DependenciaDataTable)
        End Sub
        Private Sub ShowArbol()
            Dim idEntidad As Short = CShort(Me.MySesion.Pagina.Parameter("EntidadId"))
            tvDependencia.Nodes.Clear()
            'Gerencia-Nodo Padre (Cabeza)
            Dim NewNodo As New TreeNode(tblDependenciaPadre.Rows(0).Item("Nombre_Dependencia").ToString, _
                                        tblDependenciaPadre.Rows(0).Item("id_Dependencia").ToString & "-" & tblDependenciaPadre.Rows(0).Item("fk_Nivel").ToString, _
                                        "~/_images/menu/Dependencia.png")
            NewNodo.SelectAction = TreeNodeSelectAction.Expand
            NewNodo.ShowCheckBox = True

            tvDependencia.Nodes.Add(NewNodo)
            InsertarNodos(NewNodo, idEntidad, 1)


            tvDependencia.CollapseAll()

        End Sub
        Private Sub InsertarNodos(ByRef nNodo As TreeNode, ByVal nEntidad As Short, ByVal nNodoPadre As Integer)
            Dim Nodos() As DBSecurity.SchemaConfig.TBL_DependenciaRow
            Nodos = CType(tblDependenciaPadre.Select("fk_Entidad = " & nEntidad & " AND fk_Padre " & CStr(IIf(nNodoPadre = 0, " IS NULL", "= " & CStr(nNodoPadre)))), DBSecurity.SchemaConfig.TBL_DependenciaRow())
            Try
                For Each RowNodo As DBSecurity.SchemaConfig.TBL_DependenciaRow In Nodos
                    Dim NewNodo As New TreeNode(RowNodo.Nombre_Dependencia, CStr(RowNodo.id_Dependencia) & "-" & RowNodo.fk_Nivel, "~/_images/menu/Dependencia.png")

                    NewNodo.SelectAction = TreeNodeSelectAction.Expand
                    NewNodo.ShowCheckBox = True

                    nNodo.ChildNodes.Add(NewNodo)
                    InsertarNodos(NewNodo, nEntidad, RowNodo.id_Dependencia)
                Next
            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterPopUp.MsgBoxIcon.IconError)
            End Try
        End Sub

        Private Sub Cargar()
            'Valida que se seleccione solo una dependencia            
            Select Case tvDependencia.CheckedNodes.Count
                Case 0
                    Master.ShowAlert("Debe seleccionar una dependencia padre.", MiharuMasterPopUp.MsgBoxIcon.IconWarning)
                Case 1
                    Dim Nodo As TreeNode
                    Nodo = tvDependencia.CheckedNodes(0)
                    Dim separator() As String = Nodo.Value.Split("-"c)
                    If CInt(separator(1)) = 2 Then                                             'fk_Padre
                        Master.ShowAlert("No se puede agregar dependencias a una dependencia asistencial.", MiharuMasterPopUp.MsgBoxIcon.IconWarning)
                    ElseIf CInt(separator(1)) = 1 Then
                        MySesion.Pagina.Parameter("DependenciaPadreId") = CInt(separator(0))  'id_dependencia
                        MySesion.Pagina.Parameter("DependenciaPadreNombre") = Nodo.Text
                        Master.Cerrar(True)
                    End If
                Case Is > 1
                    Master.ShowAlert("Debe seleccionar sólo una dependencia padre.", MiharuMasterPopUp.MsgBoxIcon.IconWarning)
            End Select
        End Sub
#End Region

    End Class
End Namespace