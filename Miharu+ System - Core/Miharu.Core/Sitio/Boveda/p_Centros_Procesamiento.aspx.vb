Imports DBCore

Namespace Sitio.Boveda

    Public Class p_Centros_Procesamiento
        Inherits PopupBase

#Region "Declaraciones"

        Private fk_Entidad As Short
        Private fk_Sede As Short
        Private id_Boveda As Short

#End Region

#Region "Eventos"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            fk_Entidad = CShort(GlobalParameterCollection("fk_Entidad").DefaultValue)
            fk_Sede = CShort(GlobalParameterCollection("fk_Sede").DefaultValue)
            id_Boveda = CShort(GlobalParameterCollection("id_Boveda").DefaultValue)

            If Not IsPostBack Then
                ConfigPage()
            End If

        End Sub

        Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
            Guardar()
        End Sub

#End Region

#Region "Metodos"

        Public Sub Guardar()
            Dim dbmCore As DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCoreDataBaseManager(ConnectionString.Core)
                dbmCore.Connection_Open(MySesion.Usuario.id)
                Dim CentroType As New SchemaCustody.TBL_Boveda_Centro_ProcesamientoType
                CentroType.fk_Boveda = id_Boveda
                CentroType.fk_Entidad_Boveda = fk_Entidad
                CentroType.fk_Entidad_Centro_Procesamiento = fk_Entidad
                CentroType.fk_Sede_Boveda = fk_Sede
                CentroType.fk_Sede_Centro_Procesamiento = fk_Sede

                dbmCore.Transaction_Begin()
                For Each Row As GridViewRow In grdCentroProcesamiento.Rows
                    Dim Centro = CType(MySesion.Parameter("Centro"), DataTable)
                    CentroType.fk_Centro_Procesamiento = CShort(Centro.Rows(Row.RowIndex)("id_Centro_Procesamiento"))
                    Dim si_aplica = CType(Row.FindControl("Aplica"), CheckBox).Checked
                    dbmCore.SchemaCustody.TBL_Boveda_Centro_Procesamiento.DBDelete(CentroType.fk_Entidad_Boveda, CentroType.fk_Sede_Boveda, CentroType.fk_Centro_Procesamiento)

                    If si_aplica Then dbmCore.SchemaCustody.TBL_Boveda_Centro_Procesamiento.DBInsert(CentroType)
                Next
                dbmCore.Transaction_Commit()
            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                MyMasterPage.ShowMessage(ex.Message, MsgBoxIcon.IconError, "")
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

        End Sub

        Public Sub ConfigPage()
            Dim dmCore As New DBCoreDataBaseManager(ConnectionString.Core)
            dmCore.Connection_Open(MySesion.Usuario.id)
            Dim CentrosProcesamiento = dmCore.Schemadbo.PA_Boveda_Centros_Procesamiento.DBExecute(fk_Entidad, fk_Sede, id_Boveda)
            dmCore.Connection_Close()

            MySesion.Parameter("Centro") = CentrosProcesamiento
            grdCentroProcesamiento.DataSource = CentrosProcesamiento
            grdCentroProcesamiento.DataBind()

            For Each row As GridViewRow In grdCentroProcesamiento.Rows
                CType(row.FindControl("Aplica"), CheckBox).Checked = CBool(CentrosProcesamiento(row.RowIndex)("Aplica"))
            Next
        End Sub

#End Region


    End Class
End Namespace