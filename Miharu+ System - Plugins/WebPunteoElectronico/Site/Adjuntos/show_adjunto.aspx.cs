using System;
using System.Web.UI.WebControls;
using System.IO;
using WebPunteoElectronico.Clases;
using Miharu.Security.Library.Session;
using System.Text;


namespace WebPunteoElectronico.Site.Adjuntos
{
    public partial class show_adjunto : FormBase
    {
        #region "Declaraciones"

        public Sesion _MySesion;
                   
        #endregion

        #region "Propiedades"

        public Sesion MySesion { get { return _MySesion; } }

        #endregion

        #region " Eventos "

        protected void Page_Load(object sender, EventArgs e)
        {

                _MySesion =  (Sesion)Session["Sesion"];

                Clear_Cache();

                string nid_Adjunto_Ajuste = (string)Request.Params["id_Adjunto_Ajuste"];
                string nNombreArchivo = (string)Request.Params["NombreArchivo"];
                string nContentType = (string)Request.Params["ContentType"];
                string nPDF = (string)Request.Params["pdf"];

                if (nid_Adjunto_Ajuste != null & nid_Adjunto_Ajuste != "")
                {
                    CrearFile(long.Parse(nid_Adjunto_Ajuste), nNombreArchivo, nContentType);
                }

                if (nPDF != null & nPDF != "")
                {
                    var gvFlujos = (GridView)this.MiharuSession.Pagina.Parameter["GrillaFlujos"];
                    CrearFilePDF(nPDF, gvFlujos);
                }
           
        }

        protected void lnkbDescarga_Click(object sender, EventArgs e)
        {
            if (this.MiharuSession.Pagina.Parameter["ContentType"].ToString() == "plain/text")
            {
                //Para Generacion de Otros tipos de Archivo
                DownloadFile(this.MiharuSession.Pagina.Parameter["ContentName"].ToString(), this.MiharuSession.Pagina.Parameter["Content"].ToString(), this.MiharuSession.Pagina.Parameter["ContentType"].ToString());
            }
            else
            {
                //Para Generacion de PDFs y/o ZIPs
                if (this.MiharuSession.Pagina.Parameter["ContentType"].ToString() == "application/vnd.ms-word")
                GenerarArchivo(Encoding.ASCII.GetBytes(this.MiharuSession.Pagina.Parameter["Content"].ToString()), this.MiharuSession.Pagina.Parameter["ContentName"].ToString(), this.MiharuSession.Pagina.Parameter["ContentType"].ToString());
                else
                GenerarArchivo((byte[])this.MiharuSession.Pagina.Parameter["Content"], this.MiharuSession.Pagina.Parameter["ContentName"].ToString(), this.MiharuSession.Pagina.Parameter["ContentType"].ToString());
            }
            
        }

        #endregion

        #region " Metodos "


        private void DownloadFile(string nContentName, string nContent, string nContentType)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + nContentName);
                Response.Charset = "";
                Response.ContentType = nContentType;

                Response.Output.Write(nContent);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                throw ex;
            }
        }

        private void CrearFile(long nid_Adjunto_Ajuste, string nNombre_Archivo, string nContentType)
        {
            var Contenido = (byte[])this.MiharuSession.Pagina.Parameter["Content"];
            //byte[] Contenido = System.Text.Encoding.UTF8.GetBytes(this.MiharuSession.Pagina.Parameter["Content"].ToString());
            GenerarArchivo(Contenido, nNombre_Archivo, nContentType);

        }


        private void GenerarArchivo(byte[] nContenido, string nNombreArchivo, string nContentType)
        {
            try
            {
                var a = System.Text.Encoding.UTF8.GetString(nContenido);

                var NewFilename = Server.MapPath("~/Temp").TrimEnd('\\') + '\\' + Guid.NewGuid().ToString() + ".tmp";
                using (var fileStream = new FileStream(NewFilename, FileMode.Create))
                {
                    fileStream.Write(nContenido, 0, nContenido.Length);
                    fileStream.Close();
                }

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();

                Response.AddHeader("content-disposition", "attachment; filename=" + nNombreArchivo);
                Response.ContentType = nContentType;
                Response.TransmitFile(NewFilename);

                Response.End();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        private void CrearFilePDF(string nFilename, GridView gvFlujos)
        {
            try
            {

                //Set the column widths 
                //iTextSharp.text.Table table = new iTextSharp.text.Table(gvFlujos.Columns.Count);
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvFlujos.Columns.Count);
                //table.Cellpadding = 5;


                int[] widths = new int[gvFlujos.Columns.Count];
                for (int x = 0; x <= gvFlujos.Columns.Count - 1; x++)
                {
                    widths[x] = Convert.ToInt32(gvFlujos.Columns[x].ItemStyle.Width.Value);
                    string cellText = Server.HtmlDecode(gvFlujos.HeaderRow.Cells[x].Text);
                    //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#008000"));

                    table.AddCell(cell);

                }
                table.SetWidths(widths);



                //Transfer rows from GridView to table
                for (int i = 0; i <= gvFlujos.Rows.Count - 1; i++)
                {
                    if (gvFlujos.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        for (int j = 0; j <= gvFlujos.Columns.Count - 1; j++)
                        {
                            string cellText = Server.HtmlDecode(gvFlujos.Rows[i].Cells[j].Text);
                            //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText));
                            //Set Color of Alternating row

                            if (i % 2 != 0)
                            {
                                cell.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#C2D69B"));
                            }
                            table.AddCell(cell);
                        }
                    }
                }


                //Create the PDF Document
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);

                iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.Add(table);
                pdfDoc.Close();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + nFilename);
                Response.Charset = "";
                Response.ContentType = "application/pdf";

                Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

    private void Clear_Cache()
       {
           try
           {
               var FileNames = System.IO.Directory.GetFiles(Server.MapPath("~/Temp"), "*");

               foreach (var FileName in FileNames)
               {
                   var FileProps = new FileInfo(FileName);
                   var Diferencia = DateTime.Now - FileProps.LastAccessTime;

                   if (Diferencia.Minutes > 30)
                       try { System.IO.File.Delete(FileName); }
                       catch { }
               }
           }
           catch { }
       }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.lnkbDescarga.Click += new EventHandler(lnkbDescarga_Click); 
        }
        protected override void Config_Page()
        {

            throw new NotImplementedException();
        }

        protected override void Load_Data()
        {
            throw new NotImplementedException();

        }
    }
}



