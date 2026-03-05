Imports DBCore
Imports DBIntegration
Imports DBIntegration.SchemaBcoDavivienda
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library.MiharuDMZ
Imports System.Drawing
Imports System.IO
Imports System.Web.UI.WebControls.Expressions
Imports System.Windows.Forms

Namespace Procesos.Configuracion.Imaging
    Public Class FormConfigImagenes
        Inherits Desktop.Library.FormBase


#Region " Constructores "
        Public Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub

#End Region

#Region " Eventos "

        Private Sub FormConfigImagenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            If Not Me.DesignMode Then
                cargarSerieDocumental()
                cargarFormato()
                'Cargar_Imagenes()
            End If
        End Sub

        Private Sub cbxSerieDocumental_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSerieDocumental.SelectedIndexChanged
            cargarFormato()
        End Sub

        Private Sub cbxFormato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxFormato.SelectedIndexChanged
            If cbxFormato.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cbxFormato.SelectedValue.ToString()) AndAlso cbxFormato.SelectedValue IsNot "-1" Then
                GroupBox1.Enabled = True
                GroupBox2.Enabled = True
                GroupBox3.Enabled = True
                Cargar_Imagenes()
            End If
        End Sub

        Private Sub LogoImageButton_Click(sender As Object, e As EventArgs) Handles LogoImageButton.Click
            Dim LogoOpenDialog As New OpenFileDialog()
            LogoOpenDialog.Multiselect = False
            LogoOpenDialog.Filter = "Archivos de Imagen (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|Todos los archivos (*.*)|*.*"

            If LogoOpenDialog.ShowDialog() = DialogResult.OK Then
                HeaderLogoImageTextBox.Text = LogoOpenDialog.FileName
                LogoImagePictureBox.ImageLocation = HeaderLogoImageTextBox.Text
                LogoImagePictureBox.Refresh()
            End If
        End Sub

        Private Sub LogoImageTextBox_Leave(sender As Object, e As EventArgs) Handles HeaderLogoImageTextBox.Leave
            If HeaderLogoImageTextBox.Text <> "" Then
                LogoImagePictureBox.ImageLocation = HeaderLogoImageTextBox.Text
                LogoImagePictureBox.Refresh()
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

        Private Sub FooterImageButton_Click(sender As Object, e As EventArgs) Handles FooterImageButton.Click
            Dim FooterOpenDialog As New OpenFileDialog()
            FooterOpenDialog.Multiselect = False
            FooterOpenDialog.Filter = "Archivos de Imagen (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|Todos los archivos (*.*)|*.*"

            If FooterOpenDialog.ShowDialog() = DialogResult.OK Then
                FooterLogoImageTextBox.Text = FooterOpenDialog.FileName
                FooterImagePictureBox.ImageLocation = FooterLogoImageTextBox.Text
                FooterImagePictureBox.Refresh()
            End If
        End Sub

#End Region

