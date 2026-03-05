Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config

Namespace Forms.Recepcion

    Public Class FormPrecinto
        Inherits FormBase

#Region " Funciones "

        Public Sub InsetarPrecinto()

            'If validar() Then

            Dim dm As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dm.Connection_Open(Program.Sesion.Usuario.id)
                dm.Transaction_Begin()




                Dim registro As New SchemaRisk.TBL_PrecintoType
                registro.Fecha_Precinto = CDate(Date.Now.ToShortDateString())
                registro.Destapado = False
                registro.fk_Entidad = Program.RiskGlobal.Entidad
                registro.fk_Proyecto = Program.RiskGlobal.Proyecto
                registro.fk_Usuario_Log = Program.Sesion.Usuario.id
                registro.id_Precinto = PrecintoDesktopTextBox1.Text

                If CLng(PuntoOrigenNombreComboBox.SelectedValue) > 0 Then
                    registro.Fk_Punto = CLng(PuntoOrigenNombreComboBox.SelectedValue)
                End If

                registro.FK_Sede_Procesamiento = CShort(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede)

                dm.SchemaRisk.TBL_Precinto.DBInsert(registro)

                dm.Transaction_Commit()

            Catch ex As Exception
                dm.Transaction_Rollback()
                DesktopMessageBoxControl.DesktopMessageShow("El número de precinto ya existe.", "Pecinto Existente", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally
                dm.Connection_Close()

                PrecintoDesktopTextBox1.Text = ""
                PrecintoDesktopTextBox1.Focus()
                PrecintoDesktopTextBox2.Text = ""
                PuntoOrigenCodigoComboBox.SelectedValue = -1
                PuntoOrigenNombreComboBox.SelectedValue = -1
            End Try
            ' End If

        End Sub

        Public Sub ValidarPrecinto()


            Dim dm As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)

            Try
                dm.Connection_Open(Program.Sesion.Usuario.id)

                Dim Precinto = PrecintoDesktopTextBox1.Text

                Dim PrecintoDataTable = dm.SchemaRisk.TBL_Precinto.DBGet(PrecintoDesktopTextBox1.Text, Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto)

                If PrecintoDataTable.Count <= 0 Then
                    If DesktopMessageBoxControl.DesktopMessageShow("El número de precinto no es válido, Desea crearlo?.", "Precinto No Válido", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = Windows.Forms.DialogResult.OK Then

                        If (Utilities.ValidarPermiso(Permisos.Risk.Autorizaciones, Program.AccesoDesktopAssembly, "Autorizar el destape del precinto " & PrecintoDesktopTextBox1.Text, Program.Sesion.Usuario, Program.DesktopGlobal.SecurityServiceUrl, Program.DesktopGlobal.ClientIpAddress)) Then

                            InsetarPrecinto()

                            Dim PrecintoAutorizado As New SchemaRisk.TBL_Precintos_AutorizadosType
                            PrecintoAutorizado.fk_Entidad = Program.RiskGlobal.Entidad
                            PrecintoAutorizado.fk_Proyecto = Program.RiskGlobal.Proyecto
                            PrecintoAutorizado.fk_Precinto = Precinto
                            PrecintoAutorizado.fk_Usuario_Destape = Program.Sesion.Usuario.id
                            PrecintoAutorizado.Login_Usuario_Autorizador = CType(IIf(Miharu.Desktop.Library.FormAutorizacion.UsuarioAutorizador = Nothing, Program.Sesion.Usuario.Login, Miharu.Desktop.Library.FormAutorizacion.UsuarioAutorizador), String)
                            PrecintoAutorizado.Fecha_Log = Date.Now

                            dm.SchemaRisk.TBL_Precintos_Autorizados.DBInsert(PrecintoAutorizado)

                        End If
                    End If
                End If


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error validando número de precinto.", "Precinto", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Finally
                dm.Connection_Close()

                PrecintoDesktopTextBox1.Text = ""
                PrecintoDesktopTextBox1.Focus()
                PrecintoDesktopTextBox2.Text = ""
                PuntoOrigenCodigoComboBox.SelectedValue = -1
                PuntoOrigenNombreComboBox.SelectedValue = -1
            End Try

        End Sub

        Public Function ValidarDigitacion() As Boolean
            If PrecintoDesktopTextBox1.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el número de precinto.", "Recepción", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                PrecintoDesktopTextBox1.Focus()
                PrecintoDesktopTextBox1.SelectAll()
            ElseIf PrecintoDesktopTextBox2.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el número de precinto.", "Recepción", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                PrecintoDesktopTextBox2.Focus()
                PrecintoDesktopTextBox2.SelectAll()
            ElseIf PrecintoDesktopTextBox1.Text <> PrecintoDesktopTextBox2.Text Then
                DesktopMessageBoxControl.DesktopMessageShow("Los precintos capturados no coinciden.", "Recepción", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
                PrecintoDesktopTextBox2.Focus()
                PrecintoDesktopTextBox2.SelectAll()
            Else
                Return True
            End If

            Return False
        End Function

        Private Sub CargarPuntos()
            Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                Dim DatatablePuntos = dbmArchiving.SchemaRisk.CTA_Puntos_Cliente_Lista.DBFindByFk_EntidadFk_Proyecto(CInt(Program.RiskGlobal.Entidad), CInt(Program.RiskGlobal.Proyecto))

                Utilities.Llenarcombo_(PuntoOrigenNombreComboBox, DatatablePuntos, DatatablePuntos.Id_PuntoColumn.ColumnName, DatatablePuntos.Nombre_PuntoColumn.ColumnName, True)
                Utilities.Llenarcombo_(PuntoOrigenCodigoComboBox, DatatablePuntos, DatatablePuntos.Id_PuntoColumn.ColumnName, DatatablePuntos.Codigo_PuntoColumn.ColumnName, True)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarPuntos", ex)
            Finally
                If Not dbmArchiving Is Nothing Then dbmArchiving.Connection_Close()
            End Try

        End Sub

        Private Function validar() As Boolean

            If CInt(PuntoOrigenCodigoComboBox.SelectedValue) = -1 Then

                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Punto de Origen: Código.", "Recepción", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                PuntoOrigenCodigoComboBox.Focus()
                Return False
            End If

            If CInt(PuntoOrigenNombreComboBox.SelectedValue) = -1 Then
                PuntoOrigenNombreComboBox.Focus()
                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Punto de Origen: Nombre.", "Recepción", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                Return False
            End If
            Return True

        End Function

#End Region

#Region " Eventos "

        Private Sub FormPrecinto_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            CargarPuntos()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If ValidarDigitacion() Then
                If Not Program.RiskGlobal.Usa_Validacion_Destape Then
                    ValidarPrecinto()
                Else
                    InsetarPrecinto()
                End If

            End If
        End Sub

        Private Sub PrecintoDesktopTextBox1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles PrecintoDesktopTextBox1.GotFocus
            PrecintoDesktopTextBox1.PasswordChar = CChar("")
        End Sub

        Private Sub PrecintoDesktopTextBox1_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles PrecintoDesktopTextBox1.LostFocus
            PrecintoDesktopTextBox1.PasswordChar = CChar("*")
        End Sub

        Private Sub PrecintoDesktopTextBox1_LostFocus(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles PrecintoDesktopTextBox1.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If PrecintoDesktopTextBox1.Text = "" Then
                    CancelarButton.Focus()
                Else
                    PrecintoDesktopTextBox2.Focus()
                End If
            End If
        End Sub

        Private Sub PrecintoDesktopTextBox2_LostFocus(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles PrecintoDesktopTextBox2.KeyPress
            If e.KeyChar = ChrW(Keys.Enter) Or e.KeyChar = ChrW(Keys.Tab) Then
                If PrecintoDesktopTextBox2.Text = "" Then
                    CancelarButton.Focus()
                Else
                    PuntoOrigenCodigoComboBox.Focus()
                End If
            End If
        End Sub

        Private Sub PuntoOrigenCodigoComboBox_Leave(sender As Object, e As System.EventArgs) Handles PuntoOrigenCodigoComboBox.Leave

            If PuntoOrigenCodigoComboBox.FindStringExact(PuntoOrigenCodigoComboBox.Text) < 0 Then
                PuntoOrigenCodigoComboBox.Focus()
                PuntoOrigenCodigoComboBox.SelectedValue = -1
                PuntoOrigenNombreComboBox.SelectedValue = -1
            End If
        End Sub

        Private Sub PuntoOrigenNombreComboBox_Leave(sender As Object, e As System.EventArgs) Handles PuntoOrigenNombreComboBox.Leave

            If PuntoOrigenNombreComboBox.FindStringExact(PuntoOrigenNombreComboBox.Text) < 0 Then
                PuntoOrigenNombreComboBox.Focus()
                PuntoOrigenCodigoComboBox.SelectedValue = -1
                PuntoOrigenNombreComboBox.SelectedValue = -1
            End If
        End Sub

        Private Sub PuntoOrigenCodigoComboBox2_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles PuntoOrigenCodigoComboBox.SelectedValueChanged
            Dim IdPunto As Integer = Convert.ToInt32(PuntoOrigenCodigoComboBox.SelectedValue)

            If IdPunto > 0 And CInt(PuntoOrigenCodigoComboBox.SelectedValue) <> CInt(PuntoOrigenNombreComboBox.SelectedValue) Then
                PuntoOrigenNombreComboBox.SelectedValue = IdPunto
            End If
        End Sub

        Private Sub PuntoOrigenNombreComboBox2_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles PuntoOrigenNombreComboBox.SelectedValueChanged
            Dim IdPunto As Integer = Convert.ToInt32(PuntoOrigenNombreComboBox.SelectedValue)

            If IdPunto > 0 And CInt(PuntoOrigenNombreComboBox.SelectedValue) <> CInt(PuntoOrigenCodigoComboBox.SelectedValue) Then
                PuntoOrigenCodigoComboBox.SelectedValue = IdPunto
            End If
        End Sub

#End Region

    End Class

End Namespace