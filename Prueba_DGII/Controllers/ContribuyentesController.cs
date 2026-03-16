using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_DGII.Data;
using Prueba_DGII.DTOs;

namespace Prueba_DGII.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContribuyentesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContribuyentesController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obtiene todos los contribuyentes
        /// </summary>
        /// <remarks>
        /// Ejemplo de respuesta:
        ///
        ///     GET /api/contribuyentes
        ///
        /// ```json
        /// [
        ///   {
        ///     "rncCedula": "98754321012",
        ///     "nombre": "JUAN PEREZ",
        ///     "tipo": "PERSONA FISICA",
        ///     "estatus": "activo"
        ///   },
        ///   {
        ///     "rncCedula": "123456789",
        ///     "nombre": "FARMACIA TU SALUD",
        ///     "tipo": "PERSONA JURIDICA",
        ///     "estatus": "inactivo"
        ///   }
        /// ]
        /// ```
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetContribuyentes()
        {
            var contribuyentes = await _context.Contribuyentes
            .Select(c => new
            {
                c.Id,
                c.RncCedula,
                c.Nombre,
                c.Tipo,
                c.Estatus,
                CantidadComprobantes = c.Comprobantes != null
            ? c.Comprobantes.Count()
            : 0
            })
            .ToListAsync();

            return Ok(contribuyentes);
        }


        /// <summary>
        /// Obtiene los comprobantes de un contribuyente y la suma total del ITBIS
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        ///
        /// GET /api/contribuyentes/98754321012/comprobantes
        ///
        /// ```json
        /// {
        ///   "rncCedula": "98754321012",
        ///   "nombre": "JUAN PEREZ",
        ///   "totalITBIS": 216.00,
        ///   "comprobantes": [
        ///     {
        ///       "rncCedula": "98754321012",
        ///       "ncf": "E310000000001",
        ///       "monto": 200.00,
        ///       "itbis18": 36.00
        ///     },
        ///     {
        ///       "rncCedula": "98754321012",
        ///       "ncf": "E310000000002",
        ///       "monto": 1000.00,
        ///       "itbis18": 180.00
        ///     }
        ///   ]
        /// }
        /// ```
        ///
        /// </remarks>
        [HttpGet("{rnc}/comprobantes")]
        public async Task<IActionResult> GetComprobantesPorContribuyente(string rnc)
        {
            var contribuyente = await _context.Contribuyentes
                .FirstOrDefaultAsync(c => c.RncCedula == rnc);

            if (contribuyente == null)
                return NotFound("Contribuyente no encontrado");

            var comprobantes = await _context.ComprobantesFiscales
                .Where(c => c.RncCedula == rnc)
                .ToListAsync();

            var totalITBIS = comprobantes.Sum(c => c.Itbis18);

            var resultado = new ContribuyenteDetalleDto
            {
                RncCedula = contribuyente.RncCedula,
                Nombre = contribuyente.Nombre,
                TotalITBIS = totalITBIS,
                Comprobantes = comprobantes.Select(c => new ComprobanteFiscalDto
                {
                    RncCedula = c.RncCedula,
                    NCF = c.NCF,
                    Monto = c.Monto,
                    Itbis18 = c.Itbis18
                }).ToList()
            };

            return Ok(resultado);
        }
    }
}
