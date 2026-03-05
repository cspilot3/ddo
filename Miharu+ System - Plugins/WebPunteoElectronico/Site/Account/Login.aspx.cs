using System;
using Miharu.Security.Library.SecurityServiceReference;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.WebService;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Account
{
    public partial class Login : FormInitialBase
    {
        #region Declaraciones

        private const string Path_Nodo = "0";

        #endregion

        #region Eventos

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.ShowTitle = false;
            Master.Title = "";

            LoginButton.Click += LoginButton_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserNameTextBox.Focus();

            if (!this.IsPostBack)
                Config_Page();               

            if (LoginButton.Enabled)
                ValidateLockedIP();

            if (Session["__ErrorMessage"] != null)
            {
                this.Master.ShowAlert(Session["__ErrorMessage"].ToString(), MsgBoxIcon.IconWarning);
                Session["__ErrorMessage"] = null;
            }
        }      

        void LoginButton_Click(object sender, EventArgs e)
        {
            IniciarSesion(UserNameTextBox.Text, PasswordTextBox.Text);
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
#if DEBUG
            try
            {
                string FileName = Server.MapPath("~/Site/Account/user.txt");
                if (System.IO.File.Exists(FileName))
                {
                    var UserFile = new System.IO.StreamReader(FileName, System.Text.Encoding.UTF7);

                    UserNameTextBox.Text = UserFile.ReadLine();
                    PasswordTextBox.Text = UserFile.ReadLine();

                    LoginButton.Focus();
                }
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch { }
#endif
        }

        protected override void Load_Data() { }

        private void ValidateLockedIP()
        {
            try
            {
                this.MiharuSession.ClientIPAddress = Program.getIPName();

                var WebService = new SecurityWebService(Program.SecurityWebServiceURL, this.MiharuSession.ClientIPAddress);

                if (WebService.IsIPBloqueada())
                    throw new Exception("La dirección IP: " + MiharuSession.ClientIPAddress  + " se encuentra bloqueada por exceder el número de intentos de conexión fallidos, por favor comuníquese con el administrador del sistema");
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);

                UserNameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
                LoginButton.Enabled = false;
            }
        }

        private void IniciarSesion(string nUserName, string nPassword)
        {
            if (!Validar()) return;

            var WebService = new SecurityWebService(Program.SecurityWebServiceURL, MiharuSession.ClientIPAddress);

            try
            {
                WebService.CrearCanalSeguro();
                WebService.setUser(nUserName, nPassword);

                short idEntidad;
                int idUsuario;
                EnumValidateUser LogonResult;

                if (WebService.ValidateUser(out idEntidad, out idUsuario, out LogonResult))
                {
                    var LocalSession = this.MiharuSession;
                    this.MiharuSession.Parameter["ConnectionStrings"] = Program.getCadenasConexion(ref WebService);

                    WebService.FillSession(ref LocalSession, Program.AssemblyName);

                    switch (LogonResult)
                    {
                        case EnumValidateUser.CAMBIAR_PASSWORD:
                            if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                LocalSession.Usuario.Password = nPassword;
                                    
                                Master.FireParentPostback();
                                MiharuSession.Pagina = new Pagina(typeof(Blankform).FullName, "ChangePassword", "~/Site/Account/ChangePassword.aspx", "0");
                                //Response.Redirect(MiharuSession.Pagina.PageDir);
                                Master.ShowNotification("El usuario debe cambiar la contraseña", "Cambiar contraseña");
                            }
                            else
                            {
                                Master.ShowAlert("El usuario no cuenta con permisos para ingresar a este módulo", MsgBoxIcon.IconWarning);
                            }

                            break;

                        case EnumValidateUser.VALIDO:
                            if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                //LocalSession.Usuario.Password = nPassword;

                                Master.FireParentPostback();
                                MiharuSession.Pagina = new Pagina(typeof(Blankform).FullName, "Blankform", "~/Site/DashBoard.aspx", "0");
                                //Response.Redirect(MiharuSession.Pagina.PageDir);

                                //Registrar Accion
                                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, "", "", "");
                                                                     
                            }
                            else
                            {
                                Master.ShowAlert("El usuario no cuenta con permisos para ingresar a este módulo", MsgBoxIcon.IconWarning);
                            }

                            break;

                        default:
                            Master.ShowAlert("Usuario o contraseña invalida", MsgBoxIcon.IconWarning);
                            break;
                    }
                }
                else
                {
                    switch (LogonResult)
                    {
                        case EnumValidateUser.FALTA_LOGIN:
                            Master.ShowAlert("Debe ingresar el usuario", MsgBoxIcon.IconWarning);
                            break;

                        case EnumValidateUser.INVALIDO_LOGIN:
                            Master.ShowAlert("Usuario o contraseña inválida", MsgBoxIcon.IconWarning);
                            break;

                        case EnumValidateUser.INVALIDO_PASSWORD:
                            Master.ShowAlert("Usuario o contraseña inválida", MsgBoxIcon.IconWarning);
                            break;

                        case EnumValidateUser.INACTIVO:
                            Master.ShowAlert("El usuario no se encuentra activo", MsgBoxIcon.IconWarning);
                            break;

                        default:
                            Master.ShowAlert("Usuario o contraseña inválida", MsgBoxIcon.IconWarning);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconWarning);
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (UserNameTextBox.Text == "")
            {
                Master.ShowAlert("Debe ingresar el usuario", MsgBoxIcon.IconWarning);
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
