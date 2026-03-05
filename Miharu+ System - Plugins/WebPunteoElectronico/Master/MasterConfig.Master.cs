using System;
using System.Text;
using System.Web.UI.HtmlControls;
using Slyg.Web.Controls;
using WebPunteoElectronico.Clases;
using WebPunteoElectronico.Clases.Slyg;

namespace WebPunteoElectronico.Master
{
    public partial class MasterConfig : MasterBase
    {
        #region Declaraciones

        public event DaughterClose_Delegate DaughterClose;

        public delegate void SelectedGridChanged(string nSender, string nValue, CellData nCellData);

        public event SelectedGridChanged OnSelectedGridChanged;

        private const string IconWarning = "MB_warning";
        private const string IconError = "MB_error";
        private const string IconInformation = "MB_information";

        public enum MsgBoxIcon : byte
        {
            IconInformation = 1,
            IconWarning = 2,
            IconError = 3,
        }

        #endregion

        #region Propiedades

        protected override HtmlInputHidden EndRequestActionObject
        {
            get { return this.EndRequestAction; }
        }

        public bool ShowTitle
        {
            get { return this.TitleTR.Visible; }
            set { this.TitleTR.Visible = value; }
        }

        public string Title
        {
            get { return this.TitleLabel.Text; }
            set { this.TitleLabel.Text = value; }
        }

        #endregion

        #region Eventos

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.IsPostBack) return;

            // Scripts del encabezado
            var ScriptCreator = new StringBuilder("");

            // JQuery
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-1.8.0.min.js") + "' type='text/javascript'></script>\n");
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-ui-1.8.23.custom.min.js") + "' type='text/javascript'></script>\n");
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-es.js") + "' type='text/javascript'></script>\n");
            ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery-ui-1.8.23.custom.css") + "' rel='stylesheet' type='text/css' />\n");

            // GridView
            ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/gridview/GridviewStyles.css") + "' rel='stylesheet' type='text/css' />\n");
            //ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Styles/gridview/SlygGridView.js") + "' type='text/javascript'></script>\n");

