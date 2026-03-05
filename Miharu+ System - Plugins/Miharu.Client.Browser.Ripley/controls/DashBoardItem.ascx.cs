using System;

namespace Miharu.Client.Browser.controls
{
    public partial class DashBoardItem : System.Web.UI.UserControl
    {
        #region Declaraciones

        public delegate void ItemClick(DashBoardItem nItem);

        public event ItemClick OnItemClick;

        #endregion

        #region Propiedades

        public string Title
        {
            get { return this.Title_Label.Text; }
            set { this.Title_Label.Text = value; }
        }

        public string Tooltip
        {
            get { return (string)ViewState["Tooltip"]; }
            set { ViewState["Tooltip"] = value; }
        }

        public string OnClientClick
        {
            get { return this.Icon_Image.OnClientClick; }
            set { this.Icon_Image.OnClientClick = value; this.Title_Label.OnClientClick = value; }
        }

        public string ImageUrl
        {
            get { return this.Icon_Image.ImageUrl; }
            set { this.Icon_Image.ImageUrl = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e) { }

        protected void Title_Label_Click(object sender, EventArgs e)
        {
            if (OnItemClick != null) OnItemClick(this);
        }

        #endregion
    }
}