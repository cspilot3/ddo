using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Desktop.Controls.DesktopMessageBox;
using System.IO;
using Imaging.Valle;
using DBIntegration;
using DBImaging;
using Miharu.Desktop.Library.Config;
using System.Globalization;
using Miharu.Desktop.Library;
using System.Diagnostics;

namespace BcoItau.Plugin.Imaging.Valle.Forms
{
    public partial class frmCargueLog : Form
    {
        #region Declaraciones
        private Stream _File = null;
        private int Segundos = 0;
        private int Minuto = 0;
        private int Hora = 0;
        private VallePlugin _Plugin;
        private int Entidad;
        private int Proyecto;
        private int fk_Tipo_log;
        private DateTime _fechaProceso;
        private DesktopConfig.TypeResult trResultado;
        bool CargueValido = false;
        private int _DataRegistros = 0;
        private int _DataColumnas = 0;
        private short _EstadoProceso = 0;//0 Validando, 1 Procesando
        private bool _validaExtension = false;
        private int SaltoPrimasLineas = 0;
        private DataTable _DataFile = null;
        ProcesaCargue_BcoBogota ProcesaLog = new ProcesaCargue_BcoBogota();
        private int _idCargue = 0;
        //private string Fecha_Recaudo = null;
        private bool validaArchivoCargue;
        private string passwordFile = null;
        private XLSData objXLS = new XLSData();
        private string ExtensionGlobal = null;
        #endregion

