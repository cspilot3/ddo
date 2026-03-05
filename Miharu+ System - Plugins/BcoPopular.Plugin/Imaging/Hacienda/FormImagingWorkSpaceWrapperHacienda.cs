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
using BcoPopular.Plugin.Imaging.Hacienda.Forms;

namespace Imaging.Hacienda
{
    public class FormImagingWorkSpaceWrapperHacienda
    {
        #region "Declaraciones"

        public HaciendaPlugin _plugin = null;
        TabPage pageReportes = new TabPage();
        private bool hideReporte = false;
        public ToolStripMenuItem BancoPopularToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem ReportesToolStripMenuItem = new ToolStripMenuItem();
        public Button _exportarImagenesButton;

        private Miharu.Imaging.Library.Eventos.EventManager _eventManager;

        #endregion

        #region " Constructores "
        public FormImagingWorkSpaceWrapperHacienda(HaciendaPlugin nPlugin)
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
                BancoPopularToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {ReportesToolStripMenuItem});

                BancoPopularToolStripMenuItem.Name = "BancoPopularToolStripMenuItem";
                BancoPopularToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                BancoPopularToolStripMenuItem.Text = "Banco Popular...";
                BancoPopularToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoPopular.Path);

                ////Menu Hijo Publicación Datos - Padre(Bco Popular)
                ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem";
                ReportesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                ReportesToolStripMenuItem.Text = "Reportes";
                ReportesToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoPopular.Reportes);
                ReportesToolStripMenuItem.Click += ReportesToolStripMenuItem_Click;               

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(new ToolStripItem[] { BancoPopularToolStripMenuItem });

                //PluginHelper.DisableControl(_plugin.WorkSpace.ExportarButton);
                //this._exportarImagenesButton = PluginHelper.CloneButton(_plugin.WorkSpace.ExportarButton);
                //PluginHelper.ReplaceControl(_plugin.WorkSpace.ExportarButton, _exportarImagenesButton);
                //_exportarImagenesButton.Visible = true;
                //_exportarImagenesButton.Enabled = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeConsultar(Permisos.Imaging.Proceso.Indexacion.Cargue);
                //this._exportarImagenesButton.Click += CargueButton_Click;
                //PluginHelper.DisableControl(_plugin.WorkSpace.ExportarButton);

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banco de Popular-Impuestos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void DeshacerCambios()
        {

            try
            {
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Banco de Popular-Impuestos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        private void ExportarImagenes()
        {
            if ((!_exportarImagenesButton.Visible | !_exportarImagenesButton.Enabled))
                return;
            try
            {
                var formExportar = new FormExport(this._plugin);
                formExportar.EventManager = this._eventManager;
                formExportar.ShowDialog();
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Exportar Imagenes",ref ex);
            }
        }

        #endregion

        #region "Eventos"
        
        public void ReportesToolStripMenuItem_Click(object sender, EventArgs e)
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

        public void CargueButton_Click(object sender, EventArgs e)
        {
            ExportarImagenes();
        }

        public void MenuCerrar_Click(object sender, EventArgs e)
        {
            _plugin.WorkSpace.MainTabControl.Controls.Remove(pageReportes);
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
                        //if (ControlInPage.GetType() == typeof(frmReportesImpuestos))
                        //{
                        //    item.Controls.Remove(ControlInPage);
                        //    this.hideReporte = true;
                        //    //var con = (frmReportesImpuestos)ControlInPage;
                        //    //con.Hide();
                        //    encontrado = true;
                        //}
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
