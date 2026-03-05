using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Miharu.Desktop.Library.Plugins;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Library;
using System.Reflection;
using System.Drawing;
using Miharu.Desktop.Controls.DesktopMessageBox;
using BcoItau.Plugin.Imaging.Valle.Forms;

namespace Imaging.Valle
{
    public class FormImagingWorkSpaceWrapperValle
    {
        #region "Declaraciones"

        public VallePlugin _plugin = null;
        TabPage pageReportes = new TabPage();
        private bool hideReporte = false;
        public ToolStripMenuItem BancoItauToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem PublicacionDatosToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem CruceDatosToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem ExportacionImagenesToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem ReportesImpuestosToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem CargueLogToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem ConfiguracionToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem RelacionCuentasMunicipiosToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem AseguradorasToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem DispersionFormToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem CruceFormulario_ValorToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem CruceMedioPagoToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem DevolucionesToolStripMenuItem = new ToolStripMenuItem();
        

        #endregion

        #region " Constructores "
        public FormImagingWorkSpaceWrapperValle(VallePlugin nPlugin)
        {
            _plugin = nPlugin;
        }
        #endregion

        #region " Metodos "
        public void AplicarCambios()
        {
            try
            {
                ////Menu Carpeta Unica Padre
                BancoItauToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {CargueLogToolStripMenuItem,
                    PublicacionDatosToolStripMenuItem,CruceDatosToolStripMenuItem,ExportacionImagenesToolStripMenuItem,ReportesImpuestosToolStripMenuItem, ConfiguracionToolStripMenuItem,
                    DispersionFormToolStripMenuItem,DevolucionesToolStripMenuItem});

                BancoItauToolStripMenuItem.Name = "BancoItauToolStripMenuItem";
                BancoItauToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                BancoItauToolStripMenuItem.Text = "Banco Itau ...";
                BancoItauToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.Path);

                ////Menu Hijo Publicación Datos - Padre(Bco Bogota)
                PublicacionDatosToolStripMenuItem.Name = "PublicacionDatosToolStripMenuItem";
                PublicacionDatosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                PublicacionDatosToolStripMenuItem.Text = "Prepapar Data";
                PublicacionDatosToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.PublicacionDatos);
                PublicacionDatosToolStripMenuItem.Click += PublicacionDatosToolStripMenuItem_Click;

