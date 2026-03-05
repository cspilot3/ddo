using System;

namespace DBImaging
{
    public enum TipoExportacionEnum : byte
    {
        TEXTO_PLANO = 1,
        XML = 2,
        VISOR = 3,
        PLIGIN = 4
    }

    //public enum EnumReprocesoMotivo : short
    //{
    //    Ilegible = 1,
    //    Mal_Clasificado = 2,
    //    Anexos_no_Validos = 3
    //}

    public enum EnumEtapaCaptura : byte
    {
        Sin_captura = 0,
        Indexacion = 10,
        Precaptura = 20,
        Captura = 30,
        Postcaptura = 40,
        Opcional = 100
    }

    public enum EnumModoRespuestaAutomatica : byte
    {
        No_aplica = 0,
        Constante = 1,
        Comparacion_Campo = 2,
        Comparacion_Constante = 3,
        Comparacion_Parametro = 4,
    }

    public class ParametroSistemaEnum
    {
        public const string USA_DOMINIO = "@USA_DOMINIO";
    }

    public class EstadoProcesoRelevo
    {
        public const string PendienteRelevar = "Faltante";
        public const string RelevoCialdLocal = "Relevado";
        public const string RelevoCialdCentralizador = "Centralizado";
    }
}
