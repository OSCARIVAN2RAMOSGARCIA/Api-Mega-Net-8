namespace ApiIntegrador.Models
{
// Paquete.cs
public class Paquete
{
    public int IdPaquete { get; set; }
    public string Nombre { get; set; }
    public string TipoPaquete { get; set; } // "Residencial", "Empresarial"
    public string Descripcion { get; set; }
    public ICollection<PaqueteServicio> PaqueteServicios { get; set; }
    public ICollection<Contrato> Contratos { get; set; }
}
}
