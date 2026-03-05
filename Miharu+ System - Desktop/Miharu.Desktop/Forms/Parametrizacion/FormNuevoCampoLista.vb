Imports Slyg.Tools
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl

Namespace Forms.Parametrizacion
    Public Class FormNuevoCampoLista

#Region " Declaraciones "

        Private _idEntidad As Short
        Private _idCampoLista As Short
        Private _nombreCampoLista As String

#End Region

#Region " Propiedades "

        Public Property IdEntidad() As Short
            Get
                Return _idEntidad
            End Get
            Set(ByVal value As Short)
                _idEntidad = value
            End Set
        End Property
        Public Property IdCampoLista() As Short
            Get
                Return _idCampoLista
            End Get
            Set(ByVal value As Short)
                _idCampoLista = value
            End Set
        End Property
        Public Property NombreCampoLista() As String
            Get
                Return _nombreCampoLista
            End Get
            Set(ByVal value As String)
                _nombreCampoLista = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub FormNuevoCampoLista_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            EntidadTextBox.Text = _idEntidad.ToString
            IdCampoListaTextBox.Text = _idCampoLista.ToString
            NombreCampoListaTextBox.Text = _nombreCampoLista

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim campoListaItemDataTable = dbmCore.SchemaConfig.PA_Campo_Lista_Item_Get.DBExecute(_idEntidad, _idCampoLista)

                CampoListaItemDataGridView.AutoGenerateColumns = False
                CampoListaItemDataGridView.DataSource = campoListaItemDataTable
                CampoListaItemDataGridView.Refresh()

            Catch ex As Exception
                DMB.DesktopMessageShow("Cargar Campo Lista", ex)
            End Try

        End Sub
        Private Sub AceptarButton_Click(sender As System.Object, e As EventArgs) Handles AceptarButton.Click
            GuardarItems()
        End Sub
        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            DialogResult = DialogResult.Cancel
        End Sub

#End Region

#Region " Metodos "

        Private Sub GuardarItems()

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                'dbmCore.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmCore.Transaction_Begin()

                Dim fkEntidad As Short = CShort(EntidadTextBox.Text)
                Dim fkCampoLista As Short

                If IdCampoListaTextBox.Text = "0" Then
                    fkCampoLista = dbmCore.SchemaConfig.TBL_Campo_Lista.DBNextId_for_id_Campo_Lista(fkEntidad)
                    Dim nuevoCampoListaRow As New DBCore.SchemaConfig.TBL_Campo_ListaType

                    With nuevoCampoListaRow
                        .fk_Entidad = fkEntidad
                        .id_Campo_Lista = fkCampoLista
                        .Nombre_Campo_Lista = NombreCampoListaTextBox.Text
                        .fk_Usuario_Log = Program.Sesion.Usuario.id
                        .Fecha_Log = SlygNullable.SysDate
                    End With

                    dbmCore.SchemaConfig.TBL_Campo_Lista.DBInsert(nuevoCampoListaRow)
                Else
                    fkCampoLista = CShort(IdCampoListaTextBox.Text)
                    Dim campoListaRow = dbmCore.SchemaConfig.TBL_Campo_Lista.DBGet(fkEntidad, fkCampoLista)(0).ToTBL_Campo_ListaType

                    With campoListaRow
                        .Nombre_Campo_Lista = NombreCampoListaTextBox.Text
                    End With

                    dbmCore.SchemaConfig.TBL_Campo_Lista.DBUpdate(campoListaRow, fkEntidad, fkCampoLista)
                End If

                dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBDelete(fkEntidad, fkCampoLista, Nothing)

                For Each nuevoCampoListaItemRow As DataGridViewRow In CampoListaItemDataGridView.Rows
                    If Not nuevoCampoListaItemRow.IsNewRow() Then
                        Dim campoListaItemRow = New DBCore.SchemaConfig.TBL_Campo_Lista_ItemType

                        With campoListaItemRow
                            .fk_Entidad = CShort(EntidadTextBox.Text)
                            .fk_Campo_Lista = fkCampoLista
                            .id_Campo_Lista_Item = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBNextId(fkEntidad, fkCampoLista)
                            .Etiqueta_Campo_Lista_Item = nuevoCampoListaItemRow.Cells(0).Value.ToString
                            .Valor_Campo_Lista_Item = nuevoCampoListaItemRow.Cells(1).Value.ToString
                        End With

                        dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBInsert(campoListaItemRow)
                    End If
                Next

                dbmCore.Transaction_Commit()

            Catch ex As Exception
                dbmCore.Transaction_Rollback()
                DMB.DesktopMessageShow("Guardar Campo Lista", ex)
            Finally
                If dbmCore IsNot Nothing Then dbmCore.Connection_Close()
            End Try

            DialogResult = DialogResult.OK
        End Sub

#End Region

    End Class
End Namespace