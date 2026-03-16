using Microsoft.EntityFrameworkCore;
using Prueba_DGII.Models;

namespace Prueba_DGII.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Contribuyente> Contribuyentes { get; set; }

        public DbSet<ComprobanteFiscal> ComprobantesFiscales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contribuyente>().HasData(
                new Contribuyente
                {
                    Id = 1,
                    RncCedula = "98754321012",
                    Nombre = "JUAN PEREZ",
                    Tipo = "PERSONA FISICA",
                    Estatus = "activo"
                },
                new Contribuyente
                {
                    Id = 2,
                    RncCedula = "123456789",
                    Nombre = "FARMACIA TU SALUD",
                    Tipo = "PERSONA JURIDICA",
                    Estatus = "inactivo"
                },
                new Contribuyente
                {
                    Id = 3,
                    RncCedula = "374892013",
                    Nombre = "FERRETERIA EL LIDER",
                    Tipo = "PERSONA JURIDICA",
                    Estatus = "inactivo"
                },
                new Contribuyente
                {
                    Id = 4,
                    RncCedula = "348392213",
                    Nombre = "MEDICO LA SALUD PRIMERO",
                    Tipo = "PERSONA JURIDICA",
                    Estatus = "inactivo"
                }
            );

            modelBuilder.Entity<ComprobanteFiscal>().HasData(
                new ComprobanteFiscal
                {
                    Id = 1,
                    RncCedula = "98754321012",
                    NCF = "E310000000001",
                    Monto = 200.00,
                    Itbis18 = 36.00,
                    ContribuyenteId = 1
                },
                new ComprobanteFiscal
                {
                    Id = 2,
                    RncCedula = "98754321012",
                    NCF = "E310000000002",
                    Monto = 1000.00,
                    Itbis18 = 180.00,
                    ContribuyenteId = 1
                },
                new ComprobanteFiscal
                {
                    Id = 3,
                    RncCedula = "123456789",
                    NCF = "E310000000003",
                    Monto = 500.00,
                    Itbis18 = 90.00,
                    ContribuyenteId = 2
                },
                new ComprobanteFiscal
                {
                    Id = 4,
                    RncCedula = "348392213",
                    NCF = "E310000000004",
                    Monto = 800.00,
                    Itbis18 = 100.00,
                    ContribuyenteId = 2
                },
                new ComprobanteFiscal
                {
                    Id = 5,
                    RncCedula = "347395213",
                    NCF = "E310000000004",
                    Monto = 1000.00,
                    Itbis18 = 120.00,
                    ContribuyenteId = 3
                }


            );
        }
    }
}
