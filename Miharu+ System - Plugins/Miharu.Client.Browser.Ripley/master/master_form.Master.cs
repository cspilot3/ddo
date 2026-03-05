using System;
using System.Text;
using Miharu.Client.Browser.code;

namespace  Miharu.Client.Browser.master
{
    public partial class master_form : master_base
    {
        #region Declaraciones

        // Scripts del encabezado
        public StringBuilder ScriptLinkBag = new StringBuilder("\n");

        #endregion

        #region Propiedades

        public string Title
        {
            get { return this.TitleLabel.Text; }
            set { this.TitleLabel.Text = value; }
        }

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);            
        }

        public override void Config_Page()
        {
            if (!this.IsPostBack)
            {
                // MIHARU                
                ScriptLinkBag.Append("<link href='" + ResolveClientUrl("~/styles/site.css") + "' rel='stylesheet' type='text/css' />\n");

                // JQuery
                ScriptLinkBag.Append("<link href='" + ResolveClientUrl("~/scripts/jquery/css/smoothness/jquery-ui-1.10.3.custom.min.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptLinkBag.Append("<link href='" + ResolveClientUrl("~/scripts/jquery/css/smoothness/jquery.windows-engine.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery-1.9.1.min.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery-ui-1.10.3.custom.min.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery-es.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.windows-engine.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.browser.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.corner.js") + "' type='text/javascript'></script>\n");

                //AutoList
                ScriptLinkBag.Append("<link href='" + ResolveClientUrl("~/styles/autoList/autoList.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.ui.autolist.js") + "' type='text/javascript'></script>\n");
                //ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.ui.autocomplete.custom.js") + "' type='text/javascript'></script>\n");

                //Gritter, notificaciones
                ScriptLinkBag.Append("<link href='" + ResolveClientUrl("~/scripts/jquery/css/smoothness/jquery.gritter.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.gritter.min.js") + "' type='text/javascript'></script>\n");

                // Funciones generales [Debe ser la ultima en cargarse]
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/scripts/Utils.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script src='" + ResolveClientUrl("~/master/master_form.js") + "' type='text/javascript'></script>\n");
                ScriptLinkBag.Append("<script type='text/javascript'>Global.IsLogged = " + PageGeneric.SessionManager.UserLogged.ToString().ToLower() + "; Global.SessionTimeOut = " + Program.SessionTimeOut + "; Site.SessionScan = " + Program.SessionScan + ";</script>");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            HeadScriptsLiteral.Text = ScriptLinkBag.ToString();
            base.OnPreRender(e);
        }

        #endregion

        #region Metodos

        #endregion
    }
}