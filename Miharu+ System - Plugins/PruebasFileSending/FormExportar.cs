using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBStorage;
using DBImaging.SchemaCore;
using DBImaging.SchemaProcess;
using DBCore;
using DBImaging;
using Slyg.Tools.Imaging;
using Miharu.Imaging.Library;

namespace Exportador_Acciones_Valores
{
    public partial class FormExportar : Form
    {
        #region Constructores

        public FormExportar()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void BuscarCarpetaButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog CarpetaSalida = new FolderBrowserDialog();

            CarpetaSalida.SelectedPath = CarpetaSalidaTextBox.Text;

            if (CarpetaSalida.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                CarpetaSalidaTextBox.Text = CarpetaSalida.SelectedPath;
        }

        private void ExportarButton_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Metodos

        private void Exportar()
        {
            //if (Validar())
            //{
            //    var dbmImaging = new DBImagingDataBaseManager(Program.CnnImaging);
            //    var dbmCore = new DBCoreDataBaseManager(Program.CnnCore);
            //    var TotalesDataTable = new CTA_Exportacion_TotalesDataTable();
            //    var ProgressForm = new Slyg.Tools.FormProgress();

            //    try
            //    {
            //        dbmImaging.Connection_Open(0);

            //        //TotalesDataTable = dbmImaging.Schemadbo.PA_TEMP_Exportacion_Totales_Acciones_Valores.DBExecute();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    finally
            //    {
            //        dbmImaging.Connection_Close();
            //    }

            //    if (TotalesDataTable.Rows.Count > 0)
            //    {
            //        DialogResult Respuesta;

            //        Respuesta = MessageBox.Show("Se encontró : " + "\n" +
            //                                    TotalesDataTable[0].Folders.ToString("#,##0") + " Unidades Documentales, " + "\n" +
            //                                    TotalesDataTable[0].Files.ToString("#,##0") + " Documentos, " + "\n" +
            //                                    TotalesDataTable[0].Folios.ToString("#,##0") + " Folios con " + "\n" +
            //                                    (TotalesDataTable[0].Tamaño / 1024 / 1024).ToString("#,##0.00") + "MB de tamaño, " + "\n" +
            //                                    "¿Desea Exportar esta información?", "Plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //        if (Respuesta == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            try
            //            {
            //                dbmImaging.Connection_Open(1);
            //                dbmCore.Connection_Open(1);

            //                var ExportacionDataSet = new Miharu.Imaging.OffLineViewer.Library.xsdOffLineData();
            //                //var FileDataTable = dbmImaging.Schemadbo.PA_TEMP_Exportacion_Files_Acciones_Valores.DBExecute();

            //                string OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd('\\') + "\\";

            //                //var FilesDataView = new DataView(FileDataTable);

            //                //int Progreso = 0; 

            //                //ProgressForm.Show();
            //                //ProgressForm.Process = "Exportar";
            //                //ProgressForm.ValueProcess = 0;
            //                //ProgressForm.MaxValueProcess = TotalesDataTable[0].Files;
            //                //ProgressForm.Action = "Obteniendo imágenes...";

            //                //System.Windows.Forms.Application.DoEvents();

            //                //foreach (var ItemFile in FileDataTable)
            //                //{
            //                //    if (ProgressForm.Cancel) throw new Exception("La acción fue cancelada por el usuario");

            //                //    // leer data
            //                //    var Campos = dbmCore.SchemaProcess.TBL_File_Data.DBGet(ItemFile.fk_Expediente, ItemFile.fk_Folder, ItemFile.fk_File, ItemFile.fk_Documento, null);

            //                //    if (Campos.Count > 1)
            //                //    {
            //                //        var Fecha = Campos[0].Valor_File_Data.ToString();
            //                //        var MTCN = Campos[1].Valor_File_Data.ToString();
            //                //        var FileName = OutputFolder + MTCN + "-" + Fecha + ".jpg";

            //                //        // Descargar el archivo
            //                //        var Resultado = DescargarImagenJPG(ItemFile);

            //                //        if (Resultado == "")
            //                //        {
            //                //            using (var Archivo = new StreamWriter(FileName + ".txt"))
            //                //            {
            //                //                Archivo.WriteLine("No se encontró imagen asociada al registro");
            //                //                Archivo.WriteLine("Expediente: " + ItemFile.fk_Expediente);
            //                //                Archivo.WriteLine("Folder: " + ItemFile.fk_Folder);
            //                //                Archivo.WriteLine("File: " + ItemFile.fk_File);
            //                //            }
            //                //        }
            //                //        else
            //                //        {
            //                //            int Contador = 1;

            //                //            while (File.Exists(FileName))
            //                //            {
            //                //                FileName = OutputFolder + MTCN + "-" + Fecha + "_" + Contador + ".jpg";
            //                //                Contador += 1;
            //                //            }

            //                //            File.Move(Resultado, FileName);
            //                //        }

            //                //        // Marcar como exportado
            //                //        dbmImaging.Schemadbo.TEMP_Acciones_Valores_Exportacion.DBUpdate(null, true, MTCN);
            //                //    }
            //                //    else
            //                //    {
            //                //        string FileName = OutputFolder + ItemFile.fk_Expediente + "-" + ItemFile.fk_Folder + "-" + ItemFile.fk_File + ".txt";

            //                //        using (var Archivo = new StreamWriter(FileName))
            //                //        {
            //                //            Archivo.WriteLine("No se encontró data asociada al registro");
            //                //            Archivo.WriteLine("Expediente: " + ItemFile.fk_Expediente);
            //                //            Archivo.WriteLine("Folder: " + ItemFile.fk_Folder);
            //                //            Archivo.WriteLine("File: " + ItemFile.fk_File);
            //                //        }
            //                //    }

            //                //    Progreso += 1;

            //                //    ProgressForm.IncrementProcess();

            //                //    ;
            //                //    System.Windows.Forms.Application.DoEvents();
            //                //}

            //                BorrarTemporal();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //                ProgressForm.Hide();
            //                Application.DoEvents();

            //                return;
            //            }
            //            finally
            //            {
            //                dbmImaging.Connection_Close();
            //                dbmCore.Connection_Close();

            //                ProgressForm.Close();
            //            }

            //            MessageBox.Show("La información se exportó con éxito", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Operación cancelada por Usuario", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se encontraron registros para exportar", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
        }


