Imports DBCore

Namespace Sitio.Boveda

    Public Class p_Secciones
        Inherits PopupBase

#Region "DECLARACIONES"
        Private fk_Entidad_ As Short
        Private fk_Sede_ As Integer
        Private fk_Boveda_ As Integer
        Private id_Seccion_ As Integer
#End Region

#Region "Eventos"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            fk_Entidad_ = CShort(GlobalParameterCollection("fk_Entidad").DefaultValue)
            fk_Sede_ = CInt(GlobalParameterCollection("fk_Sede").DefaultValue)
            fk_Boveda_ = CInt(GlobalParameterCollection("id_Boveda").DefaultValue)
            id_Seccion_ = CInt(GlobalParameterCollection("id_Seccion").DefaultValue)

            If Not IsPostBack Then
                Config_Page()
            Else

            End If
        End Sub

        Protected Sub imgGuardarSeccion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGuardarSeccion.Click
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                If id_Seccion_ = -1 Then    'Nuevo
                    dbmCore.Transaction_Begin()
                    Dim idSeccion As Integer = dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBNextId(CShort(fk_Entidad_), CShort(fk_Sede_), CShort(fk_Boveda_))
                    dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBInsert(fk_Entidad_, CShort(fk_Sede_), CShort(fk_Boveda_), CShort(idSeccion), Nombre_Boveda_Seccion.Text, CByte(Ambiente_Boveda_Seccion.SelectedValue), CByte(Seguridad_Boveda_Seccion.SelectedValue))
                    dbmCore.Transaction_Commit()
                    MySesion.Pagina.Parameter("RESPUESTA") = "SI"
                    CloseWindow(True)
                Else    'Editar
                    dbmCore.Transaction_Begin()
                    dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBUpdate(fk_Entidad_, CShort(fk_Sede_), CShort(fk_Boveda_), CShort(id_Seccion_), Nombre_Boveda_Seccion.Text, CByte(Ambiente_Boveda_Seccion.SelectedValue), CByte(Seguridad_Boveda_Seccion.SelectedValue), fk_Entidad_, CShort(fk_Sede_), CShort(fk_Boveda_), CShort(id_Seccion_))
                    dbmCore.Transaction_Commit()

                    Carga_Estantes()
                End If
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub imgAgregarEstante_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAgregarEstante.Click
            If ddlPlantillaEstante.SelectedValue <> "-1" Then
                CrearEstante(CInt(ddlPlantillaEstante.SelectedValue))
            Else
                AlertJS.Show("Debe seleccionar una plantilla para generar el estante.")
            End If
        End Sub
#End Region

