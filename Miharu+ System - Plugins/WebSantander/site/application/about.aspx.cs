using WebSantander.code;

namespace WebSantander.site.application
{
    public partial class about : page_form
    {
        public override bool IsSecurityPage()
        {
            return false;
        }

        public override void Config_Page()
        {
            this.Master.Title = "Acerca de MIHARU Client Browser";

            this.VersionLabel.Text = Program.AssemblyVersion;
        }
    }
}