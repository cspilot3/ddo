using System;
using System.Data;
using DBAgrario;
using Slyg.Report;
using WebPunteoElectronico.Clases;
using DBSecurity;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using DBAgrario.SchemaConfig;

namespace WebPunteoElectronico.Site.Estadisticos
{
    public partial class WebGraphManagerViewer : FormBase
    {
        #region Propiedades

        public TBL_Gerencial_Reportes_GraficasType ReportConfig
        {
            get { return this.MiharuSession.Pagina.Parameter["ReportConfig"] as TBL_Gerencial_Reportes_GraficasType; }
            set { this.MiharuSession.Pagina.Parameter["ReportConfig"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.RegionalFindDropDownList.SelectedIndexChanged += new EventHandler(RegionalFindDropDownList_SelectedIndexChanged);
            this.COBFindDropDownList.SelectedIndexChanged += new EventHandler(COBFindDropDownList_SelectedIndexChanged);
            this.ConsultarLinkButton.Click += new EventHandler(ConsultarLinkButton_Click);
            this.ModoFindDropDownList.SelectedIndexChanged += new EventHandler(ModoFindDropDownList_SelectedIndexChanged);
            this.SaveLinkButton.Click += new EventHandler(SaveLinkButton_Click);
        }

        void SaveLinkButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        void ModoFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateModo(int.Parse(ModoFindDropDownList.SelectedValue));
        }

        void ConsultarLinkButton_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        void COBFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Oficinas(short.Parse(this.COBFindDropDownList.SelectedValue));
        }

        void RegionalFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_COBs(short.Parse(this.RegionalFindDropDownList.SelectedValue));
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            this.Master.Title = "Informe Gerencial";

            this.FechaMovimientoInicialTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-', '/');
            this.FechaMovimientoFinalTextBox.Text = this.FechaMovimientoInicialTextBox.Text;
            this.FechaProcesoInicialTextBox.Text = this.FechaMovimientoInicialTextBox.Text;
            this.FechaProcesoFinalTextBox.Text = this.FechaMovimientoInicialTextBox.Text;

            this.RegionalFindDropDownList.Items.Clear();
            this.RegionalFindDropDownList.Items.Add(new ListItem("- Todos - ", "-1"));

            DBSecurityDataBaseManager dbmSecurity = null;
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(ConnectionString.Security);
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

                var ReportListDataTable = dbmBanagrario.SchemaConfig.TBL_Reportes_Estadisticos.DBGet(null, 0, new TBL_Reportes_EstadisticosEnumList(TBL_Reportes_EstadisticosEnum.Nombre_Reporte, true));
                var Lista = new List<ListItem>();

                foreach (var Fila in ReportListDataTable)
                {
                    Lista.Add(new ListItem(Fila.Nombre_Reporte, Fila.id_Reporte.ToString()));
                }

                var ReportConfigTable = dbmBanagrario.SchemaConfig.TBL_Gerencial_Reportes_Graficas.DBGet(this.MiharuSession.Usuario.id);

                if (ReportConfigTable.Count > 0)
                {
                    this.ReportConfig = ReportConfigTable[0].ToTBL_Gerencial_Reportes_GraficasType();
                }
                else
                {
                    this.ReportConfig = new TBL_Gerencial_Reportes_GraficasType();

                    this.ReportConfig.id_Reporte_1 = 0;
                    this.ReportConfig.id_Grafica_1 = 4;

                    this.ReportConfig.id_Reporte_2 = 9;
                    this.ReportConfig.id_Grafica_2 = 4;

                    this.ReportConfig.id_Reporte_3 = 10;
                    this.ReportConfig.id_Grafica_3 = 4;

                    this.ReportConfig.id_Reporte_4 = 11;
                    this.ReportConfig.id_Grafica_4 = 4;

                    this.ReportConfig.id_Reporte_5 = 16;
                    this.ReportConfig.id_Grafica_5 = 4;

                    this.ReportConfig.id_Reporte_6 = 12;
                    this.ReportConfig.id_Grafica_6 = 4;
                }

                this.GraphManager1.LoadReportList(Lista, this.ReportConfig.id_Reporte_1, this.ReportConfig.id_Grafica_1);
                this.GraphManager2.LoadReportList(Lista, this.ReportConfig.id_Reporte_2, this.ReportConfig.id_Grafica_2);
                this.GraphManager3.LoadReportList(Lista, this.ReportConfig.id_Reporte_3, this.ReportConfig.id_Grafica_3);
                this.GraphManager4.LoadReportList(Lista, this.ReportConfig.id_Reporte_4, this.ReportConfig.id_Grafica_4);
                this.GraphManager5.LoadReportList(Lista, this.ReportConfig.id_Reporte_5, this.ReportConfig.id_Grafica_5);
                this.GraphManager6.LoadReportList(Lista, this.ReportConfig.id_Reporte_6, this.ReportConfig.id_Grafica_6);
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

