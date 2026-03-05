using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Services;
using DBArchiving;
using Miharu.Explorer._Clases;
using Slyg.Tools;
using System.Globalization;
using System.Configuration;
using System.Web.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace Miharu.Explorer._Site.Garantias
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        #region Eventos
            
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
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
                        var tblPrioridad = dbmArchiving.Schemadbo.CTA_Prioridades_Usuario.DBFindByfk_Usuario(Utils.MySession.Usuario.id);
                        var tblMotivo = dbmArchiving.Schemadbo.CTA_Motivos_Usuario.DBFindByfk_Usuario(Utils.MySession.Usuario.id);
                        var tblTipo = dbmArchiving.Schemadbo.CTA_TipoSolicitudes_Usuario.DBFindByfk_Usuario(Utils.MySession.Usuario.id);
                        var tblusuario = dbmArchiving.SchemaSecurity.CTA_Usuario.DBFindByfk_Entidad(Utils.MySession.Entidad.id);

                        var resultado = (from DataRow data in dbmCore.Schemadbo.PA_Campos_Busqueda_Rol.DBExecute(Utils.MySession.Usuario.id).Rows
                                         select "<Option value='" + data["id_Campo_Busqueda"] + "-" + data["fk_Campo_Tipo"] + "'>" + data["Nombre_Campo_Busqueda"] + "</option>").ToArray();

                        //ddlPrioridad.Fill(tblPrioridad, "id_Solicitud_Prioridad", "Nombre_Solicitud_Prioridad");
                        //ddlMotivo.Fill(tblMotivo, "id_Solicitud_Motivo", "Nombre_Solicitud_Motivo");
                        //ddlTipo.Fill(tblTipo, "id_Solicitud_Tipo", "Nombre_Solicitud_Tipo");
                        //ddlUsuarioEntidad.Fill(tblusuario, "id_Usuario", "Nombres");
                        ddlEntidad.Fill(tblEntidad, "id_Entidad", "Nombre_Entidad");
                        var tblProyecto = dbmCore.SchemaSecurity.CTA_Rol_Entidades_Proyectos.DBFindByid_Entidadfk_Usuario(short.Parse(ddlEntidad.SelectedItem.Value), Utils.MySession.Usuario.id);
                        ddlProyecto.Fill(tblProyecto, "id_Proyecto", "Nombre_Proyecto");
                        ComboTipo.Value = tblTipo.getOptionGrid("id_Solicitud_Tipo", "Nombre_Solicitud_Tipo");
                        ComboPrioridad.Value = tblPrioridad.getOptionGrid("id_Solicitud_Prioridad", "Nombre_Solicitud_Prioridad");
                        ComboMotivo.Value = tblMotivo.getOptionGrid("id_Solicitud_Motivo", "Nombre_Solicitud_Motivo");

                        //ComboCampoBusqueda.Value = string.Join("", resultado);

                        EntidadUsuario.Value = Utils.MySession.Entidad.id.val();
                        ImagingURL.Value = Utils.VisorURL;

                        if (tblPrioridad.Rows.Count == 0 || tblMotivo.Rows.Count == 0 || tblTipo.Rows.Count == 0)
                            PermisoSolicitudes.Value = "0";
                        else
                            PermisoSolicitudes.Value = "1";

                        if (Solicitud == null)
                            Solicitud = new List<string>();
                    }
                    finally
                    {
                        if (dbmCore != null) dbmCore.Connection_Close();
                        if (dbmArchiving != null) dbmArchiving.Connection_Close();
                    }
                }
            }

            protected void Upload(object sender, EventArgs e)
            {
                DBCore.DBCoreDataBaseManager dbmCore = null;
                
                SlygNullable<int> idCampoBusqueda = DBNull.Value;
                SlygNullable<int> fkCampoTipo = DBNull.Value;
                SlygNullable<string> valor = DBNull.Value;
                SlygNullable<int> fkEntidad = DBNull.Value;
                SlygNullable<int> fkProyecto = DBNull.Value;
                SlygNullable<int> fkProyectoLlave = DBNull.Value;
                SlygNullable<string> valorLlave = DBNull.Value;

                //Upload and save the file
                string csvPath = Server.MapPath("~/_Temporal/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(csvPath);

                fkEntidad = int.Parse(ddlEntidad.SelectedValue);
                fkProyecto = int.Parse(ddlProyecto.SelectedValue);
                fkProyectoLlave = 1;

                DataTable dt = new DataTable();
                
                int flag = 0;
                string csvData = File.ReadAllText(csvPath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (flag == 0)
                        {
                            foreach (string cell in row.Split(';'))
                            {
                                dt.Columns.Add(cell);
                            }
                            flag = 1;
                        }
                        else
                        {

                            dt.Rows.Add();
                            int i = 0;
                            foreach (string cell in row.Split(';'))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Replace("\r", "");
                                if (i == 0)
                                {
                                    try
                                    {
                                        dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                                        dbmCore.Connection_Open(Utils.MySession.Usuario.id);

                                        valorLlave = cell.Replace("\r", "");

                                        var info = dbmCore.SchemaProcess.PA_Busqueda_get.DBExecute(idCampoBusqueda, fkCampoTipo, valor, fkEntidad, fkProyecto, fkProyectoLlave, valorLlave, Utils.MySession.Usuario.id);

                                        foreach (DataRow data in info.Rows)
                                        {
                                            string Tipo = data["Tipo"].val();
                                            if (Tipo == "Garantia")
                                            {
                                                string CBarras_Folder = data["CBarras_Folder"].val();
                                                var addcd = AddCodigo(CBarras_Folder);
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        if (dbmCore != null) dbmCore.Connection_Close();
                                    }
                                }

                                i++;
                            }
                        }
                    }
                }
                var gensol = GetGestionSolicitud();
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

                    var resultado = dbmCore.SchemaSecurity.CTA_Rol_Entidades_Proyectos.DBFindByid_Entidadfk_Usuario(idEntidad, Utils.MySession.Usuario.id);
                    return resultado.getOption("id_Proyecto", "Nombre_Proyecto");
                }
                finally
                {
                    if (dbmCore != null) dbmCore.Connection_Close();
                }
            }


            [WebMethod]
            public static short AddCodigo(string codigo)
            {
                Utils.IsValidSession();

                if (Solicitud.Remove(codigo))
                    return 0;

                Solicitud.Add(codigo);
                return 1;
            }

    
            [WebMethod]
            public static string GetGestionSolicitud()
            {
                Utils.IsValidSession();

                DBCore.DBCoreDataBaseManager dbmCore = null;

                var gestion = new List<string>();

                try
                {
                    dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                    dbmCore.Connection_Open(Utils.MySession.Usuario.id);

                    foreach (var solicitudItem in Solicitud)
                    {
                        var LlavesString = "";

                        var FolderDataTable = dbmCore.SchemaProcess.TBL_Folder.DBFindByCBarras_Folder(solicitudItem);
                        if (FolderDataTable.Rows.Count > 0)
                        {
                            foreach (var FolderRow in FolderDataTable)
                            {
                                var ExpedienteLlaves =
                                    dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(FolderRow.fk_Expediente);

                                foreach (var Llave in ExpedienteLlaves)
                                {
                                    //LlavesString = Llave.Nombre_Proyecto_Llave.Replace(' ', '_') + ": '" + Llave.Valor_Llave + "',";
                                    var Posicion = ExpedienteLlaves.Rows.IndexOf(Llave) + 1;
                                    LlavesString += "Llave" + Posicion + ": '" + Llave.Valor_Llave + "',";
                                }
                            }
                        }
                        else
                        {
                            var FileDataTable = dbmCore.SchemaProcess.TBL_File.DBFindByCBarras_File(solicitudItem);
                            if (FileDataTable.Rows.Count > 0)
                            {
                                foreach (var FileRow in FileDataTable)
                                {
                                    var ExpedienteLlaves =
                                        dbmCore.SchemaProcess.CTA_Expediente_LLave.DBFindByid_Expediente(
                                            FileRow.fk_Expediente);

                                    foreach (var Llave in ExpedienteLlaves)
                                    {
                                        //LlavesString = Llave.Nombre_Proyecto_Llave.Replace(' ', '_') + ": '" + Llave.Valor_Llave + "',";
                                        var Posicion = ExpedienteLlaves.Rows.IndexOf(Llave) + 1;
                                        LlavesString += "Llave" + Posicion + ": '" + Llave.Valor_Llave + "',";
                                    }
                                }
                            }
                        }
                        gestion.Add(solicitudItem + "---");
                        
                    }
                }
                catch (Exception)
                {
                    if (dbmCore != null) dbmCore.Connection_Close();
                    throw;
                }

                var gensol = GestionSolicitud(gestion,"1");
                //var gestion = Solicitud.Select(dato => "[{Codigo: '" + dato + "', Tipo: '" + GetTipo(dato) + "', Motivo: '', TipoSolicitud: '', Prioridad: '', Accion: ''}]").ToArray();
                return "[" + string.Join(",", gestion) + "]";

            }

                
            [WebMethod]
            public static string GestionSolicitud(List<string> solicitudesList, string usuario)
            {
                Utils.IsValidSession();

                DBArchiving.DBArchivingDataBaseManager dbmArchiving = null;

                try
                {
                    //var solicitudesList = gestiones.Split(';');
                    var usuarioSolicitud = usuario.Split(';');
                    SlygNullable<int> solicitudUsuario = null;
                    SlygNullable<int> solicitudSolicitante = null;

                    dbmArchiving = new DBArchiving.DBArchivingDataBaseManager(Utils.ConnectionString.Archiving);
                    DBArchiving.DBArchivingDataBaseManager.IdentifierDateFormat = Utils.IdentifierDateFormat;

                    dbmArchiving.Connection_Open(Utils.MySession.Usuario.id);
                    dbmArchiving.Transaction_Begin();

                    switch (usuarioSolicitud[0])
                    {
                        case "1":
                            solicitudUsuario = Utils.MySession.Usuario.id;
                            break;
                        case "2":
                            solicitudUsuario = int.Parse(usuarioSolicitud[1]);
                            break;
                        case "3":
                            var idSolicitante = dbmArchiving.SchemaCustody.TBL_Solicitud_Solicitante.DBNextId();
                            solicitudSolicitante = idSolicitante;
                            var solicitanteString = usuarioSolicitud[1].Split(',');

                            var solicitante = new DBArchiving.SchemaCustody.TBL_Solicitud_SolicitanteType
                            {
                                Nombres_Solicitante = solicitanteString[0]
                                ,
                                Apellidos_Solicitante = solicitanteString[1]
                                ,
                                Identificacion_Solicitante = solicitanteString[2]
                                ,
                                Nombre_Entidad_Solicitante = solicitanteString[3]
                                ,
                                Direccion_Solicitante = solicitanteString[4]
                                ,
                                Departamento_solicitante = solicitanteString[5]
                                ,
                                Ciudad_Solicitante = solicitanteString[6]
                                ,
                                id_Solicitante = idSolicitante
                            };
                            dbmArchiving.SchemaCustody.TBL_Solicitud_Solicitante.DBInsert(solicitante);
                            break;
                    }

                    var maxId = dbmArchiving.SchemaCustody.TBL_Solicitud.DBNextId();
                    var solicitudRow = new DBArchiving.SchemaCustody.TBL_SolicitudType
                    {
                        Bloqueada = false
                        ,
                        Fecha_Solicitud = DateTime.Now
                        ,
                        fk_Usuario = Utils.MySession.Usuario.id
                        ,
                        id_Solicitud = maxId
                        ,
                        fk_Solicitante = solicitudSolicitante
                        ,
                        fk_Usuario_Destino = solicitudUsuario
                    };
                    dbmArchiving.SchemaCustody.TBL_Solicitud.DBInsert(solicitudRow);

                    foreach (var solicitudItem in solicitudesList)
                    {
                        var item = solicitudItem.Split('-');
                        dbmArchiving.Schemadbo.PA_Solicitud_Gestionar.DBExecute(item[0], maxId, int.Parse(item[1]), int.Parse(item[2]), int.Parse(item[3]));
                    }

                    dbmArchiving.Transaction_Commit();
                    Solicitud = new List<string>();
                    return "Se ha creado la solicitud con identificador [" + maxId.val() + "]";
                }
                catch (Exception ex)
                {
                    if (dbmArchiving != null) dbmArchiving.Transaction_Rollback();
                    return ex.Message;
                }
                finally
                {
                    if (dbmArchiving != null) dbmArchiving.Connection_Close();
                }
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