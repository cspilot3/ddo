using System;
using Slyg.Web.Controls;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Ajustes
{
    public partial class AjustesAjaxController : FormBase
    {
        #region Declaraciones

        public Miharu.Security.Library.Session.Sesion _MiharuSession;

        #endregion

        #region Propiedades

        public new Master.MasterConfig Master
        {
            get { return (Master.MasterConfig)base.OriginalMaster; }
        }

        public DBAgrario.SchemaConfig.TBL_Ajuste_ObservacionDataTable pTablaObservacion
        {
            get { return (DBAgrario.SchemaConfig.TBL_Ajuste_ObservacionDataTable)this.MiharuSession.Pagina.Parameter["pTablaObservacion"]; }
            set { this.MiharuSession.Pagina.Parameter["pTablaObservacion"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            _MiharuSession = (Miharu.Security.Library.Session.Sesion)this.MiharuSession.Parameter["Sesion"];

            var Metodo = Request["Metodo"];
            if (Metodo == null) return;
            switch (Metodo)
            {
                case "GuardarObservacion": GuardarObservacion();
                    break;
            }
        }

        #endregion

        #region Metodos

        protected override void Config_Page() { }
        protected override void Load_Data() { }

        private void GuardarObservacion()
        {
            var sc = new ScriptBuilder();
            
            var Accion = Request["Accion"];
            var Cod = sc.DecodeScript(Request["Cod"]);
            var Obs = sc.DecodeScript(Request["Obs"]);

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);

                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);
                dbmBanagrario.Transaction_Begin();

                var observacion = new DBAgrario.SchemaConfig.TBL_Ajuste_ObservacionType();

                switch (Accion)
                {
                    case "Nueva":
                        observacion.id_Ajuste_Observacion = dbmBanagrario.SchemaConfig.TBL_Ajuste_Observacion.DBNextId();
                        observacion.Observacion_Ajuste = Obs;
                        observacion.Activa = true;
                        dbmBanagrario.SchemaConfig.TBL_Ajuste_Observacion.DBInsert(observacion);
                        Cod = observacion.id_Ajuste_Observacion.ToString();
                        break;
                    case "Editar":
                        observacion.Observacion_Ajuste = Obs;
                        observacion.Activa = true;
                        dbmBanagrario.SchemaConfig.TBL_Ajuste_Observacion.DBUpdate(observacion, Convert.ToInt32(Cod));
                        break;
                    default:
                        observacion.Activa = false;
                        dbmBanagrario.SchemaConfig.TBL_Ajuste_Observacion.DBUpdate(observacion, Convert.ToInt32(Cod));
                        break;
                }

                Response.Write("Cod:" + Cod + ":");

                dbmBanagrario.Transaction_Commit();
            }
            catch (Exception ex)
            {
                if (dbmBanagrario != null) dbmBanagrario.Transaction_Rollback();
                Response.Write("Error:" + ex.Message +":");
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        #endregion
    }
}