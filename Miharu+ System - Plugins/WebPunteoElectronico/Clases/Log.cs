using System;
using DBAgrarioLog;

namespace WebPunteoElectronico.Clases
{

    public class Log
    {
        public static void InsertLog(int nIdUsuario, string nIPAddress, Tipo_Accion_Log nIdAccion, string nModulo, string nQuery, string nVlrAntes, string nVlrDespues)
        {
            DBAgrarioLogDataBaseManager DBMPunteoLog = null;

            try
            {
                DBMPunteoLog = new DBAgrarioLogDataBaseManager(Program.ConnectionStringPunteoLog);

                DBMPunteoLog.Connection_Open();

                DBMPunteoLog.SchemaAudit.PA_Insert_Log.DBExecute(nIdUsuario, nIPAddress, (short)nIdAccion, nModulo, nQuery, nVlrAntes, nVlrDespues);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (DBMPunteoLog != null) DBMPunteoLog.Connection_Close();
            }
        }
    }
}