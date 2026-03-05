using System;
using System.Text;
using System.Web.UI.HtmlControls;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Master
{
    public partial class MasterPage : MasterBase
    {
        #region Propiedades

        protected override HtmlInputHidden EndRequestActionObject
        {
            get { return EndRequestAction; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Scripts del encabezado
                var ScriptCreator = new StringBuilder("");

                // JQuery
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-1.8.0.min.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-ui-1.8.23.custom.min.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-es.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery-ui-1.8.23.custom.css") + "' rel='stylesheet' type='text/css' />\n");

                // Windows
                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery.windows-engine.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery.windows-engine.js") + "' type='text/javascript'></script>\n");

                //Gritter, notificaciones
                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery.gritter.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery.gritter.min.js") + "' type='text/javascript'></script>\n");

                // Funciones generales [Debe ser la ultima en cargarse]
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Utils.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Master/MasterPage.js") + "' type='text/javascript'></script>\n");

                HeadScriptsLiteral.Text = ScriptCreator.ToString();
            }
            else
            {
                EndRequestAction.Value = "";
            }
        }

        protected void RefreshLinkButton_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Metodos

        public void CerrarSesion()
        {
            Session.Abandon();
            Response.Redirect("~/Main.aspx");
        }

        #endregion
    }
}