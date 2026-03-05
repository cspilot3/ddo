using Miharu;
using Slyg.Tools;
using Miharu.Security.Library.Session;

public class ParametrosConsulta
{
    #region Constructores

    public ParametrosConsulta()
    {
        this.FechaInicio = new SlygNullable<string>();
        this.FechaFin = new SlygNullable<string>();
        this.FechaInicioP = new SlygNullable<string>();
        this.FechaFinP = new SlygNullable<string>();

        this.CampoUno = new SlygNullable<string>();
        this.CampoDos = new SlygNullable<string>();
        this.CampoTres = new SlygNullable<string>();
        this.CampoCuatro = new SlygNullable<string>();
        this.CampoCinco = new SlygNullable<string>();
        this.CampoSeis = new SlygNullable<string>();
        this.CampoSiete = new SlygNullable<string>();
        this.CampoOcho = new SlygNullable<string>();
        this.CampoNueve = new SlygNullable<string>();
        this.CampoDiez = new SlygNullable<string>();

        //Medios de Pago
        this.Efectivo_Ini = new SlygNullable<decimal>();
        this.Efectivo_Fin = new SlygNullable<decimal>();
        this.Chq_gerencia_Ini = new SlygNullable<decimal>();
        this.Chq_gerencia_Fin = new SlygNullable<decimal>();
        this.Chq_Local_Ini = new SlygNullable<decimal>();
        this.Chq_Local_Fin = new SlygNullable<decimal>();
        this.Chq_Propio_Ini = new SlygNullable<decimal>();
        this.Chq_Propio_Fin = new SlygNullable<decimal>();
        this.Nota_Debito_Ini = new SlygNullable<decimal>();
        this.Nota_Debito_Fin = new SlygNullable<decimal>();
        this.Nota_Credito_Ini = new SlygNullable<decimal>();
        this.Nota_Credito_Fin = new SlygNullable<decimal>();
        this.Remesa_Negociada_Ini = new SlygNullable<decimal>();
        this.Remesa_Negociada_Fin = new SlygNullable<decimal>();
        this.Remesa_al_Cobro_Ini = new SlygNullable<decimal>();
        this.Remesa_al_Cobro_Fin = new SlygNullable<decimal>();

        this.Codigo_Causal = new SlygNullable<string>();
        this.Comision = new SlygNullable<string>();

        this.Key_04 = new SlygNullable<string>();
        this.Key_05 = new SlygNullable<string>();
        this.No_Chq_Gerencia = new SlygNullable<string>();
        this.No_Cta_Afectada = new SlygNullable<string>();
        //this.Nota_Debito = new SlygNullable<string>();
        this.Producto = new SlygNullable<string>();
        this.ValorIni = new SlygNullable<decimal>();
        this.ValorFin = new SlygNullable<decimal>();
        this.CodigoCOB = new SlygNullable<short>();
        this.CodigoOficina = new SlygNullable<int>();
        this.Usuario = new SlygNullable<string>();
        this.cnx = new SlygNullable<string>();
        this.usrcnx = new SlygNullable<int>();
        this.usrlgn = new SlygNullable<string>();
        this.TipoTransaccionNombre = new SlygNullable<string>();
        this.Nro_Producto = new SlygNullable<string>();
        this.Nro_Ente = new SlygNullable<string>();
        this.PageSize = new SlygNullable<int>();
        this.PageNumber = new SlygNullable<int>();
    }

    #endregion

    #region Propiedades

    public SlygNullable<string> FechaInicio { get; set; }

    public SlygNullable<string> FechaFin { get; set; }

    public SlygNullable<string> FechaInicioP { get; set; }

    public SlygNullable<string> FechaFinP { get; set; }

    public int Oficina { get; set; }

    public int Documento { get; set; }

    public SlygNullable<string> CampoUno { get; set; }

    public SlygNullable<string> CampoDos { get; set; }

    public SlygNullable<string> CampoTres { get; set; }

    public SlygNullable<string> CampoCuatro { get; set; }

    public SlygNullable<string> CampoCinco { get; set; }

    public SlygNullable<string> CampoSeis { get; set; }

    public SlygNullable<string> CampoSiete { get; set; }

    public SlygNullable<string> CampoOcho { get; set; }

    public SlygNullable<string> CampoNueve { get; set; }

    public SlygNullable<string> CampoDiez { get; set; }

    public SlygNullable<string> Codigo_Causal { get; set; }

    public SlygNullable<string> Comision { get; set; }

    //Medios de Pago
    public SlygNullable<decimal> Efectivo_Ini { get; set; }

    public SlygNullable<decimal> Efectivo_Fin { get; set; }

    public SlygNullable<decimal> Chq_gerencia_Ini { get; set; }

    public SlygNullable<decimal> Chq_gerencia_Fin { get; set; }

    public SlygNullable<decimal> Chq_Local_Ini { get; set; }

    public SlygNullable<decimal> Chq_Local_Fin { get; set; }

    public SlygNullable<decimal> Chq_Propio_Fin { get; set; }

    public SlygNullable<decimal> Chq_Propio_Ini { get; set; }

    public SlygNullable<decimal> Nota_Debito_Ini { get; set; }

    public SlygNullable<decimal> Nota_Debito_Fin { get; set; }

    public SlygNullable<decimal> Nota_Credito_Ini { get; set; }

    public SlygNullable<decimal> Nota_Credito_Fin { get; set; }

    public SlygNullable<decimal> Remesa_Negociada_Ini { get; set; }

    public SlygNullable<decimal> Remesa_Negociada_Fin { get; set; }

    public SlygNullable<decimal> Remesa_al_Cobro_Ini { get; set; }

    public SlygNullable<decimal> Remesa_al_Cobro_Fin { get; set; }


    public SlygNullable<string> Key_04 { get; set; }

    public SlygNullable<string> Key_05 { get; set; }

    public SlygNullable<string> No_Chq_Gerencia { get; set; }

    public SlygNullable<string> No_Cta_Afectada { get; set; }

    public SlygNullable<string> Producto { get; set; }

    public SlygNullable<decimal> ValorIni { get; set; }

    public SlygNullable<decimal> ValorFin { get; set; }

    public SlygNullable<short> CodigoCOB { get; set; }

    public SlygNullable<int> CodigoOficina { get; set; }

    public SlygNullable<string> Usuario { get; set; }

    public SlygNullable<string> cnx { get; set; }

    public SlygNullable<int> usrcnx { get; set; }

    public SlygNullable<string> usrlgn { get; set; }

    public SlygNullable<string> TipoTransaccionNombre { get; set; }

    public SlygNullable<string> Nro_Producto { get; set; }

    public SlygNullable<string> Nro_Ente { get; set; }

    public SlygNullable<int> PageSize { get; set; }

    public SlygNullable<int> PageNumber { get; set; }
    
    public Sesion MiharuSession { get; set; }

    #endregion
}