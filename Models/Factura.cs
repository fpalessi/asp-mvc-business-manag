namespace SandraConfecciones.Models;

public partial class Factura
{
    public int FacturaId { get; set; }

    public int? ClienteId { get; set; }

    public DateOnly? Fecha { get; set; }

    public decimal? Total { get; set; }

    public string? Descripcion { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
