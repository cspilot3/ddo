Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Drawing.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports PdfiumViewer
Imports Vintasoft
Imports Vintasoft.Twain


'Imports WIA
Namespace Forms

    Public Class Frm_Digitaliza


        Dim Newlote As Boolean
        Dim RutaLoteNuevo As String
        Dim RutaLotes As String
        Dim NombreLote As String
        Dim Imagenes(0) As ImagenEscaneada
        Dim TImagen As Integer
        Private _deviceManager As Vintasoft.Twain.DeviceManager

        Private Sub Frm_Digitaliza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Me.LblTitulo.Text = Program.DesktopGlobal.CentroProcesamientoRow.Codigo_Centro & " " & Program.DesktopGlobal.CentroProcesamientoRow.Nombre_Centro_Procesamiento
            SucursalIngreso = "0008" '= Format(Val(Program.DesktopGlobal.CentroProcesamientoRow.Codigo_Centro), "0000")
            RutaImagSCan = "C:\miharu\" 'Program.DesktopGlobal.PuestoTrabajoRow.Ruta_Local
            Newlote = False
            RutaLotes = RutaImagSCan & "\" & Format(Now(), "yyyyMMdd")
            Vintasoft.Twain.TwainGlobalSettings.Register(UsuarioReg, CorreoReg, CodigoReg)

            ' CargarEsquemas() ' Series Documentales

            CargarLotesExistentes()
            CargarToolTip()
        End Sub
        Private Sub Frm_Digitaliza_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
            'If (e.KeyCode = Keys.Alt) + (e.KeyCode = Keys.F4) Then
            '    MsgBox("Esta seguro que quiere cerrar el programa?")
            '    'Validar si existen lotes pendientes por exportar
            'End If

            If e.KeyCode = Keys.Enter Then
                If UCase(Me.ActiveControl.Name) <> "CMB_EXCLUIDO" Then
                    If TypeOf (Me.ActiveControl) Is ComboBox Then
                        SendKeys.Send("{Tab}")
                    ElseIf UCase(Me.ActiveControl.Name) <> "TXTEXCLUIDO" _
                    And UCase(Me.ActiveControl.Name) <> "TXTEXCLUIDO" Then
                        SendKeys.Send("{Tab}")
                    End If
                End If
            End If
        End Sub
        Private Sub Frm_Digitaliza_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Asc("-")) Then
                If TypeOf (Me.ActiveControl) Is TextBox Or UCase(TypeName(Me.ActiveControl)) = "TEXTBOXMASK" Then
                    Me.ActiveControl.Text = ""
                    e.Handled = True
                    'ElseIf TypeOf (Me.ActiveControl) Is ComboBox Then
                    '    If UCase(Me.ActiveControl.Name) <> "CMB_EXLUIDOLUGEXP" Then
                    '        CType(Me.ActiveControl, ComboBox).SelectedIndex = -1
                    '        CType(Me.ActiveControl, ComboBox).Text = ""
                    '    End If
                    '    e.Handled = True
                End If
            End If
            'If e.KeyChar = Microsoft.VisualBasic.ChrW(Asc("*")) Then
            '    If TypeOf (Me.ActiveControl) Is TextBox Or UCase(TypeName(Me.ActiveControl)) = "TEXTBOXMASK" Then
            '        If Trim(Me.ActiveControl.Text) = "?" Then Me.ActiveControl.Text = ""
            '        Me.ActiveControl.Text = Trim(Me.ActiveControl.Text) & "000"
            '        Select Case UCase(Me.ActiveControl.Name)
            '            Case "TXTRUTA" : TxtRuta.SelectionStart = Len(Me.TxtRuta.Text)
            '            Case "TXTCONSIGNA" : TxtConsigna.SelectionStart = Len(TxtConsigna.Text)
            '        End Select
            '        e.Handled = True
            '    End If
            'End If

        End Sub

        Private Sub Frm_Digitaliza_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            If MsgBox("Realmente quiere salir del programa?", 4 + 64 + 256, "Cerrar aplicación") = vbYes Then
                If ValidarCierreAplicativo() = True Then
                    Close()
                Else
                    MsgBox("Existen lotes sin exportar.. No se puede cerrar la aplicación aun.", 64, "Proceso pendiente.")
                    e.Cancel = True
                End If
            Else
                e.Cancel = True
            End If
        End Sub
        'Private Sub Frm_Digitaliza_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        '    ' Verificamos si la razón del cierre es por presionar Alt + F4
        '    If Control.ModifierKeys = Keys.Alt AndAlso e.CloseReason = CloseReason.UserClosing Then
        '        ' Evitamos que el formulario se cierre
        '        e.Cancel = True
        '        MessageBox.Show("No puedes cerrar la aplicación con Alt + F4.")
        '    End If
        'End Sub



