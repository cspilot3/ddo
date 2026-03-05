using System;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Controls
{
    public partial class FilterWebUserControl : UserWebControlBase
    {
        #region Declaraciones

        public delegate void FilterClickDelegate(object sender, string filtro);
        public event FilterClickDelegate FilterClick;

        #endregion

        #region Propiedades

        public string Filter
        {
            get
            {
                if (ViewState["FilterText"] == null)
                    ViewState["FilterText"] = "";

                return ViewState["FilterText"].ToString();
            }
            protected set
            {
                ViewState["FilterText"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Config_Page();
        }

        protected void NoneButton_Click(object sender, EventArgs e)
        {
            Filtrar("");
        }

        protected void AllButton_Click(object sender, EventArgs e)
        {
            Filtrar("%");
        }

        protected void ADButton_Click(object sender, EventArgs e)
        {
            Filtrar("[A-D]%");
        }

        protected void EHButton_Click(object sender, EventArgs e)
        {
            Filtrar("[E-H]%");
        }

        protected void ILButton_Click(object sender, EventArgs e)
        {
            Filtrar("[I-L]%");
        }

        protected void MPButton_Click(object sender, EventArgs e)
        {
            Filtrar("[M-P]%");
        }

        protected void QTButton_Click(object sender, EventArgs e)
        {
            Filtrar("[Q-T]%");
        }

        protected void UZButton_Click(object sender, EventArgs e)
        {
            Filtrar("[U-Z]%");
        }

        #endregion

        #region Metodos

        private void Filtrar(string nFilter)
        {
            Filter = nFilter;

            Config_Page();

            if (FilterClick != null) 
                FilterClick(this, nFilter);
        }

        private void Config_Page()
        {
            NoneButton.CssClass = "Filter-Button";
            AllButton.CssClass = "Filter-Button";
            ADButton.CssClass = "Filter-Button";
            EHButton.CssClass = "Filter-Button";
            ILButton.CssClass = "Filter-Button";
            MPButton.CssClass = "Filter-Button";
            QTButton.CssClass = "Filter-Button";
            UZButton.CssClass = "Filter-Button";

            switch (Filter)
            {
                case "%":
                    AllButton.CssClass = "Filter-Button-Selected";
                    break;

                case "[A-D]%":
                    ADButton.CssClass = "Filter-Button-Selected";
                    break;

                case "[E-H]%":
                    EHButton.CssClass = "Filter-Button-Selected";
                    break;

                case "[I-L]%":
                    ILButton.CssClass = "Filter-Button-Selected";
                    break;

                case "[M-P]%":
                    MPButton.CssClass = "Filter-Button-Selected";
                    break;

                case "[Q-T]%":
                    QTButton.CssClass = "Filter-Button-Selected";
                    break;

                case "[U-Z]%":
                    UZButton.CssClass = "Filter-Button-Selected";
                    break;

                default:
                    NoneButton.CssClass = "Filter-Button-Selected";
                    break;
            }
        }

        #endregion
    }
}