using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Desktop.Library;
using Miharu.Desktop.Library.Config;
using DBImaging;
using DBIntegration;
using DBCore;
using Miharu.FileProvider.Library;
using Microsoft.Reporting.WinForms;
using Miharu.Desktop.Controls.BarCode;
using System.Drawing.Imaging;
using System.IO;
using DBIntegration.SchemaConfig;
using Slyg.Tools;
using Slyg.Tools.Imaging;
using DBIntegration.SchemaSantander; //to do: validar si toca crear uno por cada proyecto

namespace BcoColpatria.Plugin.Imaging.Embargos.Form
{
    public partial class FormGenerarCartas : FormBase
    {
        private EmbargosPlugin _plugin;

        public FormGenerarCartas(EmbargosPlugin _plugin)
        {
            InitializeComponent();
            this._plugin = _plugin;
        }

        #region Metodos

        private void CargarOT()
        {
            OTDesktopComboBox.DataSource = null;
            OTDesktopComboBox.SelectedIndex = -1;

            DBImagingDataBaseManager dbmImaging = null;

            try
            {
                dbmImaging = new DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id);

                var OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, System.Convert.ToInt32(FechaProcesoPicker.Value.ToString("yyyyMMdd")), true, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad);

                if (OTDataTable.Count > 0)
                {
                    foreach (var OTRow in OTDataTable)
                        Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, OTDataTable.id_OTColumn.ColumnName, OTDataTable.id_OTColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((dbmImaging != null))
                    dbmImaging.Connection_Close();
            }
        }

        private void GenerarCartas()
        { 
            DBIntegrationDataBaseManager dbmIntegration = null;
            DBCoreDataBaseManager dbmCore = null;
            DBImagingDataBaseManager dbmImaging = null;
            FileProviderManager manager = null;

            try
            {
                if (Validar())
                {
                    dbmImaging = new DBImaging.DBImagingDataBaseManager(this._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                    dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._plugin.Manager.DesktopGlobal.ConnectionStrings.Integration);
                    dbmCore = new DBCore.DBCoreDataBaseManager(this._plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

                    dbmImaging.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);
                    dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);
                    dbmCore.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                    var ImagenesDataTable = dbmIntegration.SchemaProcess.PA_Get_Imagenes.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto);
                    var FormatoDataTable = dbmIntegration.SchemaConfig.TBL_Formato.DBGet(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto, null);

                    var ExpedientesDataTable = dbmIntegration.SchemaBcoCoopcentral.PA_Get_Expedientes_Carta_Respueta.DBExecute(System.Convert.ToInt32(OTDesktopComboBox.SelectedValue), _plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.ImagingGlobal.Proyecto);

                    if (ExpedientesDataTable.Rows.Count > 0)
                    {
                        CTA_Listado_CBarras_DistribucionDataTable ListadoCBarras = new CTA_Listado_CBarras_DistribucionDataTable();
                        CTA_Listado_CBarras_DistribucionDataTable ListadoCBarrasNo = new CTA_Listado_CBarras_DistribucionDataTable();

                        var CTAImagenesDataSource = new ReportDataSource("CTA_ImagenesDataSet", ImagenesDataTable);

                        var formato = Utilities.GetEnumFormat(_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Entrada.ToString());
                        var compresion = Utilities.GetEnumCompression((DesktopConfig.FormatoImagenEnum)_plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida);
                        const int MaxThumbnailWidth = 60;
                        const int MaxThumbnailHeight = 80;

                        var servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)[0].ToCTA_ServidorSimpleType();
                        var centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)[0].ToCTA_Centro_ProcesamientoSimpleType();

                        manager = new FileProviderManager(servidor, centro, ref dbmImaging, this._plugin.Manager.Sesion.Usuario.id);
                        manager.Connect();

