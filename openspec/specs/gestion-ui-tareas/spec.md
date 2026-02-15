## ADDED Requirements

### Requirement: Visualización de Tareas

La aplicación debe mostrar una lista de tareas clara y legible, donde cada elemento presente la información relevante de forma concisa.

#### Scenario: Listado Inicial

- **WHEN** el usuario abre la aplicación y tiene tareas registradas
- **THEN** la aplicación muestra la lista completa de tareas.
- **AND** cada tarea muestra su Descripción, Prioridad y Estado.
- **AND** las tareas completadas se diferencian visualmente (ej. texto tachado, color atenuado).

### Requirement: Visualización de lista de tareas con límite

La lista principal de tareas ya no mostrará la totalidad de las tareas existentes, sino que estará limitada al conjunto devuelto por la página actual.

#### Scenario: Sincronización con el tamaño de página

- **WHEN** Se define un tamaño de página de 10 tareas.
- **THEN** La lista visual en `MainWindow` nunca debe exceder los 10 elementos simultáneos, independientemente del total de tareas en la base de datos.

### Requirement: Gestión de Estado de Tareas

El usuario debe poder cambiar el estado de una tarea directamente desde la interfaz principal.

#### Scenario: Completar Tarea

- **WHEN** el usuario hace clic en el checkbox o botón de "Completar" de una tarea pendiente
- **THEN** la tarea se marca como completada visualmente.
- **AND** se registra internamente la fecha de completado.

#### Scenario: Reabrir Tarea

- **WHEN** el usuario desmarca una tarea completada
- **THEN** la tarea vuelve a estado pendiente.
- **AND** se limpia la fecha de completado.

### Requirement: Filtrado de Tareas

La interfaz debe permitir al usuario filtrar las tareas visibles según su estado.

#### Scenario: Aplicar Filtro

- **WHEN** el usuario selecciona una opción de filtro (Todas, Pendientes, Completadas)
- **THEN** la lista se actualiza inmediatamente mostrando solo las tareas que coinciden con el criterio.

### Requirement: Creación de Tareas

Debe existir un mecanismo rápido y accesible para agregar nuevas tareas.

#### Scenario: Agregar Tarea Válida

- **WHEN** el usuario ingresa texto en el campo de nueva tarea y presiona Enter o el botón "Agregar"
- **THEN** se crea una nueva tarea con prioridad "Media" por defecto.
- **AND** el campo de texto se limpia.
- **AND** la nueva tarea aparece en la lista si el filtro actual lo permite.

#### Scenario: Intentar Agregar Vacía

- **WHEN** el usuario intenta agregar una tarea sin texto
- **THEN** se muestra un mensaje de alerta indicando que la descripción es requerida.
- **AND** no se crea ninguna tarea.

#### Scenario: Limpiar Tareas

- **WHEN** el usuario elimina una tarea
- **THEN** la tarea desaparece de la lista permanentemente.

### Requirement: Propiedades de Ventana Principal

La ventana principal debe presentarse de manera profesional y maximizada.

#### Scenario: Inicio de Aplicación

- **WHEN** el usuario inicia la aplicación
- **THEN** la ventana aparece maximizada.
- **AND** el título de la ventana es "Todo App".

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

### Requirement: Edición de Tareas

El sistema debe permitir editar múltiples atributos de una tarea (descripción, prioridad, estado) en una sola acción.

#### Scenario: Abrir Diálogo de Edición

- **WHEN** el usuario solicita editar una tarea
- **THEN** se muestra un diálogo con los valores actuales de la tarea (descripción, prioridad, estado).

#### Scenario: Guardar Cambios

- **WHEN** el usuario modifica los valores en el diálogo y confirma
- **THEN** la tarea se actualiza con los nuevos valores y el diálogo se cierra.
