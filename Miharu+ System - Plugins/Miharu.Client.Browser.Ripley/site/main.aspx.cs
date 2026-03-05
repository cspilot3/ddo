using System;
using Miharu.Client.Browser.code;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.WebService;

namespace  Miharu.Client.Browser.site
{
    public partial class main : page_site
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Master.ScriptCreator.Append("<script type='text/javascript'>");
                this.Master.ScriptCreator.Append("</script>");
            }
        }

        public void ChangePassword(ScriptBuilder nHtml)
        {
            var oldPassword = Request["OldPassword"];
            var newPassword = Request["NewPassword"];

                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(
                    Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);

            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(this.SessionManager.Usuario.Login, oldPassword);
                var nMsgError = "";

                var Respuesta = WebService.ChangePassword(this.SessionManager.Usuario.Login, newPassword,
                                                          out nMsgError);

                switch (Respuesta)
                {
                    case EnumValidateUser.INVALIDO_PASSWORD:
                        nHtml.Append("Site.Valido = false;");
                        nHtml.Append("Site.Mensaje = 'Contraseña no válida';");                        
                        break;

                    case EnumValidateUser.ERROR_PASSWORD:
                        nHtml.Append("Site.Valido = false;");
                        nHtml.Append("Site.Mensaje = '" + nMsgError + "';");                        
                        break;

                    case EnumValidateUser.VALIDO:
                        nHtml.Append("Site.Valido = true;");
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
        }

        public void CloseSession(ScriptBuilder nHtml)
        {
            try
            {
                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);
                WebService.DisconnectAppSession(Program.idModulo, SessionManager.Usuario.id);
            }
            catch
            {
            }
        }

        public void ValidateToken(ScriptBuilder nHtml)
        {
            try
            {
                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);                
                var sesion = WebService.RefreshAppSession(Program.idModulo, SessionManager.Usuario.id);

                if (sesion.Token != SessionToken.Token)
                {
                    nHtml.Append("Site.TokenValid = false;");
                    nHtml.Append("Site.TokenIP = '" + sesion.Client_IP + "';");
                }
                else
                {
                    nHtml.Append("Site.TokenValid = true;");
                }
            }
            catch
            {
            }
        }

        public void GetDate(ScriptBuilder nHtml)
        {
            var now = DateTime.Now;
            nHtml.Append("AppInfo.currentDate = new Date(" + now.Year + "," + (now.Month - 1) + "," + now.Day + "," + now.Hour + "," + now.Minute + "," + now.Second + ");");
        }        

        #endregion

        #region Metodos

        public override void Config_Page()
        {
            ConfigUserPanel();
        }

        private void ConfigUserPanel()
        {
            if (this.SessionManager != null && this.SessionManager.UserLogged)
            {
                // Usuario
                this.EntidadLabel.Text = this.SessionManager.Entidad.Nombre;
                this.UserLabel.Text = this.SessionManager.Usuario.Nombres + @" " + this.SessionManager.Usuario.Apellidos;
                this.LoginLabel.Text = this.SessionManager.Usuario.Login;
                
                // Perfiles
                var securityWebService = new SecurityWebService(Program.SecurityWebServiceURL, this.SessionManager.ClientIPAddress);
                securityWebService.CrearCanalSeguro();

                var Perfiles = securityWebService.Usuario_Perfil_get(this.SessionManager.Usuario.id);
                if (Perfiles != null)
                {
                    if (Perfiles.Length == 1)
                    {
                        this.PerfilLabel.Text = Perfiles[0].Nombre_Perfil;
                        this.PerfilLabel.ToolTip = Perfiles[0].Nombre_Perfil;
                    }
                    else
                    {
                        this.PerfilLabel.Text = "[Múltiples perfiles]";
                        this.PerfilLabel.ToolTip = "[Perfiles asignados]";

                        foreach (var Perfil in Perfiles)
                        {
                            this.PerfilLabel.ToolTip += "\n- " + Perfil.Nombre_Perfil;
                        }
                    }
                }
                else
                {
                    this.PerfilLabel.Text = "-";
                }

                // Roles
                var Roles = securityWebService.Rol_get(this.SessionManager.Usuario.id);
                if (Roles != null)
                {
                    if (Roles.Length == 1)
                    {
                        this.RolLabel.Text = Roles[0].Nombre_Rol;
                        this.RolLabel.ToolTip = Roles[0].Nombre_Rol;
                    }
                    else
                    {
                        this.RolLabel.Text = "[Múltiples roles]";
                        this.RolLabel.ToolTip = "[Roles asignados]";

                        foreach (var rol in Roles)
                        {
                            this.RolLabel.ToolTip += "\n- " + rol.Nombre_Rol;
                        }
                    }
                }
                else
                {
                    this.RolLabel.Text = "-";
                }

                // Conexión
                this.IPNameLabel.Text = this.SessionManager.ClientIPAddress;
                this.LastConnectionLabel.Text = securityWebService.UltimoAcceso(this.SessionManager.Usuario.id, Program.idModulo).ToString("yyyy/MM/dd HH:mm:ss").Replace('-', '/');
            }
        }

        #endregion
    }
}