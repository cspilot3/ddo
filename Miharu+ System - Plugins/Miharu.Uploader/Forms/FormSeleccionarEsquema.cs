using System;
using System.Data;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using DBCore.SchemaSecurity;
using Miharu.Uploader.Library.UploaderWebServiceReference;

namespace Miharu.Uploader.Forms
{
    public partial class FormSeleccionarEsquema : Form
    {
        public int IdUsuario;
        public short IdEntidad;
        public short IdProyecto {get; set;}
        public short IdEsquema {get; set;}
        public string NombreProyecto { get; set; }
        private DataTable EsquemasDataTable;
        public FormSeleccionarEsquema()
        {
            InitializeComponent();
        }

        private void FormSeleccionarEsquema_Load(object sender, EventArgs e)
        {
            Load_Config();
        }

        private void Load_Config()
        {
            
                    
            var webservice = new Miharu.Uploader.Library.WebService.UploaderService();

            webservice.IdUsuario = Program.MiharuSession.Usuario.id;
            webservice.IdEntidad = Program.MiharuSession.Entidad.id;

            var CipherType = short.Parse(Config.UploaderConfig.Decrypt(webservice.getCifradoTipo().Cifrado, Config.UploaderConfig.EnumCipherType.TDES));
                       
            var Procesos = webservice.getProcesos();

            var serializer = new JavaScriptSerializer();

            var ProcesosDataTable = new CTA_Rol_AccesoDataTable();
                      
            foreach (var proceso in Procesos.Procesos)
            {
                var Respuesta = serializer.Deserialize<CTA_Rol_AccesoSimpleType>(Config.UploaderConfig.Decrypt(proceso.ToString(CultureInfo.InvariantCulture), (Config.UploaderConfig.EnumCipherType)CipherType));

                var ProcesoDataRow = ProcesosDataTable.NewCTA_Rol_AccesoRow();

                ProcesoDataRow.fk_Entidad = Respuesta.fk_Entidad;
                ProcesoDataRow.fk_Proyecto = Respuesta.fk_Proyecto;
                ProcesoDataRow.Nombre_Proyecto = Respuesta.Nombre_Proyecto;
                ProcesoDataRow.fk_Esquema = Respuesta.fk_Esquema;
                ProcesoDataRow.Nombre_Esquema = Respuesta.Nombre_Esquema;
                ProcesoDataRow.fk_Rol = Respuesta.fk_Rol;
                ProcesoDataRow.fk_Usuario = Respuesta.fk_Usuario;

                ProcesosDataTable.AddCTA_Rol_AccesoRow(ProcesoDataRow);
              
            }

            //var Respuesta = serializer.Deserialize<DataTable>(Procesos.Procesos);

            EsquemasDataTable = ProcesosDataTable.Copy();


            //var GruposD = ProcesosDataTable.GroupBy(n => new { n.fk_Grupo }).Select(g => g.Key.fk_Entidad).ToList();
            //ProcesosDataTable.
            DataTable a = new DataView(ProcesosDataTable).ToTable(true, "fk_Entidad","fk_Proyecto", "Nombre_Proyecto");

            ProyectoComboBox.DataSource = a;
            ProyectoComboBox.DisplayMember = "Nombre_Proyecto";
            ProyectoComboBox.ValueMember = "fk_Proyecto";
            ProyectoComboBox.Refresh();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            Seleccion(EsquemaDataGridView.CurrentRow.Index);
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ProyectoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ProyectoSelected = (DataRowView)ProyectoComboBox.SelectedItem;
            //var Proy = (CTA_Rol_AccesoRow)ProyectoSelected.Row;
            var Proy = ProyectoSelected.Row;
            //var query = "fk_Proyecto = " + Proy.fk_Proyecto.ToString(CultureInfo.InvariantCulture);
            var query = "fk_Proyecto = " + Proy["fk_Proyecto"].ToString();
            var Filtro = EsquemasDataTable.DefaultView;
            Filtro.RowFilter = query;
            EsquemaDataGridView.DataSource = Filtro.ToTable(true,"fk_Esquema","Nombre_Esquema");
            EsquemaDataGridView.Refresh();
        }

        private void EsquemaDataGridView_CellMouseDoubleClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            Seleccion(e.RowIndex);
        }

        private void Seleccion(int nIndex)
        {
            IdProyecto = (short)ProyectoComboBox.SelectedValue;
            IdEsquema = (short)EsquemaDataGridView.SelectedRows[nIndex].Cells["Id_Esquema"].Value;
            NombreProyecto = (string)EsquemaDataGridView.SelectedRows[nIndex].Cells["Nombre_Esquema"].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
