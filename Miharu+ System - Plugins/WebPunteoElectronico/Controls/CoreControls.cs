using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

[assembly: TagPrefix("CoreControls", "CoreControls")]

namespace WebPunteoElectronico.Controls
{
    public class DFecha : CompositeControl
    {
        #region Variables

        private MaskedEditValidator valida = null;
        private TextBox texto = new TextBox();
        private CalendarExtender Calendar = new CalendarExtender();
        private MaskedEditExtender mascara = new MaskedEditExtender();
        private TextBoxWatermarkExtender Water = new TextBoxWatermarkExtender();

        private string VarDateFormat = "yyyy/MM/dd";
        private MaskedEditUserDateFormat VarMaskDateFormat = MaskedEditUserDateFormat.YearMonthDay;
        private string VarDateMask = "9999/99/99";
        private bool VarIsRequiered;
        private string VarEmptyValueMessage = "  *";
        private string VarInvalidValueMessage = "  El dato no es válido";
        private string VarTooltipMessage = "  Ingrese Dato";

        #endregion

        #region  Crea Control

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            string Name = this.UniqueID.Replace("$", "_");
            texto.ID = Name + "DFechaText";
            texto.Width = 70;
            texto.Attributes.Add("Onfocus", "this.style.backgroundColor='#FFFFE0'");
            texto.Attributes.Add("Onblur", "this.style.backgroundColor='#FFFFFF'");

            Controls.Add(texto);

            Calendar.ID = texto.ID + "_CalendarExtender";
            Calendar.Enabled = true;
            Calendar.Format = VarDateFormat;

            Calendar.TargetControlID = texto.ID;
            Calendar.Animated = true;
            Controls.Add(Calendar);

            mascara.ID = texto.ID + "_MaskedEditExtender";
            mascara.Mask = VarDateMask;
            mascara.MaskType = MaskedEditType.Date;
            mascara.Enabled = true;
            mascara.UserDateFormat = MaskedEditUserDateFormat.YearMonthDay;
            mascara.AutoComplete = true;
            //        'mascara.Century = 21
            mascara.TargetControlID = texto.ID;
            mascara.CultureName = "zh-cn";
            mascara.DisplayMoney = MaskedEditShowSymbol.Left;
            mascara.AcceptNegative = MaskedEditShowSymbol.Left;
            Controls.Add(mascara);

            if (VarIsRequiered)
            {
                valida.ID = texto.UniqueID + "_MaskedEditValidator";
                valida.ControlToValidate = texto.ID;
                valida.EmptyValueMessage = VarEmptyValueMessage;
                valida.InvalidValueMessage = VarInvalidValueMessage;
                valida.IsValidEmpty = false;
                valida.TooltipMessage = VarTooltipMessage;
                valida.ControlExtender = texto.ID + "_MaskedEditExtender";
                valida.ForeColor = System.Drawing.Color.Red;
                Controls.Add(valida);
            }

            if (WaterText.Trim() != "")
            {
                var lit = new Literal
                          {
                              Text = "<style type=\"text/css\">.watermarked {height:18px;width:150px;padding:2px 0 0 2px;border:1px solid #BE" +
                                     "BEBE;background-color:#F0F8FF;color:gray;}</style>"
                          };
                Controls.Add(lit);

                Water.ID = this.UniqueID + "_TextBoxWatermarkExtender";
                Water.WatermarkText = WaterText;
                Water.TargetControlID = texto.ID;
                Water.WatermarkCssClass = "watermarked";
                Controls.Add(Water);
            }

            texto.TextChanged += this.OnTextChanged;
        }

        #endregion

        #region  Properties

        public bool AutoPostBack
        {
            get { return texto.AutoPostBack; }
            set { texto.AutoPostBack = value; }
        }

        public string WaterText
        {
            get { return Water.WatermarkText; }
            set { Water.WatermarkText = value; }
        }

        public override bool Enabled
        {
            get { return texto.Enabled; }
            set
            {
                texto.Enabled = value;
                texto.BackColor = System.Drawing.Color.FromName((value == false) ? "#EAE8E3" : "#FFFFFF");
            }
        }

        public string ValidationGroup
        {
            get { return valida.ValidationGroup; }
            set { valida.ValidationGroup = value; }
        }

