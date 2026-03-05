Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO

Namespace Procesos.Destape
    Public Class FormDestapeMasivo
        Inherits Form

#Region " Propiedades "
        Public Property IdOT() As Integer
        Property EventManager As EventManager
        Property SelectedPath As String
            Get
                Return RutaTextBox.Text.TrimEnd("\"c) & "\"
            End Get
            Set(ByVal value As String)
                RutaTextBox.Text = value
            End Set
        End Property
#End Region
#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        End Sub

#End Region
        Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub SelectFolderPath()
            Dim LectorFolderBrowserDialog = New FolderBrowserDialog()
            Dim Respuesta As DialogResult

            LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
            LectorFolderBrowserDialog.ShowNewFolderButton = False
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta"

            Respuesta = LectorFolderBrowserDialog.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
            End If
        End Sub

        Private Sub FormDestapeMasivo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            OTTextBox.Text = IdOT.ToString()
        End Sub

        Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
            If DesktopMessageBoxControl.DesktopMessageShow("¿Esta seguro de crear el destape masivo para la OT seleccionada?", "Crear Destape Masivo", DesktopMessageBoxControl.IconEnum.WarningIcon, True) = DialogResult.OK Then
                CrearDestapeMasivo()
            End If
        End Sub

#Region " Funciones "

        Private Function Validar() As Boolean
            If (RutaTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()

            ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
            Else
                Return True
            End If

            Return False
        End Function

        Private Sub CrearDestapeMasivo()
            If Validar() Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim DirectoryNames As String()
                    Dim Contenedores As New List(Of String)
                    DirectoryNames = Directory.GetDirectories(Me.SelectedPath)

                    Dim RutaDestino As String = Me.SelectedPath & IdOT & ".#"

                    For Each directorio In DirectoryNames
                        If (Not directorio.EndsWith("#")) Then
                            Contenedores.Add(directorio)
                        End If
                    Next

                    If Contenedores.Count > 0 Then
                        Dim id_Precinto = dbmImaging.SchemaProcess.PA_Crear_Destape_Masivo_Precinto.DBExecute(IdOT, Program.Sesion.Usuario.id, Program.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo)
                        If id_Precinto > 0 Then
                            For Each contenedor In Contenedores
                                Dim id_Contenedor = dbmImaging.SchemaProcess.PA_Crear_Destape_Masivo_Contenedor.DBExecute(IdOT, id_Precinto, contenedor.Substring(contenedor.LastIndexOf("\") + 1), Program.Sesion.Usuario.id)
                                If id_Contenedor > 0 Then
                                    MoverCarpeta(RutaDestino, contenedor)
                                End If
                            Next
                            Dim CantContenedores = dbmImaging.SchemaProcess.TBL_Contenedor.DBFindByfk_OTfk_Precinto(IdOT, id_Precinto)

                            If CantContenedores.Count = 0 Then
                                dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(IdOT, id_Precinto)
                            End If
                        End If
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("La ruta seleccionada no contiene subdirectorios", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                    End If

                    DesktopMessageBoxControl.DesktopMessageShow("Proceso Finalizado", "Crear Destape Masivo", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Me.RutaTextBox.Text = String.Empty
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub
#End Region

        Private Sub MoverCarpeta(nRutaDestino As String, nPath As String)
            Try
                If Not Directory.Exists(nRutaDestino) Then
                    Directory.CreateDirectory(nRutaDestino)
                End If
                Directory.Move(nPath, nRutaDestino & "\" & nPath.Substring(nPath.LastIndexOf("\") + 1))
            Catch

            End Try
        End Sub
    End Class
End Namespace
