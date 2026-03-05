using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Reportes
{
    public partial class WebReportViewerF : FormBase
    {
        #region Propiedades

        public WebReport WebReport
        {
            get { return this.MiharuSession.Pagina.Parameter["WebReport"] as WebReport; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.RegionalFindDropDownList.SelectedIndexChanged += RegionalFindDropDownList_SelectedIndexChanged;
            this.COBFindDropDownList.SelectedIndexChanged += COBFindDropDownList_SelectedIndexChanged;
            this.ConsultarLinkButton.Click += ConsultarLinkButton_Click;
        }

        protected void ConsultarLinkButton_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        protected void COBFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Oficinas(short.Parse(this.COBFindDropDownList.SelectedValue));
        }

        protected void RegionalFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_COBs(short.Parse(this.RegionalFindDropDownList.SelectedValue));
        }

        protected void FacturarButton_Click(object sender, EventArgs e)
        {
            Facturar();
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            this.RegionalFindDropDownList.Items.Clear();
            this.RegionalFindDropDownList.Items.Add(new ListItem("- Todos - ", "-1"));

            if (this.WebReport == null)
            {
                this.Master.ShowAlert("No se definió la clase manejadora del reporte", MsgBoxIcon.IconError);
                this.ConsultarLinkButton.Visible = false;
                return;
            }

            this.Master.Title = this.WebReport.ReportName;

            this.FechaMovimientoTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-', '/');

            DBSecurity.DBSecurityDataBaseManager dbmSecurity = null;
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmSecurity = new DBSecurity.DBSecurityDataBaseManager(ConnectionString.Security);
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);

                dbmSecurity.Connection_Open(this.MiharuSession.Usuario.id);
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                dbmSecurity.SchemaSecurity.PA_Insercion_Usuario_Acceso.DBExecute(this.MiharuSession.Usuario.id, Program.Modulo, 200, this.MiharuSession.ClientIPAddress);

                // Cargar las regionales
                var RegionalDataTable = dbmBanagrario.SchemaConfig.TBL_Regional.DBGet(null, 0, new DBAgrario.SchemaConfig.TBL_RegionalEnumList(DBAgrario.SchemaConfig.TBL_RegionalEnum.Nombre_Regional, true));

                foreach (var Fila in RegionalDataTable)
                {
                    this.RegionalFindDropDownList.Items.Add(new ListItem(Fila.Nombre_Regional, Fila.id_Regional.ToString()));
                }
            }
            catch (Exception ex)
            {
                this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
                if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
            }

            this.RegionalFindDropDownList.SelectedValue = this.WebReport.getParameter("idRegional");
            Cargar_COBs(short.Parse(this.RegionalFindDropDownList.SelectedValue));
        }

        protected override void Load_Data() { }

        private void Cargar_COBs(short nIdRegional)
        {
            this.COBFindDropDownList.Items.Clear();
            this.COBFindDropDownList.Items.Add(new ListItem("- Todos - ", "-1"));

            if (nIdRegional != -1)
            {
                DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

                try
                {
                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);

                    dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                    var CobDataTable = dbmBanagrario.SchemaConfig.TBL_COB.DBFindByfk_Regional(nIdRegional, 0, new DBAgrario.SchemaConfig.TBL_COBEnumList(DBAgrario.SchemaConfig.TBL_COBEnum.Nombre_COB, true));

                    foreach (var Fila in CobDataTable)
                    {
                        this.COBFindDropDownList.Items.Add(new ListItem(Fila.Nombre_COB, Fila.id_COB.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                }
                finally
                {
                    if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                }
            }

            this.COBFindDropDownList.SelectedValue = this.WebReport.getParameter("idCOB");
            Cargar_Oficinas(short.Parse(this.COBFindDropDownList.SelectedValue));
        }

        private void Cargar_Oficinas(short nIdCOB)
        {
            this.OficinaFindDropDownList.Items.Clear();
            this.OficinaFindDropDownList.Items.Add(new ListItem("- Todos - ", "-1"));

            if (nIdCOB != -1)
            {
                DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

                try
                {
                    dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);

                    dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                    var OficinaDataTable = dbmBanagrario.SchemaConfig.CTA_Oficinas_Web.DBFindByfk_COB(nIdCOB, 0, new DBAgrario.SchemaConfig.CTA_Oficinas_WebEnumList(DBAgrario.SchemaConfig.CTA_Oficinas_WebEnum.Nombre_Oficina, true));

                    foreach (var Fila in OficinaDataTable)
                    {
                        this.OficinaFindDropDownList.Items.Add(new ListItem(Fila.Nombre_Oficina, Fila.id_Oficina.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                }
                finally
                {
                    if (dbmBanagrario != null) dbmBanagrario.Connection_Close();
                }
            }

            this.OficinaFindDropDownList.SelectedValue = this.WebReport.getParameter("idOficina");
        }

        private void Consultar()
        {
            try
            {
                var Parametros = new Dictionary<string, object> {{"ConnectionString", this.ConnectionString}, {"IdRegional", short.Parse(this.RegionalFindDropDownList.SelectedValue)}, {"NombreRegional", this.RegionalFindDropDownList.SelectedItem.Text}, {"IdCOB", short.Parse(this.COBFindDropDownList.SelectedValue)}, {"NombreCOB", this.COBFindDropDownList.SelectedItem.Text}, {"IdOficina", int.Parse(this.OficinaFindDropDownList.SelectedValue)}, {"NombreOficina", this.OficinaFindDropDownList.SelectedItem.Text}, {"FechaMovimiento", this.FechaMovimientoTextBox.Text}, {"Modo", int.Parse(this.ModoFindDropDownList.SelectedValue)}, {"Login", this.MiharuSession.Usuario.Login}, {"IdUsuario", this.MiharuSession.Usuario.id}, {"Detallado", this.DetalladoCheck.Checked}};

                this.WebReport.Launch(ref this.PageReportViewer, Parametros);

                PerfilFacturar();
            }
            catch (Exception ex)
            {
                this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        protected void Facturar()
        {
            var CantRegistros = PageReportViewer.LocalReport.DataSources.Count;

            if (CantRegistros <= 0) return;

            var dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
            try
            {
                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                if (int.Parse(ModoFindDropDownList.SelectedValue) == 1)
                {
                    //Fecha Movimiento 
                    dbmBanagrario.SchemaProcess.PA_Facturar_Transacciones_Procesadas_Mensualmente_Tipos_Movimiento.DBExecute(short.Parse(this.RegionalFindDropDownList.SelectedValue),
                        short.Parse(this.COBFindDropDownList.SelectedValue),
                        int.Parse(this.OficinaFindDropDownList.SelectedValue),
                        getDate(FechaMovimientoTextBox.Text, false), this.MiharuSession.Usuario.id);

                    Master.ShowAlert("Se han marcado las transacciones como facturadas exitosamente", MsgBoxIcon.IconInformation);
                }
                else
                {
                    //Fecha Proceso
                    dbmBanagrario.SchemaProcess.PA_Facturar_Transacciones_Procesadas_Mensualmente_Tipos_Proceso.DBExecute(short.Parse(this.RegionalFindDropDownList.SelectedValue),
                        short.Parse(this.COBFindDropDownList.SelectedValue),
                        int.Parse(this.OficinaFindDropDownList.SelectedValue),
                        getDate(FechaMovimientoTextBox.Text, false), MiharuSession.Usuario.id);

                    Master.ShowAlert("Se han marcado las transacciones como facturadas exitosamente", MsgBoxIcon.IconInformation);


                }
            }
            catch (Exception ex)
            {
                Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
            finally
            {
                dbmBanagrario.Connection_Close();
            }
        }

        protected void PerfilFacturar()
        {
            FacturarButton.Visible = (this.MiharuSession.Usuario.PerfilManager.PuedeAcceder(Program.Permiso_Facturacion));
        }

        #endregion

        #region Funciones

        private DateTime getDate(string nFecha, bool nFinal)
        {
            var Partes = nFecha.Split('/');

            return nFinal ? new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]), 23, 59, 59) : new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]));
        }

        private bool Validar()
        {
            if (!Slyg.Tools.DataConvert.IsDate(this.FechaMovimientoTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else
            {
                try
                {
                    var Parametros = new Dictionary<string, object> {{"ConnectionString", this.ConnectionString}, {"IdRegional", short.Parse(this.RegionalFindDropDownList.SelectedValue)}, {"NombreRegional", this.RegionalFindDropDownList.SelectedItem.Text}, {"IdCOB", short.Parse(this.COBFindDropDownList.SelectedValue)}, {"NombreCOB", this.COBFindDropDownList.SelectedItem.Text}, {"IdOficina", int.Parse(this.OficinaFindDropDownList.SelectedValue)}, {"NombreOficina", this.OficinaFindDropDownList.SelectedItem.Text}, {"FechaMovimiento", this.FechaMovimientoTextBox.Text}, {"Modo", int.Parse(this.ModoFindDropDownList.SelectedValue)}, {"Login", this.MiharuSession.Usuario.Login}};

                    string ErrorMessage;
                    var Result = this.WebReport.Validate(Parametros, out ErrorMessage);

                    if (!Result)
                        this.Master.ShowAlert(ErrorMessage, MsgBoxIcon.IconWarning);
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                }
            }

            return false;
        }

        #endregion
    }
}