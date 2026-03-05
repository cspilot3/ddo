using System;
using DBSecurity;
using Miharu.Client.Browser.code;
using DBSecurity.SchemaConfig;
using DBIntegration;
using Miharu.Client.Browser.code.Grid;
using Slyg.Tools;


namespace  Miharu.Client.Browser.site.administracion.seguridad
{
    public partial class usuarios : page_form
    {
        #region Propiedades

        public AutoListHelper<TBL_EntidadDataTable, TBL_EntidadEnum, TBL_EntidadRow> Entidad
        {
            get { return GetSessionValue<AutoListHelper<TBL_EntidadDataTable, TBL_EntidadEnum, TBL_EntidadRow>>("Entidad"); }
            set { SetSessionValue("Entidad", value); }
        }

        public AutoListHelper<DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable, DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadEnum, DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadRow> Esquema
        {
            get { return GetSessionValue<AutoListHelper<DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadDataTable, DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadEnum, DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadRow>>("Esquema"); }
            set { SetSessionValue("Esquema", value); }
        }

        #endregion

        #region Eventos

        public override void Config_Page()
        {
            this.Master.Title = "Usuarios";
           
            EditOptions.Add(Option.add, Option.remove, Option.save);

            MainGrid.Initialize(new DBSecurity.SchemaSecurity.CTA_UsuarioDataTable());
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.fk_Entidad.ColumnName, Header = "Id Entidad", IsFilterList = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.fk_Esquema_Seguridad.ColumnName, Header = "id Esquema de Seguridad", IsFilterList = true, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.id_Usuario.ColumnName, Header = "Id Usuario", IsColumnID = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Login_Usuario.ColumnName, Header = "Login Usuario", Hidden = false });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Password_Usuario.ColumnName, Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombre_Esquema_Seguridad.ColumnName, Header = "Esquema de Seguridad", Hidden = false });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombres_Usuario.ColumnName, Header = "Nombres Usuario", Hidden = false });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Apellidos_Usuario.ColumnName, Header = "Apellidos Usuario", Hidden = false });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Identificacion_Usuario.ColumnName, Header = "Identificación Usuario", Hidden = false });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Solicitar_Cambio_Password.ColumnName, Header = "Cambiar Password", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Fecha_Asignacion_Password.ColumnName, Header = "Fecha Asignación Password", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Fecha_Creacion.ColumnName, Header = "Fecha Creacion Usuario", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Logeado.ColumnName, Header = "Logeado", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Usuario_Activo.ColumnName, Header = "Usuario Activo", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Eliminado_Usuario.ColumnName, Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.fk_Usuario_Log.ColumnName, Header = "Usuario log", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Fecha_log.ColumnName, Header = "Fecha", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombre_Entidad.ColumnName, Header = "Entidad", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Email_Usuario.ColumnName, Header = "Email", Hidden = false });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.fk_Dependencia.ColumnName, Header = "Id Dependencia", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Direccion_Usuario.ColumnName, Header = "Direccion Usuario", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.fk_Usuario_Jefe.ColumnName, Header = "id Usuario Jefe", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Telefono_Usuario.ColumnName, Header = "Telefono", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Observaciones.ColumnName, Header = "Observaciones", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.fk_Cargo.ColumnName, Header = "id Cargo", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Logeo_IP.ColumnName, Header = "IP Logeo", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Logeo_Fecha.ColumnName, Header = "Fecha Logeo", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.LDAP.ColumnName, Header = "LDAP", Hidden = false});
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Token.ColumnName, Header = "Token", Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Fecha_Token.ColumnName, Header = "Fecha Token", Hidden = true });

            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                  dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                  dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);

                  dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.SessionManager.Usuario.id, Program.idModulo, 101, this.SessionManager.ClientIPAddress);

                  Entidad.Init(dbmSecurity.SchemaConfig.TBL_Entidad.DBGet(Program.idCliente), TBL_EntidadEnum.Nombre_Entidad);
                  Esquema.Init(dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet(Program.idCliente, null), DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadEnum.Nombre_Esquema_Seguridad);
            }
            catch (Exception ex)
            {
                Program.TraceError(ex);
                ScriptHelper.Site.ShowAlert(this.Page, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

        }

        public void CambiarFiltro(ScriptBuilder nHtml)
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                MainGrid.LastFilter = GetValue("Filtro");
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);

                MainGrid.DataSource = dbmSecurity.SchemaSecurity.CTA_Usuario.DBFindByfk_EntidadLogin_Usuario(Program.idCliente, MainGrid.LastFilter, 0, new DBSecurity.SchemaSecurity.CTA_UsuarioEnumList(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Login_Usuario, true));
            }
            catch (Exception ex)
            {
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
        }

        public void CargarEsquemas(ScriptBuilder nHtml)
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                var nombreEntidad = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombre_Entidad);
                var entidad = Entidad.GetRowByText(nombreEntidad);

                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                Esquema.Init(dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet((entidad != null) ? entidad.id_Entidad : (short)0, null), DBSecurity.SchemaSecurity.TBL_Esquema_SeguridadEnum.Nombre_Esquema_Seguridad);

                nHtml.Append("Frm.Esquema = " + Esquema.GetJson() + ";");
            }
            catch (Exception ex)
            {
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
        }

        public void CargarPerfiles(ScriptBuilder nHtml)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var idUsuario = GetValue<int>(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.id_Usuario, false);


                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                var perfiles = dbmIntegration.SchemaSecurity.CTA_Perfiles_Entidad.DBFindByfk_Entidad(new SlygNullable<short>(Program.idCliente));
               
                nHtml.Append("Frm.Perfiles = [");

                for (var i = 0; i < perfiles.Count; i++)
                {
                    var row = perfiles[i];
                    if (i > 0) nHtml.Append(",");
                    nHtml.Append("{" + GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Perfiles_EntidadEnum.id_Perfil) + ",");
                    nHtml.Append(GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Perfiles_EntidadEnum.Nombre_Perfil) + ",");
                    var perfilasignado = dbmIntegration.SchemaSecurity.CTA_Perfiles_Usuario.DBFindByfk_Entidadfk_Usuarioid_Perfil(Program.idCliente, idUsuario, perfiles[i].id_Perfil);
                    if (perfilasignado.Rows.Count != 0)
                       nHtml.Append("asignado:true" + "}");
                    else
                        nHtml.Append("asignado:false" + "}");
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

        public void CargarRoles(ScriptBuilder nHtml)
        {
            DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var idUsuario = GetValue<SlygNullable<int>>(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.id_Usuario, false);

                dbmIntegration = new DBIntegrationDataBaseManager(this.ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                var roles = dbmIntegration.SchemaSecurity.CTA_Rol_Entidad.DBFindByfk_Entidad(Program.idCliente);

                nHtml.Append("Frm.Roles = [");

                for (var i = 0; i < roles.Count; i++)
                {
                    var row = roles[i];
                    if (i > 0) nHtml.Append(",");
                    nHtml.Append("{" + GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.id_Rol) + ",");
                    nHtml.Append(GetJsonValue(row, DBIntegration.SchemaSecurity.CTA_Rol_EntidadEnum.Nombre_Rol) + ",");
                    var rolsignado = dbmIntegration.SchemaSecurity.CTA_Rol_Usuario.DBFindByfk_Entidadid_Rolfk_Usuario(Program.idCliente, roles[i].id_Rol, idUsuario);
                    if (rolsignado.Rows.Count != 0)
                        nHtml.Append("asignado:true" + "}");
                    else
                        nHtml.Append("asignado:false" + "}");
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

        private void Guardar(ScriptBuilder nHtml)
        {
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.Transaction_Begin();

                var idUsuario = GetValue<SlygNullable<int>>(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.id_Usuario, false);
                var login = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Login_Usuario);

                var password = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Password_Usuario, false);
                var nombres = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombres_Usuario);
                var apellidos = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Apellidos_Usuario);
                var email = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Email_Usuario);
                var indent = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Identificacion_Usuario);
                var usuario_activo = GetValue<bool>(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Usuario_Activo);
                var cambiar_pass = GetValue<bool>(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Solicitar_Cambio_Password);
                var nombre_entidad = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombre_Entidad);
                var nombre_seguridad = GetValue(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Nombre_Esquema_Seguridad);
                var cambiar_password = GetValue<bool>("cambiar_password");
                var entidad = Entidad.GetRowByText(nombre_entidad);
                var seguridad = Esquema.GetRowByText(nombre_seguridad);
                if (idUsuario.IsDbNull)
                {
                    var usuariosExistentes = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(new SlygNullable<string>(login));
                    if (usuariosExistentes.Count > 0) throw new ApplicationException("El Login ya está siendo utilizado por otro usuario.");

                    idUsuario = new SlygNullable<int>(dbmSecurity.SchemaSecurity.TBL_Usuario.DBNextId());
                    var newUsuario = new DBSecurity.SchemaSecurity.TBL_UsuarioType
                                         {
                                             id_Usuario = idUsuario,
                                             Login_Usuario = new SlygNullable<string>(login),
                                             Password_Usuario = (password == "") ? new SlygNullable<string>(DBNull.Value) : new SlygNullable<string>(Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(password, "", 100)),
                                             Nombres_Usuario = new SlygNullable<string>(nombres),
                                             Apellidos_Usuario = new SlygNullable<string>(apellidos),
                                             Identificacion_Usuario = new SlygNullable<string>(indent),
                                             Email_Usuario = new SlygNullable<string>(email),
                                             Fecha_Asignacion_Password = SlygNullable.SysDate,
                                             Solicitar_Cambio_Password = new SlygNullable<bool>(cambiar_pass),
                                             Usuario_Activo = new SlygNullable<bool>(usuario_activo),
                                             Eliminado_Usuario = new SlygNullable<bool>(false),
                                             fk_Entidad = new SlygNullable<short>(entidad.id_Entidad),
                                             fk_Esquema_Seguridad = new SlygNullable<short>(seguridad.id_Esquema_Seguridad),
                                             Fecha_log = SlygNullable.SysDate,
                                             fk_Usuario_Log = new SlygNullable<int>(this.SessionManager.Usuario.id),
                                             Direccion_Usuario = "",
                                             Telefono_Usuario = "",
                                             Logeado = false
                                         };

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBInsert(newUsuario);

                    nHtml.Append("Get('id_usuario').value = '" + newUsuario.id_Usuario + "';");
                }
                else
                {
                    var userData = dbmSecurity.SchemaSecurity.TBL_Usuario.DBGet(idUsuario);
                    if (userData.Count == 0) throw new Exception("Usuario no encontrado");

                    //var userOri = userData[0].ToTBL_UsuarioType();
                    var userOri = new DBSecurity.SchemaSecurity.TBL_UsuarioType();
                    userOri.Login_Usuario = new SlygNullable<string>(login);
                    if (cambiar_password)
                        userOri.Password_Usuario = (password == "")
                                                       ? new SlygNullable<string>(DBNull.Value)
                                                       : new SlygNullable<string>(
                                                             Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(password, "",
                                                                                                           100));
                    else
                        userOri.Password_Usuario = null;
                    userOri.Fecha_Asignacion_Password = null;
                    userOri.Nombres_Usuario = new SlygNullable<string>(nombres);
                    userOri.Apellidos_Usuario = new SlygNullable<string>(apellidos);
                    userOri.Identificacion_Usuario = new SlygNullable<string>(indent);
                    userOri.Email_Usuario = new SlygNullable<string>(email);
                    userOri.Solicitar_Cambio_Password = new SlygNullable<bool>(cambiar_pass);
                    userOri.Usuario_Activo = new SlygNullable<bool>(usuario_activo);
                    userOri.fk_Usuario_Log = new SlygNullable<int>(base.SessionManager.Usuario.id);
                    userOri.Fecha_log = SlygNullable.SysDate;

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(userOri, idUsuario);
                    dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBDelete(idUsuario, null);
                    dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBDelete(idUsuario, null);
                }

                if (usuario_activo)
                    dbmSecurity.SchemaSecurity.TBL_Conexiones_Usuario.DBDelete(idUsuario, null);

                var perfiles = dbmSecurity.SchemaSecurity.TBL_Perfil.DBGet(null);
                foreach (var p in perfiles)
                {
                    var id = "perfil_" + p.id_Perfil;
                    var asignado = GetValue<bool>(id, false);
                    if (asignado)
                    {
                        var usuarioPerfilType = new DBSecurity.SchemaSecurity.TBL_Usuario_PerfilesType
                                                    {
                                                        fk_Usuario = idUsuario,
                                                        Fecha_Log = SlygNullable.SysDate,
                                                        fk_Perfil = new SlygNullable<short>(p.id_Perfil),
                                                        fk_Usuario_Log = idUsuario
                                                    };

                        dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBInsert(usuarioPerfilType);
                    }
                }

                var roles = dbmSecurity.SchemaSecurity.TBL_Rol.DBGet(null);
                foreach (var p in roles)
                {
                    var id = "rol_" + p.id_Rol;
                    var asignado = GetValue<bool>(id, false);
                    if (asignado)
                    {
                        var usuarioRolType = new DBSecurity.SchemaSecurity.TBL_Usuario_RolesType
                                                 {
                                                     fk_Rol = new SlygNullable<short>(p.id_Rol),
                                                     fk_Usuario = idUsuario,
                                                     fk_Usuario_Log = idUsuario,
                                                     Fecha_Log = SlygNullable.SysDate
                                                 };
                        dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBInsert(usuarioRolType);
                    }
                }

                MainGrid.DataSource = dbmSecurity.SchemaSecurity.CTA_Usuario.DBFindByfk_EntidadLogin_Usuario(Program.idCliente, MainGrid.LastFilter, 0, new DBSecurity.SchemaSecurity.CTA_UsuarioEnumList(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Login_Usuario, true));
                dbmSecurity.Transaction_Commit();
               
                ScriptHelper.Frm.OcultarDetalle(nHtml);
                ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje", "Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                dbmSecurity.Transaction_Rollback();
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
        }

        private void Eliminar(ScriptBuilder nHtml)
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                var idUsuario = GetValue<int>(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.id_Usuario);

                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.Transaction_Begin();

                dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(new DBSecurity.SchemaSecurity.TBL_UsuarioType() { Eliminado_Usuario = new SlygNullable<bool>(true) }, new SlygNullable<int>(idUsuario));

                MainGrid.DataSource = dbmSecurity.SchemaSecurity.CTA_Usuario.DBFindByfk_EntidadLogin_Usuario(Program.idCliente, MainGrid.LastFilter, 0, new DBSecurity.SchemaSecurity.CTA_UsuarioEnumList(DBSecurity.SchemaSecurity.CTA_UsuarioEnum.Login_Usuario, true));

                dbmSecurity.Transaction_Commit();

                ScriptHelper.Frm.OcultarDetalle(nHtml);
                ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje", "Registro eliminado con éxito");
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();
                TraceError(nHtml, ex);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
        }

        #endregion
    }
}