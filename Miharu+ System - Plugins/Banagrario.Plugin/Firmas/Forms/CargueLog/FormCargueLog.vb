Imports System.IO
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Firmas.Forms.CargueLog

    Public Class FormCargueLog

#Region " Declaraciones "
        Private _Plugin As FirmasImagingPlugin
        Public Shared Fecha_Proceso As Integer
#End Region

#Region " Propiedades "

        Property SelectedFile As String
            Get
                Return RutaTextBox.Text.TrimEnd("\"c)
            End Get
            Set(ByVal value As String)
                RutaTextBox.Text = value
            End Set
        End Property
#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioImaginPlugin As FirmasImagingPlugin)
            InitializeComponent()
            _Plugin = nBanagrarioImaginPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub CargueGetInfo_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click
            If (Validar()) Then
                If CargarLog(Me.RutaTextBox.Text) Then
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                End If
            Else
                Me.DialogResult = DialogResult.None
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
        End Sub

        Private Sub SelectFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SelectFolderButton.Click
            SelectLogFile()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadConfig()

            Dim NewDate = Now
            If (NewDate.Hour < _Plugin.HoraCambioFechaProceso) Then
                NewDate = NewDate.AddDays(-1)
            End If
            FechaProcesoPicker.Value = NewDate
            RutaTextBox.Text = "D:\Logs"
        End Sub

        Private Sub SelectLogFile()
            Dim oFD As New OpenFileDialog
            With oFD
                .Title = "Seleccionar fichero"
                .Filter = "txt files (*.lis)|*.lis|All files (*.*)|*.*"
                .FileName = Me.RutaTextBox.Text
                If .ShowDialog = DialogResult.OK Then
                    Me.RutaTextBox.Text = .FileName
                End If
            End With
        End Sub

#End Region

#Region " Funciones "

        Private Function CargarLog(ByVal nFileName As String) As Boolean
            If Not File.Exists(nFileName) Then
                MessageBox.Show("Por favor seleccionar un archivo válido", _
                                "Cargar Log", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Dim Errores As New List(Of String)
            Dim idCargue As Integer
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                DBAgrario.DBAgrarioDataBaseManager.IdentifierDateFormat = _Plugin.Manager.DesktopGlobal.IdentifierDateFormat

                ' Validar fecha de proceso
                Dim fechaValida = dbmAgrario.SchemaProcess.PA_getFecha_Habil.DBExecute(FechaProcesoPicker.Value)

                If (Not fechaValida) Then
                    MessageBox.Show("Debe seleccionar una fecha de proceso válida", "Log", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                ' Validar si el log ya fué cargado
                Dim fileName = Path.GetFileName(Trim(RutaTextBox.Text))

                Dim logTable = dbmAgrario.SchemaFirmas.TBL_Cargue.DBFindByNombre_ArchivoArchivo_Valido(fileName, True)

                If (logTable.Count > 0) Then
                    MessageBox.Show("El lóg que ya fue cargado en la fecha: " & logTable(0).Fecha_Cargue.ToString(), "Log", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                ' Crear log
                dbmAgrario.Transaction_Begin()

                idCargue = dbmAgrario.SchemaFirmas.TBL_Cargue.DBNextId()

                Dim cargueLogType = New DBAgrario.SchemaFirmas.TBL_CargueType() With {
                        .id_Cargue = idCargue,
                        .Nombre_Archivo = fileName,
                        .Fecha_Cargue = SlygNullable.SysDate,
                        .Fecha_Proceso = Fecha_Proceso.ToString(),
                        .fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id,
                        .Fecha_inicio = SlygNullable.SysDate,
                        .Archivo_Valido = False
                        }

                dbmAgrario.SchemaFirmas.TBL_Cargue.DBInsert(cargueLogType)

                dbmAgrario.Transaction_Commit()

                ' Iniciar cargue masivo
                dbmAgrario.DataBase.BeginBulkQuery()

                Dim fila As Integer = 0
                Dim lector = New Slyg.Tools.CSV.CSVData()
                lector.LinesToJump = 2
                lector.LoadCSV(nFileName, False, "|"c)

                For Each item As Slyg.Tools.CSV.CSVRow In lector.DataTable.Rows
                    fila += 1
                    Dim detalleType = GetDetalleType(Errores, item, idCargue, fila)
                    If (detalleType IsNot Nothing) Then
                        detalleType.fk_Cargue = idCargue
                        dbmAgrario.SchemaFirmas.TBL_Cargue_Detalle_Temporal.DBInsert(detalleType)
                    End If
                Next

                dbmAgrario.Transaction_Begin()

                While dbmAgrario.DataBase.BulkQueryLines > 0
                    dbmAgrario.DataBase.SendBulkQuery(1000)
                End While

                dbmAgrario.DataBase.EndBulkQuery()

                dbmAgrario.SchemaFirmas.PA_Finaliza_Cargue_Log.DBExecute(idCargue)

                cargueLogType = New DBAgrario.SchemaFirmas.TBL_CargueType() With {
                                    .Archivo_Valido = True,
                                    .Fecha_Fin = SlygNullable.SysDate
                                }
                dbmAgrario.SchemaFirmas.TBL_Cargue.DBUpdate(cargueLogType, idCargue)

                dbmAgrario.Transaction_Commit()

                DesktopMessageBoxControl.DesktopMessageShow("Archivo Log cargado correctamente, se insertaron " & fila.ToString() & " registros.", "Cargue Archivo Log", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Return True
            Catch ex As Exception
                If dbmAgrario IsNot Nothing Then dbmAgrario.Transaction_Rollback()

                Try
                    Dim cargueLogType = New DBAgrario.SchemaFirmas.TBL_CargueType() With {
                        .Fecha_Fin = SlygNullable.SysDate,
                        .Descripcion_Error = ex.Message
                    }

                    dbmAgrario.SchemaFirmas.TBL_Cargue.DBUpdate(cargueLogType, idCargue)
                Catch exsub As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Validar Cargue Archivo Log", exsub)
                End Try

                DesktopMessageBoxControl.DesktopMessageShow("Validar Cargue Archivo Log", ex)

                Return False
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try
        End Function

        Private Function Validar() As Boolean

            Fecha_Proceso = Convert.ToInt32(FechaProcesoPicker.Value.ToString("yyyyMMdd"))
            If (RutaTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
            ElseIf (Not File.Exists(Me.SelectedFile)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
            ElseIf (ArchivoCargado()) Then
                DesktopMessageBoxControl.DesktopMessageShow("Archivo Cargado previamente para la fecha de proceso " & Fecha_Proceso.ToString(), "Archivo Cargado", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            Else
                Return True
            End If
            Return False
        End Function

        'Private Function FechaInvalida() As Boolean
        '    Dim horas As TimeSpan = Date.Now.TimeOfDay
        '    Dim hreglamentaria As TimeSpan = TimeSpan.Parse(_Plugin.HoraCambioFechaProceso & ":00:00")
        '    Dim fechahoy As DateTime = CDate(Date.Now.ToShortDateString)
        '    Dim fechaselec As DateTime = CDate(FechaProcesoPicker.Value.ToShortDateString)
        '    If fechaselec = fechahoy Then
        '        Return False
        '    Else
        '        If fechaselec = fechahoy.AddDays(-1) Then
        '            If horas <= hreglamentaria Then
        '                Return False
        '            Else
        '                Return True
        '            End If
        '        Else
        '            If fechaselec > fechahoy Then
        '                Return False
        '            End If
        '        End If
        '        Return True
        '    End If
        'End Function

        Private Function ArchivoCargado() As Boolean
            Dim dbmAgrario As DBAgrario.DBAgrarioDataBaseManager = Nothing
            Try
                dbmAgrario = New DBAgrario.DBAgrarioDataBaseManager(_Plugin.BancoAgrarioConnectionString)
                dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                Dim CargueDataTable = dbmAgrario.SchemaFirmas.TBL_Cargue.DBFindByFecha_Proceso(Fecha_Proceso.ToString())
                If CargueDataTable.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Validar Cargue Archivo Log", ex)
                Return False
            Finally
                If dbmAgrario IsNot Nothing Then dbmAgrario.Connection_Close()
            End Try
        End Function

        Private Function GetDetalleType(ByVal nErrores As List(Of String), ByVal nLinea As Slyg.Tools.CSV.CSVRow, ByVal nProceso As Integer, ByVal nFila As Integer) As DBAgrario.SchemaFirmas.TBL_Cargue_Detalle_TemporalType
            Dim result As New DBAgrario.SchemaFirmas.TBL_Cargue_Detalle_TemporalType()

            Try
                result.fk_Cargue = nProceso
                result.id_Cargue_Detalle_Temporal = nFila
                result.Fecha_Movimiento = Trim(nLinea.Item(0).ToString()) 'Trim(Mid(nLinea, 1, 8))
                result.fk_Proceso_Detalle = DBNull.Value
                result.Codigo_Oficina = Trim(nLinea.Item(1).ToString())
                result.Nombre_Oficina = Trim(nLinea.Item(2).ToString())
                result.Codigo_Transaccion = Trim(nLinea.Item(3).ToString())
                result.Nombre_Transaccion = Trim(nLinea.Item(4).ToString())
                result.Producto = Trim(nLinea.Item(5).ToString())
                result.Clase_Cuenta = Trim(nLinea.Item(6).ToString())
                result.Numero_Cuenta = Trim(nLinea.Item(7).ToString())
                result.Ente = Trim(nLinea.Item(8).ToString())
                result.Clase_Ente = Trim(nLinea.Item(9).ToString())
                result.Tipo_Persona = Trim(nLinea.Item(10).ToString())
                result.Numero_Hojas_Tarjeta = Trim(nLinea.Item(11).ToString())
                result.Digito_Verificacion = Trim(nLinea.Item(12).ToString())
                result.Usuario = Trim(nLinea.Item(13).ToString())
                result.Excluido = DBNull.Value
                result.Cruzado = DBNull.Value
                result.fk_Expediente = DBNull.Value
                result.fk_Folder = DBNull.Value
                result.fk_File = DBNull.Value
                result.fk_Version = DBNull.Value
                result.fk_Recorte = DBNull.Value
                result.Numero_Cuenta_Back = DBNull.Value

            Catch ex As Exception
                nErrores.Add("Error a procesar la línea: " & nFila & ". " & ex.Message)
                Return Nothing
            End Try

            Return result
        End Function

#End Region

    End Class

End Namespace