#Region "Metodos"
        Private Sub Config_Page()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                'Nuevo Registro
                If id_Seccion_ = -1 Then

                Else    'Edición de sección
                    Dim dtSeccion As DataTable = dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBFindByfk_Entidadfk_Sedefk_Bovedaid_Boveda_Seccion(fk_Entidad_, CShort(fk_Sede_), CShort(fk_Boveda_), CShort(id_Seccion_))
                    If dtSeccion.Rows.Count > 0 Then
                        Id_Seccion.Text = CStr(id_Seccion_)
                        Nombre_Boveda_Seccion.Text = CStr(dtSeccion.Rows(0).Item("Nombre_Boveda_Seccion"))
                        Ambiente_Boveda_Seccion.SelectedValue = CStr(dtSeccion.Rows(0).Item("Ambiente_Boveda_Seccion"))
                        Seguridad_Boveda_Seccion.SelectedValue = CStr(dtSeccion.Rows(0).Item("Seguridad_Boveda_Seccion"))

                        'Carga el combo de plantillas estante
                        Dim dtPlantillaEstante As DataTable = dbmCore.SchemaCustody.TBL_Plantilla_Estante.DBGet(Nothing)
                        Fill_ListControl(CType(ddlPlantillaEstante, DropDownList), dtPlantillaEstante, "Nombre_Plantilla_Estante", "Id_Plantilla_Estante", True, "Seleccione ...", "-1", "ddlPlantillaEstante")

                        pnlAddEstante.Visible = True
                        Carga_Estantes()
                    Else
                        CloseWindow(True)
                    End If
                End If
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub Carga_Estantes()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                Dim dtEstantes As DataTable = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccion(fk_Entidad_, CShort(fk_Sede_), CShort(fk_Boveda_), CShort(id_Seccion_))
                If dtEstantes.Rows.Count > 0 Then
                    grdEstante.DataSource = dtEstantes
                    grdEstante.DataBind()
                End If
            Catch ex As Exception
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub CrearEstante(ByVal idPlantillaEstante As Integer)
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(MyBase.ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)

                'dbmCore.Connection_Open(MySesion.Usuario.id)
                dbmCore.Transaction_Begin()
                'Inserta en la tabla TBL_Boveda_Estante
                Dim newIdEstante As Integer = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBNextId(fk_Entidad_, CShort(fk_Sede_), CShort(fk_Boveda_), CShort(id_Seccion_))

                Dim typeEstante As New SchemaCustody.TBL_Boveda_EstanteType
                typeEstante.fk_Entidad = fk_Entidad_
                typeEstante.fk_Sede = CShort(fk_Sede_)
                typeEstante.fk_Boveda = CShort(fk_Boveda_)
                typeEstante.fk_Boveda_Seccion = CShort(id_Seccion_)
                typeEstante.id_Boveda_Estante = CShort(newIdEstante)

                'Obtiene los valores de la plantilla estante
                Dim dtPlantillaEstante As DataTable = dbmCore.SchemaCustody.TBL_Plantilla_Estante.DBFindByid_Plantilla_Estante(CShort(idPlantillaEstante))

                If dtPlantillaEstante.Rows.Count > 0 Then
                    typeEstante.Codigo_Boveda_Estante = Replicate(newIdEstante.ToString(), 5, "0")
                    typeEstante.Filas_Boveda_Estante = CShort(dtPlantillaEstante.Rows(0).Item("Filas_Plantilla_Estante"))
                    typeEstante.Columnas_Boveda_Estante = CShort(dtPlantillaEstante.Rows(0).Item("Columnas_Plantilla_Estante"))
                    typeEstante.Profundidades_Boveda_Estante = CShort(dtPlantillaEstante.Rows(0).Item("Profundidades_Plantilla_Estante"))
                    typeEstante.Largo_Boveda_Estante = CShort(dtPlantillaEstante.Rows(0).Item("Largo_Plantilla_Estante"))
                    typeEstante.Ancho_Boveda_Estante = CShort(dtPlantillaEstante.Rows(0).Item("Ancho_Plantilla_Estante"))
                    typeEstante.Alto_Boveda_Estante = CShort(dtPlantillaEstante.Rows(0).Item("Alto_Plantilla_Estante"))
                End If
                dbmCore.SchemaCustody.TBL_Boveda_Estante.DBInsert(typeEstante)


                'Se realiza la inserción  en boveda posición
                Dim dtFila, dtColumna, dtProfundidad As DataTable
                Dim typeBovedaPosicion As New SchemaCustody.TBL_Boveda_PosicionType
                Dim nId As Integer = 0
                Dim xFila, yColumna, zProfundidad As Integer
                xFila = 1
                yColumna = 1
                zProfundidad = 1


                dtFila = dbmCore.SchemaCustody.TBL_Plantilla_Estante_Fila.DBFindByfk_Plantilla_Estante(CShort(idPlantillaEstante))
                dtColumna = dbmCore.SchemaCustody.TBL_Plantilla_Estante_Columna.DBFindByfk_Plantilla_Estante(CShort(idPlantillaEstante))
                dtProfundidad = dbmCore.SchemaCustody.TBL_Plantilla_Estante_Profundidad.DBFindByfk_Plantilla_Estante(CShort(idPlantillaEstante))

                For Each fila As DataRow In dtFila.Rows
                    yColumna = 1
                    For Each columna As DataRow In dtColumna.Rows
                        zProfundidad = 1
                        For Each profundidad As DataRow In dtProfundidad.Rows
                            nId = dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBNextId()
                            typeBovedaPosicion.fk_Entidad = fk_Entidad_
                            typeBovedaPosicion.fk_Sede = CShort(fk_Sede_)
                            typeBovedaPosicion.fk_Boveda = CShort(fk_Boveda_)
                            typeBovedaPosicion.fk_Boveda_Seccion = CShort(id_Seccion_)
                            typeBovedaPosicion.fk_Boveda_Estante = CShort(newIdEstante)
                            typeBovedaPosicion.id_Boveda_Posicion = nId
                            'typeBovedaPosicion.Codigo_Boveda_Posicion = Replicate(id.ToString(), 5, "0")
                            'Se guarda el código como Sección-Estante-Fila-Columna-Profundidad
                            typeBovedaPosicion.Codigo_Boveda_Posicion = Replicate(id_Seccion_.ToString(), 2, "0") & "-" & Replicate(newIdEstante.ToString(), 3, "0") & "-" & Replicate(xFila.ToString(), 2, "0") & "-" & Replicate(yColumna.ToString(), 2, "0") & "-" & Replicate(zProfundidad.ToString(), 1, "0")
                            typeBovedaPosicion.Largo_Boveda_Posicion = CShort(columna.Item("Longitud_Plantilla_Estante_Columna"))
                            typeBovedaPosicion.Ancho_Boveda_Posicion = CShort(profundidad.Item("Longitud_Plantilla_Estante_Profundidad"))
                            typeBovedaPosicion.Alto_Boveda_Posicion = CShort(fila.Item("Longitud_Plantilla_Estante_Fila"))
                            typeBovedaPosicion.Fila_Boveda_Posicion = CShort(xFila)
                            typeBovedaPosicion.Columna_Boveda_Posicion = CShort(yColumna)
                            typeBovedaPosicion.Profundidad_Boveda_Posicion = CShort(zProfundidad)
                            typeBovedaPosicion.En_Arqueo_Posicion = False
                            typeBovedaPosicion.Es_Flotante = CBool(fila.Item("EsFlotante"))
                            typeBovedaPosicion.fk_Caja = DBNull.Value

                            dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBInsert(typeBovedaPosicion)
                            zProfundidad += 1
                        Next
                        yColumna += 1
                    Next
                    xFila += 1
                Next

                dbmCore.Transaction_Commit()
                Carga_Estantes()
            Catch ex As Exception
                If dbmCore IsNot Nothing Then dbmCore.Transaction_Rollback()
                Throw
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try


        End Sub
#End Region

    End Class
End Namespace