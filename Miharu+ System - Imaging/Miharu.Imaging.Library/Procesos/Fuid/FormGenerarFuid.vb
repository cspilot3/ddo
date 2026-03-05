Imports DBImaging
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Drawing
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports System.Drawing.Imaging
Imports Miharu.Desktop.Controls.DesktopReportViewer
Imports System.Xml.Linq
Imports System.Linq

Public Class FormGenerarFuid

    Private Sub Aceptar_Click(sender As System.Object, e As System.EventArgs) Handles Aceptar.Click

        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
        Dim FuidGeneral As Boolean = True
        Dim TipoFuid As String = CStr(ComboBoxTipoFuid.SelectedValue)
        Dim TipoFestion As String = CStr(TipoGestionComboBox.SelectedValue)
        Dim FUID = dbmImaging.SchemaProcess.PA_Genarar_Fuid.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, TipoFuid, "General", "", TipoFestion)
        Dim objFUID As New FormVisorFuid(FUID, CInt(TipoFuid), FuidGeneral, "")
        objFUID.ShowDialog()

    End Sub

    Private Sub FormGenerarFuid_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
        dbmCore.Connection_Open(1)
        ComboBoxTipoFuid.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBoxTipoFuid.DataSource = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, 1)
        ComboBoxTipoFuid.DisplayMember = "Etiqueta_Campo_Lista_Item"
        ComboBoxTipoFuid.ValueMember = "Valor_Campo_Lista_Item"

        TipoGestionComboBox.DropDownStyle = ComboBoxStyle.DropDownList
        TipoGestionComboBox.DataSource = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBFindByfk_Entidadfk_Campo_Lista(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, 9)
        TipoGestionComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
        TipoGestionComboBox.ValueMember = "Valor_Campo_Lista_Item"
    End Sub
End Class