using System;

namespace WebPunteoElectronico.Clases
{
    public abstract class PopUpBase : GenericBase
    {
        #region Propiedades

        public new Master.MasterPopUp Master
        {
            get { return (Master.MasterPopUp)base.Master; }
        }

        #endregion

        #region Eventos


        #endregion

        #region Funciones

        //private bool ValidarNavegacion(string nPageName, string nPathPermiso)
        //{
        //    if (this.MiharuSession == null)
        //        Response.Redirect("~/Site/Blankform.aspx");
        //    else if (this.MiharuSession.Pagina.Name != nPageName)
        //        Response.Redirect("~/Site/Blankform.aspx");
        //    else if (nPathPermiso == "0")
        //        return true;
        //    else if (ValidarPermisos(nPathPermiso))
        //        return true;

        //    return false;
        //}

        #endregion

        protected override void Config_Page()
        {
            throw new NotImplementedException();
        }

        protected override void Load_Data()
        {
            throw new NotImplementedException();
        }
    }
}