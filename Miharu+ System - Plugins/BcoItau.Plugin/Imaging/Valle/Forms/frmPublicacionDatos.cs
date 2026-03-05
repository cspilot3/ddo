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
using DBIntegration;

namespace BcoItau.Plugin.Imaging.Atlantico.Forms
{
    public partial class frmPublicacionDatos : Form
    {

        #region   "Declaraciones"
        private VallePlugin _PluginBcoBogota;
        private string strFechaProceso = "";
        private int Entidad;
        private int Proyecto;
        private List<string> ltErrores;
        private int ContadorProgress = 0;
        private int _cantidadPublicados = 0;
        #endregion

        #region "Constructor"
        public frmPublicacionDatos(VallePlugin _plugin)
        {
            InitializeComponent();
            this._PluginBcoBogota = _plugin;
        }
        #endregion

        #region "Eventos"
        private void frmPublicacionDatos_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnPublicarDatos;
            this.dpFechaProceso.MaxDate = DateTime.Now;
            this.ltErrores = new List<string>();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.backgroundWorkerPublicacion.CancelAsync();
            this.backgroundWorkerPublicacion.Dispose();
            this.Close();
        }

        private void backgroundWorkerPublicacion_DoWork(object sender, DoWorkEventArgs e)
        {
            PublicarDatos();
        }

        private void backgroundWorkerPublicacion_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pgPublicacionDatos.Value = e.ProgressPercentage;
            this.lblTitleProgress.Text = e.UserState.ToString()+": (" + e.UserState.ToString() + " %)";
        }

        private void btnPublicarDatos_Click(object sender, EventArgs e)
        {
            this.ContadorProgress = 0;
            this.pgPublicacionDatos.Style = ProgressBarStyle.Marquee;
            try
            {
                if (!this.backgroundWorkerPublicacion.IsBusy)
                {
                    this.ltErrores.Clear();
                    CheckForIllegalCrossThreadCalls = false;
                    InicializaVariables_Globales();
                    this.lblTitleProgress.Visible = true;
                    this.pgPublicacionDatos.Visible = true;
                    this.backgroundWorkerPublicacion.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("PublicarDatos", ref ex);
            }
        }

        private void backgroundWorkerPublicacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pgPublicacionDatos.Value = this.pgPublicacionDatos.Maximum;
            this.lblTitleProgress.Text = "Progreso General (" + this.pgPublicacionDatos.Maximum.ToString() + "%)";
            this.timerPublicacion.Stop();
            this.timerPublicacion.Enabled = false;

            if (this.ltErrores.Count > 0)
            {
                string strErrores = "";
                foreach (var item in this.ltErrores)
                {
                    strErrores = strErrores + " - " + item.ToString() + Environment.NewLine;
                }

                DesktopMessageBoxControl.DesktopMessageShow("Errores: " + strErrores, "Publicación con Errores", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                this.lblTitleProgress.Visible = false;
                this.pgPublicacionDatos.Visible = false;
            }
            else
            {
                DesktopMessageBoxControl.DesktopMessageShow("Publicación realizada Con Exito (Registros Publicados = "+this._cantidadPublicados.ToString()+") !!" ,"Publicación con Errores", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                this.lblTitleProgress.Visible = false;
                this.pgPublicacionDatos.Visible = false;
            }
        }

        private void timerPublicacion_Tick(object sender, EventArgs e)
        {
           this.ContadorProgress = ContadorProgress + 1;
           if (ContadorProgress <= 100)
           {
               this.backgroundWorkerPublicacion.ReportProgress(ContadorProgress, ContadorProgress.ToString());
               System.Threading.Thread.Sleep(2000);
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

        #region "Metodos"

        //Metodo para publicar datos en Tabla Reporte Integration
        private void PublicarDatos()
        {
            DBCoreDataBaseManager dbmCore = new DBCoreDataBaseManager(_PluginBcoBogota.Manager.DesktopGlobal.ConnectionStrings.Core);
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(_PluginBcoBogota.BcoItauConnectionString);
            int hayError = 0;
            int cantidadRegistros = 0;
            try
            {
                dbmCore.Connection_Open(_PluginBcoBogota.Manager.Sesion.Usuario.id);
                dbmIntegration.Connection_Open(_PluginBcoBogota.Manager.Sesion.Usuario.id);

                var dtResult = dbmIntegration.SchemaBcoItau.PA_Reporte_Municipios.DBExecute(this.Entidad, this.Proyecto, this.strFechaProceso, this._PluginBcoBogota.Manager.Sesion.Usuario.id,false);
                this.backgroundWorkerPublicacion.ReportProgress(0,"Realizando Poblado de Datos...Progreso General");

                if (dtResult.Rows.Count > 0)
                {
                    hayError = int.Parse(dtResult.Rows[0]["HayError"].ToString());
                    cantidadRegistros = int.Parse(dtResult.Rows[0]["Cantidad_Registros"].ToString());
                    this._cantidadPublicados = cantidadRegistros;
                }

                if (hayError == 1)
                {
                    this.ltErrores.Add(dtResult.Rows[0]["ERROR_MSJ"].ToString());
                    this.timerPublicacion.Enabled = false;
                    this.timerPublicacion.Stop();
                    this.backgroundWorkerPublicacion.CancelAsync();
                    return;
                }


                if(cantidadRegistros == 0)
                {
                    this.ltErrores.Add("No hay datos para esta Fecha de Proceso " + this.strFechaProceso);
                    this.timerPublicacion.Enabled = false;
                    this.timerPublicacion.Stop();
                    this.backgroundWorkerPublicacion.CancelAsync();
                }
                
            }
            catch (Exception ex)
            {
                this.ltErrores.Add(ex.Message);
                this.backgroundWorkerPublicacion.CancelAsync();
                this.timerPublicacion.Enabled = false;
                this.timerPublicacion.Stop();
            }
            finally
            {
                dbmCore.Connection_Close();
                dbmIntegration.Connection_Close();
            }
        }

        public void CruzarDatos()
        {
             DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;
            try
            {
                dbmIntegration  = new DBIntegration.DBIntegrationDataBaseManager(_PluginBcoBogota.BcoItauConnectionString);
                dbmIntegration.Connection_Open(_PluginBcoBogota.Manager.Sesion.Usuario.id);

                this.backgroundWorkerPublicacion.ReportProgress(0, "Realizando Cruzado de Datos...Progreso General");
                dbmIntegration.SchemaBcoItau.PA_Cruce.DBExecute(this.strFechaProceso, this._PluginBcoBogota.Manager.ImagingGlobal.Entidad, this._PluginBcoBogota.Manager.ImagingGlobal.Proyecto);
                
            }
            catch (Exception ex)
            {
                this.ltErrores.Add(ex.Message);
                this.backgroundWorkerPublicacion.CancelAsync();
                this.timerPublicacion.Enabled = false;
                this.timerPublicacion.Stop();
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        //Inicializa Variables globales
        private void InicializaVariables_Globales()
        {
            this.strFechaProceso = this.dpFechaProceso.Value.ToString("yyyy/MM/dd");
            this.Entidad = this._PluginBcoBogota.Manager.ImagingGlobal.Entidad;
            this.Proyecto = this._PluginBcoBogota.Manager.ImagingGlobal.Proyecto;
            this.timerPublicacion.Enabled = true;
        }


        #endregion

        

    }
}
