using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using Miharu.FileSending.Library.Clases;
using Slyg.Tools.Zip;
using System.Configuration;
using System.Data;
using DBIntegration;
using Miharu.Uploader.Library.UploaderWebServiceReference;
using System.Web.Script.Serialization;
using Miharu.Desktop.Library.Config;

namespace Miharu.Uploader.Forms

{
    public partial class FormMain : Form
    {
        #region Estructuras

        public struct ImageFile
        {
            public string Name;
            public Bitmap Image;
        }

        #endregion

        #region Declaraciones

        private List<ImageFile> _Images = new List<ImageFile>();

        public const int MaxThumbnailWidth = 90;
        public const int MaxThumbnailHeight = 120;

        // private TwainManager Scanner;

        private FileSendingClient Transfer;
        private string Identificador_T;
        private string ZipFileName = "";
        private string LastPath = "";

        private string Contenedor = "";

        private delegate void TransferBeginDelegate(object sender, string Identificador_T);

        private delegate void TransferProcessDelegate(object sender, string Identificador_T, float Avance);

        private delegate void TransferCompletedDelegate(object sender, string Identificador_T);

        private delegate void TransferErrorDelegate(object sender, string Identificador_T, string Mensaje);
        
        private List<string> FileNamesSource = new List<string>();

        #endregion

        #region Constructores

        public FormMain()
        {
            InitializeComponent();
            NewProcess();

            //ToolStripStatusLabel.Text = "Fecha: [" + DateTime.Now.ToString("dd/MMM/yyyy") + "] - Oficina: [" + Program.Config.OfficeName + "] - Usuario: [" + Program.UserName + "]";

            ToolStripStatusLabel.Text = "Fecha: [" + DateTime.Now.ToString("dd/MMM/yyyy") + "] - Oficina: [" + Program.Config.OfficeName + "] - Usuario: [" +
                                            Program.MiharuSession.Usuario.Apellidos + ", " +
                                            Program.MiharuSession.Usuario.Nombres + "]";

            ProgresoToolStripProgressBar.Minimum = 0;
            ProgresoToolStripProgressBar.Maximum = 100;
            ProgresoToolStripProgressBar.Value = 0;
            ContadorToolStripStatusLabel.Text = "0%";
        }

        #endregion

        #region Eventos

        private void FormMain_Load(object sender, EventArgs e)
        {
            //var SeleccionarProyectoEsquemaForm = new FormSeleccionarEsquema();
            //if (SeleccionarProyectoEsquemaForm.ShowDialog() == DialogResult.OK)
            //{
            //    Program.Proyecto = SeleccionarProyectoEsquemaForm.IdProyecto;
            //    Program.Esquema = SeleccionarProyectoEsquemaForm.IdEsquema;
            //}

            
                      
        }

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configurar();
        }

