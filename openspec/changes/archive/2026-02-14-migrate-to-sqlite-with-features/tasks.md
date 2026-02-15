## 1. Preparación del Proyecto

- [x] 1.1 Agregar paquete NuGet `Microsoft.Data.Sqlite`
- [x] 1.2 Agregar paquete NuGet `Dapper`
- [x] 1.3 Crear estructura de carpetas (Models, Data, Services, UI)

## 2. Modelo de Dominio

- [x] 2.1 Crear enum `Prioridad` con valores Baja, Media, Alta
- [x] 2.2 Crear clase `Tarea` con propiedades: Id, Descripcion, FechaCreacion, Completada, FechaCompletada, Prioridad

## 3. Capa de Datos - Inicialización

- [x] 3.1 Crear clase `DatabaseInitializer` con método para crear esquema SQLite
- [x] 3.2 Implementar creación de tabla `Tareas` con todas las columnas especificadas
- [x] 3.3 Implementar verificación de existencia de base de datos antes de crear

## 4. Capa de Datos - Repository

- [x] 4.1 Crear clase `TareaRepository` con conexión SQLite
- [x] 4.2 Implementar método `Agregar(Tarea)` usando Dapper
- [x] 4.3 Implementar método `ObtenerTodas()` usando Dapper
- [x] 4.4 Implementar método `ObtenerPorId(int)` usando Dapper
- [x] 4.5 Implementar método `Actualizar(Tarea)` usando Dapper
- [x] 4.6 Implementar método `Eliminar(int)` usando Dapper
- [x] 4.7 Implementar método `ObtenerPorEstado(bool completada)` para filtrado
- [x] 4.8 Implementar método `MarcarComoCompletada(int id)` con timestamp
- [x] 4.9 Implementar método `MarcarComoPendiente(int id)` limpiando FechaCompletada

## 5. Migración de Datos Legacy

- [x] 5.1 Agregar método en `DatabaseInitializer` para detectar `tareas.json`
- [x] 5.2 Implementar lectura y deserialización de `tareas.json`
- [x] 5.3 Implementar conversión de strings a objetos `Tarea` con valores default
- [x] 5.4 Implementar inserción de tareas migradas en SQLite
- [x] 5.5 Implementar renombrado de `tareas.json` a `tareas.json.backup.{timestamp}`
- [x] 5.6 Agregar manejo de errores para JSON corrupto
- [x] 5.7 Implementar logging de progreso de migración

## 6. Capa de Servicio

- [x] 6.1 Crear clase `TareaService` con inyección de `TareaRepository`
- [x] 6.2 Implementar validación de descripción (no vacía, máximo 500 caracteres)
- [x] 6.3 Implementar método `CrearTarea(string descripcion, Prioridad prioridad)` con timestamp automático
- [x] 6.4 Implementar método `ObtenerTareas(filtro, orden)` con lógica de ordenamiento
- [x] 6.5 Implementar método `CambiarEstadoTarea(int id)` toggle completada/pendiente
- [x] 6.6 Implementar método `EliminarTarea(int id)` con validación de existencia

## 7. Interfaz de Usuario - ConsoleUI

- [x] 7.1 Crear clase `ConsoleUI` con métodos para cada pantalla
- [x] 7.2 Implementar `MostrarMenuPrincipal()` con opciones actualizadas
- [x] 7.3 Implementar `MostrarFormularioAgregarTarea()` con selección de prioridad
- [x] 7.4 Implementar `MostrarListaTareas()` con metadata (prioridad, fechas, estado)
- [x] 7.5 Implementar símbolos visuales (✓ para completadas)
- [x] 7.6 Implementar `MostrarOpcionesFiltro()` (Todas/Pendientes/Completadas)
- [x] 7.7 Implementar `MostrarOpcionesOrdenamiento()` (Fecha/Prioridad/Estado)
- [x] 7.8 Implementar formateo de fechas en formato local corto
- [x] 7.9 Implementar colores en consola para prioridades (opcional, con fallback)

## 8. Refactorización de Program.cs

- [x] 8.1 Eliminar funciones locales antiguas (CargarTareas, GuardarTareas, etc.)
- [x] 8.2 Inicializar `DatabaseInitializer` y ejecutar migración si es necesario
- [x] 8.3 Crear instancias de `TareaRepository`, `TareaService` y `ConsoleUI`
- [x] 8.4 Implementar loop principal delegando a `ConsoleUI`
- [x] 8.5 Mantener solo lógica de inicialización y coordinación en Program.cs

## 9. Testing Manual

- [x] 9.1 Probar migración desde `tareas.json` existente
- [x] 9.2 Verificar que backup se crea correctamente con timestamp
- [x] 9.3 Probar agregar tarea con diferentes prioridades
- [x] 9.4 Probar marcar tarea como completada y verificar timestamp
- [x] 9.5 Probar marcar tarea completada como pendiente
- [x] 9.6 Probar filtro de tareas pendientes
- [x] 9.7 Probar filtro de tareas completadas
- [x] 9.8 Probar ordenamiento por fecha de creación
- [x] 9.9 Probar ordenamiento por prioridad
- [x] 9.10 Probar eliminación de tarea con confirmación
- [x] 9.11 Verificar que datos persisten correctamente en SQLite

## 10. Limpieza y Documentación

- [x] 10.1 Eliminar código comentado o no utilizado
- [x] 10.2 Agregar comentarios XML en clases públicas
- [x] 10.3 Actualizar README.md con nuevas funcionalidades (si existe)
- [x] 10.4 Verificar que `tareas.json` ya no se usa en el código
