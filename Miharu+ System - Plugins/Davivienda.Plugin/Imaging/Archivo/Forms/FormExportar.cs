using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Slyg.Tools.Imaging;
using Miharu.Desktop.Library.Config;
using Slyg.Tools;
using Miharu.FileProvider.Library;
using DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl;
using DBIntegration.SchemaBcoDavivienda;
using Miharu.Tools.Progress;

namespace Davivienda.Plugin.Imaging.Archivo.Forms
{
    public partial class FormExportar : Form
    {
        #region " Declaraciones "
        private bool Usa_Exportacion_PDF;
        private Slyg.Tools.Imaging.ImageManager.EnumFormat formatoAux;
        private Slyg.Tools.Imaging.ImageManager.EnumCompression CompresionAux;
        private Slyg.Tools.Imaging.ImageManager.EnumFormat formato;
        private ArchivoPlugin _Plugin;

        Slyg.Tools.Imaging.ImageManager.EnumCompression compresion;
        private DataView ViewExpedientes = new DataView();
        public static List<string> FileNamesCons = new List<string>();

        #endregion

        public FormExportar(ArchivoPlugin _plugin)
        {
            InitializeComponent();
            this._Plugin = _plugin;
        }

        private void BuscarCarpetaButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Selector = new FolderBrowserDialog();

            Selector.SelectedPath = CarpetaSalidaTextBox.Text;
            if ((Selector.ShowDialog() == DialogResult.OK))
            {
                this.CarpetaSalidaTextBox.Text = Selector.SelectedPath;
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportarButton_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
                DBImaging.DBImagingDataBaseManager dbmImaging = null;
                Miharu.Tools.Progress.FormProgress ProgressForm = new Miharu.Tools.Progress.FormProgress();

                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoDaviviendaConnectionString);
                dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);

