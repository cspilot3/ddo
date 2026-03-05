using System;
using System.IO;
using System.Web;
using DBAgrario.SchemaProcess;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site.Ajustes
{
    public partial class p_adjuntar : FormBase
    {
        #region Declaraciones

        private const string Path_Nodo = "4.6";

        #endregion

        #region Propiedades

        public new Master.MasterPopUp Master
        {
            get { return (Master.MasterPopUp)base.OriginalMaster; }
        }

        protected CTA_Ajuste_Adjunto_AjusteDataTable pTablaAdjuntos
        {
            get
            {
                if (this.MiharuSession.Parameter["pTablaAdjuntos"] == null)
                    this.MiharuSession.Parameter["pTablaAdjuntos"] = new CTA_Ajuste_Adjunto_AjusteDataTable();

                return (CTA_Ajuste_Adjunto_AjusteDataTable)this.MiharuSession.Parameter["pTablaAdjuntos"];
            }
            set
            {
                this.MiharuSession.Parameter["pTablaAdjuntos"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            btnAceptar.Click += new EventHandler(btnAceptar_Click);
            btnCancelar.Click += new EventHandler(btnCancelar_Click);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int intDocFileLength = this.ifCargar.PostedFile.ContentLength;
            AddFile();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Master.Cerrar(false);
        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            if (this.MiharuSession.Parameter["NombreArchivo"] != null)
                lblItems.Text = (this.MiharuSession.Parameter["NombreArchivo"]).ToString().Replace("|", ", ");
        }

        protected override void Load_Data() { }

        private void AddFile()
        {
           
            HttpPostedFile miFichero = ifCargar.PostedFile;
            var kb = miFichero.ContentLength / 1024;
            var mime = miFichero.ContentType;
                        
            if (kb < 2049 )
            {
                if (mime != "application/octet-stream")
                {
                    try
                    {
                        string RutaArchivoServidor = MapPath("~/Temp/" + ifCargar.FileName);
                        string NombreArchivo = ifCargar.FileName;
                        byte[] buffer = new byte[miFichero.ContentLength];
                        
                        ifCargar.PostedFile.SaveAs(RutaArchivoServidor);
                        using (var sr = new StreamReader(RutaArchivoServidor))
                        {
                            sr.BaseStream.Read(buffer, 0, buffer.Length);
                            sr.Close();
                        }
                        
                        File.Delete(RutaArchivoServidor);

                        var idAjuste = 1;
                        foreach (var adj in pTablaAdjuntos)
                        {
                            if (adj.fk_Ajuste_Adjunto >= idAjuste)
                                idAjuste = (int)(adj.fk_Ajuste_Adjunto + 1);
                        }
                        
                        pTablaAdjuntos.AddCTA_Ajuste_Adjunto_AjusteRow(0, 0, idAjuste, buffer, "new", NombreArchivo);                        
                        Master.Cerrar(true);
                        lblItems.Text = lblItems.Text + NombreArchivo;
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Ha ocurrido un error al cargar el archivo -> " + ex.Message.ToString().Replace("'", "-") + "');</script>");
                    }
                }
                else
                    Response.Write("<script>alert('No es permitido cargar archivos ejecutables');</script>");
            }
            else
                Response.Write("<script>alert('El archivo supera el límite de 2 MB');</script>");
        }

        #endregion
    }
}
