using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using Miharu.FileSending.Library.Clases;
using Slyg.Tools.Zip;
using Banagrario.Imaging;

namespace Banagrario.Scanning.Forms
{
    public partial class FormMain : Form, IMessageFilter
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
        private bool msgfilter;

        private FileSendingClient Transfer;
        private string Identificador;
        private string ZipFileName = "";
        private string LastPath = "";

        private delegate void TransferBeginDelegate(object sender, string Identificador);
        private delegate void TransferProcessDelegate(object sender, string Identificador, float Avance);
        private delegate void TransferCompletedDelegate(object sender, string Identificador);
        private delegate void TransferErrorDelegate(object sender, string Identificador, string Mensaje);

        private DateTime _fechaMovimiento;
        private short _MovimientoTipo;
        private string _TipoMovimiento;

        private List<string> FileNamesSource = new List<string>();

        #endregion

        #region Constructores

        public FormMain()
        {
            InitializeComponent();

            //Scanner = new TwainManager();
            //Scanner.Init(this.Handle);
            NewProcess();
            
            ToolStripStatusLabel.Text = "Fecha: [" + DateTime.Now.ToString("dd/MMM/yyyy") + "] - Oficina: [" + Program.Config.OfficeName + "] - Usuario: [" + Program.UserName + "]";

            ProgresoToolStripProgressBar.Minimum = 0;
            ProgresoToolStripProgressBar.Maximum = 100;
            ProgresoToolStripProgressBar.Value = 0;
            ContadorToolStripStatusLabel.Text = "0%";
            _MovimientoTipo = 0;
            _TipoMovimiento = string.Empty;
        }

        #endregion

