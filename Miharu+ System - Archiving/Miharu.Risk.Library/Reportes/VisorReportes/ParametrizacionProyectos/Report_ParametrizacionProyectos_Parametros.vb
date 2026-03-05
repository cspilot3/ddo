Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config

Namespace Reportes.VisorReportes.ParametrizacionProyectos

    Public Class Report_ParametrizacionProyectos_Parametros

#Region " Propiedades "

        Public Property Proyecto() As Short
            Get
                Return CShort(ProyectoDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                ProyectoDesktopComboBox.SelectedValue = value
            End Set
        End Property

        Public Property Entidad() As Short
            Get
                Return CShort(EntidadDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                EntidadDesktopComboBox.SelectedValue = value
            End Set
        End Property

        Public Property Esquema() As Short
            Get
                Return CShort(EsquemaDesktopComboBox.SelectedValue)
            End Get
            Set(ByVal value As Short)
                EsquemaDesktopComboBox.SelectedValue = value
            End Set
        End Property



#End Region

#Region " Constructores "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            dbmCore.Connection_Open(1)

            Try
                Dim EntidadDataTable = dbmCore.Schemadbo.CTA_Entidad.DBFindByid_Entidad(Nothing)
                Utilities.LlenarCombo(EntidadDesktopComboBox, EntidadDataTable, "id_Entidad", "Nombre_Entidad")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboEntidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmCore.Connection_Close()
            End Try

        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub EntidadDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles EntidadDesktopComboBox.SelectedIndexChanged
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            dbmCore.Connection_Open(1)

            Try
                Dim ProyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidad(CShort(EntidadDesktopComboBox.SelectedValue))
                Utilities.LlenarCombo(ProyectoDesktopComboBox, ProyectoDataTable, "id_Proyecto", "Nombre_Proyecto")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboProyecto", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Metodos "

        Private Sub ProyectoDesktopComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles ProyectoDesktopComboBox.SelectedIndexChanged
            Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

            dbmCore.Connection_Open(1)

            Try
                Dim EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBFindByfk_Entidadfk_Proyecto(CShort(EntidadDesktopComboBox.SelectedValue), CShort(ProyectoDesktopComboBox.SelectedValue))
                Utilities.LlenarCombo(EsquemaDesktopComboBox, EsquemaDataTable, "id_Esquema", "Nombre_Esquema")
            Catch ex As Exception
                MessageBox.Show(ex.Message, "LlenarComboEsquema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace