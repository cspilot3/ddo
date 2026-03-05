Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports UMV.Plugin.Imaging.UMV
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Tools.Progress
Imports Miharu.Desktop.Library.Config
Imports System.Text
Imports Miharu.Security.Library.Session
Imports Miharu.Desktop.Library
Imports System.Data.SqlClient

Namespace Imaging.Estiba
    Public Class FormCreacionEstibas
        Inherits FormBase

#Region "Declaraciones"
        Public _plugin As UMVPlugin
        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

#End Region
#Region " Contructores "

        Public Sub New(ByVal UMV_DesktopPlugin As UMVPlugin)
            InitializeComponent()

            _plugin = UMV_DesktopPlugin

        End Sub
#End Region

#Region "Métodos"
        Private Function getEstiba(Caja As String, EntidadId As Int32, ProyectoId As Int32, TipoArchivo As String, Serie As String, ruta As String, Usuario As String,
                                   Consultar As Boolean, Insertar As Boolean) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.PA_Exportacion_Data_FileStoreProcedure = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "DECLARE " +
"@ExisteCajaEmpaqueCerrada INT " +
",@ExisteCajaEmpaqueAbierta INT " +
",@ExisteCajaEmpaque INT " +
",@ExisteCajaEstiba INT " +
",@Codigo_Estiba INT " +
",@fk_Estiba INT " +
"	IF @Consultar_Caja = 1 " +
"BEGIN" +
"		 SET @ExisteCajaEstiba = (SELECT  count(ed.Caja)  FROM [DB_Miharu.Integration].[UMV].[TBL_Estiba] est " +
"								INNER JOIN [DB_Miharu.Integration].[UMV].TBL_Estiba_Detalle ed " +
"								ON ed.fk_estiba = est.CodigoEstiba " +
"								WHERE ed.Caja = @Caja " +
"								AND est.Tipo_Archivo = @Tipo_Archivo) " +
"			IF @ExisteCajaEstiba >= 1 " +
"			 SELECT 1 AS ExisteEstiba , @ExisteCajaEstiba AS ExisteEstiba, 'Existe en la tabla de estiba' AS ExiteCajaEstiba " +
"			ELSE " +
"			 SET @ExisteCajaEmpaqueCerrada =(SELECT COUNT(E.Precinto) " +
"									  FROM [DB_Miharu.Imaging_Core].Process.TBL_Empaque E " +
"									  INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_OT OT " +
"									  ON OT.fk_Entidad = @fk_Entidad " +
"									  AND OT.fk_Proyecto = @FK_Proyecto " +
"									  AND OT.id_OT = E.fk_OT " +
"									  INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data ED " +
"									  ON ED.fk_OT = E.fk_OT " +
"									  AND ED.fk_Empaque = E.id_Empaque " +
"									  AND ED.fk_Campo = 1 " +
"									  AND CAST(ED.Data_Campo AS Varchar(100)) = @Serie " +
"									  INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data ED2 " +
"									  ON ED2.fk_OT = E.fk_OT " +
"									  AND ED2.fk_Empaque = E.id_Empaque " +
"									  AND ED2.fk_Campo = 3 " +
"									  AND ED2.Data_Campo = @Tipo_Archivo " +
"									  WHERE E.Precinto = @Caja " +
"									  and E.Cerrado = 1) " +
"				IF @ExisteCajaEmpaqueCerrada >= 1 " +
"					BEGIN " +
"					SELECT 0 as Exitoso, @ExisteCajaEmpaqueCerrada as ExisteEmpaque, 'Existe en el empaque' AS ExisteCajaEmpaque " +
"                                    End " +
"				ELSE " +
"				  SET @ExisteCajaEmpaqueAbierta =(SELECT COUNT(E.Precinto) " +
"									  FROM [DB_Miharu.Imaging_Core].Process.TBL_Empaque E " +
"									  INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_OT OT " +
"									  ON OT.fk_Entidad = @fk_Entidad " +
"									  AND OT.fk_Proyecto = @FK_Proyecto " +
"									  AND OT.id_OT = E.fk_OT " +
"									  INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data ED " +
"									  ON ED.fk_OT = E.fk_OT " +
"									  AND ED.fk_Empaque = E.id_Empaque " +
"									  AND ED.fk_Campo = 1 " +
"									  AND ED.Data_Campo = @Serie " +
"									  INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data ED2 " +
"									  ON ED2.fk_OT = E.fk_OT " +
"									  AND ED2.fk_Empaque = E.id_Empaque " +
"									  AND ED2.fk_Campo = 3 " +
"									  WHERE E.Precinto = @Caja " +
"									  ) " +
"					IF @ExisteCajaEmpaqueAbierta = 0 " +
"						SELECT 2 as Error, @ExisteCajaEmpaqueAbierta as ExisteEmpaque , 'No cumple con algún parametro por favor revisar si la caja esta cerrada y cumple con el tipo de archivo y serie' AS Existe_Caja " +
" END " +
"ELSE " +
"	IF @Insertar_Caja = 1 " +
"		 BEGIN " +
"			SET @Codigo_Estiba = (SELECT CASE " +
"									WHEN (SELECT MAX(CodigoEstiba) FROM [DB_Miharu.Integration].[UMV].[TBL_Estiba]) IS NULL THEN '0' " +
"									ELSE (SELECT MAX(CodigoEstiba) FROM [DB_Miharu.Integration].[UMV].[TBL_Estiba]) " +
"								  END) " +
"			INSERT INTO [DB_Miharu.Integration].[UMV].[TBL_Estiba] " +
"				([CodigoEstiba] " +
"				,[Tipo_Archivo] " +
"				,[Serie] " +
"				,[exportada] " +
"				,[ruta] " +
"				,[fk_Usuario_Log] " +
"				,[Fecha_Log]) " +
"                VALUES " +
"				((@Codigo_Estiba + 1) " +
"				,@Tipo_Archivo " +
"				,@Serie " +
"				,0 " +
"				,@Ruta " +
"				,@fk_Usuario_Log " +
"				,GETDATE()) " +
"			SET @fk_Estiba = @@IDENTITY " +
"			INSERT INTO [DB_Miharu.Integration].[UMV].TBL_Estiba_Detalle([fk_Estiba] ,[Caja]) " +
"			VALUES (@fk_Estiba, @Caja) " +
"			SELECT 0 AS Exitoso, @Insertar_Caja as InsertaCaja, 'Se insertó correctamente' AS InsertadoEstiba " +
"                                              END "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@Caja", Caja),
                   New SqlParameter("@fk_Entidad", EntidadId),
                   New SqlParameter("@FK_Proyecto", ProyectoId),
                   New SqlParameter("@Tipo_Archivo", TipoArchivo),
                   New SqlParameter("@Serie", Serie),
                   New SqlParameter("@Ruta", ruta),
                   New SqlParameter("@fk_Usuario_Log", Usuario),
                   New SqlParameter("@Consultar_Caja", Consultar),
                   New SqlParameter("@Insertar_Caja", Insertar)
               }

                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function


        Private Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
                Try
                    dt = New DataTable
                    If params.Length > 0 Then
                        da.SelectCommand.Parameters.AddRange(params)
                    End If
                    If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(dt)
                    Return dt
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (da IsNot Nothing) Then da.SelectCommand.Connection.Close()
                End Try
                Return dt
            End Using
        End Function
#End Region


        Private Sub btnAgregarCaja_Click(sender As System.Object, e As System.EventArgs) Handles btnAgregarCaja.Click


            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            If (Me.tbnumerocaja.Text = "" Or TipoArchivoComboBox.Text = "" Or SerieTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Nigun campo debe estar no debe estar Vacio!!!", "Campos Vacios", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)


                If Me.EstibasDataGridView.Rows.Count > 0 Then
                    Me.EstibasDataGridView.DataSource = Nothing
                End If

                Me.tbnumerocaja.Focus()
                Me.Cursor = Cursors.Default
            Else
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.UMVConnectionString)
                    dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                    'Dim Resultado = getEstiba(tbnumerocaja.Text, _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, TipoArchivoComboBox.Text.ToString(), SerieTextBox.Text.ToString(), "", _plugin.Manager.Sesion.Usuario.id.ToString(), 1, 0)
                    Dim Resultado = dbmIntegration.SchemaUMV.PA_Creacion_Estibas.DBExecute(tbnumerocaja.Text, _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, TipoArchivoComboBox.Text.ToString(), SerieTextBox.Text.ToString(), "", _plugin.Manager.Sesion.Usuario.ToString(), 1, 0)

                    If Resultado.Rows.Item(0).ItemArray(0) >= 1 Then
                        DesktopMessageBoxControl.DesktopMessageShow(Resultado.Rows.Item(0).ItemArray(2), "Error", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Else

                        If EstibasDataGridView.Rows.Count() >= 1 Then
                            dr = dt.NewRow()
                            dr("Id") = EstibasDataGridView.Rows.Item(0).Cells(0).Value + 1
                            dr("Caja") = tbnumerocaja.Text
                            dr("TipoArchivo") = TipoArchivoComboBox.Text
                            dr("Serie") = SerieTextBox.Text
                            dt.Rows.Add(dr)
                            EstibasDataGridView.DataSource = dt
                        Else
                            dt.Columns.Add(New DataColumn("Id", GetType(String)))
                            dt.Columns.Add(New DataColumn("Caja", GetType(String)))
                            dt.Columns.Add(New DataColumn("TipoArchivo", GetType(String)))
                            dt.Columns.Add(New DataColumn("Serie", GetType(String)))

                            dr = dt.NewRow()
                            dr("Id") = 1
                            dr("Caja") = tbnumerocaja.Text
                            dr("TipoArchivo") = TipoArchivoComboBox.Text
                            dr("Serie") = SerieTextBox.Text
                            dt.Rows.Add(dr)
                            EstibasDataGridView.DataSource = dt
                        End If
                    End If

                    Me.Cursor = Windows.Forms.Cursors.Default

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Error en btnBuscarCaja_Click()", ex)
                    Me.Cursor = Windows.Forms.Cursors.Default
                Finally
                    If (dbmIntegration IsNot Nothing) Then
                        dbmIntegration.Connection_Close()
                    End If
                End Try
            End If
        End Sub

        Private Sub btnguardarEstiba_Click(sender As System.Object, e As System.EventArgs) Handles btnguardarEstiba.Click
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Try

                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.UMVConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                For Each Item As DataRow In dt.Rows
                    Dim Caja = Item("Caja").ToString
                    Dim TipoArchivo = Item("TipoArchivo").ToString
                    Dim serie = Item("Serie").ToString
                    Dim Resultado = getEstiba(Caja, _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, TipoArchivo, serie, "", _plugin.Manager.Sesion.Usuario.id.ToString(), 0, 1)
                    'Dim Resultado = dbmIntegration.SchemaUMV.PA_Creacion_Estibas.DBExecute(Caja, _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, TipoArchivo, serie, "", _plugin.Manager.Sesion.Usuario.id.ToString(), 0, 1)

                    If Resultado.Rows.Item(0).ItemArray(0) = 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("Se ha insertado con éxito!!", "Exitoso", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No se insertaron los datos, por favor valide", "Error", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Next
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error en btnguardarEstiba_Click()", ex)
                Me.Cursor = Windows.Forms.Cursors.Default
            Finally
                If (dbmIntegration IsNot Nothing) Then
                    dbmIntegration.Connection_Close()
                End If
            End Try

        End Sub

        Private Sub btnsalir_Click(sender As System.Object, e As System.EventArgs) Handles btnsalir.Click
            Me.Close()
        End Sub

    End Class
End Namespace


