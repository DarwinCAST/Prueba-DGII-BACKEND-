using System.Text.Json.Serialization;

namespace Prueba_DGII.Models
{
    public class Contribuyente
    {
        public int Id { get; set; }

        public string? RncCedula { get; set; }

        public string? Nombre { get; set; }

        public string? Tipo { get; set; }

        public string? Estatus { get; set; }

        [JsonIgnore]
        public ICollection<ComprobanteFiscal>? Comprobantes { get; set; }
    }
}
