
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Prueba_DGII.Controllers;
using Prueba_DGII.DTOs;
using Prueba_DGII.Models;

namespace Prueba_DGII_TESTS
{
    public class ComprobantesControllerTests
    {

        [Fact]
        public async Task GetComprobantes_ReturnsList()
        {
            var context = TestDbContextFactory.Create();

            var contribuyente = new Contribuyente
            {
                RncCedula = "123",
                Nombre = "Empresa Test",
                Tipo = "PERSONA JURIDICA",
                Estatus = "activo"
            };

            context.Contribuyentes.Add(contribuyente);

            context.ComprobantesFiscales.Add(new ComprobanteFiscal
            {
                RncCedula = "123",
                NCF = "E310000000001",
                Monto = 200,
                Itbis18 = 36
            });

            await context.SaveChangesAsync();

            var controller = new ComprobantesController(context);

            var result = await controller.GetComprobantes();

            result.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async Task CrearComprobante_ReturnsBadRequest_WhenContribuyenteNoExiste()
        {
            var context = TestDbContextFactory.Create();

            var controller = new ComprobantesController(context);

            var dto = new CrearComprobanteDto
            {
                RncCedula = "999",
                NCF = "E310000000002",
                Monto = 100
            };

            var result = await controller.CrearComprobante(dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public async Task CrearComprobante_CreatesComprobante()
        {
            var context = TestDbContextFactory.Create();

            context.Contribuyentes.Add(new Contribuyente
            {
                RncCedula = "123",
                Nombre = "Empresa Test",
                Tipo = "PERSONA JURIDICA",
                Estatus = "activo"
            });

            await context.SaveChangesAsync();

            var controller = new ComprobantesController(context);

            var dto = new CrearComprobanteDto
            {
                RncCedula = "123",
                NCF = "E310000000010",
                Monto = 100
            };

            var result = await controller.CrearComprobante(dto);

            result.Should().BeOfType<OkObjectResult>();

            context.ComprobantesFiscales.Count().Should().Be(1);
        }


        [Fact]
        public async Task CrearComprobante_ReturnsBadRequest_WhenNCFDuplicado()
        {
            var context = TestDbContextFactory.Create();

            context.Contribuyentes.Add(new Contribuyente
            {
                RncCedula = "123",
                Nombre = "Empresa Test",
                Tipo = "PERSONA JURIDICA",
                Estatus = "activo"
            });

            context.ComprobantesFiscales.Add(new ComprobanteFiscal
            {
                RncCedula = "123",
                NCF = "E310000000001",
                Monto = 200,
                Itbis18 = 36
            });

            await context.SaveChangesAsync();

            var controller = new ComprobantesController(context);

            var dto = new CrearComprobanteDto
            {
                RncCedula = "123",
                NCF = "E310000000001",
                Monto = 500
            };

            var result = await controller.CrearComprobante(dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}
