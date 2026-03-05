Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library

Namespace Reportes

    Public Class FormDestape

#Region " Propiedades "

        Public Property FechaProceso() As Date
            Get
                Return dtpFechaInicial.Value.Date
            End Get
            Set(ByVal value As Date)
                dtpFechaInicial.Value = value
            End Set
        End Property

        Public Property Punto() As Integer
            Get
                Return CInt(cmbPunto.SelectedValue)
            End Get
            Set(ByVal value As Integer)
                cmbPunto.SelectedValue = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub FormDestape_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim TipoOT = dbmImaging.SchemaProcess.TBL_OT_Tipo.DBGet(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, Nothing)
                cmbPunto.Fill(TipoOT, TipoOT.id_OT_TipoColumn, TipoOT.Nombre_OT_TipoColumn, True, "", "-1", "Todos...")

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

#End Region

    End Class

End Namespace
