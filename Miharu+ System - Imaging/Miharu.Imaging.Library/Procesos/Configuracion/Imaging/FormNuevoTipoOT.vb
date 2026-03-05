Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop

Namespace Procesos.Configuracion.Imaging

    Public Class FormNuevoTipoOT
        Inherits Desktop.Library.FormBase

#Region " Declaraciones "

        Private _fk_OT_Tipo As Short = 0
        Private _fk_entidad As Short
        Private _fk_proyecto As Short
        Private _Nombre As String
        Private _Descripcion As String
        Private _Eliminado As Boolean

#End Region

#Region " Constructor "

        Public Sub New(id_OT_Tipo As Short, fk_entidad As Short, fk_proyecto As Short, Nombre As String, Descripcion As String, Eliminado As Boolean)
            _fk_OT_Tipo = id_OT_Tipo

            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto
            _Nombre = Nombre
            _Descripcion = Descripcion
            _Eliminado = Eliminado

            InitializeComponent()
        End Sub

        Public Sub New(fk_entidad As Short, fk_proyecto As Short)
            _fk_OT_Tipo = 0
            _fk_entidad = fk_entidad
            _fk_proyecto = fk_proyecto

            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormNuevaValidacion_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            If _fk_OT_Tipo <> 0 Then
                NombreDesktopTextBox.Text = _Nombre
                DescripcionReporteDesktopTextBox.Text = _Descripcion
                EliminadoCheckBox.Checked = _Eliminado
            End If
        End Sub

        Private Sub CerrarButton_Click(sender As System.Object, e As EventArgs) Handles CerrarButton.Click
            Me.Close()
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
                    .fk_Entidad = _fk_entidad
                    .fk_Proyecto = _fk_proyecto
                    .fk_usuario_log = Program.Sesion.Usuario.id
                    .Eliminado = EliminadoCheckBox.Checked
                    .fecha_log = DateTime.Now
                End With

                dmImaging.Transaction_Begin()

                If _fk_OT_Tipo = 0 Then
                    OTTipo.id_OT_Tipo = dmImaging.SchemaProcess.TBL_OT_Tipo.DBNextId_for_id_OT_Tipo(OTTipo.fk_Entidad.Value, OTTipo.fk_Proyecto.Value)
                    dmImaging.SchemaProcess.TBL_OT_Tipo.DBInsert(OTTipo)
                Else
                    dmImaging.SchemaProcess.TBL_OT_Tipo.DBUpdate(OTTipo, _fk_entidad, _fk_proyecto, _fk_OT_Tipo)
                End If

                dmImaging.Transaction_Commit()

                DesktopMessageBoxControl.DesktopMessageShow("Datos Guardados con exito", "Tipo OT", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.DialogResult = DialogResult.OK

            Catch ex As Exception
                dmImaging.Transaction_Rollback()
                Me.DialogResult = DialogResult.Cancel
                DesktopMessageBoxControl.DesktopMessageShow("Guardar", ex)
            Finally
                If dmImaging IsNot Nothing Then dmImaging.Connection_Close()
                Me.Close()
            End Try
        End Sub

#End Region

    End Class
End Namespace