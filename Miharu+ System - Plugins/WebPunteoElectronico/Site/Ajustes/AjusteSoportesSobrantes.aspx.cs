using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using Slyg.Tools;
using System.Web.UI.WebControls;
using DBAgrario;
using DBAgrario.SchemaConfig;
using DBAgrario.SchemaProcess;
using DBSecurity;
using WebPunteoElectronico.Clases;
using WebPunteoElectronico.Clases.Slyg;
using WebPunteoElectronico.Master;

namespace WebPunteoElectronico.Site.Ajustes
{
    public partial class AjusteSoportesSobrantes : FormBase
    {
        #region Declaraciones

        public string Query = "";
        public string Path_Nodo = "4.1";
        public Tipo_Accion_Log TipoAccion;

        #endregion

        #region   Propiedades

        public new Master.MasterConfig Master
        {
            get { return (Master.MasterConfig) base.OriginalMaster; }
        }

        protected CTA_Ajuste_Adjunto_AjusteDataTable pTablaAdjuntos
        {
            get
            {
                if (this.MiharuSession.Parameter["pTablaAdjuntos"] == null)
                    this.MiharuSession.Parameter["pTablaAdjuntos"] = new CTA_Ajuste_Adjunto_AjusteDataTable();

                return (CTA_Ajuste_Adjunto_AjusteDataTable) this.MiharuSession.Parameter["pTablaAdjuntos"];
            }
            set { this.MiharuSession.Parameter["pTablaAdjuntos"] = value; }

        }

        public TBL_RegionalDataTable pTablaRegional
        {
            get { return (TBL_RegionalDataTable) this.MiharuSession.Pagina.Parameter["_RegionalDataTable"]; }
            set { this.MiharuSession.Pagina.Parameter["_RegionalDataTable"] = value; }
        }

        public TBL_COBDataTable pTablaCOB
        {
            get { return (TBL_COBDataTable) this.MiharuSession.Pagina.Parameter["_COBDataTable"]; }
            set { this.MiharuSession.Pagina.Parameter["_COBDataTable"] = value; }
        }

        public TBL_OficinaDataTable pTablaOficina
        {
            get { return (TBL_OficinaDataTable) this.MiharuSession.Pagina.Parameter["_OficinaDataTable"]; }
            set { this.MiharuSession.Pagina.Parameter["_OficinaDataTable"] = value; }
        }

        public TBL_Ajuste_ObservacionDataTable pTablaObservacion
        {
            get { return (TBL_Ajuste_ObservacionDataTable) this.MiharuSession.Pagina.Parameter["pTablaObservacion"]; }
            set { this.MiharuSession.Pagina.Parameter["pTablaObservacion"] = value; }
        }

        #endregion

