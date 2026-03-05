using System;
using System.IO;
using System.Data;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DBCore.SchemaConfig;
using Miharu.Uploader.Library.UploaderWebServiceReference;
using System.Globalization;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace Miharu.Uploader.Forms
{
    public partial class FormSeleccionarCargue : Form
    {
        public short idEntidad { get; set; }
        public short idProyecto { get; set; }
        public short IdReporte { get; set; }
        public string Path { get; set; }
        public string Separador { get; set; }

        public DataTable Reportes { get; set; }

        public FormSeleccionarCargue()
        {
            InitializeComponent();
        }

        private void FormSeleccionarCargue_Load(object sender, EventArgs e)
        {
            Cargar_Reportes();
        }

        private void Cargar_Reportes()
        {
           
           var WebService = new Miharu.Uploader.Library.WebService.UploaderService();

            WebService.IdUsuario = Program.MiharuSession.Usuario.id;
            WebService.IdEntidad = Program.EntidadCliente;
            WebService.IdProyecto = Program.Proyecto;

            var CipherType = short.Parse(Config.UploaderConfig.Decrypt(WebService.getCifradoTipo().Cifrado, Config.UploaderConfig.EnumCipherType.TDES));

            var Proceso = WebService.getReporteCargue();

            var serializer = new JavaScriptSerializer();

            var ProcesoDataTable = new CTA_Reportes_Uso_ExternoDataTable();

            if (Proceso.Reportes !=null)
            {
            foreach (var reporte in Proceso.Reportes)
            {
                var Datos = serializer.Deserialize<CTA_Reportes_Uso_ExternoSimpleType>(Config.UploaderConfig.Decrypt(reporte.ToString(CultureInfo.InvariantCulture), (Config.UploaderConfig.EnumCipherType)CipherType));
                ProcesoDataTable.Rows.Add(Datos.ToArray());
            }   
            }
                       

            CargueComboBox.DataSource = ProcesoDataTable;
            CargueComboBox.DisplayMember = "Nombre_Reporte";
            CargueComboBox.ValueMember = "Caracter_Separado";

        }
              
        private void CargarPathButton_Click(object sender, EventArgs e)
        {
            var Ruta = new OpenFileDialog();
            Ruta.Filter = "Archivo de Texto|*.txt";

            if (Ruta.ShowDialog() == DialogResult.OK)
            {
                RutaTextBox.Text = Ruta.FileName;
            }
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Path = RutaTextBox.Text;
                Separador = CargueComboBox.SelectedValue.ToString();
                var Seleccion = (DataRowView)CargueComboBox.SelectedItem;
                var Reporte = (CTA_Reportes_Uso_ExternoRow)Seleccion.Row;
                IdReporte = (short)Reporte.Id_Reporte;
                this.DialogResult = DialogResult.OK;
            }
                     
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Funciones

        private bool Validar()
        {
            if (CargueComboBox.Text.Trim() == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un tipo de cargue",
                   Program.AssemblyName,
                   Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                CargueComboBox.SelectAll();
                CargueComboBox.Focus();
                return false;
            }

            if (RutaTextBox.Text.Trim() == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("EL campo Ruta no puede ser vacio",
                   Program.AssemblyName,
                   Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                RutaTextBox.SelectAll();
                RutaTextBox.Focus();
                return false;
            }

            return true;
        }
        #endregion
    }
}
