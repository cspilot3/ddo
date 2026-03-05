Imports System
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.ComponentModel
Imports System.IO
Imports System.Drawing
Imports Miharu.Web.Controls.DocumentViewerObjects
Imports Slyg.Tools.Imaging

<Designer(GetType(DocumentViewerDesigner)), _
DefaultProperty("ImageUrl"), _
ToolboxData("<{0}:DocumentViewer runat = server Scrolling='True' Height='400' Width='650' ></{0}:DocumentViewer>")> _
Public Class DocumentViewer : Inherits WebControl : Implements INamingContainer

#Region " Enumeraciones "

    Private Enum EnumRotate As Byte
        IZQUIERDA = 0
        DERECHA = 1
    End Enum

    Public Enum EnumSaveFormat
        GIF
        JPG
        PNG
        TIFFC
        TIFFBN
        PDF
    End Enum

#End Region

#Region " Declaraciones "

    Public Const _ThumbnailWidth As Short = 70
    Private Const _ThumbnailBarWidth As Short = 100

    Private MostrarImagen As Boolean = False
    Private _NumThumbnail As Integer = 0

    Private imgImprimir As New ImageChanging
    Private imgAjustarAncho As New ImageChanging
    Private imgAjustarAlto As New ImageChanging
    Private imgZoomOut As New ImageChanging
    Private imgZoomIn As New ImageChanging

    Private ZoomState As New HtmlInputHidden()
    Private ZoomValue As New HtmlInputHidden()
    Private ScrollState As New HtmlInputHidden()
    Private Lista As New HtmlInputHidden()
    Private ScrollX As New HtmlInputHidden()
    Private ScrollY As New HtmlInputHidden()

    Private btnZoom As New ImageChangingButton
    Private btnGuardar As New ImageChangingButton
    Private btnRotateLeft As New ImageChangingButton
    Private btnRotateRight As New ImageChangingButton
    Private btnEndLeft As New ImageChangingButton
    Private btnEndRight As New ImageChangingButton
    Private btnNextLeft As New ImageChangingButton
    Private btnNextRight As New ImageChangingButton
    Private ddlPagina As New System.Web.UI.WebControls.DropDownList
    Private ddlFormato As New System.Web.UI.WebControls.DropDownList
    Private imgBase As New System.Web.UI.WebControls.Image
    Private imgBaseZoom As New System.Web.UI.WebControls.Image

    Private pnlImagen As New Panel
    Private pnlZoom As New Panel
    Private pnlLocalZoom As New Panel

    Private _LocalFileProvider As New FileProviderLocal()
    Private _ServerFileProvider As FileProvider

    'Public Event getFileProvider(ByVal sender As Object)
    Public Event SendError(ByVal sender As Object, ByVal MensajeError As String)
    Public Event Save(ByVal nSaveFormat As EnumSaveFormat, ByVal nFolioActual As Short)

    ' Propiedades
    Private ImageCollection As New List(Of DocumentItemType)

#End Region

#Region " Constructores "

    Public Sub New()
        ViewState("Inicializador") = 0
    End Sub

#End Region

