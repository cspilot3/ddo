using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Imaging.GobernacionAntioquia;
using System.Diagnostics;
using Miharu.Desktop.Controls.DesktopMessageBox;
using System.IO;


namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms
{
    public partial class frmReportesImpuestos : Form
    {
        frmFechaRecaudo frmFechaRecaudo = null;
        frmFechaRecaudoCiudad frmFechaRecaudoCiudad = null;
        string strFechaRecaudo = null;
        string strFechaTraslado = null;
        List<ReportDataSource> ltReportDataSources = new List<ReportDataSource>();
        private GobernacionAntioquiaPlugin _Plugin;
        private string strErrorExcel = null;
        SaveFileDialog saveDialog = null;
        private string _nombreReporte = "";
        private string _tipoReporte = "";
        private DataTable Matriz1 = new DataTable();
        private DataTable Matriz2 = null;
        private string tMsjMatrices = null;
        private string StrFileName_Matrices = null;
        List<string> _ltErroresReporte = new List<string>();
        DateTime FechaInicio = new DateTime();
        DateTime FechaFin = new DateTime();

        #region Constructores

        public frmReportesImpuestos(GobernacionAntioquiaPlugin _plugin)
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

        private void ConsolidadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Consolidado";
            Reporte_Consolidado();
        }

        private void PagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Pagos";
            Reporte_Pagos();

        }

        private void SistematizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Sistematizacion";
            Reporte_Sistematizacion();
        }

        private void TarjetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Tarjetas";
            Reporte_Tarjetas();
        }

        private void CuadreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Cuadre";
            Reporte_Cuadre();
        }


        private void cuadreConsolidadoTarjetastoolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Cuadre consolidado tarjetas";
            Reporte_Cuadre_Consolidado_Tarjetas();
        }

        private void Dispercion100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Dispercion100";
            Reporte_Dispercion100();
        }

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

        /*Eventos Otros Reportes*/

        private void faltantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Faltantes_Sobrantes("FALTANTES");
        }

        private void sobrantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Faltantes_Sobrantes("SOBRANTES");
        }

        private void DispercionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispercion();
        }

        private void InformeConsolidadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_InformeConsolidado();
        }

        private void InformeCiudadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_InformeCiudad();
        }

        private void SistematizacionCorregidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Sistematizacion_Corregida();
        }

        private void DispersionAutoMunicipiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoMunicipios();
        }

        private void DispersionAutoDepartamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoDepartamentos();
        }

        private void DispersionAutoSistematizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Dispersion_AutoSistematizacion();
        }

        private void DetalleRechazosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Detallado_Rechazos();
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
                    dt = dbmIntegration.SchemaBcoPopular.PA_Reporte_Detallado_Registros_Rechazados_Antioquia.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

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
                        this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_DetalladoRechazos.Reporte_DetalladoRechazos.rdlc";
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

        private void Reporte_Tarjetas()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtTarjetas = new DataTable();

            string strFecha;

            frmFechaRecaudo = new frmFechaRecaudo(false, "FECHA");
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("TARJETAS").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    dtTarjetas.Clear();
                    dtTarjetas = dbmIntegration.SchemaBcoPopular.PA_Reporte_Tarjetas.DBExecute(strFecha, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Tarjetas";
                    InformeReportDataSource.Value = dtTarjetas;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Tarjetas.Reporte_Tarjetas.rdlc";
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

        private void Reporte_Cuadre()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtCuadre = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("CUADRE").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    ////Reporte Consolidado
                    dtCuadre.Clear();
                    dtCuadre = dbmIntegration.SchemaBcoPopular.PA_Reporte_Cuadre.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Cuadre";
                    InformeReportDataSource.Value = dtCuadre;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Cuadre.Reporte_Cuadre.rdlc";
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

        private void Reporte_Cuadre_Consolidado_Tarjetas()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            
            DataTable dtCuadre = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("CUADRE_CONSOLIDADO_TARJETAS").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    ////Reporte Consolidado
                    dtCuadre.Clear();
                    dtCuadre = dbmIntegration.SchemaBcoPopular.PA_Reporte_Cuadre_Consolidado_Tarjetas.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Cuadre";
                    InformeReportDataSource.Value = dtCuadre;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));
                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Cuadre_Consolidado_Tarjetas.Reporte_Cuadre_Consolidado_Tarjetas.rdlc";
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

        private void Reporte_Dispercion100()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable Dispercion100 = new DataTable();

            string strFecha;
            string strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false, "FECHA");
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("Dispercion100").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    Dispercion100.Clear();
                    Dispercion100 = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispercion100.DBExecute(strFecha, strFecha2, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Dispercion100";
                    InformeReportDataSource.Value = Dispercion100;
                    ltReportDataSources.Add(InformeReportDataSource);

                    //List<ReportParameter> Parametros = new List<ReportParameter>();
                    //Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Dispercion100.Reporte_Dispercion100.rdlc";
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

        private void Reporte_DispercionSistematizacion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable DispercionSistematizacion = new DataTable();

            string strFecha;
            string strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false, "FECHA");
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");

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
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_DispercionSistematizacion.Reporte_DispercionSistematizacion.rdlc";
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

        private void Reporte_DispercionDepto()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable DispercionDepto = new DataTable();

            string strFecha;
            string strFecha2;

            frmFechaRecaudo = new frmFechaRecaudo(false, "FECHA");
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");

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
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_DispercionDepto.Reporte_DispercionDepto.rdlc";
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

            frmFechaRecaudo = new frmFechaRecaudo(false, "FECHA");
            frmFechaRecaudo.lblFechaRecaudo.Text = "Fecha Recuado";
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFecha = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFecha2 = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");

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
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_DispercionMunicipio.Reporte_DispercionMunicipio.rdlc";
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

        private void Reporte_Sistematizacion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtConsolidado = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("SISTEMATIZACION").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Sistematizacion.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Sistematizacion";
                    InformeReportDataSource.Value = dtConsolidado;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("ddMMyyyy")));
                    Parametros.Add(new ReportParameter("Fecha2", frmFechaRecaudo.Fecha_Recaudo2.ToString("ddMMyyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Sistematizacion.Reporte_Sistematizacion.rdlc";
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

        private void Reporte_InformeConsolidado()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            DataTable dtConsolidado = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Informe_Consolidado.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //DataTable Faltantes_Resumen = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(1, false);
                    //DataTable Faltantes_Detalle = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(1, true);
                    if (dtConsolidado.Rows.Count > 0)
                    {
                        List<DataTable> ltDatatables = new List<DataTable>();
                        ltDatatables.Add(dtConsolidado);
                        //ltDatatables.Add(Faltantes_Resumen);

                        dtConsolidado.TableName = "InformeConsolidado";
                        //Faltantes_Resumen.TableName = "Faltantes_Resumen";

                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_MENSUAL_CONSOLIDADO").FirstOrDefault().Formato_Reporte, DateTime.Now);
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
                        MessageBox.Show("No se encontraron registros para el informe");
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

        private void Reporte_Dispercion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            DataTable dtConsolidado = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispercion_Quincenal.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //DataTable Faltantes_Resumen = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(1, false);
                    //DataTable Faltantes_Detalle = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(1, true);
                    if (dtConsolidado.Rows.Count > 0)
                    {
                        List<DataTable> ltDatatables = new List<DataTable>();
                        ltDatatables.Add(dtConsolidado);
                        //ltDatatables.Add(Faltantes_Resumen);

                        dtConsolidado.TableName = "Dispercion";
                        //Faltantes_Resumen.TableName = "Faltantes_Resumen";

                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DISPERSION_QUINCENAL").FirstOrDefault().Formato_Reporte, DateTime.Now);
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
                        MessageBox.Show("No se encontraron registros para el informe");
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

        public void Reporte_InformeCiudad()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            DataTable dtConsolidado = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudoCiudad = new frmFechaRecaudoCiudad(true);

                DataTable x = new DataTable();
                DataTable x2 = new DataTable();

                //dbmIntegration.SchemaBcoPopular.TBL_Municipio.DBFill(x,1);

                dbmIntegration.SchemaBcoPopular.TBL_Municipio.DBFillByAsume_Comision(x, true);
                dbmIntegration.SchemaBcoPopular.TBL_Municipio.DBFillByAsume_Comision(x2, false);

                x.Merge(x2);

                frmFechaRecaudoCiudad.cbCiudad.DataSource = x;
                frmFechaRecaudoCiudad.cbCiudad.DisplayMember = "Nombre_Municipio";
                frmFechaRecaudoCiudad.cbCiudad.ValueMember = "id_Municipio";

                object Municipio;

                if ((frmFechaRecaudoCiudad.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudoCiudad.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudoCiudad.Fecha_Recaudo2.ToString("yyyyMMdd");
                    Municipio = frmFechaRecaudoCiudad.Municipio;

                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Informe_Ciudad.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, int.Parse(Municipio.ToString()));
                    if (dtConsolidado.Rows.Count > 0)
                    {
                        List<DataTable> ltDatatables = new List<DataTable>();
                        ltDatatables.Add(dtConsolidado);

                        dtConsolidado.TableName = "InformeCiudad";

                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_MENSUAL_CIUDAD").FirstOrDefault().Formato_Reporte, DateTime.Now);
                        _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DISPERSION_QUINCENAL").FirstOrDefault().Extension_Salida;

                        if (_tipoReporte != ".xlsx" && _tipoReporte != ".xls")
                        {
                            _tipoReporte = ".xlsx";
                        }

                        if (Genera_ReporteExcel_2(frmFechaRecaudoCiudad.RutaGeneral, _tipoReporte, _nombreReporte, ltDatatables, "hoja1"))
                        {
                            MessageBox.Show("Reporte Excel Generado con Exito!!!");
                            System.Diagnostics.Process.Start(frmFechaRecaudoCiudad.RutaGeneral);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros para el informe");
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

        private void Reporte_Faltantes_Sobrantes(string tipo)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true, "TODO");


                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable Resumen = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(tipo, false, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);
                    DataTable Detalle = dbmIntegration.SchemaBcoPopular.PA_Reporte_Faltantes_Sobrantes.DBExecute(tipo, true, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

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

        private void Reporte_Sistematizacion_Corregida()
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
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Sistematizacion_Corregida.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("SistematizacionCorregida").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("SistematizacionCorregida").FirstOrDefault().Extension_Salida;

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
        
        private void Reporte_Dispersion_AutoMunicipios()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(false, "PROCESO");

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;
                string strFechaArchivo;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");//frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");
                    dtConsolidado.Clear();
                    /*======================== INICIA RITM0317626====================================*/
                    //dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_Pagos_Masivos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);
                    strFechaArchivo = frmFechaRecaudo.Fecha_Recaudo.ToString("dd-MM-yyyy");
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_Pagos_Masivos_Antioquia.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);
                    //_nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoMunicipiosAntioquia").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoMunicipiosAntioquia").FirstOrDefault().Formato_Reporte, DateTime.Parse(strFechaArchivo));
                    /*======================== FIN RITM0317626======================================*/
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoMunicipiosAntioquia").FirstOrDefault().Extension_Salida;
                    if (Genera_Reporte_Plano(frmFechaRecaudo.RutaGeneral, _tipoReporte, _nombreReporte, dtConsolidado))
                    {
                        //MessageBox.Show("Reporte Generado con Exito!!!");
                        //System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                    }
                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_Pagos_Masivos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 0);
                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoMunicipiosAntioquia").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("InscripcionPagosAutoMunicipiosAntioquia").FirstOrDefault().Extension_Salida;
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

        private void Reporte_Dispersion_AutoDepartamentos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(false, "PROCESO");

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;
                string strFechaArchivo;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd"); //frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");
                    dtConsolidado.Clear();
                    /*======================== INICIA RITM0317626====================================*/
                    strFechaArchivo = frmFechaRecaudo.Fecha_Recaudo.ToString("dd-MM-yyyy");
                    //dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoDepartamentos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoDepartamentos_Antioquia.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);
                    //_nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoDepartamentos").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoDepartamentos").FirstOrDefault().Formato_Reporte, DateTime.Parse(strFechaArchivo));
                    /*======================== FIN RITM0317626======================================*/
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

                if (this.FechaInicio != new DateTime() && this.FechaFin != new DateTime())
                {
                    frmFechaRecaudo.Fecha_Recaudo = this.FechaInicio;
                    frmFechaRecaudo.Fecha_Recaudo2 = this.FechaFin;
                }

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;
                string strDiaInicial;
                string strFechaArchivo;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd"); //frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");
                    this.FechaInicio = frmFechaRecaudo.Fecha_Recaudo;
                    this.FechaFin = frmFechaRecaudo.Fecha_Recaudo2;
                    TimeSpan diferenciaFechas = this.FechaFin - this.FechaInicio;
                    if (diferenciaFechas.Days > 15)
                    {
                        MessageBox.Show("El rango de fechas debe ser inferior a 15 días", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Reporte_Dispersion_AutoSistematizacion();
                        return;
                    }
                    dtConsolidado.Clear();
                    /*======================== INICIA RITM0317626====================================*/
                    //dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoSistematizacion.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);
                    strDiaInicial = this.FechaInicio.Day.ToString();
                    strFechaArchivo = strDiaInicial + "_" + this.FechaFin.ToString("dd-MM-yyyy");
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Dispersion_AutoSistematizacion_Antioquia.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, 1);
                    //_nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoSistematizacion").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoSistematizacion").FirstOrDefault().Formato_Reporte, strFechaArchivo);
                    /*======================== FIN RITM0317626======================================*/
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

        private void Reporte_Consolidado()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtConsolidado = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("CONSOLIDADO").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    ////Reporte Consolidado
                    dtConsolidado.Clear();
                    dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Consolidado.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Consolidado";
                    InformeReportDataSource.Value = dtConsolidado;
                    ltReportDataSources.Add(InformeReportDataSource);

                    ////Valores Formularios
                    //valoresFormularios.Clear();
                    //valoresFormularios = dbmIntegration.SchemaBcoPopular.PA_Reportes_AuxiliaresBBogota_Impuestos.DBExecute("", 7, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //ReportDataSource InformeReportDataSource = new ReportDataSource();
                    //InformeReportDataSource.Name = "Reporte1_BBogotaImpuestos";
                    //InformeReportDataSource.Value = InformeDataTable;
                    //ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Consolidado.Reporte_Consolidado.rdlc";
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

        private void Reporte_Pagos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtPagos = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo2.ToString("yyyyMMdd");

                try
                {
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("PAGOS").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();

                    ////Reporte Consolidado
                    dtPagos.Clear();
                    dtPagos = dbmIntegration.SchemaBcoPopular.PA_Reporte_Pagos.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Pagos";
                    InformeReportDataSource.Value = dtPagos;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha", frmFechaRecaudo.Fecha_Recaudo.ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("Fecha2", frmFechaRecaudo.Fecha_Recaudo2.ToString("dd/MM/yyyy")));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms.Rpts.Reporte_Pagos.Reporte_Pagos.rdlc";
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
