Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports AjaxControlToolkit

<Assembly: TagPrefix("CoreControls", "CoreControls")> 

Public Class DFecha
    Inherits System.Web.UI.WebControls.CompositeControl

#Region "variables"

    Private valida As New MaskedEditValidator
    Private texto As New TextBox
    Private Calendar As New CalendarExtender
    Private mascara As New MaskedEditExtender
    Private Water As New TextBoxWatermarkExtender

    Private VarDateFormat As String = "dd/MM/yyyy"
    Private VarDateMask As String = "99/99/9999"
    Private VarIsRequiered As Boolean = False
    Private VarEmptyValueMessage As String = "  *"
    Private VarInvalidValueMessage As String = "  El dato no es valido"
    Private VarTooltipMessage As String = "  Ingrese Dato"

#End Region

#Region "Crea Control"

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()
        Dim Name As String = Me.UniqueID.Replace("$", "_")
        texto.ID = Name & "DFechaText"
        texto.Width = 70
        texto.Attributes.Add("Onfocus", "this.style.backgroundColor='#FFFFE0'")
        texto.Attributes.Add("Onblur", "this.style.backgroundColor='#FFFFFF'")

        Controls.Add(texto)

        Calendar.ID = texto.ID & "_CalendarExtender"
        Calendar.Enabled = True
        Calendar.Format = VarDateFormat
        Calendar.TargetControlID = texto.ID
        Controls.Add(Calendar)

        mascara.ID = texto.ID & "_MaskedEditExtender"
        mascara.Mask = VarDateMask
        mascara.MaskType = MaskedEditType.Date
        mascara.Enabled = True
        mascara.AutoComplete = True
        mascara.TargetControlID = texto.ID
        Controls.Add(mascara)

        If VarIsRequiered = True Then
            valida.ID = texto.UniqueID & "_MaskedEditValidator"
            valida.ControlToValidate = texto.ID
            valida.EmptyValueMessage = VarEmptyValueMessage
            valida.InvalidValueMessage = VarInvalidValueMessage
            valida.IsValidEmpty = False
            valida.TooltipMessage = VarTooltipMessage
            valida.ControlExtender = texto.ID & "_MaskedEditExtender"
            valida.ForeColor = Drawing.Color.Red
            Controls.Add(valida)
        End If

        If WaterText.Trim <> "" Then
            Dim lit As New Literal
            lit.Text = "<style type=""text/css"">.watermarked {height:18px;width:150px;padding:2px 0 0 2px;border:1px solid #BEBEBE;background-color:#F0F8FF;color:gray;}</style>"
            Controls.Add(lit)

            Water.ID = Me.UniqueID & "_TextBoxWatermarkExtender"
            Water.WatermarkText = WaterText
            Water.TargetControlID = texto.ID
            Water.WatermarkCssClass = "watermarked"
            Controls.Add(Water)
        End If

        AddHandler texto.TextChanged, AddressOf OnTextChanged

    End Sub

#End Region

