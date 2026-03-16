using Prueba_DGII.DTOs;

namespace Prueba_DGII.SwaggerExamples
{
    public class ContribuyentesExample
    {
        public static List<ContribuyenteDto> Get()
        {
            return new List<ContribuyenteDto>
            {
                new ContribuyenteDto
                {
                    RncCedula = "98754321012",
                    Nombre = "JUAN PEREZ",
                    Tipo = "PERSONA FISICA",
                    Estatus = "activo"
                },
                new ContribuyenteDto
                {
                    RncCedula = "123456789",
                    Nombre = "FARMACIA TU SALUD",
                    Tipo = "PERSONA JURIDICA",
                    Estatus = "inactivo"
                }
            };
        }
    }
}
