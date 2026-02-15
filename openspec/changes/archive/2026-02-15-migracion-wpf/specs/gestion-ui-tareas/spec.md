## ADDED Requirements

### Requirement: Visualización de Tareas

La aplicación debe mostrar una lista de tareas clara y legible, donde cada elemento presente la información relevante de forma concisa.

#### Scenario: Listado Inicial

- **WHEN** el usuario abre la aplicación y tiene tareas registradas
- **THEN** la aplicación muestra la lista completa de tareas.
- **AND** cada tarea muestra su Descripción, Prioridad y Estado.
- **AND** las tareas completadas se diferencian visualmente (ej. texto tachado, color atenuado).

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

### Requirement: Edición de Tareas

El usuario debe poder modificar la descripción de una tarea existente.

#### Scenario: Editar Descripción

- **WHEN** el usuario selecciona la opción de editar en una tarea
- **THEN** se le solicita la nueva descripción.
- **AND** al confirmar, la tarea se actualiza en la lista y en la base de datos.
