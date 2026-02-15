## Context

La aplicación TodoApp actualmente es un programa de consola simple que persiste tareas como strings en un archivo JSON. El código está completamente en `Program.cs` usando top-level statements, con funciones locales para cada operación (agregar, ver, eliminar tareas).

**Estado actual:**

- Persistencia: `System.Text.Json` → `tareas.json`
- Modelo: `List<string>` en memoria
- Arquitectura: Monolítica en un solo archivo
- Sin separación de responsabilidades

**Restricciones:**

- Mantener como aplicación de consola .NET
- Preservar la simplicidad de uso para el usuario final
- Migración automática sin intervención manual
- Compatibilidad con datos existentes

## Goals / Non-Goals

**Goals:**

- Migrar persistencia de JSON a SQLite usando Dapper
- Estructurar el código con separación de responsabilidades (Repository pattern)
- Extender modelo de datos con propiedades adicionales (prioridad, fechas, estado)
- Agregar funcionalidades de filtrado, ordenamiento y marcado de completado
- Migración automática de datos legacy sin pérdida de información
- Mantener interfaz de consola intuitiva

**Non-Goals:**

- No crear una API web o interfaz gráfica
- No implementar autenticación/autorización (app local de usuario único)
- No agregar sincronización en la nube
- No implementar recordatorios o notificaciones
- No crear tests unitarios en esta fase (puede ser un change futuro)

## Decisions

### 1. Arquitectura en capas con Repository Pattern

**Decisión:** Separar el código en capas: Models, Data, Services, UI

**Estructura propuesta:**

```
TodoApp/
├── Program.cs              # Entry point, inicialización
├── Models/
│   ├── Tarea.cs           # Modelo de dominio
│   └── Prioridad.cs       # Enum de prioridades
├── Data/
│   ├── TareaRepository.cs      # CRUD con Dapper
│   └── DatabaseInitializer.cs  # Inicialización y migración
├── Services/
│   └── TareaService.cs    # Lógica de negocio
└── UI/
    └── ConsoleUI.cs       # Interfaz de usuario
```

**Rationale:**

- **Repository Pattern** abstrae la persistencia, facilitando cambios futuros
- **Separación de responsabilidades** hace el código más mantenible y testeable
- **Capa de servicio** centraliza lógica de negocio (validaciones, reglas)
- **UI separada** permite cambiar la interfaz sin tocar la lógica

**Alternativas consideradas:**

- ❌ **Mantener todo en Program.cs**: Dificulta mantenimiento y testing
- ❌ **Clean Architecture completa**: Sobrecarga para una app de consola simple
- ✅ **Capas simples**: Balance entre estructura y simplicidad

### 2. Dapper como micro-ORM

**Decisión:** Usar Dapper en lugar de Entity Framework Core

**Rationale:**

- **Ligero**: Dapper es un micro-ORM con overhead mínimo
- **Control**: SQL explícito, sin magia ni tracking
- **Performance**: Mapeo rápido y eficiente
- **Simplicidad**: No requiere migraciones complejas ni DbContext

**Alternativas consideradas:**

- ❌ **Entity Framework Core**: Demasiado pesado para CRUD simple
- ❌ **ADO.NET puro**: Demasiado verboso, mucho boilerplate
- ✅ **Dapper**: Sweet spot entre control y productividad

### 3. Modelo de datos enriquecido

**Decisión:** Clase `Tarea` con propiedades adicionales

```csharp
public class Tarea
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public bool Completada { get; set; }
    public DateTime? FechaCompletada { get; set; }
    public Prioridad Prioridad { get; set; }
}

public enum Prioridad
{
    Baja = 0,
    Media = 1,
    Alta = 2
}
```

**Rationale:**

- **Id auto-incremental**: Identificador único estable
- **Fechas**: Auditoría y ordenamiento temporal
- **Estado completada**: Funcionalidad esencial de TODO app
- **Prioridad**: Permite organización por importancia
- **Enum para prioridad**: Type-safe, fácil de extender

**Alternativas consideradas:**

- ❌ **Solo string**: Insuficiente para funcionalidades avanzadas
- ❌ **Agregar categorías/tags**: Scope creep, puede ser change futuro
- ✅ **Modelo actual**: Funcionalidades esenciales sin complejidad excesiva

### 4. Esquema SQLite

**Decisión:** Tabla única `Tareas` con columnas mapeadas directamente

```sql
CREATE TABLE IF NOT EXISTS Tareas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Descripcion TEXT NOT NULL,
    FechaCreacion TEXT NOT NULL,
    Completada INTEGER NOT NULL DEFAULT 0,
    FechaCompletada TEXT NULL,
    Prioridad INTEGER NOT NULL DEFAULT 1
);
```

**Rationale:**

- **Fechas como TEXT**: SQLite no tiene tipo DATE, usar ISO 8601 es estándar
- **Completada como INTEGER**: SQLite no tiene BOOLEAN, 0/1 es convención
- **Prioridad como INTEGER**: Mapeo directo del enum
- **Sin índices iniciales**: Volumen pequeño, agregar si es necesario

**Alternativas consideradas:**

