using Slyg.Tools;
using Miharu.Security.Library.Session;

public class ParametrosReporteSMS
{
    #region Constructores

    public ParametrosReporteSMS()
    {
        this.FechaInicio = new SlygNullable<string>();
        this.FechaFin = new SlygNullable<string>();
        this.Oficina = new SlygNullable<string>();
        this.Regional = new SlygNullable<string>();
        this.COB = new SlygNullable<string>();
        this.Radicacion = new SlygNullable<string>();
        this.Usuario = new SlygNullable<string>();
    }

    #endregion

    #region Propiedades

    public SlygNullable<string> FechaInicio { get; set; }

    public SlygNullable<string> FechaFin { get; set; }

    public SlygNullable<string> Oficina { get; set; }

    public SlygNullable<string> Regional { get; set; }

    public SlygNullable<string> COB { get; set; }

    public SlygNullable<string> Radicacion { get; set; }

    public SlygNullable<string> Usuario { get; set; }

    public Sesion MiharuSession { get; set; }

    #endregion
}