using System;
using WebSantander.code;
using Miharu.Security.Library.SecurityServiceReference;

namespace  WebSantander.site.account
{
    public partial class login : page_site
    {
        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.LoginLinkButton.Click += LoginLinkButton_Click;
            this.ForgottenLinkButton.Visible = Program.ForgottenPassword;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserNameTextBox.Focus();

            if (!this.IsPostBack)
                Config_Page();

            if (LoginLinkButton.Enabled)
                Validate_Locked_IP();
        }

        void LoginLinkButton_Click(object sender, EventArgs e)
        {
            IniciarSesion(UserNameTextBox.Text, PasswordTextBox.Text);
        }

        #endregion

        #region Metodos

        public override void Config_Page()
        {
            if (IsPostBack) return;
            
            Session.Clear();
#if DEBUG
            var fileName = Server.MapPath("~/site/account/user.txt");
            if (System.IO.File.Exists(fileName))
            {
                var userFile = new System.IO.StreamReader(fileName, System.Text.Encoding.UTF7);

                UserNameTextBox.Text = userFile.ReadLine();
                PasswordTextBox.Text = userFile.ReadLine();
            }
#endif
            LoginLinkButton.Focus();            
        }

        private void Validate_Locked_IP()
        {
            try
            {
                this.MiharuSession.ClientIPAddress = Program.getIPName();                
                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.MiharuSession.ClientIPAddress);

                if (WebService.IsIPBloqueada())
                    throw new Exception("La dirección IP: " + this.MiharuSession.ClientIPAddress + " se encuentra bloqueada por exceder el número de intentos de conexión fallidos, por favor comuníquese con el administrador del sistema");
            }
            catch (Exception ex)
            {
                UserNameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
                LoginLinkButton.Enabled = false;

                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
        }

        private void IniciarSesion(string nUserName, string nPassword)
        {
            if (Validar())
            {                
                var WebService = new Miharu.Security.Library.WebService.SecurityWebService(Program.SecurityWebServiceURL, this.MiharuSession.ClientIPAddress);

                try
                {
                    WebService.CrearCanalSeguro();
                    WebService.setUser(nUserName, nPassword);

                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    if (WebService.ValidateUser(out idEntidad, out idUsuario, out LogonResult))
                    {
                        //var LocalSession = this.MiharuSession;
                        var LocalSession = this.MiharuSession;
                        this.MiharuSession.Parameter["ConnectionStrings"] = Program.getCadenasConexion(ref WebService);
                        //this.ConnectionStrings = Program.getCadenasConexion(ref WebService);
                        //this.MiharuSession.Parameter["ConnectionStrings"] = ConnectionStrings;

                        WebService.FillSession(ref LocalSession, Program.AssemblyName);

                        switch (LogonResult)
                        {
                            case EnumValidateUser.CAMBIAR_PASSWORD:
                                //if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                                if(this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                                {
                                    LocalSession.Usuario.Password = nPassword;

                                    this.MiharuSession.Parameter[consts.BackPage] = Navigation.site.account.login;
                                    //this.MiharuSession.Parameter[consts.BackPage] = Navigation.site.account.login;
                                    Response.Redirect(Navigation.site.account.change_password);
                                }
                                else
                                {
                                    ScriptHelper.Site.ShowAlert(this,
                                                                "El usuario no tiene acceso a ninguna funcionalidad de la aplicación",
                                                                MsgBoxIcon.IconWarning);
                                }

                                break;

                            case EnumValidateUser.VALIDO:
                                //if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                                if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                                    Response.Redirect(Navigation.site.account.connect);
                                else
                                    ScriptHelper.Site.ShowAlert(this, "El usuario no tiene acceso a ninguna funcionalidad de la aplicación", MsgBoxIcon.IconWarning);

                                break;

                            default:
                                PasswordTextBox.Focus();
                                ScriptHelper.Site.ShowAlert(this, "Usuario o contraseña inválida",
                                                            MsgBoxIcon.IconWarning);
                                break;
                        }
                    }
                    else
                    {
                        switch (LogonResult)
                        {
                            case EnumValidateUser.FALTA_LOGIN:
                                UserNameTextBox.Focus();
                            ScriptHelper.Site.ShowAlert(this, "Debe ingresar el usuario", MsgBoxIcon.IconWarning);
                            break;

                            case EnumValidateUser.INVALIDO_LOGIN:
                                PasswordTextBox.Focus();
                            ScriptHelper.Site.ShowAlert(this, "Usuario o contraseña inválida", MsgBoxIcon.IconWarning);
                            break;

                            case EnumValidateUser.INVALIDO_PASSWORD:
                                PasswordTextBox.Focus();
                            ScriptHelper.Site.ShowAlert(this, "Usuario o contraseña inválida", MsgBoxIcon.IconWarning);
                            break;

                            case EnumValidateUser.INACTIVO:
                                UserNameTextBox.Focus();
                            ScriptHelper.Site.ShowAlert(this, "El usuario no se encuentra activo", MsgBoxIcon.IconWarning);
                            break;

                            default:
                                PasswordTextBox.Focus();
                                ScriptHelper.Site.ShowAlert(this, "Usuario o contraseña inválida",
                                                            MsgBoxIcon.IconWarning);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
                }
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (UserNameTextBox.Text == "")
            {
                ScriptHelper.Site.ShowAlert(this, "Debe ingresar el usuario", MsgBoxIcon.IconWarning);
                UserNameTextBox.Focus();
            }
            else
            {
                return true;
            }

            return false;
        }

        #endregion        
    }
}