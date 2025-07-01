namespace ApiIntegrador.Dto
{
    public class CrearPromocionDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal DescuentoResidencial { get; set; }
        public decimal DescuentoEmpresarial { get; set; }
        public bool AplicaNuevos { get; set; }
        public string TipoPromocion { get; set; } = string.Empty;
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
        public bool Activa { get; set; } = true;
        public int? IdCiudad { get; set; } 
    }
}
