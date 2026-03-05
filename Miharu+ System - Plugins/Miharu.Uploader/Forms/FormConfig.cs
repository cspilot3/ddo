using System;
using System.IO;
using System.Windows.Forms;
using Miharu.Uploader.Library.UploaderWebServiceReference;
using System.Web.Script.Serialization;
using DBCore.SchemaSecurity;
using DBCore.SchemaConfig;
using System.Globalization;

namespace Miharu.Uploader.Forms
{
    public partial class FormConfig : Form
    {
        public partial class TypeOffice
        {

            private int idField;

            private string nameField;
        }

        //public class GenericItem<T>
        //{
        //    public GenericItem();
        //    public GenericItem(T nValue, string nDisplay);
        //    public GenericItem(T nValue, string nDisplay, string nId);

        //    public string Display { get; set; }
        //    public string Id { get; set; }
        //    public T Value { get; set; }

        //    public override string ToString();
        //}

        #region Constructores

        public FormConfig()
        {
            InitializeComponent();
        }

        #endregion

        #region Propiedades

        public string TempPath { get; set; }

        public int OfficeID { get; set; }

        public string OfficeName { get; set; }

        #endregion

        #region Eventos

        private void FormConfig_Load(object sender, EventArgs e)
        {
            
            var webservice = new Miharu.Uploader.Library.WebService.UploaderService();

            webservice.IdEntidad = Program.MiharuSession.Entidad.id;

            var CipherType = short.Parse(Config.UploaderConfig.Decrypt(webservice.getCifradoTipo().Cifrado, Config.UploaderConfig.EnumCipherType.TDES));

            var Oficinas = webservice.getOfficeList();

            var serializer = new JavaScriptSerializer();

//            var ProcesosDataTable = new CTA_Rol_AccesoDataTable();
            var OficinasDataTable = new TBL_OficinasDataTable();

            foreach (var oficina in Oficinas.Oficinas)
            {
                //var Respuesta = serializer.Deserialize<CTA_Rol_AccesoSimpleType>(Config.UploaderConfig.Decrypt(oficina.ToString(CultureInfo.InvariantCulture), (Config.UploaderConfig.EnumCipherType)CipherType));
                var Respuesta = serializer.Deserialize<TBL_OficinasSimpleType>(Config.UploaderConfig.Decrypt(oficina.ToString(CultureInfo.InvariantCulture), (Config.UploaderConfig.EnumCipherType)CipherType));

//                var OficinaDataRow = ProcesosDataTable.NewCTA_Rol_AccesoRow();
                var OficinaDataRow = OficinasDataTable.NewTBL_OficinasRow();

                OficinaDataRow.fk_Entidad = Respuesta.fk_Entidad;
                OficinaDataRow.fk_Proyecto = Respuesta.fk_Proyecto;
                OficinaDataRow.id_Oficina = Respuesta.id_Oficina;
                OficinaDataRow.codigo_Oficina = Respuesta.codigo_Oficina;
                OficinaDataRow.nombre_Oficina = Respuesta.nombre_Oficina;
                OficinaDataRow.Eliminada = Respuesta.Eliminada;
                //OficinaDataRow.Nombre_Proyecto = Respuesta.Nombre_Proyecto;
                //OficinaDataRow.fk_Esquema = Respuesta.fk_Esquema;
                //OficinaDataRow.Nombre_Esquema = Respuesta.Nombre_Esquema;
                //OficinaDataRow.fk_Rol = Respuesta.fk_Rol;
                //OficinaDataRow.fk_Usuario = Respuesta.fk_Usuario;

                //ProcesosDataTable.AddCTA_Rol_AccesoRow(OficinaDataRow);
                OficinasDataTable.AddTBL_OficinasRow(OficinaDataRow);

            }

            OfficeComboBox.DataSource = OficinasDataTable;
            OfficeComboBox.DisplayMember = "nombre_Oficina";
            OfficeComboBox.ValueMember = "id_Oficina";
            OfficeComboBox.Refresh();

            //TypeOffice[] Oficinas = OficinassDataTable;

            //OfficeComboBox.DisplayMember = "Display";
            //OfficeComboBox.ValueMember = "Value";

            //foreach (TypeOffice Oficina in Oficinas)
            //{
            //    OfficeComboBox.Items.Add(new GenericItem<int>(Oficina.Id, Oficina.Name));

            //    if (this.OfficeID == Oficina.Id)
            //        OfficeComboBox.Text = Oficina.Name;
            //}

            TempPathTextBox.Text = this.TempPath;
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void CargarPathButton_Click(object sender, EventArgs e)
        {
            TempPathTextBox.Text = GetFolder(TempPathTextBox.Text);
        }

        #endregion

        #region Metodos

        private void Save()
        {
            if (!Validar()) return;
            this.OfficeID = (int)OfficeComboBox.SelectedValue;
            this.OfficeName = OfficeComboBox.Text;
            this.TempPath = TempPathTextBox.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (TempPathTextBox.Text == "")
            {
                MessageBox.Show("Debe ingresar la carpeta temporal", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TempPathTextBox.Focus();
            }
            else if (!Directory.Exists(TempPathTextBox.Text))
            {
                MessageBox.Show("La dirección de la carpeta temporal no es una ruta válida", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TempPathTextBox.Focus();
                TempPathTextBox.SelectAll();
            }
            else
            {
                return true;
            }

            return false;
        }

        private static string GetFolder(string nPath)
        {
            var Selector = new FolderBrowserDialog
            {
                SelectedPath = nPath,
                ShowNewFolderButton = false,
                Description = "Seleccione la carpeta"
            };

            var Respuesta = Selector.ShowDialog();

            return Respuesta == DialogResult.OK ? Selector.SelectedPath : nPath;
        }

        #endregion
    }
}