#Region " Propiedades "

    <Bindable(True), Category("Behavior"), Browsable(True), _
    Description("Define si se utiliza el proveedor interno de imagenes o se utiliza uno del usuario")> _
    Public Property UseInternalFileProvider() As Boolean
        Get
            If ViewState("UseInternalFileProvider") Is Nothing Then
                ViewState("UseInternalFileProvider") = True
            End If
            Return CType(ViewState("UseInternalFileProvider"), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            ViewState("UseInternalFileProvider") = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), Browsable(True), _
    Description("Define si se muestran barras de desplazamiento cuando la imagen no cabe en el marco del control")> _
    Public Property Scrolling() As Boolean
        Get
            If ViewState("Scrolling") Is Nothing Then
                ViewState("Scrolling") = True
            End If
            Return CType(ViewState("Scrolling"), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            ViewState("Scrolling") = Value
        End Set
    End Property

    <Bindable(True), Category("Design"), Browsable(True), _
    Description("Alto del control")> _
    Public Overrides Property Height() As Unit
        Get
            Return MyBase.Height
        End Get
        Set(ByVal Value As Unit)
            If Value.Value < 100 Then
                MyBase.Height = New Unit(100)
            Else
                MyBase.Height = Value
            End If
        End Set
    End Property

    <Bindable(True), Category("Design"), Browsable(True), _
    Description("Ancho del control")> _
    Public Overrides Property Width() As Unit
        Get
            Return MyBase.Width
        End Get
        Set(ByVal Value As Unit)
            If Value.Value < 650 Then
                MyBase.Width = New Unit(650)
            Else
                MyBase.Width = Value
            End If
        End Set
    End Property

    <Bindable(True), Category("Behavior"), Browsable(False), _
    Description("Página actual")> _
    Public Property CurrentPage() As Short
        Get
            If ViewState("CurrentPage") Is Nothing Then
                ViewState("CurrentPage") = 1
            End If
            Return CType(ViewState("CurrentPage"), Short)
        End Get
        Set(ByVal Value As Short)
            If Value > Me.Pages Then
                ViewState("CurrentPage") = Me.Pages
            ElseIf Value < 1 Then
                ViewState("CurrentPage") = 1
            Else
                ViewState("CurrentPage") = Value

                If (Me.ddlPagina.SelectedIndex + 1) <> Value Then
                    Try
                        Me.ddlPagina.SelectedIndex = (Value - 1)
                    Catch ex As Exception
                        Me.ddlPagina.SelectedIndex = -1
                    End Try
                End If
            End If

            Refresh()
        End Set
    End Property

    <Bindable(True), Category("Design"), Browsable(True), _
    Description("Thumbnails cargados al inicialmente")> _
    Public Property ThumbnailsIniciales() As Short
        Get
            If ViewState("ThumbnailsIniciales") Is Nothing Then
                ViewState("ThumbnailsIniciales") = 5
            End If
            Return CType(ViewState("ThumbnailsIniciales"), Short)
        End Get
        Set(ByVal Value As Short)
            If Value < 0 Then
                ViewState("ThumbnailsIniciales") = 0
            Else
                ViewState("ThumbnailsIniciales") = Value
            End If
        End Set
    End Property

    <Bindable(True), Category("Behavior"), Browsable(True), _
    Description("Direccion URL del directorio de iconos")> _
    Public Property [IconURL]() As String
        Get
            If ViewState("IconURL") Is Nothing Then
                ViewState("IconURL") = "~/_images/_DocumentViewer/"
            End If
            Return CType(ViewState("IconURL"), String)
        End Get
        Set(ByVal value As String)
            ViewState("IconURL") = value
        End Set
    End Property

    <Bindable(True), Category("Behavior"), Browsable(True), _
    Description("Direccion URL del directorio de almacenamiento de imagenes temporales")> _
    Public Property [TempURL]() As String
        Get
            If ViewState("TempURL") Is Nothing Then
                ViewState("TempURL") = "~/_temporal/"
            End If
            Return CType(ViewState("TempURL"), String)
        End Get
        Set(ByVal value As String)
            ViewState("TempURL") = value
        End Set
    End Property

    <Bindable(True), Category("Behavior"), Browsable(True), _
        Description("Lista con las Direcciones URL de las Imagenes"), _
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), _
        Editor("Windows.Forms.VisualStyles.VisualStyleElement.Menu.DropDown", GetType(Windows.Forms.VisualStyles.VisualStyleElement.Menu.DropDown)), _
        PersistenceMode(PersistenceMode.InnerDefaultProperty)> _
    Public Property [ImageUrl]() As List(Of String)
        Get
            If ViewState("ImageUrl") Is Nothing Then
                ViewState("ImageUrl") = New List(Of String)
            End If
            Return CType(ViewState("ImageUrl"), List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            ViewState("ImageUrl") = value
        End Set
    End Property

    Public Property IsZoomActivado() As Boolean
        Get
            If ViewState("ZoomActivado") Is Nothing Then
                ViewState("ZoomActivado") = False
            End If
            Return CType(ViewState("ZoomActivado"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("ZoomActivado") = value
        End Set
    End Property

    Public Property ServerFileProvider() As FileProvider
        Get
            Return _ServerFileProvider
        End Get
        Set(ByVal value As FileProvider)
            _ServerFileProvider = value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), Browsable(True), _
        Description("Valor entre 0.1 y 1 que define la resolución de la imagen mostrada")> _
    Public Property Resolucion() As Single
        Get
            If ViewState("Resolucion") Is Nothing Then
                ViewState("Resolucion") = 1
            End If
            Return CType(ViewState("Resolucion"), Single)
        End Get
        Set(ByVal Value As Single)
            If Value < 0.1 Then
                ViewState("Resolucion") = 0.1
            ElseIf Value > 1 Then
                ViewState("Resolucion") = 1
            Else
                ViewState("Resolucion") = Value
            End If
        End Set
    End Property

    <Bindable(True), Category("Default"), Browsable(False), _
    Description("Número de páginas")> _
    Public ReadOnly Property Pages() As Short
        Get
            If ViewState("Pages") Is Nothing Then
                ViewState("Pages") = 0
            End If
            Return CType(ViewState("Pages"), Short)
        End Get
    End Property

    Public ReadOnly Property ImageItem() As DocumentItemType
        Get
            If ImageCollection.Count = 0 Then
                Return Nothing
            Else
                Return ImageCollection(Me.CurrentPage - 1)
            End If
        End Get
    End Property

    <Browsable(False)> _
    Public ReadOnly Property ThumbnailWidth() As Short
        Get
            Return _ThumbnailWidth
        End Get
    End Property

    Friend ReadOnly Property MyIconURL() As String
        Get
            Return ResolveClientUrl(IconURL).TrimEnd("/"c) & "/"
        End Get
    End Property

    Friend ReadOnly Property MyTempURL() As String
        Get
            Return ResolveClientUrl(TempURL).TrimEnd("/"c) & "/"
        End Get
    End Property

    Friend ReadOnly Property MyImagePath() As String
        Get
            If ImageUrl.Count > 1 Then
                If ImageUrl(CurrentPage - 1).Contains("\") Then
                    Return ImageUrl(CurrentPage - 1)
                Else
                    Return Page.Server.MapPath(ImageUrl(CurrentPage - 1))
                End If
            Else
                If ImageUrl(0).Contains("\") Then
                    Return ImageUrl(0)
                Else
                    Return Page.Server.MapPath(ImageUrl(0))
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property Key() As String
        Get
            If Page.Session("Key-" & Me.ClientID) Is Nothing Then
                GenerateKey()
            End If

            Return CStr(Page.Session("Key-" & Me.ClientID))
        End Get
    End Property

    Private ReadOnly Property ZoomFileName() As String
        Get
            If Page.Session("Zoom-" & Me.ClientID) Is Nothing Then
                GenerateZoomKey()
            End If

            Return CStr(Page.Session("Zoom-" & Me.ClientID))
        End Get
    End Property

#End Region

#Region " Eventos "

    Private Sub ZoomClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        LoadData()

        IsZoomActivado = Not IsZoomActivado

        If IsZoomActivado Then
            Dim DS, GS As String
            Dim SelectdZoom As String
            Dim SelectdZoomValue As Single

            DS = Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
            GS = Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

            SelectdZoom = ZoomState.Value.Replace(GS, DS)
            SelectdZoomValue = Single.Parse(SelectdZoom)

            Zoom(CShort(ScrollX.Value), CShort(ScrollY.Value), SelectdZoomValue)
        End If
    End Sub

    Private Sub GuardarClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        LoadData()
        Guardar()
    End Sub

    Private Sub RotateLeftClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        LoadData()

        If ImageItem.Rotacion > 0 Then
            ImageItem.Rotacion = CByte(ImageItem.Rotacion - 1)
        Else
            ImageItem.Rotacion = 3
        End If

        Rotar(EnumRotate.IZQUIERDA)
        Refresh()
    End Sub

    Private Sub RotateRightClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        LoadData()

        If ImageItem.Rotacion < 3 Then
            ImageItem.Rotacion = CByte(ImageItem.Rotacion + 1)
        Else
            ImageItem.Rotacion = 0
        End If

        Rotar(EnumRotate.DERECHA)
        Refresh()
    End Sub

    Private Sub EndLeftClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        ResetScroll()
        LoadData()
        If Me.CurrentPage > 0 Then
            Me.CurrentPage = 0
            MostrarImagen = True

            Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)

        Else
            Refresh()
        End If
    End Sub

    Private Sub EndRightClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        ResetScroll()
        LoadData()
        If Me.CurrentPage < Me.Pages Then
            Me.CurrentPage = Me.Pages
            MostrarImagen = True

            Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)
        Else
            Refresh()
        End If
    End Sub

    Private Sub NextLeftClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        ResetScroll()
        LoadData()
        If Me.CurrentPage > 0 Then
            Me.CurrentPage = CShort(Me.CurrentPage - 1)
            MostrarImagen = True

            Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)
        Else
            Refresh()
        End If
    End Sub

    Private Sub NextRightClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        ResetScroll()
        LoadData()
        If Me.CurrentPage < Me.Pages Then
            Me.CurrentPage = CShort(Me.CurrentPage + 1)
            MostrarImagen = True

            Refresh()
            Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)
        Else
            Refresh()
        End If
    End Sub

    Private Sub PaginasSelectedIndexChanged(ByVal sender As [Object], ByVal e As EventArgs)
        ResetScroll()
        LoadData()
        Me.CurrentPage = CShort(ddlPagina.SelectedIndex + 1)
        MostrarImagen = True

        Refresh()
        Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)
    End Sub

    Private Sub ThumbnailClicked(ByVal sender As [Object], ByVal e As ImageClickEventArgs)
        ResetScroll()
        LoadData()
        Dim ThumbnailButton As ImageButton = CType(sender, ImageButton)

        Me.CurrentPage = CShort(ThumbnailButton.ToolTip)
        MostrarImagen = False


        Refresh()
        Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)
    End Sub

    Private Sub LoadAllThumbnailClicked(ByVal sender As [Object], ByVal e As EventArgs)
        MostrarImagen = True
        LoadData()
        LoadThumbnailImages(CShort(ThumbnailsIniciales + 1), CShort(ImageCollection.Count))
    End Sub

#End Region

