using System;
using Miharu.Client.Browser.code;

namespace  Miharu.Client.Browser.site.administracion.seguridad
{
    public partial class desbloqueo_ip : page_form
    {
        #region Eventos

        public override void Config_Page()
        {
            this.Master.Title = "Desbloqueos IP";

            EditOptions.Add(Option.find, Option.unlock);
        }

        public void DesbloquearIPs(ScriptBuilder nHtml)
        {
            //DBSecurityDataBaseManager dbmSecurity = null;
            //try
            //{
            //    dbmSecurity = new DBSecurityDataBaseManager(Program.ConnectionString.Security);
            //    dbmSecurity.Connection_Open();
            //    var ips = GetValue<string>("cadena");
            //    string[] IPlist = ips.ToString().Split('|');

            //    foreach (var id_IP in IPlist)
            //    {
            //        if (id_IP != "")
            //            dbmSecurity.Schemasecurity.ps_desbloquear_ip.DBExecute(id_IP);
            //    }
            //    ScriptHelper.Global.MostrarNotificacion(nHtml, "Mensaje", "Se han desbloqueado las IPs con éxito.");
            //}
            //catch (Exception e)
            //{
            //    TraceError(nHtml, e);
            //}
            //finally
            //{
            //    if (dbmSecurity != null) { dbmSecurity.Connection_Close(); }
            //}
        }

        public void CargarIPBloqueada(ScriptBuilder nHtml)
        {
            //DBSecurityDataBaseManager dbmSecurity = null;
            //try
            //{
            //    dbmSecurity = new DBSecurityDataBaseManager(Program.ConnectionString.Security);
            //    dbmSecurity.Connection_Open();

            //    var ip_address = GetValue<string>("ip_address");
            //    var IPsBloqueadasData = dbmSecurity.Schemasecurity.ps_conexiones_ip.DBExecute(ip_address);
            //    var StrIps = "";

            //    foreach (var i in IPsBloqueadasData)
            //    {
            //        if (StrIps != "") StrIps += ",";
            //        StrIps += "{ ip_address: '" + nHtml.CleanScript(i.ip_address) + "'" +
            //            ", fecha_log: '" + nHtml.CleanScript(i.fecha_log) + "'" +
            //            ", fecha_conexion: '" + nHtml.CleanScript(i.fecha_conexion) + "'}";
            //    }
            //    nHtml.Append("Frm.IPBloqueadas" + " = [" + StrIps + "];");
            //}
            //catch (Exception ex)
            //{
            //    TraceError(nHtml, ex);
            //}
            //finally
            //{
            //    if (dbmSecurity != null) dbmSecurity.Connection_Close();
            //}
        }

        #endregion
    }
}
