using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_DGII.Data;

namespace Prueba_DGII.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprobantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComprobantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de comprobantes fiscales registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve todos los comprobantes fiscales junto con la información
        /// básica del contribuyente asociado.
        ///
        /// Ejemplo de respuesta:
        ///
        ///     GET /api/comprobantes
        ///
        ///     [
        ///        {
        ///            "id": 1,
        ///            "rncCedula": "98754321012",
        ///            "ncf": "E310000000001",
        ///            "monto": 200,
        ///            "itbis18": 36,
        ///            "contribuyenteId": 1
        ///        }
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de comprobantes fiscales.</returns>
        /// <response code="200">Retorna la lista de comprobantes</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<IActionResult> GetComprobantes()
        {
            var comprobantes = await _context.ComprobantesFiscales
            .Include(c => c.Contribuyente)
            .Select(c => new
            {
                c.Id,
                c.NCF,
                c.Monto,
                c.Itbis18,
                Contribuyente = c.Contribuyente == null ? null : new
                {
                    c.Contribuyente.RncCedula,
                    c.Contribuyente.Nombre
                }
            })
              .ToListAsync();
            return Ok(comprobantes);
        }
    }
}
