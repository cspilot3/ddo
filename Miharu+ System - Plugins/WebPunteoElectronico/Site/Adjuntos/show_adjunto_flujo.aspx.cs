using System;
using System.Data;
using Miharu.Security.Library.Session;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Adjuntos
{
    public partial class show_adjunto_flujo : FormBase
    {
        #region Declaraciones

        public Sesion _MySesion;
                   
        #endregion

        #region Propiedades

        public Sesion MySesion { get { return _MySesion; } }

        public DataTable AdjuntosDataTable = null;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            _MySesion = (Sesion)Session["Sesion"];
            
            var Flujo = int.Parse(Request["token"]);

            
            if (Flujo != 0)
            {
               DBWorkFlow.DBWorkFlowDataBaseManager dbmWorkflow = null;

                try
                {
                    dbmWorkflow = new DBWorkFlow.DBWorkFlowDataBaseManager(this.ConnectionString.Workflow);
                    dbmWorkflow.Connection_Open();

                    AdjuntosDataTable = dbmWorkflow.SchemaProcess.PA_Banagrario_Files.DBExecute(Flujo);

                    var resumenDataTable = new DataTable();
                    resumenDataTable.Columns.Add("Nombre_Flujo_File");
                    resumenDataTable.Columns.Add("URL");

                    foreach (DataRow row in AdjuntosDataTable.Rows)
                    {
                        var newRow = resumenDataTable.NewRow();

                        newRow["Nombre_Flujo_File"] = row["Nombre_Flujo_File"];
                        newRow["URL"] = Program.VisorWorkflowUrl + "?Flujo=" + row["fk_Flujo"] + "&File=" + row["id_Flujo_File"] + "&Version=" + row["id_Flujo_File_Version"];

                        resumenDataTable.Rows.Add(newRow);
                    }

                    FileFlujoGridView.DataSource = resumenDataTable;
                    FileFlujoGridView.DataBind();
                }
                finally
                {
                    if (dbmWorkflow != null) dbmWorkflow.Connection_Close();
                }
            }           

        }

        #endregion

        #region Metodos
        

        #endregion
        
        protected override void Config_Page()
        {
        }

        protected override void Load_Data()
        {

        }
    }
}



