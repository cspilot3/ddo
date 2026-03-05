Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Slyg.Tools
Imports Miharu.Imaging.Library
Imports Miharu.Imaging.Library.Procesos.Configuracion.Imaging

Namespace Forms.Parametrizacion

    Public Class FormNuevoCampoCarpeta


#Region " Declaraciones "

        Private _FileName As String = ""

#End Region

#Region " Propiedades "

        Public Property Entidad() As Short

        Public Property Proyecto() As Short

        Public Property Esquema() As Short

        Public Property Documento() As Integer

        Public Property Id_Campo() As Short

        Public Property fk_Campo_Tipo() As Byte

        Public Property Nombre_Campo() As String

        Public Property Length_Campo() As Short

        Public Property Length_Min_Campo() As String

        Public Property Precaptura() As Boolean

        Public Property PrimeraCaptura() As Boolean

        Public Property UsaDobleCaptura() As Boolean

        Public Property Eliminado_Campo() As Boolean

        Public Property Usa_Marca() As Boolean

        Public Property Marca_X_Campo() As Byte

        Public Property Marca_Y_Campo() As Byte

        Public Property Marca_Width_Campo() As Byte

        Public Property Marca_Height_Campo() As Byte

        Public Property fk_Tipo_Llave() As Byte
#End Region

#Region "Eventos"

        Private Sub FormNuevoCampo_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarListas()

            If (Id_Campo.ToString() > "0") Then
                Nombre_Campo_TextBox.Text = Nombre_Campo

                Select Case fk_Campo_Tipo
                    Case 1
                        Campo_Tipo_ComboBox.SelectedItem = "Texto"
                    Case 2
                        Campo_Tipo_ComboBox.SelectedItem = "Numérico"
                    Case 3
                        Campo_Tipo_ComboBox.SelectedItem = "Fecha"
                End Select

                Longitud_Campo_TextBox.Text = CStr(Length_Campo)
                Longitud_Minima_TextBox.Text = Length_Min_Campo
                Precaptura_CheckBox.Checked = Precaptura
                PrimeraCaptura_CheckBox.Checked = PrimeraCaptura
                DobleCaptura_CheckBox.Checked = UsaDobleCaptura
                Eliminado_CheckBox.Checked = Eliminado_Campo
                UsaMarca_CheckBox.Checked = Usa_Marca
                Marca_X_Campo_TextBox.Text = Marca_X_Campo.ToString
                Marca_Y_Campo_TextBox.Text = Marca_Y_Campo.ToString
                Marca_Width_Campo_TextBox.Text = Marca_Width_Campo.ToString
                Marca_Height_Campo_TextBox.Text = Marca_Height_Campo.ToString
                Tipo_Llave_ComboBox.SelectedValue = fk_Tipo_Llave
            End If
        End Sub

        Private Sub CargarListas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                'Dim Campo_TipoData = dbmCore.SchemaConfig.TBL_Campo_Tipo.DBGet(Nothing)
                'Campo_Tipo_ComboBox.Fill(Campo_TipoData, Campo_TipoData.id_Campo_TipoColumn, Campo_TipoData.Nombre_Campo_TipoColumn, True)

                Dim listCampoTipo As List(Of String) = New List(Of String)
                listCampoTipo.Add("")
                listCampoTipo.Add("Texto")
                listCampoTipo.Add("Numérico")
                listCampoTipo.Add("Fecha")
                Campo_Tipo_ComboBox.DataSource = listCampoTipo

                Dim Tipo_LlavesData = dbmCore.SchemaConfig.TBL_Tipo_Llave.DBGet(Nothing)
                Tipo_Llave_ComboBox.Fill(Tipo_LlavesData, Tipo_LlavesData.Id_Tipo_LlaveColumn, Tipo_LlavesData.Nombre_Tipo_LlaveColumn, True)

            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub GuardarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            GuardarCambios()
        End Sub

#End Region

