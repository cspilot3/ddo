using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Imaging.Impuestos;
using DBIntegration;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
{
    public partial class frmRelacionCuentasMunicipio : Form
    {
        #region "Declaraciones"
        ImpuestosPlugin _Plugin;
        int indexEditarColumn = 0;
        #endregion

        #region Constructor
        public frmRelacionCuentasMunicipio(ImpuestosPlugin Plugin)
        {
            this._Plugin = Plugin;
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frmRelacionCuentasMunicipio_Load(object sender, EventArgs e)
        {
            this.dgvCuentasMunicipios.AutoGenerateColumns = false;
            this.cbFiltrarPor.SelectedIndex = 0;
            this.tooltipGeneral.SetToolTip(btnActualizarGrid, "Actualizar Grid");
            cargarGrilla();
        }

        private void dgvCuentasMunicipios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderVar = (DataGridView)sender;
            int idBuscar = 0;

            try
            {
                if (e.ColumnIndex == this.dgvCuentasMunicipios.Columns["Editar"].Index)
                {
                    idBuscar = Convert.ToInt32(dgvCuentasMunicipios.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Configuracion.frmEditarCuentaMunicipio frmEditarCuenta = new Configuracion.frmEditarCuentaMunicipio(this._Plugin, idBuscar, "ACTUALIZAR");
                    frmEditarCuenta.ShowDialog();
                    cargarGrilla(null, null, null);
                }
                else if (e.ColumnIndex == this.dgvCuentasMunicipios.Columns["Eliminar"].Index)
                {
                    if (DesktopMessageBoxControl.DesktopMessageShow("¿Realmente Desea Eliminar la Cuenta?", "Eliminar Cuenta", DesktopMessageBoxControl.IconEnum.WarningIcon, false) == System.Windows.Forms.DialogResult.OK)
                    {
                        DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoBogotaConnectionString);

                        try
                        {
                            idBuscar = Convert.ToInt32(dgvCuentasMunicipios.Rows[e.RowIndex].Cells[0].Value.ToString());
                            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                            var regUpdate = new DBIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_MunicipioType();
                            int idVigente = Convert.ToInt32(dbmIntegration.SchemaBcoBogota.TBL_Estado_Cuenta.DBFindByNombre_Estado("Inactivo").ToList().First().id);
                            regUpdate.fk_Estado_Cuenta =  idVigente;

                            dbmIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_Municipio.DBUpdate(regUpdate, idBuscar);

                            DesktopMessageBoxControl.DesktopMessageShow("Registro eliminado con exito!!","Eliminado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
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

        
        private void txtFiltrarPor_TextChanged(object sender, EventArgs e)
        {
            if (this.txtFiltrarPor.Text != "")
            {
                var valorBuscar = this.txtFiltrarPor.Text;
                var SelectedFilter = this.cbFiltrarPor.Text;
                BuscarValor(SelectedFilter, valorBuscar);
            }
        }

        private void cbFiltrarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txtFiltrarPor.Text != "")
            {
                var valorBuscar = this.txtFiltrarPor.Text;
                var SelectedFilter = this.cbFiltrarPor.Text;
                BuscarValor(SelectedFilter, valorBuscar);
            }

            if (this.cbFiltrarPor.SelectedText.ToUpper() == "SIN FILTRO")
            {
                cargarGrilla(null, null, null, null, null, null);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                DataTable ReportCuentas = dbmIntegration.SchemaBcoBogota.PA_Reporte_RelacionesCuentas_Municipios.DBExecute();
                Configuracion.frmElegirRuta Ruta = new Configuracion.frmElegirRuta();

                if (Ruta.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if(new frmReportesImpuestos(this._Plugin).Genera_ReporteExcel(Ruta.RutaGenerar, ".xlsx", "REPORTE ESTADO CUENTAS MUNICIPIOS", ReportCuentas, "Hoja1"))
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
            Configuracion.frmEditarCuentaMunicipio frmEditarCuenta = new Configuracion.frmEditarCuentaMunicipio(this._Plugin, 0, "INSERTAR");
            frmEditarCuenta.ShowDialog();
            cargarGrilla();
        }

        private void btnActualizarGrid_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        private void dgvCuentasMunicipios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderVar = (DataGridView)sender;
            int idBuscar = 0;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                idBuscar = Convert.ToInt32(dgvCuentasMunicipios.Rows[e.RowIndex].Cells[0].Value.ToString());
                Configuracion.frmEditarCuentaMunicipio frmEditarCuenta = new Configuracion.frmEditarCuentaMunicipio(this._Plugin, idBuscar, "ACTUALIZAR");
                frmEditarCuenta.ShowDialog();
                cargarGrilla(null, null, null);
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


        private void cargarGrilla(int? id = null, string No_Cuenta_Bco_Bogota = null, string nombre_cuenta = null, string Codigo_Compensación = null, string Nombre_Municipio = null, string Estado = null)
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_Plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                DataTable DtCuentas = dbmIntegration.SchemaBcoBogota.PA_SelectStatement_RelacionCuentas_Municipios.DBExecute(id, No_Cuenta_Bco_Bogota, nombre_cuenta, Codigo_Compensación, Nombre_Municipio, Estado);

                this.dgvCuentasMunicipios.DataSource = DtCuentas;
                this.dgvCuentasMunicipios.Update();

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
                case "No Cuenta Bco Bogota":
                    cargarGrilla(null, valorBuscar, null);
                    break;
                case "Nombre Cuenta":
                    cargarGrilla(null, null, valorBuscar);
                    break;
                case "Codigo Compensacion":
                    cargarGrilla(null, null, null, valorBuscar);
                    break;
                case "Nombre Municipio":
                    cargarGrilla(null, null, null, null, valorBuscar);
                    break;
                case "Estado":
                    cargarGrilla(null, null, null, null, valorBuscar);
                    break;
            }
        }

        

        

        

        

        
    }
}
