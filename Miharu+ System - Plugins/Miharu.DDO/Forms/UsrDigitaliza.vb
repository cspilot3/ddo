Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.IO
Imports System.Runtime.ConstrainedExecution
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox
Imports DocumentFormat.OpenXml.ExtendedProperties
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Ghostscript.NET
Imports Ghostscript.NET.Processor
Imports Ghostscript.NET.Rasterizer
Imports Ionic.Zip
Imports Miharu.DDO.Mod_Digitaliza
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileSending.Library.Clases
Imports Miharu.Security.Library.Session
Imports Slyg.Tools.Zip
Imports Vintasoft
Imports Vintasoft.Twain
Imports Application = System.Windows.Forms.Application
Imports CheckBox = System.Windows.Forms.CheckBox
Imports Control = System.Windows.Forms.Control
Imports System.Reflection

Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Utils
Imports Excel = Microsoft.Office.Interop.Excel

Namespace Forms

    Public Class UsrDigitaliza

#Region " Declaraciones "

        Dim Newlote As Boolean
        Dim RutaLoteNuevo As String
        Dim RutaLotes As String
        Dim NombreLote As String
        Dim Imagenes(0) As ImagenEscaneada
        Dim TImagen As Integer
        Dim UltimoControl As Control
        Dim ExportandoLote As String
        Private _deviceManager As Vintasoft.Twain.DeviceManager
        Dim MsgError As String
        Dim Transfer As FileSendingClient
        ReadOnly MiharuSession As Sesion
        Dim ZipFileName As String
        Dim Identificador As String

        Dim LastPath As String = ""
        Dim Contenedor As String = ""
        Dim FileNamesSource As String()

        Private Delegate Sub TransferBeginDelegate(sender As Object, Identificador As String)
        Private Delegate Sub TransferProcessDelegate(sender As Object, Identificador As String, Avance As Single)
        Private Delegate Sub TransferCompletedDelegate(sender As Object, Identificador As String)
        Private Delegate Sub TransferErrorDelegate(sender As Object, Identificador As String, Mensaje As String)
        Dim CDig As New Cls_Digitaliza
        Private lastTabPage As TabPage
        Dim SetExt As HashSet(Of String)
        Dim FiltroExt As String

#End Region

#Region " Constructores "

        Public Sub New()
            InitializeComponent()
        End Sub

#End Region

#Region " Eventos "

        Private Sub UsrDigitaliza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            InicializateControls()
            Dim parentTabControl As TabControl = FindParentTabControl(Me)
            If parentTabControl IsNot Nothing Then
                AddHandler parentTabControl.Deselecting, AddressOf TabControl_Deselecting
                lastTabPage = parentTabControl.SelectedTab ' Inicializar con la pestaña actual
            End If
        End Sub

        Private Sub UsrDigitaliza_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Asc("-")) Then
                If TypeOf (Me.ActiveControl) Is TextBox Or UCase(TypeName(Me.ActiveControl)) = "TEXTBOXMASK" Then
                    Me.ActiveControl.Text = ""
                    e.Handled = True
                End If
            End If

        End Sub

#End Region

#Region " Metodos "

        Private Sub TabControl_Deselecting(sender As Object, e As TabControlCancelEventArgs)
            If Not ShouldAllowTabChange(sender, CType(sender, TabControl).SelectedTab) Then
                e.Cancel = True
            Else
                InicializateControls()
            End If
        End Sub

        Private Sub InicializateControls()
            If Not Me.DesignMode Then

                Vintasoft.Twain.TwainGlobalSettings.Register(UsuarioReg, CorreoReg, CodigoReg)

                Dim CodigCentro As String = Program.DesktopGlobal.CentroProcesamientoRow.Codigo_Centro
                SucursalIngreso = Val(CodigCentro).ToString("0000")
                LblNomOfic.Text = SucursalIngreso & " " & Trim(Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento)
                RutaImagSCan = Program.DesktopGlobal.PuestoTrabajoRow.Ruta_Local
                Newlote = False
                RutaLotes = RutaImagSCan & "\" & Now.ToString("yyyyMMdd")

                CargarEsquemas() ' Series Documentales

                'Buscar el tiempo de inactividad.
                TiempoInactividad = CDig.ConsultaParametroSistema(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.ImagingGlobal.Proyecto, "TiempoInactividadLoteaTransmitir")

                Reintentos = CDig.ConsultaParametroSistema(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.ImagingGlobal.Proyecto, "ReintentosparaTransmitirLote")

                Exportando = False
                Des_HabilitarBotones(0)
                Me.BtnExportarLotes.Enabled = False
                CargarLotesExistentes()
                CargarToolTip()
                TimerExportar.Enabled = True

                ' Liberar imagen de PicImagen de forma segura
                If PicImagen.Image IsNot Nothing Then
                    Dim tempImage As Image = PicImagen.Image
                    PicImagen.Image = Nothing
                    If tempImage IsNot Nothing Then
                        tempImage.Dispose()
                    End If
                End If

                If PicA IsNot Nothing Then
                    Dim tempImage As Image = PicA.Image
                    PicA.Image = Nothing
                    If tempImage IsNot Nothing Then
                        tempImage.Dispose()
                    End If
                End If

                If DgvRegistrosLote IsNot Nothing Then
                    If DgvRegistrosLote.DataSource IsNot Nothing Then
                        DgvRegistrosLote.DataSource = Nothing
                    Else
                        DgvRegistrosLote.Rows.Clear()
                    End If
                    DgvRegistrosLote.Refresh()
                End If

                If FLPImagenes IsNot Nothing Then
                    FLPImagenes.Controls.Clear()
                End If
            End If
        End Sub

#End Region

