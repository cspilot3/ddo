using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace Miharu.Explorer.Imaging._Clases
{
    public class Menu
    {
        public static string getMenu(List<MenuStruct> Estructura, int Width, int Height, string Color, Page page)
        {
            var Menus = new Dictionary<string, string>();
            var MenuFinal = "";

            //Crea los menus
            foreach (var MenuUnico in Estructura)
            {
                var Padre = "";
                if (MenuUnico.Parent != null)
                {
                    Padre = MenuUnico.Parent;
                }

                if (Menus.ContainsKey(Padre))
                    Menus[Padre] = Menus[Padre] + getItem(MenuUnico, false, Width, Height, Color, page);
                else
                    Menus.Add(Padre, Padre == "" ? getItem(MenuUnico, false, Width, Height, Color, page) : getItem(MenuUnico, true, Width, Height, Color, page));
            }


            //Agrega las propiedades para los hijos
            var Parent = true;
            foreach (var pair in Menus)
            {
                if (Parent)
                {
                    MenuFinal += "\n<div id=\"div_" + pair.Key + "\">\n" + pair.Value + "\n</div>\n";
                    Parent = false;
                }
                else
                {
                    MenuFinal += "\n<div id=\"div_" + pair.Key + "\" style=\"display:none;\">\n";

                    MenuFinal += pair.Value;
                    MenuFinal += "\n</div>\n";
                }
            }

            return MenuFinal;
        }

        public static string getMenu(DataTable Data, Page page)
        {
            var Menu = new List<MenuStruct>();
            foreach (DataRow Row in Data.Rows)
            {
                Menu.Add(new MenuStruct() {Text = Row["Nombre_Menu"].val(), URL = page.ResolveClientUrl(Row["URL"].val()), Image = page.ResolveClientUrl(Row["Image"].val()), Value = Row["id_Menu"].val(), Parent = Row["fk_Menu_Padre"].val()});
            }
            return getMenu(Menu, page);
        }

        public static string getMenu(List<MenuStruct> Estructura, Page page)
        {
            return getMenu(Estructura, 0, 0, "", page);
        }

        private static string getItem(MenuStruct MenuUnico, bool btnReturn, Page page)
        {
            return getItem(MenuUnico, btnReturn, 0, 0, "", page);
        }

        private static string getItem(MenuStruct MenuUnico, bool btnReturn, int Width, int Height, string Color, Page page)
        {


            if (Height == 0)
            {
                Height = 80;
            }
            if (Width == 0)
            {
                Width = 80;
            }
            if (Color == "")
            {
                Color = "FFFFFF";
            }

            string MenuString = "";

            if (btnReturn)
            {
                var Back = new MenuStruct() {Parent = MenuUnico.Parent, URL = "", Text = "Regresar", Image = page.ResolveClientUrl("~/_Images/Menu/Back.png")};
                MenuString += getItem(Back, false, page);
            }

            MenuString += "\n<a id=\"a_" + MenuUnico.Value + "\" class=\"menu\" style=\"width: " + (Width + 16).ToString() + "px;height: " + (Height + 16).ToString() + "px; border: thin solid #" + Color + "; Color: #" + Color + ";\"";

            if (MenuUnico.URL == "")
                MenuString += " href=\"#\" onclick=\"$('#div_" + MenuUnico.Parent + "').hide();$('#div_" + MenuUnico.Value + "').show();\">";
            else
                MenuString += " href=\"" + MenuUnico.URL + "\" target=\"content\" >";

            if (MenuUnico.Text.Length > 15)
            {
                double len= MenuUnico.Text.Length / 15.0;

                Height = Height - ((int)(System.Math.Round(len, 0, System.MidpointRounding.AwayFromZero)) + 10);
            }

            MenuString += "\n\t<img src=\"" + page.ResolveClientUrl(MenuUnico.Image) + "\" style=\"width: " + Width.ToString() + "px;height: " + Height.ToString() + "px;\" />";
            MenuString += "\n\t" + MenuUnico.Text;
            MenuString += "\n</a>";


            return MenuString;
        }

    }

    public struct MenuStruct
    {
        public string URL;
        public string Image;
        //public int Height;
        //public int Width;
        public string Text;
        public string Value;
        public string Parent;
    }
}