        #region Eventos

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            ActivateOptions();
            CrearDirectorios();
        }

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configurar();
        }

        private void SeleccionarOrigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Scanner.Seleccionar();
        }

        private void CerrarToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        private void ImagesListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ActivateOptions();
        }

        private void NewProcessToolStripButton_Click(object sender, System.EventArgs e)
        {
            DialogResult Respuesta = MessageBox.Show("¿Desea descartar las imágenes cargadas?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             if (Respuesta == System.Windows.Forms.DialogResult.Yes)
             {
                 AccionToolStripStatusLabel.ForeColor = Color.Black;
                 AccionToolStripStatusLabel.Text = "Nuevo proceso";
                 NewProcess();
             }
        }

        private void TransmitirToolStripButton_Click(object sender, System.EventArgs e)
        {            
            Transmitir();
        }

        private void ReprocesosToolStripButton_Click(object sender, EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Reprocesos";
            Reprocesos();
        }

        private void InsertFolderToolStripButton_Click(object sender, System.EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Insertar folder";
            InsertFolder();
        }

        private void InsertFileToolStripButton_Click(object sender, System.EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Insertar imagen";
            InsertFile();
        }

        private void DeleteToolStripButton_Click(object sender, System.EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Eliminar imagen";
            Delete();
        }

        private void NextToolStripButton_Click(object sender, System.EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Mover a adelante";
            Next();
        }

        private void PreviousToolStripButton_Click(object sender, System.EventArgs e)
        {
            AccionToolStripStatusLabel.ForeColor = Color.Black;
            AccionToolStripStatusLabel.Text = "Move a atrás";
            Previous();
        }

        private void AdquirirToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                AccionToolStripStatusLabel.ForeColor = Color.Black;
                AccionToolStripStatusLabel.Text = "Escanear";

                if (!msgfilter)
                {
                    FormMenuStrip.Enabled = false;
                    MenuToolStrip.Enabled = false;

                    msgfilter = true;

                    Application.AddMessageFilter(this);
                }
                //Scanner.Adquirir();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                FormMenuStrip.Enabled = true;
                MenuToolStrip.Enabled = true;
                msgfilter = false;

                this.Activate();
            }            
        }

        private void IconosToolStripMenuItem_Click(object sender, System.EventArgs e)
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

        private void ListaToolStripMenuItem_Click(object sender, System.EventArgs e)
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
                this.Invoke(MyDelegate, new[] { sender, nIdentificador });
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
                this.Invoke(MyDelegate, new[] { sender, nIdentificador, Avance });
            }
            else
            {
                ProgresoToolStripProgressBar.Value = (int)Avance;
                ContadorToolStripStatusLabel.Text = (int)Avance + "%";
            }
        }

        private void Transfer_TransferCompleted(object sender, string nIdentificador)
        {
            if (this.InvokeRequired)
            {
                TransferCompletedDelegate MyDelegate = Transfer_TransferCompleted;
                this.Invoke(MyDelegate, new[] { sender, nIdentificador });
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
                this.Invoke(MyDelegate, new[] { sender, nIdentificador, Mensaje });
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

        #endregion

        #region Metodos

        private void Configurar()
        {
            var ConfigForm = new Forms.FormConfig();

            ConfigForm.TempPath = Program.Config.WorkingFolder;
            ConfigForm.OfficeID = Program.Config.OfficeID;
            ConfigForm.OfficeName = ConfigForm.OfficeName;

            DialogResult Respuesta = ConfigForm.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                Program.Config.WorkingFolder = ConfigForm.TempPath;
                Program.Config.OfficeID = ConfigForm.OfficeID;
                Program.Config.OfficeName = ConfigForm.OfficeName;

                Program.SaveConfig();

                ToolStripStatusLabel.Text = "Fecha: [" + DateTime.Now.ToString("dd/MMM/yyyy") + "] - Oficina: [" + Program.Config.OfficeName + "] - Usuario: [" + Program.UserName + "]";
            }
        }

        private void Cancelar()
        {
            CancelarToolStripStatusLabel.Visible = false;
            ContadorToolStripStatusLabel.Visible = false;

            Transfer.Cancelar(Identificador);

            // Transmitir archivo         
            FormMenuStrip.Enabled = true;
            MenuToolStrip.Enabled = true;

            AccionToolStripStatusLabel.ForeColor = Color.Red;
            AccionToolStripStatusLabel.Text = "Transferencia cancelada";
        }

        private void DisplayImages(IEnumerable<ImageFile> ImageFiles)
        {
            ImagesListView.Items.Clear();
            ImagesListView.LargeImageList = new ImageList();
            ImagesListView.LargeImageList.ImageSize = new Size(MaxThumbnailWidth, MaxThumbnailHeight);
            ImagesListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            int i = 0;
            foreach (ImageFile NewImageFile in ImageFiles)
            {
                ImagesListView.LargeImageList.Images.Add("K" + i, NewImageFile.Image);
                ImagesListView.Items.Add("Folio " + (i + 1).ToString(), "K" + i);

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
                NewProcessToolStripButton.Enabled = true;
            }
            else
            {
                DeleteToolStripButton.Enabled = false;
                NextToolStripButton.Enabled = false;
                PreviousToolStripButton.Enabled = false;
                NewProcessToolStripButton.Enabled = false;
            }

            TransmitirToolStripButton.Enabled = (_Images.Count > 0) && Program.PuedeAcceder("2.1");
            ReprocesosToolStripButton.Enabled = Program.PuedeAcceder("2.2");
            configurarToolStripMenuItem.Enabled = Program.PuedeAcceder("2.3");
        }

        private void NewProcess()
        {
            CrearDirectorios();

            _Images.Clear();
            DisplayImages(_Images);
            ActivateOptions();

            // Borrar temporal            
            string[] TempFiles = Directory.GetFiles(Program.TempDirectory);

            foreach (string TempFile in TempFiles)
            {
                try { File.Delete(TempFile); }
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
            bool valido;

            var FormMovimiento = new FormSeleccionarFechaMovimiento();

            _fechaMovimiento = DateTime.Now;
            _MovimientoTipo = 0;

            if (FormMovimiento.ShowDialog() == DialogResult.OK)
            {
                _fechaMovimiento = FormMovimiento.FechaMovimiento;
                
                if (FormMovimiento.TipoMovimiento != string.Empty)
                {
                    _MovimientoTipo = short.Parse(FormMovimiento.TipoMovimiento.Split(new[] { ' ', '-' })[0]);
                    _TipoMovimiento = FormMovimiento.TipoMovimiento.Split(new[] { '-' })[1].TrimStart(' ');
                }
                valido = (_MovimientoTipo != 0 && _TipoMovimiento != string.Empty);
            }
            else
            {
                valido = false;
            }

            if (valido)
            {

                DialogResult Respuesta = MessageBox.Show("El sistema se encuentra listo para transmitir " + _Images.Count + " imágenes, ¿desea iniciar el proceso de transmisión de imágenes?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Respuesta == System.Windows.Forms.DialogResult.Yes)
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
                    ZipFileName = Program.TempDirectory + Program.Config.OfficeID.ToString() + "-" + DateTime.Now.ToString("yyyMMdd-hhmmss") + ".zip";
                    ZipUtil.Comprimir(FileNames, ZipFileName, false);

                    AccionToolStripStatusLabel.ForeColor = Color.Green;
                    AccionToolStripStatusLabel.Text = "Iniciando transmisión";
                    Application.DoEvents();

                    Transfer = new FileSendingClient(Program.FileServerIP, Program.FileServerPort, Program.FileServerAppName, Program.SourceDirectory, Program.PackageSize, _fechaMovimiento, _MovimientoTipo);
                    Transfer.TransferBegin += Transfer_TransferBegin;
                    Transfer.TransferProcess += Transfer_TransferProcess;
                    Transfer.TransferCompleted += Transfer_TransferCompleted;
                    Transfer.TransferError += Transfer_TransferError;

                    CancelarToolStripStatusLabel.Visible = true;
                    ContadorToolStripStatusLabel.Visible = true;

                    Identificador = Transfer.Transmitir(ZipFileName, Program.Config.OfficeID, Program.UserID);
                }
            }
            else
            {
                MessageBox.Show("No se insertaron los datos del movimiento, la transmisión no puede continuar", "Transmitir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] RenombrarImagenes(string[] FileNamesGuid)
        {
            var FileNamesNew = new string[FileNamesGuid.Length];
            try
            {
                for (int j = 0; j < FileNamesGuid.Length; j++)
                {
                    string newName = j.ToString().PadLeft(10, '0') + Path.GetExtension(FileNamesGuid[j]);
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

                //BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

                var Cargue = 0;
                short Folios = 0;
                var PaqueteName = Program.Config.OfficeID.ToString("0000") + _fechaMovimiento.ToString("yyyyMMdd");
                var Observaciones = "Oficina-" + Program.Config.OfficeName;

                var Definicion = Transfer.getDefinition(nIdentificador);

                // ServicioWeb.Cargar(Program.Config.OfficeID, CarpetaZip, out Cargue, out Folios);
                Transfer.Cargar(Program.EntidadCliente, Program.Proyecto, Program.Esquema, CarpetaZip, PaqueteName, Observaciones, Program.EntidadProcesamiento, Program.SedeProcesamiento, Program.CentroProcesamiento, Definicion.id_Log, Program.Config.OfficeID, Program.UserID, 31, ref Cargue, ref Folios);

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

                AccionToolStripStatusLabel.Text = "Proceso finalizado para la Fecha de Movimiento: [" + _fechaMovimiento.ToShortDateString() + "], Cargue [" + Cargue + "] - Imagenes [" + Folios + "]";

                MessageBox.Show(AccionToolStripStatusLabel.Text, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Reprocesos()
        {
            new FormReprocesos().ShowDialog();
        }

        private void InsertFolder()
        {
            var CarpetaFolderBrowserDialog = new FolderBrowserDialog();

            CarpetaFolderBrowserDialog.Description = "Seleccione la carpeta de las imágenes";
            CarpetaFolderBrowserDialog.ShowNewFolderButton = false;
            CarpetaFolderBrowserDialog.SelectedPath = LastPath;

            DialogResult Respuesta = CarpetaFolderBrowserDialog.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                int Index = _Images.Count;

                List<ImageFile> NewImageFiles = LoadImageFiles(CarpetaFolderBrowserDialog.SelectedPath);

                LastPath = CarpetaFolderBrowserDialog.SelectedPath;

                if (NewImageFiles.Count == 0)
                {
                    MessageBox.Show("No se encontraron imágenes para cargar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Respuesta = MessageBox.Show("Se encontraron " + NewImageFiles.Count.ToString() + " imágenes, ¿desea adjuntarlas a la imagen actual?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Respuesta == DialogResult.Yes)
                    {
                        foreach (ImageFile NewImageFile in NewImageFiles)
                        {
                            _Images.Add(NewImageFile);
                        }

                        DisplayImages(_Images);
                        ActivateOptions();

                        if (NewImageFiles.Count == 0)
                            MessageBox.Show("No se encontraron imágenes para cargar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                            ImagesListView.SelectedIndices.Add(Index);
                    }
                    else
                    {
                        CrearDirectorios();
                    }
                }
            }
        }

        private void InsertFile()
        {
            var InsertarOpenFileDialog = new OpenFileDialog();

            InsertarOpenFileDialog.Multiselect = false;
            InsertarOpenFileDialog.Title = "Seleccionar imagen";
            InsertarOpenFileDialog.Filter = "Archivos de imagen |*.gif;*.jpg;*.png;*.bmp|Imagenes GIF (*.gif)|*.gif|Imagenes BMP (*.bmp)|*.bmp|Imagenes JPG (*.jpg)|*.jpg|Imagenes PNG (*.png)|*.png|Imagenes TIFF (*.tif)|*.tif";

            var Respuesta = InsertarOpenFileDialog.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                //Se agrega el path de la imagem fuente.
                FileNamesSource.Add(InsertarOpenFileDialog.FileName);

                var Imagen = new Bitmap(InsertarOpenFileDialog.FileName);
                short Folios = ImageManager.GetFolios(Imagen);

                for (int i = 1; i <= Folios; i++)
                {
                    var ImagenFolio = ImageManager.GetFolio(Imagen, i);
                    
                    var NewImageFile = new ImageFile();
                    NewImageFile.Name = Program.TempDirectory + Guid.NewGuid().ToString() + ".jpg";
                    NewImageFile.Image = getThumbnail(ImagenFolio);
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
            string[] FileNames = Directory.GetFiles(ImagePath, "*" + Extension);

            foreach (var FileName in FileNames)
            {
                //Se agrega el path de la imagen fuente
                FileNamesSource.Add(FileName);

                var Imagen = new Bitmap(FileName);
                var Folios = ImageManager.GetFolios(Imagen);

                for (int i = 1; i <= Folios; i++)
                {
                    var ImagenFolio = ImageManager.GetFolio(Imagen, i);

                    var NewImageFile = new ImageFile();
                    NewImageFile.Name = Program.TempDirectory + Guid.NewGuid().ToString() + ".jpg";
                    NewImageFile.Image = getThumbnail(ImagenFolio);
                    NewImageFiles.Add(NewImageFile);                    

                    ImagenFolio.Save(NewImageFile.Name, ImageFormat.Jpeg);
                    ImagenFolio.Dispose();
                }

                Imagen.Dispose();
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

            var ImageThumbnail = ImageManager.GetThumbnail(nImage, ThumbnailWidth, ThumbnailHeight);
            var FormatedThumbnail = new Bitmap(MaxThumbnailWidth, MaxThumbnailHeight);

            var g = Graphics.FromImage(FormatedThumbnail);
            g.FillRectangle(new SolidBrush(Color.Silver), new Rectangle(0, 0, MaxThumbnailWidth, MaxThumbnailHeight));
            g.DrawImage(ImageThumbnail, 0, 0);
            g.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(0, 0, ImageThumbnail.Width, ImageThumbnail.Height));

            ImageThumbnail.Dispose();

            return FormatedThumbnail;
        }

        #endregion        

        private void MenuToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}