#Region " Metodos "

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        If Not (savedState Is Nothing) Then
            MyBase.LoadViewState(savedState)
        End If
        GenerateZoomKey()
    End Sub

    Protected Overrides Function SaveViewState() As Object
        Dim Data As New StringBuilder("")

        For Each Item As DocumentItemType In ImageCollection
            Data.Append(Item.getData & ";")
        Next

        ViewState("Pages") = ImageCollection.Count
        Lista.Value = Data.ToString

        Return MyBase.SaveViewState()
    End Function

    Protected Overrides Sub CreateChildControls()
        ParametrizarControlesPre()
        ParametrizarControles()
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
        ParametrizarControlesPost()

        RegistrarFunciones()

        MyBase.OnPreRender(e)
    End Sub

    Public Sub ShowImagen()
        Dim Folio As Integer
        Dim Folios As Integer
        
        Dim FileProvider As FileProvider = _LocalFileProvider
        _LocalFileProvider.FileNames = ImageUrl

        Folios = FileProvider.GetFolios()

        If Folios > 0 Then
            GenerateKey()

            Me.ImageCollection.Clear()

            For Folio = 1 To Folios
                Dim ImageItemLocal As New DocumentItemType

                ImageItemLocal.Rotacion = 0
                ImageItemLocal.ImageURL = MyTempURL & Me.Key & "Imagen" & "F" & Folio & "R" & ImageItemLocal.Rotacion & ".jpg"
                ImageItemLocal.ThumbnailURL = ""

                Me.ImageCollection.Add(ImageItemLocal)
            Next

            ViewState("Pages") = Folios
            Me.CurrentPage = 1

            Dim ThumbnailsCargar As Integer

            If Folios > ThumbnailsIniciales Then
                ThumbnailsCargar = ThumbnailsIniciales
            Else
                ThumbnailsCargar = Folios
            End If

            LoadThumbnailImages(1, ThumbnailsCargar)

            Cargar_Folio(Me.CurrentPage, Me.ImageItem.ImageURL)
        Else
            RaiseEvent SendError(Me, "No hay folios")
        End If
    End Sub

    Public Sub ClearCache()
        Dim FileNames() As String
        Dim FileName As String

        Try
            FileNames = Directory.GetFiles(Page.Server.MapPath(MyTempURL), Me.Key & "*")

            For Each FileName In FileNames
                Try : File.Delete(FileName) : Catch : End Try
            Next
        Catch

        End Try
    End Sub

    Private Sub LoadData()
        If ImageCollection.Count = 0 Then
            ' Restaurar Lista
            Dim Data As String = Lista.Value
            Dim Partes() As String = Data.Split(";"c)

            For Each Parte As String In Partes
                If Parte <> "" Then
                    ImageCollection.Add(New DocumentItemType(Parte))
                End If
            Next
        End If
    End Sub

    Private Sub Refresh()
        btnEndLeft.Enabled = (Me.CurrentPage > 1) And Not IsZoomActivado
        btnNextLeft.Enabled = (Me.CurrentPage > 1) And Not IsZoomActivado
        btnEndRight.Enabled = (Me.CurrentPage < Me.Pages) And Not IsZoomActivado
        btnNextRight.Enabled = (Me.CurrentPage < Me.Pages) And Not IsZoomActivado

        imgBase.ID = "imgBase"
        imgBase.Visible = True

        If Not Me.ImageItem Is Nothing Then
            imgBase.ImageUrl = Me.ImageItem.ImageURL
        Else
            imgBase.ImageUrl = Me.MyIconURL & "DefaultImage.gif"
        End If

        ActualizarPaginas()
    End Sub

    Private Sub ActualizarPaginas()
        Dim i As Short

        ddlPagina.Items.Clear()
        For i = 1 To Me.Pages
            ddlPagina.Items.Add(CStr(i))
        Next

        If ddlPagina.Items.Count > 0 Then
            ddlPagina.SelectedIndex = (Me.CurrentPage - 1)
        Else
            ddlPagina.SelectedIndex = -1
        End If

    End Sub

    Private Sub ParametrizarControlesPre()
        If ImageCollection.Count > 0 Then ViewState("Pages") = ImageCollection.Count

        ' Imprimir
        imgImprimir.AlternateText = " "
        imgImprimir.ImageUrl = MyIconURL & "Imprimir_Cold.png"
        imgImprimir.ChangingImageUrl = MyIconURL & "Imprimir_Hot.png"
        imgImprimir.ToolTip = "Imprimir"

        ' Formato        
        ddlFormato.AutoPostBack = False
        ddlFormato.Width = New Unit(100, UnitType.Pixel)

        ' Guardar 
        btnGuardar.AlternateText = " "
        btnGuardar.ImageUrl = MyIconURL & "Guardar_Cold.png"
        btnGuardar.ChangingImageUrl = MyIconURL & "Guardar_Hot.png"
        btnGuardar.DisableImageUrl = MyIconURL & "Guardar_Cold.png"
        btnGuardar.ToolTip = "Guardar"
        AddHandler btnGuardar.Click, AddressOf Me.GuardarClicked

        ' Rotate Left
        btnRotateLeft.AlternateText = " "
        btnRotateLeft.ImageUrl = MyIconURL & "Rotate_Left_Cold.png"
        btnRotateLeft.ChangingImageUrl = MyIconURL & "Rotate_Left_Hot.png"
        btnRotateLeft.DisableImageUrl = MyIconURL & "Rotate_Left_Cold.png"
        btnRotateLeft.ToolTip = "Rotar la imagen a la izquierda"
        AddHandler btnRotateLeft.Click, AddressOf Me.RotateLeftClicked

        ' Rotate Right
        btnRotateRight.AlternateText = " "
        btnRotateRight.ImageUrl = MyIconURL & "Rotate_Right_Cold.png"
        btnRotateRight.ChangingImageUrl = MyIconURL & "Rotate_Right_Hot.png"
        btnRotateRight.DisableImageUrl = MyIconURL & "Rotate_Right_Cold.png"
        btnRotateRight.ToolTip = "Rotar la imagen a la derecha"
        AddHandler btnRotateRight.Click, AddressOf Me.RotateRightClicked

        ' Ajustar Ancho
        imgAjustarAncho.AlternateText = " "
        imgAjustarAncho.ImageUrl = MyIconURL & "Ajustar_Ancho_Cold.png"
        imgAjustarAncho.ChangingImageUrl = MyIconURL & "Ajustar_Ancho_Hot.png"
        imgAjustarAncho.ToolTip = "Ajustar al ancho"

        ' Ajustar Alto
        imgAjustarAlto.AlternateText = " "
        imgAjustarAlto.ImageUrl = MyIconURL & "Ajustar_Alto_Cold.png"
        imgAjustarAlto.ChangingImageUrl = MyIconURL & "Ajustar_Alto_Hot.png"
        imgAjustarAlto.ToolTip = "Ajustar al alto"

        ' Zoom Out
        imgZoomOut.AlternateText = " "
        imgZoomOut.ImageUrl = MyIconURL & "Zoom_Out_Cold.png"
        imgZoomOut.ChangingImageUrl = MyIconURL & "Zoom_Out_Hot.png"
        imgZoomOut.ToolTip = "Alejar la imagen"

        ' Zoom In
        imgZoomIn.AlternateText = " "
        imgZoomIn.ImageUrl = MyIconURL & "Zoom_In_Cold.png"
        imgZoomIn.ChangingImageUrl = MyIconURL & "Zoom_In_Hot.png"
        imgZoomIn.ToolTip = "Acercar la imagen"

        'Zoom
        btnZoom.AlternateText = " "
        btnZoom.ImageUrl = MyIconURL & "Zoom_Cold.png"
        btnZoom.ChangingImageUrl = MyIconURL & "Zoom_Hot.png"
        btnZoom.DisableImageUrl = MyIconURL & "Zoom_Cold.png"
        btnZoom.ToolTip = "Zoom"
        AddHandler btnZoom.Click, AddressOf Me.ZoomClicked

        ' End Left
        btnEndLeft.AlternateText = " "
        btnEndLeft.ImageUrl = MyIconURL & "EndLeft_Cold.png"
        btnEndLeft.ChangingImageUrl = MyIconURL & "EndLeft_Hot.png"
        btnEndLeft.DisableImageUrl = MyIconURL & "EndLeft_Disable.png"
        btnEndLeft.ToolTip = "Primera pagina"
        AddHandler btnEndLeft.Click, AddressOf Me.EndLeftClicked

        ' Next Left
        btnNextLeft.AlternateText = " "
        btnNextLeft.ImageUrl = MyIconURL & "NextLeft_Cold.png"
        btnNextLeft.ChangingImageUrl = MyIconURL & "NextLeft_Hot.png"
        btnNextLeft.DisableImageUrl = MyIconURL & "NextLeft_Disable.png"
        btnNextLeft.ToolTip = "Pagina anterior"
        AddHandler btnNextLeft.Click, AddressOf Me.NextLeftClicked

        ' Pagina        
        ddlPagina.AutoPostBack = True
        ddlPagina.Width = New Unit(60, UnitType.Pixel)
        AddHandler ddlPagina.SelectedIndexChanged, AddressOf Me.PaginasSelectedIndexChanged

        ' End Right
        btnEndRight.AlternateText = " "
        btnEndRight.ImageUrl = MyIconURL & "EndRight_Cold.png"
        btnEndRight.ChangingImageUrl = MyIconURL & "EndRight_Hot.png"
        btnEndRight.DisableImageUrl = MyIconURL & "EndRight_Disable.png"
        btnEndRight.ToolTip = "Ultima pagina"
        AddHandler btnEndRight.Click, AddressOf Me.EndRightClicked

        ' Next Right
        btnNextRight.AlternateText = " "
        btnNextRight.ImageUrl = MyIconURL & "NextRight_Cold.png"
        btnNextRight.ChangingImageUrl = MyIconURL & "NextRight_Hot.png"
        btnNextRight.DisableImageUrl = MyIconURL & "NextRight_Disable.png"
        btnNextRight.ToolTip = "Pagina siguiente"
        AddHandler btnNextRight.Click, AddressOf Me.NextRightClicked

        ZoomState.ID = "ZoomState"
        If ZoomState.Value = "" Then
            ZoomState.Value = "100"
        End If

        ZoomValue.ID = "ZoomValue"
        If ZoomValue.Value = "" Then
            ZoomValue.Value = "Zoom"
        End If

        ScrollState.ID = "ScrollState"
        Lista.ID = "Lista"
        ScrollX.ID = "ScrollX"
        ScrollY.ID = "ScrollY"

        ' Imagen
        imgBase.AlternateText = ""
        imgBase.ID = "imgBase"

        imgBaseZoom.ID = "imgBaseZoom"
        imgBaseZoom.ImageUrl = MyTempURL & Me.Key & "-ImagenZoom" & "-" & ZoomFileName & ".jpg"
        imgBaseZoom.Attributes("onmouseover") = "TJPzoom(this);"

    End Sub

    Private Sub ParametrizarControles()
        Controls.Add(ZoomState)
        Controls.Add(New LiteralControl(vbCrLf))
        Controls.Add(ZoomValue)
        Controls.Add(New LiteralControl(vbCrLf))
        Controls.Add(ScrollState)
        Controls.Add(New LiteralControl(vbCrLf))
        Controls.Add(Lista)
        Controls.Add(New LiteralControl(vbCrLf))
        Controls.Add(ScrollX)
        Controls.Add(New LiteralControl(vbCrLf))
        Controls.Add(ScrollY)

        Controls.Add(New LiteralControl(vbCrLf))
        Controls.Add(New LiteralControl(vbCrLf))

        Controls.Add(New LiteralControl("<table id='tblBaseDocumentViewer' style='width:" & Me.Width.Value & "px; height:" & Me.Height.Value - 10 & "px; border: black 1px solid; background-color: #e0dfe3' cellSpacing='0' cellPadding='0' border='0'>" & vbCrLf))
        Controls.Add(New LiteralControl("   <tr>" & vbCrLf))
        Controls.Add(New LiteralControl("		<td style='height:30px' colspan='2'>" & vbCrLf))

        Controls.Add(New LiteralControl("			<table id='tblToolbarDocumentViewer' style='width:" & Me.Width.Value & "px; border: 2px outset; background-color: #e0dfe3' cellSpacing='0' cellPadding='0' align='center' border='0'>" & vbCrLf))
        Controls.Add(New LiteralControl("               <tr>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Imprimir
        Controls.Add(New LiteralControl("               	    <A onclick=""Imprimir();"" href='#'>" & vbCrLf))
        Controls.Add(imgImprimir)
        Controls.Add(New LiteralControl("                 	    </A>" & vbCrLf))

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:10px'></td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:110px'>" & vbCrLf))

        ' Formato
        Controls.Add(ddlFormato)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Guardar
        Controls.Add(btnGuardar)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'></td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Ajustar Ancho
        Controls.Add(New LiteralControl("               	    <A onclick=""AjustarAncho();"" href='#'>" & vbCrLf))
        Controls.Add(imgAjustarAncho)
        Controls.Add(New LiteralControl("               	    </A>" & vbCrLf))

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Ajustar Alto
        Controls.Add(New LiteralControl("               	    <A onclick=""AjustarAlto();"" href='#'>" & vbCrLf))
        Controls.Add(imgAjustarAlto)
        Controls.Add(New LiteralControl("               	    </A>" & vbCrLf))

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Rotate left
        Controls.Add(btnRotateLeft)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Rotate right
        Controls.Add(btnRotateRight)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td align='right' style='width:30px'>" & vbCrLf))

        ' Zoom Out
        Controls.Add(New LiteralControl("               	    <A onclick=""ZoomOut();"" href='#'>" & vbCrLf))
        Controls.Add(imgZoomOut)
        Controls.Add(New LiteralControl("               	    </A>" & vbCrLf))

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td align='center' style='width:130px'>" & vbCrLf))


        pnlLocalZoom.Controls.Add(New LiteralControl("               	    <select id='ddlZoom' style='width:120px' onchange='SeleccionarZoom();'>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='10'>10%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='25'>25%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='50'>50%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='75'>75%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='100' selected='selected'>100%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='150'>150%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='200'>200%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='300'>300%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='400'>400%</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='500'>Ajustar al ancho</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    	<option value='600'>Ajustar al alto</option>" & vbCrLf))
        pnlLocalZoom.Controls.Add(New LiteralControl("               	    </select>" & vbCrLf))

        Controls.Add(pnlLocalZoom)

        Controls.Add(New LiteralControl("                   </td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Zoom In
        Controls.Add(New LiteralControl("               	    <A onclick=""ZoomIn();"" href='#'>" & vbCrLf))
        Controls.Add(imgZoomIn)
        Controls.Add(New LiteralControl("               	    </A>" & vbCrLf))

        Controls.Add(New LiteralControl("                   </td>" & vbCrLf))

        Controls.Add(New LiteralControl("               	<td align='center' style='width:50px'>" & vbCrLf))

        ' Zoom
        Controls.Add(btnZoom)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))

        Controls.Add(New LiteralControl("               	<td style='width:" & (Me.Width.Value - 570) & "px'></td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td align='right' style='width:30px'>" & vbCrLf))

        ' End left
        Controls.Add(btnEndLeft)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td align='right' style='width:30px'>" & vbCrLf))

        ' Next left
        Controls.Add(btnNextLeft)

        Controls.Add(New LiteralControl("                   </td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td align='center' style='width:70px'>" & vbCrLf))

        ' Pagina
        Controls.Add(ddlPagina)

        Controls.Add(New LiteralControl("               	</td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' Next right
        Controls.Add(btnNextRight)

        Controls.Add(New LiteralControl("                   </td>" & vbCrLf))
        Controls.Add(New LiteralControl("               	<td style='width:30px'>" & vbCrLf))

        ' End right
        Controls.Add(btnEndRight)

        Controls.Add(New LiteralControl("                   </td>" & vbCrLf))
        Controls.Add(New LiteralControl("               </tr>" & vbCrLf))
        Controls.Add(New LiteralControl("           </table>" & vbCrLf))

        Controls.Add(New LiteralControl("       </td>" & vbCrLf))
        Controls.Add(New LiteralControl("   </tr>" & vbCrLf))
        Controls.Add(New LiteralControl("   <tr>" & vbCrLf))
        Controls.Add(New LiteralControl("       <td style='height:5px' colspan='2'></td>" & vbCrLf))
        Controls.Add(New LiteralControl("   </tr>" & vbCrLf))

        Controls.Add(New LiteralControl("   <tr>" & vbCrLf))

        Controls.Add(New LiteralControl("       <td vAlign='top' align='center' style='width:100px' >" & vbCrLf))


        Controls.Add(New LiteralControl("           <div id='divThumbnails' style='overflow:auto; border: 2px inset; background-color: #808080; width:" & _ThumbnailBarWidth & "px; height:" & (Me.Height.Value - 35) & "px'>" & vbCrLf))

        InsertThumbnails(Controls)

        Controls.Add(New LiteralControl("           </div>" & vbCrLf))

        Controls.Add(New LiteralControl("       </td>" & vbCrLf))


        Controls.Add(New LiteralControl("       <td vAlign='middle' style='width:" & Me.Width.Value - 100 & "px'>" & vbCrLf))

        If Me.Scrolling Then
            pnlImagen.Controls.Add(New LiteralControl("           <div id='divMarco' align='center' style='overflow:auto; width:" & Me.Width.Value - _ThumbnailBarWidth & "px; height:" & (Me.Height.Value - 35) & "px'>" & vbCrLf))
        Else
            pnlImagen.Controls.Add(New LiteralControl("           <div id='divMarco' align='center' style='width:" & Me.Width.Value - _ThumbnailBarWidth & "px; height:" & (Me.Height.Value - 35) & "px'>" & vbCrLf))
        End If

        pnlImagen.Controls.Add(imgBase)

        pnlImagen.Controls.Add(New LiteralControl("           </div>" & vbCrLf))
        Controls.Add(pnlImagen)

        pnlZoom.Controls.Add(imgBaseZoom)
        Controls.Add(pnlZoom)

        Controls.Add(New LiteralControl("       </td>" & vbCrLf))
        Controls.Add(New LiteralControl("   </tr>" & vbCrLf))
        Controls.Add(New LiteralControl("</table>" & vbCrLf))

        Refresh()
    End Sub

    Private Sub ParametrizarControlesPost()
        ' Boton seleccionado
        Dim ThumbnailItem As Integer = 0

        For Each ImageItemLocal As DocumentItemType In ImageCollection
            ThumbnailItem += 1

            Dim ThumbnailPanel As Panel = CType(Me.FindControl("ThumbnailPanel_" & ThumbnailItem), Panel)

            If Not ThumbnailPanel Is Nothing Then
                If ImageItemLocal.ThumbnailURL <> "" Then
                    Dim ThumbnailButton As ImageButton = CType(ThumbnailPanel.FindControl("ThumbnailButton_" & ThumbnailItem), ImageButton)

                    ThumbnailPanel.Visible = True
                    ThumbnailButton.ImageUrl = ImageItemLocal.ThumbnailURL

                    If ThumbnailItem = Me.CurrentPage Then
                        ThumbnailButton.Style("border") = "2px solid #0000FF"
                        ThumbnailButton.Attributes("onmouseover") = "this.style.border='2px solid #3399FF'"
                        ThumbnailButton.Attributes("onmouseout") = "this.style.border='2px solid #0000FF'"
                    Else
                        ThumbnailButton.Style("border") = "2px solid #000000"
                        ThumbnailButton.Attributes("onmouseover") = "this.style.border='2px solid #33CC33'"
                        ThumbnailButton.Attributes("onmouseout") = "this.style.border='2px solid #000000'"
                    End If

                    ThumbnailPanel.Visible = True
                    _NumThumbnail += 1
                Else
                    ThumbnailPanel.Visible = False
                End If

                ThumbnailPanel.Enabled = Not IsZoomActivado

            End If

        Next

        Dim Boton As Panel = CType(FindControl("LoadAllThumbnailPanel"), Panel)

        Boton.Visible = (_NumThumbnail < Me.ImageCollection.Count)
        Boton.Enabled = Not IsZoomActivado

        ' Scroll
        If Me.CurrentPage <= 1 Then ScrollState.Value = "0"

        If IsZoomActivado Then
            pnlImagen.Style("display") = "none"

            imgAjustarAlto.Style("display") = "none"
            imgAjustarAncho.Style("display") = "none"
            imgZoomIn.Style("display") = "none"
            imgZoomOut.Style("display") = "none"
            imgImprimir.Style("display") = "none"

            ddlFormato.Style("display") = "none"
            btnGuardar.Style("display") = "none"
            btnRotateLeft.Style("display") = "none"
            btnRotateRight.Style("display") = "none"

            pnlLocalZoom.Style("display") = "none"

            btnZoom.Style("border-style") = "inset"
            btnZoom.Style("border-width") = "2px"

            btnZoom.Attributes("onclick") = ""
        Else
            pnlImagen.Style("display") = ""

            imgAjustarAlto.Style("display") = ""
            imgAjustarAncho.Style("display") = ""
            imgZoomIn.Style("display") = ""
            imgZoomOut.Style("display") = ""
            imgImprimir.Style("display") = ""

            ddlFormato.Style("display") = ""
            btnGuardar.Style("display") = ""
            btnRotateLeft.Style("display") = ""
            btnRotateRight.Style("display") = ""

            pnlLocalZoom.Style("display") = ""

            btnZoom.Style("border-style") = ""
            btnZoom.Style("border-width") = ""

            btnZoom.Attributes("onclick") = "document.getElementById('" & ScrollY.ClientID & "').value = document.getElementById('divMarco').scrollTop;" & _
                                            " document.getElementById('" & ScrollX.ClientID & "').value = document.getElementById('divMarco').scrollLeft;"

        End If

        pnlZoom.Visible = IsZoomActivado

        btnNextLeft.Enabled = Not IsZoomActivado
        btnNextRight.Enabled = Not IsZoomActivado
        btnEndLeft.Enabled = Not IsZoomActivado
        btnEndRight.Enabled = Not IsZoomActivado
        ddlPagina.Enabled = Not IsZoomActivado

        Dim TempImage As String = Page.Server.MapPath(MyTempURL & Me.Key & "-ImagenZoom" & "-" & ZoomFileName & ".jpg")

        If File.Exists(TempImage) Then
            Dim Imagen As New Bitmap(TempImage)
            imgBaseZoom.Style("Width") = Imagen.Width & "px"
            imgBaseZoom.Style("Height") = Imagen.Height & "px"

            If Imagen.Width < Me.Width.Value - _ThumbnailBarWidth Then
                pnlZoom.Style("margin-left") = CInt(((Me.Width.Value - _ThumbnailBarWidth - Imagen.Width) / 2)) & "px"
            Else
                pnlZoom.Style("margin-left") = ""
            End If
        Else
            imgBaseZoom.Style("Width") = "10px"
            imgBaseZoom.Style("Height") = "10px"

            pnlZoom.Style("margin-left") = ""
        End If

        ' Formatos
        If (ddlFormato.Items.Count = 0) Then
            ddlFormato.Items.Clear()

            ddlFormato.Items.Add(New ListItem("GIF", "GIF"))
            ddlFormato.Items.Add(New ListItem("JPG", "JPG"))
            ddlFormato.Items.Add(New ListItem("PNG", "PNG"))
            ddlFormato.Items.Add(New ListItem("TIFF Color", "TIFFC"))
            ddlFormato.Items.Add(New ListItem("TIFF B&N", "TIFFBN"))
            ddlFormato.Items.Add(New ListItem("PDF", "PDF"))
            ddlFormato.SelectedIndex = 3
        End If

        Refresh()
    End Sub

    Private Sub RegistrarFunciones()
        LoadData()

        Dim strScript As New StringBuilder("")
        Dim FileName As String = Page.Server.MapPath(Me.MyIconURL & "DefaultImage.gif")
        Dim Alto As Short = 0
        Dim Ancho As Short = 0

        If Not ImageItem Is Nothing Then
            If ImageItem.ImageURL <> "" Then
                FileName = Me.Page.MapPath(ImageItem.ImageURL)
            End If
        End If

        If File.Exists(FileName) Then
            Dim Imagen As New Bitmap(FileName)

            Alto = CShort(Imagen.Height)
            Ancho = CShort(Imagen.Width)

            Imagen.Dispose()
        End If

        strScript.Append("<script type=""text/jscript"">" & vbCrLf)

        strScript.Append("<!-- 'FUNCIONES DocumentViewer'" & vbCrLf)

        'strScript.Append("attachEvent ('onload', CambiarTamano);" & vbCrLf)
        'strScript.Append("attachEvent ('onload', SetScroll);" & vbCrLf)
        'strScript.Append("attachEvent ('onload', SetZoom);" & vbCrLf)        
        strScript.Append("setTimeout(function(){ AjustarAlto(); }, 500);" & vbCrLf)

        strScript.Append("var AltoImagen = " & Alto & ";" & vbCrLf)
        strScript.Append("var AnchoImagen = " & Ancho & ";" & vbCrLf)
        strScript.Append("var AltoMarco = " & Me.Height.Value - 40 & ";" & vbCrLf)
        strScript.Append("var AnchoMarco = " & Me.Width.Value - _ThumbnailBarWidth - 20 & ";" & vbCrLf)
        strScript.Append("var Zoom = " & CType(FindControl("ZoomState"), HtmlInputHidden).Value & ";" & vbCrLf)

        '-----------------------------------------
        ' Scroll
        '-----------------------------------------
        strScript.Append("function SetScroll() {" & vbCrLf)

        If MostrarImagen Then
            If _NumThumbnail >= Me.CurrentPage Then
                Dim Imagen As Panel = CType(Me.FindControl("ThumbnailPanel_" & Me.CurrentPage), Panel)
                strScript.Append(" document.getElementById('divThumbnails').scrollTop = document.getElementById('" & Imagen.ClientID & "').offsetTop;" & vbCrLf)
            Else
                Dim Imagen As Panel = CType(Me.FindControl("ThumbnailPanel_" & Me._NumThumbnail), Panel)
                strScript.Append(" document.getElementById('divThumbnails').scrollTop = document.getElementById('" & Imagen.ClientID & "').offsetTop;" & vbCrLf)
            End If
        Else
            'strScript.Append(" document.getElementById('divThumbnails').scrollTop = document.getElementById('" & ScrollState.ClientID & "').value;" & vbCrLf)
            strScript.Append(" document.getElementById('divThumbnails').scrollTop = document.getElementById('" & ScrollState.ClientID & "').value;" & vbCrLf)
        End If

        strScript.Append(" document.getElementById('divMarco').scrollLeft = document.getElementById('" & ScrollX.ClientID & "').value;" & vbCrLf)
        strScript.Append(" document.getElementById('divMarco').scrollTop = document.getElementById('" & ScrollY.ClientID & "').value;" & vbCrLf)

        strScript.Append("}")

        '-----------------------------------------

        strScript.Append("function Imprimir()" & vbCrLf)
        strScript.Append("{ var pagina = " & Chr(34) & MyIconURL & "PrintHTMLPage.htm"";" & vbCrLf)

        If Me.ImageItem Is Nothing Then
            strScript.Append("  var Imagen = " & Chr(34) & Chr(34) & ";" & vbCrLf)
        Else
            strScript.Append("  var Imagen = " & Chr(34) & ResolveUrl(Me.ImageItem.ImageURL) & Chr(34) & ";" & vbCrLf)
        End If
        
        strScript.Append("  pagina +=""?Imagen="" + escape(Imagen);" & vbCrLf)
        strScript.Append("  window.open(pagina,'Imprimir','height=10,width=10');}" & vbCrLf)

        strScript.Append("function SetZoom()" & vbCrLf)
        strScript.Append("{ var ZoomV = '" & CType(FindControl("ZoomValue"), HtmlInputHidden).Value & "';" & vbCrLf)
        strScript.Append("  if     (ZoomV == 'Ancho') document.forms[0].ddlZoom.value = 500;" & vbCrLf)
        strScript.Append("  else if(ZoomV == 'Alto')  document.forms[0].ddlZoom.value = 600;" & vbCrLf)
        strScript.Append("	else                      document.forms[0].ddlZoom.value = Zoom; }" & vbCrLf)

        strScript.Append("function SeleccionarZoom()" & vbCrLf)
        strScript.Append("{	Zoom = document.forms[0].ddlZoom.value; " & vbCrLf)
        strScript.Append("  if     (Zoom == 500) AjustarAncho(); " & vbCrLf)
        strScript.Append("  else if(Zoom == 600) AjustarAlto();" & vbCrLf)
        strScript.Append("	else{   CambiarTamano();" & vbCrLf)
        strScript.Append("	        document.getElementById('" & CType(FindControl("ZoomValue"), HtmlInputHidden).ClientID & "').value = 'Zoom'; }}" & vbCrLf)

        strScript.Append("function AjustarAncho()" & vbCrLf)
        strScript.Append("{ Zoom = (AnchoMarco / AnchoImagen) * 100; " & vbCrLf)
        strScript.Append("	document.forms[0].ddlZoom.value = 500; " & vbCrLf)
        strScript.Append("  document.getElementById('" & CType(FindControl("ZoomValue"), HtmlInputHidden).ClientID & "').value = 'Ancho';" & vbCrLf)
        strScript.Append("	CambiarTamano(); }" & vbCrLf)

        strScript.Append("function AjustarAlto()" & vbCrLf)
        strScript.Append("{ Zoom = (AltoMarco / AltoImagen) * 100;" & vbCrLf)
        strScript.Append("	document.forms[0].ddlZoom.value = 600;" & vbCrLf)
        strScript.Append("  document.getElementById('" & CType(FindControl("ZoomValue"), HtmlInputHidden).ClientID & "').value = 'Alto';" & vbCrLf)
        strScript.Append("	CambiarTamano(); }" & vbCrLf)

        strScript.Append("function ZoomIn()" & vbCrLf)
        strScript.Append("{ if      (Zoom <  10) Zoom = 10;" & vbCrLf)
        strScript.Append("  else if (Zoom <  25) Zoom =  25;" & vbCrLf)
        strScript.Append("  else if (Zoom <  50) Zoom =  50;" & vbCrLf)
        strScript.Append("  else if (Zoom <  75) Zoom =  75;" & vbCrLf)
        strScript.Append("  else if (Zoom < 100) Zoom = 100;" & vbCrLf)
        strScript.Append("  else if (Zoom < 150) Zoom = 150;" & vbCrLf)
        strScript.Append("  else if (Zoom < 200) Zoom = 200;" & vbCrLf)
        strScript.Append("  else if (Zoom < 300) Zoom = 300;" & vbCrLf)
        strScript.Append("  else if (Zoom < 400) Zoom = 400;" & vbCrLf)
        strScript.Append("	document.forms[0].ddlZoom.value = Zoom;" & vbCrLf)
        strScript.Append("	CambiarTamano(); }" & vbCrLf)

        strScript.Append("function ZoomOut()" & vbCrLf)
        strScript.Append("{ if      (Zoom > 400) Zoom = 400;" & vbCrLf)
        strScript.Append("  else if (Zoom > 300) Zoom = 200;" & vbCrLf)
        strScript.Append("  else if (Zoom > 200) Zoom = 200;" & vbCrLf)
        strScript.Append("  else if (Zoom > 150) Zoom = 150;" & vbCrLf)
        strScript.Append("  else if (Zoom > 100) Zoom = 100;" & vbCrLf)
        strScript.Append("  else if (Zoom >  75) Zoom =  75;" & vbCrLf)
        strScript.Append("  else if (Zoom >  50) Zoom =  50;" & vbCrLf)
        strScript.Append("  else if (Zoom >  25) Zoom =  25;" & vbCrLf)
        strScript.Append("  else if (Zoom >  10) Zoom =  10;" & vbCrLf)
        strScript.Append("	document.forms[0].ddlZoom.value = Zoom;" & vbCrLf)
        strScript.Append("	CambiarTamano(); }" & vbCrLf)

        strScript.Append("function CambiarTamano()" & vbCrLf)
        strScript.Append("{ var NewAlto;" & vbCrLf)
        strScript.Append("	var NewAncho;" & vbCrLf)
        strScript.Append("	document.getElementById('" & CType(FindControl("ZoomState"), HtmlInputHidden).ClientID & "').value = Zoom;" & vbCrLf)
        strScript.Append("	if(Zoom == 500) " & vbCrLf)
        strScript.Append("	{   NewAncho = AnchoMarco; " & vbCrLf)
        strScript.Append("		NewAlto = (AnchoMarco / AnchoImagen) * AltoImagen; }" & vbCrLf)
        strScript.Append("	else if(Zoom == 600) " & vbCrLf)
        strScript.Append("	{   NewAlto = AltoMarco; " & vbCrLf)
        strScript.Append("		NewAncho = (AltoMarco / AltoImagen) * AnchoImagen; }" & vbCrLf)
        strScript.Append("	else " & vbCrLf)
        strScript.Append("	{   NewAlto = (Zoom / 100) * AltoImagen;" & vbCrLf)
        strScript.Append("		NewAncho = (Zoom / 100) * AnchoImagen; }" & vbCrLf)
        strScript.Append("	document.forms[0]." & Me.ClientID & "_imgBase.height = NewAlto;" & vbCrLf)
        strScript.Append("	document.forms[0]." & Me.ClientID & "_imgBase.width = NewAncho; }" & vbCrLf)

        strScript.Append("-->" & vbCrLf)

        strScript.Append("</script>" & vbCrLf)

        If (Not Me.Page.ClientScript.IsClientScriptBlockRegistered("DocumentViewerButtonScript")) Then
            Me.Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DocumentViewerButtonScript", strScript.ToString())
        End If
    End Sub

    Private Sub InsertThumbnails(ByVal nControls As ControlCollection)
        Dim Boton As New Button
        Dim BotonPanel As New Panel
        Dim Total As Integer

        If Me.Pages > 0 Then
            Total = Me.Pages
        Else
            Total = ThumbnailsIniciales
        End If

        For i As Integer = 1 To Total
            Dim ThumbnailPanel As New Panel
            Dim ThumbnailButton As New ImageButton

            ThumbnailPanel.ID = "ThumbnailPanel_" & i
            ThumbnailPanel.Visible = False

            ThumbnailButton.ID = "ThumbnailButton_" & i
            ThumbnailButton.ImageUrl = ""
            ThumbnailButton.ToolTip = CStr(i)
            ThumbnailButton.Attributes("onclick") = "document.getElementById('" & ScrollState.ClientID & "').value = document.getElementById('divThumbnails').scrollTop;"
            AddHandler ThumbnailButton.Click, AddressOf Me.ThumbnailClicked

            ThumbnailPanel.Controls.Add(New LiteralControl("<br/>" & vbCrLf))
            ThumbnailPanel.Controls.Add(ThumbnailButton)
            ThumbnailPanel.Controls.Add(New LiteralControl("<br/>" & vbCrLf))

            nControls.Add(ThumbnailPanel)
        Next

        BotonPanel.ID = "LoadAllThumbnailPanel"

        Boton.ID = "LoadAllThumbnail"
        Boton.Width = New Unit(_ThumbnailWidth, UnitType.Pixel)
        Boton.Text = "..."
        Boton.ToolTip = "Cargar todas las páginas"
        AddHandler Boton.Click, AddressOf Me.LoadAllThumbnailClicked

        BotonPanel.Controls.Add(New LiteralControl("<br/>" & vbCrLf))
        BotonPanel.Controls.Add(Boton)
        BotonPanel.Controls.Add(New LiteralControl("<br/>" & vbCrLf))

        nControls.Add(BotonPanel)
    End Sub

    Private Sub ResetScroll()
        ScrollX.Value = "0"
        ScrollY.Value = "0"
    End Sub

    Private Sub Zoom(ByVal ScrollX As Short, ByVal ScrollY As Short, ByVal Zoom As Single)
        Dim TempImage As String = Page.Server.MapPath(MyTempURL & Me.Key & "-ImagenZoom" & "-" & ZoomFileName & ".jpg")

        Dim ImagenSalida As Bitmap
        Dim Imagen As New Bitmap(Page.Server.MapPath(Me.ImageItem.ImageURL))

        Dim X, Y, W, H, MW, MH As Short

        X = CShort(ScrollX / (Zoom / 100))
        Y = CShort(ScrollY / (Zoom / 100))

        MW = CShort(Me.Width.Value - _ThumbnailBarWidth)
        MH = CShort(Me.Height.Value - 35)

        MW = CShort(MW / (Zoom / 100.0)) 'MW /= (Zoom / 100.0)
        MH = CShort(MH / (Zoom / 100.0)) 'MH /= (Zoom / 100.0)

        If MW > Imagen.Width - X Then
            W = CShort(Imagen.Width - X)
        Else
            W = MW
        End If

        If MH > Imagen.Height - Y Then
            H = CShort(Imagen.Height - Y)
        Else
            H = MH
        End If

        ' Recorta
        ImagenSalida = Imagen.Clone(New Rectangle(X, Y, W, H), Imagen.PixelFormat)

        W = CShort(W * (Zoom / 100))
        H = CShort(H * (Zoom / 100))

        ' Zoom
        ImagenSalida = New Bitmap(ImagenSalida, W, H)

        Dim myEncoder As Imaging.Encoder
        Dim myImageCodecInfo As Imaging.ImageCodecInfo
        Dim myEncoderParameter As Imaging.EncoderParameter
        Dim myEncoderParameters As Imaging.EncoderParameters

        myImageCodecInfo = getEncoderInfo("image/gif")
        ' Crea un objeto Codificador basado en SaveFlag.
        myEncoder = Imaging.Encoder.SaveFlag

        ' Crea los parámetros de codificación
        myEncoderParameters = New Imaging.EncoderParameters(1)
        myEncoderParameter = New Imaging.EncoderParameter(myEncoder, 0)
        myEncoderParameters.Param(0) = myEncoderParameter

        Dim myFileStream As New FileStream(TempImage, FileMode.Create)

        ImagenSalida.Save(myFileStream, myImageCodecInfo, myEncoderParameters)

        myFileStream.Close()

        myFileStream.Dispose()
        ImagenSalida.Dispose()
        Imagen.Dispose()


    End Sub

    Private Sub Guardar()
        RaiseEvent Save(CType([Enum].Parse(GetType(EnumSaveFormat), ddlFormato.SelectedValue), EnumSaveFormat), Me.CurrentPage)
    End Sub

    Private Sub Rotar(ByVal Modo As EnumRotate)
        Dim OldFileName As String = Page.Server.MapPath(Me.ImageItem.ImageURL)
        Dim NewFileName As String = MyTempURL & Me.Key & "Imagen" & "F" & Me.CurrentPage & "R" & Me.ImageItem.Rotacion & ".jpg"

        Me.ImageItem.ImageURL = NewFileName

        NewFileName = Page.Server.MapPath(NewFileName)

        If Not File.Exists(NewFileName) Then
            Dim Imagen As New Bitmap(OldFileName)

            If Modo = EnumRotate.IZQUIERDA Then
                Imagen.RotateFlip(RotateFlipType.Rotate270FlipNone)
            Else
                Imagen.RotateFlip(RotateFlipType.Rotate90FlipNone)
            End If

            Dim myEncoder As Imaging.Encoder
            Dim myImageCodecInfo As Imaging.ImageCodecInfo
            Dim myEncoderParameter As Imaging.EncoderParameter
            Dim myEncoderParameters As Imaging.EncoderParameters

            myImageCodecInfo = getEncoderInfo("image/gif")
            ' Crea un objeto Codificador basado en SaveFlag.
            myEncoder = Imaging.Encoder.SaveFlag

            ' Crea los parámetros de codificación
            myEncoderParameters = New Imaging.EncoderParameters(1)
            myEncoderParameter = New Imaging.EncoderParameter(myEncoder, 0)
            myEncoderParameters.Param(0) = myEncoderParameter

            Dim myFileStream As New FileStream(NewFileName, FileMode.Create)

            Imagen.Save(myFileStream, myImageCodecInfo, myEncoderParameters)

            myFileStream.Close()

        End If

    End Sub

    Private Sub Cargar_Folio(ByVal Folio As Short, ByVal FileName As String)
        Dim Data() As Byte
        Data = getImage(Folio)

        If Not Data Is Nothing Then
            Dim fsOutput As New FileStream(Page.Server.MapPath(FileName), FileMode.Create, FileAccess.Write)
            fsOutput.Write(Data, 0, Data.Length)
            fsOutput.Close()
        End If
    End Sub

    Private Sub LoadThumbnailImages(ByVal FolioInicial As Integer, ByVal FolioFinal As Integer)
        Dim Imagenes As List(Of Byte()) ' ArrayLis

        Imagenes = getThumbnailImage(FolioInicial, FolioFinal)

        If Not Imagenes Is Nothing Then
            Me.LoadData()

            For Folio As Integer = FolioInicial To FolioFinal
                Dim Col As List(Of DocumentItemType)
                Dim Data() As Byte = Imagenes(Folio - FolioInicial)

                Col = Me.ImageCollection

                Dim ImageItemLocal = Col(Folio - 1)
                ImageItemLocal.ThumbnailURL = MyTempURL & Me.Key & "Imagen" & "F" & Folio & "Thumbnail.jpg"

                Dim fsOutput As New FileStream(Page.Server.MapPath(ImageItemLocal.ThumbnailURL), FileMode.Create, FileAccess.Write)
                fsOutput.Write(Data, 0, Data.Length)
                fsOutput.Close()

                Me.ImageCollection = Col
            Next
        End If
    End Sub

    Private Sub GenerateKey()
        Page.Session("Key-" & Me.ClientID) = Page.Session.SessionID & "-" & Format(Now, "hhmmss")
    End Sub

    Private Sub GenerateZoomKey()
        Page.Session("Zoom-" & Me.ClientID) = Format(Now, "hhmmss")
    End Sub

#End Region

#Region " Funciones "

    Private Function getEncoderInfo(ByVal mimeType As String) As Imaging.ImageCodecInfo
        Dim i As Integer
        Dim myEncoders() As Imaging.ImageCodecInfo

        myEncoders = Imaging.ImageCodecInfo.GetImageEncoders()

        For i = 0 To myEncoders.Length - 1
            If myEncoders(i).MimeType = mimeType Then
                Return myEncoders(i)
            End If
        Next

        Return Nothing
    End Function

    Private Function getImage(ByVal Folio As Short) As Byte()
        _LocalFileProvider.FileNames = ImageUrl

        Return _LocalFileProvider.GetFolio(Folio, Resolucion, ImageManager.EnumFormat.Png)
    End Function

    Private Function getThumbnailImage(ByVal FolioInicial As Integer, ByVal FolioFinal As Integer) As List(Of Byte())
        Try
            Dim FileProvider As FileProvider = _LocalFileProvider
            _LocalFileProvider.FileNames = ImageUrl

            Return FileProvider.GetThumbnail(FolioInicial, FolioFinal, Me.ThumbnailWidth, 100, ImageManager.EnumFormat.JPEG)

        Catch ex As Exception
            RaiseEvent SendError(Me, ex.Message)
        End Try

        Return Nothing
    End Function

#End Region

End Class