        #region Constructor
        public frmCargueLog(VallePlugin _plugin)
        {
            this._Plugin = _plugin;
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frmCargueLog_Load(object sender, EventArgs e)
        {
            this.dtpFechaProceso.MaxDate = DateTime.Now;
            this.Entidad = this._Plugin.Manager.ImagingGlobal.Entidad;
            this.Proyecto = this._Plugin.Manager.ImagingGlobal.Proyecto;
            this.CargarCombos();
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

        private void CargarButton_Click(object sender, EventArgs e)
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);

            if (this.ArchivoDesktopTextBox.Text == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Archivo a cargar Vacio!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                return;
            }

            
            string strSeleccionado = this.DesktopComboBoxControlTiposLog.Text;
            bool validaExtension = false;
            string Extension = "";
            string ExtensionBD = "";
//            this.Fecha_Recaudo = "";
            this.validaArchivoCargue = false;

            try
            {
                if (this._File != null)
                {
                    string NombreArchivo = Path.GetFileName(((FileStream)_File).Name);



                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                    if ((_File != null))
                    {

                        this.fk_Tipo_log = Convert.ToInt32(this.DesktopComboBoxControlTiposLog.SelectedValue);
                        var TipoLogType = dbmIntegration.SchemaConfig.TBL_Tipos_Log.DBGet(fk_Tipo_log, Convert.ToInt32(_Plugin.Manager.ImagingGlobal.Entidad), Convert.ToInt32(_Plugin.Manager.ImagingGlobal.Proyecto));
                        validaExtension = (bool)TipoLogType.Rows[0]["Valida_Extension"];
                        ExtensionBD = TipoLogType.Rows[0]["Extension_Archivo"].ToString();
                        this.validaArchivoCargue = (bool)TipoLogType.Rows[0]["Valida_ArchivoCargue"];
                        DBIntegration.SchemaConfig.TBL_Tipos_LogRow RowTipoLog = (DBIntegration.SchemaConfig.TBL_Tipos_LogRow)TipoLogType.Rows[0];
                        this.chkEncabezado.Checked = (bool)TipoLogType.Rows[0]["ManejaEncabezado"];

                        if (!string.IsNullOrEmpty(TipoLogType.Rows[0]["Salto_Primeras_Lineas"].ToString()))
                        {
                            this.SaltoPrimasLineas = Convert.ToInt32(TipoLogType.Rows[0]["Salto_Primeras_Lineas"].ToString());
                        }


                        if (validaExtension)
                        {
                            Extension = NombreArchivo.Substring(NombreArchivo.IndexOf("."));

                            if (Extension.ToString() == "")
                            {
                                DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                                return;
                            }
                            else
                            {
                                string[] StrSplit = ExtensionBD.Split('|');
                                var encontradoSplit = StrSplit.Where(x => x.ToString().ToUpper() == Extension.ToUpper()).Select(x => x.ToString());

                                if (encontradoSplit.Count() == 0)
                                {
                                    DesktopMessageBoxControl.DesktopMessageShow("La extensión del archivo no es correcta para este Tipo de Log, por favor seleccione otro archivo de cargue.", "Error de Extensión", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                                    return;
                                }

                                this.ExtensionGlobal = Extension;
                            }
                        }


                        if (ValidaFormatoCorrecto_Log(this.fk_Tipo_log, NombreArchivo, RowTipoLog) == false)
                        {
                            DesktopMessageBoxControl.DesktopMessageShow("El formato del archivo no es correcto, por favor verifique y cargue nuevamente!!", "Format Archivo de cargue", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                            return;
                        }

                        ValidarExiste_ArchivoCargue(NombreArchivo, dbmIntegration);
                    }
                }
                else
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, intentelo nuevamente!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                    this._File = null;
                    this.ArchivoDesktopTextBox.Text = "";
                    return;
                }

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("CargarButton_Click", ref ex);
                this._File = null;
                this.ArchivoDesktopTextBox.Text = "";
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void ArchivoDesktopTextBox_Click(object sender, EventArgs e)
        {
            BuscarArchivo();
        }

        private void BuscarArchivoButton_Click(object sender, EventArgs e)
        {
            BuscarArchivo();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (Timer1.Enabled)
            {
                Segundos += 1;

                if (Segundos == 60)
                {
                    Segundos = 0;
                    Minuto += 1;
                }

                if (Minuto == 60)
                {
                    Minuto = 0;
                    Hora += 1;
                }

                TiempoLabel.Text = string.Format("{0:00}", Hora) + ":" + string.Format("{0:00}", Minuto) + ":" + string.Format("{0:00}", Segundos);
            }
        }

        private void CargueBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            if (worker.CancellationPending)
                e.Cancel = true;

            //Se obtiene entidad, esquema
            dynamic Parametros = e.Argument.ToString().Split(Convert.ToChar("-"));
            string NombreArchivo = Parametros[0];


            trResultado = CargarArchivo(NombreArchivo, this.Entidad, this.Proyecto, this.fk_Tipo_log);
            if ((trResultado.Result))
            {
                CargueValido = true;
                this.CargueBackgroundWorker.ReportProgress(CargueProgressBar.Maximum);
            }
            else
            {
                CargueValido = false;
                this.CargueBackgroundWorker.CancelAsync();
            }
        }


        private void CargueBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (e.ProgressPercentage == 0)
            {
                CargandoPictureBox.Visible = true;
                CargueProgressBar.Maximum = _DataRegistros;
                TotalRegistrosLabel.Text = _DataRegistros.ToString();

                if (_EstadoProceso == 0)
                {
                    ProcesadosTituloLabel.Text = "Validados:";
                }
                else if (_EstadoProceso == 1)
                {
                    ProcesadosTituloLabel.Text = "Procesados:";
                }
                _EstadoProceso = Convert.ToInt16(_EstadoProceso + 1);
            }
            else
            {
                //Inicio Proceso
                if ((e.ProgressPercentage < 100))
                {
                    CargueProgressBar.Value = e.ProgressPercentage;
                }
                ProcesadosLabel.Text = e.ProgressPercentage.ToString();
            }
        }

        private void CargueBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CargandoPictureBox.Visible = false;
            Timer1.Enabled = false;
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
            ArchivoDesktopTextBox.Text = "";

            try {
	            if (this.CargueValido) {
		            DatosCargadosDesktopDataGridView.DataSource = _DataFile;
		            CargueProgressBar.Value = CargueProgressBar.Maximum;
		            var strMsjFinal = "Datos cargados éxitosamente";
		            DesktopMessageBoxControl.DesktopMessageShow(strMsjFinal, "Cargue de datos", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
		            HabilitarControles(true);
		            TiempoLabel.Text = "00:00:00";
		            CargueProgressBar.Value = 0;
		            DatosCargadosDesktopDataGridView.DataSource = null;
		            _DataFile = null;
		            TotalRegistrosLabel.Text = "0";
		            ProcesadosLabel.Text = "0";
		            this._DataRegistros = 0;
		            this._DataColumnas = 0;
	            } else {
		            if ((trResultado.Parameters != null)) {
			            var Cargue_Log_Detalle = new DBIntegration.SchemaConfig.TBL_Cargue_Log_DetalleType();
			            Cargue_Log_Detalle.fk_Cargue = this._idCargue;
			            Cargue_Log_Detalle.Fecha_Cargue = null;
			            Cargue_Log_Detalle.fk_Usuario_Log = _Plugin.Manager.Sesion.Usuario.id;
			            var var = dbmIntegration.SchemaConfig.TBL_Cargue.DBGet(this._idCargue);
			            if ((var.Rows.Count > 0)) {
				            Cargue_Log_Detalle.Fecha_Cargue = Convert.ToDateTime(var.Rows[0]["Fecha_Log"]);
			            } else {
				            Cargue_Log_Detalle.Fecha_Cargue = null;
			            }

			            foreach (var itemError_loopVariable in trResultado.Parameters) {
                            Cargue_Log_Detalle.Descripcion_Error = itemError_loopVariable;
				            dbmIntegration.SchemaConfig.TBL_Cargue_Log_Detalle.DBInsert(Cargue_Log_Detalle);
			            }
			            FormResultadoValidacion myError = new FormResultadoValidacion(trResultado);
			            CargueBackgroundWorker.Dispose();
			            myError.ShowDialog();
			            HabilitarControles(true);
			            TiempoLabel.Text = "00:00:00";
			            CargueProgressBar.Value = 0;
			            DatosCargadosDesktopDataGridView.DataSource = null;
			            _DataFile = null;
			            TotalRegistrosLabel.Text = "0";
			            ProcesadosLabel.Text = "0";
			            this._DataRegistros = 0;
			            this._DataColumnas = 0;
		            }
	            }
	            _File = null;
            } catch (Exception ex) {
	            DesktopMessageBoxControl.DesktopMessageShow("Insertar Cargue (Evento:CargueBackgroundWorker_RunWorkerCompleted)  ", ref ex);
            } finally {
	            dbmIntegration.Connection_Close();
            }
        }
        #endregion

        #region Metodos

        private DesktopConfig.TypeResult CargarArchivo(string NombreArchivo, int Entidad,  int Proyecto, int IdTipo_log)
        {
            DesktopConfig.TypeResult trReturn = new DesktopConfig.TypeResult();
            trReturn.Result = true;

            switch (this.DesktopComboBoxControlTiposLog.Text)
            {
                case "RBLISRDO":
                    return CargueRBLISRDO(NombreArchivo, Entidad, Proyecto, IdTipo_log);
                case "CUADRE DIARIO":
                    return Cargue_CuadreDiario(NombreArchivo, Entidad, Proyecto, IdTipo_log);
                case "CUADRE DIARIO V2":
                    return Cargue_CuadreDiariov2(NombreArchivo, Entidad, Proyecto, IdTipo_log);
                case "VEHÍCULO":
                    return Cargue_Vehiculo(NombreArchivo, Entidad, Proyecto, IdTipo_log);
            }

            return trReturn;
        }

        private DesktopConfig.TypeResult Cargue_Vehiculo(string NombreArchivo, int Entidad, int Proyecto, int IdTipo_log)
        {
            DesktopConfig.TypeResult trReturn = new DesktopConfig.TypeResult();
            DBIntegrationDataBaseManager dbmIntregation = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);

            trReturn.Result = true;
            var lisMsgError = new List<string>();
            DataTable dtRef = null;
            DataSet dsref = new DataSet();
            List<string> ltCamposLog = new List<string>();
            int contador = 0;
            _DataFile = new DataTable();


            try
            {
                //Se valida el tipo de archivo, si es CSV,TXT o XLS
                CargaArchivoExcel(true, ref dtRef, ref dsref);
                KillSpecificProcess("EXCEL");

                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);


                DBIntegration.SchemaConfig.TBL_Campos_Tipo_logDataTable CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(false, this.fk_Tipo_log, Entidad, Proyecto, null);
                var ColumnsAdicionalesLogData = dbmIntregation.SchemaBcoItau.TBL_Log_Data.DBGet(0).Columns;

                foreach (DataColumn itemCampo in ColumnsAdicionalesLogData)
                {
                    _DataFile.Columns.Add(itemCampo.ColumnName);
                }
                _DataFile.Columns.RemoveAt(0);

                if (dsref.Tables.Count > 0)
                {
                    dtRef = dsref.Tables[0];

                    if ((dtRef.Rows.Count > 0))
                    {
                        for (int index = 0; index <= dtRef.Rows.Count - 1; index++)
                        {
                            foreach (var itemColumCampo in ColumnsAdicionalesLogData)
                            {
                                if ((itemColumCampo.ToString().ToUpper() != "ID_LOG_DATA"))
                                {
                                    if ((itemColumCampo.ToString().ToUpper().Contains("CAMPO_")))
                                    {
                                        var campoBuscar = itemColumCampo.ToString();
                                        var ax_encontrado = CamposPorTipoLog.Where(x => x.Nombre_Campo == campoBuscar.ToString()).ToList();
                                        DBIntegration.SchemaConfig.TBL_Campos_Tipo_logRow campoEncontrado = null;

                                        if ((ax_encontrado.Count > 0))
                                        {
                                            campoEncontrado = ax_encontrado.First();
                                            var valorCampo = dtRef.Rows[index][campoEncontrado.Descripcion].ToString();

                                            ltCamposLog.Add(valorCampo);
                                        }
                                        else
                                        {
                                            ltCamposLog.Add("");
                                        }
                                    }
                                    else
                                    {
                                        ltCamposLog.Add("");
                                    }
                                }
                            }
                            _DataFile.Rows.Add(ltCamposLog.ToArray());
                            _DataFile.Rows[contador]["fk_Entidad"] = this._Plugin.Manager.ImagingGlobal.Entidad;
                            _DataFile.Rows[contador]["fk_Proyecto"] = this._Plugin.Manager.ImagingGlobal.Proyecto;
                            _DataFile.Rows[contador]["fk_Tipo_Log"] = this.fk_Tipo_log;
                            _DataFile.Rows[contador]["Fecha_Proceso"] = this.dtpFechaProceso.Value.ToString("yyyyMMdd");
                            _DataFile.Rows[contador]["Fecha_Recaudo"] = this.dtpFechaRecaudo.Value.ToString("yyyy/MM/dd");
                            contador += 1;
                            ltCamposLog.Clear();
                        }
                    }

                    _DataRegistros = _DataFile.Rows.Count;
                    _DataColumnas = _DataFile.Columns.Count;

                    var NombreCampo = CamposPorTipoLog.Select("Descripcion = 'Forma de pago'")[0]["Nombre_Campo"].ToString();

                    var FormulariosTCLog = _DataFile.Select(NombreCampo + " = '26'").Count().ToString();

                                       
                    //TODO: Mover al Cruce Medio de Pago



                    //if (FormulariosTCLog == FormulariosTCProceso)
                    //{
                    DBIntegration.SchemaBcoItau.TBL_Log_DataType Data_Log = new DBIntegration.SchemaBcoItau.TBL_Log_DataType();
                    Data_Log.Fecha_Proceso = this._fechaProceso.ToString("yyyyMMdd");
                    Data_Log.fk_Tipo_log = this.fk_Tipo_log;
                    bool valida = false;


                    ProcesaLog.ProcesaLog_BcoBogota(ref _DataFile, Path.GetFileName(((FileStream)_File).Name), ref this.CargueBackgroundWorker, this._Plugin, ref this._idCargue, Data_Log, valida, "", this.dtpFechaProceso.Value.ToString("yyyyMMdd"), ref trReturn);
                    //}
                    //else
                    //{
                    //    trReturn.Result = false;
                    //    lisMsgError.Add("No se pudo cargar el archivo: La cantidad de registros de tarjeta de credito procesados (" + FormulariosTCProceso + ") es diferente a la cantidad del Log que intenta cargar(" + FormulariosTCLog + ")");
                    //    trReturn.Parameters = lisMsgError; 
                    //}

                }
                else
                {
                    trReturn.Result = false;
                    lisMsgError.Add("No se pudo cargar el archivo o está vacío.");
                    trReturn.Parameters = lisMsgError; 
                }
            }
            catch (Exception ex)
            {
                lisMsgError.Add("- Error: " + ex.Message);
                trReturn.Result = false;
                trReturn.Parameters = lisMsgError;
                KillSpecificProcess("EXCEL");
            }
            finally
            {
                dbmIntregation.Connection_Close();
                KillSpecificProcess("EXCEL");
            }

