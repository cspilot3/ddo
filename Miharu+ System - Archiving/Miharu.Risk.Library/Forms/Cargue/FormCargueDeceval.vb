Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Configuration
Imports DBArchiving

Namespace Forms.Cargue
    Public Class FormCargueDeceval
        Inherits FormBase


#Region " Declaraciones "

        Private _File As Stream = Nothing
        Private _DataFile As DataTable = Nothing
        Private _DataRegistros As Integer = 0
        Private _DataColumnas As Integer = 0
        Private _EstadoProceso As Short = 0 '0 Validando, 1 Procesando.
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private objXLS As New XLSData
        Private trResultado As DesktopConfig.TypeResult
        Private _TipoCargue As DesktopConfig.TipoCargue
        Private Segundos As Integer = 0
        Private Minuto As Integer = 0
        Private Hora As Integer = 0

#End Region

#Region " Constructor "
        Private _appSettings As String

        Sub New(ByVal TipoCargue As DesktopConfig.TipoCargue)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _TipoCargue = TipoCargue
        End Sub

#End Region

#Region " Metodos "

        Private Property AppSettings(p1 As String) As String
            Get
                Return _appSettings
            End Get
            Set(value As String)
                _appSettings = value
            End Set
        End Property

        Private Sub BuscarArchivo()
            Segundos = 0
            Minuto = 0
            Hora = 0

            Try
                Dim Respuesta = ArchivoOpenFileDialog.ShowDialog()

                If Respuesta = DialogResult.OK Then
                    Try
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName

                        'Si el archivo es txt o csv, habilita la opción de manejar separador
                        If ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".txt") OrElse ArchivoOpenFileDialog.FileName.ToLower().EndsWith(".csv") Then
                            OpcionesSeparadorGroupBox.Enabled = True
                        Else
                            OpcionesSeparadorGroupBox.Enabled = False
                        End If

                        _File = ArchivoOpenFileDialog.OpenFile()
                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
                    Finally
                        If _File IsNot Nothing Then
                            _File.Close()
                        End If
                    End Try

                ElseIf Respuesta = DialogResult.Cancel Then
                    OpcionesSeparadorGroupBox.Enabled = False
                    ArchivoDesktopTextBox.Text = ""
                    _File = Nothing
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ex)
            End Try
        End Sub

        Private Sub HabilitarControles(ByVal valor As Boolean)
            ArchivoDesktopTextBox.Enabled = valor
            BuscarArchivoButton.Enabled = valor
            CargarButton.Enabled = valor
        End Sub
      

#End Region

        Private Sub CargarButton_Click(sender As System.Object, e As System.EventArgs) Handles CargarButton.Click
            Timer1.Enabled = True

            Me.CargueBackgroundWorker.WorkerSupportsCancellation = True

            Dim dbmArchiving = New DBArchivingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Archiving)
            Try
                dbmArchiving.Connection_Open(Program.Sesion.Usuario.id)

                If Not IsNothing(_File) Then
                    'Valida si el nombre del archivo ya fué cargado.
                    Dim NombreArchivo As String = Path.GetFileName(CType(_File, FileStream).Name)

                    'Dim dtCargues = dbmArchiving.SchemaRisk.TBL_Cargue.DBFindByArchivo_Cargue(NombreArchivo)
                    Dim dtCargues = dbmArchiving.SchemaRisk.TBL_Cargue.DBFindByfk_Entidadfk_ProyectoArchivo_Cargue(Program.RiskGlobal.Entidad, Program.RiskGlobal.Proyecto, NombreArchivo)
                    If dtCargues.Count > 0 Then
                        DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        _File = Nothing
                        ArchivoDesktopTextBox.Text = ""
                    Else
                        HabilitarControles(False)
                        Me.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo & "-" & Program.RiskGlobal.Entidad & "-" & Program.RiskGlobal.Proyecto)
                    End If
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un archivo de cargue.", "Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace

