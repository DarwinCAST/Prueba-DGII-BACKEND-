using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_DGII.Data;
using Prueba_DGII.DTOs;
using Prueba_DGII.Models;

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
        /// <summary>
        /// Registra un nuevo comprobante fiscal
        /// </summary>
        /// <remarks>
        /// Permite crear un comprobante fiscal asociado a un contribuyente existente.
        /// El ITBIS se calcula automáticamente como el 18% del monto.
        ///
        /// Ejemplo:
        ///
        /// POST /api/comprobantes
        ///
        /// ```json
        /// {
        ///   "rncCedula": "98754321012",
        ///   "ncf": "E310000000003",
        ///   "monto": 500
        /// }
        /// ```
        ///
        /// </remarks>
        /// <param name="dto">Datos del comprobante</param>
        /// <returns>Comprobante creado</returns>
        /// <response code="200">Comprobante creado correctamente</response>
        /// <response code="400">NCF duplicado o contribuyente inexistente</response>
        [HttpPost]
        public async Task<IActionResult> CrearComprobante([FromBody] CrearComprobanteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var contribuyente = await _context.Contribuyentes
                .FirstOrDefaultAsync(c => c.RncCedula == dto.RncCedula);

            if (contribuyente == null)
                return BadRequest("El contribuyente no existe");


            var existeNCF = await _context.ComprobantesFiscales
                .AnyAsync(c => c.NCF == dto.NCF);

            if (existeNCF)
                return BadRequest("Ya existe un comprobante con ese NCF");

            var itbis = dto.Monto * 0.18;

            var comprobante = new ComprobanteFiscal
            {
                RncCedula = dto.RncCedula,
                NCF = dto.NCF,
                Monto = dto.Monto,
                Itbis18 = itbis
            };

            _context.ComprobantesFiscales.Add(comprobante);

            await _context.SaveChangesAsync();

            return Ok(comprobante);
        }
    }
}
