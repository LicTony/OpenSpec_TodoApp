## Context

La aplicación TodoApp es un gestor de tareas de consola en C# (.NET 10). Actualmente las tareas se almacenan en una `List<string>` en memoria, lo que significa que se pierden al cerrar la aplicación. El código ya incluye comentarios TODO que describen exactamente la implementación necesaria.

**Estado actual:**
- Estructura: Aplicación de consola simple con funciones locales
- Almacenamiento: `List<string>` en memoria
- Ubicación: Un solo archivo [`Program.cs`](Program.cs)

**Restricciones:**
- Mantener simplicidad de la aplicación (sin arquitectura compleja)
- Usar bibliotecas estándar de .NET (sin dependencias externas)
- El archivo JSON debe ser legible y editable manualmente

## Goals / Non-Goals

**Goals:**
- Persistir tareas en archivo JSON entre sesiones
- Cargar tareas automáticamente al iniciar
- Guardar tareas automáticamente después de cada modificación
- Manejar gracefully la ausencia del archivo (primera ejecución)
- Manejar errores de deserialización (archivo corrupto)

**Non-Goals:**
- No implementar backup automático del archivo
- No implementar encriptación de datos
- No implementar migración de versiones de formato
- No cambiar la estructura de datos (seguirá siendo `List<string>`)

## Decisions

### 1. Formato de almacenamiento: JSON
**Decisión:** Usar `System.Text.Json` para serialización/deserialización.

**Alternativas consideradas:**
- **CSV**: Más simple pero menos estructurado, problemas con tareas que contienen comas
- **Binary**: Más eficiente pero no legible por humanos
- **SQLite**: Overkill para una lista simple de strings

**Razón:** JSON es legible, estándar, y `System.Text.Json` está incluido en .NET sin dependencias adicionales.

### 2. Ubicación del archivo: Directorio de trabajo actual
**Decisión:** El archivo `tareas.json` se crea en el directorio de trabajo actual.

**Alternativas consideradas:**
- **AppData**: Más "correcto" pero añade complejidad de rutas
- **Directorio del ejecutable**: Similar pero menos predecible

**Razón:** Simplicidad. El usuario puede encontrar y editar el archivo fácilmente.

### 3. Estrategia de guardado: Inmediato
**Decisión:** Guardar después de cada operación de modificación (agregar/eliminar).

**Alternativas consideradas:**
- **Guardado periódico**: Riesgo de pérdida de datos si la app cierra inesperadamente
- **Guardado al salir**: Requiere interceptar el cierre, más complejo

**Razón:** Simplicidad y garantía de no pérdida de datos.

### 4. Manejo de errores: Fail-safe
**Decisión:** Si el archivo no existe o está corrupto, iniciar con lista vacía.

**Razón:** Mejor experiencia de usuario que bloquear la aplicación.

## Risks / Trade-offs

| Riesgo | Mitigación |
|--------|------------|
| Archivo corrupto por cierre inesperado durante escritura | JSON se escribe en una sola operación atómica (`WriteAllText`). Riesgo bajo. |
| Archivo crece mucho con muchas tareas | JSON es eficiente. Para miles de tareas seguiría siendo manejable. |
| Usuario edita manualmente el JSON y lo rompe | Try-catch en carga. Si falla, inicia vacío y continúa. |
| Permisos de escritura en directorio | Excepción no manejada es aceptable - informa al usuario del problema. |

## Migration Plan

No se requiere migración - es una nueva funcionalidad. El archivo se creará automáticamente en la primera ejecución que modifique la lista de tareas.

**Rollback:** Simplemente eliminar el archivo `tareas.json` para volver al estado sin persistencia.