        /// <summary>Formato de Fecha representado en (Dias dd, Meses MM, A?os yyyy)
        /// </summary>
        /// <value>Formato de Fecha (Por defecto dd/MM/yyyy)</value>
        /// <returns>Formato de Fecha (Por defecto dd/MM/yyyy)</returns>
        /// <remarks></remarks>
        /// 
        public string DateFormat
        {
            get { return VarDateFormat; }
            set
            {
                VarDateFormat = value;
                var localMascara = value;
                localMascara = localMascara.Replace("d", "9");
                localMascara = localMascara.Replace("y", "9");
                localMascara = localMascara.Replace("M", "9");
                VarDateMask = localMascara;
            }
        }

        public MaskedEditUserDateFormat MaskFormat
        {
            get { return VarMaskDateFormat; }
            set { VarMaskDateFormat = value; }
        }

        ///     <summary>
        ///     Valida que el control Requiera valores o no
        ///     </summary>
        ///     <value>true/false</value>
        ///     <returns>true/false</returns>
        ///     <remarks></remarks>
        ///     
        public bool IsRequiered
        {
            get { return VarIsRequiered; }
            set { VarIsRequiered = value; }
        }

        ///     <summary>   Texto que representa una fecha
        ///     </summary>
        ///     <value>Fecha</value>
        ///     <returns>Fecha</returns>
        ///     <remarks></remarks>
        public string Text
        {
            get
            {
                try
                {
                    return texto.Text.Trim();
                }
                catch
                {
                    return "";
                }
            }
            set { texto.Text = value; }
        }


        ///    <summary>
        ///    Mensaje de ayuda que mostrara el control
        ///     </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public string TooltipMessage
        {
            get { return VarTooltipMessage; }
            set { VarTooltipMessage = value; }
        }

        ///    <summary>
        ///    Mensaje que se muestra cuando el valor de la fecha no es valido
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public string InvalidValueMessage
        {
            get { return VarInvalidValueMessage; }
            set { VarInvalidValueMessage = value; }
        }

        ///    <summary>
        ///    Mensaje que se muestra cuando el control es Nulo o no tiene ningun valor
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public string EmptyValueMessage
        {
            get { return VarEmptyValueMessage; }
            set { VarEmptyValueMessage = value; }
        }

        ///    <summary>
        ///    Longitud maxima del control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public int MaxLength
        {
            get { return texto.MaxLength; }
            set { texto.MaxLength = value; }
        }

        ///    <summary>
        ///    Obtiene o establece el alto del control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public Unit Heigth
        {
            get { return texto.Height; }
            set { texto.Height = value; }
        }

        ///    <summary>
        ///    Obtiene o establece el ancho del control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public override Unit Width
        {
            get { return texto.Width; }
            set { texto.Width = value; }
        }

        ///    <summary>
        ///    Obtiene o establece color de fondo del control
        ///    </summary>
        ///    <value>System.Drawing.Color</value>
        ///    <returns>System.Drawing.Color</returns>
        ///    <remarks></remarks>
        public System.Drawing.Color BackColor_
        {
            get { return texto.BackColor; }
            set { texto.BackColor = value; }
        }

        ///    <summary>
        ///    Obtiene o establece La clase que dara estilo al control
        ///    </summary>
        ///    <value>CssClass Name</value>
        ///    <returns>CssClass Name</returns>
        ///    <remarks></remarks>
        public string CssClass_
        {
            get { return texto.CssClass; }
            set { texto.CssClass = value; }
        }

        ///    <summary>
        ///    Obtiene o establece el color del mensaje Ajax cuando no se cumplen ciertas condiciones
        ///    </summary>
        ///    <value>Color</value>
        ///    <returns>Color</returns>
        ///    <remarks></remarks>
        public System.Drawing.Color MensajeColor
        {
            get { return valida.ForeColor; }
            set { valida.ForeColor = value; }
        }

        #endregion

        #region Eventos

        public event EventHandler TextChanged;

        protected virtual void OnTextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        #endregion

        #region Funciones

        public void Clear()
        {
            texto.Text = "";
        }

