using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Services;
using DBArchiving;
using Miharu.Explorer._Clases;
using Slyg.Tools;

namespace Miharu.Explorer._Site.Garantias.Bancoomeva
{
    public partial class Consulta_Bancoomeva : System.Web.UI.Page
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
                var tblPrioridad = dbmArchiving.Schemadbo.CTA_Prioridades_Usuario.DBFindByfk_Usuario(Utils.MySession.Usuario.id);
                var tblMotivo = dbmArchiving.Schemadbo.CTA_Motivos_Usuario.DBFindByfk_Usuario(Utils.MySession.Usuario.id);
                var tblTipo = dbmArchiving.Schemadbo.CTA_TipoSolicitudes_Usuario.DBFindByfk_Usuario(Utils.MySession.Usuario.id);
                var tblusuario = dbmArchiving.SchemaSecurity.CTA_Usuario.DBFindByfk_Entidad(Utils.MySession.Entidad.id);

                var resultado = (from DataRow data in dbmCore.Schemadbo.PA_Campos_Busqueda_Rol.DBExecute(Utils.MySession.Usuario.id).Rows
                                 select "<Option value='" + data["id_Campo_Busqueda"] + "-" + data["fk_Campo_Tipo"] + "'>" + data["Nombre_Campo_Busqueda"] + "</option>").ToArray();

                ddlPrioridad.Fill(tblPrioridad, "id_Solicitud_Prioridad", "Nombre_Solicitud_Prioridad");
                ddlMotivo.Fill(tblMotivo, "id_Solicitud_Motivo", "Nombre_Solicitud_Motivo");
                ddlTipo.Fill(tblTipo, "id_Solicitud_Tipo", "Nombre_Solicitud_Tipo");
                ddlUsuarioEntidad.Fill(tblusuario, "id_Usuario", "Nombres");
                ddlEntidad.Fill(tblEntidad, "id_Entidad", "Nombre_Entidad");
                ComboTipo.Value = tblTipo.getOptionGrid("id_Solicitud_Tipo", "Nombre_Solicitud_Tipo");
                ComboPrioridad.Value = tblPrioridad.getOptionGrid("id_Solicitud_Prioridad", "Nombre_Solicitud_Prioridad");
                ComboMotivo.Value = tblMotivo.getOptionGrid("id_Solicitud_Motivo", "Nombre_Solicitud_Motivo");

                ComboCampoBusqueda.Value = string.Join("", resultado);

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
                
