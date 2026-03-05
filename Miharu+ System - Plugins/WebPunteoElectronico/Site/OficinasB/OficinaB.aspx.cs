using System;
using DBSecurity;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.OficinasB
{
    public partial class OficinaB : FormBase
    {
        #region Propiedades

        public string NavigationUrl
        {
            get { return Program.OficinasBUrl + "?User=" + MiharuSession.Usuario.Login; }
        }

      
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            this.Master.Title = "Módulo de Oficinas Tipo B";

            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(ConnectionString.Security);

                DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.MiharuSession.Usuario.id);
                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.MiharuSession.Usuario.id, Program.Modulo, 200, this.MiharuSession.ClientIPAddress);                
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }            
        }

        protected override void Load_Data() { }

        #endregion
    }
}