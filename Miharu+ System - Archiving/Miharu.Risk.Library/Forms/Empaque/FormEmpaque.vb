Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBArchiving
Imports Miharu.Desktop.Library
Imports Miharu.Risk.Library.Forms.Reportes.CentroDistribucion

Namespace Forms.Empaque

    Public Class FormEmpaque
        Inherits FormBase

#Region "Declaraciones"
        Private _LineaProceso As Integer
#End Region

#Region "Propiedades"
        Public Property LineaProceso As Integer
            Get
                Return _LineaProceso
            End Get
            Set(value As Integer)
                _LineaProceso = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub FormEmpaque_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            Try
                cargaConfiguracionesIniciales()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Carga Empaque", ex)
            End Try
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AgregarCarpetaButton.Click
            Try
                AgregarFolder()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Agregar Carpeta", ex)
            End Try
        End Sub

        Private Sub CerrarCajaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CerrarCajaButton.Click
            Try
                CerrarCaja()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cerrar Caja", ex)
            End Try
        End Sub

        Private Sub AgregarDocumentoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AgregarDocumentoButton.Click
            Try
                AgregarDocumento()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Agregar Documento", ex)
            End Try
        End Sub

        Private Sub SacarcarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SacarcarpetaButton.Click
            Try
                SacarFolder()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Sacar Documento", ex)
            End Try
        End Sub

        Private Sub RecibirRemisionButton_Click(sender As System.Object, e As System.EventArgs) Handles RecibirRemisionButton.Click
            RecibirRemision()
        End Sub

#End Region

#Region " Metodos "

        Private Sub cargaConfiguracionesIniciales()
            Dim dmcore As New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dmcore.Connection_Open(Program.Sesion.Usuario.id)

            CajaLabel.Text = Program.RiskGlobal.CBarras_CajaCustodia

            Dim Cajas = dmcore.Schemadbo.CTA_Folder_Cajas.DBFindBycodigo_caja(Program.RiskGlobal.CBarras_CajaCustodia)

            'Dim CajasAdicion = dmcore.Schemadbo.CTA_Folder_Cajas_Adicion.DBFindBycodigo_caja(Program.RiskGlobal.CBarras_CajaCustodia)

            CarpetasDesktopDataGridView.AutoGenerateColumns = False
            CarpetasDesktopDataGridView.DataSource = Cajas

            CarpetasLabel.Text = Cajas.Count.ToString()

            dmcore.Connection_Close()
        End Sub

        Private Sub AgregarFolder()
            Dim AgregarCarpeta As New FormAgregarCarpeta(FormAgregarCarpeta.Tipo.Empacar)
            AgregarCarpeta.ShowDialog()

            cargaConfiguracionesIniciales()
        End Sub

        Private Sub SacarFolder()
            Dim AgregarCarpeta As New FormAgregarCarpeta(FormAgregarCarpeta.Tipo.Sacar)
            AgregarCarpeta.ShowDialog()

            cargaConfiguracionesIniciales()
        End Sub

        Private Sub CerrarCaja()
            If CarpetasDesktopDataGridView.RowCount <> 0 Then
                If DesktopMessageBoxControl.DesktopMessageShow("Esta seguro de cerrar la caja?", "Cerrar Caja", DesktopMessageBoxControl.IconEnum.WarningIcon, False, True) = DialogResult.OK Then
                    Dim dbmArchiving As New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
                    dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                    'Valida que el centro de procesamiento del proyecto sea igual al centro de procesamiento del equipo
                    Dim SedeCustodia As Short = -1
                    If Not Program.RiskGlobal.SedeCustodia.IsNull Then SedeCustodia = CShort(Program.RiskGlobal.SedeCustodia)

                    Dim Estado = DBCore.EstadoEnum.Centro_Distribucion
                    If CShort(Program.RiskGlobal.EntidadCustodia) = Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad And SedeCustodia = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede Then Estado = DBCore.EstadoEnum.Por_Custodiar

                    Try
                        dbmArchiving.Schemadbo.PA_Cierra_Caja.DBExecute(Program.RiskGlobal.ID_CajaCustodia, Estado, Program.Sesion.Usuario.id, Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, CInt(Program.RiskGlobal.EntidadCustodia), Program.RiskGlobal.SedeCustodia, Program.RiskGlobal.BovedaCustodia, Program.RiskGlobal.Usa_Empaque_Adicion)
                        DesktopMessageBoxControl.DesktopMessageShow("Caja cerrada con exito", "Caja OK", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        Me.Close()
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("CerrarCaja", ex)
                    End Try

                    dbmArchiving.Connection_Close()
                End If
            Else
                DesktopMessageBoxControl.DesktopMessageShow("No puede cerrar la caja sin ninguna carpeta", "Error cerrando la caja", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            End If
        End Sub

        Private Sub AgregarDocumento()
            Dim AgregarFile As New FormAgregarDocumento
            AgregarFile.ShowDialog()
            cargaConfiguracionesIniciales()
        End Sub

        Private Sub RecibirRemision()

            Dim FormSeleccionarRemision As New Forms.CentroDistribucion.FormSeleccionarRemision
            If FormSeleccionarRemision.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim RegistroRemision As New CentroDistribucion.FormRegistrarRemision
                RegistroRemision.Remision = FormSeleccionarRemision.Remision
                RegistroRemision.LineaProceso = FormSeleccionarRemision.LineaProceso
                RegistroRemision.ModoRegistroORecepcionRemision = False
                RegistroRemision.Caja = Program.RiskGlobal.ID_CajaCustodia
                RegistroRemision.ShowDialog()
                cargaConfiguracionesIniciales()

            End If

        End Sub

#End Region

    End Class

End Namespace