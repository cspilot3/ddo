using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Slyg.Tools;

namespace Exportador_Acciones_Valores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBSecurity.DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurity.DBSecurityDataBaseManager("SlygProvider=SqlServer;Data Source=10.65.54.183\\desarrollo;Initial Catalog=DB_Miharu.Security_Core;Persist Security Info=True;User ID=sa;Password=Cambiar159-");

                dbmSecurity.Connection_Open(1);

                dbmSecurity.Transaction_Begin();

                var perfilType = new DBSecurity.SchemaSecurity.TBL_PerfilType();
                perfilType.id_Perfil = dbmSecurity.SchemaSecurity.TBL_Perfil.DBNextId();
                perfilType.Nombre_Perfil = "Perfil de prueba";
                perfilType.Descripcion_Perfil = "";
                perfilType.Eliminado = false;
                perfilType.Fecha_log = SlygNullable.SysDate;
                perfilType.fk_Usuario_Log = 1;

               // dbmSecurity.SchemaSecurity.TBL_Perfil.DBInsert(perfilType);


                var perfilDataTable = dbmSecurity.SchemaSecurity.TBL_Perfil.DBGet(null);




                this.PerfilesDataGridView.DataSource=perfilDataTable;
                
                dbmSecurity.Transaction_Commit();
            }
            catch (Exception ex)
            {
                dbmSecurity.Transaction_Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }
        }
    }
}
