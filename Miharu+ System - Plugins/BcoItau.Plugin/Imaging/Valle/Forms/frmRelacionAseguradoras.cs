using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Imaging.Valle;
using DBIntegration;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    public partial class frmRelacionAseguradoras : Form
    {
        #region "Declaraciones"
        VallePlugin _Plugin;
        int indexEditarColumn = 0;
        #endregion

        #region Constructores
        public frmRelacionAseguradoras(VallePlugin Plugin)
        {
            this._Plugin = Plugin;
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frmRelacionAseguradoras_Load(object sender, EventArgs e)
        {
            this.dgvAseguradoras.AutoGenerateColumns = false;
            this.cbFiltrarPor.SelectedIndex = 0;
            this.tooltipGeneral.SetToolTip(btnActualizarGrid, "Actualizar Grid");
            cargarGrilla();
        }

        private void cbFiltrarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txtFiltrarPor.Text != "")
            {
                var valorBuscar = this.txtFiltrarPor.Text;
                var SelectedFilter = this.cbFiltrarPor.Text;
                BuscarValor(SelectedFilter, valorBuscar);
            }
        }

        private void txtFiltrarPor_TextChanged(object sender, EventArgs e)
        {
            if (this.txtFiltrarPor.Text != "")
            {
                var valorBuscar = this.txtFiltrarPor.Text;
                var SelectedFilter = this.cbFiltrarPor.Text;
                BuscarValor(SelectedFilter, valorBuscar);
            }
        }

        private void btnActualizarGrid_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        private void dgvAseguradoras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderVar = (DataGridView)sender;
            int idBuscar = 0;

            try
            {
                if (e.ColumnIndex == this.dgvAseguradoras.Columns["Editar"].Index)
                {
                    idBuscar = Convert.ToInt32(dgvAseguradoras.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Configuracion.frmEditarAseguradora frmEditarAseguradora = new Configuracion.frmEditarAseguradora(this._Plugin, idBuscar, "ACTUALIZAR");
                    frmEditarAseguradora.ShowDialog();
                    cargarGrilla(null, null, null, null);
                }
                else if (e.ColumnIndex == this.dgvAseguradoras.Columns["Eliminar"].Index)
                {
                    if (DesktopMessageBoxControl.DesktopMessageShow("¿Realmente Desea Eliminar la Cuenta?", "Eliminar Cuenta", DesktopMessageBoxControl.IconEnum.WarningIcon, false) == System.Windows.Forms.DialogResult.OK)
                    {
                        DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);

                        try
                        {
                            idBuscar = Convert.ToInt32(dgvAseguradoras.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                            var regUpdate = new DBIntegration.SchemaBcoItau.TBL_AseguradorasType();
                            regUpdate.Activo = false;

                            dbmIntegration.SchemaBcoItau.TBL_Aseguradoras.DBUpdate(regUpdate, idBuscar);

                            DesktopMessageBoxControl.DesktopMessageShow("Registro eliminado con exito!!", "Eliminado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                            cargarGrilla(null, null, null, null, null, null);
                        }
                        catch (Exception ex)
                        {
                            DesktopMessageBoxControl.DesktopMessageShow("Error al Eliminar Cuenta!!", ref ex);
                        }
                        finally
                        {
                            dbmIntegration.Connection_Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("dgvCuentasMunicipios_CellContentClick", ref ex);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                DataTable ReportCuentas = dbmIntegration.SchemaBcoItau.PA_Select_Aseguradoras.DBExecute(null, null, null, null, null, null, false);
                Configuracion.frmElegirRuta Ruta = new Configuracion.frmElegirRuta();

                if (Ruta.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (new frmReportesImpuestos(this._Plugin).Genera_ReporteExcel(Ruta.RutaGenerar, ".xlsx", "REPORTE ASEGURADORAS", ReportCuentas, "Hoja1"))
                    {
                        MessageBox.Show("Reporte Excel Generado con Exito!!!");
                        System.Diagnostics.Process.Start(Ruta.RutaGenerar);
                    }
                }

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("btnExportar_Click", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void btnInsertarNuevo_Click(object sender, EventArgs e)
        {
            Configuracion.frmEditarAseguradora frmEditarAseguradora = new Configuracion.frmEditarAseguradora(this._Plugin, 0, "INSERTAR");
            frmEditarAseguradora.ShowDialog();
            cargarGrilla();
        }

        private void dgvAseguradoras_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderVar = (DataGridView)sender;
            int idBuscar = 0;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                idBuscar = Convert.ToInt32(dgvAseguradoras.Rows[e.RowIndex].Cells[0].Value.ToString());
                Configuracion.frmEditarAseguradora frmEditarAseguradora = new Configuracion.frmEditarAseguradora(this._Plugin, idBuscar, "ACTUALIZAR");
                frmEditarAseguradora.ShowDialog();
                cargarGrilla(null, null, null, null);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("dgvCuentasMunicipios_CellContentClick", ref ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        private void cargarGrilla(int? id_Aseguradora = null, string Tipo = null,  string Nombre_Aseguradora = null, string Nit = null, string Codigo = null, string Dv = null)
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                DataTable DtCuentas = dbmIntegration.SchemaBcoItau.PA_Select_Aseguradoras.DBExecute(id_Aseguradora, Tipo,  Nombre_Aseguradora, Nit, Codigo, Dv, true);

                this.dgvAseguradoras.DataSource = DtCuentas;
                this.dgvAseguradoras.Update();

                for (int i = 0; i <= DtCuentas.Columns.Count - 1; i++)
                {
                    var ColumnVar = (DataColumn)DtCuentas.Columns[i];

                    if (ColumnVar.ColumnName == "Editar")
                    {
                        this.indexEditarColumn = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Grid", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void BuscarValor(string SelectedFilter, string valorBuscar)
        {
            switch (SelectedFilter)
            {
                case "TIPO":
                    cargarGrilla(null, valorBuscar, null, null, null, null);
                    break;
                case "CODIGO":
                    cargarGrilla(null,null,null,null, valorBuscar, null);
                    break;
                case "DENOMINACION":
                    cargarGrilla(null,null, valorBuscar, null, null, null);
                    break;
                case "NIT":
                    cargarGrilla(null, null, null, valorBuscar, null, null);
                    break;
                case "DV":
                    cargarGrilla(null, null, null, null,null, valorBuscar);
                    break;
            }
        }

        

        

        

        

        

        

        
    }
}
