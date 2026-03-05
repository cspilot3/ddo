using System;
using System.Web.UI.HtmlControls;
using Slyg.Web.Controls;

namespace WebPunteoElectronico.Clases
{
    public abstract class MasterBase : System.Web.UI.MasterPage
    {
        #region Propiedades

        private int NotificationTimeout = 0;

        protected abstract HtmlInputHidden EndRequestActionObject { get; }

        public ScriptBuilder ScriptBag = new ScriptBuilder();

        #endregion

        #region Metodos

        protected override void OnPreRender(EventArgs e)
        {
            EndRequestActionObject.Value = ScriptBag.ToString();

            base.OnPreRender(e);
        }

        public virtual void ShowAlert(string nMensaje, MsgBoxIcon nIcon)
        {
            string Titulo = "";

            switch (nIcon)
            {
                case MsgBoxIcon.IconError:
                    Titulo = "Error";
                    break;
                case MsgBoxIcon.IconInformation:
                    Titulo = "Información";
                    break;
                case MsgBoxIcon.IconWarning:
                    Titulo = "Advertencia";
                    break;
            }

            ShowAlert(nMensaje, Titulo, nIcon, 420);
        }

        public virtual void ShowAlert(string nMessage, string nTitle, MsgBoxIcon nIcon, short Ancho)
        {
            string Icono = "";

            switch (nIcon)
            {
                case MsgBoxIcon.IconError:
                    Icono = Program.IconError;
                    break;
                case MsgBoxIcon.IconInformation:
                    Icono = Program.IconInformation;
                    break;
                case MsgBoxIcon.IconWarning:
                    Icono = Program.IconWarning;
                    break;
            }

            ScriptBag.AppendAndEncodeScript("ShowAlert('" + nMessage.Replace("'", "\"").Replace("\r", "").Replace("\n", "\t") + "','" + nTitle + "','" + Icono + "','" + Ancho + "');");
        }

        public virtual void ShowWindow(string nURLPage, string Titulo, short PageWidth, short PageHeight)
        {
            ShowWindow(nURLPage, Titulo, PageWidth, PageHeight, 0, 0, true, false, false, false, true, false);
        }

        public virtual void ShowWindow(string URLPage, string Titulo, short PageWidth, short PageHeight,
                                       short PageTop, short PageLeft, bool BarraEstado, bool BarraHerramientas,
                                       bool BarraMenus, bool Bloquear, bool CambiarTamaño, bool BarrasDesplazamiento)
        {
            var Status = Program.SetYesNoValue(BarraEstado);
            var Toolbar = Program.SetYesNoValue(BarraHerramientas);
            var Menubar = Program.SetYesNoValue(BarraMenus);
            var Location = Program.SetYesNoValue(Bloquear);
            var Resizable = Program.SetYesNoValue(CambiarTamaño);
            var Scrollbars = Program.SetYesNoValue(BarrasDesplazamiento);

            var Parametros = "height=" + PageHeight + "px, width=" + PageWidth + "px, top=" + PageTop + "px, " +
                             "left=" + PageLeft + "px, status=" + Status + ", toolbar=" + Toolbar + ", menubar=" + Menubar +
                             ", location=" + Location + ", resizable=" + Resizable + ", scrollbars=" + Scrollbars;

            ScriptBag.AppendAndEncodeScript("ShowWindow('" + URLPage + "','" + Titulo + "','" + Parametros + "');");
        }

        public virtual void ShowDialog(string strUrl, string strTitle, short valWidth, short valHeight, bool EventoCancel)
        {
            string Cancel = EventoCancel ? "1" : "0";
            ScriptBag.AppendAndEncodeScript("ShowDialog('" + strUrl + "','" + strTitle + "','" + valWidth + "','" + valHeight + "'," + Cancel + ",null);");
        }

        public virtual void ShowNotification(string nTitile, string nMessage)
        {
            NotificationTimeout += 700;
            ScriptBag.AppendAndEncodeScript("setTimeout(function(){ MostrarNotificacion('" + nTitile + "','" + nMessage + "');}, " + NotificationTimeout + ");");
        }

        public virtual void ShowMessage(string nMessage)
        {
            ScriptBag.AppendAndEncodeScript("setTimeout(function(){Mensaje(\"" + nMessage.Replace("\"", "'").Replace("\r", "").Replace("\n", "\t") + "\");}, 100);");
        }

        public void SetFormTitle(string nFormTitle)
        {
            ScriptBag.AppendAndEncodeScript("parent.SetFormTitle('" + nFormTitle + "');");
        }

        public void FireParentPostback()
        {
            ScriptBag.AppendAndEncodeScript("parent.FireParentPostback();");
        }

        public void Download(GenericBase nPagina, string nFileName, byte[] nData, string nContentType)
        {
            nPagina.MiharuSession.Pagina.Parameter["_DownloadFileName"] = nFileName;
            nPagina.MiharuSession.Pagina.Parameter["_DownloadFilePath"] = "";
            nPagina.MiharuSession.Pagina.Parameter["_DownloadContentType"] = nContentType;
            nPagina.MiharuSession.Pagina.Parameter["_DownloadData"] = nData;

            ShowWindow(ResolveClientUrl("~/Site/Download.aspx"), "Descargar", 500, 100);
        }

        public void Download(GenericBase nPagina, string nFileName, string nFilePath, string nContentType)
        {
            nPagina.MiharuSession.Pagina.Parameter["_DownloadFileName"] = nFileName;
            nPagina.MiharuSession.Pagina.Parameter["_DownloadFilePath"] = nFilePath;
            nPagina.MiharuSession.Pagina.Parameter["_DownloadContentType"] = nContentType;
            nPagina.MiharuSession.Pagina.Parameter["_DownloadData"] = null;

            ShowWindow(ResolveClientUrl("~/Site/Download.aspx"), "Descargar", 500, 100);
        }

        #endregion
    }
}