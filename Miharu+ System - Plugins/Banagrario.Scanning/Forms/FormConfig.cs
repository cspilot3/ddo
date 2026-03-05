using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Banagrario.Library.WebService;
using Banagrario.Library.BanagrarioServiceReference;
using Slyg.Tools;
using System.IO;

namespace Banagrario.Scanning.Forms
{
    public partial class FormConfig : Form
    {
        #region Constructores

        public FormConfig()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        public int OfficeID { get; set; }

        public string OfficeName { get; set; }

        public string TempPath { get; set; }

        #endregion

        #region Eventos

        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void CargarPathButton_Click(object sender, EventArgs e)
        {
            TempPathTextBox.Text = GetFolder(TempPathTextBox.Text);
        }

        #endregion

        #region Metodos

        private void LoadData()
        {
            BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

            TypeOffice[] Oficinas = ServicioWeb.getOfficeList();

            OfficeComboBox.DisplayMember = "Display";
            OfficeComboBox.ValueMember = "Value";

            foreach (TypeOffice Oficina in Oficinas)
            {
                OfficeComboBox.Items.Add(new GenericItem<int>(Oficina.Id, Oficina.Name));

                if (this.OfficeID == Oficina.Id)
                    OfficeComboBox.Text = Oficina.Name;
            }

            TempPathTextBox.Text = this.TempPath;
        }

        private void Save()
        {
            if (Validar())
            {
                this.OfficeID = ((GenericItem<int>)OfficeComboBox.SelectedItem).Value;
                this.OfficeName = OfficeComboBox.Text;
                this.TempPath = TempPathTextBox.Text;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (TempPathTextBox.Text == "")
            {
                MessageBox.Show("Debe ingresar la carpeta temporal", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TempPathTextBox.Focus();
            }
            else if (!Directory.Exists(TempPathTextBox.Text))
            {
                MessageBox.Show("La dirección de la carpeta temporal no es una ruta válida", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TempPathTextBox.Focus();
                TempPathTextBox.SelectAll();
            }
            else if (OfficeComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar la oficina", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OfficeComboBox.Focus();
            }
            else
            {
                return true;
            }

            return false;
        }

        private string GetFolder(string nPath)
        {
            FolderBrowserDialog Selector = new FolderBrowserDialog();

            Selector.SelectedPath = nPath;
            Selector.ShowNewFolderButton = false;
            Selector.Description = "Seleccione la carpeta";

            DialogResult Respuesta = Selector.ShowDialog();

            if (Respuesta == DialogResult.OK)
                return Selector.SelectedPath;
            else
                return nPath;
        }

        #endregion
    }
}