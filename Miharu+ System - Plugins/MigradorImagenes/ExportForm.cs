using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Miharu.FileProvider.Library;
using Slyg.Tools.CSV;
using Slyg.Tools.Imaging;
using Slyg.Tools.Imaging.FreeImageAPI;

namespace MigradorImagenes
{
    public delegate void WriteLogDelegate(string nText);
    public delegate void UpdateProgressDelegate(int nValue);
    public delegate void InitProgressDelegate(int nMax);

    public partial class ExportForm : Form
    {
        private const string IndexFilename = "indice.txt";

        private Conversor conversor;

        public ExportForm()
        {
            InitializeComponent();
        }

        private void procesarButton_Click(object sender, EventArgs e)
        {
            if (this.procesarButton.Text == "Procesar")
            {
                this.procesarButton.Text = "Cancelar";
                Procesar();
            }
            else
            {
                conversor.Stop();
                this.procesarButton.Text = "Procesar";
            }
        }

        private void LogClear()
        {
            this.logRichTextBox.Text = "";
        }

        private void LogWrite(string nText)
        {
            if (this.InvokeRequired)
            {
                WriteLogDelegate myDelegate = LogWrite;
                this.Invoke(myDelegate, new Object[] {nText});
            }
            else
            {
                this.logRichTextBox.Text = nText;
                //this.logRichTextBox.AppendText(nText);
                //this.logRichTextBox.Select(this.logRichTextBox.Text.Length, 0);
                //this.logRichTextBox.ScrollToCaret();
            }
        }

        private void LogWriteLine(string nText)
        {
            LogWrite(nText + "\r\n");
        }

        private void InitProgress(int nMax)
        {
            if (this.InvokeRequired)
            {
                InitProgressDelegate myDelegate = InitProgress;
                this.Invoke(myDelegate, new Object[] {nMax});
            }
            else
            {
                this.avanceProgressBar.Minimum = 0;
                this.avanceProgressBar.Value = 0;
                this.avanceProgressBar.Maximum = nMax;
            }
        }

        private void UpdateProgress(int nValue)
        {
            if (this.InvokeRequired)
            {
                UpdateProgressDelegate myDelegate = UpdateProgress;
                this.Invoke(myDelegate, new Object[] {nValue});
            }
            else
            {
                this.avanceProgressBar.Value = nValue;
            }
        }

        private void Procesar()
        {
            LogClear();

            if (!Validar()) return;

            conversor = new Conversor(this.entradaTextBox.Text, this.salidaTextBox.Text, IndexFilename);
            conversor.InitProgress += InitProgress;
            conversor.UpdateProgress += UpdateProgress;
            conversor.WriteLog += LogWrite;
            conversor.WriteLogLine += LogWriteLine;

            conversor.Run();
        }

