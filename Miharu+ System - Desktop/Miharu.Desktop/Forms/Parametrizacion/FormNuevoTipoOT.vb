Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Forms.Parametrizacion

    Public Class FormNuevoTipoOT
        Inherits Library.FormBase

#Region " Declaraciones "

        Private _fk_OT_Tipo As Short = 0
        Private _fk_entidad As Short
        Private _fk_proyecto As Short
        Private _Nombre As String
        Private _Descripcion As String

#End Region

#Region " Constructor "

        Public Sub New(id_OT_Tipo As Short, fk_entidad As Short, fk_proyecto As Short, Nombre As String, Descripcion As String)
            _fk_OT_Tipo = id_OT_Tipo

            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto
            _Nombre = Nombre
            _Descripcion = Descripcion

            InitializeComponent()
        End Sub

        Public Sub New()
            _fk_OT_Tipo = 0
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormNuevaValidacion_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            If _fk_OT_Tipo <> 0 Then
                llenarCombos(False)
                EntidadDesktopComboBox.SelectedValue = _fk_entidad
                llenarProyectos(False)
                ProyectoComboBox.SelectedValue = _fk_proyecto
                NombreDesktopTextBox.Text = _Nombre
                DescripcionReporteDesktopTextBox.Text = _Descripcion

                EntidadDesktopComboBox.Enabled = False
                ProyectoComboBox.Enabled = False
            Else
                llenarCombos(True)
            End If

        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            llenarProyectos(True)
        End Sub

        Private Sub AgregarButton_Click(sender As System.Object, e As EventArgs) Handles GuardarButton.Click
            Guardar()
        End Sub

#End Region

#Region " Metodos "

        Public Sub Guardar()
            Dim dmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim OTTipo As New DBImaging.SchemaProcess.TBL_OT_TipoType
                With OTTipo
                    .Nombre_OT_Tipo = NombreDesktopTextBox.Text
                    .Descripcion_OT_Tipo = DescripcionReporteDesktopTextBox.Text
                    .fk_Entidad = CShort(EntidadDesktopComboBox.SelectedValue)
                    .fk_Proyecto = CShort(ProyectoComboBox.SelectedValue)
                End With

                dmImaging.Transaction_Begin()

                If _fk_OT_Tipo = 0 Then
                    OTTipo.id_OT_Tipo = dmImaging.SchemaProcess.TBL_OT_Tipo.DBNextId_for_id_OT_Tipo(OTTipo.fk_Entidad.Value, OTTipo.fk_Proyecto.Value)
                    dmImaging.SchemaProcess.TBL_OT_Tipo.DBInsert(OTTipo)
                Else
                    dmImaging.SchemaProcess.TBL_OT_Tipo.DBUpdate(OTTipo, CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoComboBox.SelectedValue), _fk_OT_Tipo)
                End If

                dmImaging.Transaction_Commit()

                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Datos Guardados con exito", "Tipo OT", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.DialogResult = DialogResult.OK

            Catch ex As Exception
                dmImaging.Transaction_Rollback()
                Me.DialogResult = DialogResult.Cancel
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            Finally
                If dmImaging IsNot Nothing Then dmImaging.Connection_Close()
                Me.Close()
            End Try
        End Sub

        Public Sub llenarCombos(CargarProyectos As Boolean)
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Entidad = dmCore.SchemaSecurity.CTA_Entidad.DBGet()
                EntidadDesktopComboBox.Fill(Entidad, Entidad.id_EntidadColumn, Entidad.Nombre_EntidadColumn)
                EntidadDesktopComboBox.SelectedIndex = 0

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
                If CargarProyectos Then llenarProyectos(True)
            End Try
        End Sub

        Public Sub llenarProyectos(ByVal CargarEsquemas As Boolean)
            Dim dmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmCore.Connection_Open(Program.Sesion.Usuario.id)
                Dim Proyecto = dmCore.SchemaConfig.TBL_Proyecto.DBGet(CShort(EntidadDesktopComboBox.SelectedValue), Nothing)
                ProyectoComboBox.Fill(Proyecto, Proyecto.id_ProyectoColumn, Proyecto.Nombre_ProyectoColumn)
                ProyectoComboBox.SelectedIndex = 0

            Catch
                Throw
            Finally
                If dmCore IsNot Nothing Then dmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace