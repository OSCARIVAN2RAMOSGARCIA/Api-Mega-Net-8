namespace ApiIntegrador.Dto
{
 public class SuscriptorDeudaDTO
    {
        public int IdSuscriptor { get; set; }
        public string Nombre { get; set; } = string.Empty;
         public string Colonia { get; set; }      
        public string Ciudad { get; set; } 
        public decimal DeudaTotal { get; set; }
        public List<ContratoDeudaDetalleDTO> Contratos { get; set; } = new();
    }
}