        private string DescargarImagenJPG(CTA_Exportacion_FilesRow RowFile)
        {
            //var DBMStorage = new DBStorageDataBaseManager(Program.CnnStorage);

            //this.Cursor = Cursors.WaitCursor;
            //this.Enabled = false;

            //string Resultado = "";

            //try
            //{
            //    DBMStorage.Connection_Open(0);

            //    var FileProvider = new FileProviderManager(false, Program.CnnStorage, Program.CnnImaging);
            //    var FoliosDataTable = DBMStorage.SchemaImaging.TBL_File_Folio.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, null);

            //    if (FoliosDataTable.Count > 0)
            //    {
            //        FileProvider.Connect();
            //        var FolioRow = FoliosDataTable[0];
            //        byte[] Imagen = null;
            //        byte[] Thumbnail = null;

            //        FileProvider.GetFolio(ref Imagen, ref Thumbnail, FolioRow.fk_Expediente, FolioRow.fk_Folder, FolioRow.fk_File, FolioRow.fk_Version, FolioRow.id_File_Record_Folio);

            //        string FileName = Program.AppPath + Program.TempPath + Guid.NewGuid().ToString() + ".jpg";

            //        ImageManager.Save(new Bitmap(new MemoryStream(Imagen)), FileName, "", ImageManager.EnumFormat.JPEG, ImageManager.EnumCompression.None, false, Program.AppPath + Program.TempPath);

            //        FileProvider.Disconnect();

            //        Resultado = FileName;
            //    }
            //    else
            //    {
            //        return "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    BorrarTemporal();
            //}
            //finally
            //{
            //    DBMStorage.Connection_Close();

            //    this.Cursor = Cursors.Default;
            //    this.Enabled = true;
            //}

            //return Resultado;
        }

        private void BorrarTemporal()
        {
            try
            {
                var objDirectoryInfo = new DirectoryInfo(Program.AppPath + Program.TempPath);
                var fileInfoArray = objDirectoryInfo.GetFiles();

                foreach (FileInfo objFileInfo in fileInfoArray)
                {
                    objFileInfo.Delete();
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (!Directory.Exists(CarpetaSalidaTextBox.Text))
            {
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CarpetaSalidaTextBox.Focus();
            }
            else if (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 || Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0)
            {
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CarpetaSalidaTextBox.Focus();
            }
            else
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}