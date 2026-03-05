Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Library.Eventos

Public Class FormRotuloCaja
#Region " Propiedades "
    Public Property OT As Integer = Nothing
    Public Property EventManager As EventManager

    Public Property IdOT() As Integer

    Public Property IdEmpaque() As Short

    Public Property IdEmpaqueContenedor() As Short

#End Region
 
    Private Function Validar() As Boolean
        Dim Valido As Boolean
        If OTDesktopTextBox.Text = "" Or OTDesktopTextBox.Text = Nothing Then
            MessageBox.Show("Debe ingresar una OT", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim OTS = dbmImaging.SchemaProcess.PA_Rotulo_Caja.DBExecute(OTDesktopTextBox.Text, 0, "OT", Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, "", "")
                For Each row As DataRow In OTS.Rows
                    OT = CInt(row("fk_OT"))
                Next
                Dim DataOt = dbmImaging.SchemaProcess.TBL_OT.DBGet(OT)
                If DataOt.Rows.Count > 0 Then
                    If DataOt(0).Cerrado = True Then
                        Valido = True
                    Else
                        Valido = False
                        DesktopMessageBoxControl.DesktopMessageShow("La OT no ha sido totalmente procesada.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Else
                    Valido = False
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontro la OT a procesar.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End If
        Return Valido
    End Function

    Private Function ValidarOTS() As Boolean
        Dim Valido As Boolean
        If OT = Nothing Or OT = 0 Then
            MessageBox.Show("Debe Seleccionar una OT", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                Dim DataOt = dbmImaging.SchemaProcess.TBL_OT.DBGet(OT)
                If DataOt.Rows.Count > 0 Then
                    If DataOt(0).Cerrado = True Then
                        Dim RotuloCarpetas = RotuloCarpetaCheckBox.Checked
                        Dim Caja = OTDesktopTextBox.Text
                        Dim objDestinatario As New FormVisorRotuloCaja(Caja, OT, RotuloCarpetas, 0, "")
                        objDestinatario.ShowDialog()
                        If RotuloCarpetas Then
                            Dim objrotuloCarpeta As New FormVisorRotuloCarpeta(Caja, OT, RotuloCarpetas, 0, "")
                            objrotuloCarpeta.ShowDialog()
                        End If
                    Else
                        Valido = False
                        DesktopMessageBoxControl.DesktopMessageShow("La OT no ha sido totalmente procesada.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Else
                    Valido = False
                    DesktopMessageBoxControl.DesktopMessageShow("No se encontro la OT a procesar.", "Rotulo Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End If
        Return Valido
    End Function

    Private Sub BuscarButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarButton.Click
        If OTDesktopTextBox.Text = "" Or OTDesktopTextBox.Text = Nothing Then
            MessageBox.Show("Debe ingresa una cedula", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If (Validar()) Then
                Dim RotuloCarpetas = RotuloCarpetaCheckBox.Checked
                Dim Caja = OTDesktopTextBox.Text
                Dim objDestinatario As New FormVisorRotuloCaja(Caja, OT, RotuloCarpetas, 0, "")
                objDestinatario.ShowDialog()
                If RotuloCarpetas Then
                    Dim objrotuloCarpeta As New FormVisorRotuloCarpeta(Caja, OT, RotuloCarpetas, 0, "")
                    objrotuloCarpeta.ShowDialog()
                End If
            End If
        End If
    End Sub
End Class