                //Menu Hijo Cruce - Padre(Bco Bogota)
                CruceDatosToolStripMenuItem.Name = "CruceDatosToolStripMenuItem";
                CruceDatosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                CruceDatosToolStripMenuItem.Text = "Cruce Datos";
                CruceDatosToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.CruceDatos);
                CruceDatosToolStripMenuItem.DropDownItems.Add(CruceFormulario_ValorToolStripMenuItem);
                CruceDatosToolStripMenuItem.DropDownItems.Add(CruceMedioPagoToolStripMenuItem);

                ////Menu Hijo Exportación Imagenes - Padre(Bco Bogota)
                ExportacionImagenesToolStripMenuItem.Name = "ExportacionImagenesStripMenuItem";
                ExportacionImagenesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                ExportacionImagenesToolStripMenuItem.Text = "Exportar Imagenes";
                ExportacionImagenesToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                ExportacionImagenesToolStripMenuItem.Click += ExportacionImagenesToolStripMenuItem_Click;

                ////Menu Hijo Reportes Impuestos - Padre(Bco Bogota)
                ReportesImpuestosToolStripMenuItem.Name = "ReportesImpuestosToolStripMenuItem";
                ReportesImpuestosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                ReportesImpuestosToolStripMenuItem.Text = "Reportes Impuestos";
                ReportesImpuestosToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                ReportesImpuestosToolStripMenuItem.Click += ReportesImpuestosToolStripMenuItem_Click;

                ////Menu Hijo Cargue Log - Padre(Bco Bogota)
                CargueLogToolStripMenuItem.Name = "CargueLogToolStripMenuItem";
                CargueLogToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                CargueLogToolStripMenuItem.Text = "Cargue Logs";
                CargueLogToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                CargueLogToolStripMenuItem.Click += CargueLogToolStripMenuItem_Click;

                ////Menu Hijo Relación Cuentas Municipios - Padre(Configuración)
                RelacionCuentasMunicipiosToolStripMenuItem.Name = "RelacionCuentasMunicipiosToolStripMenuItem";
                RelacionCuentasMunicipiosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                RelacionCuentasMunicipiosToolStripMenuItem.Text = "Relación Cuentas Municipios";
                RelacionCuentasMunicipiosToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                RelacionCuentasMunicipiosToolStripMenuItem.Click += RelacionCuentasMunicipiosToolStripMenuItem_Click;

                ////Menu Hijo Aseguradoras - Padre(Configuración)
                AseguradorasToolStripMenuItem.Name = "AseguradorasToolStripMenuItem";
                AseguradorasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                AseguradorasToolStripMenuItem.Text = "Aseguradoras";
                AseguradorasToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                AseguradorasToolStripMenuItem.Click += AseguradorasToolStripMenuItem_Click;


                ////Menu Hijo Configuración - Padre(Bco Bogota)
                ConfiguracionToolStripMenuItem.Name = "ConfiguracionToolStripMenuItem";
                ConfiguracionToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                ConfiguracionToolStripMenuItem.Text = "Configuración";
                ConfiguracionToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                ConfiguracionToolStripMenuItem.DropDownItems.Add(RelacionCuentasMunicipiosToolStripMenuItem);
                ConfiguracionToolStripMenuItem.DropDownItems.Add(AseguradorasToolStripMenuItem);

                ////Menu Hijo Dispersión Form - Padre(Bco Bogota)
                DispersionFormToolStripMenuItem.Name = "DispersionFormToolStripMenuItem";
                DispersionFormToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                DispersionFormToolStripMenuItem.Text = "Relación Cuentas Dispersión";
                DispersionFormToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                DispersionFormToolStripMenuItem.Click += DispersionFormToolStripMenuItem_Click;

                ////Menu Hijo Dispersión Form - Padre(Bco Bogota)
                CruceFormulario_ValorToolStripMenuItem.Name = "CruceFormulario_ValorToolStripMenuItem";
                CruceFormulario_ValorToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                CruceFormulario_ValorToolStripMenuItem.Text = "Cruce por Formulario - Cruce por Valor";
                CruceFormulario_ValorToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                CruceFormulario_ValorToolStripMenuItem.Click += CruceDatosToolStripMenuItem_Click;

                ////Menu Hijo Dispersión Form - Padre(Bco Bogota)
                CruceMedioPagoToolStripMenuItem.Name = "CruceMedioPago_ValorToolStripMenuItem";
                CruceMedioPagoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                CruceMedioPagoToolStripMenuItem.Text = "Cruce por Medio de Pago";
                CruceMedioPagoToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                CruceMedioPagoToolStripMenuItem.Click += CruceMedioPagoToolStripMenuItem_Click;

                ////Menu Hijo Dispersión Form - Padre(Bco Bogota)
                DevolucionesToolStripMenuItem.Name = "DevolucionesToolStripMenuItem";
                DevolucionesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                DevolucionesToolStripMenuItem.Text = "Devoluciones";
                DevolucionesToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoBogota.ExportacionImagenes);
                DevolucionesToolStripMenuItem.Click += DevolucionesToolStripMenuItem_Click;

 
                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(new ToolStripItem[] { BancoItauToolStripMenuItem });


            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banco Itau - Impuestos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void DeshacerCambios()
        {

            try
            {
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Banco de Itau-Impuestos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }
        #endregion

        #region "Eventos"
        public void DevolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmDevoluciones = new frmDevoluciones(this._plugin);
            frmDevoluciones.ShowDialog();
        }

        public void PublicacionDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmPublicarDatos = new frmPublicacionDatos(this._plugin); 
            frmPublicarDatos.ShowDialog();
        }

        public void CruceDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmCruzarDatos = new frmCruzarDatos(this._plugin, "FORMULARIO");
            frmCruzarDatos.ShowDialog();
        }

        public void CruceMedioPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmCruzarDatos = new frmCruzarDatos(this._plugin, "MEDIO_PAGO");
            frmCruzarDatos.ShowDialog();
        }

        public void ExportacionImagenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmExportacionImagenes = new frmExportacionImagenes(this._plugin, "IMAGENES_FECHA_RECAUDO");
            frmExportacionImagenes.ShowDialog();
        }

        public void CargueLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmCargueLog = new frmCargueLog(_plugin);
            frmCargueLog.ShowDialog();
        }

        public void MenuCerrar_Click(object sender, EventArgs e)
        {
            _plugin.WorkSpace.MainTabControl.Controls.Remove(pageReportes);
        }

        public void RelacionCuentasMunicipiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmRelacionCuentasMunicipio = new frmRelacionCuentasMunicipio(this._plugin);
            frmRelacionCuentasMunicipio.ShowDialog();
        }

        public void AseguradorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmRelacionAseguradoras = new frmRelacionAseguradoras(this._plugin);
            frmRelacionAseguradoras.ShowDialog();
        }

        public void DispersionFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            dynamic frmRelacion_CuentasDispersadas = new frmRelacion_CuentasDispersadas(this._plugin);
            frmRelacion_CuentasDispersadas.ShowDialog();
        }

        public void ReportesImpuestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quitarTabPage();
            frmReportesImpuestos frmReportesImpuestos = new frmReportesImpuestos(this._plugin);
            ContextMenu ContMen = new ContextMenu();
            MenuItem Cerrar = new MenuItem();
            Cerrar.Text = "Cerrar";
            Cerrar.Click += new EventHandler(MenuCerrar_Click);
            ContMen.MenuItems.Add(Cerrar);

            pageReportes.Text = "Reportes Impuestos";
            pageReportes.Name = "ReporteImpuestos";
            pageReportes.ContextMenu = ContMen;
            frmReportesImpuestos.TopLevel = false;
            frmReportesImpuestos.Parent = pageReportes;
            frmReportesImpuestos.Visible = true;
            frmReportesImpuestos.Dock = DockStyle.Fill;
            _plugin.WorkSpace.MainTabControl.Controls.Add(pageReportes);
            _plugin.WorkSpace.MainTabControl.SelectedTab = pageReportes;

            frmReportesImpuestos.Show();
             
        }

        private void quitarTabPage()
        {
            bool encontrado = false;

            foreach (Control item in _plugin.WorkSpace.MainTabControl.Controls)
            {
                if (item.GetType() == typeof(TabPage) && item.Name == "ReporteImpuestos")
                {
                    foreach (Control ControlInPage in item.Controls)
                    {
                        if (ControlInPage.GetType() == typeof(frmReportesImpuestos))
                        {
                            item.Controls.Remove(ControlInPage);
                            this.hideReporte = true;
                            //var con = (frmReportesImpuestos)ControlInPage;
                            //con.Hide();
                            encontrado = true;
                        }
                    }
                    if (encontrado)
                    {
                        _plugin.WorkSpace.MainTabControl.Controls.Remove(item);
                        return;
                    }
                }
            }
        }

        #endregion

    }
}
