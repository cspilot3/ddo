Imports System.Windows.Forms
Imports DBAgrario

Namespace Firmas.Forms.Destape

    Public Class FormValidarTarjetas

#Region " Declaraciones "
        Private _plugin As FirmasImagingPlugin
#End Region

        Dim CausalesRechazoDT As DataTable
        Dim TarjetasGuardadasDataTable As DataTable

#Region "Constructor"

        Public Sub New(nPlugin As FirmasImagingPlugin)

            InitializeComponent()
            _plugin = nPlugin

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormValidarTarjetas_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load

            CargarOpcionesRechazo()

        End Sub

        Private Sub codigo_barras_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles codigo_barras.KeyPress

            If (e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return)) Then
                Consultar()
                CausalRechazoCkListBox1.Focus()
            End If

        End Sub

        Private Sub ExcluirButton_Click(sender As System.Object, e As EventArgs) Handles ExcluirButton.Click
            RechazarCodigoBarras()
        End Sub

        Private Sub ActualizarButton_Click(sender As System.Object, e As EventArgs) Handles ActualizarButton.Click
            ActualizarOBorrar()
        End Sub

        Private Sub CancelarButton_Click(sender As System.Object, e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarOpcionesRechazo()

            Dim dbmAgrario As DBAgrarioDataBaseManager = Nothing

            Try

                dbmAgrario = New DBAgrarioDataBaseManager(Me._plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                CausalesRechazoDT = dbmAgrario.SchemaFirmas.PA_Get_CausalRechActivo.DBExecute()

            Catch ex As Exception
                Throw New Exception(ex.Message)

            Finally
                If (dbmAgrario IsNot Nothing) Then dbmAgrario.Connection_Close()

            End Try

            CausalRechazoCkListBox1.DataSource = CausalesRechazoDT
            CausalRechazoCkListBox1.DisplayMember = "Descripcion"
            CausalRechazoCkListBox1.ValueMember = "Eliminado"

        End Sub

        Private Sub Consultar()

            If codigo_barras.Text.Trim <> "" Then
                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Try

                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim fecha_Proceso = FechaProcesoPicker.Value
                    Dim Cod_barras = codigo_barras.Text


                    TarjetasGuardadasDataTable = dbmAgrario.SchemaFirmas.PA_Tarjetas_Firmas_Validadas_Destape.DBExecute(Cod_barras, fecha_Proceso)

                    If TarjetasGuardadasDataTable.Rows.Count > 0 Then

                        'Dim VerificarCargue As DataTable = dbmImaging.SchemaProcess.PA_Existencia_OT_enCargue.DBExecute(81, fecha_Proceso)

                        If MessageBox.Show(String.Format("La tarjeta con código de barras: {0}, " & vbCrLf & "ya se encuentra rechazada para la fecha: {1:yyyy/MM/dd}, " & vbCrLf & "¿Desea actualizarla o borrarla?", Cod_barras, fecha_Proceso), "Atención", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                            ActualizarButton.Visible = True
                            ExcluirButton.Visible = False

                            For i = 0 To CausalesRechazoDT.Rows.Count - 1
                                If TarjetasGuardadasDataTable.Select(String.Format("fk_Rechazo = '{0}'", CausalesRechazoDT.Rows(i)(0))).Length <> 0 Then
                                    CausalRechazoCkListBox1.SetItemChecked(i, True)
                                Else
                                    CausalRechazoCkListBox1.SetItemChecked(i, False)
                                End If

                            Next

                        End If
                        Return
                    Else
                        ActualizarButton.Visible = False
                        ExcluirButton.Visible = True
                        For i = 0 To CausalesRechazoDT.Rows.Count - 1
                            CausalRechazoCkListBox1.SetItemChecked(i, False)
                        Next
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message.ToString())
                Finally
                    If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                End Try
            Else
                MessageBox.Show("El código de barras no puede ser vacio.")
            End If

        End Sub

        Private Sub RechazarCodigoBarras()

            If (Validar()) Then
                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

                Dim RechazadosGrabados As Integer = 0
                Dim IndexChecked As Integer
                Dim fkRechazos As String = ""

                Try

                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim fecha_Proceso = FechaProcesoPicker.Value
                    Dim Cod_barras = codigo_barras.Text


                    TarjetasGuardadasDataTable = dbmAgrario.SchemaFirmas.PA_Tarjetas_Firmas_Validadas_Destape.DBExecute(Cod_barras, fecha_Proceso)

                    If TarjetasGuardadasDataTable.Rows.Count = 0 Then

                        For Each IndexChecked In CausalRechazoCkListBox1.CheckedIndices

                            Dim Prb As String = CausalRechazoCkListBox1.GetItemChecked(IndexChecked)

                            dbmAgrario.SchemaFirmas.PA_Guardar_Validacion_Tarjeta.DBExecute(Cod_barras,
                                                                                            fecha_Proceso,
                                                                                            Int16.Parse(CausalesRechazoDT.Rows(IndexChecked)(0)),
                                                                                            _plugin.Manager.Sesion.Usuario.id)
                            RechazadosGrabados += 1

                            If fkRechazos = "" Then
                                fkRechazos = Int16.Parse(CausalesRechazoDT.Rows(IndexChecked)(0))
                            Else
                                fkRechazos = fkRechazos & "," & Int16.Parse(CausalesRechazoDT.Rows(IndexChecked)(0))
                            End If
                        Next

                        If RechazadosGrabados > 0 Then
                            MessageBox.Show("Registro guardado con éxito")
                            LimpiarControles()
                        End If

                        'Insertar Log
                        dbmAgrario.SchemaFirmas.PA_Guardar_Validacion_Tarjeta_Log.DBExecute(Cod_barras, fecha_Proceso, fkRechazos, _plugin.Manager.Sesion.Usuario.id)

                    Else
                        If MessageBox.Show(String.Format("La tarjeta con código de barras: {0}," & vbCrLf & "ya se encuentra rechazada para la fecha: {1:yyyy/MM/dd}," & vbCrLf & "¿Desea actualizarla o borrarla?", Cod_barras, fecha_Proceso), "Atención", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                            ActualizarButton.Visible = True
                            ExcluirButton.Visible = False
                        End If
                        Return
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message.ToString())
                Finally
                    If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub ActualizarOBorrar()

            If codigo_barras.Text.Trim <> "" Then
                Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
                Dim RechazadosAGrabar As Integer = 0
                Dim RechazadosGrabados As Integer = 0
                Dim IndexChecked As Integer
                Dim fkRechazos As String = ""


                Try

                    dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                    dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                    Dim fecha_Proceso = FechaProcesoPicker.Value
                    Dim Cod_barras = codigo_barras.Text

                    Dim TarjetaEstado = dbmAgrario.SchemaFirmas.PA_Validar_Estado_Tarjeta_Rechazos_Destape.DBExecute(Cod_barras, fecha_Proceso)


                        If (TarjetaEstado(0).Cargue > 0 AndAlso TarjetaEstado(0).Estado = 2) Or (TarjetaEstado(0).Cargue = 0) Then

                            For Each IndexChecked In CausalRechazoCkListBox1.CheckedIndices
                                RechazadosAGrabar += 1
                            Next
                            If (RechazadosAGrabar = 0) Then

                                If (MessageBox.Show("Se borrara el registro asociado a este código de barras y fecha de proceso," & vbCrLf & "¿Desea continuar ?", "Atención", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                                    Return
                                End If
                                BorrarCodigoBarras()
                                MessageBox.Show("Registro borrado con éxito")
                                LimpiarControles()
                                Return

                            Else
                                BorrarCodigoBarras()

                                For Each IndexChecked In CausalRechazoCkListBox1.CheckedIndices

                                    Dim Prb As String = CausalRechazoCkListBox1.GetItemChecked(IndexChecked)

                                    dbmAgrario.SchemaFirmas.PA_Guardar_Validacion_Tarjeta.DBExecute(Cod_barras,
                                                                                                    fecha_Proceso,
                                                                                                    Int16.Parse(CausalesRechazoDT.Rows(IndexChecked)(0)),
                                                                                                    _plugin.Manager.Sesion.Usuario.id)
                                    RechazadosGrabados += 1
                                    If fkRechazos = "" Then
                                        fkRechazos = Int16.Parse(CausalesRechazoDT.Rows(IndexChecked)(0))
                                    Else
                                        fkRechazos = fkRechazos & "," & Int16.Parse(CausalesRechazoDT.Rows(IndexChecked)(0))
                                    End If
                                Next

                                If RechazadosGrabados > 0 Then
                                    MessageBox.Show("Registro actualizado con éxito")
                                    LimpiarControles()
                                End If

                                'Insertar Log
                                dbmAgrario.SchemaFirmas.PA_Guardar_Validacion_Tarjeta_Log.DBExecute(Cod_barras, fecha_Proceso, fkRechazos, _plugin.Manager.Sesion.Usuario.id)
                            End If

                        Else

                            MessageBox.Show(String.Format("La tarjeta con código de barras: {0}," & vbCrLf & "cargada para la fecha: {1:yyyy/MM/dd}," & vbCrLf & "debe ser enviada a reprocesos para poder continuar", Cod_barras, fecha_Proceso), "Atención", MessageBoxButtons.OK)

                        End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message.ToString())
                Finally
                    If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                End Try

            Else
                MessageBox.Show("El código de barras no puede ser vacio.")
            End If

        End Sub

        Private Sub BorrarCodigoBarras()

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing

            Try

                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim fecha_Proceso = FechaProcesoPicker.Value
                Dim Cod_barras = codigo_barras.Text

                dbmAgrario.SchemaFirmas.PA_Borrar_Tarjetas_Firmas_Validadas_Destape.DBExecute(Cod_barras, fecha_Proceso)

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString())
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try

        End Sub

        Private Function Validar() As Boolean

            Dim RechazadosAGrabar As Integer = 0

            If codigo_barras.Text.Trim <> "" Then

                For Each IndexChecked In CausalRechazoCkListBox1.CheckedIndices
                    RechazadosAGrabar += 1
                Next
                If (RechazadosAGrabar = 0) Then
                    MessageBox.Show("No se puede rechazar una tarjeta sin ninguna causal de rechazo," & vbCrLf & "Por favor seleccione alguna.")
                    Return False
                End If
            Else
                MessageBox.Show("El código de barras no puede ser vacio.")
                Return False
            End If
            Return True
        End Function

        Private Sub LimpiarControles()
            codigo_barras.Text = ""
            For i = 0 To CausalesRechazoDT.Rows.Count - 1
                CausalRechazoCkListBox1.SetItemChecked(i, False)
            Next
            ActualizarButton.Visible = False
            ExcluirButton.Visible = True
        End Sub

#End Region

    End Class
End Namespace