            // Funciones generales [Debe ser la ultima en cargarse]
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Utils.js") + "' type='text/javascript'></script>\n");
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Master/MasterForm.js") + "' type='text/javascript'></script>\n");

            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/JQuery/flexigrid.custom.js") + "' type='text/javascript'></script>\n");
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/jquery/jquery.ui.autocomplete.custom.js") + "' type='text/javascript'></script>");
            ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/ui.dialogr.custom.js") + "' type='text/javascript'></script>\n");

            ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/flexigrid-1_1/flexigrid.css") + "' rel='stylesheet' type='text/css' />\n");
            ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery.dialogr.css") + "' rel='stylesheet' type='text/css' />\n");
            //ScriptCreator.Append("<link href='" + ResolveClientUrl("~/_styles/StyleSheet_DialogBox.css") + "' rel='stylesheet' type='text/css' />");
            HeadScriptsLiteral.Text = ScriptCreator.ToString();
        }

        protected void DaughterFormCloseLinkButton_Click(object sender, EventArgs e)
        {
            if (DaughterClose != null) DaughterClose();
        }

        protected void FlexiGridRowIndexChangedLinkButton_Click(object sender, EventArgs e)
        {
            if (OnSelectedGridChanged != null)
            {
                var sb = new ScriptBuilder();

                var args = FlexiGridRowIndexInfo.Value.Split(';');
                var gridId = args[0];
                var value = args[1];

                var cells = new CellData();
                var cellParts = args[2].Split(',');
                foreach (var cell in cellParts)
                {
                    var cellPart = cell.Split(':');
                    cells.Add(cellPart[0], sb.DecodeScript(cellPart[1]));
                }

                OnSelectedGridChanged(gridId, value, cells);
            }
        }

        #endregion

        #region Metodos

        public void ShowAlert(string nMensaje, MsgBoxIcon nIcon, short Ancho)
        {
            var Icono = "";

            switch (nIcon)
            {
                case MsgBoxIcon.IconError:
                    Icono = IconError;
                    break;
                case MsgBoxIcon.IconInformation:
                    Icono = IconInformation;
                    break;
                case MsgBoxIcon.IconWarning:
                    Icono = IconWarning;
                    break;
            }

            ScriptBag.AppendAndEncodeScript("parent.ShowAlert(\"" + nMensaje + "\",'" + Icono + "','" + Ancho + "');");
        }

        public void ShowAlert(string nMensaje, MsgBoxIcon nIcon)
        {
            string Icono = "";

            switch (nIcon)
            {
                case MsgBoxIcon.IconError:
                    Icono = IconError;
                    break;
                case MsgBoxIcon.IconInformation:
                    Icono = IconInformation;
                    break;
                case MsgBoxIcon.IconWarning:
                    Icono = IconWarning;
                    break;
            }

            ScriptBag.AppendAndEncodeScript("parent.ShowAlert(\"" + nMensaje + "\",'" + Icono + "','" + "420" + "');");
        }

        public void ShowDialog(string strUrl, string strNombre, string strTitle, string valWidth, string valHeight, string valLeft, string valTop, bool EventoCancel)
        {
            var Cancel = (EventoCancel ? "1" : "0");

            //ScriptBag.AppendAndEncodeScript("parent.ShowDialog('" + strUrl + "','" + strNombre + "','" + strTitle + "','" + valWidth + "','" + valHeight + "','" + valLeft + "','" + valTop + "','" + Cancel + "');");
            ScriptBag.AppendAndEncodeScript("parent.ShowDialog('" + strUrl + "','" + strNombre + "','" + strTitle + "','" + valWidth + "','" + valHeight + "', DaughterCloseEvent(true), null,'" + Cancel + "');");
        }

        public void ShowWindow(string URLPage, string Titulo, string PageWidth, string PageHeight)
        {
            var Status = SetYesNoValue(true);
            var Toolbar = SetYesNoValue(false);
            var Menubar = SetYesNoValue(false);
            var Location = SetYesNoValue(false);
            var Resizable = SetYesNoValue(true);
            var Scrollbars = SetYesNoValue(false);

            var Parametros = "height=" + PageHeight + "px, width=" + PageWidth + "px, top=" + "0" + "px, " +
                             "left=" + "0" + "px, status=" + Status + ", toolbar=" + Toolbar + ", menubar=" + Menubar +
                             ", location=" + Location + ", resizable=" + Resizable + ", scrollbars=" + Scrollbars;

            ScriptBag.AppendAndEncodeScript("parent.ShowWindow('" + URLPage + "','" + Titulo + "','" + Parametros + "');");
        }

        public void ShowWindow(string URLPage, string Titulo, string PageWidth, string PageHeight,
                               string PageTop, string PageLeft,
                               bool BarraEstado = true, bool BarraHerramientas = false,
                               bool BarraMenus = false, bool Bloquear = false,
                               bool CambiarTamaño = true, bool BarrasDesplazamiento = false)
        {
            var Status = SetYesNoValue(BarraEstado);
            var Toolbar = SetYesNoValue(BarraHerramientas);
            var Menubar = SetYesNoValue(BarraMenus);
            var Location = SetYesNoValue(Bloquear);
            var Resizable = SetYesNoValue(CambiarTamaño);
            var Scrollbars = SetYesNoValue(BarrasDesplazamiento);

            var Parametros = "height=" + PageHeight + "px, width=" + PageWidth + "px, top=" + PageTop + "px, " +
                             "left=" + PageLeft + "px, status=" + Status + ", toolbar=" + Toolbar + ", menubar=" + Menubar +
                             ", location=" + Location + ", resizable=" + Resizable + ", scrollbars=" + Scrollbars;

            ScriptBag.AppendAndEncodeScript("parent.ShowWindow('" + URLPage + "','" + Titulo + "','" + Parametros + "');");

        }

        public void ShowWindowNoBloqueo(string URLPage, string Titulo, string PageWidth, string PageHeight,
                                        string PageTop, string PageLeft,
                                        bool BarraEstado = true, bool BarraHerramientas = false,
                                        bool BarraMenus = false, bool Bloquear = false,
                                        bool CambiarTamaño = true, bool BarrasDesplazamiento = false)
        {
            var Status = SetYesNoValue(BarraEstado);
            var Toolbar = SetYesNoValue(BarraHerramientas);
            var Menubar = SetYesNoValue(BarraMenus);
            var Location = SetYesNoValue(Bloquear);
            var Resizable = SetYesNoValue(CambiarTamaño);
            var Scrollbars = SetYesNoValue(BarrasDesplazamiento);

            var Parametros = "height=" + PageHeight + "px, width=" + PageWidth + "px, top=" + PageTop + "px, " +
                             "left=" + PageLeft + "px, status=" + Status + ", toolbar=" + Toolbar + ", menubar=" + Menubar +
                             ", location=" + Location + ", resizable=" + Resizable + ", scrollbars=" + Scrollbars;

            ScriptBag.AppendAndEncodeScript("parent.ShowWindowNoBloqueo('" + URLPage + "','" + Titulo + "','" + Parametros + "');");
        }

        public void ShowWindowNoBloqueo(string URLPage, string Titulo, string PageWidth, string PageHeight)
        {
            var Status = SetYesNoValue(true);
            var Toolbar = SetYesNoValue(false);
            var Menubar = SetYesNoValue(false);
            var Location = SetYesNoValue(false);
            var Resizable = SetYesNoValue(true);
            var Scrollbars = SetYesNoValue(false);

            var Parametros = "height=" + PageHeight + "px, width=" + PageWidth + "px, top=" + "0" + "px, " +
                             "left=" + "0" + "px, status=" + Status + ", toolbar=" + Toolbar + ", menubar=" + Menubar +
                             ", location=" + Location + ", resizable=" + Resizable + ", scrollbars=" + Scrollbars;

            ScriptBag.AppendAndEncodeScript("parent.ShowWindowNoBloqueo('" + URLPage + "','" + Titulo + "','" + Parametros + "');");
        }

        public string SetYesNoValue(bool nValue)
        {
            return (nValue ? "yes" : "no");
        }

        public void SelectTab(Tabs nTab)
        {
            ScriptBag.AppendAndEncodeScript("SelectTab('" + nTab.ToString() + "');");
        }

        #endregion
    }

    public enum Tabs
    {
        Filtro = 1,
        Detalle = 2,
    }
}