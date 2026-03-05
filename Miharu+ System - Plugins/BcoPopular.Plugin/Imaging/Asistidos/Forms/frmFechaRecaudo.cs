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

namespace BcoPopular.Plugin.Imaging.Asistidos.Forms
{
    public partial class frmFechaRecaudo : Form
    {
        public string Fecha_Recaudo_Inicio { get; set; }
        public string Fecha_Recaudo_Fin { get; set; }
        public bool Matrices { get; set; }
        public string RutaGeneral { get; set; }
        public string _Tipo { get; set; }

        public frmFechaRecaudo(bool matrices = false, string Tipo = "")
        {
            InitializeComponent();

            this.Text = "Fecha de Proceso";
            this.lblFechaRecaudo.Text = "Fecha Proceso";

            //this.lblFechaRecaudo2.Visible = false;
            //this.dtpFechaInicial2.Visible = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Fecha_Recaudo_Inicio = this.dtpFechaInicial.Value.ToString("yyyy/MM/dd");
            this.Fecha_Recaudo_Fin = this.dtpFechaInicial2.Value.ToString("yyyy/MM/dd");
            this.Close();
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

        private void frmFechaRecaudo_Load(object sender, EventArgs e)
        {
            this.dtpFechaInicial.MaxDate = DateTime.Now;
            this.dtpFechaInicial2.MaxDate = DateTime.Now;
        }
    }
}
