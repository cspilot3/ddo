using System;
using WebPunteoElectronico.Clases;

namespace WebPunteoElectronico.Site
{
    public partial class About : FormBase
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                Config_Page();
            else
                Load_Data();
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.ShowTitle = false;
            Master.Title = "";

        }

        #endregion

        #region Metodos

        protected override void Config_Page()
        {
            this.ProductNameLabel.Text = Program.AssemblyProduct;
            this.VersionLabel.Text = String.Format("Versión {0}", Program.AssemblyVersion);
            this.CopyrightLabel.Text = Program.AssemblyCopyright;
            this.CompanyNameLabel.Text = Program.AssemblyCompany;
            this.DescriptionTextBox.Text = Program.AssemblyDescription;
        }

        protected override void Load_Data() { }

        #endregion
    }
}
