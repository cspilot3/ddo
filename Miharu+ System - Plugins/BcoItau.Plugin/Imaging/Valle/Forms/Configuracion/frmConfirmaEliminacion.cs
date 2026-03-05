using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BcoItau.Plugin.Imaging.Atlantico.Forms.Configuracion
{
    public partial class frmConfirmaEliminacion : Form
    {
        string _Rutacarpeta = "";
        public bool RespuestaPredeterminada { get; set; }
        public string RespuestaPredetermiandaStr { get; set; }

        public frmConfirmaEliminacion(string rutaCarpeta)
        {
            this._Rutacarpeta = rutaCarpeta;
            InitializeComponent();
        }

        private void frmConfirmaEliminacion_Load(object sender, EventArgs e)
        {
            this.lblPreguntaConfirmacion.Text = "La Carpeta " + this._Rutacarpeta + " no esta vacia, ¿Desea eliminar su contenido?";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.RespuestaPredetermiandaStr = "ACEPTAR";

            if (this.chkRespuestaAbsoluta.Checked)
            {
                this.RespuestaPredeterminada = true;
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.RespuestaPredetermiandaStr = "CANCELAR";

            if (this.chkRespuestaAbsoluta.Checked)
            {
                this.RespuestaPredeterminada = true;
            }

            this.Close();
        }
    }
}
