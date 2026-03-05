Imports Slyg.Tools

Namespace Forms.Parametrizacion

    Public Class FormTipologia
        Inherits Form

#Region "Eventos"

        Private Sub NuevoDocumentoButton_Click(sender As System.Object, e As EventArgs) Handles NuevoDocumentoButton.Click
            Dim fNuevaTipologia As New FormNuevaTipologia

            If fNuevaTipologia.ShowDialog() = DialogResult.OK Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    ''dbmCore.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmCore.Transaction_Begin()

                    Dim TipologiaType = New DBCore.SchemaConfig.TBL_TipologiaType
                    TipologiaType.id_Tipologia = dbmCore.SchemaConfig.TBL_Tipologia.DBNextId()
                    TipologiaType.Nombre_Tipologia = fNuevaTipologia.NombreTipologia
                    TipologiaType.Tipologia_Eliminado = fNuevaTipologia.Eliminado
                    TipologiaType.fk_Usuario_Log = Program.Sesion.Usuario.id
                    TipologiaType.Fecha_Log = SlygNullable.SysDate
                    dbmCore.SchemaConfig.TBL_Tipologia.DBInsert(TipologiaType)

                    dbmCore.Transaction_Commit()

                Catch ex As Exception
                    If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                    Throw
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try

                CargarTipologias()
            End If

        End Sub

        Private Sub DocumentoDataGridView_CellDoubleClick(sender As System.Object, e As DataGridViewCellEventArgs) Handles DocumentoDataGridView.CellDoubleClick

            Dim TipologiaSeleccionada = DocumentoDataGridView.Rows(DocumentoDataGridView.SelectedCells(0).RowIndex)
            Dim idTipologia = CShort(TipologiaSeleccionada.Cells("id_Tipologia").Value)
            Dim fNuevaTipologia As New FormNuevaTipologia

            If fNuevaTipologia.ShowDialog() = DialogResult.OK Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim TipologiaType = New DBCore.SchemaConfig.TBL_TipologiaType
                    TipologiaType.Nombre_Tipologia = fNuevaTipologia.NombreTipologia
                    TipologiaType.Tipologia_Eliminado = fNuevaTipologia.Eliminado
                    dbmCore.SchemaConfig.TBL_Tipologia.DBUpdate(TipologiaType, idTipologia)

                Catch
                    Throw
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try

                CargarTipologias()
            End If
        End Sub

        Private Sub FormTipologia_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
            CargarTipologias()
        End Sub

#End Region

#Region "Metodos"

        Private Sub CargarTipologias()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim TipologiaData = dbmCore.SchemaConfig.TBL_Tipologia.DBGet(Nothing)
                DocumentoDataGridView.DataSource = TipologiaData
                DocumentoDataGridView.Refresh()
            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region

    End Class

End Namespace