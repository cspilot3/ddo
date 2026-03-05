Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library

Namespace Forms.Devoluciones

    Public Class FormSeleccionOTs
        Inherits FormBase

#Region " Declaraciones "

        Public _OTs As List(Of Integer)
        Public _OTFinal As Integer

#End Region

#Region " Constructor "

        Public Sub New(ByVal OT As List(Of Integer))
            _OTs = OT
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormSeleccionOTs_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            OTDesktopTextBox.Focus()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click

            If ValidaOT() Then
                If ValidaOTEntidad() Then
                    _OTFinal = CInt(OTDesktopTextBox.Text)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("La OT Digitada no se encuentra en la base de datos o no pertenece a la entidad y proyecto seleccionados, Por favor digite una OT diferente a las seleccionadas y que pertenezca a la Entidad, Proyecto seleccionados.", "OT no valida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("La OT Digitada se encuentra en uno de los documentos seleccionados para la operación, Por favor digite una OT diferente a las seleccionadas y que pertenezca a la Entidad, Proyecto seleccionados.", "OT duplicada", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            End If

        End Sub

#End Region

#Region " Funciones "

        Public Function ValidaOT() As Boolean
            Dim OtUser = CInt(OTDesktopTextBox.Text)

            For Each OTUnica As Integer In _OTs
                If OTUnica = OtUser Then
                    Return False
                End If
            Next

            Return True
        End Function

        Public Function ValidaOTEntidad() As Boolean
            Dim OtUser = CInt(OTDesktopTextBox.Text)
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
                Dim OTTable = dbmArchiving.SchemaRisk.TBL_OT.DBGet(OtUser)

                If OTTable.Count > 0 Then

                    If Program.RiskGlobal.Entidad <> OTTable(0).fk_Entidad Or Program.RiskGlobal.Proyecto <> OTTable(0).fk_Proyecto Then
                        Return False
                    Else
                        Return True
                    End If

                Else
                    Return False
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("ValidaOTEntidad", ex)
                Return False
            Finally
                dbmArchiving.Connection_Close()
            End Try

        End Function

        'Public Function ValidaOTCarpeta() As Boolean
        '    Dim OtUser = CInt(OTDesktopTextBox.Text)

        '    Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

        '    Try
        '        dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
        '        Dim OTTable = dbmArchiving.SchemaRisk.TBL_Folder.DBGet(

        '        If OTTable.Count > 0 Then

        '            If Program.RiskGlobal.Entidad <> OTTable(0).fk_Entidad Or Program.RiskGlobal.Proyecto <> OTTable(0).fk_Proyecto Then
        '                Return False
        '            Else
        '                Return True
        '            End If

        '        Else
        '            Return False
        '        End If

        '    Catch ex As Exception
        '        DMB.DesktopMessageShow("ValidaOTEntidad", ex)
        '        Return False
        '    Finally
        '        dbmArchiving.Connection_Close()
        '    End Try

        'End Function

#End Region

#Region " Propiedades "

        Public ReadOnly Property OTSelect As Integer
            Get
                Return _OTFinal
            End Get
        End Property

#End Region

    End Class

End Namespace