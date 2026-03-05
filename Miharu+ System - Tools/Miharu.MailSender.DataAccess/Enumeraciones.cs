namespace DBTools
{

    public enum EnumEstadosCorreos : byte
    {
        Programado = 1,
        Enviado = 2,
        Rechazado = 3,
        Recibido = 4,
        EntregadoTransportadora = 5,
        ProgramadoTransportadora = 6,
    }


    public class EstadosCorreos
    {
        public const string Programado = "Programado";
        public const string Enviado = "Enviado";
        public const string Rechazado = "Rechazado";
        public const string Recibido = "Recibido";
        public const string EntregadoTransportadora = "Entregado_Transportadora";
    }

}
