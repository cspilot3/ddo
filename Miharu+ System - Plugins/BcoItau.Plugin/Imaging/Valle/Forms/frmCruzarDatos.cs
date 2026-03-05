using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore;
using Imaging.Valle;
using Miharu.Desktop.Controls.DesktopMessageBox;
using Miharu.Desktop.Library.Config;


namespace BcoItau.Plugin.Imaging.Valle.Forms
{
    public partial class frmCruzarDatos : Form
    {

        #region   "Declaraciones"
        private VallePlugin _PluginBcoBogota;
        private string strFechaProceso = "";
        //private int Entidad;
        //private int Proyecto;
        private List<string> ltErrores = new List<string>();
        //private int ContadorProgress = 0;
        private string _tipoCruce = "";
        private string strMsj = "";
        #endregion

        public frmCruzarDatos(VallePlugin _plugin, string TipoCruce)
        {
            this._tipoCruce = TipoCruce;
            InitializeComponent();
            this._PluginBcoBogota = _plugin;
        }

        private void backgroundWorkerCruceDatos_DoWork(object sender, DoWorkEventArgs e)
        {
            CruzarDatos();
        }

        private void backgroundWorkerCruceDatos_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pbCruceDatos.Value = e.ProgressPercentage;
            this.lblTitleProgress.Text = e.UserState.ToString() + ": (" + this.pbCruceDatos.Value.ToString() + " %)";
        }

        private void backgroundWorkerCruceDatos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pbCruceDatos.Value = this.pbCruceDatos.Maximum;
            this.lblTitleProgress.Text = "Progreso General (" + this.pbCruceDatos.Maximum.ToString() + "%)";
            this.pbCruceDatos.Visible = false;

            if (this.ltErrores.Count > 0)
            {
                string strErrores = "";
                foreach (var item in this.ltErrores)
                {
                    strErrores = strErrores + " - " + item.ToString() + Environment.NewLine;
                }

                DesktopMessageBoxControl.DesktopMessageShow("Errores: " + strErrores, "Publicación con Errores", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                this.lblTitleProgress.Visible = false;
                this.pbCruceDatos.Visible = false;
            }
            else
            {
                DesktopMessageBoxControl.DesktopMessageShow(this.strMsj, "Publicación con Errores", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                this.lblTitleProgress.Visible = false;
                this.pbCruceDatos.Visible = false;
            }
        }

        private void btnCruzarDatos_Click(object sender, EventArgs e)
        {
            this.pbCruceDatos.Enabled = true;
            this.strFechaProceso = this.dtpCruceDatos.Value.ToString("yyyy/MM/dd");

            if (!this.backgroundWorkerCruceDatos.IsBusy)
            {
                this.backgroundWorkerCruceDatos.RunWorkerAsync();
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCruzarDatos_Load(object sender, EventArgs e)
        {
            this.dtpCruceDatos.MaxDate = DateTime.Now;
            
            if (this._tipoCruce == "MEDIO_PAGO")
            {
                this.lblFechaProceso.Text = "Fecha de Recaudo";
            }
            else
                this.lblFechaProceso.Text = "Fecha de Proceso";

            CheckForIllegalCrossThreadCalls = false;
            this.pbCruceDatos.Enabled = false;
            this.pbCruceDatos.Visible = false;
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

        #region "Metodos"

        public void CruzarDatos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            this.strMsj = "";

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(_PluginBcoBogota.BcoItauConnectionString);
                dbmIntegration.Connection_Open(_PluginBcoBogota.Manager.Sesion.Usuario.id);                

                //this.backgroundWorkerCruceDatos.ReportProgress(0, "Progreso General");
                this.pbCruceDatos.Visible = true;
                DataTable ResultadoCruceFormulario = null;
                DataTable ResultadoCruceMedioPago = null;
                DataTable ResultadoCruceValor = null;


                //if (Validacruce(dbmIntegration) == true)
                //{

                    if (this._tipoCruce == "MEDIO_PAGO")
                    {
                        //TODO: Validar cantidad de tarjetas capturadas Vs Cantidad de tarjetas Cargadas en la fecha de Recaudo.
                        // TODO: Para ejecutar este cruce es necesario haber cargado el log de cuadre Diario
                        // TODO: Para ejecutar este cruce es necesario haber cargado el log de Vehiculo
                        this.lblTitleProgress.Text = "Ejecutando Cruce por Medio de Pago.....";
                        ResultadoCruceMedioPago = dbmIntegration.SchemaBcoItau.PA_Cruce_Medio_Pago.DBExecute(this.strFechaProceso);

                        if (ResultadoCruceMedioPago.Rows.Count > 0)
                            strMsj = ResultadoCruceMedioPago.Rows[0]["ERROR_MSJ"].ToString();
                    }
                    else
                    {
                        // TODO: Para ejecutar este cruce es necesario haber cargado el log de Vehiculo
                        this.lblTitleProgress.Text = "Ejecutando Cruce por Formulario.....";
                        ResultadoCruceFormulario = dbmIntegration.SchemaBcoItau.PA_Cruce.DBExecute(this.strFechaProceso, this._PluginBcoBogota.Manager.ImagingGlobal.Entidad, this._PluginBcoBogota.Manager.ImagingGlobal.Proyecto);

                        if (ResultadoCruceFormulario.Rows.Count > 0)
                            strMsj =  ResultadoCruceFormulario.Rows[0]["ERROR_MSJ"].ToString();

                        this.lblTitleProgress.Text = "Ejecutando Cruce por Valor.....";
                        ResultadoCruceValor = dbmIntegration.SchemaBcoItau.PA_Cruce_Por_Valor .DBExecute(this.strFechaProceso);

                        if (ResultadoCruceValor.Rows.Count > 0)
                        {
                            if (strMsj == "")
                            {
                                strMsj = "Cruce por Valor: " + ResultadoCruceValor.Rows[0]["ERROR_MSJ"].ToString();
                            }
                            else
                            {
                                strMsj = strMsj + Environment.NewLine + ResultadoCruceValor.Rows[0]["ERROR_MSJ"].ToString();
                            }
                        }

                    }

                //}
                //else
                //{
                //    strMsj = "Debe Realizar el Cargue del log de Vehículo para realizar el Cruce";
                //}

            }
            catch (Exception ex)
            {
                this.ltErrores.Add(ex.Message);
                this.backgroundWorkerCruceDatos.CancelAsync();;
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        

        //private bool Validacruce(DBIntegration.DBIntegrationDataBaseManager dbmIntregation)
        //{

        //    var logVehiculoRow = (DBIntegration.SchemaConfig.TBL_Tipos_LogRow)dbmIntregation.SchemaConfig.TBL_Tipos_Log.DBFindByNombre_Tipo_Logfk_Entidadfk_Proyecto("VEHÍCULO", _PluginBcoBogota.Manager.ImagingGlobal.Entidad, _PluginBcoBogota.Manager.ImagingGlobal.Proyecto).Rows[0];

        //    //Control del Cargue Log Vehiculo.
        //    if (!(dbmIntregation.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogFecha_ProcesoValido(_PluginBcoBogota.Manager.ImagingGlobal.Entidad, _PluginBcoBogota.Manager.ImagingGlobal.Proyecto, logVehiculoRow.id_Tipo_log, this.dtpCruceDatos.Value.ToString("yyyyMMdd"), true).Rows.Count > 0))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        #endregion
    }
}