        #region  Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            tdControlAjustar.Visible = (MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajuste_Soportes_Sobrantes_Ajustar) || MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajuste_Soportes_Sobrantes_Reabrir));
            tdControlAprobar.Visible = (MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajuste_Soportes_Sobrantes_Aprobar));
            tdControlRechazar.Visible = (MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajuste_Soportes_Sobrantes_Reabrir));

            if (!this.IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Master.OnSelectedGridChanged += Grid_OnSelectedGridChanged;
            this.ConsultarLinkButton.Click += new EventHandler(ConsultarLinkButton_Click);
            //this.lnkAjustar.Click += new EventHandler(lnkAjustar_Click);
            //this.lnkAprobar.Click += new EventHandler(lnkAprobar_Click);
            //this.lnkRechazar.Click += new EventHandler(lnkRechazar_Click);
            this.RegionalFindDropDownList.SelectedIndexChanged += new EventHandler(RegionalFindDropDownList_SelectedIndexChanged);
            this.COBFindDropDownList.SelectedIndexChanged += new EventHandler(COBFindDropDownList_SelectedIndexChanged);
            this.EditarSeleccionadosButton.Click += new EventHandler(EditarSeleccionadosButton_Click);
            this.VerImagenButton.Click += new EventHandler(VerImagenButton_Click);
            this.VerAnexoButton.Click += new EventHandler(VerAnexoButton_Click);
            this.ExportWordButton.Click += new ImageClickEventHandler(ExportWordButton_Click);
            this.ExportExcelButton0.Click += new ImageClickEventHandler(ExportExcelButton0_Click);
            this.AdjuntarButton.Click += new EventHandler(AdjuntarButton_Click);
            this.AdjuntosDataGrid.SelectedIndexChanged += new EventHandler(AdjuntosDataGrid_SelectedIndexChanged);
            this.Master.DaughterClose += new DaughterClose_Delegate(AjusteSoportesSobrantes_HijaClose);

            this.Master.Title = "Ajuste de soportes sobrantes";
        }

        private void ConfigMainGrid()
        {
            MainGrid.Initialize(new CTA_Detalle_Ajuste_Soporte_SobranteDataTable());
            MainGrid.IsMultiCheck = true;
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_Error.ColumnName, Header = "ID Error", IsColumnID = true, Width = 50});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.fk_Core_Index.ColumnName, Header = "ID Tx", Width = 50});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Fecha_Movimiento.ColumnName, Header = "Fecha movimiento", Width = 90});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Fecha_Proceso.ColumnName, Header = "Fecha proceso", Width = 90});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Fecha_Ultimo_Proceso.ColumnName, Header = "Fecha Ajuste o Calificación Web", Width = 140});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_Regional.ColumnName, Header = "Codigo Regional", Ignore = true});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Nombre_Regional.ColumnName, Header = "Regional", Width = 80});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_COB.ColumnName, Header = "Codigo COB", Ignore = true});
            
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Nombre_COB.ColumnName, Header = "COB", Width = 80});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.fk_Oficina.ColumnName, Header = "Oficina", Width = 60});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Nombre_Oficina.ColumnName, Header = "Nombre Oficina", Width = 160});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Tipo_Oficina.ColumnName, Header = "Tipo Oficina", Width = 60});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Codigo_Tx.ColumnName, Header = "Codigo Tx", Width = 60});
            
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Nombre_Transaccion.ColumnName, Header = "Nombre Tx", Width = 250});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Materializada_Original.ColumnName, Header = "Tipo de Registro PyC", Width = 70});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Valor_Transaccion.ColumnName, Header = "Valor PyC", Width = 120});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Comision_Original.ColumnName, Header = "Comisión PyC", Width = 70});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Campo_Diez_Original.ColumnName, Header = "No. Secuencial y/o Operación PyC", Width = 130});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Producto.ColumnName, Header = "No. Producto PyC", Width = 130});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Campo_Uno_Original.ColumnName, Header = "Campo 10 PyC", Width = 130});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Numero_Cuenta_Afectada_Original.ColumnName, Header = "Campo 30 PyC", Width = 130});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Url.ColumnName, Header = "Link Imagen", Width = 80});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Url_Anexo.ColumnName, Header = "Link Anexo", Width = 80});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Ajustado.ColumnName, Header = "Ajustado", Width = 40});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_Ajuste.ColumnName, Header = "Id Ajuste", Width = 40, Ignore = true});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Fecha_Ultimo_Ajuste.ColumnName, Header = "Fecha Ajuste", Width = 90});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Nombre_Estado_Ajuste.ColumnName, Header = "Estado", Width = 90});

            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_Usuario.ColumnName, Ignore = true});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Usuario.ColumnName, Header = "Usuario", Width = 90});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Activo.ColumnName, Ignore = true});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_Estado_Ajuste.ColumnName, Ignore = true});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Valor_Proceso.ColumnName, Header = "Valor Proceso", Width = 90});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Ajusta.ColumnName, Header = "Ajusta", Width = 90});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.Observacion_Ajuste.ColumnName, Header = "Observacion", Width = 200});
            MainGrid.ColModel.Add(new FlexColumnMap() {ColumnName = CTA_Detalle_Ajuste_Soporte_SobranteEnum.fk_Ajuste_Observacion.ColumnName, Ignore = true});
        }

        protected void ConsultarLinkButton_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        protected void lnkAjustar_Click(object sender, EventArgs e)
        {
            Guardar(EstadoAjusteEnum.Ingresado_sin_aprobacion);
        }

        protected void lnkAprobar_Click(object sender, EventArgs e)
        {
            Guardar(EstadoAjusteEnum.Aprobado);
        }

        protected void lnkRechazar_Click(object sender, EventArgs e)
        {
            Guardar(EstadoAjusteEnum.Rechazado);
        }

        protected void Grid_OnSelectedGridChanged(string nSender, string nValue, CellData nCellData)
        {
            try
            {
                if (nSender == MainGrid.ID)
                {
                    pTablaAdjuntos.Rows.Clear();
                    CargarDetalle(Convert.ToInt32(nValue));
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
            if (RegionalFindDropDownList.SelectedValue == "-1") return;
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {

                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);

                DataTable data_Tx = dbmBanagrario.SchemaConfig.TBL_COB.DBFindByfk_Regional((short.Parse(RegionalFindDropDownList.SelectedValue)));

                dbmBanagrario.Connection_Close();

                if (data_Tx.Rows.Count > 0)
                    CargarCombo(ref COBFindDropDownList, data_Tx, TBL_COBEnum.id_COB.ColumnName, TBL_COBEnum.Nombre_COB.ColumnName);

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

        protected void COBFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (COBFindDropDownList.SelectedValue == "-1") return;
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            try
            {

                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);


                DataTable data_Tx = dbmBanagrario.SchemaConfig.CTA_Oficinas_Web.DBFindByfk_COB(short.Parse(COBFindDropDownList.SelectedValue), 0, new CTA_Oficinas_WebEnumList(CTA_Oficinas_WebEnum.Nombre_Oficina, true));

                dbmBanagrario.Connection_Close();

                if (data_Tx.Rows.Count > 0)
                    CargarCombo(ref OficinaFindDropDownList, data_Tx, TBL_OficinaEnum.id_Oficina.ColumnName, TBL_OficinaEnum.Nombre_Oficina.ColumnName);

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

        protected void VerImagenButton_Click(object sender, EventArgs e)
        {
            VerImagen();
        }

        protected void VerAnexoButton_Click(object sender, EventArgs e)
        {
            VerAnexo();
        }

        protected void ExportWordButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportToWord();
        }

        protected void ExportExcelButton0_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportToExcel();
        }

        protected void AdjuntarButton_Click(object sender, EventArgs e)
        {
            Adjuntar();
        }

        protected void AdjuntosDataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id_Adjunto_Ajuste = AdjuntosDataGrid.SelectedItem.Cells[1].Text;
            var adj = (CTA_Ajuste_Adjunto_AjusteRow[]) pTablaAdjuntos.Select(CTA_Ajuste_Adjunto_AjusteEnum.fk_Ajuste_Adjunto.ColumnName + "=" + id_Adjunto_Ajuste);

            if (adj.Length > 0)
            {
                this.MiharuSession.Pagina.Parameter["Content"] = adj[0].Contenido_Adjunto;
                Master.ShowWindowNoBloqueo("../Adjuntos/show_adjunto.aspx?id_Adjunto_Ajuste=" + id_Adjunto_Ajuste + "&NombreArchivo=" + adj[0].Nombre_Archivo + "&ContentType=" + Path.GetExtension(adj[0].Nombre_Archivo), "Exportar", "200", "100");
            }
        }

        private void AjusteSoportesSobrantes_HijaClose()
        {
            MostrarAdjuntos();
            Master.SelectTab(Tabs.Detalle);
        }

        #endregion

        #region  Metodos

        protected override void Config_Page()
        {
            pTablaAdjuntos.Rows.Clear();

            ConfigMainGrid();
            ConfigData();
            CargarDatosCombos();

            FechaInicialTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-', '/');
            FechaFinalTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-', '/');

            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(ConnectionString.Security);

                DBSecurityDataBaseManager.IdentifierDateFormat = Program.IdentifierDateFormat;
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
        }

        protected override void Load_Data()
        {
        }

        protected void ConfigData()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);

                pTablaRegional = dbmBanagrario.SchemaConfig.TBL_Regional.DBGet(null, 0, new TBL_RegionalEnumList(TBL_RegionalEnum.Nombre_Regional, true));
                pTablaCOB = dbmBanagrario.SchemaConfig.TBL_COB.DBGet(null, 0, new TBL_COBEnumList(TBL_COBEnum.Nombre_COB, true));
                pTablaOficina = dbmBanagrario.SchemaConfig.TBL_Oficina.DBGet(null, 0, new TBL_OficinaEnumList(TBL_OficinaEnum.Nombre_Oficina, true));
                pTablaObservacion = dbmBanagrario.SchemaConfig.TBL_Ajuste_Observacion.DBFindByActiva(true);

                dbmBanagrario.Connection_Close();
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

        private void Buscar()
        {
            try
            {
                MainGrid.DataSource.Rows.Clear();
                DateTime FechaInicial = DateTime.Parse(FechaInicialTextBox.Text);
                DateTime FechaFinal = DateTime.Parse(FechaFinalTextBox.Text);

                if (FechaInicial > FechaFinal)
                    Master.ShowAlert("La fecha inicial debe ser menor o igual a la fecha final", MsgBoxIcon.IconWarning);
                else if (Math.Abs((FechaInicial - FechaFinal).Days) > 15)
                    Master.ShowAlert("El rango de Fechas debe ser menor o igual a 15 días", MsgBoxIcon.IconWarning);
                else
                    Consultar();
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        protected void CargarDatosCombos()
        {
            CargarCombo(ref RegionalFindDropDownList, pTablaRegional, TBL_RegionalEnum.id_Regional.ColumnName, TBL_RegionalEnum.Nombre_Regional.ColumnName);
            CargarCombo(ref COBFindDropDownList, pTablaCOB, TBL_COBEnum.id_COB.ColumnName, TBL_COBEnum.Nombre_COB.ColumnName);
            CargarCombo(ref OficinaFindDropDownList, pTablaOficina, TBL_OficinaEnum.id_Oficina.ColumnName, TBL_OficinaEnum.Nombre_Oficina.ColumnName);
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
                
                MainGrid.DataSource = dbmBanagrario.SchemaProcess.PA_Detalle_Ultimo_Ajuste_Soporte_Sobrante.DBExecute(
                    short.Parse(RegionalFindDropDownList.SelectedValue),
                    short.Parse(COBFindDropDownList.SelectedValue),
                    OficinaFindDropDownList.SelectedValue,
                    Slyg.Tools.DataConvert.ToDate(FechaInicialTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/'),
                    Slyg.Tools.DataConvert.ToDate(FechaFinalTextBox.Text, DataConvert.EnumDateFormat.yyyyMMdd, '/')
                    );

                //Registrar Accion
                Query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, Query, "", "");


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

        protected void CargarDetalle(int nIdError)
        {
            this.Master.ScriptBag.Append("ShowObservacion = '1';");
            DBAgrarioDataBaseManager DBBanagrario = null;

            try
            {
                DBBanagrario = new DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                
                var errorData = (CTA_Detalle_Ajuste_Soporte_SobranteRow[]) MainGrid.DataSource.Select(CTA_Detalle_Ajuste_Soporte_SobranteEnum.id_Error.ColumnName + "=" + nIdError.ToString());

                var e = errorData[0];
                MainGrid.SetSelectedRows(e);

                this.MiharuSession.Parameter["Seleccionados"] = MainGrid.GetSelectedRows();

                lblDataFechaP.Text = e.Fecha_Movimiento;
                lblDataOficina.Text = e.fk_Oficina + " -- " + e.Nombre_Oficina;
                lblDataTx.Text = e.Codigo_Tx + " -- " + e.Nombre_Transaccion;
                lblDataValor.Text = "$ " + e.Valor_Transaccion;

                if (e.id_Estado_Ajuste == (short) Estado_AjusteEnum.Aprobado)
                {
                    this.Master.ScriptBag.Append("ShowObservacion = '0';");
                    txtObservacion.Enabled = false;
                    btnNuevo.Disabled = true;
                    btnEditar.Disabled = true;
                    btnInactivar.Disabled = true;
                }
                else
                {
                    btnNuevo.Disabled = false;
                    btnEditar.Disabled = false;
                    btnInactivar.Disabled = false;
                }

                txtObservacion.Text = "";
                rbtAjuste.Items[0].Selected = false;
                rbtAjuste.Items[1].Selected = false;

                DBBanagrario.Connection_Open(MiharuSession.Usuario.id);

                pTablaAdjuntos.Rows.Clear();
                if (!e.Isid_AjusteNull())
                {
                    pTablaAdjuntos = DBBanagrario.SchemaProcess.CTA_Ajuste_Adjunto_Ajuste.DBFindByfk_Errorfk_Ajuste(e.id_Error, null);
                }

                MostrarAdjuntos();

                DBBanagrario.Connection_Close();

                if (!e.IsAjustaNull())
                {
                    if (e.Ajusta)
                        rbtAjuste.Items[0].Selected = true;
                    else
                        rbtAjuste.Items[1].Selected = true;
                }

                if (e.Isfk_Ajuste_ObservacionNull())
                    txtObservacion.Text = "";
                else
                    txtObservacion.Text = e.fk_Ajuste_Observacion + "|" + (e.IsObservacion_AjusteNull() ? "" : e.Observacion_Ajuste);
                
                VerAnexoButton.Visible = !e.IsUrl_AnexoNull();
                
                VerImagenButton.Enabled = true;
                Master.SelectTab(Tabs.Detalle);
            }            
            finally
            {
                if (DBBanagrario != null) DBBanagrario.Connection_Close();
            }
        }

        protected void CargarDetalleMultiple()
        {
            this.Master.ScriptBag.Append("ShowObservacion = '1';");
            btnNuevo.Disabled = false;
            btnEditar.Disabled = false;
            btnInactivar.Disabled = false;

            var Seleccionados = (CTA_Detalle_Ajuste_Soporte_SobranteRow[]) MainGrid.GetSelectedRows();
            txtObservacion.Text = "";

            foreach (var nRow in Seleccionados)
            {
                var obs = "";
                if (!nRow.Isfk_Ajuste_ObservacionNull())
                    obs = nRow.fk_Ajuste_Observacion + "|" + (nRow.IsObservacion_AjusteNull() ? "" : nRow.Observacion_Ajuste);
                
                if (txtObservacion.Text == "")
                    txtObservacion.Text = obs;
                else
                {
                    if (txtObservacion.Text != obs)
                    {
                        txtObservacion.Text = "";
                        break;
                    }
                }
            }

            foreach (var nRow in Seleccionados)
            {
                if (!nRow.Isfk_Ajuste_ObservacionNull())
                {
                    if (nRow.id_Estado_Ajuste == (short) Estado_AjusteEnum.Aprobado)
                    {
                        this.Master.ScriptBag.Append("ShowObservacion = '0';");
                        txtObservacion.Enabled = false;
                        btnNuevo.Disabled = true;
                        btnEditar.Disabled = true;
                        btnInactivar.Disabled = true;
                        break;
                    }
                }
            }

            lblDataFechaP.Text = "(Valores multiples)";
            lblDataOficina.Text = "(Valores multiples)";
            lblDataTx.Text = "(Valores multiples)";
            lblDataValor.Text = "(Valores multiples)";

            rbtAjuste.Items[0].Selected = false;
            rbtAjuste.Items[1].Selected = false;

            pTablaAdjuntos.Rows.Clear();
            MostrarAdjuntos();
            VerImagenButton.Enabled = false;
            VerAnexoButton.Visible = false;

            Master.SelectTab(Tabs.Detalle);

            this.MiharuSession.Parameter["Seleccionados"] = Seleccionados;
        }

        protected void Guardar(EstadoAjusteEnum nEstadoAjuste)
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;
            var texto_Observacion = "";
            var fk_Ajuste_Observacion = 0;
            try
            {

                var Seleccionados = (CTA_Detalle_Ajuste_Soporte_SobranteRow[]) this.MiharuSession.Parameter["Seleccionados"];

                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(MiharuSession.Usuario.id);
                //dbmBanagrario.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                dbmBanagrario.Transaction_Begin();
                if (Seleccionados.Length != 0)
                {
                    var Ajusta = rbtAjuste.Items[0].Selected;

                    var nuevosAdjuntos = new List<Int64>();

                    if (pTablaAdjuntos.Rows.Count > 0)
                        nuevosAdjuntos = GuardarAdjuntos(dbmBanagrario);

                    if (nEstadoAjuste != EstadoAjusteEnum.Aprobado)
                    {
                        if (txtObservacion.Text == "") throw new ApplicationException("Primero debe seleccionar una observación para el ajuste");
                        var obsParts = txtObservacion.Text.Split('|');
                        if (obsParts.Length != 2) throw new ApplicationException("La observación seleccionada no es válida");
                        fk_Ajuste_Observacion = Convert.ToInt32(obsParts[0]);
                        texto_Observacion = txtObservacion.Text.Remove(0, fk_Ajuste_Observacion.ToString().Length + 1);
                    }


                    foreach (var nRow in Seleccionados)
                    {
                        if (nRow.id_Estado_Ajuste == 0 && nEstadoAjuste >= EstadoAjusteEnum.Rechazado)
                            throw new Exception("No se permite aprobar o rechazar un ajuste directamente, primero se debe registrar el ajuste para que pueda ser aprobado");

                        if (nRow.id_Estado_Ajuste != 0 && nRow.id_Estado_Ajuste >= (short) EstadoAjusteEnum.Rechazado && nEstadoAjuste == EstadoAjusteEnum.Ingresado_sin_aprobacion)
                            nEstadoAjuste = EstadoAjusteEnum.Reapertura_sin_aprobacion;

                        if (nEstadoAjuste == EstadoAjusteEnum.Reapertura_sin_aprobacion & !this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajuste_Soportes_Sobrantes_Reabrir))
                            throw new Exception("El usuario actual no tiene permisos para reabrir ajustes ");

                        //Obtener Vlr_Antes
                        var Vlr_Antes = dbmBanagrario.SchemaProcess.PA_Get_Detalle_Ajustes.DBExecute((int) nRow.id_Error);

                        if (nEstadoAjuste == EstadoAjusteEnum.Ingresado_sin_aprobacion)
                        {
                            TipoAccion = Tipo_Accion_Log.Crear;
                            Vlr_Antes = "";
                        }
                        else
                            TipoAccion = Tipo_Accion_Log.Actualizar;


                        nRow.Ajusta = Ajusta;
                        if (nEstadoAjuste != EstadoAjusteEnum.Aprobado)
                        {
                            nRow.Observacion_Ajuste = texto_Observacion;
                        }
                        else
                        {
                            fk_Ajuste_Observacion = nRow.fk_Ajuste_Observacion;
                        }
                        nRow.id_Estado_Ajuste = (short) nEstadoAjuste;
                        nRow.Nombre_Estado_Ajuste = EstadoAjusteHelper.GetTexto(nEstadoAjuste);

                        var idAjuste = dbmBanagrario.SchemaProcess.PA_Guardar_Ajuste.DBExecute(
                            nRow.id_Error,
                            nRow.Observacion_Ajuste,
                            nRow.Ajusta,
                            false,
                            (short) nEstadoAjuste,
                            fk_Ajuste_Observacion,
                            this.MiharuSession.Usuario.id
                            );
                        //Accion realizada
                        Query = dbmBanagrario.DataBase.LastQuery;

                        //Obtener Vlr_Despues
                        var Vlr_Despues = dbmBanagrario.SchemaProcess.PA_Get_Detalle_Ajustes.DBExecute((int) nRow.id_Error);

                        //Registrar accion
                        Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), TipoAccion, Path_Nodo, Query, Vlr_Antes, Vlr_Despues);


                        foreach (var adj in nuevosAdjuntos)
                        {
                            dbmBanagrario.SchemaProcess.TBL_Ajuste_Ajunto_Ajuste.DBInsert(
                                new TBL_Ajuste_Ajunto_AjusteType() {fk_Error = nRow.id_Error, fk_Ajuste = idAjuste, fk_Ajuste_Adjunto = adj});
                        }
                    }
                }
                else
                {
                    Master.ShowAlert("Debe seleccionar un registro", MsgBoxIcon.IconInformation);
                    return;
                }

                Master.ShowAlert(Seleccionados.Length == 1 ? "El Ajuste fue guardado Exitosamente" : "Ajustes guardados Exitosamente", MsgBoxIcon.IconInformation);

                LimpiarControles();
                dbmBanagrario.Transaction_Commit();
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconInformation);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        private void VerImagen()
        {
            var Seleccionados = (CTA_Detalle_Ajuste_Soporte_SobranteRow[]) this.MiharuSession.Parameter["Seleccionados"];

            if (Seleccionados.Length != 0)
            {
                var Path = System.Configuration.ConfigurationManager.AppSettings["VisorImagen"];

                if (Path == "")
                    Path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/_sitio/verImagen.aspx";

                if (!Seleccionados[0].IsUrlNull())
                    Master.ScriptBag.AppendAndEncodeScript("window.open('" + Seleccionados[0].Url + "','_blank', 'width=1000,height=650')");
                else
                    Master.ShowAlert("La imagen asociada al soporte no se encuentra dispobible en el momento", MsgBoxIcon.IconWarning);
            }
            else
                Master.ShowAlert("Debe seleccionar un registro", MsgBoxIcon.IconInformation);

        }

        private void VerAnexo()
        {
            var Seleccionados = (CTA_Detalle_Ajuste_Soporte_SobranteRow[]) this.MiharuSession.Parameter["Seleccionados"];

            if (Seleccionados.Length != 0)
            {
                var Path = System.Configuration.ConfigurationManager.AppSettings["VisorImagen"];

                if (Path == "")
                    Path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/_sitio/verImagen.aspx";

                if (!Seleccionados[0].IsUrl_AnexoNull())
                    Master.ScriptBag.AppendAndEncodeScript("window.open('" + Seleccionados[0].Url_Anexo + "','_blank', 'width=1000,height=650')");
                else
                    Master.ShowAlert("La imagen asociada al soporte no se encuentra dispobible en el momento", MsgBoxIcon.IconWarning);
            }
            else
                Master.ShowAlert("Debe seleccionar un registro", MsgBoxIcon.IconInformation);

        }

        protected void LimpiarControles()
        {
            lblDataOficina.Text = "";
            lblDataFechaP.Text = "";
            lblDataTx.Text = "";
            lblDataValor.Text = "";
            txtObservacion.Text = "";
            rbtAjuste.Items[0].Selected = false;
            rbtAjuste.Items[1].Selected = false;

            pTablaAdjuntos.Rows.Clear();
            MostrarAdjuntos();
        }

        private void EditarSeleccionados()
        {
            pTablaAdjuntos.Rows.Clear();
            var Seleccionados = (CTA_Detalle_Ajuste_Soporte_SobranteRow[]) MainGrid.GetSelectedRows();

            if (Seleccionados.Length == 1)
                CargarDetalle((int) Seleccionados[0].id_Error);
            else
            {
                if (Seleccionados.Length > 0)
                    CargarDetalleMultiple();
                else
                    Master.ShowAlert("Debe seleccionar por lo menos un registro de la grilla, o puede hacer soble click para editar un registro especifico", MsgBoxIcon.IconError);
            }
        }

        protected void ExportToWord()
        {
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);
            var table = new GridView {DataSource = MainGrid.DataSource};
            table.DataBind();

            PrepareGridViewForExport(table);
            table.RenderControl(htw);

            this.MiharuSession.Pagina.Parameter["ContentName"] = "AjusteSoportesSobrantes.doc";
            this.MiharuSession.Pagina.Parameter["Content"] = sw.ToString();
            this.MiharuSession.Pagina.Parameter["ContentType"] = "application/vnd.ms-word";

            Master.ShowWindowNoBloqueo("../Adjuntos/show_adjunto.aspx", "Exportar", "200", "100");
        }

        protected void ExportToExcel()
        {
            try
            {
                var Exportador = new Slyg.Tools.CSV.CSVData(";", "", true);
                var Flujo = new MemoryStream();

                Exportador.SaveEncoding = System.Text.Encoding.UTF32;

                var t = new Slyg.Tools.CSV.CSVTable(MainGrid.DataSource);

                t.Columns["id_Error"].ColumnTitle = "Id Error";
                t.Columns["fk_Core_Index"].ColumnTitle = "Id";
                t.Columns["Fecha_Proceso"].ColumnTitle = "Fecha Proceso P&C";
                t.Columns["Fecha_Movimiento"].ColumnTitle = "Fecha Movimiento BAC";
                t.Columns["Activo"].ColumnTitle = "Reproceso";
                t.Columns["Activo"].Export = false;
                t.Columns["Nombre_COB"].ColumnTitle = "COB";
                t.Columns["Nombre_Oficina"].ColumnTitle = "Nombre Oficina";
                t.Columns["Tipo_Oficina"].ColumnTitle = "Tipo Oficina";
                t.Columns["Materializada_Original"].ColumnTitle = "Tipo de Registro PyC";
                t.Columns["Codigo_Tx"].ColumnTitle = "Codigo Tx";
                t.Columns["Nombre_Transaccion"].ColumnTitle = "Nombre Tx";
                t.Columns["Producto"].ColumnTitle = "No. Producto PyC";
                t.Columns["Campo_Uno_Original"].ColumnTitle = "Campo 10 PyC";
                t.Columns["Campo_Diez_Original"].ColumnTitle = "No. Secuencial y/o Operacion PyC";
                t.Columns["Numero_Cuenta_Afectada_Original"].ColumnTitle = "Campo 30 PyC";
                t.Columns["Fecha_Ultimo_Proceso"].ColumnTitle = "Fecha Ajuste o Calificación Web";
                t.Columns["Comision_Original"].ColumnTitle = "Comision PyC";
                t.Columns["Comision_Original"].Format = "$ #,###0.00";
                t.Columns["Url"].ColumnTitle = "Imagen";
                t.Columns["Url_Anexo"].ColumnTitle = "Anexo";
                t.Columns["Nombre_Regional"].ColumnTitle = "Regional";
                t.Columns["fk_Oficina"].ColumnTitle = "Oficina";
                t.Columns["Valor_Transaccion"].ColumnTitle = "Valor PyC";
                t.Columns["Valor_Transaccion"].Format = "$ #,###0.00";
                t.Columns["Ajustado"].ColumnTitle = "Ajustado";
                t.Columns["Fecha_Ultimo_Ajuste"].ColumnTitle = "Fecha Ajuste";
                t.Columns["Nombre_Estado_Ajuste"].ColumnTitle = "Estado";
                t.Columns["Usuario"].ColumnTitle = "Usuario";
                t.Columns["Valor_Proceso"].ColumnTitle = "Valor Proceso";
                t.Columns["Ajusta"].ColumnTitle = "Ajusta";
                t.Columns["Observacion_Ajuste"].ColumnTitle = "Observacion";

                //Columnas que no son requeridas en la exportación
                t.Columns["id_COB"].ColumnTitle = "id_COB";
                t.Columns["id_COB"].Export = false;
                t.Columns["id_Regional"].ColumnTitle = "id_Regional";
                t.Columns["id_Regional"].Export = false;
                t.Columns["id_Ajuste"].ColumnTitle = "Id Ajuste";
                t.Columns["id_Ajuste"].Export = false;
                t.Columns["id_Estado_Ajuste"].ColumnTitle = "id_Estado_Ajuste";
                t.Columns["id_Estado_Ajuste"].Export = false;
                t.Columns["fk_Ajuste_Observacion"].ColumnTitle = "fk_Ajuste_Observacion";
                t.Columns["fk_Ajuste_Observacion"].Export = false;
                t.Columns["id_Usuario"].ColumnTitle = "id_Usuario";
                t.Columns["id_Usuario"].Export = false;

                Exportador.SaveAsCSV(t, Flujo, true);

                this.MiharuSession.Pagina.Parameter["ContentName"] = "AjusteSoportesSobrantes.csv";
                this.MiharuSession.Pagina.Parameter["Content"] = Flujo.ToArray();
                this.MiharuSession.Pagina.Parameter["ContentType"] = "application/vnd.ms-excel";
                this.MiharuSession.Pagina.Parameter["Charset"] = "UTF-32";

                Master.ShowWindowNoBloqueo("../Adjuntos/show_adjunto.aspx", "Exportar", "400", "200");

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

                if (gv.Controls[i].GetType() == typeof (LinkButton))
                {
                    l.Text = ((LinkButton) gv.Controls[i]).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else
                {
                    if (gv.Controls[i].GetType() == typeof (DropDownList))
                    {
                        l.Text = ((DropDownList) gv.Controls[i]).SelectedItem.Text;
                        gv.Controls.Remove(gv.Controls[i]);
                        gv.Controls.AddAt(i, l);
                    }
                    else
                    {
                        if (gv.Controls[i].GetType() == typeof (CheckBox))
                        {
                            l.Text = (((CheckBox) gv.Controls[i]).Checked ? "true" : "false");
                            gv.Controls.Remove(gv.Controls[i]);
                            gv.Controls.AddAt(i, l);
                        }
                    }
                }
                if (gv.Controls[i].HasControls())
                    PrepareGridViewForExport(gv.Controls[i]);
            }
        }

        private void Adjuntar()
        {
            Master.ShowDialog(Program.LocalServerURL + "Site/Ajustes/p_adjuntar.aspx", "PopUpFile", "Adjuntar", "700", "150", "200", "200", false);
        }

        private void MostrarAdjuntos()
        {
            AdjuntosDataGrid.Visible = false;
            if (pTablaAdjuntos != null)
            {
                if (pTablaAdjuntos.Rows.Count > 0)
                {
                    AdjuntosDataGrid.Visible = true;
                    AdjuntosDataGrid.DataSource = pTablaAdjuntos;
                    AdjuntosDataGrid.DataBind();
                }
            }
        }

        #endregion

        #region Funciones

        public string GetObservacionesJsonData()
        {
            var ObservacionesJsonData = "";

            foreach (var obs in pTablaObservacion)
            {
                if (ObservacionesJsonData != "")
                    ObservacionesJsonData += ",";
                ObservacionesJsonData += "'" + obs.id_Ajuste_Observacion.ToString() + "|" + obs.Observacion_Ajuste.Replace("\"", "\\\"").Replace("'", "\\'") + "'";
            }
            return "[" + ObservacionesJsonData + "]";
        }

        private List<Int64> GuardarAdjuntos(DBAgrarioDataBaseManager nDBBanAgrario)
        {
            var nuevosAdjuntos = new List<Int64>();

            foreach (var adj in pTablaAdjuntos)
            {
                if (adj.fk_content_type == "new")
                    nuevosAdjuntos.Add(nDBBanAgrario.SchemaProcess.PA_Guardar_Adjunto_Ajuste.DBExecute(adj.Contenido_Adjunto, Path.GetExtension(adj.Nombre_Archivo), adj.Nombre_Archivo));

                //Registrar Accion
                Query = nDBBanAgrario.DataBase.LastQuery;
                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Crear, Path_Nodo, Query, "", "");

            }
            return nuevosAdjuntos;
        }

        //private byte[] StreamFile(string nFileName)
        //{
        //    using (var Fs = new FileStream(nFileName, FileMode.Open, FileAccess.Read))
        //    {
        //        var Contenido = new byte[Fs.Length];
        //        Fs.Read(Contenido, 0, (int) Fs.Length);
        //        return Contenido;
        //    }
        //}

        #endregion
    }
}