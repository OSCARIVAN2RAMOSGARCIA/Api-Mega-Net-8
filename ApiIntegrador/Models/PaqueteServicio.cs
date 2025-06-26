namespace ApiIntegrador.Models
{
public class PaqueteServicio
{
    public int IdPaquete { get; set; }
    public int IdServicio { get; set; }
    public Paquete Paquete { get; set; }
    public Servicio Servicio { get; set; }
}
}
