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
    public partial class WebGraphViewerB : FormBase
    {
        #region Propiedades

        public WebGraph WebGraph
        {
            get { return this.MiharuSession.Pagina.Parameter["WebGraph"] as WebGraph; }
        }

        public TipoReporteEnum Tipo
        {
            get { return (TipoReporteEnum)this.MiharuSession.Pagina.Parameter["Tipo"]; }
            set { this.MiharuSession.Pagina.Parameter["Tipo"] = value; }
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
           
            this.Area2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Area2DImageButton_Click);
            this.Bar2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Bar2DImageButton_Click);
            this.Column2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Column2DImageButton_Click);
            this.Column3DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Column3DImageButton_Click);
            this.Doughnut2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Doughnut2DImageButton_Click);
            this.FunnelImageButton.Click += new System.Web.UI.ImageClickEventHandler(FunnelImageButton_Click);
            this.LineImageButton.Click += new System.Web.UI.ImageClickEventHandler(LineImageButton_Click);
            this.Pie2DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Pie2DImageButton_Click);
            this.Pie3DImageButton.Click += new System.Web.UI.ImageClickEventHandler(Pie3DImageButton_Click);

            this.DowloadZipImageButton.Click += new System.Web.UI.ImageClickEventHandler(DowloadZipImageButton_Click);
        }

        void DowloadZipImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Dowload();
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

        protected void Area2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Area2D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void Bar2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Bar2D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void Column2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Column2D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void Column3DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Column3D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void Doughnut2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Doughnut2D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void FunnelImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Funnel;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void LineImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Line;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void Pie2DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Pie2D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        protected void Pie3DImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.Tipo = TipoReporteEnum.Pie3D;
            this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            this.RegionalFindDropDownList.Items.Clear();
            this.RegionalFindDropDownList.Items.Add(new ListItem("- Todos - ", "-1"));

            if (this.WebGraph == null)
            {
                this.Master.ShowAlert("No se definió la clase manejadora del reporte", MsgBoxIcon.IconError);

                this.ConsultarLinkButton.Visible = false;
            }

            this.Master.Title = this.WebGraph.ReportName;

            this.FechaMovimientoInicialTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-','/');
            this.FechaMovimientoFinalTextBox.Text = this.FechaMovimientoInicialTextBox.Text;
           
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

            this.Tipo = TipoReporteEnum.Column3D;

            this.InicioPanel.Visible = true;
            this.NoDataPanel.Visible = false;
            this.DataPanel.Visible = false;
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

        private void Consultar()
        {
            if (this.Validar())
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

                    Parametros.Add("Login", this.MiharuSession.Usuario.Login);
                    Parametros.Add("IdUsuario", this.MiharuSession.Usuario.id);

                    bool Result = this.WebGraph.Load(Parametros);
                    this.WebGraph.Draw(ref this.ChartLiteral, this.Tipo, 600, 300);

                    this.InicioPanel.Visible = false;
                    this.NoDataPanel.Visible = !Result;
                    this.DataPanel.Visible = Result;
                }
                catch (Exception ex)
                {
                    this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
                }
            }
        }

        protected void Dowload()
        {
            var data = this.WebGraph.BuildZipData();

            this.Master.Download(this, this.WebGraph.ZipFileName, data, "application/zip");
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

        private bool Validar()
        {
            if (!Slyg.Tools.DataConvert.IsDate(this.FechaMovimientoInicialTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (!Slyg.Tools.DataConvert.IsDate(this.FechaMovimientoFinalTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento final debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }            
            else if (Slyg.Tools.DataConvert.ToDate(this.FechaMovimientoInicialTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/').Value > Slyg.Tools.DataConvert.ToDate(this.FechaMovimientoFinalTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/').Value)
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe ser inferior a la fecha de movimiento final", MsgBoxIcon.IconWarning);
            }            
            else
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

                    Parametros.Add("Login", this.MiharuSession.Usuario.Login);

                    string ErrorMessage;
                    var Result = this.WebGraph.Validate(Parametros, out ErrorMessage);

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