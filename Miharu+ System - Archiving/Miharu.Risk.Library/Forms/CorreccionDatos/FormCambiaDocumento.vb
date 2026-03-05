Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Risk.Library.Forms.CBarras

Namespace Forms.CorreccionDatos

    Public Class FormCambiaDocumento
        Inherits FormBase


#Region " Declaraciones "

        Dim CBarras As String
        Dim Documento As DBArchiving.SchemaCore.CTA_Configuracion_DocumentoDataTable

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

        Private Sub FormCambiaDocumento_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            CargaFile()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub ReimpresionCBarrasButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReimpresionCBarrasButton.Click
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Dim CBarrasImpresion As New FormCBarrasFolderFile
            CBarrasImpresion.CBarras = Utilities.FindFileByCBarras(dbmArchiving, CBarras).CBarras_File
            dbmArchiving.Connection_Close()
            CBarrasImpresion.ShowDialog()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

            Try

                'Actualiza tipologia y codigo de barras
                dbmCore.Transaction_Begin()
                Dim Resultado = dbmCore.SchemaProcess.PA_Cambia_Tipologia.DBExecute(Documento(0).fk_Expediente, Documento(0).fk_Folder, Documento(0).id_File, CInt(DocumentoNuevoDesktopComboBox.SelectedValue))
                dbmCore.Transaction_Commit()

                CBarras = Resultado.Rows(0)("CBarras_File").ToString()
                CargaFile()
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
            Finally
                dbmArchiving.Connection_Close()
                dbmCore.Connection_Close()
            End Try

        End Sub

#End Region

#Region " Metodos "

        Public Sub CargaCombos()
            Dim viewTipologias As New DataView(Program.RiskGlobal.Tipologias)
            viewTipologias.Sort = Program.RiskGlobal.Tipologias.id_tipologiaColumn.ColumnName
            Utilities.LlenarCombo(DocumentoNuevoDesktopComboBox, viewTipologias.ToTable(), Program.RiskGlobal.Tipologias.id_documentoColumn, Program.RiskGlobal.Tipologias.nombre_tipologiaColumn)
        End Sub

        Public Sub CargaFile()
            'Imprime CBarras File
            CBarrasFileLabel.Text = CBarras

            'Busca la Data del documento
            Dim dbmArchiving As New DBArchiving.DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)
            Documento = dbmArchiving.SchemaCore.CTA_Configuracion_Documento.DBFindByCBarras_File(CBarras)
            dbmArchiving.Connection_Close()

            'Valida que el documento exista
            If Documento.Count = 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("Hubo problemas buscando el codigo de barras del documento, Es posible que no este en un estado valido [10:Cargado, 20:Destape, 30:Mesa de Control]", "Error CBarras", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Me.Close()
            Else
                CargaCombos()
                TipoloiaActualLabel.Text = Documento(0).Nombre_Tipologia
                DocumentoActualLabel.Text = Documento(0).Nombre_Documento
            End If
        End Sub

#End Region

    End Class

End Namespace