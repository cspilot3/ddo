using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Miharu.Security.Library.Session;

namespace WebPunteoElectronico.Site
{
    public partial class Download : System.Web.UI.Page
    {
        #region Propiedades

        public Sesion MiharuSession
        {
            get { return (Sesion)Session["Session"]; }
        }

        public string DownloadFileName
        {
            get { return (string)this.MiharuSession.Pagina.Parameter["_DownloadFileName"]; }
        }

        public string DownloadFilePath
        {
            get { return (string)this.MiharuSession.Pagina.Parameter["_DownloadFilePath"]; }
        }

        public string DownloadContentType
        {
            get { return (string)this.MiharuSession.Pagina.Parameter["_DownloadContentType"]; }
        }

        public byte[] DownloadData
        {
            get { return (byte[])this.MiharuSession.Pagina.Parameter["_DownloadData"]; }
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

            this.DownloadLinkButton.Click += new EventHandler(DownloadLinkButton_Click);
        }

        void DownloadLinkButton_Click(object sender, EventArgs e)
        {
            InitDownload();   
        }        

        #endregion

        #region Metodos

        private void Config_Page()
        {
        }

        private void InitDownload()
        {
            if (this.DownloadFilePath == "")
                InitDownloadData(this.DownloadFileName, this.DownloadData, this.DownloadContentType);
            else
                InitDownloadFile(this.DownloadFileName, this.DownloadFilePath, this.DownloadContentType);
        }

        private void InitDownloadData(string nFileName, byte[] nData, string nContentType)
        {
            try
            {
                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + nFileName);
                Response.ContentType = nContentType;
                Response.BinaryWrite(nData);
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void InitDownloadFile(string nFileName, string nFilePath, string nContentType)
        {
            try
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();

                Response.AddHeader("content-disposition", "attachment; filename=" + nFileName);
                Response.ContentType = nContentType;
                Response.TransmitFile(nFilePath);

                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #endregion

    }
}