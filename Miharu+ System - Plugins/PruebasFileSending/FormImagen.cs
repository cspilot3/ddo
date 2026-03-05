using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBStorage;
using System.IO;
using Slyg.Tools.Imaging;

namespace Exportador_Acciones_Valores
{
    public partial class FormImagen : Form
    {
        public FormImagen()
        {
            InitializeComponent();
        }

        private void MostrarFileButton_Click(object sender, EventArgs e)
        {            
            DBStorageDataBaseManager DBMStorage = null;

            try
            {
                DBMStorage = new DBStorageDataBaseManager(@"SlygProvider=SqlServer;Data Source=10.64.64.56\core;Initial Catalog=DB_Miharu.Imaging_Storage;Persist Security Info=True;User ID=sa;Password=tests123");

                DBMStorage.Connection_Open(1);

                var Expediente = long.Parse(this.ExpedienteTextBox.Text);
                var Folder = short.Parse(this.FolderTextBox.Text);
                var File = short.Parse(this.FileTextBox.Text);
                var Folio = short.Parse(this.FolioTextBox.Text);
                
                var FolioDataTable = DBMStorage.SchemaImaging.TBL_File_Folio.DBGet(Expediente, Folder, File, 1, Folio);

                if (FolioDataTable.Count > 0)
                    this.FolioPictureBox.Image = new Bitmap(new MemoryStream(FolioDataTable[0].Image_Binary));
                else
                    MessageBox.Show("No se encontró la imagen solicitada");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (DBMStorage != null) DBMStorage.Connection_Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBStorageDataBaseManager DBMStorage = null;

            try
            {
                DBMStorage = new DBStorageDataBaseManager(@"SlygProvider=SqlServer;Data Source=10.64.64.56\core;Initial Catalog=DB_Miharu.Imaging_Storage;Persist Security Info=True;User ID=sa;Password=tests123");

                DBMStorage.Connection_Open(1);

                var Expediente = int.Parse(this.ExpedienteTextBox.Text);
                var Folder = short.Parse(this.FolderTextBox.Text);
                var File = short.Parse(this.FileTextBox.Text);
                var Folio = short.Parse(this.FolioTextBox.Text);

                var FolioDataTable = DBMStorage.SchemaImaging.TBL_Item_Folio.DBGet(Expediente, Folder, File, Folio);

                if (FolioDataTable.Count > 0)
                    this.FolioPictureBox.Image = new Bitmap(new MemoryStream(FolioDataTable[0].Image_Binary));
                else
                    MessageBox.Show("No se encontró la imagen solicitada");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (DBMStorage != null) DBMStorage.Connection_Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var FileName = @"D:\3c00771e-e1d5-4ece-8a19-a18701a15dcf(1).tif";
            var DataImage = ImageManager.GetData(FileName);
            this.FolioPictureBox.Image = new Bitmap(FileName);
            this.FolioPictureBox.Image = new Bitmap(new MemoryStream(DataImage));
        }
    }
}
