
// ReSharper disable CheckNamespace
namespace DBAgrario
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Tipos de campo que se pueden utilizar en el Core
    /// </summary>
    public enum Estado_AjusteEnum
    {
        Reg_Sin_Aprobacion = 1,
        Reapertura_Sin_Aprobacion = 2,
        Rechazado = 3,
        Aprobado = 4
    }

    public enum Tipo_Log_CruceEnum
    {
        Inicio_Cruce = 10,
        Inicio_Reporte_Cierre = 20,
        Fin_Proceso = 100,
        Error_Proceso = 999
    }

    public enum EmailListEnum : short
    {
        Notificacion_Publicacion = 1
    }
}