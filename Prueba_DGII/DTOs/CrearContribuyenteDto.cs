using System.ComponentModel.DataAnnotations;

namespace Prueba_DGII.DTOs
{
    public class CrearContribuyenteDto
    {
        /// <summary>
        /// RNC o Cédula del contribuyente
        /// </summary>
        /// <example>123456789</example>
        [Required]
        [MaxLength(20)]
        public string RncCedula { get; set; }

        /// <summary>
        /// Nombre del contribuyente
        /// </summary>
        /// <example>SUPERMERCADO CENTRAL</example>
        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        /// <summary>
        /// Tipo de contribuyente
        /// </summary>
        /// <example>PERSONA JURIDICA</example>
        [Required]
        public string Tipo { get; set; }

        /// <summary>
        /// Estatus del contribuyente
        /// </summary>
        /// <example>activo</example>
        [Required]
        public string Estatus { get; set; }
    }
}
