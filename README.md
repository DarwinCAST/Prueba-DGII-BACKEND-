# 📄 Prueba DGII - Backend API

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET-Core-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Tests](https://img.shields.io/badge/tests-xUnit-orange)

API REST desarrollada en **ASP.NET Core Web API** para la gestión de **Contribuyentes y Comprobantes Fiscales**, permitiendo registrar facturas y calcular automáticamente el **ITBIS (18%)** asociado a cada comprobante.

Este proyecto fue desarrollado como una **prueba técnica**, aplicando buenas prácticas de arquitectura, validaciones, documentación y pruebas unitarias.

---

# 📚 Tabla de Contenidos

- [Tecnologías](#-tecnologías)
- [Arquitectura](#-arquitectura-del-proyecto)
- [Instalación](#-instalación)
- [Uso](#-uso)
- [Endpoints](#-endpoints)
- [Pruebas Unitarias](#-pruebas-unitarias)

---

# 🛠 Tecnologías

Este proyecto fue construido utilizando las siguientes tecnologías:

| Tecnología | Descripción |
|------------|-------------|
| .NET 8 | Framework principal |
| ASP.NET Core Web API | Creación de API REST |
| Entity Framework Core | ORM para acceso a datos |
| SQL Server | Base de datos |
| Swagger / OpenAPI | Documentación de API |
| xUnit | Framework de pruebas |
| FluentAssertions | Validaciones en tests |
| EF Core InMemory | Base de datos para pruebas |
# 🏗 Arquitectura del Proyecto

El proyecto sigue una arquitectura simple basada en **Controllers + Models + DTOs + DbContext**.
## 📁 Estructura del Proyecto

```text
Prueba_DGII
│
├── Controllers
│   ├── ContribuyentesController.cs
│   └── ComprobantesController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Models
│   ├── Contribuyente.cs
│   └── ComprobanteFiscal.cs
│
├── DTOs
│   ├── CrearContribuyenteDto.cs
│   ├── CrearComprobanteDto.cs
│   ├── ContribuyenteDetalleDto.cs
│   └── ComprobanteFiscalDto.cs
│
└── Program.cs
```
# 🚀 Instalación

## 1️⃣ Clonar el repositorio

```text
git clone https://github.com/tu-usuario/prueba-dgii-backend.git
```
## 2️⃣ Entrar al proyecto
```text
cd prueba-dgii-backend
```
## 3️⃣ Restaurar dependencias
```text
dotnet restore
Update-database
```
## 4️⃣ Ejecutar la aplicación
```text
dotnet run
```
## ▶️ Uso
Una vez ejecutada la aplicación, la API estará disponible en:
```text
https://localhost:{puerto}
```
La documentación interactiva de Swagger estará disponible en:
```text
https://localhost:{puerto}/swagger
```
## 📡Endpoints
## Obtener todos los contribuyentes
```text
GET /api/contribuyentes
```
Ejemplo de respuesta:
```text
[
  {
    "id": 1,
    "rncCedula": "98754321012",
    "nombre": "JUAN PEREZ",
    "tipo": "PERSONA FISICA",
    "estatus": "activo",
    "cantidadComprobantes": 2
  }
]
```
## Obtener comprobantes de un contribuyente
```text
GET /api/contribuyentes/{rnc}/comprobantes
```
Ejemplo de respuesta:
```text
{
  "rncCedula": "98754321012",
  "nombre": "JUAN PEREZ",
  "totalITBIS": 216,
  "comprobantes": [
    {
      "rncCedula": "98754321012",
      "ncf": "E310000000001",
      "monto": 200,
      "itbis18": 36
    }
  ]
}
```
## Crear contribuyente
```text
POST /api/contribuyentes
```
Body:
```text
{
  "rncCedula": "123456789",
  "nombre": "SUPERMERCADO CENTRAL",
  "tipo": "PERSONA JURIDICA",
  "estatus": "activo"
}
```
## Comprobantes
Obtener todos los comprobantes
```text
GET /api/comprobantes
```
## Crear comprobante fiscal
```text
POST /api/comprobantes
```
Body:
```text
{
  "rncCedula": "98754321012",
  "ncf": "E310000000003",
  "monto": 500
}
```
## 🧪 Pruebas Unitarias
El proyecto incluye pruebas unitarias para validar la lógica de negocio.

Frameworks utilizados:
- xUnit
- FluentAssertions
- EF Core InMemory

## Para ejecutar los tests:
```text
dotnet test
```
