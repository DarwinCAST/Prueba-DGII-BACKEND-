namespace Prueba_DGII.Models
{
    public class ComprobanteFiscal
    {
        public int? Id { get; set; }

        public string? RncCedula { get; set; }

        public string? NCF { get; set; }

        public double? Monto { get; set; }

        public double? Itbis18 { get; set; }

        public int? ContribuyenteId { get; set; }

        public Contribuyente? Contribuyente { get; set; }
    }
}
