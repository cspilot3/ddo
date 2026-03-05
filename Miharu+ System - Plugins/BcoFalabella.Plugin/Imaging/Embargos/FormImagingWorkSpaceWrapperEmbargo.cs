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
using BcoFalabella.Plugin.Imaging.Embargos.Forms;

namespace BcoFalabella.Plugin.Imaging.Embargos
{
    public class FormImagingWorkSpaceWrapperEmbargo
    {
        #region "Declaraciones"

        public EmbargosPlugin _plugin = null;
        public ToolStripMenuItem BancoFalabellaToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem ExportarToolStripMenuItem = new ToolStripMenuItem();

        #endregion

        #region " Constructores "
        public FormImagingWorkSpaceWrapperEmbargo(EmbargosPlugin nPlugin)
        {
            _plugin = nPlugin;
        }
        #endregion

        #region " Metodos "
        public void AplicarCambios()
        {
            try
            {
                BancoFalabellaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ExportarToolStripMenuItem });
                BancoFalabellaToolStripMenuItem.Name = "BancoFalabellaToolStripMenuItem";
                BancoFalabellaToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                BancoFalabellaToolStripMenuItem.Text = "Falabella - Embargos...";

                ////Menu Hijo Exportar - Padre(Bco Falabella)
                ExportarToolStripMenuItem.Name = "ExportarToolStripMenuItem";
                ExportarToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                ExportarToolStripMenuItem.Text = "Exportar";
                //ExportarToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoPopular.Reportes);
                ExportarToolStripMenuItem.Click += ExportarToolStripMenuItem_Click;

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(new ToolStripItem[] { BancoFalabellaToolStripMenuItem });
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banco Falabella-Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void DeshacerCambios()
        {

            try
            {
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Banco Falabella-Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
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
