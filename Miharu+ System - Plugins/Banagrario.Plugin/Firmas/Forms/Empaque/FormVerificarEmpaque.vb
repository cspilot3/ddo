Imports System.Drawing
Imports System.Windows.Forms

Namespace Firmas.Forms.Empaque

    Public Class FormVerificarEmpaque

#Region " Declaraciones "
        Private _Plugin As FirmasImagingPlugin
        'Private _RegionalDataTable As TBL_RegionalDataTable
        'Private _COBDataTable As TBL_COBDataTable
        'Private _OficinaDataTable As TBL_OficinaDataTable
        'Private Oficina As String = "0"
        Private FechaProceso As String = ""
#End Region

#Region " Propiedades "

        'Property SelectedFile As String
        '    Get
        '        Return RutaTextBox.Text.TrimEnd("\"c)
        '    End Get
        '    Set(ByVal value As String)
        '        RutaTextBox.Text = value
        '    End Set
        'End Property
#End Region

#Region " Constructores "
        Public Sub New(ByVal nBanagrarioImaginPlugin As FirmasImagingPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioImaginPlugin
            'CargaTablas()
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormVerificarEmpaque_Load(sender As Object, e As EventArgs) Handles Me.Load
            LbMensaje.Text = ""
            LbMensaje.ForeColor = Color.Black
        End Sub

        Private Sub TxtCodBar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCodBar.KeyPress
            If e.KeyChar = Convert.ToChar(Keys.Enter) Then
                VerificarCodigodeBarras()
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub VerificarCodigodeBarras()
            FechaProceso = FechaProcesoPicker.Value.ToString("yyyyMMdd")

            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim OtDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Integer.Parse(FechaProceso), Nothing, Nothing)

                If (OtDataTable.Rows.Count > 0) Then
                    Dim nidOT = OtDataTable(0).id_OT
                    Dim cruces = dbmAgrario.SchemaFirmas.PA_Existe_Cruce.DBExecute(nidOT)

                    If (cruces = 0) Then Throw New Exception("No se puede realizar la validacion de tarjetas si no se ha realizado el proceso de cruce")
                Else
                    Throw New Exception("No se puede realizar la validacion de tarjetas si no se ha realizado el proceso de cruce")
                End If

                'Dim CruceDataTable = dbmAgrario.SchemaFirmas.CTA_Verifica_Empaque.DBFindByFecha_ProcesoValor_File_Data(Integer.Parse(FechaProceso), Trim(TxtCodBar.Text))
                Dim CruceDataTable = dbmAgrario.SchemaFirmas.PA_Validar_Empaque.DBExecute(Integer.Parse(FechaProceso), Trim(TxtCodBar.Text))

                LbCodBar.Text = Trim(TxtCodBar.Text)
                'If (CruceDataTable.Rows.Count > 0) Then                    
                '    If (CruceDataTable(0).Cruzado AndAlso Not CruceDataTable(0).Rechazada) Then
                '        LbMensaje.Text = "TARJETA VÁLIDA"
                '        LbMensaje.ForeColor = Color.Blue
                '        LbCodBar.ForeColor = Color.Blue
                '    ElseIf (Not CruceDataTable(0).Cruzado AndAlso Not CruceDataTable(0).Rechazada) Then
                '        LbMensaje.Text = "TARJETA SOBRANTE"
                '        LbMensaje.BackColor = Color.SkyBlue
                '        LbMensaje.ForeColor = Color.White
                '        LbCodBar.BackColor = Color.SkyBlue
                '        LbCodBar.ForeColor = Color.White
                '    Else
                '        LbMensaje.Text = "TARJETA RECHAZADA"
                '        LbMensaje.ForeColor = Color.Red
                '        LbCodBar.ForeColor = Color.Red
                '    End If
                'Else
                '    Dim CruceCompletoDataTable = dbmAgrario.SchemaFirmas.CTA_Verifica_Empaque.DBFindByFecha_ProcesoValor_File_Data(Nothing, Trim(TxtCodBar.Text))
                '    If CruceCompletoDataTable.Rows.Count > 0 Then
                '        LbMensaje.Text = "ESTA TARJETA NO PERTENECE A ESTA FECHA DE PROCESO"
                '        LbMensaje.ForeColor = Color.Black
                '        LbCodBar.ForeColor = Color.Black
                '    Else
                '        LbMensaje.Text = "TARJETA RECHAZADA"
                '        LbMensaje.ForeColor = Color.Red
                '        LbCodBar.ForeColor = Color.Red
                '    End If
                'End If

                If (CruceDataTable.Rows.Count > 0) Then
                    If CruceDataTable(0).Oficina = 1 Then
                        LbMensaje.Text = "TARJETA PARA DIRECCIÓN GENERAL"
                        LbMensaje.ForeColor = Color.Blue
                        LbCodBar.ForeColor = Color.Blue
                    ElseIf CruceDataTable(0).Oficina = 2 Then
                        LbMensaje.Text = "TARJETA PARA DEVOLVER A LA OFICINA"
                        LbMensaje.ForeColor = Color.Red
                        LbCodBar.ForeColor = Color.Red
                    End If
                Else
                    LbMensaje.Text = "FAVOR VERIFICAR TOKEN INGRESADO VS FECHA DE PROCESO"
                    LbMensaje.ForeColor = Color.Black
                    LbCodBar.ForeColor = Color.Black
                End If

                TxtCodBar.Text = ""
                TxtCodBar.Focus()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Firmas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
                If dbmImaging IsNot Nothing Then dbmImaging.Connection_Close()
            End Try

        End Sub

#End Region

#Region " Funciones "


#End Region

    End Class
End Namespace