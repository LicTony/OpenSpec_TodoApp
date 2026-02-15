## Why

La aplicación actual persiste tareas en un archivo JSON (`tareas.json`), lo cual es simple pero limitado. Este enfoque presenta problemas de concurrencia, escalabilidad, integridad de datos y falta de capacidades de consulta avanzadas. Migrar a SQLite con Dapper proporcionará una base de datos robusta con transacciones ACID, mejor manejo de concurrencia y la posibilidad de agregar funcionalidades avanzadas como tareas completadas, fechas, prioridades y búsquedas eficientes.

## What Changes

- **BREAKING**: Cambiar la persistencia de JSON a SQLite usando Dapper como ORM
- Reestructurar el código separando responsabilidades en clases (Repository pattern)
- Extender el modelo de datos de `List<string>` a una clase `Tarea` con propiedades adicionales:
  - `Id` (int, auto-incremental)
  - `Descripcion` (string)
  - `FechaCreacion` (DateTime)
  - `Completada` (bool)
  - `FechaCompletada` (DateTime?)
  - `Prioridad` (enum: Baja, Media, Alta)
- Agregar nuevas funcionalidades en el menú:
  - Marcar tarea como completada/pendiente
  - Filtrar tareas por estado (todas/pendientes/completadas)
  - Ordenar por prioridad o fecha
- Implementar migración automática de datos existentes desde `tareas.json` a SQLite
- Mantener la interfaz de consola simple y amigable

## Capabilities

### New Capabilities
- `sqlite-persistence`: Persistencia de tareas usando SQLite con Dapper, incluyendo inicialización de base de datos, esquema y migraciones
- `task-management-enhanced`: Gestión avanzada de tareas con estados (completada/pendiente), prioridades, fechas y filtros
- `data-migration`: Migración automática de datos desde el formato JSON legacy al nuevo formato SQLite

### Modified Capabilities
<!-- No hay capacidades existentes que modificar, este es un proyecto nuevo -->

## Impact

**Código afectado:**
- `Program.cs`: Refactorización completa para separar lógica de negocio de presentación
- Nuevos archivos:
  - `Models/Tarea.cs`: Modelo de dominio
  - `Data/TareaRepository.cs`: Capa de acceso a datos con Dapper
  - `Data/DatabaseInitializer.cs`: Inicialización y migración de BD
  - `Services/TareaService.cs`: Lógica de negocio
  - `UI/ConsoleUI.cs`: Interfaz de usuario de consola

**Dependencias:**
- Agregar paquetes NuGet:
  - `Microsoft.Data.Sqlite` (SQLite provider)
  - `Dapper` (micro-ORM)

**Datos:**
- Migración de `tareas.json` → `tareas.db`
- El archivo JSON se preservará como backup después de la migración
