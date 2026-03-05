using Miharu.CargueImagenes.Servicio;
using Miharu.CargueLog.Servicio;
using Miharu.DDOTransfer.Servicio;
//using Miharu.CanjeTransfer;
//using Miharu.CanjeTransfer.Servicio;
using Miharu.FileSending;
using Miharu.FileSending.Servicio;
using Miharu.Imaging.FileTransfer.Servicio;
using Miharu.MailSender.Servicio;
using Miharu.SofTracTransfer;
using Miharu.SofTracTransfer.Servicio;
using Miharu.SynergeticsTransfer.Servicio;
using Miharu.Reportes.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exportador_Acciones_Valores
{
    public partial class FormCanjeTransfer : Form
    {
        public FormCanjeTransfer()
        {
            InitializeComponent();
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            //CanjeTransferService servicio = new CanjeTransferService();
            //SofTracService servicio = new SofTracService();
            //ExportarService servicio = new ExportarService();
            //ImagingFileTransferService servicio = new ImagingFileTransferService();
            //MailSenderService servicio = new MailSenderService();
            //SynergeticsService servicio = new SynergeticsService();
            //FileSendingService servicio = new FileSendingService();
            //DDOTransferService servicio = new DDOTransferService();
            //CargueLogService servicio = new CargueLogService();
            //CargueImagenesService servicio = new CargueImagenesService();
            ReportesService servicio = new ReportesService();

            servicio.IniciarServicio();
        }
    }
}

