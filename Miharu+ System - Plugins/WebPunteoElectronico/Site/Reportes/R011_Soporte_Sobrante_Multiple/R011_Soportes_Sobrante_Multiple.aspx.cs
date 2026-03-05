using System;
using System.Data;
using System.IO;
using System.Web.UI;
using Miharu.Desktop.Library.Config;
using Slyg.Tools;
using System.Web.UI.WebControls;
using WebPunteoElectronico.Clases;
using WebPunteoElectronico.Clases.Slyg;
using WebPunteoElectronico.Master;

namespace WebPunteoElectronico.Site.Reportes.R011_Soporte_Sobrante_Multiple
{
    public partial class R011_Soportes_Sobrante_Multiple : FormBase
    {
        #region  Declaraciones

        private const string PermisoAjustar = "5.1.1";
        //private const string PermisoAprobar = "5.1.2";
        private const string PermisoReabrir = "5.1.3";
        private const string Path_Nodo = "3.2.1";

        #endregion

        #region   Propiedades

        public new MasterConfig Master
        {
            get
            {
                return (MasterConfig)base.OriginalMaster;
            }
        }

        public string UrlVisorImagen
        {
            get
            {
                return Program.LocalServerURL + Program.URLVisorImagen;
            }
        }



        public DBAgrario.SchemaConfig.TBL_RegionalDataTable pTablaRegional
        {
            get
            {
                return (DBAgrario.SchemaConfig.TBL_RegionalDataTable)this.MiharuSession.Pagina.Parameter["_RegionalDataTable"];
            }
            set
            {
                this.MiharuSession.Pagina.Parameter["_RegionalDataTable"] = value;
            }
        }

        public DBAgrario.SchemaConfig.TBL_COBDataTable pTablaCOB
        {
            get
            {
                return (DBAgrario.SchemaConfig.TBL_COBDataTable)this.MiharuSession.Pagina.Parameter["_COBDataTable"];
            }
            set
            {
                this.MiharuSession.Pagina.Parameter["_COBDataTable"] = value;
            }
        }

        public DBAgrario.SchemaConfig.CTA_Oficinas_WebDataTable pTablaOficina
        {
            get
            {
                return (DBAgrario.SchemaConfig.CTA_Oficinas_WebDataTable)this.MiharuSession.Pagina.Parameter["_OficinaDataTable"];
            }
            set
            {
                this.MiharuSession.Pagina.Parameter["_OficinaDataTable"] = value;
            }
        }

        private AutoListHelper<DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionDataTable, DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnum, DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionRow> Tipologias
        {
            get { return GetSessionValue<AutoListHelper<DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionDataTable, DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnum, DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionRow>>("Tipologias"); }
            set { SetSessionValue("Tipologias", value); }
        }

        private AutoListHelper<DBCore.SchemaConfig.TBL_EsquemaDataTable, DBCore.SchemaConfig.TBL_EsquemaEnum, DBCore.SchemaConfig.TBL_EsquemaRow> Esquemas
        {
            get { return GetSessionValue<AutoListHelper<DBCore.SchemaConfig.TBL_EsquemaDataTable, DBCore.SchemaConfig.TBL_EsquemaEnum, DBCore.SchemaConfig.TBL_EsquemaRow>>("Esquemas"); }
            set { SetSessionValue("Esquemas", value); }
        }

        #endregion

        #region  Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //Permiso Guardar Ajuste Tipologia
            tdControlGuardar.Visible = (MiharuSession.Usuario.PerfilManager.PuedeAcceder(PermisoAjustar) || MiharuSession.Usuario.PerfilManager.PuedeAcceder(PermisoReabrir));
            var isAjaxCall = string.Equals("XMLHttpRequest", Context.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);
            
            if (!this.IsPostBack && isAjaxCall == false)            
                Config_Page();            
            else if (isAjaxCall) 
                GetTipologiasRequest();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Master.Title = "11- Soportes Sobrantes";

            this.Master.OnSelectedGridChanged += Grid_OnSelectedGridChanged;
            this.ConsultarLinkButton.Click += ConsultarLinkButton_Click;
            this.RegionalFindDropDownList.SelectedIndexChanged += RegionalFindDropDownList_SelectedIndexChanged;
            this.COBFindDropDownList.SelectedIndexChanged += COBFindDropDownList_SelectedIndexChanged;
            this.EditarSeleccionadosButton.Click += EditarSeleccionadosButton_Click;
            this.ExportWordButton.Click += ExportWordButton_Click;
            this.ExportExcelButton0.Click += ExportExcelButton0_Click;
            this.ModoFindDropDownList.SelectedIndexChanged += ModoFindDropDownList_SelectedIndexChanged;

