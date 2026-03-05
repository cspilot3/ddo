using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Imaging.GobernacionSantander;
using System.Diagnostics;
using Miharu.Desktop.Controls.DesktopMessageBox;
using System.IO;


namespace BcoPopular.Plugin.Imaging.GobernacionSantander.Forms
{
    public partial class frmReportesImpuestos : Form
    {
        frmFechaRecaudo frmFechaRecaudo = null;
        frmFechaRecaudoCiudad frmFechaRecaudoCiudad = null;
        string strFechaRecaudo = null;
        string strFechaTraslado = null;
        List<ReportDataSource> ltReportDataSources = new List<ReportDataSource>();
        private GobernacionSantanderPlugin _Plugin;
        private string strErrorExcel = null;
        SaveFileDialog saveDialog = null;
        private string _nombreReporte = "";
        private string _tipoReporte = "";
        private DataTable Matriz1 = new DataTable();
        private DataTable Matriz2 = null;
        private string tMsjMatrices = null;
        private string StrFileName_Matrices = null;
        List<string> _ltErroresReporte = new List<string>();

        #region Constructores

        public frmReportesImpuestos(GobernacionSantanderPlugin _plugin)
        {
            InitializeComponent();
            this._Plugin = _plugin;
        }
        #endregion

        #region Eventos

        private void frmReportesImpuestos_Load(object sender, EventArgs e)
        {
            this.pnlBackground.Visible = false;
            this.pictureBoxCargando.Visible = false;
            this.reportViewerImpuestos.RefreshReport();
        }

        /*Eventos Reportes*/
        private void DispercionDeptoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "DispercionDepto";
            Reporte_DispercionDepto();
        }

