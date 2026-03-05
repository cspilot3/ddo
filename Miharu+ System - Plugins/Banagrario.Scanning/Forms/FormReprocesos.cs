using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Banagrario.Library.WebService;
using Banagrario.Library.BanagrarioServiceReference;

namespace Banagrario.Scanning.Forms
{
    public partial class FormReprocesos : Form
    {
        #region Constructores

        public FormReprocesos()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void FormReprocesos_Load(object sender, EventArgs e)
        {
            LoadData();

            CargueListView.Focus();
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{UP}");
        }

        private void CerrarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ProcesarButton_Click(object sender, EventArgs e)
        {
            Procesar();
        }

        private void CargueListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarControles();
        }

        private void CargueListView_DoubleClick(object sender, EventArgs e)
        {
            Procesar();
        }

        #endregion

        #region Metodos

        private void LoadData()
        {
            CargueListView.Items.Clear();

            BanagrarioWebService ServicioWeb = new BanagrarioWebService(Program.BanagrarioWebServiceURL);

            TypeError[] Errores = ServicioWeb.getErrorList(Program.Config.OfficeID);

            if (Errores != null)
            {
                for (int i = 0; i < Errores.Length; i++)
                {
                    string[] Elementos = new string[] { i.ToString(), Errores[i].Fecha, Errores[i].Token.ToString(), Errores[i].Document, Errores[i].Description };

                    CargueListView.Items.Add(new ListViewItem(Elementos, Errores[i].ErrorID));
                }
            }
        }

        private void ActivarControles()
        {
            ProcesarButton.Enabled = (CargueListView.SelectedItems.Count > 0);
        }

        private void Procesar()
        {
            try
            {
                if (Validar())
                {
                    string Token = CargueListView.SelectedItems[0].SubItems[2].Text;

                    FormCorreccion CorrecionForm = new FormCorreccion();
                    CorrecionForm.Token = new Guid(Token);
                    DialogResult Respuesta = CorrecionForm.ShowDialog();

                    if (Respuesta == System.Windows.Forms.DialogResult.OK)
                        CargueListView.SelectedItems[0].Remove();                        
                }
                else
                {
                    ActivarControles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Funciones

        public bool Validar()
        {
            if (CargueListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un cargue", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargueListView.Focus();
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
