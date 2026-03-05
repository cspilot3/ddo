Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopCBarras
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Destape

    Public Class FormLlavesIndexacion
        Inherits FormBase

#Region " Declaraciones "

        Private _TableForm As TableLayoutPanel
        Private _TableLlavesUniversal As New DataTable
        Private _Folder As DesktopConfig.Folder

        Private _fk_Expediente As Long = 0
        Private _Esquema As Short = 0

#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)
        End Sub

        'Proceso interno de creacion y manejo de controles
        Public Sub ControlCrearCampos()
            If Program.RiskGlobal.CargueParcial = True Then
                SpaceFlowLayoutPanel.Controls.Add(ControlesCargue(Program.RiskGlobal.LLavesProyecto))
            Else
                SpaceFlowLayoutPanel.Controls.Add(ControlesSinCargue(Program.RiskGlobal.LLavesProyecto))
            End If
        End Sub

        Public Sub ControlLostFocus(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim TextBoxLostFocus As DesktopTextBoxControl = CType(sender, DesktopTextBoxControl)
            TextBoxLostFocus.PasswordChar = CChar("*")
        End Sub

        Public Sub ControlGotFocus(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim TextBoxLostFocus As DesktopTextBoxControl = CType(sender, DesktopTextBoxControl)
            TextBoxLostFocus.PasswordChar = CChar("")
        End Sub

        Public Sub ControlLimpiarLlaves(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto))
            For Each Llave As DesktopConfig.LlaveProyecto In Llaves
                Dim NombreLlave As String = Llave.Nombre

                CType(Utilities.FindControl(_TableForm, NombreLlave.Replace(" ", "_")), DesktopTextBoxControl).Text = ""

                Try : CType(Utilities.FindControl(_TableForm, NombreLlave.Replace(" ", "_") & "_Confirmar"), DesktopTextBoxControl).Text = ""
                Catch : End Try
            Next
        End Sub

        'Otros
        Public Sub Limpiarformulario()
            ControlLimpiarLlaves(Program.RiskGlobal.LLavesProyecto)
            If cbarrasDesktopCBarrasControl.Text <> "" Then
                cbarrasDesktopCBarrasControl.Text = ""
                cbarrasDesktopCBarrasControl.Focus()
            End If
        End Sub

        Public Sub AbrirFormDocumentos(ByVal nfk_Expediente As Long, ByVal nfk_Folder As Short, ByVal nfk_OT As Integer, ByVal nfk_Esquema As Short, ByVal nCBarras_Folder As String, ByVal Tipo As DesktopConfig.TipoFileCargue, ByVal Registro As DesktopConfig.RegistroTipo)
            Dim formDocumentos As New FormDocumentos(nfk_Expediente, nfk_Folder, nfk_OT, nfk_Esquema, nCBarras_Folder, Tipo, Registro)
            formDocumentos.ShowDialog()
            Limpiarformulario()
        End Sub

        'Proceso
        Public Sub Proceso()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                If Valida() Then

                    If cbarrasDesktopCBarrasControl.Text <> "" Then
                        'Busca el expediente por codigo de barras
                        Dim Folder = dbmArchiving.Schemadbo.CTA_Folder.DBFindByCBarras_Folder(cbarrasDesktopCBarrasControl.Text)
                        If (Folder.Count > 0) Then
                            _fk_Expediente = Folder(0).fk_Expediente
                            _Esquema = Folder(0).fk_Esquema
                        Else
                            Dim File = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(cbarrasDesktopCBarrasControl.Text)
                            If (File.Count > 0) Then
                                _fk_Expediente = File(0).fk_Expediente
                                _Esquema = File(0).fk_Esquema
                            Else
                                _fk_Expediente = 0
                                _Esquema = 0
                            End If
                        End If

                    Else
                        'Busca el expediente por llaves
                        Dim Llaves = ConcatenarLlaves()
                        Program.RiskGlobal.LLaves = Llaves
                        Dim Folder = dbmArchiving.Schemadbo.PA_Get_Expediente_By_Keys_2.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Nothing, Llaves)

                        If (Folder.Rows.Count > 1) Then
                            _Esquema = FormSeleccionarEsquema.GetEsquema()
                            If _Esquema = 0 Then Exit Sub
                            Dim ExpedienteView = Folder.DefaultView
                            ExpedienteView.RowFilter = "fk_Esquema = " & _Esquema.ToString()
                            Folder = ExpedienteView.ToTable()

                            If Folder.Rows.Count > 0 Then
                                _fk_Expediente = CLng(Folder.Rows(0)("fk_Expediente"))
                                _Esquema = CShort(Folder.Rows(0)("fk_Esquema"))
                            End If

                        ElseIf (Folder.Rows.Count = 1) Then
                            _fk_Expediente = CLng(Folder.Rows(0)("fk_Expediente"))
                            _Esquema = CShort(Folder.Rows(0)("fk_Esquema"))
                        Else
                            _fk_Expediente = 0
                            _Esquema = 0
                        End If

                    End If

                    If _fk_Expediente = 0 And _Esquema = 0 Then _Esquema = FormSeleccionarEsquema.GetEsquema()

                    If _Esquema = 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un esquema para continuar.", "Esquema invalido.", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                        Exit Sub
                    End If

                    ProcesarFolder()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Proceso2", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

            cbarrasDesktopCBarrasControl.Focus()
        End Sub

        Public Sub ProcesarFolder()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            'VALIDA QUE LAS LLAVES DIGITADAS ESTEN BIEN
            If Valida() Then

                Try
                    Dim UsaCargue = Program.RiskGlobal.CargueParcial
                    Dim Procesar = False
                    Dim ExisteCargueLog As Boolean = False
                    Dim AceptaSobrantesFolder = CBool(CTA_EsquemaByidEsquema(_Esquema).Rows(0)("Acepta_Sobrantes"))
                    Dim Respuesta = dbmArchiving.Schemadbo.PA_Destape_Validar.DBExecute(Program.RiskGlobal.Precinto, Program.RiskGlobal.OT, _fk_Expediente, 1, UsaCargue, AceptaSobrantesFolder)
                    Dim RespuestatipoValidacion = CShort(Respuesta.Rows(0)("TipoValidacion"))
                    Dim RespuestaMessage = CStr(Respuesta.Rows(0)("Mensaje"))
                    Dim RespuestaEstado = CInt(Respuesta.Rows(0)("Estado_Final"))
                    Dim RespuestaOT = CInt(Respuesta.Rows(0)("fk_OT"))
                    Dim RespuestaCBarras = ""
                    Dim RespuestaRegistroTipo = CInt(Respuesta.Rows(0)("fk_Registro_Tipo"))
                    Dim RespuestaEsSobrante = CBool(Respuesta.Rows(0)("ES_Sobrante"))
                    If Not IsDBNull(Respuesta.Rows(0)("CBarras_Folder")) Then RespuestaCBarras = CStr(Respuesta.Rows(0)("CBarras_Folder"))

                    ' Valida si la carpeta existe en Cargue Log
                    If Not Program.RiskGlobal.Usa_Validacion_Destape Then
                        ExisteCargueLog = dbmArchiving.SchemaProcess.PA_Validacion_Log_Sobrantes.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _Esquema, Program.RiskGlobal.LLaves, 0)
                    End If

                    If (RespuestatipoValidacion = 0) Then
                        'Validacion exitosa
                        Procesar = True

                    ElseIf (RespuestatipoValidacion = 1) Then
                        'Validacion con errores
                        DesktopMessageBoxControl.DesktopMessageShow(RespuestaMessage, "Validacion Destape", DesktopMessageBoxControl.IconEnum.ErrorIcon, False)

                    ElseIf ((RespuestatipoValidacion = 2 And Program.RiskGlobal.Usa_Validacion_Destape) Or (RespuestatipoValidacion = 2 And Not Program.RiskGlobal.Usa_Validacion_Destape And ExisteCargueLog = False)) Then
                        'Validacion con preguntas
                        If DesktopMessageBoxControl.DesktopMessageShow(RespuestaMessage, "Proceso Destape", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK Then
                            Procesar = True
                        End If
                    ElseIf (RespuestatipoValidacion = 2 And Not Program.RiskGlobal.Usa_Validacion_Destape And ExisteCargueLog = True) Then
                        'Validacion con preguntas
                        Procesar = True
                    ElseIf (RespuestatipoValidacion = 3) Then
                        'Validacion de Carpetas en diferentes precintos igual llaves
                        If DesktopMessageBoxControl.DesktopMessageShow(RespuestaMessage, "Proceso Destape", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = DialogResult.OK Then
                            Procesar = True
                        End If
                    End If


                    'Proceso de doble captura cuando el documento es sobrante
                    If RespuestaEsSobrante And Procesar And Not ExisteCargueLog Then
                        Dim LlavesSobrantes = getLlavesSobrantes()
                        SpaceFlowLayoutPanel.Visible = False
                        Dim FormLlaves As New FormLlavesDobleCaptura(LlavesSobrantes)
                        Dim RespuestaLlaves = FormLlaves.ShowDialog()
                        SpaceFlowLayoutPanel.Visible = True

                        If RespuestaLlaves <> DialogResult.OK Then

                            Procesar = False
                            DesktopMessageBoxControl.DesktopMessageShow("La data registrada no es consistente, ingrese nuevamente la información.", "Error al intentar destapar Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
                        End If
                    End If


                    If Procesar Then

                        dbmArchiving.Transaction_Begin()

                        If _fk_Expediente = 0 Then
                            'Crea un nuevo Folder
                            Dim Folder = InsertaFolder(dbmArchiving, _Esquema, CType(RespuestaEstado, DBCore.EstadoEnum), False, RespuestaOT, Program.RiskGlobal.Precinto)
                            _fk_Expediente = Folder.Expediente
                            RespuestaCBarras = Folder.CBarras

                        ElseIf RespuestatipoValidacion = 3 Then
                            'Crea un nuevo Folder
                            Dim Folder = InsertaFolderOT(dbmArchiving, _Esquema, CType(RespuestaEstado, DBCore.EstadoEnum), False, RespuestaOT, Program.RiskGlobal.Precinto)
                            _fk_Expediente = Folder.Expediente
                            RespuestaCBarras = Folder.CBarras
                            RespuestaOT = Folder.OT
                        Else
                            'Actualiza el Folder
                            dbmArchiving.Schemadbo.PA_Destape_Actualiza_Folder.DBExecute(_fk_Expediente, 1, RespuestaOT, Program.Sesion.Usuario.id, RespuestaEstado, Program.RiskGlobal.Precinto, CInt(Program.RiskGlobal.CajaProceso))
                            ActualizaDatosUniversal(dbmArchiving)
                        End If

                        dbmArchiving.Transaction_Commit()

                        If RespuestaOT > 0 And RespuestatipoValidacion = 3 Then

                            DesktopMessageBoxControl.DesktopMessageShow("La carpeta fue creada en la OT " & RespuestaOT & ".", "Destape Carpeta", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        End If

                        Dim nTipoCArgue As DesktopConfig.TipoFileCargue = DesktopConfig.TipoFileCargue.SinCargue
                        If UsaCargue Then
                            If AceptaSobrantesFolder Then
                                nTipoCArgue = DesktopConfig.TipoFileCargue.ConCargueSobrante
                            Else
                                nTipoCArgue = DesktopConfig.TipoFileCargue.ConCargueReproceso
                            End If
                        End If

                        AbrirFormDocumentos(_fk_Expediente, 1, RespuestaOT, _Esquema, RespuestaCBarras, nTipoCArgue, CType(RespuestaRegistroTipo, DesktopConfig.RegistroTipo))

                    End If



                Catch ex As Exception
                    If dbmArchiving IsNot Nothing Then dbmArchiving.Transaction_Rollback()

                    If ex.Message.ToUpper().Contains("UK_TBL_FOLDER") Then
                        dbmArchiving.Schemadbo.PA_Actualiza_Consecutivo_CBarras.DBExecute()
                        DesktopMessageBoxControl.DesktopMessageShow("Han ocurrido problemas al crear la carpeta, por favor intente nuevamente.", "Error procesando carpeta", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("Procesar Folder 2", ex)
                    End If

                Finally
                    If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
                End Try
            End If
        End Sub


        Public Sub ActualizaDatosUniversal(ByVal dbmArchiving As DBArchivingDataBaseManager)
            If Program.RiskGlobal.CargueUniversal = True Then
                Try : _TableLlavesUniversal.Clear()
                Catch : End Try

                For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                    Dim Campo As DesktopTextBoxControl = CType(Utilities.FindControl(SpaceFlowLayoutPanel, Llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl)
                    If Campo.Text = "" Then Exit Sub

                    Dim Valorllave As New Object

                    Select Case Llave.Tipo
                        Case DesktopConfig.CampoTipo.Numerico : Valorllave = CLng(Campo.Text)
                        Case DesktopConfig.CampoTipo.Fecha : Valorllave = CDate(Campo.Text)
                        Case DesktopConfig.CampoTipo.Texto : Valorllave = Campo.Text
                    End Select

                    Dim TableLlaves = dbmArchiving.Schemadbo.CTA_Llaves_Universal_Disponibles.DBFindByfk_entidadfk_proyectovalor_universal_llavefk_proyecto_llave(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Valorllave, Llave.Id)

                    If _TableLlavesUniversal.Columns.Count = 0 Then
                        _TableLlavesUniversal = Utilities.clonarDataTable(TableLlaves)
                        _TableLlavesUniversal.Clear()
                    End If

                    For Each Row As DataRow In TableLlaves.Rows
                        Dim rowLlaves = _TableLlavesUniversal.NewRow

                        For Each columna As DataColumn In TableLlaves.Columns
                            rowLlaves(columna.ColumnName) = Row(columna.ColumnName)
                        Next
                        _TableLlavesUniversal.Rows.Add(rowLlaves)
                    Next
                Next
                _TableLlavesUniversal = _TableLlavesUniversal.DefaultView.ToTable(True)

                Dim TableCount As DataTable = Utilities.CountTable(_TableLlavesUniversal, "id_Universal_Folder", "fk_entidad", "fk_proyecto", "fk_esquema", "fk_Universal_Folder_Estado")

                If TableCount.Rows.Count > 0 Then
                    'Actualiza el estado del folder en el cargue universal
                    For Each row As DataRow In TableCount.Rows
                        If CInt(row("count").ToString) >= Program.RiskGlobal.LLavesProyecto.Count Then
                            If CInt(row("fk_Universal_Folder_Estado").ToString) = DesktopConfig.FolderEstado.Pendiente Then
                                dbmArchiving.SchemaRisk.TBL_Universal_Folder.DBUpdate(Nothing, Nothing, DesktopConfig.FolderEstado.Recibido, Nothing, Nothing, Nothing, CLng(row("id_Universal_Folder")))
                            End If
                        End If
                    Next

                Else
                    'Crea las llaves en el universal como sobrantes
                    Dim RegistroFolderUniversal As New SchemaRisk.TBL_Universal_FolderType
                    RegistroFolderUniversal.fk_Esquema = _Folder.fk_Esquema
                    RegistroFolderUniversal.fk_Universal_Folder_Estado = DesktopConfig.FolderEstado.Sobrante
                    RegistroFolderUniversal.id_Universal_Folder = dbmArchiving.SchemaRisk.TBL_Universal_Folder.DBNextId
                    RegistroFolderUniversal.fk_Cargue = Nothing
                    RegistroFolderUniversal.fk_Entidad = Program.RiskGlobal.Entidad
                    RegistroFolderUniversal.fk_Proyecto = Program.RiskGlobal.Proyecto
                    dbmArchiving.SchemaRisk.TBL_Universal_Folder.DBInsert(RegistroFolderUniversal)

                    For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                        Dim Campo As DesktopTextBoxControl = CType(Utilities.FindControl(SpaceFlowLayoutPanel, Llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl)
                        Dim Valorllave As New Object

                        Select Case Llave.Tipo
                            Case DesktopConfig.CampoTipo.Numerico : Valorllave = CLng(Campo.Text)
                            Case DesktopConfig.CampoTipo.Fecha : Valorllave = CDate(Campo.Text)
                            Case DesktopConfig.CampoTipo.Texto : Valorllave = Campo.Text
                        End Select

                        Dim RegistroFolderUniversalLlaves As New SchemaRisk.TBL_Universal_Folder_LlaveType
                        RegistroFolderUniversalLlaves.fk_Proyecto_Llave = Llave.Id
                        RegistroFolderUniversalLlaves.fk_Tipo_Campo = Llave.Tipo
                        RegistroFolderUniversalLlaves.fk_Universal_Folder = RegistroFolderUniversal.id_Universal_Folder
                        RegistroFolderUniversalLlaves.id_Universal_Llave = Llave.Id
                        RegistroFolderUniversalLlaves.Valor_Universal_Llave = Valorllave
                        dbmArchiving.SchemaRisk.TBL_Universal_Folder_Llave.DBInsert(RegistroFolderUniversalLlaves)
                    Next

                End If
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function ConcatenarLlaves() As String
            Dim Llaves As String = ""
            'Dim TotalLlaves As Integer = Program.RiskGlobal.LLavesProyecto.Count
            Dim i As Integer
            For i = 1 To Program.RiskGlobal.LLavesProyecto.Count
                If Llaves <> "" Then Llaves = Llaves & "|"
                Llaves = Llaves & "[" & i.ToString() & "]"
            Next

            For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                Dim valorllave = CType(Utilities.FindControl(SpaceFlowLayoutPanel, Llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl).Text
                Llaves = Llaves.Replace("[" & Llave.Id & "]", valorllave)
            Next

            Return Llaves
        End Function

        Public Function FolderLlaves(Optional ByVal CBarras As String = "") As DesktopConfig.Folder
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim folderReturn As New DesktopConfig.Folder

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If CBarras = "" Then
                    Dim Llave As DesktopConfig.StrLlaves
                    Dim Llaves As New List(Of DesktopConfig.StrLlaves)
                    For Each VarLlave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                        Dim ControlLlave As DesktopTextBoxControl = CType(Utilities.FindControl(SpaceFlowLayoutPanel, VarLlave.Nombre.Replace(" ", "_")), DesktopTextBoxControl)
                        Llave.id_Llave = VarLlave.Id
                        Llave.Nombre_Llave = VarLlave.Nombre
                        Llave.Valor_Llave = ControlLlave.Text
                        Llaves.Add(Llave)
                    Next

                    folderReturn = Utilities.FindFolderByKeys(dbmArchiving, Llaves, Program.RiskGlobal, Program.DesktopGlobal.ConnectionStrings)
                Else
                    folderReturn = Utilities.FindFolderByCBarras(dbmArchiving, CBarras)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FolderLlaves", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try
            Return folderReturn
        End Function

        Public Function getLlavesSobrantes() As Dictionary(Of String, String)
            Dim ListaLlaves As New Dictionary(Of String, String)

            For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                Dim NombreLlave As String = Llave.Nombre
                Dim Campo1 = CType(Utilities.FindControl(_TableForm, NombreLlave.Replace(" ", "_")), DesktopTextBoxControl)
                ListaLlaves.Add(Campo1.Name, Campo1.Text)
            Next

            Return ListaLlaves
        End Function


        'Proceso interno de creacion y manejo de controles
        Public Function ControlesCargue(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As TableLayoutPanel
            _TableForm = New TableLayoutPanel
            Try
                _TableForm.ColumnCount = 2
                _TableForm.RowCount = Llaves.Count
                _TableForm.Width = 500
            Catch ex As Exception
                Throw New Exception("No existen llaves en el proyecto.")
            End Try


            Dim i As Integer = 0
            For Each Llave As DesktopConfig.LlaveProyecto In Llaves
                Dim NombreLlave As String = Llave.Nombre

                Dim LlaveLabel As New Label
                LlaveLabel.Name = "lbl_" & NombreLlave.Replace(" ", "_")
                LlaveLabel.Text = NombreLlave
                LlaveLabel.Width = 200

                Dim LlaveTextBox As New DesktopTextBoxControl
                LlaveTextBox.Name = NombreLlave.Replace(" ", "_")
                LlaveTextBox.Width = 250
                Select Case Llave.Tipo
                    Case DesktopConfig.CampoTipo.Numerico
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                    Case DesktopConfig.CampoTipo.Fecha
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                    Case DesktopConfig.CampoTipo.Texto
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                End Select


                _TableForm.Controls.Add(LlaveLabel, 0, i)
                _TableForm.Controls.Add(LlaveTextBox, 1, i)

                i += 1
            Next

            _TableForm.Refresh()

            Return _TableForm
        End Function

        Public Function ControlesSinCargue(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As TableLayoutPanel
            _TableForm = New TableLayoutPanel
            Try
                _TableForm.ColumnCount = 3
                _TableForm.RowCount = Llaves.Count
                _TableForm.Width = 600
            Catch ex As Exception
                Throw New Exception("No existen llaves en el proyecto.")
            End Try


            Dim i As Integer = 0
            For Each Llave As DesktopConfig.LlaveProyecto In Llaves
                Dim NombreLlave As String = Llave.Nombre

                Dim LlaveLabel As New Label
                LlaveLabel.Name = "lbl_" & NombreLlave.Replace(" ", "_")
                LlaveLabel.Text = NombreLlave
                LlaveLabel.Width = 140

                Dim LlaveTextBox As New DesktopTextBoxControl
                LlaveTextBox.Name = NombreLlave.Replace(" ", "_")
                AddHandler LlaveTextBox.LostFocus, AddressOf ControlLostFocus
                AddHandler LlaveTextBox.GotFocus, AddressOf ControlGotFocus

                Dim LlaveTextBox2 As New DesktopTextBoxControl
                LlaveTextBox2.Name = NombreLlave.Replace(" ", "_") & "_Confirmar"

                Select Case Llave.Tipo
                    Case DesktopConfig.CampoTipo.Numerico
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                        LlaveTextBox2.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                    Case DesktopConfig.CampoTipo.Fecha
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                        LlaveTextBox2.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                    Case DesktopConfig.CampoTipo.Texto
                        LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                        LlaveTextBox2.Type = DesktopTextBoxControl.TipoTextBox.Normal
                End Select

                _TableForm.Controls.Add(LlaveLabel, 0, i)
                _TableForm.Controls.Add(LlaveTextBox, 1, i)
                _TableForm.Controls.Add(LlaveTextBox2, 2, i)

                i += 1
            Next

            _TableForm.Refresh()

            Return _TableForm
        End Function

        Public Function ControlValidarSimilitudLlaves(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As Boolean
            For Each Llave As DesktopConfig.LlaveProyecto In Llaves
                Dim NombreLlave As String = Llave.Nombre
                Dim Campo1 As DesktopTextBoxControl = CType(Utilities.FindControl(_TableForm, NombreLlave.Replace(" ", "_")), DesktopTextBoxControl)
                Dim Campo2 As DesktopTextBoxControl = CType(Utilities.FindControl(_TableForm, NombreLlave.Replace(" ", "_") & "_Confirmar"), DesktopTextBoxControl)

                If Campo1.Text = "" Then
                    Campo1.Focus()
                    Return False
                End If

                If Campo2.Text = "" Then
                    Campo2.Focus()
                    Return False
                End If

                If Campo1.Text <> Campo2.Text Then
                    Campo1.Focus()
                    Return False
                End If
            Next

            Return True
        End Function


        'Otros
        Public Function CTA_EsquemaByidEsquema(ByVal id_Esquema As Integer) As DataTable
            Dim viewEsquema As New DataView(Program.RiskGlobal.Esquemas)
            viewEsquema.RowFilter = Program.RiskGlobal.Esquemas.fk_esquemaColumn.ColumnName & "=" & id_Esquema
            Return viewEsquema.ToTable()
        End Function

        Public Function InsertaFolder(ByRef dbmArchiving As DBArchivingDataBaseManager, ByVal Esquema As Integer, ByVal nEstado As DBCore.EstadoEnum, ByVal EsDevolucion As Boolean, ByVal OT As Integer, ByVal nPrecinto As String) As DesktopConfig.FolderCORE
            Dim Folder As DesktopConfig.FolderCORE

            If Not EsDevolucion Then
                Dim Llaves As String = ""
                'Dim TotalLlaves As Integer = Program.RiskGlobal.LLavesProyecto.Count
                Dim i As Integer
                For i = 1 To Program.RiskGlobal.LLavesProyecto.Count
                    If Llaves <> "" Then Llaves = Llaves & "|"
                    Llaves = Llaves & "[" & i.ToString() & "]"
                Next

                For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                    Dim valorllave = CType(Utilities.FindControl(SpaceFlowLayoutPanel, Llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl).Text
                    Llaves = Llaves.Replace("[" & Llave.Id & "]", valorllave)
                Next

                Dim Respuesta = dbmArchiving.Schemadbo.PA_Destape_Inserta_Folder_Nuevo.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Esquema, nEstado, Program.Sesion.Usuario.id, OT, nPrecinto, True, Llaves, CInt(Program.RiskGlobal.CajaProceso))

                Folder.Expediente = CLng(Respuesta.Rows(0)("id_Expediente"))
                Folder.Folder = CShort(Respuesta.Rows(0)("id_Folder"))
                Folder.CBarras = CStr(Respuesta.Rows(0)("CBarras_Folder"))
            Else
                Folder.Expediente = _Folder.fk_Expediente
                Folder.Folder = _Folder.id_Folder
                Folder.CBarras = _Folder.CBarras_Folder
            End If

            Return Folder
        End Function

        Public Function InsertaFolderOT(ByRef dbmArchiving As DBArchivingDataBaseManager, ByVal Esquema As Integer, ByVal nEstado As DBCore.EstadoEnum, ByVal EsDevolucion As Boolean, ByVal OT As Integer, ByVal nPrecinto As String) As DesktopConfig.FolderCOREOT
            Dim FolderOT As DesktopConfig.FolderCOREOT

            If Not EsDevolucion Then
                Dim Llaves As String = ""
                'Dim TotalLlaves As Integer = Program.RiskGlobal.LLavesProyecto.Count
                Dim i As Integer
                For i = 1 To Program.RiskGlobal.LLavesProyecto.Count
                    If Llaves <> "" Then Llaves = Llaves & "|"
                    Llaves = Llaves & "[" & i.ToString() & "]"
                Next

                For Each Llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                    Dim valorllave = CType(Utilities.FindControl(SpaceFlowLayoutPanel, Llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl).Text
                    Llaves = Llaves.Replace("[" & Llave.Id & "]", valorllave)
                Next

                Dim Respuesta = dbmArchiving.Schemadbo.PA_Destape_Inserta_Folder_Nuevo_OT.DBExecute(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Esquema, nEstado, Program.Sesion.Usuario.id, OT, nPrecinto, True, Llaves, CInt(Program.RiskGlobal.CajaProceso))

                FolderOT.Expediente = CLng(Respuesta.Rows(0)("id_Expediente"))
                FolderOT.Folder = CShort(Respuesta.Rows(0)("id_Folder"))
                FolderOT.CBarras = CStr(Respuesta.Rows(0)("CBarras_Folder"))
                FolderOT.OT = CInt(Respuesta.Rows(0)("fk_OT"))
            Else
                FolderOT.Expediente = _Folder.fk_Expediente
                FolderOT.Folder = _Folder.id_Folder
                FolderOT.CBarras = _Folder.CBarras_Folder
                FolderOT.OT = _Folder.fk_Ot
            End If

            Return FolderOT
        End Function

        Public Function Valida() As Boolean
            If Program.RiskGlobal.CargueParcial = True Then
                Return True
            Else
                Return ControlValidarSimilitudLlaves(Program.RiskGlobal.LLavesProyecto)
            End If
        End Function

#End Region

#Region " Eventos "

        Private Sub CBarrasDesktopTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
            If e.KeyCode = Keys.Enter Then
                If CType(sender, DesktopCBarrasControl).Text = "" Then
                    CBarrasDesktopTextBox_KeyDown(sender, New KeyEventArgs(Keys.Tab))
                Else
                    AceptarButton.Focus()
                End If
            End If
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub FormLlavesIndexacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                'Dim AA = Program.RiskGlobal.CTA_Esquema_DBFindByfk_entidadfk_proyectofk_esquema(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, 1)
                ControlCrearCampos()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FormLlavesIndexacion_Load", ex)
            End Try
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Proceso()
        End Sub

#End Region

    End Class

End Namespace