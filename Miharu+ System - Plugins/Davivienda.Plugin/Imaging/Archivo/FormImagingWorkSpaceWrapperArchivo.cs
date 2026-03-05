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
using Davivienda.Plugin.Imaging.Archivo.Forms;

namespace Davivienda.Plugin.Imaging.Archivo
{
    public class FormImagingWorkSpaceWrapperArchivo
    {
        #region "Declaraciones"

        public ArchivoPlugin _plugin = null;
        public ToolStripMenuItem BancoDaviviendaToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem ExportarToolStripMenuItem = new ToolStripMenuItem();

        #endregion

        #region " Constructores "
        public FormImagingWorkSpaceWrapperArchivo(ArchivoPlugin nPlugin)
        {
            _plugin = nPlugin;
        }
        #endregion

        #region " Metodos "
        public void AplicarCambios()
        {
            try
            {
                BancoDaviviendaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ExportarToolStripMenuItem });
                BancoDaviviendaToolStripMenuItem.Name = "BancoDaviviendaToolStripMenuItem";
                BancoDaviviendaToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                BancoDaviviendaToolStripMenuItem.Text = "Davivienda - Archivo...";

                ////Menu Hijo Exportar - Padre(Bco Davivienda)
                ExportarToolStripMenuItem.Name = "ExportarToolStripMenuItem";
                ExportarToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                ExportarToolStripMenuItem.Text = "Exportar";
                //ExportarToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoPopular.Reportes);
                ExportarToolStripMenuItem.Click += ExportarToolStripMenuItem_Click;

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(new ToolStripItem[] { BancoDaviviendaToolStripMenuItem });
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banco Davivienda-Archivo al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void DeshacerCambios()
        {

            try
            {
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Banco Davivienda-Archivo al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void ExportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExportar FormExportar = new FormExportar(this._plugin);


            FormExportar.Show();
        }
        #endregion
    }
}
