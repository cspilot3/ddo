using System;
using System.Collections.Generic;
using DBCore;
using DBSecurity;
using Miharu.Client.Browser.code;
using DBSecurity.SchemaConfig;
using DBIntegration;
using Miharu.Client.Browser.code.Grid;
using Slyg.Tools;

namespace  Miharu.Client.Browser.site.administracion.seguridad
{
    public partial class roles : page_form
    {
        #region Eventos

        public override void Config_Page()
        {
            this.Master.Title = "Roles";

            EditOptions.Add(Option.add, Option.remove, Option.save);

            MainGrid.Initialize(new DBIntegration.SchemaSecurity.CTA_Rol_EntidadDataTable());
            MainGrid.ColModel.Add(new FlexColumnMap()
                                      {
                                          ColumnName = DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol.ColumnName,
                                          Header = "Id Rol",
                                          IsFilterList = true
                                      });
            MainGrid.ColModel.Add(new FlexColumnMap()
                                      {
                                          ColumnName =
                                              DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.Nombre_Rol.ColumnName,
                                          Header = "Rol",
                                          IsFilterList = true
                                      });
            MainGrid.ColModel.Add(new FlexColumnMap()
            {
                ColumnName =
                    DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.fk_Entidad.ColumnName,
                Header = "Entidad",
                Hidden = true
            });

            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);

                DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.SessionManager.Usuario.id, Program.idModulo, 102, this.SessionManager.ClientIPAddress);
            }
            catch (Exception ex)
            {
                //Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                ScriptHelper.Site.ShowAlert(this, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
        }

        public void CambiarFiltro(ScriptBuilder nHtml)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                MainGrid.LastFilter = GetValue("Filtro");
                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                MainGrid.DataSource = dbmIntegration.SchemaSecurity.CTA_Rol_Entidad.DBFindByNombre_Rol(MainGrid.LastFilter);
            }
            catch (Exception ex)
            {
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        public void CargarProyectos(ScriptBuilder nHtml)
        {
            //DBIntegrationDataBaseManager dbmIntegration = null;
            //DBSecurityDataBaseManager dbmSecurity = null;
            //try
            //{
            //    var id_rol = GetValue<SlygNullable<int>>(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol, false);

            //    dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
            //    dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
            //    dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);
            //    dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);

            //    var constructoras = dbmBlock.Schemablock.cs_rol_proyecto.DBFindByid_rolfk_grupo_empresarial(id_rol, IdGrupoEmpresarial);

            //    nHtml.Append("Frm.Proyectos = [");

            //    for (var i = 0; i < constructoras.Count; i++)
            //    {
            //        var row = constructoras[i];
            //        if (i > 0) nHtml.Append(",");
            //        nHtml.Append("{" + GetJsonValue(row, cs_rol_proyectoEnum.fk_entidad) + ",");
            //        nHtml.Append(GetJsonValue(row, cs_rol_proyectoEnum.id_proyecto) + ",");
            //        nHtml.Append(GetJsonValue(row, cs_rol_proyectoEnum.nombre_entidad) + ",");
            //        nHtml.Append(GetJsonValue(row, cs_rol_proyectoEnum.nombre_proyecto) + ",");
            //        if (id_rol != null && !id_rol.IsDbNull)
            //            nHtml.Append(GetJsonValue(row, cs_rol_proyectoEnum.asignado) + "}");
            //        else
            //            nHtml.Append(cs_rol_proyectoEnum.asignado.ColumnName + ":0" + "}");
            //    }
            //    nHtml.Append("];");
            //}
            //catch (Exception ex)
            //{
            //    TraceError(nHtml, ex);
            //}
            //finally
            //{
            //    if (dbmBlock != null) dbmBlock.Connection_Close();
            //}
        }

        public void OptionClick(ScriptBuilder nHtml)
        {
            try
            {
                var option = Option.Parse(GetValue("option"));

                if (option == Option.remove) Eliminar(nHtml);
                if (option == Option.save) Guardar(nHtml);
            }
            catch (Exception ex) { TraceError(nHtml, new Exception("Opción errada, " + ex.Message, ex)); }
        }

        #endregion

        #region Metodos

        public void CargarEsquema(ScriptBuilder nHtml)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var idRol = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol, false);


                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                var EsquemaEntidad = dbmIntegration.SchemaSecurity.CTA_Entidad_Esquema.DBFindByfk_Entidad(Program.idCliente);
               
                nHtml.Append("Frm.Esquema = [");

                for (var i = 0; i < EsquemaEntidad.Count; i++)
                {
                    var row = EsquemaEntidad[i];
                    if (i > 0) nHtml.Append(",");
                    nHtml.Append("{" +
                                 GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Rol_EsquemaEnum.fk_Esquema) +
                                 ",");
                    nHtml.Append(
                        GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Rol_EsquemaEnum.Nombre_Esquema) + ",");
                    nHtml.Append(
                        GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Rol_EsquemaEnum.fk_Proyecto) + ",");
                    var esquemaasignado = dbmIntegration.SchemaSecurity.CTA_Rol_Esquema.DBFindByfk_Rolfk_Entidadfk_Esquema(idRol, Program.idCliente, EsquemaEntidad[i].fk_Esquema);
                    //if (esquemaasignado.Rows.Count != 0)
                    //    nHtml.Append("asignado:true" + "}");
                    //else
                    //    nHtml.Append("asignado:false" + "}");
                    nHtml.Append("asignado:true" + "}");

                }
                nHtml.Append("];");
            }
            catch (Exception ex)
            {
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        public void CargarDocumentoServer(ScriptBuilder nHtml)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var idEsquema = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_DocumentoEnum.fk_Esquema, false);
                var idRol = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol, false);
                var idProyecto = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_DocumentoEnum.fk_Proyecto, false);


                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                var documentoEsquema = dbmIntegration.SchemaSecurity.CTA_Documento_Esquema.DBFindByfk_Entidadfk_Proyectofk_Esquema(Program.idCliente, idProyecto, idEsquema);
                var lista = new List<string>();

                
                for (var i = 0; i < documentoEsquema.Count; i++)
                {
                    var cadena = "";
                    var row = documentoEsquema[i];
                    cadena += "{";
                    cadena += GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Documento_EsquemaEnum.fk_Documento) + ",";
                    cadena += GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Documento_EsquemaEnum.Nombre_Documento) + ",";
                    
                    var permisoasignado = dbmIntegration.SchemaSecurity.CTA_Rol_Documento.DBFindByfk_Rolfk_Entidadfk_Esquemafk_Documento(idRol,Program.idCliente,idEsquema,documentoEsquema[i].fk_Documento);
                    
                    if (permisoasignado.Rows.Count != 0)
                    {
                        cadena += "descargar:" + permisoasignado[0].Descargar.ToString().ToLower() + ",";
                        cadena += "data:" + permisoasignado[0].Ver_Data.ToString().ToLower() + ",";
                        cadena += "imagen:" + permisoasignado[0].Ver_Imagen.ToString().ToLower() + ",";
                        cadena += "registro:" + permisoasignado[0].Ver_Registro.ToString().ToLower();
                    }
                    else
                    {
                        cadena += "descargar:false" + ",";
                        cadena += "data:false" + ",";
                        cadena += "imagen:false" + ",";
                        cadena += "registro:false";
                    }

                    cadena += "}";
                    lista.Add(cadena);
                }
                nHtml.Append("Frm.Documento[Frm.Documento.length] = {fk_Esquema:" + idEsquema.ToString() + ", fk_Proyecto:" + idProyecto.ToString() + ", Documento:[" + string.Join(", ", lista.ToArray()) + "]};");
            }
            catch (Exception ex)
            {
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
            }
        }

        private void Guardar(ScriptBuilder nHtml)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            DBSecurityDataBaseManager dbmSecurity = null;
            DBCoreDataBaseManager dbmCore = null;
            try
            {

                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.Transaction_Begin();

                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                //dbmIntegration.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);
                dbmIntegration.Transaction_Begin();

                dbmCore = new DBCoreDataBaseManager(this.ConnectionStrings.Core);
                //dbmCore.DataBase.Identifier_Date_DataBase_Format = Program.IdentifierDateFormat;
                dbmCore.Connection_Open(this.SessionManager.Usuario.id);
                dbmCore.Transaction_Begin();

                //var idProyect = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_DocumentoEnum.fk_Proyecto, false);

                var esquemaDocumentoPermisoTable = GetValue("parametros");
                var parametros = esquemaDocumentoPermisoTable.Split(';');


                var objRol = new DBSecurity.SchemaSecurity.TBL_RolType
                                 {
                                     id_Rol = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol.ColumnName, false),
                                     Nombre_Rol = GetValue(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.Nombre_Rol.ColumnName),
                                     Descripcion_Rol = GetValue(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.Nombre_Rol.ColumnName)
                                 };
                var objRolIntegracion = new DBIntegration.SchemaSecurity.TBL_RolType
                                            {
                                                fk_Entidad = Program.idCliente,
                                                fk_Rol = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol.ColumnName, false)
                                            };
         
                if (objRol.id_Rol == null || objRol.id_Rol.IsDbNull || objRol.id_Rol.IsNull)
                {
                    objRolIntegracion.fk_Rol = new SlygNullable<short>(dbmSecurity.SchemaSecurity.TBL_Rol.DBNextId());
                    dbmIntegration.SchemaSecurity.TBL_Rol.DBInsert(objRolIntegracion);
                    nHtml.Append("Get('id_rol').value = '" + objRol.id_Rol + "';");

                    objRol.id_Rol = new SlygNullable<short>(dbmSecurity.SchemaSecurity.TBL_Rol.DBNextId());
                    dbmSecurity.SchemaSecurity.TBL_Rol.DBInsert(objRol);
                    nHtml.Append("Get('id_rol').value = '" + objRol.id_Rol + "';");
                }
                else
                {
                    var rolDataIntegration = dbmIntegration.SchemaSecurity.TBL_Rol.DBGet(objRolIntegracion.fk_Entidad, objRolIntegracion.fk_Rol);
                    if (rolDataIntegration.Count == 0) throw new Exception("Rol no encontrado");

                    dbmIntegration.SchemaSecurity.TBL_Rol.DBUpdate(objRolIntegracion, new SlygNullable<short>(rolDataIntegration[0].fk_Entidad), new SlygNullable<short>(rolDataIntegration[0].fk_Rol));

                    var rolData = dbmSecurity.SchemaSecurity.TBL_Rol.DBGet(objRol.id_Rol);
                    if (rolData.Count == 0) throw new Exception("Rol no encontrado");

                    dbmSecurity.SchemaSecurity.TBL_Rol.DBUpdate(objRol, new SlygNullable<short>(rolData[0].id_Rol));
                   
                }
                //Esquemas

                dbmCore.SchemaSecurity.TBL_Rol_Esquema.DBDelete(objRolIntegracion.fk_Rol,new SlygNullable<short>(Program.idCliente),new SlygNullable<short>(Program.idProyecto),null);
              
                foreach (var esquema in parametros)
                {
                    var idEsquema = FindRegistro<short>(esquema, "fk_Esquema");
                    var checkeado = FindRegistro<bool>(esquema, "esquemaChecked");
                    var idProyecto = FindRegistro<short>(esquema, "fk_Proyecto");

                    var existe = dbmCore.SchemaSecurity.TBL_Rol_Esquema.DBFindByfk_Rolfk_Entidadfk_Proyectofk_Esquema(objRolIntegracion.fk_Rol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(idProyecto), new SlygNullable<short>(idEsquema));
                    //TODO Preguntar idProyecto
                    if (checkeado && (existe.Rows.Count == 0))
                    {
                        dbmCore.SchemaSecurity.TBL_Rol_Documento.DBDelete(objRolIntegracion.fk_Rol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(idProyecto), new SlygNullable<short>(idEsquema), null);
                        
                        var esquemasType = new DBCore.SchemaSecurity.TBL_Rol_EsquemaType()
                                               {
                                                   fk_Rol = objRolIntegracion.fk_Rol,
                                                   fk_Entidad = new SlygNullable<short>(Program.idCliente),
                                                   fk_Esquema = new SlygNullable<short>(idEsquema),
                                                   fk_Proyecto = new SlygNullable<short>(idProyecto)
                                               };
                        dbmCore.SchemaSecurity.TBL_Rol_Esquema.DBInsert(esquemasType);
                    }
                    
                }
                
                foreach (var documento in parametros)
                {
                    var idEsquema = FindRegistro<short>(documento, "fk_Esquema");
                    var checkeado = FindRegistro<bool>(documento, "esquemaChecked");
                    var idDocumento = FindRegistro<int>(documento, "fk_Documento");
                    var registro = FindRegistro<bool>(documento, "registro");
                    var data = FindRegistro<bool>(documento, "data");
                    var imagen = FindRegistro<bool>(documento, "imagen");
                    var descarga = FindRegistro<bool>(documento, "descargar");
                    var idProyecto = FindRegistro<short>(documento, "fk_Proyecto");

                    var existe = dbmCore.SchemaSecurity.TBL_Rol_Documento.DBFindByfk_Rolfk_Entidadfk_Proyectofk_Esquemafk_Documento(objRolIntegracion.fk_Rol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(idProyecto), new SlygNullable<short>(idEsquema), new SlygNullable<int>(idDocumento));
                    var documentosType = new DBCore.SchemaSecurity.TBL_Rol_DocumentoType()
                    {
                        fk_Rol = objRolIntegracion.fk_Rol,
                        fk_Entidad = new SlygNullable<short>(Program.idCliente),
                        fk_Proyecto = new SlygNullable<short>(idProyecto),
                        fk_Esquema = new SlygNullable<short>(idEsquema),
                        fk_Documento = new SlygNullable<int>(idDocumento),
                        Ver_Registro = new SlygNullable<bool>(registro),
                        Ver_Data = new SlygNullable<bool>(data),
                        Ver_Imagen = new SlygNullable<bool>(imagen),
                        Descargar = new SlygNullable<bool>(descarga)
                    };
                    if (checkeado && (existe.Rows.Count == 0))
                    {
                        dbmCore.SchemaSecurity.TBL_Rol_Documento.DBDelete(objRolIntegracion.fk_Rol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(idProyecto), new SlygNullable<short>(idEsquema), new SlygNullable<int>(idDocumento));
                        dbmCore.SchemaSecurity.TBL_Rol_Documento.DBInsert(documentosType);
                    }
                    if (checkeado && (existe.Rows.Count > 0))
                    {
                        dbmCore.SchemaSecurity.TBL_Rol_Documento.DBDelete(objRolIntegracion.fk_Rol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(idProyecto), new SlygNullable<short>(idEsquema), new SlygNullable<int>(idDocumento));
                        dbmCore.SchemaSecurity.TBL_Rol_Documento.DBInsert(documentosType);
                    }
                }

                dbmSecurity.Transaction_Commit();
                dbmIntegration.Transaction_Commit();
                dbmCore.Transaction_Commit();
                MainGrid.DataSource = dbmIntegration.SchemaSecurity.CTA_Rol_Entidad.DBFindByNombre_Rol(MainGrid.LastFilter);
                

                //ScriptHelper.Frm.OcultarDetalle(nHtml);
                ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje", "Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();
                if (dbmIntegration != null) dbmIntegration.Transaction_Rollback();
                if (dbmCore != null) dbmCore.Transaction_Rollback();
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }

        private void Eliminar(ScriptBuilder nHtml)
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            DBIntegrationDataBaseManager dbmIntegration = null;
            DBCoreDataBaseManager dbmCore = null;
            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.Transaction_Begin();

                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                //dbmIntegration.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);
                dbmIntegration.Transaction_Begin();

                dbmCore = new DBCoreDataBaseManager(this.ConnectionStrings.Core);
                //dbmCore.DataBase.Identifier_Date_DataBase_Format = Program.IdentifierDateFormat;
                dbmCore.Connection_Open(this.SessionManager.Usuario.id);
                dbmCore.Transaction_Begin();

                var idRol = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol, false);
                var idProyecto = GetValue<SlygNullable<short>>(DBIntegration.SchemaSecurity.CTA_Rol_DocumentoEnum.fk_Proyecto, false);

                dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBDelete(null,idRol);
                dbmIntegration.SchemaSecurity.TBL_Rol.DBDelete(Program.idCliente,idRol);
                dbmSecurity.SchemaSecurity.TBL_Rol.DBDelete(idRol);

                dbmCore.SchemaSecurity.TBL_Rol_Esquema.DBDelete(idRol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(idProyecto), null);
                //dbmCore.SchemaSecurity.TBL_Rol_Documento.DBDelete(idRol, new SlygNullable<short>(Program.idCliente), new SlygNullable<short>(Program.idProyecto), null, null);


                MainGrid.DataSource = dbmIntegration.SchemaSecurity.CTA_Rol_Entidad.DBFindByNombre_Rol(MainGrid.LastFilter);

                dbmSecurity.Transaction_Commit();
                dbmIntegration.Transaction_Commit();
                dbmCore.Transaction_Commit();

                //ScriptHelper.Frm.OcultarDetalle(nHtml);
                ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje", "Registro eliminado con éxito");
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();
                if (dbmIntegration != null) dbmIntegration.Transaction_Rollback();
                if (dbmCore != null) dbmCore.Transaction_Rollback();
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }


        private T FindRegistro<T>(string nRegistro, string nKey, bool nRequired = true)
        {
            var type = typeof(T);
            var registro = nRegistro.Split(',');
            foreach (var r in registro)
            {
                if (r.Split(':')[0] == nKey)
                {
                    try
                    {
                        if (typeof(T) == typeof(System.DateTime) || typeof(T) == typeof(SlygNullable<DateTime>))
                        {
                            int year = int.Parse(r.Split(':')[1].Substring(0, 4));
                            int month = int.Parse(r.Split(':')[1].Substring(5, 2));
                            int day = int.Parse(r.Split(':')[1].Substring(8, 2));

                            var fecha = new DateTime(year, month, day);

                            if (typeof(T) == typeof(System.DateTime))
                                return (T)Convert.ChangeType(fecha, typeof(T));

                            return (T)Convert.ChangeType(new SlygNullable<DateTime>(fecha), typeof(T));
                        }
                        if (typeof(T) == typeof(decimal) || typeof(T) == typeof(SlygNullable<decimal>))
                        {
                            var valor = decimal.Parse(r.Split(':')[1].Replace(",", "").Replace(".",
                                                                                 Slyg.Tools.DataConvert.GetPuntoFlotante
                                                                                     ()));

                            if (typeof(T) == typeof(decimal))
                                return (T)Convert.ChangeType(valor, typeof(T));

                            return (T)Convert.ChangeType(new SlygNullable<decimal>(valor), typeof(T));
                        }

                        return (T)Convert.ChangeType(r.Split(':')[1], typeof(T));
                    }
                    catch (Exception)
                    {
                        return (T)type.Assembly.CreateInstance(type.FullName);
                    }

                }

            }

            return (T)type.Assembly.CreateInstance(type.FullName);
        }
      

        #endregion
    }
}