#Region "Propertys"

    Public Property AutoPostBack() As Boolean
        Get
            Return texto.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            texto.AutoPostBack = value
        End Set
    End Property


    Public Property WaterText() As String
        Get
            Return Water.WatermarkText
        End Get
        Set(ByVal value As String)
            Water.WatermarkText = value
        End Set
    End Property

    Public Overrides Property Enabled() As Boolean
        Get
            Return texto.Enabled
        End Get
        Set(ByVal value As Boolean)
            texto.Enabled = value
            If value = False Then
                texto.BackColor = Drawing.Color.FromName("#EAE8E3")
            Else
                texto.BackColor = Drawing.Color.FromName("#FFFFFF")
            End If
        End Set
    End Property

    Public Property ValidationGroup() As String
        Get
            Return valida.ValidationGroup
        End Get
        Set(ByVal value As String)
            valida.ValidationGroup = value
        End Set
    End Property

    ''' <summary>
    ''' Formato de Fecha representado en (Dias dd, Meses MM, Años yyyy)
    ''' </summary>
    ''' <value>Formato de Fecha (Por defecto dd/MM/yyyy)</value>
    ''' <returns>Formato de Fecha (Por defecto dd/MM/yyyy)</returns>
    ''' <remarks></remarks>
    Public Property DateFormat() As String
        Get
            Return VarDateFormat
        End Get
        Set(ByVal value As String)
            VarDateFormat = value

            Dim mascara As String = value
            mascara = mascara.Replace("d", "9")
            mascara = mascara.Replace("y", "9")
            mascara = mascara.Replace("M", "9")
            VarDateMask = mascara
        End Set
    End Property

    ''' <summary>
    ''' Valida que el control Requiera valores o no
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Property IsRequiered() As Boolean
        Get
            Return VarIsRequiered
        End Get
        Set(ByVal value As Boolean)
            VarIsRequiered = value
        End Set
    End Property

    ''' <summary>
    ''' Texto que representa una fecha
    ''' </summary>
    ''' <value>Fecha</value>
    ''' <returns>Fecha</returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Try
                Return texto.Text.Trim
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            texto.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje de ayuda que mostrara el control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property TooltipMessage() As String
        Get
            Return VarTooltipMessage
        End Get
        Set(ByVal value As String)
            VarTooltipMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje que se muestra cuando el valor de la fecha no es valido
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property InvalidValueMessage() As String
        Get
            Return VarInvalidValueMessage
        End Get
        Set(ByVal value As String)
            VarInvalidValueMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje que se muestra cuando el control es Nulo o no tiene ningun valor
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property EmptyValueMessage() As String
        Get
            Return VarEmptyValueMessage
        End Get
        Set(ByVal value As String)
            VarEmptyValueMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Longitud maxima del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property MaxLength() As Integer
        Get
            Return texto.MaxLength
        End Get
        Set(ByVal value As Integer)
            texto.MaxLength = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el alto del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property Heigth() As Unit
        Get
            Return texto.Height
        End Get
        Set(ByVal value As Unit)
            texto.Height = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ancho del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Overrides Property Width() As Unit
        Get
            Return texto.Width
        End Get
        Set(ByVal value As Unit)
            texto.Width = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece color de fondo del control
    ''' </summary>
    ''' <value>System.Drawing.Color</value>
    ''' <returns>System.Drawing.Color</returns>
    ''' <remarks></remarks>
    Public Property BackColor_() As System.Drawing.Color
        Get
            Return texto.BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            texto.BackColor = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece La clase que dara estilo al control
    ''' </summary>
    ''' <value>CssClass Name</value>
    ''' <returns>CssClass Name</returns>
    ''' <remarks></remarks>
    Public Property CssClass_() As String
        Get
            Return texto.CssClass
        End Get
        Set(ByVal value As String)
            texto.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el color del mensaje Ajax cuando no se cumplen ciertas condiciones
    ''' </summary>
    ''' <value>Color</value>
    ''' <returns>Color</returns>
    ''' <remarks></remarks>
    Public Property MensajeColor() As System.Drawing.Color
        Get
            Return valida.ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            valida.ForeColor = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Public Event TextChanged As EventHandler
    Protected Overridable Sub OnTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent TextChanged(Me, e)
    End Sub

#End Region

#Region "Funciones"

    Public Sub Clear()
        texto.Text = ""
    End Sub

    'Public Overrides Sub Focus()
    '    texto.Focus()
    'End Sub

#End Region

End Class

Public Class DTexto
    Inherits System.Web.UI.WebControls.CompositeControl

#Region "variables"

    Private VarIsRequiered As Boolean = False
    Private VarEmptyValueMessage As String = "  *"
    Private VarInvalidValueMessage As String = "  El dato no es valido"
    Private VarTooltipMessage As String = ""

    Private ValidaCon As New RequiredFieldValidator
    Private texto As New TextBox
    Private Water As New TextBoxWatermarkExtender

#End Region

#Region "Crea control"

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()

        Dim Name As String = Me.UniqueID.Replace("$", "_")
        texto.ID = Name & "DText"

        texto.Attributes.Add("Onfocus", "this.style.backgroundColor='#FFFFE0'")
        texto.Attributes.Add("Onblur", "this.style.backgroundColor='#FFFFFF'")

        Controls.Add(texto)

        If VarIsRequiered = True Then
            ValidaCon.ID = Me.UniqueID & "_RequiredFieldValidator"
            ValidaCon.ControlToValidate = texto.ID
            ValidaCon.ErrorMessage = VarEmptyValueMessage
            ValidaCon.ForeColor = Drawing.Color.Red
            Controls.Add(ValidaCon)
        End If

        If WaterText.Trim <> "" Then
            Dim lit As New Literal
            lit.Text = "<style type=""text/css"">.watermarked {height:18px;width:150px;padding:2px 0 0 2px;border:1px solid #BEBEBE;background-color:#F0F8FF;color:gray;}</style>"
            Controls.Add(lit)

            Water.ID = Me.UniqueID & "_TextBoxWatermarkExtender"
            Water.WatermarkText = WaterText
            Water.TargetControlID = texto.ID
            Water.WatermarkCssClass = "watermarked"
            Controls.Add(Water)
        End If

        AddHandler texto.TextChanged, AddressOf OnTextChanged
    End Sub

#End Region

#Region "Propertys"

    'Public Property Multiline() As Boolean
    '    Get

    '    End Get
    '    Set(ByVal value As Boolean)

    '    End Set
    'End Property

    Public Property AutoPostBack() As Boolean
        Get
            Return texto.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            texto.AutoPostBack = value
        End Set
    End Property

    Public Property WaterText() As String
        Get
            Return Water.WatermarkText
        End Get
        Set(ByVal value As String)
            Water.WatermarkText = value
        End Set
    End Property

    Public Overrides Property Enabled() As Boolean
        Get
            Return texto.Enabled
        End Get
        Set(ByVal value As Boolean)
            texto.Enabled = value
            If value = False Then
                texto.BackColor = Drawing.Color.FromName("#EAE8E3")
            Else
                texto.BackColor = Drawing.Color.FromName("#FFFFFF")
            End If
        End Set
    End Property

    Public Property ValidationGroup() As String
        Get
            Return ValidaCon.ValidationGroup
        End Get
        Set(ByVal value As String)
            ValidaCon.ValidationGroup = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje de ayuda que mostrara el control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property TooltipMessage() As String
        Get
            Return VarTooltipMessage
        End Get
        Set(ByVal value As String)
            VarTooltipMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje que se muestra cuando el valor de la fecha no es valido
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property InvalidValueMessage() As String
        Get
            Return VarInvalidValueMessage
        End Get
        Set(ByVal value As String)
            VarInvalidValueMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje que se muestra cuando el control es Nulo o no tiene ningun valor
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property EmptyValueMessage() As String
        Get
            Return VarEmptyValueMessage
        End Get
        Set(ByVal value As String)
            VarEmptyValueMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Valida que el control Requiera valores o no
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Property IsRequiered() As Boolean
        Get
            Return VarIsRequiered
        End Get
        Set(ByVal value As Boolean)
            VarIsRequiered = value
        End Set
    End Property

    ''' <summary>
    ''' Texto que contiene el control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Try
                Return texto.Text.Trim
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            texto.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Longitud maxima del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property MaxLength() As Integer
        Get
            Return texto.MaxLength
        End Get
        Set(ByVal value As Integer)
            texto.MaxLength = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el alto del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property Heigth() As Unit
        Get
            Return texto.Height
        End Get
        Set(ByVal value As Unit)
            texto.Height = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ancho del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Overrides Property Width() As Unit
        Get
            Return texto.Width
        End Get
        Set(ByVal value As Unit)
            texto.Width = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece color de fondo del control
    ''' </summary>
    ''' <value>System.Drawing.Color</value>
    ''' <returns>System.Drawing.Color</returns>
    ''' <remarks></remarks>
    Public Property BackColor_() As System.Drawing.Color
        Get
            Return texto.BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            texto.BackColor = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece La clase que dara estilo al control
    ''' </summary>
    ''' <value>CssClass Name</value>
    ''' <returns>CssClass Name</returns>
    ''' <remarks></remarks>
    Public Property CssClass_() As String
        Get
            Return texto.CssClass
        End Get
        Set(ByVal value As String)
            texto.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el control es multilinea
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Property Multiline() As TextBoxMode
        Get
            Return texto.TextMode
        End Get
        Set(ByVal value As TextBoxMode)
            texto.TextMode = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el color del mensaje Ajax cuando no se cumplen ciertas condiciones
    ''' </summary>
    ''' <value>Color</value>
    ''' <returns>Color</returns>
    ''' <remarks></remarks>
    Public Property MensajeColor() As System.Drawing.Color
        Get
            Return ValidaCon.ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            ValidaCon.ForeColor = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Public Event TextChanged As EventHandler

    Protected Overridable Sub OnTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent TextChanged(Me, e)
    End Sub

#End Region

#Region "Funciones"

    Public Sub Clear()
        texto.Text = ""
    End Sub

    'Public Overrides Sub Focus()
    '    texto.Focus()
    'End Sub

#End Region

End Class

Public Class DNumber
    Inherits System.Web.UI.WebControls.CompositeControl

#Region "variables"

    Private VarIsRequiered As Boolean = False
    Private VarRange As Boolean = False
    Private VarEmptyValueMessage As String = "  *"
    Private VarInvalidValueMessage As String = "  El dato no es válido"
    Private VarTooltipMessage As String = "  Ingrese un valor numérico"
    Private Type As TypeNumber
    Private VarMinimum As String
    Private VarMaximum As String
    Private VarPuntoFlotante As Boolean

    Private VarValidaRangs As New RangeValidator
    Private validaCon As New RequiredFieldValidator
    Private texto As New TextBox
    Private Water As New TextBoxWatermarkExtender

#End Region

#Region "Enums"

    Enum TypeNumber
        Smallint = 5
        Int = 10
        TinyInt = 3
        BigInt = 19
        Custom = 0
    End Enum

#End Region

#Region "Crea control"

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()
        Dim Name As String = Me.UniqueID.Replace("$", "_")
        texto.ID = Name & "DNumber"

        texto.Attributes.Add("Onfocus", "this.style.backgroundColor='#FFFFE0'")
        texto.Attributes.Add("Onblur", "this.style.backgroundColor='#FFFFFF'")
        Controls.Add(texto)

        VarValidaRangs.ID = texto.ID & "_ValidateRang"
        VarValidaRangs.ForeColor = Drawing.Color.Red

        If VarPuntoFlotante = False Then
            VarValidaRangs.Type = ValidationDataType.Integer
        Else
            VarValidaRangs.Type = ValidationDataType.Double
        End If

        VarValidaRangs.ControlToValidate = texto.ID
        VarValidaRangs.ErrorMessage = "Error - El valor debe ser númerico"
        VarValidaRangs.ValidationGroup = ValidationGroup


        If VarIsRequiered = True Then
            validaCon.ID = Me.UniqueID & "_RequiredFieldValidator"
            validaCon.ControlToValidate = texto.ID
            validaCon.ErrorMessage = VarEmptyValueMessage
            validaCon.ForeColor = Drawing.Color.Red
            Controls.Add(validaCon)
        End If

        If VarRange = True Then
            Try
                VarValidaRangs.MaximumValue = Me.MaximumValue
                VarValidaRangs.MinimumValue = Me.MinimumValue
            Catch : End Try
        Else
            VarValidaRangs.MaximumValue = "99999999"
            VarValidaRangs.MinimumValue = "0"
        End If

        Controls.Add(VarValidaRangs)
        
        If WaterText.Trim <> "" Then
            Dim lit As New Literal
            lit.Text = "<style type=""text/css"">.watermarked {height:18px;width:150px;padding:2px 0 0 2px;border:1px solid #BEBEBE;background-color:#F0F8FF;color:gray;}</style>"
            Controls.Add(lit)

            Water.ID = Me.UniqueID & "_TextBoxWatermarkExtender"
            Water.WatermarkText = WaterText
            Water.TargetControlID = texto.ID
            Water.WatermarkCssClass = "watermarked"
            Controls.Add(Water)
        End If

        AddHandler texto.TextChanged, AddressOf OnTextChanged

    End Sub

#End Region

#Region "Propertys"

    Public Property AceptaPuntoFlotante() As Boolean
        Get
            Return VarPuntoFlotante
        End Get
        Set(ByVal value As Boolean)
            VarPuntoFlotante = value
        End Set
    End Property

    Public Property IsRange() As Boolean
        Get
            Return VarRange
        End Get
        Set(ByVal value As Boolean)
            VarRange = value
        End Set
    End Property

    Public Property MinimumValue() As String
        Get
            Return VarMinimum
        End Get
        Set(ByVal value As String)
            If Type = TypeNumber.Custom Then
                VarMinimum = value
            Else
                VarMinimum = CStr(Data.Item(Type).ValorMinimo)
            End If
        End Set
    End Property

    Public Property MaximumValue() As String
        Get
            Return VarMaximum
        End Get
        Set(ByVal value As String)
            If Type = TypeNumber.Custom Then
                VarMaximum = value
            Else
                VarMaximum = CStr(Data.Item(Type).ValorMaximo)
            End If
        End Set
    End Property

    Public Property AutoPostBack() As Boolean
        Get
            Return texto.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            texto.AutoPostBack = value
        End Set
    End Property

    Public Property WaterText() As String
        Get
            Return Water.WatermarkText
        End Get
        Set(ByVal value As String)
            Water.WatermarkText = value
        End Set
    End Property

    ''' <summary>
    ''' Habilita o Deshabilita el Control
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Overrides Property Enabled() As Boolean
        Get
            Return texto.Enabled
        End Get
        Set(ByVal value As Boolean)
            texto.Enabled = value
            If value = False Then
                texto.BackColor = Drawing.Color.FromName("#EAE8E3")
            Else
                texto.BackColor = Drawing.Color.FromName("#FFFFFF")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece el grupo de validacion el
    ''' </summary>
    ''' <value>Nombre Grupo Validacion</value>
    ''' <returns>Nombre Grupo Validacion</returns>
    ''' <remarks></remarks>
    Public Property ValidationGroup() As String
        Get
            Return validaCon.ValidationGroup
        End Get
        Set(ByVal value As String)
            validaCon.ValidationGroup = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje de ayuda que mostrara el control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property TooltipMessage() As String
        Get
            Return VarTooltipMessage
        End Get
        Set(ByVal value As String)
            VarTooltipMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje que se muestra cuando el valor de la fecha no es valido
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property InvalidValueMessage() As String
        Get
            Return VarInvalidValueMessage
        End Get
        Set(ByVal value As String)
            VarInvalidValueMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje que se muestra cuando el control es Nulo o no tiene ningun valor
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property EmptyValueMessage() As String
        Get
            Return VarEmptyValueMessage
        End Get
        Set(ByVal value As String)
            VarEmptyValueMessage = value
        End Set
    End Property

    ''' <summary>
    ''' Valida que el control Requiera valores o no
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Property IsRequiered() As Boolean
        Get
            Return VarIsRequiered
        End Get
        Set(ByVal value As Boolean)
            VarIsRequiered = value
        End Set
    End Property

    ''' <summary>
    ''' Texto que contiene el control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Try
                Return texto.Text.Trim
            Catch ex As Exception
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            texto.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el alto del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property Heigth() As Unit
        Get
            Return texto.Height
        End Get
        Set(ByVal value As Unit)
            texto.Height = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el ancho del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Overrides Property Width() As Unit
        Get
            Return texto.Width
        End Get
        Set(ByVal value As Unit)
            texto.Width = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece color de fondo del control
    ''' </summary>
    ''' <value>System.Drawing.Color</value>
    ''' <returns>System.Drawing.Color</returns>
    ''' <remarks></remarks>
    Public Property BackColor_() As System.Drawing.Color
        Get
            Return texto.BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            texto.BackColor = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece La clase que dara estilo al control
    ''' </summary>
    ''' <value>CssClass Name</value>
    ''' <returns>CssClass Name</returns>
    ''' <remarks></remarks>
    Public Property CssClass_() As String
        Get
            Return texto.CssClass
        End Get
        Set(ByVal value As String)
            texto.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el control es multilinea
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Property Multiline() As TextBoxMode
        Get
            Return texto.TextMode
        End Get
        Set(ByVal value As TextBoxMode)
            texto.TextMode = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el color del mensaje Ajax cuando no se cumplen ciertas condiciones
    ''' </summary>
    ''' <value>Color</value>
    ''' <returns>Color</returns>
    ''' <remarks></remarks>
    Public Property MensajeColor() As System.Drawing.Color
        Get
            Return validaCon.ForeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            validaCon.ForeColor = value
        End Set
    End Property

    ''' <summary>
    ''' Longitud maxima del control
    ''' </summary>
    ''' <value>Texto</value>
    ''' <returns>Texto</returns>
    ''' <remarks></remarks>
    Public Property MaxLength() As Integer
        Get
            Return texto.MaxLength
        End Get
        Set(ByVal value As Integer)
            If Type = TypeNumber.Custom Then
                texto.MaxLength = value
                'mascara.Mask = "9{" & CStr(value) & "}"
            Else
                texto.MaxLength = Type
                'mascara.Mask = "9{" & CStr(Type) & "}"
            End If
        End Set

    End Property

    ''' <summary>
    ''' Longitud maxima que debe tenetr un control tipificado a partir de BD
    ''' </summary>
    ''' <value>Tipo de dato</value>
    ''' <returns>Tipo de dato</returns>
    ''' <remarks></remarks>
    Public Property TypeDB() As TypeNumber
        Get
            Return Type
        End Get
        Set(ByVal value As TypeNumber)
            Type = value

            If Type <> TypeNumber.Custom Then
                Me.MaxLength = Type

                Try
                    Me.MinimumValue = CStr(Data.Item(Type).ValorMinimo)
                    Me.MaximumValue = CStr(Data.Item(Type).ValorMaximo)
                Catch : End Try

                IsRange = True
            End If

        End Set
    End Property

    Public Function Data() As TipoDatoCollection
        Dim Lista As New TipoDatoCollection
        Lista.Add(TypeNumber.Smallint, 5, -32768, 32767)
        Lista.Add(TypeNumber.Int, 10, -2147483468, 2147483467)
        Lista.Add(TypeNumber.TinyInt, 3, 0, 255)
        Lista.Add(TypeNumber.BigInt, 19, -9223372036854775807, 9223372036854775807)
        Lista.Add(TypeNumber.Custom, 0)

        Return Lista
    End Function

#End Region

#Region "Eventos"

    Public Event TextChanged As EventHandler
    Protected Overridable Sub OnTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent TextChanged(Me, e)
    End Sub

#End Region

#Region "Funciones"

    Public Sub Clear()
        texto.Text = ""
    End Sub

#End Region

    Public Class TipoDato
        Public Tipo As TypeNumber
        Public Longitud As Integer
        Public ValorMaximo As Int64
        Public ValorMinimo As Int64

        Public Sub New(ByVal nTipo As TypeNumber, ByVal nLongitud As Integer)
            Tipo = nTipo
            nLongitud = nLongitud
        End Sub

        Public Sub New()
        End Sub

        Public Sub New(ByVal nTipo As TypeNumber, ByVal nLongitud As Integer, ByVal nMinimumValue As Int64, ByVal nMaximumValue As Int64)
            Tipo = nTipo
            Longitud = nLongitud
            ValorMaximo = nMaximumValue
            ValorMinimo = nMinimumValue
        End Sub
    End Class
    Public Class TipoDatoCollection

        Public Items As New List(Of TipoDato)

        Public Sub Add(ByVal nTipo As TypeNumber, ByVal nLongitud As Integer)
            Dim data As New TipoDato(nTipo, nLongitud)
            Items.Add(data)
        End Sub

        Public Sub Add(ByVal nTipoDato As TipoDato)
            Items.Add(nTipoDAto)
        End Sub

        Public Sub Add(ByVal nTipo As TypeNumber, ByVal nLongitud As Integer, ByVal nMinimumValue As Int64, ByVal nMaximumValue As Int64)
            Dim data As New TipoDato(nTipo, nLongitud, nMinimumValue, nMaximumValue)
            Items.Add(data)
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return Items.Count
            End Get
        End Property

        Public Function Item(ByVal Key As TypeNumber) As TipoDato
            Dim Item_ As New TipoDato

            For Each Item1 As TipoDato In Items
                If Item1.Tipo = Key Then
                    Item_ = Item1
                End If
            Next

            Return Item_
        End Function

    End Class

End Class

Public Class DDropDownList
    Inherits CompositeControl

#Region "variables"

    Private VarIsRequiered As Boolean = False
    Private VarEmptyValueMessage As String = "  *"
    Private VarInvalidValueMessage As String = "  El dato no es válido"
    Private VarTooltipMessage As String = "  Ingrese un valor numérico"

    Private valida As New MaskedEditValidator
    Private mascara As New MaskedEditExtender
    Private DropDownList As New DropDownList

#End Region

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()
        Dim Name As String = Me.UniqueID.Replace("$", "_")
        DropDownList.ID = Name & "DDrop"
        Controls.Add(DropDownList)
    End Sub


End Class
