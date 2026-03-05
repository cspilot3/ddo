Imports Miharu.Security._clases

Namespace _sitio.administracion.seguridad

    Partial Public Class parametros
        Inherits FormBase

#Region " Declaraciones "

        Private Const MyPathPermiso As String = "1.2.5"

        Private tblBase As DBSecurity.SchemaConfig.TBL_ParametroDataTable

#End Region

#Region " Eventos "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not MyBase.ValidarNavegacion(Me.GetType().BaseType.FullName, MyPathPermiso) Then Return

            If Not Me.IsPostBack Then
                Config_Page()
                ActivarOpciones(True)
            Else
                Load_Data()
            End If
        End Sub

        Private Sub Page_HijaClose() Handles Me.HijaClose

        End Sub

        Private Sub ibSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ibSave.Click
            GuardarCambios()
        End Sub

#End Region

#Region " Metodos "

        Private Sub Config_Page()
            tblBase = New DBSecurity.SchemaConfig.TBL_ParametroDataTable
            Me.MySesion.Pagina.Parameter("tblBase") = tblBase

            ' cargar parametros
            Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)

            Try
                dbmSecurity.Connection_Open(MySesion.Usuario.id)

                dbmSecurity.SchemaConfig.TBL_Parametro.DBFill(tblBase, Nothing)

            Catch ex As Exception
                Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)
            Finally
                dbmSecurity.Connection_Close()
            End Try

            Visualizar_Resultados()
        End Sub

        Private Sub Load_Data()
            tblBase = CType(Me.MySesion.Pagina.Parameter("tblBase"), DBSecurity.SchemaConfig.TBL_ParametroDataTable)
        End Sub

        Private Sub Visualizar_Resultados()
            Dim RowParametro As DBSecurity.SchemaConfig.TBL_ParametroRow
            Dim Texto As TextBox

            gvBase.DataSource = tblBase
            gvBase.DataBind()

            For Each Fila As GridViewRow In gvBase.Rows
                RowParametro = CType(tblBase.Rows(Fila.DataItemIndex), DBSecurity.SchemaConfig.TBL_ParametroRow)

                Texto = CType(Fila.FindControl("txtValor"), TextBox)
                Texto.Text = RowParametro.Valor_Parametro_Sistema
            Next
        End Sub

        Private Sub GuardarCambios()
            If Validar() Then
                Dim dbmSecurity As New DBSecurity.DBSecurityDataBaseManager(MyBase.ConnectionString.Security & Program.DataRemoting)
                
                Try
                    UpdateParametros()
                    'dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat

                    dbmSecurity.Connection_Open(MySesion.Usuario.id)
                    dbmSecurity.Transaction_Begin()

                    dbmSecurity.SchemaConfig.TBL_Parametro.DBSaveTable(tblBase)

                    dbmSecurity.Transaction_Commit()

                    Me.Master.ShowAlert("Los datos se almacenaron correctamente", MiharuMasterForm.MsgBoxIcon.IconInformation)

                Catch ex As Exception
                    dbmSecurity.Transaction_Rollback()
                    Master.ShowAlert(ex.Message, MiharuMasterForm.MsgBoxIcon.IconError)

                Finally
                    dbmSecurity.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub UpdateParametros()
            Dim RowParametro As DBSecurity.SchemaConfig.TBL_ParametroRow
            Dim Texto As TextBox

            For Each Fila As GridViewRow In gvBase.Rows
                RowParametro = CType(tblBase.Rows(Fila.DataItemIndex), DBSecurity.SchemaConfig.TBL_ParametroRow)

                Texto = CType(Fila.FindControl("txtValor"), TextBox)
                RowParametro.Valor_Parametro_Sistema = Texto.Text
            Next
        End Sub

        Private Sub ActivarOpciones(ByVal nActivo As Boolean)
            ibSave.Visible = nActivo
            divSave.Style("display") = CStr(IIf(ibSave.Visible, "inline", "none"))
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace