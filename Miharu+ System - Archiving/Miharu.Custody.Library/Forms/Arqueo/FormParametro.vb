Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBCore
Imports Miharu.Desktop.Library.Config

Namespace Forms.Arqueo

    Public Class FormParametro

#Region " Declaraciones "

        Public rowParametro As SchemaCustody.TBL_Arqueo_ParametroType
        Public rowArqueoParametroESQ As SchemaCustody.CTA_Arqueo_Parametro_ESQType
        Public idArqueoParametro As Integer

        Private detenerEntidad As Boolean
        Private detenerSede As Boolean
        Private detenerBoveda As Boolean
        Private detenerSeccion As Boolean
        Private detenerEstante As Boolean

#End Region

#Region " Eventos "

        Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OKButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK

            rowParametro = New SchemaCustody.TBL_Arqueo_ParametroType
            rowArqueoParametroESQ = New SchemaCustody.CTA_Arqueo_Parametro_ESQType

            rowArqueoParametroESQ.fk_Entidad = Program.Sesion.Entidad.id
            rowArqueoParametroESQ.fk_Arqueo = CShort(-1)
            rowArqueoParametroESQ.id_Arqueo_Parametro = idArqueoParametro
            rowArqueoParametroESQ.fk_Sede = CShort(SedeComboBox.SelectedValue)
            rowArqueoParametroESQ.fk_Boveda = CShort(IIf(BovedaComboBox.SelectedValue Is "0", -1, BovedaComboBox.SelectedValue))
            rowArqueoParametroESQ.Nombre_Boveda = BovedaComboBox.Text
            rowArqueoParametroESQ.fk_Seccion = CShort(IIf(SeccionComboBox.SelectedValue Is "0", -1, SeccionComboBox.SelectedValue))
            rowArqueoParametroESQ.Nombre_Boveda_Seccion = SeccionComboBox.Text
            rowArqueoParametroESQ.fk_Estante = CShort(IIf(EstanteComboBox.SelectedValue Is "0", -1, EstanteComboBox.SelectedValue))
            rowArqueoParametroESQ.Codigo_Boveda_Estante = EstanteComboBox.Text
            rowArqueoParametroESQ.Fila = CShort(IIf(FilaComboBox.SelectedItem Is "Todas", "-1", FilaComboBox.SelectedItem))
            rowArqueoParametroESQ.Columna = CShort(IIf(ColumnaComboBox.SelectedItem Is "Todas", "-1", ColumnaComboBox.SelectedItem))
            rowArqueoParametroESQ.Profundidad = CShort(IIf(ProfundidadComboBox.SelectedItem Is "Todas", "-1", ProfundidadComboBox.SelectedItem))
            rowArqueoParametroESQ.fk_Entidad_Cliente = CShort(EntidadComboBox.SelectedValue)
            rowArqueoParametroESQ.Nombre_Entidad = EntidadComboBox.Text
            rowArqueoParametroESQ.fk_Proyecto = CShort(IIf(ProyectoComboBox.SelectedValue Is "0", -1, ProyectoComboBox.SelectedValue))
            rowArqueoParametroESQ.Nombre_Proyecto = ProyectoComboBox.Text

            Me.Close()
        End Sub

        Private Sub FormParametro_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            detenerEntidad = True
            detenerBoveda = True
            detenerSede = True
            Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
            Try
                DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = Program.DesktopGlobal.IdentifierDateFormat
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)
                'Entidades
                Dim Entidad = Program.Sesion.Entidad.id
                Dim tEntidades = dbmCore.SchemaCustody.CTA_Entidad_Cliente_Boveda.DBFindByfk_Entidad_Custodia(Program.Sesion.Entidad.id).DefaultView.ToTable(True)
                Dim tBoveda = dbmCore.SchemaCustody.TBL_Boveda.DBFindByfk_Entidadfk_Sede(Nothing, Nothing)
                Dim tSede = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(Entidad, Nothing)

                If tEntidades.Rows.Count > 0 Then
                    Utilities.LlenarCombo(EntidadComboBox, tEntidades, "id_entidad", "Nombre_Entidad", True, "-1", "Todas")
                End If

                If tBoveda.Rows.Count > 0 Then
                    Utilities.LlenarCombo(BovedaComboBox, tBoveda, "id_Boveda", "Nombre_Boveda", True, "-1", "Todas")
                End If

                If tSede.Rows.Count > 0 Then
                    Utilities.LlenarCombo(SedeComboBox, tSede, "id_Sede", "Nombre_Sede", True, "-1", "Todas")
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar Entidades", ex)
            Finally
                dbmCore.Connection_Close()
                dbmSecurity.Connection_Close()
            End Try
            detenerEntidad = False
            detenerBoveda = False
            detenerSede = False
        End Sub
        Private Sub EntidadComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EntidadComboBox.SelectedIndexChanged
            If Not detenerEntidad Then
                Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Try

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    'Entidades

                    Dim Entidad = CShort(EntidadComboBox.SelectedValue)
                    Dim tProyectos = dbmCore.Schemadbo.CTA_Proyectos_Vigentes.DBFindByfk_Entidad(Entidad).DefaultView.ToTable(True)

                    If tProyectos.Rows.Count > 0 Then
                        Utilities.LlenarCombo(ProyectoComboBox, tProyectos, "id_Proyecto", "Nombre_Proyecto", True, "-1", "Todos")
                    Else
                        ProyectoComboBox.DataSource = Nothing
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Cargar Proyectos", ex)
                Finally
                    dbmCore.Connection_Close()
                End Try
            End If
        End Sub
        Private Sub BovedaComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles BovedaComboBox.SelectedIndexChanged
            If Not detenerBoveda Then
                detenerSeccion = True
                Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Try

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    'Entidades
                    Dim Sede = CShort(SedeComboBox.SelectedValue)
                    Dim Boveda = CShort(BovedaComboBox.SelectedValue)
                    Dim Entidad = Program.Sesion.Entidad.id
                    Dim tSeccion = dbmCore.SchemaCustody.TBL_Boveda_Seccion.DBFindByfk_Entidadfk_Sedefk_Boveda(Entidad, Sede, Boveda).DefaultView.ToTable(True)

                    If tSeccion.Rows.Count > 0 Then
                        Utilities.LlenarCombo(SeccionComboBox, tSeccion, "id_Boveda_Seccion", "Nombre_Boveda_Seccion", True, "-1", "Todos")
                    Else
                        SeccionComboBox.DataSource = Nothing
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Cargar Proyectos", ex)
                Finally
                    dbmCore.Connection_Close()
                End Try
                detenerSeccion = False
            End If
        End Sub
        Private Sub SeccionComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SeccionComboBox.SelectedIndexChanged
            If Not detenerBoveda Then
                If Not detenerSeccion Then
                    detenerEstante = True
                    Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    Try

                        dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                        'Entidades
                        Dim Sede = CShort(SedeComboBox.SelectedValue)
                        Dim Boveda = CShort(BovedaComboBox.SelectedValue)
                        Dim Seccion = CShort(SeccionComboBox.SelectedValue)
                        Dim Entidad = Program.Sesion.Entidad.id
                        Dim tEstante = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccion(Entidad, Sede, Boveda, Seccion).DefaultView.ToTable(True)

                        If tEstante.Rows.Count > 0 Then
                            Utilities.LlenarCombo(EstanteComboBox, tEstante, "id_Boveda_Estante", "Codigo_Boveda_Estante", True, "-1", "Todos")
                        Else
                            EstanteComboBox.DataSource = Nothing
                        End If

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Cargar Seccion", ex)
                    Finally
                        dbmCore.Connection_Close()
                    End Try
                    detenerEstante = False
                End If
            End If
        End Sub
        Private Sub SedeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SedeComboBox.SelectedIndexChanged
            If Not detenerSede Then
                detenerBoveda = True
                Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                Try

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    'Entidades
                    Dim Entidad = Program.Sesion.Entidad.id
                    Dim Sede = CShort(SedeComboBox.SelectedValue)
                    Dim tBoveda = dbmCore.SchemaCustody.TBL_Boveda.DBFindByfk_Entidadfk_Sede(Entidad, Sede).DefaultView.ToTable(True)

                    If tBoveda.Rows.Count > 0 Then
                        Utilities.LlenarCombo(BovedaComboBox, tBoveda, "id_Boveda", "Nombre_Boveda", True, "-1", "Todos")
                    Else
                        BovedaComboBox.DataSource = Nothing
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Cargar Boveda", ex)
                Finally
                    dbmCore.Connection_Close()
                End Try
                detenerBoveda = False
            End If
        End Sub
        Private Sub EstanteComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles EstanteComboBox.SelectedIndexChanged
            If Not detenerSeccion Then
                If Not detenerEstante Then
                    Dim dbmCore As New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    Try

                        dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                        'Entidades
                        Dim Sede = CShort(SedeComboBox.SelectedValue)
                        Dim Boveda = CShort(BovedaComboBox.SelectedValue)
                        Dim Seccion = CShort(SeccionComboBox.SelectedValue)
                        Dim Estante = CByte(EstanteComboBox.SelectedValue)
                        Dim Entidad = Program.Sesion.Entidad.id
                        Dim tEstante = dbmCore.SchemaCustody.TBL_Boveda_Estante.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccionid_Boveda_Estante(Entidad, Sede, Boveda, Seccion, Estante).DefaultView.ToTable(True)

                        FilaComboBox.Items.Clear()
                        FilaComboBox.Items.Add("Todas")
                        FilaComboBox.SelectedItem = "Todas"
                        ColumnaComboBox.Items.Clear()
                        ColumnaComboBox.Items.Add("Todas")
                        ColumnaComboBox.SelectedItem = "Todas"
                        ProfundidadComboBox.Items.Clear()
                        ProfundidadComboBox.Items.Add("Todas")
                        ProfundidadComboBox.SelectedItem = "Todas"

                        If tEstante.Rows.Count > 0 Then
                            FilaComboBox.Items.AddRange(RangoNumeros(CInt(tEstante.Rows(0)("Filas_Boveda_Estante"))))
                            ColumnaComboBox.Items.AddRange(RangoNumeros(CInt(tEstante.Rows(0)("Columnas_Boveda_Estante"))))
                            ProfundidadComboBox.Items.AddRange(RangoNumeros(CInt(tEstante.Rows(0)("Profundidades_Boveda_Estante"))))
                        Else
                            EstanteComboBox.DataSource = Nothing
                        End If

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Cargar Estante", ex)
                    Finally
                        dbmCore.Connection_Close()
                    End Try
                End If
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function RangoNumeros(ByVal NumeroEntrada As Integer) As Object()
            Dim Salida As New List(Of Object)
            For i As Integer = 1 To NumeroEntrada
                Salida.Add(i)
            Next
            Return Salida.ToArray()
        End Function

#End Region

    End Class

End Namespace