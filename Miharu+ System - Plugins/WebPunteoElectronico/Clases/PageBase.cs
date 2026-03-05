namespace WebPunteoElectronico.Clases
{
    public abstract class PageBase : GenericBase
    {
        #region Propiedades

        public new Master.MasterPage Master
        {
            get { return (Master.MasterPage)base.Master; }
        }

        #endregion

        #region Eventos

        #endregion

        #region Funciones

        //private bool ValidarNavegacion(string nPageName, string nPathPermiso)
        //{
        //    if (this.MiharuSession == null || Session["Session"] == null)
        //        Response.Redirect("~/_sitio/login.aspx");
        //    else if (MiharuSession.Pagina.Name != nPageName)
        //        Response.Redirect("~/_sitio/login.aspx");
        //    else if (nPathPermiso == "0" || ValidarPermisos(nPathPermiso))
        //        return true;
        //    else
        //        Response.Redirect("~/_sitio/login.aspx");

        //    return false;
        //}

        #endregion
    }
}