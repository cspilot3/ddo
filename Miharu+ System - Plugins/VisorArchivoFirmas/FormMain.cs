using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VisorArchivoFirmas
{
    public partial class FormMain : Form
    {
        #region Declaraciones

        private DataTable _dataTable;

        #endregion

        #region Constructores

        public FormMain()
        {
            InitializeComponent();

            _dataTable = new DataTable();

            _dataTable.Columns.Add("id", typeof(string));
            _dataTable.Columns.Add("Cuenta", typeof(string));
            _dataTable.Columns.Add("Ente", typeof(string));
            _dataTable.Columns.Add("Tipo", typeof(string));
            _dataTable.Columns.Add("Imagen", typeof(byte[]));
        }

        #endregion

        #region Eventos

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            Cargar();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView.CurrentRow == null) return;

            var fila = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;

            try
            {
                var datos = (byte[]) fila["Imagen"];
                var imagen = new Bitmap(new MemoryStream(datos));

                pictureBox.Image = imagen;

                var info = new ImageInfo
                {
                    Ancho = imagen.Width + " px",
                    Alto = imagen.Height + " px",
                    Resolucion = imagen.HorizontalResolution + " dpi"
                };

                info.Resolucion = imagen.VerticalResolution + " dpi";
                info.Tamaño = datos.Length.ToString("#,##0") + " bytes";
                propertyGrid.SelectedObject = info;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen, " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void acerdaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAboutBox().ShowDialog();
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            Visualizar();
        }

        private void removeFilterButton_Click(object sender, EventArgs e)
        {
            filterTextBox.Text = "";
            Visualizar();
        }

        #endregion

        #region Metodos
        
        private void Cargar()
        {
            const string rowSeparator = "<||>";
            const string colSeparator = "|<>|";

            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = @"Archivo binario (*.bin)|*bin"
            };

            var result = openFileDialog.ShowDialog();

            if(result != DialogResult.OK) return;

            var fileName = openFileDialog.FileName;

            fileNameLabel.Text = fileName;
            
            _dataTable.Clear();

            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);

                var filas = Split(data, Encoding.ASCII.GetBytes(rowSeparator));

                foreach (var fila in filas)
                {
                    var columnas = Split(fila, Encoding.ASCII.GetBytes(colSeparator));

                    var newRow = _dataTable.NewRow();

                    newRow["id"] = Encoding.ASCII.GetString(columnas[0]);
                    newRow["Cuenta"] = Encoding.ASCII.GetString(columnas[6]);
                    newRow["Ente"] = Encoding.ASCII.GetString(columnas[4]);
                    newRow["Tipo"] = Encoding.ASCII.GetString(columnas[2]);
                    newRow["Imagen"] = columnas[1];

                    _dataTable.Rows.Add(newRow);
                }
            }

            filterTextBox.Text = "";
            Visualizar();
        }

        private List<byte[]> Split(byte[] nData, byte[] nSeparator)
        {
            var partes = new List<byte[]>();
            var paso = nSeparator.Length;
            var inicio = 0;

            for (var i = 0; i < nData.Length - 4; i++)
            {
                if (nData[i] != nSeparator[0] || nData[i + 1] != nSeparator[1] || nData[i + 2] != nSeparator[2] ||
                    nData[i + 3] != nSeparator[3]) continue;

                var parte = new byte[i - inicio];

                for (var ii = 0; ii < i - inicio; ii++)
                {
                    parte[ii] = nData[ii + inicio];
                }

                partes.Add(parte);

                i = i + paso;
                inicio = i;
            }

            if (inicio < nData.Length)
            {
                var parte = new byte[nData.Length - inicio];

                for (var ii = 0; ii < nData.Length - inicio; ii++)
                {
                    parte[ii] = nData[ii + inicio];
                }

                partes.Add(parte);
            }

            return partes;
        }
        
        private void Visualizar()
        {
            if (filterTextBox.Text != "")
            {
                var filterView = new DataView(_dataTable) {RowFilter = "Cuenta = '" + filterTextBox.Text + "'"};

                dataGridView.DataSource = filterView;
            }
            else
            {
                dataGridView.DataSource = _dataTable;
            }            
        }

        #endregion
    }

    public class ImageInfo
    {
        public string Ancho { get; set; }
        public string Alto { get; set; }
        public string Resolucion { get; set; }
        public string Tamaño { get; set; }
    }
}
