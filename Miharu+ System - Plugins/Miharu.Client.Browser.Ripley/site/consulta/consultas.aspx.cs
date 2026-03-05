using System;
using Miharu.Client.Browser.code;
using Miharu.Client.Browser.code.Grid;
using Slyg.Tools;
using System.Collections.Generic;
using DBIntegration.SchemaRipley;
using System.Data;
using DBSecurity;

namespace Miharu.Client.Browser.site.consulta
{
    public partial class consultas : page_form
    {
        #region Propiedades


        public AutoListHelper<DBIntegration.SchemaConfig.CTA_ProyectoDataTable, DBIntegration.SchemaConfig.CTA_ProyectoEnum, DBIntegration.SchemaConfig.CTA_ProyectoRow> ProyectosList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaConfig.CTA_ProyectoDataTable, DBIntegration.SchemaConfig.CTA_ProyectoEnum, DBIntegration.SchemaConfig.CTA_ProyectoRow>>("ProyectosList"); }
            set { SetSessionValue("ProyectosList", value); }
        }

        public AutoListHelper<DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioDataTable, DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioEnum, DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioRow> PuntosList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioDataTable, DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioEnum, DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioRow>>("PuntosList"); }
            set { SetSessionValue("PuntosList", value); }
        }

        public AutoListHelper<DBIntegration.SchemaRipley.CTA_Documento_UsuarioDataTable, DBIntegration.SchemaRipley.CTA_Documento_UsuarioEnum, DBIntegration.SchemaRipley.CTA_Documento_UsuarioRow> DocumentosList
        {
            get { return GetSessionValue<AutoListHelper<DBIntegration.SchemaRipley.CTA_Documento_UsuarioDataTable, DBIntegration.SchemaRipley.CTA_Documento_UsuarioEnum, DBIntegration.SchemaRipley.CTA_Documento_UsuarioRow>>("DocumentosList"); }
            set { SetSessionValue("DocumentosList", value); }
        }


        public DBIntegration.SchemaRipley.TBL_Config_CampoDataTable Campos
        {
            get { return GetSessionValue<DBIntegration.SchemaRipley.TBL_Config_CampoDataTable>("Campos"); }
            set { SetSessionValue("Campos", value); }
        }

        public DBIntegration.SchemaRipley.CTA_Documento_UsuarioDataTable PermisosDocumento
        {
            get { return GetSessionValue<DBIntegration.SchemaRipley.CTA_Documento_UsuarioDataTable>("PermisosDocumento"); }
            set { SetSessionValue("PermisosDocumento", value); }
        }


        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void Config_Page()
        {
            this.Master.Title = "Consulta";

            EditOptions.Add(Option.find);

            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;
            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Ripley);

                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);


                this.ProyectosList.Init(dbmIntegration.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidad(Program.idCliente), DBIntegration.SchemaConfig.CTA_ProyectoEnum.Nombre_Proyecto);
                this.PuntosList.Init(dbmIntegration.SchemaRipley.CTA_Rol_Punto_Usuario.DBFindByfk_Usuariofk_Entidad(this.SessionManager.Usuario.id, Program.idCliente), DBIntegration.SchemaRipley.CTA_Rol_Punto_UsuarioEnum.Nombre_Punto);

                MainGrid.LastFilter = "";

                MainGrid.Initialize(new DataTable());
                MainGrid.DataSource = null;
                var documentoInput = DocumentosList.GetRowByText(TBL_Config_DocumentoEnum.Nombre_Documento.ToString());
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "UrlImagen", Header = "Imagen", Width = 40 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Config_PuntoEnum.Nombre_Punto.ColumnName, Header = "Punto", Width = 100 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_PrecintoEnum.Fecha_Publicacion.ColumnName, Header = "Fecha Publicacion", Width = 80 });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Config_DocumentoEnum.Nombre_Documento.ColumnName, Header = "Documento", Width = 150 });

                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_1.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_2.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_3.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_4.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_5.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_6.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_7.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_8.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_9.ColumnName, Header = "N/A", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = TBL_Process_DataEnum.Campo_10.ColumnName, Header = "N/A", Hidden = true });

                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "fk_Expediente", Header = "Expediente", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "fk_Folder", Header = "Folder", Hidden = true });
                MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = "fk_File", Header = "File", Hidden = true });

            }
            catch (Exception ex)
            {
                Program.TraceError(ex);
                ScriptHelper.Site.ShowAlert(this.Page, ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmIntegration != null) dbmIntegration.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
            }

            DBSecurityDataBaseManager dbmSecurity = null;
            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(this.ConnectionStrings.Security);

                DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(this.SessionManager.Usuario.id);
                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.SessionManager.Usuario.id, Program.idModulo, 201, this.SessionManager.ClientIPAddress);
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

        public void CargarDocumentosPorProyecto(ScriptBuilder nHtml)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var proyectoInput = ProyectosList.GetRowByText(GetValue("proyectoInput"));
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                this.DocumentosList.Init(dbmIntegration.SchemaRipley.CTA_Documento_Usuario.DBFindByfk_Entidadfk_Proyectofk_UsuarioVer_RegistroVer_DataVer_ImagenDescargar(Program.idCliente, proyectoInput.id_Proyecto, this.SessionManager.Usuario.id, true, true, null, null), DBIntegration.SchemaRipley.CTA_Documento_UsuarioEnum.Nombre_Documento);
                var Docs = dbmIntegration.SchemaRipley.CTA_Documento_Usuario.DBFindByfk_Entidadfk_Proyectofk_UsuarioVer_RegistroVer_DataVer_ImagenDescargar(Program.idCliente, proyectoInput.id_Proyecto, this.SessionManager.Usuario.id, true, true, null, null);
                nHtml.Append("Frm.DocumentosList = " + this.DocumentosList.GetJson() + ";");
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

        public void CargarCamposPorTipo(ScriptBuilder nHtml)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var documentoInput = DocumentosList.GetRowByText(GetValue("documentoInput"));

                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                Campos = dbmIntegration.SchemaRipley.TBL_Config_Campo.DBFindByfk_DocumentoActivoEs_Busqueda(
                    documentoInput.id_Documento, null, null);

                var isFirst = true;
                nHtml.Append("Frm.Campos = [");
                foreach (var campo in Campos)
                {
                    if (!isFirst) nHtml.Append(",");
                    isFirst = false;

                    nHtml.Append("{id_Campo:" + campo.id_Campo + ",");
                    nHtml.Append("Columna:" + campo.Columna + ",");
                    nHtml.Append("Activo:" + campo.Activo.ToString().ToLower() + ",");
                    nHtml.Append("Es_Busqueda:" + campo.Es_Busqueda.ToString().ToLower() + ",");
                    nHtml.Append("Nombre_Campo:'" + campo.Nombre_Campo + "'}");
                }

                nHtml.Append("];");

                var proyectoInput = ProyectosList.GetRowByText(GetValue("proyectoInput"));

                PermisosDocumento = dbmIntegration.SchemaRipley.CTA_Documento_Usuario.DBFindByfk_Entidadfk_Proyectofk_UsuarioVer_RegistroVer_DataVer_ImagenDescargar(Program.idCliente, proyectoInput.id_Proyecto, this.SessionManager.Usuario.id, true, true, null, null);
                //PermisosDocumento = dbmIntegration.SchemaRipley.CTA_Documento_Usuario.DBFindByfk_Entidadfk_Proyectofk_Usuarioid_DocumentoVer_RegistroVer_DataVer_ImagenDescargar(Program.idCliente, proyectoInput.id_Proyecto, this.SessionManager.Usuario.id, documentoInput.id_Documento, true, true, null, null);
                


                var isFirst2 = true;
                nHtml.Append("Frm.PermisosDocumento = [");
                foreach (var permiso in PermisosDocumento)
                {
                    if (!isFirst2) nHtml.Append(",");
                    isFirst2 = false;

                    nHtml.Append("{Ver_Imagen:'" + documentoInput.Ver_Imagen.ToString() + "',");
                    nHtml.Append("Ver_Data:'" + documentoInput.Ver_Data.ToString() + "',");
                    nHtml.Append("Ver_Registro:'" + documentoInput.Ver_Registro.ToString() + "'}");
                }

                nHtml.Append("];");

                //nHtml.Append("Frm.PermisosDocumento = [");
                //nHtml.Append("{Ver_Imagen:'" + documentoInput.Ver_Imagen.ToString() + "',");
                //nHtml.Append("Ver_Data:'" + documentoInput.Ver_Data.ToString() + "',");
                //nHtml.Append("Ver_Registro:'" + documentoInput.Ver_Registro.ToString() + "'}");
                //nHtml.Append("];");

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

        public void CargarValidaciones(ScriptBuilder nHtml)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var fk_Expediente = GetValue<long>("fk_Expediente");
                var fk_Folder = GetValue<short>("fk_Folder");
                var fk_File = GetValue<short>("fk_File");

                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);

                var validaciones = dbmIntegration.SchemaRipley.CTA_Consulta_Validacion.DBFindByfk_Expedientefk_Folderfk_File(fk_Expediente, fk_Folder, fk_File);

                var isFirst = true;
                nHtml.Append("Frm.Validaciones = [");
                foreach (var vali in validaciones)
                {
                    if (!isFirst) nHtml.Append(",");
                    isFirst = false;

                    nHtml.Append("{Respuesta:" + vali.Respuesta.ToString().ToLower() + ",");
                    nHtml.Append("Nombre_Validacion:'" + vali.Nombre_Validacion + "'}");
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

        public void Buscar(ScriptBuilder nHtml)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                var fechaInicialInput = GetValue<DateTime>("fechaInicialInput", false);
                var fechaFinalInput = GetValue<DateTime>("fechaFinalInput", false).AddDays(1).AddSeconds(-1);
                var proyectoInput = ProyectosList.GetRowByText(GetValue("proyectoInput"));
                var puntoInput = PuntosList.GetRowByText(GetValue("puntoInput"));
                var documentoInput = DocumentosList.GetRowByText(GetValue("documentoInput"));



                if (fechaInicialInput > fechaFinalInput)
                    throw new Exception("La fecha Final No puede ser superior a la fecha inicial");

                var fecha30 = fechaInicialInput.AddDays(30);

                if (fechaFinalInput > fecha30)
                    throw new Exception("La fecha Final No puede ser superior a 30 Dias");

                var fechaInicial = fechaInicialInput.ToString("yyyy/MM/dd");
                var fechaFinal = fechaFinalInput.ToString("yyyy/MM/dd");

                var valoresCampo = new List<SlygNullable<string>>();
                for (var i = 0; i <= 10; i++) valoresCampo.Add(new SlygNullable<string>(DBNull.Value));
                var campoAbsoluto = GetValue("cbAbsoluto", false);

                foreach (var campo in Campos)
                {
                    var campoVal = GetValue("campo_" + campo.id_Campo, false);
                    if (campoVal.Trim() != "")
                    {
                        if (campo.Columna > 10) throw new Exception("El campo " + campo.Nombre_Campo + " tiene asociado un numero de columna incorrecto (" + campo.Columna + ")");
                        if (campoAbsoluto != "true")
                        {
                            valoresCampo[campo.Columna] = "%" + campoVal + "%";
                        }
                        else
                        {
                            valoresCampo[campo.Columna] = campoVal;
                        }
                    }
                }


                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(ConnectionStrings.Ripley);
                dbmIntegration.Connection_Open(this.SessionManager.Usuario.id);
                
                var data = dbmIntegration.SchemaRipley.PA_Consulta_GetData.DBExecute(
                        nfk_Documento: documentoInput.id_Documento,
                        nfk_Punto: puntoInput.id_Punto,
                        nfk_Proyecto: proyectoInput.id_Proyecto,
                        nFecha_Movimiento_Inicial: fechaInicial,
                        nFecha_Movimiento_Final: fechaFinal,
                        nCampo_1: valoresCampo[1],
                        nCampo_2: valoresCampo[2],
                        nCampo_3: valoresCampo[3],
                        nCampo_4: valoresCampo[4],
                        nCampo_5: valoresCampo[5],
                        nCampo_6: valoresCampo[6],
                        nCampo_7: valoresCampo[7],
                        nCampo_8: valoresCampo[8],
                        nCampo_9: valoresCampo[9],
                        nCampo_10: valoresCampo[10]
                    );

                MainGrid.DataSource = data;             

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

        #endregion

        #region Funciones

        public string GetScriptVariables()
        {
            try
            {
                var URLVisor = (this.SessionManager.Entidad.id == Program.idProcesador) ? Program.URLVisorImagenInterno : Program.URLVisorImagenExterno;
                var sb = new ScriptBuilder();
                sb.Append("Frm.ProyectosList = " + this.ProyectosList.GetJson() + ";");
                sb.Append("Frm.PuntosList = " + this.PuntosList.GetJson() + ";");
                sb.Append("Frm.DocumentosList = " + this.DocumentosList.GetJson() + ";");
                sb.Append("Frm.UrlVisorImagen = '" + URLVisor + "';");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "alert('" + ex.Message + "');";
            }
        }

        #endregion
    }
}