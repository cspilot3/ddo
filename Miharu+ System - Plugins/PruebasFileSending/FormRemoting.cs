using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBSecurity;

namespace Exportador_Acciones_Valores
{
    public partial class FormRemoting : Form
    {
        public FormRemoting()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this.ConnectButton.Enabled = false;
            this.ErrorTextBox.Text = "";

            DBSecurityDataBaseManager DBM = null;

            try
            {
                DBM = new DBSecurityDataBaseManager(this.ConnectionstringTextBox.Text);

                DBM.Connection_Open(0);

                var Data = DBM.SchemaConfig.TBL_Parametro.DBGet(null);

                this.ResultDataGridView.DataSource = Data;

            }
            catch (Exception ex)
            {
                this.ErrorTextBox.Text = ex.Message;
                this.ErrorTextBox.Text += "\n\n--------------------------------------------------------------\n\n";
                this.ErrorTextBox.Text += ex.StackTrace;
            }
            finally
            {
                if (DBM != null) DBM.Connection_Close();
            }

            this.ConnectButton.Enabled = true;
        }
    }
}
