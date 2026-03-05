using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.FileProvider;
using Miharu.MailSender.Servicio;

namespace Exportador_Acciones_Valores
{
    public partial class FormFileProvider : Form
    {
        public FormFileProvider()
        {
            InitializeComponent();
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            FileProviderDDOService servicio = new FileProviderDDOService();
            //var servicio = new MailSenderService();
            servicio.IniciarServicio();
        }

     

       
    }
}
