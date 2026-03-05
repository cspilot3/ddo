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


namespace BcoItau.Plugin.Imaging.Atlantico.Forms.Configuracion
{
    public partial class frmEligeEstado_FechaDispersion : Form
    {
        VallePlugin _Plugin;
        List<int> _ltIdEditar = new List<int>();
        private string _strFechaElegida = "";
        private int _estadoSeleccionado = 0;
        int intProgress = 0;
        string StrInformante = "";
        List<string> ltErrores = null;

        public frmEligeEstado_FechaDispersion(VallePlugin plugin, List<int> ltIdEditar)
        {
            this._ltIdEditar = ltIdEditar;
            this._Plugin = plugin;
            InitializeComponent();
        }

        private void frmEligeEstado_FechaDispersion_Load(object sender, EventArgs e)
        {
            this.dtpFechaDispersion.MaxDate = DateTime.Now;
            CheckForIllegalCrossThreadCalls = false;
            this.lblMsjProgress.BringToFront();
            DBIntegrationDataBaseManager dbmIntegation = new DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            try
            {
                dbmIntegation.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                var EstadosDispersion = dbmIntegation.SchemaBcoItau.TBL_Estados_Dispersion.DBGet(null);

                if (EstadosDispersion.Rows.Count > 0)
                {
                    this.cbEstadosDispersion.DisplayMember = EstadosDispersion.Columns["Nombre_Estado"].ToString();
                    this.cbEstadosDispersion.ValueMember = EstadosDispersion.Columns["id_Estados_Dispersion"].ToString();
                    this.cbEstadosDispersion.DataSource = EstadosDispersion;

                    this.cbEstadosDispersion.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en frmEligeEstado_FechaDispersion_Load()", ref ex);
            }
            finally
            {
                dbmIntegation.Connection_Close();
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!this.backgroundWorkerActualizando.IsBusy)
            {
                this.ltErrores = new List<string>();
                this.lblProgreso.Visible = true;
                this.lblInformante.Visible = true;
                this.pbActualizando.Maximum = this._ltIdEditar.Count;
                this.backgroundWorkerActualizando.RunWorkerAsync();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorkerActualizando_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!e.Cancel)
            {
                ActualizarRegistros();
            }
        }

        private void backgroundWorkerActualizando_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.intProgress = e.ProgressPercentage;
            this.StrInformante = e.UserState.ToString();

            if (StrInformante != "")
            {
                this.lblInformante.Text = StrInformante;
            }

            if (intProgress > 0)
            {
                float Divide = (float)intProgress / (float)this._ltIdEditar.Count;
                this.lblProgreso.Text = "Progreso (" + ((int)(Divide * 100)).ToString() + " %)";
            }
        }

        private void backgroundWorkerActualizando_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 

            if (this.ltErrores.Count == 0)
            {
                if (DesktopMessageBoxControl.DesktopMessageShow("Registros Actualizados con Exito!!!", "Actualizados", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true) == System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                string ErrorStr ="";
                foreach (var itemStr in this.ltErrores)
	            {
		            ErrorStr = itemStr + Environment.NewLine;
	            }

                DesktopMessageBoxControl.DesktopMessageShow("Se presentarion errores, " + ErrorStr, "Actualización con Errores", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }

            
        }

        private void ActualizarRegistros()
        {
            this._estadoSeleccionado = (int)this.cbEstadosDispersion.SelectedValue;
            this._strFechaElegida = this.dtpFechaDispersion.Value.ToString("yyyy/MM/dd");
            int contadorUpdate = 0;
            this.backgroundWorkerActualizando.ReportProgress(0, "Actualizando Cuentas Dispersión...");
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            this.Cursor = Cursors.WaitCursor;
            CheckForIllegalCrossThreadCalls = false;

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                if (this._ltIdEditar.Count > 0)
                {
                    DBIntegration.SchemaBcoItau.TBL_Relacion_Municipios_DispersionType rowUpdate = null;

                    foreach (var itemIds in this._ltIdEditar)
                    {
                        contadorUpdate++;
                        this.pbActualizando.Value = contadorUpdate;
                        rowUpdate = new DBIntegration.SchemaBcoItau.TBL_Relacion_Municipios_DispersionType();
                        rowUpdate.fk_Estado_Dispersion = this._estadoSeleccionado;
                        rowUpdate.Fecha_Dispersion = Convert.ToDateTime(this._strFechaElegida);
                        dbmIntegration.SchemaBcoItau.TBL_Relacion_Municipios_Dispersion.DBUpdate(rowUpdate, itemIds);
                        this.backgroundWorkerActualizando.ReportProgress(contadorUpdate, "Actualizando Cuentas Dispersión, con RegistroId = " + itemIds.ToString());
                        System.Threading.Thread.Sleep(2000);
                    }
                }

                this.lblMsjProgress.Text = "";
                this.Cursor = Cursors.Default;

                

            }
            catch (Exception ex)
            {
                this.ltErrores.Add("Error en btnAceptar_Click() "+ex.Message);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }
    }
}
