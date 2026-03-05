using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MigradorImagenes
{
    public partial class ManipulacionArchivos : Form
    {
        public ManipulacionArchivos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var LeerCarpeta = new FolderBrowserDialog();
            textBox1.Text = LeerCarpeta.ShowDialog() == DialogResult.OK ? LeerCarpeta.SelectedPath : "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var LeerArchivo = new OpenFileDialog();
            textBox2.Text = LeerArchivo.ShowDialog() == DialogResult.OK ? LeerArchivo.FileName : "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Files = Directory.EnumerateFiles(textBox1.Text, "*.*", SearchOption.AllDirectories).OrderBy(q=>q);

            var TotalFilas = "Origen\tDestino";

            foreach (var fileUnico in Files)
            {
                switch (Path.GetExtension(fileUnico).ToLower())
                {
                    case ".image":
                    {
                        TotalFilas += "\n" + fileUnico;
                        break;
                    }
                    case ".thumbnail" :
                    {
                        //TotalFilas += ("\t" + fileUnico); 
                        TotalFilas += "\n" + fileUnico;
                        break;
                    }
                    default:
                    {
                        TotalFilas += ("\n");
                        break;
                    }
                }
            }

            richTextBox1.Text = TotalFilas;
        }
    }
}
