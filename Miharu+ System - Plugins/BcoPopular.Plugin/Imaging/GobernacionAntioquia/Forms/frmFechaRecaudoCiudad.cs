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

namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms
{
    public partial class frmFechaRecaudoCiudad : Form
    {
        public DateTime Fecha_Recaudo { get; set; }
        public DateTime Fecha_Recaudo2 { get; set; }
        public bool Matrices { get; set; }
        public string RutaGeneral { get; set; }
        public string _Tipo { get; set; }
        public object Municipio { get; set; }

        public frmFechaRecaudoCiudad(bool matrices = false, string Tipo = "")
        {
            InitializeComponent();
            this.Matrices = matrices;
            this._Tipo = Tipo;

            if (matrices)
            {
                this.lblRuta.Visible = true;
                this.RutaTextBox.Visible = true;
                this.SelectFolderButton.Visible = true;
            }
            else
            {
                this.lblRuta.Visible = false;
                this.RutaTextBox.Visible = false;
                this.SelectFolderButton.Visible = false;
            }

            if (Tipo == "PROCESO")
            {
                this.Text = "Fecha de Proceso";
                this.lblFechaRecaudo.Text = "Fecha Proceso";

                this.lblFechaRecaudo2.Visible = false;
                this.dtpFechaInicial2.Visible = false;

                this.lblRuta.Visible = true;
                this.RutaTextBox.Visible = true;
                this.SelectFolderButton.Visible = true;
            }

            if (Tipo == "TODO")
            {
                this.gbxBase.Visible = false;
                this.Text = "Ubicación Reporte";

                this.lblRuta.Visible = true;
                this.RutaTextBox.Visible = true;
                this.SelectFolderButton.Visible = true;
            }

            if (Tipo == "FECHA")
            {
                this.Text = "Fecha de Proceso";
                this.lblFechaRecaudo.Text = "Fecha Proceso";

                this.lblFechaRecaudo2.Visible = false;
                this.dtpFechaInicial2.Visible = false;

                this.lblRuta.Visible = false;
                this.RutaTextBox.Visible = false;
                this.SelectFolderButton.Visible = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Fecha_Recaudo = this.dtpFechaInicial.Value;
                this.Fecha_Recaudo2 = this.dtpFechaInicial2.Value;
                this.RutaGeneral = this.RutaTextBox.Text;
                this.Municipio = this.cbCiudad.SelectedValue;
                this.Close();
            }
            
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmFechaRecaudoCiudad_Load(object sender, EventArgs e)
        {
            this.dtpFechaInicial.MaxDate = DateTime.Now;
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

            if (this.Matrices || this.SelectFolderButton.Visible == true)
            {
                if (string.IsNullOrEmpty(RutaTextBox.Text))
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

        
    }
}
