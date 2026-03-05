using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Desktop.Controls.DesktopReportDataGridView;
using Miharu.Desktop.Controls.DesktopMessageBox;
using Miharu.Desktop.Library;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Library.Plugins;
using DBCore.SchemaConfig;
using System.Web.Script.Serialization;

namespace Miharu.Uploader.Forms
{
    public partial class FormVisorReportes : Form
    {
        private DBCore.SchemaConfig.TBL_ReporteRow _selectedReport2;
        private DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaRow _selectedReport;
        private DBCore.SchemaConfig.TBL_Reporte_Tipo_SalidaRow _SelectedTipoSalidaReporte;
        private string _connectionString;
        private string _basicConnectionString;

        public enum IconoReportes : int
        {
            Categoria = 0,
            Reporte = 1
        }

//        public DesktopPluginManager Manager
//        {
//            get
//            {
//                return this._Manager;
//            }
//        }

//        private void FormVisorReportes_Load(object sender, EventArgs e)
//        {
//            CargarCategorias();
//        }

//        private void ReportesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
//        {
//            if (ReportesTreeView.SelectedNode != null)
//            {
//                if (ReportesTreeView.SelectedNode.Name.StartsWith("C-"))
//                    CargarReportes(ReportesTreeView.SelectedNode);
//                else
//                {
//                    // _selectedReport = CType(ReportesTreeView.SelectedNode.Tag, DBCore.SchemaConfig.TBL_ReporteRow)
//                    _selectedReport = (DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaRow)ReportesTreeView.SelectedNode.Tag;
//                    nombreReporteLabel.Text = _selectedReport.Nombre_Reporte;
//                    CargarParametros();
//                }
//                ReportesTreeView.SelectedNode.Expand();
//            }
//        }

//        private void EjecutarButton_Click(object sender, EventArgs e)
//        {
//            this.Cursor = Cursors.AppStarting;
//            EjecutarSentencia();
//            this.Cursor = Cursors.Default;
//        }

//        private void ExportarMasivoButton_Click(System.Object sender, System.EventArgs e)
//{
//    this.Cursor = Cursors.AppStarting;
//    DataSet resultados = null/* TODO Change to default(_) if this is not a reference type */;
//    Int32 contador;
//    DataBaseManager dbmReporte = null/* TODO Change to default(_) if this is not a reference type */;
//    string GuardaArchivo;
//    var DesktopControl = new DesktopReportDataGridViewControl();
//    string ManejaEncabezado;
//    string SeparadoComa;
//    string SeparadoTabulador;
//    string SeparadoPuntoyComa;
//    string SeparadoVacio;
//    string FormatExcel;
//    string FormatTexto;

//    try
//    {
//        dbmReporte = new DataBaseManager(_connectionString);
//        dbmReporte.Connection_Open();

//        var dataBaseType = DataBaseFactory.GetDataBaseType(_connectionString);
//        var cSql = ReemplazarParametros(this._selectedReport.Consulta, "");
//        // dbmReporte.Transaction_Begin()

//        // resultados.Tables.Add(dbmReporte.DataBase.ExecuteQueryGet(cSql))
//        resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType);
//    }

//    // dbmReporte.Transaction_Commit()
//    catch (Exception ex)
//    {
//        // dbmReporte.Transaction_Rollback()
//        throw;
//    }

//    for (var i = resultados.Tables.Count; i >= 1; i += -1)
//    {
//        DataTable tabla;

//        tabla = resultados.Tables(i - 1);
//        var nuevaGrilla = new DesktopReportDataGridViewControl();

//        nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core;
//        nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools;

//        nuevaGrilla.Titulo = tabla.Rows.Item(0).ItemArray(0).ToString();

//        if (resultados.Tables.Count == 1)
//            nuevaGrilla.Dock = DockStyle.Fill;
//        else
//            nuevaGrilla.Dock = DockStyle.Top;
//        short index;
//        index = System.Convert.ToInt16(resultados.Tables.IndexOf(tabla) + 1);

//        nuevaGrilla.Id_Reporte = this._selectedReport.Id_Reporte;
//        if (!this._selectedReport == null)
//            nuevaGrilla.Salto_Linea = this._SelectedTipoSalidaReporte.Codigo_Salto_Linea;
//        else
//            nuevaGrilla.Salto_Linea = "";

//        if (!_selectedReport.Muestra_Columna_Nombre_Reporte_Masivo)
//        {
//            DataTable tabla2 = null/* TODO Change to default(_) if this is not a reference type */;
//            tabla2 = tabla.Copy;
//            tabla2.Columns.Remove(tabla2.Columns(0));
//            tabla2.AcceptChanges();

//            nuevaGrilla.InternalGridView.DataSource = tabla2;
//        }
//        else
//            nuevaGrilla.InternalGridView.DataSource = tabla;

//        nuevaGrilla.ButtonOnClickReportMas_ = true;
//        nuevaGrilla.Contadorshowmessage_ = contador;
//        nuevaGrilla.contarReportes_ = index;

//        var ParametrosForm = new FormParametrosExportacion();
//        if (contador == 0)
//        {
//            if (ParametrosForm.ShowDialog == System.Windows.Forms.DialogResult.OK)
//            {
//                nuevaGrilla.Exportar(Convert.ToBoolean(ParametrosForm.ManejaEncabezado), Convert.ToBoolean(ParametrosForm.SeparadoComa), Convert.ToBoolean(ParametrosForm.SeparadoTabulador), Convert.ToBoolean(ParametrosForm.SeparadoPuntoyComa), Convert.ToBoolean(ParametrosForm.SeparadoVacio), Convert.ToBoolean(ParametrosForm.FormatExcel), Convert.ToBoolean(ParametrosForm.FormatTexto), nuevaGrilla.Salto_Linea);
//                ManejaEncabezado = ParametrosForm.ManejaEncabezado;
//                SeparadoComa = ParametrosForm.SeparadoComa;
//                SeparadoTabulador = ParametrosForm.SeparadoTabulador;
//                SeparadoPuntoyComa = ParametrosForm.SeparadoPuntoyComa;
//                SeparadoVacio = ParametrosForm.SeparadoVacio;
//                FormatExcel = ParametrosForm.FormatExcel;
//                FormatTexto = ParametrosForm.FormatTexto;
//                GuardaArchivo = nuevaGrilla.GuardaArchivo_;
//            }
//        }
//        else
//        {
//            nuevaGrilla.GuardaArchivo_ = GuardaArchivo;
//            nuevaGrilla.Exportar(Convert.ToBoolean(ManejaEncabezado), Convert.ToBoolean(SeparadoComa), Convert.ToBoolean(SeparadoTabulador), Convert.ToBoolean(SeparadoPuntoyComa), Convert.ToBoolean(SeparadoVacio), Convert.ToBoolean(FormatExcel), Convert.ToBoolean(FormatTexto), nuevaGrilla.Salto_Linea);
//        }
//        contador = contador + 1;
//    }

//    this.Cursor = Cursors.Default;
//}

//        private void LoadList(Controls.ParameterList sender)
//        {
//            SqlClient.SqlConnection cnn = null/* TODO Change to default(_) if this is not a reference type */;

//            try
//            {
//                cnn = new SqlClient.SqlConnection(_basicConnectionString);
//                cnn.Open();

//                var sql = ReemplazarParametros(sender.Query, sender.ParameterName);
//                var da = new SqlClient.SqlDataAdapter(sql, cnn);
//                var resultTable = new DataTable();

//                da.Fill(resultTable);

//                sender.DataSource = resultTable;
//            }
//            catch (Exception ex)
//            {
//                DesktopMessageBoxControl.DesktopMessageShow("LoadList", ex);
//            }
//            finally
//            {
//                if ((cnn != null))
//                    cnn.Close();
//            }
//        }