        #endregion
    }

    public class DTexto : CompositeControl
    {
        #region variables

        private bool VarIsRequiered;
        private string VarEmptyValueMessage = "  *";
        private string VarInvalidValueMessage = "  El dato no es válido";
        private string VarTooltipMessage = "";

        private RequiredFieldValidator ValidaCon = new RequiredFieldValidator();
        private TextBox texto = new TextBox();
        private TextBoxWatermarkExtender Water = new TextBoxWatermarkExtender();

        #endregion

        #region Crea control

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            string Name = this.UniqueID.Replace("$", "_");
            texto.ID = Name + "DText";

            texto.Attributes.Add("Onfocus", "this.style.backgroundColor='#FFFFE0'");
            texto.Attributes.Add("Onblur", "this.style.backgroundColor='#FFFFFF'");

            Controls.Add(texto);

            if (VarIsRequiered)
            {
                ValidaCon.ID = this.UniqueID + "_RequiredFieldValidator";
                ValidaCon.ControlToValidate = texto.ID;
                ValidaCon.ErrorMessage = VarEmptyValueMessage;
                ValidaCon.ForeColor = System.Drawing.Color.Red;
                Controls.Add(ValidaCon);
            }

            if (WaterText.Trim() != "")
            {
                var lit = new Literal
                          {
                              Text = "<style type=\"text/css\">.watermarked {height:18px;width:150px;padding:2px 0 0 2px;border:1px solid #BE" +
                                     "BEBE;background-color:#F0F8FF;color:gray;}</style>"
                          };
                Controls.Add(lit);

                Water.ID = this.UniqueID + "_TextBoxWatermarkExtender";
                Water.WatermarkText = WaterText;
                Water.TargetControlID = texto.ID;
                Water.WatermarkCssClass = "watermarked";
                Controls.Add(Water);
            }

            texto.TextChanged += this.OnTextChanged;
        }

        #endregion

        #region Propiedades

        public bool AutoPostBack
        {
            get { return texto.AutoPostBack; }
            set { texto.AutoPostBack = value; }
        }

        public string WaterText
        {
            get { return Water.WatermarkText; }
            set { Water.WatermarkText = value; }
        }

        public override bool Enabled
        {
            get { return texto.Enabled; }
            set
            {
                texto.Enabled = value;
                texto.BackColor = System.Drawing.Color.FromName((value == false) ? "#EAE8E3" : "#FFFFFF");
            }
        }

        public string ValidationGroup
        {
            get { return ValidaCon.ValidationGroup; }
            set { ValidaCon.ValidationGroup = value; }

        }

        ///    <summary>
        ///    Mensaje de ayuda que mostrara el control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>M
        ///    <remarks></remarks>
        public string TooltipMessage
        {
            get { return VarTooltipMessage; }
            set { VarTooltipMessage = value; }
        }

        ///    <summary>
        ///    Mensaje que se muestra cuando el valor de la fecha no es valido
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public string InvalidValueMessage
        {
            get { return VarInvalidValueMessage; }
            set { VarInvalidValueMessage = value; }
        }

        ///    <summary>
        ///    Mensaje que se muestra cuando el control es Nulo o no tiene ningun valor
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public string EmptyValueMessage
        {
            get { return VarEmptyValueMessage; }
            set { VarEmptyValueMessage = value; }
        }

        ///    <summary>
        ///    Valida que el control Requiera valores o no
        ///    </summary>
        ///    <value>true/false</value>
        ///    <returns>true/false</returns>
        ///    <remarks></remarks>
        public bool IsRequiered
        {
            get { return VarIsRequiered; }
            set { VarIsRequiered = value; }
        }


        ///    <summary>
        ///    Texto que contiene el control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public string Text
        {
            get
            {
                try
                {
                    return texto.Text.Trim();
                }
                catch
                {
                    return "";
                }
            }
            set { texto.Text = value; }
        }

        ///    <summary>
        ///    Longitud maxima del control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public int MaxLength
        {
            get { return texto.MaxLength; }
            set { texto.MaxLength = value; }
        }

        ///    <summary>
        ///    Obtiene o establece el alto del control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public Unit Heigth
        {
            get { return texto.Height; }
            set { texto.Height = value; }
        }

        ///    <summary>
        ///    Obtiene o establece el ancho del control
        ///    </summary>
        ///    <value>Texto</value>
        ///    <returns>Texto</returns>
        ///    <remarks></remarks>
        public override Unit Width
        {
            get { return texto.Width; }
            set { texto.Width = value; }
        }

        ///    <summary>
        ///    Obtiene o establece color de fondo del control
        ///    </summary>
        ///    <value>System.Drawing.Color</value>
        ///    <returns>System.Drawing.Color</returns>
        ///    <remarks></remarks>
        public System.Drawing.Color BackColor_
        {
            get { return texto.BackColor; }
            set { texto.BackColor = value; }
        }

        ///       <summary>
        ///      Obtiene o establece La clase que dara estilo al control
        ///       </summary>
        ///       <value>CssClass Name</value>
        ///        <returns>CssClass Name</returns>
        ///        <remarks></remarks>
        public string CssClass_
        {
            get { return texto.CssClass; }
            set { texto.CssClass = value; }
        }

        ///    <summary>
        ///    Obtiene o establece si el control es multilinea
        ///    </summary>
        ///    <value>true/false</value>
        ///    <returns>true/false</returns>
        ///    <remarks></remarks>
        public TextBoxMode Multiline
        {
            get { return texto.TextMode; }
            set { texto.TextMode = value; }
        }

        ///       <summary>
        ///         Obtiene o establece el color del mensaje Ajax cuando no se cumplen ciertas condiciones
        ///       </summary>
        ///       <value>Color</value>
        ///        <returns>Color</returns>
        ///        <remarks></remarks>
        public System.Drawing.Color MensajeColor
        {
            get { return ValidaCon.ForeColor; }
            set { ValidaCon.ForeColor = value; }
        }

        #endregion

        #region Eventos

        public event EventHandler TextChanged;

        protected virtual void OnTextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        #endregion

        #region Funciones

        public void Clear()
        {
            texto.Text = "";
        }

        #endregion
    }

    public class DNumber : CompositeControl
    {
        #region variables

        private TypeNumber Type;
        private string VarMaximum;
        private bool VarIsRequiered;
        private bool VarRange;
        private string VarEmptyValueMessage = "  *";
        private string VarInvalidValueMessage = "  El dato no es válido";
        private string VarTooltipMessage = "  Ingrese un valor numérico";

        private string VarMinimum;
        private bool VarPuntoFlotante;

        private RangeValidator VarValidaRangs = new RangeValidator();
        private RequiredFieldValidator validaCon = new RequiredFieldValidator();
        private TextBox texto = new TextBox();
        private TextBoxWatermarkExtender Water = new TextBoxWatermarkExtender();

        #endregion

        #region Enums

        public enum TypeNumber
        {
            Smallint = 5,
            Int = 10,
            TinyInt = 3,
            BigInt = 19,
            Custom = 0,
        }

        #endregion

        #region Crea control

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            string Name = this.UniqueID.Replace("$", "_");
            texto.ID = Name + "DNumber";

            texto.Attributes.Add("Onfocus", "this.style.backgroundColor='#FFFFE0'");
            texto.Attributes.Add("Onblur", "this.style.backgroundColor='#FFFFFF'");
            Controls.Add(texto);

            VarValidaRangs.ID = texto.ID + "_ValidateRang";
            VarValidaRangs.ForeColor = System.Drawing.Color.Red;

            VarValidaRangs.Type = VarPuntoFlotante == false ? ValidationDataType.Integer : ValidationDataType.Double;

            VarValidaRangs.ControlToValidate = texto.ID;
            VarValidaRangs.ErrorMessage = "Error - El valor debe ser númerico";
            VarValidaRangs.ValidationGroup = ValidationGroup;

            if (VarIsRequiered)
            {
                validaCon.ID = this.UniqueID + "_RequiredFieldValidator";
                validaCon.ControlToValidate = texto.ID;
                validaCon.ErrorMessage = VarEmptyValueMessage;
                validaCon.ForeColor = System.Drawing.Color.Red;
                Controls.Add(validaCon);
            }

            if (VarRange)
            {
                try
                {
                    VarValidaRangs.MaximumValue = this.MaximumValue;
                    VarValidaRangs.MinimumValue = this.MinimumValue;
                }
// ReSharper disable once EmptyGeneralCatchClause
                catch { }
            }
            else
            {
                VarValidaRangs.MaximumValue = "99999999";
                VarValidaRangs.MinimumValue = "0";
            }

            Controls.Add(VarValidaRangs);

            if (WaterText.Trim() != "")
            {
                var lit = new Literal
                          {
                              Text = "<style type=\"text/css\">.watermarked {height:18px;width:150px;padding:2px 0 0 2px;border:1px solid #BE" +
                                     "BEBE;background-color:#F0F8FF;color:gray;}</style>"
                          };
                Controls.Add(lit);

                Water.ID = this.UniqueID + "_TextBoxWatermarkExtender";
                Water.WatermarkText = WaterText;
                Water.TargetControlID = texto.ID;
                Water.WatermarkCssClass = "watermarked";
                Controls.Add(Water);
            }

            texto.TextChanged += this.OnTextChanged;

        }

        #endregion

        #region Propiedades

        public bool AceptaPuntoFlotante
        {
            get { return VarPuntoFlotante; }
            set { VarPuntoFlotante = value; }
        }

        public bool IsRange
        {
            get { return VarRange; }
            set { VarRange = value; }
        }

        public string MinimumValue
        {
            get { return VarMinimum; }
            set {
                VarMinimum = Type == TypeNumber.Custom ? value : Data().Item(Type).ValorMinimo.ToString();
            }
        }

        public string MaximumValue
        {
            get { return VarMaximum; }
            set {
                VarMaximum = (Type == TypeNumber.Custom) ? value : Data().Item(Type).ValorMaximo.ToString();
            }
        }

        public bool AutoPostBack
        {
            get { return texto.AutoPostBack; }
            set { texto.AutoPostBack = value; }
        }

        public string WaterText
        {
            get { return Water.WatermarkText; }
            set { Water.WatermarkText = value; }
        }

        ///<summary>
        /// Habilita o Deshabilita el Control
        ///</summary>
        ///<value>true/false</value>
        ///<returns>true/false</returns>
        ///<remarks></remarks>
        public override bool Enabled
        {
            get { return texto.Enabled; }
            set
            {
                texto.Enabled = value;
                texto.BackColor = System.Drawing.Color.FromName((!value) ? "#EAE8E3" : "#FFFFFF");
            }
        }

        ///       <summary>
        ///         Establece el grupo de validacion el
        ///       </summary>
        ///       <value>Nombre Grupo Validacion</value>
        ///        <returns>Nombre Grupo Validacion</returns>
        ///        <remarks></remarks>
        public string ValidationGroup
        {
            get { return validaCon.ValidationGroup; }
            set { validaCon.ValidationGroup = value; }
        }

        ///       <summary>
        ///         Mensaje de ayuda que mostrara el control
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public string TooltipMessage
        {
            get { return VarTooltipMessage; }
            set { VarTooltipMessage = value; }
        }

        ///       <summary>
        ///         Mensaje que se muestra cuando el valor de la fecha no es valido
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public string InvalidValueMessage
        {
            get { return VarInvalidValueMessage; }
            set { VarInvalidValueMessage = value; }
        }

        ///       <summary>
        ///    ''' Mensaje que se muestra cuando el control es Nulo o no tiene ningun valor
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public string EmptyValueMessage
        {
            get { return VarEmptyValueMessage; }
            set { VarEmptyValueMessage = value; }
        }

        ///       <summary>
        ///         Valida que el control Requiera valores o no
        ///       </summary>
        ///       <value>true/false</value>
        ///        <returns>true/false</returns>
        ///        <remarks></remarks>
        public bool IsRequiered
        {
            get { return VarIsRequiered; }
            set { VarIsRequiered = value; }
        }

        ///       <summary>
        ///         Texto que contiene el control
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public string Text
        {
            get
            {
                try
                {
                    return texto.Text.Trim();
                }
                catch
                {
                    return "";
                }
            }
            set { texto.Text = value; }
        }

        ///       <summary>
        ///         Obtiene o establece el alto del control
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public Unit Heigth
        {
            get { return texto.Height; }
            set { texto.Height = value; }
        }

        ///       <summary>
        ///         Obtiene o establece el ancho del control
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public override Unit Width
        {
            get { return texto.Width; }
            set { texto.Width = value; }
        }

        ///       <summary>
        ///         Obtiene o establece color de fondo del control
        ///       </summary>
        ///       <value>System.Drawing.Color</value>
        ///        <returns>System.Drawing.Color</returns>
        ///        <remarks></remarks>
        public System.Drawing.Color BackColor_
        {
            get { return texto.BackColor; }
            set { texto.BackColor = value; }
        }

        ///       <summary>
        ///         Obtiene o establece La clase que dara estilo al control
        ///       </summary>
        ///       <value>CssClass Name</value>
        ///        <returns>CssClass Name</returns>
        ///        <remarks></remarks>
        public string CssClass_
        {
            get { return texto.CssClass; }
            set { texto.CssClass = value; }
        }

        ///       <summary>
        ///         Obtiene o establece si el control es multilinea
        ///       </summary>
        ///       <value>true/false</value>
        ///        <returns>true/false</returns>
        ///        <remarks></remarks>
        public TextBoxMode Multiline
        {
            get { return texto.TextMode; }
            set { texto.TextMode = value; }
        }

        ///       <summary>
        ///         Obtiene o establece el color del mensaje Ajax cuando no se cumplen ciertas condiciones
        ///       </summary>
        ///       <value>Color</value>
        ///        <returns>Color</returns>
        ///        <remarks></remarks>
        public System.Drawing.Color MensajeColor
        {
            get { return validaCon.ForeColor; }
            set { validaCon.ForeColor = value; }
        }

        ///       <summary>
        ///         Longitud maxima del control
        ///       </summary>
        ///       <value>Texto</value>
        ///        <returns>Texto</returns>
        ///        <remarks></remarks>
        public int MaxLength
        {
            get { return texto.MaxLength; }
            set
            {
                if (Type == TypeNumber.Custom)
                {
                    texto.MaxLength = value;
                    // mascara.Mask = "9{" & CStr(value) & "}"
                }
                else
                {
                    texto.MaxLength = (int)Type;
                    // mascara.Mask = "9{" & CStr(Type) & "}"
                }
            }
        }


        ///       <summary>
        ///         Longitud maxima que debe tenetr un control tipificado a partir de BD
        ///       </summary>
        ///       <value>Tipo de dato</value>
        ///        <returns>Tipo de dato</returns>
        ///        <remarks></remarks>

        public TypeNumber TypeDB
        {
            get { return Type; }
            set
            {
                Type = value;

                if (Type != TypeNumber.Custom)
                {
                    this.MaxLength = (int)Type;
                    try
                    {
                        this.MinimumValue = Convert.ToString(Data().Item(Type).ValorMinimo);
                        this.MaximumValue = Convert.ToString(Data().Item(Type).ValorMaximo);
                    }
// ReSharper disable once EmptyGeneralCatchClause
                    catch { }
                    IsRange = true;
                }

            }
        }

        public TipoDatoCollection Data()
        {
            var Lista = new TipoDatoCollection();
            Lista.Add(TypeNumber.Smallint, 5, -32768, 32767);
            Lista.Add(TypeNumber.Int, 10, -2147483468, 2147483467);
            Lista.Add(TypeNumber.TinyInt, 3, 0, 255);
            Lista.Add(TypeNumber.BigInt, 19, -9223372036854775807, 9223372036854775807);
            Lista.Add(TypeNumber.Custom, 0);

            return Lista;
        }

        #endregion

        #region Eventos

        public event EventHandler TextChanged;

        protected virtual void OnTextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        #endregion

        #region Funciones

        public void Clear()
        {
            texto.Text = "";
        }

        #endregion

        public class TipoDato
        {
            public TypeNumber Tipo;
            public int Longitud;
            public Int64 ValorMaximo;
            public Int64 ValorMinimo;

            public TipoDato(TypeNumber nTipo, int nLongitud)
            {
                this.Tipo = nTipo;
                this.Longitud = nLongitud;
            }

            public TipoDato(TypeNumber nTipo, int nLongitud, long nMinimumValue, long nMaximumValue)
            {
                Tipo = nTipo;
                Longitud = nLongitud;
                ValorMaximo = nMaximumValue;
                ValorMinimo = nMinimumValue;
            }
        }

        public class TipoDatoCollection
        {
            public List<TipoDato> Items = new List<TipoDato>();

            public void Add(TypeNumber nTipo, int nLongitud)
            {
                var data = new TipoDato(nTipo, nLongitud);
                Items.Add(data);
            }

            public void Add(TipoDato nTipoDato)
            {
                Items.Add(nTipoDato);
            }

            public void Add(TypeNumber nTipo, int nLongitud, long nMinimumValue, long nMaximumValue)
            {
                var data = new TipoDato(nTipo, nLongitud, nMinimumValue, nMaximumValue);
                Items.Add(data);
            }

            public int Count
            {
                get { return Items.Count; }
            }

            public TipoDato Item(TypeNumber Key)
            {
                TipoDato Item_ = null;
                foreach (TipoDato Item1 in Items)
                {
                    if (Item1.Tipo == Key)
                        Item_ = Item1;
                }

                return Item_;
            }

        }
    }

    public class DDropDownList : CompositeControl
    {
        #region variables

        private MaskedEditValidator valida = new MaskedEditValidator();
        private MaskedEditExtender mascara = new MaskedEditExtender();
        private DropDownList DropDownList = new DropDownList();

        #endregion

        #region Metodos

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            string Name = this.UniqueID.Replace("$", "_");
            DropDownList.ID = Name + "DDrop";
            Controls.Add(DropDownList);
        }

        #endregion
    }
}