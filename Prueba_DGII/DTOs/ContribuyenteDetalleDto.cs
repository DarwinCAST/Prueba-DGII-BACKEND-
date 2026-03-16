namespace Prueba_DGII.DTOs
{
    public class ContribuyenteDetalleDto
    {
        public string? RncCedula { get; set; }

        public string? Nombre { get; set; }

        public double? TotalITBIS { get; set; }

        public List<ComprobanteFiscalDto>? Comprobantes { get; set; }
    }
}
