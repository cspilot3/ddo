using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Slyg.Tools.Imaging;
using Miharu.Desktop.Library.Config;
using Slyg.Tools;
using Miharu.FileProvider.Library;
using DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl;
using Miharu.Tools.Progress;

namespace BcoFalabella.Plugin.Imaging.Embargos.Forms
{
    public partial class FormExportar : Form
    {
        #region " Declaraciones "

        private bool Usa_Exportacion_PDF;
        private Slyg.Tools.Imaging.ImageManager.EnumFormat formatoAux;
        private Slyg.Tools.Imaging.ImageManager.EnumCompression CompresionAux;
        private Slyg.Tools.Imaging.ImageManager.EnumFormat formato;
        private EmbargosPlugin _Plugin;

        Slyg.Tools.Imaging.ImageManager.EnumCompression compresion;
        private DataView ViewExpedientes = new DataView();
        public static List<string> FileNamesCons = new List<string>();

        #endregion

        public FormExportar(EmbargosPlugin _plugin)
        {
            InitializeComponent();
            this._Plugin = _plugin;
        }
    }
}
