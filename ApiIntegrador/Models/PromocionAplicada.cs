namespace ApiIntegrador.Models
{
    public class PromocionAplicada
    {
        public int IdAplicacion { get; set; }

        public int IdSuscriptor { get; set; }
        public Suscriptor Suscriptor { get; set; }

        public int IdPromocion { get; set; }
        public Promocion Promocion { get; set; }

        public DateTime FechaAplicacion { get; set; }
    }
}
