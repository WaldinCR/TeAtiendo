🍽️ Te Atiendo - Plataforma de Reservas y Órdenes
Te Atiendo es una solución integral diseñada para optimizar la interacción entre comensales y establecimientos gastronómicos. El sistema permite centralizar procesos de reserva de mesas, pedidos anticipados y gestión administrativa bajo una arquitectura robusta, segura y escalable.

🏗️ Arquitectura del Sistema
El proyecto sigue una Arquitectura de N-Capas (N-Tier) combinada con un enfoque orientado al dominio para garantizar la separación de responsabilidades y la mantenibilidad.

Tecnologías Utilizadas
Backend: ASP.NET Core Web API con .NET 8.

Frontend Web: Blazor WebAssembly (WASM).

Aplicación Desktop: WPF / C# bajo el patrón MVVM.

Persistencia: SQL Server gestionado a través de Entity Framework Core.

Servicios Externos: Integración con Stripe (Pagos) y SendGrid (Notificaciones).

📂 Estructura del Repositorio
Plaintext
TeAtiendo/
├── 📁 TeAtiendo.API           # Núcleo lógico y servicios REST (ASP.NET Core)
├── 📁 TeAtiendo.Domain        # Entidades de negocio, interfaces y reglas de dominio
├── 📁 TeAtiendo.Application   # DTOs, Mapeos y lógica de aplicación
├── 📁 TeAtiendo.Persistence   # Acceso a datos (Repositorios, Unit of Work, DbContext)
├── 📁 TeAtiendo.Web           # Cliente Web interactivo (Blazor WASM)
├── 📁 TeAtiendo.Desktop       # Panel administrativo avanzado (WPF MVVM)
└── 📁 TeAtiendo.IOC           # Registro de inyección de dependencias
🌟 Características Principales
Gestión de Reservas: Validación de disponibilidad de mesas en tiempo real para evitar duplicidad de horarios.

Órdenes Anticipadas: Preselección de platos para optimizar el servicio en cocina y reducir tiempos de espera.

Procesamiento de Pagos: Transacciones financieras seguras autorizadas y verificadas vía Stripe API.

Trazabilidad y Auditoría: Registro exhaustivo de operaciones (AuditoriaLog) para garantizar transparencia.

Control de Acceso: Sistema basado en roles (Cliente, Restaurante y Administrador) con seguridad JWT.

🚀 Instalación y Configuración
Requisitos Previos
Visual Studio 2022 (con carga de trabajo .NET y desarrollo web).

SQL Server local o remoto.

SDK de .NET 8.0.

Pasos
Clonar el repositorio:

Bash
git clone https://github.com/tu-usuario/TeAtiendo.git
Configurar Base de Datos:
Actualiza la cadena de conexión en appsettings.json dentro de TeAtiendo.API y ejecuta las migraciones:

Bash
dotnet ef database update --project TeAtiendo.Persistence --startup-project TeAtiendo.API
Iniciar la API:
Establece TeAtiendo.API como proyecto de inicio y ejecútalo.

Iniciar Clientes:
Ejecuta TeAtiendo.Web o TeAtiendo.Desktop para interactuar con el sistema.

👥 Créditos
Waldin Ceballos (2025-1112)

Robert Francisco Leclerc (2025-1390)

Profesor: Francis Ramirez
