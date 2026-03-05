using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBAgrario;
using DBAgrario.SchemaConfig;
using DBAgrario.SchemaProcess;
using DBAgrario.SchemaFirmasReport;
using DBSecurity;
using WebPunteoElectronico.Clases;
using WebPunteoElectronico.Clases.Slyg;
using WebPunteoElectronico.Master;
using System.Text;

namespace WebPunteoElectronico.Site.Ajustes
{
    public partial class AjusteTarjetaFaltante : FormBase
    {
        #region Declaraciones

        public string Query = "";
        public string Path_Nodo = "4.2";
        public Tipo_Accion_Log TipoAccion;

        #endregion

        #region  Propiedades

        public new Master.MasterConfig Master
        {
            get
            {
                return (Master.MasterConfig)OriginalMaster;
            }
        }


        protected CTA_Ajuste_Adjunto_AjusteDataTable pTablaAdjuntos
        {
            get
            {
                if (this.MiharuSession.Parameter["pTablaAdjuntos"] == null)
                    this.MiharuSession.Parameter["pTablaAdjuntos"] = new CTA_Ajuste_Adjunto_AjusteDataTable();

                return (CTA_Ajuste_Adjunto_AjusteDataTable)this.MiharuSession.Parameter["pTablaAdjuntos"];
            }
            set
            {
                this.MiharuSession.Parameter["pTablaAdjuntos"] = value;
            }
        }

        public TBL_RegionalDataTable pTablaRegional
        {
            get { return (TBL_RegionalDataTable)this.MiharuSession.Pagina.Parameter["_RegionalDataTable"]; }
            set { this.MiharuSession.Pagina.Parameter["_RegionalDataTable"] = value; }
        }

        public TBL_COBDataTable pTablaCOB
        {
            get { return (TBL_COBDataTable)(this.MiharuSession.Pagina.Parameter["_COBDataTable"]); }
            set { this.MiharuSession.Pagina.Parameter["_COBDataTable"] = value; }
        }

        public TBL_OficinaDataTable pTablaOficina
        {
            get { return (TBL_OficinaDataTable)(this.MiharuSession.Pagina.Parameter["_OficinaDataTable"]); }
            set { this.MiharuSession.Pagina.Parameter["_OficinaDataTable"] = value; }
        }

        public DBAgrario.SchemaFirmas.TBL_Ajuste_ObservacionDataTable pTablaObservacion
        {
            get { return (DBAgrario.SchemaFirmas.TBL_Ajuste_ObservacionDataTable)(this.MiharuSession.Pagina.Parameter["pTablaObservacion"]); }
            set { this.MiharuSession.Pagina.Parameter["pTablaObservacion"] = value; }
        }

        #endregion

