Imports DBSecurity
Imports DBSecurity.SchemaConfig

Namespace Procesos.Configuracion.Imaging

    Public Class FormAccesos

#Region " Eventos "

        Private Sub FormAccesos_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadData()
        End Sub

        Private Sub SedeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles SedeComboBox.SelectedIndexChanged
            CargarCentros()
        End Sub

        Private Sub CentroProcesamientoComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CentroProcesamientoComboBox.SelectedIndexChanged
            ShowPermisos()
        End Sub

        Private Sub CerrarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub GuardarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GuardarButton.Click
            Save()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadData()
            EntidadLabel.Text = Program.Sesion.Entidad.Nombre

            SedeComboBox.DisplayMember = "Display"
            SedeComboBox.ValueMember = "Value"

            CentroProcesamientoComboBox.DisplayMember = "Display"
            CentroProcesamientoComboBox.ValueMember = "Value"

            CargarSedes()
        End Sub

        Private Sub CargarSedes()
            SedeComboBox.Items.Clear()
            SedeComboBox.SelectedIndex = -1

            SedeComboBox.Items.Add(New Slyg.Tools.GenericItem(Of Short)(-1, "- Todos -"))

            Dim dbmSecurity As DBSecurityDataBaseManager = Nothing

            Try
                dbmSecurity = New DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
                dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

                Dim SedesDataTable = dbmSecurity.SchemaConfig.TBL_Sede.DBGet(Program.Sesion.Entidad.id, Nothing, 0, New TBL_SedeEnumList(TBL_SedeEnum.Nombre_Sede, True))

                For Each SedeRow In SedesDataTable
                    SedeComboBox.Items.Add(New Slyg.Tools.GenericItem(Of Short)(SedeRow.id_Sede, SedeRow.Nombre_Sede))
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try

            SedeComboBox.SelectedIndex = 0

            CargarCentros()
        End Sub

        Private Sub CargarCentros()
            CentroProcesamientoComboBox.Items.Clear()
            CentroProcesamientoComboBox.SelectedIndex = -1

            CentroProcesamientoComboBox.Items.Add(New Slyg.Tools.GenericItem(Of Short)(-1, "- Todos -"))

            If (SedeComboBox.SelectedIndex > 0) Then
                Dim Sede As Slyg.Tools.GenericItem(Of Short) = CType(SedeComboBox.SelectedItem, Slyg.Tools.GenericItem(Of Short))

                Dim dbmSecurity As DBSecurityDataBaseManager = Nothing

                Try
                    dbmSecurity = New DBSecurityDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Security)
                    dbmSecurity.Connection_Open(Program.Sesion.Usuario.id)

                    Dim CentroProcesamientosDataTable = dbmSecurity.SchemaConfig.TBL_Centro_Procesamiento.DBGet(Program.Sesion.Entidad.id, Sede.Value, Nothing, 0, New TBL_Centro_ProcesamientoEnumList(TBL_Centro_ProcesamientoEnum.Nombre_Centro_Procesamiento, True))

                    For Each CentroProcesamientoRow In CentroProcesamientosDataTable
                        CentroProcesamientoComboBox.Items.Add(New Slyg.Tools.GenericItem(Of Short)(CentroProcesamientoRow.id_Centro_Procesamiento, CentroProcesamientoRow.Nombre_Centro_Procesamiento))
                    Next
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                End Try
            End If

            CentroProcesamientoComboBox.SelectedIndex = 0

            ShowPermisos()
        End Sub

        Private Sub ShowPermisos()
            Dim Sede As Slyg.Tools.GenericItem(Of Short) = CType(SedeComboBox.SelectedItem, Slyg.Tools.GenericItem(Of Short))
            Dim Centro As Slyg.Tools.GenericItem(Of Short) = CType(CentroProcesamientoComboBox.SelectedItem, Slyg.Tools.GenericItem(Of Short))

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim AccesosDataTable = dbmImaging.SchemaConfig.PA_Accesos_get.DBExecute(Program.Sesion.Entidad.id, Sede.Value, Centro.Value, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)

                If (AccesosDataTable.Count > 0) Then
                    Dim AccesosRow = AccesosDataTable(0)

                    CargueCheckBox.Checked = (AccesosRow.Total = AccesosRow.Cargue)
                    IndexacionCheckBox.Checked = (AccesosRow.Total = AccesosRow.Indexacion)
                    ReprocesosCheckBox.Checked = (AccesosRow.Total = AccesosRow.Reprocesos)
                    ValidacionesOpcionalesCheckBox.Checked = (AccesosRow.Total = AccesosRow.Validaciones_Opcionales)
                    PreCapturaCheckBox.Checked = (AccesosRow.Total = AccesosRow.Pre_Captura)
                    PrimeraCapturaCheckBox.Checked = (AccesosRow.Total = AccesosRow.Primera_Captura)
                    SegundaCapturaCheckBox.Checked = (AccesosRow.Total = AccesosRow.Segunda_Captura)
                    TerceraCapturaCheckBox.Checked = (AccesosRow.Total = AccesosRow.Tercera_Captura)
                    CalidadCapturaCheckBox.Checked = (AccesosRow.Total = AccesosRow.Calidad_Captura)
                    RecorteCheckBox.Checked = (AccesosRow.Total = AccesosRow.Recorte)
                    CalidadRecorteCheckBox.Checked = (AccesosRow.Total = AccesosRow.Calidad_Recorte)
                    ValidacionListasCheckBox.Checked = (AccesosRow.Total = AccesosRow.Validacion_Listas)
                    CorreccionMaquinaCheckBox.Checked = (AccesosRow.Total = AccesosRow.Correccion_Maquina)
                Else
                    CargueCheckBox.Checked = True
                    IndexacionCheckBox.Checked = True
                    ReprocesosCheckBox.Checked = True
                    ValidacionesOpcionalesCheckBox.Checked = True
                    PreCapturaCheckBox.Checked = True
                    PrimeraCapturaCheckBox.Checked = True
                    SegundaCapturaCheckBox.Checked = True
                    TerceraCapturaCheckBox.Checked = True
                    CalidadCapturaCheckBox.Checked = True
                    RecorteCheckBox.Checked = True
                    CalidadRecorteCheckBox.Checked = True
                    ValidacionListasCheckBox.Checked = True
                    CorreccionMaquinaCheckBox.Checked = True
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub Save()
            Dim Sede As Slyg.Tools.GenericItem(Of Short) = CType(SedeComboBox.SelectedItem, Slyg.Tools.GenericItem(Of Short))
            Dim Centro As Slyg.Tools.GenericItem(Of Short) = CType(CentroProcesamientoComboBox.SelectedItem, Slyg.Tools.GenericItem(Of Short))

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()

                dbmImaging.SchemaConfig.PA_Accesos_set.DBExecute(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Sede.Value, Centro.Value, Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, CargueCheckBox.Checked, IndexacionCheckBox.Checked, ReprocesosCheckBox.Checked, ValidacionesOpcionalesCheckBox.Checked, PreCapturaCheckBox.Checked, PrimeraCapturaCheckBox.Checked, SegundaCapturaCheckBox.Checked, TerceraCapturaCheckBox.Checked, CalidadCapturaCheckBox.Checked, RecorteCheckBox.Checked, CalidadRecorteCheckBox.Checked, ValidacionListasCheckBox.Checked, CorreccionMaquinaCheckBox.Checked)

                dbmImaging.Transaction_Commit()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            MessageBox.Show("Los datos se almacenaron exitosamente", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

#End Region

#Region " Funciones "

#End Region

    End Class

End Namespace