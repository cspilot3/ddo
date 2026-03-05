Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Inserciones

    Public Class FormLlavesInsercion
        Inherits FormBase

#Region " Declaraciones "

        Private _TableForm As TableLayoutPanel

#End Region

#Region " Propiedades "

        Public Property Folder As DesktopConfig.Folder

#End Region

#Region " Eventos "

        Private Sub CBarrasDesktopTextBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
                If cbarrasDesktopCBarrasControl.Text = "" Then
                    SendKeys.Send("{TAB}")
                Else
                    AceptarButton.Focus()
                End If
            End If
        End Sub

        Private Sub FormLlavesIndexacion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            ControlCrearCampos()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim CBarrasFolder = cbarrasDesktopCBarrasControl.Text

                'Obtiene La data adiconal que corresponde al folder sea por llaves o por Codigo de barras
                If CBarrasFolder <> "" Then
                    _Folder = Utilities.FindFolderByCBarras(dbmArchiving, CBarrasFolder)
                    If _Folder.Existe = False Then _Folder = Utilities.FindFileByCBarras(dbmArchiving, CBarrasFolder)
                Else
                    _Folder = FolderLlaves()
                End If

                If _Folder.Existe = False Then
                    DesktopMessageBoxControl.DesktopMessageShow("No se han encontradao coincidencias con las llaves o el codigo de barras", "", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Else
                    ' Iniciar insercion
                    Dim f As New FormInserciones()

                    f.Folder = Me.Folder
                    f.LoadData()
                    f.ShowDialog()
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Inserciones", ex)
            Finally
                dbmArchiving.Connection_Close()
            End Try

            cbarrasDesktopCBarrasControl.Focus()

        End Sub
        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)
        End Sub

        Public Sub ControlCrearCampos()
            SpaceFlowLayoutPanel.Controls.Add(ControlesCargue(Program.RiskGlobal.LLavesProyecto))
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
        Public Sub Limpiarformulario()
            ControlLimpiarLlaves(Program.RiskGlobal.LLavesProyecto)
            If cbarrasDesktopCBarrasControl.Text <> "" Then
                cbarrasDesktopCBarrasControl.Text = ""
                cbarrasDesktopCBarrasControl.Focus()
            End If
        End Sub

#End Region

#Region " Funciones "

        Public Function ControlesCargue(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As TableLayoutPanel
            Try
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

                    Dim LlaveTextBox As New DesktopTextBoxControl
                    LlaveTextBox.Name = NombreLlave.Replace(" ", "_")
                    LlaveTextBox.Width = 200
                    Select Case Llave.Tipo
                        Case DesktopConfig.CampoTipo.Numerico
                            LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
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
            Catch
                Throw
            End Try
        End Function
        Public Function FolderLlaves(Optional ByVal CBarras As String = "") As DesktopConfig.Folder
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Try
                dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If CBarras = "" Then
                    Dim Llave As DesktopConfig.StrLlaves
                    Dim Llaves As New List(Of DesktopConfig.StrLlaves)
                    For Each VarLlave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                        Dim ControlLlave As DesktopTextBoxControl = CType(Utilities.FindControl(SpaceFlowLayoutPanel, VarLlave.Nombre), DesktopTextBoxControl)
                        Llave.id_Llave = VarLlave.Id
                        Llave.Nombre_Llave = VarLlave.Nombre
                        Llave.Valor_Llave = ControlLlave.Text
                        Llaves.Add(Llave)
                    Next

                    Return Utilities.FindFolderByKeys(dbmArchiving, Llaves, Program.RiskGlobal, Program.DesktopGlobal.ConnectionStrings)
                Else
                    Return Utilities.FindFolderByCBarras(dbmArchiving, CBarras)
                End If

            Catch
                Throw
            Finally
                If (dbmArchiving IsNot Nothing) Then dbmArchiving.Connection_Close()
            End Try
        End Function

#End Region

    End Class

End Namespace