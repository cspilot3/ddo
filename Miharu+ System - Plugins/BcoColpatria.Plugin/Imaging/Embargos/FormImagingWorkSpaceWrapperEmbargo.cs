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
using BcoColpatria.Plugin.Imaging.Embargos.Form;

namespace BcoColpatria.Plugin.Imaging.Embargos
{
    public class FormImagingWorkSpaceWrapperEmbargo
    {
        #region "Declaraciones"

        public EmbargosPlugin _plugin = null;
        public ToolStripMenuItem BancoColpatriaToolStripMenuItem = new ToolStripMenuItem();
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
                BancoColpatriaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { GenerarCartasToolStripMenuItem });
                BancoColpatriaToolStripMenuItem.Name = "BancoColpatriaToolStripMenuItem";
                BancoColpatriaToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                BancoColpatriaToolStripMenuItem.Text = "Colpatria - Embargos...";

                ////Menu Hijo Exportar - Padre(Bco Colpatria)
                GenerarCartasToolStripMenuItem.Name = "GenerarCartasToolStripMenuItem";
                GenerarCartasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
                GenerarCartasToolStripMenuItem.Text = "Generar Cartas";
                //ExportarToolStripMenuItem.Visible = _plugin.Manager.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.BancoPopular.Reportes);
                GenerarCartasToolStripMenuItem.Click += GenerarCartasToolStripMenuItem_Click;

                _plugin.WorkSpace.MainMenuStrip.Items.AddRange(new ToolStripItem[] { BancoColpatriaToolStripMenuItem });
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible aplicar los cambios de Banco Colpatria-Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void DeshacerCambios()
        {

            try
            {
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("No fue posible deshacer los cambios de Banco Colpatria-Embargos al workspace, " + ex.Message, "Plugin workspace", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
            }
        }

        public void GenerarCartasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGenerarCartas FormGenerarCartas = new FormGenerarCartas(this._plugin);


            FormGenerarCartas.Show();
        }
        #endregion
    }
}