                //var resultado = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBGet(idEntidad, idProyecto, null);
                var resultado = dbmCore.SchemaReport.PA_Get_ProyectoLlave_Combos.DBExecute(idEntidad, idProyecto);
                return resultado.getOption("id_Proyecto_Llave", "Nombre_Proyecto_Llave");   
            }            
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }            
        }

        [WebMethod]
        public static string Buscar(string idEntidad, string idProyecto, string idProyectoLlave, string nLlave)
        {
            if (!Utils.IsValidSession()) return "La sesion ha caducado";
            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);

                SlygNullable<int> idCampoBusqueda = DBNull.Value;
                SlygNullable<int> fkCampoTipo = DBNull.Value;
                SlygNullable<string> valor = DBNull.Value;
                SlygNullable<int> fkEntidad = DBNull.Value;
                SlygNullable<int> fkProyecto = DBNull.Value;
                SlygNullable<int> fkProyectoLlave = DBNull.Value;
                SlygNullable<string> valorLlave = DBNull.Value;

                if (nLlave != "")
                {
                    fkEntidad = int.Parse(idEntidad);
                    fkProyecto = int.Parse(idProyecto);
                    fkProyectoLlave = int.Parse(idProyectoLlave);
                    valorLlave = nLlave;
                }

                //if (val1 != "")
                //{
                //    var parametro = par1.Split('-');
                //    idCampoBusqueda = int.Parse(parametro[0]);
                //    fkCampoTipo = int.Parse(parametro[1]);
                //    valor = val1;
                //    fkEntidad = int.Parse(idEntidad);
                //}

                var info = dbmCore.SchemaReport.PA_Busqueda_get_2_2.DBExecute(idCampoBusqueda, fkCampoTipo, valor, fkEntidad, fkProyecto, fkProyectoLlave, valorLlave, Utils.MySession.Usuario.id);
                var cadena = (from DataRow data in info.Rows
                              select "{id_Expediente:'" + data["id_Expediente"].val() + "'" +
                                     ", Nombre_Entidad: '" + data["Nombre_Entidad"].val() + "'" +
                                     ", Nombre_Proyecto: '" + data["Nombre_Proyecto"].val() + "'" +
                                     ", Nombre_Esquema: '" + data["Nombre_Esquema"].val() + "'" +
                                     ", CBarras_Folder: '" + data["CBarras_Folder"].val() + "'" +
                                     ", id_Estado: '" + data["id_Estado"].val() + "'" +
                                     ", Nombre_Estado: '" + data["Nombre_Estado"].val() + "'" +
                                     ", Data_1: '" + data["Llave_01"].val() + "'" +
                                     ", Data_2: '" + data["Llave_02"].val() + "'" +
                                     ", Data_3: '" + data["Llave_03"].val() + "'" +
                                     ", id_Entidad: '" + data["id_Entidad"].val() + "'" +
                                     ", FilesCustodia: '" + data["FilesCustodia"].val() + "'" +
                                     ", llaves: '" + data["llaves"].val() + "'" +
                                     ", historia: ''" +
                                     ", Solicitado: '" + IsSolicitado(data["CBarras_Folder"].val()) + "'}").ToArray();


                return "[" + string.Join(",", cadena) + "]";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }

        [WebMethod]
        public static string GetFiles(string codigo, string fk_estado_folder, string llave1, string llave2, string fk_proyecto)
        {
            Utils.IsValidSession();

            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
  
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);
                var info = dbmCore.SchemaReport.PA_Folder_File_Busqueda_2_2.DBExecute(Utils.MySession.Usuario.id, codigo, int.Parse(fk_proyecto), int.Parse(fk_estado_folder), llave1, llave2);
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
                           ", Solicitado: '" + IsSolicitado(data["CBarras_File"].val()) + "'" +
                           ", NumeroGarantia: '" + data["NumeroGarantia"].val() + "'}").ToArray();

                return "[" + string.Join(",", resultados) + "]";
            }
            finally
            {
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }

        [WebMethod]
        public static string GetFileData(string codigo, int tipo)
        {
            Utils.IsValidSession();
            DBCore.DBCoreDataBaseManager dbmCore = null;
            
            try
            {
                dbmCore = new DBCore.DBCoreDataBaseManager(Utils.ConnectionString.Core);
                dbmCore.Connection_Open(Utils.MySession.Usuario.id);

                var files = dbmCore.Schemadbo.PA_File_Data_Busqueda.DBExecute(codigo, Utils.MySession.Usuario.id, tipo, null, null, null);
                return files.Serialize();
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
                    gestion.Add("[{" + LlavesString + " Codigo: '" + solicitudItem + "', Tipo: '" + GetTipo(solicitudItem) +
                                  "', Motivo: '', TipoSolicitud: '', Prioridad: '', Accion: ''}]");
                }
            }
            catch (Exception)
            {
                if (dbmCore != null) dbmCore.Connection_Close();
                throw;
            }
            
            //var gestion = Solicitud.Select(dato => "[{Codigo: '" + dato + "', Tipo: '" + GetTipo(dato) + "', Motivo: '', TipoSolicitud: '', Prioridad: '', Accion: ''}]").ToArray();
            return "[" + string.Join(",", gestion) + "]";
        }

        [WebMethod]
        public static string GestionSolicitud(string gestiones, string usuario)
        {
            Utils.IsValidSession();

            DBArchivingDataBaseManager dbmArchiving = null;

            try
            {
                var solicitudesList = gestiones.Split(';');
                var usuarioSolicitud = usuario.Split(';');
                SlygNullable<int> solicitudUsuario = null;
                SlygNullable<int> solicitudSolicitante = null;

                dbmArchiving = new DBArchivingDataBaseManager(Utils.ConnectionString.Archiving);
                DBArchivingDataBaseManager.IdentifierDateFormat = Utils.IdentifierDateFormat;

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

        [WebMethod]
        public static string GetHistorialSolicitud(string codigo)
        {
            Utils.IsValidSession();

            DBArchivingDataBaseManager dbmArchiving = null;
            
            try
            {
                dbmArchiving = new DBArchivingDataBaseManager(Utils.ConnectionString.Archiving);
                dbmArchiving.Connection_Open(Utils.MySession.Usuario.id);
                
                var info = dbmArchiving.Schemadbo.CTA_Consulta_Historial_Solicitud.DBFindByCBarras_FileCBarras_Folder(codigo, null);
                return info.Serialize();
            }            
            finally
            {
                if (dbmArchiving != null) dbmArchiving.Connection_Close();
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