#Region " Metodos "

        Private Sub cargarSerieDocumental()
            Dim serieDocumentalDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[TBL_Esquema]", New List(Of QueryParameter) From {
                                                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                    New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()}
                                                    }, QueryRequestType.Table, QueryResponseType.Table)

            serieDocumentalDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_EsquemaDataTable(), queryResponse.dataTable), DBCore.SchemaConfig.TBL_EsquemaDataTable)

            Utilities.Llenarcombo(cbxSerieDocumental, serieDocumentalDataTable, DBCore.SchemaConfig.TBL_EsquemaEnum.id_Esquema.ColumnName, DBCore.SchemaConfig.TBL_EsquemaEnum.Nombre_Esquema.ColumnName, True, "-1", "")

            cbxSerieDocumental.Refresh()
        End Sub

        Private Sub cargarFormato()
            Dim formatoDataTable As DBCore.SchemaConfig.TBL_DocumentoDataTable

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Config].[TBL_Documento]", New List(Of QueryParameter) From {
                                                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                    New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                    New QueryParameter With {.name = "fk_Esquema", .value = cbxSerieDocumental.SelectedValue.ToString()}
                                                    }, QueryRequestType.Table, QueryResponseType.Table)

            formatoDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_DocumentoDataTable(), queryResponse.dataTable), DBCore.SchemaConfig.TBL_DocumentoDataTable)

            If formatoDataTable.Rows.Count > 0 Then
                Utilities.Llenarcombo(cbxFormato, formatoDataTable, DBCore.SchemaConfig.TBL_DocumentoEnum.id_Documento.ColumnName, DBCore.SchemaConfig.TBL_DocumentoEnum.Nombre_Documento.ColumnName, True, "-1", "")
            Else
                ' Combo vacío
                cbxFormato.DataSource = Nothing
                cbxFormato.Items.Clear()
            End If

            cbxFormato.Refresh()
        End Sub

        Private Sub AceptarButton_Click(sender As Object, e As EventArgs) Handles AceptarButton.Click

            Try
                GuardarImagenPorRuta(HeaderLogoImageTextBox.Text, TipoImagenLogo.LogoEncabezado)
                GuardarImagenPorRuta(SignatureImageTextBox.Text, TipoImagenLogo.Firma)
                GuardarImagenPorRuta(FooterLogoImageTextBox.Text, TipoImagenLogo.LogoPiePagina)

                MessageBox.Show("Imagenes cargadas con éxito", "Logo y Firma", MessageBoxButtons.OK, MessageBoxIcon.None)
                Me.Close()

            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub Cargar_Imagenes()

            Try
                Dim imagenesDataTable As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable

                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[CTA_Imagenes_Por_Documento]", New List(Of QueryParameter) From {
                                                    New QueryParameter With {.name = "Fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                    New QueryParameter With {.name = "Fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                    New QueryParameter With {.name = "fk_Esquema", .value = cbxSerieDocumental.SelectedValue.ToString()},
                                                    New QueryParameter With {.name = "fk_Documento", .value = cbxFormato.SelectedValue.ToString()}
                                                    }, QueryRequestType.Table, QueryResponseType.Table)

                imagenesDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable(), queryResponse.dataTable), DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoDataTable)

                If imagenesDataTable Is Nothing OrElse imagenesDataTable.Rows.Count = 0 Then
                    HeaderLogoImageTextBox.Text = String.Empty
                    LogoImagePictureBox.Image = Nothing
                    LogoImagePictureBox.Refresh()
                    SignatureImageTextBox.Text = String.Empty
                    SignatureImagePictureBox.Image = Nothing
                    SignatureImagePictureBox.Refresh()
                    FooterLogoImageTextBox.Text = String.Empty
                    FooterImagePictureBox.Image = Nothing
                    FooterImagePictureBox.Refresh()
                    Exit Sub
                End If

                For Each imagenRow As DBCore.SchemaProcess.CTA_Imagenes_Por_DocumentoRow In imagenesDataTable
                    ' Imagen Vacia, continua for
                    If imagenRow.IsImage_BinaryNull() Then Continue For

                    Dim imageBytes As Byte() = CType(imagenRow.Image_Binary, Byte())
                    Dim bitmap As New Bitmap(New MemoryStream(imageBytes))

                    Select Case imagenRow.Fk_Tipo_Imagen
                        Case TipoImagenLogo.LogoEncabezado
                            HeaderLogoImageTextBox.Text = String.Empty
                            LogoImagePictureBox.Image = bitmap
                            LogoImagePictureBox.Refresh()

                        Case TipoImagenLogo.Firma
                            SignatureImageTextBox.Text = String.Empty
                            SignatureImagePictureBox.Image = bitmap
                            SignatureImagePictureBox.Refresh()

                        Case TipoImagenLogo.LogoPiePagina
                            FooterLogoImageTextBox.Text = String.Empty
                            FooterImagePictureBox.Image = bitmap
                            FooterImagePictureBox.Refresh()
                    End Select
                Next

            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub GuardarImagenPorRuta(rutaImagen As String, tipoImagen As TipoImagenLogo)

            If String.IsNullOrWhiteSpace(rutaImagen) Then Exit Sub

            Dim base64Image As String = ConvertirImagenABase64(rutaImagen)

            If String.IsNullOrWhiteSpace(base64Image) Then
                MessageBox.Show("La imagen no se pudo cargar o convertir: " & rutaImagen, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].[Process].[PA_Insert_Image_Por_Documento]", New List(Of QueryParameter) From {
                                                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                    New QueryParameter With {.name = "fk_Proyecto", .value = Program.ImagingGlobal.Proyecto.ToString()},
                                                    New QueryParameter With {.name = "fk_Esquema", .value = cbxSerieDocumental.SelectedValue.ToString()},
                                                    New QueryParameter With {.name = "fk_Documento", .value = cbxFormato.SelectedValue.ToString()},
                                                    New QueryParameter With {.name = "Id_Imagenes", .value = "0"},
                                                    New QueryParameter With {.name = "Fk_Tipo_Imagen", .value = CInt(tipoImagen).ToString()},
                                                    New QueryParameter With {.name = "Image", .value = base64Image}
                                                }, QueryRequestType.StoredProcedure, QueryResponseType.NonQuery)

            If queryResponse.error IsNot Nothing Then
                MessageBox.Show("Ocurrió un error al guardar la imagen (" & tipoImagen.ToString() & "): " & queryResponse.error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

#End Region

#Region " Funciones "

        ''' <summary>
        ''' Lee una imagen desde una ruta de archivo y la convierte a Base64.
        ''' </summary>
        ''' <param name="rutaImagen">Ruta completa del archivo de imagen.</param>
        ''' <returns>Cadena Base64 de la imagen o String.Empty si ocurre un error.</returns>
        Private Function ConvertirImagenABase64(rutaImagen As String) As String
            Try
                If String.IsNullOrWhiteSpace(rutaImagen) OrElse Not File.Exists(rutaImagen) Then
                    Return String.Empty
                End If

                Dim bytesImagen As Byte() = File.ReadAllBytes(rutaImagen)
                Return Convert.ToBase64String(bytesImagen)
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function

#End Region


    End Class
End Namespace
