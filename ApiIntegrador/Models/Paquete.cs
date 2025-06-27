namespace ApiIntegrador.Models
{
// Paquetes
public class Paquete
{
    public int IdPaquete { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string TipoPaquete { get; set; } = string.Empty; // "Residencial", "Empresarial"
    public string? Descripcion { get; set; }
    
    public ICollection<PaqueteServicio> PaqueteServicios { get; set; } = new List<PaqueteServicio>();
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    public ICollection<PromocionConfiguracion> PromocionConfiguraciones { get; set; } = new List<PromocionConfiguracion>();
}
}
