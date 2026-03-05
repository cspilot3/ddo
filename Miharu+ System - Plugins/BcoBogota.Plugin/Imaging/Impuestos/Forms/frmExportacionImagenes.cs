using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Miharu.Desktop.Controls.DesktopMessageBox;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Controls;
using Miharu.FileProvider.Library;
using System.Windows.Forms;
using System.IO;
using Slyg.Tools;
using Miharu.Imaging;
using Slyg.Tools.Imaging;
using System.Dynamic;
using System.Threading;
using DBImaging.SchemaCore;
using DBImaging.SchemaSecurity;
using Imaging.Impuestos;
using System.ComponentModel;
using DBIntegration;
using System.Linq;
using System.Globalization;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
{
    public partial class frmExportacionImagenes : Form
    {
        #region "Declaraciones"
        string _rutaStartProcess = null;
        string _rutaGenerar = null;
        string _StrArchivoLog = null;
        private ImpuestosPlugin _Plugin;
        int cantidadTotalImg = 0;
        int contadorGlobalImagenes = 0;
        List<string> _ltErroresReporte = new List<string>();
        List<Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap> nInputImages = new List<Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap>();
        string _tipoReporte_IMG = "";
        #endregion

        #region Constructores
        public frmExportacionImagenes(ImpuestosPlugin _plugin, string TipoReporte_IMG = "")
        {
            InitializeComponent();
            this._tipoReporte_IMG = TipoReporte_IMG;
            this._Plugin = _plugin;
        }
         #endregion

        #region Propiedades
        public string SelectedPath
        {
            get { return RutaTextBox.Text.TrimEnd('\\') + "\\"; }
            set { RutaTextBox.Text = value; }
        }
        #endregion

        #region Metodos
        private bool Validar()
        {
            bool result = true;

            if (string.IsNullOrEmpty(RutaTextBox.Text))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Un directorio", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon);
                result = false;
            }

            if (!ValidarRuta())
            {
                result = false;
            }

            if (cbFormatosImagenes.SelectedIndex == 0)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Un Formato de Salida de Imagen", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon);
                result = false;
            }
                

            return result;

        }


        private bool ValidarRuta()
        {
            if ((string.IsNullOrEmpty(RutaTextBox.Text)))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                RutaTextBox.Focus();

            }
            else if ((!Directory.Exists(this.SelectedPath)))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                RutaTextBox.Focus();
                RutaTextBox.SelectAll();
            }
            else
            {
                return true;
            }

            return false;
        }


        private void SelectFolderPath()
        {
            dynamic LectorFolderBrowserDialog = new FolderBrowserDialog();
            DialogResult Respuesta = default(DialogResult);

            LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text;
            LectorFolderBrowserDialog.ShowNewFolderButton = false;
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta";

            Respuesta = LectorFolderBrowserDialog.ShowDialog();

            if ((Respuesta == DialogResult.OK))
            {
                RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath;
            }
        }

        private string ValidaFechas(DateTime dtInicial, DateTime dtFinal)
        {
            string result = "";

            try
            {
                if (dtFinal.Date < dtInicial.Date)
                {
                    result = "Error, la Fecha Final no puede ser menor que la Fecha Inicial";
                    _ltErroresReporte.Add(result);
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return result;

        }


        private void ExportacionImagenes(string rutaFinal)
        {
            if(ValidaFechas(this.dtpFechaProcesoInicial.Value, this.dtFechaProcesoFinal.Value) == "")
            {

                DBImaging.DBImagingDataBaseManager dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._Plugin.BcoBogotaConnectionString);

                try
                {
                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                    dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);

                    FileProviderManager manager = null;
                    string PathImg = null;
                    string FormatoSalida = null;
                    DataTable RegistrosExportar = null;
                    DataTable Inconsistencias = null;

                    //if (this.cbFormatosImagenes.SelectedText.ToString() == "TIFF")
                    FormatoSalida = ".tiff";
                    //else
                    //    FormatoSalida = ".jpeg";

                    ImageManager.EnumCompression Compresion = default(ImageManager.EnumCompression);
                    if (this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida == (Int16)DesktopConfig.FormatoImagenEnum.TIFF_Bitonal)
                    {
                        Compresion = ImageManager.EnumCompression.Ccitt4;
                    }
                    else
                    {
                        Compresion = ImageManager.EnumCompression.Lzw;
                    }

                    DateTime dtInicial = dtpFechaProcesoInicial.Value;
                    DateTime dtFinal = dtFechaProcesoFinal.Value;

                    TimeSpan span = dtFinal - dtInicial;
                    var TotalDays = span.TotalDays;

                    this.ProgressBarExportacíonGeneral.Value = 0;
                    this.ProgressBarExportacíonGeneral.Minimum = 0;
                    this.ProgressBarExportacíonGeneral.Maximum = Convert.ToInt32(TotalDays) + 1;
                    int contadorDate = 0;

                    bool RespuestaPredeterminada = false;
                    string RespuestaPredeterminadaStr = "";

                    Configuracion.frmConfirmaEliminacion frmElimina = null;

                    if (this._tipoReporte_IMG == "INCONSISTENCIAS")
                    {
                        Inconsistencias = new DataTable();
                    }

                    while (dtInicial.Date <= dtFinal.Date)
                    {
                        contadorDate++;
                        this.ProgressBarExportacíonGeneral.Value = contadorDate;

                        if (this._tipoReporte_IMG == "IMAGENES_FECHA_RECAUDO")
                        {
                            RegistrosExportar = dbmIntegration.SchemaBcoBogota.PA_Obtiene_Datos_Exportar.DBExecute(dtInicial.ToString("yyyy/MM/dd"), dtInicial.ToString("yyyy/MM/dd"));
                        }
                        else if (this._tipoReporte_IMG == "INCONSISTENCIAS")
                        {
                            RegistrosExportar = dbmIntegration.SchemaBcoBogota.PA_Imagenes_Inconsistencias.DBExecute(dtInicial.ToString("yyyy/MM/dd"), dtInicial.ToString("yyyy/MM/dd"));
                        }

                        if (RegistrosExportar.Rows.Count > 0)
                        {
                            this.lblProgresoGeneral.Text = "Generando Imagenes de Fecha : " + dtInicial.ToString("yyyy/MM/dd") + " ...";
                            this.cantidadTotalImg = RegistrosExportar.Rows.Count;
                            this.ProgressBarExportacíon.Value = 0;
                            this.ProgressBarExportacíon.Minimum = 0;
                            this.ProgressBarExportacíon.Maximum = RegistrosExportar.Rows.Count;
                            this.contadorGlobalImagenes = 0;

                            this._rutaStartProcess = this._rutaGenerar + "\\" + dtInicial.Day.ToString("00") + DateTimeFormatInfo.CurrentInfo.GetMonthName(dtInicial.Month).ToString().Substring(0, 3) + dtInicial.Year.ToString().Remove(0, 2);

                            if (Directory.Exists(this._rutaStartProcess))
                            {
                                string[] strA = Directory.GetFiles(_rutaStartProcess, "*", SearchOption.AllDirectories);

                                if ((strA.Length >= 1))
                                {
                                    if (RespuestaPredeterminada == false)
                                    {
                                        frmElimina = new Configuracion.frmConfirmaEliminacion(_rutaStartProcess);
                                        frmElimina.ShowDialog(this);
                                        RespuestaPredeterminadaStr = frmElimina.RespuestaPredetermiandaStr;
                                        RespuestaPredeterminada = frmElimina.RespuestaPredeterminada;
                                    }

                                    if (RespuestaPredeterminadaStr == "ACEPTAR")
                                    {
                                        Utilities.CrearDirectorio(_rutaStartProcess, true);
                                    }
                                    else
                                    {
                                        Utilities.CrearDirectorio(_rutaStartProcess, false);
                                    }
                                }
                            }
                            else
                            {
                                Utilities.CrearDirectorio(_rutaStartProcess, false);
                            }


                            var Ots = RegistrosExportar.AsEnumerable().Select(x => (int)x["fk_ot"]).Distinct().ToList();

                            foreach (var fk_Ot in Ots)
                            {
                                DBImaging.SchemaCore.CTA_ServidorSimpleType servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(fk_Ot)[0].ToCTA_ServidorSimpleType();
                                dynamic centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)[0].ToCTA_Centro_ProcesamientoSimpleType();
                                manager = new FileProviderManager(servidor, centro, ref dbmImaging, _Plugin.Manager.Sesion.Usuario.id);
                                manager.Connect();

                                DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(fk_Ot, null);
                                DataView FilesDataViewExpedientes = new DataView(FileDataTable);
                                DataTable dtResultFiltrada = RegistrosExportar.AsEnumerable().Where(x => (int)x["fk_ot"] == fk_Ot).CopyToDataTable();

                                if (dtResultFiltrada.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtResultFiltrada.Rows.Count; i++)
                                    {
                                        PathImg = _rutaStartProcess + "\\" + dtResultFiltrada.Rows[i]["Oficina"].ToString();

                                        if ((!Directory.Exists(PathImg)))
                                        {
                                            Utilities.CrearDirectorio(PathImg, true);
                                        }

                                        FilesDataViewExpedientes.RowFilter = "fk_Servidor = " + servidor.id_Servidor.ToString() + " AND fk_Expediente = " + dtResultFiltrada.Rows[i]["fk_Expediente"].ToString();

                                        foreach (DataRowView itemView in FilesDataViewExpedientes)
                                        {
                                            var expFiltrado = itemView.Row["fk_expediente"];
                                            //var fileFiltrado = Convert.ToInt32(itemView.Row["fk_file"]);
                                            var Formulario = dtResultFiltrada.Rows[i]["Formulario"];
                                            var indice = Formulario.ToString().IndexOf("760");
                                            var Formulario_Unico = dtResultFiltrada.Rows[i]["Formulario"].ToString().Substring(indice, dtResultFiltrada.Rows[i]["Formulario"].ToString().Length - indice);

                                            DBImaging.SchemaProcess.CTA_Exportacion_FilesRow RowFile = (DBImaging.SchemaProcess.CTA_Exportacion_FilesRow)itemView.Row;

                                            EscribeImage(PathImg, Formulario_Unico.ToString(), (int)RowFile.Folios_Documento_File, manager, RowFile, FormatoSalida, Compresion, dbmIntegration);
                                        }
                                    }
                                }
                                manager.Disconnect();
                            }
                        }
                        else
                        {
                            this._ltErroresReporte.Add("Error, no hay Imagenes para exportar para la Fecha " + dtInicial.ToString("yyyy/MM/dd"));
                            this.lblProgresoGeneral.Text = "No Hay Imagenes de Fecha : " + dtInicial.ToString("yyyy/MM/dd") + " ...";
                            //this.backgroundWorkerExportacionImagenes.CancelAsync();
                            //this.ProgressBarExportacíon.Value = 100;
                            //manager.Disconnect();
                            //return;
                        }

                        dtInicial = dtInicial.AddDays(1);
                    }
                    
                    //Genera Archivo Plano de Inconsistencias si las Hay
                    if (this._tipoReporte_IMG == "INCONSISTENCIAS")
                    {
                        Inconsistencias = dbmIntegration.SchemaBcoBogota.PA_Reporte_Incosistencias.DBExecute();
                        var Formato_Salida = dbmIntegration.SchemaBcoBogota.TBL_Config_Reporte.DBFindByNombre_Reporte("INCONSISTENCIAS").FirstOrDefault();

                        if (Inconsistencias.Rows.Count > 0)
                        {
                            Genera_ReporteArchivoPlano(rutaFinal, Formato_Salida.Extension_Salida,string.Format(Formato_Salida.Formato_Reporte,DateTime.Now), Inconsistencias, true, true, true, true);
                        }
                    }

                }
                catch (Exception ex)
                {                
                    Utilities.EscribeLog(this._StrArchivoLog, " - Error en Metodo ExportacionImagenes(), "+ex.Message, false, true);
                }
                
            }
        }

        private void EscribeImage(string pathFinaImg, string Formulario, int folios, FileProviderManager domainManager, DBImaging.SchemaProcess.CTA_Exportacion_FilesRow RowFile, string FormatoSalida, ImageManager.EnumCompression nCompresion, DBIntegration.DBIntegrationDataBaseManager dbmIntegration)
        {
            byte[] Imagen = null;
            byte[] Miniatura = null;
            string FileName = "";

            for (Int16 i = 0; i < folios; i++)
            {

                domainManager.GetFolio(RowFile.fk_Expediente, 1, 1, RowFile.id_Version, (Int16)(i+1), ref Imagen, ref Miniatura);

                if (folios > 1)
                {
	                Stream stream = new MemoryStream(Imagen);
	                nInputImages.Add(Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap.FromStream(stream));
                } else {
                    FileName = pathFinaImg + "\\" + Formulario + FormatoSalida;

                    using (FileStream fs = new FileStream(FileName, FileMode.Create))
                    {
		                fs.Write(Imagen, 0, Imagen.Length);
		                fs.Close();
	                }
                }

                if (((i+1) == folios))
                {
                    FileName = pathFinaImg + "\\" + Formulario + FormatoSalida;
                    //-------------------------------------------------------------------------
                    ImageManager.Save(nInputImages, FileName, FormatoSalida, Slyg.Tools.Imaging.ImageManager.EnumFormat.Tiff, nCompresion, false, Formulario);
                    nInputImages.Clear();
                    //-------------------------------------------------------------------------
                }

                Utilities.EscribeLog(this._StrArchivoLog, " - Generando Imagen " + Formulario.ToString() + FormatoSalida, false, true); 
            }
            
            this.contadorGlobalImagenes += 1;

            ActualizaImagen_Str(Formulario, RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Formulario.ToString() + FormatoSalida, dbmIntegration);

            if (this.contadorGlobalImagenes == 5)
                Utilities.ClearMemory();
            this.backgroundWorkerExportacionImagenes.ReportProgress(0);
        }

        private void ActualizaImagen_Str(string Formulario, long fk_Expediente, int fk_folder, int fk_file, string imagenStr, DBIntegration.DBIntegrationDataBaseManager dbmIntegration)
        {
            try
            {
                 dbmIntegration.SchemaBcoBogota.PA_Actualiza_ImagenStr_Inconsistencias.DBExecute(Formulario, fk_Expediente, fk_folder, fk_file, imagenStr);
            }
            catch (Exception ex)
            {
                Utilities.EscribeLog(this._StrArchivoLog, "Error al actualizar Imagen " + imagenStr +", "+ex.Message, false, true); 
            }
        }


        private bool ValidaFaltantes()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._Plugin.BcoBogotaConnectionString);
            bool result = false;

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                var FechasFaltantes = dbmIntegration.SchemaBcoBogota.TBL_Registro_Faltantes.DBGet(null);

                List<string> res = new List<string>();
                string ltMesjError = "";

                for (var date = this.dtpFechaProcesoInicial.Value; date <= this.dtFechaProcesoFinal.Value; date = date.AddDays(1))
                {
                    var FechaValidar = date.ToString("yyyy/MM/dd");
                    var encontrado = FechasFaltantes.Where(x => x.Fecha_Recaudo == FechaValidar).ToList();

                    if (encontrado.Count > 0)
                    {
                        ltMesjError = ltMesjError + "Esta fecha de recaudo " + FechaValidar + " tiene registros faltantes¡¡¡" + Environment.NewLine;
                    }
                }

                if (ltMesjError != "")
                {
                    if((DesktopMessageBoxControl.DesktopMessageShow(ltMesjError + Environment.NewLine +"¿Desea Continuar con la generación de Imagenes?", "Registros Faltantes", DesktopMessageBoxControl.IconEnum.WarningIcon, false) == System.Windows.Forms.DialogResult.OK))
                    {
                        result = true;
                    }
                }
                else
                    result = true;

            }
            catch (Exception ex)
            {

                DesktopMessageBoxControl.DesktopMessageShow("Errores de Reporte", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
            
            return result;
        }

        private bool Genera_ReporteArchivoPlano(string RutaDir, string Extension, string NombreReporte, DataTable DtFinal, bool EliminaFolders = true, bool generaConTAB = true, bool? EliminaArchivo = null, bool? InsertarDebajo = null)
        {
            bool retorno = true;
            try
            {
                Utilities.CrearDirectorio(RutaDir, false);
                dynamic auxDir_total = RutaDir + "\\" + NombreReporte + Extension;

                if ((EliminaArchivo == true))
                {
                    if ((File.Exists(auxDir_total)))
                    {
                        File.Delete(auxDir_total);
                    }
                }
                int contadorGeneral = DtFinal.Columns.Count;
                int contadorInterno = 0;


                if ((InsertarDebajo == false | InsertarDebajo == null))
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(auxDir_total, EliminaFolders, System.Text.ASCIIEncoding.Default))
                    {
                        foreach (DataRow itemRow in DtFinal.Rows)
                        {
                            foreach (DataColumn itemColumn in DtFinal.Columns)
                            {
                                contadorInterno += 1;
                                string strInsertar = itemRow[itemColumn.ColumnName.Replace("\\\\", "\\")].ToString();

                                if ((contadorInterno == contadorGeneral))
                                {
                                    contadorInterno = 0;

                                    file.Write(strInsertar);
                                }
                                else
                                {
                                    if ((generaConTAB))
                                    {
                                        file.Write(strInsertar + Microsoft.VisualBasic.ControlChars.Tab);
                                    }
                                    else
                                    {
                                        file.Write(strInsertar);
                                    }
                                }
                            }
                            file.Write(Microsoft.VisualBasic.ControlChars.CrLf);
                        }
                    }
                }
                else
                {
                    StreamWriter File_aux = System.IO.File.AppendText(auxDir_total);

                    foreach (DataRow itemRow in DtFinal.Rows)
                    {
                        foreach (DataColumn itemColumn in DtFinal.Columns)
                        {
                            contadorInterno += 1;
                            if ((contadorInterno == contadorGeneral))
                            {
                                contadorInterno = 0;

                                File_aux.Write(itemRow[itemColumn.ColumnName.Replace("\\\\", "\\")].ToString());
                            }
                            else
                            {
                                if ((generaConTAB))
                                {
                                    File_aux.Write(itemRow[itemColumn.ColumnName].ToString().Replace("\\\\", "\\") + Microsoft.VisualBasic.ControlChars.Tab);
                                }
                                else
                                {
                                    File_aux.Write(itemRow[itemColumn.ColumnName].ToString().Replace("\\\\", "\\"));
                                }
                            }
                        }
                        File_aux.Write(Microsoft.VisualBasic.ControlChars.CrLf);
                    }
                    File_aux.Close();
                }

            }
            catch (Exception ex)
            {
                this._ltErroresReporte.Add("Error, Genera_ReporteArchivoPlano " + ex.Message + " - " + DateTime.Now);
                return false;
            }
            return retorno;
        }

        #endregion

        #region Eventos
        private void frmExportacionImagenes_Load(object sender, EventArgs e)
        {
            this.cbFormatosImagenes.Items.Add("------Seleccione Formato------");
            this.cbFormatosImagenes.Items.Add("JPEG");
            this.cbFormatosImagenes.Items.Add("TIFF");

            this.cbFormatosImagenes.SelectedIndex = 2;
            this.cbFormatosImagenes.Enabled = false;

            this.dtFechaProcesoFinal.MaxDate = DateTime.Now;
            this.dtpFechaProcesoInicial.MaxDate = DateTime.Now;

            if (this._tipoReporte_IMG == "INCONSISTENCIAS")
            {
                this.Text = "Exportación de Imagenes - INCONSISTENCIAS";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GenerarButton_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                this._rutaStartProcess = "";

                if (ValidaFaltantes())
                {
                    try
                    {
                        this._rutaGenerar = this.RutaTextBox.Text;
                        this._StrArchivoLog = this._rutaGenerar + "\\LOG_BcoBogota_" + System.DateTime.Now.ToString("yyyyMMdd") + "_" + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + ".txt";

                        this._rutaStartProcess = this._rutaGenerar;
                    }
                    catch (Exception ex)
                    {
                        Utilities.CrearDirectorio(_rutaStartProcess, false);
                    }

                    Utilities.EscribeLog(_StrArchivoLog, "LOG DE REPORTE PARA FECHA " + DateTime.Now.ToString(), true, false);
                    CheckForIllegalCrossThreadCalls = false;

                    if (!this.backgroundWorkerExportacionImagenes.IsBusy)
                    {
                        this.ProgressBarExportacíon.Value = 0;
                        this.ProgressBarExportacíonGeneral.Value = 0;
                        this.lblProgresoIndividual.Visible = true;
                        this.ProgressBarExportacíon.Visible = true;
                        this.lblProgresoGeneral.Visible = true;
                        this.ProgressBarExportacíonGeneral.Visible = true;
                        this.dtFechaProcesoFinal.Enabled = false;
                        this.dtpFechaProcesoInicial.Enabled = false;
                        this.RutaTextBox.Enabled = false;
                        this.GenerarButton.Enabled = false;
                        this.SelectFolderButton.Enabled = false;
                        this.lblProgresoIndividual.Text = "Progreso General(0%)";
                        this.backgroundWorkerExportacionImagenes.RunWorkerAsync();
                    }
                }
            }  
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void RutaTextBox_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void backgroundWorkerExportacionImagenes_DoWork(object sender, DoWorkEventArgs e)
        {
            ExportacionImagenes(this._rutaStartProcess);
        }

        private void backgroundWorkerExportacionImagenes_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((this.contadorGlobalImagenes <= this.ProgressBarExportacíon.Maximum))
            {
                this.ProgressBarExportacíon.Value = this.contadorGlobalImagenes;
            }

            this.lblProgresoIndividual.Text = "Progreso General (" + ((this.contadorGlobalImagenes * 100) / this.cantidadTotalImg).ToString() + "%)";
        }

        private void backgroundWorkerExportacionImagenes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblProgresoIndividual.Text = "Progreso General (100%)";

            if ((this._ltErroresReporte.Count > 0)) {
	            string Errores = "";

	            foreach (var itemError_loopVariable in this._ltErroresReporte) {
		            var itemError = itemError_loopVariable;
		            Errores = Errores + Environment.NewLine + itemError;
	            }

		        Errores = "Reportes generados pero con error en: " + Environment.NewLine + Errores;

	            DesktopMessageBoxControl.DesktopMessageShow(Errores, "Errores de Reporte", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
	            this._ltErroresReporte.Clear();
            }
            
            if(this.contadorGlobalImagenes > 0) {
	            DesktopMessageBoxControl.DesktopMessageShow("Reportes Generados con exito" + Environment.NewLine, "Reportes Generados", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
	            System.Diagnostics.Process.Start(this._rutaGenerar);
            }

            this.lblProgresoIndividual.Visible = false;
            this.lblProgresoGeneral.Visible = false;
            this.ProgressBarExportacíonGeneral.Visible = false;
            this.ProgressBarExportacíon.Visible = false;
            this.dtFechaProcesoFinal.Enabled = true;
            this.dtpFechaProcesoInicial.Enabled = true;
            this.RutaTextBox.Enabled = true;
            this.GenerarButton.Enabled = true;
            this.SelectFolderButton.Enabled = true;
        }

        #endregion

        
        
    }
}
