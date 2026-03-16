using System.ComponentModel.DataAnnotations;

namespace Prueba_DGII.DTOs
{
    public class CrearComprobanteDto
    {
        /// <summary>
        /// RNC o Cédula del contribuyente
        /// </summary>
        /// <example>98754321012</example>
        [Required]
        public string RncCedula { get; set; }


        /// <summary>
        /// Número de comprobante fiscal
        /// </summary>
        /// <example>E310000000001</example>
        [Required]
        public string NCF { get; set; }

        /// <summary>
        /// Monto de la factura
        /// </summary>
        /// <example>1000</example>
        [Required]
        [Range(0.01, double.MaxValue)]
        public double Monto { get; set; }
    }
}