            this.FechaMovimientoInicialTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-','/');
            this.FechaMovimientoFinalTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-','/').Replace('-', '/');
            this.FechaProcesoInicialTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-','/').Replace('-', '/');
            this.FechaProcesoFinalTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-','/').Replace('-', '/');


            
        }

        private void ConfigMainGrid()
        {
            MainGrid.Initialize(new DBAgrario.SchemaReport.CTA_11_Soporte_SobranteDataTable());
            MainGrid.IsMultiCheck = true;
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.fk_Core_Index.ColumnName, Header = "ID", IsColumnID = true, Width = 80 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Fecha_Proceso.ColumnName, Header = "Fecha Proceso", Width = 80 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Fecha_Movimiento.ColumnName, Header = "Fecha Movimiento BAC", Width = 82 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Activo.ColumnName, Header = "Reproceso", Width = 30, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Nombre_COB.ColumnName, Header = "COB", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.id_Oficina.ColumnName, Header = "Código Oficina", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Nombre_Oficina.ColumnName, Header = "Nombre Oficina", Width = 150 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Nombre_Oficina_Tipo.ColumnName, Header = "Tipo Oficina", Width = 70 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Materializada.ColumnName, Header = "Tipo de Registro", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Materializada_Original.ColumnName, Header = "Tipo de Registro PyC", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Codigo_Tx.ColumnName, Header = "Código Transacción", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Nombre_Tx.ColumnName, Header = "Nombre Transacción", Width = 180 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Valor.ColumnName, Header = "Valor", Width = 90 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Valor_Original.ColumnName, Header = "Valor PyC", Width = 90 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Producto.ColumnName, Header = "No. Producto PyC", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Campo_Uno_Original.ColumnName, Header = "Campo 10 PyC", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Campo_Diez_Original.ColumnName, Header = "No. Secuencial y/o Operación PyC", Width = 140 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Numero_Cuenta_Afectada_Original.ColumnName, Header = "Campo 30 PyC", Width = 140 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Fecha_Ultimo_Proceso.ColumnName, Header = "Fecha Ajuste o Calificación Web", Width = 140 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Comision.ColumnName, Header = "Comisión", Width = 90 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Comision_Original.ColumnName, Header = "Comisión PyC", Width = 90 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Url.ColumnName, Header = "Imagen", Width = 70 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Url_Link.ColumnName, Header = "Link Imagen", Width = 70 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.UrlAnexo.ColumnName, Header = "Anexo", Width = 70 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.UrlAnexo_Link.ColumnName, Header = "Link Anexo", Width = 70 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Cantidad_Folios.ColumnName, Header = "Imagenes Tx", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Cantidad_Anexos.ColumnName, Header = "Imagenes Anexos", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Total_Folios.ColumnName, Header = "Imagenes Totales", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.En_Correccion.ColumnName, Header = "Cambiar", Width = 60, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.En_Correccion_Txt.ColumnName, Header = "Reproceso", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Observacion.ColumnName, Header = "Observaciones", Width = 140 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Usuario_Ajuste.ColumnName, Header = "Funcionario Ajusta", Width = 120 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Fecha_Ajuste.ColumnName, Header = "Fecha Ajuste", Width = 80 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Usuario_Aprueba.ColumnName, Header = "Funcionario Aprueba", Width = 120 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Fecha_Aprueba.ColumnName, Header = "Fecha Aprobación", Width = 80 });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.fk_Expediente.ColumnName, Header = "fk_Expediente", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.fk_Folder.ColumnName, Header = "fk_Folder", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.fk_File.ColumnName, Header = "fk_File", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.id_COB.ColumnName, Header = "id_COB", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.id_Regional.ColumnName, Header = "id_Regional", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.id_Oficina_Tipo.ColumnName, Header = "id_Oficina_Tipo", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.fk_Documento.ColumnName, Header = "fk_Documento", Width = 1, Hidden = true });
            MainGrid.ColModel.Add(new FlexColumnMap() { ColumnName = DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.Nombre_Regional.ColumnName, Header = "Regional", Width = 70 });

        }

        protected void ConsultarLinkButton_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        protected void Grid_OnSelectedGridChanged(string nSender, string nValue, CellData nCellData){

            try
            {
                if (nSender == MainGrid.ID)
                {


                    Session["Seleccionados"] = null;
                    var soportesobranteData = (DBAgrario.SchemaReport.CTA_11_Soporte_SobranteRow[])MainGrid.DataSource.Select(DBAgrario.SchemaReport.CTA_11_Soporte_SobranteEnum.fk_Core_Index.ColumnName + "=" + nValue);
                    MainGrid.SetSelectedRows(soportesobranteData[0]);
                    Session["Seleccionados"] = MainGrid.GetSelectedRows();
                    Master.SelectTab(Tabs.Detalle);
                }
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        protected void RegionalFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                if (RegionalFindDropDownList.SelectedValue != "-1")
                {
                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                    dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);

                    DataTable data_Tx = dbmBanagrario.SchemaConfig.TBL_COB.DBFindByfk_Regional((short.Parse(RegionalFindDropDownList.SelectedValue)));

                    dbmBanagrario.Connection_Close();

                    if (data_Tx.Rows.Count > 0)
                        CargarCombo(ref COBFindDropDownList, data_Tx, DBAgrario.SchemaConfig.TBL_COBEnum.id_COB.ColumnName, DBAgrario.SchemaConfig.TBL_COBEnum.Nombre_COB.ColumnName);

                    Master.SelectTab(Tabs.Filtro);
                }
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        protected void COBFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (COBFindDropDownList.SelectedValue == "-1") return;

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);


                DataTable data_Tx = dbmBanagrario.SchemaConfig.CTA_Oficinas_Web.DBFindByfk_COB(short.Parse(COBFindDropDownList.SelectedValue), 0, new DBAgrario.SchemaConfig.CTA_Oficinas_WebEnumList(DBAgrario.SchemaConfig.CTA_Oficinas_WebEnum.Nombre_Oficina, true));

                dbmBanagrario.Connection_Close();

                if (data_Tx.Rows.Count > 0)
                    CargarCombo(ref OficinaFindDropDownList, data_Tx, DBAgrario.SchemaConfig.TBL_OficinaEnum.id_Oficina.ColumnName, DBAgrario.SchemaConfig.TBL_OficinaEnum.Nombre_Oficina.ColumnName);

                Master.SelectTab(Tabs.Filtro);

            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        protected void EditarSeleccionadosButton_Click(object sender, EventArgs e)
        {
            EditarSeleccionados();
        }

        protected void ExportWordButton_Click(object sender, ImageClickEventArgs e)
        {
            ExportToWord();
        }

        protected void ExportExcelButton0_Click(object sender, ImageClickEventArgs e)
        {
            ExportToExcel();
        }

        void ModoFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateModo(int.Parse(ModoFindDropDownList.SelectedValue));
        }

        #endregion

        #region  Metodos

        protected override void Config_Page()
        {
           
            ConfigMainGrid();
            ConfigData();
            CargarDatosCombos();
            LoadDocumentos();


            DBSecurity.DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(ConnectionString.Security);

                DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
                dbmSecurity.Connection_Open(MiharuSession.Usuario.id);
                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.MiharuSession.Usuario.id, Program.Modulo, 200, this.MiharuSession.ClientIPAddress);
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }


            UpdateModo(int.Parse(ModoFindDropDownList.SelectedValue));
            GetTipologiasRequest();

        }

        private void GetTipologiasRequest()
        {
            var Metodo = Request["Metodo"];
            if (Metodo == null) return;

            switch (Metodo)
            {
                case "BuildTipologias": BuildTipologias();
                    break;
                case "GuardarTipologias": GuardarTipologias();
                    break;
            }
        }

        private void BuildTipologias()
        {
            var Esquema = Request["Esquema"];
            var idEsquema = Esquemas.GetRowByText(Esquema);

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);

                Tipologias.Init(dbmBanagrario.SchemaConfig.CTA_Documento_TX_Concatenacion.DBFindByfk_Entidadfk_Proyectofk_Esquema(Program.EntidadId, Program.ProyectoId, idEsquema.id_Esquema, 0, new DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnumList(DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnum.Codigo_Tx, true)), DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnum.Nombre_Documento);
                Response.Write("Cod:" + Tipologias.GetJson() + ":");

            }
            catch (Exception ex)
            {
                Program.TraceError(ex);
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally { if (dbmBanagrario != null) dbmBanagrario.Connection_Close(); }
        }

        protected override void Load_Data() { }
        
        protected void ConfigData()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);
                dbmCore = new DBCore.DBCoreDataBaseManager(ConnectionString.Core);
                dbmCore.Connection_Open(this.MiharuSession.Usuario.id);


                pTablaRegional = dbmBanagrario.SchemaConfig.TBL_Regional.DBGet(null, 0, new DBAgrario.SchemaConfig.TBL_RegionalEnumList(DBAgrario.SchemaConfig.TBL_RegionalEnum.Nombre_Regional, true));
                pTablaCOB = dbmBanagrario.SchemaConfig.TBL_COB.DBGet(null, 0, new DBAgrario.SchemaConfig.TBL_COBEnumList(DBAgrario.SchemaConfig.TBL_COBEnum.Nombre_COB, true));
                pTablaOficina = dbmBanagrario.SchemaConfig.CTA_Oficinas_Web.DBGet(0, new DBAgrario.SchemaConfig.CTA_Oficinas_WebEnumList(DBAgrario.SchemaConfig.CTA_Oficinas_WebEnum.Nombre_Oficina, true));

                Esquemas.Init(dbmCore.SchemaConfig.TBL_Esquema.DBGet(Program.EntidadId, Program.ProyectoId, null, 0, new DBCore.SchemaConfig.TBL_EsquemaEnumList(DBCore.SchemaConfig.TBL_EsquemaEnum.Nombre_Esquema, true)), DBCore.SchemaConfig.TBL_EsquemaEnum.Nombre_Esquema);
               
                dbmBanagrario.Connection_Close();
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
            }
        }

        private void Buscar()
        {
            try
            {
                MainGrid.DataSource.Rows.Clear();
                if (this.Validar()) {
                    Consultar();
                }
                        
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }
        
        protected void CargarDatosCombos()
        {
            CargarCombo(ref RegionalFindDropDownList, pTablaRegional, DBAgrario.SchemaConfig.TBL_RegionalEnum.id_Regional.ColumnName, DBAgrario.SchemaConfig.TBL_RegionalEnum.Nombre_Regional.ColumnName);
            CargarCombo(ref COBFindDropDownList, pTablaCOB, DBAgrario.SchemaConfig.TBL_COBEnum.id_COB.ColumnName, DBAgrario.SchemaConfig.TBL_COBEnum.Nombre_COB.ColumnName);
            CargarCombo(ref OficinaFindDropDownList, pTablaOficina, DBAgrario.SchemaConfig.TBL_OficinaEnum.id_Oficina.ColumnName, DBAgrario.SchemaConfig.TBL_OficinaEnum.Nombre_Oficina.ColumnName);
        }

        protected void CargarCombo(ref DropDownList ddl, DataTable tabla, string id, string nombre)
        {
            try
            {
                ddl.DataSource = tabla;
                ddl.DataValueField = id;
                ddl.DataTextField = nombre;
                ddl.DataBind();

                var _item = new ListItem {Value = (-1).ToString(), Text = "--Todos--"};
                ddl.Items.Insert(0, _item);
            }                                 
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        protected void Consultar()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);
                //dbmBanagrario.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                MainGrid.DataSource = dbmBanagrario.SchemaReport.PA_11_Soporte_Sobrante.DBExecute(short.Parse(RegionalFindDropDownList.SelectedValue),
                    short.Parse(COBFindDropDownList.SelectedValue),
                    int.Parse(OficinaFindDropDownList.SelectedValue),
                    this.FechaMovimientoInicialTextBox.Text,
                    this.FechaMovimientoFinalTextBox.Text,
                    this.FechaProcesoInicialTextBox.Text,
                    this.FechaProcesoFinalTextBox.Text,
                    int.Parse(this.ModoFindDropDownList.SelectedValue),
                    TipologiaDropDownList.SelectedValue,
                    this.txtValorUno.Text,
                    this.txtValorDos.Text
                    );

                //Registrar accion
                var query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, query, "", "");

                dbmBanagrario.Connection_Close();

                if (MainGrid.DataSource.Rows.Count == 0)
                    Master.ShowAlert("No se encontraron Soportes Sobrantes que cumplan con el críterio de Búsqueda", MsgBoxIcon.IconInformation);
                else
                    Master.SelectTab(Tabs.Filtro);
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        protected void GuardarTipologias()
        {

            var Esquema = Request["Esquema"];
            var Tipologia = Request["Tipologias"];

            if (!ValidarSave(Esquema, Tipologia)) return;

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            DBCore.DBCoreDataBaseManager dbmCore = null;
            DBImaging.DBImagingDataBaseManager dbmImaging = null;

            try
            {
                var Seleccionados = (DBAgrario.SchemaReport.CTA_11_Soporte_SobranteRow[])Session["Seleccionados"];

                if (Seleccionados.Length != 0)
                {

                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                    dbmCore = new DBCore.DBCoreDataBaseManager(ConnectionString.Core);
                    dbmImaging = new DBImaging.DBImagingDataBaseManager(ConnectionString.Imaging);

                    dbmCore.Connection_Open(this.MiharuSession.Usuario.id);
                    dbmImaging.Connection_Open(this.MiharuSession.Usuario.id);
                    dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);

                    //dbmBanagrario.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;


                    var DataEsquema = Esquemas.GetRowByText(Esquema);
                    var DataTipologia = Tipologias.GetRowByText(Tipologia);

                    foreach (var nRow in Seleccionados)
                    {
                        long id_Expediente = nRow.fk_Expediente;
                        short id_Folder = nRow.fk_Folder;
                        short id_File = short.Parse(nRow.fk_File.ToString());

                        //Vlr_Anterior
                        var Vlr_Anterior = dbmBanagrario.SchemaProcess.PA_Get_Detalle_Soporte.DBExecute(id_Expediente);

                        // Tipologia
                        var ExpedienteType = new DBCore.SchemaProcess.TBL_ExpedienteType { fk_Esquema = DataEsquema.id_Esquema };
                        dbmCore.SchemaProcess.TBL_Expediente.DBUpdate(ExpedienteType, id_Expediente);

                        var FileType = new DBCore.SchemaProcess.TBL_FileType {fk_Documento = DataTipologia.id_Documento};
                        dbmCore.SchemaProcess.TBL_File.DBUpdate(FileType, id_Expediente, id_Folder, id_File);

                        // Estado
                        var FileEstadoType = new DBCore.SchemaProcess.TBL_File_EstadoType { Fecha_Log = SlygNullable.SysDate, fk_Usuario = this.MiharuSession.Usuario.id, fk_Estado = (short)DBCore.EstadoEnum.Captura };

                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(FileEstadoType, id_Expediente, id_Folder, id_File,  (byte)DesktopConfig.Modulo.Imaging);

                        // Registrar Tx en Reproceso
                        dbmBanagrario.SchemaProcess.PA_Registrar_Reproceso.DBExecute(id_Expediente, id_Folder, id_File, this.MiharuSession.Usuario.id);

                        //Last_Query
                        var Query = dbmBanagrario.DataBase.LastQuery;

                        // Borrar data
                        dbmCore.SchemaProcess.TBL_File_Data.DBDelete(id_Expediente, id_Folder, id_File, null, null);
                        dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(id_Expediente, id_Folder, id_File, null, null, null, null);
                        dbmCore.SchemaProcess.TBL_File_Validacion.DBDelete(id_Expediente, id_Folder, id_File, null, FileType.fk_Documento);

                        // Solicita nuevamente validaciones opcionales
                        var ImagingFileType = new DBCore.SchemaImaging.TBL_FileType {Validaciones_Opcionales = false};
                        dbmCore.SchemaImaging.TBL_File.DBUpdate(ImagingFileType, id_Expediente, id_Folder, id_File, null);

                        //---------------------------------------------------------------------------
                        // Actualizar Dashboard
                        //---------------------------------------------------------------------------
                        var FolderDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(id_Expediente, id_Folder);

                        if (FolderDataTable.Count > 0)
                        {
                            var CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(FolderDataTable[0].fk_Cargue);
                            var PaqueteDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(FolderDataTable[0].fk_Cargue, FolderDataTable[0].fk_Cargue_Paquete);

                            // Actualizar capturas
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(id_Expediente, id_Folder, id_File);

                            var CapDashboardType = new DBImaging.SchemaProcess.TBL_Dashboard_CapturasType { fk_Expediente = id_Expediente, fk_Folder = id_Folder, fk_File = id_File, fk_Documento = DataTipologia.id_Documento, fk_Cargue = FolderDataTable[0].fk_Cargue, fk_Cargue_Paquete = FolderDataTable[0].fk_Cargue_Paquete, fk_Entidad_Procesamiento = CargueDataTable[0].fk_Entidad_Procesamiento, fk_Sede_Procesamiento = PaqueteDataTable[0].fk_Sede_Procesamiento_Asignada, fk_Centro_Procesamiento = PaqueteDataTable[0].fk_Centro_Procesamiento_Asignado, fk_Entidad = CargueDataTable[0].fk_Entidad, fk_Proyecto = CargueDataTable[0].fk_Proyecto, fk_Estado = (short)DBCore.EstadoEnum.Captura, fk_OT = CargueDataTable[0].fk_OT };
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(CapDashboardType);

                            // Actualizar validaciones opcionales
                            var ValidacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(DataTipologia.id_Documento, (byte)DBImaging.EnumEtapaCaptura.Opcional, 1, new DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, true));

                            if (ValidacionesDataTable.Count > 0)
                            {
                                dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(id_Expediente, id_Folder, id_File);

                                var ValDashboardType = new DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType { fk_Expediente = id_Expediente, fk_Folder = id_Folder, fk_File = id_File, fk_Documento = DataTipologia.id_Documento, fk_Cargue = FolderDataTable[0].fk_Cargue, fk_Cargue_Paquete = FolderDataTable[0].fk_Cargue_Paquete, fk_Entidad_Procesamiento = CargueDataTable[0].fk_Entidad_Procesamiento, fk_Sede_Procesamiento = PaqueteDataTable[0].fk_Sede_Procesamiento_Asignada, fk_Centro_Procesamiento = PaqueteDataTable[0].fk_Centro_Procesamiento_Asignado, fk_Entidad = CargueDataTable[0].fk_Entidad, fk_Proyecto = CargueDataTable[0].fk_Proyecto, Procesado = false };

                                dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(ValDashboardType);
                            }
                        }
                        //---------------------------------------------------------------------------

                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(id_Expediente, id_Folder, id_File, null, null, null);
                        dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(id_Expediente, id_Folder, id_File, null, null, null, null, null);
                        Master.SelectTab(Tabs.Filtro);

                        //Vlr_Despues
                        var Vlr_Despues = dbmBanagrario.SchemaProcess.PA_Get_Detalle_Soporte.DBExecute((int)id_Expediente);

                        //Registrar Accion
                        Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Actualizar, Path_Nodo, Query, Vlr_Anterior, Vlr_Despues);

                    }
                }
                else
                {
                    Response.Write("Message:" + "Debe seleccionar al menos un registro :");

                    return;
                }

                if (Seleccionados.Length == 1)
                    Response.Write("Message:" + "Se realizó el cambio de Tipología Exitosamente :");

                else
                    Response.Write("Message:" + "Se realizaron los cambios de Tipología Exitosamente :");


            }
            catch (Exception ex)
            {
                Response.Write("Message:" + "Ocurrió un error Durante El proceso de la Solicitud " + ex +
                               ":");
            }
            finally
            {
                if (dbmBanagrario != null)dbmBanagrario.Connection_Close();
                if (dbmCore != null) dbmCore.Connection_Close();
                if (dbmImaging != null) dbmImaging.Connection_Close();
            }
        }

        private void EditarSeleccionados()
        {
            if (MainGrid.DataSource.Rows.Count != 0)
           {
               var Seleccionados = MainGrid.GetSelectedRows();

               if (Seleccionados.Length > 0)
               {
                   Session["Seleccionados"] = Seleccionados;
                   Master.SelectTab(Tabs.Detalle);
               }
               else
                   Master.ShowAlert("Debe seleccionar por lo menos un registro de la grilla, o puede hacer soble click para editar un registro especifico", MsgBoxIcon.IconWarning);
           } 
           else
           {
               Master.ShowAlert("No hay Registros Para Editar", MsgBoxIcon.IconError);
           }
            
        }

        protected void ExportToWord()
        {
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);
            var table = new  GridView {DataSource = MainGrid.DataSource};
            table.DataBind();

            PrepareGridViewForExport(table);
            table.RenderControl(htw);

            this.MiharuSession.Pagina.Parameter["ContentName"] = "SoportesSobrantes.doc";
            this.MiharuSession.Pagina.Parameter["Content"] = sw.ToString();
            this.MiharuSession.Pagina.Parameter["ContentType"] = "application/vnd.ms-word";

            Master.ShowWindowNoBloqueo("../../Adjuntos/show_adjunto.aspx", "Exportar", "200", "100");
        }

        protected void ExportToExcel()
        {
            try
            {
                var Exportador = new Slyg.Tools.CSV.CSVData(";", "", true);
                var Flujo = new MemoryStream();

                Exportador.SaveEncoding = System.Text.Encoding.UTF32;

                var t = new Slyg.Tools.CSV.CSVTable(MainGrid.DataSource);

                t.Columns["fk_Core_Index"].ColumnTitle = "Id";
                t.Columns["Fecha_Proceso"].ColumnTitle = "Fecha Proceso PyC";
                t.Columns["Fecha_Movimiento"].ColumnTitle = "Fecha Movimiento BAC";
                t.Columns["Activo"].ColumnTitle = "Reproceso";
                t.Columns["Activo"].Export = false;
                t.Columns["Nombre_COB"].ColumnTitle = "COB"; 
                t.Columns["id_Oficina"].ColumnTitle = "Codigo Oficina";
                t.Columns["Nombre_Oficina"].ColumnTitle = "Nombre Oficina";
                t.Columns["Nombre_Oficina_Tipo"].ColumnTitle = "Tipo Oficina";
                t.Columns["Materializada"].ColumnTitle = "Tipo de Registro";
                t.Columns["Materializada_Original"].ColumnTitle = "Tipo de Registro PyC";
                t.Columns["Codigo_Tx"].ColumnTitle = "Codigo Transaccion";
                t.Columns["Nombre_Tx"].ColumnTitle = "Nombre Transaccion";
                t.Columns["Valor"].ColumnTitle = "Valor";
                t.Columns["Valor"].Format = "$ #,###0.00";
                t.Columns["Valor_Original"].ColumnTitle = "Valor PyC";
                t.Columns["Valor_Original"].Format = "$ #,###0.00";
                t.Columns["Producto"].ColumnTitle = "No. Producto PyC";
                t.Columns["Campo_Uno_Original"].ColumnTitle = "Campo 10 PyC";
                t.Columns["Campo_Diez_Original"].ColumnTitle = "No. Secuencial y/o Operacion PyC";
                t.Columns["Numero_Cuenta_Afectada_Original"].ColumnTitle = "Campo 30 PyC";
                t.Columns["Fecha_Ultimo_Proceso"].ColumnTitle = "Fecha Ajuste o Calificación Web";
                t.Columns["Comision"].ColumnTitle = "Comision";
                t.Columns["Comision"].Format = "$ #,###0.00";
                t.Columns["Comision_Original"].ColumnTitle = "Comision PyC";
                t.Columns["Comision_Original"].Format = "$ #,###0.00";
                t.Columns["Url"].ColumnTitle = "Imagen";
                t.Columns["Url_Link"].ColumnTitle = "Link Imagen";
                t.Columns["UrlAnexo"].ColumnTitle = "Anexo";
                t.Columns["UrlAnexo_Link"].ColumnTitle = "Link Anexo";
                
                t.Columns["En_Correccion"].ColumnTitle = "Cambiar";
                t.Columns["En_Correccion"].Export = false;
                
                t.Columns["En_Correccion_Txt"].ColumnTitle = "Reproceso";
                t.Columns["Observacion"].ColumnTitle = "Observaciones";
                t.Columns["Usuario_Ajuste"].ColumnTitle = "Funcionario Ajusta";
                t.Columns["Fecha_Ajuste"].ColumnTitle = "Fecha Ajuste";
                t.Columns["Usuario_Aprueba"].ColumnTitle = "Funcionario Aprueba";
                t.Columns["Fecha_Aprueba"].ColumnTitle = "Fecha Aprobacion";

                t.Columns["fk_Expediente"].ColumnTitle = "fk_Expediente";
                t.Columns["fk_Expediente"].Export = false;

                t.Columns["fk_Folder"].ColumnTitle = "fk_Folder";
                t.Columns["fk_Folder"].Export = false;

                t.Columns["fk_File"].ColumnTitle = "fk_File";
                t.Columns["fk_File"].Export = false;

                //Columnas que no son requeridas en la exportación
                t.Columns["id_COB"].ColumnTitle = "id_COB";
                t.Columns["id_COB"].Export = false;
                t.Columns["id_Regional"].ColumnTitle = "id_Regional";
                t.Columns["id_Regional"].Export = false;

                t.Columns["id_Oficina_Tipo"].ColumnTitle = "id_Oficina_Tipo";
                t.Columns["id_Oficina_Tipo"].Export = false;

                t.Columns["fk_Documento"].ColumnTitle = "fk_Documento";
                t.Columns["fk_Documento"].Export = false;

                t.Columns["Nombre_Regional"].ColumnTitle = "Regional";
                

                Exportador.SaveAsCSV(t, Flujo, true);

                this.MiharuSession.Pagina.Parameter["ContentName"] = "SoportesSobrantes.csv";
                this.MiharuSession.Pagina.Parameter["Content"] = Flujo.ToArray();
                this.MiharuSession.Pagina.Parameter["ContentType"] = "application/vnd.ms-excel";
                this.MiharuSession.Pagina.Parameter["Charset"] = "UTF-32";

                Master.ShowWindowNoBloqueo("../../Adjuntos/show_adjunto.aspx", "Exportar", "400", "200");

            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }

        }

        private void PrepareGridViewForExport(Control gv)
        {
            var l = new Literal();

            for (var i = 0; i < gv.Controls.Count - 1; i++)
            {

                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = ((LinkButton)gv.Controls[i]).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else
                {
                    if (gv.Controls[i].GetType() == typeof(DropDownList))
                    {
                        l.Text = ((DropDownList)gv.Controls[i]).SelectedItem.Text;
                        gv.Controls.Remove(gv.Controls[i]);
                        gv.Controls.AddAt(i, l);
                    }
                    else
                    {
                        if (gv.Controls[i].GetType() == typeof(CheckBox))
                        {
                            l.Text = (((CheckBox)gv.Controls[i]).Checked ? "true" : "false");
                            gv.Controls.Remove(gv.Controls[i]);
                            gv.Controls.AddAt(i, l);
                        }
                    }
                }
                if (gv.Controls[i].HasControls())
                    PrepareGridViewForExport(gv.Controls[i]);
            }
        }
      
        private void LoadDocumentos()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                //var DocumentosDataTable = dbmBanagrario.SchemaConfig.CTA_Documento_TX_Concatenacion.DBFindByfk_Entidadfk_Proyectofk_Esquema(Program.EntidadId,Program.ProyectoId, Program.EsquemaId, 0, new CTA_Documento_TX_ConcatenacionEnumList(CTA_Documento_TX_ConcatenacionEnum.Codigo_Tx, true));
                var DocumentosDataTable = dbmBanagrario.SchemaConfig.CTA_Documento_TX_Concatenacion.DBFindByfk_Entidad(Program.EntidadId, 0, new DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnumList(DBAgrario.SchemaConfig.CTA_Documento_TX_ConcatenacionEnum.Codigo_Tx, true));

                TipologiaDropDownList.Items.Clear();
                TipologiaDropDownList.Items.Add(new ListItem("- Todos -", "-1"));

                foreach (var DocumentoRow in DocumentosDataTable)
                {
                    TipologiaDropDownList.Items.Add(new ListItem(DocumentoRow.Nombre_Documento, DocumentoRow.id_Documento.ToString()));
                }

                TipologiaDropDownList.SelectedIndex = 0;
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        private void UpdateModo(int nModo)
        {
            switch (nModo)
            {
                case 1: // Movimiento                    
                    this.FechaMovimientoLabel.Visible = true;
                    this.FechaMovimientoInicialTextBox.Visible = true;
                    this.FechaMovimientoFinalTextBox.Visible = true;

                    this.FechaProcesoLabel.Visible = false;
                    this.FechaProcesoInicialTextBox.Visible = false;
                    this.FechaProcesoFinalTextBox.Visible = false;


                    break;

                case 2: // Proceso
                    this.FechaMovimientoLabel.Visible = false;
                    this.FechaMovimientoInicialTextBox.Visible = false;
                    this.FechaMovimientoFinalTextBox.Visible = false;

                    this.FechaProcesoLabel.Visible = true;
                    this.FechaProcesoInicialTextBox.Visible = true;
                    this.FechaProcesoFinalTextBox.Visible = true;

                    break;

                case 3: // Movimiento y Proceso
                    this.FechaMovimientoLabel.Visible = true;
                    this.FechaMovimientoInicialTextBox.Visible = true;
                    this.FechaMovimientoFinalTextBox.Visible = true;

                    this.FechaProcesoLabel.Visible = true;
                    this.FechaProcesoInicialTextBox.Visible = true;
                    this.FechaProcesoFinalTextBox.Visible = true;

                    break;
            }
        }

        #endregion

        #region Funciones

        //private byte[] StreamFile(string nFileName)
        //{
        //    using (var Fs = new FileStream(nFileName, FileMode.Open, FileAccess.Read))
        //    {
        //        var Contenido = new byte[Fs.Length];
        //        Fs.Read(Contenido, 0, (int)Fs.Length);
        //        return Contenido;
        //    }
        //}

        private bool ValidarSave( string Esquema, string Tipologia)
        {
            if (Esquema == "")
            {
                Response.Write("Message:" + "Debe seleccionar un esquema :");
                
            }
            else if (Tipologia == "")
                Response.Write("Message:" + "Debe seleccionar una tipología  :");
            else
                return true;

            return false;
        }

        private bool Validar()
        {
            if (this.ModoFindDropDownList.SelectedValue != "1")
            {
                var FechaInicial = DataConvert.ToDate(FechaProcesoInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/');
                var FechaFinal = DataConvert.ToDate(FechaProcesoFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/');

                if ((FechaFinal.Value - FechaInicial.Value).Days > 15)
                {
                    this.Master.ShowAlert("El rango de fechas de proceso debe ser menor o igual a 15 días", MsgBoxIcon.IconWarning);
                    return false;
                }
            }

            if (this.ModoFindDropDownList.SelectedValue != "2")
            {
                var FechaInicial = DataConvert.ToDate(FechaMovimientoInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/');
                var FechaFinal = DataConvert.ToDate(FechaMovimientoFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/');

                if ((FechaFinal.Value - FechaInicial.Value).Days > 15)
                {
                    this.Master.ShowAlert("El rango de fechas de movimiento debe ser menor o igual a 15 días", MsgBoxIcon.IconWarning);
                    return false;
                }
            }

            if (this.ModoFindDropDownList.SelectedValue != "2" && !DataConvert.IsDate(this.FechaMovimientoInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "2" && !DataConvert.IsDate(this.FechaMovimientoFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento final debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "1" && !DataConvert.IsDate(this.FechaProcesoInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de proceso inicial debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "1" && !DataConvert.IsDate(this.FechaProcesoFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de proceso final debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "2" && DataConvert.ToDate(this.FechaMovimientoInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/').Value > DataConvert.ToDate(this.FechaMovimientoFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/').Value)
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe ser inferior a la fecha de movimiento final", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "1" && DataConvert.ToDate(this.FechaProcesoInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/').Value > DataConvert.ToDate(this.FechaProcesoFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/').Value)
            {
                this.Master.ShowAlert("La fecha de proceso inicial debe ser inferior a la fecha de proceso final", MsgBoxIcon.IconWarning);
            }
            else
            {
                return true;
            }
            return false;
        }

        public string GetTipologiasJsonData()
        {
                

            Response.Write("Esquemas_InputData=" + Esquemas.GetJson() + ";");
            Response.Write("UrlVisorImg=\"" + UrlVisorImagen + "\";");

            return "";
        }

        #endregion
    }
}
