Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Specialized
Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging

Namespace Procesos.Cargue

    Public MustInherit Class CargueBase

#Region " Declaraciones "

        Protected _EventManager As EventManager

        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        Protected _Paquetes As StringCollection

        Protected _Cargue As New DBImaging.Esquemas.xsdCargue
        Protected ProgressForm As New FormProgress

        Protected idPaquete As Short

        Protected _Validos As Integer
        Protected _Invalidos As Integer

        Protected _idCargue As Integer

        Protected _OT As Integer
        Protected _FechaProceso As DateTime
        Protected _LoadImagesDirectories As Boolean
        Protected _ExtensionAux As String


        Protected Class RenamePath
            Public Property Path As String
            Public Property Cargue As Integer
            Public Property Paquete As Short

            Public Sub New(nPath As String, nCargue As Integer, nPaquete As Short)
                Me.Path = nPath.TrimEnd("\"c)
                Me.Cargue = nCargue
                Me.Paquete = nPaquete
            End Sub

            Public Sub RenameDirectory()
                Try
                    Directory.Move(Me.Path, Me.Path & "." & Me.Cargue & "." & Me.Paquete & "#")
                Catch
                End Try
            End Sub

        End Class

#End Region

#Region " Propiedades "

        Public ReadOnly Property Datos() As DBImaging.Esquemas.xsdCargue
            Get
                Return _Cargue
            End Get
        End Property

        Public ReadOnly Property Validos() As Integer
            Get
                Return _Validos
            End Get
        End Property

        Public ReadOnly Property Invalidos() As Integer
            Get
                Return _Invalidos
            End Get
        End Property

        Public ReadOnly Property Paquetes As StringCollection
            Get
                Return _Paquetes
            End Get
        End Property

        Public ReadOnly Property EventManager As EventManager
            Get
                Return Me._EventManager
            End Get
        End Property

#End Region

#Region " Metodos "

        Public Sub Run()
            Try
                ProgressForm.SetProceso("Cargue")
                ProgressForm.SetProgreso(0)
                ProgressForm.SetMaxValue(100)

                ProgressForm.SetAccion("Leer directorios")

                _ExtensionAux = IIf(Program.ImagingGlobal.ProyectoImagingRow.Usa_Cargue_PDF, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada).ToString()

                _Cargue.Clear()

                Dim FechaInicioProceso = Now

                If Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Cargue_Masivo Then
                    LoadImagesDirectories()
                Else
                    LoadImagesDirectories_CargueMasivo()
                End If

                If (_LoadImagesDirectories) Then
                    If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                    Dim idDirectorio As Integer = 0

                    idPaquete = 0

                    For Each Directorio As String In _Paquetes
                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                        idDirectorio += 1

                        ProgressForm.SetAccion("Leer imágenes - directorio " & idDirectorio & " de " & _Paquetes.Count)

                        Dim RowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow = Nothing

                        If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                            idPaquete = CShort(idPaquete + 1)

                            RowPaquete = _Cargue.Paquete.AddPaqueteRow(idPaquete, Directorio, 0, 0, 0, True, "", "", 0, 0, "")
                        End If

                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Archivo_Indices) Then
                            LoadImagesIndice(Directorio, RowPaquete)
                        Else
                            LoadImagesNoIndice(Directorio, RowPaquete)
                        End If
                    Next

                    If (_Cargue.Paquete.Rows.Count = 0) Then
                        Throw New Exception("No se encontraron imagenes para indexar")
                    Else
                        Validar()

                        '-------------------------------------------------------------------------------------------------------------
                        ' LOGGING - PERFORMANCE
                        '-------------------------------------------------------------------------------------------------------------
                        Dim FechaFinProceso = Now

                        Dim TraceMessage As String = ""
                        TraceMessage &= "Duración:" & vbTab & (FechaInicioProceso - FechaFinProceso).TotalMilliseconds & vbTab
                        DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Cargue][Validar]")
                        '-------------------------------------------------------------------------------------------------------------

                        ProgressForm.Hide()

                        Dim CargueForm As New FormCargue
                        CargueForm._OT = _OT
                        CargueForm._FechaProceso = Integer.Parse(String.Format("{0:yyyyMMdd}", Me._FechaProceso))
                        CargueForm.setData(Me)

                        ' If CargueForm.CorrespondenciaDestapeImagenes = True Then

                        Dim Respuesta = CargueForm.ShowDialog()

                        If (Respuesta = DialogResult.OK) Then
                            If (Not Program.ImagingGlobal.ProyectoImagingRow.Aplica_Fisico AndAlso Program.ImagingGlobal.ProyectoImagingRow.Usa_Indexacion) Then
                                CargarConIndexacion()
                            Else
                                CargarSinIndexacion()
                            End If
                        End If
                        'End If
                    End If
                End If

            Catch ex As Exception
                ProgressForm.Hide()
                Application.DoEvents()

                MessageBox.Show(ex.Message, "Cargue", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ProgressForm.Close()

            End Try
        End Sub

        Private Sub LoadImagesIndice(ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow)
            Try
                Dim IndicesFileName = nPaquete.TrimEnd("\"c) & "\" & Program.ImagingGlobal.ProyectoImagingRow.Nombre_Archivo_Indices

                If (File.Exists(IndicesFileName)) Then
                    Dim CSVData As New Slyg.Tools.CSV.CSVData
                    Dim Separador As Char
                    Dim DelimitadorTexto As Char

                    Separador = FormatValidator.getSeparador(Program.ImagingGlobal.ProyectoImagingRow.fk_Separador)
                    DelimitadorTexto = FormatValidator.getDelimitadorTexto(Program.ImagingGlobal.ProyectoImagingRow.fk_Identificador_Texto)

                    CSVData.LoadCSV(IndicesFileName, Program.ImagingGlobal.ProyectoImagingRow.Usa_Encabezado_Columnas, Separador, DelimitadorTexto)

                    InsertImage(CSVData.DataTable, nPaquete, nRowPaquete)
                Else
                    Throw New Exception("No se encontró el archivo de índices: " & IndicesFileName)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message + "..")
            End Try

        End Sub

        Private Sub LoadImagesNoIndice(ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow)
            Dim Archivos() As String
            Dim Codigo As String
            Dim i As Integer = 0
            Dim NewExtension As String

            'NewExtension = EventManager.ExtensionImagen_Plugin(True)

            'If NewExtension Is String.Empty Then
            NewExtension = _ExtensionAux
            'End If

            Archivos = Directory.GetFiles(nPaquete, "*" & NewExtension)

            ProgressForm.SetMaxValue(Archivos.Length)

            For Each Archivo As String In Archivos
                i += 1

                Codigo = Path.GetFileNameWithoutExtension(Archivo)
                InsertImage(Archivo, nPaquete, Codigo, i, Program.ImagingGlobal.ProyectoImagingRow.Default_Esquema, Program.ImagingGlobal.ProyectoImagingRow.Default_Documento, nRowPaquete)

                ProgressForm.SetProgreso(i)
            Next
        End Sub

        Private Sub InsertImage(ByVal nData As Slyg.Tools.CSV.CSVTable, ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow)
            Dim CamposDataTable As DBImaging.SchemaConfig.CTA_CampoDataTable = Nothing

            Dim nDataCSV = nData.ToDataTable()

            ProgressForm.SetMaxValue(nDataCSV.Rows.Count)

            ' Cargar la parametrización de campos
            If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Indexacion) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Proyectoid_Esquema(Program.ImagingGlobal.ProyectoImagingRow.fk_Entidad, Program.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, Nothing)
                Catch
                    Throw
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            'Dim i As Integer = 0
            ' Dim Filas = nData.Tables(0).Select()
            'For Each Row As Slyg.Tools.CSV.CSVRow In nData.Rows
            For i As Integer = 1 To nDataCSV.Rows.Count
                Dim Row = nDataCSV.Rows(i - 1)
                Dim NombreImagen = CStr(Row.Item(Program.ImagingGlobal.ProyectoImagingRow.Columna_Imagen - 1))
                'MsgBox("1")
                If (Path.GetExtension(NombreImagen) = "") Then
                    NombreImagen &= Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada
                End If
                NombreImagen = NombreImagen.Substring(Program.ImagingGlobal.ProyectoImagingRow.Caracteres_Omitir)
                NombreImagen = Path.GetFileName(NombreImagen)
                Dim Esquema As Short
                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Columna_Esquema) Then
                    Dim Valor As String = Row.Item(Program.ImagingGlobal.ProyectoImagingRow.Columna_Esquema - 1).ToString()

                    If (Not Slyg.Tools.DataConvert.IsNumeric(Valor)) Then
                        Throw New Exception("El valor de la columna Esquema debe ser numérico")
                    End If

                    Esquema = CShort(Valor)
                Else
                    Esquema = Program.ImagingGlobal.ProyectoImagingRow.Default_Esquema
                End If
                Dim Documento As Short
                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Columna_Documento) Then
                    Dim Valor As String = Row.Item(Program.ImagingGlobal.ProyectoImagingRow.Columna_Documento - 1).ToString()

                    If (Not Slyg.Tools.DataConvert.IsNumeric(Valor)) Then
                        Throw New Exception("El valor de la columna Documento debe ser numérico")
                    End If

                    Documento = CShort(Valor)
                Else
                    Documento = Program.ImagingGlobal.ProyectoImagingRow.Default_Documento
                End If
                InsertImage(nPaquete & NombreImagen, nPaquete, CStr(Row.Item(Program.ImagingGlobal.ProyectoImagingRow.Columna_Key - 1)), i, Esquema, Documento, nRowPaquete)
                ' Cargar la data adicional
                If (Not Program.ImagingGlobal.ProyectoImagingRow.Usa_Indexacion) Then
                    Dim NewCampos() = CamposDataTable.Select("fk_Documento = " & Documento & "AND Columna_Cargue_Campo > 0")
                    For j = 0 To NewCampos.Length - 1
                        Dim CampoRow = NewCampos(j)
                        If (_Cargue.Campo.Select("fk_Paquete = " & nRowPaquete.id_Paquete & " AND fk_Item = " & i & " AND id_Campo = " & CShort(CampoRow.Item("id_Campo"))).Length = 0) Then
                            _Cargue.Campo.AddCampoRow(nRowPaquete.id_Paquete, i, CShort(CampoRow.Item("id_Campo")), Row.Item(CInt(CampoRow.Item("Columna_Cargue_Campo")) - 1).ToString())
                        End If
                    Next

                    'For Each CampoRow As DBImaging.SchemaConfig.CTA_CampoRow In CamposDataTable.Select("fk_Documento = " & Documento & "AND Columna_Cargue_Campo > 0")
                    '    If (_Cargue.Campo.Select("fk_Paquete = " & nRowPaquete.id_Paquete & " AND fk_Item = " & i & " AND id_Campo = " & CampoRow.id_Campo).Length = 0) Then
                    '        _Cargue.Campo.AddCampoRow(nRowPaquete.id_Paquete, i, CampoRow.id_Campo, Row.Item(CampoRow.Columna_Cargue_Campo - 1).ToString())
                    '    End If
                    'Next
                End If
                ProgressForm.SetProgreso(i)
            Next

        End Sub

        Private Sub InsertImage(ByVal nNombre As String, ByVal nPathPaquete As String, ByVal nCodigo As String, ByVal nId As Integer, nEsquema As Short, nDocumento As Short, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow)
            If (nRowPaquete Is Nothing) Then
                idPaquete = CShort(idPaquete + 1)

                nRowPaquete = _Cargue.Paquete.AddPaqueteRow(idPaquete, nPathPaquete, 0, 0, 0, True, "", "", 0, 0, "")
            End If

            ' Calcular el Key del paquete            
            nRowPaquete.Key = Path.GetFileName(nRowPaquete.Path.TrimEnd("\"c))

            If (File.Exists(nNombre)) Then
                _Cargue.Item.AddItemRow(nRowPaquete, nId, CShort(ImageManager.GetFolios(nNombre)), getTamaño(nNombre), Path.GetFileName(nNombre), nCodigo, True, "", nEsquema, nDocumento, 0, 0, "")
            Else
                _Cargue.Item.AddItemRow(nRowPaquete, nId, 0, 0, Path.GetFileName(nNombre), nCodigo, False, "No existe el archivo", nEsquema, nDocumento, 0, 0, "")
            End If
        End Sub

        Protected MustOverride Sub Validar()
        
        Protected MustOverride Sub CargarConIndexacion()
        
        Protected MustOverride Sub CargarSinIndexacion()

#End Region

#Region " Funciones "

        Protected Function LoadImagesDirectories() As Boolean
            Try
                _Paquetes = New StringCollection
                _LoadImagesDirectories = False

                If Program.ImagingGlobal.ProyectoImagingRow.Usa_Rango_Paquetes Then
                    Dim FechaPaqueteForm As New FormFechaPaquete
                    Dim Respuesta As DialogResult

                    Respuesta = FechaPaqueteForm.ShowDialog()

                    If (Respuesta = DialogResult.OK) Then

                        'Recupera esquema seleccionado.
                        Dim Fecha As Date = FechaPaqueteForm.Fecha
                        Dim strFilter = Fecha.ToString(Program.ImagingGlobal.ProyectoImagingRow.Formato_Fecha_Paquete)
                        Dim Directorios As String()

                        Try
                            Directorios = Directory.GetDirectories(Program.ImagingGlobal.ProyectoImagingRow.Input_Folder, Program.ImagingGlobal.ProyectoImagingRow.Inicio_Nombre_Paquete & strFilter & ".*")
                            Dim i As Integer = 0

#If Not Debug Then
                        ProgressForm.Show()
#End If

                            Application.DoEvents()

                            ProgressForm.SetMaxValue(Directorios.Length)

                            For Each Directorio As String In Directorios
                                If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                Try
                                    Dim Paquete As Integer = Convert.ToInt32(Directorio.Split("."c)(Directorio.Split("."c).Length - 1))

                                    If (Paquete >= FechaPaqueteForm.PaqueteIni And Paquete <= FechaPaqueteForm.PaqueteFin) Then
                                        Dim NombrePaquete = Directorio.TrimEnd("\"c)

                                        If (Not NombrePaquete.EndsWith("#")) Then
                                            _Paquetes.Add(NombrePaquete & "\")
                                        End If
                                    End If
                                Catch : End Try

                                i += 1
                                ProgressForm.SetProgreso(i)
                            Next

                            If (_Paquetes.Count = 0) Then
                                MessageBox.Show("No se encontraron paquetes", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                _LoadImagesDirectories = True
                                Return True
                            End If
                        Catch ex As Exception
                            MessageBox.Show("No se encontraron paquetes", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End Try
                    End If
                Else
                    Dim CargueNoPaqueteForm As New FormCargueNoPaquete()
                    CargueNoPaqueteForm.SelectedPath = Program.ImagingGlobal.ProyectoImagingRow.Input_Folder

                    Dim Respuesta = CargueNoPaqueteForm.ShowDialog()
                    If (Respuesta = DialogResult.OK) Then
                        Dim NombrePaquete = CargueNoPaqueteForm.SelectedPath.TrimEnd("\"c)

                        If (Not NombrePaquete.EndsWith("#")) Then
                            _Paquetes.Add(NombrePaquete & "\")

#If Not Debug Then
                    ProgressForm.Show()
#End If

                            Application.DoEvents()

                            _LoadImagesDirectories = True
                            Return True
                        End If

                        Throw New Exception("La carpeta corresponde a un paquete ya cargado")
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("ERROR función: [LoadImagesDirectories] " & ex.Message, " Validación ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            _LoadImagesDirectories = False
            Return False

        End Function

        Protected Function LoadImagesDirectories_CargueMasivo() As Boolean
            Try
                _Paquetes = New StringCollection
                _LoadImagesDirectories = False

                If Program.ImagingGlobal.ProyectoImagingRow.Usa_Rango_Paquetes Then
                    Dim FechaPaqueteForm As New Miharu.Imaging.Library.Procesos.Cargue.FormFechaPaquete
                    Dim Respuesta As DialogResult

                    Respuesta = FechaPaqueteForm.ShowDialog()

                    If (Respuesta = DialogResult.OK) Then

                        'Recupera esquema seleccionado.
                        Dim Fecha As Date = FechaPaqueteForm.Fecha
                        Dim strFilter = Fecha.ToString(Program.ImagingGlobal.ProyectoImagingRow.Formato_Fecha_Paquete)
                        Dim Directorios As String()

                        Try
                            Directorios = Directory.GetDirectories(Program.ImagingGlobal.ProyectoImagingRow.Input_Folder, Program.ImagingGlobal.ProyectoImagingRow.Inicio_Nombre_Paquete & strFilter & ".*")
                            Dim i As Integer = 0

#If Not Debug Then
                                        ProgressForm.Show()
#End If

                            Application.DoEvents()

                            ProgressForm.SetMaxValue(Directorios.Length)

                            For Each Directorio As String In Directorios
                                If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                Try
                                    Dim Paquete As Integer = Convert.ToInt32(Directorio.Split("."c)(Directorio.Split("."c).Length - 1))

                                    If (Paquete >= FechaPaqueteForm.PaqueteIni And Paquete <= FechaPaqueteForm.PaqueteFin) Then
                                        Dim NombrePaquete = Directorio.TrimEnd("\"c)

                                        If (Not NombrePaquete.EndsWith("#")) Then
                                            _Paquetes.Add(NombrePaquete & "\")
                                        End If
                                    End If
                                Catch : End Try

                                i += 1
                                ProgressForm.SetProgreso(i)
                            Next

                            If (_Paquetes.Count = 0) Then
                                MessageBox.Show("No se encontraron paquetes", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Else
                                _LoadImagesDirectories = True
                                Return True
                            End If
                        Catch ex As Exception
                            MessageBox.Show("No se encontraron paquetes", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End Try
                    End If
                Else
                    Dim CargueNoPaqueteForm As New Miharu.Imaging.Library.Procesos.Cargue.FormCargueNoPaquete()
                    CargueNoPaqueteForm.SelectedPath = Program.ImagingGlobal.ProyectoImagingRow.Input_Folder

                    Dim Respuesta = CargueNoPaqueteForm.ShowDialog()
                    If (Respuesta = DialogResult.OK) Then
                        '                        Dim NombrePaquete = CargueNoPaqueteForm.SelectedPath.TrimEnd("\"c)

                        '                        If (Not NombrePaquete.EndsWith("#")) Then
                        '                            _Paquetes.Add(NombrePaquete & "\")

                        '#If Not Debug Then
                        '                    ProgressForm.Show()
                        '#End If

                        '                            Application.DoEvents()

                        '                            Return True
                        '                        End If

                        '                        Throw New Exception("La carpeta corresponde a un paquete ya cargado")


                        'caop obtener folders

                        Dim directorios As String()

                        'directorios = Directory.GetDirectories(fechaPaqueteForm.RutaTextBox.Text)
                        directorios = Directory.GetDirectories(CargueNoPaqueteForm.SelectedPath.TrimEnd("\"c))

                        Dim i As Integer = 0

                        ProgressForm.Show()
                        Application.DoEvents()

                        ProgressForm.SetMaxValue(directorios.Length)


                        Dim rutaCargueInicial As String
                        Dim rutaCargueCodBarras As String

                        If directorios.Length = 0 Then
                            Dim directorio = CargueNoPaqueteForm.SelectedPath.TrimEnd("\"c) & "\"
                            Dim paquete = (directorio).Split("\"c)

                            'rutaCargueInicial = paquete(paquete.Length - 2).Substring(0, 1)
                            'rutaCargueCodBarras = paquete(paquete.Length - 2)

                            'If (rutaCargueInicial.ToString <> "c") And (rutaCargueInicial.ToString <> "C") And IsNumeric(rutaCargueCodBarras) Then
                            _Paquetes.Add(CargueNoPaqueteForm.SelectedPath.TrimEnd("\"c) & "\")
                            'End If
                        End If

                        For Each directorio As String In directorios
                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                            Dim paquete = (directorio).Split("\"c)

                            rutaCargueInicial = paquete(paquete.Length - 1).Substring(0, 1)
                            rutaCargueCodBarras = paquete(paquete.Length - 1)

                            Dim NombrePaquete = directorio.TrimEnd("\"c)

                            If (Not NombrePaquete.EndsWith("#")) Then
                                _Paquetes.Add(directorio.TrimEnd("\"c) & "\")
                            End If

                            i += 1
                            ProgressForm.SetProgreso(i)
                        Next

                        If _Paquetes.Count = 0 Then
                            MessageBox.Show("No se encontraron paquetes disponibles para el cargue.", "Imagenes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            _LoadImagesDirectories = True
                            Return True
                        End If

                End If
                End If
            Catch ex As Exception
                MessageBox.Show("ERROR función: [LoadImagesDirectories] " & ex.Message, " Validación ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            _LoadImagesDirectories = False
            Return False

        End Function

        Private Function getTamaño(ByVal nFileName As String) As Long
            Dim Archivo As New FileInfo(nFileName)

            ' retornar el valor en Bytes
            Return Archivo.Length
        End Function

#End Region

    End Class

End Namespace