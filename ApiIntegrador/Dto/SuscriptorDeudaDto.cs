namespace ApiIntegrador.Dto
{
    public class SuscriptorDeudaDTO
    {
        public int IdSuscriptor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal DeudaTotal { get; set; }
        public List<ContratoDeudaDetalleDTO> Contratos { get; set; } = new();
    }
}
