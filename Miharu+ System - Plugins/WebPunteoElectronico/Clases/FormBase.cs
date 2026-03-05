using System;
using Miharu.Security.Library.Session;
using WebPunteoElectronico.Master;

namespace WebPunteoElectronico.Clases
{
    public abstract class FormBase : GenericBase
    {        
        #region Propiedades

        public new MasterForm Master
        {
            get { return (MasterForm)base.Master; }
        }

        public EnumModo PageMode
        {
            get { return (EnumModo)this.MiharuSession.Pagina.Parameter["PageMode"]; }
            set { this.MiharuSession.Pagina.Parameter["PageMode"] = value; }
        }

        #endregion

        #region Eventos

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.MiharuSession.UserLogged)
            {
                Session["__ErrorMessage"] = "La sesión ha caducado, por favor vuelva a ingresar al sistema";
                Response.Redirect(Navigation.Site.Account.Login);
            }
            // Enlazar pagina maestra            
        }

        #endregion

        #region Funciones

        protected bool ValidarNavegacion(string nPageName, string nPathPermiso)
        {
            if (this.MiharuSession == null || Session["Session"] == null || this.MiharuSession.Pagina == null)
            {
                Master.ShowAlert("La sesión ha caducado, por favor salga y vuelva a ingresar al aplicativo", MsgBoxIcon.IconError);
                this.CreateSession();
                this.MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Blankform).FullName, "Blankform", "~/Wellcome.aspx", "0");
                Master.FireParentPostback(); 
            }
            else if (this.MiharuSession.Pagina.Name != nPageName)
            {
                Master.ShowAlert("El usuario no esta autorizado para ingresar a esta sección", MsgBoxIcon.IconError);
                MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Blankform).FullName, "Blankform", "~/Site/DashBoard.aspx", "0");
                Master.FireParentPostback(); 
            }
            else if (nPathPermiso == "0" || ValidarPermisos(nPathPermiso))
            {
                return true;
            }
            else
            {
                Master.ShowAlert("El usuario no esta autorizado para ingresar a esta sección", MsgBoxIcon.IconError);
                MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Blankform).FullName, "Blankform", "~/Site/DashBoard.aspx", "0");
                Master.FireParentPostback(); 
            }

            return false;
        }

        protected bool ValidarHorario()
        {
            //Se desactiva el código de validación por solicitud del cliente, se deja por si acaso lo solicitan de nuevo. Si se activa de nuevo
            //se debe borrar la siguiente línea:
            return true;

            // Conexión al servicio web de configuración
            //ConfigWebService ServicioConfig = new ConfigWebService(Program.WebPunteoElectronicoConfigWebServiceURL, MiharuSession.Usuario.id);
            //var usuarios = ServicioConfig.Usuario_get(this.MiharuSession.Usuario.id);
            //short idCalendario = 0;
            //bool esValido = false;

            //if (usuarios == null)
            //{                
            //    MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Blankform).FullName, "Blankform", "~/Site/DashBoard.aspx", "0");
            //    Master.ShowAlert("El usuario no tiene asociado un calendario para validar los horarios.", MsgBoxIcon.IconError);
            //    Master.FireParentPostback(); 
            //}
            //else
            //{
            //    idCalendario = usuarios.First().fk_calendario;
            //    SecurityWebService ServicioSecurity = new SecurityWebService(Program.SecurityWebServiceURL, this.MiharuSession.ClientIPAddress);
            //    ServicioSecurity.CrearCanalSeguro();

            //    esValido = ServicioSecurity.Horario_get(this.EntidadTrabajo, idCalendario);

            //    if (esValido)
            //    {
            //        return true;
            //    }
            //    else
            //    {                    
            //        MiharuSession.Pagina = new Pagina(typeof(WebPunteoElectronico.Site.Blankform).FullName, "Blankform", "~/Site/DashBoard.aspx", "0");
            //        Master.ShowAlert("La operación no está disponible según el horario configurado para el usuario.", MsgBoxIcon.IconError);
            //        Master.FireParentPostback();                    
            //    }
            //}

            //return false;
        }

        #endregion
    }
}