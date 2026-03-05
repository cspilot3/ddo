Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools

Namespace Procesos.Cargue

    Public Class FormCargue

#Region " Declaraciones "
        Public CorrespondenciaDestapeImagenes As Boolean
        Public _OT As Integer
        Public _FechaProceso As Integer
        Public _Data As CargueBase
#End Region

#Region " Eventos "

        Private Sub dgvItems_DataBindingComplete(ByVal sender As System.Object, ByVal e As Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvItems.DataBindingComplete
            lblItems.Text = "Items - [ " & dgvItems.RowCount & " ]"
        End Sub

        Private Sub dgvPaquetes_DataBindingComplete(ByVal sender As System.Object, ByVal e As Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvPaquetes.DataBindingComplete
            lblPaquetes.Text = "Paquetes - [ " & dgvPaquetes.RowCount & " ]"
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (ValidarCargueDestape(_Data)) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Public Sub setData(ByRef nData As CargueBase)

            dsCargue = nData.Datos
            _Data = nData

            dgvPaquetes.DataSource = dsCargue
            dgvItems.DataSource = dsCargue

            dgvPaquetes.Refresh()
            dgvItems.Refresh()

            lblValidos.Text = CStr(nData.Validos)
            lblInvalidos.Text = CStr(nData.Invalidos)
            lblTotal.Text = CStr(nData.Validos + nData.Invalidos)

            AceptarButton.Enabled = (nData.Validos > 0)

            'If Program.ImagingGlobal.ProyectoImagingRow.Correspondencia_Destape_Vs_Imagenes = True Then

            '    Dim nHojasDestape As Integer
            '    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing


            '    Try
            '        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            '        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            '        nHojasDestape = dbmImaging.SchemaProcess.PA_Get_Contenedor_Destape_nHojas.DBExecute(_OT, _FechaProceso, nData.Datos.Paquete(0).Key)

            '        If nData.Validos = nHojasDestape And nData.Validos > 0 Then
            '            CorrespondenciaDestapeImagenes = True
            '        Else
            '            MessageBox.Show("El número de imagenes validas a cargar no corresponde con el reportado en el destape, " + vbCrLf + "verifique el lote, el destape e intente realizar el cargue nuevamente", "Atención")
            '            CorrespondenciaDestapeImagenes = False
            '        End If

            '    Catch
            '        MessageBox.Show("No existe destape registrado para el código de carpeta en la ruta seleccionada")
            '        CorrespondenciaDestapeImagenes = False
            '    Finally
            '        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            '    End Try


            'Else
            '    CorrespondenciaDestapeImagenes = True
            'End If
        End Sub

#End Region

#Region " Funciones "
        Public Function ValidarCargueDestape(ByVal nData As CargueBase) As Boolean


            If Program.ImagingGlobal.ProyectoImagingRow.Correspondencia_Destape_Vs_Imagenes = True Then

                Dim nHojasDestape As Integer
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing


                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                    nHojasDestape = dbmImaging.SchemaProcess.PA_Get_Contenedor_Destape_nHojas.DBExecute(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, _OT, _FechaProceso, nData.Datos.Paquete(0).Key)

                    If nData.Validos = nHojasDestape And nData.Validos > 0 Then
                        CorrespondenciaDestapeImagenes = True
                    Else
                        MessageBox.Show("El número de imágenes a cargar no corresponde con lo digitado en el destape para el contenedor: " & nData.Datos.Paquete(0).Key & ". Verifique el cargue y el destape e intente nuevamente", "Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CorrespondenciaDestapeImagenes = False
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    CorrespondenciaDestapeImagenes = False
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try


            Else
                CorrespondenciaDestapeImagenes = True
            End If

            Return CorrespondenciaDestapeImagenes

        End Function
#End Region

    End Class
End Namespace