## ADDED Requirements

### Requirement: Mark task as completed

El sistema SHALL permitir marcar una tarea como completada, registrando la fecha y hora de completado.

#### Scenario: Mark pending task as completed

- **WHEN** el usuario marca una tarea pendiente como completada
- **THEN** el campo `Completada` se establece en `true` y `FechaCompletada` se establece con la fecha/hora actual

#### Scenario: Mark completed task as pending

- **WHEN** el usuario marca una tarea completada como pendiente
- **THEN** el campo `Completada` se establece en `false` y `FechaCompletada` se establece en `null`

### Requirement: Task priority levels

El sistema SHALL soportar tres niveles de prioridad para las tareas: Baja, Media y Alta.

#### Scenario: Create task with priority

- **WHEN** se crea una nueva tarea
- **THEN** el usuario puede especificar la prioridad (Baja, Media o Alta)

#### Scenario: Default priority is Media

- **WHEN** se crea una tarea sin especificar prioridad
- **THEN** la prioridad se establece automáticamente como Media

#### Scenario: Change task priority

- **WHEN** el usuario modifica la prioridad de una tarea existente
- **THEN** el sistema actualiza la prioridad en la base de datos

### Requirement: Filter tasks by status

El sistema SHALL permitir filtrar tareas por su estado de completado.

#### Scenario: View all tasks

- **WHEN** el usuario selecciona "Ver todas las tareas"
- **THEN** se muestran tanto tareas pendientes como completadas

#### Scenario: View only pending tasks

- **WHEN** el usuario selecciona "Ver tareas pendientes"
- **THEN** se muestran solo las tareas con `Completada = false`

#### Scenario: View only completed tasks

- **WHEN** el usuario selecciona "Ver tareas completadas"
- **THEN** se muestran solo las tareas con `Completada = true`

### Requirement: Sort tasks

El sistema SHALL permitir ordenar las tareas por diferentes criterios.

#### Scenario: Sort by creation date

- **WHEN** el usuario ordena por fecha de creación
- **THEN** las tareas se muestran de más reciente a más antigua

#### Scenario: Sort by priority

- **WHEN** el usuario ordena por prioridad
- **THEN** las tareas se muestran en orden: Alta, Media, Baja

#### Scenario: Sort by completion status

- **WHEN** el usuario ordena por estado
- **THEN** las tareas pendientes se muestran primero, seguidas de las completadas

### Requirement: Task creation timestamp

El sistema SHALL registrar automáticamente la fecha y hora de creación de cada tarea.

#### Scenario: Automatic timestamp on creation

- **WHEN** se crea una nueva tarea
- **THEN** el campo `FechaCreacion` se establece automáticamente con la fecha/hora actual

#### Scenario: Creation timestamp is immutable

- **WHEN** se modifica una tarea existente
- **THEN** el campo `FechaCreacion` permanece sin cambios

### Requirement: Display task metadata

El sistema SHALL mostrar información adicional de las tareas en la interfaz de consola.

#### Scenario: Display task with metadata

- **WHEN** se listan las tareas
- **THEN** se muestra: número, descripción, prioridad, fecha de creación y estado (✓ si completada)

#### Scenario: Display completion date for completed tasks

- **WHEN** se muestra una tarea completada
- **THEN** se incluye la fecha de completado en el formato legible
