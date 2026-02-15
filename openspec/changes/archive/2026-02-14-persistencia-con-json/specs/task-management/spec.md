# task-management Delta Specification

## MODIFIED Requirements

### Requirement: Add Task

The system SHALL allow users to add a new task to the list and persist it to storage.

#### Scenario: User adds a task successfully

- **WHEN** the user selects option "1" (Agregar tarea)
- **THEN** the system prompts for the task description
- **AND** when the user enters a non-empty description
- **THEN** the task is added to the list
- **AND** the system saves the updated list to `tareas.json`
- **AND** the system displays a confirmation message
- **AND** the menu is displayed again

#### Scenario: User tries to add an empty task

- **WHEN** the user enters an empty or whitespace-only description
- **THEN** the system displays an error message "La tarea no puede estar vacía"
- **AND** prompts again for the task description

---

### Requirement: Delete Task

The system SHALL allow users to delete a task by its index number and persist the change to storage.

#### Scenario: User deletes a task successfully

- **WHEN** the user selects option "3" (Eliminar tarea)
- **THEN** the system displays the current task list
- **AND** prompts for the task number to delete
- **AND** when the user enters a valid index
- **THEN** the task is removed from the list
- **AND** the system saves the updated list to `tareas.json`
- **AND** the system displays a confirmation message
- **AND** the menu is displayed again

#### Scenario: User enters invalid task number

- **WHEN** the user enters a non-numeric value
- **THEN** the system displays an error message "Ingrese un número válido"

#### Scenario: User enters out-of-range task number

- **WHEN** the user enters a number outside the valid range
- **THEN** the system displays an error message "Número de tarea fuera de rango"