                        foreach (var ExpedientesRow in ExpedientesDataTable)
                        {
                            var CBarrasRow = ListadoCBarras.NewCTA_Listado_CBarras_DistribucionRow();

                            DBIntegration.SchemaConfig.TBL_FormatoRow[] rowFormatoRespuesta;

                            var TipoFormatoRespuesta = System.Convert.ToInt32(ExpedientesRow.TipoCartaRespuesta);

                            rowFormatoRespuesta = (DBIntegration.SchemaConfig.TBL_FormatoRow[])FormatoDataTable.Select("id_Formato = " + TipoFormatoRespuesta.ToString());

                            var FormatoParametrosDataTable = dbmIntegration.SchemaProcess.PA_Formato_Parametros.DBExecute((long)ExpedientesRow.fk_Expediente, (short)ExpedientesRow.fk_Folder, 1, System.Convert.ToInt32(TipoFormatoRespuesta));

                            CBarrasRow.Numero_Unico = "";
                            DBIntegration.SchemaConfig.TBL_FormatoSimpleType CartaFormateadaRow = new DBIntegration.SchemaConfig.TBL_FormatoSimpleType();

                            CartaFormateadaRow = rowFormatoRespuesta[0].ToTBL_FormatoSimpleType();

                            foreach (var tblFormatoParametrosRow in FormatoParametrosDataTable)
                            {
                                switch (tblFormatoParametrosRow.fk_parametro_tipo)
                                    {
                                        case 1:
                                            CartaFormateadaRow.Destinatario = CartaFormateadaRow.Destinatario.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro);
                                            if (tblFormatoParametrosRow.Es_Numero_Unico)
                                            {
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "");
                                            }
                                            break;
                                        case 2:
                                            CartaFormateadaRow.Asunto = CartaFormateadaRow.Asunto.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro);
                                        if (tblFormatoParametrosRow.Es_Numero_Unico)
                                            {
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "");
                                            }
                                            break;
                                        case 3:
                                            CartaFormateadaRow.Cuerpo = CartaFormateadaRow.Cuerpo.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro);
                                        if (tblFormatoParametrosRow.Es_Numero_Unico)
                                            {
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "");
                                            }
                                            break;
                                        case 4:
                                            CartaFormateadaRow.Firma = CartaFormateadaRow.Firma.Replace(tblFormatoParametrosRow.parametro, tblFormatoParametrosRow.Valor_Parametro);
                                        if (tblFormatoParametrosRow.Es_Numero_Unico)
                                            {
                                                CBarrasRow.Numero_Unico = tblFormatoParametrosRow.Valor_Parametro.Replace("<b>", "").Replace("</b>", "");
                                            }
                                            break;
                                     }
                            }

                            System.Globalization.CultureInfo cultures = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
                            string FechaGeneracion = DateTime.Now.ToString("D", cultures);

                            CartaFormateadaRow.Destinatario = CartaFormateadaRow.Destinatario.Replace("@FechaGeneracion", FechaGeneracion);

                            DataTable FormatoDT = FormatoDataTable.Clone();

                            DataRow Fdtr = FormatoDT.NewRow();
                            Fdtr["Fk_Entidad"] = CartaFormateadaRow.Fk_Entidad;
                            Fdtr["Fk_Proyecto"] = CartaFormateadaRow.Fk_Proyecto;
                            Fdtr["Id_Formato"] = CartaFormateadaRow.Id_Formato;
                            Fdtr["Nombre_Formato"] = CartaFormateadaRow.Nombre_Formato;
                            Fdtr["fk_Ente_Coactivo"] = CartaFormateadaRow.fk_Ente_Coactivo;
                            Fdtr["Descripción"] = CartaFormateadaRow.Descripción;
                            Fdtr["Destinatario"] = CartaFormateadaRow.Destinatario;
                            Fdtr["Asunto"] = CartaFormateadaRow.Asunto;
                            Fdtr["Cuerpo"] = CartaFormateadaRow.Cuerpo;
                            Fdtr["Firma"] = CartaFormateadaRow.Firma;

                            FormatoDT.Rows.Add(Fdtr);

                            CTA_Imagen_CBarras_ReportDataTable CodigoBarrasDataTable = new CTA_Imagen_CBarras_ReportDataTable();
                            var CodigoBarrasDataRow = CodigoBarrasDataTable.NewCTA_Imagen_CBarras_ReportRow();
                                                       
                            {
                                BarCodeControl CodigoBarras = new BarCodeControl();

                                CodigoBarras.BarCode = ExpedientesRow.CBarras_File;
                                CodigoBarras.BarCodeHeight = 50;
                                CodigoBarras.BarCodeType = BarCodeTypeType.EAN128;
                                CodigoBarras.Align = AlignType.Center;
                                CodigoBarras.Width = 200;
                                CodigoBarras.Height = 40;
                                CodigoBarras.Weight = BarCodeWeight.Small;
                                CodigoBarras.ShowHeader = false;
                                CodigoBarras.FooterLinesClear();
                                CodigoBarras.Update();

                                CodigoBarrasDataRow.ImagenCBarras = CodigoBarras.GetImage(ImageFormat.Jpeg);
                                CBarrasRow.ImagenCBarras = CodigoBarras.GetImage(ImageFormat.Jpeg);
                            }


                            CodigoBarrasDataTable.AddCTA_Imagen_CBarras_ReportRow(CodigoBarrasDataRow);

                            var TBLFormatoDataSource = new ReportDataSource("TBL_FormatoDataSet", FormatoDT);
                            var CodigoBarrasDataSource = new ReportDataSource("ImagenCBarrasDataSet", (DataTable)CodigoBarrasDataTable);

                            this.ReportViewer1.LocalReport.ReportEmbeddedResource = "BcoColpatria.Plugin.Imaging.Embargos.Form.Report_GenerarCartas.rdlc";
                            this.ReportViewer1.LocalReport.DataSources.Clear();
                            this.ReportViewer1.LocalReport.DataSources.Add(CTAImagenesDataSource);
                            this.ReportViewer1.LocalReport.DataSources.Add(TBLFormatoDataSource);
                            this.ReportViewer1.LocalReport.DataSources.Add(CodigoBarrasDataSource);
                            var Parametros = new ReportParameter();
                            Parametros = new ReportParameter("Cbarras", ExpedientesRow.CBarras_File);
                            this.ReportViewer1.LocalReport.SetParameters(Parametros);

                            this.ReportViewer1.RefreshReport();

                            string FileName = "";
                            byte[] Imagen = null;
                            byte[] Thumbnail = null;
                            var identificador = Guid.NewGuid();

                            FileName = Program.AppPath + Program.TempPath + identificador.ToString() + ".tiff";
                            Imagen = this.ReportViewer1.LocalReport.Render("Image");

                            using (var fs = new FileStream(FileName, FileMode.Create))
                            {
                                fs.Write(Imagen, 0, Imagen.Length);
                                fs.Close();
                            }

                            bool leerFolio = true;
                            short Folios = 1;
                            var FolioBitmap = ImageManager.GetFolioBitmap(FileName, Folios);

                            dbmCore.Transaction_Begin();
                                    manager.TransactionBegin();

                            for (int folio = 1, loopTo = Folios; folio <= loopTo; folio++)
                            {
                                var dataImage = ImageManager.GetFolioData(FolioBitmap, folio, 1, formato, compresion);
                                var dataImageThumbnail = ImageManager.GetThumbnailData(FileName, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight);

                                if (Folios == 1)
                                {
                                    manager.CreateItem((long)ExpedientesRow.fk_Expediente, (short)ExpedientesRow.fk_Folder, (short)ExpedientesRow.id_File, 1, _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, identificador);

                                    var fileImgType = new DBCore.SchemaImaging.TBL_FileType();

                                    fileImgType.fk_Expediente = ExpedientesRow.fk_Expediente;
                                    fileImgType.fk_Folder = (short)ExpedientesRow.fk_Folder;
                                    fileImgType.fk_File = (short)ExpedientesRow.id_File;
                                    fileImgType.id_Version = 1;
                                    fileImgType.File_Unique_Identifier = identificador;
                                    fileImgType.Folios_Documento_File = Folios;
                                    fileImgType.Tamaño_Imagen_File = 0;
                                    fileImgType.Nombre_Imagen_File = "";
                                    fileImgType.Key_Cargue_Item = "";
                                    fileImgType.Save_FileName = "";
                                    fileImgType.fk_Content_Type = _plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida;
                                    fileImgType.fk_Usuario_Log = _plugin.Manager.Sesion.Usuario.id;
                                    fileImgType.Validaciones_Opcionales = false;
                                    fileImgType.Es_Anexo = false;
                                    fileImgType.fk_Anexo = null;
                                    fileImgType.fk_Entidad_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad;
                                    fileImgType.fk_Servidor = _plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor;
                                    fileImgType.Fecha_Creacion = SlygNullable.SysDate;
                                    fileImgType.Fecha_Transferencia = null;
                                    fileImgType.En_Transferencia = false;
                                    fileImgType.fk_Entidad_Servidor_Transferencia = null;
                                    fileImgType.fk_Servidor_Transferencia = null;
                                    dbmCore.SchemaImaging.TBL_File.DBInsert(fileImgType);
                                }

                                manager.CreateFolio((long)ExpedientesRow.fk_Expediente, (short)ExpedientesRow.fk_Folder, (short)ExpedientesRow.id_File, 1, (short)folio, dataImage, dataImageThumbnail[0], false);
                            }

                            FolioBitmap.Dispose();

                            dbmCore.Transaction_Commit();
                            manager.TransactionCommit();

                            Utilities.ActualizaEstadoFileImaging(ref dbmImaging, ref dbmCore, ExpedientesRow.CBarras_File.ToString(), DesktopConfig.Modulo.Imaging, 42, _plugin.Manager.Sesion.Usuario.id);
                        }
                        MessageBox.Show("Cartas generadas correctamente", "Generar Cartas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No hay cartas para generar", "Generar Cartas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "GenerarCartas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dbmCore != null)
                dbmCore.Transaction_Rollback();
                if (manager != null)
                manager.TransactionRollback();
            }
            finally
            {
                if ((dbmImaging != null))
                    dbmImaging.Connection_Close();
                if ((dbmCore != null))
                    dbmCore.Connection_Close();
                if ((dbmIntegration != null))
                    dbmIntegration.Connection_Close();
            }
        }

        #endregion

        #region Funciones

        private bool Validar()
        {
            if (OTDesktopComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No hay OT's para la fecha de proceso seleccionada", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        #endregion

        #region eventos

        private void FormGenerarCartas_Load(object sender, EventArgs e)
        {
            ReportViewer1.RefreshReport();
            CargarOT();
        }

        private void FechaProcesoPicker_ValueChanged(object sender, EventArgs e)
        {
            CargarOT();
        }

        private void GenerarButton_Click(object sender, EventArgs e)
        {
            GenerarCartas();
        }

        #endregion       
    }
}
