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
    public partial class FormSeleccionarFechaMovimiento : Form
    {
        #region Propiedades

        public DateTime FechaMovimiento
        {
            get { return this.FechaMovimientoDateTimePicker.Value; }
            set { this.FechaMovimientoDateTimePicker.Value = value; }
        }

        public string TipoMovimiento 
        {
            get
            {
                if (this.TipoMovimientoComboBox.SelectedItem != null)
                    return TipoMovimientoComboBox.SelectedItem.ToString();
                else
                    return string.Empty;
            }
            set
            {
                this.TipoMovimientoComboBox.SelectedValue = value;
            }
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
            BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

            TypeMovement[] Movimientos = ServicioWeb.getMovementList();

            this.TipoMovimientoComboBox.DisplayMember = "Display";
            this.TipoMovimientoComboBox.ValueMember = "Value";

            foreach (TypeMovement Movimiento in Movimientos)
            {
                this.TipoMovimientoComboBox.Items.Add(new GenericItem<int>(Movimiento.Id, Movimiento.Name));

                if (this.MovementID == Movimiento.Id)
                    this.TipoMovimientoComboBox.Text = Movimiento.Name;
            }            
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
            else if (this.TipoMovimientoComboBox.SelectedIndex < 0)
            {
                this.TipoMovimientoComboBox.Focus();
                MessageBox.Show("Debe seleccionar el tipo de movimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
