using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Library;
using Miharu.Tools.Progress;
using Slyg.Tools.Imaging;
using Miharu.FileProvider.Library;
using Slyg.Tools;
using Miharu.Imaging.Library.Eventos;
using DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl;
using System.Xml.Linq;
using System.Linq;
using Slyg.Tools.CSV;
using Imaging.GobernacionSantander;
using System.Globalization;

namespace BcoPopular.Plugin.Imaging.GobernacionSantander.Forms
{
    public partial class FormExport : FormBase
    {

        #region " Declaraciones "
        private bool Usa_Exportacion_PDF;
        private Slyg.Tools.Imaging.ImageManager.EnumFormat formatoAux;
        private Slyg.Tools.Imaging.ImageManager.EnumCompression CompresionAux;
        private Slyg.Tools.Imaging.ImageManager.EnumFormat formato;
        private GobernacionSantanderPlugin _Plugin;

        Slyg.Tools.Imaging.ImageManager.EnumCompression compresion;
        private DataView ViewExpedientes = new DataView();
        private DataTable ExpedientesSeleccion = new DataTable();
        public static List<string> FileNamesCons = new List<string>();

        string FolderNameOutput;
        #endregion
        EventManager _EventManager;

        #region " Propiedades"
        public EventManager EventManager
        {
            get { return this._EventManager; }
            set { _EventManager = value; }
        }
        #endregion

        #region " Eventos "
        public void FormExport_Load(object sender, System.EventArgs e)
        {

            if (!(this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos))
            {
                CheckBoxExpedientesValidos.Visible = false;
            }

            Usa_Exportacion_PDF = this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF;
            formato = Utilities.GetEnumFormat(this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida);
            compresion = Utilities.GetEnumCompression((DesktopConfig.FormatoImagenEnum)this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida);

            Load_FormatoCargue();

        }


        private void BuscarFechaButton_Click(System.Object sender, EventArgs e)
        {
            if (validarFechaProceso())
            {
                if (!this.CheckBoxExpedientes.Checked)
                {
                    CargarOTs();
                }
                else
                {
                    CargarExpedientes();
                }
            }



        }

        private void BuscarCarpetaButton_Click(System.Object sender, EventArgs e)
        {
            FolderBrowserDialog Selector = new FolderBrowserDialog();

            Selector.SelectedPath = CarpetaSalidaTextBox.Text;
            if ((Selector.ShowDialog() == DialogResult.OK))
            {
                this.CarpetaSalidaTextBox.Text = Selector.SelectedPath;
            }
        }


        private void ExportarButton_Click(System.Object sender, EventArgs e)
        {
            if (validarFechaProceso())
            {
                if (!this.CheckBoxExpedientes.Checked)
                {
                    CargarOTs();
                }
                else
                {
                    if (CargarExpedientes())
                    {
                        ExportarExpedientes();
                    }
                }
            }

            //if (!CheckBoxExpedientes.Checked)
            //{
            //    ExportarOTs();
            //}
            //else
            //{
            //    ExportarExpedientes();
            //}

        }

        private void CancelarButton_Click(System.Object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckBoxExpedientes_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxExpedientes.Checked)
            {
                CheckBoxExpedientesValidos.Checked = false;
            }
            MostrarDatagrid();
        }

