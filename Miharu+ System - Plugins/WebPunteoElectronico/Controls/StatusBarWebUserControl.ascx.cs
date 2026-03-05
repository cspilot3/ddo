using System;
using WebPunteoElectronico.Clases;
using Miharu.Security.Library.WebService;

namespace WebPunteoElectronico.Controls
{
    public partial class StatusBarWebUserControl : UserWebControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigStatusBar();
        }

        private void ConfigStatusBar()
        {
            if (this.Page.MiharuSession.UserLogged)
            {
                this.DefaultPanel.Visible = false;
                this.LoggedPanel.Visible = true;

                this.EntidadLabel.Text = this.Page.MiharuSession.Entidad.Nombre;

                this.UsuarioLabel.Text = this.Page.MiharuSession.Usuario.Login;
                this.FechaLabel.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace('-', '/');

                var dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(this.Page.ConnectionString.Security);

                try
                {
                    dbmSecurity.Connection_Open(this.Page.MiharuSession.Usuario.id);
                    this.UltimoIngresoLabel.Text = dbmSecurity.SchemaSecurity.PA_Consulta_Ultimo_Acceso.DBExecute(this.Page.MiharuSession.Usuario.id, Program.Modulo).ToString("yyyy/MM/dd HH:mm:ss").Replace('-', '/');
                }
                catch (Exception)
                {
                    this.UltimoIngresoLabel.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace('-', '/');
                }
                finally
                {
                    dbmSecurity.Connection_Close();
                }


                this.IPLabel.Text = this.Page.MiharuSession.ClientIPAddress;

                var securityWebService = new SecurityWebService(Program.SecurityWebServiceURL, this.Page.MiharuSession.ClientIPAddress);
                securityWebService.CrearCanalSeguro();

                var Perfiles = securityWebService.Usuario_Perfil_get(this.Page.MiharuSession.Usuario.id);

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
            }
            else
            {
                DefaultPanel.Visible = true;
                LoggedPanel.Visible = false;
            }
        }
    }
}