                try
                {
                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                    dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    var FileDataTable = new DBIntegration.SchemaBcoDavivienda.CTA_Exportacion_Expedientes_ArchivoDataTable();
                    var FileDataDataTable = new DBIntegration.SchemaBcoDavivienda.CTA_Exportacion_Expedientes_Data_ArchivoDataTable();

                    FileDataTable = dbmIntegration.SchemaBcoDavivienda.PA_GetExpedientesExportacion.DBExecute(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, Convert.ToInt32(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), Convert.ToInt32(FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")));
                    FileDataDataTable = dbmIntegration.SchemaBcoDavivienda.PA_GetExpedientesDataExportacion.DBExecute(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, Convert.ToInt32(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), Convert.ToInt32(FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd")));

                    int cantfilesxcarpeta = 0;

                    cantfilesxcarpeta = Convert.ToInt32(dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, "CantidadFilesxCarpeta")[0].Valor_Parametro_Sistema);

                    if ((FileDataTable.Rows.Count > 0) && (FileDataDataTable.Rows.Count > 0))
                    {
                        DialogResult Respuesta = default(DialogResult);

                        Respuesta = MessageBox.Show("Se encontró : " + FileDataTable.Rows.Count + " Unidades Documentales, " + "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (Respuesta == DialogResult.Yes)
                        {
                            int Progreso = 0;

#if !Debug
                            ProgressForm.Show();
#endif

                            ProgressForm.SetProceso("Exportar");
                            ProgressForm.SetAccion("Obteniendo imágenes...");
                            ProgressForm.SetProgreso(0);
                            ProgressForm.SetMaxValue(FileDataTable.Rows.Count);

                            Application.DoEvents();

                            var esquemaExportacion = FileDataTable.GroupBy(n => new { n.Sigla }).Select(g => g.Key.Sigla).ToList();

                            foreach (string sigla in esquemaExportacion)
                            {
                                var filesByGroup = FileDataTable.Select("Sigla = '" + sigla + "'").CopyToDataTable();

                                DataView FilesbyGroupDataView = new DataView(filesByGroup);

                                int cantidadregistros = FilesbyGroupDataView.Count;
                                int cantidadCarpetas = 1;
                                if (cantfilesxcarpeta > 0)
                                {
                                    cantidadCarpetas = Convert.ToInt32(Math.Ceiling((double)cantidadregistros / (double)cantfilesxcarpeta));
                                }

                                string OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\";

                                ImageManager.EnumCompression Compresion = default(ImageManager.EnumCompression);

                                if ((this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida == (short)DesktopConfig.FormatoImagenEnum.TIFF_Bitonal))
                                {
                                    Compresion = ImageManager.EnumCompression.Ccitt4;
                                }
                                else
                                {
                                    Compresion = ImageManager.EnumCompression.Lzw;
                                }

                                if (cantidadCarpetas == 1)
                                {

                                    var NomArchivo = sigla + '_' + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
                                    var OutputFolderCarpeta = OutputFolder + NomArchivo;

                                    if (!Directory.Exists(OutputFolderCarpeta))
                                    {
                                        Directory.CreateDirectory(OutputFolderCarpeta);
                                    }

                                    //crear el archivo
                                    var sw = new StreamWriter(File.Open(OutputFolderCarpeta + "\\" + NomArchivo + ".csv", FileMode.Create), System.Text.Encoding.Default);
                                    sw.Close();
                                    sw.Dispose();

                                    foreach (DataRowView ItemFile in FilesbyGroupDataView)
                                    {
                                        FileProviderManager manager = null;

                                        try
                                        {
                                            if (ProgressForm.Cancelar)
                                                throw new Exception("La acción fue cancelada por el usuario");

                                            var expediente = Convert.ToInt64(ItemFile["fk_Expediente"]);
                                            var folder = Convert.ToInt16(ItemFile["fk_Folder"]);
                                            var file = Convert.ToInt16(ItemFile["fk_File"]);
                                            var version = Convert.ToInt16(ItemFile["fk_Version"]);

                                            manager = new FileProviderManager(expediente, folder, file, version, ref dbmImaging, this._Plugin.Manager.Sesion.Usuario.id);
                                            manager.Connect();

                                            ExportarImagen(manager, ItemFile, Compresion, OutputFolderCarpeta);

                                            //escribir en el archivo csv
                                            var DataExpediente = FileDataDataTable.Select("fk_Expediente = " + expediente + " AND fk_Folder = " + folder + " AND fk_File = " + file).CopyToDataTable();
                                            DataView DataExpedienteView = new DataView(DataExpediente);
                                            string datacsv = "";

                                            foreach (DataRowView registro in DataExpedienteView)
                                            {
                                                if (datacsv == "")
                                                {
                                                    datacsv = registro["dataCsv"].ToString();
                                                }
                                                else
                                                {
                                                    datacsv = datacsv + ";" + registro["dataCsv"].ToString();
                                                }
                                            }

                                            datacsv = ItemFile["DataCsv"].ToString().Replace("@data", datacsv);


                                            sw = new StreamWriter(File.Open(OutputFolderCarpeta + "\\" + NomArchivo + ".csv", FileMode.Append), System.Text.Encoding.Default);
                                            sw.WriteLine(datacsv);
                                            sw.Close();
                                            sw.Dispose();

                                            Progreso += 1;
                                            ProgressForm.SetProgreso(Progreso);
                                            Application.DoEvents();
                                        }
                                        catch (Exception ex)
                                        {
                                            throw;
                                        }
                                        finally
                                        {
                                            if ((manager != null))
                                                manager.Disconnect();
                                        }
                                    }
                                }
                                else //cantidad de carpetas > 1
                                {
                                    int contador = 1;
                                    bool crearnuevacarpeta = true;
                                    string NomArchivo = "";
                                    string OutputFolderCarpeta = "";
                                    StreamWriter sw;

                                    foreach (DataRowView ItemFile in FilesbyGroupDataView)
                                    {
                                        if (crearnuevacarpeta)
                                        {
                                            NomArchivo = sigla + '_' + DateTime.Now.ToString("dd-MM-yyyy-HH-mm");
                                            OutputFolderCarpeta = OutputFolder + NomArchivo;
                                            if (!Directory.Exists(OutputFolderCarpeta))
                                            {
                                                Directory.CreateDirectory(OutputFolderCarpeta);
                                            }

                                            //crear el archivo
                                            sw = new StreamWriter(File.Open(OutputFolderCarpeta + "\\" + NomArchivo + ".csv", FileMode.Create), System.Text.Encoding.Default);
                                            sw.Close();
                                            sw.Dispose();
                                        }
                                        crearnuevacarpeta = false;

                                        FileProviderManager manager = null;

                                        try
                                        {
                                            if (ProgressForm.Cancelar)
                                                throw new Exception("La acción fue cancelada por el usuario");

                                            var expediente = Convert.ToInt64(ItemFile["fk_Expediente"]);
                                            var folder = Convert.ToInt16(ItemFile["fk_Folder"]);
                                            var file = Convert.ToInt16(ItemFile["fk_File"]);
                                            var version = Convert.ToInt16(ItemFile["fk_Version"]);

                                            manager = new FileProviderManager(expediente, folder, file, version, ref dbmImaging, this._Plugin.Manager.Sesion.Usuario.id);
                                            manager.Connect();

                                            ExportarImagen(manager, ItemFile, Compresion, OutputFolderCarpeta);

                                            //escribir en el archivo csv
                                            var DataExpediente = FileDataDataTable.Select("fk_Expediente = " + expediente + " AND fk_Folder = " + folder + " AND fk_File = " + file).CopyToDataTable();
                                            DataView DataExpedienteView = new DataView(DataExpediente);
                                            string datacsv = "";

                                            foreach (DataRowView registro in DataExpedienteView)
                                            {
                                                if (datacsv == "")
                                                {
                                                    datacsv = registro["dataCsv"].ToString();
                                                }
                                                else
                                                {
                                                    datacsv = datacsv + ";" + registro["dataCsv"].ToString();
                                                }
                                            }

                                            datacsv = ItemFile["DataCsv"].ToString().Replace("@data", datacsv);


                                            sw = new StreamWriter(File.Open(OutputFolderCarpeta + "\\" + NomArchivo + ".csv", FileMode.Append), System.Text.Encoding.Default);
                                            sw.WriteLine(datacsv);
                                            sw.Close();
                                            sw.Dispose();

                                            contador = contador + 1;

                                            if (contador > cantfilesxcarpeta)
                                            {
                                                crearnuevacarpeta = true;
                                                contador = 1;
                                            }

                                            Progreso += 1;
                                            ProgressForm.SetProgreso(Progreso);
                                            Application.DoEvents();
                                        }
                                        catch (Exception ex)
                                        {
                                            throw;
                                        }
                                        finally
                                        {
                                            if ((manager != null))
                                                manager.Disconnect();

                                        }
                                    }
                                }
                            }
                            MessageBox.Show("Proceso finalizado", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Operación cancelada por Usuario", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay registros para exportar en el rango de fechas seleccionadas", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    ProgressForm.Hide();
                    Application.DoEvents();
                }
                finally
                {
                    if ((dbmImaging != null))
                        dbmImaging.Connection_Close();
                    if ((dbmIntegration != null))
                        dbmIntegration.Connection_Close();

                    ProgressForm.Close();
                }
            }

        }

        private void ExportarImagen(FileProviderManager nManager, DataRowView ItemFile, ImageManager.EnumCompression nCompresion, string nFileFolderName)
        {
            try
            {
                short Folios = 0;

                Folios = nManager.GetFolios(Convert.ToInt64(ItemFile["fk_Expediente"]), Convert.ToInt16(ItemFile["fk_Folder"]), Convert.ToInt16(ItemFile["fk_File"]), Convert.ToInt16(ItemFile["fk_Version"]));

                List<string> FileNames = new List<string>();
                string FileName = null;
                string FileNameAux = null;
                string ExtensionAux = string.Empty;

                for (short folio = 1; folio <= Folios; folio++)
                {
                    byte[] Imagen = null;
                    byte[] Thumbnail = null;

                    nManager.GetFolio(Convert.ToInt64(ItemFile["fk_Expediente"]), Convert.ToInt16(ItemFile["fk_Folder"]), Convert.ToInt16(ItemFile["fk_File"]), Convert.ToInt16(ItemFile["fk_Version"]), (short)folio, ref Imagen, ref Thumbnail);


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
                                //nCompresion = ImageManager.EnumCompression.Lzw;
                                break;
                            case ".png":
                                Format = ImageManager.EnumFormat.Png;
                                break;
                            case ".tif":
                                Format = ImageManager.EnumFormat.Tiff;
                                //nCompresion = ImageManager.EnumCompression.Ccitt4;

                                //nCompresion = ImageManager.EnumCompression.Lzw;
                                break;
                        }

                        bool Valido = true;
                        string MsgError = "";


                        FileNameAux = ItemFile["Nombre_Imagen_File"].ToString();

                        if (FileNameAux == string.Empty)
                        {
                            Valido = false;
                            MsgError = "No se encontró nombre de imagen para el expediente: " + Convert.ToInt64(ItemFile["fk_Expediente"]).ToString() + ", fk_Documento: " + Convert.ToInt64(ItemFile["fk_Documento"]).ToString();
                        }

                        ExtensionAux = (formatoAux == ImageManager.EnumFormat.Pdf ? ".pdf" : this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString();

                        if (((Valido == true) && (FileNameAux != null || FileNameAux != string.Empty)))
                        {
                            ExtensionAux = Convert.ToString((object.ReferenceEquals(ExtensionAux, string.Empty) ? this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida : ExtensionAux));
                            FileName = nFileFolderName + '\\' + FileNameAux + ExtensionAux;
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
            }
            catch (Exception ex)
            {
                DMB.DesktopMessageShow("Exportar imagen", ref ex);
            }
        }

        private bool validar()
        {
            if (!validarRangoFechaProceso())
            {
                return false;
            }

            if (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 || Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0)
            {
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.CarpetaSalidaTextBox.Focus();
                return false;
            }
            else if ((!Directory.Exists(CarpetaSalidaTextBox.Text)))
            {
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.CarpetaSalidaTextBox.Focus();
                return false;

            }
            else
            {
                return true;
            }
        }

        private bool validarRangoFechaProceso()
        {
            DateTime FechaInicial = new DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day);
            DateTime FechaFinal = new DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day);

            if ((FechaInicial > FechaFinal))
            {
                MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;

            }
            double difFecha = (FechaFinal - FechaInicial).TotalDays;

            if (difFecha > 5)
            {
                MessageBox.Show("El rango de fechas no puede superar los 5 días", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;

            }

            return true;
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

        private void FormExportar_Load(object sender, EventArgs e)
        {
            Usa_Exportacion_PDF = this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF;
            formato = Utilities.GetEnumFormat(this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida);
            compresion = Utilities.GetEnumCompression((DesktopConfig.FormatoImagenEnum)this._Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida);

            Load_FormatoCargue();
        }
    }
}