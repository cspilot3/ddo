Imports System.Windows.Forms
Imports DBArchiving
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.MesaControlFisicos

    Public Class FormCodigoVsLlavesDeceval
        Inherits FormBase

#Region " Declaraciones "

        Dim _CBarras As String
        Dim TableForm As TableLayoutPanel

#End Region

#Region " Constructor "

        Sub New(ByVal CBarras As String)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _CBarras = CBarras
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormCodigoVsLlaves_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CBarrasLabel.Text = CStr(_CBarras)
            SpacePanel.Controls.Add(CrearPeticionLlaves(Program.RiskGlobal.LLavesProyecto))
        End Sub

        Private Sub DestaparButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DestaparButton.Click
            ValidarLlaves()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Function ValidaLlavesCBarras(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As DesktopConfig.DatosCore
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim Table = dbmArchiving.Schemadbo.CTA_Folder_llaves.DBFindByfk_entidadfk_proyectoCBarras_folder(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, _CBarras)
            dbmArchiving.Connection_Close()

            Dim Valida As DesktopConfig.DatosCore
            Valida.Existe = False
            Valida.Expediente = ""
            Valida.Folder = ""

            Dim Filtro(Program.RiskGlobal.LLavesProyecto.Count - 1) As String
            Dim i As Integer = 0

            For Each llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                Dim ControlLlave As DesktopTextBoxControl = CType(Utilities.FindControl(SpacePanel, llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl)
                Filtro(i) = "(nombre_proyecto_llave = '" & llave.Nombre & "' and valor_llave='" & ControlLlave.Text & "')"
                i += 1
            Next

            Dim view As DataView = Utilities.clonarDataTable(Table).DefaultView
            view.RowFilter = Join(Filtro, " or ")


            For Each row As DataRow In Utilities.CountTable(view.ToTable, "id_folder", "fk_expediente").Rows
                If CInt(row("count").ToString) >= Program.RiskGlobal.LLavesProyecto.Count Then
                    Valida.Existe = True
                    Valida.Expediente = row("fk_expediente").ToString
                    Valida.Folder = row("id_folder").ToString
                End If
            Next

            Return Valida

        End Function

        Public Function CrearPeticionLlaves(ByVal Llaves As List(Of DesktopConfig.LlaveProyecto)) As TableLayoutPanel
            Try
                TableForm = New TableLayoutPanel
                Try
                    TableForm.ColumnCount = 2
                    TableForm.RowCount = Llaves.Count
                    TableForm.Width = 500
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
                    Select Case Llave.Tipo
                        Case DesktopConfig.CampoTipo.Numerico
                            LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                        Case DesktopConfig.CampoTipo.Fecha
                            LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                        Case DesktopConfig.CampoTipo.Texto
                            LlaveTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                    End Select
                    LlaveTextBox.Width = 150

                    TableForm.Controls.Add(LlaveLabel, 0, i)
                    TableForm.Controls.Add(LlaveTextBox, 1, i)

                    i += 1
                Next

                TableForm.Refresh()

                Return TableForm
            Catch
                Throw
            End Try
        End Function

        Public Sub ValidarLlaves()
            Dim Valida As DesktopConfig.DatosCore = ValidaLlavesCBarras(Program.RiskGlobal.LLavesProyecto)

            If Valida.Existe = True Then
                Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

                Try
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    dbmArchiving.Transaction_Begin()
                    Dim FolderUpdate As New SchemaRisk.TBL_FolderType With {.Validacion_CBarra = True}
                    dbmArchiving.SchemaRisk.TBL_Folder.DBUpdate(FolderUpdate, CLng(Valida.Expediente), CShort(Valida.Folder), Nothing)
                    dbmArchiving.Transaction_Commit()

                    Dim formMesa As New FormMesaControlFisicosDeceval(CBarrasLabel.Text, DesktopConfig.TipoCaptura.Primera_Captura)
                    formMesa.ShowDialog()
                    Me.Close()

                Catch
                    dbmArchiving.Transaction_Rollback()
                    Throw
                Finally
                    dbmArchiving.Connection_Close()
                End Try
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Las llaves digitadas no concuerdan con el codigo de barras", "Error de llaves", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                For Each llave As DesktopConfig.LlaveProyecto In Program.RiskGlobal.LLavesProyecto
                    Dim ControlLlave As DesktopTextBoxControl = CType(Utilities.FindControl(SpacePanel, llave.Nombre.Replace(" ", "_")), DesktopTextBoxControl)
                    ControlLlave.Clear()
                Next
                CType(Utilities.FindControl(SpacePanel, Program.RiskGlobal.LLavesProyecto(0).Nombre.Replace(" ", "_")), DesktopTextBoxControl).Focus()
            End If

        End Sub

#End Region

    End Class

End Namespace