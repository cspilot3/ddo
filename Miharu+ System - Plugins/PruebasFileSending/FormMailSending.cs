using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Banco_Agrario.DataAccess;
using Miharu.MailSender.Library;
using Miharu.MailSender.DataAccess;

namespace Exportador_Acciones_Valores
{
    public partial class FormMailSending : Form
    {
        public FormMailSending()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var dbmBanagrario = new DBAgrario.DBAgrarioDataBaseManager(Program.CnnBanagrario);
                //var Oficina = textBox1.Text;
                //var fecha = dateTimePicker1.Value;
                dbmBanagrario.Connection_Open(1);

                var datos = dbmBanagrario.SchemaReport.PA_Gestion_Registros_Sobrantes.DBExecute(null, null, 230, "2011/08/04");

                var OficinaDataTable = dbmBanagrario.SchemaConfig.TBL_Oficina.DBGet(230);

                if (OficinaDataTable.Count > 0)
                {
                    var OficinaRow = (DBAgrario.SchemaConfig.TBL_OficinaRow)OficinaDataTable.Rows[0];

                    enviar_correo("julian.duque@slyg.com.co", OficinaRow.Correo_Contacto, GetDataTableAsHTML(datos), "Correo de Prueba");
                }

               dbmBanagrario.Connection_Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void enviar_correo(string From, string To, string Message, string Subject)  
        {    //Declarando Variables  

            var DBMTools = new Miharu.MailSender.DataAccess.Miharu_Tools.DBToolsDataBaseManager(Program.CnnTools);

            try
            {
                DBMTools.Connection_Open();

                Miharu.MailSender.DataAccess.Miharu_Tools.SchemaMail.TBL_QueueType MailType = new Miharu.MailSender.DataAccess.Miharu_Tools.SchemaMail.TBL_QueueType();

                MailType.id_Queue = Guid.NewGuid();
                MailType.fk_Entidad = 1;
                MailType.fk_Usuario = 137;
                MailType.Fecha_Queue = DateTime.Now;

                MailType.EmailAddress_Queue = To;
                MailType.CC_Queue = ";";
                MailType.CCO_Queue = ";";

                MailType.Subject_Queue = Subject;
                MailType.Message_Queue = Message;

                DBMTools.SchemaMail.TBL_Queue.DBInsert(MailType.id_Queue, MailType.fk_Entidad, MailType.fk_Usuario, MailType.Fecha_Queue, MailType.EmailAddress_Queue, MailType.CC_Queue, MailType.CCO_Queue, MailType.Subject_Queue, MailType.Message_Queue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBMTools.Connection_Close();
            }
                
        }

        private string GetDataTableAsHTML(DataTable thisTable)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat(@"<caption> Total de Registros =");
            sb.AppendFormat(thisTable.Rows.Count.ToString());
            sb.AppendFormat(@"  </caption>");

            sb.Append("<TABLE BORDER=1>");

            sb.Append("<TR ALIGN='CENTER'>");

            //first append the column names.
            foreach (DataColumn column in thisTable.Columns)
            {
                sb.Append("<TD><B>");
                sb.Append(column.ColumnName);
                sb.Append("</B></TD>");
            }

            sb.Append("</TR>");

            // next, the column values.
            foreach (DataRow row in thisTable.Rows)
            {
                sb.Append("<TR ALIGN='CENTER'>");

                foreach (DataColumn column in thisTable.Columns)
                {
                    sb.Append("<TD>");
                    if (row[column].ToString().Trim().Length > 0)
                        sb.Append(row[column]);
                    else
                        sb.Append(" ");
                    sb.Append("</TD>");
                }

                sb.Append("</TR>");
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }
        
    }
}
