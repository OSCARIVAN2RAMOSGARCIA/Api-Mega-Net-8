namespace ApiIntegrador.Models
{
// Ubicación
public class Ciudad
{
    public int IdCiudad { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Colonia> Colonias { get; set; } = new List<Colonia>();
}
}


