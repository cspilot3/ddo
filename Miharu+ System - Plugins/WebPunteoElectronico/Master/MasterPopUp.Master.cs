using System;
using System.Text;
using System.Web.UI.HtmlControls;
using Slyg.Web.Controls;
using WebPunteoElectronico.Clases;
using WebPunteoElectronico.Clases.Slyg;

namespace WebPunteoElectronico.Master
{
    public partial class MasterPopUp : MasterBase
    {
        #region Declaraciones

        public delegate void SelectedGridChanged(string nSender, string nValue, CellData nCellData);

        public event SelectedGridChanged OnSelectedGridChanged;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Scripts del encabezado
                var ScriptCreator = new StringBuilder("");

                // JQuery
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-1.8.0.min.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-ui-1.8.23.custom.min.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/jquery-es.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery-ui-1.8.23.custom.css") + "' rel='stylesheet' type='text/css' />\n");

                // GridView
                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/gridview/GridviewStyles.css") + "' rel='stylesheet' type='text/css' />\n");

                // Funciones generales [Debe ser la ultima en cargarse]
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Utils.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Master/MasterPopUp.js") + "' type='text/javascript'></script>\n");

                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/JQuery/flexigrid.custom.js") + "' type='text/javascript'></script>\n");
                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/flexigrid-1_1/flexigrid.css") + "' rel='stylesheet' type='text/css' />\n");

                ScriptCreator.Append("<link href='" + ResolveClientUrl("~/Styles/green-theme/jquery.dialogr.css") + "' rel='stylesheet' type='text/css' />\n");
                ScriptCreator.Append("<script src='" + ResolveClientUrl("~/Scripts/Jquery/ui.dialogr.custom.js") + "' type='text/javascript'></script>\n");

                HeadScriptsLiteral.Text = ScriptCreator.ToString();
            }
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

        #region Propiedades

        protected override HtmlInputHidden EndRequestActionObject
        {
            get { return EndRequestAction; }
        }

        #endregion

        #region Metodos

        public void Cerrar(bool result)
        {
            ScriptBag.AppendAndEncodeScript("DlgClose(" + result.ToString().ToLower() + ");");
        }

        public override void ShowWindow(string nURLPage, string Titulo, short PageWidth, short PageHeight)
        {
            throw new NotImplementedException();
        }

        public override void ShowWindow(string URLPage, string Titulo, short PageWidth, short PageHeight,
                                        short PageTop, short PageLeft, bool BarraEstado, bool BarraHerramientas,
                                        bool BarraMenus, bool Bloquear, bool CambiarTamaño, bool BarrasDesplazamiento)
        {
            throw new NotImplementedException();
        }

        public override void ShowDialog(string strUrl, string strTitle, short valWidth, short valHeight, bool EventoCancel)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}