namespace  Miharu.Client.Browser.code
{
    public static class ScriptHelper
    {
        #region Declaraciones

        private static int ScriptId;

        #endregion

        public static class Script
        {
            #region Metodos

            public static void Write(System.Web.UI.Page nPage, string nScript){
                nPage.ClientScript.RegisterStartupScript(nPage.GetType(), "ScriptId" + (ScriptId++).ToString(), nScript, true);
            }

            public static void Write(ScriptBuilder nHtml, string nScript)
            {
                nHtml.Append(nScript);
            }

            #endregion
        }

        public static class Site
        {
            #region Metodos

            public static void ShowAlert(System.Web.UI.Page nPage, string nMessage, MsgBoxIcon nIcon)
            {
                ShowAlert(nPage, nMessage, "", nIcon, 420);
            }

            public static void ShowAlert(System.Web.UI.Page nPage, string nMessage, string nTitle, MsgBoxIcon nIcon, short Ancho)
            {
                string Icono = "";

                switch (nIcon)
                {
                    case MsgBoxIcon.IconError: Icono = Program.IconError; break;
                    case MsgBoxIcon.IconInformation: Icono = Program.IconInformation; break;
                    case MsgBoxIcon.IconWarning: Icono = Program.IconWarning; break;
                }
                nPage.ClientScript.RegisterStartupScript(nPage.GetType(), "ShowAlert", "Site.ShowAlert('" + nMessage.Replace("'", "\"").Replace("\r", "").Replace("\n", "\t") + "','" + nTitle + "','" + Icono + "','" + Ancho + "');",true);
            }

            public static void ShowAlert(ScriptBuilder nHtml, string nMessage, MsgBoxIcon nIcon)
            {
                ShowAlert(nHtml, nMessage, "", nIcon, 420);
            }

            public static void ShowAlert(ScriptBuilder nHtml, string nMessage, string nTitle, MsgBoxIcon nIcon, short Ancho)
            {
                string Icono = "";

                switch (nIcon)
                {
                    case MsgBoxIcon.IconError: Icono = Program.IconError; break;
                    case MsgBoxIcon.IconInformation: Icono = Program.IconInformation; break;
                    case MsgBoxIcon.IconWarning: Icono = Program.IconWarning; break;
                }
                nHtml.Append("Site.ShowAlert('" + nMessage.Replace("'", "\"").Replace("\r", "").Replace("\n", "\t") + "','" + nTitle + "','" + Icono + "','" + Ancho + "');");
            }

            #endregion
        }

        public static class Global
        {
            #region Metodos

            public static void MostrarNotificacion(ScriptBuilder nHtml, string nTitulo, string nText)
            {
                nHtml.Append("Global.MostrarNotificacion('" + nHtml.CleanScript(nTitulo) + "','" + nHtml.CleanScript(nText) + "');");
            }

            #endregion
        }

        public static class Frm
        {
            #region Metodos

            public static void OcultarDetalle(ScriptBuilder nHtml)
            {
                nHtml.Append("Frm.OcultarDetalle();");
            }

            #endregion
        }        
    }
}