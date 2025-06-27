namespace ApiIntegrador.Models
{
public class Servicio
{
    public int IdServicio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioResidencial { get; set; }
    public decimal PrecioEmpresarial { get; set; }
    public string TipoServicio { get; set; } = string.Empty; // "Residencial", "Empresarial", "Ambos"
    
    public ICollection<PaqueteServicio> PaqueteServicios { get; set; } = new List<PaqueteServicio>();
}

}
