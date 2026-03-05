using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPunteoElectronico.Controls
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
            get { return Title_Label.Text; }
            set { Title_Label.Text = value; }
        }

        public string Tooltip
        {
            get { return (string)ViewState["Tooltip"]; }
            set { ViewState["Tooltip"] = value; }
        }

        public string OnClientClick
        {
            get { return Icon_Image.OnClientClick; }
            set { Icon_Image.OnClientClick = value; Title_Label.OnClientClick = value; }
        }

        public string ImageUrl
        {
            get { return Icon_Image.ImageUrl; }
            set { Icon_Image.ImageUrl = value; }
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