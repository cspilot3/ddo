using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Imaging.Hacienda;
using System.Diagnostics;
using Miharu.Desktop.Controls.DesktopMessageBox;
using System.IO;


namespace BcoPopular.Plugin.Imaging.Hacienda.Forms
{
    public partial class frmReportesImpuestos : Form
    {
        frmFechaRecaudo frmFechaRecaudo = null;
        frmFechaRecaudoCiudad frmFechaRecaudoCiudad = null;
        string strFechaRecaudo = null;
        string strFechaTraslado = null;
        List<ReportDataSource> ltReportDataSources = new List<ReportDataSource>();
        private HaciendaPlugin _Plugin;
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

        public frmReportesImpuestos(HaciendaPlugin _plugin)
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


        /*Eventos Otros Reportes*/

        private void faltantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Faltantes_Sobrantes("FALTANTES");
        }

        private void sobrantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_Faltantes_Sobrantes("SOBRANTES");
        }

        private void ArchivoSIIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo_SIIF();
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

        private void Archivo_SIIF()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                frmFechaRecaudo = new frmFechaRecaudo(false,"PROCESO");

                DataTable dtConsolidado = new DataTable();

                string strFechaInicio;
                string strFechaFin;

                if ((frmFechaRecaudo.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    strFechaInicio = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");
                    strFechaFin = frmFechaRecaudo.Fecha_Recaudo.ToString("yyyyMMdd");

                    dtConsolidado.Clear();  
                    //dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Archivo_SIIF.DBExecute(strFechaInicio, strFechaFin, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad);

                    _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("Archivo_SIIF").FirstOrDefault().Formato_Reporte, DateTime.Now);
                    _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DispersionPagosAutoSistematizacion").FirstOrDefault().Extension_Salida;

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
                    StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8);
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
