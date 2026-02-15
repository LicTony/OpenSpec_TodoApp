## ADDED Requirements

### Requirement: Prioridad en Creación

El usuario debe poder especificar la prioridad de una nueva tarea al momento de crearla.

#### Scenario: Crear con Prioridad Alta

- **WHEN** el usuario selecciona "Alta" en el selector de prioridad y agrega una tarea
- **THEN** la tarea se crea con prioridad Alta.

### Requirement: Control de Ordenamiento

La interfaz debe permitir reordenar la lista de tareas según diferentes criterios.

#### Scenario: Ordenar por Prioridad

- **WHEN** el usuario selecciona "Prioridad" en el control de orden
- **THEN** las tareas se reordenan mostrando primero las de prioridad Alta, luego Media, luego Baja.

#### Scenario: Ordenar por Estado

- **WHEN** el usuario selecciona "Estado" en el control de orden
- **THEN** las tareas pendientes se muestran antes que las completadas.

## MODIFIED Requirements

### Requirement: Edición de Tareas (Avanzada)

El sistema debe permitir editar múltiples atributos de una tarea (descripción, prioridad, estado) en una sola acción.

#### Scenario: Abrir Diálogo de Edición

- **WHEN** el usuario solicita editar una tarea
- **THEN** se muestra un diálogo con los valores actuales de la tarea (descripción, prioridad, estado).

#### Scenario: Guardar Cambios

- **WHEN** el usuario modifica los valores en el diálogo y confirma
- **THEN** la tarea se actualiza con los nuevos valores y el diálogo se cierra.
