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
using BcoCoopcentral.Plugin.Imaging.Embargos.Form;

namespace BcoCoopcentral.Plugin.Imaging.Embargos
{
    public class FormImagingWorkSpaceWrapperEmbargo
    {
        #region "Declaraciones"

        public EmbargosPlugin _plugin = null;
        public ToolStripMenuItem BcoCoopcentralToolStripMenuItem = new ToolStripMenuItem();
        public ToolStripMenuItem GenerarCartasToolStripMenuItem = new ToolStripMenuItem();

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
                ////Menu Carpeta Unica Padre
                BcoCoopcentralToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { GenerarCartasToolStripMenuItem });

                BcoCoopcentralToolStripMenuItem.Name = "BcoCoopcentralToolStripMenuItem";
                BcoCoopcentralToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                BcoCoopcentralToolStripMenuItem.Text = "Banco Coopcentral...";

                ////Menu Hijo Generar Cartas- Padre(Banco Coopcentral...)
                GenerarCartasToolStripMenuItem.Name = "GenerarCartasToolStripMenuItem";
                GenerarCartasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                GenerarCartasToolStripMenuItem.Text = "Generar Cartas";
                GenerarCartasToolStripMenuItem.Click += GenerarCartasToolStripMenuItem_Click;

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(new ToolStripItem[] { BcoCoopcentralToolStripMenuItem });
                
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banco Coopcentral-Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void DeshacerCambios()
        {

            try
            {
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Banco Coopcentral-Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }
        #endregion

        public void GenerarCartasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var formGenerarCartas = new FormGenerarCartas(this._plugin);
                formGenerarCartas.ShowDialog();
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Exportar Imagenes", ref ex);
            }
        }
    }
}
