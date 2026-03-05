Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms


Namespace Procesos.Empaque


    Public Class FormCierreCaja

        

#Region " Declaraciones "

        Private _CajasCerrar As DataTable

#End Region

#Region "Eventos"
        Private Sub FormCierreCaja_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Me.dgvCajas.AutoGenerateColumns = True
        End Sub

        Private Sub btnBuscarCaja_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscarCaja.Click
            Buscar()
        End Sub

        Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
            Me.Close()
        End Sub

        Private Sub btnCerrar_Click(sender As System.Object, e As System.EventArgs) Handles btnCerrar.Click
            If Me._CajasCerrar Is Nothing Then
                DesktopMessageBoxControl.DesktopMessageShow("Error En Cierre de Caja, No hay caja para procesar!!!", "Error Cierre", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)
            Else
                Dim TipoProceso As Integer
                Dim Cerrada As Boolean
                Dim Caja As String
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                Try
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    For Each itemRow As DataRow In Me._CajasCerrar.Rows
                        Cerrada = CBool(itemRow("CERRADA").ToString())

                        If Not (Cerrada) Then
                            'Caja = CType(itemRow("CAJA").ToString(), Long)
                            'TipoProceso = CInt(itemRow("TIPO_PROCESO").ToString())

                            'Dim RowUpdate As DBIntegration.SchemaBCSCarpetaUnica.TBL_CajaType = New DBIntegration.SchemaBCSCarpetaUnica.TBL_CajaType

                            'RowUpdate.Cerrada = True
                            'dbmImaging.SchemaBCSCarpetaUnica.TBL_Caja.DBUpdate(RowUpdate, Caja)
                            'DesktopMessageBoxControl.DesktopMessageShow("Caja Numero #" + Caja.ToString() + " cerrada exitosamente!!", "Caja Cerrada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                            Buscar()
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("Caja Numero #" + Me.txtCaja.Text.ToString() + " ya se encuentra cerrada, favor verificar.", "Caja Cerrada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        End If
                    Next

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Error En btnCerrar_Click()", ex)
                Finally
                    dbmImaging.Connection_Close()
                End Try

            End If
        End Sub

#End Region

        Private Sub Buscar()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.ToString(), "Error buscando caja(s)", DesktopMessageBoxControl.IconEnum.ErrorIcon, True, False)
            Finally
                If (dbmImaging IsNot Nothing) Then
                    dbmImaging.Connection_Close()
                End If
            End Try
        End Sub
    End Class
End Namespace

