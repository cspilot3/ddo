using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miharu.Imaging.Library.Eventos;
using Imaging.GobernacionAntioquia;

namespace BcoPopular.Plugin.Imaging.GobernacionAntioquia.Forms
{
    public partial class FormExportWeb : Form
    {
        EventManager _EventManager;
        private GobernacionAntioquiaPlugin _Plugin;

        #region " Propiedades"
        public EventManager EventManager
        {
            get { return this._EventManager; }
            set { _EventManager = value; }
        }
        #endregion

        public FormExportWeb()
        {
            InitializeComponent();
        }

        private void ExportarButton_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                RestSharp.RestClient client = new RestSharp.RestClient("http://proapconvenios");
                client.Timeout = (1000 * 60 * 60);
                RestSharp.RestRequest request = new RestSharp.RestRequest("/ServicioExportar/api/create/zip", RestSharp.Method.POST);
                request.Timeout = (1000 * 60 * 60);

                ClassGenerarImagenesApi generarimagenes = new ClassGenerarImagenesApi();

                generarimagenes.entidad = this._Plugin.Manager.ImagingGlobal.Entidad;
                generarimagenes.proyecto = this._Plugin.Manager.ImagingGlobal.Proyecto;
                generarimagenes.fecha_inicial = FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd");
                generarimagenes.fecha_final = FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd");

                request.AddBody(generarimagenes);
                RestSharp.IRestResponse response = client.ExecuteAsPost(request, "POST");

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    MessageBox.Show("Generación éxitosa", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    MessageBox.Show(response.Content, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool validar()
        {
            dynamic FechaInicial = new DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day);
            dynamic FechaFinal = new DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day);

            if ((FechaInicial > FechaFinal))
            {
                MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }else
            {
                //validar que las FP esten cerradas
                DBImaging.DBImagingDataBaseManager dbmImaging = null;

                try
                {
                    dbmImaging = new DBImaging.DBImagingDataBaseManager(this._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging);
                    dbmImaging.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    Int32 Resultado = dbmImaging.SchemaProcess.PA_Consultar_Rango_Fecha_Proceso_Estado.DBExecute(this._Plugin.Manager.ImagingGlobal.Entidad, this._Plugin.Manager.ImagingGlobal.Proyecto, Convert.ToInt32(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), Convert.ToInt32(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd")), false);

                    if (Resultado > 0)
                    {
                        MessageBox.Show("Dentro del rango de fechas seleccionado hay fechas abiertas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if ((dbmImaging != null))
                        dbmImaging.Connection_Close();
                }

            }

            return true;
        }
    }
}
