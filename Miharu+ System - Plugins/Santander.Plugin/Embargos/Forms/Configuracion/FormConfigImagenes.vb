Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports DBIntegration
Imports DBIntegration.SchemaConfig

Namespace Embargos.Forms.Configuracion
    Public Class FormConfigImagenes

        Private _plugin As EmbargosImagingPlugin

        Public Sub New(nPlugin As EmbargosImagingPlugin)
            _plugin = nPlugin

            InitializeComponent()
        End Sub

        Private Sub LogoImageButton_Click(sender As Object, e As EventArgs) Handles LogoImageButton.Click
            Dim LogoOpenDialog As New OpenFileDialog()
            LogoOpenDialog.Multiselect = False
            LogoOpenDialog.Filter = "Archivos de Imagen (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|Todos los archivos (*.*)|*.*"

            If LogoOpenDialog.ShowDialog() = DialogResult.OK Then
                LogoImageTextBox.Text = LogoOpenDialog.FileName
                LogoImagePictureBox.ImageLocation = LogoImageTextBox.Text
                LogoImagePictureBox.Refresh()
            End If

        End Sub

        Private Sub LogoImageTextBox_Leave(sender As Object, e As EventArgs) Handles LogoImageTextBox.Leave
            If LogoImageTextBox.Text <> "" Then
                LogoImagePictureBox.ImageLocation = LogoImageTextBox.Text
                LogoImagePictureBox.Refresh()
            End If
        End Sub

        Private Sub SignatureImageButton_Click(sender As Object, e As EventArgs) Handles SignatureImageButton.Click
            Dim SignatureOpenDialog As New OpenFileDialog()
            SignatureOpenDialog.Multiselect = False
            SignatureOpenDialog.Filter = "Archivos de Imagen (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|Todos los archivos (*.*)|*.*"

            If SignatureOpenDialog.ShowDialog() = DialogResult.OK Then
                SignatureImageTextBox.Text = SignatureOpenDialog.FileName
                SignatureImagePictureBox.ImageLocation = SignatureImageTextBox.Text
                SignatureImagePictureBox.Refresh()
            End If
        End Sub

        Private Sub SignatureImageTextBox_Leave(sender As Object, e As EventArgs) Handles SignatureImageTextBox.Leave
            If SignatureImageTextBox.Text <> "" Then
                SignatureImagePictureBox.ImageLocation = SignatureImageTextBox.Text
                SignatureImagePictureBox.Refresh()
            End If
        End Sub

        Private Sub CancelarButton_Click(sender As Object, e As EventArgs) Handles CancelarButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub AceptarButton_Click(sender As Object, e As EventArgs) Handles AceptarButton.Click
            Dim dbmIntegration As DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegrationDataBaseManager(_plugin.SantanderConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim ImagenesDataTable = dbmIntegration.SchemaProcess.PA_Get_Imagenes.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto)

                If Not String.IsNullOrEmpty(LogoImageTextBox.Text) Then
                    Dim imagenLogoDataRow = New TBL_ImagenesType()

                    With imagenLogoDataRow
                        .Fk_Entidad = _plugin.Manager.ImagingGlobal.Entidad
                        .Fk_Proyecto = _plugin.Manager.ImagingGlobal.Proyecto
                        .Fk_Tipo_Imagen = 1
                        Using ImagenLogo = New FileStream(LogoImageTextBox.Text, FileMode.Open)
                            Dim longitud = ImagenLogo.Length
                            Dim data As Byte() = New Byte(longitud - 1) {}
                            ImagenLogo.Read(data, 0, longitud)
                            ImagenLogo.Close()
                            .Image_Binary = data
                        End Using

                        If ImagenesDataTable.Rows.Count > 0 Then
                            dbmIntegration.SchemaConfig.TBL_Imagenes.DBUpdate(imagenLogoDataRow, .Fk_Entidad, .Fk_Proyecto, .Fk_Tipo_Imagen)
                        Else
                            dbmIntegration.SchemaConfig.TBL_Imagenes.DBInsert(imagenLogoDataRow)
                        End If

                    End With
                End If

                If Not String.IsNullOrEmpty(SignatureImageTextBox.Text) Then

                    Dim imagenFirmaDataRow = New TBL_ImagenesType()

                    With imagenFirmaDataRow
                        .Fk_Entidad = _plugin.Manager.ImagingGlobal.Entidad
                        .Fk_Proyecto = _plugin.Manager.ImagingGlobal.Proyecto
                        .Fk_Tipo_Imagen = 2
                        Using ImagenFirma = New FileStream(SignatureImageTextBox.Text, FileMode.Open)
                            Dim longitud = ImagenFirma.Length
                            Dim data As Byte() = New Byte(longitud - 1) {}
                            ImagenFirma.Read(data, 0, longitud)
                            ImagenFirma.Close()
                            .Image_Binary = data
                        End Using

                        If ImagenesDataTable.Rows.Count > 0 Then
                            dbmIntegration.SchemaConfig.TBL_Imagenes.DBUpdate(imagenFirmaDataRow, .Fk_Entidad, .Fk_Proyecto, .Fk_Tipo_Imagen)
                        Else
                            dbmIntegration.SchemaConfig.TBL_Imagenes.DBInsert(imagenFirmaDataRow)
                        End If
                    End With

                End If

            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmIntegration IsNot Nothing Then dbmIntegration.Connection_Close()
            End Try
            MessageBox.Show("Imagenes cargadas con éxito", "Logo y Firma", MessageBoxButtons.OK, MessageBoxIcon.None)
            Me.Close()
        End Sub

        Private Sub FormConfigImagenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Cargar_Imagenes()
        End Sub

        Private Sub Cargar_Imagenes()
            Dim dbmIntegration As DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegrationDataBaseManager(_plugin.SantanderConnectionString)
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                Dim ImagenesDataTable = dbmIntegration.SchemaProcess.PA_Get_Imagenes.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto)

                If Not ImagenesDataTable(0).ItemArray(2).ToString = String.Empty Or Not ImagenesDataTable(0).ItemArray(3).ToString = String.Empty Then
                    Dim ImagenesRow = ImagenesDataTable(0)

                    Dim ImagenLogo = New Bitmap(New MemoryStream(CType(ImagenesRow("ImagenLogo"), Byte())))
                    Dim ImagenFirma = New Bitmap(New MemoryStream(CType(ImagenesRow("ImagenFirma"), Byte())))

                    LogoImagePictureBox.Image = ImagenLogo
                    LogoImagePictureBox.Refresh()
                    SignatureImagePictureBox.Image = ImagenFirma
                    SignatureImagePictureBox.Refresh()

                End If

            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbmIntegration IsNot Nothing Then dbmIntegration.Connection_Close()
            End Try
        End Sub
     
    End Class
End Namespace