        private void CerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAboutBox().ShowDialog();
        }

        private void CancelarToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void ImagesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateOptions();
        }

        private void TransmitirToolStripButton_Click(object sender, EventArgs e)
        {
            if (Program.IsImage == true)
            {
                Transmitir();
            }
            else 
            {
               CargarCampos();
            }
            
        }

        private void InsertFolderToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Insertar folder";
            InsertFolder();
            this.Cursor = Cursors.Default;
        }

        private void IconosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IconosToolStripMenuItem.Checked = true;
            ListaToolStripMenuItem.Checked = false;

            ImagesListView.View = View.LargeIcon;

            if (ImagesListView.SelectedIndices.Count > 0)
            {
                int Index = ImagesListView.SelectedIndices[0];
                DisplayImages(_Images);
                ImagesListView.SelectedIndices.Add(Index);
            }
            else
            {
                DisplayImages(_Images);
            }
        }

        private void ListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IconosToolStripMenuItem.Checked = false;
            ListaToolStripMenuItem.Checked = true;

            ImagesListView.View = View.List;
            ImagesListView.Refresh();
        }

        private void Transfer_TransferBegin(object sender, string nIdentificador)
        {
            if (this.InvokeRequired)
            {
                TransferBeginDelegate MyDelegate = Transfer_TransferBegin;
                this.Invoke(MyDelegate, new[] {sender, nIdentificador});
            }
            else
            {
                AccionToolStripStatusLabel.ForeColor = Color.Green;
                AccionToolStripStatusLabel.Text = "Transmitiendo";

                ProgresoToolStripProgressBar.Value = 0;
                ContadorToolStripStatusLabel.Text = "0%";
            }
        }

        private void Transfer_TransferProcess(object sender, string nIdentificador, float Avance)
        {
            if (this.InvokeRequired)
            {
                TransferProcessDelegate MyDelegate = Transfer_TransferProcess;
                this.Invoke(MyDelegate, new[] {sender, nIdentificador, Avance});
            }
            else
            {
                ProgresoToolStripProgressBar.Value = (int) Avance;
                ContadorToolStripStatusLabel.Text = (int) Avance + "%";
            }
        }

        private void Transfer_TransferCompleted(object sender, string nIdentificador)
        {
            if (this.InvokeRequired)
            {
                TransferCompletedDelegate MyDelegate = Transfer_TransferCompleted;
                this.Invoke(MyDelegate, new[] {sender, nIdentificador});
            }
            else
            {
                Cargar(nIdentificador);
            }
        }

        private void Transfer_TransferError(object sender, string nIdentificador, string Mensaje)
        {
            if (this.InvokeRequired)
            {
                TransferErrorDelegate MyDelegate = Transfer_TransferError;
                this.Invoke(MyDelegate, new[] {sender, nIdentificador, Mensaje});
            }
            else
            {
                AccionToolStripStatusLabel.ForeColor = Color.Red;

                //FormMenuStrip.Enabled = true;
                //MenuToolStrip.Enabled = true;
                //CancelarToolStripStatusLabel.Visible = false;
                //ContadorToolStripStatusLabel.Visible = false;
                //Transfer.Detener();
                //Transfer = null;

                AccionToolStripStatusLabel.Text = "Intentando reanudar la transmisón...";
            }
        }

        private void ReportePlanillasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mostrar_Reporte();
        }

        private void ReportePlanillatoolStripButton_Click(object sender, EventArgs e)
        {
            //Mostrar_Reporte();
        }

        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Eliminar imagen";
            Delete();
        }

        private void NextToolStripButton_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void PreviousToolStripButton_Click(object sender, EventArgs e)
        {
            Previous();
        }

        private void ConfigToolStripButton_Click(object sender, EventArgs e)
        {
            //var ConfigForm = new FormConfig();
            //ConfigForm.Show();
            var Proyecto = new FormSeleccionarEsquema();
            Proyecto.Show();
        }

        private void NewProcessToolStripButton_Click(object sender, EventArgs e)
        {
            //Seleccionar proyecto
            var SeleccionarProyectoEsquemaForm = new FormSeleccionarEsquema();
            if (SeleccionarProyectoEsquemaForm.ShowDialog() == DialogResult.OK)
            {
                Program.Proyecto = SeleccionarProyectoEsquemaForm.IdProyecto;
                Program.Esquema = SeleccionarProyectoEsquemaForm.IdEsquema;
                Program.EntidadCliente = Program.MiharuSession.Entidad.id;

                this.ToolStripStatusLabel.Text = SeleccionarProyectoEsquemaForm.NombreProyecto;

                //Seleccionar proceso
                var newProcessForm = new FormNewProcess();

                if (newProcessForm.ShowDialog() != DialogResult.OK) return;
                if (newProcessForm.IsImaging)
                {
                    ImagesListView.Clear();
                    FileNamesSource.Clear();
                    _Images.Clear();
                    InsertFolderToolStripButton.Visible = true;
                    NextToolStripButton.Visible = true;
                    PreviousToolStripButton.Visible = true;
                    DeleteToolStripButton.Visible = true;
                    ImagesListView.Visible = true;

                    DataDeletionStripButton.Visible = false;
                    InsertLogFileStripButton.Visible = false;
                    loadedDataDataGridView.Visible = false;

                    InsertFolder();
                }
                else if (newProcessForm.IsData)
                {
                    InsertFolderToolStripButton.Visible = false;
                    NextToolStripButton.Visible = false;
                    PreviousToolStripButton.Visible = false;
                    DeleteToolStripButton.Visible = false;
                    ImagesListView.Visible = false;

                    DataDeletionStripButton.Visible = true;
                    InsertLogFileStripButton.Visible = true;
                    loadedDataDataGridView.Visible = true;
                }
                else
                {
                    InsertFolderToolStripButton.Visible = false;
                    NextToolStripButton.Visible = false;
                    PreviousToolStripButton.Visible = false;
                    DeleteToolStripButton.Visible = false;
                    ImagesListView.Visible = false;

                    DataDeletionStripButton.Visible = false;
                    InsertLogFileStripButton.Visible = false;
                    loadedDataDataGridView.Visible = false;
                }

            }
        }

        private void InsertFileToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Insertar file";
            InsertFile();
            this.Cursor = Cursors.Default;
        }

        private void InsertLogFileStripButton_Click(object sender, EventArgs e)
        {
            //var SeleccionarEsquemaForm = new FormSeleccionarEsquema();
            //SeleccionarEsquemaForm.IdEntidad = Program.MiharuSession.Entidad.id;
            //SeleccionarEsquemaForm.IdUsuario = Program.MiharuSession.Usuario.id;
            
            //if (SeleccionarEsquemaForm.ShowDialog() != DialogResult.OK) return;
            //Program.Esquema = SeleccionarEsquemaForm.IdEsquema;
            //Program.Proyecto = SeleccionarEsquemaForm.IdProyecto;
            //Program.EntidadCliente = Program.MiharuSession.Entidad.id;
            CargarReportes(true);
        }
        
        #endregion

        #region Metodos

        private void Configurar()
        {
            var ConfigForm = new FormConfig();
            ConfigForm.TempPath = Program.Config.WorkingFolder;
            //{TempPath = Program.UploaderConfigFile.WorkingFolder};
            ConfigForm.OfficeID = Program.Config.OfficeID;
            ConfigForm.OfficeName = ConfigForm.OfficeName;

            DialogResult Respuesta = ConfigForm.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                Program.UploaderConfigFile.WorkingFolder = ConfigForm.TempPath;
                Program.Config.OfficeID = ConfigForm.OfficeID;
                Program.Config.OfficeName = ConfigForm.OfficeName;

                Program.SaveConfig();

                ToolStripStatusLabel.Text = "Fecha: [" + DateTime.Now.ToString("dd/MMM/yyyy") + "] - Oficina: [" + Program.Config.OfficeName + "] - Usuario: [" +
                                            Program.MiharuSession.Usuario.Apellidos + ", " +
                                            Program.MiharuSession.Usuario.Nombres + "]";
            }
        }
        
        private void Cancelar()
        {
            CancelarToolStripStatusLabel.Visible = false;
            ContadorToolStripStatusLabel.Visible = false;

            Transfer.Cancelar(Identificador_T);

            NewProcess();

            // Transmitir archivo         
            FormMenuStrip.Enabled = true;
            MenuToolStrip.Enabled = true;

            AccionToolStripStatusLabel.ForeColor = Color.Red;
            AccionToolStripStatusLabel.Text = "Transferencia cancelada";
        }

        private void DisplayImages(IEnumerable<ImageFile> ImageFiles)
        {
            ImagesListView.Items.Clear();
            ImagesListView.LargeImageList = new ImageList
            {
                ImageSize = new Size(MaxThumbnailWidth, MaxThumbnailHeight),
                ColorDepth = ColorDepth.Depth32Bit
            };

            int i = 0;
            foreach (ImageFile NewImageFile in ImageFiles)
            {
                ImagesListView.LargeImageList.Images.Add("K" + i, NewImageFile.Image);
                ImagesListView.Items.Add("Folio " + (i + 1), "K" + i);

                i += 1;
            }

            ImagesListView.Refresh();
        }

        private void ActivateOptions()
        {
            if (ImagesListView.SelectedIndices.Count > 0)
            {
                int Index = ImagesListView.SelectedIndices[0];

                DeleteToolStripButton.Enabled = true;
                NextToolStripButton.Enabled = (Index < (_Images.Count - 1));
                PreviousToolStripButton.Enabled = (Index > 0);
            }
            else
            {
                DeleteToolStripButton.Enabled = false;
                NextToolStripButton.Enabled = false;
                PreviousToolStripButton.Enabled = false;
            }

        }

        private void NewProcess()
        {
            CrearDirectorios();
            _Images.Clear();
            DisplayImages(_Images);
            ActivateOptions();
            
            TransmitirToolStripButton.Enabled = false;

            // Borrar temporal            
            string[] TempFiles = Directory.GetFiles(Program.TempDirectory);

            foreach (string TempFile in TempFiles)
            {
                try
                {
                    File.Delete(TempFile);
                }
                catch { }
            }

           
        }

        private void CrearDirectorios()
        {
            try
            {
                if (Directory.Exists(Program.TempDirectory))
                    Directory.Delete(Program.TempDirectory, true);

                Directory.CreateDirectory(Program.TempDirectory);
            }
            catch { }

            try
            {
                if (Directory.Exists(Program.SourceDirectory))
                    Directory.Delete(Program.SourceDirectory, true);

                Directory.CreateDirectory(Program.SourceDirectory);
            }
            catch { }
        }

        private void Transmitir()
        {
            var Respuesta =
                MessageBox.Show(
                    "El sistema se encuentra listo para transmitir " + _Images.Count +
                    " imágenes, ¿desea iniciar el proceso de transmisión de imágenes?", Program.AssemblyTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Respuesta == DialogResult.Yes)
            {
                // Transmitir archivo         
                FormMenuStrip.Enabled = false;
                MenuToolStrip.Enabled = false;

                var FileNames = new string[_Images.Count];

                for (int i = 0; i < _Images.Count; i++)
                {
                    FileNames[i] = _Images[i].Name;
                }

                //Renombrar Imagenes con consecutivo
                FileNames = RenombrarImagenes(FileNames);

                AccionToolStripStatusLabel.ForeColor = Color.Black;
                AccionToolStripStatusLabel.Text = "Comprimir";
                Application.DoEvents();

                // Convertir a ZIP
                ZipFileName = Program.TempDirectory + DateTime.Now.ToString("yyyMMdd-hhmmss") + ".zip";
                ZipUtil.Comprimir(FileNames, ZipFileName, false);

                AccionToolStripStatusLabel.ForeColor = Color.Green;
                AccionToolStripStatusLabel.Text = "Iniciando transmisión";
                Application.DoEvents();

                Transfer = new FileSendingClient(Program.FileServerIP, Program.FileServerPort, Program.FileServerAppName,Program.SourceDirectory, Program.PackageSize);
                Transfer.TransferBegin += Transfer_TransferBegin;
                Transfer.TransferProcess += Transfer_TransferProcess;
                Transfer.TransferCompleted += Transfer_TransferCompleted;
                Transfer.TransferError += Transfer_TransferError;

                CancelarToolStripStatusLabel.Visible = true;
                ContadorToolStripStatusLabel.Visible = true;

                Identificador_T = Transfer.Transmitir(ZipFileName, Program.MiharuSession.Usuario.id);
             }
        }

        private string[] RenombrarImagenes(string[] FileNamesGuid)
        {
            var FileNamesNew = new string[FileNamesGuid.Length];
            try
            {
                for (int j = 0; j < FileNamesGuid.Length; j++)
                {
                    string newName = j.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0') + Path.GetExtension(FileNamesGuid[j]);
                    FileNamesNew[j] = Path.GetDirectoryName(FileNamesGuid[j]) + '\\' + newName;

                    File.Move(FileNamesGuid[j], FileNamesNew[j]);
                }
            }
            catch (Exception ex)
            {
                AccionToolStripStatusLabel.Text = "Error: " + ex.Message;
                MessageBox.Show(AccionToolStripStatusLabel.Text, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return FileNamesNew;
        }

        private void Cargar(string nIdentificador)
        {
            try
            {
                CancelarToolStripStatusLabel.Visible = false;
                ContadorToolStripStatusLabel.Visible = false;

                AccionToolStripStatusLabel.Text = "Descomprimiendo archivos";
                Application.DoEvents();

                // Cargar
                string CarpetaZip = Path.GetFileNameWithoutExtension(Path.GetFileName(ZipFileName));
                Transfer.Unzip(Path.GetFileName(ZipFileName));

                AccionToolStripStatusLabel.Text = "Almacenando datos";
                Application.DoEvents();

                var Cargue = 0;
                short Folios = 0;
                var PaqueteName = "0000" + DateTime.Now.ToString("yyyyMMdd");
                var Observaciones = "";

                var Definicion = Transfer.getDefinition(nIdentificador);
                
                Transfer.Cargar(Program.MiharuSession.Entidad.id, Program.Proyecto, Program.Esquema, CarpetaZip, PaqueteName, Observaciones, Program.EntidadProcesamiento, Program.SedeProcesamiento, Program.CentroProcesamiento, Definicion.id_Log, Program.MiharuSession.Usuario.id, 31,Contenedor, ref Cargue, ref Folios);

                AccionToolStripStatusLabel.ForeColor = Color.Green;

                ProgresoToolStripProgressBar.Value = 100;
                ContadorToolStripStatusLabel.Text = "100%";

                // Borrar la información temporal
                CrearDirectorios();

                NewProcess();

                Transfer.Detener();
                Transfer = null;

               
                //Se elimina las imagenes trasnmitidas.
                EliminarImagenesTransmitidas();

                AccionToolStripStatusLabel.Text = "Proceso finalizado para la Fecha de Proceso: [" + DateTime.Now.ToShortDateString() + "], Cargue [" + Cargue + "] - Imagenes [" + Folios + "]";

                MessageBox.Show(AccionToolStripStatusLabel.Text, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                AccionToolStripStatusLabel.Text = "";
            }
            catch (Exception ex)
            {
                NewProcess();
                AccionToolStripStatusLabel.Text = "Error: " + ex.Message;
                MessageBox.Show(AccionToolStripStatusLabel.Text, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FormMenuStrip.Enabled = true;
                MenuToolStrip.Enabled = true;
            }
        }

        private void EliminarImagenesTransmitidas()
        {
            try
            {
                foreach (string fileName in FileNamesSource)
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                AccionToolStripStatusLabel.Text = "Error: " + ex.Message;
                MessageBox.Show(AccionToolStripStatusLabel.Text, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FormMenuStrip.Enabled = true;
                MenuToolStrip.Enabled = true;
            }
        }
        
        private void InsertFolder()
        {
            this.Cursor = Cursors.WaitCursor;
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Insertar folder";
            
            var CarpetaFolderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Seleccione la carpeta de las imágenes",
                ShowNewFolderButton = false,
                SelectedPath = LastPath
            };

            DialogResult Respuesta = CarpetaFolderBrowserDialog.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                int Index = _Images.Count;

                List<ImageFile> NewImageFiles = LoadImageFiles(CarpetaFolderBrowserDialog.SelectedPath);

                LastPath = CarpetaFolderBrowserDialog.SelectedPath;

                Contenedor = LastPath.Substring(LastPath.LastIndexOf('\\') + 1);

                if (NewImageFiles.Count == 0)
                {
                    TransmitirToolStripButton.Enabled = false;
                    VistaToolStripDropDownButton.Enabled = false;
                    MessageBox.Show("No se encontraron imágenes para cargar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Respuesta = MessageBox.Show("Se encontraron " + NewImageFiles.Count + " imágenes, ¿desea adjuntarlas a la imagen actual?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Respuesta == DialogResult.Yes)
                    {
                        foreach (ImageFile NewImageFile in NewImageFiles)
                        {
                            _Images.Add(NewImageFile);
                        }

                        DisplayImages(_Images);
                        ActivateOptions();

                        if (NewImageFiles.Count == 0)
                        {
                            TransmitirToolStripButton.Enabled = false;
                            VistaToolStripDropDownButton.Enabled = false;
                            MessageBox.Show("No se encontraron imágenes para cargar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                   }
                        else
                        {
                            ImagesListView.SelectedIndices.Add(Index);
                            TransmitirToolStripButton.Enabled = true;
                            VistaToolStripDropDownButton.Enabled = true;
                        }
                    }
                    else
                    {
                        CrearDirectorios();
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void InsertFile()
        {
            var InsertarOpenFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Seleccionar imagen",
                Filter =
                    "Archivos de imagen |*.gif;*.jpg;*.png;*.bmp|Imagenes GIF (*.gif)|*.gif|Imagenes BMP (*.bmp)|*.bmp|Imagenes JPG (*.jpg)|*.jpg|Imagenes PNG (*.png)|*.png|Imagenes TIFF (*.tif)|*.tif"
            };

            var Respuesta = InsertarOpenFileDialog.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                //Se agrega el path de la imagem fuente.
                FileNamesSource.Add(InsertarOpenFileDialog.FileName);

                var Imagen = new Bitmap(InsertarOpenFileDialog.FileName);
                short Folios = Imaging.ImageManager.GetFolios(Imagen);

                for (int i = 1; i <= Folios; i++)
                {
                    var ImagenFolio = Imaging.ImageManager.GetFolio(Imagen, i);

                    var NewImageFile = new ImageFile
                    {
                        Name = Program.TempDirectory + Guid.NewGuid().ToString() + ".jpg",
                        Image = getThumbnail(ImagenFolio)
                    };
                    _Images.Add(NewImageFile);

                    ImagenFolio.Save(NewImageFile.Name, ImageFormat.Jpeg);
                    ImagenFolio.Dispose();
                }

                Imagen.Dispose();

                DisplayImages(_Images);
                ImagesListView.SelectedIndices.Add(_Images.Count - 1);

                ActivateOptions();
            }
        }

        //private void LoadScannedImage(ArrayList nImages)
        //{
        //    int Index = _Images.Count;

        //    foreach (Bitmap ImageFile in nImages)
        //    {
        //        ImageFile NewImageFile = new ImageFile();
        //        NewImageFile.Name = Program.TempDirectory + Guid.NewGuid().ToString() + ".jpg";
        //        NewImageFile.Image = getThumbnail(ImageFile);
        //        _Images.Add(NewImageFile);

        //        ImageFile.Save(NewImageFile.Name, ImageFormat.Jpeg);
        //    }

        //    DisplayImages(_Images);
        //    ActivateOptions();

        //    if (nImages.Count == 0)
        //        MessageBox.Show("No se cargaron imagenes", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    else
        //        ImagesListView.SelectedIndices.Add(Index);
        //}

        private void Delete()
        {
            if (ImagesListView.SelectedIndices.Count > 0)
            {
                int Index = ImagesListView.SelectedIndices[0];

                DialogResult Respuesta = MessageBox.Show("¿Desea remover la imagen seleccionada?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Respuesta == DialogResult.Yes)
                {
                    _Images.RemoveAt(Index);

                    DisplayImages(_Images);
                }
            }

            ActivateOptions();
        }

        private void Next()
        {
            if (ImagesListView.SelectedIndices.Count > 0)
            {
                int Index = ImagesListView.SelectedIndices[0];

                ImageFile Item = _Images[Index];

                _Images.Remove(Item);
                _Images.Insert(Index + 1, Item);

                DisplayImages(_Images);
                ImagesListView.SelectedIndices.Add(Index + 1);
            }

            ActivateOptions();
        }

        private void Previous()
        {
            if (ImagesListView.SelectedIndices.Count > 0)
            {
                int Index = ImagesListView.SelectedIndices[0];

                ImageFile Item = _Images[Index];

                _Images.Remove(Item);
                _Images.Insert(Index - 1, Item);

                DisplayImages(_Images);
                ImagesListView.SelectedIndices.Add(Index - 1);
            }

            ActivateOptions();
        }

        //private void EndingScan()
        //{
        //    if (msgfilter)
        //    {
        //        Application.RemoveMessageFilter(this);
        //        msgfilter = false;

        //        FormMenuStrip.Enabled = true;
        //        MenuToolStrip.Enabled = true;

        //        this.Activate();
        //    }
        //}

        private void LoadImageFilesByExtension(string ImagePath, ref List<ImageFile> NewImageFiles, string Extension)
        {
            var FileNames = Directory.GetFiles(ImagePath, "*" + Extension);

            foreach (var FileName in FileNames)
            {
                //Se agrega el path de la imagen fuente
                FileNamesSource.Add(FileName);

                var Imagen = new Bitmap(FileName);
                var Folios = Imaging.ImageManager.GetFolios(Imagen);

                for (int i = 1; i <= Folios; i++)
                {
                    var ImagenFolio = Imaging.ImageManager.GetFolio(Imagen, i);

                    var NewImageFile = new ImageFile
                    {
                        Name = Program.TempDirectory + Guid.NewGuid().ToString() + ".jpg",
                        Image = getThumbnail(ImagenFolio)
                    };
                    NewImageFiles.Add(NewImageFile);

                    ImagenFolio.Save(NewImageFile.Name, ImageFormat.Jpeg);
                    ImagenFolio.Dispose();
                }

                Imagen.Dispose();
            }
        }

        private void CargarReportes(bool nusa_Archivo)
        {
            if (nusa_Archivo)
            {
                var CargueForm = new FormSeleccionarCargue
                {
                    idEntidad = Program.EntidadCliente,
                    idProyecto = Program.Proyecto
                };

                if (CargueForm.ShowDialog() == DialogResult.OK)
                {
                    this.loadedDataDataGridView.DataSource = CargarGrid(CargueForm).Tables[0].DefaultView;
                    Program.Reporte = CargueForm.IdReporte;
                }
            }
            else
            {

            }

           
            
        }

        private static DataSet CargarGrid(FormSeleccionarCargue Cargue)
        {
            DataSet ds = new DataSet();
            StreamReader sr = new StreamReader(Cargue.Path);
            Program.PathBytes = File.ReadAllBytes(Cargue.Path);
            ds.Tables.Add();
            CargarColumnasGrid(ds);

            string Datos = sr.ReadToEnd();
            if (Datos.Length <= 0)
            {
                MessageBox.Show("El archivo cargado no contiene información", "Carga Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ds;
            }
            string[] rows = Datos.Split("\n".ToCharArray());

            try
            {
                foreach (string r in rows)
                {
                    string[] items = r.Split(Cargue.Separador.ToCharArray());
                    ds.Tables[0].Rows.Add(items);
                }
            }
            catch (Exception ex)
            {
                ds.Clear();
                MessageBox.Show("El archivo cargado no cumple con el formato seleccionado.", "Carga Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                        
            return ds;
       }

      private static void CargarColumnasGrid(DataSet Tabla)
        {
            //var WebService = new UploaderWebService { Url = Program.UploaderWebServiceURL };
            var WebService = new Miharu.Uploader.Library.WebService.UploaderService();

            WebService.IdEntidad = Program.EntidadCliente;
            WebService.IdProyecto = Program.Proyecto;
            WebService.IdEsquema = Program.Esquema;
            WebService.IdUsuario = Program.MiharuSession.Usuario.id;

            var CipherType = short.Parse(Config.UploaderConfig.Decrypt(WebService.getCifradoTipo().Cifrado, Config.UploaderConfig.EnumCipherType.TDES));

            var proceso = WebService.getCampos();
            var serializer = new JavaScriptSerializer();
            foreach (var campos in proceso.Campos)
            {
                var Datos = serializer.Deserialize<DBIntegration.SchemaConfig.TBL_CamposSimpleType>(Config.UploaderConfig.Decrypt(campos.ToString(CultureInfo.InvariantCulture), (Config.UploaderConfig.EnumCipherType)CipherType));
                Tabla.Tables[0].Columns.Add(Datos.nombre_Campo.ToString());
            }
        }

        private void CargarCampos()
        {
          
            var WebService = new Miharu.Uploader.Library.WebService.UploaderService();
            WebService.IdEntidad = Program.EntidadCliente;

            var CipherType = short.Parse(Config.UploaderConfig.Decrypt(WebService.getCifradoTipo().Cifrado, Config.UploaderConfig.EnumCipherType.TDES));

            var serializer = new JavaScriptSerializer();
            
            DataView Vista = (DataView)loadedDataDataGridView.DataSource;
            DataTable TablaDatos = Vista.ToTable();

            string Resultado;
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    var XmlSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(DataTable));
                    XmlSerialiser.Serialize(sw, TablaDatos);
                    Resultado = Encoding.UTF8.GetString(ms.ToArray()); 
                }
            }

            var Datos = Config.UploaderConfig.Encrypt(serializer.Serialize(Resultado),(Config.UploaderConfig.EnumCipherType)CipherType);

            WebService.IdUsuario = Program.MiharuSession.Usuario.id;
            WebService.IdEntidad = Program.EntidadCliente;
            WebService.IdProyecto = Program.Proyecto;
            WebService.IdEsquema = Program.Esquema;
            WebService.IdReporte = Program.Reporte;
            WebService.DataCargue = Datos;
            var proceso = WebService.CargueData();

                //(Program.MiharuSession.Usuario.id, Program.EntidadCliente,Program.Proyecto, Program.Esquema, Program.Reporte, Datos);

            if (proceso.Result == true)
            {
                loadedDataDataGridView.DataSource = null;
                loadedDataDataGridView.Refresh();
                MessageBox.Show("El archivo fue cargado exitosamente.","Transmisión de archivos",MessageBoxButtons.OK);
            }
        }

        #endregion

        #region Funciones

        private List<ImageFile> LoadImageFiles(string ImagePath)
        {
            var NewImageFiles = new List<ImageFile>();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                LoadImageFilesByExtension(ImagePath, ref NewImageFiles, ".gif");
                LoadImageFilesByExtension(ImagePath, ref NewImageFiles, ".bmp");
                LoadImageFilesByExtension(ImagePath, ref NewImageFiles, ".jpg");
                LoadImageFilesByExtension(ImagePath, ref NewImageFiles, ".png");
                LoadImageFilesByExtension(ImagePath, ref NewImageFiles, ".tif");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;

            return NewImageFiles;
        }

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            //try
            //{
            //    TwainCommand cmd = Scanner.PasarMensaje(ref m);
            //    if (cmd == TwainCommand.Not)
            //        return false;

            //    switch (cmd)
            //    {
            //        case TwainCommand.CloseRequest:
            //            EndingScan();
            //            Scanner.CerrarFuente();
            //            break;

            //        case TwainCommand.CloseOk:
            //            EndingScan();
            //            Scanner.CerrarFuente();
            //            break;

            //        case TwainCommand.DeviceEvent:
            //            break;

            //        case TwainCommand.TransferReady:
            //            ArrayList bitmaps = Scanner.TransferirBitmaps();
            //            EndingScan();

            //            Scanner.CerrarFuente();

            //            LoadScannedImage(bitmaps);

            //            break;
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    EndingScan();

            //    return false;
            //}
            return false;
        }

        private Bitmap getThumbnail(Bitmap nImage)
        {
            var ThumbnailWidth = nImage.Width > MaxThumbnailWidth ? MaxThumbnailWidth : nImage.Width;
            var ThumbnailHeight = nImage.Height > MaxThumbnailHeight ? MaxThumbnailHeight : nImage.Height;

            var ImageThumbnail = Imaging.ImageManager.GetThumbnail(nImage, ThumbnailWidth, ThumbnailHeight);
            var FormatedThumbnail = new Bitmap(MaxThumbnailWidth, MaxThumbnailHeight);

            var g = Graphics.FromImage(FormatedThumbnail);
            g.FillRectangle(new SolidBrush(Color.Silver), new Rectangle(0, 0, MaxThumbnailWidth, MaxThumbnailHeight));
            g.DrawImage(ImageThumbnail, 0, 0);
            g.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(0, 0, ImageThumbnail.Width, ImageThumbnail.Height));

            ImageThumbnail.Dispose();

            return FormatedThumbnail;
        }

        #endregion        

        private void DataDeletionStripButton_Click(object sender, EventArgs e)
        {

        }

        private void ReportesStripButton_Click(object sender, EventArgs e)
        {
            CargarReportes(false);
        }

     

    
            
    }
}