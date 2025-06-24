namespace ApiIntegrador.Models
{
    public class Paquete
    {
        public int IdPaquete { get; set; }
        public string NombrePaquete { get; set; }

        public ICollection<PaqueteServicio> PaqueteServicios { get; set; }
        public ICollection<Suscriptor> Suscriptores { get; set; }
    }
}
