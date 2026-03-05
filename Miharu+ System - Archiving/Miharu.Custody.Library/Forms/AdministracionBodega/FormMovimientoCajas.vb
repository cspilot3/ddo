Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms

Namespace Forms.AdministracionBodega
    Public Class FormMovimientoCajas

#Region " Declaraciones "

        Private CajaRow As DBCore.SchemaCustody.TBL_CajaRow
        Private PosicionInicialRow As DBCore.SchemaCustody.TBL_Boveda_PosicionRow
        Private PosicionFinalRow As DBCore.SchemaCustody.TBL_Boveda_PosicionRow

#End Region

#Region " Eventos "

        Private Sub FormMovimientoCajas_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Me.CBarrasTextBox.Focus()
        End Sub

        Private Sub FindButton_Click(sender As System.Object, e As System.EventArgs) Handles FindButton.Click
            BuscarCaja()
        End Sub

        Private Sub AsignarButton_Click(sender As System.Object, e As System.EventArgs) Handles AsignarButton.Click
            AsignarPosicion()
        End Sub

        Private Sub FindPosicionButton_Click(sender As System.Object, e As System.EventArgs) Handles FindPosicionButton.Click
            BuscarCajaFinal()
        End Sub

#End Region
        
#Region " Metodos "

        Private Sub BuscarCaja()
            If (ValidarCaja()) Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim byCodigoCaja = dbmCore.SchemaCustody.TBL_Caja.DBFindByCodigo_Caja(CBarrasTextBox.Text)

                    If byCodigoCaja.Count > 0 Then
                        Dim codigoBovedaPosicionInicial = dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Caja(byCodigoCaja(0).id_Caja)

                        If codigoBovedaPosicionInicial.Count > 0 Then
                            CajaRow = byCodigoCaja(0)
                            Me.LimpiarPosicion()
                            Me.PosicionInicialRow = codigoBovedaPosicionInicial(0)
                            Me.MostrarPosicionInicial()
                            'Me.DetallePanel.Visible = True
                            Me.AsignarButton.Visible = False
                            Me.CajaLabel.Text = Me.CajaRow.Codigo_Caja
                            Me.PosicionTextBox.Focus()
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("La caja no tiene una posición asignada", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El código de la caja no es válido", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub BuscarCajaFinal()
            If (ValidarPosicion()) Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                    Dim codigoBovedaPosicion = dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Entidadfk_Sedefk_BovedaCodigo_Boveda_Posicion(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede, Program.DesktopGlobal.BovedaRow.id_Boveda, Me.PosicionTextBox.Text)

                    If codigoBovedaPosicion.Count > 0 Then
                        Dim cajaTipoDataTable = dbmCore.SchemaCustody.TBL_Caja_Tipo.DBGet(Me.CajaRow.fk_Caja_Tipo)
                        If ((codigoBovedaPosicion(0).Alto_Boveda_Posicion < cajaTipoDataTable(0).Alto_Caja_Tipo) Or (CInt(codigoBovedaPosicion(0).Ancho_Boveda_Posicion) < CInt(cajaTipoDataTable(0).Ancho_Caja_Tipo)) Or (CInt(codigoBovedaPosicion(0).Largo_Boveda_Posicion) < CInt(cajaTipoDataTable(0).Largo_Caja_Tipo))) Then
                            DesktopMessageBoxControl.DesktopMessageShow("La caja es mas grande que la posición seleccionada", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Me.PosicionTextBox.SelectAll()
                            Me.PosicionTextBox.Focus()
                        ElseIf codigoBovedaPosicion(0).Isfk_CajaNull Then
                            If (codigoBovedaPosicion(0).Es_Flotante) And (codigoBovedaPosicion(0).Fila_Boveda_Posicion > 1) Then
                                If (dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Entidadfk_Sedefk_Bovedafk_Boveda_Seccionfk_Boveda_EstanteFila_Boveda_PosicionColumna_Boveda_PosicionProfundidad_Boveda_Posicion(Program.DesktopGlobal.BovedaRow.fk_Entidad, Program.DesktopGlobal.BovedaRow.fk_Sede, Program.DesktopGlobal.BovedaRow.id_Boveda, codigoBovedaPosicion(0).fk_Boveda_Seccion, codigoBovedaPosicion(0).fk_Boveda_Estante, CType(codigoBovedaPosicion(0).Fila_Boveda_Posicion - 1, Global.Slyg.Tools.SlygNullable(Of Short)), codigoBovedaPosicion(0).Columna_Boveda_Posicion, codigoBovedaPosicion(0).Profundidad_Boveda_Posicion)(0).Isfk_CajaNull()) Then
                                    DesktopMessageBoxControl.DesktopMessageShow("No se puede asignar una posición flotante si la fila inferior no se encuentra ocupada", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                                    Me.LimpiarPosicion()
                                    Me.PosicionTextBox.SelectAll()
                                    Me.PosicionTextBox.Focus()
                                Else
                                    Me.PosicionFinalRow = codigoBovedaPosicion(0)
                                    Me.MostrarPosicionFinal()
                                End If
                            Else
                                Me.PosicionFinalRow = codigoBovedaPosicion(0)
                                Me.MostrarPosicionFinal()
                            End If
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("La posición seleccionada se encuentra ocupada por la caja: " + dbmCore.SchemaCustody.TBL_Caja.DBGet(codigoBovedaPosicion(0).fk_Caja)(0).Codigo_Caja, "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                            Me.LimpiarPosicion()
                            Me.PosicionTextBox.SelectAll()
                            Me.PosicionTextBox.Focus()
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("El código de la posición no es válido", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                        Me.LimpiarPosicion()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub LimpiarPosicion()
            Me.SeccionLabel.Text = ""
            Me.EstanteLabel.Text = ""
            Me.FilaLabel.Text = ""
            Me.ColumnaLabel.Text = ""
            Me.ProfundidadLabel.Text = ""
            Me.PosicionTextBox.Text = ""
            Me.SeccionLabelOrigen.Text = ""
            Me.EstanteLabelOrigen.Text = ""
            Me.FilaLabelOrigen.Text = ""
            Me.ColumnaLabelOrigen.Text = ""
            Me.ProfundidadLabelOrigen.Text = ""
            Me.CBarrasTextBox.Text = ""
        End Sub

        Private Sub LimpiarPosicionFinal()
            Me.SeccionLabel.Text = ""
            Me.EstanteLabel.Text = ""
            Me.FilaLabel.Text = ""
            Me.ColumnaLabel.Text = ""
            Me.ProfundidadLabel.Text = ""
            Me.PosicionTextBox.Text = ""
        End Sub

        Private Sub MostrarPosicionFinal()
            Me.SeccionLabel.Text = CStr(Me.PosicionFinalRow.fk_Boveda_Seccion)
            Me.EstanteLabel.Text = CStr(Me.PosicionFinalRow.fk_Boveda_Estante)
            Me.FilaLabel.Text = CStr(Me.PosicionFinalRow.Fila_Boveda_Posicion)
            Me.ColumnaLabel.Text = CStr(Me.PosicionFinalRow.Columna_Boveda_Posicion)
            Me.ProfundidadLabel.Text = CStr(Me.PosicionFinalRow.Profundidad_Boveda_Posicion)
            Me.AsignarButton.Visible = True
            Me.AsignarButton.Focus()
        End Sub

        Private Sub MostrarPosicionInicial()
            Me.SeccionLabelOrigen.Text = CStr(Me.PosicionInicialRow.fk_Boveda_Seccion)
            Me.EstanteLabelOrigen.Text = CStr(Me.PosicionInicialRow.fk_Boveda_Estante)
            Me.FilaLabelOrigen.Text = CStr(Me.PosicionInicialRow.Fila_Boveda_Posicion)
            Me.ColumnaLabelOrigen.Text = CStr(Me.PosicionInicialRow.Columna_Boveda_Posicion)
            Me.ProfundidadLabelOrigen.Text = CStr(Me.PosicionInicialRow.Profundidad_Boveda_Posicion)
        End Sub

        Private Sub AsignarPosicion()
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmCore.Transaction_Begin()

                If (dbmCore.SchemaCustody.TBL_Boveda_Posicion.DBGet(Me.PosicionFinalRow.id_Boveda_Posicion)(0).Isfk_CajaNull) Then
                    dbmCore.SchemaCustody.PA_Asignar_Posicion.DBExecute(Me.CajaRow.id_Caja, Me.PosicionFinalRow.id_Boveda_Posicion)
                    DesktopMessageBoxControl.DesktopMessageShow("Movimiento realizado con éxito", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Me.LimpiarPosicion()
                    'Me.DetallePanel.Visible = False
                    Me.CBarrasTextBox.SelectAll()
                    Me.CBarrasTextBox.Focus()
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("La posición seleccionada se encuentra ocupada", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                    Me.LimpiarPosicionFinal()
                End If

                dbmCore.Transaction_Commit()
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                dbmCore.Transaction_Rollback()
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

#End Region
        
#Region " Funciones "

        Private Function ValidarCaja() As Boolean
            If CBarrasTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el código de la caja inicial", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                CBarrasTextBox.Focus()
                Return False
            End If

            Return True
        End Function

        Private Function ValidarPosicion() As Boolean
            If PosicionTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el código de la caja destino", "Movimiento Cajas", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                PosicionTextBox.Focus()
                Return False
            End If

            Return True
        End Function

#End Region

    End Class
End Namespace
