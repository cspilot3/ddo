using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Services;
using DBArchiving;
using Miharu.Explorer.Imaging._Clases;
using Slyg.Tools;
using System.Globalization;

namespace Miharu.Explorer.Imaging._Site.Garantias.Bancoomeva
{
    public partial class Reporte_Multiactiva : System.Web.UI.Page
            {

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.IsValidSession();
            DBCore.DBCoreDataBaseManager dbmCore = null;
            DBArchivingDataBaseManager dbmArchiving = null;

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);

                dbmArchiving = new DBArchiving.DBArchivingDataBaseManager(Utils.ConnectionString.Archiving);
                dbmArchiving.Connection_Open(Utils.MySession.Usuario.id);

                var tblEntidad = dbmCore.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Utils.MySession.Entidad.id);
                var tblFechaRecoleccion = dbmArchiving.SchemaRisk.CTA_Control_Cargue_Reporte.DBFindByfk_Entidad(Utils.MySession.Entidad.id);
                var resultado = (from DataRow data in dbmCore.Schemadbo.PA_Campos_Busqueda_Rol.DBExecute(Utils.MySession.Usuario.id).Rows
                                 select "<Option value='" + data["id_Campo_Busqueda"] + "-" + data["fk_Campo_Tipo"] + "'>" + data["Nombre_Campo_Busqueda"] + "</option>").ToArray();

                ddlEntidad.Fill(tblEntidad, "id_Entidad", "Nombre_Entidad");
                

                EntidadUsuario.Value = Utils.MySession.Entidad.id.val();
                ImagingURL.Value = Utils.VisorURL;

            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
                if (dbmArchiving != null) dbmArchiving.Connection_Close();
            }
        }

        #endregion

        #region MetodosPublicos

        [WebMethod]
        public static string GetProyecto(short idEntidad)
        {
            Utils.IsValidSession();

            DBCore.DBCoreDataBaseManager dbmCore = null;
            
            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);

                var resultado = dbmCore.SchemaConfig.TBL_Proyecto.DBGet(idEntidad, null);
                return resultado.getOption("id_Proyecto", "Nombre_Proyecto");
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }            
        }

        [WebMethod]
        public static string GetProyectoLlave(short idEntidad, short idProyecto)
        {
            Utils.IsValidSession();
            
            DBCore.DBCoreDataBaseManager dbmCore = null;
            
            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);
                
                var resultado = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBGet(idEntidad, idProyecto, null);
                return resultado.getOption("id_Proyecto_Llave", "Nombre_Proyecto_Llave");   
            }            
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }            
        }

        [WebMethod]
        //public static string BuscarFaltanteLogico(string par1, string val1, string idEntidad, string idProyecto, string idProyectoLlave, string nLlave)
        public static string BuscarReporte(string fecharecoleccion)
        {
            if (!Utils.IsValidSession()) return "La sesion ha caducado";
            //DBCore.DBCoreDataBaseManager dbmCore = null;
            DBArchiving.DBArchivingDataBaseManager dbmArchiving = null;

            try
            {
                DateTime Fecha_Recoleccion;
                Fecha_Recoleccion = DateTime.Parse(fecharecoleccion);
                //dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                //dbmCore.Connection_Open(Utils.MySession.Usuario.id);
                dbmArchiving = new DBArchiving.DBArchivingDataBaseManager(Utils.ConnectionString.Archiving);
                dbmArchiving.Connection_Open(Utils.MySession.Usuario.id);
               //var info = dbmCore.SchemaReport.PA_Reporte_Multiactiva.DBExecute(fecharecoleccion);
                var info = dbmArchiving.SchemaReport.PA_Reporte_Multiactiva.DBExecute(Fecha_Recoleccion);
                return info.Serialize();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (dbmArchiving != null) dbmArchiving.Connection_Close();
            }
        }

        [WebMethod]
        public static string GetFiles(string codigo)
        {
            Utils.IsValidSession();

            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);
                var info = dbmCore.SchemaProcess.PA_Folder_File_Busqueda.DBExecute(Utils.MySession.Usuario.id, codigo);
                var resultados = (from DataRow data in info.Rows
                    select "{CBarras_File:'" + data["CBarras_File"].val() + "'" +
                           ", Folios_File: '" + data["Folios_File"].val() + "'" +
                           ", Monto_File: '" + data["Monto_File"].val() + "'" +
                           ", Nombre_Tipologia: '" + data["Nombre_Tipologia"].val() + "'" +
                           ", Nombre_Estado: '" + data["Nombre_Estado"].val() + "'" +
                           ", id_Estado: '" + data["id_Estado"].val() + "'" +
                           ", id_Entidad: '" + data["id_Entidad"].val() + "'" +
                           ", Imaging: '" + data["Imaging"].val() + "'" +
                           ", File_Unique_Identifier: '" + data["File_Unique_Identifier"].val() + "'" +
                           ", historia: ''" +
                           ", Solicitado: '" + IsSolicitado(data["CBarras_File"].val()) + "'}").ToArray();

                return "[" + string.Join(",", resultados) + "]";
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }

        [WebMethod]
        public static void SesionCaduco()
        {
            var ex = new Utils();
            ex.SesionCaduco();
        }

        #endregion

        #region Funciones

        public static string IsSolicitado(string codigo)
        {
            return Solicitud.Contains(codigo) ? "1" : "0";
        }

        public static string GetTipo(string codigo)
        {
            return codigo.Substring(0, 3) == "000" ? "Carpeta: " + codigo : "Documento: " + codigo;
        }

        #endregion

        #region Propiedades

        public static List<string> Solicitud
        {
            get { return (List<string>)HttpContext.Current.Session["Solicitud"]; }
            set { HttpContext.Current.Session["Solicitud"] = value; }
        }

        #endregion
    }
}