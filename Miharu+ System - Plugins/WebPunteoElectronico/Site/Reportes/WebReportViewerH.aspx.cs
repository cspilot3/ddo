using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using WebPunteoElectronico.Clases;
using System.Data;

namespace WebPunteoElectronico.Site.Reportes
{
    public partial class WebReportViewerH : FormBase
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
            this.ExportarLinkButton.Click += ExportarLinkButton_Click;
            this.ModoFindDropDownList.SelectedIndexChanged += ModoFindDropDownList_SelectedIndexChanged;
        }

        void ModoFindDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateModo(int.Parse(ModoFindDropDownList.SelectedValue));
        }

        void ConsultarLinkButton_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        void ExportarLinkButton_Click(object sender, EventArgs e)
        {
            Exportar();
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
            this.RegionalFindDropDownList.Items.Clear();
            this.RegionalFindDropDownList.Items.Add(new ListItem("- Todos - ", "-1"));

            if (this.WebReport == null)
            {
                this.Master.ShowAlert("No se definió la clase manejadora del reporte", MsgBoxIcon.IconError);
                this.ConsultarLinkButton.Visible = false;
                return;
            }

            this.Master.Title = this.WebReport.ReportName;

            this.FechaMovimientoInicialTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd").Replace('-','/');
            this.FechaMovimientoFinalTextBox.Text = this.FechaMovimientoInicialTextBox.Text;
            this.FechaProcesoInicialTextBox.Text = this.FechaMovimientoInicialTextBox.Text;
            this.FechaProcesoFinalTextBox.Text = this.FechaMovimientoInicialTextBox.Text;

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

            UpdateModo(int.Parse(ModoFindDropDownList.SelectedValue));
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
                  
                    // Importantisimo!!!!!!!
                    //var query = dbmBanagrario.DataBase.LastQuery;

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
            if (!this.Validar()) return;
            try
            {
                var Parametros = new Dictionary<string, object> {{"ConnectionString", this.ConnectionString}, {"IdRegional", short.Parse(this.RegionalFindDropDownList.SelectedValue)}, {"NombreRegional", this.RegionalFindDropDownList.SelectedItem.Text}, {"IdCOB", short.Parse(this.COBFindDropDownList.SelectedValue)}, {"NombreCOB", this.COBFindDropDownList.SelectedItem.Text}, {"IdOficina", int.Parse(this.OficinaFindDropDownList.SelectedValue)}, {"NombreOficina", this.OficinaFindDropDownList.SelectedItem.Text}, {"FechaMovimientoInicial", this.FechaMovimientoInicialTextBox.Text}, {"FechaMovimientoFinal", this.FechaMovimientoFinalTextBox.Text}, {"FechaProcesoInicial", this.FechaProcesoInicialTextBox.Text}, {"FechaProcesoFinal", this.FechaProcesoFinalTextBox.Text}, {"Modo", int.Parse(this.ModoFindDropDownList.SelectedValue)}, {"Login", this.MiharuSession.Usuario.Login}, {"IdUsuario", this.MiharuSession.Usuario.id}};

                this.WebReport.Launch(ref this.PageReportViewer, Parametros);
            }
            catch (Exception ex)
            {
                this.Master.ShowAlert(ex.Message, MsgBoxIcon.IconError);
            }
        }

        private void Exportar()
        {
            if (!this.Validar()) return;

            var IdRegional = short.Parse(this.RegionalFindDropDownList.SelectedValue);
            var idCOB = short.Parse(this.COBFindDropDownList.SelectedValue);
            var IdOficina = int.Parse(this.OficinaFindDropDownList.SelectedValue);
            var FechaMovimientoInicial = this.FechaMovimientoInicialTextBox.Text;
            var FechaMovimientoFinal = this.FechaMovimientoFinalTextBox.Text;
            var FechaProcesoInicial = this.FechaProcesoInicialTextBox.Text;
            var FechaProcesoFinal = this.FechaProcesoFinalTextBox.Text;
            var Modo = int.Parse(this.ModoFindDropDownList.SelectedValue);

            DBAgrario.DBAgrarioDataBaseManager dbmBanagrario = null;

            try
            {
                dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(ConnectionString.BanAgrario);
                dbmBanagrario.Connection_Open(1);

                var DatosDataTable = dbmBanagrario.SchemaReport.PA_29_Devolucion_Canje_Recibido_Texto.DBExecute(IdRegional,
                    idCOB,
                    IdOficina,
                    FechaMovimientoInicial,
                    FechaMovimientoFinal,
                    FechaProcesoInicial,
                    FechaProcesoFinal,
                    Modo);

                var sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);
                //GridView table = new GridView();
                //table.DataSource = DatosDataTable;
                //table.DataBind();
                //table.RenderControl(htw);

                var encabezados = string.Empty;
                foreach (DataColumn Columna  in DatosDataTable.Columns)
                {
                    encabezados += Columna.ColumnName + "|";
                }
                sw.WriteLine(encabezados);

                foreach (DataRow Fila in DatosDataTable.Rows)
                {
                    var FilaCadena = string.Empty;

                    foreach (DataColumn Columna in DatosDataTable.Columns)
                    {
                        FilaCadena += Fila[Columna.ColumnName] + "|";
                    }

                    sw.WriteLine(FilaCadena);
                }
                    
                this.MiharuSession.Pagina.Parameter["ContentName"] = "DevolucionCanjeRecibido.txt";
                this.MiharuSession.Pagina.Parameter["Content"] = sw.ToString();
                this.MiharuSession.Pagina.Parameter["ContentType"] = "text/plain";

                Master.ShowWindowNoBloqueo("../Adjuntos/show_adjunto.aspx", "Exportar", "200", "100");
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

        #endregion

        #region Funciones

        private DateTime getDate(string nFecha, bool nFinal)
        {
            var Partes = nFecha.Split('/');

            return nFinal ? new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]), 23, 59, 59) : new DateTime(int.Parse(Partes[0]), int.Parse(Partes[1]), int.Parse(Partes[2]));
        }

        private bool Validar()
        {
            if (this.ModoFindDropDownList.SelectedValue != "2" && !Slyg.Tools.DataConvert.IsDate(this.FechaMovimientoInicialTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "2" && !Slyg.Tools.DataConvert.IsDate(this.FechaMovimientoFinalTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de movimiento final debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "1" && !Slyg.Tools.DataConvert.IsDate(this.FechaProcesoInicialTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de proceso inicial debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "1" && !Slyg.Tools.DataConvert.IsDate(this.FechaProcesoFinalTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/'))
            {
                this.Master.ShowAlert("La fecha de proceso final debe tener un formáto válido", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "2" && Slyg.Tools.DataConvert.ToDate(this.FechaMovimientoInicialTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/').Value > Slyg.Tools.DataConvert.ToDate(this.FechaMovimientoFinalTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/').Value)
            {
                this.Master.ShowAlert("La fecha de movimiento inicial debe ser inferior a la fecha de movimiento final", MsgBoxIcon.IconWarning);
            }
            else if (this.ModoFindDropDownList.SelectedValue != "1" && Slyg.Tools.DataConvert.ToDate(this.FechaProcesoInicialTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/').Value > Slyg.Tools.DataConvert.ToDate(this.FechaProcesoFinalTextBox.Text, Slyg.Tools.DataConvert.EnumDateFormat.yyyyMMdd, '/').Value)
            {
                this.Master.ShowAlert("La fecha de proceso inicial debe ser inferior a la fecha de proceso final", MsgBoxIcon.IconWarning);
            }
            else
            {
                try
                {
                    var Parametros = new Dictionary<string, object> {{"ConnectionString", this.ConnectionString}, {"IdRegional", short.Parse(this.RegionalFindDropDownList.SelectedValue)}, {"NombreRegional", this.RegionalFindDropDownList.SelectedItem.Text}, {"IdCOB", short.Parse(this.COBFindDropDownList.SelectedValue)}, {"NombreCOB", this.COBFindDropDownList.SelectedItem.Text}, {"IdOficina", int.Parse(this.OficinaFindDropDownList.SelectedValue)}, {"NombreOficina", this.OficinaFindDropDownList.SelectedItem.Text}, {"FechaMovimientoInicial", this.FechaMovimientoInicialTextBox.Text}, {"FechaMovimientoFinal", this.FechaMovimientoFinalTextBox.Text}, {"FechaProcesoInicial", this.FechaProcesoInicialTextBox.Text}, {"FechaProcesoFinal", this.FechaProcesoFinalTextBox.Text}, {"Modo", int.Parse(this.ModoFindDropDownList.SelectedValue)}, {"Login", this.MiharuSession.Usuario.Login}};

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