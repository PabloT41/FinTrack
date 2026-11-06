
# FinTrack - Sistema de Gestión Financiera

Sistema web de administración financiera desarrollado con ASP.NET Core MVC, Entity Framework Core y SQL Server.

FinTrack permite administrar categorías financieras y registrar movimientos de ingresos y gastos mediante una interfaz web con autenticación y control de acceso administrativo.

## 1. Objetivo del proyecto

Desarrollar una aplicación web que permita gestionar información financiera mediante operaciones CRUD, aplicando el patrón arquitectónico Modelo-Vista-Controlador (MVC), persistencia de datos relacionales y validaciones del lado del servidor.

## 2. Tecnologías utilizadas

| Tecnología | Descripción |
|---|---|
| C# | Lenguaje de programación |
| ASP.NET Core MVC | Framework web (.NET 10) |
| Entity Framework Core | Acceso y persistencia de datos |
| SQL Server | Sistema gestor de base de datos |
| ASP.NET Core Identity | Autenticación y gestión de usuarios |
| Razor Views | Generación de vistas dinámicas |
| Bootstrap | Diseño de la interfaz |
| Git | Control de versiones |
| GitHub | Repositorio de código fuente |

## 3. Arquitectura del sistema

FinTrack utiliza el patrón Modelo-Vista-Controlador (MVC).

### Modelo (Model)

Representa las entidades del sistema y sus reglas de validación.

Entidades principales:

- Categoria
- Movimiento

### Vista (View)

Contiene las interfaces desarrolladas con Razor y Bootstrap.

Permite visualizar, registrar, editar y eliminar información financiera.

### Controlador (Controller)

Procesa las solicitudes HTTP, aplica las validaciones correspondientes e interactúa con la base de datos mediante Entity Framework Core.

Controladores principales:

- HomeController
- CategoriasController
- MovimientosController

## 4. Funcionalidades

### Gestión de categorías

- Registrar categorías financieras.
- Consultar categorías existentes.
- Editar información de categorías.
- Eliminar categorías, según las restricciones de integridad.
- Clasificar categorías como ingresos o gastos.
- Gestionar su estado activo o inactivo.

### Gestión de movimientos

- Registrar movimientos financieros.
- Consultar movimientos registrados.
- Editar movimientos.
- Eliminar movimientos.
- Asociar movimientos con categorías existentes.
- Registrar concepto, monto, fecha y descripción.

### Validaciones

El sistema incorpora validaciones del lado del servidor para proteger la integridad de los datos.

Entre ellas:

- El monto de un movimiento debe ser mayor que cero.
- La categoría seleccionada debe existir.
- La categoría seleccionada debe encontrarse activa.
- Se utiliza una relación entre movimientos y categorías.

### Autenticación y autorización

FinTrack utiliza ASP.NET Core Identity.

Características:

- Inicio de sesión mediante credenciales.
- Contraseñas protegidas mediante el sistema de Identity.
- Autorización basada en roles.
- Acceso administrativo restringido.
- Registro público de usuarios deshabilitado.
- Cierre de sesión.

Los módulos administrativos están protegidos mediante el rol Admin.

## 5. Base de datos

La aplicación utiliza SQL Server y Entity Framework Core para administrar la persistencia.

### Tabla Categorias

| Campo | Descripción |
|---|---|
| Id | Identificador de la categoría |
| Nombre | Nombre de la categoría |
| Tipo | Ingreso o gasto |
| Descripcion | Información adicional |
| Activa | Estado de la categoría |

### Tabla Movimientos

| Campo | Descripción |
|---|---|
| Id | Identificador del movimiento |
| Concepto | Nombre o concepto del movimiento |
| Monto | Valor monetario |
| Fecha | Fecha del movimiento |
| CategoriaId | Clave foránea de categoría |
| Descripcion | Información adicional |

La relación entre Categorias y Movimientos es de uno a muchos.

El campo Monto se configura como decimal(18,2) para almacenar valores monetarios con dos decimales.

ASP.NET Core Identity incorpora tablas adicionales para usuarios, roles y autenticación.

## 6. Estructura del proyecto

```text
FinTrack/
|
|-- Areas/
|   |-- Identity/
|       |-- Pages/
|
|-- Controllers/
|   |-- HomeController.cs
|   |-- CategoriasController.cs
|   |-- MovimientosController.cs
|
|-- Data/
|   |-- ApplicationDbContext.cs
|
|-- Migrations/
|
|-- Models/
|   |-- Categoria.cs
|   |-- Movimiento.cs
|
|-- Views/
|   |-- Home/
|   |-- Categorias/
|   |-- Movimientos/
|   |-- Shared/
|
|-- wwwroot/
|
|-- Program.cs
|-- appsettings.json
|-- FinTrack.csproj
|-- README.md
```

## 7. Requisitos para ejecutar el proyecto

Se necesita:

- .NET SDK 10.
- SQL Server.
- Visual Studio Code o Visual Studio.
- Git.
- Acceso a una instancia de SQL Server con permisos para crear o actualizar la base de datos.

## 8. Instalación y ejecución

### Clonar el repositorio

```bash
git clone https://github.com/PabloT41/FinTrack.git
cd FinTrack
```

### Restaurar dependencias

```bash
dotnet restore
```

### Configurar la base de datos

Configurar la cadena de conexión `DefaultConnection` en `appsettings.Development.json` o mediante variables de entorno.

Ejemplo para una instancia local con autenticación de Windows:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FinTrackDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

La configuración del servidor debe adaptarse a cada equipo.

### Aplicar migraciones

```bash
dotnet ef database update
```

### Configurar el administrador

El proyecto permite inicializar un administrador mediante las variables de entorno:

- FINTRACK_ADMIN_EMAIL
- FINTRACK_ADMIN_PASSWORD

Estas variables deben configurarse de manera privada, sin publicar las credenciales en el repositorio.

Ejemplo en PowerShell:

```powershell
$env:FINTRACK_ADMIN_EMAIL = "admin@fintrack.local"
$securePassword = Read-Host "Contraseña del administrador" -AsSecureString
$env:FINTRACK_ADMIN_PASSWORD = [System.Net.NetworkCredential]::new("", $securePassword).Password
```

### Ejecutar la aplicación

```bash
dotnet run
```

Abrir la dirección local indicada en la terminal.

## 9. Seguridad

FinTrack implementa medidas de seguridad como:

- Autenticación mediante ASP.NET Core Identity.
- Autorización por roles.
- Validación de datos desde el backend.
- Restricción de acceso a operaciones administrativas.
- Registro público deshabilitado.
- Configuración privada de credenciales.
- Separación de configuraciones de desarrollo y producción.

Las credenciales administrativas no deben almacenarse en el código fuente.

## 10. Control de versiones

El proyecto utiliza Git para gestionar el historial de cambios y GitHub para alojar el código fuente.

Repositorio:

https://github.com/PabloT41/FinTrack

## 11. Estado del proyecto

Funcionalidades implementadas:

- Arquitectura MVC.
- Persistencia con SQL Server.
- Gestión de categorías.
- Gestión de movimientos.
- Validaciones de backend.
- Autenticación mediante Identity.
- Autorización administrativa.
- Repositorio GitHub.

Próximas actividades:

- Despliegue en un servidor web.
- Pruebas finales de funcionamiento.
- Elaboración del video demostrativo.

## 12. Autor

Proyecto académico desarrollado para la carrera de Ingeniería de Software.

**Repositorio:** [FinTrack](https://github.com/PabloT41/FinTrack)

## 13. Licencia

Proyecto desarrollado con fines académicos y educativos. No se ha establecido una licencia de distribución.
