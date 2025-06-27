namespace ApiIntegrador.Models
{
    public class PromocionConfiguracion
    {
        public int IdPromocionConfiguracion { get; set; }
        public int IdPromocion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdColonia { get; set; }
        public int? IdPaquete { get; set; }
        
        public Promocion Promocion { get; set; } = null!;
        public Ciudad? Ciudad { get; set; }
        public Colonia? Colonia { get; set; }
        public Paquete? Paquete { get; set; }
        public ICollection<PromocionAplicada> PromocionesAplicadas { get; set; } = new List<PromocionAplicada>();
    }
}