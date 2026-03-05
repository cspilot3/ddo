Imports Slyg.Data.Manager
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls

Namespace Forms.Reportes
    Public Class FormConfirmacionReporte

#Region " Declaraciones "

        Public Property Consulta() As String

        Public Property Conexion() As DataBaseManager

#End Region
#Region " Constructores "

        Public Sub New(ByVal ConnectionStrings_Core As String, ByVal ConnectionStrings_Tools As String)
            InitializeComponent()
            ResultadosDataGridView.Conection_String_Core = ConnectionStrings_Core
            ResultadosDataGridView.Conection_String_Tools = ConnectionStrings_Tools
        End Sub

#End Region
#Region " Eventos "

        Private Sub FormConfirmacionReporte_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            Consultar(_Conexion)
        End Sub

        Private Sub ok_Click(sender As System.Object, e As EventArgs) Handles ok.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub cancel_Click(sender As System.Object, e As EventArgs) Handles cancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Consultar(dbmCore As DataBaseManager)
            Try
                Dim cSql As String = _Consulta

                Dim resultados = SqlData.ExecuteQuery(cSql, dbmCore)
                Dim tabla As DataTable

                tabla = resultados.Tables(0)
                ResultadosDataGridView.InternalGridView.DataSource = tabla
                
            Catch ex As Exception
                DesktopMessageBox.DesktopMessageBoxControl.DesktopMessageShow("EjecutarConfirmacion", New Exception("Por favor revise si esta insertando el archivo correcto :" + ex.Message))
            End Try

        End Sub
#End Region

    End Class
End Namespace