#Region " Funciones "

        Private Function FindParentTabControl(ctrl As Control) As TabControl
            Dim parent = ctrl.Parent
            While parent IsNot Nothing
                If TypeOf parent Is TabControl Then
                    Return CType(parent, TabControl)
                End If
                parent = parent.Parent
            End While
            Return Nothing
        End Function
        Private Function ShouldAllowTabChange(sender As Object, nextTab As TabPage) As Boolean
            Dim tabControl As TabControl = CType(sender, TabControl)
            Dim currentTab As TabPage = tabControl.SelectedTab

            If currentTab.Controls.Contains(Me) Then

                Dim respuesta As DialogResult = MessageBox.Show(
                                "Está a punto de salir de esta página." & Environment.NewLine &
                                "Si tiene información pendiente, por favor guárdela antes de continuar." & Environment.NewLine &
                                "¿Desea salir de esta página?",
                                "Confirmación de salida",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                If respuesta = DialogResult.No Then
                    Return False
                Else
                End If
            End If

            Return True
        End Function

#End Region


#Region "Botones"
        'BOTONES PRINCIPALES

        Private Sub CrearCarpetaTipologia()

            If CrearLote() = False Then
                Exit Sub
            End If
            RutaLoteNuevo = RutaLotes & "\" & Me.LblNumLote.Text
            CargarInformaciondelote(Me.LblNumLote.Text)
            CargarImagenesEnMiniatura(RutaLoteNuevo)
            Des_HabilitarBotones(1)
            Des_habilitarCamposCaptura(0)
            Me.BtnEscanear.Focus()

        End Sub

        Private Sub BtnNewCarpeta_Click(sender As Object, e As EventArgs) Handles BtnNewCarpeta.Click
            Newlote = True
            Des_HabilitarBotones(0)
            Me.BtnExportarLotes.Enabled = False
            NuevoLote()
        End Sub

        'BOTONES CONTROL ESCANER
        Private Sub BtnEscanear_Click(sender As Object, e As EventArgs) Handles BtnEscanear.Click
            If Trim(ParamTipologia.Numlote) = "" Then
                MsgBox("No hay un lote activo para iniciar la digitalización.  Verifique.")
                Exit Sub
            End If

            Me.DgvCarpetas.Enabled = False
            Me.BtnExportarLotes.Enabled = False
            If _deviceManager IsNot Nothing Then
                Try : _deviceManager.Close() : Catch : End Try
            End If
            Try
                'Using _deviceManager As New Vintasoft.Twain.DeviceManager()
                _deviceManager = New Vintasoft.Twain.DeviceManager()
                If Not _deviceManager.IsTwainAvailable Then
                    MsgBox("El DSM TWAIN 2.x no está instalado o no se encuentra en rutas del sistema.")
                    Return
                End If
                ' specify that TWAIN device manager 2.x must be used
                _deviceManager.VintasoftTwainServiceExePath = Application.StartupPath '& "\VintasoftTwainService.exe"
                _deviceManager.IsTwain2Compatible = True
                ' specify that 64-bit TWAIN device manager 2.x must use 32-bit devices
                _deviceManager.Use32BitDevices()
                ' open the device manager
                _deviceManager.Open()
                ' show a dialog for selecting 32-bit TWAIN driver
                '_deviceManager.ShowDefaultDeviceSelectionDialog()
                '  End Using
            Catch ex As Exception
                MsgError = ex.Message
                Exit Sub
            End Try
            '_deviceManager = New Vintasoft.Twain.DeviceManager(Me, Me.Handle)
            '_deviceManager.Open()
            StartImageAcquisitionButton()

            'AcquireImagesFromTwainDeviceAndProcessImages()

        End Sub

        Private Sub GuardarInformacionLote()

            Dim ArchiLotes As String = RutaLotes & "\Control_lotes.txt"
            If File.Exists(ArchiLotes) = False Then
                MsgBox("Archivo control de lotes no existe... Verifique.." & Chr(13) & ArchiLotes)
                Exit Sub
            End If
            Dim EntroaLote As Boolean = False
            Dim lines As List(Of String) = File.ReadAllLines(ArchiLotes).ToList()
            Dim Eslote As Boolean = False
            For i As Integer = 0 To lines.Count - 1
                If Mid(Trim(lines(i)), 1, 7).ToUpper = "NUMLOTE" Then
                    If Trim(Mid(lines(i), 9)) = Trim(ParamTipologia.Numlote) And Eslote = False Then
                        Eslote = True
                        Continue For
                    End If
                    If EntroaLote = True Then
                        Exit For
                    End If
                End If
                ParamTipologia.HoraInactivo = Now.ToString("HH:mm")
                If Eslote = True And Len(Trim(lines(i))) > 2 Then
                    EntroaLote = True
                    If Mid(Trim(lines(i)), 1, 8) = "IMAGENES" Then
                        lines(i) = "IMAGENES: " & ParamTipologia.Imagenes
                    End If
                    If Mid(Trim(lines(i)), 1, 9) = "NUMIMAGEN" Then
                        lines(i) = "NUMIMAGEN: " & ParamTipologia.NumImgen
                    End If
                    If Mid(Trim(lines(i)), 1, 12) = "HORAINACTIVO" Then
                        lines(i) = "HORAINACTIVO: " & ParamTipologia.HoraInactivo
                    End If
                End If
            Next i
            File.WriteAllLines(ArchiLotes, lines)

            'Debemos guardar el indice de las imagenes
            Dim ArchivoIndice As String = RutaLoteNuevo & "\" & "IndiceEscaneo.txt"
            If File.Exists(ArchivoIndice) = False Then
                FileOpen(1, ArchivoIndice, OpenMode.Output)
                FileClose(1)
            End If
            Dim TotImg As Integer = 0
            Using writer As New StreamWriter(ArchivoIndice)
                For Each Esimagen As ImagenEscaneada In Imagenes
                    TotImg += 1
                    Esimagen.IdImg = TotImg
                    Dim lineas As String = $"{Esimagen.IdImg},{Esimagen.Exportada},{Esimagen.NomImagen}"
                    writer.WriteLine(lineas)
                Next
            End Using

            If Me.InvokeRequired Then
                Me.Invoke(Sub()
                              Me.DgvCarpetas.Enabled = True
                              Me.BtnExportarLotes.Enabled = True
                          End Sub)
            Else
                Me.DgvCarpetas.Enabled = True
                Me.BtnExportarLotes.Enabled = True
            End If

        End Sub
        Private Sub BtnEliminaPagina_Click(sender As Object, e As EventArgs) Handles BtnEliminaPagina.Click
            If ParamTipologia.Imagenes = 0 Then
                Exit Sub
            End If
            If ParamTipologia.Estado <> "ABIERTO" Then
                Exit Sub
            End If
            If MsgBox("Esta seguro que quiere eliminar la imagen activa.", 324, "Eliminiar documento.") = MsgBoxResult.No Then
                Exit Sub
            End If
            Dim NombreImg As String = PicImagen.Tag
            Try
                PicImagen.Tag = ""
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
                PicImagen.Image = Nothing
                PicA.Image = Nothing
                PicA.Image = Nothing
            Catch ex As Exception
            End Try
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
                PicA.Image = Nothing
            End If
            Me.DgvRegistrosLote.Rows.Clear()
            FLPImagenes.Controls.Clear()
            'Resto una imagen del control de lotes

            'Borramos la imagen de la carpeta
            Dim EstaBorrado As Boolean = True
            While File.Exists(RutaLoteNuevo & "\" & NombreImg) = True
                Try
                    File.Delete(RutaLoteNuevo & "\" & NombreImg)
                    Exit While
                Catch ex As Exception
                    ' MsgBox(Err.Description)
                    EstaBorrado = False
                    Exit While
                End Try
            End While
            If EstaBorrado = False Then
                Exit Sub
            End If
            ControlCargueImagen("", "PDF " & ParamTipologia.Numlote, 0, False)
            ControlCargueImagen("", NombreImg, 0, False)
            ParamTipologia.Imagenes -= 1
            Dim ArchiLotes As String = RutaLotes & "\Control_lotes.txt"
            If File.Exists(ArchiLotes) = False Then
                MsgBox("Archivo control de lotes no existe... Verifique.." & Chr(13) & ArchiLotes)
                Exit Sub
            End If
            Dim lines As List(Of String) = File.ReadAllLines(ArchiLotes).ToList()
            Dim EntroaLote As Boolean = False
            Dim Eslote As Boolean = False
            For i As Integer = 0 To lines.Count - 1
                If Mid(Trim(lines(i)), 1, 7).ToUpper = "NUMLOTE" Then
                    If Trim(Mid(lines(i), 9)) = Trim(ParamTipologia.Numlote) And Eslote = False Then
                        Eslote = True
                        Continue For
                    End If
                    If EntroaLote = True Then
                        Exit For
                    End If
                End If
                If Eslote = True And Len(Trim(lines(i))) > 2 Then
                    EntroaLote = True
                    If Mid(Trim(lines(i)), 1, 8) = "IMAGENES" Then
                        lines(i) = "IMAGENES: " & ParamTipologia.Imagenes
                        Exit For
                    End If
                End If
            Next i
            File.WriteAllLines(ArchiLotes, lines)

            'Debemos guardar el indice de las imagenes sin la imagen borrada
            Dim ArchivoIndice As String = RutaLoteNuevo & "\" & "IndiceEscaneo.txt"
            If File.Exists(ArchivoIndice) = False Then
                FileOpen(1, ArchivoIndice, OpenMode.Output)
                FileClose(1)
            End If
            Dim TotImg As Integer = 0
            Using writer As New StreamWriter(ArchivoIndice)
                For Each Esimagen As ImagenEscaneada In Imagenes
                    If Esimagen.NomImagen <> NombreImg Then
                        TotImg += 1
                        Esimagen.IdImg = TotImg
                        Dim lineas As String = $"{Esimagen.IdImg},{Esimagen.Exportada},{Esimagen.NomImagen}"
                        writer.WriteLine(lineas)
                    End If
                Next
            End Using
            'Validamos si la imagen fue cargada desde un archivo y borramos el control

            'Debemos cargar de nuevo la informacion del lote
            ActualizaCantidadImagenes()
            CargarImagenesEnMiniatura(RutaLoteNuevo)
            IdRImg = ParamTipologia.Imagenes
            'Me.BtnEliminaPagina.Enabled = False
            'MsgBox("Imagen borrada.")
        End Sub
        Private Sub BtnEliminaLote_Click(sender As Object, e As EventArgs) Handles BtnEliminaLote.Click
            If MsgBox("Esta seguro que quiere eliminar el lote activo.", 324, "Eliminiar documento.") = MsgBoxResult.No Then
                Exit Sub
            End If
            Dim ArchiLotes As String = RutaLotes & "\Control_lotes.txt"
            If File.Exists(ArchiLotes) = False Then
                MsgBox("Archivo control de lotes no existe... Verifique.." & Chr(13) & ArchiLotes)
                Exit Sub
            End If
            Dim lines As List(Of String) = File.ReadAllLines(ArchiLotes).ToList()
            If File.Exists(ArchiLotes) = True Then
                FileOpen(1, ArchiLotes, OpenMode.Output)
                FileClose(1)
            End If
            Dim Eslote As Boolean = False
            FileOpen(1, ArchiLotes, OpenMode.Append)
            For i As Integer = 0 To lines.Count - 1
                If Mid(Trim(lines(i)), 1, 7).ToUpper = "NUMLOTE" Then
                    Eslote = False
                    If Trim(Mid(lines(i), 9)) = Trim(ParamTipologia.Numlote) And Eslote = False Then
                        Eslote = True
                        Continue For
                    End If
                End If
                If Eslote = False Then
                    PrintLine(1, lines(i))
                End If
            Next i
            FileClose(1)
            Me.LblNumLote.Text = ""
            'Me.LblTitulo.Text = ""
            Me.lblTipologia.Text = ""
            PnCampos.Controls.Clear()
            Me.PicImagen.Image = Nothing
            Me.PicImagen.Image = Nothing
            Me.PicA.Image = Nothing
            Me.PicA.Image = Nothing
            Me.DgvRegistrosLote.Rows.Clear()
            Me.DgvCarpetas.Rows.Clear()
            FLPImagenes.Controls.Clear()

            'Desmarcamos todas la imagenes cargadas en el lote
            Dim extensionesPermitidas = {".jpg", ".JPG", ".png", ".PNG", ".tif", ".TIF", ".tiff", ".TIFF"}
            Dim archivos = Directory.GetFiles(ParamTipologia.RutaLote) _
            .Where(Function(f) extensionesPermitidas.Contains(Path.GetExtension(f))).ToArray()

            For Each archivo In archivos
                ControlCargueImagen("", "PDF " & ParamTipologia.Numlote, 0, False)
                ControlCargueImagen(archivo, Path.GetFileName(archivo), 0, False)
            Next

            'Procedemos a borrar la carpeta con las imagenes
            If Directory.Exists(RutaLoteNuevo) = True Then
                Try
                    FLPImagenes.Controls.Clear()
                    Directory.Delete(RutaLoteNuevo, True)
                Catch
                    '  MsgBox(Err.Description)
                End Try
            End If

            'REGISTRO DE LOTE EN BASE DE DATOS
            Dim DtLote As DataTable
            Dim FechaCargue As String = Now.ToString("yyyy/MM/dd") & " " & Now.ToString("HH:mm:ss")
            DtLote = CDig.ActualizarLoteDigitalizacion(ParamTipologia.idEntidad, ParamTipologia.idProyecto,
               ParamTipologia.Numlote, FechaCargue, ParamTipologia.Imagenes, "CERRADO", 1)
            If DtLote Is Nothing Then
                MsgBox("Se presento un problema al borrar el lote en base de datos... Verifique.")
                Exit Sub
            End If

            ParamTipologia.Imagenes = 0

            'limpiamos todos los controles y cargamos de nuevo las carpetas
            If CargarLotesExistentes() = False Then
            End If
            MsgBox("Lote eliminado..")
            Des_HabilitarBotones(0)

        End Sub


#End Region

#Region "Sub y Function"

        Private Sub CargarEsquemas()

            DtEsquemas = New DataTable
            DtEsquemas = CDig.ConsultaSiriesDocumentales
            If DtEsquemas Is Nothing Then
                MsgBox("Se presento un problema al consultar series documentales... Verifique.")
                Exit Sub
            End If
            If Not DtEsquemas.Rows.Count > 0 Then
                MsgBox("No existen series documentales configuradas... Verifique.")
                Exit Sub
            End If
            Me.CmbTipologia.Items.Clear()

            For i As Integer = 0 To DtEsquemas.Rows.Count - 1
                CmbTipologia.Items.Add(Val(DtEsquemas.Rows(i).Item("id_Esquema")).ToString("00") & " - " & Trim(DtEsquemas.Rows(i).Item("Nombre_Esquema")).ToUpper)
            Next
        End Sub

        Private Sub CargarParametrosTipologia()
            If Trim(Me.CmbTipologia.Text) = "" Then
                Exit Sub
            End If
            Dim idEsquema As Integer = Val(Mid(Me.CmbTipologia.Text, 1, 2))
            If DtEsquemas Is Nothing Then
                MsgBox("No existen series documentales cargadas para este proceso.. Verifique.")
                Exit Sub
            End If
            If Not DtEsquemas.Rows.Count > 0 Then
                MsgBox("No existen series documentales cargadas para este proceso.. Verifique.")
                Exit Sub
            End If
            For i As Integer = 0 To DtEsquemas.Rows.Count - 1
                If DtEsquemas.Rows(i).Item("id_Esquema") = idEsquema Then
                    ParamTipologia.idEntidad = DtEsquemas.Rows(i).Item("fk_Entidad")
                    ParamTipologia.idProyecto = DtEsquemas.Rows(i).Item("fk_Proyecto")
                    ParamTipologia.idEsquema = DtEsquemas.Rows(i).Item("id_Esquema")
                    ParamTipologia.EsquemaNombre = DtEsquemas.Rows(i).Item("Nombre_Esquema")
                    ParamTipologia.TamPapel = DtEsquemas.Rows(i).Item("fk_Tipo_Papel")
                    ParamTipologia.ResolucionX = DtEsquemas.Rows(i).Item("Resolucion_X")
                    ParamTipologia.ResolucionY = DtEsquemas.Rows(i).Item("Resolucion_Y")
                    ParamTipologia.FormatoSalida = DtEsquemas.Rows(i).Item("Formato_Compresion")
                    ParamTipologia.Contraste = DtEsquemas.Rows(i).Item("Contraste")
                    ParamTipologia.TipoImagen = DtEsquemas.Rows(i).Item("fk_Tipo_Imagen")
                    ParamTipologia.Duplex = DtEsquemas.Rows(i).Item("Duplex")
                    ParamTipologia.Usuario = ""
                    ParamTipologia.RutaLote = ""
                    ParamTipologia.Numlote = ""
                    ParamTipologia.CamposCaptura = ""
                    ParamTipologia.FechaProceso = Now.ToString("yyyy/MM/dd")
                    ParamTipologia.Imagenes = 0
                    ParamTipologia.NumImgen = 0
                    ParamTipologia.Estado = "ABIERTO"
                    ParamTipologia.HoraInactivo = Now.ToString("HH:mm")
                    ParamTipologia.TamanoHojaBlanca = DtEsquemas.Rows(i).Item("PesoImagenenBlanco")
                    ParamTipologia.IntentoExportar = 0
                    Exit For
                End If
            Next
            'Cargar campos a capturar en la tipologia
            DtCampos = New DataTable
            DtCampos = CDig.ConsultaEsquemaCampo(ParamTipologia.idEsquema, ParamTipologia.idEntidad, ParamTipologia.idProyecto)
            If DtCampos Is Nothing Then
                MsgBox("Se presento un problema al consultar los campos... Verifique.")
                Exit Sub
            End If
            For i As Integer = 0 To DtCampos.Rows.Count - 1
                If Mid(DtCampos.Rows(i).Item("Nombre_Campo").ToString.ToUpper.Trim, 1, 5) = "SERIE" Then
                    ParamTipologia.idSerieDctal = DtCampos.Rows(i).Item("id_Esquema_Campo").ToString.Trim
                    Continue For
                End If
                If DtCampos.Rows(i).Item("Nombre_Campo").ToString.ToUpper.Trim = "OFICINA" Then
                    ParamTipologia.idOficina = DtCampos.Rows(i).Item("id_Esquema_Campo").ToString.Trim
                End If
            Next i
            CrearCamposCapturaEsquema()
            PrepararExtensiones()
            EnviarFocoAlPrimerCampoDelPanel(PnCampos)

        End Sub

        Private Sub CrearCamposCapturaEsquema()
            ' Limpiar controles anteriores
            PnCampos.Controls.Clear()
            Dim IniLabel As Integer = 5
            Dim Initext As Integer = 5
            Dim Espacio As Integer = 25
            Dim DtLista As New DataTable

            ' Recorrer cada fila del DataTable
            For Each fila As DataRow In DtCampos.Rows
                If Trim(fila("Campo_Fijo")) = False Then
                    Continue For
                End If
                ' Crear un Label
                Dim lbl As New Label() With {
                .Text = Trim(fila("Detalle_Campo")),
                .Name = "Lbl" & Trim(fila("Nombre_Campo")),
                .Size = New Size(138, 20),
                .BorderStyle = BorderStyle.Fixed3D,
                .Location = New Point(5, IniLabel),
                .AutoSize = False}
                PnCampos.Controls.Add(lbl)
                ' Texto, Numérico, Fecha, Si/ No, Lista, Lista Enlazada, Funcion,  Tabla Asociada
                Select Case fila("Nombre_Campo_Tipo")
                    Case "Texto"
                        ' Crear un TextBox
                        Dim txt As New TextBoxMask.TextBoxMask() With {
                        .Name = "Txt" & Trim(fila("Nombre_Campo")),
                        .Size = New Size(175, 20),
                        .Tag = fila("id_Esquema_Campo"),
                        .MaxLength = fila("Length_Campo"),
                        .Solo = TextBoxMask.TextBoxMask.Permite.Todos,
                        .Location = New Point(145, Initext)}
                        AddHandler txt.KeyDown, AddressOf Campo_KeyDown
                        PnCampos.Controls.Add(txt)
                        UltimoControl = txt
                        If fila("Campo_Fijo") And Not IsDBNull(fila("Valor_Por_Defecto")) AndAlso fila("Valor_Por_Defecto") <> "" Then
                            txt.Text = fila("Valor_Por_Defecto")
                            'txt.Enabled = False
                        End If

                    Case "Numérico"
                        ' Crear un TextBox
                        Dim txN As New TextBoxMask.TextBoxMask() With {
                        .Name = "Txn" & Trim(fila("Nombre_Campo")),
                        .Size = New Size(175, 20),
                        .Tag = fila("id_Esquema_Campo"),
                        .MaxLength = fila("Length_Campo"),
                        .Location = New Point(145, Initext)}
                        If fila("Usa_Decimales") = False Then
                            txN.Solo = TextBoxMask.TextBoxMask.Permite.Entero
                        Else
                            txN.Solo = TextBoxMask.TextBoxMask.Permite.Real
                        End If
                        AddHandler txN.KeyDown, AddressOf Campo_KeyDown
                        PnCampos.Controls.Add(txN)
                        UltimoControl = txN
                        If fila("Campo_Fijo") And fila("Valor_Por_Defecto") <> "" Then
                            txN.Text = fila("Valor_Por_Defecto")
                            'txN.Enabled = False
                        End If

                    Case "Fecha"
                        Dim Dtp As New DateTimePicker() With {
                         .Name = "Dtp" & Trim(fila("Nombre_Campo")),
                        .Size = New Size(175, 20),
                        .Tag = fila("id_Esquema_Campo"),
                        .Format = DateTimePickerFormat.Short,
                        .Value = Now(),
                        .MaxDate = DateTime.Today,
                        .Location = New Point(145, Initext)}
                        AddHandler Dtp.KeyDown, AddressOf Campo_KeyDown
                        PnCampos.Controls.Add(Dtp)
                        UltimoControl = Dtp

                    Case "Si/No"
                        Dim Chb As New CheckBox() With {
                         .Name = "Chb" & Trim(fila("Nombre_Campo")),
                         .Text = "",
                        .Size = New Size(20, 20),
                        .Tag = fila("id_Esquema_Campo"),
                        .Location = New Point(145, Initext)}
                        PnCampos.Controls.Add(Chb)
                        UltimoControl = Chb

                    Case "Lista" 'ComboBox
                        DtLista = New DataTable
                        DtLista = CDig.ConsultaEsquemaCampolista(ParamTipologia.idEntidad, fila("fk_Campo_Lista"))
                        If DtLista Is Nothing Then
                            MsgBox("Se presento un problema al consultar los campos... Verifique.")
                            Exit Sub
                        End If
                        '[id_Campo_Lista_Item], [Etiqueta_Campo_Lista_Item]
                        Dim Comb = New ComboBox() With {
                         .Name = "Cmb" & Trim(fila("Nombre_Campo")),
                        .Size = New Size(175, 20),
                        .Tag = fila("id_Esquema_Campo"),
                        .Location = New Point(145, Initext),
                        .DropDownStyle = ComboBoxStyle.DropDown,
                        .DataSource = DtLista,
                        .DisplayMember = "Etiqueta_Campo_Lista_Item",
                        .ValueMember = "Valor_Campo_Lista_Item"}
                        AddHandler Comb.GotFocus, AddressOf ComboBox_GotFocus
                        AddHandler Comb.KeyDown, AddressOf Campo_KeyDown
                        PnCampos.Controls.Add(Comb)
                        UltimoControl = Comb
                End Select
                ' Incrementar posición vertical para la próxima fila
                IniLabel += Espacio
                Initext += Espacio
            Next
        End Sub

        Private Sub Campo_KeyDown(sender As Object, e As KeyEventArgs)
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True ' Evita que suene el "ding"

                ' Buscar el siguiente control dentro del mismo contenedor (por ejemplo, Panel1)
                Dim ctrlActual As Control = CType(sender, Control)
                Dim parentContainer As Control = ctrlActual.Parent

                If ctrlActual Is UltimoControl Then
                    CrearCarpetaTipologia()
                    Exit Sub
                End If

                Dim siguienteEncontrado As Boolean = False
                Dim controlesOrdenados = parentContainer.Controls.Cast(Of Control)().
                                  Where(Function(c) c.CanSelect).
                                  OrderBy(Function(c) c.TabIndex)

                For Each ctrl In controlesOrdenados
                    If siguienteEncontrado Then
                        ctrl.Focus()
                        Exit Sub
                    End If

                    If ctrl Is ctrlActual Then
                        siguienteEncontrado = True
                    End If
                Next
            End If
        End Sub
        Private Sub ComboBox_GotFocus(sender As Object, e As EventArgs)
            Dim cbx As ComboBox = CType(sender, ComboBox)
            ' Abrir el desplegable
            cbx.DroppedDown = True
        End Sub
        Private Sub EnviarFocoAlPrimerCampoDelPanel(pnl As Panel)
            Dim primerControl = pnl.Controls.Cast(Of Control)().Where(Function(c) c.CanSelect).OrderBy(Function(c) c.TabIndex).FirstOrDefault()

            primerControl?.Focus()
            'If primerControl IsNot Nothing Then
            '    primerControl.Focus()
            'End If
        End Sub
        Private Sub DgvRegistrosLote_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvRegistrosLote.CellDoubleClick
            If e.RowIndex >= 0 Then
                ' Obtener la fila seleccionada
                Dim filaSeleccionada As DataGridViewRow = DgvRegistrosLote.Rows(e.RowIndex)
                ' Obtener los valores de las celdas de la fila seleccionada
                Dim NomImagen As String = filaSeleccionada.Cells("Nombre").Value.ToString()
                ' Mostrar los datos de la fila seleccionada
                MostrarImagen(RutaLoteNuevo & "\" & NomImagen, NomImagen)
            End If
        End Sub

        Private Sub CmbTipologia_GotFocus(sender As Object, e As EventArgs) Handles CmbTipologia.GotFocus
            SendKeys.Send("{F4}")
        End Sub
        'Private Sub CmbTipologia_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles CmbTipologia.PreviewKeyDown
        '    If e.KeyData = Keys.Enter Then
        '        SendKeys.Send("{TAB}")
        '    End If
        'End Sub
        Private Sub CmbTipologia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTipologia.SelectedIndexChanged
            SendKeys.Send("{TAB}")
        End Sub
        Private Sub CmbTipologia_MouseUp(sender As Object, e As MouseEventArgs) Handles CmbTipologia.MouseUp
            SendKeys.Send("{TAB}")
        End Sub
        Private Sub CmbTipologia_LostFocus(sender As Object, e As EventArgs) Handles CmbTipologia.LostFocus
            CambioTipologia()
        End Sub


        Private Sub CambioTipologia()
            If Trim(Me.CmbTipologia.Text) <> "" Then
                Me.lblTipologia.Text = Me.CmbTipologia.Text
                Me.CmbTipologia.Visible = False
                CargarParametrosTipologia()
            End If
        End Sub

        Private Sub DgvCarpetas_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvCarpetas.CellMouseDoubleClick
            If e.RowIndex >= 0 Then
                Dim fila As DataGridViewRow = DgvCarpetas.Rows(e.RowIndex)
                NombreLote = fila.Cells(0).Value.ToString()
                RutaLoteNuevo = RutaLotes & "\" & NombreLote
                'Me.CmbTipologia.Enabled = False
                CargarInformaciondelote(NombreLote)
                Des_habilitarCamposCaptura(0)
                CargarImagenesEnMiniatura(RutaLoteNuevo)
                Des_HabilitarBotones(1)
                Me.BtnExportarLotes.Enabled = True
                If ParamTipologia.Estado = "ABIERTO" Then
                    Des_HabilitarBotones(1)
                    Me.BtnExportarLotes.Enabled = True
                    Me.BtnEscanear.Focus()
                Else
                    Des_HabilitarBotones(0)
                    Me.BtnExportarLotes.Enabled = False
                End If
            End If
        End Sub

        Private Sub Des_HabilitarBotones(Quehago As Integer)
            If Quehago = 0 Then
                Me.BtnEscanear.Enabled = False
                Me.BtnEscanear.FlatStyle = FlatStyle.Standard
                Me.BtnAddCarpeta.Enabled = False
                Me.BtnAddCarpeta.FlatStyle = FlatStyle.Standard
                Me.BtnAddImagen.Enabled = False
                Me.BtnAddImagen.FlatStyle = FlatStyle.Standard
                Me.BtnEliminaPagina.Enabled = False
                Me.BtnEliminaPagina.FlatStyle = FlatStyle.Standard
                Me.BtnEliminaLote.Enabled = False
                Me.BtnEliminaLote.FlatStyle = FlatStyle.Standard
            Else
                Me.BtnEscanear.Enabled = True
                Me.BtnEscanear.FlatStyle = FlatStyle.Flat
                Me.BtnAddCarpeta.Enabled = True
                Me.BtnAddCarpeta.FlatStyle = FlatStyle.Flat
                Me.BtnAddImagen.Enabled = True
                Me.BtnAddImagen.FlatStyle = FlatStyle.Flat
                Me.BtnEliminaPagina.Enabled = True
                Me.BtnEliminaPagina.FlatStyle = FlatStyle.Flat
                Me.BtnEliminaLote.Enabled = True
                Me.BtnEliminaLote.FlatStyle = FlatStyle.Flat
            End If
        End Sub


        Private Sub Des_habilitarCamposCaptura(Quehago As Integer)
            For Each ctrl As Control In PnCampos.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("Txt") Then
                    If Quehago = 0 Then
                        ctrl.Enabled = False
                    Else
                        ctrl.Enabled = True
                    End If
                    'Dim Txt As TextBox = CType(ctrl, TextBox)
                End If

                If TypeOf ctrl Is ComboBox AndAlso ctrl.Name.StartsWith("Cmb") Then
                    If Quehago = 0 Then
                        ctrl.Enabled = False
                    Else
                        ctrl.Enabled = True
                    End If
                    'Dim cbx As ComboBox = CType(ctrl, ComboBox)
                End If

                If TypeOf ctrl Is DateTimePicker AndAlso ctrl.Name.StartsWith("Dtp") Then
                    If Quehago = 0 Then
                        ctrl.Enabled = False
                    Else
                        ctrl.Enabled = True
                    End If
                    'Dim Dtp As DateTimePicker = CType(ctrl, DateTimePicker)
                End If

                If TypeOf ctrl Is CheckBox AndAlso ctrl.Name.StartsWith("Chb") Then
                    If Quehago = 0 Then
                        ctrl.Enabled = False
                    Else
                        ctrl.Enabled = True
                    End If
                    'Dim Chb As CheckBox = CType(ctrl, CheckBox)
                End If


            Next
        End Sub

        Private Sub PictureMini_DoubleClick(sender As Object, e As MouseEventArgs)
            Dim PictureMini As PictureBox = CType(sender, PictureBox)

            For Each fila As DataGridViewRow In DgvRegistrosLote.Rows
                If fila.Cells("Nombre").Value.ToString.ToLower = PictureMini.Name.ToLower Then
                    IdRImg = fila.Index
                    Exit For
                End If
            Next
            MostrarImagen(RutaLoteNuevo & "\" & PictureMini.Name, PictureMini.Name)

        End Sub

        Private Sub CargarToolTip()
            ToolTip.AutoPopDelay = 5000    ' El ToolTip permanecerá visible durante 5 segundos
            ToolTip.InitialDelay = 500    ' El ToolTip aparecerá después de 1 segundo
            ToolTip.ReshowDelay = 500

            ToolTip.SetToolTip(Me.BtnPrimero, Me.BtnPrimero.Tag)
            ToolTip.SetToolTip(Me.BtnAnterior, Me.BtnAnterior.Tag)
            ToolTip.SetToolTip(Me.BtnSiguiente, Me.BtnSiguiente.Tag)
            ToolTip.SetToolTip(Me.BtnUltimo, Me.BtnUltimo.Tag)
            ToolTip.SetToolTip(Me.BtnGiraDerecha, Me.BtnGiraDerecha.Tag)
            ToolTip.SetToolTip(Me.BtnGiraIzquieda, Me.BtnGiraIzquieda.Tag)
            ToolTip.SetToolTip(Me.BtnNormal, Me.BtnNormal.Tag)
            ToolTip.SetToolTip(Me.BtnAutoTamaño, Me.BtnAutoTamaño.Tag)
            ToolTip.SetToolTip(Me.BtnAmpliar, Me.BtnAmpliar.Tag)
            ToolTip.SetToolTip(Me.BtnReducir, Me.BtnReducir.Tag)
            ToolTip.SetToolTip(Me.BtnNewCarpeta, Me.BtnNewCarpeta.Tag)
            ToolTip.SetToolTip(Me.BtnExportarLotes, Me.BtnExportarLotes.Tag)
            ToolTip.SetToolTip(Me.BtnEscanear, Me.BtnEscanear.Tag)
            ToolTip.SetToolTip(Me.BtnAddImagen, Me.BtnAddImagen.Tag)
            ToolTip.SetToolTip(Me.BtnAddCarpeta, Me.BtnAddCarpeta.Tag)
            ToolTip.SetToolTip(Me.BtnEliminaPagina, Me.BtnEliminaPagina.Tag)
            ToolTip.SetToolTip(Me.BtnEliminaLote, Me.BtnEliminaLote.Tag)

            ToolTip.SetToolTip(Me.DgvCarpetas, "Relación carpetas de lotes.")
            ToolTip.SetToolTip(Me.DgvRegistrosLote, "Relación de documentos escaneados.")
            ToolTip.SetToolTip(Me.FLPImagenes, "Miniatura de imagenes escaneadas.")
            ToolTip.SetToolTip(Me.PicImagen, "Imagen escaneada activa.")
        End Sub

        Private Sub CargarDetallesLotesCreados()
            Dim ArchiLotes As String = RutaLotes & "\Control_lotes.txt"
            Dim Tlot As Integer = 0
            ReDim LotesLeidos(0)
            If File.Exists(ArchiLotes) = False Then
                Exit Sub
            End If
            Dim Linea As String
            FileOpen(1, ArchiLotes, OpenMode.Input, OpenAccess.Default)
            Do While Not EOF(1)
                Linea = LineInput(1)
                If Trim(Linea) <> "RELACION DE LOTES CREADOS" And Len(Trim(Linea)) > 2 Then
                    If Mid(Trim(Linea), 1, 7).ToUpper = "NUMLOTE" Then
                        Tlot += 1
                        ReDim Preserve LotesLeidos(Tlot)
                    End If
                    Select Case Trim(Mid(Trim(Linea), 1, InStr(Trim(Linea), ":") - 1).ToUpper)
                        Case "NUMLOTE" : LotesLeidos(Tlot).Numlote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDENTIDAD" : LotesLeidos(Tlot).idEntidad = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDPROYECTO" : LotesLeidos(Tlot).idProyecto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDESQUEMA" : LotesLeidos(Tlot).idEsquema = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDSERIEDCTAL" : LotesLeidos(Tlot).idSerieDctal = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDOFICINA" : LotesLeidos(Tlot).idOficina = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESQUEMANOMBRE" : LotesLeidos(Tlot).EsquemaNombre = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TAMPAPEL" : LotesLeidos(Tlot).TamPapel = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RESOLUCIONX" : LotesLeidos(Tlot).ResolucionX = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RESOLUCIONY" : LotesLeidos(Tlot).ResolucionY = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FORMATOSALIDA" : LotesLeidos(Tlot).FormatoSalida = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONTRASTE" : LotesLeidos(Tlot).Contraste = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TIPOIMAGEN" : LotesLeidos(Tlot).TipoImagen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "DUPLEX" : LotesLeidos(Tlot).Duplex = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "USUARIO" : LotesLeidos(Tlot).Usuario = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RUTALOTE" : LotesLeidos(Tlot).RutaLote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FECHAPROCESO" : LotesLeidos(Tlot).FechaProceso = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CAMPOSCAPTURA" : LotesLeidos(Tlot).CamposCaptura = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IMAGENES" : LotesLeidos(Tlot).Imagenes = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "NUMIMAGEN" : LotesLeidos(Tlot).NumImgen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESTADO" : LotesLeidos(Tlot).Estado = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "HORAINACTIVO" : LotesLeidos(Tlot).HoraInactivo = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TAMANOHOJABLANCA" : LotesLeidos(Tlot).TamanoHojaBlanca = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "INTENTOEXPORTAR" : LotesLeidos(Tlot).IntentoExportar = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ERROREXPORTAR" : LotesLeidos(Tlot).ErrorExportar = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                    End Select

                End If
            Loop
            FileClose(1)
        End Sub

        Private Sub CargarInformaciondelote(Nombre As String)
            If Trim(Nombre) = "" Then Exit Sub
            Dim ArchiLotes As String = RutaLotes & "\Control_lotes.txt"
            If File.Exists(ArchiLotes) = False Then
                MsgBox("Archivo control de lotes no existe... Verifique.." & Chr(13) & ArchiLotes)
                Exit Sub
            End If
            Dim Linea As String
            Dim EsTipo As Boolean = False
            ParamTipologia.Numlote = Nombre
            FileOpen(1, ArchiLotes, OpenMode.Input, OpenAccess.Default)
            Do While Not EOF(1)
                Linea = LineInput(1) 'Archivo.ReadLine
                If Mid(Trim(Linea), 1, 7).ToUpper = "NUMLOTE" Then
                    If Trim(Mid(Linea, 9)) = Trim(Nombre) And EsTipo = False Then
                        EsTipo = True
                        Continue Do
                    Else
                        If EsTipo = True Then
                            Exit Do
                        End If
                    End If
                End If
                If EsTipo = True And Len(Trim(Linea)) > 2 Then
                    Select Case Trim(Mid(Trim(Linea), 1, InStr(Trim(Linea), ":") - 1).ToUpper)
                        Case "NUMLOTE" : ParamTipologia.Numlote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDENTIDAD" : ParamTipologia.idEntidad = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDPROYECTO" : ParamTipologia.idProyecto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDESQUEMA" : ParamTipologia.idEsquema = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDSERIEDCTAL" : ParamTipologia.idSerieDctal = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDOFICINA" : ParamTipologia.idOficina = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESQUEMANOMBRE" : ParamTipologia.EsquemaNombre = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TAMPAPEL" : ParamTipologia.TamPapel = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RESOLUCIONX" : ParamTipologia.ResolucionX = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RESOLUCIONY" : ParamTipologia.ResolucionY = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FORMATOSALIDA" : ParamTipologia.FormatoSalida = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONTRASTE" : ParamTipologia.Contraste = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TIPOIMAGEN" : ParamTipologia.TipoImagen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "DUPLEX" : ParamTipologia.Duplex = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "USUARIO" : ParamTipologia.Usuario = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RUTALOTE" : ParamTipologia.RutaLote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FECHAPROCESO" : ParamTipologia.FechaProceso = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CAMPOSCAPTURA" : ParamTipologia.CamposCaptura = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IMAGENES" : ParamTipologia.Imagenes = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "NUMIMAGEN" : ParamTipologia.NumImgen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESTADO" : ParamTipologia.Estado = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "HORAINACTIVO" : ParamTipologia.HoraInactivo = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TAMANOHOJABLANCA" : ParamTipologia.TamanoHojaBlanca = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "INTENTOEXPORTAR" : ParamTipologia.IntentoExportar = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ERROREXPORTAR" : ParamTipologia.ErrorExportar = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                    End Select
                End If
            Loop
            FileClose(1)
            If ParamTipologia.RutaLote Is Nothing Then
                Try
                    Directory.Delete(RutaLoteNuevo, True)
                    Exit Sub
                Catch ex As Exception
                    MsgBox(Err.Description)
                End Try
            End If
            If ParamTipologia.RutaLote Is Nothing Then
                Exit Sub
            End If
            Me.LblNumLote.Text = ParamTipologia.Numlote

            'Cargar campos a capturar en la tipologia
            DtCampos = New DataTable
            DtCampos = CDig.ConsultaEsquemaCampo(ParamTipologia.idEsquema, ParamTipologia.idEntidad, ParamTipologia.idProyecto)
            If DtCampos Is Nothing Then
                MsgBox("Se presento un problema al consultar los campos... Verifique.")
                Exit Sub
            End If
            CrearCamposCapturaEsquema()
            PrepararExtensiones()
            CargarInformacionCapturadadeLote()
            Me.CmbTipologia.Text = ParamTipologia.EsquemaNombre
            Me.lblTipologia.Text = ParamTipologia.EsquemaNombre
            RutaLoteNuevo = ParamTipologia.RutaLote

        End Sub

        Private Sub PrepararExtensiones()
            Dim ExtensionesOriginales As String = ""
            For i As Integer = 0 To DtEsquemas.Rows.Count - 1
                If DtEsquemas.Rows(i).Item("id_Esquema") = ParamTipologia.idEsquema Then
                    ExtensionesOriginales = DtEsquemas.Rows(i).Item("formato_Archivo_Cargue")
                    Exit For
                End If
            Next
            Dim listaExt = ParsearExtensiones(ExtensionesOriginales)
            ' 2) Construir el set con punto al inicio para validar: {".jpg",".tif",".png",".pdf"}
            SetExt = ConstruirSetExtensionesPermitidas(listaExt)

            ' 3) Construir el filtro para OpenFileDialog
            FiltroExt = ConstruirFiltroOpenFileDialog(listaExt, "Tipos permitidos")

        End Sub

        Private Sub CargarInformacionCapturadadeLote()
            Dim Temp As New TextBox With {
                .Multiline = True,
                .Text = Replace(ParamTipologia.CamposCaptura, "|", vbNewLine)}

            For Each ctrl As Control In PnCampos.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("Txt") Then
                    Dim Txt As TextBox = CType(ctrl, TextBox)
                    For Each linea As String In Temp.Lines
                        Dim Plinea() As String = linea.Split(";"c)
                        If Val(Plinea(0)) = Val(Txt.Tag) Then
                            Txt.Text = Trim(Plinea(1))
                            Exit For
                        End If
                    Next
                End If

                If TypeOf ctrl Is ComboBox AndAlso ctrl.Name.StartsWith("Cmb") Then
                    Dim cbx As ComboBox = CType(ctrl, ComboBox)
                    For Each linea As String In Temp.Lines
                        Dim Plinea() As String = linea.Split(";"c)
                        If Val(Plinea(0)) = Val(cbx.Tag) Then
                            cbx.SelectedValue = Val(Plinea(1))
                            'Dim Idindex As Integer = cbx.FindString(Trim(Plinea(1)))
                            'If Idindex >= 0 Then
                            '    cbx.SelectedIndex = Idindex
                            'End If
                            Exit For
                        End If
                    Next
                End If

                If TypeOf ctrl Is DateTimePicker AndAlso ctrl.Name.StartsWith("Dtp") Then
                    Dim Dtp As DateTimePicker = CType(ctrl, DateTimePicker)
                    For Each linea As String In Temp.Lines
                        Dim Plinea() As String = linea.Split(";"c)
                        If Val(Plinea(0)) = Val(Dtp.Tag) Then
                            Dtp.Value = CDate(Plinea(1))
                            Exit For
                        End If
                    Next
                End If

                If TypeOf ctrl Is CheckBox AndAlso ctrl.Name.StartsWith("Chb") Then
                    Dim Chb As CheckBox = CType(ctrl, CheckBox)
                    For Each linea As String In Temp.Lines
                        Dim Plinea() As String = linea.Split(";"c)
                        If Val(Plinea(0)) = Val(Chb.Tag) Then
                            Chb.Checked = Plinea(1)
                            Exit For
                        End If
                    Next
                End If
            Next

        End Sub

        Private Sub NuevoLote()
            Me.LblNumLote.Text = ""
            Me.lblTipologia.Text = ""
            Me.CmbTipologia.Visible = True
            Me.PicImagen.Image = Nothing
            Me.DgvRegistrosLote.Rows.Clear()
            PnCampos.Controls.Clear()
            FLPImagenes.Controls.Clear()
            Me.CmbTipologia.Text = ""
            Me.CmbTipologia.SelectedIndex = -1
            CmbTipologia.Focus()
            'CmbTipologia.Focus()
        End Sub
        Private Function CrearLote() As Boolean
            If ValidarCampos() = False Then
                Return False
            End If

            RutaLotes = RutaImagSCan & "\" & Now.ToString("yyyyMMdd")
            RutaLoteNuevo = RutaLotes & "\" & Trim(SucursalIngreso) & "-"
            RutaLoteNuevo &= Trim(Mid(Me.lblTipologia.Text, 1, InStr(Me.lblTipologia.Text, "-") - 1)) & "-"
            RutaLoteNuevo &= Now.ToString("yyyyMMdd") & Now.ToString("HHmmss")
            Me.LblNumLote.Text = Mid(RutaLoteNuevo, InStrRev(RutaLoteNuevo, "\") + 1)
            If Directory.Exists(RutaLoteNuevo) = False Then
                Try
                    Directory.CreateDirectory(RutaLoteNuevo)
                Catch ex As Exception
                    MsgBox(Err.Description)
                    Return False
                End Try
            End If
            ParamTipologia.Numlote = Me.LblNumLote.Text
            ParamTipologia.CamposCaptura = GuardarCamposCapturados()
            ParamTipologia.FechaProceso = Now.ToString("yyyy/MM/dd")
            ParamTipologia.EsquemaNombre = Me.CmbTipologia.Text
            ParamTipologia.Usuario = Program.MiharuSession.Usuario.id
            ParamTipologia.RutaLote = RutaLoteNuevo
            ParamTipologia.Imagenes = 0
            ParamTipologia.NumImgen = 0
            ParamTipologia.Estado = "ABIERTO"
            ParamTipologia.HoraInactivo = Now.ToString("HH:mm")
            TImagen = -1
            ReDim Imagenes(0)

            'REGISTRO DE LOTE EN BASE DE DATOS
            If Jornada Is Nothing Then Jornada = "Normal"
            If NumLabel Is Nothing Then NumLabel = "0"
            If FechaMvto = Date.MinValue Then FechaMvto = Now.ToString("yyyy/MM/dd")
            Dim FuncionarioOfi As String = Trim(Program.MiharuSession.Usuario.Nombres) & " " & Trim(Program.MiharuSession.Usuario.Apellidos)
            Dim FechaDigitaliza As String = Now.ToString("yyyy/MM/dd") & " " & Now.ToString("HH:mm:ss")
            Dim DtLote As DataTable = CDig.RegistrarLoteaDigitalizar(ParamTipologia.idEntidad, ParamTipologia.idProyecto, Trim(Mid(Me.CmbTipologia.Text, 1, 3)), Trim(Mid(Me.CmbTipologia.Text, 6)),
               ParamTipologia.Numlote, ParamTipologia.FechaProceso, FechaDigitaliza, FechaDigitaliza, Jornada, Mid(Me.LblNomOfic.Text, 1, 5), Mid(Me.LblNomOfic.Text, 6), NumLabel, ParamTipologia.Imagenes, Environment.MachineName, FuncionarioOfi, "ABIERTO", 0)
            If DtLote Is Nothing Then
                MsgBox("Se presento un problema al crear el lote en base de datos... Verifique.")
                Return False
            End If

            GuardarLoteCreado()
            CargarLotesExistentes()
            Des_HabilitarBotones(1)
            ' Me.BtnExportarLotes.Enabled = True
            Return True
        End Function
        Private Sub GuardarLoteCreado()
            Dim ArchiLotes As String
            ArchiLotes = RutaLotes & "\Control_lotes.txt"
            If File.Exists(ArchiLotes) = False Then
                FileOpen(1, ArchiLotes, OpenMode.Output)
                FileClose(1)
                FileOpen(1, ArchiLotes, OpenMode.Append)
                PrintLine(1, "RELACION DE LOTES CREADOS")
            Else
                FileOpen(1, ArchiLotes, OpenMode.Append)
            End If
            PrintLine(1, "")
            PrintLine(1, "NUMLOTE: " & ParamTipologia.Numlote)
            PrintLine(1, "IDENTIDAD: " & ParamTipologia.idEntidad)
            PrintLine(1, "IDPROYECTO: " & ParamTipologia.idProyecto)
            PrintLine(1, "IDESQUEMA: " & ParamTipologia.idEsquema)
            PrintLine(1, "IDSERIEDCTAL: " & ParamTipologia.idSerieDctal)
            PrintLine(1, "IDOFICINA: " & ParamTipologia.idOficina)
            PrintLine(1, "ESQUEMANOMBRE: " & ParamTipologia.EsquemaNombre)
            PrintLine(1, "TAMPAPEL: " & ParamTipologia.TamPapel)
            PrintLine(1, "RESOLUCIONX: " & ParamTipologia.ResolucionX)
            PrintLine(1, "RESOLUCIONY: " & ParamTipologia.ResolucionY)
            PrintLine(1, "FORMATOSALIDA: " & ParamTipologia.FormatoSalida)
            PrintLine(1, "CONTRASTE: " & ParamTipologia.Contraste)
            PrintLine(1, "TIPOIMAGEN: " & ParamTipologia.TipoImagen)
            PrintLine(1, "DUPLEX: " & ParamTipologia.Duplex)
            PrintLine(1, "USUARIO: " & ParamTipologia.Usuario)
            PrintLine(1, "RUTALOTE: " & ParamTipologia.RutaLote)
            PrintLine(1, "FECHAPROCESO: " & ParamTipologia.FechaProceso)
            PrintLine(1, "CAMPOSCAPTURA: " & ParamTipologia.CamposCaptura)
            PrintLine(1, "IMAGENES: " & ParamTipologia.Imagenes)
            PrintLine(1, "NUMIMAGEN: " & ParamTipologia.NumImgen)
            PrintLine(1, "ESTADO: " & ParamTipologia.Estado)
            PrintLine(1, "HORAINACTIVO: " & ParamTipologia.HoraInactivo)
            PrintLine(1, "TAMANOHOJABLANCA: " & ParamTipologia.TamanoHojaBlanca)
            PrintLine(1, "INTENTOEXPORTAR: " & ParamTipologia.IntentoExportar)
            PrintLine(1, "ERROREXPORTAR: " & ParamTipologia.ErrorExportar)
            FileClose(1)
        End Sub
        Private Function CargarLotesExistentes() As Boolean
            CargarLotesExistentes = True
            If Directory.Exists(RutaLotes) = False Then
                ' MsgBox("Directorio de lote no existe.. Verifique.", 64, "Error en directorio de lote.")
                Return False
            End If
            CargarDetallesLotesCreados()
            Dim IndexDgv As Integer
            Dim Subdirectorios As String() = Directory.GetDirectories(RutaLotes)
            If Subdirectorios.Length = 0 Then
                DgvCarpetas.Visible = False
            Else
                DgvCarpetas.Visible = True
            End If
            DgvCarpetas.Rows.Clear()
            For Each subdir As String In Subdirectorios
                Dim info As New DirectoryInfo(subdir)
                If UBound(LotesLeidos) > 0 Then
                    For i As Integer = 1 To UBound(LotesLeidos)
                        If LotesLeidos(i).Numlote = info.Name Then
                            IndexDgv = DgvCarpetas.Rows.Add(info.Name, LotesLeidos(i).Estado, LotesLeidos(i).Imagenes, Trim(Mid(LotesLeidos(i).EsquemaNombre, InStr(LotesLeidos(i).EsquemaNombre, "-") + 1)))
                            Exit For
                        End If
                    Next
                End If
            Next
            ' 4. Seleccionamos y enfocamos la última fila (el nuevo registro)
            Dim nuevaFilaIndex As Integer = DgvCarpetas.Rows.Count - 1
            If nuevaFilaIndex >= 0 Then
                DgvCarpetas.ClearSelection()
                DgvCarpetas.Rows(nuevaFilaIndex).Selected = True
                DgvCarpetas.FirstDisplayedScrollingRowIndex = nuevaFilaIndex
            End If

        End Function

        Private Function ValidarCampos() As Boolean
            ValidarCampos = True
            If Trim(Me.lblTipologia.Text) = "" Then
                MsgBox("Debes seleccionar el tipo de documento a digitalizar.", 64, "Campo pendiente.")
                Me.BtnNewCarpeta.Focus()
                Return False
            End If
            For Each ctrl As Control In PnCampos.Controls
                'Validaciones genericas de los campos de tipo texto 
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("Txt") Then
                    For Each fila As DataRow In DtCampos.Rows
                        If ctrl.Name = "Txt" & Trim(fila("Nombre_Campo")) Then
                            If fila("Es_Obligatorio_Campo") = True Then
                                If Trim(ctrl.Text) = "" Then
                                    MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " es obligatorio.", 64, "Campo pendiente de captura.")
                                    ctrl.Focus()
                                    Return False
                                End If
                            End If
                            If Trim(ctrl.Text) <> "" Then
                                If Len(Trim(ctrl.Text)) < fila("Length_Min_Campo") Then
                                    MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " no puede tener menos de " & fila("Length_Min_Campo") & " caracteres. Verifique.", 64, "Campo pendiente de captura.")
                                    ctrl.Focus()
                                    Return False
                                End If
                            End If
                            Exit For
                        End If
                    Next
                End If

                'Validaciones genericas de los campos de tipo numericos.
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("Txn") Then
                    For Each fila As DataRow In DtCampos.Rows
                        If ctrl.Name = "Txn" & Trim(fila("Nombre_Campo")) Then
                            If fila("Es_Obligatorio_Campo") = True Then
                                If Trim(ctrl.Text) = "" Then
                                    MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " es obligatorio.", 64, "Campo pendiente de captura.")
                                    ctrl.Focus()
                                    Return False
                                End If
                            End If
                            If Trim(ctrl.Text) <> "" Then
                                If Len(Trim(ctrl.Text)) < fila("Length_Min_Campo") Then
                                    MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " no puede tener menos de " & fila("Length_Min_Campo") & " caracteres. Verifique.", 64, "Campo pendiente de captura.")
                                    ctrl.Focus()
                                    Return False
                                End If
                            End If
                            Exit For
                        End If
                    Next
                End If

                'Validaciones genericas de los campos de tipo lista Combobox
                If TypeOf ctrl Is ComboBox AndAlso ctrl.Name.StartsWith("Cmb") Then
                    Dim cbx As ComboBox = CType(ctrl, ComboBox)
                    For Each fila As DataRow In DtCampos.Rows
                        If cbx.Name = "Cmb" & Trim(fila("Nombre_Campo")) Then
                            If fila("Es_Obligatorio_Campo") = True Then
                                If Trim(cbx.Text) = "" Then
                                    MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " es obligatorio.", 64, "Campo pendiente de captura.")
                                    cbx.Focus()
                                    Return False
                                End If
                            End If
                            If Trim(cbx.Text) <> "" Then
                                Dim Idindex As Integer = cbx.FindString(Trim(cbx.Text))
                                If Idindex >= 0 Then
                                    cbx.SelectedIndex = Idindex ' Esto actualiza .Text, .SelectedValue y .SelectedItem correctamente
                                Else
                                    cbx.Text = ""
                                    MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " no fue diligenciado correctamente. Verifique.", 64, "Campo pendiente de captura.")
                                    cbx.Focus()
                                    Return False
                                End If
                            End If
                            Exit For
                        End If
                    Next
                End If

                'Validamos la fecha, En este caso buscamos el campo de fecha de movimiento el cual no puede ser mayor a la fecha actual  
                If TypeOf ctrl Is DateTimePicker AndAlso ctrl.Name.StartsWith("Dtp") Then
                    Dim DtpFec As DateTimePicker = CType(ctrl, DateTimePicker)
                    For Each fila As DataRow In DtCampos.Rows
                        If DtpFec.Name = "Dtp" & Trim(fila("Nombre_Campo")) Then
                            If DtpFec.Value.ToString("yyyyMMdd") > Now.ToString("yyyyMMdd") Then
                                MsgBox("El campo " & Trim(fila("Detalle_Campo")) & " no puede ser mayor a la fecha de hoy. Verifique.", 64, "Campo pendiente de captura.")
                                DtpFec.Value = Now()
                                DtpFec.Focus()
                                Return False
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next

        End Function

        Private Sub MostrarImagen(RutaImg As String, NombreImg As String)
            Try
                PicImagen.Image = Nothing
                PicImagen.Image = Nothing
                PicA.Image = Nothing
                PicA.Image = Nothing
            Catch
            End Try
            Try
                'Cargar la imagen sin bloquear el archivo de imagen.
                Dim imgTemp As Image
                Using fs As New FileStream(RutaImg, FileMode.Open, FileAccess.Read)
                    imgTemp = Image.FromStream(fs)
                End Using
                'Dim Largo As Integer = imgTemp.Height
                PicImagen.Image = New Bitmap(imgTemp)
                imgTemp.Dispose()
                PicImagen.Tag = NombreImg
                PicImagen.ClientSize = New System.Drawing.Point(720, 800)
                PicImagen.SizeMode = PictureBoxSizeMode.Zoom
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            'PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(RutaImg)))
            'Try
            '    PicImagen.Image = Image.FromFile(RutaImg)
            '    PicImagen.Tag = NombreImg
            '    PicImagen.SizeMode = PictureBoxSizeMode.StretchImage
            'Catch
            'End Try
            Me.BtnEliminaPagina.Enabled = True
            PicA.Image = PicImagen.Image
        End Sub

        Private Sub CargarImagenesEnMiniatura(RutaImagenes As String)
            ' Limpiar el FlowLayoutPanel para evitar duplicados
            Try
                PicImagen.Image = Nothing
                PicImagen.Image = Nothing
            Catch
            End Try
            FLPImagenes.Controls.Clear()
            ' Verificar si la carpeta existe
            If Directory.Exists(RutaImagenes) Then
                ' Obtener todos los archivos de imagen en la carpeta
                Dim archivosImagen As String() = Directory.GetFiles(RutaImagenes, "*.*", SearchOption.TopDirectoryOnly) _
                                            .Where(Function(f) f.EndsWith(".jpg") Or f.EndsWith(".JPG") Or f.EndsWith(".TIFF") Or f.EndsWith(".tiff") Or f.EndsWith(".TIF") Or f.EndsWith(".tif")).ToArray()

                ' Recorrer todos los archivos de imagen
                Dim TImg As Integer = 0
                DgvRegistrosLote.Rows.Clear()
                For Each ArcImagen As String In archivosImagen
                    ' Moscar en relacion en GridView
                    TImg += 1
                    DgvRegistrosLote.Rows.Add(TImg, System.IO.Path.GetFileName(ArcImagen))

                    Dim img As Image = Image.FromFile(ArcImagen)
                    ' Generar la miniatura de la imagen
                    Dim thumb As Image = img.GetThumbnailImage(100, 100, Nothing, IntPtr.Zero)
                    ' Crear un PictureBox para mostrar la miniatura
                    Dim PictureMini As New PictureBox() With {
                    .Image = thumb,     '.Width = 100,        '.Height = 150,
                    .Name = System.IO.Path.GetFileName(ArcImagen),
                    .Margin = New Padding(5),
                    .Tag = System.IO.Path.GetFileName(ArcImagen),
                    .SizeMode = PictureBoxSizeMode.StretchImage}
                    AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick

                    Dim lbl As New Label() With {
                        .Text = TImg,
                        .AutoSize = False,
                        .Font = New System.Drawing.Font(.Font.FontFamily, 8, FontStyle.Bold),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .Dock = DockStyle.Bottom,
                        .Height = 10}
                    ' Agregar el PictureBox al FlowLayoutPanel
                    FLPImagenes.Controls.Add(lbl)
                    FLPImagenes.Controls.Add(PictureMini)

                    ' Liberar los recursos de la imagen original
                    img.Dispose()

                Next

                TImagen = -1
                ReDim Imagenes(0)
                Dim FilePath As String = RutaImagenes & "\" & "IndiceEscaneo.txt"
                If File.Exists(FilePath) = False Then Exit Sub
                ' Leer el archivo TXT
                Using Reader As New StreamReader(FilePath)
                    While Not Reader.EndOfStream
                        Dim campos As String() = Reader.ReadLine().Split(","c)
                        ' Dim campos As String() = linea.Split(","c)
                        ' Crear un objeto rsona y agregarlo a la lista
                        If campos.Length = 3 Then
                            TImagen += 1
                            ReDim Preserve Imagenes(TImagen)
                            Imagenes(TImagen).IdImg = TImagen + 1
                            Imagenes(TImagen).Exportada = campos(1)
                            Imagenes(TImagen).NomImagen = campos(2)
                        End If
                    End While
                End Using
            End If
            If DgvRegistrosLote.Rows.Count > 0 Then
                IdRImg = DgvRegistrosLote.Rows.Count
                MostrarImagen()
            End If
        End Sub

        Private Function RecortarBlanco(img As Bitmap) As Bitmap
            Dim minX As Integer = img.Width
            Dim minY As Integer = img.Height
            Dim maxX As Integer = 0
            Dim maxY As Integer = 0

            Dim blanco As System.Drawing.Color = System.Drawing.Color.White

            For y As Integer = 0 To img.Height - 1
                For x As Integer = 0 To img.Width - 1
                    Dim pixelColor As System.Drawing.Color = img.GetPixel(x, y)
                    If pixelColor.ToArgb() <> blanco.ToArgb() Then
                        If x < minX Then minX = x
                        If x > maxX Then maxX = x
                        If y < minY Then minY = y
                        If y > maxY Then maxY = y
                    End If
                Next
            Next

            ' Asegúrate de que se detectó algo no blanco
            If maxX > minX And maxY > minY Then
                Dim rect As New Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1)
                Return img.Clone(rect, img.PixelFormat)
            Else
                ' Imagen completamente blanca
                Return img
            End If
        End Function

        Private Function CrearMiniatura(rutaImagen As String) As Image
            'Cargar la imagen si que se presente bloque del archivo.
            Dim imagenOriginal As Image
            Using fs As New FileStream(rutaImagen, FileMode.Open, FileAccess.Read)
                imagenOriginal = New Bitmap(Image.FromStream(fs))
            End Using

            'Dim imagenOriginal As Image = Image.FromFile(rutaImagen)
            Dim tamanioMiniatura As New Size(100, 100) ' Tamaño deseado para las miniaturas
            ' Crear la miniatura manteniendo la proporción
            Dim imagenMiniatura As Image = imagenOriginal.GetThumbnailImage(tamanioMiniatura.Width, tamanioMiniatura.Height, Nothing, IntPtr.Zero)
            Return imagenMiniatura
        End Function

        Private Sub ActualizaCantidadImagenes()
            For Each fila As DataGridViewRow In DgvCarpetas.Rows
                If Not fila.IsNewRow Then ' Evita la fila nueva vacía
                    If fila.Cells(0).Value IsNot Nothing AndAlso fila.Cells(0).Value.ToString() = Mid(RutaLoteNuevo, InStrRev(RutaLoteNuevo, "\") + 1) Then
                        DgvCarpetas.Rows(fila.Index).Cells(2).Value = ParamTipologia.Imagenes
                        Exit For
                    End If
                End If
            Next



        End Sub




#End Region

#Region "Mouse Move_Leave"
        Private Sub BtnEscanear_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnEscanear.MouseMove
            Me.BtnEscanear.Size = New Size(40, 40)
        End Sub
        Private Sub BtnEscanear_MouseLeave(sender As Object, e As EventArgs) Handles BtnEscanear.MouseLeave
            Me.BtnEscanear.Size = New Size(35, 35)
        End Sub
        Private Sub BtnAddImagen_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAddImagen.MouseMove
            Me.BtnAddImagen.Size = New Size(40, 40)
        End Sub
        Private Sub BtnAddImagen_MouseLeave(sender As Object, e As EventArgs) Handles BtnAddImagen.MouseLeave
            Me.BtnAddImagen.Size = New Size(35, 35)
        End Sub

        Private Sub BtnAddCarpeta_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAddCarpeta.MouseMove
            Me.BtnAddCarpeta.Size = New Size(40, 40)
        End Sub

        Private Sub BtnAddCarpeta_MouseLeave(sender As Object, e As EventArgs) Handles BtnAddCarpeta.MouseLeave
            Me.BtnAddCarpeta.Size = New Size(35, 35)
        End Sub

        Private Sub BtnEliminaLote_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnEliminaLote.MouseMove
            Me.BtnEliminaLote.Size = New Size(40, 40)
        End Sub
        Private Sub BtnEliminaLote_MouseLeave(sender As Object, e As EventArgs) Handles BtnEliminaLote.MouseLeave
            Me.BtnEliminaLote.Size = New Size(35, 35)
        End Sub
        Private Sub BtnNewCarpeta_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnNewCarpeta.MouseMove
            Me.BtnNewCarpeta.Size = New Size(40, 40)
        End Sub
        Private Sub BtnNewCarpeta_MouseLeave(sender As Object, e As EventArgs) Handles BtnNewCarpeta.MouseLeave
            Me.BtnNewCarpeta.Size = New Size(35, 35)
        End Sub
        Private Sub BtnExportarLotes_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnExportarLotes.MouseMove
            Me.BtnExportarLotes.Size = New Size(40, 40)
        End Sub
        Private Sub BtnExportarLotes_MouseLeave(sender As Object, e As EventArgs) Handles BtnExportarLotes.MouseLeave
            Me.BtnExportarLotes.Size = New Size(35, 35)
        End Sub

        Private Sub BntPrimero_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnPrimero.MouseMove
            Me.BtnPrimero.Size = New Size(40, 35)
        End Sub
        Private Sub BntPrimero_MouseLeave(sender As Object, e As EventArgs) Handles BtnPrimero.MouseLeave
            Me.BtnPrimero.Size = New Size(30, 25)
        End Sub
        Private Sub BtnAnterior_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAnterior.MouseMove
            Me.BtnAnterior.Size = New Size(40, 35)
        End Sub
        Private Sub BtnAnterior_MouseLeave(sender As Object, e As EventArgs) Handles BtnAnterior.MouseLeave
            Me.BtnAnterior.Size = New Size(30, 25)
        End Sub
        Private Sub BtnSiguiente_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnSiguiente.MouseMove
            Me.BtnSiguiente.Size = New Size(40, 35)
        End Sub
        Private Sub BtnSiguiente_MouseLeave(sender As Object, e As EventArgs) Handles BtnSiguiente.MouseLeave
            Me.BtnSiguiente.Size = New Size(30, 25)
        End Sub
        Private Sub BtnUltimo_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnUltimo.MouseMove
            Me.BtnUltimo.Size = New Size(40, 35)
        End Sub
        Private Sub BtnUltimo_MouseLeave(sender As Object, e As EventArgs) Handles BtnUltimo.MouseLeave
            Me.BtnUltimo.Size = New Size(30, 25)
        End Sub

        Private Sub BtnGiraIzquieda_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnGiraIzquieda.MouseMove
            Me.BtnGiraIzquieda.Size = New Size(40, 35)
        End Sub
        Private Sub BtnGiraIzquieda_MouseLeave(sender As Object, e As EventArgs) Handles BtnGiraIzquieda.MouseLeave
            Me.BtnGiraIzquieda.Size = New Size(30, 25)
        End Sub

        Private Sub BntGiraDerecha_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnGiraDerecha.MouseMove
            Me.BtnGiraDerecha.Size = New Size(40, 35)
        End Sub
        Private Sub BntGiraDerecha_MouseLeave(sender As Object, e As EventArgs) Handles BtnGiraDerecha.MouseLeave
            Me.BtnGiraDerecha.Size = New Size(30, 25)
        End Sub
        Private Sub BtnNormal_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnNormal.MouseMove
            Me.BtnNormal.Size = New Size(40, 35)
        End Sub
        Private Sub BtnNormal_MouseLeave(sender As Object, e As EventArgs) Handles BtnNormal.MouseLeave
            Me.BtnNormal.Size = New Size(30, 25)
        End Sub
        Private Sub BtnAutoTamaño_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAutoTamaño.MouseMove
            Me.BtnAutoTamaño.Size = New Size(40, 35)
        End Sub
        Private Sub BtnAutoTamaño_MouseLeave(sender As Object, e As EventArgs) Handles BtnAutoTamaño.MouseLeave
            Me.BtnAutoTamaño.Size = New Size(30, 25)
        End Sub
        Private Sub BtnReducir_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnReducir.MouseMove
            Me.BtnReducir.Size = New Size(40, 35)
        End Sub
        Private Sub BtnReducir_MouseLeave(sender As Object, e As EventArgs) Handles BtnReducir.MouseLeave
            Me.BtnReducir.Size = New Size(30, 25)
        End Sub
        Private Sub BtnAmpliar_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAmpliar.MouseMove
            Me.BtnAmpliar.Size = New Size(40, 35)
        End Sub
        Private Sub BtnAmpliar_MouseLeave(sender As Object, e As EventArgs) Handles BtnAmpliar.MouseLeave
            Me.BtnAmpliar.Size = New Size(30, 25)
        End Sub

        Private Sub BtnEliminaPagina_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnEliminaPagina.MouseMove
            Me.BtnEliminaPagina.Size = New Size(40, 35)
        End Sub
        Private Sub BtnEliminaPagina_MouseLeave(sender As Object, e As EventArgs) Handles BtnEliminaPagina.MouseLeave
            Me.BtnEliminaPagina.Size = New Size(30, 25)
        End Sub
#End Region




#Region "Metodos Escaner ScanJetPro 3000 s4"

        '''' <summary>
        '''' Checks that TWAIN device manager is installed in the system.
        '''' </summary>
        'Private Function CheckTwain() As Boolean
        '    Dim isTwainAvailable As Boolean = False

        '    ' create the device manager
        '    Using deviceManager As New Vintasoft.Twain.DeviceManager()
        '        ' specify that TWAIN device manager 2.x must be used
        '        deviceManager.IsTwain2Compatible = True
        '        ' if TWAIN device manager 2.x is available
        '        If deviceManager.IsTwainAvailable Then
        '            MsgBox("TWAIN device manager 2.x is available.", 64, "TWain")
        '            'System.Console.WriteLine("TWAIN device manager 2.x is available.")
        '            isTwainAvailable = True
        '        End If

        '        ' specify that TWAIN device manager 1.x must be used
        '        deviceManager.IsTwain2Compatible = False
        '        ' if TWAIN device manager 1.x is available
        '        If deviceManager.IsTwainAvailable Then
        '            MsgBox("TWAIN device manager 1.x is available.", 64, "TWain")
        '            'System.Console.WriteLine("TWAIN device manager 1.x is available.")
        '            isTwainAvailable = True
        '        End If

        '        ' if TWAIN device manager is NOT available
        '        If Not isTwainAvailable Then
        '            MsgBox("TWAIN device manager is NOT available.", 64, "TWain")
        '            'System.Console.WriteLine("TWAIN device manager is NOT available.")
        '        End If
        '    End Using
        '    Return isTwainAvailable
        'End Function


        ''' <summary>
        ''' Starts the image acquisition from scanner.
        ''' </summary>
        Private Sub StartImageAcquisitionButton()

            Dim device As Vintasoft.Twain.Device = Nothing
            Try
                ''Select the Default device
                ' _deviceManager.ShowDefaultDeviceSelectionDialog()

                ' get reference to the default device
                device = _deviceManager.DefaultDevice

                ' if device is not found
                If device Is Nothing Then
                    Exit Sub
                End If
                ' subscribe to the device events
                SubscribeToDeviceEvents(device)
                ' set Memory transfer mode
                device.TransferMode = Vintasoft.Twain.TransferMode.Memory
                device.ShowUI = False
                device.ShowIndicators = False
                device.Open()

                ' Seleccionar fuente de alimentación automática (ADF)
                If device.HasFeeder Then
                    device.DocumentFeeder.Enabled = True ' Activar el alimentador automático
                    If ParamTipologia.Duplex = 1 Or ParamTipologia.Duplex = True Then
                        If device.DocumentFeeder.DuplexMode <> Vintasoft.Twain.DuplexMode.None Then
                            device.DocumentFeeder.DuplexEnabled = True ' Activar escaneo a doble cara si está disponible
                        End If
                    End If
                End If

                Select Case ParamTipologia.TipoImagen
                    Case 1 : device.PixelType = Vintasoft.Twain.PixelType.Gray
                    Case 2
                        device.PixelType = Vintasoft.Twain.PixelType.RGB
                        device.UnitOfMeasure = Vintasoft.Twain.UnitOfMeasure.Inches
                        device.Resolution = New Vintasoft.Twain.Resolution(300.0F, 200.0F)
                    'device.Resolution = New Vintasoft.Twain.Resolution(ParamTipologia.ResolucionX, ParamTipologia.ResolucionY)
                    Case 4 : device.PixelType = Vintasoft.Twain.PixelType.BW
                        device.Threshold = 128
                    Case Else : device.PixelType = Vintasoft.Twain.PixelType.BW
                End Select

                ' set image layout (get only the top half of the page)
                'device.UnitOfMeasure = Vintasoft.Twain.UnitOfMeasure.Inches
                'Dim imageLayout As Vintasoft.Primitives.VintasoftRectF = device.ImageLayout.[Get]()
                'device.ImageLayout.[Set](0, 0, imageLayout.Width, imageLayout.Height / 2)

                'device.Contrast = ParamTipologia.Contraste
                'device.PageSize = PageSize.USLETTER
                Try
                    device.PageAutoSize = PageAutoSize.Auto
                Catch
                End Try
                Try
                    device.PageOrientation = PageOrientation.Auto
                Catch
                End Try
                '' Si no deseas bordes blancos adicionales, puedes intentar establecer un área de escaneo personalizada


                device.Acquire()
            Catch ex As Vintasoft.Twain.TwainException
                ' if device is found
                If device IsNot Nothing Then
                    ' if device is opened
                    If device.State >= Vintasoft.Twain.DeviceState.Opened Then
                        ' close the device
                        device.Close()
                    End If
                    ' unsubscribe from device events
                    UnsubscribeFromDeviceEvents(device)
                End If

                MsgBox(ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Image acquisition is in progress.
        ''' </summary>
        Private Sub Device_ImageAcquiringProgress(ByVal sender As Object, ByVal e As Vintasoft.Twain.ImageAcquiringProgressEventArgs)
            ' update progress bar
            'TSPBproceso.Value = e.Progress
        End Sub

        ''' <summary>
        ''' Image is acquired.
        ''' </summary>
        Private Sub Device_ImageAcquired(ByVal sender As Object, ByVal e As Vintasoft.Twain.ImageAcquiredEventArgs)
            ' dispose image stored in the picture box
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
            End If
            Me.BtnEliminaPagina.Enabled = True
            Dim Extension As String
            Select Case ParamTipologia.FormatoSalida
                Case "TIFF" : Extension = "tif"
                Case "JPEG" : Extension = "jpg"
                Case "PNG" : Extension = "png"
                Case Else : Extension = "tif"
            End Select
            Dim NuevaImagen As String = ParamTipologia.Numlote & (Val(ParamTipologia.NumImgen) + 1).ToString("000") & "." & Extension
            e.Image.Save(IO.Path.Combine(ParamTipologia.RutaLote, NuevaImagen))
            e.Image.Dispose()
            'VERIFICAR PESO DE IMAGEN 
            Dim infoArchivo As New FileInfo(ParamTipologia.RutaLote & "\" & NuevaImagen)
            If infoArchivo.Length < ParamTipologia.TamanoHojaBlanca Then
                MsgBox("Cuidado... Imagen posiblemente en blanco... Verifique.", 64, "Cuidado.. Valide.")
            End If

            ParamTipologia.NumImgen += 1
            ParamTipologia.Imagenes += 1
            'PicImagen.Image = RecortarBlanco(System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ParamTipologia.RutaLote & "\" & NuevaImagen))))

            Me.PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ParamTipologia.RutaLote & "\" & NuevaImagen)))

            Me.PicImagen.Tag = NuevaImagen
            Me.PicImagen.SizeMode = PictureBoxSizeMode.StretchImage
            'Me.DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
            TImagen += 1
            ReDim Preserve Imagenes(TImagen)
            Imagenes(TImagen).IdImg = TImagen + 1
            Imagenes(TImagen).Exportada = 0
            Imagenes(TImagen).NomImagen = NuevaImagen
            Dim lbl As New Label() With {
                        .Text = ParamTipologia.Imagenes,
                        .AutoSize = False,
                        .Font = New System.Drawing.Font(.Font.FontFamily, 8, FontStyle.Bold),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .Dock = DockStyle.Bottom,
                        .Height = 10}

            Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
            Dim PictureMini As New PictureBox With {
            .Image = miniatura,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Name = NuevaImagen,
            .Tag = NuevaImagen,
            .Margin = New Padding(5)}
            AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick

            If Me.InvokeRequired Then
                Me.Invoke(Sub()
                              Me.DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
                              Me.FLPImagenes.Controls.Add(lbl)
                              Me.FLPImagenes.Controls.Add(PictureMini)
                          End Sub)
            Else
                Me.DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
                Me.FLPImagenes.Controls.Add(lbl)
                Me.FLPImagenes.Controls.Add(PictureMini)
            End If
            IdRImg = DgvRegistrosLote.Rows.Count

            ActualizaCantidadImagenes()
        End Sub

        ''' <summary>
        ''' Scan is completed.
        ''' </summary>
        Private Sub Device_ScanCompleted(ByVal sender As Object, ByVal e As EventArgs)
            ' MessageBox.Show("Scan is competed.")
        End Sub

        ''' <summary>
        ''' Scan is canceled.
        ''' </summary>
        Private Sub Device_ScanCanceled(ByVal sender As Object, ByVal e As EventArgs)
            ' MessageBox.Show("Scan is canceled.")
        End Sub

        ''' <summary>
        ''' Scan is failed.
        ''' </summary>
        Private Sub Device_ScanFailed(ByVal sender As Object, ByVal e As Vintasoft.Twain.ScanFailedEventArgs)
            ' MessageBox.Show(String.Format("Scan is failed: {0}", e.ErrorString))
        End Sub

        ''' <summary>
        ''' User interface of device is closed.
        ''' </summary>
        Private Sub Device_UserInterfaceClosed(ByVal sender As Object, ByVal e As EventArgs)
            ' MessageBox.Show("User Interface is closed.")
        End Sub

        ''' <summary>
        ''' Scan is finished.
        ''' </summary>
        Private Sub Device_ScanFinished(ByVal sender As Object, ByVal e As EventArgs)
            Dim device1 As Vintasoft.Twain.Device = DirectCast(sender, Vintasoft.Twain.Device)
            ' unsubscribe from device events
            UnsubscribeFromDeviceEvents(device1)

            ' if device is not closed
            If device1.State <> Vintasoft.Twain.DeviceState.Closed Then
                ' close the device
                device1.Close()
            End If

            If _deviceManager IsNot Nothing Then
                Try : _deviceManager.Close() : Catch : End Try
            End If

            GuardarInformacionLote()
            MessageBox.Show("Escaneo Finalizado.")
        End Sub

        ''' <summary>
        ''' Subscribes to the device events.
        ''' </summary>
        Private Sub SubscribeToDeviceEvents(ByVal device As Vintasoft.Twain.Device)
            AddHandler device.ImageAcquiringProgress, New EventHandler(Of Vintasoft.Twain.ImageAcquiringProgressEventArgs)(AddressOf Device_ImageAcquiringProgress)
            AddHandler device.ImageAcquired, New EventHandler(Of Vintasoft.Twain.ImageAcquiredEventArgs)(AddressOf Device_ImageAcquired)
            AddHandler device.ScanCompleted, New EventHandler(AddressOf Device_ScanCompleted)
            AddHandler device.ScanCanceled, New EventHandler(AddressOf Device_ScanCanceled)
            AddHandler device.ScanFailed, New EventHandler(Of Vintasoft.Twain.ScanFailedEventArgs)(AddressOf Device_ScanFailed)
            AddHandler device.UserInterfaceClosed, New EventHandler(AddressOf Device_UserInterfaceClosed)
            AddHandler device.ScanFinished, New EventHandler(AddressOf Device_ScanFinished)
        End Sub

        ''' <summary>
        ''' Unsubscribes from the device events.
        ''' </summary>
        Private Sub UnsubscribeFromDeviceEvents(ByVal device As Vintasoft.Twain.Device)
            RemoveHandler device.ImageAcquiringProgress, New EventHandler(Of Vintasoft.Twain.ImageAcquiringProgressEventArgs)(AddressOf Device_ImageAcquiringProgress)
            RemoveHandler device.ImageAcquired, New EventHandler(Of Vintasoft.Twain.ImageAcquiredEventArgs)(AddressOf Device_ImageAcquired)
            RemoveHandler device.ScanCompleted, New EventHandler(AddressOf Device_ScanCompleted)
            RemoveHandler device.ScanCanceled, New EventHandler(AddressOf Device_ScanCanceled)
            RemoveHandler device.ScanFailed, New EventHandler(Of Vintasoft.Twain.ScanFailedEventArgs)(AddressOf Device_ScanFailed)
            RemoveHandler device.UserInterfaceClosed, New EventHandler(AddressOf Device_UserInterfaceClosed)
            RemoveHandler device.ScanFinished, New EventHandler(AddressOf Device_ScanFinished)
        End Sub


#End Region

#Region "Otra forma de escanear"
        '        Public Shared Sub AcquireImagesFromTwainDeviceAndProcessImages()
        '            Try
        '                ' create the device manager
        '                Using deviceManager As New Vintasoft.Twain.DeviceManager()
        '                    ' open the device manager
        '                    deviceManager.Open()

        '                    ' select the device in the default device selectio ndialog
        '                    deviceManager.ShowDefaultDeviceSelectionDialog()

        '                    ' get reference to the selected device
        '                    Dim device As Vintasoft.Twain.Device = deviceManager.DefaultDevice

        '                    ' specify that device UI must not be used
        '                    device.ShowUI = False
        '                    ' specify that device must be closed after scan
        '                    device.DisableAfterAcquire = True

        '                    Dim tiffFilename As String = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "multipage.tif")

        '                    ' acquire images from device
        '                    Dim acquireModalState As Vintasoft.Twain.AcquireModalState = Vintasoft.Twain.AcquireModalState.None
        '                    Dim acquiredImage As Vintasoft.Twain.AcquiredImage
        '                    Do
        '                        acquireModalState = device.AcquireModal()
        '                        Select Case acquireModalState
        '                            Case Vintasoft.Twain.AcquireModalState.ImageAcquired
        '                                ' get reference to the image acquired from device
        '                                acquiredImage = device.AcquiredImage

        '                                ' despeckle/deskew/detect border
        '                                ProcessAcquiredImage(acquiredImage)
        '                                ' add image to multipage TIFF file if image is not blank
        '                                If Not acquiredImage.IsBlank(0.01F) Then
        '                                    acquiredImage.Save(tiffFilename)
        '                                End If

        '                                ' dispose acquired image
        '                                acquiredImage.Dispose()
        '                                Exit Select
        '                        End Select
        '                    Loop While acquireModalState <> Vintasoft.Twain.AcquireModalState.None

        '                    ' close the device
        '                    device.Close()

        '                    ' close the device manager
        '                    deviceManager.Close()
        '                End Using
        '            Catch ex As Exception
        '                MsgBox(Err.Description)
        '            End Try
        '        End Sub

        '        Private Shared Sub ProcessAcquiredImage(acquiredImage As Vintasoft.Twain.AcquiredImage)
        '            System.Console.WriteLine(String.Format("Image ({0})", acquiredImage.ImageInfo))

        '            Try
        '                ' subscribe to processing events
        '                AddHandler acquiredImage.Processing, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingEventArgs)(AddressOf AcquiredImage_Processing)
        '                AddHandler acquiredImage.Progress, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingProgressEventArgs)(AddressOf AcquiredImage_Progress)
        '                AddHandler acquiredImage.Processed, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessedEventArgs)(AddressOf AcquiredImage_Processed)

        '                ' despeckle/deskew/detect border
        '                acquiredImage.Despeckle(8, 25, 30, 400)
        '                acquiredImage.Deskew(Vintasoft.Twain.ImageProcessing.TwainBorderColor.AutoDetect, 5, 5)
        '                acquiredImage.DetectBorder(5)
        '            Catch ex As System.Exception
        '                'System.Console.WriteLine("Error: " & ex.Message)
        '                MsgBox(ex.Message)
        '            Finally
        '                ' unsubscribe from processing events
        '                RemoveHandler acquiredImage.Processing, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingEventArgs)(AddressOf AcquiredImage_Processing)
        '                RemoveHandler acquiredImage.Progress, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingProgressEventArgs)(AddressOf AcquiredImage_Progress)
        '                RemoveHandler acquiredImage.Processed, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessedEventArgs)(AddressOf AcquiredImage_Processed)
        '            End Try
        '        End Sub
        '        Private Shared Sub AcquiredImage_Processing(sender As Object, e As Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingEventArgs)
        '            ' System.Console.Write(e.Action.ToString() & " ")
        '        End Sub
        '        Private Shared Sub AcquiredImage_Progress(sender As Object, e As Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingProgressEventArgs)
        '            ' System.Console.Write(".")
        '        End Sub
        '        Private Shared Sub AcquiredImage_Processed(sender As Object, e As Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessedEventArgs)
        '            ' System.Console.WriteLine(" finished")
        '            MsgBox(" finished")
        '        End Sub
#End Region

#Region "Cargue de Imagenes fisicas"

        Private Sub BtnAddImagen_Click(sender As Object, e As EventArgs) Handles BtnAddImagen.Click
            Dim openFileDialog As New OpenFileDialog() With {
        .Title = "Seleccione archivo a adiccionar.",
        .Multiselect = False,
        .Filter = FiltroExt}  ' "Todos los archivos (*.*)|*.*"}
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim ArcSeleccionado As String = openFileDialog.FileName
                If Not EsExtensionPermitida(ArcSeleccionado, SetExt) Then
                    MessageBox.Show("El archivo seleccionado no está en la lista de tipos permitidos para este proceso.")
                    Exit Sub
                End If
                'Validar si es un archivo o una carpeta lo que se selecciono.
                Dim Extension As String = Mid(ArcSeleccionado, InStrRev(ArcSeleccionado, ".") + 1)
                Select Case Extension.ToLower
                    Case "png"
                        CargarArchivoPng(ArcSeleccionado)
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case "tif"
                        'Verificar si es un multitiff
                        Dim imagenOriginal As Image = Image.FromFile(ArcSeleccionado)
                        ' Obtener la dimensión de frames (páginas)
                        Dim dimension As New FrameDimension(imagenOriginal.FrameDimensionsList(0))
                        Dim numPaginas As Integer = imagenOriginal.GetFrameCount(dimension)
                        ' Carpeta destino (puedes personalizar esto con SaveFileDialog si quieres)
                        'Dim carpetaDestino As String = System.IO.Path.GetDirectoryName(ArcSeleccionado)
                        If numPaginas > 1 Then
                            CargarArchivoTiff(ArcSeleccionado)
                        Else
                            CargarArchivoPng(ArcSeleccionado)
                        End If
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case "jpg"
                        CargarArchivoPng(ArcSeleccionado)
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case "tiff"
                        CargarArchivoTiff(ArcSeleccionado)
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case "pdf"
                        CargarArchivo_Pdf(ArcSeleccionado, "")
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case "doc", "docx"
                        CargarArchivo_doc(ArcSeleccionado)
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case "xls", "xlsx"
                        CargarArchivo_Xls(ArcSeleccionado)
                        IdRImg = DgvRegistrosLote.Rows.Count
                        'Case "ppt", "pptx"
                        'CargarArchivo_Ppt(ArcSeleccionado)
                        'IdRImg = DgvRegistrosLote.Rows.Count
                    Case Else
                        MsgBox("Archivo seleccionado no valido para este proceso.." & Chr(13) &
                        "(" & FiltroExt & ")", 64, "Error en formato de archivo.")
                        Exit Sub
                End Select
            End If
        End Sub

        Private Sub CargarArchivoPng(ArchivoImagen As String)
            Dim Extension As String
            Select Case ParamTipologia.FormatoSalida
                Case "TIFF" : Extension = "tif"
                Case "JPEG" : Extension = "jpg"
                Case "PNG" : Extension = "png"
                Case Else : Extension = "tif"
            End Select
            Dim NuevaImagen As String = ""
            NuevaImagen = Path.GetFileName(ArchivoImagen)
            NuevaImagen &= "_001." & Extension
            'Dim NuevaImagen As String = ParamTipologia.Numlote & (Val(ParamTipologia.NumImgen) + 1).ToString("000") & "." & Extension

            If ControlCargueImagen(ArchivoImagen, NuevaImagen, 1, True) = True Then
                MsgBox("Archivo de imagen " & ArchivoImagen & " ya se habia cargado... Verifique..")
                Exit Sub
            End If
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
            End If
            Me.BtnEliminaPagina.Enabled = True

            'Dim imgOriginal As Image = Image.FromFile(ArchivoImagen)
            'Dim imgGris As Bitmap = ConvertirAEscalaGrises(imgOriginal)
            'PicImagen.Image = imgGris

            Dim bmpOriginal As Bitmap
            bmpOriginal = Image.FromFile(ArchivoImagen)
            PicImagen.Image = Image.FromFile(ArchivoImagen)
            If EsImagenColor(PicImagen.Image) = True Then
                Dim prepared As Bitmap = PrepareImage(bmpOriginal)
                PicImagen.Image = ConvertToGrayscaleFast(prepared)
            End If

            'PicImagen.Image = Image.FromFile(ArchivoImagen)

            Select Case ParamTipologia.FormatoSalida
                Case "TIFF"
                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Tiff)
                Case "JPEG"
                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Jpeg)
                Case "PNG"
                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Png)
                Case Else
                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Tiff)
            End Select
            ParamTipologia.NumImgen += 1
            ParamTipologia.Imagenes += 1
            MostrarImagen(ParamTipologia.RutaLote & "\" & NuevaImagen, NuevaImagen)

            DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
            TImagen += 1
            ReDim Preserve Imagenes(TImagen)
            Imagenes(TImagen).IdImg = TImagen + 1
            Imagenes(TImagen).Exportada = 0
            Imagenes(TImagen).NomImagen = NuevaImagen

            Dim lbl As New Label() With {
                        .Text = ParamTipologia.Imagenes,
                        .AutoSize = False,
                        .Font = New System.Drawing.Font(.Font.FontFamily, 8, FontStyle.Bold),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .Dock = DockStyle.Bottom,
                        .Height = 10}

            Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
            Dim PictureMini As New PictureBox With {
            .Image = miniatura,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Name = NuevaImagen,
            .Tag = NuevaImagen,
            .Margin = New Padding(5)}
            AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
            FLPImagenes.Controls.Add(lbl)
            FLPImagenes.Controls.Add(PictureMini)
            ControlCargueImagen(ArchivoImagen, NuevaImagen, 1, False)

            ActualizaCantidadImagenes()
            GuardarInformacionLote()
        End Sub
        Private Function ConvertirAEscalaGrises(ByVal imagenColor As Image) As Bitmap
            Dim bmpGrises As New Bitmap(imagenColor.Width, imagenColor.Height)
            Using g As Graphics = Graphics.FromImage(bmpGrises)
                Dim colorMatrix As New Imaging.ColorMatrix(New Single()() {
            New Single() {0.3, 0.3, 0.3, 0, 0},
            New Single() {0.59, 0.59, 0.59, 0, 0},
            New Single() {0.11, 0.11, 0.11, 0, 0},
            New Single() {0, 0, 0, 1, 0},
            New Single() {0, 0, 0, 0, 1}
        })
                Dim atributos As New Imaging.ImageAttributes()
                atributos.SetColorMatrix(colorMatrix)
                g.DrawImage(imagenColor, New Rectangle(0, 0, imagenColor.Width, imagenColor.Height), 0, 0, imagenColor.Width, imagenColor.Height, GraphicsUnit.Pixel, atributos)
            End Using
            Return bmpGrises
        End Function

        Private Sub CargarArchivoTiff(ArchivoTiff As String)
            If ControlCargueImagen(ArchivoTiff, "PDF " & ParamTipologia.Numlote, 1, True) = True Then
                MsgBox("Archivo de imagen " & ArchivoTiff & " ya esta cargado... Verifique.")
                Exit Sub
            End If
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
            End If
            Dim NuevaImagen As String = ""
            Dim imagenOriginal As Image = Image.FromFile(ArchivoTiff)
            ' Obtener la dimensión de frames (páginas)
            Dim dimension As New FrameDimension(imagenOriginal.FrameDimensionsList(0))
            Dim numPaginas As Integer = imagenOriginal.GetFrameCount(dimension)
            Dim Extension As String = "tiff"
            lblC_pdf.Text = "0/" & numPaginas.ToString.Trim
            lblC_pdf.Visible = True
            lblC_pdf.Refresh()
            For i As Integer = 0 To numPaginas - 1
                lblC_pdf.Text = i.ToString.Trim & "/" & numPaginas.ToString.Trim
                lblC_pdf.Refresh()
                ' NuevaImagen = Path.GetFileNameWithoutExtension(ArchivoTiff)
                'NuevaImagen &= "_" & (i + 1).ToString("000") & "." & Extension
                NuevaImagen = Path.GetFileName(ArchivoTiff)
                NuevaImagen &= "_" & (i + 1).ToString("000") & "." & Extension

                'NuevaImagen = ParamTipologia.Numlote & (Val(ParamTipologia.NumImgen) + 1).ToString("000") & "." & Extension
                imagenOriginal.SelectActiveFrame(dimension, i)

                Dim bmpOriginal As New Bitmap(imagenOriginal)
                Dim bitmapPagina As New Bitmap(imagenOriginal)
                If EsImagenColor(bmpOriginal) = True Then
                    Dim prepared As Bitmap = PrepareImage(bmpOriginal)
                    bitmapPagina = ConvertToGrayscaleFast(prepared)
                End If

                ' Guardar cada página como TIFF individual
                bitmapPagina.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Tiff)
                bitmapPagina.Dispose()
                Dim NewNomImg As String = Replace(NuevaImagen, ".tiff", ".tif")
                My.Computer.FileSystem.RenameFile(ParamTipologia.RutaLote & "\" & NuevaImagen, NewNomImg)
                NuevaImagen = Replace(NuevaImagen, ".tiff", ".tif")
                ParamTipologia.NumImgen += 1
                ParamTipologia.Imagenes += 1

                DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
                TImagen += 1
                ReDim Preserve Imagenes(TImagen)
                Imagenes(TImagen).IdImg = TImagen + 1
                Imagenes(TImagen).Exportada = 0
                Imagenes(TImagen).NomImagen = NuevaImagen

                Dim lbl As New Label() With {
                        .Text = ParamTipologia.Imagenes,
                        .AutoSize = False,
                        .Font = New System.Drawing.Font(.Font.FontFamily, 8, FontStyle.Bold),
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .Dock = DockStyle.Bottom,
                        .Height = 10}

                Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
                Dim PictureMini As New PictureBox With {
                .Image = miniatura,
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Margin = New Padding(5),
                .Tag = NuevaImagen,
                .Name = NuevaImagen}
                AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
                FLPImagenes.Controls.Add(lbl)
                FLPImagenes.Controls.Add(PictureMini)
                ControlCargueImagen(ArchivoTiff, NuevaImagen, 1, False)
            Next
            ActualizaCantidadImagenes()
            GuardarInformacionLote()
            lblC_pdf.Visible = False
            MostrarImagen(ParamTipologia.RutaLote & "\" & NuevaImagen, NuevaImagen)
            imagenOriginal.Dispose()
        End Sub

        Private Function ContieneColor(ByVal bmp As Bitmap) As Boolean
            ' Normaliza a un formato “lineal” para lectura rápida
            Dim work As Bitmap = bmp
            Dim needsClone As Boolean =
        (bmp.PixelFormat <> PixelFormat.Format24bppRgb AndAlso
         bmp.PixelFormat <> PixelFormat.Format32bppArgb AndAlso
         bmp.PixelFormat <> PixelFormat.Format32bppRgb)

            If needsClone Then
                work = New Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb)
                Using g = Graphics.FromImage(work)
                    g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height)
                End Using
            End If

            Dim rect = New Rectangle(0, 0, work.Width, work.Height)
            Dim data = work.LockBits(rect, ImageLockMode.ReadOnly, work.PixelFormat)
            Try
                Dim bpp As Integer = Image.GetPixelFormatSize(work.PixelFormat) \ 8
                Dim stride As Integer = data.Stride
                Dim ptr As IntPtr = data.Scan0
                Dim bytes As Integer = stride * work.Height
                Dim buffer(bytes - 1) As Byte
                Marshal.Copy(ptr, buffer, 0, bytes)

                ' Recorrido fila por fila
                For y = 0 To work.Height - 1
                    Dim rowIndex = y * stride
                    For x = 0 To work.Width - 1
                        Dim px = rowIndex + x * bpp
                        ' Orden GDI+ por defecto en 24/32bpp: B, G, R, (A)
                        Dim b As Byte = buffer(px + 0)
                        Dim g As Byte = buffer(px + 1)
                        Dim r As Byte = buffer(px + 2)

                        If Not (r = g AndAlso g = b) Then
                            Return True ' Encontramos color
                        End If
                    Next
                Next
                Return False ' Nunca encontramos píxel con color
            Finally
                work.UnlockBits(data)
                If needsClone Then work.Dispose()
            End Try
        End Function

        Private Function EsImagenColor(bmp As Bitmap) As Boolean
            For y As Integer = 0 To bmp.Height - 1
                For x As Integer = 0 To bmp.Width - 1
                    Dim pixel As System.Drawing.Color = bmp.GetPixel(x, y)
                    If pixel.R <> pixel.G OrElse pixel.G <> pixel.B Then
                        Return True ' Al menos un píxel tiene color
                    End If
                Next
            Next
            Return False ' Todos los píxeles son en escala de grises
        End Function

        Private Function PrepareImage(original As Bitmap) As Bitmap
            Dim bmp As New Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(System.Drawing.Color.White)
                g.DrawImage(original, 0, 0, original.Width, original.Height)
            End Using
            Return bmp
        End Function

        Private Function ConvertToGrayscaleFast(original As Bitmap) As Bitmap
            Dim grayBitmap As New Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb)

            Dim rect As New Rectangle(0, 0, original.Width, original.Height)
            Dim originalData As BitmapData = original.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
            Dim grayData As BitmapData = grayBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb)

            Dim bytes As Integer = Math.Abs(originalData.Stride) * original.Height
            Dim pixelBuffer(bytes - 1) As Byte
            Dim resultBuffer(bytes - 1) As Byte

            Marshal.Copy(originalData.Scan0, pixelBuffer, 0, bytes)
            original.UnlockBits(originalData)

            For k As Integer = 0 To pixelBuffer.Length - 3 Step 3
                Dim blue As Byte = pixelBuffer(k)
                Dim green As Byte = pixelBuffer(k + 1)
                Dim red As Byte = pixelBuffer(k + 2)

                Dim gray As Byte = CByte(red * 0.3 + green * 0.59 + blue * 0.11)

                resultBuffer(k) = gray
                resultBuffer(k + 1) = gray
                resultBuffer(k + 2) = gray
            Next

            Marshal.Copy(resultBuffer, 0, grayData.Scan0, bytes)
            grayBitmap.UnlockBits(grayData)

            Return grayBitmap
        End Function


        Private Sub CargarArchivo_Pdf(ArchivoPdf As String, NombreOriginal As String)
            If ControlCargueImagen(ArchivoPdf, "PDF " & ParamTipologia.Numlote, 1, True) = True Then
                MsgBox("Archivo de imagen " & ArchivoPdf & " ya esta cargado... Verifique.")
                Exit Sub
            End If
            Dim Extension As String
            Select Case ParamTipologia.FormatoSalida
                Case "TIFF" : Extension = "tif"
                Case "JPEG" : Extension = "jpg"
                Case "PNG" : Extension = "png"
                Case Else : Extension = "tif"
            End Select
            Dim ci As Integer = 0
            Dim NuevaImagen As String = ""
            '************************************************************************************************************
            ' --- Preparación de ruta y Ghostscript ---
            Dim pdfPathUnix = ArchivoPdf.Replace("\", "/")
            Dim vInfo = GsNativeLoader.GetGsVersionInfo64(AppDomain.CurrentDomain.BaseDirectory)

            ' --- Paso previo: asegurar que Ghostscript pueda leer (sanear si hace falta) ---
            Dim pdfParaAbrir As String = Nothing
            Dim paginasDetectadas As Integer = 0
            Dim log As String = Nothing

            If Not EnsureGhostscriptReadable(pdfPathUnix, vInfo, pdfParaAbrir, paginasDetectadas, log) Then
                MsgBox("No fue posible abrir el PDF con Ghostscript. " & If(log, ""))
                Exit Sub
            End If
            Using Rasterizer As New Ghostscript.NET.Rasterizer.GhostscriptRasterizer()
                ' Mantengo tu switch. Si prefieres, comenta esto y habilita PDFSTOPONERROR arriba o aquí.
                'Rasterizer.CustomSwitches.Add("-dNOSAFER")
                'Rasterizer.CustomSwitches.Add("-dSAFER")
                'Rasterizer.CustomSwitches.Add("-dALLOWPSTRANSPARENCY")
                'Rasterizer.CustomSwitches.Add("-dPDFSTOPONERROR")
                'Rasterizer.CustomSwitches.Add("-dPDFSETTINGS=/prepress")
                'Rasterizer.CustomSwitches.Add("-dUseCropBox")
                Rasterizer.CustomSwitches.Add("-dNOSAFER")
                Try
                    Rasterizer.Open(pdfParaAbrir, vInfo, False)
                Catch ex As Exception
                    MsgError = ex.Message
                    Throw
                End Try
                Try
                    lblC_pdf.Visible = True
                    ci = Rasterizer.PageCount
                    lblC_pdf.Text = "0/" & ci.ToString.Trim
                    lblC_pdf.Refresh()
                    For folio As Integer = 1 To Rasterizer.PageCount
                        Dim paginaImg As Bitmap = Rasterizer.GetPage(300, folio)
                        If paginaImg Is Nothing Then
                            paginaImg = Rasterizer.GetPage(200, folio)
                        End If
                        If PicImagen.Image IsNot Nothing Then
                            PicImagen.Image.Dispose()
                            PicImagen.Image = Nothing
                        End If
                        lblC_pdf.Text = folio.ToString.Trim & "/" & ci.ToString.Trim
                        lblC_pdf.Refresh()
                        If paginaImg Is Nothing Then
                            MsgError = "No fue posible extraer imagenes del pdf."
                            Throw New Exception(MsgError)
                        Else
                            If ContieneColor(paginaImg) = True Then
                                Dim ms As New MemoryStream(ImageToGrayscaleJpegBytes(paginaImg, 90))
                                PicImagen.Image = Image.FromStream(ms)
                            Else
                                PicImagen.Image = paginaImg
                            End If
                        End If
                        Try
                            'NuevaImagen = Path.GetFileNameWithoutExtension(ArchivoPdf)
                            'NuevaImagen &= "_" & (folio).ToString("000") & "." & Extension
                            If Trim(NombreOriginal) = "" Then
                                NuevaImagen = Path.GetFileName(ArchivoPdf)
                            Else
                                NuevaImagen = Path.GetFileName(NombreOriginal)
                            End If
                            NuevaImagen &= "_" & (folio).ToString("000") & "." & Extension
                            'NuevaImagen = ParamTipologia.Numlote & (Val(ParamTipologia.NumImgen) + 1).ToString("000") & "." & Extension
                            Me.BtnEliminaPagina.Enabled = True
                            Select Case ParamTipologia.FormatoSalida
                                Case "TIFF"
                                    Using bmp As New Bitmap(PicImagen.Image.Width, PicImagen.Image.Height, PixelFormat.Format24bppRgb)
                                        Using g = Graphics.FromImage(bmp)
                                            g.DrawImage(PicImagen.Image, 0, 0, bmp.Width, bmp.Height)
                                        End Using
                                        Dim codecTiff = ImageCodecInfo.GetImageEncoders().
                                                        FirstOrDefault(Function(c) c.MimeType = "image/tiff")
                                        If codecTiff Is Nothing Then
                                            ' MessageBox.Show("Codec TIFF no disponible en el sistema.")
                                            Exit Sub
                                        End If
                                        Dim encParams As New EncoderParameters(1)
                                        encParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionLZW))
                                        bmp.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, codecTiff, encParams)
                                    End Using
                                   ' PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Tiff)
                                Case "JPEG"
                                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Jpeg)
                                Case "PNG"
                                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Png)
                                Case Else
                                    PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & NuevaImagen, ImageFormat.Tiff)
                            End Select
                            ParamTipologia.NumImgen += 1
                            ParamTipologia.Imagenes += 1

                            DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
                            TImagen += 1
                            ReDim Preserve Imagenes(TImagen)
                            Imagenes(TImagen).IdImg = TImagen + 1
                            Imagenes(TImagen).Exportada = 0
                            Imagenes(TImagen).NomImagen = NuevaImagen

                            Dim lbl As New Label() With {
                                .Text = ParamTipologia.Imagenes,
                                .AutoSize = False,
                                .Font = New System.Drawing.Font(.Font.FontFamily, 8, FontStyle.Bold),
                                .TextAlign = ContentAlignment.MiddleLeft,
                                .Dock = DockStyle.Bottom,
                                .Height = 10}

                            Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
                            Dim PictureMini As New PictureBox With {
                                .Image = miniatura,
                                .SizeMode = PictureBoxSizeMode.StretchImage,
                                .Name = NuevaImagen,
                                .Tag = NuevaImagen,
                                .Margin = New Padding(5)}
                            AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
                            FLPImagenes.Controls.Add(lbl)
                            FLPImagenes.Controls.Add(PictureMini)
                        Catch ex As Exception
                            MsgBox(Err.Description)
                        End Try
                        paginaImg.Dispose()
                    Next
                Catch ex As Exception
                    MsgError = "No fue posible extraer imagenes del pdf."
                    Throw New Exception(MsgError)
                Finally
                    lblC_pdf.Visible = False
                End Try
            End Using
            If Trim(NuevaImagen) <> "" Then
                MostrarImagen(ParamTipologia.RutaLote & "\" & NuevaImagen, NuevaImagen)
            End If
            ActualizaCantidadImagenes()
            GuardarInformacionLote()
            ControlCargueImagen(ArchivoPdf, "PDF " & ParamTipologia.Numlote, 1, False)

        End Sub
        ' --- Orquestador: decidir si sanear y devolver ruta final + páginas ---
        Private Function EnsureGhostscriptReadable(originalPath As String,
                                   vInfo As GhostscriptVersionInfo,
                                   ByRef finalPath As String,
                                   ByRef pages As Integer,
                                   ByRef log As String) As Boolean
            Dim msgs As New List(Of String)
            finalPath = originalPath
            pages = 0
            ' A) Rápidas
            If IsClearlyBroken(originalPath) Then
                msgs.Add("Archivo ausente, vacío o sin cabecera PDF válida.")
                ' Intentar sanear directo
                Dim fixedPath = Path.Combine(Path.GetDirectoryName(originalPath),
                             Path.GetFileNameWithoutExtension(originalPath) & "_fixed.pdf")
                Try
                    Dim n = SanitizePdfWithIText(originalPath, fixedPath)
                    msgs.Add($"Saneado por longitud/header. Páginas (iText): {n}.")
                    Dim err As String = Nothing
                    pages = TryGhostscriptPageCount(fixedPath, vInfo, err)
                    If pages > 0 Then
                        finalPath = fixedPath
                        log = String.Join(" | ", msgs)
                        Return True
                    Else
                        msgs.Add("Ghostscript aún con 0 páginas tras saneo.")
                        If Not String.IsNullOrWhiteSpace(err) Then msgs.Add("GS: " & err)
                        log = String.Join(" | ", msgs)
                        Return False
                    End If
                Catch ex As Exception
                    msgs.Add("Error al sanear: " & ex.Message)
                    log = String.Join(" | ", msgs)
                    Return False
                End Try
            End If
            ' B) Intento directo con Ghostscript
            Dim gsErr As String = Nothing
            pages = TryGhostscriptPageCount(originalPath, vInfo, gsErr)
            If pages > 0 Then
                msgs.Add($"OK con Ghostscript (sin saneo). Páginas: {pages}.")
                log = String.Join(" | ", msgs)
                Return True
            End If
            ' C) Saneo + reintento
            msgs.Add("Ghostscript devolvió 0 páginas o lanzó error: " & If(gsErr, ""))
            Dim outFixed = Path.Combine(Path.GetDirectoryName(originalPath),
                        Path.GetFileNameWithoutExtension(originalPath) & "_fixed2.pdf")
            ' 1) Libera recursos propios
            GC.Collect()
            GC.WaitForPendingFinalizers()
            Try
                Dim n = SanitizePdfWithIText(originalPath, outFixed)
                msgs.Add($"Saneado. Páginas (iText): {n}.")
                gsErr = Nothing
                pages = TryGhostscriptPageCount(outFixed, vInfo, gsErr)
                If pages > 0 Then
                    finalPath = outFixed
                    msgs.Add($"OK con Ghostscript tras saneo. Páginas: {pages}.")
                    log = String.Join(" | ", msgs)
                    Return True
                Else
                    msgs.Add("Ghostscript aún sin páginas tras saneo.")
                    If Not String.IsNullOrWhiteSpace(gsErr) Then msgs.Add("GS: " & gsErr)
                    log = String.Join(" | ", msgs)
                    Return False
                End If
            Catch ex As Exception
                msgs.Add("Error al sanear: " & ex.Message)
                log = String.Join(" | ", msgs)
                Return False
            End Try
        End Function
        ' --- Verifica longitud y header ---
        Private Function IsClearlyBroken(pdfPath As String) As Boolean
            Dim fi = New FileInfo(pdfPath)
            If Not fi.Exists Then Return True
            If fi.Length = 0 Then Return True ' <-- Aquí verificas "longitud = 0"

            Try
                Using fs = New FileStream(pdfPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Dim buf(7) As Byte
                    Dim read = fs.Read(buf, 0, buf.Length)
                    If read < 5 Then Return True
                    Dim head = System.Text.Encoding.ASCII.GetString(buf, 0, read)
                    'System.Text.Encoding.Latin1.GetString(buf, 0, read)
                    If Not head.Contains("%PDF-") Then Return True
                End Using
            Catch
                Return True
            End Try

            Return False
        End Function
        ' --- Solo obtener PageCount con Ghostscript (sin render) ---
        Private Function TryGhostscriptPageCount(pdfPath As String,
                                 vInfo As GhostscriptVersionInfo,
                                 ByRef errorMessage As String) As Integer
            errorMessage = Nothing
            Try
                Using raster As New GhostscriptRasterizer()
                    ' Mantengo tu NOSAFER; si quieres ver fallos “duros”, habilita PDFSTOPONERROR:
                    raster.CustomSwitches.Add("-dNOSAFER")
                    ' raster.CustomSwitches.Add("-dPDFSTOPONERROR") ' opcional
                    raster.Open(pdfPath, vInfo, False)
                    Return raster.PageCount
                End Using
            Catch ex As Exception
                errorMessage = ex.Message
                Return 0
            End Try
        End Function
        ' --- Saneamiento: reescribir PDF copiando páginas con iText7 ---
        Private Function SanitizePdfWithIText(inputPath As String, outputPath As String) As Integer
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath))
            Using reader As New PdfReader(inputPath)
                Using writer As New PdfWriter(outputPath)
                    Using src As New iText.Kernel.Pdf.PdfDocument(reader)
                        Using dst As New iText.Kernel.Pdf.PdfDocument(writer)
                            Dim n As Integer = src.GetNumberOfPages()
                            If n <= 0 Then
                                Throw New InvalidDataException("No fue posible obtener el número de páginas durante el saneamiento.")
                            End If
                            src.CopyPagesTo(1, n, dst)
                            Return n
                        End Using
                    End Using
                End Using
            End Using
        End Function


        Private Sub CargarArchivo_doc(ArchivoDoc As String)
            If ControlCargueImagen(ArchivoDoc, "DOCX " & ParamTipologia.Numlote, 1, True) = True Then
                MsgBox("Archivo de imagen " & ArchivoDoc & " ya esta cargado... Verifique.")
                Exit Sub
            End If
            Dim ArchivoPdf = ConvertDocxToPdf(ArchivoDoc)
            If Trim(ArchivoPdf) = "" Then
                Exit Sub
            End If
            CargarArchivo_Pdf(ArchivoPdf, ArchivoDoc)
            Try
                File.Delete(ArchivoPdf)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub CargarArchivo_Xls(ArchivoXls As String)
            If ControlCargueImagen(ArchivoXls, "XLSX " & ParamTipologia.Numlote, 1, True) = True Then
                MsgBox("Archivo de imagen " & ArchivoXls & " ya esta cargado... Verifique.")
                Exit Sub
            End If
            Dim ArchivoPdf = ConvertExcelToPdf(ArchivoXls, False, True)
            If Trim(ArchivoPdf) = "" Then
                Exit Sub
            End If
            CargarArchivo_Pdf(ArchivoPdf, ArchivoXls)
            Try
                File.Delete(ArchivoPdf)
            Catch ex As Exception

            End Try
        End Sub

        ''' <param name="inputPath">Ruta completa del Excel de entrada</param>
        ''' <param name="openAfter">True para abrir el PDF al terminar</param>
        ''' <param name="ignorePrintAreas">True para ignorar áreas de impresión</param>
        Public Function ConvertExcelToPdf(inputPath As String,
                                 Optional openAfter As Boolean = False,
                                 Optional ignorePrintAreas As Boolean = False) As String

            Dim outputPdf As String = ""
            outputPdf = Path.Combine(System.IO.Path.GetTempPath(), Path.GetFileNameWithoutExtension(inputPath) & ".pdf")
            'OutputPdf = Path.Combine(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath) & ".pdf")
            'System.IO.Path.GetTempPath()

            Dim app As New Excel.Application With {.DisplayAlerts = False}
            ' .Visible = False,


            Dim wb As Excel.Workbook = Nothing
            ' evita prompts como "¿Guardar cambios?"
            ' [4](https://stackoverflow.com/questions/43881196/excel-to-pdf-using-interop)
            Try
                wb = app.Workbooks.Open(Filename:=inputPath, ReadOnly:=True)
                lblC_pdf.Visible = True
                Dim Ci As Integer = wb.Worksheets.Count
                Dim Pag As Integer = 0
                lblC_pdf.Text = "0/" & Ci.ToString.Trim
                lblC_pdf.Refresh()
                For Each ws As Excel.Worksheet In wb.Worksheets
                    Pag += 1
                    lblC_pdf.Text = "H_" & Pag.ToString.Trim & "/" & Ci.ToString.Trim
                    lblC_pdf.Refresh()
                    ' Omite Chart sheets
                    If TypeOf ws Is Excel.Worksheet Then
                        Dim sh = CType(ws, Excel.Worksheet)
                        ' 1) Limpia saltos de página manuales (horizontal y vertical)
                        Try
                            sh.HPageBreaks.Clear()
                            sh.VPageBreaks.Clear()
                        Catch
                        End Try
                        ' 2) Define el área de impresión (opcional: limita a UsedRange)
                        Dim ur As Excel.Range = sh.UsedRange
                        If ur IsNot Nothing Then
                            sh.PageSetup.PrintArea = ur.Address(ReferenceStyle:=Excel.XlReferenceStyle.xlA1)
                        Else
                            sh.PageSetup.PrintArea = ""
                        End If

                        ' 3) Ajustes de página para no partir columnas
                        With sh.PageSetup
                            .Orientation = Excel.XlPageOrientation.xlLandscape   ' Horizontal
                            .PaperSize = Excel.XlPaperSize.xlPaperA4              ' Ajusta si usas Carta: xlPaperLetter
                            .CenterHorizontally = True
                            .CenterVertically = False
                            ' Márgenes (en puntos). Ajusta a tus necesidades.
                            .LeftMargin = app.InchesToPoints(0.3)
                            .RightMargin = app.InchesToPoints(0.3)
                            .TopMargin = app.InchesToPoints(0.5)
                            .BottomMargin = app.InchesToPoints(0.5)
                            .HeaderMargin = app.InchesToPoints(0.3)
                            .FooterMargin = app.InchesToPoints(0.3)
                            ' *** CLAVE ***
                            ' Para FitToPagesWide/Tall, debes desactivar Zoom.
                            .Zoom = False
                            ' Forzar UNA página de ancho (no rompe columnas)
                            .FitToPagesWide = 1
                            ' Permitir varias páginas de alto (no comprime de más)
                            ' En Interop, usa False (o 0) para “tantas como haga falta”
                            .FitToPagesTall = False
                            ' Respeta las áreas de impresión definidas
                            ' (en ExportAsFixedFormat usaremos IgnorePrintAreas:=False)
                        End With
                        ' 4) (Opcional) Ajusta columnas al contenido antes de escalar
                        '    Si tienes columnas extremadamente anchas, AutoFit puede ayudar:
                        '    ur.Columns.AutoFit()
                    End If
                Next
                lblC_pdf.Visible = False
                ' Exporta a PDF con calidad estándar. También puedes usar xlQualityMinimum.
                wb.ExportAsFixedFormat(
                Type:=Excel.XlFixedFormatType.xlTypePDF,
                Filename:=outputPdf,
                Quality:=Excel.XlFixedFormatQuality.xlQualityStandard,
                IncludeDocProperties:=True,
                IgnorePrintAreas:=ignorePrintAreas,
                From:=Type.Missing,  ' primera página
                To:=Type.Missing,    ' última página
                OpenAfterPublish:=openAfter,
                FixedFormatExtClassPtr:=Type.Missing
            )

                ' Documentado en _Workbook.ExportAsFixedFormat (Excel PIA). [1](https://learn.microsoft.com/en-us/dotnet/api/microsoft.office.interop.excel._workbook.exportasfixedformat?view=excel-pia)

                Return outputPdf
            Catch ex As Exception
                lblC_pdf.Visible = False
                Return ""
            Finally
                ' Cierre y liberación COM en orden inverso
                If wb IsNot Nothing Then
                    wb.Close(SaveChanges:=False)
                    Marshal.FinalReleaseComObject(wb)
                    wb = Nothing
                End If
                If app IsNot Nothing Then
                    app.Quit()
                    Marshal.FinalReleaseComObject(app)
                    app = Nothing
                End If
                ' Acelera la liberación de RCWs
                GC.Collect()
                GC.WaitForPendingFinalizers()
                GC.Collect()
                GC.WaitForPendingFinalizers()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app)
            End Try

        End Function
        Public Function ImageToGrayscaleJpegBytes(img As Image, calidad As Long) As Byte()
            ' calidad: 0–100 (100 = máxima calidad; archivos más grandes)
            If calidad < 0 OrElse calidad > 100 Then calidad = 90

            ' Matriz de color para escala de grises (luminancia)
            Dim cm As New ColorMatrix(New Single()() {
        New Single() {0.299F, 0.299F, 0.299F, 0, 0},
        New Single() {0.587F, 0.587F, 0.587F, 0, 0},
        New Single() {0.114F, 0.114F, 0.114F, 0, 0},
        New Single() {0, 0, 0, 1, 0},
        New Single() {0, 0, 0, 0, 1}
    })
            Dim ia As New ImageAttributes()
            ia.SetColorMatrix(cm)

            ' Crear un bitmap destino con el mismo tamaño que la imagen original
            Using bmp As New Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb)
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                    Dim rect = New Rectangle(0, 0, bmp.Width, bmp.Height)
                    ' Dibuja aplicando la matriz de grises
                    g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
                End Using

                ' Guardar a JPEG con calidad controlada
                Dim jpegCodec = ImageCodecInfo.GetImageEncoders().First(Function(c) c.MimeType = "image/jpeg")
                Dim encParams As New EncoderParameters(1)
                encParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, calidad)

                Using ms As New MemoryStream()
                    bmp.Save(ms, jpegCodec, encParams)
                    Return ms.ToArray()
                End Using
            End Using
        End Function

        Public Function ControlCargueImagen(ByVal Archivo As String, NomImagen As String, Accion As Integer, ByVal Verificar As Boolean) As Boolean
            ControlCargueImagen = False
            Dim Linea As String
            Dim Arch As String
            Dim SiEsta As Boolean = False
            Arch = RutaLotes & "\ControlCargueImagen"
            If File.Exists(Arch) = False Then
                FileOpen(1, Arch, OpenMode.Output)
                FileClose(1)
            End If
            If Accion = 1 Then
                FileOpen(1, Arch, OpenMode.Input, OpenAccess.Default)
                Do While Not EOF(1)
                    Linea = LineInput(1)
                    If Trim(Linea) = Trim(Archivo) Then
                        SiEsta = True
                    End If
                Loop
                FileClose(1)
                If Verificar = True Then
                    Return SiEsta
                Else
                    If SiEsta = False Then
                        FileOpen(1, Arch, OpenMode.Append)
                        PrintLine(1, "IMAGENID:" & Trim(NomImagen))
                        PrintLine(1, Trim(Archivo))
                        PrintLine(1, "Usuario : " & Program.DesktopGlobal.PuestoTrabajoRow.PC_Name & "  Fecha y Hora de realización : " & Now.ToString("yyyy/MM/dd HH:mm:ss tt"))
                        FileClose(1)
                        Return True
                    End If
                End If
            End If
            If Accion = 0 Then
                FileOpen(1, Arch, OpenMode.Input, OpenAccess.Default)
                Dim Tlin As Integer = 0
                Dim Lineas(0) As String
                SiEsta = False
                Do While Not EOF(1)
                    Tlin += 1
                    ReDim Preserve Lineas(Tlin)
                    Lineas(Tlin) = LineInput(1)
                Loop
                FileClose(1)
                FileOpen(1, Arch, OpenMode.Output)
                FileClose(1)
                FileOpen(1, Arch, OpenMode.Append)
                For i As Integer = 1 To Tlin
                    If Mid(Lineas(i), 1, 9) = "IMAGENID:" Then
                        SiEsta = False
                        If Trim(Mid(Lineas(i), 10)) = Trim(NomImagen) Then
                            SiEsta = True
                        End If
                    End If
                    Linea = Trim(Lineas(i))
                    If SiEsta = False Then
                        PrintLine(1, Linea)
                    End If
                Next
                FileClose(1)
            End If
        End Function


#End Region

#Region "Manejo Imagen"

        Private Sub BtnAddCarpeta_Click(sender As Object, e As EventArgs) Handles BtnAddCarpeta.Click
            Dim folderDialog As New FolderBrowserDialog() With {
         .Description = "Seleccionar carpeta a cargar."}

            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim CarpSeleccionada As String = folderDialog.SelectedPath
                'Verificar si la carpeta contiene imagenes validas para cargar

                Dim extensionesPermitidas = SetExt
                ' {".jpg", ".JPG", ".png", ".PNG", ".pdf", ".PDF", ".tif", ".TIF", ".tiff", ".TIFF"}
                Dim archivosFiltrados = Directory.GetFiles(CarpSeleccionada) _
                  .Where(Function(f) extensionesPermitidas.Contains(Path.GetExtension(f))).ToArray()

                ' Mostrar en un ListBox, DataGridView o MessageBox
                For Each archivo In archivosFiltrados
                    Select Case Path.GetExtension(archivo).ToLower
                        Case ".png"
                            CargarArchivoPng(archivo)
                        Case ".tif"
                            'Verificar si es un multitiff
                            Dim imagenOriginal As Image = Image.FromFile(archivo)
                            ' Obtener la dimensión de frames (páginas)
                            Dim dimension As New FrameDimension(imagenOriginal.FrameDimensionsList(0))
                            Dim numPaginas As Integer = imagenOriginal.GetFrameCount(dimension)
                            ' Carpeta destino (puedes personalizar esto con SaveFileDialog si quieres)
                            'Dim carpetaDestino As String = System.IO.Path.GetDirectoryName(ArcSeleccionado)
                            If numPaginas > 1 Then
                                CargarArchivoTiff(archivo)
                            Else
                                CargarArchivoPng(archivo)
                            End If
                        Case ".jpg"
                            CargarArchivoPng(archivo)
                        Case ".tiff"
                            CargarArchivoTiff(archivo)
                        Case ".pdf"
                            CargarArchivo_Pdf(archivo, "")
                        Case "doc", "docx"
                            CargarArchivo_doc(archivo)
                            IdRImg = DgvRegistrosLote.Rows.Count
                        Case "xls", "xlsx"
                            CargarArchivo_Xls(archivo)
                            IdRImg = DgvRegistrosLote.Rows.Count
                            'Case "ppt", "pptx"
                            'CargarArchivo_Ppt(archivo)
                            'IdRImg = DgvRegistrosLote.Rows.Count
                        Case Else
                            MsgBox("Archivo seleccionado no valido para este proceso.." & Chr(13) &
                            "Formatos validos (png,tif,jpg,tiff,pdf)", 64, "Error en formato de archivo.")
                            Exit Sub
                    End Select
                Next
                IdRImg = DgvRegistrosLote.Rows.Count
            End If

        End Sub

        Private Sub BtnPrimero_Click(sender As Object, e As EventArgs) Handles BtnPrimero.Click
            If DgvRegistrosLote.Rows.Count > 0 Then
                IdRImg = 1
                MostrarImagen()
            End If
        End Sub

        Private Sub BtnAnterior_Click(sender As Object, e As EventArgs) Handles BtnAnterior.Click
            If IdRImg > 1 Then
                IdRImg -= 1
                MostrarImagen()
            End If
        End Sub

        Private Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles BtnSiguiente.Click
            If IdRImg < DgvRegistrosLote.Rows.Count Then
                IdRImg += 1
                MostrarImagen()
            End If
        End Sub

        Private Sub BtnUltimo_Click(sender As Object, e As EventArgs) Handles BtnUltimo.Click
            If DgvRegistrosLote.Rows.Count > 0 Then
                IdRImg = DgvRegistrosLote.Rows.Count
                MostrarImagen()
            End If
        End Sub
        Private Sub MostrarImagen()
            Dim filaSeleccionada As DataGridViewRow = DgvRegistrosLote.Rows(IdRImg - 1)
            Dim NomImagen As String = filaSeleccionada.Cells("Nombre").Value.ToString()
            MostrarImagen(RutaLoteNuevo & "\" & NomImagen, NomImagen)
        End Sub

        Private Sub BtnGiraIzquieda_Click(sender As Object, e As EventArgs) Handles BtnGiraIzquieda.Click
            If PicImagen.Image IsNot Nothing Then
                Try
                    Dim bmp As Image = PicImagen.Image
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    PicImagen.Image = bmp
                    If ParamTipologia.Estado = "ABIERTO" Then
                        'Guardamos la imagen con el cambio
                        Select Case ParamTipologia.FormatoSalida
                            Case "TIFF"
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Tiff)
                            Case "JPEG"
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Jpeg)
                            Case "PNG"
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Png)
                            Case Else
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Tiff)
                        End Select
                        'Debemos cambiar la imagen miniatura
                        For Each ctrl As Control In FLPImagenes.Controls
                            If TypeOf ctrl Is PictureBox Then
                                Dim pic As PictureBox = CType(ctrl, PictureBox)
                                If pic.Tag = PicImagen.Tag Then
                                    pic.Image?.Dispose()

                                    Dim imagenOriginal As Image
                                    Using fs As New FileStream(ParamTipologia.RutaLote & "\" & PicImagen.Tag, FileMode.Open, FileAccess.Read)
                                        imagenOriginal = New Bitmap(Image.FromStream(fs))
                                    End Using
                                    Dim tamanioMiniatura As New Size(100, 100) ' Tamaño deseado para las miniaturas
                                    Dim imagenMiniatura As Image = imagenOriginal.GetThumbnailImage(tamanioMiniatura.Width, tamanioMiniatura.Height, Nothing, IntPtr.Zero)
                                    pic.Image = imagenMiniatura

                                    Exit For
                                End If
                            End If
                        Next
                        FLPImagenes.Refresh()
                    Else
                        MsgBox("Los cambios no seran guardados debido a que el lote ya no se encuentra abierto.", 64, "Cambio no guardado.")
                    End If
                Catch ex As Exception
                End Try
            End If
        End Sub

        Private Sub BtnGiraDerecha_Click(sender As Object, e As EventArgs) Handles BtnGiraDerecha.Click
            'If Giro = 1 Or Giro = 3 Then PicImagen.ClientSize = New System.Drawing.Point(653, 284)
            'If Giro = 0 Or Giro = 2 Then PicImagen.ClientSize = New System.Drawing.Point(284, 653)

            'Giro += 1
            If PicImagen.Image IsNot Nothing Then
                Try
                    Dim bmp As Image = PicImagen.Image
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone)
                    PicImagen.Image = bmp
                    If ParamTipologia.Estado = "ABIERTO" Then
                        Select Case ParamTipologia.FormatoSalida
                            Case "TIFF"
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Tiff)
                            Case "JPEG"
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Jpeg)
                            Case "PNG"
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Png)
                            Case Else
                                PicImagen.Image.Save(ParamTipologia.RutaLote & "\" & PicImagen.Tag, ImageFormat.Tiff)
                        End Select
                        For Each ctrl As Control In FLPImagenes.Controls
                            If TypeOf ctrl Is PictureBox Then
                                Dim pic As PictureBox = CType(ctrl, PictureBox)
                                If pic.Tag = PicImagen.Tag Then
                                    pic.Image?.Dispose()

                                    Dim imagenOriginal As Image
                                    Using fs As New FileStream(ParamTipologia.RutaLote & "\" & PicImagen.Tag, FileMode.Open, FileAccess.Read)
                                        imagenOriginal = New Bitmap(Image.FromStream(fs))
                                    End Using
                                    Dim tamanioMiniatura As New Size(100, 100) ' Tamaño deseado para las miniaturas
                                    Dim imagenMiniatura As Image = imagenOriginal.GetThumbnailImage(tamanioMiniatura.Width, tamanioMiniatura.Height, Nothing, IntPtr.Zero)
                                    pic.Image = imagenMiniatura

                                    Exit For
                                End If
                            End If
                        Next
                        FLPImagenes.Refresh()
                    Else
                        MsgBox("Los cambios no seran guardados debido a que el lote ya no se encuentra abierto.", 64, "Cambio no guardado.")
                    End If
                Catch ex As Exception
                End Try
            End If
            'If Giro = 4 Then Giro = 0
        End Sub

        Private Sub BtnNormal_Click(sender As Object, e As EventArgs) Handles BtnNormal.Click
            If PicImagen.Image IsNot Nothing Then
                Try
                    PicImagen.SizeMode = PictureBoxSizeMode.Zoom
                    'PicImagen.SizeMode = PictureBoxSizeMode.StretchImage
                    PicImagen.ClientSize = New System.Drawing.Point(720, 800)
                    PicImagen.Image = PicA.Image
                Catch
                End Try
            End If
        End Sub

        Private Sub BtnAutoTamaño_Click(sender As Object, e As EventArgs) Handles BtnAutoTamaño.Click
            If PicImagen.Image IsNot Nothing Then
                Try
                    PicImagen.SizeMode = PictureBoxSizeMode.Normal
                    PicImagen.Size = PicImagen.Image.Size

                    Panel1.AutoScroll = False
                    Panel1.AutoScroll = True
                Catch
                End Try
            End If
        End Sub

        Private Sub BtnReducir_Click(sender As Object, e As EventArgs) Handles BtnReducir.Click
            If PicImagen.Image IsNot Nothing Then
                Try
                    If PicImagen.Height < PicImagen.Width Then
                        PicImagen.Height = PicImagen.Height - 50
                        PicImagen.Width = PicImagen.Width - 50
                        PicImagen.SizeMode = PictureBoxSizeMode.Zoom
                    Else
                        PicImagen.Height = PicImagen.Height - 50
                        PicImagen.Width = PicImagen.Width - 50
                        PicImagen.SizeMode = PictureBoxSizeMode.Zoom
                    End If
                Catch
                End Try
            End If
        End Sub

        Private Sub BtnAmpliar_Click(sender As Object, e As EventArgs) Handles BtnAmpliar.Click
            If PicImagen.Image IsNot Nothing Then
                Try
                    If PicImagen.Height < PicImagen.Width Then
                        PicImagen.Height = PicImagen.Height + 50
                        PicImagen.Width = PicImagen.Width + 50
                        PicImagen.SizeMode = PictureBoxSizeMode.Zoom
                    Else
                        PicImagen.Height = PicImagen.Height + 50
                        PicImagen.Width = PicImagen.Width + 50
                        PicImagen.SizeMode = PictureBoxSizeMode.Zoom
                    End If
                Catch
                End Try
            End If
        End Sub

        Private Function GuardarCamposCapturados() As String
            Dim CamposCapturados As String
            'Campo Oficina obligatorio
            CamposCapturados = Trim(Val(ParamTipologia.idOficina)) & ";"
            CamposCapturados &= Trim(Mid(ParamTipologia.Numlote, 1, 4))

            'Campo Serie documental obligatorio
            CamposCapturados &= "|" & Trim(Val(ParamTipologia.idSerieDctal)) & ";"
            CamposCapturados &= Trim(Val(Mid(ParamTipologia.Numlote, 6, 2)))

            For Each ctrl As Control In PnCampos.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("Txt") Then
                    Dim Txt As TextBox = CType(ctrl, TextBox)
                    CamposCapturados &= "|" & Txt.Tag & ";" & Trim(Txt.Text)
                    If InStr(ctrl.Name.ToUpper, "LABEL") > 0 Then
                        NumLabel = Txt.Text
                    End If
                End If
                'Validar cantidad de decimales del numero para guardarlo
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("Txn") Then
                    Dim Txn As TextBox = CType(ctrl, TextBox)
                    CamposCapturados &= "|" & Txn.Tag & ";" & Trim(Txn.Text)
                End If
                If TypeOf ctrl Is ComboBox AndAlso ctrl.Name.StartsWith("Cmb") Then
                    Dim cbx As ComboBox = CType(ctrl, ComboBox)
                    CamposCapturados &= "|" & cbx.Tag & ";" & Trim(cbx.SelectedValue)
                    If ctrl.Name.ToUpper = "CMBHORARIO" Then
                        Jornada = cbx.Text
                    End If
                End If
                If TypeOf ctrl Is DateTimePicker AndAlso ctrl.Name.StartsWith("Dtp") Then
                    Dim Dtp As DateTimePicker = CType(ctrl, DateTimePicker)
                    CamposCapturados &= "|" & Dtp.Tag & ";" & Dtp.Value.ToString("yyyy/MM/dd")
                    If ctrl.Name.ToUpper = "FECHA MOVIMIENTO" Then
                        FechaMvto = Dtp.Text
                    End If
                End If
                If TypeOf ctrl Is CheckBox AndAlso ctrl.Name.StartsWith("Chb") Then
                    Dim Chb As CheckBox = CType(ctrl, CheckBox)
                    CamposCapturados &= "|" & Chb.Tag & ";" & Chb.Checked
                End If
            Next
            Return CamposCapturados
        End Function


#End Region

        Public Function TransferirLote() As Boolean
            TransferirLote = True
            Dim extensionesPermitidas = {".jpg", ".JPG", ".png", ".PNG", ".tif", ".TIF", ".tiff", ".TIFF"}
            Dim archivos = Directory.GetFiles(LoteExportar.RutaLote) _
            .Where(Function(f) extensionesPermitidas.Contains(Path.GetExtension(f))).ToArray()

            Try
                Directory.Delete(RutaImagSCan & "\temp", recursive:=True)
            Catch ex As Exception
                CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: TransferirLote")
            End Try

            ZipFileName = RutaImagSCan & "\temp\"
            If Directory.Exists(ZipFileName) = False Then
                Try
                    Directory.CreateDirectory(ZipFileName)
                Catch ex As Exception
                    CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Crear directorio Temp")
                    'MsgBox(Err.Description)
                End Try
            End If
            ZipFileName &= LoteExportar.Numlote & ".zip"

            Using zip As New ZipFile()
                For Each archivo In archivos
                    zip.AddFile(archivo, "")  'ParamTipologia.RutaLote 
                Next
                zip.Save(ZipFileName)
            End Using

            'ZipUtil.Comprimir(FileNames, ZipFileName, False)
            Try
                Directory.Delete(RutaImagSCan & "\Source", recursive:=True)
            Catch ex As Exception
                CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Borrar directorio Source")
            End Try
            Dim FolderSource As String = RutaImagSCan & "\Source"
            If Directory.Exists(FolderSource) = False Then
                Try
                    Directory.CreateDirectory(FolderSource)
                Catch ex As Exception
                    CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Crear directorio Source")
                    'MsgBox(Err.Description)
                End Try
            End If
            Try
                Transfer = New FileSendingClient(Program.FileServerIP, Program.FileServerPort, Program.FileServerAppName, FolderSource, Program.PackageSize, LoteExportar.FechaProceso, Program.MiharuSession.Usuario.id)

                AddHandler Transfer.TransferBegin, AddressOf Transfer_TransferBegin
                AddHandler Transfer.TransferProcess, AddressOf Transfer_TransferProcess
                AddHandler Transfer.TransferCompleted, AddressOf Transfer_TransferCompleted
                AddHandler Transfer.TransferError, AddressOf Transfer_TransferError

                'Identificador = Transfer.Transmitir(ZipFileName, Program.Config.PlazaID, Program.UserID)
                Identificador = Transfer.TransmitirDDO(ZipFileName, Program.DesktopGlobal.CentroProcesamientoRow.Codigo_Centro, Program.MiharuSession.Usuario.id, LoteExportar.FechaProceso)
            Catch ex As Exception
                CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Final TransferirLote")
                'MessageBox.Show(ex.ToString(), "Transmitir", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Throw
            End Try

        End Function
        Private Sub Transfer_TransferBegin(sender As Object, nIdentificador As String)
            If Me.InvokeRequired Then
                Dim MyDelegate As TransferBeginDelegate = AddressOf Transfer_TransferBegin
                Me.Invoke(MyDelegate, {sender, nIdentificador})
            Else
                'AccionToolStripStatusLabel.ForeColor = Color.Green
                'AccionToolStripStatusLabel.Text = "Transmitiendo"
                'ProgresoToolStripProgressBar.Value = 0
                'ContadorToolStripStatusLabel.Text = "0%"
            End If
        End Sub
        Private Sub Transfer_TransferProcess(sender As Object, nIdentificador As String, Avance As Single)
            If Me.InvokeRequired Then
                Dim MyDelegate As TransferProcessDelegate = AddressOf Transfer_TransferProcess
                Me.Invoke(MyDelegate, {sender, nIdentificador, Avance})
            Else
                'ProgresoToolStripProgressBar.Value = CInt(Avance)
                'ContadorToolStripStatusLabel.Text = CInt(Avance) & "%"
            End If
        End Sub
        Private Sub Transfer_TransferCompleted(ByVal sender As Object, ByVal nIdentificador As String)
            If Me.InvokeRequired Then
                Dim MyDelegate As TransferCompletedDelegate = AddressOf Transfer_TransferCompleted
                Me.Invoke(MyDelegate, {sender, nIdentificador})
            Else
                Try

                    If CargarLote(Identificador) = True Then
                        LoteExportar.Estado = "TRANSFERIDO"
                        CambioEstadodeLote("TRANSFERIDO", LoteExportar.IntentoExportar, "", LoteExportar)
                        'REGISTRO DE LOTE EN BASE DE DATOS
                        Dim DtLote As DataTable
                        Dim FechaCargue As String = Now.ToString("yyyy/MM/dd") & " " & Now.ToString("HH:mm:ss")
                        DtLote = CDig.ActualizarLoteDigitalizacion(LoteExportar.idEntidad, LoteExportar.idProyecto,
                             LoteExportar.Numlote, FechaCargue, LoteExportar.Imagenes, "TRANSFERIDO", 0)
                        If DtLote Is Nothing Then
                            MsgBox("Se presento un problema al crear el lote en base de datos... Verifique.")
                            Exit Sub
                        End If
                        Exportando = False
                    Else
                        Exportando = False
                        LoteExportar.Estado = "CERRADO"
                        CambioEstadodeLote("CERRADO", LoteExportar.IntentoExportar + 1, "", LoteExportar)
                    End If
                Catch ex As Exception
                    Exportando = False
                    LoteExportar.Estado = "CERRADO"
                    CambioEstadodeLote("CERRADO", LoteExportar.IntentoExportar, "", LoteExportar)
                    CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Transfer_TransferCompleted")
                End Try
            End If
        End Sub
        Private Sub Transfer_TransferError(ByVal sender As Object, ByVal nIdentificador As String, ByVal Mensaje As String)
            If Me.InvokeRequired Then
                Dim MyDelegate As TransferErrorDelegate = AddressOf Transfer_TransferError
                Me.Invoke(MyDelegate, {sender, nIdentificador, Mensaje})
            Else
                If ExportandoLote <> LoteExportar.RutaLote Then
                    CDig.RegistrarError("No a sido posible realizar la transmisión, verique que el servicio FileSending este funcionando correctamente ", "Lote: " & LoteExportar.Numlote, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Transfer_TransferError")
                    ExportandoLote = LoteExportar.RutaLote
                End If
                'AccionToolStripStatusLabel.ForeColor = Color.Red
                'AccionToolStripStatusLabel.Text = "Intentando reanudar la transmisón..."
            End If
        End Sub

        Public Function CargarLote(nIdentificador As String) As Boolean
            CargarLote = True

            Dim ExtImgSalida As String
            Select Case LoteExportar.FormatoSalida
                Case "TIFF" : ExtImgSalida = ".tif"
                Case "JPG" : ExtImgSalida = ".jpg"
                Case "TIF" : ExtImgSalida = ".tif"
                Case "PNG" : ExtImgSalida = ".png"
                Case Else : ExtImgSalida = ".tif"
            End Select
            Dim Cargue As Integer = 0
            Dim Folios As Integer = 0


            Dim Carpetazip As String = Path.GetFileNameWithoutExtension(Path.GetFileName(ZipFileName))

            Transfer.Unzip(Path.GetFileName(ZipFileName))

            Dim Definicion = Transfer.getDefinition(nIdentificador)
            Try
                Transfer.CargarDDO(CShort(LoteExportar.idEntidad), CShort(LoteExportar.idProyecto), CShort(LoteExportar.idEsquema), LoteExportar.Numlote, LoteExportar.Numlote, Program.DesktopGlobal.PcName, CShort(LoteExportar.idEntidad), CShort(Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede), CShort(Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento), Definicion.id_Log, Program.MiharuSession.Usuario.id, CShort(31), LoteExportar.Numlote, CInt(Cargue), CShort(Folios), LoteExportar.CamposCaptura, ExtImgSalida)
            Catch ex As Exception
                CDig.RegistrarError(Err.Description, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: CargarDDO lote: " & LoteExportar.Numlote & " Revisar precinto sin contenedor.")
                'MsgBox(Err.Description)
                Return False
            End Try

            CrearDirectorios()

            NewProcess()

            Transfer.Detener()
            ' Transfer = vbNull


        End Function
        Private Sub CrearDirectorios()
            Try
                If System.IO.Directory.Exists(Program.TempDirectory) Then
                    System.IO.Directory.Delete(Program.TempDirectory, True)
                End If

                System.IO.Directory.CreateDirectory(Program.TempDirectory)
            Catch ex As Exception
                CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: CreateDirectorios - No fue posible crear directorio.")
                ' Handle exception if needed
            End Try

            Try
                If System.IO.Directory.Exists(Program.SourceDirectory) Then
                    System.IO.Directory.Delete(Program.SourceDirectory, True)
                End If

                System.IO.Directory.CreateDirectory(Program.SourceDirectory)
            Catch ex As Exception
                CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: CreateDirectorios - No fue posible crear directorio.")
                ' Handle exception if needed
            End Try
        End Sub
        Private Sub NewProcess()
            CrearDirectorios()
            '_Images.Clear()
            'DisplayImages(_Images)
            'ActivateOptions()

            'TransmitirToolStripButton.Enabled = False

            ' Borrar temporal            
            Dim temporaryFiles As String() = Directory.GetFiles(Program.TempDirectory)

            For Each temporaryFile As String In temporaryFiles
                Try
                    File.Delete(temporaryFile)
                Catch ex As Exception
                    CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: NewProcess - No fue posible eliminar archivos temporales.")
                    ' Handle exception if necessary
                End Try
            Next
        End Sub

        Private Sub BtnExportarLotes_Click(sender As Object, e As EventArgs) Handles BtnExportarLotes.Click
            If ParamTipologia.Imagenes = 0 Then
                MsgBox("No existen imagenes para exportar.." & Chr(13) &
                       "Por favor seleccione el lote a exportar" & Chr(13) &
                       "o proceda a crear un nuevo lote...")
                Exit Sub
            End If
            If ParamTipologia.Estado = "ABIERTO" Then
                If MsgBox("Esta seguro de cerrar el lote activo para su exportación.", 324, "Cierre de lote.") = MsgBoxResult.No Then
                    Exit Sub
                End If
                Try
                    ParamTipologia.Estado = "CERRADO"
                    LoteExportar = ParamTipologia
                    Des_HabilitarBotones(0)
                    'REGISTRO DE LOTE EN BASE DE DATOS
                    Dim DtLote As DataTable
                    Dim FechaCargue As String = Now.ToString("yyyy/MM/dd") & " " & Now.ToString("HH:mm:ss")
                    DtLote = CDig.ActualizarLoteDigitalizacion(LoteExportar.idEntidad, LoteExportar.idProyecto,
                     LoteExportar.Numlote, FechaCargue, LoteExportar.Imagenes, "CERRADO", 0)
                    If DtLote Is Nothing Then
                        MsgBox("Se presento un problema al crear el lote en base de datos... Verifique.")
                        Exit Sub
                    End If
                    CambioEstadodeLote("CERRADO", LoteExportar.IntentoExportar + 1, "", LoteExportar)
                    Me.BtnNewCarpeta.Focus()
                    Me.BtnExportarLotes.Enabled = False
                Catch ex As Exception
                    CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Click Transferir ABIERTO")
                End Try
            Else
                MsgBox("Lote no disponible para su cierre", 64, "Alerta.")
            End If

            If ParamTipologia.Estado = "CERRADO" Then
                If Exportando = False Then
                    Try
                        Exportando = True
                        LoteExportar.IntentoExportar += 1
                        CambioEstadodeLote("TRANSFIRIENDO", LoteExportar.IntentoExportar, "", LoteExportar)
                        ExportandoLote = ""
                        TransferirLote()
                    Catch ex As Exception
                        CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Click Transferir CERRADO")
                        CambioEstadodeLote("CERRADO", LoteExportar.IntentoExportar, ex.Message, LoteExportar)
                        Exportando = False
                    End Try
                End If
            Else
                MsgBox("Este lote no esta disponible para su transmisión. verifique.", 64, "Imposible transmitir..")
            End If

        End Sub

        Private Sub CambioEstadodeLote(Estado As String, Intentos As Integer, ErrorExportar As String, QueLote As Esquema)
            Dim ArchiLotes As String = RutaLotes & "\Control_lotes.txt"
            If File.Exists(ArchiLotes) = False Then
                MsgBox("Archivo control de lotes no existe... Verifique.." & Chr(13) & ArchiLotes)
                Exit Sub
            End If
            Dim EntroaLote As Boolean = False
            Dim lines As List(Of String) = File.ReadAllLines(ArchiLotes).ToList()
            Dim Eslote As Boolean = False
            For i As Integer = 0 To lines.Count - 1
                If Mid(Trim(lines(i)), 1, 7).ToUpper = "NUMLOTE" Then
                    If Trim(Mid(lines(i), 9)) = Trim(QueLote.Numlote) And Eslote = False Then
                        Eslote = True
                        Continue For
                    End If
                    If EntroaLote = True Then
                        Exit For
                    End If
                End If
                If Eslote = True And Len(Trim(lines(i))) > 2 Then
                    EntroaLote = True
                    If Mid(Trim(lines(i)), 1, 7).ToUpper = "NUMLOTE" Then
                        'Sale del lote si encuentra otro numlote
                        Exit For
                    End If
                    If Mid(Trim(lines(i)), 1, 6) = "ESTADO" Then
                        lines(i) = "ESTADO: " & QueLote.Estado
                    End If
                    If Mid(Trim(lines(i)), 1, 15) = "INTENTOEXPORTAR" Then
                        lines(i) = "INTENTOEXPORTAR: " & Intentos
                    End If
                    If Mid(Trim(lines(i)), 1, 13) = "ERROREXPORTAR" Then
                        lines(i) = "ERROREXPORTAR: " & ErrorExportar
                    End If
                End If
            Next i
            File.WriteAllLines(ArchiLotes, lines)

            'Actualizar datagridview
            For Each fila As DataGridViewRow In DgvCarpetas.Rows
                If Not fila.IsNewRow Then ' Evita la fila nueva vacía
                    If fila.Cells(0).Value IsNot Nothing AndAlso fila.Cells(0).Value.ToString() = QueLote.Numlote Then
                        DgvCarpetas.Rows(fila.Index).Cells(1).Value = Estado
                        DgvCarpetas.Refresh()
                        Exit For
                    End If
                End If
            Next

        End Sub

        Private Sub TimerExportar_Tick(sender As Object, e As EventArgs) Handles TimerExportar.Tick
            If Exportando = True Then
                Exit Sub
            End If
            'Cargar todos los lotes leidos
            CargarDetallesLotesCreados()
            Dim SiEsta As Boolean = False
            'Verificar si hay lotes abiertos sin movimiento mas de x tiempo
            For i As Integer = 1 To UBound(LotesLeidos)
                If LotesLeidos(i).Estado = "ABIERTO" And LotesLeidos(i).Imagenes > 0 Then
                    Dim horaUsuario As DateTime = DateTime.ParseExact(LotesLeidos(i).HoraInactivo, "HH:mm", Nothing)
                    Dim horaActualSoloHora As DateTime = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", Nothing)
                    Dim diferencia As TimeSpan = horaActualSoloHora - horaUsuario
                    If diferencia.TotalMinutes >= TiempoInactividad Then
                        LoteExportar = LotesLeidos(i)
                        ParamTipologia.Estado = "CERRADO"
                        'Validar si el lote que estaba habierto es el mismo lote activo para dehabilitar botones.
                        If LoteExportar.Numlote = ParamTipologia.Numlote Then
                            Des_HabilitarBotones(0)
                        End If
                        'REGISTRO DE LOTE EN BASE DE DATOS
                        Dim DtLote As DataTable
                        Dim FechaCargue As String = Now.ToString("yyyy/MM/dd") & " " & Now.ToString("HH:mm:ss")
                        DtLote = CDig.ActualizarLoteDigitalizacion(LoteExportar.idEntidad, LoteExportar.idProyecto,
                                LoteExportar.Numlote, FechaCargue, LoteExportar.Imagenes, "CERRADO", 0)
                        If DtLote Is Nothing Then
                            MsgBox("Se presento un problema al cerrar el lote en base de datos... Verifique.")
                            ' Exit Sub
                        End If
                        CambioEstadodeLote("CERRADO", LoteExportar.IntentoExportar, "", LoteExportar)
                    End If
                End If
            Next i
            'Verificar si hay lotes cerrados sin transmitir

            'Leer los lotes cerrados y por cada uno intentar transmitir.
            For r As Integer = 0 To 20
                For i As Integer = 1 To UBound(LotesLeidos)
                    If LotesLeidos(i).Estado = "CERRADO" And LotesLeidos(i).Imagenes > 0 And LotesLeidos(i).IntentoExportar = r Then
                        If r > 1 Then
                            'Validar en base de datos si ya fue transferido
                            Dim DtLote As DataTable
                            DtLote = CDig.ConsultarLoteTransferido(LotesLeidos(i).idEntidad, LotesLeidos(i).idProyecto,
                                            LotesLeidos(i).Numlote)
                            If DtLote Is Nothing Or DtLote.Rows.Count = 0 Then
                                'Aun no se ha marcado como transmitidoNo existe el lote en base de datos
                                'Continue For
                            Else
                                'Actualizar estado en archivo de control
                                LotesLeidos(i).Estado = "TRANSFERIDO"
                                CambioEstadodeLote("TRANSFERIDO", LotesLeidos(i).IntentoExportar, "", LotesLeidos(i))
                                Continue For
                            End If
                        End If
                        If LotesLeidos(i).IntentoExportar >= Reintentos Then
                            'Marcar error y no intentar mas
                            LotesLeidos(i).Estado = "ERROR"
                            CambioEstadodeLote("ERROR", LotesLeidos(i).IntentoExportar, "MAXIMO NUMERO DE INTENTOS ALCANZADO", LotesLeidos(i))
                            Continue For
                        End If
                        'DEJAR 10 INTENTOS COMO MAXIMO MARCO ERROR
                        LoteExportar = LotesLeidos(i)
                        SiEsta = True
                        Exit For
                    End If
                Next i
                If SiEsta = True Then
                    Exit For
                End If
            Next r
            If SiEsta = True Then
                Exportando = True
                LoteExportar.Estado = "CERRADO"
                'REGISTRO DE LOTE EN BASE DE DATOS
                Dim DtLote As DataTable
                Dim FechaCargue As String = Now.ToString("yyyy/MM/dd") & " " & Now.ToString("HH:mm:ss")
                DtLote = CDig.ActualizarLoteDigitalizacion(LoteExportar.idEntidad, LoteExportar.idProyecto,
               LoteExportar.Numlote, FechaCargue, LoteExportar.Imagenes, "CERRADO", 0)
                If DtLote Is Nothing Then
                    MsgBox("Se presento un problema al crear el lote en base de datos... Verifique.")
                    Exit Sub
                End If
                If LoteExportar.Numlote = ParamTipologia.Numlote Then
                    ParamTipologia.Estado = "CERRADO"
                    Des_HabilitarBotones(0)
                End If
                LoteExportar.IntentoExportar += 1
                CambioEstadodeLote("TRANSFIRIENDO", LoteExportar.IntentoExportar, "", LoteExportar)
                Try
                    ExportandoLote = ""
                    TransferirLote()
                Catch ex As Exception
                    ' MsgBox(Err.Description)
                    CambioEstadodeLote("CERRADO", LoteExportar.IntentoExportar, ex.Message, LoteExportar)
                    CDig.RegistrarError(ex.Message, ex.StackTrace, Environment.UserName, Me.Name, Environment.MachineName, "Accion: Tick TransferirLote")
                    Exportando = False
                End Try
            End If
        End Sub

        Private Sub DgvCarpetas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvCarpetas.CellContentClick

        End Sub
    End Class


End Namespace