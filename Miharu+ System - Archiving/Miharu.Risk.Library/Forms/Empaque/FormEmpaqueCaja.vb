Imports System.Windows.Forms
Imports DBArchiving
Imports DBCore
Imports Miharu.Desktop.Controls.DesktopMessageBox

Namespace Forms.Empaque
    Public Class FormEmpaqueCaja

#Region " Declaraciones "

        Private _CBarras_Caja As String
        Public Property CBarras_Caja() As String
            Get
                Return _CBarras_Caja
            End Get
            Set(ByVal value As String)
                _CBarras_Caja = value
            End Set
        End Property


#End Region

#Region " Metodos "

        Private Sub cargaConfiguracionesIniciales()

            Dim dmcore As DBCoreDataBaseManager = Nothing
            Try
                dmcore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmcore.Connection_Open(Program.Sesion.Usuario.id)

                Dim Carpetas = dmcore.Schemadbo.CTA_Folder_Cajas.DBFindBycodigo_caja(_CBarras_Caja)

                CarpetasDesktopDataGridView.AutoGenerateColumns = False
                CarpetasDesktopDataGridView.DataSource = Carpetas

                CajaLabel.Text = _CBarras_Caja
                CarpetasLabel.Text = Carpetas.Count.ToString()
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error al abrir la caja: " + ex.Message, "Error abriendo la caja", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Finally
                If dmcore IsNot Nothing Then dmcore.Connection_Close()
            End Try

        End Sub

        Private Sub AgregarFolder()
            Dim AgregarCarpeta As New FormAgregarCarpeta(FormAgregarCarpeta.Tipo.EmpacarCustodia)
            AgregarCarpeta.CBarras_Caja = _CBarras_Caja
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

#End Region

#Region " Eventos "

        Private Sub FormEmpaqueCaja_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            Dim Valido = False
            
            Dim dmcore As DBCoreDataBaseManager = Nothing
            Try
                dmcore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dmcore.Connection_Open(Program.Sesion.Usuario.id)

                Dim CajaDataTable = dmcore.SchemaCustody.TBL_Caja.DBFindByCodigo_Caja(_CBarras_Caja)
                If CajaDataTable.Rows.Count > 0 Then
                    Dim PosicionDataTable = dmcore.SchemaCustody.TBL_Boveda_Posicion.DBFindByfk_Caja(CajaDataTable(0).id_Caja)

                    If PosicionDataTable.Rows.Count > 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("La caja ya se encuentra asignada a una posición en Boveda", "Error Abriendo la caja", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Valido = False
                    Else
                        Valido = True
                        Program.RiskGlobal.ID_CajaCustodia = CShort(CajaDataTable(0).id_Caja)
                        Program.RiskGlobal.CBarras_CajaCustodia = _CBarras_Caja

                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("La caja no existe", "Error Abriendo la caja", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    Valido = False
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Error al abrir la caja: " + ex.Message, "Error Abriendo la caja", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
            Finally
                If dmcore IsNot Nothing Then dmcore.Connection_Close()
            End Try
            
            If _CBarras_Caja <> "" And Valido Then
                cargaConfiguracionesIniciales()
            Else
                Me.Close()
            End If

        End Sub

        Private Sub AgregarCarpetaButton_Click(sender As System.Object, e As System.EventArgs) Handles AgregarCarpetaButton.Click
            AgregarFolder()
        End Sub

        Private Sub CerrarCajaButton_Click(sender As System.Object, e As System.EventArgs) Handles CerrarCajaButton.Click
            CerrarCaja()
        End Sub

#End Region

    End Class
End Namespace