        private bool Validar()
        {
            if (this.entradaTextBox.Text == "")
            {
                MessageBox.Show("Debe ingresar la ruta de entrada", "Conversor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.entradaTextBox.Focus();
            }
            else if (!Directory.Exists(this.entradaTextBox.Text))
            {
                MessageBox.Show("La ruta de entrada, debe ser una ruta válida", "Conversor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.entradaTextBox.Focus();
                this.entradaTextBox.SelectAll();
            }
            else if (this.salidaTextBox.Text == "")
            {
                MessageBox.Show("Debe ingresar la ruta de salida", "Conversor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.salidaTextBox.Focus();
            }
            else if (!Directory.Exists(this.salidaTextBox.Text))
            {
                MessageBox.Show("La ruta de salida, debe ser una ruta válida", "Conversor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.salidaTextBox.Focus();
                this.salidaTextBox.SelectAll();
            }
            else if (!File.Exists(this.entradaTextBox.Text.TrimEnd('\\') + "\\" + IndexFilename))
            {
                MessageBox.Show("No se encontró el archivo indice.txt en la ruta de entrada", "Conversor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                return true;
            }

            return false;
        }

        private class Conversor
        {
            public event WriteLogDelegate WriteLog;
            public event WriteLogDelegate WriteLogLine;
            public event UpdateProgressDelegate UpdateProgress;
            public event InitProgressDelegate InitProgress;

            private string InputPath { get; set; }
            private string OutputPath { get; set; }
            private string IndexFileName { get; set; }
            
            private const string CounterFileName = "counter.txt";

            private const int MaxThumbnailWidth = 60;
            private const int MaxThumbnailHeight = 80;

            private bool stop;

            public Conversor(string nInputPath, string nOutputPath, string nIndexFileName)
            {
                this.InputPath = nInputPath.TrimEnd('\\') + "\\";
                this.OutputPath = nOutputPath.TrimEnd('\\') + "\\";
                this.IndexFileName = nIndexFileName;
            }

            public void Run()
            {
                this.stop = false;

                var hilo = new Thread(Process);
                hilo.Start();                                
                //Process();
            }

            private void Process()
            {
                WriteLogLine("--------------------------------------------------");
                WriteLogLine("Iniciando proceso: [" + DateTime.Now + "]");
                WriteLogLine("--------------------------------------------------");
                WriteLogLine("");

                int lineaActual = 0;
                var fileProvider = new FileProvider(this.OutputPath);

                // Validar si existe el archivo de salida
                if (File.Exists(this.OutputPath + CounterFileName))
                {
                    using (var counterStreamReader = new StreamReader(this.OutputPath + CounterFileName))
                    {
                        var linea = counterStreamReader.ReadLine();
                        lineaActual = (int)Slyg.Tools.DataConvert.ToNumeric(linea);
                    }
                }
                
                WriteLogLine("Leyendo archivo de indices...");
                WriteLogLine("[" + DateTime.Now + "]");
                
                // Leer el archivo de indices
                var lector = new CSVData("\t", "\"", true);
                lector.LoadCSV(this.InputPath + this.IndexFileName);

                WriteLogLine("Archivo de indices leido");
                WriteLogLine("[" + DateTime.Now + "]");
                WriteLogLine("");

                WriteLogLine("Líneas: [" + lector.DataTable.Rows.Count + "]");
                WriteLogLine("Última línea procesada: [" + lineaActual + "]");
                WriteLogLine("Líneas a procesar: [" + (lector.DataTable.Rows.Count - lineaActual) + "]");
                WriteLogLine("");

                // Inicializar el progreso
                InitProgress(lector.DataTable.Rows.Count - lineaActual);
                
                // Abrir el archivo de log
                var LogFileName = "log-"+ DateTime.Now.ToString("yyyy-MM-dd HHmmss") +".txt";
                using (var logStreamWriter = new StreamWriter(this.OutputPath + LogFileName, true))
                {
                    logStreamWriter.WriteLine("Linea\tArchivo\tEstado\tFolios");

                    for (var contador = lineaActual; contador < lector.DataTable.Rows.Count; contador++)
                    {
                        var linea = lector.DataTable.Rows[contador];

                        if (this.stop)
                        {
                            WriteLogLine("--------------------------------------------------");
                            WriteLogLine("--------------------------------------------------");
                            WriteLogLine("La acción fue cancelada por el usuario");
                            WriteLogLine(DateTime.Now.ToString());
                            return;
                        }

                        try
                        {
                            // Ruta de la imagen de entrada
                            var entidad = short.Parse(linea["fk_Entidad"].ToString());
                            var proyecto = short.Parse(linea["fk_Proyecto"].ToString());
                            var esquema = short.Parse(linea["fk_Esquema"].ToString());
                            var fecha = linea["Fecha_Creacion"].ToString().Substring(0, 10).Replace("-", "");
                            var expediente = long.Parse(linea["id_Expediente"].ToString());
                            var folder = short.Parse(linea["fk_Folder"].ToString());
                            var file = short.Parse(linea["fk_File"].ToString());
                            var version = short.Parse(linea["id_Version"].ToString());

                            var inputFileName = InputPath + "Entidad-" + entidad.ToString("0000") + "\\Proyecto-" + proyecto.ToString("00") + "\\Esquema-" + esquema.ToString("000") + "\\" + fecha + "\\" + expediente + "-" + folder + "-" + file.ToString("000") + ".tif";
                            var msgError = "";
                            var numLinea = (contador + 1).ToString("0000000");

                            WriteLog(numLinea + " -> Imagen: [" + inputFileName + "]");

                            if (!File.Exists(inputFileName))
                            {
                                WriteLogLine(" Faltante");
                                logStreamWriter.WriteLine(lineaActual + "\t" + inputFileName + "\tOK\tFaltante");
                                logStreamWriter.Flush();
                            }
                            else
                            {
                                var folios = ImageManager.GetFolios(inputFileName);

                                using (var bitmap = new FreeImageBitmap(inputFileName))
                                {
                                    var thumbnail = ImageManager.GetThumbnailData(bitmap, 1, folios, MaxThumbnailWidth, MaxThumbnailHeight);

                                    for (short i = 1; i <= folios; i++)
                                    {
                                        var image = ImageManager.GetFolioData(bitmap, i, 1, ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.Ccitt4);

                                        fileProvider.CreateFile("", fecha, expediente, folder, file, version, i, image, thumbnail[i - 1], ref msgError);
                                    }
                                }

                                // Actualizar el archivo de contador
                                lineaActual++;
                                using (var counterStreamWriter = new StreamWriter(this.OutputPath + CounterFileName, false))
                                {
                                    counterStreamWriter.Write(lineaActual);
                                    counterStreamWriter.Close();
                                }

                                // Actualizar log
                                logStreamWriter.WriteLine(lineaActual + "\t" + inputFileName + "\tOK\t" + folios);
                                logStreamWriter.Flush();

                                // Actualizar la barra de progreso
                                UpdateProgress((contador+1) - lineaActual);

                                WriteLogLine(" OK");
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLogLine("");
                            WriteLogLine("--------------------------------------------------");
                            WriteLogLine("--------------------------------------------------");
                            WriteLogLine("Error:");
                            WriteLogLine(ex.Message);
                            WriteLogLine("Trace:");
                            WriteLogLine(ex.StackTrace);

                            return;
                        }
                    }
                }

                WriteLogLine("--------------------------------------------------");
                WriteLogLine("--------------------------------------------------");
                WriteLogLine("El proceso finalizó exitosamente");
                WriteLogLine(DateTime.Now.ToString());
            }

            internal void Stop()
            {
                this.stop = true;
            }
        }

        private void domeNetbutton_Click(object sender, EventArgs e)
        {
            var onlyRename = RenombrarCheckbox.Checked;
            var inicio = DateTime.Now.ToString();
            var imagenes = 0;
            var fileName = entradaTextBox.Text;
            //var fileName = @"D:\Imagenes\FMMB\Prueba_FMMB_2.txt";
            try
            {
                using (var streamReader = new StreamReader(fileName))
                {
                    var line = streamReader.ReadLine();
                    line = streamReader.ReadLine();

                    while (line != null )
                    {
                        imagenes += 1;
                        var partes = line.Split('\t');

                        if (File.Exists(partes[0]))
                        {
                            if (onlyRename)
                            {
                                try
                                {
                                    var imageName = partes[1] + ".image";
                                    var thumbnailName = partes[1] + ".thumbnail";

                                    // Directorio
                                    var directoryName = Path.GetDirectoryName(imageName);
                                    if (!Directory.Exists(directoryName))
                                        Directory.CreateDirectory(directoryName);

                                    // Imagen
                                    if (!File.Exists(imageName))
                                        Directory.Move(partes[0], imageName);
                                    else
                                        File.Delete(imageName);

                                    // thumbnail
                                    if (!File.Exists(thumbnailName))
                                        Directory.Move(partes[0] + ".thumbnail", thumbnailName);
                                    else
                                        File.Delete(partes[0] + ".thumbnail");
                                }
                                catch (Exception ex)
                                {
                                    using (var Log = new StreamWriter(salidaTextBox.Text, true))
                                    {
                                        Log.WriteLine("Renombrar: " + partes[0] + " - " + partes[1] + " - ", ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                var imageName = partes[0];
                                var thumbnailName = partes[0] + ".thumbnail";

                                // thumbnail
                                if (!File.Exists(thumbnailName))
                                {
                                    try
                                    {


                                        var thumbnail = ImageManager.GetThumbnailData(imageName, 1, 1, 60, 80);
                                        using (var fileStream = new FileStream(thumbnailName, FileMode.Create, FileAccess.Write))
                                        {
                                            fileStream.Write(thumbnail[0], 0, thumbnail[0].Length);
                                            fileStream.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        using (var Log = new StreamWriter(salidaTextBox.Text, true))
                                        {
                                            Log.WriteLine("Crear Thumbnail: " + partes[0] + " - ", ex.Message);
                                        }
                                    }
                                }
                            }
                        }

                        line = streamReader.ReadLine();

                        if (imagenes % 100 == 0)
                        {
                            logRichTextBox.Text = "Inicio: " + inicio + "\nFin: ...\nImagenes: " + imagenes.ToString("#,##0");
                            Application.DoEvents();
                        }
                    }
                }

                MessageBox.Show("El proceso finalizó exitósamente", "Migrador", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Migrador", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var fin = DateTime.Now.ToString();

            logRichTextBox.Text = "Inicio: " + inicio + "\nFin: " + fin + "\nImagenes: " + imagenes.ToString("#,##0");
        }
    }
}