- ❌ **Múltiples tablas normalizadas**: Overkill para este caso
- ❌ **Fechas como INTEGER (Unix timestamp)**: Menos legible en queries directas
- ✅ **Tabla simple con TEXT para fechas**: Estándar SQLite, compatible con Dapper

### 5. Migración automática de datos

**Decisión:** Migración en el primer arranque, con backup del JSON

**Flujo:**

1. Al iniciar, verificar si existe `tareas.json`
2. Si existe y `tareas.db` no tiene datos, migrar
3. Leer JSON, crear tareas con valores default para nuevos campos
4. Renombrar `tareas.json` → `tareas.json.backup.{timestamp}`
5. Continuar con SQLite

**Rationale:**

- **Automático**: Sin intervención del usuario
- **Seguro**: Backup preserva datos originales
- **Idempotente**: No duplica datos si se ejecuta múltiples veces
- **Timestamp en backup**: Permite múltiples backups si es necesario

**Alternativas consideradas:**

- ❌ **Migración manual**: Mala UX, propenso a errores
- ❌ **Eliminar JSON**: Riesgoso, pérdida de datos si algo falla
- ✅ **Migración automática con backup**: Seguro y conveniente

### 6. Interfaz de consola mejorada

**Decisión:** Menú principal + submenús para filtros y ordenamiento

**Flujo propuesto:**

```
=== Gestor de Tareas ===
1. Agregar tarea
2. Ver tareas
3. Marcar tarea como completada/pendiente
4. Eliminar tarea
5. Salir

[En "Ver tareas"]
- Filtro: Todas / Pendientes / Completadas
- Orden: Fecha / Prioridad / Estado
```

**Rationale:**

- **Menú simple**: Fácil de navegar
- **Opciones de visualización**: Flexibilidad sin complejidad
- **Feedback visual**: Símbolos (✓) y colores (si la terminal lo soporta)

**Alternativas consideradas:**

- ❌ **Comandos tipo CLI**: Menos intuitivo para usuarios no técnicos
- ❌ **TUI compleja (Terminal.Gui)**: Overkill, dependencia adicional
- ✅ **Menú numérico simple**: Familiar, fácil de usar

## Risks / Trade-offs

### Risk: Migración de datos corruptos

**Descripción:** Si `tareas.json` está corrupto, la migración puede fallar
**Mitigación:**

- Try-catch en deserialización JSON
- Si falla, log error y continuar con BD vacía
- JSON original se preserva para recuperación manual

### Risk: Concurrencia en SQLite

**Descripción:** SQLite tiene limitaciones con escrituras concurrentes
**Mitigación:**

- App de consola es single-user, single-process
- No es un problema en este contexto
- Si se necesita en futuro, considerar PostgreSQL/MySQL

### Trade-off: Complejidad vs Funcionalidad

**Descripción:** Más archivos y capas aumentan complejidad
**Justificación:**

- Beneficio: Código mantenible, testeable, extensible
- Costo: Más archivos para navegar
- Balance: Estructura simple pero organizada

### Trade-off: Dapper vs EF Core

**Descripción:** Dapper requiere SQL manual
**Justificación:**

- Beneficio: Control total, performance, simplicidad
- Costo: No hay change tracking ni migraciones automáticas
- Balance: Para CRUD simple, Dapper es suficiente

### Risk: Breaking change para usuarios

**Descripción:** Cambio de formato de persistencia
**Mitigación:**

- Migración automática transparente
- Backup del JSON original
- Comunicar cambio en README si es necesario

## Migration Plan

### Fase 1: Preparación

1. Agregar paquetes NuGet: `Microsoft.Data.Sqlite`, `Dapper`
2. Crear estructura de carpetas (Models, Data, Services, UI)

### Fase 2: Implementación

1. Crear modelo `Tarea` y enum `Prioridad`
2. Implementar `DatabaseInitializer` con creación de esquema
3. Implementar `TareaRepository` con operaciones CRUD
4. Implementar migración de JSON a SQLite
5. Crear `TareaService` con lógica de negocio
6. Refactorizar `ConsoleUI` con nuevas opciones
7. Actualizar `Program.cs` como entry point

### Fase 3: Testing manual

1. Probar migración desde JSON existente
2. Probar todas las operaciones CRUD
3. Probar filtros y ordenamiento
4. Verificar que backup se crea correctamente

### Fase 4: Rollback (si es necesario)

- Si la migración falla, el JSON original permanece intacto
- Usuario puede revertir a versión anterior de la app
- Restaurar desde `tareas.json.backup` si es necesario

## Open Questions

1. **¿Agregar colores en la consola?**
   - Usar `Console.ForegroundColor` para mejorar UX
   - Decisión: Sí, pero con fallback si terminal no soporta

2. **¿Validación de descripción de tarea?**
   - Longitud máxima, caracteres permitidos
   - Decisión: Mínimo 1 carácter, máximo 500, sin restricciones especiales

3. **¿Formato de fecha en display?**
   - ISO 8601 vs formato local
   - Decisión: Formato local corto para mejor legibilidad (`dd/MM/yyyy HH:mm`)
