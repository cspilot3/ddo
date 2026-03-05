using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SLYG.Tools.Scanning;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using Banagrario.Library.WebService;
using Banagrario.Imaging;

namespace Banagrario.Scanning.Forms
{
    public partial class FormCorreccion : Form, IMessageFilter
    {
        #region
        
        public struct ImageFile
        {
            public string Name;
            public Bitmap Image;
        }

        #endregion

        #region Declaraciones
        
        private List<Bitmap> _OldImages = new List<Bitmap>();
        private List<ImageFile> _NewImages = new List<ImageFile>();
        
        public const int MaxThumbnailWidth = 90;
        public const int MaxThumbnailHeight = 120;

        private TwainManager Scanner;
        private bool msgfilter;

        private string LastPath = "";

        #endregion

        #region Propiedades

        public Guid Token { get; set; }

        #endregion

        #region Costructores

        public FormCorreccion()
        {
            InitializeComponent();

            Scanner = new TwainManager();
            Scanner.Init(this.Handle);            
        }        
        
        #endregion
        
        #region Eventos

        private void FormCorreccion_Load(object sender, EventArgs e)
        {
            LoadOldImages();
            ActivateOptions();
        }

        private void ImagesListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ActivateOptions();
        }

        private void SeleccionarOrigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scanner.Seleccionar();
        }

        private void TransferirToolStripButton_Click(object sender, EventArgs e)
        {
            Transferir();
        }

        private void AdquirirToolStripButton_Click(object sender, EventArgs e)
        {
            if (!msgfilter)
            {
                this.Enabled = false;
                msgfilter = true;

                Application.AddMessageFilter(this);
            }
            Scanner.Adquirir();
        }

        private void InsertFolderToolStripButton_Click(object sender, EventArgs e)
        {
            InsertFolder();
        }

        private void InsertFileToolStripButton_Click(object sender, EventArgs e)
        {
            InsertFile();
        }

        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
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

        private void CancelarToolStripButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #endregion        

        #region Metodos

        private void LoadOldImages()
        {
            BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

            OldImagesListView.Items.Clear();
            OldImagesListView.LargeImageList = new ImageList();
            OldImagesListView.LargeImageList.ImageSize = new Size(MaxThumbnailWidth, MaxThumbnailHeight);
            OldImagesListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            short Folios = ServicioWeb.getFolios(Token);

            for (short i = 0; i < Folios;i++ )
            {
                Bitmap Imagen = ServicioWeb.getFolio(Token, (short)(i + 1));
                OldImagesListView.LargeImageList.Images.Add("K" + i, getThumbnail(Imagen));
                OldImagesListView.Items.Add("Folio " + (i + 1).ToString(), "K" + i);

                Imagen.Dispose();
            }

            OldImagesListView.Refresh();
        }

        private void DisplayImages(List<ImageFile> ImageFiles)
        {
            NewImagesListView.Items.Clear();
            NewImagesListView.LargeImageList = new ImageList();
            NewImagesListView.LargeImageList.ImageSize = new Size(MaxThumbnailWidth, MaxThumbnailHeight);
            NewImagesListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            int i = 0;
            foreach (ImageFile NewImageFile in ImageFiles)
            {
                NewImagesListView.LargeImageList.Images.Add("K" + i, NewImageFile.Image);
                NewImagesListView.Items.Add("Folio " + (i + 1).ToString(), "K" + i);

                i += 1;
            }

            NewImagesListView.Refresh();
        }

        private void ActivateOptions()
        {
            TransferirToolStripButton.Enabled = (_NewImages.Count > 0);

            if (NewImagesListView.SelectedIndices.Count > 0)
            {
                int Index = NewImagesListView.SelectedIndices[0];

                DeleteToolStripButton.Enabled = true;
                NextToolStripButton.Enabled = (Index < (_NewImages.Count - 1));
                PreviousToolStripButton.Enabled = (Index > 0);
            }
            else
            {
                DeleteToolStripButton.Enabled = false;
                NextToolStripButton.Enabled = false;
                PreviousToolStripButton.Enabled = false;
            }
        }

        private void Transferir()
        {
            FormMenuStrip.Enabled = false;
            MenuToolStrip.Enabled = false;

            ProcesoToolStripStatusLabel.Text = "Iniciando transmisión...";
            Application.DoEvents();

            try
            {
                ProcesoToolStripStatusLabel.Text = "Conectando al servicio Web...";
                Application.DoEvents();

                BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

                ProcesoToolStripStatusLabel.Text = "Transmitiendo folio 1...";
                Application.DoEvents();

                byte[] Data = ImageManager.GetData(_NewImages[0].Name);
                Guid NewToken = ServicioWeb.NewImage(Token, Data, Program.UserID, (_NewImages.Count == 1));

                for (int i = 1; i < _NewImages.Count; i++)
                {
                    ProcesoToolStripStatusLabel.Text = "Transmitiendo folio " + (i + 1) + "...";
                    Application.DoEvents();

                    Data = ImageManager.GetData(_NewImages[i].Name);
                    ServicioWeb.AddFolio(NewToken, Data, (i == (_NewImages.Count - 1)));
                }

                ProcesoToolStripStatusLabel.Text = "Transmisión finalizada";
                Application.DoEvents();

                MessageBox.Show("La transmisión finalizó exitosamente, Folios: " + _NewImages.Count, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FormMenuStrip.Enabled = true;
                MenuToolStrip.Enabled = true;
            }
        }

        private void InsertFolder()
        {
            FolderBrowserDialog CarpetaFolderBrowserDialog = new FolderBrowserDialog();

            CarpetaFolderBrowserDialog.Description = "Seleccione la carpeta de las imágenes";
            CarpetaFolderBrowserDialog.ShowNewFolderButton = false;
            CarpetaFolderBrowserDialog.SelectedPath = LastPath;

            DialogResult Respuesta = CarpetaFolderBrowserDialog.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                int Index = _NewImages.Count;

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
                            _NewImages.Add(NewImageFile);
                        }

                        DisplayImages(_NewImages);
                        ActivateOptions();

                        if (NewImageFiles.Count == 0)
                            MessageBox.Show("No se encontraron imágenes para cargar", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                            NewImagesListView.SelectedIndices.Add(Index);
                    }
                }
            }            
        }

        private void InsertFile()
        {
            OpenFileDialog InsertarOpenFileDialog = new OpenFileDialog();

            InsertarOpenFileDialog.Multiselect = false;
            InsertarOpenFileDialog.Title = "Seleccionar imagen";
            InsertarOpenFileDialog.Filter = "Archivos de imagen |*.gif;*.jpg;*.png;*.bmp|Imagenes GIF (*.gif)|*.gif|Imagenes BMP (*.bmp)|*.bmp|Imagenes JPG (*.jpg)|*.jpg|Imagenes PNG (*.png)|*.png|Imagenes TIFF (*.tif)|*.tif";

            DialogResult Respuesta = InsertarOpenFileDialog.ShowDialog();

            if (Respuesta == DialogResult.OK)
            {
                Bitmap Imagen = new Bitmap(InsertarOpenFileDialog.FileName);
                int Folios = ImageManager.GetFolios(Imagen);

                for (int i = 1; i <= Folios; i++)
                {
                    Bitmap ImagenFolio = ImageManager.GetFolio(Imagen, i);

                    ImageFile NewImageFile = new ImageFile();
                    NewImageFile.Name = InsertarOpenFileDialog.FileName;
                    NewImageFile.Image = getThumbnail(ImagenFolio);
                    _NewImages.Add(NewImageFile);

                    ImagenFolio.Dispose();
                }

                DisplayImages(_NewImages);
                NewImagesListView.SelectedIndices.Add(_NewImages.Count - 1);

                ActivateOptions();
            }            
        }

        private void LoadScannedImage(ArrayList nImages)
        {
            int Index = _NewImages.Count;

            foreach (Bitmap ImageFile in nImages)
            {
                ImageFile NewImageFile = new ImageFile();
                NewImageFile.Name = Program.TempDirectory + Guid.NewGuid().ToString() + ".jpg";
                NewImageFile.Image = getThumbnail(ImageFile);
                _NewImages.Add(NewImageFile);

                ImageFile.Save(NewImageFile.Name, ImageFormat.Jpeg);
            }

            DisplayImages(_NewImages);
            ActivateOptions();

            if (nImages.Count == 0)
                MessageBox.Show("No se cargaron imagenes", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                NewImagesListView.SelectedIndices.Add(Index);            
        }

        private void Delete()
        {
            if (NewImagesListView.SelectedIndices.Count > 0)
            {
                int Index = NewImagesListView.SelectedIndices[0];

                DialogResult Respuesta = MessageBox.Show("¿Desea remover la imagen seleccionada?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Respuesta == DialogResult.Yes)
                {                    
                    _NewImages.RemoveAt(Index);

                    DisplayImages(_NewImages);
                }
            }

            ActivateOptions();
        }

        private void Next()
        {
            if (NewImagesListView.SelectedIndices.Count > 0)
            {
                int Index = NewImagesListView.SelectedIndices[0];

                ImageFile Item = _NewImages[Index];

                _NewImages.Remove(Item);
                _NewImages.Insert(Index + 1, Item);

                DisplayImages(_NewImages);
                NewImagesListView.SelectedIndices.Add(Index + 1);
            }

            ActivateOptions();            
        }

        private void Previous()
        {
            if (NewImagesListView.SelectedIndices.Count > 0)
            {
                int Index = NewImagesListView.SelectedIndices[0];

                ImageFile Item = _NewImages[Index];

                _NewImages.Remove(Item);
                _NewImages.Insert(Index - 1, Item);

                DisplayImages(_NewImages);
                NewImagesListView.SelectedIndices.Add(Index - 1);
            }

            ActivateOptions();            
        }

        private void EndingScan()
        {
            if (msgfilter)
            {
                Application.RemoveMessageFilter(this);
                msgfilter = false;
                this.Enabled = true;
                this.Activate();
            }
        }

        private void LoadImageFilesByExtension(string ImagePath, ref List<ImageFile> NewImageFiles, string Extension)
        {
            string[] FileNames = Directory.GetFiles(ImagePath, "*" + Extension);

            foreach (string FileName in FileNames)
            {
                Bitmap Imagen = new Bitmap(FileName);
                int Folios = ImageManager.GetFolios(Imagen);

                for (int i = 1; i <= Folios; i++)
                {
                    Bitmap ImagenFolio = ImageManager.GetFolio(Imagen, i);

                    ImageFile NewImageFile = new ImageFile();
                    NewImageFile.Name = FileName;
                    NewImageFile.Image = getThumbnail(ImagenFolio);
                    NewImageFiles.Add(NewImageFile);

                    ImagenFolio.Dispose();
                }

                Imagen.Dispose();
            }
        }

        private Bitmap getThumbnail(Bitmap nImage)
        {
            int ThumbnailWidth = nImage.Width > MaxThumbnailWidth ? MaxThumbnailWidth : nImage.Width;
            int ThumbnailHeight = nImage.Height > MaxThumbnailHeight ? MaxThumbnailHeight : nImage.Height;

            Bitmap ImageThumbnail = ImageManager.GetThumbnail(nImage, ThumbnailWidth, ThumbnailHeight);
            Bitmap FormatedThumbnail = new Bitmap(MaxThumbnailWidth, MaxThumbnailHeight);

            Graphics g = Graphics.FromImage(FormatedThumbnail);
            g.FillRectangle(new SolidBrush(Color.Silver), new Rectangle(0, 0, MaxThumbnailWidth, MaxThumbnailHeight));
            g.DrawImage(ImageThumbnail, 0, 0);
            g.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(0, 0, ImageThumbnail.Width, ImageThumbnail.Height));

            ImageThumbnail.Dispose();

            return FormatedThumbnail;
        }

        #endregion

        #region Funciones

        private List<ImageFile> LoadImageFiles(string ImagePath)
        {
            List<ImageFile> NewImageFiles = new List<ImageFile>();

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
            TwainCommand cmd = Scanner.PasarMensaje(ref m);
            if (cmd == TwainCommand.Not)
                return false;

            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    EndingScan();
                    Scanner.CerrarFuente();
                    break;

                case TwainCommand.CloseOk:
                    EndingScan();
                    Scanner.CerrarFuente();
                    break;

                case TwainCommand.DeviceEvent:
                    break;

                case TwainCommand.TransferReady:
                    ArrayList bitmaps = Scanner.TransferirBitmaps();
                    EndingScan();
                    Scanner.CerrarFuente();

                    LoadScannedImage(bitmaps);

                    break;
            }

            return true;
        }

        #endregion
    }
}