            this.RegionalFindDropDownList.SelectedIndex = 0;
            Cargar_COBs(-1);

            UpdateModo(int.Parse(ModoFindDropDownList.SelectedValue));
        }

        protected override void Load_Data() { }

        private void Save()
        {
            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);

                dbmBanagrario.Connection_Open(this.MiharuSession.Usuario.id);

                this.ReportConfig.fk_usuario = this.MiharuSession.Usuario.id;
                this.ReportConfig.id_Reporte_1 = this.GraphManager1.idReport;
                this.ReportConfig.id_Grafica_1 = (short)this.GraphManager1.Tipo;

                this.ReportConfig.id_Reporte_2 = this.GraphManager2.idReport;
                this.ReportConfig.id_Grafica_2 = (short)this.GraphManager2.Tipo;

                this.ReportConfig.id_Reporte_3 = this.GraphManager3.idReport;
                this.ReportConfig.id_Grafica_3 = (short)this.GraphManager3.Tipo;

                this.ReportConfig.id_Reporte_4 = this.GraphManager4.idReport;
                this.ReportConfig.id_Grafica_4 = (short)this.GraphManager4.Tipo;

                this.ReportConfig.id_Reporte_5 = this.GraphManager5.idReport;
                this.ReportConfig.id_Grafica_5 = (short)this.GraphManager5.Tipo;

                this.ReportConfig.id_Reporte_6 = this.GraphManager6.idReport;
                this.ReportConfig.id_Grafica_6 = (short)this.GraphManager6.Tipo;

                dbmBanagrario.SchemaConfig.TBL_Gerencial_Reportes_Graficas.DBDelete(this.MiharuSession.Usuario.id);
                dbmBanagrario.SchemaConfig.TBL_Gerencial_Reportes_Graficas.DBInsert(this.ReportConfig);

                this.Master.ShowNotification("Guardar", "La configuración del reporte se almacenó satisfactoriamente");
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

                    var CobDataTable = dbmBanagrario.SchemaConfig.TBL_COB.DBFindByfk_Regional(nIdRegional, 0, new TBL_COBEnumList(TBL_COBEnum.Nombre_COB, true));

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

            this.COBFindDropDownList.SelectedIndex = 0;
            Cargar_Oficinas(-1);
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

                    var OficinaDataTable = dbmBanagrario.SchemaConfig.CTA_Oficinas_Web.DBFindByfk_COB(nIdCOB, 0, new CTA_Oficinas_WebEnumList(CTA_Oficinas_WebEnum.Nombre_Oficina, true));

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

        private void Consultar()
        {
            try
            {
                var Parametros = new Dictionary<string, object>();

                Parametros.Add("ConnectionString", this.ConnectionString);

                Parametros.Add("IdRegional", short.Parse(this.RegionalFindDropDownList.SelectedValue));
                Parametros.Add("NombreRegional", this.RegionalFindDropDownList.SelectedItem.Text);

                Parametros.Add("IdCOB", short.Parse(this.COBFindDropDownList.SelectedValue));
                Parametros.Add("NombreCOB", this.COBFindDropDownList.SelectedItem.Text);

                Parametros.Add("IdOficina", int.Parse(this.OficinaFindDropDownList.SelectedValue));
                Parametros.Add("NombreOficina", this.OficinaFindDropDownList.SelectedItem.Text);

                Parametros.Add("FechaMovimientoInicial", this.FechaMovimientoInicialTextBox.Text);
                Parametros.Add("FechaMovimientoFinal", this.FechaMovimientoFinalTextBox.Text);
                Parametros.Add("FechaProcesoInicial", this.FechaProcesoInicialTextBox.Text);
                Parametros.Add("FechaProcesoFinal", this.FechaProcesoFinalTextBox.Text);

                Parametros.Add("Modo", int.Parse(this.ModoFindDropDownList.SelectedValue));
                Parametros.Add("Login", this.MiharuSession.Usuario.Login);
                Parametros.Add("IdUsuario", this.MiharuSession.Usuario.id);

                var ModoParametros = (WebGraph.ModoParametrosEnum)byte.Parse(this.ModoFindDropDownList.SelectedValue);
                this.GraphManager1.Consultar(Parametros, ModoParametros);
                this.GraphManager2.Consultar(Parametros, ModoParametros);
                this.GraphManager3.Consultar(Parametros, ModoParametros);
                this.GraphManager4.Consultar(Parametros, ModoParametros);
                this.GraphManager5.Consultar(Parametros, ModoParametros);
                this.GraphManager6.Consultar(Parametros, ModoParametros);
            }
            catch (Exception ex)
            {
                this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        #endregion

        #region Funciones

        private DateTime getDate(string nFecha, bool nFinal)
        {
            var Partes = nFecha.Split('/');

            if (nFinal)
                return new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]), 23, 59, 59);
            else
                return new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]));
        }

        #endregion
    }
}