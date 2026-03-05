using System;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico
{
    public partial class Wellcome : FormInitialBase
    {
        #region Eventos



        protected void Page_Load(object sender, EventArgs e)
        {
            Master.ShowTitle = false;
            Master.Title = "";
            
        }

        #endregion

        #region Metodos

        protected override void Config_Page() { }

        protected override void Load_Data() { }

        #endregion
    }
}