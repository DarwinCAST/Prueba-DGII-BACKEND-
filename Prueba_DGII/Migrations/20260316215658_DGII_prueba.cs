using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prueba_DGII.Migrations
{
    /// <inheritdoc />
    public partial class DGII_prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contribuyentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RncCedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribuyentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComprobantesFiscales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RncCedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NCF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<double>(type: "float", nullable: true),
                    Itbis18 = table.Column<double>(type: "float", nullable: true),
                    ContribuyenteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobantesFiscales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprobantesFiscales_Contribuyentes_ContribuyenteId",
                        column: x => x.ContribuyenteId,
                        principalTable: "Contribuyentes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Contribuyentes",
                columns: new[] { "Id", "Estatus", "Nombre", "RncCedula", "Tipo" },
                values: new object[,]
                {
                    { 1, "activo", "JUAN PEREZ", "98754321012", "PERSONA FISICA" },
                    { 2, "inactivo", "FARMACIA TU SALUD", "123456789", "PERSONA JURIDICA" },
                    { 3, "inactivo", "FERRETERIA EL LIDER", "374892013", "PERSONA JURIDICA" },
                    { 4, "inactivo", "MEDICO LA SALUD PRIMERO", "321892013", "PERSONA JURIDICA" }
                });

            migrationBuilder.InsertData(
                table: "ComprobantesFiscales",
                columns: new[] { "Id", "ContribuyenteId", "Itbis18", "Monto", "NCF", "RncCedula" },
                values: new object[,]
                {
                    { 1, 1, 36.0, 200.0, "E310000000001", "98754321012" },
                    { 2, 1, 180.0, 1000.0, "E310000000002", "98754321012" },
                    { 3, 2, 90.0, 500.0, "E310000000003", "123456789" },
                    { 4, 2, 100.0, 800.0, "E310000000004", "348392213" },
                    { 5, 3, 120.0, 1000.0, "E310000000005", "374892013" },
                    { 6, 4, 150.0, 1200.0, "E310000000005", "321892013" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesFiscales_ContribuyenteId",
                table: "ComprobantesFiscales",
                column: "ContribuyenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprobantesFiscales");

            migrationBuilder.DropTable(
                name: "Contribuyentes");
        }
    }
}