        private void CheckBoxExpedientesValidos_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxExpedientesValidos.Checked)
            {
                CheckBoxExpedientes.Checked = false;
            }
            MostrarDatagrid();
        }

        private void ExpedientesDataGridView_ColumnHeaderMouseDoubleClick(System.Object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                for (var i = 0; i <= ExpedientesDataGridView.RowCount - 1; i++)
                {
                    ExpedientesDataGridView.Rows[i].Cells["Exportar"].Value = true;
                }
            }
        }
        #endregion

        #region " Metodos "
        private void CargarOTs()
        {
            this.OTDataGridView.AutoGenerateColumns = false;
            this.OTDataGridView.DataSource = getOTs();
            this.OTDataGridView.Refresh();

            if ((this.OTDataGridView.RowCount == 0))
            {
                MessageBox.Show("No se encontraron OTs para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CargarExpedientes()
        {
            this.ExpedientesDataGridView.AutoGenerateColumns = false;
            this.ExpedientesDataGridView.DataSource = getExpedientes();
            this.ExpedientesDataGridView.Refresh();

            if ((this.ExpedientesDataGridView.RowCount == 0))
            {
                MessageBox.Show("No se encontraron Expedientes para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void ExportarOTs()
        {
            if ((Validar()))
            {
                DBImaging.DBImagingDataBaseManager dbmImaging = null;
                DBCore.DBCoreDataBaseManager dbmCore = null;
                DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable TotalesDataTable = default(DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable);
                FormProgress ProgressForm = new FormProgress();
                dynamic OTRow = (DBImaging.SchemaProcess.CTA_Exportacion_OTRow)((DataRowView)this.OTDataGridView.CurrentRow.DataBoundItem).Row;
                FileNamesCons = new List<string>();

                try
                {
                    dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);

                    dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    if ((CheckBoxExpedientesValidos.Visible))
                    {
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked);
                    }
                    else
                    {
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(OTRow.id_OT, null);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if ((dbmImaging != null))
                        dbmImaging.Connection_Close();
                }

                if ((TotalesDataTable.Rows.Count > 0))
                {
                    DialogResult Respuesta = default(DialogResult);

                    Respuesta = MessageBox.Show("Se encontró : " + Constants.vbCrLf + TotalesDataTable[0].Folders + " Unidades Documentales, " + Constants.vbCrLf + TotalesDataTable[0].Files + " Documentos, " + Constants.vbCrLf + TotalesDataTable[0].Folios + " Folios con " + Constants.vbCrLf + (TotalesDataTable[0].Tamaño / 1024 / 1024).ToString("#,##0.00") + "MB de tamaño, " + Constants.vbCrLf + "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            this.Enabled = false;

                            dbmCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

                            dbmImaging.Connection_Open(1);
                            dbmCore.Connection_Open(1);

                            DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable FolderDataTable = default(DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable);
                            DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable FileDataTable = default(DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable);
                            DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable FileDataDataTable = default(DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable);
                            DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable FileValidacionDataTable = default(DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable);

                            dynamic ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(OTRow.id_OT);
                            if ((CheckBoxExpedientesValidos.Visible))
                            {
                                FolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked);
                                FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked);
                                FileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked);
                                FileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones.DBExecute(OTRow.id_OT, CheckBoxExpedientesValidos.Checked);
                            }
                            else
                            {
                                FolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders.DBExecute(OTRow.id_OT, null);
                                FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(OTRow.id_OT, null);
                                FileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data.DBExecute(OTRow.id_OT, null);
                                FileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones.DBExecute(OTRow.id_OT, null);
                            }

                            string OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\";
                            DataView FilesDataView = new DataView(FileDataTable);
                            int Progreso = 0;

#if !Debug
                            ProgressForm.Show();
#endif

                            ProgressForm.SetProceso("Exportar");
                            ProgressForm.SetAccion("Obteniendo imágenes...");
                            ProgressForm.SetProgreso(0);
                            ProgressForm.SetMaxValue(TotalesDataTable[0].Folios);

                            Application.DoEvents();

                            // Crear el directorio de las imágenes
                            Directory.CreateDirectory(OutputFolder + "images");

                            ImageManager.EnumCompression Compresion = default(ImageManager.EnumCompression);

                            if ((this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida == (short)DesktopConfig.FormatoImagenEnum.TIFF_Bitonal))
                            {
                                Compresion = ImageManager.EnumCompression.Ccitt4;
                            }
                            else
                            {
                                Compresion = ImageManager.EnumCompression.Lzw;
                            }

                            foreach (var RowServidor in ServidoresDataTable)
                            {
                                FileProviderManager manager = null;

                                try
                                {
                                    dynamic servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(OTRow.id_OT)(0).ToCTA_ServidorSimpleType();
                                    dynamic centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)[0].ToCTA_Centro_ProcesamientoSimpleType();

                                    manager = new FileProviderManager(servidor, centro, ref dbmImaging, this._Plugin.Manager.Sesion.Usuario.id);
                                    manager.Connect();

                                    dynamic FileFolderName = "images\\" + OTRow.id_OT.ToString("0000000000") + "\\";
                                    FolderNameOutput = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\" + FileFolderName;

                                    if ((!Directory.Exists(OutputFolder + FileFolderName)))
                                    {
                                        Directory.CreateDirectory(OutputFolder + FileFolderName);
                                    }

                                    //var GruposD = null;
                                    //GruposD = (from a in FileDataTableaa.Field<int>("fk_Grupo")GroupGroup.Select(x => x("fk_Grupo")).First()).ToList();

                                    var GruposD = FileDataTable.GroupBy(n => new { n.fk_Grupo }).Select(g => g.Key.fk_Grupo).ToList();

                                    //var x =       FileDataTable.GroupBy(n => new { n.fk_Grupo}).Select(g => new {g.Key.fk_Grupo}).ToList();

                                    //(From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Grupo") Into Group Select Group.Select(Function(x) x("fk_Grupo")).First()).ToList()

                                    foreach (int grupo in GruposD)
                                    {
                                        DataTable FilesbyGroup = FileDataTable.Select("fk_Grupo = " + grupo.ToString()).CopyToDataTable();

                                        if (grupo == 0)
                                        {
                                            DataView FilesbyGroupDataView = new DataView(FilesbyGroup);

                                            // Obtener los Files a transferir   
                                            FilesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " + RowServidor.fk_Entidad + " AND fk_Servidor = " + RowServidor.id_Servidor;

                                            foreach (DataRowView ItemFile in FilesbyGroupDataView)
                                            {
                                                if (ProgressForm.Cancelar)
                                                    throw new Exception("La acción fue cancelada por el usuario");

                                                // Enviar el archivo
                                                ExportarImagen(manager, ItemFile, Compresion, OutputFolder + FileFolderName);

                                                Progreso += 1;
                                                ProgressForm.SetProgreso(Progreso);
                                                Application.DoEvents();
                                            }
                                        }
                                        else
                                        {
                                            //List<object> Expedientes = null;
                                            //Expedientes = (from a in FilesbyGroupaa.Field<long>("fk_Expediente")GroupGroup.Select(x => x("fk_Expediente")).First()).ToList();


                                            var Expedientes = FileDataTable.GroupBy(n => new { n.fk_Expediente }).Select(g => g.Key.fk_Expediente).ToList();


                                            foreach (long Expediente in Expedientes)
                                            {
                                                DataTable FilesExpedientesbyGroup = FilesbyGroup.Select("fk_Grupo = " + grupo.ToString() + "AND fk_Expediente = " + Expediente.ToString()).CopyToDataTable();
                                                DataView FilesExpedientesbyGroupDataView = new DataView(FilesExpedientesbyGroup);

                                                // Obtener los Files a transferir   
                                                FilesExpedientesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " + RowServidor.fk_Entidad + " AND fk_Servidor = " + RowServidor.id_Servidor;

                                                if (ProgressForm.Cancelar)
                                                    throw new Exception("La acción fue cancelada por el usuario");

                                                // Enviar el archivo
                                                ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder + FileFolderName);

                                                Progreso += 1;
                                                ProgressForm.SetProgreso(Progreso);
                                                Application.DoEvents();
                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if ((manager != null))
                                        manager.Disconnect();
                                    throw (ex);
                                }
                            }

                            //------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                            if ((Convert.ToBoolean(this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)))
                            {
                                ExportAllFillesInTiff(Compresion, FolderNameOutput);
                            }
                            //--------------------------------

                            if ((this.VisorRadioButton.Checked))
                            {
                                GenerarVisor(dbmCore, dbmImaging, OTRow.id_OT, OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable);
                            }
                            else if ((this.XMLRadioButton.Checked))
                            {
                                GenerarXML(dbmCore, dbmImaging, OTRow.id_OT, OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable);
                            }
                            else
                            {
                                GenerarTXT(OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable);
                            }

                            // Crear la exportación

                            dynamic ExportacionType = new DBImaging.SchemaProcess.TBL_ExportacionType();

                            if ((this.VisorRadioButton.Checked))
                                ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.VISOR;
                            if ((this.XMLRadioButton.Checked))
                                ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.XML;
                            if ((this.TXTRadioButton.Checked))
                                ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.TEXTO_PLANO;
                            ExportacionType.Fecha_Exportacion = SlygNullable.SysDate;
                            ExportacionType.fk_Usuario = this._Plugin.Manager.Sesion.Usuario.id;
                            ExportacionType.fk_Sede_Procesamiento = this._Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede;
                            ExportacionType.fk_Centro_Procesamiento = this._Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento;
                            ExportacionType.fk_Puesto_Trabajo = this._Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo;
                            ExportacionType.Total_Folders = TotalesDataTable[0].Folders;
                            ExportacionType.Total_Files = TotalesDataTable[0].Files;
                            ExportacionType.Ruta = OutputFolder;

                            dynamic OTType = new DBImaging.SchemaProcess.TBL_OTType();
                            OTType.Exportado = true;

                            dynamic OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBGet(OTRow.id_OT);
                            bool Exportado = false;
                            if ((!OTDataTable(0).Isfk_ExportacionNull()))
                            {
                                Exportado = dbmImaging.SchemaProcess.TBL_Exportacion.DBGet(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion).Count > 0;
                            }

                            try
                            {
                                dbmImaging.Transaction_Begin();

                                if ((Exportado))
                                {
                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBUpdate(ExportacionType, OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion);

                                    OTType.fk_Exportacion = OTDataTable(0).fk_Exportacion;
                                }
                                else
                                {
                                    ExportacionType.fk_Entidad_Procesamiento = OTDataTable(0).fk_Entidad_Procesamiento;
                                    ExportacionType.fk_Entidad = OTDataTable(0).fk_Entidad;
                                    ExportacionType.fk_Proyecto = OTDataTable(0).fk_Proyecto;
                                    ExportacionType.fk_Fecha_Proceso = OTDataTable(0).fk_fecha_proceso;
                                    ExportacionType.id_Exportacion = dbmImaging.SchemaProcess.TBL_Exportacion.DBNextId(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso);

                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBInsert(ExportacionType);

                                    OTType.fk_Exportacion = ExportacionType.id_Exportacion;
                                }

                                dbmImaging.SchemaProcess.TBL_OT.DBUpdate(OTType, OTRow.id_OT);

                                dbmImaging.Transaction_Commit();
                            }
                            catch
                            {
                                dbmImaging.Transaction_Rollback();
                                throw;
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            ProgressForm.Hide();
                            Application.DoEvents();

                            return;
                        }
                        finally
                        {
                            this.Enabled = true;

                            if ((dbmImaging != null))
                                dbmImaging.Connection_Close();
                            if ((dbmCore != null))
                                dbmCore.Connection_Close();

                            BorrarTemporal();

                            ProgressForm.Close();
                        }
                        MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarOTs();
                    }
                    else
                    {
                        MessageBox.Show("Operación cancelada por Usuario", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            BorrarTemporal();
        }

        private void ExportAllFillesInTiff(ImageManager.EnumCompression nCompresion, string nFileName)
        {

            try
            {

                if (FileNamesCons.Count > 0)
                {
                    string FileName = nFileName + DateTime.Now.ToString("yyyyMMdd") + ".tiff";

                    try
                    {
                        if (File.Exists(FileName))
                        {
                            File.Delete(FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error eliminando la imagen en la ruta: " + FileName + " - Error: " + ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ImageManager.Save(FileNamesCons, FileName, "", ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.Lzw, false, Program.AppPath + Program.TempPath, true);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un error al exportar la imagen TIFF: " + ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ExportarExpedientes()
        {
            if ((Validar()))
            {
                DBImaging.DBImagingDataBaseManager dbmImaging = null;
                DBCore.DBCoreDataBaseManager dbmCore = null;
                DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable TotalesDataTable = default(DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable);
                FormProgress ProgressForm = new FormProgress();
                FileNamesCons = new List<string>();


                if (LlenaExpedientesSeleccion())
                {
                    int Folders = 0;
                    int Files = 0;
                    int Folios = 0;
                    double Tamaño = 0;
                    //Por cada OT se crea una carpeta

                    dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);

                    dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    foreach (DataRow row in ViewExpedientes.ToTable().Rows)
                    {
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Expediente_Totales.DBExecute(Convert.ToInt32(row[0]));
                        if (TotalesDataTable.Rows.Count > 0)
                        {
                            Folders += Convert.ToInt32(TotalesDataTable.Rows[0]["Folders"]);
                            Files += Convert.ToInt32(TotalesDataTable.Rows[0]["Files"]);
                            Folios += Convert.ToInt32(TotalesDataTable.Rows[0]["Folios"]);
                            Tamaño += Convert.ToDouble(TotalesDataTable.Rows[0]["Tamaño"]);
                        }
                    }

                    dbmImaging.Connection_Close();

                    DialogResult Respuesta = default(DialogResult);

                    Respuesta = MessageBox.Show("Se encontró : " + Constants.vbCrLf + Folders + " Unidades Documentales, " + Constants.vbCrLf + Files + " Documentos, " + Constants.vbCrLf + Folios + " Folios con " + Constants.vbCrLf + (Tamaño / 1024 / 1024).ToString("#,##0.00") + "MB de tamaño, " + Constants.vbCrLf + "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Respuesta == DialogResult.Yes)
                    {
                        try
                        {
                            this.Enabled = false;

                            //Se crea Tabla de OTs
                            DataTable OTs = new DataTable();
                            OTs = ViewExpedientes.ToTable(true, "fk_OT");


                            Miharu.Imaging.OffLineViewer.Library.xsdOfflineData ExportacionDataSetXML = new Miharu.Imaging.OffLineViewer.Library.xsdOfflineData();
                            dynamic FolderDataTableTXT = new DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable();
                            dynamic FileDataTableTXT = new DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable();
                            dynamic FileDataDataTableTXT = new DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable();
                            dynamic FileValidacionDataTableTXT = new DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable();

                            if (OTs.Rows.Count > 0)
                            {
                                int Progreso = 0;
                                ProgressForm.SetProceso("Exportar");
                                ProgressForm.SetAccion("Obteniendo imágenes...");
                                ProgressForm.SetProgreso(0);
                                ProgressForm.SetMaxValue(Files);
                                Application.DoEvents();

                                dbmCore = new DBCore.DBCoreDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Core);
                                dbmCore.Connection_Open(1);
                                dbmImaging.Connection_Open(1);

#if !Debug
                                //ProgressForm.Show();
#endif


                                foreach (DataRow rowOt in OTs.Rows)
                                {
                                    DataTable ExpedientesOT = new DataTable();
                                    dynamic ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(rowOt[0]));
                                    dynamic FolderDataTable = new DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable();
                                    var FileDataTable = new DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable();
                                    dynamic FileDataDataTable = new DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable();
                                    dynamic FileValidacionDataTable = new DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable();

                                    //Filtar expedientes por OT
                                    ViewExpedientes.RowFilter = "fk_OT = " + rowOt[0].ToString();
                                    ExpedientesOT = ViewExpedientes.ToTable();

                                    foreach (DataRow rowExpediente in ViewExpedientes.ToTable().Rows)
                                    {
                                        dynamic tmpFolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders_Expediente.DBExecute(Convert.ToInt32(rowExpediente[0]));
                                        dynamic tmpFileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files_Expediente.DBExecute(Convert.ToInt32(rowExpediente[0]));
                                        dynamic tmpFileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data_Expediente.DBExecute(Convert.ToInt32(rowExpediente[0]));
                                        dynamic tmpFileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones_Expediente.DBExecute(Convert.ToInt32(rowExpediente[0]));

                                        TablaDesdeTemporal(FolderDataTable, tmpFolderDataTable);
                                        TablaDesdeTemporal(FileDataTable, tmpFileDataTable);
                                        TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTable);
                                        TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTable);

                                        if (this.TXTRadioButton.Checked)
                                        {
                                            TablaDesdeTemporal(FolderDataTableTXT, tmpFolderDataTable);
                                            TablaDesdeTemporal(FileDataTableTXT, tmpFileDataTable);
                                            TablaDesdeTemporal(FileDataDataTableTXT, tmpFileDataDataTable);
                                            TablaDesdeTemporal(FileValidacionDataTableTXT, tmpFileValidacionDataTable);
                                        }

                                    }

                                    string OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\";
                                    DataView FilesDataViewExpedientes = new DataView(FileDataTable);


                                    DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                                    string nombreMes = formatoFecha.GetMonthName(this.FechaProcesoDateTimePicker.Value.Month);

                                    var desde = this.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd");
                                    var hasta = this.FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd");

                                    // Crear el directorio de las imágenes
                                    if (!Directory.Exists(OutputFolder + "images_" + desde + "-" + hasta))
                                    {
                                        Directory.CreateDirectory(OutputFolder + "images_" + desde + "-" + hasta);
                                    }

                                    ImageManager.EnumCompression Compresion = default(ImageManager.EnumCompression);

                                    if ((this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida == (short)DesktopConfig.FormatoImagenEnum.TIFF_Bitonal))
                                    {
                                        Compresion = ImageManager.EnumCompression.Ccitt4;
                                    }
                                    else
                                    {
                                        Compresion = ImageManager.EnumCompression.Lzw;
                                    }

                                    foreach (var RowServidor in ServidoresDataTable)
                                    {

                                        FileProviderManager manager = null;
                                    }

                                    foreach (var RowServidor in ServidoresDataTable)
                                    {
                                        FileProviderManager manager = null;

                                        try
                                        {
                                            dynamic servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(rowOt[0]))[0].ToCTA_ServidorSimpleType();
                                            dynamic centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)[0].ToCTA_Centro_ProcesamientoSimpleType();

                                            manager = new FileProviderManager(servidor, centro, ref dbmImaging, this._Plugin.Manager.Sesion.Usuario.id);
                                            manager.Connect();

                                            //dynamic FileFolderName = "images\\" + Convert.ToInt32(rowOt[0]).ToString("0000000000") + "\\";
                                            //FolderNameOutput = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\" + FileFolderName;

                                            //if ((!Directory.Exists(OutputFolder + FileFolderName)))
                                            //{
                                            //    Directory.CreateDirectory(OutputFolder + FileFolderName);
                                            //}

                                            //List<object> GruposD = null;
                                            //GruposD = (from a in FileDataTable group p.Field<int>("fk_Grupo")GroupGroup.Select(x => x("fk_Grupo")).First()).ToList();

                                            var GruposD = FileDataTable.GroupBy(n => new { n.fk_Grupo }).Select(g => g.Key.fk_Grupo).ToList(); //(from x in FileDataTable group x by x.fk_Grupo into dateGroup select new { dateGroup.fk_Grupo }).ToList(); (from x in FileDataTable group x by x.fk_Grupo into dateGroup select new { dateGroup.Key.fk_Grupo }).ToList(); //FileDataTable.GroupBy(n => new { n.fk_Grupo }).Select(g => g.Key.fk_Grupo).ToList(); //(from x in FileDataTable group x by x.fk_Grupo into dateGroup select new { dateGroup.fk_Grupo }).ToList(); 

                                            //FileDataTable.GroupBy(n => new { n.fk_Grupo }).Select(g => new { g.Key.fk_Grupo }).ToList();

                                            //(From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Grupo") Into Group Select Group.Select(Function(x) x("fk_Grupo")).First()).ToList()
                                            //from p in persons
                                            //group p.car by p.PersonId into g
                                            //select new { PersonId = g.Key, Cars = g.ToList() };

                                            var FileFolderName = "";

                                            foreach (int grupo in GruposD)
                                            {
                                                dynamic FilesbyGroup = FileDataTable.Select("fk_Grupo = " + grupo.ToString()).CopyToDataTable();

                                                if (grupo == 0)
                                                {
                                                    DataView FilesbyGroupDataView = new DataView(FilesbyGroup);

                                                    // Obtener los Files a transferir   
                                                    FilesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " + RowServidor.fk_Entidad + " AND fk_Servidor = " + RowServidor.id_Servidor;


                                                    foreach (DataRowView ItemFile in FilesbyGroupDataView)
                                                    {
                                                        var expediente = Convert.ToInt64(ItemFile["fk_Expediente"]);
                                                        var folder = Convert.ToInt16(ItemFile["fk_Folder"]);
                                                        var file = Convert.ToInt16(ItemFile["fk_File"]);
                                                        var documento = Convert.ToInt32(ItemFile["fk_Documento"]);
                                                        var group = Convert.ToInt32(ItemFile["fk_Grupo"]);

                                                        DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
                                                        dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                                                        var nombre = dbmIntegration.SchemaBcoPopular.PA_GetNombreArchivo.DBExecute(expediente, folder, file, documento, group);
                                                        dbmIntegration.Connection_Close();

                                                        if (nombre == string.Empty)
                                                        {
                                                            nombre = "DEFAULT";
                                                        }
                                                        else
                                                        {
                                                            nombre = nombre.Substring(1, 5);
                                                        }

                                                        FileFolderName = "images_" + desde + "-" + hasta + "\\" + nombre + "\\";//"images\\" + Convert.ToInt32(rowOt[0]).ToString("0000000000") + "\\";                                                        
                                                        FolderNameOutput = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\" + FileFolderName;
                                                        
                                                        if ((!Directory.Exists(FolderNameOutput)))
                                                        {
                                                            Directory.CreateDirectory(FolderNameOutput);
                                                        }

                                                        if (nombre != "DEFAULT")
                                                        {
                                                            CrearArchivoMunicipio(Convert.ToInt32(nombre), desde, hasta, FolderNameOutput);
                                                        }

                                                        if (ProgressForm.Cancelar)
                                                            throw new Exception("La acción fue cancelada por el usuario");

                                                        // Enviar el archivo
                                                        ExportarImagen(manager, ItemFile, Compresion, FolderNameOutput);

                                                        Progreso += 1;
                                                        ProgressForm.SetProgreso(Progreso);
                                                        Application.DoEvents();
                                                    }
                                                }
                                                else
                                                {
                                                    //List<object> Expedientes = null;
                                                    //Expedientes = (from a in FilesbyGroupaa.Field<long>("fk_Expediente")GroupGroup.Select(x => x("fk_Expediente")).First()).ToList();

                                                    var Expedientes = FileDataTable.GroupBy(n => new { n.fk_Expediente }).Select(g => g.Key.fk_Expediente).ToList();


                                                    foreach (long Expediente in Expedientes)
                                                    {
                                                        dynamic FilesExpedientesbyGroup = FilesbyGroup.Select("fk_Grupo = " + grupo.ToString() + "AND fk_Expediente = " + Expediente.ToString()).CopyToDataTable;
                                                        DataView FilesExpedientesbyGroupDataView = new DataView(FilesExpedientesbyGroup);


                                                        // Obtener los Files a transferir   
                                                        FilesExpedientesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " + RowServidor.fk_Entidad + " AND fk_Servidor = " + RowServidor.id_Servidor;

                                                        if (ProgressForm.Cancelar)
                                                            throw new Exception("La acción fue cancelada por el usuario");

                                                        FileFolderName = "5001";
                                                        // Enviar el archivo
                                                        ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder + FileFolderName);

                                                        Progreso += 1;
                                                        ProgressForm.SetProgreso(Progreso);
                                                        Application.DoEvents();
                                                    }

                                                }
                                            }
                                            manager.Disconnect();
                                        }
                                        catch (Exception ex)
                                        {
                                            if ((manager != null))
                                                manager.Disconnect();
                                            throw;
                                        }
                                    }

                                    //------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                    if ((Convert.ToBoolean(this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)))
                                    {
                                        ExportAllFillesInTiff(Compresion, FolderNameOutput);
                                    }
                                    //--------------------------------

                                    //if ((this.VisorRadioButton.Checked))
                                    //{
                                    //    GenerarVisor(dbmCore, dbmImaging, Convert.ToInt32(rowOt[0]), OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable);
                                    //}
                                    //else if ((this.XMLRadioButton.Checked))
                                    //{
                                    //    bool generar = false;
                                    //    if (rowOt.Equals(OTs.Rows[OTs.Rows.Count - 1]))
                                    //    {
                                    //        generar = true;
                                    //    }
                                    //    GenerarXMLExpedientes(generar, dbmCore, dbmImaging, Convert.ToInt32(rowOt[0]), OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, ExportacionDataSetXML);

                                    //}
                                    //else if (rowOt.Equals(OTs.Rows[OTs.Rows.Count - 1]))
                                    //{
                                    //    GenerarTXT(OutputFolder, FolderDataTableTXT, FileDataTableTXT, FileDataDataTableTXT, FileValidacionDataTableTXT);

                                    //}

                                    //Aqui debe ir codigo que genera la ruta

                                }
                                MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            this.Enabled = true;
                            //Finalizar conexiones
                            if ((dbmImaging != null))
                                dbmImaging.Connection_Close();
                            if ((dbmCore != null))
                                dbmCore.Connection_Close();


                            BorrarTemporal();

                            //Ocultar barras de progreso
                            ProgressForm.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No se seleccionaron registros para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            BorrarTemporal();

        }

        private void TablaDesdeTemporal(DataTable tabla, DataTable temporal)
        {

            foreach (DataRow drow in temporal.Rows)
            {
                DataRow newRow = tabla.NewRow();

                foreach (DataColumn col in temporal.Columns)
                {
                    newRow[col.Ordinal] = drow[col.Ordinal];
                }

                tabla.Rows.Add(newRow);
            }
        }

        private bool LlenaExpedientesSeleccion()
        {

            //Se crea la tabla que contendrá los expedientes seleccionados en la grilla
            DataTable ExpedientesSeleccion_ = new DataTable();

            foreach (DataGridViewColumn col in ExpedientesDataGridView.Columns)
            {
                ExpedientesSeleccion_.Columns.Add(col.DataPropertyName);
            }

            foreach (DataGridViewRow row in ExpedientesDataGridView.Rows)
            {
                //Expedientes seleccionados
                //if (Convert.ToBoolean(row.Cells["Exportar"].Value))
                //{
                DataRow newRow = ExpedientesSeleccion_.NewRow();

                foreach (DataGridViewColumn col in ExpedientesDataGridView.Columns)
                {
                    newRow[col.Index] = row.Cells[col.Index].Value;
                }

                ExpedientesSeleccion_.Rows.Add(newRow);
                //}
            }

            ExpedientesSeleccion = ExpedientesSeleccion_;

            if (ExpedientesSeleccion_.Rows.Count > 0)
            {
                ViewExpedientes.RowFilter = string.Empty;
                ViewExpedientes = new DataView(ExpedientesSeleccion_);
                return true;
            }

            return false;

        }

        private void ExportarImagen(FileProviderManager nManager, DataRowView ItemFile, ImageManager.EnumCompression nCompresion, string nFileFolderName)
        {
            dynamic Folios = nManager.GetFolios(Convert.ToInt64(ItemFile["fk_Expediente"]), Convert.ToInt16(ItemFile["fk_Folder"]), Convert.ToInt16(ItemFile["fk_File"]), Convert.ToInt16(ItemFile["id_Version"]));

            List<string> FileNames = new List<string>();
            string FileName = null;
            string FileNameAux = null;
            string ExtensionAux = string.Empty;

            try
            {
                for (short folio = 1; folio <= Folios; folio++)
                {
                    byte[] Imagen = null;
                    byte[] Thumbnail = null;

                    nManager.GetFolio(Convert.ToInt64(ItemFile["fk_Expediente"]), Convert.ToInt16(ItemFile["fk_Folder"]), Convert.ToInt16(ItemFile["fk_File"]), Convert.ToInt16(ItemFile["id_Version"]), folio, ref Imagen, ref Thumbnail);

                    FileName = Program.AppPath + Program.TempPath + Guid.NewGuid().ToString() + this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida;
                    FileNames.Add(FileName);

                    FileNamesCons.Add(FileName);

                    using (var fs = new FileStream(FileName, FileMode.Create))
                    {
                        fs.Write(Imagen, 0, Imagen.Length);
                        fs.Close();
                    }
                }


                if (!(this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF))
                {
                    if ((FileNames.Count > 0))
                    {
                        ImageManager.EnumFormat Format = default(ImageManager.EnumFormat);

                        switch (this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        {
                            case ".bmp":
                                Format = ImageManager.EnumFormat.Bmp;
                                break;
                            case ".gif":
                                Format = ImageManager.EnumFormat.Gif;
                                break;
                            case ".jpg":
                                Format = ImageManager.EnumFormat.Jpeg;
                                nCompresion = ImageManager.EnumCompression.Jpeg;
                                break;
                            case ".pdf":
                                Format = ImageManager.EnumFormat.Pdf;
                                nCompresion = ImageManager.EnumCompression.Jpeg;
                                break;
                            case ".png":
                                Format = ImageManager.EnumFormat.Png;
                                break;
                            case ".tif":
                                Format = ImageManager.EnumFormat.Tiff;
                                break;
                        }

                        bool Valido = true;
                        string MsgError = "";

                        if ((this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion))
                        {
                            //FileNameAux = EventManager.Nombre_Imagen_Exportar(Convert.ToInt64(ItemFile["fk_Expediente"]), Convert.ToInt16(ItemFile["fk_Folder"]), Convert.ToInt16(ItemFile["fk_File"]), Convert.ToInt32(ItemFile["fk_Documento"]), Convert.ToInt32(ItemFile["fk_Grupo"]), ref Valido, ref MsgError);

                            //if (((Valido == true) & (FileNameAux == string.Empty)))
                            //{
                            FileNameAux = Nombre_Imagen_Exportar(Convert.ToInt64(ItemFile["fk_Expediente"]), Convert.ToInt16(ItemFile["fk_Folder"]), Convert.ToInt16(ItemFile["fk_File"]), Convert.ToInt32(ItemFile["fk_Documento"]), Convert.ToInt32(ItemFile["fk_Grupo"]), ref Valido, ref MsgError);
                            //}
                        }

                        ExtensionAux = (formatoAux == ImageManager.EnumFormat.Pdf ? ".pdf" : this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString();

                        if (((Valido == true) && (FileNameAux == null || FileNameAux == string.Empty)))
                        {
                            FileName = nFileFolderName + ItemFile["File_Unique_Identifier"].ToString() + "_0001" + ExtensionAux;
                        }
                        else if (((Valido == true) && (FileNameAux != null || FileNameAux != string.Empty)))
                        {
                            ExtensionAux = Convert.ToString((object.ReferenceEquals(ExtensionAux, string.Empty) ? this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida : ExtensionAux));
                            FileName = nFileFolderName + FileNameAux + ExtensionAux;
                        }
                        else if (Valido == false)
                        {
                            throw new Exception(MsgError);
                        }

                        //-------------------------------------------------------------------------
                        ImageManager.Save(FileNames, FileName, "", formatoAux, nCompresion, false, Program.AppPath + Program.TempPath, true);
                        //-------------------------------------------------------------------------
                    }
                }
            }
            catch (Exception ex)
            {
                DMB.DesktopMessageShow("Exportar imagen", ref ex);
            }
        }

        public string GetFileName()
        {
            //Return _EventManager.Nombre_Imagen_Exportar
            return "";
        }

        private void BorrarTemporal()
        {
            dynamic objDirectoryInfo = new DirectoryInfo(Program.AppPath + Program.TempPath);
            FileInfo[] fileInfoArray = objDirectoryInfo.GetFiles();
            //FileInfo objFileInfo = default(FileInfo);
            foreach (FileInfo objFileInfo in fileInfoArray)
            {
                try
                {
                    objFileInfo.Delete();
                }
                catch
                {
                }
            }
        }

        private void GenerarVisor(DBCore.DBCoreDataBaseManager dbmCore, DBImaging.DBImagingDataBaseManager dbmImaging, int idOT, string OutputFolder, DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable FolderDataTable, DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable FileDataTable, DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable FileDataDataTable, DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable FileValidacionesDataTable)
        {
            const string DataBaseName = "ExportedData.accdb";
            string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + OutputFolder + DataBaseName + ";Persist Security Info=False";

            System.Data.OleDb.OleDbConnection Conexion = null;

            try
            {
                // Copiar visor
                if (!File.Exists(OutputFolder + DataBaseName))
                {
                    File.Copy(Program.AppPath + "OffLineViewer\\ExportedData.accdb", OutputFolder + DataBaseName, true);
                }
                if (!File.Exists(OutputFolder + "OffLineViewer.exe"))
                {
                    File.Copy(Program.AppPath + "OffLineViewer\\OffLineViewer.exe", OutputFolder + "OffLineViewer.exe", true);
                }
                if (!File.Exists(OutputFolder + "OffLineViewer.Library.dll"))
                {
                    File.Copy(Program.AppPath + "OffLineViewer\\OffLineViewer.Library.dll", OutputFolder + "OffLineViewer.Library.dll", true);
                }


                Conexion = new System.Data.OleDb.OleDbConnection(ConnectionString);
                Conexion.Open();

                dynamic Comando = new System.Data.OleDb.OleDbCommand("", Conexion);

                // Llaves
                dynamic KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

                string KeyName1 = "";
                if ((KeysDataTable.Count > 0))
                    KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave;
                string KeyName2 = "";
                if ((KeysDataTable.Count > 1))
                    KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave;
                string KeyName3 = "";
                if ((KeysDataTable.Count > 2))
                    KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave;

                // Crear Configuracion

                DataTable Dtresultados = new DataTable();
                System.Data.OleDb.OleDbDataAdapter adapter = default(System.Data.OleDb.OleDbDataAdapter);


                Comando.CommandText = "SELECT * FROM TBL_Config WHERE id_Entidad = " + this._Plugin.Manager.ImagingGlobal.Entidad + " AND id_Proyecto = " + this._Plugin.Manager.ImagingGlobal.Proyecto + ";";

                adapter = new System.Data.OleDb.OleDbDataAdapter(Comando);
                adapter.Fill(Dtresultados);

                if (Dtresultados.Rows.Count == 0)
                {
                    dynamic EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(this._Plugin.Manager.ImagingGlobal.Entidad);
                    Comando.CommandText = " INSERT INTO TBL_Config (id_Entidad, Nombre_Entidad, id_Proyecto, Nombre_Proyecto, Key_1, Key_2, Key_3)" + "SELECT " + this._Plugin.Manager.ImagingGlobal.Entidad + ", '" + EntidadDataTable(0).Nombre_Entidad + "', " + this._Plugin.Manager.ImagingGlobal.Proyecto + ", '" + this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto + "', '" + KeyName1 + "', '" + KeyName2 + "', '" + KeyName3 + "';";

                    Comando.ExecuteNonQuery();
                }



                // Crear los Esquemas
                dynamic EsquemasDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Esquema.DBExecute(idOT);




                foreach (var EsquemaRow in EsquemasDataTable)
                {
                    Comando.CommandText = "SELECT * FROM TBL_Esquema WHERE id_Esquema = " + EsquemaRow.id_Esquema + ";";

                    Dtresultados = new DataTable();
                    adapter = new System.Data.OleDb.OleDbDataAdapter(Comando);
                    adapter.Fill(Dtresultados);

                    if (Dtresultados.Rows.Count == 0)
                    {
                        Comando.CommandText = "INSERT INTO TBL_Esquema (id_Esquema, Nombre_Esquema)" + "SELECT " + EsquemaRow.id_Esquema + ", '" + EsquemaRow.Nombre_Esquema + "';";

                        Comando.ExecuteNonQuery();
                    }
                }

                // Crear los Documentos
                dynamic DocumentosDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Documento.DBExecute(idOT);

                foreach (var DocumentoRow in DocumentosDataTable)
                {
                    Comando.CommandText = "SELECT * FROM TBL_Documento WHERE id_Documento = " + DocumentoRow.id_Documento + ";";

                    Dtresultados = new DataTable();
                    adapter = new System.Data.OleDb.OleDbDataAdapter(Comando);
                    adapter.Fill(Dtresultados);
                    if (Dtresultados.Rows.Count == 0)
                    {
                        Comando.CommandText = "INSERT INTO TBL_Documento (id_Documento, Nombre_Documento)" + "SELECT " + DocumentoRow.id_Documento + ", '" + DocumentoRow.Nombre_Documento + "';";

                        Comando.ExecuteNonQuery();
                    }

                }

                // Crear Campos de Búsqueda
                dynamic CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT);

                foreach (var CampoBusquedaRow in CampoBusquedaDataTable)
                {
                    Comando.CommandText = "SELECT * FROM TBL_Campo_Busqueda WHERE" + " fk_Campo_Tipo = " + CampoBusquedaRow.fk_Campo_Tipo + " AND id_Campo_Busqueda = " + CampoBusquedaRow.id_Campo_Busqueda + ";";

                    Dtresultados = new DataTable();
                    adapter = new System.Data.OleDb.OleDbDataAdapter(Comando);
                    adapter.Fill(Dtresultados);

                    if (Dtresultados.Rows.Count == 0)
                    {
                        Comando.CommandText = "INSERT INTO TBL_Campo_Busqueda (fk_Campo_Tipo, id_Campo_Busqueda, Nombre_Campo_Busqueda)" + "SELECT " + CampoBusquedaRow.fk_Campo_Tipo + ", " + CampoBusquedaRow.id_Campo_Busqueda + ", '" + CampoBusquedaRow.Nombre_Campo_Busqueda + "';";

                        Comando.ExecuteNonQuery();

                    }

                }

                // Crear los Campos
                dynamic CamposDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo.DBExecute(idOT);

                foreach (var CampoRow in CamposDataTable)
                {
                    Comando.CommandText = "SELECT * FROM TBL_Campo WHERE" + " fk_Documento = " + CampoRow.fk_Documento + " AND id_Campo = " + CampoRow.id_Campo + ";";

                    Dtresultados = new DataTable();
                    adapter = new System.Data.OleDb.OleDbDataAdapter(Comando);
                    adapter.Fill(Dtresultados);

                    if (Dtresultados.Rows.Count == 0)
                    {
                        Comando.CommandText = "INSERT INTO TBL_Campo (fk_Documento, id_Campo, Nombre_Campo, Es_Campo_Busqueda, fk_Campo_Tipo, fk_Campo_Busqueda)" + "SELECT " + CampoRow.fk_Documento + ", " + CampoRow.id_Campo + ", '" + CampoRow.Nombre_Campo + "'" + ", " + (CampoRow.Es_Campo_Busqueda ? "1" : "0").ToString() + ", " + CampoRow.fk_Campo_Tipo + ", " + CampoRow.fk_Campo_Busqueda + ";";

                        Comando.ExecuteNonQuery();
                    }
                }

                // Crear las Validaciones
                dynamic ValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validacion.DBExecute(idOT);

                foreach (var ValidacionRow in ValidacionDataTable)
                {
                    Comando.CommandText = "SELECT * FROM TBL_Validacion WHERE" + " fk_Documento = " + ValidacionRow.fk_Documento + " AND id_Validacion = " + ValidacionRow.id_Validacion + ";";

                    Dtresultados = new DataTable();
                    adapter = new System.Data.OleDb.OleDbDataAdapter(Comando);
                    adapter.Fill(Dtresultados);

                    if (Dtresultados.Rows.Count == 0)
                    {
                        Comando.CommandText = "INSERT INTO TBL_Validacion (fk_Documento, id_Validacion, Pregunta_Validacion)" + "SELECT " + ValidacionRow.fk_Documento + ", " + ValidacionRow.id_Validacion + ", '" + ValidacionRow.Pregunta_Validacion + "';";

                        Comando.ExecuteNonQuery();
                    }
                }

                // Crear Folders            
                foreach (var FolderRow in FolderDataTable)
                {
                    Comando.CommandText = "INSERT INTO TBL_Folder (fk_Expediente, id_Folder, fk_Esquema, Key_1, Key_2, Key_3, CBarras_Folder)" + "SELECT " + FolderRow.fk_Expediente + ", " + FolderRow.fk_Folder + ", " + FolderRow.id_Esquema + ", '" + FolderRow.Key_1 + "'" + ", '" + FolderRow.Key_2 + "'" + ", '" + FolderRow.Key_3 + "'" + ", '" + FolderRow.CBarras_Folder + "';";

                    Comando.ExecuteNonQuery();
                }

                // Crear Files            
                foreach (var FileRow in FileDataTable)
                {
                    Comando.CommandText = "INSERT INTO TBL_File (fk_Expediente, fk_Folder, id_File, id_Version, File_Unique_Identifier, fk_Documento, Nombre_Imagen_File, Folios_Documento_File, Tamaño_Imagen_File)" + "SELECT " + FileRow.fk_Expediente + ", " + FileRow.fk_Folder + ", " + FileRow.fk_File + ", " + FileRow.id_Version + ", '" + FileRow.File_Unique_Identifier.ToString() + "'" + ", " + FileRow.fk_Documento + ", '" + FileRow.Nombre_Imagen_File + "'" + ", " + FileRow.Folios_Documento_File + ", " + FileRow.Tamaño_Imagen_File + ";";

                    Comando.ExecuteNonQuery();
                }

                // Crear File Data            
                foreach (var DataRow in FileDataDataTable)
                {
                    //Dim valor As String = ""
                    //If (Not DataRow.IsNull("Valor_File_Data")) Then valor = DataRow.Valor_File_Data
                    Comando.CommandText = "INSERT INTO TBL_File_Data (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Campo, fk_Documento, fk_Campo_Tipo, Valor_File_Data)" + "SELECT " + DataRow.fk_Expediente + ", " + DataRow.fk_Folder + ", " + DataRow.fk_File + ", " + DataRow.id_Version + ", " + DataRow.id_Campo + ", " + DataRow.fk_Documento + ", " + DataRow.fk_Campo_Tipo + ", '" + DataRow.Valor_File_Data + "';";

                    Comando.ExecuteNonQuery();
                }

                // Crear File Validacion            
                foreach (var DataRow in FileValidacionesDataTable)
                {
                    Comando.CommandText = "INSERT INTO TBL_File_Validacion (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Validacion, fk_Documento, Respuesta)" + "SELECT " + DataRow.fk_Expediente + ", " + DataRow.fk_Folder + ", " + DataRow.fk_File + ", " + DataRow.id_Version + ", " + DataRow.id_Validacion + ", " + DataRow.fk_Documento + ", " + (DataRow.Respuesta ? "1" : "0").ToString() + ";";

                    Comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if ((Conexion != null))
                    Conexion.Close();
            }
        }

        private void GenerarXML(DBCore.DBCoreDataBaseManager dbmCore, DBImaging.DBImagingDataBaseManager dbmImaging, int idOT, string OutputFolder, DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable FolderDataTable, DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable FileDataTable, DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable FileDataDataTable, DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable FileValidacionDataTable)
        {
            Miharu.Imaging.OffLineViewer.Library.xsdOfflineData ExportacionDataSet = new Miharu.Imaging.OffLineViewer.Library.xsdOfflineData();

            // Llaves
            dynamic KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

            string KeyName1 = "";
            if ((KeysDataTable.Count > 0))
                KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave;
            string KeyName2 = "";
            if ((KeysDataTable.Count > 1))
                KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave;
            string KeyName3 = "";
            if ((KeysDataTable.Count > 2))
                KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave;

            // Configuración
            dynamic EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(this._Plugin.Manager.ImagingGlobal.Entidad);
            ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(this._Plugin.Manager.ImagingGlobal.Entidad, EntidadDataTable(0).Nombre_Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto, KeyName1, KeyName2, KeyName3);

            // Folder
            foreach (var FolderRow in FolderDataTable)
            {
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder);
            }

            // File
            foreach (var FileRow in FileDataTable)
            {
                ExportacionDataSet.TBL_File.AddTBL_FileRow(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.File_Unique_Identifier, FileRow.Nombre_Documento, FileRow.Nombre_Imagen_File, FileRow.Folios_Documento_File, FileRow.Tamaño_Imagen_File);
            }

            // Data
            foreach (var FileDataRow in FileDataDataTable)
            {
                ExportacionDataSet.TBL_File_Data.AddTBL_File_DataRow(FileDataRow.fk_Expediente, FileDataRow.fk_Folder, FileDataRow.fk_File, FileDataRow.id_Version, FileDataRow.id_Campo, FileDataRow.Nombre_Campo, FileDataRow.Es_Campo_Busqueda, FileDataRow.fk_Campo_Tipo, FileDataRow.fk_Campo_Busqueda, FileDataRow.Valor_File_Data,
                FileDataRow.fk_Documento);
            }

            // Validaciones
            foreach (var FileValidacionRow in FileValidacionDataTable)
            {
                ExportacionDataSet.TBL_File_Validacion.AddTBL_File_ValidacionRow(FileValidacionRow.fk_Expediente, FileValidacionRow.fk_Folder, FileValidacionRow.fk_File, FileValidacionRow.id_Version, FileValidacionRow.id_Validacion, FileValidacionRow.Pregunta_Validacion, FileValidacionRow.Respuesta, FileValidacionRow.fk_Documento);
            }

            // Busqueda
            dynamic CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT);
            foreach (var CampoBusquedaRow in CampoBusquedaDataTable)
            {
                ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CampoBusquedaRow.fk_Campo_Tipo, CampoBusquedaRow.id_Campo_Busqueda, CampoBusquedaRow.Nombre_Campo_Busqueda);
            }


            ExportacionDataSet.WriteXml(OutputFolder + "\\" + "ExportedData.xml");
        }

        private void GenerarXMLExpedientes(bool Generar, DBCore.DBCoreDataBaseManager dbmCore, DBImaging.DBImagingDataBaseManager dbmImaging, int idOT, string OutputFolder, DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable FolderDataTable, DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable FileDataTable, DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable FileDataDataTable, DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable FileValidacionDataTable, Miharu.Imaging.OffLineViewer.Library.xsdOfflineData ExportacionDataSet)
        {
            //Dim ExportacionDataSet As New OffLineViewer.Library.xsdOffLineData

            // Llaves
            dynamic KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);

            string KeyName1 = "";
            if ((KeysDataTable.Count > 0))
                KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave;
            string KeyName2 = "";
            if ((KeysDataTable.Count > 1))
                KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave;
            string KeyName3 = "";
            if ((KeysDataTable.Count > 2))
                KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave;

            // Configuración
            if (ExportacionDataSet.TBL_Config.Select("id_Entidad = " + this._Plugin.Manager.ImagingGlobal.Entidad + "and  id_Proyecto = " + this._Plugin.Manager.ImagingGlobal.Proyecto).Length == 0)
            {
                dynamic EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(this._Plugin.Manager.ImagingGlobal.Entidad);
                ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(this._Plugin.Manager.ImagingGlobal.Entidad, EntidadDataTable(0).Nombre_Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto, KeyName1, KeyName2, KeyName3);
            }

            // Folder
            foreach (var FolderRow in FolderDataTable)
            {
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder);
            }

            // File
            foreach (var FileRow in FileDataTable)
            {
                ExportacionDataSet.TBL_File.AddTBL_FileRow(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.File_Unique_Identifier, FileRow.Nombre_Documento, FileRow.Nombre_Imagen_File, FileRow.Folios_Documento_File, FileRow.Tamaño_Imagen_File);
            }

            // Data
            foreach (var FileDataRow in FileDataDataTable)
            {
                ExportacionDataSet.TBL_File_Data.AddTBL_File_DataRow(FileDataRow.fk_Expediente, FileDataRow.fk_Folder, FileDataRow.fk_File, FileDataRow.id_Version, FileDataRow.id_Campo, FileDataRow.Nombre_Campo, FileDataRow.Es_Campo_Busqueda, FileDataRow.fk_Campo_Tipo, FileDataRow.fk_Campo_Busqueda, FileDataRow.Valor_File_Data,
                FileDataRow.fk_Documento);
            }

            // Validaciones
            foreach (var FileValidacionRow in FileValidacionDataTable)
            {
                ExportacionDataSet.TBL_File_Validacion.AddTBL_File_ValidacionRow(FileValidacionRow.fk_Expediente, FileValidacionRow.fk_Folder, FileValidacionRow.fk_File, FileValidacionRow.id_Version, FileValidacionRow.id_Validacion, FileValidacionRow.Pregunta_Validacion, FileValidacionRow.Respuesta, FileValidacionRow.fk_Documento);
            }

            // Busqueda
            dynamic CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT);
            foreach (var CampoBusquedaRow in CampoBusquedaDataTable)
            {
                if (ExportacionDataSet.TBL_Campo_Busqueda.Select("fk_Campo_Tipo = " + CampoBusquedaRow.fk_Campo_Tipo + "and  id_Campo_Busqueda = " + CampoBusquedaRow.id_Campo_Busqueda).Length == 0)
                {
                    ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CampoBusquedaRow.fk_Campo_Tipo, CampoBusquedaRow.id_Campo_Busqueda, CampoBusquedaRow.Nombre_Campo_Busqueda);
                }

            }



            if (Generar)
            {
                if (!File.Exists(OutputFolder + "\\" + "ExportedData.xml"))
                {
                    ExportacionDataSet.WriteXml(OutputFolder + "\\" + "ExportedData.xml");
                }

            }

        }

        private void GenerarTXT(string OutputFolder, DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable FolderDataTable, DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable FileDataTable, DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable FileDataDataTable, DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable FileValidacionDataTable)
        {
            dynamic CSVData = new Slyg.Tools.CSV.CSVData(Constants.vbTab, "\"", true);

            CSVData.SaveAsCSV(new Slyg.Tools.CSV.CSVTable(FolderDataTable), OutputFolder + "Folders.txt", false);
            CSVData.SaveAsCSV(new Slyg.Tools.CSV.CSVTable(FileDataTable), OutputFolder + "Files.txt", false);
            CSVData.SaveAsCSV(new Slyg.Tools.CSV.CSVTable(FileDataDataTable), OutputFolder + "Data.txt", false);
            CSVData.SaveAsCSV(new Slyg.Tools.CSV.CSVTable(FileValidacionDataTable), OutputFolder + "Validaciones.txt", false);
        }

        private void MostrarDatagrid()
        {
            if ((this.CheckBoxExpedientes.Checked))
            {
                OTLabel.Text = "Expedientes";
                ExpedientesDataGridView.Visible = true;
                OTDataGridView.Visible = false;
            }
            else
            {
                OTLabel.Text = "OTs";
                ExpedientesDataGridView.Visible = false;
                OTDataGridView.Visible = true;
            }

        }

        private void Load_FormatoCargue()
        {
            if ((!Usa_Exportacion_PDF))
            {
                formatoAux = formato;
                CompresionAux = compresion;
            }
            else
            {
                formatoAux = ImageManager.EnumFormat.Pdf;
                CompresionAux = Utilities.GetEnumCompression((DesktopConfig.FormatoImagenEnum)formatoAux);
            }

        }

        private void ExportarImagenAgrupada(FileProviderManager nManager, DataView nFilesExpedientesbyGroupDataView, int ngrupo, long nfk_Expediente, short nfk_Folder, ImageManager.EnumCompression nCompresion, string nFileFolderName)
        {
            List<string> FileNames = new List<string>();
            string FileName = null;
            string FileNameAux = null;
            string ExtensionAux = string.Empty;


            foreach (DataRowView itemfile in nFilesExpedientesbyGroupDataView)
            {
                dynamic Folios = nManager.GetFolios(Convert.ToInt64(itemfile["fk_Expediente"]), Convert.ToInt16(itemfile["fk_Folder"]), Convert.ToInt16(itemfile["fk_File"]), Convert.ToInt16(itemfile["id_Version"]));

                for (short folio = 1; folio <= Folios; folio++)
                {
                    byte[] Imagen = null;
                    byte[] Thumbnail = null;

                    nManager.GetFolio(Convert.ToInt64(itemfile["fk_Expediente"]), Convert.ToInt16(itemfile["fk_Folder"]), Convert.ToInt16(itemfile["fk_File"]), Convert.ToInt16(itemfile["id_Version"]), folio, ref Imagen, ref Thumbnail);


                    FileName = Program.AppPath + Program.TempPath + Guid.NewGuid().ToString() + this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida;
                    FileNames.Add(FileName);

                    using (var fs = new FileStream(FileName, FileMode.Create))
                    {
                        fs.Write(Imagen, 0, Imagen.Length);
                        fs.Close();
                    }
                }
            }

            if ((FileNames.Count > 0))
            {
                ImageManager.EnumFormat Format = default(ImageManager.EnumFormat);

                switch (this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                {
                    case ".bmp":
                        Format = ImageManager.EnumFormat.Bmp;
                        break;
                    case ".gif":
                        Format = ImageManager.EnumFormat.Gif;
                        break;
                    case ".jpg":
                        Format = ImageManager.EnumFormat.Jpeg;
                        nCompresion = ImageManager.EnumCompression.Jpeg;
                        break;
                    case ".pdf":
                        Format = ImageManager.EnumFormat.Pdf;
                        nCompresion = ImageManager.EnumCompression.Jpeg;
                        break;
                    case ".png":
                        Format = ImageManager.EnumFormat.Png;
                        break;
                    case ".tif":
                        Format = ImageManager.EnumFormat.Tiff;
                        break;
                }

                bool Valido = true;
                string MsgError = "";

                if ((this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion))
                {
                    FileNameAux = EventManager.Nombre_Imagen_Agrupada_Exportar(nfk_Expediente, nfk_Folder, ngrupo, ref Valido, ref MsgError);

                    if (((Valido == true) & (FileNameAux == string.Empty)))
                    {
                        FileNameAux = Nombre_Imagen_Agrupada_Exportar(nfk_Expediente, nfk_Folder, ngrupo, ref Valido, ref MsgError);
                    }
                }

                ExtensionAux = (formatoAux == ImageManager.EnumFormat.Pdf ? ".pdf" : this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString();

                if (((Valido == true) & (FileNameAux == string.Empty)))
                {
                    FileName = nFileFolderName + Guid.NewGuid().ToString() + "_0001" + ExtensionAux;
                }
                else if (((Valido == true) & (FileNameAux != string.Empty)))
                {
                    ExtensionAux = Convert.ToString((object.ReferenceEquals(ExtensionAux, string.Empty) ? this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida : ExtensionAux));
                    FileName = nFileFolderName + FileNameAux + ExtensionAux;
                }
                else if (Valido == false)
                {
                    throw new Exception(MsgError);
                }

                //-------------------------------------------------------------------------
                ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, false, Program.AppPath + Program.TempPath, true);
                //-------------------------------------------------------------------------
            }
        }
        #endregion

        #region " Funciones "
        private bool Validar()
        {

            if (!validarFechaProceso())
            {
                return false;
            }

            if ((!Directory.Exists(CarpetaSalidaTextBox.Text)))
            {
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.CarpetaSalidaTextBox.Focus();

            }
            //else if ((Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 | Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0))
            //{
            //    MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.CarpetaSalidaTextBox.Focus();

            //}
            else if ((this.OTDataGridView.SelectedRows.Count == 0) & !this.CheckBoxExpedientes.Checked)
            {
                MessageBox.Show("Se debe seleccionar una OT", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OTDataGridView.Focus();

            }
            else if (!this.CheckBoxExpedientes.Checked)
            {
                dynamic OTRow = (DBImaging.SchemaProcess.CTA_Exportacion_OTRow)((DataRowView)this.OTDataGridView.CurrentRow.DataBoundItem).Row;

                // Validar si ya fue exportado
                if ((OTRow.Exportado))
                {
                    dynamic Respuesta = MessageBox.Show("La OT: " + OTRow.id_OT + ", ya fue exportada, ¿desea volverla a exportar?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if ((Respuesta == DialogResult.No))
                        return false;
                }

                // Validar si la OT se puede exportar
                DBImaging.DBImagingDataBaseManager dbmImaging = null;

                try
                {
                    dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                    dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    dynamic Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(OTRow.id_OT);

                    if ((!Resultado))
                    {
                        MessageBox.Show("La OT no ha sido totalmente procesada y no se puede exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.OTDataGridView.Focus();
                    }
                    else
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if ((dbmImaging != null))
                        dbmImaging.Connection_Close();
                }

            }
            else
            {
                //Validaciones Exportación Expedientes

                if ((this.ExpedientesDataGridView.Rows.Count == 0))
                {
                    MessageBox.Show("No se han seleccionado Expedientes para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ExpedientesDataGridView.Focus();
                    return false;

                }
                else
                {
                    foreach (DataGridViewRow row in ExpedientesDataGridView.Rows)
                    {
                        if (ExpedientesDataGridView.Rows.Count > 0)
                        {
                            return true;
                        }
                        //if (Convert.ToBoolean(row.Cells["Exportar"].Value))
                        //{
                        //    return true;
                        //}
                    }

                    MessageBox.Show("No se han seleccionado Expedientes para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ExpedientesDataGridView.Focus();
                    return false;

                }

            }

            return false;
        }

        private bool validarFechaProceso()
        {
            dynamic FechaInicial = new DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day);
            dynamic FechaFinal = new DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day);

            if ((FechaInicial > FechaFinal))
            {
                MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;

            }

            return true;

        }

        private int getFechaInicio()
        {
            return Convert.ToInt32(this.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"));
        }

        private int getFechaFinal()
        {
            return Convert.ToInt32(this.FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd"));
        }

        private DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable getOTs()
        {
            DBImaging.DBImagingDataBaseManager dbmImaging = null;

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                return dbmImaging.SchemaProcess.PA_Exportacion_OT.DBExecute(this._Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, getFechaInicio(), getFechaFinal());

                //    Return dbmImaging.SchemaProcess.CTA_Exportacion_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_proceso(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                //                                                                                                                            this._Plugin.Manager.ImagingGlobal.Entidad, _
                //                                                                                                                            this._Plugin.Manager.ImagingGlobal.Proyecto, _
                //                                                                                                                            getFechaInicio())
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((dbmImaging != null))
                    dbmImaging.Connection_Close();
            }

            return new DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable();
        }

        private /*DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable*/ DataTable getExpedientes()
        {
            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                return dbmIntegration.SchemaBcoPopular.PA_Exportacion_Expediente.DBExecute(Convert.ToDateTime(this.FechaProcesoDateTimePicker.Value), Convert.ToDateTime(this.FechaProcesoFinalDateTimePicker.Value), this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);
                //return (DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable)dbmIntegration.SchemaBcoPopular.PA_Exportacion_Expediente.DBExecute(Convert.ToDateTime(this.FechaProcesoDateTimePicker.Value), Convert.ToDateTime(this.FechaProcesoFinalDateTimePicker.Value), this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);
                //return dbmImaging.SchemaProcess.PA_Exportacion_Expediente.DBExecute(Convert.ToDateTime(this.FechaProcesoDateTimePicker.Value), Convert.ToDateTime(this.FechaProcesoFinalDateTimePicker.Value), this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((dbmImaging != null))
                    dbmImaging.Connection_Close();
                if ((dbmIntegration != null))
                    dbmIntegration.Connection_Close();
            }

            return new DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable();
        }

        private string Nombre_Imagen_Exportar(long nidExpediente, short nidFolder, short nidFile, int nFk_Documento, int nGrupo, ref bool nValido, ref string nMsgError)
        {
            string Nombre_Imagen = string.Empty;

            DBImaging.DBImagingDataBaseManager dbmImaging = null;
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);

                dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                Nombre_Imagen = dbmImaging.SchemaProcess.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, nidFile, nFk_Documento, nGrupo);
                Nombre_Imagen = dbmIntegration.SchemaBcoPopular.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, nidFile, nFk_Documento, nGrupo);

                if (Nombre_Imagen == string.Empty)
                {
                    nValido = false;
                    nMsgError = "No se encontró nombre de imagen para el expediente: " + nidExpediente.ToString() + ", fk_Documento: " + nFk_Documento.ToString();
                }
            }
            catch (Exception ex)
            {
                nValido = false;
                throw new Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString() + ") Se genero el error:" + ex.Message, ex.InnerException);
            }
            finally
            {
                if ((dbmImaging != null))
                    dbmImaging.Connection_Close();
                if ((dbmIntegration != null))
                    dbmIntegration.Connection_Close();
            }


            return Nombre_Imagen;
        }

        private string Nombre_Imagen_Agrupada_Exportar(long nidExpediente, short nidFolder, int ngrupo, ref bool nValido, ref string nMsgError)
        {
            string Nombre_Imagen = string.Empty;

            DBImaging.DBImagingDataBaseManager dbmImaging = null;

            try
            {
                dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);

                dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                Nombre_Imagen = dbmImaging.SchemaProcess.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, DBNull.Value, DBNull.Value, ngrupo);

                if (Nombre_Imagen == string.Empty)
                {
                    nValido = false;
                    nMsgError = "No se encontró nombre de imagen para el expediente: " + nidExpediente.ToString() + ", fk_Documento: " + 0.ToString();
                }
            }
            catch (Exception ex)
            {
                nValido = false;
                throw new Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString() + ") Se genero el error:" + ex.Message, ex.InnerException);
            }
            finally
            {
                if ((dbmImaging != null))
                    dbmImaging.Connection_Close();
            }


            return Nombre_Imagen;
        }
        public FormExport(GobernacionSantanderPlugin _plugin)
        {
            InitializeComponent();
            this._Plugin = _plugin;
            Load += FormExport_Load;
        }

        public void CrearArchivoMunicipio(int municipio, string fehaINI, string fechaFIN, string ruta)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoPopularConnectionString);
            try
            {


                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                var dtConsolidado = dbmIntegration.SchemaBcoPopular.PA_Reporte_Informe_Ciudad.DBExecute(fehaINI, fechaFIN, this._Plugin.Manager.ImagingGlobal.Proyecto, this._Plugin.Manager.ImagingGlobal.Entidad, municipio);
                List<DataTable> ltDatatables = new List<DataTable>();
                ltDatatables.Add(dtConsolidado);

                dtConsolidado.TableName = "InformeCiudad";

                var _nombreReporte = string.Format(dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("INFORME_MENSUAL_CIUDAD").FirstOrDefault().Formato_Reporte, DateTime.Now);
                var _tipoReporte = dbmIntegration.SchemaBcoPopular.TBL_Config_Reporte.DBFindByNombre_Reporte("DISPERSION_QUINCENAL").FirstOrDefault().Extension_Salida;

                if (_tipoReporte != ".xlsx" && _tipoReporte != ".xls")
                {
                    _tipoReporte = ".xlsx";
                }

                if (Genera_ReporteExcel_2(ruta, _tipoReporte, _nombreReporte, ltDatatables, "hoja1"))
                {
                    //MessageBox.Show("Reporte Excel Generado con Exito!!!");
                    //System.Diagnostics.Process.Start(ruta);
                }
            }
            catch
            {
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }

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

    public class ImageManagerDomain : MarshalByRefObject
    {

        public void Save(List<string> nInputFileNames, string nOutputFileName, string nSuffixFormat, ImageManager.EnumFormat nFormat, ImageManager.EnumCompression nCompression, bool nSinglePage, string nTempPath, bool nIsInputSingle)
        {
            ImageManager.Save(nInputFileNames, nOutputFileName, nSuffixFormat, nFormat, nCompression, nSinglePage, nTempPath, nIsInputSingle);
        }
    }
}
