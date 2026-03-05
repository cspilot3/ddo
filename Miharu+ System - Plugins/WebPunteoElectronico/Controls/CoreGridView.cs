using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebPunteoElectronico.Controls
{
    public class CoreGridView : GridView
    {

        #region  Declaraciones

        public enum EnumClickAction
        {
            OnClickNoEvents,
            OnClickSelectedPostBack,
            OnDblClickSelectedPostBack,
        }

        private HtmlGenericControl DivContainerControl;

        private HtmlInputHidden PreSelectedIndexControl;

        private HtmlInputHidden SelectedIndexControl;

        private HtmlInputHidden IsConfigureRequiredControl;

        private HtmlInputHidden IsInitializeRequiredControl;

        private Literal ScriptControl;

        private bool _EnableSort = true;

        private EnumClickAction _ClickAction = EnumClickAction.OnDblClickSelectedPostBack;

        private string _PreSelectedStyleCssClass = "";

        private string _OnBeginPreSelect = "";

        private string _OnBeginSelect = "";

        private string _OnEndPreSelect = "";

        private string _OnEndSelect = "";

        #endregion

        #region  Propiedades

        public int GridNum { get; set; }

        public bool EnableSort
        {
            get { return _EnableSort; }
            set { _EnableSort = value; }
        }

        public EnumClickAction ClickAction
        {
            get { return _ClickAction; }
            set { _ClickAction = value; }
        }

        private string ClientGridId
        {
            get { return "Grd_" + this.ID + GridNum; }
        }

        private string DivContainerClientControlId
        {
            get { return "Grd_Div_" + this.ID + GridNum; }
        }

        private string PreSelectedIndexClientControlId
        {
            get { return "Grd_PreSelected_" + this.ID + GridNum; }
        }

        private string SelectedIndexClientControlId
        {
            get { return "Grd_SelectedIndex_" + this.ID + GridNum; }
        }

        private string IsConfigureRequiredClientControlId
        {
            get { return "Grd_IsConfigureRequired_" + this.ID + GridNum; }
        }

        private string IsInitializeRequiredClientControlId
        {
            get { return "Grd_IsInitializeRequired_" + this.ID + GridNum; }
        }

        public string PreSelectedStyleCssClass
        {
            get { return _PreSelectedStyleCssClass; }
            set { _PreSelectedStyleCssClass = value; }

        }

        public string OnBeginPreSelect
        {
            get { return _OnBeginPreSelect; }
            set { _OnBeginPreSelect = value; }
        }

        public string OnBeginSelect
        {
            get { return _OnBeginSelect; }
            set { _OnBeginSelect = value; }
        }

        public string OnEndPreSelect
        {
            get { return _OnEndPreSelect; }
            set { _OnEndPreSelect = value; }
        }

        public string OnEndSelect
        {
            get { return _OnEndSelect; }
            set { _OnEndSelect = value; }
        }

        public override int SelectedIndex
        {
            get { return base.SelectedIndex; }
            set
            {
                base.SelectedIndex = value;
                _IsConfigureRequired = true;
            }
        }

        public int PreSelectedIndex
        {
            get { return _PreSelectedIndex; }
            set
            {
                _PreSelectedIndex = value;
                _IsConfigureRequired = true;
            }
        }

        private int _PreSelectedIndex = -1;

        private bool _IsConfigureRequired;

        private bool _IsInitializeRequired;

        #endregion

        #region  Metodos

        public void SelectPreselectedIndex()
        {
            if (DesignMode) return;
            if (int.Parse(PreSelectedIndexControl.Value) <= -1) return;

            SelectedIndex = int.Parse(PreSelectedIndexControl.Value);
            OnSelectedIndexChanged(null);
        }

        public CoreGridView()
        {
            base.CssClass = "yui-datatable-theme";
            base.RowStyle.CssClass = "nor-data-row";
            base.AlternatingRowStyle.CssClass = "alt-data-row";
            base.EditRowStyle.CssClass = "row-edit";
            base.SelectedRowStyle.CssClass = "row-Select";
            base.PagerStyle.CssClass = "pager-stl";
            PreSelectedStyleCssClass = "row-PreSelect";
        }

        protected override void LoadControlState(object savedState)
        {
            base.LoadControlState(savedState);

            _PreSelectedIndex = int.Parse(GetControlStateValue(PreSelectedIndexClientControlId, "-1"));
            SelectedIndex = int.Parse(GetControlStateValue(SelectedIndexClientControlId, "-1"));
            _IsConfigureRequired = bool.Parse(GetControlStateValue(IsConfigureRequiredClientControlId, "false"));
            _IsInitializeRequired = bool.Parse(GetControlStateValue(IsInitializeRequiredClientControlId, "false"));

        }

        protected override object SaveControlState()
        {
            try
            {
                PreSelectedIndexControl.Value = (_PreSelectedIndex).ToString();
                SelectedIndexControl.Value = (SelectedIndex).ToString();
                IsConfigureRequiredControl.Value = (_IsConfigureRequired).ToString();
                IsInitializeRequiredControl.Value = (_IsInitializeRequired).ToString();
                ScriptControl.Text = GetClientScript();
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }

            return base.SaveControlState();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.CreateHidenControls();
            this.RegisterHidenControls();
        }

        protected override void OnPreRender(EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(typeof (string), ClientGridId + "_Initialize", "Sys.Application.add_load( function(sender, args) {{" + ClientGridId + ".Configure(sender, args)" + "}});", true);
            base.OnPreRender(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (DesignMode) return;
            try
            {
                base.Render(writer);
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
            try
            {
                DivContainerControl.RenderControl(writer);
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }


        private string GetControlStateValue(string ControlID, string DefaultValue = "")
        {
            //If (Page Is Nothing Or Page.Request Is Nothing Or Page.Request.Form Is Nothing) Then Return DefaultValue
            for (var i = 0; i < Page.Request.Form.Keys.Count; i++)
            {
                if ((Page.Request.Form.Keys[i] == null)) continue;
// ReSharper disable once StringIndexOfIsCultureSpecific.2
                if ((Page.Request.Form.Keys[i].IndexOf(ControlID, 0) > 0))
                    return Page.Request.Form[i].Split(',')[0];
            }
            return DefaultValue;
        }

        protected void CreateHidenControls()
        {
            try
            {
                if (DesignMode) return;
                DivContainerControl = new HtmlGenericControl("div");
                PreSelectedIndexControl = new HtmlInputHidden();
                SelectedIndexControl = new HtmlInputHidden();
                IsConfigureRequiredControl = new HtmlInputHidden();
                IsInitializeRequiredControl = new HtmlInputHidden();
                ScriptControl = new Literal();
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }


        protected void RegisterHidenControls()
        {
            try
            {
                if (!DesignMode)
                {
                    base.Controls.Add(DivContainerControl);

                    PreSelectedIndexControl.ID = PreSelectedIndexClientControlId;
                    PreSelectedIndexControl.Value = (_PreSelectedIndex).ToString();
                    DivContainerControl.Controls.Add(PreSelectedIndexControl);

                    SelectedIndexControl.ID = SelectedIndexClientControlId;
                    SelectedIndexControl.Value = (SelectedIndex).ToString();
                    DivContainerControl.Controls.Add(SelectedIndexControl);

                    IsConfigureRequiredControl.ID = IsConfigureRequiredClientControlId;
                    IsConfigureRequiredControl.Value = (_IsConfigureRequired).ToString();
                    DivContainerControl.Controls.Add(IsConfigureRequiredControl);

                    IsInitializeRequiredControl.ID = IsInitializeRequiredClientControlId;
                    IsInitializeRequiredControl.Value = (_IsInitializeRequired).ToString();
                    DivContainerControl.Controls.Add(IsInitializeRequiredControl);

                    DivContainerControl.Controls.Add(ScriptControl);
                }
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }


        public override object DataSource
        {
            get { return base.DataSource; }
            set
            {
                base.DataSource = value;
                _IsInitializeRequired = true;
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if ((DesignMode)) return;
            if ((_EnableSort))
            {
                if ((e.Row.RowType == DataControlRowType.Header))
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes["onclick"] = ClientGridId + ".SortColumn( " + i + " );";
                    }
                }
            }

            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                e.Row.Attributes["onclick"] = ClientGridId + ".PreSelectedIndexChanged" + "( this, " + e.Row.RowIndex + ")";
                e.Row.Attributes["ondblclick"] = ClientGridId + ".SelectedIndexChanged" + "( this, " + e.Row.RowIndex + ")";
                e.Row.Attributes["ServerIndex"] = Convert.ToString(e.Row.RowIndex);
            }

            base.OnRowDataBound(e);
        }

        public string GetClientScript()
        {
            var writer = new StringBuilder("");

            var isPreSelectedDoPostBack = (_ClickAction == EnumClickAction.OnClickSelectedPostBack ? "true" : "false");
            var isSelectedDoPostBack = (_ClickAction == EnumClickAction.OnDblClickSelectedPostBack ? "true" : "false");

            writer.Append("<script type='text/javascript'>" + "\r\n");
            writer.Append("    var " + ClientGridId + " = ERGridView.CreateNew('" + this.ClientID + "' , '" + this.UniqueID + "' , " + GridNum + " , '" + PreSelectedIndexControl.ClientID + "', '" + SelectedIndexControl.ClientID + "', '" + IsConfigureRequiredControl.ClientID + "' , '" + IsInitializeRequiredControl.ClientID + "' , '" + RowStyle.CssClass + "' , '" + AlternatingRowStyle.CssClass + "' , '" + PreSelectedStyleCssClass + "' , '" + SelectedRowStyle.CssClass + "' , " + isPreSelectedDoPostBack + " , " + isSelectedDoPostBack + " );" + "\r\n");

            if (_OnBeginPreSelect != "")
            {
                writer.Append(ClientGridId + ".OnBeginPreSelect = " + _OnBeginPreSelect + ";" + "\r\n");
            }
            if (_OnBeginSelect != "")
            {
                writer.Append(ClientGridId + ".OnBeginSelect = " + _OnBeginSelect + ";" + "\r\n");
            }
            if (_OnEndPreSelect != "")
            {
                writer.Append(ClientGridId + ".OnEndPreSelect = " + _OnEndPreSelect + ";" + "\r\n");
            }
            if (_OnEndSelect != "")
            {
                writer.Append(ClientGridId + ".OnEndSelect = " + _OnEndSelect + ";" + "\r\n");
            }

            writer.Append("</script>" + "\r\n");
            return writer.ToString();
        }

        #endregion
    }
}