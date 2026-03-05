Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config

Public Class FormFechaProcesoOT

    Private strFechaProceso As String = ""
    Public OT_Valida As Boolean = True

    Public Property Fecha_Proceso() As String
        Get
            Return Me.strFechaProceso
        End Get
        Set(value As String)
            Me.strFechaProceso = value
        End Set
    End Property

    Public Property OT() As Slyg.Tools.SlygNullable(Of String)
        Get
            If (IsNothing(OTDesktopComboBox.SelectedValue)) Then
                Return DBNull.Value
            Else
                Return CStr(OTDesktopComboBox.SelectedValue)
            End If
        End Get
        Set(ByVal value As Slyg.Tools.SlygNullable(Of String))
            OTDesktopComboBox.SelectedValue = value.Value.ToString
        End Set

    End Property

    Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        If Validar() Then
            Me.Fecha_Proceso = Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd")
            Me.OT = Me.OTDesktopComboBox.SelectedValue.ToString()
            Me.Close()
        Else
            Return
        End If

    End Sub


    Private Sub FormFechaProcesoOT_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        CargarOT()
        'Me.AcceptButton = btnAceptar
    End Sub

    Private Sub CargarOT()
        Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
        Try
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

            Dim OTDataTable As DBImaging.SchemaProcess.TBL_OTDataTable

            If IsNothing(OTDesktopComboBox.SelectedValue) Then
                OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoid_OT(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(dtpFechaProceso.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), Nothing)
            Else
                OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoid_OT(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(dtpFechaProceso.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), CInt(OTDesktopComboBox.SelectedValue))
            End If

            OTDesktopComboBox.DataSource = Nothing

            If OTDataTable.Count > 0 Then
                Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True, "-1", "--TODOS--")
            End If
        Catch ex As Exception
            DesktopMessageBoxControl.DesktopMessageShow("Cargar OT", ex)
        Finally
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try
    End Sub

#Region " Funciones "
    Public Function Validar() As Boolean
        If OTDesktopComboBox.Items.Count <= 0 Then
            MessageBox.Show("La Fecha de Proceso seleccionada no contiene OT(s)", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            OT_Valida = False
            Return False
        End If

        Return True
    End Function
#End Region

    Private Sub dtpFechaProceso_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFechaProceso.ValueChanged
        CargarOT()
    End Sub
End Class