Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms

Namespace Imaging.Carpeta_Unica.Forms.Empaque


    Public Class FormCierreCaja


#Region " Declaraciones "

        Private _Plugin As CarpetaUnicaPlugin
        Private _CajasCerrar As DataTable


#End Region

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()
            _Plugin = nCarpetaUnicaDesktopPlugin
        End Sub

#End Region

#Region "Eventos"
        Private Sub FormCierreCaja_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Me.dgvCajas.AutoGenerateColumns = True
        End Sub

        Private Sub btnBuscarCaja_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscarCaja.Click
            BuscarCaja()
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
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                Try
                    dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)

                    For Each itemRow As DataRow In Me._CajasCerrar.Rows
                        Cerrada = CBool(itemRow("CERRADA").ToString())

                        If Not (Cerrada) Then
                            Caja = CType(itemRow("CAJA").ToString(), Long)
                            TipoProceso = CInt(itemRow("TIPO_PROCESO").ToString())

                            Dim RowUpdate As DBIntegration.SchemaBCSCarpetaUnica.TBL_CajaType = New DBIntegration.SchemaBCSCarpetaUnica.TBL_CajaType

                            RowUpdate.Cerrada = True
                            dbmIntegration.SchemaBCSCarpetaUnica.TBL_Caja.DBUpdate(RowUpdate, Caja)
                            DesktopMessageBoxControl.DesktopMessageShow("Caja Numero #" + Caja.ToString() + " cerrada exitosamente!!", "Caja Cerrada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

                            BuscarCaja()
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("Caja Numero #" + Me.txtCaja.Text.ToString() + " ya se encuentra cerrada, favor verificar.", "Caja Cerrada", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        End If
                    Next

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Error En btnCerrar_Click()", ex)
                Finally
                    dbmIntegration.Connection_Close()
                End Try

            End If
        End Sub

#End Region

        Private Sub BuscarCaja()
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            If (Me.txtCaja.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Error en busqueda, parametro Caja no debe estar Vacio!!!", "Caja Nula", DesktopMessageBoxControl.IconEnum.ErrorIcon, True)

                If Me.dgvCajas.Rows.Count > 0 Then
                    Me.dgvCajas.DataSource = Nothing
                    Me._CajasCerrar.Clear()
                End If

                Me.txtCaja.Focus()
                Me.Cursor = Cursors.Default
            Else
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                    dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)

                    Dim Resultado = dbmIntegration.SchemaBCSCarpetaUnica.PA_Trae_Caja_Cerrar.DBExecute(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, Me.txtCaja.Text.Trim())

                    If Resultado.ToString() = "OK" Then
                        Dim CajaEncontrada = dbmIntegration.SchemaBCSCarpetaUnica.CTA_Caja.DBFindByCAJA(Me.txtCaja.Text)

                        Me._CajasCerrar = CajaEncontrada
                        Me.dgvCajas.DataSource = CajaEncontrada
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow(Resultado.ToString(), "Cerrar Caja", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Me.dgvCajas.DataSource = Nothing
                        If Me._CajasCerrar IsNot Nothing Then
                            Me._CajasCerrar.Clear()
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


       
    End Class
End Namespace

