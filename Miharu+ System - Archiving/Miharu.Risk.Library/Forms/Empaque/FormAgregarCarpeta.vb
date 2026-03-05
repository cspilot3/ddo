Imports DBArchiving
Imports DBCore
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports System.Windows.Forms

Namespace Forms.Empaque

    Public Class FormAgregarCarpeta
        Inherits FormBase

#Region " Declaraciones "

        Enum Tipo
            Empacar
            Sacar
            EmpacarCustodia
        End Enum

        Private _Tipo As Tipo
        Private _cBarras_Caja As String
        Public Property CBarras_Caja() As String
            Get
                Return _cBarras_Caja
            End Get
            Set(ByVal value As String)
                _cBarras_Caja = value
            End Set
        End Property


#End Region

#Region " Constructor "

        Sub New(ByVal nTipo As Tipo)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            cbarrasDesktopCBarrasControl.Init(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, Program.DesktopGlobal.ConnectionStrings.Archiving)
            _Tipo = nTipo

        End Sub

#End Region

#Region " Metodos "

        Private Sub AgregarCarpeta()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

            Try
                Dim Resultados As DataTable = dbmArchiving.Schemadbo.PA_Empaca_Folder_Caja_Final.DBExecute(cbarrasDesktopCBarrasControl.Text, Program.RiskGlobal.ID_CajaCustodia, Program.RiskGlobal.Folder_Tipo, Program.Sesion.Usuario.id, Program.RiskGlobal.Usa_Empaque_Adicion)

                If Resultados.Rows.Count > 0 Then
                    If CInt(Resultados.Rows(0)("CORRECTO")) = 1 Then
                        FolderLabel.Text = cbarrasDesktopCBarrasControl.Text
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow(Resultados.Rows(0)("MENSAJE").ToString(), "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("AgregarCarpeta", ex)
            End Try

            cbarrasDesktopCBarrasControl.Clear()
            cbarrasDesktopCBarrasControl.Focus()

            dbmArchiving.Connection_Close()
        End Sub

        Private Sub SacarCarpeta()
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dmcore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            dmcore.Connection_Open(Program.Sesion.Usuario.id)

            'Busca que la carpeta digitada se encuentre en estado de Empacado
            Dim Folder = dbmArchiving.Schemadbo.CTA_Folder.DBFindByCBarras_Folderfk_Estado(cbarrasDesktopCBarrasControl.Text, DBCore.EstadoEnum.Empacado)

            'Valida que la carpeta este en la caja seleccionada y se pueda eliminar
            If ValidaSacarCarpeta(Folder, dbmArchiving) Then
                Try
                    dbmArchiving.Transaction_Begin()
                    dmcore.Transaction_Begin()

                    For Each rowFolder As DBArchiving.Schemadbo.CTA_FolderRow In Folder.Rows
                        dbmArchiving.Schemadbo.PA_Saca_Folder_Caja.DBExecute(rowFolder.fk_Expediente, rowFolder.id_Folder, Program.Sesion.Usuario.id, Program.RiskGlobal.Usa_Empaque_Adicion, Program.RiskGlobal.ID_CajaCustodia)
                    Next

                    dbmArchiving.Transaction_Commit()
                    dmcore.Transaction_Commit()
                    DesktopMessageBoxControl.DesktopMessageShow("Se ha extraido la carpeta de la caja con exito", "Documento extraido OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Catch ex As Exception
                    dmcore.Transaction_Rollback()
                    dbmArchiving.Transaction_Rollback()
                    DesktopMessageBoxControl.DesktopMessageShow("SacarCarpeta", ex)
                End Try
            End If

            cbarrasDesktopCBarrasControl.Clear()
            cbarrasDesktopCBarrasControl.Focus()

            dbmArchiving.Connection_Close()
            dmcore.Connection_Close()
        End Sub

        Function ValidaSacarCarpeta(ByVal Folder As DBArchiving.Schemadbo.CTA_FolderDataTable, ByVal dbmArchiving As DBArchiving.DBArchivingDataBaseManager) As Boolean
            Dim validacion As Boolean = True

            If Folder.Count > 0 Then
                Dim RowFolder = Folder(0)

                Dim folderCustodia = dbmArchiving.SchemaCore.CTA_Folder_Custodia.DBFindByfk_Cajafk_Expedientefk_Folder(Program.RiskGlobal.ID_CajaCustodia, RowFolder.fk_Expediente, RowFolder.id_Folder)
                If folderCustodia.Count = 0 Then
                    DesktopMessageBoxControl.DesktopMessageShow("La carpeta no se puede sacar de la caja porque no pertenece a esta", "Error sacando carpeta", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    validacion = False
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("La carpeta no existe en la base de datos o no esta en el proyecto actual", "Error Buscando Carpeta", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                validacion = False
            End If

            Return validacion
        End Function

        Private Sub AgregarCarpetaCajaNueva()
            Dim dbmArchiving As DBArchivingDataBaseManager = Nothing
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmArchiving = New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                dbmCore = New DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim CajaDT = dbmCore.SchemaCustody.TBL_Caja.DBFindByCodigo_Caja(_cBarras_Caja)
                Dim CajaId = CajaDT(0).id_Caja

                Dim Resultados As DataTable = dbmArchiving.Schemadbo.PA_Empaca_Folder_Caja_Nueva.DBExecute(cbarrasDesktopCBarrasControl.Text, CajaId, Program.RiskGlobal.Folder_Tipo, Program.Sesion.Usuario.id, Program.RiskGlobal.Usa_Empaque_Adicion)

                If Resultados.Rows.Count > 0 Then
                    If CInt(Resultados.Rows(0)("CORRECTO")) = 1 Then
                        FolderLabel.Text = cbarrasDesktopCBarrasControl.Text
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow(Resultados.Rows(0)("MENSAJE").ToString(), "Empaque", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("AgregarCarpeta", ex)
            Finally
                If dbmArchiving IsNot Nothing Then dbmArchiving.Connection_Close()
            End Try

            cbarrasDesktopCBarrasControl.Clear()
            cbarrasDesktopCBarrasControl.Focus()

        End Sub

#End Region

#Region " Eventos "

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Select Case _Tipo
                Case Tipo.Empacar
                    AgregarCarpeta()
                Case Tipo.Sacar
                    SacarCarpeta()
                Case Tipo.EmpacarCustodia
                    AgregarCarpetaCajaNueva()
            End Select

        End Sub

        Private Sub FormAgregarCarpeta_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            If _Tipo = Tipo.Empacar Or _Tipo = Tipo.EmpacarCustodia Then
                TipoLabel.Text = "Empaque de carpeta en caja"
                TipoLabel.ForeColor = Drawing.Color.SeaGreen
            Else
                TipoLabel.Text = "Extraer carpeta de caja"
                TipoLabel.ForeColor = Drawing.Color.IndianRed
                CarpetaLabel.Visible = False
                FolderLabel.Visible = False
            End If
        End Sub

        Private Sub cbarrasDesktopCBarrasControl_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbarrasDesktopCBarrasControl.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                If (Not String.IsNullOrWhiteSpace(cbarrasDesktopCBarrasControl.Text)) Then
                    AceptarButton.Focus()
                Else
                    CancelarButton.Focus()
                End If
            End If
        End Sub

#End Region
    End Class

End Namespace