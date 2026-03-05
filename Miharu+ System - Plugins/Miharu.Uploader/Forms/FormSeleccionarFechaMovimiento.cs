using System;
using System.Windows.Forms;

namespace Miharu.Uploader.Forms
{
    public partial class FormSeleccionarFechaMovimiento : Form
    {
        #region Propiedades

        public DateTime FechaMovimiento
        {
            get { return this.FechaMovimientoDateTimePicker.Value; }
            set { this.FechaMovimientoDateTimePicker.Value = value; }
        }
        
        public short MovementID { get; set; }

        #endregion

        #region Constructores

        public FormSeleccionarFechaMovimiento()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void FormSeleccionarFechaMovimiento_Load(object sender, EventArgs e)
        {
            FechaMovimientoDateTimePicker.Value = DateTime.Now;
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (this.FechaMovimientoDateTimePicker.Value > DateTime.Now)
            {
                this.FechaMovimientoDateTimePicker.Focus();
                MessageBox.Show("La fecha de movimiento no puede ser posterior a la fecha actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
