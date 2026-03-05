Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports DBAgrario

Namespace Imaging.Forms.Cargue

    Public Class FormCargueNoPaqueteMixto

#Region " Declaraciones "

        Public RutaGlobal As String
        Private _plugin As New BanagrarioImagingPlugin
        Public ListaOficinas As New DataSet
        Public Lista As New List(Of Slyg.Tools.GenericItem(Of String))

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As BanagrarioImagingPlugin)
            InitializeComponent()

            _Plugin = nBanagrarioDesktopPlugin

        End Sub

#End Region

#Region " Propiedades "

        Property SelectedPath As String
            Get
                Return RutaTextBox.Text.TrimEnd("\"c) & "\"
            End Get
            Set(ByVal value As String)
                RutaTextBox.Text = value
            End Set
        End Property

        Public Property Fecha As DateTime
            Get
                Return FechaProcesoPicker.Value
            End Get
            Set(ByVal value As DateTime)
                FechaProcesoPicker.Value = value
            End Set
        End Property

#End Region

#Region " Eventos "

        Private Sub SelectFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub FormCargueNoPaquete_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadConfig()
        End Sub

        Private Sub AceptarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AceptarButton.Click

            If (Validar()) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Me.DialogResult = DialogResult.None
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
        End Sub

        'Private Sub ComboBoxOficinas_Enter(ByVal sender As System.Object, ByVal e As EventArgs)
        '    ComboBoxOficinas.Mostrar(Oficinas_ComboBox, Lista)
        '    Oficinas_ComboBox.Refresh()
        'End Sub

        'Private Sub Oficinas_ComboBox_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        '    ComboBoxOficinas.Mostrar(Oficinas_ComboBox, Lista)
        'End Sub

        Private Sub Oficinas_ComboBox_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles Oficinas_ComboBox.KeyDown
            ComboBoxOficinas.Mostrar(Oficinas_ComboBox, Lista)
        End Sub

        Private Sub Oficinas_ComboBox_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles Oficinas_ComboBox.Enter


            AceptarButton.Enabled = False
            ComboBoxOficinas.Mostrar(Oficinas_ComboBox, Lista)



        End Sub

        Private Sub ComboBoxOficinas_VisibleChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ComboBoxOficinas.VisibleChanged

            AceptarButton.Enabled = Not ComboBoxOficinas.Visible
        End Sub


#End Region

#Region " Metodos "

        Private Sub LoadConfig()
            Dim newDate = Now
            Carga_Oficinas()

            ComboBoxOficinas.InicializarCombo()
            If (newDate.Hour < _plugin.HoraCambioFechaProceso) Then
                newDate = newDate.AddDays(-1)
            End If

            FechaProcesoPicker.Value = newDate
            FechaMovimientoPicker.Value = newDate


            If (RutaGlobal <> "") Then
                RutaTextBox.Text = RutaGlobal
            End If


        End Sub

        Private Sub SelectFolderPath()

            Dim lectorFolderBrowserDialog = New FolderBrowserDialog()
            Dim respuesta As DialogResult
            lectorFolderBrowserDialog.SelectedPath = "D:\Scanner"


            '        LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
            lectorFolderBrowserDialog.ShowNewFolderButton = False
            lectorFolderBrowserDialog.Description = "Seleccione la carpeta"

            respuesta = lectorFolderBrowserDialog.ShowDialog()

            If (respuesta = DialogResult.OK) Then
                RutaTextBox.Text = lectorFolderBrowserDialog.SelectedPath
            End If
        End Sub

        Private Sub Carga_Oficinas()
            Dim dbmAgrario As New DBAgrarioDataBaseManager(Me._Plugin.BancoAgrarioConnectionString)
            dbmAgrario.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Dim listaOficinasLocal = dbmAgrario.SchemaConfig.CTA_Regional_COB_Oficina_Concatenacion.DBGet()
            dbmAgrario.Connection_Close()

            Oficinas_ComboBox.DataSource = listaOficinasLocal
            Oficinas_ComboBox.DisplayMember = listaOficinasLocal.Columns("Nombre_Oficina").ToString
            Oficinas_ComboBox.ValueMember = listaOficinasLocal.Columns("id_Oficina").ToString

            For Each fila As DataRow In listaOficinasLocal.Rows
                Lista.Add(New Slyg.Tools.GenericItem(Of String)(fila("id_Oficina").ToString(), fila("Nombre_Oficina").ToString()))
            Next


        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean

            RutaGlobal = RutaTextBox.Text

            Dim rutaCargue = RutaTextBox.Text.Split("\"c)
            Dim rutaCargueFolder = rutaCargue(rutaCargue.Length - 1)

            Dim codigoOficinaFolder As String
            Dim fechaMovimientoFolder As String
            Dim fechaPick = FechaMovimientoPicker.Value.ToString("yyyyMMdd")

            Try
                codigoOficinaFolder = rutaCargueFolder.Substring(0, 4)
                fechaMovimientoFolder = rutaCargueFolder.Substring(4, 8)
            Catch ex As Exception
                Throw New Exception(". Debe seleccionar un directorio válido.", ex)
            End Try


            If (RutaTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
            ElseIf (Convert.ToInt16(codigoOficinaFolder).ToString = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe ingresar el código de la oficina", "Código inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Oficinas_ComboBox.Focus()
                Oficinas_ComboBox.SelectAll()
            ElseIf (Getdate(FechaMovimientoPicker.Value) > Getdate(FechaProcesoPicker.Value)) Then
                DesktopMessageBoxControl.DesktopMessageShow("La fecha de movimiento no puede ser superior a la fecha de proceso", "Fecha inválida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                FechaMovimientoPicker.Focus()
                FechaMovimientoPicker.Select()

            ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()


            ElseIf (fechaMovimientoFolder <> fechaPick) Then
                DesktopMessageBoxControl.DesktopMessageShow("La fecha de movimiento no corresponde a la de la carpeta del cargue", "Fecha inválida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                FechaMovimientoPicker.Focus()
                FechaMovimientoPicker.Select()

            ElseIf (Convert.ToInt16(codigoOficinaFolder).ToString <> Oficinas_ComboBox.SelectedValue.ToString) Then
                DesktopMessageBoxControl.DesktopMessageShow("La oficina de movimiento no corresponde a la de la carpeta del cargue", "Oficina inválida", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                Oficinas_ComboBox.Focus()
                Oficinas_ComboBox.SelectAll()

            Else

                Return True
            End If

            Return False
        End Function

        Private Function Getdate(ByVal nFecha As DateTime) As String
            Dim getFecha = nFecha.Date.ToString("yyyyMMdd")
            Return getFecha
        End Function

#End Region

    End Class

End Namespace