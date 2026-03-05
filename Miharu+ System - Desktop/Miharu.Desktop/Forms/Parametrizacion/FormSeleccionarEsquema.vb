Imports DBCore.SchemaConfig
Imports DBCore.SchemaSecurity
Imports Miharu.Desktop.Library.Config

Namespace Forms.Parametrizacion

    Public Class FormSeleccionarEsquema

        Public ReadOnly Property IdEntidad() As Short
            Get
                Return CType(entidadComboBox.SelectedValue, Short)
            End Get
        End Property

        Public ReadOnly Property NombreEntidad() As String
            Get
                Return entidadComboBox.Text
            End Get
        End Property
        
        Public ReadOnly Property IdProyecto() As Short
            Get
                Return CType(proyectoComboBox.SelectedValue, Short)
            End Get
        End Property

        Public ReadOnly Property NombreProyecto() As String
            Get
                Return proyectoComboBox.Text
            End Get
        End Property

        Public ReadOnly Property IdEsquema() As Short
            Get
                Return CType(esquemaComboBox.SelectedValue, Short)
            End Get
        End Property

        Public ReadOnly Property NombreEsquema() As String
            Get
                Return esquemaComboBox.Text
            End Get
        End Property


#Region " Eventos "

        Private Sub FormSeleccionarEsquema_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarEntidades()
        End Sub

        Private Sub entidadComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles entidadComboBox.SelectedIndexChanged
            CargarProyectos()
        End Sub

        Private Sub proyectoComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles proyectoComboBox.SelectedIndexChanged
            CargarEsquemas()
        End Sub

        Private Sub esquemaComboBox_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles esquemaComboBox.SelectedIndexChanged
            ActivarOpciones()
        End Sub

        Private Sub closeButton_Click(sender As System.Object, e As EventArgs) Handles closeButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub okButton_Click(sender As System.Object, e As EventArgs) Handles okButton.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarEntidades()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim entidadDataTable = dbmCore.SchemaSecurity.CTA_Entidad.DBGet(0, New CTA_EntidadEnumList(CTA_EntidadEnum.Nombre_Entidad, True))
                entidadComboBox.Fill(entidadDataTable, entidadDataTable.id_EntidadColumn, entidadDataTable.Nombre_EntidadColumn)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarProyectos()
        End Sub

        Private Sub CargarProyectos()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim proyectoDataTable = dbmCore.SchemaConfig.TBL_Proyecto.DBGet(Me.IdEntidad, Nothing, 0, New TBL_ProyectoEnumList(TBL_ProyectoEnum.Nombre_Proyecto, True))
                proyectoComboBox.Fill(proyectoDataTable, proyectoDataTable.id_ProyectoColumn, proyectoDataTable.Nombre_ProyectoColumn)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            CargarEsquemas()
        End Sub

        Private Sub CargarEsquemas()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim esquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(Me.IdEntidad, Me.IdProyecto, Nothing, 0, New TBL_EsquemaEnumList(TBL_EsquemaEnum.Nombre_Esquema, True))
                esquemaComboBox.Fill(esquemaDataTable, esquemaDataTable.id_EsquemaColumn, esquemaDataTable.Nombre_EsquemaColumn)

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try

            ActivarOpciones()
        End Sub

        Private Sub ActivarOpciones()
            okButton.Enabled = esquemaComboBox.SelectedIndex >= 0
        End Sub

#End Region

    End Class

End Namespace