            return trReturn;
        }

        private DesktopConfig.TypeResult Cargue_CuadreDiario(string NombreArchivo, int Entidad, int Proyecto, int IdTipo_log)
        {
            DesktopConfig.TypeResult trReturn = new DesktopConfig.TypeResult();
            DBIntegrationDataBaseManager dbmIntregation = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);
            trReturn.Result = true;
            var lisMsgError = new List<string>();
            DataTable dtRef = null;
            DataSet dsref = new DataSet();
            int MaxNumCamposLog_BcoBogota = 0;
            List<string> ltCamposLog = new List<string>();
            int contador = 0;
            _DataFile = new DataTable();           
            int CantidadFormularios = 0;
            long SumTotal = 0;
            long SumComision = 0;
            long SumNeto = 0;
            int TotalRecaudoTarjetaa_Hoja1 = -1;   
            long ValorCupones_Hoja1 = -1;
            long ValorDiferencia_Hoja1 = -1;
            string StrFechaRecaudo = "";

            try
            {
                //Se valida el tipo de archivo, si es CSV,TXT o XLS
                CargaArchivoExcel(true, ref dtRef, ref dsref);
                KillSpecificProcess("EXCEL");

                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);

                var MaxNumCamposLog = dbmIntregation.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "MaxNumero_CamposLog_BcoBogota");
                if ((MaxNumCamposLog.Rows.Count > 0))
                {
                    MaxNumCamposLog_BcoBogota = Convert.ToInt32(MaxNumCamposLog.Rows[0]["Valor_Parametro_Sistema"].ToString());
                }

                DBIntegration.SchemaConfig.TBL_Campos_Tipo_logDataTable CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(false, this.fk_Tipo_log, Entidad, Proyecto, null);
                var ColumnsAdicionalesLogData = dbmIntregation.SchemaBcoItau.TBL_Log_Data.DBGet(0).Columns;

                foreach (DataColumn itemCampo in ColumnsAdicionalesLogData)
                {
                    _DataFile.Columns.Add(itemCampo.ColumnName);
                }
                _DataFile.Columns.RemoveAt(0);

                if ((this.ExtensionGlobal.ToString().ToLower().Contains(".xls")))
                { //.EXCEL

                    for (int i = 0; i <= dsref.Tables.Count - 1; i++)
                    {
                        dtRef = dsref.Tables[i];

                        if ((dtRef.Rows.Count > 0) && dtRef.TableName.ToUpper().Contains("INFORMACION DE VTA CON TARJETAS$"))
                        {
                            for (int index = 0; index <= dtRef.Rows.Count - 2; index++) //Menos 2 por totales al final
                            {
                                foreach (var itemColumCampo in ColumnsAdicionalesLogData)
                                {
                                    if ((itemColumCampo.ToString().ToUpper() != "ID_LOG_DATA"))
                                    {
                                        if ((itemColumCampo.ToString().ToUpper().Contains("CAMPO_")))
                                        {
                                            var campoBuscar = itemColumCampo.ToString();
                                            var ax_encontrado = CamposPorTipoLog.Where(x => x.Nombre_Campo == campoBuscar.ToString()).ToList();
                                            DBIntegration.SchemaConfig.TBL_Campos_Tipo_logRow campoEncontrado = null;

                                            if ((ax_encontrado.Count > 0))
                                            {
                                                campoEncontrado = ax_encontrado.First();
                                                var valorCampo = dtRef.Rows[index][campoEncontrado.Descripcion].ToString();

                                                switch (campoEncontrado.Descripcion)
                                                {
                                                    case "No#":
                                                        CantidadFormularios++;
                                                        break;
                                                    case "TOTAL":
                                                        SumTotal = SumTotal + Convert.ToInt64(valorCampo);
                                                        break;
                                                    case "COMISION":
                                                        SumComision = SumComision + Convert.ToInt64(valorCampo);
                                                        break;
                                                    case "NETO":
                                                        SumNeto = SumNeto + Convert.ToInt64(valorCampo);
                                                        break;
                                                    case "FEC_CONSI":
                                                        valorCampo = Convert.ToDateTime(valorCampo).ToString("yyyy/MM/dd");
                                                        break;
                                                    case "FEC_TR":
                                                        valorCampo = Convert.ToDateTime(valorCampo).ToString("yyyy/MM/dd");
                                                        StrFechaRecaudo = valorCampo;
                                                        break;
 
                                                }

                                                ltCamposLog.Add(valorCampo);
                                            }
                                            else
                                            {
                                                ltCamposLog.Add("");
                                            }
                                        }
                                        else
                                        {
                                            ltCamposLog.Add("");
                                        }
                                    }
                                }
                                _DataFile.Rows.Add(ltCamposLog.ToArray());
                                _DataFile.Rows[contador]["fk_Entidad"] = this._Plugin.Manager.ImagingGlobal.Entidad;
                                _DataFile.Rows[contador]["fk_Proyecto"] = this._Plugin.Manager.ImagingGlobal.Proyecto;
                                _DataFile.Rows[contador]["fk_Tipo_Log"] = this.fk_Tipo_log;
                                _DataFile.Rows[contador]["Fecha_Proceso"] = this.dtpFechaProceso.Value.ToString("yyyyMMdd");
                                _DataFile.Rows[contador]["Fecha_Recaudo"] = StrFechaRecaudo;
                                contador += 1;
                                ltCamposLog.Clear();
                            }
                        }
                        else
                        {
                            if ((dtRef.Rows.Count > 0) && dtRef.TableName.ToUpper() != "INFORMACION DE VTA CON TARJETAS$")
                            {
                                for (int j = 0; j <= dtRef.Rows.Count - 1; j++)
                                {
                                    for (int k = 0; k <= dtRef.Columns.Count - 1; k++)
                                    {
                                        var ValorBuscar = dtRef.Rows[j][k].ToString();
                                        if (ValorBuscar.ToUpper() == "RECAUDO EN TARJETAS" && TotalRecaudoTarjetaa_Hoja1 == -1 && ValorCupones_Hoja1 == -1)
                                        {
                                            TotalRecaudoTarjetaa_Hoja1 = Convert.ToInt32(dtRef.Rows[j][k + 1].ToString());
                                            ValorCupones_Hoja1 = Convert.ToInt64(dtRef.Rows[j][k + 2].ToString());
                                        }
                                        else if (ValorBuscar.ToUpper() == "DIFERENCIA" && ValorDiferencia_Hoja1 == -1)
                                        {
                                            ValorDiferencia_Hoja1 = Convert.ToInt64(dtRef.Rows[j][k + 1].ToString());
                                        }

                                        if (TotalRecaudoTarjetaa_Hoja1 != -1 && ValorCupones_Hoja1 != -1 && ValorDiferencia_Hoja1 != -1)
                                        {
                                            break;
                                        }
                                    }
                                    if (TotalRecaudoTarjetaa_Hoja1 != -1 && ValorCupones_Hoja1 != -1 && ValorDiferencia_Hoja1 != -1)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                    }

                    if (CantidadFormularios != TotalRecaudoTarjetaa_Hoja1)
                    {
                        trReturn.Result = false;
                        lisMsgError.Add("la Cantidad de Cupones de la Hoja1 ("+TotalRecaudoTarjetaa_Hoja1.ToString()+") no coincide con los de la Hoja2 ("+CantidadFormularios.ToString()+")");
                        trReturn.Parameters = lisMsgError; 
                    }

                    if (SumTotal != ValorCupones_Hoja1)
                    {
                        trReturn.Result = false;
                        lisMsgError.Add("El valor Total de Cupones de la Hoja1 ("+ValorCupones_Hoja1.ToString()+") no coincide con el ValorTotal de la Hoja2 ("+SumTotal.ToString()+")");
                        trReturn.Parameters = lisMsgError;
                    }

                    if (Math.Abs(SumComision) != Math.Abs(ValorDiferencia_Hoja1))
                    {
                        trReturn.Result = false;
                        lisMsgError.Add("La Diferencia de la Hoja1 ("+ValorDiferencia_Hoja1.ToString()+") no coincide con el Total Comision de la Hoja2 ("+SumComision.ToString()+")");
                        trReturn.Parameters = lisMsgError;
                    }

                    if(lisMsgError.Count > 0)
                        goto Salida;

                    _DataRegistros = _DataFile.Rows.Count;
                    _DataColumnas = _DataFile.Columns.Count;

                    DBIntegration.SchemaBcoItau.TBL_Log_DataType Data_Log = new DBIntegration.SchemaBcoItau.TBL_Log_DataType();
                    Data_Log.Fecha_Proceso = this._fechaProceso.ToString("yyyyMMdd");
                    Data_Log.fk_Tipo_log = this.fk_Tipo_log;
                    bool valida = false;
                    ProcesaLog.ProcesaLog_BcoBogota(ref _DataFile, Path.GetFileName(((FileStream)_File).Name), ref this.CargueBackgroundWorker, this._Plugin, ref this._idCargue, Data_Log, valida, "", this.dtpFechaProceso.Value.ToString("yyyyMMdd"), ref trReturn);   
                }

            }

            catch (Exception ex)
            {
                lisMsgError.Add("- Error: " + ex.Message);
                trReturn.Result = false;
                trReturn.Parameters = lisMsgError;
                KillSpecificProcess("EXCEL");
            }
            finally
            {
                dbmIntregation.Connection_Close();
                KillSpecificProcess("EXCEL");
            }

            Salida:
            return trReturn;
        }


        private DesktopConfig.TypeResult Cargue_CuadreDiariov2(string NombreArchivo, int Entidad, int Proyecto, int IdTipo_log)
        {
            DesktopConfig.TypeResult trReturn = new DesktopConfig.TypeResult();
            DBIntegrationDataBaseManager dbmIntregation = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);
            trReturn.Result = true;
            var lisMsgError = new List<string>();
            DataTable dtRef = null;
            DataSet dsref = new DataSet();
            int MaxNumCamposLog_BcoBogota = 0;
            List<string> ltCamposLog = new List<string>();
            _DataFile = new DataTable();
            double TotalRecaudoTarjetaa_Hoja1 = -1;
            int CantidadCuponesTC_Hoja1 = -1;
            double ValorDiferencia_Hoja1 = -1;
            double ValorDiferencias_Hoja1 = -1;
            double ValorAbonoPorRecaudoEnOficina_Hoja1 = -1;
            double TotalValorAbonado_Hoja1 = -1;
            int CantidadCuponesEfectivoCheque_Hoja1 = -1;
            string Fecha_Real_Recaudo_hoja1 = "";
            double ValorComisionCalculado = -1;
            
            try
            {

                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);

                //OK: if VALIDACIONES
                    CargaArchivoExcel(true, ref dtRef, ref dsref);
                    KillSpecificProcess("EXCEL");

                    var MaxNumCamposLog = dbmIntregation.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "MaxNumero_CamposLog_BcoBogota");
                    if ((MaxNumCamposLog.Rows.Count > 0))
                    {
                        MaxNumCamposLog_BcoBogota = Convert.ToInt32(MaxNumCamposLog.Rows[0]["Valor_Parametro_Sistema"].ToString());
                    }

                    DBIntegration.SchemaConfig.TBL_Campos_Tipo_logDataTable CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(false, this.fk_Tipo_log, Entidad, Proyecto, null);
                    var ColumnsAdicionalesLogData = dbmIntregation.SchemaBcoItau.TBL_Log_Data.DBGet(0).Columns;

                    foreach (DataColumn itemCampo in ColumnsAdicionalesLogData)
                    {
                        _DataFile.Columns.Add(itemCampo.ColumnName);
                    }
                    _DataFile.Columns.RemoveAt(0);

                    if ((this.ExtensionGlobal.ToString().ToLower().Contains(".xls")))
                    { //.EXCEL

                        if (dsref.Tables.Count > 0)
                        {
                            dtRef = dsref.Tables[0];

                            if (dtRef.Rows.Count > 0)
                            {
                                if ((dtRef.Rows.Count > 0) && dtRef.TableName.ToUpper() != "INFORMACION DE VTA CON TARJETAS$")
                                {
                                    for (int j = 0; j <= dtRef.Rows.Count - 1; j++)
                                    {
                                        for (int k = 0; k <= dtRef.Columns.Count - 1; k++)
                                        {
                                            var ValorBuscar = dtRef.Rows[j][k].ToString().ToUpper();

                                            switch (ValorBuscar)
                                            { 
                                                case "FECHA REAL DEL RECAUDO:":
                                                    double dtvalue = double.Parse(dtRef.Rows[j][k + 1].ToString());
                                                    DateTime dateInfo= DateTime.FromOADate(dtvalue );
                                                    Fecha_Real_Recaudo_hoja1 = dateInfo.ToString("yyyy/MM/dd");
                                                    break;

                                                case "ABONO POR RECAUDO REALIZADO EN OFICINA":
                                                    ValorAbonoPorRecaudoEnOficina_Hoja1 = double.Parse(dtRef.Rows[j][k + 1].ToString());
                                                    break;
                                                   
                                                case "TOTAL VALOR ABONADO":
                                                    TotalValorAbonado_Hoja1 = double.Parse(dtRef.Rows[j][k + 1].ToString());
                                                    break;

                                                case "DIFERENCIA":
                                                    ValorDiferencia_Hoja1 = Math.Abs(Convert.ToInt64(dtRef.Rows[j][k + 1].ToString()));
                                                    break;

                                                case "RECAUDO EN EFECTIVO / CHEQUE":
                                                    CantidadCuponesEfectivoCheque_Hoja1 = Convert.ToInt32(dtRef.Rows[j][k + 1].ToString());
                                                    break;

                                                case "RECAUDO EN TARJETAS":
                                                    CantidadCuponesTC_Hoja1 = Convert.ToInt32(dtRef.Rows[j][k + 1].ToString());
                                                    TotalRecaudoTarjetaa_Hoja1 = double.Parse(dtRef.Rows[j][k + 2].ToString());
                                                    break;
                
                                                case "DIFERENCIAS":
                                                    ValorDiferencias_Hoja1 = Math.Abs(double.Parse(dtRef.Rows[j][k + 2].ToString()));
                                                    break;

                                            }

                                            //TODO: if cantidad diferencias break
                                            if (ValorDiferencias_Hoja1 != -1)
                                            {
                                                break;
                                            }

                                        }
                                        if (ValorDiferencias_Hoja1 != -1)
                                        {
                                            break;
                                        }
                                    }
                                }


                                ValorComisionCalculado = CalcularPorcentajeComision(TotalRecaudoTarjetaa_Hoja1, ValorDiferencia_Hoja1);

                           

                            }
                            else
                            {
                                trReturn.Result = false;
                                lisMsgError.Add("No se pudo cargar el archivo o está vacío.");
                                trReturn.Parameters = lisMsgError;
                            }

                        }

                        //VALIDAR QUE NO EXISTA FECHA DE RECAUDO ANTERIOR CARGADA
                        var LogPrevioPorFechaRecaudo = dbmIntregation.SchemaBcoItau.TBL_Log_Data.DBFindByfk_Entidadfk_Proyectofk_Tipo_logFecha_ProcesoFecha_Recaudo(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, IdTipo_log, null, Fecha_Real_Recaudo_hoja1);

                        if (LogPrevioPorFechaRecaudo.Rows.Count > 0)
                        {
                            trReturn.Result = false;
                            lisMsgError.Add("No se puede cargar el log de Cuadre diario. Ya ha sido cargado un log para la fecha de recaudo: " + Fecha_Real_Recaudo_hoja1);
                            trReturn.Parameters = lisMsgError;
                            return trReturn;
                        }

                         if (!(lisMsgError.Count > 0))
                        {

                            _DataRegistros = _DataFile.Rows.Count;
                            _DataColumnas = _DataFile.Columns.Count;

                            if (ValorComisionCalculado != -1)
                            {
                                _DataFile.Rows.Add(ltCamposLog.ToArray());
                                _DataFile.Rows[0]["fk_Entidad"] = this._Plugin.Manager.ImagingGlobal.Entidad;
                                _DataFile.Rows[0]["fk_Proyecto"] = this._Plugin.Manager.ImagingGlobal.Proyecto;
                                _DataFile.Rows[0]["fk_Tipo_Log"] = this.fk_Tipo_log;
                                _DataFile.Rows[0]["Fecha_Proceso"] = this.dtpFechaProceso.Value.ToString("yyyyMMdd");
                                _DataFile.Rows[0]["Cruzado"] = "0";
                                _DataFile.Rows[0]["Fecha_Recaudo"] = Convert.ToDateTime(Fecha_Real_Recaudo_hoja1).ToString("yyyyMMdd");
                                _DataFile.Rows[0]["Campo_1"] = Fecha_Real_Recaudo_hoja1;
                                _DataFile.Rows[0]["Campo_2"] = ValorAbonoPorRecaudoEnOficina_Hoja1;
                                _DataFile.Rows[0]["Campo_3"] = TotalValorAbonado_Hoja1;
                                _DataFile.Rows[0]["Campo_4"] = ValorDiferencia_Hoja1;
                                _DataFile.Rows[0]["Campo_5"] = CantidadCuponesEfectivoCheque_Hoja1;
                                _DataFile.Rows[0]["Campo_6"] = CantidadCuponesTC_Hoja1;
                                _DataFile.Rows[0]["Campo_7"] = TotalRecaudoTarjetaa_Hoja1;
                                _DataFile.Rows[0]["Campo_8"] = ValorDiferencias_Hoja1;
                                _DataFile.Rows[0]["Campo_9"] = ValorComisionCalculado;

                                DBIntegration.SchemaBcoItau.TBL_Log_DataType Data_Log = new DBIntegration.SchemaBcoItau.TBL_Log_DataType();
                                Data_Log.Fecha_Proceso = this._fechaProceso.ToString("yyyyMMdd");
                                Data_Log.Fecha_Recaudo = _DataFile.Rows[0]["Fecha_Recaudo"].ToString();
                                Data_Log.fk_Tipo_log = this.fk_Tipo_log;
                                bool valida = false;

                                ProcesaLog.ProcesaLog_BcoBogota(ref _DataFile, Path.GetFileName(((FileStream)_File).Name), ref this.CargueBackgroundWorker, this._Plugin, ref this._idCargue, Data_Log, valida, "", this.dtpFechaProceso.Value.ToString("yyyyMMdd"), ref trReturn);

                            }
                        }
                        else
                        {
                            trReturn.Result = false;
                            lisMsgError.Add("No fue posible realizar el cálculo de la comisión.");
                            trReturn.Parameters = lisMsgError;
                        }

                    }
                //}
            }

            catch (Exception ex)
            {
                lisMsgError.Add("- Error: " + ex.Message);
                trReturn.Result = false;
                trReturn.Parameters = lisMsgError;
                KillSpecificProcess("EXCEL");
            }
            finally
            {
                dbmIntregation.Connection_Close();
                KillSpecificProcess("EXCEL");
            }

            return trReturn;
        }

        private double CalcularPorcentajeComision(double ValorCuponesRecaudoTC, double ValorDiferencia)
        {
            var TotalComisionTC = ValorDiferencia / ValorCuponesRecaudoTC;

           return TotalComisionTC;
        }



        private  DesktopConfig.TypeResult  CargueRBLISRDO(string NombreArchivo, int Entidad, int Proyecto, int IdTipo_log)
        {
            DesktopConfig.TypeResult trReturn = new DesktopConfig.TypeResult();
            trReturn.Result = true;
            List<string> listErrores = new List<string>();
            string strFechaRecaudo = ""; //yyyyMMdd
            int contadorLineas = 0;
            DBIntegrationDataBaseManager dbmIntregation = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);
            _DataFile = new DataTable();
            List<string> ltCamposLog = new List<string>();
            int MaxNumCamposLog_BcoBogota = 0;
            int contadorValoresEncontrados = 0;
            int contador = 0;
            bool CamposLLenos = false;
            bool TieneFechaRecaudo = false;
            List<string> lisMsgError = new List<string>();

            try
            {
                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);

                var MaxNumCamposLog = dbmIntregation.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "MaxNumero_CamposLog_BcoBogota");
                if ((MaxNumCamposLog.Rows.Count > 0))
                {
                    MaxNumCamposLog_BcoBogota = Convert.ToInt32(MaxNumCamposLog.Rows[0]["Valor_Parametro_Sistema"].ToString());
                }

                DBIntegration.SchemaConfig.TBL_Campos_Tipo_logDataTable CamposPorTipoLog = dbmIntregation.SchemaConfig.TBL_Campos_Tipo_log.DBFindByCampo_Adicionalfk_Tipo_logfk_Entidadfk_ProyectoDescripcion(false, this.fk_Tipo_log, Entidad, Proyecto, null);
                var ColumnsAdicionalesLogData = dbmIntregation.SchemaBcoItau.TBL_Log_Data.DBGet(0).Columns;

                foreach (DataColumn itemCampo in ColumnsAdicionalesLogData)
                {
                    _DataFile.Columns.Add(itemCampo.ColumnName);
                }
                _DataFile.Columns.RemoveAt(0);

                using (StreamReader st = new StreamReader(((FileStream)_File).Name, System.Text.Encoding.Default))
                {
                    while ((!st.EndOfStream))
                    {
                        contadorLineas++;
                        var Linea = st.ReadLine().ToString();

                        if (contadorLineas > this.SaltoPrimasLineas)
                        {
                            if (Linea != "")
                            {
                                if (Linea.Contains("FECHA RECAUDO") && strFechaRecaudo == "" && TieneFechaRecaudo == false)
                                {
                                    int LastIndexFecha = Linea.LastIndexOf(":");
                                    strFechaRecaudo = Linea.Substring(LastIndexFecha + 1, 8);
                                    TieneFechaRecaudo = true;
                                }
                                else if (Linea.Contains("1*") || Linea.Contains("BANCO DE BOGOTA  ") || Linea.Contains("PARA ENTREGAR EN OFICINA") || Linea.Contains("EMPRESA:"))
                                {
                                    continue;
                                }
                                else if (Linea.Substring(0, 1).ToString() == "1")
                                {
                                    continue;
                                }
                                else
                                {
                                    string[] strSplitValores = Linea.Split(' ');
                                    strSplitValores = strSplitValores.Where(x => x.ToString() != "").Select(x => x.ToString()).ToArray();

                                    foreach (var item in ColumnsAdicionalesLogData)
                                    {
                                        if (item.ToString().ToUpper() != "ID_LOG_DATA")
                                        {
                                            if (item.ToString().ToUpper().Contains("CAMPO_"))
                                            {
                                                if (CamposLLenos == false)
                                                {
                                                    for (int index = 0; index <= MaxNumCamposLog_BcoBogota - 1; index++)
                                                    {
                                                        var idBuscar = index + 1;
                                                        var idCampo_Encontrado = CamposPorTipoLog.Where(x => x.Campo_Log_Data == idBuscar);

                                                        if ((idCampo_Encontrado.ToList().Count > 0) && contadorValoresEncontrados < strSplitValores.Length)
                                                        {
                                                            ltCamposLog.Add(strSplitValores[index]);
                                                            contadorValoresEncontrados++;
                                                        }
                                                        else
                                                        {
                                                            ltCamposLog.Add("");
                                                        }
                                                        CamposLLenos = true;
                                                    }
                                                    contadorValoresEncontrados = 0;
                                                }

                                            }
                                            else
                                                ltCamposLog.Add("");
                                        }
                                    }
                                    CamposLLenos = false;
                                    _DataFile.Rows.Add(ltCamposLog.ToArray());
                                    _DataFile.Rows[contador]["fk_Entidad"] = this._Plugin.Manager.ImagingGlobal.Entidad;
                                    _DataFile.Rows[contador]["fk_Proyecto"] = this._Plugin.Manager.ImagingGlobal.Proyecto;
                                    _DataFile.Rows[contador]["Fecha_Proceso"] = this.dtpFechaProceso.Value.ToString("yyyyMMdd");
                                    _DataFile.Rows[contador]["Cruzado"] = "0";
                                    _DataFile.Rows[contador]["Fecha_Cruce"] = "";
                                    _DataFile.Rows[contador]["fk_Proceso_Data"] = "";
                                    _DataFile.Rows[contador]["Fecha_Proceso_Cruce"] = "";
                                    _DataFile.Rows[contador]["fk_Tipo_log"] = this.fk_Tipo_log;
                                    _DataFile.Rows[contador]["Fecha_Recaudo"] = strFechaRecaudo;
                                    contador += 1;
                                    ltCamposLog.Clear();
                                }
                            }
                        }
                    }
                }

                _DataRegistros = _DataFile.Rows.Count;
                _DataColumnas = _DataFile.Columns.Count;

                DBIntegration.SchemaBcoItau.TBL_Log_DataType Data_Log = new DBIntegration.SchemaBcoItau.TBL_Log_DataType();
                Data_Log.Fecha_Proceso = this._fechaProceso.ToString("yyyyMMdd");
                Data_Log.fk_Tipo_log = this.fk_Tipo_log;
                bool valida = false;
                //KillSpecificProcess("EXCEL");
                ProcesaLog.ProcesaLog_BcoBogota(ref _DataFile, Path.GetFileName(((FileStream)_File).Name), ref this.CargueBackgroundWorker, this._Plugin, ref this._idCargue, Data_Log, valida, "", this.dtpFechaProceso.Value.ToString("yyyyMMdd"), ref trReturn);   
            }  
            catch (Exception ex)
            {
                lisMsgError.Add("- Error: " + ex.Message);
                trReturn.Result = false;
                trReturn.Parameters = lisMsgError;
            }
            finally
            {
                dbmIntregation.Connection_Close();
            }

            return trReturn;
        }

        private void KillSpecificProcess(string processName)
        {
	        var processes = Process.GetProcessesByName(processName).Where(x=>x.ProcessName == processName).Select(x=>x);

	        foreach (var process__1_loopVariable in processes) {
		        process__1_loopVariable.Kill();
	        }
        }

        private void Validaciones()
        {
            if (this.ArchivoDesktopTextBox.Text != "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Ruta de archivo vacia!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                return;
            }
            else if (File.Exists(this.ArchivoDesktopTextBox.Text))
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Log, Archivo no existe!!", "ERROR", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                return;
            }
        }

        private void CargaArchivoExcel(bool UsarReferencia, ref DataTable dtRef, ref DataSet dsref)
        {
            dynamic nTempPath = VallePlugin.AppPath + VallePlugin.TempPath;
            if ((UsarReferencia))
            {
                if ((dtRef != null))
                {
                    dtRef = objXLS.ReadExcelIntoDataTable(((FileStream)_File).Name, nTempPath, chkEncabezado.Checked, this.passwordFile, ref dtRef, ref dsref);
                }
                else if ((dsref != null))
                {
                    objXLS.ReadExcelIntoDataTable(((FileStream)_File).Name, nTempPath, chkEncabezado.Checked, this.passwordFile, ref dtRef, ref dsref);
                }

            }
            else
            {
                _DataFile = objXLS.ReadExcelIntoDataTable(((FileStream)_File).Name, nTempPath, chkEncabezado.Checked, this.passwordFile, ref dtRef, ref dsref);
            }

        }

        private bool ValidaFormatoCorrecto_Log(int IdTipoLog, string NombreArchivo, DBIntegration.SchemaConfig.TBL_Tipos_LogRow RowTipoLog)
        {
            bool retorno = true;
            string NombreArchivo_Aux = "";
            if ((this._validaExtension & NombreArchivo.Contains(".")))
            {
                NombreArchivo_Aux = NombreArchivo.Remove(NombreArchivo.IndexOf("."), NombreArchivo.Length - NombreArchivo.IndexOf("."));
            }
            else
            {
                NombreArchivo_Aux = NombreArchivo;
            }

            try
            {
                if ((RowTipoLog != null)) {
	                ValidacionesSplit(ref retorno, RowTipoLog.Validaciones_ArchivoPlano.Split('|'), RowTipoLog.LengthValidaciones_ArchivoPlano.Split('|'), NombreArchivo_Aux);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return retorno;
        }

        
        private object ValidacionesSplit(ref bool retorno, string[] StrValidacionColumn, string[] StrLengthValidaciones, string NombreArchivo_Aux)
        {
	        try {
		        if ((StrValidacionColumn.Count() > 0)) {
			        for (int index = 0; index <= StrValidacionColumn.Count() - 1; index++) {
				        string AuxItemValidacion = null;
				        if ((StrValidacionColumn[index].ToString().Contains("FECHA"))) {
					        AuxItemValidacion = StrValidacionColumn[index].ToString();
				        } else {
					        AuxItemValidacion = StrValidacionColumn[index].ToString().ToUpper();
				        }
				        dynamic LengthItemValidacion = StrLengthValidaciones[index].Split('-');
				        dynamic lengthInicio = Convert.ToInt32(LengthItemValidacion[0].ToString());
				        dynamic lengthFinal = Convert.ToInt32(LengthItemValidacion[1].ToString());
				        string auxArchivoVal = null;

				        if ((AuxItemValidacion.Contains("*"))) {
					        var OrAnd = AuxItemValidacion.Split('*');
					        if ((OrAnd.Contains("OR") & !OrAnd.Contains("AND"))) {

						        OrAnd = OrAnd.Where(x => !x.Contains("OR")).ToArray();

						        foreach (var itemOr_loopVariable in OrAnd) {
							        auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper();
                                    if ((auxArchivoVal == itemOr_loopVariable.ToUpper()))
                                    {
								        retorno = true;
							        }
						        }
					        }
				        } else if ((AuxItemValidacion.Contains("<FECHA>"))) {
					        DateTime dtAux = new DateTime();
					        auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio));

					        AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "");
					        retorno = DateTime.TryParseExact(auxArchivoVal, AuxItemValidacion, null, DateTimeStyles.None, out dtAux);


					        if ((retorno == false)) {
						        break; // TODO: might not be correct. Was : Exit For
					        }
				        }
                        else if (AuxItemValidacion.Contains("<OFICINA>"))
                        {
                            AuxItemValidacion = AuxItemValidacion.Replace("<FECHA>", "");
                            continue;

                        }
                        else if (AuxItemValidacion.Contains("<ANIO>"))
                        {
                            AuxItemValidacion = AuxItemValidacion.Replace("<ANIO>", "");
                            var anios = Enumerable.Range(1900, DateTime.Now.Year);
                            var encontrado = anios.Where(x => x.ToString() == AuxItemValidacion);

                            if (encontrado.Count() == 0)
                            {
                                retorno = false;
                            }
                        }
                        else
                        {
                            auxArchivoVal = NombreArchivo_Aux.Substring(lengthInicio, (lengthFinal - lengthInicio)).ToUpper();
                            if ((auxArchivoVal != AuxItemValidacion))
                            {
                                retorno = false;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
			        }
		        }
	        } catch (Exception ex) {
		        retorno = false;
	        }

	        return retorno;
        }


        private void ValidarExiste_ArchivoCargue(string NombreArchivo, DBIntegrationDataBaseManager dbmIntegration)
        {
            try
            {

                DBIntegration.SchemaConfig.TBL_CargueDataTable Cargue_valido  = dbmIntegration.SchemaConfig.TBL_Cargue.DBFindByArchivo_CargueValido(NombreArchivo, true);

                if ((Cargue_valido.Rows.Count > 0) && this.validaArchivoCargue)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                    return;
                }

                var fechaProceso = this.dtpFechaProceso.Value.ToString("yyyyMMdd");


                if ((Cargue_valido.Select("Fecha_Proceso = '" + fechaProceso + "'").Count() > 0) && this.validaArchivoCargue == false)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado con un Cargue Valido.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                    return;
                }
              
                dynamic dtCargues = dbmIntegration.SchemaConfig.TBL_Cargue.DBFindByfk_Entidadfk_Proyectofk_Tipo_LogArchivo_CargueValidoFecha_Proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, this.fk_Tipo_log, NombreArchivo, true, null);

                if (dtCargues.Count > 0 && this.validaArchivoCargue)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("El nombre de archivo que estan intentando cargar ya se encuentra registrado, por favor seleccione otro archivo de cargue.", "Archivo de Cargue existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                    _File = null;
                    ArchivoDesktopTextBox.Text = "";
                }
                else
                {

                    if (VerificarFechaProceso())
                    {
                        if ((Path.GetExtension(((FileStream)_File).Name).ToLower().StartsWith(".xl")))
                        {
                            if ((MessageBox.Show("¿Este archivo maneja contraseña?", "Archivo con Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                            {
                                this.passwordFile = Microsoft.VisualBasic.Interaction.InputBox("Digite la contraseña: ", "Password", "");
                            }
                            else
                            {
                                this.passwordFile = "";
                            }
                        }

                        this.CargandoPictureBox.Visible = true;
                        Timer1.Enabled = true;
                        HabilitarControles(false);
                        CheckForIllegalCrossThreadCalls = false;
                        this.CargueBackgroundWorker.RunWorkerAsync(NombreArchivo + "-" + _Plugin.Manager.ImagingGlobal.Entidad.ToString() + "-" + _Plugin.Manager.ImagingGlobal.Proyecto.ToString() + "-" + this.fk_Tipo_log.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("ValidarExiste ArchivoCargue", ref ex);
            }
        }


        public bool VerificarFechaProceso()
        {
            bool retorno = true;
            DataTable ControlCargue = new DataTable();
            DataTable ControlProceso = new DataTable();
            DataTable fechaProcesoDataTable = new DataTable();

            _fechaProceso = this.dtpFechaProceso.Value;

   
            DBImagingDataBaseManager DBMIMaging = new DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
            DBIntegrationDataBaseManager dbmIntregation = new DBIntegrationDataBaseManager(_Plugin.BcoItauConnectionString);

            try
            {
                DBMIMaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                dbmIntregation.Connection_Open(_Plugin.Manager.Sesion.Usuario.id);
                dynamic fechaProceso = int.Parse(_fechaProceso.ToString("yyyyMMdd"));

  
                fechaProcesoDataTable = DBMIMaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, fechaProceso, null);

                if (fechaProcesoDataTable.Rows.Count == 0)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error, no existe la fecha de proceso Seleccionada", "Error de Fecha proceso", DesktopMessageBoxControl.IconEnum.WarningIcon, true);
                    retorno = false;
                }
                else
                {
                    if ((Convert.ToBoolean(fechaProcesoDataTable.Rows[0]["Cerrado"]) == true) & !DesktopComboBoxControlTiposLog.Text.Contains("V2"))
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("La fecha de proceso ha sido cerrada, no es posible cargar Log a través de esta opción!", "Carga de Log", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                        retorno = false;
                    }

                }

                return retorno;
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Crear Fecha de Proceso", ref ex);
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            finally
            {
                DBMIMaging.Connection_Close();
                dbmIntregation.Connection_Close();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            return retorno;
        }

        private void BuscarArchivo()
        {
            Segundos = 0;
            Minuto = 0;
            Hora = 0;

            try
            {
                dynamic Respuesta = ArchivoOpenFileDialog.ShowDialog();
                if (Respuesta == DialogResult.OK)
                {
                    try
                    {
                        ArchivoDesktopTextBox.Text = ArchivoOpenFileDialog.FileName;
                        _File = ArchivoOpenFileDialog.OpenFile();
                    }
                    catch (Exception ex)
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ref ex);
                    }
                    finally
                    {
                        if (_File != null)
                        {
                            _File.Close();
                        }
                    }
                }
                else if (Respuesta == DialogResult.Cancel)
                {
                    ArchivoDesktopTextBox.Text = "";
                    _File = null;
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("BuscarArchivo", ref ex);
            }
        }

        private void HabilitarControles(bool valor)
        {
            ArchivoDesktopTextBox.Enabled = valor;
            BuscarArchivoButton.Enabled = valor;
            CargarButton.Enabled = valor;
        }

        private void CargarCombos()
        {

            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._Plugin.BcoItauConnectionString);
            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);
                DataTable TiposLog = dbmIntegration.SchemaConfig.TBL_Tipos_Log.DBFindByfk_Entidadfk_ProyectoVisible(this.Entidad, this.Proyecto,true);

                if (TiposLog != null)
                {
                    if (TiposLog.Rows.Count > 0)
                    {
                        DesktopComboBoxControlTiposLog.DisplayMember = TiposLog.Columns["Nombre_Tipo_Log"].ToString();
                        DesktopComboBoxControlTiposLog.ValueMember = TiposLog.Columns["id_Tipo_log"].ToString();
                        DesktopComboBoxControlTiposLog.DataSource = TiposLog;
                    }
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("CargaDatos", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }
        #endregion

        private void DesktopComboBoxControlTiposLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DesktopComboBoxControlTiposLog.Text.Contains("VEHÍCULO"))
            {
                dtpFechaRecaudo.Enabled = true;
            }
            else
            { dtpFechaRecaudo.Enabled = false; }

        }

        


        

        

        

        
    }
}
