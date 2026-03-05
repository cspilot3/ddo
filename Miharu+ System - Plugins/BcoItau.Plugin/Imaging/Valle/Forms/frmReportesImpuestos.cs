using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Imaging.Valle;
using System.Diagnostics;
using Miharu.Desktop.Controls.DesktopMessageBox;


namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    public partial class frmReportesImpuestos : Form
    {
        frmFechaRecaudo frmFechaRecaudo = null;
        string strFechaRecaudo = null;
        string strFechaTraslado = null;
        List<ReportDataSource> ltReportDataSources = new List<ReportDataSource>();
        private VallePlugin _Plugin;
        private string strErrorExcel = null;
        //SaveFileDialog saveDialog = null;
        private string _nombreReporte = "";
        private string _tipoReporte = "";
        private DataTable Matriz1 =  new DataTable();
        private DataTable Matriz2 = null;
        private string tMsjMatrices = null;
        private string StrFileName_Matrices = null;
        List<string> _ltErroresReporte = new List<string>();



        #region Constructores
        public frmReportesImpuestos(VallePlugin _plugin)
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

        private void Reporte_1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Municipios_1();
        }

        private void Reporte_2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Municipios_2();
        }

        private void matriz1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Matriz1";
            Reporte_Matriz_1();
        }

        private void matriz2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Matriz2";
            Reporte_Matriz_2();
            
        }

        private void faltantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Fantantes();
        }

        private void sobrantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Sobrantes();
        }

        private void inconsistenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Conciliacion();
        }

        private void todosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Todos();
        }

        private void dispersion1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispersion_1();
        }

        private void desperion2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispersion_2_Resumida();
        }

        private void reporteCartaEntregaFisicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_CartaEntrega();
        }

        private void reporteAgrupadoDepartamentosDiferentesDelValleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_AgrupadoDepartamentos();
        }

        private void infivalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Infivalle();
        }

        private void nOVALLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FechaRecaudo_Detallado_Novalle();
        }

        private void reporteFranquiciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteFranquicias();
        }

        private void reportesEmpaqueOperaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes_Empaque();
        }

        private void reporteFormulariosConRespuestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes_Inconsistencias();
        }

        private void backgroundWorkerReport_DoWork(object sender, DoWorkEventArgs e)
        {
            var dbmIntegration = (DBIntegration.DBIntegrationDataBaseManager)e.Argument;
            dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
            
            if (!e.Cancel)
            {
                if (this._tipoReporte == "Matriz1")
                {
                    this.backgroundWorkerReport.ReportProgress(0);
                    this.Matriz1 = dbmIntegration.SchemaBcoItau.PA_Reporte_Matriz1.DBExecute(strFechaRecaudo);

                    if (this.Matriz1.Rows.Count > 0)
                    {
                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("MATRIZ1").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);
                        this.strErrorExcel = null;

                        string FileNameReport = frmFechaRecaudo.RutaGeneral;
                        if (Genera_ReporteExcel(FileNameReport, ".xls", _nombreReporte, this.Matriz1, "Matriz1"))
                        {
                            this.tMsjMatrices = "Reporte Excel Generado con Exito!!!";
                            this.StrFileName_Matrices = FileNameReport;
                        }
                        else
                            this.tMsjMatrices = "Error al generar Excel " + Environment.NewLine + "Detalle: " + this.strErrorExcel;
                    }
                    else
                    {
                        this.tMsjMatrices = "No hay datos para esta Fecha Recaudo!!";
                        return;
                    }
                }
                else if (this._tipoReporte == "Matriz2")
                {
                    this.Matriz2 = dbmIntegration.SchemaBcoItau.PA_Reporte_Matriz2.DBExecute(strFechaRecaudo);

                    if (this.Matriz2.Rows.Count > 0)
                    {
                        _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("MATRIZ2").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                        this.strErrorExcel = null;

                        string FileNameReport = frmFechaRecaudo.RutaGeneral;
                        if (Genera_ReporteExcel(FileNameReport, ".xls", _nombreReporte, this.Matriz2, "Matriz2"))
                        {
                            this.tMsjMatrices = "Reporte Excel Generado con Exito!!!";
                            System.Diagnostics.Process.Start(FileNameReport);
                        }
                        else
                            this.tMsjMatrices = "Error al generar Excel " + Environment.NewLine + "Detalle: " + this.strErrorExcel;
                    }
                    else
                    {
                        this.tMsjMatrices = "No hay datos para esta Fecha Recaudo!!";
                        return;
                    }
                }
            }
            
            
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

        private void Reportes_Inconsistencias()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
                frmExportacionImagenes ExporImg = new frmExportacionImagenes(this._Plugin, "INCONSISTENCIAS");
                ExporImg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en Reportes_Inconsistencias()!!", ref ex);
            }
            finally
            {
                if (dbmIntegration != null)
                    dbmIntegration.Connection_Close();
            }
        }

        private void ReporteFranquicias()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
               dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
               frmFechaRecaudo = new frmFechaRecaudo(true);

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    DataTable dtFranquicias = new DataTable();

                    dtFranquicias = dbmIntegration.SchemaBcoItau.PA_Reporte_Franquicias.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));
                    string reportFormat = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTE_FRANQUICIAS").FirstOrDefault().Formato_Reporte;

                    if (reportFormat.Contains("dd"))
                    {
                        _nombreReporte = string.Format(reportFormat, DateTime.Now);
                    }
                    else
                    {
                        _nombreReporte = reportFormat;
                    }
                    string Extension = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTE_FRANQUICIAS").FirstOrDefault().Extension_Salida;

                    Genera_ReporteExcel(frmFechaRecaudo.RutaGeneral, Extension, this._nombreReporte, dtFranquicias, "Reporte Franquicias");

                    DesktopMessageBoxControl.DesktopMessageShow("Reporte Excel - Reporte Franquicias generado con Exito!!!", "Reporte Franquicias", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                    System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                }
               

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en ReporteFranquicias()!!", ref ex);
            }
            finally
            {
                if (dbmIntegration != null)
                    dbmIntegration.Connection_Close();
            }
        }

        private void Reportes_Empaque()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                

                Configuracion.frmFechaRecaudo_Doble ElegirFecha = new Configuracion.frmFechaRecaudo_Doble(true);
                ElegirFecha.ShowDialog(this);

                if (ElegirFecha.FechaFinal != null && ElegirFecha.FechaInicial != null)
                {
                    DataTable dtReportesEmpaque1 = new DataTable();
                    DataTable dtReportesEmpaque2 = new DataTable();
                    DataTable dtReportesEmpaque3 = new DataTable();

                    List<DataTable> ltEmpaques = new List<DataTable>();

                    this.Cursor = Cursors.WaitCursor;

                    dtReportesEmpaque1 = dbmIntegration.SchemaBcoItau.PA_Reporte_Empaque.DBExecute(ElegirFecha.FechaInicial, ElegirFecha.FechaFinal, 1);
                    dtReportesEmpaque1.TableName = "Reporte Empaque 1";
                    dtReportesEmpaque2 = dbmIntegration.SchemaBcoItau.PA_Reporte_Empaque.DBExecute(ElegirFecha.FechaInicial, ElegirFecha.FechaFinal, 2);
                    dtReportesEmpaque2.TableName = "Reporte Empaque 2";
                    dtReportesEmpaque3 = dbmIntegration.SchemaBcoItau.PA_Reporte_Empaque.DBExecute(ElegirFecha.FechaInicial, ElegirFecha.FechaFinal, 3);
                    dtReportesEmpaque3.TableName = "Reporte Empaque 3";

                    if (dtReportesEmpaque1.Rows.Count > 0 && dtReportesEmpaque2.Rows.Count > 0 && dtReportesEmpaque3.Rows.Count > 0)
                    {
                        ltEmpaques.Add(dtReportesEmpaque3);
                        ltEmpaques.Add(dtReportesEmpaque2);
                        ltEmpaques.Add(dtReportesEmpaque1);

                        this._nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTES_EMPAQUE").FirstOrDefault().Formato_Reporte, DateTime.Now);
                        string Extension = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTES_EMPAQUE").FirstOrDefault().Extension_Salida;

                        Genera_ReporteExcel_2(ElegirFecha.RutaGeneral, Extension, _nombreReporte, ltEmpaques, "");

                        DesktopMessageBoxControl.DesktopMessageShow("Reporte Excel - Reportes Empaque generado con Exito!!!", "Reportes Empaque", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                        System.Diagnostics.Process.Start(ElegirFecha.RutaGeneral);
                    }
                    else
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("No hay datos para este rango de Fechas!!", "Error", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en FechaRecaudo_Detallado_Novalle()!!", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void Reporte_Infivalle()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable dtDetallado_Novalle = new DataTable();

                    dtDetallado_Novalle = dbmIntegration.SchemaBcoItau.PA_Reporte_Fecha_Recaudo.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));
                    this._nombreReporte = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("FECHA_RECAUDO_DETALLADO_INFIVALLE").FirstOrDefault().Formato_Reporte;
                    string Extension = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("FECHA_RECAUDO_DETALLADO_INFIVALLE").FirstOrDefault().Extension_Salida;

                    Genera_ReporteExcel(frmFechaRecaudo.RutaGeneral, Extension, this._nombreReporte, dtDetallado_Novalle, "Reporte Infivalle");

                    DesktopMessageBoxControl.DesktopMessageShow("Reporte Excel - Reporte Detallado Infivalle generado con Exito!!!", "DETALLADO Infivalle", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                    System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en FechaRecaudo_Detallado_Novalle()!!", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void FechaRecaudo_Detallado_Novalle()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true);

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable dtDetallado_Novalle = new DataTable();

                    dtDetallado_Novalle = dbmIntegration.SchemaBcoItau.PA_Reporte_Fecha_Recaudo_Detallado.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));

                    this._nombreReporte = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("FECHA_RECAUDO_DETALLADO_NOVALLE").FirstOrDefault().Formato_Reporte;
                    string Extension = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("FECHA_RECAUDO_DETALLADO_NOVALLE").FirstOrDefault().Extension_Salida;

                    Genera_ReporteExcel(frmFechaRecaudo.RutaGeneral, Extension, this._nombreReporte, dtDetallado_Novalle, this._nombreReporte.Replace("_", " "));

                    DesktopMessageBoxControl.DesktopMessageShow("Reporte Excel - Detallado FechaRecaudo NOVALLE generado con Exito!!!", "DETALLADO NO VALLE", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                    System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en FechaRecaudo_Detallado_Novalle()!!", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void Dispersion_2_Resumida()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                frmFechaRecaudo = new frmFechaRecaudo(true);
                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable dtResumidaHoja1 = new DataTable();
                    DataTable dtResumidaHoja2 = new DataTable();

                    var resumidaGeneral = dbmIntegration.SchemaBcoItau.PA_Select_Municipios_Dispersion.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"));


                    if (resumidaGeneral.Rows.Count > 0)
                    {
                        var FiltradaResumida = resumidaGeneral.AsEnumerable().Where(x => Convert.ToDateTime(x["Fecha_Recaudo"].ToString()) < frmFechaRecaudo.Fecha_Recaudo.Date).ToList();

                        if (FiltradaResumida.Count > 0)
                        {
                            dtResumidaHoja2 = FiltradaResumida.CopyToDataTable();
                        }

                        var FiltradoFechaElegida = resumidaGeneral.AsEnumerable().Where(x => x["Fecha_Recaudo"].ToString() == frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd")).ToList();

                        if (FiltradoFechaElegida.Count > 0)
                        {
                            dtResumidaHoja1 = FiltradoFechaElegida.CopyToDataTable();
                        }

                        if (dtResumidaHoja1.Rows.Count == 0)
                        {
                            dtResumidaHoja1.Columns.Add(" ");
                        }
                        else
                        {
                            if (dtResumidaHoja1.Columns.Contains("id_Cuenta_Dispersada"))
                            {
                                dtResumidaHoja1.Columns.Remove("id_Cuenta_Dispersada");
                            }

                            if (dtResumidaHoja1.Columns.Contains("Codigo_Municipio"))
                            {
                                dtResumidaHoja1.Columns.Remove("Codigo_Municipio");
                            }

                            if (dtResumidaHoja1.Columns.Contains("Codigo_Departamento"))
                            {
                                dtResumidaHoja1.Columns.Remove("Codigo_Departamento");
                            }

                            if (dtResumidaHoja1.Columns.Contains("fk_Relacion_Cuenta_Municipio"))
                            {
                                dtResumidaHoja1.Columns.Remove("fk_Relacion_Cuenta_Municipio");
                            }

                            if (dtResumidaHoja1.Columns.Contains("fk_Estado_Dispersion"))
                            {
                                dtResumidaHoja1.Columns.Remove("fk_Estado_Dispersion");
                            }

                            if (dtResumidaHoja1.Columns.Contains("Editar"))
                            {
                                dtResumidaHoja1.Columns.Remove("Editar");
                            }

                            
   
                        }

                        if (dtResumidaHoja2.Rows.Count == 0)
                        {
                            dtResumidaHoja2.Columns.Add(" ");
                        }
                        else
                        {
                            if (dtResumidaHoja2.Columns.Contains("id_Cuenta_Dispersada"))
                            {
                                dtResumidaHoja2.Columns.Remove("id_Cuenta_Dispersada");
                            }

                            if (dtResumidaHoja2.Columns.Contains("Codigo_Municipio"))
                            {
                                dtResumidaHoja2.Columns.Remove("Codigo_Municipio");
                            }

                            if (dtResumidaHoja2.Columns.Contains("Codigo_Departamento"))
                            {
                                dtResumidaHoja2.Columns.Remove("Codigo_Departamento");
                            }

                            if (dtResumidaHoja2.Columns.Contains("fk_Relacion_Cuenta_Municipio"))
                            {
                                dtResumidaHoja2.Columns.Remove("fk_Relacion_Cuenta_Municipio");
                            }

                            if (dtResumidaHoja2.Columns.Contains("fk_Estado_Dispersion"))
                            {
                                dtResumidaHoja2.Columns.Remove("fk_Estado_Dispersion");
                            }

                            if (dtResumidaHoja2.Columns.Contains("Editar"))
                            {
                                dtResumidaHoja2.Columns.Remove("Editar");
                            }
                        }


                        List<DataTable> dtListaResumidas = new List<DataTable>();
                        dtListaResumidas.Add(dtResumidaHoja2);
                        dtListaResumidas.Add(dtResumidaHoja1);

                        dtResumidaHoja1.TableName = "Resumida_1";
                        dtResumidaHoja2.TableName = "Resumida_2";

                        Genera_ReporteExcel_2(frmFechaRecaudo.RutaGeneral, ".xlsx", "DisResumida", dtListaResumidas, "");

                        DesktopMessageBoxControl.DesktopMessageShow("Reporte Excel Generado con Exito!!!", "DISPERSIÓN RESUMIDA", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                        System.Diagnostics.Process.Start(frmFechaRecaudo.RutaGeneral);
                    }
                    else
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error, Esta consulta no tiene registros!!!", "Consulta Sin datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true); ;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en Dispersion_2_Resumida()!!", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
            
        }

        private void Dispersion_1()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtRecaudoValle = new DataTable();
            DataTable dtRecaudoMunicipiosOtros = new DataTable();
            DataTable dtRecaudoOtrosMunicipios = new DataTable();
            DataTable InformeDataTable = new DataTable();
            DataTable InformeDataTable_aux = new DataTable();
            DataTable ConsolidadoOficinas = new DataTable();
            DataTable ConsolidadoFormasPago = new DataTable();
            DataTable TiposFormularios = new DataTable();
            DataTable valoresFormularios = new DataTable();

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaRecaudo = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd");
                strFechaTraslado = frmFechaRecaudo.Fecha_Recaudo.AddDays(20).ToString("dd/MM/yyyy");


                try
                {
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("DISPERSION_DETALLADA").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();
                    InformeDataTable.Clear();
                    InformeDataTable = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 10, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);


                    //Recaudo Municipios Valle
                    dtRecaudoValle.Clear();
                    dtRecaudoValle = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 1, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Recaudo Municipios Otros
                    dtRecaudoOtrosMunicipios.Clear();
                    dtRecaudoOtrosMunicipios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 2, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Consolidado Oficinas 
                    ConsolidadoOficinas.Clear();
                    ConsolidadoOficinas = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 4, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Consolidados Formas de Pago
                    ConsolidadoFormasPago.Clear();
                    ConsolidadoFormasPago = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 5, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Tipos Formularios
                    TiposFormularios.Clear();
                    TiposFormularios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 6, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Valores Formularios
                    valoresFormularios.Clear();
                    valoresFormularios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 7, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Reporte1_BBogotaImpuestos";
                    InformeReportDataSource.Value = InformeDataTable;
                    ltReportDataSources.Add(InformeReportDataSource);

                    ReportDataSource InformeReportDataSource_2 = new ReportDataSource();
                    InformeReportDataSource_2.Name = "RECAUDO_MUNICIPIOS_VALLE";
                    InformeReportDataSource_2.Value = dtRecaudoValle;
                    ltReportDataSources.Add(InformeReportDataSource_2);

                    ReportDataSource InformeReportDataSource_3 = new ReportDataSource();
                    InformeReportDataSource_3.Name = "RECAUDO_MUNICIPIOS_OTROS";
                    InformeReportDataSource_3.Value = dtRecaudoOtrosMunicipios;
                    ltReportDataSources.Add(InformeReportDataSource_3);

                    ReportDataSource InformeReportDataSource_4 = new ReportDataSource();
                    InformeReportDataSource_4.Name = "CONSOLIDADO_OFICINAS";
                    InformeReportDataSource_4.Value = ConsolidadoOficinas;
                    ltReportDataSources.Add(InformeReportDataSource_4);

                    ReportDataSource InformeReportDataSource_5 = new ReportDataSource();
                    InformeReportDataSource_5.Name = "CONSOLIDADO_FORMAS_DE_PAGO";
                    InformeReportDataSource_5.Value = ConsolidadoFormasPago;
                    ltReportDataSources.Add(InformeReportDataSource_5);

                    ReportDataSource InformeReportDataSource_6 = new ReportDataSource();
                    InformeReportDataSource_6.Name = "TIPOS_FORMULARIOS";
                    InformeReportDataSource_6.Value = TiposFormularios;
                    ltReportDataSources.Add(InformeReportDataSource_6);

                    ReportDataSource InformeReportDataSource_7 = new ReportDataSource();
                    InformeReportDataSource_7.Name = "VALORES_FORMULARIOS";
                    InformeReportDataSource_7.Value = valoresFormularios;
                    ltReportDataSources.Add(InformeReportDataSource_7);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha_Proceso", Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("Fecha_Traslado", Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("Fecha_Recaudo", Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy")));


                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoItau.Plugin.Imaging.Valle.Forms.Rpts.ReporteDispersion.ReporteDispersion.rdlc";
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
                    //dbCore.Connection_Close();
                }
            }
        }

        private void Reporte_Todos()
        {
            Slyg.Tools.Progress.FormProgress ProgressForm = new Slyg.Tools.Progress.FormProgress();

            //Iniciar proceso                

            ProgressForm.Process = "";
            ProgressForm.Action = "";
            ProgressForm.ValueProcess = 0;
            ProgressForm.ValueAction = 0;

            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                var Reportes = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBGet(null);
                ProgressForm.MaxValueProcess = Reportes.Count-1;
                ProgressForm.MaxValueAction = 100;
                DataTable Resumen;
                DataTable Detalle;
                DataTable dtAux;
                List<DataTable> ltDataTables;
                string StrFechaProceso = "";
                string Extension = "";
                frmFechaRecaudo = new frmFechaRecaudo(false, "PROCESO");
                string strRutaStarter = "";
                ProgressForm.Process = "Ejecutando Reportes..";

                 if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                 {
                     StrFechaProceso = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd");

                     ProgressForm.Show();
                     this.Cursor = Cursors.WaitCursor;

                     for (int i = 0; i <= Reportes.Count - 1; i++)
                     {
                         var AplicaTodos = Reportes[i]["Aplica_Todos"].ToString() == "True" ? true : false;

                         if (AplicaTodos)
                         {
                             var ReportName = Reportes[i]["Nombre_Reporte"].ToString();
                             Extension = Reportes[i]["Extension_Salida"].ToString();
                             ProgressForm.Action = "Ejecutando Reporte " + Reportes[i]["Nombre_Reporte"].ToString();
                             ProgressForm.ValueAction = i;
                             ProgressForm.ValueProcess = i;
                             Application.DoEvents();

                             _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte(ReportName).FirstOrDefault().Formato_Reporte, DateTime.Now);


                             System.Threading.Thread.Sleep(1000);

                             if (ReportName == "FALTANTES" || ReportName == "SOBRANTES")
                             {
                                 ProgressForm.ValueAction = 50;
                                 Application.DoEvents();

                                 Resumen = dbmIntegration.SchemaBcoItau.PA_Selecciona_Reporte.DBExecute(ReportName, StrFechaProceso, true);
                                 Detalle = dbmIntegration.SchemaBcoItau.PA_Selecciona_Reporte.DBExecute(ReportName, StrFechaProceso, false);
                                 ltDataTables = new List<DataTable>();
                                 ltDataTables.Add(Detalle);
                                 ltDataTables.Add(Resumen);

                                 Detalle.TableName = "Detalle";
                                 Resumen.TableName = "Resumen";

                                 Genera_ReporteExcel_2(frmFechaRecaudo.RutaGeneral, Extension, _nombreReporte, ltDataTables, "");

                                 ProgressForm.ValueAction = 75;
                                 Application.DoEvents();
                             }
                             else
                             {
                                 ProgressForm.ValueAction = 50;
                                 Application.DoEvents();

                                 dtAux = dbmIntegration.SchemaBcoItau.PA_Selecciona_Reporte.DBExecute(ReportName, StrFechaProceso, false);
                                 Genera_ReporteExcel(frmFechaRecaudo.RutaGeneral, Extension, _nombreReporte, dtAux, ReportName);

                                 ProgressForm.ValueAction = 75;
                                 Application.DoEvents();
                             }

                             ProgressForm.ValueAction = 100;
                             Application.DoEvents();

                             if (strRutaStarter == "")
                                 strRutaStarter = frmFechaRecaudo.RutaGeneral;

                             if ((ProgressForm.Cancel))
                                 throw new Exception("Operación cancelada por el usuario");
                         }

                         ProgressForm.ValueAction = 0;
                         Application.DoEvents();
                     }

                     DesktopMessageBoxControl.DesktopMessageShow("Reportes Generados con Exito!!!", "Reportes", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                     System.Diagnostics.Process.Start(strRutaStarter);
                 }   
            }
            catch (Exception ex)
            {
                ProgressForm.Hide();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ProgressForm.Visible = false;
                ProgressForm.Close();
                dbmIntegration.Connection_Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void Reporte_Conciliacion()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true, "PROCESO");

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable Conciliacion = dbmIntegration.SchemaBcoItau.PA_Reporte_Conciliacion.DBExecute();

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("CONCILIACION").FirstOrDefault().Formato_Reporte, DateTime.Now);

                    if (Genera_ReporteExcel(frmFechaRecaudo.RutaGeneral, ".xlsx", _nombreReporte, Conciliacion, "Conciliación"))
                    {
                        MessageBox.Show("Reporte Excel Generado con Exito!!!");
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

        private void Reporte_Sobrantes()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true, "PROCESO");

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable Sobrantes_Resumen = dbmIntegration.SchemaBcoItau.PA_Reporte_Sobrantes.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"), true);
                    DataTable Sobrantes_Detalle = dbmIntegration.SchemaBcoItau.PA_Reporte_Sobrantes.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"), false);

                    List<DataTable> ltDatatables = new List<DataTable>();
                    ltDatatables.Add(Sobrantes_Detalle);
                    ltDatatables.Add(Sobrantes_Resumen);

                    Sobrantes_Detalle.TableName = "Sobrantes_Detalle";
                    Sobrantes_Resumen.TableName = "Sobrantes_Resumen";

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("SOBRANTES").FirstOrDefault().Formato_Reporte, DateTime.Now);

                    if (Genera_ReporteExcel_2(frmFechaRecaudo.RutaGeneral, ".xlsx", _nombreReporte, ltDatatables, "hoja1"))
                    {
                        MessageBox.Show("Reporte Excel Generado con Exito!!!");
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

        //Actualizado
        private void Reporte_Fantantes()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(true, "PROCESO");

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    DataTable Faltantes_Resumen = dbmIntegration.SchemaBcoItau.PA_Reporte_Faltantes.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"), true);
                    DataTable Faltantes_Detalle = dbmIntegration.SchemaBcoItau.PA_Reporte_Faltantes.DBExecute(frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd"), false);

                    List<DataTable> ltDatatables = new List<DataTable>();
                    ltDatatables.Add(Faltantes_Detalle);
                    ltDatatables.Add(Faltantes_Resumen);

                    Faltantes_Detalle.TableName = "Faltantes_Detalle";
                    Faltantes_Resumen.TableName = "Faltantes_Resumen";

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("FALTANTES").FirstOrDefault().Formato_Reporte, DateTime.Now);

                    if (Genera_ReporteExcel_2(frmFechaRecaudo.RutaGeneral, ".xlsx", _nombreReporte, ltDatatables, "hoja1"))
                    {
                        MessageBox.Show("Reporte Excel Generado con Exito!!!");
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

        //Actualizado
        private void Reporte_Municipios_1()
        { 
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            DataTable dtRecaudoValle = new DataTable();
            DataTable dtRecaudoMunicipiosOtros = new DataTable();
            DataTable dtRecaudoOtrosMunicipios = new DataTable();
            DataTable InformeDataTable = new DataTable();
            DataTable InformeDataTable_aux = new DataTable();
            DataTable ConsolidadoOficinas = new DataTable();
            DataTable ConsolidadoFormasPago = new DataTable();
            DataTable TiposFormularios = new DataTable();
            DataTable valoresFormularios = new DataTable();

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaRecaudo = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd");
                strFechaTraslado = frmFechaRecaudo.Fecha_Recaudo.AddDays(20).ToString("dd/MM/yyyy");


                try
                {
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("WORD1").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

                    this.ltReportDataSources.Clear();
                    InformeDataTable.Clear();
                    InformeDataTable = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 10, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);


                    //Recaudo Municipios Valle
                    dtRecaudoValle.Clear();
                    dtRecaudoValle = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 1, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Recaudo Municipios Otros
                    dtRecaudoOtrosMunicipios.Clear();
                    dtRecaudoOtrosMunicipios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 2, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Consolidado Oficinas 
                    ConsolidadoOficinas.Clear();
                    ConsolidadoOficinas = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 4, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Consolidados Formas de Pago
                    ConsolidadoFormasPago.Clear();
                    ConsolidadoFormasPago = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 5, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Tipos Formularios
                    TiposFormularios.Clear();
                    TiposFormularios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 6, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    //Valores Formularios
                    valoresFormularios.Clear();
                    valoresFormularios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 7, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Reporte1_BBogotaImpuestos";
                    InformeReportDataSource.Value = InformeDataTable;
                    ltReportDataSources.Add(InformeReportDataSource);

                    ReportDataSource InformeReportDataSource_2 = new ReportDataSource();
                    InformeReportDataSource_2.Name = "RECAUDO_MUNICIPIOS_VALLE";
                    InformeReportDataSource_2.Value = dtRecaudoValle;
                    ltReportDataSources.Add(InformeReportDataSource_2);

                    ReportDataSource InformeReportDataSource_3 = new ReportDataSource();
                    InformeReportDataSource_3.Name = "RECAUDO_MUNICIPIOS_OTROS";
                    InformeReportDataSource_3.Value = dtRecaudoOtrosMunicipios;
                    ltReportDataSources.Add(InformeReportDataSource_3);

                    ReportDataSource InformeReportDataSource_4 = new ReportDataSource();
                    InformeReportDataSource_4.Name = "CONSOLIDADO_OFICINAS";
                    InformeReportDataSource_4.Value = ConsolidadoOficinas;
                    ltReportDataSources.Add(InformeReportDataSource_4);

                    ReportDataSource InformeReportDataSource_5 = new ReportDataSource();
                    InformeReportDataSource_5.Name = "CONSOLIDADO_FORMAS_DE_PAGO";
                    InformeReportDataSource_5.Value = ConsolidadoFormasPago;
                    ltReportDataSources.Add(InformeReportDataSource_5);

                    ReportDataSource InformeReportDataSource_6 = new ReportDataSource();
                    InformeReportDataSource_6.Name = "TIPOS_FORMULARIOS";
                    InformeReportDataSource_6.Value = TiposFormularios;
                    ltReportDataSources.Add(InformeReportDataSource_6);

                    ReportDataSource InformeReportDataSource_7 = new ReportDataSource();
                    InformeReportDataSource_7.Name = "VALORES_FORMULARIOS";
                    InformeReportDataSource_7.Value = valoresFormularios;
                    ltReportDataSources.Add(InformeReportDataSource_7);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha_Proceso", Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("Fecha_Traslado", Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy")));
                    Parametros.Add(new ReportParameter("Fecha_Recaudo", Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy")));


                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoItau.Plugin.Imaging.Impuestos.Forms.Rpts.ReporteMunicipios_BancoItau.Reporte1_Municipios.rdlc.rdlc";
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
                    //dbCore.Connection_Close();
                }
            }
        }

        private void Reporte_Municipios_2()
        {
            DataTable dtDetalladoValle = new DataTable();
            DataTable dtDetalladoValle_aux = new DataTable();
            DataTable dtDetalladoOtrosMunicipios = new DataTable();
            DataTable dtDetalladoOtrosMunicipios_aux = new DataTable();

            frmFechaRecaudo frmFechaRecaudo = new frmFechaRecaudo();
            List<ReportDataSource> ltReportDataSources = new List<ReportDataSource>();
            this.reportViewerImpuestos.LocalReport.DataSources.Clear();

            //DBCore.DBCoreDataBaseManager dbCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK)) {
	            
                try {
		            strFechaRecaudo = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd");
                    //dbCore.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("WORD1").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo);

		            //Detallado Valle
		            dtDetalladoValle.Clear();
                    dtDetalladoValle = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 8, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

		            //Detallado Otros Municipios
		            dtDetalladoOtrosMunicipios.Clear();
                    dtDetalladoOtrosMunicipios = dbmIntegration.SchemaBcoItau.PA_Reporte_Auxiliares.DBExecute("", 9, strFechaRecaudo, this._Plugin.Manager.ImagingGlobal.Entidad);

                    strFechaRecaudo = Convert.ToDateTime(strFechaRecaudo).ToString("dd/MM/yyyy");
                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "DETALLADO_MUNICIPIOS_VALLE";
                    InformeReportDataSource.Value = dtDetalladoValle;
                    ltReportDataSources.Add(InformeReportDataSource);

                    ReportDataSource InformeReportDataSource_2 = new ReportDataSource();
                    InformeReportDataSource_2.Name = "DETALLADO_OTROS_MUNICIPIOS";
                    InformeReportDataSource_2.Value = dtDetalladoOtrosMunicipios;
                    ltReportDataSources.Add(InformeReportDataSource_2);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha_Proceso", strFechaRecaudo));
                    Parametros.Add(new ReportParameter("Fecha_Recaudo", strFechaRecaudo));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = Convert.ToDateTime(strFechaRecaudo).ToString("ddMMyyyy") + "-L_SOPOR_REPORTE_2";
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoItau.Plugin.Imaging.Valle.Forms.Rpts.ReporteMunicipios2_Detallado_BBogota.Reporte2_BBogotaImpuestos.rdlc";
                    this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    this.reportViewerImpuestos.LocalReport.DataSources.Clear();
                    this.reportViewerImpuestos.Visible = true;

                    foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    {
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    }
                    this.reportViewerImpuestos.RefreshReport();
	            } catch (Exception ex) {
		            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		            return;
	            } finally {
                    //if ((dbCore != null))
                    //    dbCore.Connection_Close();

                    if ((dbmIntegration != null))
                        dbmIntegration.Connection_Close();
	            }
            }
        }

        private void Reporte_Matriz_1()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            CheckForIllegalCrossThreadCalls = false;

            frmFechaRecaudo = new frmFechaRecaudo(true);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                try
                {
                    strFechaRecaudo = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd");
                    this.Cursor = Cursors.WaitCursor;
                    Genera_matriz1_Background(strFechaRecaudo, dbmIntegration);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if ((dbmIntegration != null))
                        dbmIntegration.Connection_Close();
                }
            }

            
        }

        private void Genera_matriz1_Background(string strFechaRecaudo, DBIntegration.DBIntegrationDataBaseManager  dbmIntegration)
        {
            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                //Ejecuta faltantes
                dbmIntegration.SchemaBcoItau.PA_Reporte_Faltantes.DBExecute(strFechaRecaudo, true);

                var Faltantes_FechaRecaudo = dbmIntegration.SchemaBcoItau.PA_Reporte_Faltantes.DBExecute(strFechaRecaudo, true);
                bool TieneFaltantes = false;
                bool conFirmaFaltantes = true;

                if (Faltantes_FechaRecaudo.Rows.Count > 0)
                {
                    TieneFaltantes = true;
                }

                if (TieneFaltantes)
                {
                    if (DesktopMessageBoxControl.DesktopMessageShow("La fecha de recaudo seleccionada tiene Faltantes, ¿Desea seguir generando el Reporte?", "Faltantes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, false) == System.Windows.Forms.DialogResult.OK)
                    {
                        conFirmaFaltantes = true;
                    }
                    else
                    {
                        conFirmaFaltantes = false;
                    }
                }

                if (conFirmaFaltantes)
                {
                    if (!this.backgroundWorkerReport.IsBusy)
                    {
                        this.pnlBackground.Visible = true;
                        this.pictureBoxCargando.Visible = true;
                        this.backgroundWorkerReport.RunWorkerAsync(dbmIntegration);
                    }
                }
            }
            catch (Exception ex)
            {
                this._ltErroresReporte.Add("Error, Genera Reporte " + ex.Message + " - " + DateTime.Now);
            }
            
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

        private void Reporte_Matriz_2()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            CheckForIllegalCrossThreadCalls = false;
            frmFechaRecaudo = new frmFechaRecaudo(true);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                try
                {
                    strFechaRecaudo = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyy/MM/dd");
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    //Ejecuta faltantes
                    var Faltantes_FechaRecaudo = dbmIntegration.SchemaBcoItau.PA_Reporte_Faltantes.DBExecute(strFechaRecaudo, true);
                    bool TieneFaltantes = false;
                    bool conFirmaFaltantes = true;

                    if (Faltantes_FechaRecaudo.Rows.Count > 0)
                    {
                        TieneFaltantes = true;
                    }

                    if (TieneFaltantes)
                    {
                        if (DesktopMessageBoxControl.DesktopMessageShow("La fecha de recaudo seleccionada tiene Faltantes, ¿Desea seguir generando el Reporte?", "Faltantes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, false) == System.Windows.Forms.DialogResult.OK)
                        {
                            conFirmaFaltantes = true;
                        }
                        else
                        {
                            conFirmaFaltantes = false;
                        }
                    }

                    if (conFirmaFaltantes)
                    {
                        if (!this.backgroundWorkerReport.IsBusy)
                        {
                            this.pnlBackground.Visible = true;
                            this.pictureBoxCargando.Visible = true;
                            this.backgroundWorkerReport.RunWorkerAsync(dbmIntegration);
                        } 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if ((dbmIntegration != null))
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
                if ((DtFinal == null | DtFinal.Rows.Count == 0)) {
	                DtFinal = new DataTable();
	                DtFinal.Columns.Add(" ");
	                List<string> ItemsDinamicos = new List<string>();

	                foreach (DataColumn row_loopVariable in DtFinal.Columns) {
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

        public void Reporte_CartaEntrega()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            string Fecha_Actual = "";
            string Mes = "";
            string Dias = "";

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                DataTable dtReporteCarta_R1 = new DataTable();
                DataTable dtReporteCarta_R2 = new DataTable();
                List<ReportParameter> Parametros = new List<ReportParameter>();
                List<string> StrFechasPresentadas = new List<string>();
                Dictionary<string, long> DicTotalPresentadas = new  Dictionary<string, long>();
                List<string> StrTotalPresentadas = new List<string>();
                long SumCantidadPorDia = 0;
                string MensajeFechas = "";
                string MensajeEntregado_Fisicos = "";

                Configuracion.frmFechaRecaudo_Doble ElegirFecha = new Configuracion.frmFechaRecaudo_Doble();
                ElegirFecha.ShowDialog(this);

                if (ElegirFecha.FechaFinal != null && ElegirFecha.FechaInicial != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.ltReportDataSources.Clear();
                    dtReporteCarta_R1.Clear();
                    dtReporteCarta_R2.Clear();

                    dtReporteCarta_R1 = dbmIntegration.SchemaBcoItau.PA_Reporte_Carta_Entrega_Fisico_R1.DBExecute(ElegirFecha.FechaInicial, ElegirFecha.FechaFinal);
                    dtReporteCarta_R2 = dbmIntegration.SchemaBcoItau.PA_Reporte_Carta_Entrega_Fisico_R2.DBExecute(ElegirFecha.FechaInicial, ElegirFecha.FechaFinal);

                    this._nombreReporte = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTE_CARTA_ENTREGA_FISICO").FirstOrDefault().Formato_Reporte;

                    Fecha_Actual = DateTime.Now.ToString("MMMM").ToUpperInvariant() +" "+ DateTime.Now.Day.ToString()+ " de "+" "+ DateTime.Now.Year.ToString();

                    if (dtReporteCarta_R1.Rows.Count > 0)
                    {
                        var MesesAgrupados = dtReporteCarta_R1.AsEnumerable().Select(x => x["MES"].ToString()).Distinct().ToList();

                        for (int i = 0; i < MesesAgrupados.Count; i++)
                        {
                            Dias = "";
                            Mes = MesesAgrupados[i];
                            SumCantidadPorDia = 0;

                            for (int j = 0; j < dtReporteCarta_R1.Rows.Count; j++)
                            {
                                if (dtReporteCarta_R1.Rows[j]["MES"].ToString() == Mes)
                                {
                                    Dias = Dias + ", " + dtReporteCarta_R1.Rows[j]["DIA"].ToString();
                                    SumCantidadPorDia = SumCantidadPorDia + Convert.ToInt64(dtReporteCarta_R1.Rows[j]["REGISTROS"].ToString());
                                }
                            }

                            StrFechasPresentadas.Add(Dias + " de " + Mes + " de " + DateTime.Now.Year.ToString() + Environment.NewLine);
                            DicTotalPresentadas.Add(Mes, Convert.ToInt64(SumCantidadPorDia));
                        }

                    }
                    else
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("No hay datos para este rango de Fechas!!", "Error", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    foreach (var itemDictionary in DicTotalPresentadas)
                    {
                        StrTotalPresentadas.Add("Entrega Total de Formularios Fisicos de " + itemDictionary.Key + " : " + itemDictionary.Value + Environment.NewLine);
                    }

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "Carta_Entrega_Fisico_R1";
                    InformeReportDataSource.Value = dtReporteCarta_R1;
                    ltReportDataSources.Add(InformeReportDataSource);

                    ReportDataSource InformeReportDataSource_2 = new ReportDataSource();
                    InformeReportDataSource_2.Name = "Carta_Entrega_Fisico_R2";
                    InformeReportDataSource_2.Value = dtReporteCarta_R2;
                    ltReportDataSources.Add(InformeReportDataSource_2);

                    foreach (var item in StrFechasPresentadas)
                    {
                        MensajeFechas = MensajeFechas + item.ToString(); 
                    }

                    MensajeFechas = Environment.NewLine + MensajeFechas;

                    foreach (var item in StrTotalPresentadas)
                    {
                        MensajeEntregado_Fisicos = MensajeEntregado_Fisicos + item.ToString();
                    }

                    ReportParameter Fecha_ActualParameter = new ReportParameter("Fecha_Actual", Fecha_Actual);
                    Parametros.Add(Fecha_ActualParameter);

                    ReportParameter FechasPresentadas = new ReportParameter("Fechas_Presentadas", MensajeFechas);
                    Parametros.Add(FechasPresentadas);

                    ReportParameter TotalPresentadas = new ReportParameter("Total_Presentadas", MensajeEntregado_Fisicos);
                    Parametros.Add(TotalPresentadas);

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoItau.Plugin.Imaging.Valle.Forms.Rpts.Reporte_Carta_EntregaFisico.Reporte_Carta_EntregaFisico.rdlc";
                    this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                    this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                    foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                    {
                        this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                    }

                    this.reportViewerImpuestos.Visible = true;
                    this.Cursor = Cursors.Default;
                    this.reportViewerImpuestos.RefreshReport();

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                DesktopMessageBoxControl.DesktopMessageShow("Error en Reporte_CartaEntrega()!!",ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void Reporte_AgrupadoDepartamentos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                DataTable dtAgrupadosNoValle = new DataTable();
                List<ReportParameter> Parametros = new List<ReportParameter>();

                Configuracion.frmFechaRecaudo_Doble ElegirFecha = new Configuracion.frmFechaRecaudo_Doble();
                ElegirFecha.ShowDialog(this);

                if (ElegirFecha.FechaFinal != null && ElegirFecha.FechaInicial != null)
                {
                    dtAgrupadosNoValle = dbmIntegration.SchemaBcoItau.PA_Reporte_Agrupado_Departamentos.DBExecute(ElegirFecha.FechaInicial, ElegirFecha.FechaFinal);
                }
                else
                {
                    DesktopMessageBoxControl.DesktopMessageShow("No hay datos para este rango de Fechas!!", "Error", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                    this.Cursor = Cursors.Default;
                    return;
                }

                this.reportViewerImpuestos.LocalReport.DataSources.Clear();
                ltReportDataSources.Clear();
                ReportDataSource InformeReportDataSource = new ReportDataSource();
                InformeReportDataSource.Name = "Agrupados_NOVALLE";
                InformeReportDataSource.Value = dtAgrupadosNoValle;
                ltReportDataSources.Add(InformeReportDataSource);

                this._nombreReporte = dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTE_DETALLADO_DPTOS_NOVALLE").FirstOrDefault().Formato_Reporte;

                this.reportViewerImpuestos.Reset();
                this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte;
                this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoItau.Plugin.Imaging.Valle.Forms.Rpts.Reporte_Agrupado_Departamentos_NOVALLE.Reporte_Agrupado_Departamentos_NOVALLE.rdlc";
                //this.reportViewerImpuestos.LocalReport.SetParameters(Parametros);
                this.reportViewerImpuestos.LocalReport.DataSources.Clear();

                foreach (ReportDataSource reports_loopVariable in ltReportDataSources)
                {
                    this.reportViewerImpuestos.LocalReport.DataSources.Add(reports_loopVariable);
                }

                this.reportViewerImpuestos.Visible = true;
                this.Cursor = Cursors.Default;
                this.reportViewerImpuestos.RefreshReport();

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                DesktopMessageBoxControl.DesktopMessageShow("Error en Reporte_AgrupadoDepartamentos()!!", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
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
