using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms.Configuracion
{
    public partial class frmElegirRuta : Form
    {

        public string RutaGenerar { get; set; }

        public frmElegirRuta()
        {
            InitializeComponent();
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
            return result;
        }

        private bool ValidarRuta()
        {
            if ((string.IsNullOrEmpty(RutaTextBox.Text)))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                RutaTextBox.Focus();

            }
            else if ((!Directory.Exists(RutaTextBox.Text)))
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

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void RutaTextBox_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.RutaGenerar = this.RutaTextBox.Text;
                this.Cursor = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(1000);
                this.Close();
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
    }
}