#Region " Metodos "

        Private Sub GuardarCambios()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Transaction_Begin()

                Dim TipoCampo As Integer

                If Campo_Tipo_ComboBox.SelectedValue.ToString() <> "" Then
                    Try
                        Select Case Campo_Tipo_ComboBox.SelectedValue.ToString()
                            Case "-1"
                                Throw New Exception("Debe seleccionar un Tipo de Campo")
                            Case "Texto"
                                TipoCampo = 1
                            Case "Numérico"
                                TipoCampo = 2
                            Case "Fecha"
                                TipoCampo = 3
                        End Select
                    Catch ex As Exception
                        Throw New Exception("Debe seleccionar un Tipo de Campo")
                    End Try
                Else
                    Throw New Exception("Debe seleccionar un Tipo de Campo")
                End If

                Try
                    Select Case Convert.ToInt16(Tipo_Llave_ComboBox.SelectedValue)
                        Case -1
                            Throw New Exception("Debe seleccionar un Tipo de Llave")
                    End Select
                Catch ex As Exception
                    Throw New Exception("Debe seleccionar un Tipo de Llave")
                End Try

                Try
                    If Precaptura_CheckBox.Checked = False And PrimeraCaptura_CheckBox.Checked = False And DobleCaptura_CheckBox.Checked = False Then
                        Throw New Exception("Debe seleccionar una etapa de captura")
                    End If
                Catch ex As Exception
                    Throw New Exception("Debe seleccionar una etapa de captura")
                End Try

                If (Longitud_Campo_TextBox.Text = "") Then Longitud_Campo_TextBox.Text = "0"
                If (Longitud_Minima_TextBox.Text = "") Then Longitud_Minima_TextBox.Text = "0"

                Dim CampoCarpeta As New DBImaging.SchemaConfig.CTA_Campo_LlaveType

                Dim Marca_Height_Campo As Byte
                Dim Marca_Width_Campo As Byte
                Dim Marca_X_Campo As Byte
                Dim Marca_Y_Campo As Byte


                If (UsaMarca_CheckBox.Checked = True) Then
                    Try
                        If Marca_Height_Campo_TextBox.Text = "" Or Marca_Width_Campo_TextBox.Text = "" Or Marca_X_Campo_TextBox.Text = "" Or Marca_Y_Campo_TextBox.Text = "" Then
                            Throw New Exception("Debe asignar una marca valida")
                        End If
                    Catch ex As Exception
                        Throw New Exception("Debe asignar una marca valida")
                    End Try

                    Marca_Height_Campo = CByte(Marca_Height_Campo_TextBox.Text)
                    Marca_Width_Campo = CByte(Marca_Width_Campo_TextBox.Text)
                    Marca_X_Campo = CByte(Marca_X_Campo_TextBox.Text)
                    Marca_Y_Campo = CByte(Marca_Y_Campo_TextBox.Text)
                Else
                    Marca_Height_Campo = CByte(0)
                    Marca_Width_Campo = CByte(0)
                    Marca_X_Campo = CByte(0)
                    Marca_Y_Campo = CByte(0)
                End If

                If CByte(Tipo_Llave_ComboBox.SelectedValue) = 2 Then
                    Try
                        Dim CampoCarpetaEmpaque = dbmCore.SchemaConfig.TBL_Campo_Llave.DBFindByfk_Entidadfk_Proyectofk_Esquemafk_Tipo_LlaveEliminado_Campo(Entidad, Esquema, Proyecto, 2, False)

                        If CampoCarpetaEmpaque.Count > 0 And Id_Campo < 0 Then
                            Throw New Exception("Ya existe un campo carpeta tipo empaque, por favor verifique la información ingresada!!!")
                        End If
                    Catch ex As Exception
                        Throw New Exception("Ya existe un campo carpeta tipo empaque, por favor verifique la información ingresada!!!")
                    End Try
                End If

                dbmCore.SchemaConfig.PA_Crea_Campos_Llave.DBExecute(Entidad,
                                                                     Esquema,
                                                                     Proyecto,
                                                                     Documento,
                                                                     Id_Campo,
                                                                     TipoCampo,
                                                                     Nombre_Campo_TextBox.Text,
                                                                     CShort(Longitud_Campo_TextBox.Text),
                                                                     CShort(Longitud_Minima_TextBox.Text),
                                                                     Eliminado_CheckBox.Checked,
                                                                     Precaptura_CheckBox.Checked,
                                                                     PrimeraCaptura_CheckBox.Checked,
                                                                     DobleCaptura_CheckBox.Checked,
                                                                     UsaMarca_CheckBox.Checked,
                                                                     Marca_X_Campo,
                                                                     Marca_Y_Campo,
                                                                     Marca_Width_Campo,
                                                                     Marca_Height_Campo,
                                                                     Program.Sesion.Usuario.id,
                                                                     CByte(Tipo_Llave_ComboBox.SelectedValue))



                dbmCore.Transaction_Commit()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Se han guardado los datos con éxito.", "Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Me.Close()

            Catch ex As Exception
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow(vbCrLf & vbCrLf & ex.Message, "Problemas en Campo Core", Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

        Private Sub Campo_Tipo_ComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles Campo_Tipo_ComboBox.SelectedIndexChanged
            'Fase_Captura_ComboBox.Enabled = (CInt(Campo_Tipo_ComboBox.SelectedValue) = 5)
        End Sub

        Private Sub Label10_Click(sender As System.Object, e As System.EventArgs) Handles Label10.Click

        End Sub

        Private Sub ConfigurarMarca()
            Dim f = New FormConfigCampo()
            Dim Cargado As Boolean

            Try
                f.X = Marca_X_Campo
                f.Y = Marca_Y_Campo
                f.W = Marca_Width_Campo
                f.H = Marca_Height_Campo

                If (_FileName <> "") Then
                    Cargado = f.Cargar(_FileName)
                Else
                    Cargado = f.Cargar()
                End If

                If (Cargado) Then
                    If (f.ShowDialog() = DialogResult.OK) Then
                        _FileName = f.FileName

                        Marca_X_Campo = CByte(f.X)
                        Marca_Y_Campo = CByte(f.Y)
                        Marca_Width_Campo = CByte(f.W)
                        Marca_Height_Campo = CByte(f.H)
                        Usa_Marca = True

                        UsaMarca_CheckBox.Checked = True
                        Marca_X_Campo_TextBox.Text = Marca_X_Campo.ToString
                        Marca_Y_Campo_TextBox.Text = Marca_Y_Campo.ToString
                        Marca_Width_Campo_TextBox.Text = Marca_Width_Campo.ToString
                        Marca_Height_Campo_TextBox.Text = Marca_Height_Campo.ToString
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub Btn_AsignarMarca_Click(sender As System.Object, e As System.EventArgs) Handles Btn_AsignarMarca.Click
            ConfigurarMarca()
        End Sub
    End Class

End Namespace