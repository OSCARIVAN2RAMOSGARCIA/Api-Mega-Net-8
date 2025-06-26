namespace ApiIntegrador.Models
{
public class Contrato
{
    public int IdContrato { get; set; }
    public int IdSuscriptor { get; set; }
    public int IdPaquete { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaTermino { get; set; }
    public bool Activo { get; set; } = true;
    public string TipoContrato { get; set; } // "Residencial", "Empresarial"
    public Suscriptor Suscriptor { get; set; }
    public Paquete Paquete { get; set; }
    public ICollection<PromocionAplicada> PromocionesAplicadas { get; set; }
}
}