#Region "Botones"
        'BOTONES PRINCIPALES
        Private Sub BtnDigitalizador_Click(sender As Object, e As EventArgs) Handles BtnDigitalizador.Click
            Me.CmbDigitaliza.Visible = True
        End Sub

        Private Sub BtnCrearCarpeta_Click_1(sender As Object, e As EventArgs) Handles BtnCrearCarpeta.Click
            If ParamTipologia.ConJornada = 1 Then
                If Not (Me.TxtNumJornada.Text >= 0 And Me.TxtNumJornada.Text <= 1) Then
                    MsgBox("Debes seleccionar una jornada valida..", 64, "Parametro pendiente.")
                    Me.TxtNumJornada.Focus()
                    Exit Sub
                End If
            End If
            If ParamTipologia.ConStiker = 1 Then
                If Len(Trim(Me.TxtNumStiker.Text)) < 5 Then
                    MsgBox("Debes ingresar un número de stiker..", 64, "Parametro pendiente.")
                    Me.TxtNumStiker.Focus()
                    Exit Sub
                End If
            End If
            CrearLote()
            RutaLoteNuevo = RutaLotes & "\" & Me.LblNumLote.Text
            CargarInformaciondelote(Me.LblNumLote.Text)
            CargarImagenesEnMiniatura(RutaLoteNuevo)
            Me.BtnDigitalizar.Enabled = True
            Me.PanEscaner.Visible = True
            Me.BtnEscanear.Focus()
            Me.BtnCrearCarpeta.Enabled = False

        End Sub
        Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
            If MsgBox("Realmente quiere salir del programa?", 4 + 64 + 256, "Cerrar aplicación") = vbYes Then
                If ValidarCierreAplicativo() = True Then
                    Close()
                Else
                    MsgBox("Existen lotes sin exportar.. No se puede cerrar la aplicación aun.", 64, "Proceso pendiente.")
                End If

            End If
        End Sub
        Private Sub BtnNewCarpeta_Click(sender As Object, e As EventArgs) Handles BtnNewCarpeta.Click
            Newlote = True
            NuevoLote()
        End Sub
        Private Sub BtnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click
            Me.PanEscaner.Visible = False
            Me.BtnDigitalizar.Enabled = True
        End Sub
        Private Sub BtnDigitalizar_Click(sender As Object, e As EventArgs) Handles BtnDigitalizar.Click
            Me.PanEscaner.Visible = True
        End Sub



        'BOTONES CONTROL ESCANER
        Private Sub BtnEscanear_Click(sender As Object, e As EventArgs) Handles BtnEscanear.Click
            If Trim(ParamTipologia.Numlote) = "" Then
                MsgBox("No hay un lote activo para iniciar la digitalización.  Verifique.")
                Exit Sub
            End If

            Me.DgvCarpetas.Enabled = False
            Me.BtnCerrar.Enabled = False
            Me.BtnExportarLotes.Enabled = False
            Me.BtnVolver.Enabled = False
            If _deviceManager IsNot Nothing Then
                Try : _deviceManager.Close() : Catch : End Try
            End If

            _deviceManager = New Vintasoft.Twain.DeviceManager(Me, Me.Handle)
            _deviceManager.Open()
            'StartImageAcquisitionButton()

            AcquireImagesFromTwainDeviceAndProcessImages()

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
                If Eslote = True And Len(Trim(lines(i))) > 2 Then
                    EntroaLote = True
                    If Mid(Trim(lines(i)), 1, 8) = "IMAGENES" Then
                        lines(i) = "IMAGENES: " & ParamTipologia.Imagenes
                    End If
                    If Mid(Trim(lines(i)), 1, 9) = "NUMIMAGEN" Then
                        lines(i) = "NUMIMAGEN: " & ParamTipologia.NumImgen
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
                    Dim linea As String = $"{Esimagen.IdImg},{Esimagen.Exportada},{Esimagen.NomImagen}"
                    writer.WriteLine(linea)
                Next
            End Using
            'FileOpen(1, ArchiLotes, OpenMode.Append)
            'PrintLine(1, "EXPORTADO: " & ParamTipologia.Exportado)
            'FileClose(1)
            Me.DgvCarpetas.Enabled = True
            Me.BtnCerrar.Enabled = True
            Me.BtnExportarLotes.Enabled = True
            Me.BtnVolver.Enabled = True
            '  MsgBox("Lote guardado.")
        End Sub
        Private Sub BtnEliminaPagina_Click(sender As Object, e As EventArgs) Handles BtnEliminaPagina.Click
            If MsgBox("Esta seguro que quiere eliminar la imagen activa.", 324, "Eliminiar documento.") = MsgBoxResult.No Then
                Exit Sub
            End If
            Dim Nombre As String = PicImagen.Tag
            Try
                PicImagen.Tag = ""
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
                PicImagen.Image = Nothing
            Catch ex As Exception
            End Try
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
            End If
            FLPImagenes.Controls.Clear()
            'Resto una imagen del control de lotes
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
                If Mid(Trim(lines(i)), 1, 4).ToUpper = "LOTE" Then
                    If Trim(Mid(lines(i), 6)) = Trim(ParamTipologia.Numlote) And Eslote = False Then
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

            'Borramos la imagen de la carpeta
            'Quitamos la imagen del picturebox para poder borrarla
            While File.Exists(RutaLoteNuevo & "\" & Nombre) = True
                Try
                    File.Delete(RutaLoteNuevo & "\" & Nombre)
                Catch ex As Exception
                    MsgBox(Err.Description)
                    Exit While
                End Try
            End While
            'Debemos guardar el indice de las imagenes sin la imagen borrada
            Dim ArchivoIndice As String = RutaLoteNuevo & "\" & "IndiceEscaneo.txt"
            If File.Exists(ArchivoIndice) = False Then
                FileOpen(1, ArchivoIndice, OpenMode.Output)
                FileClose(1)
            End If
            Dim TotImg As Integer = 0
            Using writer As New StreamWriter(ArchivoIndice)
                For Each Esimagen As ImagenEscaneada In Imagenes
                    If Esimagen.NomImagen <> Nombre Then
                        TotImg += 1
                        Esimagen.IdImg = TotImg
                        Dim linea As String = $"{Esimagen.IdImg},{Esimagen.Exportada},{Esimagen.NomImagen}"
                        writer.WriteLine(linea)
                    End If
                Next
            End Using
            'Debemos cargar de nuevo la informacion del lote
            ' CargarInformaciondelote(ParamTipologia.Numlote)
            CargarImagenesEnMiniatura(RutaLoteNuevo)

            Me.BtnEliminaPagina.Enabled = False
            MsgBox("Imagen borrada.")

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
                If Mid(Trim(lines(i)), 1, 4).ToUpper = "LOTE" Then
                    Eslote = False
                    If Trim(Mid(lines(i), 6)) = Trim(ParamTipologia.Numlote) And Eslote = False Then
                        Eslote = True
                        Continue For
                    End If
                End If
                If Eslote = False Then
                    PrintLine(1, lines(i))
                End If
            Next i
            FileClose(1)
            'Procedemos a borrar la carpeta con las imagenes
            If Directory.Exists(RutaLoteNuevo) = True Then
                Try
                    Try
                        PicImagen.Image = Nothing
                        PicImagen.Image = Nothing
                    Catch
                    End Try
                    FLPImagenes.Controls.Clear()
                    Directory.Delete(RutaLoteNuevo, True)
                Catch
                    MsgBox(Err.Description)
                End Try
            End If
            'limpiamos todos los controles y cargamos de nuevo las carpetas
            Me.LblNumLote.Text = ""
            'Me.LblTitulo.Text = ""
            Me.lblTipologia.Text = ""
            Me.TxtNumStiker.Text = ""
            Me.TxtNumJornada.Text = ""
            Me.TxtNomJornada.Text = ""
            Me.PicImagen.Image = Nothing
            Me.DgvRegistrosLote.Rows.Clear()
            Me.DgvCarpetas.Rows.Clear()
            FLPImagenes.Controls.Clear()

            If CargarLotesExistentes() = False Then
            End If

            MsgBox("Lote eliminado..")

            Me.PanEscaner.Visible = False
            Me.BtnDigitalizar.Enabled = True

        End Sub
        Private Sub BtnServidor_Click(sender As Object, e As EventArgs) Handles BtnServidor.Click
            RutaLotes = RutaImagSCan & "\" & Format(Me.DtpFecha.Value, "yyyyMMdd")
            CargarLotesExistentes()
            Me.BtnServidor.Enabled = False
        End Sub


#End Region

#Region "Sub y Function"

        Private Sub CargarEsquemas()

            DtEsquemas = New DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable
            Dim ClaseDigitaliza As New Cls_Digitaliza
            DtEsquemas = ClaseDigitaliza.ConsultaSiriesDocumentales
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
                CmbTipologia.Items.Add(Format(DtEsquemas.Rows(i).Item("id_Esquema"), "00") & " " & Trim(DtEsquemas.Rows(i).Item("Nombre_Esquema")).ToUpper)
            Next
        End Sub


        Private Sub CargarParametrosTipologia()
            If Trim(Me.CmbTipologia.Text) = "" Then
                Exit Sub
            End If
            Dim idEsquema As Integer = Val(Mid(Me.CmbTipologia.Text, 1, 2))
            If Not DtEsquemas.Rows.Count > 0 Then
                MsgBox("No existen series documentales cargadas para este proceso.. Verifique.")
                Exit Sub
            End If

            'fk_Entidad, fk_Proyecto, , Nombre_Esquema, fk_Tipo_Papel, Tipo_Papel, Resolucion_X, Resolucion_Y,
            'fk_Formato_Salida, Formato_Salida, Contraste, fk_Tipo_Imagen, Tipo_Imagen, Duplex
            For i As Integer = 0 To DtEsquemas.Rows.Count - 1
                If DtEsquemas.Rows(i).Item("id_Esquema") = idEsquema Then
                    ParamTipologia.idEntidad = DtEsquemas.Rows(i).Item("fk_Entidad")
                    ParamTipologia.idProyecto = DtEsquemas.Rows(i).Item("fk_Proyecto")
                    ParamTipologia.idEsquema = DtEsquemas.Rows(i).Item("id_Esquema")
                    ParamTipologia.EsquemaNombre = DtEsquemas.Rows(i).Item("Nombre_Esquema")
                    ParamTipologia.TamPapel = DtEsquemas.Rows(i).Item("fk_Tipo_Papel")
                    ParamTipologia.ResolucionX = DtEsquemas.Rows(i).Item("Resolucion_X")
                    ParamTipologia.ResolucionY = DtEsquemas.Rows(i).Item("Resolucion_Y")
                    ParamTipologia.FormatoSalida = DtEsquemas.Rows(i).Item("Formato_Salida")
                    ParamTipologia.Contraste = DtEsquemas.Rows(i).Item("Contraste")
                    ParamTipologia.TipoImagen = DtEsquemas.Rows(i).Item("fk_Tipo_Imagen")
                    ParamTipologia.Usuario = ""
                    ParamTipologia.RutaLote = ""
                    ParamTipologia.Numlote = ""
                    ParamTipologia.ConFecha = 0
                    ParamTipologia.FechaMovimiento = Format(Now(), "yyyy/MM/dd")
                    ParamTipologia.FechaProceso = Format(Now(), "yyyy/MM/dd")
                    ParamTipologia.DetFecha = ""
                    ParamTipologia.ConStiker = 0
                    ParamTipologia.Stiker = ""
                    ParamTipologia.DetStiker = ""
                    ParamTipologia.ConJornada = 0
                    ParamTipologia.Jornada = ""
                    ParamTipologia.DetJornada = ""
                    ParamTipologia.ConPrecinto = 0
                    ParamTipologia.Precinto = ""
                    ParamTipologia.DetPrecinto = ""
                    ParamTipologia.Imagenes = 0
                    ParamTipologia.NumImgen = 0
                    ParamTipologia.Estado = "ABIERTO"
                    ParamTipologia.HoraInactivo = ""
                    Exit For
                End If
            Next
            'Cargar campos a capturar en la tipologia
            Dim DtCampos As DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable
            Dim ClaseDigitaliza As New Cls_Digitaliza
            DtCampos = ClaseDigitaliza.ConsultaEsquemaCampo(ParamTipologia.idEsquema)
            If DtCampos Is Nothing Then
                MsgBox("Se presento un problema al consultar los campos... Verifique.")
                Exit Sub
            End If
            For i As Integer = 0 To DtCampos.Rows.Count - 1
                Select Case DtCampos.Rows(i).Item("Nombre_Campo").ToString.ToUpper.Trim
                    Case "FECHA_PROCESO"
                        ParamTipologia.ConFecha = 1
                    Case "JORNADA"
                        ParamTipologia.ConJornada = 1
                    Case "STIKER"
                        ParamTipologia.ConStiker = 1
                    Case Else
                        ParamTipologia.ConPrecinto = 1
                End Select
            Next

        End Sub


        Private Function ValidarCierreAplicativo() As Boolean
            ValidarCierreAplicativo = True
            CargarDetallesLotesCreados()
            If UBound(LotesLeidos) > 0 Then
                For i As Integer = 1 To UBound(LotesLeidos)
                    If LotesLeidos(i).Estado <> "ABIERTO" Then
                        Return False
                    End If
                Next
            End If
        End Function
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

        Private Sub DtpFecha_LostFocus(sender As Object, e As EventArgs) Handles DtpFecha.LostFocus
            Me.DtpFecha.Enabled = False
            Me.TxtNumJornada.Focus()
        End Sub
        Private Sub CmbDigitaliza_LostFocus(sender As Object, e As EventArgs) Handles CmbDigitaliza.LostFocus
            Me.LblDigitalizador.Text = Me.CmbDigitaliza.Text
            Me.CmbDigitaliza.Visible = False
            'Me.CmbTipologia.Focus()
        End Sub
        Private Sub CmbTipologia_LostFocus(sender As Object, e As EventArgs) Handles CmbTipologia.LostFocus
            If Trim(Me.CmbTipologia.Text) <> "" Then
                Me.lblTipologia.Text = Me.CmbTipologia.Text
                Me.CmbTipologia.Visible = False
                CargarParametrosTipologia()
                If ParamTipologia.ConStiker = 1 Then
                    Me.LblStiker.Visible = True
                    Me.TxtNumStiker.Visible = True
                Else
                    Me.LblStiker.Visible = False
                    Me.TxtNumStiker.Visible = False
                End If
                Me.DtpFecha.Focus()
                Me.DtpFecha.Show()
            End If
        End Sub
        Private Sub TxtNumJornada_LostFocus(sender As Object, e As EventArgs) Handles TxtNumJornada.LostFocus
            If Trim(Me.TxtNumJornada.Text) <> "" Then
                Select Case Val(Me.TxtNumJornada.Text)
                    Case 0 : Me.TxtNomJornada.Text = "NORMAL"
                    Case 1 : Me.TxtNomJornada.Text = "ADICIONAL"
                    Case Else
                        MsgBox("Jornada no valida.. Debes ingresar 0 o 1.", 64, "Error en jornada.")
                        Me.TxtNumJornada.Text = ""
                End Select
            End If
        End Sub
        Private Sub CmbDigitaliza_GotFocus(sender As Object, e As EventArgs) Handles CmbDigitaliza.GotFocus
            SendKeys.Send("{F4}")
        End Sub
        Private Sub CmbTipologia_GotFocus(sender As Object, e As EventArgs) Handles CmbTipologia.GotFocus
            SendKeys.Send("{F4}")
        End Sub
        Private Sub DtpFecha_GotFocus(sender As Object, e As EventArgs) Handles DtpFecha.GotFocus
            'If Newlote = True Then
            '    SendKeys.Send("{F4}")
            'End If
        End Sub

        Private Sub DgvCarpetas_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvCarpetas.CellDoubleClick
            If e.RowIndex >= 0 Then
                Dim fila As DataGridViewRow = DgvCarpetas.Rows(e.RowIndex)
                NombreLote = fila.Cells(0).Value.ToString()
                RutaLoteNuevo = RutaLotes & "\" & NombreLote
                CargarInformaciondelote(NombreLote)
                CargarImagenesEnMiniatura(RutaLoteNuevo)
                Me.BtnDigitalizar.Enabled = True
                Me.PanEscaner.Visible = True
                Me.BtnEliminaPagina.Enabled = False
                Me.BtnEscanear.Focus()
                CambiarColoresCarpetas()
            End If
        End Sub

        Private Sub CambiarColoresCarpetas()
            For Each fila As DataGridViewRow In DgvCarpetas.Rows
                ' Asegúrate de que la fila no sea una nueva fila (fila vacía al final)
                If Not fila.IsNewRow Then
                    Dim celda As DataGridViewCell = fila.Cells(0) ' Primera columna
                    If Not IsDBNull(celda.Value) AndAlso celda.Value.ToString() = NombreLote Then
                        fila.Selected = True
                        DgvCarpetas.DefaultCellStyle.SelectionBackColor = Color.DarkBlue
                        DgvCarpetas.DefaultCellStyle.SelectionForeColor = Color.White
                    Else
                        DgvCarpetas.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
                        DgvCarpetas.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
                    End If
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

            ToolTip.SetToolTip(Me.BtnNewCarpeta, Me.BtnNewCarpeta.Tag)
            ToolTip.SetToolTip(Me.BtnCrearCarpeta, Me.BtnCrearCarpeta.Tag)
            ToolTip.SetToolTip(Me.BtnDigitalizar, Me.BtnDigitalizar.Tag)
            ToolTip.SetToolTip(Me.BtnExportarLotes, Me.BtnExportarLotes.Tag)
            ToolTip.SetToolTip(Me.BtnCerrar, Me.BtnCerrar.Tag)
            ToolTip.SetToolTip(Me.BtnEscanear, Me.BtnEscanear.Tag)
            ToolTip.SetToolTip(Me.BtnEliminaPagina, Me.BtnEliminaPagina.Tag)
            ToolTip.SetToolTip(Me.BtnEliminaLote, Me.BtnEliminaLote.Tag)
            ToolTip.SetToolTip(Me.BtnVolver, Me.BtnVolver.Tag)
            ToolTip.SetToolTip(Me.DgvRegistrosLote, "Relación de documentos escaneados.")
            ToolTip.SetToolTip(Me.FLPImagenes, "Miniatura de imagenes escaneadas.")
            ToolTip.SetToolTip(Me.DgvCarpetas, "Carpetas de lotes escaneados.")
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
                    LotesLeidos(Tlot).Numlote = Trim(Mid(Linea, 6))
                    Select Case Trim(Mid(Trim(Linea), 1, InStr(Trim(Linea), ":") - 1).ToUpper)
                        Case "NUMLOTE" : LotesLeidos(Tlot).Numlote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "IDENTIDAD" : LotesLeidos(Tlot).idEntidad = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDPROYECTO" : LotesLeidos(Tlot).idProyecto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "IDESQUEMA" : LotesLeidos(Tlot).idEsquema = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESQUEMANOMBRE" : LotesLeidos(Tlot).EsquemaNombre = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :   ") + 1))
                        Case "TAMPAPEL" : LotesLeidos(Tlot).TamPapel = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RESOLUCIONX" : LotesLeidos(Tlot).ResolucionX = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "RESOLUCIONY" : LotesLeidos(Tlot).ResolucionY = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FORMATOSALIDA" : LotesLeidos(Tlot).FormatoSalida = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "CONTRASTE" : LotesLeidos(Tlot).Contraste = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TIPOIMAGEN" : LotesLeidos(Tlot).TipoImagen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "USUARIO" : LotesLeidos(Tlot).Usuario = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RUTALOTE" : LotesLeidos(Tlot).RutaLote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "CONFECHA" : LotesLeidos(Tlot).ConFecha = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FECHAMOVIMIENTO" : LotesLeidos(Tlot).FechaMovimiento = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "FECHAPROCESO" : LotesLeidos(Tlot).FechaProceso = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONSTIKER" : LotesLeidos(Tlot).ConStiker = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "STIKER" : LotesLeidos(Tlot).Stiker = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONJORNADA" : LotesLeidos(Tlot).ConJornada = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "JORNADA" : LotesLeidos(Tlot).Jornada = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONPRECINTO" : LotesLeidos(Tlot).ConPrecinto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "PRECINTO" : LotesLeidos(Tlot).Precinto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IMAGENES" : LotesLeidos(Tlot).Imagenes = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "NUMIMAGEN" : LotesLeidos(Tlot).NumImgen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESTADO" : LotesLeidos(Tlot).Estado = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :   ") + 1))
                        Case "HORAINACTIVO" : LotesLeidos(Tlot).HoraInactivo = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
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
                        Case "NUMLOTE" : ParamTipologia.Numlote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "IDENTIDAD" : ParamTipologia.idEntidad = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IDPROYECTO" : ParamTipologia.idProyecto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "IDESQUEMA" : ParamTipologia.idEsquema = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESQUEMANOMBRE" : ParamTipologia.EsquemaNombre = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :   ") + 1))
                        Case "TAMPAPEL" : ParamTipologia.TamPapel = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RESOLUCIONX" : ParamTipologia.ResolucionX = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "RESOLUCIONY" : ParamTipologia.ResolucionY = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FORMATOSALIDA" : ParamTipologia.FormatoSalida = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "CONTRASTE" : ParamTipologia.Contraste = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "TIPOIMAGEN" : ParamTipologia.TipoImagen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "USUARIO" : ParamTipologia.Usuario = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "RUTALOTE" : ParamTipologia.RutaLote = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "CONFECHA" : ParamTipologia.ConFecha = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "FECHAMOVIMIENTO" : ParamTipologia.FechaMovimiento = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "FECHAPROCESO" : ParamTipologia.FechaProceso = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONSTIKER" : ParamTipologia.ConStiker = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "STIKER" : ParamTipologia.Stiker = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONJORNADA" : ParamTipologia.ConJornada = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "JORNADA" : ParamTipologia.Jornada = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "CONPRECINTO" : ParamTipologia.ConPrecinto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "PRECINTO" : ParamTipologia.Precinto = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "IMAGENES" : ParamTipologia.Imagenes = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :  ") + 1))
                        Case "NUMIMAGEN" : ParamTipologia.NumImgen = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
                        Case "ESTADO" : ParamTipologia.Estado = Trim(Mid(Trim(Linea), InStr(Trim(Linea), " :   ") + 1))
                        Case "HORAINACTIVO" : ParamTipologia.HoraInactivo = Trim(Mid(Trim(Linea), InStr(Trim(Linea), ":") + 1))
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

            If ParamTipologia.ConFecha = 1 Then
                Me.LblFecha.Visible = True
                Me.DtpFecha.Visible = True
                Me.DtpFecha.Value = ParamTipologia.FechaMovimiento
            Else
                Me.LblFecha.Visible = False
                Me.DtpFecha.Visible = False
            End If

            If ParamTipologia.ConJornada = 1 Then
                Me.LblJornada.Visible = True
                Me.TxtNumJornada.Visible = True
                Me.TxtNomJornada.Visible = True
                Me.TxtNumJornada.Text = ParamTipologia.Jornada
                Select Case Trim(Me.TxtNumJornada.Text)
                    Case "0" : Me.TxtNomJornada.Text = "NORMAL"
                    Case "1" : Me.TxtNomJornada.Text = "ADICIONAL"
                    Case Else
                        MsgBox("Jornada no valida.. Debes ingresar 0 o 1.", 64, "Error en jornada.")
                        TxtNumJornada.Text = ""
                End Select
            Else
                Me.LblJornada.Visible = False
                Me.TxtNumJornada.Visible = False
                Me.TxtNomJornada.Visible = False
            End If

            If ParamTipologia.ConStiker = 1 Then
                Me.LblStiker.Visible = True
                Me.TxtNumStiker.Visible = True
                Me.TxtNumStiker.Text = ParamTipologia.Stiker
            Else
                Me.LblStiker.Visible = False
                Me.TxtNumStiker.Visible = False
            End If
            Me.CmbDigitaliza.Text = ParamTipologia.Usuario
            Me.LblDigitalizador.Text = ParamTipologia.Usuario
            Me.CmbTipologia.Text = ParamTipologia.EsquemaNombre
            Me.lblTipologia.Text = ParamTipologia.EsquemaNombre
            Me.DtpFecha.Value = ParamTipologia.FechaMovimiento
            RutaLoteNuevo = ParamTipologia.RutaLote

            CargarParametrosTipologia()
        End Sub
        Private Sub NuevoLote()
            Me.LblNumLote.Text = ""
            ' Me.LblTitulo.Text = ""
            Me.lblTipologia.Text = ""
            Me.TxtNumStiker.Text = ""
            Me.LblStiker.Visible = False
            Me.TxtNumStiker.Visible = False
            Me.DtpFecha.Enabled = True
            Me.CmbTipologia.Visible = True
            Me.TxtNumJornada.Text = ""
            Me.TxtNomJornada.Text = ""
            Me.BtnCrearCarpeta.Enabled = True
            Me.CmbDigitaliza.Visible = True
            Me.CmbDigitaliza.Text = Me.CmbDigitaliza.Items.Item(0)
            Me.LblDigitalizador.Text = Me.CmbDigitaliza.Text
            Me.PicImagen.Image = Nothing
            Me.DgvRegistrosLote.Rows.Clear()
            FLPImagenes.Controls.Clear()

            CmbTipologia.Focus()
        End Sub
        Private Sub CrearLote()
            If ValidarCampos() = False Then
                Exit Sub
            End If
            RutaLotes = RutaImagSCan & "\" & Format(Now(), "yyyyMMdd")
            RutaLoteNuevo = RutaLotes & "\" & Trim(SucursalIngreso) & "-"
            RutaLoteNuevo &= Trim(Mid(Me.lblTipologia.Text, 1, InStr(Me.lblTipologia.Text, "-") - 1)) & "-"
            RutaLoteNuevo &= Format(Now(), "yyyyMMdd") & Format(Now(), "HHmmss")
            Me.LblNumLote.Text = Mid(RutaLoteNuevo, InStrRev(RutaLoteNuevo, "\") + 1)
            If Directory.Exists(RutaLoteNuevo) = False Then
                Try
                    Directory.CreateDirectory(RutaLoteNuevo)
                Catch ex As Exception
                    MsgBox(Err.Description)
                    Exit Sub
                End Try
            End If
            If ParamTipologia.ConFecha = 1 Then
                ParamTipologia.FechaMovimiento = Format(Me.DtpFecha.Value, "yyyy/MM/dd")
            End If
            ParamTipologia.FechaProceso = Format(Now(), "yyyy/MM/dd")
            ParamTipologia.EsquemaNombre = Me.CmbTipologia.Text
            ParamTipologia.Usuario = Me.CmbDigitaliza.Text
            ParamTipologia.Numlote = Me.LblNumLote.Text
            ParamTipologia.RutaLote = RutaLoteNuevo
            ParamTipologia.Jornada = Me.TxtNumJornada.Text
            ParamTipologia.Stiker = Me.TxtNumStiker.Text
            ParamTipologia.Imagenes = 0
            ParamTipologia.NumImgen = 0
            ParamTipologia.Estado = "ABIERTO"
            TImagen = -1
            ReDim Imagenes(0)
            GuardarLoteCreado()
            CargarLotesExistentes()
        End Sub
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
            PrintLine(1, "ESQUEMANOMBRE: " & ParamTipologia.EsquemaNombre)
            PrintLine(1, "TAMPAPEL: " & ParamTipologia.TamPapel)
            PrintLine(1, "RESOLUCIONX: " & ParamTipologia.ResolucionX)
            PrintLine(1, "RESOLUCIONY: " & ParamTipologia.ResolucionY)
            PrintLine(1, "FORMATOSALIDA: " & ParamTipologia.FormatoSalida)
            PrintLine(1, "CONTRASTE: " & ParamTipologia.Contraste)
            PrintLine(1, "TIPOIMAGEN: " & ParamTipologia.TipoImagen)
            PrintLine(1, "USUARIO: " & ParamTipologia.Usuario)
            PrintLine(1, "RUTALOTE: " & ParamTipologia.RutaLote)
            PrintLine(1, "CONFECHA: " & ParamTipologia.ConFecha)
            PrintLine(1, "FECHAMOVIMIENTO: " & ParamTipologia.FechaMovimiento)
            PrintLine(1, "FECHAPROCESO: " & ParamTipologia.FechaProceso)
            PrintLine(1, "CONSTIKER: " & ParamTipologia.ConStiker)
            PrintLine(1, "STIKER: " & ParamTipologia.Stiker)
            PrintLine(1, "CONJORNADA: " & ParamTipologia.ConJornada)
            PrintLine(1, "JORNADA: " & ParamTipologia.Jornada)
            PrintLine(1, "CONPRECINTO: " & ParamTipologia.ConPrecinto)
            PrintLine(1, "PRECINTO: " & ParamTipologia.Precinto)
            PrintLine(1, "IMAGENES: " & ParamTipologia.Imagenes)
            PrintLine(1, "NUMIMAGEN: " & ParamTipologia.NumImgen)
            PrintLine(1, "ESTADO: " & ParamTipologia.Estado)
            PrintLine(1, "HORAINACTIVO: " & ParamTipologia.HoraInactivo)
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
                Me.BtnExportarLotes.Enabled = True
                If UBound(LotesLeidos) > 0 Then
                    For i As Integer = 1 To UBound(LotesLeidos)
                        If LotesLeidos(i).Numlote = info.Name Then
                            IndexDgv = DgvCarpetas.Rows.Add(info.Name, LotesLeidos(i).Estado, LotesLeidos(i).Imagenes)
                            'Validar estado para cambiar de color
                            Select Case LotesLeidos(i).Estado
                                Case "ABIERTO"
                                    If info.Name = Trim(Me.LblNumLote.Text) Then
                                        DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.BackColor = Color.White
                                        DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.ForeColor = Color.Black
                                    Else
                                        DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.BackColor = Color.Blue
                                        DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.ForeColor = Color.Black
                                    End If
                                Case "CERRADO"
                                    DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.BackColor = Color.Red
                                    DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.ForeColor = Color.Black
                                Case "TRANSMITIENDO"
                                    DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.BackColor = Color.DarkRed
                                    DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.ForeColor = Color.Black
                                Case "TRANSMITIDO"
                                    DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.BackColor = Color.Black
                                    DgvCarpetas.Rows(IndexDgv).DefaultCellStyle.ForeColor = Color.White
                            End Select
                            Exit For
                        End If
                    Next
                End If
            Next


        End Function

        Private Function ValidarCampos() As Boolean
            ValidarCampos = True
            If Not (Val(Me.TxtNumJornada.Text) = 0 Or Val(Me.TxtNumJornada.Text) = 1) Then
                MsgBox("Debes ingresar la jornada de digitalización.", 64, "Campo pendiente.")
                Me.TxtNumJornada.Focus()
                Return False
            End If
            If Trim(Me.lblTipologia.Text) = "" Then
                MsgBox("Debes seleccionar el tipo de documento a digitalizar.", 64, "Campo pendiente.")
                Me.BtnNewCarpeta.Focus()
                Return False
            End If

        End Function
        Private Sub MostrarImagen(RutaImg As String, NombreImg As String)
            Try
                PicImagen.Image = Nothing
                PicImagen.Image = Nothing
            Catch
            End Try
            'PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(RutaImg)))
            PicImagen.Image = Image.FromFile(RutaImg)
            PicImagen.Tag = NombreImg
            PicImagen.SizeMode = PictureBoxSizeMode.StretchImage
            Me.BtnEliminaPagina.Enabled = True
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
                                            .Where(Function(f) f.EndsWith(".jpg") Or f.EndsWith(".jpeg") Or f.EndsWith(".tif")).ToArray()

                ' Recorrer todos los archivos de imagen
                Dim TImg As Integer = 0
                DgvRegistrosLote.Rows.Clear()
                For Each ArcImagen As String In archivosImagen
                    ' Moscar en relacion en GridView
                    TImg += 1
                    DgvRegistrosLote.Rows.Add(TImg, System.IO.Path.GetFileName(ArcImagen))

                    Dim img As Image = Image.FromFile(ArcImagen)
                    ' Generar la miniatura de la imagen
                    Dim thumb As Image = img.GetThumbnailImage(100, 50, Nothing, IntPtr.Zero)
                    ' Crear un PictureBox para mostrar la miniatura
                    Dim PictureMini As New PictureBox() With {
                    .Image = thumb,
                    .Name = System.IO.Path.GetFileName(ArcImagen),
                    .Margin = New Padding(5),
                    .SizeMode = PictureBoxSizeMode.StretchImage}
                    AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
                    ' Agregar el PictureBox al FlowLayoutPanel
                    FLPImagenes.Controls.Add(PictureMini)
                    ' Liberar los recursos de la imagen original
                    img.Dispose()

                    '' Crear una imagen miniatura
                    'Dim miniatura As Image = CrearMiniatura(ArcImagen)
                    '' Crear un PictureBox para mostrar la miniatura
                    'Dim PictureMini As New PictureBox With {
                    '    .Image = miniatura,
                    '    .SizeMode = PictureBoxSizeMode.StretchImage,
                    '    .Name = System.IO.Path.GetFileName(ArcImagen),
                    '    .Margin = New Padding(5)}
                    ''.Width = 100, ' Ancho de la miniatura
                    ''.Height = 50, ' Alto de la miniatura
                    'AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
                    '' Añadir la miniatura al FlowLayoutPanel
                    'FLPImagenes.Controls.Add(PictureMini)
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
        Private Function CrearMiniatura(rutaImagen As String) As Image
            Dim imagenOriginal As Image = Image.FromFile(rutaImagen)
            Dim tamanioMiniatura As New Size(100, 100) ' Tamaño deseado para las miniaturas
            ' Crear la miniatura manteniendo la proporción
            Dim imagenMiniatura As Image = imagenOriginal.GetThumbnailImage(tamanioMiniatura.Width, tamanioMiniatura.Height, Nothing, IntPtr.Zero)
            Return imagenMiniatura
        End Function



        Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
            Close()
        End Sub

#End Region

#Region "Mouse Move_Leave"
        Private Sub BtnEscanear_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnEscanear.MouseMove
            Me.BtnEscanear.Size = New Size(45, 45)
        End Sub
        Private Sub BtnEscanear_MouseLeave(sender As Object, e As EventArgs) Handles BtnEscanear.MouseLeave
            Me.BtnEscanear.Size = New Size(35, 35)
        End Sub
        Private Sub BtnEliminaPagina_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnEliminaPagina.MouseMove
            Me.BtnEliminaPagina.Size = New Size(45, 45)
        End Sub
        Private Sub BtnEliminaPagina_MouseLeave(sender As Object, e As EventArgs) Handles BtnEliminaPagina.MouseLeave
            Me.BtnEliminaPagina.Size = New Size(35, 35)
        End Sub
        Private Sub BtnEliminaLote_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnEliminaLote.MouseMove
            Me.BtnEliminaLote.Size = New Size(45, 45)
        End Sub
        Private Sub BtnEliminaLote_MouseLeave(sender As Object, e As EventArgs) Handles BtnEliminaLote.MouseLeave
            Me.BtnEliminaLote.Size = New Size(35, 35)
        End Sub
        Private Sub BtnVolver_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnVolver.MouseMove
            Me.BtnVolver.Size = New Size(45, 45)
        End Sub
        Private Sub BtnVolver_MouseLeave(sender As Object, e As EventArgs) Handles BtnVolver.MouseLeave
            Me.BtnVolver.Size = New Size(35, 35)
        End Sub
        Private Sub BtnNewCarpeta_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnNewCarpeta.MouseMove
            Me.BtnNewCarpeta.Size = New Size(45, 45)
        End Sub
        Private Sub BtnNewCarpeta_MouseLeave(sender As Object, e As EventArgs) Handles BtnNewCarpeta.MouseLeave
            Me.BtnNewCarpeta.Size = New Size(35, 35)
        End Sub
        Private Sub BtnServidor_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnServidor.MouseMove
            Me.BtnServidor.Size = New Size(45, 45)
        End Sub
        Private Sub BtnServidor_MouseLeave(sender As Object, e As EventArgs) Handles BtnServidor.MouseLeave
            Me.BtnServidor.Size = New Size(35, 35)
        End Sub
        Private Sub BtnCrearCarpeta_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnCrearCarpeta.MouseMove
            Me.BtnCrearCarpeta.Size = New Size(45, 45)
        End Sub
        Private Sub BtnCrearCarpeta_MouseLeave(sender As Object, e As EventArgs) Handles BtnCrearCarpeta.MouseLeave
            Me.BtnCrearCarpeta.Size = New Size(35, 35)
        End Sub
        Private Sub BtnDigitalizar_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnDigitalizar.MouseMove
            Me.BtnDigitalizar.Size = New Size(45, 45)
        End Sub
        Private Sub BtnDigitalizar_MouseLeave(sender As Object, e As EventArgs) Handles BtnDigitalizar.MouseLeave
            Me.BtnDigitalizar.Size = New Size(35, 35)
        End Sub
        Private Sub BtnExportarLotes_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnExportarLotes.MouseMove
            Me.BtnExportarLotes.Size = New Size(45, 45)
        End Sub
        Private Sub BtnExportarLotes_MouseLeave(sender As Object, e As EventArgs) Handles BtnExportarLotes.MouseLeave
            Me.BtnExportarLotes.Size = New Size(35, 35)
        End Sub
        Private Sub BtnCerrar_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnCerrar.MouseMove
            Me.BtnCerrar.Size = New Size(45, 45)
        End Sub
        Private Sub BtnCerrar_MouseLeave(sender As Object, e As EventArgs) Handles BtnCerrar.MouseLeave
            Me.BtnCerrar.Size = New Size(35, 35)
        End Sub

        Private Sub BtnAddImagen_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAddImagen.MouseMove
            Me.BtnCerrar.Size = New Size(45, 45)
        End Sub
        Private Sub BtnAddImagen_MouseLeave(sender As Object, e As EventArgs) Handles BtnAddImagen.MouseLeave
            Me.BtnCerrar.Size = New Size(45, 45)
        End Sub

        Private Sub BtnAddCarpeta_MouseMove(sender As Object, e As MouseEventArgs) Handles BtnAddCarpeta.MouseMove
            Me.BtnCerrar.Size = New Size(45, 45)
        End Sub
        Private Sub BtnAddCarpeta_MouseLeave(sender As Object, e As EventArgs) Handles BtnAddCarpeta.MouseLeave
            Me.BtnCerrar.Size = New Size(45, 45)
        End Sub

#End Region




#Region "Metodos Escaner ScanJetPro 3000 s4"

        ''' <summary>
        ''' Starts the image acquisition from scanner.
        ''' </summary>
        Private Sub StartImageAcquisitionButton()

            Dim device As Vintasoft.Twain.Device = Nothing
            Try
                ' select the default device
                '  _deviceManager.ShowDefaultDeviceSelectionDialog()

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
                ' device.PageAutoSize = PageAutoSize.Auto
                'device.PageOrientation = PageOrientation.Auto
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
            Dim NuevaImagen As String = ParamTipologia.Numlote & Format(ParamTipologia.NumImgen + 1, "000") & "." & ParamTipologia.FormatoSalida
            e.Image.Save(IO.Path.Combine(ParamTipologia.RutaLote, NuevaImagen))
            e.Image.Dispose()
            ParamTipologia.NumImgen += 1
            ParamTipologia.Imagenes += 1
            PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ParamTipologia.RutaLote & "\" & NuevaImagen)))
            PicImagen.Tag = NuevaImagen
            PicImagen.SizeMode = PictureBoxSizeMode.StretchImage

            DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
            TImagen += 1
            ReDim Preserve Imagenes(TImagen)
            Imagenes(TImagen).IdImg = TImagen + 1
            Imagenes(TImagen).Exportada = 0
            Imagenes(TImagen).NomImagen = NuevaImagen

            Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
            Dim PictureMini As New PictureBox With {
            .Image = miniatura,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Name = System.IO.Path.GetFileName(RutaLoteNuevo & "\" & NuevaImagen),
            .Margin = New Padding(5)}
            AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
            FLPImagenes.Controls.Add(PictureMini)
            IdRImg = DgvRegistrosLote.Rows.Count
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
        Public Shared Sub AcquireImagesFromTwainDeviceAndProcessImages()
            ' create the device manager
            Using deviceManager As New Vintasoft.Twain.DeviceManager()
                ' open the device manager
                deviceManager.Open()

                ' select the device in the default device selectio ndialog
                deviceManager.ShowDefaultDeviceSelectionDialog()

                ' get reference to the selected device
                Dim device As Vintasoft.Twain.Device = deviceManager.DefaultDevice

                ' specify that device UI must not be used
                device.ShowUI = False
                ' specify that device must be closed after scan
                device.DisableAfterAcquire = True

                Dim tiffFilename As String = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "multipage.tif")

                ' acquire images from device
                Dim acquireModalState As Vintasoft.Twain.AcquireModalState = Vintasoft.Twain.AcquireModalState.None
                Dim acquiredImage As Vintasoft.Twain.AcquiredImage
                Do
                    acquireModalState = device.AcquireModal()
                    Select Case acquireModalState
                        Case Vintasoft.Twain.AcquireModalState.ImageAcquired
                            ' get reference to the image acquired from device
                            acquiredImage = device.AcquiredImage

                            ' despeckle/deskew/detect border
                            ProcessAcquiredImage(acquiredImage)
                            ' add image to multipage TIFF file if image is not blank
                            If Not acquiredImage.IsBlank(0.01F) Then
                                acquiredImage.Save(tiffFilename)
                            End If

                            ' dispose acquired image
                            acquiredImage.Dispose()
                            Exit Select
                    End Select
                Loop While acquireModalState <> Vintasoft.Twain.AcquireModalState.None

                ' close the device
                device.Close()

                ' close the device manager
                deviceManager.Close()
            End Using
        End Sub

        Private Shared Sub ProcessAcquiredImage(acquiredImage As Vintasoft.Twain.AcquiredImage)
            System.Console.WriteLine(String.Format("Image ({0})", acquiredImage.ImageInfo))

            Try
                ' subscribe to processing events
                AddHandler acquiredImage.Processing, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingEventArgs)(AddressOf AcquiredImage_Processing)
                AddHandler acquiredImage.Progress, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingProgressEventArgs)(AddressOf AcquiredImage_Progress)
                AddHandler acquiredImage.Processed, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessedEventArgs)(AddressOf AcquiredImage_Processed)

                ' despeckle/deskew/detect border
                acquiredImage.Despeckle(8, 25, 30, 400)
                acquiredImage.Deskew(Vintasoft.Twain.ImageProcessing.TwainBorderColor.AutoDetect, 5, 5)
                acquiredImage.DetectBorder(5)
            Catch ex As System.Exception
                'System.Console.WriteLine("Error: " & ex.Message)
                MsgBox(ex.Message)
            Finally
                ' unsubscribe from processing events
                RemoveHandler acquiredImage.Processing, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingEventArgs)(AddressOf AcquiredImage_Processing)
                RemoveHandler acquiredImage.Progress, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingProgressEventArgs)(AddressOf AcquiredImage_Progress)
                RemoveHandler acquiredImage.Processed, New System.EventHandler(Of Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessedEventArgs)(AddressOf AcquiredImage_Processed)
            End Try
        End Sub
        Private Shared Sub AcquiredImage_Processing(sender As Object, e As Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingEventArgs)
            ' System.Console.Write(e.Action.ToString() & " ")
        End Sub
        Private Shared Sub AcquiredImage_Progress(sender As Object, e As Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessingProgressEventArgs)
            ' System.Console.Write(".")
        End Sub
        Private Shared Sub AcquiredImage_Processed(sender As Object, e As Vintasoft.Twain.ImageProcessing.TwainAcquiredImageProcessedEventArgs)
            ' System.Console.WriteLine(" finished")
            MsgBox(" finished")
        End Sub
#End Region

        Private Sub BtnAddImagen_Click(sender As Object, e As EventArgs) Handles BtnAddImagen.Click
            Dim openFileDialog As New OpenFileDialog() With {
        .Title = "Seleccione archivo a adiccionar.",
        .Filter = "Todos los archivos (*.*)|*.*"}
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim ArcSeleccionado As String = openFileDialog.FileName
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
                        CargarArchivoPdf(ArcSeleccionado)
                        IdRImg = DgvRegistrosLote.Rows.Count
                    Case Else
                        MsgBox("Archivo seleccionado no valido para este proceso.." & Chr(13) &
                        "Formatos validos (png,tif,jpg,tiff,pdf)", 64, "Error en formato de archivo.")
                        Exit Sub
                End Select

            End If
        End Sub

        Private Sub CargarArchivoPng(ArchivoImagen As String)
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
            End If
            Me.BtnEliminaPagina.Enabled = True
            Dim NuevaImagen As String = ParamTipologia.Numlote & Format(ParamTipologia.NumImgen + 1, "000") & "." & ParamTipologia.FormatoSalida

            PicImagen.Image = Image.FromFile(ArchivoImagen)
            PicImagen.Image.Save(ParamTipologia.RutaLote & NuevaImagen, ImageFormat.Tiff)

            ParamTipologia.NumImgen += 1
            ParamTipologia.Imagenes += 1
            'PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ParamTipologia.RutaLote & "\" & NuevaImagen)))
            PicImagen.Tag = NuevaImagen
            PicImagen.SizeMode = PictureBoxSizeMode.StretchImage

            DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
            TImagen += 1
            ReDim Preserve Imagenes(TImagen)
            Imagenes(TImagen).IdImg = TImagen + 1
            Imagenes(TImagen).Exportada = 0
            Imagenes(TImagen).NomImagen = NuevaImagen

            Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
            Dim PictureMini As New PictureBox With {
            .Image = miniatura,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Name = System.IO.Path.GetFileName(RutaLoteNuevo & "\" & NuevaImagen),
            .Margin = New Padding(5)}
            AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
            FLPImagenes.Controls.Add(PictureMini)

        End Sub

        Private Sub CargarArchivoTiff(ArchivoTiff As String)
            If PicImagen.Image IsNot Nothing Then
                PicImagen.Image.Dispose()
                PicImagen.Image = Nothing
            End If
            Me.BtnEliminaPagina.Enabled = True

            Dim imagenOriginal As Image = Image.FromFile(ArchivoTiff)
            ' Obtener la dimensión de frames (páginas)
            Dim dimension As New FrameDimension(imagenOriginal.FrameDimensionsList(0))
            Dim numPaginas As Integer = imagenOriginal.GetFrameCount(dimension)

            For i As Integer = 0 To numPaginas - 1
                imagenOriginal.SelectActiveFrame(dimension, i)
                Dim bitmapPagina As New Bitmap(imagenOriginal)

                ' Guardar cada página como TIFF individual
                Dim NuevaImagen As String = ParamTipologia.Numlote & Format(ParamTipologia.NumImgen + 1, "000") & "." & ParamTipologia.FormatoSalida
                bitmapPagina.Save(ParamTipologia.RutaLote & NuevaImagen, ImageFormat.Tiff)
                bitmapPagina.Dispose()

                ParamTipologia.NumImgen += 1
                ParamTipologia.Imagenes += 1
                PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ParamTipologia.RutaLote & "\" & NuevaImagen)))
                PicImagen.Tag = NuevaImagen
                PicImagen.SizeMode = PictureBoxSizeMode.StretchImage

                DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
                TImagen += 1
                ReDim Preserve Imagenes(TImagen)
                Imagenes(TImagen).IdImg = TImagen + 1
                Imagenes(TImagen).Exportada = 0
                Imagenes(TImagen).NomImagen = NuevaImagen

                Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
                Dim PictureMini As New PictureBox With {
                .Image = miniatura,
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Name = System.IO.Path.GetFileName(RutaLoteNuevo & "\" & NuevaImagen),
                .Margin = New Padding(5)}
                AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
                FLPImagenes.Controls.Add(PictureMini)
            Next
            imagenOriginal.Dispose()
        End Sub

        Private Sub CargarArchivoPdf(ArchivoPdf As String)
            'Dim documento As PdfiumViewer.PdfDocument
            'documento = PdfiumViewer.PdfDocument.Load(ArchivoPdf)
            'Me.BtnEliminaPagina.Enabled = True
            'For indice As Integer = 0 To documento.PageCount - 1
            '    ' Renderizar la página como imagen
            '    If PicImagen.Image IsNot Nothing Then
            '        PicImagen.Image.Dispose()
            '        PicImagen.Image = Nothing
            '    End If
            '    Dim imagen As Image = documento.Render(indice, PicImagen.Width, PicImagen.Height, True)
            '    PicImagen.Image = imagen
            '    Dim NuevaImagen As String = ParamTipologia.Numlote & Format(ParamTipologia.NumImgen + 1, "000") & "." & ParamTipologia.FormatoSalida
            '    PicImagen.Image.Save(ParamTipologia.RutaLote & NuevaImagen, ImageFormat.Tiff)

            '    ParamTipologia.NumImgen += 1
            '    ParamTipologia.Imagenes += 1
            '    'PicImagen.Image = System.Drawing.Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ParamTipologia.RutaLote & "\" & NuevaImagen)))
            '    PicImagen.Tag = NuevaImagen
            '    PicImagen.SizeMode = PictureBoxSizeMode.StretchImage

            '    DgvRegistrosLote.Rows.Add(ParamTipologia.Imagenes, System.IO.Path.GetFileName(NuevaImagen))
            '    TImagen += 1
            '    ReDim Preserve Imagenes(TImagen)
            '    Imagenes(TImagen).IdImg = TImagen + 1
            '    Imagenes(TImagen).Exportada = 0
            '    Imagenes(TImagen).NomImagen = NuevaImagen

            '    Dim miniatura As Image = CrearMiniatura(RutaLoteNuevo & "\" & NuevaImagen)
            '    Dim PictureMini As New PictureBox With {
            '    .Image = miniatura,
            '    .SizeMode = PictureBoxSizeMode.StretchImage,
            '    .Name = System.IO.Path.GetFileName(RutaLoteNuevo & "\" & NuevaImagen),
            '    .Margin = New Padding(5)}
            '    AddHandler PictureMini.MouseDoubleClick, AddressOf PictureMini_DoubleClick
            '    FLPImagenes.Controls.Add(PictureMini)
            'Next
        End Sub


        Private Sub BtnAddCarpeta_Click(sender As Object, e As EventArgs) Handles BtnAddCarpeta.Click
            Dim folderDialog As New FolderBrowserDialog() With {
         .Description = "Seleccionar carpeta a cargar."}

            If folderDialog.ShowDialog() = DialogResult.OK Then
                Dim CarpSeleccionada As String = folderDialog.SelectedPath
                'Verificar si la carpeta contiene imagenes validas para cargar

                Dim extensionesPermitidas As String() = {".jpg", ".png", ".tif", ".tiff", ".pdf"}
                ' Obtener todos los archivos de la carpeta
                Dim archivos As String() = Directory.GetFiles(CarpSeleccionada)

                ' Filtrar solo los archivos con extensiones deseadas
                Dim archivosFiltrados = From archivo In archivos
                                        Where extensionesPermitidas.Contains(Path.GetExtension(archivo).ToLower())
                                        Select archivo

                ' Mostrar en un ListBox, DataGridView o MessageBox
                For Each archivo In archivosFiltrados
                    Select Case Path.GetExtension(archivo).ToLower
                        Case "png"
                            CargarArchivoPng(archivo)
                        Case "tif"
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
                        Case "jpg"
                            CargarArchivoPng(archivo)
                        Case "tiff"
                            CargarArchivoTiff(archivo)
                        Case "pdf"
                            CargarArchivoPdf(archivo)
                        Case Else
                            MsgBox("Archivo seleccionado no valido para este proceso.." & Chr(13) &
                            "Formatos validos (png,tif,jpg,tiff,pdf)", 64, "Error en formato de archivo.")
                            Exit Sub
                    End Select
                Next
                IdRImg = DgvRegistrosLote.Rows.Count
            End If

        End Sub

        Private Sub BntPrimero_Click(sender As Object, e As EventArgs) Handles BntPrimero.Click
            If DgvRegistrosLote.Rows.Count > 0 Then
                IdRImg = 0
                MostrarImagen()
            End If
        End Sub

        Private Sub BtnAnterior_Click(sender As Object, e As EventArgs) Handles BtnAnterior.Click
            If IdRImg > 0 Then IdRImg -= 1
            MostrarImagen()
        End Sub

        Private Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles BtnSiguiente.Click
            If IdRImg < DgvRegistrosLote.Rows.Count Then IdRImg += 1
            MostrarImagen()
        End Sub

        Private Sub BtnUltimo_Click(sender As Object, e As EventArgs) Handles BtnUltimo.Click
            If DgvRegistrosLote.Rows.Count > 0 Then
                IdRImg = DgvRegistrosLote.Rows.Count
                MostrarImagen()
            End If
        End Sub
        Private Sub MostrarImagen()
            Dim filaSeleccionada As DataGridViewRow = DgvRegistrosLote.Rows(IdRImg)
            Dim NomImagen As String = filaSeleccionada.Cells("Nombre").Value.ToString()
            MostrarImagen(RutaLoteNuevo & "\" & NomImagen, NomImagen)
        End Sub

        Private Sub BntGiraDerecha_Click(sender As Object, e As EventArgs) Handles BntGiraDerecha.Click
            'If Giro = 1 Or Giro = 3 Then PicImagen.ClientSize = New System.Drawing.Point(653, 284)
            'If Giro = 0 Or Giro = 2 Then PicImagen.ClientSize = New System.Drawing.Point(284, 653)
            'Giro += 1
            Dim bmp As Image = PicImagen.Image
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone)
            PicImagen.Image = bmp
            'If Giro = 4 Then Giro = 0
        End Sub


        Private Sub BtnNormal_Click(sender As Object, e As EventArgs) Handles BtnNormal.Click
            PicImagen.SizeMode = PictureBoxSizeMode.StretchImage
            PicImagen.ClientSize = New System.Drawing.Point(736, 593)
            PicImagen.Image = PicA.Image
            'If TImagen = 1 Then
            '    PicImagen.Image = PicA.Image
            'Else
            '    PicImagen.Image = PicR.Image
            'End If
        End Sub

        Private Sub BtnAutoTamaño_Click(sender As Object, e As EventArgs) Handles BtnAutoTamaño.Click
            PicImagen.SizeMode = PictureBoxSizeMode.AutoSize
        End Sub
    End Class


End Namespace