       //metodos

//        private void CargarCategorias()
//        {
//            var WebService = new Miharu.Uploader.Library.WebService.UploaderService();

//            WebService.IdUsuario = Program.MiharuSession.Usuario.id;
//            WebService.IdEntidad = Program.EntidadCliente;
//            WebService.IdProyecto = Program.Proyecto;

//            var CipherType = short.Parse(Config.UploaderConfig.Decrypt(WebService.getCifradoTipo().Cifrado, Config.UploaderConfig.EnumCipherType.TDES));

//            try
//            {
//                var Proceso = WebService.getReporteCargue();

//                var serializer = new JavaScriptSerializer();

//                var ProcesoDataTable = new CTA_Reportes_Uso_ExternoDataTable();

//                if (Proceso.Reportes != null)
//                {
//                    foreach (var reporte in Proceso.Reportes)
//                    {
//                        var Datos = serializer.Deserialize<CTA_Reportes_Uso_ExternoSimpleType>(Config.UploaderConfig.Decrypt(reporte.ToString(CultureInfo.InvariantCulture), (Config.UploaderConfig.EnumCipherType)CipherType));
//                        ProcesoDataTable.Rows.Add(Datos.ToArray());
//                    }
//                }

//                var categoriasTable2 = dbmCore.SchemaConfig.CTA_Categoria_Reportes.DBFindByfk_Entidad(Program.Sesion.Entidad.id, 0, new DBCore.SchemaConfig.CTA_Categoria_ReportesEnumList(DBCore.SchemaConfig.CTA_Categoria_ReportesEnum.Nombre_Categoria_Reporte, true));

//                var categoriasTable = dbmCore.SchemaConfig.CTA_Categoria_Reportes_2.DBFindByfk_Usuario(Program.Sesion.Usuario.id, 0, new DBCore.SchemaConfig.CTA_Categoria_Reportes_2EnumList(DBCore.SchemaConfig.CTA_Categoria_Reportes_2Enum.Nombre_Categoria_Reporte, true));

//                foreach (var categoria in categoriasTable)
//                {
//                    var newNode = ReportesTreeView.Nodes.Add("C-" + categoria.Id_Categoria_Reporte, categoria.Nombre_Categoria_Reporte, IconoReportes.Categoria, IconoReportes.Categoria);
//                    newNode.Tag = categoria.Id_Categoria_Reporte;

//                    ReportesTreeView.CollapseAll();
//                }
//            }
//            catch (Exception ex)
//            {
//                DesktopMessageBoxControl.DesktopMessageShow("CargarReportes", ex);
//            }
//            finally
//            {
//                if ((dbmCore != null))
//                    dbmCore.Connection_Close();
//            }
//        }

//        private void CargarReportes(TreeNode nCategoriaNode)
//        {
//            DBCore.DBCoreDataBaseManager dbmCore = null/* TODO Change to default(_) if this is not a reference type */;

//            try
//            {
//                dbmCore = new DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core);
//                dbmCore.Connection_Open(Program.Sesion.Usuario.id);

//                nombreReporteLabel.Text = "";

//                var idCategoria = System.Convert.ToInt16(nCategoriaNode.Tag);

//                nCategoriaNode.Nodes.Clear();

//                var reportesTable2 = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_Reporte(idCategoria, 0, new DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, true));

//                var reportesTable = dbmCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_Categoria.DBFindByfk_UsuarioId_Categoria_Reporte(Program.Sesion.Usuario.id, idCategoria, 0, new DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaEnumList(DBCore.SchemaConfig.CTA_Reportes_Por_RolesPermiso_Usuario_CategoriaEnum.Nombre_Reporte, true));

//                // Dim reportesTable = dbmCore.SchemaConfig.TBL_Reporte.DBFindByfk_Categoria_Reporte(idCategoria, 0, New DBCore.SchemaConfig.TBL_ReporteEnumList(DBCore.SchemaConfig.TBL_ReporteEnum.Nombre_Reporte, True))

//                foreach (var reporte in reportesTable)
//                {
//                    var newNode = nCategoriaNode.Nodes.Add("R-" + reporte.Id_Reporte, reporte.Nombre_Reporte, IconoReportes.Reporte, IconoReportes.Reporte);
//                    newNode.Tag = reporte;
//                }
//            }
//            catch (Exception ex)
//            {
//                DesktopMessageBoxControl.DesktopMessageShow("CrearItemsReporte", ex);
//            }
//            finally
//            {
//                if ((dbmCore != null))
//                    dbmCore.Connection_Close();
//            }
//        }

//        private void CargarParametros()
//{
//    DBCore.DBCoreDataBaseManager dbmCore = null/* TODO Change to default(_) if this is not a reference type */;

//    try
//    {
//        dbmCore = new DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core);
//        dbmCore.Connection_Open(Program.Sesion.Usuario.id);

//        // Recuperar la cadena de conexión
//        var conexionTable = dbmCore.SchemaConfig.TBL_Conexion.DBGet(this._selectedReport.fk_Entidad, this._selectedReport.fk_Conexion);

//        _connectionString = conexionTable(0).Cadena_Conexion;
//        _basicConnectionString = GetBasicConnectionString(_connectionString);

//        var parametrosTable = dbmCore.SchemaConfig.TBL_Reporte_Parametro.DBGet(null/* TODO Change to default(_) if this is not a reference type */, this._selectedReport.Id_Reporte);

//        camposPanel.Controls.Clear();

//        TableLayoutPanel tableLayoutPanelControles = new TableLayoutPanel();
//        tableLayoutPanelControles.Name = "controlesTableLayoutPanel";
//        tableLayoutPanelControles.ColumnCount = 2;
//        tableLayoutPanelControles.RowCount = parametrosTable.Count;

//        int i = 0;
//        foreach (var parametro in parametrosTable)
//        {
//            // Crea el Label del campo
//            Label parametroLabel = new Label();
//            parametroLabel.Name = "lbl_" + parametro.Nombre_Parametro;
//            parametroLabel.Text = parametro.Etiqueta_Parametro;
//            parametroLabel.AutoSize = false;
//            parametroLabel.Dock = DockStyle.Fill;
//            parametroLabel.TextAlign = ContentAlignment.TopLeft;

//            IParameter parameter;

//            // Crea la caja de texto
//            switch (parametro.fk_Tipo_Parametro)
//            {
//                case object _ when DesktopConfig.CampoTipo.Texto:
//                    {
//                        parameter = new Controls.ParameterText();
//                        break;
//                    }

//                case object _ when DesktopConfig.CampoTipo.Numerico:
//                    {
//                        parameter = new Controls.ParameterNumeric();
//                        break;
//                    }

//                case object _ when DesktopConfig.CampoTipo.Fecha:
//                    {
//                        parameter = new Controls.ParameterDate();
//                        break;
//                    }

//                case object _ when DesktopConfig.CampoTipo.SiNo:
//                    {
//                        parameter = new Controls.ParameterCheck();
//                        break;
//                    }

//                case object _ when DesktopConfig.CampoTipo.Lista:
//                    {
//                        parameter = new Controls.ParameterList();

//                        if ((parametro.IsConsulta_ListaNull()))
//                            throw new Exception("El parámetro de tipo lista: " + parametro.Nombre_Parametro + ", no se ecuentra configurado");

//                        var lista = (Controls.ParameterList)parameter;

//                        lista.Query = parametro.Consulta_Lista;
//                        lista.DisplayMember = parametro.Columna_Etiqueta_Lista;
//                        lista.ValueMember = parametro.Columna_Valor_Lista;

//                        lista.LoadList += LoadList;
//                        break;
//                    }

//                default:
//                    {
//                        throw new Exception("Tipo de parámetro no válido: " + parametro.Nombre_Parametro);
//                        break;
//                    }
//            }

//            parameter.ParameterName = parametro.Nombre_Parametro;

//            var parameterControl = (UserControl)parameter;
//            parameterControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
//            parameterControl.Dock = DockStyle.Fill;

//            tableLayoutPanelControles.Controls.Add(parametroLabel, 0, i);
//            tableLayoutPanelControles.Controls.Add(parameterControl, 1, i);

//            i += 1;
//        }

//        tableLayoutPanelControles.Dock = DockStyle.Fill;
//        tableLayoutPanelControles.AutoSizeMode = AutoSizeMode.GrowAndShrink;
//        tableLayoutPanelControles.AutoScroll = true;
//        camposPanel.Controls.Add(tableLayoutPanelControles);

//        DBCore.SchemaConfig.TBL_Reporte_Tipo_SalidaDataTable TipoSalidaReporteDataTable;

//        TipoSalidaReporteDataTable = dbmCore.SchemaConfig.TBL_Reporte_Tipo_Salida.DBFindByid_Reporte_Tipo_Salida(_selectedReport.fk_Reporte_Tipo_Salida);

//        if (TipoSalidaReporteDataTable.Rows.Count > 0)
//            _SelectedTipoSalidaReporte = (DBCore.SchemaConfig.TBL_Reporte_Tipo_SalidaRow)TipoSalidaReporteDataTable.Rows(0);
//        else
//            _SelectedTipoSalidaReporte = null;
//    }


//    catch (Exception ex)
//    {
//        DesktopMessageBoxControl.DesktopMessageShow("CrearCampos", ex);
//    }
//    finally
//    {
//        if ((dbmCore != null))
//            dbmCore.Connection_Close();
//    }
//}

//        private void EjecutarSentencia()
//        {
//            if ((_selectedReport != null))
//            {
//                DataTable dataFile = null/* TODO Change to default(_) if this is not a reference type */;
//                DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable titulosDataTable;
//                DBCore.SchemaConfig.TBL_Reporte_ColumnaDataTable columnasDataTable = null/* TODO Change to default(_) if this is not a reference type */;
//                DBCore.DBCoreDataBaseManager dbmCore = null/* TODO Change to default(_) if this is not a reference type */;

//                try
//                {
//                    dbmCore = new DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core);
//                    dbmCore.Connection_Open(Program.Sesion.Usuario.id);

//                    // Cargar los titulos de los resultados
//                    titulosDataTable = dbmCore.SchemaConfig.TBL_Reporte_Salida.DBGet(this._selectedReport.Id_Reporte, null/* TODO Change to default(_) if this is not a reference type */);

//                    if ((_selectedReport.Usa_Archivo && this._selectedReport.Usa_Columnas_Ancho_Fijo))
//                        columnasDataTable = dbmCore.SchemaConfig.TBL_Reporte_Columna.DBGet(this._selectedReport.Id_Reporte, null/* TODO Change to default(_) if this is not a reference type */);
//                }
//                catch (Exception ex)
//                {
//                    DesktopMessageBoxControl.DesktopMessageShow("Error al conectarse a Core", ex);
//                    return;
//                }
//                finally
//                {
//                    if ((dbmCore != null))
//                        dbmCore.Connection_Close();
//                }


//                bool salto = false;
//                DataBaseManager dbmReporte = null/* TODO Change to default(_) if this is not a reference type */;

//                try
//                {
//                    dbmReporte = new DataBaseManager(_connectionString);
//                    dbmReporte.Connection_Open();

//                    var dataBaseType = DataBaseFactory.GetDataBaseType(_connectionString);

//                    // Realiza los reemplazos de los valores.
//                    var cSql = ReemplazarParametros(this._selectedReport.Consulta, "");


//                    // Funcionalidad para cargar informacion a una tabla temporal
//                    if ((_selectedReport.Usa_Archivo))
//                    {
//                        if ((dataBaseType != dataBaseType.SqlServer))
//                            throw new Exception("La opción [Usa Archivo] solo es compatible con bases de datos SQL Server");

//                        string nFileName = "";

//                        dataFile = CargarFile(this._selectedReport.Usa_Columnas_Ancho_Fijo, columnasDataTable, nFileName);
//                        nFileName = "'" + Path.GetFileName(nFileName) + "'";
//                        cSql = cSql.ToString().Replace("$FileName", nFileName);

//                        if ((dataFile == null))
//                            return;
//                    }

//                    // Logica exclusiva para la funcionalidad de custodia de cajas
//                    if (ReportesTreeView.SelectedNode.Name == "2")
//                    {
//                        cSql = cSql.ToString().Replace("@Sede", Program.DesktopGlobal.PuestoTrabajoRow.fk_Sede.ToString);
//                        cSql = cSql.ToString().Replace("@Boveda", Program.DesktopGlobal.BovedaRow.id_Boveda.ToString);
//                    }

//                    // Ejecuta la sentencia
//                    try
//                    {
//                        if ((this._selectedReport.Usa_Archivo))
//                            BulkInsert.InsertDataTableReport(dataFile, dbmReporte, "#Report");
//                    }
//                    catch (Exception ex)
//                    {
//                        throw new Exception("Error al cargar el archivo a la base de datos, la configuración es diferente al esquema del archivo seleccionado." + ex.Message);
//                    }

//                    // Consulta de confirmación
//                    if ((this._selectedReport.Usa_Consulta_Confirmacion))
//                        salto = ConsultaConfirmacion(dbmReporte);

//                    // Consulta usa exportar masivo
//                    if (!(_selectedReport.Usa_Exportar_Masivo))
//                    {
//                        ExportarMasivoButton.Visible = false;
//                        if ((salto))
//                            return;

//                        resultadosPanel.Controls.Clear();

//                        if ((this._selectedReport.Exportar_Texto_Plano))
//                            LeerDataReader(dbmReporte, dataBaseType, cSql);
//                        else
//                            LeerDataSet(dbmReporte, dataBaseType, titulosDataTable, cSql);
//                    }
//                    else
//                    {
//                        ExportarMasivoButton.Visible = true;
//                        LeerDataSetUsaMasivo(dbmReporte, dataBaseType, titulosDataTable, cSql);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    DesktopMessageBoxControl.DesktopMessageShow("EjecutarSentencia", ex);
//                    dbmReporte.Transaction_Rollback();
//                }
//                finally
//                {
//                    if ((dbmReporte != null))
//                        dbmReporte.Connection_Close();
//                }
//            }
//            else
//                DesktopMessageBoxControl.DesktopMessageShow("Se debe seleccionar un reporte para obtener los resultados.", "Selección de Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
//        }

//        private void LeerDataSetUsaMasivo(DataBaseManager dbmReporte, DataBaseType dataBaseType, DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable titulosDataTable, string cSql)
//        {
//            DataSet resultados = null/* TODO Change to default(_) if this is not a reference type */;
//            try
//            {
//                resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }

//            if (resultados.Tables.Count > 0)
//            {
//                for (var i = resultados.Tables.Count; i >= 1; i += -1)
//                {
//                    DataTable tabla;

//                    tabla = resultados.Tables(i - 1);
//                    if (tabla.Rows.Count() > 0)
//                    {
//                        var nuevaGrilla = new DesktopReportDataGridViewControl();
//                        nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core;
//                        nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools;


//                        if (resultados.Tables.Count == 1)
//                            nuevaGrilla.Dock = DockStyle.Fill;
//                        else
//                            nuevaGrilla.Dock = DockStyle.Top;
//                        short index;
//                        index = System.Convert.ToInt16(resultados.Tables.IndexOf(tabla) + 1);

//                        var titulo = titulosDataTable.Select("Id_Reporte_Salida = '" + tabla.Rows.Item(0).ItemArray(0).ToString() + "'");

//                        nuevaGrilla.Titulo = tabla.Rows.Item(0).ItemArray(0).ToString();
//                        nuevaGrilla.Id_Reporte = this._selectedReport.Id_Reporte;
//                        if (!this._selectedReport == null)
//                            nuevaGrilla.Salto_Linea = this._SelectedTipoSalidaReporte.Codigo_Salto_Linea;
//                        else
//                            nuevaGrilla.Salto_Linea = "";

//                        if (!_selectedReport.Muestra_Columna_Nombre_Reporte_Masivo)
//                        {
//                            DataTable tabla2 = null/* TODO Change to default(_) if this is not a reference type */;
//                            tabla2 = tabla.Copy;
//                            tabla2.Columns.Remove(tabla2.Columns(0));
//                            tabla2.AcceptChanges();

//                            nuevaGrilla.InternalGridView.DataSource = tabla2;
//                        }
//                        else
//                            nuevaGrilla.InternalGridView.DataSource = tabla;

//                        nuevaGrilla.ButtonOnClickReportMas_ = false;
//                        resultadosPanel.Controls.Add(nuevaGrilla);
//                    }
//                    else
//                    {
//                        ExportarMasivoButton.Visible = false;
//                        DesktopMessageBoxControl.DesktopMessageShow("No hay registros para la fecha seleccionada.", "Selección de Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
//                    }
//                }
//            }
//            else
//            {
//                ExportarMasivoButton.Visible = false;
//                DesktopMessageBoxControl.DesktopMessageShow("No hay registros para la fecha seleccionada.", "Selección de Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
//            }
//        }

//        private void LeerDataSet(DataBaseManager dbmReporte, DataBaseType dataBaseType, DBCore.SchemaConfig.TBL_Reporte_SalidaDataTable titulosDataTable, string cSql)
//        {
//            DataSet resultados = null/* TODO Change to default(_) if this is not a reference type */;
//            try
//            {
//                // dbmReporte.Transaction_Begin()

//                // resultados.Tables.Add(dbmReporte.DataBase.ExecuteQueryGet(cSql))
//                resultados = SqlData.ExecuteQuery(cSql, dbmReporte, dataBaseType);
//            }

//            // dbmReporte.Transaction_Commit()
//            catch (Exception ex)
//            {
//                // dbmReporte.Transaction_Rollback()
//                throw;
//            }

//            for (var i = resultados.Tables.Count; i >= 1; i += -1)
//            {
//                DataTable tabla;

//                tabla = resultados.Tables(i - 1);
//                var nuevaGrilla = new DesktopReportDataGridViewControl();
//                nuevaGrilla.Conection_String_Core = Program.DesktopGlobal.ConnectionStrings.Core;
//                nuevaGrilla.Conection_String_Tools = Program.DesktopGlobal.ConnectionStrings.Tools;


//                if (resultados.Tables.Count == 1)
//                    nuevaGrilla.Dock = DockStyle.Fill;
//                else
//                    nuevaGrilla.Dock = DockStyle.Top;
//                short index;
//                index = System.Convert.ToInt16(resultados.Tables.IndexOf(tabla) + 1);

//                var titulo = titulosDataTable.Select("Id_Reporte_Salida = '" + index + "'");
//                if ((titulo.Length > 0))
//                {
//                    var tituloRow = (DBCore.SchemaConfig.TBL_Reporte_SalidaRow)titulo(0);
//                    nuevaGrilla.Titulo = FormatearNombreSalidaArchivo(tituloRow.Titulo_Salida);
//                }
//                else
//                    nuevaGrilla.Titulo = "Tabla - " + System.Convert.ToHexString(index);

//                nuevaGrilla.Id_Reporte = this._selectedReport.Id_Reporte;
//                if (!this._selectedReport == null)
//                    nuevaGrilla.Salto_Linea = this._SelectedTipoSalidaReporte.Codigo_Salto_Linea;
//                else
//                    nuevaGrilla.Salto_Linea = "";

//                nuevaGrilla.InternalGridView.DataSource = tabla;
//                resultadosPanel.Controls.Add(nuevaGrilla);
//            }
//        }

//        private void LeerDataReader(DataBaseManager dbmReporte, DataBaseType dataBaseType, string cSql)
//        {
//            var selector = new SaveFileDialog();
//            selector.Filter = "Archivo de texto plano delimitado (*.csv)|*.csv";

//            var respuesta = selector.ShowDialog();

//            if ((respuesta != DialogResult.OK))
//                return;

//            var fileName = selector.FileName;

//            dbmReporte.Transaction_Begin();

//            var resultados = SqlData.ExecuteReader(cSql, dbmReporte, dataBaseType);

//            using (var archivo = new StreamWriter(fileName))
//            {
//                var columnas = resultados.GetSchemaTable();

//                // Crear los encabezados
//                for (var col = 0; col <= columnas.Rows.Count - 1; col++)
//                {
//                    if ((col > 0))
//                        archivo.Write(Constants.vbTab);
//                    archivo.Write(columnas.Rows(col)("ColumnName").ToString());
//                }
//                archivo.WriteLine();

//                // Escribir la data
//                while ((resultados.Read()))
//                {
//                    for (var col = 0; col <= columnas.Rows.Count - 1; col++)
//                    {
//                        if ((col > 0))
//                            archivo.Write(Constants.vbTab);
//                        archivo.Write(resultados.GetString(col));
//                    }
//                    archivo.WriteLine();
//                }
//            }

//            resultados.Close();

//            dbmReporte.Transaction_Commit();

//            MessageBox.Show("El reporte se exportó exitosamente", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        private DataTable CargarFile(bool nUsaColumnasAnchoFijo, DBCore.SchemaConfig.TBL_Reporte_ColumnaDataTable nColumnas, ref string nFileName)
//        {
//            try
//            {
//                OpenFileDialog archivoCargue = new OpenFileDialog();
//                archivoCargue.Title = "Por favor seleccione el archivo a cargar";
//                archivoCargue.Filter = "Archivos de texto plano|*.csv; *.txt; *.*";

//                if ((archivoCargue.ShowDialog() == DialogResult.OK))
//                {
//                    DBCore.CSVData csvData = new DBCore.CSVData(); // Slyg.Tools.CSV.CSVData()
//                    char caracter;

//                    csvData.LinesToJump = this._selectedReport.Filas_Omitir;

//                    if ((nUsaColumnasAnchoFijo))
//                    {
//                        if ((nColumnas.Count == 0))
//                            throw new Exception("No se encuentra la configuración para el manejo del archivo, falta la definición de columnas, por favor comuniquese con el administrador para que la configure.");

//                        var definiciones = new List<DBCore.CSVLoadColumnDefinition>();

//                        foreach (var column in nColumnas)
//                            definiciones.Add(new DBCore.CSVLoadColumnDefinition(column.Nombre_Columna, column.Inicio_Columna, column.Longitud_Columna));

//                        csvData.HasHeader = this._selectedReport.Maneja_Encabezado;
//                        csvData.LoadCSV(archivoCargue.FileName, definiciones);
//                    }
//                    else
//                    {
//                        if ((this._selectedReport.IsCaracter_SeparadoNull() | this._selectedReport.IsIdentificador_TextoNull()))
//                            throw new Exception("No se encuentra la configuración para el manejo del archivo, por favor comuniquese con el administrador para que la configure.");

//                        switch (this._selectedReport.Caracter_Separado)
//                        {
//                            case ";":
//                                {
//                                    caracter = ';';
//                                    break;
//                                }

//                            case ",":
//                                {
//                                    caracter = Strings.Chr(44);
//                                    break;
//                                }

//                            case "TAB":
//                                {
//                                    caracter = Strings.Chr(9);
//                                    break;
//                                }

//                            case " ":
//                                {
//                                    caracter = Strings.Chr(32);
//                                    break;
//                                }

//                            default:
//                                {
//                                    caracter = Strings.Chr(0);
//                                    break;
//                                }
//                        }

//                        csvData.LoadCSV(archivoCargue.FileName, this._selectedReport.Maneja_Encabezado, caracter, this._selectedReport.Identificador_Texto);
//                    }

//                    nFileName = archivoCargue.FileName;

//                    return csvData.DataTable.ToDataTable();
//                }
//                else
//                    return null/* TODO Change to default(_) if this is not a reference type */;
//            }
//            catch (Exception ex)
//            {
//                DesktopMessageBoxControl.DesktopMessageShow("Cargar Archivo", ex);
//                return null/* TODO Change to default(_) if this is not a reference type */;
//            }
//        }

//        private string ReemplazarParametros(string nSql, string nToIgnore)
//        {
//            string WordCancelados = Strings.Mid(nSql, 1, 46);
//            bool ProcesoCancelados;

//            if ((WordCancelados == "EXEC [Report].[PA_Ejecutar_Cancelados_Informe]"))
//                ProcesoCancelados = true;

//            if (ProcesoCancelados == true)
//                nSql = nSql.ToString().Replace("@UsuarioAccion", "'" + Program.Sesion.Usuario.Login + "'");

//            foreach (Control parameterControl in camposPanel.Controls("ControlesTableLayoutPanel").Controls)
//            {
//                if ((parameterControl.GetType().GetInterfaces().Contains(typeof(IParameter))))
//                {
//                    var parameter = (IParameter)parameterControl;

//                    if ((parameter.ParameterName != nToIgnore))
//                        nSql = nSql.ToString().Replace(parameter.ParameterName, parameter.GetStringParameter());
//                }
//            }
//            return nSql;
//        }

//        private bool ConsultaConfirmacion(DataBaseManager dbmReporte)
//        {
//            if ((!this._selectedReport.IsConsulta_ConfirmacionNull() && this._selectedReport.Consulta_Confirmacion != ""))
//            {
//                FormConfirmacionReporte consultaConfirmacionForm = new FormConfirmacionReporte(Program.DesktopGlobal.ConnectionStrings.Core, Program.DesktopGlobal.ConnectionStrings.Tools);

//                var cSqlC = ReemplazarParametros(this._selectedReport.Consulta_Confirmacion, "");

//                consultaConfirmacionForm.Consulta = cSqlC;
//                consultaConfirmacionForm.Conexion = dbmReporte;

//                if ((!consultaConfirmacionForm.ShowDialog == DialogResult.OK))
//                    return true;
//                else if ((consultaConfirmacionForm.ResultadosDataGridView.InternalGridView.RowCount == 0))
//                {
//                    DesktopMessageBoxControl.DesktopMessageShow("Se enco tro algún error en el archivo, no se puede continuar", "Consulta Confirmación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon);
//                    return true;
//                }
//            }
//            else
//            {
//                DesktopMessageBoxControl.DesktopMessageShow("Aunque esta configurado para que haya consulta de confirmación, no hay ninguna consulta para evaluar. Por favor contacte a su administrador para que se configure correctamente.", "Consulta Confirmación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon);
//                return true;
//            }

//            return false;
//        }
        
