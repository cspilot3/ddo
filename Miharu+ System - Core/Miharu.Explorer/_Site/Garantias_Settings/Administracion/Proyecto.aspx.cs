using System.Web.Services;
using Miharu.Explorer._Clases;

namespace Miharu.Explorer._Site.Garantias_Settings.Administracion
{
    public partial class Proyecto : System.Web.UI.Page
    {
        [WebMethod]
        public static string getData()
        {
            var conexionCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
            conexionCore.Connection_Open();
            var tblProyecto = conexionCore.SchemaConfig.TBL_Proyecto.DBGet(null, null);
            conexionCore.Connection_Close();
            return tblProyecto.Serialize();
        }
    }
}