using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Imaging.Asistidos;
using System.Diagnostics;
using Miharu.Desktop.Controls.DesktopMessageBox;
using System.IO;
using Miharu.Desktop.Controls.DesktopReportViewer;


namespace BcoPopular.Plugin.Imaging.Asistidos.Forms
{
    public partial class frmReportesImpuestos : Form
    {
        frmFechaRecaudo frmFechaRecaudo = null;
        string strFechaRecaudo = null;
        string strFechaTraslado = null;
        List<ReportDataSource> ltReportDataSources = new List<ReportDataSource>();
        private AsistidosPlugin _Plugin;
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

        public frmReportesImpuestos(AsistidosPlugin _plugin)
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

        private void RepConciliacionPrecapturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Reporte Conciliación precaptura";
            Reporte_Conciliacio_Precaptura();
        }

        private void InformeMensualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._tipoReporte = "Informe Mensual Cliente";
            Informe_Mensual_Cliente();
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

        private void Reporte_Conciliacio_Precaptura()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            //DataTable dtPrecaptura = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo_Inicio;
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo_Fin;

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_CONCILIACION_PRECAPTURA").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo_Inicio);

                    this.ltReportDataSources.Clear();

                    ////Reporte Consolidado
                    //dtPrecaptura.Clear();
                    var dtPrecaptura = dbmIntegration.SchemaBcoPopular.PA_Reporte_Conciliacion_Precaptura.DBExecute(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, frmFechaRecaudo.Fecha_Recaudo_Inicio, frmFechaRecaudo.Fecha_Recaudo_Fin);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "dsPrecaptura";
                    InformeReportDataSource.Value = dtPrecaptura;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("Fecha_inicio_recaudo", frmFechaRecaudo.Fecha_Recaudo_Inicio));
                    Parametros.Add(new ReportParameter("Fecha_fin_recaudo", frmFechaRecaudo.Fecha_Recaudo_Fin));
                    Parametros.Add(new ReportParameter("Total_registros", dtPrecaptura.Rows.Count.ToString()));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte; //"INFORME CONCILIACION PRECAPTURA"; 
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.Asistidos.Forms.Rpts.Conciliacion.Reporte_ConciliacionPrecaptura.rdlc";
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

        private void Informe_Mensual_Cliente()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            DataTable dsInforme = new DataTable();

            string strFechaInicio;
            string strFechaFin;

            frmFechaRecaudo = new frmFechaRecaudo(false);
            if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                this.reportViewerImpuestos.Visible = true;
                strFechaInicio = frmFechaRecaudo.Fecha_Recaudo_Inicio;
                strFechaFin = frmFechaRecaudo.Fecha_Recaudo_Fin;

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_MENSUAL_CLIENTE").FirstOrDefault().Formato_Reporte, frmFechaRecaudo.Fecha_Recaudo_Inicio);

                    this.ltReportDataSources.Clear();

                    ////Reporte Consolidado
                    dsInforme.Clear();
                    dsInforme = dbmIntegration.SchemaBcoPopular.PA_Reporte_Mensual_Impuestos_Asistidos.DBExecute(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, frmFechaRecaudo.Fecha_Recaudo_Inicio, frmFechaRecaudo.Fecha_Recaudo_Fin);

                    ReportDataSource InformeReportDataSource = new ReportDataSource();
                    InformeReportDataSource.Name = "dsReporteMensual";
                    InformeReportDataSource.Value = dsInforme;
                    ltReportDataSources.Add(InformeReportDataSource);

                    List<ReportParameter> Parametros = new List<ReportParameter>();
                    Parametros.Add(new ReportParameter("MesInforme", Convert.ToDateTime(frmFechaRecaudo.Fecha_Recaudo_Inicio).ToString("MMMM").ToUpper()));
                    Parametros.Add(new ReportParameter("AnoInforme", Convert.ToDateTime(frmFechaRecaudo.Fecha_Recaudo_Inicio).Year.ToString()));

                    this.reportViewerImpuestos.Reset();
                    this.reportViewerImpuestos.LocalReport.DisplayName = _nombreReporte; //  "INFORME MENSUAL CLIENTE"; 
                    this.reportViewerImpuestos.LocalReport.ReportEmbeddedResource = "BcoPopular.Plugin.Imaging.Asistidos.Forms.Rpts.InformeMensual.Informe_Mensual_Cliente.rdlc";
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