        private void DispercionMunicipioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "DispercionMunicipio";
            Reporte_DispercionMunicipio();
        }

        private void DispercionDispercionSistematizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "DispercionSistematizacion";
            Reporte_DispercionSistematizacion();
        }

        private void TapaRadicacionFisicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "TapaRadicacionFisicos";
            Reporte_TapaRadicacionFisicos();
        }

        private void TapaRadicacionDispersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "TapaRadicacionDispersion";
            Reporte_TapaRadicacionDispersion();
        }

        /*Eventos Otros Reportes*/

        private void faltantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Faltantes_Sobrantes("FALTANTES");
        }

        private void sobrantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Faltantes_Sobrantes("SOBRANTES");
        }

        private void DispersionAutoMunicipiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoMunicipios(1);
        }

        private void InscripcionAutoMunicipiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoMunicipios(0);
        }

        private void DispersionAutoDepartamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoDepartamentos();
        }

        private void DispersionAutoSistematizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoSistematizacion();
        }

        private void DetalladoPorFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Detallado_Por_Fecha();
        }

        private void DetalleRechazosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Detallado_Rechazos();
        }

        private void DetalladoFisicoVsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Detallado_FisicoVSLog();
        }

        /*Eventos Background*/

        private void backgroundWorkerReport_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorkerReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pnlBackground.Visible = true;
            this.pictureBoxCargando.Visible = true;
        }

        private void backgroundWorkerReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.pnlBackground.Visible = false;
            this.pictureBoxCargando.Visible = false;
            if (MessageBox.Show(this.tMsjMatrices, "Matrices", MessageBoxButtons.OK, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                if (StrFileName_Matrices != null)
                {
                    System.Diagnostics.Process.Start(StrFileName_Matrices);
                }
            }
        }

        #endregion

        #region Metodos

        private void Reporte_DispercionSistematizacion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable DispercionSistematizacion = new DataTable();

            string strFecha;
            string strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispercionSistematizacion").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    DispercionSistematizacion.Clear();
                    DispercionSistematizacion = dbmIntegration.SchemaBcoPopular.PA_Reporte_DispercionSistematizacion.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "DispercionSistematizacion";
                    InformeReportDataSource.Value = DispercionSistematizacion;
                    ltReportDataSources.Add(InformeReportDataSource);

                    //List<ReportParameter> Parametros = new List<ReportParameter>();
                    //Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_DispercionSistematizacion.Reporte_DispercionSistematizacion.rdlc";
                    //this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                    foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    {
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    }

                    this.reportViewerImpuestos.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Reporte_TapaRadicacionFisicos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable TapaRadicacionFisicos = new DataTable();

            string strFecha1;
            string strFecha2;

            string concecutivo, nombre_director, nombre_gobernacion, cantidad;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha1 = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");
                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("TapaRadicacionFisicos").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    //this.ltReportDataSources.Clear();

                    TapaRadicacionFisicos.Clear();
                    TapaRadicacionFisicos = dbmIntegration.SchemaBcoPopular.PA_Reporte_TapaRadicacionFisicos.DBExecute(strFecha1, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    cantidad = TapaRadicacionFisicos.Rows[0].ItemArray[0].ToString();
                    nombre_director = TapaRadicacionFisicos.Rows[0].ItemArray[1].ToString();
                    nombre_gobernacion = TapaRadicacionFisicos.Rows[0].ItemArray[2].ToString();
                    concecutivo = TapaRadicacionFisicos.Rows[0].ItemArray[3].ToString();

                    //ReportDataSource InformeReportDataSource = new ReportDataSource();
                    //InformeReportDataSource.Name = "DispercionSistematizacion";
                    //InformeReportDataSource.Value = DispercionSistematizacion;
                    //ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", DateTime.Now.ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("FechaIni", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("FechaFin", frmFechaRecaudo.Fecha_Recaudo2.ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("cantidad", cantidad));
                    Parametros.Add(new ReportParameter("nombre_director", nombre_director));
                    Parametros.Add(new ReportParameter("nombre_gobernacion", nombre_gobernacion));
                    Parametros.Add(new ReportParameter("concecutivo", concecutivo));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_TapaRadicacionFisicos.Reporte_TapaRadicacionFisicos.rdlc";
                    this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    //this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                    //foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    //{
                    //    this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    //}

                    this.reportViewerImpuestos.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Reporte_TapaRadicacionDispersion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            DataTable TapaRadicacionFisicos_P = new DataTable();
            DataTable TapaRadicacionFisicos_A = new DataTable();
            DataTable TapaRadicacionFisicos_R = new DataTable();

            string strFecha1;
            string strFecha2;
            DataColumn column;

            string concecutivo, nombre_director, nombre_gobernacion, cuenta_rechazo;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha1 = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");
                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("TapaRadicacionDispersion").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    TapaRadicacionFisicos_P.Clear();
                    TapaRadicacionFisicos_P = dbmIntegration.SchemaBcoPopular.PA_Reporte_TapaRadicacionDispersion.DBExecute(strFecha1, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);

                    nombre_director = TapaRadicacionFisicos_P.Rows[0].ItemArray[0].ToString();
                    nombre_gobernacion = TapaRadicacionFisicos_P.Rows[0].ItemArray[1].ToString();
                    concecutivo = TapaRadicacionFisicos_P.Rows[0].ItemArray[2].ToString();
                    cuenta_rechazo = TapaRadicacionFisicos_P.Rows[0].ItemArray[3].ToString();

                    this.ltReportDataSources.Clear();

                    TapaRadicacionFisicos_A.Clear();
                    TapaRadicacionFisicos_A = dbmIntegration.SchemaBcoPopular.PA_Reporte_TapaRadicacionDispersion.DBExecute(strFecha1, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 2);
                    
                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "TapaRadicacionFisicos_A";
                    InformeReportDataSource.Value = TapaRadicacionFisicos_A;

                    ltReportDataSources.Add(InformeReportDataSource);

                    TapaRadicacionFisicos_R.Clear();
                    TapaRadicacionFisicos_R = dbmIntegration.SchemaBcoPopular.PA_Reporte_TapaRadicacionDispersion.DBExecute(strFecha1, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 3);

                    ReportDataSource InformeReportDataSource2 = new ReportDataSource();
                    InformeReportDataSource2.Name = "TapaRadicacionFisicos_R";
                    InformeReportDataSource2.Value = TapaRadicacionFisicos_R;

                    ltReportDataSources.Add(InformeReportDataSource2);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", DateTime.Now.ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("nombre_director", nombre_director));
                    Parametros.Add(new ReportParameter("nombre_gobernacion", nombre_gobernacion));
                    Parametros.Add(new ReportParameter("concecutivo", concecutivo));
                    Parametros.Add(new ReportParameter("cuenta_rechazo", cuenta_rechazo));
                    Parametros.Add(new ReportParameter("FechaDesde", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("FechaHasta", frmFechaRecaudo.Fecha_Recaudo2.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_TapaRadicacionDispersion.Reporte_TapaRadicacionDispersion.rdlc";
                    this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                    foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    {
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    }

                    this.reportViewerImpuestos.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Reporte_DispercionDepto()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable DispercionDepto = new DataTable();

            string strFecha;
            string strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispercionDepto").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    DispercionDepto.Clear();
                    DispercionDepto = dbmIntegration.SchemaBcoPopular.PA_Reporte_DispercionDepto.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "DispercionDepto";
                    InformeReportDataSource.Value = DispercionDepto;
                    ltReportDataSources.Add(InformeReportDataSource);

                    //List<ReportParameter> Parametros = new List<ReportParameter>();
                    //Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_DispercionDepto.Reporte_DispercionDepto.rdlc";
                    //this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                    foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    {
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    }

                    this.reportViewerImpuestos.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    dbmIntegration.Connection_Close();
                }
            }

        }

        private void Reporte_DispercionMunicipio()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable DispercionMunicipio = new DataTable();

            string strFecha;
            string strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispercionMunicipio").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    DispercionMunicipio.Clear();
                    DispercionMunicipio = dbmIntegration.SchemaBcoPopular.PA_Reporte_DispercionMunicipio.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "DispercionMunicipio";
                    InformeReportDataSource.Value = DispercionMunicipio;
                    ltReportDataSources.Add(InformeReportDataSource);

                    //List<ReportParameter> Parametros = new List<ReportParameter>();
                    //Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_DispercionMunicipio.Reporte_DispercionMunicipio.rdlc";
                    //this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                    foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    {
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    }

                    this.reportViewerImpuestos.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Reporte_Faltantes_Sobrantes(string tipo)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true, "TODO");


                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable Resumen = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(tipo, false, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);
                    DataTable Detalle = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(tipo, true, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    if (Detalle.Rows.Count > 0)
                    {
                        List<DataTable> ltDatatables = new List<DataTable>();
                        ltDatatables.Add(Detalle);
                        ltDatatables.Add(Resumen);

                        Detalle.TableName = tipo + "_DETALLE";
                        Resumen.TableName = tipo + "_RESUMEN";

                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte(tipo).FirstOrDefault().Formato_Reporte, DateTime.Now);
                        _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DISPERSION_QUINCENAL").FirstOrDefault().Extension_Salida;

                        if (_tipoReporte != ".xlsx" && _tipoReporte != ".xls")
                        {
                            _tipoReporte = ".xlsx";
                        }

                        if (Genera_ReporteExcel_2(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, ltDatatables, "hoja1"))
                        {
                            MessageBox.Show("Reporte Excel Generado con Exito!!!");
                            System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron " + tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void Reporte_Dispersion_AutoMunicipios(int TIPO)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                    if (TIPO == 1)
                    {
                        dtConsolidado.Clear();
                        dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_Pagos_Masivos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);

                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoMunicipios").FirstOrDefault().Formato_Reporte, DateTime.Now);
                        _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoMunicipios").FirstOrDefault().Extension_Salida;

                        if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                        {
                            MessageBox.Show("Reporte Generado con Exito!!!");
                            System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                        }
                    }
                    if (TIPO == 0)
                    {
                        dtConsolidado.Clear();
                        dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_Pagos_Masivos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 0);

                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoMunicipios").FirstOrDefault().Formato_Reporte, DateTime.Now);
                        _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoMunicipios").FirstOrDefault().Extension_Salida;

                        if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                        {
                            MessageBox.Show("Reporte Generado con Exito!!!");
                            System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void Reporte_Dispersion_AutoDepartamentos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoDepartamentos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoDepartamentos").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoDepartamentos").FirstOrDefault().Extension_Salida;

                    if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                    {
                        //MessageBox.Show("Reporte Generado con Exito!!!");
                        //System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                    }

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoDepartamentos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 0);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoDepartamentos").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoDepartamentos").FirstOrDefault().Extension_Salida;

                    if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                    {
                        MessageBox.Show("Reporte Generado con Exito!!!");
                        System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void Reporte_Dispersion_AutoSistematizacion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoSistematizacion.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoSistematizacion").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoSistematizacion").FirstOrDefault().Extension_Salida;

                    if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                    {
                        //MessageBox.Show("Reporte Generado con Exito!!!");
                        //System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                    }

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoSistematizacion.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 0);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoSistematizacion").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoSistematizacion").FirstOrDefault().Extension_Salida;

                    if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                    {
                        MessageBox.Show("Reporte Generado con Exito!!!");
                        System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void Reporte_Detallado_Por_Fecha()
        {
            //Instanciacion de la base de datos
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            //Variables auxiliares
            DataTable dt = new DataTable();
            String strFecha;
            String strFecha2;
            //String _tipoReporte;

            frmFechaRecaudo = new frmFechaRecaudo(false, "DETALLADO");

            if (this.reportViewerImpuestos.Visible == true)
            {
                this.reportViewerImpuestos.Visible = false;
            }

            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                //Captura de los parametros de filtrado del procedimiento almacenado
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");                

                try
                {
                    //Apertura base de datos
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    //Llamado al Procedimiento Almacenado
                    dt.Clear();
                    dt = dbmIntegration.SchemaBcoPopular.PA_Reporte_Detallado_Registros_Por_Fecha.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

                    if (dt.Rows.Count > 0)
                    {
                        this.reportViewerImpuestos.Visible = true;

                        //Creacion y asignacion de los datos al un ReportDataSource
                        ReportDataSource rds = new ReportDataSource("Datos", dt);
                        ReportParameter[] parameters = new ReportParameter[3];                        
                        parameters[0] = new ReportParameter("FechaIni", frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));
                        parameters[1] = new ReportParameter("FechaFin", frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyy/MM/dd"));
                        parameters[2] = new ReportParameter("FechaReporte", DateTime.Today.ToString("yyyy/MM/dd"));

                        //Preparacion del ReportViewer
                        this.reportViewerImpuestos.Reset();
                        this.reportViewerImpuestos.Clear();
                        this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_DetalladoRegistrosPorFecha.Reporte_DetalladoPorFecha.rdlc";
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(rds);
                        this.reportViewerImpuestos.LocalReport.SetParameters(parameters);
                        this.reportViewerImpuestos.RefreshReport();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros");
                    }
                }
                catch (Exception ex)
                {
                    //Captura de errores
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    //Cierre de la base de datos
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Reporte_Detallado_Rechazos()
        {
            //Instanciacion de la base de datos
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            //Variables auxiliares
            DataTable dt = new DataTable();
            String strFecha;
            String strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false, "DETALLADO");

            if (this.reportViewerImpuestos.Visible == true)
            {
                this.reportViewerImpuestos.Visible = false;
            }

            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                //Captura de los parametros de filtrado del procedimiento almacenado
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");                

                try
                {
                    //Apertura base de datos
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    //Llamado al Procedimiento Almacenado
                    dt.Clear();
                    dt = dbmIntegration.SchemaBcoPopular.PA_Reporte_Detallado_Registros_Rechazados.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

                    if (dt.Rows.Count > 0)
                    {
                        this.reportViewerImpuestos.Visible = true;

                        //Creacion y asignacion de los datos al un ReportDataSource
                        ReportDataSource rds = new ReportDataSource("Datos", dt);

                        //Array que contendrá los parámetros
                        ReportParameter[] parameters = new ReportParameter[3];
                        //Establecemos los valores de los parámetros
                        parameters[0] = new ReportParameter("FechaIni", frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));
                        parameters[1] = new ReportParameter("FechaFin", frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyy/MM/dd"));
                        parameters[2] = new ReportParameter("FechaReporte", DateTime.Today.ToString("yyyy/MM/dd"));

                        //Preparacion del ReportViewer
                        this.reportViewerImpuestos.Reset();
                        this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_DetalladoRechazos.Reporte_DetalladoRechazos.rdlc";
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(rds);
                        this.reportViewerImpuestos.LocalReport.SetParameters(parameters);
                        this.reportViewerImpuestos.RefreshReport();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros");
                    }
                }
                catch (Exception ex)
                {
                    //Captura de errores
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    //Cierre de la base de datos
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Reporte_Detallado_FisicoVSLog()
        {
            //Instanciacion de la base de datos
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            //Variables auxiliares
            DataTable dt = new DataTable();
            String strFecha;

            frmFechaRecaudo = new frmFechaRecaudo(false, "FECHA");
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha de Recaudo";
            frmFechaRecaudo.Text = "";

            if (this.reportViewerImpuestos.Visible == true)
            {
                this.reportViewerImpuestos.Visible = false;
            }

            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                //Captura de los parametros de filtrado del procedimiento almacenado
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");                

                try
                {
                    //Apertura base de datos
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    //Llamado al Procedimiento Almacenado
                    dt.Clear();
                    dt = dbmIntegration.SchemaBcoPopular.PA_Reporte_Detallado_Diferencias_Fisicos_vs_Log.DBExecute(strFecha, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

                    if (dt.Rows.Count > 0)
                    {
                        this.reportViewerImpuestos.Visible = true;

                        //Creacion y asignacion de los datos al un ReportDataSource
                        ReportDataSource rds = new ReportDataSource("Datos", dt);

                        //Array que contendrá los parámetros
                        ReportParameter[] parameters = new ReportParameter[2];
                        //Establecemos los valores de los parámetros
                        parameters[0] = new ReportParameter("FechaRecaudo", frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));
                        parameters[1] = new ReportParameter("FechaReporte", DateTime.Today.ToString("yyyy/MM/dd"));

                        //Preparacion del ReportViewer
                        this.reportViewerImpuestos.Reset();
                        this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionSantander.Forms.Rpts.Reporte_Detallado_FisicoVsLog.Reporte_DetalladoFisicosVsLog.rdlc";
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(rds);
                        this.reportViewerImpuestos.LocalReport.SetParameters(parameters);
                        this.reportViewerImpuestos.RefreshReport();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros");
                    }
                }
                catch (Exception ex)
                {
                    //Captura de errores
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    //Cierre de la base de datos
                    dbmIntegration.Connection_Close();
                }
            }
        }

        private void Genera_matriz1_Background(string strFechaRecaudo, DBIntegration.DBIntegrationDataBaseManager dbmIntegration)
        {
            //try
            //{
            //    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

            //    //Ejecuta faltantes
            //    dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes.DBExecute(strFechaRecaudo, true);

            //    var Faltantes_FechaRecaudo = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes.DBExecute(strFechaRecaudo, true);
            //    bool TieneFaltantes = false;
            //    bool conFirmaFaltantes = true;

            //    if (Faltantes_FechaRecaudo.Rows.Count > 0)
            //    {
            //        TieneFaltantes = true;
            //    }

            //    if (TieneFaltantes)
            //    {
            //        if (DesktopMessageBoxControl.DesktopMessageShow("La fecha de recaudo seleccionada tiene Faltantes, ¿Desea seguir generando el Reporte?", "Faltantes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, false) == System.Windows.Forms.DialogResult.OK)
            //        {
            //            conFirmaFaltantes = true;
            //        }
            //        else
            //        {
            //            conFirmaFaltantes = false;
            //        }
            //    }

            //    if (conFirmaFaltantes)
            //    {
            //        if (!this.backgroundWorkerReport.IsBusy)
            //        {
            //            this.pnlBackground.Visible = true;
            //            this.pictureBoxCargando.Visible = true;
            //            this.backgroundWorkerReport.RunWorkerAsync(dbmIntegration);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this._ltErroresReporte.Add("Error, Genera Reporte " + ex.Message + " - " + DateTime.Now);
            //}

        }

        private void Genera_matriz1_Background(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (this._ltErroresReporte.Count > 0)
            {
                var Errores = "";

                foreach (var Itemerror in this._ltErroresReporte)
                {
                    Errores = Errores + Environment.NewLine + Itemerror;
                }

                DesktopMessageBoxControl.DesktopMessageShow(Errores, "Errores generando el reporte", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                this._ltErroresReporte.Clear();
            }
        }

        public bool Genera_ReporteExcel(string RutaDir, string Extension, string NombreReporte, DataTable DtFinal, string nombreHojaExcel)
        {
            bool retorno = true;
            //Creae an Excel application instance
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Application excel = default(Microsoft.Office.Interop.Excel.Application);
            Microsoft.Office.Interop.Excel.Workbook worKbooK = default(Microsoft.Office.Interop.Excel.Workbook);
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT = default(Microsoft.Office.Interop.Excel.Worksheet);

            excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            worKbooK = excel.Workbooks.Add(Type.Missing);

            try
            {
                if ((DtFinal == null | DtFinal.Rows.Count == 0))
                {
                    DtFinal = new DataTable();
                    DtFinal.Columns.Add(" ");
                    List<string> ItemsDinamicos = new List<string>();

                    foreach (DataColumn row_loopVariable in DtFinal.Columns)
                    {
                        ItemsDinamicos.Add(" ");
                    }
                    DtFinal.Rows.Add(ItemsDinamicos.ToArray());
                }
                else
                {
                    if (DtFinal.Columns.Contains("Editar"))
                    {
                        DtFinal.Columns.Remove("Editar");
                    }

                    if (DtFinal.Columns.Contains("Eliminar"))
                    {
                        DtFinal.Columns.Remove("Eliminar");
                    }
                }

                worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                worKsheeT.Name = nombreHojaExcel;
                worKsheeT.Cells.NumberFormat = "@";
                worKsheeT.Columns.NumberFormat = "@";
                worKsheeT.Rows.NumberFormat = "@";

                for (int i = 1; i <= DtFinal.Columns.Count; i++)
                {
                    worKsheeT.Cells[1, i] = DtFinal.Columns[i - 1].ColumnName;

                }

                //worKsheeT.Cells.Columns.Interior.Color = System.Drawing.Color.LightBlue;
                //worKsheeT.get_Range("A1", Cell2).Interior.Color = System.Drawing.Color.LightBlue;

                dynamic dataMatriz = new object[DtFinal.Rows.Count + 1, DtFinal.Columns.Count];
                int ContadorProgress = 2;


                for (int j = 0; j <= DtFinal.Rows.Count - 1; j++)
                {
                    for (int i = 0; i <= DtFinal.Columns.Count - 1; i++)
                    {
                        if (ContadorProgress < 100)
                        {
                            //bw.ReportProgress(ContadorProgress)
                        }

                        dataMatriz[j, i] = DtFinal.Rows[j][i].ToString();
                        ContadorProgress += 2;
                    }
                }
                ContadorProgress = 2;

                worKsheeT.Cells.NumberFormat = "@";
                worKsheeT.Columns.NumberFormat = "@";
                worKsheeT.Rows.NumberFormat = "@";
                dynamic startCell = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[2, 1];
                startCell.NumberFormat = "@";
                dynamic endCell = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[DtFinal.Rows.Count + 1, DtFinal.Columns.Count];
                endCell.NumberFormat = "@";
                dynamic writeRange = worKsheeT.Range[startCell, endCell];
                writeRange.NumberFormat = "@";
                writeRange.Value2 = dataMatriz;
                worKsheeT.Columns.AutoFit();
                worKsheeT.Rows.AutoFit();
                if (RutaDir.Substring(RutaDir.Length - 1).ToString() != "\\")
                {
                    RutaDir = RutaDir + "\\";
                }

                worKbooK.SaveAs(RutaDir + NombreReporte + Extension);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.strErrorExcel = ex.Message;
                return false;
            }
            finally
            {
                worKbooK.Close();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKsheeT);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKbooK);
                excel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
            }

            return retorno;
        }

        public bool Genera_ReporteExcel_2(string RutaDir, string Extension, string NombreReporte, List<DataTable> ListaDtFinal, string nombreHojaExcel)
        {
            bool retorno = true;
            int contadorHojas = 0;
            List<string> ltSheetsHojas = new List<string>();

            //Creae an Excel application instance
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Application excel = default(Microsoft.Office.Interop.Excel.Application);
            Microsoft.Office.Interop.Excel.Workbook worKbooK = default(Microsoft.Office.Interop.Excel.Workbook);

            excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            worKbooK = excel.Workbooks.Add(Type.Missing);

            try
            {
                foreach (DataTable DtFinal in ListaDtFinal)
                {
                    if ((DtFinal == null | DtFinal.Rows.Count == 0))
                    {
                        List<string> ItemsDinamicos = new List<string>();

                        foreach (DataColumn row_loopVariable in DtFinal.Columns)
                        {
                            ItemsDinamicos.Add(" ");
                        }

                        DtFinal.Rows.Add(ItemsDinamicos.ToArray());
                    }
                }

                foreach (DataTable DtFinal in ListaDtFinal)
                {

                    Microsoft.Office.Interop.Excel.Worksheet worKsheeT = default(Microsoft.Office.Interop.Excel.Worksheet);

                    contadorHojas++;

                    worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                    worKsheeT.Name = DtFinal.TableName;
                    ltSheetsHojas.Add(DtFinal.TableName);
                    worKsheeT.Cells.NumberFormat = "@";
                    worKsheeT.Columns.NumberFormat = "@";
                    worKsheeT.Rows.NumberFormat = "@";

                    for (int i = 1; i <= DtFinal.Columns.Count; i++)
                    {
                        worKsheeT.Cells[1, i] = DtFinal.Columns[i - 1].ColumnName;

                    }

                    dynamic dataMatriz = new object[DtFinal.Rows.Count + 1, DtFinal.Columns.Count];
                    int ContadorProgress = 2;


                    for (int j = 0; j <= DtFinal.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i <= DtFinal.Columns.Count - 1; i++)
                        {
                            if (ContadorProgress < 100)
                            {
                                //bw.ReportProgress(ContadorProgress)
                            }

                            dataMatriz[j, i] = DtFinal.Rows[j][i].ToString();
                            ContadorProgress += 2;
                        }
                    }
                    ContadorProgress = 2;


                    worKsheeT.Cells.NumberFormat = "@";
                    worKsheeT.Columns.NumberFormat = "@";
                    worKsheeT.Rows.NumberFormat = "@";
                    dynamic startCell = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[2, 1];
                    startCell.NumberFormat = "@";
                    dynamic endCell = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[DtFinal.Rows.Count + 1, DtFinal.Columns.Count];
                    endCell.NumberFormat = "@";
                    dynamic writeRange = worKsheeT.Range[startCell, endCell];
                    writeRange.NumberFormat = "@";
                    writeRange.Value2 = dataMatriz;
                    worKsheeT.Columns.AutoFit();
                    worKsheeT.Rows.AutoFit();
                    worKbooK.Sheets.Add(worKsheeT);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKsheeT);
                }


                foreach (Microsoft.Office.Interop.Excel.Worksheet itemSheet in worKbooK.Sheets)
                {
                    var encontado = ltSheetsHojas.Where(x => x.ToString() == itemSheet.Name.ToString()).ToList();

                    if (encontado.Count == 0)
                    {
                        itemSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVeryHidden;
                    }

                }

                if (RutaDir.Substring(RutaDir.Length - 1).ToString() != "\\")
                {
                    RutaDir = RutaDir + "\\";
                }

                worKbooK.SaveAs(RutaDir + NombreReporte + Extension);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                KillSpecificProcess("EXCEL");
                this.strErrorExcel = ex.Message;
                return false;
            }
            finally
            {
                worKbooK.Close();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKbooK);
                excel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
            }

            return retorno;
        }

        public bool Genera_Reporte_Plano(string RutaDir, string Extension, string NombreReporte, DataTable DtFinal)
        {
            bool retorno = true;
            try
            {
                var archivo = RutaDir + "\\" + NombreReporte + Extension;

                // eliminar el fichero si ya existe
                if (System.IO.File.Exists(archivo))
                    System.IO.File.Delete(archivo);

                // crear el fichero
                using (var fileStream = System.IO.File.Create(archivo))
                {
                    StreamWriter writer = new StreamWriter(fileStream);
                    for (int j = 0; j <= DtFinal.Rows.Count - 1; j++)
                    {
                        for (int i = 0; i <= DtFinal.Columns.Count - 1; i++)
                        {
                            //var texto = new UTF8Encoding(true).GetBytes(DtFinal.Rows[j][i].ToString());
                            writer.Write(DtFinal.Rows[j][i].ToString());
                            //dataMatriz[j, i] = DtFinal.Rows[j][i].ToString();
                        }
                        writer.WriteLine("");
                    }
                    fileStream.Flush();
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.strErrorExcel = e.Message;
                return false;
            }

            return retorno;
        }

        private void KillSpecificProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName).Where(x => x.ProcessName == processName).Select(x => x);

            foreach (var process__1_loopVariable in processes)
            {
                process__1_loopVariable.Kill();
            }
        }
        #endregion

    }
}
