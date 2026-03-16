using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Prueba_DGII.Controllers;
using Prueba_DGII.DTOs;
using Prueba_DGII.Models;


namespace Prueba_DGII_TESTS
{
    public class ContribuyentesControllerTests
    {

        [Fact]
        public async Task GetContribuyentes_ReturnsList()
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

            var controller = new ContribuyentesController(context);

            var result = await controller.GetContribuyentes();

            result.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async Task GetComprobantesPorContribuyente_ReturnsNotFound_WhenNotExists()
        {
            var context = TestDbContextFactory.Create();

            var controller = new ContribuyentesController(context);

            var result = await controller.GetComprobantesPorContribuyente("999");

            result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task CrearContribuyente_CreatesNewContribuyente()
        {
            var context = TestDbContextFactory.Create();

            var controller = new ContribuyentesController(context);

            var dto = new CrearContribuyenteDto
            {
                RncCedula = "987654321",
                Nombre = "Empresa Nueva",
                Tipo = "PERSONA JURIDICA",
                Estatus = "activo"
            };

            var result = await controller.CrearContribuyente(dto);

            result.Should().BeOfType<OkObjectResult>();

            context.Contribuyentes.Count().Should().Be(1);
        }


        [Fact]
        public async Task CrearContribuyente_ReturnsBadRequest_WhenRncDuplicado()
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

            var controller = new ContribuyentesController(context);

            var dto = new CrearContribuyenteDto
            {
                RncCedula = "123",
                Nombre = "Empresa Duplicada",
                Tipo = "PERSONA JURIDICA",
                Estatus = "activo"
            };

            var result = await controller.CrearContribuyente(dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}
