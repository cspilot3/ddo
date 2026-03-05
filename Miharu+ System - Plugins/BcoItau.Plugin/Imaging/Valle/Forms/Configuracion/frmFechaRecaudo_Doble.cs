using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Desktop.Controls.DesktopMessageBox;
using System.IO;

namespace BcoItau.Plugin.Imaging.Atlantico.Forms.Configuracion
{
    public partial class frmFechaRecaudo_Doble : Form
    {
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string RutaGeneral { get; set; }

        public frmFechaRecaudo_Doble(bool MuestraRuta = false)
        {
            InitializeComponent();

            if (MuestraRuta)
            {
                this.lblRuta_FechaRecaudo.Visible = true;
                this.RutaTextBox_Fecha.Visible = true;
                this.SelectFolderButton_Fecha.Visible = true;
            }
            else
            {
                this.lblRuta_FechaRecaudo.Visible = false;
                this.RutaTextBox_Fecha.Visible = false;
                this.SelectFolderButton_Fecha.Visible = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.dtpFechaRecaudoFinal.Value.Date < this.dtpFechaInicial.Value.Date)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error, La Fecha Final no debe ser Menor a la Fecha Inicial", "Error", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                return;
            }

            

            if (this.lblRuta_FechaRecaudo.Visible)
            {
                if (Validar())
                {
                    this.RutaGeneral = RutaTextBox_Fecha.Text;
                }
                else
                    return;
            }

            FechaInicial = this.dtpFechaInicial.Value.ToString("yyyy/MM/dd");
            FechaFinal = this.dtpFechaRecaudoFinal.Value.ToString("yyyy/MM/dd");

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void RutaTextBox_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void SelectFolderButton_Click_1(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void RutaTextBox_Fecha_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private bool Validar()
        {
            bool result = true;

            if (this.SelectFolderButton_Fecha.Visible == true)
            {
                if (string.IsNullOrEmpty(RutaTextBox_Fecha.Text))
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Un directorio", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon);
                    result = false;
                }

                if (!ValidarRuta())
                {
                    result = false;
                }
            }
            return result;
        }

        private bool ValidarRuta()
        {
            if ((string.IsNullOrEmpty(RutaTextBox_Fecha.Text)))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                RutaTextBox_Fecha.Focus();

            }
            else if ((!Directory.Exists(RutaTextBox_Fecha.Text)))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                RutaTextBox_Fecha.Focus();
                RutaTextBox_Fecha.SelectAll();
            }
            else
            {
                return true;
            }

            return false;
        }

        private void SelectFolderPath()
        {
            dynamic LectorFolderBrowserDialog = new FolderBrowserDialog();
            DialogResult Respuesta = default(DialogResult);

            LectorFolderBrowserDialog.SelectedPath = RutaTextBox_Fecha.Text;
            LectorFolderBrowserDialog.ShowNewFolderButton = false;
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta";

            Respuesta = LectorFolderBrowserDialog.ShowDialog();

            if ((Respuesta == DialogResult.OK))
            {
                RutaTextBox_Fecha.Text = LectorFolderBrowserDialog.SelectedPath;
            }
        }

        
    }
}
