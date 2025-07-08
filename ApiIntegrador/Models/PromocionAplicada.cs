using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegrador.Models
{
    public class PromocionAplicada
    {   [Key] 
        public int IdPromocionAplicada { get; set; }
        public int IdContrato { get; set; }
        public int IdPromocion { get; set; }
        public int? IdPromocionConfiguracion { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public DateTime? FechaTermino { get; set; }
        // public decimal DescuentoAplicado { get; set; }
        
        public Contrato Contrato { get; set; } = null!;
        public Promocion Promocion { get; set; } = null!;
        public PromocionConfiguracion? PromocionConfiguracion { get; set; }
    }

}
