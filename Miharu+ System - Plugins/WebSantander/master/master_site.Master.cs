using System;
using WebSantander.code;

namespace  WebSantander.master
{
    public partial class master_site : master_base
    {
        #region Propiedades
        
        private ScriptBuilder MasterScriptCreator = new ScriptBuilder();
        public ScriptBuilder ScriptCreator = new ScriptBuilder();

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //RefreshLinkButton.Click += new EventHandler(RefreshLinkButton_Click);
        }

        public override void Config_Page()
        {
            if (!this.IsPostBack)
            {
                // Scripts del encabezado
                
                // MIHARU                
                this.MasterScriptCreator.Append("<link href='" + ResolveClientUrl("~/styles/site.css") + "' rel='stylesheet' type='text/css' />\n");
                this.MasterScriptCreator.Append("<link href='" + ResolveClientUrl("~/styles/frame.css") + "' rel='stylesheet' type='text/css' />\n");

                // JQuery
                this.MasterScriptCreator.Append("<link href='" + ResolveClientUrl("~/scripts/jquery/css/smoothness/jquery-ui-1.10.3.custom.min.css") + "' rel='stylesheet' type='text/css' />\n");
                this.MasterScriptCreator.Append("<link href='" + ResolveClientUrl("~/scripts/jquery/css/smoothness/jquery.windows-engine.css") + "' rel='stylesheet' type='text/css' />\n");


                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery-1.9.1.min.js") + "' type='text/javascript'></script>\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery-ui-1.10.3.custom.min.js") + "' type='text/javascript'></script>\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery-es.js") + "' type='text/javascript'></script>\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.windows-engine.js") + "' type='text/javascript'></script>\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/ui.dialogr.custom.js") + "' type='text/javascript'></script>\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.browser.js") + "' type='text/javascript'></script>\n");

                //AutoList
                this.MasterScriptCreator.Append("<link href='" + ResolveClientUrl("~/styles/autoList/autoList.css") + "' rel='stylesheet' type='text/css' />\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.ui.autolist.js") + "' type='text/javascript'></script>\n");
                
                // Menu
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.corner.js") + "' type='text/javascript'></script>\n");

                //Gritter, notificaciones
                this.MasterScriptCreator.Append("<link href='" + ResolveClientUrl("~/scripts/jquery/css/smoothness/jquery.gritter.css") + "' rel='stylesheet' type='text/css' />\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/jquery/js/jquery.gritter.min.js") + "' type='text/javascript'></script>\n");

                // Funciones generales [Debe ser la ultima en cargarse]
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/scripts/Utils.js") + "' type='text/javascript'></script>\n");
                this.MasterScriptCreator.Append("<script src='" + ResolveClientUrl("~/master/master_site.js") + "' type='text/javascript'></script>\n");

                if (this.SessionManager.UserLogged)
                {
                    this.MasterScriptCreator.Append("<script type='text/javascript'>Site.SessionScan = " +
                                                    Program.SessionScan + "; Site.SessionTokenStart();</script>");
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.IsPostBack)
                this.HeadScriptsLiteral.Text = this.MasterScriptCreator.ToString() + this.ScriptCreator;

            base.OnPreRender(e);
        }
        
        #endregion

        #region Metodos

        public void CerrarSesion()
        {
            Session.Abandon();
            Response.Redirect(Navigation.site.account.login);
        }

        #endregion        
    }
}