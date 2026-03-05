using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace WebPunteoElectronico.Controls
{
    public class CoreDateBox : TextBox
    {
        private MaskedEditExtender oMEE;


        private string _DateFormat = "yyyy-MM-dd";

        public CoreDateBox()
        {
            IsRequired = false;
        }

        public bool IsRequired { get; set; }

        public string DateFormat
        {
            get { return _DateFormat; }
            set { _DateFormat = value; }
        }

        public MaskedEditExtender MyMaskedEditExtender
        {
            get { return oMEE; }
        }

        public string MaskedEditExtenderID
        {
            get { return "MEE_" + this.ID; }
        }

        public string MaskedEditValidatorID
        {
            get { return "Validator_" + this.ID; }
        }

        [Bindable(true), DefaultValue("*")]
        string ErrorMessage
        {
            get
            {
                return Convert.ToString(ViewState[this.ID + "ErrorMessage"]);
            }
            set
            {
                ViewState[this.ID + "ErrorMessage"] = value;
            }
        }


        protected override void OnInit(EventArgs e)
        {
            this.BuildControls();
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write("&nbsp;");
            if (!DesignMode)
            {
                oMEE.RenderControl(writer);
            }
        }

        protected void BuildControls()
        {
            oMEE = new MaskedEditExtender {ID = MaskedEditExtenderID, MaskType = MaskedEditType.Date, Mask = "9999/99/99", AutoComplete = false, CultureName = "en-CA", MessageValidatorTip = true, TargetControlID = this.ID};
            this.Controls.Add(oMEE);
        }

        public string ToDateString()
        {
            if (Text.Trim() == "")
                return "";
            
            try
            {
                var d = Convert.ToDateTime(Text);
                return d.ToString(DateFormat);
            }
            catch
            {
                return "";
            }
        }
    }
}