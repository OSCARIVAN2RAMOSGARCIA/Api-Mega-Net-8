namespace ApiIntegrador.Dtos
{
    public class SuscriptorDto
    {
        public int IdSuscriptor { get; set; }
        public string Nombre { get; set; }
        public int IdPaquete { get; set; }
        public bool EsNuevo { get; set; }
        public int IdColonia { get; set; }
    }
}
