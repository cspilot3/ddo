using System;
using Miharu.Client.Browser.code.Grid;
using Slyg.Tools;
using Miharu.Client.Browser.code;

namespace  Miharu.Client.Browser.site.administracion.seguridad
{
    public partial class esquemas : page_form
    {
        #region Propiedades

        //public AutoListHelper<tc_entidadDataTable, tc_entidadEnum, tc_entidadRow> Entidad
        //{
        //    get { return GetSessionValue<AutoListHelper<tc_entidadDataTable, tc_entidadEnum, tc_entidadRow>>("Entidad"); }
        //    set { SetSessionValue("Entidad", value); }
        //}

        //public AutoListHelper<ts_esquema_seguridadDataTable, ts_esquema_seguridadEnum, ts_esquema_seguridadRow> Esquema
        //{
        //    get { return GetSessionValue<AutoListHelper<ts_esquema_seguridadDataTable, ts_esquema_seguridadEnum, ts_esquema_seguridadRow>>("Esquema"); }
        //    set { SetSessionValue("Esquema", value); }
        //}

        #endregion

        #region Eventoos

        public override void Config_Page()
        {
            this.Master.Title = "Esquemas de Seguridad";

            EditOptions.Add(Option.add, Option.remove, Option.save);

            //MainGrid.Initialize(new cs_esquemas_seguridadDataTable());


            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.fk_entidad.ColumnName, Hidden = true });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.id_esquema_seguridad.ColumnName, Header = "ID Esquema Seguridad" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.nombre_esquema_seguridad.ColumnName, Header = "Esquema", IsFilterList = true });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.nombre_entidad.ColumnName, Header = "Entidad" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.min_longitud.ColumnName, Header = "Longitud" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.min_especiales.ColumnName, Header = "Especiales" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.min_mayusculas.ColumnName, Header = "Mayúsculas" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.min_minusculas.ColumnName, Header = "Minúsculas" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.min_numeros.ColumnName, Header = "Números" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.num_historial.ColumnName, Header = "Historial", });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.cambiar_password.ColumnName, Header = "Cambiar Contraseña" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.dias_cambio_password.ColumnName, Header = "Días Cambio Contraseña" });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.fk_usuario_log.ColumnName, Header = "Huella", Hidden = true });
            //MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = cs_esquemas_seguridadEnum.fecha_log.ColumnName, Header = "Calidad huella", Hidden = true });


            //DBSecurityDataBaseManager dbmSecurity = null;

            //try
            //{
            //    dbmSecurity = new DBSecurityDataBaseManager(Program.ConnectionString.Security);
            //    dbmSecurity.Connection_Open();

            //    Entidad.Init(dbmSecurity.Schemasecurity.tc_entidad.DBGet(null), tc_entidadEnum.nombre_entidad);

            //}
            //catch (Exception ex)
            //{
            //    Program.TraceError(ex);
            //    ScriptHelper.Site.ShowAlert(this.Page, ex.Message, MsgBoxIcon.IconError);
            //}
            //finally
            //{
            //    if (dbmSecurity != null) dbmSecurity.Connection_Close();
            //}
        }

        public void CambiarFiltro(ScriptBuilder nHtml)
        {
            //DBSecurityDataBaseManager dbmSecurity = null;
            //try
            //{
            //    var entidad = Entidad.GetRowByText(GetValue("entidad_filtro", false));
            //    SlygNullable<short> idEntidad = null;

            //    if (entidad != null)
            //        idEntidad = entidad.id_entidad;

            //    MainGrid.LastFilter = GetValue("Filtro").Replace("*", "%");
            //    dbmSecurity = new DBSecurityDataBaseManager(Program.ConnectionString.Security);
            //    dbmSecurity.Connection_Open();

            //    // MainGrid.DataSource = dbmSecurity.Schemasecurity.cs_esquemas_seguridad.DBFindByfk_entidadnombre_esquema_seguridad(idEntidad, MainGrid.LastFilter);
            //    MainGrid.DataSource = dbmSecurity.Schemasecurity.ps_esquemas_seguridad_find.DBExecute(idEntidad, MainGrid.LastFilter);
            //}
            //catch (Exception ex)
            //{
            //    TraceError(nHtml, ex);
            //}
            //finally
            //{
            //    if (dbmSecurity != null) dbmSecurity.Connection_Close();
            //}
        }

        public void EliminarEsquemaSeguridad(ScriptBuilder nHtml)
        {
            //DBSecurityDataBaseManager dbmSecurity = null;
            //try
            //{
            //    dbmSecurity = new DBSecurityDataBaseManager(Program.ConnectionString.Security);
            //    dbmSecurity.Connection_Open();

            //    var id_entidad = GetValue<short>("id_entidad");
            //    var id_esquema_seguridad = GetValue<int>("id_esquema_seguridad");

            //    var usuarioData =
            //        dbmSecurity.Schemasecurity.ts_usuario.DBFindByfk_entidadfk_esquema_seguridad(id_entidad,
            //                                                                                     id_esquema_seguridad);
            //    if (usuarioData.Rows.Count == 0)
            //    {
            //        dbmSecurity.Schemasecurity.ts_esquema_seguridad.DBDelete(id_entidad, id_esquema_seguridad);
            //        ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje",
            //                                                "Esquema de Seguridad Eliminado con Exito!!");
            //    }
            //    else
            //    {
            //        nHtml.Append("alert('No es posible eliminar el esquema pues esta siendo utilizado.');");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    TraceError(nHtml, ex);
            //}
            //finally
            //{
            //    if (dbmSecurity != null) dbmSecurity.Connection_Close();
            //}
        }

        public void GuardarEsquemaSeguridad(ScriptBuilder nHtml)
        {
            //DBSecurityDataBaseManager dbmSecurity = null;
            //try
            //{

            //    dbmSecurity = new DBSecurityDataBaseManager(Program.ConnectionString.Security);
            //    //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
            //    dbmSecurity.Connection_Open();
            //    dbmSecurity.Transaction_Begin();


            //    var id_esquema_seguridad = GetValue<SlygNullable<int>>("id_esquema_seguridad", false);
            //    var entidad = Entidad.GetRowByText(GetValue("nombre_entidad_esquem", false));


            //    var esquema_seguridad_type = new ts_esquema_seguridadType();
            //    esquema_seguridad_type.fk_entidad = entidad.id_entidad;
            //    esquema_seguridad_type.nombre_esquema_seguridad = GetValue("nombre_esquem", false);
            //    esquema_seguridad_type.min_longitud = GetValue<byte>("longitud_equem");
            //    esquema_seguridad_type.min_especiales = GetValue<byte>("especiales_esquem");
            //    esquema_seguridad_type.min_mayusculas = GetValue<byte>("mayusculas_esquem");
            //    esquema_seguridad_type.min_minusculas = GetValue<byte>("minusculas_esquem");
            //    esquema_seguridad_type.min_numeros = GetValue<byte>("numeros_esquem");
            //    esquema_seguridad_type.num_historial = GetValue<byte>("historial_esquem");
            //    esquema_seguridad_type.cambiar_password = GetValue("cambio_pswd_esquem");
            //    esquema_seguridad_type.dias_cambio_password = GetValue<byte>("dias_cambio_equem");
            //    esquema_seguridad_type.fk_usuario_log = base.SessionManager.User.id;
            //    esquema_seguridad_type.fecha_log = DateTime.Now;

            //    if (id_esquema_seguridad.IsDbNull)
            //    {

            //        esquema_seguridad_type.id_esquema_seguridad =
            //            dbmSecurity.Schemasecurity.ts_esquema_seguridad.DBNextId(entidad.id_entidad);
            //        dbmSecurity.Schemasecurity.ts_esquema_seguridad.DBInsert(esquema_seguridad_type);
            //        nHtml.Append("Frm.id_esquema_seguridad=" + esquema_seguridad_type.id_esquema_seguridad +
            //                     ";");
            //    }
            //    else
            //        dbmSecurity.Schemasecurity.ts_esquema_seguridad.DBUpdate(esquema_seguridad_type, entidad.id_entidad,
            //                                                                 id_esquema_seguridad);


            //    ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje", "Registro Guardado Existosamente!!!");
            //    dbmSecurity.Transaction_Commit();
            //}
            //catch (Exception ex)
            //{
            //    TraceError(nHtml, ex);
            //    if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();
            //}
            //finally
            //{
            //    if (dbmSecurity != null) dbmSecurity.Connection_Close();
            //}
        }

        #endregion
    }
}