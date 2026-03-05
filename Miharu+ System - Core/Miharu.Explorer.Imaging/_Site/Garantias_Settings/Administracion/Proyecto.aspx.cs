using System.Web.Services;
using Miharu.Explorer.Imaging._Clases;

namespace Miharu.Explorer.Imaging._Site.Garantias_Settings.Administracion
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