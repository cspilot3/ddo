Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms

Namespace Forms.AdministracionBodega
    Public Class FormMovimientoCarpetas

#Region " Declaraciones "

        Private CajaInicialRow As DBCore.SchemaCustody.TBL_CajaRow
        Private CajaFinalRow As DBCore.SchemaCustody.TBL_CajaRow
        Private PosicionInicialRow As DBCore.SchemaCustody.TBL_Boveda_PosicionRow
        Private PosicionFinalRow As DBCore.SchemaCustody.TBL_Boveda_PosicionRow

#End Region

#Region " Eventos "

        Private Sub FormMovimientoCarpetas_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Me.CajaInicialTextBox.Focus()
        End Sub

        Private Sub FindButton_Click(sender As System.Object, e As System.EventArgs) Handles FindButton.Click
            BuscarCajaInicial()
        End Sub

        Private Sub FindCajaButton_Click(sender As System.Object, e As System.EventArgs) Handles FindCajaButton.Click
            BuscarCajaFinal()
        End Sub

        Private Sub FindFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles FindFolderButton.Click
            Mover()
        End Sub

#End Region

#Region " Metodos "

        Private Sub BuscarCajaInicial()
            If (ValidarCajaInicial()) Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim byCodigoCaja = dbmCore.SchemaCustody.TBL_Caja.DBFindByCodigo_Caja(CajaInicialTextBox.Text)

                    If byCodigoCaja.Count = 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("El código de la caja no es válido", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        Me.CajaInicialTextBox.SelectAll()
                        Me.CajaInicialTextBox.Focus()
                    ElseIf byCodigoCaja(0).fk_Estado <> 1100 Then
                        DesktopMessageBoxControl.DesktopMessageShow("La caja no se encuentra en custodia", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        Me.CajaInicialTextBox.SelectAll()
                        Me.CajaInicialTextBox.Focus()
                    Else
                        Dim codigoBovedaPosicionInicial = dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Caja(byCodigoCaja(0).id_Caja)
                        If codigoBovedaPosicionInicial.Count > 0 Then
                            Me.CajaInicialRow = byCodigoCaja(0)
                            Me.LimpiarPosicion()
                            Me.PosicionInicialRow = codigoBovedaPosicionInicial(0)
                            Me.MostrarPosicionInicial()
                            'Me.DetallePanel.Visible = True
                            Me.CajaFinalTextBox.SelectAll()
                            Me.CajaFinalTextBox.Focus()
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("La caja no tiene una posición asignada", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub BuscarCajaFinal()
            If (ValidarCajaFinal()) Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim byCodigoCaja = dbmCore.SchemaCustody.TBL_Caja.DBFindByCodigo_Caja(CajaFinalTextBox.Text)

                    If byCodigoCaja.Count > 0 Then
                        Me.CajaFinalRow = byCodigoCaja(0)

                        If (CajaInicialRow.fk_Entidad_Cliente <> CajaFinalRow.fk_Entidad_Cliente) Or (CajaFinalRow.fk_Proyecto_Cliente <> CajaInicialRow.fk_Proyecto_Cliente) Then
                            DesktopMessageBoxControl.DesktopMessageShow("Las cajas no corresponden al mismo proyecto", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Me.CajaFinalTextBox.SelectAll()
                            Me.CajaFinalTextBox.Focus()
                        ElseIf CajaFinalRow.fk_Estado <> 1100 Then
                            DesktopMessageBoxControl.DesktopMessageShow("La caja no se encuentra en custodia", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Me.CajaFinalTextBox.SelectAll()
                            Me.CajaFinalTextBox.Focus()
                        Else
                            Dim byfkCaja = dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Caja(CajaFinalRow.id_Caja)
                            If byfkCaja.Count > 0 Then
                                Me.PosicionFinalRow = byfkCaja(0)
                                Me.MostrarPosicionFinal()
                                Me.cbarrasDesktopCBarrasControl.Visible = True
                                Me.FindFolderButton.Visible = True
                                Me.cbarrasDesktopCBarrasControl.Focus()
                            Else
                                DesktopMessageBoxControl.DesktopMessageShow("La caja no tiene una posición asignada", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                                Me.CajaFinalTextBox.SelectAll()
                                Me.CajaFinalTextBox.Focus()
                            End If
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El código de la caja no es válido", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        Me.CajaFinalTextBox.SelectAll()
                        Me.CajaFinalTextBox.Focus()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub Mover()
            If ValidarFolder() Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim byFolderCustody As DBCore.SchemaCustody.TBL_FolderDataTable = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    'Dim byCbarrasFolder = dbmCore.SchemaProcess.TBL_Folder.DBFindByCBarras_Folder(Me.cbarrasDesktopCBarrasControl.Text)
                    Dim byCbarrasFolder = dbmCore.SchemaCustody.PA_Get_Carpeta_Campo_Empaque.DBExecute(Me.cbarrasDesktopCBarrasControl.Text)

                    If (byCbarrasFolder.Count > 0) Then
                        byFolderCustody = dbmCore.SchemaCustody.TBL_Folder.DBGet(byCbarrasFolder(0).fk_Expediente, byCbarrasFolder(0).id_Folder)

                        If byFolderCustody.Count > 0 Then
                            If byFolderCustody(0).fk_Caja = CajaInicialRow.id_Caja Then
                                Dim FolderCustodyType = New DBCore.SchemaCustody.TBL_FolderType()
                                FolderCustodyType.fk_Caja = CajaFinalRow.id_Caja
                                dbmCore.SchemaCustody.TBL_Folder.DBUpdate(FolderCustodyType, byFolderCustody(0).fk_Expediente, byFolderCustody(0).fk_Folder)

                                dbmCore.SchemaCustody.PA_Actualizar_Caja_File.DBExecute(byFolderCustody(0).fk_Caja, CajaFinalRow.id_Caja, byFolderCustody(0).fk_Expediente, byFolderCustody(0).fk_Folder)

                                DesktopMessageBoxControl.DesktopMessageShow("Movimiento realizado con éxito", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                                Me.LimpiarPosicion()

                                If ChkMantenerCajaOrigenCajaDestino.Checked Then
                                    Me.cbarrasDesktopCBarrasControl.Focus()
                                Else
                                    Me.CajaInicialTextBox.SelectAll()
                                    Me.CajaInicialTextBox.Focus()
                                End If
                            Else
                                DesktopMessageBoxControl.DesktopMessageShow("La carpeta no se encuentra asignada a la caja origen", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            End If
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("La carpeta no se encuentra asignada a una caja", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El código de la carpeta no es válido", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    End If
                Catch ex As Exception
                    'Reversar posibles cambios realizados
                    If byFolderCustody IsNot Nothing And byFolderCustody.Rows.Count > 0 Then
                        Dim FolderCustodyType = New DBCore.SchemaCustody.TBL_FolderType()
                        FolderCustodyType.fk_Caja = byFolderCustody(0).fk_Caja
                        dbmCore.SchemaCustody.TBL_Folder.DBUpdate(FolderCustodyType, byFolderCustody(0).fk_Expediente, byFolderCustody(0).fk_Folder)

                        dbmCore.SchemaCustody.PA_Actualizar_Caja_File.DBExecute(CajaFinalRow.id_Caja, byFolderCustody(0).fk_Caja, byFolderCustody(0).fk_Expediente, byFolderCustody(0).fk_Folder)
                    End If

                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub LimpiarPosicion()

            If Me.ChkMantenerCajaOrigenCajaDestino.Checked Then
                Me.cbarrasDesktopCBarrasControl.Text = ""
            Else
                Me.SeccionLabel.Text = ""
                Me.EstanteLabel.Text = ""
                Me.FilaLabel.Text = ""
                Me.ColumnaLabel.Text = ""
                Me.ProfundidadLabel.Text = ""
                Me.CajaLabel.Text = ""
                Me.SeccionLabelOrigen.Text = ""
                Me.EstanteLabelOrigen.Text = ""
                Me.FilaLabelOrigen.Text = ""
                Me.ColumnaLabelOrigen.Text = ""
                Me.ProfundidadLabelOrigen.Text = ""
                Me.CajaInicialTextBox.Text = ""
                Me.CajaFinalTextBox.Text = ""
                Me.cbarrasDesktopCBarrasControl.Text = ""
            End If
        End Sub

        Private Sub LimpiarPosicionFinal()
            Me.SeccionLabel.Text = ""
            Me.EstanteLabel.Text = ""
            Me.FilaLabel.Text = ""
            Me.ColumnaLabel.Text = ""
            Me.ProfundidadLabel.Text = ""
            Me.CajaLabel.Text = ""
            Me.CajaFinalTextBox.Text = ""
            Me.cbarrasDesktopCBarrasControl.Text = ""
        End Sub

        Private Sub MostrarPosicionFinal()
            Me.SeccionLabel.Text = CStr(Me.PosicionFinalRow.fk_Boveda_Seccion)
            Me.EstanteLabel.Text = CStr(Me.PosicionFinalRow.fk_Boveda_Estante)
            Me.FilaLabel.Text = CStr(Me.PosicionFinalRow.Fila_Boveda_Posicion)
            Me.ColumnaLabel.Text = CStr(Me.PosicionFinalRow.Columna_Boveda_Posicion)
            Me.ProfundidadLabel.Text = CStr(Me.PosicionFinalRow.Profundidad_Boveda_Posicion)
            Me.CajaLabel.Text = Me.CajaFinalRow.Codigo_Caja
        End Sub

        Private Sub MostrarPosicionInicial()
            Me.SeccionLabelOrigen.Text = CStr(Me.PosicionInicialRow.fk_Boveda_Seccion)
            Me.EstanteLabelOrigen.Text = CStr(Me.PosicionInicialRow.fk_Boveda_Estante)
            Me.FilaLabelOrigen.Text = CStr(Me.PosicionInicialRow.Fila_Boveda_Posicion)
            Me.ColumnaLabelOrigen.Text = CStr(Me.PosicionInicialRow.Columna_Boveda_Posicion)
            Me.ProfundidadLabelOrigen.Text = CStr(Me.PosicionInicialRow.Profundidad_Boveda_Posicion)
        End Sub
#End Region

#Region " Funciones "

        Private Function ValidarCajaInicial() As Boolean
            If CajaInicialTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el código de la caja inicial", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                CajaInicialTextBox.Focus()
                Return False
            End If

            Return True
        End Function

        Private Function ValidarCajaFinal() As Boolean
            If CajaFinalTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el código de la caja final", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                CajaFinalTextBox.Focus()
                Return False
            End If

            Return True
        End Function

        Private Function ValidarFolder() As Boolean
            If cbarrasDesktopCBarrasControl.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el código de la carpeta", "Movimiento Carpetas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                cbarrasDesktopCBarrasControl.Focus()
                Return False
            End If

            Return True
        End Function

#End Region

        
        Private Sub cbarrasDesktopCBarrasControl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbarrasDesktopCBarrasControl.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Then
                Mover()
            End If
        End Sub
    End Class
End Namespace