        #region  Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            tdControlAjustar.Visible = (this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Registros_Sobrantes_Ajustar) || this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Registros_Sobrantes_Reabrir));
            tdControlAprobar.Visible = (this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Registros_Sobrantes_Aprobar));
            tdControlRechazar.Visible = (this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Ajustes_Registros_Sobrantes_Reabrir));

            if (!this.IsPostBack)
                Config_Page();

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.OnSelectedGridChanged += Grid_OnSelectedGridChanged;
            this.ConsultarLinkButton.Click += ConsultarLinkButton_Click;
            //this.lnkAjustar.Click += new EventHandler(lnkAjustar_Click);
            //this.lnkAprobar.Click += new EventHandler(lnkAprobar_Click);
            //this.lnkRechazar.Click += new EventHandler(lnkRechazar_Click);
            this.RegionalFindDropDownList.SelectedIndexChanged += RegionalFindDropDownList_SelectedIndexChanged;
            this.COBFindDropDownList.SelectedIndexChanged += COBFindDropDownList_SelectedIndexChanged;
            this.EditarSeleccionadosButton.Click += EditarSeleccionadosButton_Click;
            this.VerRegistroButton.Click += VerRegistroButton_Click;
            this.ExportWordButton.Click += ExportWordButton_Click;
            this.ExportExcelButton0.Click += ExportExcelButton0_Click;
            //this.AdjuntarButton.Click += AdjuntarButton_Click;
            //this.AdjuntosDataGrid.SelectedIndexChanged += AdjuntosDataGrid_SelectedIndexChanged;
            this.Master.DaughterClose += AjusteSoportesSobrantes_HijaClose;

            this.Master.Title = "Ajuste de Tarjetas Faltantes";

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

        protected void lnkRechazar_Click(Object sender, EventArgs e)
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
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                if (RegionalFindDropDownList.SelectedValue == "-1") return;

                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                DataTable data_Tx = dbmBanagrario.SchemaConfig.TBL_COB.DBFindByfk_Regional(short.Parse(RegionalFindDropDownList.SelectedValue));

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
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                if (COBFindDropDownList.SelectedValue == "-1") return;
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                DataTable data_Tx = dbmBanagrario.SchemaConfig.CTA_Oficinas_Web.DBFindByfk_COB((short.Parse(COBFindDropDownList.SelectedValue)), 0, new CTA_Oficinas_WebEnumList(CTA_Oficinas_WebEnum.Nombre_Oficina, true));

                dbmBanagrario.Connection_Close();

                if (data_Tx.Rows.Count > 0)
                {
                    CargarCombo(ref OficinaFindDropDownList, data_Tx, TBL_OficinaEnum.id_Oficina.ColumnName, TBL_OficinaEnum.Nombre_Oficina.ColumnName);

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

        protected void EditarSeleccionadosButton_Click(object sender, EventArgs e)
        {
            EditarSeleccionados();
        }

        protected void VerRegistroButton_Click(object sender, EventArgs e)
        {
            VerRegistro();
        }

        protected void ExportWordButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportToWord();
        }

        protected void ExportExcelButton0_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ExportToExcel();
        }

        //protected void AdjuntarButton_Click(object sender, EventArgs e)
        //{
        //    Adjuntar();
        //}

        //protected void AdjuntosDataGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var id_Adjunto_Ajuste = AdjuntosDataGrid.SelectedItem.Cells[1].Text;
        //    var adj = (CTA_Ajuste_Adjunto_AjusteRow[])pTablaAdjuntos.Select(CTA_Ajuste_Adjunto_AjusteEnum.fk_Ajuste_Adjunto.ColumnName + "=" + id_Adjunto_Ajuste);

        //    if (adj.Length <= 0) return;
        //    this.MiharuSession.Pagina.Parameter["Content"] = adj[0].Contenido_Adjunto;
        //    Master.ShowWindowNoBloqueo("../Adjuntos/show_adjunto.aspx?id_Adjunto_Ajuste=" + id_Adjunto_Ajuste + "&NombreArchivo=" + adj[0].Nombre_Archivo + "&ContentType=" + Path.GetExtension(adj[0].Nombre_Archivo), "Exportar", "200", "100");
        //}

        private void AjusteSoportesSobrantes_HijaClose()
        {
            //MostrarAdjuntos();
            Master.SelectTab(Tabs.Detalle);
        }

        #endregion

        #region Metodos

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
                dbmSecurity.Connection_Open(this.MiharuSession.Usuario.id);
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

        protected override void Load_Data() { }

        protected void ConfigData()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                pTablaRegional = dbmBanagrario.SchemaConfig.TBL_Regional.DBGet(null, 0, new TBL_RegionalEnumList(TBL_RegionalEnum.Nombre_Regional, true));
                pTablaCOB = dbmBanagrario.SchemaConfig.TBL_COB.DBGet(null, 0, new TBL_COBEnumList(TBL_COBEnum.Nombre_COB, true));
                pTablaOficina = dbmBanagrario.SchemaConfig.TBL_Oficina.DBGet(null, 0, new TBL_OficinaEnumList(TBL_OficinaEnum.Nombre_Oficina, true));
                pTablaObservacion = dbmBanagrario.SchemaFirmas.TBL_Ajuste_Observacion.DBFindByActiva(true);

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

        private void ConfigMainGrid()
        {
            MainGrid.Initialize(new TBL_Gestion_Tarjetas_Firmas_FaltantesDataTable());
            //MainGrid.Initialize();
            MainGrid.IsMultiCheck = true;
            MainGrid.GenerateByColModel = true;
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.id_Cargue_Detalle.ColumnName, Header = "Id", IsColumnID = true, Width = 50 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Fecha_Movimiento.ColumnName, Header = "Fecha de Movimiento", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Fecha_Proceso_Cargue.ColumnName, Header = "Fecha Proceso", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Fecha_Proceso.ColumnName, Header = "Fecha Proceso", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Codigo_Regional.ColumnName, Header = "Código Regional", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Codigo_COB.ColumnName, Header = "Código COB", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Nombre_Cob.ColumnName, Header = "COB", Width = 150 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Codigo_Oficina.ColumnName, Header = "Código Oficina", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Nombre_Oficina.ColumnName, Header = "Oficina", Width = 150 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Producto.ColumnName, Header = "Producto", Width = 60 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Codigo_Transaccion.ColumnName, Header = "Codigo Transacción", Width = 150 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Nombre_Transaccion.ColumnName, Header = "Nombre de Transacción", Width = 250 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Numero_Cuenta.ColumnName, Header = "Número de Cuenta/Ente", Width = 150 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.esFaltante.ColumnName, Header = "Faltante", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.esRechazada.ColumnName, Header = "Rechazada", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.esContingencia.ColumnName, Header = "Contingencia", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Exitoso.ColumnName, Header = "Exitoso", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Ente.ColumnName, Header = "Ente", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.File_Unique_Identifier.ColumnName, Header = "File_Unique_Identifier", Ignore = true });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = "URL", Header = "Hipervinculo Registro", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Usuario.ColumnName, Header = "Usuario", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = "Estado", Header = "Estado", Width = 100 });
            MainGrid.ColModel.Add(new FlexColumnMap { ColumnName = TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.Observacion.ColumnName, Header = "Observaciones", Width = 250 });

        }

        private void Buscar()
        {
            try
            {
                if (MainGrid.DataSource != null)
                {
                    MainGrid.DataSource.Rows.Clear();
                }

                DateTime FechaInicial = DateTime.Parse(FechaInicialTextBox.Text);
                DateTime FechaFinal = DateTime.Parse(FechaFinalTextBox.Text);

                if (FechaInicial > FechaFinal)
                    Master.ShowAlert("La fecha inicial debe ser menor o igual a la fecha final", MsgBoxIcon.IconWarning);
                else
                    if (Math.Abs((FechaInicial - FechaFinal).Days) > 15)
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

                var _item = new ListItem { Value = (-1).ToString(), Text = "--Todos--" };
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
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);
                MainGrid.DataSource = dbmBanagrario.SchemaFirmasReport.PA_Gestion_Tarjetas_Firmas_Faltantes.DBExecute(
                                                                                                                    short.Parse(RegionalFindDropDownList.SelectedValue),
                                                                                                                    short.Parse(COBFindDropDownList.SelectedValue),
                                                                                                                    int.Parse(OficinaFindDropDownList.SelectedValue),
                                                                                                                    int.Parse(FechaInicialTextBox.Text.Replace("/", "")),
                                                                                                                    int.Parse(FechaFinalTextBox.Text.Replace("/", "")),
                                                                                                                    int.Parse(FechaInicialTextBox.Text.Replace("/", "")),
                                                                                                                    int.Parse(FechaFinalTextBox.Text.Replace("/", "")),
                                                                                                                    1,
                                                                                                                    -1);


                //Registrar Accion
                Query = dbmBanagrario.DataBase.LastQuery;
                Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Consultar, Path_Nodo, Query, "", "");

                dbmBanagrario.Connection_Close();

                if (MainGrid.DataSource.Rows.Count == 0)
                    Master.ShowAlert("No se encontraron Tarjetas Faltantes que cumplan con el críterio de Búsqueda", MsgBoxIcon.IconInformation);
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

        protected void CargarDetalle(int nIdCargueDetalle)
        {
            this.Master.ScriptBag.Append("ShowObservacion = '1';");
            DBAgrarioDataBaseManager DBBanagrario = null;

            try
            {
                DBBanagrario = new DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                //var errorData = (TBL_Gestion_Tarjetas_Firmas_FaltantesRow[])MainGrid.DataSource.Select(TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.id_Cargue_Detalle.ColumnName + "=" + nIdCargueDetalle.ToString());
                var errorData = MainGrid.DataSource.Select(TBL_Gestion_Tarjetas_Firmas_FaltantesEnum.id_Cargue_Detalle.ColumnName + "=" + nIdCargueDetalle.ToString());
                var e = errorData[0];
                MainGrid.SetSelectedRows(e);
                this.MiharuSession.Parameter["Seleccionados"] = MainGrid.GetSelectedRows();

                this.Master.ScriptBag.Append("ShowObservacion = '1';");
                btnNuevo.Disabled = false;
                btnEditar.Disabled = false;
                btnInactivar.Disabled = false;
                txtObservacion.Enabled = true;

                //var Seleccionados = (TBL_Gestion_Tarjetas_Firmas_FaltantesRow[])MainGrid.GetSelectedRows();
                var Seleccionados = MainGrid.GetSelectedRows();
                txtObservacion.Text = "";

                foreach (var nRow in Seleccionados)
                {
                    //if (!nRow.IsObservacionNull())
                    if ((string)nRow.ItemArray[14].ToString() != "")
                    {
                        this.Master.ScriptBag.Append("ShowObservacion = '0';");
                        txtObservacion.Enabled = false;
                        btnNuevo.Disabled = true;
                        btnEditar.Disabled = true;
                        btnInactivar.Disabled = true;
                        break;
                    }
                }

                lblDataFechaP.Text = e[1].ToString();//e.Fecha_Movimiento;
                lblDataOficina.Text = e[4].ToString() + " -- " + e[5].ToString();//e.Codigo_Oficina.ToString() + " -- " + e.Nombre_Oficina;
                lblDataTx.Text = e[7].ToString() + " -- " + e[8].ToString();//e.Codigo_Transaccion + " -- " + e.Nombre_Transaccion;

                //lblDataValor.Text = "$ " + e.Valor;

                //if (e.id_Estado_Ajuste == (short)Estado_AjusteEnum.Aprobado)
                //{
                //    this.Master.ScriptBag.Append("ShowObservacion = '0';");
                //    txtObservacion.Enabled = false;
                //    btnNuevo.Disabled = true;
                //    btnEditar.Disabled = true;
                //    btnInactivar.Disabled = true;
                //}
                //else
                //{
                //    btnNuevo.Disabled = false;
                //    btnEditar.Disabled = false;
                //    btnInactivar.Disabled = false;
                //}

                //rbtAjuste.Items[0].Selected = false;
                //rbtAjuste.Items[1].Selected = false;

                //DBBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                //pTablaAdjuntos.Rows.Clear();
                //if (!e.Isid_AjusteNull())
                //{
                //    pTablaAdjuntos = DBBanagrario.SchemaProcess.CTA_Ajuste_Adjunto_Ajuste.DBFindByfk_Errorfk_Ajuste(e.id_Error, null);
                //}

                //MostrarAdjuntos();

                //DBBanagrario.Connection_Close();

                //if (!e.IsAjustaNull())
                //{
                //    if (e.Ajusta)
                //        rbtAjuste.Items[0].Selected = true;
                //    else
                //        rbtAjuste.Items[1].Selected = true;
                //}

                //if (e.Isfk_Ajuste_ObservacionNull())
                //    txtObservacion.Text = "";
                //else
                //    txtObservacion.Text = e.fk_Ajuste_Observacion + "|" + (e.IsObservacion_AjusteNull() ? "" : e.Observacion_Ajuste);

                VerRegistroButton.Enabled = true;

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

            //var Seleccionados = (TBL_Gestion_Tarjetas_Firmas_FaltantesRow[])MainGrid.GetSelectedRows();
            var Seleccionados = MainGrid.GetSelectedRows();
            txtObservacion.Text = "";

            foreach (var nRow in Seleccionados)
            {
                //if (!nRow.IsObservacionNull())
                if ((string)nRow.ItemArray[14].ToString() != "")
                {
                    this.Master.ScriptBag.Append("ShowObservacion = '0';");
                    txtObservacion.Enabled = false;
                    btnNuevo.Disabled = true;
                    btnEditar.Disabled = true;
                    btnInactivar.Disabled = true;
                    break;
                }
            }

            lblDataFechaP.Text = "(Valores multiples)";
            lblDataOficina.Text = "(Valores multiples)";
            lblDataTx.Text = "(Valores multiples)";
            //lblDataValor.Text = "(Valores multiples)";

            //rbtAjuste.Items[0].Selected = false;
            //rbtAjuste.Items[1].Selected = false;

            pTablaAdjuntos.Rows.Clear();
            //MostrarAdjuntos();

            VerRegistroButton.Enabled = false;
            this.MiharuSession.Parameter["Seleccionados"] = Seleccionados;
            Master.SelectTab(Tabs.Detalle);
        }

        protected void Guardar(EstadoAjusteEnum nEstadoAjuste)
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                //var Seleccionados = (TBL_Gestion_Tarjetas_Firmas_FaltantesRow[])this.MiharuSession.Parameter["Seleccionados"];
                var Seleccionados = (DataRow[])this.MiharuSession.Parameter["Seleccionados"];

                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);
                //dbmBanagrario.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                dbmBanagrario.Transaction_Begin();

                if (Seleccionados.Length != 0)
                {
                    //var Ajusta = rbtAjuste.Items[0].Selected;

                    //var nuevosAdjuntos = new List<Int64>();

                    //if (pTablaAdjuntos.Rows.Count > 0)
                    //{
                    //    nuevosAdjuntos = GuardarAdjuntos(dbmBanagrario);
                    //}

                    if (txtObservacion.Text == "") throw new ApplicationException("Primero debe seleccionar una observacion para el ajuste");
                    var obsParts = txtObservacion.Text.Split('|');

                    if (obsParts.Length != 2) throw new ApplicationException("La observacion seleccionada no es válida");

                    var fk_Ajuste_Observacion = Convert.ToInt32(obsParts[0]);
                    var texto_Observacion = txtObservacion.Text.Remove(0, fk_Ajuste_Observacion.ToString().Length + 1);

                    foreach (var nRow in Seleccionados)
                    {

                        //var FaltantesType = nRow.ToTBL_Gestion_Tarjetas_Firmas_FaltantesType();
                        var Row = dbmBanagrario.SchemaFirmasReport.TBL_Gestion_Tarjetas_Firmas_Faltantes.DBGet(Convert.ToInt32(nRow[0])).NewTBL_Gestion_Tarjetas_Firmas_FaltantesRow();

                        var FaltantesType = Row.ToTBL_Gestion_Tarjetas_Firmas_FaltantesType();
                        FaltantesType.Observacion = texto_Observacion;

                        //dbmBanagrario.SchemaFirmasReport.TBL_Gestion_Tarjetas_Firmas_Faltantes.DBUpdate(FaltantesType, nRow.id_Cargue_Detalle);
                        dbmBanagrario.SchemaFirmasReport.TBL_Gestion_Tarjetas_Firmas_Faltantes.DBUpdate(FaltantesType, Convert.ToInt32(nRow[0]));

                        //Registrar accion
                        //Query = dbmBanagrario.DataBase.LastQuery;
                        //Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), TipoAccion, Path_Nodo, Query, "", texto_Observacion);


                        //foreach (var adj in nuevosAdjuntos)
                        //{
                        //    dbmBanagrario.SchemaProcess.TBL_Ajuste_Ajunto_Ajuste.DBInsert(
                        //        new TBL_Ajuste_Ajunto_AjusteType { fk_Error = nRow.id_Error, fk_Ajuste = idAjuste, fk_Ajuste_Adjunto = adj }
                        //    );
                        //}
                    }

                    Master.ShowAlert(Seleccionados.Length == 1 ? "El Ajuste fue guardado Exitosamente" : "Ajustes guardados Exitosamente", MsgBoxIcon.IconInformation);

                    LimpiarControles();
                    dbmBanagrario.Transaction_Commit();
                }
                else
                {
                    Master.ShowAlert("Debe seleccionar un registro", MsgBoxIcon.IconInformation);
                }
            }
            catch (Exception ex)
            {
                if (dbmBanagrario != null) dbmBanagrario.Transaction_Rollback();
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconInformation);
            }
            finally
            {
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }
        }

        private void VerRegistro()
        {
            //var Seleccionados = (TBL_Gestion_Tarjetas_Firmas_FaltantesRow[])this.MiharuSession.Parameter["Seleccionados"];
            var Seleccionados = (DataRow[])this.MiharuSession.Parameter["Seleccionados"];

            if (Seleccionados.Length != 0)
            {
                var Path = Program.LocalServerURL + System.Configuration.ConfigurationManager.AppSettings["VisorTxDetalleFirmas"];

                if (Path == "")
                {
                    Path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/_sitio/VerTxDetalleLog.aspx";
                }

                //Master.ScriptBag.AppendAndEncodeScript("window.open('" + Path + "?tx=" + Seleccionados[0].id_Cargue_Detalle + "','_blank', 'width=1000,height=650')");
                Master.ScriptBag.AppendAndEncodeScript("window.open('" + Path + "?tx=" + Seleccionados[0].ItemArray[0].ToString() + "','_blank', 'width=1000,height=650')");
            }
            else
                Master.ShowAlert("Debe seleccionar un registro", MsgBoxIcon.IconInformation);
        }

        protected void LimpiarControles()
        {
            lblDataOficina.Text = "";
            lblDataFechaP.Text = "";
            lblDataTx.Text = "";
            //lblDataValor.Text = "";
            txtObservacion.Text = "";
            //rbtAjuste.Items[0].Selected = false;
            //rbtAjuste.Items[1].Selected = false;

            pTablaAdjuntos.Rows.Clear();
            //MostrarAdjuntos();
        }

        private void EditarSeleccionados()
        {
            pTablaAdjuntos.Rows.Clear();
            var Seleccionados = MainGrid.GetSelectedRows();
            this.MiharuSession.Parameter["Seleccionados"] = Seleccionados;
            //var data = new TBL_Gestion_Tarjetas_Firmas_FaltantesType();
            //data.FromDataRow(Seleccionados[0]);

            if (Seleccionados.Length == 1)
                //CargarDetalle((int)data.id_Cargue_Detalle);
                CargarDetalle(Convert.ToInt32(Seleccionados[0].ItemArray[0].ToString()));

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

            var table = new GridView { DataSource = MainGrid.DataSource };
            table.DataBind();

            PrepareGridViewForExport(table);
            table.RenderControl(htw);

            this.MiharuSession.Pagina.Parameter["ContentName"] = "AjusteTarjetasFaltantes.doc";
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

                t.Columns["id_Cargue_Detalle"].ColumnTitle = "Id";
                t.Columns["Fecha_Movimiento"].ColumnTitle = "Fecha Movimiento";
                t.Columns["Fecha_proceso"].ColumnTitle = "Fecha Proceso";
                t.Columns["Nombre_Cob"].ColumnTitle = "COB";
                t.Columns["Codigo_Oficina"].ColumnTitle = "Codigo Oficina";
                t.Columns["Nombre_Oficina"].ColumnTitle = "Nombre Oficina";
                t.Columns["Producto"].ColumnTitle = "Producto";
                t.Columns["Codigo_Transaccion"].ColumnTitle = "Codigo Transacción";
                t.Columns["Nombre_Transaccion"].ColumnTitle = "Nombre de Transacción";
                t.Columns["Numero_Cuenta"].ColumnTitle = "Número de Cuenta/Ente";
                t.Columns["Usuario"].ColumnTitle = "Usuario";
                t.Columns["Estado"].ColumnTitle = "Estado";
                t.Columns["Observacion"].ColumnTitle = "Observaciones";

                //Columnas que no son requeridas en la exportación

                t.Columns["URL"].ColumnTitle = "URL";
                t.Columns["URL"].Export = false;
                t.Columns["Check_Multi"].ColumnTitle = "Check_Multi";
                t.Columns["Check_Multi"].Export = false;
                /*
                t.Columns["Codigo_Regional"].ColumnTitle = "Codigo_Regional";
                t.Columns["Codigo_Regional"].Export = false;
                t.Columns["Codigo_COB"].ColumnTitle = "Codigo_COB";
                t.Columns["Codigo_COB"].Export = false;
                t.Columns["Fecha_Proceso"].ColumnTitle = "Fecha_Proceso";
                t.Columns["Fecha_Proceso"].Export = false;
                t.Columns["esFaltante"].ColumnTitle = "esFaltante";
                t.Columns["esFaltante"].Export = false;
                t.Columns["esRechazada"].ColumnTitle = "esRechazada";
                t.Columns["esRechazada"].Export = false;
                t.Columns["esContingencia"].ColumnTitle = "esContingencia";
                t.Columns["esContingencia"].Export = false;
                t.Columns["Exitoso"].ColumnTitle = "Exitoso";
                t.Columns["Exitoso"].Export = false;
                t.Columns["File_Unique_Identifier"].ColumnTitle = "File_Unique_Identifier";
                t.Columns["File_Unique_Identifier"].Export = false;
                t.Columns["Ente"].ColumnTitle = "Ente";
                t.Columns["Ente"].Export = false;
                 */

                Exportador.SaveAsCSV(t, Flujo, true);

                this.MiharuSession.Pagina.Parameter["ContentName"] = "AjusteTarjetasFaltantes.csv";
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

            for (var i = 0; i <= gv.Controls.Count - 1; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = ((LinkButton)gv.Controls[i]).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);

                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = ((DropDownList)gv.Controls[i]).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);

                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (((CheckBox)gv.Controls[i]).Checked ? "true" : "false");
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);

                }
                if (gv.Controls[i].HasControls())
                    PrepareGridViewForExport(gv.Controls[i]);
            }
        }

        //private void Adjuntar()
        //{
        //    Master.ShowDialog(Program.LocalServerURL +"Site/Ajustes/p_adjuntar.aspx", "PopUpFile", "Adjuntar", "580", "200", "100", "100", false);
        //}

        //private void MostrarAdjuntos()
        //{
        //    AdjuntosDataGrid.Visible = false;
        //    if (pTablaAdjuntos != null)
        //    {
        //        if (pTablaAdjuntos.Rows.Count > 0)
        //        {
        //            AdjuntosDataGrid.Visible = true;
        //            AdjuntosDataGrid.DataSource = pTablaAdjuntos;
        //            AdjuntosDataGrid.DataBind();
        //        }
        //    }
        //}

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

        //private List<Int64> GuardarAdjuntos(DBAgrarioDataBaseManager nDBBanAgrario)
        //{
        //    var nuevosAdjuntos = new List<Int64>();

        //    foreach (var adj in pTablaAdjuntos)
        //    {
        //        if (adj.fk_content_type == "new")
        //            nuevosAdjuntos.Add(nDBBanAgrario.SchemaProcess.PA_Guardar_Adjunto_Ajuste.DBExecute(adj.Contenido_Adjunto, Path.GetExtension(adj.Nombre_Archivo), adj.Nombre_Archivo));

        //        //Registrar Accion
        //        Query = nDBBanAgrario.DataBase.LastQuery;
        //        Log.InsertLog(this.MiharuSession.Usuario.id, Program.getIPName(), Tipo_Accion_Log.Crear, Path_Nodo, Query, "", "");

        //    }
        //    return nuevosAdjuntos;
        //}

        //private byte[] StreamFile(string nFileName)
        //{
        //    var Fs = new FileStream(nFileName, FileMode.Open, FileAccess.Read);
        //    var Contenido = new byte[Fs.Length];
        //    Fs.Read(Contenido, 0, (int)(Fs.Length));
        //    Fs.Close();
        //    return Contenido;
        //}

        #endregion
    }
}