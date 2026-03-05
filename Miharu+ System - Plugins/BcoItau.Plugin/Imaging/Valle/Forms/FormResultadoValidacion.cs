using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Desktop.Library.Config;

namespace BcoItau.Plugin.Imaging.Valle.Forms
{
    public partial class FormResultadoValidacion : Form
    {

        public FormResultadoValidacion(DesktopConfig.TypeResult trErrores)
        {
	        InitializeComponent();

	        //Carga detalle errores
	        string msgRespuestaCargue = "";
	        //Dim countErros As Integer = 0
	        //Dim ErroresMayores100 As Boolean = False

	        if (trErrores.Parameters.Count > 0) {
		        for (int index = 0; index <= trErrores.Parameters.Count - 1; index++) {
			        msgRespuestaCargue = msgRespuestaCargue + (index + 1).ToString() + ". " + trErrores.Parameters[index].ToString() +  Environment.NewLine;

			        if ((index >= 100)) {
				        break; // TODO: might not be correct. Was : Exit For
			        }
		        }

		        ResultadosRichTextBox.Text = msgRespuestaCargue;
	        }

	        //Carga Resumen de errores
	        ValidosDesktopTextBox.Text = trErrores.Resumen.Valido.ToString();
	        NoValidosDesktopTextBox.Text = trErrores.Resumen.NoValido.ToString();

	        EsquemaNoValidoDesktopTextBox.Text = trErrores.Resumen.EsquemaNoValido.ToString();
	        TipoDocumentoNoValidoDesktopTextBox.Text = trErrores.Resumen.TipoDocumentoNoValido.ToString();
	        ClaseRegistroNoValidoDesktopTextBox.Text = trErrores.Resumen.ClaseRegistroNoValido.ToString();
	        DevolucionSinCodigoBarrasDesktopTextBox.Text = trErrores.Resumen.DevolucionSinCodigoBarras.ToString();
	        AdicionConLlavesInexistentesDesktopTextBox.Text = trErrores.Resumen.AdicionConLlavesInexistentes.ToString();
	        NuevoConLlavesInexistentesDesktopTextBox.Text = trErrores.Resumen.NuevoConLlavesInexistentes.ToString();
	        NumeroLlavesNoCoincideDesktopTextBox.Text = trErrores.Resumen.NumeroLlavesNoCoincide.ToString();
	        TipoDatoLlavesNoCoincideDesktopTextBox.Text = trErrores.Resumen.TipoDatoLlavesNoCoincide.ToString();
	        NumeroCamposDataNoCoincideDesktopTextBox.Text = trErrores.Resumen.NumeroCamposDataNoCoincide.ToString();
	        TipoDatoCamposDataNoCoincideDesktopTextBox.Text = trErrores.Resumen.TipoDatoCamposDataNoCoincide.ToString();
        }

    }
}
