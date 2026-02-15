## Why

Las tareas se almacenan actualmente en memoria y se pierden al cerrar la aplicación. Los usuarios necesitan que sus tareas persistan entre sesiones para que la aplicación sea útil en el mundo real. Este es un requisito fundamental para cualquier gestor de tareas.

## What Changes

- Agregar persistencia de datos usando archivos JSON
- Cargar tareas automáticamente al iniciar la aplicación desde `tareas.json`
- Guardar tareas automáticamente después de cada modificación (agregar/eliminar)
- Crear archivo `tareas.json` si no existe al primer uso

## Capabilities

### New Capabilities
- `task-persistence`: Capacidad de persistir tareas en almacenamiento persistente (archivo JSON) y cargarlas al iniciar la aplicación.

### Modified Capabilities
- `task-management`: El comportamiento de agregar y eliminar tareas ahora incluye guardado automático. El inicio de la aplicación ahora incluye carga de tareas existentes.

## Impact

- **Código afectado**: [`Program.cs`](Program.cs) - agregar `using System.Text.Json;`, modificar inicialización de `tareas`, agregar guardado en `AgregarTarea()` y `EliminarTarea()`
- **Nueva dependencia**: `System.Text.Json` (incluido en .NET)
- **Nuevo archivo de datos**: `tareas.json` (creado automáticamente en el directorio de trabajo)
- **Compatibilidad**: Si el archivo JSON está corrupto, la aplicación iniciará con lista vacía