        //


        //funciones

        //private static string GetBasicConnectionString(string nConnectionString)
        //{
        //    var partes = nConnectionString.Split(';');
        //    string result = "";

        //    foreach (var parte in partes)
        //    {
        //        if ((!parte.ToUpper().StartsWith("SLYGPROVIDER")))
        //            result += parte + ";";
        //    }

        //    return result;
        //}

        //private string FormatearNombreSalidaArchivo(string FileName)
        //{
        //    string result = "";
        //    DateTime Fecha = DateTime.Now;

        //    string[] strArr;
        //    int count;

        //    strArr = FileName.Split(System.Convert.ToChar("#"));
        //    for (count = 0; count <= strArr.Length - 1; count++)
        //    {
        //        switch (strArr[count])
        //        {
        //            case "yMMdd":
        //                {
        //                    result = result + strArr[count].ToString().Replace(strArr[count], Fecha.ToString("yMMdd").ToString());
        //                    break;
        //                }

        //            case "yyMMdd":
        //                {
        //                    result = result + strArr[count].ToString().Replace(strArr[count], Fecha.ToString("yyMMdd").ToString());
        //                    break;
        //                }

        //            case "hhmmss":
        //                {
        //                    result = result + strArr[count].ToString().Replace(strArr[count], Fecha.ToString("HHmmss").ToString());
        //                    break;
        //                }

        //            case "yyyy/MM/dd":
        //                {
        //                    result = result + strArr[count].ToString().Replace(strArr[count], Fecha.ToString("yyyy/MM/dd").ToString());
        //                    break;
        //                }

        //            case "dd/MM/yyyy":
        //                {
        //                    result = result + strArr[count].ToString().Replace(strArr[count], Fecha.ToString("dd/MM/yyyy").ToString());
        //                    break;
        //                }

        //            case "yyyyMMdd":
        //                {
        //                    result = result + strArr[count].ToString().Replace(strArr[count], Fecha.ToString("yyyyMMdd").ToString());
        //                    break;
        //                }

        //            default:
        //                {
        //                    result = result + strArr[count];
        //                    break;
        //                }
        //        }
        //    }
        //    return result;
        //}

        ////
        public FormVisorReportes()
        {
            InitializeComponent();
        }
    }
}
