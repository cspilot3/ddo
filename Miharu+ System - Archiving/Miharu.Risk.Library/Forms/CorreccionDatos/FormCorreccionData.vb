Imports System.Text
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Controls.DesktopComboBox
Imports Miharu.Desktop.Controls
Imports Miharu.Risk.Library.Forms.CBarras
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.CorreccionDatos

    Public Class FormCorreccionData
        Inherits FormBase

#Region " Declaraciones "

        Dim CamposTable As DBArchiving.Schemadbo.CTA_Correccion_Data_CamposDataTable
        Dim LlavesTable As DBArchiving.Schemadbo.CTA_Correccion_Data_LlavesDataTable
        Dim ValidacionesTable As DBArchiving.Schemadbo.CTA_Correccion_Data_ValidacionesDataTable
        Dim ContenedorCampos As TableLayoutPanel
        Dim ContenedorLLaves As TableLayoutPanel
        Dim ContenedorValidaciones As TableLayoutPanel
        Dim fk_Expediente As Long
        Dim fk_Folder As Short
        Dim fk_File As Short
        Dim CBarras As String

#End Region

#Region " Constructor "

        Sub New(ByVal CBarrasFile As String)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            CBarras = CBarrasFile
        End Sub

#End Region

#Region " Eventos "

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub ReimpresionCBarrasButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReimpresionCBarrasButton.Click
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim CBarrasImpresion As New FormCBarrasFolderFile
            CBarrasImpresion.CBarras = Utilities.FindFileByCBarras(dbmArchiving, CBarras).CBarras_Folder
            dbmArchiving.Connection_Close()

            CBarrasImpresion.ShowDialog()
        End Sub

        Private Sub FormCorreccionData_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim File = dbmArchiving.Schemadbo.CTA_File.DBFindByCBarras_File(CBarras)
                CamposTable = dbmArchiving.Schemadbo.CTA_Correccion_Data_Campos.DBFindByCBarras_File(CBarras)
                ValidacionesTable = dbmArchiving.Schemadbo.CTA_Correccion_Data_Validaciones.DBFindByCBarras_File(CBarras)
                LlavesTable = dbmArchiving.Schemadbo.CTA_Correccion_Data_Llaves.DBFindByCBarras_File(CBarras)

                fk_Expediente = File(0).fk_Expediente
                fk_Folder = File(0).fk_Folder
                fk_File = File(0).id_File

                DataFlowLayoutPanel.Controls.Add(CreaCamposData)
                ValidacionesFlowLayoutPanel.Controls.Add(CreaValidaciones)
                LlavesFlowLayoutPanel.Controls.Add(CreaCamposLlaves)

                CargaMontoFolios()
                dbmArchiving.Connection_Close()

            Catch ex As Exception
                dbmArchiving.Connection_Close()
                DesktopMessageBoxControl.DesktopMessageShow("FormCorreccionData_Load", ex)
            End Try
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Dim Error_ As New StringBuilder
            Dim ValidaData As Boolean
            Dim ValidaValidaciones As Boolean
            Dim ValidaFile As Boolean

            GuardarLLaves(Error_, ValidaData)
            GuardarData(Error_, ValidaData)
            GuardarValidaciones(Error_, ValidaValidaciones)
            GuardarFile(Error_, ValidaFile)

            If (Not ValidaData) Or (Not ValidaValidaciones) Or (Not ValidaFile) Then
                DesktopMessageBoxControl.DesktopMessageShow(Error_.ToString, "Inconsistencias al guardar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Else
                DesktopMessageBoxControl.DesktopMessageShow(Error_.ToString, "Datos almacenados con exito", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
            End If

        End Sub

#End Region

#Region " Funciones "

        Public Function CreaValidaciones() As TableLayoutPanel
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            ContenedorValidaciones = New TableLayoutPanel
            ContenedorValidaciones.AutoScroll = True
            ContenedorValidaciones.Name = "ContenedorValidaciones"
            ContenedorValidaciones.Width = ValidacionesFlowLayoutPanel.Width - 10
            ContenedorValidaciones.Height = ValidacionesFlowLayoutPanel.Height - 10
            ContenedorValidaciones.ColumnCount = 1

            Dim Count As Integer = 0
            For Each Validacion In ValidacionesTable
                Dim Check As New DesktopCheckBox.DesktopCheckBoxControl
                Check.Width = ContenedorValidaciones.Width - 10
                Check.Name = "Validacion" & Validacion.id_Validacion
                Check.Text = Validacion.Pregunta_Validacion
                If Not Validacion.IsRespuestaNull Then
                    Check.Checked = Validacion.Respuesta
                Else
                    Check.Checked = False
                End If

                ContenedorValidaciones.Controls.Add(Check, 0, Count)
                Count += 1
            Next

            dbmArchiving.Connection_Close()
            Return ContenedorValidaciones
        End Function

        Public Function CreaCamposData() As TableLayoutPanel
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            ContenedorCampos = New TableLayoutPanel
            ContenedorCampos.Name = "Contenedor"
            ContenedorCampos.Width = DataFlowLayoutPanel.Width - 10
            ContenedorCampos.Height = DataFlowLayoutPanel.Height - 10
            ContenedorCampos.AutoScroll = True
            ContenedorCampos.ColumnCount = 3
            ContenedorCampos.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial

            Dim Count As Integer = 0
            For Each Campo In CamposTable
                'LABELS
                Dim LabelCampo As New Label
                LabelCampo.Text = Campo.Nombre_Campo
                ContenedorCampos.Controls.Add(LabelCampo, 0, Count)

                'FECHAS
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Fecha Then
                    Dim Control_ As New DesktopTextBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Control_.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                    Control_.MaxLength = Campo.Length_Campo
                    If Not Campo.Isfk_Proyecto_LlaveNull Then
                        Control_.BackColor = Drawing.Color.Bisque
                        Control_.FocusIn = Drawing.Color.Bisque
                        Control_.FocusOut = Drawing.Color.Bisque
                    End If
                    ContenedorCampos.Controls.Add(Control_, 1, Count)
                End If

                'TEXTO
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Texto Then
                    Dim Control_ As New DesktopTextBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Control_.Type = DesktopTextBoxControl.TipoTextBox.Normal
                    Control_.MaxLength = Campo.Length_Campo
                    If Not Campo.IsValor_File_DataNull Then Control_.Text = Campo.Valor_File_Data.ToString
                    If Not Campo.Isfk_Proyecto_LlaveNull Then
                        Control_.BackColor = Drawing.Color.Bisque
                        Control_.FocusIn = Drawing.Color.Bisque
                        Control_.FocusOut = Drawing.Color.Bisque
                    End If
                    ContenedorCampos.Controls.Add(Control_, 1, Count)
                End If

                'NUMERICO
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Numerico Then
                    Dim Control_ As New DesktopTextBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Control_.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                    Control_.MaxLength = Campo.Length_Campo
                    If Not Campo.IsValor_File_DataNull Then Control_.Text = Campo.Valor_File_Data.ToString
                    If Not Campo.Isfk_Proyecto_LlaveNull Then
                        Control_.BackColor = Drawing.Color.Bisque
                        Control_.FocusIn = Drawing.Color.Bisque
                        Control_.FocusOut = Drawing.Color.Bisque
                    End If
                    ContenedorCampos.Controls.Add(Control_, 1, Count)
                End If

                'BOOLEANOS
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.SiNo Then
                    Dim Control_ As New DesktopCheckBox.DesktopCheckBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    If Not Campo.IsValor_File_DataNull Then Control_.Checked = CBool(Campo.Valor_File_Data)
                    ContenedorCampos.Controls.Add(Control_, 1, Count)
                End If

                'LISTAS
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Lista Then
                    Dim Control_ As New DesktopComboBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Dim Lista = dbmArchiving.SchemaCore.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(Nothing, Campo.fk_Campo_Lista)
                    Utilities.LlenarCombo(Control_, Lista, Lista.Valor_Campo_Lista_ItemColumn.ColumnName, Lista.Etiqueta_Campo_Lista_ItemColumn.ColumnName)
                    If Not Campo.IsValor_File_DataNull Then Control_.SelectedValue = Campo.Valor_File_Data
                    ContenedorCampos.Controls.Add(Control_, 1, Count)
                End If

                'LABELS VALORES
                Dim LabelValores As New Label
                LabelValores.ForeColor = Drawing.Color.SeaGreen
                If Not Campo.IsValor_File_DataNull Then LabelValores.Text = Campo.Valor_File_Data.ToString()
                ContenedorCampos.Controls.Add(LabelValores, 2, Count)

                Count += 1
            Next

            ContenedorCampos.Refresh()

            dbmArchiving.Connection_Close()
            Return ContenedorCampos
        End Function

        Public Function CreaCamposLlaves() As TableLayoutPanel
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            ContenedorLLaves = New TableLayoutPanel
            ContenedorLLaves.Name = "Contenedor"
            ContenedorLLaves.Width = LlavesFlowLayoutPanel.Width - 10
            ContenedorLLaves.Height = LlavesFlowLayoutPanel.Height - 10
            ContenedorLLaves.AutoScroll = True
            ContenedorLLaves.ColumnCount = 3
            ContenedorLLaves.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial

            Dim Count As Integer = 0
            For Each Campo In LlavesTable
                'LABELS
                Dim LabelCampo As New Label
                LabelCampo.Text = Campo.Nombre_Campo
                ContenedorLLaves.Controls.Add(LabelCampo, 0, Count)

                'FECHAS
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Fecha Then
                    Dim Control_ As New DesktopTextBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Control_.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                    Control_.MaxLength = Campo.Length_Campo
                    If Not Campo.IsValor_LlaveNull Then Control_.Text = Campo.IsValor_LlaveNull.ToString
                    ContenedorLLaves.Controls.Add(Control_, 1, Count)
                End If

                'TEXTO
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Texto Then
                    Dim Control_ As New DesktopTextBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Control_.Type = DesktopTextBoxControl.TipoTextBox.Normal
                    Control_.MaxLength = Campo.Length_Campo
                    If Not Campo.IsValor_LlaveNull Then Control_.Text = Campo.Valor_Llave.ToString
                    ContenedorLLaves.Controls.Add(Control_, 1, Count)
                End If

                'NUMERICO
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Numerico Then
                    Dim Control_ As New DesktopTextBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Control_.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                    Control_.MaxLength = Campo.Length_Campo
                    If Not Campo.IsValor_LlaveNull Then Control_.Text = Campo.Valor_Llave.ToString
                    ContenedorLLaves.Controls.Add(Control_, 1, Count)
                End If

                'BOOLEANOS
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.SiNo Then
                    Dim Control_ As New DesktopCheckBox.DesktopCheckBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    If Not Campo.IsValor_LlaveNull Then Control_.Checked = CBool(Campo.Valor_Llave)
                    ContenedorLLaves.Controls.Add(Control_, 1, Count)
                End If

                'LISTAS
                If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Lista Then
                    Dim Control_ As New DesktopComboBoxControl
                    Control_.Name = "Campo" & Campo.id_Campo
                    Dim Lista = dbmArchiving.SchemaCore.CTA_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(Nothing, Campo.fk_Campo_Lista)
                    Utilities.LlenarCombo(Control_, Lista, Lista.Valor_Campo_Lista_ItemColumn.ColumnName, Lista.Etiqueta_Campo_Lista_ItemColumn.ColumnName)
                    If Not Campo.IsValor_LlaveNull Then Control_.SelectedValue = Campo.Valor_Llave
                    ContenedorLLaves.Controls.Add(Control_, 1, Count)
                End If

                'LABELS VALORES
                Dim LabelValores As New Label
                LabelValores.ForeColor = Drawing.Color.SeaGreen
                If Not Campo.IsValor_LlaveNull Then LabelValores.Text = Campo.Valor_Llave.ToString
                ContenedorLLaves.Controls.Add(LabelValores, 2, Count)

                Count += 1
            Next

            ContenedorLLaves.Refresh()

            dbmArchiving.Connection_Close()
            Return ContenedorLLaves
        End Function

        Public Sub CargaMontoFolios()
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            Dim File = dbmCore.SchemaProcess.TBL_File.DBGet(fk_Expediente, fk_Folder, fk_File)
            FoliosDesktopTextBox.Text = CStr(File(0).Folios_File)
            MontoDesktopTextBox.Text = CStr(File(0).Monto_File)
            dbmCore.Connection_Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub GuardarData(ByRef Error_ As StringBuilder, ByRef Validacion As Boolean)
            Validacion = False
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            ContenedorCampos = CType(Utilities.FindControl(DataFlowLayoutPanel, "Contenedor"), TableLayoutPanel)

            Try
                dbmCore.Transaction_Begin()
                Dim Valida As Boolean = True
                Dim CampoLlaveModificado As Boolean = False

                For Each Campo In CamposTable
                    Dim Nulo As Boolean = False
                    Dim Data As New DBCore.SchemaProcess.TBL_File_DataType
                    Data.fk_Campo = Campo.id_Campo
                    Data.fk_Documento = Campo.id_Documento
                    Data.fk_Expediente = fk_Expediente
                    Data.fk_File = fk_File
                    Data.fk_Folder = fk_Folder

                    'FECHAS
                    If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Fecha Then
                        Dim Control_ As DesktopTextBoxControl = CType(Utilities.FindControl(ContenedorCampos, "Campo" & CStr(Campo.id_Campo)), DesktopTextBoxControl)
                        Data.Valor_File_Data = Utilities.DDate(Control_.Text)
                        If Control_.Text = "" Then Nulo = True
                    End If

                    'TEXTO
                    If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Texto Then
                        Dim Control_ As DesktopTextBoxControl = CType(Utilities.FindControl(ContenedorCampos, "Campo" & CStr(Campo.id_Campo)), DesktopTextBoxControl)
                        Data.Valor_File_Data = Utilities.DStr(Control_.Text)
                        If Control_.Text = "" Then Nulo = True
                    End If

                    'NUMERICO
                    If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Numerico Then
                        Dim Control_ As DesktopTextBoxControl = CType(Utilities.FindControl(ContenedorCampos, "Campo" & CStr(Campo.id_Campo)), DesktopTextBoxControl)
                        Data.Valor_File_Data = Utilities.Dlng(Control_.Text)
                        If Control_.Text = "" Then Nulo = True
                    End If

                    'BOOLEANOS
                    If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.SiNo Then
                        Dim Control_ As DesktopCheckBox.DesktopCheckBoxControl = CType(Utilities.FindControl(ContenedorCampos, "Campo" & CStr(Campo.id_Campo)), DesktopCheckBox.DesktopCheckBoxControl)
                        Data.Valor_File_Data = Control_.Checked
                    End If

                    'LISTAS
                    If Campo.fk_Campo_Tipo = DesktopConfig.CampoTipo.Lista Then
                        Dim Control_ As DesktopComboBoxControl = CType(Utilities.FindControl(ContenedorCampos, "Campo" & CStr(Campo.id_Campo)), DesktopComboBoxControl)
                        Data.Valor_File_Data = Utilities.DStr(Control_.SelectedValue)
                        If IsNothing(Utilities.DStr(Control_.SelectedValue).Value) Or IsDBNull(Utilities.DStr(Control_.SelectedValue).Value) Then Nulo = True
                    End If

                    'GUARDA LA DATA

                    'Valida que el campo no sea nulo si es obligatorio
                    If Campo.Es_Obligatorio_Campo And Nulo Then
                        Error_.AppendLine("El Campo " & Campo.Nombre_Campo & " es obligatorio.")
                        Valida = False
                        Exit For
                    Else
                        'Valida que el campo no sea nulo si es campo llave
                        If Nulo And Not Campo.Isfk_Proyecto_LlaveNull Then
                            Error_.AppendLine("El Campo " & Campo.Nombre_Campo & " es Llave y no puede estar vacio.")
                            Valida = False
                            Exit For
                        Else
                            'Elimina la Data que esta vacia
                            If Nulo Then
                                dbmCore.SchemaProcess.TBL_File_Data.DBDelete(Data.fk_Expediente, Data.fk_Folder, Data.fk_File, Data.fk_Documento, Data.fk_Campo)
                            Else
                                'Inserta o actualiza data
                                Dim Existe = dbmCore.SchemaProcess.TBL_File_Data.DBGet(Data.fk_Expediente, Data.fk_Folder, Data.fk_File, Data.fk_Documento, Data.fk_Campo)
                                If Existe.Count = 0 Then
                                    Data.Conteo_File_Data = 0
                                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(Data)
                                Else
                                    If Data.Valor_File_Data.ToString() <> Campo.Valor_File_Data.ToString() Then
                                        dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(Data, Data.fk_Expediente, Data.fk_Folder, Data.fk_File, Data.fk_Documento, Data.fk_Campo)

                                        'actualiza los datos que son cambiados cuando son llaves del proyecto
                                        If Not Campo.Isfk_Proyecto_LlaveNull Then
                                            Dim Llave As New DBCore.SchemaProcess.TBL_Expediente_LlaveType
                                            Llave.Valor_Llave = Data.Valor_File_Data
                                            dbmCore.SchemaProcess.TBL_Expediente_Llave.DBUpdate(Llave, fk_Expediente, Campo.fk_Proyecto_Llave)
                                            CampoLlaveModificado = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

                If Valida = True Then
                    dbmCore.Transaction_Commit()
                    Error_.AppendLine("Data almacenada con exito")
                    Validacion = True

                    'Si se ha modificado el valor de alguna llave se pide la reimpresion del codigo de barras
                    If CampoLlaveModificado Then
                        Error_.AppendLine("Alguna llave ha sido modificada, por favor reimprima el codigo de barras de la carpeta")
                    End If
                Else
                    dbmCore.Transaction_Rollback()
                End If

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                Error_.AppendLine(ex.Message)
            End Try

            dbmCore.Connection_Close()
        End Sub

        Public Sub GuardarLLaves(ByRef Error_ As StringBuilder, ByRef Validacion As Boolean)
            Validacion = False
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            ContenedorLLaves = CType(Utilities.FindControl(LlavesFlowLayoutPanel, "Contenedor"), TableLayoutPanel)

            Try
                dbmCore.Transaction_Begin()
                Dim Valida As Boolean = True
                Dim CampoLlaveModificado As Boolean = False

                For Each Llaves In LlavesTable
                    Dim Nulo As Boolean = False
                    Dim LlaveType As New DBCore.SchemaProcess.TBL_Expediente_LlaveType
                    LlaveType.fk_campo_tipo = Llaves.fk_Campo_Tipo
                    LlaveType.fk_Expediente = fk_Expediente
                    LlaveType.fk_proyecto_Llave = Llaves.fk_proyecto_Llave

                    'FECHAS
                    If Llaves.fk_Campo_Tipo = DesktopConfig.CampoTipo.Fecha Then
                        Dim Control_ As DesktopTextBoxControl = CType(Utilities.FindControl(ContenedorLLaves, "Campo" & CStr(Llaves.id_Campo)), DesktopTextBoxControl)
                        LlaveType.Valor_Llave = Utilities.DDate(Control_.Text)
                        If Control_.Text = "" Then Nulo = True
                    End If

                    'TEXTO
                    If Llaves.fk_Campo_Tipo = DesktopConfig.CampoTipo.Texto Then
                        Dim Control_ As DesktopTextBoxControl = CType(Utilities.FindControl(ContenedorLLaves, "Campo" & CStr(Llaves.id_Campo)), DesktopTextBoxControl)
                        LlaveType.Valor_Llave = Utilities.DStr(Control_.Text)
                        If Control_.Text = "" Then Nulo = True
                    End If

                    'NUMERICO
                    If Llaves.fk_Campo_Tipo = DesktopConfig.CampoTipo.Numerico Then
                        Dim Control_ As DesktopTextBoxControl = CType(Utilities.FindControl(ContenedorLLaves, "Campo" & CStr(Llaves.id_Campo)), DesktopTextBoxControl)
                        LlaveType.Valor_Llave = Utilities.Dlng(Control_.Text)
                        If Control_.Text = "" Then Nulo = True
                    End If

                    'BOOLEANOS
                    If Llaves.fk_Campo_Tipo = DesktopConfig.CampoTipo.SiNo Then
                        Dim Control_ As DesktopCheckBox.DesktopCheckBoxControl = CType(Utilities.FindControl(ContenedorLLaves, "Campo" & CStr(Llaves.id_Campo)), DesktopCheckBox.DesktopCheckBoxControl)
                        LlaveType.Valor_Llave = Control_.Checked
                    End If

                    'LISTAS
                    If Llaves.fk_Campo_Tipo = DesktopConfig.CampoTipo.Lista Then
                        Dim Control_ As DesktopComboBoxControl = CType(Utilities.FindControl(ContenedorLLaves, "Campo" & CStr(Llaves.id_Campo)), DesktopComboBoxControl)
                        LlaveType.Valor_Llave = Utilities.DStr(Control_.SelectedValue)
                        If IsNothing(Utilities.DStr(Control_.SelectedValue).Value) Or IsDBNull(Utilities.DStr(Control_.SelectedValue).Value) Then Nulo = True
                    End If

                    'GUARDA LA DATA

                    'Valida que el campo no sea nulo si es obligatorio
                    If Llaves.Es_Obligatorio_Campo And Nulo Then
                        Error_.AppendLine("El Campo [" & Llaves.Nombre_Campo & "] es llave y es obligatorio.")
                        Valida = False
                        Exit For
                    Else
                        If Llaves.Valor_Llave.ToString() <> LlaveType.Valor_Llave.ToString() Then
                            Dim localLlavesTable = dbmCore.SchemaProcess.TBL_Expediente_Llave.DBFindByfk_Expedientefk_proyecto_Llavefk_campo_tipoValor_Llave(Nothing, Llaves.fk_proyecto_Llave, Nothing, LlaveType.Valor_Llave)

                            If localLlavesTable.Rows.Count > 0 Then
                                Error_.AppendLine("El Campo [" & Llaves.Nombre_Campo & "] ya se encuentra en otro documento [exp:" & localLlavesTable(0).fk_Expediente & "], por favor pruebe con otro valor de llave.")
                                Valida = False
                                Exit For
                            Else
                                dbmCore.SchemaProcess.TBL_Expediente_Llave.DBUpdate(LlaveType, fk_Expediente, Llaves.fk_proyecto_Llave)
                                CampoLlaveModificado = True

                                Dim FileType As New DBCore.SchemaProcess.TBL_File_DataType
                                FileType.Valor_File_Data = LlaveType.Valor_Llave
                                dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(FileType, Llaves.id_Expediente, Llaves.fk_Folder, Llaves.id_File, Llaves.fk_Documento, Llaves.id_Campo)
                            End If
                        End If
                    End If

                Next

                If Valida = True Then
                    dbmCore.Transaction_Commit()



                    Error_.AppendLine("Llaves almacenada con exito")
                    Validacion = True

                    'Si se ha modificado el valor de alguna llave se pide la reimpresion del codigo de barras
                    If CampoLlaveModificado Then
                        Error_.AppendLine("Alguna llave ha sido modificada, por favor reimprima el codigo de barras de la carpeta")
                    End If
                Else
                    dbmCore.Transaction_Rollback()
                End If

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                Error_.AppendLine(ex.Message)
            End Try

            dbmCore.Connection_Close()
        End Sub

        Public Sub GuardarValidaciones(ByRef Error_ As StringBuilder, ByRef Validacion_ As Boolean)
            Validacion_ = False
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            ContenedorValidaciones = CType(Utilities.FindControl(ValidacionesFlowLayoutPanel, "ContenedorValidaciones"), TableLayoutPanel)

            Try
                dbmCore.Transaction_Begin()

                For Each Validacion In ValidacionesTable
                    Dim Valida As New DBCore.SchemaProcess.TBL_File_ValidacionType
                    Dim Control_ As DesktopCheckBox.DesktopCheckBoxControl = CType(Utilities.FindControl(ContenedorValidaciones, "Validacion" & CStr(Validacion.id_Validacion)), DesktopCheckBox.DesktopCheckBoxControl)
                    Valida.Respuesta = Control_.Checked
                    Valida.fk_Expediente = fk_Expediente
                    Valida.fk_File = fk_File
                    Valida.fk_Folder = fk_Folder
                    Valida.fk_Validacion = Validacion.id_Validacion

                    Dim Existe = dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(fk_Expediente, fk_Folder, fk_File, Validacion.id_Validacion, Nothing)
                    If Existe.Count = 0 Then
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(Valida)
                    Else
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Valida, fk_Expediente, fk_Folder, fk_File, Validacion.id_Validacion, Nothing)
                    End If
                Next

                dbmCore.Transaction_Commit()
                Error_.AppendLine("Validaciones almacenadas con exito")
                Validacion_ = True

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                Error_.AppendLine(ex.Message)
            End Try

            dbmCore.Connection_Close()
        End Sub

        Public Sub GuardarFile(ByRef Error_ As StringBuilder, ByRef Validacion_ As Boolean)
            Validacion_ = False
            Dim Valida As Boolean = True

            If FoliosDesktopTextBox.Text = "" Then
                Error_.AppendLine("Debe digitar folios para el documento")
                Valida = False
            End If

            If MontoDesktopTextBox.Text = "" Then
                Error_.AppendLine("Debe digitar Monto para el documento")
                Valida = False
            End If

            If Valida Then
                Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmCore.Transaction_Begin()
                    Dim File As New DBCore.SchemaProcess.TBL_FileType
                    File.Folios_File = CShort(FoliosDesktopTextBox.Text)
                    File.Monto_File = CDec(MontoDesktopTextBox.Text)
                    dbmCore.SchemaProcess.TBL_File.DBUpdate(File, fk_Expediente, fk_Folder, fk_File)
                    dbmCore.Transaction_Commit()
                    Error_.AppendLine("Folios y monto almacenados con exito")
                    Validacion_ = True
                Catch ex As Exception
                    dbmCore.Transaction_Rollback()
                    Error_.AppendLine(ex.Message)
                Finally
                    dbmCore.Connection_Close()
                End Try
            End If

        End Sub

#End Region

    End Class

End Namespace