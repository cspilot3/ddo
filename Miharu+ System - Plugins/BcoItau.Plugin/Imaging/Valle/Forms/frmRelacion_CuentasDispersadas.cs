using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBIntegration;
using Imaging.Valle;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    public partial class frmRelacion_CuentasDispersadas : Form
    {
        #region Declaraciones
        VallePlugin _plugin;
        string _StrFechaRecaudo = "";
        List<int> ltIdsEditar = null;
        public string _nombreReporte { get; set; }
        #endregion


        public frmRelacion_CuentasDispersadas(VallePlugin plugin)
        {
            this._plugin = plugin;
            InitializeComponent();
        }

        private void frmRelacion_CuentasDispersadas_Load(object sender, EventArgs e)
        {
            this.dtpFechaRecaudo.MaxDate = DateTime.Now;
            this.dgvDispersadasCuentas.AutoGenerateColumns = false;
        }

        private void btnMostrarDatos_Click(object sender, EventArgs e)
        {
            this._StrFechaRecaudo = this.dtpFechaRecaudo.Value.ToString("yyyy/MM/dd");
            CargarGrilla();
        }

        private void btnEdiarTodos_Click(object sender, EventArgs e)
        {
            this.ltIdsEditar = new List<int>();

            if (this.dgvDispersadasCuentas.Rows.Count > 0)
            {
                int indexSeleccionar = this.dgvDispersadasCuentas.Columns["Seleccionar"].Index;

                foreach (DataGridViewRow itemRow in this.dgvDispersadasCuentas.Rows)
                {
                    if ((int)itemRow.Cells[indexSeleccionar].Value == 1)
                    {
                        ltIdsEditar.Add((int)itemRow.Cells[0].Value);
                    }
                }
            }
            else
            {
                return;
            }

            if (this.ltIdsEditar.Count > 0)
            {
                Configuracion.frmEligeEstado_FechaDispersion frmEligeDatos = new Configuracion.frmEligeEstado_FechaDispersion(this._plugin, this.ltIdsEditar);
                frmEligeDatos.ShowDialog();
                CargarGrilla();
            }
            else
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error, debe seleccionar un registro para editar!!", "Editar", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        private void dgvDispersadasCuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderVar = (DataGridView)sender;
            int idBuscar = 0;
            this.ltIdsEditar = new List<int>();

            try
            {
                if (e.ColumnIndex == this.dgvDispersadasCuentas.Columns["Seleccionar"].Index)
                {
                    var Check = (DataGridViewCheckBoxCell)this.dgvDispersadasCuentas.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if ((int)Check.Value == 0)
                    {
                        Check.Value = 1;
                    }
                    else
                    {
                        Check.Value = 0;
                    }
                }
                else if (e.ColumnIndex == this.dgvDispersadasCuentas.Columns["Editar"].Index)
                {
                    idBuscar = Convert.ToInt32(this.dgvDispersadasCuentas.Rows[e.RowIndex].Cells[0].Value.ToString());
                    ltIdsEditar.Add(idBuscar);
                    Configuracion.frmEligeEstado_FechaDispersion frmEligeDatos = new Configuracion.frmEligeEstado_FechaDispersion(this._plugin, this.ltIdsEditar);
                    frmEligeDatos.ShowDialog();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en Evento CellContentClick() ", ref ex);
            } 
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_plugin.BcoItauConnectionString);

            try
            {
                dbmIntegration.Connection_Open(_plugin.Manager.Sesion.Usuario.id);
                DataTable ReportCuentas_Dispersadas = dbmIntegration.SchemaBcoItau.PA_Select_Municipios_Dispersion.DBExecute(this._StrFechaRecaudo);
                EliminaColumnas(ReportCuentas_Dispersadas);
                _nombreReporte = string.Format(dbmIntegration.SchemaBcoItau.TBL_Config_Reporte.DBFindByNombre_Reporte("REPORTE_FORM_DISPERSION").FirstOrDefault().Formato_Reporte, DateTime.Now);

                Configuracion.frmElegirRuta Ruta = new Configuracion.frmElegirRuta();

                if (Ruta.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (new frmReportesImpuestos(this._plugin).Genera_ReporteExcel(Ruta.RutaGenerar, ".xlsx", _nombreReporte, ReportCuentas_Dispersadas, "Hoja1"))
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

        private void dgvDispersadasCuentas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            var CheckBox_se = (CheckBox)sender;
            int indexSeleccionar = this.dgvDispersadasCuentas.Columns["Seleccionar"].Index;

            if (CheckBox_se.Checked)
            {
                if (this.dgvDispersadasCuentas.Rows.Count == 0)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error, No hay regsitros para seleccionar!!", "Registros Seleccionar", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                    this.chkSeleccionarTodos.Checked = false;
                    return;
                }
                else
                {
                    foreach (DataGridViewRow itemRow in this.dgvDispersadasCuentas.Rows)
                    {
                        var Check = (DataGridViewCheckBoxCell)itemRow.Cells[indexSeleccionar];
                        if ((int)Check.Value == 0)
                        {
                            Check.Value = 1;
                        }
                        else
                        {
                            Check.Value = 0;
                        }
                    }
                }    
            }
            else
            {
                if (this.dgvDispersadasCuentas.Rows.Count > 0)
                {
                    foreach (DataGridViewRow itemRow in this.dgvDispersadasCuentas.Rows)
                    {
                        var Check = (DataGridViewCheckBoxCell)itemRow.Cells[indexSeleccionar];
                        Check.Value = 0;
                    }
                }
            }   
        }


        private void CargarGrilla()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoItauConnectionString);
            this.chkSeleccionarTodos.Checked = false;
            DataTable Dispersados = new DataTable();
            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);
                Dispersados = dbmIntegration.SchemaBcoItau.PA_Select_Municipios_Dispersion.DBExecute(this._StrFechaRecaudo);

                if (Dispersados.Rows.Count == 0)
                {
                    dbmIntegration.SchemaBcoItau.PA_Dispersion_Segunda.DBExecute(this._StrFechaRecaudo, false);
                    Dispersados = dbmIntegration.SchemaBcoItau.PA_Select_Municipios_Dispersion.DBExecute(this._StrFechaRecaudo);
                }


                if (Dispersados.Rows.Count > 0)
                {
                    this.dgvDispersadasCuentas.DataSource = Dispersados;

                    if (this.dgvDispersadasCuentas.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow itemRow in dgvDispersadasCuentas.Rows)
                        {
                            var ItemRowCell = itemRow.Cells["Seleccionar"];
                            ItemRowCell.Value = 0;
                        }
                    }
                }
                else
                {
                    this.dgvDispersadasCuentas.DataSource = null;
                    DesktopMessageBoxControl.DesktopMessageShow("No hay regsitros para esta Fecha!!!", "Registros", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                }

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error al Cargar grilla ", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void EliminaColumnas(DataTable ReportCuentas_Dispersadas)
        {
            if (ReportCuentas_Dispersadas.Columns.Count > 0)
            {
                if (ReportCuentas_Dispersadas.Columns.Contains("id_Cuenta_Dispersada"))
                {
                    ReportCuentas_Dispersadas.Columns.Remove("id_Cuenta_Dispersada");
                }

                if (ReportCuentas_Dispersadas.Columns.Contains("ID"))
                {
                    ReportCuentas_Dispersadas.Columns.Remove("ID");
                }

                if (ReportCuentas_Dispersadas.Columns.Contains("fk_Reporte_Publicacion"))
                {
                    ReportCuentas_Dispersadas.Columns.Remove("fk_Reporte_Publicacion");
                }

                if (ReportCuentas_Dispersadas.Columns.Contains("fk_Estado_Dispersion"))
                {
                    ReportCuentas_Dispersadas.Columns.Remove("fk_Estado_Dispersion");
                }

                if (ReportCuentas_Dispersadas.Columns.Contains("fk_Relacion_Cuenta_Municipio"))
                {
                    ReportCuentas_Dispersadas.Columns.Remove("fk_Relacion_Cuenta_Municipio");
                }
            }
        }



        
    }
}
